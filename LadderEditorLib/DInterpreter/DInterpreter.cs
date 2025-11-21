using LadderDrawing;
using System.Collections.Generic;
using System.Linq;
using XMPS2000.Core;

namespace LadderEditorLib.DInterpreter
{
    public struct Objvalue
    {
        public string value;
        public string type;
        public string negation;
        public bool elements;
        public int relateTo;
        public int connectTo;
    }

    public struct FunctionBlockData
    {
        public string function_name;
        public string input1;
        public string input2;
        public string input3;
        public string input4;
        public string input5;
        public string output1;
        public string output2;
        public string output3;
        public string enable;
        public string TCName;
        internal string DataType;
        internal string DataType_Nm;
        internal string OutputType;
        internal string OutPutType_NM;
        internal string OpCode;
        public List<string> InPuts;
        public List<string> OutPuts;

    }

    public class DInterpreter
    {

        private static Objvalue getcur(LadderElement _element)
        {
            Objvalue curElement;
            curElement.type = _element.CustomType;
            curElement.elements = _element.Elements.Any();
            curElement.negation = _element.Negation ? "~" : "";
            if (_element.Attributes.Where(T => T.Name == "LogicalAddress").Any())
            {
                string logicalAddress = _element.Attributes["LogicalAddress"].ToString();
                string tagname = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == logicalAddress).Select(T => T.Tag).FirstOrDefault();
                string currrentTagName = _element.Attributes["caption"].ToString();
                if (currrentTagName != tagname && tagname != null)
                {
                    _element.Attributes["caption"] = tagname;
                }
            }
            curElement.value = _element.Attributes.Any() ? (string)_element.Attributes.ElementAtOrDefault(0).Value : null;
            if (_element.Negation) { curElement.value = "~" + curElement.value; };
            curElement.relateTo = _element.Position.RelateTo.Count();
            curElement.connectTo = _element.Position.ConnectTo.Count();
            return curElement;
        }

        private static FunctionBlockData getFunctionBlock(LadderElement _functionBlock)
        {
            ///Change add list of inputs and outputs here in the bock data
            FunctionBlockData _curFunctionBlock = new FunctionBlockData();
            List<string>  inputs = _functionBlock.Attributes.Where(t => t.Name.StartsWith("in") && !t.Value.ToString().EndsWith("-")).Select(t => t.Value.ToString()).ToList();
            List<string> outputs = _functionBlock.Attributes.Where(t => t.Name.StartsWith("out") && !t.Value.ToString().Contains('-')).Select(t => t.Value.ToString()).ToList();

            _curFunctionBlock.function_name = _functionBlock.Attributes["function_name"].ToString().Replace(' ', '@');
            _curFunctionBlock.InPuts = inputs;
            _curFunctionBlock.OutPuts = outputs;
            _curFunctionBlock.input1 = _functionBlock.Attributes["input1"].ToString();
            _curFunctionBlock.input2 = _functionBlock.Attributes["input2"].ToString();
            _curFunctionBlock.input3 = _functionBlock.Attributes["input3"].ToString();
            _curFunctionBlock.input4 = _functionBlock.Attributes["input4"].ToString();
            _curFunctionBlock.input5 = _functionBlock.Attributes["input5"].ToString();
            _curFunctionBlock.DataType = _functionBlock.Attributes["DataType"].ToString();
            _curFunctionBlock.DataType_Nm = _functionBlock.Attributes["DataType_Nm"].ToString();
            _curFunctionBlock.OutputType = _functionBlock.Attributes["OutputType"].ToString();
            _curFunctionBlock.OutPutType_NM = _functionBlock.Attributes["OutPutType_NM"].ToString();
            _curFunctionBlock.OpCode = _functionBlock.Attributes["OpCode"].ToString();
            _curFunctionBlock.output1 = _functionBlock.Attributes["output1"].ToString();
            _curFunctionBlock.output2 = _functionBlock.Attributes["output2"].ToString();
            _curFunctionBlock.output3 = _functionBlock.Attributes["output3"].ToString();
            _curFunctionBlock.enable = _functionBlock.Attributes["enable"].ToString();
            _curFunctionBlock.TCName = _functionBlock.Attributes["TCName"].ToString();

            return _curFunctionBlock;
        }

        #region Expression conversion with dictionary

        static List<string> _coilList = new List<string>();
        static List<string> _functionBlockList = new List<string>();
        static List<string> _elementList = new List<string>();
        static List<string> _commentList = new List<string>();
        private static void AddCoilToList(LadderElement _curElement)
        {
            Objvalue _curCoil = getcur(_curElement);
            if (_curElement.Attributes["SetReset"].ToString() != "")
                _coilList.Add(_curCoil.value+"{"+_curElement.Attributes["SetReset"].ToString()+"}".Replace(" ", ""));
            else
                _coilList.Add(_curCoil.value.Replace(" ", ""));

            if (_curElement.Elements.Any())
            {
                AddCoilToList(_curElement.Elements[0]);
            }
        }

        private static void AddFunctionBlockToList(LadderElement element)
        {
            FunctionBlockData _curBlock = getFunctionBlock(element);

            _functionBlockList.Add($"[{_curBlock.function_name}]");
        }

