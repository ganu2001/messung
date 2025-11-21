using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS1000.LadderLogic
{
	public class ApplicationData
	{
        public ApplicationData()
        {
        }

        public ApplicationData(ApplicationData applicationData)
        {
            WindowName = applicationData.WindowName;
            LineNumber = applicationData.LineNumber;
            TC_Name = applicationData.TC_Name;
            OutputType = applicationData.OutputType;
            OutPutType_NM = applicationData.OutPutType_NM;
            DataType = applicationData.DataType;
            DataType_Nm = applicationData.DataType_Nm;
            Enable = applicationData.Enable;
            Output1 = applicationData.Output1;
            Output2 = applicationData.Output2;
            OpCodeNm = applicationData.OpCodeNm;
            OpCode = applicationData.OpCode;
            Input1 = applicationData.Input1;
            Input2 = applicationData.Input2;
            Input3 = applicationData.Input3;
            Input4 = applicationData.Input4;
            Comments = applicationData.Comments;
        }

        public string WindowName { get; set; }
		public int LineNumber { get; set; }
		public string TC_Name { get; set; }
		public string OutputType { get; set; }
		public string OutPutType_NM { get; set; }
		public string DataType { get; set; }
		public string DataType_Nm { get; set; }
		public string Enable { get; set; }
		public string Output1 { get; set; }
		public string Output2 { get; set; }
		public string OpCodeNm { get; set; }
		public string OpCode { get; set; }
		public string Input1 { get; set; }
		public string Input2 { get; set; }
		public string Input3 { get; set; }
		public string Input4 { get; set; }
		public string Comments { get; set; }

	}
}
