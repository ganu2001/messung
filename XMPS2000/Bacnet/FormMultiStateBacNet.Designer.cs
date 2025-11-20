using System;

namespace XMPS2000.Bacnet
{
    partial class FormMultiStateBacNet
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
            this.labelNoStates = new System.Windows.Forms.Label();
            this.textObjectName = new System.Windows.Forms.TextBox();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.textNoOfStates = new System.Windows.Forms.TextBox();
            this.labelObjIdentifier = new System.Windows.Forms.Label();
            this.labelInstanceno = new System.Windows.Forms.Label();
            this.labelObjType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnVieworUpdateStates = new System.Windows.Forms.Button();
            this.othervaluesgrpbox = new XMPS2000.BorderlessGroupBox();
            this.cmb_BinaryValue = new System.Windows.Forms.ComboBox();
            this.lbl_BinaryValue = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbInitialValue = new System.Windows.Forms.ComboBox();
            this.lblInitialValue = new System.Windows.Forms.Label();
            this.textAlarmValue = new System.Windows.Forms.TextBox();
            this.checkEnable = new System.Windows.Forms.CheckBox();
            this.labelAlarmval = new System.Windows.Forms.Label();
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
            this.cmbReDefault = new System.Windows.Forms.ComboBox();
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
            this.label7.Location = new System.Drawing.Point(8, 150);
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
            this.label9.Location = new System.Drawing.Point(8, 187);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "Description";
            // 
            // labelNoStates
            // 
            this.labelNoStates.AutoSize = true;
            this.labelNoStates.Location = new System.Drawing.Point(13, 264);
            this.labelNoStates.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNoStates.Name = "labelNoStates";
            this.labelNoStates.Size = new System.Drawing.Size(110, 16);
            this.labelNoStates.TabIndex = 10;
            this.labelNoStates.Text = "Number of States";
            // 
            // textObjectName
            // 
            this.textObjectName.Location = new System.Drawing.Point(180, 142);
            this.textObjectName.Margin = new System.Windows.Forms.Padding(4);
            this.textObjectName.MaxLength = 25;
            this.textObjectName.Name = "textObjectName";
            this.textObjectName.Size = new System.Drawing.Size(313, 22);
            this.textObjectName.TabIndex = 17;
            this.textObjectName.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textObjectName.TextChanged += new System.EventHandler(this.textObjectName_TextChanged);
            this.textObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textObjectName_Validating);
            this.textObjectName.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(180, 178);
            this.textDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textDescription.MaxLength = 25;
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(313, 22);
            this.textDescription.TabIndex = 18;
            this.textDescription.Text = "Digital Input";
            this.textDescription.TextChanged += new System.EventHandler(this.textDescription_TextChanged);
            this.textDescription.Validating += new System.ComponentModel.CancelEventHandler(this.textDescription_Validating);
            this.textDescription.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // textNoOfStates
            // 
            this.textNoOfStates.Location = new System.Drawing.Point(182, 261);
            this.textNoOfStates.Margin = new System.Windows.Forms.Padding(4);
            this.textNoOfStates.Name = "textNoOfStates";
            this.textNoOfStates.Size = new System.Drawing.Size(85, 22);
            this.textNoOfStates.TabIndex = 20;
            this.textNoOfStates.Text = "0";
            this.textNoOfStates.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textNoOfStates_KeyPress);
            this.textNoOfStates.Leave += new System.EventHandler(this.textNoOfStates_Leave);
            this.textNoOfStates.Validating += new System.ComponentModel.CancelEventHandler(this.textNoOfStates_Validating);
            this.textNoOfStates.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
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
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(249, 703);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 31);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(133, 703);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 31);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbReDefault);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btnVieworUpdateStates);
            this.groupBox1.Controls.Add(this.othervaluesgrpbox);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.labelObjType);
            this.groupBox1.Controls.Add(this.labelInstanceno);
            this.groupBox1.Controls.Add(this.labelObjIdentifier);
            this.groupBox1.Controls.Add(this.textNoOfStates);
            this.groupBox1.Controls.Add(this.textDescription);
            this.groupBox1.Controls.Add(this.textObjectName);
            this.groupBox1.Controls.Add(this.labelNoStates);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(16, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(528, 744);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 223);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 16);
            this.label10.TabIndex = 59;
            this.label10.Text = "Relinquish Default";
            // 
            // btnVieworUpdateStates
            // 
            this.btnVieworUpdateStates.Location = new System.Drawing.Point(282, 259);
            this.btnVieworUpdateStates.Margin = new System.Windows.Forms.Padding(4);
            this.btnVieworUpdateStates.Name = "btnVieworUpdateStates";
            this.btnVieworUpdateStates.Size = new System.Drawing.Size(215, 30);
            this.btnVieworUpdateStates.TabIndex = 58;
            this.btnVieworUpdateStates.Text = "View/Update States";
            this.btnVieworUpdateStates.UseVisualStyleBackColor = true;
            this.btnVieworUpdateStates.Click += new System.EventHandler(this.btnVieworUpdateStates_Click);
            // 
            // othervaluesgrpbox
            // 
            this.othervaluesgrpbox.Controls.Add(this.cmb_BinaryValue);
            this.othervaluesgrpbox.Controls.Add(this.lbl_BinaryValue);
            this.othervaluesgrpbox.Controls.Add(this.label11);
            this.othervaluesgrpbox.Controls.Add(this.label4);
            this.othervaluesgrpbox.Controls.Add(this.label3);
            this.othervaluesgrpbox.Controls.Add(this.label2);
            this.othervaluesgrpbox.Controls.Add(this.cmbInitialValue);
            this.othervaluesgrpbox.Controls.Add(this.lblInitialValue);
            this.othervaluesgrpbox.Controls.Add(this.textAlarmValue);
            this.othervaluesgrpbox.Controls.Add(this.checkEnable);
            this.othervaluesgrpbox.Controls.Add(this.labelAlarmval);
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
            this.othervaluesgrpbox.Location = new System.Drawing.Point(11, 297);
            this.othervaluesgrpbox.Margin = new System.Windows.Forms.Padding(4);
            this.othervaluesgrpbox.Name = "othervaluesgrpbox";
            this.othervaluesgrpbox.Padding = new System.Windows.Forms.Padding(4);
            this.othervaluesgrpbox.Size = new System.Drawing.Size(516, 398);
            this.othervaluesgrpbox.TabIndex = 42;
            this.othervaluesgrpbox.TabStop = false;
            // 
            // cmb_BinaryValue
            // 
            this.cmb_BinaryValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_BinaryValue.FormattingEnabled = true;
            this.cmb_BinaryValue.Location = new System.Drawing.Point(167, 182);
            this.cmb_BinaryValue.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_BinaryValue.Name = "cmb_BinaryValue";
            this.cmb_BinaryValue.Size = new System.Drawing.Size(320, 24);
            this.cmb_BinaryValue.TabIndex = 63;
            // 
            // lbl_BinaryValue
            // 
            this.lbl_BinaryValue.AutoSize = true;
            this.lbl_BinaryValue.Location = new System.Drawing.Point(0, 190);
            this.lbl_BinaryValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_BinaryValue.Name = "lbl_BinaryValue";
            this.lbl_BinaryValue.Size = new System.Drawing.Size(117, 16);
            this.lbl_BinaryValue.TabIndex = 64;
            this.lbl_BinaryValue.Text = "Inhibit binary value";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(361, 79);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 16);
            this.label11.TabIndex = 62;
            this.label11.Text = "(0 to 4294967295)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(361, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 61;
            this.label4.Text = "(0 to 4294967295)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(488, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 20);
            this.label3.TabIndex = 60;
            this.label3.Text = "s";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(488, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 20);
            this.label2.TabIndex = 59;
            this.label2.Text = "s";
            // 
            // cmbInitialValue
            // 
            this.cmbInitialValue.FormattingEnabled = true;
            this.cmbInitialValue.Location = new System.Drawing.Point(174, 366);
            this.cmbInitialValue.Margin = new System.Windows.Forms.Padding(4);
            this.cmbInitialValue.Name = "cmbInitialValue";
            this.cmbInitialValue.Size = new System.Drawing.Size(313, 24);
            this.cmbInitialValue.TabIndex = 58;
            this.cmbInitialValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInitialValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbInitialValue_KeyPress);
            this.cmbInitialValue.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // lblInitialValue
            // 
            this.lblInitialValue.AutoSize = true;
            this.lblInitialValue.Location = new System.Drawing.Point(1, 370);
            this.lblInitialValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInitialValue.Name = "lblInitialValue";
            this.lblInitialValue.Size = new System.Drawing.Size(72, 16);
            this.lblInitialValue.TabIndex = 57;
            this.lblInitialValue.Text = "Inital Value";
            // 
            // textAlarmValue
            // 
            this.textAlarmValue.Location = new System.Drawing.Point(167, 111);
            this.textAlarmValue.Margin = new System.Windows.Forms.Padding(4);
            this.textAlarmValue.Name = "textAlarmValue";
            this.textAlarmValue.Size = new System.Drawing.Size(319, 22);
            this.textAlarmValue.TabIndex = 25;
            this.textAlarmValue.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checkEnable
            // 
            this.checkEnable.AutoSize = true;
            this.checkEnable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkEnable.Checked = true;
            this.checkEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEnable.Location = new System.Drawing.Point(0, 11);
            this.checkEnable.Margin = new System.Windows.Forms.Padding(4);
            this.checkEnable.Name = "checkEnable";
            this.checkEnable.Size = new System.Drawing.Size(169, 20);
            this.checkEnable.TabIndex = 22;
            this.checkEnable.Text = "Event Detection Enable";
            this.checkEnable.UseVisualStyleBackColor = true;
            this.checkEnable.CheckedChanged += new System.EventHandler(this.checkEnable_CheckedChanged);
            this.checkEnable.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            //
            // labelAlarmval
            // 
            this.labelAlarmval.AutoSize = true;
            this.labelAlarmval.Location = new System.Drawing.Point(-1, 119);
            this.labelAlarmval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAlarmval.Name = "labelAlarmval";
            this.labelAlarmval.Size = new System.Drawing.Size(87, 16);
            this.labelAlarmval.TabIndex = 56;
            this.labelAlarmval.Text = "Alarm Values";
            // 
            // textTimeDelayNormal
            // 
            this.textTimeDelayNormal.Location = new System.Drawing.Point(167, 75);
            this.textTimeDelayNormal.Margin = new System.Windows.Forms.Padding(4);
            this.textTimeDelayNormal.Name = "textTimeDelayNormal";
            this.textTimeDelayNormal.Size = new System.Drawing.Size(179, 22);
            this.textTimeDelayNormal.TabIndex = 24;
            this.textTimeDelayNormal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTimeDelay_KeyPress);
            this.textTimeDelayNormal.Leave += new System.EventHandler(this.textTimeDelayNormal_Leave);
            this.textTimeDelayNormal.Validating += new System.ComponentModel.CancelEventHandler(this.textTimeDelayNormal_Validating);
            this.textTimeDelayNormal.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0, 48);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 16);
            this.label13.TabIndex = 43;
            this.label13.Text = "Time Delay";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 55;
            this.label1.Text = "Time Delay Normal";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(-1, 158);
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
            this.grpnotifytype.Location = new System.Drawing.Point(169, 283);
            this.grpnotifytype.Margin = new System.Windows.Forms.Padding(4);
            this.grpnotifytype.Name = "grpnotifytype";
            this.grpnotifytype.Padding = new System.Windows.Forms.Padding(4);
            this.grpnotifytype.Size = new System.Drawing.Size(340, 65);
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
            this.checkEvent.Margin = new System.Windows.Forms.Padding(4);
            this.checkEvent.Name = "checkEvent";
            this.checkEvent.Size = new System.Drawing.Size(63, 20);
            this.checkEvent.TabIndex = 32;
            this.checkEvent.Text = "Event";
            this.checkEvent.UseVisualStyleBackColor = true;
            this.checkEvent.CheckedChanged += new System.EventHandler(this.checkEvent_CheckedChanged);
            this.checkEvent.Validating += new System.ComponentModel.CancelEventHandler(this.checkEvent_Validating);
            this.checkEvent.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checkAlarm
            // 
            this.checkAlarm.AutoSize = true;
            this.checkAlarm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkAlarm.Location = new System.Drawing.Point(16, 23);
            this.checkAlarm.Margin = new System.Windows.Forms.Padding(4);
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
            this.label16.Location = new System.Drawing.Point(2, 243);
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
            this.grpeventenb.Location = new System.Drawing.Point(169, 214);
            this.grpeventenb.Margin = new System.Windows.Forms.Padding(4);
            this.grpeventenb.Name = "grpeventenb";
            this.grpeventenb.Padding = new System.Windows.Forms.Padding(4);
            this.grpeventenb.Size = new System.Drawing.Size(340, 62);
            this.grpeventenb.TabIndex = 53;
            this.grpeventenb.TabStop = false;
            // 
            // checkToNormal
            // 
            this.checkToNormal.AutoSize = true;
            this.checkToNormal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkToNormal.Location = new System.Drawing.Point(224, 23);
            this.checkToNormal.Margin = new System.Windows.Forms.Padding(4);
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
            this.checkToFault.Margin = new System.Windows.Forms.Padding(4);
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
            this.checktooffNormal.Location = new System.Drawing.Point(11, 25);
            this.checktooffNormal.Margin = new System.Windows.Forms.Padding(4);
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
            this.label17.Location = new System.Drawing.Point(6, 305);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 16);
            this.label17.TabIndex = 46;
            this.label17.Text = "Notify Type";
            // 
            // textTimeDelay
            // 
            this.textTimeDelay.Location = new System.Drawing.Point(167, 39);
            this.textTimeDelay.Margin = new System.Windows.Forms.Padding(4);
            this.textTimeDelay.Name = "textTimeDelay";
            this.textTimeDelay.Size = new System.Drawing.Size(179, 22);
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
            this.comboNotifyclass.Location = new System.Drawing.Point(167, 146);
            this.comboNotifyclass.Margin = new System.Windows.Forms.Padding(4);
            this.comboNotifyclass.Name = "comboNotifyclass";
            this.comboNotifyclass.Size = new System.Drawing.Size(319, 24);
            this.comboNotifyclass.TabIndex = 26;
            this.comboNotifyclass.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // cmbReDefault
            // 
            this.cmbReDefault.FormattingEnabled = true;
            this.cmbReDefault.Location = new System.Drawing.Point(180, 220);
            this.cmbReDefault.Margin = new System.Windows.Forms.Padding(4);
            this.cmbReDefault.Name = "cmbReDefault";
            this.cmbReDefault.Size = new System.Drawing.Size(313, 24);
            this.cmbReDefault.TabIndex = 65;
            this.cmbReDefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReDefault.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbReDefault_KeyPress);
            // 
            // FormMultiStateBacNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 761);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMultiStateBacNet";
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
        private System.Windows.Forms.TextBox textAlarmValue;
        private System.Windows.Forms.CheckBox checkEnable;
        private System.Windows.Forms.Label labelAlarmval;
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
        private System.Windows.Forms.Label labelObjType;
        private System.Windows.Forms.Label labelInstanceno;
        private System.Windows.Forms.Label labelObjIdentifier;
        private System.Windows.Forms.TextBox textNoOfStates;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.TextBox textObjectName;
        private System.Windows.Forms.Label labelNoStates;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnVieworUpdateStates;
        private System.Windows.Forms.Label lblInitialValue;
        private System.Windows.Forms.ComboBox cmbInitialValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_BinaryValue;
        private System.Windows.Forms.Label lbl_BinaryValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbReDefault;
    }
}