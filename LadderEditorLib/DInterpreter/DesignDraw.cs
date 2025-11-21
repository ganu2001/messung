using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LadderEditorLib.DInterpreter
{
    public struct Rung
    {
        public string _coils;
        public string _elements;
    }

    public class DesignDraw
    {

        private static Rung ConvertToObj(string _rung)
        {
            Rung _curRung;

            Tuple<string, string> _curRungExp = new Tuple<string, string>(_rung.Split('=')[0], _rung.Split('=')[1]);
            _curRung._elements = _curRungExp.Item2;
            _curRung._coils = _curRungExp.Item1;

            return _curRung;
        }

        private static void DrawCoils(ref LadderDesign _reDesign, string _coils, bool isCommented)
        {
            List<string> _coilsList = new List<string>();
            string[] result = _coils.Substring(2, _coils.Length - 4).Split(',');

            foreach (string eCoil in result)
            {
                _coilsList.Add(eCoil.Replace(" ", ""));
            }

            if (_coilsList[0].ToString() != "")
            {
                foreach (string _curCoilName in _coilsList)
                {
                    string _setReset = "";
                    LadderElement _newCoil = _reDesign.InsertCoil();

                    if (_curCoilName.Contains("~"))
                    {
                        _newCoil.Negation = _curCoilName.Contains("~");
                        _newCoil.Attributes[0].Value = _newCoil.Negation ? _curCoilName.Replace("~", "") : _curCoilName;
                    }
                    if (_curCoilName.Contains("{"))
                    {
                        _setReset = _curCoilName.Substring(_curCoilName.IndexOf("{"), (_curCoilName.IndexOf("}") + 1) - _curCoilName.IndexOf("{"));
                        _newCoil.Attributes["Caption"] = _curCoilName.Replace(_setReset, "");
                    }
                    else
                    {
                        _newCoil.Attributes["Caption"] = _curCoilName;
                    }

                    if (_setReset.Length > 0) _newCoil.Attributes["SetReset"] = _setReset.Replace("{", "").Replace("}", "").Trim();
                    LadderDrawing.LadderDesign.ClickedElement = _newCoil;
                    //Adding these for Identified Coil is commented or not
                    if (isCommented)
                    {
                        LadderDrawing.Attribute attribute = new LadderDrawing.Attribute();
                        attribute.Name = "isCommented";
                        _newCoil.Attributes.Add(attribute);
                    }

                }
            }
        }


        private static void DrawContacts(ref LadderDesign _reDesign, string _equation)
        {
            // Variables and lists
            int i = -1;
            int _position = -1;
            int _curBrackets = -1;
            int _elementNo = 0;
            bool _isFirstContact = true;
            string _curOperation = "";
            string _curBracketStatus = "";

            // Temp
            string _curWord;
            List<LadderElement> _tempMultiParallel = new List<LadderElement>();
            Stack<LadderElement> _tempSelectedElement = new Stack<LadderElement>();

            // Test for subtracting all elements count on ( 
            List<Tuple<int, int, LadderElement>> _test = new List<Tuple<int, int, LadderElement>>();
            int _totalClosedBrackets = 0;

            // Stack to store which top element is clicked
            Stack<LadderElement> _curClickedElement = new Stack<LadderElement>();

            List<string> _contacts = new List<string>();
            // list will store <bracket number, element number, element>
            List<Tuple<int, int, LadderElement>> _pTupleList = new List<Tuple<int, int, LadderElement>>();


            LadderElement newrung = _reDesign.InsertBlankRung();
            LadderElement lastelement = newrung.Elements[0];
            LadderDrawing.LadderDesign.ClickedElement = lastelement;

            while (true)
            {
                i++;
                _position++;

                if (_equation[i] == ';')
                {
                    break;
                }

                if (_equation[i] == ' ')
                {
                    continue;
                }

                if (_equation[i] == '(')
                {
                    if (_curBrackets == 0)
                    {
                        _test.Clear();
                        _totalClosedBrackets = 0;
                    }

                    if (_curOperation == "OR")
                    {
                        _curBrackets = 0;
                        _tempMultiParallel = _pTupleList.Where(x => x.Item1 == _curBrackets).Select(x => x.Item3).ToList();
                    }

                    _curBrackets++;
                    _curBracketStatus = "(";

                    continue;
                }

                if (_equation[i] == ')')
                {
                    if (!_isFirstContact)
                    {

                        //LadderDrawing.LadderDesign.ClickedElement = 
                    }

                    if (_curOperation == "AND")
                        _tempMultiParallel = _pTupleList.Where(x => x.Item1 == _curBrackets).Select(x => x.Item3).ToList();
                    else if (_curOperation == "OR")
                    {
                        LadderDesign.ClickedElement = _pTupleList.Where(x => x.Item1 == _curBrackets).Select(x => x.Item3).First();
                        lastelement = LadderDesign.ClickedElement;
                    }

                    _pTupleList.RemoveAll(x => x.Item1 == _curBrackets);
                    _curBrackets--;

                    {
                        _totalClosedBrackets++;
                    }
                    {
                        if (_curBrackets == 0)
                        {
                            _test.Clear();
                            _totalClosedBrackets = 0;
                        }

                    }
                    _elementNo--;
                    _curBracketStatus = ")";

                    continue;
                }

                while (_equation[_position + 1] != '(' && _equation[_position + 1] != ')' && _equation[_position + 1] != ' ')
                    _position++;

                _curWord = _equation.Substring(i, _position - i + 1);
                i = _position;

                if (_curWord == "AND")
                {
                    _curOperation = "AND";

                    {
                        if (_test.Any() && _totalClosedBrackets > 0)
                        {
                            lastelement = _test.Where(x => x.Item1 - _totalClosedBrackets == 1).Select(x => x.Item3).First();
                            LadderDrawing.LadderDesign.ClickedElement = lastelement;
                        }
                    }

                    continue;
                }
                else if (_curWord == "OR")
                {
                    _curOperation = "OR";

                    if (_curBracketStatus == "(")
                    {
                        LadderDesign.ClickedElement = _pTupleList.Where(x => x.Item1 == _curBrackets).Select(x => x.Item3).First();
                    }

                    if (_curBracketStatus == ")")
                    {

                    }

                    continue;
                }
                else
                {
                    _elementNo++;
                    if (_isFirstContact)
                    {
                        LadderElement _newContact = _reDesign.InsertContactBefore(ref lastelement);
                        lastelement = _newContact;
                        lastelement.Attributes[0].Value = _curWord;
                        LadderDrawing.LadderDesign.ClickedElement = lastelement;
                        lastelement = _newContact;


                        // Insert into tuple then list
                        Tuple<int, int, LadderElement> _parsingTuple = new Tuple<int, int, LadderElement>(_curBrackets, _elementNo, _newContact);
                        _pTupleList.Add(_parsingTuple);

                        {
                            Tuple<int, int, LadderElement> _testParse = new Tuple<int, int, LadderElement>(_curBrackets, _elementNo, _newContact);
                            _test.Add(_testParse);
                        }

                        // Insert into clickedElement stack
                        _curClickedElement.Push(_newContact);

                        _isFirstContact = false;
                    }
                    else if (_curOperation == "AND")
                    {
                        LadderElement _newContact = _reDesign.InsertContactAfter(ref lastelement, addToSuperSet: false);
                        _newContact.Negation = _curWord.Contains("~");
                        _newContact.Attributes[0].Value = _newContact.Negation ? _curWord.Replace("~", "") : _curWord;

                        // Set current element as clicked element
                        LadderDrawing.LadderDesign.ClickedElement = _newContact;
                        lastelement = _newContact;

                        // Insert into tuple then list
                        Tuple<int, int, LadderElement> _parsingTuple = new Tuple<int, int, LadderElement>(_curBrackets, _elementNo, _newContact);
                        _pTupleList.Add(_parsingTuple);

                        {
                            Tuple<int, int, LadderElement> _testParse = new Tuple<int, int, LadderElement>(_curBrackets, _elementNo, _newContact);
                            _test.Add(_testParse);
                        }
                    }
                    else if (_curOperation == "OR")
                    {
                        List<LadderElement> _test1 = _test.Where(x => x.Item1 - _totalClosedBrackets == 1).Select(x => x.Item3).ToList();

                        List<LadderElement> _lastelement = _tempMultiParallel.Any() ? _tempMultiParallel : _test1;

                        LadderDrawing.LadderDesign.ClickedElement = _lastelement[0];
                        LadderElement element = LadderDesign.ClickedElement;
                        LadderElement _newPContact = _reDesign.InsertContactParallelNew(ref element, ref _lastelement);
                        _newPContact.Negation = _curWord.Contains("~");
                        _newPContact.Attributes[0].Value = _newPContact.Negation ? _curWord.Replace("~", "") : _curWord;

                        // Set current element as clicked element 
                        LadderDrawing.LadderDesign.ClickedElement = _newPContact;
                        lastelement = _newPContact;

                        // Insert into tuple then list
                        Tuple<int, int, LadderElement> _parsingTuple = new Tuple<int, int, LadderElement>(_curBrackets, _elementNo, _newPContact);
                        _pTupleList.Add(_parsingTuple);

                        {
                            Tuple<int, int, LadderElement> _testParse = new Tuple<int, int, LadderElement>(_curBrackets, _elementNo, _newPContact);
                            _test.Add(_testParse);
                        }

                        // clearing temp list 
                        _tempMultiParallel.Clear();
                    }
                }
            }

            LadderDrawing.LadderDesign.ClickedElement = newrung.Elements[0];
        }


        #region PSolution

        // Helper Dictionary
        private static Dictionary<string, List<LadderElement>> _helperDictionary = new Dictionary<string, List<LadderElement>>();

        // Public helper list
        private static List<LadderElement> _helperDictList = new List<LadderElement>();

        // Public Class Flags
        public static bool _isFirstElement = true;

        /// <summary>
        /// Get the list of elements from _helperDictionary for provided key
        /// </summary>
        /// <param name="_key">key to search for in dictionary</param>
        /// <returns></returns>
        private static List<LadderElement> GetSelectedElementFromKey(string _key)
        {
            return _helperDictionary[_key];
        }

        private static List<LadderElement> DrawElements(ref LadderDesign _reDesign, string _key, string _curOperation, LadderElement lastelement, ref Dictionary<string, string> keyValuePairs, List<LadderElement> _passedSelectedList = null)
        {
            string _curExp = keyValuePairs[_key];

            List<LadderElement> _curSelectedElements = new List<LadderElement>();

            List<LadderElement> _curExpElements = new List<LadderElement>();

            List<LadderElement> _andElements = new List<LadderElement>();
            List<LadderElement> _orElements = new List<LadderElement>();

            // Flags 
            bool _containsKey = false;
            bool _isFirstCur = true;

            // Previous record
            string _previousOperation = _curOperation;
            if (_passedSelectedList != null)
                _andElements.AddRange(_passedSelectedList.Distinct());

            // Current record
            List<Tuple<int, LadderElement>> _curRecord = new List<Tuple<int, LadderElement>>();
            int _curIndex = -1;

            LadderDesign.ClickedElement = lastelement;

            List<string> _parts = _curExp.Split(' ').ToList();
            int totalInput = GetTotalInputOrOutput(_parts);
            
            // If the current Expression is for logic block or for contact
            if (_key.Contains("FN_"))
            {
                LadderElement element = LadderDesign.ClickedElement;
                LadderElement _newFB = _reDesign.InsertFBAfter(ref element);

                int _inputNo = 1;
                int _outputNo = 1;
                foreach (string _part in _parts)
                {
                    if (_part.Contains("IN:"))
                    {
                        _newFB.Attributes[$"input{_inputNo}"] = (_part.Replace("IN:", ""));
                        _inputNo++;
                        continue;
                    }

                    if (_part.Contains("OP:"))
                    {
                        _newFB.Attributes[$"output{_outputNo}"] = (_part.Replace("OP:", ""));
                        _outputNo++;
                        continue;
                    }

                    if (_part.Contains("FN:"))
                        _newFB.Attributes["function_name"] = _part.Replace("FN:", "").Replace("@", " ");

                    if (_part.Contains("DT:"))
                        _newFB.Attributes["DataType"] = _part.Replace("DT:", "");

                    if (_part.Contains("DTN:"))
                        _newFB.Attributes["DataType_Nm"] = _part.Replace("DTN:", "");

                    if (_part.Contains("OPT:"))
                        _newFB.Attributes["OutputType"] = _part.Replace("OPT:", "");

                    if (_part.Contains("OPTN:"))
                        _newFB.Attributes["OutPutType_NM"] = _part.Replace("OPTN:", "");

                    if (_part.Contains("OPC:"))
                        _newFB.Attributes["OpCode"] = _part.Replace("OPC:", "");

                    if (_part.Contains("TC:"))
                        _newFB.Attributes["TCName"] = _part.Replace("TC:", "");

                    if (_part.Contains("EN:"))
                        _newFB.Attributes["enable"] = _part.Replace("EN:", "");
                }
                // Adjust height according to number of inputs
                _newFB.Position.Height = 140 - ((4 - totalInput) * 25) + 25;
                return new List<LadderElement> { element };
            }
            else
            {

                foreach (string _part in _parts)
                {
                    string _pnStatus = "";
                    _curIndex++;

                    if (_part == "AND")
                    {
                        _curOperation = "AND";
                        _previousOperation = "";
                        continue;
                    }

                    if (_part == "OR")
                    {
                        _curOperation = "OR";
                        _previousOperation = "";
                        continue;
                    }

                    if (_part == "")
                        continue;

                    if (keyValuePairs.ContainsKey(_part))
                    {

                        if (_parts.Contains("AND"))
                        {
                            _curSelectedElements = DrawElements(ref _reDesign, _part, _curOperation, LadderDesign.ClickedElement, ref keyValuePairs);
                            _andElements.AddRange(_curSelectedElements);
                            if (!_helperDictionary.ContainsKey(_part))
                                _helperDictionary.Add(_part, _curSelectedElements);
                            LadderDesign.ClickedElement = _andElements.Last();
                        }
                        else if (_parts.Contains("OR"))
                        {
                            List<LadderElement> _tempList = new List<LadderElement>();
                            if (!_containsKey)
                                _tempList = DrawElements(ref _reDesign, _part, _curOperation, LadderDesign.ClickedElement, ref keyValuePairs, _orElements.Any() ? new List<LadderElement> { _orElements.First() } : null);
                            else
                                _tempList = DrawElements(ref _reDesign, _part, _curOperation, LadderDesign.ClickedElement, ref keyValuePairs, _andElements);
                            if (!_helperDictionary.ContainsKey(_part))
                                _helperDictionary.Add(_part, _tempList);
                            _orElements.AddRange(_tempList);

                            if (_isFirstCur)
                            {
                                _andElements.AddRange(_tempList);
                                _isFirstCur = false;
                            }
                        }

                        _containsKey = true;
                    }
                    else
                    {
                        if (_isFirstElement)
                        {
                            LadderElement element = LadderDesign.ClickedElement;
                            LadderElement _newContact = _reDesign.InsertContactBefore(ref element);
                            _newContact.Negation = _part.Contains("~");
                            if (_part.Contains("{"))
                            {
                                _pnStatus = _part.Substring(_part.IndexOf("{"));
                                _newContact.Attributes[0].Value = _newContact.Negation ? _part.Replace("~", "").Replace(_pnStatus, "") : _part.Replace(_pnStatus, "");
                            }
                            else
                            {
                                _newContact.Attributes[0].Value = _newContact.Negation ? _part.Replace("~", "") : _part;
                            }

                            if (_pnStatus.Length > 0)
                            {
                                _newContact.Attributes["PNStatus"] = _pnStatus.Replace("{", "").Replace("}", "").Trim();
                            }


                            LadderDrawing.LadderDesign.ClickedElement = _newContact;
                            _curExpElements.Add(_newContact);
                            _curSelectedElements.Add(_newContact);
                            _isFirstElement = false;
                            _isFirstCur = false;

                            if (_curOperation == "")
                            {
                                if (_parts.Contains("AND"))
                                {
                                    _andElements.Add(_newContact);

                                    Tuple<int, LadderElement> _tempTuple = new Tuple<int, LadderElement>(_curIndex, _newContact);
                                    _curRecord.Add(_tempTuple);
                                }
                                else
                                {
                                    _orElements.Add(_newContact);
                                    _andElements.Add(_newContact);
                                }
                            }
                        }
                        else
                        {
                            if (_curOperation == "AND")
                            {
                                LadderElement element = LadderDesign.ClickedElement;
                                LadderElement _newContact = _reDesign.InsertContactAfter(ref element, addToSuperSet: false);
                                _newContact.Negation = _part.Contains("~");

                                if (_part.Contains("{"))
                                {
                                    _pnStatus = _part.Substring(_part.IndexOf("{"));
                                    _newContact.Attributes[0].Value = _newContact.Negation ? _part.Replace("~", "").Replace(_pnStatus, "") : _part.Replace(_pnStatus, "");
                                }
                                else
                                {
                                    _newContact.Attributes[0].Value = _newContact.Negation ? _part.Replace("~", "") : _part;
                                }

                                if (_pnStatus.Length > 0)
                                    _newContact.Attributes["PNStatus"] = _pnStatus.Replace("{", "").Replace("}", "").Trim();


                                LadderDrawing.LadderDesign.ClickedElement = _newContact;
                                _curExpElements.Add(_newContact);

                                // Adding in the lists
                                if (_previousOperation == "AND" && !_parts.Contains("AND"))
                                {
                                    _orElements.Add(_newContact);
                                    _previousOperation = "";

                                    // if previous operation was AND and current does not contain AND
                                    {
                                        _andElements.Add(_newContact);
                                    }

                                }
                                else if (_curOperation == "AND")
                                {
                                    _andElements.Add(_newContact);

                                    // Add into current record 
                                    Tuple<int, LadderElement> _tempTuple = new Tuple<int, LadderElement>(_curIndex, _newContact);
                                    _curRecord.Add(_tempTuple);
                                }

                                _isFirstCur = false;
                            }
                            else if (_curOperation == "OR")
                            {
                                string _pnStatus1 = "";
                                List<LadderElement> _tempSelected = _andElements.Any() ? new List<LadderElement>(_andElements) : new List<LadderElement> { LadderDesign.ClickedElement };
                                LadderDrawing.LadderDesign.ClickedElement = _tempSelected[0];
                                LadderElement element = LadderDesign.ClickedElement;
                                LadderElement _newPContact = _reDesign.InsertContactParallelNew(ref element, ref _tempSelected);
                                _newPContact.Negation = _part.Contains("~");
                                if (_part.Contains("{"))
                                {
                                    _pnStatus1 = _part.Substring(_part.IndexOf("{"));
                                    _newPContact.Attributes[0].Value = _newPContact.Negation ? _part.Replace("~", "").Replace(_pnStatus1, "") : _part.Replace(_pnStatus1, "");
                                }
                                else
                                {
                                    _newPContact.Attributes[0].Value = _newPContact.Negation ? _part.Replace("~", "") : _part;
                                }

                                if (_pnStatus1.Length > 0)
                                    _newPContact.Attributes["PNStatus"] = _pnStatus1.Replace("{", "").Replace("}", "").Trim();


                                if (_previousOperation == "OR" && !_parts.Contains("OR"))
                                {
                                    _andElements.Add(_newPContact);
                                    _previousOperation = "";

                                    // Add into current record 
                                    Tuple<int, LadderElement> _tempTuple = new Tuple<int, LadderElement>(_curIndex, _newPContact);
                                    _curRecord.Add(_tempTuple);

                                    _curExpElements.Add(_newPContact);
                                    LadderDesign.ClickedElement = _newPContact;
                                }
                                else
                                {
                                    _orElements.Add(_newPContact);
                                }

                                _isFirstCur = false;
                            }
                        }
                    }
                }
            }

            // Return logic for current expression

            if (_parts.Contains("OR"))
            {
                if (keyValuePairs.ContainsKey(_parts[0]))
                {
                    List<LadderElement> _tempList = GetSelectedElementFromKey(_parts[0]);
                    LadderDesign.ClickedElement = _tempList.Last();
                    return _tempList;
                }
                LadderDesign.ClickedElement = _orElements.First();
                return new List<LadderElement> { _orElements.First() };
            }
            else if (_parts.Contains("AND"))
            {
                if (_containsKey)
                {
                    List<LadderElement> _tempList = new List<LadderElement>();

                    for (int i = 0; i < _parts.Count; i++)
                    {
                        if (_helperDictionary.ContainsKey(_parts[i]))
                            _tempList.AddRange(GetSelectedElementFromKey(_parts[i]));
                        else if (_curRecord.Where(x => x.Item1 == i).Any())
                            _tempList.Add(_curRecord.Where(x => x.Item1 == i).Select(x => x.Item2).First());
                    }
                    LadderDesign.ClickedElement = _tempList.Last();
                    return _tempList;

                }
                LadderDesign.ClickedElement = _andElements.Last();
                return _andElements;
            }

            return null;
        }

        private static int GetTotalInputOrOutput(List<string> _parts)
        {
            int inCount = _parts.Count(part => part.StartsWith("IN:"));
            int opCount = _parts.Count(part => part.StartsWith("OP:") && !part.Contains("A5:999"));
            int bitouts = _parts.Where(t => t.StartsWith("OP:") && (t.Contains("F2:") || t.Contains("."))).Count();
            opCount = (inCount == opCount && bitouts > 0) ? opCount + (opCount / 5) : opCount;

            opCount = opCount + (bitouts / 2) + ((opCount - bitouts) / 5);
            return inCount > opCount ? inCount : opCount;
        }

        /// <summary>
        /// Draw current rung elements including function block before contact
        /// </summary>
        /// <param name="_reDesign">LadderDesign to append the rung to</param>
        /// <param name="exp">Current SimplifiedExpression for parsing</param>
        /// <param name="keyValuePairs">Current SimplifiedExpression dictionary</param>
        public static void DrawRung(ref LadderDesign _reDesign, string exp, Dictionary<string, string> keyValuePairs, int index, string addComment, bool isCommented)
        {

            string _curOperation = "";

            _isFirstElement = true;

            string _pnStatus = "";

            LadderElement newrung = _reDesign.InsertBlankRung();
            foreach (LadderElement _curElement in newrung.Elements)
            {
                if (_curElement.customDrawing.GetType() == typeof(LadderDrawing.Comment))
                {
                    _curElement.Attributes[0].Value = addComment;
                }
            }

            LadderElement lastelement = newrung.Elements[0];
            LadderDrawing.LadderDesign.ClickedElement = lastelement;

            // Clear values of helper dictionary
            _helperDictionary.Clear();

            List<string> lines = exp.Split(' ').ToList();

            foreach (string _line in lines)
            {
                if (_line == "AND")
                {
                    _curOperation = "AND";
                    continue;
                }

                if (_line == "OR")
                {
                    _curOperation = "OR";
                    continue;
                }

                if (_line == "" || _line == " ")
                    continue;

                if (keyValuePairs.ContainsKey(_line))
                {
                    if (_isFirstElement)
                    {
                        List<LadderElement> _tempList = DrawElements(ref _reDesign, _line, _curOperation, lastelement, ref keyValuePairs);
                        if (!_helperDictionary.ContainsKey(_line))
                            _helperDictionary.Add(_line, _tempList);
                        lastelement = _tempList.Last();
                        LadderDesign.ClickedElement = lastelement;
                        _isFirstElement = false;
                    }
                    else
                    {
                        List<LadderElement> _tempList = DrawElements(ref _reDesign, _line, _curOperation, lastelement, ref keyValuePairs);
                        if (!_helperDictionary.ContainsKey(_line))
                            _helperDictionary.Add(_line, _tempList);
                        lastelement = _tempList.Last();
                        LadderDesign.ClickedElement = lastelement;
                    }
                }
                else
                {
                    if (_isFirstElement)
                    {
                        LadderElement _newContact = _reDesign.InsertContactBefore(ref lastelement);
                        lastelement = _newContact;
                        _newContact.Negation = _line.Contains("~");
                        if (_line.Contains("{"))
                        {
                            _pnStatus = _line.Substring(_line.IndexOf("{"));
                            _newContact.Attributes[0].Value = _newContact.Negation ? _line.Replace("~", "").Replace(_pnStatus, "") : _line.Replace(_pnStatus, "");
                        }
                        else
                        {
                            _newContact.Attributes[0].Value = _newContact.Negation ? _line.Replace("~", "") : _line;
                        }

                        if (_pnStatus.Length > 0)
                            _newContact.Attributes["PNStatus"] = _pnStatus.Replace("{", "").Replace("}", "").Trim();
                        _pnStatus = "";
                        LadderDrawing.LadderDesign.ClickedElement = lastelement;
                        lastelement = _newContact;
                        _isFirstElement = false;
                    }
                    else
                    {
                        if (_curOperation == "AND")
                        {
                            LadderElement _newContact = _reDesign.InsertContactAfter(ref lastelement, addToSuperSet: false);
                            lastelement = _newContact;
                            _newContact.Negation = _line.Contains("~");
                            if (_line.Contains("{"))
                            {
                                _pnStatus = _line.Substring(_line.IndexOf("{"));
                                _newContact.Attributes[0].Value = _newContact.Negation ? _line.Replace("~", "").Replace(_pnStatus, "") : _line.Replace(_pnStatus, "");
                            }
                            else
                            {
                                _newContact.Attributes[0].Value = _newContact.Negation ? _line.Replace("~", "") : _line;
                            }

                            if (_pnStatus.Length > 0)
                                _newContact.Attributes["PNStatus"] = _pnStatus.Replace("{", "").Replace("}", "").Trim();
                            _pnStatus = "";
                            LadderDrawing.LadderDesign.ClickedElement = lastelement;
                            lastelement = _newContact;
                        }
                        else if (_curOperation == "OR")
                        {
                            List<LadderElement> _tempSelected = new List<LadderElement> { LadderDesign.ClickedElement };
                            LadderDrawing.LadderDesign.ClickedElement = _tempSelected[0];
                            LadderElement element = LadderDesign.ClickedElement;
                            LadderElement _newPContact = _reDesign.InsertContactParallelNew(ref element, ref _tempSelected);
                            _newPContact.Negation = _line.Contains("~");
                            if (_line.Contains("{"))
                            {
                                _pnStatus = _line.Substring(_line.IndexOf("{"));
                                _newPContact.Attributes[0].Value = _newPContact.Negation ? _line.Replace("~", "").Replace(_pnStatus, "") : _line.Replace(_pnStatus, "");
                            }
                            else
                            {
                                _newPContact.Attributes[0].Value = _newPContact.Negation ? _line.Replace("~", "") : _line;
                            }

                            if (_pnStatus.Length > 0)
                                _newPContact.Attributes["PNStatus"] = _pnStatus.Replace("{", "").Replace("}", "").Trim();
                            _pnStatus = "";
                            lastelement = _newPContact;
                        }
                    }
                }

            }
            newrung.Position.Index = index;
            LadderDrawing.LadderDesign.ClickedElement = newrung.Elements[0];
            if (isCommented)
            {
                foreach (LadderElement ele in newrung.Elements)
                {
                    LadderDrawing.Attribute attribute = new LadderDrawing.Attribute();
                    attribute.Name = "isCommented";
                    ele.Attributes.Add(attribute);
                }

            }
        }
        #endregion

        /// <summary>
        /// Creates UI for provided list of expressions
        /// </summary>
        /// <param name="_ladderDesign">Refrence to LadderDesing</param>
        /// <param name="_rungs">List of expressions</param>
        /// <param name="index">Count of elements before the paste element position</param>
        /// <returns>LadderDesing with added elements from the rungs</returns>
        public static LadderDesign GetDesign(ref LadderDesign _ladderDesign, List<string> _rungs, List<string> _comment,/* List<string> _contact,*/ int index = 0, string runStatus = "")
        {

            LadderDrawing.LadderDesign _reDesign = runStatus == "" ? _ladderDesign : new LadderDesign();
            int _i = 0;
            foreach (string _rung in _rungs)
            {
                bool rungCommented = false;
                if (_rung.StartsWith("'"))
                {
                    rungCommented = true;
                }
                else
                {
                    rungCommented = false;
                }

                Rung _curRung = ConvertToObj(_rung.Replace("'", ""));

                // Simplify the _elements part of the rung
                (string myExp, Dictionary<string, string> keyValues) = Equation.SimplifyEquation(_curRung._elements);
                string comment = "";
                if (_comment.Count() != 0) comment = _comment[_i];

                // Add new rung and add contacts and function blocks to the rung
                //Adding one variable ==rungCommented for find if Rung is Commented or Not
                DrawRung(ref _reDesign, myExp, keyValues, index, comment, rungCommented);

                //LadderDrawing.LadderDesign.ClickedElement = newrung.Elements[0];
                // Add coil to rung
                //Adding one variable ==rungCommented for find if Rung is Commented or Not
                DrawCoils(ref _reDesign, _curRung._coils, rungCommented);

                //Add Comment to rung
                // Increment index
                index++;
                _i++;
            }


            LadderDrawing.LadderDesign.ClickedElement = null;
            if (runStatus != "")
            {
                if (runStatus == "between")
                {
                    int newInsertedIndex = index - _rungs.Count();
                    int incrementBy = _rungs.Count();
                    for (int i = newInsertedIndex; i < _ladderDesign.Elements.Count(); i++)
                    {
                        _ladderDesign.Elements[i].Position.Index += incrementBy;
                    }
                    _ladderDesign.Elements.InsertRange(newInsertedIndex, _reDesign.Elements);
                }
                else
                {
                    _ladderDesign.Elements.AddRange(_reDesign.Elements);
                }

                foreach (HashSet<LadderElement> key in _reDesign.parallelElementsDictionary.Keys)
                    if (!_ladderDesign.parallelElementsDictionary.ContainsKey(key))
                        _ladderDesign.parallelElementsDictionary.Add(key, _reDesign.parallelElementsDictionary[key]);
            }

            return _ladderDesign;
        }
    }
}
