using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000
{
    public partial class BuildTypeForm : XMProForm
    {
        public string selectedSetUpType;
        public BuildTypeForm()
        {
            InitializeComponent();
        }

        private void HandleSetTypeCheckBoxes(bool isXMPRO, bool isXBLD, bool isBoth)
        {
            checkXMPRO.CheckedChanged -= checkXMPRO_CheckedChanged;
            checkXBLD.CheckedChanged -= checkXBLD_CheckedChanged;
            checkBoth.CheckedChanged -= checkBoth_CheckedChanged;

            checkXMPRO.Checked = isXMPRO;
            checkXBLD.Checked = isXBLD;
            checkBoth.Checked = isBoth;

            checkXMPRO.CheckedChanged += checkXMPRO_CheckedChanged;
            checkXBLD.CheckedChanged += checkXBLD_CheckedChanged;
            checkBoth.CheckedChanged += checkBoth_CheckedChanged;
        }

        private void checkXMPRO_CheckedChanged(object sender, EventArgs e)
        {
            HandleSetTypeCheckBoxes(true, false, false);
        }

        private void checkXBLD_CheckedChanged(object sender, EventArgs e)
        {
            HandleSetTypeCheckBoxes(false, true, false);
        }

        private void checkBoth_CheckedChanged(object sender, EventArgs e)
        {
            HandleSetTypeCheckBoxes(false, false, true);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!AnyTypeSelect())
            {
                errorProvider.SetError(buttonSave, "Please select any one option from above.");
                return;
            }
            else
                errorProvider.SetError(buttonSave,string.Empty);
            selectedSetUpType = checkXMPRO.Checked ? "XMPRO" : checkXBLD.Checked ? "XBLD" : "Both";
            this.DialogResult = DialogResult.OK;
        }

        private bool AnyTypeSelect()
        {
            if (checkXMPRO.Checked || checkXBLD.Checked || checkBoth.Checked)
                return true;
            return false;
        }
    }
}
