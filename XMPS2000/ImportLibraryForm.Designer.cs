namespace XMPS2000
{
    partial class ImportLibraryForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblAvailableLibraries;
        private System.Windows.Forms.DataGridView dgvLibraries;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblAvailableLibraries = new System.Windows.Forms.Label();
            this.dgvLibraries = new System.Windows.Forms.DataGridView();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();

            this.SuspendLayout();

            // 
            // lblAvailableLibraries
            // 
            this.lblAvailableLibraries.AutoSize = true;
            this.lblAvailableLibraries.Location = new System.Drawing.Point(12, 9);
            this.lblAvailableLibraries.Name = "lblAvailableLibraries";
            this.lblAvailableLibraries.Size = new System.Drawing.Size(112, 13);
            this.lblAvailableLibraries.Text = "Available Libraries:";

            // 
            // dgvLibraries
            // 
            this.dgvLibraries.ColumnHeadersVisible = false; // Hide the column headers.
            this.dgvLibraries.RowHeadersVisible = false;
            this.dgvLibraries.Location = new System.Drawing.Point(12, 25);
            this.dgvLibraries.Name = "dgvLibraries";
            this.dgvLibraries.Size = new System.Drawing.Size(376, 250);
            this.dgvLibraries.RowTemplate.Height = 24;
            this.dgvLibraries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLibraries.BackColor = System.Drawing.Color.White; // Set DataGridView background to white
            this.dgvLibraries.DefaultCellStyle.BackColor = System.Drawing.Color.White; // Set cell background to white
            this.dgvLibraries.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White; // Remove gray alternate rows

            // Add a single column without a header
            var libraryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            libraryColumn.HeaderText = ""; // Empty header text
            libraryColumn.Name = "Library";
            this.dgvLibraries.Columns.Add(libraryColumn);

            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 281);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;

            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(313, 281);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 307);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 13);
            this.lblStatus.Text = "Ready to import libraries";

            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "CSV Library Files (*.csv)|*.csv|All Files (*.*)|*.*";
            this.openFileDialog.Title = "Select a Library File";

            // 
            // ImportLibraryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.dgvLibraries);
            this.Controls.Add(this.lblAvailableLibraries);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportLibraryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Library - XMPS2000";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
