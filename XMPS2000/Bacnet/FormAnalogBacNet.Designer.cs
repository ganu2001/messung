using System;
using System.Windows.Forms;

namespace XMPS2000.Bacnet
{
    partial class FormAnalogBacNet
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
            this.textInitialValue = new System.Windows.Forms.TextBox();
            this.lblInitialValue = new System.Windows.Forms.Label();
            this.comboBoxUnits = new System.Windows.Forms.ComboBox();
            this.textBoxCovIncr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxMaxPresVal = new System.Windows.Forms.TextBox();
            this.textBoxMinPresVal = new System.Windows.Forms.TextBox();
            this.lblOutOfRangeManagement = new System.Windows.Forms.Label();
            this.checkBoxOutOfRange = new System.Windows.Forms.CheckBox();
            this.labelMinValue = new System.Windows.Forms.Label();
            this.textBoxMinValue = new System.Windows.Forms.TextBox();
            this.labelMaxValue = new System.Windows.Forms.Label();
            this.textBoxMaxValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelDeviceType = new System.Windows.Forms.Label();
            this.labelObjType = new System.Windows.Forms.Label();
            this.labelInstanceno = new System.Windows.Forms.Label();
            this.labelObjIdentifier = new System.Windows.Forms.Label();
            this.textReDefault = new System.Windows.Forms.TextBox();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.textObjectName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkLoLimit = new System.Windows.Forms.CheckBox();
            this.checkHiLimit = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textDeadband = new System.Windows.Forms.TextBox();
            this.grpnotifytype = new System.Windows.Forms.GroupBox();
            this.checkEvent = new System.Windows.Forms.CheckBox();
            this.checkAlarm = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxLowLimit = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxHighLimit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textTimeDelayNormal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_BinaryValue = new System.Windows.Forms.ComboBox();
            this.comboNotifyclass = new System.Windows.Forms.ComboBox();
            this.lbl_BinaryValue = new System.Windows.Forms.Label();
            this.textTimeDelay = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.grpeventenb = new System.Windows.Forms.GroupBox();
            this.checkToNormal = new System.Windows.Forms.CheckBox();
            this.checkToFault = new System.Windows.Forms.CheckBox();
            this.checktooffNormal = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.checkEnable = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpnotifytype.SuspendLayout();
            this.grpeventenb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textInitialValue);
            this.groupBox1.Controls.Add(this.lblInitialValue);
            this.groupBox1.Controls.Add(this.comboBoxUnits);
            this.groupBox1.Controls.Add(this.textBoxCovIncr);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textReDefault);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBoxMaxPresVal);
            this.groupBox1.Controls.Add(this.textBoxMinPresVal);
            this.groupBox1.Controls.Add(this.lblOutOfRangeManagement);
            this.groupBox1.Controls.Add(this.textBoxMaxValue);
            this.groupBox1.Controls.Add(this.labelMaxValue);
            this.groupBox1.Controls.Add(this.textBoxMinValue);
            this.groupBox1.Controls.Add(this.labelMinValue);
            this.groupBox1.Controls.Add(this.checkBoxOutOfRange);
            this.groupBox1.Controls.Add(this.textInitialValue);
            this.groupBox1.Controls.Add(this.lblInitialValue);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelDeviceType);
            this.groupBox1.Controls.Add(this.labelObjType);
            this.groupBox1.Controls.Add(this.labelInstanceno);
            this.groupBox1.Controls.Add(this.labelObjIdentifier);
            this.groupBox1.Controls.Add(this.textReDefault);
            this.groupBox1.Controls.Add(this.textDescription);
            this.groupBox1.Controls.Add(this.textObjectName);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(16, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(479, 610);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // textInitialValue
            // 
            this.textInitialValue.Location = new System.Drawing.Point(177, 528);
            this.textInitialValue.Margin = new System.Windows.Forms.Padding(4);
            this.textInitialValue.Name = "textInitialValue";
            this.textInitialValue.Size = new System.Drawing.Size(263, 22);
            this.textInitialValue.TabIndex = 55;
            this.textInitialValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textInitialValue_KeyPress);
            this.textInitialValue.Validating += new System.ComponentModel.CancelEventHandler(this.textInitialValue_Validating);
            this.textInitialValue.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // lblInitialValue
            // 
            this.lblInitialValue.AutoSize = true;
            this.lblInitialValue.Location = new System.Drawing.Point(8, 528);
            this.lblInitialValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInitialValue.Name = "lblInitialValue";
            this.lblInitialValue.Size = new System.Drawing.Size(75, 16);
            this.lblInitialValue.TabIndex = 54;
            this.lblInitialValue.Text = "Initial Value";
            // 
            // comboBoxUnits
            // 
            this.comboBoxUnits.FormattingEnabled = true;
            this.comboBoxUnits.Location = new System.Drawing.Point(177, 289);
            this.comboBoxUnits.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxUnits.Name = "comboBoxUnits";
            this.comboBoxUnits.Size = new System.Drawing.Size(263, 24);
            this.comboBoxUnits.TabIndex = 53;
            this.comboBoxUnits.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxUnits_DrawItem);
            this.comboBoxUnits.SelectedIndexChanged += new System.EventHandler(this.comboBoxUnits_SelectedIndexChanged);
            this.comboBoxUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxUnits_KeyPress);
            this.comboBoxUnits.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            this.comboBoxUnits.DropDown += new System.EventHandler(this.comboBoxUnits_DropDown);
            this.comboBoxUnits.DropDownClosed += new System.EventHandler(this.comboBoxUnits_DropDownClosed);
            // 
            // textBoxCovIncr
            // 
            this.textBoxCovIncr.Location = new System.Drawing.Point(177, 496);
            this.textBoxCovIncr.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCovIncr.MaxLength = 20;
            this.textBoxCovIncr.Name = "textBoxCovIncr";
            this.textBoxCovIncr.Size = new System.Drawing.Size(263, 22);
            this.textBoxCovIncr.TabIndex = 23;
            this.textBoxCovIncr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHighLimit_KeyPress);
            this.textBoxCovIncr.Leave += new System.EventHandler(this.textBoxCovIncr_Leave);
            this.textBoxCovIncr.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCovIncr_Validating);
            this.textBoxCovIncr.TextChanged += new EventHandler(BacNetValidator.ControlChanged);

            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 500);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 52;
            this.label4.Text = "COV Increment";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 330);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(97, 16);
            this.label18.TabIndex = 50;
            this.label18.Text = "Min Pres Value";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 372);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 16);
            this.label12.TabIndex = 49;
            this.label12.Text = "Max Pres Value";
            // 
            // textBoxMaxPresVal
            // 
            this.textBoxMaxPresVal.Location = new System.Drawing.Point(177, 363);
            this.textBoxMaxPresVal.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMaxPresVal.MaxLength = 20;
            this.textBoxMaxPresVal.Name = "textBoxMaxPresVal";
            this.textBoxMaxPresVal.Size = new System.Drawing.Size(263, 22);
            this.textBoxMaxPresVal.TabIndex = 21;
            this.textBoxMaxPresVal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHighLimit_KeyPress);
            this.textBoxMaxPresVal.Leave += new System.EventHandler(this.textBoxMaxPresVal_Leave);
            this.textBoxMaxPresVal.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMaxPresVal_Validating);
            this.textBoxMaxPresVal.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // textBoxMinPresVal
            // 
            this.textBoxMinPresVal.Location = new System.Drawing.Point(177, 326);
            this.textBoxMinPresVal.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMinPresVal.MaxLength = 20;
            this.textBoxMinPresVal.Name = "textBoxMinPresVal";
            this.textBoxMinPresVal.Size = new System.Drawing.Size(263, 22);
            this.textBoxMinPresVal.TabIndex = 20;
            this.textBoxMinPresVal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHighLimit_KeyPress);
            this.textBoxMinPresVal.Leave += new System.EventHandler(this.textBoxMinPresVal_Leave);
            this.textBoxMinPresVal.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMinPresVal_Validating);
            this.textBoxMaxPresVal.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            this.textBoxMinPresVal.TextChanged += BacNetValidator.ControlChanged;
            this.textBoxMinPresVal.Validated += BacNetValidator.ControlChanged;
            //
            // lblOutOfRangeManagement
            //
            this.lblOutOfRangeManagement.AutoSize = true;
            this.lblOutOfRangeManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutOfRangeManagement.Location = new System.Drawing.Point(8, 390);
            this.lblOutOfRangeManagement.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutOfRangeManagement.Name = "lblOutOfRangeManagement";
            this.lblOutOfRangeManagement.Size = new System.Drawing.Size(213, 18);
            this.lblOutOfRangeManagement.TabIndex = 61;
            this.lblOutOfRangeManagement.Text = "Out of Range Management";
            // 
            // checkBoxOutOfRange
            // 
            this.checkBoxOutOfRange.AutoSize = true;
            this.checkBoxOutOfRange.Location = new System.Drawing.Point(8, 419);
            this.checkBoxOutOfRange.Name = "checkBoxOutOfRange";
            this.checkBoxOutOfRange.Size = new System.Drawing.Size(156, 20);
            this.checkBoxOutOfRange.TabIndex = 56;
            this.checkBoxOutOfRange.Text = "Check out of range";
            this.checkBoxOutOfRange.UseVisualStyleBackColor = true;
            this.checkBoxOutOfRange.CheckedChanged += new System.EventHandler(this.checkBoxOutOfRange_CheckedChanged);
            this.checkBoxOutOfRange.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelMinValue
            // 
            this.labelMinValue.AutoSize = true;
            this.labelMinValue.Location = new System.Drawing.Point(8, 447);
            this.labelMinValue.Name = "labelMinValue";
            this.labelMinValue.Size = new System.Drawing.Size(87, 16);
            this.labelMinValue.TabIndex = 57;
            this.labelMinValue.Text = "Minimum Value";
            // 
            // textBoxMinValue
            // 
            this.textBoxMinValue.Location = new System.Drawing.Point(177, 444);
            this.textBoxMinValue.Name = "textBoxMinValue";
            this.textBoxMinValue.Size = new System.Drawing.Size(263, 22);
            this.textBoxMinValue.TabIndex = 58;
            this.textBoxMinValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHighLimit_KeyPress);
            this.textBoxMinValue.Leave += new System.EventHandler(this.textBoxMinValue_Leave);
            this.textBoxMinValue.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMinValue_Validating);
            this.textBoxMinValue.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelMaxValue
            // 
            this.labelMaxValue.AutoSize = true;
            this.labelMaxValue.Location = new System.Drawing.Point(8, 475);
            this.labelMaxValue.Name = "labelMaxValue";
            this.labelMaxValue.Size = new System.Drawing.Size(91, 16);
            this.labelMaxValue.TabIndex = 59;
            this.labelMaxValue.Text = "Maximum Value";
            // 
            // textBoxMaxValue
            // 
            this.textBoxMaxValue.Location = new System.Drawing.Point(177, 472);
            this.textBoxMaxValue.Name = "textBoxMaxValue";
            this.textBoxMaxValue.Size = new System.Drawing.Size(263, 22);
            this.textBoxMaxValue.TabIndex = 60;
            this.textBoxMaxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHighLimit_KeyPress);
            this.textBoxMaxValue.Leave += new System.EventHandler(this.textBoxMaxValue_Leave);
            this.textBoxMaxValue.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMaxValue_Validating);
            this.textBoxMaxValue.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 289);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 16);
            this.label3.TabIndex = 44;
            this.label3.Text = "Units";
            // 
            // labelDeviceType
            // 
            this.labelDeviceType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDeviceType.Location = new System.Drawing.Point(177, 142);
            this.labelDeviceType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDeviceType.Name = "labelDeviceType";
            this.labelDeviceType.Size = new System.Drawing.Size(263, 24);
            this.labelDeviceType.TabIndex = 34;
            this.labelDeviceType.Text = "3000";
            // 
            // labelObjType
            // 
            this.labelObjType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjType.Location = new System.Drawing.Point(177, 105);
            this.labelObjType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjType.Name = "labelObjType";
            this.labelObjType.Size = new System.Drawing.Size(263, 24);
            this.labelObjType.TabIndex = 33;
            this.labelObjType.Text = "3";
            // 
            // labelInstanceno
            // 
            this.labelInstanceno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInstanceno.Location = new System.Drawing.Point(177, 68);
            this.labelInstanceno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInstanceno.Name = "labelInstanceno";
            this.labelInstanceno.Size = new System.Drawing.Size(263, 24);
            this.labelInstanceno.TabIndex = 32;
            this.labelInstanceno.Text = "000";
            // 
            // labelObjIdentifier
            // 
            this.labelObjIdentifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjIdentifier.Location = new System.Drawing.Point(177, 31);
            this.labelObjIdentifier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjIdentifier.Name = "labelObjIdentifier";
            this.labelObjIdentifier.Size = new System.Drawing.Size(263, 24);
            this.labelObjIdentifier.TabIndex = 31;
            this.labelObjIdentifier.Text = "000";
            // 
            // textReDefault
            // 
            this.textReDefault.Location = new System.Drawing.Point(177, 256);
            this.textReDefault.Margin = new System.Windows.Forms.Padding(4);
            this.textReDefault.Name = "textReDefault";
            this.textReDefault.Size = new System.Drawing.Size(263, 22);
            this.textReDefault.TabIndex = 22;
            this.textReDefault.Text = "0";
            this.textReDefault.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHighLimit_KeyPress);
            this.textReDefault.Leave += new System.EventHandler(this.textReDefault_Leave);
            this.textReDefault.Validating += new System.ComponentModel.CancelEventHandler(this.textReDefault_Validating);
            this.textReDefault.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(177, 215);
            this.textDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textDescription.MaxLength = 25;
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(263, 22);
            this.textDescription.TabIndex = 18;
            this.textDescription.Text = "Digital Input";
            this.textDescription.TextChanged += new System.EventHandler(this.textDescription_TextChanged);
            this.textDescription.Validating += new System.ComponentModel.CancelEventHandler(this.textDescription_Validating);
            this.textDescription.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // textObjectName
            // 
            this.textObjectName.Location = new System.Drawing.Point(177, 178);
            this.textObjectName.Margin = new System.Windows.Forms.Padding(4);
            this.textObjectName.MaxLength = 25;
            this.textObjectName.Name = "textObjectName";
            this.textObjectName.Size = new System.Drawing.Size(263, 22);
            this.textObjectName.TabIndex = 17;
            this.textObjectName.TextChanged += new System.EventHandler(this.textObjectName_TextChanged);
            this.textObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textObjectName_Validating);
            this.textObjectName.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 262);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 16);
            this.label11.TabIndex = 10;
            this.label11.Text = "Relinquish Default";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 150);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 9;
            this.label10.Text = "Device Type";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.textDeadband);
            this.groupBox2.Controls.Add(this.grpnotifytype);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.textBoxLowLimit);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.textBoxHighLimit);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textTimeDelayNormal);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmb_BinaryValue);
            this.groupBox2.Controls.Add(this.comboNotifyclass);
            this.groupBox2.Controls.Add(this.lbl_BinaryValue);
            this.groupBox2.Controls.Add(this.textTimeDelay);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.grpeventenb);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Location = new System.Drawing.Point(495, 37);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(499, 438);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(336, 51);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(109, 16);
            this.label24.TabIndex = 64;
            this.label24.Text = "(0 to 4294967295)";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(336, 19);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(109, 16);
            this.label23.TabIndex = 63;
            this.label23.Text = "(0 to 4294967295)";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(453, 48);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(18, 20);
            this.label22.TabIndex = 62;
            this.label22.Text = "s";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(453, 17);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 20);
            this.label14.TabIndex = 61;
            this.label14.Text = "s";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkLoLimit);
            this.groupBox3.Controls.Add(this.checkHiLimit);
            this.groupBox3.Location = new System.Drawing.Point(149, 266);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(343, 62);
            this.groupBox3.TabIndex = 60;
            this.groupBox3.TabStop = false;
            // 
            // checkLoLimit
            // 
            this.checkLoLimit.AutoSize = true;
            this.checkLoLimit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkLoLimit.Location = new System.Drawing.Point(129, 23);
            this.checkLoLimit.Margin = new System.Windows.Forms.Padding(4);
            this.checkLoLimit.Name = "checkLoLimit";
            this.checkLoLimit.Size = new System.Drawing.Size(83, 20);
            this.checkLoLimit.TabIndex = 32;
            this.checkLoLimit.Text = "Low Limit";
            this.checkLoLimit.UseVisualStyleBackColor = true;
            this.checkLoLimit.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checkHiLimit
            // 
            this.checkHiLimit.AutoSize = true;
            this.checkHiLimit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkHiLimit.Checked = true;
            this.checkHiLimit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkHiLimit.Location = new System.Drawing.Point(11, 25);
            this.checkHiLimit.Margin = new System.Windows.Forms.Padding(4);
            this.checkHiLimit.Name = "checkHiLimit";
            this.checkHiLimit.Size = new System.Drawing.Size(87, 20);
            this.checkHiLimit.TabIndex = 31;
            this.checkHiLimit.Text = "High Limit";
            this.checkHiLimit.UseVisualStyleBackColor = true;
            this.checkHiLimit.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 296);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 16);
            this.label21.TabIndex = 59;
            this.label21.Text = "Limit Enable";
            // 
            // textDeadband
            // 
            this.textDeadband.Location = new System.Drawing.Point(149, 195);
            this.textDeadband.Margin = new System.Windows.Forms.Padding(4);
            this.textDeadband.Name = "textDeadband";
            this.textDeadband.Size = new System.Drawing.Size(311, 22);
            this.textDeadband.TabIndex = 30;
            this.textDeadband.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHighLimit_KeyPress);
            this.textDeadband.Leave += new System.EventHandler(this.textDeadband_Leave);
            this.textDeadband.Validating += new System.ComponentModel.CancelEventHandler(this.textDeadband_Validating);
            this.textDeadband.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // grpnotifytype
            // 
            this.grpnotifytype.Controls.Add(this.checkEvent);
            this.grpnotifytype.Controls.Add(this.checkAlarm);
            this.grpnotifytype.Location = new System.Drawing.Point(149, 371);
            this.grpnotifytype.Margin = new System.Windows.Forms.Padding(4);
            this.grpnotifytype.Name = "grpnotifytype";
            this.grpnotifytype.Padding = new System.Windows.Forms.Padding(4);
            this.grpnotifytype.Size = new System.Drawing.Size(343, 57);
            this.grpnotifytype.TabIndex = 44;
            this.grpnotifytype.TabStop = false;
            // 
            // checkEvent
            // 
            this.checkEvent.AutoSize = true;
            this.checkEvent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkEvent.Checked = true;
            this.checkEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEvent.Location = new System.Drawing.Point(129, 25);
            this.checkEvent.Margin = new System.Windows.Forms.Padding(4);
            this.checkEvent.Name = "checkEvent";
            this.checkEvent.Size = new System.Drawing.Size(63, 20);
            this.checkEvent.TabIndex = 37;
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
            this.checkAlarm.Margin = new System.Windows.Forms.Padding(4);
            this.checkAlarm.Name = "checkAlarm";
            this.checkAlarm.Size = new System.Drawing.Size(64, 20);
            this.checkAlarm.TabIndex = 36;
            this.checkAlarm.Text = "Alarm";
            this.checkAlarm.UseVisualStyleBackColor = true;
            this.checkAlarm.CheckedChanged += new System.EventHandler(this.checkAlarm_CheckedChanged);
            this.checkAlarm.Validating += new System.ComponentModel.CancelEventHandler(this.checkAlarm_Validating);
            this.checkAlarm.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 199);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 16);
            this.label20.TabIndex = 57;
            this.label20.Text = "Deadband";
            // 
            // textBoxLowLimit
            // 
            this.textBoxLowLimit.Location = new System.Drawing.Point(149, 160);
            this.textBoxLowLimit.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxLowLimit.Name = "textBoxLowLimit";
            this.textBoxLowLimit.Size = new System.Drawing.Size(311, 22);
            this.textBoxLowLimit.TabIndex = 29;
            this.textBoxLowLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHighLimit_KeyPress);
            this.textBoxLowLimit.Leave += new System.EventHandler(this.textBoxLowLimit_Leave);
            this.textBoxLowLimit.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxLowLimit_Validating);
            this.textBoxLowLimit.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 163);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(61, 16);
            this.label19.TabIndex = 55;
            this.label19.Text = "Low Limit";
            // 
            // textBoxHighLimit
            // 
            this.textBoxHighLimit.Location = new System.Drawing.Point(149, 122);
            this.textBoxHighLimit.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxHighLimit.Name = "textBoxHighLimit";
            this.textBoxHighLimit.Size = new System.Drawing.Size(311, 22);
            this.textBoxHighLimit.TabIndex = 28;
            this.textBoxHighLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHighLimit_KeyPress);
            this.textBoxHighLimit.Leave += new System.EventHandler(this.textBoxHighLimit_Leave);
            this.textBoxHighLimit.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxHighLimit_Validating);
            this.textBoxHighLimit.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 126);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 53;
            this.label2.Text = "High Limit";
            // 
            // textTimeDelayNormal
            // 
            this.textTimeDelayNormal.Location = new System.Drawing.Point(149, 48);
            this.textTimeDelayNormal.Margin = new System.Windows.Forms.Padding(4);
            this.textTimeDelayNormal.Name = "textTimeDelayNormal";
            this.textTimeDelayNormal.Size = new System.Drawing.Size(177, 22);
            this.textTimeDelayNormal.TabIndex = 26;
            this.textTimeDelayNormal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTimeDelayNormal_KeyPress);
            this.textTimeDelayNormal.Leave += new System.EventHandler(this.textTimeDelayNormal_Leave);
            this.textTimeDelayNormal.Validating += new System.ComponentModel.CancelEventHandler(this.textTimeDelayNormal_Validating);
            this.textTimeDelayNormal.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 51;
            this.label1.Text = "Time Delay Normal";
            // 
            // cmb_BinaryValue
            // 
            this.cmb_BinaryValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_BinaryValue.FormattingEnabled = true;
            this.cmb_BinaryValue.Location = new System.Drawing.Point(148, 234);
            this.cmb_BinaryValue.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_BinaryValue.Name = "cmb_BinaryValue";
            this.cmb_BinaryValue.Size = new System.Drawing.Size(311, 24);
            this.cmb_BinaryValue.Sorted = false;
            this.cmb_BinaryValue.TabIndex = 27;
            // 
            // comboNotifyclass
            // 
            this.comboNotifyclass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNotifyclass.FormattingEnabled = true;
            this.comboNotifyclass.Location = new System.Drawing.Point(149, 83);
            this.comboNotifyclass.Margin = new System.Windows.Forms.Padding(4);
            this.comboNotifyclass.Name = "comboNotifyclass";
            this.comboNotifyclass.Size = new System.Drawing.Size(311, 24);
            this.comboNotifyclass.TabIndex = 27;
            this.comboNotifyclass.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            // lbl_BinaryValue
            // 
            this.lbl_BinaryValue.AutoSize = true;
            this.lbl_BinaryValue.Location = new System.Drawing.Point(7, 244);
            this.lbl_BinaryValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_BinaryValue.Name = "lbl_BinaryValue";
            this.lbl_BinaryValue.Size = new System.Drawing.Size(120, 16);
            this.lbl_BinaryValue.TabIndex = 47;
            this.lbl_BinaryValue.Text = "Inhibit Binary Value";
            // 
            // textTimeDelay
            // 
            this.textTimeDelay.Location = new System.Drawing.Point(149, 16);
            this.textTimeDelay.Margin = new System.Windows.Forms.Padding(4);
            this.textTimeDelay.Name = "textTimeDelay";
            this.textTimeDelay.Size = new System.Drawing.Size(177, 22);
            this.textTimeDelay.TabIndex = 25;
            this.textTimeDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTimeDelay_KeyPress);
            this.textTimeDelay.Leave += new System.EventHandler(this.textTimeDelay_Leave);
            this.textTimeDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textTimeDelay_Validating);
            this.textTimeDelay.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 93);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(110, 16);
            this.label15.TabIndex = 47;
            this.label15.Text = "Notification Class";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 19);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 16);
            this.label13.TabIndex = 45;
            this.label13.Text = "Time Delay";
            // 
            // grpeventenb
            // 
            this.grpeventenb.Controls.Add(this.checkToNormal);
            this.grpeventenb.Controls.Add(this.checkToFault);
            this.grpeventenb.Controls.Add(this.checktooffNormal);
            this.grpeventenb.Location = new System.Drawing.Point(149, 327);
            this.grpeventenb.Margin = new System.Windows.Forms.Padding(4);
            this.grpeventenb.Name = "grpeventenb";
            this.grpeventenb.Padding = new System.Windows.Forms.Padding(4);
            this.grpeventenb.Size = new System.Drawing.Size(343, 43);
            this.grpeventenb.TabIndex = 43;
            this.grpeventenb.TabStop = false;
            // 
            // checkToNormal
            // 
            this.checkToNormal.AutoSize = true;
            this.checkToNormal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkToNormal.Location = new System.Drawing.Point(224, 15);
            this.checkToNormal.Margin = new System.Windows.Forms.Padding(4);
            this.checkToNormal.Name = "checkToNormal";
            this.checkToNormal.Size = new System.Drawing.Size(93, 20);
            this.checkToNormal.TabIndex = 35;
            this.checkToNormal.Text = "To normal:";
            this.checkToNormal.UseVisualStyleBackColor = true;
            this.checkToNormal.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checkToFault
            // 
            this.checkToFault.AutoSize = true;
            this.checkToFault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkToFault.Location = new System.Drawing.Point(133, 15);
            this.checkToFault.Margin = new System.Windows.Forms.Padding(4);
            this.checkToFault.Name = "checkToFault";
            this.checkToFault.Size = new System.Drawing.Size(76, 20);
            this.checkToFault.TabIndex = 34;
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
            this.checktooffNormal.Location = new System.Drawing.Point(11, 14);
            this.checktooffNormal.Margin = new System.Windows.Forms.Padding(4);
            this.checktooffNormal.Name = "checktooffNormal";
            this.checktooffNormal.Size = new System.Drawing.Size(107, 20);
            this.checktooffNormal.TabIndex = 33;
            this.checktooffNormal.Text = "To offnormal:";
            this.checktooffNormal.UseVisualStyleBackColor = true;
            this.checktooffNormal.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 396);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 16);
            this.label17.TabIndex = 40;
            this.label17.Text = "Notify Type";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 349);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 16);
            this.label16.TabIndex = 39;
            this.label16.Text = "Event Enable";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(643, 479);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 31);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(759, 479);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 31);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // checkEnable
            // 
            this.checkEnable.AutoSize = true;
            this.checkEnable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkEnable.Checked = true;
            this.checkEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEnable.Location = new System.Drawing.Point(503, 15);
            this.checkEnable.Margin = new System.Windows.Forms.Padding(4);
            this.checkEnable.Name = "checkEnable";
            this.checkEnable.Size = new System.Drawing.Size(169, 20);
            this.checkEnable.TabIndex = 45;
            this.checkEnable.Text = "Event Detection Enable";
            this.checkEnable.UseVisualStyleBackColor = true;
            this.checkEnable.CheckedChanged += new System.EventHandler(this.checkEnable_CheckedChanged);
            this.checkEnable.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // FormAnalogBacNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(1003, 524);
            this.ControlBox = false;
            this.Controls.Add(this.checkEnable);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAnalogBacNet";
            this.ShowIcon = false;
            this.Text = "FormDigitalBacNet";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpnotifytype.ResumeLayout(false);
            this.grpnotifytype.PerformLayout();
            this.grpeventenb.ResumeLayout(false);
            this.grpeventenb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.TextBox textObjectName;
        private System.Windows.Forms.Label labelObjIdentifier;
        private System.Windows.Forms.Label labelDeviceType;
        private System.Windows.Forms.Label labelObjType;
        private System.Windows.Forms.Label labelInstanceno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxMaxPresVal;
        private System.Windows.Forms.TextBox textBoxMinPresVal;
        private System.Windows.Forms.Label lblOutOfRangeManagement;
        private System.Windows.Forms.CheckBox checkBoxOutOfRange;
        private System.Windows.Forms.Label labelMinValue;
        private System.Windows.Forms.TextBox textBoxMinValue;
        private System.Windows.Forms.Label labelMaxValue;
        private System.Windows.Forms.TextBox textBoxMaxValue;
        private System.Windows.Forms.TextBox textReDefault;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxCovIncr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkLoLimit;
        private System.Windows.Forms.CheckBox checkHiLimit;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textDeadband;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxLowLimit;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxHighLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textTimeDelayNormal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboNotifyclass;
        private System.Windows.Forms.TextBox textTimeDelay;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox grpnotifytype;
        private System.Windows.Forms.CheckBox checkEvent;
        private System.Windows.Forms.CheckBox checkAlarm;
        private System.Windows.Forms.GroupBox grpeventenb;
        private System.Windows.Forms.CheckBox checkToNormal;
        private System.Windows.Forms.CheckBox checkToFault;
        private System.Windows.Forms.CheckBox checktooffNormal;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox comboBoxUnits;
        private System.Windows.Forms.CheckBox checkEnable;
        private TextBox textInitialValue;
        private Label lblInitialValue;
        private Label label22;
        private Label label14;
        private Label label24;
        private Label label23;
        private ComboBox cmb_BinaryValue;
        private Label lbl_BinaryValue;
    }
}