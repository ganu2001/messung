namespace XMPS2000
{
    partial class FindAndReplace
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
            this.lblFindWhat = new System.Windows.Forms.Label();
            this.lblReplaceWith = new System.Windows.Forms.Label();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Findcmbox = new System.Windows.Forms.ComboBox();
            this.Replacecmbox = new System.Windows.Forms.ComboBox();
            this.CheckForLogicblock = new System.Windows.Forms.ComboBox();
            this.lblLogicblkName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFindWhat
            // 
            this.lblFindWhat.AutoSize = true;
            this.lblFindWhat.Location = new System.Drawing.Point(12, 17);
            this.lblFindWhat.Name = "lblFindWhat";
            this.lblFindWhat.Size = new System.Drawing.Size(53, 13);
            this.lblFindWhat.TabIndex = 0;
            this.lblFindWhat.Text = "Find what";
            // 
            // lblReplaceWith
            // 
            this.lblReplaceWith.AutoSize = true;
            this.lblReplaceWith.Location = new System.Drawing.Point(12, 47);
            this.lblReplaceWith.Name = "lblReplaceWith";
            this.lblReplaceWith.Size = new System.Drawing.Size(69, 13);
            this.lblReplaceWith.TabIndex = 1;
            this.lblReplaceWith.Text = "Replace with";
            this.lblReplaceWith.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(259, 12);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(75, 23);
            this.btnFindNext.TabIndex = 6;
            this.btnFindNext.Text = "Find next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(259, 42);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 7;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(259, 70);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(75, 23);
            this.btnReplaceAll.TabIndex = 8;
            this.btnReplaceAll.Text = "Replace all";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(259, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Findcmbox
            // 
            this.Findcmbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Findcmbox.FormattingEnabled = true;
            this.Findcmbox.Location = new System.Drawing.Point(103, 9);
            this.Findcmbox.Margin = new System.Windows.Forms.Padding(2);
            this.Findcmbox.Name = "Findcmbox";
            this.Findcmbox.Size = new System.Drawing.Size(151, 21);
            this.Findcmbox.TabIndex = 10;
            this.Findcmbox.SelectedIndexChanged += new System.EventHandler(this.Findcmbox_SelectedIndexChanged);
            // 
            // Replacecmbox
            // 
            this.Replacecmbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Replacecmbox.FormattingEnabled = true;
            this.Replacecmbox.Location = new System.Drawing.Point(103, 39);
            this.Replacecmbox.Margin = new System.Windows.Forms.Padding(2);
            this.Replacecmbox.Name = "Replacecmbox";
            this.Replacecmbox.Size = new System.Drawing.Size(151, 21);
            this.Replacecmbox.TabIndex = 11;
            // 
            // CheckForLogicblock
            // 
            this.CheckForLogicblock.AutoCompleteCustomSource.AddRange(new string[] {
            "Current LogicBlock",
            "All LogicBlock"});
            this.CheckForLogicblock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CheckForLogicblock.FormattingEnabled = true;
            this.CheckForLogicblock.Items.AddRange(new object[] {
            "Current LogicBlock",
            "Entire LogicBlock",
            "Entire Project"});
            this.CheckForLogicblock.Location = new System.Drawing.Point(103, 72);
            this.CheckForLogicblock.Margin = new System.Windows.Forms.Padding(2);
            this.CheckForLogicblock.Name = "CheckForLogicblock";
            this.CheckForLogicblock.Size = new System.Drawing.Size(151, 21);
            this.CheckForLogicblock.TabIndex = 12;
            this.CheckForLogicblock.SelectedIndexChanged += new System.EventHandler(this.CheckForLogicblock_SelectedIndexChanged);
            // 
            // lblLogicblkName
            // 
            this.lblLogicblkName.AutoSize = true;
            this.lblLogicblkName.Location = new System.Drawing.Point(12, 80);
            this.lblLogicblkName.Name = "lblLogicblkName";
            this.lblLogicblkName.Size = new System.Drawing.Size(38, 13);
            this.lblLogicblkName.TabIndex = 13;
            this.lblLogicblkName.Text = "Scope";
            // 
            // FindAndReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 129);
            this.ControlBox = false;
            this.Controls.Add(this.lblLogicblkName);
            this.Controls.Add(this.CheckForLogicblock);
            this.Controls.Add(this.Replacecmbox);
            this.Controls.Add(this.Findcmbox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.lblReplaceWith);
            this.Controls.Add(this.lblFindWhat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindAndReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find And Replace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindAndReplace_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFindWhat;
        private System.Windows.Forms.Label lblReplaceWith;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ComboBox Findcmbox;
        private System.Windows.Forms.ComboBox Replacecmbox;
        private System.Windows.Forms.ComboBox CheckForLogicblock;
        private System.Windows.Forms.Label lblLogicblkName;
    }
}