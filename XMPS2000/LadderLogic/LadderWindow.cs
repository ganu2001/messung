using LadderDrawing;
using LadderDrawing.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using XMPS2000.Configuration;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.LadderLogic;
using Button = System.Windows.Forms.Button;
using Control = System.Windows.Forms.Control;
using Panel = System.Windows.Forms.Panel;
using TreeView = System.Windows.Forms.TreeView;

namespace XMPS2000.LadderLogic
{
    public partial class LadderWindow : Form, IXMForm
    {
        private string ParentNodeName = "";
        private bool isDirectlyAddedFB = false;

        //adding for resizing the instruction panel.
        private const int resizeBorderWidh = 7;
        private const int MinWidth = 40;
        private const int DefaultWidth = 150;
        private bool isResizing = false;
        private Point lastMouseLocation;
        public int deltaXAxis = 0;
        public int newWidth1 = 0;
        private int initialSplitterDistance = 0;
        private int lastSplitterDistance = 150;
        private bool isSplitterDistanceChanged = false;
        public LadderWindow(string nodeName)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += ladderEditorControl1_KeyDown;
            ladderEditorControl1.ItemDropped += LadderEditorControl1_ItemDropped;
            tslblblockname.Text = nodeName.ToString();
        }
        public LadderEditorControl getLadderEditor()
        {
            return ladderEditorControl1;
        }
        public ToolStrip getLadderEditorToolStrip()
        {
            return tsBlocks;
        }
        private void FunctionBlock_Clicked(LadderElement ladderElement)
        {
            string parent = ladderElement.Name.ToString();
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            List<string> lstInput = new List<string> { };
            lstInput.AddRange(ladderElement.Attributes.Where(t => t.Name.StartsWith("in") && t.Value != null && t.Value.ToString() != "-").Select(t => t.Name).ToList());
            List<string> lstOutput = new List<string> { };
            lstOutput.AddRange(ladderElement.Attributes.Where(t => t.Name.StartsWith("out") && t.Value != null && !t.Value.ToString().Contains('-')).Select(t => t.Name).ToList());
            FunctionBlockInputsAndOutputs userControl = new FunctionBlockInputsAndOutputs(tempForm);
            tempForm.Text = "Add New Function Block";
            userControl.edit = false;
            if (XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName == tslblblockname.Text.ToString().Replace(" Logic", "")).Count() > 0)
                userControl.udfbname = tslblblockname.Text.Replace(" Logic", "");
            else
                userControl.udfbname = "";
            tempForm.Height = userControl.Height + 30;
            tempForm.Width = userControl.Width + 30;
            ApplicationRung AppRecs = new ApplicationRung();
            LadderDesign.ClickedElement = ladderElement;
            int prvinputs = 0, prvOutputs = 1;
            if (LadderDesign.ClickedElement.getRoot().Elements.Count > 2)
            {
                userControl.hasLadders = true;
            }
            else
            {
                userControl.hasLadders = false;
            }
            //Edit 
            if (ladderElement.Attributes["input1"].ToString() != "")
            {
                if (ladderElement.Attributes["TCName"].ToString().StartsWith("FB") || ladderElement.Attributes["OpCode"].ToString() == "9999")
                {
                    UDFBInfo currentUDFB = XMPS.Instance.LoadedProject.UDFBInfo.FirstOrDefault(u => u.UDFBName == ladderElement.Attributes["function_name"].ToString());
                    if (currentUDFB == null)
                    {
                        MessageBox.Show($"UDFB:- {ladderElement.Attributes["function_name"].ToString()} not found in current project", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //checking input and outputs configuration in case performing of undo redo
                    if (currentUDFB.Inputs != lstInput.Count)
                    {
                        if (currentUDFB.Inputs > lstInput.Count)
                        {
                            int nextCount = currentUDFB.Inputs;
                            //add
                            int newAttribute = currentUDFB.Inputs - lstInput.Count;
                            for (int i = 0; i < newAttribute; i++)
                            {
                                ladderElement.Attributes[$"input{nextCount}"] = "-";
                                nextCount++;
                            }
                        }
                        //remove
                        if (currentUDFB.Inputs < lstInput.Count)
                        {
                            int nextCount = lstInput.Count;
                            int newAttribute = lstInput.Count - currentUDFB.Inputs;
                            for (int i = 0; i < newAttribute; i++)
                            {
                                ladderElement.Attributes.RemoveAll(t => t.Name.Equals($"input{nextCount}"));
                                nextCount--;
                            }
                        }
                    }
                    if (currentUDFB.Outputs != lstOutput.Count)
                    {
                        if (currentUDFB.Outputs > lstOutput.Count)
                        {
                            int nextCount = currentUDFB.Outputs;
                            //add
                            int newAttribute = currentUDFB.Outputs - lstOutput.Count;
                            for (int i = 0; i < newAttribute; i++)
                            {
                                ladderElement.Attributes[$"output{nextCount}"] = "-";
                                nextCount++;
                            }
                        }
                        //remove
                        if (currentUDFB.Outputs < lstOutput.Count)
                        {
                            int nextCount = lstOutput.Count;
                            int newAttribute = lstOutput.Count - currentUDFB.Outputs;
                            for (int i = 0; i < newAttribute; i++)
                            {
                                ladderElement.Attributes.RemoveAll(t => t.Name.Equals($"output{nextCount}"));
                                nextCount--;
                            }
                        }
                    }
                    tempForm.StartPosition = FormStartPosition.CenterParent;
                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    tempForm.Text = "Add New Address Added in Logic";
                    FunctionBlockInputsAndOutputs UDFBuserControl;
                    UDFBuserControl = new FunctionBlockInputsAndOutputs(ladderElement);
                    tempForm.Height = UDFBuserControl.Height + 25;
                    tempForm.Width = UDFBuserControl.Width;
                    UDFBuserControl.isEnabled = isDirectlyAddedFB ? false : userControl.hasLadders;
                    tempForm.Controls.Add(UDFBuserControl);
                    var UDFBfrmTemp = this.ParentForm as frmMain;
                    DialogResult udfbresult = tempForm.ShowDialog(UDFBfrmTemp);
                    if (udfbresult == DialogResult.OK)
                    {
                        LadderDesign.ClickedElement = ladderElement;
                        List<ApplicationRung> rungs = XMPS.Instance.LoadedProject.LogicRungs;
                        int fbHeight = 120;
                        ApplicationRung rung = rungs[rungs.Count - 1];

                        AddInputOutputsToladderElement(ref rung, ref ladderElement, ref fbHeight);

                        if (AppRecs.OpCode != rung.OpCode && AppRecs.TC_Name != "-")
                        {
                            if (AppRecs.OpCode != null)
                            {
                                DeleteTCName(Convert.ToInt32(Regex.Match(AppRecs.TC_Name.ToString(), @"\d+").Value), AppRecs.OpCode);
                                XMPS.Instance.LoadedProject.LogicRungs.Remove(XMPS.Instance.LoadedProject.LogicRungs.Where(R => R.TC_Name == AppRecs.TC_Name.ToString() && R.OpCode == AppRecs.OpCode).FirstOrDefault());
                            }
                        }
                        AddingOtherParametersToLadderElement(ref rung, ref ladderElement);

                        ladderElement.Attributes["PreviousInputs"] = prvinputs;
                        ladderElement.Attributes["PreviousOutputs"] = prvOutputs;
                        ladderElement.Position.Height = fbHeight;
                        if (rung.Enable == "-")
                        {
                            List<LadderElement> ElementList = new List<LadderElement>();
                            LadderElement Root = ladderElement.getRoot();
                            foreach (LadderElement OtherElement in Root.Elements)
                            {
                                if (OtherElement.customDrawing.GetType() != typeof(Coil) && ladderElement.Position.Parent == Root && OtherElement != ladderElement)
                                {
                                    ElementList.Add(OtherElement);
                                }
                            }
                            for (int i = 0; i < ElementList.Count; i++)
                            {
                                if (ElementList[i].customDrawing.toString() != "Comment")
                                {
                                    ElementList[i].getRoot().Elements.Remove(ElementList[i]);
                                }
                            }
                        }
                    }
                    RefreshLadderEditor();
                    RefreshLadderEditor();
                }
                else
                {
                    string instructionName = ladderElement.Attributes["function_name"].ToString().StartsWith("MES_PID") ? "MES_PID" : ladderElement.Attributes["function_name"].ToString();
                    if (XMPS.Instance.instructionsList == null || XMPS.Instance.instructionsList.Count == 0 || !XMPS.Instance.instructionsList.Any(t => t.Text.Equals(instructionName)))
                    {
                        MessageBox.Show("Please reload instruction file once", "XMPS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    AddingDataToApplicationRung(ref AppRecs, ref ladderElement);

                    userControl.tc_instuction = ladderElement.Attributes["TCName"].ToString();
                    userControl.sel_opcode = ladderElement.Attributes["OpCode"].ToString();
                    userControl.edit = true;
                    userControl.isEnabled = true;
                    if (LadderDesign.ClickedElement.getRoot().Elements.Count > 0)
                    {
                        userControl._curRungTcName = ladderElement.Attributes["OpCode"].ToString().StartsWith("040E")
                                                     ? ladderElement.Attributes["function_name"].ToString()
                                                     : ladderElement.Attributes["TCName"].ToString();
                    }
                    userControl.SetValues(ladderElement);
                }
            }
            //New
            if (!ladderElement.Attributes["TCName"].ToString().StartsWith("FB"))
            {
                if (this.ParentNodeName == "UDFB" || XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName == ladderElement.Attributes["function_name"].ToString()).Any())
                {
                    tempForm.StartPosition = FormStartPosition.CenterParent;
                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    tempForm.Text = "Add New Address Added in Logic";
                    FunctionBlockInputsAndOutputs UDFBuserControl;
                    UDFBuserControl = new FunctionBlockInputsAndOutputs(ladderElement);
                    tempForm.Height = UDFBuserControl.Height + 25;
                    tempForm.Width = UDFBuserControl.Width;
                    tempForm.Controls.Add(UDFBuserControl);
                    UDFBuserControl.isEnabled = isDirectlyAddedFB ? false : userControl.hasLadders;

                    var UDFBfrmTemp = this.ParentForm as frmMain;
                    DialogResult udfbresult = tempForm.ShowDialog(UDFBfrmTemp);
                    if (udfbresult == DialogResult.OK)
                    {
                        LadderDesign.ClickedElement = ladderElement;
                        List<ApplicationRung> rungs = XMPS.Instance.LoadedProject.LogicRungs;
                        int fbHeight = 120;
                        ApplicationRung rung = rungs[rungs.Count - 1];
                        AddInputOutputsToladderElement(ref rung, ref ladderElement, ref fbHeight);

                        AddingOtherParametersToLadderElement(ref rung, ref ladderElement);

                        if (AppRecs.OpCode != rung.OpCode && AppRecs.TC_Name != "-")
                        {
                            if (AppRecs.OpCode != null)
                            {
                                DeleteTCName(Convert.ToInt32(Regex.Match(AppRecs.TC_Name.ToString(), @"\d+").Value), AppRecs.OpCode);
                                XMPS.Instance.LoadedProject.LogicRungs.Remove(XMPS.Instance.LoadedProject.LogicRungs.Where(R => R.TC_Name == AppRecs.TC_Name.ToString() && R.OpCode == AppRecs.OpCode).FirstOrDefault());
                            }
                        }

                        ladderElement.Attributes["PreviousInputs"] = prvinputs;
                        ladderElement.Attributes["PreviousOutputs"] = prvOutputs;
                        ladderElement.Position.Height = fbHeight;
                        if (rung.Enable == "-")
                        {
                            List<LadderElement> ElementList = new List<LadderElement>();
                            LadderElement Root = ladderElement.getRoot();
                            foreach (LadderElement OtherElement in Root.Elements)
                            {
                                if (OtherElement.customDrawing.GetType() != typeof(Coil) && ladderElement.Position.Parent == Root && OtherElement != ladderElement)
                                {
                                    ElementList.Add(OtherElement);
                                }
                            }
                            for (int i = 0; i < ElementList.Count; i++)
                            {
                                if (ElementList[i].customDrawing.toString() != "Comment")
                                {
                                    ElementList[i].getRoot().Elements.Remove(ElementList[i]);
                                }
                            }
                        }
                    }
                    RefreshLadderEditor();
                }
                else
                {
                    userControl.SetValues(ladderElement);
                    userControl.isEnabled = isDirectlyAddedFB ? false : userControl.hasLadders;
                    tempForm.Controls.Add(userControl);
                    var frmTemp = this.ParentForm as frmMain;
                    DialogResult result = tempForm.ShowDialog(frmTemp);
                    if (result == DialogResult.OK)
                    {
                        List<ApplicationRung> rungs = XMPS.Instance.LoadedProject.LogicRungs;
                        if (rungs.Count > 0)
                        {
                            int fbHeight = 120;
                            ApplicationRung rung = rungs[rungs.Count - 1];
                            LadderDesign.ClickedElement = ladderElement;
                            ladderElement.Attributes["function_name"] = rung.OpCodeNm;
                            if (rung.Inputs["Input1"] != null || rung.Outputs["Output1"] != null)
                            {
                                ladderElement.Attributes.RemoveAll(t => t.Name.StartsWith("input"));
                                AddInputOutputsToladderElement(ref rung, ref ladderElement, ref fbHeight);
                            }

                            AddingOtherParametersToLadderElement(ref rung, ref ladderElement);
                            if (AppRecs.OpCode != rung.OpCode && AppRecs.TC_Name != "-")
                            {
                                if (ladderElement.Attributes["function_name"].ToString().Equals("Pack") ||
                                    ladderElement.Attributes["function_name"].ToString().Equals("UnPack") ||
                                    ladderElement.Attributes["function_name"].ToString().Equals("MQTT Publish") ||
                                    ladderElement.Attributes["function_name"].ToString().Equals("MQTT Subscribe"))
                                {
                                    if (AppRecs.OpCode != null)
                                    {
                                        DeleteTCName(Convert.ToInt32(Regex.Match(AppRecs.TC_Name.ToString(), @"\d+").Value), AppRecs.OpCode);
                                        XMPS.Instance.LoadedProject.LogicRungs.Remove(XMPS.Instance.LoadedProject.LogicRungs.Where(R => R.TC_Name == AppRecs.TC_Name.ToString() && R.OpCode == AppRecs.OpCode).FirstOrDefault());
                                    }
                                }

                            }
                            ladderElement.Attributes["PreviousInputs"] = prvinputs;
                            ladderElement.Attributes["PreviousOutputs"] = prvOutputs;
                            ladderElement.Position.Height = fbHeight;
                            if (rung.Enable == "-")
                            {
                                List<LadderElement> ElementList = new List<LadderElement>();
                                LadderElement Root = ladderElement.getRoot();
                                foreach (LadderElement OtherElement in Root.Elements)
                                {
                                    if (OtherElement.customDrawing.GetType() != typeof(Coil) && ladderElement.Position.Parent == Root && OtherElement != ladderElement)
                                    {
                                        ElementList.Add(OtherElement);
                                    }
                                }
                                for (int i = 0; i < ElementList.Count; i++)
                                {
                                    if (ElementList[i].customDrawing.toString() != "Comment")
                                    {
                                        ElementList[i].getRoot().Elements.Remove(ElementList[i]);
                                    }
                                }
                            }
                            RefreshLadderEditor();
                            //caling save method of frmMain form for, saving the edited rungs of UDFB's logic blocks
                            if (XMPS.Instance.CurrentScreen.StartsWith("UDFLadderForm"))
                            {
                                ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain")).save(false);
                            }
                        }
                    }
                    else
                    {
                        LadderDesign.ClickedElement = null;
                    }
                }

                getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
            }
        }

        private void AddingDataToApplicationRung(ref ApplicationRung AppRecs, ref LadderElement ladderElement)
        {
            AppRecs.OpCode = ladderElement.Attributes["OpCode"].ToString();
            AppRecs.TC_Name = ladderElement.Attributes["TCName"].ToString();
            AppRecs.OpCodeNm = ladderElement.Attributes["function_name"].ToString();
        }
        private void RefreshLadderEditor()
        {
            ladderEditorControl1.Invalidate();
            ladderEditorControl1.ReScale();
            ladderEditorControl1.Update();
        }
        private void AddingOtherParametersToLadderElement(ref ApplicationRung rung, ref LadderElement ladderElement)
        {
            ladderElement.Attributes["DataType"] = rung.DataType;
            ladderElement.Attributes["DataType_Nm"] = rung.DataType_Nm;
            ladderElement.Attributes["OutputType"] = rung.OutputType;
            ladderElement.Attributes["OutPutType_NM"] = rung.OutPutType_NM;
            ladderElement.Attributes["OpCode"] = rung.OpCode;
            ladderElement.Attributes["function_name"] = rung.OpCodeNm;
            ladderElement.Attributes["name"] = rung.Name;
            ladderElement.Attributes["enable"] = rung.Enable;
            ladderElement.Attributes["TCName"] = rung.TC_Name;
            ladderElement.Attributes["Linenumber"] = rung.LineNumber;
        }

        private void AddInputOutputsToladderElement(ref ApplicationRung rung, ref LadderElement ladderElement, ref int fbHeight)
        {
            // Set inputs
            ladderElement.Attributes.RemoveAll(a => a.Name.StartsWith("input") || a.Name.StartsWith("output"));
            for (int i = 1; i <= rung.Inputs.Count; i++)
            {
                string inputKey = $"Input{i}";
                string inputValue = rung.Inputs[inputKey].ToString();
                ladderElement.Attributes[$"input{i}"] = inputValue;
                if (inputValue == "-")
                {
                    fbHeight -= 25;
                }
            }
            // Set outputs
            for (int i = 1; i <= rung.Outputs.Count; i++)
            {
                string outputKey = $"Output{i}";
                ladderElement.Attributes[$"output{i}"] = rung.Outputs[outputKey];
            }
        }

        private void DeleteTCName(int tccount, string opCode)
        {
            ApplicationRung AppRecs = new ApplicationRung();
            var filteredBlocks = XMPS.Instance.LoadedProject.Blocks.Where(b => b.Type.Equals("LogicBlock") || b.Type.Equals("UDFB"));
            foreach (var _block in filteredBlocks)
            {
                string screenKey = _block.Type.Equals("LogicBlock") ? $"LadderForm#{_block.Name}" : $"UDFLadderForm#{_block.Name}";
                if (XMPS.Instance.LoadedScreens.TryGetValue(screenKey, out var screen))
                {
                    LadderWindow _windowRef = (LadderWindow)screen;
                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
                    foreach (LadderElement rungElements in LadderDrawing.LadderDesign.Active.Elements)
                    {
                        string attributeName = opCode == "040E" ? "function_name" : "TcName";
                        string functionName = opCode == "040E" ? "MES_PID_" : "";
                        LadderElement fbelement = new LadderElement();
                        if (opCode != "040E")
                        {
                            //Checking for other Instruction.
                            fbelement = (LadderElement)rungElements.Elements.Where(E => E.CustomType.ToString() == "LadderDrawing.FunctionBlock" && E.Attributes["OpCode"].Equals(opCode)
                                       && Convert.ToInt32(Regex.Replace(E.Attributes["TCName"].ToString(), @"\D", "")) > tccount).FirstOrDefault();
                        }
                        else
                        {
                            //Checking for the PID Instruction.
                            fbelement = (LadderElement)rungElements.Elements.Where(E => E.CustomType.ToString() == "LadderDrawing.FunctionBlock" && E.Attributes["OpCode"].Equals(opCode)
                                        && Convert.ToInt32(E.Attributes["function_name"].ToString().Replace("MES_PID_", "")) > tccount).FirstOrDefault();
                        }

                        if (fbelement != null)
                        {
                            string oldName = fbelement.Attributes[attributeName].ToString();
                            int result = Convert.ToInt32(Regex.Match(fbelement.Attributes[attributeName].ToString(), @"\d+").Value);
                            fbelement.Attributes[attributeName] = oldName.Replace(result.ToString(), (result - 1).ToString());

                            AppRecs = XMPS.Instance.LoadedProject.LogicRungs
                                .Where(R => functionName == "MES_PID_" ? R.OpCodeNm == oldName : R.TC_Name == oldName && R.OpCode.Equals(fbelement.Attributes["OpCode"]))
                                .FirstOrDefault();

                            if (AppRecs != null && !AppRecs.OpCode.Equals("040E"))
                                AppRecs.TC_Name = fbelement.Attributes[attributeName].ToString();

                            if (AppRecs != null && AppRecs.OpCode.Equals("040E"))
                                AppRecs.OpCodeNm = fbelement.Attributes[attributeName].ToString();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Get Last element of the Rung which is not Coil
        /// </summary>
        /// <param name="element"></param> Send the element of which root has to be searched
        /// <returns></returns> Return X of last element + width of that element
        private void LadderEditorControl1_ItemDropped(object sender, DragEventArgs e)
        {
            /*
            LadderElement ladderElement = (LadderElement)sender;
            string parent = "";
            string nodeinfo = e.Data.GetData(DataFormats.Text).ToString();
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //(*Multiply *)
            ApplicationUserControl userControl = new ApplicationUserControl(tempForm);
            userControl.SelectedInstructionType = parent.ToString().Replace("TreeNode: ", "");
            userControl.SelectedInstruction = nodeinfo;
            tempForm.Text = "Add New Rung For " + parent.ToString().Replace("TreeNode: ", "") + " With Instruction " + nodeinfo;
            userControl.edit = false;
            tempForm.Height = userControl.Height + 30;
            tempForm.Width = userControl.Width + 30;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK)
            {
                List<ApplicationRung> rungs = XMPS.Instance.LoadedProject.LogicRungs;
                if (rungs.Count > 0)
                {
                    ApplicationRung rung = rungs[rungs.Count - 1];
                    LadderDesign.ClickedElement = ladderElement;
                    LadderElement newelement = ladderEditorControl1.InsertFBAfter();
                    newelement.Attributes["function_name"] = nodeinfo;
                    newelement.Attributes["input1"] = rung.Outputs["Input1"];
                    newelement.Attributes["input2"] = rung.Outputs["Input2"];
                    newelement.Attributes["input3"] = rung.Outputs["Input3"];
                    newelement.Attributes["input4"] = rung.Outputs["Input4"];
                    newelement.Attributes["input5"] = rung.Outputs["Input5"];
                    newelement.Attributes["name"] = rung.Name;
                    newelement.Attributes["enable"] = rung.Enable;
                    newelement.Attributes["output1"] = rung.Outputs["Output1"];
                    newelement.Attributes["output2"] = rung.Outputs["Output2"];
                    newelement.Attributes["output3"] = rung.Outputs["Output3"];
                    ladderEditorControl1.Invalidate();
                }
            }
            */
        }
        public void OnShown()
        {
            ShowInstructionLabel();
            //adding for renaming the tslbblockname after renaming the logic block name.
            if (tslblblockname.Text != XMPS.Instance.CurrentScreen.Split('#')[1].ToString())
                tslblblockname.Text = XMPS.Instance.CurrentScreen.Split('#')[1].ToString();
        }
        private void ladderEditorControl1_TagValidation(object sender, CancelEventArgs e)
        {
            string var = sender.ToString();
            if (var != "")                                                  //if Tag is Word then proceed further
            {
                ladderEditorControl1.ValidText = CheckandAddtag(sender.ToString(), out string ans);
            }
        }
        private void ladderEditorControl1_ItemDeleted(object sender, KeyEventArgs e)
        {
            LadderElement elementTobeDeleted = (LadderElement)sender;
            if (elementTobeDeleted == null) return;
            DeleteItem(elementTobeDeleted);
        }
        public void DeleteItem(LadderElement elementTobeDeleted)
        {
            if (elementTobeDeleted.customDrawing.toString() == "FunctionBlock")
            {
                if (elementTobeDeleted.Attributes["TCName"].ToString() != "-" && elementTobeDeleted.Attributes["TCName"].ToString() != ""
                    || elementTobeDeleted.Attributes["function_name"].ToString().StartsWith("MES_PID_"))
                {
                    DeleteTCCount(elementTobeDeleted);
                }
            }
            if (elementTobeDeleted.customDrawing.toString() == "Rung")
            {
                int countOfRetentiveAdd = 0;
                string retentiveAdd = "";
                string Output2 = "";
                XMIOConfig OutputTag = new XMIOConfig();
                foreach (LadderElement childelement in elementTobeDeleted.getRoot().Elements)
                {
                    if (childelement.customDrawing.toString() == "FunctionBlock")
                    {
                        if ((childelement.Attributes["TCName"].ToString() != "-" && childelement.Attributes["TCName"].ToString() != "")
                            || childelement.Attributes["function_name"].ToString().StartsWith("MES_PID_"))
                        {
                            if (childelement.Attributes["function_name"].ToString().Contains("RTON"))
                            {
                                Output2 = childelement.Attributes["output2"].ToString();
                                OutputTag = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output2);
                                if (OutputTag != null)
                                {
                                    countOfRetentiveAdd = countOfRetentiveAdd + 1;
                                }
                                if (OutputTag.Retentive)
                                {
                                    retentiveAdd = OutputTag.RetentiveAddress.ToString();
                                }

                                DeleteTCCount(childelement);
                            }
                            else
                            {
                                var isthere1 = XMPS.Instance.LoadedProject.LogicRungs.ToList();
                                foreach (var i in isthere1)
                                {
                                    if (i.DataType_Nm == "RTON")
                                    {
                                        var output2 = childelement.Attributes["output2"].ToString();
                                        XMIOConfig outputTags = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output2);
                                        if (outputTags != null)
                                        {
                                            countOfRetentiveAdd = countOfRetentiveAdd + 1;
                                        }
                                    }
                                }
                                if (countOfRetentiveAdd == 1)
                                {
                                    OutputTag.Retentive = false;
                                    OutputTag.RetentiveAddress = null;
                                }
                                DeleteTCCount(childelement);
                            }
                        }
                        var isthere = XMPS.Instance.LoadedProject.LogicRungs.ToList();
                        foreach (var i in isthere)
                        {
                            if (i.DataType_Nm == "RTON")
                            {
                                var output2 = childelement.Attributes["output2"].ToString();
                                XMIOConfig outputTags = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output2);
                                if (outputTags != null)
                                {
                                    countOfRetentiveAdd = countOfRetentiveAdd + 1;
                                }
                            }
                        }
                        if (countOfRetentiveAdd == 1)
                        {
                            OutputTag.Retentive = false;
                            OutputTag.RetentiveAddress = null;
                        }
                    }
                }
            }
        }
        private void DeleteTCCount(LadderElement elementTobeDeleted)
        {
            if (elementTobeDeleted.Attributes["function_name"].ToString().Equals("Pack") ||
               elementTobeDeleted.Attributes["function_name"].ToString().Equals("UnPack") ||
               elementTobeDeleted.Attributes["function_name"].ToString().Equals("MQTT Publish") ||
               elementTobeDeleted.Attributes["function_name"].ToString().Equals("MQTT Subscribe") ||
               elementTobeDeleted.Attributes["OpCode"].ToString().Equals("9999"))
            {
                //Added Check with Function Name for Deleting the UDFB Block
                List<ApplicationRung> AppRecs = XMPS.Instance.LoadedProject.LogicRungs.Where(R => R.TC_Name == elementTobeDeleted.Attributes["TcName"].ToString() && R.OpCodeNm == elementTobeDeleted.Attributes["function_name"].ToString()).ToList();
                foreach (ApplicationRung aprung in AppRecs)
                    XMPS.Instance.LoadedProject.LogicRungs.Remove(aprung);
                string tcName = elementTobeDeleted.Attributes["TcName"].ToString();
                string opCode = elementTobeDeleted.Attributes["OpCode"].ToString();
                string functionm = elementTobeDeleted.Attributes["function_name"].ToString();
                int tccount;
                if (!tcName.Contains("PK") && !functionm.StartsWith("MES_PID_"))
                {

                    tccount = Convert.ToInt32(Regex.Replace(tcName, @"\D", ""));
                    DeleteTCName(tccount, opCode);
                }
                else if (functionm.StartsWith("MES_PID_"))
                {
                    int.TryParse(functionm.Split('_')[2], out tccount);
                    DeleteTCName(tccount, opCode);
                }
                else
                {
                    XMPS.Instance.LoadedProject.Tags.RemoveAll(T => T.ActualName != null && T.ActualName.StartsWith(tcName));
                }
            }
        }
        private void ladderEditorControl1_ItemClicked(object sender, MouseEventArgs e)
        {
            if (XMPS.Instance.PlcStatus == "LogIn" && sender != null && (XMPS.Instance.LoadedProject.MainLadderLogic.Where(b => !b.StartsWith("'") && b.Contains(XMPS.Instance.CurrentScreen.Split('#')[1])).Count() > 0))
            {
                if (((LadderElement)sender).customDrawing.toString() != "HorizontalLine" && ((LadderElement)sender).customDrawing.toString() != "Comment" && !((LadderElement)sender).Attributes.Where(n => n.Name == "isCommented").Any())
                {
                    if (XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == ((LadderElement)sender).Attributes["LogicalAddress"].ToString()).Select(t => t.ReadOnly).FirstOrDefault())
                        MessageBox.Show("Read only elements can't be forced", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        ForceFunctionality_Clicked((LadderElement)sender);
                }
            }
            else if (XMPS.Instance.PlcStatus != "LogIn")
            {
                if (sender is LadderElement ladderElement &&
                  !ladderElement.Attributes.Any(n => n.Name == "isCommented") &&
                   ladderElement.customDrawing.toString() != "HorizontalLine" &&
                   ladderElement.customDrawing.toString() != "Comment")
                {
                    var frmTemp = this.ParentForm as frmMain;
                    string selectedNode = frmTemp.tvProjects.SelectedNode.Text;
                    string normalizedSelectedNode = selectedNode.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                        ? selectedNode.Substring(0, selectedNode.Length - 6).Trim()
                        : selectedNode;
                    string currentScreenName = XMPS.Instance.CurrentScreen ?? string.Empty;
                    if (currentScreenName.Contains("UDFLadderForm"))
                    {
                        string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string libraryRoot = Path.Combine(basePath, "MessungSystems", "XMPS2000", "Library");
                        string modelFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD") ? "XBLDLibraries" : "XMLibraries";
                        string libraryPath = Path.Combine(libraryRoot, modelFolder);
                        List<string> udfbNames = XMPS.Instance.LoadedProject.UDFBInfo.Select(u => u.UDFBName).ToList();

                        var fileNames = Directory.Exists(libraryPath) ? Directory.GetFiles(libraryPath, "*.csv")
                            .Select(Path.GetFileNameWithoutExtension)
                            .Select(name => name.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                                    ? name.Substring(0, name.Length - 6).Trim()
                                    : name)
                            : Enumerable.Empty<string>();

                        bool isUdfbMatch = fileNames.Any(fileName => fileName.Equals(normalizedSelectedNode, StringComparison.OrdinalIgnoreCase) &&
                            udfbNames.Any(udfbName => udfbName.Equals(normalizedSelectedNode, StringComparison.OrdinalIgnoreCase)));

                        if (isUdfbMatch)
                        {
                            string savedChoice = XMPS.Instance.LoadedProject.GetUDFBChoice(normalizedSelectedNode);
                            string savedLocalCopyName = null;
                            if (!string.IsNullOrEmpty(savedChoice) && savedChoice.StartsWith("CreateLocalCopy:"))
                            {
                                savedLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);
                            }

                            bool localCopyWithDifferentNameExists = !string.IsNullOrEmpty(savedLocalCopyName) && !savedLocalCopyName.Equals(normalizedSelectedNode, StringComparison.OrdinalIgnoreCase) &&
                                XMPS.Instance.LoadedProject.Blocks.Any(b => b.Type == "UserFunctionBlock" && b.Name.Equals(savedLocalCopyName, StringComparison.OrdinalIgnoreCase));

                            // If library UDFB exists AND a local copy exists, it means library was re-imported
                            // In this case, ignore saved choice and show popup
                            if (localCopyWithDifferentNameExists)
                            {
                                using (var optionsForm = new UDFBEditOptionsForm(normalizedSelectedNode))
                                {
                                    if (optionsForm.ShowDialog() == DialogResult.OK)
                                    {
                                        if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                        {
                                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "EditMainFile");
                                            if (ladderElement.customDrawing.GetType() == typeof(LadderDrawing.FunctionBlock))
                                                FunctionBlock_Clicked(ladderElement);
                                            else
                                                TagWindow_Clicked(ladderElement);
                                        }
                                        else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                        {
                                            string newLocalCopyName = optionsForm.LocalCopyName;
                                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "CreateLocalCopy:" + newLocalCopyName);
                                            frmTemp.CreateAndEditLocalCopy(ladderElement, normalizedSelectedNode, newLocalCopyName);
                                        }
                                    }
                                }
                                return;
                            }

                            if (!string.IsNullOrEmpty(savedChoice))
                            {
                                if (savedChoice == "EditMainFile")
                                {
                                    if (ladderElement.customDrawing.GetType() == typeof(LadderDrawing.FunctionBlock))
                                        FunctionBlock_Clicked(ladderElement);
                                    else
                                        TagWindow_Clicked(ladderElement);
                                }
                                else if (savedChoice.StartsWith("CreateLocalCopy:"))
                                {
                                    string existingLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);
                                    bool specificLocalCopyExists = XMPS.Instance.LoadedProject.Blocks.Any(b =>
                                        b.Type == "UserFunctionBlock" && b.Name.Equals(existingLocalCopyName, StringComparison.OrdinalIgnoreCase));

                                    if (specificLocalCopyExists)
                                    {
                                        if (ladderElement.customDrawing.GetType() == typeof(LadderDrawing.FunctionBlock))
                                            FunctionBlock_Clicked(ladderElement);
                                        else
                                            TagWindow_Clicked(ladderElement);
                                    }
                                    else
                                    {
                                        XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "");

                                        using (var optionsForm = new UDFBEditOptionsForm(normalizedSelectedNode))
                                        {
                                            if (optionsForm.ShowDialog() == DialogResult.OK)
                                            {
                                                if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                                {
                                                    XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "EditMainFile");
                                                    if (ladderElement.customDrawing.GetType() == typeof(LadderDrawing.FunctionBlock))
                                                        FunctionBlock_Clicked(ladderElement);
                                                    else
                                                        TagWindow_Clicked(ladderElement);
                                                }
                                                else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                                {
                                                    string recreatedLocalCopyName = optionsForm.LocalCopyName;
                                                    XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "CreateLocalCopy:" + recreatedLocalCopyName);
                                                    frmTemp.CreateAndEditLocalCopy(ladderElement, normalizedSelectedNode, recreatedLocalCopyName);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (var optionsForm = new UDFBEditOptionsForm(normalizedSelectedNode))
                                {
                                    if (optionsForm.ShowDialog() == DialogResult.OK)
                                    {
                                        if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                        {
                                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "EditMainFile");
                                            if (ladderElement.customDrawing.GetType() == typeof(LadderDrawing.FunctionBlock))
                                                FunctionBlock_Clicked(ladderElement);
                                            else
                                                TagWindow_Clicked(ladderElement);
                                        }
                                        else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                        {
                                            string initialLocalCopyName = optionsForm.LocalCopyName;
                                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "CreateLocalCopy:" + initialLocalCopyName);
                                            frmTemp.CreateAndEditLocalCopy(ladderElement, normalizedSelectedNode, initialLocalCopyName);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // For UDFLadderForm but no library match - execute normal logic
                            if (ladderElement.customDrawing.GetType() == typeof(LadderDrawing.FunctionBlock))
                                FunctionBlock_Clicked(ladderElement);
                            else
                                TagWindow_Clicked(ladderElement);

                            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
                        }
                    }
                    else
                    {
                        // For non-UDFLadderForm screens - execute normal logic without library validation
                        if (ladderElement.customDrawing.GetType() == typeof(LadderDrawing.FunctionBlock))
                            FunctionBlock_Clicked(ladderElement);
                        else
                            TagWindow_Clicked(ladderElement);

                        getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
                    }
                }
                else if (((LadderElement)sender).customDrawing.toString() == "Comment" &&
                         !((LadderElement)sender).Attributes.Any(n => n.Name == "isCommented"))
                {
                    CommentBox_Clicked((LadderElement)sender);
                }
            }
        }
        private void ForceFunctionality_Clicked(LadderElement selectedElement)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            if (selectedElement.customDrawing.toString() == "Contact" || selectedElement.customDrawing.toString() == "Coil" || selectedElement.customDrawing.toString() == "Parallel Coil")
            {
                ForceBitValue tempForceControl = new ForceBitValue(selectedElement.Attributes["LogicalAddress"].ToString());
                tempForm.Height = tempForceControl.Height + tempForm.DesktopBounds.Height - tempForm.DisplayRectangle.Height;
                tempForm.Width = tempForceControl.Width + tempForm.DesktopBounds.Width - tempForm.DisplayRectangle.Width;
                tempForm.Controls.Add(tempForceControl);
            }
            else
            {
                ForceFunctionBlock tempForceControl = new ForceFunctionBlock(selectedElement);
                tempForm.Height = tempForceControl.maxheight;
                tempForm.Width = tempForceControl.maxWidth;
                tempForm.Controls.Add(tempForceControl);
            }
            tempForm.Text = "Force functionality";
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK || result == DialogResult.Cancel)
            {
                OnlineMonitoringHelper.HoldOnlineMonitor = false;
                ((frmMain)FindParentSplitContainer(this).ParentForm).ShowOnlineMonitor();
            }
        }
        public void refreshAfterForce()
        {
            OnlineMonitoringHelper.HoldOnlineMonitor = false;
            ((frmMain)FindParentSplitContainer(this).ParentForm).ShowOnlineMonitor();
        }
        private void TagWindow_Clicked(LadderElement selectedElement)
        {
            string parent = selectedElement.Name.ToString();
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            AutoComplete userControl = new AutoComplete();
            tempForm.Text = "Select or add tag";
            tempForm.Height = userControl.Height + 50;
            tempForm.Width = userControl.Width + 30;
            tempForm.Location = new Point(LadderDesign.ClickedElement.getX() + 20, MousePosition.Y - 60);
            LadderDesign.ClickedElement = selectedElement;
            tempForm.Controls.Add(userControl);
            if (selectedElement.Attributes["Caption"].ToString() != "FunctionBlock" && selectedElement.customDrawing.toString() != "HorizontalLine" && selectedElement.customDrawing.toString() != "comment")
            {
                List<string> list = new List<string>();
                //adding check for showing only UserDefined Tags in Logical Block not shows UDFB tags
                UDFBInfo udfbios = (UDFBInfo)XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName == tslblblockname.Text.ToString().Replace(" Logic", "")).FirstOrDefault();
                string modelName = "User Defined Tags";
                if (udfbios != null)
                {
                    modelName = tslblblockname.Text.ToString().Replace(" Logic", "") + " Tags";
                }
                if (selectedElement.CustomType == "LadderDrawing.Coil" || selectedElement.CustomType == "LadderDrawing.CoilParallel")
                {
                    list = XMPS.Instance.LoadedProject.Tags.Where(T => (T.LogicalAddress.StartsWith("F") || T.LogicalAddress.Contains(".") && !T.Type.ToString().Contains("Input")) && (T.Model == modelName || XMPS.Instance.PlcModels.Contains(T.Model.Split('_')[0]) || T.Model is null)).Select(T => T.Tag).ToList();
                    list.AddRange(XMPS.Instance.LoadedProject.Tags.Where(T => T.IoList == Core.Types.IOListType.Default && T.Label == "Bool" && ! T.ReadOnly  ).Select(T => T.Tag).ToList());
                    list.AddRange(XMPS.Instance.LoadedProject.Tags.Where(T => !T.LogicalAddress.Contains(".") && T.Mode == "Digital" && !T.Type.ToString().Contains("Input")).Select(T => T.Tag).ToList());

                }
                else
                {
                    list = XMPS.Instance.LoadedProject.Tags.Where(T => (T.LogicalAddress.StartsWith("F") || T.LogicalAddress.Contains(".")) && (T.Model == modelName || XMPS.Instance.PlcModels.Contains(T.Model.Split('_')[0]) || T.Model is null)).Select(T => T.Tag).ToList();
                    list.AddRange(XMPS.Instance.LoadedProject.Tags.Where(T => T.IoList == Core.Types.IOListType.Default && T.Label == "Bool").Select(T => T.Tag).ToList());
                    list.AddRange(XMPS.Instance.LoadedProject.Tags.Where(T => ! T.LogicalAddress.Contains(".") && T.Mode == "Digital").Select(T => T.Tag).ToList());

                }
                if (udfbios != null)
                {
                    if (udfbios.Inputs > 0)
                    {
                        list.AddRange(udfbios.UDFBlocks.Where(t => t.DataType == "Bool").Select(t => t.Text).ToList());
                    }
                }
                if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
                {
                    var filteredList = list
                    .Where(tag => !(tag != null && (tag.EndsWith("_OR", StringComparison.OrdinalIgnoreCase) || tag.EndsWith("_OL", StringComparison.OrdinalIgnoreCase))))
                    .ToList();

                    for (int x = 0; x < filteredList.Count; x++)
                    {
                        userControl.AddListItem(x.ToString(), filteredList[x].Trim());
                    }
                }
                else
                {
                    for (int x = 0; x < list.Count; x++)
                    {
                        userControl.AddListItem(x.ToString(), list[x].Trim());
                    }
                }                 
            }
            var frmTemp = this.ParentForm as frmMain;
            userControl.SetText(selectedElement.Attributes["Caption"].ToString());
            if (selectedElement.customDrawing.toString() != "Comment")
            {
                DialogResult result = tempForm.ShowDialog(frmTemp);
                if (result == DialogResult.OK)
                {
                    if (userControl.EnteredText.ToString() == "") return;
                    if (CheckandAddtag(userControl.EnteredText.ToString(), out string GetAns))
                    {
                        selectedElement.Attributes["Caption"] = GetAns;
                        selectedElement.Attributes.RemoveAll(t => t.Name.Equals("LogicalAddress"));
                        LadderDrawing.Attribute attribute = new LadderDrawing.Attribute();
                        attribute.Name = "LogicalAddress";
                        selectedElement.Attributes.Add(attribute);
                        selectedElement.Attributes["LogicalAddress"] = XMProValidator.GetTheAddressFromTag(GetAns);
                    }
                    ladderEditorControl1.ReScale();
                }
            }
        }
        private void tsbInsertRung_Click(object sender, EventArgs e)
        {
            ladderEditorControl1.InsertRung();
        }
        private void tsbInsertContactParallal_Click(object sender, EventArgs e)
        {
            ladderEditorControl1.InsertContactParallel();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        private void tsbInsertContactBefore_Click_1(object sender, EventArgs e)
        {
            ladderEditorControl1.InsertContactBefore();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        private void tsbSwapItemStyle_Click(object sender, EventArgs e)
        {
            if (!ValidateUDFBEditPermission())
                return;
            ladderEditorControl1.NegateStyle();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        private void tsbInsertContactAfter_Click(object sender, EventArgs e)
        {
            ladderEditorControl1.InsertContactAfter();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        private void tsbInsertFBBefore_Click(object sender, EventArgs e)
        {
            ladderEditorControl1.InsertFBBefore();
        }

        private void tsbInsertCoil_Click(object sender, EventArgs e)
        {
            ladderEditorControl1.InsertCoil();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (XMPS.Instance.instructionsList == null || XMPS.Instance.instructionsList.Count == 0)
            {
                MessageBox.Show("Please reload instruction file once", "XMPS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ladderEditorControl1.InsertFBAfter();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        private bool CheckandAddtag(string Tag, out string Ans)
        {
            var tag = XMPS.Instance.LoadedProject.Tags.Where(d => d.Tag == Tag).FirstOrDefault();
            if (tag == null)
            {
                string screenName = XMPS.Instance.CurrentScreen.ToString();
                UDFBInfo udfbios = (UDFBInfo)XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName == tslblblockname.Text.ToString().Replace(" Logic", "")).FirstOrDefault();
                var chkios = udfbios != null ? udfbios.UDFBlocks.Where(i => i.Text == Tag).ToList() : null;
                if (chkios == null || chkios.Count <= 0)
                {
                    XMProForm tempForm = new XMProForm();
                    tempForm.StartPosition = FormStartPosition.CenterParent;
                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    tempForm.Text = "Add New Address Added in Logic";
                    TagsUserControl userControl;
                    if (screenName.Contains("UDF"))
                    {
                        string[] splitResult = screenName.Split(new string[] { "#" }, StringSplitOptions.None);
                        string udfbName = splitResult[1];
                        string actualUdfbName = udfbName.Replace("Logic", "Tags");
                        userControl = new TagsUserControl(Tag.ToString(), udfbName);
                    }
                    else
                    {
                        userControl = new TagsUserControl(Tag.ToString());
                    }
                    tempForm.Height = userControl.Height + 25;
                    tempForm.Width = userControl.Width;
                    tempForm.Controls.Add(userControl);
                    var frmTemp = this.ParentForm as frmMain;
                    DialogResult result = tempForm.ShowDialog(frmTemp);
                    if (result == DialogResult.OK)
                    {
                        Ans = userControl.TagText.ToString().Trim();
                        return true;
                    }
                    else
                    {
                        Ans = "";
                        return false;
                    }
                }
                else
                {
                    Ans = Tag;
                    return true;
                }
            }
            else
            {
                Ans = Tag;
                return true;
            }
        }
        private void tbcmdSave_Click_1(object sender, EventArgs e)
        {
            ladderEditorControl1.InsertBlankRung();
        }
        private bool ValidateUDFBEditPermission()
        {
            string currentScreenName = XMPS.Instance.CurrentScreen.Split('#')[1];
            if (!currentScreenName.EndsWith(" logic", StringComparison.OrdinalIgnoreCase))
                return true;
            string normalizedNode = currentScreenName.Replace(" Logic", "").Trim();
            string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MessungSystems", "XMPS2000", "Library");
            string librarySubFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD", StringComparison.OrdinalIgnoreCase)? "XBLDLibraries": "XMLibraries";
            string libraryPath = Path.Combine(basePath, librarySubFolder);
            string[] csvFiles = Directory.Exists(libraryPath) ? Directory.GetFiles(libraryPath, "*.csv") : Array.Empty<string>();
            List<string> udfbNames = XMPS.Instance.LoadedProject.UDFBInfo.Select(u => u.UDFBName).ToList();
            var fileNames = csvFiles.Select(Path.GetFileNameWithoutExtension).Select(name => name.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                    ? name.Substring(0, name.Length - 6).Trim()
                    : name);
            bool isUdfbMatch = fileNames.Any(fileName => fileName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase) && udfbNames.Any(udfbName => udfbName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase)));
            if (isUdfbMatch)
            {
                string savedChoice = XMPS.Instance.LoadedProject.GetUDFBChoice(normalizedNode);
                if (string.IsNullOrEmpty(savedChoice))
                {
                    MessageBox.Show($"UDFB '{normalizedNode}' is a library function. Please configure UDFB edit preferences first.","XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        private void CommentBox_Clicked(LadderElement selectedElement)
        {
            string parent = selectedElement.Name.ToString();
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            AddComment userControl = new AddComment();
            tempForm.Text = "Adding Comment ";
            tempForm.Height = userControl.Height + 50;
            tempForm.Width = userControl.Width + 30;
            tempForm.Location = new Point(LadderDesign.ClickedElement.getX() + 20, MousePosition.Y - 60);
            LadderDesign.ClickedElement = selectedElement;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            userControl.SetText(selectedElement.Attributes["Caption"].ToString());
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK)
            {
                selectedElement.Attributes["Caption"] = userControl.EnteredText;
                ladderEditorControl1.ReScale();
            }
        }
        private void tsbSetCoil_Click(object sender, EventArgs e)
        {
            if (!ValidateUDFBEditPermission())
                return;
            ladderEditorControl1.UpdateSetStatus();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        private void tbcmdReset_Click(object sender, EventArgs e)
        {
            if (!ValidateUDFBEditPermission())
                return;
            ladderEditorControl1.UpdateResetStatus();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        internal int FindReplace(string text, string v1, int v2, uint num)
        {
            throw new NotImplementedException();
        }
        private void tsbInsertPNContact_Click(object sender, EventArgs e)
        {
            if (!ValidateUDFBEditPermission())
                return;
            ladderEditorControl1.UpdatePNStatus();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        private void tsbClearContact_Click(object sender, EventArgs e)
        {
            ladderEditorControl1.ClearStatus();
            getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
        }
        private void ShowUdfbInfo(string instructionName)
        {
            var infoForm = new UdfbInfoPopupForm(instructionName);
            infoForm.ShowDialog();
        }
        private string GetValue(string input)
        {
            return input.Trim().Trim('"');
        }
        
        private void TreeViewNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            frmMain frmMain = (frmMain)this.ParentForm;
            if (XMPS.Instance.PlcStatus != "LogIn")
            {
                if (e.Button == MouseButtons.Right && e.Node != null && e.Node.Parent != null)
                {
                    if (e.Node.Parent.Text == "HVAC_AND_OTHERS")
                    {
                        ContextMenuStrip contextMenu = new ContextMenuStrip();
                        ToolStripMenuItem infoItem = new ToolStripMenuItem("Info");
                        infoItem.Click += (s, args) => ShowUdfbInfo(e.Node.Text);
                        contextMenu.Items.Add(infoItem);
                        e.Node.TreeView.SelectedNode = e.Node;
                        contextMenu.Show(Cursor.Position);
                        return;
                    }
                }
                if (e.Node.Nodes.Count == 0)
                {
                    string instructionText = e.Node.Text;
                    if (e.Node.Parent != null && e.Node.Parent.Text == "HVAC_AND_OTHERS")
                    {
                        DialogResult udfbResult = MessageBox.Show(
                            $"Do you want to add this instruction as a UDFB to your project?",
                            "Insert UDFB",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (udfbResult != DialogResult.Yes)
                            return;
                        // ✅ Decide library folder based on PLC model
                        string baseLibraryPath = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "MessungSystems", "XMPS2000", "Library");

                        string libraryFolder;
                        if (XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD"))
                            libraryFolder = "XBLDLibraries";
                        else
                            libraryFolder = "XMLibraries";

                        string libraryPath = Path.Combine(baseLibraryPath, libraryFolder);
                        string csvFilePath = Path.Combine(libraryPath, $"{instructionText + " Logic"}.csv");
                        if (!File.Exists(csvFilePath))
                        {
                            MessageBox.Show($"UDFB file not found: {csvFilePath}", "File Not Found",
                                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (File.Exists(csvFilePath))
                        {                           
                            // Local definition of GetValue if not available
                            string GetValue(string value) => value?.Trim() ?? string.Empty;

                            // Read CSV file to check all tag names
                            List<string> csvTagNames = new List<string>();
                            List<string> csvLogicalAddresses = new List<string>();
                            using (StreamReader reader = new StreamReader(csvFilePath))
                            {
                                string line;
                                bool tagsSection = false;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (line.Equals("User Defined Tags"))
                                    {
                                        tagsSection = true;
                                        continue;
                                    }
                                    if (tagsSection && line.Contains(","))
                                    {
                                        string[] parts = line.Split(',');
                                        if (parts.Length > 3) // Tag name is in the 4th column (index 3)
                                        {
                                            string tagNameRaw = GetValue(parts[3]); // Tag name at index 3 (e.g., Tag: abc)
                                            string tagName = tagNameRaw.Replace("Tag: ", ""); // Remove "Tag: " prefix
                                            string logicalAddressRaw = GetValue(parts[2]); // Logical address at index 2 (e.g., LogicalAddress: F2:000)
                                            string logicalAddress = logicalAddressRaw.Replace("LogicalAddress:", ""); // Remove "LogicalAddress:" prefix
                                            if (!string.IsNullOrEmpty(tagName) && !string.IsNullOrEmpty(logicalAddress))
                                            {
                                                csvTagNames.Add(tagName);
                                                csvLogicalAddresses.Add(logicalAddress);
                                            }
                                        }
                                    }
                                }
                            }
                            List<string> foundTags = new List<string> { };
                            foreach (string tagnm in csvTagNames)
                            {
                                string corTagNm = tagnm.Replace("Tag: ", "");
                                if (XMPS.Instance.LoadedProject.Tags.Where(t => t.Tag == corTagNm).Count() > 0)
                                    foundTags.Add(tagnm);
                            }
                            List<string> foundLogicalAddresses = new List<string>();
                            foreach (string logicalAddr in csvLogicalAddresses)
                            {
                                if (XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == logicalAddr).Count() > 0)
                                    foundLogicalAddresses.Add(logicalAddr);
                            }
                            List<string> foundTagsWithLogicalAddresses = new List<string>();
                            for (int i = 0; i < csvTagNames.Count; i++)
                            {
                                string tagnm = csvTagNames[i];
                                string csvLogicalAddress = csvLogicalAddresses[i];
                                if (XMPS.Instance.LoadedProject.Tags.Where(t => t.Tag == tagnm).Count() > 0)
                                {
                                    var existingTag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.Tag == tagnm);
                                    if (existingTag != null && !(existingTag.LogicalAddress?.StartsWith("'") ?? false) && !(existingTag.LogicalAddress?.StartsWith("S") ?? false) && !(existingTag.LogicalAddress?.StartsWith("S3") ?? false)
                                        && !(existingTag.LogicalAddress?.StartsWith("I1") ?? false) && !(existingTag.LogicalAddress?.StartsWith("Q0") ?? false))
                                    {
                                        foundTagsWithLogicalAddresses.Add($"{tagnm} (Logical Address: {existingTag.LogicalAddress})");
                                    }
                                }
                            }
                            if (foundTagsWithLogicalAddresses.Count > 0 || foundLogicalAddresses.Count > 0)
                            {
                                string message = "Warning: Duplicate entries were found\n\n";
                                if (foundTagsWithLogicalAddresses.Count > 0)
                                    message += "- Tag Names already used: " + string.Join(", ", foundTagsWithLogicalAddresses) + "\n";
                                if (foundLogicalAddresses.Count > 0)
                                    message += "- Logical Addresses already used: " + string.Join(", ", foundLogicalAddresses) + "\n";
                                message += "\nThe UDFB will still be imported, but please review these duplicates.";
                                MessageBox.Show(message, "Duplicate Tag Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            frmMain.ImportLogicBlockAndUDFB(csvFilePath, "UDFB", true, true);
                            return;
                        }
                    }
                    string currentScreenName = XMPS.Instance.CurrentScreen.Split('#')[0];
                    if (currentScreenName.Contains("UDFLadderForm") && e.Node.Parent.Text.Equals("UDFB"))
                    {
                        MessageBox.Show($"Not Allowed", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (LadderDesign.ClickedElement != null || LadderDesign.PrevClickedElement != null)
                        {
                            if (LadderDesign.PrevClickedElement != null && LadderDesign.PrevClickedElement.Attributes["caption"].Equals("FunctionBlock"))
                            {
                                LadderDesign.PrevClickedElement = null;
                            }
                            else if (LadderDesign.ClickedElement != null && LadderDesign.ClickedElement.Attributes["caption"].Equals("FunctionBlock"))
                            {
                                LadderDesign.ClickedElement = null;
                            }
                            else
                            {
                                try
                                {
                                    if (e.Node.Text == "MQTT Publish")
                                    {
                                        var publist = XMPS.Instance.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                                        if (publist.Count < 1)
                                        {
                                            MessageBox.Show($"Please Add Publish Topic First", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    else if (e.Node.Text == "MQTT Subscribe")
                                    {
                                        var sublist = XMPS.Instance.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                                        if (sublist.Count < 1)
                                        {
                                            MessageBox.Show($"Please Add Subscribe Topic First", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    this.ParentNodeName = e.Node.Parent.Text;
                                    ladderEditorControl1.InsertFBAfter();
                                    LadderElement ladderElement = LadderDesign.PrevClickedElement;
                                    if (LadderDesign.PrevClickedElement != null)
                                    {
                                        ladderElement.Attributes["function_name"] = instructionText;
                                        FunctionBlock_Clicked(ladderElement);
                                        LadderDesign.PrevClickedElement = null;
                                        this.ParentNodeName = "";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                if (e.Node != null && e.Node.Text == "HVAC_AND_OTHERS")
                                {
                                    e.Node.Toggle();
                                    return;
                                }
                                bool isHVAC_Udfb = e.Node.Parent != null && e.Node.Parent.Text == "HVAC_AND_OTHERS";
                                if (!ValidateUDFBEditPermission())
                                {
                                    return; 
                                }
                                DialogResult dialogResult = isHVAC_Udfb
                                    ? DialogResult.Yes
                                    : MessageBox.Show("You Want to Insert New Rung", "XMPS 2000",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    if (e.Node.Text == "MQTT Publish")
                                    {
                                        var publist = XMPS.Instance.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                                        if (publist.Count < 1)
                                        {
                                            MessageBox.Show($"Please Add Publish Topic First", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    else if (e.Node.Text == "MQTT Subscribe")
                                    {
                                        var sublist = XMPS.Instance.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                                        if (sublist.Count < 1)
                                        {
                                            MessageBox.Show($"Please Add Subscribe Topic First", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    this.ParentNodeName = e.Node.Parent.Text;
                                    LadderWindow _windowRef = (LadderWindow)XMPS.Instance.LoadedScreens[XMPS.Instance.CurrentScreen];
                                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
                                    LadderDesign.Active.InsertRung();
                                    getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
                                    LadderElement ladderElement = LadderDesign.CurrentAddedElement;
                                    if (ladderElement == null)
                                        return;
                                    LadderElement ld = ladderElement.Elements.Where(T => T.CustomType.Equals("LadderDrawing.Contact")).FirstOrDefault();
                                    LadderDesign.ClickedElement = ld;
                                    ladderEditorControl1.InsertFBAfter();
                                    getLadderEditor().getCanvas().getDesignView().SetStateForUndoRedo();
                                    LadderElement ladderElement1 = LadderDesign.PrevClickedElement;
                                    ladderElement1.Attributes["function_name"] = instructionText;
                                    this.isDirectlyAddedFB = true;
                                    FunctionBlock_Clicked(ladderElement1);
                                    this.isDirectlyAddedFB = false;
                                    LadderDesign.PrevClickedElement = null;
                                    LadderDesign.ClickedElement = null;
                                    this.ParentNodeName = "";
                                    string screenName = XMPS.Instance.CurrentScreen;
                                    LadderWindow window = (LadderWindow)XMPS.Instance.LoadedScreens[screenName];
                                    window.getLadderEditor().ReScale(true);
                                    ladderEditorControl1.ReScale();
                                    ladderEditorControl1.Refresh();
                                    ShowInstructionLabel();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void InstructionPinButtonClick(object sender, MouseEventArgs e)
        {
            if (sender is Control control)
            {
                Control parent = control.Parent;
                SplitContainer sc = parent.Parent as SplitContainer;
                sc.SplitterDistance = sc.Right - 20;
                sc.Panel2.Hide();
                ShowInstructionLabel();
            }
        }
        private void ShowInstructionLabel()
        {
            SplitContainer parentSplitContainer = FindParentSplitContainer(this);
            Panel basePanel = parentSplitContainer.Panel2; // Assuming the base panel is in Panel1
            SplitContainer sc1 = basePanel.Controls[0] as SplitContainer;
            Button button = new Button();
            button.AutoSize = true;
            button.Size = (Size)new Point(20, 20);
            button.BackColor = Color.Wheat;
            string imagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\Devices\\PinSymbol.png";
            if (System.IO.File.Exists(imagePath))
            {
                button.Image = System.Drawing.Image.FromFile(imagePath);
            }
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            button.MouseClick += Label_MouseClick;
            sc1.Panel2.Controls.Clear();
            sc1.Panel2.Controls.Add(button);
            sc1.Panel2.PerformLayout();
            sc1.SplitterDistance = sc1.Right - 20;
            sc1.Panel2.Show();
            this.lblInstructions.Visible = true;
        }
        private TreeView GetTreeViewOfInstruction()
        {
            TreeView treeView1 = new TreeView();
            try
            {
                TreeView instructionTreeNodes = XMPS.Instance.instructionTreeNodes;
                foreach (TreeNode node in instructionTreeNodes.Nodes)
                {
                    treeView1.Nodes.Add((TreeNode)node.Clone());
                }
                //Adding New Node In tree View for the UDFB
                TreeNode nodeToRemove = treeView1.Nodes["UDFB"];
                if (nodeToRemove != null)
                    treeView1.Nodes.Remove(nodeToRemove);
                TreeNode uDFBNode = new TreeNode("UDFB");
                FunctionBlockInputsAndOutputs fbIO = new FunctionBlockInputsAndOutputs();
                List<string> udfbInstruction = fbIO.udfbInstructionNames;
                foreach (string instName in udfbInstruction)
                {
                    uDFBNode.Nodes.Add(instName);
                }
                if (uDFBNode.Nodes.Count > 0)
                    treeView1.Nodes.Add(uDFBNode);
                TreeNode hvacNode = new TreeNode("HVAC_AND_OTHERS");
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string libraryRoot = Path.Combine(basePath, "MessungSystems", "XMPS2000", "Library");
                string modelFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD")? "XBLDLibraries": "XMLibraries";
                string path = Path.Combine(libraryRoot, modelFolder);

                if (Directory.Exists(path))
                {
                    string[] csvFiles = Directory.GetFiles(path, "*.csv");
                    foreach (string filePath in csvFiles)
                    {
                        try
                        {
                            var firstLines = File.ReadLines(filePath).Take(15);

                            bool isUdfb = firstLines.Any(line =>
                                line.Trim().Equals("Block Type :- UDFB Block", StringComparison.OrdinalIgnoreCase));

                            bool isLogic = firstLines.Any(line =>
                                line.Trim().Equals("Block Type :- Logic Block", StringComparison.OrdinalIgnoreCase));

                            if (!isUdfb || isLogic)
                                continue;

                            string fileName = Path.GetFileNameWithoutExtension(filePath);

                            if (fileName.EndsWith("Logic", StringComparison.OrdinalIgnoreCase))
                                fileName = fileName.Substring(0, fileName.Length - "Logic".Length).Trim();

                            hvacNode.Nodes.Add(fileName);
                        }
                        catch (IOException ioex) when ((ioex.HResult & 0xFFFF) == 32) 
                        {
                            MessageBox.Show(
                                $"The library file '{Path.GetFileName(filePath)}' is open in excel or another program.\n" +
                                $"Please close it and refresh the instruction list.",
                                "File In Use",
                                MessageBoxButtons.OK,MessageBoxIcon.Warning
                            );
                            continue;
                        }
                    }
                }

                if (!treeView1.Nodes.Cast<TreeNode>().Any(n => n.Text == hvacNode.Text))
                    treeView1.Nodes.Add(hvacNode);

                return treeView1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading instructions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private SplitContainer FindParentSplitContainer(System.Windows.Forms.Control control)
        {
            System.Windows.Forms.Control parent = control.Parent;

            while (parent != null)
            {
                if (parent is SplitContainer)
                {
                    if (parent.Name == "splcMain")
                    {
                        return (SplitContainer)parent;
                    }
                }
                parent = parent.Parent;
            }
            // If no SplitContainer is found in the hierarchy
            return null;
        }
        private void ladderEditorControl1_Load(object sender, EventArgs e)
        {
            ShowInstructionLabel();
        }
        private void Label_MouseClick(object sender, MouseEventArgs e)
        {
            string screenName = XMPS.Instance.CurrentScreen;
            if (screenName.Contains("LadderForm") && XMPS.Instance.PlcStatus != "LogIn")
            {
                this.lblInstructions.Visible = false;
                if (sender is Control control)
                {
                    SplitContainer parentSplitContainer = FindParentSplitContainer(control);
                    if (parentSplitContainer != null)
                    {
                        // Access the base panel of the SplitContainer
                        Panel basePanel = parentSplitContainer.Panel2; // Assuming the base panel is in Panel1
                        SplitContainer sc1 = basePanel.Controls[0] as SplitContainer;
                        sc1.Panel2.BorderStyle = BorderStyle.FixedSingle;
                        //sc1.Panel2.BackColor  = Color.Red;
                        Button button = new Button();
                        button.AutoSize = true;
                        button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                        button.Size = new Size(25, 25);
                        string imagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\Devices\PinSymbol.png";
                        button.BackColor = Color.Wheat;
                        if (System.IO.File.Exists(imagePath))
                        {
                            button.Image = System.Drawing.Image.FromFile(imagePath);
                        }
                        button.BackgroundImageLayout = ImageLayout.Stretch;
                        button.ImageAlign = ContentAlignment.MiddleCenter;
                        button.MouseClick += new MouseEventHandler(this.InstructionPinButtonClick);

                        sc1.Panel2.Padding = new Padding(5);
                        Panel treeViewPanel = new Panel();
                        treeViewPanel.Dock = DockStyle.Fill;
                        treeViewPanel.Width = DefaultWidth;
                        treeViewPanel.BorderStyle = BorderStyle.FixedSingle;

                        //creating TreeView of instructions
                        TreeView treeView1 = GetTreeViewOfInstruction();
                        treeView1.Dock = DockStyle.Fill;
                        treeView1.BorderStyle = BorderStyle.None;
                        treeView1.NodeMouseClick += TreeViewNodeClick;

                        // Add the TreeView to the Panel
                        treeViewPanel.Controls.Add(treeView1);

                        // Add mouse events for resizing to the Panel
                        sc1.Panel2.MouseDown += TreeViewPanel_MouseDown;
                        sc1.Panel2.MouseMove += TreeViewPanel_MouseMove;
                        sc1.Panel2.MouseUp += TreeViewPanel_MouseUp;
                        sc1.Panel2.MouseEnter += TreeViewPanel_MouseEnter;
                        sc1.Panel2.MouseLeave += TreeViewPanel_MouseLeave;


                        sc1.Panel2.Controls.Clear();
                        sc1.Panel2.Controls.Add(button);
                        sc1.Panel2.Controls.Add(treeViewPanel);
                        sc1.SplitterDistance = isSplitterDistanceChanged ? (sc1.Width < lastSplitterDistance) ? lastSplitterDistance - sc1.Width : lastSplitterDistance : sc1.Width - 150;
                        sc1.Panel2.Show();

                        //LadderWindow window = (LadderWindow)XMPS.Instance.LoadedScreens[screenName];
                        //window.getLadderEditor().ReScale(true);
                        //ladderEditorControl1.ReScale();
                        //ladderEditorControl1.Refresh();
                        sc1.Panel2.Refresh();
                    }
                }
            }
        }
        private void TreeViewPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Control panel = sender as Control;
            if (!isResizing)
            {
                Cursor.Current = IsNearBorder(panel, e.Location) ? Cursors.VSplit : Cursors.Default;
                return;
            }

            if (isResizing && e.Button == MouseButtons.Left)
            {
                SplitContainer parentSplitContainer = FindParentSplitContainer(panel);
                if (parentSplitContainer != null)
                {
                    SplitContainer sc1 = parentSplitContainer.Panel2.Controls[0] as SplitContainer;
                    if (sc1 == null) return;

                    int deltaX = e.Location.X - lastMouseLocation.X;
                    newWidth1 = panel.Width + deltaX;

                    if (newWidth1 >= MinWidth)
                    {
                        deltaXAxis = (initialSplitterDistance += deltaX);
                    }

                    lastMouseLocation = e.Location;
                }
            }
        }

        private void TreeViewPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Control panel = sender as Control;
            if (e.Button == MouseButtons.Left && IsNearBorder((Control)sender, e.Location))
            {
                isResizing = true;
                lastMouseLocation = e.Location;
                Cursor.Current = Cursors.VSplit;

                panel = sender as Control;
                newWidth1 = panel.Width;

                SplitContainer parentSplitContainer = FindParentSplitContainer(panel);
                if (parentSplitContainer != null)
                {
                    SplitContainer sc1 = parentSplitContainer.Panel2.Controls[0] as SplitContainer;
                    if (sc1 != null)
                    {
                        initialSplitterDistance = sc1.SplitterDistance; // Store initial SplitterDistance
                    }
                }
            }
        }

        private void TreeViewPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isResizing && e.Button == MouseButtons.Left)
            {
                Control panel = sender as Control;
                if (panel == null) return;

                panel.Width = newWidth1;

                SplitContainer parentSplitContainer = FindParentSplitContainer(panel);
                if (parentSplitContainer != null)
                {
                    SplitContainer sc1 = parentSplitContainer.Panel2.Controls[0] as SplitContainer;
                    if (sc1 != null && deltaXAxis > 20 && newWidth1 > 40)
                    {
                        isSplitterDistanceChanged = true;
                        sc1.SuspendLayout();
                        sc1.SplitterDistance = deltaXAxis;
                        lastSplitterDistance = deltaXAxis;
                        sc1.ResumeLayout();
                    }
                    else
                    {
                        if (newWidth1 < 41)
                        {
                            ShowInstructionLabel();
                            lastSplitterDistance = sc1.Width - 150;
                        }
                    }
                }

                isResizing = false;
                Cursor.Current = Cursors.Default;
            }
        }
        private void TreeViewPanel_MouseEnter(object sender, EventArgs e)
        {
            if (IsNearBorder((Control)sender, ((Control)sender).PointToClient(Cursor.Position)))
            {
                Cursor.Current = Cursors.VSplit;
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void TreeViewPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!isResizing)
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private bool IsNearBorder(Control panel, Point location)
        {
            return location.X <= resizeBorderWidh || location.X >= panel.Width - resizeBorderWidh;
        }

        private void ladderEditorControl1_KeyDown(object sender, KeyEventArgs e)
        {
            string loginStatus = XMPS.Instance.PlcStatus;
            LadderElement element = LadderDesign.ClickedElement;
            if (element == null && e.KeyCode == Keys.Space)
                element = LadderDesign.PNStatusElement;

            Type typeOf = element?.customDrawing.GetType();
            if (e.Control && e.KeyCode == Keys.B && loginStatus != "LogIn")
            {
                toolStripButton1_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.K && loginStatus != "LogIn")
            {
                tsbInsertContactBefore_Click_1(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.D && loginStatus != "LogIn")
            {
                tsbInsertContactAfter_Click(sender, e);
            }
            else if (element != null)
            {
                if (e.KeyCode == Keys.Enter && loginStatus != "LogIn")
                {
                    if (typeOf == typeof(LadderDrawing.FunctionBlock))
                    {
                        FunctionBlock_Clicked(element);
                    }
                    if ((typeOf == typeof(LadderDrawing.Contact) || typeOf == typeof(LadderDrawing.Coil)))
                    {
                        TagWindow_Clicked(element);
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (typeOf == typeof(Contact))
                    {
                        if (XMPS.Instance.PlcStatus == "LogIn")
                        {
                            if (element == null) return;
                            if (XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == element.Attributes["LogicalAddress"].ToString()).Select(t => t.ReadOnly).FirstOrDefault())
                                MessageBox.Show("Read only elements can't be forced", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                ForceBitValue forceBitValue = new ForceBitValue(element.Attributes["LogicalAddress"].ToString());
                                bool sevalue = OnlineMonitoringStatus.AddressValues.ContainsKey(element.Attributes["LogicalAddress"].ToString())
                                    ? Convert.ToBoolean(Convert.ToInt16(OnlineMonitoringStatus.AddressValues[element.Attributes["LogicalAddress"].ToString()]))
                                    : OnlineMonitoringStatus.AddressValues.ContainsKey(element.Attributes["caption"].ToString())
                                    ? Convert.ToBoolean(Convert.ToInt16(OnlineMonitoringStatus.AddressValues[element.Attributes["caption"].ToString()])) : false;
                                forceBitValue.CommonForceFunctionality(!sevalue);
                            }
                        }
                        else
                        {
                            if (element.Negation == false && (element.Attributes["PNStatus"] == null || element.Attributes["PNStatus"].ToString() == ""))
                            {
                                tsbSwapItemStyle_Click(sender, e);
                            }
                            else if (element.Negation == true)
                            {
                                element.Negation = false;
                                tsbInsertPNContact_Click(sender, e);
                            }
                            else if (element.Attributes["PNStatus"] != null || element.Attributes["PNStatus"].ToString() != "")
                            {
                                tsbInsertPNContact_Click(sender, e);
                            }
                        }
                    }
                    else if (typeOf == typeof(Coil) || typeOf == typeof(CoilParallel))
                    {
                        if (XMPS.Instance.PlcStatus == "LogIn")
                        {
                            ForceBitValue forceBitValue = new ForceBitValue(element.Attributes["LogicalAddress"].ToString());
                            bool sevalue = OnlineMonitoringStatus.AddressValues.ContainsKey(element.Attributes["LogicalAddress"].ToString())
                                ? Convert.ToBoolean(Convert.ToInt16(OnlineMonitoringStatus.AddressValues[element.Attributes["LogicalAddress"].ToString()]))
                                : OnlineMonitoringStatus.AddressValues.ContainsKey(element.Attributes["caption"].ToString())
                                ? Convert.ToBoolean(Convert.ToInt16(OnlineMonitoringStatus.AddressValues[element.Attributes["caption"].ToString()])) : false;
                            forceBitValue.CommonForceFunctionality(!sevalue);
                        }
                        else
                        {
                            if (element.Attributes["SetReset"].ToString() == "")
                            {
                                tsbSetCoil_Click(sender, e);
                            }
                            else if (element.Attributes["SetReset"].ToString() == "S" || element.Attributes["SetReset"].ToString() == "R")
                            {
                                tbcmdReset_Click(sender, e);
                            }
                        }
                    }
                }
            }
        }

        private void ladderEditorControl1_CrossReferanceClicked(object sender, EventArgs e)
        {
            frmMain frmMain = (frmMain)this.ParentForm;
            LadderElement element = LadderDesign.ClickedElement;
            frmMain.InvokeCrossReference(element.Attributes["caption"].ToString());
        }
    }
}
