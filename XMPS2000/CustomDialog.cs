using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000
{
    internal class CustomDialog:Form
    {
        public CustomDialog()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Set up the custom dialog form
            Text = "XMPS2000";
            Width = 300;
            Height = 150;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            // Add custom buttons or controls
            Button buttonLoginWithDownload = new Button();
            buttonLoginWithDownload.AutoSize = true;
            buttonLoginWithDownload.Text = "With download";
            buttonLoginWithDownload.DialogResult = DialogResult.Yes;
            buttonLoginWithDownload.Location = new System.Drawing.Point(30, 50);
            buttonLoginWithDownload.Click += ButtonLoginWithDownload_Click;
            Controls.Add(buttonLoginWithDownload);

            Button buttonLoginWithoutDownload = new Button();
            buttonLoginWithoutDownload.AutoSize = true;
            buttonLoginWithoutDownload.Text = "Without download";
            buttonLoginWithoutDownload.DialogResult = DialogResult.No;
            buttonLoginWithoutDownload.Location = new System.Drawing.Point(160, 50);
            buttonLoginWithoutDownload.Click += ButtonLoginWithoutDownload_Click;
            Controls.Add(buttonLoginWithoutDownload);

            Label labelMessage = new Label();
            labelMessage.AutoSize = true;
            labelMessage.Location = new System.Drawing.Point(10, 10);
            labelMessage.Text = "Do You Want to Login ?";
            Controls.Add(labelMessage);
            
        }

        private void ButtonLoginWithDownload_Click(object sender, EventArgs e)
        {
            // Handle the "Login with download" button click event
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void ButtonLoginWithoutDownload_Click(object sender, EventArgs e)
        {
            // Handle the "Login without download" button click event
            DialogResult = DialogResult.No;
            Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CustomDialog
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomDialog";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }
    }
}
    