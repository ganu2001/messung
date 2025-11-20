using System;
using System.Data;
using XMPS2000.Core.Base.Helpers;
using XMPS2000.DBHelper;
using System.Windows.Forms;
using XMPS2000.Resource_File;
using XMPS2000.Core;
using XMPS2000.Core.Devices;
using System.Linq;
using System.Net;
using Microsoft.VisualBasic.Devices;

namespace XMPS2000
{
    public partial class EthernetSettingsUserControl : UserControl
    {
        private Ethernet dataSource;
        XMPS xm;

        public EthernetSettingsUserControl()
        {
            InitializeComponent();
            ////Change names of Labels, take names from LabelNames Resource file
            this.lblipadd.Text = LabelNames.ipadd;
            this.lblsubnet.Text = LabelNames.subnet;
            this.lblgateway.Text = LabelNames.gateway;
            this.lblport.Text = LabelNames.port;

            this.lblchangeipadd.Text = "Change " + LabelNames.ipadd;
            this.lblchangesubnet.Text = "Change " + LabelNames.subnet;
            this.lblchangegateway.Text = "Change " + LabelNames.gateway;

            xm = XMPS.Instance;
            dataSource = new Ethernet();
            lblnetwork.Visible = false;
            NetworkNo.Visible = false;
            lblNetworkRange.Visible = false;
            if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
            {
                groupBox2.Text = "BACNet Settings";
                lblnetwork.Top = 35;
                NetworkNo.Top = 37;
                lblNetworkRange.Top = 38;
                lblport.Top = lblnetwork.Top + 35;
                Port.Top = NetworkNo.Top + 35;
                label8.Top = lblNetworkRange.Top + 35;
                Port.Value = 47808;
                lblnetwork.Visible = true;
                NetworkNo.Visible = true;
                lblNetworkRange.Visible = true;
            }
            else
                groupBox2.Text = "Port Settings";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(! IPAddress.TryParse(this.ipAddressControl.Text, out _))
            {
                MessageBox.Show("Invalid IP Address, enter valid values", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (! IPAddress.TryParse(this.ipAddressSubnet.Text, out _))
            {
                MessageBox.Show("Invalid subnet value, enter valid values", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (! IPAddress.TryParse(this.ipAddressGateway.Text, out _))
            {
                MessageBox.Show("Invalid Gateway value, enter valid values", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IPAddress.TryParse(this.ipAddressChangeControl.Text, out _))
            {
                MessageBox.Show("Invalid Changed IP Address value, enter valid values", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IPAddress.TryParse(this.ipAddressChangeSubnet.Text, out _))
            {
                MessageBox.Show("Invalid changed subnet value, enter valid values", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IPAddress.TryParse(this.ipAddressChangeGateway.Text, out _))
            {
                MessageBox.Show("Invalid changed Gateway value, enter valid values", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataSource.UseDHCPServer = this.checkBoxUseDHCP.Checked;
            if (!this.checkBoxUseDHCP.Checked)
            {
                dataSource.EthernetIPAddress = IPAddress.Parse(this.ipAddressControl.Text);
                dataSource.EthernetSubNet = IPAddress.Parse(this.ipAddressSubnet.Text);
                dataSource.EthernetGetWay = IPAddress.Parse(this.ipAddressGateway.Text);

                dataSource.ChangeIPAddress = IPAddress.Parse(this.ipAddressChangeControl.Text);
                dataSource.ChangeSubNet = IPAddress.Parse(this.ipAddressChangeSubnet.Text);
                dataSource.ChangeGetWay = IPAddress.Parse(this.ipAddressChangeGateway.Text);

                dataSource.Port = (int)this.Port.Value;
                dataSource.NetworkNo = (int)this.NetworkNo.Value;

            }
            else if (this.checkBoxUseDHCP.Checked)
            {
                //clearvalues();
                dataSource.EthernetIPAddress = IPAddress.Parse(this.ipAddressControl.Text);
                dataSource.EthernetSubNet = IPAddress.Parse(this.ipAddressSubnet.Text);
                dataSource.EthernetGetWay = IPAddress.Parse(this.ipAddressGateway.Text);

                dataSource.ChangeIPAddress = IPAddress.Parse(this.ipAddressChangeControl.Text);
                dataSource.ChangeSubNet = IPAddress.Parse(this.ipAddressChangeSubnet.Text);
                dataSource.ChangeGetWay = IPAddress.Parse(this.ipAddressChangeGateway.Text);

                dataSource.Port = (int)this.Port.Value;
                dataSource.NetworkNo = (int)this.NetworkNo.Value;

            }
            XMPS.Instance._connectedIPAddress = dataSource.EthernetIPAddress.ToString();
            xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "Ethernet");
            xm.LoadedProject.Devices.Add(dataSource);
            xm.MarkProjectModified(true);
            // --- update BacNet NetworkPort object ---
            if (XMPS.Instance.LoadedProject.BacNetIP != null)
            {
                if (XMPS.Instance.LoadedProject.BacNetIP.NetworkPort == null)
                    XMPS.Instance.LoadedProject.BacNetIP.NetworkPort = new XMPS2000.Core.BacNet.NetworkPort();

                var networkPort = XMPS.Instance.LoadedProject.BacNetIP.NetworkPort;
                networkPort.NetworkNumber = dataSource.NetworkNo.ToString();
                networkPort.IPAddress = dataSource.ChangeIPAddress.ToString();
                networkPort.IPSubnetMask = dataSource.ChangeSubNet.ToString();
                networkPort.IPDefaultGateway = dataSource.ChangeGetWay.ToString();
                networkPort.BacnetIPUDPPort = dataSource.Port.ToString();
                XMPS.Instance.LoadedProject.BacNetIP.NetworkPort = networkPort;
            }
            this.ParentForm.Close();
            this.ParentForm.DialogResult = DialogResult.OK;
        }

        private void DataBind()
        {
            dataSource = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
            this.checkBoxUseDHCP.Checked = dataSource.UseDHCPServer;
            this.ipAddressControl.Text = dataSource.EthernetIPAddressForXML;
            this.ipAddressSubnet.Text = dataSource.EthernetSubNetForXML;
            this.ipAddressGateway.Text = dataSource.EthernetGetWayForXML;

            this.ipAddressChangeControl.Text = dataSource.ChangeIPAddressForXML;
            this.ipAddressChangeSubnet.Text = dataSource.ChangeSubNetForXML;
            this.ipAddressChangeGateway.Text = dataSource.ChangeGetWayForXML;

            this.Port.Value = dataSource.Port;
            this.NetworkNo.Value = dataSource.NetworkNo < NetworkNo.Minimum ? NetworkNo.Minimum : dataSource.NetworkNo;

        }

        private void checkBoxUseDHCP_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseDHCP.Checked)
            {
                ipAddressControl.Enabled = false;
                ipAddressSubnet.Enabled = false;
                ipAddressGateway.Enabled = false;
                ipAddressChangeControl.Enabled = false;
                ipAddressChangeSubnet.Enabled = false;
                ipAddressChangeGateway.Enabled = false;
                if (! xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                    Port.Enabled = false;
                else
                    Port.Enabled = true;
            }
            else
            {
                ipAddressControl.Enabled = false;
                ipAddressSubnet.Enabled = false;
                ipAddressGateway.Enabled = false;
                ipAddressChangeControl.Enabled = true;
                ipAddressChangeSubnet.Enabled = true;
                ipAddressChangeGateway.Enabled = true;
                if (!xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                    Port.Enabled = false;
                else
                    Port.Enabled = true;
            }
            clearvalues();
        }

        private void clearvalues()
        {
            ipAddressControl.Text = "0.0.0.0";
            ipAddressSubnet.Text = "0.0.0.0";
            ipAddressGateway.Text = "0.0.0.0";
            ipAddressChangeControl.Text = "0.0.0.0";
            ipAddressChangeSubnet.Text = "0.0.0.0";
            ipAddressChangeGateway.Text = "0.0.0.0";
        }
        private void EthernetSettingsUserControl_Load(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}
