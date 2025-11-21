namespace XMPS2000.DBHelper
{
    /// <summary>
    /// Class Declared for DB in and Out purpose
    /// </summary>
    public class OnlineMonitor
    {
		//Adding New Change of 1 o/p and i/p
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
