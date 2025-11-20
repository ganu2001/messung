namespace XMPS2000.Configuration
{
    partial class TagsUserControl
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
            this.lblModel = new System.Windows.Forms.Label();
            this.lblLogicalAddress = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.lblLabel = new System.Windows.Forms.Label();
            this.textBoxLogicalAddress = new System.Windows.Forms.TextBox();
            this.textBoxIOList = new System.Windows.Forms.TextBox();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblIOList = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBoxInitialValue = new System.Windows.Forms.TextBox();
            this.lblInitialValue = new System.Windows.Forms.Label();
            this.chkIsRetentive = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.comboBoxIOType = new System.Windows.Forms.ComboBox();
            this.ChkShowLogicalAddress = new System.Windows.Forms.CheckBox();
            this.autoaddcheckbox = new System.Windows.Forms.CheckBox();
            this.TagCount = new System.Windows.Forms.NumericUpDown();
            this.lblNumberOfTag = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxEnableInputFilter = new System.Windows.Forms.CheckBox();
            this.textBoxInFilValue = new System.Windows.Forms.TextBox();
            this.labelDigitalFilter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TagCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(53, 19);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(36, 13);
            this.lblModel.TabIndex = 7;
            this.lblModel.Text = "Model";
            // 
            // lblLogicalAddress
            // 
            this.lblLogicalAddress.AutoSize = true;
            this.lblLogicalAddress.Location = new System.Drawing.Point(53, 141);
            this.lblLogicalAddress.Name = "lblLogicalAddress";
            this.lblLogicalAddress.Size = new System.Drawing.Size(82, 13);
            this.lblLogicalAddress.TabIndex = 9;
            this.lblLogicalAddress.Text = "Logical Address";
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.Location = new System.Drawing.Point(53, 184);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(26, 13);
            this.lblTag.TabIndex = 10;
            this.lblTag.Text = "Tag";
            // 
            // textBoxTag
            // 
            this.textBoxTag.Location = new System.Drawing.Point(154, 181);
            this.textBoxTag.MaxLength = 20;
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(150, 20);
            this.textBoxTag.TabIndex = 4;
            // 
            // textBoxModel
            // 
            this.textBoxModel.Enabled = false;
            this.textBoxModel.Location = new System.Drawing.Point(154, 15);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(150, 20);
            this.textBoxModel.TabIndex = 1;
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.Enabled = false;
            this.textBoxLabel.Location = new System.Drawing.Point(154, 57);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.Size = new System.Drawing.Size(150, 20);
            this.textBoxLabel.TabIndex = 2;
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(53, 59);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(33, 13);
            this.lblLabel.TabIndex = 8;
            this.lblLabel.Text = "Label";
            // 
            // textBoxLogicalAddress
            // 
            this.textBoxLogicalAddress.Enabled = false;
            this.textBoxLogicalAddress.Location = new System.Drawing.Point(154, 137);
            this.textBoxLogicalAddress.Name = "textBoxLogicalAddress";
            this.textBoxLogicalAddress.Size = new System.Drawing.Size(150, 20);
            this.textBoxLogicalAddress.TabIndex = 3;
            this.textBoxLogicalAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxLogicalAddress_Validating);
            // 
            // textBoxIOList
            // 
            this.textBoxIOList.Enabled = false;
            this.textBoxIOList.Location = new System.Drawing.Point(154, 224);
            this.textBoxIOList.Name = "textBoxIOList";
            this.textBoxIOList.Size = new System.Drawing.Size(150, 20);
            this.textBoxIOList.TabIndex = 5;
            // 
            // textBoxType
            // 
            this.textBoxType.Enabled = false;
            this.textBoxType.Location = new System.Drawing.Point(154, 98);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(150, 20);
            this.textBoxType.TabIndex = 6;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(53, 101);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 12;
            this.lblType.Text = "Type";
            // 
            // lblIOList
            // 
            this.lblIOList.AutoSize = true;
            this.lblIOList.Location = new System.Drawing.Point(53, 228);
            this.lblIOList.Name = "lblIOList";
            this.lblIOList.Size = new System.Drawing.Size(34, 13);
            this.lblIOList.TabIndex = 11;
            this.lblIOList.Text = "IOList";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(150, 480);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBoxInitialValue
            // 
            this.textBoxInitialValue.Location = new System.Drawing.Point(154, 269);
            this.textBoxInitialValue.Name = "textBoxInitialValue";
            this.textBoxInitialValue.Size = new System.Drawing.Size(150, 20);
            this.textBoxInitialValue.TabIndex = 7;
            this.textBoxInitialValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxInitialValue.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxInitialValue_Validating);
            // 
            // lblInitialValue
            // 
            this.lblInitialValue.AutoSize = true;
            this.lblInitialValue.Location = new System.Drawing.Point(53, 271);
            this.lblInitialValue.Name = "lblInitialValue";
            this.lblInitialValue.Size = new System.Drawing.Size(31, 13);
            this.lblInitialValue.TabIndex = 15;
            this.lblInitialValue.Text = "Type";
            // 
            // chkIsRetentive
            // 
            this.chkIsRetentive.AutoSize = true;
            this.chkIsRetentive.Location = new System.Drawing.Point(154, 310);
            this.chkIsRetentive.Name = "chkIsRetentive";
            this.chkIsRetentive.Size = new System.Drawing.Size(80, 17);
            this.chkIsRetentive.TabIndex = 8;
            this.chkIsRetentive.Text = "checkBox1";
            this.chkIsRetentive.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Location = new System.Drawing.Point(154, 403);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(139, 21);
            this.comboBoxMode.TabIndex = 10;
            this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMode_SelectedIndexChanged);
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(53, 411);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(31, 13);
            this.lblMode.TabIndex = 17;
            this.lblMode.Text = "Type";
            // 
            // comboBoxIOType
            // 
            this.comboBoxIOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIOType.FormattingEnabled = true;
            this.comboBoxIOType.Location = new System.Drawing.Point(154, 98);
            this.comboBoxIOType.Name = "comboBoxIOType";
            this.comboBoxIOType.Size = new System.Drawing.Size(150, 21);
            this.comboBoxIOType.TabIndex = 53;
            this.comboBoxIOType.Visible = false;
            this.comboBoxIOType.SelectedIndexChanged += new System.EventHandler(this.comboBoxIOType_SelectedIndexChanged);
            // 
            // ChkShowLogicalAddress
            // 
            this.ChkShowLogicalAddress.AutoSize = true;
            this.ChkShowLogicalAddress.Location = new System.Drawing.Point(154, 347);
            this.ChkShowLogicalAddress.Name = "ChkShowLogicalAddress";
            this.ChkShowLogicalAddress.Size = new System.Drawing.Size(131, 17);
            this.ChkShowLogicalAddress.TabIndex = 9;
            this.ChkShowLogicalAddress.Text = "Show Logical Address";
            this.ChkShowLogicalAddress.UseVisualStyleBackColor = true;
            // 
            // autoaddcheckbox
            // 
            this.autoaddcheckbox.AutoSize = true;
            this.autoaddcheckbox.Location = new System.Drawing.Point(56, 453);
            this.autoaddcheckbox.Name = "autoaddcheckbox";
            this.autoaddcheckbox.Size = new System.Drawing.Size(67, 17);
            this.autoaddcheckbox.TabIndex = 54;
            this.autoaddcheckbox.Text = "AutoAdd";
            this.autoaddcheckbox.UseVisualStyleBackColor = true;
            this.autoaddcheckbox.Visible = false;
            // 
            // TagCount
            // 
            this.TagCount.Location = new System.Drawing.Point(235, 450);
            this.TagCount.Margin = new System.Windows.Forms.Padding(2);
            this.TagCount.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.TagCount.Name = "TagCount";
            this.TagCount.Size = new System.Drawing.Size(69, 20);
            this.TagCount.TabIndex = 55;
            this.TagCount.Visible = false;
            // 
            // lblNumberOfTag
            // 
            this.lblNumberOfTag.AutoSize = true;
            this.lblNumberOfTag.Location = new System.Drawing.Point(151, 454);
            this.lblNumberOfTag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNumberOfTag.Name = "lblNumberOfTag";
            this.lblNumberOfTag.Size = new System.Drawing.Size(80, 13);
            this.lblNumberOfTag.TabIndex = 56;
            this.lblNumberOfTag.Text = "Number Of Tag";
            this.lblNumberOfTag.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(234, 480);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 55;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkBoxEnableInputFilter
            // 
            this.checkBoxEnableInputFilter.AutoSize = true;
            this.checkBoxEnableInputFilter.Location = new System.Drawing.Point(154, 373);
            this.checkBoxEnableInputFilter.Name = "checkBoxEnableInputFilter";
            this.checkBoxEnableInputFilter.Size = new System.Drawing.Size(111, 17);
            this.checkBoxEnableInputFilter.TabIndex = 57;
            this.checkBoxEnableInputFilter.Text = "Enable Input Filter";
            this.checkBoxEnableInputFilter.UseVisualStyleBackColor = true;
            this.checkBoxEnableInputFilter.Visible = false;
            this.checkBoxEnableInputFilter.CheckedChanged += new System.EventHandler(this.checkBoxEnableInputFilter_CheckedChanged);
            // 
            // textBoxInFilValue
            // 
            this.textBoxInFilValue.Location = new System.Drawing.Point(264, 371);
            this.textBoxInFilValue.Name = "textBoxInFilValue";
            this.textBoxInFilValue.Size = new System.Drawing.Size(40, 20);
            this.textBoxInFilValue.TabIndex = 58;
            this.textBoxInFilValue.Text = "10";
            this.textBoxInFilValue.Visible = false;
            this.textBoxInFilValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxInFilValue_KeyPress);
            this.textBoxInFilValue.Leave += new System.EventHandler(this.textBoxInFilValue_Leave);
            // 
            // labelDigitalFilter
            // 
            this.labelDigitalFilter.AutoSize = true;
            this.labelDigitalFilter.Location = new System.Drawing.Point(304, 375);
            this.labelDigitalFilter.Name = "labelDigitalFilter";
            this.labelDigitalFilter.Size = new System.Drawing.Size(20, 13);
            this.labelDigitalFilter.TabIndex = 59;
            this.labelDigitalFilter.Text = "ms";
            // 
            // TagsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.labelDigitalFilter);
            this.Controls.Add(this.textBoxInFilValue);
            this.Controls.Add(this.checkBoxEnableInputFilter);
            this.Controls.Add(this.lblNumberOfTag);
            this.Controls.Add(this.TagCount);
            this.Controls.Add(this.autoaddcheckbox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.ChkShowLogicalAddress);
            this.Controls.Add(this.comboBoxIOType);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.comboBoxMode);
            this.Controls.Add(this.chkIsRetentive);
            this.Controls.Add(this.textBoxInitialValue);
            this.Controls.Add(this.lblInitialValue);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.textBoxIOList);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblIOList);
            this.Controls.Add(this.textBoxLogicalAddress);
            this.Controls.Add(this.textBoxLabel);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.textBoxModel);
            this.Controls.Add(this.textBoxTag);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lblLogicalAddress);
            this.Controls.Add(this.lblModel);
            this.Name = "TagsUserControl";
            this.Size = new System.Drawing.Size(400, 540);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TagCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblLogicalAddress;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.TextBox textBoxLogicalAddress;
        private System.Windows.Forms.TextBox textBoxIOList;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblIOList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBoxInitialValue;
        private System.Windows.Forms.Label lblInitialValue;
        private System.Windows.Forms.CheckBox chkIsRetentive;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.ComboBox comboBoxIOType;
        private System.Windows.Forms.CheckBox ChkShowLogicalAddress;
        private System.Windows.Forms.CheckBox autoaddcheckbox;
        private System.Windows.Forms.Label lblNumberOfTag;
        private System.Windows.Forms.NumericUpDown TagCount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBoxEnableInputFilter;
        private System.Windows.Forms.TextBox textBoxInFilValue;
        private System.Windows.Forms.Label labelDigitalFilter;
    }
}
