using System;

namespace XMPS2000.Bacnet
{
    partial class FormSchedule
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
            this.lblObjectIdentifier = new System.Windows.Forms.Label();
            this.lblInstanceNumber = new System.Windows.Forms.Label();
            this.lblObjectType = new System.Windows.Forms.Label();
            this.lblObjectName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblScheduleValue = new System.Windows.Forms.Label();
            this.lblEffectivePeriod = new System.Windows.Forms.Label();
            this.lblScheduleType = new System.Windows.Forms.Label();
            this.textBoxObjectIdentifier = new System.Windows.Forms.TextBox();
            this.textBoxInstanceNumber = new System.Windows.Forms.TextBox();
            this.textBoxObjectType = new System.Windows.Forms.TextBox();
            this.textBoxObjectName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.comboBoxScheduleValue = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTagName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxAny = new System.Windows.Forms.CheckBox();
            this.comboBoxScheduleType = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBoxSpecialEvents = new System.Windows.Forms.GroupBox();
            this.dataGridViewSpecialEvents = new System.Windows.Forms.DataGridView();
            this.EventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Start_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.textBoxTotalNoOfEvent = new System.Windows.Forms.TextBox();
            this.comboBoxEventType = new System.Windows.Forms.ComboBox();
            this.lblEventType = new System.Windows.Forms.Label();
            this.lblTotalNoOfEvents = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.GbValue = new System.Windows.Forms.GroupBox();
            this.textObjectName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ChkNull = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxSpecialEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpecialEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.GbValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblObjectIdentifier
            // 
            this.lblObjectIdentifier.AutoSize = true;
            this.lblObjectIdentifier.Location = new System.Drawing.Point(5, 23);
            this.lblObjectIdentifier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjectIdentifier.Name = "lblObjectIdentifier";
            this.lblObjectIdentifier.Size = new System.Drawing.Size(99, 16);
            this.lblObjectIdentifier.TabIndex = 0;
            this.lblObjectIdentifier.Text = "Object Identifier";
            // 
            // lblInstanceNumber
            // 
            this.lblInstanceNumber.AutoSize = true;
            this.lblInstanceNumber.Location = new System.Drawing.Point(5, 58);
            this.lblInstanceNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstanceNumber.Name = "lblInstanceNumber";
            this.lblInstanceNumber.Size = new System.Drawing.Size(108, 16);
            this.lblInstanceNumber.TabIndex = 1;
            this.lblInstanceNumber.Text = "Instance Number";
            // 
            // lblObjectType
            // 
            this.lblObjectType.AutoSize = true;
            this.lblObjectType.Location = new System.Drawing.Point(5, 90);
            this.lblObjectType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjectType.Name = "lblObjectType";
            this.lblObjectType.Size = new System.Drawing.Size(81, 16);
            this.lblObjectType.TabIndex = 2;
            this.lblObjectType.Text = "Object Type";
            // 
            // lblObjectName
            // 
            this.lblObjectName.AutoSize = true;
            this.lblObjectName.Location = new System.Drawing.Point(5, 128);
            this.lblObjectName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjectName.Name = "lblObjectName";
            this.lblObjectName.Size = new System.Drawing.Size(86, 16);
            this.lblObjectName.TabIndex = 4;
            this.lblObjectName.Text = "Object Name";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(9, 165);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(75, 16);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Description";
            // 
            // lblScheduleValue
            // 
            this.lblScheduleValue.AutoSize = true;
            this.lblScheduleValue.Location = new System.Drawing.Point(7, 206);
            this.lblScheduleValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScheduleValue.Name = "lblScheduleValue";
            this.lblScheduleValue.Size = new System.Drawing.Size(99, 16);
            this.lblScheduleValue.TabIndex = 6;
            this.lblScheduleValue.Text = "Schedule Type";
            // 
            // lblEffectivePeriod
            // 
            this.lblEffectivePeriod.AutoSize = true;
            this.lblEffectivePeriod.Location = new System.Drawing.Point(8, 364);
            this.lblEffectivePeriod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEffectivePeriod.Name = "lblEffectivePeriod";
            this.lblEffectivePeriod.Size = new System.Drawing.Size(98, 16);
            this.lblEffectivePeriod.TabIndex = 7;
            this.lblEffectivePeriod.Text = "EffectivePeriod";
            // 
            // lblScheduleType
            // 
            this.lblScheduleType.AutoSize = true;
            this.lblScheduleType.Location = new System.Drawing.Point(9, 425);
            this.lblScheduleType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScheduleType.Name = "lblScheduleType";
            this.lblScheduleType.Size = new System.Drawing.Size(99, 16);
            this.lblScheduleType.TabIndex = 8;
            this.lblScheduleType.Text = "Configure Schedule";
            // 
            // textBoxObjectIdentifier
            // 
            this.textBoxObjectIdentifier.Enabled = false;
            this.textBoxObjectIdentifier.Location = new System.Drawing.Point(157, 20);
            this.textBoxObjectIdentifier.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxObjectIdentifier.Name = "textBoxObjectIdentifier";
            this.textBoxObjectIdentifier.Size = new System.Drawing.Size(221, 22);
            this.textBoxObjectIdentifier.TabIndex = 9;
            // 
            // textBoxInstanceNumber
            // 
            this.textBoxInstanceNumber.Enabled = false;
            this.textBoxInstanceNumber.Location = new System.Drawing.Point(157, 53);
            this.textBoxInstanceNumber.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxInstanceNumber.Name = "textBoxInstanceNumber";
            this.textBoxInstanceNumber.Size = new System.Drawing.Size(221, 22);
            this.textBoxInstanceNumber.TabIndex = 10;
            // 
            // textBoxObjectType
            // 
            this.textBoxObjectType.Enabled = false;
            this.textBoxObjectType.Location = new System.Drawing.Point(157, 85);
            this.textBoxObjectType.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxObjectType.Name = "textBoxObjectType";
            this.textBoxObjectType.Size = new System.Drawing.Size(221, 22);
            this.textBoxObjectType.TabIndex = 11;
            // 
            // textBoxObjectName
            // 
            this.textBoxObjectName.Location = new System.Drawing.Point(157, 124);
            this.textBoxObjectName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxObjectName.MaxLength = 25;
            this.textBoxObjectName.Name = "textBoxObjectName";
            this.textBoxObjectName.Size = new System.Drawing.Size(221, 22);
            this.textBoxObjectName.TabIndex = 13;
            this.textBoxObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxObjectName_Validating);
            this.textBoxObjectName.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(157, 161);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDescription.MaxLength = 25;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(221, 22);
            this.textBoxDescription.TabIndex = 14;
            this.textBoxDescription.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // comboBoxScheduleValue
            // 
            this.comboBoxScheduleValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleValue.Enabled = false;
            this.comboBoxScheduleValue.FormattingEnabled = true;
            this.comboBoxScheduleValue.Items.AddRange(new object[] {
            "Boolean",
            "Numeric"});
            this.comboBoxScheduleValue.Location = new System.Drawing.Point(157, 194);
            this.comboBoxScheduleValue.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxScheduleValue.Name = "comboBoxScheduleValue";
            this.comboBoxScheduleValue.Size = new System.Drawing.Size(221, 24);
            this.comboBoxScheduleValue.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelTagName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(-3, -6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(435, 295);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // labelTagName
            // 
            this.labelTagName.AutoSize = true;
            this.labelTagName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTagName.Location = new System.Drawing.Point(163, 245);
            this.labelTagName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTagName.Name = "labelTagName";
            this.labelTagName.Size = new System.Drawing.Size(104, 18);
            this.labelTagName.TabIndex = 28;
            this.labelTagName.Text = "Schedule Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 247);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 27;
            this.label1.Text = "Tag";
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(240, 23);
            this.dateTimePickerStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(151, 22);
            this.dateTimePickerStartDate.TabIndex = 17;
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.DateTimePickerStartDate_ValueChanged);
            this.dateTimePickerStartDate.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(240, 58);
            this.dateTimePickerEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(152, 22);
            this.dateTimePickerEndDate.TabIndex = 18;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(157, 31);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(77, 16);
            this.lblStartDate.TabIndex = 19;
            this.lblStartDate.Text = "Start Period";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(157, 374);
            this.lblEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(74, 16);
            this.lblEndDate.TabIndex = 20;
            this.lblEndDate.Text = "End Period";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxAny);
            this.groupBox2.Controls.Add(this.dateTimePickerStartDate);
            this.groupBox2.Controls.Add(this.lblStartDate);
            this.groupBox2.Controls.Add(this.dateTimePickerEndDate);
            this.groupBox2.Location = new System.Drawing.Point(-3, 310);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(468, 102);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // checkBoxAny
            // 
            this.checkBoxAny.AutoSize = true;
            this.checkBoxAny.Location = new System.Drawing.Point(404, 43);
            this.checkBoxAny.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAny.Name = "checkBoxAny";
            this.checkBoxAny.Size = new System.Drawing.Size(52, 20);
            this.checkBoxAny.TabIndex = 20;
            this.checkBoxAny.Text = "Any";
            this.checkBoxAny.UseVisualStyleBackColor = true;
            this.checkBoxAny.CheckedChanged += new System.EventHandler(this.checkBoxAny_CheckedChanged);
            this.checkBoxAny.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // comboBoxScheduleType
            // 
            this.comboBoxScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleType.FormattingEnabled = true;
            this.comboBoxScheduleType.Items.AddRange(new object[] {
            "Weekly",
            "Special Events"});
            this.comboBoxScheduleType.Location = new System.Drawing.Point(161, 420);
            this.comboBoxScheduleType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxScheduleType.Name = "comboBoxScheduleType";
            this.comboBoxScheduleType.Size = new System.Drawing.Size(221, 24);
            this.comboBoxScheduleType.TabIndex = 22;
            this.comboBoxScheduleType.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleType_SelectedIndexChanged);
            this.comboBoxScheduleType.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(168, 818);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 28);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBoxSpecialEvents
            // 
            this.groupBoxSpecialEvents.Controls.Add(this.dataGridViewSpecialEvents);
            this.groupBoxSpecialEvents.Controls.Add(this.textBoxTotalNoOfEvent);
            this.groupBoxSpecialEvents.Controls.Add(this.comboBoxEventType);
            this.groupBoxSpecialEvents.Controls.Add(this.lblEventType);
            this.groupBoxSpecialEvents.Controls.Add(this.lblTotalNoOfEvents);
            this.groupBoxSpecialEvents.Location = new System.Drawing.Point(8, 557);
            this.groupBoxSpecialEvents.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxSpecialEvents.Name = "groupBoxSpecialEvents";
            this.groupBoxSpecialEvents.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxSpecialEvents.Size = new System.Drawing.Size(889, 258);
            this.groupBoxSpecialEvents.TabIndex = 24;
            this.groupBoxSpecialEvents.TabStop = false;
            // 
            // dataGridViewSpecialEvents
            // 
            this.dataGridViewSpecialEvents.AllowUserToAddRows = false;
            this.dataGridViewSpecialEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSpecialEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventType,
            this.EventId,
            this.EventName,
            this.Start_Date,
            this.End_Date,
            this.Edit,
            this.Remove});
            this.dataGridViewSpecialEvents.Location = new System.Drawing.Point(13, 87);
            this.dataGridViewSpecialEvents.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewSpecialEvents.Name = "dataGridViewSpecialEvents";
            this.dataGridViewSpecialEvents.ReadOnly = true;
            this.dataGridViewSpecialEvents.RowHeadersWidth = 51;
            this.dataGridViewSpecialEvents.Size = new System.Drawing.Size(860, 134);
            this.dataGridViewSpecialEvents.TabIndex = 4;
            this.dataGridViewSpecialEvents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewSpecialEvents_CellContentClick);
            // 
            // EventType
            // 
            this.EventType.Frozen = true;
            this.EventType.HeaderText = "EventType";
            this.EventType.MinimumWidth = 6;
            this.EventType.Name = "EventType";
            this.EventType.ReadOnly = true;
            this.EventType.Width = 125;
            // 
            // EventId
            // 
            this.EventId.Frozen = true;
            this.EventId.HeaderText = "EventId";
            this.EventId.MinimumWidth = 6;
            this.EventId.Name = "EventId";
            this.EventId.ReadOnly = true;
            this.EventId.Visible = false;
            this.EventId.Width = 125;
            // 
            // EventName
            // 
            this.EventName.HeaderText = "EventName";
            this.EventName.MinimumWidth = 6;
            this.EventName.Name = "EventName";
            this.EventName.ReadOnly = true;
            this.EventName.Width = 125;
            // 
            // Start_Date
            // 
            this.Start_Date.HeaderText = "Start_Date";
            this.Start_Date.MinimumWidth = 6;
            this.Start_Date.Name = "Start_Date";
            this.Start_Date.ReadOnly = true;
            this.Start_Date.Width = 125;
            // 
            // End_Date
            // 
            this.End_Date.HeaderText = "End_Date";
            this.End_Date.MinimumWidth = 6;
            this.End_Date.Name = "End_Date";
            this.End_Date.ReadOnly = true;
            this.End_Date.Width = 125;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.MinimumWidth = 6;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Text = "Edit";
            this.Edit.UseColumnTextForButtonValue = true;
            this.Edit.Width = 125;
            // 
            // Remove
            // 
            this.Remove.HeaderText = "Remove";
            this.Remove.MinimumWidth = 6;
            this.Remove.Name = "Remove";
            this.Remove.ReadOnly = true;
            this.Remove.Text = "Remove";
            this.Remove.UseColumnTextForButtonValue = true;
            this.Remove.Width = 125;
            // 
            // textBoxTotalNoOfEvent
            // 
            this.textBoxTotalNoOfEvent.Location = new System.Drawing.Point(197, 16);
            this.textBoxTotalNoOfEvent.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTotalNoOfEvent.Name = "textBoxTotalNoOfEvent";
            this.textBoxTotalNoOfEvent.ReadOnly = true;
            this.textBoxTotalNoOfEvent.Size = new System.Drawing.Size(219, 22);
            this.textBoxTotalNoOfEvent.TabIndex = 3;
            // 
            // comboBoxEventType
            // 
            this.comboBoxEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEventType.FormattingEnabled = true;
            this.comboBoxEventType.Items.AddRange(new object[] {
            "Date",
            "Date Range",
            "Week And Day",
            "Calendar Reference"});
            this.comboBoxEventType.Location = new System.Drawing.Point(195, 54);
            this.comboBoxEventType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxEventType.Name = "comboBoxEventType";
            this.comboBoxEventType.Size = new System.Drawing.Size(221, 24);
            this.comboBoxEventType.TabIndex = 2;
            this.comboBoxEventType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEventType_SelectedIndexChanged);
            // 
            // lblEventType
            // 
            this.lblEventType.AutoSize = true;
            this.lblEventType.Location = new System.Drawing.Point(9, 57);
            this.lblEventType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEventType.Name = "lblEventType";
            this.lblEventType.Size = new System.Drawing.Size(76, 16);
            this.lblEventType.TabIndex = 1;
            this.lblEventType.Text = "Event Type";
            // 
            // lblTotalNoOfEvents
            // 
            this.lblTotalNoOfEvents.AutoSize = true;
            this.lblTotalNoOfEvents.Location = new System.Drawing.Point(8, 20);
            this.lblTotalNoOfEvents.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalNoOfEvents.Name = "lblTotalNoOfEvents";
            this.lblTotalNoOfEvents.Size = new System.Drawing.Size(168, 16);
            this.lblTotalNoOfEvents.TabIndex = 0;
            this.lblTotalNoOfEvents.Text = "Total No Of Special Events";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox3
            // 
            this.GbValue.Controls.Add(this.textObjectName);
            this.GbValue.Controls.Add(this.label7);
            this.GbValue.Controls.Add(this.ChkNull);
            this.GbValue.Location = new System.Drawing.Point(12, 465);
            this.GbValue.Name = "GbValue";
            this.GbValue.Size = new System.Drawing.Size(366, 85);
            this.GbValue.TabIndex = 25;
            this.GbValue.TabStop = false;
            this.GbValue.Text = "Schedule Default Value";
            // 
            // textObjectName
            // 
            this.textObjectName.Location = new System.Drawing.Point(127, 49);
            this.textObjectName.Margin = new System.Windows.Forms.Padding(4);
            this.textObjectName.MaxLength = 25;
            this.textObjectName.Name = "textObjectName";
            this.textObjectName.Size = new System.Drawing.Size(170, 22);
            this.textObjectName.TabIndex = 38;
            this.textObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textObjectName_Validating);
            this.textObjectName.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 55);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 16);
            this.label7.TabIndex = 39;
            this.label7.Text = "Value";
            // 
            // checkBox1
            // 
            this.ChkNull.AutoSize = true;
            this.ChkNull.Location = new System.Drawing.Point(29, 22);
            this.ChkNull.Margin = new System.Windows.Forms.Padding(4);
            this.ChkNull.Name = "ChkNull";
            this.ChkNull.Size = new System.Drawing.Size(52, 20);
            this.ChkNull.TabIndex = 21;
            this.ChkNull.Text = "Null";
            this.ChkNull.UseVisualStyleBackColor = true;
            this.ChkNull.CheckedChanged += new System.EventHandler(this.ChkNull_CheckedChanged);
            this.ChkNull.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // FormSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(912, 850);
            this.ControlBox = false;
            this.Controls.Add(this.GbValue);
            this.Controls.Add(this.groupBoxSpecialEvents);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.comboBoxScheduleType);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.comboBoxScheduleValue);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxObjectName);
            this.Controls.Add(this.textBoxObjectType);
            this.Controls.Add(this.textBoxInstanceNumber);
            this.Controls.Add(this.textBoxObjectIdentifier);
            this.Controls.Add(this.lblScheduleType);
            this.Controls.Add(this.lblEffectivePeriod);
            this.Controls.Add(this.lblScheduleValue);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblObjectName);
            this.Controls.Add(this.lblObjectType);
            this.Controls.Add(this.lblInstanceNumber);
            this.Controls.Add(this.lblObjectIdentifier);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSchedule";
            this.ShowIcon = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxSpecialEvents.ResumeLayout(false);
            this.groupBoxSpecialEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpecialEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.GbValue.ResumeLayout(false);
            this.GbValue.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblObjectIdentifier;
        private System.Windows.Forms.Label lblInstanceNumber;
        private System.Windows.Forms.Label lblObjectType;
        private System.Windows.Forms.Label lblObjectName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblScheduleValue;
        private System.Windows.Forms.Label lblEffectivePeriod;
        private System.Windows.Forms.Label lblScheduleType;
        private System.Windows.Forms.TextBox textBoxObjectIdentifier;
        private System.Windows.Forms.TextBox textBoxInstanceNumber;
        private System.Windows.Forms.TextBox textBoxObjectType;
        private System.Windows.Forms.TextBox textBoxObjectName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ComboBox comboBoxScheduleValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxScheduleType;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBoxSpecialEvents;
        private System.Windows.Forms.TextBox textBoxTotalNoOfEvent;
        private System.Windows.Forms.Label lblTotalNoOfEvents;
        private System.Windows.Forms.DataGridView dataGridViewSpecialEvents;
        private System.Windows.Forms.ComboBox comboBoxEventType;
        private System.Windows.Forms.Label lblEventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventId;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn End_Date;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewButtonColumn Remove;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label labelTagName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAny;
        private System.Windows.Forms.GroupBox GbValue;
        private System.Windows.Forms.CheckBox ChkNull;
        private System.Windows.Forms.TextBox textObjectName;
        private System.Windows.Forms.Label label7;
    }
}