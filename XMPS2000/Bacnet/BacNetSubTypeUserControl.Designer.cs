namespace XMPS2000.Bacnet
{
    partial class BacNetSubTypeUserControl
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
            this.components = new System.ComponentModel.Container();
            this.lblObjectIdentifier = new System.Windows.Forms.Label();
            this.lblInstaceNumber = new System.Windows.Forms.Label();
            this.lblObjectType = new System.Windows.Forms.Label();
            this.lblObjectName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.textBoxObjectIdentifier = new System.Windows.Forms.TextBox();
            this.textBoxInstanceNumber = new System.Windows.Forms.TextBox();
            this.textBoxObjectType = new System.Windows.Forms.TextBox();
            this.textBoxObjectName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblScheduleValue = new System.Windows.Forms.Label();
            this.grpSchedule = new System.Windows.Forms.GroupBox();
            this.textBoxVariable = new System.Windows.Forms.TextBox();
            this.TagEnable = new System.Windows.Forms.ComboBox();
            this.lblVariable = new System.Windows.Forms.Label();
            this.comboBoxScheduleValue = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.grpSchedule.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblObjectIdentifier
            // 
            this.lblObjectIdentifier.AutoSize = true;
            this.lblObjectIdentifier.Location = new System.Drawing.Point(26, 22);
            this.lblObjectIdentifier.Name = "lblObjectIdentifier";
            this.lblObjectIdentifier.Size = new System.Drawing.Size(84, 13);
            this.lblObjectIdentifier.TabIndex = 0;
            this.lblObjectIdentifier.Text = "Object_Identifier";
            // 
            // lblInstaceNumber
            // 
            this.lblInstaceNumber.AutoSize = true;
            this.lblInstaceNumber.Location = new System.Drawing.Point(26, 54);
            this.lblInstaceNumber.Name = "lblInstaceNumber";
            this.lblInstaceNumber.Size = new System.Drawing.Size(91, 13);
            this.lblInstaceNumber.TabIndex = 1;
            this.lblInstaceNumber.Text = "Instance_Number";
            // 
            // lblObjectType
            // 
            this.lblObjectType.AutoSize = true;
            this.lblObjectType.Location = new System.Drawing.Point(26, 86);
            this.lblObjectType.Name = "lblObjectType";
            this.lblObjectType.Size = new System.Drawing.Size(68, 13);
            this.lblObjectType.TabIndex = 2;
            this.lblObjectType.Text = "Object_Type";
            // 
            // lblObjectName
            // 
            this.lblObjectName.AutoSize = true;
            this.lblObjectName.Location = new System.Drawing.Point(26, 117);
            this.lblObjectName.Name = "lblObjectName";
            this.lblObjectName.Size = new System.Drawing.Size(72, 13);
            this.lblObjectName.TabIndex = 4;
            this.lblObjectName.Text = "Object_Name";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(26, 149);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Description";
            // 
            // textBoxObjectIdentifier
            // 
            this.textBoxObjectIdentifier.Enabled = false;
            this.textBoxObjectIdentifier.Location = new System.Drawing.Point(140, 18);
            this.textBoxObjectIdentifier.Name = "textBoxObjectIdentifier";
            this.textBoxObjectIdentifier.Size = new System.Drawing.Size(174, 20);
            this.textBoxObjectIdentifier.TabIndex = 6;
            // 
            // textBoxInstanceNumber
            // 
            this.textBoxInstanceNumber.Enabled = false;
            this.textBoxInstanceNumber.Location = new System.Drawing.Point(140, 50);
            this.textBoxInstanceNumber.Name = "textBoxInstanceNumber";
            this.textBoxInstanceNumber.Size = new System.Drawing.Size(173, 20);
            this.textBoxInstanceNumber.TabIndex = 7;
            // 
            // textBoxObjectType
            // 
            this.textBoxObjectType.Enabled = false;
            this.textBoxObjectType.Location = new System.Drawing.Point(140, 82);
            this.textBoxObjectType.Name = "textBoxObjectType";
            this.textBoxObjectType.Size = new System.Drawing.Size(172, 20);
            this.textBoxObjectType.TabIndex = 8;
            // 
            // textBoxObjectName
            // 
            this.textBoxObjectName.Location = new System.Drawing.Point(140, 113);
            this.textBoxObjectName.Name = "textBoxObjectName";
            this.textBoxObjectName.Size = new System.Drawing.Size(171, 20);
            this.textBoxObjectName.TabIndex = 10;
            this.textBoxObjectName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxObjectName_KeyPress);
            this.textBoxObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxObjectName_Validating);
            this.textBoxObjectName.TextChanged += new System.EventHandler(this.textBoxObjectName_TextChanged);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(140, 145);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(171, 20);
            this.textBoxDescription.TabIndex = 11;
            this.textBoxDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDescription_KeyPress);
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(226, 253);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Validating += new System.ComponentModel.CancelEventHandler(this.btnSave_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblScheduleValue
            // 
            this.lblScheduleValue.AutoSize = true;
            this.errorProvider1.SetIconAlignment(this.lblScheduleValue, System.Windows.Forms.ErrorIconAlignment.TopLeft);
            this.lblScheduleValue.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblScheduleValue.Location = new System.Drawing.Point(8, 15);
            this.lblScheduleValue.Name = "lblScheduleValue";
            this.lblScheduleValue.Size = new System.Drawing.Size(82, 13);
            this.lblScheduleValue.TabIndex = 21;
            this.lblScheduleValue.Text = "Schedule Type";
            this.lblScheduleValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpSchedule
            // 
            this.grpSchedule.Controls.Add(this.textBoxVariable);
            this.grpSchedule.Controls.Add(this.TagEnable);
            this.grpSchedule.Controls.Add(this.lblVariable);
            this.grpSchedule.Controls.Add(this.comboBoxScheduleValue);
            this.grpSchedule.Controls.Add(this.lblScheduleValue);
            this.grpSchedule.Location = new System.Drawing.Point(16, 171);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Size = new System.Drawing.Size(414, 76);
            this.grpSchedule.TabIndex = 21;
            this.grpSchedule.TabStop = false;
            // 
            // textBoxVariable
            // 
            this.textBoxVariable.Location = new System.Drawing.Point(297, 45);
            this.textBoxVariable.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxVariable.Name = "textBoxVariable";
            this.textBoxVariable.Size = new System.Drawing.Size(102, 20);
            this.textBoxVariable.TabIndex = 25;
            this.textBoxVariable.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxVariable_Validating);
            // 
            // TagEnable
            // 
            this.TagEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TagEnable.FormattingEnabled = true;
            this.TagEnable.Location = new System.Drawing.Point(122, 44);
            this.TagEnable.Margin = new System.Windows.Forms.Padding(2);
            this.TagEnable.Name = "TagEnable";
            this.TagEnable.Size = new System.Drawing.Size(171, 21);
            this.TagEnable.TabIndex = 24;
            this.TagEnable.SelectedIndexChanged += new System.EventHandler(this.TagEnable_SelectedIndexChanged);
            this.TagEnable.Validating += new System.ComponentModel.CancelEventHandler(this.TagEnable_Validating);
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Location = new System.Drawing.Point(8, 44);
            this.lblVariable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(45, 13);
            this.lblVariable.TabIndex = 23;
            this.lblVariable.Text = "Variable";
            // 
            // comboBoxScheduleValue
            // 
            this.comboBoxScheduleValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleValue.FormattingEnabled = true;
            this.comboBoxScheduleValue.Items.AddRange(new object[] {
            "Boolean",
            "Numeric"});
            this.comboBoxScheduleValue.Location = new System.Drawing.Point(122, 12);
            this.comboBoxScheduleValue.Name = "comboBoxScheduleValue";
            this.comboBoxScheduleValue.Size = new System.Drawing.Size(171, 21);
            this.comboBoxScheduleValue.TabIndex = 22;
            this.comboBoxScheduleValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleValue_SelectedIndexChanged);
            this.comboBoxScheduleValue.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxScheduleValue_Validating);
            // 
            // BacNetSubTypeUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.grpSchedule);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxObjectName);
            this.Controls.Add(this.textBoxObjectType);
            this.Controls.Add(this.textBoxInstanceNumber);
            this.Controls.Add(this.textBoxObjectIdentifier);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblObjectName);
            this.Controls.Add(this.lblObjectType);
            this.Controls.Add(this.lblInstaceNumber);
            this.Controls.Add(this.lblObjectIdentifier);
            this.Name = "BacNetSubTypeUserControl";
            this.Size = new System.Drawing.Size(437, 295);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.grpSchedule.ResumeLayout(false);
            this.grpSchedule.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblObjectIdentifier;
        private System.Windows.Forms.Label lblInstaceNumber;
        private System.Windows.Forms.Label lblObjectType;
        private System.Windows.Forms.Label lblObjectName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox textBoxObjectIdentifier;
        private System.Windows.Forms.TextBox textBoxInstanceNumber;
        private System.Windows.Forms.TextBox textBoxObjectType;
        private System.Windows.Forms.TextBox textBoxObjectName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox grpSchedule;
        private System.Windows.Forms.TextBox textBoxVariable;
        private System.Windows.Forms.ComboBox TagEnable;
        private System.Windows.Forms.Label lblVariable;
        private System.Windows.Forms.ComboBox comboBoxScheduleValue;
        private System.Windows.Forms.Label lblScheduleValue;
    }
}
