using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.Devices.Slaves
{
    public class MODBUSTCPClient_Slave
    {

        IPAddress _serverIPAddress;
        int _port;
        int _polling;
        int _deviceId;
        int _address;
        int _length;
        string _variable;
        string _functionCode;
        private string _name;
        private string _tag;
        //Adding one check for the checking deleted Tag
        private bool _isDeleted = false;
        string _multiplicationFactor;
        public MODBUSTCPClient_Slave()
        {

        }

        public MODBUSTCPClient_Slave(string name, IPAddress serverIPAddress, int port, int polling, int deviceId, int address, int length, string variable, string tag, string functionCode,bool isDelete,string multiplicationFactor)
        {
            _name = name;
            _serverIPAddress = serverIPAddress;
            _port = port;
            _polling = polling;
            _deviceId = deviceId;
            _address = address;
            _length = length;
            _variable = variable;
            _tag = tag;
            _functionCode = functionCode;
            _isDeleted = isDelete;
            _multiplicationFactor = multiplicationFactor;
        }

        [DisplayName("Server IP Address")]
        [XmlIgnore]
        public IPAddress ServerIPAddress { get => _serverIPAddress; set => _serverIPAddress = value; }

        [Browsable(false)]
        [XmlElement("ServerIPAddress")]
        public string ServerIPAddressForXML { get => _serverIPAddress.ToString(); set => _serverIPAddress = IPAddress.Parse(value); }

        [XmlElement("Port")]
        public int Port { get => _port; set => _port = value; }

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
        public string Functioncode { get => _functionCode; set => _functionCode = value; }

        [DisplayName("Multiplication Factor")]
        [XmlElement("MultiplicationFactor")]
        public string MultiplicationFactor { get => _multiplicationFactor; set => _multiplicationFactor = value; }

        [XmlElement("Name")]
        public string Name { get => _name; set => _name = value; }

        public string Tag { get => _tag; set => _tag = value; }

        public bool IsDeletedRequest { get => _isDeleted; set => _isDeleted = value; }


    }
}
