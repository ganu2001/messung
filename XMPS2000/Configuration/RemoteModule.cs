using System.Collections.Generic;
using System.ComponentModel;

namespace XMPS2000
{
    public class Mode
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<Mode> List =
        new List<Mode>()
        {
                new Mode { ID = 1, Text = "0 to 10V" },
                new Mode { ID = 2, Text = "0 to 20mA" },
                new Mode { ID = 3, Text = "4 to 20mA" },
                new Mode { ID = 4, Text = "" },
        };
    }
    public class RemoteModule
    {
        public string Name { get; private set; }
        public string IOType { get; private set; } = "Remote I/O";
        public int StartingPosition { get; private set; } = 0;
        public List<TypeAndIO> SupportedTypesAndIOs { get; private set; }
        public override string ToString()
        {
            return Name;
        }

        

        public class ModuleType
        {
            public const string DigitalInput = "Digital Input";
            public const string DigitalOutput = "Digital Output";
            public const string AnalogInput = "Analog Input";
            public const string AnalogOutput = "Analog Output";
            public const string Other = "Other";
        }
        public class TypeAndIO
        {
            public string Type { get; set; }
            public string LabelPrefix { get; set; }
            public int Units { get; set; }
            public string TypeText { get; set; } = string.Empty;
        }

        public static readonly List<RemoteModule> List =
           new List<RemoteModule>() {
           new RemoteModule { Name = "XM-Pro Junior", IOType = "On-Board I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "Analog Input", LabelPrefix="AI", Units = 2 },
                   new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Analog Output", LabelPrefix="AO", Units = 1 },
                   new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 8 },
                   new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 6 }
               } },
           new RemoteModule { Name = "MOD-DO-4R",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 4 } } },
           new RemoteModule { Name = "MOD-DO-8R",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 8 } } },
           new RemoteModule { Name = "MOD-DO-16R",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 16 } } },
           new RemoteModule { Name = "MOD-DI-4",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 4 } } },
           new RemoteModule { Name = "MOD-DI-8",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 8 } } },
           new RemoteModule { Name = "MOD-DI-16",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 16 } } },
           new RemoteModule { Name = "MOD-AI-2",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "Analog Input", LabelPrefix="AI", Units = 2 } } },
           new RemoteModule { Name = "MOD-AI-4",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "Analog Input", LabelPrefix="AI", Units = 4 } } },
           new RemoteModule { Name = "MOD-AO-2",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Analog Output", LabelPrefix="AO", Units = 2 } } },
           new RemoteModule { Name = "MOD-AO-4",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Analog Output", LabelPrefix="AO", Units = 4 } } },
           new RemoteModule { Name = "MOD-DI8-DO6",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "DI & DO", LabelPrefix="DI", Units = 8 },
                   new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "DI & DO", LabelPrefix="DO", Units = 6 }
               } },
           new RemoteModule { Name = "MOD-AI2-AO-2",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "AI & AO", LabelPrefix="AI", Units = 2 },
                   new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "AI & AO", LabelPrefix="AO", Units = 2 }
               } },
           new RemoteModule { Name = "MOD-CFC-4", StartingPosition = 1,
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Other", LabelPrefix="CH", Units = 4 } } },
           new RemoteModule { Name = "MOD-DIM-2-P", StartingPosition = 1,
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Other", LabelPrefix="CH", Units = 2 } } },
           new RemoteModule { Name = "MOD-DIM-4-P", StartingPosition = 1,
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Other", LabelPrefix="CH", Units = 4 } } },
           new RemoteModule { Name = "MOD-DO-4-R-S",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 4 } } },
           new RemoteModule { Name = "MOD-DO-8-R-S",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 8 } } },
           new RemoteModule { Name = "MOD-DO-16-R-S",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 16 } } },
           new RemoteModule { Name = "MOD-CTP-6",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Other", LabelPrefix="Key", Units = 6 } } },
           new RemoteModule { Name = "Other",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "Analog Input", LabelPrefix="AI", Units = 1 },
                   new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Analog Output", LabelPrefix="AO", Units = 0 },
                   new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 0 },
                   new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 0 }
               } },
           new RemoteModule { Name = "XM-DO-4R", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 4 } } },
           new RemoteModule { Name = "XM-DO-8R", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 8 } } },
           new RemoteModule { Name = "XM-DO-16R", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 16 } } },
           new RemoteModule { Name = "XM-DI-4", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 4 } } },
           new RemoteModule { Name = "XM-DI-8", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 8 } } },
           new RemoteModule { Name = "XM-DI-16", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 16 } } },
           new RemoteModule { Name = "XM-AI-2", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "Analog Input", LabelPrefix="AI", Units = 2 } } },
           new RemoteModule { Name = "XM-AI-4", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "Analog Input", LabelPrefix="AI", Units = 4 } } },
           new RemoteModule { Name = "XM-AO-2", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Analog Output", LabelPrefix="AO", Units = 2 } } },
           new RemoteModule { Name = "XM-AO-4", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Analog Output", LabelPrefix="AO", Units = 4 } } },
           new RemoteModule { Name = "XM-DI8-DO6T", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "DI & DO", LabelPrefix="DI", Units = 8 },
                   new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "DI & DO", LabelPrefix="DO", Units = 6 }
               } },
           new RemoteModule { Name = "XM-DI8-DO6R", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "DI & DO", LabelPrefix="DI", Units = 8 },
                   new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "DI & DO", LabelPrefix="DO", Units = 6 }
               } },
           new RemoteModule { Name = "XM-AI2-AO-2", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "AI & AO", LabelPrefix="AI", Units = 2 },
                   new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "AI & AO", LabelPrefix="AO", Units = 2 }
               } },
           new RemoteModule { Name = "XM-DO-4T", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 4 } } },
           new RemoteModule { Name = "XM-DO-8T", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 8 } } },
           new RemoteModule { Name = "XM-DO-16T", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 16 } } }
           };
    }
}
