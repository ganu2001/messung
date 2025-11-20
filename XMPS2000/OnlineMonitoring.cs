using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using XMPS2000.Core;
using static iTextSharp.text.pdf.events.IndexEvents;
using XMPS2000.Core.Types;
using System.Threading.Tasks;
using RawPrint;
using System.Threading;

namespace XMPS2000
{
    internal class OnlineMonitoring
    {
        XMPS xm;
        private static Int32 port = 1169;
        static List<byte> SendDataFrame = new List<byte>();
        static List<byte> tempDataFrame = new List<byte>();
        static int responceindex = 0;
        static int connectionfailed = 0;
        private bool isConnected = false;
        TcpClient client;
        private NetworkStream stream;
        // Static instance variable
        private static OnlineMonitoring _instance;
        // Lock object for thread safety
        private static readonly object _lock = new object();
        // Connection state management
        private bool _isConnected;
        private DateTime _lastSuccessfulCommunication;
        private readonly object _connectionLock = new object();
        // Connection timeout setting(set to 0 to disable timeout)
        private readonly TimeSpan _connectionTimeout = TimeSpan.FromMinutes(5); // Change to TimeSpan.Zero to disable
        private TcpHealthMonitor monitor;

        public OnlineMonitoring()
        {
            xm = XMPS.Instance;
        }

        // Public static method to get the instance
        public static OnlineMonitoring GetInstance()
        {
            // Check if instance exists
            if (_instance == null)
            {
                _instance = new OnlineMonitoring();
            }
            return _instance;
        }

        public static void DestroyInstance()
        {
            if (_instance != null)
            {
                // Perform cleanup if necessary
                _instance.CloseConnection();
                _instance = null;
            }
        }


