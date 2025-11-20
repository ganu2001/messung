using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.Core.App
{
    public class McodeGeneration
    {
        //private XMPS xm = XMPS.Instance;
        //public McodeGeneration() { }
        private static readonly XMPS xm = XMPS.Instance;
        // public static List<byte> _mcodelist = new List<byte>();
        public static List<long> _mcodelist2 = new List<long>();
        // for the CRC Calculation
        public static List<long> _mcodelistForCRC = new List<long>();
        public static uint intValue;
        public static byte[] byteArray;
        public static byte[] byteArray2 = new byte[4];      //as #, $ are of 2 bytes copying zero in remaining 4 byte
        public static string _convertlogicaladdess;
        public static int _rowcount;
        public static int _startframe = 36; //36  $
        public static int _endframe = 35;  //35   #
        public static string Rung_no { get; set; }
        public static string Datatype { get; set; }
        public static string Enable_type { get; set; } // ==> we have to create list for enabletype as enable is - as 0 (unchecked), if address is presnt -> 1 (checked) & 2 -> negation
        public static string Enable { get; set; }
        public static string Opcode { get; set; }
        public static string Type_operand_1 { get; set; }

        public static string OP1 { get; set; }              //Operand 1 --> Inputs

        public static string Type_operand_2 { get; set; }

        public static string OP2 { get; set; }

        public static string Type_operand_3 { get; set; }

        public static string OP3 { get; set; }

        public static string Type_operand_4 { get; set; }

        public static string OP4 { get; set; }

        public static int No_of_Operand_ { get; set; }       //No of Inputs Count

        public static string T_C_Name { get; set; }

        public static string Output1 { get; set; }

        public static string Output2 { get; set; }



        int opcounter = 0;
        //Get the AppCsv file 
        // Task =====> Create three separate function for hexvalue for storing in list 
        // 1. if value is "-" set zero and convert 4 byte 
        // 2. For Inputs , Output or any Logical Address present 
        // 3. Convert Simple Values to byte
        public void FetchDataCSV(ref DataTable AppData)
        {
            var Publish = (Publish)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").FirstOrDefault();
            _mcodelist2.Add(8484); //Start of frame 
            McodeofMainLogicBlocks(AppData);
            // StaticFrame(_startframe);
            _mcodelist2.Add(_startframe);  //Start of logic !$
            int ipt_count = AppData.AsEnumerable().Where(t => t.Field<String>("WindowName") != null && t.Field<String>("WindowName").StartsWith("I")).Count();
            ipt_count = ipt_count > 0 ? Convert.ToInt32(AppData.AsEnumerable().Where(t => t.Field<String>("WindowName") != null && t.Field<String>("WindowName").StartsWith("I"))
                                .Select(t =>
                                {
                                    var lineNumber = t.Field<string>("Line Number");
                                    return lineNumber.Contains("@") ? lineNumber.Split('@')[1] : lineNumber;
                                }).FirstOrDefault()) : ipt_count;
            //GettingRows Count
            int rowsCount = ipt_count > 0 ? AppData.Rows.Count - (AppData.Rows.Count - ipt_count + 1) : AppData.Rows.Count;
            _rowcount = rowsCount;
            FillSimpleByteList(_rowcount.ToString());
            int rungno = 0;
            ///Generate M code for each rung
            for (int i = 0; i < rowsCount; i++)
            {
                string blockName = AppData.Rows[i].ItemArray[13].ToString();
                if (blockName.Length < 1)
                    rungno++;
                else
                    rungno = 1;
                Rung_no = AppData.Rows[i].ItemArray[0].ToString();
                FillSimpleByteList(Rung_no);
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))        //Dataype As per Srs Datatype Should be zero of Bool For Mqtt
                    Datatype = "0";
                else
                    Datatype = AppData.Rows[i].ItemArray[3].ToString();
                int Converttoint = Convert.ToInt32(Datatype, 16);
                FillSimpleByteList(Converttoint.ToString());
                Enable_type = AppData.Rows[i].ItemArray[4].ToString();
                if (Enable_type == "-")
                    Enable_type = "0";
                else if (Enable_type.Contains("~"))
                    Enable_type = "2";
                else
                    Enable_type = "1";
                FillSimpleByteList(Enable_type);
                Enable = AppData.Rows[i].ItemArray[4].ToString();
                if (Enable == "-")
                    FillSimpleByteList("0");
                else
                    FillLogicalByteList(Enable);
                //Opcode -----> Opcode.Remove(3)
                Opcode = AppData.Rows[i].ItemArray[7].ToString();
                Opcode = Opcode.Remove(3);
                int OPhexvalue = Convert.ToInt32(Opcode, 16);
                FillSimpleByteList(OPhexvalue.ToString());
                //Type_Operand 1 --> Logical address --> 0 (Normal Mode) , 1 - Negation ?????, Empty -> not used 2 , Numeric --> 3
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))
                    FillSimpleByteList("0");
                else
                    FillSimpleByteList(GetOperandType(AppData.Rows[i].ItemArray[8].ToString()));
                //Operand 1
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))                //As Per SRS For MQTT FB Operand1 will Topic number
                {
                    OP1 = AppData.Rows[i].ItemArray[6].ToString().Replace(".Topic", "");//xm.LoadedProject.TopicSelected.ToString();
                    string opval = OP1;
                    if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB"))
                    {
                        var publist = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
                        opval = publist.Where(pb => pb.topic == OP1).Select(pb => pb.keyvalue).FirstOrDefault().ToString();
                    }
                    else
                    {
                        var sublist = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                        opval = sublist.Where(sb => sb.topic == OP1).Select(sb => sb.key).FirstOrDefault().ToString();
                    }
                    FillSimpleByteList(OP1);
                    opcounter++;
                }
                else if (AppData.Rows[i].ItemArray[7].ToString().Equals("0415") || AppData.Rows[i].ItemArray[7].ToString().Equals("0446")  || AppData.Rows[i].ItemArray[7].ToString().Equals("0456")) // If instruction is NULL instruction and data type selected is Real
                    GetOperand(AppData.Rows[i].ItemArray[8].ToString(), "0004");
                else
                    GetOperand(AppData.Rows[i].ItemArray[8].ToString(), Datatype);
                //Type_Operand 2
                FillSimpleByteList(GetOperandType(AppData.Rows[i].ItemArray[9].ToString()));
                //Operand 2
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))                //As Per SRS For MQTT FB Operand1 will Topic number
                    FillSimpleByteList("0");
                else
                {
                    if (!Opcode.Equals("041") && !Opcode.Equals("042") && !Opcode.Equals("043") && !Opcode.Equals("044")  && !Opcode.Equals("045"))
                        GetOperand(AppData.Rows[i].ItemArray[9].ToString(), Datatype);
                    else
                    {
                        ConvertNumericToByte(AppData.Rows[i].ItemArray[9].ToString());
                        opcounter++;
                    }
                }
                //Type_Operand 3
                FillSimpleByteList(GetOperandType(AppData.Rows[i].ItemArray[10].ToString()));
                //Operand 3 
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))                //As Per SRS For MQTT FB Operand1 will Topic number
                    FillSimpleByteList("0");
                else
                {
                    if (!Opcode.Equals("041") && !Opcode.Equals("042") && !Opcode.Equals("043"))
                        GetOperand(AppData.Rows[i].ItemArray[10].ToString(), Datatype);
                    else
                    {
                        ConvertNumericToByte(AppData.Rows[i].ItemArray[10].ToString());
                        opcounter++;
                    }
                }

                //Operand 4
                FillSimpleByteList(GetOperandType(AppData.Rows[i].ItemArray[11].ToString()));
                //Operand 4
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))                //As Per SRS For MQTT FB Operand1 will Topic number
                    FillSimpleByteList("0");
                else if (AppData.Rows[i].ItemArray[7].ToString().Equals("0425")) // If instruction is NULL instruction and data type selected is Real
                    GetOperand(AppData.Rows[i].ItemArray[11].ToString(), "0004");
                else
                    GetOperand(AppData.Rows[i].ItemArray[11].ToString(), Datatype);
                //Number of Operands 
                No_of_Operand_ = opcounter;
                FillSimpleByteList(No_of_Operand_.ToString());
                //TC Name
                T_C_Name = AppData.Rows[i].ItemArray[1].ToString();
                if (T_C_Name == "-" || AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))
                    T_C_Name = "0";
                else
                    T_C_Name = SeparateTCname(T_C_Name);
                FillSimpleByteList(T_C_Name);
                Output1 = AppData.Rows[i].ItemArray[5].ToString();
                if (Output1 == "-" || Output1 == "")
                    FillSimpleByteList("0");
                else
                    FillLogicalByteList(Output1);
                //Output 2
                Output2 = AppData.Rows[i].ItemArray[6].ToString();
                if (Output2 == "-" || Output2 == "" || AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))
                    FillSimpleByteList("0");
                else
                    FillLogicalByteList(Output2);
                opcounter = 0;
                //adding for calculating the Mcode for the ReadProperty Function block.
                if (Opcode.Equals("043"))
                {
                    ForReadPropertyFunctionBlock(ref i, ref AppData);
                }
            }
            //End of Frame 
            //Adding F in decimal in _mcodelist for Interrupt block
            _mcodelist2.Add(15);
            ///M code of first intrrupt block
            if (xm.LoadedProject.FirstInterruptBlock != "" && xm.LoadedProject.FirstInterruptBlock != "None" && xm.LoadedProject.FirstInterruptBlock != null)
            {
                DataTable dt = new DataTable();
                string interruptBlockName = xm.LoadedProject.FirstInterruptBlock;
                Block B = xm.LoadedProject.Blocks.Where(T => T.Name == interruptBlockName).FirstOrDefault();
                int count = 0;
                if (xm.LoadedProject.FirstInterruptBlock == "Interrupt_Logic_Block04")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB04").TakeWhile(row => row.Field<string>("WindowName") == "IHB04" || row.Field<string>("WindowName") == null).Count();
                }
                else if (xm.LoadedProject.FirstInterruptBlock == "Interrupt_Logic_Block03")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB03").TakeWhile(row => row.Field<string>("WindowName") == "IHB03" || row.Field<string>("WindowName") == null).Count();
                }
                else if (xm.LoadedProject.FirstInterruptBlock == "Interrupt_Logic_Block02")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB02").TakeWhile(row => row.Field<string>("WindowName") == "IHB02" || row.Field<string>("WindowName") == null).Count();
                }
                else
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB01").TakeWhile(row => row.Field<string>("WindowName") == "IHB01" || row.Field<string>("WindowName") == null).Count();
                }
                WriteInterruptionMCode(interruptBlockName, count, AppData, rowsCount);
            }
            rowsCount = GetWithoutInterruptBlkRowCount(AppData);
            ///M code of second intrrupt block
            if (xm.LoadedProject.SecondInterruptBlock != "" && xm.LoadedProject.SecondInterruptBlock != "None" && xm.LoadedProject.SecondInterruptBlock != null)
            {
                DataTable dt = new DataTable();
                string interruptBlockName = xm.LoadedProject.SecondInterruptBlock;
                Block B = xm.LoadedProject.Blocks.Where(T => T.Name == interruptBlockName).FirstOrDefault();
                int count = 0;
                if (xm.LoadedProject.SecondInterruptBlock == "Interrupt_Logic_Block04")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB04").TakeWhile(row => row.Field<string>("WindowName") == "IHB04" || row.Field<string>("WindowName") == null).Count();
                }
                else if (xm.LoadedProject.SecondInterruptBlock == "Interrupt_Logic_Block03")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB03").TakeWhile(row => row.Field<string>("WindowName") == "IHB03" || row.Field<string>("WindowName") == null).Count();
                }
                else if (xm.LoadedProject.SecondInterruptBlock == "Interrupt_Logic_Block02")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB02").TakeWhile(row => row.Field<string>("WindowName") == "IHB02" || row.Field<string>("WindowName") == null).Count();
                }
                else
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB01").TakeWhile(row => row.Field<string>("WindowName") == "IHB01" || row.Field<string>("WindowName") == null).Count();
                }
                WriteInterruptionMCode(interruptBlockName, count, AppData, rowsCount);
            }
            rowsCount = GetWithoutInterruptBlkRowCount(AppData);
            ///M code of third intrrupt block
            if (xm.LoadedProject.ThirdInterruptBlock != "" && xm.LoadedProject.ThirdInterruptBlock != "None" && xm.LoadedProject.ThirdInterruptBlock != null)
            {
                DataTable dt = new DataTable();
                string interruptBlockName = xm.LoadedProject.ThirdInterruptBlock;
                Block B = xm.LoadedProject.Blocks.Where(T => T.Name == interruptBlockName).FirstOrDefault();
                int count = 0;
                if (xm.LoadedProject.ThirdInterruptBlock == "Interrupt_Logic_Block04")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB04").TakeWhile(row => row.Field<string>("WindowName") == "IHB04" || row.Field<string>("WindowName") == null).Count();
                }
                else if (xm.LoadedProject.ThirdInterruptBlock == "Interrupt_Logic_Block03")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB03").TakeWhile(row => row.Field<string>("WindowName") == "IHB03" || row.Field<string>("WindowName") == null).Count();
                }
                else if (xm.LoadedProject.ThirdInterruptBlock == "Interrupt_Logic_Block02")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB02").TakeWhile(row => row.Field<string>("WindowName") == "IHB02" || row.Field<string>("WindowName") == null).Count();
                }
                else
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB01").TakeWhile(row => row.Field<string>("WindowName") == "IHB01" || row.Field<string>("WindowName") == null).Count();
                }
                WriteInterruptionMCode(interruptBlockName, count, AppData, rowsCount);
            }
            rowsCount = GetWithoutInterruptBlkRowCount(AppData);
            ///M code of forth intrrupt block
            if (xm.LoadedProject.FourthInterruptBlock != "" && xm.LoadedProject.FourthInterruptBlock != "None" && xm.LoadedProject.FourthInterruptBlock != null)
            {
                DataTable dt = new DataTable();
                string interruptBlockName = xm.LoadedProject.FourthInterruptBlock;
                Block B = xm.LoadedProject.Blocks.Where(T => T.Name == interruptBlockName).FirstOrDefault();
                int count = 0;
                if (xm.LoadedProject.FourthInterruptBlock == "Interrupt_Logic_Block04")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB04").TakeWhile(row => row.Field<string>("WindowName") == "IHB04" || row.Field<string>("WindowName") == null).Count();
                }
                else if (xm.LoadedProject.FourthInterruptBlock == "Interrupt_Logic_Block03")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB03").TakeWhile(row => row.Field<string>("WindowName") == "IHB03" || row.Field<string>("WindowName") == null).Count();
                }
                else if (xm.LoadedProject.FourthInterruptBlock == "Interrupt_Logic_Block02")
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB02").TakeWhile(row => row.Field<string>("WindowName") == "IHB02" || row.Field<string>("WindowName") == null).Count();
                }
                else
                {
                    count = AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != "IHB01").TakeWhile(row => row.Field<string>("WindowName") == "IHB01" || row.Field<string>("WindowName") == null).Count();
                }
                WriteInterruptionMCode(interruptBlockName, count, AppData, rowsCount);
            }
            _mcodelist2.Add(_endframe);
            if (ProjectHelper.CheckFileCRC(_mcodelist2, "Mcode"))
                if (!ProjectHelper.CheckWithOldFile("McodeVersion.txt", _mcodelist2))
                    xm.LoadedProject.MCodeChangeCRC ^= 1;
            NotePadFile(_mcodelist2);
        }

        private void ForReadPropertyFunctionBlock(ref int i, ref DataTable AppData)
        {
            i++;
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 12; k++)
                {
                    string outputAddress = AppData.Rows[i].ItemArray[k].ToString();
                    outputAddress = outputAddress.Contains("@") ? outputAddress.Split('@')[0] : outputAddress;
                    if (outputAddress.Equals("-") || string.IsNullOrEmpty(outputAddress))
                    {
                        FillSimpleByteList("0");
                    }
                    else
                    {
                        FillLogicalByteList(outputAddress);
                    }
                }

                i++;
            }
            i--;
            for (int blank = 0; blank < 10; blank++)
                FillSimpleByteList("0");

        }

        private static byte[] GetStreamHash(HashAlgorithm hashAlgorithm, MemoryStream memoryStream)
        {
            // Ensure the memory stream position is at the beginning
            memoryStream.Position = 0;
            return hashAlgorithm.ComputeHash(memoryStream);
        }

        private void WriteInterruptionMCode(string interruptBlockName, int count, DataTable AppData, int rowsCount)
        {
            string windowNameToExtract = GetBlockNameFromAppFile(interruptBlockName);
            int interruptBlockNum = GetBlockNumberFromName(interruptBlockName);
            _mcodelist2.Add(interruptBlockNum);
            var filteredRows = AppData.AsEnumerable()
                .SkipWhile(row => row.Field<string>("WindowName") != windowNameToExtract)
                .Take(count);
            if (filteredRows.Count() > 0)
            {
                DataTable newDataTable = filteredRows.CopyToDataTable();
                switch (interruptBlockName)
                {
                    case "Interrupt_Logic_Block04":
                        rowsCount = ProcessInterruptBlock("IHB03", "IHB02", "IHB01", AppData, rowsCount);
                        break;
                    case "Interrupt_Logic_Block03":
                        rowsCount = ProcessInterruptBlock(null, "IHB02", "IHB01", AppData, rowsCount);
                        break;
                    case "Interrupt_Logic_Block02":
                        rowsCount = ProcessInterruptBlock(null, null, "IHB01", AppData, rowsCount);
                        break;
                }
                CalculateMcodeForInterrupt(newDataTable, rowsCount);
            }
            else
            {
                _mcodelist2.Add(0);
            }
        }

        private void GetOperand(string operand, string datatype)
        {
            if (operand == "-" || operand == "")
            {
                FillSimpleByteList("0");
            }
            else if (operand.Contains(":"))
            {
                FillLogicalByteList(operand);
                opcounter++;
            }
            else
            {
                if ((datatype == "0005" || datatype == "0004" || datatype == "000C") && operand.StartsWith("-"))          //Datatype Is Real ==> Convert Numeric Values to Float
                {
                    float parsedFloat1 = float.Parse(operand);
                    ConvertFloatValuesToInt(parsedFloat1);
                }
                //if Numeric values is Float values
                else if (datatype == "0005" || operand.Contains("."))
                {
                    float parsedFloat1 = float.Parse(operand);
                    ConvertFloatValuesToInt(parsedFloat1);
                }
                else
                    ConvertNumericToByte(operand);
                opcounter++;
            }
        }

        private string GetOperandType(string operandType)
        {
            if (operandType != "-" && !operandType.Contains(":"))
                operandType = Regex.Match(operandType, @"^-?(\d+)").Groups[1].Value;  //12.12
            if (operandType.Contains("~"))
                operandType = "1";                                       //1 --> negation
            else if (operandType.Contains(":"))
                operandType = "0";
            else if (operandType == "-" || operandType == "")
                operandType = "2";
            else                                  //Numeric Values
                operandType = "3";
            return operandType;
        }

        private void McodeofMainLogicBlocks(DataTable AppData)
        {
            FillSimpleByteList(xm.LoadedProject.MainLadderLogic.Where(R => !R.StartsWith("'") && !R.StartsWith("Interrupt_Logic")).Count().ToString());
            List<string> blocks = xm.LoadedProject.MainLadderLogic.Where(d => !(d.StartsWith("'")) && !d.StartsWith("Interrupt_Logic")).ToList();
            int blockno = 1;
            foreach (string lb in blocks)
            {
                string lbname = lb.Contains("[") ? lb.Substring(lb.IndexOf('[')).Replace("[", "").Replace("]", "") : lb;
                //Logic block number
                FillSimpleByteList(blockno.ToString());
                //Start rung number of Logic block 1
                var data = AppData.AsEnumerable();
                if (data.Count() > 0)
                {
                    //data.Where(d => d.Field<string>("LadderName") == lbname).Select(d => decimal.Parse(d.Field<string>("Line Number"))).Min()
                    FillSimpleByteList((data.Where(d => d.Field<string>("LadderName") == lbname).Count()) > 0 ? data.Where(d => d.Field<string>("LadderName") == lbname)
                                                  .Select(d =>
                                                  {
                                                      var lineNumber = d.Field<string>("Line Number");
                                                      var actualValue = lineNumber.Contains("@") ? lineNumber.Split('@')[1] : lineNumber;
                                                      return decimal.Parse(actualValue);
                                                  }).Min().ToString() : "0");
                    //Number of rungs in Logic block 1 ex. 3 rungs.

                    int min = (data.Where(d => d.Field<string>("LadderName") == lbname).Count()) > 0 ? (int)data.Where(d => d.Field<string>("LadderName") == lbname)
                                              .Select(d =>
                                              {
                                                  var lineNumber = d.Field<string>("Line Number");
                                                  var actualValue = lineNumber.Contains("@") ? lineNumber.Split('@')[1] : lineNumber;
                                                  return decimal.Parse(actualValue);
                                              }).Min() - 1 : 0;
                    if ((data.Where(d => d.Field<string>("LadderName") == lbname).Count()) > 0)
                        FillSimpleByteList(((int)(data.Where(d => d.Field<string>("LadderName") == lbname)
                                           .Select(d =>
                                           {
                                               var lineNumber = d.Field<string>("Line Number");
                                               var actualValue = lineNumber.Contains("@") ? lineNumber.Split('@')[1] : lineNumber;
                                               return decimal.Parse(actualValue);
                                           })).Max() - min).ToString());
                    else
                        FillSimpleByteList("0");
                    string[] x = lb.Split(')');
                    string OP1 = lb.Contains('(') ? x[0].Replace("(", "") : string.Empty;
                    string OP2 = x.Count() > 1 ? x[1].Replace("AND (", "") : "";
                    // Type_operand_1 normal/ negation
                    FillSimpleByteList(GetValueOfContact(OP1.Trim()));

                    //Convert hex data to uint before adding it to list
                    FillSimpleByteList(uint.Parse(xm.GetHexAddress(OP1.Contains(":") ? OP1 : xm.LoadedProject.Tags.Where(t => t.Tag == OP1.Replace("~", "").Trim()).Select(t => t.LogicalAddress).FirstOrDefault()), System.Globalization.NumberStyles.HexNumber).ToString());
                    //Type_operand_2 normal/ negation / 0
                    //OP2
                    FillSimpleByteList(GetValueOfContact(OP2.Trim()));
                    //Convert hex data to uint before adding it to list
                    FillSimpleByteList(uint.Parse(xm.GetHexAddress(OP2.Contains(":") ? OP2.Replace("~", "") : xm.LoadedProject.Tags.Where(t => t.Tag == OP2.Replace("~", "").Trim()).Select(t => t.LogicalAddress).FirstOrDefault()), System.Globalization.NumberStyles.HexNumber).ToString());
                }
                else
                {
                    FillSimpleByteList("0");
                    FillSimpleByteList("0");
                    string[] x = lb.Split(')');
                    string OP1 = lb.Contains('(') ? x[0].Replace("(", "") : string.Empty;
                    string OP2 = x.Count() > 1 ? x[1].Replace("AND (", "") : "";
                    // Type_operand_1 normal/ negation
                    FillSimpleByteList(GetValueOfContact(OP1.Trim()));
                    //Convert hex data to uint before adding it to list
                    FillSimpleByteList(uint.Parse(xm.GetHexAddress(OP1.Contains(":") ? OP1 : xm.LoadedProject.Tags.Where(t => t.Tag == OP1.Replace("~", "").Trim()).Select(t => t.LogicalAddress).FirstOrDefault()), System.Globalization.NumberStyles.HexNumber).ToString());
                    //Type_operand_2 normal/ negation / 0
                    //OP2
                    FillSimpleByteList(GetValueOfContact(OP2.Trim()));
                    //Convert hex data to uint before adding it to list
                    FillSimpleByteList(uint.Parse(xm.GetHexAddress(OP2.Contains(":") ? OP2.Replace("~", "") : xm.LoadedProject.Tags.Where(t => t.Tag == OP2.Replace("~", "").Trim()).Select(t => t.LogicalAddress).FirstOrDefault()), System.Globalization.NumberStyles.HexNumber).ToString());
                }

                blockno++;
            }
        }
        private string GetValueOfContact(string Operation)
        {
            ///Return 0 for normal address and 1 for negation and 2 for not used
            return (Operation is null || xm.LoadedProject.Tags.Where(t => t.Tag == Operation.Trim().Replace("~", "")).Count() == 0) ? "2" : Operation.StartsWith("~") ? "1" : "0";
        }
        private int GetWithoutInterruptBlkRowCount(DataTable AppData)
        {
            int ipt_count = AppData.AsEnumerable().Where(t => t.Field<String>("WindowName") != null && t.Field<String>("WindowName").StartsWith("I")).Count();
            ipt_count = ipt_count > 0 ? Convert.ToInt32(AppData.AsEnumerable().Where(t => t.Field<String>("WindowName") != null && t.Field<String>("WindowName").StartsWith("I"))
                .Select(d =>
                {
                    var lineNumber = d.Field<string>("Line Number");
                    var actualValue = lineNumber.Contains("@") ? lineNumber.Split('@')[1] : lineNumber;
                    return decimal.Parse(actualValue);
                }).FirstOrDefault()) : ipt_count;
            //GettingRows Count
            int rowsCount = ipt_count > 0 ? AppData.Rows.Count - (AppData.Rows.Count - ipt_count + 1) : AppData.Rows.Count;
            return rowsCount;
        }

        private int ProcessInterruptBlock(string v1, string v2, string v3, DataTable AppData, int rowsCount)
        {
            if (v1 != null)
            {
                var count1 = AppData.AsEnumerable().Count(row => row.Field<string>("WindowName") == v1);
                if (count1 > 0)
                {
                    rowsCount += AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != v1)
                        .TakeWhile(row => row.Field<string>("WindowName") == v1 || row.Field<string>("WindowName") == null)
                        .Count();
                }
            }

            if (v2 != null)
            {
                var count1 = AppData.AsEnumerable().Count(row => row.Field<string>("WindowName") == v2);
                if (count1 > 0)
                {
                    rowsCount += AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != v2)
                         .TakeWhile(row => row.Field<string>("WindowName") == v2 || row.Field<string>("WindowName") == null)
                         .Count();
                }
            }

            if (v3 != null)
            {
                var count1 = AppData.AsEnumerable().Count(row => row.Field<string>("WindowName") == v3);
                if (count1 > 0)
                {
                    rowsCount += AppData.AsEnumerable().SkipWhile(row => row.Field<string>("WindowName") != v3)
                        .TakeWhile(row => row.Field<string>("WindowName") == v3 || row.Field<string>("WindowName") == null)
                        .Count();
                }
            }
            return rowsCount;
        }

        private int GetBlockNumberFromName(string firstInterruptBlockName)
        {
            switch (firstInterruptBlockName)
            {
                case "Interrupt_Logic_Block01":
                    return 1;
                case "Interrupt_Logic_Block02":
                    return 2;
                case "Interrupt_Logic_Block03":
                    return 3;
                case "Interrupt_Logic_Block04":
                    return 4;
                default: return 0;
            }
        }

        private string GetBlockNameFromAppFile(string firstInterruptBlockName)
        {
            switch (firstInterruptBlockName)
            {
                case "Interrupt_Logic_Block01":
                    return "IHB01";
                case "Interrupt_Logic_Block02":
                    return "IHB02";
                case "Interrupt_Logic_Block03":
                    return "IHB03";
                case "Interrupt_Logic_Block04":
                    return "IHB04";
                default: return "";
            }
        }


        private void CalculateMcodeForInterrupt(DataTable AppData, int rowsCount)
        {
            _rowcount = AppData.Rows.Count;
            // _mcodelist2.Add(_rowcount);
            FillSimpleByteList(_rowcount.ToString());
            for (int i = 0; i < AppData.Rows.Count; i++)
            {
                string blockName = AppData.Rows[i].ItemArray[13].ToString();
                Rung_no = AppData.Rows[i].ItemArray[0].ToString();
                int rung_No = Convert.ToInt32(Rung_no) - rowsCount;
                //_mcodelist2.Add(Convert.ToInt64(Rung_no));
                FillSimpleByteList(rung_No.ToString());

                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))        //Dataype As per Srs Datatype Should be zero of Bool For Mqtt
                {
                    Datatype = "0";
                }
                else
                {
                    Datatype = AppData.Rows[i].ItemArray[3].ToString();
                }
                int Converttoint = Convert.ToInt32(Datatype, 16);
                FillSimpleByteList(Converttoint.ToString());

                Enable_type = AppData.Rows[i].ItemArray[4].ToString();
                if (Enable_type == "-")
                    Enable_type = "0";
                else if (Enable_type.Contains("~"))
                    Enable_type = "2";
                else
                    Enable_type = "1";

                FillSimpleByteList(Enable_type);

                Enable = AppData.Rows[i].ItemArray[4].ToString();
                if (Enable == "-")
                {
                    Enable = "0";
                    FillSimpleByteList(Enable);
                }
                else
                {
                    FillLogicalByteList(Enable);
                }

                //Opcode -----> Opcode.Remove(3)
                Opcode = AppData.Rows[i].ItemArray[7].ToString();
                Opcode = Opcode.Remove(3);
                int OPhexvalue = Convert.ToInt32(Opcode, 16);
                FillSimpleByteList(OPhexvalue.ToString());
                //Type_Operand 1 --> Logical address --> 0 (Normal Mode) , 1 - Negation ?????, Empty -> not used 2 , Numeric --> 3
                FillSimpleByteList(GetOperandType(AppData.Rows[i].ItemArray[8].ToString()));
                //Operand 1
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))                //As Per SRS For MQTT FB Operand1 will Topic number
                {
                    //OP1 = Publish.
                    OP1 = AppData.Rows[i].ItemArray[6].ToString().Replace(".Topic", "");//xm.LoadedProject.TopicSelected.ToString();
                    //Applying Regex For Considering only Number of Topic
                    //string pattern = @"\d+"; // This pattern matches one or more digits
                    //MatchCollection matches = Regex.Matches(OP1, pattern);
                    //OP1 = matches[0].Value;
                    string opval = OP1;
                    if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB"))
                    {
                        var publist = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
                        opval = publist.Where(pb => pb.topic == OP1).Select(pb => pb.keyvalue).FirstOrDefault().ToString();
                    }
                    else
                    {
                        var sublist = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                        opval = sublist.Where(sb => sb.topic == OP1).Select(sb => sb.key).FirstOrDefault().ToString();
                    }
                    FillSimpleByteList(OP1);
                    opcounter++;
                }
                else
                    GetOperand(AppData.Rows[i].ItemArray[8].ToString(), Datatype);

                //Type_Operand 2
                FillSimpleByteList(GetOperandType(AppData.Rows[i].ItemArray[9].ToString()));
                //Operand 2
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))                //As Per SRS For MQTT FB Operand1 will Topic number
                {
                    //OP1 = Publish.
                    // opcounter++;                                      //According To SRS Operands 2 to 4 will remain empty.
                    OP2 = "0";
                    FillSimpleByteList("0");
                }
                else
                {
                    if (!Opcode.Equals("041") && !Opcode.Equals("042") && !Opcode.Equals("043"))
                        GetOperand(AppData.Rows[i].ItemArray[9].ToString(), Datatype);
                    else
                    {
                        ConvertNumericToByte(AppData.Rows[i].ItemArray[9].ToString());
                        opcounter++;
                    }
                }
                //Type_Operand 3
                FillSimpleByteList(GetOperandType(AppData.Rows[i].ItemArray[10].ToString()));
                //Operand 3 
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))                //As Per SRS For MQTT FB Operand1 will Topic number
                {
                    //OP1 = Publish.
                    // opcounter++;                                              //According To SRS Operands 2 to 4 will remain empty.
                    OP3 = "0";
                    FillSimpleByteList("0");
                }
                else
                {
                    if (!Opcode.Equals("041") && !Opcode.Equals("042") && !Opcode.Equals("043"))
                        GetOperand(AppData.Rows[i].ItemArray[10].ToString(), Datatype);
                    else
                    {
                        ConvertNumericToByte(AppData.Rows[i].ItemArray[10].ToString());
                        opcounter++;
                    }
                }
                //Operand 4
                FillSimpleByteList(GetOperandType(AppData.Rows[i].ItemArray[11].ToString()));
                //Operand 4
                if (AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))                //As Per SRS For MQTT FB Operand1 will Topic number
                {
                    //OP1 = Publish.
                    //opcounter++;                                          //According To SRS Operands 2 to 4 will remain empty.
                    OP4 = "0";
                    FillSimpleByteList("0");
                }
                else
                    GetOperand(AppData.Rows[i].ItemArray[11].ToString(), Datatype);
                //Number of Operands 
                No_of_Operand_ = opcounter;
                FillSimpleByteList(No_of_Operand_.ToString());
                //TC Name
                T_C_Name = AppData.Rows[i].ItemArray[1].ToString();
                if (T_C_Name == "-" || AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))
                    FillSimpleByteList("0");
                else
                {
                    T_C_Name = SeparateTCname(T_C_Name);
                    FillSimpleByteList(T_C_Name);
                }
                Output1 = AppData.Rows[i].ItemArray[5].ToString();
                if (Output1 == "-" || Output1 == "")
                    FillSimpleByteList("0");
                else
                    FillLogicalByteList(Output1);
                //Output 2
                Output2 = AppData.Rows[i].ItemArray[6].ToString();
                if (Output2 == "-" || Output2 == "" || AppData.Rows[i].ItemArray[1].ToString().Contains("PUB") || AppData.Rows[i].ItemArray[1].ToString().Contains("SUB"))
                    FillSimpleByteList("0");
                else
                    FillLogicalByteList(Output2);
                opcounter = 0;
                //adding for calculating the Mcode for the ReadProperty Function block.
                if (Opcode.Equals("043"))
                {
                    ForReadPropertyFunctionBlock(ref i, ref AppData);
                }
            }

        }

        private void CheckInterruptLogicBlock(string blockName)
        {
            string firstInterruptBlock = xm.LoadedProject.FirstInterruptBlock;
            string secondInterruptBlock = xm.LoadedProject.SecondInterruptBlock;
            string[] parts = blockName.Split('B');
            //IHB01
            if (firstInterruptBlock.EndsWith(parts[1]) || secondInterruptBlock.EndsWith(parts[1]))
            {
                string interruptLogicBlockName = $"Interrupt_Logic_Block{blockName.Split('B')[1]}";
                _mcodelist2.Add(Convert.ToInt32(blockName.Split('B')[1]));
                Block B = xm.LoadedProject.Blocks.Where(b => b.Name == interruptLogicBlockName).FirstOrDefault();
                int rungs = B.Elements.Count();
                _mcodelist2.Add(rungs);
            }
            else
            {
                _mcodelist2.Add(0);
            }
        }



        /// <summary>
        /// Converts Simple values to 4  byte values
        /// </summary>
        /// <param name="data"></param>
        public void FillSimpleByteList(string data)
        {
            intValue = uint.Parse(data);
            _mcodelist2.Add(intValue);
        }

        /// <summary>
        /// Converts Logical Address to 4 byte value For e.g.
        /// </summary>
        /// <param name="data"></param>
        public void FillLogicalByteList(string data)
        {
            if (data.Contains("~"))
            {
                string data1 = data.Substring(1);
                _convertlogicaladdess = xm.GetHexAddress(data1);
            }
            else
            {
                _convertlogicaladdess = xm.GetHexAddress(data);          //Gets the Hexvalue for corresponding address

            }

            uint integerValue = uint.Parse(_convertlogicaladdess, System.Globalization.NumberStyles.HexNumber);
            _mcodelist2.Add((uint)integerValue);
        }

        /// <summary>
        /// Separates the digits and Character For eg. Character => T ,digit => 0
        /// </summary>
        /// <param name="tcvalue"></param>
        /// <returns></returns>
        public string SeparateTCname(string tcvalue)
        {
            string input = tcvalue;
            Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            Match result = re.Match(input);

            string charPart = result.Groups[1].Value;

            string numberPart = result.Groups[2].Value;

            //Converting charpart to single character as per ASCII VALUE OF T IS 84
            char character = charPart.ToCharArray()[0];

            //Convert the the tC_name to hexvalue
            string hex = /*((int)character).ToString("X") +*/ numberPart;

            return hex;
        }

        /// <summary>
        /// Transfering list to notepad file
        /// </summary>
        /// <param name="_mcodelist"></param>
        public void NotePadFile(List<long> _mcodelist2)
        {
            string newfilepath = ProjectHelper.GetfilePath("McodeVersion.txt");
            using (StreamWriter sw = new StreamWriter(newfilepath))
            {
                foreach (long i in _mcodelist2)
                {
                    sw.WriteLine(i);
                }
            }
            //Added _mcodelist2 into _mcodelistForCRC
            _mcodelistForCRC.AddRange(_mcodelist2);
            PerformXOR();
            //Clear List After WriteFile
            _mcodelist2.Clear();
        }

        /// <summary>
        /// Convert Float value to Byte array generate Int Value  12345.6 ==> 1178658406
        /// </summary>
        /// <param name="value"></param>
        public void ConvertFloatValuesToInt(float value)
        {
            float float_variable = value;
            byte[] bytes = BitConverter.GetBytes(float_variable);


            int convertedInt = BitConverter.ToInt32(bytes, 0);              //Float Number has converted to int
            //Console.WriteLine("Converted integer: " + convertedInt);
            if (Datatype == "0004" || Datatype == "000C")
                convertedInt = Convert.ToInt32(value);
            //AS If Number is -ve then Get The Differnce Between Standard Bit & Actual value
            if (value < 0)          //Datatype Is Real ==> Convert Numeric Values to Float
            {
                // int convertedval = Convert.ToInt32(value);
                long bit_value_32 = 4294967296;
                int absIntValue = Math.Abs(convertedInt); // Take the absolute value

                long Real_result = bit_value_32 - absIntValue;

                //  int result = Convert.ToInt64(Real_result); // Cast the long to int
                //    _mcodelist2.Add(intValue);
                _mcodelist2.Add(Real_result);
            }
            else
                _mcodelist2.Add(convertedInt);
        }


        /// <summary>
        /// Digits to byteArray eg => num -> 355 byteArray[99,1,0,0]
        /// </summary>
        /// <param name="numericString"></param>
        public void ConvertNumericToByte(string numericString)
        {
            long decimalValue = long.Parse(numericString);
            _mcodelist2.Add(decimalValue);
        }

        //Calculating CRC for the PLC Synchronization
        private void PerformXOR()
        {
            xm.LoadedProject.McodeCRC = 0;
            if (_mcodelistForCRC.Count == 0)
            {
                xm.LoadedProject.McodeCRC = 0;
            }
            xm.LoadedProject.McodeCRC = _mcodelistForCRC[0];

            for (int i = 1; i < _mcodelistForCRC.Count; i++)
            {
                xm.LoadedProject.McodeCRC ^= _mcodelistForCRC[i];
            }
            _mcodelistForCRC.Clear();
        }

    }

}
