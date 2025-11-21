using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Types;

namespace XMPS2000
{
    internal class PLCForceFunctionality
    {
        private static Int32 port = 1169;

        public static string Tftpaddress = "";

        private static PLCForceFunctionality instance = null;

        public static PLCForceFunctionality Instance
        {
            get
            {
                if (instance == null)
                    instance = new PLCForceFunctionality();

                return instance;
            }
        }

        public static void ConfigureTftpaddress()
        {
            string errmsg = XMPS2000.CommonFunctions.GetEasyConnection(XMPS.Instance._connectedIPAddress);
            PLCCommunications pLCCommunications = new PLCCommunications();
            if (pLCCommunications.GetIPAddress() != "Error")
                PLCForceFunctionality.Tftpaddress = pLCCommunications.Tftpaddress.ToString();
            else
                MessageBox.Show(errmsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool PLCAlive()
        {
            Ping x = new Ping();
            if (string.IsNullOrEmpty(Tftpaddress))
                ConfigureTftpaddress();
            PingReply reply = x.Send(System.Net.IPAddress.Parse(Tftpaddress));
            if (reply.Status == IPStatus.Success)
                return true;

            return false;
        }
        public string CreateAndSendFrame(string logicalAddress, string value, bool isforce = true)
        {
            byte[] dataFrame = new byte[250];
            int frmindex = 0;
            Int16 inputStr;
            string memoryval = "";
            string CRC = "0";
            int getcrc = 0;
            int datalength = 0;
            int totlength = 0;
            string tagname = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == logicalAddress).Select(t => t.Tag).FirstOrDefault();
            string data = tagname;
            string datatype = XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == logicalAddress.ToString() && L.Type == IOType.DataType).Select(T => T.Label).FirstOrDefault();
            datatype = datatype == null ? logicalAddress.Contains(".") ? "Bool" : XMPS.Instance.LoadedProject.CPUDatatype : datatype;
            if (logicalAddress.Contains(".") || logicalAddress.StartsWith("F2") || datatype == "Bool" || datatype == "Byte")
            {
                datalength = 1;
            }
            else if (datatype == "Real" || datatype == "Double Word" || datatype == "DINT" || datatype == "UDINT")  //if (Int16.TryParse(setValueDic[data].Value, out short val) && (datatype == null || datatype == "Word"))
                datalength = 4;
            else
                datalength = 2;

            dataFrame[frmindex] = Convert.ToByte(datalength.ToString("X"));
            CRC = Convert.ToInt32(getcrc).ToString("X");
            getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(datalength.ToString(), 16);
            frmindex++;

            memoryval = XMPS.Instance.GetHexAddressForOnlineMonoitoring(logicalAddress);
            inputStr = Int16.Parse(memoryval, System.Globalization.NumberStyles.HexNumber);
            byte[] frame = BitConverter.GetBytes(inputStr);
            for (int cnt = 0; cnt < frame.Length; cnt++)
            {
                dataFrame[frmindex] = frame[cnt];
                frmindex++;
                CRC = Convert.ToInt32(getcrc).ToString("X");
                getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(frame[cnt].ToString("X"), 16);
            }

            if (datalength == 1)
            {
                dataFrame[frmindex] = Convert.ToByte(value);
                frmindex++;
                CRC = Convert.ToInt32(getcrc).ToString("X");
                getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(value);
            }
            else if (datalength == 2)
            {
                byte[] valframe;
                if (logicalAddress.ToString().StartsWith("W") && datatype == "Int")
                    valframe = BitConverter.GetBytes(Convert.ToInt16(value));
                else
                    valframe = BitConverter.GetBytes(Convert.ToUInt16(value));

                for (int cnt = 0; cnt < valframe.Length; cnt++)
                {
                    dataFrame[frmindex] = valframe[cnt];
                    frmindex++;
                    CRC = Convert.ToInt32(getcrc).ToString("X");
                    getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(valframe[cnt]);
                }
            }
            else if (datalength == 4)
            {
                byte[] valframe;
                if (datatype == "Real")
                    if (value.ToString().Contains("."))
                        valframe = BitConverter.GetBytes(Convert.ToSingle(value));
                    else
                        valframe = BitConverter.GetBytes(Convert.ToInt32(value));
                else if (datatype == "DINT")
                    valframe = BitConverter.GetBytes(Convert.ToInt32(value));
                else
                    valframe = BitConverter.GetBytes(Convert.ToUInt32(value));

                for (int cnt = 0; cnt < valframe.Length; cnt++)
                {
                    dataFrame[frmindex] = valframe[cnt];
                    frmindex++;
                    CRC = Convert.ToInt32(getcrc).ToString("X");
                    getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(valframe[cnt]);
                }
            }
            if (frmindex != 0)
            {
                totlength = frmindex;
                CRC = Convert.ToInt32(getcrc).ToString("X");
                int conctc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32("97", 16);
                CRC = Convert.ToInt32(conctc).ToString("X");
                inputStr = Int16.Parse(CRC, System.Globalization.NumberStyles.HexNumber);
                byte[] crcframe = BitConverter.GetBytes(inputStr);
                dataFrame[frmindex] = crcframe[0];
                frmindex++;
            }
            if (isforce)
                return SendForceFrame(dataFrame.Take(frmindex).ToArray(), totlength);
            else
                return SendUnforceFrame(dataFrame.Take(frmindex).ToArray(), totlength);

        }
        public string SendForceFrame(byte[] forcedata, int frmlength)
        {
            if (PLCAlive())
            {
                byte[] Frame = new byte[250];

                Frame = ForceFrame(forcedata, frmlength);

                int dataExchange = PLCDataExchange(Tftpaddress, Frame);

                if (dataExchange == 187)///Reply sent when updated sucessfully
                {
                    XMPS.Instance.isforced = true;
                    return ("Values selected are forced sucessfully");
                }
                else if (dataExchange == 170)///Reply sent when error in updating
                {
                    return ("Error while forcing the selected values");
                }
                else ///Reply sent when error 
                {
                    return ("Error while forcing the values to PLC");
                }

            }
            return "No response from PLC";
        }

