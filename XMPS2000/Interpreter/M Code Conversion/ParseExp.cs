using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInterpreter
{
    public class ParseExp
    {
        public static List<string> GetRungs(string exp)
        {
            //string expression = "Q0:000.05=(((I1:000.04 AND I1:000.05) OR I1:001.02 OR I1:001.07) AND (I1:000.02) AND (F2:004 OR I1:001.14 OR I1:002.13) AND F2:008 );";

            //Console.WriteLine("Initial expression: " + exp);

            var myExp = exp.Replace(";", string.Empty);

            Addresses vl = new Addresses();
            List<string> placeHolders = vl.AutoMemoryFlags;

            OperatorList ol = new OperatorList();
            List<string> operatorList = ol.OperatorsList;
            List<string> logicalOperatorsList = ol.LogicalOperatorsList;
            List<string> arithmeticOperatorsList = ol.ArithmeticOperatorsList;
            List<string> bitshiftOperatorsList = ol.BitShiftOperatorsList;
            List<string> limitOperatorsList = ol.LimitOperatorsList; 
            List<string> timerOperatorsList = ol.TimerOperatorsList;
            List<string> compareOperatorsList = ol.CompareOperatorsList;
            List<string> counterOperatorsList = ol.CounterOperatorsList;
            List<string> flipflopOperatorsList = ol.FlipFlopOperatorsList;

            List<string> rungs = new List<string>();

            var i = -1;
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
                                // handling limit functional blocks
                                foreach (string s in limitOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var splitString = trimmedStr.Split(" ");
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
                                        var splitString = trimmedStr.Split(" ");
                                        var p = splitString[0];
                                        var input1 = splitString[2];
                                        var input2 = splitString[3];
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p + " = " + s + "(" + input1 + ", " + input2 + ")");
                                        goto done;
                                    }
                                }
                                // handling compare functional blocks
                                foreach (string s in compareOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var p = placeHolders[rungs.Count];
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p + " = " + trimmedStr);
                                        goto done;
                                    }
                                }
                                // handling timer functional blocks
                                foreach (string s in timerOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var input1 = "";
                                        if (rungs.Count > 0)
                                        {
                                            input1 = rungs[rungs.Count - 1].Split('=')[0].Trim();
                                        }
                                        else
                                        {
                                            var k = i - 1;
                                            while(k > 0 && myExp[k] != '(')
                                            {
                                                k--;
                                            }
                                            var subExp = myExp.Substring(k + 1, i - k - 5);
                                            input1 = subExp.Trim();
                                        }
                                        var input2 = trimmedStr.Split(' ')[1];
                                        var output2 = trimmedStr.Split(' ')[2];
                                        var p = placeHolders[rungs.Count];
                                        var optor = trimmedStr.Split(' ')[0];
                                        var a = myExp.Substring(i - 12, j - i + 1 + 12);
                                        myExp = myExp.Replace(myExp.Substring(i - 12, j - i + 2 + 12), p);
                                        rungs.Add(p + ", " + output2 + " = " + optor + "(" + input1 + ", " + input2 + ")");
                                        goto done;
                                    }
                                }
                                // handling counter functional blocks
                                foreach (string s in counterOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var splitString = trimmedStr.Split(" ");
                                        var p = splitString[0];
                                        var optor = splitString[1];
                                        var input1 = splitString[2];
                                        var input2 = splitString[3];
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p + " = " + optor + "(" + input1 + ", " + input2 + ")");
                                        goto done;
                                    }
                                }
                                // handling flipflop functional blocks
                                foreach (string s in flipflopOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var input1 = "";
                                        if (rungs.Count > 0)
                                        {
                                            input1 = rungs[rungs.Count - 1].Split('=')[0].Trim();
                                        }
                                        else
                                        {
                                            var k = i - 1;
                                            while (k > 0 && myExp[k] != '(')
                                            {
                                                k--;
                                            }
                                            var subExp = myExp.Substring(k + 1, i - k - 5);
                                            input1 = subExp.Trim();
                                        }
                                        var p = placeHolders[rungs.Count];
                                        var optor = trimmedStr.Split(" ")[0];
                                        var input2 = trimmedStr.Split(" ")[1];
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p + " = " + optor + "(" + input1 + ", " + input2 + ")");
                                        goto done;
                                    }
                                }
                                // handling arithmetic functional blocks
                                foreach (string s in arithmeticOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var outputExpression = trimmedStr.Split('=')[0];
                                        var inputExpression = trimmedStr.Split('=')[1];
                                        var p = outputExpression;
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p + " = " + inputExpression);
                                    }
                                }
                                // handling general inputs, also logical operations
                                foreach (string s in logicalOperatorsList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        var p = placeHolders[rungs.Count];
                                        myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                                        rungs.Add(p + " = " + trimmedStr);
                                    }
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
            foreach(string rung in rungs)
            {
                Console.WriteLine(rung);
            }
            return rungs;
        }

        public static RungOperands GetRungOperands(string rung)
        {
            RungOperands ro = new RungOperands();
            
            List<Operand> operands = new List<Operand>();

            OperatorList ol = new OperatorList();
            List<string> operatorList = ol.OperatorsList;

            List<string> logicalOperatorsList = ol.LogicalOperatorsList;
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
                            //tc_name = rightExp.Split(':')[0].Trim();
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
                            break;
                default: op = rightExp.Split(' ');
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
