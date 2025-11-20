using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000
{
    public partial class MQTTLicense : Form
    {
        public MQTTLicense()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                txt_FilePath.Text = selectedFileName;
            }
        }

        private void txt_FilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnFileDownlaod_click_Click(object sender, EventArgs e)
        {
            string filePath = txt_FilePath.Text;
            string ipAddress = ipAddressControl1.Text; 

           
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please select a file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (string.IsNullOrEmpty(ipAddress))
            {
                MessageBox.Show("Please enter a valid IP address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
               
                string fileName = Path.GetFileName(filePath);

               
                string newFilePath = Path.Combine("C:\\Users\\sai\\source\\repos\\XMPS2000\\XMPS2000\\ProjectIPFile\\", $"{ipAddress}_{fileName}");

               
                File.Copy(filePath, newFilePath);

                MessageBox.Show("File downloaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ipAddressControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
