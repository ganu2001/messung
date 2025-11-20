using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using XMPS2000.DBHelper;

namespace XMPS2000
{
    public class XMProForm : Form
    {
        public XMProForm()
        {  

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XMProForm
            // 
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(630, 554);
            this.Name = "XMProForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
