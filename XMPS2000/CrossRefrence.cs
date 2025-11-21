using iTextSharp.text;
using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.LadderLogic;
using XMPS2000.LadderLogic;

namespace XMPS2000
{
    public partial class CrossRefrence : Form
    {
        XMPS xm;
        public LadderElement ladderElement = LadderDesign.ClickedElement;
        private bool isUDFBScreen;
        public CrossRefrence()
        {
            //Return if element is not selected
            if (ladderElement == null)
            {
                return;
            }
            xm = XMPS.Instance;
            InitializeComponent();
            string firstLogicalAdd = ladderElement.Attributes
                                     .Where(t => (t.Name.StartsWith("input") || t.Name.StartsWith("output")) && t.Value.ToString().Contains(":"))
                                     .Select(t => t.Value.ToString()).FirstOrDefault();

            isUDFBScreen = xm.CurrentScreen.StartsWith("UDFLadderForm") ? true : false;
            if (isUDFBScreen && ladderElement.CustomType == "LadderDrawing.FunctionBlock")
            {
                firstLogicalAdd = firstLogicalAdd == null ? ladderElement.Attributes
                                         .Where(t => (t.Name.StartsWith("input") || t.Name.StartsWith("output")))
                                         .Select(t => t.Value.ToString()).FirstOrDefault() : firstLogicalAdd;
            }
            else if (isUDFBScreen && (ladderElement.CustomType.Equals("LadderDrawing.Coil") || ladderElement.CustomType.Equals("LadderDrawing.Contact")))
            {
                firstLogicalAdd = ladderElement.Attributes["caption"].ToString();
            }
            bool isUDFBVariable = isUDFBScreen ? xm.LoadedProject.UDFBInfo.Where(T => T.UDFBName.Equals(xm.CurrentScreen.Split('#')[1].ToString().Replace(" Logic", ""))).FirstOrDefault().UDFBlocks.Any(t => t.Text.Equals(firstLogicalAdd))
                                  : false;

            if (isUDFBScreen && isUDFBVariable)
            {
                List<Block> BlockCount = xm.LoadedProject.Blocks.Where(T => T.Type.Equals("UDFB") && T.Name.Equals(xm.CurrentScreen.Split('#')[1].ToString())).ToList();
                textBox1.Text = firstLogicalAdd;
                return;
            }
            var Element = ladderElement.CustomType.Equals("LadderDrawing.FunctionBlock") ? XMProValidator.GetTheTagnameFromAddress(firstLogicalAdd?.Replace("~", "")) : ladderElement.Attributes.Select(T => T.Value).First();
            if (Element != null)
            {
                textBox1.Text = Element.ToString();
            }

        }
        public CrossRefrence(string tag)
        {
            xm = XMPS.Instance;
            InitializeComponent();
            if (tag != null)
            {
                textBox1.Text = tag;
            }
        }
        public DataGridView CrossRefrenceDGV1
        {
            get { return CrossRefrenceDGV; }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string EnteredText = textBox1.Text.Contains(":") ? XMProValidator.GetTheTagnameFromAddress(textBox1.Text) : textBox1.Text;
                isUDFBScreen = xm.CurrentScreen.StartsWith("UDFLadderForm") ? true : false;

                string blockName = xm.CurrentScreen.Split('#')[1].ToString();
                var isCurrentUDFBTag = isUDFBScreen ? xm.LoadedProject.UDFBInfo.Where(T => T.UDFBName.Equals(blockName.Replace(" Logic", ""))).FirstOrDefault().UDFBlocks.Any(t => t.Text.Equals(EnteredText))
                    : false;

                //Datatable to Fill Grid
                DataTable dr = new DataTable();
                dr.Columns.Add("Variable", typeof(string));
                dr.Columns.Add("Location", typeof(string));
                dr.Columns.Add("Type", typeof(string));
                dr.Columns.Add("Address", typeof(string));
                dr.Columns.Add("Rung", typeof(string));

                if (isUDFBScreen && isCurrentUDFBTag)
                {
                    List<Block> BlockCount = xm.LoadedProject.Blocks.Where(T => T.Type.Equals("UDFB") && T.Name.Equals(blockName)).ToList();
                    FindInLogicBlocks(BlockCount, EnteredText, true, ref dr);
                    CrossRefrenceDGV.DataSource = dr;
                    return;
                }
                if (xm.LoadedProject.Tags.Where(t => t.Tag.Equals(EnteredText)).Any() && EnteredText != "")
                {
                    var Tag = xm.LoadedProject.Tags.ToList();

                    //ModbusTcpClient
                    MODBUSTCPClient ModbusTcpClientList = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();

                    //ModbusTcpServer
                    MODBUSTCPServer ModbusTcpServerList = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();

                    //ModbusRtuMaster
                    MODBUSRTUMaster ModbusRtuMaster = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();

                    //ModbusRTUSlaves
                    MODBUSRTUSlaves ModbusRtuSlaves = (MODBUSRTUSlaves)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
                    //check in BacNet objects.
                    string logicalAdd = EnteredText.Contains(":") ? EnteredText : XMProValidator.GetTheAddressFromTag(EnteredText);
                    AnalogIOV analogValue = xm.LoadedProject.BacNetIP != null ? xm.LoadedProject.BacNetIP.AnalogIOValues.Where(d => d.LogicalAddress.Equals(logicalAdd)).FirstOrDefault() : null;
                    BinaryIOV binaryIOV = xm.LoadedProject.BacNetIP != null ? xm.LoadedProject.BacNetIP.BinaryIOValues.Where(d => d.LogicalAddress.Equals(logicalAdd)).FirstOrDefault() : null;
                    MultistateIOV multistateIOV = xm.LoadedProject.BacNetIP != null ? xm.LoadedProject.BacNetIP.MultistateValues.Where(d => d.LogicalAddress.Equals(logicalAdd)).FirstOrDefault() : null;
                    Schedule schedule = xm.LoadedProject.BacNetIP != null ? xm.LoadedProject.BacNetIP.Schedules.Where(d => d.LogicalAddress.Equals(logicalAdd)).FirstOrDefault() : null;




                    //LogicBlock Check
                    List<Block> BlockCount = xm.LoadedProject.Blocks.Where(T => T.Type.Equals("LogicBlock") ||
                                                                   T.Type.Equals("InterruptLogicBlock") || T.Type.Equals("UDFB")).ToList();
                    FindINMainLogicBlock(EnteredText, ref dr);
                    FindInLogicBlocks(BlockCount, EnteredText, false, ref dr);

                    var CrossTag = Tag.Where(D => D.Tag.Contains(EnteredText)).Any();  //UserTag
                    if (CrossTag)
                    {
                        string _locationupdate;
                        string _typeupdate;
                        DataRow data = dr.NewRow();

                        data["Variable"] = EnteredText;

                        if (Tag.Where(d => d.Tag.Contains(EnteredText)).FirstOrDefault().Model != null)
                        {
                            _locationupdate = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Model).First();
                        }
                        else if (Tag.Where(d => d.Tag.Contains(EnteredText)).FirstOrDefault().LogicalAddress.StartsWith("S3"))
                        {
                            _locationupdate = "Default System Tag";
                        }
                        else
                        {
                            _locationupdate = "User Defined Tag";
                        }
                        data["Location"] = _locationupdate;

                        if (_locationupdate != "User Defined Tag")
                        {
                            _typeupdate = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Type).First().ToString();
                        }
                        else
                        {
                            _typeupdate = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Label).First();
                        }

