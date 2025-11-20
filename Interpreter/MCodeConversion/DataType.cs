using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.MCodeConversion
{
    class DataType
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<DataType> List =
           new List<DataType>() {
           new DataType { ID=0x00, Text="Bool"},
           new DataType { ID=0x01, Text="Byte"},
           new DataType { ID=0x02, Text="Word"},
           new DataType { ID=0x03, Text="Double Word"},
           new DataType { ID=0x04, Text="Int"},
           new DataType { ID=0x05, Text="Real"},
           new DataType { ID=0x06, Text="TON"},
           new DataType { ID=0x07, Text="TOF"},
           new DataType { ID=0x08, Text="CTU"},
           new DataType { ID=0x09, Text="CTD"},
           new DataType { ID=0x0A, Text="TP"}};
    }
}
