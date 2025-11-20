using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.LadderLogic;

namespace XMPS2000
{
    class AddressDataTypesValue
    {
        public static Dictionary<AddressDataTypes, int> AddressValues = new Dictionary<AddressDataTypes, int>() {
            { AddressDataTypes.BYTE, 1},
            { AddressDataTypes.BOOL, 1},
            { AddressDataTypes.WORD, 2},
            { AddressDataTypes.INT, 2},
            { AddressDataTypes.DWORD, 4},
            { AddressDataTypes.REAL, 4},
            { AddressDataTypes.STRING, 11},
            { AddressDataTypes.DINT, 4},
            { AddressDataTypes.UDINT, 4}
        };
    }

    class OnlineMonitoringHelper
    {
        XMPS xm = XMPS.Instance;


        /// <summary>
        /// Will contain
        /// "DigitalInputDI0" = (I1:000,type<BIT>)
        /// Only for current block
        /// </summary>
        Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddress = new Dictionary<string, Tuple<string, AddressDataTypes>>();

        /// <summary>
        /// Will contain
        /// "DigitalInputDI0" = (I1:000,type<BIT>)
        /// Tags from the Whole project
        /// </summary>
        private Dictionary<string, Tuple<string, string>> tagToAddress = new Dictionary<string, Tuple<string, string>>();

        /// <summary>
        /// Will contain
        /// rungNumber = (I1:000, I1:001, ..)
        /// </summary>
        public static Dictionary<int, List<string>> VisibleRungsDic = new Dictionary<int, List<string>>();

        public static Dictionary<string, string> AddressValues = new Dictionary<string, string>();

        public LadderCanvas CurCanvas = new LadderCanvas();

        private static OnlineMonitoringHelper instance = null;
        public static OnlineMonitoringHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new OnlineMonitoringHelper();

