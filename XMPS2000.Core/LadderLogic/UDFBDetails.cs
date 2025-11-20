using System.Xml.Serialization;

namespace XMPS2000.Core.LadderLogic
{
    public class UDFBDetails
    {
        public UDFBDetails()
        {
        }

        public UDFBDetails(UDFBDetails udfbdata)
        {
            LineNumber = udfbdata.LineNumber;
            TC_Name = udfbdata.TC_Name;
            OutputType = udfbdata.OutputType;
            OutPutType_NM = udfbdata.OutPutType_NM;
            DataType = udfbdata.DataType;
            DataType_Nm = udfbdata.DataType_Nm;
            Enable = udfbdata.Enable;
            Output1 = udfbdata.Output1;
            Output2 = udfbdata.Output2;
            Output3 = udfbdata.Output3;
        }

        

        [XmlElement("Name")]
        public string Name { get; set; }
        public int LineNumber { get; set; }
        public string TC_Name { get; set; }
        public string OutputType { get; set; }
        public string OutPutType_NM { get; set; }
        public string DataType { get; set; }
        public string DataType_Nm { get; set; }
        public string Enable { get; set; }
        public string Output1 { get; set; }
        public string Output2 { get; set; }

        public string Output3 { get; set; }
        public string OpCodeNm { get; set; }
        public string OpCode { get; set; }
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Input3 { get; set; }
        public string Input4 { get; set; }

        public string Input5 { get; set; }
        public string Comments { get; set; }

        public string WindowName { get; set; }
    }
}