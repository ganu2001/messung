using System;
using System.Windows;

namespace XMPS2000.Bacnet
{
    partial class FormDigitalBacNet
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
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelReqdefault = new System.Windows.Forms.Label();
            this.textObjectName = new System.Windows.Forms.TextBox();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.textReDefault = new System.Windows.Forms.TextBox();
            this.labelObjIdentifier = new System.Windows.Forms.Label();
            this.labelInstanceno = new System.Windows.Forms.Label();
            this.labelObjType = new System.Windows.Forms.Label();
            this.labelDeviceType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbRelinquishDefault = new System.Windows.Forms.ComboBox();
            this.comboBoxInitialValue = new System.Windows.Forms.ComboBox();
            this.lblInitialValue = new System.Windows.Forms.Label();
            this.othervaluesgrpbox = new XMPS2000.BorderlessGroupBox();
            this.cmb_BinaryValue = new System.Windows.Forms.ComboBox();
            this.lbl_BinaryValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTimeDelayNormalUnit = new System.Windows.Forms.Label();
            this.lblTimeDelayUnit = new System.Windows.Forms.Label();
            this.comboboxFeedbackValue = new System.Windows.Forms.ComboBox();
            this.txtActive = new System.Windows.Forms.TextBox();
            this.txtInactive = new System.Windows.Forms.TextBox();
            this.lblActive = new System.Windows.Forms.Label();
            this.lblInactive = new System.Windows.Forms.Label();
            this.comboPolarity = new System.Windows.Forms.ComboBox();
            this.checkEnable = new System.Windows.Forms.CheckBox();
            this.labelfeedbackval = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textTimeDelayNormal = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.grpnotifytype = new System.Windows.Forms.GroupBox();
            this.checkEvent = new System.Windows.Forms.CheckBox();
            this.checkAlarm = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.grpeventenb = new System.Windows.Forms.GroupBox();
            this.checkToNormal = new System.Windows.Forms.CheckBox();
            this.checkToFault = new System.Windows.Forms.CheckBox();
            this.checktooffNormal = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textTimeDelay = new System.Windows.Forms.TextBox();
            this.comboNotifyclass = new System.Windows.Forms.ComboBox();

            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.othervaluesgrpbox.SuspendLayout();
            this.grpnotifytype.SuspendLayout();
            this.grpeventenb.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 39);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Object Identifier";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 76);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Instance Number";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 187);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Object Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 113);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Object Type";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 224);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "Description";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 150);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 9;
            this.label10.Text = "Device Type";
            // 
            // labelReqdefault
            // 
            this.labelReqdefault.AutoSize = true;
            this.labelReqdefault.Location = new System.Drawing.Point(8, 261);
            this.labelReqdefault.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReqdefault.Name = "labelReqdefault";
            this.labelReqdefault.Size = new System.Drawing.Size(115, 16);
            this.labelReqdefault.TabIndex = 10;
            this.labelReqdefault.Text = "Relinquish Default";
            // 
            // textObjectName
            // 
            this.textObjectName.Location = new System.Drawing.Point(180, 178);
            this.textObjectName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textObjectName.MaxLength = 25;
            this.textObjectName.Name = "textObjectName";
            this.textObjectName.Size = new System.Drawing.Size(313, 22);
            this.textObjectName.TabIndex = 17;
            this.textObjectName.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textObjectName.TextChanged += new System.EventHandler(this.textObjectName_TextChanged);
            this.textObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textObjectName_Validating);
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(180, 215);
            this.textDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textDescription.MaxLength = 25;
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(313, 22);
            this.textDescription.TabIndex = 18;
            this.textDescription.Text = "Digital Input";
            this.textDescription.Validating += new System.ComponentModel.CancelEventHandler(this.textDescription_Validating);
            this.textDescription.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textDescription.TextChanged += new System.EventHandler(this.textDescription_TextChanged);
            this.textDescription.Validating += new System.ComponentModel.CancelEventHandler(this.textDescription_Validating);
            // 
            // textReDefault
            // 
            this.textReDefault.Location = new System.Drawing.Point(240, 310);
            this.textReDefault.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.textReDefault.Name = "textReDefault";
            this.textReDefault.Size = new System.Drawing.Size(416, 22);
            this.textReDefault.TabIndex = 20;
            this.textReDefault.Text = "0";
            this.textReDefault.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textReDefault_KeyPress);
            this.textReDefault.Leave += new System.EventHandler(this.textReDefault_Leave);
            this.textReDefault.Validating += new System.ComponentModel.CancelEventHandler(this.textReDefault_Validating);
            this.textReDefault.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelObjIdentifier
            // 
            this.labelObjIdentifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjIdentifier.Location = new System.Drawing.Point(180, 31);
            this.labelObjIdentifier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjIdentifier.Name = "labelObjIdentifier";
            this.labelObjIdentifier.Size = new System.Drawing.Size(314, 24);
            this.labelObjIdentifier.TabIndex = 31;
            this.labelObjIdentifier.Text = "000";
            // 
            // labelInstanceno
            // 
            this.labelInstanceno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInstanceno.Location = new System.Drawing.Point(180, 68);
            this.labelInstanceno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInstanceno.Name = "labelInstanceno";
            this.labelInstanceno.Size = new System.Drawing.Size(314, 24);
            this.labelInstanceno.TabIndex = 32;
            this.labelInstanceno.Text = "000";
            // 
            // labelObjType
            // 
            this.labelObjType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjType.Location = new System.Drawing.Point(180, 105);
            this.labelObjType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjType.Name = "labelObjType";
            this.labelObjType.Size = new System.Drawing.Size(314, 24);
            this.labelObjType.TabIndex = 33;
            this.labelObjType.Text = "3";
            // 
            // labelDeviceType
            // 
            this.labelDeviceType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDeviceType.Location = new System.Drawing.Point(180, 142);
            this.labelDeviceType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDeviceType.Name = "labelDeviceType";
            this.labelDeviceType.Size = new System.Drawing.Size(314, 24);
            this.labelDeviceType.TabIndex = 34;
            this.labelDeviceType.Text = "3000";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(393, 793);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 31);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(277, 793);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 31);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbRelinquishDefault);
            this.groupBox1.Controls.Add(this.comboBoxInitialValue);
            this.groupBox1.Controls.Add(this.lblInitialValue);
            this.groupBox1.Controls.Add(this.othervaluesgrpbox);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.labelDeviceType);
            this.groupBox1.Controls.Add(this.labelObjType);
            this.groupBox1.Controls.Add(this.labelInstanceno);
            this.groupBox1.Controls.Add(this.labelObjIdentifier);
            this.groupBox1.Controls.Add(this.textReDefault);
            this.groupBox1.Controls.Add(this.textDescription);
            this.groupBox1.Controls.Add(this.textObjectName);
            this.groupBox1.Controls.Add(this.labelReqdefault);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(16, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(524, 862);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cmbRelinquishDefault
            // 
            this.cmbRelinquishDefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelinquishDefault.FormattingEnabled = true;
            this.cmbRelinquishDefault.Location = new System.Drawing.Point(180, 254);
            this.cmbRelinquishDefault.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRelinquishDefault.Name = "cmbRelinquishDefault";
            this.cmbRelinquishDefault.Size = new System.Drawing.Size(313, 24);
            this.cmbRelinquishDefault.TabIndex = 45;
            // 
            // comboBoxInitialValue
            // 
            this.comboBoxInitialValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInitialValue.FormattingEnabled = true;
            this.comboBoxInitialValue.Location = new System.Drawing.Point(176, 761);
            this.comboBoxInitialValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxInitialValue.Name = "comboBoxInitialValue";
            this.comboBoxInitialValue.Size = new System.Drawing.Size(313, 24);
            this.comboBoxInitialValue.TabIndex = 44;
            this.comboBoxInitialValue.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // lblInitialValue
            // 
            this.lblInitialValue.AutoSize = true;
            this.lblInitialValue.Location = new System.Drawing.Point(15, 764);
            this.lblInitialValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInitialValue.Name = "lblInitialValue";
            this.lblInitialValue.Size = new System.Drawing.Size(75, 16);
            this.lblInitialValue.TabIndex = 43;
            this.lblInitialValue.Text = "Initial Value";
            // 
            // othervaluesgrpbox
            // 
            this.othervaluesgrpbox.Controls.Add(this.cmb_BinaryValue);
            this.othervaluesgrpbox.Controls.Add(this.lbl_BinaryValue);
            this.othervaluesgrpbox.Controls.Add(this.label4);
            this.othervaluesgrpbox.Controls.Add(this.label3);
            this.othervaluesgrpbox.Controls.Add(this.lblTimeDelayNormalUnit);
            this.othervaluesgrpbox.Controls.Add(this.lblTimeDelayUnit);
            this.othervaluesgrpbox.Controls.Add(this.comboboxFeedbackValue);
            this.othervaluesgrpbox.Controls.Add(this.txtActive);
            this.othervaluesgrpbox.Controls.Add(this.txtInactive);
            this.othervaluesgrpbox.Controls.Add(this.lblActive);
            this.othervaluesgrpbox.Controls.Add(this.lblInactive);
            this.othervaluesgrpbox.Controls.Add(this.comboPolarity);
            this.othervaluesgrpbox.Controls.Add(this.checkEnable);
            this.othervaluesgrpbox.Controls.Add(this.labelfeedbackval);
            this.othervaluesgrpbox.Controls.Add(this.label12);
            this.othervaluesgrpbox.Controls.Add(this.textTimeDelayNormal);
            this.othervaluesgrpbox.Controls.Add(this.label13);
            this.othervaluesgrpbox.Controls.Add(this.label1);
            this.othervaluesgrpbox.Controls.Add(this.label15);
            this.othervaluesgrpbox.Controls.Add(this.grpnotifytype);
            this.othervaluesgrpbox.Controls.Add(this.label16);
            this.othervaluesgrpbox.Controls.Add(this.grpeventenb);
            this.othervaluesgrpbox.Controls.Add(this.label17);
            this.othervaluesgrpbox.Controls.Add(this.textTimeDelay);
            this.othervaluesgrpbox.Controls.Add(this.comboNotifyclass);
            this.othervaluesgrpbox.Location = new System.Drawing.Point(8, 284);
            this.othervaluesgrpbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.othervaluesgrpbox.Name = "othervaluesgrpbox";
            this.othervaluesgrpbox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.othervaluesgrpbox.Size = new System.Drawing.Size(516, 471);
            this.othervaluesgrpbox.TabIndex = 42;
            this.othervaluesgrpbox.TabStop = false;
            // 
            // cmb_BinaryValue
            // 
            this.cmb_BinaryValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_BinaryValue.FormattingEnabled = true;
            this.cmb_BinaryValue.Location = new System.Drawing.Point(172, 281);
            this.cmb_BinaryValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_BinaryValue.Name = "cmb_BinaryValue";
            this.cmb_BinaryValue.Size = new System.Drawing.Size(313, 24);
            this.cmb_BinaryValue.TabIndex = 66;
            // 
            // lbl_BinaryValue
            // 
            this.lbl_BinaryValue.AutoSize = true;
            this.lbl_BinaryValue.Location = new System.Drawing.Point(5, 289);
            this.lbl_BinaryValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_BinaryValue.Name = "lbl_BinaryValue";
            this.lbl_BinaryValue.Size = new System.Drawing.Size(117, 16);
            this.lbl_BinaryValue.TabIndex = 67;
            this.lbl_BinaryValue.Text = "Inhibit binary value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(353, 217);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 65;
            this.label4.Text = "(0 to 4294967295)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 184);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 64;
            this.label3.Text = "(0 to 4294967295)";
            // 
            // lblTimeDelayNormalUnit
            // 
            this.lblTimeDelayNormalUnit.AutoSize = true;
            this.lblTimeDelayNormalUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeDelayNormalUnit.Location = new System.Drawing.Point(467, 214);
            this.lblTimeDelayNormalUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimeDelayNormalUnit.Name = "lblTimeDelayNormalUnit";
            this.lblTimeDelayNormalUnit.Size = new System.Drawing.Size(18, 20);
            this.lblTimeDelayNormalUnit.TabIndex = 63;
            this.lblTimeDelayNormalUnit.Text = "s";
            // 
            // lblTimeDelayUnit
            // 
            this.lblTimeDelayUnit.AutoSize = true;
            this.lblTimeDelayUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeDelayUnit.Location = new System.Drawing.Point(467, 181);
            this.lblTimeDelayUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimeDelayUnit.Name = "lblTimeDelayUnit";
            this.lblTimeDelayUnit.Size = new System.Drawing.Size(18, 20);
            this.lblTimeDelayUnit.TabIndex = 62;
            this.lblTimeDelayUnit.Text = "s";
            // 
            // comboboxFeedbackValue
            // 
            this.comboboxFeedbackValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxFeedbackValue.FormattingEnabled = true;
            this.comboboxFeedbackValue.Location = new System.Drawing.Point(173, 112);
            this.comboboxFeedbackValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboboxFeedbackValue.Name = "comboboxFeedbackValue";
            this.comboboxFeedbackValue.Size = new System.Drawing.Size(313, 24);
            this.comboboxFeedbackValue.TabIndex = 61;
            this.comboboxFeedbackValue.Validating += new System.ComponentModel.CancelEventHandler(this.comboboxFeedbackValue_Validating);
            this.comboboxFeedbackValue.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // txtActive
            // 
            this.txtActive.Location = new System.Drawing.Point(172, 73);
            this.txtActive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtActive.Name = "txtActive";
            this.txtActive.Size = new System.Drawing.Size(313, 22);
            this.txtActive.TabIndex = 60;
            this.txtActive.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtActive.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // txtInactive
            // 
            this.txtInactive.Location = new System.Drawing.Point(172, 38);
            this.txtInactive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtInactive.Name = "txtInactive";
            this.txtInactive.Size = new System.Drawing.Size(313, 22);
            this.txtInactive.TabIndex = 59;
            this.txtInactive.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtInactive.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Location = new System.Drawing.Point(1, 78);
            this.lblActive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(73, 16);
            this.lblActive.TabIndex = 58;
            this.lblActive.Text = "Active Text";
            // 
            // lblInactive
            // 
            this.lblInactive.AutoSize = true;
            this.lblInactive.Location = new System.Drawing.Point(0, 46);
            this.lblInactive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInactive.Name = "lblInactive";
            this.lblInactive.Size = new System.Drawing.Size(76, 16);
            this.lblInactive.TabIndex = 57;
            this.lblInactive.Text = "Inactive text";
            // 
            // comboPolarity
            // 
            this.comboPolarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPolarity.FormattingEnabled = true;
            this.comboPolarity.Items.AddRange(new object[] {
            "Normal",
            "Reverse "});
            this.comboPolarity.Location = new System.Drawing.Point(172, 2);
            this.comboPolarity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboPolarity.Name = "comboPolarity";
            this.comboPolarity.Size = new System.Drawing.Size(313, 24);
            this.comboPolarity.TabIndex = 21;
            this.comboPolarity.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checkEnable
            // 
            this.checkEnable.AutoSize = true;
            this.checkEnable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkEnable.Checked = true;
            this.checkEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEnable.Location = new System.Drawing.Point(0, 152);
            this.checkEnable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkEnable.Name = "checkEnable";
            this.checkEnable.Size = new System.Drawing.Size(169, 20);
            this.checkEnable.TabIndex = 22;
            this.checkEnable.Text = "Event Detection Enable";
            this.checkEnable.UseVisualStyleBackColor = true;
            this.checkEnable.CheckedChanged += new System.EventHandler(this.checkEnable_CheckedChanged);
            this.checkEnable.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelfeedbackval
            // 
            this.labelfeedbackval.AutoSize = true;
            this.labelfeedbackval.Location = new System.Drawing.Point(2, 120);
            this.labelfeedbackval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelfeedbackval.Name = "labelfeedbackval";
            this.labelfeedbackval.Size = new System.Drawing.Size(107, 16);
            this.labelfeedbackval.TabIndex = 56;
            this.labelfeedbackval.Text = "Feedback Value";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 12);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 16);
            this.label12.TabIndex = 42;
            this.label12.Text = "Polarity";
            // 
            // textTimeDelayNormal
            // 
            this.textTimeDelayNormal.Location = new System.Drawing.Point(172, 214);
            this.textTimeDelayNormal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textTimeDelayNormal.Name = "textTimeDelayNormal";
            this.textTimeDelayNormal.Size = new System.Drawing.Size(180, 22);
            this.textTimeDelayNormal.TabIndex = 24;
            this.textTimeDelayNormal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTimeDelay_KeyPress);
            this.textTimeDelayNormal.Leave += new System.EventHandler(this.textTimeDelayNormal_Leave);
            this.textTimeDelayNormal.Validating += new System.ComponentModel.CancelEventHandler(this.textTimeDelayNormal_Validating);
            this.textTimeDelayNormal.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0, 184);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 16);
            this.label13.TabIndex = 43;
            this.label13.Text = "Time Delay";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 217);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 55;
            this.label1.Text = "Time Delay Normal";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1, 252);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(110, 16);
            this.label15.TabIndex = 44;
            this.label15.Text = "Notification Class";
            // 
            // grpnotifytype
            // 
            this.grpnotifytype.Controls.Add(this.checkEvent);
            this.grpnotifytype.Controls.Add(this.checkAlarm);
            this.grpnotifytype.Location = new System.Drawing.Point(172, 393);
            this.grpnotifytype.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpnotifytype.Name = "grpnotifytype";
            this.grpnotifytype.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpnotifytype.Size = new System.Drawing.Size(333, 64);
            this.grpnotifytype.TabIndex = 54;
            this.grpnotifytype.TabStop = false;
            // 
            // checkEvent
            // 
            this.checkEvent.AutoSize = true;
            this.checkEvent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkEvent.Checked = true;
            this.checkEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEvent.Location = new System.Drawing.Point(144, 23);
            this.checkEvent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkEvent.Name = "checkEvent";
            this.checkEvent.Size = new System.Drawing.Size(63, 20);
            this.checkEvent.TabIndex = 32;
            this.checkEvent.Text = "Event";
            this.checkEvent.UseVisualStyleBackColor = true;
            this.checkEvent.CheckedChanged += new System.EventHandler(this.checkEvent_CheckedChanged);
            this.checkEvent.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checkAlarm
            // 
            this.checkAlarm.AutoSize = true;
            this.checkAlarm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkAlarm.Location = new System.Drawing.Point(16, 23);
            this.checkAlarm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkAlarm.Name = "checkAlarm";
            this.checkAlarm.Size = new System.Drawing.Size(64, 20);
            this.checkAlarm.TabIndex = 31;
            this.checkAlarm.Text = "Alarm";
            this.checkAlarm.UseVisualStyleBackColor = true;
            this.checkAlarm.CheckedChanged += new System.EventHandler(this.checkAlarm_CheckedChanged);
            this.checkAlarm.Validating += new System.ComponentModel.CancelEventHandler(this.checkAlarm_Validating);
            this.checkAlarm.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(0, 350);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 16);
            this.label16.TabIndex = 45;
            this.label16.Text = "Event Enable";
            // 
            // grpeventenb
            // 
            this.grpeventenb.Controls.Add(this.checkToNormal);
            this.grpeventenb.Controls.Add(this.checkToFault);
            this.grpeventenb.Controls.Add(this.checktooffNormal);
            this.grpeventenb.Location = new System.Drawing.Point(172, 318);
            this.grpeventenb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpeventenb.Name = "grpeventenb";
            this.grpeventenb.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpeventenb.Size = new System.Drawing.Size(340, 62);
            this.grpeventenb.TabIndex = 53;
            this.grpeventenb.TabStop = false;
            // 
            // checkToNormal
            // 
            this.checkToNormal.AutoSize = true;
            this.checkToNormal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkToNormal.Location = new System.Drawing.Point(224, 23);
            this.checkToNormal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkToNormal.Name = "checkToNormal";
            this.checkToNormal.Size = new System.Drawing.Size(93, 20);
            this.checkToNormal.TabIndex = 30;
            this.checkToNormal.Text = "To normal:";
            this.checkToNormal.UseVisualStyleBackColor = true;
            this.checkToNormal.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checkToFault
            // 
            this.checkToFault.AutoSize = true;
            this.checkToFault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkToFault.Location = new System.Drawing.Point(129, 23);
            this.checkToFault.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkToFault.Name = "checkToFault";
            this.checkToFault.Size = new System.Drawing.Size(76, 20);
            this.checkToFault.TabIndex = 29;
            this.checkToFault.Text = "To fault:";
            this.checkToFault.UseVisualStyleBackColor = true;
            this.checkToFault.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checktooffNormal
            // 
            this.checktooffNormal.AutoSize = true;
            this.checktooffNormal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checktooffNormal.Checked = true;
            this.checktooffNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checktooffNormal.Location = new System.Drawing.Point(8, 23);
            this.checktooffNormal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checktooffNormal.Name = "checktooffNormal";
            this.checktooffNormal.Size = new System.Drawing.Size(107, 20);
            this.checktooffNormal.TabIndex = 28;
            this.checktooffNormal.Text = "To offnormal:";
            this.checktooffNormal.UseVisualStyleBackColor = true;
            this.checktooffNormal.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(5, 418);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 16);
            this.label17.TabIndex = 46;
            this.label17.Text = "Notify Type";
            // 
            // textTimeDelay
            // 
            this.textTimeDelay.Location = new System.Drawing.Point(172, 180);
            this.textTimeDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textTimeDelay.Name = "textTimeDelay";
            this.textTimeDelay.Size = new System.Drawing.Size(180, 22);
            this.textTimeDelay.TabIndex = 23;
            this.textTimeDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTimeDelay_KeyPress);
            this.textTimeDelay.Leave += new System.EventHandler(this.textTimeDelay_Leave);
            this.textTimeDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textTimeDelay_Validating);
            this.textTimeDelay.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // comboNotifyclass
            // 
            this.comboNotifyclass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNotifyclass.FormattingEnabled = true;
            this.comboNotifyclass.Location = new System.Drawing.Point(172, 244);
            this.comboNotifyclass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboNotifyclass.Name = "comboNotifyclass";
            this.comboNotifyclass.Size = new System.Drawing.Size(313, 24);
            this.comboNotifyclass.TabIndex = 26;
            this.comboNotifyclass.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 









            // FormDigitalBacNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 854);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormDigitalBacNet";
            this.ShowIcon = false;
            this.Text = "FormDigitalBacNet";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.othervaluesgrpbox.ResumeLayout(false);
            this.othervaluesgrpbox.PerformLayout();
            this.grpnotifytype.ResumeLayout(false);
            this.grpnotifytype.PerformLayout();
            this.grpeventenb.ResumeLayout(false);
            this.grpeventenb.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox1;
        private BorderlessGroupBox othervaluesgrpbox;
        private System.Windows.Forms.ComboBox comboPolarity;
        private System.Windows.Forms.CheckBox checkEnable;
        private System.Windows.Forms.Label labelfeedbackval;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textTimeDelayNormal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox grpnotifytype;
        private System.Windows.Forms.CheckBox checkEvent;
        private System.Windows.Forms.CheckBox checkAlarm;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox grpeventenb;
        private System.Windows.Forms.CheckBox checkToNormal;
        private System.Windows.Forms.CheckBox checkToFault;
        private System.Windows.Forms.CheckBox checktooffNormal;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textTimeDelay;
        private System.Windows.Forms.ComboBox comboNotifyclass;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labelDeviceType;
        private System.Windows.Forms.Label labelObjType;
        private System.Windows.Forms.Label labelInstanceno;
        private System.Windows.Forms.Label labelObjIdentifier;
        private System.Windows.Forms.TextBox textReDefault;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.TextBox textObjectName;
        private System.Windows.Forms.Label labelReqdefault;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.Label lblInactive;
        private System.Windows.Forms.ComboBox comboboxFeedbackValue;
        private System.Windows.Forms.TextBox txtActive;
        private System.Windows.Forms.TextBox txtInactive;
        private System.Windows.Forms.Label lblInitialValue;
        private System.Windows.Forms.ComboBox comboBoxInitialValue;
        private System.Windows.Forms.Label lblTimeDelayNormalUnit;
        private System.Windows.Forms.Label lblTimeDelayUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_BinaryValue;
        private System.Windows.Forms.Label lbl_BinaryValue;
        private System.Windows.Forms.ComboBox cmbRelinquishDefault;
    }
}