        /// <summary>
        /// Create data frame to send and assing the received data to known address
        /// </summary>
        /// <param name="activeAddress">List of addresses for online monitoring</param>                ---> TagName => 
        /// <param name="addressInfoDic">Dictionary with current Logic Block tags</param>             ---> (Tagname,Address,Type)
        /// <param name="AddressValues">Dictionary will contain address and their value</param>       ----> (Tagname , "Recevied Value as Response")
        /// <param name="Result">Result for the current operation</param>
        public void GetValues(List<string> activeAddress, ref Dictionary<string, Tuple<string, AddressDataTypes>> addressInfoDic, ref Dictionary<string, string> AddressValues, out string Result)
        {
            try
            {

                Result = "";

                UInt16 inputStr;
                int framelength;

                string CRC = "0";
                int totalDataLength = 0;
                int onlyDataLength = 0;
                string curAddress = "";
                string memoryval = "";

                byte[] frame = new byte[2];

                SendDataFrame.Clear();
                tempDataFrame.Clear();
                OnlineMonitoringStatus.isOnlineMonitoring = true;
                Result = "Making Frame to send the data to PLC.....";

                // Start frame creation
                // Add SOC to frame
                AddtoSendByteList(0xFC);

                // Start of converting the address into data and storing in temp list
                // Adds (length and data<address>) to temp list data frame
                inputStr = 0;
                foreach (string address in activeAddress)
                {
                    curAddress = address.Replace("~", "");
                    memoryval = XMPS.Instance.GetHexAddressForOnlineMonoitoring(addressInfoDic[curAddress].Item1);

                    int getcrc = 0;
                    inputStr = (UInt16)AddressDataTypesValue.AddressValues[addressInfoDic[curAddress].Item2];
                    totalDataLength += 3;
                    onlyDataLength += inputStr;
                    getcrc = (Int32)AddressDataTypesValue.AddressValues[addressInfoDic[curAddress].Item2];

                    CRC = getcrc.ToString("X");

                    AddtoDataByteList(Convert.ToByte(inputStr));
                    inputStr = Convert.ToUInt16(memoryval.ToString(), 16); // Int16.Parse(memoryval, System.Globalization.NumberStyles.HexNumber);
                    frame = BitConverter.GetBytes(inputStr);
                    for (int i = 0; i < frame.Length; i++)
                    {
                        AddtoDataByteList(frame[i]);
                    }
                    getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(memoryval.ToString(), 16);
                    CRC = Convert.ToInt32(getcrc).ToString("X");
                }

                // Create CRC for the current temp list data frame
                CRC = "0";
                foreach (byte b in tempDataFrame)
                {
                    CRC = (Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(b.ToString())).ToString("X");
                }
                int conctc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32("97", 16);
                CRC = Convert.ToInt32(conctc).ToString("X");
                inputStr = UInt16.Parse(CRC, System.Globalization.NumberStyles.HexNumber);
                frame = BitConverter.GetBytes(inputStr);
                framelength = Array.FindLastIndex(frame, value => value != 0);
                for (int i = 0; i <= framelength; i++)
                {
                    AddtoDataByteList(frame[i]);
                }

                // Add Total Data Length to frame
                AddtoSendByteList((byte)totalDataLength);

                // Add n(Length and code) to frame
                SendDataFrame.AddRange(tempDataFrame);

                string result = "";

                // Send PLC request frame and check responce frame
                Byte[] response = ConnectAndExchange(xm._connectedIPAddress, onlyDataLength, 0xFA, out result);
                // Check for responce
                if (result == "Failed to connect,Online Monitoring is stopped")
                {
                    CloseConnection();
                    Result = result;
                    return;
                }
                if (response != null && result == "CRC Matched preparing to show data")
                {
                    // Assign the value to respective address
                    int skipCount = 2;
                    foreach (string address in activeAddress)
                    {
                        curAddress = address.Replace("~", "");
                        int addressRange = (Int16)AddressDataTypesValue.AddressValues[addressInfoDic[curAddress].Item2];
                        if (addressRange > 1)
                        {
                            if (addressRange == 2)
                            {
                                if (addressInfoDic[curAddress].Item2 == AddressDataTypes.WORD)
                                    AddressValues[address] = $"{BitConverter.ToUInt16(response.Skip(skipCount).Take(addressRange).ToArray(), 0)}";
                                else
                                    AddressValues[address] = $"{BitConverter.ToInt16(response.Skip(skipCount).Take(addressRange).ToArray(), 0)}";
                            }
                            else
                            {
                                if (addressInfoDic[curAddress].Item2 == AddressDataTypes.INT)
                                    AddressValues[address] = $"{BitConverter.ToInt32(response.Skip(skipCount).Take(addressRange).ToArray(), 0)}";
                                else if (addressInfoDic[curAddress].Item2 == AddressDataTypes.DWORD)
                                    AddressValues[address] = $"{BitConverter.ToUInt32(response.Skip(skipCount).Take(addressRange).ToArray(), 0)}";
                                else if (addressInfoDic[curAddress].Item2 == AddressDataTypes.UDINT)
                                    AddressValues[address] = $"{BitConverter.ToUInt32(response.Skip(skipCount).Take(addressRange).ToArray(), 0)}";
                                else if (addressInfoDic[curAddress].Item2 == AddressDataTypes.DINT)
                                    AddressValues[address] = $"{BitConverter.ToInt32(response.Skip(skipCount).Take(addressRange).ToArray(), 0)}";
                                else if (addressInfoDic[curAddress].Item2 == AddressDataTypes.STRING)
                                    AddressValues[address] = GetStringData($"{BitConverter.ToString(response.Skip(skipCount).Take(addressRange).ToArray(), 0)}");
                                else
                                {
                                    AddressValues[address] = $"{BitConverter.ToSingle(response.Skip(skipCount).Take(addressRange).ToArray(), 0)}";
                                    if (AddressValues[address].ToString().Contains("N")) // When NAN message is shown indicating Not a number then convert with number logic or else show value as it is even if it is e rasie to something
                                        AddressValues[address] = $"{BitConverter.ToInt32(response.Skip(skipCount).Take(addressRange).ToArray(), 0)}";
                                }
                            }
                        }
                        else
                            AddressValues[address] = $"{response[skipCount]}";

                        skipCount += addressRange;
                    }
                }
                else
                {
                    Result = result + " Data not received properly";
                    return;
                }
            }
            catch (Exception er)
            {
                CloseConnection();
                Result = er.Message;
                return;
            }
        }

