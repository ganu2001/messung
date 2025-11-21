using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMPS2000.Core.Base;
using XMPS2000.Core.LadderLogic;

namespace XMPS2000.Core.App
{
    class CSVHelper
    {

        private XMPS xm = XMPS.Instance;

        private int CurD10RefNumber = 0;
        private int InputLimit = 4;

        // For window name column
        private bool isFirstBlockRow = true;
        private Dictionary<string, int> _curBlocksNameDic = new Dictionary<string, int>();
        private string _curBlockName;

        private List<Tuple<int, string>> errorList = new List<Tuple<int, string>>();
        private Tuple<int, string> _diagnostics;

        private Dictionary<string, string> tagToAddress = new Dictionary<string, string>();
        //
        private string dataTypeExpression = string.Empty;
        private string datatypeCurrentInput = string.Empty;

        //adding flag for checking current instruction is DataConversion.
        private bool isDataConversionInstruction = false;
        /// <summary>
        /// Fill data into row for Set and Reset operations for coil
        /// </summary>
        /// <param name="dt">Refrence of DataTable</param>
        /// <param name="key">The output value in column, or key from the keyvalues pair</param>
        /// <param name="expression">The input values in the column</param>
        /// <returns></returns>
        private DataRow SetResetData(ref DataTable dt, string key, string expression)
        {
            string setResetVal = "";
            if (expression.Contains('{'))
            {
                DataRow drEdg = dt.NewRow();

                int dictionaryKey = CurD10RefNumber;
                CurD10RefNumber++;
                string opadd = $"D10:{dictionaryKey:0000}";
                drEdg["Input1"] = expression.Contains('{') ? expression.Substring(0, expression.IndexOf('{')) : expression;
                drEdg["Input2"] = "-";
                drEdg["Input3"] = "-";
                drEdg["Input4"] = "-";
                drEdg["Op Code"] = expression.Contains("{P") ? "0170" : "0180";
                drEdg["Output1"] = opadd;
                drEdg["DataType"] = "000";
                drEdg["T/C Name"] = "-";
                drEdg["OutputType"] = 0;
                drEdg["Enable"] = "-";
                if (isFirstBlockRow)
                {
                    drEdg["WindowName"] = _curBlockName;
                    isFirstBlockRow = false;
                }
                dt.Rows.Add(drEdg);
                expression = opadd;
            }
            DataRow dr = dt.NewRow();
            dr["Output1"] = key.Split('@').First();
            setResetVal = key.Replace(key.Split('@').First(), "");
            setResetVal = setResetVal.Replace("@", "").Replace("{", "").Replace("}", "");
            dr[$"Input1"] = expression;

            // Contact and coil will have only two OP Codes
            //For S coil 0x2E0 & For R coil 0x2F0
            if (setResetVal == "S")
                dr["Op Code"] = "0310";
            else
                dr["Op Code"] = "0320";
            // For bool value
            dr["DataType"] = "0000";

            // For bool value
            dr["OutputType"] = "00";

            // Blank records
            dr["Output2"] = "-";
            dr[$"Input2"] = "-";
            dr[$"Input3"] = "-";
            dr[$"Input4"] = "-";
            dr["Enable"] = "-";
            dr["T/C Name"] = "-";

            // For Window Name
            if (isFirstBlockRow)
            {
                dr["WindowName"] = _curBlockName;
                isFirstBlockRow = false;
            }
            return dr;
        }