                return instance;
            }
        }
        public static bool HoldOnlineMonitor = false;
        OnlineMonitoring onlineMonitor = OnlineMonitoring.GetInstance();

        /// <summary>
        /// Populate the tagToAddress dictionary with tags from current project
        /// </summary>
        public void PopulateTagToAddress()
        {
            // Load the tags into dictionary
            foreach (Core.Base.XMIOConfig curTag in xm.LoadedProject.Tags)
            {
                //tagToAddress.Add(curTag.Tag, Tuple.Create(curTag.LogicalAddress, curTag.Type.ToString()));
                tagToAddress[curTag.Tag.Trim()] = Tuple.Create(curTag.LogicalAddress, curTag.Type.ToString());
            }
        }

        /// <summary>
        /// Add the address to VisibleRungDic dictionary, add to list if already value present
        /// </summary>
        /// <param name="rungNo">Rung number</param>
        /// <param name="_curWord">Address to put in the dictionary</param>
        private void AddToVisibleRungs(int rungNo, string _curWord)
        {
            if (!VisibleRungsDic.ContainsKey(rungNo))
            {
                VisibleRungsDic.Add(rungNo, new List<string> { _curWord });
            }
            else
            {
                VisibleRungsDic[rungNo].Add(_curWord);
            }
        }

        private void AddToAddressValues(string _curWord)
        {
            AddressValues[_curWord] = "";
        }

        /// <summary>
        /// Get address type of the provided address
        /// </summary>
        /// <param name="address">Address to get data type of</param>
        /// <returns>Return AddressDataType type for provided address</returns>
        private AddressDataTypes GetAddressType(string address)
        {
            if (address == "Bool" || address == "Bit")
                return AddressDataTypes.BOOL;

            if (address == "Byte")
                return AddressDataTypes.BYTE;

            if (address == "Word")
                return AddressDataTypes.WORD;

            if (address == "Int")
                return AddressDataTypes.INT;

            if (address == "Real")
                return AddressDataTypes.REAL;

            if (address == "Double" || address == "Double Word")
                return AddressDataTypes.DWORD;

            if (address == "DINT")
                return AddressDataTypes.DINT;

            if (address == "UDINT")
                return AddressDataTypes.UDINT;
            if (address == "String")
                return AddressDataTypes.STRING;
            return AddressDataTypes.INT;
        }

        public AddressDataTypes GetAddressTypeOf(string address)
        {
            return GetAddressType(address);
        }

        /// <summary>
        /// Populate CurBlockData dictionary with address from whole active form
        /// </summary>
        public void PopulateCurBlockData()
        {
            if (!xm.CurrentScreen.Contains("LadderForm#"))
                return;

            int _blockIndex = xm.LoadedProject.Blocks.FindIndex(d => d.Name == $"{xm.CurrentScreen.Split('#')[1]}");

            List<string> curBlockRungs = xm.LoadedProject.Blocks[_blockIndex].Elements;

            CurBlockAddress.Clear();
            VisibleRungsDic.Clear();
            AddressValues.Clear();

            int rungNo = 1;

            foreach (string curRung in curBlockRungs)
            {
                ProcessRung(curRung, ref rungNo);
            }
        }
        private void ProcessRung(string curRung, ref int rungNo)
        {
            int i = -1;
            int _position = -1;

            string _curWord = "";
            char _cur;

            List<string> rungErrors = new List<string>();

            while (true)
            {
                i++;
                _position++;

                _cur = curRung[i];

                if (_cur == ';')
                    break;

                if (_cur == ' ' || _cur == ',' || _cur == '=' || _cur == '(' || _cur == ')')
                    continue;

                if (_cur == '[')
                {
                    _curWord = ExtractCurWord(curRung, ref i, ref _position);
                    ProcessCurWord(_curWord, curRung, ref rungNo);
                }
                else
                {
                    _curWord = ExtractCurWordOutsideBrackets(curRung, ref i, ref _position);
                    ProcessSimpleWord(_curWord, rungNo);
                }
            }

            rungNo++;
        }
        /// <summary>
        /// Extracting the word from currentRung.
        /// </summary>
        private string ExtractCurWord(string curRung, ref int i, ref int _position)
        {
            while (curRung[_position] != ']')
                _position++;

            string _curWord = curRung.Substring(i, _position - i + 1);
            i = _position;
            return _curWord;
        }
        private string ExtractCurWordOutsideBrackets(string curRung, ref int i, ref int _position)
        {
            while (curRung[_position + 1] != '(' && curRung[_position + 1] != ')' && curRung[_position + 1] != ' ' && curRung[_position + 1] != ',')
                _position++;

            string _curWord = curRung.Substring(i, _position - i + 1);
            i = _position;
            return _curWord;
        }
        /// <summary>
        /// proceesing over the Inputs Outputs.
        /// </summary>
        private void ProcessCurWord(string _curWord, string curRung, ref int rungNo)
        {
            string fbDatatype = _curWord.Split(' ').Where(x => x.Contains("DTN:")).First().ToString().Replace("DTN:", "");
            AddressDataTypes optype = xm.LoadedProject.CPUDatatype.Equals("Real") ? AddressDataTypes.REAL : AddressDataTypes.WORD;
            AddressDataTypes curADT = GetAddressType(fbDatatype);
            if ((! DataType.ContainsText(fbDatatype)))
                curADT = optype;
            foreach (string _word in _curWord.Split(' '))
            {
                if (_word.Contains("EN:"))
                {
                    ProcessENWord(_word, curADT, rungNo);
                }
                else if (_word.Contains("IN:"))
                {
                    ProcessINWord(_word, curRung, optype, rungNo);
                }
                else if (_word.Contains("OP:"))
                {
                    ProcessOPWord(_word, curRung, curADT, optype, rungNo);
                }
                else if (_word.Contains("DTN:"))
                {
                    optype = _word.Replace("DTN:", "").Replace("]", "") == "Int" ? AddressDataTypes.INT : xm.LoadedProject.CPUDatatype.Equals("Real") ? AddressDataTypes.REAL : AddressDataTypes.WORD;
                }
            }
        }
        /// <summary>
        /// Processing the Enable input value.
        /// </summary>
        private void ProcessENWord(string _word, AddressDataTypes curADT, int rungNo)
        {
            string _temp = _word.Replace("EN:", "");
            if (_temp == "Enabled" || _temp == "-" || _temp == "") return;

            CurBlockAddress[_temp] = Tuple.Create(tagToAddress[xm.LoadedProject.Tags.Where(L => L.LogicalAddress == _temp).Select(L => L.Tag).First().ToString()].Item1, curADT);
            AddToVisibleRungs(rungNo, _temp);
            AddToAddressValues(_temp);
        }
        /// <summary>
        /// processing the input parameter
        /// </summary>
        private void ProcessINWord(string _word, string curRung, AddressDataTypes curADT, int rungNo)
        {
            string opcodeofFB = curRung.Substring(curRung.IndexOf("OPC") + 4, 4);
            string _temp = _word.Replace("IN:", "").Replace("~", "");
            if (_temp == "-" || !_temp.Contains(":")) return;

            if (xm.LoadedProject.Tags.Where(L => L.LogicalAddress == _temp).Count() > 0)
            {
                curADT = _temp.Contains('.') ? AddressDataTypes.BOOL : curADT;
                curADT = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == _temp).Select(t => t.IoList).FirstOrDefault().ToString() == "NIL"
                    ? (AddressDataTypes)Enum.Parse(typeof(AddressDataTypes), xm.LoadedProject.Tags.Where(t => t.LogicalAddress == _temp).Select(t => t.Label).FirstOrDefault().ToString().ToUpper().Replace("DOUBLE WORD", "DWORD"))
                    : curADT;

                CurBlockAddress[_temp] = Tuple.Create(tagToAddress[xm.LoadedProject.Tags.Where(L => L.LogicalAddress == _temp).Select(L => L.Tag).First().ToString()].Item1, curADT);

                if (opcodeofFB == "0390")
                    AddPackUnpackBlockInputs(_temp, curADT, rungNo);
            }
            else
            {
                CurBlockAddress[_temp] = Tuple.Create(_temp, xm.LoadedProject.CPUDatatype.Equals("Real") ? AddressDataTypes.REAL : AddressDataTypes.WORD);
            }

            AddToVisibleRungs(rungNo, _temp);
            AddToAddressValues(_temp);

            if (opcodeofFB == "0390")
                AddPackUnpackBlockInputs(_temp, curADT, rungNo);
        }

        /// <summary>
        /// processing the output address
        /// </summary>
        private void ProcessOPWord(string _word, string curRung, AddressDataTypes curADT, AddressDataTypes optype, int rungNo)
        {
            string opcodeofFB = curRung.Substring(curRung.IndexOf("OPC") + 4, 4);
            string _temp = _word.Replace("OP:", "").Replace("]", "");
            if (_temp.Contains("-") || !_temp.Contains(":")) return;

            //not sending the hardcodeddata tag data into online monitoring tag.
            if (_temp.Equals("A5:999"))
                return;

            if (xm.LoadedProject.Tags.Where(L => L.LogicalAddress == _temp).Count() > 0)
            {
                AddressDataTypes datatype = GetAddressType(xm.LoadedProject.Tags.Where(L => L.LogicalAddress == _temp).Select(L => L.Label).FirstOrDefault());

                if (xm.LoadedProject.Tags.Where(L => L.LogicalAddress == _temp).Select(L => L.Model).FirstOrDefault() == "User Defined Tags")
                {
                    CurBlockAddress[_temp] = Tuple.Create(tagToAddress[xm.LoadedProject.Tags.Where(L => L.LogicalAddress == _temp).Select(L => L.Tag).First().ToString()].Item1, datatype);

                    if (opcodeofFB == "03A2")
                        AddPackUnpackBlockInputs(_temp, datatype, rungNo);
                }
                else
                {
                    curADT = _temp.Contains('.') ? AddressDataTypes.BOOL : curADT;
                    curADT = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == _temp).Select(t => t.IoList).FirstOrDefault().ToString() == "NIL"
                        ? (AddressDataTypes)Enum.Parse(typeof(AddressDataTypes), xm.LoadedProject.Tags.Where(t => t.LogicalAddress == _temp).Select(t => t.Label).FirstOrDefault().ToString().ToUpper().Replace("DOUBLE WORD", "DWORD"))
                        : curADT;

                    string[] conversionPrefixes = { "02A", "02B", "02C", "02D", "02E", "02F", "030" };
                    if (conversionPrefixes.Any(prefix => opcodeofFB.StartsWith(prefix)))
                        curADT = CheckConvrsionForOutPut(opcodeofFB);

                    CurBlockAddress[_temp] = Tuple.Create(tagToAddress[xm.LoadedProject.Tags.Where(L => L.LogicalAddress == _temp).Select(L => L.Tag).First().ToString()].Item1, curADT);
                }
            }
            else
            {
                CurBlockAddress[_temp] = Tuple.Create(_temp, optype);
            }

            AddToVisibleRungs(rungNo, _temp);
            AddToAddressValues(_temp);

            //if (opcodeofFB == "03A2")
            //    AddPackUnpackBlockInputs(_temp, curADT, rungNo);
        }

        private void ProcessSimpleWord(string _curWord, int rungNo)
        {
            if (_curWord == "AND" || _curWord == "OR")
                return;

            _curWord = _curWord.Replace("~", "");
            if (_curWord.Contains("{"))
            {
                _curWord = _curWord.Substring(0, _curWord.IndexOf("{"));
            }

            if (tagToAddress.ContainsKey(_curWord))
            {
                CurBlockAddress[_curWord] = Tuple.Create(tagToAddress[_curWord].Item1, AddressDataTypes.BOOL);
                AddToVisibleRungs(rungNo, _curWord);
                AddToAddressValues(_curWord);
            }
        }

        private void AddPackUnpackBlockInputs(string _temp, AddressDataTypes curADT, int rungNo)
        {
            if (!string.IsNullOrEmpty(_temp))
            {
                string[] parts = _temp.Split(':');
                int secondPart;
                int.TryParse(parts[1], out secondPart);
                for (int j = 1; j < 16; j++)
                {
                    int lastTagAdd = secondPart + j;
                    string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                    CurBlockAddress[endAdd] = Tuple.Create(tagToAddress[xm.LoadedProject.Tags.Where(L => L.LogicalAddress == endAdd).Select(L => L.Tag).First().ToString()].Item1, curADT);
                    AddToVisibleRungs(rungNo, endAdd);
                    AddToAddressValues(endAdd);
                }
            }
        }
        private AddressDataTypes CheckConvrsionForOutPut(string opcodeofFB)
        {
            var typeMap = new Dictionary<string, AddressDataTypes>
                {
                { "0", AddressDataTypes.BOOL },
                { "1", AddressDataTypes.BYTE },
                { "2", AddressDataTypes.WORD },
                { "3", AddressDataTypes.DWORD },
                { "4", AddressDataTypes.INT },
                { "5", AddressDataTypes.REAL },
                { "C", AddressDataTypes.DINT }
                };
            return typeMap.TryGetValue(opcodeofFB.Substring(opcodeofFB.Length - 1, 1), out var dataType) ? dataType : xm.LoadedProject.CPUDatatype.Equals("Real") ? AddressDataTypes.REAL : AddressDataTypes.WORD;
        }

        public void SetCurrentCanvas(LadderCanvas Canvas)
        {
            CurCanvas = Canvas;
        }

        /// <summary>
        /// Get current active rungs
        /// </summary>
        public bool SendActiveRungAddress()
        {
            List<int> activeRungs = LadderDesign.GetActiveRungNo();
            List<string> addressList = new List<string>();
            foreach (int rung in activeRungs)
            {
                addressList.AddRange(VisibleRungsDic[rung + 1]);
            }
            ///Send the status address in every cycle to check status of CPU
            string tagName = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == XMPS.Instance.LoadedProject.PLCStatusTag).Select(t => t.Tag).FirstOrDefault();
            if (tagName != null)
            {
                addressList.Add(tagName);
                AddToAddressValues(XMPS.Instance.LoadedProject.PLCStatusTag.ToString());
                CurBlockAddress[tagName] = Tuple.Create(XMPS.Instance.LoadedProject.PLCStatusTag, AddressDataTypes.WORD);
            }
            // Send the error status address in every cycle to check status of CPU
            foreach (string errorTagAddress in XMPS.Instance.LoadedProject.ErrorStatusTags)
            {
                XMIOConfig errTagDtl = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == errorTagAddress).FirstOrDefault();
                if (Enum.GetNames(typeof(AddressDataTypes)).Any(name => name.Equals(errTagDtl.Label, StringComparison.OrdinalIgnoreCase)))
                {
                    addressList.Add(errTagDtl.Tag);
                    AddToAddressValues(errTagDtl.LogicalAddress.ToString());
                    CurBlockAddress[errTagDtl.Tag] = Tuple.Create(errTagDtl.LogicalAddress, (AddressDataTypes)Enum.Parse(typeof(AddressDataTypes), errTagDtl.Label, true));
                }
            }
            int cntTotalAddresses = addressList.Distinct().Count();
            int cntUsedAddresses = 0;
            while (cntUsedAddresses <= cntTotalAddresses)
            {
                onlineMonitor.GetValues(addressList.Distinct().Skip(cntUsedAddresses).Take(cntUsedAddresses + 50).ToList(), ref CurBlockAddress, ref AddressValues, out string Result);
                if (Result.Contains("fail") || Result.Contains("Fail"))  return false;
                cntUsedAddresses = cntUsedAddresses + 50;
            }
            OnlineMonitoringStatus.PopulateAddressValues(ref AddressValues, ref CurBlockAddress);
            CurCanvas.Invalidate();
            return OnlineMonitoringStatus.isOnlineMonitoring;
        }

        public bool SendActiveRungAddress(List<string> addressList)
        {
            foreach (string address in addressList)
            {
                AddToAddressValues(address);
                CurBlockAddress[address] = Tuple.Create(tagToAddress[xm.LoadedProject.Tags.Where(L => L.Tag == address).Select(L => L.Tag).First().ToString()].Item1, AddressDataTypes.BOOL);
            }
            onlineMonitor.GetValues(addressList.Distinct().ToList(), ref CurBlockAddress, ref AddressValues, out string Result);
            OnlineMonitoringStatus.PopulateAddressValues(ref AddressValues, ref CurBlockAddress);
            //LadderCanvas.Active.RefreshCanvas();
            //LadderCanvas.Active.Invalidate();
            CurCanvas.Invalidate();
            return OnlineMonitoringStatus.isOnlineMonitoring;        
        }
    }
}
