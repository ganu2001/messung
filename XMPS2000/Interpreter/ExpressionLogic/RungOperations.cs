
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestInterpreter;
using TestInterpreter.Helpers;

namespace ConsoleApp1
{
    public class RungOperations
    {
        public static object Envoronment { get; private set; }
        public static string FBLine = "";
        // forming expression from given list of input lines
        static string var11 = "";
        static string var12 = "";
        //string fun2 = "";
        //string fun11 = "";
        //string fun22 = "";
        //string fun1 = "";
        //string fun3 = "";
        //string fun4 = "";
        //string exprsign = "";
        //string addedinstr = "";


        public void FunctionBlock(string FB)
        {
            FBLine = FBLine + FB;
            // Console.WriteLine(FBLine);
        }
        public static void formula(List<string> rungLines)

        {
            bool NSBfound = false;
            bool first = true;
            bool bracket = false;
            string expression1 = "";
            string eor = "EOR";
            string expression = "";
            string[] Fun = new string[] { };

            string fun2 = "";
            string fun11 = "";
            string fun22 = "";
            string fun1 = "";
            string fun3 = "";
            string fun4 = "";
            string exprsign = "";
            string[] FunctionBlock = new string[] { };
            string[] FunctionExpression = new string[] { };
            string[] FunctionExpression1 = new string[] { };
            string str = "";

            var operatorType = "default";
            //string addedinstr = "";
            //Logic for Expression:-

            foreach (string line in rungLines)
            {
                //line.Substring(line.IndexOf('*'), line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);

                if (line.Contains("XIC"))
                {
                    if (NSBfound == true)
                    {
                        if (first)
                        {
                            expression = expression + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        }
                        //else
                        //{
                        //    if (addedinstr == "AND")
                        //    {
                        //        expression = "(" + expression + " ) OR " + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        //    }
                        else
                        {
                            expression = expression + "  OR " + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        }

                        //}
                        //addedinstr = "OR";
                        NSBfound = false;
                    }
                    else if (first && expression1 == "")
                    {
                        expression = expression + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        first = false;
                    }
                    else if (first && expression1 != "")
                    {
                        expression = expression + " AND " + expression1.ToString() + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        first = false;
                        // addedinstr = "";
                    }
                    else if (line.Contains("FB"))
                    {
                        expression = " " + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1) + " ";
                    }
                    else if (!first)
                    {
                        expression = expression + " AND " + expression1.ToString() + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        //addedinstr = "AND";
                    }
                    expression1 = "";
                }

                else if (line.StartsWith("XIO"))
                {
                    if (NSBfound == true)
                    {
                        if (first)
                        {
                            expression = expression + " NOT" + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        }
                        else
                        {
                            expression = expression + " OR NOT" + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        }
                        NSBfound = false;
                    }
                    else if (first && expression1 == "")
                    {
                        expression = expression + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        first = false;
                    }
                    else if (first && expression1 != "")
                    {
                        expression = expression + " AND " + expression1.ToString() + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                        first = false;
                    }
                    else if (!first)
                    {
                        expression = expression + " AND " + " NOT" + expression1.ToString() + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                    }
                    expression1 = "";
                }
                else if (line.Contains("NXB"))
                {
                    NSBfound = true;
                }

                else if (line.Contains("BST"))
                {
                    if (expression.Length <= 1)
                    {
                        expression = expression + "(";
                        // addedinstr = "";
                    }
                    else
                    {
                        expression1 = "(";
                        // addedinstr = "";
                    }
                    first = true;

                }
                else if (line.Contains("BND"))
                {
                    expression = expression + ")";
                    // addedinstr = "";
                    first = false;
                }
                else if (line.StartsWith("FB"))
                {
                    //  expression = expression;if needed add "]" here
                    FBLine = FBLine + line;


                }
                else if (line.Trim().StartsWith("("))
                {
                    FBLine = FBLine + line;
                }
                else if (line.Trim().StartsWith(";"))
                {
                    //Logic for function block :-
                    FBLine = FBLine + line;
                    //checking the function block for the type of the operation:-
                    OperatorList ol = new OperatorList();
                    List<string> operatorsList = ol.OperatorsList;
                    List<string> logicalOperators = ol.LogicalOperatorsList;
                    List<string> arithmeticOperators = ol.ArithmeticOperatorsList;
                    List<string> timerOperators = ol.TimerOperatorsList;
                    List<string> compareOperators = ol.CompareOperatorsList;
                    List<string> limitOperators = ol.LimitOperatorsList;
                    List<string> counteroperators = ol.CounterOperatorsList;
                    List<string> flipflopoperators = ol.FlipFlopOperatorsList;
                    List<string> bitshiftoperators = ol.BitShiftOperatorsList;
                   // blockOperations block = new blockOperations();
                    //if (FBLine.Contains("TON") || FBLine.Contains("TOF") || FBLine.Contains("TP"))
                    //{
                    //    block.timerBlockFun(FBLine);
                    //}

                    foreach (string op in timerOperators)
                    {
                        //timer operator:-

                        if (FBLine.Contains(op))
                        {
                            FunctionBlock = FBLine.Split(";");
                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                               // exprsign = "";
                                FunctionExpression = FunctionBlock[i].Split("(");
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("TON") || FunctionExpression[j].Contains("TOF"))
                                        {
                                            str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                        }
                                        else if (FunctionExpression[j].Contains("TP"))
                                        {
                                            str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                        }
                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 = exprsign +fun1  + FunctionExpression[j];//if needed add"()" here.
                                                fun1 = fun1.Replace("\"", "");
                                                fun1 = fun1.Replace("#", "");
                                                //removed "" and # from the timer input.
                                            }
                                            else
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun2 = fun2 + " " + FunctionExpression[j];//if needed add"()" here.
                                            }
                                            if (str != "")
                                            {

                                                exprsign = str;//if needed add "()"here.
                                                str = "";
                                            }
                                        }
                                    }
                                }
                                if (i % 2 == 1)
                                {                                    
                                        fun4 = " AND " + "(" + exprsign+" " + fun1  + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                }
                            }
                            expression = "(" + expression + ")" + fun3;
                            FBLine = "";
                            break;
                        }
                    }
                    foreach (string op in arithmeticOperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            //  if (operatorType.Contains("+") || operatorType.Contains("-") || operatorType.Contains("/") || operatorType.Contains("%") || operatorType.Contains("MOV"))
                            FunctionBlock = FBLine.Split(";");

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {

                                exprsign = "";
                                // fun3
                                FunctionExpression = FunctionBlock[i].Split("(");
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("+") || FunctionExpression[j].Contains("-") || FunctionExpression[j].Contains("/") || FunctionExpression[j].Contains("***") || FunctionExpression[j].Contains("%"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                        }
                                        else if (FunctionExpression[j].Contains(">=") || FunctionExpression[j].Contains("<=") || FunctionExpression[j].Contains("+=") || FunctionExpression[j].Contains("-=") || FunctionExpression[j].Contains("/=") || FunctionExpression[j].Contains("*="))
                                        {
                                            str = FunctionExpression[j].Substring(1, 2);
                                        }
                                        else if (FunctionExpression[j].Contains("MOV"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 3);
                                        }

                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 = fun1 + exprsign + FunctionExpression[j];//if needed add"()" here.
                                                fun1 = fun1.Replace("\"", "");
                                                //removed "" from the timer input.

                                            }
                                            else
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun2 = fun2 + " " + FunctionExpression[j];//if needed add"()" here.
                                            }
                                            // Console.Write("(" + FunctionExpression[j]);// to print the expression for input 
                                            if (str != "")
                                            {
                                                exprsign = str;//if needed add "()"here.
                                                exprsign = " " + exprsign + " ";
                                                str = "";
                                            }
                                        }
                                    }
                                }
                                if (i % 2 == 1)
                                {
                                    if (fun1.Contains("+") || fun1.Contains("-") || fun1.Contains("*") || fun1.Contains("/") || fun1.Contains("%"))
                                    {
                                        fun4 = " AND " + "(" + fun2 + " = " + fun1 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        fun4 = " AND " + "(" + fun2 + fun1 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }

                                }
                            }
                            expression = "(" + expression + ")" + fun3;
                            fun3 = "";
                            FBLine = "";
                            break;
                        }
                    }
                    foreach (string op in logicalOperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(";");

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {

                                exprsign = "";
                                FunctionExpression = FunctionBlock[i].Split("(");
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("+") || FunctionExpression[j].Contains("-") || FunctionExpression[j].Contains("/") || FunctionExpression[j].Contains("***"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                        }
                                        else if (FunctionExpression[j].Contains(">=") || FunctionExpression[j].Contains("<=") || FunctionExpression[j].Contains("+=") || FunctionExpression[j].Contains("-=") || FunctionExpression[j].Contains("/=") || FunctionExpression[j].Contains("*="))
                                        {
                                            str = FunctionExpression[j].Substring(1, 2);
                                        }
                                        else if (FunctionExpression[j].Contains("MOV"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 3);
                                        }

                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 = fun1 + exprsign + FunctionExpression[j];//if needed add"()" here.
                                                fun1 = fun1.Replace("\"", "");
                                                //removed "" from the timer input.

                                            }
                                            else
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun2 = fun2 + " " + FunctionExpression[j];//if needed add"()" here.
                                            }
                                            // Console.Write("(" + FunctionExpression[j]);// to print the expression for input 
                                            if (str != "")
                                            {
                                                exprsign = str;//if needed add "()"here.
                                                str = "";
                                            }
                                        }
                                    }
                                }
                                if (i % 2 == 1)
                                {
                                    if (fun1.Contains("+") || fun1.Contains("-") || fun1.Contains("*") || fun1.Contains("/") || fun1.Contains("%"))
                                    {
                                        fun4 = " AND " + "(" + fun1 + "=" + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }

                                }
                            }
                            expression = "(" + expression + ")" + fun3;
                            fun3 = "";
                            FBLine = "";
                            break;
                        }
                    }
                    foreach (string op in compareOperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(";");

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {

                                exprsign = "";
                                FunctionExpression = FunctionBlock[i].Split("(");
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("+") || FunctionExpression[j].Contains("-") || FunctionExpression[j].Contains("/") || FunctionExpression[j].Contains("***"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                        }
                                        else if (FunctionExpression[j].Contains(">=") || FunctionExpression[j].Contains("<=") || FunctionExpression[j].Contains("+=") || FunctionExpression[j].Contains("-=") || FunctionExpression[j].Contains("/=") || FunctionExpression[j].Contains("*="))
                                        {
                                            str = FunctionExpression[j].Substring(1, 2);
                                        }
                                        else if (FunctionExpression[j].Contains("MOV"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 3);
                                        }
                                        else if (FunctionExpression[j].Contains("<") || FunctionExpression[j].Contains(">"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                        }

                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 = fun1 + exprsign + FunctionExpression[j];//if needed add"()" here.
                                                fun1 = fun1.Replace("\"", "");
                                                exprsign = "";
                                                //removed "" from the timer input.

                                            }
                                            else
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun2 = fun2 + " " + FunctionExpression[j];//if needed add"()" here.
                                            }
                                            // Console.Write("(" + FunctionExpression[j]);// to print the expression for input 
                                            if (str != "")
                                            {
                                                exprsign = str;//if needed add "()"here.
                                                exprsign = " " + exprsign + " ";
                                                str = "";
                                            }
                                        }
                                    }
                                }
                                if (i % 2 == 1)
                                {
                                    if (fun1.Contains("<") || fun1.Contains(">"))
                                    {
                                        fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }

                                }
                            }
                            expression = "(" + expression + ")" + fun3;
                            fun3 = "";
                            FBLine = "";
                            break;
                        }
                    }
                    foreach (string op in limitOperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(";");

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                                FunctionExpression = FunctionBlock[i].Split("(");
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("+") || FunctionExpression[j].Contains("-") || FunctionExpression[j].Contains("/") || FunctionExpression[j].Contains("***"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                        }

                                        else if (FunctionExpression[j].Contains("LIMIT"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 5);
                                        }
                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 = fun1 + exprsign + FunctionExpression[j];//if needed add"()" here.
                                                fun1 = fun1.Replace("\"", "");
                                                exprsign = " ";
                                                //removed "" from the timer input.

                                            }
                                            else
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun2 = fun2 + " " + FunctionExpression[j];//if needed add"()" here.
                                            }
                                            if (str != "")
                                            {
                                                exprsign = str;//if needed add "()"here.
                                                exprsign = " " + exprsign + " ";
                                                str = "";
                                            }
                                        }
                                    }
                                }
                                if (i % 2 == 1)
                                {
                               
                                        fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    

                                }
                            }
                            expression = "(" + expression + ")" + fun3;
                            fun3 = "";
                            FBLine = "";
                            break;
                        }
                    }
                    foreach (string op in counteroperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(";");

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                                FunctionExpression = FunctionBlock[i].Split("(");
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("+") || FunctionExpression[j].Contains("-") || FunctionExpression[j].Contains("/") || FunctionExpression[j].Contains("***"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                        }
                                        else if (FunctionExpression[j].Contains(">=") || FunctionExpression[j].Contains("<=") || FunctionExpression[j].Contains("+=") || FunctionExpression[j].Contains("-=") || FunctionExpression[j].Contains("/=") || FunctionExpression[j].Contains("*="))
                                        {
                                            str = FunctionExpression[j].Substring(1, 2);
                                        }
                                        else if (FunctionExpression[j].Contains("MOV"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 3);
                                        }
                                        else if (FunctionExpression[j].Contains("LIMIT"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 5);
                                        }
                                        else if (FunctionExpression[j].Contains("<") || FunctionExpression[j].Contains(">"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                        }
                                        else if (FunctionExpression[j].Contains("CTU") || FunctionExpression[j].Contains("CTD"))
                                        {
                                            str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);

                                            //FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1)
                                        }

                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 = fun1 + exprsign + FunctionExpression[j];//if needed add"()" here.
                                                fun1 = fun1.Replace("\"", "");
                                                exprsign = " ";
                                                //removed "" from the timer input.

                                            }
                                            else
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun2 = fun2 + " " + FunctionExpression[j];//if needed add"()" here.
                                            }
                                            if (str != "")
                                            {
                                                exprsign = str;//if needed add "()"here.
                                                exprsign = " " + exprsign + " ";
                                                str = "";
                                            }
                                        }
                                    }
                                }
                                if (i % 2 == 1)
                                {
                                    if (fun1.Contains("<") || fun1.Contains(">"))
                                    {
                                        fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }

                                }
                            }
                            expression = "(" + expression + ")" + fun3;
                            fun3 = "";
                            FBLine = "";
                            break;
                        }
                    }
                    foreach (string op in flipflopoperators)
                    {
                        if (FBLine.Contains(op))
                        {

                            FunctionBlock = FBLine.Split(";");

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                                FunctionExpression = FunctionBlock[i].Split("(");
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("CTU") || FunctionExpression[j].Contains("CTD"))
                                        {
                                            str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);

                                            //FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1)
                                        }
                                        else if (FunctionExpression[j].Contains("RS") || FunctionExpression[j].Contains("SR"))
                                        {
                                            str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);

                                            //FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1)
                                        }
                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 = fun1 + exprsign + FunctionExpression[j];//if needed add"()" here.
                                                fun1 = fun1.Replace("\"", "");
                                                exprsign = " ";
                                                //removed "" from the timer input.
                                            }
                                            else
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun2 = fun2 + " " + FunctionExpression[j];//if needed add"()" here.
                                            }
                                            if (str != "")
                                            {
                                                exprsign = str;//if needed add "()"here.
                                                exprsign = " " + exprsign + " ";
                                                str = "";
                                            }
                                        }
                                    }
                                }
                                if (i % 2 == 1)
                                {
                                    if (fun1.Contains("<") || fun1.Contains(">"))
                                    {
                                        fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        fun4 = " AND " + "(" + exprsign + fun1 + fun2 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }

                                }
                            }
                            expression = "(" + expression + ")" + fun3;
                            fun3 = "";
                            FBLine = "";
                            break;
                        }
                    }
                    foreach (string op in bitshiftoperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(";");

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                                FunctionExpression = FunctionBlock[i].Split("(");
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("+") || FunctionExpression[j].Contains("-") || FunctionExpression[j].Contains("/") || FunctionExpression[j].Contains("***"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                        }                                 
                                        else if (FunctionExpression[j].Contains("CTU") || FunctionExpression[j].Contains("CTD"))
                                        {
                                            str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                        }
                                        else if (FunctionExpression[j].Contains("SHL") || FunctionExpression[j].Contains("SHR") || FunctionExpression[j].Contains("ROR") || FunctionExpression[j].Contains("ROL"))
                                        {
                                            str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                        }
                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 = exprsign + fun1 + " " +  FunctionExpression[j];//if needed add"()" here.
                                                fun1 = fun1.Replace("\"", "");
                                                exprsign = " ";
                                                //removed "" from the timer input.

                                            }
                                            else
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun2 = fun2  + FunctionExpression[j];//if needed add"()" here.
                                            }
                                            if (str != "")
                                            {
                                                exprsign = str;//if needed add "()"here.
                                                exprsign = " " + exprsign ;
                                                str = "";
                                            }
                                        }
                                    }
                                }
                                if (i % 2 == 1)
                                {                                                                     
                                        fun4 = " AND " + "(" + fun2 + fun1 + ")";
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";                                  
                                }
                            }
                            expression = "(" + expression + ")" + fun3;
                            fun3 = "";
                            FBLine = "";
                            break;
                        }
                    }
                }
                else if (line.Contains("OTE"))
                {
                    expression = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1) + " = " + "(" + expression + ")";
                    Console.WriteLine();
                }
            }
            Console.WriteLine(expression);
            FinalRungOperations.TestOperation(expression);
        }
    }
   


    // make sure expression has correct address and operators before passing further
   

    // Console.WriteLine(expression.ToString());
    //            string[] FunctionBlock = new string[] { };
    //            string[] FunctionExpression = new string[] { };
    //            string[] FunctionExpression1 = new string[] { };
    //            string str = "";
    //            //fun11 = "";
    //            //fun22 = "";//done
    //            if (FBLine != "")
    //            {
    //                // Console.WriteLine(FBLine);
    //                FunctionExpression1 = FBLine.Split("FB");
    //                FBLine = "";
    //                for (int k = 0; k < FunctionExpression1.Length; k++)
    //                {
    //                    fun2 = "";
    //                    fun22 = "";
    //                    fun11 = "";
    //                    fun1 = "";

    //                    exprsign = "";
    //                    Console.WriteLine();
    //                   //  Console.WriteLine(FunctionExpression1[k]);
    //                        exprsign = "";
    //                        FunctionBlock = FunctionExpression1[k].Split(";");

    //                        for (int i = 0; i < FunctionBlock.Length; i++)
    //                        {


    //                            FunctionExpression = FunctionBlock[i].Split("(");
    //                            for (int j = 0; j < FunctionExpression.Length; j++)
    //                            {
    //                               // fun4 = "";
    //                                //Console.WriteLine("("+ FunctionExpression[j]);
    //                                if (FunctionExpression[j].StartsWith("**"))
    //                                {
    //                                    continue;
    //                                }
    //                                else if (FunctionExpression[j].StartsWith("*"))
    //                                {
    //                                    if (FunctionExpression[j].Contains("+") || FunctionExpression[j].Contains("-") || FunctionExpression[j].Contains("/") || FunctionExpression[j].Contains("***"))
    //                                    {
    //                                        str = FunctionExpression[j].Substring(1, 1);
    //                                    }
    //                                    else if (FunctionExpression[j].Contains(">=") || FunctionExpression[j].Contains("<=") || FunctionExpression[j].Contains("+=") || FunctionExpression[j].Contains("-=") || FunctionExpression[j].Contains("/=") || FunctionExpression[j].Contains("*="))
    //                                    {
    //                                        str = FunctionExpression[j].Substring(1, 2);
    //                                    }

    //                                    else
    //                                    {
    //                                        if (i == 0)
    //                                        {
    //                                            FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
    //                                            fun1 = fun1 + exprsign + "(" + FunctionExpression[j] + ")";
    //                                            //Console.Write(fun1);
    //                                        }
    //                                        else
    //                                        {
    //                                            FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
    //                                            fun2 = fun2 + "(" + FunctionExpression[j] + ")";
    //                                          //  Console.WriteLine("="+fun2);
    //                                            //FunctionExpression[j] = "";
    //                                        }
    //                                        // Console.Write("(" + FunctionExpression[j]);// to print the expression for input 
    //                                        if (str != "")
    //                                        {
    //                                            str = "(" + str + ")";
    //                                             exprsign = str;
    //                                            str = "";
    //                                        }
    //                                    }
    //                                }
    //                                else if (FunctionExpression[j].Contains(";"))
    //                                {
    //                                    //  Console.WriteLine("OutPut");
    //                                }
    //                                else
    //                                {
    //                                    // Console.Write(FunctionExpression[j]);
    //                                }
    //                            }
    //                            //if(i%2==1)
    //                            //{
    //                              // List<string> fun3 = new List<string>();
    //                               fun4 = " and " + "("+ fun2 + "=" + fun1+")";
    //                                fun3 = fun3 + fun4;
    //                                fun1 = "";
    //                                fun2 = "";
    //                                fun4 = "";
    //                           // }
    //                        }
    //                  //  }
    //;
    //                     }//k can be closed here
    //                    //  Console.WriteLine(expression.ToString());
    //                    Console.WriteLine(" Expression :- ");
    //                string[] FinalExpr = new string[] { };
    //                string[] FinalExpr1 = new string[] { };
    //                FinalExpr = expression.Split("=");
    //                for (int i = 0; i < FinalExpr.Length; i++)
    //                {
    //                    if (i == 0)
    //                    {
    //                        Console.Write(FinalExpr[i] + " = ");
    //                    }
    //                    // Console.WriteLine(FinalExpr[i]);
    //                    else if (i == 1)
    //                    {
    //                        FinalExpr1 = FinalExpr[i].Split("]");
    //                        for (int j = 0; j < FinalExpr1.Length; j++)
    //                        {
    //                            if (j == 0)
    //                            {


    //                                FinalExpr1[j] = "(" + FinalExpr1[j] + fun3 + ")";// if needed add "]"here.
    //                               // fun11 = "";
    //                               // fun1 = "";
    //                            }
    //                            else
    //                            {
    //                                FinalExpr1[j] =  FinalExpr1[j]; //if needed add ")"here.
    //                               // fun22 = "";
    //                               // fun2 = "";
    //                            }
    //                            Console.Write("  " + FinalExpr1[j]);
    //                        }
    //                    }

    //                    // k = k + 1;
    //                }
    //                //}closing of k block
    //            }
    //            else
    //            {
    //                Console.WriteLine(expression);
    //            }
    //            //  FBLine = "";





}


