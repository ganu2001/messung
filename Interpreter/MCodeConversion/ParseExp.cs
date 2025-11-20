using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.MCodeConversion
{
    public class ParseExp
    {
        public static List<string> GetRungs(string exp)
        {
            //string expression = "Q0:000.05=(((I1:000.04 AND I1:000.05) OR I1:001.02 OR I1:001.07) AND (I1:000.02) AND (F2:004 OR I1:001.14 OR I1:002.13) AND F2:008 );";

            //Console.WriteLine("Initial expression: " + exp);

            var myExp = exp.Replace(";", string.Empty);

            Addresses vl = new Addresses();
            List<string> placeHolders = vl.AutoMemoryFlagsBitAddress;

            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> operatorList = ol.OperatorsList;
            List<string> logicalOperatorsList = ol.LogicalOperatorsList;
            List<string> logicalBlockOperatorsList = ol.LogicalBlockOperatorsList;
            List<string> arithmeticOperatorsList = ol.ArithmeticOperatorsList;
            List<string> bitshiftOperatorsList = ol.BitShiftOperatorsList;
            List<string> limitOperatorsList = ol.LimitOperatorsList; 
            List<string> timerOperatorsList = ol.TimerOperatorsList;
            List<string> compareOperatorsList = ol.CompareOperatorsList;
            List<string> counterOperatorsList = ol.CounterOperatorsList;
            List<string> flipflopOperatorsList = ol.FlipFlopOperatorsList;

            List<string> rungs = new List<string>();

            var i = -1;
            var placeholderCount = 0;
            while (myExp.Contains('(') || myExp.Contains(')'))
            {
                i++;
                if (myExp[i] == ' ')
                {
                    continue;
                }
                if (myExp[i] == '(')
                {
                    for (int j = i + 1; j < myExp.Length; j++)
                    {
                        if (myExp[j] == '(')
                        {
                            break;
                        }
                        else if (myExp[j] == ')')
                        {
                            var str = myExp.Substring(i + 1, j - i - 1);
                            var trimmedStr = str.Trim();
                            var flag = false;
                            if (trimmedStr.Contains("En"))
                            {
                                var eno = trimmedStr.Split(' ')[0];
                                foreach (string s in compareOperatorsList)
                                {
                                    if (rungs[rungs.Count - 1].Contains(s))
                                    {
                                        eno = trimmedStr.Split(' ')[2];
                                        break;
                                    }
                                }
                                myExp = myExp.Replace(myExp.Substring(i, str.Length + 2), eno);
                                rungs.Add(trimmedStr);
                                goto done;
                            }
                            foreach(string s in operatorList)
                            {
                                if (trimmedStr.Contains(s))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                myExp = myExp.Replace(myExp.Substring(i, str.Length + 2), trimmedStr);
                            }
                            else
                            {
                                // handling arithmetic functional blocks
                                foreach (string s in arithmeticOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var outputExpression = trimmedStr.Split('=')[0];
                                        var inputExpression = trimmedStr.Split('=')[1];
                                        var p = outputExpression;
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p.Trim() + " = " + inputExpression.Trim());
                                        goto done;
                                    }
                                }
                                // handling general inputs, also logical operations
                                foreach (string s in logicalBlockOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var input1 = "";
                                        var k1 = i - 1;
                                        while (k1 > 0 && myExp[k1] != ' ')
                                        {
                                            k1--;
                                        }
                                        var k2 = k1;
                                        while (k2 > 0 && myExp[k2] != '(')
                                        {
                                            k2--;
                                        }
                                        var subExp = myExp.Substring(k2 + 1, k1 - k2 - 5);
                                        input1 = subExp.Trim();
                                        var p = placeHolders[placeholderCount];
                                        placeholderCount++;
                                        var inputExp = trimmedStr.Split(' ');
                                        var inputOperands = inputExp.Where(io => io != "" && io != s).ToArray();
                                        var a = myExp.Substring(k2 + 1, j - k2);
                                        myExp = myExp.Replace(a, p);
                                        if (s == "Not")
                                        {
                                            rungs.Add(p + " = NOT" + input1);
                                        } else
                                        {
                                            if (s == "||")
                                            {
                                                rungs.Add(p + " = " + input1 + " " + "OR" + " " + string.Join(" OR ", inputOperands));
                                            } else
                                            {
                                                rungs.Add(p + " = " + input1 + " " + s + " " + string.Join(" " + s + " ", inputOperands));
                                            }
                                            
                                        }
                                        goto done;
                                    }
                                }
                                // handling general inputs, also logical operations
                                foreach (string s in logicalOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var p = placeHolders[placeholderCount];
                                        placeholderCount++;
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p + " = " + trimmedStr);
                                        goto done;
                                    }
                                }
                                // handling limit functional blocks
                                foreach (string s in limitOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var splitString = trimmedStr.Split(' ');
                                        var p = splitString[0];
                                        var min = splitString[2];
                                        var inp = splitString[3];
                                        var max = splitString[4];
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p + " = " + s + "(" + min + ", " + max + ", " + inp + ")");
                                        goto done;
                                    }
                                }
                                // handling compare functional blocks
                                foreach (string s in bitshiftOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var splitString = trimmedStr.Split('=');
                                        //var input1 = "";
                                        //var k1 = i - 1;
                                        //while (k1 > 0 && myExp[k1] != ' ')
                                        //{
                                        //    k1--;
                                        //}
                                        //var k2 = k1;
                                        //while (k2 > 0 && myExp[k2] != '(')
                                        //{
                                        //    k2--;
                                        //}
                                        //var subExp = myExp.Substring(k2 + 1, k1 - k2 - 5);
                                        //input1 = subExp.Trim();
                                        var input2 = splitString[1].Trim().Split(' ')[1];
                                        var input3 = splitString[1].Trim().Split(' ')[2];
                                        var output2 = splitString[0].Trim();
                                        var p = output2;
                                        placeholderCount++;
                                        var a = myExp.Substring(i, j - i + 1);
                                        myExp = myExp.Replace(a, p);
                                        rungs.Add(p + " = " + s + "(" + input2 + ", " + input3 + ")");
                                        goto done;
                                    }
                                }
                                // handling timer functional blocks
                                foreach (string s in timerOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var input1 = "";
                                        var k1 = i - 1;
                                        while (k1 > 0 && myExp[k1] != ' ')
                                        {
                                            k1--;
                                        }
                                        var k2 = k1;
                                        while (k2 > 0 && myExp[k2] != '(')
                                        {
                                            k2--;
                                        }
                                        var subExp = myExp.Substring(k2 + 1, k1 - k2 - 5);
                                        input1 = subExp.Trim();
                                        var inputExp = trimmedStr.Split('=')[1].Trim();
                                        var input2 = inputExp.Split(' ')[1];
                                        var output2 = trimmedStr.Split('=')[0];
                                        var p = placeHolders[placeholderCount];
                                        placeholderCount++;
                                        var optor = inputExp.Split(' ')[0];
                                        var a = myExp.Substring(k2 + 1, j - k2);
                                        myExp = myExp.Replace(a, p);
                                        rungs.Add(p + ", " + output2 + " = " + optor + "(" + input1 + ", " + input2 + ")");
                                        goto done;
                                    }
                                }
                                // handling counter functional blocks
                                foreach (string s in counterOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var input1 = "";
                                        var k1 = i - 1;
                                        while (k1 > 0 && myExp[k1] != ' ')
                                        {
                                            k1--;
                                        }
                                        var k2 = k1;
                                        while (k2 > 0 && myExp[k2] != '(')
                                        {
                                            k2--;
                                        }
                                        var subExp = myExp.Substring(k2 + 1, k1 - k2 - 5);
                                        input1 = subExp.Trim();
                                        var inputExp = trimmedStr.Split('=')[1].Trim();
                                        var p = placeHolders[placeholderCount];
                                        placeholderCount++;
                                        var optor = inputExp.Split(' ')[1].Trim();
                                        var input2 = inputExp.Split(' ')[0].Trim();
                                        var input3 = inputExp.Split(' ')[2].Trim();
                                        var output2 = trimmedStr.Split('=')[0].Trim();
                                        var a = myExp.Substring(k2 + 1, j - k2);
                                        myExp = myExp.Replace(a, p);
                                        rungs.Add(p + ", " + output2 + " = " + optor + "(" + input1 + ", " + input2 + ", " + input3 + ")");
                                        goto done;
                                    }
                                }
                                // handling flipflop functional blocks
                                foreach (string s in flipflopOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var input1 = "";
                                        var k1 = i - 1;
                                        while (k1 > 0 && myExp[k1] != ' ')
                                        {
                                            k1--;
                                        }
                                        var k2 = k1;
                                        while (k2 > 0 && myExp[k2] != '(')
                                        {
                                            k2--;
                                        }
                                        var subExp = myExp.Substring(k2 + 1, k1 - k2 - 5);
                                        input1 = subExp.Trim();
                                        var p = placeHolders[placeholderCount];
                                        placeholderCount++;
                                        //var optor = trimmedStr.Split(' ')[0].Trim();
                                        var optor = s;
                                        var input2 = trimmedStr.Split(' ')[1].Trim();
                                        var a = myExp.Substring(k2 + 1, j - k2);
                                        myExp = myExp.Replace(a, p);
                                        rungs.Add(p + " = " + optor + "(" + input1 + ", " + input2 + ")");
                                        goto done;
                                    }
                                }
                                // handling compare functional blocks
                                foreach (string s in compareOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var p = placeHolders[placeholderCount];
                                        placeholderCount++;
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p + " = " + trimmedStr);
                                        goto done;
                                    }
                                }
                                if (trimmedStr.Contains('='))
                                {
                                    var outputExp = trimmedStr.Split('=')[0];
                                    var inputExp = trimmedStr.Split('=')[1];
                                    var placeHolder = outputExp;
                                    myExp = myExp.Replace(myExp.Substring(i, j - i + 1), placeHolder);
                                    rungs.Add(placeHolder.Trim() + " = " + inputExp.Trim());
                                    goto done;
                                }
                            }
                            done:
                            i = -1;
                            break;
                        }
                    }
                }
            }
            rungs.Add(String.Join(" = ", myExp.Trim().Split('=')));
            //foreach(string rung in rungs)
            //{
            //    Console.WriteLine(rung);
            //}
            return rungs;
        }

        public static RungOperands GetRungOperands(string rung)
        {
            RungOperands ro = new RungOperands();
            
            List<Operand> operands = new List<Operand>();

            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> operatorList = ol.OperatorsList;

            List<string> logicalOperatorsList = ol.LogicalOperatorsList;
            List<string> logicalBlockOperatorsList = ol.LogicalBlockOperatorsList;
            List<string> arithmeticOperatorsList = ol.ArithmeticOperatorsList;
            List<string> bitshiftOperatorsList = ol.BitShiftOperatorsList;
            List<string> limitOperatorsList = ol.LimitOperatorsList;
            List<string> timerOperatorsList = ol.TimerOperatorsList;
            List<string> compareOperatorsList = ol.CompareOperatorsList;
            List<string> counterOperatorsList = ol.CounterOperatorsList;
            List<string> flipflopOperatorsList = ol.FlipFlopOperatorsList;

            var rightExp = (rung.Split('='))[1].Trim();

            string optorType = "";
            string optor = null;

            foreach(string logical in logicalOperatorsList)
            {
                if (rightExp.Contains(logical))
                {
                    optor = logical;
                    optorType = "logical";
                    goto getOperands;
                }
            }
            foreach (string logicalBlock in logicalBlockOperatorsList)
            {
                if (rightExp.Contains(logicalBlock))
                {
                    optor = logicalBlock;
                    optorType = "logical";
                    goto getOperands;
                }
            }
            foreach (string compare in compareOperatorsList)
            {
                if (rightExp.Contains(compare))
                {
                    optor = compare;
                    optorType = "compare";
                    goto getOperands;
                }
            }
            foreach (string arithmetic in arithmeticOperatorsList)
            {
                if (rightExp.Contains(arithmetic))
                {
                    optor = arithmetic;
                    optorType = "arithmetic";
                    goto getOperands;
                }
            }
            foreach (string bitshift in bitshiftOperatorsList)
            {
                if (rightExp.Contains(bitshift))
                {
                    optor = bitshift;
                    optorType = "bitshift";
                    goto getOperands;
                }
            }
            foreach (string timer in timerOperatorsList)
            {
                if (rightExp.Contains(timer))
                {
                    optor = timer;
                    optorType = "timer";
                    goto getOperands;
                }
            }
            foreach (string counter in counterOperatorsList)
            {
                if (rightExp.Contains(counter))
                {
                    optor = counter;
                    optorType = "counter";
                    goto getOperands;
                }
            }
            foreach (string flipflop in flipflopOperatorsList)
            {
                if (rightExp.Contains(flipflop))
                {
                    optor = flipflop;
                    optorType = "flipflop";
                    goto getOperands;
                }
            }
            foreach (string limit in limitOperatorsList)
            {
                if (rightExp.Contains(limit))
                {
                    optor = limit;
                    optorType = "limit";
                    goto getOperands;
                }
            }

        getOperands:
            string[] op;
            string op1;
            string op2;
            string dType = null;
            string tc_name = null;
            switch (optorType)
            {
                case "counter": op1 = rightExp.Split('(')[1];
                            op2 = op1.Split(')')[0];
                            op = op2.Split(',');
                            foreach (string s in op)
                            {
                                var sTrimmed = s.Trim();
                                if (!operatorList.Contains(sTrimmed) && sTrimmed != "")
                                {
                                    Operand operand = new Operand();
                                    if (sTrimmed.Contains("NOT"))
                                    {
                                        operand.TypeOfOperand = "Negation";
                                    }
                                    else if (!sTrimmed.Contains(':'))
                                    {
                                        operand.TypeOfOperand = "Numeric";
                                    }
                                    else
                                    {
                                        operand.TypeOfOperand = "Normal";
                                    }
                                    operand.OP = sTrimmed;
                                    operands.Add(operand);
                                }
                            }
                            tc_name = rightExp.Split(':')[0].Trim();
                            dType = optor;
                            break;
                case "bitshift": op1 = rightExp.Split('(')[1];
                            op2 = op1.Split(')')[0];
                            op = op2.Split(',');
                            foreach (string s in op)
                            {
                                var sTrimmed = s.Trim();
                                if (!operatorList.Contains(sTrimmed) && sTrimmed != "")
                                {
                                    Operand operand = new Operand();
                                    if (sTrimmed.Contains("NOT"))
                                    {
                                        operand.TypeOfOperand = "Negation";
                                    }
                                    else if (!sTrimmed.Contains(':'))
                                    {
                                        operand.TypeOfOperand = "Numeric";
                                    }
                                    else
                                    {
                                        operand.TypeOfOperand = "Normal";
                                    }
                                    operand.OP = sTrimmed;
                                    operands.Add(operand);
                                }
                            }
                            dType = "Word";
                            break;
                case "flipflop": op1 = rightExp.Split('(')[1];
                            op2 = op1.Split(')')[0];
                            op = op2.Split(',');
                            foreach (string s in op)
                            {
                                var sTrimmed = s.Trim();
                                if (!operatorList.Contains(sTrimmed) && sTrimmed != "")
                                {
                                    Operand operand = new Operand();
                                    if (sTrimmed.Contains("NOT"))
                                    {
                                        operand.TypeOfOperand = "Negation";
                                    }
                                    else if (!sTrimmed.Contains(':'))
                                    {
                                        operand.TypeOfOperand = "Numeric";
                                    }
                                    else
                                    {
                                        operand.TypeOfOperand = "Normal";
                                    }
                                    operand.OP = sTrimmed;
                                    operands.Add(operand);
                                }
                            }
                            dType = "Bool";
                            break;
                case "timer": op1 = rightExp.Split('(')[1];
                            op2 = op1.Split(')')[0];
                            op = op2.Split(',');
                            foreach(string s in op)
                            {
                                var sTrimmed = s.Trim();
                                if (!operatorList.Contains(sTrimmed) && sTrimmed != "")
                                {
                                    Operand operand = new Operand();
                                    if (sTrimmed.Contains("NOT"))
                                    {
                                        operand.TypeOfOperand = "Negation";
                                    }
                                    else if (!sTrimmed.Contains(':'))
                                    {
                                        operand.TypeOfOperand = "Numeric";
                                    }
                                    else
                                    {
                                        operand.TypeOfOperand = "Normal";
                                    }
                                    operand.OP = sTrimmed;
                                    operands.Add(operand);
                                }
                            }
                            tc_name = rightExp.Split(':')[0].Trim();
                            dType = optor;
                            break;
                case "limit": op1 = rightExp.Split('(')[1];
                            op2 = op1.Split(')')[0];
                            op = op2.Split(',');
                            foreach (string s in op)
                            {
                                var sTrimmed = s.Trim();
                                if (!operatorList.Contains(sTrimmed) && sTrimmed != "")
                                {
                                    Operand operand = new Operand();
                                    if (sTrimmed.Contains("NOT"))
                                    {
                                        operand.TypeOfOperand = "Negation";
                                    }
                                    else if (!sTrimmed.Contains(':'))
                                    {
                                        operand.TypeOfOperand = "Numeric";
                                    }
                                    else
                                    {
                                        operand.TypeOfOperand = "Normal";
                                    }
                                    operand.OP = sTrimmed;
                                    operands.Add(operand);
                                }
                            }
                            dType = "Word";
                            break;
                default: op = rightExp.Split(' ');
                        foreach (string s in op)
                        {
                            var sTrimmed = s.Trim();
                            if (!operatorList.Contains(sTrimmed) && sTrimmed != "")
                            {
                                if (sTrimmed.Contains('.') || sTrimmed.Contains('F') || sTrimmed.Contains('D'))
                                {
                                    dType = "Bool";
                                }
                                else
                                {
                                    dType = "Word";
                                }
                                Operand operand = new Operand();
                                if (sTrimmed.Contains("NOT"))
                                {
                                    operand.TypeOfOperand = "Negation";
                                }
                                else if (!s.Contains(':'))
                                {
                                    operand.TypeOfOperand = "Numeric";
                                }
                                else
                                {
                                    operand.TypeOfOperand = "Normal";
                                }
                                operand.OP = sTrimmed;
                                operands.Add(operand);
                            }
                        }
                    break;
            }

            ro.DataType = dType;
            ro.Oprator = optor;
            ro.Operands = operands;
            ro.NoOfOperands = operands.Count;
            ro.T_CName = tc_name;

            return ro;
        }

        public static string GetRungOutput(string rung)
        {
            var output = (rung.Split('='))[0].Trim();
            return output;
        }

        public static string GetAddressHex(string address)
        {
            var s = address[0];
            string hex = null;
            switch (s)
            {
                case 'I': hex = ParseExp.GetInputAddressHex(address);
                          break;
                case 'Q': hex = ParseExp.GetOutputAddressHex(address);
                          break;
                case 'F': hex = ParseExp.GetAddressHexGeneral(address, "22388000", 4);
                          break;
                case 'S': hex = ParseExp.GetAddressHexGeneral(address, "2001C420", 2);
                          break;
                case 'W': hex = ParseExp.GetAddressHexGeneral(address, "2001C620", 2);
                          break;
                case 'P': hex = ParseExp.GetAddressHexGeneral(address, "2001C820", 4);
                          break;
                case 'T': hex = ParseExp.GetAddressHexGeneral(address, "2001CC20", 2);
                          break;
                case 'C': hex = ParseExp.GetAddressHexGeneral(address, "2001CE20", 2);
                          break;
                case 'X': hex = ParseExp.GetAddressHexGeneral(address, "2001D020", 2);
                          break;
                case 'Y': hex = ParseExp.GetAddressHexGeneral(address, "2001C620", 2);
                          break;
                case 'D': hex = ParseExp.GetAddressHexGeneral(address, "223A8400", 4);
                          break;
                default: break;
            }
            return hex;
        }
        public static string GetAddressHexGeneral(string address, string initAddress, int nextByte)
        {
            string res = "";

            var num = int.Parse((address.Split(':'))[1]);

            int bytesToAdd = num * nextByte;

            var sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;
            res = sum.ToString("X");

            return res;
        }
        public static string GetInputAddressHex(string address)
        {
            string res = "";
            if (address.Contains('.'))
            {
                string initAddress = "22384000";

                var num = (address.Split(':'))[1];
                var num1 = int.Parse((num.Split('.'))[0]);
                var num2 = int.Parse((num.Split('.'))[1]);

                int bytesToAdd = ((num1*16) + num2) * 4;

                var sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;
                res = sum.ToString("X");
            } else
            {
                string initAddress = "2001C200";

                var num = int.Parse((address.Split(':'))[1]);
                int bytesToAdd = num * 2;

                var sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;
                res = sum.ToString("X");
            }
            return res;
        }

        public static string GetOutputAddressHex(string address)
        {
            string res = "";
            if (address.Contains('.'))
            {
                string initAddress = "22380000";

                var num = (address.Split(':'))[1];
                var num1 = int.Parse((num.Split('.'))[0]);
                var num2 = int.Parse((num.Split('.'))[1]);

                int bytesToAdd = ((num1 * 16) + num2) * 4;

                var sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;
                res = sum.ToString("X");
            }
            else
            {
                string initAddress = "2001C000";

                var num = int.Parse((address.Split(':'))[1]);
                int bytesToAdd = num * 2;

                var sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;
                res = sum.ToString("X");
            }
            return res;
        }



        public static byte[] GetAddressBytes(string address)
        {
            var s = address[0];
            byte[] addressBytes = new byte[] {};
            switch (s)
            {
                case 'I':
                    addressBytes = ParseExp.GetInputAddressBytes(address);
                    break;
                case 'Q':
                    addressBytes = ParseExp.GetOutputAddressBytes(address);
                    break;
                case 'F':
                    addressBytes = ParseExp.GetAddressBytesGeneral(address, "22388000", 4);
                    break;
                case 'S':
                    addressBytes = ParseExp.GetAddressBytesGeneral(address, "2001C420", 2);
                    break;
                case 'W':
                    addressBytes = ParseExp.GetAddressBytesGeneral(address, "2001C620", 2);
                    break;
                case 'P':
                    addressBytes = ParseExp.GetAddressBytesGeneral(address, "2001C820", 4);
                    break;
                case 'T':
                    addressBytes = ParseExp.GetAddressBytesGeneral(address, "2001CC20", 2);
                    break;
                case 'C':
                    addressBytes = ParseExp.GetAddressBytesGeneral(address, "2001CE20", 2);
                    break;
                case 'X':
                    addressBytes = ParseExp.GetAddressBytesGeneral(address, "2001D020", 2);
                    break;
                case 'Y':
                    addressBytes = ParseExp.GetAddressBytesGeneral(address, "2001C620", 2);
                    break;
                case 'D':
                    addressBytes = ParseExp.GetAddressBytesGeneral(address, "223A8400", 4);
                    break;
                default: break;
            }
            return addressBytes;
        }
        public static byte[] GetAddressBytesGeneral(string address, string initAddress, int nextByte)
        {
            byte[] addressBytes;
            var sum = 0;

            var num = int.Parse((address.Split(':'))[1]);

            int bytesToAdd = num * nextByte;

            sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;

            addressBytes = BitConverter.GetBytes(Convert.ToInt32(sum));
            return addressBytes;
        }
        public static byte[] GetInputAddressBytes(string address)
        {
            byte[] addressBytes;
            var sum = 0;
            if (address.Contains('.'))
            {
                string initAddress = "22384000";

                var num = (address.Split(':'))[1];
                var num1 = int.Parse((num.Split('.'))[0]);
                var num2 = int.Parse((num.Split('.'))[1]);

                int bytesToAdd = ((num1 * 16) + num2) * 4;

                sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;
            }
            else
            {
                string initAddress = "2001C200";

                var num = int.Parse((address.Split(':'))[1]);
                int bytesToAdd = num * 2;

                sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;
            }
            addressBytes = BitConverter.GetBytes(Convert.ToInt32(sum));
            return addressBytes;
        }

        public static byte[] GetOutputAddressBytes(string address)
        {
            byte[] addressBytes;
            var sum = 0;
            if (address.Contains('.'))
            {
                string initAddress = "22380000";

                var num = (address.Split(':'))[1];
                var num1 = int.Parse((num.Split('.'))[0]);
                var num2 = int.Parse((num.Split('.'))[1]);

                int bytesToAdd = ((num1 * 16) + num2) * 4;

                sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;
            }
            else
            {
                string initAddress = "2001C000";

                var num = int.Parse((address.Split(':'))[1]);
                int bytesToAdd = num * 2;

                sum = Convert.ToInt32(initAddress, 16) + bytesToAdd;
            }
            addressBytes = BitConverter.GetBytes(Convert.ToInt32(sum));
            return addressBytes;
        }
    }
}
