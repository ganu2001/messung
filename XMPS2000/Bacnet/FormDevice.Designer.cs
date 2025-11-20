using System;
using System.ComponentModel;

namespace XMPS2000.Bacnet
{
    partial class FormDevice
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ChkTrue = new System.Windows.Forms.CheckBox();
            this.ChkFalse = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textInstanceNo = new System.Windows.Forms.TextBox();
            this.labelObjType = new System.Windows.Forms.Label();
            this.textLocation = new System.Windows.Forms.TextBox();
            this.labelObjIdentifier = new System.Windows.Forms.Label();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.textObjectName = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();          
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericNoofAPDU = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.numericAPDUSegment = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.numericAPDUTimeout = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.textVenderName = new System.Windows.Forms.TextBox();
            this.textVenderId = new System.Windows.Forms.TextBox();
            this.textModelName = new System.Windows.Forms.TextBox();
            this.lblVendername = new System.Windows.Forms.Label();
            this.lblVenderId = new System.Windows.Forms.Label();
            this.lblNoApdu = new System.Windows.Forms.Label();
            this.lblApduSegment = new System.Windows.Forms.Label();
            this.lblapdu = new System.Windows.Forms.Label();
            this.lblModelName = new System.Windows.Forms.Label();
            this.cmb_UTCOffset = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNoofAPDU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAPDUSegment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAPDUTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_UTCOffset);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textInstanceNo);
            this.groupBox1.Controls.Add(this.labelObjType);
            this.groupBox1.Controls.Add(this.textLocation);
            this.groupBox1.Controls.Add(this.labelObjIdentifier);
            this.groupBox1.Controls.Add(this.textDescription);
            this.groupBox1.Controls.Add(this.textObjectName);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(-5, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(480, 346);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ChkTrue);
            this.groupBox3.Controls.Add(this.ChkFalse);
            this.groupBox3.Location = new System.Drawing.Point(199, 250);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(265, 59);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            // 
            // ChkTrue
            // 
            this.ChkTrue.AutoSize = true;
            this.ChkTrue.Location = new System.Drawing.Point(24, 21);
            this.ChkTrue.Name = "ChkTrue";
            this.ChkTrue.Size = new System.Drawing.Size(57, 20);
            this.ChkTrue.TabIndex = 50;
            this.ChkTrue.Text = "True";
            this.ChkTrue.UseVisualStyleBackColor = true;
            this.ChkTrue.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.ChkTrue.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // ChkFalse
            // 
            this.ChkFalse.AutoSize = true;
            this.ChkFalse.Checked = true;
            this.ChkFalse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkFalse.Location = new System.Drawing.Point(154, 21);
            this.ChkFalse.Name = "ChkFalse";
            this.ChkFalse.Size = new System.Drawing.Size(63, 20);
            this.ChkFalse.TabIndex = 51;
            this.ChkFalse.Text = "False";
            this.ChkFalse.UseVisualStyleBackColor = true;
            this.ChkFalse.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 316);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 49;
            this.label10.Text = "UTC Offset";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 267);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 16);
            this.label4.TabIndex = 47;
            this.label4.Text = "Daylight Saving Status";
            // 
            // textInstanceNo
            // 
            this.textInstanceNo.Location = new System.Drawing.Point(199, 60);
            this.textInstanceNo.Margin = new System.Windows.Forms.Padding(4);
            this.textInstanceNo.MaxLength = 0;
            this.textInstanceNo.Name = "textInstanceNo";
            this.textInstanceNo.Size = new System.Drawing.Size(264, 22);
            this.textInstanceNo.TabIndex = 2;
            this.textInstanceNo.Text = "2000";
            this.textInstanceNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textInstanceNo_KeyPress);
            this.textInstanceNo.Leave += new System.EventHandler(this.textInstanceNo_Leave);
            this.textInstanceNo.Validating += new System.ComponentModel.CancelEventHandler(this.textInstanceNo_Validating);
            this.textInstanceNo.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelObjType
            // 
            this.labelObjType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjType.Location = new System.Drawing.Point(199, 97);
            this.labelObjType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjType.Name = "labelObjType";
            this.labelObjType.Size = new System.Drawing.Size(265, 24);
            this.labelObjType.TabIndex = 45;
            this.labelObjType.Text = "8";
            this.labelObjType.TextChanged += new System.EventHandler(this.labelObjType_TextChanged);
            // 
            // textLocation
            // 
            this.textLocation.Location = new System.Drawing.Point(199, 215);
            this.textLocation.Margin = new System.Windows.Forms.Padding(4);
            this.textLocation.MaxLength = 20;
            this.textLocation.Name = "textLocation";
            this.textLocation.Size = new System.Drawing.Size(264, 22);
            this.textLocation.TabIndex = 13;
            this.textLocation.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelObjIdentifier
            // 
            this.labelObjIdentifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjIdentifier.Location = new System.Drawing.Point(199, 23);
            this.labelObjIdentifier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjIdentifier.Name = "labelObjIdentifier";
            this.labelObjIdentifier.Size = new System.Drawing.Size(265, 24);
            this.labelObjIdentifier.TabIndex = 43;
            this.labelObjIdentifier.Text = "000";
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(199, 175);
            this.textDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textDescription.MaxLength = 25;
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(264, 22);
            this.textDescription.TabIndex = 4;
            this.textDescription.Text = "Digital Input";
            this.textDescription.TextChanged += new System.EventHandler(this.textDescription_TextChanged);
            this.textDescription.Validating += new System.ComponentModel.CancelEventHandler(this.textDescription_Validating);
            this.textDescription.TextChanged += new EventHandler(this.textDescription_TextChanged);
            textDescription.TextChanged += BacNetValidator.ControlChanged;
            textDescription.Validated += BacNetValidator.ControlChanged;
            // 
            // textObjectName
            // 
            this.textObjectName.Location = new System.Drawing.Point(199, 138);
            this.textObjectName.Margin = new System.Windows.Forms.Padding(4);
            this.textObjectName.MaxLength = 25;
            this.textObjectName.Name = "textObjectName";
            this.textObjectName.Size = new System.Drawing.Size(264, 22);
            this.textObjectName.TabIndex = 3;
            this.textObjectName.TextChanged += new System.EventHandler(this.textObjectName_TextChanged);
            this.textObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textObjectName_Validating);
            this.textObjectName.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(27, 221);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(64, 16);
            this.lblLocation.TabIndex = 6;
            this.lblLocation.Text = "Location :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 180);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 16);
            this.label9.TabIndex = 39;
            this.label9.Text = "Description";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 102);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 16);
            this.label8.TabIndex = 38;
            this.label8.Text = "Object Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 143);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 16);
            this.label7.TabIndex = 37;
            this.label7.Text = "Object Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 65);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "Device Instance";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Object Identifier";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(373, 356);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 31);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.Location = new System.Drawing.Point(503, 356);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 31);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox2
            //          
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numericNoofAPDU);
            this.groupBox2.Controls.Add(this.numericAPDUSegment);
            this.groupBox2.Controls.Add(this.numericAPDUTimeout);
            this.groupBox2.Controls.Add(this.textVenderName);
            this.groupBox2.Controls.Add(this.textVenderId);
            this.groupBox2.Controls.Add(this.textModelName);
            this.groupBox2.Controls.Add(this.lblVendername);
            this.groupBox2.Controls.Add(this.lblVenderId);
            this.groupBox2.Controls.Add(this.lblNoApdu);
            this.groupBox2.Controls.Add(this.lblApduSegment);
            this.groupBox2.Controls.Add(this.lblapdu);
            this.groupBox2.Controls.Add(this.lblModelName);
            this.groupBox2.Location = new System.Drawing.Point(497, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(543, 246);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            //            
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(475, 102);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "ms";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(475, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "ms";
            // 
            // numericNoofAPDU
            // 
            this.numericNoofAPDU.Location = new System.Drawing.Point(208, 138);
            this.numericNoofAPDU.Margin = new System.Windows.Forms.Padding(4);
            this.numericNoofAPDU.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericNoofAPDU.Name = "numericNoofAPDU";
            this.numericNoofAPDU.Size = new System.Drawing.Size(259, 22);
            this.numericNoofAPDU.TabIndex = 16;
            this.numericNoofAPDU.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericNoofAPDU.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // numericAPDUSegment
            // 
            this.numericAPDUSegment.Location = new System.Drawing.Point(208, 97);
            this.numericAPDUSegment.Margin = new System.Windows.Forms.Padding(4);
            this.numericAPDUSegment.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericAPDUSegment.Name = "numericAPDUSegment";
            this.numericAPDUSegment.Size = new System.Drawing.Size(259, 22);
            this.numericAPDUSegment.TabIndex = 15;
            this.numericAPDUSegment.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericAPDUSegment.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // numericAPDUTimeout
            // 
            this.numericAPDUTimeout.Location = new System.Drawing.Point(208, 60);
            this.numericAPDUTimeout.Margin = new System.Windows.Forms.Padding(4);
            this.numericAPDUTimeout.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericAPDUTimeout.Name = "numericAPDUTimeout";
            this.numericAPDUTimeout.Size = new System.Drawing.Size(259, 22);
            this.numericAPDUTimeout.TabIndex = 14;
            this.numericAPDUTimeout.Value = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.numericAPDUTimeout.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // textVenderName
            // 
            this.textVenderName.Enabled = false;
            this.textVenderName.Location = new System.Drawing.Point(208, 210);
            this.textVenderName.Margin = new System.Windows.Forms.Padding(4);
            this.textVenderName.Name = "textVenderName";
            this.textVenderName.Size = new System.Drawing.Size(257, 22);
            this.textVenderName.TabIndex = 12;
            this.textVenderName.Text = " Messung Systems Pvt Ltd";
            // 
            // textVenderId
            // 
            this.textVenderId.Enabled = false;
            this.textVenderId.Location = new System.Drawing.Point(208, 175);
            this.textVenderId.Margin = new System.Windows.Forms.Padding(4);
            this.textVenderId.Name = "textVenderId";
            this.textVenderId.Size = new System.Drawing.Size(257, 22);
            this.textVenderId.TabIndex = 11;
            this.textVenderId.Text = "1501";
            // 
            // textModelName
            // 
            this.textModelName.Enabled = false;
            this.textModelName.Location = new System.Drawing.Point(208, 23);
            this.textModelName.Margin = new System.Windows.Forms.Padding(4);
            this.textModelName.MaxLength = 20;
            this.textModelName.Name = "textModelName";
            this.textModelName.Size = new System.Drawing.Size(257, 22);
            this.textModelName.TabIndex = 7;
            // 
            // lblVendername
            // 
            this.lblVendername.AutoSize = true;
            this.lblVendername.Location = new System.Drawing.Point(23, 215);
            this.lblVendername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVendername.Name = "lblVendername";
            this.lblVendername.Size = new System.Drawing.Size(97, 16);
            this.lblVendername.TabIndex = 5;
            this.lblVendername.Text = "Vendor Name :";
            // 
            // lblVenderId
            // 
            this.lblVenderId.AutoSize = true;
            this.lblVenderId.Location = new System.Drawing.Point(23, 180);
            this.lblVenderId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVenderId.Name = "lblVenderId";
            this.lblVenderId.Size = new System.Drawing.Size(73, 16);
            this.lblVenderId.TabIndex = 4;
            this.lblVenderId.Text = "Vendor ID :";
            // 
            // lblNoApdu
            // 
            this.lblNoApdu.AutoSize = true;
            this.lblNoApdu.Location = new System.Drawing.Point(23, 143);
            this.lblNoApdu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNoApdu.Name = "lblNoApdu";
            this.lblNoApdu.Size = new System.Drawing.Size(135, 16);
            this.lblNoApdu.TabIndex = 3;
            this.lblNoApdu.Text = "No. of APDU Retries :";
            // 
            // lblApduSegment
            // 
            this.lblApduSegment.AutoSize = true;
            this.lblApduSegment.Location = new System.Drawing.Point(23, 102);
            this.lblApduSegment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApduSegment.Name = "lblApduSegment";
            this.lblApduSegment.Size = new System.Drawing.Size(160, 16);
            this.lblApduSegment.TabIndex = 2;
            this.lblApduSegment.Text = "APDU Segment Timeout :";
            // 
            // lblapdu
            // 
            this.lblapdu.AutoSize = true;
            this.lblapdu.Location = new System.Drawing.Point(23, 65);
            this.lblapdu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblapdu.Name = "lblapdu";
            this.lblapdu.Size = new System.Drawing.Size(103, 16);
            this.lblapdu.TabIndex = 1;
            this.lblapdu.Text = "APDU Timeout :";
            // 
            // lblModelName
            // 
            this.lblModelName.AutoSize = true;
            this.lblModelName.Location = new System.Drawing.Point(23, 28);
            this.lblModelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModelName.Name = "lblModelName";
            this.lblModelName.Size = new System.Drawing.Size(91, 16);
            this.lblModelName.TabIndex = 0;
            this.lblModelName.Text = "Model Name :";
            // 
            // cmb_UTCOffset
            // 
            this.cmb_UTCOffset.FormattingEnabled = true;
            this.cmb_UTCOffset.Location = new System.Drawing.Point(199, 313);
            this.cmb_UTCOffset.Name = "cmb_UTCOffset";
            this.cmb_UTCOffset.Size = new System.Drawing.Size(264, 24);
            this.cmb_UTCOffset.TabIndex = 8;
            this.cmb_UTCOffset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_UTCOffset.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            cmb_UTCOffset.MouseWheel += Cmb_UTCOffset_MouseWheel;
            // FormDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(1056, 400);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDevice";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNoofAPDU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAPDUSegment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAPDUTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelObjType;
        private System.Windows.Forms.Label labelObjIdentifier;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.TextBox textObjectName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textInstanceNo;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblModelName;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblVendername;
        private System.Windows.Forms.Label lblVenderId;
        private System.Windows.Forms.Label lblNoApdu;
        private System.Windows.Forms.Label lblApduSegment;
        private System.Windows.Forms.Label lblapdu;
        private System.Windows.Forms.TextBox textModelName;
        private System.Windows.Forms.TextBox textLocation;
        private System.Windows.Forms.TextBox textVenderName;
        private System.Windows.Forms.TextBox textVenderId;
        private Core.Base.Helpers.MyNumericUpDown numericNoofAPDU;
        private Core.Base.Helpers.MyNumericUpDown numericAPDUSegment;
        private Core.Base.Helpers.MyNumericUpDown numericAPDUTimeout;       
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox ChkTrue;
        private System.Windows.Forms.CheckBox ChkFalse;
        private System.Windows.Forms.ComboBox cmb_UTCOffset;
    }
}