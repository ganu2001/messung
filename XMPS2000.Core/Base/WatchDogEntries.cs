using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000
{
    public class WatchDogEntries
    {
        public string SrNo { get; set; }
        public string Address { get; set; }
        public string Tag { get; set; }
        public string DataType { get; set; }
        public string Retentive { get; set; }
        public string ActualValue { get; set; }
        public string PreparedValue { get; set; }
        public string BtnForce { get; set; }
        public string UnForceValue { get; set; }
        public string HiddenActualValue { get; set; }
    }
}
