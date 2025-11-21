using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMPS2000.Core.Devices
{
    public class MODBUSTCPServer : Device
    {
        public List<MODBUSTCPServer_Request> Requests { get; set; }

        public MODBUSTCPServer()
        {
            Requests = new List<MODBUSTCPServer_Request>();
        }

        public bool AddRequest(string name,int address, int length, int port, string variable,int deviceId,string Tag,string functionCode)
        {
            bool _success = false;
            //string newTagName = (variable.StartsWith("Q") || variable.StartsWith("I")) && !variable.Contains(".") ? "-Select Tag Name-" : Tag;
            Requests.RemoveAll(r => r.Name == name);
            //Requests.RemoveAll(r => r.DeviceId == deviceId);
            var obj = new MODBUSTCPServer_Request(name,address,length,port,variable, Tag, functionCode);

            Requests.Add(obj);

            return _success;
        }

    }
}
