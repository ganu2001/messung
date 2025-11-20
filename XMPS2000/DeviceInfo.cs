using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Serialization;
using XMPS2000.Core;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Types;

namespace XMPS2000
{
    public partial class DeviceInfo : Form
    {
        private XMPS xm;
        public DeviceInfo()
        {
            this.xm = XMPS.Instance;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (xm.LoadedProject != null)
            {

                //FOR DATAGRIDVIEW 1 EXPANSION COUNT
                dataGridView1.AllowUserToAddRows = false;
                dataGridView2.AllowUserToAddRows = false;

                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
                this.labelModel.Text = $"{xm.PlcModel}";
                //  int ethPortCount = xm.LoadedProject.Devices.Where(D => D.Type.Equals(typeof(Ethernet))).Count();
                // this.labelEthP.Text = ethPortCount.ToString();
                List<RemoteModule> expansionModuleList = RemoteModule.List.FindAll(x => x.IOType.Equals("Expansion I/O")).ToList();

                for (int i = 0; i < expansionModuleList.Count; i++)
                {
                    int unitsCount = 0;
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["Expansion"].Value = expansionModuleList[i];
                    int exapansionCount = xm.LoadedProject.Tags.Where(T => T.IoList == IOListType.ExpansionIO && T.Model.StartsWith(expansionModuleList[i].ToString())).Count();
                    if (exapansionCount > 0)
                    {
                        for (int j = 0; j < expansionModuleList[i].SupportedTypesAndIOs.Count(); j++)
                        {
                            unitsCount += expansionModuleList[i].SupportedTypesAndIOs[j].Units;
                        }
                        dataGridView1.Rows[i].Cells["Total_Quantity"].Value = exapansionCount / unitsCount;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["Total_Quantity"].Value = 0;
                    }
                }

                //FOR DATAGRIDVIEW 2 IO COUNT
                DataGridViewColumn column = dataGridView2.Columns[0];
                column.HeaderText = "Module Name" + " " + $"({xm.PlcModel})";
                List<string> InputOutputList = new List<string>();
                InputOutputList.Add("DI");
                InputOutputList.Add("DO");
                InputOutputList.Add("AI");
                InputOutputList.Add("AO");
                InputOutputList.Add("UI");
                InputOutputList.Add("UO");

                for (int i = 0; i < InputOutputList.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells["Module_Name"].Value = InputOutputList[i];
                    dataGridView2.Rows[i].Cells["On_Board"].Value = xm.LoadedProject.Tags.Where(T => T.Label.StartsWith(InputOutputList[i]) && T.IoList == IOListType.OnBoardIO
                                && !(T.Tag.EndsWith("_OR") || T.Tag.EndsWith("_OL"))).Count();
                    dataGridView2.Rows[i].Cells["Expansion1"].Value = xm.LoadedProject.Tags.Where(T => T.Label.StartsWith(InputOutputList[i]) && T.IoList == IOListType.ExpansionIO
                                && !(T.Tag.EndsWith("_OR") || T.Tag.EndsWith("_OL"))).Count();
                    int onbord = (int)dataGridView2.Rows[i].Cells["On_Board"].Value;
                    int expansion = (int)dataGridView2.Rows[i].Cells["Expansion1"].Value;
                    dataGridView2.Rows[i].Cells["Total"].Value = onbord + expansion;

                }
            }
            else
            {
                this.tabControl1.Hide();
                this.labelModel.Text = "Not Avalible";
                this.labelComPort.Hide();
                this.labelComp.Hide();
                this.labelEthPort.Hide();
                this.labelEthP.Hide();
                this.dataGridView1.Hide();
                this.dataGridView2.Hide();
            }
        }

        private void DeviceInfo_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 1);
            e.Graphics.DrawRectangle(pen, new Rectangle(5, 5, 485, 310));
        }

        private void buttonok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
