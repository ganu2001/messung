using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.LadderLogic;

namespace XMPS2000
{
    public partial class ParallelWindow : Form
    {
        public XMPS xm = XMPS.Instance;
        private readonly PLCForceFunctionality forceFunctionality = PLCForceFunctionality.Instance;
        public int _dgvIndex = 0;
        public static string _setactualvalue;
        public List<XMIOConfig> _updatedTagList = new List<XMIOConfig>();
        //Created List to Add multiple addresses to parallel watch window.
        public List<XMIOConfig> _multipleTagList = new List<XMIOConfig>();
        public ParallelWindow()
        {
            InitializeComponent();
            this.DatatypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
            {
                cmbWatch.DataSource = xm.LoadedProject.Tags.Where(T => !(T.Tag.EndsWith("OR") ||T.Tag.EndsWith("OL"))).Select(t => t.Tag).ToList();
            }
            else
            {
                cmbWatch.DataSource = xm.LoadedProject.Tags.Select(T => T.Tag).ToList();
            }
            foreach (DataGridViewColumn column in WatchDGV.Columns)
            {
                column.ReadOnly = true;
            }
            WatchDGV.Columns["PreparedValue"].ReadOnly = false;
            var Tag = xm.LoadedProject.Tags.ToList();
            this.DatatypeComboBox.Items.AddRange(xm.LoadedProject.Tags.Where(t => !t.Label.StartsWith("AI") && !t.Label.StartsWith("AO") && !t.Label.StartsWith("UI") && !t.Label.StartsWith("UO")).Select(t => t.LogicalAddress.Contains(".") ? "Bool" : t.Label).Distinct().ToArray());
            WatchDGV.AllowUserToAddRows = false;
            AutoResizeComboBoxDropDownWidth(cmbWatch);
        }


