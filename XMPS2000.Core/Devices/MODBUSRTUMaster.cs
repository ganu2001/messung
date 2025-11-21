using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.Devices
{
    public class MODBUSRTUMaster : Device
    {
        public List<MODBUSRTUMaster_Slave> Slaves { get; set; }

        public MODBUSRTUMaster()
        {
            Slaves = new List<MODBUSRTUMaster_Slave>();
        }

        public bool AddSlave(string name, int polling, int deviceId, int address, int length, string variable, string tag, string functioCode,string disablingVariables, string multiplicationFactor)
        {
            bool _success= false;

            Slaves.RemoveAll(s => s.Name == name);
            //string newTagName = (variable.StartsWith("Q") || variable.StartsWith("I")) && !variable.Contains(".") ? "-Select Tag Name-" : tag;
            var obj = new MODBUSRTUMaster_Slave(name, polling, deviceId, address, length, variable, tag, functioCode, disablingVariables, false, multiplicationFactor);
            Slaves.Add(obj);
 
            return _success;
        }

    }
}
