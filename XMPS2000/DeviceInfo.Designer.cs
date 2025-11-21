namespace XMPS2000
{
    partial class DeviceInfo
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
            this.labelModelName = new System.Windows.Forms.Label();
            this.labelComPort = new System.Windows.Forms.Label();
            this.labelEthPort = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelComp = new System.Windows.Forms.Label();
            this.labelEthP = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Device_Details = new System.Windows.Forms.TabPage();
            this.IO_Count = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Expansion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Module_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.On_Board = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expansion1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonok = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.Device_Details.SuspendLayout();
            this.IO_Count.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelModelName
            // 
            this.labelModelName.AutoSize = true;
            this.labelModelName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelModelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModelName.Location = new System.Drawing.Point(8, 7);
            this.labelModelName.Name = "labelModelName";
            this.labelModelName.Size = new System.Drawing.Size(94, 16);
            this.labelModelName.TabIndex = 0;
            this.labelModelName.Text = "Model Name  :";
            // 
            // labelComPort
            // 
            this.labelComPort.AutoSize = true;
            this.labelComPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComPort.Location = new System.Drawing.Point(8, 33);
            this.labelComPort.Name = "labelComPort";
            this.labelComPort.Size = new System.Drawing.Size(95, 16);
            this.labelComPort.TabIndex = 1;
            this.labelComPort.Text = "Com Port          :";
            // 
            // labelEthPort
            // 
            this.labelEthPort.AutoSize = true;
            this.labelEthPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEthPort.Location = new System.Drawing.Point(206, 35);
            this.labelEthPort.Name = "labelEthPort";
            this.labelEthPort.Size = new System.Drawing.Size(101, 16);
            this.labelEthPort.TabIndex = 2;
            this.labelEthPort.Text = "ETH Port          :  ";
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModel.Location = new System.Drawing.Point(109, 7);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(44, 16);
            this.labelModel.TabIndex = 7;
            this.labelModel.Text = "label1";
            // 
            // labelComp
            // 
            this.labelComp.AutoSize = true;
            this.labelComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComp.Location = new System.Drawing.Point(109, 35);
            this.labelComp.Name = "labelComp";
            this.labelComp.Size = new System.Drawing.Size(65, 16);
            this.labelComp.TabIndex = 8;
            this.labelComp.Text = "1 (RS485)";
            // 
            // labelEthP
            // 
            this.labelEthP.AutoSize = true;
            this.labelEthP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEthP.Location = new System.Drawing.Point(307, 35);
            this.labelEthP.Name = "labelEthP";
            this.labelEthP.Size = new System.Drawing.Size(14, 16);
            this.labelEthP.TabIndex = 9;
            this.labelEthP.Text = "1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Device_Details);
            this.tabControl1.Controls.Add(this.IO_Count);
            this.tabControl1.Location = new System.Drawing.Point(12, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(460, 217);
            this.tabControl1.TabIndex = 10;
            // 
            // Device_Details
            // 
            this.Device_Details.Controls.Add(this.dataGridView1);
            this.Device_Details.Location = new System.Drawing.Point(4, 22);
            this.Device_Details.Name = "Device_Details";
            this.Device_Details.Padding = new System.Windows.Forms.Padding(3);
            this.Device_Details.Size = new System.Drawing.Size(452, 191);
            this.Device_Details.TabIndex = 0;
            this.Device_Details.Text = "Device Details";
            this.Device_Details.UseVisualStyleBackColor = true;
            // 
            // IO_Count
            // 
            this.IO_Count.Controls.Add(this.dataGridView2);
            this.IO_Count.Location = new System.Drawing.Point(4, 22);
            this.IO_Count.Name = "IO_Count";
            this.IO_Count.Padding = new System.Windows.Forms.Padding(3);
            this.IO_Count.Size = new System.Drawing.Size(452, 191);
            this.IO_Count.TabIndex = 1;
            this.IO_Count.Text = "IO Count";
            this.IO_Count.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Expansion,
            this.Total_Quantity});
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(243, 161);
            this.dataGridView1.TabIndex = 5;
            // 
            // Expansion
            // 
            this.Expansion.HeaderText = "Expansion";
            this.Expansion.Name = "Expansion";
            // 
            // Total_Quantity
            // 
            this.Total_Quantity.HeaderText = "Total Quantity";
            this.Total_Quantity.Name = "Total_Quantity";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Module_Name,
            this.On_Board,
            this.Expansion1,
            this.Total});
            this.dataGridView2.Location = new System.Drawing.Point(3, 11);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(443, 172);
            this.dataGridView2.TabIndex = 7;
            // 
            // Module_Name
            // 
            this.Module_Name.HeaderText = "Module Name";
            this.Module_Name.Name = "Module_Name";
            // 
            // On_Board
            // 
            this.On_Board.HeaderText = "On Board";
            this.On_Board.Name = "On_Board";
            // 
            // Expansion1
            // 
            this.Expansion1.HeaderText = "Expansion";
            this.Expansion1.Name = "Expansion1";
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            // 
            // buttonok
            // 
            this.buttonok.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonok.Location = new System.Drawing.Point(393, 287);
            this.buttonok.Name = "buttonok";
            this.buttonok.Size = new System.Drawing.Size(75, 23);
            this.buttonok.TabIndex = 11;
            this.buttonok.Text = "OK";
            this.buttonok.UseVisualStyleBackColor = false;
            this.buttonok.Click += new System.EventHandler(this.buttonok_Click);
            // 
            // DeviceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 320);
            this.Controls.Add(this.buttonok);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelEthP);
            this.Controls.Add(this.labelComp);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.labelEthPort);
            this.Controls.Add(this.labelComPort);
            this.Controls.Add(this.labelModelName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeviceInfo";
            this.ShowIcon = false;
            this.Text = "DeviceInfo";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DeviceInfo_Paint);
            this.tabControl1.ResumeLayout(false);
            this.Device_Details.ResumeLayout(false);
            this.IO_Count.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelModelName;
        private System.Windows.Forms.Label labelComPort;
        private System.Windows.Forms.Label labelEthPort;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelComp;
        private System.Windows.Forms.Label labelEthP;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Device_Details;
        private System.Windows.Forms.TabPage IO_Count;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expansion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total_Quantity;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn On_Board;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expansion1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.Button buttonok;
    }
}