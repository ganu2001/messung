using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000.LadderLogic
{
    internal class ClsLineWithLabel : System.Windows.Forms.Label
    {
        bool ShowTextBefore = true;
        public ClsLineWithLabel(string LogicalAddress, bool showbefore = true)
        {
            this.Width = 100;
            this.AutoSize = false;
            Label Variable = new Label();
            ShowTextBefore = showbefore;
            if (!showbefore)
            {
                Variable.Location = new System.Drawing.Point(40, 0);
                Variable.TextAlign = ContentAlignment.TopRight;
                Variable.Text = LogicalAddress.Trim();
            }
            else
            {
                Variable.TextAlign = ContentAlignment.TopRight;
                Variable.Location = new System.Drawing.Point(this.Width - 25, 0);
                Variable.Text = LogicalAddress;
            }
            //ToolTip tt = new ToolTip();
            //if (LogicalAddress.Length > 15)
            //{
            //    Variable.Text = LogicalAddress.Substring(0, 15);
            //}
            //else
            //{
            //    Variable.Text = LogicalAddress;
            //}
            Font font = new Font(FontFamily.GenericSerif, 9);
            Variable.AutoSize = true;
            Variable.Font = font; 
            //Variable.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(Variable);
            this.Height = 20;

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
            if (!ShowTextBefore)
            {
                e.Graphics.DrawLine(p1, new Point(0, 10), new Point(40, 10));
            }
            else
            {
                e.Graphics.DrawLine(p1, new Point(230, 10), new Point(300, 10));
            }
        }
    }
}

