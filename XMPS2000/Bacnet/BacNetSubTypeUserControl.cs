using System;
using System.Windows.Forms;
using XMPS2000.Core;
using System.Linq;
using XMPS2000.Core.BacNet;
using XMPS2000.Configuration;
using System.ComponentModel;
using XMPS2000.LadderLogic;

namespace XMPS2000.Bacnet
{
    public partial class BacNetSubTypeUserControl : UserControl
    {
        private BacNetIP bacNetIP;
        private string currentDeviceType;
        string currentLogicalAddress;
        public BacNetSubTypeUserControl(string DeviceType)
        {
            InitializeComponent();
            bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            currentDeviceType = DeviceType;
            AssingDefaultValues(DeviceType);
        }

        private void AssingDefaultValues(string deviceType)
        {
            if (deviceType.Equals("Schedule"))
            {
                int scheduleObjCount = bacNetIP.Schedules.Where(t => t.ObjectType.Equals("17:Schedule")).Count() > 0 ? bacNetIP.Schedules.Where(t => t.ObjectType.Equals("17:Schedule")).Max(t => Convert.ToInt32(t.InstanceNumber)) + 1 : 0;

                this.textBoxObjectIdentifier.Text = $"Schedule:{scheduleObjCount}";
                this.textBoxInstanceNumber.Text = scheduleObjCount.ToString();
                this.textBoxObjectType.Text = "17:Schedule";
                this.grpSchedule.Visible = true;

            }
            else if (deviceType.Equals("Calendar"))
            {
                int calendarObjCount = bacNetIP.Calendars.Where(t => t.ObjectType.Equals("6:Calendar")).Count() > 0 ? bacNetIP.Calendars.Where(t => t.ObjectType.Equals("6:Calendar")).Max(t => Convert.ToInt32(t.InstanceNumber)) + 1 : 0;

                this.textBoxObjectIdentifier.Text = $"Calendar:{calendarObjCount}";
                this.textBoxInstanceNumber.Text = calendarObjCount.ToString();
                this.textBoxObjectType.Text = "6:Calendar";
                this.grpSchedule.Visible = false;
            }
        }

        private void textBoxObjectName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Safely get and trim text
            string text = textBoxObjectName?.Text?.Trim() ?? string.Empty;

            // Check if text is empty or whitespace
            if (string.IsNullOrWhiteSpace(text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxObjectName, "Tag name cannot be empty or whitespace.");
                return;
            }
        
            // Check for invalid characters
            foreach (char ch in text)
            {
                if (!(char.IsLetterOrDigit(ch) || ch == '_' || ch != ' '))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(textBoxObjectName, "Only letters, digits, underscore (_), and spaces are allowed.");
                    return;
                }
            }