        private static string GetCurParallelExpression(LadderElement _element)
        {
            string branchedString = "";


            List<string> _branchedContacts = new List<string>();

            Objvalue _curElement = getcur(_element);

            if (_element.Position.ConnectTo.Any())
            {
                string pn = _element.Attributes["PNStatus"].ToString();
                _branchedContacts.Add(pn.Length > 0 ? _curElement.value + "{" + pn + "}" : _curElement.value);

                foreach (var _sibling in _element.Position.ConnectTo)
                {
                    string _pppnStatus = "";

                    _pppnStatus = _sibling.Attributes["PNStatus"].ToString();
                    //adding logic for updating TagName in Parallel Elements.
                    if (_sibling.Attributes.Where(T => T.Name == "LogicalAddress").Any())
                    {
                        string logicalAddress = _sibling.Attributes["LogicalAddress"].ToString();
                        string tagname = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == logicalAddress).Select(T => T.Tag).FirstOrDefault();
                        string currrentTagName = _sibling.Attributes["caption"].ToString();
                        if (currrentTagName != tagname && tagname != null)
                        {
                            _sibling.Attributes["caption"] = tagname;
                        }
                    }
                    if (_sibling.Negation)
                        _branchedContacts.Add("~" + _sibling.Attributes[0].Value.ToString());
                    else if (_pppnStatus.Length > 0)
                    {
                        _branchedContacts.Add(_sibling.Attributes[0].Value.ToString() + "{" + _pppnStatus + "}");
                    }
                    else
                    {
                        _branchedContacts.Add(_sibling.Attributes[0].Value.ToString());
                    }

                }
                return $"( {string.Join(" AND ", _branchedContacts)} )";
            }
            
