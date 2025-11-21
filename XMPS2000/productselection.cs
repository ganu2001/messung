using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000
{
    class productselection
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public int productselections { get; set; }
        public override string ToString()
        {
            return Text;
        }

        public static readonly List<productselection> List =
           new List<productselection>() {
               new productselection { ID=0x0000, Text="Logical"},
               new productselection { ID=0x0001, Text="Arithmetic"},
               new productselection { ID=0x0002, Text="Bit Shift"},
               new productselection { ID=0x0003, Text="Limit"},
               new productselection { ID=0x0004, Text="Compare"},
               new productselection { ID=0x0005, Text="Edge Detector"},
               new productselection { ID=0x0006, Text="Counter"},
               new productselection { ID=0x0007, Text="Timer TON"},
               new productselection { ID=0x0008, Text="Timer TOFF"},
               new productselection { ID=0x0009, Text="Timer TP"},
               new productselection { ID=0x000A, Text="Flipflop"}
           };
    }
}


