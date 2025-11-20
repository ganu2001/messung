using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMPS2000;

namespace XMPS2000
{
    public partial class InputBoxForm : Form, IXMForm
    {
        public string IPaddress { get; set; }
        public string Caller;
        public InputBoxForm()
        {
            InitializeComponent();
            
        }


       
        private void btnimport_Click(object sender, EventArgs e)
        {
            IPaddress = ipAddressControl.Text.ToString();
            this.Close();            
        }

        private void ipAddressControl_Click(object sender, EventArgs e)
        {

        }

        private void InputBoxForm_Load(object sender, EventArgs e)
        {
            //We are changing the button text on the fly to show Download or Upload as per the source
            if (Caller == "Post")
            {
                btnimport.Text = "Download";
            }
            else
            {
                btnimport.Text = "Upload";
            }
        }

        void IXMForm.OnShown()
        {
            return;
        }
    }
}
