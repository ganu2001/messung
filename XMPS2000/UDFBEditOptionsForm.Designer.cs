namespace XMPS2000
{
    partial class UDFBEditOptionsForm
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
            this.lblInstruction = new System.Windows.Forms.Label();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.txtLocalName = new System.Windows.Forms.TextBox();
            this.lblLocalName = new System.Windows.Forms.Label();
            this.rbCreateLocal = new System.Windows.Forms.RadioButton();
            this.rbEditMain = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInstruction
            // 
            this.lblInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstruction.Location = new System.Drawing.Point(15, 15);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(360, 30);
            this.lblInstruction.TabIndex = 0;
            this.lblInstruction.Text = "How would you like to edit the UDFB?";
            this.lblInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlOptions
            // 
            this.pnlOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOptions.Controls.Add(this.txtLocalName);
            this.pnlOptions.Controls.Add(this.lblLocalName);
            this.pnlOptions.Controls.Add(this.rbCreateLocal);
            this.pnlOptions.Controls.Add(this.rbEditMain);
            this.pnlOptions.Location = new System.Drawing.Point(15, 55);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(360, 120);
            this.pnlOptions.TabIndex = 1;
            // 
            // txtLocalName
            // 
            this.txtLocalName.Enabled = false;
            this.txtLocalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalName.Location = new System.Drawing.Point(30, 90);
            this.txtLocalName.Name = "txtLocalName";
            this.txtLocalName.Size = new System.Drawing.Size(200, 20);
            this.txtLocalName.TabIndex = 3;
            this.txtLocalName.TextChanged += new System.EventHandler(this.txtLocalName_TextChanged);
            this.txtLocalName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLocalName_KeyPress);
            // 
            // lblLocalName
            // 
            this.lblLocalName.AutoSize = true;
            this.lblLocalName.Enabled = false;
            this.lblLocalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalName.Location = new System.Drawing.Point(30, 75);
            this.lblLocalName.Name = "lblLocalName";
            this.lblLocalName.Size = new System.Drawing.Size(136, 13);
            this.lblLocalName.TabIndex = 2;
            this.lblLocalName.Text = "Enter name for local copy:";
            // 
            // rbCreateLocal
            // 
            this.rbCreateLocal.AutoSize = true;
            this.rbCreateLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCreateLocal.Location = new System.Drawing.Point(10, 45);
            this.rbCreateLocal.Name = "rbCreateLocal";
            this.rbCreateLocal.Size = new System.Drawing.Size(329, 17);
            this.rbCreateLocal.TabIndex = 1;
            this.rbCreateLocal.Text = "Create and edit a local copy with project-specific modifications";
            this.rbCreateLocal.UseVisualStyleBackColor = true;
            this.rbCreateLocal.CheckedChanged += new System.EventHandler(this.rbCreateLocal_CheckedChanged);
            // 
            // rbEditMain
            // 
            this.rbEditMain.AutoSize = true;
            this.rbEditMain.Checked = true;
            this.rbEditMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEditMain.Location = new System.Drawing.Point(10, 15);
            this.rbEditMain.Name = "rbEditMain";
            this.rbEditMain.Size = new System.Drawing.Size(190, 17);
            this.rbEditMain.TabIndex = 0;
            this.rbEditMain.TabStop = true;
            this.rbEditMain.Text = "Edit the main (original) UDFB file";
            this.rbEditMain.UseVisualStyleBackColor = true;
            this.rbEditMain.CheckedChanged += new System.EventHandler(this.rbEditMain_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(215, 185);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(300, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UDFBEditOptionsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(390, 225);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlOptions);
            this.Controls.Add(this.lblInstruction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UDFBEditOptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UDFB Edit Options";
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.TextBox txtLocalName;
        private System.Windows.Forms.Label lblLocalName;
        private System.Windows.Forms.RadioButton rbCreateLocal;
        private System.Windows.Forms.RadioButton rbEditMain;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}