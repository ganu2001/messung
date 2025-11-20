using LadderDrawing;
using LadderDrawing.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Configuration;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.LadderLogic;
using Attribute = LadderDrawing.Attribute;

namespace XMPS2000
{
    public partial class MainLadderForm : Form, IXMForm
    {
        XMPS xm;
        string DraggedItem = "";
        List<string> line = new List<string> { };
        public MainLadderForm()
        {
            InitializeComponent();
            xm = XMPS.Instance;
            CheckandAddAllAddedRecords();

        }

        public LadderEditorControl getLadderEditor()
        {
            return ladderEditorControlMain;
        }

        public ToolStrip getLadderEditorToolStrip()
        {
            return tsBlocks;
        }
        public void OnShown()
        {
            if (xm.PlcStatus != "LogIn")
            {
                tsBlocks.Enabled = true;
                tvLogicBlocks.Enabled = true;
            }
            else
            {
                tsBlocks.Enabled = false;
                tvLogicBlocks.Enabled = false;
            }

            LadderDesign.ClickedElement = null;
            this.tvLogicBlocks.Nodes.Clear();
            this.tvLogicBlocks.Nodes.Add("Logic Blocks", "Logic Blocks", 0, 0);
            for (int i = 0; i < xm.LoadedProject.Blocks.Count; i++)
            {
                //Adding Interrupt Logic Blocks in MainLadderForm tvLogicBlocks dropdown
                if (xm.LoadedProject.Blocks[i].Type == "LogicBlock")
                    this.tvLogicBlocks.Nodes[0].Nodes.Add(xm.LoadedProject.Blocks[i].Name.ToString());
            }
            CheckandAddAllAddedRecords();
        }

        private void tvLogicBlocks_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DraggedItem = e.Item.ToString().Replace("TreeNode: ", "");
            line.Add(DraggedItem);
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void LadderMainGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }


        private void LadderMainGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            Graphics gfx = e.Control.CreateGraphics();

