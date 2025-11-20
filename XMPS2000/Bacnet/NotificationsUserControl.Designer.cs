using System.Windows.Forms;
using System;

namespace XMPS2000.Bacnet
{
    partial class NotificationsUserControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtpkrEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpkrStartTime = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.textBoxDeviceInst = new System.Windows.Forms.TextBox();
            this.comboReceipentType = new System.Windows.Forms.ComboBox();
            this.chkUnconfirmed = new System.Windows.Forms.CheckBox();
            this.chkConfirmed = new System.Windows.Forms.CheckBox();
            this.textProcessInd = new System.Windows.Forms.TextBox();
            this.chkSat = new System.Windows.Forms.CheckBox();
            this.chkThu = new System.Windows.Forms.CheckBox();
            this.chkFri = new System.Windows.Forms.CheckBox();
            this.chkTue = new System.Windows.Forms.CheckBox();
            this.chkWed = new System.Windows.Forms.CheckBox();
            this.chkMon = new System.Windows.Forms.CheckBox();
            this.chkSun = new System.Windows.Forms.CheckBox();
            this.textName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkToNormal = new System.Windows.Forms.CheckBox();
            this.chkToFault = new System.Windows.Forms.CheckBox();
            this.chkTooffNormal = new System.Windows.Forms.CheckBox();
            this.lblTransition = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkToNormal);
            this.groupBox1.Controls.Add(this.chkToFault);
            this.groupBox1.Controls.Add(this.chkTooffNormal);
            this.groupBox1.Controls.Add(this.lblTransition);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.dtpkrEndTime);
            this.groupBox1.Controls.Add(this.dtpkrStartTime);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.textBoxDeviceInst);
            this.groupBox1.Controls.Add(this.comboReceipentType);
            this.groupBox1.Controls.Add(this.chkUnconfirmed);
            this.groupBox1.Controls.Add(this.chkConfirmed);
            this.groupBox1.Controls.Add(this.textProcessInd);
            this.groupBox1.Controls.Add(this.chkSat);
            this.groupBox1.Controls.Add(this.chkThu);
            this.groupBox1.Controls.Add(this.chkFri);
            this.groupBox1.Controls.Add(this.chkTue);
            this.groupBox1.Controls.Add(this.chkWed);
            this.groupBox1.Controls.Add(this.chkMon);
            this.groupBox1.Controls.Add(this.chkSun);
            this.groupBox1.Controls.Add(this.textName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 277);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Recipient";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(443, 235);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 29);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dtpkrEndTime
            // 
            this.dtpkrEndTime.CustomFormat = "HH:mm:ss";
            this.dtpkrEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkrEndTime.Location = new System.Drawing.Point(144, 109);
            this.dtpkrEndTime.Name = "dtpkrEndTime";
            this.dtpkrEndTime.ShowUpDown = true;
            this.dtpkrEndTime.Size = new System.Drawing.Size(70, 20);
            this.dtpkrEndTime.TabIndex = 10;
            this.dtpkrEndTime.Value = new System.DateTime(2024, 6, 25, 0, 0, 0, 0);
            this.dtpkrEndTime.Validating += new System.ComponentModel.CancelEventHandler(this.dtpkrEndTime_Validating);
            // 
            // dtpkrStartTime
            // 
            this.dtpkrStartTime.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpkrStartTime.CustomFormat = "HH:mm:ss";
            this.dtpkrStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkrStartTime.Location = new System.Drawing.Point(144, 80);
            this.dtpkrStartTime.Name = "dtpkrStartTime";
            this.dtpkrStartTime.ShowUpDown = true;
            this.dtpkrStartTime.Size = new System.Drawing.Size(70, 20);
            this.dtpkrStartTime.TabIndex = 9;
            this.dtpkrStartTime.Value = new System.DateTime(2024, 6, 25, 0, 0, 0, 0);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(342, 235);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 29);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textBoxDeviceInst
            // 
            this.textBoxDeviceInst.Location = new System.Drawing.Point(144, 248);
            this.textBoxDeviceInst.MaxLength = 20;
            this.textBoxDeviceInst.Name = "textBoxDeviceInst";
            this.textBoxDeviceInst.Size = new System.Drawing.Size(70, 20);
            this.textBoxDeviceInst.TabIndex = 18;
            this.textBoxDeviceInst.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDeviceInst_KeyPress);
            this.textBoxDeviceInst.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDeviceInst_Validating);
            // 
            // comboReceipentType
            // 
            this.comboReceipentType.FormattingEnabled = true;
            this.comboReceipentType.Items.AddRange(new object[] {
            "Device"});
            this.comboReceipentType.Location = new System.Drawing.Point(144, 217);
            this.comboReceipentType.Name = "comboReceipentType";
            this.comboReceipentType.Size = new System.Drawing.Size(70, 21);
            this.comboReceipentType.TabIndex = 17;
            this.comboReceipentType.Validating += new System.ComponentModel.CancelEventHandler(this.comboReceipentType_Validating);
            // 
            // chkUnconfirmed
            // 
            this.chkUnconfirmed.AutoSize = true;
            this.chkUnconfirmed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUnconfirmed.Location = new System.Drawing.Point(144, 165);
            this.chkUnconfirmed.Name = "chkUnconfirmed";
            this.chkUnconfirmed.Size = new System.Drawing.Size(86, 17);
            this.chkUnconfirmed.TabIndex = 12;
            this.chkUnconfirmed.Text = "Unconfirmed";
            this.chkUnconfirmed.UseVisualStyleBackColor = true;
            this.chkUnconfirmed.CheckedChanged += new System.EventHandler(this.chkUnconfirmed_CheckedChanged);
            // 
            // chkConfirmed
            // 
            this.chkConfirmed.AutoSize = true;
            this.chkConfirmed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConfirmed.Location = new System.Drawing.Point(260, 165);
            this.chkConfirmed.Name = "chkConfirmed";
            this.chkConfirmed.Size = new System.Drawing.Size(73, 17);
            this.chkConfirmed.TabIndex = 13;
            this.chkConfirmed.Text = "Confirmed";
            this.chkConfirmed.UseVisualStyleBackColor = true;
            this.chkConfirmed.CheckedChanged += new System.EventHandler(this.chkConfirmed_CheckedChanged);
            // 
            // textProcessInd
            // 
            this.textProcessInd.Location = new System.Drawing.Point(144, 135);
            this.textProcessInd.MaxLength = 20;
            this.textProcessInd.Name = "textProcessInd";
            this.textProcessInd.Size = new System.Drawing.Size(70, 20);
            this.textProcessInd.TabIndex = 11;
            this.textProcessInd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textProcessInd_KeyPress);
            this.textProcessInd.Validating += new System.ComponentModel.CancelEventHandler(this.textProcessInd_Validating);
            // 
            // chkSat
            // 
            this.chkSat.AutoSize = true;
            this.chkSat.Checked = true;
            this.chkSat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSat.Location = new System.Drawing.Point(430, 56);
            this.chkSat.Name = "chkSat";
            this.chkSat.Size = new System.Drawing.Size(42, 17);
            this.chkSat.TabIndex = 8;
            this.chkSat.Text = "Sat";
            this.chkSat.UseVisualStyleBackColor = true;
            // 
            // chkThu
            // 
            this.chkThu.AutoSize = true;
            this.chkThu.Checked = true;
            this.chkThu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThu.Location = new System.Drawing.Point(324, 56);
            this.chkThu.Name = "chkThu";
            this.chkThu.Size = new System.Drawing.Size(45, 17);
            this.chkThu.TabIndex = 6;
            this.chkThu.Text = "Thu";
            this.chkThu.UseVisualStyleBackColor = true;
            // 
            // chkFri
            // 
            this.chkFri.AutoSize = true;
            this.chkFri.Checked = true;
            this.chkFri.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFri.Location = new System.Drawing.Point(381, 56);
            this.chkFri.Name = "chkFri";
            this.chkFri.Size = new System.Drawing.Size(37, 17);
            this.chkFri.TabIndex = 7;
            this.chkFri.Text = "Fri";
            this.chkFri.UseVisualStyleBackColor = true;
            // 
            // chkTue
            // 
            this.chkTue.AutoSize = true;
            this.chkTue.Checked = true;
            this.chkTue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTue.Location = new System.Drawing.Point(206, 57);
            this.chkTue.Name = "chkTue";
            this.chkTue.Size = new System.Drawing.Size(45, 17);
            this.chkTue.TabIndex = 4;
            this.chkTue.Text = "Tue";
            this.chkTue.UseVisualStyleBackColor = true;
            // 
            // chkWed
            // 
            this.chkWed.AutoSize = true;
            this.chkWed.Checked = true;
            this.chkWed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWed.Location = new System.Drawing.Point(263, 56);
            this.chkWed.Name = "chkWed";
            this.chkWed.Size = new System.Drawing.Size(49, 17);
            this.chkWed.TabIndex = 5;
            this.chkWed.Text = "Wed";
            this.chkWed.UseVisualStyleBackColor = true;
            // 
            // chkMon
            // 
            this.chkMon.AutoSize = true;
            this.chkMon.Checked = true;
            this.chkMon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMon.Location = new System.Drawing.Point(147, 56);
            this.chkMon.Name = "chkMon";
            this.chkMon.Size = new System.Drawing.Size(47, 17);
            this.chkMon.TabIndex = 3;
            this.chkMon.Text = "Mon";
            this.chkMon.UseVisualStyleBackColor = true;
            // 
            // chkSun
            // 
            this.chkSun.AutoSize = true;
            this.chkSun.Checked = true;
            this.chkSun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSun.Location = new System.Drawing.Point(485, 57);
            this.chkSun.Name = "chkSun";
            this.chkSun.Size = new System.Drawing.Size(45, 17);
            this.chkSun.TabIndex = 2;
            this.chkSun.Text = "Sun";
            this.chkSun.UseVisualStyleBackColor = true;
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(144, 23);
            this.textName.MaxLength = 20;
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(236, 20);
            this.textName.TabIndex = 1;
            this.textName.Validating += new System.ComponentModel.CancelEventHandler(this.textName_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 248);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Device Instance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Recipient Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Notifications";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Process Identifier";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "End Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Start Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Days of Week";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // chkToNormal
            // 
            this.chkToNormal.AutoSize = true;
            this.chkToNormal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkToNormal.Checked = true;
            this.chkToNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkToNormal.Location = new System.Drawing.Point(299, 190);
            this.chkToNormal.Name = "chkToNormal";
            this.chkToNormal.Size = new System.Drawing.Size(76, 17);
            this.chkToNormal.TabIndex = 65;
            this.chkToNormal.Text = "To normal:";
            this.chkToNormal.UseVisualStyleBackColor = true;
            // 
            // chkToFault
            // 
            this.chkToFault.AutoSize = true;
            this.chkToFault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkToFault.Checked = true;
            this.chkToFault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkToFault.Location = new System.Drawing.Point(231, 190);
            this.chkToFault.Name = "chkToFault";
            this.chkToFault.Size = new System.Drawing.Size(65, 17);
            this.chkToFault.TabIndex = 64;
            this.chkToFault.Text = "To fault:";
            this.chkToFault.UseVisualStyleBackColor = true;
            // 
            // chkTooffNormal
            // 
            this.chkTooffNormal.AutoSize = true;
            this.chkTooffNormal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTooffNormal.Checked = true;
            this.chkTooffNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTooffNormal.Location = new System.Drawing.Point(139, 190);
            this.chkTooffNormal.Name = "chkTooffNormal";
            this.chkTooffNormal.Size = new System.Drawing.Size(88, 17);
            this.chkTooffNormal.TabIndex = 63;
            this.chkTooffNormal.Text = "To offnormal:";
            this.chkTooffNormal.UseVisualStyleBackColor = true;
            // 
            // lblTransition
            // 
            this.lblTransition.AutoSize = true;
            this.lblTransition.Location = new System.Drawing.Point(10, 194);
            this.lblTransition.Name = "lblTransition";
            this.lblTransition.Size = new System.Drawing.Size(58, 13);
            this.lblTransition.TabIndex = 62;
            this.lblTransition.Text = "Transitions";
            // 
            // NotificationsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(543, 287);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotificationsUserControl";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.CheckBox chkSun;
        private System.Windows.Forms.CheckBox chkSat;
        private System.Windows.Forms.CheckBox chkThu;
        private System.Windows.Forms.CheckBox chkFri;
        private System.Windows.Forms.CheckBox chkTue;
        private System.Windows.Forms.CheckBox chkWed;
        private System.Windows.Forms.CheckBox chkMon;
        private System.Windows.Forms.CheckBox chkUnconfirmed;
        private System.Windows.Forms.CheckBox chkConfirmed;
        private System.Windows.Forms.TextBox textProcessInd;
        private System.Windows.Forms.ComboBox comboReceipentType;
        private System.Windows.Forms.TextBox textBoxDeviceInst;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DateTimePicker dtpkrStartTime;
        private System.Windows.Forms.DateTimePicker dtpkrEndTime;
        private System.Windows.Forms.Button btnCancel;
        private ErrorProvider errorProvider;
        private CheckBox chkToNormal;
        private CheckBox chkToFault;
        private CheckBox chkTooffNormal;
        private Label lblTransition;
    }
}
