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
using XMPS2000.Core.Base.Helpers;
using XMPS2000.LadderLogic;

namespace XMPS2000
{
    public partial class IOConfigAdd: UserControl
    {
        private XMIOConfig dataSource;
        XMPS xm;


        public IOConfigAdd()
        {
            InitializeComponent();

            xm = XMPS.Instance;
            ////Change names of Labels, take names from LabelNames Resource file
            this.labelType.Text = LabelNames.Type;
            this.labelLabel.Text = LabelNames.Label;
            this.labelLogicalAddress.Text = LabelNames.Logical_Address;
            this.labelTag.Text = LabelNames.Tag;
            dataSource = new XMIOConfig();
            var model = RemoteModule.List.Find(x => x.Name.Equals("Other"));
            comboBoxIOType.DataSource = model.SupportedTypesAndIOs.Select(x => x.TypeText).Distinct().ToList();

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

            dataSource.Model = "Other";
            dataSource.Label = this.textBoxLabel.Text;
            dataSource.LogicalAddress = this.textBoxLogicalAddress.Text;
            dataSource.Tag = this.textBoxTag.Text;
            dataSource.IoList = (IOListType)Enum.Parse(typeof(IOListType), "RemoteIO");
            dataSource.Type = (IOType)Enum.Parse(typeof(IOType), this.comboBoxIOType.Text.ToString().Replace(" ",""));
            xm.LoadedProject.Tags.Add(dataSource);
            xm.MarkProjectModified(true);
            this.ParentForm.Close();
            this.ParentForm.DialogResult = DialogResult.OK;
        }

        private void textBoxLogicalAddress_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(textBoxLogicalAddress.Text.ToString().Contains("."))
            {
                ValidateBitAddress(textBoxLogicalAddress, e, "Inavalid bit address or value for Enable field");
            }
            else
            {
                ValidateWordAddress(textBoxLogicalAddress, e, "Inavalid bit address or value for Enable field");
            }
            
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
    }
}
