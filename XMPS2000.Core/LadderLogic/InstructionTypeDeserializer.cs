using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMPS2000.Core.LadderLogic
{
    [XmlRoot("Instructions")]
    public class InstructionsRoot
    {
        [XmlElement("InstructionType")]
        public List<InstrType> InstrctionTypes { get; set; }
    }

    public class InstrType
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Instruction")]
        public List<InstructionTypeDeserializer> Instructions { get; set; }
    }


    public class InstructionTypeDeserializer
    {
        [XmlElement("ID")]
        public string ID { get; set; }

        [XmlElement("Text")]
        public string Text { get; set; }

        [XmlElement("InstructionType")]
        public string InstructionType { get; set; }

        [XmlElement("IsInputRequired")]
        public bool IsInputRequired { get; set; } = true;

        [XmlArray("InputsOutputs")]
        [XmlArrayItem("IOModel")]
        public List<IOModel> InputsOutputs { get; set; }

        [XmlElement("TcNameDetails")]
        public Counter TcNameDetails { get; set; }

        [XmlArray("SupportedDataTypes")]
        [XmlArrayItem("DataTypeDesilizer")]
        public List<DataTypeDesilizer> SupportedDataTypes { get; set; }

        [XmlArray("OutputDataTypes")]
        [XmlArrayItem("DataTypeDesilizer")]
        public List<DataTypeDesilizer> OutputDataTypes { get; set; }

    }
    public class IOModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Text")]
        public string Text { get; set; }

        [XmlElement("IsRequired")]
        public bool IsRequired { get; set; }

        [XmlElement("TextInFB")]
        public string TextInFB { get; set; }

        [XmlElement("DataType")]
        public string DataType { get; set; }
    }
    public class DataTypeDesilizer
    {
        public string ID { get; set; }
        public string Text { get; set; }

        public static readonly List<DataTypeDesilizer> List =
          new List<DataTypeDesilizer>() {
           new DataTypeDesilizer { ID="0x0000", Text="Bool"},
           new DataTypeDesilizer { ID="0x0001", Text="Byte"},
           new DataTypeDesilizer { ID="0x0002", Text="Word"},
           new DataTypeDesilizer { ID="0x0003", Text="Double Word"},
           new DataTypeDesilizer { ID="0x0004", Text="Int"},
           new DataTypeDesilizer { ID="0x0005", Text="Real"},
           new DataTypeDesilizer { ID="0x0006", Text="TON"},
           new DataTypeDesilizer { ID="0x0007", Text="TOFF"},
           new DataTypeDesilizer { ID="0x0008", Text="CTU"},
           new DataTypeDesilizer { ID="0x0009", Text="CTD"},
           new DataTypeDesilizer { ID="0x000A", Text="TP"},
           new DataTypeDesilizer { ID="0x000B", Text="RTON"},
           new DataTypeDesilizer { ID="0x000C", Text="DINT"},
           new DataTypeDesilizer { ID="0x000D", Text="UDINT"}
          };
    }
    public class Counter
    {
        [XmlElement("TC_Text")]
        public string Instruction { get; set; }

        [XmlElement("MaxIndex")]
        public int Maximum { get; set; }

        [XmlElement("StartIndex")]
        public int CurrentPosition { get; set; }

        public override string ToString()
        {
            return Instruction + CurrentPosition.ToString();
        }
    }
}