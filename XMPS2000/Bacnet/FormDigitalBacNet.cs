using System;
using System.ComponentModel;
using System.Windows.Forms;
using XMPS2000.Core.BacNet;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using System.Linq;
using System.Drawing.Text;
using static iTextSharp.text.pdf.PRTokeniser;
using System.Reflection;
using System.Collections.Generic;

namespace XMPS2000.Bacnet
{
    public partial class FormDigitalBacNet : Form, IXMForm
    {
        private string tagname;
        private string logicaladdress;
        private string tagType;
        private readonly bool _isReadOnly;
        List<string> tagsToAdd = new List<string>();
        XMPS xm = XMPS.Instance;
        public FormDigitalBacNet(XMIOConfig ioconfig, bool isReadOnly = false)
        {
            InitializeComponent();
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            tagname = ioconfig.Tag.ToString();
            txtInactive.MaxLength = 20;
            txtActive.MaxLength = 20;
            comboboxFeedbackValue.Items.Clear();
            if (ioconfig.Label == "Bool")
            {
                ChangeTheOtherControlUpward();
                ShowRelinquishInput(true);
                DisablePolarityFromBinaryValue();
                labelfeedbackval.Text = "Alarm Value";
                labelDeviceType.Visible = false;
                label10.Visible = false;
            }
            else
            {
                if (ioconfig.Type.ToString().EndsWith("Input"))
                {
                    labelfeedbackval.Text = "Alarm Value";
                    ShowRelinquishInput(false);
                }
                else
                    ShowRelinquishInput(true);
            }
            lblInitialValue.Visible = ioconfig.Type.ToString().EndsWith("Input") ? false : true;
            comboBoxInitialValue.Visible = ioconfig.Type.ToString().EndsWith("Input") ? false : true;
            cmbRelinquishDefault.Visible = ioconfig.Type.ToString().EndsWith("Input") ? false : true;
            label16.Top = label15.Top + 58;
            grpeventenb.Top = comboNotifyclass.Top + 55;
            grpnotifytype.Top = grpeventenb.Top + 60;
            label17.Top = label16.Top + 60;
            textObjectName.Text = tagname;
            logicaladdress = ioconfig.LogicalAddress;
            tagType = ioconfig.Type.ToString();
            AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            XMPS.Instance.BacNetCurrentScreen = "FormDigitalBacNet";
            _isReadOnly = isReadOnly;
            ApplyReadOnly();
        }
        private void ApplyReadOnly()
        {
            if (_isReadOnly)
            {
                textObjectName.Enabled = false;
                textDescription.Enabled = false;
                textReDefault.Enabled = false;
                comboPolarity.Enabled = false;
                txtInactive.Enabled = false;
                txtActive.Enabled = false;
                checkEnable.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                cmbRelinquishDefault.Enabled = false;
            }
        }
        private void DisablePolarityFromBinaryValue()
        {
            label12.Visible = false;
            comboPolarity.Visible = false;
            foreach (Control control in othervaluesgrpbox.Controls)
            {
                control.Top = control.Top - 30;
            }
            lblInitialValue.Top = lblInitialValue.Top - 30;
            comboBoxInitialValue.Top = comboBoxInitialValue.Top - 30;
            btnSave.Top = btnSave.Top - 30;
            btnCancel.Top = btnCancel.Top - 30;
        }

        private void ChangeTheOtherControlUpward()
        {
            //ObjectName Discription and Lable
            label7.Top = label7.Top - 30;
            textObjectName.Top = textObjectName.Top - 30;

            //Discription lable and textBox
            label9.Top = label9.Top - 30;
            textDescription.Top = textDescription.Top - 30;

            labelReqdefault.Top = labelReqdefault.Top - 30;
            //textReDefault.Top = textReDefault.Top - 30;
            cmbRelinquishDefault.Top = cmbRelinquishDefault.Top - 30;

            othervaluesgrpbox.Top = othervaluesgrpbox.Top - 60;

            lblInitialValue.Top = lblInitialValue.Top - 30;
            comboBoxInitialValue.Top = comboBoxInitialValue.Top - 30;
            //btnSave.Top = btnSave.Top - 30;
            //btnCancel.Top = btnCancel.Top - 30;
        }

