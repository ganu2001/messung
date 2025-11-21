namespace XMPS2000.Bacnet
{
    partial class BacNetObjectInformation
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
            this.bacNetObjectInfoGrid = new System.Windows.Forms.DataGridView();
            this.ObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaximumCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bacNetObjectInfoGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // bacNetObjectInfoGrid
            // 
            this.bacNetObjectInfoGrid.AllowUserToAddRows = false;
            this.bacNetObjectInfoGrid.AllowUserToDeleteRows = false;
            this.bacNetObjectInfoGrid.AllowUserToOrderColumns = true;
            this.bacNetObjectInfoGrid.AllowUserToResizeColumns = false;
            this.bacNetObjectInfoGrid.AllowUserToResizeRows = false;
            this.bacNetObjectInfoGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bacNetObjectInfoGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ObjectName,
            this.CurrentCount,
            this.MaximumCount});
            this.bacNetObjectInfoGrid.Location = new System.Drawing.Point(12, 12);
            this.bacNetObjectInfoGrid.Name = "bacNetObjectInfoGrid";
            this.bacNetObjectInfoGrid.Size = new System.Drawing.Size(348, 198);
            this.bacNetObjectInfoGrid.TabIndex = 0;
            // 
            // ObjectName
            // 
            this.ObjectName.HeaderText = "Object Name";
            this.ObjectName.Name = "ObjectName";
            this.ObjectName.ReadOnly = true;
            // 
            // CurrentCount
            // 
            this.CurrentCount.HeaderText = "Current Count";
            this.CurrentCount.Name = "CurrentCount";
            this.CurrentCount.ReadOnly = true;
            // 
            // MaximumCount
            // 
            this.MaximumCount.HeaderText = "Maximum Count";
            this.MaximumCount.Name = "MaximumCount";
            this.MaximumCount.ReadOnly = true;
            // 
            // BacNetObjectInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 222);
            this.Controls.Add(this.bacNetObjectInfoGrid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BacNetObjectInformation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "BacNetObjectInformation";
            ((System.ComponentModel.ISupportInitialize)(this.bacNetObjectInfoGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView bacNetObjectInfoGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaximumCount;
    }
}