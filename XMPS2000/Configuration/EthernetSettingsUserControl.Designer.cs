using XMPS2000.Core.Base.Helpers;
namespace XMPS2000
{
    partial class EthernetSettingsUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblchangegateway = new System.Windows.Forms.Label();
            this.lblchangesubnet = new System.Windows.Forms.Label();
            this.lblchangeipadd = new System.Windows.Forms.Label();
            this.ipAddressChangeGateway = new IPAddressControlLib.IPAddressControl();
            this.ipAddressChangeSubnet = new IPAddressControlLib.IPAddressControl();
            this.ipAddressChangeControl = new IPAddressControlLib.IPAddressControl();
            this.checkBoxUseDHCP = new System.Windows.Forms.CheckBox();
            this.lblgateway = new System.Windows.Forms.Label();
            this.lblsubnet = new System.Windows.Forms.Label();
            this.lblipadd = new System.Windows.Forms.Label();
            this.ipAddressGateway = new IPAddressControlLib.IPAddressControl();
            this.ipAddressSubnet = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl = new IPAddressControlLib.IPAddressControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.NetworkNo = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.lblNetworkRange = new System.Windows.Forms.Label();
            this.lblnetwork = new System.Windows.Forms.Label();
            this.Port = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.lblport = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NetworkNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblchangegateway);
            this.groupBox1.Controls.Add(this.lblchangesubnet);
            this.groupBox1.Controls.Add(this.lblchangeipadd);
            this.groupBox1.Controls.Add(this.ipAddressChangeGateway);
            this.groupBox1.Controls.Add(this.ipAddressChangeSubnet);
            this.groupBox1.Controls.Add(this.ipAddressChangeControl);
            this.groupBox1.Controls.Add(this.checkBoxUseDHCP);
            this.groupBox1.Controls.Add(this.lblgateway);
            this.groupBox1.Controls.Add(this.lblsubnet);
            this.groupBox1.Controls.Add(this.lblipadd);
            this.groupBox1.Controls.Add(this.ipAddressGateway);
            this.groupBox1.Controls.Add(this.ipAddressSubnet);
            this.groupBox1.Controls.Add(this.ipAddressControl);
            this.groupBox1.Location = new System.Drawing.Point(19, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 237);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ethernet Settings";
            // 
            // lblchangegateway
            // 
            this.lblchangegateway.AutoSize = true;
            this.lblchangegateway.Location = new System.Drawing.Point(15, 204);
            this.lblchangegateway.Name = "lblchangegateway";
            this.lblchangegateway.Size = new System.Drawing.Size(49, 13);
            this.lblchangegateway.TabIndex = 11;
            this.lblchangegateway.Text = "Gateway";
            // 
            // lblchangesubnet
            // 
            this.lblchangesubnet.AutoSize = true;
            this.lblchangesubnet.Location = new System.Drawing.Point(15, 173);
            this.lblchangesubnet.Name = "lblchangesubnet";
            this.lblchangesubnet.Size = new System.Drawing.Size(41, 13);
            this.lblchangesubnet.TabIndex = 9;
            this.lblchangesubnet.Text = "Subnet";
            // 
            // lblchangeipadd
            // 
            this.lblchangeipadd.AutoSize = true;
            this.lblchangeipadd.Location = new System.Drawing.Point(15, 142);
            this.lblchangeipadd.Name = "lblchangeipadd";
            this.lblchangeipadd.Size = new System.Drawing.Size(98, 13);
            this.lblchangeipadd.TabIndex = 7;
            this.lblchangeipadd.Text = "Change IP Address";
            // 
            // ipAddressChangeGateway
            // 
            this.ipAddressChangeGateway.AllowInternalTab = false;
            this.ipAddressChangeGateway.AutoHeight = true;
            this.ipAddressChangeGateway.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressChangeGateway.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressChangeGateway.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressChangeGateway.Location = new System.Drawing.Point(113, 197);
            this.ipAddressChangeGateway.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressChangeGateway.Name = "ipAddressChangeGateway";
            this.ipAddressChangeGateway.ReadOnly = false;
            this.ipAddressChangeGateway.Size = new System.Drawing.Size(212, 20);
            this.ipAddressChangeGateway.TabIndex = 7;
            this.ipAddressChangeGateway.Text = "...";
            // 
            // ipAddressChangeSubnet
            // 
            this.ipAddressChangeSubnet.AllowInternalTab = false;
            this.ipAddressChangeSubnet.AutoHeight = true;
            this.ipAddressChangeSubnet.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressChangeSubnet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressChangeSubnet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressChangeSubnet.Location = new System.Drawing.Point(113, 166);
            this.ipAddressChangeSubnet.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressChangeSubnet.Name = "ipAddressChangeSubnet";
            this.ipAddressChangeSubnet.ReadOnly = false;
            this.ipAddressChangeSubnet.Size = new System.Drawing.Size(212, 20);
            this.ipAddressChangeSubnet.TabIndex = 6;
            this.ipAddressChangeSubnet.Text = "...";
            // 
            // ipAddressChangeControl
            // 
            this.ipAddressChangeControl.AllowInternalTab = false;
            this.ipAddressChangeControl.AutoHeight = true;
            this.ipAddressChangeControl.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressChangeControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressChangeControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressChangeControl.Location = new System.Drawing.Point(113, 135);
            this.ipAddressChangeControl.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressChangeControl.Name = "ipAddressChangeControl";
            this.ipAddressChangeControl.ReadOnly = false;
            this.ipAddressChangeControl.Size = new System.Drawing.Size(212, 20);
            this.ipAddressChangeControl.TabIndex = 5;
            this.ipAddressChangeControl.Text = "...";
            // 
            // checkBoxUseDHCP
            // 
            this.checkBoxUseDHCP.AutoSize = true;
            this.checkBoxUseDHCP.Location = new System.Drawing.Point(18, 29);
            this.checkBoxUseDHCP.Name = "checkBoxUseDHCP";
            this.checkBoxUseDHCP.Size = new System.Drawing.Size(112, 17);
            this.checkBoxUseDHCP.TabIndex = 1;
            this.checkBoxUseDHCP.Text = "Use DHCP Server";
            this.checkBoxUseDHCP.UseVisualStyleBackColor = true;
            this.checkBoxUseDHCP.CheckedChanged += new System.EventHandler(this.checkBoxUseDHCP_CheckedChanged);
            // 
            // lblgateway
            // 
            this.lblgateway.AutoSize = true;
            this.lblgateway.Location = new System.Drawing.Point(15, 113);
            this.lblgateway.Name = "lblgateway";
            this.lblgateway.Size = new System.Drawing.Size(49, 13);
            this.lblgateway.TabIndex = 5;
            this.lblgateway.Text = "Gateway";
            // 
            // lblsubnet
            // 
            this.lblsubnet.AutoSize = true;
            this.lblsubnet.Location = new System.Drawing.Point(15, 87);
            this.lblsubnet.Name = "lblsubnet";
            this.lblsubnet.Size = new System.Drawing.Size(41, 13);
            this.lblsubnet.TabIndex = 4;
            this.lblsubnet.Text = "Subnet";
            // 
            // lblipadd
            // 
            this.lblipadd.AutoSize = true;
            this.lblipadd.Location = new System.Drawing.Point(15, 61);
            this.lblipadd.Name = "lblipadd";
            this.lblipadd.Size = new System.Drawing.Size(58, 13);
            this.lblipadd.TabIndex = 3;
            this.lblipadd.Text = "IP Address";
            // 
            // ipAddressGateway
            // 
            this.ipAddressGateway.AllowInternalTab = false;
            this.ipAddressGateway.AutoHeight = true;
            this.ipAddressGateway.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressGateway.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressGateway.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressGateway.Enabled = false;
            this.ipAddressGateway.Location = new System.Drawing.Point(113, 106);
            this.ipAddressGateway.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressGateway.Name = "ipAddressGateway";
            this.ipAddressGateway.ReadOnly = false;
            this.ipAddressGateway.Size = new System.Drawing.Size(212, 20);
            this.ipAddressGateway.TabIndex = 4;
            this.ipAddressGateway.Text = "...";
            // 
            // ipAddressSubnet
            // 
            this.ipAddressSubnet.AllowInternalTab = false;
            this.ipAddressSubnet.AutoHeight = true;
            this.ipAddressSubnet.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressSubnet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressSubnet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressSubnet.Enabled = false;
            this.ipAddressSubnet.Location = new System.Drawing.Point(113, 80);
            this.ipAddressSubnet.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressSubnet.Name = "ipAddressSubnet";
            this.ipAddressSubnet.ReadOnly = false;
            this.ipAddressSubnet.Size = new System.Drawing.Size(212, 20);
            this.ipAddressSubnet.TabIndex = 3;
            this.ipAddressSubnet.Text = "...";
            // 
            // ipAddressControl
            // 
            this.ipAddressControl.AllowInternalTab = false;
            this.ipAddressControl.AutoHeight = true;
            this.ipAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl.Enabled = false;
            this.ipAddressControl.Location = new System.Drawing.Point(113, 54);
            this.ipAddressControl.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressControl.Name = "ipAddressControl";
            this.ipAddressControl.ReadOnly = false;
            this.ipAddressControl.Size = new System.Drawing.Size(212, 20);
            this.ipAddressControl.TabIndex = 2;
            this.ipAddressControl.Text = "...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NetworkNo);
            this.groupBox2.Controls.Add(this.lblNetworkRange);
            this.groupBox2.Controls.Add(this.lblnetwork);
            this.groupBox2.Controls.Add(this.Port);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblport);
            this.groupBox2.Location = new System.Drawing.Point(19, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 102);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Port Settings";
            // 
            // NetworkNo
            // 
            this.NetworkNo.Location = new System.Drawing.Point(117, 53);
            this.NetworkNo.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NetworkNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NetworkNo.Name = "NetworkNo";
            this.NetworkNo.Size = new System.Drawing.Size(95, 20);
            this.NetworkNo.TabIndex = 106;
            this.NetworkNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblNetworkRange
            // 
            this.lblNetworkRange.AutoSize = true;
            this.lblNetworkRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblNetworkRange.Location = new System.Drawing.Point(223, 55);
            this.lblNetworkRange.Name = "lblNetworkRange";
            this.lblNetworkRange.Size = new System.Drawing.Size(102, 13);
            this.lblNetworkRange.TabIndex = 107;
            this.lblNetworkRange.Text = "(Range: 1 to 65535)";
            // 
            // lblnetwork
            // 
            this.lblnetwork.AutoSize = true;
            this.lblnetwork.Location = new System.Drawing.Point(15, 60);
            this.lblnetwork.Name = "lblnetwork";
            this.lblnetwork.Size = new System.Drawing.Size(67, 13);
            this.lblnetwork.TabIndex = 105;
            this.lblnetwork.Text = "Network No.";
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(117, 27);
            this.Port.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.Port.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(95, 20);
            this.Port.TabIndex = 8;
            this.Port.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label8.Location = new System.Drawing.Point(223, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 104;
            this.label8.Text = "(Range: 2 to 65534)";
            // 
            // lblport
            // 
            this.lblport.AutoSize = true;
            this.lblport.Location = new System.Drawing.Point(15, 34);
            this.lblport.Name = "lblport";
            this.lblport.Size = new System.Drawing.Size(26, 13);
            this.lblport.TabIndex = 0;
            this.lblport.Text = "Port";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(146, 371);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // EthernetSettingsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonSave);
            this.Name = "EthernetSettingsUserControl";
            this.Size = new System.Drawing.Size(392, 419);
            this.Load += new System.EventHandler(this.EthernetSettingsUserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NetworkNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxUseDHCP;
        private System.Windows.Forms.Label lblgateway;
        private System.Windows.Forms.Label lblsubnet;
        private System.Windows.Forms.Label lblipadd;
        private IPAddressControlLib.IPAddressControl ipAddressGateway;
        private IPAddressControlLib.IPAddressControl ipAddressSubnet;
        private IPAddressControlLib.IPAddressControl ipAddressControl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblport;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label8;
        private MyNumericUpDown Port;
        private System.Windows.Forms.Label lblchangegateway;
        private System.Windows.Forms.Label lblchangesubnet;
        private System.Windows.Forms.Label lblchangeipadd;
        private IPAddressControlLib.IPAddressControl ipAddressChangeGateway;
        private IPAddressControlLib.IPAddressControl ipAddressChangeSubnet;
        private IPAddressControlLib.IPAddressControl ipAddressChangeControl;
        private MyNumericUpDown NetworkNo;
        private System.Windows.Forms.Label lblNetworkRange;
        private System.Windows.Forms.Label lblnetwork;
    }
}
