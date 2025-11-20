namespace XMPS2000
{
    partial class BuildTypeForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkBoth = new System.Windows.Forms.CheckBox();
            this.checkXBLD = new System.Windows.Forms.CheckBox();
            this.checkXMPRO = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.checkBoth);
            this.groupBox1.Controls.Add(this.checkXBLD);
            this.groupBox1.Controls.Add(this.checkXMPRO);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 151);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(62, 102);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkBoth
            // 
            this.checkBoth.AutoSize = true;
            this.checkBoth.Location = new System.Drawing.Point(62, 78);
            this.checkBoth.Name = "checkBoth";
            this.checkBoth.Size = new System.Drawing.Size(48, 17);
            this.checkBoth.TabIndex = 2;
            this.checkBoth.Text = "Both";
            this.checkBoth.UseVisualStyleBackColor = true;
            this.checkBoth.CheckedChanged += new System.EventHandler(this.checkBoth_CheckedChanged);
            // 
            // checkXBLD
            // 
            this.checkXBLD.AutoSize = true;
            this.checkXBLD.Location = new System.Drawing.Point(62, 54);
            this.checkXBLD.Name = "checkXBLD";
            this.checkXBLD.Size = new System.Drawing.Size(75, 17);
            this.checkXBLD.TabIndex = 1;
            this.checkXBLD.Text = "OnlyXBLD";
            this.checkXBLD.UseVisualStyleBackColor = true;
            this.checkXBLD.CheckedChanged += new System.EventHandler(this.checkXBLD_CheckedChanged);
            // 
            // checkXMPRO
            // 
            this.checkXMPRO.AutoSize = true;
            this.checkXMPRO.Location = new System.Drawing.Point(62, 30);
            this.checkXMPRO.Name = "checkXMPRO";
            this.checkXMPRO.Size = new System.Drawing.Size(86, 17);
            this.checkXMPRO.TabIndex = 0;
            this.checkXMPRO.Text = "OnlyXMPRO";
            this.checkXMPRO.UseVisualStyleBackColor = true;
            this.checkXMPRO.CheckedChanged += new System.EventHandler(this.checkXMPRO_CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // BuildTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 179);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuildTypeForm";
            this.ShowIcon = false;
            this.Text = "BuildTypeForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoth;
        private System.Windows.Forms.CheckBox checkXBLD;
        private System.Windows.Forms.CheckBox checkXMPRO;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}