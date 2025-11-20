using Microsoft.Win32;
using RawPrint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using XMPS2000.Core;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMPS2000
{
    internal class PLCCommunications
    {
        XMPS xm;
        public string Tftpaddress = "";
        private static Int32 port = 1169;
        static int responceindex = 0;
        public PLCCommunications()
        {
            xm = XMPS.Instance;
        }
        public (string, string) DownlodMcodeFile()
        {
            try
            {
                if (CheckPostConnectionSuccess() == IPStatus.Success)
                {
                    int _mCnt = xm.DownloadTFTPMcodeFile(Tftpaddress);
                    if (XMPS.Instance._noMcodeFrames == _mCnt)
                        return ("Mcode File Downloaded Successfully", "LimeGreen");
                    else
                        return ("Mcode File Download Failed...", "OrangeRed");
                }
                else
                {
                    return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, "OrangeRed");
            }

        }

        private IPStatus CheckPostConnectionSuccess()
        {
            try
            {
                if (GetIPAddress() == "Error")
                    return IPStatus.Unknown;
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
                return IPStatus.Success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return IPStatus.Unknown;
            }
        }

        public (string, string) DownlodCcodeFile()
        {
            try
            {
                if ((CheckPostConnectionSuccess() == IPStatus.Success))
                {
                    int _ccnt = xm.DownloadTFTPCcodeFile(Tftpaddress);
                    if (XMPS.Instance._noCcodeFrames == _ccnt)
                        return ("Ccode File Downloaded Successfully", "LimeGreen");
                    else
                        return ("Ccode File Download Failed...", "OrangeRed");
                }
                else
                {
                    return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
                }
            }
            catch (Exception ex)
            {
                return ("Error while sending C Code : " + ex.Message, "OrangeRed");
            }

        }

        public (string, string) DownlodMqttFile()
        {
            try
            {
                if ((CheckPostConnectionSuccess() == IPStatus.Success))
                {
                    int _cntCnt = xm.DownloadTFTPMqttFile(Tftpaddress);
                    if (_cntCnt == XMPS.Instance._noCnfFrames)
                        return ("MQTT CNF File Downloaded Successfully", "LimeGreen");
                    else
                        return ("MQTT CNF File Download Failed...", "OrangeRed");

                }
                else
                {
                    return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, "OrangeRed");
            }

        }

        public void ReplaceIPAddress()
        {
            Ethernet ethernet = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
            ethernet.EthernetIPAddress = ethernet.ChangeIPAddress.ToString() != "0.0.0.0" ? ethernet.ChangeIPAddress : ethernet.EthernetIPAddress;
            ethernet.EthernetGetWay = ethernet.ChangeGetWay.ToString() != "0.0.0.0" ? ethernet.ChangeGetWay : ethernet.EthernetGetWay;
            ethernet.EthernetSubNet = ethernet.ChangeSubNet.ToString() != "0.0.0.0" ? ethernet.ChangeSubNet : ethernet.EthernetSubNet;
            ethernet.ChangeIPAddress = IPAddress.Parse("0.0.0.0");
            ethernet.ChangeGetWay = IPAddress.Parse("0.0.0.0");
            ethernet.ChangeSubNet = IPAddress.Parse("0.0.0.0");
        }

        public (string, string) DownlodBcodeFile()
        {
            try
            {
                Ethernet ethernet = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
                if ((CheckPostConnectionSuccess() == IPStatus.Success))
                {
                    int _bCodeCnt = xm.DownloadTFTPBCodeFile(Tftpaddress);

                    if (_bCodeCnt == XMPS.Instance._noBcodeFrames)
                        return ("BCode File Downloaded Successfully", "LimeGreen");
                    else
                        return ("BCode File Download Failed...", "OrangeRed");
                }
                else
                {
                    return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, "OrangeRed");
            }

        }

        public (string, string) DownlodFiles()
        {
            try
            {
                Ethernet ethernet = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
                if ((CheckPostConnectionSuccess() == IPStatus.Success))
                {
                    xm.DownloadTFTPFile(Tftpaddress);

                    ethernet.EthernetIPAddress = IPAddress.Parse(Tftpaddress);
                    if (ethernet.ChangeIPAddress.ToString() != "0.0.0.0")
                    {
                        ethernet.EthernetIPAddress = ethernet.ChangeIPAddress;
                        ethernet.EthernetGetWay = ethernet.ChangeGetWay;
                        ethernet.EthernetSubNet = ethernet.ChangeSubNet;
                        ethernet.ChangeIPAddress = IPAddress.Parse("0.0.0.0");
                        ethernet.ChangeGetWay = IPAddress.Parse("0.0.0.0");
                        ethernet.ChangeSubNet = IPAddress.Parse("0.0.0.0");
                    }
                    return (" MQTT_CNF File Downloaded Successfully", "LimeGreen");
                }
                else
                {
                    return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, "OrangeRed");
            }

        }
        public (string, string) PLCLogin()
        {
            try
            {

                if (GetIPAddress() == "Error")
                    return (" Invalid Ip Address ", "OrangeRed");
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
                int response = Connect(Tftpaddress, loginframe(), true);
                if (response == 170)///Reply sent when PLC is in Run Mode
                {
                    return ("PLC is in Run Mode", "DodgerBlue");
                }
                else if (response == 85)///Reply sent when PLC is in Stop Mode
                {
                    return ("PLC is in Stop mode", "Gold");
                }
                else/// Reply sent when error occures or CRC is incorrect
                {
                    return ("Invalid Responce", "OrangeRed");
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, "OrangeRed");
            }
        }

        public Byte[] PLCSyncLogin()
        {
            try
            {
                if (GetIPAddress() == "Error")
                {
                    Byte[] resp = new byte[1];
                    resp[0] = 255;
                    return resp;
                }
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
                return ConnectSynchronization(Tftpaddress, loginframe());
            }
            catch (Exception ex)
            {
                Byte[] resp = new byte[1];
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return resp;
            }
        }

        public string ResetPLC(string condition)
        {
            if (GetIPAddress() == "Error")
                return (" Invalid Ip Address ");
            Byte[] Reset = new Byte[10];
            Ping x = new Ping();
            PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
            if ((reply.Status == IPStatus.Success))
            {
                if (condition == "Warm")
                {
                    Reset = ResetWarmPLCFrame();
                    //"PLC Reset Warm Command Sent";
                }
                else if (condition == "Cold")
                {

                    Reset = ResetColdPLCFrame();
                    //"PLC Reset Cold Command Sent";
                }
                else if (condition == "Origin")
                {

                    Reset = ResetOriginPLCFrame();
                    //"PLC Reset Origin Command Sent";
                }
                int responce = Connect(Tftpaddress, Reset, false);
                if (responce == 0)
                {
                    return ("Waiting for PLC to Respond.................");
                }
                else
                {
                    return ("Couldn't send Stop PLC Command, Kindly check the connection and try again");
                }
            }
            else
            {
                return ("IP Address Selected is Not Reachable, Kindly check the connection and try again");
            }


        }

        public (string, string) StopPLC()
        {
            if ((CheckPostConnectionSuccess() == IPStatus.Success))
            {
                int response = Connect(Tftpaddress, stopplcframe(), true);
                if (response == 170)///Reply sent when PLC is in Run Mode
                {
                    return ("PLC is in Run Mode", "DodgerBlue");
                }
                else if (response == 85)///Reply sent when PLC is in Stop Mode
                {
                    return ("PLC is in Stop mode", "Gold");
                }
                else/// Reply sent when error occures or CRC is incorrect
                {
                    return ("Invalid Responce", "OrangeRed");
                }
            }
            else
            {
                return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
            }

        }

        public (string, string) RunPLC()
        {
            if ((CheckPostConnectionSuccess() == IPStatus.Success))
            {
                int response = Connect(Tftpaddress, runplcframe(), true);
                if (response == 170)///Reply sent when PLC is in Run Mode
                {
                    return ("PLC is in Run Mode", "DodgerBlue");
                }
                else if (response == 85)///Reply sent when PLC is in Stop Mode
                {
                    return ("PLC is in Stop mode", "Gold");
                }
                else/// Reply sent when error occures or CRC is incorrect
                {
                    return ("Invalid Responce", "OrangeRed");
                }
            }
            else
            {
                return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
            }

        }

        //Response for Rtc Setting
        public (string, string) RtcSetting()
        {

            if ((CheckPostConnectionSuccess() == IPStatus.Success))
            {
                return ("Error while configuring RTC", "OrangeRed");
            }
            else
            {
                return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
            }

        }

        public Byte[] loginframe()
        {
            //Calculating CCode and MCode CRC
            long finalCcodeMcode = xm.LoadedProject.ProgramCRC;

            byte[] FinalCcodeMcodeCRC = BitConverter.GetBytes(finalCcodeMcode); //

            //Byte[] FinalCcodeMcodeCRC into single Byte.
            byte FinalCMcodeCRCSinglByte = 0;
            foreach (byte singleByte in FinalCcodeMcodeCRC)
            {
                FinalCMcodeCRCSinglByte ^= singleByte;
            }
            //Calculate Module Type
            byte plcModuleType = 0;
            ////PlcModels xm.PlcModel; 
            //string PlcModule = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();
            //if (PlcModule == "XM-14-DT")
            //{
            //    plcModuleType = 1;
            //}
            //else if (PlcModule == "XM-17-ADT")
            //{
            //    plcModuleType = 2;
            //}
            //else
            //{
            //    plcModuleType = 3;
            //}
            plcModuleType = (byte)xm.ProjectTemplates.Templates.Where(t => t.PlcName == xm.PlcModel).Select(t => t.PlcId).FirstOrDefault();
            //Calculate Program CRC
            byte finalXORCMcodewithModuleType = (byte)(FinalCMcodeCRCSinglByte ^ plcModuleType);
            byte finalloginfrmCRC = (byte)(finalXORCMcodewithModuleType ^ 0x97);
            Byte[] Loginfrm = new Byte[15];
            int i = 0;
            Loginfrm[i] = 0xDB;
            i++;
            foreach (byte value in FinalCcodeMcodeCRC)
            {
                Loginfrm[i] = value;
                i++;                                           //Calculate Ccode and Mcode CRC
            }
            Loginfrm[i] = plcModuleType;
            i++;
            Loginfrm[i] = finalloginfrmCRC;
            i++;
            Loginfrm[i] = 0xDB;
            return Loginfrm;
        }

        public Byte[] MainLogInFrame()
        {

            Byte[] Loginfrm = new Byte[10];
            Loginfrm[0] = 0xFD;
            Loginfrm[1] = 0x01;
            Loginfrm[2] = 0xEA;
            Loginfrm[3] = 0x7D;//CRC
            Loginfrm[4] = 0xFB;
            Loginfrm[5] = 0xFB;
            return Loginfrm;
        }

        public Byte[] ReconnectFrame()
        {

            Byte[] Returnfrm = new Byte[4];
            Returnfrm[0] = 0xFD;
            Returnfrm[1] = 0x03;
            Returnfrm[2] = 0x94;
            Returnfrm[3] = 0xFB;
            return Returnfrm;
        }

        /// <summary>
        /// Generate Static Framde for Run PLC
        /// </summary>
        public Byte[] runplcframe()
        {
            Byte[] RunPLCFrame = new Byte[10];
            RunPLCFrame[0] = 0xFD;
            RunPLCFrame[1] = 0x02;
            RunPLCFrame[2] = 0xEA;
            RunPLCFrame[3] = 0xAA;
            RunPLCFrame[4] = 0xD7;//CRC
            RunPLCFrame[5] = 0xFB;
            return RunPLCFrame;
        }
        /// <summary>
        /// Generate static frame for Stop PLC
        /// </summary>
        public Byte[] stopplcframe()
        {
            Byte[] StopPLCFrame = new Byte[10];
            StopPLCFrame[0] = 0xFD;
            StopPLCFrame[1] = 0x02;
            StopPLCFrame[2] = 0xEA;
            StopPLCFrame[3] = 0x55;
            StopPLCFrame[4] = 0x28;//CRC
            StopPLCFrame[5] = 0xFB;
            return StopPLCFrame;
        }

        /// <summary>
        /// Generate static frame for Stop PLC
        /// </summary>
        public Byte[] ResetWarmPLCFrame()
        {
            Byte[] ResetWarmPLCFrame = new Byte[10];
            ResetWarmPLCFrame[0] = 0xFA;
            ResetWarmPLCFrame[1] = 0x02;
            ResetWarmPLCFrame[2] = 0xEB;
            ResetWarmPLCFrame[3] = 0xD1;
            ResetWarmPLCFrame[4] = 0xAD;//CRC
            ResetWarmPLCFrame[5] = 0xFC;
            return ResetWarmPLCFrame;
        }

        /// <summary>
        /// Generate static frame for Stop PLC
        /// </summary>
        public Byte[] ResetColdPLCFrame()
        {
            Byte[] ResetColdPLCFrame = new Byte[10];
            ResetColdPLCFrame[0] = 0xFA;
            ResetColdPLCFrame[1] = 0x02;
            ResetColdPLCFrame[2] = 0xEB;
            ResetColdPLCFrame[3] = 0xD2;
            ResetColdPLCFrame[4] = 0xAE;//CRC
            ResetColdPLCFrame[5] = 0xFC;
            return ResetColdPLCFrame;
        }

        /// <summary>
        /// Generate static frame for Stop PLC
        /// </summary>
        public Byte[] ResetOriginPLCFrame()
        {
            Byte[] ResetOriginPLCFrame = new Byte[10];
            ResetOriginPLCFrame[0] = 0xFA;
            ResetOriginPLCFrame[1] = 0x02;
            ResetOriginPLCFrame[2] = 0xEB;
            ResetOriginPLCFrame[3] = 0xD3;
            ResetOriginPLCFrame[4] = 0xAF;//CRC
            ResetOriginPLCFrame[5] = 0xFC;
            return ResetOriginPLCFrame;
        }

        /// <summary>
        /// Connect to PLC and Send the packet generated with the option selected by the user and get the reply and send it back
        /// </summary>
        /// <param name="server"></param> IP address of Server i.e PLC which is connected 
        /// <param name="message"></param> Packate generated by selection of User
        /// <param name="withreply"></param>If reply is required ? if reply is required then only stop for the reaply 
        /// and sent the responce to the caller else send 0 by default 
        /// <returns></returns>
        public int Connect(String server, byte[] message, bool withreply)
        {
            try
            {

                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                // Get a client stream for reading and writing.
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
                // Send the message to the connected TcpServer.
                stream.Write(message, 0, message.Length);
                //string responseDatahas = BitConverter.ToString(SendData.ToArray(), 0, SendData.Length);
                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                String Gdata = null;
                // String to store the response ASCII representation.
                //Time out in milliseconds so wait for 2 seconds max  
                stream.ReadTimeout = 2000;
                String responseData = String.Empty;
                // Read the first batch of the TcpServer response bytes.
                // Get a stream object for reading and writing
                NetworkStream Gstream = client.GetStream();
                int i;
                Byte[] bytes = new Byte[250];
                responceindex = -1;
                if (withreply == true)
                {
                    // Loop to receive all the data sent by the client.
                    while ((i = Gstream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var msgindex = Array.FindLastIndex(bytes, value => value != 0);
                        for (int j = 0; j <= msgindex; j++)
                        {
                            // Translate data bytes to a ASCII string.
                            Byte[] gotbyte = new Byte[1];
                            gotbyte[0] = bytes[j];

                            Gdata = BitConverter.ToString(gotbyte, 0, 1);
                            //Check the index of last found value in the array for both array's Main and CRC
                            var mainindex = responceindex;//Array.FindLastIndex(mainstring, value => value != 0);
                            var crcindex = Array.FindLastIndex(CRCPacket, value => value != 0);
                            if ((Gdata.ToString() == "FD" || Gdata.ToString() == "FE") && mainindex == -1)
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
                            else if (Gdata.ToString() == "DB")
                            {
                                stream.Close();
                                client.Close();
                                return 0;
                            }
                            else if ((Gdata.ToString() == "FB" || Gdata.ToString() == "FF") && crcindex != -1)
                            {
                                mainstring[responceindex] = 0xFA;
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
                                else if (j > (Gpktlength + 1))
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
                                    //System.Buffer.BlockCopy(gotbyte, 0, mainstring, mindex + 1, index + 1);
                                    CheckCRC = CheckCRC ^ int.Parse(Gdata, System.Globalization.NumberStyles.HexNumber);
                                }
                            }
                            gotbyte.Initialize();
                            responceindex = responceindex + 1;
                        }
                        if (eof) break;
                        Gstream.Flush();
                        bytes = new Byte[250];
                    }
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
                else
                {
                    stream.Close();
                    client.Close();
                    return 0;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }




        }

        ////<For Plc Synchronization>
        ///
        public Byte[] ConnectSynchronization(String server, byte[] message)
        {
            Byte[] response = new Byte[20];

            try
            {
                TcpClient client = new TcpClient(server, port);
                //To get Data From the PLC Back
                NetworkStream stream = client.GetStream();
                // Send the message to the connected TcpServer.
                stream.Write(message, 0, message.Length);
                stream.ReadTimeout = 3000;
                Byte[] bytes = new Byte[250];
                // Get a stream object for reading and writing
                NetworkStream Gstream = client.GetStream();
                Gstream.Read(bytes, 0, bytes.Length);
                response = bytes;
                stream.Close();
                client.Close();
                return response;

            }
            catch (Exception ex)
            {

                if (message[0] != 212)  MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return response;
            }

        }
        /// <summary>
        /// Connect to PLC and Send the packet generated with the option selected by the user and get the reply and send it back
        /// </summary>nn 
        /// <param name="server"></param> IP address of Server i.e PLC which is connected 
        /// <param name="message"></param> Packate generated by selection of User
        /// <param name="withreply"></param>If reply is required ? if reply is required then only stop for the reaply 
        /// and sent the responce to the caller else send 0 by default 
        /// <returns></returns>
        public int ConnectForReplyWithoutLength(String server, byte[] message, bool withreply)
        {
            try
            {

                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();
                bool eof = false;
                bool sof = false;
                int CheckCRC = 0;
                NetworkStream stream = client.GetStream();
                Byte[] CRC = new Byte[250];
                Byte[] CRCPacket = new Byte[250];
                Byte[] mainstring = new Byte[250];
                Byte[] ActVal = new Byte[4];
                // Send the message to the connected TcpServer.
                stream.Write(message, 0, message.Length);
                //string responseDatahas = BitConverter.ToString(SendData.ToArray(), 0, SendData.Length);
                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                String Gdata = null;
                // String to store the response ASCII representation.
                stream.ReadTimeout = 5000;
                String responseData = String.Empty;
                // Read the first batch of the TcpServer response bytes.
                // Get a stream object for reading and writing
                NetworkStream Gstream = client.GetStream();
                int i;
                Byte[] bytes = new Byte[250];
                responceindex = -1;
                if (withreply == true)
                {
                    // Loop to receive all the data sent by the client.
                    while ((i = Gstream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var msgindex = Array.FindLastIndex(bytes, value => value != 0);
                        for (int j = 0; j <= msgindex; j++)
                        {
                            // Translate data bytes to a ASCII string.
                            Byte[] gotbyte = new Byte[1];
                            gotbyte[0] = bytes[j];

                            Gdata = BitConverter.ToString(gotbyte, 0, 1);
                            //Check the index of last found value in the array for both array's Main and CRC
                            var mainindex = responceindex;//Array.FindLastIndex(mainstring, value => value != 0);
                            var crcindex = Array.FindLastIndex(CRCPacket, value => value != 0);
                            if ((Gdata.ToString() == "FD" || Gdata.ToString() == "FE") && mainindex == -1)
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
                            else if ((Gdata.ToString() == "FB" || Gdata.ToString() == "FF") && crcindex != -1)
                            {
                                mainstring[responceindex] = 0xFA;
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
                                    //Check the value in data and perform the XOR operation on CRC byte as this is CRC value
                                    mainstring[responceindex] = gotbyte[0];
                                    ActVal[0] = gotbyte[0];
                                    CheckCRC = CheckCRC ^ int.Parse(Gdata, System.Globalization.NumberStyles.HexNumber);
                                }
                                else if (!sof)
                                {
                                    //If Total data being sent is less than index of current data then trat it as data byte and add the same in CRC data for CRC calculation and also add in main string
                                    mainstring[responceindex] = gotbyte[0];
                                    System.Buffer.BlockCopy(gotbyte.ToArray(), 0, CRCPacket, 0, 1);
                                }
                            }
                            gotbyte.Initialize();
                            responceindex = responceindex + 1;
                        }
                        if (eof) break;
                        Gstream.Flush();
                        bytes = new Byte[250];
                    }
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
                else
                {
                    stream.Close();
                    client.Close();
                    return 0;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }




        }

        //RTC Frame 
        public int ConnectRTc(String server, byte[] message, bool withreply)
        {
            try
            {
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                // Get a client stream for reading and writing.
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
                // Send the message to the connected TcpServer.
                stream.Write(message, 0, message.Length);
                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                String Gdata = null;
                // String to store the response ASCII representation.
                //stream.ReadTimeout = 5000;
                String responseData = String.Empty;
                // Read the first batch of the TcpServer response bytes.
                // Get a stream object for reading and writing
                NetworkStream Gstream = client.GetStream();
                int i;
                Byte[] bytes = new Byte[250];
                responceindex = -1;
                if (withreply == true)
                {
                    Gstream.ReadTimeout = 300;
                    // Loop to receive all the data sent by the client.
                    while ((i = Gstream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var msgindex = Array.FindLastIndex(bytes, value => value != 0);
                        for (int j = 0; j <= msgindex; j++)
                        {
                            // Translate data bytes to a ASCII string.
                            Byte[] gotbyte = new Byte[1];
                            gotbyte[0] = bytes[j];

                            Gdata = BitConverter.ToString(gotbyte, 0, 1);
                            //Check the index of last found value in the array for both array's Main and CRC
                            var mainindex = responceindex;//Array.FindLastIndex(mainstring, value => value != 0);
                            var crcindex = Array.FindLastIndex(CRCPacket, value => value != 0);
                            if (Gdata.ToString() == "F9" && mainindex == -1)
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
                            else if (Gdata.ToString() == "F8" && crcindex != -1)
                            {
                                mainstring[responceindex] = 0xFA;
                                //Do nothing at end of file just write the value in main string
                                responseData = BitConverter.ToString(mainstring.ToArray(), 0, responceindex + 1);
                                eof = true;
                            }
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
                                else if (j > (Gpktlength + 2))
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
                                    CheckCRC = CheckCRC ^ int.Parse(Gdata, System.Globalization.NumberStyles.HexNumber);
                                }
                            }
                            gotbyte.Initialize();
                            responceindex = responceindex + 1;
                        }
                        if (eof) break;
                        Gstream.Flush();
                        bytes = new Byte[250];
                    }
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
                else
                {
                    stream.Close();
                    client.Close();
                    return 0;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }



        }

        internal (string, string) LogOut()
        {
            return PLCLogin();
        }

        internal object UplodFiles()
        {
            try
            {
                if (GetIPAddress() == "Error")
                    return (" Invalid Ip Address ", "OrangeRed");
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
                if ((reply.Status == IPStatus.Success))
                {
                    xm.UploadTFTPSourceFile(Tftpaddress);
                    return ("File Downloaded Successfully", "LimeGreen");
                }
                else
                {
                    return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, "OrangeRed");
            }
        }

        public (string, string) UpdateFirmwares(string filepath)
        {
            try
            {
                if (xm.LoadedProject != null)
                {
                    if (GetIPAddress() == "Error")
                        return (" Invalid Ip Address ", "OrangeRed");
                }
                else
                {
                    LoadEasyScan();
                    if (GetIPAddress() == "Error") return (" Invalid Ip Address ", "OrangeRed");
                }
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
                if ((reply.Status == IPStatus.Success))
                {
                    DialogResult dialogResult = MessageBox.Show("Start Firmware Update", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        int response = ConnectForReplyWithoutLength(Tftpaddress, UpdateFirmwareFrame(), true);
                        if (response == 237)///Reply sent when download the source
                        {
                            if (GetandRunBrowser())
                            {
                                return ("Firmware Update Initialised Successfully", "LimeGreen");
                            }
                            else
                            {
                                return ("Error, Firefox not installed, Kindly install and try again...", "Red");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error, Please try again", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return ("Try Again", "Black");
                        }
                    }
                    else/// Reply sent when error occures or CRC is incorrect
                    {
                        return ("Firmware Update Cancelled By User ", "DodgerBlue");
                    }

                }
                else
                {
                    return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
                }
            }

            catch (Exception ex)
            {
                return (ex.Message, "OrangeRed");
            }
        }

        public string GetIPAddress()
        {
            try
            {
                if (xm._connectedIPAddress != "" && xm._connectedIPAddress != "0.0.0.0")
                {
                    Checkconnection(2);
                    if (Tftpaddress != "")
                        return Tftpaddress;
                }
                if (xm.LoadedProject != null)
                    Tftpaddress = "Error";
                return Tftpaddress;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Error";
            }
        }

        public void Checkconnection(int retries)
        {
            try
            {
                if (xm._connectedIPAddress != "")
                {
                    int retry = 0;
                Chkagain:
                    bool isdhcp = false;
                    if (xm.LoadedProject != null)
                    {
                        Ethernet ethernet = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
                        isdhcp = ethernet.UseDHCPServer;
                    }
                    Ping x = new Ping();
                    int timeout = 100;
                    if (isdhcp)
                        timeout = 1500;
                    PingReply reply = x.Send(IPAddress.Parse(xm._connectedIPAddress), timeout);
                    if ((reply.Status == IPStatus.Success))
                    {
                        Tftpaddress = xm._connectedIPAddress.ToString();
                    }
                    else
                    {
                        retry++;
                        if (retry < retries)
                            goto Chkagain;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, Please try again " + ex.Message, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LoadEasyScan()
        {
            // Check if there's already an EasyScan form open
            Form existingForm = Application.OpenForms.OfType<EasyScan>().FirstOrDefault();

            if (existingForm != null)
            {
                // If found, bring it to front instead of creating a new one
                existingForm.BringToFront();
                existingForm.Focus();
            }
            else
            {
                // If not found, create a new instance
                EasyScan easyScan = new EasyScan();
                easyScan.Text = "Easy Connection";
                easyScan.ShowDialog();
            }
        }

        /// <summary>
        /// Get the browser and then use url to activate the browser
        /// </summary>
        public bool GetandRunBrowser()
        {
            try
            {
                // Find the Firefox installation path from the Windows Registry
                string firefoxPath =  GetFirefoxPath(); 

                if (!string.IsNullOrEmpty(firefoxPath))
                {
                    // Specify the URL you want to open in Firefox
                    string url = "192.168.15.60";

                    // Start a new process for Firefox with the specified URL
                    Process.Start(firefoxPath, url);
                    return true;
                }
                else
                {
                    Console.WriteLine("Firefox is not installed or the path could not be determined.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;

            }
        }
        /// <summary>
        /// Get path of Firefox where it is installed
        /// </summary>
        /// <returns> Path of installtion </returns>

        private string GetFirefoxPath()
        {
            // Method 1: Check traditional registry locations
            string path = GetFirefoxFromRegistry();
            if (!string.IsNullOrEmpty(path)) return path;

            // Method 2: Check common installation directories
            path = GetFirefoxFromCommonPaths();
            if (!string.IsNullOrEmpty(path)) return path;

            // Method 3: Check Microsoft Store installation
            path = GetFirefoxFromMicrosoftStore();
            if (!string.IsNullOrEmpty(path)) return path;

            // Method 4: Try using Windows App Execution Alias
            path = GetFirefoxFromAppExecutionAlias();
            if (!string.IsNullOrEmpty(path)) return path;

            // Method 5: Last resort - check if firefox.exe is in PATH
            return IsFirefoxInPath() ? "firefox.exe" : null;
        }

        private string GetFirefoxFromRegistry()
        {
            try
            {
                // Check 64-bit registry first
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Mozilla\Mozilla Firefox"))
                {
                    if (key != null)
                    {
                        var version = key.GetSubKeyNames().FirstOrDefault();
                        if (version != null)
                        {
                            using (var versionKey = key.OpenSubKey($@"{version}\Main"))
                            {
                                var path = versionKey?.GetValue("PathToExe")?.ToString();
                                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                                    return path;
                            }
                        }
                    }
                }

                // Check 32-bit registry on 64-bit systems
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Mozilla\Mozilla Firefox"))
                {
                    if (key != null)
                    {
                        var version = key.GetSubKeyNames().FirstOrDefault();
                        if (version != null)
                        {
                            using (var versionKey = key.OpenSubKey($@"{version}\Main"))
                            {
                                var path = versionKey?.GetValue("PathToExe")?.ToString();
                                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                                    return path;
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private string GetFirefoxFromCommonPaths()
        {
            string[] commonPaths = {
        @"C:\Program Files\Mozilla Firefox\firefox.exe",
        @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe",
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                     @"Mozilla Firefox\firefox.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                     @"Mozilla Firefox\firefox.exe")
    };

            return commonPaths.FirstOrDefault(File.Exists);
        }

        private string GetFirefoxFromMicrosoftStore()
        {
            try
            {
                // Check Microsoft Store installation directory
                string storeAppsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                  "Microsoft", "WindowsApps");

                if (Directory.Exists(storeAppsPath))
                {
                    var firefoxDirs = Directory.GetDirectories(storeAppsPath, "*Firefox*", SearchOption.TopDirectoryOnly);
                    foreach (var dir in firefoxDirs)
                    {
                        var firefoxExe = Path.Combine(dir, "firefox.exe");
                        if (File.Exists(firefoxExe))
                            return firefoxExe;
                    }
                }

                // Alternative: Check if Firefox protocol is registered for Microsoft Store
                using (var key = Registry.ClassesRoot.OpenSubKey(@"FirefoxURL-308046B0AF4A39CB\shell\open\command"))
                {
                    var command = key?.GetValue("")?.ToString();
                    if (!string.IsNullOrEmpty(command))
                    {
                        // Extract path from command string
                        var match = System.Text.RegularExpressions.Regex.Match(command, "\"([^\"]+)\"");
                        if (match.Success)
                        {
                            var path = match.Groups[1].Value;
                            if (File.Exists(path))
                                return path;
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private string GetFirefoxFromAppExecutionAlias()
        {
            try
            {
                // Check Windows App Execution Alias
                string aliasPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                              "Microsoft", "WindowsApps", "firefox.exe");

                if (File.Exists(aliasPath))
                    return aliasPath;
            }
            catch { }
            return null;
        }

        private bool IsFirefoxInPath()
        {
            try
            {
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "where",
                    Arguments = "firefox.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                });

                process.WaitForExit();
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }
       

        public Byte[] UpdateFirmwareFrame()
        {
            Byte[] updateFirmware = new Byte[10];
            updateFirmware[0] = 0xFE;//SOF
            updateFirmware[1] = 0xED;//CMD
            updateFirmware[2] = 0x7A;//CRC
            updateFirmware[3] = 0xFF;//EOF
            return updateFirmware;
        }


        public (string, string) DownlodSourceCodeFiles()
        {

            try
            {
                if (GetIPAddress() == "Error")
                    return (" Invalid Ip Address ", "OrangeRed");
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
                if ((reply.Status == IPStatus.Success))
                {
                    int response = ConnectForReplyWithoutLength(Tftpaddress, Dnldsourcecode(), true);
                    if (response == 234)///Reply sent when download the source
                    {
                        DialogResult dialogResult = MessageBox.Show("Start Source Download", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                            xm.DownloadTFTPProjectFile(Tftpaddress);
                        else
                        {
                            return ("Try Again", "Black");
                        }
                        return ("Source code is downloading", "DodgerBlue");
                    }
                    else/// Reply sent when error occures or CRC is incorrect
                    {
                        MessageBox.Show("Error, Please try again", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return ("Try Again", "Black");
                    }
                }
                else
                {
                    return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
                }
            }

            catch (Exception ex)
            {
                return (ex.Message, "OrangeRed");
            }

        }


        public Byte[] Dnldsourcecode()
        {
            Byte[] DnldSourceCode = new Byte[10];
            DnldSourceCode[0] = 0xFE;//SOF
            DnldSourceCode[1] = 0xEA;//CMD
            DnldSourceCode[2] = 0x7D;//CRC
            DnldSourceCode[3] = 0xFF;//EOF
            return DnldSourceCode;
        }

        public Byte[] Upldsourcecode()
        {
            Byte[] Upldsourcecode = new Byte[10];
            Upldsourcecode[0] = 0xFE;//SOF
            Upldsourcecode[1] = 0xEC;//CMD
            Upldsourcecode[2] = 0x7B;//CRC
            Upldsourcecode[3] = 0xFF;//EOF
            return Upldsourcecode;
        }


        internal object UplodSourceCodeFiles()
        {
            try
            {
                string errmsg = XMPS2000.CommonFunctions.GetEasyConnection(xm._connectedIPAddress);
                if (GetIPAddress() == "Error")
                    return (errmsg, "OrangeRed");
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
                if ((reply.Status == IPStatus.Success))
                {
                    int response = ConnectForReplyWithoutLength(Tftpaddress, Upldsourcecode(), true);
                    if (response == 236)///Reply sent when upload the source
                    {
                        DialogResult dialogResult = MessageBox.Show("Start Source Upload", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            xm.UploadTFTPSourceFile(Tftpaddress);
                            return ("Source code is uploading", "DodgerBlue");
                        }
                        else
                        {
                            return ("Try Again", "Black");
                        }
                    }
                    else/// Reply sent when error occures or CRC is incorrect
                    {
                        MessageBox.Show("Error, Please try again", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return ("Try Again", "Black");
                    }
                }
                else
                {
                    return ("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed");
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, "OrangeRed");
            }
        }

        internal byte[] DownloadChkStatus()
        {
            try
            {
                if (GetIPAddress() == "Error")
                {
                    Byte[] resp = new byte[1];
                    return resp;
                }
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(Tftpaddress));
                return ConnectSynchronization(Tftpaddress, DownloadFrame());
            }
            catch (Exception ex)
            {
                Byte[] resp = new byte[1];
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return resp;
            }
        }

        public Byte[] DownloadFrame()
        {
            Byte[] Upldfrm = new Byte[20];
            byte crc = 0;
            Upldfrm[0] = 0xDE;//SOF
            int r = 1;// range of array
            byte[] version = BitConverter.GetBytes(double.Parse(xm.UtilityVersion));
            foreach (byte var in version)
            {
                Upldfrm[r] = var; //Utility version
                crc = (byte)(Convert.ToInt32(crc) ^ Convert.ToInt32(var));
                r++;
            }
            Upldfrm[r] = (byte)xm.PlcModuleType;//Module Code
            crc = (byte)(Convert.ToInt32(crc) ^ xm.PlcModuleType);
            r++;
            Upldfrm[r] = (byte)xm.NoOfFilesDownload;//No.of files to be download
            crc = (byte)(Convert.ToInt32(crc) ^ xm.NoOfFilesDownload);
            r++;
            crc = (byte)(Convert.ToInt32(crc) ^ 151); // CRC XOR Decimal conversion of hex value 97 
            Upldfrm[r] = crc;//CRC
            r++;
            Upldfrm[r] = 0xDF;//EOF
            return Upldfrm;
        }

        internal bool CheckReadyToDownload(string filetype)
        {
            if ((CheckPostConnectionSuccess() == IPStatus.Success))
            {
                byte[] response = ConnectSynchronization(Tftpaddress, BaseDownloadFrame(filetype));
                if (response != null)
                    if (response[0] == 210)
                        return (response[2] == 244 && response[3] == 187 && response[4] == 216) ? true : false;
            }
            return false;
        }

        internal void SendDownloadCompleteFrame()
        {
            if ((CheckPostConnectionSuccess() == IPStatus.Success))
                ConnectRTc(Tftpaddress, DownloadCompleted(), false);
        }

        private byte[] DownloadCompleted()
        {
            Byte[] DnldCFrame = new Byte[10];
            DnldCFrame[0] = 0xD2;//SOF
            DnldCFrame[1] = 0x02;//CMD
            DnldCFrame[2] = 0xF5;//Type
            DnldCFrame[3] = 0xBB;//Status
            DnldCFrame[4] = 0xD9;//217CRC
            DnldCFrame[5] = 0xD1;//EOF
            return DnldCFrame;
        }

        private UInt32 GetInt32CRC(string filepath, out int lineno)
        {
            UInt32 crc = 0; // Start with CRC as 0
            lineno = 0;
            // Read the file and process byte-by-byte
            string updatedFilePath = filepath;
            try
            {
                using (var fileStream = new FileStream(updatedFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (UInt32.TryParse(line, out _))
                        {
                            crc ^= UInt32.Parse(line);
                            lineno++;
                        }
                        else
                        {
                            int plineno = 0;
                            crc ^= ProcessData(Encoding.ASCII.GetBytes(line), out plineno); // Mask to remove negative sign
                            lineno += plineno;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return crc;
        }


        public UInt32 ProcessData(byte[] inputBytes, out int length)
        {
            UInt32 resultInt32 = 0;
            length = 0;
            try
            {
                // Iterate through chunks of 4 bytes
                for (int i = 0; i < inputBytes.Length; i += 4)
                {
                    byte[] chunk = new byte[4];

                    // Copy 4 bytes or pad with zeros if fewer than 4 bytes left
                    for (int j = 0; j < 4; j++)
                    {
                        if (i + j < inputBytes.Length)
                            chunk[j] = inputBytes[i + j];
                        else
                            goto exitloop;
                        //chunk[j] = 0; // Padding with 0
                    }
                    // XOR the chunk into resultInt32
                    resultInt32 ^= BitConverter.ToUInt32(chunk, 0); // Shift to properly align in Int32
                    length++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        exitloop:
            return resultInt32;
        }
        /*
        static byte[] ProcessData(byte[] inputBytes, out int lines)
        {
            byte[] result = new byte[4];
            int lineno = 0;
            int xorLength = Math.Min(inputBytes.Length, 4); // Ensure we XOR only up to 4 bytes

            for (int i = 0; i < xorLength; i++)
            {
                result[i % 4] ^= inputBytes[i];
                lineno++;
            }
            lines = lineno;
            return result;
        }
        */
        public int GetFileLength(string filepath)
        {
            int lineNumber = 0;

            string updatedFilePath = filepath;
            try
            {
                using (var fileStream = new FileStream(updatedFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return lineNumber;
        }

        public Byte[] BaseDownloadFrame(string filetype)
        {
            int lineno = 0;
            Byte[] BaseFrame = new Byte[25];
            int ind = 0;
            byte CRC = 0;
            BaseFrame[ind] = 0xD2;//SOF
            ind++;
            BaseFrame[ind] = 0x06;//CMD
            ind++;
            BaseFrame[ind] = 0xF4;//Type
            ind++;
            //CRC = 0xF4;
            //McodeVersion.txt
            string filepath = XMPS.Instance.LoadedProject.ProjectPath;
            BinaryReader fileStream;
            if (filetype == "MCode")
            {
                BaseFrame[ind] = 0xF1;//Type
                //CRC = (byte)(Convert.ToInt32(CRC) ^ Convert.ToInt32(BaseFrame[ind]));
                ind++;
                /// Get the block length by getting file size int binary and dividing it by 512
                fileStream = new BinaryReader(new FileStream(filepath.Replace(filepath.Split('\\').Last(), "McodeVersion.txt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                XMPS.Instance._noMcodeFrames = (int)Math.Ceiling((double)fileStream.BaseStream.Length / 512);
                if ((double)fileStream.BaseStream.Length % 512 == 0) XMPS.Instance._noMcodeFrames++;
                byte[] mLength = BitConverter.GetBytes((Int16)XMPS.Instance._noMcodeFrames);
                foreach (byte var in mLength)
                {
                    BaseFrame[ind] = var; //Mcode file length in blocks
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }
                /// Get the block CRC of M Code
                byte[] mfileCRC = BitConverter.GetBytes(GetInt32CRC((filepath.Replace(filepath.Split('\\').Last(), "McodeVersion.txt")).ToString(), out lineno));
                //GetInt32CRC((filepath.Replace(filepath.Split('\\').Last(), "McodeVersion.txt")).ToString())
                foreach (byte var in mfileCRC)
                {
                    BaseFrame[ind] = var; //Mcode file CRC
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }
                /// Get the file length 
                //byte[] mfileLength = BitConverter.GetBytes((uint)fileStream.BaseStream.Length);
                byte[] mfileLength = BitConverter.GetBytes(lineno);
                foreach (byte var in mfileLength)
                {
                    BaseFrame[ind] = var; //Mcode file length
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }
            }
            //CcodeVersion.txt
            if (filetype == "CCode")
            {
                BaseFrame[ind] = 0xF2;//Type
                //CRC = (byte)(Convert.ToInt32(CRC) ^ Convert.ToInt32(BaseFrame[ind]));
                ind++;
                fileStream = new BinaryReader(new FileStream(filepath.Replace(filepath.Split('\\').Last(), "CcodeVersion.txt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                XMPS.Instance._noCcodeFrames = (int)Math.Ceiling((double)fileStream.BaseStream.Length / 512);
                if ((double)fileStream.BaseStream.Length % 512 == 0) XMPS.Instance._noCcodeFrames++;
                byte[] cLength = BitConverter.GetBytes((Int16)XMPS.Instance._noCcodeFrames);
                foreach (byte var in cLength)
                {
                    BaseFrame[ind] = var; //Ccode file length in blocks
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }

                /// Get the CRC of C Code
                byte[] cfileCRC = BitConverter.GetBytes(GetInt32CRC((filepath.Replace(filepath.Split('\\').Last(), "CcodeVersion.txt")).ToString(), out lineno));
                //GetInt32CRC((filepath.Replace(filepath.Split('\\').Last(), "McodeVersion.txt")).ToString())
                foreach (byte var in cfileCRC)
                {
                    BaseFrame[ind] = var; //Ccode file CRC
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }

                //byte[] cfileLength = BitConverter.GetBytes((uint)fileStream.BaseStream.Length);
                byte[] cfileLength = BitConverter.GetBytes(lineno);
                foreach (byte var in cfileLength)
                {
                    BaseFrame[ind] = var; //Ccode file length
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }
            }
            //MQTT_CNF.txt
            if (filetype == "Config")
            {
                BaseFrame[ind] = 0xF3;//Type
                //CRC = (byte)(Convert.ToInt32(CRC) ^ Convert.ToInt32(BaseFrame[ind]));
                ind++;
                fileStream = new BinaryReader(new FileStream(filepath.Replace(filepath.Split('\\').Last(), "MQTT_CNF.txt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                XMPS.Instance._noCnfFrames = (int)Math.Ceiling((double)fileStream.BaseStream.Length / 512);
                if ((double)fileStream.BaseStream.Length % 512 == 0) XMPS.Instance._noCnfFrames++;

                byte[] cnfLength = BitConverter.GetBytes((Int16)XMPS.Instance._noCnfFrames);
                foreach (byte var in cnfLength)
                {
                    BaseFrame[ind] = var; //Config file length in blocks
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }
                // Initialize the byte array to hold file data
                byte[] fileBytes = new byte[fileStream.BaseStream.Length];

                // Read data from the file into the byte array
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                /// Get the CRC of C Code
                byte[] cnffileCRC = BitConverter.GetBytes((Int32)ProcessData(fileBytes, out lineno));
                foreach (byte var in cnffileCRC)
                {
                    BaseFrame[ind] = var; //Ccode file CRC
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }

                byte[] cnffileLength = BitConverter.GetBytes(lineno);
                foreach (byte var in cnffileLength)
                {
                    BaseFrame[ind] = var; //Ccode file length
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }
            }
            //BCode.txt
            if (filetype == "BCode")
            {
                BaseFrame[ind] = 0xF6;//Type
                //CRC = (byte)(Convert.ToInt32(CRC) ^ Convert.ToInt32(BaseFrame[ind]));
                ind++;
                fileStream = new BinaryReader(new FileStream(filepath.Replace(filepath.Split('\\').Last(), "Bcode.txt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                XMPS.Instance._noBcodeFrames = (int)Math.Ceiling((double)fileStream.BaseStream.Length / 512);
                if ((double)fileStream.BaseStream.Length % 512 == 0) XMPS.Instance._noBcodeFrames++;
                byte[] bCodeLength = BitConverter.GetBytes((Int16)XMPS.Instance._noBcodeFrames);
                foreach (byte var in bCodeLength)
                {
                    BaseFrame[ind] = var; //Bcode file length in blocks
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }
                // Initialize the byte array to hold file data
                byte[] fileBytes = new byte[fileStream.BaseStream.Length];

                // Read data from the file into the byte array
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                /// Get the CRC of C Code
                byte[] bcdfileCRC = BitConverter.GetBytes((UInt32)ProcessData(fileBytes, out lineno));
                foreach (byte var in bcdfileCRC)
                {
                    BaseFrame[ind] = var; //Ccode file CRC
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }

                byte[] cnffileLength = BitConverter.GetBytes(lineno);
                foreach (byte var in cnffileLength)
                {
                    BaseFrame[ind] = var; //Ccode file length
                    CRC = (byte)(Convert.ToUInt32(CRC) ^ Convert.ToUInt32(var));
                    ind++;
                }

                //BaseFrame[ind] = GetByteWiseCRC(fileStream); //Ccode file length
                //CRC = (byte)(Convert.ToInt32(CRC) ^ Convert.ToInt32(BaseFrame[ind]));
                //ind++;

                //byte[] bcfileLength = BitConverter.GetBytes((uint)fileStream.BaseStream.Length);
                //foreach (byte var in bcfileLength)
                //{
                //    BaseFrame[ind] = var; //Bcode file length
                //    CRC = (byte)(Convert.ToInt32(CRC) ^ Convert.ToInt32(var));
                //    ind++;
                //}

                //byte[] cnffileLength = BitConverter.GetBytes(lineno);
                //foreach (byte var in cnffileLength)
                //{
                //    BaseFrame[ind] = var; //Ccode file length
                //    CRC = (byte)(Convert.ToInt32(CRC) ^ Convert.ToInt32(var));
                //    ind++;
                //}
            }
            CRC = (byte)(Convert.ToUInt32(CRC) ^ 151);// CRC XOR Decimal conversion of hex value 97 
            BaseFrame[ind] = CRC; //CRC
            ind++;
            BaseFrame[ind] = 0xD1;//EOF
            return BaseFrame;
        }




        internal bool IsMachineReady(string frameType)
        {
            try
            {
                int retries = 0;
            Retry:
                int status = 0;
                if ((CheckPostConnectionSuccess() == IPStatus.Success))
                {

                    byte[] response = ConnectSynchronization(Tftpaddress, frameType == "MCode" ? McodeFrame() : frameType == "CCode" ? CcodeFrame() : frameType == "Config" ? ConfigFrame() : BCodeFrame());
                    if (response != null)
                    {
                        if (response[0] == 210)
                        {
                            switch (frameType)
                            {
                                case "MCode":
                                    status = (response[2] == 241 && response[4] == 221) ? response[3] : 0;
                                    break;
                                case "CCode":
                                    status = (response[2] == 242 && response[4] == 222) ? response[3] : 0;
                                    break;
                                case "Config":
                                    status = (response[2] == 243 && response[4] == 223) ? response[3] : 0;
                                    break;
                                case "BCode":
                                    status = (response[2] == 246 && response[4] == 218) ? response[3] : 0;
                                    break;
                            }
                        }
                        if (status == 187)///Reply sent when upload the source
                            return true;
                        else
                        {
                            if (retries < 3)
                            {
                                retries++;
                                Thread.Sleep(CalculateTimeOut(frameType));
                                goto Retry;
                            }
                            return false;

                        }
                    }
                    else if (retries < 3)
                    {
                        retries++;
                        Thread.Sleep(CalculateTimeOut(frameType));
                        goto Retry;
                    }
                    return false;
                }
                else
                {
                    return false; //("IP Address Selected is Not Reachable, Kindly check the connection and try again", "OrangeRed")
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private int CalculateTimeOut(string filetype)
        {
            int lineCount = 0;

            using (StreamReader reader = new StreamReader(xm.LoadedProject.ProjectPath.Replace(xm.LoadedProject.ProjectPath.Split('\\').Last(), filetype == "Config" ? "MQTT_CNF.txt" : filetype == "BCode" ? filetype.Trim() + ".txt" : filetype.Trim() + "Version.txt")))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }
            return lineCount;
        }

        public Byte[] McodeFrame()
        {
            Byte[] McodeFrame = new Byte[10];
            McodeFrame[0] = 0xD2;//SOF
            McodeFrame[1] = 0x02;//CMD
            McodeFrame[2] = 0xF1;//Type
            McodeFrame[3] = 0xBB;//Status
            McodeFrame[4] = 0xDD;//221CRC
            McodeFrame[5] = 0xD1;//EOF
            return McodeFrame;
        }

        public Byte[] CcodeFrame()
        {
            Byte[] McodeFrame = new Byte[10];
            McodeFrame[0] = 0xD2;//SOF
            McodeFrame[1] = 0x02;//CMD
            McodeFrame[2] = 0xF2;//Type
            McodeFrame[3] = 0xBB;//Status
            McodeFrame[4] = 0xDE;//222CRC
            McodeFrame[5] = 0xD1;//EOF
            return McodeFrame;
        }

        public Byte[] ConfigFrame()
        {
            Byte[] CnfFrame = new Byte[10];
            CnfFrame[0] = 0xD2;//SOF
            CnfFrame[1] = 0x02;//CMD
            CnfFrame[2] = 0xF3;//Type
            CnfFrame[3] = 0xBB;//Status
            CnfFrame[4] = 0xDF;//223CRC
            CnfFrame[5] = 0xD1;//EOF
            return CnfFrame;
        }

        public Byte[] BCodeFrame()
        {
            Byte[] CnfFrame = new Byte[10];
            CnfFrame[0] = 0xD2;//SOF
            CnfFrame[1] = 0x02;//CMD
            CnfFrame[2] = 0xF6;//Type
            CnfFrame[3] = 0xBB;//Status
            CnfFrame[4] = 0xE2;//226CRC
            CnfFrame[5] = 0xD1;//EOF
            return CnfFrame;
        }

        public async void PingAllAddresses()
        {
            List<Task> pingTasks = new List<Task>();
            foreach (IPAddress ipAddress in GetLocalIPAddresses())
            {
                string[] ipParts = ipAddress.ToString().Split('.');
                if (ipParts.Length == 4)
                {
                    string baseIP = $"{ipParts[0]}.{ipParts[1]}.{ipParts[2]}.";

                    for (int i = 1; i <= 254; i++)
                    {
                        string testIP = baseIP + i;

                        // Add the ping task to the list for parallel execution
                        pingTasks.Add(PingAndLogAsync(testIP));
                    }
                }
            }
            // Wait for all pings to complete
            await Task.WhenAll(pingTasks);
        }

        static async Task PingAndLogAsync(string ipAddress)
        {
            Ping ping = new Ping();
            try
            {
                PingReply reply = await ping.SendPingAsync(ipAddress, 1000); // Timeout 1000ms
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error pinging {ipAddress}: {ex.Message}");
            }
        }

        static List<IPAddress> GetLocalIPAddresses()
        {
            List<IPAddress> ipAddresses = new List<IPAddress>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddresses.Add(ip.Address);
                        }
                    }
                }
            }
            return ipAddresses;
        }

        internal string GetConnectionDetails(string conIPAddress)
        {
            try
            {
                string result = "Error";
                byte[] response = ConnectSynchronization(conIPAddress, ConnectionEssentialFrame());
                if (response != null)
                {
                    if (response[0] == 212 && response[1] == 241)
                    {
                        string ipaddress = response[2].ToString() + "." + response[3].ToString() + "." + response[4].ToString() + "." + response[5].ToString();
                        string subnetmask = response[6].ToString() + "." + response[7].ToString() + "." + response[8].ToString() + "." + response[9].ToString();
                        string gateway = response[10].ToString() + "." + response[11].ToString() + "." + response[12].ToString() + "." + response[13].ToString();
                        string macID = response[14].ToString("X") + "-" + response[15].ToString("X") + "-" + response[16].ToString("X") + "-" + response[17].ToString("X") + "-" + response[18].ToString("X") + "-" + response[19].ToString("X") ;
                        result = ipaddress + "\n" + subnetmask + "\n" + gateway + "\n" + macID;
                    }
                }
                return result;
            }
            catch { return "Error"; }
        }

        public Byte[] ConnectionEssentialFrame()
        {
            Byte[] CneFrame = new Byte[5];
            CneFrame[0] = 0xD4;//SOF
            CneFrame[1] = 0xF1;//CMD
            CneFrame[2] = 0xB2;//CRC
            CneFrame[3] = 0xD3;//EOF
            return CneFrame;
        }
    }
}