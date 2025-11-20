using System;
using System.Data;
using System.Windows.Forms;
using XMPS2000.Resource_File;
using XMPS2000.DBHelper;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Types;
using XMPS2000.Core;
using System.Linq;

namespace XMPS2000
{
    public partial class COMSettingsUserControl: UserControl
    {
        private COMDevice dataSource;
        XMPS xm;

        public COMSettingsUserControl()
        {
            InitializeComponent();

            xm = XMPS.Instance;
            ////Change names of Labels, take names from LabelNames Resource file
            this.lblbaudrt.Text = LabelNames.baudrt;
            this.lbldatalen.Text = LabelNames.datalen;
            this.lblstopbit.Text = LabelNames.stopbit;
            this.lblparity.Text = LabelNames.parity;
            this.lblsenddelay.Text = LabelNames.senddelay;
            this.lblmininterface.Text = LabelNames.mininterface;
            this.lblcomtout.Text = LabelNames.comtout;
            this.lblcomtoutrange.Text = LabelNames.comtoutrange;
            this.lblnoretries.Text = LabelNames.noretries;
            this.lblnoretriesrange.Text = LabelNames.noretriesrange;

            dataSource = new COMDevice();
            this.comboBoxBaudRate.DataSource = CleanValues(Enum.GetNames(typeof(COMBaudRate)));
            this.comboBoxDataLength.DataSource = CleanValues(Enum.GetNames(typeof(COMDataLength)));
            this.comboBoxParity.DataSource = CleanValues(Enum.GetNames(typeof(COMParity)));
            this.comboBoxStopBit.DataSource = CleanValues(Enum.GetNames(typeof(COMStopBit)));

        }

        private string[] CleanValues(string[] vs)
        {
            int i = 0;
            foreach(var v in vs)
            {
                vs[i] = v.Replace("_", "");
                i++;
            }

            return vs;
        }

        private void DataBind()
        {
            this.dataSource = (COMDevice)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "COMDevice").FirstOrDefault();

            this.comboBoxBaudRate.SelectedItem = dataSource.BaudRate.ToString();
            this.comboBoxDataLength.SelectedItem = dataSource.DataLength.ToString();
            this.comboBoxParity.SelectedItem = dataSource.Parity.ToString();
            this.comboBoxStopBit.SelectedItem = dataSource.StopBit.ToString();
            this.SendDelay.Value = dataSource.SendDelay;
            this.NumberOfRetries.Value = dataSource.NumberOfRetries < this.NumberOfRetries.Minimum ? this.NumberOfRetries.Value : dataSource.NumberOfRetries;
            this.CommunicationTimeout.Value = dataSource.CommunicationTimeout < this.CommunicationTimeout.Minimum ? this.CommunicationTimeout.Value : dataSource.CommunicationTimeout;
            this.MinimumInterface.Value = dataSource.MinimumInterface;
        }
     
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataSource.BaudRate = Int32.Parse(this.comboBoxBaudRate.SelectedItem.ToString());
            dataSource.DataLength = Int32.Parse(this.comboBoxDataLength.SelectedItem.ToString());
            dataSource.Parity = this.comboBoxParity.SelectedItem.ToString();
            dataSource.StopBit = Int32.Parse(this.comboBoxStopBit.SelectedItem.ToString());
            dataSource.SendDelay = Int32.Parse(this.SendDelay.Value.ToString());
            dataSource.MinimumInterface = this.MinimumInterface.Value;
            dataSource.NumberOfRetries = Int32.Parse(this.NumberOfRetries.Value.ToString());
            dataSource.CommunicationTimeout  = Int32.Parse(this.CommunicationTimeout.Value.ToString());

            xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "COMDevice");
            xm.LoadedProject.Devices.Add(dataSource);
            xm.MarkProjectModified(true);
            this.ParentForm.Close();
            this.ParentForm.DialogResult = DialogResult.OK;
        }

        private void COMSettingsUserControl_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            grpadvance.Enabled = true; 
        }
    }
}
