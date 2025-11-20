using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using XMPS2000.Bacnet;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Devices;
using IPAddress = System.Net.IPAddress;

namespace XMPS2000
{
    public class EasyScan : Form
    {
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblDevices;
        private List<string> connectedMachines;
        private DataGridView dataGridViewMAC;
        private CheckBox checkBoxUseDHCP;
        private GroupBox grpbxDHCP;
        private IPAddressControlLib.IPAddressControl txtGetway;
        private IPAddressControlLib.IPAddressControl txtSubnet;
        private IPAddressControlLib.IPAddressControl txtIP;
        private Label lblGetway;
        private Label lblsubnetworkmask;
        private Label lblIP;
        private TextBox txtMac;
        private Label lblMac;
        private Button buttonApply;
        private Button buttoncopytoethernet;
        private List<string> filteredMachines;
        private DataGridViewTextBoxColumn Column1;
        private Label labelError;
        private List<string> listofdevices;
        public EasyScan()
        {
            InitializeComponent();
            dataGridViewMAC.DataSource = listofdevices;
            connectedMachines = new List<string>();
            filteredMachines = new List<string>();
            listofdevices = new List<string>();
            GetConnection();
            if (XMPS.Instance.LoadedProject != null)
            {
                Ethernet ethernet = (Ethernet)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
                checkBoxUseDHCP.Checked = ethernet.UseDHCPServer;
            }
        }


        static public uint ReturnFirtsOctet(string ipAddress)
        {
            System.Net.IPAddress iPAddress = System.Net.IPAddress.Parse(ipAddress);
            byte[] byteIP = iPAddress.GetAddressBytes();
            uint ipInUint = (uint)byteIP[0];
            return ipInUint;
        }

