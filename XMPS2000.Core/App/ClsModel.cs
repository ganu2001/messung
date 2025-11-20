using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS1000.Core.App
{
    public class ClsModel
    {
        public string ListOfDevices { get; set; }
        public string ModelName { get; set; }
        public string IOType { get; set; }
        public string AnalogInputs { get; set; }
        public string AnalogOutputs { get; set; }
        public string DegitalInputs { get; set; }
        public string DegitalOutputs { get; set; }
        public string PathofMasterFile { get; set; }
        public string PathofImageFile { get; set; }
    }
}
