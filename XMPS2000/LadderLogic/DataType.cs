using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000.LadderLogic
{
    public class DataType
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<DataType> List =
           new List<DataType>() {
           new DataType { ID=0x0000, Text="Bool"},
           new DataType { ID=0x0001, Text="Byte"},
           new DataType { ID=0x0002, Text="Word"},
           new DataType { ID=0x0003, Text="Double Word"},
           new DataType { ID=0x0004, Text="Int"},
           new DataType { ID=0x0005, Text="Real"},
           new DataType { ID=0x0006, Text="TON"},
           new DataType { ID=0x0007, Text="TOFF"},
           new DataType { ID=0x0008, Text="CTU"},
           new DataType { ID=0x0009, Text="CTD"},
           new DataType { ID=0x000A, Text="TP"},
           new DataType { ID=0x000B, Text="RTON"},
          /* new DataType { ID=0x000C, Text="Pack" },*///Added new DataType for the Pack Instruction
           new DataType { ID=0x000C, Text="DINT"},//New DataType DINT.
           new DataType { ID=0x000D, Text="UDINT"},//New DataType UDINT.
           new DataType { ID=0x000E, Text="MQTT"}//New DataType MQTT  
    };
        public static bool ContainsText(string text)
        {
            return List.Any(dt => dt.Text == text);
        }



    }
}
