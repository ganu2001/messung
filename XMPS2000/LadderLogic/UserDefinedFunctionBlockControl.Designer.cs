namespace XMPS2000.LadderLogic
{
    partial class UserDefinedFunctionBlockControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddFB = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.GvUDFB = new System.Windows.Forms.DataGridView();
            this.clmSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textText = new System.Windows.Forms.TextBox();
            this.lblcontroltext = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDataType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textboxOutput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textUDFBName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvUDFB)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddFB);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.textboxOutput);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textboxInput);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textUDFBName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 437);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnAddFB
            // 
            this.btnAddFB.BackColor = System.Drawing.Color.Transparent;
            this.btnAddFB.Location = new System.Drawing.Point(392, 408);
            this.btnAddFB.Name = "btnAddFB";
            this.btnAddFB.Size = new System.Drawing.Size(134, 23);
            this.btnAddFB.TabIndex = 10;
            this.btnAddFB.Text = "Add UDFB";
            this.btnAddFB.UseVisualStyleBackColor = false;
            this.btnAddFB.Click += new System.EventHandler(this.btnAddFB_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboType);
            this.groupBox2.Controls.Add(this.GvUDFB);
            this.groupBox2.Controls.Add(this.buttonDel);
            this.groupBox2.Controls.Add(this.buttonAdd);
            this.groupBox2.Controls.Add(this.textText);
            this.groupBox2.Controls.Add(this.lblcontroltext);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comboBoxDataType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(9, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(529, 334);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Details";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(88, 19);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(151, 21);
            this.cboType.TabIndex = 4;
            // 
            // GvUDFB
            // 
            this.GvUDFB.AllowUserToAddRows = false;
            this.GvUDFB.AllowUserToDeleteRows = false;
            this.GvUDFB.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GvUDFB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvUDFB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSelect});
            this.GvUDFB.Location = new System.Drawing.Point(9, 82);
            this.GvUDFB.Name = "GvUDFB";
            this.GvUDFB.RowHeadersWidth = 51;
            this.GvUDFB.RowTemplate.Height = 24;
            this.GvUDFB.Size = new System.Drawing.Size(508, 246);
            this.GvUDFB.TabIndex = 9;
            this.GvUDFB.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvUDFB_CellDoubleClick);
            this.GvUDFB.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GvUDFB_DataError);
            // 
            // Select
            // 
            this.clmSelect.HeaderText = "Select";
            this.clmSelect.Name = "Select";
            this.clmSelect.Width = 50;
            // 
            // buttonDel
            // 
            this.buttonDel.BackColor = System.Drawing.Color.Transparent;
            this.buttonDel.Location = new System.Drawing.Point(419, 53);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(98, 23);
            this.buttonDel.TabIndex = 8;
            this.buttonDel.Text = "Delete";
            this.buttonDel.UseVisualStyleBackColor = false;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.Transparent;
            this.buttonAdd.Location = new System.Drawing.Point(285, 53);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(98, 23);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textText
            // 
            this.textText.Location = new System.Drawing.Point(88, 46);
            this.textText.Name = "textText";
            this.textText.Size = new System.Drawing.Size(151, 20);
            this.textText.TabIndex = 6;
            this.textText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textText_KeyPress);
            // 
            // lblcontroltext
            // 
            this.lblcontroltext.AutoSize = true;
            this.lblcontroltext.Location = new System.Drawing.Point(6, 53);
            this.lblcontroltext.Name = "lblcontroltext";
            this.lblcontroltext.Size = new System.Drawing.Size(76, 13);
            this.lblcontroltext.TabIndex = 14;
            this.lblcontroltext.Text = "Variable Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 22);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Data Type";
            // 
            // comboBoxDataType
            // 
            this.comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataType.FormattingEnabled = true;
            this.comboBoxDataType.Location = new System.Drawing.Point(366, 19);
            this.comboBoxDataType.Name = "comboBoxDataType";
            this.comboBoxDataType.Size = new System.Drawing.Size(151, 21);
            this.comboBoxDataType.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Type";
            // 
            // textboxOutput
            // 
            this.textboxOutput.Location = new System.Drawing.Point(473, 39);
            this.textboxOutput.MaxLength = 15;
            this.textboxOutput.Name = "textboxOutput";
            this.textboxOutput.Size = new System.Drawing.Size(53, 20);
            this.textboxOutput.TabIndex = 3;
            this.textboxOutput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textboxOutput_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "No. of Outputs";
            // 
            // textboxInput
            // 
            this.textboxInput.Location = new System.Drawing.Point(113, 39);
            this.textboxInput.MaxLength = 15;
            this.textboxInput.Name = "textboxInput";
            this.textboxInput.Size = new System.Drawing.Size(53, 20);
            this.textboxInput.TabIndex = 2;
            this.textboxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textboxInput_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "No. of Inputs";
            // 
            // textUDFBName
            // 
            this.textUDFBName.Location = new System.Drawing.Point(113, 16);
            this.textUDFBName.Name = "textUDFBName";
            this.textUDFBName.Size = new System.Drawing.Size(413, 20);
            this.textUDFBName.TabIndex = 1;
            this.textUDFBName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textUDFBName_KeyPress);
            this.textUDFBName.Leave += new System.EventHandler(this.textUDFBName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "UDFB Name";
            // 
            // UserDefinedFunctionBlockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UserDefinedFunctionBlockControl";
            this.Size = new System.Drawing.Size(549, 443);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvUDFB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textboxOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textUDFBName;
        private System.Windows.Forms.ComboBox comboBoxDataType;
        private System.Windows.Forms.TextBox textText;
        private System.Windows.Forms.Label lblcontroltext;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button btnAddFB;
        private System.Windows.Forms.DataGridView GvUDFB;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmSelect;
    }
}
