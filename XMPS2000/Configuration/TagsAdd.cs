using System;
using System.Data;
using System.Windows.Forms;
using XMPS2000.Resource_File;
using XMPS2000.DBHelper;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Types;
using XMPS2000.Core;
using System.Linq;
using XMPS2000.Core.Base;
using System.ComponentModel;
//using XMPS2000.Core.Base.Helpers;
using XMPS2000.LadderLogic;

namespace XMPS2000
{
    public partial class TagsAdd : UserControl
    {
        private XMIOConfig dataSource;
        XMPS xm;


        public TagsAdd()
        {
            InitializeComponent();

            xm = XMPS.Instance;
            ////Change names of Labels, take names from LabelNames Resource file
            this.labelType.Text = LabelNames.Type;
            this.labelLabel.Text = LabelNames.Label;
            this.labelLogicalAddress.Text = LabelNames.Logical_Address;
            this.labelTag.Text = LabelNames.Tag;
            dataSource = new XMIOConfig();
            //comboBoxIOType.Items.Add(IOType.DataType);
            comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6).ToList();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var Logicname = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.Tag == this.textBoxTag.Text && d.LogicalAddress != this.textBoxLogicalAddress.Text).FirstOrDefault();
            if (Logicname != null)
            {
                MessageBox.Show("Tag name is already used for Logical Address " + Logicname.LogicalAddress, "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var LogicId = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == this.textBoxLogicalAddress.Text).FirstOrDefault();
            if (LogicId != null)
            {
                MessageBox.Show("Logical Id is already used ", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dataSource.Model = "";
            dataSource.Label = this.comboBoxIOType.SelectedItem.ToString();
            dataSource.LogicalAddress = this.textBoxLogicalAddress.Text;
            dataSource.Tag = this.textBoxTag.Text;
            dataSource.IoList = (IOListType)IOListType.NIL;
            dataSource.Type = (IOType)IOType.DataType;
            dataSource.Key = xm.LoadedProject.Tags.Max(K => K.Key) + 1;
            xm.LoadedProject.Tags.Add(dataSource);
            xm.MarkProjectModified(true);
            this.ParentForm.Close();
            this.ParentForm.DialogResult = DialogResult.OK;
        }

        private void textBoxLogicalAddress_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateOperand(textBoxLogicalAddress, e);
        }

        /// <summary>
        /// Validate operands depending on the type of control 
        /// </summary>
        /// <param name="control">Name of the control from whoes validate this call is generated.</param>
        /// <param name="e">This parameter will specify whether to add or update the line.</param>
        /// <returns>Describe return value.</returns>
        private void ValidateOperand(Control control, CancelEventArgs e)
        {
            bool validationSuccessful = true;
            string error = string.Empty;

            if (string.IsNullOrEmpty(control.Text))     // Allow untouched or emptied operands.
            {
                validationSuccessful = true;
            }
            else
            {
                validationSuccessful = ValidateAddressOperand(control, out error);
            }

            if (validationSuccessful)
            {
                e.Cancel = false;
                errorProvider1.SetError(control, null);
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(control, error);
            }
        }


        /// <summary>
        /// Validating Address type of operands
        /// </summary>
        /// <param name="control">Send the nmae of control.</param>        
        /// <returns>True if operand is valid else false.</returns>
        /// <returns>Error decription as String.</returns>
        private bool ValidateAddressOperand(Control control, out string error)
        {
            if (string.IsNullOrEmpty(control.Text))     // Allow untouched or emptied operands.
            {
                error = "";
                return true;

            }
            string address = control.Text;

            if (address == "-") address = control.Text;
            bool validationSuccessful;

            string dataType = ((DataType)comboBoxIOType.SelectedItem).Text;
            switch (dataType)
            {
                case "Bool":
                    error = "Invalid Bit Address";
                    validationSuccessful = address.IsValidBitAddress();
                    break;
                case "Real":
                    error = "Invalid Word address for Real data type";
                    validationSuccessful = address.IsValidRealWordAddress();
                    break;
                default:
                    error = "Invalid Word address";
                    validationSuccessful = address.IsValidWordAddress();
                    break;
            }

            if (validationSuccessful)
            {
                error = string.Empty;
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// Check if the Word Address entered by the user is Valid or not
        /// </summary>
        /// <param name="control"></param> Name of Control 
        /// <param name="e"></param> Cancel Event Handler
        /// <param name="errorMessageToDisplay"></param>Error to be shown
        private void ValidateWordAddress(Control control, CancelEventArgs e, string errorMessageToDisplay)
        {
            if (control.Text.IsValidWordAddress())
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
        /// Check if the Bit Address entered by the user is Valid or not
        /// </summary>
        /// <param name="control"></param> Name of Control 
        /// <param name="e"></param> Cancel Event Handler
        /// <param name="errorMessageToDisplay"></param>Error to be shown

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

        private void textBoxTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar) != 8 && (e.KeyChar) != 95)
            {
                e.Handled = true;
            }
        }
    }
}
