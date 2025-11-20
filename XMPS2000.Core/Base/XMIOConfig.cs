using System.Collections.Generic;
using System.Xml.Serialization;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.Base
{
    public class XMIOConfig
    {
        IOListType _ioList;
        string _model;
        IOType _type;
        string _label;
        string _logicalAddress;
        string _tag;
        string _initialValue;
        private bool _retentive;
        private bool _showLogicalAddress;
        private string _retentiveAddress;
        private string _mode;
        private bool _editable;
        private int _key = 1;
        //Added One Parameter For Pack Instruction
        private string _actualName;
        //Adding List of List of EnumValues
        private List<EnumValue> _enumValues;
        private bool _readonly;
        //for Digital Filer
        private bool _isEnableInputFilter;
        private string _inpuFilterValue;
        public XMIOConfig()
        {
            _enumValues = new List<EnumValue>();
        }

        public XMIOConfig(IOListType ioList, string model, IOType type, string label, string logicalAddress, string tag,string Mode)
        {
            _ioList = ioList;
            _model = model;
            _type = type;
            _label = label;
            _logicalAddress = logicalAddress;
            _tag = tag;
            _initialValue = InitialValue;
            _retentive = Retentive;
            _showLogicalAddress = ShowLogicalAddress;
            _retentiveAddress = RetentiveAddress;
            _mode = Mode;
            _editable = Editable;
            _key = Key + 1;
            _key++;
        }

        [XmlAttribute("Model")]
        public string Model { get => _model; set => _model = value; }

        [XmlAttribute("Label")]
        public string Label { get => _label; set => _label = value; }

        [XmlAttribute("LogicalAddress")]
        public string LogicalAddress { get => _logicalAddress; set => _logicalAddress = value; }

        [XmlAttribute("Tag")]
        public string Tag { get => _tag; set => _tag = value; }

        [XmlAttribute("IOList")]
        public IOListType IoList { get => _ioList; set => _ioList = value; }

        [XmlAttribute("Type")]
        public IOType Type { get => _type; set => _type = value; }

        [XmlAttribute("InitialValue")]
        public string InitialValue { get => _initialValue; set => _initialValue = value; }

        [XmlAttribute("Retentive")]
        public bool Retentive { get => _retentive; set => _retentive = value; }

        [XmlAttribute("ShowLogicalAddress")]
        public bool ShowLogicalAddress { get => _showLogicalAddress; set => _showLogicalAddress = value; }

        [XmlAttribute("RetentiveAddress")]
        public string  RetentiveAddress { get => _retentiveAddress; set => _retentiveAddress = value; }

        [XmlAttribute("Mode")]
        public string Mode { get => _mode; set => _mode = value; }

        [XmlAttribute("Editable")]
        public bool Editable { get => _editable; set => _editable = value; }

        [XmlAttribute("ReadOnly")]
        public bool ReadOnly { get => _readonly; set => _readonly = value; }
        public List<EnumValue> EnumValues { get=> _enumValues; set=>_enumValues=value; }
        public int Key { get => _key; set => _key = value; }
        public string ActualName { get=>_actualName; set=>_actualName = value; }
        public bool IsEnableInputFilter { get => _isEnableInputFilter; set => _isEnableInputFilter = value; }
        public string InpuFilterValue { get => _inpuFilterValue; set => _inpuFilterValue = value; }

    }

    public class EnumValue
    {
        public int Value;
        public string ValueName;

        public EnumValue()
        {

        }
    }
}