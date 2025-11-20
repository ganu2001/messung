using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInterpreter
{
    public class Rung : RungOperands
    {
        public string Exp { get; set; }
        public int RungNo { get; set; }
        public string Datatype { get; set; }
        public bool EnableType { get; set; }
        public string? Enable { get; set; }
        public string? T_CName { get; set; }
        public string Output1 { get; set; }
        public string? Output2 { get; set; }

    }
}
