using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.Base;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.Devices
{
    public class MODBUSTCPServer_Request
    {
        int _address;
        int _length;
        int _port;
        string _variable;
        string _functionCode;
        //int _deviceId;
      //  string Tag;

        private string _name;
        private string _Tag;
        //Adding one check for the checking deleted Tag
        private bool _isDeleted = false;
        public MODBUSTCPServer_Request()
        {

        }

        public MODBUSTCPServer_Request(string name, int address, int length, int port, string variable,string Tag, string functionCode, bool isDeleted=false)
        {
            _address = address;
            _length = length;
            _port = port;
            _variable = variable;
            _functionCode = functionCode;
            //_deviceId = deviceId;
            _name = name;
            _Tag = Tag;
            _isDeleted = false;
        }

        [XmlElement("Address")]
        public int Address { get => _address; set => _address = value; }

        //[DisplayName("Device ID")]
        //[XmlElement("DeviceId")]
        //public int DeviceId { get => _deviceId; set => _deviceId = value; }

        [XmlElement("Length")]
        public int Length { get => _length; set => _length = value; }
        
        [XmlElement("Port")]
        public int Port { get => _port; set => _port = value; }
        
        [XmlElement("Variable")]
        public string Variable { get => _variable; set => _variable = value; }

        [DisplayName("Function Code")]
        [XmlElement("FunctionCode")]
        public string FunctionCode { get => _functionCode; set => _functionCode = value; }

        [XmlElement("Name")]
        public string Name { get => _name; set => _name = value; }

        public string Tag { get => _Tag;set => _Tag = value; }
        public bool IsDeletedRequest { get => _isDeleted; set => _isDeleted = value; }

    }
}
