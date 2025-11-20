using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000.LadderLogic
{
    class Instruction
    {
        // Instruction Code
        public int ID { get; set; }
        // Instruction Text Name
        public string Text { get; set; }
        // Instruction Type Code
        public int InstructionType { get; set; }

        public string Operand1Label { get; set; } = "Input 1";
        public bool Operand1Enabled { get; set; } = true;
        public string Operand2Label { get; set; } = "Input 2";
        public bool Operand2Enabled { get; set; } = true;
        public string Operand3Label { get; set; } = "Input 3";
        public bool Operand3Enabled { get; set; } = true;
        public string Operand4Label { get; set; } = "Input 4";
        public bool Operand4Enabled { get; set; } = true;

        public string Operand5LAbel { get; set; } = "Input 5";

        public bool Operand5Enabled { get; set; } = false;
        public string Output1Label { get; set; } = "Output";
        public bool Output1Enabled { get; set; } = true;
        public string Output2Label { get; set; } = "Output 2";
        public bool Output2Enabled { get; set; } = false;


        public string Output3Label { get; set; } = "Output 3";

        public bool Output3Enabled { get; set; } = false;

        public bool TopicEnabled { get; set; } = false;

        //Adding third output and 1 input here
        public List<DataType> SupportedDataTypes { get; private set; } =
           new List<DataType>() {
           new DataType { ID=0x0000, Text="Bool"},
           new DataType { ID=0x0001, Text="Byte"},
           new DataType { ID=0x0002, Text="Word"},
           new DataType { ID=0x0003, Text="Double Word"},
           new DataType { ID=0x0005, Text="Real"},
           new DataType { ID=0x0004, Text="Int"}};

        public List<DataType> OutputDataTypes { get; private set; } =
           new List<DataType>() { };

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<Instruction> List =
           new List<Instruction>() {
               // Logical Instructions
               new Instruction {
                   ID=0x0000, Text="AND", InstructionType=0x0000,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} }
               },
               new Instruction {
                   ID=0x0010, Text="OR", InstructionType=0x0000,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} }
               },
               new Instruction {
                   ID=0x0020, Text="XOR", InstructionType=0x0000,
                   Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} }
               },
               new Instruction {
                   ID=0x0030, Text="NOT", InstructionType=0x0000, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} }
               },

               // Arithmetic Instructions
               new Instruction { ID=0x0040, Text="ADD", InstructionType=0x0001,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}}},
               new Instruction { ID=0x0050, Text="SUB", InstructionType=0x0001, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}}},
               new Instruction { ID=0x0060, Text="MUL", InstructionType=0x0001,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}}},
               new Instruction { ID=0x0070, Text="DIV", InstructionType=0x0001, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}}},
               new Instruction { ID=0x0080, Text="MOD", InstructionType=0x0001, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}}},
               new Instruction { ID=0x0090, Text="MOV", InstructionType=0x0001, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}}}, 

               //Exponential
               new Instruction { ID =0x0380,Text = "EXP",InstructionType=0x0001,Operand1Label = "Input1",Operand2Enabled = false,Operand3Enabled = false,Operand4Enabled = false,Output1Label = "Text (Output)",Output2Enabled =false,
                   SupportedDataTypes = new List<DataType>() {
                     //  new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}}},

               // Bit Shift
               new Instruction { ID=0x00A0, Text="SHL", InstructionType=0x0002, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"} } },
               new Instruction { ID=0x00B0, Text="SHR", InstructionType=0x0002, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"} } },
               new Instruction { ID=0x00C0, Text="ROR", InstructionType=0x0002, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"} } },
               new Instruction { ID=0x00D0, Text="ROL", InstructionType=0x0002, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"} } },

               // Limit
               //Remove Limit Types Max and Min as they are not required anymore as per communication with Sagar on dt 30 Aug 2021
               //new Instruction { ID=0x00E0, Text="MAX", InstructionType=0x0003, Operand4Enabled = false,
               //    Operand1Label = "Max value", Operand2Label = "Actual value", Operand3Label = "Min value",
               //    SupportedDataTypes = new List<DataType>() {
               //        new DataType { ID=0x0001, Text="Byte"},
               //        new DataType { ID=0x0002, Text="Word"},
               //        new DataType { ID=0x0003, Text="Double Word"},
               //        new DataType { ID=0x0005, Text="Real"},
               //        new DataType { ID=0x0004, Text="Int"}}},
               //new Instruction { ID=0x00F0, Text="MIN", InstructionType=0x0003, Operand4Enabled = false,
               //    Operand1Label = "Max value", Operand2Label = "Actual value", Operand3Label = "Min value",
               //    SupportedDataTypes = new List<DataType>() {
               //        new DataType { ID=0x0001, Text="Byte"},
               //        new DataType { ID=0x0002, Text="Word"},
               //        new DataType { ID=0x0003, Text="Double Word"},
               //        new DataType { ID=0x0005, Text="Real"},
               //        new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0100, Text="LIMIT", InstructionType=0x0003, Operand4Enabled = false,
                   Operand1Label = "Max Value", Operand2Label = "Actual Value", Operand3Label = "Min Value",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},

               // Compare
               new Instruction { ID=0x0110, Text="GT", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}},

                   OutputDataTypes = new List<DataType>()
                   {
                       new DataType{ID=0x0000,Text="Bool"}
                   }
               },
               new Instruction { ID=0x0120, Text="GE", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}},

                   OutputDataTypes = new List<DataType>()
                   {
                       new DataType{ID=0x0000,Text="Bool"}
                   }
               },
               new Instruction { ID=0x0130, Text="LT", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}},

                   OutputDataTypes = new List<DataType>()
                   {
                       new DataType{ID=0x0000,Text="Bool"}
                   }
               },
               new Instruction { ID=0x0140, Text="LE", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}},

                   OutputDataTypes = new List<DataType>()
                   {
                       new DataType{ID=0x0000,Text="Bool"}
                   }
               },
               new Instruction { ID=0x0150, Text="EQ", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}},

                   OutputDataTypes = new List<DataType>()
                   {
                       new DataType{ID=0x0000,Text="Bool"}
                   }
               },
               new Instruction { ID=0x0160, Text="NE", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x000C, Text="DINT"},
                       new DataType { ID=0x000D, Text="UDINT"}},

                   OutputDataTypes = new List<DataType>()
                   {
                       new DataType{ID=0x0000,Text="Bool"}
                   }
               }, 
               
               // Edge Detector
               new Instruction { ID=0x0170, Text="Rising Edge", InstructionType=0x0005, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} } },
               new Instruction { ID=0x0180, Text="Falling Edge", InstructionType=0x0005, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} } },

               // Counter
               new Instruction { ID=0x0190, Text="CTU", InstructionType=0x0006, Operand4Enabled = false,
                   Operand1Label = "Count Up (Bool)", Operand2Label = "Reset (Bool)", Operand3Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Count Value (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0008, Text="CTU"}
                   } },
               new Instruction { ID=0x01A0, Text="CTD", InstructionType=0x0006, Operand4Enabled = false,
                   Operand1Label = "Count Down (Bool)", Operand2Label = "Load (Bool)", Operand3Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Count Value (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0009, Text="CTD"}
                   } },

               // Timer TON
               new Instruction { ID=0x01B0, Text="0.01s TON", InstructionType=0x0007,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0006, Text="TON"}
                   } },
               new Instruction { ID=0x01C0, Text="0.1s TON", InstructionType=0x0007,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0006, Text="TON"}
                   } },
               new Instruction { ID=0x01D0, Text="1s TON", InstructionType=0x0007,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0006, Text="TON"}
                   } },
               // Timer TOFF
               new Instruction { ID=0x01E0, Text="0.01s TOFF", InstructionType=0x0008,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0007, Text="TOFF"}
                   } },
               new Instruction { ID=0x01F0, Text="0.1s TOFF", InstructionType=0x0008,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0007, Text="TOFF"}
                   } },
               new Instruction { ID=0x0200, Text="1s TOFF", InstructionType=0x0008,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0007, Text="TOFF"}
                   } },
               // Timer TP
               new Instruction { ID=0x0210, Text="0.01s TP", InstructionType=0x0009,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000A, Text="TP"}
                   } },
               new Instruction { ID=0x0220, Text="0.1s TP", InstructionType=0x0009,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000A, Text="TP"}
                   } },
               new Instruction { ID=0x0230, Text="1s TP", InstructionType=0x0009,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000A, Text="TP"}
                   } },

               // Flipflop
               new Instruction { ID=0x0240, Text="RS", InstructionType=0x000A,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "SET", Operand2Label = "RESET1",
                   Output1Label = "Done",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0000, Text="Bool"}
                   } },
               new Instruction { ID=0x0250, Text="SR", InstructionType=0x000A,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "SET1", Operand2Label = "RESET",
                   Output1Label = "Done",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0000, Text="Bool"}
                   } },

               //Limit Alarm  ----> Id  & InstructionType Given here is not according to Doc
               new Instruction{ ID=0x00E0, Text="Limit Alarm - O",InstructionType=0x000B, //---> Swap type
               Operand1Label = "Max Value" , Operand2Label =  "Actual Value", Operand3Label = "Min Value" ,
                   Operand4Enabled = false,Output1Label = "Over Limit", Output2Label = "In Limit" ,Output1Enabled= true,Output2Enabled = true,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0000, Text="Bool"},                  //Opcode
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x0005, Text="Real"}

                   },
                   //For Output Datatypes
                   OutputDataTypes = new List<DataType>()
                   {
                       new DataType {ID=0x000E, Text="Bool"}
                   },


               },

                new Instruction{ ID=0x00F0, Text="Limit Alarm - U",InstructionType=0x000B,
               Operand1Label = "Max Value" , Operand2Label =  "Actual Value", Operand3Label = "Min Value" ,
                   Operand4Enabled = false,Output1Label = "Under Limit", Output2Label = "In Limit",Output1Enabled= true,Output2Enabled = true,Operand5Enabled = false,Output3Enabled = false,
                SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0000, Text="Bool"},                  //Opcode
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0004, Text="Int"},
                       new DataType { ID=0x0005, Text="Real"}
                   },
                     OutputDataTypes = new List<DataType>()
                     {
                       new DataType {ID=0x0001, Text="Bool"}
                     },
                },


                //Swap
                new Instruction{ID=0x0260, Text="SWAP CDAB",InstructionType=0x000C,
                  Operand1Label = "Input" , Operand2Enabled=false, Operand3Enabled=false,
                   Operand4Enabled = false,Output1Label="Output",Output2Enabled=false,Operand5Enabled = false,Output3Enabled = false,
                SupportedDataTypes = new List<DataType>()
               {
                   new DataType {ID=0x0003, Text="Double Word"},              //Predefined Id
                   new DataType {ID=0x0005, Text="Real"}
               },

                },

                new Instruction{ID=0x0270, Text="SWAP BADC",InstructionType=0x000C,
                  Operand1Label = "Input" , Operand2Enabled=false, Operand3Enabled=false,
                   Operand4Enabled = false,Output1Label="Output",Output2Enabled=false,Operand5Enabled = false,Output3Enabled = false,
                SupportedDataTypes = new List<DataType>()
               {
                   new DataType {ID=0x0003, Text="Double Word"},
                   new DataType {ID=0x0005, Text="Real"}
               },
                },

                new Instruction{ID=0x0280, Text="SWAP DCBA",InstructionType=0x000C,
                  Operand1Label = "Input" , Operand2Enabled=false, Operand3Enabled=false,
                  Operand4Enabled = false,Output1Label="Output",Output2Enabled=false,Operand5Enabled = false,Output3Enabled = false,
                SupportedDataTypes = new List<DataType>()
               {
                   new DataType {ID=0x0003, Text="Double Word"},
                   new DataType {ID=0x0005, Text="Real"}
               },
                OutputDataTypes= new List<DataType>()
                  {

                  }
                },
                                //Data Conversion  1.1
                new Instruction{ID=0x02000,Text="ANY to BOOL",InstructionType=0x000D,
                 Operand1Label = "Input",Operand2Enabled = false,Operand3Enabled = false,Operand4Enabled = false,
                 Output1Label = "Output",Output2Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                  SupportedDataTypes = new List<DataType>()
                  {

                      new DataType {ID=0xA,Text="Bool"},
                      new DataType {ID=0xB,Text="Byte"},
                      new DataType {ID=0xC,Text="Word"},
                      new DataType {ID=0xE,Text="Int"},             // Changed The ID for Dword & Int For Generating Opcode as Per Messung Requirement
                      new DataType {ID=0xD,Text="Double Word"},
                      new DataType {ID=0xF,Text="Real"},
                  },
                  OutputDataTypes= new List<DataType>()
                  {
                      new DataType{ID=0x02B0,Text="Bool"}
                  }
                },
                //1.2
                new Instruction{ID=0x02001,Text="ANY to BYTE",InstructionType=0x000D,
                 Operand1Label = "Input",Operand2Enabled = false,Operand3Enabled = false,Operand4Enabled = false,
                 Output1Label = "Output ",Output2Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                  SupportedDataTypes = new List<DataType>()
                  {

                      new DataType {ID=0xA,Text="Bool"},
                      new DataType {ID=0xB,Text="Byte"},
                      new DataType {ID=0xC,Text="Word"},
                      new DataType {ID=0xE,Text="Int"},
                      new DataType {ID=0xD,Text="Double Word"},
                      new DataType {ID=0xF,Text="Real"},
                  },
                  OutputDataTypes= new List<DataType>()
                  {
                      new DataType{ID=0x02B1,Text="Byte"}
                  }
                },
                //1.3
                new Instruction{ID=0x02002,Text="ANY to Word",InstructionType=0x000D,
                 Operand1Label = "Input",Operand2Enabled = false,Operand3Enabled = false,Operand4Enabled = false,
                 Output1Label = "Output ",Output2Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                  SupportedDataTypes = new List<DataType>()
                  {

                      new DataType {ID=0xA,Text="Bool"},
                      new DataType {ID=0xB,Text="Byte"},
                      new DataType {ID=0xC,Text="Word"},
                      new DataType {ID=0xE,Text="Int"},
                      new DataType {ID=0xD,Text="Double Word"},
                      new DataType {ID=0xF,Text="Real"},
                  },
                  OutputDataTypes= new List<DataType>()
                  {
                      new DataType{ID=0x02C2,Text="Word"}
                  }
                },
                //1.4
                new Instruction{ID=0x02003,Text="ANY to Dword",InstructionType=0x000D,
                 Operand1Label = "Input",Operand2Enabled = false,Operand3Enabled = false,Operand4Enabled = false,
                 Output1Label = "Output ",Output2Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                  SupportedDataTypes = new List<DataType>()
                  {

                      new DataType {ID=0xA,Text="Bool"},
                      new DataType {ID=0xB,Text="Byte"},
                      new DataType {ID=0xC,Text="Word"},
                      new DataType {ID=0xE,Text="Int"},
                      new DataType {ID=0xD,Text="Double Word"},
                      new DataType {ID=0xF,Text="Real"},
                  },
                  OutputDataTypes= new List<DataType>()
                  {
                      new DataType{ID=0x02D3,Text="Double Word"},
                  }
                },
                //1.5
                new Instruction{ID=0x02004,Text="ANY to INT",InstructionType=0x000D,
                 Operand1Label = "Input",Operand2Enabled = false,Operand3Enabled = false,Operand4Enabled = false,
                 Output1Label = "Output ",Output2Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                  SupportedDataTypes = new List<DataType>()
                  {

                      new DataType {ID=0xA,Text="Bool"},
                      new DataType {ID=0xB,Text="Byte"},
                      new DataType {ID=0xC,Text="Word"},
                      new DataType {ID=0xE,Text="Int"},
                      new DataType {ID=0xD,Text="Double Word"},
                      new DataType {ID=0xF,Text="Real"},
                  },
                  OutputDataTypes= new List<DataType>()
                  {
                      new DataType{ID=0x02C4,Text="Int"}
                  }
                },
                //1.6
                new Instruction{ID=0x02005,Text="ANY to REAL",InstructionType=0x000D,
                 Operand1Label = "Input",Operand2Enabled = false,Operand3Enabled = false,Operand4Enabled = false,
                 Output1Label = "Output ",Output2Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                  SupportedDataTypes = new List<DataType>()
                  {

                      new DataType {ID=0xA,Text="Bool"},
                      new DataType {ID=0xB,Text="Byte"},
                      new DataType {ID=0xC,Text="Word"},
                      new DataType {ID=0xE,Text="Int"},
                      new DataType {ID=0xD,Text="Double Word"},
                      new DataType {ID=0xF,Text="Real"},
                  },
                  OutputDataTypes= new List<DataType>()
                  {
                      new DataType {ID=0x02D5,Text="Real"}
                  }
                },

                new Instruction{ID=0x0290,Text="Scale",InstructionType=0x000E,
                  Operand1Label = "IN",Operand2Label = "IN MIN",Operand3Label = "IN MAX",Operand4Label = "OUT MIN",
                 Operand5LAbel = "OUT MAX",Output1Label = "IN Min Error",Output2Label = "Output",Output3Label = "IN Max Error",Output3Enabled = true,Operand5Enabled=true, Output2Enabled = true,
                 SupportedDataTypes= new List<DataType>()
                 {
                     new DataType {ID=0x0005,Text="Real"}
                 },
                 OutputDataTypes= new List<DataType>()
                  {
                      new DataType {ID=0x0001,Text="Bool"}
                  }
                },


                  // Timer RTON
                new Instruction { ID=0X0340, Text="0.01s RTON", InstructionType=0x000F,
                   Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Reset(Bool)", Operand3Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000B, Text="RTON"}
                   } },

                new Instruction { ID=0X0350, Text="0.1s RTON", InstructionType=0x000F,
                   Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Reset(Bool)", Operand3Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000B, Text="RTON"}
                   } },

                new Instruction { ID=0X0360, Text="1s RTON", InstructionType=0x000F,
                    Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Reset(Bool)", Operand3Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000B, Text="RTON"}
                   } },
                 new Instruction { ID=0X0370, Text="1m RTON", InstructionType=0x000F,
                   Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Reset(Bool)", Operand3Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000B, Text="RTON"}
                   } },

                 //for new Instruction --Pack--
                 new Instruction { ID=0x0390, Text="Pack", InstructionType=0x0010, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                   }},
                 //For new Instruction --Un-Pack--
                  new Instruction { ID=0x03A0, Text="UnPack", InstructionType=0x0011, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0002, Text="Word"},
                   }},

                  //For new Instruction --MQTT FB--
                  new Instruction { ID=0x03B0, Text="MQTT Publish", InstructionType=0x0012, Operand1Label = "Input",Operand1Enabled = false,
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,TopicEnabled = true,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0002, Text="Bool"},
                   }},
                  //For new Instruction --MQTT FB--
                  new Instruction { ID=0x03C0, Text="MQTT Subscribe", InstructionType=0x0012, Operand1Label = "Input",Operand1Enabled = false,
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,Operand5Enabled = false,Output3Enabled = false,TopicEnabled = true,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0002, Text="Bool"},
                   }},
           };
    }
}