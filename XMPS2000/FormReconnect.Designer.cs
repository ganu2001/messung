namespace XMPS2000
{
    partial class FormReconnect
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
            this.btnreconnect = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Expansion module mismatch ";
            // 
            // btnreconnect
            // 
            this.btnreconnect.Location = new System.Drawing.Point(142, 50);
            this.btnreconnect.Name = "btnreconnect";
            this.btnreconnect.Size = new System.Drawing.Size(80, 25);
            this.btnreconnect.TabIndex = 1;
            this.btnreconnect.Text = "Reconnect";
            this.btnreconnect.UseVisualStyleBackColor = true;
            this.btnreconnect.Click += new System.EventHandler(this.btnreconnect_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(245, 50);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(80, 25);
            this.btncancel.TabIndex = 2;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // FormReconnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 87);
            this.ControlBox = false;
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnreconnect);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReconnect";
            this.Text = "Reconnect...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnreconnect;
        private System.Windows.Forms.Button btncancel;
    }
}