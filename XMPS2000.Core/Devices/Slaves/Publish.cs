using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XMPS2000.Core.Base;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.Devices.Slaves
{
    public class Publish : Device
    {
        private string Topic;
        private string Qos;
        private string RetainFlag;
        private int KeyValue;

        public Publish() 
        {
            PubRequest = new List<PubRequest>();
        }

        [DisplayName("TOPIC")]
        public string topic {  get =>Topic; set =>Topic= value; }

        [DisplayName("OQS")]
        public string qos { get => Qos; set => Qos = value; }

        [DisplayName("RETAINFLAG")]
        public string retainflag {  get => RetainFlag; set => RetainFlag = value; }

        public int keyvalue { get => KeyValue; set => KeyValue = value; }

        public List<PubRequest> PubRequest { get; set; }

        public void AddPublish(string _topic, string _qos, string _retainFlag,int _keyval)
        {
            topic = _topic;
            qos = _qos;
            retainflag = _retainFlag;
            keyvalue = _keyval;
        }
        public void AddPublishRequest(PubRequest pubRequest)
        {
            PubRequest.Add(pubRequest);
        }
    }
}
