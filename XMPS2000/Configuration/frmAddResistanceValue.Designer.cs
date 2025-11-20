namespace XMPS2000.Configuration
{
    partial class frmAddResistanceValue
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
            this.lblResistance = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtResistance = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblResistance
            // 
            this.lblResistance.AutoSize = true;
            this.lblResistance.Location = new System.Drawing.Point(37, 48);
            this.lblResistance.Name = "lblResistance";
            this.lblResistance.Size = new System.Drawing.Size(114, 16);
            this.lblResistance.TabIndex = 0;
            this.lblResistance.Text = "Resistance (Ohm)";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(65, 107);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(45, 16);
            this.lblOutput.TabIndex = 1;
            this.lblOutput.Text = "Output";
            // 
            // txtResistance
            // 
            this.txtResistance.Location = new System.Drawing.Point(189, 42);
            this.txtResistance.Name = "txtResistance";
            this.txtResistance.Size = new System.Drawing.Size(226, 22);
            this.txtResistance.TabIndex = 0;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(189, 107);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(226, 22);
            this.txtOutput.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(102, 181);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(234, 181);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAddResistanceValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 246);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtResistance);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblResistance);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddResistanceValue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Resistance Value";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAddResistanceValue_FormClosed); 
            this.ResumeLayout(false);
            this.ShowIcon = false;
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblResistance;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtResistance;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}