using iTextSharp.text;
using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using XMPS2000.Core;
using XMPS2000.Core.Base;

namespace XMPS2000.LadderLogic
{

    public partial class UserDefinedFunctionBlockControl : UserControl
    {
        public string FunctionBlockName = "";
        private int _listno = 0;
        private int edited = 0;
        private DataTable UDFB_table = new DataTable();
        private bool isEdit = false;
        private bool udfbEdit = false;
        private string currentEditedUdfb = string.Empty;
        /// For saving the data after editing udfb information <summary>
        HashSet<string> attributeToRemove = new HashSet<string>();
        HashSet<string> attributeToAdd = new HashSet<string>();

        Dictionary<string, string> variablePair = new Dictionary<string, string>();

        public UserDefinedFunctionBlockControl()
        {
            InitializeComponent();
            comboBoxDataType.DataSource = DataType.List.Where(T => T.ID < 6 || T.ID == 12).ToList();
            InitializeTable();
            List<string> Type = new List<string> { };
            Type.Add("Input");
            Type.Add("Output");
            cboType.DataSource = Type;
        }

        public UserDefinedFunctionBlockControl(string udfbname)
        {
            this.udfbEdit = true;
            InitializeComponent();
            comboBoxDataType.DataSource = DataType.List.Where(T => T.ID < 6 || T.ID == 12).ToList();
            InitializeTable();
            MakeColumnReadOnly(false);
            List<string> Type = new List<string> { };
            Type.Add("Input");
            Type.Add("Output");
            cboType.DataSource = Type;
            UDFBInfo udfbinfo = (UDFBInfo)XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName == udfbname).FirstOrDefault();
            textboxInput.Text = udfbinfo.Inputs.ToString();
            textboxOutput.Text = udfbinfo.Outputs.ToString();
            textUDFBName.Text = udfbinfo.UDFBName.ToString();
            foreach (UserDefinedFunctionBlock ud in udfbinfo.UDFBlocks)
            {
                DataRow udfb_row = UDFB_table.Rows.Add();
                udfb_row[0] = (_listno + 1).ToString();
                udfb_row[1] = ud.Type.ToString();
                udfb_row[2] = ud.DataType.ToString();
                udfb_row[3] = ud.Text.ToString();
                _listno++;
            }
            GvUDFB.DataSource = UDFB_table;
            this.textUDFBName.Enabled = false;
            currentEditedUdfb = udfbname;
            MakeColumnReadOnly(true);
        }

        private void MakeColumnReadOnly(bool value)
        {
            UDFB_table.Columns["SrNo."].ReadOnly = value;
            UDFB_table.Columns["Type"].ReadOnly = value;
            UDFB_table.Columns["DataType"].ReadOnly = value;
            UDFB_table.Columns["Text"].ReadOnly = value;
        }
        private void InitializeTable()
        {
            _listno = 0;
            UDFB_table.Columns.Clear();
            UDFB_table.Columns.Add("SrNo.");
            UDFB_table.Columns.Add("Type");
            UDFB_table.Columns.Add("DataType");
            UDFB_table.Columns.Add("Text");
            MakeColumnReadOnly(true);
        }

