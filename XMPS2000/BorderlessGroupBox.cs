using System;
using System.Windows.Forms;
using System.Drawing;


namespace XMPS2000
{
   
    public partial class BorderlessGroupBox : GroupBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw the text in the GroupBox
            e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, 0, 0);
        }
    }

    // Usage in your form
    public class MyForm : Form
    {
        private BorderlessGroupBox myGroupBox;

        public MyForm()
        {
            myGroupBox = new BorderlessGroupBox();
            myGroupBox.Text = "My Group Box";
            myGroupBox.Location = new Point(10, 10);
            myGroupBox.Size = new Size(200, 100);
            // Add other controls to the group box here

            this.Controls.Add(myGroupBox);
        }
    }
}
