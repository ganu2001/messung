using System.Windows.Forms;

namespace XMPS2000.LadderLogic
{
    partial class FunctionBlockInputsAndOutputs
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTopic = new System.Windows.Forms.ComboBox();
            this.labelTopic = new System.Windows.Forms.Label();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDataType = new System.Windows.Forms.ComboBox();
            this.comboBoxInstruction = new System.Windows.Forms.ComboBox();
            this.comboBoxInstructionType = new System.Windows.Forms.ComboBox();
            this.buttonFBfromClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.datagridFunctionBlockIn = new System.Windows.Forms.DataGridView();
            this.Input_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datatype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operand_Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Tag_For_Operand = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Tag_Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.datagridFunctionBlockOut = new System.Windows.Forms.DataGridView();
            this.output = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outdatatype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outputoperand = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.outputTag = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.OPTagaddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblerror = new System.Windows.Forms.Label();
            this.btnFBAddnew = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridFunctionBlockIn)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridFunctionBlockOut)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTopic);
            this.groupBox1.Controls.Add(this.labelTopic);
            this.groupBox1.Controls.Add(this.checkBoxEnable);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxDataType);
            this.groupBox1.Controls.Add(this.comboBoxInstruction);
            this.groupBox1.Controls.Add(this.comboBoxInstructionType);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(581, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // cmbTopic
            // 
            this.cmbTopic.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTopic.FormattingEnabled = true;
            this.cmbTopic.Location = new System.Drawing.Point(392, 61);
            this.cmbTopic.Name = "cmbTopic";
            this.cmbTopic.Size = new System.Drawing.Size(177, 21);
            this.cmbTopic.TabIndex = 8;
            // 
            // labelTopic
            // 
            this.labelTopic.AutoSize = true;
            this.labelTopic.Location = new System.Drawing.Point(336, 64);
            this.labelTopic.Name = "labelTopic";
            this.labelTopic.Size = new System.Drawing.Size(34, 13);
            this.labelTopic.TabIndex = 7;
            this.labelTopic.Text = "Topic";
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Location = new System.Drawing.Point(224, 62);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(59, 17);
            this.checkBoxEnable.TabIndex = 6;
            this.checkBoxEnable.Text = "Enable";
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Instructions";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(442, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Data Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type";
            // 
            // comboBoxDataType
            // 
            this.comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataType.FormattingEnabled = true;
            this.comboBoxDataType.Location = new System.Drawing.Point(391, 34);
            this.comboBoxDataType.Name = "comboBoxDataType";
            this.comboBoxDataType.Size = new System.Drawing.Size(178, 21);
            this.comboBoxDataType.TabIndex = 2;
            this.comboBoxDataType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataType_SelectedIndexChanged);
            // 
            // comboBoxInstruction
            // 
            this.comboBoxInstruction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInstruction.FormattingEnabled = true;
            this.comboBoxInstruction.Location = new System.Drawing.Point(210, 34);
            this.comboBoxInstruction.Name = "comboBoxInstruction";
            this.comboBoxInstruction.Size = new System.Drawing.Size(166, 21);
            this.comboBoxInstruction.TabIndex = 1;
            this.comboBoxInstruction.SelectedIndexChanged += new System.EventHandler(this.comboBoxInstruction_SelectedIndexChanged);
            // 
            // comboBoxInstructionType
            // 
            this.comboBoxInstructionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInstructionType.FormattingEnabled = true;
            this.comboBoxInstructionType.Location = new System.Drawing.Point(20, 34);
            this.comboBoxInstructionType.Name = "comboBoxInstructionType";
            this.comboBoxInstructionType.Size = new System.Drawing.Size(169, 21);
            this.comboBoxInstructionType.TabIndex = 0;
            this.comboBoxInstructionType.SelectedIndexChanged += new System.EventHandler(this.comboBoxInstructionType_SelectedIndexChanged);
            // 
            // buttonFBfromClose
            // 
            this.buttonFBfromClose.Location = new System.Drawing.Point(490, 375);
            this.buttonFBfromClose.Name = "buttonFBfromClose";
            this.buttonFBfromClose.Size = new System.Drawing.Size(94, 23);
            this.buttonFBfromClose.TabIndex = 3;
            this.buttonFBfromClose.Text = "Close";
            this.buttonFBfromClose.UseVisualStyleBackColor = true;
            this.buttonFBfromClose.Click += new System.EventHandler(this.buttonFBfromClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 94);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(613, 272);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(605, 246);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "INPUT";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.datagridFunctionBlockIn);
            this.groupBox2.Location = new System.Drawing.Point(10, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(589, 213);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Inputs";
            // 
            // datagridFunctionBlockIn
            // 
            this.datagridFunctionBlockIn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridFunctionBlockIn.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.datagridFunctionBlockIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridFunctionBlockIn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Input_1,
            this.Datatype,
            this.Operand_Type,
            this.Tag_For_Operand,
            this.Tag_Address});
            this.datagridFunctionBlockIn.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.datagridFunctionBlockIn.Location = new System.Drawing.Point(6, 19);
            this.datagridFunctionBlockIn.Name = "datagridFunctionBlockIn";
            this.datagridFunctionBlockIn.ShowEditingIcon = false;
            this.datagridFunctionBlockIn.Size = new System.Drawing.Size(543, 180);
            this.datagridFunctionBlockIn.TabIndex = 7;
            this.datagridFunctionBlockIn.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.datagridFunctionBlockIn.CurrentCellDirtyStateChanged += new System.EventHandler(this.datagridFunctionBlock_CurrentCellDirtyStateChanged);
            this.datagridFunctionBlockIn.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.datagridFunctionBlockIn_DataError);
            this.datagridFunctionBlockIn.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridFunctionBlock_EditingControlShowing);
            // 
            // Input_1
            // 
            this.Input_1.HeaderText = "Input";
            this.Input_1.Name = "Input_1";
            this.Input_1.ReadOnly = true;
            this.Input_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Datatype
            // 
            this.Datatype.HeaderText = "Datatype";
            this.Datatype.Name = "Datatype";
            this.Datatype.ReadOnly = true;
            this.Datatype.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Operand_Type
            // 
            this.Operand_Type.HeaderText = "Operand_Type";
            this.Operand_Type.Name = "Operand_Type";
            // 
            // Tag_For_Operand
            // 
            this.Tag_For_Operand.AutoComplete = false;
            this.Tag_For_Operand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Tag_For_Operand.HeaderText = "Tag_For_Operand";
            this.Tag_For_Operand.Name = "Tag_For_Operand";
            // 
            // Tag_Address
            // 
            this.Tag_Address.HeaderText = "Tag_Value";
            this.Tag_Address.Name = "Tag_Address";
            this.Tag_Address.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(605, 246);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "OUTPUT";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.datagridFunctionBlockOut);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Location = new System.Drawing.Point(9, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(590, 206);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Outputs";
            // 
            // datagridFunctionBlockOut
            // 
            this.datagridFunctionBlockOut.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.datagridFunctionBlockOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridFunctionBlockOut.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.output,
            this.outdatatype,
            this.outputoperand,
            this.outputTag,
            this.OPTagaddress});
            this.datagridFunctionBlockOut.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.datagridFunctionBlockOut.Location = new System.Drawing.Point(7, 19);
            this.datagridFunctionBlockOut.Name = "datagridFunctionBlockOut";
            this.datagridFunctionBlockOut.Size = new System.Drawing.Size(543, 180);
            this.datagridFunctionBlockOut.TabIndex = 14;
            this.datagridFunctionBlockOut.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridFunctionBlockOut_CellValueChanged);
            this.datagridFunctionBlockOut.CurrentCellDirtyStateChanged += new System.EventHandler(this.datagridFunctionBlockOut_CurrentCellDirtyStateChanged);
            this.datagridFunctionBlockOut.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.datagridFunctionBlockOut_DataError);
            this.datagridFunctionBlockOut.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridFunctionBlockOut_EditingControlShowing);
            // 
            // output
            // 
            this.output.HeaderText = "Output";
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.output.Width = 70;
            // 
            // outdatatype
            // 
            this.outdatatype.HeaderText = "Datatype";
            this.outdatatype.Name = "outdatatype";
            this.outdatatype.ReadOnly = true;
            this.outdatatype.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // outputoperand
            // 
            this.outputoperand.HeaderText = "Operand_Type";
            this.outputoperand.Name = "outputoperand";
            // 
            // outputTag
            // 
            this.outputTag.AutoComplete = false;
            this.outputTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.outputTag.HeaderText = "Tag_For_Operand";
            this.outputTag.Name = "outputTag";
            // 
            // OPTagaddress
            // 
            this.OPTagaddress.HeaderText = "Tag_Value";
            this.OPTagaddress.Name = "OPTagaddress";
            this.OPTagaddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(364, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 13);
            this.label13.TabIndex = 13;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(212, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(0, 13);
            this.label14.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(78, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 13);
            this.label15.TabIndex = 8;
            // 
            // lblerror
            // 
            this.lblerror.AutoSize = true;
            this.lblerror.ForeColor = System.Drawing.Color.Red;
            this.lblerror.Location = new System.Drawing.Point(20, 364);
            this.lblerror.Name = "lblerror";
            this.lblerror.Size = new System.Drawing.Size(0, 13);
            this.lblerror.TabIndex = 8;
            // 
            // btnFBAddnew
            // 
            this.btnFBAddnew.Location = new System.Drawing.Point(380, 375);
            this.btnFBAddnew.Name = "btnFBAddnew";
            this.btnFBAddnew.Size = new System.Drawing.Size(94, 23);
            this.btnFBAddnew.TabIndex = 9;
            this.btnFBAddnew.Text = "Add";
            this.btnFBAddnew.UseVisualStyleBackColor = true;
            this.btnFBAddnew.Click += new System.EventHandler(this.btnFBAddnew_Click);
            // 
            // FunctionBlockInputsAndOutputs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnFBAddnew);
            this.Controls.Add(this.lblerror);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonFBfromClose);
            this.Controls.Add(this.groupBox1);
            this.Name = "FunctionBlockInputsAndOutputs";
            this.Size = new System.Drawing.Size(619, 412);
            this.Load += new System.EventHandler(this.FunctionBlockInputsAndOutputs_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridFunctionBlockIn)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridFunctionBlockOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDataType;
        private System.Windows.Forms.ComboBox comboBoxInstruction;
        private System.Windows.Forms.ComboBox comboBoxInstructionType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonFBfromClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView datagridFunctionBlockIn;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private Label lblerror;
        private DataGridView datagridFunctionBlockOut;
        private Button btnFBAddnew;
        private CheckBox checkBoxEnable;
        private ComboBox cmbTopic;
        private Label labelTopic;
        private DataGridViewTextBoxColumn Input_1;
        private DataGridViewTextBoxColumn Datatype;
        private DataGridViewComboBoxColumn Operand_Type;
        private DataGridViewComboBoxColumn Tag_For_Operand;
        private DataGridViewTextBoxColumn Tag_Address;
        private DataGridViewTextBoxColumn output;
        private DataGridViewTextBoxColumn outdatatype;
        private DataGridViewComboBoxColumn outputoperand;
        private DataGridViewComboBoxColumn outputTag;
        private DataGridViewTextBoxColumn OPTagaddress;
    }
}