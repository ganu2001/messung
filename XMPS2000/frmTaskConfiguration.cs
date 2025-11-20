using System;
using System.Windows.Forms;
using XMPS2000.Core;

namespace XMPS2000
{
    public partial class frmTaskConfiguration : Form, IXMForm
    {
        XMPS xm;
        ErrorProvider errorProvider;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lblTime;
        private CheckBox checkBox1;
        private Label lblUnit;
        private TextBox txtTime;
        private Label lblScanTime;
        private Label lblType;
        private Label lblScanUnit;
        private TextBox txtScanTime;
        private System.ComponentModel.IContainer components;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;

        public frmTaskConfiguration()
        {
            InitializeComponent();
            xm=XMPS.Instance;
            DataBind();
        }
        public void OnShown()
        {
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblScanUnit = new System.Windows.Forms.Label();
            this.txtScanTime = new System.Windows.Forms.TextBox();
            this.lblScanTime = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblScanUnit);
            this.groupBox1.Controls.Add(this.txtScanTime);
            this.groupBox1.Controls.Add(this.lblScanTime);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(625, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type";
            // 
            // lblScanUnit
            // 
            this.lblScanUnit.AutoSize = true;
            this.lblScanUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScanUnit.Location = new System.Drawing.Point(589, 34);
            this.lblScanUnit.Name = "lblScanUnit";
            this.lblScanUnit.Size = new System.Drawing.Size(22, 15);
            this.lblScanUnit.TabIndex = 3;
            this.lblScanUnit.Text = "ms";
            // 
            // txtScanTime
            // 
            this.txtScanTime.Location = new System.Drawing.Point(264, 34);
            this.txtScanTime.Name = "txtScanTime";
            this.txtScanTime.Size = new System.Drawing.Size(309, 20);
            this.txtScanTime.TabIndex = 2;
            this.txtScanTime.Text = "20";
            this.txtScanTime.TextChanged += new System.EventHandler(this.txtScanTime_TextChanged);
            this.txtScanTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScanTime_KeyPress);
            // 
            // lblScanTime
            // 
            this.lblScanTime.AutoSize = true;
            this.lblScanTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScanTime.Location = new System.Drawing.Point(85, 34);
            this.lblScanTime.Name = "lblScanTime";
            this.lblScanTime.Size = new System.Drawing.Size(165, 15);
            this.lblScanTime.TabIndex = 1;
            this.lblScanTime.Text = "Task cycle time(eg. 5 to 2000ms)";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblType.Location = new System.Drawing.Point(20, 34);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(37, 15);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Cyclic";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblUnit);
            this.groupBox2.Controls.Add(this.txtTime);
            this.groupBox2.Controls.Add(this.lblTime);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(6, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(625, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Task Watchdog";
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnit.Location = new System.Drawing.Point(559, 54);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(22, 15);
            this.lblUnit.TabIndex = 3;
            this.lblUnit.Text = "ms";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(147, 54);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(396, 20);
            this.txtTime.TabIndex = 2;
            this.txtTime.Text = "10";
            this.txtTime.TextChanged += new System.EventHandler(this.txtTime_TextChanged);
            this.txtTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTime.Location = new System.Drawing.Point(20, 54);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(102, 15);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "Time (2 to 2000 ms)";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(59, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Enable";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmTaskConfiguration
            // 
            this.ClientSize = new System.Drawing.Size(643, 247);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTaskConfiguration";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        public void DataBind()
        {
            // Update UI controls
            checkBox1.Checked = xm.LoadedProject._isEnable;
            txtScanTime.Text = xm.LoadedProject._scanTime.ToString();
            txtTime.Text = xm.LoadedProject._timeRange.ToString();

            bool isBacnet = xm.LoadedProject.ProjectName.StartsWith("XBLD", StringComparison.OrdinalIgnoreCase);

            if (xm.LoadedProject._scanTime == 0 || xm.LoadedProject._timeRange == 0)
            {
                if (isBacnet)
                {
                    xm.LoadedProject._scanTime = 150;
                    xm.LoadedProject._timeRange = 70;
                }
                else
                {
                    xm.LoadedProject._scanTime = 20;
                    xm.LoadedProject._timeRange = 10;
                }

                txtScanTime.Text = xm.LoadedProject._scanTime.ToString();
                txtTime.Text = xm.LoadedProject._timeRange.ToString();
            }

            else if (checkBox1.Checked && xm.LoadedProject._timeRange >= xm.LoadedProject._scanTime)
            {
                if (isBacnet)
                {
                    xm.LoadedProject._scanTime = 150;
                    xm.LoadedProject._timeRange = 70;
                }
                else
                {
                    xm.LoadedProject._scanTime = 20;
                    xm.LoadedProject._timeRange = 10;
                }

                txtScanTime.Text = xm.LoadedProject._scanTime.ToString();
                txtTime.Text = xm.LoadedProject._timeRange.ToString();
            }

            ValidateWatchdogVsCycleTime();
        }

        private void txtScanTime_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtScanTime.Text))
            {
                xm.LoadedProject._scanTime = 0;
                errorProvider.SetError(txtScanTime, "Task Cycle Time cannot be empty");
                return;
            }

            if (int.TryParse(txtScanTime.Text, out int value))
            {
                if (value >= 5 && value <= 2000)
                {
                    xm.LoadedProject._scanTime = value;
                    errorProvider.SetError(txtScanTime, ""); // Clear any previous error
                    ValidateWatchdogVsCycleTime();
                }
                else
                {
                    xm.LoadedProject._scanTime = value;
                    errorProvider.SetError(txtScanTime, "Task Cycle Time must be between 5-2000");
                }
            }
            else
            {
                errorProvider.SetError(txtScanTime, "Invalid input"); // Handle non-numeric input
            }
        }

        private void txtTime_TextChanged(object sender, EventArgs e)
       {
            if (string.IsNullOrWhiteSpace(txtTime.Text))
            {
                xm.LoadedProject._timeRange = 0;
                errorProvider.SetError(txtTime, "Task Watchdog Time cannot be empty");
                return;
            }

            if (int.TryParse(txtTime.Text, out int value))
            {
                if (value >= 2 && value <= 2000)
                {
                    xm.LoadedProject._timeRange = value;
                    errorProvider.SetError(txtTime, ""); // Clear any previous error
                    ValidateWatchdogVsCycleTime();
                }
                else
                {
                    xm.LoadedProject._timeRange = value;
                    errorProvider.SetError(txtTime, "Time must be between 2-2000");
                }
            }
            else
            {
                errorProvider.SetError(txtTime, "Invalid input"); // Handle non-numeric input
            }
        }

        private void ValidateWatchdogVsCycleTime()
        {
            if (!checkBox1.Checked)
            {
                errorProvider.SetError(txtTime, ""); 
                return;
            }

            if (int.TryParse(txtScanTime.Text, out int cycleTime) &&
                int.TryParse(txtTime.Text, out int watchdogTime))
            {
                if (watchdogTime >= cycleTime)
                {
                    errorProvider.SetError(txtTime, "Watchdog time must be less than Task cycle time");
                }
                else
                {
                    errorProvider.SetError(txtTime, "");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            xm.LoadedProject._isEnable = checkBox1.Checked;
            ValidateWatchdogVsCycleTime();
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
        }

        private void txtScanTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
        }
    }
}