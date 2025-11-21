
using Interpreter.MCodeConversion;
using System;
using System.Collections.Generic;

namespace Interpreter.ExpressionLogic
{

    public class RungOperations
        {
        public static object Envoronment { get; private set; }
        public static string FBLine = "";
        // forming expression from given list of input lines

        OperatorList ol = new OperatorList();
        public void FunctionBlock(string FB)
        {
            FBLine = FBLine + FB;
            // Console.WriteLine(FBLine);
        }
        public static (bool, string, List<Exception>) Formula(List<string> rungLines, List<string> udfNames)

        {
            var errorFound = false;
            List<Exception> errors = new List<Exception>();
            
            bool NSBfound = false;
            bool first = true;
            string expression1 = "";
            string expression = "";
            string[] Fun = new string[] { };

            string fun2 = "";
            string fun1 = "";
            string fun3 = "";
            string fun4 = "";
            string exprsign = "";
            string[] FunctionBlock = new string[] { };
            string[] FunctionExpression = new string[] { };
            string[] FunctionExpression1 = new string[] { };
            string[] OutputBlock = new string[] { };
            string output = "";
            string str = "";
            string simpleExpr = "";
            var checkExpr = "";

            Addresses adr = new Addresses();
            List<string> validInputBitAddressExpr = new List<string> { };
            validInputBitAddressExpr.AddRange(adr.InputBitAddress);
            validInputBitAddressExpr.AddRange(adr.OutputBitAddress);
            //string addedinstr = "";
            //Logic for Expression:-

            foreach (string line in rungLines)
            {
                //line.Substring(line.IndexOf('*'), line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);

                if (line.Contains("XIC"))
                {
                    var lineSplit = line.Split(new[] { "**" }, StringSplitOptions.None);
                    if (lineSplit.Length > 2)
                    {
                        errorFound = true;
                        errors.Add(new CompilerException("Logical input error"));
                    } else
                    {
                        if (NSBfound == true)
                        {
                            if (first)
                            {
                                checkExpr =  line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
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
                                checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                                expression = expression + " OR " + "("+ line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            }
                            //}
                            //addedinstr = "OR";
                            NSBfound = false;
                        }
                        else if (first && expression != "" && expression1 == "")
                        {
                            checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression + "AND" + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            first = false;
                        }
                        else if (first && expression1 == "" && expression == "")
                        {
                            checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression  + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            first = false;
                        }
                        else if (first && expression1 != "" && expression == "")
                        {
                            checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression  + expression1.ToString() + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            first = false;
                            // addedinstr = "";
                        }
                        else if (first && expression1 != "" && expression != "")
                        {
                            checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression + "AND" + expression1.ToString() + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            first = false;
                            // addedinstr = "";
                        }
                        else if (line.Contains("FB"))
                        {
                            checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1) + " ";
                            expression = " " + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1) + " ";
                        }
                        else if (!first)
                        {
                            checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression + " AND " + expression1.ToString() + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1)+")";
                            //addedinstr = "AND";
                        }

                        //Checking whether the expression is empty or not and expression is given in proper input format or not
                        if (!validInputBitAddressExpr.Contains(checkExpr))
                        {
                            errorFound = true;
                            errors.Add(new CompilerException("Invalid logical input address '" + checkExpr.Trim() + "'"));
                        }
                        checkExpr = "";
                        expression1 = "";
                    }
                }

