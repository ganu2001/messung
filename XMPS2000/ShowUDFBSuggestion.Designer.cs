namespace XMPS2000
{
    partial class ShowUDFBSuggestion
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnsPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnsPanel
            // 
            this.btnsPanel.Location = new System.Drawing.Point(4, 16);
            this.btnsPanel.Name = "btnsPanel";
            this.btnsPanel.Size = new System.Drawing.Size(321, 168);
            this.btnsPanel.TabIndex = 0;
            // 
            // ShowUDFBSuggestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 192);
            this.Controls.Add(this.btnsPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowUDFBSuggestion";
            this.ShowIcon = false;
            this.Text = "Used UDFB Location";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel btnsPanel;
    }
}
