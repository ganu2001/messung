using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;

namespace XMPS2000
{
    public partial class DeviceMonitorInput : Form
    {
        public string startingAddress = string.Empty;
        public int length;
        public DeviceMonitorInput()
        {
            InitializeComponent();
            this.errorProvider = new ErrorProvider();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XM-Pro PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            startingAddress = textAddress.Text;
            length = (int)lengthNumericValue.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textAddress_Validating(object sender, CancelEventArgs e)
        {
            string _logicalAddress = textAddress.Text;
            if(_logicalAddress.StartsWith("I1") || _logicalAddress.StartsWith("Q0"))
            {
               errorProvider.SetError(textAddress, "Please add memory address varible");
                e.Cancel = true;
            }
            
            if (XMPS.Instance.LoadedProject.Tags.Any(T => T.LogicalAddress == _logicalAddress))
            {
            }
            else
            {
                if (errorProvider != null && textAddress != null)
                {
                    errorProvider.SetError(textAddress, "Please enter a valid address");
                }
                e.Cancel = true;
            }
        }
    }
}