            using (Brush borderBrush = new SolidBrush(Color.Red))
            {
                using (Pen borderPen = new Pen(borderBrush, 2))
                {
                    System.Drawing.Rectangle rectDimensions = e.Control.ClientRectangle;
                    rectDimensions.Width -= 2;
                    rectDimensions.Height -= 2;
                    rectDimensions.X = rectDimensions.Left + 1;
                    rectDimensions.Y = rectDimensions.Top + 1;

                    gfx.DrawRectangle(borderPen, rectDimensions);
                }
            }
        }



        private void LadderMainGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void tvLogicBlocks_DoubleClick(object sender, EventArgs e)
        {
            if (xm.PlcStatus == "LogIn")
            {
                MessageBox.Show("Block addition is not allowed in log in mode, log out for addtion", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tvLogicBlocks.SelectedNode != null)
            {
                if (xm.LoadedProject.Blocks.Where(d => d.Name == tvLogicBlocks.SelectedNode.Text.ToString()).Count() > 0)
                {
                    xm.LoadedProject.MainLadderLogic.Add(tvLogicBlocks.SelectedNode.Text.ToString());
                    ladderEditorControlMain.InsertNewBlock(tvLogicBlocks.SelectedNode.Text.ToString());
                    SaveData();
                    refreshLadderMainGridView();
                }
            }
        }

        private void SaveData()
        {
            xm.LoadedProject.MainLadderLogic.Clear();
            LadderDrawing.LadderDesign.Active = ladderEditorControlMain.getCanvas().getDesignView();

            foreach (LadderElement ld in LadderDrawing.LadderDesign.Active.Elements)
            {
                if (ld.Elements.Count > 1)
                {
                    string buildFormula = "";
                    foreach (LadderElement baseElement in ld.Elements)
                    {
                        string negate = baseElement.Negation ? "~" : "";
                        string comment = "";
                        if (!buildFormula.StartsWith("'"))
                        {
                            comment = CheckRungIsCommented(baseElement) ? "'" : "";
                        }
                        string controlName = comment + (baseElement.CustomType.ToString() == "LadderDrawing.Contact" ? "(" + negate + baseElement.Attributes["caption"].ToString() + ")" : "[" + baseElement.Attributes["caption"].ToString() + "]");
                        buildFormula = buildFormula.Length > 0 ? buildFormula + " AND " + controlName : controlName;
                    }
                    xm.LoadedProject.MainLadderLogic.Add(buildFormula);

                }
                else
                    if (ld.Elements.Count > 0) xm.LoadedProject.MainLadderLogic.Add(ld.Elements[0].Attributes[0].Value.ToString());
            }
        }

        public bool CheckRungIsCommented(LadderElement ladderElement)
        {
            // TO get rootElement 
            LadderElement rootElement = ladderElement.getRoot();
            //Find if First Ladder Element from rootLadderElement is Commented or Not
            LadderElement firstLadderElement = rootElement.Elements.First();
            foreach (LadderElement ld in rootElement.Elements)
            {
                foreach (Attribute attribute in ld.Attributes.ToList())
                {
                    if (attribute.Name == "isCommented")
                    {
                        Attribute newAttribute = new Attribute();
                        newAttribute.Name = "isCommented";
                        return true;
                    }
                }
            }

            return false;
        }
        private void CheckandAddAllAddedRecords()
        {
            //foreach (string line in xm.LoadedProject.MainLadderLogic)
            //    if (line != "" && !line.StartsWith("Interrupt_Logic_Block")) LadderMainGridView.Rows.Add(line);
            ladderEditorControlMain.getCanvas().getDesignView().Elements.Clear();
            int i = 0;
            foreach (string line in xm.LoadedProject.MainLadderLogic)
            {
                if (!line.StartsWith("Interrupt_Logic_Block"))
                {
                    if ((line.StartsWith("'") && line.Contains("(") || line.StartsWith("(")))
                        ShowdataFromFormula(line, i);
                    else
                        ladderEditorControlMain.InsertNewBlock(line, i);
                    i++;
                }
            }

        }

        private void ShowdataFromFormula(string line, int i)
        {
            int pointer = 0;
            int nextpointer = 0;
            string remainString = line;
            LadderElement addedBlock = new LadderElement();
            bool commented = remainString.Contains("'") ? true : false;
            pointer = remainString.IndexOf("[");
            nextpointer = remainString.IndexOf("]");
            string addObject = line.Substring(pointer, nextpointer - (pointer - 1));
            addedBlock = ladderEditorControlMain.InsertNewBlock(addObject.Replace("[", "").Replace("]", ""), i);
            if (commented) addedBlock.getRoot().Attributes["isCommented"] = "";
            if (commented) addedBlock.Attributes["isCommented"] = "";

            remainString = remainString.Replace(addObject, "");
            while (remainString.Contains("("))
            {
                pointer = remainString.IndexOf("(");
                nextpointer = remainString.IndexOf(")");
                addObject = remainString.Substring(pointer, nextpointer - (pointer - 1));
                LadderDesign.ClickedElement = addedBlock;
                LadderElement addedElement = ladderEditorControlMain.InsertContactBefore();
                addedElement.Negation = addObject.Contains("~") ? true : false;
                //string tagName= addObject.Replace("(", "").Replace(")", "").Replace("~", "");
                addedElement.Attributes["caption"] = addObject.Replace("(", "").Replace(")", "").Replace("~", "") /*tagName*/;
                remainString = remainString.Replace(addObject, "");
            }
            Refresh();
            //ladderEditorControl1.InsertContactBefore(line);
        }

        private void cntxdelete_Click(object sender, EventArgs e)
        {
            xm.LoadedProject.MainLadderLogic.RemoveAll(R => R.Contains(LadderDesign.ClickedElement.ToString()));
            //xm.LoadedProject.MainLadderLogic.RemoveAt(LadderMainGridView.SelectedCells[0].RowIndex);
            OnShown();
            refreshLadderMainGridView();
        }



        private void refreshLadderMainGridView()
        {
            //LadderMainGridView.Rows.Clear();
            CheckandAddAllAddedRecords();
        }

        private void cntxCommentBlock_Click(object sender, EventArgs e)
        {
            xm.LoadedProject.MainLadderLogic.RemoveAll(T => T.StartsWith("Interrupt_Logic_Block"));
            //string blockName = xm.LoadedProject.MainLadderLogic.ElementAt(LadderMainGridView.SelectedCells[0].RowIndex);
            //int index = xm.LoadedProject.MainLadderLogic.FindIndex(x => x == blockName);
            //if (index != -1)
            //{
            //    xm.LoadedProject.MainLadderLogic[index] = "'" + blockName;
            //}
            //xm.LoadedProject.MainLadderLogic.Select(s => s == blockName ? ("'" + blockName) : s).ToList();
            //Block blockToComment = xm.LoadedProject.Blocks.Where(d => d.Name == blockName).FirstOrDefault();
            //blockToComment.Name = "'" + blockToComment.Name;
            refreshLadderMainGridView();
        }

        private void cntxUnCommentBlock_Click(object sender, EventArgs e)
        {
            //string blockName = xm.LoadedProject.MainLadderLogic.ElementAt(LadderMainGridView.SelectedCells[0].RowIndex);
            //int index = xm.LoadedProject.MainLadderLogic.FindIndex(x => x == blockName);
            //if (index != -1)
            //{
            //    xm.LoadedProject.MainLadderLogic[index] =blockName.Replace("'","");
            //}
            ////xm.LoadedProject.MainLadderLogic.Select(s => s == blockName ? ("'" + blockName) : s).ToList();
            ////Block blockToComment = xm.LoadedProject.Blocks.Where(d => d.Name == blockName).FirstOrDefault();
            ////blockToComment.Name = blockToComment.Name.Replace("'", "");
            refreshLadderMainGridView();
        }

        private void tsbInsertRung_Click(object sender, EventArgs e)
        {
            string BlockName = "";
            ladderEditorControlMain.InsertNewBlock(BlockName);
            SaveData();
        }

        private void tsbInsertContactBefore_Click(object sender, EventArgs e)
        {
            LadderElement element = LadderDesign.ClickedElement;
            if (element == null)
            {
                MessageBox.Show("Select ladder block or contact before adding contact", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (element.getRoot().Elements.Count() < 3)
            {
                ladderEditorControlMain.InsertContactBefore();
                SaveData();
            }
            else
            {
                MessageBox.Show("Maximum number of contacts added in this rung", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void tsbSwapItemStyle_Click(object sender, EventArgs e)
        {
            ladderEditorControlMain.NegateStyle();
            SaveData();
        }

        private void tsbInsertContactAfter_Click(object sender, EventArgs e)
        {
            ladderEditorControlMain.InsertContactAfter();

        }

        private void tvLogicBlocks_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void ladderEditorControl1_ItemClicked(object sender, MouseEventArgs e)
        {
            LadderElement selectedelement = (LadderElement)sender;
            if (selectedelement.customDrawing.toString() == "Contact")
            {
                if (XMPS.Instance.PlcStatus == "LogIn")
                    ForceFunctionality_Clicked((LadderElement)sender);
                else if (XMPS.Instance.PlcStatus != "LogIn")
                    TagWindow_Clicked((LadderElement)sender);
            }

        }
        private void ForceFunctionality_Clicked(LadderElement selectedElement)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            if (selectedElement.customDrawing.toString() == "Contact" || selectedElement.customDrawing.toString() == "Coil")
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
            }
        }
        /// <summary>
        /// Show Tag window as user has clicked on contact element
        /// </summary>
        /// <param name="selectedElement"> Pass element selected by the user </param>
        private void TagWindow_Clicked(LadderElement selectedElement)
        {

            if (selectedElement.customDrawing.GetType() != typeof(Contact)) return;

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

                if (selectedElement.CustomType == "LadderDrawing.Coil" || selectedElement.CustomType == "LadderDrawing.CoilParallel")
                {
                    list = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F") || T.LogicalAddress.Contains(".") && !T.Type.ToString().Contains("Input")).Select(T => T.Tag).ToList();
                    list.AddRange(XMPS.Instance.LoadedProject.Tags.Where(T => T.IoList == Core.Types.IOListType.Default && T.Label == "Bool").Select(T => T.Tag).ToList());
                }
                else
                {
                    list = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F") || T.LogicalAddress.Contains(".")).Select(T => T.Tag).ToList();
                    list.AddRange(XMPS.Instance.LoadedProject.Tags.Where(T => T.IoList == Core.Types.IOListType.Default && T.Label == "Bool").Select(T => T.Tag).ToList());
                }
                UDFBInfo udfbios = (UDFBInfo)XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName == tslblblockname.Text.ToString().Replace(" Logic", "")).FirstOrDefault();
                if (udfbios != null)
                {
                    if (udfbios.Inputs > 0)
                    {
                        list.AddRange(udfbios.UDFBlocks.Where(t => t.DataType == "Bool").Select(t => t.Text).ToList());
                    }
                }
                for (int x = 0; x < list.Count; x++)
                {
                    userControl.AddListItem(x.ToString(), list[x].Trim()); //x.ToString(), "var " + x.ToString()
                }

            }
            var frmTemp = this.ParentForm as frmMain;
            userControl.SetText(selectedElement.Attributes["Caption"].ToString());
            if (selectedElement.customDrawing.toString() != "Comment")
            {
                DialogResult result = tempForm.ShowDialog(frmTemp);
                if (result == DialogResult.OK)
                {
                    if (CheckandAddtag(userControl.EnteredText.ToString(), out string GetAns))
                    {
                        selectedElement.Attributes["Caption"] = GetAns;
                        selectedElement.Attributes["LogicalAddress"] = XMPS.Instance.LoadedProject.Tags.Where(t => t.Tag == GetAns).Select(t => t.LogicalAddress).FirstOrDefault();

                    }
                    ladderEditorControlMain.ReScale();
                }
            }
            SaveData();
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
                    //Adding Tag From UdfbLogic Window
                    //string screenName =XMPS.Instance.CurrentScreen.ToString();
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
                        Ans = userControl.TagText.ToString();
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

        private void ladderEditorControl1_ItemDeleted(object sender, KeyEventArgs e)
        {
            if (LadderDesign.ClickedElement != null && LadderDesign.ClickedElement.CustomType == "LadderDrawing.Contact")
            {
                string linecode = xm.LoadedProject.MainLadderLogic[LadderDesign.ClickedElement.Position.Parent.Position.Index];
                linecode = linecode.Replace(LadderDesign.ClickedElement.Attributes["Caption"].ToString(), "").Replace("()", "");
                linecode = linecode.Trim().StartsWith("AND") ? linecode.Replace("AND", "").Trim() : linecode.Trim();
                xm.LoadedProject.MainLadderLogic[LadderDesign.ClickedElement.Position.Parent.Position.Index] = linecode;
                LadderDesign.ClickedElement.Position.Parent.Elements.Remove(LadderDesign.ClickedElement);

            }
            else if (LadderDesign.ClickedElement != null && LadderDesign.ClickedElement.CustomType == "LadderDrawing.LadderBlock")
            {
                xm.LoadedProject.MainLadderLogic.RemoveAt(LadderDesign.ClickedElement.Position.Parent.Position.Index);
                LadderDesign.ClickedElement.Position.Parent.Elements.Clear();

            }
            else if (LadderDesign.ClickedElement != null && LadderDesign.ClickedElement.CustomType == "LadderDrawing.Rung")
            {
                xm.LoadedProject.MainLadderLogic.RemoveAt(LadderDesign.ClickedElement.Position.Index);
                LadderDesign.ClickedElement.Elements.Clear();
            }
            SaveData();
            ladderEditorControlMain.ReScale();

        }
        private void ladderEditorControlMain_CrossReferanceClicked(object sender, EventArgs e)
        {
            frmMain frmMain = (frmMain)this.ParentForm;
            LadderElement element = LadderDesign.ClickedElement;
            frmMain.InvokeCrossReference(element.Attributes["caption"].ToString());
        }
    }
}
