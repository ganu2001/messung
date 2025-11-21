namespace XMPS2000
{
    partial class MQTTLicense
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
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblDetectIP = new System.Windows.Forms.Label();
            this.txt_FilePath = new System.Windows.Forms.TextBox();
            this.ipAddressControl1 = new IPAddressControlLib.IPAddressControl();
            this.btnFileDownlaod_click = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFile_Click = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(76, 81);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(70, 16);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "Select File";
            // 
            // lblDetectIP
            // 
            this.lblDetectIP.AutoSize = true;
            this.lblDetectIP.Location = new System.Drawing.Point(76, 140);
            this.lblDetectIP.Name = "lblDetectIP";
            this.lblDetectIP.Size = new System.Drawing.Size(61, 16);
            this.lblDetectIP.TabIndex = 1;
            this.lblDetectIP.Text = "Detect IP";
            // 
            // txt_FilePath
            // 
            this.txt_FilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_FilePath.Location = new System.Drawing.Point(203, 79);
            this.txt_FilePath.Name = "txt_FilePath";
            this.txt_FilePath.Size = new System.Drawing.Size(234, 22);
            this.txt_FilePath.TabIndex = 2;
            this.txt_FilePath.TextChanged += new System.EventHandler(this.txt_FilePath_TextChanged);
            // 
            // ipAddressControl1
            // 
            this.ipAddressControl1.AllowInternalTab = false;
            this.ipAddressControl1.AutoHeight = true;
            this.ipAddressControl1.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl1.Location = new System.Drawing.Point(203, 137);
            this.ipAddressControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl1.MinimumSize = new System.Drawing.Size(99, 22);
            this.ipAddressControl1.Name = "ipAddressControl1";
            this.ipAddressControl1.ReadOnly = false;
            this.ipAddressControl1.Size = new System.Drawing.Size(182, 22);
            this.ipAddressControl1.TabIndex = 4;
            this.ipAddressControl1.Text = "...";
            this.ipAddressControl1.Click += new System.EventHandler(this.ipAddressControl1_Click);
            // 
            // btnFileDownlaod_click
            // 
            this.btnFileDownlaod_click.Location = new System.Drawing.Point(233, 205);
            this.btnFileDownlaod_click.Name = "btnFileDownlaod_click";
            this.btnFileDownlaod_click.Size = new System.Drawing.Size(109, 31);
            this.btnFileDownlaod_click.TabIndex = 5;
            this.btnFileDownlaod_click.Text = "Download";
            this.btnFileDownlaod_click.UseVisualStyleBackColor = true;
            this.btnFileDownlaod_click.Click += new System.EventHandler(this.btnFileDownlaod_click_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOpenFile_Click
            // 
            this.btnOpenFile_Click.Location = new System.Drawing.Point(443, 79);
            this.btnOpenFile_Click.Name = "btnOpenFile_Click";
            this.btnOpenFile_Click.Size = new System.Drawing.Size(44, 25);
            this.btnOpenFile_Click.TabIndex = 6;
            this.btnOpenFile_Click.Text = "...";
            this.btnOpenFile_Click.UseVisualStyleBackColor = true;
            this.btnOpenFile_Click.Click += new System.EventHandler(this.btnOpenFile_Click_Click);
            // 
            // MQTTLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 299);
            this.Controls.Add(this.btnOpenFile_Click);
            this.Controls.Add(this.btnFileDownlaod_click);
            this.Controls.Add(this.ipAddressControl1);
            this.Controls.Add(this.txt_FilePath);
            this.Controls.Add(this.lblDetectIP);
            this.Controls.Add(this.lblFilePath);
            this.Name = "MQTTLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MQTTLicense";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblDetectIP;
        private System.Windows.Forms.TextBox txt_FilePath;
        private IPAddressControlLib.IPAddressControl ipAddressControl1;
        private System.Windows.Forms.Button btnFileDownlaod_click;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpenFile_Click;
    }
}