            // All validations passed
            e.Cancel = false;
            errorProvider1.SetError(textBoxObjectName, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please Resolve Error First", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                bool isObjectNameUsed = BacNetFormFactory.ValidateObjectName(textBoxObjectName.Text.ToString(), currentDeviceType);
                if (isObjectNameUsed)
                {
                    MessageBox.Show("Object Name Already Used", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AddObject();
                this.ParentForm.Close();
                this.ParentForm.DialogResult = DialogResult.OK;
            }
        }

        private void AddObject()
        {
            switch (currentDeviceType)
            {
                case "Schedule":
                    bacNetIP.Schedules.Add(new Schedule(textBoxObjectIdentifier.Text, textBoxInstanceNumber.Text, textBoxObjectType.Text, textBoxObjectName.Text, textBoxDescription.Text, textBoxVariable.Text, comboBoxScheduleValue.Text.Equals("Numeric") ? 1 : 0));
                    break;
                case "Calendar":
                    bacNetIP.Calendars.Add(new Calendar(textBoxObjectIdentifier.Text, textBoxInstanceNumber.Text, textBoxObjectType.Text, textBoxObjectName.Text, textBoxDescription.Text));
                    break;
            }
        }

        private void textBoxObjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonTextBox_KeyPress(sender, e);
        }
        private void CommonTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text.Length >= 25)
                {
                    if (e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        private void textBoxDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonTextBox_KeyPress(sender, e);
        }

        private void textBoxVariable_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Check for select value in the function code combobox according to this required validation changes 
            if (textBoxVariable.Text == "") return;
            if (comboBoxScheduleValue.SelectedItem?.ToString() == "Boolean")
            {
                // Check validation for bit address
                ValidateBitAddress(textBoxVariable, e, "Inavalid bit address or value for Enable field");
            }
            else
            {
                // Check validation for word address   
                ValidateWordAddress(textBoxVariable, e, "Inavalid word address or value for Enable field");
            }
            if (!e.Cancel)
            {
                var checktag = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
                if (checktag != null)
                    this.TagEnable.Text = checktag.Tag.ToString();
                else if (!textBoxVariable.Text.Contains('.') && (textBoxVariable.Text.StartsWith("Q0") || textBoxVariable.Text.StartsWith("I1")))
                {
                    checktag = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString() + ".00").FirstOrDefault();
                    this.TagEnable.Text = "";
                }
                else
                {
                    //for creating Tag from Modbus Window
                    string newTag = "";
                    if (textBoxVariable.Text != "")
                    {
                        if (MessageBox.Show("Variable Tag is not Added You Want to Add", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            XMProForm tempForm = new XMProForm();
                            tempForm.StartPosition = FormStartPosition.CenterParent;
                            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                            tempForm.Text = "Add New Address Added in Logic";
                            newTag = textBoxVariable.Text;
                            TagsUserControl userControl = new TagsUserControl(0, newTag);
                            tempForm.Height = userControl.Height + 25;
                            tempForm.Width = userControl.Width;
                            tempForm.Controls.Add(userControl);
                            var frmTemp = this.ParentForm as frmMain;
                            DialogResult dialogResult = tempForm.ShowDialog(frmTemp);
                            if (dialogResult == DialogResult.OK)
                            {
                                this.currentLogicalAddress = userControl.LogicalAddressForModbus;
                            }
                            else
                                this.currentLogicalAddress = "";
                        }
                    }
                    FillTagDropDown();
                    if (currentLogicalAddress != "") TagEnable.Text = XMProValidator.GetTheTagnameFromAddress(this.currentLogicalAddress);
                    return;
                }
            }
        }

        private void FillTagDropDown()
        {
            // Check for select value in the function code combobox according to this required validation changes 
            if (comboBoxScheduleValue.SelectedItem.ToString() == "Boolean")
            {
                var obje = XMProValidator.FillTagOperandsForSchedule("Bool");
                TagEnable.DataSource = obje;
            }
            else
            {
                var obje = XMProValidator.FillTagOperandsForSchedule("Real");
                TagEnable.DataSource = obje;
            }
        }
        /// <summary>
        /// Validate if the entered Bit Address is as per requirenments or not
        /// </summary>
        /// <param name="control"></param> Control for which the check is required
        /// <param name="e"></param> Cancel Event Handler
        /// <param name="errorMessageToDisplay"></param> Error to show
        private void ValidateBitAddress(Control control, CancelEventArgs e, string errorMessageToDisplay)
        {
            if (control.Text.IsValidBitAddress())
            {
                e.Cancel = false;
                errorProvider1.SetError(control, null);
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(control, errorMessageToDisplay);
            }
        }

        /// <summary>
        /// Check if the entered Word Address is valid or not and raise error if any
        /// </summary>
        /// <param name="control"></param> Control for which the check is required
        /// <param name="e"></param> Cancel Event Handler
        /// <param name="errorMessageToDisplay"></param> Error Message to show
        private void ValidateWordAddress(Control control, CancelEventArgs e, string errorMessageToDisplay)
        {
            if (control.Text.IsValidWordAddressForModBus())
            {
                e.Cancel = false;
                errorProvider1.SetError(control, null);
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(control, errorMessageToDisplay);
            }
        }

        private void comboBoxScheduleValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTagDropDown();
        }

        private void TagEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxVariable.Text))
            {
                string logicalAdd = XMProValidator.GetTheAddressFromTag(TagEnable.Text);
                textBoxVariable.Text = (currentLogicalAddress == logicalAdd || logicalAdd == "") ? currentLogicalAddress : logicalAdd;
            }
            else
            {
                textBoxVariable.Text = XMProValidator.GetTheAddressFromTag(TagEnable.Text);
            }
        }

        private void comboBoxScheduleValue_Validating(object sender, CancelEventArgs e)
        {
            if (comboBoxScheduleValue.SelectedIndex == -1 && currentDeviceType == "Schedule")
            {
                e.Cancel = true;
                errorProvider1.SetError(comboBoxScheduleValue, "Please select schedule value");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(comboBoxScheduleValue, null);
            }
        }

        private void TagEnable_Validating(object sender, CancelEventArgs e)
        {
            if ((TagEnable.SelectedIndex == -1 || string.IsNullOrEmpty(textBoxVariable.Text)) && currentDeviceType == "Schedule")
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxVariable, "Please select variable value");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBoxVariable, null);
            }
        }
        private void textBoxObjectName_TextChanged(object sender, EventArgs e)
        {
            const int maxLength = 25;
            var tb = (TextBox)sender;

            if (tb.Text.Length > maxLength)
            {
                int selectionStart = tb.SelectionStart;
                tb.Text = tb.Text.Substring(0, maxLength);
                tb.SelectionStart = selectionStart > maxLength ? maxLength : selectionStart;
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            const int maxLength = 25;
            var tb = (TextBox)sender;

            if (tb.Text.Length > maxLength)
            {
                int selectionStart = tb.SelectionStart;
                tb.Text = tb.Text.Substring(0, maxLength);
                tb.SelectionStart = selectionStart > maxLength ? maxLength : selectionStart;
            }
        }
        private void btnSave_Validating(object sender, CancelEventArgs e)
        {
            if (currentDeviceType != "Schedule")
                return;

            XMPS xm = XMPS.Instance;
            bool isThere = false;
            string result = textBoxVariable.Text;
            // Check validation based on selected type
            if (comboBoxScheduleValue.SelectedItem?.ToString() == "Boolean")
            {
                if (!e.Cancel)
                {
                    isThere = xm.LoadedProject.Tags.Any(d => (d.LogicalAddress.Contains(".") || d.LogicalAddress.StartsWith("F2") || (d.Label == "Bool"
                    && !d.LogicalAddress.StartsWith("'"))) && d.LogicalAddress == result);
                }
            }
            else
            {
                if (!e.Cancel)
                {
                    isThere = xm.LoadedProject.Tags.Any(d => ((!d.LogicalAddress.Contains(".") && !d.Label.Contains("Word") && !d.Label.Contains("Bool")
                    && !d.Label.Contains("Byte") && !d.Label.Contains("String") && !d.Label.Contains("Int")) || d.LogicalAddress.StartsWith("P5") || (d.Label == "Real"
                    && !d.LogicalAddress.StartsWith("'"))) && d.LogicalAddress == result);
                }

            }

            // After address format check, handle tag existence check
            if (!e.Cancel)
            {
                if (!isThere)
                {
                    e.Cancel = true;

                    string errorMsg = comboBoxScheduleValue.SelectedItem?.ToString() == "Boolean"
                        ? "The specified Boolean tag does not exist."
                        : "The specified Real tag does not exist.";

                    errorProvider1.SetError(textBoxVariable, errorMsg);
                }
                else
                {
                    errorProvider1.SetError(textBoxVariable, null); // Clear error if everything is valid
                }
            }
        }
    }
}

