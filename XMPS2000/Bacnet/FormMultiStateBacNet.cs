using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using System.Reflection;
using XMPS2000.LadderLogic;

namespace XMPS2000.Bacnet
{
    public partial class FormMultiStateBacNet : Form, IXMForm
    {
        private string tagname;
        private string logicaladdress;
        private MultistateIOV currentMultistateIOV;
        private readonly bool _isReadOnly;
        public FormMultiStateBacNet(XMIOConfig ioconfig, bool isReadOnly = false)
        {
            InitializeComponent();
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            tagname = ioconfig.Tag.ToString();
            textObjectName.Text = tagname;
            logicaladdress = ioconfig.LogicalAddress;
            AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            XMPS.Instance.BacNetCurrentScreen = "FormMultiStateBacNet";
            _isReadOnly = isReadOnly;
            ApplyReadOnly();
        }
        private void ApplyReadOnly()
        {
            if (_isReadOnly)
            {
                textObjectName.Enabled = false;
                textDescription.Enabled = false;
                btnVieworUpdateStates.Enabled = false;
                textNoOfStates.Enabled = false;
                checkEnable.Enabled = false;
                btnCancel.Enabled = false;
                btnSave.Enabled = false;
                cmbReDefault.Enabled = false;
            }
        }
        /// <summary>
        /// Get data from list and bind it to controls
        /// </summary>
        private void AssignValues()
        {
            MultistateIOV MultiStateIOV = XMPS.Instance.LoadedProject.BacNetIP.MultistateValues.Where(b => b.LogicalAddress == logicaladdress).FirstOrDefault();
            labelObjIdentifier.Text = MultiStateIOV.ObjectIdentifier.ToString();
            labelInstanceno.Text = MultiStateIOV.InstanceNumber.ToString();
            labelObjType.Text = MultiStateIOV.ObjectType;
            textObjectName.Text = MultiStateIOV.ObjectName;
            textDescription.Text = MultiStateIOV.Description;
            logicaladdress = MultiStateIOV.LogicalAddress;
            textNoOfStates.Text = MultiStateIOV.NumberOfStates > 0 ? MultiStateIOV.NumberOfStates.ToString() : string.Empty;
            textTimeDelay.Text = MultiStateIOV.TimeDelay.ToString();
            textTimeDelayNormal.Text = MultiStateIOV.TimeDelayNormal.ToString();
            if (MultiStateIOV.AlarmValues != null && MultiStateIOV.AlarmValues.Count > 0)
            {
                var validAlarmValues = MultiStateIOV.AlarmValues.Where(av => av > 0 && av <= MultiStateIOV.NumberOfStates).OrderBy(av => av).ToList();
                MultiStateIOV.AlarmValues = validAlarmValues;
                textAlarmValue.Text = validAlarmValues.Count > 0? string.Join(",", validAlarmValues): string.Empty;
            }
            else
            {
                textAlarmValue.Text = string.Empty;
            }
            textAlarmValue.ReadOnly = true;
            comboNotifyclass.DataSource = XMPS.Instance.LoadedProject.BacNetIP.Notifications.ToList();
            comboNotifyclass.DisplayMember = "ObjectName";
            comboNotifyclass.ValueMember = "InstanceNumber";
            comboNotifyclass.SelectedValue = MultiStateIOV.NotificationClass.ToString();
            checkEnable.Checked = MultiStateIOV.EventDetectionEnable == 1 ? true : false;
            CheckEventEnableCheckBoxes(MultiStateIOV.EventEnable);
            CheckNotificationEnableCheckBoxes(MultiStateIOV.NotifyType);
            currentMultistateIOV = MultiStateIOV;
            DisplayInitialValue(MultiStateIOV);
            var binaryOnlyList = XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(x => x.ObjectType.Contains("Binary Value")).ToList();
            binaryOnlyList.Insert(0, new BinaryIOV { ObjectName = "Select Inhibit Binary Value", InstanceNumber = "" });
            cmb_BinaryValue.DataSource = binaryOnlyList;
            cmb_BinaryValue.DisplayMember = "ObjectName";
            cmb_BinaryValue.ValueMember = "InstanceNumber";
            cmbReDefault.SelectedIndex = Convert.ToInt32(MultiStateIOV.RelinquishDefault);
            if (cmb_BinaryValue.SelectedIndex == 0)
            {
                cmb_BinaryValue.SelectedIndex = 0;
            }
            cmb_BinaryValue.SelectedIndex = MultiStateIOV.BinaryValue;
        }

