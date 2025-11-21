using XMPS2000.Core.Base.Helpers;

namespace XMPS2000
{
    partial class COMSettingsUserControl
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
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.comboBoxStopBit = new System.Windows.Forms.ComboBox();
            this.comboBoxDataLength = new System.Windows.Forms.ComboBox();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.lblparity = new System.Windows.Forms.Label();
            this.lblstopbit = new System.Windows.Forms.Label();
            this.lbldatalen = new System.Windows.Forms.Label();
            this.lblbaudrt = new System.Windows.Forms.Label();
            this.MinimumInterface = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.lblmininterface = new System.Windows.Forms.Label();
            this.lblsenddelay = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SendDelay = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblnoretriesrange = new System.Windows.Forms.Label();
            this.lblnoretries = new System.Windows.Forms.Label();
            this.lblcomtoutrange = new System.Windows.Forms.Label();
            this.lblcomtout = new System.Windows.Forms.Label();
            this.grpadvance = new System.Windows.Forms.GroupBox();
            this.NumberOfRetries = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.CommunicationTimeout = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.btnAdvance = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumInterface)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SendDelay)).BeginInit();
            this.grpadvance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfRetries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommunicationTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxParity);
            this.groupBox1.Controls.Add(this.comboBoxStopBit);
            this.groupBox1.Controls.Add(this.comboBoxDataLength);
            this.groupBox1.Controls.Add(this.comboBoxBaudRate);
            this.groupBox1.Controls.Add(this.lblparity);
            this.groupBox1.Controls.Add(this.lblstopbit);
            this.groupBox1.Controls.Add(this.lbldatalen);
            this.groupBox1.Controls.Add(this.lblbaudrt);
            this.groupBox1.Location = new System.Drawing.Point(20, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(459, 268);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "COM (RS485) Settings";
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Location = new System.Drawing.Point(180, 213);
            this.comboBoxParity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(241, 24);
            this.comboBoxParity.TabIndex = 4;
            // 
            // comboBoxStopBit
            // 
            this.comboBoxStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStopBit.FormattingEnabled = true;
            this.comboBoxStopBit.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBoxStopBit.Location = new System.Drawing.Point(180, 158);
            this.comboBoxStopBit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxStopBit.Name = "comboBoxStopBit";
            this.comboBoxStopBit.Size = new System.Drawing.Size(241, 24);
            this.comboBoxStopBit.TabIndex = 3;
            // 
            // comboBoxDataLength
            // 
            this.comboBoxDataLength.DisplayMember = "8";
            this.comboBoxDataLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataLength.FormattingEnabled = true;
            this.comboBoxDataLength.Items.AddRange(new object[] {
            "8",
            "6",
            "7"});
            this.comboBoxDataLength.Location = new System.Drawing.Point(180, 101);
            this.comboBoxDataLength.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxDataLength.Name = "comboBoxDataLength";
            this.comboBoxDataLength.Size = new System.Drawing.Size(241, 24);
            this.comboBoxDataLength.TabIndex = 2;
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Location = new System.Drawing.Point(180, 47);
            this.comboBoxBaudRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(241, 24);
            this.comboBoxBaudRate.TabIndex = 1;
            // 
            // lblparity
            // 
            this.lblparity.AutoSize = true;
            this.lblparity.Location = new System.Drawing.Point(40, 217);
            this.lblparity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblparity.Name = "lblparity";
            this.lblparity.Size = new System.Drawing.Size(41, 16);
            this.lblparity.TabIndex = 3;
            this.lblparity.Text = "Parity";
            // 
            // lblstopbit
            // 
            this.lblstopbit.AutoSize = true;
            this.lblstopbit.Location = new System.Drawing.Point(40, 161);
            this.lblstopbit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblstopbit.Name = "lblstopbit";
            this.lblstopbit.Size = new System.Drawing.Size(53, 16);
            this.lblstopbit.TabIndex = 2;
            this.lblstopbit.Text = "Stop Bit";
            // 
            // lbldatalen
            // 
            this.lbldatalen.AutoSize = true;
            this.lbldatalen.Location = new System.Drawing.Point(40, 101);
            this.lbldatalen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbldatalen.Name = "lbldatalen";
            this.lbldatalen.Size = new System.Drawing.Size(79, 16);
            this.lbldatalen.TabIndex = 1;
            this.lbldatalen.Text = "Data Length";
            // 
            // lblbaudrt
            // 
            this.lblbaudrt.AutoSize = true;
            this.lblbaudrt.Location = new System.Drawing.Point(40, 50);
            this.lblbaudrt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblbaudrt.Name = "lblbaudrt";
            this.lblbaudrt.Size = new System.Drawing.Size(71, 16);
            this.lblbaudrt.TabIndex = 0;
            this.lblbaudrt.Text = "Baud Rate";
            // 
            // MinimumInterface
            // 
            this.MinimumInterface.BackColor = System.Drawing.SystemColors.Window;
            this.MinimumInterface.DecimalPlaces = 1;
            this.MinimumInterface.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.MinimumInterface.Location = new System.Drawing.Point(180, 105);
            this.MinimumInterface.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumInterface.Minimum = new decimal(new int[] {
            35,
            0,
            0,
            65536});
            this.MinimumInterface.Name = "MinimumInterface";
            this.MinimumInterface.ReadOnly = true;
            this.MinimumInterface.Size = new System.Drawing.Size(243, 22);
            this.MinimumInterface.TabIndex = 6;
            this.MinimumInterface.Value = new decimal(new int[] {
            35,
            0,
            0,
            65536});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(176, 68);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "(Range: 0 to 65535)";
            // 
            // lblmininterface
            // 
            this.lblmininterface.AutoSize = true;
            this.lblmininterface.Location = new System.Drawing.Point(40, 107);
            this.lblmininterface.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblmininterface.Name = "lblmininterface";
            this.lblmininterface.Size = new System.Drawing.Size(114, 16);
            this.lblmininterface.TabIndex = 11;
            this.lblmininterface.Text = "Minimum Interface";
            // 
            // lblsenddelay
            // 
            this.lblsenddelay.AutoSize = true;
            this.lblsenddelay.Location = new System.Drawing.Point(40, 42);
            this.lblsenddelay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblsenddelay.Name = "lblsenddelay";
            this.lblsenddelay.Size = new System.Drawing.Size(107, 16);
            this.lblsenddelay.TabIndex = 9;
            this.lblsenddelay.Text = "Send Delay (ms)";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(200, 647);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 28);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.MinimumInterface);
            this.groupBox2.Controls.Add(this.SendDelay);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblsenddelay);
            this.groupBox2.Controls.Add(this.lblmininterface);
            this.groupBox2.Location = new System.Drawing.Point(20, 279);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(459, 174);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modbus RTU Master Settings";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label8.Location = new System.Drawing.Point(176, 133);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "(Range: 3.5 to 100)";
            // 
            // SendDelay
            // 
            this.SendDelay.Location = new System.Drawing.Point(180, 39);
            this.SendDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SendDelay.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.SendDelay.Name = "SendDelay";
            this.SendDelay.Size = new System.Drawing.Size(243, 22);
            this.SendDelay.TabIndex = 5;
            // 
            // lblnoretriesrange
            // 
            this.lblnoretriesrange.AutoSize = true;
            this.lblnoretriesrange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblnoretriesrange.Location = new System.Drawing.Point(244, 127);
            this.lblnoretriesrange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblnoretriesrange.Name = "lblnoretriesrange";
            this.lblnoretriesrange.Size = new System.Drawing.Size(93, 16);
            this.lblnoretriesrange.TabIndex = 115;
            this.lblnoretriesrange.Text = "(Range: 1 to 9)";
            // 
            // lblnoretries
            // 
            this.lblnoretries.AutoSize = true;
            this.lblnoretries.Location = new System.Drawing.Point(40, 98);
            this.lblnoretries.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblnoretries.Name = "lblnoretries";
            this.lblnoretries.Size = new System.Drawing.Size(115, 16);
            this.lblnoretries.TabIndex = 114;
            this.lblnoretries.Text = "Number of Retries";
            // 
            // lblcomtoutrange
            // 
            this.lblcomtoutrange.AutoSize = true;
            this.lblcomtoutrange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblcomtoutrange.Location = new System.Drawing.Point(227, 59);
            this.lblcomtoutrange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcomtoutrange.Name = "lblcomtoutrange";
            this.lblcomtoutrange.Size = new System.Drawing.Size(128, 16);
            this.lblcomtoutrange.TabIndex = 113;
            this.lblcomtoutrange.Text = "(Range: 50 to 65535)";
            // 
            // lblcomtout
            // 
            this.lblcomtout.AutoSize = true;
            this.lblcomtout.Location = new System.Drawing.Point(40, 33);
            this.lblcomtout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcomtout.Name = "lblcomtout";
            this.lblcomtout.Size = new System.Drawing.Size(180, 16);
            this.lblcomtout.TabIndex = 112;
            this.lblcomtout.Text = "Communication Timeout (ms)";
            // 
            // grpadvance
            // 
            this.grpadvance.Controls.Add(this.lblcomtoutrange);
            this.grpadvance.Controls.Add(this.lblnoretriesrange);
            this.grpadvance.Controls.Add(this.NumberOfRetries);
            this.grpadvance.Controls.Add(this.lblcomtout);
            this.grpadvance.Controls.Add(this.CommunicationTimeout);
            this.grpadvance.Controls.Add(this.lblnoretries);
            this.grpadvance.Enabled = false;
            this.grpadvance.Location = new System.Drawing.Point(20, 484);
            this.grpadvance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpadvance.Name = "grpadvance";
            this.grpadvance.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpadvance.Size = new System.Drawing.Size(459, 156);
            this.grpadvance.TabIndex = 116;
            this.grpadvance.TabStop = false;
            this.grpadvance.Text = "Advanced Settings";
            // 
            // NumberOfRetries
            // 
            this.NumberOfRetries.Location = new System.Drawing.Point(237, 98);
            this.NumberOfRetries.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NumberOfRetries.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.NumberOfRetries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumberOfRetries.Name = "NumberOfRetries";
            this.NumberOfRetries.Size = new System.Drawing.Size(137, 22);
            this.NumberOfRetries.TabIndex = 111;
            this.NumberOfRetries.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // CommunicationTimeout
            // 
            this.CommunicationTimeout.Location = new System.Drawing.Point(237, 31);
            this.CommunicationTimeout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CommunicationTimeout.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.CommunicationTimeout.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.CommunicationTimeout.Name = "CommunicationTimeout";
            this.CommunicationTimeout.Size = new System.Drawing.Size(137, 22);
            this.CommunicationTimeout.TabIndex = 110;
            this.CommunicationTimeout.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // btnAdvance
            // 
            this.btnAdvance.Location = new System.Drawing.Point(19, 455);
            this.btnAdvance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Size = new System.Drawing.Size(153, 28);
            this.btnAdvance.TabIndex = 117;
            this.btnAdvance.Text = "Advance Settings";
            this.btnAdvance.UseVisualStyleBackColor = true;
            this.btnAdvance.Click += new System.EventHandler(this.btnAdvance_Click);
            // 
            // COMSettingsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.btnAdvance);
            this.Controls.Add(this.grpadvance);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSave);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(527, 574);
            this.MinimumSize = new System.Drawing.Size(527, 697);
            this.Name = "COMSettingsUserControl";
            this.Size = new System.Drawing.Size(527, 697);
            this.Load += new System.EventHandler(this.COMSettingsUserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumInterface)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SendDelay)).EndInit();
            this.grpadvance.ResumeLayout(false);
            this.grpadvance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfRetries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommunicationTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.ComboBox comboBoxStopBit;
        private System.Windows.Forms.ComboBox comboBoxDataLength;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Label lblparity;
        private System.Windows.Forms.Label lblstopbit;
        private System.Windows.Forms.Label lbldatalen;
        private System.Windows.Forms.Label lblbaudrt;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label lblmininterface;
        private System.Windows.Forms.Label lblsenddelay;
        private MyNumericUpDown SendDelay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown MinimumInterface;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MyNumericUpDown NumberOfRetries;
        private MyNumericUpDown CommunicationTimeout;
        private System.Windows.Forms.Label lblnoretriesrange;
        private System.Windows.Forms.Label lblnoretries;
        private System.Windows.Forms.Label lblcomtoutrange;
        private System.Windows.Forms.Label lblcomtout;
        private System.Windows.Forms.GroupBox grpadvance;
        private System.Windows.Forms.Button btnAdvance;
    }
}