                else if (line.StartsWith("XIO"))
                {
                    var lineSplit = line.Split(new[] { "**" }, StringSplitOptions.None);
                    if (lineSplit.Length > 2)
                    {
                        errorFound = true;
                        errors.Add(new CompilerException("Logical input error"));
                    }
                    else
                    {
                        if (NSBfound == true)
                        {
                            if (first)
                            {
                                checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                                expression = expression + " NOT" + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            }
                            else
                            {
                                checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                                expression = expression + " OR"+"("+ "NOT" + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            }
                            NSBfound = false;
                        }
                        else if (first && expression1 == "" && expression == "")
                        {
                            checkExpr =  line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression + " NOT" + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            first = false;
                        }
                        else if (first && expression1 == "" && expression != "")
                        {
                            checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression + "AND" +" NOT" + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            first = false;
                        }
                        else if (first && expression1 != "" && expression == "")
                        {
                            checkExpr = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression + " NOT" + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            first = false;
                        }
                        else if (first && expression1 != "" && expression != "")
                        {
                            checkExpr =  line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression + " AND "+ "NOT" + expression1.ToString() + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            first = false;
                        }
                        else if (!first)
                        {
                            checkExpr =  line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1);
                            expression = expression + " AND " + " NOT" + expression1.ToString() + line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1)+")";
                        }

