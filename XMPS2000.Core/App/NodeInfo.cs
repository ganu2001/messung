using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS2000.Core.Types;

namespace XMPS2000.Core.App
{
    public class NodeInfo
    {
        private NodeType _nodeType;
        private string _info;

        public NodeType NodeType { get => _nodeType; set => _nodeType = value; }
        public string Info { get => _info; set => _info = value; }
    }
}