        private void DisplayInitialValue(MultistateIOV MultiStateIOV)
        {
            string initialvalue = XMPS.Instance.LoadedProject.Tags
                  .FirstOrDefault(t => t.LogicalAddress.Equals(logicaladdress))?.InitialValue;
            cmbInitialValue.DataSource = MultiStateIOV.States != null ? MultiStateIOV.States.Select(t => t.StateValue).ToList() : null;
            cmbReDefault.DataSource = MultiStateIOV.States != null ? MultiStateIOV.States.Select(t => t.StateValue).ToList() : null;
            int parsedValue;

            if (int.TryParse(initialvalue, out parsedValue))
            {
                if (parsedValue <= MultiStateIOV.NumberOfStates)
                {
                    cmbInitialValue.SelectedIndex = parsedValue - 1;
                }
                else
                {
                    cmbInitialValue.SelectedIndex = -1;
                    cmbInitialValue.Text = initialvalue.ToString();
                }
            }
        }
        public void OnShown()
        {
            textObjectName.Text = tagname;
        }

        private void checkEnable_CheckedChanged(object sender, EventArgs e)
        {
            textTimeDelay.Enabled = checkEnable.Checked;
            textTimeDelayNormal.Enabled = checkEnable.Checked;
            textAlarmValue.Enabled = checkEnable.Checked;
            comboNotifyclass.Enabled = checkEnable.Checked;
            grpeventenb.Enabled = checkEnable.Checked;
            grpnotifytype.Enabled = checkEnable.Checked;
            cmbInitialValue.Enabled = checkEnable.Checked;
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

        private void textNoOfStates_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int noOfStates = !string.IsNullOrEmpty(textNoOfStates.Text) ? Convert.ToInt32(textNoOfStates.Text) : 0;
            if (noOfStates > 0)
            {
                int currentMultistateState = currentMultistateIOV.States != null ? currentMultistateIOV.States.Count : 0;

                if (currentMultistateState != noOfStates)
                {
                    e.Cancel = true;
                    errorProvider.SetError(textNoOfStates, "Please check no of state count not match with state configuration");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(textNoOfStates, string.Empty);
                }
            }

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
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges(sender, e);
        }

