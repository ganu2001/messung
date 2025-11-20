using System.ComponentModel;
using System.Xml.Serialization;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.Core.Base
{
    [XmlInclude(typeof(COMDevice))]
    [XmlInclude(typeof(Ethernet))]
    [XmlInclude(typeof(MODBUSRTUMaster))]
    [XmlInclude(typeof(MODBUSTCPClient))]
    [XmlInclude(typeof(MODBUSTCPServer))]
    [XmlInclude(typeof(MODBUSRTUSlaves))]
    [XmlInclude(typeof(MQTTForm))]
    //[XmlInclude(typeof(MQTTPublish))]
    [XmlInclude(typeof(Publish))]
    [XmlInclude(typeof(Subscribe))]

    public class Device
    {
        private string _name;
        private string _type;

        [Browsable(false)]
        [XmlElement("Name")]
        public string Name { get => _name; set => _name = value; }

        [Browsable(false)]
        public string Type { get => _type; set => _type = value; }
    }
}