namespace XMPS2000
{
    partial class Login_Firmware
    {   /// <summary>
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
            this.FirmwareLoginUserID = new System.Windows.Forms.Label();
            this.FirmwareLoginPassword = new System.Windows.Forms.Label();
            this.UserID = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.LoginFrm = new System.Windows.Forms.GroupBox();
            this.LoginFrm.SuspendLayout();
            this.SuspendLayout();
            // 
            // FirmwareLoginUserID
            // 
            this.FirmwareLoginUserID.AutoSize = true;
            this.FirmwareLoginUserID.Location = new System.Drawing.Point(17, 30);
            this.FirmwareLoginUserID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FirmwareLoginUserID.Name = "FirmwareLoginUserID";
            this.FirmwareLoginUserID.Size = new System.Drawing.Size(67, 13);
            this.FirmwareLoginUserID.TabIndex = 0;
            this.FirmwareLoginUserID.Text = "User_ID      :";
            // 
            // FirmwareLoginPassword
            // 
            this.FirmwareLoginPassword.AutoSize = true;
            this.FirmwareLoginPassword.Location = new System.Drawing.Point(17, 63);
            this.FirmwareLoginPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FirmwareLoginPassword.Name = "FirmwareLoginPassword";
            this.FirmwareLoginPassword.Size = new System.Drawing.Size(68, 13);
            this.FirmwareLoginPassword.TabIndex = 1;
            this.FirmwareLoginPassword.Text = "Password    :";
            // 
            // UserID
            // 
            this.UserID.Location = new System.Drawing.Point(86, 30);
            this.UserID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UserID.Name = "UserID";
            this.UserID.Size = new System.Drawing.Size(85, 20);
            this.UserID.TabIndex = 2;
            this.UserID.Text = "admin";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(86, 63);
            this.Password.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(85, 20);
            this.Password.TabIndex = 3;
            this.Password.Text = "admin";
            this.Password.UseSystemPasswordChar = true;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(101, 94);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(55, 24);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginFrm
            // 
            this.LoginFrm.Controls.Add(this.UserID);
            this.LoginFrm.Controls.Add(this.LoginButton);
            this.LoginFrm.Controls.Add(this.FirmwareLoginUserID);
            this.LoginFrm.Controls.Add(this.Password);
            this.LoginFrm.Controls.Add(this.FirmwareLoginPassword);
            this.LoginFrm.Location = new System.Drawing.Point(20, 15);
            this.LoginFrm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LoginFrm.Name = "LoginFrm";
            this.LoginFrm.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LoginFrm.Size = new System.Drawing.Size(268, 131);
            this.LoginFrm.TabIndex = 5;
            this.LoginFrm.TabStop = false;
            this.LoginFrm.Text = "Login";
            // 
            // Login_Firmware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LoginFrm);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Login_Firmware";
            this.Size = new System.Drawing.Size(311, 154);
            this.LoginFrm.ResumeLayout(false);
            this.LoginFrm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label FirmwareLoginUserID;
        private System.Windows.Forms.Label FirmwareLoginPassword;
        private System.Windows.Forms.TextBox UserID;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.GroupBox LoginFrm;
    }
}
