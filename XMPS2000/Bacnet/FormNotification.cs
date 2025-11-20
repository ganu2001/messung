using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;

namespace XMPS2000.Bacnet
{

    public partial class FormNotification : Form
    {
        public string objectname;
        private List<Recipient> prevrecipients = new List<Recipient>();
        List<Recipient> recipients = new List<Recipient>();
        private readonly bool _isReadOnly;
        public FormNotification(string name, bool isReadOnly = false)
        {
            InitializeComponent();
            objectname = name;
            AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            XMPS.Instance.BacNetCurrentScreen = "FormNotification";
            _isReadOnly = isReadOnly;
            ApplyReadOnly();
        }
        private void ApplyReadOnly()
        {
            if (_isReadOnly)
            {
                textObjectName.Enabled = false;
                textDescription.Enabled = false;
                TextOffNormalPrt.Enabled = false;
                TextFaultPrio.Enabled = false;
                TextNormalPriority.Enabled = false;
                checktooffNormal.Enabled = false;
                checkToFault.Enabled = false;
                checkToNormal.Enabled = false;
                btnAddRcpt.Enabled = false;
                btnDeleteRec.Enabled = false;
                dgRecipient.Enabled = false;
                btnCancel.Enabled = false;
                btnSave.Enabled = false;
            }
        }
        private void AssignValues()
        {
            foreach (var notif in XMPS.Instance.LoadedProject.BacNetIP.Notifications.Where(a => a.ObjectName == "Notification"))
            {
                notif.ObjectName = "Notification_Class";
            }
            Notification notification = XMPS.Instance.LoadedProject.BacNetIP.Notifications.Where(b => b.ObjectName == objectname).FirstOrDefault();
            if (notification != null)
            {
                string number = XMPS.Instance.LoadedProject.BacNetIP.Notifications.Where(n => n.ObjectName == objectname).Select(n => n.InstanceNumber).FirstOrDefault().ToString();
                string notcnt = (Convert.ToInt64(XMPS.Instance.LoadedProject.BacNetIP.Notifications.Max(n => n.InstanceNumber)) + 1).ToString();
                if (notification.ObjectIdentifier.Contains("Notification:") || notification.ObjectIdentifier == "Notification Class:")
                    notification.ObjectIdentifier = "Notification Class:" + number;
                if (notification.ObjectType == "15:Notification" || notification.ObjectType == "Notification Class")
                    notification.ObjectType = "15:Notification Class";
                if (notification.ObjectName == "Notification" || notification.ObjectName == "Notification Class")
                    notification.ObjectName = "Notification_Class" + notcnt;
                if (notification.Description == "Notification" || notification.Description == "15:Notification Class")
                    notification.Description = "Notification Class";

                labelObjIdentifier.Text = notification.ObjectIdentifier;
                labelInstanceNo.Text = notification.InstanceNumber;
                labelObjType.Text = notification.ObjectType;
                textObjectName.Text = notification.ObjectName;
                textDescription.Text = notification.Description;
                TextOffNormalPrt.Text = notification.OffNormalPriority.ToString();
                TextFaultPrio.Text = notification.FaultPriority.ToString();
                TextNormalPriority.Text = notification.NormalPriority.ToString();
                dgRecipient.DataSource = notification.Recipients;
                recipients = notification.Recipients;
                prevrecipients.Clear();
                prevrecipients.AddRange(recipients);
                CheckAckRequiredCheckBoxes(notification.AckRequired);
            }
            else
            {
                string notcnt = (Convert.ToInt64(XMPS.Instance.LoadedProject.BacNetIP.Notifications.Max(n => n.InstanceNumber)) + 1).ToString();
                labelObjIdentifier.Text = "Notification Class:" + notcnt;
                labelInstanceNo.Text = notcnt;
                labelObjType.Text = "15:Notification Class";
                textObjectName.Text = "Notification_Class";
                textDescription.Text = "Notification Class";
                TextOffNormalPrt.Text = "255";
                TextFaultPrio.Text = "255";
                TextNormalPriority.Text = "255";
                dgRecipient.DataSource = recipients;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if in edit mode if recipients are not save and directly click on cancel button then old recipients data assign to notification.
            Notification notification;
            if (objectname != "" && objectname != null)
            {
                notification = XMPS.Instance.LoadedProject.BacNetIP.Notifications.Where(b => b.ObjectName == objectname).FirstOrDefault();
                notification.Recipients.Clear();
                notification.Recipients.AddRange(prevrecipients);
            }
            this.AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            if (this.ParentForm != null)
                ((FormBacNet)this.ParentForm).RefreshGridView();
            else
                this.Close();
        }

        private void textInstanceNo_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToUInt64(labelInstanceNo.Text) >= 0 && Convert.ToUInt64(labelInstanceNo.Text) <= 4194302)
            {
                e.Cancel = false;
                errorProvider.SetError(labelInstanceNo, null);
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(labelInstanceNo, "Entered value should be in range of 0 to 4194302");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges(sender, e);
        }

