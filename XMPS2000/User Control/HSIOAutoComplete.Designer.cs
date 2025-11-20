namespace XMPS2000.User_Control
{
    partial class HSIOAutoComplete
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelOperandType = new System.Windows.Forms.Label();
            this.comboBoxOperandType = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelTagName = new System.Windows.Forms.Label();
            this.LabelAddTag = new System.Windows.Forms.Label();
            this.GroupBoxTagList = new System.Windows.Forms.GroupBox();
            this.HsioListBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.GroupBoxTagList.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelOperandType);
            this.groupBox1.Controls.Add(this.comboBoxOperandType);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.labelTagName);
            this.groupBox1.Controls.Add(this.LabelAddTag);
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // labelOperandType
            // 
            this.labelOperandType.AutoSize = true;
            this.labelOperandType.Location = new System.Drawing.Point(23, 26);
            this.labelOperandType.Name = "labelOperandType";
            this.labelOperandType.Size = new System.Drawing.Size(75, 13);
            this.labelOperandType.TabIndex = 4;
            this.labelOperandType.Text = "Operand Type";
            // 
            // comboBoxOperandType
            // 
            this.comboBoxOperandType.FormattingEnabled = true;
            this.comboBoxOperandType.Items.AddRange(new object[] {
            "Normal Operand",
            "Negation Operand",
            "Numeric Operand"});
            this.comboBoxOperandType.Location = new System.Drawing.Point(110, 22);
            this.comboBoxOperandType.Name = "comboBoxOperandType";
            this.comboBoxOperandType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOperandType.TabIndex = 3;
            this.comboBoxOperandType.SelectedIndexChanged += new System.EventHandler(this.comboBoxOperandType_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(110, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            this.textBox1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // labelTagName
            // 
            this.labelTagName.AutoSize = true;
            this.labelTagName.Location = new System.Drawing.Point(23, 59);
            this.labelTagName.Name = "labelTagName";
            this.labelTagName.Size = new System.Drawing.Size(57, 13);
            this.labelTagName.TabIndex = 1;
            this.labelTagName.Text = "Tag Name";
            // 
            // LabelAddTag
            // 
            this.LabelAddTag.AutoSize = true;
            this.LabelAddTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelAddTag.Location = new System.Drawing.Point(259, 58);
            this.LabelAddTag.Name = "LabelAddTag";
            this.LabelAddTag.Size = new System.Drawing.Size(50, 15);
            this.LabelAddTag.TabIndex = 0;
            this.LabelAddTag.Text = "Add Tag";
            this.LabelAddTag.Click += new System.EventHandler(this.label1_Click);
            // 
            // GroupBoxTagList
            // 
            this.GroupBoxTagList.Controls.Add(this.HsioListBox);
            this.GroupBoxTagList.Location = new System.Drawing.Point(12, 107);
            this.GroupBoxTagList.Name = "GroupBoxTagList";
            this.GroupBoxTagList.Size = new System.Drawing.Size(347, 146);
            this.GroupBoxTagList.TabIndex = 1;
            this.GroupBoxTagList.TabStop = false;
            this.GroupBoxTagList.Text = "Select tag from list below";
            // 
            // HsioListBox
            // 
            this.HsioListBox.FormattingEnabled = true;
            this.HsioListBox.Location = new System.Drawing.Point(14, 16);
            this.HsioListBox.Name = "HsioListBox";
            this.HsioListBox.Size = new System.Drawing.Size(243, 121);
            this.HsioListBox.TabIndex = 0;
            this.HsioListBox.Click += new System.EventHandler(this.listBox1_Click);
            this.HsioListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
            // 
            // HSIOAutoComplete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 263);
            this.Controls.Add(this.GroupBoxTagList);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HSIOAutoComplete";
            this.ShowIcon = false;
            this.Text = "Select or Add New Tag";
            this.Load += new System.EventHandler(this.HSIOAutoComplete_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GroupBoxTagList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelTagName;
        private System.Windows.Forms.Label LabelAddTag;
        private System.Windows.Forms.GroupBox GroupBoxTagList;
        private System.Windows.Forms.ListBox HsioListBox;
        private System.Windows.Forms.Label labelOperandType;
        private System.Windows.Forms.ComboBox comboBoxOperandType;
    }
}