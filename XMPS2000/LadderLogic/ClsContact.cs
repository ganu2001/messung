using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace XMPS2000.LadderLogic
{
    internal class ClsContact : System.Windows.Forms.Label
    {
        private string Address = "";
        Label Variable = new Label();
        public ClsContact(string LogicalAddress)
        {
            this.Width = 300;
            this.AutoSize = false;

            Variable.Location = new System.Drawing.Point(0, 15);
            Variable.Text = LogicalAddress.Substring(0, LogicalAddress.Length - 13);
            Variable.Tag = LogicalAddress;
            Variable.AutoSize = true;
            //ToolTip tt = new ToolTip();
            //if (LogicalAddress.Length > 15)
            //{
            //    Variable.Text = LogicalAddress.Substring(0, 15);
            //}
            //else
            //{
            //    Variable.Text = LogicalAddress;
            //}
            string CheckName;
            CheckName = LogicalAddress.Substring(LogicalAddress.IndexOf(':') + 1, LogicalAddress.Length - (LogicalAddress.IndexOf(':') + 1)).Trim();
            if (CheckName.LastIndexOf(':') > 3)
            {
                CheckName = LogicalAddress.Substring(LogicalAddress.LastIndexOf(':'), 5).Trim();
                Variable.Text = LogicalAddress.Replace(CheckName, "");
            }
            else
            {
                Variable.Text = LogicalAddress;
            }
            Font font = new Font(FontFamily.GenericSerif, 9);
            Variable.Font = font;
            this.Controls.Add(Variable);
            Address = LogicalAddress;
            Pen p1 = new Pen(Color.Black, 2);
            Graphics g = this.CreateGraphics();
            g.DrawLine(p1, new Point(0, 0), new Point(150, 0));
            this.Height = 28;
        }

        private void Variable_MouseHover(object sender, EventArgs e)
        {
            Variable.Text = Variable.Tag.ToString();
        }

        private void Variable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Select the Vaiable from the list");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen p1 = new Pen(Color.Black, 1);
            int midwidth = 190;
            //Draw Horizontal Lines
            e.Graphics.DrawLine(p1, new Point(0, 10), new Point(midwidth, 10));
            e.Graphics.DrawLine(p1, new Point(midwidth + 10, 10), new Point(this.Width, 10));
            //Draw Parellel Lines
            e.Graphics.DrawLine(p1, midwidth, 0, midwidth, 20);
            e.Graphics.DrawLine(p1, midwidth + 10, 0, midwidth + 10, 20);
            //Draw Negation Line
            if (Address.Contains("~"))
            {
                e.Graphics.DrawLine(p1, midwidth + 10, 0, midwidth, 20);
            }
            string CheckName;
            CheckName = Address.Substring(Address.IndexOf(':') + 1, Address.Length - (Address.IndexOf(':') + 1)).Trim();
            if (CheckName.LastIndexOf(':') > 3)
            {
                CheckName = Address.Substring(Address.LastIndexOf(':') + 3, 1).Trim();

                int angle = 90;
                Color c1 = Color.Black; // .FromArgb(color1Transparent, color1);
                Color c2 = Color.Black; // .FromArgb(color2Transparent, color2);
                Font font = new Font(FontFamily.GenericSerif, 10, FontStyle.Regular);
                //draw a string of text label  
                Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, 50, 50), c1, c2, angle);
                e.Graphics.DrawString(CheckName, font, b, new Point(midwidth - 2, 2));
            }
        }
    }
}
