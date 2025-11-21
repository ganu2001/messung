using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.LadderLogic;
using XMPS2000.Core.Serializer;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.App
{

    public static class ProjectHelper
    {
        private static XMPS xm;
        private static SerializeDeserialize<XMProject> _sdXMProject = new SerializeDeserialize<XMProject>();
        public static XMProject LoadXMProject(string ProjectPath)
        {
            XMProject _project = _sdXMProject.DeserializeData(ProjectPath);
            _project.ProjectName = _project.ProjectName.Replace(" ", "");
            return _project;
        }

        public static void SaveXMProject(XMProject project)
        {
            XMProject _project = project;
            var projectFile = _sdXMProject.SerializeData(project);
            var fullPath = _project.ProjectPath;
            File.Delete(fullPath);
            File.WriteAllText(fullPath, projectFile, Encoding.Unicode);

            xm = XMPS.Instance;            
            if (xm.isCompilied)
            {
                xm.errorInCompiler = false;

                List<string> errorInBlock = new List<string>();
                DataTable Appdata = GenerateAppCSV();
                App.CSVHelper csvHelper = new App.CSVHelper();
                //Adding Interrupt Logical Blocks into the Main Ladder Logic
                xm.LoadedProject.MainLadderLogic.RemoveAll(T => T.StartsWith("Interrupt_Logic_Block"));
                for (int i = 1; i <= 4; i++)
                {
                    string blockName = "Interrupt_Logic_Block0" + i;

                    if (xm.LoadedProject.MainLadderLogic.Where(b => b.Contains(blockName)).Count() == 0)
                    {
                        if (xm.LoadedProject.FirstInterruptBlock == blockName || xm.LoadedProject.SecondInterruptBlock == blockName ||
                            xm.LoadedProject.ThirdInterruptBlock == blockName || xm.LoadedProject.FourthInterruptBlock == blockName)
                        {
                            xm.LoadedProject.MainLadderLogic.Add(blockName);
                        }
                    }
                }

                foreach (string _curBlock in xm.LoadedProject.MainLadderLogic)
                {
                    if (_curBlock.Contains("[") && _curBlock.Contains("]") && _curBlock.Contains("???"))
                    {
                        throw new Exception(String.Join(Environment.NewLine, $"Main : Tag is not assigned on contact which is added with {_curBlock.Substring(_curBlock.IndexOf("[") + 1, _curBlock.IndexOf("]") - _curBlock.IndexOf("[") - 1)}"));
                    }
                    string delimiter = "AND";
                    string[] usedTags = _curBlock.Split(new string[] { delimiter }, StringSplitOptions.None);
                    foreach (string addressTag in usedTags)
                    {
                        if (addressTag.Contains('('))
                        {
                            int checktag = xm.LoadedProject.Tags.Where(t => t.Tag == addressTag.Replace("'", "").Replace("~", "").Replace('(', ' ').Replace(')', ' ').Trim().ToString() && !t.LogicalAddress.StartsWith("'")).Count();
                            if (checktag == 0)
                                throw new Exception(String.Join(Environment.NewLine, $"Main : Tag is not assigned on contact " + addressTag.Replace('(', ' ').Replace(')', ' ').Trim().ToString() + $" which is added on {_curBlock.Substring(_curBlock.IndexOf("[") + 1, _curBlock.IndexOf("]") - _curBlock.IndexOf("[") - 1)}"));
                        }
                    }
                    // Handle for the sequence contain no element
                    if (!_curBlock.StartsWith("'"))
                    {
                        string newCurBlock = _curBlock.Contains("[") ? _curBlock.Substring(_curBlock.IndexOf("[") + 1, _curBlock.IndexOf("]") - _curBlock.IndexOf("[") - 1) : _curBlock;
                        if (newCurBlock == "")
                        {
                            continue;
                        }

                        //Adding Check for the Commented Block for Not Generating the CSV File
                        //belowCondition Added For Checking if the MainLaddderLogicBloc does not start with
                        //"'".

                        if (!(newCurBlock.StartsWith("'")))
                        {
                            if (xm.LoadedProject.Blocks.Where(x => x.Name == newCurBlock).Count() > 0)
                            {
                                List<string> curBlockRungs = xm.LoadedProject.Blocks.Where(x => x.Name == newCurBlock).First().Elements;
                                errorInBlock.Add(newCurBlock);
                                (bool error, List<Tuple<int, string>> errorList) = csvHelper.GetData(curBlockRungs, newCurBlock, ref Appdata);
                                if (error)
                                {
                                    xm.errorInCompiler = true;
                                    foreach (Tuple<int, string> _error in errorList)
                                    {
                                        errorInBlock.Add($"\tRung {_error.Item1}: {_error.Item2}");
                                    }
                                }
                                else
                                {
                                    errorInBlock.Add("\tBlock compilation successful");
                                }
                            }
                        }

                    }

                }

                if (xm.errorInCompiler)
                {
                    throw new Exception(String.Join(Environment.NewLine, errorInBlock));
                }

                File.Delete(fullPath.Replace(".xmprj", "App.csv"));
                for (int i = 0; i < Appdata.Rows.Count; i++)
                {
                    Appdata.Rows[i]["Line Number"] = string.IsNullOrEmpty(Appdata.Rows[i]["Line Number"].ToString())
                                                    ? (i + 1).ToString()
                                                    : $"{Appdata.Rows[i]["Line Number"]}@{i + 1}";
                } 
                SaveDatatoCSV(fullPath.Replace(".xmprj", "App.csv"), Appdata);

                if (xm.LoadedProject.PlcModel != null && xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                {
                    BcodeGeneration bcode = new BcodeGeneration();
                    bcode.CalculateBcodeFile();
                }
                MqttTextGeneration mqttTextGeneration = new MqttTextGeneration();
                mqttTextGeneration.FetchDataToText();

                McodeGeneration mcodeGeneration = new McodeGeneration(); //--->Working on Ccode
                mcodeGeneration.FetchDataCSV(ref Appdata);

                var _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MessungSystems\XMPS2000\XM Projects\\" + xm.LoadedProject.ProjectName.Replace(".xmprj", "") + "\\License").Replace("*", "");
                if ((!System.IO.Directory.Exists(_path)))
                {
                    System.IO.Directory.CreateDirectory(_path);
                }
                MQTTForm mqttForm = (MQTTForm)xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "MQTTForm");
                if (mqttForm != null)
                {
                    string[] allPaths = new string[] { mqttForm.ca3certificate, mqttForm.clientCertificate, mqttForm.clientKey, mqttForm.license };
                    foreach (var file in allPaths)
                    {
                        if (file != null && file != "")
                            File.Copy(file, Path.Combine(_path, Path.GetFileName(file)), true);
                    }
                }

                var ApplicationRecs1 = xm.LoadedProject.LogicRungs;
                foreach (ApplicationRung ApRec in ApplicationRecs1)
                {
                    if (ApRec.Name.StartsWith("B"))
                    {
                        string str = ApRec.Name.ToString().Replace("B", "LadderForm#LogicBlock");
                        ApRec.Name = str;
                    }

                }
                File.Delete(fullPath.Replace(".xmprj", "Config.csv"));
                SaveDatatoCSV(fullPath.Replace(".xmprj", "Config.csv"), GenerateConfigCSV());

                CcodeGeneration ccodeGeneration = new CcodeGeneration();
                ccodeGeneration.ConfigComSettings();
            }
        }

        private static DataTable GenerateAppCSV()
        {
            DataTable appData = new DataTable();

            appData.Columns.Add("Line Number");
            appData.Columns.Add("T/C Name");
            appData.Columns.Add("OutputType");
            appData.Columns.Add("DataType");
            appData.Columns.Add("Enable");
            appData.Columns.Add("Output1");
            appData.Columns.Add("Output2");
            appData.Columns.Add("Op Code");
            appData.Columns.Add("Input1");
            appData.Columns.Add("Input2");
            appData.Columns.Add("Input3");
            appData.Columns.Add("Input4");
            appData.Columns.Add("Comments");
            appData.Columns.Add("WindowName");
            appData.Columns.Add("LadderName");
            return appData;
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

        static string ApplyIPpadding(string ipAddress)
        {
            string formattedIpAddress = "";
            List<string> recvdIpAddress = new List<string> { };
            recvdIpAddress.AddRange(ipAddress.Split('.'));
            foreach (string partIP in recvdIpAddress)
                formattedIpAddress += padding(partIP, 3).ToString() + ".";
            return formattedIpAddress.Substring(0, formattedIpAddress.Length - 1);
        }
        private static DataTable GenerateConfigCSV()
        {
            DataTable Dt = new DataTable();
            Dt.Columns.Add("ConfigType");
            Dt.Columns.Add("Use DHCP");
            Dt.Columns.Add("IP Address");
            Dt.Columns.Add("Subnet");
            Dt.Columns.Add("Gateway");
            Dt.Columns.Add("Port Number");
            Dt.Columns.Add("Baud Rate");
            Dt.Columns.Add("Data Length");
            Dt.Columns.Add("Stop Bit");
            Dt.Columns.Add("Parity");
            Dt.Columns.Add("SendDelay");
            Dt.Columns.Add("MinimumInterface");
            Dt.Columns.Add("On-Board IO");
            Dt.Columns.Add("Remote IO");
            Dt.Columns.Add("ModbusType");
            Dt.Columns.Add("Slave ID");
            Dt.Columns.Add("CommunicationTimeout");
            Dt.Columns.Add("NoOfRetries");
            Dt.Columns.Add("Polling");
            Dt.Columns.Add("SlaveIPAddress");
            Dt.Columns.Add("TCPPort");
            Dt.Columns.Add("Variable");
            Dt.Columns.Add("Data Start address");
            Dt.Columns.Add("Data Size");
            Dt.Columns.Add("FunctionCode");
            Dt.Columns.Add("IO List");
            Dt.Columns.Add("Model");
            Dt.Columns.Add("Type");
            Dt.Columns.Add("Mode");
            Dt.Columns.Add("Label");
            Dt.Columns.Add("Logical Address");
            Dt.Columns.Add("Tag");
            Dt.Columns.Add("RetAdd");
            Dt.Columns.Add("Init_val");


            var comsetting = (COMDevice)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "COMDevice").FirstOrDefault();
            DataRow Dr = Dt.NewRow();
            Dr["ConfigType"] = "COM Settings";
            Dr["Baud Rate"] = ((int)Enum.Parse(typeof(COMBaudRate), "_" + comsetting.BaudRate.ToString())) - 1;
            Dr["Data Length"] = ((int)Enum.Parse(typeof(COMDataLength), "_" + comsetting.DataLength.ToString())) - 1;  //comsetting.DataLength;
            Dr["Stop Bit"] = ((int)Enum.Parse(typeof(COMStopBit), "_" + comsetting.StopBit.ToString())) - 1;  //comsetting.StopBit;
            Dr["Parity"] = ((int)Enum.Parse(typeof(COMParity), comsetting.Parity.ToString())) - 1;  //comsetting.Parity;
            Dr["SendDelay"] = comsetting.SendDelay;
            Dr["MinimumInterface"] = comsetting.MinimumInterface;
            Dr["CommunicationTimeout"] = comsetting.CommunicationTimeout;
            Dr["NoOfRetries"] = comsetting.NumberOfRetries;
            Dt.Rows.Add(Dr);

            DataRow Dr1 = Dt.NewRow();
            var ethernetset = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
            Dr1["ConfigType"] = "Ethernet Settings";
            Dr1["Use DHCP"] = Convert.ToByte(ethernetset.UseDHCPServer);
            Dr1["IP Address"] = ApplyIPpadding(ethernetset.EthernetIPAddress.ToString());
            Dr1["Subnet"] = ApplyIPpadding(ethernetset.EthernetSubNet.ToString());
            Dr1["Gateway"] = ApplyIPpadding(ethernetset.EthernetGetWay == null ? "0.0.0.0": ethernetset.EthernetGetWay.ToString());
            Dr1["Port Number"] = ethernetset.Port;
            Dt.Rows.Add(Dr1);

            DataRow DrCE = Dt.NewRow();
            DrCE["ConfigType"] = "NEthernet Settings";
            DrCE["Use DHCP"] = "-";
            DrCE["IP Address"] = ApplyIPpadding(ethernetset.ChangeIPAddress.ToString());
            DrCE["Subnet"] = ApplyIPpadding(ethernetset.ChangeSubNet.ToString());
            DrCE["Gateway"] = ApplyIPpadding(ethernetset.ChangeGetWay.ToString());
            DrCE["Port Number"] = "-";
            Dt.Rows.Add(DrCE);

            DataRow DrDI = Dt.NewRow();
            DrDI["ConfigType"] = "Digital Input";
            DrDI["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.DigitalInput && d.IoList == Types.IOListType.OnBoardIO).Count();
            DrDI["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.DigitalInput && d.IoList != Types.IOListType.OnBoardIO).Count();
            Dt.Rows.Add(DrDI);
            DataRow DrDO = Dt.NewRow();
            DrDO["ConfigType"] = "Digital Output";
            DrDO["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.DigitalOutput && d.IoList == Types.IOListType.OnBoardIO).Count();
            DrDO["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.DigitalOutput && d.IoList != Types.IOListType.OnBoardIO).Count();
            Dt.Rows.Add(DrDO);
            DataRow DrAI = Dt.NewRow();
            DrAI["ConfigType"] = "Analog Input";
            DrAI["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.AnalogInput && d.IoList == Types.IOListType.OnBoardIO).Count();
            DrAI["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.AnalogInput && d.IoList != Types.IOListType.OnBoardIO).Count();
            Dt.Rows.Add(DrAI);
            DataRow DrAO = Dt.NewRow();
            DrAO["ConfigType"] = "Analog Output";
            DrAO["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.AnalogOutput && d.IoList == Types.IOListType.OnBoardIO).Count();
            DrAO["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.AnalogOutput && d.IoList != Types.IOListType.OnBoardIO).Count();
            Dt.Rows.Add(DrAO);
            //Adding For Universal
            DataRow DrUI = Dt.NewRow();
            DrUI["ConfigType"] = "Universal Input";
            DrUI["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.UniversalInput && d.IoList == Types.IOListType.OnBoardIO).Count();
            DrUI["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.UniversalInput && d.IoList != Types.IOListType.OnBoardIO).Count();
            Dt.Rows.Add(DrUI);
            DataRow DrUO = Dt.NewRow();
            DrUO["ConfigType"] = "Universal Output";
            DrUO["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.UniversalOutput && d.IoList == Types.IOListType.OnBoardIO).Count();
            DrUO["Remote IO"] = xm.LoadedProject.Tags.Where(d => d.Type == Types.IOType.UniversalOutput && d.IoList != Types.IOListType.OnBoardIO).Count();
            Dt.Rows.Add(DrUO);

            var OBIOS = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).OrderBy(d => d.Model).ThenBy(d => d.LogicalAddress);     //Contains DigitalInput / O/P 
            foreach (var io in OBIOS)
            {
                DataRow DrIO = Dt.NewRow();
                DrIO["ConfigType"] = "IO_Mapping";
                if (io.Label == "DI0")
                {
                    DrIO["IO List"] = io.IoList;
                    string modelcode = RemoteModule.List.Where(R => R.Name == io.Model).Select(R => R.ModCode).FirstOrDefault();
                    DrIO["Model"] = modelcode == null ? io.Model : modelcode;
                    DrIO["Type"] = io.Type;
                }
                if (io.Label == "DO0" || io.Label == "AO0" || io.Label == "DI0" || io.Label == "AI0" || io.Label == "UI0" || io.Label == "UO0")
                {
                    DrIO["Type"] = io.Type;
                }
                DrIO["Label"] = io.Label;
                DrIO["Logical Address"] = io.LogicalAddress;
                DrIO["Tag"] = io.Tag;

                //Adding Modes Values for the the HSIO Block
                if (xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).Select(d => d.Model).FirstOrDefault() == "XM-14-DT-HIO")
                {
                    DrIO["Mode"] = io.Mode?.ToString();
                }
                else
                {
                    DrIO["Mode"] = (io.Mode == null || io.Mode == "" || io.Mode == "-") ? "-" :Mode.List.Where(d => d.Text == io.Mode).Select(d => d.ID).FirstOrDefault()?.ToString() ?? "-";
                }

                Dt.Rows.Add(DrIO);
            }
            string exmodel = "";
            var IOS = xm.LoadedProject.Tags.Where(d => d.Model != "" && d.Model != null && d.IoList != IOListType.OnBoardIO).OrderBy(d => d.Key).ThenBy(d => d.Model).ThenBy(d => d.LogicalAddress);        //Contains Added Expansion & Remote Module
            foreach (var io in IOS)
            {
                DataRow DrIO = Dt.NewRow();
                DrIO["ConfigType"] = "IO_Mapping";
                if (exmodel == "" || exmodel != io.Model)
                {
                    DrIO["IO List"] = io.IoList;
                    string modelcode = "";
                    if (io.Model.ToString().IndexOf("_") < 0)
                    {
                        modelcode = RemoteModule.List.Where(R => R.Name == io.Model).Select(R => R.ModCode).FirstOrDefault();
                        DrIO["Model"] = (modelcode == null || modelcode == "null" || modelcode == "") ? io.Model : modelcode;
                    }
                    else
                    {
                        modelcode = RemoteModule.List.Where(R => R.Name == io.Model.Substring(0, io.Model.ToString().IndexOf("_"))).Select(R => R.ModCode).FirstOrDefault();
                        DrIO["Model"] = (modelcode == null || modelcode == "null" || modelcode == "") ? io.Model.Substring(0, io.Model.ToString().IndexOf("_")) : modelcode;
                    }

                    DrIO["Type"] = io.Type;
                }
                if (io.Label == "DO0" || io.Label == "AO0" || io.Label == "DI0" || io.Label == "AI0")
                {
                    DrIO["Type"] = io.Type;
                }
                DrIO["Label"] = io.Label;
                DrIO["Logical Address"] = io.LogicalAddress;
                DrIO["Tag"] = io.Tag;
                if (io.Label.StartsWith("UI"))
                {
                    DrIO["Mode"] = (io.Mode == null || io.Mode == "" || io.Mode == "-") ? "-" : ModeUI.List.Where(d => d.Text == io.Mode).Select(d => d.ID).FirstOrDefault().ToString();
                }
                else if (io.Label.StartsWith("UO"))
                {
                    DrIO["Mode"] = (io.Mode == null || io.Mode == "" || io.Mode == "-") ? "-" : ModeUO.List.Where(d => d.Text == io.Mode).Select(d => d.ID).FirstOrDefault().ToString();
                }
                else
                {
                    DrIO["Mode"] = (io.Mode == null || io.Mode == "" || io.Mode == "-") ? "-" : Mode.List.Where(d => d.Text == io.Mode).Select(d => d.ID).FirstOrDefault().ToString();

                }
                Dt.Rows.Add(DrIO);
                exmodel = io.Model;
            }

            MODBUSRTUMaster modBUSRTUMaster = (MODBUSRTUMaster)xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "MODBUSRTUMaster");
            if (modBUSRTUMaster != null)
            {
                foreach (var Modbus in modBUSRTUMaster.Slaves)
                {
                    var Modcomsetting = (COMDevice)xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "COMDevice");
                    DataRow DrModbus = Dt.NewRow();
                    DrModbus["ModbusType"] = 1;
                    DrModbus["ConfigType"] = "Modbus";
                    DrModbus["Slave ID"] = Modbus.DeviceId;
                    DrModbus["Polling"] = Modbus.Polling;
                    DrModbus["Variable"] = Modbus.Variable;
                    DrModbus["FunctionCode"] = ModbusFunctionCode.List.Where(m => m.Text == Modbus.FunctionCode).Select(m => m.ID).FirstOrDefault().ToString("00");
                    DrModbus["Data Start address"] = Modbus.Address;
                    DrModbus["Logical Address"] = string.IsNullOrEmpty(Modbus.DisablingVariables)? "0": Modbus.DisablingVariables;
                    DrModbus["Data Size"] = Modbus.Length;
                    Dt.Rows.Add(DrModbus);
                }
            }




            ///For Testing Purpose
            MODBUSRTUSlaves modBUSRTUSlaves = (MODBUSRTUSlaves)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
            if (modBUSRTUSlaves != null)
            {
                foreach (var Modbus in modBUSRTUSlaves.Slaves)
                {
                    var Modcomsetting = (COMDevice)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "COMDevice").FirstOrDefault();
                    DataRow DrModbus = Dt.NewRow();
                    DrModbus["ModbusType"] = 1;
                    DrModbus["ConfigType"] = "Modbus";
                    DrModbus["Variable"] = Modbus.Variable;
                    DrModbus["FunctionCode"] = ModbusFunctionCode.List.Where(m => m.Text == Modbus.FunctionCode).Select(m => m.ID).FirstOrDefault().ToString("00");
                    DrModbus["Data Start address"] = Modbus.Address;
                    DrModbus["Data Size"] = Modbus.Length;

                    Dt.Rows.Add(DrModbus);
                }

            }


            MODBUSTCPClient modBUSTCPClient = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
            foreach (var Modbus in modBUSTCPClient.Slaves)
            {
                DataRow DrModbus = Dt.NewRow();
                DrModbus["ModbusType"] = 2;
                DrModbus["ConfigType"] = "Modbus";
                DrModbus["Slave ID"] = Modbus.DeviceId;
                DrModbus["Polling"] = Modbus.Polling;
                DrModbus["SlaveIPAddress"] = Modbus.ServerIPAddress;
                DrModbus["TCPPort"] = Modbus.Port;
                DrModbus["Variable"] = Modbus.Variable;
                DrModbus["FunctionCode"] = ModbusFunctionCode.List.Where(m => m.Text == Modbus.Functioncode).Select(m => m.ID).FirstOrDefault().ToString("00");
                DrModbus["Data Start address"] = Modbus.Address;
                DrModbus["Data Size"] = Modbus.Length;
                Dt.Rows.Add(DrModbus);
            }

            MODBUSTCPServer modBUSTCPServer = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
            foreach (var Modbus in modBUSTCPServer.Requests)
            {
                DataRow DrModbus = Dt.NewRow();
                DrModbus["ModbusType"] = 3;
                DrModbus["ConfigType"] = "Modbus";
                DrModbus["TCPPort"] = Modbus.Port;
                DrModbus["Variable"] = Modbus.Variable;
                DrModbus["FunctionCode"] = ModbusFunctionCode.List.Where(M => M.Text.Replace("Read ", "").Split('(').First() == Modbus.FunctionCode).ToList().Select(M => M.ID).FirstOrDefault().ToString("00");
                DrModbus["Data Start address"] = Modbus.Address;
                DrModbus["Data Size"] = Modbus.Length;
                Dt.Rows.Add(DrModbus);

            }


            //Commeted Tag those Logical Address are Start With "'" those are not allowed to added in CSV
            // For Above Command Added Condition where !(d.logicalAddress.StartWith("'"))
            var MTags = xm.LoadedProject.Tags.Where(d => !(d.LogicalAddress.StartsWith("'")) && d.Model == "" || d.Model == null && d.IoList != IOListType.Default);
            foreach (var mtag in MTags)
            {
                DataRow DrTag = Dt.NewRow();
                DrTag["ConfigType"] = "Memory_Address";
                DrTag["Logical Address"] = mtag.LogicalAddress;
                DrTag["Tag"] = mtag.Tag;
                DrTag["Label"] = mtag.Label;
                Dt.Rows.Add(DrTag);
            }


            var RTags = xm.LoadedProject.Tags.Where(d => d.RetentiveAddress != null && !d.LogicalAddress.StartsWith("'") && d.RetentiveAddress != "");
            foreach (var tag in RTags)
            {
                DataRow DrTag = Dt.NewRow();
                DrTag["ConfigType"] = "Retentive";
                DrTag["Logical Address"] = tag.LogicalAddress;
                DrTag["RetAdd"] = tag.RetentiveAddress;
                Dt.Rows.Add(DrTag);
            }

            var ITags = xm.LoadedProject.Tags.Where(d => d.InitialValue != null && !d.LogicalAddress.StartsWith("'") && d.InitialValue != "");
            foreach (var tag in ITags)
            {
                DataRow DrTag = Dt.NewRow();
                DrTag["ConfigType"] = "Init_val";
                DrTag["Logical Address"] = tag.LogicalAddress;
                DrTag["Init_val"] = tag.InitialValue;
                Dt.Rows.Add(DrTag);
            }

            return (Dt);
        }

        internal static void UploadTFTPFile(string filepath, string ipaddress)
        {
            string From = filepath;
            //We hace complete file name with it's path we need to split it so that we can get actual file name
            string myfile = filepath;
            //Split is not recognising \\ so replace it with some other char 
            myfile = myfile.Replace("\\", "$");
            string[] fullpath = myfile.Split('$');
            //After splitting we must get actual file name in the last node of the splitted string which is derived by Length - 1
            string actfilenam = fullpath[fullpath.Length - 1].Replace(fullpath[fullpath.Length - 2], "");
            //Actual Uploading of file
            TFTPClient t = new TFTPClient(ipaddress);
            t.Get(@"" + actfilenam.Replace(".txt", ".zip"), @"" + filepath);
        }

        /// <summary>
        /// Convert Data From CSV file string to Datatabel
        /// </summary>
        /// <param name="fileName"></param> Name of the file to be converted
        /// <param name="delimiters"></param> delimitter used
        /// <param name="columnsToValidate"></param>columns to be validated with
        /// <returns></returns>
        public static DataTable NewDataTable(string fileName, string delimiters, string[] columnsToValidate)
        {
            DataTable result = new DataTable();

            using (TextFieldParser tfp = new TextFieldParser(fileName))
            {
                tfp.SetDelimiters(delimiters);

                // Get Some Column Names
                if (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();

                    for (int i = 0; i < fields.Count(); i++)
                    {
                        if (string.Equals(fields[i], columnsToValidate[i]))
                            result.Columns.Add(fields[i]);
                        else
                            throw new Exception("Received invalid column name. Expected: "
                                + columnsToValidate[i] + " Received: " + fields[i]);
                    }
                }

                // Get Remaining Rows
                while (!tfp.EndOfData)
                    result.Rows.Add(tfp.ReadFields());
            }

            return result;
        }

        /// <summary>
        /// Get the Data Transformed from List Of Class to Datatable, for Application File with removal of all unnecessary columns and renaming them
        /// </summary>
        /// <typeparam name="data"></typeparam>List of objects
        /// <returns></returns>Datatable of converted records
        public static DataTable BeforeSaveToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                string propnm;
                propnm = prop.Name;
                propnm = (propnm == "LineNumber") ? "Line Number" : propnm;
                propnm = (propnm == "TC_Name") ? "T/C Name" : propnm;
                propnm = (propnm == "OpCode") ? "Op Code" : propnm;
                table.Columns.Add(propnm, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {

                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                if (values[1].ToString() != "1")
                {
                    values[17] = "";
                }
                else
                {
                    values[17] = values[0];
                }
                table.Rows.Add(values);
            }
            table.Columns.Remove("OutPutType_NM");
            table.Columns.Remove("DataType_Nm");
            table.Columns.Remove("OpCodeNm");
            table.Columns.Remove("Name");
            return table;
        }

        /// <summary>
        /// Convert Data from List of objects to the Satatable
        /// </summary>
        /// <param name="data"></param>List of Class Objects to be converted from Class objects to Datatable
        /// <returns></returns>Convrted Datatable 
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        /// <summary>
        /// Save Data to CSV File
        /// </summary>
        /// <param name="Filename"></param> Name of file and path to save
        /// <param name="dt"></param> Datatable to save as csv file
        public static void SaveDatatoCSV(string Filename, DataTable dt)
        {
            string csvString = ExportDataTableToCSV(dt);
            File.WriteAllText(Filename, csvString);
        }


        /// <summary>
        /// Export data from Datatable to CSV format with delimiter as ","
        /// </summary>
        /// <param name="dt"></param> Datatable to convert the rows to string
        /// <returns></returns>
        public static string ExportDataTableToCSV(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }

        public static RecentProject CreateNewProject(string projectPath, ProjectTemplate template)
        {
            var tempProject = _sdXMProject.DeserializeData(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\" + template.TemplatePath);
            tempProject.ProjectPath = projectPath;
            if (xm == null)
                xm = XMPS.Instance;
            xm.PlcStatus = "";
            tempProject.ProjectName = Path.GetFileName(projectPath);
            var projectFile = _sdXMProject.SerializeData(tempProject);
            Directory.CreateDirectory(Path.GetDirectoryName(projectPath));
            File.WriteAllText(projectPath, projectFile, Encoding.Unicode);

            RecentProject recentProject = new RecentProject();
            recentProject.ProjectName = Path.GetFileName(projectPath);
            recentProject.ProjectPath = projectPath;

            return recentProject;
        }

        public static RecentProject SaveAsProject(string projectPath, ProjectTemplate template, XMProject tempProject)
        {
            var newProject = _sdXMProject.DeserializeData(tempProject.ProjectPath);
            newProject.ProjectPath = projectPath;
            newProject.ProjectName = Path.GetFileName(projectPath);
            var projectFile = _sdXMProject.SerializeData(newProject);
            //Add code to create directory and file while save as if file not present.
            string directory = Path.GetDirectoryName(projectPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(projectPath))
            {
                using (File.Create(projectPath)) { }
            }
            File.WriteAllText(projectPath, projectFile, Encoding.Unicode);

            RecentProject recentProject = new RecentProject();
            recentProject.ProjectName = Path.GetFileName(projectPath);
            recentProject.ProjectPath = projectPath;

            return recentProject;
        }
        public static string GetModelName(string ModelName)
        {
            return RemoteModule.List.Where(R => R.ModCode == ModelName).Select(R => R.Name).FirstOrDefault();
        }

        public static int DownloadTFTPFile(string filepath, string ipaddress)
        {
            string From = filepath;
            //We hace complete file name with it's path we need to split it so that we can get actual file name
            string myfile = filepath;
            //Split is not recognising \\ so replace it with some other char 
            myfile = myfile.Replace("\\", "$");
            string[] fullpath = myfile.Split('$');
            //After splitting we must get actual file name in the last node of the splitted string which is derived by Length - 1
            string actfilenam = "";
            if (myfile.Contains("App.csv"))
                actfilenam = "App.csv";
            else if (myfile.Contains("Config.csv"))
                actfilenam = "Config.csv";
            else if (myfile.Contains(".txt"))
                actfilenam = myfile.Split('$').Last().Replace("Version.txt", ".bin");
            else
                actfilenam = fullpath[fullpath.Length - 1].Replace(fullpath[fullpath.Length - 2], "");
            //Actual Uploading of file
            TFTPClient t = new TFTPClient(ipaddress);
            return t.Put(@"" + actfilenam, @"" + filepath, ipaddress);
        }


        public static DataTable getIO_ConfigurationDataForPrintting()
        {
            xm = XMPS.Instance;
            DataTable Dt = new DataTable();
            Dt.Columns.Add("IO List");
            Dt.Columns.Add("Model");
            Dt.Columns.Add("Type");
            Dt.Columns.Add("Label");
            Dt.Columns.Add("Logical Address");
            Dt.Columns.Add("Tag");
            Dt.Columns.Add("Mode");
            Dt.Columns.Add("Select Mode");
            var OBIOS = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO).OrderBy(d => d.Model).ThenBy(d => d.LogicalAddress);
            foreach (var io in OBIOS)
            {
                DataRow DrIO = Dt.NewRow();
                if (io.Label == "DI0")
                {
                    DrIO["IO List"] = io.IoList;
                    string modelcode = RemoteModule.List.Where(R => R.Name == io.Model).Select(R => R.ModCode).FirstOrDefault();
                    DrIO["Model"] = modelcode == null ? io.Model : modelcode;
                    DrIO["Type"] = io.Type;
                }
                if (io.Label == "DO0" || io.Label == "AO0" || io.Label == "DI0" || io.Label == "AI0")
                {
                    DrIO["Type"] = io.Type;
                }
                DrIO["Label"] = io.Label;
                DrIO["Logical Address"] = io.LogicalAddress;
                DrIO["Tag"] = io.Tag;
                DrIO["Mode"] = (io.Mode is null || io.Mode == "") ? "-" : Mode.List.Where(d => d.Text == io.Mode).Select(d => d.ID).FirstOrDefault().ToString();
                Dt.Rows.Add(DrIO);
            }
            string exmodel = "";
            var IOS = xm.LoadedProject.Tags.Where(d => d.Model != "" && d.IoList != IOListType.OnBoardIO).OrderBy(d => d.Model).ThenBy(d => d.LogicalAddress);
            foreach (var io in IOS)
            {
                DataRow DrIO = Dt.NewRow();
                if (exmodel == "" || exmodel != io.Model)
                {
                    DrIO["IO List"] = io.IoList;
                    string modelcode = RemoteModule.List.Where(R => R.Name == io.Model).Select(R => R.ModCode).FirstOrDefault();
                    DrIO["Model"] = (modelcode == null || modelcode == "null" || modelcode == "") ? io.Model : modelcode;
                    DrIO["Type"] = io.Type;
                }
                if (io.Label == "DO0" || io.Label == "AO0" || io.Label == "DI0" || io.Label == "AI0")
                {
                    DrIO["Type"] = io.Type;
                }
                DrIO["Label"] = io.Label;
                DrIO["Logical Address"] = io.LogicalAddress;
                DrIO["Tag"] = io.Tag;
                DrIO["Mode"] = (io.Mode is null || io.Mode == "") ? "-" : Mode.List.Where(d => d.Text == io.Mode).Select(d => d.ID).FirstOrDefault().ToString();
                Dt.Rows.Add(DrIO);
                exmodel = io.Model;

            }
            return Dt;
        }

        public static bool CheckWithOldFile(string filename, List<long> actfile)
        {
            // Create a temporary file in memory
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Write some data to the memory stream (this simulates the in-memory file)
                StreamWriter writer = new StreamWriter(memoryStream);
                writer.Write(actfile);
                writer.Flush();

                // Reset the position to the beginning of the stream
                memoryStream.Position = 0;

                // Create a temporary file path
                string tempFilePath = Path.Combine(Path.GetDirectoryName(GetfilePath(filename)), Path.GetFileNameWithoutExtension(GetfilePath(filename)) + "_temp.txt");

                // Save MemoryStream to the temporary file
                using (StreamWriter sw = new StreamWriter(tempFilePath))
                {
                    foreach (long i in actfile)
                    {
                        sw.WriteLine(i);
                    }
                }

                // Compare the saved file with the in-memory file
                bool areFilesEqual = FileCompare(GetfilePath(filename), tempFilePath);
                File.Delete(tempFilePath);
                return areFilesEqual;
            }
        }

        public static bool CheckFileCRC(List<long> mcodelist2, string filetype)
        {
            long newMcodeCRC = mcodelist2[0];
            for (int i = 1; i < mcodelist2.Count; i++)
                newMcodeCRC ^= mcodelist2[i];
            if (filetype == "Ccode")
                return xm.LoadedProject.CcodeCRC == newMcodeCRC;
            else
                return xm.LoadedProject.McodeCRC == newMcodeCRC;
        }

        public static string GetfilePath(string filename)
        {
            string filepath = xm.CurrentProjectData.ProjectPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            return xm.CurrentProjectData.ProjectPath.Replace(filepath, filename);
        }

        public static bool FileCompare(string file1, string file2)
        {
            if (!File.Exists(file1)) return true;
            using (var sha256 = SHA256.Create())
            {
                byte[] hash1 = GetFileHash(sha256, file1);
                byte[] hash2 = GetFileHash(sha256, file2);

                return StructuralComparisons.StructuralEqualityComparer.Equals(hash1, hash2);
            }
        }

        private static byte[] GetFileHash(HashAlgorithm hashAlgorithm, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return hashAlgorithm.ComputeHash(stream);
            }
        }
    }
}
