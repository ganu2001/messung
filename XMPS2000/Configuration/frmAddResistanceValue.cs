using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.Configuration
{
    public partial class frmAddResistanceValue : Form
    {
        public XMPS xm = XMPS.Instance;
        private RESISTANCETable_Values dataSource;
        private List<RESISTANCETable_Values> list = new List<RESISTANCETable_Values>();
        private int _rowIndex = -1;
        private string tablename;
        public frmAddResistanceValue(string resistance = "", string output = "", int rowIndex = -1, string tablename="")
        {
            InitializeComponent();
            _rowIndex = rowIndex; 
            this.tablename=tablename;
            DataBind(resistance, output);
            xm.CurrentScreen = "frmAddResistanceValue";
            txtResistance.KeyPress += NumericOnly_KeyPress;
            txtOutput.KeyPress += NumericOnly_KeyPress;
        }
        private RESISTANCETable_Values _editingRecord = null;

        private void DataBind(string resistance, string output)
        {
            if (xm.LoadedProject.ResistanceValues == null)
                xm.LoadedProject.ResistanceValues = new List<RESISTANCETable_Values>();
            var tableRecords = xm.LoadedProject.ResistanceValues
            .Where(r => r.Name == tablename)
            .ToList();

            if (_rowIndex >= 0 && _rowIndex < tableRecords.Count)
            {
                var record = tableRecords[_rowIndex]; 
                dataSource = record;

                txtResistance.Text = record.Resistance.ToString();
                txtOutput.Text = record.output.ToString();
            }
            else
            {
                dataSource = null;
                txtResistance.Text = resistance;
                txtOutput.Text = output;
            }
            txtResistance.Enabled = true;
            txtOutput.Enabled = true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtResistance.Text))
            {
                MessageBox.Show("Please enter a resistance value.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtResistance.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOutput.Text))
            {
                MessageBox.Show("Please enter an output value.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOutput.Focus();
                return;
            }

            if (!double.TryParse(txtResistance.Text, out double resistanceValue) ||
                resistanceValue < -2147483648 || resistanceValue > 2147483647)
            {
                MessageBox.Show("Resistance must be a valid number between -2147483648 and 2147483647.",
                                "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtResistance.Focus();
                return;
            }

            if (!double.TryParse(txtOutput.Text, out double outputValue) ||
                outputValue < -2147483648 || outputValue > 2147483647)
            {
                MessageBox.Show("Output must be a valid number between -2147483648 and 2147483647.",
                                "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOutput.Focus();
                return;
            }
            double resistanceValue1 = double.Parse(txtResistance.Text);
            double outputValue1 = double.Parse(txtOutput.Text);

            if (dataSource != null)
            {
                // Update existing record
                dataSource.Resistance = resistanceValue1;
                dataSource.output = outputValue1;
                dataSource.Name = tablename;
            }
            else
            {
                string name = xm.SelectedNode.Info;
                // Creating new entry
                RESISTANCETable_Values obj = new RESISTANCETable_Values
                {
                    Resistance = resistanceValue,
                    output = outputValue,
                    Name = name
                };
                XMPS.Instance.LoadedProject.ResistanceValues.Add(obj);
                xm.LoadedProject.NewAddedTagIndex = XMPS.Instance.LoadedProject.ResistanceValues.Where(r => r.Name == name).Count() - 1;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            xm.CurrentScreen = "ResistanceValue#";
            xm.MarkProjectModified(true);
            var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
            curgridform.OnShown();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            xm.CurrentScreen = "ResistanceValue#";
            xm.MarkProjectModified(true);
            var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
            curgridform.OnShown();
        }
        private void frmAddResistanceValue_FormClosed(object sender, FormClosedEventArgs e)
        {
            xm.CurrentScreen = "ResistanceValue#";
            xm.MarkProjectModified(true);
            var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
            curgridform.OnShown();
        }
        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '-' && txt.SelectionStart > 0)
            {
                e.Handled = true;
            }
        }

    }
}