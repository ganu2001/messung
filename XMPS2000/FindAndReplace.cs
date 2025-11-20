using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.LadderLogic;
using XMPS2000.Resource_File;

namespace XMPS2000
{
    public partial class FindAndReplace : Form
    {
        XMPS xm;
        public LadderWindow _objParent;

        public static List<LadderElement> _findList = new List<LadderElement>();
        public List<LadderDrawing.Attribute> L1 = new List<LadderDrawing.Attribute>();
        public static string _functionblockAttribute;
        FindAndReplaceLogic findAndReplaceLogic;
        public FindAndReplace()
        {
            InitializeComponent();
            this.lblFindWhat.Text = LabelNames.lblFindWhat;
            this.lblReplaceWith.Text = LabelNames.lblReplaceWith;
            this.btnFindNext.Text = LabelNames.btnFindNext;
            this.btnReplace.Text = LabelNames.btnReplace;
            this.btnReplaceAll.Text = LabelNames.btnReplaceAll;
            this.btnCancel.Text = LabelNames.btnCancel;
            xm = XMPS.Instance;
            this.CheckForLogicblock.Text = "Current LogicBlock";
            // Creating String List for storing tag list and Logical address if showLogicalAddress is true for Find 
            List<string> allFindAddress = new List<string>();
            if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
            {
                allFindAddress.AddRange(xm.LoadedProject.Tags.Where(T => T.ShowLogicalAddress == true && !(T.Label.EndsWith("_OR") || T.Label.EndsWith("_OL"))).Select(T => T.LogicalAddress).ToList());
                allFindAddress.AddRange(xm.LoadedProject.Tags.Where(T => T.ShowLogicalAddress == false && !(T.Label.EndsWith("_OR") || T.Label.EndsWith("_OL"))).Select(T => T.Tag).ToList());
            }
            else
            {
                allFindAddress.AddRange(xm.LoadedProject.Tags.Where(T => T.ShowLogicalAddress == true).Select(T => T.LogicalAddress).ToList());
                allFindAddress.AddRange(xm.LoadedProject.Tags.Where(T => T.ShowLogicalAddress == false).Select(T => T.Tag).ToList());
            }   
            Findcmbox.DataSource = allFindAddress;
            List<string> allReplaceAddress = new List<string>();
            if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
            {
                allReplaceAddress.AddRange(xm.LoadedProject.Tags.Where(T => T.ShowLogicalAddress == true && !(T.Label.EndsWith("_OR") || T.Label.EndsWith("_OL"))).Select(T => T.LogicalAddress).ToList());
                allReplaceAddress.AddRange(xm.LoadedProject.Tags.Where(T => T.ShowLogicalAddress == false && !(T.Label.EndsWith("_OR") || T.Label.EndsWith("_OL"))).Select(T => T.Tag).ToList());
            }
            else
            {
                allReplaceAddress.AddRange(xm.LoadedProject.Tags.Where(T => T.ShowLogicalAddress == true).Select(T => T.LogicalAddress).ToList());
                allReplaceAddress.AddRange(xm.LoadedProject.Tags.Where(T => T.ShowLogicalAddress == false).Select(T => T.Tag).ToList());
            }
            Replacecmbox.DataSource = allReplaceAddress;
            var currScreen = xm.CurrentScreen;
            if (currScreen.Contains("Ladder"))
            {
                var currScreenWindow = (LadderWindow)xm.LoadedScreens[currScreen];
                _objParent = currScreenWindow;
                findAndReplaceLogic = new FindAndReplaceLogic(xm, _objParent);
            }
        }
        private void btnFindNext_Click(object sender, EventArgs e)
        {
            findAndReplaceLogic.blkWiseCount.Clear();
            string findText = Findcmbox.Text;
            string logicBlockCheck = CheckForLogicblock.Text;
            if (CheckForLogicblock.Text == "Current LogicBlock")
            {
                findAndReplaceLogic.FindNext(findText, logicBlockCheck,false);
                _objParent.Refresh();
            }
            else if(CheckForLogicblock.Text == "Entire LogicBlock")
            {
                findAndReplaceLogic.FindForEntireLogicBlock(findText,  false);
                _objParent.Refresh();
            }
            else if (CheckForLogicblock.Text == "Entire Project")
            {
                xm.FindDevicesList = new List<Tuple<string, string>>();
                xm.FindInMainBlockList = new List<Tuple<string, string>>();
                findAndReplaceLogic.FindInOtherLocation(findText);
                findAndReplaceLogic.FindInMainBlock(findText,  false);
                findAndReplaceLogic.FindForEntireLogicBlock(findText, false);
                _objParent.Refresh();
            }
        }
        private void btnReplace_Click(object sender, EventArgs e)
        {
            string FindcmbText = this.Findcmbox.Text;
            string ReplaceText = this.Replacecmbox.Text;
            if(CheckForLogicblock.Text == "Entire Project")
            {
                MessageBox.Show("Entire Project functionality is applicable only for Replace All", "XMPS2000", MessageBoxButtons.OK);
                return;
            }
            findAndReplaceLogic.ReplaceText(FindcmbText, ReplaceText);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            string FindcmbText = this.Findcmbox.Text;
            string ReplaceText = this.Replacecmbox.Text;
            findAndReplaceLogic.ReplaceAllText(FindcmbText, ReplaceText,false, null, CheckForLogicblock.Text.Equals("Entire Project"));
        }
        private void FindAndReplace_FormClosing(object sender, FormClosingEventArgs e)
        {
            _findList.Clear();
            xm.FindList.Clear();
            _objParent.Refresh();
            LadderCanvas.RefreshCanvas();
            xm.FindList.Clear();
        }
        private void CheckForLogicblock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (findAndReplaceLogic != null)
                findAndReplaceLogic.ResetNextButtonCount();
        }

        private void Findcmbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (findAndReplaceLogic != null)
                findAndReplaceLogic.ResetNextButtonCount();
        }
    }
}
