using LadderEditorLib.MementoDesign;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using XMPS2000.Core;
namespace LadderDrawing.UserControls
{
    public partial class LadderEditorControl : UserControl
    {
        public event EventHandler<DragEventArgs> ItemDropped;
        public event EventHandler<MouseEventArgs> ItemClicked;
        public event EventHandler<KeyEventArgs> ItemDeleted;
        public event EventHandler<CancelEventArgs> TextValidation;
        public event EventHandler<EventArgs> MainLogicBlockRefresh;
        public event EventHandler<EventArgs> CrossReferanceClicked;

        public int VerticalScrollValue = 0;
        public bool ValidText { set; get; }
        //checking if rung is pasted 
        public bool isPasted = false;
        public static Dictionary<LadderElement, LadderElement> OldVsNewElements = new Dictionary<LadderElement, LadderElement>();
        public LadderEditorControl()
        {
            InitializeComponent();
            ladderCanvas1.OnDoubleClickEvent += LadderCanvas1_OnDoubleClickEvent;
            ladderCanvas1.ItemDeleted += ladderCanvas1_ItemDeleted;
            ladderCanvas1.MainLogicRefresh += MainLadderRefresh;
            ladderCanvas1.OnDragCreate += LadderCanvas1_OnDragCreate;
            ladderCanvas1.CrossReferanceClicked += CrossReferanceRequired;

            this.DoubleBuffered = true;
            // Scroll event
            this.MouseWheel += LadderEditorControl_MouseWheel;
        }
        private void MainLadderRefresh(object sender, EventArgs e)
        {
            MainLogicBlockRefresh(LadderDesign.ClickedElement, e);
        }
        private void CrossReferanceRequired(object sender, EventArgs e)
        {
            if (LadderDesign.ClickedElement != null)
            {
                CrossReferanceClicked(this.ParentForm, e);
            }
        }
         
        public LadderCanvas getCanvas()
        {
            return ladderCanvas1;
        }
        private void LadderCanvas1_OnDragCreate(object sender, DragEventArgs e)
        {
            LadderDesign.ClickedElement = (LadderElement)sender;
            if (ItemDropped != null)
            {
                ItemDropped(sender, e);
            }
        }
       

       
        private bool CheckInRung(LadderElement ld, ref LadderElement element)
        {
            if (element.Attributes["deletedStatus"].ToString().Equals("NegationEle"))
            {
                ld.Negation = true;
            }
            else
            {
                Attribute pnStatus = new Attribute();
                pnStatus.Name = "PNStatus";
                pnStatus.Value = element.Attributes["deletedStatus"].ToString();
                ld.Attributes.Add(pnStatus);
            }
            element.Attributes.RemoveAll(T => T.Name == "deletedStatus");
            LadderCanvas.RefreshCanvas();
            return true;
        }

        private bool CheckInDummyParalled(LadderElement ld, ref LadderElement element)
        {
            return CheckElementInHierarchy(ld, element);
        }

