using System;
using System.ComponentModel;
using System.Xml.Schema;

namespace XMPS2000.LadderLogic
{
    partial class LadderWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LadderWindow));
            this.cntxmRung = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cntxeditrung = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxDeleteRung = new System.Windows.Forms.ToolStripMenuItem();
            this.cntcInsertAfterRung = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlineMonitorTimer = new System.Windows.Forms.Timer(this.components);
            this.tsBlocks = new System.Windows.Forms.ToolStrip();
            this.tbcmdSave = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertRung = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertContactBefore = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertContactAfter = new System.Windows.Forms.ToolStripButton();
            this.tsbSwapItemStyle = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertContactParallal = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertCoil = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbSetCoil = new System.Windows.Forms.ToolStripButton();
            this.tbcmdReset = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertPNContact = new System.Windows.Forms.ToolStripButton();
            this.tsbClearContact = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertHorizontalLine = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertFBBefore = new System.Windows.Forms.ToolStripButton();
            this.tsbInsertFBAfter = new System.Windows.Forms.ToolStripButton();
            this.tslblblockname = new System.Windows.Forms.ToolStripLabel();
            this.lblInstructions = new System.Windows.Forms.ToolStripLabel();
            this.ladderEditorControl1 = new LadderDrawing.UserControls.LadderEditorControl();
            this.DoubleBuffered = true;
            this.cntxmRung.SuspendLayout();
            this.tsBlocks.SuspendLayout();
            this.SuspendLayout();
            // 
            // cntxmRung
            // 
            this.cntxmRung.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxmRung.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cntxeditrung,
            this.cntxDeleteRung,
            this.cntcInsertAfterRung});
            this.cntxmRung.Name = "cntxmRung";
            this.cntxmRung.Size = new System.Drawing.Size(164, 70);
            // 
            // cntxeditrung
            // 
            this.cntxeditrung.Name = "cntxeditrung";
            this.cntxeditrung.Size = new System.Drawing.Size(163, 22);
            this.cntxeditrung.Text = "Edit Rung";
            // 
            // cntxDeleteRung
            // 
            this.cntxDeleteRung.Name = "cntxDeleteRung";
            this.cntxDeleteRung.Size = new System.Drawing.Size(163, 22);
            this.cntxDeleteRung.Text = "Delete Rung";
            // 
            // cntcInsertAfterRung
            // 
            this.cntcInsertAfterRung.Name = "cntcInsertAfterRung";
            this.cntcInsertAfterRung.Size = new System.Drawing.Size(163, 22);
            this.cntcInsertAfterRung.Text = "Insert After Rung";
            // 
            // OnlineMonitorTimer
            // 
            this.OnlineMonitorTimer.Interval = 10;
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
            this.tsbSwapItemStyle,
            this.tsbInsertContactParallal,
            this.tsbInsertCoil,
            this.toolStripButton1,
            this.tsbSetCoil,
            this.tbcmdReset,
            this.tsbInsertPNContact,
            this.tsbClearContact,
            this.tsbInsertHorizontalLine,
            this.tsbInsertFBBefore,
            this.tsbInsertFBAfter,
            this.tslblblockname,
            this.lblInstructions});
            this.tsBlocks.Location = new System.Drawing.Point(0, 0);
            this.tsBlocks.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tsBlocks.Name = "tsBlocks";
            this.tsBlocks.Padding = new System.Windows.Forms.Padding(0);
            this.tsBlocks.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsBlocks.Size = new System.Drawing.Size(800, 31);
            this.tsBlocks.TabIndex = 6;
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
            this.tbcmdSave.Click += new System.EventHandler(this.tbcmdSave_Click_1);
            // 
            // tsbInsertRung
            // 
            this.tsbInsertRung.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertRung.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertRung.Image")));
            this.tsbInsertRung.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertRung.Name = "tsbInsertRung";
            this.tsbInsertRung.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertRung.Text = "tsbInsertRung";
            this.tsbInsertRung.ToolTipText = "Insert Rung      Ctrl+I";
            this.tsbInsertRung.Click += new System.EventHandler(this.tsbInsertRung_Click);
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
            this.tsbInsertContactBefore.ToolTipText = "Insert Contact Before   Ctrl+K";
            this.tsbInsertContactBefore.Click += new System.EventHandler(this.tsbInsertContactBefore_Click_1);
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
            this.tsbInsertContactAfter.ToolTipText = "Insert Contact After   Ctrl+D";
            this.tsbInsertContactAfter.Click += new System.EventHandler(this.tsbInsertContactAfter_Click);
            // 
            // tsbSwapItemStyle
            // 
            this.tsbSwapItemStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSwapItemStyle.Image = ((System.Drawing.Image)(resources.GetObject("tsbSwapItemStyle.Image")));
            this.tsbSwapItemStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSwapItemStyle.Name = "tsbSwapItemStyle";
            this.tsbSwapItemStyle.Size = new System.Drawing.Size(28, 28);
            this.tsbSwapItemStyle.Text = "tsbSwapItemStyle";
            this.tsbSwapItemStyle.ToolTipText = "Swap Item Style   Spacebar";
            this.tsbSwapItemStyle.Click += new System.EventHandler(this.tsbSwapItemStyle_Click);
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
            this.tsbInsertContactParallal.Click += new System.EventHandler(this.tsbInsertContactParallal_Click);
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
            this.tsbInsertCoil.Click += new System.EventHandler(this.tsbInsertCoil_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "F";
            this.toolStripButton1.ToolTipText = "Insert Function Block   Ctrl+B";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbSetCoil
            // 
            this.tsbSetCoil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSetCoil.Image = ((System.Drawing.Image)(resources.GetObject("tsbSetCoil.Image")));
            this.tsbSetCoil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetCoil.Name = "tsbSetCoil";
            this.tsbSetCoil.Size = new System.Drawing.Size(28, 28);
            this.tsbSetCoil.Text = "S";
            this.tsbSetCoil.ToolTipText = "Set Coil    Spacebar";
            this.tsbSetCoil.Click += new System.EventHandler(this.tsbSetCoil_Click);
            // 
            // tbcmdReset
            // 
            this.tbcmdReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbcmdReset.Image = ((System.Drawing.Image)(resources.GetObject("tbcmdReset.Image")));
            this.tbcmdReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbcmdReset.Name = "tbcmdReset";
            this.tbcmdReset.Size = new System.Drawing.Size(28, 28);
            this.tbcmdReset.Text = "R";
            this.tbcmdReset.ToolTipText = "Reset Coil  Spacebar";
            this.tbcmdReset.Click += new System.EventHandler(this.tbcmdReset_Click);
            // 
            // tsbInsertPNContact
            // 
            this.tsbInsertPNContact.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertPNContact.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertPNContact.Image")));
            this.tsbInsertPNContact.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertPNContact.Name = "tsbInsertPNContact";
            this.tsbInsertPNContact.Size = new System.Drawing.Size(28, 28);
            this.tsbInsertPNContact.Text = "P";
            this.tsbInsertPNContact.ToolTipText = "PNContact    Spacebar";
            this.tsbInsertPNContact.Click += new System.EventHandler(this.tsbInsertPNContact_Click);
            // 
            // tsbClearContact
            // 
            this.tsbClearContact.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClearContact.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearContact.Image")));
            this.tsbClearContact.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearContact.Name = "tsbClearContact";
            this.tsbClearContact.Size = new System.Drawing.Size(23, 28);
            this.tsbClearContact.Click += new System.EventHandler(this.tsbClearContact_Click);
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
            this.tsbInsertFBBefore.Click += new System.EventHandler(this.tsbInsertFBBefore_Click);
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
            this.tslblblockname.Size = new System.Drawing.Size(84, 28);
            this.tslblblockname.Text = "Logical Block 1";
            // 
            // lblInstructions
            // 
            this.lblInstructions.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.ForeColor = System.Drawing.Color.Blue;
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(73, 28);
            this.lblInstructions.Text = "Instructions";
            // 
            // ladderEditorControl1
            // 
            this.ladderEditorControl1.AutoScroll = true;
            this.ladderEditorControl1.AutoSize = true;
            this.ladderEditorControl1.BackColor = System.Drawing.Color.White;
            this.ladderEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ladderEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.ladderEditorControl1.Margin = new System.Windows.Forms.Padding(2);
            this.ladderEditorControl1.Name = "ladderEditorControl1";
            this.ladderEditorControl1.Size = new System.Drawing.Size(800, 450);
            this.ladderEditorControl1.TabIndex = 1;
            this.ladderEditorControl1.ValidText = false;
            this.ladderEditorControl1.ItemClicked += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.ladderEditorControl1_ItemClicked);
            this.ladderEditorControl1.ItemDeleted += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.ladderEditorControl1_ItemDeleted);
            this.ladderEditorControl1.TextValidation += new System.EventHandler<System.ComponentModel.CancelEventArgs>(this.ladderEditorControl1_TagValidation);
            this.ladderEditorControl1.CrossReferanceClicked += new System.EventHandler<System.EventArgs>(this.ladderEditorControl1_CrossReferanceClicked);
            this.ladderEditorControl1.Load += new System.EventHandler(this.ladderEditorControl1_Load);
            this.ladderEditorControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ladderEditorControl1_KeyDown);
            // 
            // LadderWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.tsBlocks);
            this.Controls.Add(this.ladderEditorControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LadderWindow";
            this.Text = "LadderWindow";
            this.cntxmRung.ResumeLayout(false);
            this.tsBlocks.ResumeLayout(false);
            this.tsBlocks.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cntxmRung;
        private System.Windows.Forms.ToolStripMenuItem cntxeditrung;
        private System.Windows.Forms.ToolStripMenuItem cntxDeleteRung;
        private System.Windows.Forms.ToolStripMenuItem cntcInsertAfterRung;
        private System.Windows.Forms.Timer OnlineMonitorTimer;
        private LadderDrawing.UserControls.LadderEditorControl ladderEditorControl1;
        private System.Windows.Forms.ToolStrip tsBlocks;
        private System.Windows.Forms.ToolStripButton tbcmdReset;
        private System.Windows.Forms.ToolStripButton tsbInsertContactBefore;
        private System.Windows.Forms.ToolStripButton tsbInsertContactAfter;
        private System.Windows.Forms.ToolStripButton tsbInsertContactParallal;
        private System.Windows.Forms.ToolStripButton tsbInsertHorizontalLine;
        private System.Windows.Forms.ToolStripButton tsbSwapItemStyle;
        private System.Windows.Forms.ToolStripButton tsbInsertFBBefore;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbInsertFBAfter;
        private System.Windows.Forms.ToolStripButton tsbInsertRung;
        private System.Windows.Forms.ToolStripButton tsbInsertCoil;
        private System.Windows.Forms.ToolStripButton tbcmdSave;
        private System.Windows.Forms.ToolStripButton tsbSetCoil;
        private System.Windows.Forms.ToolStripLabel tslblblockname;
        private System.Windows.Forms.ToolStripButton tsbInsertPNContact;
        private System.Windows.Forms.ToolStripButton tsbClearContact;
        private System.Windows.Forms.ToolStripLabel lblInstructions;
    }
}