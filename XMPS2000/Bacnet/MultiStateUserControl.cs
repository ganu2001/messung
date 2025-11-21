using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;

namespace XMPS2000.Bacnet
{
    public partial class MultistateUserControl : UserControl
    {
        MultistateIOV currentMultistateIOV;
        int currentStatesCount;
        List<CheckBox> stateCheckBoxes = new List<CheckBox>();
        public event Action<List<int>> AlarmValuesChanged;
        public MultistateUserControl(int noOfState, string logicalAddress, ref MultistateIOV multistateIOV)
        {
            InitializeComponent();
            currentMultistateIOV = multistateIOV;
            currentStatesCount = noOfState;
            CreateAnLabelAndTextBoxes(noOfState);
        }

        private void CreateAnLabelAndTextBoxes(int noOfState)
        {
            int labelTop = 50;
            int textBoxTop = 50;
            int controlHeight = 30;
            int totalHeight = 50;

            Label statesHeaderLabel = new Label();
            statesHeaderLabel.Name = "lblStatesHeader";
            statesHeaderLabel.Text = "States";
            statesHeaderLabel.Top = 20;
            statesHeaderLabel.Left = 120;
            statesHeaderLabel.AutoSize = true;
            statesHeaderLabel.Font = new System.Drawing.Font(statesHeaderLabel.Font, System.Drawing.FontStyle.Regular);

            Label alarmStatesHeaderLabel = new Label();
            alarmStatesHeaderLabel.Name = "lblAlarmStatesHeader";
            alarmStatesHeaderLabel.Text = "Alarm States";
            alarmStatesHeaderLabel.Top = 20;
            alarmStatesHeaderLabel.Left = 280;
            alarmStatesHeaderLabel.AutoSize = true;
            alarmStatesHeaderLabel.Font = new System.Drawing.Font(alarmStatesHeaderLabel.Font, System.Drawing.FontStyle.Regular);

            this.Controls.Add(statesHeaderLabel);
            this.Controls.Add(alarmStatesHeaderLabel);

            for (int i = 1; i <= noOfState; i++)
            {
                Label stateLabel = new Label();
                stateLabel.Name = "lblState" + i;
                stateLabel.Text = "StateText " + i;
                stateLabel.Top = labelTop;
                stateLabel.Left = 10;
                stateLabel.AutoSize = true;

                TextBox stateTextBox = new TextBox();
                stateTextBox.Name = "txtState" + i;
                string currentBoxText = currentMultistateIOV.States.Count >= i ? currentMultistateIOV.States.FirstOrDefault(t=>t.StateNumber == i).StateValue : "state text " + i;
                stateTextBox.Text = currentBoxText;
                stateTextBox.Top = textBoxTop;
                stateTextBox.Left = 120;
                stateTextBox.Width = 150;
                // Set maximum length to 20 characters
                stateTextBox.MaxLength = 20;
                stateTextBox.KeyPress += StateTextBox_KeyPress;

                CheckBox stateCheckBox = new CheckBox();
                stateCheckBox.Name = "chkState" + i;
                stateCheckBox.Text = "";
                stateCheckBox.Top = textBoxTop;
                stateCheckBox.Left = 310;
                stateCheckBox.Width = 20;
                stateCheckBox.Enabled = true;
                stateCheckBox.CheckedChanged += StateCheckBox_CheckedChanged;

                if (currentMultistateIOV.AlarmValues != null && currentMultistateIOV.AlarmValues.Count > 0)
                {
                    stateCheckBox.Checked = currentMultistateIOV.AlarmValues.Contains((long)i);
                }
                else
                {
                    stateCheckBox.Checked = false;
                }

                this.Controls.Add(stateLabel);
                this.Controls.Add(stateTextBox);
                this.Controls.Add(stateCheckBox);
                stateCheckBoxes.Add(stateCheckBox);
                labelTop += controlHeight;
                textBoxTop += controlHeight;
                totalHeight += controlHeight;
            }
            Button saveButton = new Button();
            saveButton.Name = "btnSave";
            saveButton.Text = "Save";
            saveButton.Top = totalHeight + 20;
            saveButton.Left = 120;
            saveButton.Width = 80;
            saveButton.Click += SaveButton_Click;

            this.Controls.Add(saveButton);
            this.Height = totalHeight + controlHeight + 50;
            this.Width = 400;
        }
        private void StateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAlarmValues();
        }

        private void UpdateAlarmValues()
        {
            List<int> selectedStates = new List<int>();

            for (int i = 0; i < stateCheckBoxes.Count; i++)
            {
                if (stateCheckBoxes[i].Checked)
                {
                    selectedStates.Add(i + 1); 
                }
            }
            AlarmValuesChanged?.Invoke(selectedStates);
        }

        private void StateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            currentMultistateIOV.States.Clear();

            HashSet<string> enteredValues = new HashSet<string>();

            for (int i = 1; i <= currentStatesCount; i++)
            {
                TextBox textBox = this.Controls.Find("txtState" + i, true).FirstOrDefault() as TextBox;

                if (textBox == null || string.IsNullOrWhiteSpace(textBox.Text))
                {
                    MessageBox.Show($"Please add value for the state of StateText {i}", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string trimmedValue = textBox.Text.Trim();

                if (!enteredValues.Add(trimmedValue))
                {
                    MessageBox.Show($" All states must have unique values.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                State state = new State { StateNumber = i, StateValue = trimmedValue };
                currentMultistateIOV.States.Add(state);
            }

            List<long> alarmValues = new List<long>();
            for (int i = 0; i < stateCheckBoxes.Count; i++)
            {
                if (stateCheckBoxes[i].Checked)
                {
                    alarmValues.Add(i + 1);
                }
            }
            currentMultistateIOV.AlarmValues = alarmValues;

            if (alarmValues.Count > 0)
            {
                currentMultistateIOV.AlarmValue = alarmValues[0];
            }
            else
            {
                currentMultistateIOV.AlarmValue = 0;
            }
            UpdateAlarmValues();
            MessageBox.Show("State information updated successfully", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.ParentForm.DialogResult = DialogResult.OK;
        }
    }
}
