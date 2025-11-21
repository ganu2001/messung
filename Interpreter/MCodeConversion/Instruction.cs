using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.MCodeConversion
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
        public string Output1Label { get; set; } = "Output";
        public bool Output1Enabled { get; set; } = true;
        public string Output2Label { get; set; } = "Output 2";
        public bool Output2Enabled { get; set; } = false;
        public List<DataType> SupportedDataTypes { get; private set; } =
           new List<DataType>() {
           new DataType { ID=0x0000, Text="Bool"},
           new DataType { ID=0x0001, Text="Byte"},
           new DataType { ID=0x0002, Text="Word"},
           new DataType { ID=0x0003, Text="Double Word"},
           new DataType { ID=0x0005, Text="Real"},
           new DataType { ID=0x0004, Text="Int"}};

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<Instruction> List =
           new List<Instruction>() {
               // Logical Instructions
               new Instruction {
                   ID=0x0000, Text="AND", InstructionType=0x0000,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} }
               },
               new Instruction {
                   ID=0x0010, Text="OR", InstructionType=0x0000,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} }
               },
               new Instruction {
                   ID=0x0020, Text="XOR", InstructionType=0x0000,
                   Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} }
               },
               new Instruction {
                   ID=0x0030, Text="NOT", InstructionType=0x0000, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} }
               },

               new Instruction {
                   ID=0x0000, Text="&", InstructionType=0x0000,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} }
               },

               // Arithmetic Instructions
               new Instruction { ID=0x0040, Text="+", InstructionType=0x0001,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0050, Text="-", InstructionType=0x0001, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0060, Text="*", InstructionType=0x0001,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0070, Text="/", InstructionType=0x0001, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0080, Text="%", InstructionType=0x0001, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0090, Text="MOV", InstructionType=0x0001, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}}, 

               // Bit Shift
               new Instruction { ID=0x00A0, Text="shl", InstructionType=0x0002, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"} } },
               new Instruction { ID=0x00B0, Text="shr", InstructionType=0x0002, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"} } },
               new Instruction { ID=0x00C0, Text="ror", InstructionType=0x0002, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"} } },
               new Instruction { ID=0x00D0, Text="rol", InstructionType=0x0002, Operand3Enabled = false, Operand4Enabled = false,
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
                   Operand1Label = "Max Value", Operand2Label = "Actual Value", Operand3Label = "Min Value",
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},

               // Compare
               new Instruction { ID=0x0110, Text=">", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0120, Text=">=", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0130, Text="<", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0140, Text="<=", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0150, Text="=", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}},
               new Instruction { ID=0x0160, Text="<>", InstructionType=0x0004, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() {
                       new DataType { ID=0x0000, Text="Bool"},
                       new DataType { ID=0x0001, Text="Byte"},
                       new DataType { ID=0x0002, Text="Word"},
                       new DataType { ID=0x0003, Text="Double Word"},
                       new DataType { ID=0x0005, Text="Real"},
                       new DataType { ID=0x0004, Text="Int"}}}, 
               
               // Edge Detector
               new Instruction { ID=0x0170, Text="Rising Edge", InstructionType=0x0005, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} } },
               new Instruction { ID=0x0180, Text="Falling Edge", InstructionType=0x0005, Operand1Label = "Input",
                   Operand2Enabled = false, Operand3Enabled = false, Operand4Enabled = false,
                   SupportedDataTypes = new List<DataType>() { new DataType { ID=0x0000, Text="Bool"} } },

               // Counter
               new Instruction { ID=0x0190, Text="CTU", InstructionType=0x0006, Operand4Enabled = false,
                   Operand1Label = "Count Up (Bool)", Operand2Label = "Reset (Bool)", Operand3Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Count Value (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0008, Text="CTU"}
                   } },
               new Instruction { ID=0x01A0, Text="CTD", InstructionType=0x0006, Operand4Enabled = false,
                   Operand1Label = "Count Down (Bool)", Operand2Label = "Load (Bool)", Operand3Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Count Value (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0009, Text="CTD"}
                   } },

               // Timer TON
               new Instruction { ID=0x01B0, Text="TON", InstructionType=0x0007,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0006, Text="TON"}
                   } },
               new Instruction { ID=0x01B0, Text="0.01s TON", InstructionType=0x0007,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0006, Text="TON"}
                   } },
               new Instruction { ID=0x01C0, Text="0.1s TON", InstructionType=0x0007,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0006, Text="TON"}
                   } },
               new Instruction { ID=0x01D0, Text="1s TON", InstructionType=0x0007,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0006, Text="TON"}
                   } },
               // Timer TOFF
               new Instruction { ID=0x01E0, Text="TOF", InstructionType=0x0008,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0007, Text="TOFF"}
                   } },
               new Instruction { ID=0x01E0, Text="0.01s TOFF", InstructionType=0x0008,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0007, Text="TOFF"}
                   } },
               new Instruction { ID=0x01F0, Text="0.1s TOFF", InstructionType=0x0008,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0007, Text="TOFF"}
                   } },
               new Instruction { ID=0x0200, Text="1s TOFF", InstructionType=0x0008,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0007, Text="TOFF"}
                   } },
               // Timer TP
               new Instruction { ID=0x0210, Text="TP", InstructionType=0x0009,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000A, Text="TP"}
                   } },
               new Instruction { ID=0x0210, Text="0.01s TP", InstructionType=0x0009,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000A, Text="TP"}
                   } },
               new Instruction { ID=0x0220, Text="0.1s TP", InstructionType=0x0009,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000A, Text="TP"}
                   } },
               new Instruction { ID=0x0230, Text="1s TP", InstructionType=0x0009,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "In (Bool)", Operand2Label = "Preset Value (Word)",
                   Output1Label = "Done", Output2Enabled = true, Output2Label = "Elapsed Time (Word)",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x000A, Text="TP"}
                   } },

               // Flipflop
               new Instruction { ID=0x0240, Text="RS", InstructionType=0x000A,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "SET", Operand2Label = "RESET1",
                   Output1Label = "Done", 
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0000, Text="Bool"}
                   } },
               new Instruction { ID=0x0250, Text="SR", InstructionType=0x000A,
                   Operand3Enabled = false, Operand4Enabled = false,
                   Operand1Label = "SET1", Operand2Label = "RESET",
                   Output1Label = "Done",
                   SupportedDataTypes = new List<DataType>()
                   {
                       new DataType { ID=0x0000, Text="Bool"}
                   } }
           };
    }
}
