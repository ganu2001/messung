namespace XMPS2000
{
    partial class CrossRefrence
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CrossRefrenceDGV = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CrossRefrenceDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(19, 10);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(311, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // CrossRefrenceDGV
            // 
            this.CrossRefrenceDGV.BackgroundColor = System.Drawing.Color.White;
            this.CrossRefrenceDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CrossRefrenceDGV.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.CrossRefrenceDGV.Location = new System.Drawing.Point(19, 34);
            this.CrossRefrenceDGV.Margin = new System.Windows.Forms.Padding(2);
            this.CrossRefrenceDGV.Name = "CrossRefrenceDGV";
            this.CrossRefrenceDGV.RowHeadersWidth = 51;
            this.CrossRefrenceDGV.RowTemplate.Height = 24;
            this.CrossRefrenceDGV.Size = new System.Drawing.Size(972, 90);
            this.CrossRefrenceDGV.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(363, 8);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(73, 20);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // CrossRefrence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 144);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CrossRefrenceDGV);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CrossRefrence";
            this.ShowIcon = false;
            this.Text = "CrossRefrence";
            ((System.ComponentModel.ISupportInitialize)(this.CrossRefrenceDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView CrossRefrenceDGV;
        public System.Windows.Forms.TextBox textBox1;
    }
}