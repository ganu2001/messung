using System.Collections.Generic;

namespace XMPS2000
{
    public class Mode
    {
        public string ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<Mode> List =
        new List<Mode>()
        {
                new Mode { ID = "00", Text = "" },
                new Mode { ID = "01", Text = "0 to 10V" },
                new Mode { ID = "02", Text = "0 to 20mA" },
                new Mode { ID = "03", Text = "4 to 20mA" },

        };
        //Mode for UI And UO

    }
    public class ModeUI
    {
        public string ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<ModeUI> List =
        new List<ModeUI>()
        {
                new ModeUI { ID = "00", Text = "" },
                new ModeUI { ID = "01", Text = "Digital" },
                new ModeUI { ID = "02", Text = "0-10V" },
                new ModeUI { ID = "03", Text = "0-5V" },
                new ModeUI { ID = "04", Text = "0-20mA" },
                new ModeUI { ID = "05", Text = "4-20mA" },
                new ModeUI { ID = "06", Text = "Resistance" },
                new ModeUI { ID = "07", Text = "PT100" },
                new ModeUI { ID = "08", Text = "PT1000" },
                new ModeUI { ID = "09", Text = "10K -NTC" },
                new ModeUI { ID = "10", Text = "20K -NTC" },
        };
        //Mode for UI And UO
        public static void ClearResistanceTables()
        {
            List.RemoveAll(m => int.TryParse(m.ID, out int id) && id >= 100);
        }
    }
    public class ModeUO
    {
        public string ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static readonly List<ModeUO> List =
        new List<ModeUO>()
        {
                new ModeUO { ID = "00", Text = "" },
                new ModeUO { ID = "01", Text = "Digital" },
                new ModeUO { ID = "02", Text = "0-10V" },
                new ModeUO { ID = "03", Text = "0-20mA" },
                new ModeUO { ID = "04", Text = "4-20mA" },

        };
        //Mode for UI And UO

    }
    public class RemoteModule
    {
        public string Name { get; private set; }
        public string ModCode { get; private set; }
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
            public const string UniversalInput = "Universal Input";
            public const string UniversalOutput = "Universal Output";
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
           new RemoteModule { Name = "MOD-DI-8",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 8 } } },
           new RemoteModule { Name = "MOD-DO-16R",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 16 } } },
           new RemoteModule { Name = "MOD-AI-4",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "Analog Input", LabelPrefix="AI", Units = 4 } } },
           new RemoteModule { Name = "MOD-AO-4",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Analog Output", LabelPrefix="AO", Units = 4 } } },
           new RemoteModule { Name = "MOD-CFC-4", StartingPosition = 1,
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Other", LabelPrefix="CH", Units = 4 } } },
           new RemoteModule { Name = "MOD-DIM-2-P", StartingPosition = 1,
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Other", LabelPrefix="CH", Units = 2 } } },
           new RemoteModule { Name = "MOD-DIM-4-P", StartingPosition = 1,
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "Other", LabelPrefix="CH", Units = 4 } } },
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
           new RemoteModule { Name = "XM-AI2-AO-2", ModCode = "0x000D", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "AI & AO", LabelPrefix="AI", Units = 2 },
                   new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "AI & AO", LabelPrefix="AO", Units = 2 }
               } },
           new RemoteModule { Name = "XM-DI-16", ModCode = "0x0006", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 16 } } },
           new RemoteModule { Name = "XM-DO-16T", ModCode = "0x0010", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 16 } } },
           new RemoteModule { Name = "XM-DI8-DO6T", ModCode = "0x000B", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "DI & DO", LabelPrefix="DI", Units = 8 },
                   new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "DI & DO", LabelPrefix="DO", Units = 6 }
               } },
           //New Expression
           new RemoteModule{Name = "XM-UI4-UO2",ModCode = "0x0011",IOType = "Expansion I/O",
            SupportedTypesAndIOs= new List<TypeAndIO>()
            {
                new TypeAndIO{Type = ModuleType.UniversalInput, TypeText = "Universal Input",LabelPrefix = "UI", Units = 4, },
                new TypeAndIO{Type = ModuleType.UniversalOutput, TypeText = "Universal Output",LabelPrefix = "UO", Units = 2,},
                //new TypeAndIO{T}
            } },
           ////Removing the model after confirmation from Sagar as on 12 May 2025
            ////new RemoteModule { Name = "XM-AI-4", ModCode = "0x0008", IOType = "Expansion I/O",
            ////   SupportedTypesAndIOs = new List<TypeAndIO>()
            ////   { new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "Analog Input", LabelPrefix="AI", Units = 4 } } },

            //adding new data in list for the XBLD Project Templates
               new RemoteModule{Name = "XBLD-UI4-UO2",ModCode = "0x0011",IOType = "Expansion I/O",
               SupportedTypesAndIOs= new List<TypeAndIO>()
               {
                new TypeAndIO{Type = ModuleType.UniversalInput, TypeText = "Universal Input",LabelPrefix = "UI", Units = 4, },
                new TypeAndIO{Type = ModuleType.UniversalOutput, TypeText = "Universal Output",LabelPrefix = "UO", Units = 2,},
                //new TypeAndIO{T}
               } },

               new RemoteModule { Name = "XBLD-AI2-AO2", ModCode = "0x000D", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.AnalogInput, TypeText = "AI & AO", LabelPrefix="AI", Units = 2 },
                   new TypeAndIO { Type = ModuleType.AnalogOutput, TypeText = "AI & AO", LabelPrefix="AO", Units = 2 }
               } },

                new RemoteModule { Name = "XBLD-DI16", ModCode = "0x0006", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "Digital Input", LabelPrefix="DI", Units = 16 } } },

                 new RemoteModule { Name = "XBLD-DO16", ModCode = "0x0010", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               { new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "Digital Output", LabelPrefix="DO", Units = 16 } } },

                 new RemoteModule { Name = "XBLD-DI8-DO6", ModCode = "0x000B", IOType = "Expansion I/O",
               SupportedTypesAndIOs = new List<TypeAndIO>()
               {
                   new TypeAndIO { Type = ModuleType.DigitalInput, TypeText = "DI & DO", LabelPrefix="DI", Units = 8 },
                   new TypeAndIO { Type = ModuleType.DigitalOutput, TypeText = "DI & DO", LabelPrefix="DO", Units = 6 }
               } },
           };
    }
}
