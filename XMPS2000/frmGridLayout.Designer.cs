
namespace XMPS2000
{
    partial class frmGridLayout
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdMain = new System.Windows.Forms.DataGridView();
            this.cntxmain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cntxdelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddResiValues = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxDisVar = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCntx = new System.Windows.Forms.ToolStripMenuItem();
            this.cutCntx = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteCntx = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxCommentTag = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxUncommentTag = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxremoveDisablingVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxAddRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxCrossReferance = new System.Windows.Forms.ToolStripMenuItem();
            this.omtimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.cntxmain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdMain);
            this.splitContainer1.Size = new System.Drawing.Size(1067, 554);
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // grdMain
            // 
            this.grdMain.AllowUserToAddRows = false;
            this.grdMain.AllowUserToDeleteRows = false;
            this.grdMain.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdMain.Name = "grdMain";
            this.grdMain.ReadOnly = true;
            this.grdMain.RowHeadersWidth = 51;
            this.grdMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdMain.Size = new System.Drawing.Size(1067, 499);
            this.grdMain.TabIndex = 1;
            this.grdMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdMain_CellFormatting);
            this.grdMain.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdMain_CellMouseDoubleClick);
            this.grdMain.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdMain_ColumnHeaderMouseClick);
            this.grdMain.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            this.grdMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdMain_KeyDown);
            this.grdMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.grdMain_MouseClick);
            // 
            // cntxmain
            // 
            this.cntxmain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxmain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cntxdelete,
            this.tsmAddResiValues,
            this.cntxDisVar,
            this.copyCntx,
            this.cutCntx,
            this.pasteCntx,
            this.cntxCommentTag,
            this.cntxUncommentTag,
            this.cntxremoveDisablingVariable,
            this.cntxAddRequest,
            this.cntxCrossReferance});
            this.cntxmain.Name = "cntxmain";
            this.cntxmain.Size = new System.Drawing.Size(264, 244);
            // 
            // cntxdelete
            // 
            this.cntxdelete.Name = "cntxdelete";
            this.cntxdelete.Size = new System.Drawing.Size(263, 24);
            this.cntxdelete.Text = "Delete Record";
            this.cntxdelete.Click += new System.EventHandler(this.cntxdelete_Click);
            // 
            this.tsmAddResiValues.Name = "tsmAddResiValues";
            this.tsmAddResiValues.Size = new System.Drawing.Size(225, 24);
            this.tsmAddResiValues.Text = "Add Resistance Values";
            this.tsmAddResiValues.Click += new System.EventHandler(this.tsmAddResiValues_Click);
            // cntxDisVar
            // 
            this.cntxDisVar.Name = "cntxDisVar";
            this.cntxDisVar.Size = new System.Drawing.Size(263, 24);
            this.cntxDisVar.Text = "Generate Disabling Variable";
            this.cntxDisVar.Click += new System.EventHandler(this.cntxDisVar_Click);
            // 
            // copyCntx
            // 
            this.copyCntx.Name = "copyCntx";
            this.copyCntx.Size = new System.Drawing.Size(263, 24);
            this.copyCntx.Text = "Copy";
            this.copyCntx.Click += new System.EventHandler(this.copyCntx_Click);
            // 
            // cutCntx
            // 
            this.cutCntx.Name = "cutCntx";
            this.cutCntx.Size = new System.Drawing.Size(263, 24);
            this.cutCntx.Text = "Cut";
            this.cutCntx.Click += new System.EventHandler(this.cutCntx_Click);
            // 
            // pasteCntx
            // 
            this.pasteCntx.Name = "pasteCntx";
            this.pasteCntx.Size = new System.Drawing.Size(263, 24);
            this.pasteCntx.Text = "Paste";
            this.pasteCntx.Click += new System.EventHandler(this.pasteCntx_Click);
            // 
            // cntxCommentTag
            // 
            this.cntxCommentTag.Name = "cntxCommentTag";
            this.cntxCommentTag.Size = new System.Drawing.Size(263, 24);
            this.cntxCommentTag.Text = "Comment Tag";
            this.cntxCommentTag.Click += new System.EventHandler(this.cntxCommentTag_Click);
            // 
            // cntxUncommentTag
            // 
            this.cntxUncommentTag.Name = "cntxUncommentTag";
            this.cntxUncommentTag.Size = new System.Drawing.Size(263, 24);
            this.cntxUncommentTag.Text = "UnComment Tag";
            this.cntxUncommentTag.Click += new System.EventHandler(this.cntxUncommentTag_Click);
            // 
            // cntxremoveDisablingVariable
            // 
            this.cntxremoveDisablingVariable.Name = "cntxremoveDisablingVariable";
            this.cntxremoveDisablingVariable.Size = new System.Drawing.Size(263, 24);
            this.cntxremoveDisablingVariable.Text = "Remove Disabling Variable";
            this.cntxremoveDisablingVariable.Click += new System.EventHandler(this.cntxRemoveDisVar);
            // 
            // cntxAddRequest
            // 
            this.cntxAddRequest.Name = "cntxAddRequest";
            this.cntxAddRequest.Size = new System.Drawing.Size(263, 24);
            this.cntxAddRequest.Text = "Add Request";
            this.cntxAddRequest.Click += new System.EventHandler(this.cntxAddRequest_Click);
            // 
            // cntxCrossReferance
            // 
            this.cntxCrossReferance.Name = "cntxCrossReferance";
            this.cntxCrossReferance.Size = new System.Drawing.Size(263, 24);
            this.cntxCrossReferance.Text = "Cross Reference";
            this.cntxCrossReferance.Click += new System.EventHandler(this.cntxCrossReferance_Click);
            // 
            // omtimer
            // 
            this.omtimer.Interval = 110;
            this.omtimer.Tick += new System.EventHandler(this.omtimer_Tick);
            // 
            // frmGridLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGridLayout";
            this.Text = "frmGridLayout";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGridLayout_FormClosed);
            this.Shown += new System.EventHandler(this.frmGridLayout_Shown);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.cntxmain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView grdMain;
        private System.Windows.Forms.ContextMenuStrip cntxmain;
        private System.Windows.Forms.ToolStripMenuItem cntxdelete;
        private System.Windows.Forms.ToolStripMenuItem tsmAddResiValues;
        private System.Windows.Forms.ToolStripMenuItem cntxDisVar;
        private System.Windows.Forms.ToolStripMenuItem copyCntx;
        private System.Windows.Forms.ToolStripMenuItem cutCntx;
        private System.Windows.Forms.ToolStripMenuItem pasteCntx;
        private System.Windows.Forms.Timer omtimer;
        private System.Windows.Forms.ToolStripMenuItem cntxCommentTag;
        private System.Windows.Forms.ToolStripMenuItem cntxUncommentTag;
        private System.Windows.Forms.ToolStripMenuItem cntxremoveDisablingVariable;
        private System.Windows.Forms.ToolStripMenuItem cntxAddRequest;
        private System.Windows.Forms.ToolStripMenuItem cntxCrossReferance;
    }
}