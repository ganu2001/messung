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
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.LadderLogic;


namespace XMPS2000
{
    public partial class GenerateDisabalingVariable : Form
    {
        public DialogResult dialogResult=DialogResult.Cancel;
        public GenerateDisabalingVariable()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GenerateDisabalingVariable_Load(object sender, EventArgs e)
        {
            LabelMsg.Visible = false;
            comboBoxBitAddress.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxBitAddress.DataSource = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F") || T.LogicalAddress.Contains(".")).Select(T=>T.Tag).ToList();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string bitAddress=(string)comboBoxBitAddress.SelectedValue;
            string LogicalAddress = comboBoxBitAddress.Text;
            if(!LogicalAddress.StartsWith("F2") && bitAddress==null)
            {
                LabelMsg.Visible = true;
                LabelMsg.Text = "Invalid Logical Address Use Only Bit Address";
                LabelMsg.ForeColor = Color.Red;
                return;
            }
            dialogResult = DialogResult.OK;
           // string logicalAddress = XMProValidator.GetTheAddressFromTag(bitAddress.ToString());
            this.Close();
        }
    }
}
