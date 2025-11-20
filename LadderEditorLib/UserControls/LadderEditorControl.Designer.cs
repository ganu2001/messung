using System.ComponentModel;

namespace LadderDrawing.UserControls
{
    partial class LadderEditorControl
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
            this.ladderCanvas1 = new LadderDrawing.LadderCanvas();
            this.DoubleBuffered = true;
            this.SuspendLayout();
            // 
            // ladderCanvas1
            // 
            this.ladderCanvas1.AllowDrop = true;
            this.ladderCanvas1.Location = new System.Drawing.Point(1, 10);
            this.ladderCanvas1.Margin = new System.Windows.Forms.Padding(1);
            this.ladderCanvas1.Name = "ladderCanvas1";
            this.ladderCanvas1.Size = new System.Drawing.Size(173, 167);
            this.ladderCanvas1.TabIndex = 6;
            // 
            // LadderEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.ladderCanvas1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LadderEditorControl";
            this.Size = new System.Drawing.Size(709, 465);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.LadderEditorControl_Scroll);
            this.ResumeLayout(false);

        }

        #endregion
        private LadderCanvas ladderCanvas1;
    }
}
