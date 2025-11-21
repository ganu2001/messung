using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMPS2000.Core.Base;

namespace XMPS2000.Core.App
{
    public class ProjectTemplates
    {
        private List<ProjectTemplate> _templates;

        public ProjectTemplates()
        {
            _templates = new List<ProjectTemplate>();
        }

        public List<ProjectTemplate> Templates { get => _templates; set => _templates = value; }
    }
}
