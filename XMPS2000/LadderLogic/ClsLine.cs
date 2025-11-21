using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace XMPS2000.LadderLogic
{
    internal class ClsLine : System.Windows.Forms.Label
    {
        public ClsLine(string LogicalAddress)
        {
            this.Width = 100;
            this.AutoSize = false;
            Label Variable = new Label();
            Variable.Location = new System.Drawing.Point(2, 15);
            //ToolTip tt = new ToolTip();
            //if(LogicalAddress.Length > 15)
            //{
            //    Variable.Text = LogicalAddress.Substring(0, 15);
            //}
            //else
            //{
            //    Variable.Text = LogicalAddress;
            //}
            
            //tt.SetToolTip(Variable, LogicalAddress);
            Variable.AutoSize = true;
            this.Controls.Add(Variable);
            this.Height = 28;
            //Graphics g = this.CreateGraphics();
            //Pen p1 = new Pen(Color.Black, 3);
            //g.DrawLine(p1, new Point(0, 10), new Point(this.Width, 10));
            //g.DrawLine(p1, new Point(0, 0), new Point(150, 0));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.AutoSize = false;
            //draw a string of text label  
            //Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, 50, 50), c1, c2, angle);
            //e.Graphics.DrawString(this.Text, this.Font, b, new Point(0, 0));
            Pen p1 = new Pen(Color.Black, 1);
            e.Graphics.DrawLine(p1, new Point(0, 10), new Point(this.Width, 10));
        }
    }
}