        public string SendUnforceFrame(byte[] forcedata, int frmlength)
        {
            if (PLCAlive())
            {
                byte[] Frame = new byte[250];

                Frame = UnforceFrame(forcedata, frmlength);

                int dataExchange = PLCDataExchange(Tftpaddress, Frame);

                if (dataExchange == 187)///Reply sent when updated sucessfully
                {
                    XMPS.Instance.isforced = false;
                    return ("Values selected are unforced sucessfully");
                }
                else if (dataExchange == 170)///Reply sent when error in updating
                {
                    return ("Error while unforcing the selected values");
                }
                else ///Reply sent when error 
                {
                    return ("Error while unforcing the values to PLC");
                }

            }
            return "No response from PLC";

        }

        public string SendUnforceAllFrame()
        {
            try
            {
                if (PLCAlive())
                {
                    byte[] Frame = new byte[5];

                    Frame = UnforceAllFrame();

                    int dataExchange = PLCDataExchange(Tftpaddress, Frame);
                    if (dataExchange == 187)///Reply sent when updated sucessfully
                    {
                        XMPS.Instance.isforced = false;
                        return ("All values unforced sucessfully");
                    }
                    else if (dataExchange == 170)///Reply sent when error in updating
                    {
                        return ("Error while unforcing all values");
                    }
                    else ///Reply sent when error 
                    {
                        return ("Error while unforcing all values to PLC");
                    }
                }

                return "No response from PLC";
            }
            catch (Exception e) { return "Error while unforcing all values to PLC" + e.Message; }
        }