        /// <summary>
        /// Get hex value and share the string data converted out of that hex value
        /// </summary>
        /// <param name="strHexValue"></param>
        /// <returns></returns> show string data
        private string GetStringData(string strHexValue)
        {
            // Return empty string if input is null or empty
            if (string.IsNullOrWhiteSpace(strHexValue))
            {
                return string.Empty;
            }

            try
            {
                // Split the hex string by the dash "-" character
                string[] hexValues = strHexValue.Split('-');

                // Handle empty array case
                if (hexValues.Length == 0)
                {
                    return string.Empty;
                }

                byte[] bytes = new byte[hexValues.Length];

                for (int i = 0; i < hexValues.Length; i++)
                {
                    // Skip empty values and continue processing
                    if (string.IsNullOrWhiteSpace(hexValues[i]))
                    {
                        continue;
                    }

                    // Trim any whitespace that might be present
                    string cleanHex = hexValues[i].Trim();

                    // Try to parse the hex string, use 0 as default if parsing fails
                    if (!byte.TryParse(cleanHex, System.Globalization.NumberStyles.HexNumber,
                                       System.Globalization.CultureInfo.InvariantCulture, out bytes[i]))
                    {
                        bytes[i] = 0; // Default value if parsing fails
                    }
                }

                // Convert bytes to ASCII string, handle potential encoding errors
                try
                {
                    string result = System.Text.Encoding.ASCII.GetString(bytes);

                    // Handle non-printable characters if needed
                    char[] resultChars = result.ToCharArray();
                    for (int i = 0; i < resultChars.Length; i++)
                    {
                        // Replace non-printable characters with a space
                        if (resultChars[i] < 32 || resultChars[i] > 126)
                        {
                            resultChars[i] = ' ';
                        }
                    }

                    return new string(resultChars);
                }
                catch (EncoderFallbackException)
                {
                    // Fallback to a safe encoding of the bytes
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        // Only append printable ASCII characters
                        if (b >= 32 && b <= 126)
                        {
                            sb.Append((char)b);
                        }
                        else
                        {
                            sb.Append(' ');
                        }
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                // Return empty string or error indicator based on requirements
                return string.Empty; // or "[Error]" depending on your error handling strategy
            }
        }

        /// <summary>
        /// Add Byte to temp data list. Temp list will only contain data part of frame
        /// </summary>
        /// <param name="message">Message to be sent</param>
        /// <returns></returns>
        public string AddtoDataByteList(byte message)
        {
            try
            {
                byte[] data = new byte[1];
                data[0] = message;
                tempDataFrame.Add(data[0]);
                return "Success";
            }
            catch (ArgumentException e)
            {
                return (e.Message);
            }
            catch (SocketException e)
            {
                return (e.Message);
            }
        }

        /// <summary>
        /// Add Byte to main data list. Main data frame
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string AddtoSendByteList(byte message)
        {
            try
            {
                byte[] data = new byte[1];
                data[0] = message;
                SendDataFrame.Add(data[0]);
                return "Success";
            }
            catch (ArgumentException e)
            {
                return (e.Message);
            }
            catch (SocketException e)
            {
                return (e.Message);
            }
        }

        /// <summary>
        /// Establishes connection to PLC - extracted from your original code
        /// </summary>
        public bool EstablishConnection(string server, out string Result)
        {
            try
            {
            // Your original connection logic
            starter:
                client = new TcpClient();
                client.NoDelay = true;
                client.LingerState = new LingerOption(true, 300); // 5 minutes
                                                                  // Much more effective than long linger time
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                // Set reasonable linger time
                client.LingerState = new LingerOption(true, 10); // 10 seconds, not 30,000!


                // Simulate some work

                Result = "Connecting to PLC.....";
                IAsyncResult result = client.BeginConnect(server, port, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3));

                if (!success)
                {
                    client.Close();
                    connectionfailed = connectionfailed + 1;
                    if (connectionfailed > 1)
                    {
                        connectionfailed = 0;
                        stopmonitor(); // Your existing method
                        Result = "Failed to connect,Resolve The Issue And Start The Monitoring Again";
                        Result = "Failed to connect,Online Monitoring is stopped";
                        return false;
                    }
                    else
                    {
                        Result = "Connection Failed " + connectionfailed + " times ";
                        goto starter;
                    }
                }
                else
                {
                    Result = "Connected to PLC....";
                    connectionfailed = 0;

                    // Get the stream for communication
                    stream = client.GetStream();
                    isConnected = true;
                    return true;
                }

                monitor = new TcpHealthMonitor(client);
                // Subscribe to health events
                monitor.HealthChanged += (sender, args) =>
                {
                    Console.WriteLine($"Health changed: {args.PreviousHealth?.Status} -> {args.CurrentHealth.Status}");
                };

                monitor.HealthAlert += (sender, message) =>
                {
                    Console.WriteLine($"ALERT: {message}");
                };

                // Start monitoring every 2 seconds
                monitor.StartMonitoring(2000);

            }
            catch (ArgumentNullException e)
            {
                Result = e.Message;
                return false;
            }
            catch (SocketException e)
            {
                Result = e.Message;
                return false;
            }
        }
        /// <summary>
        /// Check if connection is still active
        /// </summary>
        public bool IsConnectionActive()
        {
            return isConnected && client != null && client.Connected;
        }
        // Check if connection is still valid
        private bool IsConnectionValid()
        {
            if (!_isConnected)
                return false;

            // Skip timeout check if timeout is disabled (set to Zero)
            if (_connectionTimeout == TimeSpan.Zero)
                return true;

            // Check if connection has timed out
            var timeSinceLastSuccess = DateTime.Now - _lastSuccessfulCommunication;
            if (timeSinceLastSuccess > _connectionTimeout)
            {
                Console.WriteLine("Connection timed out - will reconnect");
                return false;
            }

            return true;

        }

