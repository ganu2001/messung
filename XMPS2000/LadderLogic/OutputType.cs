using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000.LadderLogic
{
    class OutputType
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<OutputType> List =
            new List<OutputType>() {
               new OutputType { ID=0x0000, Text="On-board"},
               new OutputType { ID=0x0001, Text="Remote"},
               new OutputType { ID=0x0002, Text="Memory Address Variable"},
               new OutputType { ID=0x0003, Text="Expansion"}
            };
    }
}