        public int PLCDataExchange(string server, byte[] message)
        {
            try
            {
                TcpClient client = new TcpClient(server, port);

                //  Stream stream = client.GetStream();
                bool eof = false;
                bool sof = false;
                int CheckCRC = 0;
                int Gpktlength = 0;
                NetworkStream stream = client.GetStream();

                Byte[] CRC = new Byte[250];
                Byte[] CRCPacket = new Byte[250];
                Byte[] mainstring = new Byte[250];
                Byte[] ActVal = new Byte[4];

                stream.Write(message, 0, message.Length);

                NetworkStream Gstream = client.GetStream();

                int i;
                string Gdata = null;
                String responseData = String.Empty;
                byte[] bytes = new byte[6];
                Gstream.ReadTimeout = 2000;
                bool gotval = false;
                int responceindex = -1;
                while ((i = Gstream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    int msgindex = Array.FindLastIndex(bytes, value => value != 0);
                    for (int j = 0; j <= msgindex; j++)
                    {
                        byte[] gotbyte = new byte[1];
                        gotbyte[0] = bytes[j];

                        Gdata = BitConverter.ToString(gotbyte, 0, 1);

                        //Check the index of last found value in the array for both array's Main and CRC
                        var mainindex = responceindex;//Array.FindLastIndex(mainstring, value => value != 0);
                        var crcindex = Array.FindLastIndex(CRCPacket, value => value != 0);
                        if (Gdata.ToString() == "F7" && mainindex == -1)
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
                        else if (Gdata.ToString() == "CA" || Gdata.ToString() == "CB")
                        {
                            mainstring[responceindex] = 0xCA;
                            //Do nothing at end of file just write the value in main string
                            responseData = BitConverter.ToString(mainstring.ToArray(), 0, responceindex + 1);
                        }
                        else if (Gdata.ToString() == "F6" && crcindex != -1)
                        {
                            mainstring[responceindex] = 0xF6;
                            //Do nothing at end of file just write the value in main string
                            responseData = BitConverter.ToString(mainstring.ToArray(), 0, responceindex + 1);
                            eof = true;
                        }
                        else
                        {
                            //Check index of Main file and value sent from server
                            var mindex = responceindex;//Array.FindLastIndex(mainstring, value => value != 0);

                            if (sof)
                            {
                                //If Start of File is already received then set it to false now
                                sof = false;
                                //Get the total length of data being sent
                                Gpktlength = int.Parse(Gdata, System.Globalization.NumberStyles.HexNumber);
                                //Enter the value in mainstring
                                mainstring[responceindex] = gotbyte[0];
                            }
                            else if (gotval)
                            {
                                //If Total data being sent is less than index of current data then trat it as data byte and add the same in CRC data for CRC calculation and also add in main string
                                mainstring[responceindex] = gotbyte[0];
                                System.Buffer.BlockCopy(gotbyte.ToArray(), 0, CRCPacket, 0, 1);
                            }
                            else
                            {
                                //Check the value in data and perform the XOR operation on CRC byte as this is CRC value
                                mainstring[responceindex] = gotbyte[0];
                                ActVal[0] = gotbyte[0];
                                gotval = true;
                                CheckCRC = CheckCRC ^ int.Parse(Gdata, System.Globalization.NumberStyles.HexNumber);
                            }
                            gotbyte.Initialize();
                            responceindex = responceindex + 1;
                        }
                        if (eof) break;
                    }
                    Gstream.Flush();
                    bytes = new Byte[250];

                    //Check if cumputed CRC and CRC sent by server are same 
                    string chekcgcrc = CRCPacket[0].ToString();
                    CheckCRC = CheckCRC ^ Convert.ToInt32("97", 16);
                    stream.Close();
                    client.Close();
                    if (chekcgcrc.ToString() == CheckCRC.ToString())
                    {

                        return (BitConverter.ToInt16(ActVal, 0));
                    }
                    else
                    {
                        return 0;
                    }

                }
                return 0;
            }
            catch (Exception e) { MessageBox.Show(e.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error); return 0; }
        }

        private byte[] ForceFrame(byte[] forcedata, int frmlength)
        {
            int frameindex = 0;
            byte[] forceFrame = new byte[250];
            forceFrame[frameindex] = 0xF7;
            frameindex++;
            forceFrame[frameindex] = 0xCA;
            frameindex++;
            forceFrame[frameindex] = Convert.ToByte(frmlength);
            frameindex++;
            for (int cnt = 0; cnt < forcedata.Length; cnt++)
            {
                forceFrame[frameindex] = forcedata[cnt];
                frameindex++;
            }
            forceFrame[frameindex] = 0XF6;
            frameindex++;
            return forceFrame;
        }
        private byte[] UnforceFrame(byte[] forcedata, int frmlength)
        {
            int frameindex = 0;
            byte[] unforceFrame = new byte[250];
            unforceFrame[frameindex] = 0xF7;
            frameindex++;
            unforceFrame[frameindex] = 0xCB;
            frameindex++;
            unforceFrame[frameindex] = Convert.ToByte(frmlength);
            frameindex++;
            for (int cnt = 0; cnt < forcedata.Length; cnt++)
            {
                unforceFrame[frameindex] = forcedata[cnt];
                frameindex++;
            }
            unforceFrame[frameindex] = 0XF6;
            return unforceFrame;
        }
        private byte[] UnforceAllFrame()
        {
            byte[] unforceFrame = new byte[5];
            unforceFrame[0] = 0xF7;
            unforceFrame[1] = 0xCC;
            unforceFrame[2] = 0xBB;
            unforceFrame[3] = 0x0B;
            unforceFrame[4] = 0xF6;
            XMPS.Instance.Forcedvalues.Clear();
            return unforceFrame;
        }
    }
}
