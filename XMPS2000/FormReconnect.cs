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
    public partial class FormReconnect : Form
    {
        public string status = "";
        public FormReconnect()
        {
            InitializeComponent();
        }

        public FormReconnect(string Messagetoshow)
        {
            InitializeComponent();
            label1.Text = Messagetoshow;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            status = "Cancel";
            this.Close(); 
        }

        private void btnreconnect_Click(object sender, EventArgs e)
        {
            status = "Reconnect";
            this.Close();
        }
    }
}
