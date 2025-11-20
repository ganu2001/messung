using iTextSharp.text.pdf;
using LadderDrawing;
using LadderEditorLib.MementoDesign;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.LadderLogic;

namespace XMPS2000
{
    public partial class ShowUDFBSuggestion : Form
    {
        private XMPS xm;
        private LadderDesignMemento oldDesing;
        private List<string> currentUDFBBlockElemets;
        public ShowUDFBSuggestion(List<string> items)
        {
            InitializeComponent();
            xm = XMPS.Instance;
            items.Reverse();
            AddButtons(items);
        }
        public void AddButtons(List<string> items)
        {
            btnsPanel.Controls.Clear();
            foreach (string item in items)
            {
                // Create a new button
                Button btn = new Button
                {
                    Text = item,
                    AutoSize = true,
                    Dock = DockStyle.Top,
                    Margin = new Padding(5)
                };
                btn.Click += (sender, e) => Button_Click(sender, e, item);
                btnsPanel.Controls.Add(btn);
            }
            btnsPanel.AutoScroll = true;
            // Set focus to the first button (visually top one)
            if (btnsPanel.Controls.Count > 0 && btnsPanel.Controls[btnsPanel.Controls.Count - 1] is Button firstButton)
            {
                firstButton.Select();
            }
            
        }

        private void Button_Click(object sender, EventArgs e, string item)
        {
            string[] parts = item.Split(' ');

            string logicBlock = item.Split(new[] { "Logic Block" }, StringSplitOptions.None).LastOrDefault()?.Trim().Split(new[] { "rung" }, StringSplitOptions.None)[0].Trim();
            int rungNumber = int.Parse(parts[parts.Length - 1]);

            LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"LadderForm#{logicBlock}"];
            LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
            LadderElement ladderElement = LadderDrawing.LadderDesign.Active.Elements.FirstOrDefault(t => t.Position.Index == rungNumber - 1);

            Dictionary<string, string> mapvalues = new Dictionary<string, string> { };

