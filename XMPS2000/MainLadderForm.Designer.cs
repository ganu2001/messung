using System;
using System.Windows.Forms;

namespace XMPS2000
{
    partial class MainLadderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainLadderForm));
            this.tvLogicBlocks = new System.Windows.Forms.TreeView();
            this.cntxmain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cntxdelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxCommentBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxUnCommentBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tsBlocks = new System.Windows.Forms.ToolStrip();
            this.tbcmdSave = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertRung = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertContactBefore = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertContactAfter = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertContactParallal = new System.Windows.Forms.ToolStripButton();
            this.tsbSwapItemStyle = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertCoil = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbSetCoil = new System.Windows.Forms.ToolStripButton();
            this.tbcmdReset = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertPNContact = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertHorizontalLine = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertFBBefore = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertFBAfter = new System.Windows.Forms.ToolStripButton();
            this.tslblblockname = new System.Windows.Forms.ToolStripLabel();
            this.ladderEditorControlMain = new LadderDrawing.UserControls.LadderEditorControl();
            this.cntxmain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tsBlocks.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvLogicBlocks
            // 
            this.tvLogicBlocks.AllowDrop = true;
            this.tvLogicBlocks.Location = new System.Drawing.Point(824, 2);
            this.tvLogicBlocks.Name = "tvLogicBlocks";
            this.tvLogicBlocks.Size = new System.Drawing.Size(309, 738);
            this.tvLogicBlocks.TabIndex = 1;
            this.tvLogicBlocks.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvLogicBlocks_ItemDrag);
            this.tvLogicBlocks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLogicBlocks_AfterSelect);
            this.tvLogicBlocks.DoubleClick += new System.EventHandler(this.tvLogicBlocks_DoubleClick);
            // 
            // cntxmain
            // 
            this.cntxmain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cntxdelete,
            this.cntxCommentBlock,
            this.cntxUnCommentBlock});
            this.cntxmain.Name = "cntxmain";
            this.cntxmain.Size = new System.Drawing.Size(176, 70);
            // 
            // cntxdelete
            // 
            this.cntxdelete.Name = "cntxdelete";
            this.cntxdelete.Size = new System.Drawing.Size(175, 22);
            this.cntxdelete.Text = "Delete Record";
            this.cntxdelete.Click += new System.EventHandler(this.cntxdelete_Click);
            // 
            // cntxCommentBlock
            // 
            this.cntxCommentBlock.Name = "cntxCommentBlock";
            this.cntxCommentBlock.Size = new System.Drawing.Size(175, 22);
            this.cntxCommentBlock.Text = "Comment Block";
            this.cntxCommentBlock.Click += new System.EventHandler(this.cntxCommentBlock_Click);
            // 
            // cntxUnCommentBlock
            // 
            this.cntxUnCommentBlock.Name = "cntxUnCommentBlock";
            this.cntxUnCommentBlock.Size = new System.Drawing.Size(175, 22);
            this.cntxUnCommentBlock.Text = "UnComment Block";
            this.cntxUnCommentBlock.Click += new System.EventHandler(this.cntxUnCommentBlock_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tsBlocks);
            this.panel1.Controls.Add(this.ladderEditorControlMain);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(817, 738);
            this.panel1.TabIndex = 9;
            // 
            // tsBlocks
            // 
            this.tsBlocks.BackColor = System.Drawing.SystemColors.Control;
            this.tsBlocks.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsBlocks.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsBlocks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbcmdSave,
            this.tsbInsertRung,
            this.tsbInsertContactBefore,
            this.tsbInsertContactAfter,
            this.tsbInsertContactParallal,
            this.tsbSwapItemStyle,
            this.tsbInsertCoil,
            this.toolStripButton1,
            this.tsbSetCoil,
            this.tbcmdReset,
            this.tsbInsertPNContact,
            this.tsbInsertHorizontalLine,
            this.tsbInsertFBBefore,
            this.tsbInsertFBAfter,
            this.tslblblockname});
            this.tsBlocks.Location = new System.Drawing.Point(0, 0);
            this.tsBlocks.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tsBlocks.Name = "tsBlocks";
            this.tsBlocks.Padding = new System.Windows.Forms.Padding(0);
            this.tsBlocks.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsBlocks.Size = new System.Drawing.Size(817, 31);
            this.tsBlocks.TabIndex = 10;
            this.tsBlocks.Text = "tsBlocks";
            // 
            // tbcmdSave
            // 
            this.tbcmdSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbcmdSave.Image = ((System.Drawing.Image)(resources.GetObject("tbcmdSave.Image")));
            this.tbcmdSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbcmdSave.Name = "tbcmdSave";
            this.tbcmdSave.Size = new System.Drawing.Size(28, 28);
            this.tbcmdSave.Text = "tsbInsertComment";
            this.tbcmdSave.ToolTipText = "Insert Blank Rung";
            this.tbcmdSave.Visible = false;
            // 
            // tsbInsertRung
            // 
            this.tsbInsertRung.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertRung.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertRung.Image")));
            this.tsbInsertRung.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertRung.Name = "tsbInsertRung";
            this.tsbInsertRung.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertRung.Text = "tsbInsertRung";
            this.tsbInsertRung.ToolTipText = "Insert Rung";
            this.tsbInsertRung.Visible = false;
            // 
            // tsbInsertContactBefore
            // 
            this.tsbInsertContactBefore.BackColor = System.Drawing.SystemColors.Control;
            this.tsbInsertContactBefore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertContactBefore.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertContactBefore.Image")));
            this.tsbInsertContactBefore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertContactBefore.Name = "tsbInsertContactBefore";
            this.tsbInsertContactBefore.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertContactBefore.Text = "tsbInsertContactBefore";
            this.tsbInsertContactBefore.ToolTipText = "Insert Contact Before";
            this.tsbInsertContactBefore.Click += new System.EventHandler(this.tsbInsertContactBefore_Click);
            // 
            // tsbInsertContactAfter
            // 
            this.tsbInsertContactAfter.BackColor = System.Drawing.SystemColors.Control;
            this.tsbInsertContactAfter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertContactAfter.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertContactAfter.Image")));
            this.tsbInsertContactAfter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertContactAfter.Name = "tsbInsertContactAfter";
            this.tsbInsertContactAfter.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertContactAfter.Text = "tsbInsertContactAfter";
            this.tsbInsertContactAfter.ToolTipText = "Insert Contact After";
            this.tsbInsertContactAfter.Visible = false;
            // 
            // tsbInsertContactParallal
            // 
            this.tsbInsertContactParallal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertContactParallal.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertContactParallal.Image")));
            this.tsbInsertContactParallal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertContactParallal.Name = "tsbInsertContactParallal";
            this.tsbInsertContactParallal.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertContactParallal.Text = "tsbInsertContactParallal";
            this.tsbInsertContactParallal.ToolTipText = "Insert Contact Parallal";
            this.tsbInsertContactParallal.Visible = false;
            // 
            // tsbSwapItemStyle
            // 
            this.tsbSwapItemStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSwapItemStyle.Image = ((System.Drawing.Image)(resources.GetObject("tsbSwapItemStyle.Image")));
            this.tsbSwapItemStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSwapItemStyle.Name = "tsbSwapItemStyle";
            this.tsbSwapItemStyle.Size = new System.Drawing.Size(28, 28);
            this.tsbSwapItemStyle.Text = "tsbSwapItemStyle";
            this.tsbSwapItemStyle.ToolTipText = "Swap Item Style";
            this.tsbSwapItemStyle.Click += new System.EventHandler(this.tsbSwapItemStyle_Click);
            // 
            // tsbInsertCoil
            // 
            this.tsbInsertCoil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertCoil.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertCoil.Image")));
            this.tsbInsertCoil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertCoil.Name = "tsbInsertCoil";
            this.tsbInsertCoil.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertCoil.Text = "tsbInsertCoil";
            this.tsbInsertCoil.ToolTipText = "Insert Coil";
            this.tsbInsertCoil.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "F";
            this.toolStripButton1.ToolTipText = "Insert Function Block";
            this.toolStripButton1.Visible = false;
            // 
            // tsbSetCoil
            // 
            this.tsbSetCoil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSetCoil.Image = ((System.Drawing.Image)(resources.GetObject("tsbSetCoil.Image")));
            this.tsbSetCoil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetCoil.Name = "tsbSetCoil";
            this.tsbSetCoil.Size = new System.Drawing.Size(28, 28);
            this.tsbSetCoil.Text = "S";
            this.tsbSetCoil.ToolTipText = "Set Coil";
            this.tsbSetCoil.Visible = false;
            // 
            // tbcmdReset
            // 
            this.tbcmdReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbcmdReset.Image = ((System.Drawing.Image)(resources.GetObject("tbcmdReset.Image")));
            this.tbcmdReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbcmdReset.Name = "tbcmdReset";
            this.tbcmdReset.Size = new System.Drawing.Size(28, 28);
            this.tbcmdReset.Text = "R";
            this.tbcmdReset.ToolTipText = "Reset Coil";
            this.tbcmdReset.Visible = false;
            // 
            // tsbInsertPNContact
            // 
            this.tsbInsertPNContact.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertPNContact.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertPNContact.Image")));
            this.tsbInsertPNContact.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertPNContact.Name = "tsbInsertPNContact";
            this.tsbInsertPNContact.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertPNContact.Text = "P";
            this.tsbInsertPNContact.ToolTipText = "PNContact";
            this.tsbInsertPNContact.Visible = false;
            // 
            // tsbInsertHorizontalLine
            // 
            this.tsbInsertHorizontalLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertHorizontalLine.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertHorizontalLine.Image")));
            this.tsbInsertHorizontalLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertHorizontalLine.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.tsbInsertHorizontalLine.Name = "tsbInsertHorizontalLine";
            this.tsbInsertHorizontalLine.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertHorizontalLine.Text = "tsbInsertHorizontalLine";
            this.tsbInsertHorizontalLine.ToolTipText = "Insert Horizontal Line";
            this.tsbInsertHorizontalLine.Visible = false;
            // 
            // tsbInsertFBBefore
            // 
            this.tsbInsertFBBefore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertFBBefore.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertFBBefore.Image")));
            this.tsbInsertFBBefore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertFBBefore.Name = "tsbInsertFBBefore";
            this.tsbInsertFBBefore.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertFBBefore.Text = "tsbInsertFBBefore";
            this.tsbInsertFBBefore.ToolTipText = "Insert Function Block Before";
            this.tsbInsertFBBefore.Visible = false;
            // 
            // tsbInsertFBAfter
            // 
            this.tsbInsertFBAfter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertFBAfter.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertFBAfter.Image")));
            this.tsbInsertFBAfter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertFBAfter.Name = "tsbInsertFBAfter";
            this.tsbInsertFBAfter.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertFBAfter.Text = "tsbInsertFBAfter";
            this.tsbInsertFBAfter.ToolTipText = "Insert Function Block After";
            this.tsbInsertFBAfter.Visible = false;
            // 
            // tslblblockname
            // 
            this.tslblblockname.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslblblockname.ForeColor = System.Drawing.Color.Blue;
            this.tslblblockname.Name = "tslblblockname";
            this.tslblblockname.Size = new System.Drawing.Size(96, 28);
            this.tslblblockname.Text = "Main Logic Block";
            // 
            // ladderEditorControlMain
            // 
            this.ladderEditorControlMain.AutoScroll = true;
            this.ladderEditorControlMain.AutoSize = true;
            this.ladderEditorControlMain.BackColor = System.Drawing.Color.White;
            this.ladderEditorControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ladderEditorControlMain.Location = new System.Drawing.Point(0, 0);
            this.ladderEditorControlMain.Margin = new System.Windows.Forms.Padding(2);
            this.ladderEditorControlMain.Name = "ladderEditorControlMain";
            this.ladderEditorControlMain.Size = new System.Drawing.Size(817, 738);
            this.ladderEditorControlMain.TabIndex = 9;
            this.ladderEditorControlMain.ValidText = false;
            this.ladderEditorControlMain.ItemClicked += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.ladderEditorControl1_ItemClicked);
            this.ladderEditorControlMain.ItemDeleted += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.ladderEditorControl1_ItemDeleted);
            this.ladderEditorControlMain.MainLogicBlockRefresh += new System.EventHandler<System.EventArgs>(this.cntxCommentBlock_Click);
            this.ladderEditorControlMain.CrossReferanceClicked += new System.EventHandler<System.EventArgs>(this.ladderEditorControlMain_CrossReferanceClicked);
            // 
            // MainLadderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 744);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tvLogicBlocks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainLadderForm";
            this.ShowInTaskbar = false;
            this.Text = "MainLadderForm";
            this.cntxmain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsBlocks.ResumeLayout(false);
            this.tsBlocks.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.TreeView tvLogicBlocks;
        private System.Windows.Forms.ContextMenuStrip cntxmain;
        private System.Windows.Forms.ToolStripMenuItem cntxdelete;
        private System.Windows.Forms.ToolStripMenuItem cntxCommentBlock;
        private System.Windows.Forms.ToolStripMenuItem cntxUnCommentBlock;
        private System.Windows.Forms.Panel panel1;
        private LadderDrawing.UserControls.LadderEditorControl ladderEditorControlMain;
        private System.Windows.Forms.ToolStrip tsBlocks;
        private System.Windows.Forms.ToolStripButton tbcmdSave;
        private System.Windows.Forms.ToolStripButton tsbInsertRung;
        private System.Windows.Forms.ToolStripButton tsbInsertContactBefore;
        private System.Windows.Forms.ToolStripButton tsbInsertContactAfter;
        private System.Windows.Forms.ToolStripButton tsbInsertContactParallal;
        private System.Windows.Forms.ToolStripButton tsbSwapItemStyle;
        private System.Windows.Forms.ToolStripButton tsbInsertCoil;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbSetCoil;
        private System.Windows.Forms.ToolStripButton tbcmdReset;
        private System.Windows.Forms.ToolStripButton tsbInsertPNContact;
        private System.Windows.Forms.ToolStripButton tsbInsertHorizontalLine;
        private System.Windows.Forms.ToolStripButton tsbInsertFBBefore;
        private System.Windows.Forms.ToolStripButton tsbInsertFBAfter;
        private System.Windows.Forms.ToolStripLabel tslblblockname;
    }
}