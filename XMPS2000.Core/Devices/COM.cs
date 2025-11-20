using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.Base;

namespace XMPS2000.Core.Devices
{
    public class COMDevice : Device
    {
        private int _baudRate;
        private int _dataLength;
        private int _stopBit;
        private string _parity;
        private int _sendDelay;
        private decimal _minimumInterface;
        private int _communicationTimeout;
        private int _numberOfRetries;

        [DisplayName("Baud Rate")]
        [XmlElement("BaudRate")]
        public int BaudRate { get => _baudRate; set => _baudRate = value; }

        [DisplayName("Data Length")]
        [XmlElement("DataLength")]
        public int DataLength { get => _dataLength; set => _dataLength = value; }

        [DisplayName("Stop Bit")]
        [XmlElement("StopBit")]
        public int StopBit { get => _stopBit; set => _stopBit = value; }

        [XmlElement("Parity")]
        public string Parity { get => _parity; set => _parity = value; }

        [DisplayName("Send Delay")]
        [XmlElement("SendDelay")]
        public int SendDelay { get => _sendDelay; set => _sendDelay = value; }

        [DisplayName("Minimum Interface")]
        [XmlElement("MinimumInterface")]
        public decimal MinimumInterface { get => _minimumInterface; set => _minimumInterface = value; }
        [DisplayName("Communication Timeout")]
        [XmlElement("CommunicationTimeout")]
        public int CommunicationTimeout { get => _communicationTimeout; set => _communicationTimeout = value; }

        [DisplayName("Number Of Retries")]
        [XmlElement("NumberOfRetries")]
        public int NumberOfRetries { get => _numberOfRetries; set => _numberOfRetries = value; }



    }
}