        private bool SaveChanges(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var dublicateObject = XMPS.Instance.LoadedProject.BacNetIP.Notifications.FirstOrDefault(t => t.ObjectName.Equals(textObjectName.Text.ToString().Trim()));
            if (BacNetFormFactory.ValidateObjectName(textObjectName.Text.ToString(), "Notification") && dublicateObject?.ObjectIdentifier != labelObjIdentifier.Text)
            {
                MessageBox.Show("Object name already used, Change object name and try again...", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Notification notification;
            if (objectname != "" && objectname != null)
            {
                notification = XMPS.Instance.LoadedProject.BacNetIP.Notifications.Where(b => b.ObjectName == objectname).FirstOrDefault();
                if (notification == null)
                {
                    notification = XMPS.Instance.LoadedProject.BacNetIP.Notifications.Where(b => b.ObjectName == "Notification"|| b.ObjectName == "Notification Class").FirstOrDefault();
                }
            }
            else
                notification = new Notification();
            notification.ObjectIdentifier = labelObjIdentifier.Text;
            notification.InstanceNumber = labelInstanceNo.Text;
            notification.ObjectType = labelObjType.Text;
            notification.ObjectName = textObjectName.Text.Trim();
            notification.Description = textDescription.Text.Trim();
            notification.OffNormalPriority = Convert.ToInt32(TextOffNormalPrt.Text);
            notification.FaultPriority = Convert.ToInt32(TextFaultPrio.Text);
            notification.NormalPriority = Convert.ToInt32(TextNormalPriority.Text);
            notification.AckRequired = BacNetValidator.GetEventCheckBoxResult(checktooffNormal.Checked, checkToFault.Checked, checkToNormal.Checked);
            notification.Recipients = recipients;
            prevrecipients.Clear();
            prevrecipients.AddRange(recipients);
            MessageBox.Show("Notification Class infomation updated", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            XMPS.Instance.LoadedProject.isChanged = false;
            if (objectname == "" || objectname == null)
            {
                XMPS.Instance.LoadedProject.BacNetIP.Notifications.Add(notification);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if ((FormBacNet)this.ParentForm != null)
                    ((FormBacNet)this.ParentForm).RefreshGridView();
                else
                {
                    var formBacNetInstance = Application.OpenForms
                                  .OfType<FormBacNet>()
                                  .FirstOrDefault(f => f.Name == "FormBacNet");
                    formBacNetInstance?.RefreshGridView();
                }
            }
            objectname = textObjectName.Text.Trim();
            //((FormBacNet)this.ParentForm).RefreshGridView();
            return true;
        }

        private void CheckAckRequiredCheckBoxes(int eventEnable)
        {
            string checkboxesVal = Convert.ToString(eventEnable, 2).PadLeft(3, '0');
            checktooffNormal.Checked = checkboxesVal[0] == '1';
            checkToFault.Checked = checkboxesVal[1] == '1';
            checkToNormal.Checked = checkboxesVal[2] == '1';
        }
        //private void textObjectName_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (textObjectName.Text.Length >= 20 && !char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void textDescription_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (textDescription.Text.Length >= 20 && !char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void textInstanceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextOffNormalPrt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextFaultPrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextNormalPriority_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextOffNormalPrt_Validating(object sender, CancelEventArgs e)
        {
            ValidateBacNetInput(TextOffNormalPrt, e, "Byte");
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

        private void TextFaultPrio_Validating(object sender, CancelEventArgs e)
        {
            ValidateBacNetInput(TextFaultPrio, e, "Byte");
        }

        private void TextNormalPriority_Validating(object sender, CancelEventArgs e)
        {
            ValidateBacNetInput(TextNormalPriority, e, "Byte");
        }

        private void btnAddRcpt_Click(object sender, EventArgs e)
        {
            if (recipients.Count() >= 5 && (XMPS.Instance.PlcModel.Equals("XBLD-14E") || XMPS.Instance.PlcModel.Equals("XBLD-17E")))
            {
                MessageBox.Show("Maximum limit for recipients reach", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NotificationsUserControl notificationsUserControl = new NotificationsUserControl();
            DialogResult status = notificationsUserControl.ShowDialog();
            if (status == DialogResult.OK)
            {
                recipients.Add(notificationsUserControl.recipient);
                BacNetValidator.ControlChanged(null, null);
            }
            dgRecipient.DataSource = null;
            dgRecipient.DataSource = new List<Recipient>(recipients);
            dgRecipient.Refresh();
            notificationsUserControl.Close();
        }


        private void OpenPopup(string dtlname)
        {
            Recipient recipient = recipients.Where(r => r.Name == dtlname).FirstOrDefault();
            NotificationsUserControl notificationsUserControl = new NotificationsUserControl(recipient);
            DialogResult status = notificationsUserControl.ShowDialog();
            if (status == DialogResult.OK)
            {
                int index = recipients.IndexOf(recipient);
                recipients.Remove(recipient);
                recipients.Insert(index, notificationsUserControl.recipient);
                //handling prevrecipients 
                int prevrecipientList = prevrecipients.IndexOf(recipient);
                if (prevrecipientList != -1)
                {
                    prevrecipients.Remove(recipient);
                    prevrecipients.Insert(prevrecipientList, notificationsUserControl.recipient);
                }
            }
            dgRecipient.DataSource = null;
            dgRecipient.DataSource = new List<Recipient>(recipients);
            dgRecipient.Refresh();
        }

        private void dgRecipient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgRecipient.SelectedCells.Count == 0 || e.RowIndex < 0)
                return;
            if (dgRecipient.SelectedCells[0].ColumnIndex == 0)
                dgRecipient.SelectedCells[0].Value = Convert.ToInt32(dgRecipient.SelectedCells[0].Value) == 1 ? 0 : 1;
        }

        private void btnDeleteRec_Click(object sender, EventArgs e)
        {
            var recipientsToDelete = new List<Recipient>();

            foreach (DataGridViewRow dr in dgRecipient.Rows)
            {
                if (Convert.ToInt32(dr.Cells[0].Value) == 1)
                {
                    var recipientName = dr.Cells["Name"].Value.ToString();
                    var recipient = recipients.FirstOrDefault(t => t.Name == recipientName);
                    if (recipient != null)
                    {
                        recipientsToDelete.Add(recipient);
                    }
                }
            }
            if (recipientsToDelete.Count == 0)
            {
                MessageBox.Show("No recipient selected for deletion.", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var confirmResult = MessageBox.Show("Are you sure you want to delete selected recipients?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                foreach (var recipient in recipientsToDelete)
                {
                    recipients.Remove(recipient);
                    BacNetValidator.ControlChanged(null, null);
                }
                dgRecipient.DataSource = null;
                dgRecipient.DataSource = new List<Recipient>(recipients);
                dgRecipient.Refresh();
            }
        }

        private void dgRecipient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            DataGridViewRow R1 = (DataGridViewRow)(((DataGridView)sender).Rows[dgRecipient.SelectedCells[0].RowIndex]);
            string dtlname = R1.Cells["Name"].Value.ToString();
            OpenPopup(dtlname);
        }

        private void dgRecipient_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

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

        private void textObjectName_Validating(object sender, CancelEventArgs e)
        {
            string text = textObjectName.Text.Trim();

            if (!string.IsNullOrWhiteSpace(text) && text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Please resolve the errors first");
                return;
            }
            // Empty check
            if (string.IsNullOrEmpty(text))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Tag name cannot be empty.");
                return;
            }
            // Invalid character check
            foreach (char ch in text)
            {
                if (!char.IsLetterOrDigit(ch) && ch != '_' && ch != ' ')
                {
                    e.Cancel = true;
                    errorProvider.SetError(textObjectName, "Invalid character detected. Only letters, digits, and underscore (_) are allowed.");
                    return;
                }
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
            // If all validations pass
            errorProvider.SetError(textObjectName, "");
            e.Cancel = false;
        }
    }
}

