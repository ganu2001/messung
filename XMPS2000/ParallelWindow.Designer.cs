namespace XMPS2000
{
    partial class ParallelWindow
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
            this.lblTagname = new System.Windows.Forms.Label();
            this.cmbWatch = new System.Windows.Forms.ComboBox();
            this.WatchDGV = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cntxDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.signedDecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsignedDecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexadecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bCDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btndelete = new System.Windows.Forms.Button();
            this.AddMultiplebtn = new System.Windows.Forms.Button();
            this.DatatypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.srNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.retentive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ActualValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreparedValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnForce = new System.Windows.Forms.DataGridViewButtonColumn();
            this.UnForceValue = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HiddenActualValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.WatchDGV)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTagname
            // 
            this.lblTagname.AutoSize = true;
            this.lblTagname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagname.Location = new System.Drawing.Point(15, 20);
            this.lblTagname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTagname.Name = "lblTagname";
            this.lblTagname.Size = new System.Drawing.Size(74, 17);
            this.lblTagname.TabIndex = 0;
            this.lblTagname.Text = "Tag Name";
            // 
            // cmbWatch
            // 
            this.cmbWatch.FormattingEnabled = true;
            this.cmbWatch.Location = new System.Drawing.Point(103, 18);
            this.cmbWatch.Margin = new System.Windows.Forms.Padding(2);
            this.cmbWatch.Name = "cmbWatch";
            this.cmbWatch.Size = new System.Drawing.Size(164, 21);
            this.cmbWatch.TabIndex = 1;
            // 
            // WatchDGV
            // 
            this.WatchDGV.BackgroundColor = System.Drawing.SystemColors.Control;
            this.WatchDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WatchDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.srNo,
            this.Address,
            this.Tag,
            this.DataType,
            this.retentive,
            this.ActualValue,
            this.PreparedValue,
            this.btnForce,
            this.UnForceValue,
            this.Type,
            this.HiddenActualValue});
            this.WatchDGV.Location = new System.Drawing.Point(8, 51);
            this.WatchDGV.Margin = new System.Windows.Forms.Padding(2);
            this.WatchDGV.Name = "WatchDGV";
            this.WatchDGV.RowHeadersVisible = false;
            this.WatchDGV.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WatchDGV.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.WatchDGV.RowTemplate.Height = 24;
            this.WatchDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WatchDGV.Size = new System.Drawing.Size(981, 187);
            this.WatchDGV.TabIndex = 3;
            this.WatchDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.WatchDGV_CellContentClick);
            this.WatchDGV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WatchDGV_MouseClick_1);
            this.WatchDGV.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.WatchDGV_PreviewKeyDown);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(286, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 22);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cntxDeleteRow,
            this.signedDecimalToolStripMenuItem,
            this.unsignedDecimalToolStripMenuItem,
            this.hexadecimalToolStripMenuItem,
            this.aSCIIToolStripMenuItem,
            this.bCDToolStripMenuItem,
            this.binaryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 158);
            // 
            // cntxDeleteRow
            // 
            this.cntxDeleteRow.Name = "cntxDeleteRow";
            this.cntxDeleteRow.Size = new System.Drawing.Size(170, 22);
            this.cntxDeleteRow.Text = "Delete Row";
            this.cntxDeleteRow.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click_1);
            // 
            // signedDecimalToolStripMenuItem
            // 
            this.signedDecimalToolStripMenuItem.Name = "signedDecimalToolStripMenuItem";
            this.signedDecimalToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.signedDecimalToolStripMenuItem.Text = "Signed Decimal";
            this.signedDecimalToolStripMenuItem.Click += new System.EventHandler(this.signedDecimalToolStripMenuItem_Click_1);
            // 
            // unsignedDecimalToolStripMenuItem
            // 
            this.unsignedDecimalToolStripMenuItem.Name = "unsignedDecimalToolStripMenuItem";
            this.unsignedDecimalToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.unsignedDecimalToolStripMenuItem.Text = "Unsigned Decimal";
            this.unsignedDecimalToolStripMenuItem.Click += new System.EventHandler(this.unsignedDecimalToolStripMenuItem_Click);
            // 
            // hexadecimalToolStripMenuItem
            // 
            this.hexadecimalToolStripMenuItem.Name = "hexadecimalToolStripMenuItem";
            this.hexadecimalToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.hexadecimalToolStripMenuItem.Text = "Hexadecimal";
            this.hexadecimalToolStripMenuItem.Click += new System.EventHandler(this.hexadecimalToolStripMenuItem_Click);
            // 
            // aSCIIToolStripMenuItem
            // 
            this.aSCIIToolStripMenuItem.Name = "aSCIIToolStripMenuItem";
            this.aSCIIToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aSCIIToolStripMenuItem.Text = "ASCII";
            this.aSCIIToolStripMenuItem.Click += new System.EventHandler(this.aSCIIToolStripMenuItem_Click);
            // 
            // bCDToolStripMenuItem
            // 
            this.bCDToolStripMenuItem.Name = "bCDToolStripMenuItem";
            this.bCDToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bCDToolStripMenuItem.Text = "BCD";
            this.bCDToolStripMenuItem.Click += new System.EventHandler(this.bCDToolStripMenuItem_Click);
            // 
            // binaryToolStripMenuItem
            // 
            this.binaryToolStripMenuItem.Name = "binaryToolStripMenuItem";
            this.binaryToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.binaryToolStripMenuItem.Text = "Binary";
            this.binaryToolStripMenuItem.Click += new System.EventHandler(this.binaryToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btndelete);
            this.groupBox1.Controls.Add(this.AddMultiplebtn);
            this.groupBox1.Controls.Add(this.DatatypeComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.WatchDGV);
            this.groupBox1.Controls.Add(this.cmbWatch);
            this.groupBox1.Controls.Add(this.lblTagname);
            this.groupBox1.Location = new System.Drawing.Point(9, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(993, 242);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // btndelete
            // 
            this.btndelete.Location = new System.Drawing.Point(497, 18);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(75, 21);
            this.btndelete.TabIndex = 11;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // AddMultiplebtn
            // 
            this.AddMultiplebtn.Location = new System.Drawing.Point(368, 18);
            this.AddMultiplebtn.Name = "AddMultiplebtn";
            this.AddMultiplebtn.Size = new System.Drawing.Size(111, 21);
            this.AddMultiplebtn.TabIndex = 10;
            this.AddMultiplebtn.Text = "Add Multiple Tags";
            this.AddMultiplebtn.UseVisualStyleBackColor = true;
            this.AddMultiplebtn.Click += new System.EventHandler(this.AddMultiplebtn_Click);
            // 
            // DatatypeComboBox
            // 
            this.DatatypeComboBox.FormattingEnabled = true;
            this.DatatypeComboBox.Location = new System.Drawing.Point(660, 19);
            this.DatatypeComboBox.Name = "DatatypeComboBox";
            this.DatatypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.DatatypeComboBox.TabIndex = 9;
            this.DatatypeComboBox.SelectedIndexChanged += new System.EventHandler(this.DatatypeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(597, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Data Type";
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // srNo
            // 
            this.srNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.srNo.HeaderText = "Sr.  No";
            this.srNo.Name = "srNo";
            // 
            // Address
            // 
            this.Address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Address.HeaderText = "Logical Address";
            this.Address.MinimumWidth = 6;
            this.Address.Name = "Address";
            // 
            // Tag
            // 
            this.Tag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Tag.HeaderText = "Tag Name";
            this.Tag.MinimumWidth = 6;
            this.Tag.Name = "Tag";
            // 
            // DataType
            // 
            this.DataType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataType.HeaderText = "Data Type";
            this.DataType.Name = "DataType";
            this.DataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // retentive
            // 
            this.retentive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.retentive.HeaderText = "Retentive";
            this.retentive.Name = "retentive";
            this.retentive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.retentive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ActualValue
            // 
            this.ActualValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ActualValue.HeaderText = "Actual Value";
            this.ActualValue.MinimumWidth = 6;
            this.ActualValue.Name = "ActualValue";
            this.ActualValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PreparedValue
            // 
            this.PreparedValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PreparedValue.HeaderText = "Prepared value";
            this.PreparedValue.Name = "PreparedValue";
            // 
            // btnForce
            // 
            this.btnForce.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.btnForce.HeaderText = "ForceValue";
            this.btnForce.Name = "btnForce";
            this.btnForce.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnForce.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btnForce.Text = "Force";
            // 
            // UnForceValue
            // 
            this.UnForceValue.HeaderText = "UnForceValue";
            this.UnForceValue.Name = "UnForceValue";
            this.UnForceValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UnForceValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.UnForceValue.Text = "UnForceValue";
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Type.HeaderText = "ConversionType";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Visible = false;
            // 
            // HiddenActualValue
            // 
            this.HiddenActualValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HiddenActualValue.HeaderText = "hiddenActualValue";
            this.HiddenActualValue.Name = "HiddenActualValue";
            this.HiddenActualValue.ReadOnly = true;
            this.HiddenActualValue.Visible = false;
            // 
            // ParallelWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 249);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParallelWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ParallelWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ParallelWindow_FormClosing);
            this.Load += new System.EventHandler(this.ParallelWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ParallelWindow_Paint);
            this.Resize += new System.EventHandler(this.ParallelWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.WatchDGV)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTagname;
        private System.Windows.Forms.ComboBox cmbWatch;
        private System.Windows.Forms.DataGridView WatchDGV;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cntxDeleteRow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox DatatypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddMultiplebtn;
        private System.Windows.Forms.ToolStripMenuItem signedDecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsignedDecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexadecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSCIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bCDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binaryToolStripMenuItem;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn srNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tag;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn retentive;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActualValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn PreparedValue;
        private System.Windows.Forms.DataGridViewButtonColumn btnForce;
        private System.Windows.Forms.DataGridViewButtonColumn UnForceValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn HiddenActualValue;
    }
}