        private bool AddUDFB_Validated()
        {
            if (!string.IsNullOrEmpty(textboxInput.Text))
            {
                if (Convert.ToInt64(textboxInput.Text) != UDFB_table.AsEnumerable().Count(I => I.Field<string>("Type") == "Input"))
                {
                    MessageBox.Show("Check ! number of inputs added and required are not maching", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(textboxOutput.Text))
            {
                if (Convert.ToInt64(textboxOutput.Text) != UDFB_table.AsEnumerable().Count(I => I.Field<string>("Type") == "Output"))
                {
                    MessageBox.Show("Check ! number of outputs added and required are not maching", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (textUDFBName.Text.ToString().Length < 1)
            {
                MessageBox.Show("Blank UDFB name is not allowed", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnAddFB_Click(object sender, EventArgs e)
        {
            if (!AddUDFB_Validated())
                return;
            int input, output;
            if (!int.TryParse(textboxInput.Text, out input) || !int.TryParse(textboxOutput.Text, out output) || input == 0 || output == 0)
            {
                MessageBox.Show("UDFB with blank or zero inputs or outputs not allowed", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FunctionBlockName = textUDFBName.Text.ToString();
            UDFBInfo uDFBInfo = new UDFBInfo();
            XMPS.Instance.LoadedProject.UDFBInfo.RemoveAll(u => u.UDFBName == FunctionBlockName);
            uDFBInfo.UDFBName = FunctionBlockName;
            uDFBInfo.Inputs = Convert.ToInt32(textboxInput.Text);
            uDFBInfo.Outputs = Convert.ToInt32(textboxOutput.Text);
            List<UserDefinedFunctionBlock> udfbinfo = new List<UserDefinedFunctionBlock> { };
            foreach (DataRow dr in UDFB_table.Rows)
            {
                UserDefinedFunctionBlock udfbios = new UserDefinedFunctionBlock();
                udfbios.Type = dr[1].ToString();
                udfbios.DataType = dr[2].ToString();
                udfbios.Text = dr[3].ToString();
                udfbios.Name = FunctionBlockName;
                udfbinfo.Add(udfbios);
            }
            UpdateOnFunctionBlockConfiguration();
            uDFBInfo.AddUDFB(udfbinfo, FunctionBlockName);
            XMPS.Instance.LoadedProject.UDFBInfo.Add(uDFBInfo);
            if (variablePair.Count > 0)
            {
                foreach (string key in variablePair.Keys)
                {
                    XMProValidator.ChangeUDFBVariableInUDFBLogicBlock(FunctionBlockName, key, variablePair[key]);
                }
            }
            variablePair.Clear();
            XMPS.Instance.MarkProjectModified(true);
            this.ParentForm.Close();
            this.ParentForm.DialogResult = DialogResult.OK;
        }

        private void UpdateOnFunctionBlockConfiguration()
        {
            List<LadderElement> ladderElements = XMProValidator.GettigUDFBFunctionBlockRungs(textUDFBName.Text);
            if(ladderElements != null && ladderElements.Count > 0)
            {
                foreach(LadderElement ladderElement in ladderElements)
                {
                    foreach (string attributeName in attributeToRemove)
                    {
                        ladderElement.Attributes.RemoveAll(t => t.Name.Equals(attributeName));
                    }
                }
                //Rearrange the attributes with updated input output number.
                foreach (LadderElement element in ladderElements)
                {
                    if (attributeToRemove.Any(t => t.StartsWith("input")))
                    {
                        var remainingAttributes = element.Attributes
                            .Where(t => t.Name.StartsWith("input"))
                            .OrderBy(t => int.Parse(t.Name.Substring("input".Length)))
                            .ToList();
                        // Rename attributes sequentially
                        for (int i = 0; i < remainingAttributes.Count; i++)
                        {
                            remainingAttributes[i].Name = $"input{i + 1}";
                        }
                    }
                    if (attributeToRemove.Any(t => t.StartsWith("output")))
                    {
                        var remainingAttributes = element.Attributes
                            .Where(t => t.Name.StartsWith("output"))
                            .OrderBy(t => int.Parse(t.Name.Substring("output".Length)))
                            .ToList();
                        // Rename attributes sequentially
                        for (int i = 0; i < remainingAttributes.Count; i++)
                        {
                            remainingAttributes[i].Name = $"output{i + 1}";
                        }
                    }
                }
                //Adding new attribute if added by user in configuration
                foreach (LadderElement element in ladderElements)
                {
                    foreach (string variableType in attributeToAdd)
                    {
                        string attributeType = variableType.Equals("input") ? "input" : "output";
                        int attributeCount = element.Attributes.Count(t => t.Name.StartsWith(attributeType));
                         element.Attributes[$"{attributeType}{attributeCount + 1}"] = "-";
                    }
                }
            }
        }

        private bool Validation()
        {
            if (!string.IsNullOrEmpty(textboxInput.Text))
            {
                if (Convert.ToInt32(textboxInput.Text) == 0 || (cboType.Text == "Input" && Convert.ToInt32(textboxInput.Text) == UDFB_table.AsEnumerable().Count(I => I.Field<string>("Type") == "Input")))
                {
                    MessageBox.Show("Check ! number of inputs added and required are not maching", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(textboxOutput.Text))
            {
                if (Convert.ToInt32(textboxOutput.Text) == 0 || (cboType.Text == "Output" && Convert.ToInt32(textboxOutput.Text) == UDFB_table.AsEnumerable().Count(I => I.Field<string>("Type") == "Output")))
                {
                    MessageBox.Show("Check ! number of outputs added and required are not maching", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!isEdit) if (!Validation()) return;
            if (textText.Text == "")
            {
                MessageBox.Show("Empty Variable name is not accepted", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textText.Text == "IN")
            {
                MessageBox.Show($"{textText.Text} Variable name is not accepted", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!string.IsNullOrEmpty(textText.Text) && textText.Text.Equals("NULL"))
            {
                textUDFBName.Text = "";
                MessageBox.Show("NULL as a UDFB Block Name not allow please select alternate", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string pattern = @"^\d+(\.\d+)?$";
            bool isNumeric = Regex.IsMatch(textText.Text.ToString(), pattern);
            if(isNumeric)
            {
                MessageBox.Show($"Only numeric values not allow as a variable name", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textText.Text.ToString().Length < 3)
            {
                MessageBox.Show($"Variable name is short please add more than 3 character.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (XMPS.Instance.instructionsList.Any(t => t.Text.ToLower().Equals(textText.Text.ToLower())))
            {
                MessageBox.Show($"Please use an alternate variable name instead of '{textText.Text}', as this name is already " +
                                 $"used for an instruction name, which may cause ambiguity.",
                                 "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textText.Text = "";
                return;
            }
            if(textText.Text.Equals(textUDFBName.Text) || XMPS.Instance.LoadedProject.UDFBInfo.Any(t => t.UDFBName.Equals(textText.Text)))
            {
                MessageBox.Show($"Variable name is already use as UDFB name so please try with another variable name", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textboxInput.Text) || string.IsNullOrEmpty(textboxOutput.Text))
            {
                MessageBox.Show("Check number of Inputs and Outputs", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //checking in current udfb is variable name already is used or not.
            int matchingRowIndex = UDFB_table.AsEnumerable().Select((row, index) => new { Row = row, Index = index })
                                  .FirstOrDefault(x => x.Row.Field<string>(3) == textText.Text)?.Index + 1 ?? -1;

            if ((UDFB_table.AsEnumerable().Any(row => row.Field<string>(3) == textText.Text.ToString()) && !isEdit) || (isEdit && matchingRowIndex > 0 && edited != matchingRowIndex))
            {
                MessageBox.Show("Variable name already used", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //checking if variable name is used in any XMIOCONfig Tag
            var checkInTag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(textText.Text.ToString()));
            if (checkInTag != null)
            {
                MessageBox.Show($"Variable name already used as Tag Name for {checkInTag.LogicalAddress}", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow udfb_row = UDFB_table.NewRow();
            int rowCount = UDFB_table.Rows.Count;
            udfb_row[0] = edited == 0 ? (rowCount + 1).ToString() : edited.ToString();
            udfb_row[1] = cboType.Text.ToString();
            udfb_row[2] = comboBoxDataType.Text.ToString();
            udfb_row[3] = textText.Text.ToString();
            if (isEdit && UDFB_table.Rows.Count >= edited)
            {
                if(!textText.Text.ToLower().Equals(UDFB_table.Rows[edited - 1].ItemArray[3].ToString().ToLower()) && udfbEdit)
                {
                    string oldName = UDFB_table.Rows[edited - 1].ItemArray[3].ToString();
                    string newName = textText.Text;

                    string originalKey = variablePair.FirstOrDefault(kvp => kvp.Value == oldName).Key;
                    if (!string.IsNullOrEmpty(originalKey))
                    {
                        variablePair[originalKey] = newName;
                    }
                    else if (variablePair.ContainsKey(oldName))
                    {
                        variablePair[oldName] = newName;
                    }
                    else
                    {
                        variablePair[oldName] = newName;
                    }
                }
                UDFB_table.Rows.RemoveAt(edited - 1);
                _listno--;
                isEdit = false;
                UDFB_table.Rows.InsertAt(udfb_row, edited - 1);
            }
            else
            {
                isEdit = false;
                UDFB_table.Rows.Add(udfb_row);
                AddAttributeForFunctionBlock(cboType.Text.ToString());
            }
            DataTable sortedtable = UDFB_table;
            UDFB_table = sortedtable;
            GvUDFB.DataSource = UDFB_table;
            edited = 0;
            textText.Text = "";
            _listno++;
        }
        private void AddAttributeForFunctionBlock(string variableType)
        {
            if (XMPS.Instance.LoadedProject.UDFBInfo.Any(t => t.UDFBName.Equals(textUDFBName.Text)))
            {
                string attributeType = variableType.Equals("Input") ? "input" : "output";
                attributeToAdd.Add($"{attributeType}");
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            int rowCount = UDFB_table.Rows.Count;
            List<DataGridViewRow> rowsForDelete = GvUDFB.Rows.Cast<DataGridViewRow>().Where(row => Convert.ToBoolean(row.Cells["Select"].Value) == true).ToList();
            if (rowsForDelete.Count == 0)
            {
                MessageBox.Show("Please Select The Row", "XMPS200", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (DataGridViewRow dr in rowsForDelete)
            {
                if (XMPS.Instance.LoadedProject.UDFBInfo.Any(t => t.UDFBName.Equals(textUDFBName.Text)))
                {
                    string drType = dr.Cells["Type"].Value?.ToString();
                    string drText = dr.Cells["Text"].Value?.ToString();
                    var filteredRowsList = UDFB_table.AsEnumerable()
                       .Where(row => row.Field<string>("Type") == drType)
                       .OrderBy(row => Convert.ToInt32(row["SrNo."]))
                       .ToList();

                    if (!filteredRowsList.Any())
                        continue;
                    DataTable filteredRows = filteredRowsList.CopyToDataTable();
                    int rowIndex = filteredRowsList.FindIndex(row => row["Text"].ToString() == drText);

                    if (rowIndex != -1)
                    {
                        string attributeType = drType.Equals("Input") ? "input" : "output";
                        attributeToRemove.Add($"{attributeType}{rowIndex + 1}");
                    }
                }
            }
            foreach (DataGridViewRow dr in rowsForDelete)
            {
                string oldName = UDFB_table.Rows[dr.Index].ItemArray[3].ToString();
                string newName = textText.Text;

                var existingEntry = variablePair.FirstOrDefault(kvp => kvp.Value == oldName);
                if (!string.IsNullOrEmpty(existingEntry.Key))
                {
                    variablePair.Remove(existingEntry.Key);
                }
                UDFB_table.Rows.RemoveAt(dr.Index);
            }
            rowCount = UDFB_table.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow dr = GvUDFB.Rows[i];
                GvUDFB.Rows[i].Cells["SrNo."].Value = i + 1;
            }
            GvUDFB.DataSource = UDFB_table;
        }

        private void GvUDFB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var selindex = GvUDFB.Rows[e.RowIndex].Cells[1].Value;
            if (string.IsNullOrEmpty(selindex.ToString()))
                return;
            edited = Convert.ToInt32(selindex);
            cboType.Text = GvUDFB.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBoxDataType.Text = GvUDFB.Rows[e.RowIndex].Cells[3].Value.ToString();
            textText.Text = GvUDFB.Rows[e.RowIndex].Cells[4].Value.ToString();
            isEdit = true;
        }
        private void textUDFBName_Leave(object sender, EventArgs e)
        {
            string functionBlockName = textUDFBName.Text.ToString();
            int udfbCout = XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName.ToLower() == functionBlockName.ToLower()).Count();
            if (udfbCout > 0 && !udfbEdit)
            {
                textUDFBName.Text = "";
                MessageBox.Show("UDFB Block Name Already Used", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(functionBlockName) && functionBlockName.Equals("NULL"))
            {
                textUDFBName.Text = "";
                MessageBox.Show("NULL as a UDFB Block Name not allow please select alternate", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (XMPS.Instance.LoadedProject.Blocks.Any(t => t.Name.Equals(functionBlockName)) && !udfbEdit)
            {
                textUDFBName.Text = "";
                MessageBox.Show("UDFB name already used in Logic blocks.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            if (functionBlockName.ToLower().Equals("logic"))
            {
                textUDFBName.Text = "";
                MessageBox.Show($"Please add valid name to UDFB instead of {functionBlockName}", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (XMPS.Instance.instructionsList.Any(t => t.Text.ToLower().Equals(functionBlockName.ToLower())))
            {
                textUDFBName.Text = "";
                MessageBox.Show("Function Block Name already in Main Instructions", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void textText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, digits, and underscore
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '_' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textUDFBName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, digits, and underscore
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '_' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length >= 20 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textboxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textboxOutput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void GvUDFB_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
