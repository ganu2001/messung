using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using XMPS2000.Core.App;

namespace XMPS2000
{
    public partial class FirmwareUpdate : UserControl
    {
        public FirmwareUpdate()
        {
            InitializeComponent();
        }


        private void BinBrowse_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            string sDefaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MessungSystems\XMPS2000\XM Projects");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Source Code File (*.bin)| *.bin";
            openFileDialog.Title = "Browse bin Files";
            openFileDialog.InitialDirectory = sDefaultPath;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            DialogResult dr = openFileDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                var projectName = openFileDialog.FileName;
                TextBin.Text = projectName.ToString();



                RecentProject recentProject = new RecentProject();

                recentProject.ProjectPath = projectName;
                recentProject.ProjectName = Path.GetFileName(projectName);
            }
        }

        private void UpdateFirmware_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to Update the Firmware to the Target?" + Environment.NewLine + "Make sure the PLC is in Stop Mode & Do Not Power off the System", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                PLCCommunications pLCCommunications = new PLCCommunications();
                //tssStatusLabel.Text = pLCCommunications.StopPLC();
                var resultStop = pLCCommunications.UpdateFirmwares(TextBin.Text.ToString());
                MessageBox.Show(resultStop.Item1, "XMPS 2000");
            }
            else
            if (dialogResult == DialogResult.No)
            {
                //do something else
                return;
            }
        }
    }
}
