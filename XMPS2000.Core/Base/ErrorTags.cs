using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using XMPS2000.Core.LadderLogic;
using XMPS2000.Core.Devices;

namespace XMPS2000.Core.Base
{
    public class ErrorTags
    {

    }

    [XmlRoot("SystemConfiguration")]
    public class SystemConfiguration
    {
        [XmlElement("DeviceModel")]
        public List<DeviceModel> Devices { get; set; }
    }

    public class DeviceModel
    {
        [XmlAttribute("Type")]
        public string DeviceType { get; set; }

        [XmlElement("Template")]
        public List<Template> Templates { get; set; }
    }

    public class Template
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Ethernet")]
        public Communication Ethernet { get; set; }

        [XmlElement("RS485")]
        public Communication RS485 { get; set; }
    }

    public class Communication
    {
        [XmlElement("TreeNodeText")]
        public List<TreeNodeText> TreeNodes { get; set; }
    }

    public class TreeNodeText
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Device")]
        public List<DeviceDetials> Devices { get; set; }
    }

    public class DeviceDetials
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("maxCount")]
        public int MaxCount { get; set; }
        [XmlAttribute("currentCount")]
        public int CurrentCount { get; set; }

        [XmlElement("Property")]
        public List<Property> Properties { get; set; }
    }

    public class Property
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("maxCount")]
        public int MaxCount { get; set; }
        [XmlAttribute("currentCount")]
        public int CurrentCount { get; set; }


    }
}