using LadderEditorLib.DesignControl;
using System.Windows.Forms;

namespace LadderDrawing
{
    partial class LadderCanvas
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
                if (timerToolTip != null)
                {
                    timerToolTip.Enabled = false;
                    timerToolTip.Dispose();
                    timerToolTip = null;
                }
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
            this.components = new System.ComponentModel.Container();
            this.CntxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CntxMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.CntxMenuInsAftr = new System.Windows.Forms.ToolStripMenuItem();
            this.CntxMenuCommtRung = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxMenuUncommentRung = new System.Windows.Forms.ToolStripMenuItem();
            this.crossReferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerToolTip = new System.Windows.Forms.Timer(this.components);
            this.CntxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // CntxMenu
            // 
            this.CntxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CntxMenuDelete,
            this.CntxMenuInsAftr,
            this.CntxMenuCommtRung,
            this.cntxMenuUncommentRung,
            this.crossReferenceToolStripMenuItem});
            this.CntxMenu.Name = "CntxMenu";
            this.CntxMenu.Size = new System.Drawing.Size(173, 114);
            // 
            // CntxMenuDelete
            // 
            this.CntxMenuDelete.Name = "CntxMenuDelete";
            this.CntxMenuDelete.Size = new System.Drawing.Size(172, 22);
            this.CntxMenuDelete.Text = "Delete Rung";
            this.CntxMenuDelete.Click += new System.EventHandler(this.CntxMenuDelete_Click);
            // 
            // CntxMenuInsAftr
            // 
            this.CntxMenuInsAftr.Name = "CntxMenuInsAftr";
            this.CntxMenuInsAftr.Size = new System.Drawing.Size(172, 22);
            this.CntxMenuInsAftr.Text = "Insert After Rung";
            this.CntxMenuInsAftr.Click += new System.EventHandler(this.CntxMenuInsAftr_Click);
            // 
            // CntxMenuCommtRung
            // 
            this.CntxMenuCommtRung.Name = "CntxMenuCommtRung";
            this.CntxMenuCommtRung.Size = new System.Drawing.Size(172, 22);
            this.CntxMenuCommtRung.Text = "Comment Rung";
            this.CntxMenuCommtRung.Click += new System.EventHandler(this.CntxMenuCommtRung_Click);
            // 
            // cntxMenuUncommentRung
            // 
            this.cntxMenuUncommentRung.Name = "cntxMenuUncommentRung";
            this.cntxMenuUncommentRung.Size = new System.Drawing.Size(172, 22);
            this.cntxMenuUncommentRung.Text = "Uncomment Rung";
            this.cntxMenuUncommentRung.Click += new System.EventHandler(this.cntxMenuUncommentRung_Click);
            // 
            // crossReferenceToolStripMenuItem
            // 
            this.crossReferenceToolStripMenuItem.Name = "crossReferenceToolStripMenuItem";
            this.crossReferenceToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.crossReferenceToolStripMenuItem.Text = "Cross Reference";
            this.crossReferenceToolStripMenuItem.Click += new System.EventHandler(this.CntxMenuCrossRef_Click);
            // 
            // timerToolTip
            // 
            this.timerToolTip.Interval = 3000;
            this.timerToolTip.Tick += new System.EventHandler(this.timerToolTip_Tick);
            // 
            // LadderCanvas
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LadderCanvas";
            this.Size = new System.Drawing.Size(674, 461);
            this.Load += new System.EventHandler(this.LadderCanvas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LadderCanvas_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LadderCanvas_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LadderCanvas_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LadderCanvas_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LadderCanvas_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LadderDrawing_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LadderCanvas_MouseUp);
            this.CntxMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip CntxMenu;
        private System.Windows.Forms.ToolStripMenuItem CntxMenuDelete;
        private System.Windows.Forms.ToolStripMenuItem CntxMenuInsAftr;
        private Timer timerToolTip;
        private ToolStripMenuItem CntxMenuCommtRung;
        private ToolStripMenuItem cntxMenuUncommentRung;
        private ToolStripMenuItem crossReferenceToolStripMenuItem;
    }
}
