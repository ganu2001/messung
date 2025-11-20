using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.App
{
    public class CcodeGeneration
    {
        private static readonly XMPS xm = XMPS.Instance;
        //Start of Frame              
        public static int _startframe = 36; //36  $
        public static string _convertlogicaladdess;

        public static int _universal_io_count;
        public static bool _skipIpAddress = false;


        public static List<string> AIModes = new List<string>();
        public static List<long> _ccodelistForCRC = new List<long>();
        public static List<long> _ccodelist = new List<long>();
        public static int _endframe = 35;  //35   #
        public static int CRC = 8526;

        public int _modbusType;
        public string PlcModel = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();


        public void ConfigComSettings()
        {
            _ccodelist.Add(_startframe);

            int _comSettings = 8513;
            _ccodelist.Add(_comSettings);

            var comsetting = (COMDevice)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "COMDevice").FirstOrDefault();


            int baudrate = ((int)Enum.Parse(typeof(COMBaudRate), "_" + comsetting.BaudRate.ToString())) - 1;
            _ccodelist.Add(baudrate);

            int parity = ((int)Enum.Parse(typeof(COMParity), comsetting.Parity.ToString())) - 1;
            _ccodelist.Add(parity);

            int communicationTimeout = comsetting.CommunicationTimeout;
            if (communicationTimeout == 0)
                communicationTimeout = 500;
            _ccodelist.Add(communicationTimeout);

            int no_of_retries = comsetting.NumberOfRetries;
            _ccodelist.Add(no_of_retries);

            int data_length = comsetting.DataLength;
            _ccodelist.Add(data_length);

            int stop_bit = comsetting.StopBit;
            _ccodelist.Add(stop_bit);

            int send_delay = comsetting.SendDelay;
            _ccodelist.Add(send_delay);

            float min_interface = (float)comsetting.MinimumInterface;
            ConvertFloatValuesToInt(min_interface);
            ConfigEthernetSettings();
        }
        public void ConfigEthernetSettings()
        {
            int _ethSettings = 8514;
            _ccodelist.Add(_ethSettings);

            var ethernetset = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();


            int use_dhcp = (int)Convert.ToByte(ethernetset.UseDHCPServer);
            _ccodelist.Add(use_dhcp);

            string IP_Address = ApplyIPpadding(ethernetset.EthernetIPAddress.ToString());
            _skipIpAddress = false;
            //Spliting IP Address into subparts
            string[] IP_Output = IP_Address.Split('.');
            string IP_address_1st = IP_Output[0];
            ConvertStringToInt(IP_address_1st);

            string IP_address_2nd = IP_Output[1];
            ConvertStringToInt(IP_address_2nd);

            string IP_address_3rd = IP_Output[2];
            ConvertStringToInt(IP_address_3rd);

            string IP_address_4th = IP_Output[3];
            ConvertStringToInt(IP_address_4th);


            string Subnet_Output1 = ApplyIPpadding(ethernetset.EthernetSubNet.ToString());

            //Spliting Subnet into subparts
            string[] Subnet_Output = Subnet_Output1.Split('.');

            string subnet_1st = Subnet_Output[0];
            ConvertStringToInt(subnet_1st);

            string subnet_2nd = Subnet_Output[1];
            ConvertStringToInt(subnet_2nd);

            string subnet_3rd = Subnet_Output[2];
            ConvertStringToInt(subnet_3rd);

            string subnet_4th = Subnet_Output[3];
            ConvertStringToInt(subnet_4th);

            string Gateway1 = ApplyIPpadding(ethernetset.EthernetGetWay == null ? "0.0.0.0" : ethernetset.EthernetGetWay.ToString());
            //Spliting Gateway into subparts
            string[] Gateway_Output = Gateway1.Split('.');

            string gateway_1st = Gateway_Output[0];
            ConvertStringToInt(gateway_1st);

            string gateway_2nd = Gateway_Output[1];
            ConvertStringToInt(gateway_2nd);

            string gateway_3rd = Gateway_Output[2];
            ConvertStringToInt(gateway_3rd);

            string gateway_4th = Gateway_Output[3];
            ConvertStringToInt(gateway_4th);

            int port_no = ethernetset.Port;
            _ccodelist.Add(port_no);

            ConfigNEthernetSettings();

        }


        public void ConfigNEthernetSettings()
        {
            int _nethSettings = 8515;
            _ccodelist.Add(_nethSettings);

            var ethernetset = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();


            bool use_dhcp = ethernetset.UseDHCPServer;
            if (use_dhcp)
                _ccodelist.Add(1);
            else
                _ccodelist.Add(0);


            string IP_out = ApplyIPpadding(ethernetset.ChangeIPAddress.ToString());
            //Spliting IP Address into subparts
            string[] IP_Output = IP_out.Split('.');

            string IP_address_1st = IP_Output[0];
            ConvertStringToInt(IP_address_1st);
            _skipIpAddress = (IP_Output[0] == "000" && IP_Output[1] == "000" && IP_Output[2] == "000" && IP_Output[3] == "000") ? _skipIpAddress : true;
            string IP_address_2nd = IP_Output[1];
            ConvertStringToInt(IP_address_2nd);

            string IP_address_3rd = IP_Output[2];
            ConvertStringToInt(IP_address_3rd);

            string IP_address_4th = IP_Output[3];
            ConvertStringToInt(IP_address_4th);

            string Subnet_out = ApplyIPpadding(ethernetset.ChangeSubNet.ToString());
            //Spliting Subnet into subparts
            string[] Subnet_Output = Subnet_out.Split('.');
            _skipIpAddress = (Subnet_Output[0] == "000" && Subnet_Output[1] == "000" && Subnet_Output[2] == "000" && Subnet_Output[3] == "000") ? _skipIpAddress : true;

            string subnet_1st = Subnet_Output[0];
            ConvertStringToInt(subnet_1st);

            string subnet_2nd = Subnet_Output[1];
            ConvertStringToInt(subnet_2nd);

            string subnet_3rd = Subnet_Output[2];
            ConvertStringToInt(subnet_3rd);

            string subnet_4th = Subnet_Output[3];
            ConvertStringToInt(subnet_4th);

            string Gateway = ApplyIPpadding(ethernetset.ChangeGetWay.ToString());
            //Spliting Gateway into subparts
            string[] Gateway_Output = Gateway.Split('.');
            _skipIpAddress = (Gateway_Output[0] == "000" && Gateway_Output[1] == "000" && Gateway_Output[2] == "000" && Gateway_Output[3] == "000") ? _skipIpAddress : true;

            string gateway_1st = Gateway_Output[0];
            ConvertStringToInt(gateway_1st);

            string gateway_2nd = Gateway_Output[1];
            ConvertStringToInt(gateway_2nd);

            string gateway_3rd = Gateway_Output[2];
            ConvertStringToInt(gateway_3rd);

            string gateway_4th = Gateway_Output[3];
            ConvertStringToInt(gateway_4th);

            int port_no = 0;
            _ccodelist.Add(port_no);

            //Add PLC Model
            AddPLCModel();

        }

        public void AddPLCModel()
        {
            //Adding PLC Model
            int plcModel = 8516;
            _ccodelist.Add(plcModel);
            //If XM-14 is Present => 1 And If XM-17 take 2
            string Ispresent = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault();
            Ispresent = xm.ProjectTemplates.Templates.Where(t => t.PlcName == Ispresent).Select(t => t.PlcId).FirstOrDefault().ToString();
            ConvertStringToInt(Ispresent);
            RemoteIOConfig();
        }

        public void RemoteIOConfig()
        {
            int remote_IO_Config = 8517;
            _ccodelist.Add(remote_IO_Config);

            int no_of_Remote_DI = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.DigitalInput && d.IoList == Types.IOListType.RemoteIO).Count();
            _ccodelist.Add(no_of_Remote_DI);

            int no_of_Remote_DO = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.DigitalOutput && d.IoList == Types.IOListType.RemoteIO).Count();
            _ccodelist.Add(no_of_Remote_DO);

            int no_of_AI = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.AnalogInput && d.IoList == Types.IOListType.RemoteIO).Count();
            _ccodelist.Add(no_of_AI);

            int no_of_AO = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.AnalogOutput && d.IoList == Types.IOListType.RemoteIO).Count();
            _ccodelist.Add(no_of_AO);

            int no_of_UI = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.UniversalInput && d.IoList == Types.IOListType.RemoteIO).Count();
            _ccodelist.Add(no_of_UI);

            int no_of_UO = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.UniversalOutput && d.IoList == Types.IOListType.RemoteIO).Count();
            _ccodelist.Add(no_of_UO);


            PlcOnBoardConfig();

        }

        public void PlcOnBoardConfig()
        {
            int plcOnBoard = 8518;
            _ccodelist.Add(plcOnBoard);

            int no_of_OnboardDI = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.DigitalInput && d.IoList == Types.IOListType.OnBoardIO).Count();
            _ccodelist.Add(no_of_OnboardDI);

            int no_of_OnboardDO = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.DigitalOutput && d.IoList == Types.IOListType.OnBoardIO).Count();
            _ccodelist.Add(no_of_OnboardDO);

            int no_of_OnboardAI = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.AnalogInput && d.IoList == Types.IOListType.OnBoardIO && !d.LogicalAddress.Contains(".")).Count();
            _ccodelist.Add(no_of_OnboardAI);

            int no_of_OnboardAO = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.AnalogOutput && d.IoList == Types.IOListType.OnBoardIO && !d.LogicalAddress.Contains(".")).Count();
            _ccodelist.Add(no_of_OnboardAO);

            string AI1;
            string AI2;
            string AO1;

            var OBIOS = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO && d.Label.StartsWith("A") && !d.LogicalAddress.Contains("."))
                .OrderBy(d => d.Model).ThenBy(d => d.LogicalAddress).ToList();     //Contains DigitalInput / O/P 


            if (xm.PlcModel.Contains("17"))
            {
                foreach (var item in OBIOS)
                {
                    string label = item.Label;
                    string mode = item.Mode;
                    if (label == "AI0" || label == "AI1" || label == "AO0")
                    {
                        if (mode != "" && mode != null)
                            AIModes.Add(Mode.List.Where(d => d.Text == mode).Select(d => d.ID).FirstOrDefault().ToString());
                        else
                            AIModes.Add("0");
                    }

                }
                AI1 = AIModes[0].ToString();
                ConvertStringToInt(AI1);

                AI2 = AIModes[1].ToString();
                ConvertStringToInt(AI2);

                AO1 = AIModes[2].ToString();
                ConvertStringToInt(AO1);

                AIModes.Clear();

            }
            else
            {
                AI1 = "0";
                ConvertStringToInt(AI1);
                AI2 = "0";
                ConvertStringToInt(AI1);
                AO1 = "0";
                ConvertStringToInt(AO1);

            }
            ////Saving OnBoard Calibration Address for XM-17-ADT model
            if (xm.PlcModel == "XM-17-ADT" || xm.PlcModel == "XBLD-17" || xm.PlcModel == "XBLD-17E" || xm.PlcModel == "XM-17-ADT-E")
            {
                SaveCalibrationTagsInfo(xm.PlcModel);
            }
            //adding Enable Filter 
            AddingEnableFilterDetails();
            //adding HSIO Modes of base io 
            if (PlcModel == "XM-14-DT-HIO" || PlcModel == "XM-14-DT-HIO-E")
            {
                AddHSIOMode();
            }
            //Create a New Function for Calling getting data for the HSIO Function Blocks
            if (PlcModel == "XM-14-DT-HIO" || PlcModel == "XM-14-DT-HIO-E")
            {
                GettingDataHSIO();
            }
            ModbusType();
        }

        private void AddingEnableFilterDetails()
        {
            var onBoardDI = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.DigitalInput && d.IoList == Types.IOListType.OnBoardIO).ToList().OrderBy(t => t.Label.Substring(2, 1));
            foreach (XMIOConfig tag in onBoardDI)
            {
                if (tag.IsEnableInputFilter)
                {
                    _ccodelist.Add(Convert.ToInt32(tag.InpuFilterValue));
                }
                else
                {
                    _ccodelist.Add(0);
                }
            }
        }

        private void GettingDataHSIO()
        {
            _ccodelist.Add(8519);
            List<HSIO> hSIOs = xm.LoadedProject.HsioBlock.Where(T => T.HSIOFunctionBlockName.Contains("HSI")).ToList();
            for (int i = 0; i < hSIOs.Count(); i++)
            {
                long HSIOBlocksNum = i + 1;
                _ccodelist.Add(Convert.ToInt64($"{HSIOBlocksNum + "F"}", 16));
                //Adding Hard-coded values for operant type for HSIO functionBlocks
                List<HSIOFunctionBlock> hsioBlocks = XMPS.Instance.LoadedProject.HsioBlock.Where(hsio => hsio.HSIOFunctionBlockName == hSIOs[i].HSIOFunctionBlockName).SelectMany(hsio => hsio.HSIOBlocks).ToList();
                //For getting Operand Types of Inputs and Outputs of the Hsio Fuction Blocks
                int InputOperandCount = 1;
                string InputOperandsFirstNine = string.Empty;
                string OutputOperandsNextNine = string.Empty;
                foreach (HSIOFunctionBlock hSIOFunctionBlock in hsioBlocks)
                {
                    string input = hSIOFunctionBlock.Value;
                    bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");

                    if (InputOperandCount <= 9)
                    {
                        if (!hSIOFunctionBlock.Value.StartsWith("~") && hSIOFunctionBlock.Value != "???" && containsAlphabetic)
                        {
                            //for Normal Operand
                            InputOperandsFirstNine += "1";
                        }
                        else if (hSIOFunctionBlock.Value.StartsWith("~"))
                        {
                            //For Neagation Operand
                            InputOperandsFirstNine += "2";
                        }
                        else if (hSIOFunctionBlock.Value == "???")
                        {
                            InputOperandsFirstNine += "4";
                        }
                        else
                        {
                            //For Numeric Operand
                            InputOperandsFirstNine += "3";
                        }
                        InputOperandCount = InputOperandCount + 1;
                    }
                    else if (InputOperandCount > 9)
                    {
                        if (!hSIOFunctionBlock.Value.StartsWith("~") && hSIOFunctionBlock.Value != "???" && containsAlphabetic)
                        {
                            //For Normal Operand
                            OutputOperandsNextNine += "1";
                        }
                        else if (hSIOFunctionBlock.Value.StartsWith("~"))
                        {
                            //For Negation Operand
                            OutputOperandsNextNine += "2";
                        }
                        else if (hSIOFunctionBlock.Value == "???")
                        {
                            OutputOperandsNextNine += "4";
                        }
                        else
                        {
                            //For Numeric Operand
                            OutputOperandsNextNine += "3";
                        }
                        InputOperandCount = InputOperandCount + 1;
                    }

                }
                _ccodelist.Add(long.Parse(InputOperandsFirstNine));
                _ccodelist.Add(long.Parse(OutputOperandsNextNine));

                foreach (HSIOFunctionBlock hSIOFunctionBlock in hsioBlocks)
                {
                    string input = hSIOFunctionBlock.Value;
                    bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");
                    if (hSIOFunctionBlock.Value != "???" && containsAlphabetic)
                    {
                        if (hSIOFunctionBlock.Value.Contains(":"))
                        {
                            LogicalAddToDec(hSIOFunctionBlock.Value.Replace("~", ""));
                        }
                        else
                        {
                            string logicalAdd = xm.LoadedProject.Tags.Where(T => T.Tag == hSIOFunctionBlock.Value.Replace("~", "")).Select(T => T.LogicalAddress).FirstOrDefault().ToString();
                            LogicalAddToDec(logicalAdd);
                        }
                    }
                    else if (hSIOFunctionBlock.Value == "???")
                    {
                        //if Address are not present then added "0" in ccode file.
                        _ccodelist.Add(0);
                    }
                    else
                    {
                        //Adding Actual Numeric Value into the CCode.
                        if (long.Parse(hSIOFunctionBlock.Value) < 0)
                        {
                            long bit_value_32 = 4294967296;
                            int absIntValue = Math.Abs(int.Parse(hSIOFunctionBlock.Value)); // Take the absolute value
                            long Real_result = bit_value_32 - absIntValue;
                            _ccodelist.Add(Real_result);
                        }
                        else
                            _ccodelist.Add(long.Parse(hSIOFunctionBlock.Value));
                    }
                }
            }

            List<HSIO> hSIOutputs = xm.LoadedProject.HsioBlock.Where(T => T.HSIOFunctionBlockName.Contains("HSO")).ToList();
            for (int i = 0; i < hSIOutputs.Count(); i++)
            {
                long HSIOBlocksNum = i + 5;
                _ccodelist.Add(Convert.ToInt64($"{HSIOBlocksNum + "F"}", 16));
                List<HSIOFunctionBlock> hsioBlocks = XMPS.Instance.LoadedProject.HsioBlock.Where(hsio => hsio.HSIOFunctionBlockName == hSIOutputs[i].HSIOFunctionBlockName).SelectMany(hsio => hsio.HSIOBlocks).ToList();

                //for the Getting Operands Type of Inputs and OutPuts of HSIO
                int InputOperandCount = 1;
                string InputOperandsFirstNine = string.Empty;
                string OutputOperandsNextNine = string.Empty;
                foreach (HSIOFunctionBlock hSIOFunctionBlock in hsioBlocks)
                {
                    string input = hSIOFunctionBlock.Value;
                    bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");

                    if (InputOperandCount <= 9)
                    {
                        if (!hSIOFunctionBlock.Value.StartsWith("~") && hSIOFunctionBlock.Value != "???" && containsAlphabetic)
                        {
                            //Normal Operand
                            InputOperandsFirstNine += "1";
                        }
                        else if (hSIOFunctionBlock.Value.StartsWith("~"))
                        {
                            //Negation Operand
                            InputOperandsFirstNine += "2";
                        }
                        else if (hSIOFunctionBlock.Value == "???")
                        {
                            InputOperandsFirstNine += "4";
                        }
                        else
                        {
                            //Numeric Operand
                            InputOperandsFirstNine += "3";
                        }
                        InputOperandCount = InputOperandCount + 1;
                    }
                    else if (InputOperandCount > 9)
                    {
                        if (!hSIOFunctionBlock.Value.StartsWith("~") && hSIOFunctionBlock.Value != "???" && containsAlphabetic)
                        {
                            //Normal Operand
                            OutputOperandsNextNine += "1";
                        }
                        else if (hSIOFunctionBlock.Value.StartsWith("~"))
                        {
                            //Negation Operand
                            OutputOperandsNextNine += "2";
                        }
                        else if (hSIOFunctionBlock.Value == "???")
                        {
                            OutputOperandsNextNine += "4";
                        }
                        else
                        {
                            //Numeric Operand
                            OutputOperandsNextNine += "3";
                        }
                        InputOperandCount = InputOperandCount + 1;
                    }

                }
                //Adding All the Operands Type into the CCode File for the HSIOs
                _ccodelist.Add(long.Parse(InputOperandsFirstNine));
                _ccodelist.Add(long.Parse(OutputOperandsNextNine));

                foreach (HSIOFunctionBlock hSIOFunctionBlock in hsioBlocks)
                {
                    string input = hSIOFunctionBlock.Value;
                    bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");
                    if (hSIOFunctionBlock.Value != "???" && containsAlphabetic)
                    {
                        if (hSIOFunctionBlock.Value.Contains(":"))
                        {
                            LogicalAddToDec(hSIOFunctionBlock.Value.Replace("~", ""));
                        }
                        else
                        {
                            if (xm.LoadedProject.Tags.Where(T => T.Tag == hSIOFunctionBlock.Value.Replace("~", "")).Count() == 0)
                            {
                                xm.errorInCompiler = true;
                                throw new Exception(String.Join(Environment.NewLine, hSIOFunctionBlock.Value.ToString() + " is not defined"));
                            }

                            string logicalAdd = xm.LoadedProject.Tags.Where(T => T.Tag == hSIOFunctionBlock.Value.Replace("~", "")).Select(T => T.LogicalAddress).FirstOrDefault().ToString();
                            LogicalAddToDec(logicalAdd);
                        }
                    }
                    else if (hSIOFunctionBlock.Value == "???")
                    {
                        //if Address are not present then added "0" in ccode file.
                        _ccodelist.Add(0);
                    }
                    else
                    {
                        //Adding Actual Numeric Value into the CCode.
                        if (long.Parse(hSIOFunctionBlock.Value) < 0)
                        {
                            long bit_value_32 = 4294967296;
                            int absIntValue = Math.Abs(int.Parse(hSIOFunctionBlock.Value));
                            long Real_result = bit_value_32 - absIntValue;
                            _ccodelist.Add(Real_result);
                        }
                        else
                            _ccodelist.Add(long.Parse(hSIOFunctionBlock.Value));
                    }
                }
            }
        }

        private void AddHSIOMode()
        {
            var onBoardIO = xm.LoadedProject.Tags.Where(T => T.IoList == Core.Types.IOListType.OnBoardIO).OrderBy(T => T.LogicalAddress).ToList();
            foreach (XMIOConfig xMIO in onBoardIO)
            {
                string mode = xMIO.Mode;
                string type = xMIO.Type.ToString();
                if (!string.IsNullOrWhiteSpace(mode))
                {
                    int modeNumber = GetModeNo(mode, type);
                    _ccodelist.Add(modeNumber);
                }
            }
        }

        private int GetModeNo(string mode, string type)
        {
            if (type == "DigitalInput")
            {
                if (mode == "Up")
                    return 1;
                else if (mode == "Down")
                    return 2;
                else if (mode.Contains("Up/Down"))
                    return 3;
                else if (mode.Contains("Quadrature 2x Encoder"))
                    return 4;
                else if (mode.Contains("Quadrature 4x Encoder"))
                    return 5;
                else if (mode == "Interrupt")
                    return 6;
                else
                    return 7;
            }
            else
            {
                if (mode == "PTO")
                    return 1;
                else if (mode == "Interrupt Output")
                    return 2;
                else
                    return 3;
            }

        }

        int _rtuCount, _tcpCount, _serverCount;
        public void ModbusType()
        {
            bool isSlaveMode = xm.LoadedProject.RS485Mode == "Slave";
            bool isMasterMode = xm.LoadedProject.RS485Mode == "Master";
            MODBUSRTUMaster modBUSRTUMaster = xm.LoadedProject.Devices.OfType<MODBUSRTUMaster>().OrderBy(d => d.Name).FirstOrDefault();
            bool masterHasSlaves = modBUSRTUMaster != null && modBUSRTUMaster.Slaves.Count > 0;
            if (isMasterMode || masterHasSlaves)
            {
                if (modBUSRTUMaster != null)
                {
                    _rtuCount = modBUSRTUMaster.Slaves.Count;
                    ModbusRTU(modBUSRTUMaster);
                }
                else
                {
                    _rtuCount = 0;
                }
            }
            else if (isSlaveMode)
            {
                _ccodelist.Add(8520);
                _ccodelist.Add(0);
            }
            else
            {
                _ccodelist.Add(8520);
                _ccodelist.Add(0);
            }
            MODBUSTCPClient modBUSTCPClient = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").OrderBy(d => d.Name).FirstOrDefault();
            _tcpCount = modBUSTCPClient.Slaves.Count();


            MODBUSTCPServer modBUSTCPServer = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").OrderBy(d => d.Name).FirstOrDefault();
            _serverCount = modBUSTCPServer.Requests.Count();
            ModbusTCP(modBUSTCPClient);
            ModbusServer(modBUSTCPServer);
        }

        /// <summary>
        /// Get the Type Of Modbus 1 ->   RTU
        /// </summary>
        /// <param name="mODBUSRTUMasters"></param>
        public void GetModbusType(MODBUSRTUMaster mODBUSRTUMasters)
        {
            foreach (var modbusInfo in mODBUSRTUMasters.Slaves)
            {
                _modbusType = 1;
                _ccodelist.Add(_modbusType);
            }
        }

        public void GetModbusType2(MODBUSTCPClient mODBUSTCPClient)
        {
            foreach (var modbusInfo in mODBUSTCPClient.Slaves)
            {
                _modbusType = 2;
                _ccodelist.Add(_modbusType);
            }
        }

        public void GetModbusType3(MODBUSTCPServer mODBUSTCPServer)
        {
            foreach (var modbusInfo in mODBUSTCPServer.Requests)
            {
                _modbusType = 3;
                _ccodelist.Add(_modbusType);
            }
        }

        public void ModbusRTU(MODBUSRTUMaster master)
        {
            string ModbusRtu = "8520";
            ConvertStringToInt(ModbusRtu);

            int _slavesCount = _rtuCount;
            _ccodelist.Add(_slavesCount);

            foreach (var _modbusInfo in master.Slaves.OrderBy(m => m.Name))
            {
                int _slaveID = _modbusInfo.DeviceId;
                _ccodelist.Add(_slaveID);

                int _polling = _modbusInfo.Polling;
                _ccodelist.Add(_polling);

                string _variable = _modbusInfo.Variable;
                LogicalAddToDec(_variable);

                string _functionCode = ModbusFunctionCode.List.Where(m => m.Text == _modbusInfo.FunctionCode).Select(m => m.ID).FirstOrDefault().ToString("00");// _modbusInfo.FunctionCode;            //Note 3 ==> Considering Only Digit for Function Code "Read Coil (01) "  ==> 01
                ConvertStringToInt(_functionCode);

                int _address = _modbusInfo.Address;
                _ccodelist.Add(_address);

                int _length = _modbusInfo.Length;
                _ccodelist.Add(_length);

                string _disablevar = _modbusInfo.DisablingVariables;
                if (_disablevar == null || _disablevar == "0" || _disablevar == "")
                {
                    _disablevar = "0";
                    ConvertStringToInt(_disablevar);

                }
                else
                    LogicalAddToDec(_disablevar);
            }

        }

        public void ModbusTCP(MODBUSTCPClient client)
        {
            string ModbusTcp = "8521";
            ConvertStringToInt(ModbusTcp);

            int _slavesCount = _tcpCount;
            _ccodelist.Add(_slavesCount);

            foreach (var _modbusInfo in client.Slaves.OrderBy(m => m.Name))
            {
                int _polling = _modbusInfo.Polling;
                _ccodelist.Add(_polling);

                IPAddress IP = _modbusInfo.ServerIPAddress;
                string _ServerIPAddress = IP.ToString();

                string[] IP_Output = _ServerIPAddress.ToString().Split('.');

                string IP_address_1st = IP_Output[0];
                ConvertStringToInt(IP_address_1st);

                string IP_address_2nd = IP_Output[1];
                ConvertStringToInt(IP_address_2nd);

                string IP_address_3rd = IP_Output[2];
                ConvertStringToInt(IP_address_3rd);

                string IP_address_4th = IP_Output[3];
                ConvertStringToInt(IP_address_4th);

                int _portNo = _modbusInfo.Port;
                _ccodelist.Add(_portNo);

                int _slaveID = _modbusInfo.DeviceId;
                _ccodelist.Add(_slaveID);

                string _variable = _modbusInfo.Variable;
                LogicalAddToDec(_variable);

                string _functionCode = ModbusFunctionCode.List.Where(m => m.Text == _modbusInfo.Functioncode).Select(m => m.ID).FirstOrDefault().ToString("00"); //ModbusFunctionCode.List.Where(m => m.Text == _modbusInfo.FunctionCode).Select(m => m.ID).FirstOrDefault().ToString("00");            
                ConvertStringToInt(_functionCode);

                int _address = _modbusInfo.Address;
                _ccodelist.Add(_address);

                int _length = _modbusInfo.Length;
                _ccodelist.Add(_length);
            }
        }

        public void ModbusServer(MODBUSTCPServer modBUSTCPServer)
        {
            string ModbusServer = "8522";
            ConvertStringToInt(ModbusServer);

            int no_of_request = _serverCount;
            _ccodelist.Add(no_of_request);

            foreach (var _modbusServer in modBUSTCPServer.Requests.OrderBy(m => m.Name))
            {
                int _portNo = _modbusServer.Port;
                _ccodelist.Add(_portNo);

                string _variable = _modbusServer.Variable;
                LogicalAddToDec(_variable);

                string functionCode2 = ModbusFunctionCode.List.Where(M => M.Text.Replace("Read ", "").Split('(').First() == _modbusServer.FunctionCode).ToList().Select(M => M.ID).FirstOrDefault().ToString("00");
                ConvertStringToInt(functionCode2);

                int Address = _modbusServer.Address;
                _ccodelist.Add(Address);

                int _length = _modbusServer.Length;
                _ccodelist.Add(_length);


            }
            RetentiveConfig();
        }

        public void RetentiveConfig()
        {
            string RetentiveConfig = "8523";
            ConvertStringToInt(RetentiveConfig);

            int no_of_retentive = 0;

            var RTags = xm.LoadedProject.Tags.Where(d => d.RetentiveAddress != null && d.RetentiveAddress != "" && !d.LogicalAddress.StartsWith("'")).ToList();

            no_of_retentive = RTags.Count();
            _ccodelist.Add(no_of_retentive);

            foreach (var RTag in RTags.OrderBy(m => m.Key))
            {
                string _retaddress = RTag.RetentiveAddress;
                LogicalAddToDec(_retaddress);

                string _logicaladdress = RTag.LogicalAddress;
                LogicalAddToDec(_logicaladdress);
            }
            InitialConfig();
        }

        public void InitialConfig()
        {
            string Intial = "8524";
            ConvertStringToInt(Intial);
            int no_of_initial = 0;

            var ITags = xm.LoadedProject.Tags.Where(d => d.InitialValue != null && d.InitialValue != "" && !d.LogicalAddress.StartsWith("'")).ToList();

            no_of_initial = ITags.Count();
            _ccodelist.Add(no_of_initial);

            foreach (var Tag in ITags.OrderBy(m => m.Key))
            {
                string _logicaladdress = Tag.LogicalAddress;
                LogicalAddToDec(_logicaladdress);

                string IntialVal = Tag.InitialValue;


                if (IntialVal != null)
                {
                    if ((Tag?.Label == "Real" || ((XMPS.Instance?.LoadedProject?.CPUDatatype == "Real") && !(Tag?.LogicalAddress?.Contains(".") ?? true) && !(Tag?.Model?.EndsWith("Tags") ?? true))) || (Tag?.Label == "DINT" && (IntialVal?.StartsWith("-") ?? false)) || (Tag?.Label == "Int" && ((IntialVal?.StartsWith("-") ?? false) || (IntialVal?.Contains(".") ?? false))))
                    {
                        float _intialVal = float.Parse(IntialVal);
                        ConvertFloatValuesToInt(_intialVal, Tag.Label);
                    }
                    else if (Tag.Label == "Double Word")
                    {
                        _ccodelist.Add(long.Parse(IntialVal));
                    }
                    else
                    {
                        ConvertStringToInt(IntialVal);
                    }
                }

            }
            Expansion();
        }

        public void Expansion()
        {
            string Expansion = "8525";
            ConvertStringToInt(Expansion);

            var IOS = xm.LoadedProject.Tags.Where(d => d.Model != "" && d.Model != null && d.IoList == IOListType.ExpansionIO && !d.Label.StartsWith("U")).OrderBy(d => d.Key).ThenBy(d => d.Model).ThenBy(d => d.LogicalAddress);        //Contains Added Expansion without considering Universal I/O

            var Select1 = IOS.Select(e => new { e.LogicalAddress, e.Model }).Distinct().ToList();

            var IOS2 = xm.LoadedProject.Tags.Where(d => d.Model != "" && d.Model != null && d.IoList == IOListType.ExpansionIO).OrderBy(t => t.Key).Select(T => T.Model).Distinct().ToList();      //IOS.Select(T => T.Model).Distinct().ToList();        //Count of Expansion

            int count = (int)IOS2.Count();
            _ccodelist.Add(count);
            foreach (string Tag in IOS2)
            {
                if (Tag.Contains("_"))
                {
                    string modcode1 = RemoteModule.List.Where(e => e.Name == Tag.Substring(0, Tag.Length - 2)).Select(e => e.ModCode).FirstOrDefault();
                    int Converttoint1 = Convert.ToInt32(modcode1, 16);
                    _ccodelist.Add(Converttoint1);
                }
                else
                {
                    string modcode = RemoteModule.List.Where(e => e.Name == Tag).Select(e => e.ModCode).FirstOrDefault();
                    int Converttoint = Convert.ToInt32(modcode, 16);
                    _ccodelist.Add(Converttoint);
                }


                var Logical_address = xm.LoadedProject.Tags.Where(D => D.Model == Tag && (!D.Tag.EndsWith("_OR") && !D.Tag.EndsWith("_OL")))
                                     .OrderBy(m => m.Key).ToList();
                StoreExpansionList(Logical_address);
                if (Tag.Contains("XM-AI2-AO-2") || Tag.Contains("XM-UI4-UO2") || Tag.Contains("XBLD-UI4-UO2") || Tag.Contains("XBLD-AI2-AO2"))
                    SaveCalibrationTagsInfo(Tag);
                //saving filter value for Expansion IOs.
                if (Logical_address.Any(t => (t.Type == IOType.DigitalInput || t.Type == IOType.UniversalInput) && t.IsEnableInputFilter))
                {
                    int filterValue = Convert.ToInt32(Logical_address.FirstOrDefault(t => t.IsEnableInputFilter).InpuFilterValue);
                    _ccodelist.Add(filterValue);
                }
                else
                {
                    if (Tag.Contains("XM-DI-16") || Tag.Contains("XM-DI8-DO6T") || Tag.Contains("XM-UI4-UO2") ||
                        Tag.Contains("XBLD-DI16") || Tag.Contains("XBLD-DI8-DO6") || Tag.Contains("XBLD-UI4-UO2"))
                    {
                        _ccodelist.Add(0);
                    }
                }
            }

            UniversalConfig();
        }

        private void SaveCalibrationTagsInfo(string modelName)
        {
            var Logical_address = xm.LoadedProject.Tags.Where(d => d.Model == modelName && (d.Label.EndsWith("_OR") || d.Label.EndsWith("_OL")))
                        .ToList().OrderBy(t => t.Type.ToString().EndsWith("Output")).ThenBy(t => t.Label.ToString().Substring(2, 1));

            foreach (XMIOConfig value in Logical_address)
            {
                LogicalAddToDec(value.LogicalAddress);
            }
        }

        //As Discuss with Messung UI - UO Are Covered Under Expansion Module
        public void UniversalConfig()
        {
            //Adding Interrupt Logic Blocks information
            AddInterruptLogicBlocksInfo();
            AddTaskConfigInfo();

            var slaveDevice = xm.LoadedProject.Devices.OfType<MODBUSRTUSlaves>().OrderBy(d => d.Name).FirstOrDefault();
            AddModbusRTUSlaves(slaveDevice);

            _ccodelist.Add(_endframe);

            NotePadFile(_ccodelist);

        }

        private void AddModbusRTUSlaves(MODBUSRTUSlaves slaves)
        {
            if (xm.LoadedProject == null) return;
            bool isSlaveMode = xm.LoadedProject.RS485Mode == "Slave";
            _ccodelist.Add(8528);
            if (isSlaveMode)
            {
                int slaveID = xm.LoadedProject.SlaveID;
                if (slaveID >= 1 && slaveID <= 247)
                {
                    _ccodelist.Add(slaveID);
                }
                else
                {
                    _ccodelist.Add(0);
                }
            }
            else
            {
                _ccodelist.Add(0);
            }
        }

        // Adding Task Config information
        private void AddTaskConfigInfo()
        {
            _ccodelist.Add(8527);
            _ccodelist.Add(1);
            int sTime = xm.LoadedProject.ScanTime;
            _ccodelist.Add(sTime);

            bool isCheckEnable = xm.LoadedProject.IsEnable;
            _ccodelist.Add(isCheckEnable ? 1 : 0);

            int time = xm.LoadedProject.TimeRange;
            _ccodelist.Add(time);
        }

        private void AddInterruptLogicBlocksInfo()
        {
            _ccodelist.Add(8526);

            //first Interrupt Block
            _ccodelist.Add(1);
            string firstInerrupt = xm.LoadedProject.FirstInterruptBlock;
            if (firstInerrupt != null)
            {
                if (firstInerrupt.EndsWith("01"))
                    _ccodelist.Add(1);
                else if (firstInerrupt.EndsWith("02"))
                    _ccodelist.Add(2);
                else if (firstInerrupt.EndsWith("03"))
                    _ccodelist.Add(3);
                else if (firstInerrupt.EndsWith("04"))
                    _ccodelist.Add(4);
                else
                    _ccodelist.Add(0);
            }
            else
            {
                _ccodelist.Add(0);
            }

            //Second Interrupt Block
            _ccodelist.Add(2);
            string secondInterrupt = xm.LoadedProject.SecondInterruptBlock;
            if (secondInterrupt != null)
            {
                if (secondInterrupt.EndsWith("01"))
                    _ccodelist.Add(1);
                else if (secondInterrupt.EndsWith("02"))
                    _ccodelist.Add(2);
                else if (secondInterrupt.EndsWith("03"))
                    _ccodelist.Add(3);
                else if (secondInterrupt.EndsWith("04"))
                    _ccodelist.Add(4);
                else
                    _ccodelist.Add(0);
            }
            else
            {
                _ccodelist.Add(0);
            }

            //Third Interrupt Block
            _ccodelist.Add(3);
            string thirdInterrupt = xm.LoadedProject.ThirdInterruptBlock;
            if (thirdInterrupt != null)
            {
                if (thirdInterrupt.EndsWith("01"))
                    _ccodelist.Add(1);
                else if (thirdInterrupt.EndsWith("02"))
                    _ccodelist.Add(2);
                else if (thirdInterrupt.EndsWith("03"))
                    _ccodelist.Add(3);
                else if (thirdInterrupt.EndsWith("04"))
                    _ccodelist.Add(4);
                else
                    _ccodelist.Add(0);
            }
            else
            {
                _ccodelist.Add(0);
            }

            //Fourth Interrupt Block
            _ccodelist.Add(4);
            string fourthInterrupt = xm.LoadedProject.FourthInterruptBlock;
            if (fourthInterrupt != null)
            {
                if (fourthInterrupt.EndsWith("01"))
                    _ccodelist.Add(1);
                else if (fourthInterrupt.EndsWith("02"))
                    _ccodelist.Add(2);
                else if (fourthInterrupt.EndsWith("03"))
                    _ccodelist.Add(3);
                else if (fourthInterrupt.EndsWith("04"))
                    _ccodelist.Add(4);
                else
                    _ccodelist.Add(0);
            }
            else
            {
                _ccodelist.Add(0);
            }
        }

        // Converts the Expansion List Logical Address to Decimal Values
        public void StoreExpansionList(List<Base.XMIOConfig> values)
        {
            foreach (Base.XMIOConfig value in values)
            {
                //_convertlogicaladdess = GetAddressHex(value.LogicalAddress);
                LogicalAddToDec(value.LogicalAddress);

                if (value.Type == IOType.AnalogInput || value.Type == IOType.AnalogOutput)
                {
                    string mode = value.Mode;
                    if (mode != "" && mode != null)
                    {
                        string getModVal = Mode.List.Where(d => d.Text == mode).Select(d => d.ID).FirstOrDefault().ToString();
                        ConvertStringToInt(getModVal);
                    }

                    else
                    {
                        string getModVal = "0";
                        ConvertStringToInt(getModVal);
                    }

                }
                else if (value.Type == IOType.UniversalInput)
                {
                    string mode = value.Mode;
                    if (mode != "" && mode != null)
                    {
                        string getModVal = ModeUI.List.Where(d => d.Text == mode).Select(d => d.ID).FirstOrDefault().ToString();
                        ConvertStringToInt(getModVal);
                    }

                    else
                    {
                        string getModVal = "0";                         //If Mode Value is not present adding Zero
                        ConvertStringToInt(getModVal);
                    }

                }
                else if (value.Type == IOType.UniversalOutput)
                {
                    string mode = value.Mode;
                    if (mode != "" && mode != null)
                    {
                        string getModVal = ModeUO.List.Where(d => d.Text == mode).Select(d => d.ID).FirstOrDefault().ToString();
                        ConvertStringToInt(getModVal);
                    }

                    else
                    {
                        string getModVal = "0";                         //If Mode Value is not present adding Zero
                        ConvertStringToInt(getModVal);
                    }
                }
            }
        }

        //Converts string to decimal Values
        public void ConvertStringToInt(string str)
        {
            if (str.Contains("."))
            {
                ConvertFloatValuesToInt(float.Parse(str));
            }
            else
            {
                int intvalue;
                intvalue = int.Parse(str);
                _ccodelist.Add(intvalue);
            }
        }


        /// <summary>
        /// Convert the Logical Address to Decimal values
        /// </summary>
        /// <param name="data"></param>
        public void LogicalAddToDec(string data)
        {
            if (data.Contains("~"))
            {
                string data1 = data.Substring(1);
                _convertlogicaladdess = xm.GetHexAddress(data1);
            }
            else
            {
                _convertlogicaladdess = xm.GetHexAddress(data);

            }
            uint integerValue = uint.Parse(_convertlogicaladdess, System.Globalization.NumberStyles.HexNumber);

            _ccodelist.Add((uint)integerValue);
        }

        /// <summary>
        /// Transfering list to notepad file
        /// </summary>
        /// <param name="_ccodelist"></param>
        public void NotePadFile(List<long> _ccodelist)
        {
            //For Creating CRC of CCode
            _ccodelistForCRC.AddRange(_ccodelist);
            PerformXOR();

            string filepath = xm.CurrentProjectData.ProjectPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            string newfilepath = xm.CurrentProjectData.ProjectPath.Replace(filepath, "CcodeVersion.txt");
            using (StreamWriter sw = new StreamWriter(newfilepath))
            {

                foreach (long i in _ccodelist)
                {
                    sw.WriteLine(i);
                }

            }

            //Clear List After WriteFile
            _ccodelist.Clear();
        }

        private void PerformXOR()
        {
            Ethernet ethernet = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
            xm.LoadedProject.CcodeCRC = 0;
            if (_ccodelistForCRC.Count == 0)
            {
                xm.LoadedProject.CcodeCRC = 0;
            }
            xm.LoadedProject.CcodeCRC = _ccodelistForCRC[0];
            bool startSkipping = false;
            for (int i = 1; i < _ccodelistForCRC.Count; i++)
            {
                xm.LoadedProject.CcodeCRC ^= _ccodelistForCRC[i];
            }
            string CCodeCRC = (xm.LoadedProject.CcodeCRC).ToString("X");
            string MCodeCRC = (xm.LoadedProject.McodeCRC).ToString("X");
            string MQTTCRC = (xm.LoadedProject.MQTTCRC).ToString("X");
            long oldprogCRC = xm.LoadedProject.ProgramCRC;
            long finalCcodeMcode = Convert.ToInt64(MCodeCRC, 16) ^ Convert.ToInt64((xm.LoadedProject.MCodeChangeCRC).ToString("X"), 16) ^ Convert.ToInt64(CCodeCRC, 16) ^ Convert.ToInt64(MQTTCRC, 16) ^ Convert.ToInt64("97", 16);
            xm.LoadedProject.ProgramCRC = finalCcodeMcode;
            _ccodelist.Insert(1, 8545);
            _ccodelist.Insert(2, finalCcodeMcode);
            if (oldprogCRC == finalCcodeMcode)
            {
                if (!ProjectHelper.CheckWithOldFile("CcodeVersion.txt", _ccodelist))
                {
                    finalCcodeMcode ^= 1;
                    xm.LoadedProject.ProgramCRC = finalCcodeMcode;
                }
            }
            _ccodelistForCRC.Clear();
        }

        private long GetCRCOfIpAddress(string _ipAddress)
        {
            long _ipCRC = 0;
            string[] ipAddresOP = _ipAddress.Split('.');
            for (int cnt = 0; cnt < 4; cnt++)
                _ipCRC ^= int.Parse(ipAddresOP[cnt]);
            return _ipCRC;
        }

        /// <summary>
        /// Convert Float value to Byte array generate Int Value  12345.6 ==> 1178658406
        /// </summary>
        /// <param name="value"></param>
        public void ConvertFloatValuesToInt(float value)
        {
            float float_variable = value;
            byte[] bytes = BitConverter.GetBytes(float_variable);

            int convertedInt = BitConverter.ToInt32(bytes, 0);
            //Console.WriteLine("Converted integer: " + convertedInt);
            _ccodelist.Add(convertedInt);
            //Logic for Checking if Generate number is actual previous float number
            float float_variable1 = BitConverter.ToSingle(bytes, 0);
        }
        public void ConvertFloatValuesToInt(float value, string Datatype)
        {
            float float_variable = value;
            byte[] bytes = BitConverter.GetBytes(float_variable);


            int convertedInt = BitConverter.ToInt32(bytes, 0);
            if (Datatype == "Int" || Datatype == "DINT")
                convertedInt = Convert.ToInt32(value);

            if (value < 0)
            {
                long bit_value_32 = 4294967296;
                int absIntValue = Math.Abs(convertedInt);
                long Real_result = bit_value_32 - absIntValue;

                _ccodelist.Add(Real_result);
            }
            else
                _ccodelist.Add(convertedInt);
        }

        static string ApplyIPpadding(string ipAddress)
        {
            string formattedIpAddress = "";
            List<string> recvdIpAddress = new List<string> { };
            recvdIpAddress.AddRange(ipAddress.Split('.'));
            foreach (string partIP in recvdIpAddress)
                formattedIpAddress += padding(partIP, 3).ToString() + ".";
            return formattedIpAddress.Substring(0, formattedIpAddress.Length - 1);
        }

        static string padding(string str, int len)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str == "")
                {
                    if (len == 2)
                    {
                        str = "00";
                    }
                    else
                        str = "000";

                }
                else
                {
                    if (len == 2)
                    {
                        str = int.Parse(str).ToString("00");
                    }
                    else
                        str = int.Parse(str).ToString("000");
                }
            }
            return str;
        }
    }
}
