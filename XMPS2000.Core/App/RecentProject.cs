using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000.Core.App
{
    public class RecentProject
    {
        private string _projectName;
        private string _ProjectPath;

        public string ProjectName { get => _projectName; set => _projectName = value; }
        public string ProjectPath { get => _ProjectPath; set => _ProjectPath = value; }

        public string DefaultPath { get; set; }

        public RecentProject()
        {
            _projectName = string.Empty;
            _ProjectPath = string.Empty;
        }


    }
}
