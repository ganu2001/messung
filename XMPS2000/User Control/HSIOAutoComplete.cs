using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XMPS2000.Configuration;
using XMPS2000.Core;
using XMPS2000.Core.Base;

namespace XMPS2000.User_Control
{
    public partial class HSIOAutoComplete : Form
    {
        public event EventHandler OnSelect;
        public ErrorProvider errorProvider;
        public string EnteredText { set; get; }
        public bool ValidText { set; get; }
        private List<string> _allTags;
        public string SelectedText { set; get; }
        public string Datatype = "";
        public string ActualDataType = "";
        public string currentTagName = "";
        public string currentHSBName = "";
        public string CurrentHSIOFunctionBlockName;
        public string CurrentInputTextName;
        public string text { set; get; }
        List<string> operands = new List<string>
        {
            "Normal Operand","Negation Operand","Numeric Operand"
        };

        public HSIOAutoComplete(string dataType, string curTag, string HSIOName, string HSIOFunName, string CurrentInput)
        {
            this.CurrentHSIOFunctionBlockName = HSIOFunName;
            this.CurrentInputTextName = CurrentInput;
            this.Datatype = dataType;
            this.ActualDataType = dataType;
            this.currentTagName = curTag;
            this.currentHSBName = HSIOName;
            InitializeComponent();
            this.GroupBoxTagList.Visible = true;
            this.LabelAddTag.Text = "Add Tag";
            if (dataType == "Bool" || dataType == "Bit")
            {
                this.comboBoxOperandType.DataSource = operands;
            }
            else
            {
                operands.Remove("Negation Operand");
                this.comboBoxOperandType.DataSource = operands;
            }
            //this.comboBoxOperandType.Text=
            AddListItem(dataType, HSIOName);

            string input = curTag;
            bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");
            if (containsAlphabetic)
            {
                if (!curTag.StartsWith("~"))
                {
                    comboBoxOperandType.Text = "Normal Operand";
                }
                else
                {
                    comboBoxOperandType.Text = "Negation Operand";
                }
            }
            else if (!containsAlphabetic)
            {
                if (input == "???" || input=="")
                {
                    if (dataType == "Bool" || dataType == "Bit")
                    {
                        this.comboBoxOperandType.DataSource = operands;
                    }
                    else
                    {
                        operands.Remove("Negation Operand");
                        this.comboBoxOperandType.DataSource = operands;
                    }
                }
                else
                {
                    comboBoxOperandType.Text = "Numeric Operand";
                }
            }

        }

        public void ClearList()
        {
            HsioListBox.Items.Clear();
        }

        public void AddListItem(string DataType, string HSIOName)
        {
            _allTags = new List<string>();
            string DataType1 = Datatype == "Double Word" ? "UDINT" : Datatype == "UDINT" ? "Double Word" : "";
            if (DataType == "Bit")
            {
                DataType = "Bool";
            }
            List<XMIOConfig> taglist = new List<XMIOConfig>();
            if (DataType == "Bool")
            {
                var allTags = XMPS.Instance.LoadedProject.Tags.AsEnumerable(); 
                if (HSIOName.StartsWith("HSI"))
                {
                    taglist = allTags.Where(t => (t.IoList == Core.Types.IOListType.OnBoardIO &&(t.Type == Core.Types.IOType.DigitalOutput ||t.Type == Core.Types.IOType.DigitalInput)) ||t.Label == DataType ||t.Tag.EndsWith("_OR") ||t.Tag.EndsWith("_OL")).OrderBy(t => t.LogicalAddress).ToList();
                }
                else
                {
                    taglist = allTags.Where(t => (t.IoList == Core.Types.IOListType.OnBoardIO && (t.Type == Core.Types.IOType.DigitalOutput ||t.Type == Core.Types.IOType.DigitalInput)) ||t.Label == DataType ||t.Tag.EndsWith("_OR") ||t.Tag.EndsWith("_OL")).OrderBy(t => t.LogicalAddress).ToList();
                }
            }
            else
            {
                taglist = XMPS.Instance.LoadedProject.Tags.Where(t => (t.Label == DataType || t.Label == DataType1) && t.IoList != Core.Types.IOListType.Default).ToList();
            }
            HsioListBox.Items.Clear();
            Datatype = DataType;
            foreach (XMIOConfig tag in taglist)
            {
                HsioListBox.Items.Add(tag.Tag);
                _allTags.Add(tag.Tag);
            }

        }
        public void SetText(string text)
        {
            textBox1.Text = text;
            textBox1.Focus();
        }


