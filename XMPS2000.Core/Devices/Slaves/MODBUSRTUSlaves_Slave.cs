using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XMPS2000.Core.Devices.Slaves
{
    public class MODBUSRTUSlaves_Slave
    {
       
        private int _address;
        private int _length;
        private string _variable;
        private string _functionCode;
        private string _tag;
        private string _name;
        private bool _isDeleted = false;

        public MODBUSRTUSlaves_Slave()
        {

        }

        public MODBUSRTUSlaves_Slave(string name,int address, int length, string variable, string tag, string functioCode, bool isDeleted = false)
        {
            
            _address = address;
            _length = length;
            _variable = variable;
            _tag = tag;
            _functionCode = functioCode;
            _name = name;
            _isDeleted = isDeleted;

        }

        [XmlElement("Address")]
        public int Address { get => _address; set => _address = value; }

        [XmlElement("Length")]
        public int Length { get => _length; set => _length = value; }

        [XmlElement("Variable")]
        public string Variable { get => _variable; set => _variable = value; }

        [DisplayName("Function Code")]
        [XmlElement("FunctionCode")]
        public string FunctionCode { get => _functionCode; set => _functionCode = value; }

        [XmlElement("Name")]
        public string Name { get => _name; set => _name = value; }

        [XmlElement("Tag")]
        public string Tag { get => _tag; set => _tag = value; }

        public bool IsDeletedRequest { get => _isDeleted; set => _isDeleted = value; }

    }
}
