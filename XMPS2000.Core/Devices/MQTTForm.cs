using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using XMPS2000.Core.Base;

namespace XMPS2000.Core.Devices
{
    public class MQTTForm : Device
    {
        private string _hostName;
        private string _clientId;
        private int _port;
        private string _username;
        private string _password;
        private string _ca3Certificate;
        private string _clientCertificate;
        private string _license;
        private string _clientKey;
        private string _cleanSession;
        private int _KeepAlive;

        //public List<MQTTForm> Mqtt { get; set; }

        public MQTTForm()
        {
            //Mqtt = new List<MQTTForm>();
            //_hostName = "0";
            //_port = 0;
            //_username = "0";
            //_password = "0";
            //_ca3Certificate = "0";
            //_clientCertificate = "0";
            //_clientKey = "0";
            //_cleanSession = "0";
            //_KeepAlive = 0;
        }
        [DisplayName("Hostname")]
        
        public string hostname { get => _hostName; set => _hostName = value; }
        [DisplayName("Client Id")]

        public string clientId { get => _clientId; set => _clientId = value; }

        [DisplayName("Port")]
        public int port { get => _port; set => _port = value; }

        [DisplayName("Username")]
        public string username { get => _username; set => _username = value; }

        [DisplayName("Password")]
        public string password { get => _password; set => _password = value; }

        [DisplayName("CA3 Certificate")]
        public string ca3certificate { get => _ca3Certificate; set => _ca3Certificate = value; }

        [DisplayName("Client Certificate")]
        public string clientCertificate { get => _clientCertificate; set => _clientCertificate = value; }

        [DisplayName("License")]
        public string license { get => _license; set => _license = value; }
        [DisplayName("Client Key")]
        public string clientKey { get => _clientKey; set => _clientKey = value; }

        [DisplayName("Clean Session")]
        public string cleanSession { get => _cleanSession; set => _cleanSession = value; }

        [DisplayName("Keep Alive")]
        public int keepAlive { get => _KeepAlive; set => _KeepAlive = value; }

    }
}