                        data["Type"] = _typeupdate;
                        data["Address"] = Tag.Where(T => T.Tag.Contains(EnteredText)).Select(D => D.LogicalAddress).First();
                        dr.Rows.Add(data);

                    }
                    var CrossTag2 = ModbusTcpClientList.Slaves.Where(D => D.Tag.Contains(EnteredText)).Any();     //ModbusTcpClient
                    if (CrossTag2)
                    {
                        DataRow data = dr.NewRow();

                        data["Variable"] = EnteredText;
                        data["Location"] = "ModbusTcpClient";
                        if (EnteredText.StartsWith("Digital"))
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Type).First();
                        else
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Label).First();
                        data["Address"] = ModbusTcpClientList.Slaves.Where(T => T.Tag.Contains(EnteredText)).Select(D => D.Variable).First();
                        dr.Rows.Add(data);
                    }
                    var CrossTag3 = ModbusTcpServerList.Requests.Where(D => D.Tag.Contains(EnteredText)).Any();   //ModbusTcpServer
                    if (CrossTag3)
                    {

                        DataRow data = dr.NewRow();

                        data["Variable"] = EnteredText;
                        data["Location"] = "ModbusTcpServer";
                        if (EnteredText.StartsWith("Digital"))
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Type).First();
                        else
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Label).First();
                        data["Address"] = ModbusTcpServerList.Requests.Where(T => T.Tag.Contains(EnteredText)).Select(D => D.Variable).First();
                        dr.Rows.Add(data);

                    }
                    var CrossTag4 = ModbusRtuMaster?.Slaves?.Where(D => D.Tag?.Contains(EnteredText) ?? false).Any() ?? false;      //ModbusRtuMaster
                    if (CrossTag4)
                    {
                        DataRow data = dr.NewRow();

                        data["Variable"] = EnteredText;
                        data["Location"] = "ModbusRtuMaster";
                        if (EnteredText.StartsWith("Digital"))
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Type).First();
                        else
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Label).First();
                        data["Address"] = ModbusRtuMaster.Slaves.Where(T => T.Tag.Contains(EnteredText)).Select(t => t.Variable).First();
                        dr.Rows.Add(data);
                    }

                    var CrossTag5 = ModbusRtuSlaves?.Slaves?.Where(D => D.Tag?.Contains(EnteredText) ?? false).Any() ?? false;  //MODBUSRTUSlaves
                    if (CrossTag5)
                    {
                        DataRow data = dr.NewRow();

                        data["Variable"] = EnteredText;
                        data["Location"] = "ModbusRtuSlaves";
                        if (EnteredText.StartsWith("Digital"))
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Type).First();
                        else
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Label).First();
                        data["Address"] = ModbusRtuSlaves.Slaves.Where(T => T.Tag.Contains(EnteredText)).Select(t => t.Variable).First();
                        dr.Rows.Add(data);
                    }

                    //show location if present in BinaryIOV
                    if (binaryIOV != null && xm.PlcModel.Contains("XBLD"))
                    {
                        DataRow data = dr.NewRow();
                        data["Variable"] = EnteredText;
                        data["Location"] = $"BacNet {binaryIOV.ObjectIdentifier.Split(':')[0]} Object";
                        if (EnteredText.StartsWith("Digital"))
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Type).First();
                        else
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Label).First();
                        data["Address"] = binaryIOV.LogicalAddress;
                        dr.Rows.Add(data);
                    }
                    //show location if present in AnalogIOV
                    if (analogValue != null && xm.PlcModel.Contains("XBLD"))
                    {
                        DataRow data = dr.NewRow();
                        data["Variable"] = EnteredText;
                        data["Location"] = $"BacNet {analogValue.ObjectIdentifier.Split(':')[0]} Object";
                        if (EnteredText.StartsWith("Digital"))
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Type).First();
                        else
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Label).First();
                        data["Address"] = analogValue.LogicalAddress;
                        dr.Rows.Add(data);
                    }
                    //show location if present in MultistateIOV
                    if (multistateIOV != null && xm.PlcModel.Contains("XBLD"))
                    {
                        DataRow data = dr.NewRow();
                        data["Variable"] = EnteredText;
                        data["Location"] = $"BacNet {multistateIOV.ObjectIdentifier.Split(':')[0]} Object";
                        if (EnteredText.StartsWith("Digital"))
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Type).First();
                        else
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Label).First();
                        data["Address"] = multistateIOV.LogicalAddress;
                        dr.Rows.Add(data);
                    }
                    //show location if present in Schedule Object
                    if (schedule != null && xm.PlcModel.Contains("XBLD"))
                    {
                        DataRow data = dr.NewRow();
                        data["Variable"] = EnteredText;
                        data["Location"] = $"BacNet {schedule.ObjectIdentifier.Split(':')[0]} Object";
                        if (EnteredText.StartsWith("Digital"))
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Type).First();
                        else
                            data["Type"] = Tag.Where(d => d.Tag.Contains(EnteredText)).Select(T => T.Label).First();
                        data["Address"] = schedule.LogicalAddress;
                        dr.Rows.Add(data);
                    }
                    CrossRefrenceDGV.DataSource = dr;
                }
                else
                {
                    //if there no data found regarding tag or for any other text except tagName
                    CrossRefrenceDGV.DataSource = null;
                }
            }
            catch
            {
                //if there any exception occured then we shown empty grid.
                CrossRefrenceDGV.DataSource = null;
            }
        }

        private void FindINMainLogicBlock(string enteredText, ref DataTable dr)
        {
            var MainBlockRungs = xm.LoadedProject.MainLadderLogic.ToList();
            var Tag = xm.LoadedProject.Tags.ToList();
            int rungCount = 0;
            Regex regex = new Regex(@"\((.*?)\)");
            foreach (var rung in MainBlockRungs)
            {
                rungCount++;
                var matches = regex.Matches(rung).Cast<Match>().Select(m => m.Groups[1].Value).ToList();
                foreach(var match in matches)
                {
                    string sign = "";
                    string address = string.Empty;
                    if(match.Contains(enteredText))
                    {
                        if (match.StartsWith("~")) 
                        {
                            sign = "~"; 
                        }
                        DataRow data = dr.NewRow();
                        data["Variable"] = enteredText;
                        data["Location"] = "Main Logic Block";
                        if (enteredText.StartsWith("Digital"))
                            data["Type"] = Tag.Where(d => d.Tag.Contains(enteredText)).Select(T => T.Type).First();
                        else
                            data["Type"] = Tag.Where(d => d.Tag.Contains(enteredText)).Select(T => T.Label).First();
                        address = Tag.Where(T => T.Tag.Contains(enteredText)).Select(D => D.LogicalAddress).First();
                        data["Address"] = !string.IsNullOrEmpty(sign) ? $"{sign}{address}" : address;
                        data["Rung"] = rungCount;
                        dr.Rows.Add(data);
                    }
                }
            }
        }
        private void FindInLogicBlocks(List<Block> blockCount, string currentText, bool isUDFB, ref DataTable dr)
        {
            var Tag = xm.LoadedProject.Tags.ToList();
            string address = string.Empty;
            for (int i = 0; i < blockCount.Count; i++)
            {
                var BlkList = blockCount[i].Name;
                string formName = blockCount[i].Type.Equals("UDFB") ? $"UDFLadderForm#{blockCount[i].Name}" : $"LadderForm#{blockCount[i].Name}";
                if (xm.LoadedScreens.ContainsKey(formName))
                {
                    LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[formName];
                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
                    for (int J = 0; J < LadderDrawing.LadderDesign.Active.Elements.Count; J++)
                    {
                        int k = 0;
                        var ans2 = LadderDrawing.LadderDesign.Active.Elements[J].Elements.Where(t => t.Attributes["Caption"].Equals(currentText)).Any();        // Check Ele is Present
                        var check = LadderDrawing.LadderDesign.Active.Elements[J].Elements.Where(T => T.Attributes["Caption"].Equals(currentText)).Count();    //Count of Element
                        int _ele = LadderDrawing.LadderDesign.Active.Elements[J].Elements.Count;
                        //check and short All Dummy Parallel Elements.
                        var dummyParallelElements = LadderDrawing.LadderDesign.Active.Elements[J].Elements.Where(t => t.CustomType == "LadderDrawing.DummyParallelParent");
                        if (_ele != 0 && LadderDrawing.LadderDesign.Active.Elements[J].Elements[_ele - 1].CustomType == "LadderDrawing.FunctionBlock")
                        {
                            var _presentTag = isUDFB ? currentText : Tag.Where(T => T.Tag.Contains(currentText)).Select(T => T.LogicalAddress).First();

                            var ans3 = LadderDrawing.LadderDesign.Active.Elements[J].Elements[_ele - 1];

                            var functionblkadd = isUDFB ? ans3.Attributes.Where(t => (t.Name.StartsWith("input") || t.Name.StartsWith("output")) && t.Value.ToString().Equals(_presentTag))
                                                        .Select(t => t.Value.ToString()) :
                                                        ans3.Attributes.Where(t => (t.Name.StartsWith("input") || t.Name.StartsWith("output")) && t.Value.ToString().Contains(":"))
                                                        .Select(t => t.Value.ToString());

                            var _getEle = functionblkadd.Where(t => t.Replace("~", "").Equals(_presentTag)).Any();
                            if (_getEle == true)
                            {
                                var check2 = functionblkadd.Where(t => t.Replace("~", "").Equals(_presentTag)).Count();
                                while (check2 > 0)
                                {

                                    DataRow data = dr.NewRow();

                                    data["Variable"] = currentText;
                                    data["Location"] = $"{formName.Split('#')[1]} FunctionBlock";
                                    if (!isUDFB)
                                    {
                                        if (currentText.StartsWith("Digital"))
                                            data["Type"] = Tag.Where(d => d.Tag.Contains(currentText)).Select(T => T.Type).First();
                                        else
                                            data["Type"] = Tag.Where(d => d.Tag.Contains(currentText)).Select(T => T.Label).First();
                                        address = Tag.Where(T => T.Tag.Contains(currentText)).Select(D => D.LogicalAddress).First();
                                        data["Address"] = address;
                                    }
                                    else
                                    {
                                        data["Type"] = xm.LoadedProject.UDFBInfo.Where(T => T.UDFBName.Equals(blockCount[i].Name.Replace(" Logic", ""))).FirstOrDefault().UDFBlocks.FirstOrDefault(t => t.Text.Equals(currentText)).Type;
                                        data["Address"] = string.Empty;
                                    }
                                    data["Rung"] = J + 1;
                                    dr.Rows.Add(data);
                                    check2--;
                                }
                            }

                        }
                        //Check and short All Parallel Coil Elements.
                        var paralledCoil = LadderDrawing.LadderDesign.Active.Elements[J].Elements.FirstOrDefault(t => t.CustomType == "LadderDrawing.Coil");
                        Dictionary<int, string> paralledInfo = new Dictionary<int, string>();
                        //Check Tag Present in any Dummy Paralle child Element.
                        foreach (LadderElement dummyParallel in dummyParallelElements)
                        {
                            if (dummyParallel != null && dummyParallel.Elements.Any())
                            {
                                CheckInChildElements(dummyParallel.Elements, currentText, ref check, ref paralledInfo);
                            }
                        }
                        //checking Tag present in any parallel coil element.
                        if (paralledCoil != null && paralledCoil.Elements.Any())
                        {
                            CheckInChildElements(paralledCoil.Elements, currentText, ref check, ref paralledInfo);
                        }
                        var filteredElements = LadderDrawing.LadderDesign.Active.Elements[J].Elements
                                .Where(t => t.CustomType == "LadderDrawing.Contact" || t.CustomType == "LadderDrawing.Coil" || t.CustomType == "LadderDrawing.CoilParallel" /*|| t.CustomType == "LadderDrawing.DummyParallelParent"*/)
                                .Where(t => t.Attributes["caption"].Equals(currentText))
                                .ToList();

                        foreach (LadderElement CoilParallel in filteredElements.ToList())
                        {
                            if (CoilParallel.CustomType == "LadderDrawing.Coil" && CoilParallel.Elements.Any())
                            {
                                AddNestedElements(CoilParallel, filteredElements);
                            }
                        }
                        filteredElements.Insert(0, null);
                        while (check > 0)
                        {
                            k++;
                            DataRow data = dr.NewRow();

                            data["Variable"] = currentText;
                            data["Location"] = formName.Split('#')[1];
                            if (!isUDFB)
                            {
                                if (currentText.StartsWith("Digital"))
                                    data["Type"] = Tag.Where(d => d.Tag.Contains(currentText)).Select(T => T.Type).First();
                                else
                                    data["Type"] = Tag.Where(d => d.Tag.Contains(currentText)).Select(T => T.Label).First();
                                address = Tag.Where(T => T.Tag.Contains(currentText)).Select(D => D.LogicalAddress).First();
                                data["Address"] = address;
                            }
                            else
                            {
                                data["Type"] = xm.LoadedProject.UDFBInfo.Where(T => T.UDFBName.Equals(blockCount[i].Name.Replace(" Logic", ""))).FirstOrDefault().UDFBlocks.FirstOrDefault(t => t.Text.Equals(currentText)).Type;
                                data["Address"] = string.Empty;
                            }
                            data["Rung"] = J + 1;
                            if (paralledInfo.ContainsKey(k))
                            {
                                if (paralledInfo[k] == "NC")
                                {
                                    address = "~" + address;
                                    data["Address"] = address;
                                }
                                else if (paralledInfo[k] == "P" || paralledInfo[k] == "N")
                                {
                                    string _status = paralledInfo[k] == "P" ? "P" : "N";
                                    address = address + " |" + _status + "|";
                                    data["Address"] = address;
                                }
                                else if (paralledInfo[k] == "S" || paralledInfo[k] == "R")
                                {
                                    string status = paralledInfo[k] == "S" ? "S" : "R";
                                    address = address + " (" + status + ")";
                                    data["Address"] = address;
                                }
                            }
                            if (k > 0 && k < filteredElements.Count && !paralledInfo.ContainsKey(k))
                            {
                                var selectedelement = filteredElements[k];
                                if (selectedelement.Negation)
                                {
                                    address = "~" + address;
                                }
                                var statusMappings = new Dictionary<string, string>
                                {
                                   { "P", " |P|" },
                                   { "N", " |N|" },
                                   { "S", " (S)" },
                                   { "R", " (R)" }
                                };
                                foreach (var attributeKey in new[] { "PNStatus", "SetReset" })
                                {
                                    if (selectedelement.Attributes != null && selectedelement.Attributes[attributeKey] != null)
                                    {
                                        string status = selectedelement.Attributes[attributeKey].ToString();
                                        if (statusMappings.ContainsKey(status))
                                        {
                                            address += statusMappings[status];
                                            break;
                                        }
                                    }
                                }
                                data["Address"] = address;
                            }
                            dr.Rows.Add(data);
                            check--;
                        }
                    }
                }
            }
        }
        public void AddNestedElements(LadderElement element, List<LadderElement> elementsList)
        {
            if (element.Elements.Any())
            {
                elementsList.AddRange(element.Elements);
                foreach (var childElement in element.Elements)
                {
                    AddNestedElements(childElement, elementsList);
                }
            }
        }
        private void CheckInChildElements(LadderElements elements, string enteredText, ref int checkCount, ref Dictionary<int, string> paralledInfo)
        {
            foreach (var element in elements)
            {
                if (element.Attributes.Any(attr => attr.Value.Equals(enteredText)))
                {
                    checkCount++;
                    if (!string.IsNullOrEmpty(element.Attributes["PNStatus"].ToString()) && !element.Negation)
                    {
                        paralledInfo.Add(checkCount, element.Attributes["PNStatus"].ToString());
                    }
                    else if (!string.IsNullOrEmpty(element.Attributes["SetReset"].ToString()) && !element.Negation)
                    {
                        paralledInfo.Add(checkCount, element.Attributes["SetReset"].ToString());
                    }
                    else
                    {
                        paralledInfo.Add(checkCount, element.Negation ? "NC" : "NO");
                    }
                }
                if (element.Elements.Any())
                {
                    CheckInChildElements(element.Elements, enteredText, ref checkCount, ref paralledInfo);
                }
            }
        }
        private void CheckInChildElements(LadderElements elements, string enteredText, ref int checkCount)
        {
            foreach (var element in elements)
            {
                if (element.Attributes.Any(attr => attr.Value.Equals(enteredText)))
                {
                    checkCount++;
                }

                if (element.Elements.Any())
                {
                    CheckInChildElements(element.Elements, enteredText, ref checkCount);
                }
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '_')
            {
                e.Handled = true;
            }
        }
    }
}
