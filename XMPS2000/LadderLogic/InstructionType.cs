using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000.LadderLogic
{
    class InstructionType
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<InstructionType> List =
           new List<InstructionType>() {
               new InstructionType { ID=0x0000, Text="Logical"},
               new InstructionType { ID=0x0001, Text="Arithmetic"},
               new InstructionType { ID=0x0002, Text="Bit Shift"},
               new InstructionType { ID=0x0003, Text="Limit"},
               new InstructionType { ID=0x0004, Text="Compare"},
               new InstructionType { ID=0x0005, Text="Edge Detector"},
               new InstructionType { ID=0x0006, Text="Counter"},
               new InstructionType { ID=0x0007, Text="Timer TON"},
               new InstructionType { ID=0x0008, Text="Timer TOFF"},
               new InstructionType { ID=0x0009, Text="Timer TP"},
               new InstructionType { ID=0x000A, Text="Flipflop"},
               new InstructionType { ID=0x000B, Text ="Limit Alarm"},
               new InstructionType { ID=0x000C, Text ="Swap"},
               new InstructionType { ID=0x000D,Text="Data Conversion"},          //Id Here is given Random accoeding to Hex Number
               new InstructionType {ID=0x000E,Text="Scale"},
               new InstructionType {ID=0x000F,Text="Timer RTON"},                //Retentive Timer
               new InstructionType{ID=0x0010,Text="Pack"},                       //Adding new Instruction Pack.
               new InstructionType{ID=0x0011,Text="UnPack"},                     //Adding new Instruction UnPack
               new InstructionType{ID=0x0012,Text="MQTT"},                     //Adding new Instruction MQTT
           };
    }
}
