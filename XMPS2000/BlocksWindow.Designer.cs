
namespace XMPS2000
{
    partial class BlocksWindow
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
            this.tvBlocks = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvBlocks
            // 
            this.tvBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvBlocks.Location = new System.Drawing.Point(0, 0);
            this.tvBlocks.Name = "tvBlocks";
            this.tvBlocks.PathSeparator = "";
            this.tvBlocks.ShowNodeToolTips = true;
            this.tvBlocks.Size = new System.Drawing.Size(265, 450);
            this.tvBlocks.TabIndex = 0;
            this.tvBlocks.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvBlocks_ItemDrag);
            // 
            // BlocksWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 450);
            this.Controls.Add(this.tvBlocks);
            this.Name = "BlocksWindow";
            this.Text = "Blocks";
            this.Shown += new System.EventHandler(this.BlocksWindow_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvBlocks;
    }
}