namespace XMPS2000
{
    partial class FilterFormPopUp
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
            this.comboBoxLogicalAddress = new System.Windows.Forms.ComboBox();
            this.comboBoxTags = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxLogicalAddress
            // 
            this.comboBoxLogicalAddress.FormattingEnabled = true;
            this.comboBoxLogicalAddress.Location = new System.Drawing.Point(27, 3);
            this.comboBoxLogicalAddress.Name = "comboBoxLogicalAddress";
            this.comboBoxLogicalAddress.Size = new System.Drawing.Size(206, 21);
            this.comboBoxLogicalAddress.TabIndex = 0;
            this.comboBoxLogicalAddress.SelectedIndexChanged += new System.EventHandler(this.comboBoxTags_SelectedLogicalAddIndexChanged);
            this.comboBoxLogicalAddress.TextChanged += new System.EventHandler(this.comboBoxTags_LogicalAddTextChanged);
            // 
            // comboBoxTags
            // 
            this.comboBoxTags.FormattingEnabled = true;
            this.comboBoxTags.Location = new System.Drawing.Point(3, 3);
            this.comboBoxTags.Name = "comboBoxTags";
            this.comboBoxTags.Size = new System.Drawing.Size(206, 21);
            this.comboBoxTags.TabIndex = 1;
            this.comboBoxTags.SelectedIndexChanged += new System.EventHandler(this.comboBoxTags_SelectedIndexChanged);
            this.comboBoxTags.TextChanged += new System.EventHandler(this.comboBoxTags_TextChanged);
            // 
            // FilterFormPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxTags);
            this.Controls.Add(this.comboBoxLogicalAddress);
            this.Name = "FilterFormPopUp";
            this.Size = new System.Drawing.Size(241, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxLogicalAddress;
        private System.Windows.Forms.ComboBox comboBoxTags;
    }
}