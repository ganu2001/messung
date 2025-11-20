using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.ConfigConversion
{
    public class ModbusFunctionCode
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<ModbusFunctionCode> List =
            new List<ModbusFunctionCode>()
            {
                new ModbusFunctionCode { ID = 0x01, Text = "Coil (01)" },
                new ModbusFunctionCode { ID = 0x02, Text = " Discrete Input (02)" },
                new ModbusFunctionCode { ID = 0x03, Text = " Holding Registers (03)" },
                new ModbusFunctionCode { ID = 0x04, Text = " Input Registers (04)" },
                new ModbusFunctionCode { ID = 0x05, Text = " Single Coil (05)" },
                new ModbusFunctionCode { ID = 0x06, Text = " Single Holding Register (06)" },
                new ModbusFunctionCode { ID = 0x0F, Text = " Multiple Coils (15)" },
                new ModbusFunctionCode { ID = 0x10, Text = " Multiple Holding Registers (16)" }
            };
    }
}
