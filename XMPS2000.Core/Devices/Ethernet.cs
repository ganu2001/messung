using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.Base;

namespace XMPS2000.Core.Devices
{
    public class Ethernet : Device
    {
        private bool _useDHCPServer;
        private IPAddress _ethernetIPAddress;
        private IPAddress _ethernetSubNet;
        private IPAddress _ethernetGetWay;
        private IPAddress _changeIPAddress;
        private IPAddress _changeSubNet;
        private IPAddress _changeGetWay;
        private int _port;
        private int _networkNo;

        public Ethernet()
        {
            _ethernetIPAddress = IPAddress.Parse("0.0.0.0");
            _ethernetSubNet = IPAddress.Parse("0.0.0.0");
            _ethernetGetWay = IPAddress.Parse("0.0.0.0");
            _changeIPAddress = IPAddress.Parse("0.0.0.0");
            _changeSubNet = IPAddress.Parse("0.0.0.0");
            _changeGetWay = IPAddress.Parse("0.0.0.0");
            _useDHCPServer = false;
            _port = 0;
            _networkNo = 0;
        }

        [DisplayName("Use DHCP Server")]
        [XmlElement("UseDHCPServer")]
        public bool UseDHCPServer { get => _useDHCPServer; set => _useDHCPServer = value; }

        [DisplayName("IP Address")]
        [XmlIgnore]
        public IPAddress EthernetIPAddress { get => _ethernetIPAddress; set => _ethernetIPAddress = value; }

        [DisplayName("Sub Net")]
        [XmlIgnore]
        public IPAddress EthernetSubNet { get => _ethernetSubNet; set => _ethernetSubNet = value; }

        [DisplayName("GateWay")]
        [XmlIgnore]
        public IPAddress EthernetGetWay { get => _ethernetGetWay; set => _ethernetGetWay = value; }

        [Browsable(false)]
        [XmlElement("EthernetIPAddress")]
        public string EthernetIPAddressForXML { get => _ethernetIPAddress.ToString(); set => _ethernetIPAddress = IPAddress.Parse(value); }

        [Browsable(false)]
        [XmlElement("EthernetSubNet")]
        public string EthernetSubNetForXML { get => _ethernetSubNet.ToString(); set => _ethernetSubNet = IPAddress.Parse(value); }

        [Browsable(false)]
        [XmlElement("EthernetGetWay")]
        public string EthernetGetWayForXML { get => _ethernetGetWay?.ToString(); set => _ethernetGetWay = IPAddress.Parse(value); }

        [XmlElement("Port")]
        public int Port { get => _port; set => _port = value; }

        [XmlElement("NetworkNo")]
        public int NetworkNo { get => _networkNo; set => _networkNo = value ; }

        [DisplayName("Change IP Address")]
        [XmlIgnore]
        public IPAddress ChangeIPAddress { get => _changeIPAddress; set => _changeIPAddress = value; }

        [DisplayName("Change Sub Net")]
        [XmlIgnore]
        public IPAddress ChangeSubNet { get => _changeSubNet; set => _changeSubNet = value; }

        [DisplayName("Change GateWay")]
        [XmlIgnore]
        public IPAddress ChangeGetWay { get => _changeGetWay; set => _changeGetWay = value; }

        [Browsable(false)]
        [XmlElement("ChangeIPAddress")]
        public string ChangeIPAddressForXML { get => _changeIPAddress.ToString(); set => _changeIPAddress = IPAddress.Parse(value); }

        [Browsable(false)]
        [XmlElement("ChangeSubNet")]
        public string ChangeSubNetForXML { get => _changeSubNet.ToString(); set => _changeSubNet = IPAddress.Parse(value); }

        [Browsable(false)]
        [XmlElement("ChangeGetWay")]
        public string ChangeGetWayForXML { get => _changeGetWay.ToString(); set => _changeGetWay = IPAddress.Parse(value); }


        
    }
}
