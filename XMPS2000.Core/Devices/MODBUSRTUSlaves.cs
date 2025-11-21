using System.Collections.Generic;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.Core.Devices
{
    public class MODBUSRTUSlaves : Device 
    {
        public List<MODBUSRTUSlaves_Slave> Slaves { get; set; }

        public MODBUSRTUSlaves()
        {
            Slaves = new List<MODBUSRTUSlaves_Slave>();
        }

        public bool AddSlave(string name,int address, int length, string variable, string tag, string functioCode)
        {
            bool _success = false;

            Slaves.RemoveAll(s => s.Name == name);
            var obj = new MODBUSRTUSlaves_Slave(name,address, length, variable, tag, functioCode);
            Slaves.Add(obj);

            return _success;
            
        }
    }
}