        private void ShowRelinquishInput(bool show)
        {
            othervaluesgrpbox.Top = show ? cmbRelinquishDefault.Top + 30 : cmbRelinquishDefault.Top;
        }
        private void ConfigureRelinquishDefaultControl()
        {
            bool isBinaryValue = labelObjType.Text.Contains("Binary Value");
            bool isBinaryOutput = labelObjType.Text.Contains("Binary Output");
            textReDefault.Visible = false;
            cmbRelinquishDefault.Visible = false;
            labelReqdefault.Visible = false;
            if (isBinaryValue)
            {
                textReDefault.Visible = true;
                labelReqdefault.Visible = true;
                textReDefault.Location = cmbRelinquishDefault.Location;
                textReDefault.Width = cmbRelinquishDefault.Width;
            }
            else if (isBinaryOutput)
            {
                cmbRelinquishDefault.Visible = true;
                labelReqdefault.Visible = true;
            }
        }
        /// <summary>
        /// Get data from list and bind it to controls
        /// </summary>
        private void AssignValues()
        {
            BinaryIOV binaryIOV = XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(b => b.LogicalAddress == logicaladdress).FirstOrDefault();
            if (binaryIOV == null) return;
            labelObjIdentifier.Text = binaryIOV.ObjectIdentifier.ToString();
            labelInstanceno.Text = binaryIOV.InstanceNumber.ToString();
            labelObjType.Text = binaryIOV.ObjectType;
            ConfigureRelinquishDefaultControl();
            labelDeviceType.Text = tagType.StartsWith("Universal") ?
                                    XMPS.Instance.LoadedProject.Tags
                                   .FirstOrDefault(t => t.LogicalAddress.Equals(logicaladdress))?.Mode
                                   : binaryIOV.DeviceType;
            textObjectName.Text = binaryIOV.ObjectName;
            textDescription.Text = binaryIOV.Description;
            logicaladdress = binaryIOV.LogicalAddress;
            textReDefault.Text = binaryIOV.RelinquishDefault.ToString();
            comboPolarity.SelectedIndex = binaryIOV.Polarity;
            textTimeDelay.Text = binaryIOV.TimeDelay.ToString();
            textTimeDelayNormal.Text = binaryIOV.TimeDelayNormal.ToString();
            checkEnable.Checked = binaryIOV.EventDetectionEnable == 1 ? true : false;
            txtActive.Text = binaryIOV.ActiveText == null ? "true" : binaryIOV.ActiveText;
            txtInactive.Text = binaryIOV.InactiveText == null ? "false" : binaryIOV.InactiveText;
            comboboxFeedbackValue.Items.Clear();
            comboBoxInitialValue.Items.Clear();
            cmbRelinquishDefault.Items.Clear();
            //Inactive
            cmbRelinquishDefault.Items.Add(!string.IsNullOrEmpty(binaryIOV.InactiveText) ? binaryIOV.InactiveText : txtInactive.Text);
            comboBoxInitialValue.Items.Add(!string.IsNullOrEmpty(binaryIOV.InactiveText) ? binaryIOV.InactiveText : txtInactive.Text);
            //Active
            cmbRelinquishDefault.Items.Add(!string.IsNullOrEmpty(binaryIOV.ActiveText) ? binaryIOV.ActiveText : txtActive.Text);
            comboBoxInitialValue.Items.Add(!string.IsNullOrEmpty(binaryIOV.ActiveText) ? binaryIOV.ActiveText : txtActive.Text);
            DisplayInitialValue(ref binaryIOV);

            if (labelObjType.Text.Contains("Binary Output"))
            {
                
                tagsToAdd = xm.LoadedProject.Tags
                        .Where(t =>
                            t.Label == "Bool"
                            || t.Type == Core.Types.IOType.DigitalInput
                            || t.Type == Core.Types.IOType.DigitalOutput
                            || (((t.Type == Core.Types.IOType.UniversalInput || t.Type == Core.Types.IOType.UniversalOutput || t.Type == Core.Types.IOType.AnalogInput || t.Type == Core.Types.IOType.AnalogOutput) && t.Mode == "Digital")
                                && !(t.Label.EndsWith("OR") || t.Label.EndsWith("OL")))
                        )
                        .Select(t => t.Tag)
                         .OrderBy(tag => tag)
                        .ToList();
                comboboxFeedbackValue.Items.Insert(0, "");
                comboboxFeedbackValue.Items.Insert(1, !string.IsNullOrEmpty(binaryIOV.InactiveText) ? binaryIOV.InactiveText : txtInactive.Text);
                comboboxFeedbackValue.Items.Insert(2, !string.IsNullOrEmpty(binaryIOV.ActiveText) ? binaryIOV.ActiveText : txtActive.Text);
                comboboxFeedbackValue.Items.AddRange(tagsToAdd.ToArray());
                string tagName = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == binaryIOV.TagValue).Select(t => t.Tag).FirstOrDefault();
                comboboxFeedbackValue.Text = tagName;
                if (!string.IsNullOrEmpty(binaryIOV.SelectedFeedbackText))
                {
                    comboboxFeedbackValue.Text = binaryIOV.SelectedFeedbackText;
                }
                else
                {
                    comboboxFeedbackValue.Text = "";
                }
            }
            else
            {
                comboboxFeedbackValue.Items.Add(!string.IsNullOrEmpty(binaryIOV.InactiveText) ? binaryIOV.InactiveText : txtInactive.Text);
                comboboxFeedbackValue.Items.Add(!string.IsNullOrEmpty(binaryIOV.ActiveText) ? binaryIOV.ActiveText : txtActive.Text);
                comboboxFeedbackValue.Items.Insert(0, "");

                if (!string.IsNullOrEmpty(binaryIOV.SelectedFeedbackText))
                {
                    comboboxFeedbackValue.Text = binaryIOV.SelectedFeedbackText;
                }
                else
                {
                    comboboxFeedbackValue.Text = "";
                }
            }
            //comboboxFeedbackValue.SelectedText = binaryIOV.FeedbackValue.ToString();
            comboNotifyclass.DataSource = XMPS.Instance.LoadedProject.BacNetIP.Notifications.ToList();
            comboNotifyclass.DisplayMember = "ObjectName";
            comboNotifyclass.ValueMember = "InstanceNumber";
            comboNotifyclass.SelectedValue = binaryIOV.NotificationClass.ToString();

