namespace XMPS2000.Bacnet
{
    partial class FormSpecialEvents
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
            this.textBoxPriority = new System.Windows.Forms.TextBox();
            this.labelPriority = new System.Windows.Forms.Label();
            this.comboBoxEventsType = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelType = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.groupBoxStartDate = new System.Windows.Forms.GroupBox();
            this.dateRangeStartDateAny = new System.Windows.Forms.CheckBox();
            this.daTimePickerStartSelecDate = new System.Windows.Forms.DateTimePicker();
            this.labelStartSelectDate = new System.Windows.Forms.Label();
            this.comboBoxStartYear = new System.Windows.Forms.ComboBox();
            this.comboBoxStartMonth = new System.Windows.Forms.ComboBox();
            this.labelStartYear = new System.Windows.Forms.Label();
            this.labelStartMonth = new System.Windows.Forms.Label();
            this.labelStartDay = new System.Windows.Forms.Label();
            this.numericUpDownStartDay = new System.Windows.Forms.NumericUpDown();
            this.labelStartWeekDay = new System.Windows.Forms.Label();
            this.comboBoxStartWeekDay = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxEventValue = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.labelEndTime = new System.Windows.Forms.Label();
            this.comboBoxValue = new System.Windows.Forms.ComboBox();
            this.labelStartTime = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.groupBoxEndDate = new System.Windows.Forms.GroupBox();
            this.dateRangeEndDateAny = new System.Windows.Forms.CheckBox();
            this.daTimePickerEndSelecDate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxEndWeekDay = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelEndWeekDay = new System.Windows.Forms.Label();
            this.comboBoxEndYear = new System.Windows.Forms.ComboBox();
            this.numericUpDownEndDay = new System.Windows.Forms.NumericUpDown();
            this.comboBoxEndMonth = new System.Windows.Forms.ComboBox();
            this.labelEndDay = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewTimeSlots = new System.Windows.Forms.DataGridView();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxWeekAndDay = new System.Windows.Forms.GroupBox();
            this.ComboBoxMonth = new System.Windows.Forms.ComboBox();
            this.labelMonth = new System.Windows.Forms.Label();
            this.ComboBoxWeek = new System.Windows.Forms.ComboBox();
            this.labelWeek = new System.Windows.Forms.Label();
            this.ComboBoxWeekDay = new System.Windows.Forms.ComboBox();
            this.labelWeekDay = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBoxReferences = new System.Windows.Forms.GroupBox();
            this.textBoxCalendarInstaceNo = new System.Windows.Forms.TextBox();
            this.ComboBoxCalendarObjects = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCalendarInstanceNo = new System.Windows.Forms.Label();
            this.dateRangeSDWeekDay = new System.Windows.Forms.CheckBox();
            this.dateRangeSDDay = new System.Windows.Forms.CheckBox();
            this.dateRangeSDMonth = new System.Windows.Forms.CheckBox();
            this.dateRangeSDYear = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxStartDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartDay)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBoxEndDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTimeSlots)).BeginInit();
            this.groupBoxWeekAndDay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBoxReferences.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxPriority);
            this.groupBox1.Controls.Add(this.labelPriority);
            this.groupBox1.Controls.Add(this.comboBoxEventsType);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.labelType);
            this.groupBox1.Controls.Add(this.labelName);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 106);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // textBoxPriority
            // 
            this.textBoxPriority.Location = new System.Drawing.Point(76, 44);
            this.textBoxPriority.Name = "textBoxPriority";
            this.textBoxPriority.Size = new System.Drawing.Size(119, 20);
            this.textBoxPriority.TabIndex = 5;
            this.textBoxPriority.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxPriority_KeyPress);
            this.textBoxPriority.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxPriority_Validating);
            // 
            // labelPriority
            // 
            this.labelPriority.AutoSize = true;
            this.labelPriority.Location = new System.Drawing.Point(7, 52);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(38, 13);
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
            "Week And Day",
            "Calendar Reference"});
            this.comboBoxEventsType.Location = new System.Drawing.Point(76, 74);
            this.comboBoxEventsType.Name = "comboBoxEventsType";
            this.comboBoxEventsType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEventsType.TabIndex = 3;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(76, 16);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(121, 20);
            this.textBoxName.TabIndex = 2;
            this.textBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxName_KeyPress);
            this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxName_Validating);
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(6, 78);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(31, 13);
            this.labelType.TabIndex = 1;
            this.labelType.Text = "Type";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(6, 20);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name";
            // 
            // groupBoxStartDate
            // 
            this.groupBoxStartDate.Controls.Add(this.dateRangeSDYear);
            this.groupBoxStartDate.Controls.Add(this.dateRangeSDMonth);
            this.groupBoxStartDate.Controls.Add(this.dateRangeSDDay);
            this.groupBoxStartDate.Controls.Add(this.dateRangeSDWeekDay);
            this.groupBoxStartDate.Controls.Add(this.dateRangeStartDateAny);
            this.groupBoxStartDate.Controls.Add(this.daTimePickerStartSelecDate);
            this.groupBoxStartDate.Controls.Add(this.labelStartSelectDate);
            this.groupBoxStartDate.Controls.Add(this.comboBoxStartYear);
            this.groupBoxStartDate.Controls.Add(this.comboBoxStartMonth);
            this.groupBoxStartDate.Controls.Add(this.labelStartYear);
            this.groupBoxStartDate.Controls.Add(this.labelStartMonth);
            this.groupBoxStartDate.Controls.Add(this.labelStartDay);
            this.groupBoxStartDate.Controls.Add(this.numericUpDownStartDay);
            this.groupBoxStartDate.Controls.Add(this.labelStartWeekDay);
            this.groupBoxStartDate.Controls.Add(this.comboBoxStartWeekDay);
            this.groupBoxStartDate.Location = new System.Drawing.Point(13, 124);
            this.groupBoxStartDate.Name = "groupBoxStartDate";
            this.groupBoxStartDate.Size = new System.Drawing.Size(244, 177);
            this.groupBoxStartDate.TabIndex = 2;
            this.groupBoxStartDate.TabStop = false;
            this.groupBoxStartDate.Text = "Start Date";
            // 
            // dateRangeStartDateAny
            // 
            this.dateRangeStartDateAny.AutoSize = true;
            this.dateRangeStartDateAny.Location = new System.Drawing.Point(75, 14);
            this.dateRangeStartDateAny.Name = "dateRangeStartDateAny";
            this.dateRangeStartDateAny.Size = new System.Drawing.Size(44, 17);
            this.dateRangeStartDateAny.TabIndex = 10;
            this.dateRangeStartDateAny.Text = "Any";
            this.dateRangeStartDateAny.UseVisualStyleBackColor = true;
            // 
            // daTimePickerStartSelecDate
            // 
            this.daTimePickerStartSelecDate.Location = new System.Drawing.Point(76, 150);
            this.daTimePickerStartSelecDate.Name = "daTimePickerStartSelecDate";
            this.daTimePickerStartSelecDate.Size = new System.Drawing.Size(162, 20);
            this.daTimePickerStartSelecDate.TabIndex = 9;
            this.daTimePickerStartSelecDate.ValueChanged += new System.EventHandler(this.DaTimePickerStartSelecDate_ValueChanged);
            this.daTimePickerStartSelecDate.Validating += new System.ComponentModel.CancelEventHandler(this.DaTimePickerStartSelecDate_Validating);
            // 
            // labelStartSelectDate
            // 
            this.labelStartSelectDate.AutoSize = true;
            this.labelStartSelectDate.Location = new System.Drawing.Point(6, 154);
            this.labelStartSelectDate.Name = "labelStartSelectDate";
            this.labelStartSelectDate.Size = new System.Drawing.Size(63, 13);
            this.labelStartSelectDate.TabIndex = 8;
            this.labelStartSelectDate.Text = "Select Date";
            // 
            // comboBoxStartYear
            // 
            this.comboBoxStartYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartYear.Enabled = false;
            this.comboBoxStartYear.FormattingEnabled = true;
            this.comboBoxStartYear.Location = new System.Drawing.Point(76, 122);
            this.comboBoxStartYear.Name = "comboBoxStartYear";
            this.comboBoxStartYear.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStartYear.TabIndex = 7;
            // 
            // comboBoxStartMonth
            // 
            this.comboBoxStartMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartMonth.Enabled = false;
            this.comboBoxStartMonth.FormattingEnabled = true;
            this.comboBoxStartMonth.Items.AddRange(new object[] {
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
            this.comboBoxStartMonth.Location = new System.Drawing.Point(76, 94);
            this.comboBoxStartMonth.Name = "comboBoxStartMonth";
            this.comboBoxStartMonth.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStartMonth.TabIndex = 6;
            // 
            // labelStartYear
            // 
            this.labelStartYear.AutoSize = true;
            this.labelStartYear.Location = new System.Drawing.Point(6, 126);
            this.labelStartYear.Name = "labelStartYear";
            this.labelStartYear.Size = new System.Drawing.Size(29, 13);
            this.labelStartYear.TabIndex = 5;
            this.labelStartYear.Text = "Year";
            // 
            // labelStartMonth
            // 
            this.labelStartMonth.AutoSize = true;
            this.labelStartMonth.Location = new System.Drawing.Point(6, 98);
            this.labelStartMonth.Name = "labelStartMonth";
            this.labelStartMonth.Size = new System.Drawing.Size(37, 13);
            this.labelStartMonth.TabIndex = 4;
            this.labelStartMonth.Text = "Month";
            // 
            // labelStartDay
            // 
            this.labelStartDay.AutoSize = true;
            this.labelStartDay.Location = new System.Drawing.Point(6, 70);
            this.labelStartDay.Name = "labelStartDay";
            this.labelStartDay.Size = new System.Drawing.Size(26, 13);
            this.labelStartDay.TabIndex = 3;
            this.labelStartDay.Text = "Day";
            // 
            // numericUpDownStartDay
            // 
            this.numericUpDownStartDay.Enabled = false;
            this.numericUpDownStartDay.Location = new System.Drawing.Point(76, 66);
            this.numericUpDownStartDay.Name = "numericUpDownStartDay";
            this.numericUpDownStartDay.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownStartDay.TabIndex = 2;
            // 
            // labelStartWeekDay
            // 
            this.labelStartWeekDay.AutoSize = true;
            this.labelStartWeekDay.Location = new System.Drawing.Point(6, 41);
            this.labelStartWeekDay.Name = "labelStartWeekDay";
            this.labelStartWeekDay.Size = new System.Drawing.Size(58, 13);
            this.labelStartWeekDay.TabIndex = 1;
            this.labelStartWeekDay.Text = "Week Day";
            // 
            // comboBoxStartWeekDay
            // 
            this.comboBoxStartWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartWeekDay.Enabled = false;
            this.comboBoxStartWeekDay.FormattingEnabled = true;
            this.comboBoxStartWeekDay.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.comboBoxStartWeekDay.Location = new System.Drawing.Point(76, 37);
            this.comboBoxStartWeekDay.Name = "comboBoxStartWeekDay";
            this.comboBoxStartWeekDay.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStartWeekDay.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxEventValue);
            this.groupBox3.Controls.Add(this.buttonAdd);
            this.groupBox3.Controls.Add(this.dateTimePickerEndTime);
            this.groupBox3.Controls.Add(this.dateTimePickerStartTime);
            this.groupBox3.Controls.Add(this.labelEndTime);
            this.groupBox3.Controls.Add(this.comboBoxValue);
            this.groupBox3.Controls.Add(this.labelStartTime);
            this.groupBox3.Controls.Add(this.labelValue);
            this.groupBox3.Location = new System.Drawing.Point(263, 11);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(281, 130);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Event Value";
            // 
            // textBoxEventValue
            // 
            this.textBoxEventValue.Location = new System.Drawing.Point(198, 13);
            this.textBoxEventValue.Name = "textBoxEventValue";
            this.textBoxEventValue.Size = new System.Drawing.Size(77, 20);
            this.textBoxEventValue.TabIndex = 7;
            this.textBoxEventValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEventValue_KeyPress);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(182, 104);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(71, 79);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(185, 20);
            this.dateTimePickerEndTime.TabIndex = 5;
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(71, 49);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(185, 20);
            this.dateTimePickerStartTime.TabIndex = 4;
            // 
            // labelEndTime
            // 
            this.labelEndTime.AutoSize = true;
            this.labelEndTime.Location = new System.Drawing.Point(7, 83);
            this.labelEndTime.Name = "labelEndTime";
            this.labelEndTime.Size = new System.Drawing.Size(52, 13);
            this.labelEndTime.TabIndex = 3;
            this.labelEndTime.Text = "End Time";
            // 
            // comboBoxValue
            // 
            this.comboBoxValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxValue.FormattingEnabled = true;
            this.comboBoxValue.Items.AddRange(new object[] {
            "True",
            "False"});
            this.comboBoxValue.Location = new System.Drawing.Point(71, 13);
            this.comboBoxValue.Name = "comboBoxValue";
            this.comboBoxValue.Size = new System.Drawing.Size(121, 21);
            this.comboBoxValue.TabIndex = 2;
            // 
            // labelStartTime
            // 
            this.labelStartTime.AutoSize = true;
            this.labelStartTime.Location = new System.Drawing.Point(7, 53);
            this.labelStartTime.Name = "labelStartTime";
            this.labelStartTime.Size = new System.Drawing.Size(55, 13);
            this.labelStartTime.TabIndex = 1;
            this.labelStartTime.Text = "Start Time";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(7, 17);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(34, 13);
            this.labelValue.TabIndex = 0;
            this.labelValue.Text = "Value";
            // 
            // groupBoxEndDate
            // 
            this.groupBoxEndDate.Controls.Add(this.dateRangeEndDateAny);
            this.groupBoxEndDate.Controls.Add(this.daTimePickerEndSelecDate);
            this.groupBoxEndDate.Controls.Add(this.comboBoxEndWeekDay);
            this.groupBoxEndDate.Controls.Add(this.label1);
            this.groupBoxEndDate.Controls.Add(this.labelEndWeekDay);
            this.groupBoxEndDate.Controls.Add(this.comboBoxEndYear);
            this.groupBoxEndDate.Controls.Add(this.numericUpDownEndDay);
            this.groupBoxEndDate.Controls.Add(this.comboBoxEndMonth);
            this.groupBoxEndDate.Controls.Add(this.labelEndDay);
            this.groupBoxEndDate.Controls.Add(this.label2);
            this.groupBoxEndDate.Controls.Add(this.label3);
            this.groupBoxEndDate.Location = new System.Drawing.Point(12, 306);
            this.groupBoxEndDate.Name = "groupBoxEndDate";
            this.groupBoxEndDate.Size = new System.Drawing.Size(244, 179);
            this.groupBoxEndDate.TabIndex = 4;
            this.groupBoxEndDate.TabStop = false;
            this.groupBoxEndDate.Text = "End Date";
            // 
            // dateRangeEndDateAny
            // 
            this.dateRangeEndDateAny.AutoSize = true;
            this.dateRangeEndDateAny.Location = new System.Drawing.Point(76, 14);
            this.dateRangeEndDateAny.Name = "dateRangeEndDateAny";
            this.dateRangeEndDateAny.Size = new System.Drawing.Size(44, 17);
            this.dateRangeEndDateAny.TabIndex = 11;
            this.dateRangeEndDateAny.Text = "Any";
            this.dateRangeEndDateAny.UseVisualStyleBackColor = true;
            // 
            // daTimePickerEndSelecDate
            // 
            this.daTimePickerEndSelecDate.Location = new System.Drawing.Point(76, 150);
            this.daTimePickerEndSelecDate.Name = "daTimePickerEndSelecDate";
            this.daTimePickerEndSelecDate.Size = new System.Drawing.Size(162, 20);
            this.daTimePickerEndSelecDate.TabIndex = 19;
            this.daTimePickerEndSelecDate.ValueChanged += new System.EventHandler(this.DaTimePickerEndSelecDate_ValueChanged);
            // 
            // comboBoxEndWeekDay
            // 
            this.comboBoxEndWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEndWeekDay.Enabled = false;
            this.comboBoxEndWeekDay.FormattingEnabled = true;
            this.comboBoxEndWeekDay.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.comboBoxEndWeekDay.Location = new System.Drawing.Point(76, 37);
            this.comboBoxEndWeekDay.Name = "comboBoxEndWeekDay";
            this.comboBoxEndWeekDay.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEndWeekDay.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Select Date";
            // 
            // labelEndWeekDay
            // 
            this.labelEndWeekDay.AutoSize = true;
            this.labelEndWeekDay.Location = new System.Drawing.Point(6, 41);
            this.labelEndWeekDay.Name = "labelEndWeekDay";
            this.labelEndWeekDay.Size = new System.Drawing.Size(58, 13);
            this.labelEndWeekDay.TabIndex = 11;
            this.labelEndWeekDay.Text = "Week Day";
            // 
            // comboBoxEndYear
            // 
            this.comboBoxEndYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEndYear.Enabled = false;
            this.comboBoxEndYear.FormattingEnabled = true;
            this.comboBoxEndYear.Location = new System.Drawing.Point(76, 122);
            this.comboBoxEndYear.Name = "comboBoxEndYear";
            this.comboBoxEndYear.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEndYear.TabIndex = 17;
            // 
            // numericUpDownEndDay
            // 
            this.numericUpDownEndDay.Enabled = false;
            this.numericUpDownEndDay.Location = new System.Drawing.Point(76, 66);
            this.numericUpDownEndDay.Name = "numericUpDownEndDay";
            this.numericUpDownEndDay.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownEndDay.TabIndex = 12;
            // 
            // comboBoxEndMonth
            // 
            this.comboBoxEndMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEndMonth.Enabled = false;
            this.comboBoxEndMonth.FormattingEnabled = true;
            this.comboBoxEndMonth.Items.AddRange(new object[] {
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
            this.comboBoxEndMonth.Location = new System.Drawing.Point(76, 94);
            this.comboBoxEndMonth.Name = "comboBoxEndMonth";
            this.comboBoxEndMonth.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEndMonth.TabIndex = 16;
            // 
            // labelEndDay
            // 
            this.labelEndDay.AutoSize = true;
            this.labelEndDay.Location = new System.Drawing.Point(6, 70);
            this.labelEndDay.Name = "labelEndDay";
            this.labelEndDay.Size = new System.Drawing.Size(26, 13);
            this.labelEndDay.TabIndex = 13;
            this.labelEndDay.Text = "Day";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Year";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Month";
            // 
            // dataGridViewTimeSlots
            // 
            this.dataGridViewTimeSlots.AllowUserToAddRows = false;
            this.dataGridViewTimeSlots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTimeSlots.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StartTime,
            this.EndTime,
            this.Value,
            this.Delete});
            this.dataGridViewTimeSlots.Location = new System.Drawing.Point(263, 148);
            this.dataGridViewTimeSlots.Name = "dataGridViewTimeSlots";
            this.dataGridViewTimeSlots.Size = new System.Drawing.Size(444, 150);
            this.dataGridViewTimeSlots.TabIndex = 5;
            this.dataGridViewTimeSlots.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewTimeSlots_CellClick);
            this.dataGridViewTimeSlots.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewTimeSlots_CellContentClick);
            // 
            // StartTime
            // 
            this.StartTime.HeaderText = "StartTime";
            this.StartTime.Name = "StartTime";
            // 
            // EndTime
            // 
            this.EndTime.HeaderText = "EndTime";
            this.EndTime.Name = "EndTime";
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.Text = "Delete";
            this.Delete.UseColumnTextForButtonValue = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(526, 304);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(95, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // groupBoxWeekAndDay
            // 
            this.groupBoxWeekAndDay.Controls.Add(this.ComboBoxMonth);
            this.groupBoxWeekAndDay.Controls.Add(this.labelMonth);
            this.groupBoxWeekAndDay.Controls.Add(this.ComboBoxWeek);
            this.groupBoxWeekAndDay.Controls.Add(this.labelWeek);
            this.groupBoxWeekAndDay.Controls.Add(this.ComboBoxWeekDay);
            this.groupBoxWeekAndDay.Controls.Add(this.labelWeekDay);
            this.groupBoxWeekAndDay.Location = new System.Drawing.Point(263, 318);
            this.groupBoxWeekAndDay.Name = "groupBoxWeekAndDay";
            this.groupBoxWeekAndDay.Size = new System.Drawing.Size(244, 113);
            this.groupBoxWeekAndDay.TabIndex = 7;
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
    "Even Months"
});
            this.ComboBoxMonth.Location = new System.Drawing.Point(84, 76);
            this.ComboBoxMonth.Name = "ComboBoxMonth";
            this.ComboBoxMonth.Size = new System.Drawing.Size(138, 21);
            this.ComboBoxMonth.TabIndex = 5;
            // 
            // labelMonth
            // 
            this.labelMonth.AutoSize = true;
            this.labelMonth.Location = new System.Drawing.Point(7, 80);
            this.labelMonth.Name = "labelMonth";
            this.labelMonth.Size = new System.Drawing.Size(37, 13);
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
            this.ComboBoxWeek.Location = new System.Drawing.Point(84, 49);
            this.ComboBoxWeek.Name = "ComboBoxWeek";
            this.ComboBoxWeek.Size = new System.Drawing.Size(138, 21);
            this.ComboBoxWeek.TabIndex = 3;
            ComboBoxWeek.DropDownWidth = 270;
            ComboBoxWeek.IntegralHeight = false;
            ComboBoxWeek.MaxDropDownItems = 10;
            // 
            // labelWeek
            // 
            this.labelWeek.AutoSize = true;
            this.labelWeek.Location = new System.Drawing.Point(7, 53);
            this.labelWeek.Name = "labelWeek";
            this.labelWeek.Size = new System.Drawing.Size(36, 13);
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
            this.ComboBoxWeekDay.Location = new System.Drawing.Point(84, 22);
            this.ComboBoxWeekDay.Name = "ComboBoxWeekDay";
            this.ComboBoxWeekDay.Size = new System.Drawing.Size(138, 21);
            this.ComboBoxWeekDay.TabIndex = 1;
            // 
            // labelWeekDay
            // 
            this.labelWeekDay.AutoSize = true;
            this.labelWeekDay.Location = new System.Drawing.Point(7, 26);
            this.labelWeekDay.Name = "labelWeekDay";
            this.labelWeekDay.Size = new System.Drawing.Size(58, 13);
            this.labelWeekDay.TabIndex = 0;
            this.labelWeekDay.Text = "Week Day";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(627, 304);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // groupBoxReferences
            // 
            this.groupBoxReferences.Controls.Add(this.textBoxCalendarInstaceNo);
            this.groupBoxReferences.Controls.Add(this.ComboBoxCalendarObjects);
            this.groupBoxReferences.Controls.Add(this.label4);
            this.groupBoxReferences.Controls.Add(this.labelCalendarInstanceNo);
            this.groupBoxReferences.Location = new System.Drawing.Point(513, 351);
            this.groupBoxReferences.Name = "groupBoxReferences";
            this.groupBoxReferences.Size = new System.Drawing.Size(206, 76);
            this.groupBoxReferences.TabIndex = 9;
            this.groupBoxReferences.TabStop = false;
            this.groupBoxReferences.Text = "References";
            // 
            // textBoxCalendarInstaceNo
            // 
            this.textBoxCalendarInstaceNo.Enabled = false;
            this.textBoxCalendarInstaceNo.Location = new System.Drawing.Point(147, 37);
            this.textBoxCalendarInstaceNo.Name = "textBoxCalendarInstaceNo";
            this.textBoxCalendarInstaceNo.Size = new System.Drawing.Size(56, 20);
            this.textBoxCalendarInstaceNo.TabIndex = 3;
            // 
            // ComboBoxCalendarObjects
            // 
            this.ComboBoxCalendarObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCalendarObjects.FormattingEnabled = true;
            this.ComboBoxCalendarObjects.Location = new System.Drawing.Point(6, 37);
            this.ComboBoxCalendarObjects.Name = "ComboBoxCalendarObjects";
            this.ComboBoxCalendarObjects.Size = new System.Drawing.Size(113, 21);
            this.ComboBoxCalendarObjects.TabIndex = 2;
            this.ComboBoxCalendarObjects.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCalendarObjects_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Instance No";
            // 
            // labelCalendarInstanceNo
            // 
            this.labelCalendarInstanceNo.AutoSize = true;
            this.labelCalendarInstanceNo.Location = new System.Drawing.Point(3, 20);
            this.labelCalendarInstanceNo.Name = "labelCalendarInstanceNo";
            this.labelCalendarInstanceNo.Size = new System.Drawing.Size(88, 13);
            this.labelCalendarInstanceNo.TabIndex = 0;
            this.labelCalendarInstanceNo.Text = "Calendar Objects";
            // 
            // dateRangeSDWeekDay
            // 
            this.dateRangeSDWeekDay.AutoSize = true;
            this.dateRangeSDWeekDay.Location = new System.Drawing.Point(198, 39);
            this.dateRangeSDWeekDay.Name = "dateRangeSDWeekDay";
            this.dateRangeSDWeekDay.Size = new System.Drawing.Size(44, 17);
            this.dateRangeSDWeekDay.TabIndex = 11;
            this.dateRangeSDWeekDay.Text = "Any";
            this.dateRangeSDWeekDay.UseVisualStyleBackColor = true;
            // 
            // dateRangeSDDay
            // 
            this.dateRangeSDDay.AutoSize = true;
            this.dateRangeSDDay.Location = new System.Drawing.Point(198, 68);
            this.dateRangeSDDay.Name = "dateRangeSDDay";
            this.dateRangeSDDay.Size = new System.Drawing.Size(44, 17);
            this.dateRangeSDDay.TabIndex = 12;
            this.dateRangeSDDay.Text = "Any";
            this.dateRangeSDDay.UseVisualStyleBackColor = true;
            // 
            // dateRangeSDMonth
            // 
            this.dateRangeSDMonth.AutoSize = true;
            this.dateRangeSDMonth.Location = new System.Drawing.Point(198, 96);
            this.dateRangeSDMonth.Name = "dateRangeSDMonth";
            this.dateRangeSDMonth.Size = new System.Drawing.Size(44, 17);
            this.dateRangeSDMonth.TabIndex = 13;
            this.dateRangeSDMonth.Text = "Any";
            this.dateRangeSDMonth.UseVisualStyleBackColor = true;
            // 
            // dateRangeSDYear
            // 
            this.dateRangeSDYear.AutoSize = true;
            this.dateRangeSDYear.Location = new System.Drawing.Point(198, 124);
            this.dateRangeSDYear.Name = "dateRangeSDYear";
            this.dateRangeSDYear.Size = new System.Drawing.Size(44, 17);
            this.dateRangeSDYear.TabIndex = 14;
            this.dateRangeSDYear.Text = "Any";
            this.dateRangeSDYear.UseVisualStyleBackColor = true;
            // 
            // FormSpecialEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(720, 497);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxReferences);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBoxWeekAndDay);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dataGridViewTimeSlots);
            this.Controls.Add(this.groupBoxEndDate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxStartDate);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSpecialEvents";
            this.ShowIcon = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxStartDate.ResumeLayout(false);
            this.groupBoxStartDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartDay)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxEndDate.ResumeLayout(false);
            this.groupBoxEndDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTimeSlots)).EndInit();
            this.groupBoxWeekAndDay.ResumeLayout(false);
            this.groupBoxWeekAndDay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBoxReferences.ResumeLayout(false);
            this.groupBoxReferences.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxEventsType;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.GroupBox groupBoxStartDate;
        private System.Windows.Forms.NumericUpDown numericUpDownStartDay;
        private System.Windows.Forms.Label labelStartWeekDay;
        private System.Windows.Forms.ComboBox comboBoxStartWeekDay;
        private System.Windows.Forms.Label labelStartSelectDate;
        private System.Windows.Forms.ComboBox comboBoxStartYear;
        private System.Windows.Forms.ComboBox comboBoxStartMonth;
        private System.Windows.Forms.Label labelStartYear;
        private System.Windows.Forms.Label labelStartMonth;
        private System.Windows.Forms.Label labelStartDay;
        private System.Windows.Forms.DateTimePicker daTimePickerStartSelecDate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.Label labelEndTime;
        private System.Windows.Forms.ComboBox comboBoxValue;
        private System.Windows.Forms.Label labelStartTime;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.GroupBox groupBoxEndDate;
        private System.Windows.Forms.DateTimePicker daTimePickerEndSelecDate;
        private System.Windows.Forms.ComboBox comboBoxEndWeekDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelEndWeekDay;
        private System.Windows.Forms.ComboBox comboBoxEndYear;
        private System.Windows.Forms.NumericUpDown numericUpDownEndDay;
        private System.Windows.Forms.ComboBox comboBoxEndMonth;
        private System.Windows.Forms.Label labelEndDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewTimeSlots;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.GroupBox groupBoxWeekAndDay;
        private System.Windows.Forms.Label labelWeekDay;
        private System.Windows.Forms.ComboBox ComboBoxMonth;
        private System.Windows.Forms.Label labelMonth;
        private System.Windows.Forms.ComboBox ComboBoxWeek;
        private System.Windows.Forms.Label labelWeek;
        private System.Windows.Forms.ComboBox ComboBoxWeekDay;
        private System.Windows.Forms.TextBox textBoxPriority;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBoxReferences;
        private System.Windows.Forms.TextBox textBoxCalendarInstaceNo;
        private System.Windows.Forms.ComboBox ComboBoxCalendarObjects;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCalendarInstanceNo;
        private System.Windows.Forms.TextBox textBoxEventValue;
        private System.Windows.Forms.CheckBox dateRangeStartDateAny;
        private System.Windows.Forms.CheckBox dateRangeEndDateAny;
        private System.Windows.Forms.CheckBox dateRangeSDYear;
        private System.Windows.Forms.CheckBox dateRangeSDMonth;
        private System.Windows.Forms.CheckBox dateRangeSDDay;
        private System.Windows.Forms.CheckBox dateRangeSDWeekDay;
    }
}