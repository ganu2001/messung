using LadderDrawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.LadderLogic;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMPS2000
{
    public class FindAndReplaceLogic
    {
        XMPS xm;
        private LadderWindow _objParent;
        private List<string> skipedElementId = new List<string>();
        private List<LadderElement> _findList = new List<LadderElement>();
        private List<LadderElement> _findMainList = new List<LadderElement>();
        public List<LadderDrawing.Attribute> L1 = new List<LadderDrawing.Attribute>();
        public Dictionary<string, int> blkWiseCount = new Dictionary<string, int>();
        private int nextbtnClickCount = 0;
        private int currentLogicBlockNo = 0;

        public static string _functionblockAttribute;

        public FindAndReplaceLogic(XMPS xmInstance, LadderWindow parentWindow)
        {
            xm = xmInstance;
            _objParent = parentWindow;
        }

        public void FindNext(string findText, string logicBlockCheck, bool IsProjectUpdated)
        {
            blkWiseCount.Clear();
            var tempFindList = new List<Tuple<string, string>>(xm.FindList);
            xm.FindDevicesList = new List<Tuple<string, string>>();
            xm.FindInMainBlockList = new List<Tuple<string, string>>();
            string currentScreen = xm.CurrentScreen.Split('#')[1];
            var findTagsBlocks = GetLogicalBlockNames(currentScreen, findText, IsProjectUpdated);
            if (findTagsBlocks.Count > 0)
            {
                var findBlock = findTagsBlocks[0];
                //LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"LadderForm#{findBlock.Item1}"];
                LadderWindow _windowRef = (LadderWindow)(findBlock.Item1.ToString().EndsWith(" Logic") ? xm.LoadedScreens[$"UDFLadderForm#{findBlock.Item1}"] : xm.LoadedScreens[$"LadderForm#{findBlock.Item1}"]);

                LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
                _objParent = _windowRef;
                frmMain frmMain = new frmMain();
                frmMain.OpenLogicBlockScreen((findBlock.Item1.ToString().EndsWith(" Logic") ? $"UDFLadderForm#{findBlock.Item1}" : $"LadderForm#{findBlock.Item1}"));
                var list = findBlock.Item2.First();
                if (skipedElementId.Contains(list.Item1) && skipedElementId.Count < findBlock.Item2.Count)
                {
                    list = findBlock.Item2[skipedElementId.Count];
                }
                else if (skipedElementId.Count == findBlock.Item2.Count && IsProjectUpdated == false)
                {
                    MessageBox.Show("The Element is not Present in Ladder Window", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    skipedElementId.Clear();
                    if (!IsProjectUpdated)
                        _objParent.Refresh();
                    xm.FindList = new List<Tuple<string, string>>(tempFindList);
                    return;
                }
                var functionblk = findBlock.Item2.Where(T => T.Item1.Equals(list.Item1));
                if (functionblk.Count() > 1)
                {
                    foreach (var ele in functionblk)
                    {
                        xm.FindList.Add(Tuple.Create(ele.Item1, ele.Item2));
                        skipedElementId.Add(ele.Item1);
                    }
                }
                else
                {
                    xm.FindList.Add(Tuple.Create(list.Item1, list.Item2));
                    skipedElementId.Add(list.Item1);
                }
                if (!IsProjectUpdated)
                    _objParent.Refresh();
            }
            else if (IsProjectUpdated == false)
            {
                MessageBox.Show("The Element is not Present in Ladder Window", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void ResetNextButtonCount()
        {
            currentLogicBlockNo = 0;
            nextbtnClickCount = 0;
            skipedElementId.Clear();
        }
        public void FindForEntireLogicBlock(string findText, bool IsProjectUpdate)
        {
            nextbtnClickCount++;
            var findTagsBlocks = GetLogicalBlockNames("", findText, IsProjectUpdate);
            if (findTagsBlocks.Count > 0)
            {
                var selectedIndex = IsProjectUpdate ?
                            (findTagsBlocks.Count > 1 ? findTagsBlocks.Count - 1 : 0) :
                            Math.Min(currentLogicBlockNo, findTagsBlocks.Count - 1);  // Ensure index is within bounds
                var findBlock = findTagsBlocks[selectedIndex];
                if (nextbtnClickCount > findBlock.Item2.Count())
                {
                    currentLogicBlockNo++;
                    if (findTagsBlocks.Count > currentLogicBlockNo)
                        findBlock = findTagsBlocks[currentLogicBlockNo];
                    else if (IsProjectUpdate == false)
                    {
                        MessageBox.Show("The Element is not Present in Ladder Window", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        skipedElementId.Clear();
                        if (!IsProjectUpdate && _objParent != null)
                            _objParent.Refresh();
                        return;
                    }
                }
                LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"LadderForm#{findBlock.Item1}"];

                using (frmMain frmMain = new frmMain())
                {
                    frmMain.OpenLogicBlockScreen($"LadderForm#{findBlock.Item1}");

                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
                    _objParent = _windowRef;

                    var list = findBlock.Item2.First();

                    if (skipedElementId.Contains(list.Item1) && skipedElementId.Count < findBlock.Item2.Count)
                    {
                        list = findBlock.Item2[skipedElementId.Count];
                    }
                    else if (skipedElementId.Count == findBlock.Item2.Count && IsProjectUpdate == false)
                    {
                        MessageBox.Show("The Element is not Present in Ladder Window", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        skipedElementId.Clear();
                        _objParent.Refresh();
                        return;
                    }
                    var functionblk = findBlock.Item2.Where(T => T.Item1.Equals(list.Item1));
                    if (functionblk.Count() > 1)
                    {
                        foreach (var ele in functionblk)
                        {
                            xm.FindList.Add(Tuple.Create(ele.Item1, ele.Item2));
                            skipedElementId.Add(ele.Item1);
                            nextbtnClickCount++;
                        }
                        --nextbtnClickCount;
                    }
                    else
                    {
                        xm.FindList.Add(Tuple.Create(list.Item1, list.Item2));
                        skipedElementId.Add(list.Item1);
                    }
                    if (!IsProjectUpdate)
                        _objParent.Refresh();
                }
            }
            else if (xm.FindDevicesList != null && (xm.FindDevicesList.Count() > 0 || xm.FindInMainBlockList.Count > 0))
            {
                if (IsProjectUpdate == false)
                {
                    //MessageBox.Show("The Element is Present in Modbus  Mqtt windows", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (IsProjectUpdate == false)
                {
                    MessageBox.Show("The Element is not Present in Ladder Window", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void FindInOtherLocation(string findText)
        {
            var ModbusTcpClientList = xm.LoadedProject.Devices
                    .FirstOrDefault(d => d.GetType().Name == "MODBUSTCPClient") as MODBUSTCPClient;
            if (ModbusTcpClientList != null)
            {
                var matchingSlave = ModbusTcpClientList.Slaves
                    .FirstOrDefault(slave => slave.Tag != null && slave.Tag.Contains(findText));
                if (matchingSlave != null)
                {
                    xm.FindDevicesList.Add(Tuple.Create(matchingSlave.Name, findText));
                }
            }

            // ModbusTcpServer
            var ModbusTcpServerList = xm.LoadedProject.Devices
                .FirstOrDefault(d => d.GetType().Name == "MODBUSTCPServer") as MODBUSTCPServer;
            if (ModbusTcpServerList != null)
            {
                var matchingSlave = ModbusTcpServerList.Requests
                    .FirstOrDefault(request => request.Tag != null && request.Tag.Contains(findText));
                if (matchingSlave != null)
                {
                    xm.FindDevicesList.Add(Tuple.Create(matchingSlave.Name, findText));
                }
            }

            // ModbusRtuMaster
            var ModbusRtuMaster = xm.LoadedProject.Devices
                .FirstOrDefault(d => d.GetType().Name == "MODBUSRTUMaster") as MODBUSRTUMaster;
            if (ModbusRtuMaster != null)
            {
                var matchingSlave = ModbusRtuMaster.Slaves
                    .FirstOrDefault(slave => slave.Tag != null && slave.Tag.Contains(findText));
                if (matchingSlave != null)
                {
                    xm.FindDevicesList.Add(Tuple.Create(matchingSlave.Name, findText));
                }
            }

            // ModbusRtuSlaves
            var ModbusRtuSlaves = xm.LoadedProject.Devices
                .FirstOrDefault(d => d.GetType().Name == "MODBUSRTUSlaves") as MODBUSRTUSlaves;
            if (ModbusRtuSlaves != null)
            {
                var matchingSlave = ModbusRtuSlaves.Slaves
                    .FirstOrDefault(slave => slave.Tag != null && slave.Tag.Contains(findText));
                if (matchingSlave != null)
                {
                    xm.FindDevicesList.Add(Tuple.Create(matchingSlave.Name, findText));
                }
            }

            //MQTT Publish
            var publish = xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "Publish") as Publish;
            if (publish != null)
            {
                string logicaladdress = xm.LoadedProject.Tags.Where(T => T.Tag == findText).Select(p => p.LogicalAddress).FirstOrDefault();
                if (!string.IsNullOrEmpty(logicaladdress))
                {
                    var matchingRequests = publish.PubRequest
                        .Where(request => request.Tag != null && request.Tag.Contains(logicaladdress)).ToList();

                    foreach (var req in matchingRequests)
                    {
                        xm.FindDevicesList.Add(Tuple.Create(req.req, findText));
                    }
                }
            }
            var subscribe = xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "Subscribe") as Subscribe;
            if (subscribe != null)
            {
                string logicalAddress = xm.LoadedProject.Tags.Where(T => T.Tag == findText).Select(p => p.LogicalAddress).FirstOrDefault();
                {
                    if (!string.IsNullOrEmpty(logicalAddress))
                    {
                        var matchingRequests = subscribe.SubRequest.Where(request => request.Tag != null && request.Tag.Contains(logicalAddress)).ToList();
                        foreach (var req in matchingRequests)
                        {
                            xm.FindDevicesList.Add(Tuple.Create(req.req, findText));
                        }
                    }
                }
            }

            string logicalAddressforHSIO = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.Tag == findText).LogicalAddress;
            if (!string.IsNullOrEmpty(logicalAddressforHSIO))
            {
                var hsioFunction = XMProValidator.CheckInHSIOBlocks(logicalAddressforHSIO);
                if (hsioFunction != null)
                {
                    xm.FindDevicesList.Add(Tuple.Create("HSIO", findText));
                }

            }
        }
        private List<(string, List<(string, string)>)> GetLogicalBlockNames(string currentBlockName, string findText, bool IsProjectUpdated)
        {
            List<(string, List<(string, string)>)> logicBlock = new List<(string, List<(string, string)>)>();
            _functionblockAttribute = "";
            _findList.Clear();
            xm.FindList.Clear();
            string text = findText;

            var checkinTag = text.Contains(":") ? xm.LoadedProject.Tags.Where(T => T.LogicalAddress.Equals(text)).ToList() : xm.LoadedProject.Tags.Where(T => T.Tag.Equals(text)).ToList();
            XMIOConfig showtag = null;
            if (text.Contains(":"))
            {
                showtag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.ToString() == text).FirstOrDefault();
                if (showtag == null)
                {
                    return logicBlock;
                }
            }
            List<string> BlockNames = new List<string>();

            if (currentBlockName == "")
                BlockNames = xm.LoadedProject.Blocks.Where(T => T.Type == "LogicBlock" || T.Type == "InterruptLogicBlock").OrderByDescending(T => T.Type).Select(T => T.Name).ToList();
            else
                BlockNames = xm.LoadedProject.Blocks.Where(T => T.Name.Equals(currentBlockName)).Select(T => T.Name).ToList();
            int currentFindTagCount = 0;
            foreach (string blkName in BlockNames)
            {
                LadderWindow _windowRef = null; string key = blkName.EndsWith(" Logic") ? $"UDFLadderForm#{blkName}" : $"LadderForm#{blkName}"; _objParent = _windowRef;
                if (xm.LoadedScreens.ContainsKey(key))
                {
                    _windowRef = (LadderWindow)xm.LoadedScreens[key];
                    _objParent = _windowRef;
                }
                else
                {
                    continue;
                }
                LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
                for (int i = 0; i < LadderDrawing.LadderDesign.Active.Elements.Count(); i++)
                {
                    //Change for accepting tag if the tag is ShowLogicalAddress is true
                    var foundElement3 = text.Contains(":") ? LadderDrawing.LadderDesign.Active.Elements[i].Elements.Where(t => t.Attributes["Caption"].Equals(showtag.Tag)).ToList() : LadderDrawing.LadderDesign.Active.Elements[i].Elements.Where(t => t.Attributes["Caption"].Equals(text)).ToList();
                    _findList.AddRange(foundElement3);
                    foreach (LadderElement ld in LadderDrawing.LadderDesign.Active.Elements[i].Elements)
                    {
                        if (ld.CustomType == "LadderDrawing.Coil")
                        {
                            if (ld.Elements.Count > 0 /*&& text.Contains(":")*/)
                                GetCoilParallelElement(ld.Elements.First(), text.Contains(":") ? showtag.Tag : text);
                        }
                    }
                    //Searching For DummyParallel & Other Elements                          -----> ConnectTo
                    for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[i].Elements.Count; j++)
                    {
                        var DummyCheck = LadderDrawing.LadderDesign.Active.Elements[i].Elements[j];
                        if (DummyCheck.CustomType == "LadderDrawing.DummyParallelParent")
                        {
                            CheckInChildElements(DummyCheck.Elements, text, ref _findList);
                        }
                    }
                    var ElementListFB = LadderDrawing.LadderDesign.Active.Elements[i].Elements;
                    if (ElementListFB.Count() != 0 && checkinTag.Count() != 0 && LadderDrawing.LadderDesign.Active.Elements[i].Elements[ElementListFB.Count() - 1].CustomType == "LadderDrawing.FunctionBlock")
                    {
                        var _presentTag = text.Contains(":") ? checkinTag.Where(T => T.LogicalAddress.Contains(text)).Select(T => T.LogicalAddress).First() : checkinTag.Where(T => T.Tag.Contains(text)).Select(T => T.LogicalAddress).First();
                        foreach (var element in ElementListFB)
                        {
                            if (element.CustomType == "LadderDrawing.FunctionBlock")
                            {
                                var _getEle = element.Attributes.Where(t => t.Value.ToString().Replace("~", "").Equals(_presentTag)).Any();     //As Address is Present in Attribute

                                var _getEle2 = element.Attributes.Where(t => t.Value.ToString().Replace("~", "").Equals(text)).Any();

                                //Gets the count of attribute present in Element
                                var check2 = element.Attributes.Where(T => T.Value.ToString().Replace("~", "").Equals(_presentTag)).Count();

                                if (_getEle)
                                {
                                    while (check2 > 0)
                                    {
                                        _findList.Add(element);
                                        _functionblockAttribute = checkinTag.Select(T => T.Tag).FirstOrDefault();
                                        check2--;
                                    }
                                }
                                else if (_getEle2)
                                {
                                    _findList.Add(element);
                                    _functionblockAttribute = checkinTag.Select(T => T.Tag).FirstOrDefault();
                                }
                            }
                        }
                    }
                }
                if (_findList.Count() == 0)
                {
                }
                else
                {
                    if (_findList.Count() > currentFindTagCount)
                    {
                        List<(string, string)> tuplelist = new List<(string, string)>();
                        foreach (var element in _findList)
                        {
                            string value = "";
                            string tagname = string.Empty;
                            if (element.CustomType == "LadderDrawing.FunctionBlock")
                            {
                                value = element.Id;
                                tagname = _functionblockAttribute.ToString();
                            }
                            else
                            {
                                value = element.Id;
                                tagname = element.Attributes["Caption"].ToString();

                            }
                            tuplelist.Add((value, tagname));
                        }
                        logicBlock.Add((blkName, tuplelist));
                        if (!blkWiseCount.ContainsKey(blkName))
                        {
                            blkWiseCount.Add(blkName, (_findList.Count() - currentFindTagCount));
                        }
                    }
                    currentFindTagCount = _findList.Count();
                }
                LadderCanvas.RefreshCanvas();
                if (!IsProjectUpdated)
                    _objParent.Refresh();
            }
            return logicBlock;
        }
        private void CheckInChildElements(LadderElements elements, string enteredText, ref List<LadderElement> findlist)
        {
            foreach (var element in elements)
            {
                if (element.Attributes.Any(attr => attr.Value.Equals(enteredText)))
                {
                    _findList.Add(element);
                }
                if (element.Elements.Any())
                {
                    CheckInChildElements(element.Elements, enteredText, ref findlist);
                }
            }
        }
        private string CheckDatatype(string tagName)
        {
            if (tagName == "???") return "";

            var tagInfo = xm.LoadedProject.Tags.Where(T => tagName.Contains(":") ? T.LogicalAddress == tagName : T.Tag == tagName).FirstOrDefault();
            if (tagInfo.Model == null || tagInfo.Model.Contains("Tags"))
                if (tagInfo.LogicalAddress.Contains("."))
                    return "Bool";
                else
                    return tagInfo.Label;
            else
            {
                if (tagInfo.LogicalAddress.Contains("."))
                    return "Bool";
                else
                    return "";
            }
        }

        private void GetCoilParallelElement(LadderElement element, string findtext)
        {
            LadderElement coilParallel = null;
            //Get The Next Element 
            if (element.Elements.Count > 0)
            {
                coilParallel = element.Elements.First();
            }
            if (element.Attributes.Where(t => t.Value.Equals(findtext)).Any())
            {
                _findList.Add(element);
            }
            //Base case
            if (element.Elements.Count == 0)
                return;

            GetCoilParallelElement(coilParallel, findtext);
        }
        private void ValidateTags(string val1, string val2, bool IsProjectUpdate)
        {
            string fType = CheckDatatype(val1), rType = CheckDatatype(val2);
            if (fType != rType)
            {
                if ((fType == "" && rType != "Bool") || (rType == "" && fType != "Bool"))
                {
                }
                else
                {
                    MessageBox.Show("Datatype of both the tags is not matching", "XMPX2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _findList.Clear();
                    LadderCanvas.RefreshCanvas();
                    if (!IsProjectUpdate)
                        _objParent.Refresh();
                    return;
                }
            }
        }

        public void ReplaceText(string FindcmbText, string ReplaceText)
        {
            XMIOConfig showtag = null;
            if (FindcmbText.Contains(":"))
            {
                //All Related Information for findelement
                showtag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.ToString() == FindcmbText).First();
            }
            XMIOConfig Replacetag = null;
            if (ReplaceText.Contains(":"))
            {
                //All Related Information for Replaceelement
                Replacetag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.ToString() == ReplaceText).First();
            }
            var checkinTag = FindcmbText.Contains(":") ? xm.LoadedProject.Tags.Where(T => T.LogicalAddress.Equals(FindcmbText)).ToList() : xm.LoadedProject.Tags.Where(T => T.Tag.Equals(FindcmbText)).ToList();
            var LogicalAddress = checkinTag[0].LogicalAddress;
            var Tag = checkinTag[0].Tag;
            var ReplaceElement = ReplaceText.Contains(":") ? xm.LoadedProject.Tags.Where(T => T.LogicalAddress.Equals(ReplaceText)).ToList() : xm.LoadedProject.Tags.Where(T => T.Tag.Equals(ReplaceText)).ToList();
            var UpdateFindElement = ReplaceElement.Count > 0 ? ReplaceElement[0].LogicalAddress : "???";
            //Validation For Type 
            ValidateTags(FindcmbText, ReplaceText, false);
            if (_findList.Count == 0)
            {
                MessageBox.Show("No Element is Present ", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (xm.FindList.Count > 0)
                {
                    var currentFindEle = xm.FindList.First();
                    var Elements = _findList.Where(T => T.Id.Equals(currentFindEle.Item1)).ToList();
                    foreach (var Element in Elements)
                    {
                        --nextbtnClickCount;
                        skipedElementId.Remove(currentFindEle.Item1);
                        if (Element.CustomType == "LadderDrawing.FunctionBlock")
                        {
                            var ElementListInfo = Element.Attributes.Where(T => T.Value.ToString().Replace("~", "").Equals(LogicalAddress)).ToList();  //Find the Attribute to change with ReplaceText
                            if (L1.Count() == 0)
                            {
                                L1.AddRange(ElementListInfo);
                            }
                            var ElementTagInfo = Element.Attributes.Where(T => T.Value.Equals(Tag)).ToList();
                            if (ElementListInfo.Count > 0)
                                ElementListInfo.ElementAt(0).Value = ElementListInfo.ElementAt(0).Value.ToString().StartsWith("~") ? "~" + UpdateFindElement : UpdateFindElement;
                            LadderCanvas.RefreshCanvas();   //Check 
                            _objParent.Refresh();
                            if (L1.Count > 0)
                                L1.RemoveAt(0);
                        }
                        else
                        {
                            Element.Attributes["Caption"] = ReplaceElement[0].Tag;
                            string logicAdd = xm.LoadedProject.Tags.Where(T => T.Tag.Equals(ReplaceElement[0].Tag)).Select(T => T.LogicalAddress).FirstOrDefault();
                            Element.Attributes["LogicalAddress"] = logicAdd;
                            LadderCanvas.RefreshCanvas();
                            _objParent.Refresh();
                        }
                        if (L1.Count() == 0)
                            _findList.Remove(Element);
                    }
                }
            }
        }
        public void ReplaceAllText(string FindcmbText, string ReplaceText, bool IsProjectUpdate, XMIOConfig tag, bool isEntireProject = false)
        {
            XMIOConfig showtag = null;
            if (FindcmbText.Contains(":"))
            {
                var matchingTags = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.ToString() == FindcmbText).ToList();
                if (matchingTags.Count == 0)
                {
                    return;
                }
                showtag = matchingTags.First();
            }
            XMIOConfig Replacetag = null;
            if (ReplaceText.Contains(":"))
            {
                var matchingReplaceTags = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.ToString() == ReplaceText).ToList();
                if (matchingReplaceTags.Count == 0)
                {
                    return;
                }
                Replacetag = matchingReplaceTags.First();
            }
            //Testing for Now
            var checkinTag = FindcmbText.Contains(":") ? xm.LoadedProject.Tags.Where(T => T.LogicalAddress.Equals(FindcmbText)).ToList() : xm.LoadedProject.Tags.Where(T => T.Tag.Equals(FindcmbText)).ToList();
            if (checkinTag.Count < 1) return;
            var LogicalAddress = checkinTag[0].LogicalAddress;
            var Tag = checkinTag[0].Tag;
            CommonFunctions.UpdateTagNames(LogicalAddress, Tag);
            var ReplaceElement = ReplaceText.Contains(":") ? xm.LoadedProject.Tags.Where(T => T.LogicalAddress.Equals(ReplaceText)).ToList() : xm.LoadedProject.Tags.Where(T => T.Tag.Equals(ReplaceText)).ToList();
            if (ReplaceElement.Count == 0) return;
            var UpdateFindElement = ReplaceElement[0].LogicalAddress;
            ValidateTags(FindcmbText, ReplaceText, IsProjectUpdate);
            if (_findList.Count == 0 && xm.FindDevicesList != null && IsProjectUpdate == false && xm.FindDevicesList.Count == 0 && xm.FindInMainBlockList.Count == 0)
            {
                MessageBox.Show("No Element is Present ", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (_findList.Count > 0)
            {
                for (int i = _findList.Count; i > 0; i--)
                {
                    var Element = _findList.First();
                    if (Element.CustomType == "LadderDrawing.FunctionBlock")
                    {
                        var ElementListInfo = Element.Attributes.Where(T => T.Value.ToString().Replace("~", "").Equals(LogicalAddress)).ToList();  //Find the Attribute to change with ReplaceText
                        if (L1.Count() == 0)
                        {
                            L1.AddRange(ElementListInfo);
                        }
                        var ElementTagInfo = Element.Attributes.Where(T => T.Value.Equals(Tag)).ToList();
                        if (ElementListInfo.Count > 0)
                        {
                            if (tag != null && UpdateFindElement != tag.LogicalAddress)
                            {
                                UpdateFindElement = tag.LogicalAddress;
                            }
                            ElementListInfo.ElementAt(0).Value = ElementListInfo.ElementAt(0).Value.ToString().StartsWith("~") ? "~" + UpdateFindElement : UpdateFindElement;

                        }
                        LadderCanvas.RefreshCanvas();
                        if (!IsProjectUpdate)
                            _objParent.Refresh();
                        if (L1.Count > 0)
                            L1.RemoveAt(0);
                    }
                    else
                    {
                        if (tag == null)
                        {
                            Element.Attributes["Caption"] = ReplaceElement[0].Tag;
                            string logicAdd = xm.LoadedProject.Tags.Where(T => T.Tag.Equals(ReplaceElement[0].Tag)).Select(T => T.LogicalAddress).FirstOrDefault();
                            Element.Attributes["LogicalAddress"] = logicAdd;
                            LadderCanvas.RefreshCanvas();
                            if (!IsProjectUpdate)
                                _objParent.Refresh();
                        }
                        if (tag != null)
                        {
                            if (Element.Attributes["LogicalAddress"].ToString() == tag.LogicalAddress)
                            {
                                Element.Attributes["Caption"] = ReplaceElement[0].Tag;
                                string logicAdd = xm.LoadedProject.Tags.Where(T => T.Tag.Equals(ReplaceElement[0].Tag)).Select(T => T.LogicalAddress).FirstOrDefault();
                                Element.Attributes["LogicalAddress"] = logicAdd;
                                LadderCanvas.RefreshCanvas();
                                if (!IsProjectUpdate)
                                    _objParent.Refresh();
                            }
                            else
                            {
                                if (Element.Attributes["caption"].ToString() == tag.Tag)
                                {
                                    Element.Attributes["LogicalAddress"] = tag.LogicalAddress;
                                }
                                else
                                {
                                    Element.Attributes["LogicalAddress"] = tag.LogicalAddress;
                                    Element.Attributes["caption"] = tag.Tag;
                                }
                                LadderCanvas.RefreshCanvas();
                                if (!IsProjectUpdate)
                                    _objParent.Refresh();
                            }
                        }
                        //var tag = xm.LoadedProject.Tags.Where(t => t.Tag.Equals(ReplaceText)).FirstOrDefault();
                    }
                    if (L1.Count() == 0)
                        _findList.Remove(Element);
                    else
                        _findList.Remove(Element);
                }
            }
            if (!IsProjectUpdate)
            {
                if (xm.FindDevicesList != null && xm.FindDevicesList.Count > 0 && _findList.Count == 0)
                {
                    for (int i = xm.FindDevicesList.Count - 1; i >= 0; i--)
                    {
                        var item = xm.FindDevicesList[i];
                        string deviceName = item.Item1;
                        string originalText = item.Item2;

                        // Update MODBUSTCPClient
                        if (deviceName.Contains("MODBUSTCPClient"))
                        {
                            var tcpClient = xm.LoadedProject.Devices
                                .FirstOrDefault(d => d.GetType().Name == "MODBUSTCPClient") as MODBUSTCPClient;
                            if (tcpClient != null)
                            {
                                foreach (var slave in tcpClient.Slaves)
                                {
                                    if (slave.Name.Equals(deviceName))
                                    {
                                        if (slave.Tag != null && slave.Tag.Contains(originalText))
                                        {
                                            slave.Tag = slave.Tag.Replace(originalText, ReplaceText);
                                            slave.Variable = UpdateFindElement /*slave.Variable.Replace(slave.Variable, UpdateFindElement)*/;
                                        }
                                    }
                                }
                            }
                        }
                        // Update MODBUSTCPServer
                        else if (deviceName.Contains("MODBUSTCPServer"))
                        {
                            var tcpServer = xm.LoadedProject.Devices
                                .FirstOrDefault(d => d.GetType().Name == "MODBUSTCPServer") as MODBUSTCPServer;
                            if (tcpServer != null)
                            {
                                foreach (var request in tcpServer.Requests)
                                {
                                    if (request.Name.Equals(deviceName))
                                    {
                                        if (request.Tag != null && request.Tag.Contains(originalText))
                                        {
                                            request.Tag = request.Tag.Replace(originalText, ReplaceText);
                                            request.Variable = UpdateFindElement/*request.Tag.Replace(request.Variable, UpdateFindElement)*/;
                                        }
                                    }
                                }
                            }
                        }
                        // Update MODBUSRTUMaster
                        else if (deviceName.Contains("MODBUSRTUMaster"))
                        {
                            var rtuMaster = xm.LoadedProject.Devices
                                .FirstOrDefault(d => d.GetType().Name == "MODBUSRTUMaster") as MODBUSRTUMaster;
                            if (rtuMaster != null)
                            {
                                foreach (var slave in rtuMaster.Slaves)
                                {
                                    if (slave.Name.Equals(deviceName))
                                    {
                                        if (slave.Tag != null && slave.Tag.Contains(originalText))
                                        {
                                            slave.Tag = slave.Tag.Replace(originalText, ReplaceText);
                                            slave.Variable = UpdateFindElement /*slave.Tag.Replace(slave.Variable, UpdateFindElement)*/;
                                        }
                                    }
                                }
                            }
                        }
                        // Update MODBUSRTUSlaves
                        else if (deviceName.Contains("MODBUSRTUSlaves"))
                        {
                            var rtuSlaves = xm.LoadedProject.Devices
                                .FirstOrDefault(d => d.GetType().Name == "MODBUSRTUSlaves") as MODBUSRTUSlaves;
                            if (rtuSlaves != null)
                            {
                                foreach (var slave in rtuSlaves.Slaves)
                                {
                                    if (slave.Name.Equals(deviceName))
                                    {
                                        if (slave.Tag != null && slave.Tag.Contains(originalText))
                                        {
                                            slave.Tag = slave.Tag.Replace(originalText, ReplaceText);
                                            slave.Variable = UpdateFindElement /*slave.Tag.Replace(slave.Variable, UpdateFindElement)*/;
                                        }
                                    }
                                }
                            }
                        }
                        //for HISO function blocks
                        else if (deviceName.Equals("HSIO"))
                        {
                            if (isEntireProject)
                                XMProValidator.ReplaceInHSIOFunctionBlock(FindcmbText, ReplaceText);
                        }
                        // Update MQTT (Publish/Subscribe)
                        else
                        {
                            var publish = xm.LoadedProject.Devices
                                .FirstOrDefault(d => d.GetType().Name == "Publish") as Publish;
                            if (publish != null)
                            {
                                foreach (var request in publish.PubRequest)
                                {
                                    if (request.req.Equals(deviceName))
                                    {
                                        request.Tag = request.Tag.Replace(request.Tag, UpdateFindElement);
                                    }
                                }
                            }
                            var subscribe = xm.LoadedProject.Devices
                                .FirstOrDefault(d => d.GetType().Name == "Subscribe") as Subscribe;
                            if (subscribe != null)
                            {
                                foreach (var request in subscribe.SubRequest)
                                {
                                    if (request.req.Equals(deviceName))
                                    {
                                        request.Tag = request.Tag.Replace(request.Tag, UpdateFindElement);
                                    }
                                }
                            }
                        }
                        // Safely remove item after processing
                        xm.FindDevicesList.RemoveAt(i);
                    }
                }
                if (xm.FindInMainBlockList != null && xm.FindInMainBlockList.Count > 0 && _findList.Count == 0 && xm.FindDevicesList.Count == 0)
                {
                    for (int i = 0; i < xm.FindInMainBlockList.Count; i++)
                    {
                        var ele = xm.FindInMainBlockList[i];
                        var findedElement = ele.Item2;
                        bool isReplaced = false;
                        var replaceData = xm.LoadedProject.MainLadderLogic;
                        for (int j = 0; j < xm.LoadedProject.MainLadderLogic.Count; j++)
                        {
                            if (xm.LoadedProject.MainLadderLogic[j].Contains(findedElement))
                            {
                                xm.LoadedProject.MainLadderLogic[j] =
                                    xm.LoadedProject.MainLadderLogic[j].Replace(findedElement, ReplaceText);
                                isReplaced = true;
                            }
                        }
                        if (isReplaced)
                        {
                            xm.FindInMainBlockList.Clear();
                        }
                    }
                }
            }
        }
        internal void FindInMainBlock(string findText, bool IsProjectUpdate)
        {
            xm.FindInMainBlockList.Clear();
            var mainLogicData = xm.LoadedProject.MainLadderLogic;
            foreach (var data in mainLogicData)
            {
                if (data.Contains(findText))
                {
                    xm.FindInMainBlockList.Add(Tuple.Create(data, findText));
                }
            }
        }
    }
}
