using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core.BacNet;

namespace XMPS2000.Bacnet
{
    public partial class NotificationsUserControl : Form
    {
        public Recipient recipient;
        public NotificationsUserControl()
        {
            InitializeComponent();
        }

        public NotificationsUserControl(Recipient passedRecipient)
        {
            InitializeComponent();
            textName.Text = passedRecipient.Name.ToString();
            dtpkrStartTime.Value = DateTime.Today.Add(TimeSpan.Parse(passedRecipient.StartTime.ToString()));
            dtpkrEndTime.Text = passedRecipient.EndTime.ToString();
            textProcessInd.Text = passedRecipient.ProcessIdentifier.ToString();
            comboReceipentType.Text = passedRecipient.RecipientType.ToString();
            textBoxDeviceInst.Text = passedRecipient.DeviceInstance.ToString();
            chkUnconfirmed.Checked = passedRecipient.Notification.ToString() == "0" ? true : false;
            chkConfirmed.Checked = passedRecipient.Notification.ToString() == "1" ? true : false;
            SetWeekDetails(passedRecipient.DaysofWeek.ToString());
            dtpkrStartTime.Value = DateTime.Today.Add(TimeSpan.Parse(passedRecipient.StartTime.ToString()));
            chkTooffNormal.Checked = passedRecipient.Transition.ToString() == "1" ? true : false;
            chkToFault.Checked = passedRecipient.Transition.ToString() == "1" ? true : false;
            chkToNormal.Checked = passedRecipient.Transition.ToString() == "1" ? true : false;
            CheckAckRequiredCheckBoxes(passedRecipient.Transition);
        }

        private void SetWeekDetails(string DaysOfWeek)
        {
            string[] daynames = DaysOfWeek.Split(',');
            // Dictionary to map day abbreviations to checkbox controls.
            var dayCheckboxMap = new Dictionary<string, CheckBox>
                {
                { "Mon", chkMon },
                { "Tue", chkTue },
                { "Wed", chkWed },
                { "Thu", chkThu },
                { "Fri", chkFri },
                { "Sat", chkSat},
                { "Sun", chkSun}
                };
            dayCheckboxMap.Values.ToList().ForEach(cb => cb.Checked = false);

            foreach (string daynm in daynames)
            {
                if (dayCheckboxMap.TryGetValue(daynm, out CheckBox checkBox))
                {
                    checkBox.Checked = true;
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboReceipentType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select valid Receipent Type", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textName.Text.Length < 1)
            {
                MessageBox.Show("Name is required, can't be blank", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!chkSun.Checked && !chkMon.Checked && !chkTue.Checked && !chkWed.Checked && !chkThu.Checked && !chkFri.Checked && !chkSat.Checked && !chkSun.Checked)
            {
                MessageBox.Show("Select atleast one day of week", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            recipient = new Recipient();
            recipient.Name = textName.Text.Trim();
            recipient.DaysofWeek = GetDaysOfWeek().TrimStart(',');
            recipient.StartTime = dtpkrStartTime.Text.ToString();
            recipient.EndTime = dtpkrEndTime.Text.ToString();
            recipient.ProcessIdentifier = textProcessInd.Text.ToString();
            recipient.Notification = chkConfirmed.Checked ? "1" : "0";
            recipient.RecipientType = comboReceipentType.Text.ToString();
            recipient.DeviceInstance = textBoxDeviceInst.Text.ToString();
            recipient.Transition = BacNetValidator.GetEventCheckBoxResult(chkTooffNormal.Checked, chkToFault.Checked, chkToNormal.Checked);
            this.DialogResult = DialogResult.OK;
        }

        private string GetDaysOfWeek()
        {
            string selecteddays = "";
            selecteddays = selecteddays + (chkMon.Checked == true ? ",Mon" : "");
            selecteddays = selecteddays + (chkTue.Checked == true ? ",Tue" : "");
            selecteddays = selecteddays + (chkWed.Checked == true ? ",Wed" : "");
            selecteddays = selecteddays + (chkThu.Checked == true ? ",Thu" : "");
            selecteddays = selecteddays + (chkFri.Checked == true ? ",Fri" : "");
            selecteddays = selecteddays + (chkSat.Checked == true ? ",Sat" : "");
            selecteddays = selecteddays + (chkSun.Checked == true ? ",Sun" : "");
            return selecteddays;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkUnconfirmed_CheckedChanged(object sender, EventArgs e)
        {
            chkConfirmed.Checked = chkUnconfirmed.Checked ? false : chkConfirmed.Checked;
        }

        private void chkConfirmed_CheckedChanged(object sender, EventArgs e)
        {
            chkUnconfirmed.Checked = chkConfirmed.Checked ? false : chkUnconfirmed.Checked;
        }

        private void textProcessInd_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateBacNetInput(textProcessInd, e, "Byte");
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

        private void textBoxDeviceInst_Validating(object sender, CancelEventArgs e)
        {
            bool validationSuccessful = true;
            if (!string.IsNullOrEmpty(textBoxDeviceInst.Text) && int.TryParse(textBoxDeviceInst.Text,out _)  )
                validationSuccessful = Convert.ToInt32(textBoxDeviceInst.Text) >= 0 && Convert.ToInt32(textBoxDeviceInst.Text) < 4194303 ? true : false;
            else
                validationSuccessful = false;
            e.Cancel = !validationSuccessful;
            errorProvider.SetError(textBoxDeviceInst, validationSuccessful ? null : "Invalid value for device instance it can be between 0 or 4194302");
        }

        private void dtpkrEndTime_Validating(object sender, CancelEventArgs e)
        {
            if (dtpkrStartTime.Value >= dtpkrEndTime.Value)
            {
                e.Cancel = true;
                errorProvider.SetError(dtpkrEndTime,"End time should be greater than start time.");
            }
            else
            {
                errorProvider.SetError(dtpkrEndTime, null);
                e.Cancel = false;
            }

        }

        private void textProcessInd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxDeviceInst_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void comboReceipentType_Validating(object sender, CancelEventArgs e)
        {
            if (comboReceipentType.SelectedIndex < 0)
            {
                e.Cancel = true;
                errorProvider.SetError(comboReceipentType, "Please select Receipent Type.");
            }
            else
            {
                errorProvider.SetError(comboReceipentType, null);
                e.Cancel = false;
            }
        }
        private void textName_Validating(object sender, CancelEventArgs e)
        {
            if (textName.Text.Length < 1)
            {
                e.Cancel = true;
                errorProvider.SetError(textName, "Name is required, can't be blank.");
            }
            else
            {
                errorProvider.SetError(textName, null);
                e.Cancel = false;
            }
        }
        private void CheckAckRequiredCheckBoxes(int eventEnable)
        {
            string checkboxesVal = Convert.ToString(eventEnable, 2).PadLeft(3, '0');
            chkTooffNormal.Checked = checkboxesVal[0] == '1';
            chkToFault.Checked = checkboxesVal[1] == '1';
            chkToNormal.Checked = checkboxesVal[2] == '1';
        }     
    }
}
