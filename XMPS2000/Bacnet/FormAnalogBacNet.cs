using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using System.Globalization;

namespace XMPS2000.Bacnet
{
    public partial class FormAnalogBacNet : Form, IXMForm
    {
        public string tagname;
        public string logicaladdress;
        private Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
        private ListBox listBoxSub;
        private string ioTypeName;
        private readonly bool _isReadOnly;
        private bool requiresSubUnitSelection = false;
        private string selectedMainUnit = string.Empty;
        private ToolTip unitToolTip = new ToolTip();
        private Timer tooltipTimer = new Timer();
        private List<string> recentSubUnits = new List<string>();
        private int lastHoveredIndex = -1;
        public FormAnalogBacNet(XMIOConfig ioconfig, bool isReadOnly = false)
        {
            InitializeComponent();
            tagname = ioconfig.Tag.ToString();
            logicaladdress = ioconfig.LogicalAddress;
            ioTypeName = ioconfig.Type.ToString().Equals("UniversalOutput") ? "AnalogOutput"
                        : ioconfig.Type.ToString().Equals("UniversalInput") ? "AnalogInput"
                        : ioconfig.Type.ToString().Equals("DataType") ? "AnalogValue" : ioconfig.Type.ToString();

            // UI adjustments based on type
            if (ioconfig.Type.ToString().EndsWith("Input") || ioconfig.Type.ToString().EndsWith("DataType"))
            {
                lblInitialValue.Visible = !ioconfig.Type.ToString().EndsWith("Input");
                textInitialValue.Visible = !ioconfig.Type.ToString().EndsWith("Input");
            }
            else
            {
                lblInitialValue.Visible = true;
                textInitialValue.Visible = true;
                textReDefault.Visible = true;
                label11.Visible = true;
                label4.Top = label11.Top + 120;
                textBoxCovIncr.Top = textReDefault.Top + 120;
            }
            bool isAnalogInput = ioconfig.Type.ToString().Equals("AnalogInput", StringComparison.OrdinalIgnoreCase)
                                || ioconfig.Type.ToString().Equals("UniversalInput", StringComparison.OrdinalIgnoreCase); checkBoxOutOfRange.Visible = isAnalogInput;
            labelMinValue.Visible = isAnalogInput;
            lblOutOfRangeManagement.Visible = isAnalogInput;
            textBoxMinValue.Visible = isAnalogInput;
            labelMaxValue.Visible = isAnalogInput;
            textBoxMaxValue.Visible = isAnalogInput;
            textObjectName.Text = tagname;
            comboBoxUnits.Items.Clear();
            BindUnitsData();
            // Load recent selections
            AnalogIOV analogIOV = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues
            .FirstOrDefault(b => b.LogicalAddress == logicaladdress);
            recentSelections = (analogIOV?.RecentUnits ?? new List<string>()).Where(u => data.Values.Any(subList => subList.Contains(u))).Take(5).ToList();

            recentSubUnits = (analogIOV?.RecentUnits ?? new List<string>()).Where(u => data.Values.Any(subList => subList.Contains(u))).Take(5).ToList();

            //recentSelections = (analogIOV?.RecentUnits ?? new List<string>()).ToList();
            // Force dropdown refresh to ensure recent selections are displayed
            //comboBoxUnits.DroppedDown = true;
            comboBoxUnits.DroppedDown = false;
            // Configure ComboBox
            comboBoxUnits.DrawMode = DrawMode.OwnerDrawFixed;
            comboBoxUnits.DrawItem += comboBoxUnits_DrawItem;
            tooltipTimer.Interval = 200;
            tooltipTimer.Tick += TooltipTimer_Tick;
            // Populate comboBoxUnits
            RebuildComboBoxUnits(analogIOV?.Units);
            AssignValues();
            AdjustCovAndInitialValuePosition();
            XMPS.Instance.LoadedProject.isChanged = false;
            XMPS.Instance.BacNetCurrentScreen = "FormAnalogBacNet";
            _isReadOnly = isReadOnly;
            comboBoxUnits.Validating += comboBoxUnits_Validating;
            textBoxMinPresVal.TextChanged += textBoxMinPresVal_TextChanged;
            textBoxMaxPresVal.TextChanged += textBoxMaxPresVal_TextChanged;
            ApplyReadOnly();
        }
        private void ApplyReadOnly()
        {
            if (_isReadOnly)
            {
                textObjectName.Enabled = false;
                textDescription.Enabled = false;
                comboBoxUnits.Enabled = false;
                textBoxMinPresVal.Enabled = false;
                textBoxMaxPresVal.Enabled = false;
                textBoxCovIncr.Enabled = false;
                textInitialValue.Enabled = false;
                checkEnable.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                textReDefault.Enabled = false;
            }
        }
        private void RebuildComboBoxUnits(string selectedUnit = null)
        {
            comboBoxUnits.SelectedIndexChanged -= comboBoxUnits_SelectedIndexChanged;
            comboBoxUnits.Items.Clear();

            if (recentSubUnits.Any())
            {
                comboBoxUnits.Items.Add("---- Recent ----");

                foreach (var subUnit in recentSubUnits)
                {
                    comboBoxUnits.Items.Add(subUnit);
                }
                comboBoxUnits.Items.Add("-----------------------------------------------------");
            }

            foreach (var mainUnit in data.Keys)
            {
                comboBoxUnits.Items.Add(mainUnit);
            }

            if (!string.IsNullOrEmpty(selectedUnit))
            {
                comboBoxUnits.Text = selectedUnit;
            }

            comboBoxUnits.SelectedIndexChanged += comboBoxUnits_SelectedIndexChanged;
        }
        private void ChangeRemainingControlsPos()
        {
            //label ObjectName.
            label7.Top = label7.Top - 30;
            textObjectName.Top = textObjectName.Top - 30;

            //label Description.
            label9.Top = label9.Top - 30;
            textDescription.Top = textDescription.Top - 30;

            //label Relinquish Default
            label11.Top = label11.Top - 30;
            textReDefault.Top = textReDefault.Top - 30;

            //label Units.
            label3.Top = label3.Top - 15;
            comboBoxUnits.Top = comboBoxUnits.Top - 15;

            //label MinPresVal.
            label18.Top = label18.Top - 10;
            textBoxMinPresVal.Top = textBoxMinPresVal.Top - 10;

            //label MaxPresVal.
            label12.Top = label12.Top - 3;
            textBoxMaxPresVal.Top = textBoxMaxPresVal.Top - 3;

            //lable TextBoxCov Increment.
            label4.Top = label4.Top - 30;
            textBoxCovIncr.Top = textBoxCovIncr.Top - 30;

            //lable InitialValue.
            lblInitialValue.Top = lblInitialValue.Top - 30;
            textInitialValue.Top = textInitialValue.Top - 30;
        }

