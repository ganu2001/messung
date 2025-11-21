using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.MCodeConversion
{
    public class RungOperands
    {
        public string DataType { get; set; }
        public string Oprator { get; set; }
        public List<Operand> Operands { get; set; }
        public int NoOfOperands { get; set; }
        public string T_CName { get; set; }
    }
}