            CheckEventEnableCheckBoxes(binaryIOV.EventEnable);
            CheckNotificationEnableCheckBoxes(binaryIOV.NotifyType);
            var binaryOnlyList = XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(x => x.ObjectType.Contains("Binary Value")).ToList();
            binaryOnlyList.Insert(0, new BinaryIOV { ObjectName = "Select Inhibit Binary Value", InstanceNumber = "" });
            cmb_BinaryValue.DataSource = binaryOnlyList;
            cmb_BinaryValue.DisplayMember = "ObjectName";
            cmb_BinaryValue.ValueMember = "InstanceNumber";
            cmb_BinaryValue.SelectedIndex = 0;
            cmb_BinaryValue.SelectedIndex = binaryIOV.BinaryBValue;
        }


        private void DisplayInitialValue(ref BinaryIOV binaryIOV)
        {
            string initialvalue = XMPS.Instance.LoadedProject.Tags
                  .FirstOrDefault(t => t.LogicalAddress.Equals(logicaladdress))?.InitialValue;
            comboBoxInitialValue.SelectedIndex = initialvalue == "1" ? 1 : 0;
            cmbRelinquishDefault.SelectedIndex = initialvalue == "1" ? 1 : 0;
        }
        public void OnShown()
        {
            textObjectName.Text = tagname;
        }

        private void checkEnable_CheckedChanged(object sender, EventArgs e)
        {
            textTimeDelay.Enabled = checkEnable.Checked;
            textTimeDelayNormal.Enabled = checkEnable.Checked;
            comboBoxInitialValue.Enabled = checkEnable.Checked;
            //cmbRelinquishDefault.Enabled = checkEnable.Checked;
            //comboboxFeedbackValue.Enabled = checkEnable.Checked;
            comboNotifyclass.Enabled = checkEnable.Checked;
            grpeventenb.Enabled = checkEnable.Checked;
            grpnotifytype.Enabled = checkEnable.Checked;
            cmb_BinaryValue.Enabled = checkEnable.Checked;
            //Showing Event Enable selected 
            if (checkEnable.Checked)
                EventEnableCheckBoxDefault();
        }