        /// <summary>
        /// Open the UDFB logic and replce input and output variables used in main function within UDFB logic and pass these values as rungs
        /// </summary>
        /// <param name="rung"></param> Current rung from which UDFB logic is to be derived.
        /// <returns></returns> list of complete UDFB logic rungs.
        private List<string> AddUDFBDetailstoData(string rung)
        {
            List<string> udfblines = new List<string> { };
            List<string> convertedfn = new List<string> { };

            string functionname = rung.Substring(rung.IndexOf("FN"));
            functionname = functionname.Substring(0, functionname.IndexOf(" ")).Replace("FN:", "");
            List<List<string>> actfunction = xm.LoadedProject.Blocks.Where(N => N.Name == functionname + " Logic").Select(B => B.Elements).ToList();
            List<string> udfbtexts = xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == functionname.ToString()).FirstOrDefault().UDFBlocks.Where(S => S.Type == "Input").Select(T => T.Text).ToList();
            Dictionary<string, string> mapvalues = new Dictionary<string, string> { };
            string strsplit = rung.Split(new[] { "DT" }, StringSplitOptions.None)[0];
            for (int i = 0; i < udfbtexts.Count; i++)
            {
                mapvalues.Add(udfbtexts[i], strsplit.Split(new[] { "IN:" }, StringSplitOptions.None)[i + 1]);
            }
            int inputs = udfbtexts.Count;
            udfbtexts.AddRange(xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == functionname.ToString()).FirstOrDefault().UDFBlocks.Where(S => S.Type == "Output").Select(T => T.Text).ToList());
            strsplit = rung.Split(new[] { "OPTN" }, StringSplitOptions.None)[1];
            for (int i = 0; i < udfbtexts.Count - inputs; i++)
            {
                mapvalues.Add(udfbtexts[i + inputs], strsplit.Split(new[] { "OP:" }, StringSplitOptions.None)[i + 1].Replace("]);", ""));
            }
            foreach (string corformula in actfunction[0])
            {
                if (!corformula.StartsWith("'")) /// Skip the rungs starting with comment 
                {
                    string finalformula = corformula.Replace("]", " ]");

                    // Use regex to replace mapvalues keys with values +(*) asterisk
                    foreach (var kvp in mapvalues)
                    {
                        string key = Regex.Escape(kvp.Key);
                        string value = kvp.Value.Trim();
                        //\b is for Word Boundary for exactly match the key.
                        finalformula = Regex.Replace(finalformula, $@"\b{key}\b", $"{value}*");
                    }
                    udfblines.Add(finalformula.Replace("~~", "~"));
                }
            }
            return udfblines;
        }

        /// <summary>
        /// Convert the rung equation from tag name to its corresponding address and check if the equation is valid or not
        /// </summary>
        /// <param name="_coils">The LSH part of the rung expression, everyting before the '=' sign -> () = ();</param>
        /// <returns>List of coils</returns>
        private List<string> SimplifyCoil(string _coils)
        {
            List<string> coils = new List<string>();

            int i = -1;
            int _position = -1;

            string _curWord;
            List<string> _coilsList = new List<string>();

            while (true)
            {
                i++;
                _position++;
                if (i >= _coils.Length)
                    break;

                if (_coils[i] == '(')
                    continue;

                if (_coils[i] == ')')
                    continue;

                if (_coils[i] == ',')
                    continue;

                if (_coils[i] == ' ')
                    continue;

                while (_coils[_position + 1] != '(' && _coils[_position + 1] != ')' && _coils[_position + 1] != ',' && _coils[_position + 1] != ' ')
                    _position++;

                _curWord = _coils.Substring(i, _position - i + 1);
                i = _position;

                coils.Add(_curWord);
            }

            return coils;
        }

        /// <summary>
        /// Gives block index for window name column
        /// </summary>
        /// <param name="blockName">Current bock name</param>
        /// <returns>Int value respective to current block name</returns>
        private int GetBlockIndex(string blockName)
        {
            if (_curBlocksNameDic.ContainsKey(blockName))
                return _curBlocksNameDic[blockName];
            else
                _curBlocksNameDic[blockName] = _curBlocksNameDic.Count() + 1;

            return _curBlocksNameDic[blockName];
        }

        private bool LogicalAddressContain(string add, string uDFBName = null)
        {
            string tag = add;
            int count = 0;
            int count1 = 0;
            if (add.StartsWith("~"))
            {
                tag = tag.Replace("~", "");
            }
            if (add.Contains("]"))
            {
                tag = tag.Replace("]", "");
            }
            count = xm.LoadedProject.Tags.Where(x => x.LogicalAddress.Equals(tag.Replace("*", ""))).Count();
            if (count <= 0)
            {
                if (!(tag.Contains(".")) && (tag.StartsWith("Q") || tag.StartsWith("I")))
                {
                    count1 = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == tag + ".00").Count();
                }
            }
            else if (uDFBName != null && !add.EndsWith("*"))
            {
                var xMIOConfig = xm.LoadedProject.Tags.Where(x => x.LogicalAddress.Equals(tag)).FirstOrDefault();
                if (xMIOConfig.Model != uDFBName + " Tags" && xMIOConfig.IoList == XMPS2000.Core.Types.IOListType.NIL)
                    return false;
            }

            if (count > 0 || count1 > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Function to tell if the number of elements in expression exceed the limit
        /// </summary>
        /// <param name="exp">The expression to check for limit</param>
        /// <param name="operation">The operation in the expression</param>
        /// <param name="limit">For custom limit checking</param>
        /// <returns>(True/False if the elements exceed limit, list of elements)</returns>
        private (bool, string[]) IsOverLimit(string exp, string operation, int limit = -1)
        {
            limit = limit != -1 ? limit : InputLimit;

            string[] expElements = exp.Replace(operation, null).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (limit < expElements.Count())
                return (true, expElements);

            return (false, expElements);
        }

        /// <summary>
        /// Convert the complex equation (equation containing '(' brackets) into D10 address
        /// </summary>
        /// <param name="_rung">Only the RHS part of the rung expression, everything after the '=' sign -> () = ();</param>
        /// <returns>(Simplified equation, Dictionary<D10:address, complex expression>)</returns>
        private (string, Dictionary<string, string>) SimplifyEquation(string _rung)
        {
            string exp = _rung.Replace(';', ' ');
            exp = exp.Remove(0, 2).Trim();
            exp = exp.Remove(exp.Length - 1, 1).Trim();

            // Replace double whitespace with single
            exp = exp.Replace("  ", " ");

            Dictionary<string, string> simplifyDictionary = new Dictionary<string, string>();
            string myExp = exp;
            List<string> rung = new List<string>();
            List<string> operatorList = new List<string> { "AND", "OR" };
            int dictionaryKey = 1;
            int i = -1;

            while (myExp.Contains('(') || myExp.Contains(')') || myExp.Contains('[') || myExp.Contains(']'))
            {
                i++;
                if (myExp[i] == ' ')
                    continue;
                if (myExp[i] == '(')
                {
                    for (int j = i + 1; j < myExp.Length; j++)
                    {
                        if (myExp[j] == '(')
                            break;
                        if (myExp[j] == '[')
                            break;
                        else if (myExp[j] == ')')
                        {
                            string str = myExp.Substring(i + 1, j - i - 1);
                            string trimmedStr = str.Trim();
                            bool flag = false;
                            foreach (string s in operatorList)
                            {
                                if (trimmedStr.Contains(s))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                                myExp = myExp.Replace(myExp.Substring(i, str.Length + 2), trimmedStr);
                            else
                            {
                                foreach (string operation in operatorList)
                                {
                                    if (trimmedStr.Contains(operation))
                                    {
                                        (bool overLimit, string[] elements) = IsOverLimit(trimmedStr, operation);
                                        if (overLimit)
                                        {
                                            dictionaryKey = CurD10RefNumber;
                                            CurD10RefNumber++;
                                            string p = $"D10:{dictionaryKey:0000}";
                                            simplifyDictionary.Add(p, string.Join($" {operation} ", elements.Take(InputLimit)));

                                            int skipCount = InputLimit;
                                            for (int _indx = 0; _indx < elements.Count() / (InputLimit - 1); _indx++)
                                            {
                                                if (elements.Skip(skipCount).Any())
                                                {
                                                    dictionaryKey = CurD10RefNumber;
                                                    CurD10RefNumber++;
                                                    p = $"D10:{dictionaryKey:0000}";
                                                    simplifyDictionary.Add(p, $"D10:{dictionaryKey - 1:0000} {operation} {string.Join($" {operation} ", elements.Skip(skipCount).Take(InputLimit - 1))}");

                                                    skipCount += InputLimit - 1;
                                                }
                                            }

                                            myExp = myExp.Replace(trimmedStr, p);
                                            rung.Add(p + " = " + trimmedStr);
                                        }
                                        else
                                        {
                                            if (trimmedStr.Contains("{"))
                                            {
                                                string newstring = ExtractString(trimmedStr, ref CurD10RefNumber, ref simplifyDictionary, ref rung);
                                                myExp = myExp.Replace(trimmedStr, newstring);
                                                trimmedStr = newstring;
                                            }
                                            dictionaryKey = CurD10RefNumber;
                                            CurD10RefNumber++;

                                            string p = $"D10:{dictionaryKey:0000}";
                                            myExp = myExp.Replace(trimmedStr, p);
                                            rung.Add(p + " = " + trimmedStr);
                                            simplifyDictionary.Add(p, trimmedStr);
                                        }
                                    }
                                }
                            }
                            i = -1;
                            break;
                        }
                    }
                    continue;
                }
                if (myExp[i] == '[')
                {
                    for (int j = i + 1; j < myExp.Length; j++)
                    {
                        if (myExp[j] == ']')
                        {
                            string str = myExp.Substring(i + 1, j - i - 1);
                            string trimmedStr = str.Trim();
                            dictionaryKey = rung.Count();
                            string p = $"FN_KEY{dictionaryKey}";
                            myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                            rung.Add(p + " = " + trimmedStr);
                            simplifyDictionary.Add(p, trimmedStr);
                            i = -1;
                            break;
                        }
                    }
                }
            }
            return (myExp, simplifyDictionary);
        }

        public string ExtractString(string input, ref int CurD10RefNumber, ref Dictionary<string, string> simplifyDictionary, ref List<string> rung)
        {
            String[] val = input.Split(' ');
            string newval = "";
            foreach (string address in val)
            {
                newval = newval + " " + address;
            }
            return newval;
        }

        /// <summary>
        /// Convert the function block expression into csv data row
        /// </summary>
        /// <param name="dt">Refrence of DataTable to get the column data</param>
        /// <param name="expression">Function block expression</param>
        /// <param name="EnableAddress">If the block is enabled then pass the main expression</param>
        /// <returns>DataRow with data in the form of the provided DataTable</returns>
        private List<DataRow> FunctionBlockConversion(ref DataTable dt, string expression, string EnableAddress = null)
        {
            List<DataRow> rows = new List<DataRow>();

            bool inputLimitReach = expression.Split(' ').Count(x => x.Contains("IN:") && x.Replace("IN:", "") != "-" && x.Replace("IN:", "") != "") > 4;
            List<string> FunctionBlock = expression.Split(' ').ToList();

            bool isPIDFunctionBlock = expression.Substring(expression.IndexOf("OPC:"), 8).Equals("OPC:040E");
            int _inputNo = 1;
            int _outputNo = 1;
            // Find the start of Function Name in the string
            int startIndexFN = expression.IndexOf("FN:");
            int endIndexFN = expression.IndexOf(" ", startIndexFN + 3);
            string instructionName = expression.Substring(startIndexFN, endIndexFN - startIndexFN);

            instructionName = instructionName.Split(':')[1];

            bool isScaleInstruction = inputLimitReach && instructionName.Equals("Scale");

            string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\NewInstructionFormat.xml");
            XDocument xmlDoc = XDocument.Load(filePath);
            string instructionText = instructionName.Equals("ANY@to@Dword") ? "ANY to DWORD" : instructionName;
            //Removing the PID instruction number from the function_name Attributes.
            instructionText = instructionText.StartsWith("MES_PID_") ? "MES_PID" : instructionText;
            InstructionTypeDeserializer instruction = xm.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionText.Replace("@", " ")));
            string instructionType = instruction.InstructionType;
            var inputOutputs = instructionText.Replace("@", " ").Equals("MQTT Publish") ? null : instruction.InputsOutputs.Select(t => t.DataType.ToString()).ToList();

            int startIndexDT = expression.IndexOf("DT:");
            int endIndexDT = expression.IndexOf(" ", startIndexDT + 3);
            dataTypeExpression = expression.Substring(startIndexDT, endIndexDT - startIndexDT);
            dataTypeExpression = dataTypeExpression.Split(':')[1];

            int inputOperandsCount = instruction.InputsOutputs.Where(t => t.Type.Equals("Input")).Count();
            int outputOperandsCount = instruction.InputsOutputs.Where(t => t.Type.Equals("Output")).Count();

            int dtRowsCount = instruction.InstructionType.Equals("ReadProperty") ? 3
                              : Math.Max((int)Math.Ceiling(inputOperandsCount / 4.0), (int)Math.Ceiling(outputOperandsCount / 2.0));

            // Create list to store dynamically generated rows
            List<DataRow> dataRows = new List<DataRow>();

            for (int i = 0; i < dtRowsCount; i++)
            {
                dataRows.Add(dt.NewRow());
            }
            foreach (string _curExp in FunctionBlock)
            {
                if (_curExp.Substring(0, 3) == "IN:")
                {
                    if (_inputNo > instruction.InputsOutputs.Where(t => t.Type.Equals("Input")).Count() && !instructionType.Equals("MQTT")
                        && !instructionType.Equals("Scale") && !instruction.Text.Equals("NULL"))
                        continue;
                    //Getting DataType ID code.
                    string inputDataType = inputOutputs != null ? inputOutputs.ElementAt(_inputNo - 1) : string.Empty;
                    if (inputDataType != "" && inputDataType != "Unknown" && inputDataType != "Schedule" && inputDataType != "Notification" && inputDataType != "Device")
                        datatypeCurrentInput = DataTypeDesilizer.List.FirstOrDefault(t => t.Text.Equals(inputDataType)).ID.Substring(2, 4);
                    else
                        datatypeCurrentInput = string.Empty;
                    bool isdataTypeDiff = (dataTypeExpression != datatypeCurrentInput && (datatypeCurrentInput == "0005" || (datatypeCurrentInput == "000C" && _curExp.Replace("IN:", "").StartsWith("-"))));

                    //adding check for data conversion instructions.
                    if (instructionName.StartsWith("ANY@to@") && (dataTypeExpression == "000F" ||
                        ((dataTypeExpression == "000E" || dataTypeExpression == "0030") && _curExp.Replace("IN:", "").StartsWith("-"))))
                    {
                        isdataTypeDiff = true;
                        isDataConversionInstruction = true;
                    }
                    else
                        isDataConversionInstruction = false;

                    instructionName = instruction.InstructionType.Equals("ReadProperty") ? instruction.InstructionType : instructionName;
                    string subInstructionName = expression.Substring(startIndexFN, endIndexFN - startIndexFN).Split(':')[1];
                    SaveInputs(_curExp.Replace("IN:", ""), isScaleInstruction, ref _inputNo, ref dataRows, isdataTypeDiff, instructionName, subInstructionName);
                }
                else if (_curExp.Substring(0, 3) == "OP:")
                {
                    if (_outputNo > instruction.InputsOutputs.Where(t => t.Type.Equals("Output")).Count() && !instructionType.Equals("MQTT") && !instructionType.Equals("Scale"))
                        continue;
                    SaveOutputs(_curExp.Replace("OP:", ""), isScaleInstruction, ref _outputNo, ref dataRows, instructionName);

                    if (instructionType.Equals("ReadProperty") && _outputNo == 3)
                    {
                        int currentIndex = FunctionBlock.IndexOf(_curExp) + 1;
                        //Calculating the DataRow Index as per Input count
                        int rowIndex = (_outputNo - 1) / 2;
                        // Define column mapping order
                        var columns = new List<string>
                                        {
                                            "Line Number", "T/C Name", "OutputType", "DataType", "Enable",
                                            "Output1", "Output2", "Op Code",
                                            "Input1", "Input2", "Input3", "Input4"
                                        };

                        // Fill data rows (2 rows since _outputNo == 3 implies 2 outputs)
                        for (int i = 0; i < 2; i++)
                        {
                            if (rowIndex + i >= dataRows.Count)
                                break;

                            DataRow dr = dataRows[rowIndex + i];
                            foreach (var column in columns)
                            {
                                string outputValue = string.Empty;
                                if (currentIndex < FunctionBlock.Count)
                                {
                                    outputValue = FunctionBlock[currentIndex++].Replace("OP:", "");
                                    outputValue = outputValue.Equals("A5:999") ? "-" : outputValue;
                                    dr[column] = outputValue;
                                }
                                else
                                {
                                    outputValue = "-";
                                    dr[column] = outputValue;
                                }

                            }
                        }
                        break;
                    }
                }
                else if (_curExp.Substring(0, 3) == "EN:")
                {
                    DataRow dr1 = dataRows[0];
                    SaveEnableInputData(_curExp, EnableAddress, ref isFirstBlockRow, ref dr1, ref dt, ref rows);
                    for (int i = 1; i < dataRows.Count; i++)
                    {
                        dataRows[i]["Enable"] = "-";
                    }
                }
                else if (_curExp.Substring(0, 3) == "DT:")
                {
                    SaveDataType(_curExp.Replace("DT:", ""), isScaleInstruction, ref dataRows);
                }
                else if (_curExp.Substring(0, 3) == "TC:")
                {
                    if (isPIDFunctionBlock)
                    {
                        int endIndex = expression.IndexOf(' ', expression.IndexOf("FN:"));
                        string pidFunctionNo = expression.Substring(expression.IndexOf("FN:"), endIndex - expression.IndexOf("FN:"));
                        string pidTCName = $"PID{pidFunctionNo.Split('_')[2]}";
                        SaveTCName(pidTCName, "MES_PID", ref dataRows);
                    }
                    else
                        SaveTCName(_curExp.Replace("TC:", ""), instructionName.Replace("@", " "), ref dataRows);
                }
                else if (_curExp.Substring(0, 4) == "OPT:")
                {
                    SaveOutputType(_curExp.Replace("OPT:", ""), isScaleInstruction, ref dataRows);
                }
                else if (_curExp.Substring(0, 4) == "OPC:")
                {
                    SaveOPCode(_curExp.Replace("OPC:", ""), isScaleInstruction, ref dataRows);
                }
            }

            if (FunctionBlock.Any(F => F.StartsWith("FN:ANY"))) dataRows[0]["DataType"] = dataRows[0]["Op Code"].ToString().Substring(3, 1);

            // For Window Name
            if (isFirstBlockRow)
            {
                dataRows[0]["WindowName"] = _curBlockName;
                isFirstBlockRow = false;
            }
            rows.AddRange(dataRows);
            return rows;
        }

        /// <summary>
        /// Saving the Enable Value in CSV File.
        /// </summary>
        /// <param name="_curExp"></param>
        /// <param name="EnableAddress"></param>
        /// <param name="isFirstBlockRow"></param>
        /// <param name="dr"></param>
        /// <param name="dt"></param>
        /// <param name="rows"></param>
        private void SaveEnableInputData(string _curExp, string EnableAddress, ref bool isFirstBlockRow, ref DataRow dr, ref DataTable dt, ref List<DataRow> rows)
        {
            if ((_curExp.Replace("EN:", "") == "Enabled" && EnableAddress != null) || EnableAddress != null)
            {
                if (EnableAddress.Contains('{'))
                {
                    DataRow drEdg = dt.NewRow();

                    int dictionaryKey = CurD10RefNumber;
                    CurD10RefNumber++;
                    string opadd = $"D10:{dictionaryKey:0000}";

                    drEdg["Input1"] = EnableAddress.Substring(0, EnableAddress.IndexOf('{'));
                    drEdg["Input2"] = "-";
                    drEdg["Input3"] = "-";
                    drEdg["Input4"] = "-";
                    drEdg["Op Code"] = EnableAddress.Contains("{P") ? "0170" : "0180";
                    drEdg["Output1"] = opadd;
                    drEdg["DataType"] = "000";
                    drEdg["T/C Name"] = "-";
                    drEdg["OutputType"] = 0;
                    drEdg["Enable"] = "-";
                    if (isFirstBlockRow)
                    {
                        drEdg["WindowName"] = _curBlockName;
                        isFirstBlockRow = false;
                    }
                    rows.Add(drEdg);
                    EnableAddress = opadd;
                }
                dr["Enable"] = EnableAddress;
            }
            else
            {
                dr["Enable"] = "-";
            }
        }
        /// <summary>
        /// Saving Inputs Values in CSV File.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="isforTwoRow"></param>
        /// <param name="_inputNo"></param>
        /// <param name="dataRows"></param>
        private void SaveInputs(string inputValue, bool isScaleInstruction, ref int _inputNo, ref List<DataRow> dataRows, bool isDataTypeDiff, string functionName, string subInstruction)
        {
            if (isDataTypeDiff)
            {
                if (!inputValue.Equals("") && !inputValue.Equals("-") && !inputValue.Contains(":"))
                {
                    float parsedFloat1 = float.Parse(inputValue);
                    long conveterdValues = ConvertFloatValuesToInt(parsedFloat1);
                    inputValue = conveterdValues.ToString();
                }
            }
            if (isScaleInstruction)
            {
                if (_inputNo > 3)
                {
                    DataRow dr2 = dataRows[1];
                    dr2[$"Input{_inputNo - 3}"] = inputValue;
                }
                else
                {
                    DataRow dr1 = dataRows[0];
                    dr1[$"Input{_inputNo}"] = inputValue;
                }
            }
            else
            {
                //Calculating the DataRow Index as per Input count
                int rowIndex = (_inputNo - 1) / 4;

                // Getting DataRow by indexing
                DataRow dr = dataRows[rowIndex];

                // Calculating the input no index.
                int columnIndex = (_inputNo - 1) % 4 + 1;

                string oldInputValue = string.Empty;
                //checking if subInstruction is Notification or Device or Schedule form Read Property
                if ((subInstruction.Equals("Notification") || subInstruction.Equals("Device") || subInstruction.Equals("Schedule")) && _inputNo == 1)
                {
                    oldInputValue = inputValue;
                    inputValue = "0";
                }
                else
                {
                    oldInputValue = inputValue;
                }

                inputValue = inputValue.Equals("NULL") ? "0" : inputValue;
                // Adding value on that particular input position by input index
                if ((!(functionName.Equals("ISNULL") && columnIndex == 2)) && (!(functionName.Equals("W_P_V"))))
                    dr[$"Input{columnIndex}"] = inputValue;
                //saving the bacnet details for NULL Function BLOCK.
                if (!string.IsNullOrEmpty(functionName) && (functionName.Equals("ISNULL") || functionName.Equals("W_P_V") || functionName.Equals("R_P_V")))
                {
                    if (columnIndex == 1 && functionName.Equals("ISNULL"))
                        AddBacNetObjectDetails(inputValue, "ISNULL", ref dr);
                    else if (functionName.Equals("W_P_V") || functionName.Equals("R_P_V"))
                        if (functionName.Equals("W_P_V") && columnIndex == 2)
                            AddBacNetObjectDetails(inputValue, "W_P_V", ref dr);
                        else if (functionName.Equals("R_P_V") && columnIndex == 1)
                            AddBacNetObjectDetails(inputValue, "R_P_V", ref dr);
                        else
                            dr[$"Input{((columnIndex == 3) ? (columnIndex + 1) : columnIndex)}"] = inputValue;
                    else
                        dr[$"Input4"] = inputValue;

                }
                if (!string.IsNullOrEmpty(functionName) && functionName.Equals("ReadProperty"))
                {
                    AddBacNetObjectDetails(oldInputValue, subInstruction, ref dr);
                }
            }
            _inputNo++;
        }

        private long ConvertFloatValuesToInt(float value)
        {
            float float_variable = value;
            byte[] bytes = BitConverter.GetBytes(float_variable);

            int convertedInt = BitConverter.ToInt32(bytes, 0);
            if ((datatypeCurrentInput == "0004" || datatypeCurrentInput == "000C")
                || (isDataConversionInstruction && (dataTypeExpression == "000E" || dataTypeExpression == "0030")))
            {
                convertedInt = Convert.ToInt32(value);
            }
            if (value < 0)
            {
                long bit_value_32 = 4294967296;
                int absIntValue = Math.Abs(convertedInt); // Take the absolute value

                long Real_result = bit_value_32 - absIntValue;
                return Real_result;
            }
            else
                return convertedInt;
        }

        /// <summary>
        /// Saving the Output Values in CSV File.
        /// </summary>
        /// <param name="outputValue"></param>
        /// <param name="checkforTwoRow"></param>
        /// <param name="_outputNo"></param>
        /// <param name="dataRows"></param>
        private void SaveOutputs(string outputValue, bool isScaleInstruction, ref int _outputNo, ref List<DataRow> dataRows, string instructionName)
        {
            if (isScaleInstruction)
            {
                if (_outputNo == 1)
                {
                    DataRow dr1 = dataRows[0];
                    dr1[$"Output1"] = "-";

                    dr1["Output2"] = outputValue;
                    _outputNo++;
                }
                else
                {
                    DataRow dr2 = dataRows[1];
                    dr2[$"Output{_outputNo - 2}"] = outputValue;
                }
            }
            else
            {
                //Calculating the DataRow Index as per Input count
                int rowIndex = (_outputNo - 1) / 2;

                // Getting DataRow by indexing
                DataRow dr = dataRows[rowIndex];

                // Calculating the Output no index.
                int columnIndex = (_outputNo - 1) % 2 + 1;  // Outputs stored as Output1, Output2, etc.

                //checking if dummy addresses added or not if added then change to "-" for creating mcode properly
                outputValue = outputValue.Equals("A5:999") ? "-" : outputValue;
                // Adding value on that particular input position by input index
                dr[$"Output{columnIndex}"] = outputValue;

                //saving the bacnet details for NULL Function BLOCK.
                if (!string.IsNullOrEmpty(instructionName) && (instructionName.Equals("NULL")))
                {
                    AddBacNetObjectDetails(outputValue, string.Empty, ref dr);
                }
            }
            _outputNo++;
        }

        private void AddBacNetObjectDetails(string outputValue, string subInstruction, ref DataRow dr)
        {
            string instanceNumber = string.Empty;
            string objectType = string.Empty;
            if (!string.IsNullOrEmpty(subInstruction) && (subInstruction.Equals("Notification") || subInstruction.Equals("Schedule") || subInstruction.Equals("Device")))
            {
                switch (subInstruction)
                {
                    case "Notification":
                        if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.Notifications.Any(t => t.ObjectName.Equals(outputValue)))
                        {
                            instanceNumber = xm.LoadedProject.BacNetIP.Notifications.FirstOrDefault(t => t.ObjectName.Equals(outputValue)).InstanceNumber;
                            dr[$"Input{2}"] = instanceNumber;
                            objectType = xm.LoadedProject.BacNetIP.Notifications.FirstOrDefault(t => t.ObjectName.Equals(outputValue)).ObjectType;
                            dr[$"Input{3}"] = objectType.Split(':')[0];
                            if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;
                        }
                        else
                        {
                            dr[$"Input{2}"] = 0;
                            dr[$"Input{3}"] = 0;
                            if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;

                        }
                        break;
                    case "Schedule":
                        if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.Schedules.Any(t => t.ObjectName.Equals(outputValue)))
                        {
                            instanceNumber = xm.LoadedProject.BacNetIP.Schedules.FirstOrDefault(t => t.ObjectName.Equals(outputValue)).InstanceNumber;
                            dr[$"Input{2}"] = instanceNumber;
                            objectType = xm.LoadedProject.BacNetIP.Schedules.FirstOrDefault(t => t.ObjectName.Equals(outputValue)).ObjectType;
                            dr[$"Input{3}"] = objectType.Split(':')[0];
                            string _logicalAddress = xm.LoadedProject.BacNetIP.Schedules.FirstOrDefault(t => t.ObjectName.Equals(outputValue)).LogicalAddress;
                            dr[$"Input{1}"] = _logicalAddress;
                        }
                        else
                        {
                            dr[$"Input{2}"] = 0;
                            dr[$"Input{3}"] = 0;
                            if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;

                        }
                        break;
                    case "Device":
                        if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.Device.ObjectName.Equals(outputValue))
                        {
                            instanceNumber = xm.LoadedProject.BacNetIP.Device.InstanceNumber;
                            dr[$"Input{2}"] = instanceNumber;
                            objectType = xm.LoadedProject.BacNetIP.Device.ObjectType;
                            dr[$"Input{3}"] = objectType.Split(':')[0];
                            if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;
                        }
                        else
                        {
                            dr[$"Input{2}"] = 0;
                            dr[$"Input{3}"] = 0;
                            if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;
                        }
                        break;
                }
                if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;
                return;
            }
            var xmioTag = xm.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(outputValue));
            if (xmioTag == null)
            {
                dr[$"Input{2}"] = 0;
                dr[$"Input{3}"] = 0;
                if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;
                if (subInstruction == "W_P_V" || subInstruction == "R_P_V")
                {
                    UpdateWPVRPVValues(outputValue, subInstruction, ref dr);
                }
            }
            if (xmioTag != null)
            {
                if (subInstruction == "W_P_V" || subInstruction == "R_P_V")
                {
                    UpdateWPVRPVValues(outputValue, subInstruction,ref dr);
                }
                else
                {
                    if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.AnalogIOValues.Any(t => t.LogicalAddress.Equals(xmioTag.LogicalAddress)))
                    {
                        instanceNumber = xm.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(xmioTag.LogicalAddress)).InstanceNumber;
                        dr[$"Input{2}"] = instanceNumber;
                        objectType = xm.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(xmioTag.LogicalAddress)).ObjectType;
                        dr[$"Input{3}"] = objectType.Split(':')[0];
                        if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;
                    }
                    else if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.BinaryIOValues.Any(t => t.LogicalAddress.Equals(xmioTag.LogicalAddress)))
                    {
                        instanceNumber = xm.LoadedProject.BacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(xmioTag.LogicalAddress)).InstanceNumber;
                        dr[$"Input{2}"] = instanceNumber;
                        objectType = xm.LoadedProject.BacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(xmioTag.LogicalAddress)).ObjectType;
                        dr[$"Input{3}"] = objectType.Split(':')[0];
                        if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;
                    }
                    else if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.MultistateValues.Any(t => t.LogicalAddress.Equals(xmioTag.LogicalAddress)))
                    {
                        instanceNumber = xm.LoadedProject.BacNetIP.MultistateValues.FirstOrDefault(t => t.LogicalAddress.Equals(xmioTag.LogicalAddress)).InstanceNumber;
                        dr[$"Input{2}"] = instanceNumber;
                        objectType = xm.LoadedProject.BacNetIP.MultistateValues.FirstOrDefault(t => t.LogicalAddress.Equals(xmioTag.LogicalAddress)).ObjectType;
                        dr[$"Input{3}"] = objectType.Split(':')[0];
                        if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;
                    }
                    else
                    {
                        dr[$"Input{2}"] = 0;
                        dr[$"Input{3}"] = 0;
                        if (subInstruction == "ISNULL") dr[$"Input{4}"] = outputValue;

                    }
                }
            }
            //functionName.Equals("ISNULL")
        }

        private void UpdateWPVRPVValues(string outputValue, string subInstruction, ref DataRow dr)
        {
            string instanceNumber = string.Empty;
            string objectType = string.Empty;
            string logicalAddress = dr["Input1"].ToString();
            if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.AnalogIOValues.Any(t => t.LogicalAddress.Equals(logicalAddress)))
            {
                instanceNumber = xm.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(logicalAddress)).InstanceNumber;
                dr[$"Input{1}"] = instanceNumber;
                objectType = xm.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(logicalAddress)).ObjectType;
                dr[$"Input{2}"] = objectType.Split(':')[0];
                if (subInstruction == "W_P_V") dr[$"Input{3}"] = outputValue;
            }
            else if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.BinaryIOValues.Any(t => t.LogicalAddress.Equals(logicalAddress)))
            {
                instanceNumber = xm.LoadedProject.BacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(logicalAddress)).InstanceNumber;
                dr[$"Input{1}"] = instanceNumber;
                objectType = xm.LoadedProject.BacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(logicalAddress)).ObjectType;
                dr[$"Input{2}"] = objectType.Split(':')[0];
                if (subInstruction == "W_P_V") dr[$"Input{3}"] = outputValue;
            }
            else if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.LoadedProject.BacNetIP != null && xm.LoadedProject.BacNetIP.MultistateValues.Any(t => t.LogicalAddress.Equals(logicalAddress)))
            {
                instanceNumber = xm.LoadedProject.BacNetIP.MultistateValues.FirstOrDefault(t => t.LogicalAddress.Equals(logicalAddress)).InstanceNumber;
                dr[$"Input{1}"] = instanceNumber;
                objectType = xm.LoadedProject.BacNetIP.MultistateValues.FirstOrDefault(t => t.LogicalAddress.Equals(logicalAddress)).ObjectType;
                dr[$"Input{2}"] = objectType.Split(':')[0];
                if (subInstruction == "W_P_V") dr[$"Input{3}"] = outputValue;
            }
            else
            {
                dr[$"Input{1}"] = 0;
                dr[$"Input{2}"] = 0;
                if (subInstruction == "W_P_V") dr[$"Input{3}"] = outputValue;

            }
        }

        /// <summary>
        /// Saving the OpCode in CSV File.
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="dr"></param>
        private void SaveOPCode(string opCode, bool isScaleInstruction, ref List<DataRow> dr)
        {
            if (isScaleInstruction)
            {
                dr[0]["Op Code"] = opCode;
                dr[1]["Op Code"] = opCode;
            }
            else
            {
                dr[0]["Op Code"] = opCode;
                for (int i = 1; i < dr.Count; i++)
                {
                    dr[i]["Op Code"] = "0000";
                }
            }

        }
        /// <summary>
        /// Saving outputType Values in CSV File.
        /// </summary>
        /// <param name="outputType"></param>
        /// <param name="dr"></param>
        private void SaveOutputType(string outputType, bool isScaleInstuction, ref List<DataRow> dr)
        {
            if (isScaleInstuction)
            {
                dr[0]["OutputType"] = outputType;
                dr[1]["OutputType"] = outputType;
            }
            else
            {
                dr[0]["OutputType"] = outputType;
                for (int i = 1; i < dr.Count; i++)
                {
                    dr[i]["OutputType"] = "0";
                }
            }
        }
        /// <summary>
        /// Saving the TCName in CSV File.
        /// </summary>
        /// <param name="tcName"></param>
        /// <param name="dr"></param>
        private void SaveTCName(string tcName, string functionName, ref List<DataRow> dr)
        {
            Counter counter = new Counter();
            XMPS.Instance.tcNamesCount.TryGetValue(functionName, out counter);
            if (counter != null)
            {
                if (functionName.Equals("MES_PID"))
                    tcName = "PID" + counter.CurrentPosition;
                else
                    tcName = counter.Instruction + counter.CurrentPosition;
            }
            dr[0]["T/C Name"] = !tcName.Contains("-") ? tcName : "-";
            for (int i = 1; i < dr.Count; i++)
            {
                dr[i]["T/C Name"] = "-";
            }
            if (counter != null)
            {
                XMPS.Instance.tcNamesCount[functionName.Replace("@", " ")] = new Counter()
                {
                    Instruction = counter.Instruction,
                    CurrentPosition = counter.CurrentPosition + 1,
                    Maximum = counter.Maximum
                };
            }
        }
        /// <summary>
        /// Saving Data Type in CSV file.
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="dr"></param>
        private void SaveDataType(string dataType, bool isScaleInstuction, ref List<DataRow> dr)
        {
            if (isScaleInstuction)
            {
                dr[0]["DataType"] = dataType;
                dr[1]["DataType"] = dataType;
            }
            else
            {
                dr[0]["DataType"] = dataType;
                for (int i = 1; i < dr.Count; i++)
                {
                    dr[i]["DataType"] = "0";
                }
            }
        }
        /// <summary>
        /// Fill data into row for MOV operation
        /// </summary>
        /// <param name="dt">Refrence of DataTable</param>
        /// <param name="key">The output value in column, or key from the keyvalues pair</param>
        /// <param name="expression">The input values in the column</param>
        /// <returns></returns>
        private DataRow MOVData(ref DataTable dt, string key, string expression)
        {
            DataRow dr = dt.NewRow();
            dr["Output1"] = key;
            dr[$"Input1"] = expression.Contains('{') ? expression.Substring(0, expression.IndexOf('{')) : expression;

            // Contact and coil will have only two OP Codes
            dr["Op Code"] = expression.Contains('{') ? expression.Contains('P') ? "0170" : "0180" : "0090";


            // For bool value
            dr["DataType"] = "0000";

            // For bool value
            dr["OutputType"] = "00";

            // Blank records
            dr["Output2"] = "-";
            dr[$"Input2"] = "-";
            dr[$"Input3"] = "-";
            dr[$"Input4"] = "-";
            dr["Enable"] = "-";
            dr["T/C Name"] = "-";

            // For Window Name
            if (isFirstBlockRow)
            {
                dr["WindowName"] = _curBlockName;
                isFirstBlockRow = false;
            }

            return dr;
        }
        /// <summary>
        /// Fill data into data row
        /// </summary>
        /// <param name="dt">Refrence of DataTable</param>
        /// <param name="key">The output value in column, or key from the keyvalues pair</param>
        /// <param name="keyValue">The input values in the column, or simple equation</param>
        /// <param name="curOperation">The operation in the current simple equation</param>
        /// <returns>DataRow with filled Output1, Input1-4 and Op Code</returns>
        private DataRow AddDataToRow(ref DataTable dt, string key, string keyValue, string curOperation)
        {
            DataRow dr = dt.NewRow();
            int i = 1;
            dr["Output1"] = key;
            dr["Output2"] = "-";
            foreach (string val in keyValue.Split(' '))
            {
                if (val != curOperation)
                {
                    if (val != "")
                    {
                        if (val.Contains("{"))
                        {
                            DataRow drEdg = dt.NewRow();

                            int dictionaryKey = CurD10RefNumber;
                            CurD10RefNumber++;
                            string opadd = $"D10:{dictionaryKey:0000}";

                            drEdg["Input1"] = val.Substring(0, val.IndexOf('{'));
                            drEdg["Input2"] = "-";
                            drEdg["Input3"] = "-";
                            drEdg["Input4"] = "-";
                            drEdg["Op Code"] = val.Contains("{P") ? "0170" : "0180";
                            drEdg["Output1"] = opadd;
                            drEdg["DataType"] = "000";
                            drEdg["T/C Name"] = "-";
                            drEdg["OutputType"] = 0;
                            drEdg["Enable"] = "-";
                            if (isFirstBlockRow)
                            {
                                drEdg["WindowName"] = _curBlockName;
                                isFirstBlockRow = false;
                            }
                            dt.Rows.Add(drEdg);
                            dr[$"Input{i}"] = opadd;
                            if (!keyValue.Contains("AND") && !keyValue.Contains("OR")) return null;
                        }
                        else
                            dr[$"Input{i}"] = val.Contains('{') ? val.Substring(0, val.IndexOf('{')) : val;
                        i++;
                    }
                }
            }
            // Put "-" for all empty inputs
            for (; i <= InputLimit; i++)
                dr[$"Input{i}"] = "-";

            // Contact and coil will have only two OP Codes OR Direct move instruction 
            dr["Op Code"] = keyValue == "" ? "0090" : curOperation == "AND" ? "0000" : "0010";

            dr["Input1"] = keyValue == "" ? "1" : dr["Input1"];


            // For bool value
            dr["DataType"] = "0000";

            // For bool value
            dr["OutputType"] = "00";

            // No enable for d10
            dr["Enable"] = "-";

            // No T/C name for d10
            dr["T/C Name"] = "-";

            // For Window Name
            if (isFirstBlockRow)
            {
                dr["WindowName"] = _curBlockName;
                isFirstBlockRow = false;
            }

            return dr;
        }

        /// <summary>
        /// Fill data of the current rung equation into the DataTable
        /// </summary>
        /// <param name="exp">Current simplified rung equation</param>
        /// <param name="keyValues">Current dictionary of simplified rung equation</param>
        /// <param name="coils">Current list of coils</param>
        /// <param name="dt">Datatable to fill the data into</param>
        private void ConvertToData(string exp, Dictionary<string, string> keyValues, List<string> coils, ref DataTable dt, string enable = "-")
        {
            // Converting all complex expression into table row
            foreach (string key in keyValues.Keys)
            {
                if (key.Contains("FN"))
                {
                    /// Check if the limit for the expression is matched
                    /// if the expression contains more than 1 contact it will convert the expression into D10 address first
                    (bool overLimit, string[] elements) = IsOverLimit(exp.Replace(key, ""), exp.Contains("AND") ? "AND" : "OR", 1);
                    if (overLimit)
                    {
                        (string mainExp, Dictionary<string, string> mainExpDic) = MainExpressionConversion(exp.Replace(key, ""), exp.Contains("AND") ? "AND" : "OR", convertToD10: true);
                        foreach (string _key in mainExpDic.Keys)
                        {
                            DataRow drMain = AddDataToRow(ref dt, _key, mainExpDic[_key], mainExpDic[_key].Contains("AND") ? "AND" : "OR");
                            dt.Rows.Add(drMain);
                        }
                        List<DataRow> drFNList = FunctionBlockConversion(ref dt, keyValues[key], mainExp);
                        foreach (DataRow drFN in drFNList)
                            dt.Rows.Add(drFN);
                    }
                    else
                    {
                        List<DataRow> drFNList = FunctionBlockConversion(ref dt, keyValues[key], elements.Any() ? elements.First() : null);
                        foreach (DataRow drFN in drFNList)
                            dt.Rows.Add(drFN);
                    }

                    continue;
                }

                DataRow dr = AddDataToRow(ref dt, key, keyValues[key], keyValues[key].Contains("AND") ? "AND" : "OR");
                if (dr != null)
                    dt.Rows.Add(dr);
            }

            // Main equation conversion for coils
            if (coils.Any())
            {
                (bool overLimit, string[] elements) = IsOverLimit(exp, exp.Contains("AND") ? "AND" : "OR", -1);
                if (overLimit)
                {
                    (string mainExp, Dictionary<string, string> mainExpDic) = MainExpressionConversion(exp, exp.Contains("AND") ? "AND" : "OR");

                    if (mainExpDic != null)
                    {
                        foreach (string key in mainExpDic.Keys)
                        {
                            DataRow dr = AddDataToRow(ref dt, key, mainExpDic[key], mainExpDic[key].Contains("AND") ? "AND" : "OR");
                            dt.Rows.Add(dr);
                        }
                    }
                    (_, string[] mainElements) = IsOverLimit(mainExp, mainExp.Contains("AND") ? "AND" : "OR");
                    foreach (string coil in coils)
                    {
                        DataRow dr;
                        if (mainElements.Length == 1)
                            if (coil.Contains('@'))
                                dr = SetResetData(ref dt, coil, mainElements[0]);
                            else
                                dr = MOVData(ref dt, coil, mainElements[0]);
                        else
                        if (coil.Contains('@'))
                        {
                            int dictionaryKey = CurD10RefNumber;
                            CurD10RefNumber++;
                            string p = $"D10:{dictionaryKey:0000}";
                            string curoperation = mainExp.Contains("AND") ? "AND" : "OR";
                            mainExpDic.Add(p, string.Join($" {curoperation} ", elements.Take(InputLimit)));
                            dr = AddDataToRow(ref dt, p, mainExp, mainExp.Contains("AND") ? "AND" : "OR");
                            dt.Rows.Add(dr);
                            dr = SetResetData(ref dt, coil, p);
                        }
                        else
                            dr = AddDataToRow(ref dt, coil, mainExp, mainExp.Contains("AND") ? "AND" : "OR");

                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    foreach (string coil in coils)
                    {
                        if (elements.Length == 1)
                        {
                            DataRow drM;
                            if (coil.Contains('@'))
                                drM = SetResetData(ref dt, coil, elements.First());
                            else
                                drM = MOVData(ref dt, coil, elements.First());
                            drM["Enable"] = enable;
                            dt.Rows.Add(drM);
                        }
                        else
                        {
                            DataRow dr;
                            if (coil.Contains('@'))
                            {
                                int dictionaryKey = CurD10RefNumber;
                                CurD10RefNumber++;
                                string p = $"D10:{dictionaryKey:0000}";
                                string curoperation = exp.Contains("AND") ? "AND" : "OR";
                                dr = AddDataToRow(ref dt, p, exp, exp.Contains("AND") ? "AND" : "OR");
                                dr["Enable"] = enable;
                                dt.Rows.Add(dr);
                                dr = SetResetData(ref dt, coil, p);
                            }
                            else
                                dr = AddDataToRow(ref dt, coil, exp, exp.Contains("AND") ? "AND" : "OR");
                            dr["Enable"] = enable;
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Convert the main expression into expression with less than 4 inputs
        /// </summary>
        /// <param name="expression">Expression to convert</param>
        /// <param name="curOperation">Operation in the expression</param>
        /// <returns>(Converted expression in simplest form, dictionary<D10:key, expression>)</returns>
        private (string, Dictionary<string, string>) MainExpressionConversion(string expression, string curOperation, bool convertToD10 = false)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            int dictionaryKey = 1;

            (bool overLimit, string[] elements) = IsOverLimit(expression, curOperation);
            if (overLimit)
            {
                dictionaryKey = CurD10RefNumber;
                CurD10RefNumber++;
                string p = $"D10:{dictionaryKey:0000}";
                dic.Add(p, string.Join($" {curOperation} ", elements.Take(InputLimit)));

                int skipCount = InputLimit;
                for (int _indx = 0; _indx < (elements.Count() / (InputLimit - 1)) - 1; _indx++)
                {
                    dictionaryKey = CurD10RefNumber;
                    CurD10RefNumber++;
                    p = $"D10:{dictionaryKey:0000}";
                    dic.Add(p, $"D10:{dictionaryKey - 1:0000} {curOperation} {string.Join($" {curOperation} ", elements.Skip(skipCount).Take(InputLimit - 1))}");

                    skipCount += InputLimit - 1;
                }

                if (convertToD10)
                {
                    if (elements.Skip(skipCount).Any())
                    {
                        dictionaryKey = CurD10RefNumber;
                        CurD10RefNumber++;
                        p = $"D10:{dictionaryKey:0000}";
                        dic.Add(p, $"D10:{dictionaryKey - 1:0000} {curOperation} {string.Join($" {curOperation} ", elements.Skip(skipCount).Take(InputLimit - 1))}");
                    }
                    return (p, dic);
                }
                else
                    return ($"D10:{dictionaryKey:0000} {curOperation} {string.Join($" {curOperation} ", elements.Skip(skipCount).Take(InputLimit - 1))}", dic);
            }
            else
            {
                if (convertToD10)
                {
                    dictionaryKey = CurD10RefNumber;
                    CurD10RefNumber++;
                    string p = $"D10:{dictionaryKey:0000}";
                    dic.Add(p, string.Join($" {curOperation} ", elements));
                    return (p, dic);
                }
                else
                    return (expression, null);
            }
        }


        public CSVHelper()
        {
            string currentLogicalAdd = string.Empty;
            try
            {
                CurD10RefNumber = 0;
                tagToAddress.Clear();

                var duplicateTags = xm.LoadedProject.Tags.GroupBy(t => t.LogicalAddress).Where(g => g.Count() > 1).SelectMany(g => g).ToList();

                if (duplicateTags.Any())
                {
                    var tag = duplicateTags.First(); // Take first duplicate for error reporting
                    string modelName = tag.Model == "User Defined Tags" ? "User Defined Tags" : tag.Model;
                    var matchingTags = xm.LoadedProject.Tags.Where(t => t.Model == modelName).OrderBy(d => d.Key);
                    int rowNumber = matchingTags.Select((t, index) => new { t, index }).FirstOrDefault(x => x.t == tag)?.index ?? -1;
                    throw new Exception($"{(tag.Model.Equals("User Defined Tags") ? "TagsForm" : "TagsForm@" + tag.Model.Replace(" Tags", ""))} : {tag.LogicalAddress}, Duplicate Tag found at row no-{rowNumber}");
                }
                // Load the tags into dictionary
                foreach (Base.XMIOConfig curTag in xm.LoadedProject.Tags)
                {
                    //added check for the Commented Tag not Added to the Dictionary
                    if (!(curTag.LogicalAddress.StartsWith("'")))
                    {
                        currentLogicalAdd = curTag.LogicalAddress;
                        if (curTag.LogicalAddress.StartsWith("F") || curTag.LogicalAddress.Contains(".") || curTag.Label == "Bool" || curTag.Mode == "Digital")
                        {
                            tagToAddress.Add(curTag.Tag.Trim(), curTag.LogicalAddress);
                        }

                    }

                }
            }
            catch
            {
                XMIOConfig xMIOConfig = xm.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(currentLogicalAdd));
                if (xMIOConfig != null)
                {
                    string modelName = xMIOConfig.Model == "User Defined Tags" ? "User Defined Tags" : xMIOConfig.Model;
                    var matchingTags = xm.LoadedProject.Tags.Where(t => t.Model == modelName).OrderBy(D => D.Key);
                    int rowNumber = matchingTags.Select((t, index) => new { t, index })
                                                .FirstOrDefault(x => x.t == xMIOConfig)?.index ?? -1;
                    throw new Exception($"{(xMIOConfig.Model.Equals("User Defined Tags") ? "TagsForm" : "TagsForm@" + xMIOConfig.Model.Replace(" Tags", ""))} {":" + xMIOConfig.LogicalAddress} Duplicate Tag found. at row no-{rowNumber}");
                }
                else
                    throw new Exception($"Dublicate tag found for {currentLogicalAdd} in current project.");
            }

        }

        /// <summary>
        /// Insert the rung expressions into data table
        /// </summary>
        /// <param name="rungs">List of rung expressions</param>
        /// <param name="curBlockName">Current block name</param>
        /// <param name="AppData">Refrence of data table to insert values into</param>
        /// <returns>(True/False if error present, List of tuple<lineNo, error in line>)</returns>
        public (bool, List<Tuple<int, string>>) GetData(List<string> rungs, string curBlockName, ref DataTable AppData)
        {
            int i = 1;
            bool errorInBlock = false;
            isFirstBlockRow = true;
            errorList.Clear();

            foreach (string rung in rungs)
            {
                //Added Check For Not Generating CSV For Commented Rung 
                if (!rung.StartsWith("'"))
                {
                    (string expression, bool error, List<string> rungErrors) = AddressAssociation(rung);

                    if (xm.InstructionCheckErrors.ContainsKey(curBlockName))
                    {
                        var values = xm.InstructionCheckErrors[curBlockName];
                        bool keyExists = values.Any(kvp => kvp.Key == rungs.IndexOf(rung) + 1);
                        if (keyExists)
                        {
                            var result = values.FirstOrDefault(kvp => kvp.Key == rungs.IndexOf(rung) + 1);
                            rungErrors.Add(result.Value);
                            error = true;
                        }
                    }
                    if (xm.UDFBCompileTimeErrors.ContainsKey(curBlockName))
                    {
                        var values = xm.UDFBCompileTimeErrors[curBlockName];
                        bool keyExists = values.Any(kvp => kvp.Key == rungs.IndexOf(rung) + 1);
                        if (keyExists)
                        {
                            var result = values.FirstOrDefault(kvp => kvp.Key == rungs.IndexOf(rung) + 1);
                            rungErrors.Add(result.Value);
                            error = true;
                        }
                    }
                    if (xm.MQTTCompileTimeErrors.ContainsKey(curBlockName))
                    {
                        var values = xm.MQTTCompileTimeErrors[curBlockName];
                        bool keyExists = values.Any(kvp => kvp.Key == rungs.IndexOf(rung) + 1);
                        if (keyExists)
                        {
                            var result = values.FirstOrDefault(kvp => kvp.Key == rungs.IndexOf(rung) + 1);
                            rungErrors.Add(result.Value);
                            error = true;
                        }
                    }

                    if (!error)
                    {
                        ///Write separate logic for UDFB FB blocks for which 9999 is the opcode check if this opcode exists
                        if (rung.Contains("OPC:9999"))
                        {
                            string enable = "-";
                            string calculatedEnable = "";

                            ///check if the rung is having anything else than the function block and add it in one variable
                            string runglogic = expression.Split('[')[0].Replace("(  ) = (", "").TrimStart();

                            //adding logic for getting used address form main rung logic 
                            string pattern = @"\b\w+:\w+\b";
                            MatchCollection matches = Regex.Matches(runglogic, pattern);
                            // Convert matches to a list
                            List<string> addressFromMainLogic = new List<string>();
                            foreach (Match match in matches)
                            {
                                addressFromMainLogic.Add(match.Value);
                            }
                            ///Get complete function of the UDFB logic
                            List<string> udfb_rungs = AddUDFBDetailstoData(rung);
                            //Getting UDFB Name
                            int startIndex = expression.IndexOf("FN:") + 3;
                            int endIndex = expression.IndexOf(" ", startIndex);
                            string functionName = expression.Substring(startIndex, endIndex - startIndex);
                            string mainLogicEnable = rung.Split('[')[0].Trim().TrimEnd(" AND".ToCharArray()) + " )";
                            ///Iterate the UDFB logic line by line
                            foreach (string orgrung in udfb_rungs)
                            {
                                if (!orgrung.Contains("FN:") && runglogic != "" && mainLogicEnable != "")
                                {
                                    if (runglogic.Substring(0, runglogic.Length - 5).Length > 9)
                                    {
                                        enable = $"D10:{CurD10RefNumber:0000}";
                                        CurD10RefNumber++;
                                        string newexpression = "(" + enable + " ) = (" + runglogic.Substring(0, runglogic.Length - 5) + ")  ;";
                                        (string orgexp, Dictionary<string, string> keyValues) = SimplifyEquation(newexpression.Split('=')[1]);
                                        List<string> coilsList = SimplifyCoil(newexpression.Split('=')[0]);
                                        _curBlockName = $"B{GetBlockIndex(curBlockName):00}";
                                        ConvertToData(orgexp, keyValues, coilsList, ref AppData, "-");
                                    }
                                    else
                                        enable = runglogic.Substring(0, runglogic.Length - 5);
                                }
                                else if (mainLogicEnable.Contains("AND") || mainLogicEnable.Contains("OR"))
                                {
                                    calculatedEnable = $"D10:{CurD10RefNumber:0000}";
                                    CurD10RefNumber++;
                                    string newexpression = "(" + calculatedEnable + " ) = (" + mainLogicEnable.Split('=')[1] + ");";
                                    (string enbexpression, bool endberror, List<string> enbrungErrors) = AddressAssociation(newexpression, functionName, true, addressFromMainLogic);

                                    (string orgexp, Dictionary<string, string> keyValues) = SimplifyEquation(enbexpression.Split('=')[1]);
                                    List<string> coilsList = SimplifyCoil(newexpression.Split('=')[0]);
                                    _curBlockName = $"B{GetBlockIndex(curBlockName):00}";
                                    ConvertToData(orgexp, keyValues, coilsList, ref AppData, "-");
                                    mainLogicEnable = "";
                                    calculatedEnable = "() = (" + calculatedEnable + " AND ";
                                }
                                else if (mainLogicEnable.Length > 2)
                                {
                                    calculatedEnable = mainLogicEnable.Split('=')[0] + " = (" + mainLogicEnable.Split('=')[1] + " AND ";
                                    mainLogicEnable = "";
                                }
                                ///Add before FB logic to this rung to get it represented as Enable address with other enable address added in if exists
                                ///string actrung = runglogic != "" ? rung.Split('[')[0] + (orgrung.Split('[')[0].Split('=')[1].Length > 5 ? orgrung.Split('[')[0].Split('=')[1].Trim().Substring(1).Trim() : " ") + " " + (orgrung.Contains('[') ? "[" + orgrung.Split('[')[1] : orgrung) : orgrung;
                                string actrung = "";
                                if (orgrung.Contains('['))
                                    actrung = runglogic != "" ? (calculatedEnable.Length > 2 ? calculatedEnable : " ") + " " + (orgrung.Split('[')[0].Split('=')[1].Length > 5 ? orgrung.Split('[')[0].Split('=')[1].Trim().Substring(1).Trim() : " ") + " " + (orgrung.Contains('[') ? "[" + orgrung.Split('[')[1] : orgrung) : orgrung;
                                else
                                    actrung = orgrung.Insert(orgrung.IndexOf("=") + 3, calculatedEnable.Length > 2 ? calculatedEnable.Substring(calculatedEnable.IndexOf('=') + 3) : runglogic);
                                ///Address association
                                (string orgexpression, bool udfberror, List<string> udfbrungErrors) = AddressAssociation(actrung, functionName, true, addressFromMainLogic);
                                if (!udfberror)
                                {
                                    orgexpression = orgexpression.Replace("*", "");
                                    (string orgexp, Dictionary<string, string> keyValues) = SimplifyEquation(orgexpression.Split('=')[1]);
                                    List<string> coilsList = SimplifyCoil(orgexpression.Split('=')[0]);
                                    _curBlockName = $"B{GetBlockIndex(curBlockName):00}";
                                    ConvertToData(orgexp, keyValues, coilsList, ref AppData, enable);
                                }
                                else
                                {
                                    foreach (string line in udfbrungErrors)
                                    {
                                        _diagnostics = new Tuple<int, string>(i, line);
                                        errorList.Add(_diagnostics);
                                    }
                                    errorInBlock = true;
                                }

                            }
                        }
                        else
                        {
                            (string exp, Dictionary<string, string> keyValues) = SimplifyEquation(expression.Split('=')[1]);
                            List<string> coilsList = SimplifyCoil(expression.Split('=')[0]);
                            if (curBlockName.StartsWith("Interrupt_Logic_Block"))
                            {
                                int logicalBlockNameNo = GetBlockIndex(curBlockName);
                                _curBlockName = $"IHB{curBlockName.Split('k')[1]}";
                            }
                            else
                            {
                                _curBlockName = $"B{GetBlockIndex(curBlockName):00}";
                            }
                            ConvertToData(exp, keyValues, coilsList, ref AppData);
                        }
                    }
                    else
                    {
                        foreach (string line in rungErrors)
                        {
                            _diagnostics = new Tuple<int, string>(i, line);
                            errorList.Add(_diagnostics);
                        }
                        errorInBlock = true;
                    }
                    ////Add name of block in last column which is utilised while calculating M code
                    foreach (DataRow dr in AppData.AsEnumerable())
                        dr[14] = dr[14].ToString().Length > 1 ? dr[14] : curBlockName;
                }
                i++;
            }
            return (errorInBlock, errorList);
        }

        /// <summary>
        /// Convert the rung equation from tag name to its corresponding address and check if the equation is valid or not
        /// </summary>
        /// <example>
        /// ( DigitalOutput_DO0 ) = (DigitalInput_DI0 AND DigitalInput_DI1); -> ( Q0:000.00 ) = (I1:000.00 AND I1:000.01);
        /// </example>
        /// <param name="curRung">Current rung to be parsed</param>
        /// <returns>(new converted equation, True/False for error present, list of errors)</returns>
        private (string, bool, List<string>) AddressAssociation(string curRung, string udfbName = null, bool isUDFB = false, List<string> addressFroMain = null)
        {
            int i = -1;
            int _position = -1;

            string newExpression = "";
            string _curWord = "";
            string _curSetReset = "";
            char _cur;
            // Flags
            bool error = false;
            bool equalPresent = false;
            bool isCoilPresent = false;

            // Counters 
            int roundBrackets = 0;
            int squareBrackets = 0;

            List<string> rungErrors = new List<string>();

            while (true)
            {
                i++;
                _position++;
                string edged = "";
                _cur = curRung[i];

                if (_cur == ';')
                {
                    newExpression += ';';
                    break;
                }
                if (_cur == ' ')
                {
                    newExpression += ' ';
                    continue;
                }

                if (_cur == ',')
                {
                    newExpression += ',';
                    continue;
                }

                if (_cur == '=')
                {
                    newExpression += '=';
                    equalPresent = true;
                    continue;
                }

                if (_cur == '(')
                {
                    newExpression += '(';
                    roundBrackets++;
                    continue;
                }

                if (_cur == ')')
                {
                    newExpression += ')';
                    roundBrackets--;
                    continue;
                }

                if (_cur == '[')
                {
                    while (curRung[_position] != ']')
                        _position++;

                    _curWord = curRung.Substring(i, _position - i + 1);
                    newExpression += _curWord;

                    i = _position;

                    // Check for if function block is populated with data
                    //also check for if the Function Block is contains commeted tag or not
                    List<string> inputAddressList = new List<string>();
                    foreach (string inputs in _curWord.Split(' ').Where(x => x.Contains("IN:") || x.Contains("OP:")))
                    {
                        if (inputs.Contains("IN:"))
                        {
                            inputAddressList.Add(inputs.Replace("IN:", ""));
                        }
                        else
                        {
                            inputAddressList.Add(inputs.Replace("OP:", ""));
                        }
                    }
                    foreach (string add in inputAddressList)
                    {

                        if ((add.StartsWith("F2") || add.StartsWith("W4") || add.Contains(":")) && (!(add.StartsWith("-"))) && !add.Contains("A5:999"))
                        {
                            if (tagToAddress.ContainsValue(add.Replace("*", "")) && udfbName == null)
                            {
                                if (!add.StartsWith("S3") && !add.StartsWith("Q0") && !add.StartsWith("I1"))
                                {
                                    if (xm.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(add)).Model != "User Defined Tags")
                                    {
                                        error = true;
                                        rungErrors.Add($"Error occured while parsing, No Tag found in current logic block for the address {add}");
                                    }
                                }
                            }
                            else if (LogicalAddressContain(add, udfbName))
                            {
                                if (udfbName == null)
                                {
                                    if (!add.StartsWith("S3") && !add.StartsWith("Q0") && !add.StartsWith("I1"))
                                    {
                                        string tag = add;
                                        if (add.StartsWith("~"))
                                        {
                                            tag = tag.Replace("~", "");
                                        }
                                        if (add.Contains("]"))
                                        {
                                            tag = tag.Replace("]", "");
                                        }
                                        if (xm.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(tag.Replace("*", ""))).Model != "User Defined Tags")
                                        {
                                            error = true;
                                            rungErrors.Add($"Error occured while parsing, No Tag found in current logic block for the address {add}");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                error = true;
                                if (udfbName == null)
                                    rungErrors.Add($"Error occured while parsing, No Tag found for the address {add}");
                                else
                                    rungErrors.Add($"UDFB_{udfbName} Error occured while parsing, No Tag found for the address {add}");
                            }
                        }
                        else
                        {
                            //adding extra check if rung with udfb variables paste in normal logic block.
                            string tag = add.Replace("]", "");
                            string pattern = @"^-?\d+(\.\d+)?$";
                            bool isNumeric = Regex.IsMatch(tag.Replace("*", ""), pattern);
                            //checking is input form the Notification or Device or Schedule Object
                            int startIndex = curRung.IndexOf("FN:") + 3;
                            int endIndex = curRung.IndexOf(" ", startIndex);
                            string functionName = curRung.Substring(startIndex, endIndex - startIndex);
                            bool isBacnetObjectText = false;
                            if (functionName.Equals("Notification") || functionName.Equals("Device") || functionName.Equals("Schedule"))
                            {
                                switch (functionName)
                                {
                                    case "Notification":
                                        isBacnetObjectText = XMPS.Instance.LoadedProject.BacNetIP.Notifications.Any(t => t.ObjectName.Equals(tag));
                                        break;
                                    case "Device":
                                        isBacnetObjectText = XMPS.Instance.LoadedProject.BacNetIP.Device.ObjectName.Equals(tag);
                                        break;
                                    case "Schedule":
                                        isBacnetObjectText = XMPS.Instance.LoadedProject.BacNetIP.Schedules.Any(t => t.ObjectName.Equals(tag));
                                        break;
                                }
                            }

                            if (!isNumeric && !string.IsNullOrEmpty(tag) && !add.StartsWith("PUB.")
                                && !add.StartsWith("SUB.") && !add.Equals("NULL") && !add.Contains("A5:999") && !isBacnetObjectText)
                            {
                                if (!xm.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(add.Replace("~", "").Replace("]", "").Replace("*", ""))))
                                {
                                    error = true;
                                    rungErrors.Add($"Error occured while parsing, No Tag found for the address {add}");
                                }

                            }
                        }
                    }
                    //ABOVE LOGIC FOR THE TESTING FOR COMMENTE TAG NOT ALLOW TO FB

                    //adding check for the creating rung with blank function block add compile time error
                    try
                    {
                        string input1 = _curWord.Split(' ').Where(x => x.Contains("IN:")).First();
                        if (input1.Replace("IN:", "") != "")
                        {
                            squareBrackets++;
                        }
                        continue;
                    }
                    catch (Exception ex)
                    {
                        error = true;
                        rungErrors.Add($"Please check Rung {_curWord}");
                    }
                }

                while (curRung[_position + 1] != '(' && curRung[_position + 1] != ')' && curRung[_position + 1] != ' ' && curRung[_position + 1] != ',')
                    _position++;

                _curWord = curRung.Substring(i, _position - i + 1);
                i = _position;

                if (_curWord == "AND")
                {
                    newExpression += "AND";
                    continue;
                }

                if (_curWord == "OR")
                {
                    newExpression += "OR";
                    continue;
                }

                // Check for string and replace it with respective logical address
                {
                    if (_curWord.Contains('~'))
                    {
                        _curWord = _curWord.Remove(0, 1);
                        newExpression += "~";
                    }
                    else if (_curWord.Contains("{S}"))
                    {
                        _curSetReset = _curWord.Substring(_curWord.IndexOf("{", 1));
                        _curWord = _curWord.Replace(_curSetReset, "");
                    }
                    else if (_curWord.Contains("{R}"))
                    {
                        _curSetReset = _curWord.Substring(_curWord.IndexOf("{", 1));
                        _curWord = _curWord.Replace(_curSetReset, "");
                    }
                    else if (_curWord.Contains("{"))
                    {
                        edged = _curWord.Substring(_curWord.IndexOf("{")).ToString();
                        //Remove the logic of negating contact for edge detector as applied other logic for this case
                        _curWord = _curWord.Replace(_curWord.Substring(_curWord.IndexOf("{")), "");
                    }

                    if (tagToAddress.ContainsKey(_curWord.Replace("*", "")))
                    {
                        if (udfbName == null)
                        {
                            if (xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(_curWord.Replace("*", ""))).Model != "User Defined Tags"
                                   && xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(_curWord.Replace("*", ""))).LogicalAddress.StartsWith("F2"))
                            {
                                error = true;
                                rungErrors.Add($"Error occured while parsing, No Tag found in current logic block for the address {_curWord}");
                            }
                        }
                        if (udfbName != null)
                        {
                            if (addressFroMain != null && !addressFroMain.Contains(_curWord.Replace("*", "")) && !addressFroMain.Contains(xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(_curWord.Replace("*", ""))).LogicalAddress))
                            {
                                if (xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(_curWord.Replace("*", ""))).Model != $"{udfbName} Tags"
                                   && xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(_curWord.Replace("*", ""))).LogicalAddress.StartsWith("F2"))
                                {
                                    error = true;
                                    rungErrors.Add($"UDFB_{udfbName} Error occured while parsing, No Tag found for the address {_curWord}");
                                }
                            }
                        }
                        newExpression += tagToAddress[_curWord] + edged;
                        if ((!newExpression.Contains("=")) && _curSetReset != "")
                        {
                            newExpression += "@" + _curSetReset;
                            _curSetReset = "";
                        }
                    }
                    else if (tagToAddress.ContainsValue(_curWord.Replace("~", "").Replace("*", "")))
                    {
                        string tag = _curWord.Replace("~", "").Replace("]", "");
                        if (udfbName == null)
                        {
                            if (tag.Contains(":") && tag.StartsWith("F2"))
                            {
                                if (xm.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(tag.Replace("*", ""))).Model != "User Defined Tags")
                                {
                                    error = true;
                                    rungErrors.Add($"Error occured while parsing, No Tag found in current logic block for the address {tag}");
                                }
                            }
                        }
                        if (udfbName != null)
                        {
                            if (addressFroMain != null && !addressFroMain.Contains(_curWord.Replace("*", "")))
                            {
                                if (tag.Contains(":") && tag.StartsWith("F2") && !tag.EndsWith("*"))
                                {
                                    if (xm.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(tag.Replace("*", ""))).Model != $"{udfbName} Tags")
                                    {
                                        error = true;
                                        rungErrors.Add($"UDFB_{udfbName} Error occured while parsing, No Tag found for the address {tag}");
                                    }
                                }
                            }

                        }

                        newExpression += tagToAddress[XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == _curWord.Replace("~", "").Replace("*", "")).Select(t => t.Tag).FirstOrDefault()] + edged;
                        if ((!newExpression.Contains("=")) && _curSetReset != "")
                        {
                            newExpression += "@" + _curSetReset;
                            _curSetReset = "";
                        }
                    }
                    else if (_curWord.StartsWith("D10"))
                        newExpression += _curWord;
                    else
                    {
                        error = true;
                        if (udfbName == null)
                        {
                            XMIOConfig isCommented = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(_curWord));
                            _curWord = isCommented != null ? ((isCommented.LogicalAddress.StartsWith("'") || isCommented.ShowLogicalAddress == true) ? isCommented.LogicalAddress : _curWord) : _curWord;
                            rungErrors.Add($"Error occured while parsing, No logical address was found for {_curWord}");
                        }
                        else
                            rungErrors.Add($"UDFB_{udfbName} Error occured while parsing, No Tag found for the address {_curWord}");
                    }
                    if (!equalPresent)
                        isCoilPresent = true;
                }
            }

            // Check for output statement in equation
            if (!isCoilPresent && squareBrackets <= 0)
                return ("", true, new List<string> { "Error: No output declaration for rung" });

            // Validate the equation 
            bool isOutputInExpression = true;
            if (isOutputInExpression && roundBrackets != 0)
                return ("", true, new List<string> { "Error: Missing parenthesis in the equation" });

            return (newExpression, error, rungErrors);
        }

    }
}

