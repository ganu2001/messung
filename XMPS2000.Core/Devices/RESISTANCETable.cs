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
    public class RESISTANCETable : Device
    {
        public List<RESISTANCETable_Values> Values { get; set; }
        public static List<string> TableNames { get; set; } = new List<string>();

        public int Resistance { get; set; }
        public int Output { get; set; }
        public RESISTANCETable()
        {
            Values = new List<RESISTANCETable_Values>();
            Resistance = 0;
            Output = 0;
        }

        public bool AddValue(string name, int Resistance, int Output)
        {
            bool _success= false;

            //Values.RemoveAll(s => s.Name == name);
            Values.RemoveAll(s => s.Name == name);
            var obj = new RESISTANCETable_Values(name, Resistance, Output);
            Values.Add(obj);
            return _success;
        }

    }
}
