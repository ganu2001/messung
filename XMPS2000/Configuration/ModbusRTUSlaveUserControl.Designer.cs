namespace XMPS2000.Configuration
{
    partial class ModbusRTUSlaveUserControl
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
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblFunctionCode = new System.Windows.Forms.Label();
            this.lblVariable = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxFunctionCode = new System.Windows.Forms.ComboBox();
            this.TagEnable = new System.Windows.Forms.ComboBox();
            this.textBoxVariable = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.Length = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.Address = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.Length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(68, 71);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 13);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "Address";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(68, 126);
            this.lblLength.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(40, 13);
            this.lblLength.TabIndex = 1;
            this.lblLength.Text = "Length";
            // 
            // lblFunctionCode
            // 
            this.lblFunctionCode.AutoSize = true;
            this.lblFunctionCode.Location = new System.Drawing.Point(68, 184);
            this.lblFunctionCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFunctionCode.Name = "lblFunctionCode";
            this.lblFunctionCode.Size = new System.Drawing.Size(76, 13);
            this.lblFunctionCode.TabIndex = 2;
            this.lblFunctionCode.Text = "Function Code";
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Location = new System.Drawing.Point(68, 231);
            this.lblVariable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(45, 13);
            this.lblVariable.TabIndex = 3;
            this.lblVariable.Text = "Variable";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(362, 71);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "(Range 0 to 9998)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 126);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "(Range 1 to 255)";
            // 
            // comboBoxFunctionCode
            // 
            this.comboBoxFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFunctionCode.FormattingEnabled = true;
            this.comboBoxFunctionCode.Location = new System.Drawing.Point(162, 181);
            this.comboBoxFunctionCode.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxFunctionCode.Name = "comboBoxFunctionCode";
            this.comboBoxFunctionCode.Size = new System.Drawing.Size(171, 21);
            this.comboBoxFunctionCode.TabIndex = 9;
            this.comboBoxFunctionCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFunctionCode_SelectedIndexChanged);
            this.comboBoxFunctionCode.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxFunctionCode_Validating);
            // 
            // TagEnable
            // 
            this.TagEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TagEnable.FormattingEnabled = true;
            this.TagEnable.Location = new System.Drawing.Point(162, 228);
            this.TagEnable.Margin = new System.Windows.Forms.Padding(2);
            this.TagEnable.Name = "TagEnable";
            this.TagEnable.Size = new System.Drawing.Size(171, 21);
            this.TagEnable.TabIndex = 10;
            this.TagEnable.SelectedIndexChanged += new System.EventHandler(this.TagEnable_SelectedIndexChanged);
            // 
            // textBoxVariable
            // 
            this.textBoxVariable.Location = new System.Drawing.Point(364, 231);
            this.textBoxVariable.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxVariable.Name = "textBoxVariable";
            this.textBoxVariable.Size = new System.Drawing.Size(102, 20);
            this.textBoxVariable.TabIndex = 11;
            this.textBoxVariable.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxVariable_Validating);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightGreen;
            this.btnSave.Location = new System.Drawing.Point(193, 281);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Length
            // 
            this.Length.Location = new System.Drawing.Point(162, 126);
            this.Length.Margin = new System.Windows.Forms.Padding(2);
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
            this.Length.Size = new System.Drawing.Size(107, 20);
            this.Length.TabIndex = 7;
            this.Length.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Length.Leave += new System.EventHandler(this.Length_Leave);
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(162, 69);
            this.Address.Margin = new System.Windows.Forms.Padding(2);
            this.Address.Maximum = new decimal(new int[] {
            9998,
            0,
            0,
            0});
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(107, 20);
            this.Address.TabIndex = 6;
            this.Address.Leave += new System.EventHandler(this.Address_Leave);
            // 
            // ModbusRTUSlaveUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.textBoxVariable);
            this.Controls.Add(this.TagEnable);
            this.Controls.Add(this.comboBoxFunctionCode);
            this.Controls.Add(this.Length);
            this.Controls.Add(this.Address);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblVariable);
            this.Controls.Add(this.lblFunctionCode);
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.lblAddress);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ModbusRTUSlaveUserControl";
            this.Size = new System.Drawing.Size(527, 349);
            this.Load += new System.EventHandler(this.ModbusRTUSlaveUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblFunctionCode;
        private System.Windows.Forms.Label lblVariable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Core.Base.Helpers.MyNumericUpDown Address;
        private Core.Base.Helpers.MyNumericUpDown Length;
        private System.Windows.Forms.ComboBox comboBoxFunctionCode;
        private System.Windows.Forms.ComboBox TagEnable;
        private System.Windows.Forms.TextBox textBoxVariable;
        private System.Windows.Forms.Button btnSave;
    }
}