        private void CreateCellReadOnly()
        {
            foreach (DataGridViewRow row in WatchDGV.Rows)
            {
                // Skip the "new row" if AllowUserToAddRows is true
                if (row.IsNewRow)
                    continue;

                string tagValue = row.Cells["Tag"].Value?.ToString();

                if (!string.IsNullOrEmpty(tagValue) && tagValue.EndsWith("R"))
                {
                    row.Cells["PreparedValue"].ReadOnly = true;
                    // row.Cells["PreparedValue"].Style.BackColor = Color.LightGray;
                }
                else
                {
                    row.Cells["PreparedValue"].ReadOnly = false;
                    //row.Cells["PreparedValue"].Style.BackColor = Color.White;
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string EnteredText = this.cmbWatch.Text;

            var AddedElement = xm.LoadedProject.Tags
                                  .Where(T => T.Tag.Equals(EnteredText, StringComparison.OrdinalIgnoreCase))
                                  .ToList();

            if (AddedElement == null || AddedElement.Count == 0)
            {
                MessageBox.Show("Invalid tag name. Please enter a valid tag.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string addressStartLetter = AddedElement[0].LogicalAddress.Substring(0, 1);
            var matchingTags = WatchDGV.Rows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells["Address"].Value != null &&
                              row.Cells["Address"].Value.ToString().StartsWith(addressStartLetter))
                .Select(row => row.Cells["Tag"].Value?.ToString())
                .ToList();

            if (!CanAddTags(AddedElement, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (AddedElement.Count > 0)
                WatchWindowValues(AddedElement);
            AddTagToWatchWindow(AddedElement.FirstOrDefault());

            CreateCellReadOnly();
        }

        private bool CanAddTags(List<XMPS2000.Core.Base.XMIOConfig> tagsToAdd, out string errorMessage)
        {
            errorMessage = string.Empty;
            string addressStartLetter = tagsToAdd[0].LogicalAddress.Substring(0, 1);
            var matchingTags = WatchDGV.Rows
               .Cast<DataGridViewRow>()
               .Where(row => row.Cells["Address"].Value != null && row.Cells["Address"].Value.ToString().StartsWith(addressStartLetter))
               .Select(row => row.Cells["Tag"].Value?.ToString())
               .ToList();

            if ((addressStartLetter == "I" || addressStartLetter == "Q") && matchingTags.Count() >= 20)
            {
                errorMessage = $"Only 20 tags starting with {addressStartLetter} are allowed.";
                return false;
            }
            else if (addressStartLetter != "I" && addressStartLetter != "Q")
            {
                var otherTagsCount = WatchDGV.Rows
                    .Cast<DataGridViewRow>()
                    .Count(row =>
                    {
                        var addr = row.Cells["Address"].Value?.ToString();
                        return addr != null && !addr.StartsWith("I") && !addr.StartsWith("Q");
                    });

                if (otherTagsCount >= 40)
                {
                    errorMessage = "Only 40 tags of other types are allowed.";
                    return false;
                }
            }
            return true;
        }

        public void WatchWindowValues(List<XMIOConfig> tags)
        {
            if (xm.PlcStatus == "LogIn")
            {
                var Tag = tags;
                // 1. activeAddress">List of addresses for online  ---> Pass Logical Address from index to Ending List Element
                List<string> _systemtagsAddress = new List<string>();  //Logical Address
                List<string> _ListTagName = new List<string>();       // Tagname
                List<AddressDataTypes> _Type = new List<AddressDataTypes>();
                OnlineMonitoringHelper omh = OnlineMonitoringHelper.Instance;
                Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddressInfo = new Dictionary<string, Tuple<string, AddressDataTypes>>();

                for (int i = 0; i < Tag.Count; i++)
                {

                    _systemtagsAddress.Add(Tag[i].LogicalAddress.ToString());
                    _ListTagName.Add(Tag[i].Tag.ToString());
                    _Type.Add(omh.GetAddressTypeOf(Tag[i].Label));
                }
                for (int j = 0; j < _systemtagsAddress.Count; j++)
                {
                    CurBlockAddressInfo.Add(_ListTagName[j], Tuple.Create(_systemtagsAddress[j], _Type[j]));
                }
                // 2. name="addressInfoDic">Dictionary with current Logic Block tags ----> (Tagname,Address,Type)

                // 3. AddressValues ----------> tagname ,""
                Dictionary<string, string> _AddressValues = new Dictionary<string, string>();
                _AddressValues.Clear();
                foreach (string AddressValue in _ListTagName)
                { _AddressValues.Add(AddressValue, ""); }

                OnlineMonitoring onlineMonitoring = OnlineMonitoring.GetInstance();
                onlineMonitoring.GetValues(_ListTagName, ref CurBlockAddressInfo, ref _AddressValues, out string Result);

                //Create a Static Varible To Store Values Actual Value Current Tag
                _setactualvalue = _AddressValues.Values.First();
            }
        }
        private void WatchDGV_MouseClick_1(object sender, MouseEventArgs e)
        {
            var hitTestInfo = WatchDGV.HitTest(e.X, e.Y);
            if (e.Button == MouseButtons.Right && hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex == 5)
            {
                cntxDeleteRow.Visible = false;
                signedDecimalToolStripMenuItem.Visible = true;
                unsignedDecimalToolStripMenuItem.Visible = true;
                hexadecimalToolStripMenuItem.Visible = true;
                aSCIIToolStripMenuItem.Visible = true;
                bCDToolStripMenuItem.Visible = true;
                binaryToolStripMenuItem.Visible = true;
                contextMenuStrip1.Show(WatchDGV, new Point(e.X, e.Y));
            }
            if (e.Button == MouseButtons.Right && hitTestInfo.ColumnIndex != 5)
            {
                signedDecimalToolStripMenuItem.Visible = false;
                unsignedDecimalToolStripMenuItem.Visible = false;
                hexadecimalToolStripMenuItem.Visible = false;
                aSCIIToolStripMenuItem.Visible = false;
                bCDToolStripMenuItem.Visible = false;
                binaryToolStripMenuItem.Visible = false;
                cntxDeleteRow.Visible = true;
                contextMenuStrip1.Show(WatchDGV, new Point(e.X, e.Y));
            }
        }
        private void deleteRowToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DeleteSelectedRows();
        }
        private void ParallelWindow_Paint(object sender, PaintEventArgs e)
        {
            //Pen pen = new Pen(Color.Black, 1);
            //e.Graphics.DrawRectangle(pen, new Rectangle(5, 5, 470, 250));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (WatchDGV.Rows.Count > 0)
            {
                for (int i = 0; i < WatchDGV.Rows.Count; i++)
                {
                    WatchDGV.Rows[i].Cells["srNo"].Value = i + 1;
                }
            }
            OnlineParallelTag(_updatedTagList);
            WatchDGV.Columns["ActualValue"].ReadOnly = true;
            // WatchDGV.Refresh();
        }
        private void OnlineParallelTag(List<XMIOConfig> updatedTagList)
        {
            int index = 0;
            if (xm.PlcStatus == "LogIn")
            {
                var Tag = updatedTagList;
                // 1. activeAddress">List of addresses for online  ---> Pass Logical Address from index to Ending List Element
                List<string> _systemtagsAddress = new List<string>();  //Logical Address
                List<string> _ListTagName = new List<string>();       // Tagname
                List<AddressDataTypes> _Type = new List<AddressDataTypes>();
                OnlineMonitoringHelper omh = OnlineMonitoringHelper.Instance;
                Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddressInfo = new Dictionary<string, Tuple<string, AddressDataTypes>>();

                for (int i = 0; i < Tag.Count; i++)
                {
                    _systemtagsAddress.Add(Tag[i].LogicalAddress.ToString());
                    _ListTagName.Add(Tag[i].Tag.ToString());
                    if (!XMPS2000.LadderLogic.DataType.ContainsText(Tag[i].Label))
                        _Type.Add(Tag[i].LogicalAddress.ToString().Contains(".") ? omh.GetAddressTypeOf("Bool") : omh.GetAddressTypeOf(xm.LoadedProject.CPUDatatype));
                    else
                        _Type.Add(omh.GetAddressTypeOf(Tag[i].Label));
                }
                for (int j = 0; j < _systemtagsAddress.Count; j++)
                {
                    CurBlockAddressInfo.Add(_ListTagName[j], Tuple.Create(_systemtagsAddress[j], _Type[j]));
                }
                // 2. name="addressInfoDic">Dictionary with current Logic Block tags ----> (Tagname,Address,Type)


                // 3. AddressValues ----------> tagname ,""
                Dictionary<string, string> _AddressValues = new Dictionary<string, string>();
                _AddressValues.Clear();
                foreach (string AddressValue in _ListTagName)
                { _AddressValues.Add(AddressValue, ""); }

                OnlineMonitoring onlineMonitoring = OnlineMonitoring.GetInstance();
                onlineMonitoring.GetValues(_ListTagName, ref CurBlockAddressInfo, ref _AddressValues, out string Result);

                //Create a Static Varible To Store Values Actual Value Current Tag
                // _setactualvalue = _AddressValues.Values.First();
                foreach (string AddressValue in _AddressValues.Values)
                {
                    string conversionType = WatchDGV.Rows[index].Cells[9].Value?.ToString();
                    if (!string.IsNullOrEmpty(conversionType))
                    {
                        string valueToConvert = string.Empty;
                        if (!AddressValue.Contains("E"))
                            valueToConvert = AddressValue;
                        else
                            valueToConvert = "1";
                        string convertedValue = ConvertValue(valueToConvert, conversionType);
                        WatchDGV.Rows[index].Cells["ActualValue"].Value = convertedValue;
                    }
                    else
                    {
                        WatchDGV.Rows[index].Cells["ActualValue"].Value = AddressValue;
                        WatchDGV.Rows[index].Cells["HiddenActualValue"].Value = AddressValue;
                    }
                    index++;
                }
            }
        }
        void LoadData()
        {
            if (xm.Entries != null && xm.Entries.Count > 0)
            {
                foreach (var entry in xm.Entries)
                {
                    XMIOConfig config = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == entry.Address).FirstOrDefault();
                    if (config != null)
                    {
                        int rowIndex = WatchDGV.Rows.Add();
                        DataGridViewRow row = WatchDGV.Rows[rowIndex];
                        row.Cells["srNo"].Value = entry.SrNo;
                        row.Cells["Address"].Value = entry.Address;
                        row.Cells["Tag"].Value = config.Tag;
                        row.Cells["DataType"].Value = entry.DataType;
                        row.Cells["retentive"].Value = xm.LoadedProject.Tags.Any(t => t.Retentive && t.Tag == entry.Tag);
                        row.Cells["ActualValue"].Value = entry.ActualValue;
                        row.Cells["PreparedValue"].Value = entry.PreparedValue;
                        row.Cells["btnForce"].Value = entry.BtnForce;
                        row.Cells["UnForceValue"].Value = entry.UnForceValue;
                        row.Cells["HiddenActualValue"].Value = entry.HiddenActualValue;
                        _updatedTagList.AddRange(new[] { xm.LoadedProject.Tags.Where(t => t.LogicalAddress == entry.Address).FirstOrDefault() });
                    }
                }
            }
        }

        private void ParallelWindow_Load(object sender, EventArgs e)
        {
            LoadData();
            CreateCellReadOnly();
            timer1.Start();
        }
        private void DatatypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDataType = DatatypeComboBox.Text;
            this.cmbWatch.DataSource = null;
            if (selectedDataType == "Bool")
            {
                if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
                {
                    this.cmbWatch.DataSource = xm.LoadedProject.Tags
                      .Where(t =>
                          t.Label == "Bool"
                          || t.Type == Core.Types.IOType.DigitalInput
                          || t.Type == Core.Types.IOType.DigitalOutput
                          || (((t.Type == Core.Types.IOType.UniversalInput || t.Type == Core.Types.IOType.UniversalOutput || t.Type == Core.Types.IOType.AnalogInput || t.Type == Core.Types.IOType.AnalogOutput) && t.Mode == "Digital")
                              && !(t.Label.EndsWith("OR") || t.Label.EndsWith("OL")))
                      )
                      .Select(t => t.Tag)
                       .OrderBy(tag => tag)
                      .ToList();
                }
                else
                {
                    this.cmbWatch.DataSource = xm.LoadedProject.Tags
                     .Where(t =>
                         t.Label == "Bool"
                         || t.Type == Core.Types.IOType.DigitalInput
                         || t.Type == Core.Types.IOType.DigitalOutput
                         || ((t.Type == Core.Types.IOType.UniversalInput || t.Type == Core.Types.IOType.UniversalOutput || t.Type == Core.Types.IOType.AnalogInput || t.Type == Core.Types.IOType.AnalogOutput)
                             && (t.Label.EndsWith("OR") || t.Label.EndsWith("OL")))
                     )
                     .Select(t => t.Tag)
                      .OrderBy(tag => tag)
                     .ToList();
                }
            }
            else if (selectedDataType == "Word")
            {
                this.cmbWatch.DataSource = xm.LoadedProject.Tags.Where(t => (t.Label == "Word" && !t.LogicalAddress.Contains("."))
                || ((t.Type == Core.Types.IOType.AnalogInput || t.Type == Core.Types.IOType.AnalogOutput || t.Type == Core.Types.IOType.UniversalInput || t.Type == Core.Types.IOType.UniversalOutput) && t.Model.StartsWith("XM") && !t.Label.EndsWith("OR") && !t.Label.EndsWith("OL")))
             .Select(t => t.Tag).OrderBy(tag => tag).ToList();
            }
            else if (selectedDataType == "Real")
            {
                this.cmbWatch.DataSource = xm.LoadedProject.Tags
                .Where(T => T.Label == selectedDataType
                    || ((((T.Type == Core.Types.IOType.AnalogInput || T.Type == Core.Types.IOType.AnalogOutput || T.Type == Core.Types.IOType.UniversalInput || T.Type == Core.Types.IOType.UniversalOutput ) && T.Model.StartsWith("XBLD") &&  T.Mode != "Digital")) && (!T.Label.EndsWith("OR") && !T.Label.EndsWith("OL")) && !T.LogicalAddress.Contains(".")))
                .Select(t => t.Tag).OrderBy(tag => tag).ToList();
            }
            else
            {
                this.cmbWatch.DataSource = xm.LoadedProject.Tags.Where(t => t.Label == selectedDataType).Select(t => t.Tag).OrderBy(tag => tag).ToList();
            }
        }
        private void AddMultiplebtn_Click(object sender, EventArgs e)
        {
            using (DeviceMonitorInput deviceInput = new DeviceMonitorInput())
            {
                if (deviceInput.ShowDialog() == DialogResult.OK &&
                    !string.IsNullOrEmpty(deviceInput.startingAddress))
                {
                    string _StartingAddress = deviceInput.startingAddress;
                    string startingAddressPart = _StartingAddress.Split(':')[0];
                    int startingAddressIndex = Convert.ToInt32(_StartingAddress.Split(':')[1]);
                    int _length = deviceInput.length;
                    //extract the addresses upto provided length.
                    _multipleTagList = xm.LoadedProject.Tags
                        .Where(T => T.LogicalAddress.StartsWith(startingAddressPart))
                        .OrderBy(T => Convert.ToInt32(T.LogicalAddress.Split(':')[1]))
                        .SkipWhile(T => Convert.ToInt32(T.LogicalAddress.Split(':')[1]) < startingAddressIndex)
                        .Take(_length).ToList();

                    foreach (var tag in _multipleTagList)
                    {
                        if (!CanAddTags(new List<XMPS2000.Core.Base.XMIOConfig> { tag }, out string errorMessage))
                        {
                            MessageBox.Show(errorMessage, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        WatchWindowValues(new List<XMPS2000.Core.Base.XMIOConfig> { tag });
                        AddTagToWatchWindow(tag);
                    }
                    if (_multipleTagList.Count < _length)
                    {
                        MessageBox.Show("Only these addresses are present within the selected range", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        //Common method to add tags to parallel watch window grid.
        private void AddTagToWatchWindow(XMIOConfig tag)
        {
            // Check for duplicates in the grid
            bool isDuplicate = WatchDGV.Rows.Cast<DataGridViewRow>()
                .Any(row => row.Cells["Tag"].Value?.ToString() == tag.Tag);

            if (isDuplicate)
            {
                MessageBox.Show($"The Tag '{tag.Tag}' is already in the Watch Window.", "Duplicate Tag", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Skip adding if duplicate
            }
            _updatedTagList.AddRange(new[] { tag });
            // Add to the grid
            int newRowIndex = WatchDGV.Rows.Add();
            WatchDGV.Rows[newRowIndex].Cells["srNo"].Value = (newRowIndex + 1).ToString();
            WatchDGV.Rows[newRowIndex].Cells["Address"].Value = tag.LogicalAddress;
            WatchDGV.Rows[newRowIndex].Cells["Tag"].Value = tag.Tag;
            WatchDGV.Rows[newRowIndex].Cells["DataType"].Value = tag.LogicalAddress.Contains(".") ? "Bool" : tag.Label;
            if (!tag.LogicalAddress.Contains(".") && !new[] { "String", "Byte", "Word", "Bool", "Double Word", "Real", "DINT", "Int" }.Contains(tag.Label))

            {
                WatchDGV.Rows[newRowIndex].Cells["DataType"].Value = !tag.LogicalAddress.Contains(".") ? xm.LoadedProject.CPUDatatype : tag.Label;
            }
            WatchDGV.Rows[newRowIndex].Cells["retentive"].Value = tag.Retentive;
            WatchDGV.Rows[newRowIndex].Cells["ActualValue"].Value = _setactualvalue;
            WatchDGV.Rows[newRowIndex].Cells["PreparedValue"].Value = "";
            WatchDGV.Rows[newRowIndex].Cells["btnForce"].Value = "Force";
            WatchDGV.Rows[newRowIndex].Cells["UnForceValue"].Value = "UnForce";
            WatchDGV.Rows[newRowIndex].Cells["HiddenActualValue"].Value = _setactualvalue;
            CreateCellReadOnly();
            // Reset the actual value
            _setactualvalue = "";
        }
        private void ForceFunctionality()
        {
            try
            {
                string error;
                int rowIndex = WatchDGV.CurrentCell.RowIndex;
                var currentCellValue = WatchDGV.Rows[rowIndex].Cells[6].Value?.ToString();
                var dataType = WatchDGV.Rows[rowIndex].Cells["DataType"].Value.ToString();
                var Logical_Address = WatchDGV.Rows[rowIndex].Cells["Address"].Value.ToString();
                if (!XMPS2000.LadderLogic.DataType.ContainsText(dataType))
                    dataType = Logical_Address.Contains(".") ? "Bool" : xm.LoadedProject.CPUDatatype;
                bool result = XMProValidator.ValidateNumericInputOperand(currentCellValue, dataType, out error, Logical_Address);
                if (result == false)
                {
                    if (error != "")
                        MessageBox.Show(error, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show($"Invalid input value. Value does not match for {dataType} data type", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WatchDGV.Rows[rowIndex].Cells[6].Value = null;
                    return;
                }
                if (dataType == "Bool")
                {
                    bool value = currentCellValue == "1" ? true : false;
                    ForceBitValue forceBitValueInstance = new ForceBitValue(Logical_Address);
                    forceBitValueInstance.CommonForceFunctionality(value);
                }
                else
                {
                    ForceFunctionBlock forceFunctionBlock = new ForceFunctionBlock();
                    currentCellValue = CommonFunctions.IsRealValue(Logical_Address) && !Logical_Address.Contains(".") && !currentCellValue.Contains(".") ? currentCellValue.Trim() + ".00" : currentCellValue;

                    forceFunctionBlock.CommonFunctionToForceTag(Logical_Address, currentCellValue);
                    LadderWindow ladderWindow = System.Windows.Forms.Application.OpenForms.OfType<LadderWindow>().FirstOrDefault();
                    if (ladderWindow != null)
                    {
                        ladderWindow.refreshAfterForce();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void WatchDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || (e.ColumnIndex != 7 && e.ColumnIndex != 8))
                return;
            var cellValue = WatchDGV.Rows[e.RowIndex].Cells[6].Value?.ToString();
            if (e.RowIndex >= 0 && e.ColumnIndex == WatchDGV.Columns["btnForce"].Index && WatchDGV.Rows[e.RowIndex].Cells[6].Value != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                ForceFunctionality();
                WatchDGV.Rows[e.RowIndex].Cells["HiddenActualValue"].Value = cellValue;
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == WatchDGV.Columns["UnForceValue"].Index && WatchDGV.Rows[e.RowIndex].Cells[6].Value != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                UnForceFunctionality();
            }
        }

        private void UnForceFunctionality()
        {
            int rowIndex = WatchDGV.CurrentCell.RowIndex;
            var currentCellValue = WatchDGV.Rows[rowIndex].Cells[6].Value?.ToString();
            var dataType = WatchDGV.Rows[rowIndex].Cells["DataType"].Value.ToString();
            var Logical_Address = WatchDGV.Rows[rowIndex].Cells["Address"].Value.ToString();
            ForceBitValue unForecValue = new ForceBitValue(Logical_Address);
            unForecValue.CommonUnForceFunctionality();
        }

        private void signedDecimalToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ConvertActualValue("SignedDecimal");
        }
        private void unsignedDecimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertActualValue("UnsignedDecimal");
        }
        private void hexadecimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertActualValue("Hexadecimal");
        }
        private void aSCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertActualValue("ASCII");
        }
        private void bCDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertActualValue("BCD");
        }
        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertActualValue("Binary");
        }
        private void ConvertActualValue(string conversionType)
        {
            if (WatchDGV.CurrentCell != null)
            {
                var rowIndex = WatchDGV.CurrentCell.RowIndex;
                var actualValueCell = WatchDGV.Rows[rowIndex].Cells["HiddenActualValue"];
                var actualValue = actualValueCell.Value?.ToString();
                string convertedValue = actualValue;
                WatchDGV.Columns["ActualValue"].ReadOnly = false;
                if (!string.IsNullOrEmpty(actualValue))
                {
                    string valueToConvert = string.Empty;
                    if (!actualValue.Contains("E"))
                        valueToConvert = actualValue;
                    else
                        valueToConvert = "1";
                    convertedValue = ConvertValue(valueToConvert, conversionType);
                    WatchDGV.Rows[rowIndex].Cells[9].Value = conversionType;
                    WatchDGV.Rows[rowIndex].Cells[5].Value = convertedValue;
                    WatchDGV.Refresh();
                }
                else
                {
                    MessageBox.Show("Actual value is empty or invalid.");
                }
                conversionType = string.Empty;
            }
        }
        private string ConvertValue(string ValueToConvert, string conversionType)
        {
            switch (conversionType)
            {
                case "SignedDecimal":
                    if (double.TryParse(ValueToConvert, out double signedDecimal))
                    {
                        return signedDecimal.ToString();
                    }
                    break;
                case "UnsignedDecimal":
                    if (double.TryParse(ValueToConvert, out double unsignedDecimal))
                    {
                        return Math.Abs(unsignedDecimal).ToString();
                    }
                    break;
                case "Hexadecimal":
                    string hexResult = string.Empty;
                    if (double.TryParse(ValueToConvert, out double doubleValue))
                    {
                        if (doubleValue < 0)
                        {
                            hexResult = "-" + ConvertToHexadecimal(ValueToConvert);
                        }
                        else
                            hexResult = ConvertToHexadecimal(ValueToConvert);
                    }

                    return hexResult;
                case "ASCII":
                    string asciiResult = convertDecimalToAscii(ValueToConvert);
                    return asciiResult;
                case "BCD":
                    string BCDString = string.Empty;
                    if (double.TryParse(ValueToConvert, out double numericValue))
                    {
                        if (numericValue < 0)
                        {
                            BCDString = "-" + ConvertToBCD(Math.Abs(numericValue));
                        }
                        else
                            BCDString = ConvertToBCD(numericValue);
                    }
                    return BCDString;
                case "Binary":
                    string stringBinary = string.Empty;
                    if (double.TryParse(ValueToConvert, out double value))
                    {
                        if (value < 0)
                        {
                            stringBinary = "-" + ConvertToBinary(Math.Abs(value));
                        }
                        else
                            stringBinary = ConvertToBinary(value);
                    }
                    return stringBinary;
                default:
                    MessageBox.Show("Invalid conversion type.");
                    return string.Empty;
            }
            return string.Empty;
        }
        //convert Actual Value into binary format.
        private string ConvertToBinary(double value)
        {
            long integerPart = (long)Math.Floor(value);
            double fractionalPart = value - integerPart;
            string binaryIntegerPart = Convert.ToString(integerPart, 2).PadLeft(8, '0');
            string binaryFractionalFractionPart = string.Empty;
            if (fractionalPart > 0)
            {
                long fPart = long.Parse(fractionalPart.ToString("0.##").Split('.')[1]);
                binaryFractionalFractionPart = Convert.ToString(fPart, 2).PadLeft(8, '0');
            }
            return fractionalPart > 0 ? $"{binaryIntegerPart}{"."}{binaryFractionalFractionPart}" : binaryIntegerPart;
        }
        //Convert actual value into Hex Format.
        private string ConvertToHexadecimal(string valueToConvert)
        {
            if (valueToConvert.Contains(" "))
            {
                string[] parts = valueToConvert.Split(' ');
                StringBuilder hexResult = new StringBuilder();
                foreach (string part in parts)
                {
                    if (part.Contains("."))
                    {
                        string hexValue = ConvertFloatToHexadecimal(part);
                        hexResult.Append(hexValue + " ");
                    }
                    else if (int.TryParse(part, out int intValue))
                    {
                        hexResult.Append(intValue.ToString("X").PadLeft(2, '0') + " ");
                    }
                    else
                    {
                        MessageBox.Show($"Invalid part '{part}' in input.");
                        return string.Empty;
                    }
                }
                return hexResult.ToString().Trim();
            }
            else
            {
                if (valueToConvert.Contains("."))
                {
                    return ConvertFloatToHexadecimal(valueToConvert);
                }
                else if (int.TryParse(valueToConvert, out int singleIntValue))
                {
                    return singleIntValue.ToString("X");
                }
                else
                {
                    MessageBox.Show($"Invalid input '{valueToConvert}'.");
                    return string.Empty;
                }
            }
        }
        //Convert Actual value in Float to Hexadecimal.
        private string ConvertFloatToHexadecimal(string valueToConvert)
        {
            if (float.TryParse(valueToConvert, out float floatValue))
            {
                int wholeNumberPart = (int)floatValue;
                float fractionalPart = floatValue - wholeNumberPart;
                fractionalPart = Math.Abs(fractionalPart);
                long fracPart = long.Parse(fractionalPart.ToString("0.##").Split('.')[1]);
                string wholeNumberHex = wholeNumberPart < 0
                    ? Math.Abs(wholeNumberPart).ToString("X")
                    : wholeNumberPart.ToString("X");
                string FractionHexPart = fracPart.ToString("X");
                return fractionalPart > 0 ? wholeNumberHex + "." + FractionHexPart : wholeNumberHex;
            }
            return string.Empty;
        }
        //Conversion of Actual value into ASCII format
        private static string convertDecimalToAscii(string value)
        {
            string result = "";
            string valueString = value.ToString();
            if (valueString.StartsWith("-"))
            {
                value = valueString.TrimStart('-');
            }
            if (value.Contains("."))
            {
                if (float.TryParse(value, out float floatValue))
                {
                    int wholeNumberPart = (int)floatValue;
                    float fractionalPart = floatValue - wholeNumberPart;
                    fractionalPart = Math.Abs(fractionalPart);
                    fractionalPart = (float)Math.Round(fractionalPart, 2);
                    string wholeNumberAscii = ConvertIntegerToAscii(wholeNumberPart);
                    result += wholeNumberAscii;
                    string fractionalAscii = ConvertFractionToAscii(fractionalPart);
                    result += fractionalPart > 0 ? "." + fractionalAscii : "";
                }
            }
            else
            {
                result = ConvertIntegerToAscii(int.Parse(value));
            }
            return result;
        }
        //Convert fractional part of actual value into ASCII format
        private static string ConvertFractionToAscii(float fractionalPart)
        {
            string result = string.Empty;
            int fractionInt = (int)(fractionalPart * 100);

            string fractionString = fractionInt.ToString();
            result += ConvertIntegerToAscii(int.Parse(fractionString));
            return result;
        }
        //Convert Integer part of Actual value into ASCII format
        private static string ConvertIntegerToAscii(int wholeNumberPart)
        {
            string result = "";
            string numberString = wholeNumberPart.ToString();

            for (int i = 0; i < numberString.Length; i += 3)
            {
                string group = numberString.Substring(i, Math.Min(3, numberString.Length - i));
                int groupValue = int.Parse(group);

                if (groupValue >= 0 && groupValue <= 127)
                {
                    result += (char)groupValue;
                }
                else
                {
                    result += "?";
                }
            }
            return result;
        }
        /// <summary>
        /// Conversion of actual value into BCD format
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ConvertToBCD(double value)
        {
            string number = value.ToString().Replace(".", "");
            string[] parts = value.ToString().Split('.');
            string integerPart = string.Join(" ", parts[0].Select(c => Convert.ToString(int.Parse(c.ToString()), 2).PadLeft(4, '0')));
            string fractionalPart = parts.Length > 1 ? "." + string.Join(" ", parts[1].Select(c => Convert.ToString(int.Parse(c.ToString()), 2).PadLeft(4, '0'))) : "";
            return integerPart + fractionalPart;
        }
        private void WatchDGV_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                List<string> deletedTags = new List<string>();
                List<DataGridViewRow> selectedRowsSorted = WatchDGV.SelectedRows
                        .Cast<DataGridViewRow>()
                        .OrderBy(r => r.Index)
                        .ToList();
                selectedRowsSorted.Reverse();
                foreach (var gr in selectedRowsSorted)
                {
                    int _index = gr.Index;
                    string tagName = _updatedTagList[_index].Tag;
                    if (!string.IsNullOrEmpty(tagName))
                    {
                        deletedTags.Add(tagName);
                    }
                    _updatedTagList.RemoveAt(_index);
                }
                MessageBox.Show($"Deleted tags are: {string.Join(", ", deletedTags)}", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ParallelWindow_Resize(object sender, EventArgs e)
        {
            groupBox1.Width = this.Width - 35;
            groupBox1.Height = this.Height - 50;
            WatchDGV.Width = groupBox1.Width - 40;
            WatchDGV.Height = groupBox1.Height - 70;
        }

        private void ParallelWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            if (xm.Entries == null)
                xm.Entries = new List<WatchDogEntries>();
            List<WatchDogEntries> entries = xm.Entries;
            xm.Entries.Clear();
            foreach (DataGridViewRow row in WatchDGV.Rows)
            {
                if (!row.IsNewRow)
                {
                    xm.Entries.Add(new WatchDogEntries
                    {
                        SrNo = row.Cells["srNo"].Value?.ToString(),
                        Address = row.Cells["Address"].Value?.ToString(),
                        Tag = row.Cells["Tag"].Value?.ToString(),
                        DataType = row.Cells["DataType"].Value?.ToString(),
                        Retentive = row.Cells["retentive"].Value?.ToString(),
                        ActualValue = row.Cells["ActualValue"].Value?.ToString(),
                        PreparedValue = row.Cells["PreparedValue"].Value?.ToString(),
                        BtnForce = row.Cells["btnForce"].Value?.ToString(),
                        UnForceValue = row.Cells["UnForceValue"].Value?.ToString(),
                        HiddenActualValue = row.Cells["HiddenActualValue"].Value?.ToString()
                    });
                }
            }
        }
        private void btndelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedRows();
        }
        private void DeleteSelectedRows()
        {
            this.timer1.Stop();
            List<string> deletedTags = new List<string>();
            if (WatchDGV.SelectedRows.Count > 0)
            {
                var selectedRows = WatchDGV.SelectedRows.Cast<DataGridViewRow>()
                                      .Where(row => !row.IsNewRow)
                                      .OrderByDescending(row => row.Index)
                                      .ToList();

                foreach (var row in selectedRows)
                {
                    int rowIndex = row.Index;
                    var tagValue = row.Cells["Tag"].Value?.ToString();
                    if (!string.IsNullOrEmpty(tagValue))
                    {
                        deletedTags.Add(tagValue);
                    }
                    if (rowIndex >= 0 && rowIndex < _updatedTagList.Count)
                    {
                        _updatedTagList.RemoveAt(rowIndex);
                    }
                    WatchDGV.Rows.RemoveAt(rowIndex);
                }

                // Reassign srNo
                for (int i = 0; i < WatchDGV.Rows.Count; i++)
                {
                    if (!WatchDGV.Rows[i].IsNewRow)
                    {
                        WatchDGV.Rows[i].Cells["srNo"].Value = (i + 1).ToString();
                    }
                }
                MessageBox.Show($"Deleted tags are: {string.Join(", ", deletedTags)}", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.timer1.Start();
        }
        private void AutoResizeComboBoxDropDownWidth(ComboBox comboBox)
        {
            int maxWidth = 0;
            using (Graphics g = comboBox.CreateGraphics())
            {
                foreach (var item in comboBox.Items)
                {
                    // Measure the text width of each item
                    int itemWidth = (int)g.MeasureString(item.ToString(), comboBox.Font).Width;
                    if (itemWidth > maxWidth)
                    {
                        maxWidth = itemWidth;
                    }
                }
            }
            comboBox.DropDownWidth = maxWidth;
        }
    }
}