        private void EventEnableCheckBoxDefault()
        {
            checktooffNormal.Checked = checkEnable.Checked;
            checkToFault.Checked = checkEnable.Checked;
            checkToNormal.Checked = checkEnable.Checked;
        }
        private void CheckNotificationEnableCheckBoxes(int notifyType)
        {
            checkAlarm.Checked = notifyType == 0;
            checkEvent.Checked = notifyType == 1;
        }
        private void CheckEventEnableCheckBoxes(int eventEnable)
        {
            string checkboxesVal = Convert.ToString(eventEnable, 2).PadLeft(3, '0');
            checktooffNormal.Checked = checkboxesVal[0] == '1';
            checkToFault.Checked = checkboxesVal[1] == '1';
            checkToNormal.Checked = checkboxesVal[2] == '1';
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            ((FormBacNet)this.ParentForm).RefreshGridView();
        }

        private void textReDefault_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateBacNetInput(textReDefault, e, "Bool");
        }
        /// <summary>
        /// Validate operands depending on the type of control 
        /// </summary>
        /// <param name="control">Name of the control from whoes validate this call is generated.</param>
        /// <param name="e">This parameter will specify whether to add or update the line.</param>
        /// <returns>Describe return value.</returns>
        private void ValidateBacNetInput(TextBox control, CancelEventArgs e, string dataType)
        {
            bool validationSuccessful = true;
            string error = string.Empty;
            if (!string.IsNullOrEmpty(control.Text))
                validationSuccessful = BacNetValidator.ValidateBacNetInputVal(control.Text, out error, dataType);
            e.Cancel = !validationSuccessful;
            errorProvider.SetError(control, validationSuccessful ? null : error);
        }

        private void ValidateBacNetDigitalInput(ComboBox control, CancelEventArgs e, string dataType)
        {
            bool validationSuccessful = true;
            string error = string.Empty;
            string value = "0";
            if ((control.Text != null && control.Text != "") && control.Text == this.txtActive.Text)
            {
                value = "1";
            }
            else if ((control.Text != null && control.Text != "") && control.Text == this.txtInactive.Text)
            {
                value = "0";
            }
            if (!string.IsNullOrEmpty(/*control.Text*/value))
                validationSuccessful = BacNetValidator.ValidateBacNetInputVal(value, out error, dataType);
            e.Cancel = !validationSuccessful;
            errorProvider.SetError(control, validationSuccessful ? null : error);
        }
        private void textTimeDelay_Validating(object sender, CancelEventArgs e)
        {
            if (!checkEnable.Checked)
                return;
            ValidateBacNetInput(textTimeDelay, e, "UDINT");
        }
        private void textTimeDelayNormal_Validating(object sender, CancelEventArgs e)
        {
            if (!checkEnable.Checked)
                return;
            ValidateBacNetInput(textTimeDelayNormal, e, "UDINT");
        }
        private void comboboxFeedbackValue_Validating(object sender, CancelEventArgs e)
        {
            if (!checkEnable.Checked)
                return;
            ValidateBacNetDigitalInput(comboboxFeedbackValue, e, "Bool");
            if(this.comboboxFeedbackValue.SelectedItem == null)
            {
                errorProvider.SetError(comboboxFeedbackValue, "Pleese select value");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(comboboxFeedbackValue, null);
                e.Cancel = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges(sender, e);
        }

        private bool SaveChanges(object sender, EventArgs e)
        {
            var dublicateObject = XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.FirstOrDefault(t => t.ObjectName.Equals(textObjectName.Text.ToString().Trim()));
            if (BacNetFormFactory.ValidateObjectName(textObjectName.Text.ToString().Trim(), "BinaryIOValues") && dublicateObject?.ObjectIdentifier != labelObjIdentifier.Text.ToString())
            {
                MessageBox.Show("Object name is already used, change the name and try again ...", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtActive.Text == txtInactive.Text && !string.IsNullOrEmpty(txtActive.Text))
            {
                MessageBox.Show("The values of Active and Inactive text boxes cannot be the same. Please change one of them before saving.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtActive.Focus();
                return false; // Prevent save
            }
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            BinaryIOV binaryIOV = new BinaryIOV();
            BacNetIP bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            binaryIOV.ObjectIdentifier = labelObjIdentifier.Text.ToString();
            binaryIOV.InstanceNumber = labelInstanceno.Text.ToString();
            binaryIOV.ObjectType = labelObjType.Text.ToString();
            binaryIOV.DeviceType = labelDeviceType.Text.ToString();
            binaryIOV.ObjectName = textObjectName.Text.Trim();
            binaryIOV.Description = textDescription.Text.Trim();
            binaryIOV.LogicalAddress = logicaladdress;
            binaryIOV.BinaryBValue = cmb_BinaryValue.SelectedIndex;
            binaryIOV.RelinquishDefault = Convert.ToInt32(cmbRelinquishDefault.Text.Equals(txtActive.Text) ? "1" : "0");
            binaryIOV.EventDetectionEnable = checkEnable.Checked ? 1 : 0;
            binaryIOV.TimeDelay = checkEnable.Checked ? Convert.ToInt64(textTimeDelay.Text.ToString()) : 0;
            binaryIOV.TimeDelayNormal = checkEnable.Checked ? Convert.ToInt64(textTimeDelayNormal.Text.ToString()) : 0;
            binaryIOV.NotificationClass = checkEnable.Checked ? Convert.ToInt32(comboNotifyclass.SelectedValue) : 0;
            binaryIOV.Polarity = Convert.ToInt32(comboPolarity.SelectedIndex);
            binaryIOV.EventEnable = checkEnable.Checked ? BacNetValidator.GetEventCheckBoxResult(checktooffNormal.Checked, checkToFault.Checked, checkToNormal.Checked) : 0;
            binaryIOV.ActiveText = txtActive.Text.Trim();
            binaryIOV.InactiveText = txtInactive.Text.Trim();
            if (labelObjType.Text.Contains("Binary Output"))
            {
                var selectedTag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag == comboboxFeedbackValue.Text);

                if (comboboxFeedbackValue.SelectedIndex != 1 && comboboxFeedbackValue.SelectedIndex != 2 && selectedTag != null)
                {
                    binaryIOV.TagValue = selectedTag.LogicalAddress;
                }
                else
                {
                    binaryIOV.TagValue = "";
                }
            }
            string selectedFeedback = comboboxFeedbackValue.SelectedItem?.ToString()?.Trim() ?? "";
            string activeText = txtActive.Text.Trim();
            string inactiveText = txtInactive.Text.Trim();
            int feedbackVal = 0;
            if (selectedFeedback == activeText)
                feedbackVal = 1;
            else
                feedbackVal = 0;
            binaryIOV.FeedbackValue = feedbackVal;
            binaryIOV.SelectedFeedbackText = selectedFeedback;
            binaryIOV.FeedbackValue = !string.IsNullOrEmpty(selectedFeedback) ? feedbackVal : 0;

            binaryIOV.NotifyType = checkEnable.Checked ? (checkAlarm.Checked ? 0 : checkEvent.Checked ? 1 : 0) : 0;
            binaryIOV.IsEnable = bacNetIP.BinaryIOValues.Where(b => b.LogicalAddress == logicaladdress).FirstOrDefault().IsEnable;
            int oldObjectIndex = bacNetIP.BinaryIOValues.IndexOf(bacNetIP.BinaryIOValues.Where(b => b.LogicalAddress == logicaladdress).FirstOrDefault());
            bacNetIP.BinaryIOValues.Remove(bacNetIP.BinaryIOValues.Where(b => b.LogicalAddress == logicaladdress).FirstOrDefault());
            bacNetIP.BinaryIOValues.Insert(oldObjectIndex, binaryIOV);
            XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == logicaladdress).FirstOrDefault().Tag = binaryIOV.ObjectName;
            CommonFunctions.UpdateTagNames(logicaladdress, binaryIOV.ObjectName);
            string tempInitial = comboBoxInitialValue.Text.Equals(txtActive.Text) ? "1" : "0";
            string tempRelinquish = cmbRelinquishDefault.Text.Equals(txtActive.Text) ? "1" : "0";
            XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == logicaladdress).FirstOrDefault().InitialValue = tempInitial;
            XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == logicaladdress).FirstOrDefault().InitialValue = tempRelinquish;
            if (cmbRelinquishDefault.Visible)
            {
                binaryIOV.RelinquishDefault = Convert.ToInt32(cmbRelinquishDefault.Text.Equals(txtActive.Text) ? "1" : "0");
            }
            else if (textReDefault.Visible)
            {
                if (!string.IsNullOrEmpty(textReDefault.Text))
                {
                    binaryIOV.TagValue = textReDefault.Text;
                }
            }
            MessageBox.Show("Binary infomation updated", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            XMPS.Instance.LoadedProject.isChanged = false;
            if ((FormBacNet)this.ParentForm != null)
                ((FormBacNet)this.ParentForm).RefreshGridView();
            else
            {
                var formBacNetInstance = Application.OpenForms
                              .OfType<FormBacNet>()
                              .FirstOrDefault(f => f.Name == "FormBacNet");
                formBacNetInstance?.RefreshGridView();
            }
            return true;
        }



        private void textReDefault_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textTimeDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void checkAlarm_CheckedChanged(object sender, EventArgs e)
        {
            HandleNotificationCheckBoxes(true, false, false);
        }

        private void checkNotification_CheckedChanged(object sender, EventArgs e)
        {
            HandleNotificationCheckBoxes(false, true, false);
        }

        private void checkEvent_CheckedChanged(object sender, EventArgs e)
        {
            HandleNotificationCheckBoxes(false, false, true);
        }
        private void HandleNotificationCheckBoxes(bool isAlarmChecked, bool isNotificationChecked, bool isEventChecked)
        {
            //temporarily remove event notification checkboxes not call event recursively.
            checkAlarm.CheckedChanged -= checkAlarm_CheckedChanged;
            checkEvent.CheckedChanged -= checkEvent_CheckedChanged;

            if (checkEnable.Checked)
            {
                checkAlarm.Checked = isAlarmChecked;
                checkEvent.Checked = isEventChecked;
            }
            else
            {
                checkAlarm.Checked = false;
                checkEvent.Checked = false;
            }
            //adding event notification checkboxes.
            checkAlarm.CheckedChanged += checkAlarm_CheckedChanged;
            checkEvent.CheckedChanged += checkEvent_CheckedChanged;
        }

        private void textObjectName_Validating(object sender, CancelEventArgs e)
        {
            bool isObjNamePresent = XMPS.Instance.LoadedProject.Tags.Any(t => t.Tag.Equals(textObjectName.Text));
            string tagAdd = XMPS.Instance.LoadedProject.Tags.Where(t => t.Tag.Equals(textObjectName.Text)).Select(t => t.LogicalAddress).FirstOrDefault();
            if (isObjNamePresent && logicaladdress != tagAdd)
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Object Name is Already Used");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textObjectName, null);
            }
            //for checking length
            if (!string.IsNullOrWhiteSpace(textObjectName.Text) && textObjectName.Text.Length > 30)
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Please resolve the errors first");
            }
            if (textObjectName.Text == "")
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Please resolve the errors first");
            }
            string text = textObjectName?.Text?.Trim() ?? string.Empty;

            // Check if text is empty or whitespace
            if (string.IsNullOrWhiteSpace(text))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Tag name cannot be empty or whitespace.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(textObjectName.Text) && textObjectName.Text.Any(ch => !(char.IsLetterOrDigit(ch) || ch == '_' || ch == ' ')))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Only letters, digits, underscore (_) and spaces are allowed.");
            }
            if (char.IsDigit(textObjectName.Text[0]))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Tag name cannot start with a number.");
                return;
            }
            foreach (char ch in textObjectName.Text.Trim())
            {
                if (!char.IsLetterOrDigit(ch) && ch != 95 && ch != 3 && ch != 22) // Allowed characters
                {
                    e.Cancel = true;
                    errorProvider.SetError(textObjectName, "Invalid character detected. Only letters, digits, and underscore (_) are allowed.");
                    return;
                }
            }
        }

        private void textTimeDelayNormal_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textTimeDelayNormal.Text))
            {
                textTimeDelayNormal.Text = "0";
                textTimeDelayNormal.SelectionStart = textTimeDelayNormal.Text.Length;
            }
        }
        private void textReDefault_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textReDefault.Text))
            {
                // textReDefault.Text = "0";
                textReDefault.SelectionStart = textReDefault.Text.Length;
            }
        }

        private void textTimeDelay_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textTimeDelay.Text))
            {
                textTimeDelay.Text = "0";
                textTimeDelay.SelectionStart = textTimeDelay.Text.Length;
            }
        }

        private void textTimeDelayNormal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textTimeDelayNormal.Text))
            {
                textTimeDelayNormal.Text = "0";
                textTimeDelayNormal.SelectionStart = textTimeDelayNormal.Text.Length;
            }
        }

        private void textFeedbackValue_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboboxFeedbackValue.Text))
            {
                comboboxFeedbackValue.Text = "0";
                comboboxFeedbackValue.SelectionStart = textTimeDelayNormal.Text.Length;
            }
        }

        private void checkAlarm_Validating(object sender, CancelEventArgs e)
        {
            if (checkEnable.Checked == true && checkAlarm.Checked == false && checkEvent.Checked == false)
            {
                e.Cancel = true;
                errorProvider.SetError(checkEvent, "Please Select Notify Type ");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(checkEvent, null);
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            ChechText();
            UpdateComboBox();
        }
        private void ChechText()
        {
            if (txtActive.Text == txtInactive.Text && !string.IsNullOrEmpty(txtActive.Text))
            {
                MessageBox.Show("The values of Active and Inactive text boxes cannot be the same.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtActive.Focus();
            }
        }
        private void UpdateComboBox()
        {
            var previousSelections = new Dictionary<ComboBox, (int index, string value)>
            {
                { cmbRelinquishDefault, (cmbRelinquishDefault.SelectedIndex, cmbRelinquishDefault.SelectedItem?.ToString()) },
                { comboBoxInitialValue, (comboBoxInitialValue.SelectedIndex, comboBoxInitialValue.SelectedItem?.ToString()) },
                { comboboxFeedbackValue, (comboboxFeedbackValue.SelectedIndex, comboboxFeedbackValue.SelectedItem?.ToString()) }
            };

            foreach (var combo in new[] { cmbRelinquishDefault, comboBoxInitialValue, comboboxFeedbackValue })
                combo.Items.Clear();
            comboboxFeedbackValue.Items.Insert(0,"");
            cmbRelinquishDefault.Items.Add(string.IsNullOrEmpty(txtInactive.Text) ? "" : txtInactive.Text);
            comboBoxInitialValue.Items.Add(string.IsNullOrEmpty(txtInactive.Text) ? "" : txtInactive.Text);
            comboboxFeedbackValue.Items.Add(string.IsNullOrEmpty(txtInactive.Text) ? "" : txtInactive.Text);

            cmbRelinquishDefault.Items.Add(string.IsNullOrEmpty(txtActive.Text) ? "" : txtActive.Text);
            comboBoxInitialValue.Items.Add(string.IsNullOrEmpty(txtActive.Text) ? "" : txtActive.Text);
            comboboxFeedbackValue.Items.Add(string.IsNullOrEmpty(txtActive.Text) ? "" : txtActive.Text);  
            comboboxFeedbackValue.Items.AddRange(tagsToAdd.ToArray());          

            foreach (var kvp in previousSelections)
            {
                var combo = kvp.Key;
                var (index, value) = kvp.Value;

                if (!string.IsNullOrEmpty(value) && combo.Items.Contains(value))
                    combo.SelectedItem = value;
                else if (index >= 0 && index < combo.Items.Count)
                    combo.SelectedIndex = index;
                else
                    combo.SelectedIndex = -1;
            }
        }
        private void textObjectName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textObjectName.Text) && textObjectName.Text.Length > 25)
            {
                errorProvider.SetError(textObjectName, "Object Name must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(textObjectName, null);
            }
            if (string.IsNullOrEmpty(textObjectName.Text))
            {
                //e.Cancel = true;
                errorProvider.SetError(textObjectName, "Please resolve the errors first");
            }
        }
        private void textDescription_TextChanged(object sender, EventArgs e)
        {
            if (textDescription.Text.Length > 25)
            {
                errorProvider.SetError(textDescription, "Object Name must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(textDescription, null);
            }
        }

        private void textDescription_Validating(object sender, CancelEventArgs e)
        {
            //for checking length
            if (!string.IsNullOrWhiteSpace(textDescription.Text) && textDescription.Text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textDescription, "Please resolve the errors first");
            }
        }
    }
}
