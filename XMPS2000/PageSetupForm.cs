using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000
{
    public partial class PageSetupForm : Form, IXMForm
    {
        public string ProjectPath = "";

        public string PageSize { get; set; }
        public bool IsLandscape { get; set; }
        public float LeftMargin { get; set; }
        public float RightMargin { get; set; }
        public float TopMargin { get; set; }
        public float BottomMargin { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }

        public string TitleHeader { get; set; }
        public string ProjectName { get; set; }

        public string CustomerName { get; set; }
        public string TitleDate { get; set; }

        public string Profile { get; set; }

        public PageSetupForm()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void PageSetupForm_Load(object sender, EventArgs e)
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (PaperSize size in settings.PaperSizes)
            {
                cbpagesize.Items.Add(size.PaperName);
            }
            cbpagesize.SelectedItem = "A4";
            txtTitleProjNm.Text = ProjectPath.Split(Path.DirectorySeparatorChar).Last();
            txtTitleDate.Text = DateTime.Now.Day.ToString() + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month).ToString() + "-" + DateTime.Now.Year.ToString();
            txtFooter.Text = "Project Name : " + txtTitleProjNm.Text.ToString() + " Date : " + txtTitleDate.Text.ToString() + " Page No. @";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PageSize = cbpagesize.Text.ToString();
            IsLandscape = rdLandscape.Checked;
            LeftMargin = Convert.ToInt32(txtmLeft.Text);
            RightMargin = Convert.ToInt32(txtmRight.Text);
            TopMargin = Convert.ToInt32(txtmTop.Text);
            BottomMargin = Convert.ToInt32(txtmBottom.Text);
            Header = txtHeader.Text.ToString();
            Footer = txtFooter.Text.ToString();
            TitleHeader = txtTitleHeader.Text.ToString();
            ProjectName = txtTitleProjNm.Text.ToString();
            CustomerName = txtTitleCustNm.Text.ToString();
            TitleDate = txtTitleDate.Text.ToString();
            Profile = txtTitleProfile.Text.ToString();
            this.Close();
        }

        public void OnShown()
        {
            throw new NotImplementedException();
        }
    }
}
