namespace XMPS2000
{
    partial class TraceWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.linklblAddVariable = new System.Windows.Forms.LinkLabel();
            this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(844, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "+";
            // 
            // linklblAddVariable
            // 
            this.linklblAddVariable.AutoSize = true;
            this.linklblAddVariable.Location = new System.Drawing.Point(868, 39);
            this.linklblAddVariable.Name = "linklblAddVariable";
            this.linklblAddVariable.Size = new System.Drawing.Size(67, 13);
            this.linklblAddVariable.TabIndex = 2;
            this.linklblAddVariable.TabStop = true;
            this.linklblAddVariable.Text = "Add Variable";
            this.linklblAddVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblAddVariable_LinkClicked);
            // 
            // entityCommand1
            // 
            this.entityCommand1.CommandTimeout = 0;
            this.entityCommand1.CommandTree = null;
            this.entityCommand1.Connection = null;
            this.entityCommand1.EnablePlanCaching = true;
            this.entityCommand1.Transaction = null;
            // 
            // TraceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 663);
            this.ControlBox = false;
            this.Controls.Add(this.linklblAddVariable);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TraceWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linklblAddVariable;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
    }
}