namespace XMPS2000
{
    partial class CompileErrors
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
            this.mainErrorPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // mainErrorPanel
            // 
            this.mainErrorPanel.AutoScroll = true;
            this.mainErrorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainErrorPanel.Location = new System.Drawing.Point(0, 0);
            this.mainErrorPanel.Name = "mainErrorPanel";
            this.mainErrorPanel.Size = new System.Drawing.Size(800, 192);
            this.mainErrorPanel.TabIndex = 0;
            // 
            // CompileErrors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 192);
            this.Controls.Add(this.mainErrorPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CompileErrors";
            this.ShowIcon = false;
            this.Text = "CompileErrors";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainErrorPanel;
    }
}