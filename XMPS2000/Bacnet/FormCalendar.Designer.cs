
using System;

namespace XMPS2000.Bacnet
{
    partial class FormCalendar
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
            this.DaTimePickerEndSelecDate = new System.Windows.Forms.DateTimePicker();
            this.ComboBoxEndWeekDay = new System.Windows.Forms.ComboBox();
            this.labelEndWeekDay = new System.Windows.Forms.Label();
            this.ComboBoxEndYear = new System.Windows.Forms.ComboBox();
            this.NumericUpDownEndDay = new System.Windows.Forms.NumericUpDown();
            this.ComboBoxEndMonth = new System.Windows.Forms.ComboBox();
            this.labelEndDay = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxEndDate = new System.Windows.Forms.GroupBox();
            this.dateRangeEndDateAny = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DaTimePickerStartSelecDate = new System.Windows.Forms.DateTimePicker();
            this.labelStartSelectDate = new System.Windows.Forms.Label();
            this.ComboBoxStartYear = new System.Windows.Forms.ComboBox();
            this.ComboBoxStartMonth = new System.Windows.Forms.ComboBox();
            this.labelStartYear = new System.Windows.Forms.Label();
            this.labelStartMonth = new System.Windows.Forms.Label();
            this.labelStartDay = new System.Windows.Forms.Label();
            this.NumericUpDownStartDay = new System.Windows.Forms.NumericUpDown();
            this.labelStartWeekDay = new System.Windows.Forms.Label();
            this.ComboBoxStartWeekDay = new System.Windows.Forms.ComboBox();
            this.groupBoxStartDate = new System.Windows.Forms.GroupBox();
            this.dateEventYearAny = new System.Windows.Forms.CheckBox();
            this.dateEventMonthAny = new System.Windows.Forms.CheckBox();
            this.dateEventDayAny = new System.Windows.Forms.CheckBox();
            this.dateEventWeekDayAny = new System.Windows.Forms.CheckBox();
            this.dateRangeStartDateAny = new System.Windows.Forms.CheckBox();
            this.TextBoxPriority = new System.Windows.Forms.TextBox();
            this.labelPriority = new System.Windows.Forms.Label();
            this.comboBoxEventsType = new System.Windows.Forms.ComboBox();
            this.TextBoxName = new System.Windows.Forms.TextBox();
            this.labelEventType = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelName = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBoxWeekAndDay = new System.Windows.Forms.GroupBox();
            this.ComboBoxMonth = new System.Windows.Forms.ComboBox();
            this.labelMonth = new System.Windows.Forms.Label();
            this.ComboBoxWeek = new System.Windows.Forms.ComboBox();
            this.labelWeek = new System.Windows.Forms.Label();
            this.ComboBoxWeekDay = new System.Windows.Forms.ComboBox();
            this.labelWeekDay = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxObjectName = new System.Windows.Forms.TextBox();
            this.textBoxObjectType = new System.Windows.Forms.TextBox();
            this.textBoxInstanceNumber = new System.Windows.Forms.TextBox();
            this.textBoxObjectIdentifier = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblObjectName = new System.Windows.Forms.Label();
            this.lblObjectType = new System.Windows.Forms.Label();
            this.lblInstanceNumber = new System.Windows.Forms.Label();
            this.lblObjectIdentifier = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxStartAndEndDate = new System.Windows.Forms.GroupBox();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.dataGridViewEventsInfo = new System.Windows.Forms.DataGridView();
            this.EventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownEndDay)).BeginInit();
            this.groupBoxEndDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownStartDay)).BeginInit();
            this.groupBoxStartDate.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxWeekAndDay.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxStartAndEndDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEventsInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // DaTimePickerEndSelecDate
            // 
            this.DaTimePickerEndSelecDate.Location = new System.Drawing.Point(101, 185);
            this.DaTimePickerEndSelecDate.Margin = new System.Windows.Forms.Padding(4);
            this.DaTimePickerEndSelecDate.Name = "DaTimePickerEndSelecDate";
            this.DaTimePickerEndSelecDate.Size = new System.Drawing.Size(215, 22);
            this.DaTimePickerEndSelecDate.TabIndex = 19;
            this.DaTimePickerEndSelecDate.ValueChanged += new EventHandler(BacNetValidator.ControlChanged);
            this.DaTimePickerEndSelecDate.ValueChanged += new EventHandler(this.DaTimePickerEndSelecDate_ValueChanged);

            // 
            // ComboBoxEndWeekDay
            // 
            this.ComboBoxEndWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxEndWeekDay.Enabled = false;
            this.ComboBoxEndWeekDay.FormattingEnabled = true;
            this.ComboBoxEndWeekDay.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.ComboBoxEndWeekDay.Location = new System.Drawing.Point(101, 46);
            this.ComboBoxEndWeekDay.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxEndWeekDay.Name = "ComboBoxEndWeekDay";
            this.ComboBoxEndWeekDay.Size = new System.Drawing.Size(160, 24);
            this.ComboBoxEndWeekDay.TabIndex = 10;
            // 
            // labelEndWeekDay
            // 
            this.labelEndWeekDay.AutoSize = true;
            this.labelEndWeekDay.Location = new System.Drawing.Point(8, 50);
            this.labelEndWeekDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEndWeekDay.Name = "labelEndWeekDay";
            this.labelEndWeekDay.Size = new System.Drawing.Size(71, 16);
            this.labelEndWeekDay.TabIndex = 11;
            this.labelEndWeekDay.Text = "Week Day";
            // 
            // ComboBoxEndYear
            // 
            this.ComboBoxEndYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxEndYear.Enabled = false;
            this.ComboBoxEndYear.FormattingEnabled = true;
            this.ComboBoxEndYear.Location = new System.Drawing.Point(101, 150);
            this.ComboBoxEndYear.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxEndYear.Name = "ComboBoxEndYear";
            this.ComboBoxEndYear.Size = new System.Drawing.Size(160, 24);
            this.ComboBoxEndYear.TabIndex = 17;
            // 
            // NumericUpDownEndDay
            // 
            this.NumericUpDownEndDay.Enabled = false;
            this.NumericUpDownEndDay.Location = new System.Drawing.Point(101, 81);
            this.NumericUpDownEndDay.Margin = new System.Windows.Forms.Padding(4);
            this.NumericUpDownEndDay.Name = "NumericUpDownEndDay";
            this.NumericUpDownEndDay.Size = new System.Drawing.Size(160, 22);
            this.NumericUpDownEndDay.TabIndex = 12;
            // 
            // ComboBoxEndMonth
            // 
            this.ComboBoxEndMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxEndMonth.Enabled = false;
            this.ComboBoxEndMonth.FormattingEnabled = true;
            this.ComboBoxEndMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.ComboBoxEndMonth.Location = new System.Drawing.Point(101, 116);
            this.ComboBoxEndMonth.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxEndMonth.Name = "ComboBoxEndMonth";
            this.ComboBoxEndMonth.Size = new System.Drawing.Size(160, 24);
            this.ComboBoxEndMonth.TabIndex = 16;
            // 
            // labelEndDay
            // 
            this.labelEndDay.AutoSize = true;
            this.labelEndDay.Location = new System.Drawing.Point(8, 86);
            this.labelEndDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEndDay.Name = "labelEndDay";
            this.labelEndDay.Size = new System.Drawing.Size(32, 16);
            this.labelEndDay.TabIndex = 13;
            this.labelEndDay.Text = "Day";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 155);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Year";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Month";
            // 
            // groupBoxEndDate
            // 
            this.groupBoxEndDate.Controls.Add(this.dateRangeEndDateAny);
            this.groupBoxEndDate.Controls.Add(this.DaTimePickerEndSelecDate);
            this.groupBoxEndDate.Controls.Add(this.ComboBoxEndWeekDay);
            this.groupBoxEndDate.Controls.Add(this.label1);
            this.groupBoxEndDate.Controls.Add(this.labelEndWeekDay);
            this.groupBoxEndDate.Controls.Add(this.ComboBoxEndYear);
            this.groupBoxEndDate.Controls.Add(this.NumericUpDownEndDay);
            this.groupBoxEndDate.Controls.Add(this.ComboBoxEndMonth);
            this.groupBoxEndDate.Controls.Add(this.labelEndDay);
            this.groupBoxEndDate.Controls.Add(this.label2);
            this.groupBoxEndDate.Controls.Add(this.label3);
            this.groupBoxEndDate.Location = new System.Drawing.Point(24, 314);
            this.groupBoxEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxEndDate.Name = "groupBoxEndDate";
            this.groupBoxEndDate.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxEndDate.Size = new System.Drawing.Size(325, 218);
            this.groupBoxEndDate.TabIndex = 7;
            this.groupBoxEndDate.TabStop = false;
            this.groupBoxEndDate.Text = "End Date";
            // 
            // dateRangeEndDateAny
            // 
            this.dateRangeEndDateAny.AutoSize = true;
            this.dateRangeEndDateAny.Location = new System.Drawing.Point(100, 17);
            this.dateRangeEndDateAny.Margin = new System.Windows.Forms.Padding(4);
            this.dateRangeEndDateAny.Name = "dateRangeEndDateAny";
            this.dateRangeEndDateAny.Size = new System.Drawing.Size(52, 20);
            this.dateRangeEndDateAny.TabIndex = 11;
            this.dateRangeEndDateAny.Text = "Any";
            this.dateRangeEndDateAny.UseVisualStyleBackColor = true;
            this.dateRangeEndDateAny.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 190);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Select Date";
            // 
            // DaTimePickerStartSelecDate
            // 
            this.DaTimePickerStartSelecDate.Location = new System.Drawing.Point(101, 180);
            this.DaTimePickerStartSelecDate.Margin = new System.Windows.Forms.Padding(4);
            this.DaTimePickerStartSelecDate.Name = "DaTimePickerStartSelecDate";
            this.DaTimePickerStartSelecDate.Size = new System.Drawing.Size(215, 22);
            this.DaTimePickerStartSelecDate.TabIndex = 9;
            this.DaTimePickerStartSelecDate.ValueChanged += new System.EventHandler(this.DaTimePickerStartSelecDate_ValueChanged);
            this.DaTimePickerStartSelecDate.Validating += new System.ComponentModel.CancelEventHandler(this.DaTimePickerStartSelecDate_Validating);
            this.DaTimePickerStartSelecDate.ValueChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelStartSelectDate
            // 
            this.labelStartSelectDate.AutoSize = true;
            this.labelStartSelectDate.Location = new System.Drawing.Point(8, 185);
            this.labelStartSelectDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStartSelectDate.Name = "labelStartSelectDate";
            this.labelStartSelectDate.Size = new System.Drawing.Size(77, 16);
            this.labelStartSelectDate.TabIndex = 8;
            this.labelStartSelectDate.Text = "Select Date";
            // 
            // ComboBoxStartYear
            // 
            this.ComboBoxStartYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStartYear.Enabled = false;
            this.ComboBoxStartYear.FormattingEnabled = true;
            this.ComboBoxStartYear.Location = new System.Drawing.Point(101, 145);
            this.ComboBoxStartYear.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxStartYear.Name = "ComboBoxStartYear";
            this.ComboBoxStartYear.Size = new System.Drawing.Size(160, 24);
            this.ComboBoxStartYear.TabIndex = 7;
            // 
            // ComboBoxStartMonth
            // 
            this.ComboBoxStartMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStartMonth.Enabled = false;
            this.ComboBoxStartMonth.FormattingEnabled = true;
            this.ComboBoxStartMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.ComboBoxStartMonth.Location = new System.Drawing.Point(101, 111);
            this.ComboBoxStartMonth.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxStartMonth.Name = "ComboBoxStartMonth";
            this.ComboBoxStartMonth.Size = new System.Drawing.Size(160, 24);
            this.ComboBoxStartMonth.TabIndex = 6;
            // 
            // labelStartYear
            // 
            this.labelStartYear.AutoSize = true;
            this.labelStartYear.Location = new System.Drawing.Point(8, 150);
            this.labelStartYear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStartYear.Name = "labelStartYear";
            this.labelStartYear.Size = new System.Drawing.Size(36, 16);
            this.labelStartYear.TabIndex = 5;
            this.labelStartYear.Text = "Year";
            // 
            // labelStartMonth
            // 
            this.labelStartMonth.AutoSize = true;
            this.labelStartMonth.Location = new System.Drawing.Point(8, 116);
            this.labelStartMonth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStartMonth.Name = "labelStartMonth";
            this.labelStartMonth.Size = new System.Drawing.Size(43, 16);
            this.labelStartMonth.TabIndex = 4;
            this.labelStartMonth.Text = "Month";
            // 
            // labelStartDay
            // 
            this.labelStartDay.AutoSize = true;
            this.labelStartDay.Location = new System.Drawing.Point(8, 81);
            this.labelStartDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStartDay.Name = "labelStartDay";
            this.labelStartDay.Size = new System.Drawing.Size(32, 16);
            this.labelStartDay.TabIndex = 3;
            this.labelStartDay.Text = "Day";
            // 
            // NumericUpDownStartDay
            // 
            this.NumericUpDownStartDay.Enabled = false;
            this.NumericUpDownStartDay.Location = new System.Drawing.Point(101, 76);
            this.NumericUpDownStartDay.Margin = new System.Windows.Forms.Padding(4);
            this.NumericUpDownStartDay.Name = "NumericUpDownStartDay";
            this.NumericUpDownStartDay.Size = new System.Drawing.Size(160, 22);
            this.NumericUpDownStartDay.TabIndex = 2;
            // 
            // labelStartWeekDay
            // 
            this.labelStartWeekDay.AutoSize = true;
            this.labelStartWeekDay.Location = new System.Drawing.Point(8, 46);
            this.labelStartWeekDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStartWeekDay.Name = "labelStartWeekDay";
            this.labelStartWeekDay.Size = new System.Drawing.Size(71, 16);
            this.labelStartWeekDay.TabIndex = 1;
            this.labelStartWeekDay.Text = "Week Day";
            // 
            // ComboBoxStartWeekDay
            // 
            this.ComboBoxStartWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStartWeekDay.Enabled = false;
            this.ComboBoxStartWeekDay.FormattingEnabled = true;
            this.ComboBoxStartWeekDay.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.ComboBoxStartWeekDay.Location = new System.Drawing.Point(101, 41);
            this.ComboBoxStartWeekDay.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxStartWeekDay.Name = "ComboBoxStartWeekDay";
            this.ComboBoxStartWeekDay.Size = new System.Drawing.Size(160, 24);
            this.ComboBoxStartWeekDay.TabIndex = 0;
            // 
            // groupBoxStartDate
            // 
            this.groupBoxStartDate.Controls.Add(this.dateEventYearAny);
            this.groupBoxStartDate.Controls.Add(this.dateEventMonthAny);
            this.groupBoxStartDate.Controls.Add(this.dateEventDayAny);
            this.groupBoxStartDate.Controls.Add(this.dateEventWeekDayAny);
            this.groupBoxStartDate.Controls.Add(this.dateRangeStartDateAny);
            this.groupBoxStartDate.Controls.Add(this.DaTimePickerStartSelecDate);
            this.groupBoxStartDate.Controls.Add(this.labelStartSelectDate);
            this.groupBoxStartDate.Controls.Add(this.ComboBoxStartYear);
            this.groupBoxStartDate.Controls.Add(this.ComboBoxStartMonth);
            this.groupBoxStartDate.Controls.Add(this.labelStartYear);
            this.groupBoxStartDate.Controls.Add(this.labelStartMonth);
            this.groupBoxStartDate.Controls.Add(this.labelStartDay);
            this.groupBoxStartDate.Controls.Add(this.NumericUpDownStartDay);
            this.groupBoxStartDate.Controls.Add(this.labelStartWeekDay);
            this.groupBoxStartDate.Controls.Add(this.ComboBoxStartWeekDay);
            this.groupBoxStartDate.Location = new System.Drawing.Point(25, 91);
            this.groupBoxStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxStartDate.Name = "groupBoxStartDate";
            this.groupBoxStartDate.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxStartDate.Size = new System.Drawing.Size(325, 218);
            this.groupBoxStartDate.TabIndex = 6;
            this.groupBoxStartDate.TabStop = false;
            this.groupBoxStartDate.Text = "Start Date";
            // 
            // dateEventYearAny
            // 
            this.dateEventYearAny.AutoSize = true;
            this.dateEventYearAny.Location = new System.Drawing.Point(271, 148);
            this.dateEventYearAny.Margin = new System.Windows.Forms.Padding(4);
            this.dateEventYearAny.Name = "dateEventYearAny";
            this.dateEventYearAny.Size = new System.Drawing.Size(52, 20);
            this.dateEventYearAny.TabIndex = 14;
            this.dateEventYearAny.Text = "Any";
            this.dateEventYearAny.UseVisualStyleBackColor = true;
            this.dateEventYearAny.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);

            // 
            // dateEventMonthAny
            // 
            this.dateEventMonthAny.AutoSize = true;
            this.dateEventMonthAny.Location = new System.Drawing.Point(271, 113);
            this.dateEventMonthAny.Margin = new System.Windows.Forms.Padding(4);
            this.dateEventMonthAny.Name = "dateEventMonthAny";
            this.dateEventMonthAny.Size = new System.Drawing.Size(52, 20);
            this.dateEventMonthAny.TabIndex = 13;
            this.dateEventMonthAny.Text = "Any";
            this.dateEventMonthAny.UseVisualStyleBackColor = true;
            this.dateEventMonthAny.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // dateEventDayAny
            // 
            this.dateEventDayAny.AutoSize = true;
            this.dateEventDayAny.Location = new System.Drawing.Point(271, 79);
            this.dateEventDayAny.Margin = new System.Windows.Forms.Padding(4);
            this.dateEventDayAny.Name = "dateEventDayAny";
            this.dateEventDayAny.Size = new System.Drawing.Size(52, 20);
            this.dateEventDayAny.TabIndex = 12;
            this.dateEventDayAny.Text = "Any";
            this.dateEventDayAny.UseVisualStyleBackColor = true;
            this.dateEventDayAny.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // dateEventWeekDayAny
            // 
            this.dateEventWeekDayAny.AutoSize = true;
            this.dateEventWeekDayAny.Location = new System.Drawing.Point(271, 43);
            this.dateEventWeekDayAny.Margin = new System.Windows.Forms.Padding(4);
            this.dateEventWeekDayAny.Name = "dateEventWeekDayAny";
            this.dateEventWeekDayAny.Size = new System.Drawing.Size(52, 20);
            this.dateEventWeekDayAny.TabIndex = 11;
            this.dateEventWeekDayAny.Text = "Any";
            this.dateEventWeekDayAny.UseVisualStyleBackColor = true;
            this.dateEventWeekDayAny.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // dateRangeStartDateAny
            // 
            this.dateRangeStartDateAny.AutoSize = true;
            this.dateRangeStartDateAny.Location = new System.Drawing.Point(99, 12);
            this.dateRangeStartDateAny.Margin = new System.Windows.Forms.Padding(4);
            this.dateRangeStartDateAny.Name = "dateRangeStartDateAny";
            this.dateRangeStartDateAny.Size = new System.Drawing.Size(52, 20);
            this.dateRangeStartDateAny.TabIndex = 10;
            this.dateRangeStartDateAny.Text = "Any";
            this.dateRangeStartDateAny.UseVisualStyleBackColor = true;
            this.dateRangeStartDateAny.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // TextBoxPriority
            // 
            this.TextBoxPriority.Location = new System.Drawing.Point(101, 49);
            this.TextBoxPriority.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxPriority.Name = "TextBoxPriority";
            this.TextBoxPriority.Size = new System.Drawing.Size(157, 22);
            this.TextBoxPriority.TabIndex = 5;
            this.TextBoxPriority.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxPriority_KeyPress);
            this.TextBoxPriority.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxPriority_Validating);
            this.TextBoxPriority.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelPriority
            // 
            this.labelPriority.AutoSize = true;
            this.labelPriority.Location = new System.Drawing.Point(9, 53);
            this.labelPriority.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(48, 16);
            this.labelPriority.TabIndex = 4;
            this.labelPriority.Text = "Priority";
            // 
            // comboBoxEventsType
            // 
            this.comboBoxEventsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEventsType.FormattingEnabled = true;
            this.comboBoxEventsType.Items.AddRange(new object[] {
            "Date",
            "Date Range",
            "Week And Day"});
            this.comboBoxEventsType.Location = new System.Drawing.Point(152, 208);
            this.comboBoxEventsType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxEventsType.Name = "comboBoxEventsType";
            this.comboBoxEventsType.Size = new System.Drawing.Size(221, 24);
            this.comboBoxEventsType.TabIndex = 3;
            this.comboBoxEventsType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEventsType_SelectedIndexChanged);
            this.comboBoxEventsType.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);

            // 
            // TextBoxName
            // 
            this.TextBoxName.Location = new System.Drawing.Point(101, 16);
            this.TextBoxName.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxName.Name = "TextBoxName";
            this.TextBoxName.Size = new System.Drawing.Size(160, 22);
            this.TextBoxName.TabIndex = 2;
           // this.TextBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxName_KeyPress);
            this.TextBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxName_Validating_1);
            this.TextBoxName.TextChanged += new EventHandler(BacNetValidator.ControlChanged);

            // 
            // labelEventType
            // 
            this.labelEventType.AutoSize = true;
            this.labelEventType.Location = new System.Drawing.Point(11, 212);
            this.labelEventType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEventType.Name = "labelEventType";
            this.labelEventType.Size = new System.Drawing.Size(76, 16);
            this.labelEventType.TabIndex = 1;
            this.labelEventType.Text = "Event Type";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBoxPriority);
            this.groupBox1.Controls.Add(this.labelPriority);
            this.groupBox1.Controls.Add(this.TextBoxName);
            this.groupBox1.Controls.Add(this.labelName);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(23, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(325, 82);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(8, 20);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(44, 16);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(-1, -80);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 16);
            this.label14.TabIndex = 20;
            this.label14.Text = "Description";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(-5, -117);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 16);
            this.label13.TabIndex = 19;
            this.label13.Text = "Object Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(-5, -246);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Object Identifier";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(-5, -149);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 16);
            this.label12.TabIndex = 18;
            this.label12.Text = "Device Type";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(-5, -212);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 16);
            this.label10.TabIndex = 16;
            this.label10.Text = "Instance Number";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(-5, -180);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Object Type";
            // 
            // groupBoxWeekAndDay
            // 
            this.groupBoxWeekAndDay.Controls.Add(this.ComboBoxMonth);
            this.groupBoxWeekAndDay.Controls.Add(this.labelMonth);
            this.groupBoxWeekAndDay.Controls.Add(this.ComboBoxWeek);
            this.groupBoxWeekAndDay.Controls.Add(this.labelWeek);
            this.groupBoxWeekAndDay.Controls.Add(this.ComboBoxWeekDay);
            this.groupBoxWeekAndDay.Controls.Add(this.labelWeekDay);
            this.groupBoxWeekAndDay.Location = new System.Drawing.Point(91, 481);
            this.groupBoxWeekAndDay.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxWeekAndDay.Name = "groupBoxWeekAndDay";
            this.groupBoxWeekAndDay.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxWeekAndDay.Size = new System.Drawing.Size(325, 174);
            this.groupBoxWeekAndDay.TabIndex = 8;
            this.groupBoxWeekAndDay.TabStop = false;
            this.groupBoxWeekAndDay.Text = "Week and Day";
            // 
            // ComboBoxMonth
            // 
            this.ComboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxMonth.FormattingEnabled = true;
            this.ComboBoxMonth.Items.AddRange(new object[] {
            "Any",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
            "Odd Months",
            "Even Months"});
            this.ComboBoxMonth.Location = new System.Drawing.Point(112, 121);
            this.ComboBoxMonth.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxMonth.Name = "ComboBoxMonth";
            this.ComboBoxMonth.Size = new System.Drawing.Size(183, 24);
            this.ComboBoxMonth.TabIndex = 5;
            this.ComboBoxMonth.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelMonth
            // 
            this.labelMonth.AutoSize = true;
            this.labelMonth.Location = new System.Drawing.Point(9, 126);
            this.labelMonth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMonth.Name = "labelMonth";
            this.labelMonth.Size = new System.Drawing.Size(43, 16);
            this.labelMonth.TabIndex = 4;
            this.labelMonth.Text = "Month";
            // 
            // ComboBoxWeek
            // 
            this.ComboBoxWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxWeek.FormattingEnabled = true;
            this.ComboBoxWeek.Items.AddRange(new object[] {
            "Any",
            "First",
            "Second",
            "Third",
            "Fourth",
            "Fifth",
            "Last 7 days of this month",
            "Any of the 7 days prior to the last 7 days of this month",
            "Any of the 7 days prior to the last 14 days of this month",
            "Any of the 7 days prior to the last 21 days of this month"});
            this.ComboBoxWeek.Location = new System.Drawing.Point(112, 78);
            this.ComboBoxWeek.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxWeek.Name = "ComboBoxWeek";
            this.ComboBoxWeek.Size = new System.Drawing.Size(183, 24);
            this.ComboBoxWeek.TabIndex = 3;
            ComboBoxWeek.DropDownWidth = 270;
            ComboBoxWeek.IntegralHeight = false;
            ComboBoxWeek.MaxDropDownItems = 10;
            this.ComboBoxWeek.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelWeek
            // 
            this.labelWeek.AutoSize = true;
            this.labelWeek.Location = new System.Drawing.Point(9, 82);
            this.labelWeek.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWeek.Name = "labelWeek";
            this.labelWeek.Size = new System.Drawing.Size(43, 16);
            this.labelWeek.TabIndex = 2;
            this.labelWeek.Text = "Week";
            // 
            // ComboBoxWeekDay
            // 
            this.ComboBoxWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxWeekDay.FormattingEnabled = true;
            this.ComboBoxWeekDay.Items.AddRange(new object[] {
            "Any",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.ComboBoxWeekDay.Location = new System.Drawing.Point(112, 37);
            this.ComboBoxWeekDay.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxWeekDay.Name = "ComboBoxWeekDay";
            this.ComboBoxWeekDay.Size = new System.Drawing.Size(183, 24);
            this.ComboBoxWeekDay.TabIndex = 1;
            this.ComboBoxWeekDay.SelectedIndexChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // labelWeekDay
            // 
            this.labelWeekDay.AutoSize = true;
            this.labelWeekDay.Location = new System.Drawing.Point(9, 42);
            this.labelWeekDay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWeekDay.Name = "labelWeekDay";
            this.labelWeekDay.Size = new System.Drawing.Size(71, 16);
            this.labelWeekDay.TabIndex = 0;
            this.labelWeekDay.Text = "Week Day";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(192, 443);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(163, 159);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDescription.MaxLength = 25;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(221, 22);
            this.textBoxDescription.TabIndex = 26;
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            //this.textBoxDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDescription_KeyPress);
            this.textBoxDescription.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // textBoxObjectName
            // 
            this.textBoxObjectName.Location = new System.Drawing.Point(163, 119);
            this.textBoxObjectName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxObjectName.MaxLength = 25;
            this.textBoxObjectName.Name = "textBoxObjectName";
            this.textBoxObjectName.Size = new System.Drawing.Size(221, 22);
            this.textBoxObjectName.TabIndex = 25;
            this.textBoxObjectName.TextChanged += new System.EventHandler(this.textBoxObjectName_TextChanged);
            //this.textBoxObjectName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxObjectName_KeyPress);
            this.textBoxObjectName.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            this.textBoxObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxObjectName_Validating);
            // 
            // textBoxObjectType
            // 
            this.textBoxObjectType.Enabled = false;
            this.textBoxObjectType.Location = new System.Drawing.Point(163, 82);
            this.textBoxObjectType.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxObjectType.Name = "textBoxObjectType";
            this.textBoxObjectType.Size = new System.Drawing.Size(221, 22);
            this.textBoxObjectType.TabIndex = 23;
            // 
            // textBoxInstanceNumber
            // 
            this.textBoxInstanceNumber.Enabled = false;
            this.textBoxInstanceNumber.Location = new System.Drawing.Point(163, 49);
            this.textBoxInstanceNumber.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxInstanceNumber.Name = "textBoxInstanceNumber";
            this.textBoxInstanceNumber.Size = new System.Drawing.Size(221, 22);
            this.textBoxInstanceNumber.TabIndex = 22;
            // 
            // textBoxObjectIdentifier
            // 
            this.textBoxObjectIdentifier.Enabled = false;
            this.textBoxObjectIdentifier.Location = new System.Drawing.Point(163, 15);
            this.textBoxObjectIdentifier.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxObjectIdentifier.Name = "textBoxObjectIdentifier";
            this.textBoxObjectIdentifier.Size = new System.Drawing.Size(221, 22);
            this.textBoxObjectIdentifier.TabIndex = 21;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 162);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(75, 16);
            this.lblDescription.TabIndex = 20;
            this.lblDescription.Text = "Description";
            // 
            // lblObjectName
            // 
            this.lblObjectName.AutoSize = true;
            this.lblObjectName.Location = new System.Drawing.Point(20, 123);
            this.lblObjectName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjectName.Name = "lblObjectName";
            this.lblObjectName.Size = new System.Drawing.Size(86, 16);
            this.lblObjectName.TabIndex = 19;
            this.lblObjectName.Text = "Object Name";
            // 
            // lblObjectType
            // 
            this.lblObjectType.AutoSize = true;
            this.lblObjectType.Location = new System.Drawing.Point(20, 87);
            this.lblObjectType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjectType.Name = "lblObjectType";
            this.lblObjectType.Size = new System.Drawing.Size(81, 16);
            this.lblObjectType.TabIndex = 17;
            this.lblObjectType.Text = "Object Type";
            // 
            // lblInstanceNumber
            // 
            this.lblInstanceNumber.AutoSize = true;
            this.lblInstanceNumber.Location = new System.Drawing.Point(20, 54);
            this.lblInstanceNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstanceNumber.Name = "lblInstanceNumber";
            this.lblInstanceNumber.Size = new System.Drawing.Size(108, 16);
            this.lblInstanceNumber.TabIndex = 16;
            this.lblInstanceNumber.Text = "Instance Number";
            // 
            // lblObjectIdentifier
            // 
            this.lblObjectIdentifier.AutoSize = true;
            this.lblObjectIdentifier.Location = new System.Drawing.Point(20, 18);
            this.lblObjectIdentifier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjectIdentifier.Name = "lblObjectIdentifier";
            this.lblObjectIdentifier.Size = new System.Drawing.Size(99, 16);
            this.lblObjectIdentifier.TabIndex = 15;
            this.lblObjectIdentifier.Text = "Object Identifier";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxEventsType);
            this.groupBox2.Controls.Add(this.labelEventType);
            this.groupBox2.Location = new System.Drawing.Point(11, -5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(385, 266);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            // 
            // groupBoxStartAndEndDate
            // 
            this.groupBoxStartAndEndDate.Controls.Add(this.ButtonAdd);
            this.groupBoxStartAndEndDate.Controls.Add(this.groupBox1);
            this.groupBoxStartAndEndDate.Controls.Add(this.groupBoxStartDate);
            this.groupBoxStartAndEndDate.Controls.Add(this.groupBoxEndDate);
            this.groupBoxStartAndEndDate.Location = new System.Drawing.Point(548, -1);
            this.groupBoxStartAndEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxStartAndEndDate.Name = "groupBoxStartAndEndDate";
            this.groupBoxStartAndEndDate.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxStartAndEndDate.Size = new System.Drawing.Size(361, 590);
            this.groupBoxStartAndEndDate.TabIndex = 28;
            this.groupBoxStartAndEndDate.TabStop = false;
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(127, 539);
            this.ButtonAdd.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(123, 28);
            this.ButtonAdd.TabIndex = 0;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // dataGridViewEventsInfo
            // 
            this.dataGridViewEventsInfo.AllowUserToAddRows = false;
            this.dataGridViewEventsInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEventsInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventType,
            this.EventName,
            this.EventId,
            this.StartDate,
            this.EndDate,
            this.Edit,
            this.Remove});
            this.dataGridViewEventsInfo.Location = new System.Drawing.Point(11, 284);
            this.dataGridViewEventsInfo.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewEventsInfo.Name = "dataGridViewEventsInfo";
            this.dataGridViewEventsInfo.ReadOnly = true;
            this.dataGridViewEventsInfo.RowHeadersWidth = 51;
            this.dataGridViewEventsInfo.Size = new System.Drawing.Size(529, 148);
            this.dataGridViewEventsInfo.TabIndex = 29;
            this.dataGridViewEventsInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEventsInfo_CellContentClick);
            // 
            // EventType
            // 
            this.EventType.Frozen = true;
            this.EventType.HeaderText = "EventType";
            this.EventType.MinimumWidth = 6;
            this.EventType.Name = "EventType";
            this.EventType.ReadOnly = true;
            this.EventType.Width = 60;
            // 
            // EventName
            // 
            this.EventName.Frozen = true;
            this.EventName.HeaderText = "EventName";
            this.EventName.MinimumWidth = 6;
            this.EventName.Name = "EventName";
            this.EventName.ReadOnly = true;
            this.EventName.Width = 70;
            // 
            // EventId
            // 
            this.EventId.Frozen = true;
            this.EventId.HeaderText = "EventId";
            this.EventId.MinimumWidth = 6;
            this.EventId.Name = "EventId";
            this.EventId.ReadOnly = true;
            this.EventId.Visible = false;
            this.EventId.Width = 30;
            // 
            // StartDate
            // 
            this.StartDate.HeaderText = "StartDate";
            this.StartDate.MinimumWidth = 6;
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            this.StartDate.Width = 60;
            // 
            // EndDate
            // 
            this.EndDate.HeaderText = "EndDate";
            this.EndDate.MinimumWidth = 6;
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            this.EndDate.Width = 60;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.MinimumWidth = 6;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Text = "Edit";
            this.Edit.UseColumnTextForButtonValue = true;
            this.Edit.Width = 50;
            // 
            // Remove
            // 
            this.Remove.HeaderText = "Remove";
            this.Remove.MinimumWidth = 6;
            this.Remove.Name = "Remove";
            this.Remove.ReadOnly = true;
            this.Remove.Text = "Remove";
            this.Remove.UseColumnTextForButtonValue = true;
            this.Remove.Width = 50;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FormCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(919, 783);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridViewEventsInfo);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxObjectName);
            this.Controls.Add(this.groupBoxWeekAndDay);
            this.Controls.Add(this.textBoxObjectType);
            this.Controls.Add(this.textBoxInstanceNumber);
            this.Controls.Add(this.textBoxObjectIdentifier);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblObjectName);
            this.Controls.Add(this.lblObjectType);
            this.Controls.Add(this.lblInstanceNumber);
            this.Controls.Add(this.lblObjectIdentifier);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxStartAndEndDate);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormCalendar";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownEndDay)).EndInit();
            this.groupBoxEndDate.ResumeLayout(false);
            this.groupBoxEndDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownStartDay)).EndInit();
            this.groupBoxStartDate.ResumeLayout(false);
            this.groupBoxStartDate.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxWeekAndDay.ResumeLayout(false);
            this.groupBoxWeekAndDay.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxStartAndEndDate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEventsInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DaTimePickerEndSelecDate;
        private System.Windows.Forms.ComboBox ComboBoxEndWeekDay;
        private System.Windows.Forms.Label labelEndWeekDay;
        private System.Windows.Forms.ComboBox ComboBoxEndYear;
        private System.Windows.Forms.NumericUpDown NumericUpDownEndDay;
        private System.Windows.Forms.ComboBox ComboBoxEndMonth;
        private System.Windows.Forms.Label labelEndDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DaTimePickerStartSelecDate;
        private System.Windows.Forms.Label labelStartSelectDate;
        private System.Windows.Forms.ComboBox ComboBoxStartYear;
        private System.Windows.Forms.ComboBox ComboBoxStartMonth;
        private System.Windows.Forms.Label labelStartYear;
        private System.Windows.Forms.Label labelStartMonth;
        private System.Windows.Forms.Label labelStartDay;
        private System.Windows.Forms.NumericUpDown NumericUpDownStartDay;
        private System.Windows.Forms.Label labelStartWeekDay;
        private System.Windows.Forms.ComboBox ComboBoxStartWeekDay;
        private System.Windows.Forms.GroupBox groupBoxStartDate;
        private System.Windows.Forms.TextBox TextBoxPriority;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.ComboBox comboBoxEventsType;
        private System.Windows.Forms.TextBox TextBoxName;
        private System.Windows.Forms.Label labelEventType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.GroupBox groupBoxWeekAndDay;
        private System.Windows.Forms.ComboBox ComboBoxMonth;
        private System.Windows.Forms.Label labelMonth;
        private System.Windows.Forms.ComboBox ComboBoxWeek;
        private System.Windows.Forms.Label labelWeek;
        private System.Windows.Forms.ComboBox ComboBoxWeekDay;
        private System.Windows.Forms.Label labelWeekDay;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxObjectName;
        private System.Windows.Forms.TextBox textBoxObjectType;
        private System.Windows.Forms.TextBox textBoxInstanceNumber;
        private System.Windows.Forms.TextBox textBoxObjectIdentifier;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblObjectName;
        private System.Windows.Forms.Label lblObjectType;
        private System.Windows.Forms.Label lblInstanceNumber;
        private System.Windows.Forms.Label lblObjectIdentifier;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxStartAndEndDate;
        private System.Windows.Forms.DataGridView dataGridViewEventsInfo;
        private System.Windows.Forms.Button ButtonAdd;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventId;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewButtonColumn Remove;
        private System.Windows.Forms.CheckBox dateRangeEndDateAny;
        private System.Windows.Forms.CheckBox dateRangeStartDateAny;
        private System.Windows.Forms.CheckBox dateEventWeekDayAny;
        private System.Windows.Forms.CheckBox dateEventYearAny;
        private System.Windows.Forms.CheckBox dateEventMonthAny;
        private System.Windows.Forms.CheckBox dateEventDayAny;
    }
}