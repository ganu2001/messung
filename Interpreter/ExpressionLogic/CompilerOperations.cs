using Interpreter.MCodeConversion;
using Interpreter.MCodeConversion.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS2000.Core;
using XMPS2000.Core.App;

namespace Interpreter.ExpressionLogic
{
    public class CompilerOperations
    {
        public static (bool, Dictionary<string, string[]>) Compile(XMPS xm)
        {
            // validate all block files in the project folder, i.e. all the src files
            var projectTags = xm.LoadedProject.Tags;

            Dictionary<string, string[]> rungExpressions = new Dictionary<string, string[]>();

            var isCompilationError = false;
            Dictionary<string, string[]> compilationErrors = new Dictionary<string, string[]>();

            foreach (string srcFilePath in Directory.GetFiles(Path.GetDirectoryName(xm.CurrentProjectData.ProjectPath), "*.src", SearchOption.AllDirectories))
            {
                // 1. the file name should exist in the project file?

                string text = File.ReadAllText(srcFilePath);
                foreach (var tag in projectTags)
                {
                    if (tag.Tag != null && tag.Tag != "" && text.Contains(tag.Tag))
                    {
                        text = text.Replace(tag.Tag, tag.LogicalAddress);
                    }
                }
                //string[] lines = File.ReadAllLines(srcFilePath);
                string[] lines = text.Replace("\r", string.Empty).Split('\n');
                List<List<string>> rungs = new List<List<string>>();

                // getting rungs as list from the given text file
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    if (line.Contains("SOR"))
                    {
                        List<string> arr = new List<string>();
                        for (int j = i + 1; j < lines.Length; j++)
                        {
                            if (lines[j].Contains("EOR"))
                            {
                                i = j - 1;
                                rungs.Add(arr);
                                break;
                            }
                            else
                            {
                                arr.Add(lines[j]);
                            }

                        }
                    }

                }
                List<string> expressions = new List<string>();

                var udfs = xm.LoadedProject.Blocks.Where(u => u.Type == "UserFunctionBlock");
                var udfNames = udfs.Select(u => u.Name).ToList();

                List<string> compileErrors = new List<string>();

                for (int i = 0; i < rungs.Count; i++)
                {
                    //(bool isCompiled, string expression, List<Exception> errors) = RungOperations.Formula(rungs[i], udfNames);
                    (bool isCompiled, string expression, List<Exception> errors) = RungToExpression.GetExpression(rungs[i], udfNames);
                    if (!isCompiled)
                    {
                        isCompilationError = true;
                        foreach (var err in errors)
                        {
                            compileErrors.Add("Rung #" + (i + 1) + ": " + err.Message);
                        }
                    }
                    if (!isCompilationError)
                    {
                        //expressions.Add("Rung #" + (i + 1) + ": " + expression);
                        expressions.Add(expression);
                    }
                }

                if (!isCompilationError)
                {
                    var rungExpression = expressions.ToArray();

                    rungExpressions.Add(Path.GetFileName(srcFilePath).Replace(".src", string.Empty), rungExpression);
                }
                else
                {
                    compilationErrors.Add(Path.GetFileName(srcFilePath).Replace(".src", string.Empty), compileErrors.ToArray());
                }
            }
            if (isCompilationError)
            {
                return (false, compilationErrors);
            }

            #region create rungs file for each block
            foreach(var block in rungExpressions)
            {
                if (block.Value.Length > 0)
                {
                    List<string> rungs = new List<string> { };
                    foreach (string exp in block.Value)
                    {
                        rungs.Add("Expression: " + exp);
                        rungs.AddRange(ParseExp.GetRungs(exp));
                    }
                    var blockRungsfilePath = Path.Combine(Path.GetDirectoryName(xm.CurrentProjectData.ProjectPath), block.Key + "Rungs.txt");
                    File.WriteAllLines(blockRungsfilePath, rungs);
                }
            }
            #endregion

            return (true, rungExpressions);
        }

