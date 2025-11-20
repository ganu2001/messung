using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestInterpreter.Helpers
{
    public class RungOperations
    {
        public static async void TestOperation(string expression)
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
                rung.Datatype = "Bool";
                rung.EnableType = false;

                var ro = ParseExp.GetRungOperands(rungs[i]);
                rung.Oprator = ro.Oprator;
                rung.Operands = ro.Operands;
                rung.NoOfOperands = ro.NoOfOperands;
                rung.T_CName = ro.T_CName;

                var rungOutput = ParseExp.GetRungOutput(rungs[i]);
                if (rungOutput != null)
                {
                    if (rungOutput.Contains(','))
                    {
                        var outputs = rungOutput.Split(",");
                        rung.Output1 = outputs[0].Trim();
                        rung.Output2 = outputs[1].Trim();
                    }
                    else
                    {
                        rung.Output1 = rungOutput.Trim();
                        rung.Output2 = null;
                    }
                }

                rungList.Add(rung);

                // displaying result for each rung 
                foreach (PropertyInfo prop in rung.GetType().GetProperties())
                {
                    if (prop.Name == "Operands")
                    {
                        for (var j = 0; j < rung.Operands.Count; j++)
                        {
                            foreach (PropertyInfo property in typeof(Operand).GetProperties())
                            {
                                //Console.WriteLine($"{property.Name} {j + 1}: {property.GetValue(rung.Operands[j], null)}");
                            }
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"{prop.Name}: {prop.GetValue(rung, null)}");
                    }
                }

            }

            // getting hex string
            List<string> finalResultString = new List<string>();
            List<string> linesToFile = new List<string>();
            for (var i = 0; i < rungList.Count; i++)
            {
                var stringOutput = "";
                

                //Console.WriteLine('\n');
                var rung = rungList[i];

                var rungNoHex = rung.RungNo.ToString("X2");
                //Console.WriteLine("        Rung No: " + rungNoHex);
                stringOutput += rungNoHex;

                var rungNoBytes = BitConverter.GetBytes(Convert.ToInt32(rungNoHex, 16));
                //Console.WriteLine("        Rung No: " + string.Join("-", rungNoBytes));
                linesToFile.Add(rung.RungNo + ","+ string.Join("-", rungNoBytes));

                var dataTypeHex = DataType.List.Find(d => d.Text == rung.Datatype)?.ID.ToString("X2");
                //Console.WriteLine("      Data type: " + dataTypeHex);
                stringOutput += dataTypeHex;

                //var dataType = DataType.List.Find(d => d.Text == rung.Datatype)?.ID;
                var dataTypeBytes = BitConverter.GetBytes(Convert.ToInt32(dataTypeHex, 16));
                //Console.WriteLine("      Data Type: " + string.Join("-", dataTypeBytes));
                linesToFile.Add(rung.Datatype + "," + (dataTypeBytes != null ? string.Join("-", dataTypeBytes) : "-"));

                var enableTypeHex = rung.EnableType ? "01" : "00";
                //Console.WriteLine("    Enable type: " + enableTypeHex);
                stringOutput += enableTypeHex;

                var enableTypeBytes = BitConverter.GetBytes(Convert.ToInt32(enableTypeHex, 16));
                linesToFile.Add(rung.EnableType + "," + string.Join("-", enableTypeBytes));

                var enableHex = rung.EnableType ? ParseExp.GetAddressHex(rung.Enable) : null;
                //Console.WriteLine("         Enable: " + enableHex);
                stringOutput += enableHex;

                var enableBytes = enableHex != null ? BitConverter.GetBytes(Convert.ToInt32(enableHex, 16)) : null;
                linesToFile.Add(rung.Enable + "," + (enableBytes != null ? string.Join("-", enableBytes) : "-"));

                var operatorHex = rung.Oprator != null ? Instruction.List.Find(ins => ins.Text == rung.Oprator)?.ID.ToString("X4") : null;
                var opcodeHex = operatorHex != null ? operatorHex.Substring(0, 3) + dataTypeHex.Substring(dataTypeHex.Length - 1) : null;
                //Console.WriteLine("         OPCode: " + opcodeHex);
                stringOutput += operatorHex;

                var opCodeBytes = opcodeHex != null ? BitConverter.GetBytes(Convert.ToInt32(opcodeHex, 16)) : null;
                linesToFile.Add(rung.Oprator + "," + (opCodeBytes != null ? string.Join("-", opCodeBytes) : "-"));


                for (var j = 0; j < 8; j++)
                {
                    if (j >= rung.Operands.Count)
                    {
                        //Console.WriteLine("Type of Operand: -");
                        //Console.WriteLine("             OP: -");
                        linesToFile.Add(",-");
                        linesToFile.Add(",-");
                    } else
                    {
                        string typeOfOperandHex = null;
                        string opHex = null;
                        byte[] typeOfOpBytes = null;
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
                        
                        //Console.WriteLine("Type of Operand: " + typeOfOperandHex);
                        //Console.WriteLine("            OP" + (j + 1).ToString() + ": " + opHex);
                        stringOutput += typeOfOperandHex;
                        stringOutput += opHex;

                        typeOfOpBytes = BitConverter.GetBytes(Convert.ToInt32(typeOfOperandHex, 16));
                        linesToFile.Add(rung.Operands[j].TypeOfOperand + "," + (typeOfOpBytes != null ? string.Join("-", typeOfOpBytes) : "-"));
                        opBytes = BitConverter.GetBytes(Convert.ToInt32(opHex, 16));
                        linesToFile.Add(rung.Operands[j].OP + "," + (opBytes != null ? string.Join("-", opBytes) : "-"));


                    }
                }

                var noOfOperandHex = rung.NoOfOperands.ToString("X2");
                //Console.WriteLine("  No of Operand: " + noOfOperandHex);
                stringOutput += noOfOperandHex;

                var noOfOperandBytes = BitConverter.GetBytes(Convert.ToInt32(noOfOperandHex, 16));
                linesToFile.Add(rung.NoOfOperands + "," + (noOfOperandBytes != null ? string.Join("-", noOfOperandBytes) : "-"));


                var tcName = rung.T_CName != null ? rung.T_CName : null;
                //Console.WriteLine("       T_C Name: " + tcName);
                stringOutput += tcName;

                var tcNameBytes = tcName != null ? Encoding.ASCII.GetBytes(tcName) : null;
                linesToFile.Add(rung.T_CName + "," + (tcNameBytes != null ? string.Join("-", tcNameBytes) : "-"));

                var output1Hex = rung.Output1 != null ? ParseExp.GetAddressHex(rung.Output1) : null;
                //Console.WriteLine("       Output 1: " + output1Hex);
                stringOutput += output1Hex;

                var output1Bytes = output1Hex != null ? BitConverter.GetBytes(Convert.ToInt32(output1Hex, 16)) : null;
                linesToFile.Add(rung.Output1 + "," + (output1Bytes != null ? string.Join("-", output1Bytes) : "-"));

                var output2Hex = rung.Output2 != null ? ParseExp.GetAddressHex(rung.Output2) : null;
                //Console.WriteLine("       Output 2: " + output2Hex);
                stringOutput += output2Hex;

                var output2Bytes = output2Hex != null ? BitConverter.GetBytes(Convert.ToInt32(output2Hex, 16)) : null;
                linesToFile.Add(rung.Output2 + "," + (output2Bytes != null ? string.Join("-", output2Bytes) : "-"));

                finalResultString.Add(stringOutput);


                
            }
            String[] str = linesToFile.ToArray();
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