        private bool SaveChanges(object sender, EventArgs e)
        {
            var dublicateObject = XMPS.Instance.LoadedProject.BacNetIP.MultistateValues.FirstOrDefault(t => t.ObjectName.Equals(textObjectName.Text.ToString().Trim()));
            if (BacNetFormFactory.ValidateObjectName(textObjectName.Text.ToString().Trim(), "MultistateValues") && dublicateObject?.ObjectIdentifier != labelObjIdentifier.Text.ToString())
            {
                MessageBox.Show("Tag name is already used, change the name and try again ...", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            List<long> alarmValues = new List<long>();
            if (!string.IsNullOrEmpty(textAlarmValue.Text))
            {
                var values = textAlarmValue.Text.Split(',');
                foreach (var value in values)
                {
                    if (long.TryParse(value.Trim(), out long alarmVal))
                    {
                        alarmValues.Add(alarmVal);
                    }
                }
            }
            MultistateIOV MultiStateIOValues = new MultistateIOV();
            BacNetIP bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            MultiStateIOValues.ObjectIdentifier = labelObjIdentifier.Text.ToString();
            MultiStateIOValues.InstanceNumber = labelInstanceno.Text.ToString();
            MultiStateIOValues.ObjectType = labelObjType.Text.ToString();
            MultiStateIOValues.ObjectName = textObjectName.Text.Trim();
            MultiStateIOValues.Description = textDescription.Text.Trim();
            MultiStateIOValues.LogicalAddress = logicaladdress;
            MultiStateIOValues.BinaryValue = cmb_BinaryValue.SelectedIndex;
            MultiStateIOValues.NumberOfStates = !string.IsNullOrEmpty(textNoOfStates.Text) ? Convert.ToInt64(textNoOfStates.Text.ToString()) : 0;
            MultiStateIOValues.EventDetectionEnable = checkEnable.Checked ? 1 : 0;
            MultiStateIOValues.TimeDelay = checkEnable.Checked ? Convert.ToInt64(textTimeDelay.Text.ToString()) : 0;
            MultiStateIOValues.TimeDelayNormal = checkEnable.Checked ? Convert.ToInt64(textTimeDelayNormal.Text.ToString()) : 0;
            MultiStateIOValues.NotificationClass = checkEnable.Checked ? Convert.ToInt32(comboNotifyclass.SelectedValue) : 0;
            MultiStateIOValues.EventEnable = checkEnable.Checked ? BacNetValidator.GetEventCheckBoxResult(checktooffNormal.Checked, checkToFault.Checked, checkToNormal.Checked) : 0;
            MultiStateIOValues.AlarmValue = alarmValues.Count > 0 ? alarmValues[0] : 0;
            MultiStateIOValues.AlarmValues = alarmValues;
            MultiStateIOValues.States = currentMultistateIOV.States;
            MultiStateIOValues.NotifyType = checkEnable.Checked ? (checkAlarm.Checked ? 0 : checkEvent.Checked ? 1 : 0) : 0;
            MultiStateIOValues.IsEnable = bacNetIP.MultistateValues.Where(b => b.LogicalAddress == logicaladdress).FirstOrDefault().IsEnable;
            var existingItem = bacNetIP.MultistateValues.FirstOrDefault(b => b.LogicalAddress == logicaladdress);
            if (existingItem != null)
            {
                bacNetIP.MultistateValues.Remove(existingItem);
            }
            bacNetIP.MultistateValues.Add(MultiStateIOValues);
            XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == logicaladdress).FirstOrDefault().Tag = MultiStateIOValues.ObjectName;
            CommonFunctions.UpdateTagNames(logicaladdress, MultiStateIOValues.ObjectName);
            MultiStateIOValues.RelinquishDefault = cmbReDefault.SelectedIndex;
            string stateNumber = string.Empty;
            //check if States are not null and States.Count is greater than zero.
            if (MultiStateIOValues.States != null && MultiStateIOValues.States.Count > 0)
            {
                foreach (var state in MultiStateIOValues.States)
                {
                    if (state.StateValue == cmbInitialValue.Text)
                    {
                        stateNumber = state.StateNumber.ToString();
                        break;
                    }
                }
            }
            XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == logicaladdress).FirstOrDefault().InitialValue = !string.IsNullOrEmpty(stateNumber) ? stateNumber : cmbInitialValue.Text;

            MessageBox.Show("Multi State Values information updated", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            XMPS.Instance.LoadedProject.isChanged = false;
            ((FormBacNet)this.ParentForm).RefreshGridView();
            return true;
        }

        private void textNoOfStates_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            string newText;
            if (textNoOfStates.SelectionLength > 0)
            {
                newText = textNoOfStates.Text.Remove(textNoOfStates.SelectionStart, textNoOfStates.SelectionLength)
                                              .Insert(textNoOfStates.SelectionStart, e.KeyChar.ToString());
            }
            else
            {
                newText = textNoOfStates.Text.Insert(textNoOfStates.SelectionStart, e.KeyChar.ToString());
            }
            if (!char.IsControl(e.KeyChar) && int.TryParse(newText, out int number))
            {
                if (number == 0)
                {
                    MessageBox.Show("Value cannot be 0. Please enter a value between 1 and 15.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Handled = true;
                    textNoOfStates.Text = "1";
                    return;
                }
                if (number > 15)
                {
                    e.Handled = true;
                    return;
                }
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
        private void checkEvent_Validating(object sender, CancelEventArgs e)
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
            if (!string.IsNullOrWhiteSpace(textObjectName.Text.Trim()) && textObjectName.Text.Trim().Any(ch => !(char.IsLetterOrDigit(ch) || ch == '_' || ch != ' ')))
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

        }

        private void textNoOfStates_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textNoOfStates.Text))
            {
                textNoOfStates.Text = "0";
                textNoOfStates.SelectionStart = textNoOfStates.Text.Length;
            }
        }