            string pnm = _element.Attributes["PNStatus"].ToString();
            string mainstring = pnm.Length > 0 ? _curElement.value + "{" + pnm + "}" : _curElement.value;
            branchedString += $"{mainstring}";
            return branchedString;
        }

        private static string GetElementExpression(List<LadderElement> _elements)
        {
            bool _isFirstBranch = true;
            string _orExpression = "";
            List<string> _branchList = new List<string>();

            foreach (LadderElement _branchedElement in _elements)
            {
                string _curPExpression = GetCurParallelExpression(_branchedElement);
                if (_isFirstBranch)
                {
                    _orExpression = $"{_curPExpression}";
                    _isFirstBranch = false;
                }
                else
                {
                    _orExpression = $"{_orExpression} OR {_curPExpression}";
                }

            }
            return _orExpression;
        }

        public static (string, string) RungToExpression(LadderElement _rung, ref Dictionary<HashSet<LadderElement>, LadderElements> _parallelDict)
        {
            List<LadderElement> _rootParsedElements = new List<LadderElement>();
            List<string> _rungExpression = new List<string>();

            string _curComment = "";
            _coilList.Clear();
            _elementList.Clear();
            _functionBlockList.Clear();

            foreach (LadderElement _element  in _rung.Elements)
            {
                Objvalue _elementInfo = getcur(_element);
                if (_elementInfo.type == "LadderDrawing.Comment")
                {
                    _curComment = _elementInfo.value;
                }
                if (_elementInfo.type == "LadderDrawing.Coil")
                {
                    AddCoilToList(_element);
                    break;
                }

                else if (_elementInfo.type == "LadderDrawing.Contact")
                {
                    if (_rootParsedElements.Any())
                    {
                        List<int> _addBracketTo = new List<int>();
                        string _tempParallelStr = "";
                        string pnstatus = "";
                        List<LadderElement> _ladderElements = new List<LadderElement>();
                        _ladderElements.Add(_element);
                        _tempParallelStr = $"{_tempParallelStr} {GetElementExpression(_ladderElements)}";
                        foreach (LadderElement pnelement in _ladderElements)
                        {
                            string s = pnelement.Attributes["caption"].ToString();
                            if (s.Equals(_tempParallelStr.Replace(" ", "")))
                            {
                                pnstatus = pnelement.Attributes["PNStatus"].ToString();
                                if (pnstatus.Length > 0)
                                {
                                    _tempParallelStr += "{" + pnstatus + "}";
                                }
                            }
                        }
                        _rootParsedElements.Add(_element);
                        for (int i = _rootParsedElements.Count - 1; i >= 0; i--)
                        {
                            List<LadderElement> _curElements = new List<LadderElement>();
                            _curElements.AddRange(_rootParsedElements.GetRange(i, _rootParsedElements.Count - i));
                            _parallelDict.TryGetValue(new HashSet<LadderElement>(_curElements), out LadderElements parallelElements);
                            if (parallelElements != null && parallelElements.Count() != 0)
                            {
                                if (_curElements.Count() <= 1)
                                {
                                    string ppnstatus = "";
                                    foreach (LadderElement ele in parallelElements)
                                    {
                                        ppnstatus = ele.Attributes["PNStatus"].ToString();

                                    }
                                    if (ppnstatus.Length > 0)
                                    {
                                        _tempParallelStr = $"{_tempParallelStr} OR {GetElementExpression(parallelElements) /*+ "{" + ppnstatus + "}"*/} )";
                                    }
                                    else
                                    {
                                        _tempParallelStr = $"{_tempParallelStr} OR {GetElementExpression(parallelElements)} )";
                                    }


                                }
                                else
                                {
                                    string ppnstatus = "";
                                    foreach (LadderElement ele in parallelElements)
                                    {
                                        ppnstatus = ele.Attributes["PNStatus"].ToString();
                                    }
                                    if (ppnstatus.Length > 0)
                                    {
                                        _tempParallelStr = $"{_tempParallelStr} ) OR {GetElementExpression(parallelElements)} )";
                                    }
                                    else
                                    {
                                        _tempParallelStr = $"{_tempParallelStr} ) OR {GetElementExpression(parallelElements)} ) ";
                                    }

                                    _addBracketTo.Add(i);
                                }

                                _addBracketTo.Add(i);
                            }

                        }
                        _tempParallelStr = $"{_tempParallelStr}";
                        _elementList.Add(_tempParallelStr);

                        foreach (int _index in _addBracketTo)
                            _elementList[_index] = $"({_elementList[_index]}";
                    }
                    else
                    {

                        string _tempStr = "";
                        string ppnstatus = "";
                        List<LadderElement> _ladderElement = new List<LadderElement>();
                        _ladderElement.Add(_element);
                        string pnstatus = _element.Attributes["PNStatus"].ToString();
                        if (pnstatus.Length > 0)
                            _tempStr = GetElementExpression(_ladderElement) ;
                        else
                            _tempStr = GetElementExpression(_ladderElement);
                        _rootParsedElements.Add(_element);

                        List<LadderElement> _curElements = new List<LadderElement>();
                        _curElements.Add(_element);
                        _parallelDict.TryGetValue(new HashSet<LadderElement>(_curElements), out LadderElements parallelElements);
                        if (parallelElements != null && parallelElements.Count() != 0)
                        {
                            foreach (LadderElement ele in parallelElements)
                            {
                                ppnstatus = ele.Attributes["PNStatus"].ToString();
                            }
                            _tempStr = $" ({_tempStr} OR {GetElementExpression(parallelElements)} )";
                        }
                        _elementList.Add(_tempStr);
                    }
                }

                else if (_elementInfo.type == "LadderDrawing.FunctionBlock")
                {
                    string inputs = "",outputs = "";
                    FunctionBlockData _curBlock = getFunctionBlock(_element);
                    foreach(string input in _curBlock.InPuts)
                    {
                        inputs+= " IN:" + input +"";
                    }
                    foreach (string output in _curBlock.OutPuts)
                    {
                        outputs += " OP:" + output + "";
                    }
                    _elementList.Add($"[FN:{_curBlock.function_name} TC:{_curBlock.TCName} EN:{_curBlock.enable} {inputs} DT:{_curBlock.DataType} DTN:{_curBlock.DataType_Nm} OPC:{_curBlock.OpCode} OPT:{_curBlock.OutputType} OPTN:{_curBlock.OutPutType_NM} {outputs}]");
                    break;
                }
            }

            ///
            ///For Finding the Commented Rung
            foreach (LadderDrawing.Attribute attribute in _rung.Attributes)
            {
                if (attribute.Name == "isCommented")
                {
                    return ("'" + $" ( {string.Join(", ", _coilList)} ) = ({string.Join(" AND ", _elementList)});", _curComment);
                }
            }

            ////
            ///For the second time when save the project
            LadderElement ladderElement = _rung.Elements.First();
            foreach(LadderDrawing.Attribute attribute in ladderElement.Attributes)
            {
                if (attribute.Name == "isCommented")
                {
                    return ("'" + $" ( {string.Join(", ", _coilList)} ) = ({string.Join(" AND ", _elementList)});", _curComment);
                }
            }
           
            return ($" ( {string.Join(", ", _coilList)} ) = ({string.Join(" AND ", _elementList)});", _curComment);

        }

        public static (bool, List<string>, List<string>, List<string>) DesingToExpression(LadderDesign data)
        {

            int _rungNo = 0;

            bool errorPresent = false;
            List<string> errorRungs = new List<string>();
            List<string> _expressions = new List<string>();
            Dictionary<HashSet<LadderElement>, LadderElements> _parallelDict = data.GetDict();
            _commentList.Clear();

            foreach (LadderElement _rung in data.Elements)
            {
                _rungNo++;
                (string _curExpression, string comment) = RungToExpression(_rung, ref _parallelDict);
                bool isRungValid = Equation.Validate(_curExpression);
                //if (!isRungValid)
                //{
                //    errorPresent = true;
                //    errorRungs.Add($"Rung {_rungNo}: no contact declare for the Rung");
                //}
                _expressions.Add(_curExpression);
                _commentList.Add(comment);
            }
            
            if(errorRungs.Count > 0)
            {
                errorPresent = true;
            }
            return (errorPresent, errorRungs, _expressions, _commentList);
        }
        #endregion
    }
}
