using System.Windows.Forms.VisualStyles;

namespace XMPS2000
{
    partial class frmHSIOConfigeration
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
            this.Tabcntrl = new System.Windows.Forms.TabControl();
            this.Input = new System.Windows.Forms.TabPage();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.tabIntInput = new System.Windows.Forms.TabPage();
            this.Interrupt_Input = new System.Windows.Forms.GroupBox();
            this.lblType4 = new System.Windows.Forms.Label();
            this.comboBoxSelectInLogicBlock4 = new System.Windows.Forms.ComboBox();
            this.labelInterruptLoagicBlock4 = new System.Windows.Forms.Label();
            this.textBoxExternalEvent4 = new System.Windows.Forms.TextBox();
            this.labelExternalEvent4 = new System.Windows.Forms.Label();
            this.comboBoxType4 = new System.Windows.Forms.ComboBox();
            this.lblType3 = new System.Windows.Forms.Label();
            this.comboBoxSelectInLogicBlock3 = new System.Windows.Forms.ComboBox();
            this.labelInterruptLoagicBlock3 = new System.Windows.Forms.Label();
            this.textBoxExternalEvent3 = new System.Windows.Forms.TextBox();
            this.labelExternalEvent3 = new System.Windows.Forms.Label();
            this.comboBoxType3 = new System.Windows.Forms.ComboBox();
            this.comboBoxSelectInLogicBlock2 = new System.Windows.Forms.ComboBox();
            this.labelInterruptLoagicBlock2 = new System.Windows.Forms.Label();
            this.textBoxExternalEvent2 = new System.Windows.Forms.TextBox();
            this.lblSelectEvent2 = new System.Windows.Forms.Label();
            this.lblType2 = new System.Windows.Forms.Label();
            this.comboBoxType2 = new System.Windows.Forms.ComboBox();
            this.comboBoxSelectInLogicBlock1 = new System.Windows.Forms.ComboBox();
            this.labelInterruptLoagicBlock1 = new System.Windows.Forms.Label();
            this.textBoxExternalEvent1 = new System.Windows.Forms.TextBox();
            this.lblSelectEvent1 = new System.Windows.Forms.Label();
            this.lblType1 = new System.Windows.Forms.Label();
            this.comboBoxType1 = new System.Windows.Forms.ComboBox();
            this.HSIOsOMtimer = new System.Windows.Forms.Timer(this.components);
            this.HSToolTipTimer = new System.Windows.Forms.Timer(this.components);
            this.Tabcntrl.SuspendLayout();
            this.tabIntInput.SuspendLayout();
            this.Interrupt_Input.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabcntrl
            // 
            this.Tabcntrl.Controls.Add(this.Input);
            this.Tabcntrl.Controls.Add(this.tabOutput);
            this.Tabcntrl.Controls.Add(this.tabIntInput);
            this.Tabcntrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabcntrl.Location = new System.Drawing.Point(0, 0);
            this.Tabcntrl.Multiline = true;
            this.Tabcntrl.Name = "Tabcntrl";
            this.Tabcntrl.SelectedIndex = 0;
            this.Tabcntrl.Size = new System.Drawing.Size(1300, 788);
            this.Tabcntrl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Tabcntrl.TabIndex = 1;
            this.Tabcntrl.TabIndexChanged += new System.EventHandler(this.Tabcntrl_TabIndexChanged);
            this.Tabcntrl.Click += new System.EventHandler(this.Tabcntrl_Click);
            // 
            // Input
            // 
            this.Input.Location = new System.Drawing.Point(4, 22);
            this.Input.Name = "Input";
            this.Input.Padding = new System.Windows.Forms.Padding(3);
            this.Input.Size = new System.Drawing.Size(1292, 762);
            this.Input.TabIndex = 0;
            this.Input.Text = "Input";
            this.Input.UseVisualStyleBackColor = true;
            // 
            // tabOutput
            // 
            this.tabOutput.Location = new System.Drawing.Point(4, 22);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(1292, 762);
            this.tabOutput.TabIndex = 1;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // tabIntInput
            // 
            this.tabIntInput.Controls.Add(this.Interrupt_Input);
            this.tabIntInput.Location = new System.Drawing.Point(4, 22);
            this.tabIntInput.Name = "tabIntInput";
            this.tabIntInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabIntInput.Size = new System.Drawing.Size(1292, 762);
            this.tabIntInput.TabIndex = 2;
            this.tabIntInput.Text = "Interrupt Input";
            this.tabIntInput.UseVisualStyleBackColor = true;
            // 
            // Interrupt_Input
            // 
            this.Interrupt_Input.Controls.Add(this.lblType4);
            this.Interrupt_Input.Controls.Add(this.comboBoxSelectInLogicBlock4);
            this.Interrupt_Input.Controls.Add(this.labelInterruptLoagicBlock4);
            this.Interrupt_Input.Controls.Add(this.textBoxExternalEvent4);
            this.Interrupt_Input.Controls.Add(this.labelExternalEvent4);
            this.Interrupt_Input.Controls.Add(this.comboBoxType4);
            this.Interrupt_Input.Controls.Add(this.lblType3);
            this.Interrupt_Input.Controls.Add(this.comboBoxSelectInLogicBlock3);
            this.Interrupt_Input.Controls.Add(this.labelInterruptLoagicBlock3);
            this.Interrupt_Input.Controls.Add(this.textBoxExternalEvent3);
            this.Interrupt_Input.Controls.Add(this.labelExternalEvent3);
            this.Interrupt_Input.Controls.Add(this.comboBoxType3);
            this.Interrupt_Input.Controls.Add(this.comboBoxSelectInLogicBlock2);
            this.Interrupt_Input.Controls.Add(this.labelInterruptLoagicBlock2);
            this.Interrupt_Input.Controls.Add(this.textBoxExternalEvent2);
            this.Interrupt_Input.Controls.Add(this.lblSelectEvent2);
            this.Interrupt_Input.Controls.Add(this.lblType2);
            this.Interrupt_Input.Controls.Add(this.comboBoxType2);
            this.Interrupt_Input.Controls.Add(this.comboBoxSelectInLogicBlock1);
            this.Interrupt_Input.Controls.Add(this.labelInterruptLoagicBlock1);
            this.Interrupt_Input.Controls.Add(this.textBoxExternalEvent1);
            this.Interrupt_Input.Controls.Add(this.lblSelectEvent1);
            this.Interrupt_Input.Controls.Add(this.lblType1);
            this.Interrupt_Input.Controls.Add(this.comboBoxType1);
            this.Interrupt_Input.Location = new System.Drawing.Point(43, 39);
            this.Interrupt_Input.Name = "Interrupt_Input";
            this.Interrupt_Input.Size = new System.Drawing.Size(857, 317);
            this.Interrupt_Input.TabIndex = 0;
            this.Interrupt_Input.TabStop = false;
            // 
            // lblType4
            // 
            this.lblType4.AutoSize = true;
            this.lblType4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType4.Location = new System.Drawing.Point(16, 225);
            this.lblType4.Name = "lblType4";
            this.lblType4.Size = new System.Drawing.Size(39, 16);
            this.lblType4.TabIndex = 23;
            this.lblType4.Text = "Type";
            // 
            // comboBoxSelectInLogicBlock4
            // 
            this.comboBoxSelectInLogicBlock4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectInLogicBlock4.FormattingEnabled = true;
            this.comboBoxSelectInLogicBlock4.Location = new System.Drawing.Point(668, 245);
            this.comboBoxSelectInLogicBlock4.Name = "comboBoxSelectInLogicBlock4";
            this.comboBoxSelectInLogicBlock4.Size = new System.Drawing.Size(160, 21);
            this.comboBoxSelectInLogicBlock4.TabIndex = 22;
            this.comboBoxSelectInLogicBlock4.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectInLogicBlock4_SelectedIndexChanged);
            // 
            // labelInterruptLoagicBlock4
            // 
            this.labelInterruptLoagicBlock4.AutoSize = true;
            this.labelInterruptLoagicBlock4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterruptLoagicBlock4.Location = new System.Drawing.Point(493, 248);
            this.labelInterruptLoagicBlock4.Name = "labelInterruptLoagicBlock4";
            this.labelInterruptLoagicBlock4.Size = new System.Drawing.Size(168, 16);
            this.labelInterruptLoagicBlock4.TabIndex = 21;
            this.labelInterruptLoagicBlock4.Text = "Select Interrupt Logic Block";
            // 
            // textBoxExternalEvent4
            // 
            this.textBoxExternalEvent4.Location = new System.Drawing.Point(306, 245);
            this.textBoxExternalEvent4.Name = "textBoxExternalEvent4";
            this.textBoxExternalEvent4.ReadOnly = true;
            this.textBoxExternalEvent4.Size = new System.Drawing.Size(130, 20);
            this.textBoxExternalEvent4.TabIndex = 20;
            this.textBoxExternalEvent4.Text = " HI_DI6_INTP";
            // 
            // labelExternalEvent4
            // 
            this.labelExternalEvent4.AutoSize = true;
            this.labelExternalEvent4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExternalEvent4.Location = new System.Drawing.Point(205, 248);
            this.labelExternalEvent4.Name = "labelExternalEvent4";
            this.labelExternalEvent4.Size = new System.Drawing.Size(92, 16);
            this.labelExternalEvent4.TabIndex = 19;
            this.labelExternalEvent4.Text = "External Event";
            // 
            // comboBoxType4
            // 
            this.comboBoxType4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType4.FormattingEnabled = true;
            this.comboBoxType4.Items.AddRange(new object[] {
            "External"});
            this.comboBoxType4.Location = new System.Drawing.Point(19, 244);
            this.comboBoxType4.Name = "comboBoxType4";
            this.comboBoxType4.Size = new System.Drawing.Size(150, 21);
            this.comboBoxType4.TabIndex = 18;
            // 
            // lblType3
            // 
            this.lblType3.AutoSize = true;
            this.lblType3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType3.Location = new System.Drawing.Point(16, 153);
            this.lblType3.Name = "lblType3";
            this.lblType3.Size = new System.Drawing.Size(39, 16);
            this.lblType3.TabIndex = 17;
            this.lblType3.Text = "Type";
            // 
            // comboBoxSelectInLogicBlock3
            // 
            this.comboBoxSelectInLogicBlock3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectInLogicBlock3.FormattingEnabled = true;
            this.comboBoxSelectInLogicBlock3.Location = new System.Drawing.Point(668, 173);
            this.comboBoxSelectInLogicBlock3.Name = "comboBoxSelectInLogicBlock3";
            this.comboBoxSelectInLogicBlock3.Size = new System.Drawing.Size(160, 21);
            this.comboBoxSelectInLogicBlock3.TabIndex = 16;
            this.comboBoxSelectInLogicBlock3.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectInLogicBlock3_SelectedIndexChanged);
            // 
            // labelInterruptLoagicBlock3
            // 
            this.labelInterruptLoagicBlock3.AutoSize = true;
            this.labelInterruptLoagicBlock3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterruptLoagicBlock3.Location = new System.Drawing.Point(493, 176);
            this.labelInterruptLoagicBlock3.Name = "labelInterruptLoagicBlock3";
            this.labelInterruptLoagicBlock3.Size = new System.Drawing.Size(168, 16);
            this.labelInterruptLoagicBlock3.TabIndex = 15;
            this.labelInterruptLoagicBlock3.Text = "Select Interrupt Logic Block";
            // 
            // textBoxExternalEvent3
            // 
            this.textBoxExternalEvent3.Location = new System.Drawing.Point(306, 173);
            this.textBoxExternalEvent3.Name = "textBoxExternalEvent3";
            this.textBoxExternalEvent3.ReadOnly = true;
            this.textBoxExternalEvent3.Size = new System.Drawing.Size(130, 20);
            this.textBoxExternalEvent3.TabIndex = 14;
            this.textBoxExternalEvent3.Text = " HI_DI4_INTP";
            // 
            // labelExternalEvent3
            // 
            this.labelExternalEvent3.AutoSize = true;
            this.labelExternalEvent3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExternalEvent3.Location = new System.Drawing.Point(205, 176);
            this.labelExternalEvent3.Name = "labelExternalEvent3";
            this.labelExternalEvent3.Size = new System.Drawing.Size(92, 16);
            this.labelExternalEvent3.TabIndex = 13;
            this.labelExternalEvent3.Text = "External Event";
            // 
            // comboBoxType3
            // 
            this.comboBoxType3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType3.FormattingEnabled = true;
            this.comboBoxType3.Items.AddRange(new object[] {
            "External"});
            this.comboBoxType3.Location = new System.Drawing.Point(19, 172);
            this.comboBoxType3.Name = "comboBoxType3";
            this.comboBoxType3.Size = new System.Drawing.Size(150, 21);
            this.comboBoxType3.TabIndex = 12;
            // 
            // comboBoxSelectInLogicBlock2
            // 
            this.comboBoxSelectInLogicBlock2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectInLogicBlock2.FormattingEnabled = true;
            this.comboBoxSelectInLogicBlock2.Location = new System.Drawing.Point(668, 109);
            this.comboBoxSelectInLogicBlock2.Name = "comboBoxSelectInLogicBlock2";
            this.comboBoxSelectInLogicBlock2.Size = new System.Drawing.Size(160, 21);
            this.comboBoxSelectInLogicBlock2.TabIndex = 11;
            this.comboBoxSelectInLogicBlock2.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectInLogicBlock2_SelectedIndexChanged);
            // 
            // labelInterruptLoagicBlock2
            // 
            this.labelInterruptLoagicBlock2.AutoSize = true;
            this.labelInterruptLoagicBlock2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterruptLoagicBlock2.Location = new System.Drawing.Point(493, 112);
            this.labelInterruptLoagicBlock2.Name = "labelInterruptLoagicBlock2";
            this.labelInterruptLoagicBlock2.Size = new System.Drawing.Size(168, 16);
            this.labelInterruptLoagicBlock2.TabIndex = 10;
            this.labelInterruptLoagicBlock2.Text = "Select Interrupt Logic Block";
            // 
            // textBoxExternalEvent2
            // 
            this.textBoxExternalEvent2.Location = new System.Drawing.Point(306, 109);
            this.textBoxExternalEvent2.Name = "textBoxExternalEvent2";
            this.textBoxExternalEvent2.ReadOnly = true;
            this.textBoxExternalEvent2.Size = new System.Drawing.Size(130, 20);
            this.textBoxExternalEvent2.TabIndex = 9;
            this.textBoxExternalEvent2.Text = " HI_DI3_INTP";
            // 
            // lblSelectEvent2
            // 
            this.lblSelectEvent2.AutoSize = true;
            this.lblSelectEvent2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectEvent2.Location = new System.Drawing.Point(206, 112);
            this.lblSelectEvent2.Name = "lblSelectEvent2";
            this.lblSelectEvent2.Size = new System.Drawing.Size(92, 16);
            this.lblSelectEvent2.TabIndex = 8;
            this.lblSelectEvent2.Text = "External Event";
            // 
            // lblType2
            // 
            this.lblType2.AutoSize = true;
            this.lblType2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType2.Location = new System.Drawing.Point(17, 87);
            this.lblType2.Name = "lblType2";
            this.lblType2.Size = new System.Drawing.Size(39, 16);
            this.lblType2.TabIndex = 7;
            this.lblType2.Text = "Type";
            // 
            // comboBoxType2
            // 
            this.comboBoxType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType2.FormattingEnabled = true;
            this.comboBoxType2.Items.AddRange(new object[] {
            "External"});
            this.comboBoxType2.Location = new System.Drawing.Point(20, 108);
            this.comboBoxType2.Name = "comboBoxType2";
            this.comboBoxType2.Size = new System.Drawing.Size(150, 21);
            this.comboBoxType2.TabIndex = 6;
            // 
            // comboBoxSelectInLogicBlock1
            // 
            this.comboBoxSelectInLogicBlock1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectInLogicBlock1.FormattingEnabled = true;
            this.comboBoxSelectInLogicBlock1.Location = new System.Drawing.Point(668, 38);
            this.comboBoxSelectInLogicBlock1.Name = "comboBoxSelectInLogicBlock1";
            this.comboBoxSelectInLogicBlock1.Size = new System.Drawing.Size(160, 21);
            this.comboBoxSelectInLogicBlock1.TabIndex = 5;
            this.comboBoxSelectInLogicBlock1.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectInLogicBlock1_SelectedIndexChanged);
            // 
            // labelInterruptLoagicBlock1
            // 
            this.labelInterruptLoagicBlock1.AutoSize = true;
            this.labelInterruptLoagicBlock1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterruptLoagicBlock1.Location = new System.Drawing.Point(492, 41);
            this.labelInterruptLoagicBlock1.Name = "labelInterruptLoagicBlock1";
            this.labelInterruptLoagicBlock1.Size = new System.Drawing.Size(168, 16);
            this.labelInterruptLoagicBlock1.TabIndex = 4;
            this.labelInterruptLoagicBlock1.Text = "Select Interrupt Logic Block";
            // 
            // textBoxExternalEvent1
            // 
            this.textBoxExternalEvent1.Location = new System.Drawing.Point(305, 38);
            this.textBoxExternalEvent1.Name = "textBoxExternalEvent1";
            this.textBoxExternalEvent1.ReadOnly = true;
            this.textBoxExternalEvent1.Size = new System.Drawing.Size(130, 20);
            this.textBoxExternalEvent1.TabIndex = 3;
            this.textBoxExternalEvent1.Text = " HI_DI0_INTP";
            // 
            // lblSelectEvent1
            // 
            this.lblSelectEvent1.AutoSize = true;
            this.lblSelectEvent1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectEvent1.Location = new System.Drawing.Point(205, 41);
            this.lblSelectEvent1.Name = "lblSelectEvent1";
            this.lblSelectEvent1.Size = new System.Drawing.Size(92, 16);
            this.lblSelectEvent1.TabIndex = 2;
            this.lblSelectEvent1.Text = "External Event";
            // 
            // lblType1
            // 
            this.lblType1.AutoSize = true;
            this.lblType1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType1.Location = new System.Drawing.Point(16, 16);
            this.lblType1.Name = "lblType1";
            this.lblType1.Size = new System.Drawing.Size(39, 16);
            this.lblType1.TabIndex = 1;
            this.lblType1.Text = "Type";
            // 
            // comboBoxType1
            // 
            this.comboBoxType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType1.FormattingEnabled = true;
            this.comboBoxType1.Items.AddRange(new object[] {
            "External"});
            this.comboBoxType1.Location = new System.Drawing.Point(19, 37);
            this.comboBoxType1.Name = "comboBoxType1";
            this.comboBoxType1.Size = new System.Drawing.Size(150, 21);
            this.comboBoxType1.TabIndex = 0;
            // 
            // HSIOsOMtimer
            // 
            this.HSIOsOMtimer.Interval = 30;
            this.HSIOsOMtimer.Tick += new System.EventHandler(this.HSIOsOMtimer_Tick);
            // 
            // HSToolTipTimer
            // 
            this.HSToolTipTimer.Interval = 1500;
            this.HSToolTipTimer.Tick += new System.EventHandler(this.HSToolTipTimer_Tick);
            // 
            // frmHSIOConfigeration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 788);
            this.ControlBox = false;
            this.Controls.Add(this.Tabcntrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHSIOConfigeration";
            this.Text = "frmHSIOConfigeration";
            this.Tabcntrl.ResumeLayout(false);
            this.tabIntInput.ResumeLayout(false);
            this.Interrupt_Input.ResumeLayout(false);
            this.Interrupt_Input.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabcntrl;
        private System.Windows.Forms.TabPage Input;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.TabPage tabIntInput;
        private System.Windows.Forms.GroupBox Interrupt_Input;
        private System.Windows.Forms.ComboBox comboBoxSelectInLogicBlock2;
        private System.Windows.Forms.Label labelInterruptLoagicBlock2;
        private System.Windows.Forms.TextBox textBoxExternalEvent2;
        private System.Windows.Forms.Label lblSelectEvent2;
        private System.Windows.Forms.Label lblType2;
        private System.Windows.Forms.ComboBox comboBoxType2;
        private System.Windows.Forms.ComboBox comboBoxSelectInLogicBlock1;
        private System.Windows.Forms.Label labelInterruptLoagicBlock1;
        private System.Windows.Forms.TextBox textBoxExternalEvent1;
        private System.Windows.Forms.Label lblSelectEvent1;
        private System.Windows.Forms.Label lblType1;
        private System.Windows.Forms.ComboBox comboBoxType1;
        private System.Windows.Forms.Timer HSIOsOMtimer;
        private System.Windows.Forms.Label lblType4;
        private System.Windows.Forms.ComboBox comboBoxSelectInLogicBlock4;
        private System.Windows.Forms.Label labelInterruptLoagicBlock4;
        private System.Windows.Forms.TextBox textBoxExternalEvent4;
        private System.Windows.Forms.Label labelExternalEvent4;
        private System.Windows.Forms.ComboBox comboBoxType4;
        private System.Windows.Forms.Label lblType3;
        private System.Windows.Forms.ComboBox comboBoxSelectInLogicBlock3;
        private System.Windows.Forms.Label labelInterruptLoagicBlock3;
        private System.Windows.Forms.TextBox textBoxExternalEvent3;
        private System.Windows.Forms.Label labelExternalEvent3;
        private System.Windows.Forms.ComboBox comboBoxType3;
        private System.Windows.Forms.Timer HSToolTipTimer;
    }
}