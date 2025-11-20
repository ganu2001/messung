using System;

namespace XMPS2000
{
    partial class UdfbInfoPopupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox groupIO;
        private System.Windows.Forms.ListView lvIO;
        private System.Windows.Forms.GroupBox groupTags;
        private System.Windows.Forms.ListView lvTags;
        private System.Windows.Forms.ToolTip toolTip;

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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.groupTags = new System.Windows.Forms.GroupBox();
            this.groupIO = new System.Windows.Forms.GroupBox();
            this.lvIO = new System.Windows.Forms.ListView();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.groupTags.SuspendLayout();
            this.groupIO.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlHeader.Controls.Add(this.lblDescription);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlHeader.Size = new System.Drawing.Size(800, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Location = new System.Drawing.Point(20, 45);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(254, 15);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "User Defined Function Block Information";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(193, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "UDFB: [Name Here]";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.groupTags);
            this.pnlMain.Controls.Add(this.groupIO);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 80);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.pnlMain.Size = new System.Drawing.Size(800, 420);
            this.pnlMain.TabIndex = 1;
            // 
            // groupTags
            // 
            this.groupTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupTags.Controls.Add(this.lvTags);
            this.groupTags.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupTags.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.groupTags.Location = new System.Drawing.Point(20, 178);
            this.groupTags.Name = "groupTags";
            this.groupTags.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this.groupTags.Size = new System.Drawing.Size(760, 222);
            this.groupTags.TabIndex = 3;
            this.groupTags.TabStop = false;
            this.groupTags.Text = "📋 Tags Used";
            this.lvTags = new System.Windows.Forms.ListView();
            // 
            // lvTags
            // 
            this.lvTags.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.lvTags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTags.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvTags.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lvTags.FullRowSelect = true;
            this.lvTags.GridLines = true;
            this.lvTags.HideSelection = false;
            this.lvTags.View = System.Windows.Forms.View.Details;
            //this.toolTip.SetToolTip(this.lvTags, "Detailed list of tags with properties");
            this.groupTags.Controls.Add(this.lvTags);

            // 
            // groupIO
            // 
            this.groupIO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupIO.Controls.Add(this.lvIO);
            this.groupIO.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupIO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.groupIO.Location = new System.Drawing.Point(20, 20);
            this.groupIO.Name = "groupIO";
            this.groupIO.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this.groupIO.Size = new System.Drawing.Size(760, 148);
            this.groupIO.TabIndex = 2;
            this.groupIO.TabStop = false;
            this.groupIO.Text = "🔗 Inputs & Outputs";
            // 
            // lvIO
            // 
            this.lvIO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lvIO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIO.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvIO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lvIO.FullRowSelect = true;
            this.lvIO.GridLines = true;
            this.lvIO.HideSelection = false;
            this.lvIO.Location = new System.Drawing.Point(10, 23);
            this.lvIO.MultiSelect = false;
            this.lvIO.Name = "lvIO";
            this.lvIO.Size = new System.Drawing.Size(740, 115);
            this.lvIO.TabIndex = 0;
            //this.toolTip.SetToolTip(this.lvIO, "Input and Output parameters for this UDFB");
            this.lvIO.UseCompatibleStateImageBehavior = false;
            this.lvIO.View = System.Windows.Forms.View.Details;

            // 
            // UdfbInfoPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UdfbInfoPopupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UDFB Information - XMPS2000";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.groupTags.ResumeLayout(false);
            this.groupIO.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region Optional: Add resize handling for better proportions

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (pnlMain != null && groupIO != null && groupTags != null)
            {
                int availableHeight = pnlMain.Height - 40; // Subtract padding
                int ioHeight = (int)(availableHeight * 0.4); // 40% for I/O
                int tagsHeight = (int)(availableHeight * 0.6); // 60% for Tags
                int spacing = 10; // Space between controls

                // Adjust I/O group
                groupIO.Height = ioHeight;

                // Adjust Tags group
                groupTags.Top = groupIO.Bottom + spacing;
                groupTags.Height = tagsHeight - spacing;
            }
        }

        #endregion

        #endregion
    }
}