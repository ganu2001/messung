using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.LadderLogic;
namespace XMPS2000.Core.App
{
    public class XMProject
    {
        private string _plcModel;
        private List<XMIOConfig> _tags;
        private string _projectPath = string.Empty;
        private string _projectName = string.Empty;
        private List<Device> _devices;
        private List<XMIOConfig> _IOConfig;
        private List<Block> _blocks;
        private List<string> _MainLadderLogic;
        private List<string> _errorStatusTags;
        private List<ApplicationRung> _logicRungs;
        private List<ErrorTags> _errorTags;
        private List<UDFBInfo> _udfbinfo;
        private BacNetIP _bacNetIP;
        private SystemConfiguration _systemConfiguration;

        private bool _isEditableDigitalFilter;

        private int _newTagIndex = 0;
        private int _newFocusIndex = 0;
        //variable to store task config data
        public int _scanTime;
        public int _timeRange;
        public bool _isEnable = false;
        public bool isChanged = false;
        public int _previousIndex = 0;
        public string _plcStatus = string.Empty;
        public List<string> CommonAddresses { get; set; } = new List<string>();
        private List<HSIO> _HsioBlocks;
        public string RS485Mode { get; set; } = null;
        public int SlaveID { get; set; } = 0;
        //Adding Variable For TopicSelected in Mcode
        private string _topicSelected;
        private string _plcStatusTag;
        //Adding for the clearing the filter
        private bool _clearFilter;

        public long _ccodeCRC;
        public long _mcodeCRC;
        public string _firstInterruptBlock;
        public string _secondInterruptBlock;
        public string _thirdInterruptBlock;
        public string _fourthInterruptBlock;
        public long _mqttCRC;
        public long _bcodeCRC;
        private long _programcodeCRC;
        private long _mcodechangedCRC;
        private bool _diagnosticParametersEnabled = false;
        private string _cupDatatype;
        private string _expansionErrorType;
        private List<RESISTANCETable_Values> _resistanceValues;
        private List<RESISTANCETable> _resistanceTables = new List<RESISTANCETable>();

        [XmlAttribute("PlcModel")]
        public string PlcModel { get => _plcModel; set => _plcModel = value; }
        public string CPUDatatype { get => _cupDatatype; set => _cupDatatype = value; }
        public string ExpansionErrorType { get => _expansionErrorType; set => _expansionErrorType = value; }


        [XmlElement("ProjectPath")]
        public string ProjectPath { get => _projectPath; set => _projectPath = value; }

        [XmlElement("ProjectName")]
        public string ProjectName { get => _projectName; set => _projectName = value; }

        public List<XMIOConfig> Tags { get => _tags; set => _tags = value; }

        public List<Device> Devices { get => _devices; set => _devices = value; }

        public List<XMIOConfig> IOConfig { get => _IOConfig; set => _IOConfig = value; }

        public List<Block> Blocks { get => _blocks; set => _blocks = value; }

        public List<string> MainLadderLogic { get => _MainLadderLogic; set => _MainLadderLogic = value; }
        public List<ApplicationRung> LogicRungs { get => _logicRungs; set => _logicRungs = value; }
        public List<ErrorTags> ErrorTags { get => _errorTags; set => _errorTags = value; }
        public List<UDFBInfo> UDFBInfo { get => _udfbinfo; set => _udfbinfo = value; }
        public List<RESISTANCETable_Values> ResistanceValues { get => _resistanceValues; set => _resistanceValues = value; }
        public List<string> UDFBEditChoices { get; set; } = new List<string>();
        public SystemConfiguration SystemConfiguration { get => _systemConfiguration; set => _systemConfiguration = value; }
        public BacNetIP BacNetIP { get => _bacNetIP; set => _bacNetIP = value; }
        //Added Two Variable Ccode CRC and Mcode CRC for the sending extra frame for plc
        public long CcodeCRC { get => _ccodeCRC; set => _ccodeCRC = value; }
        public long McodeCRC { get => _mcodeCRC; set => _mcodeCRC = value; }
        public long ProgramCRC { get => _programcodeCRC; set => _programcodeCRC = value; }
        public string PLCStatusTag { get => _plcStatusTag; set => _plcStatusTag = value; }
        public List<string> ErrorStatusTags { get => _errorStatusTags; set => _errorStatusTags = value; }


        public long MCodeChangeCRC { get => _mcodechangedCRC; set => _mcodechangedCRC = value; }

        //Stroing information of selected interrupt blocks
        public string FirstInterruptBlock { get => _firstInterruptBlock; set => _firstInterruptBlock = value; }
        public string SecondInterruptBlock { get => _secondInterruptBlock; set => _secondInterruptBlock = value; }
        public string ThirdInterruptBlock { get => _thirdInterruptBlock; set => _thirdInterruptBlock = value; }
        public string FourthInterruptBlock { get => _fourthInterruptBlock; set => _fourthInterruptBlock = value; }

        public List<HSIO> HsioBlock { get => _HsioBlocks; set => _HsioBlocks = value; }

