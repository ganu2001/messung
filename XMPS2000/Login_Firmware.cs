using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000
{
    public partial class Login_Firmware : UserControl
    {
        Thread th;

        public Login_Firmware()
        {
            InitializeComponent(); 
            UserID.Text = "admin";
            Password.Text = "admin";
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
            th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            //var firmwarebin = new FirmwareBin();
            //firmwarebin.ShowDialog();
        }

        
        private void opennewform()
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "Update Firmware";
            FirmwareUpdate userControl = new FirmwareUpdate();
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            tempForm.Controls.Add(userControl);
            var frmTemp2 = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp2);
        }
    }
}
