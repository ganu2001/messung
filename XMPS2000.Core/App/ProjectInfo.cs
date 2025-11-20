using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000.Core.App
{
    public class ProjectInfo
    {
        private string _plcModel;
        private string _ProjectPath;

        public string PLCModel { get => _plcModel; set => _plcModel = value; }
        public string ProjectPath { get => _ProjectPath; set => _ProjectPath = value; }
    }
}
