using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.LadderLogic;

namespace XMPS2000
{
    public partial class UDFBEditOptionsForm : Form
    {
        public enum EditOption
        {
            None,
            EditMainFile,
            CreateLocalCopy
        }

        public EditOption SelectedOption { get; private set; } = EditOption.None;
        public string LocalCopyName { get; private set; } = string.Empty;

        private readonly string _udfbName;

        public UDFBEditOptionsForm(string udfbName)
        {
            _udfbName = udfbName ?? throw new ArgumentNullException(nameof(udfbName));
            InitializeComponent();

            // Set the instruction text with the UDFB name
            lblInstruction.Text = $"How would you like to edit the UDFB '{_udfbName}'?";

            // Set default local copy name
            txtLocalName.Text = $"{_udfbName}_Local";
        }
       
        private void rbEditMain_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEditMain.Checked)
            {
                lblLocalName.Enabled = false;
                txtLocalName.Enabled = false;
                ValidateForm();
            }
        }

        private void rbCreateLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCreateLocal.Checked)
            {
                lblLocalName.Enabled = true;
                txtLocalName.Enabled = true;
                txtLocalName.Focus();
                txtLocalName.SelectAll();
                ValidateForm();
            }
        }

        private void txtLocalName_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void ValidateForm()
        {
            bool isValid = true;

            if (rbCreateLocal.Checked)
            {
                // Validate local copy name
                string localName = txtLocalName.Text.Trim();
                isValid = !string.IsNullOrEmpty(localName) &&
                         localName != _udfbName &&
                         IsValidFileName(localName);
            }

            btnOK.Enabled = isValid;
        }

        private bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            // Check for invalid characters
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                if (fileName.Contains(c.ToString()))
                    return false;
            }

            return true;
        }
        private void txtLocalName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '_' && e.KeyChar != '\b')
            {
                e.Handled = true; 
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rbEditMain.Checked)
            {
                SelectedOption = EditOption.EditMainFile;
                LocalCopyName = string.Empty;
            }
            else if (rbCreateLocal.Checked)
            {
                int usageCount = 0;
                if (XMPS.Instance.LoadedProject.LogicRungs != null)
                {
                    usageCount = XMPS.Instance.LoadedProject.LogicRungs.Where(r => r.OpCodeNm != null && r.OpCodeNm.Equals(_udfbName)).Count();
                }
                if (usageCount > 0)
                {
                    MessageBox.Show($"Cannot create local copy. The UDFB '{_udfbName}' is currently used in logic.\n\nPlease delete all instances from logic first.","UDFB In Use",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }
                SelectedOption = EditOption.CreateLocalCopy;
                LocalCopyName = txtLocalName.Text.Trim();
                if (IsUDFBNameAlreadyExists(LocalCopyName))
                {
                    MessageBox.Show($"A UDFB with the name '{LocalCopyName}' already exists. Please choose a different name.",
                                  "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocalName.Focus();
                    txtLocalName.SelectAll();
                    return;
                }
                if (string.IsNullOrEmpty(LocalCopyName) || LocalCopyName == _udfbName)
                {
                    MessageBox.Show("Please enter a valid name for the local copy that is different from the original UDFB name.",
                                  "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocalName.Focus();
                    return;
                }

                if (!IsValidFileName(LocalCopyName))
                {
                    MessageBox.Show("The name contains invalid characters. Please enter a valid file name.",
                                  "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocalName.Focus();
                    return;
                }

                if (LocalCopyName.Length > 20)
                {
                    MessageBox.Show("The name should not exceed 20 characters as per UDFB naming rules.","Name Too Long", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocalName.Focus();
                    return;
                }
                List<XMIOConfig> oldTags = XMPS.Instance.LoadedProject.Tags.Where(t => !string.IsNullOrEmpty(t.Model) && t.Model.Equals(_udfbName + " Tags")).ToList();
                foreach (var tag in oldTags)
                {
                    tag.Model = LocalCopyName + " Tags";
                }

                var oldScreenKey = XMPS.Instance.LoadedScreens.Where(d => d.Key.Contains($"UDFLadderForm#{_udfbName}")).Select(d => d.Key).FirstOrDefault();
                var oldScreenValue = XMPS.Instance.LoadedScreens.Where(d => d.Key.Contains($"UDFLadderForm#{_udfbName}")).Select(d => d.Value).FirstOrDefault();

                string newScreenKey = $"UDFLadderForm#{LocalCopyName}";

                if (XMPS.Instance.LoadedScreens.ContainsKey(oldScreenKey))
                {
                    XMPS.Instance.LoadedScreens.Remove(oldScreenKey);
                }

                if (!XMPS.Instance.LoadedScreens.ContainsKey(newScreenKey))
                {
                    XMPS.Instance.LoadedScreens.Add(newScreenKey, oldScreenValue);
                }

                if (XMPS.Instance.CurrentScreen.Contains(oldScreenKey))
                {
                    XMPS.Instance.CurrentScreen = newScreenKey;
                }

                if (XMPS.Instance.ScreensToNavigate.Contains(oldScreenKey))
                {
                    XMPS.Instance.ScreensToNavigate.Remove(oldScreenKey);
                }

                if (!XMPS.Instance.ScreensToNavigate.Contains(newScreenKey))
                {
                    XMPS.Instance.ScreensToNavigate.Add(newScreenKey);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private bool IsUDFBNameAlreadyExists(string udfbName)
        {
            if (XMPS.Instance?.LoadedProject?.Blocks == null)
                return false;

            return XMPS.Instance.LoadedProject.Blocks
                .Any(block => block.Type.Equals("UserFunctionBlock", StringComparison.OrdinalIgnoreCase) &&
                             block.Name.Equals(udfbName, StringComparison.OrdinalIgnoreCase));
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ValidateForm();
        }
    }
}
