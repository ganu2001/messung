using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000
{
    public partial class PrintPreviewForm : Form
    {

        public static Environment.SpecialFolder DataPath = Environment.SpecialFolder.ApplicationData;
        public PrintPreviewForm()
        {
            InitializeComponent();
        }

        private void PrintPreview_Load(object sender, EventArgs e)
        {
        }

    }
}
