using XMPS2000.Core.Base.Helpers;
namespace XMPS2000
{
    partial class ModbusTCPServerUserControl
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
            this.TagEnable = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxFunctionCode = new System.Windows.Forms.ComboBox();
            this.lblFunctionCode = new System.Windows.Forms.Label();
            this.lblVariable = new System.Windows.Forms.Label();
            this.textBoxVariable = new System.Windows.Forms.TextBox();
            this.lblPortRange = new System.Windows.Forms.Label();
            this.Port = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblLengthRange = new System.Windows.Forms.Label();
            this.Length = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblAddressRange = new System.Windows.Forms.Label();
            this.Address = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.lblAddress = new System.Windows.Forms.Label();
            this.labelDeviceIdRange = new System.Windows.Forms.Label();
            this.DeviceId = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.labelDeviceId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceId)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TagEnable);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.comboBoxFunctionCode);
            this.groupBox1.Controls.Add(this.lblFunctionCode);
            this.groupBox1.Controls.Add(this.lblVariable);
            this.groupBox1.Controls.Add(this.textBoxVariable);
            this.groupBox1.Controls.Add(this.lblPortRange);
            this.groupBox1.Controls.Add(this.Port);
            this.groupBox1.Controls.Add(this.lblPort);
            this.groupBox1.Controls.Add(this.lblLengthRange);
            this.groupBox1.Controls.Add(this.Length);
            this.groupBox1.Controls.Add(this.lblLength);
            this.groupBox1.Controls.Add(this.lblAddressRange);
            this.groupBox1.Controls.Add(this.Address);
            this.groupBox1.Controls.Add(this.lblAddress);
            this.groupBox1.Controls.Add(this.labelDeviceIdRange);
            this.groupBox1.Controls.Add(this.DeviceId);
            this.groupBox1.Controls.Add(this.labelDeviceId);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 376);
            this.groupBox1.TabIndex = 117;
            this.groupBox1.TabStop = false;
            // 
            // TagEnable
            // 
            this.TagEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TagEnable.DropDownWidth = 196;
            this.TagEnable.FormattingEnabled = true;
            this.TagEnable.Items.AddRange(new object[] {
            "Normal Operand",
            "Numeric Operand"});
            this.TagEnable.Location = new System.Drawing.Point(151, 204);
            this.TagEnable.Name = "TagEnable";
            this.TagEnable.Size = new System.Drawing.Size(199, 21);
            this.TagEnable.TabIndex = 6;
            this.TagEnable.SelectedIndexChanged += new System.EventHandler(this.TagEnable_SelectedIndexChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.LightGreen;
            this.buttonSave.Location = new System.Drawing.Point(205, 272);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 133;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxFunctionCode
            // 
            this.comboBoxFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFunctionCode.DropDownWidth = 299;
            this.comboBoxFunctionCode.FormattingEnabled = true;
            this.comboBoxFunctionCode.Location = new System.Drawing.Point(151, 165);
            this.comboBoxFunctionCode.Name = "comboBoxFunctionCode";
            this.comboBoxFunctionCode.Size = new System.Drawing.Size(199, 21);
            this.comboBoxFunctionCode.TabIndex = 5;
            this.comboBoxFunctionCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFunctionCode_SelectedIndexChanged);
            // 
            // lblFunctionCode
            // 
            this.lblFunctionCode.AutoSize = true;
            this.lblFunctionCode.Location = new System.Drawing.Point(20, 165);
            this.lblFunctionCode.Name = "lblFunctionCode";
            this.lblFunctionCode.Size = new System.Drawing.Size(76, 13);
            this.lblFunctionCode.TabIndex = 129;
            this.lblFunctionCode.Text = "Function Code";
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Location = new System.Drawing.Point(20, 208);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(45, 13);
            this.lblVariable.TabIndex = 127;
            this.lblVariable.Text = "Variable";
            // 
            // textBoxVariable
            // 
            this.textBoxVariable.Location = new System.Drawing.Point(366, 206);
            this.textBoxVariable.Name = "textBoxVariable";
            this.textBoxVariable.Size = new System.Drawing.Size(93, 20);
            this.textBoxVariable.TabIndex = 7;
            this.textBoxVariable.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxVariable_Validating);
            // 
            // lblPortRange
            // 
            this.lblPortRange.AutoSize = true;
            this.lblPortRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPortRange.Location = new System.Drawing.Point(363, 116);
            this.lblPortRange.Name = "lblPortRange";
            this.lblPortRange.Size = new System.Drawing.Size(102, 13);
            this.lblPortRange.TabIndex = 125;
            this.lblPortRange.Text = "(Range: 2 to 65534)";
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(151, 116);
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
            this.Port.TabIndex = 4;
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
            this.lblPort.Location = new System.Drawing.Point(20, 116);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 124;
            this.lblPort.Text = "Port";
            // 
            // lblLengthRange
            // 
            this.lblLengthRange.AutoSize = true;
            this.lblLengthRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblLengthRange.Location = new System.Drawing.Point(363, 76);
            this.lblLengthRange.Name = "lblLengthRange";
            this.lblLengthRange.Size = new System.Drawing.Size(90, 13);
            this.lblLengthRange.TabIndex = 122;
            this.lblLengthRange.Text = "(Range: 1 to 255)";
            // 
            // Length
            // 
            this.Length.Location = new System.Drawing.Point(151, 76);
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
            this.Length.TabIndex = 3;
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
            this.lblLength.Location = new System.Drawing.Point(20, 76);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(40, 13);
            this.lblLength.TabIndex = 121;
            this.lblLength.Text = "Length";
            // 
            // lblAddressRange
            // 
            this.lblAddressRange.AutoSize = true;
            this.lblAddressRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAddressRange.Location = new System.Drawing.Point(363, 34);
            this.lblAddressRange.Name = "lblAddressRange";
            this.lblAddressRange.Size = new System.Drawing.Size(96, 13);
            this.lblAddressRange.TabIndex = 119;
            this.lblAddressRange.Text = "(Range: 0 to 9998)";
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(151, 34);
            this.Address.Maximum = new decimal(new int[] {
            9998,
            0,
            0,
            0});
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(95, 20);
            this.Address.TabIndex = 2;
            this.Address.Leave += new System.EventHandler(this.Address_Leave);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(20, 34);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 13);
            this.lblAddress.TabIndex = 118;
            this.lblAddress.Text = "Address";
            // 
            // labelDeviceIdRange
            // 
            this.labelDeviceIdRange.AutoSize = true;
            this.labelDeviceIdRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelDeviceIdRange.Location = new System.Drawing.Point(363, 333);
            this.labelDeviceIdRange.Name = "labelDeviceIdRange";
            this.labelDeviceIdRange.Size = new System.Drawing.Size(90, 13);
            this.labelDeviceIdRange.TabIndex = 116;
            this.labelDeviceIdRange.Text = "(Range: 0 to 255)";
            this.labelDeviceIdRange.Visible = false;
            // 
            // DeviceId
            // 
            this.DeviceId.Location = new System.Drawing.Point(151, 333);
            this.DeviceId.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.DeviceId.Name = "DeviceId";
            this.DeviceId.Size = new System.Drawing.Size(95, 20);
            this.DeviceId.TabIndex = 1;
            this.DeviceId.Visible = false;
            // 
            // labelDeviceId
            // 
            this.labelDeviceId.AutoSize = true;
            this.labelDeviceId.Location = new System.Drawing.Point(20, 333);
            this.labelDeviceId.Name = "labelDeviceId";
            this.labelDeviceId.Size = new System.Drawing.Size(53, 13);
            this.labelDeviceId.TabIndex = 115;
            this.labelDeviceId.Text = "Device Id";
            this.labelDeviceId.Visible = false;
            // 
            // ModbusTCPServerUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ModbusTCPServerUserControl";
            this.Size = new System.Drawing.Size(507, 392);
            this.Load += new System.EventHandler(this.ModbusTCPServerUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceId)).EndInit();
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
        private System.Windows.Forms.Label lblPortRange;
        private MyNumericUpDown Port;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblLengthRange;
        private MyNumericUpDown Length;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblAddressRange;
        private MyNumericUpDown Address;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label labelDeviceIdRange;
        private MyNumericUpDown DeviceId;
        private System.Windows.Forms.Label labelDeviceId;
        private System.Windows.Forms.ComboBox TagEnable;
    }
}
