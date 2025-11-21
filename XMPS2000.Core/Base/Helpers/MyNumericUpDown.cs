using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000.Core.Base.Helpers
{
    public class MyNumericUpDown : NumericUpDown
    {
        public MyNumericUpDown()
        {
            Controls[0].Hide();
        }

        protected override void OnTextBoxResize(object source, EventArgs e)
        {
            Controls[1].Width = Width - 4;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
       {
            // Allow only backspace and numeric keys... Implemented to avoid decimal (.) and comma 
            if (e.KeyChar != 8 && e.KeyChar < 48 && e.KeyChar !=3 && e.KeyChar !=22 || e.KeyChar > 57) //  3 & 22 for copy and paste
            {
                e.Handled = true;
            }
        }
    }
}
