namespace XMPS2000
{
    partial class RtcForm
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
            this.Setup = new System.Windows.Forms.GroupBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.UploadRtcBtn = new System.Windows.Forms.Button();
            this.dtpickerTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.Date_label = new System.Windows.Forms.Label();
            this.dtpickerDate = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Setup.SuspendLayout();
            this.SuspendLayout();
            // 
            // Setup
            // 
            this.Setup.Controls.Add(this.CancelBtn);
            this.Setup.Controls.Add(this.UploadRtcBtn);
            this.Setup.Controls.Add(this.dtpickerTime);
            this.Setup.Controls.Add(this.label1);
            this.Setup.Controls.Add(this.Date_label);
            this.Setup.Controls.Add(this.dtpickerDate);
            this.Setup.Controls.Add(this.checkBox1);
            this.Setup.Location = new System.Drawing.Point(3, 4);
            this.Setup.Margin = new System.Windows.Forms.Padding(2);
            this.Setup.Name = "Setup";
            this.Setup.Padding = new System.Windows.Forms.Padding(2);
            this.Setup.Size = new System.Drawing.Size(253, 131);
            this.Setup.TabIndex = 1;
            this.Setup.TabStop = false;
            this.Setup.Text = "Setup";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(142, 97);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(94, 28);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // UploadRtcBtn
            // 
            this.UploadRtcBtn.Location = new System.Drawing.Point(14, 98);
            this.UploadRtcBtn.Margin = new System.Windows.Forms.Padding(2);
            this.UploadRtcBtn.Name = "UploadRtcBtn";
            this.UploadRtcBtn.Size = new System.Drawing.Size(100, 27);
            this.UploadRtcBtn.TabIndex = 5;
            this.UploadRtcBtn.Text = "Update PLC time";
            this.UploadRtcBtn.UseVisualStyleBackColor = true;
            this.UploadRtcBtn.Click += new System.EventHandler(this.UploadRtcBtn_Click);
            // 
            // dtpickerTime
            // 
            this.dtpickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpickerTime.Location = new System.Drawing.Point(71, 66);
            this.dtpickerTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtpickerTime.Name = "dtpickerTime";
            this.dtpickerTime.ShowUpDown = true;
            this.dtpickerTime.Size = new System.Drawing.Size(165, 20);
            this.dtpickerTime.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Time:";
            // 
            // Date_label
            // 
            this.Date_label.AutoSize = true;
            this.Date_label.Location = new System.Drawing.Point(12, 42);
            this.Date_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Date_label.Name = "Date_label";
            this.Date_label.Size = new System.Drawing.Size(33, 13);
            this.Date_label.TabIndex = 2;
            this.Date_label.Text = "Date:";
            // 
            // dtpickerDate
            // 
            this.dtpickerDate.Location = new System.Drawing.Point(71, 38);
            this.dtpickerDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpickerDate.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtpickerDate.Name = "dtpickerDate";
            this.dtpickerDate.Size = new System.Drawing.Size(165, 20);
            this.dtpickerDate.TabIndex = 1;
            this.dtpickerDate.Value = new System.DateTime(2023, 2, 15, 0, 0, 0, 0);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 17);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Apply PC time";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // RtcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(267, 146);
            this.ControlBox = false;
            this.Controls.Add(this.Setup);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RtcForm";
            this.Text = "RTC Date & Time Update";
            this.Setup.ResumeLayout(false);
            this.Setup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Setup;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button UploadRtcBtn;
        private System.Windows.Forms.DateTimePicker dtpickerTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Date_label;
        private System.Windows.Forms.DateTimePicker dtpickerDate;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}