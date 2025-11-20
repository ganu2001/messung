using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.Devices.Slaves
{
    public class MODBUSRTUMaster_Slave
    {
        private int _polling;
        private int _deviceId;
        private int _address;
        private int _length;
        private string _variable;
        private string _functionCode;
        private string _name;
        private string _tag;
        private string _disablingVariables;
        //Adding one check for the checking deleted Tag
        private bool _isDeleted = false;
        string _multiplicationFactor;
        public MODBUSRTUMaster_Slave()
        {
            this._disablingVariables = "0";
        }
        
        public MODBUSRTUMaster_Slave(string name, int polling, int deviceId, int address, int length, string variable, string tag,string functioCode,string disablingVariables,bool isDeleted, string multiplicationFactor)
        {
            _polling = polling;
            _deviceId = deviceId;
            _address = address;
            _length = length;
            _variable = variable;
            _tag = tag;
            _functionCode = functioCode;
            _name = name;
            _disablingVariables = disablingVariables;
            _isDeleted = isDeleted;
            _multiplicationFactor = multiplicationFactor;
            //if (disablingVariables=="")
            //{
            //  _disablingVariables= "0";
            //}
            //else
            //{
            //  _disablingVariables = disablingVariables;
            //}

        }

        [XmlElement("Polling")]
        public int Polling { get => _polling; set => _polling = value; }

        [DisplayName("Device ID")]
        [XmlElement("DeviceId")]
        public int DeviceId { get => _deviceId; set => _deviceId = value; }

        [XmlElement("Address")]
        public int Address { get => _address; set => _address = value; }

        [XmlElement("Length")]
        public int Length { get => _length; set => _length = value; }

        [XmlElement("Variable")]
        public string Variable { get => _variable; set => _variable = value; }

        [DisplayName("Function Code")]
        [XmlElement("FunctionCode")]
        public string FunctionCode { get => _functionCode; set => _functionCode = value; }

        [DisplayName("Multiplication Factor")]
        [XmlElement("MultiplicationFactor")]
        public string MultiplicationFactor { get => _multiplicationFactor; set => _multiplicationFactor = value; }

        [XmlElement("Name")]
        public string Name { get => _name; set => _name = value; }

        [XmlElement("Tag")]
        public string Tag { get => _tag; set => _tag = value; }

        [DisplayName("Disabling Variables")]
        [XmlElement("DisablingVariables")]
        public string DisablingVariables { get => _disablingVariables; set=> _disablingVariables=value; }

        public bool IsDeletedRequest { get => _isDeleted; set => _isDeleted = value; }
    }
}