        private void listBox1_Click(object sender, EventArgs e)
        {
            if (HsioListBox.SelectedIndex != -1)
            {
                SelectedText = HsioListBox.SelectedItem.ToString();
                if (OnSelect != null)
                    OnSelect(this, new EventArgs());
            }
            CloseForm(SelectedText);
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (HsioListBox.SelectedIndex != -1 && e.KeyCode == Keys.Enter)
            {
                SelectedText = HsioListBox.SelectedItem.ToString();
                if (OnSelect != null)
                    OnSelect(this, new EventArgs());
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectedText = textBox1.Text;
                if (OnSelect != null)
                    OnSelect(this, new EventArgs());
                if(textBox1.Text=="")
                    CloseForm(null, true);
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            errorProvider = new ErrorProvider();
            ValidText = true;
            EnteredText = textBox1.Text.ToString();
            string OperandType = comboBoxOperandType.Text;
            bool validText = VadidationTextBox(EnteredText, OperandType);
            if (!validText)
            {
                errorProvider.SetError(textBox1, "Not valid Numeric Entry for " + ActualDataType + " Data Type");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterTagList(textBox1.Text);
        }
        private void FilterTagList(string searchText)
        {
            if (_allTags == null)
            {
                _allTags = new List<string>();
                foreach (var item in HsioListBox.Items)
                {
                    _allTags.Add(item.ToString());
                }
            }
            HsioListBox.Items.Clear();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                HsioListBox.Items.AddRange(_allTags.ToArray());
                return;
            }
            var filteredItems = _allTags
                .Where(tag => tag.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            HsioListBox.Items.AddRange(filteredItems.ToArray());
        }
        private bool VadidationTextBox(string enteredText, string operandType)
        {
            if (operandType == "Numeric Operand" && textBox1.Text != "")
            {
                switch (Datatype)
                {
                    case "Bool":
                        if (!textBox1.Text.Equals("1") && !textBox1.Text.Equals("0"))
                        {
                            return false;
                        }
                        break;
                    case "Byte":
                        if (textBox1.Text.StartsWith("-") || !byte.TryParse(textBox1.Text, out _))
                        {
                            return false;
                        }
                        break;
                    case "Word":
                        if (textBox1.Text.StartsWith("-") || !ushort.TryParse(textBox1.Text, out _))
                        {
                            return false;
                        }
                        break;
                    case "Double Word":
                        if (textBox1.Text.StartsWith("-") || !uint.TryParse(textBox1.Text, out _))
                        {
                            return false;
                        }
                        break;
                    case "Int":
                        if (!int.TryParse(textBox1.Text, out _))
                        {
                            return false;
                        }
                        break;
                    case "Real":
                        Double result;
                        if (Double.TryParse(textBox1.Text, out result))
                        {
                            if (Convert.ToDouble(textBox1.Text) < -2147483648 || Convert.ToDouble(textBox1.Text) > 2147483647)
                            {
                                return false;
                            }
                        }
                        break;
                    case "DINT":
                        long resultDINT;
                        if (long.TryParse(textBox1.Text, out resultDINT))
                        {
                            if (!(resultDINT >= -2147483648 && resultDINT <= 2147483647))
                            {
                                return false;
                            }
                        }
                        break;
                    case "UDINT":
                        uint resultUdint;
                        if (uint.TryParse(textBox1.Text, out resultUdint))
                        {
                            if (!(resultUdint >= 0 && resultUdint <= 4294967295))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        private void CloseForm(string sendText,bool isClear=false)
        {
            string OperandType = comboBoxOperandType.Text;
            if (OperandType == "Negation Operand" && sendText != null)
            {
                if (sendText == "" && currentTagName != "")
                {
                    EnteredText = "~" + currentTagName;
                }
                else if (sendText == "" && currentTagName == "???")
                {
                    EnteredText = "~" + currentTagName;
                }
                else
                {
                    XMIOConfig tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == sendText.Replace("~", "")).FirstOrDefault();
                    if (tag != null && tag.ShowLogicalAddress == true && OperandType != "Numeric Operand")
                    {
                        EnteredText = "~" + tag.LogicalAddress;
                    }
                    else
                    {
                        EnteredText = "~" + sendText;
                    }
                }
            }
            else
            {
                if (sendText == null && currentTagName != "" && !isClear)
                {
                    EnteredText = currentTagName;
                }
                else if (sendText == "" && currentTagName == "???" && !isClear)
                {
                    EnteredText = currentTagName;
                }
                else if (isClear)
                {
                    EnteredText = null;
                }
                else
                {
                    XMIOConfig tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == sendText).FirstOrDefault();
                    if (tag != null && tag.ShowLogicalAddress == true && OperandType != "Numeric Operand")
                    {
                        EnteredText = tag.LogicalAddress;
                    }
                    else
                    {
                        EnteredText = sendText;
                    }
                }
            }
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            errorProvider = new ErrorProvider();
            string OperandType = comboBoxOperandType.Text;
            bool validText = VadidationTextBox(EnteredText, OperandType);
            if (!validText)
            {
                errorProvider.SetError(textBox1, "Not valid Numeric Entry for " + ActualDataType + " Data Type");
                return;
            }
            string input = textBox1.Text;
            bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");
            if (OperandType != "Numeric Operand" && (containsAlphabetic || input == "" || input == "???"))
            {
                XMIOConfig tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == input).FirstOrDefault();
                if (tag == null)
                {
                    XMProForm tempForm = new XMProForm();
                    tempForm.StartPosition = FormStartPosition.CenterParent;
                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    tempForm.Text = "Add New Address Added in Logic";
                    TagsUserControl userControl = new TagsUserControl(textBox1.Text.ToString(), Datatype, 0);
                    tempForm.Height = userControl.Height + 25;
                    tempForm.Width = userControl.Width;
                    tempForm.Controls.Add(userControl);
                    var frmTemp = this.ParentForm as frmMain;
                    DialogResult result = tempForm.ShowDialog(frmTemp);
                    if (result == DialogResult.OK)
                    {
                        textBox1.Text = userControl.TagText;
                        CloseForm(textBox1.Text);
                    }
                }
                else
                {
                    bool isValidTag = CheckTagDataType(input);
                    if (isValidTag)
                    {
                        CloseForm(input);
                    }
                    else
                    {
                        errorProvider.SetError(textBox1, "Not valid Tag for " + ActualDataType + " Data Type");
                        return;
                    }
                }
                //calling tagUserControl form for adding new tag
            }
            else if (OperandType == "Numeric Operand" && !containsAlphabetic && input != "")
            {
                string inputError = string.Empty;
                //if (currentHSBName.StartsWith("HSI"))
                //{
                    inputError = ValidationForInputs(currentHSBName, CurrentInputTextName, input);
                //}
                if (inputError == null || inputError == "")
                {
                    CloseForm(textBox1.Text);
                }
                else
                {
                    errorProvider.SetError(textBox1, inputError);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Not Valid", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
        private string ValidationForInputs(string HSIOName, string CurrentinputtextName, string CurrentValue)
        {
            string error = string.Empty;
            bool containsAlphabetic = Regex.IsMatch(CurrentValue, "[a-zA-Z]");
            if (HSIOName.StartsWith("HSI") && CurrentinputtextName.StartsWith("Compare") && !containsAlphabetic)
            {
                HSIOFunctionBlock hsioBlockCompare = XMPS.Instance.LoadedProject.HsioBlock
                                                .Where(hsio => hsio.HSIOFunctionBlockName == HSIOName).SelectMany(hsio => hsio.HSIOBlocks)
                                                .Where(h => h.Text == "Compare ").SingleOrDefault();
                HSIOFunctionBlock hsioBlockCompareLow = XMPS.Instance.LoadedProject.HsioBlock
                                               .Where(hsio => hsio.HSIOFunctionBlockName == HSIOName).SelectMany(hsio => hsio.HSIOBlocks)
                                               .Where(h => h.Text == "Compare Low").SingleOrDefault();
                HSIOFunctionBlock hsioBlockCompareHigh = XMPS.Instance.LoadedProject.HsioBlock
                                        .Where(hsio => hsio.HSIOFunctionBlockName == HSIOName).SelectMany(hsio => hsio.HSIOBlocks)
                                        .Where(h => h.Text == "Compare High").SingleOrDefault();

                switch (CurrentinputtextName)
                {
                    case "Compare ":
                        if (hsioBlockCompareHigh != null && hsioBlockCompareHigh.Value != "???" && hsioBlockCompareLow != null && hsioBlockCompareLow.Value != "???")
                        {
                            if (long.Parse(hsioBlockCompareHigh.Value) - long.Parse(hsioBlockCompareLow.Value) >= 2)
                            {
                                if (hsioBlockCompareLow != null && hsioBlockCompareLow.Value != "???")
                                {
                                    containsAlphabetic = Regex.IsMatch(hsioBlockCompareLow.Value, "[a-zA-Z]");
                                    if (!containsAlphabetic && hsioBlockCompareLow.Value != "???")
                                    {
                                        if (long.Parse(CurrentValue) < long.Parse(hsioBlockCompareLow.Value))
                                        {
                                            return "Compare Value is always Greater than Compare Low value";
                                        }
                                    }
                                }
                                if (hsioBlockCompareHigh != null && hsioBlockCompareHigh.Value != "???")
                                {
                                    containsAlphabetic = Regex.IsMatch(hsioBlockCompareHigh.Value, "[a-zA-Z]");
                                    if (!containsAlphabetic && hsioBlockCompareHigh.Value != "???")
                                    {
                                        if (long.Parse(CurrentValue) > long.Parse(hsioBlockCompareHigh.Value))
                                        {
                                            return "Compare Value is always Smaller than Compare High value";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                return "Please Check values of Compare High/Compare Low";
                            }
                        }
                        else
                        {

                        }
                        break;
                    case "Compare High":
                        if (hsioBlockCompare != null && hsioBlockCompare.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockCompare.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && long.Parse(CurrentValue) < long.Parse(hsioBlockCompare.Value))
                            {
                                return "Compare High input is always greater than compare";

                            }
                        }
                        break;
                    case "Compare Low":
                        if (!containsAlphabetic && hsioBlockCompare != null && hsioBlockCompare.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockCompare.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && long.Parse(CurrentValue) > long.Parse(hsioBlockCompare.Value))
                            {
                                return "Compare Low input is always smaller than compare";
                            }
                        }
                        break;
                }

            }
            if (HSIOName.StartsWith("HSO") && (CurrentinputtextName.Contains("Pulse")) && !containsAlphabetic)
            {
                //start Pulse=Accel.Time(ms)
                HSIOFunctionBlock hsioBlockStartPulse = XMPS.Instance.LoadedProject.HsioBlock
                                                .Where(hsio => hsio.HSIOFunctionBlockName == HSIOName).SelectMany(hsio => hsio.HSIOBlocks)
                                                .Where(h => h.Text == "Accel Pulses").SingleOrDefault();
                //stop Pulse=Decel.Time(ms)
                HSIOFunctionBlock hsioBlockStopPulse = XMPS.Instance.LoadedProject.HsioBlock
                                               .Where(hsio => hsio.HSIOFunctionBlockName == HSIOName).SelectMany(hsio => hsio.HSIOBlocks)
                                               .Where(h => h.Text == "Decel Pulses").SingleOrDefault();
                HSIOFunctionBlock hsioBlockSlowPulse = XMPS.Instance.LoadedProject.HsioBlock
                                        .Where(hsio => hsio.HSIOFunctionBlockName == HSIOName).SelectMany(hsio => hsio.HSIOBlocks)
                                        .Where(h => h.Text == "Slow Pulse").SingleOrDefault();
                HSIOFunctionBlock hsioBlockTotalPulse = XMPS.Instance.LoadedProject.HsioBlock
                                        .Where(hsio => hsio.HSIOFunctionBlockName == HSIOName).SelectMany(hsio => hsio.HSIOBlocks)
                                        .Where(h => h.Text == "Total Pulses").SingleOrDefault();

                long totalPulsesCount = 0;
                containsAlphabetic = true;
                switch (CurrentinputtextName)
                {
                    case "Accel Pulses":
                        containsAlphabetic = Regex.IsMatch(CurrentValue, "[a-zA-Z]");
                        if (!containsAlphabetic && CurrentValue != "???" && CurrentValue != "")
                        {
                            totalPulsesCount += long.Parse(CurrentValue);
                        }

                        if (hsioBlockStopPulse != null && hsioBlockStopPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockStopPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockStopPulse != null && hsioBlockStopPulse.Value != "???")
                            {
                                totalPulsesCount += (long.Parse(hsioBlockStopPulse.Value));
                            }
                        }
                        if (hsioBlockSlowPulse != null && hsioBlockSlowPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockSlowPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockSlowPulse != null && hsioBlockSlowPulse.Value != "???")
                            {
                                totalPulsesCount += (long.Parse(hsioBlockSlowPulse.Value));
                            }
                        }
                        if (hsioBlockTotalPulse != null && hsioBlockTotalPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockTotalPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockTotalPulse != null && hsioBlockTotalPulse.Value != "???")
                            {
                                if (long.Parse(hsioBlockTotalPulse.Value) < totalPulsesCount)
                                    return "Sum of Accel Pulses, Decel Pulses, slow pulse should always be less than or equal to total pulses";
                            }
                        }
                        break;
                    case "Decel Pulses":
                        containsAlphabetic = Regex.IsMatch(CurrentValue, "[a-zA-Z]");
                        if (!containsAlphabetic && CurrentValue != "???" && CurrentValue != "")
                        {
                            totalPulsesCount += long.Parse(CurrentValue);
                        }
                        if (hsioBlockStartPulse != null && hsioBlockStartPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockStartPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockStartPulse != null && hsioBlockStartPulse.Value != "???")
                            {
                                totalPulsesCount += (long.Parse(hsioBlockStartPulse.Value));
                            }
                        }
                        if (hsioBlockSlowPulse != null && hsioBlockSlowPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockSlowPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockSlowPulse != null && hsioBlockSlowPulse.Value != "???")
                            {
                                totalPulsesCount += (long.Parse(hsioBlockSlowPulse.Value));
                            }
                        }
                        if (hsioBlockTotalPulse != null && hsioBlockTotalPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockTotalPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockTotalPulse != null && hsioBlockTotalPulse.Value != "???")
                            {
                                if (long.Parse(hsioBlockTotalPulse.Value) < totalPulsesCount)
                                    return "Sum of Accel Pulses, Decel Pulses, slow pulse should always be less than or equal to total pulses";
                            }
                        }
                        break;
                    case "Slow Pulse":
                        containsAlphabetic = Regex.IsMatch(CurrentValue, "[a-zA-Z]");
                        if (!containsAlphabetic && CurrentValue != "???" && CurrentValue != "")
                        {
                            totalPulsesCount += long.Parse(CurrentValue);
                        }
                        if (hsioBlockStopPulse != null && hsioBlockStopPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockStopPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockStopPulse != null && hsioBlockStopPulse.Value != "???")
                            {
                                totalPulsesCount += (long.Parse(hsioBlockStopPulse.Value));
                            }
                        }
                        if (hsioBlockStartPulse != null && hsioBlockStartPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockStartPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockStartPulse != null && hsioBlockStartPulse.Value != "???")
                            {
                                totalPulsesCount += (long.Parse(hsioBlockStartPulse.Value));
                            }
                        }
                        if (hsioBlockTotalPulse != null && hsioBlockTotalPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockTotalPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockTotalPulse != null && hsioBlockTotalPulse.Value != "???")
                            {
                                if (long.Parse(hsioBlockTotalPulse.Value) < totalPulsesCount)
                                    return "Sum of Accel Pulses, Decel Pulses, slow pulse should always be less than or equal to total pulses";
                            }
                        }
                        break;
                    case "Total Pulses":
                        //if (CurrentValue != "???" && CurrentValue != "")
                        //{
                        //    totalPulsesCount += long.Parse(CurrentValue);
                        //}
                        if (hsioBlockStopPulse != null && hsioBlockStopPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockStopPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockStopPulse != null && hsioBlockStopPulse.Value != "???")
                            {
                                totalPulsesCount += (long.Parse(hsioBlockStopPulse.Value));
                            }
                        }
                        if (hsioBlockStartPulse != null && hsioBlockStartPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockStartPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockStartPulse != null && hsioBlockStartPulse.Value != "???")
                            {
                                totalPulsesCount += (long.Parse(hsioBlockStartPulse.Value));
                            }
                        }
                        if (hsioBlockSlowPulse != null && hsioBlockSlowPulse.Value != "???")
                        {
                            containsAlphabetic = Regex.IsMatch(hsioBlockSlowPulse.Value, "[a-zA-Z]");
                            if (!containsAlphabetic && hsioBlockSlowPulse != null && hsioBlockSlowPulse.Value != "???")
                            {
                                totalPulsesCount += (long.Parse(hsioBlockSlowPulse.Value));
                            }
                        }
                        if (long.Parse(CurrentValue) < totalPulsesCount)
                            return "Total pulses should always be greater than or equal to sum of Decel Pulses, Accel Pulses, slow pulse";
                        break;
                }
            }
            return error;
        }
        private bool CheckTagDataType(string input)
        {
            XMIOConfig tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == input).FirstOrDefault();
            if (tag.Label == ActualDataType)
            {
                return true;
            }
            if (ActualDataType == "Bit")
            {
                XMIOConfig tag1 = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == input).FirstOrDefault();
                if (tag1.Label == "Bool")
                    return true;
                else
                    return false;
            }
            return false;
        }

        private void HSIOAutoComplete_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = currentTagName.Contains(":") ? XMPS.Instance.LoadedProject.Tags
                .Where(T => T.LogicalAddress == currentTagName).
                Select(T => T.Tag).FirstOrDefault().ToString() : currentTagName;
            AddListItem(Datatype, currentHSBName);
            textBox1.TextChanged += textBox1_TextChanged;
        }

        private void comboBoxOperandType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string OperandType = comboBoxOperandType.Text;
            if (OperandType == "Normal Operand")
            {
                this.GroupBoxTagList.Visible = true;
                this.LabelAddTag.Text = "Add Tag";
                this.labelTagName.Text = "Tag Name";
                this.textBox1.Text = currentTagName != "" ? currentTagName : "";
                AddListItem(Datatype, currentHSBName);
            }
            else if (OperandType == "Negation Operand")
            {
                this.GroupBoxTagList.Visible = true;
                this.LabelAddTag.Text = "Add Tag";
                this.labelTagName.Text = "Tag Name";
                this.textBox1.Text = currentTagName != "" ? currentTagName : "";
                AddListItem(Datatype, currentHSBName);
            }
            else
            {
                this.GroupBoxTagList.Visible = false;
                this.LabelAddTag.Text = "Add Value";
                this.labelTagName.Text = "Enter Value";
                this.textBox1.Text = "";
            }
        }

        private void btnclearTag_Click(object sender, EventArgs e)
        {
            CloseForm(null,true);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