        private void textTimeDelay_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textTimeDelay.Text))
            {
                textTimeDelay.Text = "0";
                textTimeDelay.SelectionStart = textTimeDelay.Text.Length;
            }
        }
        private void textNoOfStates_Leave(object sender, EventArgs e)
        {
            if (textNoOfStates.Text.Equals("0"))
            {
                textNoOfStates.Text = "1";
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
        private void checkNotification_Validating(object sender, CancelEventArgs e)
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
        private void btnVieworUpdateStates_Click(object sender, EventArgs e)
        {
            int numberOfStates = !string.IsNullOrEmpty(textNoOfStates.Text) ? Convert.ToInt32(textNoOfStates.Text) : 0;

            if (numberOfStates > 0)
            {
                if (numberOfStates > 15)
                {
                    numberOfStates = 15;
                    textNoOfStates.Text = "15";
                }
                ShowStatePopUp(numberOfStates);
            }

        }
        private void ShowStatePopUp(int noOfStates)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            if (currentMultistateIOV.States == null)
            {
                currentMultistateIOV.States = new List<State>();
            }

            List<State> oldStates = currentMultistateIOV.States;
            MultistateUserControl multistateUserControl = new MultistateUserControl(noOfStates, logicaladdress, ref currentMultistateIOV);
            multistateUserControl.AlarmValuesChanged += (selectedStates) =>
            {
                UpdateAlarmValuesField(selectedStates);
            };
            tempForm.Text = "Configure States";
            tempForm.Height = multistateUserControl.Height + 50;
            tempForm.Width = multistateUserControl.Width + tempForm.DesktopBounds.Width - tempForm.DisplayRectangle.Width;

            tempForm.Controls.Add(multistateUserControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.Cancel || result == DialogResult.None)
            {
                currentMultistateIOV.States = oldStates;
            }
            cmbInitialValue.DataSource = currentMultistateIOV.States.Select(t => t.StateValue).ToList();
            cmbReDefault.DataSource = currentMultistateIOV.States.Select(t => t.StateValue).ToList();
            CancelEventArgs cancelEventArgs = new CancelEventArgs();
            textNoOfStates_Validating(textNoOfStates, cancelEventArgs);
        }
        public void UpdateAlarmValuesField(List<int> selectedStates)
        {
            if (selectedStates != null && selectedStates.Count > 0)
            {
                textAlarmValue.Text = string.Join(",", selectedStates.OrderBy(x => x));
            }
            else
            {
                textAlarmValue.Text = string.Empty; 
            }
        }

        private void cmbInitialValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                if (char.IsControl(e.KeyChar))
                {
                    return;
                }
                string newText = cmbInitialValue.Text.Insert(cmbInitialValue.SelectionStart, e.KeyChar.ToString());
                if (int.TryParse(newText, out int newValue))
                {
                    if (newValue >= 0 && newValue <= 65535)
                    {
                        errorProvider.SetError(cmbInitialValue, null);
                    }
                    else
                    {
                        e.Handled = true;
                        errorProvider.SetError(cmbInitialValue, "Entered value is out of range (0 - 65535).");
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
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

        private void cmbReDefault_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                if (char.IsControl(e.KeyChar))
                {
                    return;
                }
                string newText = cmbReDefault.Text.Insert(cmbReDefault.SelectionStart, e.KeyChar.ToString());
                if (int.TryParse(newText, out int newValue))
                {
                    if (newValue >= 0 && newValue <= 65535)
                    {
                        errorProvider.SetError(cmbReDefault, null);
                    }
                    else
                    {
                        e.Handled = true;
                        errorProvider.SetError(cmbReDefault, "Entered value is out of range (0 - 65535).");
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
