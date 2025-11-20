using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS2000.Core.Base;

namespace XMPS2000.Core.Devices.Slaves
{
    public class Subscribe:Device
    {
        private string Topic;
        private string Qos;
        private int Key;
        public List<SubscribeRequest> requests;

        public Subscribe()
        {
            SubRequest = new List<SubscribeRequest>();
        }

        [DisplayName("TOPIC")]
        public string topic { get => Topic; set => Topic = value; }

        [DisplayName("OQS")]
        public string qos { get => Qos; set => Qos = value; }

        public int key { get => Key; set => Key = value; }

        public List<SubscribeRequest> SubRequest { get; set; }

        public void AddSubscribe(string _topic, string _qos,int _key)
        {
            topic = _topic;
            qos = _qos;
            key = _key;
        }
        public void AddPublishRequest(SubscribeRequest subRequest)
        {
            SubRequest.Add(subRequest);
        }
    }

    public class SubscribeRequest
    {
        private int Topic;
        private string Keyname;
        private string Variable;
        private int Key;

        public SubscribeRequest()
        {

        }
        [DisplayName("TOPIC")]
        public int topic { get => Topic; set => Topic = value; }

        [DisplayName("KEYNAME")]
        public string req { get => Keyname; set => Keyname = value; }

        [DisplayName("VARIABLE")]
        public string Tag { get => Variable; set => Variable = value; }
        public int key { get => Key; set => Key= value; }

        public int GetTopicId(SubscribeRequest subscribe)
        {
            return subscribe.Topic;
        }

    }

}
