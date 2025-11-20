namespace XMPS2000.Bacnet
{
    partial class CustomCalendar
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
            this.gridViewCalendar = new System.Windows.Forms.DataGridView();
            this.btnsave = new System.Windows.Forms.Button();
            this.comboBoxEventValue = new System.Windows.Forms.ComboBox();
            this.labelValue = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.textBoxSelectedDays = new System.Windows.Forms.TextBox();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.EndTimePicker = new System.Windows.Forms.DateTimePicker();
            this.StartTimePicker = new System.Windows.Forms.DateTimePicker();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.datagridSelectedDays = new System.Windows.Forms.DataGridView();
            this.Day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RectangleData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectedData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.groupBoxSave = new System.Windows.Forms.GroupBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCalendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridSelectedDays)).BeginInit();
            this.groupBoxSave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewCalendar
            // 
            this.gridViewCalendar.AllowUserToAddRows = false;
            this.gridViewCalendar.AllowUserToResizeColumns = false;
            this.gridViewCalendar.AllowUserToResizeRows = false;
            this.gridViewCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewCalendar.Location = new System.Drawing.Point(13, 13);
            this.gridViewCalendar.Name = "gridViewCalendar";
            this.gridViewCalendar.ReadOnly = true;
            this.gridViewCalendar.RowHeadersWidth = 51;
            this.gridViewCalendar.Size = new System.Drawing.Size(704, 551);
            this.gridViewCalendar.TabIndex = 0;
            this.gridViewCalendar.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridViewCalendar_CellMouseUp);
            this.gridViewCalendar.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridViewCalendar_CellPainting);
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(184, 111);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(75, 23);
            this.btnsave.TabIndex = 7;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // comboBoxEventValue
            // 
            this.comboBoxEventValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEventValue.FormattingEnabled = true;
            this.comboBoxEventValue.Items.AddRange(new object[] {
            "True",
            "False"});
            this.comboBoxEventValue.Location = new System.Drawing.Point(733, 214);
            this.comboBoxEventValue.Name = "comboBoxEventValue";
            this.comboBoxEventValue.Size = new System.Drawing.Size(103, 21);
            this.comboBoxEventValue.TabIndex = 6;
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(729, 200);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(34, 13);
            this.labelValue.TabIndex = 5;
            this.labelValue.Text = "Value";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(150, 71);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(26, 13);
            this.lblValue.TabIndex = 2;
            this.lblValue.Text = "Day";
            // 
            // textBoxSelectedDays
            // 
            this.textBoxSelectedDays.Location = new System.Drawing.Point(184, 67);
            this.textBoxSelectedDays.Name = "textBoxSelectedDays";
            this.textBoxSelectedDays.ReadOnly = true;
            this.textBoxSelectedDays.Size = new System.Drawing.Size(107, 20);
            this.textBoxSelectedDays.TabIndex = 1;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(728, 287);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(49, 13);
            this.lblEndTime.TabIndex = 3;
            this.lblEndTime.Text = "EndTime";
            // 
            // EndTimePicker
            // 
            this.EndTimePicker.Location = new System.Drawing.Point(732, 303);
            this.EndTimePicker.Name = "EndTimePicker";
            this.EndTimePicker.Size = new System.Drawing.Size(132, 20);
            this.EndTimePicker.TabIndex = 2;
            this.EndTimePicker.ValueChanged += new System.EventHandler(this.EndTimePicker_ValueChanged);
            // 
            // StartTimePicker
            // 
            this.StartTimePicker.Location = new System.Drawing.Point(733, 259);
            this.StartTimePicker.Name = "StartTimePicker";
            this.StartTimePicker.Size = new System.Drawing.Size(131, 20);
            this.StartTimePicker.TabIndex = 1;
            this.StartTimePicker.ValueChanged += new System.EventHandler(this.StartTimePicker_ValueChanged);
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(731, 241);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(52, 13);
            this.lblStartTime.TabIndex = 0;
            this.lblStartTime.Text = "StartTime";
            // 
            // datagridSelectedDays
            // 
            this.datagridSelectedDays.AllowUserToAddRows = false;
            this.datagridSelectedDays.AllowUserToResizeColumns = false;
            this.datagridSelectedDays.AllowUserToResizeRows = false;
            this.datagridSelectedDays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridSelectedDays.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Day,
            this.StartDate,
            this.EndDate,
            this.Value,
            this.RectangleData,
            this.SelectedData,
            this.deleteButton});
            this.datagridSelectedDays.Location = new System.Drawing.Point(723, 12);
            this.datagridSelectedDays.Name = "datagridSelectedDays";
            this.datagridSelectedDays.ReadOnly = true;
            this.datagridSelectedDays.RowHeadersWidth = 51;
            this.datagridSelectedDays.Size = new System.Drawing.Size(392, 179);
            this.datagridSelectedDays.TabIndex = 2;
            this.datagridSelectedDays.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridSelectedDays_CellClick);
            // 
            // Day
            // 
            this.Day.Frozen = true;
            this.Day.HeaderText = "Day";
            this.Day.MinimumWidth = 6;
            this.Day.Name = "Day";
            this.Day.ReadOnly = true;
            this.Day.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Day.Width = 80;
            // 
            // StartDate
            // 
            this.StartDate.Frozen = true;
            this.StartDate.HeaderText = "StartTime";
            this.StartDate.MinimumWidth = 6;
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            this.StartDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StartDate.Width = 80;
            // 
            // EndDate
            // 
            this.EndDate.Frozen = true;
            this.EndDate.HeaderText = "EndTime";
            this.EndDate.MinimumWidth = 6;
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            this.EndDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EndDate.Width = 80;
            // 
            // Value
            // 
            this.Value.Frozen = true;
            this.Value.HeaderText = "Value";
            this.Value.MinimumWidth = 6;
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Value.Width = 50;
            // 
            // RectangleData
            // 
            this.RectangleData.HeaderText = "RectangleData";
            this.RectangleData.MinimumWidth = 6;
            this.RectangleData.Name = "RectangleData";
            this.RectangleData.ReadOnly = true;
            this.RectangleData.Visible = false;
            this.RectangleData.Width = 125;
            // 
            // SelectedData
            // 
            this.SelectedData.HeaderText = "SelectedData";
            this.SelectedData.MinimumWidth = 6;
            this.SelectedData.Name = "SelectedData";
            this.SelectedData.ReadOnly = true;
            this.SelectedData.Visible = false;
            this.SelectedData.Width = 125;
            // 
            // deleteButton
            // 
            this.deleteButton.Frozen = true;
            this.deleteButton.HeaderText = "Delete";
            this.deleteButton.MinimumWidth = 6;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.ReadOnly = true;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseColumnTextForButtonValue = true;
            this.deleteButton.Width = 50;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(741, 368);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(95, 23);
            this.btnSaveAndClose.TabIndex = 8;
            this.btnSaveAndClose.Text = "Save";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // groupBoxSave
            // 
            this.groupBoxSave.Controls.Add(this.lblValue);
            this.groupBoxSave.Controls.Add(this.textBoxValue);
            this.groupBoxSave.Controls.Add(this.btnsave);
            this.groupBoxSave.Controls.Add(this.textBoxSelectedDays);
            this.groupBoxSave.Location = new System.Drawing.Point(724, 192);
            this.groupBoxSave.Name = "groupBoxSave";
            this.groupBoxSave.Size = new System.Drawing.Size(294, 161);
            this.groupBoxSave.TabIndex = 9;
            this.groupBoxSave.TabStop = false;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(10, 22);
            this.textBoxValue.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(102, 20);
            this.textBoxValue.TabIndex = 26;
            this.textBoxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxValue_KeyPress);
            this.textBoxValue.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxValue_Validating);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(888, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CustomCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 566);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.datagridSelectedDays);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.EndTimePicker);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.comboBoxEventValue);
            this.Controls.Add(this.StartTimePicker);
            this.Controls.Add(this.gridViewCalendar);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.groupBoxSave);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomCalendar";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.CustomCalendar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCalendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridSelectedDays)).EndInit();
            this.groupBoxSave.ResumeLayout(false);
            this.groupBoxSave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridViewCalendar;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox textBoxSelectedDays;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.DateTimePicker EndTimePicker;
        private System.Windows.Forms.DateTimePicker StartTimePicker;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.ComboBox comboBoxEventValue;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.DataGridView datagridSelectedDays;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.GroupBox groupBoxSave;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn Day;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn RectangleData;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectedData;
        private System.Windows.Forms.DataGridViewButtonColumn deleteButton;
        private System.Windows.Forms.Button btnCancel;
    }
}