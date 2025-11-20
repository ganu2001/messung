namespace XMPS2000
{
    partial class FormBacNet
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgbacknet = new System.Windows.Forms.DataGridView();
            this.dgclmSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgclmObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgclmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objidntfr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cntxForBacnet = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbacknet)).BeginInit();
            this.cntxForBacnet.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgbacknet);
            this.splitContainer1.Size = new System.Drawing.Size(1067, 554);
            this.splitContainer1.SplitterDistance = 355;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgbacknet
            // 
            this.dgbacknet.AllowUserToAddRows = false;
            this.dgbacknet.AllowUserToDeleteRows = false;
            this.dgbacknet.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgbacknet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbacknet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgclmSelect,
            this.dgclmObject,
            this.dgclmType,
            this.objidntfr});
            this.dgbacknet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgbacknet.Location = new System.Drawing.Point(0, 0);
            this.dgbacknet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgbacknet.Name = "dgbacknet";
            this.dgbacknet.ReadOnly = true;
            this.dgbacknet.RowHeadersWidth = 51;
            this.dgbacknet.Size = new System.Drawing.Size(355, 554);
            this.dgbacknet.TabIndex = 0;
            this.dgbacknet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgbacknet_CellClick);
            this.dgbacknet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgbacknet_MouseClick);
            // 
            // dgclmSelect
            // 
            this.dgclmSelect.Frozen = true;
            this.dgclmSelect.HeaderText = "Select";
            this.dgclmSelect.MinimumWidth = 6;
            this.dgclmSelect.Name = "dgclmSelect";
            this.dgclmSelect.ReadOnly = true;
            this.dgclmSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgclmSelect.Width = 30;
            // 
            // dgclmObject
            // 
            this.dgclmObject.Frozen = true;
            this.dgclmObject.HeaderText = "Object";
            this.dgclmObject.MinimumWidth = 6;
            this.dgclmObject.Name = "dgclmObject";
            this.dgclmObject.ReadOnly = true;
            this.dgclmObject.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgclmObject.Width = 220;
            // 
            // dgclmType
            // 
            this.dgclmType.Frozen = true;
            this.dgclmType.HeaderText = "Type";
            this.dgclmType.MinimumWidth = 6;
            this.dgclmType.Name = "dgclmType";
            this.dgclmType.ReadOnly = true;
            this.dgclmType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgclmType.Visible = false;
            this.dgclmType.Width = 300;
            // 
            // objidntfr
            // 
            this.objidntfr.Frozen = true;
            this.objidntfr.HeaderText = "Object Identifier";
            this.objidntfr.MinimumWidth = 6;
            this.objidntfr.Name = "objidntfr";
            this.objidntfr.ReadOnly = true;
            this.objidntfr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.objidntfr.Width = 300;
            // 
            // cntxForBacnet
            // 
            this.cntxForBacnet.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxForBacnet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteObjectToolStripMenuItem});
            this.cntxForBacnet.Name = "cntxForBacnet";
            this.cntxForBacnet.Size = new System.Drawing.Size(171, 28);
            // 
            // deleteObjectToolStripMenuItem
            // 
            this.deleteObjectToolStripMenuItem.Name = "deleteObjectToolStripMenuItem";
            this.deleteObjectToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.deleteObjectToolStripMenuItem.Text = "Delete Object";
            this.deleteObjectToolStripMenuItem.Click += new System.EventHandler(this.deleteObjectToolStripMenuItem_Click);
            // 
            // FormBacNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormBacNet";
            this.Text = "FormBacNet";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgbacknet)).EndInit();
            this.cntxForBacnet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgbacknet;
        private System.Windows.Forms.ContextMenuStrip cntxForBacnet;
        private System.Windows.Forms.ToolStripMenuItem deleteObjectToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgclmSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgclmObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgclmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn objidntfr;
    }
}