namespace XMPS2000
{
    partial class FirmwareUpdate
    {
        //// <summary>
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
            this.label1 = new System.Windows.Forms.Label();
            this.BinBrowse = new System.Windows.Forms.Button();
            this.UpdateFirmwarebtn = new System.Windows.Forms.Button();
            this.TextBin = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(743, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Specify the binary file to update into the target";
            // 
            // BinBrowse
            // 
            this.BinBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BinBrowse.Location = new System.Drawing.Point(712, 86);
            this.BinBrowse.Name = "BinBrowse";
            this.BinBrowse.Size = new System.Drawing.Size(129, 47);
            this.BinBrowse.TabIndex = 2;
            this.BinBrowse.Text = "Browse";
            this.BinBrowse.UseVisualStyleBackColor = true;
            this.BinBrowse.Click += new System.EventHandler(this.BinBrowse_Click);
            // 
            // UpdateFirmwarebtn
            // 
            this.UpdateFirmwarebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateFirmwarebtn.Location = new System.Drawing.Point(325, 198);
            this.UpdateFirmwarebtn.Name = "UpdateFirmwarebtn";
            this.UpdateFirmwarebtn.Size = new System.Drawing.Size(197, 45);
            this.UpdateFirmwarebtn.TabIndex = 3;
            this.UpdateFirmwarebtn.Text = "Update";
            this.UpdateFirmwarebtn.UseVisualStyleBackColor = true;
            this.UpdateFirmwarebtn.Click += new System.EventHandler(this.UpdateFirmware_Click);
            // 
            // TextBin
            // 
            this.TextBin.Location = new System.Drawing.Point(60, 92);
            this.TextBin.Name = "TextBin";
            this.TextBin.Size = new System.Drawing.Size(626, 26);
            this.TextBin.TabIndex = 4;
            // 
            // FirmwareBin
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 294);
            this.Controls.Add(this.TextBin);
            this.Controls.Add(this.UpdateFirmwarebtn);
            this.Controls.Add(this.BinBrowse);
            this.Controls.Add(this.label1);
            this.Name = "FirmwareBin";
            this.Text = "FirmwareBin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BinBrowse;
        private System.Windows.Forms.Button UpdateFirmwarebtn;
        private System.Windows.Forms.TextBox TextBin;
    }
}