                        if (checkExpr == "")
                        {
                            errorFound = true;
                            errors.Add(new CompilerException("Logical input error"));
                        }
                        else if (!validInputBitAddressExpr.Contains(checkExpr))
                        {
                            errorFound = true;
                            errors.Add(new CompilerException("Invalid logical input address '" + checkExpr.Trim() + "'"));
                        }
                        expression1 = "";
                        checkExpr = "";
                    }
                }
                else if (line.Contains("NXB"))
                {
                    NSBfound = true;
                }
                else if (line.Contains("BST"))
                {
                    if (expression.Length <= 1)
                    {
                        expression1 = expression1 + "(";//If needed remove 1 from here.
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
                    string[] expressionOperator = new string[] { };
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
                   // List<string> udfnames = ol.UdfNamesList;
                    foreach (string op in timerOperators)
                    {
                        //timer operator:-

                        if (FBLine.Contains(op))
                        {
                            FunctionBlock = FBLine.Split(';');
                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                               // exprsign = "";
                                FunctionExpression = FunctionBlock[i].Split('(');
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("TON") )
                                        {
                                             str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                            //str = "TON";
                                        }
                                        else if(FunctionExpression[j].Contains("TOF"))
                                        {
                                            str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                           // str = "TOF";
                                        }
                                        else if (FunctionExpression[j].Contains("TP"))
                                        {
                                             str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                            //str = "TP";
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
                                                //fun1 = fun1 + exprsign;
                                                str = "";
                                            }
                                        }
                                    }
                                }
                                if (i % 2 == 1)
                                {
                                        simpleExpr = fun2 + "=" + exprsign + fun1 + simpleExpr;
                                    if(expression != "")
                                    {
                                        fun4 = " AND " + "(" + exprsign + " " + fun1 + fun2 + ")";
                                    }
                                    else
                                    {
                                        fun4 = "(" + exprsign + " " + fun1 + fun2 + ")";
                                    }
                                        
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                }
                            }
                            (bool isCompileSuccess, var errorMessages) = ExceptionCheck.TimerBlockExceptionCheck(simpleExpr);
                            if (!isCompileSuccess)
                            {
                                errorFound = true;
                                errors.AddRange(errorMessages);
                            }
                            if (expression != "")
                            {
                                expression = "(" + expression + ")" + fun3;
                            }
                            else
                            {
                                expression = expression  + fun3;
                            }
                          
                            simpleExpr = "";
                            FBLine = "";
                            break;
                        }
                        first = false;
                    }
                    foreach (string op in arithmeticOperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            //  if (operatorType.Contains("+") || operatorType.Contains("-") || operatorType.Contains("/") || operatorType.Contains("%") || operatorType.Contains("MOV"))
                            FunctionBlock = FBLine.Split(';');

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {

                                exprsign = "";
                                // fun3
                                FunctionExpression = FunctionBlock[i].Split('(');
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("+") || FunctionExpression[j].Contains("-") || FunctionExpression[j].Contains("/") || FunctionExpression[j].Contains("***") || FunctionExpression[j].Contains("MOD"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
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
                                                //removed "" from the input.

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
                                    if (fun1.Contains("+") || fun1.Contains("-") || fun1.Contains("*") || fun1.Contains("/") || fun1.Contains("MOD"))
                                    {
                                        simpleExpr = fun2 + "=" + fun1 + simpleExpr;
                                        if(expression != "")
                                        {
                                            fun4 = " AND " + "(" + fun2 + " = " + fun1 + ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + fun2 + " = " + fun1 + ")";
                                        }
                                       
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        simpleExpr = fun2 + "=" + fun1 + simpleExpr;
                                        if (expression != "")
                                        {
                                            fun4 = " AND " + "(" + fun2 + " = " + fun1 + ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + fun2 + " = " + fun1 + ")";
                                        }
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                }
                            }
                            (bool isCompileSuccess, var errorMessages) = ExceptionCheck.ArithmeticBlockExceptionCheck(simpleExpr);
                            if (!isCompileSuccess)
                            {
                                errorFound = true;
                                errors.AddRange(errorMessages);
                            }
                            if (expression != "")
                            {
                                expression = "(" + expression + ")" + fun3;
                            }
                            else
                            {
                                expression =   expression + fun3;
                            }
                           
                            fun3 = "";
                            FBLine = "";
                            simpleExpr = "";
                            break;
                        }
                        first = false;
                    }
                    foreach (string op in logicalOperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(';');

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                               // exprsign = "";
                                FunctionExpression = FunctionBlock[i].Split('(');
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains("AND") || FunctionExpression[j].Contains("XOR") || FunctionExpression[j].Contains("Not"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 3);
                                        }
                                        else if (FunctionExpression[j].Contains("OR"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 2);
                                        }
                                        else if (FunctionExpression[j].Contains("&"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                        }
                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 = fun1 + str+" " + FunctionExpression[j];//if needed add"()" here.
                                                fun1 = fun1.Replace("\"", "");
                                                fun1 = fun1.Replace("OR", "||");
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
                                    if (fun1.Contains("AND") || fun1.Contains("OR") || fun1.Contains("XOR") || fun1.Contains("&") || fun1.Contains("Not"))
                                    {
                                        simpleExpr = fun2 + fun1 + simpleExpr;
                                        // fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        if (expression != "")
                                        {
                                            fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + fun1 + fun2 + ")";
                                        }
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        simpleExpr = fun2 + fun1 + simpleExpr;
                                        // fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        if (expression != "")
                                        {
                                            fun4 = " AND " + "(" + fun1  + fun2 + ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + fun1 + fun2 + ")";
                                        }
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }

                                }
                            }
                            (bool isCompileSuccess, var errorMessages) = ExceptionCheck.LogicalBlockExceptionCheck(simpleExpr);
                            if (!isCompileSuccess)
                            {
                                errorFound = true;
                                errors.AddRange(errorMessages);
                            }
                            //expression = "(" + expression + ")" + fun3;
                            if (expression != "")
                            {
                                expression = "(" + expression + ")" + fun3;
                            }
                            else
                            {
                                expression = expression + fun3;
                            }
                            fun3 = "";
                            simpleExpr = "";
                            FBLine = "";
                            break;
                        }
                        first = false;
                    }
                    foreach (string op in compareOperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(';');

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {

                                exprsign = "";
                                FunctionExpression = FunctionBlock[i].Split('(');
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                        if (FunctionExpression[j].Contains(">=") || FunctionExpression[j].Contains("<=") || FunctionExpression[j].Contains("+=") || FunctionExpression[j].Contains("-=") || FunctionExpression[j].Contains("/=") || FunctionExpression[j].Contains("<>"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 2);
                                        }
                                        else if (FunctionExpression[j].Contains("<") || FunctionExpression[j].Contains(">") || FunctionExpression[j].Contains("="))
                                        {
                                            str = FunctionExpression[j].Substring(1, 1);
                                            if(str.Contains("="))
                                            {
                                                str = str.Replace("=", "==");
                                            }
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
                                    if (fun1.Contains("<") || fun1.Contains(">") || fun1.Contains(">=") || fun1.Contains("<=") || fun1.Contains("=") || fun1.Contains("!="))
                                    {
                                        simpleExpr = fun2 + fun1 + simpleExpr;
                                        //  fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        if (expression != "")
                                        {
                                            fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + fun1  + fun2 + ")";
                                        }
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        simpleExpr = fun2 + fun1 + simpleExpr;
                                        //  fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        if (expression != "")
                                        {
                                            fun4 = " AND " + "(" + fun1  + fun2 + ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + fun1  + fun2 + ")";
                                        }
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }

                                }
                            }
                            (bool isCompileSuccess, var errorMessages) = ExceptionCheck.CompareBlockExceptionCheck(simpleExpr);
                            if (!isCompileSuccess)
                            {
                                errorFound = true;
                                errors.AddRange(errorMessages);
                            }
                            // expression = "(" + expression + ")" + fun3;
                            if (expression != "")
                            {
                                expression = "(" + expression + ")" + fun3;
                            }
                            else
                            {
                                expression = expression + fun3;
                            }
                            fun3 = "";
                            simpleExpr = "";
                            FBLine = "";
                            break;
                        }
                        first = false;
                    }
                    foreach (string op in limitOperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(';');

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                                FunctionExpression = FunctionBlock[i].Split('(');
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {
                                         if (FunctionExpression[j].Contains("LIMIT"))
                                        {
                                            str = FunctionExpression[j].Substring(1, 5);
                                        }
                                        else
                                        {
                                            if (i == 0)
                                            {
                                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                                fun1 =  exprsign+ fun1 +" " +FunctionExpression[j]+" ";//if needed add"()" here.
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
                                    simpleExpr = fun2 + fun1 + simpleExpr;
                                    // fun4 = " AND " + "("+ fun2  + fun1 + ")";
                                    if (expression != "")
                                    {
                                        fun4 = " AND " + "(" + fun2  + fun1 + ")";
                                    }
                                    else
                                    {
                                        fun4 = "(" + fun2  + fun1 + ")";
                                    }
                                    fun3 = fun3 + fun4;
                                }
                            }
                            (bool isCompileSuccess, var errorMessages) = ExceptionCheck.LimitBlockExceptionCheck(simpleExpr);
                            if (!isCompileSuccess)
                            {
                                errorFound = true;
                                errors.AddRange(errorMessages);
                            }
                            // expression = "(" + expression + ")" + fun3;
                            if (expression != "")
                            {
                                expression = "(" + expression + ")" + fun3;
                            }
                            else
                            {
                                expression = expression + fun3;
                            }
                            fun3 = "";
                            simpleExpr = "";
                            FBLine = "";
                            break;
                        }
                        first = false;
                    }
                    foreach (string op in counteroperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(';');

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                                FunctionExpression = FunctionBlock[i].Split('(');
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {

                                         if (FunctionExpression[j].Contains("CTU"))
                                        {
                                             str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                            //str = "CTU";
                                            //FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1)
                                        }
                                         else if(FunctionExpression[j].Contains("CTD"))
                                        {
                                            str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                            //str = "CTD";
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
                                        simpleExpr = fun2 + "=" + fun1 + simpleExpr;
                                        //  fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        if (expression != "")
                                        {
                                            fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + fun1 + fun2 + ")";
                                        }
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        simpleExpr = fun2 + "=" + fun1 + simpleExpr;
                                        // fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        if (expression != "")
                                        {
                                            fun4 = " AND " + "(" + fun1  + fun2 + ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + fun1 + fun2 + ")";
                                        }
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }

                                }
                                
                            }
                            (bool isCompileSuccess, var errorMessages) = ExceptionCheck.CounterBlockExceptionCheck(simpleExpr);
                            if (!isCompileSuccess)
                            {
                                errorFound = true;
                                errors.AddRange(errorMessages);
                            }
                            //expression = "(" + expression + ")" + fun3;
                            if (expression != "")
                            {
                                expression = "(" + expression + ")" + fun3;
                            }
                            else
                            {
                                expression = expression + fun3;
                            }
                            fun3 = "";
                            simpleExpr = "";
                            FBLine = "";
                            break;
                        }
                        first = false;
                    }
                    foreach (string op in flipflopoperators)
                    {
                        if (FBLine.Contains(op))
                        {

                            FunctionBlock = FBLine.Split(';');

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                                FunctionExpression = FunctionBlock[i].Split('(');
                                for (int j = 0; j < FunctionExpression.Length; j++)
                                {
                                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                                    {
                                        continue;
                                    }
                                    else if (FunctionExpression[j].StartsWith("*"))
                                    {

                                         if (FunctionExpression[j].Contains("RS"))
                                        {
                                            // str = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                                            str = "RS";
                                            //FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1)
                                        }
                                         else if(FunctionExpression[j].Contains("SR"))
                                        {
                                            str = "SR";
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
                                        simpleExpr = fun2  + fun1 + simpleExpr;
                                        //fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        if (expression != "")
                                        {
                                            fun4 = " AND " + "(" + fun1 + fun2 + ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + fun1 + fun2 + ")";
                                        }
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                    else
                                    {
                                        simpleExpr = fun2 + fun1 + simpleExpr;
                                        //fun4 = " AND " + "(" + exprsign + fun1 + fun2 + ")";
                                        if (expression != "")
                                        {
                                            fun4 = " AND " + "(" +exprsign + fun1  + fun2+ ")";
                                        }
                                        else
                                        {
                                            fun4 = "(" + exprsign + fun1  + fun2 + ")";
                                        }
                                        fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";
                                    }
                                }
                            }
                            (bool isCompileSuccess, var errorMessages) = ExceptionCheck.FlipFlopBlockExceptionCheck(fun3);
                            if (!isCompileSuccess)
                            {
                                errorFound = true;
                                errors.AddRange(errorMessages);
                            }
                            //expression = "(" + expression + ")" + fun3;
                            if (expression != "")
                            {
                                expression = "(" + expression + ")" + fun3;
                            }
                            else
                            {
                                expression = expression + fun3;
                            }
                            fun3 = "";
                            simpleExpr = "";
                            FBLine = "";
                            break;
                        }
                        first = false;
                    }
                    foreach (string op in bitshiftoperators)
                    {
                        if (FBLine.Contains(op))
                        {
                            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                            FunctionBlock = FBLine.Split(';');

                            for (int i = 0; i < FunctionBlock.Length; i++)
                            {
                                FunctionExpression = FunctionBlock[i].Split('(');
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
                                    simpleExpr = fun2 + "=" + fun1 + simpleExpr;
                                    //fun4 = " AND " + "(" + fun2 + fun1 + ")";
                                    if (expression != "")
                                    {
                                        fun4 = " AND " + "(" + fun2  + fun1 + ")";
                                    }
                                    else
                                    {
                                        fun4 = "(" + fun2  + fun1 + ")";
                                    }
                                    fun3 = fun3 + fun4;
                                        fun1 = "";
                                        fun2 = "";
                                        fun4 = "";                                  
                                }
                            }
                            (bool isCompileSuccess, var errorMessages) = ExceptionCheck.BitShiftBlockExceptionCheck(simpleExpr);
                            if (!isCompileSuccess)
                            {
                                errorFound = true;
                                errors.AddRange(errorMessages);
                            }
                            // expression = "(" + expression + ")" + fun3;
                            if (expression != "")
                            {
                                expression = "(" + expression + ")" + fun3;
                            }
                            else
                            {
                                expression = expression + fun3;
                            }
                            fun3 = "";
                            simpleExpr = "";
                            FBLine = "";
                            break;
                        }
                        first = false;
                    }
                //    foreach (string op in udfNames)
                //    {
                //        if (FBLine.Contains(op))
                //        {
                //            // if (operatorType.Contains("AND") || operatorType.Contains("OR") || operatorType.Contains("XOR"))
                //            FunctionBlock = FBLine.Split(';');

                //            for (int i = 0; i < FunctionBlock.Length; i++)
                //            {
                //                // exprsign = "";
                //                FunctionExpression = FunctionBlock[i].Split('(');
                //                for (int j = 0; j < FunctionExpression.Length; j++)
                //                {
                //                    if (FunctionExpression[j].StartsWith("**") && !FunctionExpression[j].StartsWith("***"))
                //                    {
                //                        continue;
                //                    }
                //                    else if (FunctionExpression[j].StartsWith("*"))
                //                    {
                //                            if (i == 0)
                //                            {
                //                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                //                                fun1 = fun1 + str + FunctionExpression[j];//if needed add"()" here.
                //                                fun1 = fun1.Replace("\"", "");
                //                                //removed "" from the timer input.
                //                            }
                //                            else
                //                            {
                //                                FunctionExpression[j] = FunctionExpression[j].Substring(FunctionExpression[j].IndexOf('*') + 1, FunctionExpression[j].IndexOf('*', FunctionExpression[j].IndexOf('*') + 2) - FunctionExpression[j].IndexOf('*') - 1);
                //                                fun2 = fun2 + " " + FunctionExpression[j];//if needed add"()" here.
                //                            }
                //                            // Console.Write("(" + FunctionExpression[j]);// to print the expression for input 
                //                            if (str != "")
                //                            {
                //                                exprsign = str;//if needed add "()"here.
                //                                str = "";
                //                            }
                //                    }
                //                }
                //                if (i % 2 == 1)
                //                {
                //                        simpleExpr = fun2 + fun1 + simpleExpr;
                //                        fun4 = " AND " + "(" + fun1 + fun2 + ")";
                //                        fun3 = fun3 + fun4;
                //                        fun1 = "";
                //                        fun2 = "";
                //                        fun4 = "";
                //                }
                //            }
                //            (bool isCompileSuccess, var message) = ExceptionCheck.LogicalBlockExceptionCheck(simpleExpr);
                //            expression = "(" + expression + ")" + fun3;
                //            fun3 = "";
                //            simpleExpr = "";
                //            FBLine = "";
                //            break;
                //        }
                //    }
                }
                if (line.Contains("OTE"))
                {
                    OutputBlock = line.Split('(');
                    for(int i=0;i<OutputBlock.Length;i++)
                    {
                        if(OutputBlock[i].StartsWith("**"))
                        {
                            continue;
                        }
                        else if(OutputBlock[i].StartsWith("*"))
                        {
                            output = output + OutputBlock[i].Substring(OutputBlock[i].IndexOf('*') + 1, OutputBlock[i].IndexOf('*', OutputBlock[i].IndexOf('*') + 2) - OutputBlock[i].IndexOf('*') - 1);
                        }
                    }
                    if(output == "")
                    {
                        errorFound = true;
                        errors.Add(new CompilerException("Final Output missing"));
                    }
                    //  expression = line.Substring(line.IndexOf('*') + 1, line.IndexOf('*', line.IndexOf('*') + 2) - line.IndexOf('*') - 1) + " = " + "(" + expression + ")";
                    //Console.WriteLine();
                    if(expression != "")
                    {
                        expression = output + " = " + "(" + expression + ")";
                    }
                    else
                    {
                        expression = output + "=" + expression;
                    }
                   
                }
            }
            //Console.WriteLine(expression);
            
            if (errorFound)
            {
                return (false, null, errors);
            }
            return (true, expression, null);
        }
    }
   







}


