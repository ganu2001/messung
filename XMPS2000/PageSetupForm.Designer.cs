namespace XMPS2000
{
    partial class PageSetupForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbpagesize = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LBLmTop = new System.Windows.Forms.Label();
            this.lblmBottom = new System.Windows.Forms.Label();
            this.LBLmRight = new System.Windows.Forms.Label();
            this.txtmBottom = new System.Windows.Forms.TextBox();
            this.txtmTop = new System.Windows.Forms.TextBox();
            this.txtmRight = new System.Windows.Forms.TextBox();
            this.txtmLeft = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdLandscape = new System.Windows.Forms.RadioButton();
            this.rdportrait = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTitleProfile = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTitleDate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTitleCustNm = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTitleProjNm = new System.Windows.Forms.TextBox();
            this.txtTitleHeader = new System.Windows.Forms.TextBox();
            //((System.ComponentModel.ISupportInitialize)(this.IO_config)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(266, 465);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(371, 465);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbpagesize
            // 
            this.cbpagesize.FormattingEnabled = true;
            this.cbpagesize.Location = new System.Drawing.Point(101, 23);
            this.cbpagesize.Name = "cbpagesize";
            this.cbpagesize.Size = new System.Drawing.Size(301, 21);
            this.cbpagesize.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbpagesize);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 63);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paper";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.LBLmTop);
            this.groupBox2.Controls.Add(this.lblmBottom);
            this.groupBox2.Controls.Add(this.LBLmRight);
            this.groupBox2.Controls.Add(this.txtmBottom);
            this.groupBox2.Controls.Add(this.txtmTop);
            this.groupBox2.Controls.Add(this.txtmRight);
            this.groupBox2.Controls.Add(this.txtmLeft);
            this.groupBox2.Location = new System.Drawing.Point(243, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Margin (Milimiters)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Left";
            // 
            // LBLmTop
            // 
            this.LBLmTop.AutoSize = true;
            this.LBLmTop.Location = new System.Drawing.Point(6, 56);
            this.LBLmTop.Name = "LBLmTop";
            this.LBLmTop.Size = new System.Drawing.Size(26, 13);
            this.LBLmTop.TabIndex = 6;
            this.LBLmTop.Text = "Top";
            // 
            // lblmBottom
            // 
            this.lblmBottom.AutoSize = true;
            this.lblmBottom.Location = new System.Drawing.Point(105, 61);
            this.lblmBottom.Name = "lblmBottom";
            this.lblmBottom.Size = new System.Drawing.Size(40, 13);
            this.lblmBottom.TabIndex = 5;
            this.lblmBottom.Text = "Bottom";
            // 
            // LBLmRight
            // 
            this.LBLmRight.AutoSize = true;
            this.LBLmRight.Location = new System.Drawing.Point(105, 22);
            this.LBLmRight.Name = "LBLmRight";
            this.LBLmRight.Size = new System.Drawing.Size(32, 13);
            this.LBLmRight.TabIndex = 4;
            this.LBLmRight.Text = "Right";
            // 
            // txtmBottom
            // 
            this.txtmBottom.Location = new System.Drawing.Point(147, 57);
            this.txtmBottom.Name = "txtmBottom";
            this.txtmBottom.Size = new System.Drawing.Size(41, 20);
            this.txtmBottom.TabIndex = 7;
            this.txtmBottom.Text = "25";
            this.txtmBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtmTop
            // 
            this.txtmTop.Location = new System.Drawing.Point(42, 57);
            this.txtmTop.Name = "txtmTop";
            this.txtmTop.Size = new System.Drawing.Size(41, 20);
            this.txtmTop.TabIndex = 6;
            this.txtmTop.Text = "25";
            this.txtmTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtmRight
            // 
            this.txtmRight.Location = new System.Drawing.Point(147, 19);
            this.txtmRight.Name = "txtmRight";
            this.txtmRight.Size = new System.Drawing.Size(41, 20);
            this.txtmRight.TabIndex = 5;
            this.txtmRight.Text = "25";
            this.txtmRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtmLeft
            // 
            this.txtmLeft.Location = new System.Drawing.Point(42, 19);
            this.txtmLeft.Name = "txtmLeft";
            this.txtmLeft.Size = new System.Drawing.Size(41, 20);
            this.txtmLeft.TabIndex = 4;
            this.txtmLeft.Text = "25";
            this.txtmLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdLandscape);
            this.groupBox3.Controls.Add(this.rdportrait);
            this.groupBox3.Location = new System.Drawing.Point(12, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(225, 100);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Orientation";
            // 
            // rdLandscape
            // 
            this.rdLandscape.AutoSize = true;
            this.rdLandscape.Checked = true;
            this.rdLandscape.Location = new System.Drawing.Point(24, 57);
            this.rdLandscape.Name = "rdLandscape";
            this.rdLandscape.Size = new System.Drawing.Size(78, 17);
            this.rdLandscape.TabIndex = 3;
            this.rdLandscape.TabStop = true;
            this.rdLandscape.Text = "Landscape";
            this.rdLandscape.UseVisualStyleBackColor = true;
            // 
            // rdportrait
            // 
            this.rdportrait.AutoSize = true;
            this.rdportrait.Location = new System.Drawing.Point(24, 19);
            this.rdportrait.Name = "rdportrait";
            this.rdportrait.Size = new System.Drawing.Size(58, 17);
            this.rdportrait.TabIndex = 2;
            this.rdportrait.Text = "Portrait";
            this.rdportrait.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtFooter);
            this.groupBox4.Controls.Add(this.txtHeader);
            this.groupBox4.Location = new System.Drawing.Point(12, 187);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(446, 96);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Header And Footer Text ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Header text And alignment";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Footer text And alignment";
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(161, 58);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(262, 20);
            this.txtFooter.TabIndex = 9;
            this.txtFooter.Text = " Page no.";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(161, 32);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(262, 20);
            this.txtHeader.TabIndex = 8;
            this.txtHeader.Text = "Main Application Program";
            this.txtHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.txtTitleProfile);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txtTitleDate);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtTitleCustNm);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txtTitleProjNm);
            this.groupBox5.Controls.Add(this.txtTitleHeader);
            this.groupBox5.Location = new System.Drawing.Point(12, 289);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(446, 170);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Title Page Text And Alignment With Font settings";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 134);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Profile";
            // 
            // txtTitleProfile
            // 
            this.txtTitleProfile.Location = new System.Drawing.Point(161, 136);
            this.txtTitleProfile.Name = "txtTitleProfile";
            this.txtTitleProfile.Size = new System.Drawing.Size(262, 20);
            this.txtTitleProfile.TabIndex = 14;
            this.txtTitleProfile.Text = "XMPS-100";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 108);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Date";
            // 
            // txtTitleDate
            // 
            this.txtTitleDate.Location = new System.Drawing.Point(162, 110);
            this.txtTitleDate.Name = "txtTitleDate";
            this.txtTitleDate.Size = new System.Drawing.Size(262, 20);
            this.txtTitleDate.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Customer name";
            // 
            // txtTitleCustNm
            // 
            this.txtTitleCustNm.Location = new System.Drawing.Point(162, 84);
            this.txtTitleCustNm.Name = "txtTitleCustNm";
            this.txtTitleCustNm.Size = new System.Drawing.Size(262, 20);
            this.txtTitleCustNm.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Header text And alignment";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Project name";
            // 
            // txtTitleProjNm
            // 
            this.txtTitleProjNm.Location = new System.Drawing.Point(161, 58);
            this.txtTitleProjNm.Name = "txtTitleProjNm";
            this.txtTitleProjNm.Size = new System.Drawing.Size(262, 20);
            this.txtTitleProjNm.TabIndex = 11;
            // 
            // txtTitleHeader
            // 
            this.txtTitleHeader.Location = new System.Drawing.Point(161, 32);
            this.txtTitleHeader.Name = "txtTitleHeader";
            this.txtTitleHeader.Size = new System.Drawing.Size(262, 20);
            this.txtTitleHeader.TabIndex = 10;
            this.txtTitleHeader.Text = "Project documentation";
            // 
            // PageSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 500);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOK);
            this.Location = new System.Drawing.Point(100, 100);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PageSetupForm";
            this.Text = "Select Page Settings";
            this.Load += new System.EventHandler(this.PageSetupForm_Load);
            //((System.ComponentModel.ISupportInitialize)(this.IO_config)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbpagesize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdLandscape;
        private System.Windows.Forms.RadioButton rdportrait;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LBLmTop;
        private System.Windows.Forms.Label lblmBottom;
        private System.Windows.Forms.Label LBLmRight;
        private System.Windows.Forms.TextBox txtmBottom;
        private System.Windows.Forms.TextBox txtmTop;
        private System.Windows.Forms.TextBox txtmRight;
        private System.Windows.Forms.TextBox txtmLeft;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTitleProfile;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTitleDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTitleCustNm;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTitleProjNm;
        private System.Windows.Forms.TextBox txtTitleHeader;
    }
}