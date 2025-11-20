using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000
{
    public partial class FilterFormPopUp : UserControl
    {
        public event EventHandler<string> SelectedValueLogicalAddChanged;
        public event EventHandler<string> SelectedIndexLogicalAddChanged;
        public event EventHandler<string> SelectedValueTagsChanged;
        public event EventHandler<string> SelectedIndexTagsChnaged;
        public FilterFormPopUp()
        {
            InitializeComponent();
        }
        public FilterFormPopUp(List<string> LogicalAddress)
        {
            InitializeComponent();
            this.comboBoxTags.Visible = false;
            this.comboBoxLogicalAddress.Items.Clear();
            this.comboBoxLogicalAddress.DataSource=LogicalAddress;
        }

        public FilterFormPopUp(List<string> LogicalAddress,int index)
        {
            InitializeComponent();
            this.comboBoxLogicalAddress.Visible = false;
            this.comboBoxTags.Items.Clear();
            this.comboBoxTags.DataSource = LogicalAddress;
        }
        private void comboBoxTags_LogicalAddTextChanged(object sender, EventArgs e)
        {
            SelectedValueLogicalAddChanged?.Invoke(this, comboBoxLogicalAddress.Text);
        }

        private void comboBoxTags_SelectedLogicalAddIndexChanged(object sender, EventArgs e)
        {
            string selectedlogicalAddress=comboBoxLogicalAddress.SelectedItem.ToString();
            SelectedIndexLogicalAddChanged?.Invoke(this, selectedlogicalAddress);
        }

        private void comboBoxTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedlogicalAddress = comboBoxTags.SelectedItem.ToString();
            SelectedIndexTagsChnaged?.Invoke(this, selectedlogicalAddress);
        }

        private void comboBoxTags_TextChanged(object sender, EventArgs e)
        {
            SelectedValueTagsChanged?.Invoke(this, comboBoxTags.Text);
        }
    }
}
