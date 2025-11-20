using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000.LadderLogic
{
    internal class ClsCoil : System.Windows.Forms.Label 
    {
        private int angle = 90;
        public ClsCoil(string LogicalAddress)
        {
            this.Width = 100;
            this.AutoSize = false;
            Label Variable = new Label();
            Variable.Location = new System.Drawing.Point(35, 0);
            //ToolTip tt = new ToolTip();
            //if (LogicalAddress.Length > 15)
            //{
            //    Variable.Text = LogicalAddress.Substring(0, 15);
            //}
            //else
            //{
            //    Variable.Text = LogicalAddress;
            //}
            //tt.SetToolTip(Variable, LogicalAddress);
            Variable.Text = LogicalAddress.Trim();
            Variable.AutoSize = true;
            Font font = new Font(FontFamily.GenericSerif, 9);
            Variable.Font = font;
            this.Controls.Add(Variable);  
            Pen p1 = new Pen(Color.Black, 3);
            Graphics g = this.CreateGraphics();
            g.DrawLine(p1, new Point(0, 0), new Point(150, 0));
            this.Height = 18;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color c1 = Color.Black; // .FromArgb(color1Transparent, color1);
            Color c2 = Color.Black; // .FromArgb(color2Transparent, color2);
            this.AutoSize = false;
            Font font = new Font("Thoma", 10, FontStyle.Regular);
            //draw a string of text label  
            Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, 50, 50), c1, c2, angle);
            e.Graphics.DrawString("O", font, b, new Point(22, -2));
            Pen p1 = new Pen(Color.Black, 1);
            e.Graphics.DrawLine(p1, new Point(0, 5), new Point(25, 5));
            //System.Drawing.Rectangle hl_rect = new Rectangle();
            //hl_rect.X = 25;
            //hl_rect.Y = 10;
            //hl_rect.Height = 20;
            //hl_rect.Width = 20;
            //e.Graphics.DrawPie(p1, hl_rect, 0, 360);
        }
    }
}
