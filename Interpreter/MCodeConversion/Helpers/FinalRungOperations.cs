using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.MCodeConversion.Helpers
{
    public class FinalRungOperations
    {
        public static (List<byte>, int, List<string>) RungExpressionToBytes(string expression, int prevRungsCount)
        {
            // sample
            //expression = "Q0:000.05=(((I1:000.04 AND (I1:000.05 OR I1:001.02 OR I1:001.07)) AND (I1:000.02) AND (F2:004 OR (I1:001.14 OR I1:002.13))) AND NOTF2:008 );";

            List<string> result = new List<string>();

            //VariableList vl = new VariableList();
            //List<string> variableList = vl.Variables;
            //ValidateMessung vdm = new ValidateMessung(variableList, expression);
            //Console.WriteLine(vdm.CheckExpressionSyntax());

            // parsing given exp into rungs
            List<string> rungs = ParseExp.GetRungs(expression);

            List<Rung> rungList = new List<Rung>();

            // getting details from each rung
            for (var i = 0; i < rungs.Count; i++)
            {
                //Console.WriteLine('\n');

                var rung = new Rung();
                rung.Exp = rungs[i];
                rung.RungNo = i + 1;

                var ro = ParseExp.GetRungOperands(rungs[i]);
                rung.Oprator = ro.Oprator;
                rung.Operands = ro.Operands;
                rung.NoOfOperands = ro.NoOfOperands;
                rung.TC_Name = ro.T_CName;

                rung.Datatype = ro.DataType;

                var rungOutput = ParseExp.GetRungOutput(rungs[i]);
                if (rungOutput != null)
                {
                    if (rungOutput.Contains(','))
                    {
                        var outputs = rungOutput.Split(',');
                        rung.Output1 = outputs[0].Trim();
                        rung.Output2 = outputs[1].Trim();
                    }
                    else
                    {
                        rung.Output1 = rungOutput.Trim();
                        rung.Output2 = null;
                    }
                }

                if (i < (rungs.Count - 1) && rungs[i + 1].Contains("En"))
                {
                    rung.EnableType = true;
                    rung.Enable = rungs[i + 1].Split(' ')[0];
                    i++;
                } else
                {
                    rung.EnableType = false;
                }

                rungList.Add(rung);

                // displaying result for each rung 
                //foreach (PropertyInfo prop in rung.GetType().GetProperties())
                //{
                //    if (prop.Name == "Operands")
                //    {
                //        for (var j = 0; j < rung.Operands.Count; j++)
                //        {
                //            foreach (PropertyInfo property in typeof(Operand).GetProperties())
                //            {
                //                //Console.WriteLine($"{property.Name} {j + 1}: {property.GetValue(rung.Operands[j], null)}");
                //            }
                //        }
                //    }
                //    else
                //    {
                //        //Console.WriteLine($"{prop.Name}: {prop.GetValue(rung, null)}");
                //    }
                //}

            }

            // getting hex string
            List<string> finalResultString = new List<string>();
            List<string> linesToFile = new List<string>();
            List<byte> rungByteArray = new List<byte>();
            for (var i = 0; i < rungList.Count; i++)
            {
                string emptyValueHex = Convert.ToByte('-').ToString("x2");
                string emptyValueHex2 = Convert.ToByte('-').ToString("x4");
                string emptyValueHex4 = Convert.ToByte('-').ToString("x8");

                byte emptyValueByte1 = Convert.ToByte('-');
                byte[] emptyValueByte2 = BitConverter.GetBytes(Convert.ToInt16('-'));
                byte[] emptyValueByte4 = BitConverter.GetBytes(Convert.ToInt32('-'));


                var rung = rungList[i];

                var rungNoHex = (i + 1 + prevRungsCount).ToString("X2");
                var rungNoBytes = BitConverter.GetBytes(Convert.ToInt16(rungNoHex, 16));
                linesToFile.Add(i + 1 + "," + rungNoHex);
                rungByteArray.AddRange(rungNoBytes);

                var dataTypeHex = rung.Datatype != null ? DataType.List.Find(d => d.Text == rung.Datatype)?.ID.ToString("X2") : null;
                var dataTypeBytes = dataTypeHex != null ? BitConverter.GetBytes(Convert.ToInt16(dataTypeHex, 16)) : null;
                linesToFile.Add(rung.Datatype + "," + (dataTypeBytes != null ? dataTypeHex : emptyValueHex2));
                rungByteArray.AddRange(dataTypeBytes ?? emptyValueByte2);

                var enableTypeHex = rung.EnableType ? "01" : "00";
                var enableTypeBytes = byte.Parse(enableTypeHex);
                linesToFile.Add(rung.EnableType + "," + enableTypeHex);
                rungByteArray.Add(enableTypeBytes);

                var enableHex = rung.EnableType ? ParseExp.GetAddressHex(rung.Enable) : null;
                var enableBytes = enableHex != null ? BitConverter.GetBytes(Convert.ToInt32(enableHex, 16)) : null;
                linesToFile.Add(rung.Enable + "," + (enableBytes != null ? enableHex : emptyValueHex4));
                rungByteArray.AddRange(enableBytes ?? emptyValueByte4);

                var operatorHex = rung.Oprator != null ? Instruction.List.Find(ins => ins.Text == rung.Oprator)?.ID.ToString("X4") : null;
                var opcodeHex = operatorHex != null ? operatorHex.Substring(0, 3) + dataTypeHex.Substring(dataTypeHex.Length - 1) : null;
                var opCodeBytes = opcodeHex != null ? BitConverter.GetBytes(Convert.ToInt16(opcodeHex, 16)) : null;
                linesToFile.Add(rung.Oprator + "," + (opCodeBytes != null ? opcodeHex : emptyValueHex2));
                rungByteArray.AddRange(opCodeBytes ?? emptyValueByte2);


                for (var j = 0; j < 8; j++)
                {
                    if (j >= rung.Operands.Count)
                    {
                        linesToFile.Add("," + emptyValueHex);
                        linesToFile.Add("," + emptyValueHex4);
                        rungByteArray.Add(emptyValueByte1);
                        rungByteArray.AddRange(emptyValueByte4);
                    }
                    else
                    {
                        string typeOfOperandHex = "01";
                        string opHex = null;
                        byte typeOfOpBytes;
                        byte[] opBytes = null;
                        if (rung.Operands[j].TypeOfOperand == "Numeric")
                        {
                            typeOfOperandHex = "03";
                            opHex = int.Parse(rung.Operands[j].OP).ToString("X2");
                        }
                        else if (rung.Operands[j].TypeOfOperand == "Negation")
                        {
                            typeOfOperandHex = "02";
                            opHex = ParseExp.GetAddressHex(rung.Operands[j].OP.Replace("NOT", string.Empty));
                        }
                        else if (rung.Operands[j].TypeOfOperand == "Normal")
                        {
                            typeOfOperandHex = "01";
                            opHex = ParseExp.GetAddressHex(rung.Operands[j].OP);
                        }

                        typeOfOpBytes = byte.Parse(typeOfOperandHex);
                        linesToFile.Add(rung.Operands[j].TypeOfOperand + "," + (typeOfOpBytes!= null ? typeOfOperandHex : emptyValueHex));
                        opBytes = BitConverter.GetBytes(Convert.ToInt32(opHex, 16));
                        linesToFile.Add(rung.Operands[j].OP + "," + (opBytes != null ? opHex : emptyValueHex4));
                        rungByteArray.Add(typeOfOpBytes != null ? typeOfOpBytes : emptyValueByte1);
                        rungByteArray.AddRange(opBytes);

                    }
                }

                var noOfOperandHex = rung.NoOfOperands.ToString("X2");
                var noOfOperandBytes = byte.Parse(noOfOperandHex);
                linesToFile.Add(rung.NoOfOperands + "," + (noOfOperandBytes != null ? noOfOperandHex : emptyValueHex));
                rungByteArray.Add(noOfOperandBytes);


                var tcName = rung.TC_Name != null ? rung.TC_Name : null;
                var tcNameBytes = tcName != null ? Encoding.ASCII.GetBytes(tcName) : null;
                //var tcNameBytes = tcName != null ? BitConverter.GetBytes(Convert.ToInt32(tcName)) : null;
                linesToFile.Add(rung.TC_Name + "," + (tcNameBytes != null ? tcName : emptyValueHex4));
                rungByteArray.AddRange(tcNameBytes ?? emptyValueByte4);

                var output1Hex = rung.Output1 != null ? ParseExp.GetAddressHex(rung.Output1) : null;
                var output1Bytes = output1Hex != null ? BitConverter.GetBytes(Convert.ToInt32(output1Hex, 16)) : null;
                linesToFile.Add(rung.Output1 + "," + (output1Bytes != null ? output1Hex : emptyValueHex4));
                rungByteArray.AddRange(output1Bytes ?? emptyValueByte4);

                var output2Hex = rung.Output2 != null ? ParseExp.GetAddressHex(rung.Output2) : null;
                var output2Bytes = output2Hex != null ? BitConverter.GetBytes(Convert.ToInt32(output2Hex, 16)) : null;
                linesToFile.Add(rung.Output2 + "," + (output2Bytes != null ? output2Hex : emptyValueHex4));
                rungByteArray.AddRange(output2Bytes ?? emptyValueByte4);

            }
            return (rungByteArray, rungList.Count, linesToFile);

            //string path = @"D:\c#\ConsoleAppMessung\ConsoleAppMessung\TestInterpreter\newFile.txt";

            //File.WriteAllLines(path, str);

            // final string in bytes
            //for (var i = 0; i < finalResultString.Count; i++)
            //{
            //    Console.WriteLine(finalResultString[i]);
            //}

            //var b = "223A8400";
            //var a = Convert.ToInt32(b, 16);
            //Console.WriteLine(a);
            //var m = 220;
            //var n = m.ToString("X");
            //Console.WriteLine(n);

            //Console.WriteLine(ParseExp.GetAddressHex("I1:001.04"));
        }
    }
}
