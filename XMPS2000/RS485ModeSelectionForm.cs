using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Devices;

namespace XMPS2000
{
    public partial class RS485ModeSelectionForm : Form
    {
        public string SelectedMode { get; private set; } = "Master";
        public int SlaveID { get; private set; } = 0;

        private RadioButton rbMaster;
        private RadioButton rbSlave;
        private Label lblSlaveId;
        private TextBox txtSlaveId;
        private Button btnOk;
        private Button btnClose; 


        private XMPS xm;

        public RS485ModeSelectionForm()
        {
            InitializeComponent();
            xm = XMPS.Instance;
            InitializeCustomLayout();
            LoadProjectRS485Settings(); 
        }

        private void InitializeCustomLayout()
        {
            this.Text = "Select RS485 Mode";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(400, 160);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.LightBlue; 

            rbMaster = new RadioButton()
            {
                Text = "MODBUS RTU MASTER",
                Location = new Point(30, 20),
                AutoSize = true,
                ForeColor = Color.Black,
                BackColor = Color.Transparent 
            };

            rbSlave = new RadioButton()
            {
                Text = "MODBUS RTU SLAVE",
                Location = new Point(30, 50),
                AutoSize = true,
                ForeColor = Color.Black,
                BackColor = Color.Transparent 
            };

            lblSlaveId = new Label()
            {
                Text = "Slave ID",
                Location = new Point(180, 52),
                AutoSize = true,
                ForeColor = Color.Black,
                Visible = false,
                BackColor = Color.Transparent 
            };

            txtSlaveId = new TextBox()
            {
                Location = new Point(240, 50),
                Width = 60,
                Visible = false
            };

            btnOk = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(70, 90),
                Width = 80
            };
            btnClose = new Button()
            {
                Text = "Close",
                DialogResult = DialogResult.Cancel,
                Location = new Point(230, 90), 
                Width = 80
            };
            btnClose.Click += (s, e) => this.Close();

            rbMaster.CheckedChanged += RadioButtons_CheckedChanged;
            rbSlave.CheckedChanged += RadioButtons_CheckedChanged;
            btnOk.Click += BtnOk_Click;

            this.Controls.Add(rbMaster);
            this.Controls.Add(rbSlave);
            this.Controls.Add(lblSlaveId);
            this.Controls.Add(txtSlaveId);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnClose); 
        }

        private void LoadProjectRS485Settings()
        {
            if (xm.LoadedProject == null)
                return;
            if (xm.LoadedProject.PlcModel.Equals("XBLD-14E", StringComparison.OrdinalIgnoreCase) || xm.LoadedProject.PlcModel.Equals("XBLD-17E", StringComparison.OrdinalIgnoreCase) ||
                xm.LoadedProject.PlcModel.Equals("XBLD-14", StringComparison.OrdinalIgnoreCase) || xm.LoadedProject.PlcModel.Equals("XBLD-17", StringComparison.OrdinalIgnoreCase))
            {
                rbSlave.Enabled = false;
                rbMaster.Checked = true;
            }

            if (xm.LoadedProject.RS485Mode == "Slave" && rbSlave.Enabled)
            {
                rbSlave.Checked = true;
                txtSlaveId.Text = xm.LoadedProject.SlaveID > 0 ? xm.LoadedProject.SlaveID.ToString() : "";
                lblSlaveId.Visible = true;
                txtSlaveId.Visible = true;
            }
            else
            {
                rbMaster.Checked = true;
            }
        }

        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSlave.Checked)
            {
                lblSlaveId.Visible = true;
                txtSlaveId.Visible = true;
            }
            else
            {
                lblSlaveId.Visible = false;
                txtSlaveId.Visible = false;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (rbMaster.Checked)
            {
                var slaveExists = xm.LoadedProject.RS485Mode == "Slave";
                if (slaveExists)
                {
                    MessageBox.Show("Cannot switch to Master mode while Slave node already exists. Please delete the Slave node first.", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                SelectedMode = "Master";
                SaveToProject("Master", xm.LoadedProject.SlaveID);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (rbSlave.Checked)
            {
                bool masterExists =xm.LoadedProject.RS485Mode == "Master" || xm.LoadedProject.Devices.Any(d => d.GetType().Name == "MODBUSRTUMaster");
                if (masterExists)
                {
                    MessageBox.Show("Cannot switch to Slave mode while Master node already exists. Please delete the Master node first.",
                        "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    return;
                }
                // Restrict Slave mode for XBLD-14E and XBLD-17E models
                if (xm.LoadedProject.PlcModel.Equals("XBLD-14E", StringComparison.OrdinalIgnoreCase) ||
                    xm.LoadedProject.PlcModel.Equals("XBLD-17E", StringComparison.OrdinalIgnoreCase) ||
                    xm.LoadedProject.PlcModel.Equals("XBLD-14", StringComparison.OrdinalIgnoreCase) ||
                    xm.LoadedProject.PlcModel.Equals("XBLD-17", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Modbus RTU Slave is not supported for model " + xm.LoadedProject.PlcModel + ".",
                        "Model Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                    return;
                }
                if (!int.TryParse(txtSlaveId.Text.Trim(), out int id) || id < 1 || id > 247)
                {
                    MessageBox.Show("Please enter a valid Slave ID between 1 and 247.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSlaveId.Focus();
                    this.DialogResult = DialogResult.None;
                    return;
                }

                SelectedMode = "Slave";
                SlaveID = id;
                SaveToProject("Slave", id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void SaveToProject(string mode, int slaveId)
        {
            if (xm.LoadedProject != null)
            {
                xm.LoadedProject.RS485Mode = mode;
                xm.LoadedProject.SlaveID = slaveId;
                xm.MarkProjectModified(true); 
            }
        }
    }
}