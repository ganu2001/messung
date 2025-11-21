using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Types;

namespace XMPS2000.LadderLogic
{
    public struct ForceValuesData
    {
        public string Tag;
        public string Address;
        public string Value;
        public bool isForced;

        public ForceValuesData(string _tag, string _address, string _value, bool _isforced) : this()
        {
            this.Tag = _tag;
            this.Address = _address;
            this.Value = _value;
            this.isForced = _isforced;
        }
    }

    public partial class ForceUserControl : UserControl
    {
        private Button Set_value;
        private Label label1;
        private TextBox textValue;
        private Label label3;
        private TextBox textLogicalAddress;
        private Label label2;
        private GroupBox groupBox2;
        private Button Force_values;
        private Button Unforce_values;
        private Button Unforce_all;
        private DataGridView GvForceEle;
        private GroupBox groupBox1;
        private ComboBox TagComboBox;

        public static List<ForceValuesData> setValueList = new List<ForceValuesData>();
        public static Dictionary<string, ForceValuesData> setValueDic = new Dictionary<string, ForceValuesData>();
        private DataGridViewCheckBoxColumn colSelect;
        private Button btnDeleteVal;
        private ErrorProvider errorProvider;
        private System.ComponentModel.IContainer components;
        private readonly PLCForceFunctionality forceFunctionality = PLCForceFunctionality.Instance;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteVal = new System.Windows.Forms.Button();
            this.TagComboBox = new System.Windows.Forms.ComboBox();
            this.textValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textLogicalAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Set_value = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Force_values = new System.Windows.Forms.Button();
            this.Unforce_values = new System.Windows.Forms.Button();
            this.Unforce_all = new System.Windows.Forms.Button();
            this.GvForceEle = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvForceEle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteVal);
            this.groupBox1.Controls.Add(this.TagComboBox);
            this.groupBox1.Controls.Add(this.textValue);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textLogicalAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Set_value);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(581, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Element";
            // 
            // btnDeleteVal
            // 
            this.btnDeleteVal.Location = new System.Drawing.Point(429, 60);
            this.btnDeleteVal.Name = "btnDeleteVal";
            this.btnDeleteVal.Size = new System.Drawing.Size(116, 33);
            this.btnDeleteVal.TabIndex = 8;
            this.btnDeleteVal.Text = "Delete";
            this.btnDeleteVal.UseVisualStyleBackColor = true;
            this.btnDeleteVal.Click += new System.EventHandler(this.btnDeleteVal_Click);
            // 
            // TagComboBox
            // 
            this.TagComboBox.FormattingEnabled = true;
            this.TagComboBox.Location = new System.Drawing.Point(76, 22);
            this.TagComboBox.Name = "TagComboBox";
            this.TagComboBox.Size = new System.Drawing.Size(347, 21);
            this.TagComboBox.TabIndex = 7;
            this.TagComboBox.SelectedIndexChanged += new System.EventHandler(this.TagComboBox_SelectedIndexChanged);
            // 
            // textValue
            // 
            this.textValue.Location = new System.Drawing.Point(76, 75);
            this.textValue.Name = "textValue";
            this.textValue.Size = new System.Drawing.Size(195, 20);
            this.textValue.TabIndex = 6;
            this.textValue.Validating += new System.ComponentModel.CancelEventHandler(this.textValue_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Value :";
            // 
            // textLogicalAddress
            // 
            this.textLogicalAddress.Location = new System.Drawing.Point(76, 49);
            this.textLogicalAddress.Name = "textLogicalAddress";
            this.textLogicalAddress.Size = new System.Drawing.Size(195, 20);
            this.textLogicalAddress.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Address :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tag :";
            // 
            // Set_value
            // 
            this.Set_value.Location = new System.Drawing.Point(307, 60);
            this.Set_value.Name = "Set_value";
            this.Set_value.Size = new System.Drawing.Size(116, 33);
            this.Set_value.TabIndex = 0;
            this.Set_value.Text = "Set Value";
            this.Set_value.UseVisualStyleBackColor = true;
            this.Set_value.Click += new System.EventHandler(this.Set_value_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Force_values);
            this.groupBox2.Controls.Add(this.Unforce_values);
            this.groupBox2.Controls.Add(this.Unforce_all);
            this.groupBox2.Controls.Add(this.GvForceEle);
            this.groupBox2.Location = new System.Drawing.Point(3, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(581, 284);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Force Element list";
            // 
            // Force_values
            // 
            this.Force_values.Location = new System.Drawing.Point(9, 223);
            this.Force_values.Name = "Force_values";
            this.Force_values.Size = new System.Drawing.Size(116, 33);
            this.Force_values.TabIndex = 9;
            this.Force_values.Text = "Force values";
            this.Force_values.UseVisualStyleBackColor = true;
            this.Force_values.Click += new System.EventHandler(this.Force_values_Click);
            // 
            // Unforce_values
            // 
            this.Unforce_values.Location = new System.Drawing.Point(155, 223);
            this.Unforce_values.Name = "Unforce_values";
            this.Unforce_values.Size = new System.Drawing.Size(116, 33);
            this.Unforce_values.TabIndex = 8;
            this.Unforce_values.Text = "Unforce Values";
            this.Unforce_values.UseVisualStyleBackColor = true;
            this.Unforce_values.Click += new System.EventHandler(this.Unforce_values_Click);
            // 
            // Unforce_all
            // 
            this.Unforce_all.Location = new System.Drawing.Point(307, 223);
            this.Unforce_all.Name = "Unforce_all";
            this.Unforce_all.Size = new System.Drawing.Size(116, 33);
            this.Unforce_all.TabIndex = 7;
            this.Unforce_all.Text = "Unforce All";
            this.Unforce_all.UseVisualStyleBackColor = true;
            this.Unforce_all.Click += new System.EventHandler(this.Unforce_all_Click);
            // 
            // GvForceEle
            // 
            this.GvForceEle.AllowUserToDeleteRows = false;
            this.GvForceEle.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GvForceEle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvForceEle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect});
            this.GvForceEle.Location = new System.Drawing.Point(6, 19);
            this.GvForceEle.Name = "GvForceEle";
            this.GvForceEle.RowHeadersWidth = 51;
            this.GvForceEle.RowTemplate.Height = 24;
            this.GvForceEle.Size = new System.Drawing.Size(568, 198);
            this.GvForceEle.TabIndex = 0;
            this.GvForceEle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvForceEle_CellClick);
            // 
            // Select
            // 
            this.colSelect.HeaderText = "Select";
            this.colSelect.Name = "Select";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ForceUserControl
            // 
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ForceUserControl";
            this.Size = new System.Drawing.Size(590, 414);
            this.Load += new System.EventHandler(this.ForceUserControl_Load);
            this.Leave += new System.EventHandler(this.ForceUserControl_Leave);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvForceEle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        public ForceUserControl(LadderElement element)
        {
            InitializeComponent();
            OnlineMonitoringHelper.HoldOnlineMonitor = true;
            Populate_dataGridView();
            PopulateSelectedTags(element);
        }
        //<>
        //For The HSIO function block Enable Force Functionality
        //
        public ForceUserControl(HSIO hSIO)
        {
            InitializeComponent();
            OnlineMonitoringHelper.HoldOnlineMonitor = true;
            Populate_dataGridView();
            PopulateSelectedHSIOBlockTags(hSIO.HSIOBlocks);
        }

        private void PopulateSelectedHSIOBlockTags(List<HSIOFunctionBlock> hSIOBlocks)
        {
            List<string> tags = new List<string>();
            foreach (HSIOFunctionBlock hSIO in hSIOBlocks)
            {
                string input = hSIO.Value;
                bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");
                if (input != "???" && containsAlphabetic)
                {
                    if (input.Contains(":"))
                    {
                        string tagName = XMProValidator.GetTheTagnameFromAddress(input.Replace("~", ""));
                        tags.Add(tagName);
                    }
                    else
                        tags.Add(hSIO.Value.Replace("~", ""));
                }
            }
            TagComboBox.DataSource = tags;
        }

        private void Populate_dataGridView()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = setValueDic.Select(x => new { x.Value.Tag, x.Value.Address, x.Value.Value }).ToList();
            //bs.DataSource = new 
            GvForceEle.DataSource = bs;

            GvForceEle.AutoResizeColumns();
            GvForceEle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            GvForceEle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect= false;
            GvForceEle.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            GvForceEle.AllowUserToAddRows = false;
            GvForceEle.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        private void PopulateSelectedTags(LadderElement element)
        {
            List<string> tags = new List<string>();
            if (element.customDrawing.GetType() == typeof(LadderDrawing.FunctionBlock))
            {
                //Checking the selected Element is Pack or Unpack and adding All the 15 inputs and outputs into the force tag list
                if (element.Attributes["function_name"].ToString() == "Pack")
                {
                    foreach (LadderDrawing.Attribute attribute in element.Attributes)
                        if (attribute.Name.Contains("input"))
                        {
                            if (XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == attribute.Value.ToString().Replace("~","")).Count() > 0)
                            {
                                AddAllPackAndUnpack(attribute.Value.ToString(), ref tags);
                            }
                        }
                        else if (attribute.Name.Contains("output1"))
                        {
                            tags.Add(XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == attribute.Value.ToString()).Select(L => L.Tag).First().ToString());
                        }
                }
                else if (element.Attributes["function_name"].ToString() == "UnPack")
                {
                    foreach (LadderDrawing.Attribute attribute in element.Attributes)
                        if (attribute.Name.Contains("output"))
                        {
                            if (XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == attribute.Value.ToString().Replace("~", "")).Count() > 0)
                            {
                                AddAllPackAndUnpack(attribute.Value.ToString(), ref tags);
                            }
                        }
                        else if (attribute.Name.Contains("input1"))
                        {
                            tags.Add(XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == attribute.Value.ToString()).Select(L => L.Tag).First().ToString());
                        }
                }
                else
                {
                    foreach (LadderDrawing.Attribute attribute in element.Attributes)
                        if (attribute.Name.Contains("input") || attribute.Name.Contains("output"))
                            if (XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == attribute.Value.ToString()).Count() > 0)
                                tags.Add(XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == attribute.Value.ToString()).Select(L => L.Tag).First().ToString());
                            else if (attribute.Value.ToString().Contains(":"))
                                tags.Add(attribute.Value.ToString());
                }
            }
            else
            {
                tags.Add(element.Attributes.First().Value.ToString());
            }
            TagComboBox.DataSource = tags;
        }

        private void AddAllPackAndUnpack(string value, ref List<string> tags)
        {
            string firstAdd = value.Replace("~","");
            string[] parts = firstAdd.Split(':');
            int lastAddPart = int.Parse(parts[1]);
            int lastTagAdd = int.Parse(parts[1]) + 15;
            string endAdd = $"{parts[0]}:{lastTagAdd:000}";
            for (int i = int.Parse(parts[1]); i <= lastTagAdd; i++)
            {
                tags.Add(XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == $"{parts[0]}:{i.ToString("D3")}").Select(L => L.Tag).First().ToString());
            }
        }

        private void ForceUserControl_Load(object sender, System.EventArgs e)
        {
        }

        private void Set_value_Click(object sender, System.EventArgs e)
        {
            if (textValue.Text != "")
            {
                CheckforRealValue(textLogicalAddress.Text, textValue.Text);
                setValueDic[TagComboBox.Text] = new ForceValuesData(TagComboBox.Text, textLogicalAddress.Text, textValue.Text, false);
                GvForceEle.DataSource = setValueDic.Select(x => new { x.Value.Tag, x.Value.Address, x.Value.Value }).ToList();
                ClearFields();
            }
        }

        private void CheckforRealValue(string logicalAddress, string valueEntered)
        {
            if (logicalAddress.StartsWith("P") && !valueEntered.Contains("."))
                textValue.Text = valueEntered + ".00";
        }

        private void ClearFields()
        {
            TagComboBox.Text = "";
            textLogicalAddress.Text = "";
            textValue.Text = "";
        }

        private void Unforce_values_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //{
            //    if (setValueDic[row.Cells[0].Value.ToString()].isForced)
            //    {

            //    }
            //}

            byte[] dataFrame = new byte[250];
            int frmindex = 0;
            int datalength = 0;
            Int16 inputStr;
            string memoryval = "";
            string CRC = "0";
            int getcrc = 0;
            string datatype = "";
            int totlength = 0;
            OnlineMonitoringHelper.HoldOnlineMonitor = true;
            PLCCommunications pLCCommunications = new PLCCommunications();
            if (pLCCommunications.GetIPAddress() != "Error")
                PLCForceFunctionality.Tftpaddress = pLCCommunications.Tftpaddress.ToString();
            else
            {
                string errmsg = XMPS2000.CommonFunctions.GetEasyConnection(XMPS.Instance._connectedIPAddress);
                MessageBox.Show(errmsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (DataGridViewRow row in GvForceEle.Rows)
            {
                if (row.Cells["Select"].EditedFormattedValue.ToString() == "True")
                {
                    string data = row.Cells["Tag"].Value.ToString();
                    XMPS.Instance.Forcedvalues.Remove(data);
                    datatype = XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == setValueDic[data].Address.ToString() && L.Type == IOType.DataType).Select(T => T.Label).FirstOrDefault();
                    setValueDic[data] = new ForceValuesData(setValueDic[data].Tag, setValueDic[data].Address, setValueDic[data].Value, false);
                    if (setValueDic[data].Address.Contains(".") || datatype == "Bool" || datatype == "Byte")
                    {
                        datalength = 1;
                    }
                    else if (datatype == "Real" || datatype == "Double Word")  // if (Int16.TryParse(setValueDic[data].Value, out short val) && (datatype == null || datatype == "Word"))
                        datalength = 4;
                    else
                        datalength = 2;
                    dataFrame[frmindex] = Convert.ToByte(datalength.ToString("X"));
                    CRC = Convert.ToInt32(getcrc).ToString("X");
                    getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(datalength.ToString(), 16);
                    frmindex++;

                    memoryval = XMPS.Instance.GetHexAddressForOnlineMonoitoring(setValueDic[data].Address);
                    inputStr = Int16.Parse(memoryval, System.Globalization.NumberStyles.HexNumber);
                    byte[] frame = BitConverter.GetBytes(inputStr);
                    for (int cnt = 0; cnt < frame.Length; cnt++)
                    {
                        dataFrame[frmindex] = frame[cnt];
                        frmindex++;
                        CRC = Convert.ToInt32(getcrc).ToString("X");
                        getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(frame[cnt].ToString("X"), 16);
                    }

                    if (datalength == 1)
                    {
                        dataFrame[frmindex] = Convert.ToByte(0);
                        frmindex++;
                        CRC = Convert.ToInt32(getcrc).ToString("X");
                        getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(0);
                    }
                    else if (datalength == 2)
                    {
                        byte[] valframe = BitConverter.GetBytes(Convert.ToInt16(0));
                        for (int cnt = 0; cnt < valframe.Length; cnt++)
                        {
                            dataFrame[frmindex] = valframe[cnt];
                            frmindex++;
                            CRC = Convert.ToInt32(getcrc).ToString("X");
                            getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(0);
                        }
                    }
                    else if (datalength == 4)
                    {
                        byte[] valframe = BitConverter.GetBytes(Convert.ToInt32(0));
                        for (int cnt = 0; cnt < valframe.Length; cnt++)
                        {
                            dataFrame[frmindex] = valframe[cnt];
                            frmindex++;
                            CRC = Convert.ToInt32(getcrc).ToString("X");
                            getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(valframe[cnt]);
                        }
                    }
                }
            }
            if (frmindex != 0)
            {
                totlength = frmindex;
                CRC = Convert.ToInt32(getcrc).ToString("X");
                int conctc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32("97", 16);
                CRC = Convert.ToInt32(conctc).ToString("X");
                inputStr = Int16.Parse(CRC, System.Globalization.NumberStyles.HexNumber);
                byte[] crcframe = BitConverter.GetBytes(inputStr);
                dataFrame[frmindex] = crcframe[0];
                frmindex++;
            }
            MessageBox.Show(forceFunctionality.SendUnforceFrame(dataFrame.Take(frmindex).ToArray(), totlength), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Unforce_all_Click(object sender, EventArgs e)
        {
            MessageBox.Show(forceFunctionality.SendUnforceAllFrame(), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TagComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textLogicalAddress.Text = XMProValidator.GetTheAddressFromTag(TagComboBox.Text.ToString());

            if (setValueDic.ContainsKey(TagComboBox.Text))
                textValue.Text = setValueDic[TagComboBox.Text].Value;
            else
                textValue.Text = "";
        }

        private void Force_values_Click(object sender, EventArgs e)
        {
            byte[] dataFrame = new byte[250];
            int frmindex = 0;
            Int16 inputStr;
            string memoryval = "";
            string CRC = "0";
            int getcrc = 0;
            int datalength = 0;
            int totlength = 0;
            OnlineMonitoringHelper.HoldOnlineMonitor = true;
            string datatype = "";
            PLCCommunications pLCCommunications = new PLCCommunications();
            if (pLCCommunications.GetIPAddress() != "Error")
                PLCForceFunctionality.Tftpaddress = pLCCommunications.Tftpaddress.ToString();
            else
            {
                string errmsg = XMPS2000.CommonFunctions.GetEasyConnection(XMPS.Instance._connectedIPAddress);
                MessageBox.Show(errmsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (DataGridViewRow row in GvForceEle.Rows)
            {
                if (row.Cells["Select"].EditedFormattedValue.ToString() == "True")
                {
                    string data = row.Cells["Tag"].Value.ToString();
                    XMPS.Instance.Forcedvalues.Add(data);
                    datatype = XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == setValueDic[data].Address.ToString() && L.Type == IOType.DataType).Select(T => T.Label).FirstOrDefault();
                    setValueDic[data] = new ForceValuesData(setValueDic[data].Tag, setValueDic[data].Address, setValueDic[data].Value, true);
                    if (setValueDic[data].Address.Contains(".") || setValueDic[data].Address.StartsWith("F2") || datatype == "Bool" || datatype == "Byte")
                    {
                        datalength = 1;
                    }
                    else if (datatype == "Real" || datatype == "Double Word" || datatype == "DINT" || datatype == "UDINT")  //if (Int16.TryParse(setValueDic[data].Value, out short val) && (datatype == null || datatype == "Word"))
                        datalength = 4;
                    else
                        datalength = 2;

                    dataFrame[frmindex] = Convert.ToByte(datalength.ToString("X"));
                    CRC = Convert.ToInt32(getcrc).ToString("X");
                    getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(datalength.ToString(), 16);
                    frmindex++;
                    memoryval = XMPS.Instance.GetHexAddressForOnlineMonoitoring(setValueDic[data].Address);
                    inputStr = Int16.Parse(memoryval, System.Globalization.NumberStyles.HexNumber);
                    byte[] frame = BitConverter.GetBytes(inputStr);
                    for (int cnt = 0; cnt < frame.Length; cnt++)
                    {
                        dataFrame[frmindex] = frame[cnt];
                        frmindex++;
                        CRC = Convert.ToInt32(getcrc).ToString("X");
                        getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(frame[cnt].ToString("X"), 16);
                    }

                    if (datalength == 1)
                    {
                        dataFrame[frmindex] = Convert.ToByte(setValueDic[data].Value);
                        frmindex++;
                        CRC = Convert.ToInt32(getcrc).ToString("X");
                        getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(setValueDic[data].Value);
                    }
                    else if (datalength == 2)
                    {
                        byte[] valframe;
                        if (setValueDic[data].Address.ToString().StartsWith("W") && datatype == "Int")
                            valframe = BitConverter.GetBytes(Convert.ToInt16(setValueDic[data].Value));
                        else
                            valframe = BitConverter.GetBytes(Convert.ToUInt16(setValueDic[data].Value));

                        for (int cnt = 0; cnt < valframe.Length; cnt++)
                        {
                            dataFrame[frmindex] = valframe[cnt];
                            frmindex++;
                            CRC = Convert.ToInt32(getcrc).ToString("X");
                            getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(valframe[cnt]);
                        }
                    }
                    else if (datalength == 4)
                    {
                        byte[] valframe;
                        if (datatype == "Real")
                            if (setValueDic[data].Value.Contains("."))
                                valframe = BitConverter.GetBytes(Convert.ToSingle(setValueDic[data].Value));
                            else
                                valframe = BitConverter.GetBytes(Convert.ToInt32(setValueDic[data].Value));
                        else if (datatype == "DINT")
                            valframe = BitConverter.GetBytes(Convert.ToInt32(setValueDic[data].Value));
                        else
                            valframe = BitConverter.GetBytes(Convert.ToUInt32(setValueDic[data].Value));

                        for (int cnt = 0; cnt < valframe.Length; cnt++)
                        {
                            dataFrame[frmindex] = valframe[cnt];
                            frmindex++;
                            CRC = Convert.ToInt32(getcrc).ToString("X");
                            getcrc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32(valframe[cnt]);
                        }
                    }

                }
            }
            if (frmindex != 0)
            {
                totlength = frmindex;
                CRC = Convert.ToInt32(getcrc).ToString("X");
                int conctc = Convert.ToInt32(CRC, 16) ^ Convert.ToInt32("97", 16);
                CRC = Convert.ToInt32(conctc).ToString("X");
                inputStr = Int16.Parse(CRC, System.Globalization.NumberStyles.HexNumber);
                byte[] crcframe = BitConverter.GetBytes(inputStr);
                dataFrame[frmindex] = crcframe[0];
                frmindex++;

                //for (int cnt = 0; cnt < crcframe.Length; cnt++)
                //{
                //    dataFrame[frmindex] = crcframe[cnt];
                //    frmindex++;
                //}
            }
            MessageBox.Show(forceFunctionality.SendForceFrame(dataFrame.Take(frmindex).ToArray(), totlength), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnDeleteVal_Click(object sender, EventArgs e)
        {
            OnlineMonitoringHelper.HoldOnlineMonitor = true;
            foreach (DataGridViewRow row in GvForceEle.Rows)
            {
                if (row.Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    string data = row.Cells["Tag"].Value.ToString();
                    setValueDic.Remove(data);
                }
            }
            GvForceEle.DataSource = setValueDic.Select(x => new { x.Value.Tag, x.Value.Address, x.Value.Value }).ToList();

        }

        private void GvForceEle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = GvForceEle.SelectedRows[0];
            TagComboBox.Text = row.Cells["Tag"].FormattedValue.ToString();
            textLogicalAddress.Text = row.Cells["Address"].FormattedValue.ToString();
            textValue.Text = row.Cells["Value"].FormattedValue.ToString();
        }

        private void ForceUserControl_Leave(object sender, EventArgs e)
        {
            OnlineMonitoringHelper.HoldOnlineMonitor = false;
        }

        private void textValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool validationSuccessful = true;
            string error = string.Empty;
            validationSuccessful = ValidateNumericOperand(textValue, out error);
            e.Cancel = !validationSuccessful;
            errorProvider.SetError(textValue, validationSuccessful ? null : error);
        }

        private bool ValidateNumericOperand(Control control, out string error)
        {
            string number = control.Text;
            string datatype = XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == textLogicalAddress.Text.ToString() && L.Type == IOType.DataType).Select(T => T.Label).FirstOrDefault();
            if (datatype == null)
                datatype = textLogicalAddress.Text.ToString().Contains(".") ? "Bool" : "Word";
            if (number != "")
            {
                switch (datatype)
                {
                    case "Bool":
                        if (!number.Equals("1") && !number.Equals("0"))
                        {
                            error = "Invalid input value. Value does not match for Boolean data type";
                            return false;
                        }
                        break;
                    case "Byte":
                        if (number.StartsWith("-") || !byte.TryParse(number, out _))
                        {
                            error = "Invalid input value. Value does not match for Byte data type";
                            return false;
                        }
                        break;
                    case "Word":
                        if (number.StartsWith("-") || !ushort.TryParse(number, out _))
                        {
                            error = "Invalid input value. Value does not match for Word data type";
                            return false;
                        }
                        break;
                    case "Double Word":
                        if (number.StartsWith("-") || !uint.TryParse(number, out _))
                        {
                            error = "Invalid input value. Value does not match for Double Word data type";
                            return false;
                        }
                        break;
                    case "Int":
                        if (!int.TryParse(number, out _))
                        {
                            error = "Invalid input value. Value does not match for Integer data type";
                            return false;
                        }
                        break;
                    case "Real":
                        Double result;
                        if (Double.TryParse(number, out result))
                        {
                            if (Convert.ToDouble(number) < -2147483648 || Convert.ToDouble(number) > 2147483647)
                            {
                                error = "Invalid input value. Value does not match for Real data type";
                                return false;
                            }
                        }
                        break;
                    case "DINT":
                        long resultDINT;
                        if (long.TryParse(number, out resultDINT))
                        {
                            if (!(resultDINT >= -2147483648 && resultDINT <= 2147483647))
                            {
                                error = "Invalid input value. Value does not match for DINT data type";
                                return false;
                            }
                        }
                        break;
                    case "UDINT":
                        uint resultUdint;
                        if (uint.TryParse(number, out resultUdint))
                        {
                            if (!(resultUdint >= 0 && resultUdint <= 4294967295))
                            {
                                error = "Invalid input value. Value does not match for UDINT data type";
                                return false;
                            }
                        }
                        else
                        {
                            error = "Invalid input value. Value does not match for Real data type or is not Numeric";
                            return false;
                        }
                        break;
                }
            }
            error = string.Empty;
            return true;
        }

    }
}
