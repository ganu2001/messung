using XMPS2000.Core.Base.Helpers;

namespace XMPS2000
{
    partial class IOConfigAdd
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxIOType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.labelTag = new System.Windows.Forms.Label();
            this.textBoxLogicalAddress = new System.Windows.Forms.TextBox();
            this.labelLogicalAddress = new System.Windows.Forms.Label();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.labelLabel = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxIOType);
            this.groupBox1.Controls.Add(this.labelType);
            this.groupBox1.Controls.Add(this.textBoxTag);
            this.groupBox1.Controls.Add(this.labelTag);
            this.groupBox1.Controls.Add(this.textBoxLogicalAddress);
            this.groupBox1.Controls.Add(this.labelLogicalAddress);
            this.groupBox1.Controls.Add(this.textBoxLabel);
            this.groupBox1.Controls.Add(this.labelLabel);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 183);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "I/O Mapping";
            // 
            // comboBoxIOType
            // 
            this.comboBoxIOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIOType.FormattingEnabled = true;
            this.comboBoxIOType.Location = new System.Drawing.Point(133, 38);
            this.comboBoxIOType.Name = "comboBoxIOType";
            this.comboBoxIOType.Size = new System.Drawing.Size(105, 21);
            this.comboBoxIOType.TabIndex = 52;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(30, 46);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(31, 13);
            this.labelType.TabIndex = 51;
            this.labelType.Text = "Type";
            // 
            // textBoxTag
            // 
            this.textBoxTag.Location = new System.Drawing.Point(133, 141);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(105, 20);
            this.textBoxTag.TabIndex = 47;
            // 
            // labelTag
            // 
            this.labelTag.AutoSize = true;
            this.labelTag.Location = new System.Drawing.Point(30, 148);
            this.labelTag.Name = "labelTag";
            this.labelTag.Size = new System.Drawing.Size(26, 13);
            this.labelTag.TabIndex = 50;
            this.labelTag.Text = "Tag";
            // 
            // textBoxLogicalAddress
            // 
            this.textBoxLogicalAddress.Location = new System.Drawing.Point(133, 110);
            this.textBoxLogicalAddress.Name = "textBoxLogicalAddress";
            this.textBoxLogicalAddress.Size = new System.Drawing.Size(98, 20);
            this.textBoxLogicalAddress.TabIndex = 46;
            this.textBoxLogicalAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxLogicalAddress_Validating);
            // 
            // labelLogicalAddress
            // 
            this.labelLogicalAddress.AutoSize = true;
            this.labelLogicalAddress.Location = new System.Drawing.Point(30, 113);
            this.labelLogicalAddress.Name = "labelLogicalAddress";
            this.labelLogicalAddress.Size = new System.Drawing.Size(82, 13);
            this.labelLogicalAddress.TabIndex = 49;
            this.labelLogicalAddress.Text = "Logical Address";
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.Location = new System.Drawing.Point(135, 75);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.Size = new System.Drawing.Size(103, 20);
            this.textBoxLabel.TabIndex = 45;
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(30, 78);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(33, 13);
            this.labelLabel.TabIndex = 48;
            this.labelLabel.Text = "Label";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(113, 201);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // IOConfigAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSave);
            this.MaximumSize = new System.Drawing.Size(300, 266);
            this.MinimumSize = new System.Drawing.Size(300, 266);
            this.Name = "IOConfigAdd";
            this.Size = new System.Drawing.Size(300, 266);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSave;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.Label labelTag;
        private System.Windows.Forms.TextBox textBoxLogicalAddress;
        private System.Windows.Forms.Label labelLogicalAddress;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxIOType;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
