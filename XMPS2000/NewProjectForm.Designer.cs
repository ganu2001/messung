
namespace XMPS2000
{
    partial class NewProjectForm
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
            this.ddlModel = new System.Windows.Forms.ComboBox();
            this.lblPlcModel = new System.Windows.Forms.Label();
            this.btnpath = new System.Windows.Forms.Button();
            this.lblProjectNm = new System.Windows.Forms.Label();
            this.lblprjpath = new System.Windows.Forms.Label();
            this.TxtProjectName = new System.Windows.Forms.TextBox();
            this.TxtPath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblHSIOCount = new System.Windows.Forms.Label();
            this.lblPLCName = new System.Windows.Forms.Label();
            this.lblExpansionSlots = new System.Windows.Forms.Label();
            this.lblEthernet = new System.Windows.Forms.Label();
            this.lblComPorts = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOutputs = new System.Windows.Forms.Label();
            this.lblInputs = new System.Windows.Forms.Label();
            this.lblDigitalOutput = new System.Windows.Forms.Label();
            this.lblAnalogOutput = new System.Windows.Forms.Label();
            this.lblDigitalInput = new System.Windows.Forms.Label();
            this.lblAnalogInput = new System.Windows.Forms.Label();
            this.lblDegitalio = new System.Windows.Forms.Label();
            this.lblanalogio = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.epNewProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epNewProject)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.ddlModel);
            this.groupBox1.Controls.Add(this.lblPlcModel);
            this.groupBox1.Controls.Add(this.btnpath);
            this.groupBox1.Controls.Add(this.lblProjectNm);
            this.groupBox1.Controls.Add(this.lblprjpath);
            this.groupBox1.Controls.Add(this.TxtProjectName);
            this.groupBox1.Controls.Add(this.TxtPath);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 134);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // ddlModel
            // 
            this.ddlModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlModel.FormattingEnabled = true;
            this.ddlModel.Items.AddRange(new object[] {
            "Select Model"});
            this.ddlModel.Location = new System.Drawing.Point(150, 21);
            this.ddlModel.Name = "ddlModel";
            this.ddlModel.Size = new System.Drawing.Size(210, 21);
            this.ddlModel.Sorted = true;
            this.ddlModel.TabIndex = 28;
            this.ddlModel.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ddlModel_DrawItem);
            this.ddlModel.SelectedIndexChanged += new System.EventHandler(this.ddlModel_SelectedIndexChanged);
            this.ddlModel.Validating += new System.ComponentModel.CancelEventHandler(this.ddlModel_Validating);
            // 
            // lblPlcModel
            // 
            this.lblPlcModel.AutoSize = true;
            this.lblPlcModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblPlcModel.Location = new System.Drawing.Point(20, 24);
            this.lblPlcModel.Name = "lblPlcModel";
            this.lblPlcModel.Size = new System.Drawing.Size(92, 13);
            this.lblPlcModel.TabIndex = 27;
            this.lblPlcModel.Text = "Select Model";
            // 
            // btnpath
            // 
            this.btnpath.Location = new System.Drawing.Point(365, 57);
            this.btnpath.Name = "btnpath";
            this.btnpath.Size = new System.Drawing.Size(28, 20);
            this.btnpath.TabIndex = 26;
            this.btnpath.Text = "...";
            this.btnpath.UseVisualStyleBackColor = true;
            this.btnpath.Click += new System.EventHandler(this.btnpath_Click);
            // 
            // lblProjectNm
            // 
            this.lblProjectNm.AutoSize = true;
            this.lblProjectNm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblProjectNm.Location = new System.Drawing.Point(20, 95);
            this.lblProjectNm.Name = "lblProjectNm";
            this.lblProjectNm.Size = new System.Drawing.Size(99, 13);
            this.lblProjectNm.TabIndex = 25;
            this.lblProjectNm.Text = "Enter Project Name";
            // 
            // lblprjpath
            // 
            this.lblprjpath.AutoSize = true;
            this.lblprjpath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblprjpath.Location = new System.Drawing.Point(20, 60);
            this.lblprjpath.Name = "lblprjpath";
            this.lblprjpath.Size = new System.Drawing.Size(98, 13);
            this.lblprjpath.TabIndex = 24;
            this.lblprjpath.Text = "Select Project Path";
            // 
            // TxtProjectName
            // 
            this.TxtProjectName.Location = new System.Drawing.Point(150, 92);
            this.TxtProjectName.Name = "TxtProjectName";
            this.TxtProjectName.Size = new System.Drawing.Size(210, 20);
            this.TxtProjectName.TabIndex = 23;
            this.TxtProjectName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtProjectName_KeyPress);
            this.TxtProjectName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtProjectName_Validating);
            // 
            // TxtPath
            // 
            this.TxtPath.Enabled = false;
            this.TxtPath.Location = new System.Drawing.Point(150, 57);
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.Size = new System.Drawing.Size(210, 20);
            this.TxtPath.TabIndex = 22;
            this.TxtPath.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPath_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lblHSIOCount);
            this.groupBox2.Controls.Add(this.lblPLCName);
            this.groupBox2.Controls.Add(this.lblExpansionSlots);
            this.groupBox2.Controls.Add(this.lblEthernet);
            this.groupBox2.Controls.Add(this.lblComPorts);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblOutputs);
            this.groupBox2.Controls.Add(this.lblInputs);
            this.groupBox2.Controls.Add(this.lblDigitalOutput);
            this.groupBox2.Controls.Add(this.lblAnalogOutput);
            this.groupBox2.Controls.Add(this.lblDigitalInput);
            this.groupBox2.Controls.Add(this.lblAnalogInput);
            this.groupBox2.Controls.Add(this.lblDegitalio);
            this.groupBox2.Controls.Add(this.lblanalogio);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.groupBox2.Location = new System.Drawing.Point(12, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 181);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Description";
            // 
            // lblHSIOCount
            // 
            this.lblHSIOCount.AutoSize = true;
            this.lblHSIOCount.Location = new System.Drawing.Point(315, 45);
            this.lblHSIOCount.Name = "lblHSIOCount";
            this.lblHSIOCount.Size = new System.Drawing.Size(38, 13);
            this.lblHSIOCount.TabIndex = 45;
            this.lblHSIOCount.Text = "HSIOs";
            // 
            // lblPLCName
            // 
            this.lblPLCName.AutoSize = true;
            this.lblPLCName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPLCName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblPLCName.Location = new System.Drawing.Point(176, 16);
            this.lblPLCName.Name = "lblPLCName";
            this.lblPLCName.Size = new System.Drawing.Size(113, 20);
            this.lblPLCName.TabIndex = 44;
            this.lblPLCName.Text = "Not Selected";
            // 
            // lblExpansionSlots
            // 
            this.lblExpansionSlots.AutoSize = true;
            this.lblExpansionSlots.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblExpansionSlots.Location = new System.Drawing.Point(147, 142);
            this.lblExpansionSlots.Name = "lblExpansionSlots";
            this.lblExpansionSlots.Size = new System.Drawing.Size(21, 13);
            this.lblExpansionSlots.TabIndex = 43;
            this.lblExpansionSlots.Text = "No";
            // 
            // lblEthernet
            // 
            this.lblEthernet.AutoSize = true;
            this.lblEthernet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblEthernet.Location = new System.Drawing.Point(147, 118);
            this.lblEthernet.Name = "lblEthernet";
            this.lblEthernet.Size = new System.Drawing.Size(21, 13);
            this.lblEthernet.TabIndex = 42;
            this.lblEthernet.Text = "No";
            // 
            // lblComPorts
            // 
            this.lblComPorts.AutoSize = true;
            this.lblComPorts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblComPorts.Location = new System.Drawing.Point(147, 94);
            this.lblComPorts.Name = "lblComPorts";
            this.lblComPorts.Size = new System.Drawing.Size(21, 13);
            this.lblComPorts.TabIndex = 41;
            this.lblComPorts.Text = "No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(38, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Expansion Slots :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(73, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Ethernet :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(14, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Communication Ports :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(233, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Outputs";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(147, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Inputs";
            // 
            // lblOutputs
            // 
            this.lblOutputs.AutoSize = true;
            this.lblOutputs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblOutputs.Location = new System.Drawing.Point(233, 46);
            this.lblOutputs.Name = "lblOutputs";
            this.lblOutputs.Size = new System.Drawing.Size(44, 13);
            this.lblOutputs.TabIndex = 35;
            this.lblOutputs.Text = "Outputs";
            // 
            // lblInputs
            // 
            this.lblInputs.AutoSize = true;
            this.lblInputs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblInputs.Location = new System.Drawing.Point(147, 46);
            this.lblInputs.Name = "lblInputs";
            this.lblInputs.Size = new System.Drawing.Size(36, 13);
            this.lblInputs.TabIndex = 34;
            this.lblInputs.Text = "Inputs";
            // 
            // lblDigitalOutput
            // 
            this.lblDigitalOutput.AutoSize = true;
            this.lblDigitalOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblDigitalOutput.Location = new System.Drawing.Point(292, 46);
            this.lblDigitalOutput.Name = "lblDigitalOutput";
            this.lblDigitalOutput.Size = new System.Drawing.Size(13, 13);
            this.lblDigitalOutput.TabIndex = 33;
            this.lblDigitalOutput.Text = "0";
            // 
            // lblAnalogOutput
            // 
            this.lblAnalogOutput.AutoSize = true;
            this.lblAnalogOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblAnalogOutput.Location = new System.Drawing.Point(292, 68);
            this.lblAnalogOutput.Name = "lblAnalogOutput";
            this.lblAnalogOutput.Size = new System.Drawing.Size(13, 13);
            this.lblAnalogOutput.TabIndex = 32;
            this.lblAnalogOutput.Text = "0";
            // 
            // lblDigitalInput
            // 
            this.lblDigitalInput.AutoSize = true;
            this.lblDigitalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblDigitalInput.Location = new System.Drawing.Point(196, 46);
            this.lblDigitalInput.Name = "lblDigitalInput";
            this.lblDigitalInput.Size = new System.Drawing.Size(13, 13);
            this.lblDigitalInput.TabIndex = 31;
            this.lblDigitalInput.Text = "0";
            // 
            // lblAnalogInput
            // 
            this.lblAnalogInput.AutoSize = true;
            this.lblAnalogInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblAnalogInput.Location = new System.Drawing.Point(196, 68);
            this.lblAnalogInput.Name = "lblAnalogInput";
            this.lblAnalogInput.Size = new System.Drawing.Size(13, 13);
            this.lblAnalogInput.TabIndex = 30;
            this.lblAnalogInput.Text = "0";
            // 
            // lblDegitalio
            // 
            this.lblDegitalio.AutoSize = true;
            this.lblDegitalio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblDegitalio.Location = new System.Drawing.Point(65, 46);
            this.lblDegitalio.Name = "lblDegitalio";
            this.lblDegitalio.Size = new System.Drawing.Size(61, 13);
            this.lblDegitalio.TabIndex = 29;
            this.lblDegitalio.Text = "Digital I/O :";
            // 
            // lblanalogio
            // 
            this.lblanalogio.AutoSize = true;
            this.lblanalogio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(64)))));
            this.lblanalogio.Location = new System.Drawing.Point(61, 70);
            this.lblanalogio.Name = "lblanalogio";
            this.lblanalogio.Size = new System.Drawing.Size(65, 13);
            this.lblanalogio.TabIndex = 28;
            this.lblanalogio.Text = "Analog I/O :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.Location = new System.Drawing.Point(447, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(294, 313);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // btnclose
            // 
            this.btnclose.CausesValidation = false;
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.Location = new System.Drawing.Point(663, 355);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 23);
            this.btnclose.TabIndex = 24;
            this.btnclose.Text = "Cancel";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(567, 355);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 23;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // epNewProject
            // 
            this.epNewProject.ContainerControl = this;
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(749, 390);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Project Details";
            this.Load += new System.EventHandler(this.NewProjectForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epNewProject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPlcModel;
        private System.Windows.Forms.Button btnpath;
        private System.Windows.Forms.Label lblProjectNm;
        private System.Windows.Forms.Label lblprjpath;
        private System.Windows.Forms.TextBox TxtProjectName;
        private System.Windows.Forms.TextBox TxtPath;
        private System.Windows.Forms.ComboBox ddlModel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblOutputs;
        private System.Windows.Forms.Label lblInputs;
        private System.Windows.Forms.Label lblDigitalOutput;
        private System.Windows.Forms.Label lblAnalogOutput;
        private System.Windows.Forms.Label lblDigitalInput;
        private System.Windows.Forms.Label lblAnalogInput;
        private System.Windows.Forms.Label lblDegitalio;
        private System.Windows.Forms.Label lblanalogio;
        private System.Windows.Forms.Label lblPLCName;
        private System.Windows.Forms.Label lblExpansionSlots;
        private System.Windows.Forms.Label lblEthernet;
        private System.Windows.Forms.Label lblComPorts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ErrorProvider epNewProject;
        private System.Windows.Forms.Label lblHSIOCount;
    }
}