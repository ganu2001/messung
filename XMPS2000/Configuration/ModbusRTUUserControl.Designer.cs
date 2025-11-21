
namespace XMPS2000
{
    partial class ModbusRTUUserControl
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
            this.lblAddressRange = new System.Windows.Forms.Label();
            this.lblDeviceIdRange = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbmultiplicationfact = new System.Windows.Forms.ComboBox();
            this.cmdbtnCancel = new System.Windows.Forms.Button();
            this.cmdbtnOK = new System.Windows.Forms.Button();
            this.lblMultiplicationFactor = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblpolling = new System.Windows.Forms.Label();
            this.lblpollingrange = new System.Windows.Forms.Label();
            this.labelDeviceId = new System.Windows.Forms.Label();
            this.labelDeviceIdRange = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblLengthRange = new System.Windows.Forms.Label();
            this.textBoxVariable = new System.Windows.Forms.TextBox();
            this.lblVariable = new System.Windows.Forms.Label();
            this.lblFunctionCode = new System.Windows.Forms.Label();
            this.comboBoxFunctionCode = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.Length = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.Address = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.DeviceId = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.Polling = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.TagEnable = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Polling)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lblAddressRange
            // 
            this.lblAddressRange.AutoSize = true;
            this.lblAddressRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAddressRange.Location = new System.Drawing.Point(382, 176);
            this.lblAddressRange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAddressRange.Name = "lblAddressRange";
            this.lblAddressRange.Size = new System.Drawing.Size(96, 13);
            this.lblAddressRange.TabIndex = 141;
            this.lblAddressRange.Text = "(Range: 0 to 9998)";
            // 
            // lblDeviceIdRange
            // 
            this.lblDeviceIdRange.AutoSize = true;
            this.lblDeviceIdRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblDeviceIdRange.Location = new System.Drawing.Point(388, 101);
            this.lblDeviceIdRange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            this.groupBox2.Location = new System.Drawing.Point(155, 243);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(238, 81);
            this.groupBox2.TabIndex = 139;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
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
            this.cmbmultiplicationfact.Location = new System.Drawing.Point(131, 21);
            this.cmbmultiplicationfact.Margin = new System.Windows.Forms.Padding(2);
            this.cmbmultiplicationfact.Name = "cmbmultiplicationfact";
            this.cmbmultiplicationfact.Size = new System.Drawing.Size(90, 21);
            this.cmbmultiplicationfact.TabIndex = 139;
            this.cmbmultiplicationfact.SelectedIndexChanged += new System.EventHandler(this.cmbmultiplicationfact_SelectedIndexChanged);
            // 
            // cmdbtnCancel
            // 
            this.cmdbtnCancel.BackColor = System.Drawing.Color.LightGreen;
            this.cmdbtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdbtnCancel.Location = new System.Drawing.Point(124, 53);
            this.cmdbtnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.cmdbtnCancel.Name = "cmdbtnCancel";
            this.cmdbtnCancel.Size = new System.Drawing.Size(56, 22);
            this.cmdbtnCancel.TabIndex = 138;
            this.cmdbtnCancel.Text = "Cancel";
            this.cmdbtnCancel.UseVisualStyleBackColor = false;
            this.cmdbtnCancel.Click += new System.EventHandler(this.cmdbtnCancel_Click);
            // 
            // cmdbtnOK
            // 
            this.cmdbtnOK.BackColor = System.Drawing.Color.LightGreen;
            this.cmdbtnOK.Location = new System.Drawing.Point(41, 53);
            this.cmdbtnOK.Margin = new System.Windows.Forms.Padding(2);
            this.cmdbtnOK.Name = "cmdbtnOK";
            this.cmdbtnOK.Size = new System.Drawing.Size(50, 22);
            this.cmdbtnOK.TabIndex = 137;
            this.cmdbtnOK.Text = "OK";
            this.cmdbtnOK.UseVisualStyleBackColor = false;
            this.cmdbtnOK.Click += new System.EventHandler(this.cmdbtnOK_Click);
            // 
            // lblMultiplicationFactor
            // 
            this.lblMultiplicationFactor.AutoSize = true;
            this.lblMultiplicationFactor.Location = new System.Drawing.Point(11, 21);
            this.lblMultiplicationFactor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMultiplicationFactor.Name = "lblMultiplicationFactor";
            this.lblMultiplicationFactor.Size = new System.Drawing.Size(101, 13);
            this.lblMultiplicationFactor.TabIndex = 126;
            this.lblMultiplicationFactor.Text = "Multiplication Factor";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGreen;
            this.button1.Location = new System.Drawing.Point(185, 263);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 24);
            this.button1.TabIndex = 137;
            this.button1.Text = "Multiplication Factor";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblpolling
            // 
            this.lblpolling.AutoSize = true;
            this.lblpolling.Location = new System.Drawing.Point(29, 21);
            this.lblpolling.Name = "lblpolling";
            this.lblpolling.Size = new System.Drawing.Size(60, 13);
            this.lblpolling.TabIndex = 111;
            this.lblpolling.Text = "Polling (ms)";
            // 
            // lblpollingrange
            // 
            this.lblpollingrange.AutoSize = true;
            this.lblpollingrange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblpollingrange.Location = new System.Drawing.Point(388, 21);
            this.lblpollingrange.Name = "lblpollingrange";
            this.lblpollingrange.Size = new System.Drawing.Size(114, 13);
            this.lblpollingrange.TabIndex = 112;
            this.lblpollingrange.Text = "(Range: 0 to 3600000)";
            // 
            // labelDeviceId
            // 
            this.labelDeviceId.AutoSize = true;
            this.labelDeviceId.Location = new System.Drawing.Point(29, 61);
            this.labelDeviceId.Name = "labelDeviceId";
            this.labelDeviceId.Size = new System.Drawing.Size(53, 13);
            this.labelDeviceId.TabIndex = 114;
            this.labelDeviceId.Text = "Device Id";
            // 
            // labelDeviceIdRange
            // 
            this.labelDeviceIdRange.AutoSize = true;
            this.labelDeviceIdRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelDeviceIdRange.Location = new System.Drawing.Point(388, 61);
            this.labelDeviceIdRange.Name = "labelDeviceIdRange";
            this.labelDeviceIdRange.Size = new System.Drawing.Size(90, 13);
            this.labelDeviceIdRange.TabIndex = 115;
            this.labelDeviceIdRange.Text = "(Range: 1 to 255)";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(29, 101);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 13);
            this.lblAddress.TabIndex = 117;
            this.lblAddress.Text = "Address";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(29, 141);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(40, 13);
            this.lblLength.TabIndex = 120;
            this.lblLength.Text = "Length";
            // 
            // lblLengthRange
            // 
            this.lblLengthRange.AutoSize = true;
            this.lblLengthRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblLengthRange.Location = new System.Drawing.Point(388, 141);
            this.lblLengthRange.Name = "lblLengthRange";
            this.lblLengthRange.Size = new System.Drawing.Size(90, 13);
            this.lblLengthRange.TabIndex = 121;
            this.lblLengthRange.Text = "(Range: 1 to 100)";
            // 
            // textBoxVariable
            // 
            this.textBoxVariable.Location = new System.Drawing.Point(391, 215);
            this.textBoxVariable.Name = "textBoxVariable";
            this.textBoxVariable.Size = new System.Drawing.Size(111, 20);
            this.textBoxVariable.TabIndex = 8;
            this.textBoxVariable.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxVariable_Validating);
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Location = new System.Drawing.Point(29, 222);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(45, 13);
            this.lblVariable.TabIndex = 123;
            this.lblVariable.Text = "Variable";
            // 
            // lblFunctionCode
            // 
            this.lblFunctionCode.AutoSize = true;
            this.lblFunctionCode.Location = new System.Drawing.Point(29, 179);
            this.lblFunctionCode.Name = "lblFunctionCode";
            this.lblFunctionCode.Size = new System.Drawing.Size(76, 13);
            this.lblFunctionCode.TabIndex = 125;
            this.lblFunctionCode.Text = "Function Code";
            // 
            // comboBoxFunctionCode
            // 
            this.comboBoxFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFunctionCode.DropDownWidth = 239;
            this.comboBoxFunctionCode.FormattingEnabled = true;
            this.comboBoxFunctionCode.Location = new System.Drawing.Point(186, 176);
            this.comboBoxFunctionCode.Name = "comboBoxFunctionCode";
            this.comboBoxFunctionCode.Size = new System.Drawing.Size(139, 21);
            this.comboBoxFunctionCode.TabIndex = 6;
            this.comboBoxFunctionCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFunctionCode_SelectedIndexChanged);
            this.comboBoxFunctionCode.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxFunctionCode_Validating);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.LightGreen;
            this.buttonSave.Location = new System.Drawing.Point(229, 329);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // Length
            // 
            this.Length.Location = new System.Drawing.Point(186, 141);
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
            this.Length.TabIndex = 5;
            this.Length.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Length.Leave += new System.EventHandler(this.Length_Leave);
            this.Length.Validating += new System.ComponentModel.CancelEventHandler(this.Length_Validating);
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(186, 101);
            this.Address.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(95, 20);
            this.Address.TabIndex = 4;
            this.Address.Leave += new System.EventHandler(this.Address_Leave);
            // 
            // DeviceId
            // 
            this.DeviceId.Location = new System.Drawing.Point(186, 61);
            this.DeviceId.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.DeviceId.Name = "DeviceId";
            this.DeviceId.Size = new System.Drawing.Size(95, 20);
            this.DeviceId.TabIndex = 3;
            this.DeviceId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DeviceId.Leave += new System.EventHandler(this.DeviceId_Leave);
            // 
            // Polling
            // 
            this.Polling.Increment = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Polling.Location = new System.Drawing.Point(186, 19);
            this.Polling.Maximum = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
            this.Polling.Name = "Polling";
            this.Polling.Size = new System.Drawing.Size(103, 20);
            this.Polling.TabIndex = 2;
            this.Polling.Leave += new System.EventHandler(this.Polling_Leave);
            // 
            // TagEnable
            // 
            this.TagEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TagEnable.DropDownWidth = 196;
            this.TagEnable.FormattingEnabled = true;
            this.TagEnable.Items.AddRange(new object[] {
            "Normal Operand",
            "Numeric Operand"});
            this.TagEnable.Location = new System.Drawing.Point(185, 214);
            this.TagEnable.Name = "TagEnable";
            this.TagEnable.Size = new System.Drawing.Size(140, 21);
            this.TagEnable.TabIndex = 7;
            this.TagEnable.SelectedIndexChanged += new System.EventHandler(this.TagEnable_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAddressRange);
            this.groupBox1.Controls.Add(this.lblDeviceIdRange);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.TagEnable);
            this.groupBox1.Controls.Add(this.Polling);
            this.groupBox1.Controls.Add(this.DeviceId);
            this.groupBox1.Controls.Add(this.Address);
            this.groupBox1.Controls.Add(this.Length);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.comboBoxFunctionCode);
            this.groupBox1.Controls.Add(this.lblFunctionCode);
            this.groupBox1.Controls.Add(this.lblVariable);
            this.groupBox1.Controls.Add(this.textBoxVariable);
            this.groupBox1.Controls.Add(this.lblLengthRange);
            this.groupBox1.Controls.Add(this.lblLength);
            this.groupBox1.Controls.Add(this.lblAddress);
            this.groupBox1.Controls.Add(this.labelDeviceIdRange);
            this.groupBox1.Controls.Add(this.labelDeviceId);
            this.groupBox1.Controls.Add(this.lblpollingrange);
            this.groupBox1.Controls.Add(this.lblpolling);
            this.groupBox1.Location = new System.Drawing.Point(0, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 374);
            this.groupBox1.TabIndex = 109;
            this.groupBox1.TabStop = false;
            // 
            // ModbusRTUUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ModbusRTUUserControl";
            this.Size = new System.Drawing.Size(576, 382);
            this.Load += new System.EventHandler(this.ModbusRTUUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Polling)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox TagEnable;
        private Core.Base.Helpers.MyNumericUpDown Polling;
        private Core.Base.Helpers.MyNumericUpDown DeviceId;
        private Core.Base.Helpers.MyNumericUpDown Address;
        private Core.Base.Helpers.MyNumericUpDown Length;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxFunctionCode;
        private System.Windows.Forms.Label lblFunctionCode;
        private System.Windows.Forms.Label lblVariable;
        private System.Windows.Forms.TextBox textBoxVariable;
        private System.Windows.Forms.Label lblLengthRange;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblAddressRange;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label labelDeviceIdRange;
        private System.Windows.Forms.Label labelDeviceId;
        private System.Windows.Forms.Label lblpollingrange;
        private System.Windows.Forms.Label lblpolling;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdbtnCancel;
        private System.Windows.Forms.Button cmdbtnOK;
        private System.Windows.Forms.Label lblMultiplicationFactor;
      //private System.Windows.Forms.Label lblAddressRange;
        private System.Windows.Forms.Label lblDeviceIdRange;
        private System.Windows.Forms.ComboBox cmbmultiplicationfact;
    }
}
