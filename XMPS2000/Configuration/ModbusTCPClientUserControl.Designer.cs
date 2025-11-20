using XMPS2000.Core.Base.Helpers;

namespace XMPS2000
{
    partial class ModbusTCPClientUserControl
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
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAddressRange = new System.Windows.Forms.Label();
            this.lblDeviceIdRange = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbmultiplicationfact = new System.Windows.Forms.ComboBox();
            this.cmdbtnCancel = new System.Windows.Forms.Button();
            this.cmdbtnOK = new System.Windows.Forms.Button();
            this.lblMultiplicationFactor = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TagEnable = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxFunctionCode = new System.Windows.Forms.ComboBox();
            this.lblFunctionCode = new System.Windows.Forms.Label();
            this.lblVariable = new System.Windows.Forms.Label();
            this.textBoxVariable = new System.Windows.Forms.TextBox();
            this.lblLengthRange = new System.Windows.Forms.Label();
            this.Length = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.lblLength = new System.Windows.Forms.Label();
            this.Address = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.lblAddress = new System.Windows.Forms.Label();
            this.DeviceId = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.lblDeviceId = new System.Windows.Forms.Label();
            this.lblPollingRange = new System.Windows.Forms.Label();
            this.Polling = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.lblPolling = new System.Windows.Forms.Label();
            this.lblPortRange = new System.Windows.Forms.Label();
            this.Port = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.lblPort = new System.Windows.Forms.Label();
            this.IPAddress = new IPAddressControlLib.IPAddressControl();
            this.lblSlaveIpAdd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Polling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAddressRange);
            this.groupBox1.Controls.Add(this.lblDeviceIdRange);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.TagEnable);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.comboBoxFunctionCode);
            this.groupBox1.Controls.Add(this.lblFunctionCode);
            this.groupBox1.Controls.Add(this.lblVariable);
            this.groupBox1.Controls.Add(this.textBoxVariable);
            this.groupBox1.Controls.Add(this.lblLengthRange);
            this.groupBox1.Controls.Add(this.Length);
            this.groupBox1.Controls.Add(this.lblLength);
            this.groupBox1.Controls.Add(this.Address);
            this.groupBox1.Controls.Add(this.lblAddress);
            this.groupBox1.Controls.Add(this.DeviceId);
            this.groupBox1.Controls.Add(this.lblDeviceId);
            this.groupBox1.Controls.Add(this.lblPollingRange);
            this.groupBox1.Controls.Add(this.Polling);
            this.groupBox1.Controls.Add(this.lblPolling);
            this.groupBox1.Controls.Add(this.lblPortRange);
            this.groupBox1.Controls.Add(this.Port);
            this.groupBox1.Controls.Add(this.lblPort);
            this.groupBox1.Controls.Add(this.IPAddress);
            this.groupBox1.Controls.Add(this.lblSlaveIpAdd);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(558, 463);
            this.groupBox1.TabIndex = 114;
            this.groupBox1.TabStop = false;
            // 
            // lblAddressRange
            // 
            this.lblAddressRange.AutoSize = true;
            this.lblAddressRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAddressRange.Location = new System.Drawing.Point(372, 200);
            this.lblAddressRange.Name = "lblAddressRange";
            this.lblAddressRange.Size = new System.Drawing.Size(96, 13);
            this.lblAddressRange.TabIndex = 141;
            this.lblAddressRange.Text = "(Range: 0 to 9998)";
            // 
            // lblDeviceIdRange
            // 
            this.lblDeviceIdRange.AutoSize = true;
            this.lblDeviceIdRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblDeviceIdRange.Location = new System.Drawing.Point(372, 160);
            this.lblDeviceIdRange.Name = "lblDeviceIdRange";
            this.lblDeviceIdRange.Size = new System.Drawing.Size(90, 13);
            this.lblDeviceIdRange.TabIndex = 140;
            this.lblDeviceIdRange.Text = "(Range: 0 to 255)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbmultiplicationfact);
            this.groupBox2.Controls.Add(this.cmdbtnCancel);
            this.groupBox2.Controls.Add(this.cmdbtnOK);
            this.groupBox2.Controls.Add(this.lblMultiplicationFactor);
            this.groupBox2.Location = new System.Drawing.Point(153, 330);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(247, 96);
            this.groupBox2.TabIndex = 139;
            this.groupBox2.TabStop = false;
            // 
            // cmbmultiplicationfact
            // 
            this.cmbmultiplicationfact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbmultiplicationfact.DropDownWidth = 275;
            this.cmbmultiplicationfact.FormattingEnabled = true;
            this.cmbmultiplicationfact.Items.AddRange(new object[] {
            "1",
            "0.1",
            "0.01",
            "0.001",
            "0.0001",
            "0.00001",
            "10",
            "100",
            "1000",
            "10000"});
            this.cmbmultiplicationfact.Location = new System.Drawing.Point(122, 26);
            this.cmbmultiplicationfact.Name = "cmbmultiplicationfact";
            this.cmbmultiplicationfact.Size = new System.Drawing.Size(102, 21);
            this.cmbmultiplicationfact.TabIndex = 139;
            this.cmbmultiplicationfact.SelectedIndexChanged += new System.EventHandler(this.cmbmultiplicationfact_SelectedIndexChanged);
            // 
            // cmdbtnCancel
            // 
            this.cmdbtnCancel.BackColor = System.Drawing.Color.LightGreen;
            this.cmdbtnCancel.Location = new System.Drawing.Point(149, 65);
            this.cmdbtnCancel.Name = "cmdbtnCancel";
            this.cmdbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdbtnCancel.TabIndex = 138;
            this.cmdbtnCancel.Text = "Cancel";
            this.cmdbtnCancel.UseVisualStyleBackColor = false;
            this.cmdbtnCancel.Click += new System.EventHandler(this.cmdbtnCancel_Click);
            // 
            // cmdbtnOK
            // 
            this.cmdbtnOK.BackColor = System.Drawing.Color.LightGreen;
            this.cmdbtnOK.Location = new System.Drawing.Point(42, 65);
            this.cmdbtnOK.Name = "cmdbtnOK";
            this.cmdbtnOK.Size = new System.Drawing.Size(75, 23);
            this.cmdbtnOK.TabIndex = 137;
            this.cmdbtnOK.Text = "OK";
            this.cmdbtnOK.UseVisualStyleBackColor = false;
            this.cmdbtnOK.Click += new System.EventHandler(this.cmdbtnOK_Click);
            // 
            // lblMultiplicationFactor
            // 
            this.lblMultiplicationFactor.AutoSize = true;
            this.lblMultiplicationFactor.Location = new System.Drawing.Point(15, 26);
            this.lblMultiplicationFactor.Name = "lblMultiplicationFactor";
            this.lblMultiplicationFactor.Size = new System.Drawing.Size(101, 13);
            this.lblMultiplicationFactor.TabIndex = 126;
            this.lblMultiplicationFactor.Text = "Multiplication Factor";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGreen;
            this.button1.Location = new System.Drawing.Point(177, 348);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 23);
            this.button1.TabIndex = 137;
            this.button1.Text = "Multiplication Factor";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TagEnable
            // 
            this.TagEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TagEnable.DropDownWidth = 196;
            this.TagEnable.FormattingEnabled = true;
            this.TagEnable.Items.AddRange(new object[] {
            "Normal Operand",
            "Numeric Operand"});
            this.TagEnable.Location = new System.Drawing.Point(177, 304);
            this.TagEnable.Name = "TagEnable";
            this.TagEnable.Size = new System.Drawing.Size(175, 21);
            this.TagEnable.TabIndex = 8;
            this.TagEnable.SelectedIndexChanged += new System.EventHandler(this.TagEnable_SelectedIndexChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.LightGreen;
            this.buttonSave.Location = new System.Drawing.Point(227, 432);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 136;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxFunctionCode
            // 
            this.comboBoxFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFunctionCode.DropDownWidth = 275;
            this.comboBoxFunctionCode.FormattingEnabled = true;
            this.comboBoxFunctionCode.Location = new System.Drawing.Point(177, 266);
            this.comboBoxFunctionCode.Name = "comboBoxFunctionCode";
            this.comboBoxFunctionCode.Size = new System.Drawing.Size(175, 21);
            this.comboBoxFunctionCode.TabIndex = 7;
            this.comboBoxFunctionCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFunctionCode_SelectedIndexChanged);
            this.comboBoxFunctionCode.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxFunctionCode_Validating);
            // 
            // lblFunctionCode
            // 
            this.lblFunctionCode.AutoSize = true;
            this.lblFunctionCode.Location = new System.Drawing.Point(22, 269);
            this.lblFunctionCode.Name = "lblFunctionCode";
            this.lblFunctionCode.Size = new System.Drawing.Size(76, 13);
            this.lblFunctionCode.TabIndex = 132;
            this.lblFunctionCode.Text = "Function Code";
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Location = new System.Drawing.Point(22, 312);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(45, 13);
            this.lblVariable.TabIndex = 130;
            this.lblVariable.Text = "Variable";
            // 
            // textBoxVariable
            // 
            this.textBoxVariable.Location = new System.Drawing.Point(375, 304);
            this.textBoxVariable.Name = "textBoxVariable";
            this.textBoxVariable.Size = new System.Drawing.Size(96, 20);
            this.textBoxVariable.TabIndex = 9;
            this.textBoxVariable.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxVariable_Validating);
            // 
            // lblLengthRange
            // 
            this.lblLengthRange.AutoSize = true;
            this.lblLengthRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblLengthRange.Location = new System.Drawing.Point(372, 228);
            this.lblLengthRange.Name = "lblLengthRange";
            this.lblLengthRange.Size = new System.Drawing.Size(90, 13);
            this.lblLengthRange.TabIndex = 128;
            this.lblLengthRange.Text = "(Range: 1 to 255)";
            // 
            // Length
            // 
            this.Length.Location = new System.Drawing.Point(177, 228);
            this.Length.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Length.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Length.Name = "Length";
            this.Length.Size = new System.Drawing.Size(95, 20);
            this.Length.TabIndex = 6;
            this.Length.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Length.Leave += new System.EventHandler(this.Length_Leave);
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(22, 228);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(40, 13);
            this.lblLength.TabIndex = 127;
            this.lblLength.Text = "Length";
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(177, 193);
            this.Address.Maximum = new decimal(new int[] {
            9998,
            0,
            0,
            0});
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(95, 20);
            this.Address.TabIndex = 5;
            this.Address.Leave += new System.EventHandler(this.Address_Leave);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(22, 193);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 13);
            this.lblAddress.TabIndex = 124;
            this.lblAddress.Text = "Address";
            // 
            // DeviceId
            // 
            this.DeviceId.Location = new System.Drawing.Point(177, 158);
            this.DeviceId.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.DeviceId.Name = "DeviceId";
            this.DeviceId.Size = new System.Drawing.Size(95, 20);
            this.DeviceId.TabIndex = 4;
            this.DeviceId.Leave += new System.EventHandler(this.DeviceId_Leave);
            // 
            // lblDeviceId
            // 
            this.lblDeviceId.AutoSize = true;
            this.lblDeviceId.Location = new System.Drawing.Point(22, 158);
            this.lblDeviceId.Name = "lblDeviceId";
            this.lblDeviceId.Size = new System.Drawing.Size(53, 13);
            this.lblDeviceId.TabIndex = 121;
            this.lblDeviceId.Text = "Device Id";
            // 
            // lblPollingRange
            // 
            this.lblPollingRange.AutoSize = true;
            this.lblPollingRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPollingRange.Location = new System.Drawing.Point(372, 123);
            this.lblPollingRange.Name = "lblPollingRange";
            this.lblPollingRange.Size = new System.Drawing.Size(114, 13);
            this.lblPollingRange.TabIndex = 119;
            this.lblPollingRange.Text = "(Range: 0 to 3600000)";
            // 
            // Polling
            // 
            this.Polling.Increment = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Polling.Location = new System.Drawing.Point(177, 123);
            this.Polling.Maximum = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
            this.Polling.Name = "Polling";
            this.Polling.Size = new System.Drawing.Size(103, 20);
            this.Polling.TabIndex = 3;
            this.Polling.Leave += new System.EventHandler(this.Polling_Leave);
            // 
            // lblPolling
            // 
            this.lblPolling.AutoSize = true;
            this.lblPolling.Location = new System.Drawing.Point(22, 123);
            this.lblPolling.Name = "lblPolling";
            this.lblPolling.Size = new System.Drawing.Size(60, 13);
            this.lblPolling.TabIndex = 118;
            this.lblPolling.Text = "Polling (ms)";
            // 
            // lblPortRange
            // 
            this.lblPortRange.AutoSize = true;
            this.lblPortRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPortRange.Location = new System.Drawing.Point(372, 88);
            this.lblPortRange.Name = "lblPortRange";
            this.lblPortRange.Size = new System.Drawing.Size(102, 13);
            this.lblPortRange.TabIndex = 116;
            this.lblPortRange.Text = "(Range: 2 to 65534)";
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(177, 88);
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
            this.Port.TabIndex = 2;
            this.Port.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
            this.Port.Leave += new System.EventHandler(this.Port_Leave);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(22, 88);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 115;
            this.lblPort.Text = "Port";
            // 
            // IPAddress
            // 
            this.IPAddress.AllowInternalTab = false;
            this.IPAddress.AutoHeight = true;
            this.IPAddress.BackColor = System.Drawing.SystemColors.Window;
            this.IPAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.IPAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.IPAddress.Location = new System.Drawing.Point(177, 53);
            this.IPAddress.Margin = new System.Windows.Forms.Padding(4);
            this.IPAddress.MinimumSize = new System.Drawing.Size(87, 20);
            this.IPAddress.Name = "IPAddress";
            this.IPAddress.ReadOnly = false;
            this.IPAddress.Size = new System.Drawing.Size(168, 20);
            this.IPAddress.TabIndex = 1;
            this.IPAddress.Text = "...";
            // 
            // lblSlaveIpAdd
            // 
            this.lblSlaveIpAdd.AutoSize = true;
            this.lblSlaveIpAdd.Location = new System.Drawing.Point(22, 53);
            this.lblSlaveIpAdd.Name = "lblSlaveIpAdd";
            this.lblSlaveIpAdd.Size = new System.Drawing.Size(88, 13);
            this.lblSlaveIpAdd.TabIndex = 80;
            this.lblSlaveIpAdd.Text = "Slave IP Address";
            // 
            // ModbusTCPClientUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ModbusTCPClientUserControl";
            this.Size = new System.Drawing.Size(597, 482);
            this.Load += new System.EventHandler(this.ModbusTCPClientUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Polling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxFunctionCode;
        private System.Windows.Forms.Label lblFunctionCode;
        private System.Windows.Forms.Label lblVariable;
        private System.Windows.Forms.TextBox textBoxVariable;
        private System.Windows.Forms.Label lblLengthRange;
        private MyNumericUpDown Length;
        private System.Windows.Forms.Label lblLength;
        private MyNumericUpDown Address;
        private System.Windows.Forms.Label lblAddress;
        private MyNumericUpDown DeviceId;
        private System.Windows.Forms.Label lblDeviceId;
        private System.Windows.Forms.Label lblPollingRange;
        private MyNumericUpDown Polling;
        private System.Windows.Forms.Label lblPolling;
        private System.Windows.Forms.Label lblPortRange;
        private MyNumericUpDown Port;
        private System.Windows.Forms.Label lblPort;
        private IPAddressControlLib.IPAddressControl IPAddress;
        private System.Windows.Forms.Label lblSlaveIpAdd;
        private System.Windows.Forms.ComboBox TagEnable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdbtnCancel;
        private System.Windows.Forms.Button cmdbtnOK;
        private System.Windows.Forms.Label lblMultiplicationFactor;
        private System.Windows.Forms.Label lblAddressRange;
        private System.Windows.Forms.Label lblDeviceIdRange;
        private System.Windows.Forms.ComboBox cmbmultiplicationfact;
    }
}