        public static (bool, Dictionary<string, string[]>) Run(XMPS xm)
        {
            // catch and return errors if any
            (bool compileSuccess, var compileOutput) = Compile(xm);

            Dictionary<string, string[]> rungExpressions;
            Dictionary<string, string[]> compileErrors;

            if (compileSuccess)
            {
                rungExpressions = compileOutput;
                compileErrors = null;
            }
            else
            {
                rungExpressions = null;
                compileErrors = compileOutput;
                return (compileSuccess, compileErrors);
            }

            // interpreter
            if (compileSuccess)
            {
                // replace UDFs with actual expression
                Dictionary<string, string[]> finalrungExpressions = new Dictionary<string, string[]> { };
                var UDFs = xm.LoadedProject.Blocks.Where(u => u.Type == "UserFunctionBlock");
                var UDFnames = UDFs.Select(u => u.Name).ToList();
                foreach (var value in rungExpressions)
                {
                    if (value.Value.Length > 0)
                    {
                        List<string> val = new List<string>();
                        foreach (var line in value.Value)
                        {
                            var changingLine = false;
                            foreach (var udfname in UDFnames)
                            {
                                if (line.Contains(udfname))
                                {
                                    // replace UDF name with the expression
                                    var thisLine = line;
                                    var start = line.IndexOf(udfname);
                                    var end = line.IndexOf(udfname);
                                    while (line[start] != '(')
                                    {
                                        start--;
                                    }
                                    while (line[end] != ')')
                                    {
                                        end++;
                                    }
                                    changingLine = true;
                                    thisLine = line.Replace(line.Substring(start, end - start + 1), "(" + rungExpressions[udfname + ".src"][0] + ")");
                                    val.Add(thisLine);
                                    break;
                                }
                            }
                            if (!changingLine)
                            {
                                val.Add(line);
                            }
                        }
                        finalrungExpressions[value.Key] = val.ToArray();
                    }
                }

                // interpret only main file expression
                var mainFilePath = Path.Combine(Path.GetDirectoryName(xm.CurrentProjectData.ProjectPath), "Main.txt");
                if (File.Exists(mainFilePath))
                {
                    string[] mainFileLines = File.ReadAllLines(mainFilePath);
                    List<byte> mainByteArray = new List<byte>();
                    List<byte> rungsByteArray = new List<byte>();
                    int totalNoOfRungs = 0;
                    foreach (var blockname in mainFileLines)
                    {
                        List<string> rungMCode = new List<string> { };
                        if (finalrungExpressions.ContainsKey(blockname))
                        {
                            foreach (var line in finalrungExpressions[blockname])
                            {
                                (List<byte> expToBytes, int noOfRungs, List<string> expToHex) = FinalRungOperations.RungExpressionToBytes(line, totalNoOfRungs);
                                rungsByteArray.AddRange(expToBytes);
                                totalNoOfRungs += noOfRungs;
                                rungMCode.AddRange(expToHex);
                            }
                            // file with hex values
                            var blockMCodefilePath = Path.Combine(Path.GetDirectoryName(xm.CurrentProjectData.ProjectPath), blockname + "Hex.txt");
                            File.WriteAllLines(blockMCodefilePath, rungMCode);
                        }
                    }

                    mainByteArray.Add(Convert.ToByte('$'));
                    mainByteArray.AddRange(BitConverter.GetBytes(Convert.ToInt16(totalNoOfRungs)));
                    mainByteArray.AddRange(rungsByteArray);
                    mainByteArray.Add(Convert.ToByte('#'));


                    #region create .bin file
                    BinaryWriter bw = new BinaryWriter(File.Open(Path.Combine(Path.GetDirectoryName(xm.CurrentProjectData.ProjectPath), "Main.bin"), FileMode.Create));
                    bw.Write(mainByteArray.ToArray());
                    bw.Flush();
                    bw.Close();
                    #endregion
                }
            }
            return (compileSuccess, rungExpressions);
        }
    }
}
