using System;
using System.Collections;
using System.Xml.Serialization;

namespace XMPS2000.Core.LadderLogic
{
    public class ApplicationRung
    {
        public ApplicationRung()
        {
            Inputs = new Hashtable();
            Outputs = new Hashtable();
        }

        public ApplicationRung(ApplicationRung applicationData)
        {
            LineNumber = applicationData.LineNumber;
            TC_Name = applicationData.TC_Name;
            OutputType = applicationData.OutputType;
            OutPutType_NM = applicationData.OutPutType_NM;
            DataType = applicationData.DataType;
            DataType_Nm = applicationData.DataType_Nm;
            Enable = applicationData.Enable;
            Outputs = applicationData.Outputs;
            OpCodeNm = applicationData.OpCodeNm;
            OpCode = applicationData.OpCode;
            Inputs = applicationData.Inputs;
            Comments = applicationData.Comments;
            WindowName = applicationData.WindowName;
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
        [XmlIgnore]
        public Hashtable Outputs { get; set; }
        public string OpCodeNm { get; set; }
        public string OpCode { get; set; }
        [XmlIgnore]
        public Hashtable Inputs { get; set; }
        public string Comments { get; set; }

        public string WindowName { get; set; }
 
    }
}