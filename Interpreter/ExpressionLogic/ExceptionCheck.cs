using Interpreter.MCodeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.ExpressionLogic
{
    public class ExceptionCheck
    {

        public static (bool, List<Exception>) ArithmeticBlockExceptionCheck(string expression)
        {
            var arithmeticExceptionsOccured = new List<Exception>();//List of exceptions are added here.
            var errorFound = false;
            if (expression.Contains("("))
            {
                expression = expression.Replace(")", "");
            }
            if (expression.Contains(")"))
            {
                expression = expression.Replace(")", "");
            }
            if (expression.Contains("AND"))
            {
                expression = expression.Replace("AND", "");
            }

            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> arithmeticOperators = ol.ArithmeticOperatorsList;
            bool containsOperator = false;
            string optor = "";

            foreach (string op in arithmeticOperators)
            {
                if (expression.Contains(op))
                {
                    containsOperator = true;
                    optor = op;
                    break;
                }
            }
            if (containsOperator)
            {
                // char[] arr = { '*' };
                var inputExp = expression.Split('=')[1].Trim();
                var inputOperands = inputExp.Split(new[] { optor }, StringSplitOptions.None);
                if (inputOperands.Length > 0)
                {
                    Addresses adr = new Addresses();
                    List<string> validInputAddresses = new List<string> { };
                    validInputAddresses.AddRange(adr.InputWordAddress);
                    validInputAddresses.AddRange(adr.OutputWordAddress);
                    validInputAddresses.AddRange(adr.IntWordAddress);
                    foreach (string operand in inputOperands)
                    {
                        var trimmedOperand = operand.Trim();
                        if (!validInputAddresses.Contains(trimmedOperand))
                        {
                            // throw exception
                            errorFound = true;
                            arithmeticExceptionsOccured.Add(new CompilerException("Arithmetic Function Block: Invalid Input Address '" + trimmedOperand + "'"));
                            // throw new CompilerException(" Exception Occured" + trimmedOperand + "This is not in a proper format");
                        }
                    }
                }
                else
                {
                    errorFound = true;
                    arithmeticExceptionsOccured.Add(new CompilerException("Arithmetic Function Block: Input operands error"));
                }
                var outputExp = expression.Split('=')[0].Trim();
                if (outputExp.Length > 0)
                {
                    Addresses adr = new Addresses();
                    List<string> validOutputAddresses = new List<string> { };
                    validOutputAddresses.AddRange(adr.OutputWordAddress);
                    validOutputAddresses.AddRange(adr.IntWordAddress);
                    var trimmedOperand = outputExp.Trim();
                    if (!validOutputAddresses.Contains(trimmedOperand))
                    {
                        // throw exception
                        //throw new CompilerException("Arithmetic Output Address error.");
                        errorFound = true;
                        arithmeticExceptionsOccured.Add(new CompilerException("Arithmetic Function Block: Invalid Output Address '" + trimmedOperand + "'"));
                    }
                }
                else
                {
                    // throw exception
                    //throw new CompilerException(" Exception Occured at output address");
                    errorFound = true;
                    arithmeticExceptionsOccured.Add(new CompilerException("Arithmetic Function Block: Output operand error"));
                }
            }
            else
            {
                // throw exception
                //throw new CompilerException(" Exception Occured at Operator");
                errorFound = true;
                arithmeticExceptionsOccured.Add(new CompilerException("Arithmetic Function Block: Operands error"));
            }

            if (errorFound)
            {
                return (false, arithmeticExceptionsOccured);
            }
            return (true, null);
        }
        public static (bool, List<Exception>) TimerBlockExceptionCheck(string expression)
        {

            var timerExceptionsOccured = new List<Exception>();//List of exceptions are added here
            var errorFound = false;
            if (expression.Contains("("))
            {
                expression = expression.Replace("(", "");
            }
            if (expression.Contains(')'))
            {
                expression = expression.Replace(")", "");
            }
            if (expression.Contains("AND"))
            {
                expression = expression.Replace("AND", "");
            }
            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> timerOperators = ol.TimerOperatorsList;


            bool containsOperator = false;
            string optor = "";

            foreach (string op in timerOperators)
            {
                if (expression.Contains(op))
                {
                    containsOperator = true;
                    optor = op;
                    break;
                }
            }
            if (containsOperator)
            {
                var inputExp = expression.Split('=')[1].Trim();
                var inputExpSplit = inputExp.Split(new[] { optor }, StringSplitOptions.None);
                if (inputExpSplit.Length < 2)
                {
                    errorFound = true;
                    timerExceptionsOccured.Add(new CompilerException("Timer Block error"));
                }
                else
                {
                    var timerName = inputExpSplit[0];
                    if (timerName.Replace(":", string.Empty) == "")
                    {
                        errorFound = true;
                        timerExceptionsOccured.Add(new CompilerException("Timer Block: Block name error"));
                    }
                    var inputOperand = inputExpSplit[1];
                    if (inputOperand != "")
                    {
                        Addresses adr = new Addresses();
                        List<string> validInputAddresses = new List<string> { };
                        validInputAddresses.AddRange(adr.InputWordAddress);
                        validInputAddresses.AddRange(adr.OutputWordAddress);
                        validInputAddresses.AddRange(adr.IntWordAddress);
                        var trimmedOperand = inputOperand.Trim();
                        if (!validInputAddresses.Contains(trimmedOperand))
                        {
                            timerExceptionsOccured.Add(new CompilerException("Timer Block: Invalid Input Address '" + trimmedOperand + "'"));
                            errorFound = true;
                        }
                    }
                    else
                    {
                        timerExceptionsOccured.Add(new CompilerException("Timer Block: Input Operands error"));
                        errorFound = true;
                    }
                }
                var outputExp = expression.Split('=')[0].Trim();
                if (outputExp != "")
                {
                    Addresses adr = new Addresses();
                    List<string> validOutputAddresses = new List<string> { };
                    validOutputAddresses.AddRange(adr.OutputWordAddress);
                    validOutputAddresses.AddRange(adr.IntWordAddress);

                    var trimmedOperand = outputExp.Trim();
                    if (!validOutputAddresses.Contains(trimmedOperand))
                    {
                        timerExceptionsOccured.Add(new CompilerException("Timer Block: Invalid Output Address '" + trimmedOperand + "'"));
                        errorFound = true;
                    }
                }
                else
                {
                    timerExceptionsOccured.Add(new CompilerException("Timer Block: Output Operands error"));
                    errorFound = true;
                }
            }
            else
            {
                errorFound = true;
                timerExceptionsOccured.Add(new CompilerException("Timer Block: Operands error"));
            }
            if (errorFound)
            {
                return (false, timerExceptionsOccured);
            }
            return (true, null);
        }
        public static (bool, List<Exception>) LimitBlockExceptionCheck(string expression)
        {
            //List of exceptions are added here
            var limitExceptionsOccured = new List<Exception>();
            var errorFound = false;
            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> limitOperators = ol.LimitOperatorsList;
            if (expression.Contains('('))
            {
                expression.Replace("(", "");
            }
            if (expression.Contains(')'))
            {
                expression.Replace(")", "");
            }
            bool containsOperator = false;
            string optor = "";

            foreach (string op in limitOperators)
            {
                if (expression.Contains(op))
                {
                    containsOperator = true;
                    optor = op;
                    break;
                }
            }

            if (containsOperator)
            {
                var outputExp = expression.Split(new[] { optor }, StringSplitOptions.None)[0].Trim();
                var inputExp = expression.Split(new[] { optor }, StringSplitOptions.None)[1].Trim();

                var limitInputExpr = inputExp.Split(' ');
                var limitInputOp = limitInputExpr.Where(o => o != "").ToArray();
                if (limitInputOp.Length < 3)
                {
                    limitExceptionsOccured.Add(new CompilerException("Limit Block: Input Operands error"));
                    errorFound = true;
                }
                else
                {
                    Addresses adr = new Addresses();
                    List<string> validInputAddresses = new List<string> { };
                    validInputAddresses.AddRange(adr.InputWordAddress);
                    validInputAddresses.AddRange(adr.OutputWordAddress);
                    validInputAddresses.AddRange(adr.IntWordAddress);
                    foreach (string operand in limitInputOp)
                    {
                        var trimmedOperand = operand.Trim();
                        if (!validInputAddresses.Contains(trimmedOperand))
                        {
                            if (trimmedOperand == "")
                            {
                                continue;
                            }
                            else
                            {
                                limitExceptionsOccured.Add(new CompilerException("Limit Block: Invalid Input Address '" + trimmedOperand + "'"));
                                errorFound = true;
                            }
                        }
                    }
                }

                if (outputExp != "")
                {
                    Addresses adr = new Addresses();
                    List<string> validOutputAddresses = new List<string> { };
                    validOutputAddresses.AddRange(adr.OutputWordAddress);
                    validOutputAddresses.AddRange(adr.IntWordAddress);
                    var trimmedOperand = outputExp.Trim();
                    if (!validOutputAddresses.Contains(trimmedOperand))
                    {
                        limitExceptionsOccured.Add(new CompilerException("Limit Block: Invalid Output Address '" + trimmedOperand + "'"));
                        errorFound = true;
                    }
                }
                else
                {
                    limitExceptionsOccured.Add(new CompilerException("Limit Block: Output operands error"));
                    errorFound = true;
                }
            }
            else
            {
                limitExceptionsOccured.Add(new CompilerException("Limit Block: Operands error"));
                errorFound = true;
            }
            if (errorFound)
            {
                return (false, limitExceptionsOccured);
            }
            return (true, null);
        }
        public static (bool, List<Exception>) CompareBlockExceptionCheck(string expression)
        {
            var compareExceptionsOccured = new List<Exception>();
            var errorFound = false;

            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> compareOperators = ol.CompareOperatorsList;
            if (expression.Contains('('))
            {
                expression = expression.Replace("(", string.Empty);
            }
            if (expression.Contains(')'))
            {
                expression = expression.Replace(")", string.Empty);

            }
            bool containsOperator = false;
            string optor = "";

            foreach (string op in compareOperators)
            {
                if (expression.Contains(op))
                {
                    containsOperator = true;
                    optor = op;
                    break;
                }
            }
            if (containsOperator)
            {
                var input1Exp = expression.Split(new[] { optor }, StringSplitOptions.None)[0].Trim();
                var input2Exp = expression.Split(new[] { optor }, StringSplitOptions.None)[1].Trim();
                //var trimmedInputOperand = inputExp.Trim();
                List<string> validInputWordAddress = new List<string>();
                List<string> validInputBitAddresses = new List<string> { };
                string operandType = null;

                Addresses adr = new Addresses();

                validInputBitAddresses.AddRange(adr.InputBitAddress);
                validInputBitAddresses.AddRange(adr.OutputBitAddress);
                validInputBitAddresses.AddRange(adr.FlagsMemoryBitAddress);

                validInputWordAddress.AddRange(adr.InputWordAddress);
                validInputWordAddress.AddRange(adr.OutputWordAddress);
                validInputWordAddress.AddRange(adr.IntWordAddress);
                if (input1Exp != "")
                {
                    if (validInputBitAddresses.Contains(input1Exp))
                    {
                        operandType = "bit";

                    }
                    else if (validInputWordAddress.Contains(input1Exp))
                    {
                        operandType = "word";
                    }
                    else
                    {
                        errorFound = true;
                        compareExceptionsOccured.Add(new CompilerException("Compare Block: Invalid input address '" + input1Exp + "'"));
                    }
                }
                else
                {
                    errorFound = true;
                    compareExceptionsOccured.Add(new CompilerException("Compare Block: Invalid input address"));
                }
                if (input2Exp != "")
                {
                    if (!validInputBitAddresses.Contains(input1Exp) && !(validInputWordAddress.Contains(input1Exp)))
                    {
                        errorFound = true;
                        compareExceptionsOccured.Add(new CompilerException("Compare Block: Invalid input address '" + input2Exp + "'"));
                    }
                    if (!(operandType == "bit" && validInputBitAddresses.Contains(input2Exp)) && !(operandType == "word" && validInputWordAddress.Contains(input2Exp)))
                    {
                        errorFound = true;
                        compareExceptionsOccured.Add(new CompilerException("Compare Block: Input addresses data type mismatch"));
                    }
                }
                else
                {
                    compareExceptionsOccured.Add(new CompilerException("Compare Block: Invalid input address"));
                    errorFound = true;
                }
            }
            else
            {
                compareExceptionsOccured.Add(new CompilerException("Compare Block: Operands error"));
                errorFound = true;
            }

            if (errorFound)
            {
                return (false, compareExceptionsOccured);
            }
            return (true, null);
        }
        public static (bool, List<Exception>) BitShiftBlockExceptionCheck(string expression)
        {
            var bitshiftExceptionsOccured = new List<Exception>();
            var errorOccured = false;

            if (expression.Contains("("))
            {
                expression = expression.Replace(")", "");
            }
            if (expression.Contains(")"))
            {
                expression = expression.Replace(")", "");
            }
            if (expression.Contains("AND"))
            {
                expression = expression.Replace("AND", "");
            }

            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> bitshiftoperators = ol.BitShiftOperatorsList;

            bool containsOperator = false;
            string optor = "";

            foreach (string op in bitshiftoperators)
            {
                if (expression.Contains(op))
                {
                    containsOperator = true;
                    optor = op;
                    break;
                }
            }
            if (containsOperator)
            {
                var inputExp = expression.Split('=')[1].Trim();
                var inputExpSplit = inputExp.Trim().Split(' ');
                var inputOperands = inputExpSplit.Where(i => (i != "" && !bitshiftoperators.Contains(i))).ToArray();
                if (inputOperands.Length < 2)
                {
                    errorOccured = true;
                    bitshiftExceptionsOccured.Add(new CompilerException("BitShift Block: Operands error"));
                }
                else
                {
                    Addresses adr = new Addresses();
                    List<string> validInputAddresses = new List<string> { };
                    validInputAddresses.AddRange(adr.InputWordAddress);
                    validInputAddresses.AddRange(adr.OutputWordAddress);
                    validInputAddresses.AddRange(adr.IntWordAddress);
                    foreach (string operand in inputOperands)
                    {
                        var trimmedOperand = operand.Trim();
                        if (!validInputAddresses.Contains(trimmedOperand))
                        {
                            bitshiftExceptionsOccured.Add(new CompilerException("BitShift Block: Invalid Input Address '" + trimmedOperand + "'"));
                            errorOccured = true;
                        }
                    }
                }
                var outputExp = expression.Split('=')[0].Trim();
                if (outputExp != "")
                {
                    Addresses adr = new Addresses();
                    List<string> validOutputAddresses = new List<string> { };
                    validOutputAddresses.AddRange(adr.OutputWordAddress);
                    validOutputAddresses.AddRange(adr.IntWordAddress);
                    var trimmedOperand = outputExp.Trim();
                    if (!validOutputAddresses.Contains(trimmedOperand))
                    {
                        bitshiftExceptionsOccured.Add(new CompilerException("BitShift Block: Invalid Output Address '" + trimmedOperand + "'"));
                        errorOccured = true;
                    }
                }
                else
                {
                    errorOccured = true;
                    bitshiftExceptionsOccured.Add(new CompilerException("BitShift Block: Output operand error"));
                }
            }
            else
            {
                bitshiftExceptionsOccured.Add(new CompilerException("BitShift Block: Operands error"));
                errorOccured = true;
            }
            if (errorOccured)
            {
                return (false, bitshiftExceptionsOccured);
            }
            return (true, null);
        }
        public static (bool, List<Exception>) FlipFlopBlockExceptionCheck(string expression)
        {
            var flipflopExceptionsOccured = new List<Exception>();
            var errorOccured = false;

            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> flipflopoperators = ol.FlipFlopOperatorsList;
            if (expression.Contains('('))
            {
                expression = expression.Replace("(", string.Empty);
            }
            if (expression.Contains(')'))
            {
                expression = expression.Replace(")", string.Empty);
            }
            bool containsOperator = false;
            string optor = "";

            foreach (string op in flipflopoperators)
            {
                if (expression.Contains(op))
                {
                    containsOperator = true;
                    optor = op;
                    break;
                }
            }
            if (containsOperator)
            {
                var inputExp = expression.Split(new[] { optor }, StringSplitOptions.None)[1].Trim();
                var blockName = expression.Split(new[] { optor }, StringSplitOptions.None)[0].Replace(":", string.Empty).Trim();
                if (blockName == "")
                {
                    errorOccured = true;
                    flipflopExceptionsOccured.Add(new CompilerException("Flipflop Block: Block name error"));
                }
                var inputOperands = inputExp.Split(' ');
                if (inputOperands.Length != 1)
                {
                    flipflopExceptionsOccured.Add(new CompilerException("Flipflop Block: Operands error"));
                    errorOccured = true;
                } else
                {
                    Addresses adr = new Addresses();
                    List<string> validInputAddresses = new List<string> { };
                    validInputAddresses.AddRange(adr.InputBitAddress);
                    validInputAddresses.AddRange(adr.OutputBitAddress);
                    validInputAddresses.AddRange(adr.FlagsMemoryBitAddress);
                    var trimmedOperand = inputOperands[0].Trim();
                    if (!validInputAddresses.Contains(trimmedOperand))
                    {
                        flipflopExceptionsOccured.Add(new CompilerException("Flipflop Block: Invalid Input Address '" + trimmedOperand + "'"));
                        errorOccured = true;
                    }
                }
            } else
            {
                flipflopExceptionsOccured.Add(new CompilerException("Flipflop Block: Operands error"));
                errorOccured = true;
            }
            if (errorOccured)
            {
                return (false, flipflopExceptionsOccured);
            }
            return (true, null);
        }
        public static (bool, List<Exception>) LogicalBlockExceptionCheck(string expression)
        {
            var logicalExceptionsOccured = new List<Exception>();
            var errorOccured = false;
            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> logicalBlockOperators = ol.LogicalBlockOperatorsList;
            if (expression.Contains('('))
            {
                expression = expression.Replace("(", string.Empty);
            }
            else if (expression.Contains(')'))
            {
                expression = expression.Replace(")", string.Empty);

            }
            bool containsOperator = false;
            string optor = "";

            foreach (string op in logicalBlockOperators)
            {
                if (expression.Contains(op))
                {
                    containsOperator = true;
                    optor = op;
                    break;
                }
            }
            if (containsOperator)
            {
                var inputExp = expression.Split(new[] { optor }, StringSplitOptions.None)[1].Trim();
                var inputOperands = inputExp.Split(' ');
                if (inputOperands.Length > 0)
                {
                    Addresses adr = new Addresses();
                    List<string> validInputAddresses = new List<string> { };
                    validInputAddresses.AddRange(adr.InputBitAddress);
                    validInputAddresses.AddRange(adr.OutputBitAddress);
                    validInputAddresses.AddRange(adr.FlagsMemoryBitAddress);
                    foreach (string operand in inputOperands)
                    {
                        var trimmedOperand = operand.Trim();
                        if (!validInputAddresses.Contains(trimmedOperand))
                        {   
                            logicalExceptionsOccured.Add(new CompilerException("Logical Block: Invalid Input Address '" + trimmedOperand + "'"));
                            errorOccured = true;
                        }
                    }
                } else
                {
                    logicalExceptionsOccured.Add(new CompilerException("Logical Block: Input operands error"));
                    errorOccured = true;
                }
            } else
            {
                logicalExceptionsOccured.Add(new CompilerException("Logical Block: Operands error"));
                errorOccured = true;
            }
            if (errorOccured)
            {
                return (false, logicalExceptionsOccured);
            }
            else
            {
                return (true, null);
            }
        }
        public static (bool, List<Exception>) CounterBlockExceptionCheck(string expression)
        {
            var counterExceptionsOccured = new List<Exception>();
            var errorOccured = false;
            if (expression.Contains('('))
            {
                expression = expression.Replace("(", string.Empty);
            }
            else if (expression.Contains(')'))
            {
                expression = expression.Replace(")", string.Empty);
            }
            InterpreterOperatorList ol = new InterpreterOperatorList();
            List<string> counteroperators = ol.CounterOperatorsList;
            bool containsOperator = false;
            string optor = "";
            foreach (string op in counteroperators)
            {
                if (expression.Contains(op))
                {
                    containsOperator = true;
                    optor = op;
                    break;
                }
            }
            if (containsOperator)
            {
                var inputExp = expression.Split('=')[1].Trim();
                var inputExpSplit = inputExp.Split(' ');
                var inputOperands = inputExpSplit.Where(i => i != "").ToArray();
                if (inputOperands.Length != 3)
                {
                    errorOccured = true;
                    counterExceptionsOccured.Add(new CompilerException("Counter Block: Operands error"));
                }
                else
                {
                    var input1 = inputOperands[0].Trim();
                    if (input1 == "")
                    {
                        errorOccured = true;
                        counterExceptionsOccured.Add(new CompilerException("Counter Block: Input operand error"));
                    }
                    else
                    {
                        Addresses adr = new Addresses();
                        List<string> validInputAddresses = new List<string> { };
                        validInputAddresses.AddRange(adr.InputBitAddress);
                        validInputAddresses.AddRange(adr.OutputBitAddress);
                        validInputAddresses.AddRange(adr.FlagsMemoryBitAddress);
                        if (!validInputAddresses.Contains(input1))
                        {
                            errorOccured = true;
                            counterExceptionsOccured.Add(new CompilerException("Counter Block: Invalid Input address '" + input1 + "'"));
                        }
                    }
                    var counterName = inputOperands[1].Trim().Split(':')[0];
                    if (counterName == "")
                    {
                        errorOccured = true;
                        counterExceptionsOccured.Add(new CompilerException("Counter Block: Block name error"));
                    }
                    var input2 = inputOperands[2].Trim();
                    if (input2 == "")
                    {
                        errorOccured = true;
                        counterExceptionsOccured.Add(new CompilerException("Counter Block: Input operand error"));
                    }
                    else
                    {
                        Addresses adr = new Addresses();
                        List<string> validInputAddresses = new List<string> { };
                        validInputAddresses.AddRange(adr.InputWordAddress);
                        validInputAddresses.AddRange(adr.OutputWordAddress);
                        validInputAddresses.AddRange(adr.IntWordAddress);
                        if (!validInputAddresses.Contains(input2))
                        {
                            errorOccured = true;
                            counterExceptionsOccured.Add(new CompilerException("Counter Block: Invalid Input address '" + input2 + "'"));
                        }
                    }
                }
                var outputExp = expression.Split('=')[0].Trim();
                if (outputExp != "")
                {
                    Addresses adr = new Addresses();
                    List<string> validOutputAddresses = new List<string> { };
                    validOutputAddresses.AddRange(adr.OutputWordAddress);
                    validOutputAddresses.AddRange(adr.IntWordAddress);
                    var trimmedOperand = outputExp.Trim();
                    if (!validOutputAddresses.Contains(trimmedOperand))
                    {
                        counterExceptionsOccured.Add(new CompilerException("Counter Block: Invalid Output Address '" + trimmedOperand + "'"));
                        errorOccured = true;
                    }
                }
                else
                {
                    counterExceptionsOccured.Add(new CompilerException("Counter Block: Output operand error"));
                    errorOccured = true;
                }
            }
            else
            {
                counterExceptionsOccured.Add(new CompilerException("Counter Block: Operands error"));
                errorOccured = true;
            }
            if (errorOccured)
            {
                return (false, counterExceptionsOccured);
            }
            return (true, null);
        }

    }
}