        private bool CheckElementInHierarchy(LadderElement parentElement, LadderElement element)
        {
            if (parentElement.Attributes["caption"] == element.Attributes["caption"])
            {
                if (element.Attributes["deletedStatus"].ToString().Equals("NegationEle"))
                {
                    parentElement.Negation = true;
                }
                else
                {
                    Attribute pnStatus = new Attribute();
                    pnStatus.Name = "PNStatus";
                    pnStatus.Value = element.Attributes["deletedStatus"].ToString();
                    parentElement.Attributes.Add(pnStatus);
                }
                element.Attributes.RemoveAll(T => T.Name == "deletedStatus");
                LadderCanvas.RefreshCanvas();
                return true;
            }
            foreach (LadderElement childElement in parentElement.Elements)
            {
                if (childElement.Elements.Count > 0)
                {
                    if (CheckElementInHierarchy(childElement, element))
                        return true;
                }
                else
                {
                    if (childElement.Attributes["caption"] == element.Attributes["caption"])
                    {
                        if (element.Attributes["deletedStatus"].ToString().Equals("NegationEle"))
                        {
                            childElement.Negation = true;
                        }
                        else
                        {
                            Attribute pnStatus = new Attribute();
                            pnStatus.Name = "PNStatus";
                            pnStatus.Value = element.Attributes["deletedStatus"].ToString();
                            childElement.Attributes.Add(pnStatus);
                        }
                        element.Attributes.RemoveAll(T => T.Name == "deletedStatus");
                        LadderCanvas.RefreshCanvas();
                        return true;
                    }
                }
            }
            return false;
        }
        //Taking common method to null active variable after redo perform.
        public void nullUsedvariable(string ele)
        {
            ele = null;
            LadderDesign.Active.pnStatus = null;
            LadderDesign.Active.negation = false;
        }
        /// <summary>
        /// Taking common method to Refresh Ladder canvas
        /// </summary>
        private void UpdateCanvasAndRefresh()
        {
            LadderCanvas.RefreshCanvas();
            ReScale();
            ladderCanvas1.Update();
            ladderCanvas1.Refresh();
        }
        private void LadderCanvas1_OnDoubleClickEvent(object sender, MouseEventArgs e)
        {
            if (this.ItemClicked != null)
                ItemClicked(LadderDesign.ClickedElement, e);
        }
        private void ladderCanvas1_ItemDeleted(object sender, KeyEventArgs e)
        {
            if (this.ItemDeleted != null)
            {
                if (e.KeyData == Keys.Execute)
                    ItemDeleted(LadderDesign.ClickedElement.getRoot(), e);
                else
                    ItemDeleted(LadderDesign.ClickedElement, e);
            }
        }
        public void ReScale(bool paintstate = false, int visibleRung = 0)
        {
            int scrollValue = LadderDesign.ClickedElement != null ? VerticalScroll.Value : (visibleRung == 0 ? LadderDesign.currentRungScroll : visibleRung);
            LadderDesign design = ladderCanvas1.getDesignView();
            int ycord = 30;
            int xcord = 30;
            for (int i = 0; i < design.Elements.Count; i++)
            {
                design.Elements[i].Position.Y = ycord;
                design.Elements[i].Position.X = 0;
                ycord += (int)(design.Elements[i].getHeight()) + (LadderDesign.ControlHeight / 2);
                xcord = design.Elements[i].getWidth();
                if (ladderCanvas1.Width < xcord)
                    ladderCanvas1.Width = 1000 + xcord + (LadderDesign.ControlSpacing / 2);
            }
            // Increase the height by twice if elements are going beyond the canvas height
            if (ladderCanvas1.Height < ycord)
            {
                ladderCanvas1.Height = ycord * 2;
            }
            design.PopulateHeightList();
            if (isPasted)
            {
                scrollValue = design.m_Height_dic.Last().Key - 450;
                isPasted = false;
            }
            ladderCanvas1.Refresh();
            //// Set following value twice. Ref - https://stackoverflow.com/questions/757408/c-sharp-usercontrol-verticalscroll-value-not-being-set
            //if ((scrollValue < VerticalScroll.Maximum) && (scrollValue > VerticalScroll.Minimum))
            //{
            //    VerticalScroll.Value = scrollValue;
            //    VerticalScroll.Value = scrollValue;
            //}

            // Clamp the scroll value to valid range
            int clampedScrollValue = Math.Max(VerticalScroll.Minimum,
                                      Math.Min(scrollValue, VerticalScroll.Maximum));
            VerticalScroll.Value = clampedScrollValue;
            // Store the actual requested value, even if out of range
            VerticalScrollValue = scrollValue;
        }
        public void ChaneVisibleRungs(Point erroredRungPosition)
        {
            ReScale(true, erroredRungPosition.Y);
        }
        public void InsertBlankRung()
        {
            ladderCanvas1.getDesignView().InsertBlankRung();
            ReScale();
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

        public void InsertCoil()
        {
            if (!ValidateUDFBEditPermission())
                return;
            LadderElement newelement = ladderCanvas1.getDesignView().InsertCoil();
            ReScale();
            LadderDesign.ClickedElement = null;
        }
        public void InsertRung(int Rungno = 0)
        {
            ((SplitContainer)this.Parent.Parent.Parent).Panel2Collapsed = true;
            ladderCanvas1.getDesignView().InsertRung(Rungno);
            ladderCanvas1.getDesignView().SetStateForUndoRedo();
            ReScale();
            XMPS.Instance.MarkProjectModified(true);
            //showing focus on rung which is newely added.
            if (Rungno == 0)
            {
                LadderElement ladderElement = ladderCanvas1.getDesignView().m_Height_dic.Last().Value;
                ladderCanvas1.getDesignView().Elements.HighlightRungElements(ladderCanvas1, ladderElement, true);
            }
        }
        public LadderElement InsertNewBlock(string BlockName, int Rungno = 0)
        {
            LadderElement lastContact = ladderCanvas1.getDesignView().InsertNewBlock(BlockName, Rungno);
            ReScale();
            return lastContact;
        }
        public void InsertContactParallel()
        {
            if (!ValidateUDFBEditPermission())
                return;
            if (LadderDesign.ClickedElement != null && LadderDesign.ClickedElement.Position.RelateTo.Count == 0 && LadderDesign.ClickedElement.Position.Parent.Position.ConnectTo.Count == 0)
            {
                LadderDesign design = ladderCanvas1.getDesignView();
                LadderElement element = LadderDesign.ClickedElement;
                List<LadderElement> elements = ladderCanvas1.selectedElements;
                // Check for selected elements on different rung+
                foreach (LadderElement _element in elements)
                    if (_element.Position.Parent.Position.Index != LadderDesign.ClickedElement.Position.Parent.Position.Index)
                    {
                        ladderCanvas1.selectedElements.Clear();
                        LadderDesign.ClickedElement = null;
                        ladderCanvas1.selectedElements.Clear();
                        return;
                    }
                        
                List<LadderElement> elementsList = elements;
                if (elements.Count() > 0)
                    elementsList = CheckAllElements(elements);
                design.InsertContactParallelNew(ref element, ref elementsList);
                ReScale();
                Global.SelectActive(LadderDesign.ClickedElement.getRoot(), ladderCanvas1.getGraphics());
                ladderCanvas1.selectedElements.Clear();
                LadderDesign.ClickedElement = null;
                ladderCanvas1.selectedElements.Clear();
                ReScale();
                LadderDesign.currentRungScroll = VerticalScrollValue;
            }
        }
        private List<LadderElement> CheckAllElements(List<LadderElement> elements)
        {
            List<LadderElement> newList = elements;
            LadderElement root = elements[0].getRoot();
            int minElementIndex = 100, maxElementIndex = 0;
            foreach (LadderElement ladderElement in elements)
            {
                if (maxElementIndex < ladderElement.Position.Index)
                    maxElementIndex = ladderElement.Position.Index;
                if (minElementIndex > ladderElement.Position.Index)
                    minElementIndex = ladderElement.Position.Index;
            }
            newList.Clear();
            for (int index = minElementIndex; index <= maxElementIndex; index++)
            {
                if (root.Elements[index].customDrawing.toString() == "Contact")
                    newList.Add(root.Elements[index]);
            }
            return newList;
        }
        public LadderElement InsertContactBefore()
        {
            if (!ValidateUDFBEditPermission())
                return null;
            LadderDesign design = ladderCanvas1.getDesignView();
            LadderElement element = LadderDesign.ClickedElement;
            LadderElement addedElement = design.InsertContactBefore(ref element);
            RefreshCanvas();
            return addedElement;
        }
        public void NegateStyle()
        {
            LadderDesign design = ladderCanvas1.getDesignView();
            if (LadderDesign.ClickedElement == null)
            {
                LadderDesign.ClickedElement = LadderDesign.PNStatusElement;
            }
            LadderElement element = LadderDesign.ClickedElement;
            design.NegateStyle(ref element);
            ladderCanvas1.Invalidate();
            LadderDesign.PNStatusElement = LadderDesign.ClickedElement;
            LadderDesign.ClickedElement = null;
            RefreshCanvas();
            if (LadderDesign.PNStatusElement != null)
            {
                ICustomDrawing customDrawing = LadderDesign.PNStatusElement.customDrawing;
                customDrawing.OnSelect(ladderCanvas1.getGraphics(), LadderDesign.PNStatusElement);
            }
        }
        public void InsertContactAfter()
        {
            if (!ValidateUDFBEditPermission())
                return;
            LadderDesign design = ladderCanvas1.getDesignView();
            LadderElement element = LadderDesign.ClickedElement;
            design.InsertContactAfter(ref element);
            RefreshCanvas();
        }
        public void InsertFBBefore()
        {
            LadderDesign design = ladderCanvas1.getDesignView();
            LadderElement element = LadderDesign.ClickedElement;
            design.InsertFBBefore(ref element);
            ReScale();
            LadderDesign.ClickedElement = null;
            ladderCanvas1.selectedElements.Clear();
        }
        public LadderElement InsertFBAfter()
        {
            if (!ValidateUDFBEditPermission())
                return null;
            LadderDesign design = ladderCanvas1.getDesignView();
            LadderElement element = LadderDesign.ClickedElement;
            if (LadderDesign.ClickedElement != null)
            {
                LadderElement newelement = design.InsertFBAfter(ref element);
                ReScale();
                Invalidate();
                ReScale();
                Update();
                LadderDesign.PrevClickedElement = newelement;
                LadderDesign.ClickedElement = null;
                return newelement;
            }
            LadderDesign.ClickedElement = null;
            ladderCanvas1.selectedElements.Clear();
            return null;
        }
        private void LadderEditorControl_Scroll(object sender, ScrollEventArgs e)
        {
            VerticalScrollValue = VerticalScroll.Value;
        }
        private void LadderEditorControl_MouseWheel(object sender, MouseEventArgs e)
        {
            VerticalScrollValue = VerticalScroll.Value;
        }
        public void UpdateSetStatus()
        {
            LadderDesign design = ladderCanvas1.getDesignView();
            if (LadderDesign.ClickedElement == null)
            {
                LadderDesign.ClickedElement = LadderDesign.PNStatusElement;
            }
            LadderElement element = LadderDesign.ClickedElement;
            design.UpdateSetStatus(ref element);
            LadderDesign.PNStatusElement = element;
            RefreshCanvas();
            if (LadderDesign.PNStatusElement != null)
            {
                ICustomDrawing customDrawing = LadderDesign.PNStatusElement.customDrawing;
                customDrawing.OnSelect(ladderCanvas1.getGraphics(), LadderDesign.PNStatusElement);
            }
        }
        public void UpdateResetStatus()
        {
            LadderDesign design = ladderCanvas1.getDesignView();
            if (LadderDesign.ClickedElement == null)
            {
                LadderDesign.ClickedElement = LadderDesign.PNStatusElement;
            }
            LadderElement element = LadderDesign.ClickedElement;
            design.UpdateResetStatus(ref element);
            LadderDesign.PNStatusElement = element;
            // Commenting Reset for UndoRedo LadderLogic
            RefreshCanvas();
            if (LadderDesign.PNStatusElement != null)
            {
                ICustomDrawing customDrawing = LadderDesign.PNStatusElement.customDrawing;
                customDrawing.OnSelect(ladderCanvas1.getGraphics(), LadderDesign.PNStatusElement);
            }
        }
        public void UpdatePNStatus()
        {
            LadderDesign design = ladderCanvas1.getDesignView();
            if(LadderDesign.ClickedElement == null)
            {
                LadderDesign.ClickedElement = LadderDesign.PNStatusElement;
            }
            LadderElement element = LadderDesign.ClickedElement;
            design.UpdatePNStatus(ref element);
            LadderDesign.PNStatusElement = element;
            RefreshCanvas();
            if(LadderDesign.PNStatusElement != null)
            {
                ICustomDrawing customDrawing = LadderDesign.PNStatusElement.customDrawing;
                customDrawing.OnSelect(ladderCanvas1.getGraphics(), LadderDesign.PNStatusElement);
            }
        }
        public void ClearStatus()
        {
            LadderDesign design = ladderCanvas1.getDesignView();
            LadderElement element = LadderDesign.ClickedElement;
            design.ClearStatus(ref element);
            RefreshCanvas();
        }
        /// <summary>
        /// taking common method to refresh ladder canvas
        /// </summary>
        public void RefreshCanvas()
        {
            ReScale();
            ladderCanvas1.Update();
            ladderCanvas1.Refresh();
            LadderDesign.ClickedElement = null;
            ladderCanvas1.selectedElements.Clear();
        }
        /// <summary>
        /// //Do not remove this function override for stable cursor on screen
        /// </summary>
        /// <param name="activeControl"></param>
        /// <returns>Returns the location where click is fired and shows exact same location</returns>
        protected override Point ScrollToControl(Control activeControl)
        {
            return this.DisplayRectangle.Location;
        }
    }
}
