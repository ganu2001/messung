using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000.LadderLogic
{
    internal class ClsPowerLine : System.Windows.Forms.Label
    {
        public ClsPowerLine()
        {
            //LHeight = height;   
            this.Height = 100;
            this.AutoSize = false ;
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
            Pen p1 = new Pen(Color.Black, 3);
            e.Graphics.DrawLine(p1, new Point(3, 0), new Point(3, this.Height));
        }
    }
}