        //Storing information of Scan Time Configuration
        public int ScanTime { get => _scanTime; set => _scanTime = value; }

        public bool IsEnable { get => _isEnable; set => _isEnable = value; }
        public int TimeRange { get => _timeRange; set => _timeRange = value; }

        //Storing the Editable filter status info
        public bool IsEditableDigitalFilter { get => _isEditableDigitalFilter; set => _isEditableDigitalFilter = value; }

        //Adding Variable For TopicSelected in Mcode
        [ObsoleteAttribute]
        public string TopicSelected { get => _topicSelected; set => _topicSelected = value; }
        public long MQTTCRC { get => _mqttCRC; set => _mqttCRC = value; }
        public int NewAddedTagIndex { get => _newTagIndex; set => _newTagIndex = value; }
        public int NewFocusIndex { get => _newFocusIndex; set => _newFocusIndex = value; }
        public long BCodeCRC { get => _bcodeCRC; set => _bcodeCRC = value; }
        public bool DiagnosticParametersEnabled { get => _diagnosticParametersEnabled; set => _diagnosticParametersEnabled = value; }
        [XmlArray("ResistanceTables")]
        [XmlArrayItem("ResistanceTable")]
        public List<RESISTANCETable> ResistanceTables {
       
            get => _resistanceTables;
            set => _resistanceTables = value;
        }
        public bool HasDiagnosticTags { get; set; }
        public bool IsDigitalFilterApply { get; set; }

        //for clearing the filter
        public bool ClearFilter { get => _clearFilter; set => _clearFilter = value; }
        public XMProject()
        {
            Tags = new List<XMIOConfig>();
            IOConfig = new List<XMIOConfig>();
            Devices = new List<Device>();
            Blocks = new List<Block>();
            LogicRungs = new List<ApplicationRung>();
            ErrorTags = new List<ErrorTags>();
            SystemConfiguration = new SystemConfiguration();
            _resistanceTables = new List<RESISTANCETable>();
            this._projectPath = string.Empty;
            this._projectName = string.Empty;
        }

