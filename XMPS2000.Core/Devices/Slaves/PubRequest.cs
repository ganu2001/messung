using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS2000.Core.Base;

namespace XMPS2000.Core.Devices.Slaves
{
    public class PubRequest : Device
    {
        private int TopicId;
        private string Request;
        private string tagname;
        private int KeyValue;

        public PubRequest()
        {
            tagname = "";
        }
       
        public PubRequest(int topicId, string Keyname, string TagName, int keyValue)
        {
            TopicId = topicId;
            Request = Keyname;
            tagname = TagName;
            KeyValue = keyValue;
        }

        [DisplayName("TOPIC")]
        public int topic { get => TopicId; set => TopicId = value; }

        [DisplayName("REQUESTNAME")]
        public string req { get => Request; set => Request = value; }

        [DisplayName("TagName")]
        public string Tag { get => tagname; set => tagname = value; }
        public int Keyvalue { get => KeyValue; set => KeyValue = value; }

        public int GetTopicId(PubRequest pubRequest)
        {
            return pubRequest.TopicId;
        }

    }
}