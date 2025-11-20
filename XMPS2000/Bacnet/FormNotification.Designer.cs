using System;

namespace XMPS2000.Bacnet
{
    partial class FormNotification
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelInstanceNo = new System.Windows.Forms.Label();
            this.btnDeleteRec = new System.Windows.Forms.Button();
            this.checkToNormal = new System.Windows.Forms.CheckBox();
            this.checkToFault = new System.Windows.Forms.CheckBox();
            this.checktooffNormal = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddRcpt = new System.Windows.Forms.Button();
            this.dgRecipient = new System.Windows.Forms.DataGridView();
            this.clmSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TextNormalPriority = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextFaultPrio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextOffNormalPrt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelObjType = new System.Windows.Forms.Label();
            this.labelObjIdentifier = new System.Windows.Forms.Label();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.textObjectName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRecipient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelInstanceNo);
            this.groupBox1.Controls.Add(this.btnDeleteRec);
            this.groupBox1.Controls.Add(this.checkToNormal);
            this.groupBox1.Controls.Add(this.checkToFault);
            this.groupBox1.Controls.Add(this.checktooffNormal);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnAddRcpt);
            this.groupBox1.Controls.Add(this.dgRecipient);
            this.groupBox1.Controls.Add(this.TextNormalPriority);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TextFaultPrio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TextOffNormalPrt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelObjType);
            this.groupBox1.Controls.Add(this.labelObjIdentifier);
            this.groupBox1.Controls.Add(this.textDescription);
            this.groupBox1.Controls.Add(this.textObjectName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(-5, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(589, 689);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // labelInstanceNo
            // 
            this.labelInstanceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInstanceNo.Location = new System.Drawing.Point(199, 57);
            this.labelInstanceNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInstanceNo.Name = "labelInstanceNo";
            this.labelInstanceNo.Size = new System.Drawing.Size(314, 24);
            this.labelInstanceNo.TabIndex = 63;
            this.labelInstanceNo.Text = "8";
            // 
            // btnDeleteRec
            // 
            this.btnDeleteRec.Location = new System.Drawing.Point(261, 357);
            this.btnDeleteRec.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteRec.Name = "btnDeleteRec";
            this.btnDeleteRec.Size = new System.Drawing.Size(245, 31);
            this.btnDeleteRec.TabIndex = 62;
            this.btnDeleteRec.Text = "Delete Recipient -";
            this.btnDeleteRec.UseVisualStyleBackColor = true;
            this.btnDeleteRec.Click += new System.EventHandler(this.btnDeleteRec_Click);
            // 
            // checkToNormal
            // 
            this.checkToNormal.AutoSize = true;
            this.checkToNormal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkToNormal.Checked = true;
            this.checkToNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkToNormal.Location = new System.Drawing.Point(412, 318);
            this.checkToNormal.Margin = new System.Windows.Forms.Padding(4);
            this.checkToNormal.Name = "checkToNormal";
            this.checkToNormal.Size = new System.Drawing.Size(93, 20);
            this.checkToNormal.TabIndex = 61;
            this.checkToNormal.Text = "To normal:";
            this.checkToNormal.UseVisualStyleBackColor = true;
            this.checkToNormal.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checkToFault
            // 
            this.checkToFault.AutoSize = true;
            this.checkToFault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkToFault.Checked = true;
            this.checkToFault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkToFault.Location = new System.Drawing.Point(321, 318);
            this.checkToFault.Margin = new System.Windows.Forms.Padding(4);
            this.checkToFault.Name = "checkToFault";
            this.checkToFault.Size = new System.Drawing.Size(76, 20);
            this.checkToFault.TabIndex = 60;
            this.checkToFault.Text = "To fault:";
            this.checkToFault.UseVisualStyleBackColor = true;
            this.checkToFault.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // checktooffNormal
            // 
            this.checktooffNormal.AutoSize = true;
            this.checktooffNormal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checktooffNormal.Checked = true;
            this.checktooffNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checktooffNormal.Location = new System.Drawing.Point(199, 318);
            this.checktooffNormal.Margin = new System.Windows.Forms.Padding(4);
            this.checktooffNormal.Name = "checktooffNormal";
            this.checktooffNormal.Size = new System.Drawing.Size(107, 20);
            this.checktooffNormal.TabIndex = 59;
            this.checktooffNormal.Text = "To offnormal:";
            this.checktooffNormal.UseVisualStyleBackColor = true;
            this.checktooffNormal.CheckedChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 322);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 16);
            this.label12.TabIndex = 57;
            this.label12.Text = "Ack Required";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(271, 287);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 16);
            this.label11.TabIndex = 56;
            this.label11.Text = "(0... 255)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(271, 250);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 16);
            this.label10.TabIndex = 55;
            this.label10.Text = "(0... 255)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 213);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 54;
            this.label4.Text = "(0... 255)";
            // 
            // btnAddRcpt
            // 
            this.btnAddRcpt.Location = new System.Drawing.Point(8, 357);
            this.btnAddRcpt.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddRcpt.Name = "btnAddRcpt";
            this.btnAddRcpt.Size = new System.Drawing.Size(245, 31);
            this.btnAddRcpt.TabIndex = 53;
            this.btnAddRcpt.Text = "Add Recipient +";
            this.btnAddRcpt.UseVisualStyleBackColor = true;
            this.btnAddRcpt.Click += new System.EventHandler(this.btnAddRcpt_Click);
            // 
            // dgRecipient
            // 
            this.dgRecipient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRecipient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSelect});
            this.dgRecipient.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgRecipient.Location = new System.Drawing.Point(8, 395);
            this.dgRecipient.Margin = new System.Windows.Forms.Padding(4);
            this.dgRecipient.MultiSelect = false;
            this.dgRecipient.Name = "dgRecipient";
            this.dgRecipient.ReadOnly = true;
            this.dgRecipient.RowHeadersVisible = false;
            this.dgRecipient.RowHeadersWidth = 51;
            this.dgRecipient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRecipient.Size = new System.Drawing.Size(573, 287);
            this.dgRecipient.TabIndex = 52;
            this.dgRecipient.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRecipient_CellClick);
            this.dgRecipient.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRecipient_CellDoubleClick);
            // 
            // clmSelect
            // 
            this.clmSelect.HeaderText = "Select";
            this.clmSelect.MinimumWidth = 6;
            this.clmSelect.Name = "clmSelect";
            this.clmSelect.ReadOnly = true;
            this.clmSelect.Width = 125;
            // 
            // TextNormalPriority
            // 
            this.TextNormalPriority.Location = new System.Drawing.Point(199, 278);
            this.TextNormalPriority.Margin = new System.Windows.Forms.Padding(4);
            this.TextNormalPriority.MaxLength = 20;
            this.TextNormalPriority.Name = "TextNormalPriority";
            this.TextNormalPriority.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TextNormalPriority.Size = new System.Drawing.Size(63, 22);
            this.TextNormalPriority.TabIndex = 7;
            this.TextNormalPriority.Text = "255";
            this.TextNormalPriority.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextNormalPriority_KeyPress);
            this.TextNormalPriority.Validating += new System.ComponentModel.CancelEventHandler(this.TextNormalPriority_Validating);
            this.TextNormalPriority.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 287);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 51;
            this.label3.Text = "To Normal Priority";
            // 
            // TextFaultPrio
            // 
            this.TextFaultPrio.Location = new System.Drawing.Point(199, 241);
            this.TextFaultPrio.Margin = new System.Windows.Forms.Padding(4);
            this.TextFaultPrio.MaxLength = 20;
            this.TextFaultPrio.Name = "TextFaultPrio";
            this.TextFaultPrio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TextFaultPrio.Size = new System.Drawing.Size(63, 22);
            this.TextFaultPrio.TabIndex = 6;
            this.TextFaultPrio.Text = "255";
            this.TextFaultPrio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextFaultPrio_KeyPress);
            this.TextFaultPrio.Validating += new System.ComponentModel.CancelEventHandler(this.TextFaultPrio_Validating);
            this.TextFaultPrio.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 250);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 49;
            this.label2.Text = "To Fault Priority";
            // 
            // TextOffNormalPrt
            // 
            this.TextOffNormalPrt.Location = new System.Drawing.Point(199, 204);
            this.TextOffNormalPrt.Margin = new System.Windows.Forms.Padding(4);
            this.TextOffNormalPrt.MaxLength = 20;
            this.TextOffNormalPrt.Name = "TextOffNormalPrt";
            this.TextOffNormalPrt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TextOffNormalPrt.Size = new System.Drawing.Size(63, 22);
            this.TextOffNormalPrt.TabIndex = 5;
            this.TextOffNormalPrt.Text = "255";
            this.TextOffNormalPrt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextOffNormalPrt_KeyPress);
            this.TextOffNormalPrt.Validating += new System.ComponentModel.CancelEventHandler(this.TextOffNormalPrt_Validating);
            this.TextOffNormalPrt.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 213);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 47;
            this.label1.Text = "To Off Normal Priority";
            // 
            // labelObjType
            // 
            this.labelObjType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjType.Location = new System.Drawing.Point(199, 94);
            this.labelObjType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjType.Name = "labelObjType";
            this.labelObjType.Size = new System.Drawing.Size(314, 24);
            this.labelObjType.TabIndex = 45;
            this.labelObjType.Text = "8";
            // 
            // labelObjIdentifier
            // 
            this.labelObjIdentifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjIdentifier.Location = new System.Drawing.Point(199, 20);
            this.labelObjIdentifier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjIdentifier.Name = "labelObjIdentifier";
            this.labelObjIdentifier.Size = new System.Drawing.Size(314, 24);
            this.labelObjIdentifier.TabIndex = 43;
            this.labelObjIdentifier.Text = "000";
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(199, 167);
            this.textDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textDescription.MaxLength = 25;
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(313, 22);
            this.textDescription.TabIndex = 4;
            this.textDescription.Text = "Digital Input";
            this.textDescription.TextChanged += new System.EventHandler(this.textDescription_TextChanged);
            this.textDescription.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // this.textDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textDescription_KeyPress);
            // 
            // textObjectName
            // 
            this.textObjectName.Location = new System.Drawing.Point(199, 130);
            this.textObjectName.Margin = new System.Windows.Forms.Padding(4);
            this.textObjectName.MaxLength = 25;
            this.textObjectName.Name = "textObjectName";
            this.textObjectName.Size = new System.Drawing.Size(313, 22);
            this.textObjectName.TabIndex = 3;
            this.textObjectName.TextChanged += new System.EventHandler(this.textObjectName_TextChanged);
            //this.textObjectName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textObjectName_KeyPress);
            this.textObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textObjectName_Validating);
            this.textObjectName.TextChanged += new EventHandler(BacNetValidator.ControlChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 176);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 16);
            this.label9.TabIndex = 39;
            this.label9.Text = "Description";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 102);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 16);
            this.label8.TabIndex = 38;
            this.label8.Text = "Object Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 139);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 16);
            this.label7.TabIndex = 37;
            this.label7.Text = "Object Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 65);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "Instance Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Object Identifier";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(352, 699);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 31);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(468, 699);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 31);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FormNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(585, 729);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNotification";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRecipient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelObjType;
        private System.Windows.Forms.Label labelObjIdentifier;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.TextBox textObjectName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox TextNormalPriority;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextFaultPrio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextOffNormalPrt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgRecipient;
        private System.Windows.Forms.Button btnAddRcpt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkToNormal;
        private System.Windows.Forms.CheckBox checkToFault;
        private System.Windows.Forms.CheckBox checktooffNormal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmSelect;
        private System.Windows.Forms.Button btnDeleteRec;
        private System.Windows.Forms.Label labelInstanceNo;
    }
}