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
using XMPS2000.Core.LadderLogic;

namespace XMPS2000.LadderLogic
{
    public partial class LadderDisplay : UserControl
    {
        List<ApplicationRung> AppData = new List<ApplicationRung>();
        int y = 20;
        public LadderDisplay(XMProForm parentForm) 
        {
            InitializeComponent();
            allocate();
        }

        private void allocate()
        {
            //AppData = SQLiteDataAccess.loadApplicationrecords();
            //foreach (Control cr in this.Controls)
            //{
            //    if ()
            //    {
            //        cr.Controls.Clear();
            //        cr.Height = 0;
            //    }
            //}
            //this.Refresh();
            foreach (ApplicationRung ApRec in AppData)
            {
                FunctionBlock functionBlock = new FunctionBlock(ApRec);
                functionBlock.Location = new System.Drawing.Point(10, y); ;
                functionBlock.Width = 1500;
                //functionBlock.Height = 220;
                this.Controls.Add(functionBlock);
                y = y + functionBlock.Height;
            }

            //FunctionBlock functionBlock1 = new FunctionBlock();
            //functionBlock1.Location = new System.Drawing.Point(10, 230); ;
            //functionBlock1.Width = 1500;
            //functionBlock1.Height = 220;
            //this.Controls.Add(functionBlock1);
            //FunctionBlock functionBlock2 = new FunctionBlock();
            //functionBlock2.Location = new System.Drawing.Point(10, 470); ;
            //functionBlock2.Width = 1500;
            //functionBlock2.Height = 220;
            //this.Controls.Add(functionBlock2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            allocate();
        }
    }
}
