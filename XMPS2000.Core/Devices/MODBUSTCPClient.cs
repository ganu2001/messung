using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.Devices
{
    public class MODBUSTCPClient : Device
    {
        public List<MODBUSTCPClient_Slave> Slaves { get; set; }

        public MODBUSTCPClient()
        {
            Slaves = new List<MODBUSTCPClient_Slave>();
        }

        public bool AddSlave(string name, IPAddress serverIPAddress, int port, int polling, int deviceId, int address, int length, string variable, string tag, string functionCode,string multiplicationFactor)
        {
            bool _success = false;
            //string newTagName = (variable.StartsWith("Q") || variable.StartsWith("I")) && !variable.Contains(".") ? "-Select Tag Name-" : tag;
            Slaves.RemoveAll(s => s.Name == name);
            var obj = new MODBUSTCPClient_Slave(name,serverIPAddress,port,polling,deviceId,address,length,variable, tag, functionCode, false, multiplicationFactor);

            Slaves.Add(obj);

            return _success;
        }

    }
}
