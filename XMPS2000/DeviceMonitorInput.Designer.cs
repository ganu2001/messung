namespace XMPS2000
{
    partial class DeviceMonitorInput
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
            this.label1 = new System.Windows.Forms.Label();
            this.textAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lengthNumericValue = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.lengthNumericValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Starting Address";
            // 
            // textAddress
            // 
            this.textAddress.Location = new System.Drawing.Point(36, 48);
            this.textAddress.Name = "textAddress";
            this.textAddress.Size = new System.Drawing.Size(100, 20);
            this.textAddress.TabIndex = 1;
            this.textAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textAddress_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Length";
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(166, 46);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(75, 23);
            this.btnInput.TabIndex = 3;
            this.btnInput.Text = "Input";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.Location = new System.Drawing.Point(166, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lengthNumericValue
            // 
            this.lengthNumericValue.Location = new System.Drawing.Point(36, 107);
            this.lengthNumericValue.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.lengthNumericValue.Name = "lengthNumericValue";
            this.lengthNumericValue.Size = new System.Drawing.Size(100, 20);
            this.lengthNumericValue.TabIndex = 2;
            this.lengthNumericValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DeviceMonitorInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 153);
            this.Controls.Add(this.lengthNumericValue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textAddress);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeviceMonitorInput";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DeviceMonitorInput";
            ((System.ComponentModel.ISupportInitialize)(this.lengthNumericValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button btnCancel;
        private Core.Base.Helpers.MyNumericUpDown lengthNumericValue;
    }
}