            string functionname = ladderElement.Elements.FirstOrDefault(t => t.CustomType == "LadderDrawing.FunctionBlock").Attributes["function_name"].ToString();
            if (!xm.LoadedScreens.ContainsKey($"UDFLadderForm#{functionname + " Logic"}"))
            {
                this.Close();
                return;
            }
            //adding inputs
            List<string> udfbtexts = xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == functionname.ToString()).FirstOrDefault().UDFBlocks.Where(S => S.Type == "Input").Select(T => T.Text).ToList();
            if (udfbtexts.Count > 0)
            {
                for (int i = 0; i < udfbtexts.Count; i++)
                {
                    mapvalues.Add(udfbtexts[i], ladderElement.Elements.FirstOrDefault(t => t.CustomType == "LadderDrawing.FunctionBlock").Attributes[$"input{i + 1}"].ToString());
                }
            }
            udfbtexts.Clear();
            //adding outputs
            udfbtexts.AddRange(xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == functionname.ToString()).FirstOrDefault().UDFBlocks.Where(S => S.Type == "Output").Select(T => T.Text).ToList());
            if (udfbtexts.Count > 0)
            {
                for (int i = 0; i < udfbtexts.Count; i++)
                {
                    mapvalues.Add(udfbtexts[i], ladderElement.Elements.FirstOrDefault(t => t.CustomType == "LadderDrawing.FunctionBlock").Attributes[$"output{i + 1}"].ToString());
                }
            }
            //for getting UDFB ladder rungs
            LadderWindow udfbWindow = (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{functionname + " Logic"}"];
            LadderDrawing.LadderDesign.Active = udfbWindow.getLadderEditor().getCanvas().getDesignView();

            oldDesing = new LadderDesignMemento(LadderDrawing.LadderDesign.Active);

            int _blockIndex = xm.LoadedProject.Blocks.FindIndex(d => d.Name == $"{functionname + " Logic"}");

            List<string> elements = xm.LoadedProject.Blocks[_blockIndex].Elements;
            currentUDFBBlockElemets = new List<string>(elements);

            for (int k = 0; k < LadderDrawing.LadderDesign.Active.Elements.Count(); k++)
            {
                for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[k].Elements.Count; j++)
                {
                    LadderElement ld = LadderDrawing.LadderDesign.Active.Elements[k].Elements[j];
                    if (ld.CustomType == "LadderDrawing.Contact")
                    {
                        CheckForContactCoil(ld, mapvalues, _blockIndex, ld.Position.Parent.Position.Index);
                    }
                    else if (ld.CustomType == "LadderDrawing.DummyParallelParent" || ld.CustomType == "LadderDrawing.Coil")
                    {
                        if (ld.CustomType == "LadderDrawing.Coil")
                        {
                            CheckForContactCoil(ld, mapvalues, _blockIndex, ld.Position.Parent.Position.Index);
                        }
                        if (ld.Elements.Count > 0)
                        {
                            CheckInChildElements(ld.Elements, mapvalues, _blockIndex, ld.Position.Parent.Position.Index);
                        }
                    }
                    else if (ld.CustomType == "LadderDrawing.FunctionBlock")
                    {
                        foreach (LadderDrawing.Attribute att in ld.Attributes)
                        {
                            if (mapvalues.ContainsKey(att.Value.ToString().Replace("~","")))
                            {
                                string actualKey = att.Value.ToString();
                                bool isNegationPresent = att.Value.ToString().StartsWith("~");
                                string actualLogicalAdd = string.Empty;
                                mapvalues.TryGetValue(att.Value.ToString().Replace("~", ""), out actualLogicalAdd);
                                att.Value = isNegationPresent ? "~"+actualLogicalAdd : actualLogicalAdd;
                                int rungNo = ld.Position.Parent.Position.Index;
                                xm.LoadedProject.Blocks[_blockIndex].Elements[rungNo] =
                               Regex.Replace(xm.LoadedProject.Blocks[_blockIndex].Elements[rungNo], $@"\b{actualKey}\b", $"{att.Value}");
                            }
                        }
                    }
                }
            }
            this.Close();
        }
        private void CheckForContactCoil(LadderElement ld, Dictionary<string, string> mapvalues, int _blockIndex, int rungIndex)
        {
            foreach (LadderDrawing.Attribute att in ld.Attributes.ToList())
            {
                if (mapvalues.ContainsKey(att.Value.ToString()))
                {
                    string actualKey = att.Value.ToString();
                    string actualLogicalAdd = string.Empty;
                    mapvalues.TryGetValue(att.Value.ToString(), out actualLogicalAdd);
                    ld.Attributes["caption"] = XMProValidator.GetTheTagnameFromAddress(actualLogicalAdd.Replace("~",""));

                    if (ld.Attributes.Any(t => t.Name.Equals("LogicalAddress")))
                    {
                        ld.Attributes["LogicalAddress"] = actualLogicalAdd.Replace("~", "");
                    }
                    else
                    {
                        LadderDrawing.Attribute logicalAdd = new LadderDrawing.Attribute();
                        logicalAdd.Name = "LogicalAddress";
                        logicalAdd.Value = actualLogicalAdd.Replace("~", "");
                        ld.Attributes.Add(logicalAdd);
                    }
                    xm.LoadedProject.Blocks[_blockIndex].Elements[rungIndex] =
                   Regex.Replace(xm.LoadedProject.Blocks[_blockIndex].Elements[rungIndex], $@"\b{actualKey}\b", $"{ld.Attributes["caption"]}");
                }
            }
        }

        private void CheckInChildElements(LadderElements elements, Dictionary<string, string> valuePairs, int _blockIndex, int rungIndex)
        {
            foreach (var element in elements)
            {
                if (element.customDrawing.GetType().Name.Equals("Contact") || element.customDrawing.GetType().Name.Equals("CoilParallel"))
                {
                    CheckForContactCoil(element, valuePairs, _blockIndex, rungIndex);
                }
                if (element.Elements.Any())
                {
                    CheckInChildElements(element.Elements, valuePairs, _blockIndex, rungIndex);
                }
            }
        }
        public LadderDesignMemento OldUDFBDesing()
        {
            if (oldDesing != null)
            {
                return oldDesing;
            }
            return null;
        }
        public List<string> OldBlockElements()
        {
            if (currentUDFBBlockElemets != null)
                return currentUDFBBlockElemets;

            return null;
        }
    }
}
