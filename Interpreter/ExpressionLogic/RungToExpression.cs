using Interpreter.MCodeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.ExpressionLogic
{
    public class RungToExpression
    {
        public static (bool, string, List<Exception>) GetExpression(List<string> rungLines, List<string> udfNames)
        {
            var errorFound = false;
            List<Exception> compilationErrors = new List<Exception>();

            var finalExpression = "";

            var prevXIC = false;
            for (var i = 0; i < rungLines.Count; i++)
            {
                var line = rungLines[i];

                if (line.Contains("XIC") || line.Contains("XIO"))
                {
                    (bool isCompileSuccess, string expression, List<Exception> errors) = GetXICXIOExpression(line);
                    if (isCompileSuccess)
                    {
                        if (prevXIC && finalExpression != "")
                        {
                            finalExpression = "(" + finalExpression + ")" + " AND " + expression;
                        }
                        else
                        {
                            finalExpression += expression;
                        }
                    }
                    else
                    {
                        errorFound = true;
                        compilationErrors.AddRange(errors);
                    }
                    prevXIC = true;
                }
                else if (line.Contains("BST"))
                {
                    List<List<string>> orBranches = new List<List<string>>();
                    List<string> orLines = new List<string>();

                    var BSTCount = 1;
                    var BNDCount = 0;

                    for (var j = i + 1; j < rungLines.Count; j++)
                    {
                        if (rungLines[j].Contains("BST"))
                        {
                            BSTCount++;
                        }
                        if (rungLines[j].Contains("BND"))
                        {
                            BNDCount++;
                            if (BSTCount == BNDCount)
                            {
                                orBranches.Add(orLines);
                                i = j;
                                break;
                            }
                        }
                        if (rungLines[j].Contains("NXB"))
                        {
                            if (BSTCount - BNDCount == 1)
                            {
                                orBranches.Add(orLines);
                                orLines = new List<string>();
                            } else
                            {
                                orLines.Add(rungLines[j]);
                            }
                        } else
                        {
                            orLines.Add(rungLines[j]);
                        }
                    }
                    var firstBranch = true;
                    var orExpression = "";
                    foreach (List<string> ob in orBranches)
                    {
                        (bool isCompileSuccess, string expression, List<Exception> errors) = GetExpression(ob, udfNames);
                        if (isCompileSuccess)
                        {
                            if (firstBranch)
                            {
                                orExpression = "(" + expression + ")";
                                firstBranch = false;
                            } else
                            {
                                orExpression += " OR (" + expression + ")";
                            }
                        }
                        else
                        {
                            errorFound = true;
                            compilationErrors.AddRange(errors);
                        }
                    }
                    if (!errorFound)
                    {
                        if (finalExpression == "")
                        {
                            finalExpression = orExpression;
                        }
                        else if (orExpression != "")
                        {
                            finalExpression = "(" + finalExpression + ") AND (" + orExpression + ")";
                        }
                    }
                    prevXIC = true;
                }
                else if (line.Contains("FB"))
                {
                    List<string> fbLines = new List<string>();
                    for (var j = i; j < i+3; j++)
                    {
                        fbLines.Add(rungLines[j]);
                    }
                    (bool isCompileSuccess, string expression, bool enable, List<Exception> errors) = GetFBExpression(fbLines, udfNames);
                    if (isCompileSuccess)
                    {
                        if (finalExpression == "")
                        {
                            finalExpression += "(" + expression + ")";
                        }
                        else
                        {
                            if (enable)
                            {
                                finalExpression = "(" + finalExpression + ")" + " En " + "(" + expression + ")";
                            } else
                            {
                                finalExpression = "(" + finalExpression + ")" + " AND " + "(" + expression + ")";
                            }
                        }
                    }
                    else
                    {
                        errorFound = true;
                        compilationErrors.AddRange(errors);
                    }
                    prevXIC = true;
                }
                else if (line.Contains("OTE") || line.Contains("OTO"))
                {
                    (bool isCompileSuccess, string expression, List<Exception> errors) = GetOTEExpression(line);
                    if (isCompileSuccess)
                    {
                        if (line.Contains("OTO"))
                        {
                            finalExpression = expression + " = (NOT(" + finalExpression + "))";
                        }
                        else
                        {
                            finalExpression = expression + " = (" + finalExpression + ")";
                        }
                    }
                    else
                    {
                        errorFound = true;
                        compilationErrors.AddRange(errors);
                    }
                }
            }
            return (!errorFound, finalExpression, compilationErrors);
        }
        private static (bool, string, List<Exception>) GetXICXIOExpression(string exp)
        {
            var errorFound = false;
            List<Exception> compilationErrors = new List<Exception>();

            var inp = exp.Split('*')[1];
            Addresses adr = new Addresses();
            List<string> validInputBitAddress = new List<string> { };
            validInputBitAddress.AddRange(adr.InputBitAddress);
            validInputBitAddress.AddRange(adr.OutputBitAddress);
            validInputBitAddress.AddRange(adr.FlagsMemoryBitAddress);
            if (inp == "" || !validInputBitAddress.Contains(inp))
            {
                errorFound = true;
                compilationErrors.Add(new CompilerException("Invalid logical input address '" + inp + "'"));
            }
            if (errorFound)
            {
                return (!errorFound, null, compilationErrors);
            }
            if (exp.Contains("XIO"))
            {
                return (!errorFound, "NOT" + inp, null);
            }
            return (!errorFound, inp, null);
        }
        private static (bool, string, List<Exception>) GetOTEExpression(string exp)
        {
            var errorFound = false;
            List<Exception> compilationErrors = new List<Exception>();
            string expression = null;

            var inp = exp.Split('*')[1];
            Addresses adr = new Addresses();
            List<string> validOutputBitAddress = new List<string> { };
            validOutputBitAddress.AddRange(adr.OutputBitAddress);
            if (inp == "" || !validOutputBitAddress.Contains(inp))
            {
                errorFound = true;
                compilationErrors.Add(new CompilerException("Invalid rung output address"));
            }
            else
            {
                expression = inp;
            }
            if (errorFound)
            {
                return (!errorFound, null, compilationErrors);
            }
            return (!errorFound, expression, null);
        }
        private static (bool, string, bool, List<Exception>) GetFBExpression(List<string> fbLines, List<string> udfNames)
        {
            var errorFound = false;
            List<Exception> compilationErrors = new List<Exception>();
            string fbExpression = null;
            string optor = null;
            bool enable = false;
            var optorLine = fbLines[0];
            var inputOperands = fbLines[1].Split(new[] { "*", "(", ")", ",", " " }, StringSplitOptions.None).Where(i => i != "").ToArray();
            var outputOperands = fbLines[2].Split(new[] { "*", "(", ")", ";", " " ,","}, StringSplitOptions.None).Where(i => i != "").ToArray();
            OperatorList ol = new OperatorList();
            
            List<string> logicalBlockOperators = ol.LogicalBlockOperatorsList;
            if (optor == null)
            {
                foreach (string optr in logicalBlockOperators)
                {
                    if (optorLine.Contains(optr))
                    {
                        optor = optr;
                        if (optor == "Not")
                        {
                            string logicalExpression = optor;
                            fbExpression = logicalExpression;
                            break;
                        } else
                        {
                            if (optor == "OR")
                            {
                                optor = "||";
                            }
                            string logicalExpression = optor + " " + string.Join(" ", inputOperands);
                            (bool isCompileSuccess, List<Exception> errors) = ExceptionCheck.LogicalBlockExceptionCheck(logicalExpression);
                            if (!isCompileSuccess)
                            {
                                errorFound = true;
                                compilationErrors.AddRange(errors);
                            }
                            else
                            {
                                fbExpression = logicalExpression;
                            }
                            break;
                        }
                    }
                }
            }
            List<string> arithmeticOperators = ol.ArithmeticOperatorsList;
            if (optor == null)
            {
                foreach (string optr in arithmeticOperators)
                {
                    if (optorLine.Contains(optr))
                    {
                        optor = optr;
                        if (optor == "***")
                        {
                            optor = "*";
                        }
                        enable = true;
                        string arithmeticExpression = string.Join(" ", outputOperands) + " = " + string.Join(" " + optor + " ", inputOperands);
                        (bool isCompileSuccess, List<Exception> errors) = ExceptionCheck.ArithmeticBlockExceptionCheck(arithmeticExpression);
                        if (!isCompileSuccess)
                        {
                            errorFound = true;
                            compilationErrors.AddRange(errors);
                        }
                        else
                        {
                            fbExpression = arithmeticExpression;
                        }
                        break;
                    }
                }
            }
            List<string> timerOperators = ol.TimerOperatorsList;
            if (optor == null)
            {
                var optorText = fbLines[0].Split('*')[1];
                foreach (string optr in timerOperators)
                {
                    if (optorLine.Contains(optr))
                    {
                        optor = optr;
                        string timerExpression = string.Join(" ", outputOperands) + " = " + optorText + " " + string.Join(" ", inputOperands);
                        (bool isCompileSuccess, List<Exception> errors) = ExceptionCheck.TimerBlockExceptionCheck(timerExpression);
                        if (!isCompileSuccess)
                        {
                            errorFound = true;
                            compilationErrors.AddRange(errors);
                        }
                        else
                        {
                            fbExpression = timerExpression;
                        }
                        break;
                    }
                }
            }
            List<string> compareOperators = ol.CompareOperatorsList;
            if (optor == null)
            {
                foreach (string optr in compareOperators)
                {
                    if (optorLine.Contains(optr))
                    {
                        optor = optr;
                        enable = true;
                        if (optor == "=")
                        {
                            optor = "==";
                        }
                        //string compareExpression = string.Join(" ", outputOperands) + string.Join(" " + optor + " ", inputOperands);
                        string compareExpression = string.Join(" " + optor + " ", inputOperands);
                        (bool isCompileSuccess, List<Exception> errors) = ExceptionCheck.CompareBlockExceptionCheck(compareExpression);
                        if (!isCompileSuccess)
                        {
                            errorFound = true;
                            compilationErrors.AddRange(errors);
                        }
                        else
                        {
                            fbExpression = compareExpression;
                        }
                        break;
                    }
                }
            }
            List<string> limitOperators = ol.LimitOperatorsList;
            if (optor == null)
            {
                foreach (string optr in limitOperators)
                {
                    if (optorLine.Contains(optr))
                    {
                        optor = optr;
                        enable = true;
                        string limitExpression = string.Join(" ", outputOperands) + " " + optor +  " " + string.Join(" ", inputOperands);
                        (bool isCompileSuccess, List<Exception> errors) = ExceptionCheck.LimitBlockExceptionCheck(limitExpression);
                        if (!isCompileSuccess)
                        {
                            errorFound = true;
                            compilationErrors.AddRange(errors);
                        }
                        else
                        {
                            fbExpression = limitExpression;
                        }
                        break;
                    }
                }
            }
            List<string> counterOperators = ol.CounterOperatorsList;
            if (optor == null)
            {
                var optorText = fbLines[0].Split('*')[1];
                foreach (string optr in counterOperators)
                {
                    if (optorLine.Contains(optr))
                    {
                        optor = optr;
                        string counterExpression = string.Join(" ", outputOperands) + " = " + string.Join(" " + optorText + " ", inputOperands);
                        (bool isCompileSuccess, List<Exception> errors) = ExceptionCheck.CounterBlockExceptionCheck(counterExpression);
                        if (!isCompileSuccess)
                        {
                            errorFound = true;
                            compilationErrors.AddRange(errors);
                        }
                        else
                        {
                            fbExpression = counterExpression;
                        }
                        break;
                    }
                }
            }
            List<string> flipflopoperators = ol.FlipFlopOperatorsList;
            if (optor == null)
            {
                var optorText = fbLines[0].Split('*')[1];
                foreach (string optr in flipflopoperators)
                {
                    if (optorLine.Contains(optr))
                    {
                        optor = optr;
                        //string flipflopExpression = string.Join(" ", outputOperands) + string.Join(" " + optor + " ", inputOperands);
                        string flipflopExpression = optorText + " " + string.Join(" ", inputOperands);
                        (bool isCompileSuccess, List<Exception> errors) = ExceptionCheck.FlipFlopBlockExceptionCheck(flipflopExpression);
                        if (!isCompileSuccess)
                        {
                            errorFound = true;
                            compilationErrors.AddRange(errors);
                        }
                        else
                        {
                            fbExpression = flipflopExpression;
                        }
                        break;
                    }
                }
            }
            List<string> bitshiftoperators = ol.BitShiftOperatorsList;
            if (optor == null)
            {
                foreach (string optr in bitshiftoperators)
                {
                    if (optorLine.Contains(optr))
                    {
                        optor = optr;
                        enable = true;
                        string bitshiftExpression = string.Join(" ", outputOperands) + " = " + optor + " " + string.Join(" ", inputOperands);
                        (bool isCompileSuccess, List<Exception> errors) = ExceptionCheck.BitShiftBlockExceptionCheck(bitshiftExpression);
                        if (!isCompileSuccess)
                        {
                            errorFound = true;
                            compilationErrors.AddRange(errors);
                        }
                        else
                        {
                            fbExpression = bitshiftExpression;
                        }
                        break;
                    }
                }
            }
            // udf
            if (optor == null)
            {
                foreach (string optr in udfNames)
                {
                    if (optorLine.Contains(optr))
                    {
                        optor = optr;
                        var udfExpressionSplit = optorLine.Split('*');
                        fbExpression = udfExpressionSplit[1];
                        break;
                    }
                }
            }
            if (optor == null)
            {
                errorFound = true;
                compilationErrors.Add(new CompilerException("Function block operator error"));
            }
            return (!errorFound, fbExpression, enable, compilationErrors);
        }
    }
}
