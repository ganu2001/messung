using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.App;

namespace XMPS2000.Core.Base
{
    public class ProjectTemplate
    {
        private string _templatePath;
        private string _plcName;
        private int _plcId;
        private int _noOfDigitalInput;
        private int _noOfDigitalOutput;
        private int _noOfAnalogInput;
        private int _noOfAnalogOutput;
        private bool _hasEthernet;
        private bool _hasCommunicationPorts;
        private bool _hasExpansionSlots;

        public string TemplatePath { get => _templatePath; set => _templatePath = value; }

        [XmlAttribute("PlcName")]
        public string PlcName { get => _plcName; set => _plcName = value; }
        public int PlcId { get => _plcId; set => _plcId = value; }
        public int NoOfDigitalInput { get => _noOfDigitalInput; set => _noOfDigitalInput = value; }
        public int NoOfDigitalOutput { get => _noOfDigitalOutput; set => _noOfDigitalOutput = value; }
        public int NoOfAnalogInput { get => _noOfAnalogInput; set => _noOfAnalogInput = value; }
        public int NoOfAnalogOutput { get => _noOfAnalogOutput; set => _noOfAnalogOutput = value; }
        public bool HasEthernet { get => _hasEthernet; set => _hasEthernet = value; }
        public bool HasCommunicationPorts { get => _hasCommunicationPorts; set => _hasCommunicationPorts = value; }
        public bool HasExpansionSlots { get => _hasExpansionSlots; set => _hasExpansionSlots = value; }

        public ProjectTemplate()
        {

        }

        public ProjectTemplate(string plcName, string templatePath)
        {
            this._templatePath = templatePath;
            this._plcName = plcName;
        }
    }
}