        private void CreateAndPopulateListBox(string mainNode, int? index = null, Point? dropdownTopLeft = null)
        {
            if (listBoxSub != null)
            {
                this.Controls.Remove(listBoxSub);
                listBoxSub.Dispose();
                listBoxSub = null;
            }
            Point itemLocation;
            int listBoxHeight = 100;
            Size listBoxSize = new Size(200, listBoxHeight);
            if (index.HasValue && dropdownTopLeft.HasValue)
            {
                int itemHeight = comboBoxUnits.ItemHeight;
                int itemYScreen = dropdownTopLeft.Value.Y + (index.Value * itemHeight) + comboBoxUnits.Height;
                Rectangle screenBounds = Screen.FromControl(this).WorkingArea;
                if (data.ContainsKey(mainNode))
                {
                    int actualHeight = Math.Min(data[mainNode].Count * 15 + 4, 100);
                    listBoxHeight = actualHeight;
                    listBoxSize.Height = actualHeight;
                }
                int spaceBelow = screenBounds.Bottom - itemYScreen;
                if (spaceBelow < listBoxHeight)
                {
                    itemYScreen = itemYScreen - listBoxHeight - itemHeight;
                }
                itemLocation = this.PointToClient(new Point(comboBoxUnits.PointToScreen(Point.Empty).X + comboBoxUnits.Width + 5, itemYScreen));
            }
            else
            {
                itemLocation = new Point(comboBoxUnits.Right + 20, comboBoxUnits.Top);
            }
            listBoxSub = new ListBox
            {
                Location = itemLocation,
                Size = listBoxSize,
                Enabled = true
            };

            if (data.ContainsKey(mainNode))
            {
                foreach (var subNode in data[mainNode])
                    listBoxSub.Items.Add(subNode);
            }
            listBoxSub.MouseUp += ListBoxSub_MouseUp;
            listBoxSub.Click += ListBoxSub_Click;
            this.Controls.Add(listBoxSub);
            listBoxSub.BringToFront();
            listBoxSub.Show();
        }
        private void ListBoxSub_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int index = listBoxSub.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches && index >= 0)
                {
                    string selectedSubNode = listBoxSub.Items[index].ToString();
                    requiresSubUnitSelection = false;
                    selectedMainUnit = string.Empty;
                    comboBoxUnits.Text = selectedSubNode;
                    this.Controls.Remove(listBoxSub);
                    listBoxSub.Dispose();
                    listBoxSub = null;
                    errorProvider.SetError(comboBoxUnits, "");
                    comboBoxUnits.DroppedDown = false;
                }
            }
        }
        private void ListBoxSub_Click(object sender, EventArgs e)
        {
            if (listBoxSub.SelectedItem != null)
            {
                string selectedSubNode = listBoxSub.SelectedItem.ToString();
                requiresSubUnitSelection = false;
                selectedMainUnit = string.Empty;
                comboBoxUnits.Text = selectedSubNode;
                this.Controls.Remove(listBoxSub);
                listBoxSub.Dispose();
                listBoxSub = null;
                errorProvider.SetError(comboBoxUnits, "");
                comboBoxUnits.DroppedDown = false;
            }
        }       
        private void ListBoxSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSub.SelectedItem != null)
            {
                comboBoxUnits.DroppedDown = false;
                string selectedSubNode = listBoxSub.SelectedItem.ToString();
                requiresSubUnitSelection = false;
                selectedMainUnit = string.Empty;
                // Rebuild comboBoxUnits with the sub-item as the selected text
                RebuildComboBoxUnits(selectedSubNode);
                if (comboBoxUnits.DroppedDown)
                    comboBoxUnits.DroppedDown = false;
                // Remove list box
                this.Controls.Remove(listBoxSub);
                listBoxSub.Dispose();
                listBoxSub = null;
                errorProvider.SetError(comboBoxUnits, "");
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (listBoxSub != null &&
                !comboBoxUnits.Bounds.Contains(e.Location) &&
                !listBoxSub.Bounds.Contains(e.Location))
            {
                this.Controls.Remove(listBoxSub);
                listBoxSub.Dispose();
                listBoxSub = null;
            }
        }   
        private void comboBoxUnits_DropDown(object sender, EventArgs e)
        {
            tooltipTimer.Start();
        }

        private void comboBoxUnits_DropDownClosed(object sender, EventArgs e)
        {
            tooltipTimer.Stop();
            unitToolTip.Hide(comboBoxUnits);
            lastHoveredIndex = -1;
        }

        private void TooltipTimer_Tick(object sender, EventArgs e)
        {
            if (!comboBoxUnits.DroppedDown)
            {
                if (listBoxSub != null)
                {
                    this.Controls.Remove(listBoxSub);
                    listBoxSub.Dispose();
                    listBoxSub = null;
                }
                return;
            }

            Point mousePos = Cursor.Position;

            if (listBoxSub != null)
            {
                var subListScreenBounds = new Rectangle(listBoxSub.PointToScreen(Point.Empty), listBoxSub.Size);
                if (subListScreenBounds.Contains(mousePos))
                {
                    unitToolTip.Hide(comboBoxUnits);
                    return;
                }
            }
            int itemHeight = comboBoxUnits.ItemHeight;
            int itemCount = comboBoxUnits.Items.Count;
            var dropdownTopLeft = comboBoxUnits.PointToScreen(Point.Empty);
            var dropdownBounds = new Rectangle(
                dropdownTopLeft.X,
                dropdownTopLeft.Y + comboBoxUnits.Height,
                comboBoxUnits.Width,
                itemHeight * itemCount);

            if (!dropdownBounds.Contains(mousePos))
            {
                unitToolTip.Hide(comboBoxUnits);
                lastHoveredIndex = -1;
                if (listBoxSub != null)
                {
                    this.Controls.Remove(listBoxSub);
                    listBoxSub.Dispose();
                    listBoxSub = null;
                }
                return;
            }

            int index = (mousePos.Y - dropdownBounds.Y) / itemHeight;
            if (index < 0 || index >= itemCount)
            {
                if (listBoxSub != null)
                {
                    this.Controls.Remove(listBoxSub);
                    listBoxSub.Dispose();
                    listBoxSub = null;
                }
                lastHoveredIndex = -1;
                return;
            }
            if (index == lastHoveredIndex) return;

            string mainUnit = comboBoxUnits.Items[index].ToString();

            if (mainUnit.StartsWith("--") || recentSubUnits.Contains(mainUnit))
            {
                unitToolTip.Hide(comboBoxUnits);
                if (listBoxSub != null)
                {
                    this.Controls.Remove(listBoxSub);
                    listBoxSub.Dispose();
                    listBoxSub = null;
                }
                lastHoveredIndex = -1;
                return;
            }
            lastHoveredIndex = index;

            if (data.ContainsKey(mainUnit) && data[mainUnit].Count > 0)
            {
                CreateAndPopulateListBox(mainUnit, index, dropdownTopLeft);
                if (listBoxSub != null && listBoxSub.Items.Count > 0)
                {
                    listBoxSub.SelectedIndex = 0;
                }
            }
            else
            {
                if (listBoxSub != null)
                {
                    this.Controls.Remove(listBoxSub);
                    listBoxSub.Dispose();
                    listBoxSub = null;
                }
            }
        }
        private void AssignValues()
        {
            AnalogIOV analogIOV = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues
           .FirstOrDefault(b => b.LogicalAddress == logicaladdress);
            labelObjIdentifier.Text = analogIOV.ObjectIdentifier.ToString();
            labelInstanceno.Text = analogIOV.InstanceNumber.ToString();
            labelObjType.Text = analogIOV.ObjectType;
            var mode = XMPS.Instance.LoadedProject.Tags
            .FirstOrDefault(t => t.LogicalAddress.Equals(logicaladdress))?.Mode;
            labelDeviceType.Text = string.IsNullOrEmpty(mode) ? "Unconfigured" : mode;
            textObjectName.Text = analogIOV.ObjectName;
            textDescription.Text = analogIOV.Description;
            logicaladdress = analogIOV.LogicalAddress;
            // Update comboBoxUnits with current unit
            RebuildComboBoxUnits(analogIOV.Units);
            // Assign other values
            AssignDefaultValues(ref analogIOV);
            DisplayInitialValue(ref analogIOV);
            ConfigureRelinquishDefaultControl();
            textBoxMinPresVal.Text = analogIOV.MinPresValue.ToString();
            checkBoxOutOfRange.Checked = analogIOV.OutOfRangeEnabled;
            checkBoxOutOfRange.Enabled = true;
            textBoxMinPresVal.Text = analogIOV.MinPresValue.ToString();
            textBoxMaxPresVal.Text = analogIOV.MaxPresValue.ToString();
            if (analogIOV.MinValue == 0 && analogIOV.MaxValue == 0)
            {
                textBoxMinValue.Text = analogIOV.MinPresValue.ToString();
                textBoxMaxValue.Text = analogIOV.MaxPresValue.ToString();
            }
            else
            {
                textBoxMinValue.Text = analogIOV.MinValue.ToString();
                textBoxMaxValue.Text = analogIOV.MaxValue.ToString();
            }
            textBoxMinValue.Enabled = checkBoxOutOfRange.Checked;
            textBoxMaxValue.Enabled = checkBoxOutOfRange.Checked;
            textReDefault.Text = analogIOV.RelinquishDefault == "0" ? "" : analogIOV.RelinquishDefault;
            textTimeDelay.Text = analogIOV.TimeDelay.ToString();
            textTimeDelayNormal.Text = analogIOV.TimeDelayNormal.ToString();
            textBoxHighLimit.Text = analogIOV.HighLimit.ToString();
            textBoxLowLimit.Text = analogIOV.LowLimit.ToString();
            textDeadband.Text = analogIOV.Deadband.ToString();
            checkEnable.Checked = analogIOV.EventDetectionEnable == 1;
            comboNotifyclass.DataSource = XMPS.Instance.LoadedProject.BacNetIP.Notifications.ToList();
            comboNotifyclass.DisplayMember = "ObjectName";
            comboNotifyclass.ValueMember = "InstanceNumber";
            comboNotifyclass.SelectedValue = analogIOV.NotificationClass.ToString();
            CheckLimitEnableCheckBoxes(analogIOV.LimitEnable);
            CheckEventEnableCheckBoxes(analogIOV.EventEnable);
            CheckNotificationEnableCheckBoxes(analogIOV.NotifyType);
            var binaryOnlyList = XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(x => x.ObjectType.Contains("Binary Value")).ToList();
            binaryOnlyList.Insert(0, new BinaryIOV { ObjectName = "Select Inhibit Binary Value", InstanceNumber = "" });
            this.cmb_BinaryValue.DataSource = binaryOnlyList;
            this.cmb_BinaryValue.DisplayMember = "ObjectName";
            this.cmb_BinaryValue.ValueMember = "InstanceNumber";
            this.cmb_BinaryValue.SelectedIndex = analogIOV.BinaryAValue > 0 ? analogIOV.BinaryAValue : 0;
            comboBoxUnits.Text = string.IsNullOrEmpty(analogIOV.Units) ? "95: no-units" : analogIOV.Units;
        }
        private void ConfigureRelinquishDefaultControl()
        {
            bool isAnalogValue = labelObjType.Text.Contains("Analog Value");
            bool isAnalogOutput = labelObjType.Text.Contains("Analog Output");
            bool isAnalogInput = labelObjType.Text.Contains("Analog Input");

            textReDefault.Visible = false;
            textReDefault.Visible = false;
            textReDefault.Visible = false;
            labelDeviceType.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            if (isAnalogValue)
            {
                label11.Visible = true;
                textReDefault.Visible = true;
                textReDefault.Visible = true;
                textReDefault.Location = textReDefault.Location;
                textReDefault.Width = textReDefault.Width;
                ChangeRemainingControlsPos();
            }
            else if (isAnalogOutput)
            {
                labelDeviceType.Visible = true;
                label10.Visible = true;
                textReDefault.Visible = true;
                label11.Visible = true;
            }
            else if (isAnalogInput)
            {
                labelDeviceType.Visible = true;
                label10.Visible = true;
                label11.Visible = false;
                textReDefault.Visible = false;
                label3.Top = label11.Top + 0;
                comboBoxUnits.Top = comboBoxUnits.Top - 25;
                label18.Top = label18.Top - 18;
                textBoxMinPresVal.Top = textBoxMinPresVal.Top - 18;
                label12.Top = label12.Top - 18;
                textBoxMaxPresVal.Top = textBoxMaxPresVal.Top - 18;
                label4.Top = label4.Top - 18;
                textBoxCovIncr.Top = textBoxCovIncr.Top - 18;
            }
        }
        private void DisplayInitialValue(ref AnalogIOV analogIOV)
        {
            string initialvalue = XMPS.Instance.LoadedProject.Tags
                  .FirstOrDefault(t => t.LogicalAddress.Equals(logicaladdress))?.InitialValue;

            textInitialValue.Text = initialvalue;
        }
        private void AssignDefaultValues(ref AnalogIOV analogIOV)
        {
            textBoxMaxPresVal.Text = analogIOV.MaxPresValue.ToString();
            textBoxCovIncr.Text = analogIOV.COVIncrement?.ToString() == null ? "0" : analogIOV.COVIncrement.ToString();
        }
        private void CheckNotificationEnableCheckBoxes(int notifyType)
        {
            checkAlarm.Checked = notifyType == 0;
            checkEvent.Checked = notifyType == 1;
        }
        private void CheckEventEnableCheckBoxes(int eventEnable)
        {
            string checkboxesVal = Convert.ToString(eventEnable, 2).PadLeft(3, '0');
            checktooffNormal.Checked = checkboxesVal[0] == '1';
            checkToFault.Checked = checkboxesVal[1] == '1';
            checkToNormal.Checked = checkboxesVal[2] == '1';
        }
        private void CheckLimitEnableCheckBoxes(int limitEnable)
        {
            string checkboxesVal = Convert.ToString(limitEnable, 2).PadLeft(2, '0');
            checkHiLimit.Checked = checkboxesVal[0] == '1';
            checkLoLimit.Checked = checkboxesVal[1] == '1';
        }
        public void OnShown()
        {
            textObjectName.Text = tagname;
        }
        private void checkEnable_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Controls.Cast<Control>().Where(c => !(c is Label)).ToList().ForEach(c => c.Enabled = checkEnable.Checked);
            if (checkEnable.Checked)
                EventEnableCheckBoxDefault();

        }
        private void EventEnableCheckBoxDefault()
        {
            checktooffNormal.Checked = checkEnable.Checked;
            checkToFault.Checked = checkEnable.Checked;
            checkToNormal.Checked = checkEnable.Checked;
            checkHiLimit.Checked = false;
            checkLoLimit.Checked = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
            AnalogIOV analogIOV = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(b => b.LogicalAddress == logicaladdress);
            if (analogIOV != null)
            {
                recentSelections = (analogIOV.RecentUnits ?? new List<string>()).Where(u => data.ContainsKey(u)).Take(5).ToList();
            }
            this.AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            ((FormBacNet)this.ParentForm).RefreshGridView();
        }
        private void textBoxMinPresVal_Validating(object sender, CancelEventArgs e)
        {
            ValidateBacNetInput(textBoxMinPresVal, e, "Real");
            if (!ValidateTextBox(textBoxMinPresVal, "Min Press Value", e, out double minValue)) return;
            if (!ValidateTextBox(textBoxMaxPresVal, "Max Press Value", e, out double maxValue)) return;
            if (!ValidateTextBox(textBoxLowLimit, "Low Limit", e, out double lowLimit)) return;
            if (maxValue != 0 && maxValue <= minValue)
            {
                SetError(textBoxMinPresVal, "Min Press Value must be less than Max Press Value", e);
                return;
            }
            else if (lowLimit != 0 && checkEnable.Checked && lowLimit < minValue)
            {
                SetError(textBoxLowLimit, "Low Limit should be greater than or equal to Min Press Value", e);
                return;
            }
            else
            {
                ClearErrors(textBoxMaxPresVal, textBoxMinPresVal, textBoxLowLimit);
                e.Cancel = false;
            }
        }
        private bool ValidateTextBox(TextBox textBox, string fieldName, CancelEventArgs e, out double value)
        {
            decimal parsedValue;
            bool iserror = false;
            string error = string.Empty;
            if (decimal.TryParse(textBox.Text.ToString(), out parsedValue))
            {
                // Check if the value is within the acceptable range
                if (parsedValue >= Convert.ToDecimal(int.MinValue) && parsedValue <= Convert.ToDecimal(int.MaxValue))
                {
                    // Check if the value has up to 2 decimal places
                    int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(parsedValue)[3])[2];
                    iserror = !(decimalPlaces <= 2);
                    error = "Invalid value, onlye 2 decimal places allowed";
                }
                else
                {
                    iserror = true;
                    error = "Invalid value Real data type range is -2147483648 to 2147483647";
                }
            }
            else
            {
                iserror = true;
                error = "Invalid value Real data type range is -2147483648 to 2147483647";
            }
            value = 0;
            if (iserror)
            {
                errorProvider.SetError(textBox, $"{fieldName} {error}");
                e.Cancel = true;
                return false;
            }

            if (!double.TryParse(textBox.Text, out value))
            {
                errorProvider.SetError(textBox, $"{fieldName} {error}");
                e.Cancel = true;
                return false;
            }
            else
            {
                errorProvider.SetError(textBox, "");
                e.Cancel = false;
                return true;
            }

        }
        private void SetError(Control control, string message, CancelEventArgs e)
        {
            errorProvider.SetError(control, message);
            e.Cancel = true;
            return;
        }
        private void ClearErrors(params Control[] controls)
        {
            foreach (var control in controls)
            {
                errorProvider.SetError(control, "");
            }
        }
        /// <summary>
        /// Validate operands depending on the type of control 
        /// </summary>
        /// <param name="control">Name of the control from whoes validate this call is generated.</param>
        /// <param name="e">This parameter will specify whether to add or update the line.</param>
        /// <returns>Describe return value.</returns>
        private void ValidateBacNetInput(TextBox control, CancelEventArgs e, string dataType)
        {
            bool validationSuccessful = true;
            string error = string.Empty;
            if (!string.IsNullOrEmpty(control.Text))
                validationSuccessful = BacNetValidator.ValidateBacNetInputVal(control.Text, out error, dataType);
            e.Cancel = !validationSuccessful;
            errorProvider.SetError(control, validationSuccessful ? null : error);
        }

        private void textBoxMaxPresVal_Validating(object sender, CancelEventArgs e)
        {
            ValidateBacNetInput(textBoxMaxPresVal, e, "Real");
            if (!ValidateTextBox(textBoxMaxPresVal, "Max Press Value", e, out double maxValue)) return;
            if (!ValidateTextBox(textBoxMinPresVal, "Min Press Value", e, out double minValue)) return;
            if (!ValidateTextBox(textBoxHighLimit, "High Limit", e, out double highLimit)) return;
            if (maxValue < minValue || maxValue == minValue)
            {
                SetError(textBoxMaxPresVal, "Max Press Value must be greater than Min Press Value", e);
                e.Cancel = true;
                return;
            }
            else if (highLimit != 0 && checkEnable.Checked && highLimit > maxValue)
            {
                SetError(textBoxHighLimit, "High Limit value should be less than or equal to Max Press Value", e);
                e.Cancel = true;
                return;
            }
            else
            {
                ClearErrors(textBoxMaxPresVal, textBoxMinPresVal, textBoxHighLimit);
                e.Cancel = false;
            }
        }
        private void checkBoxOutOfRange_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMinValue.Enabled = checkBoxOutOfRange.Checked;
            textBoxMaxValue.Enabled = checkBoxOutOfRange.Checked;
            if (!checkBoxOutOfRange.Checked)
            {
                textBoxMinValue.Text = textBoxMinPresVal.Text;
                textBoxMaxValue.Text = textBoxMaxPresVal.Text;
            }
        }

        private void textBoxMinValue_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMinValue.Text))
            {
                textBoxMinValue.Text = textBoxMinPresVal.Text;
                textBoxMinValue.SelectionStart = textBoxMinValue.Text.Length;
            }
        }

        private void textBoxMaxValue_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMaxValue.Text))
            {
                textBoxMaxValue.Text = textBoxMaxPresVal.Text;
                textBoxMaxValue.SelectionStart = textBoxMaxValue.Text.Length;
            }
        }

        private void textBoxMinValue_Validating(object sender, CancelEventArgs e)
        {
            if (!checkBoxOutOfRange.Checked) return;
            ValidateBacNetInput(textBoxMinValue, e, "Real");
            if (!ValidateTextBox(textBoxMinValue, "Minimum Value", e, out double minValue)) return;
            if (!ValidateTextBox(textBoxMinPresVal, "Min Pres Value", e, out double minPres)) return;
            if (minValue < minPres)
            {
                SetError(textBoxMinValue, "Minimum Value must be >= Min Pres Value", e);
                return;
            }
            ClearErrors(textBoxMinValue);
        }

        private void textBoxMaxValue_Validating(object sender, CancelEventArgs e)
        {
            if (!checkBoxOutOfRange.Checked) return;
            ValidateBacNetInput(textBoxMaxValue, e, "Real");
            if (!ValidateTextBox(textBoxMaxValue, "Maximum Value", e, out double maxValue)) return;
            if (!ValidateTextBox(textBoxMaxPresVal, "Max Pres Value", e, out double maxPres)) return;
            if (maxValue > maxPres)
            {
                SetError(textBoxMaxValue, "Maximum Value must be <= Max Pres Value", e);
                return;
            }
            ClearErrors(textBoxMaxValue);
        }
        private void textBoxMinPresVal_TextChanged(object sender, EventArgs e)
        {
            if (!checkBoxOutOfRange.Checked && !string.IsNullOrEmpty(textBoxMinPresVal.Text))
            {
                textBoxMinValue.Text = textBoxMinPresVal.Text;
            }
            ValidateRelinquishDefaultAgainstRange();
        }

        private void textBoxMaxPresVal_TextChanged(object sender, EventArgs e)
        {
            if (!checkBoxOutOfRange.Checked && !string.IsNullOrEmpty(textBoxMaxPresVal.Text))
            {
                textBoxMaxValue.Text = textBoxMaxPresVal.Text;
            }
            ValidateRelinquishDefaultAgainstRange();
        }
        private void AdjustCovAndInitialValuePosition()
        {
            bool isPureAnalogInput = ioTypeName == "AnalogInput";
            bool isUniversalInput = ioTypeName == "AnalogInput"; // Since UniversalInput becomes AnalogInput
            bool isAnalogOutput = ioTypeName == "AnalogOutput";
            bool isAnalogValue = ioTypeName == "AnalogValue";

            int defaultCovLabelTop = 300;
            int defaultCovTextBoxTop = 320;
            int defaultInitialLabelTop = 350;
            int defaultInitialTextBoxTop = 370;

            if (isPureAnalogInput)
            {
                // Expanded layout for pure AnalogInput (with gap for hidden controls)
                label4.Top = defaultCovLabelTop + 120;
                textBoxCovIncr.Top = defaultCovTextBoxTop + 100;
                lblInitialValue.Top = defaultInitialLabelTop + 120;
                textInitialValue.Top = defaultInitialTextBoxTop + 120;
            }
            else if (isAnalogOutput || isAnalogValue)
            {
                label4.Top = defaultCovLabelTop + 35;
                textBoxCovIncr.Top = defaultCovTextBoxTop + 15;
                lblInitialValue.Top = defaultInitialLabelTop + 18;
                textInitialValue.Top = defaultInitialTextBoxTop - 3;
            }            
        }
        private void ValidateRelinquishDefaultAgainstRange()
        {
            if (!string.IsNullOrEmpty(textReDefault.Text) &&
                decimal.TryParse(textReDefault.Text, out decimal relDefault) &&
                decimal.TryParse(textBoxMinPresVal.Text, out decimal minPresValue) &&
                decimal.TryParse(textBoxMaxPresVal.Text, out decimal maxPresValue))
            {
                if (relDefault < minPresValue || relDefault > maxPresValue)
                {
                    errorProvider.SetError(textReDefault,
                        $"Relinquish Default value is out of range. Must be between {minPresValue} and {maxPresValue}.");
                }
                else
                {
                    errorProvider.SetError(textReDefault, "");
                }
            }
        }
        private void textReDefault_Validating(object sender, CancelEventArgs e)
        {
            //if (!checkEnable.Checked)
            //return;
            string input = textReDefault.Text.Trim();
            float value;
            if (string.IsNullOrEmpty(input))
            {
                errorProvider.SetError(textReDefault, "");
                return;
            }
            if (!float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                errorProvider.SetError(textReDefault, "Enter a valid number (e.g. 123.456789).");
                e.Cancel = true;
                return;
            }
            if (value < -2147483648f || value > 2147483647f)
            {
                errorProvider.SetError(textReDefault, "Value must be between -2,147,483,648 and 2,147,483,647.");
                e.Cancel = true;
                return;
            }
            // NEW VALIDATION: Check against Min_Pres_Value and Max_Pres_Value
            if (decimal.TryParse(textBoxMinPresVal.Text, out decimal minPresValue) &&
                decimal.TryParse(textBoxMaxPresVal.Text, out decimal maxPresValue))
            {
                if ((decimal)value < minPresValue || (decimal)value > maxPresValue)
                {
                    errorProvider.SetError(textReDefault,
                        $"Relinquish Default value must be between {minPresValue} and {maxPresValue}.");
                    e.Cancel = true;
                    return;
                }
            }
            errorProvider.SetError(textReDefault, string.Empty);

            // Make sure this method is NOT restricting decimal places:
            ValidateBacNetInput(textReDefault, e, "Real");
        }
        private void textBoxHighLimit_Validating(object sender, CancelEventArgs e)
        {
            if (!checkEnable.Checked)
                return;
            ValidateBacNetInput(textBoxHighLimit, e, "Real");
            if (!ValidateTextBox(textBoxHighLimit, "High Limit", e, out double highLimit)) return;
            if (!ValidateTextBox(textBoxMaxPresVal, "Max Press Value", e, out double maxValue)) return;
            if (!ValidateTextBox(textBoxMinPresVal, "Min Press Value", e, out double minPress)) return;
            if (!ValidateTextBox(textBoxLowLimit, "Low Limit", e, out double lowLimit)) return;
            if (checkEnable.Checked && highLimit == 0 && lowLimit == 0)
            {
                errorProvider.SetError(textBoxHighLimit, "High limit should be greater than Low limit.");
                e.Cancel = true;
                return;
            }
            if (highLimit > maxValue)
            {
                errorProvider.SetError(textBoxHighLimit, "High Limit value should be less than or equal to Max Press Value");
                e.Cancel = true;
                return;
            }
            else if (highLimit < minPress)
            {
                errorProvider.SetError(textBoxHighLimit, "High Limit value should be greater than or equal to Min Press Value.");
                e.Cancel = true;
                return;
            }
            else if (highLimit <= lowLimit)
            {
                errorProvider.SetError(textBoxHighLimit, "High Limit value should be greater than Low Limit");
                e.Cancel = true;
                return;
            }
            else
            {
                e.Cancel = false;
                ClearErrors(textBoxHighLimit);
            }
        }
        private void textBoxLowLimit_Validating(object sender, CancelEventArgs e)
        {
            if (!checkEnable.Checked)
                return;
            ValidateBacNetInput(textBoxLowLimit, e, "Real");
            if (!ValidateTextBox(textBoxHighLimit, "High Limit", e, out double highLimit)) return;
            if (!ValidateTextBox(textBoxLowLimit, "Low Limit", e, out double lowLimit)) return;
            if (!ValidateTextBox(textBoxMinPresVal, "Min Press Value", e, out double minPress)) return;
            if (!ValidateTextBox(textBoxMaxPresVal, "Max Press Value", e, out double maxPress)) return;
            if (checkEnable.Checked && highLimit == 0 && lowLimit == 0)
            {
                errorProvider.SetError(textBoxLowLimit, "Low limit should be less than High limit.");
                e.Cancel = true;
                return;
            }
            if (lowLimit < minPress)
            {
                errorProvider.SetError(textBoxLowLimit, "Low Limit should be greater than or equal to Min Press Value");
                e.Cancel = true;
                return;
            }
            else if (lowLimit > maxPress)
            {
                errorProvider.SetError(textBoxLowLimit, "Low Limit should be less than or equal to Min Press Value");
                e.Cancel = true;
                return;
            }
            else if (lowLimit >= highLimit)
            {
                errorProvider.SetError(textBoxLowLimit, "Low Limit should be less than High Limit.");
                e.Cancel = true;
                return;
            }
            else
            {
                e.Cancel = false;
                ClearErrors(textBoxLowLimit);
            }
        }
        private void textDeadband_Validating(object sender, CancelEventArgs e)
        {
            if (!checkEnable.Checked)
                return;
            ValidateBacNetInput(textDeadband, e, "Real");
        }
        private void textTimeDelayNormal_Validating(object sender, CancelEventArgs e)
        {
            if (!checkEnable.Checked)
                return;
            ValidateBacNetInput(textTimeDelayNormal, e, "UDINT");
        }
        private void textTimeDelay_Validating(object sender, CancelEventArgs e)
        {
            if (!checkEnable.Checked)
                return;
            ValidateBacNetInput(textTimeDelay, e, "UDINT");
        }
        private void textBoxCovIncr_Validating(object sender, CancelEventArgs e)
        {
            ValidateBacNetInput(textBoxCovIncr, e, "Real");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
            SaveChanges(sender, e);
        }
        private string GetMainUnitFromSubUnit(string unit)
        {
            if (data.ContainsKey(unit))
            {
                return unit;
            }
            foreach (var mainUnit in data.Keys)
            {
                if (data[mainUnit].Contains(unit))
                {
                    return mainUnit;
                }
            }
            return unit;
        }
        private bool SaveChanges(object sender, EventArgs e)
        {
            if (requiresSubUnitSelection)
            {
                MessageBox.Show($"Please select a specific unit from the '{selectedMainUnit}' category before saving.","Unit Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxUnits.Focus();
                errorProvider.SetError(comboBoxUnits, "Please select a specific unit from the list");
                return false;
            }
            errorProvider.SetError(comboBoxUnits, "");
            if (!string.IsNullOrEmpty(textReDefault.Text) &&
                decimal.TryParse(textReDefault.Text, out decimal relDefault) &&
                decimal.TryParse(textBoxMinPresVal.Text, out decimal minPresValue) &&
                decimal.TryParse(textBoxMaxPresVal.Text, out decimal maxPresValue))
            {
                if (relDefault < minPresValue || relDefault > maxPresValue)
                {
                    MessageBox.Show($"Relinquish Default value {relDefault} is out of range. Must be between {minPresValue} and {maxPresValue}.",
                        "Value Out of Range", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textReDefault.Focus();
                    return false;
                }
            }
            var duplicateObject = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.ObjectName.Equals(textObjectName.Text.Trim()));
            if (BacNetFormFactory.ValidateObjectName(textObjectName.Text.Trim(), "AnalogIOValues") && duplicateObject?.ObjectIdentifier != labelObjIdentifier.Text)
            {
                MessageBox.Show("Object name is already used, change the name and try again ...", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var cancelEventArgs = new CancelEventArgs();
            if (checkEnable.Checked && textBoxHighLimit.Text == "0" && textBoxLowLimit.Text == "0")
            {
                errorProvider.SetError(textBoxHighLimit, " High limit should be greater than Low limit.");
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxHighLimit.Focus();
                return false;
            }
            textBoxHighLimit_Validating(textBoxHighLimit, cancelEventArgs);
            if (cancelEventArgs.Cancel && int.TryParse(textBoxHighLimit.Text, out int highlimit) && highlimit > 0)
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxHighLimit.Focus();
                return false;
            }
            textBoxLowLimit_Validating(textBoxLowLimit, cancelEventArgs);
            if (cancelEventArgs.Cancel && int.TryParse(textBoxLowLimit.Text, out int lowLimit) && lowLimit > 0)
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLowLimit.Focus();
                return false;
            }
            textBoxMaxPresVal_Validating(textBoxMaxPresVal, cancelEventArgs);
            if (cancelEventArgs.Cancel)
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxMaxPresVal.Focus();
                return false;
            }
            textBoxMinPresVal_Validating(textBoxMinPresVal, cancelEventArgs);
            if (cancelEventArgs.Cancel)
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxMinPresVal.Focus();
                return false;
            }
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string selectedUnit = comboBoxUnits.Text;
            if (!string.IsNullOrEmpty(selectedUnit))
            {
                recentSelections.Remove(selectedUnit);
                recentSelections.Insert(0, selectedUnit);
                if (recentSelections.Count > 5)
                {
                    recentSelections.RemoveAt(5);
                }
            }
            string selectedUnit1 = comboBoxUnits.Text;

            if (!string.IsNullOrEmpty(selectedUnit))
            {
                recentSubUnits.Remove(selectedUnit1);
                recentSubUnits.Insert(0, selectedUnit1);
                if (recentSubUnits.Count > 5)
                {
                    recentSubUnits.RemoveAt(5);
                }
            }
            AnalogIOV analogIOV = new AnalogIOV
            {
                ObjectIdentifier = labelObjIdentifier.Text,
                InstanceNumber = labelInstanceno.Text,
                ObjectType = labelObjType.Text,
                ObjectName = textObjectName.Text.Trim(),
                Description = textDescription.Text.Trim(),
                LogicalAddress = logicaladdress,
                Units = string.IsNullOrEmpty(selectedUnit) ? "95: no-units" : selectedUnit,
                MinPresValue = Convert.ToDecimal(textBoxMinPresVal.Text),
                MaxPresValue = Convert.ToDecimal(textBoxMaxPresVal.Text),
                MinValue = Convert.ToDecimal(textBoxMinValue.Text),
                MaxValue = Convert.ToDecimal(textBoxMaxValue.Text),
                COVIncrement = textBoxCovIncr.Text,
                RelinquishDefault = string.IsNullOrWhiteSpace(textReDefault.Text) ? "0" : textReDefault.Text.Trim(),
                TimeDelay = checkEnable.Checked ? Convert.ToInt64(textTimeDelay.Text) : 0,
                TimeDelayNormal = checkEnable.Checked ? Convert.ToInt64(textTimeDelayNormal.Text) : 0,
                NotificationClass = checkEnable.Checked ? Convert.ToInt32(comboNotifyclass.SelectedValue) : 0,
                HighLimit = checkEnable.Checked ? Convert.ToDecimal(textBoxHighLimit.Text) : 0,
                LowLimit = checkEnable.Checked ? Convert.ToDecimal(textBoxLowLimit.Text) : 0,
                Deadband = checkEnable.Checked ? Convert.ToDecimal(textDeadband.Text) : 0,
                EventDetectionEnable = checkEnable.Checked ? 1 : 0,
                LimitEnable = checkEnable.Checked ? BacNetValidator.GetLimitCheckBoxResult(checkHiLimit.Checked, checkLoLimit.Checked) : 0,
                EventEnable = checkEnable.Checked ? BacNetValidator.GetEventCheckBoxResult(checktooffNormal.Checked, checkToFault.Checked, checkToNormal.Checked) : 0,
                NotifyType = checkEnable.Checked ? (checkAlarm.Checked ? 0 : checkEvent.Checked ? 1 : 0) : 0,
                IsEnable = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues
                    .FirstOrDefault(b => b.LogicalAddress == logicaladdress)?.IsEnable ?? true,
                BinaryAValue = cmb_BinaryValue.SelectedIndex,
                OutOfRangeEnabled = checkBoxOutOfRange.Checked,
                isEdited = true,
                RecentUnits = recentSelections.ToList()
            };

            BacNetIP bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            bacNetIP.AnalogIOValues.Remove(bacNetIP.AnalogIOValues.FirstOrDefault(b => b.LogicalAddress == logicaladdress));
            bacNetIP.AnalogIOValues.Add(analogIOV);
            var tag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress == logicaladdress);
            if (tag != null)
            {
                tag.Tag = analogIOV.ObjectName;
                tag.InitialValue = textInitialValue.Text;
            }
            CommonFunctions.UpdateTagNames(logicaladdress, analogIOV.ObjectName);
            MessageBox.Show("Analog information updated", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            XMPS.Instance.LoadedProject.isChanged = false;

            if ((FormBacNet)this.ParentForm != null)
                ((FormBacNet)this.ParentForm).RefreshGridView();
            else
            {
                var formBacNetInstance = Application.OpenForms
                              .OfType<FormBacNet>()
                              .FirstOrDefault(f => f.Name == "FormBacNet");
                formBacNetInstance?.RefreshGridView();
            }
            //// Refresh grid
            //var formBacNet = Application.OpenForms.OfType<FormBacNet>().FirstOrDefault(f => f.Name == "FormBacNet") ?? (FormBacNet)this.ParentForm;
            //formBacNet?.RefreshGridView();

            return true;
        }
        private void comboBoxUnits_Validating(object sender, CancelEventArgs e)
        {
            if (requiresSubUnitSelection)
            {
                e.Cancel = true;
                errorProvider.SetError(comboBoxUnits, "Please select a specific unit from the list");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(comboBoxUnits, "");
            }
        }
        private void textBoxHighLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;
            // Check if the input is valid
            e.Handled = !IsValidInput(e.KeyChar, textBox);
        }
        private bool IsValidInput(char keyChar, TextBox textBox)
        {
            string currentText = textBox.Text;
            if (char.IsControl(keyChar))
            {
                return true;
            }
            if (char.IsDigit(keyChar) || keyChar == '.')
            {
                if (keyChar == '.' && currentText.Contains('.'))
                {
                    return false;
                }
                return true;
            }
            if (keyChar == '-')
            {
                int selectionStart = textBox.SelectionStart;
                int selectionLength = textBox.SelectionLength;

                if (selectionLength == currentText.Length || (selectionStart == 0 && !currentText.Contains('-')))
                {
                    return true;
                }

                return false;
            }
            return false;
        }
        private void HandleNotificationCheckBoxes(bool isAlarmChecked, bool isNotificationChecked, bool isEventChecked)
        {
            //temporarily remove event notification checkboxes not call event recursively.
            checkAlarm.CheckedChanged -= checkAlarm_CheckedChanged;
            checkEvent.CheckedChanged -= checkEvent_CheckedChanged;

            if (checkEnable.Checked)
            {
                checkAlarm.Checked = isAlarmChecked;
                checkEvent.Checked = isEventChecked;
            }
            else
            {
                checkAlarm.Checked = false;
                checkEvent.Checked = false;
            }
            //adding event notification checkboxes.
            checkAlarm.CheckedChanged += checkAlarm_CheckedChanged;
            checkEvent.CheckedChanged += checkEvent_CheckedChanged;
        }
        private void checkAlarm_CheckedChanged(object sender, EventArgs e)
        {
            HandleNotificationCheckBoxes(true, false, false);
        }
        private void checkEvent_CheckedChanged(object sender, EventArgs e)
        {
            HandleNotificationCheckBoxes(false, false, true);
        }
        private void checkNotification_CheckedChanged(object sender, EventArgs e)
        {
            HandleNotificationCheckBoxes(false, true, false);
        }
        List<string> recentSelections = new List<string>();

        private void comboBoxUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUnits.Text.StartsWith("--"))
            {
                comboBoxUnits.Text = recentSelections[0];
            }
            string selectedItem = comboBoxUnits.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedItem)) return;
            // Check if this is a main unit that has sub-units
            if (data.ContainsKey(selectedItem) && data[selectedItem].Count > 0)
            {
                requiresSubUnitSelection = true;
                selectedMainUnit = selectedItem;
                CreateAndPopulateListBox(selectedItem);
            }
            else
            {
                requiresSubUnitSelection = false;
                selectedMainUnit = string.Empty;
            }

            RebuildComboBoxUnits(selectedItem);
            comboBoxUnits.Text = selectedItem;
        }
        private void BindUnitsData()
        {
            data = new Dictionary<string, List<string>>
            {
                ["Acceleration"] = new List<string>
                {
                    "166: meters-per-second-per-second"
                },
                ["Area"] = new List<string>
                {
                    "0: square-meters m²",
                    "116: square-centimeters cm²",
                    "1: square-feet ft²",
                    "115: square-inches in²"
                },
                ["Currency"] = new List<string>
                {
                    "105: currency1",
                    "106: currency2",
                    "107: currency3",
                    "108: currency4",
                    "109: currency5",
                    "110: currency6",
                    "111: currency7",
                    "112: currency8",
                    "113: currency9",
                    "114: currency10"
                },
                ["Electrical"] = new List<string>
                {
                    "2: milliamperes  mA",
                    "3: amperes A",
                    "167: amperes-per-meter A/m",
                    "168: amperes-per-square-meter  A/m²",
                    "169: ampere-square-meters  Am²",
                    "199: decibels dB",
                    "200: decibels-millivolt dBmV",
                    "201: decibels-volt dBV",
                    "170: farads F",
                    "171: henrys H",
                    "4: ohms Ω",
                    "237: ohm-meter-squared-per-meter Ωm²/m",
                    "172: ohm-meters Ωm",
                    "145: milliohms mΩ",
                    "122: kilohms KΩ",
                    "123: megohms MΩ",
                    "190: microsiemens",
                    "202: millisiemens",
                    "173: siemens",
                    "174: siemens-per-meter",
                    "175: teslas T",
                    "5: volts V",
                    "124: millivolts  mV",
                    "6: kilovolts KV",
                    "7: megavolts MV",
                    "8: volt-amperes VA",
                    "9: kilovolt-amperes KVA",
                    "10: megavolt-amperes MVA",
                    "11: volt-amperes-reactive var",
                    "12: kilovolt-amperes-reactive KVAR",
                    "13: megavolt-amperes-reactive MVAR",
                    "176: volts-per-degree-kelvin V/K",
                    "177: volts-per-meter V/m",
                    "14: degrees-phase",
                    "15: power-factor KW",
                    "178: webers Wb"
                },
                ["Energy"] = new List<string>
                {
                    "238: ampere-seconds",
                    "239: volt-ampere-hours",
                    "240: kilovolt-ampere-hours",
                    "241: megavolt-ampere-hours",
                    "242: volt-ampere-hours-reactive",
                    "243: kilovolt-ampere-hours-reactive",
                    "244: megavolt-ampere-hours-reactive",
                    "245: volt-square-hours",
                    "246: ampere-square-hours",
                    "16: joules J",
                    "17: kilojoules KJ",
                    "125: kilojoules-per-kilogram KJ/Kg",
                    "126: megajoules MJ",
                    "18: watt-hours Wh",
                    "19: kilowatt-hours KWh",
                    "146: megawatt-hours Mwh",
                    "203: watt-hours-reactive",
                    "204: kilowatt-hours-reactive Kvarh",
                    "205: megawatt-hours-reactive Mvarh",
                    "20: btus",
                    "147: kilo-btus",
                    "148: mega-btus",
                    "21: therms",
                    "22: ton-hours"
                },
                ["Enthalpy"] = new List<string>
                {
                    "23: joules-per-kilogram-dry-air",
                    "149: kilojoules-per-kilogram-dry-air",
                    "150: megajoules-per-kilogram-dry-air",
                    "24: btus-per-pound-dry-air",
                    "117: btus-per-pound"
                },
                ["Entropy"] = new List<string>
                {
                    "127: joules-per-degree-kelvin",
                    "151: kilojoules-per-degree-kelvin",
                    "152: megajoules-per-degree-kelvin",
                    "128: joules-per-kilogram-degree-kelvin"
                },
                ["Force"] = new List<string>
                {
                    "153: newton N"
                },
                ["Frequency"] = new List<string>
                {
                    "25: cycles-per-hour",
                    "26: cycles-per-minute",
                    "27: hertz Hz",
                    "129: kilohertz KHz",
                    "130: megahertz Mhz",
                    "131: per-hour"
                },
                ["Humidity"] = new List<string>
                {
                    "28: grams-of-water-per-kilogram-dry-air",
                    "29: percent-relative-humidity"
                },
                ["Length"] = new List<string>
                {
                    "194: micrometers",
                    "30: millimeters mm",
                    "118: centimeters cm",
                    "193: kilometers Km",
                    "31: meters m",
                    "32: inches in",
                    "33: feet ft"
                },
                ["Light"] = new List<string>
                {
                    "179: candelas cd",
                    "180: candelas-per-square-meter",
                    "34: watts-per-square-foot",
                    "35: watts-per-square-meter",
                    "36: lumens",
                    "37: luxes",
                    "38: foot-candles"
                },
                ["Mass"] = new List<string>
                {
                    "196: milligrams mg",
                    "195: grams g",
                    "39: kilograms kg",
                    "40: pounds-mass",
                    "41: tons"
                },
                ["Mass Flow"] = new List<string>
                {
                    "154: grams-per-second g/s",
                    "155: grams-per-minute g/m",
                    "42: kilograms-per-second kg/s",
                    "43: kilograms-per-minute kg/min",
                    "44: kilograms-per-hour kg/h",
                    "119: pounds-mass-per-second",
                    "45: pounds-mass-per-minute",
                    "46: pounds-mass-per-hour",
                    "156: tons-per-hour"
                },
                ["Power"] = new List<string>
                {
                    "132: milliwatts mW",
                    "47: watts W",
                    "48: kilowatts KW",
                    "49: megawatts MW",
                    "50: btus-per-hour",
                    "157: kilo-btus-per-hour",
                    "247: joule-per-hours",
                    "51: horsepower hp",
                    "52: tons-refrigeration"
                },
                ["Pressure"] = new List<string>
                {
                    "53: pascals Pa",
                    "133: hectopascals",
                    "54: kilopascals",
                    "134: millibars",
                    "55: bars",
                    "56: pounds-force-per-square-inch",
                    "206: millimeters-of-water",
                    "57: centimeters-of-water",
                    "58: inches-of-water",
                    "59: millimeters-of-mercury",
                    "60: centimeters-of-mercury",
                    "61: inches-of-mercury"
                },
                ["Temperature"] = new List<string>
                {
                    "62: degrees-celsius",
                    "63: degrees-kelvin",
                    "181: degrees-kelvin-per-hour",
                    "182: degrees-kelvin-per-minute",
                    "64: degrees-fahrenheit",
                    "65: degree-days-celsius",
                    "66: degree-days-fahrenheit",
                    "120: delta-degrees-fahrenheit",
                    "121: delta-degrees-kelvin"
                },
                ["Time"] = new List<string>
                {
                    "67: years",
                    "68: months",
                    "69: weeks",
                    "70: days",
                    "71: hours",
                    "72: minutes",
                    "73: seconds",
                    "158: hundredths-seconds",
                    "159: milliseconds"
                },
                ["Torque"] = new List<string>
                {
                    "160: newton-meters"
                },
                ["Velocity"] = new List<string>
                {
                    "161: millimeters-per-second",
                    "162: millimeters-per-minute",
                    "74: meters-per-second",
                    "163: meters-per-minute",
                    "164: meters-per-hour",
                    "75: kilometers-per-hour",
                    "76: feet-per-second",
                    "77: feet-per-minute",
                    "78: miles-per-hour"
                },
                ["Volume"] = new List<string>
                {
                    "79: cubic-feet",
                    "80: cubic-meters",
                    "81: imperial-gallons",
                    "197: milliliters",
                    "82: liters",
                    "83: us-gallons"
                },
                ["Volumetric Flow"] = new List<string>
                {
                    "142: cubic-feet-per-second",
                    "84: cubic-feet-per-minute",
                    "254: million-standard-cubic-feet-per-minute",
                    "191: cubic-feet-per-hour",
                    "248: cubic-feet-per-day",
                    "47808: standard-cubic-feet-per-day",
                    "47809: million-standard-cubic-feet-per-day",
                    "47810: thousand-cubic-feet-per-day",
                    "47811: thousand-standard-cubic-feet-per-day",
                    "47812: pounds-mass-per-day",
                    "85: cubic-meters-per-second",
                    "165: cubic-meters-per-minute",
                    "135: cubic-meters-per-hour",
                    "249: cubic-meters-per-day",
                    "86: imperial-gallons-per-minute",
                    "198: milliliters-per-second",
                    "87: liters-per-second",
                    "88: liters-per-minute",
                    "136: liters-per-hour",
                    "89: us-gallons-per-minute",
                    "192: us-gallons-per-hour"
                },
                ["Other"] = new List<string>
                {
                    "90: degrees-angular",
                    "91: degrees-celsius-per-hour",
                    "92: degrees-celsius-per-minute",
                    "93: degrees-fahrenheit-per-hour",
                    "94: degrees-fahrenheit-per-minute",
                    "183: joule-seconds",
                    "186: kilograms-per-cubic-meter",
                    "137: kilowatt-hours-per-square-meter",
                    "138: kilowatt-hours-per-square-foot",
                    "250: watt-hours-per-cubic-meter",
                    "251: joules-per-cubic-meter",
                    "139: megajoules-per-square-meter",
                    "140: megajoules-per-square-foot",
                    "252: mole-percent",
                    "95: no-units",
                    "187: newton-seconds",
                    "188: newtons-per-meter",
                    "96: parts-per-million",
                    "97: parts-per-billion",
                    "253: pascal-seconds",
                    "98: percent",
                    "143: percent-obscuration-per-foot",
                    "144: percent-obscuration-per-meter",
                    "99: percent-per-second",
                    "100: per-minute",
                    "101: per-second",
                    "102: psi-per-degree-fahrenheit",
                    "103: radians",
                    "184: radians-per-second",
                    "104: revolutions-per-minute",
                    "185: square-meters-per-newton",
                    "189: watts-per-meter-per-degree-kelvin",
                    "141: watts-per-square-meter-degree-kelvin",
                    "207: per-mille",
                    "208: grams-per-gram",
                    "209: kilograms-per-kilogram",
                    "210: grams-per-kilogram",
                    "211: milligrams-per-gram",
                    "212: milligrams-per-kilogram",
                    "213: grams-per-milliliter",
                    "214: grams-per-liter",
                    "215: milligrams-per-liter",
                    "216: micrograms-per-liter",
                    "217: grams-per-cubic-meter",
                    "218: milligrams-per-cubic-meter",
                    "219: micrograms-per-cubic-meter",
                    "220: nanograms-per-cubic-meter",
                    "221: grams-per-cubic-centimeter g/cm³",
                    "222: becquerels",
                    "223: kilobecquerels",
                    "224: megabecquerels",
                    "225: gray",
                    "226: milligray",
                    "227: microgray",
                    "228: sieverts",
                    "229: millisieverts",
                    "230: microsieverts",
                    "231: microsieverts-per-hour",
                    "47814: millirems",
                    "47815: millirems-per-hour",
                    "232: decibels-a",
                    "233: nephelometric-turbidity-unit",
                    "234: pH",
                    "235: grams-per-square-meter g/m²",
                    "236: minutes-per-degree-kelvin"
                }
            };
        }
        private void textObjectName_Validating(object sender, CancelEventArgs e)
        {
            bool isObjNamePresent = XMPS.Instance.LoadedProject.Tags.Any(t => t.Tag.Equals(textObjectName.Text));
            string tagAdd = XMPS.Instance.LoadedProject.Tags.Where(t => t.Tag.Equals(textObjectName.Text)).Select(t => t.LogicalAddress).FirstOrDefault();
            if (isObjNamePresent && logicaladdress != tagAdd)
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Object Name is Already Used");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textObjectName, null);
            }

            if (!string.IsNullOrWhiteSpace(textObjectName.Text.Trim()) && textObjectName.Text.Trim().Any(ch => !(char.IsLetterOrDigit(ch) || ch == '_' || ch != ' ')))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Only letters, digits, underscore (_) and spaces are allowed.");
            }

            //for checking length
            if (!string.IsNullOrWhiteSpace(textObjectName.Text) && textObjectName.Text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Please resolve the errors first");
            }
            if (textObjectName.Text == "")
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Please resolve the errors first");
            }
            if (!string.IsNullOrEmpty(textObjectName.Text) && char.IsDigit(textObjectName.Text[0]))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Tag name cannot start with a number.");
                return;
            }
            foreach (char ch in textObjectName.Text.Trim())
            {
                if (!char.IsLetterOrDigit(ch) && ch != 95 && ch != 3 && ch != 22) // Allowed characters
                {
                    e.Cancel = true;
                    errorProvider.SetError(textObjectName, "Invalid character detected. Only letters, digits, and underscore (_) are allowed.");
                    return;
                }
            }
            string text = textObjectName?.Text?.Trim() ?? string.Empty;

            // Check if text is empty or whitespace
            if (string.IsNullOrWhiteSpace(text))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Tag name cannot be empty or whitespace.");
                return;
            }
        }
        private void textBoxMinPresVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMinPresVal.Text))
            {
                textBoxMinPresVal.Text = "0";
                textBoxMinPresVal.SelectionStart = textBoxMinPresVal.Text.Length;
            }
            if (textBoxMinPresVal.Text.Equals("-0"))
            {
                textBoxMinPresVal.Text = "0";
            }
        }
        private void textBoxMaxPresVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMaxPresVal.Text))
            {
                textBoxMaxPresVal.Text = "0";
                textBoxMaxPresVal.SelectionStart = textBoxMaxPresVal.Text.Length;
            }
            if (textBoxMaxPresVal.Text.Equals("-0"))
            {
                textBoxMaxPresVal.Text = "0";
            }
        }
        private void textReDefault_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textReDefault.Text))
            {
                textReDefault.Text = "0";
                textReDefault.SelectionStart = textReDefault.Text.Length;
            }
        }
        private void textBoxCovIncr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCovIncr.Text))
            {
                textBoxCovIncr.Text = "0";
                textBoxCovIncr.SelectionStart = textBoxCovIncr.Text.Length;
            }
            if (textBoxCovIncr.Text.Equals("-0"))
            {
                textBoxCovIncr.Text = "0";
            }
        }
        private void textTimeDelay_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textTimeDelay.Text))
            {
                textTimeDelay.Text = "0";
                textTimeDelay.SelectionStart = textTimeDelay.Text.Length;
            }
        }
        private void textTimeDelayNormal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textTimeDelayNormal.Text))
            {
                textTimeDelayNormal.Text = "0";
                textTimeDelayNormal.SelectionStart = textTimeDelayNormal.Text.Length;
            }
        }
        private void textBoxHighLimit_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxHighLimit.Text))
            {
                textBoxHighLimit.Text = "0";
                textBoxHighLimit.SelectionStart = textBoxHighLimit.Text.Length;
            }
            if (textBoxHighLimit.Text.Equals("-0"))
            {
                textBoxHighLimit.Text = "0";
            }
        }
        private void textBoxLowLimit_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLowLimit.Text))
            {
                textBoxLowLimit.Text = "0";
                textBoxLowLimit.SelectionStart = textBoxLowLimit.Text.Length;
            }
            if (textBoxLowLimit.Text.Equals("-0"))
            {
                textBoxLowLimit.Text = "0";
            }
        }
        private void textDeadband_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textDeadband.Text))
            {
                textDeadband.Text = "0";
                textDeadband.SelectionStart = textDeadband.Text.Length;
            }
            if (textDeadband.Text.Equals("-0"))
            {
                textDeadband.Text = "0";
            }
        }
        private void textTimeDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void textTimeDelayNormal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void comboBoxUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void checkAlarm_Validating(object sender, CancelEventArgs e)
        {
            if (checkEnable.Checked == true && checkAlarm.Checked == false && checkEvent.Checked == false)
            {
                e.Cancel = true;
                errorProvider.SetError(checkEvent, "Please Select Notify Type ");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(checkEvent, null);
            }
        }
        private void textInitialValue_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (textInitialValue.Text.Equals("-0"))
                {
                    textInitialValue.Text = "0";
                }

                long resultDINT;
                if (ioTypeName == "AnalogValue" && long.TryParse(textInitialValue.Text, out resultDINT))
                {
                    if (!(resultDINT >= -2147483648 && resultDINT <= 2147483647))
                    {
                        SetValidationError("Invalid Initial value for selected Address", e);
                        return;
                    }
                }
                // string value = textInitialValue.Text.Contains(".") ? textInitialValue.Text.Replace(".", "") : textInitialValue.Text;
                if (ioTypeName == "AnalogOutput" && long.TryParse(textInitialValue.Text, out resultDINT))
                {
                    if (!(resultDINT >= -2147483648 && resultDINT <= 2147483647))
                    {
                        SetValidationError("Invalid Initial value for selected Address", e);
                        return;
                    }
                }
                SetValidationError(null, e);
            }
            catch
            {
                SetValidationError("Invalid Initial value for selected Address", e);
                return;
            }
        }
        private void SetValidationError(string errorMessage, CancelEventArgs e)
        {
            errorProvider.SetError(textInitialValue, errorMessage);
            e.Cancel = !string.IsNullOrEmpty(errorMessage);
        }
        private void textInitialValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '-') && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
            // Only allow '-' at the beginning of the text
            if (e.KeyChar == '-' && (sender as TextBox).SelectionStart != 0)
            {
                e.Handled = true;
            }
        }

        private void comboBoxUnits_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox combo = (ComboBox)sender;
            string itemText = combo.Items[e.Index].ToString();

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            bool isRecent = recentSelections.Any() && recentSelections[0].Equals(itemText);

            Color bgColor;
            Color fgColor;

            if (isSelected)
            {
                bgColor = SystemColors.Highlight;
                fgColor = SystemColors.HighlightText;
            }
            else if (isRecent)
            {
                bgColor = Color.FromArgb(36, 114, 200); // Light blue for recent items
                fgColor = SystemColors.ControlText;
            }
            else
            {
                bgColor = SystemColors.Window;
                fgColor = SystemColors.ControlText;
            }

            // Draw background
            using (SolidBrush backgroundBrush = new SolidBrush(bgColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            // Draw text
            using (SolidBrush textBrush = new SolidBrush(fgColor))
            {
                e.Graphics.DrawString(itemText, e.Font, textBrush, e.Bounds.X, e.Bounds.Y);
            }

            e.DrawFocusRectangle();
        }

        private void textObjectName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textObjectName.Text) && textObjectName.Text.Length > 25)
            {
                errorProvider.SetError(textObjectName, "Object Name must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(textObjectName, null);
            }
            if (string.IsNullOrEmpty(textObjectName.Text))
            {
                //e.Cancel = true;
                errorProvider.SetError(textObjectName, "Please resolve the errors first");
            }
        }

        private void textDescription_TextChanged(object sender, EventArgs e)
        {
            if (textDescription.Text.Length > 25)
            {
                errorProvider.SetError(textDescription, "Object Name must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(textDescription, null);
            }
        }

        private void textDescription_Validating(object sender, CancelEventArgs e)
        {
            //for checking length
            if (!string.IsNullOrWhiteSpace(textDescription.Text) && textDescription.Text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textDescription, "Please resolve the errors first");
            }
        }
    }
}
