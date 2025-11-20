namespace XMPS2000.LadderLogic
{
    partial class ForceBitValue
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTrue = new System.Windows.Forms.Button();
            this.btnFalse = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btnUnforce = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTrue
            // 
            this.btnTrue.Location = new System.Drawing.Point(15, 11);
            this.btnTrue.Name = "btnTrue";
            this.btnTrue.Size = new System.Drawing.Size(75, 23);
            this.btnTrue.TabIndex = 0;
            this.btnTrue.Text = "TRUE";
            this.btnTrue.UseVisualStyleBackColor = true;
            this.btnTrue.Click += new System.EventHandler(this.btnTrue_Click);
            // 
            // btnFalse
            // 
            this.btnFalse.Location = new System.Drawing.Point(15, 40);
            this.btnFalse.Name = "btnFalse";
            this.btnFalse.Size = new System.Drawing.Size(75, 23);
            this.btnFalse.TabIndex = 1;
            this.btnFalse.Text = "FALSE";
            this.btnFalse.UseVisualStyleBackColor = true;
            this.btnFalse.Click += new System.EventHandler(this.btnFalse_Click);
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(96, 11);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(40, 23);
            this.btn1.TabIndex = 2;
            this.btn1.Text = "[ 1 ]";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn0
            // 
            this.btn0.Location = new System.Drawing.Point(96, 40);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(40, 23);
            this.btn0.TabIndex = 3;
            this.btn0.Text = "[ 0 ]";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btn0_Click);
            // 
            // btnUnforce
            // 
            this.btnUnforce.Location = new System.Drawing.Point(15, 69);
            this.btnUnforce.Name = "btnUnforce";
            this.btnUnforce.Size = new System.Drawing.Size(75, 23);
            this.btnUnforce.TabIndex = 4;
            this.btnUnforce.Text = "UNFORCE";
            this.btnUnforce.UseVisualStyleBackColor = true;
            this.btnUnforce.Click += new System.EventHandler(this.btnUnforce_Click);
            // 
            // ForceBitValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUnforce);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btnFalse);
            this.Controls.Add(this.btnTrue);
            this.Name = "ForceBitValue";
            this.Size = new System.Drawing.Size(150, 102);
            this.Leave += new System.EventHandler(this.ForceBitValue_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTrue;
        private System.Windows.Forms.Button btnFalse;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btnUnforce;
    }
}