        // Ensure connection is active - close and reopen if needed
        private bool EnsureConnection(string server, out string result)
        {
            result = "Connected ...";
            // If connection is valid, use it
            if (IsConnectionValid() && client != null)
                return true;

            // Close existing connection if it exists
            if (_isConnected)
            {
                CloseConnection();
            }

            // Open new connection
            return OpenNewConnection(server, out result);

        }

        // Open a new connection
        private bool OpenNewConnection(string server, out string result)
        {
            try
            {
                // Your actual connection logic here
                Connect(server, out result);
                _isConnected = true;
                _lastSuccessfulCommunication = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                _isConnected = false;
                result = "Connection failed..." + ex.Message;
                return false;
            }
        }

        private void Connect(string server, out string result)
        {
            // Replace with your actual connection logic
            EstablishConnection(server, out result);
        }

        void SendData(NetworkStream stream, byte[] data)
        {
            const int maxRetries = 3;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    int totalSent = 0;
                    int size = data.Length;
                    while (totalSent < size)
                    {
                        int toSend = size - totalSent;
                        stream.Write(data, totalSent, toSend);
                        totalSent += toSend; // In reality, NetworkStream.Write sends all requested bytes or throws
                    }
                    stream.Flush();
                    return; // Success - no wait needed!
                }
                catch (IOException)
                {
                    if (attempt == maxRetries)
                        Thread.Sleep(100 * attempt);
                }
            }
        }

        /// <summary>
        /// Send data to tcp client and validate the responce data
        /// </summary>
        /// <param name="server">Ip address of the server</param>
        /// <param name="pktLength"> Length of data</param>
        /// <param name="message">Message to be sent (end of file message)</param>
        /// <param name="Result">Result in output to show</param>
        /// <returns>Return the data recived in array of bytes</returns>
        public byte[] ConnectAndExchange(string server, int pktLength, byte message, out string Result)
        {
            if (client == null)
                CloseConnection();
            // Ensure connection is established
            if (!EnsureConnection(server, out Result))
                return null;
            try
            {
                client.NoDelay = true;
                client.LingerState = new LingerOption(true, 300); // 5 minutes
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                byte[] inOptionValues = new byte[12];
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);      // Enable
                BitConverter.GetBytes((uint)30000).CopyTo(inOptionValues, 4);  // Delay (30s)
                BitConverter.GetBytes((uint)100000).CopyTo(inOptionValues, 8);  // Interval (10s)
                client.Client.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);


                int maxAttempts = 3;          // 1 original + 2 retries = 3 attempts
                int timeoutMs = 2000;         // 2 seconds
                byte[] mainstring = null;
                for (int attempt = 1; attempt <= maxAttempts; attempt++)
                {
                    try
                    {
                        Result = "Connected to PLC....";
                        connectionfailed = 0;

                        int CheckCRC = 0;
                        bool sof = false;
                        bool eof = false;
                        int Gpktlength = 0;
                        EnsureConnection(server, out Result);
                        mainstring = new byte[pktLength + 4];    // +4 for EOF,SOF, Total data length AND CRC
                        Byte[] CRCPacket = new Byte[1];

                        // Get a client stream for reading and writing
                        NetworkStream stream = client.GetStream();
                        Result = "Sending Data to PLC....";

                        // Append EOF to data frame and convert to array
                        if (attempt == 1) AddtoSendByteList(message);
                        byte[] FinalDataToSend = SendDataFrame.ToArray();
                        // Send the data frame to PLC
                        //Task.Delay(150).Wait();
                        //Thread.Sleep(150);
                        stream.Flush();
                        // Get the underlying socket
                        Socket socket = client.Client;
                        socket.NoDelay = true;
                        SendData(stream, FinalDataToSend);
                        //Task.Delay(50).Wait();
                        //stream.Write(FinalDataToSend, 0, FinalDataToSend.Length);
                        string Gdata = null;
                        client.NoDelay = true;
                        // Timeout for responce
                        stream.ReadTimeout = timeoutMs;
                        string responseData = string.Empty;

                        // Read the first batch of the TcpServer response bytes.
                        EnsureConnection(server, out Result);
                        NetworkStream Gstream = client.GetStream();

                        int i;
                        byte[] bytes = new byte[pktLength + 4];     // +4 for EOF,SOF, Total data length AND CRC
                        responceindex = -1;
                        var crcindex = -1;

                        // Loop to receive all the data sent by the client
                        Result = "Receiving Data from PLC.....";
                        while ((i = ReadExact(Gstream, ref bytes, 0, bytes.Length)) != 0)
                        {
                            int msgindex = Array.FindLastIndex(bytes, value => value != 0);
                            for (int j = 0; j <= msgindex; j++)
                            {
                                // Translate data bytes into ASCII string.
                                byte[] gotbyte = new byte[1];
                                gotbyte[0] = bytes[j];
                                Gdata = BitConverter.ToString(gotbyte, 0, 1);

                                int mainindex = responceindex;

                                // If the data is SOF
                                if (Gdata.ToString() == "FC" && mainindex == -1)
                                {
                                    //Intialise the array at the start of packet
                                    mainstring.Initialize();
                                    responceindex = 0;
                                    //Insert the value of FC in main string 
                                    System.Buffer.BlockCopy(gotbyte, 0, mainstring, 0, 1);
                                    //Set SOF start of File to true as we found it
                                    sof = true;
                                    //Set check CRC to 0 as we are now looking for it
                                    CheckCRC = 0;
                                }
                                // If the data is EOF
                                else if (Gdata.ToString() == "FA" && crcindex != -1)
                                {
                                    mainstring[responceindex] = 0xFA;
                                    //Do nothing at end of file just write the value in main string
                                    responseData = BitConverter.ToString(mainstring.ToArray(), 0, responceindex + 1);
                                    eof = true;
                                }
                                // For other data
                                else
                                {
                                    //Check index of Main file and value sent from server
                                    var mindex = responceindex;

                                    if (sof)
                                    {
                                        //If Start of File is already received then set it to false now
                                        sof = false;
                                        //Get the total length of data being sent
                                        Gpktlength = int.Parse(Gdata, System.Globalization.NumberStyles.HexNumber);
                                        //Enter the value in mainstring
                                        mainstring[responceindex] = gotbyte[0];
                                    }
                                    else if (j > (Gpktlength + 1))
                                    {
                                        //If Total data being sent is less than index of current data then trat it as data byte and add the same in CRC data for CRC calculation and also add in main string
                                        mainstring[responceindex] = gotbyte[0];
                                        System.Buffer.BlockCopy(gotbyte.ToArray(), 0, CRCPacket, 0, 1);
                                        crcindex = CRCPacket[0];
                                    }
                                    else
                                    {
                                        //Check the value in data and perform the XOR operation on CRC byte as this is CRC value
                                        mainstring[responceindex] = gotbyte[0];
                                        CheckCRC = CheckCRC ^ int.Parse(Gdata, System.Globalization.NumberStyles.HexNumber);
                                    }
                                }
                                gotbyte.Initialize();
                                responceindex = responceindex + 1;
                            }
                            if (eof) break;
                            Gstream.Flush();
                            bytes = new byte[pktLength + 4];    // +4 for EOF,SOF, Total data length AND CRC
                        }
                        Result = "Calculating CRC....";

                        // Check for CRC
                        string checkcgcrc = CRCPacket[0].ToString();
                        CheckCRC = CheckCRC ^ Convert.ToInt32("97", 16);

                        //stream.Close();
                        //client.Close();
                        ClearAllBuffersWithTimeout();
                        if (checkcgcrc.ToString() == CheckCRC.ToString())
                        {
                            Result = "CRC Matched preparing to show data";
                            return mainstring;
                        }
                        else
                        {
                            Result = "CRC is not matching....";
                            return null;
                        }

                    }
                    catch (IOException ioEx) when (ioEx.InnerException is System.Net.Sockets.SocketException sockEx && sockEx.SocketErrorCode == System.Net.Sockets.SocketError.TimedOut)
                    {
                        
                        stream.Flush();
                        CloseConnection(); 
                        Result = $"Timeout ({timeoutMs / 1000}s) waiting for PLC reply. Retrying... ({attempt}/{maxAttempts})";
                        if (attempt == maxAttempts) // If last attempt, give up
                        {
                            Result = "Failed to get PLC reply after 3 attempts.";
                            return null;
                        }
                        // Otherwise, just continue to next attempt
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Result = "Failed to get result " + e.Message;
                return null;
            }
            catch (SocketException e)
            {
                Result = "Failed to get result " + e.Message;
                return null;
            }
            return null;
        }

        // Method 5: Complete buffer clearing with timeout
        public void ClearAllBuffersWithTimeout(int timeoutMs = 1000)
        {
            try
            {
                // Set read timeout
                stream.ReadTimeout = timeoutMs;

                // Clear outbound
                stream.Flush();

                // Clear inbound with timeout protection
                byte[] buffer = new byte[4096];
                DateTime startTime = DateTime.Now;

                while (stream.DataAvailable &&
                       (DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;
                }

                Console.WriteLine("All buffers cleared with timeout protection");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing buffers: {ex.Message}");
            }
        }


        public int ReadExact(NetworkStream stream, ref byte[] buffer, int offset, int size)
        {
            int totalRead = 0;
            while (totalRead < size)
            {
                int bytesRead = stream.Read(buffer, offset + totalRead, size - totalRead);
                if (bytesRead == 0)
                {
                    // Connection closed before we got all data
                    CloseConnection();
                }
                totalRead += bytesRead;
            }
            return totalRead;
        }

        /// <summary>
        /// Stoping Online Monitor, This function wull change the status from online monitoring start to stop and 
        /// also reset the values which may have been changed while Online Montoring was start 
        /// and also this will close the client call for TCP so that any connection will get suspended 
        /// and new connection next time should not give any issues
        /// </summary>
        public void stopmonitor()
        {
            xm.onlinemonitoring = false;
            OnlineMonitoringStatus.isOnlineMonitoring = false;
            CloseConnection();
            if (!string.IsNullOrEmpty(xm.CurrentScreen) && !xm.CurrentScreen.Contains("LadderForm"))
                ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain")).DisableOnlineModeFromHSIO();
        }

        /// <summary>
        /// Closes the connection - new method
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
                if (client != null)
                {
                    client.Close();
                    client = null;
                }
                isConnected = false;
            }
            catch
            {
                // Ignore cleanup errors
            }
        }

        public static string GetAddressHex(string address)
        {
            //Applying Changes For Increased Bit Size
            int _addressInint = 0;
            string _afterColon = new string(address.SkipWhile(c => c != ':').Skip(1).ToArray());         //Get the address 
            //If Address is word & Boolean
            if (address.StartsWith("F") || address.StartsWith("W") || address.StartsWith("X") || address.StartsWith("Y"))
                _addressInint = Convert.ToInt32(_afterColon);

            var s = address[0];
            string hex = null;
            switch (s)
            {
                case 'I':
                    hex = GetInputAddressHex(address);
                    break;
                case 'Q':
                    hex = GetOutputAddressHex(address);
                    break;
                case 'F':
                    if (_addressInint > 255) //Greater than 256 have changed base address 
                    {
                        string baseaddress = address.Substring(0, address.IndexOf(':') + 1);
                        int number = Convert.ToInt32(address.Substring(address.IndexOf(':') + 1)) - 256;
                        hex = GetAddressHexGeneral(baseaddress + number.ToString(), "10763", 1);
                        //hex = GetAddressHexGeneral(address, "223A5823", 4);    //"0x2001D340".
                    }
                    else
                        hex = GetAddressHexGeneral(address, "523", 1);
                    break;
                case 'S':
                    hex = GetAddressHexGeneral(address, "779", 1);
                    break;
                case 'W':
                    if (_addressInint > 255)
                    //hex = GetAddressHexGeneral(address, "11531", 1);                              //Changed For Increase Size Greater than 256  {Convert Hex Code 2D0B}
                    {
                        string baseaddress = address.Substring(0, address.IndexOf(':') + 1);
                        int number = Convert.ToInt32(address.Substring(address.IndexOf(':') + 1)) - 256;
                        hex = GetAddressHexGeneral(baseaddress + number.ToString(), "11531", 1);    //0x2001D3A0

                    }
                    else
                        hex = GetAddressHexGeneral(address, "1035", 1);
                    break;
                case 'P':
                    hex = GetAddressHexGeneral(address, "1291", 1);
                    break;
                case 'T':
                    hex = GetAddressHexGeneral(address, "1547", 1);
                    break;
                case 'C':
                    hex = GetAddressHexGeneral(address, "1803", 1);
                    break;
                case 'X':
                    if (_addressInint > 255)
                    {
                        string baseaddress = address.Substring(0, address.IndexOf(':') + 1);
                        int number = Convert.ToInt32(address.Substring(address.IndexOf(':') + 1)) - 256;
                        hex = GetAddressHexGeneral(baseaddress + number.ToString(), "223B7400", 4);
                    }
                    else
                    {
                        hex = GetAddressHexGeneral(address, "223A0400", 4);         //For X8:000 =574227456,X8:001 =574227460 Note ==> Changed The Initial Value from previous
                    }
                    break;
                case 'Y':
                    if (_addressInint > 255)
                    {
                        string baseaddress = address.Substring(0, address.IndexOf(':') + 1);
                        int number = Convert.ToInt32(address.Substring(address.IndexOf(':') + 1)) - 256;
                        hex = GetAddressHexGeneral(baseaddress + number.ToString(), "2001DC00", 2);
                    }
                    else
                    {
                        hex = GetAddressHexGeneral(address, "2001D040", 2);//Fixed Y9:000 = 536989760  ,Y9:001= 536989762
                    }
                    break;
                case 'D':
                    hex = GetAddressHexGeneral(address, "223A8400", 4);
                    break;
                default: break;
            }
            return hex;
        }
        public static string GetAddressHexGeneral(string address, string initAddress, int nextByte)
        {
            string res = "";

            var num = int.Parse((address.Split(':'))[1]);

            int bytesToAdd = num * nextByte;
            var sum = Convert.ToInt32(initAddress) + bytesToAdd;
            //var sum = Convert.ToInt32(initAddress) + Convert.ToInt32(bytesToAdd);
            //res = sum.ToString();
            res = sum.ToString("X");

            return res;
        }
        public static string GetInputAddressHex(string address)
        {
            string res = "";
            if (address.Contains('.'))
            {
                string initAddress = "6155";

                var num = (address.Split(':'))[1];
                var num1 = int.Parse((num.Split('.'))[0]);
                var num2 = int.Parse((num.Split('.'))[1]);

                int bytesToAdd = ((num1 * 16) + num2);

                var sum = Convert.ToInt32(initAddress) + bytesToAdd;
                res = sum.ToString("X");
            }
            else
            {
                string initAddress = "267";

                var num = int.Parse((address.Split(':'))[1]);
                int bytesToAdd = num;

                var sum = Convert.ToInt32(initAddress) + bytesToAdd;
                res = sum.ToString("X");
            }
            return res;
        }

        public static string GetOutputAddressHex(string address)
        {
            string res = "";
            if (address.Contains('.'))
            {
                string initAddress = "2059";

                var num = (address.Split(':'))[1];
                var num1 = int.Parse((num.Split('.'))[0]);
                var num2 = int.Parse((num.Split('.'))[1]);

                int bytesToAdd = ((num1 * 16) + num2);

                var sum = Convert.ToInt32(initAddress) + bytesToAdd;
                res = sum.ToString("X");
            }
            else
            {
                string initAddress = "11";

                var num = int.Parse((address.Split(':'))[1]);
                int bytesToAdd = num;

                var sum = Convert.ToInt32(initAddress) + bytesToAdd;
                res = sum.ToString("X");
            }
            return res;
        }
    }
}