        public XMProject(string projectPath, string projectName)
        {
            Tags = new List<XMIOConfig>();
            IOConfig = new List<XMIOConfig>();
            Devices = new List<Device>();
            Blocks = new List<Block>();
            LogicRungs = new List<ApplicationRung>();
            ErrorTags = new List<ErrorTags>();
            SystemConfiguration = new SystemConfiguration();
            _resistanceTables = new List<RESISTANCETable>();
            this._projectPath = projectPath;
            this._projectName = projectName;
        }
        public string GetUDFBChoice(string udfbName)
        {
            string entry = UDFBEditChoices.FirstOrDefault(x => !string.IsNullOrEmpty(x) && x.StartsWith(udfbName + "=", StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrEmpty(entry))
                return null;
            return entry.Substring(udfbName.Length + 1); // after '='
        }

        public void SetUDFBChoice(string udfbName, string choice)
        {
            if (string.IsNullOrEmpty(udfbName) || string.IsNullOrEmpty(choice))
                return;

            UDFBEditChoices.RemoveAll(x => !string.IsNullOrEmpty(x) && x.StartsWith(udfbName + "=", StringComparison.OrdinalIgnoreCase));
            UDFBEditChoices.Add(udfbName + "=" + choice);
        }

        public void AddRung(ApplicationRung applicationRung)
        {
            LogicRungs.Add(applicationRung);
        }

        public string AddBlock(NodeInfo nodeInfo, string updatedName)
        {
            if (string.IsNullOrEmpty(updatedName))
            {
                var newBlockName = String.Format("{0}{1}", nodeInfo.Info, GetBlockSequence(nodeInfo.Info));
                Block blk = new Block();
                blk.Name = newBlockName;
                blk.Type = nodeInfo.Info;
                Blocks.Add(blk);
                return newBlockName;
            }
            else
            {
                Block blk = new Block();
                blk.Name = updatedName;
                blk.Type = nodeInfo.Info;
                Blocks.Add(blk);
                return updatedName;
            }
        }
        public void AddUDFBBlock(NodeInfo nodeInfo, string newBlockName)
        {
            Block blk = new Block();
            blk.Name = newBlockName;
            blk.Type = nodeInfo.Info;
            Blocks.Add(blk);
        }

        public string GetSlaveName(NodeInfo nodeInfo)
        {
            string newSlaveName = string.Empty;

            if (nodeInfo.Info == "MODBUSRTUMaster")
            {
                newSlaveName = String.Format("{0}{1}", nodeInfo.Info + "Slave", GetRTUSlavesSequence(nodeInfo.Info));
            }
            else if (nodeInfo.Info == "MODBUSRTUSlaves")
            {
                newSlaveName = String.Format("{0}{1}", nodeInfo.Info + "Slave", GetRTUSlaves_SlaveSequence(nodeInfo.Info));
            }
            else
            {
                newSlaveName = String.Format("{0}{1}", nodeInfo.Info + "Slave", GetTCPSlavesSequence(nodeInfo.Info));
            }
            return newSlaveName;
        }

        public string GetMqttName(NodeInfo nodeInfo)
        {
            string newMqttName = string.Empty;
            if (nodeInfo.Info == "MQTT Publish")
            {
                newMqttName = string.Format("{0}{1}", nodeInfo.Info + "Publish", GetPublishSequence(nodeInfo.Info));
            }
            else if (nodeInfo.Info == "MQTT Subscribe")
            {
                newMqttName = string.Format("{0}{1}", nodeInfo.Info + "Subscribe", GetPublishSequence(nodeInfo.Info));
            }
            return newMqttName;
        }

        public string GetRequestName(NodeInfo nodeInfo)
        {
            string newReqName = string.Empty;
            newReqName = String.Format("{0}{1}", nodeInfo.Info + "Request", GetRequestSequence(nodeInfo.Info));
            return newReqName;
        }

        private string GetBlockSequence(string nodeType)
        {
            var listOfBlocks = this.Blocks.Where(b => b.Type == nodeType).Select(b => b.Name).ToList();
            int temp = listOfBlocks.Count();
            if (temp == 0)
            {
                temp = 0;
            }
            else
            {
                var maxOfLogicBlock = this.Blocks.Where(b => b.Type == nodeType).Select(b => b.Name).Where(b => b.Contains("LogicBlock")).Any() == false ? 0
                    : Array.ConvertAll(this.Blocks.Where(b => b.Type == nodeType).
                     Select(b => b.Name).Where(b => b.Contains("LogicBlock")).
                     Select(d => d.Remove(0, 10)).Where(d => d.Any() && Regex.IsMatch(d, "^[0-9]+$")).ToArray(), s => int.Parse(s)).DefaultIfEmpty(0).Max();
                var max = maxOfLogicBlock;
                if (this.LogicRungs.Count() > 0)
                {
                    var maxOfLogicRungs = this.LogicRungs.Select(b => b.Name).Where(b => b.Contains("LadderForm#LogicBlock")).Any() ?
                                                Array.ConvertAll(this.LogicRungs.Select(b => b.Name).Where(b => b.Contains("LadderForm#LogicBlock")).
                                                Select(d => d.Remove(0, 21)).Where(d => d.Any() && Regex.IsMatch(d, "^[0-9]+$")).ToArray(), s => int.Parse(s)).Max() : 0;
                    max = Math.Max(maxOfLogicRungs, maxOfLogicBlock);
                }
                temp = max > temp ? max : temp;
            }
            return (++temp).ToString("00");
        }

        private string GetRequestSequence(string nodeType)
        {
            var modBUSTCPServer = (MODBUSTCPServer)this.Devices.Where(d => d.GetType().Name == nodeType).FirstOrDefault();
            int temp = 0;
            int number = 0;
            foreach (var req in modBUSTCPServer.Requests)
            {
                var success = Int32.TryParse(req.Name.Replace(nodeType + "Request", ""), out number);
                temp = success ? (temp < number) ? number : temp : temp;
            }
            return (++temp).ToString("00");
        }
        private string GetRTUSlavesSequence(string nodeType)
        {
            var device = this.Devices
                            .FirstOrDefault(d => d.GetType().Name == nodeType) as MODBUSRTUMaster;

            if (device == null || device.Slaves == null)
                return "01"; // default starting sequence

            int temp = 0;
            int number = 0;
            foreach (var slave in device.Slaves)
            {
                var success = Int32.TryParse(slave.Name.Replace(nodeType + "Slave", ""), out number);
                temp = success ? Math.Max(temp, number) : temp;
            }
            return (++temp).ToString("00");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        private string GetRTUSlaves_SlaveSequence(string nodeType)
        {
            var device = (MODBUSRTUSlaves)this.Devices.Where(d => d.GetType().Name == nodeType).FirstOrDefault();
            int temp = 0;
            int number = 0;
            foreach (var slave in device.Slaves)
            {
                var success = Int32.TryParse(slave.Name.Replace(nodeType + "Slave", ""), out number);
                temp = success ? (temp < number) ? number : temp : temp;
            }
            return (++temp).ToString("00");
        }

        private string GetTCPSlavesSequence(string nodeType)
        {
            var device = (MODBUSTCPClient)this.Devices.Where(d => d.GetType().Name == nodeType).FirstOrDefault();
            int temp = 0;
            int number = 0;
            if (device != null)
            {
                foreach (var slave in device.Slaves)
                {
                    var success = Int32.TryParse(slave.Name.Replace(nodeType + "Slave", ""), out number);
                    temp = success ? (temp < number) ? number : temp : temp;
                }
            }
            return (++temp).ToString("00");
        }

        public string GetPublishSequence(string nodeType)
        {
            var device = (MQTTPublish)this.Devices.Where(d => d.GetType().Name == nodeType).FirstOrDefault();
            int temp = 0;
            if (device != null)
            {
                foreach (var publish in device.MQTTPublish_Blocks)
                {
                }
            }
            return (++temp).ToString("00");
        }




    }
}
