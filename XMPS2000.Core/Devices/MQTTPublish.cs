using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.Core.Devices
{
    public class MQTTPublish : Device
    {
        //int _topic;
        public List<MQTTPublish_Blocks> MQTTPublish_Blocks { get; set; }
     

        public MQTTPublish()
        {
            MQTTPublish_Blocks = new List<MQTTPublish_Blocks>();
        }

        public bool AddBlocks()
        {
            return true;
        }
    }
}