        //Getting SubnetMask from IPAddress
        //https://asp-blogs.azurewebsites.net/razan/finding-subnet-mask-from-ip4-address-using-c
        static IPAddress GetSubnetMask(IPAddress ipAddress)
        {
            uint firstOctet = ReturnFirtsOctet(ipAddress.ToString());
            if (firstOctet >= 0 && firstOctet <= 127)
                return IPAddress.Parse("255.0.0.0");
            else if (firstOctet >= 128 && firstOctet <= 191)
                return IPAddress.Parse("255.255.0.0");
            else if (firstOctet >= 192 && firstOctet <= 223)
                return IPAddress.Parse("255.255.255.0");
            else return IPAddress.Parse("0.0.0.0");
        }
        //Find Getway of IP Address
        static IPAddress GetGateway(string ipAddress)
        {
            return NetworkInterface.GetAllNetworkInterfaces()
    .Where(n => n.OperationalStatus == OperationalStatus.Up)
    .SelectMany(n => n.GetIPProperties().GatewayAddresses)
    .Select(g => g.Address.ToString())
    .FirstOrDefault() is string gateway && !string.IsNullOrEmpty(gateway)
        ? IPAddress.Parse(gateway)
        : null;
        }
        // Accessing connected machines in network
        private void GetConnectedMachines()
        {
            connectedMachines.Clear();
            try
            {
                using (Process process = new Process())
                {
                    // Get all network interfaces
                    var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (var ni in networkInterfaces)
                    {
                        // Get the IP properties for each network interface
                        var ipProperties = ni.GetIPProperties();

                        // Loop through all unicast IP addresses
                        foreach (var ip in ipProperties.UnicastAddresses)
                        {
                            // Only consider IPv4 addresses (ARP is not used for IPv6)
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                string interfaceIp = ip.Address.ToString();
                                Console.WriteLine($"Getting ARP for interface IP: {interfaceIp}");

                                // Run the arp -a command for the specific interface IP
                                //GetArpForInterface(interfaceIp);
                                ProcessStartInfo psi = new ProcessStartInfo
                                {
                                    FileName = "arp",
                                    Arguments = $"-a",
                                    RedirectStandardOutput = true,
                                    UseShellExecute = false,
                                    CreateNoWindow = true
                                };
                                process.StartInfo = psi;
                                process.Start();
                                string arpOutput = process.StandardOutput.ReadToEnd();
                                process.WaitForExit();
                                ParseArpOutput(arpOutput);
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error running arp command: " + ex.Message);
            }
        }
        //Access Dynamically connected machines.
        private void ParseArpOutput(string arpOutput)
        {
            string[] lines = arpOutput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if ((line.Contains("dynamic") || line.Contains("statc")) && line.Length >= 17)
                {
                    string machineInfo = line.Substring(line.IndexOfAny("0123456789".ToCharArray()));
                    connectedMachines.Add(machineInfo);
                }
            }
        }
        //Getting MacAddress wchich starts with "00-80"
        private void ExtractMacAddresses()
        {
            try
            {
                foreach (string machineInfo in connectedMachines)
                {
                    string[] machineParts = machineInfo.Split(' ');
                    int i = 0;
                    foreach (string part in machineParts)
                    {
                        if ((part.Contains(".") && (machineInfo.Contains("00-81"))) || machineInfo.Contains("8c-1f-64"))
                        {
                            if (!filteredMachines.Any(m => m.Contains(part)))
                            {
                                filteredMachines.Add(machineInfo);
                                i++;
                            }
                        }
                    }
                }
                if (filteredMachines.Count > 0)
                {
                    dataGridViewMAC.AllowUserToAddRows = false;
                    foreach (var machine in filteredMachines)
                    {
                        var parts = machine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length > 1)
                        {
                            string macAddress = parts[1]; // "00-81-e4-00-10-00"
                            string ipAddress = parts[0]; // "192.168.0.50"
                            listofdevices.Add(ipAddress);
                            int rowIndex = dataGridViewMAC.Rows.Add();
                            dataGridViewMAC.Rows[rowIndex].Cells[0].Value = ipAddress;
                            if (ipAddress == XMPS.Instance._connectedIPAddress)
                            {
                                dataGridViewMAC.Rows[rowIndex].DefaultCellStyle.Font = new Font(dataGridViewMAC.Font, FontStyle.Bold);
                            }
                            else
                            {
                                dataGridViewMAC.Rows[rowIndex].DefaultCellStyle.Font = new Font(dataGridViewMAC.Font, FontStyle.Regular);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to fetch mac address from network data : " + ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataGridViewMAC.Rows.Clear();
            listofdevices.Clear();
            PLCCommunications pLCCommunications = new PLCCommunications();
            pLCCommunications.PingAllAddresses();
            this.txtIP.Clear();
            this.txtSubnet.Clear();
            this.txtGetway.Clear();
            this.txtMac.Clear();
            connectedMachines.Clear();
            filteredMachines.Clear();
            GetConnection();
        }

        private void GetConnection()
        {
            this.Cursor = Cursors.WaitCursor;
            GetConnectedMachines();
            ExtractMacAddresses();
            if (dataGridViewMAC.RowCount == 0 && this.Visible)
                MessageBox.Show($"No device connected, please check the connection...", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Cursor = Cursors.Default;
        }


        private void dataGridViewMAC_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e?.RowIndex >= 0 && e.RowIndex < dataGridViewMAC.Rows.Count)
            {
                try
                {
                    string clickedIPAddress = dataGridViewMAC.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString()?.Trim();
                    //this.txtIP.Text = "";
                    this.txtSubnet.Text = "";
                    this.txtGetway.Text = "";
                    this.txtMac.Text = "";
                    // Check for blank/null IP address and validate format
                    if (string.IsNullOrWhiteSpace(clickedIPAddress) || !IPAddress.TryParse(clickedIPAddress, out _))
                    {
                        if (!string.IsNullOrWhiteSpace(clickedIPAddress))
                            MessageBox.Show("Please select a valid IP address.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    PLCCommunications pLCCommunications = new PLCCommunications();
                    string connectionDetails = pLCCommunications.GetConnectionDetails(clickedIPAddress);

                    if (!string.IsNullOrEmpty(connectionDetails) && !connectionDetails.Equals("Error", StringComparison.OrdinalIgnoreCase))
                    {
                        // Split and validate we have enough parts
                        string[] parts = connectionDetails.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length >= 4 &&
                            IPAddress.TryParse(parts[0]?.Trim(), out _) &&
                            IPAddress.TryParse(parts[1]?.Trim(), out _))
                        {
                            this.txtIP.Text = clickedIPAddress.Trim();
                            this.txtSubnet.Text = parts[1].Trim();
                            this.txtGetway.Text = parts[2]?.Trim() ?? "";
                            this.txtMac.Text = parts[3]?.Trim() ?? "";
                        }
                        else
                        {
                            MessageBox.Show("Invalid connection details format received from device.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        this.txtIP.Text = clickedIPAddress;

                        MessageBox.Show($"Failed to get connection details for {clickedIPAddress}. Please check the device connection.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ValidateNetworkSettings();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing device connection: {ex.Message}", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool IsValidIPv4Address(string input)
        {
            // Remove any surrounding braces or whitespace
            string cleanInput = input.Trim().Trim('{', '}');

            // Regular expression for IPv4 address format (0-255.0-255.0-255.0-255)
            string pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

            return System.Text.RegularExpressions.Regex.IsMatch(cleanInput, pattern);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasyScan));
            this.lblDevices = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dataGridViewMAC = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxUseDHCP = new System.Windows.Forms.CheckBox();
            this.grpbxDHCP = new System.Windows.Forms.GroupBox();
            this.labelError = new System.Windows.Forms.Label();
            this.txtGetway = new IPAddressControlLib.IPAddressControl();
            this.txtSubnet = new IPAddressControlLib.IPAddressControl();
            this.txtIP = new IPAddressControlLib.IPAddressControl();
            this.lblGetway = new System.Windows.Forms.Label();
            this.lblsubnetworkmask = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtMac = new System.Windows.Forms.TextBox();
            this.lblMac = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttoncopytoethernet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMAC)).BeginInit();
            this.grpbxDHCP.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDevices
            // 
            this.lblDevices.Location = new System.Drawing.Point(12, 30);
            this.lblDevices.Name = "lblDevices";
            this.lblDevices.Size = new System.Drawing.Size(75, 13);
            this.lblDevices.TabIndex = 0;
            this.lblDevices.Text = "Devices (0/0):";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(15, 324);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(37, 38);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dataGridViewMAC
            // 
            this.dataGridViewMAC.AllowUserToAddRows = false;
            this.dataGridViewMAC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMAC.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewMAC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMAC.ColumnHeadersVisible = false;
            this.dataGridViewMAC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridViewMAC.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dataGridViewMAC.Location = new System.Drawing.Point(15, 47);
            this.dataGridViewMAC.Name = "dataGridViewMAC";
            this.dataGridViewMAC.RowHeadersVisible = false;
            this.dataGridViewMAC.Size = new System.Drawing.Size(193, 271);
            this.dataGridViewMAC.TabIndex = 11;
            this.dataGridViewMAC.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewMAC_CellMouseClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // checkBoxUseDHCP
            // 
            this.checkBoxUseDHCP.AutoSize = true;
            this.checkBoxUseDHCP.Enabled = false;
            this.checkBoxUseDHCP.Location = new System.Drawing.Point(217, 47);
            this.checkBoxUseDHCP.Name = "checkBoxUseDHCP";
            this.checkBoxUseDHCP.Size = new System.Drawing.Size(112, 17);
            this.checkBoxUseDHCP.TabIndex = 12;
            this.checkBoxUseDHCP.Text = "Use DHCP Server";
            this.checkBoxUseDHCP.UseVisualStyleBackColor = true;
            this.checkBoxUseDHCP.CheckedChanged += new System.EventHandler(this.checkBoxUseDHCP_CheckedChanged);
            // 
            // grpbxDHCP
            // 
            this.grpbxDHCP.Controls.Add(this.labelError);
            this.grpbxDHCP.Controls.Add(this.txtGetway);
            this.grpbxDHCP.Controls.Add(this.txtSubnet);
            this.grpbxDHCP.Controls.Add(this.txtIP);
            this.grpbxDHCP.Controls.Add(this.lblGetway);
            this.grpbxDHCP.Controls.Add(this.lblsubnetworkmask);
            this.grpbxDHCP.Controls.Add(this.lblIP);
            this.grpbxDHCP.Controls.Add(this.txtMac);
            this.grpbxDHCP.Controls.Add(this.lblMac);
            this.grpbxDHCP.Location = new System.Drawing.Point(214, 70);
            this.grpbxDHCP.Name = "grpbxDHCP";
            this.grpbxDHCP.Size = new System.Drawing.Size(287, 248);
            this.grpbxDHCP.TabIndex = 13;
            this.grpbxDHCP.TabStop = false;
            // 
            // labelError
            // 
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelError.Location = new System.Drawing.Point(6, 193);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(274, 38);
            this.labelError.TabIndex = 18;
            this.labelError.Visible = false;
            // 
            // txtGetway
            // 
            this.txtGetway.AllowInternalTab = false;
            this.txtGetway.AutoHeight = true;
            this.txtGetway.BackColor = System.Drawing.SystemColors.Window;
            this.txtGetway.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtGetway.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGetway.Enabled = false;
            this.txtGetway.Location = new System.Drawing.Point(114, 107);
            this.txtGetway.Margin = new System.Windows.Forms.Padding(4);
            this.txtGetway.MinimumSize = new System.Drawing.Size(87, 20);
            this.txtGetway.Name = "txtGetway";
            this.txtGetway.ReadOnly = false;
            this.txtGetway.Size = new System.Drawing.Size(169, 20);
            this.txtGetway.TabIndex = 17;
            this.txtGetway.Text = "...";
            // 
            // txtSubnet
            // 
            this.txtSubnet.AllowInternalTab = false;
            this.txtSubnet.AutoHeight = true;
            this.txtSubnet.BackColor = System.Drawing.SystemColors.Window;
            this.txtSubnet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtSubnet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSubnet.Enabled = false;
            this.txtSubnet.Location = new System.Drawing.Point(114, 66);
            this.txtSubnet.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubnet.MinimumSize = new System.Drawing.Size(87, 20);
            this.txtSubnet.Name = "txtSubnet";
            this.txtSubnet.ReadOnly = false;
            this.txtSubnet.Size = new System.Drawing.Size(169, 20);
            this.txtSubnet.TabIndex = 16;
            this.txtSubnet.Text = "...";
            // 
            // txtIP
            // 
            this.txtIP.AllowInternalTab = false;
            this.txtIP.AutoHeight = true;
            this.txtIP.BackColor = System.Drawing.SystemColors.Window;
            this.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIP.Enabled = false;
            this.txtIP.Location = new System.Drawing.Point(114, 25);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP.MinimumSize = new System.Drawing.Size(87, 20);
            this.txtIP.Name = "txtIP";
            this.txtIP.ReadOnly = false;
            this.txtIP.Size = new System.Drawing.Size(169, 20);
            this.txtIP.TabIndex = 15;
            this.txtIP.Text = "...";
            // 
            // lblGetway
            // 
            this.lblGetway.AutoSize = true;
            this.lblGetway.Location = new System.Drawing.Point(6, 114);
            this.lblGetway.Name = "lblGetway";
            this.lblGetway.Size = new System.Drawing.Size(90, 13);
            this.lblGetway.TabIndex = 14;
            this.lblGetway.Text = "Gateway Address";
            this.lblGetway.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblsubnetworkmask
            // 
            this.lblsubnetworkmask.AutoSize = true;
            this.lblsubnetworkmask.Location = new System.Drawing.Point(6, 73);
            this.lblsubnetworkmask.Name = "lblsubnetworkmask";
            this.lblsubnetworkmask.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblsubnetworkmask.Size = new System.Drawing.Size(93, 13);
            this.lblsubnetworkmask.TabIndex = 13;
            this.lblsubnetworkmask.Text = "Subnetwork Mask";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(6, 32);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(61, 13);
            this.lblIP.TabIndex = 12;
            this.lblIP.Text = "IP Address ";
            // 
            // txtMac
            // 
            this.txtMac.Enabled = false;
            this.txtMac.Location = new System.Drawing.Point(112, 151);
            this.txtMac.Name = "txtMac";
            this.txtMac.Size = new System.Drawing.Size(169, 20);
            this.txtMac.TabIndex = 11;
            // 
            // lblMac
            // 
            this.lblMac.AutoSize = true;
            this.lblMac.Location = new System.Drawing.Point(6, 158);
            this.lblMac.Name = "lblMac";
            this.lblMac.Size = new System.Drawing.Size(71, 13);
            this.lblMac.TabIndex = 10;
            this.lblMac.Text = "MAC Address";
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(425, 324);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(76, 24);
            this.buttonApply.TabIndex = 19;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttoncopytoethernet
            // 
            this.buttoncopytoethernet.Location = new System.Drawing.Point(293, 324);
            this.buttoncopytoethernet.Name = "buttoncopytoethernet";
            this.buttoncopytoethernet.Size = new System.Drawing.Size(111, 24);
            this.buttoncopytoethernet.TabIndex = 20;
            this.buttoncopytoethernet.Text = "Copy to Ethernet";
            this.buttoncopytoethernet.UseVisualStyleBackColor = true;
            this.buttoncopytoethernet.Click += new System.EventHandler(this.buttoncopytoethernet_Click);
            // 
            // EasyScan
            // 
            this.ClientSize = new System.Drawing.Size(506, 369);
            this.Controls.Add(this.buttoncopytoethernet);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.grpbxDHCP);
            this.Controls.Add(this.checkBoxUseDHCP);
            this.Controls.Add(this.dataGridViewMAC);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblDevices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EasyScan";
            this.ShowIcon = false;
            this.Text = "Easy Connection";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMAC)).EndInit();
            this.grpbxDHCP.ResumeLayout(false);
            this.grpbxDHCP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void checkBoxUseDHCP_CheckedChanged(object sender, EventArgs e)
        {
            grpbxDHCP.Enabled = !checkBoxUseDHCP.Checked;
            checkBoxUseDHCP.Enabled = false;
        }
        private bool ValidateNetworkSettings()
        {
            labelError.Text = "";
            // Validate IP Address
            if (!IsValidIPv4Address(txtIP.Text))
            {
                MessageBox.Show("Please select a valid device with a proper IP address.",
                              "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate Subnet Mask
            if (!IsValidIPv4Address(txtSubnet.Text))
                labelError.Text = "The subnet mask is not valid, you can continue with further operations.";

            // Validate Gateway (if provided)
            if (!string.IsNullOrEmpty(txtGetway.Text) && txtGetway.Text != "..." && !IsValidIPv4Address(txtGetway.Text))
                labelError.Text = "The gateway address is not valid, you can continue with further operations.";

            return true;
        }
        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (!ValidateNetworkSettings())
                return;
            XMPS.Instance._connectedIPAddress = IPAddress.Parse(this.txtIP.Text).ToString();
            this.Close();
        }

        private void buttoncopytoethernet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateNetworkSettings())
                    return;
                // Get the selected IP

                string selectedIP = txtIP.Text;
                // Update the Ethernet settings without connecting

                if (XMPS.Instance.LoadedProject == null)
                {
                    DialogResult result = MessageBox.Show("No project is currently loaded. Would you like to create a new project to save these Ethernet settings?", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show("Please create a new project first using the main application menu.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        return; // User chose not to create new project
                    }
                }
                Ethernet ethernet = (Ethernet)XMPS.Instance.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "Ethernet");
                checkBoxUseDHCP.Checked = ethernet.UseDHCPServer;
                if (ethernet != null)
                {
                    ethernet.ChangeIPAddress = IPAddress.Parse(selectedIP);
                    ethernet.ChangeSubNet = IsValidIPv4Address(txtSubnet.Text) ? IPAddress.Parse(txtSubnet.Text) : IPAddress.Parse("0.0.0.0");
                    ethernet.ChangeGetWay = IsValidIPv4Address(txtGetway.Text) ? IPAddress.Parse(txtGetway.Text) : IPAddress.Parse("0.0.0.0");
                    UpdateBacNetNetworkPort(selectedIP, ethernet.ChangeSubNet.ToString(), ethernet.ChangeGetWay.ToString());

                    MessageBox.Show("Ethernet settings have been updated with the selected IP.\n" + "Note: You need to manually connect using these settings.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error copying settings: " + ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBacNetNetworkPort(string ipAddress, string subnetMask, string gateway)
        {
            try
            {
                if (!XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD"))
                {
                    return;
                }
                if (XMPS.Instance.LoadedProject.BacNetIP.NetworkPort == null)
                {
                    XMPS.Instance.LoadedProject.BacNetIP.NetworkPort = new NetworkPort();
                }

                XMPS.Instance.LoadedProject.BacNetIP.NetworkPort.IPAddress = ipAddress;
                XMPS.Instance.LoadedProject.BacNetIP.NetworkPort.IPSubnetMask = subnetMask;
                XMPS.Instance.LoadedProject.BacNetIP.NetworkPort.IPDefaultGateway = gateway;
                XMPS.Instance.LoadedProject.BacNetIP.NetworkPort.IPDHCPEnable = "0";

                if (string.IsNullOrEmpty(XMPS.Instance.LoadedProject.BacNetIP.NetworkPort.BacnetIPUDPPort))
                {
                    XMPS.Instance.LoadedProject.BacNetIP.NetworkPort.BacnetIPUDPPort = "47808";
                }

                XMPS.Instance.LoadedProject.isChanged = false;

                var networkPortForm = Application.OpenForms.OfType<FormNetworkPort>().FirstOrDefault();
                networkPortForm?.RefreshFormData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating BACnet settings: " + ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
