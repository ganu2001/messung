using LadderDrawing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMPS2000.Configuration;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.LadderLogic;
using ComboBox = System.Windows.Forms.ComboBox;
using Control = System.Windows.Forms.Control;
using TextBox = System.Windows.Forms.TextBox;

namespace XMPS2000.LadderLogic
{
    public partial class FunctionBlockInputsAndOutputs : System.Windows.Forms.UserControl
    {
        XMPS xm;
        private List<Instruction> udfbInstrion = new List<Instruction> { };
        public List<Tuple<int, string>> Inputs = new List<Tuple<int, string>> { };
        public List<Tuple<int, string>> Outputs = new List<Tuple<int, string>> { };
        private Dictionary<string, Counter> Counters = new Dictionary<string, Counter>();
        public List<string> udfbInstructionNames = new List<string> { };
        public string _curRungTcName;
        private string TcName = null;
        public int Linenumber = 0;
        public string udfbname;
        public bool hasLadders = false;
        public bool edit;
        public string tc_instuction;
        public string sel_opcode;
        public bool isEnabled = false;
        public bool isRetentive = false;
        private ErrorProvider errorProviderFB = new ErrorProvider();
        private bool enableCellValueChanged = true;
        private HashSet<string> filledAddresses = new HashSet<string>();

        //Checking Pack and Unpack Fix input or output changed or not.
        private bool isUnPackInputEdit = false;
        private bool isUnPackOutputEdit = false;
        private bool isPackOutputEdit = false;
        private bool isPackInputEdit = false;
        public FunctionBlockInputsAndOutputs()
        {
            xm = XMPS.Instance;
            InitializeComponent();
            InitializeTimerCounters();
            AddUDFBInformation();
        }
        public FunctionBlockInputsAndOutputs(XMProForm xMPro)
        {
            xm = XMPS.Instance;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            InitializeComponent();
            InitializeTimerCounters();
            AddUDFBInformation();
            comboBoxDataType.DataSource = DataTypeDesilizer.List.Where(T => Convert.ToInt32(T.ID, 16) < 6 || Convert.ToInt32(T.ID, 16) > 11).ToList();
            comboBoxDataType.DisplayMember = "Text";
            comboBoxDataType.ValueMember = "ID";
            List<InstructionType> instructionList = GettingInstructioTypeData();
            InstructionType instructionType = new InstructionType();
            instructionType.Text = "UDFB";
            instructionType.ID = 900;
            instructionList.Add(instructionType);
            comboBoxInstructionType.DataSource = instructionList;
        }
        public void GetSupportedDataTypes(string instructionName)
        {
            string selectedInstructionName = comboBoxInstructionType.Text;
            InstructionTypeDeserializer instruction = XMPS.Instance.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionName));
            if (instruction != null)
            {
                var supportedDataTypes = instruction.SupportedDataTypes;
                if (!XMPS.Instance.PlcModel.StartsWith("XBLD") && comboBoxInstructionType.Text == "Arithmetic" && comboBoxInstruction.Text == "MOD")
                {
                    supportedDataTypes = supportedDataTypes.Where(t => t.Text != "Real").ToList();
                }
                this.comboBoxDataType.DataSource = supportedDataTypes;
                comboBoxDataType.DisplayMember = "Text";
                comboBoxDataType.ValueMember = "ID";
            }
        }

        private List<InstructionType> GettingInstructioTypeData()
        {
            List<InstructionType> instructionTypes = new List<InstructionType>();
            int ID = 1;
            foreach (TreeNode node in XMPS.Instance.instructionTreeNodes.Nodes)
            {
                InstructionType instructionType = new InstructionType();
                instructionType.Text = node.Text;
                instructionType.ID = ID;
                instructionTypes.Add(instructionType);
                ID++;
            }
            return instructionTypes;
        }
        private List<InstructionTypeDeserializer> GettingSubInstruction(InstructionType selectedInstructionType)
        {
            string selectedInstructionName = selectedInstructionType.Text;
            List<InstructionTypeDeserializer> instructionTypeDeserializers = new List<InstructionTypeDeserializer>();

            instructionTypeDeserializers.AddRange(xm.instructionsList.Where(t => t.InstructionType.Equals(selectedInstructionName)));
            return instructionTypeDeserializers;
        }
        public void SetValues(LadderElement ladderElement)
        {
            this.enableCellValueChanged = false;
            xm = XMPS.Instance;
            if (ladderElement.Attributes["caption"].Equals("FunctionBlock"))
            {
                this.checkBoxEnable.Checked = false;
            }
            string instructionText = ladderElement.Attributes["function_name"].ToString().Equals("ANY to Dword") ? "ANY to DWORD" : ladderElement.Attributes["function_name"].ToString();
            //Removing the PID instruction number from the function_name Attributes.
            instructionText = instructionText.StartsWith("MES_PID_") ? "MES_PID" : instructionText;
            InstructionTypeDeserializer instruction = xm.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionText));
            string instructionType = instruction.InstructionType;

            this.comboBoxDataType.DataSource = instruction.SupportedDataTypes;
            this.comboBoxDataType.DisplayMember = "Text";
            this.comboBoxDataType.ValueMember = "ID";
            if (instructionType.Contains("Timer") || instructionType.Contains("Counter"))
            {
                this.comboBoxDataType.DataSource = instruction.SupportedDataTypes;
            }
            this.comboBoxInstructionType.Text = instructionType;
            this.enableCellValueChanged = false;
            this.comboBoxInstruction.Text = instructionText;
            this.enableCellValueChanged = false;
            //add below check for the Double Word DataType after Copy Paste Scenario at the time of paste DataType spilt with (' ') so Double Word set to Double
            string selectedDataType = ladderElement.Attributes["DataType_Nm"].ToString().Equals("Double") ? "Double Word" : ladderElement.Attributes["DataType_Nm"].ToString();
            this.comboBoxDataType.Text = !selectedDataType.Equals("") ? selectedDataType : instruction.SupportedDataTypes.Select(T => T.Text).FirstOrDefault();
            TcName = ladderElement.Attributes["TCName"].ToString();
            this.enableCellValueChanged = false;
            AddInputandOutPut();
            var addresses = ladderElement.Attributes.Where(t => t.Name.StartsWith("in") && t.Value != null && (!t.Value.ToString().Contains('-') || (t.Value.ToString().Contains('-') && t.Value.ToString().Length > 1))).Select(t => t.Value).ToList();
            //Checking if any Instruction having less inputs than inputs present in rung(currnt LadderElement)
            if (instruction.InputsOutputs.Where(T => T.Type.Equals("Input")).Count() < addresses.Count)
            {
                int diff = addresses.Count - instruction.InputsOutputs.Where(T => T.Type.Equals("Input")).Count();
                if (diff > 0)
                {
                    addresses.RemoveRange(addresses.Count - diff, diff);
                }
            }
            addresses.RemoveAll(t => t.Equals(""));
            if (instructionType == "MQTT")
            {
                this.labelTopic.Visible = true;
                this.cmbTopic.Visible = true;
                FillMQTTTopicDetails();
            }
            int i = 0;
            foreach (string input in addresses)
            {
                if (instruction.InputsOutputs.Where(T => T.Type.Equals("Input")).Count() > 0)
                {
                    string dataType = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == input).Select(T => T.Label).FirstOrDefault();
                    this.enableCellValueChanged = false;
                    if (xm.CurrentScreen.Contains("UDFLadderForm"))
                    {
                        if (input.StartsWith("~")) datagridFunctionBlockIn.Rows[i].Cells[2].Value = "Negation Operand";
                        if (!input.Contains(":"))
                        {
                            if (double.TryParse(input, out _))
                            {
                                datagridFunctionBlockIn.Rows[i].Cells[2].Value = "Numeric Operand";
                                ((DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[3]).DataSource = null;
                            }
                            else if (input.Any(ch => Char.IsLetter(ch)))
                            {
                                datagridFunctionBlockIn.Rows[i].Cells[2].Value = "Normal Operand";
                            }
                        }
                        if (input.Contains(":") && !input.StartsWith("~")) datagridFunctionBlockIn.Rows[i].Cells[2].Value = "Normal Operand";

                    }
                    else
                    {
                        if (input.StartsWith("~")) datagridFunctionBlockIn.Rows[i].Cells[2].Value = "Negation Operand";
                        if (!input.Contains(":"))
                        {
                            if (double.TryParse(input, out _))
                            {
                                datagridFunctionBlockIn.Rows[i].Cells[2].Value = "Numeric Operand";
                                ((DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[3]).DataSource = null;
                            }
                            else if (input.Any(ch => Char.IsLetter(ch)))
                            {
                                datagridFunctionBlockIn.Rows[i].Cells[2].Value = "Normal Operand";
                            }
                        }
                        if (input.Contains(":") && !input.StartsWith("~")) datagridFunctionBlockIn.Rows[i].Cells[2].Value = "Normal Operand";
                    }
                    DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[3];
                    tags.Value = XMProValidator.GetTheTagnameFromAddress(input.Replace("~", ""));
                    datagridFunctionBlockIn.Rows[i].Cells[4].Value = input.Replace("~", "");
                    string prvDataType = datagridFunctionBlockIn.Rows[i].Cells[1].Value.ToString();
                    //datagridFunctionBlockIn.Rows[i].Cells[1].Value = (dataType != null && dataType != "UDINT" && (!input.StartsWith("Q") && !input.StartsWith("I") && !input.StartsWith("S"))) ? dataType : prvDataType;
                    i++;
                }
            }

            addresses = ladderElement.Attributes.Where(t => t.Name.StartsWith("out") && t.Value != null && !t.Value.ToString().Contains('-')).Select(t => t.Value).ToList();
            addresses.RemoveAll(t => t.Equals(""));
            //Checking if any Instruction having less inputs than inputs present in rung(currnt LadderElement)
            if (instruction.InputsOutputs.Where(T => T.Type.Equals("Output")).Count() < addresses.Count)
            {
                int diff = addresses.Count - instruction.InputsOutputs.Where(T => T.Type.Equals("Output")).Count();
                if (diff > 0)
                {
                    addresses.RemoveRange(addresses.Count - diff, diff);
                }
            }
            i = 0;
            foreach (string output in addresses)
            {
                this.enableCellValueChanged = false;
                string dataType = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == output).Select(T => T.Label).FirstOrDefault();
                XMIOConfig outputTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == output).FirstOrDefault();
                if (outputTag == null && !addresses.Contains("."))
                    outputTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith(output) && T.LogicalAddress.Contains('.')).FirstOrDefault();
                if (outputTag != null)
                {
                    if (outputTag.IoList == Core.Types.IOListType.OnBoardIO) datagridFunctionBlockOut.Rows[i].Cells[2].Value = "On-board";
                    if (outputTag.IoList == Core.Types.IOListType.RemoteIO) datagridFunctionBlockOut.Rows[i].Cells[2].Value = "Remote";
                    if (outputTag.IoList == Core.Types.IOListType.ExpansionIO) datagridFunctionBlockOut.Rows[i].Cells[2].Value = "Expansion";
                    if (outputTag.IoList != Core.Types.IOListType.OnBoardIO && outputTag.IoList != Core.Types.IOListType.RemoteIO && outputTag.IoList != Core.Types.IOListType.ExpansionIO) datagridFunctionBlockOut.Rows[i].Cells[2].Value = "Memory Address Variable";
                    DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[i].Cells[3];
                    tags.Value = XMProValidator.GetTheTagnameFromAddress(output);
                    DataGridViewCell cell = datagridFunctionBlockOut.Rows[i].Cells[4];
                    string prvDataType = datagridFunctionBlockOut.Rows[i].Cells[1].Value.ToString();
                    //datagridFunctionBlockOut.Rows[i].Cells[1].Value = (dataType != null && dataType != "UDINT" && (!output.StartsWith("Q") && !output.StartsWith("I"))) ? dataType : prvDataType;
                    cell.Value = output.Replace("~", "");

                    //binding tags to outputdropdown.
                    tags.DataSource = OutPutTypeChangedTags(datagridFunctionBlockOut.Rows[i].Cells[2].Value.ToString(), i);
                }
                else if (xm.CurrentScreen.Contains("UDFLadderForm"))
                {
                    bool iscurrentUDFBIO = xm.LoadedProject.UDFBInfo.Where(t => t.UDFBName == xm.CurrentScreen.Split('#')[1].Replace(" Logic", "").ToString())
                                            .Any(t => t.UDFBlocks.Where(d => d.Text.Equals(output)).Any());
                    if (outputTag == null && iscurrentUDFBIO)
                    {
                        datagridFunctionBlockOut.Rows[i].Cells[2].Value = "Memory Address Variable";
                        DataGridViewCell cell = datagridFunctionBlockOut.Rows[i].Cells[4];
                        cell.Value = output;
                        //binding tags to outputdropdown.
                        DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[i].Cells[3];
                        tags.DataSource = OutPutTypeChangedTags(datagridFunctionBlockOut.Rows[i].Cells[2].Value.ToString(), i);
                    }
                }
                //Check for "Q0:000"
                if (instructionType == "MQTT" && i == 1)
                {
                    SetTopicValue(output);
                    DataGridViewCell cell = datagridFunctionBlockOut.Rows[i].Cells[4];
                    cell.Value = output.Replace("~", "");
                    datagridFunctionBlockOut.Rows[i].Visible = false;

                }
                if (datagridFunctionBlockOut.Rows[i].Cells[1].Value.ToString().Equals("Word") && output.StartsWith("Q") && xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith(output)).Count() > 0)
                {
                    XMIOConfig onboardIO = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith(output)).FirstOrDefault();
                    DataGridViewCell cell = datagridFunctionBlockOut.Rows[i].Cells[4];
                    cell.Value = output.Replace("~", "");
                }
                i++;
            }
            //disabling Input1 and Output1 for Pack and Unpacked Instruction
            var opCodeInHex = ladderElement.Attributes["OpCode"].ToString();
            if (opCodeInHex == "0390" && edit)
            {
                comboBoxDataType.Text = "Bool";
                comboBoxInstructionType.Enabled = false;
            }
            if (opCodeInHex == "03A2" && edit)
            {
                comboBoxDataType.Text = "Word";
                comboBoxInstructionType.Enabled = false;
            }
            this.enableCellValueChanged = true;
        }

        private void SetTopicValue(string ID)
        {
            for (int i = 0; i < cmbTopic.Items.Count; i++)
            {
                TopicList topicList = (TopicList)cmbTopic.Items[i];
                if (topicList.ID.ToString().Equals(ID))
                {
                    cmbTopic.SelectedIndex = i;
                    break; // exit the loop once the item is found
                }
            }
        }
        private string GetInstructionType(XDocument xmlDoc, string instructionText)
        {
            XElement instruction = xmlDoc.Descendants("Instruction")
                                    .FirstOrDefault(e => e.Element("Text")?.Value == instructionText);

            if (instruction != null)
            {
                // Get the parent element's name
                return instruction.Parent?.Name.LocalName;
            }
            return null;
        }

        public FunctionBlockInputsAndOutputs(LadderElement ladderElement)
        {
            xm = XMPS.Instance;
            InitializeComponent();
            InitializeTimerCounters();
            AddUDFBInformation();
            comboBoxDataType.DataSource = DataType.List;
            comboBoxInstructionType.DataSource = InstructionType.List.Where(u => u.Text == "UDFB").ToList();
            comboBoxInstruction.Text = ladderElement.Attributes["function_name"].ToString();
            TcName = ladderElement.Attributes["TCName"].ToString();

            AddInputandOutPut();
            var addresses = ladderElement.Attributes.Where(t => t.Name.StartsWith("in") && t.Value != null && (!t.Value.ToString().Contains('-') || (t.Value.ToString().Contains('-') && t.Value.ToString().Length > 1))).Select(t => t.Value).ToList();
            int i = 0;
            foreach (string input in addresses)
            {
                datagridFunctionBlockIn.Rows[i].Cells["Tag_Address"].Value = input.Replace("~", "");
                if (input.StartsWith("~")) datagridFunctionBlockIn.Rows[i].Cells["Operand_Type"].Value = "Negation Operand";
                if (!input.Contains(":"))
                {
                    datagridFunctionBlockIn.Rows[i].Cells["Operand_Type"].Value = "Numeric Operand";
                    ((DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[3]).DataSource = null;

                }
                if (input.Contains(":") && !input.StartsWith("~")) datagridFunctionBlockIn.Rows[i].Cells["Operand_Type"].Value = "Normal Operand";

                DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells["Tag_For_Operand"];
                tags.Value = XMProValidator.GetTheTagnameFromAddress(input.Replace("~", ""));
                i++;
            }
            addresses = ladderElement.Attributes.Where(t => t.Name.StartsWith("out") && t.Value != null && !t.Value.ToString().Contains('-')).Select(t => t.Value).ToList();
            i = 0;
            foreach (string output in addresses)
            {
                XMIOConfig outputTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == output).FirstOrDefault();
                if (outputTag != null)
                {
                    if (outputTag.IoList == Core.Types.IOListType.OnBoardIO) datagridFunctionBlockOut.Rows[i].Cells[2].Value = "On-board";
                    if (outputTag.IoList == Core.Types.IOListType.RemoteIO) datagridFunctionBlockOut.Rows[i].Cells[2].Value = "Remote";
                    if (outputTag.IoList == Core.Types.IOListType.ExpansionIO) datagridFunctionBlockOut.Rows[i].Cells[2].Value = "Expansion";
                    if (outputTag.IoList != Core.Types.IOListType.OnBoardIO && outputTag.IoList != Core.Types.IOListType.RemoteIO && outputTag.IoList != Core.Types.IOListType.ExpansionIO) datagridFunctionBlockOut.Rows[i].Cells[2].Value = "Memory Address Variable";
                }
                datagridFunctionBlockOut.Rows[i].Cells["OPTagaddress"].Value = output;
                DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[i].Cells["outputTag"];
                tags.Value = XMProValidator.GetTheTagnameFromAddress(output);
                //binding tags to output dropdown.
                tags.DataSource = OutPutTypeChangedTags(datagridFunctionBlockOut.Rows[i].Cells[2].Value.ToString(), i);
                i++;
            }
        }
        /// <summary>
        /// Add complete UDFB details so that user can use it here
        /// </summary>
        private void AddUDFBInformation()
        {
            if (InstructionType.List.Where(u => u.Text == "UDFB").Count() == 0)
            {
                InstructionType instructionType = new InstructionType();
                instructionType.Text = "UDFB";
                instructionType.ID = 900;
                InstructionType.List.Add(instructionType);
            }
            foreach (UDFBInfo uDFBInfo in xm.LoadedProject.UDFBInfo)
            {
                List<UserDefinedFunctionBlock> uDFBDetails = uDFBInfo.UDFBlocks;

                Instruction instruction = new Instruction
                {
                    ID = 1200,
                    Text = uDFBInfo.UDFBName,
                    InstructionType = 900,
                };

                List<DataType> dataTypes = new List<DataType> { };
                foreach (string datatype in uDFBInfo.UDFBlocks.Select(b => b.DataType).Distinct())
                    dataTypes.AddRange(DataType.List.Where(d => d.Text.ToString() == datatype));
                instruction.SupportedDataTypes.Clear();
                instruction.SupportedDataTypes.AddRange(dataTypes);
                //Adding UDFBInstruction into the List
                udfbInstrion.Add(instruction);
            }
            //storing all the UDFB Instruction Names into the new string List
            udfbInstructionNames.AddRange(udfbInstrion.Select(T => T.Text));
        }
        /// <summary>
        /// for handling Dialog Box Error of dataGridView1
        /// </summary>
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        private void ChangeInstructionType()
        {
            if (comboBoxInstructionType.Text == "UDFB")
            {
                InstructionType selectedInstructionType = ((InstructionType)comboBoxInstructionType.SelectedValue);
                comboBoxInstruction.DataSource = udfbInstrion;
                comboBoxInstruction.DisplayMember = "Text";
                comboBoxInstruction.ValueMember = "ID";
            }
            else
            {
                InstructionType selectedInstructionType = ((InstructionType)comboBoxInstructionType.SelectedValue);
                List<InstructionTypeDeserializer> instructions = GettingSubInstruction(selectedInstructionType);
                comboBoxInstruction.DataSource = instructions;
                comboBoxInstruction.DisplayMember = "Text";
                comboBoxInstruction.ValueMember = "ID";
            }
        }
        private void comboBoxInstructionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableCellValueChanged = false;
            if (comboBoxInstructionType.SelectedIndex != -1)
                ChangeInstructionType();
            enableCellValueChanged = true;
        }
        private void AddInputandOutPut()
        {
            if (comboBoxInstruction.SelectedItem == null)
            {
                return;
            }
            // Retrieve the selected instruction 
            if (comboBoxInstructionType.Text == "UDFB")
            {
                datagridFunctionBlockIn.AllowUserToAddRows = false;
                datagridFunctionBlockOut.AllowUserToAddRows = false;
                Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);
                UDFBInfo uDFBInfo = (UDFBInfo)xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == comboBoxInstruction.SelectedItem.ToString()).FirstOrDefault();
                if (uDFBInfo != null)
                {
                    List<UserDefinedFunctionBlock> uDFBDetails = uDFBInfo.UDFBlocks;
                    // Populate supported data types for this instruction
                    comboBoxDataType.DataSource = selectedInstruction.SupportedDataTypes;

                    List<UserDefinedFunctionBlock> bi = uDFBInfo.UDFBlocks.Where(b => b.Type == "Input").ToList();
                    int i = 0;
                    datagridFunctionBlockIn.Rows.Clear();
                    foreach (UserDefinedFunctionBlock ub in bi)
                    {
                        datagridFunctionBlockIn.Rows.Add();
                        datagridFunctionBlockIn.Rows[i].Cells[0].Value = ub.Text;
                        datagridFunctionBlockIn.Rows[i].Cells[1].Value = ub.DataType;
                        DataGridViewComboBoxCell operandtype = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells["Operand_Type"];
                        List<string> operand = new List<string>();
                        operand.Add("Select Operand Type");
                        operand.Add("Normal Operand");
                        operand.Add("Negation Operand");
                        operand.Add("Numeric Operand");
                        if (!ub.DataType.Equals("Bool"))
                        {
                            operand.Remove("Negation Operand");
                        }
                        operandtype.DataSource = operand;
                        operandtype.Value = operandtype.Items[0];

                        DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells["Tag_For_Operand"];
                        tags.DataSource = XMProValidator.FillTagOperands(ub.DataType);
                        tags.Value = tags.Items[0];

                        datagridFunctionBlockIn.Rows[i].Cells["Tag_Address"].Value = "";
                        i++;
                    }
                    datagridFunctionBlockOut.Rows.Clear();
                    bi = uDFBInfo.UDFBlocks.Where(b => b.Type == "Output").ToList();
                    i = 0;
                    foreach (UserDefinedFunctionBlock ub in bi)
                    {
                        datagridFunctionBlockOut.Rows.Add();
                        datagridFunctionBlockOut.Rows[i].Cells["output"].Value = ub.Text;
                        datagridFunctionBlockOut.Rows[i].Cells["outdatatype"].Value = ub.DataType;
                        DataGridViewComboBoxCell cbooptype = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[i].Cells["outputoperand"];
                        List<string> optype = new List<string>();
                        optype.Add("On-board");
                        optype.Add("Remote");
                        optype.Add("Memory Address Variable");
                        optype.Add("Expansion");
                        cbooptype.DataSource = optype;
                        cbooptype.Value = cbooptype.Items[0];
                        DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[i].Cells["outputTag"];
                        if (cbooptype.Value.ToString() == "Memory Address Variable")
                        {
                            List<string> outputTags = XMProValidator.FillOutputAdddressForUDFB(ub.DataType);
                            var TagOp1 = outputTags.RemoveAll(DefaultAddedTag);
                            tags.DataSource = outputTags;
                            tags.Value = tags.Items[0];
                        }
                        else
                        {
                            if (cbooptype.Value.ToString() == "On-board" && ub.DataType == "Bool")
                            {
                                tags.DataSource = xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.OnBoardIO).Select(L => L.Tag).ToList();
                                tags.Value = tags.Items[0];
                            }
                            else if (cbooptype.Value.ToString() == "Remote")
                            {
                                tags.DataSource = xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.RemoteIO).Select(L => L.Tag).ToList();
                                tags.Value = tags.Items[0];
                            }
                            else if (cbooptype.Value.ToString() == "Expansion")
                            {
                                tags.DataSource = xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO).Select(L => L.Tag).ToList();
                                tags.Value = tags.Items[0];
                            }
                        }
                        datagridFunctionBlockOut.Rows[i].Cells["OPTagaddress"].Value = "";
                        i++;
                    }
                }
            }
            else
            {
                datagridFunctionBlockIn.AllowUserToAddRows = false;
                datagridFunctionBlockOut.AllowUserToAddRows = false;
                if (((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).Text != "XMPS2000.LadderLogic.InstructionTypeDeserializer")
                {
                    BindDataSourceToGridView();
                    if (((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).Text.Contains("MQTT") && datagridFunctionBlockOut.Rows.Count > 1)
                        datagridFunctionBlockOut.Rows[datagridFunctionBlockOut.Rows.Count - 1].Visible = false;
                }
            }
        }

        private void BindDataSourceToGridView()
        {
            datagridFunctionBlockIn.Rows.Clear();
            datagridFunctionBlockOut.Rows.Clear();
            InstructionTypeDeserializer selectedInstruction = ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem);
            InstructionTypeDeserializer instruction = xm.instructionsList.FirstOrDefault(t => t.Text.Equals(selectedInstruction.Text));
            int i = 0;
            int j = 0;
            foreach (IOModel iOModel in instruction.InputsOutputs)
            {
                if (iOModel.Type.Equals("Input"))
                {
                    datagridFunctionBlockIn.Rows.Add();
                    datagridFunctionBlockIn.Rows[i].Cells[0].Value = iOModel.Text;
                    datagridFunctionBlockIn.Rows[i].Cells[1].Value = !iOModel.DataType.Equals("Unknown") ? iOModel.DataType : ((DataTypeDesilizer)comboBoxDataType.SelectedItem).Text;
                    DataGridViewComboBoxCell Inputoperandtype = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[2];
                    if (datagridFunctionBlockIn.Rows[i].Cells[1].Value.ToString() == "Bool")
                    {
                        if (comboBoxInstructionType.Text.Equals("Pack") || (comboBoxInstructionType.Text.Equals("PulseVelocity") && i == 1))
                        {
                            Inputoperandtype.DataSource = new List<string>() { "Select Operand Type",
                                                                           "Normal Operand",
                                                                           "Negation Operand"};
                        }
                        else
                        {
                            Inputoperandtype.DataSource = new List<string>() { "Select Operand Type",
                                                                           "Normal Operand",
                                                                           "Negation Operand",
                                                                           "Numeric Operand"};
                        }

                    }
                    else
                    {
                        if (comboBoxInstructionType.Text.Contains("Timer") || comboBoxInstructionType.Text.Contains("Counter"))
                        {
                            //for Timer/Counter getting dynamic dataType
                            if (iOModel.DataType == "Bool")
                            {
                                Inputoperandtype.DataSource = new List<string>() { "Select Operand Type",
                                                                                   "Normal Operand",
                                                                                   "Negation Operand",
                                                                                   "Numeric Operand"};
                            }
                            else
                            {
                                Inputoperandtype.DataSource = new List<string>()
                                                                      {
                                                                        "Select Operand Type",
                                                                        "Normal Operand",
                                                                        "Numeric Operand"
                                                                      };
                            }
                            datagridFunctionBlockIn.Rows[i].Cells[1].Value = !iOModel.DataType.Equals("Unknown") ? iOModel.DataType : ((DataTypeDesilizer)comboBoxDataType.SelectedItem).Text;
                        }
                        else
                        {
                            Inputoperandtype.DataSource = new List<string>()
                                                                      {
                                                                        "Select Operand Type",
                                                                        "Normal Operand",
                                                                        "Numeric Operand"
                                                                      };
                        }
                    }
                    if (comboBoxInstructionType.Text.Equals("ReadProperty"))
                    {
                        Inputoperandtype.DataSource = new List<string>() { "Select Operand Type",
                                                                           "Normal Operand" };
                    }
                    Inputoperandtype.Value = Inputoperandtype.Items[0];
                    DataGridViewComboBoxCell InputTags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[3];
                    string selectedDataType = datagridFunctionBlockIn.Rows[i].Cells[1].Value.ToString();
                    string selectedDataType1 = comboBoxDataType.Text;
                    InputTags.DataSource = XMProValidator.FillTagOperands(selectedDataType, udfbname);
                    if (comboBoxInstructionType.Text.Equals("Pack"))
                    {
                        InputTags.DataSource = null;
                        InputTags.DataSource = XMProValidator.FillTagOperands("Pack-Bool", udfbname);
                    }
                    if (comboBoxInstructionType.Text.Equals("PulseVelocity") && i == 1)
                    {
                        InputTags.DataSource = null;
                        InputTags.DataSource = XMProValidator.FillTagOperands("PulseVelocity", udfbname);
                    }
                    else if (comboBoxInstructionType.Text.Equals("Write_Read_PV") && iOModel.Text.Equals("IN(Object)") && (selectedDataType.Equals("Bool") || selectedDataType.Equals("Real")))
                    {
                        InputTags.DataSource = null;
                        InputTags.DataSource = XMProValidator.FillBacnetObjectNames(selectedDataType, comboBoxInstruction.Text);
                    }
                    if (comboBoxInstructionType.Text.Equals("ReadProperty"))
                    {
                        InputTags.DataSource = null;
                        InputTags.DataSource = XMProValidator.FillBacnetObjectNames(comboBoxInstruction.Text);
                        if (comboBoxInstruction.Text.Equals("Schedule"))
                        {
                            InputTags.DataSource = null;
                            var obj = XMProValidator.FillTagOperands1(selectedDataType1, udfbname, "Schedule");
                            InputTags.DataSource = obj;
                        }
                    }
                    InputTags.Value = InputTags.Items[0];
                    i++;
                }
                if (iOModel.Type.Equals("Output"))
                {
                    datagridFunctionBlockOut.Rows.Add();
                    datagridFunctionBlockOut.Rows[j].Cells[0].Value = iOModel.Text;
                    List<string> outputDataTypes = instruction.OutputDataTypes.Select(T => T.Text).ToList();
                    datagridFunctionBlockOut.Rows[j].Cells[1].Value = outputDataTypes.Count > 0 ? outputDataTypes.First() :
                                                                    (!iOModel.DataType.Equals("Unknown") ? iOModel.DataType : ((DataTypeDesilizer)comboBoxDataType.SelectedItem).Text);
                    DataGridViewComboBoxCell OutputTypeList = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[j].Cells[2];
                    OutputTypeList.DataSource = OutputType.List;
                    OutputTypeList.DisplayMember = "Text";
                    OutputTypeList.ValueMember = "Text";
                    OutputTypeList.Value = OutputTypeList.Items[0];
                    if (comboBoxInstructionType.Text.Contains("Timer") || comboBoxInstructionType.Text.Contains("Counter") ||
                        comboBoxInstructionType.Text.Contains("PulseVelocity") || comboBoxInstructionType.Text.Contains("PID"))
                    {
                        //for Timer/Counter getting dynamic dataType
                        datagridFunctionBlockOut.Rows[j].Cells[1].Value = !iOModel.DataType.Equals("Unknown") ? iOModel.DataType : ((DataTypeDesilizer)comboBoxDataType.SelectedItem).Text;
                    }
                    //for the Scale instruction for second Output is REAL 
                    if (comboBoxInstructionType.Text.Contains("Scale") && j == 1)
                    {
                        datagridFunctionBlockOut.Rows[j].Cells[1].Value = ((DataTypeDesilizer)comboBoxDataType.SelectedItem).Text;
                    }
                    if (comboBoxInstructionType.Text.Equals("MQTT") && j == 1)
                    {
                        datagridFunctionBlockOut.Rows[j].Visible = false;
                    }
                    string selectedOutputType = datagridFunctionBlockOut.Rows[j].Cells[2].FormattedValue.ToString();
                    DataGridViewComboBoxCell OutputTags = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[j].Cells[3];
                    OutputTags.DataSource = null;
                    if (selectedOutputType != null)
                    {
                        OutputTags.DataSource = OutPutTypeChangedTags(selectedOutputType.ToString(), j);
                    }
                    if (comboBoxInstructionType.Text.Equals("Write_Read_PV"))
                        DisableFirstRowOutput(true);
                    else
                        DisableFirstRowOutput(false);
                    j++;
                }
            }
        }

        private void DisableFirstRowOutput(bool value)
        {
            if (datagridFunctionBlockOut.Rows.Count > 0)
            {
                DataGridViewRow firstRowContainer = (DataGridViewRow)datagridFunctionBlockOut.Rows[0];
                if (firstRowContainer != null)
                    firstRowContainer.ReadOnly = value;
            }
        }

        private void FunctionBlockInputsAndOutputs_Load(object sender, EventArgs e)
        {
            if (isEnabled) checkBoxEnable.Checked = true;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (comboBoxInstructionType.Text == "UDFB")
            {
                int rowCount = datagridFunctionBlockIn.RowCount;
                if (datagridFunctionBlockIn.CurrentCell != null)
                {
                    int actcol = datagridFunctionBlockIn.CurrentCell.RowIndex;
                    if (rowCount > 0)
                    {
                        if (e.ColumnIndex == 2)
                        {
                            DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[actcol].Cells["Operand_Type"];
                            string selectedValue = comboBoxCell.Value?.ToString();
                            DataGridViewCell cell = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[4];
                            if (selectedValue.Equals("Normal Operand") || selectedValue.Equals("Negation Operand") || selectedValue.Equals("Select Operand Type"))
                            {
                                cell.Value = GetNextAddressAndUpdateTextBox(selectedValue, cell, "Inputs", "");
                                ((DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[e.RowIndex].Cells[3]).DataSource = XMProValidator.FillTagOperands(datagridFunctionBlockIn.Rows[e.RowIndex].Cells[1].Value.ToString(), udfbname);
                            }
                            else if (selectedValue.Equals("Numeric Operand"))
                            {
                                datagridFunctionBlockIn.Rows[actcol].Cells["Tag_Address"].Value = string.Empty;
                                ((DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[e.RowIndex].Cells[3]).DataSource = null;
                            }
                        }
                        else if (e.ColumnIndex == 3)
                        {
                            DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[actcol].Cells["Tag_For_Operand"];
                            string selectedValue = comboBoxCell.Value?.ToString();
                            if (selectedValue != null)
                            {
                                datagridFunctionBlockIn.Rows[actcol].Cells["Tag_Address"].Value = XMProValidator.GetTheAddressFromTag(selectedValue);
                            }
                        }
                        else if (e.ColumnIndex == 4)
                        {
                            DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[actcol].Cells["Operand_Type"];
                            datagridFunctionBlockIn.Rows[actcol].Cells["Tag_Address"].Value = ((bool)(comboBoxCell.Value?.ToString().Contains(":"))) ? string.Empty :
                                                                                             datagridFunctionBlockIn.Rows[actcol].Cells["Tag_Address"].Value;

                            TextBox textBox = new TextBox();
                            if (datagridFunctionBlockIn.Rows[actcol].Cells["Tag_Address"].Value != null)
                            {
                                string cell = datagridFunctionBlockIn.Rows[actcol].Cells["Tag_Address"].Value.ToString();
                                cell = cell.Trim();
                                cell = cell.StartsWith("+") ? cell.Replace("+", "").Trim() : cell;
                                datagridFunctionBlockIn.Rows[actcol].Cells["Tag_Address"].Value = cell;
                                textBox.Text = datagridFunctionBlockIn.Rows[actcol].Cells["Tag_Address"].Value.ToString();
                                textBox.Tag = datagridFunctionBlockIn.Rows[actcol].Cells["datatype"].Value.ToString();
                                textBox.Name = datagridFunctionBlockIn.Rows[actcol].Cells["Input_1"].Value.ToString();

                            }

                            ValidateOperand(textBox, datagridFunctionBlockIn.Rows[actcol].Cells["Operand_Type"].Value.ToString(), "", "Input");
                        }
                    }
                }
            }
            else if (e.RowIndex < datagridFunctionBlockIn.Rows.Count)
            {
                if (enableCellValueChanged)
                {
                    if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                    {
                        datagridFunctionBlockIn.BeginInvoke(new Action(() =>
                        {
                            if (!(e.RowIndex < datagridFunctionBlockIn.Rows.Count)) return;
                            string selectedValue = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            if (selectedValue != null)
                            {
                                DataGridViewComboBoxCell InputTags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[e.RowIndex].Cells[3];
                                if (selectedValue == "Numeric Operand")
                                {
                                    DataGridViewCell cell = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[4];
                                    cell.Value = string.Empty;
                                    InputTags.DataSource = null;
                                }
                                else if (selectedValue == "Negation Operand" || selectedValue == "Normal Operand")
                                {
                                    DataGridViewCell cell = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[4];
                                    cell.Value = "";
                                    string selectedDataType = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[1].Value.ToString();
                                    if (comboBoxInstruction.Text == "Pack")
                                    {
                                        InputTags.DataSource = XMProValidator.FillTagOperands("Pack-Bool", "");
                                        InputTags.Value = InputTags.Items[0];
                                    }
                                    else if (comboBoxInstructionType.Text.Equals("PulseVelocity") && e.RowIndex == 1)
                                    {
                                        InputTags.DataSource = null;
                                        InputTags.DataSource = XMProValidator.FillTagOperands("PulseVelocity", udfbname);
                                    }
                                    else if (comboBoxInstructionType.Text.Equals("Write_Read_PV") && datagridFunctionBlockIn.Rows[e.RowIndex].Cells[0].Value.ToString().Equals("IN(Object)") && (selectedDataType.Equals("Bool") || selectedDataType.Equals("Real")))
                                    {
                                        InputTags.DataSource = null;
                                        InputTags.DataSource = XMProValidator.FillBacnetObjectNames(selectedDataType, comboBoxInstruction.Text);
                                    }
                                    else if (comboBoxInstructionType.Text.Equals("ReadProperty"))
                                    {
                                        InputTags.DataSource = null;
                                        InputTags.DataSource = XMProValidator.FillBacnetObjectNames(comboBoxInstruction.Text);
                                        if (comboBoxInstruction.Text == "Schedule")
                                        {
                                            InputTags.DataSource = null;
                                            var obj = XMProValidator.FillTagOperands1(comboBoxDataType.Text, udfbname, "Schedule");
                                            InputTags.DataSource = obj;
                                        }
                                    }
                                    else
                                    {
                                        InputTags.DataSource = XMProValidator.FillTagOperands(selectedDataType, udfbname);
                                        InputTags.Value = InputTags.Items[0];
                                    }
                                    if (comboBoxInstructionType.Text.Equals("PulseVelocity") && e.RowIndex == 1)
                                        cell.Value = string.Empty;
                                    else if (comboBoxInstruction.Text.Equals("AI") || comboBoxInstruction.Text.Equals("AO") || comboBoxInstruction.Text.Equals("BI") || comboBoxInstruction.Text.Equals("BO"))
                                        cell.Value = string.Empty;
                                    else
                                        cell.Value = GetNextAddressAndUpdateTextBox(selectedValue, cell, "Inputs", "");
                                }
                            }
                        }));
                    }
                    else if (e.ColumnIndex == 4 && e.RowIndex >= 0)
                    {
                        datagridFunctionBlockIn.BeginInvoke(new Action(() =>
                        {
                            if (datagridFunctionBlockIn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                              datagridFunctionBlockIn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                            {
                                string selectedValue = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                                string OpreandType = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[2].Value.ToString();
                                if (selectedValue != null)
                                {
                                    DataGridViewComboBoxCell InputTags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[e.RowIndex].Cells[3];
                                    if (OpreandType == "Numeric Operand")
                                    {
                                        DataGridViewCell cell = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[4];
                                        cell.Value = cell.Value.ToString().Contains(":") ? string.Empty : cell.Value;
                                        string error = string.Empty;
                                        cell.Value = cell.Value.ToString().Trim();
                                        cell.Value = cell.Value.ToString().StartsWith("+") ? cell.Value.ToString().Replace("+", "").Trim() : cell.Value;
                                        bool isvalidateTag = ValidateCellValue(cell, out error, OpreandType);
                                        errorProviderFB.Clear();
                                        lblerror.Text = "";
                                        if (!isvalidateTag && !string.IsNullOrEmpty(error))
                                        {
                                            errorProviderFB.Clear();
                                            lblerror.Text = "";
                                            // by using these we set common error for dataGridView
                                            int rownumber = e.RowIndex;
                                            errorProviderFB.SetError(datagridFunctionBlockIn, error + " at Input No " + (rownumber + 1));
                                            lblerror.Text = error;
                                        }
                                    }
                                    else if (OpreandType == "Negation Operand" || OpreandType == "Normal Operand" || OpreandType == "Select Operand Type")
                                    {
                                        DataGridViewCell cell = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[4];
                                        string error = string.Empty;
                                        bool isvalidateTag = ValidateCellValue(cell, out error, OpreandType);
                                        errorProviderFB.Clear();
                                        lblerror.Text = "";
                                        if (!isvalidateTag && !string.IsNullOrEmpty(error))
                                        {
                                            errorProviderFB.Clear();
                                            lblerror.Text = "";
                                            // by using these we set common error for dataGridView
                                            int rownumber = e.RowIndex;
                                            errorProviderFB.SetError(datagridFunctionBlockIn, error + " at Input No " + (rownumber + 1));
                                            lblerror.Text = error;
                                        }
                                    }
                                }
                                //for Pack Instruction shown an next address
                                if (comboBoxInstructionType.Text.ToString().Equals("Pack"))
                                {
                                    string startAdd = selectedValue;
                                    string error = (startAdd.Contains(".") || startAdd.Contains("S3")) ? "User Memory address variable." : "No available address found";

                                    if (startAdd != "" && startAdd != "No available address found" && startAdd.Contains(":") && !startAdd.Contains(".") && !startAdd.StartsWith("S3"))
                                    {
                                        string[] AddressParts = startAdd.Split(':');
                                        int AddressPartSecond = int.Parse(AddressParts[1]) + 15;
                                        string LastlogicalAddress = $"{AddressParts[0]}:{AddressPartSecond:000}";
                                        int usedAddCount = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")).Where(T => int.Parse(T.LogicalAddress.Split(':')[1]) >= int.Parse(startAdd.Split(':')[1]) && int.Parse(T.LogicalAddress.Split(':')[1]) <= int.Parse(LastlogicalAddress.Split(':')[1])).Count();
                                        if (usedAddCount > 0)
                                        {
                                            LastlogicalAddress = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")).OrderBy(T => T.LogicalAddress).Last().LogicalAddress;
                                        }
                                        List<XMIOConfig> usedTagList = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")).Where(T => int.Parse(T.LogicalAddress.Split(':')[1]) >= int.Parse(startAdd.Split(':')[1]) && int.Parse(T.LogicalAddress.Split(':')[1]) <= int.Parse(LastlogicalAddress.Split(':')[1])).OrderBy(T => T.LogicalAddress).ToList();
                                        DialogResult dialog = DialogResult.No;
                                        if (usedTagList.Count > 0)
                                        {
                                            dialog = MessageBox.Show("Next Some Address Are Already Used You Want To Change Starting Add", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                            string[] parts = usedTagList.Last().LogicalAddress.Split(':');
                                            int secondPart = int.Parse(parts[1]);
                                            int lastTagAdd = int.Parse(parts[1]) + 1;
                                            string nextUnusedTagAdd = $"{parts[0]}:{lastTagAdd:000}";
                                            if (dialog == DialogResult.OK)
                                            {
                                                datagridFunctionBlockIn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = nextUnusedTagAdd;
                                            }
                                        }

                                        if (usedTagList.Count == 0 || dialog == DialogResult.OK)
                                        {
                                            //ONLY FOR SHOWING IN BRACKETS
                                            string[] parts = datagridFunctionBlockIn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Split(':');
                                            int secondPart = int.Parse(parts[1]);
                                            int lastTagAdd = int.Parse(parts[1]) + 15;
                                            string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                                            if (secondPart + 15 > 1023)
                                            {
                                                lblerror.Text = "Max Tag-Range Excied";
                                                lblerror.ForeColor = Color.Red;
                                                lblerror.Visible = true;
                                            }
                                            else
                                            {
                                                lblerror.Text = "(" + datagridFunctionBlockIn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() + "..." + endAdd + ")";
                                                lblerror.ForeColor = Color.Red;
                                                lblerror.Visible = true;
                                            }
                                        }
                                        isPackInputEdit = true;
                                    }
                                    else if (startAdd != "" || startAdd != "No available address found")
                                    {
                                        errorProviderFB.SetError(datagridFunctionBlockIn, error + " at Input No " + (e.RowIndex + 1));
                                        lblerror.Text = error + " at Input No " + (e.RowIndex + 1);
                                    }
                                }

                                if (comboBoxInstructionType.Text.ToString().Equals("UnPack") && edit)
                                {
                                    isUnPackInputEdit = true;
                                }
                            }

                        }));
                        if (comboBoxInstructionType.Text.Equals("Write_Read_PV"))
                        {
                            string opLogicalAddress = datagridFunctionBlockIn.Rows[0].Cells[4].Value.ToString();
                            if (opLogicalAddress != "")
                            {
                                if (XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress.Equals(opLogicalAddress)).Select(t => t.IoList).ToString() == "OnBoardIO")
                                    datagridFunctionBlockOut.Rows[0].Cells[2].Value = "On-board";
                                else if (XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress.Equals(opLogicalAddress)).Select(t => t.IoList).ToString() == "ExpansionIO")
                                    datagridFunctionBlockOut.Rows[0].Cells[2].Value = "Expansion";
                                else if (XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress.Equals(opLogicalAddress)).Select(t => t.IoList).ToString() == "RemoteIO")
                                    datagridFunctionBlockOut.Rows[0].Cells[2].Value = "Remote";
                                else
                                    datagridFunctionBlockOut.Rows[0].Cells[2].Value = "Memory Address Variable";
                                if (XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == opLogicalAddress).Count() > 0)
                                    datagridFunctionBlockOut.Rows[0].Cells[3].Value = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress.Equals(opLogicalAddress)).Select(t => t.Tag).FirstOrDefault().ToString();
                                datagridFunctionBlockOut.Rows[0].Cells[4].Value = opLogicalAddress;
                            }
                        }
                    }
                }
            }

        }
        private bool IsAddressFilled(string address)
        {
            return filledAddresses.Contains(address);
        }
        private string GetNextAddressAndUpdateTextBox(string selectedValue, DataGridViewCell cell, string gridViewName, string instructionType)
        {
            string dataType = gridViewName.Equals("Inputs") ? datagridFunctionBlockIn.Rows[cell.RowIndex].Cells[1].Value.ToString()
                                                           : datagridFunctionBlockOut.Rows[cell.RowIndex].Cells[1].Value.ToString();
            List<string> otherAddresses = new List<string> { };
            foreach (DataGridViewRow row in datagridFunctionBlockIn.Rows)
            {
                dataType = row.Cells[1].Value.ToString();
                if (row.Cells[4].Value != null && row.Cells[4].Value.ToString() != "" && row.Cells[4].Value.ToString().Contains(":"))
                {
                    if (dataType == "Double Word" || dataType == "DINT" || dataType == "UDINT")
                    {
                        if (row.Cells[4].Value.ToString().StartsWith("W4"))
                        {
                            otherAddresses.Add(row.Cells[4].Value.ToString());
                            otherAddresses.Add(row.Cells[4].Value.ToString().Split(':')[0] + ":" + (int.Parse(row.Cells[4].Value.ToString().Split(':')[1]) + 1));
                        }
                    }
                    else
                    {
                        otherAddresses.Add(row.Cells[4].Value.ToString());
                    }
                }
            }
            foreach (DataGridViewRow row in datagridFunctionBlockOut.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    dataType = row.Cells[1].Value.ToString();

                    if (row.Cells[4].Value != null && row.Cells[4].Value.ToString() != "" && row.Cells[4].Value.ToString().Contains(":"))
                    {
                        if (dataType == "Double Word" || dataType == "DINT" || dataType == "UDINT")
                        {
                            if (row.Cells[4].Value.ToString().StartsWith("W4"))
                            {
                                otherAddresses.Add(row.Cells[4].Value.ToString());
                                otherAddresses.Add(row.Cells[4].Value.ToString().Split(':')[0] + ":" + (int.Parse(row.Cells[4].Value.ToString().Split(':')[1]) + 1));
                            }
                        }
                        else
                        {
                            otherAddresses.Add(row.Cells[4].Value.ToString());
                        }
                    }
                }
            }
            dataType = gridViewName.Equals("Inputs") ? datagridFunctionBlockIn.Rows[cell.RowIndex].Cells[1].Value.ToString()
                                               : datagridFunctionBlockOut.Rows[cell.RowIndex].Cells[1].Value.ToString();
            string address = XMProValidator.GetNextAddress(dataType, instructionType, otherAddresses);
            if (address != null && !IsAddressFilled(address))
            {
                return address;
            }
            return "";
        }
        private bool ValidateCellValue(DataGridViewCell cell, out string error, string oprandType)
        {
            bool validationSuccessful = true;
            error = string.Empty;
            if (cell.Value == null) return false;
            if (string.IsNullOrEmpty(cell.Value.ToString()))     // Allow untouched or emptied operands.
            {
                validationSuccessful = true;
            }
            else if (comboBoxInstructionType.SelectedItem.ToString() == "UnPack")
            {
                string address = cell.Value.ToString();
                XMIOConfig tag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == address).FirstOrDefault();
                if (tag != null && tag.Label != "Word")
                {
                    error = "Invalid Word Address";
                    validationSuccessful = false;
                }
            }
            else
            {
                switch (oprandType)
                {
                    case "Normal Operand":     // Normal i.e. Address
                    case "Negation Operand":     // Negation Operand
                    case "Select Operand Type":
                        validationSuccessful = ValidateAddressOperandForFB(cell, out error);
                        break;
                    case "Numeric Operand":     // Numeric Operand
                        validationSuccessful = ValidateNumericOperandForFB(cell, out error);
                        break;
                }
            }
            if (validationSuccessful)
            {
                ///Adding Logic For the Checking Tags Location
                string CurrentLogicalBlock = xm.CurrentScreen.Split('#')[1];
                string input1Address = cell.Value.ToString();
                XMIOConfig EnteredTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == input1Address).FirstOrDefault();
                if (EnteredTag != null)
                {
                    Block currentBlk = xm.LoadedProject.Blocks.FirstOrDefault(t => t.Name.Equals(CurrentLogicalBlock));
                    if (currentBlk.Type.Equals("LogicBlock") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !XMPS.Instance.PlcModels.Contains(EnteredTag.Model.Split('_')[0]))))
                    {
                        if (EnteredTag.Model != "User Defined Tags" && EnteredTag.Model != "")
                        {
                            error = $"Used Tag is Not Found in Current Logic Block at Input {cell.RowIndex + 1} ";
                            return false;
                        }
                    }
                    else if (currentBlk.Type.Equals("UDFB") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !XMPS.Instance.PlcModels.Contains(EnteredTag.Model.Split('_')[0]))))
                    {
                        string UdfbName = CurrentLogicalBlock.Split(' ')[0];
                        if (EnteredTag.Model.ToString() != UdfbName + " Tags")
                        {
                            error = $"Used Tag is Not Found in Current UDFB Block at Input {cell.RowIndex + 1}";
                            return false;
                        }
                    }

                }
                return true;
            }
            else
            {
                if (udfbname != "")
                {
                    if (oprandType == "Normal Operand" || oprandType == "Negation Operand" || oprandType == "Select Operand Type")
                    {
                        UDFBInfo uDFB = xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == udfbname).FirstOrDefault();
                        if (uDFB.UDFBlocks.Where(b => b.Text == cell.Value.ToString() && b.DataType == datagridFunctionBlockIn.Rows[cell.RowIndex].Cells[1].Value.ToString()).Count() == 0)
                        {
                            error = "Invalid Address for " + $"{datagridFunctionBlockIn.Rows[cell.RowIndex].Cells[1].Value.ToString()}"
                            + "DataType";
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (!validationSuccessful && (error != "" || error != null))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
                return false;
            }
        }
        private bool ValidateNumericOperandForFB(DataGridViewCell cell, out string error)
        {
            string number = cell.Value.ToString();
            string dataType = datagridFunctionBlockIn.Rows[cell.RowIndex].Cells[1].Value.ToString();
            return XMProValidator.ValidateNumericInputOperand(number, dataType, out error,null);
        }
        private bool ValidateAddressOperandForFB(DataGridViewCell cell, out string error)
        {
            string address = cell.Value.ToString();
            if (address == "-") cell.Value.ToString();
            bool validationSuccessful;
            string dataType = datagridFunctionBlockIn.Rows[cell.RowIndex].Cells[1].Value.ToString();
            string Instruction = comboBoxInstructionType.SelectedItem.ToString();
            string currentInstruction = comboBoxInstruction.Text;
            switch (dataType)
            {
                case "Bool":
                    error = $"Invalid Bit Address at Input {cell.RowIndex + 1}";
                    if ((currentInstruction == "BI" || currentInstruction == "BO") && address.StartsWith("F2:"))
                    {
                        validationSuccessful = false;
                    }
                    else
                    {
                        validationSuccessful = address.IsValidBitAddress();
                    }
                    break;
                case "Real":
                    error = $"Invalid Real Address at Input {cell.RowIndex + 1}";
                    if (currentInstruction == "AI" || currentInstruction == "AO")
                    {
                        validationSuccessful = address.IsValidRealWordAddress() &&
                                            (address.StartsWith("I1:") || address.StartsWith("Q0:"));
                    }
                    else
                    {
                        validationSuccessful = address.IsValidRealWordAddress();
                    }
                    break;
                case "DINT":
                    error = $"Invalid Word address for DINT data type at Input{cell.RowIndex + 1}";
                    validationSuccessful = address.IsValidDINTWordAddress();
                    break;
                case "UDINT":
                    error = $"Invalid Word address for UDINT data type at Input{cell.RowIndex + 1}";
                    validationSuccessful = address.IsValidUDINTWordAddress();
                    break;
                case "Word":
                    error = $"Invalid Word address at Input{cell.RowIndex + 1}";
                    validationSuccessful = address.IsValidWordAddress();
                    break;
                case "Byte":
                case "Double Word":
                case "Int":
                    error = $"Invalid {dataType} address at Input{cell.RowIndex + 1}";
                    validationSuccessful = address.IsValidByteAddress(dataType);
                    break;
                case "Schedule":
                case "Notification":
                case "Device":
                    validationSuccessful = false;
                    error = string.Empty;
                    bool isThere = false;
                    switch (comboBoxInstruction.Text)
                    {
                        case "Notification":
                            isThere = xm.LoadedProject.BacNetIP.Notifications.Any(t => t.ObjectName.Equals(address));
                            if (isThere)
                            {
                                error = $"Invalid Notification object Name {cell.RowIndex + 1}";
                                validationSuccessful = true;
                            }
                            else
                            {
                                error = $"Invalid Notification object Name {cell.RowIndex + 1}";
                                validationSuccessful = false;
                            }
                            break;
                        case "Device":
                            isThere = xm.LoadedProject.BacNetIP.Device.ObjectName.Equals(address);
                            if (isThere)
                            {
                                error = $"Invalid Device object Name {cell.RowIndex + 1}";
                                validationSuccessful = true;
                            }
                            else
                            {
                                error = $"Invalid Device object Name {cell.RowIndex + 1}";
                                validationSuccessful = false;
                            }
                            break;
                        case "Schedule":
                            if (comboBoxDataType.Text == "Bool")
                            {
                                isThere = xm.LoadedProject.BacNetIP.Schedules.Any(t => t.ObjectName.Equals(address) && t.ScheduleValue.Equals(0));
                            }

                            else
                            {
                                isThere = xm.LoadedProject.BacNetIP.Schedules.Any(t => t.ObjectName.Equals(address) && t.ScheduleValue.Equals(1));
                            }

                            if (isThere)
                            {
                                error = $"Invalid Notification object Name {cell.RowIndex + 1}";
                                validationSuccessful = true;
                            }
                            else
                            {
                                error = $"Invalid Schedule object Name {cell.RowIndex + 1}";
                                validationSuccessful = false;
                            }
                            break;
                    }
                    break;
                // Timer data types
                case "TON":
                case "TOFF":
                case "TP":
                case "RTON":
                case "CTU":
                case "CTD":
                case "Pack":
                default:
                    if (Instruction == "Arithmetic" || Instruction == "Bit Shift" || Instruction == "Limit" || Instruction == "Compare" || Instruction == "Data Conversion")
                    {
                        error = "Invalid Word address";
                        validationSuccessful = address.IsValidNonFlotingWordAddress();
                    }
                    else
                    {
                        error = $"Invalid {dataType} address";
                        validationSuccessful = address.IsValidWordAddress(dataType);
                    }
                    break;
            }

            if (validationSuccessful)
            {
                bool fbDataType = dataType == "TON" || dataType == "TOFF" || dataType == "RTON" || dataType == "TP" || dataType == "CTU" || dataType == "CTD";
                var addedDataType = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == cell.Value.ToString()).FirstOrDefault();
                if (addedDataType != null)
                {
                    if (addedDataType.Model != "" || ((addedDataType.Label == dataType || fbDataType)) || Instruction == "UnPack")
                    {
                        error = string.Empty;
                        return true;
                    }
                    else
                    {
                        error = "Data type not matching need to select " + dataType + " and this logical address has datatype " + addedDataType;
                        return false;
                    }
                }
                else
                {
                    error = string.Empty;
                    return true;
                }
            }
            else
                return false;
        }

        private void buttonFBfromClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
        private void datagridFunctionBlock_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Check if the current cell is in the desired column
            var currentCell = datagridFunctionBlockIn.CurrentCell;
            if (currentCell != null && (currentCell.ColumnIndex == 2 || currentCell.ColumnIndex == 3))
            {
                // Commit the cell value to trigger the ValueChanged event
                if (datagridFunctionBlockIn.IsCurrentCellDirty)
                {
                    datagridFunctionBlockIn.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }
        private void datagridFunctionBlock_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //Adding for the Che 
            if (e.Control is ComboBox comboBox)
            {
                // Remove the previous event handler, if any
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                // Add a new event handler for the ComboBox within the DataGridView
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                int selectedIndex = comboBox.SelectedIndex;
                //stop the other code until updating values in last cell from data GridView
                datagridFunctionBlockIn.BeginInvoke(new Action(() =>
                {
                    int currentRowIndex = datagridFunctionBlockIn.CurrentCell.RowIndex;
                    int currentColumnIndex = datagridFunctionBlockIn.CurrentCell.ColumnIndex;
                    object cellValue = datagridFunctionBlockIn.Rows[currentRowIndex].Cells[currentColumnIndex].Value;
                    if (currentColumnIndex == 3)
                    {
                        if (cellValue != null)
                        {
                            if (cellValue.ToString() == "-Select Tag Name-")
                            {
                                string selectedValue = datagridFunctionBlockIn.Rows[currentRowIndex].Cells[currentColumnIndex - 1].Value.ToString();
                                if (selectedValue == "Negation Operand" || selectedValue == "Normal Operand")
                                {
                                    if (comboBoxInstruction.Text.Equals("PLSV") && currentRowIndex == 1)
                                        return;
                                    else if (comboBoxInstruction.Text.Equals("AI") || comboBoxInstruction.Text.Equals("AO") || comboBoxInstruction.Text.Equals("BI") || comboBoxInstruction.Text.Equals("BO"))
                                        return;
                                    DataGridViewCell cell = datagridFunctionBlockIn.Rows[currentRowIndex].Cells[4];
                                    cell.Value = GetNextAddressAndUpdateTextBox(selectedValue, cell, "Inputs", "");
                                }
                            }
                            else
                            {
                                string LogicalAdd = XMProValidator.GetTheAddressFromTag(cellValue.ToString());
                                DataGridViewCell cell = datagridFunctionBlockIn.Rows[currentRowIndex].Cells[4];
                                cell.Value = LogicalAdd;
                            }
                        }
                    }
                }));
            }

        }
        /// <summary>
        /// Validate operands depending on the type of control 
        /// </summary>
        /// <param name="control">Name of the control from whoes validate this call is generated.</param>
        /// <param name="e">This parameter will specify whether to add or update the line.</param>
        /// <returns>Describe return value.</returns>
        private void ValidateOperand(Control control, string OperandType, string OutPutType, string gridViewType)
        {
            bool validationSuccessful = true;
            string error = string.Empty;
            if (string.IsNullOrEmpty(control.Text))     // Allow untouched or emptied operands.
            {
                validationSuccessful = true;
            }
            else
            {
                int operandType = OperandType.StartsWith("Nume") ? 1 : 0;
                switch (operandType)
                {
                    case 0:     // Normal i.e. Address
                    case 2:     // Negation Operand
                        validationSuccessful = ValidateAddressOperand(control, out error, OutPutType, gridViewType);
                        break;
                    case 1:     // Numeric Operand
                        validationSuccessful = ValidateNumericOperand(control, out error);
                        break;
                }
            }

            if (validationSuccessful)
            {
                lblerror.Text = "";
            }
            else
            {
                lblerror.Text = control.Name + " : " + error;
                MessageBox.Show(error);
            }
            string CurrentLogicalBlock = xm.CurrentScreen.Split('#')[1];
            string input1Address = control.Text;
            XMIOConfig EnteredTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == input1Address).FirstOrDefault();
            if (EnteredTag != null)
            {
                Block currentBlk = xm.LoadedProject.Blocks.FirstOrDefault(t => t.Name.Equals(CurrentLogicalBlock));

                if (currentBlk.Type.Equals("LogicBlock") && (EnteredTag != null && (EnteredTag.Model != null && !XMPS.Instance.PlcModels.Contains(EnteredTag.Model.Split('_')[0]))))
                {
                    if (EnteredTag.Model != "User Defined Tags" && EnteredTag.Model != "")
                    {
                        lblerror.Text = control.Name + " : " + "Used Tag is Not Found in Current Logic Block";
                    }
                }
            }
        }

        /// <summary>
        /// Validate Numerical Operands depending upon the selected type
        /// </summary>        
        /// <param name="linenumber">Integer this parameter will tell at what line record should get added 
        /// this be differant on click of different buttons like on Insert After click it will take the Line number which 
        /// is selected before and add one to it and then send this line number and like wise.</param>
        /// <param name="counterValue">This parameter is used in specific conditions like Time or Counter instruction is Updated or Insert After Click.</param>
        /// <param name="addingNewRow">This parameter will specify whether to add or update the line.</param>
        /// <returns>Describe return value.</returns>
        private bool ValidateNumericOperand(Control control, out string error)
        {
            string number = control.Text;
            string dataType = control.Tag.ToString();
            return XMProValidator.ValidateNumericInputOperand(number, dataType, out error,null);
        }
        /// <summary>
        /// Validating Address type of operands
        /// </summary>
        /// <param name="control">Send the nmae of control.</param>        
        /// <returns>True if operand is valid else false.</returns>
        /// <returns>Error decription as String.</returns>
        private bool ValidateAddressOperand(Control control, out string error, string OutPutType, string gridViewType)
        {
            string address = control.Text;
            if (address == "-") address = control.Text;
            bool validationSuccessful;
            string dataType = control.Tag.ToString(); //comboBoxDataType.SelectedItem
            string Instruction = comboBoxInstructionType.SelectedItem.ToString();
            switch (dataType)
            {
                case "Bool":
                    error = "Invalid Bit Address";
                    if (gridViewType.Equals("Input"))
                        validationSuccessful = address.IsValidBitAddress();
                    else
                        validationSuccessful = address.IsValidOutputBitAddress(OutPutType, xm.LoadedProject.Tags.Where(R => R.LogicalAddress.Contains(".") && R.Type.ToString().Contains("Output")).ToList());
                    break;
                case "Real":
                    error = "Invalid Word address for Real data type";
                    validationSuccessful = address.IsValidRealWordAddress();
                    break;
                case "DINT":
                    error = "Invalid Word address for DINT data type";
                    validationSuccessful = address.IsValidDINTWordAddress();
                    break;
                case "UDINT":
                    error = "Invalid Word address for UDINT data type";
                    validationSuccessful = address.IsValidUDINTWordAddress();
                    break;
                case "Word":
                    error = "Invalid Word address";
                    validationSuccessful = address.IsValidWordAddress();
                    break;
                case "Byte":
                    error = "Invalid Byte address";
                    validationSuccessful = address.IsValidByteAddress(dataType);
                    break;

                // Timer data types
                case "TON":
                case "TOFF":
                case "TP":
                    if (control.Name.Equals("textBoxOp1Address"))
                    {
                        error = "Invalid Bit Address";
                        validationSuccessful = address.IsValidBitAddress();
                    }
                    else
                    {
                        error = "Invalid Word address";
                        validationSuccessful = address.IsValidNonFlotingWordAddress();
                    }
                    break;
                // Counter data types
                case "CTU":
                case "CTD":
                    if (control.Name.Equals("textBoxOp3Address"))
                    {
                        error = "Invalid Word address";
                        validationSuccessful = address.IsValidNonFlotingWordAddress();
                    }
                    else
                    {
                        error = "Invalid Bit Address";
                        validationSuccessful = address.IsValidBitAddress();
                    }
                    break;

                default:
                    if (Instruction == "Arithmetic" || Instruction == "Bit Shift" || Instruction == "Limit" || Instruction == "Compare" || Instruction == "Data Conversion")
                    {
                        error = "Invalid Word address";
                        validationSuccessful = address.IsValidNonFlotingWordAddress();
                    }
                    else
                    {
                        error = "Invalid " + dataType + " address";
                        validationSuccessful = address.IsValidWordAddress(dataType);
                    }
                    break;
            }

            if (validationSuccessful)
            {
                error = string.Empty;
                return true;
            }
            else
                return false;
        }
        private void datagridFunctionBlockOut_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (comboBoxInstructionType.Text == "UDFB")
            {
                int rowCount = datagridFunctionBlockOut.RowCount;
                if (datagridFunctionBlockOut.CurrentCell != null)
                {
                    int actcol = datagridFunctionBlockOut.CurrentCell.RowIndex;
                    if (rowCount > 0)
                    {
                        if (e.ColumnIndex == 2)
                        {
                            ((DataGridViewCell)datagridFunctionBlockOut.Rows[e.RowIndex].Cells[4]).Value = string.Empty;
                            ((DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[actcol].Cells["outputTag"]).DataSource = null;
                            DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[actcol].Cells["outputoperand"];
                            string selectedValue = comboBoxCell.Value?.ToString();
                            string datatype = datagridFunctionBlockOut.Rows[e.RowIndex].Cells["outdatatype"].Value?.ToString();
                            List<string> tagList = new List<string> { };
                            if (selectedValue != "")
                            {
                                switch (selectedValue)
                                {
                                    case "On-board":
                                        if (datatype == "Bool")
                                            tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.OnBoardIO).Select(L => L.Tag).ToList());
                                        if (datatype == "Word")
                                            tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.Type == Core.Types.IOType.AnalogOutput && L.IoList == Core.Types.IOListType.OnBoardIO).Select(L => L.Tag).ToList());
                                        break;
                                    case "Remote":
                                        tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.RemoteIO).Select(L => L.Tag).ToList());
                                        break;
                                    case "Expansion":
                                        if (datatype == "Bool")
                                            tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO && L.LogicalAddress.Contains('.')).Select(L => L.Tag).ToList());
                                        else
                                            tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO && !L.LogicalAddress.Contains('.')).Select(L => L.Tag).ToList());
                                        break;
                                    case "Memory Address Variable":
                                        List<string> logicBlockTags = new List<string>();
                                        logicBlockTags.AddRange(XMProValidator.FillTagOperands(datatype));
                                        tagList.AddRange(logicBlockTags);
                                        var NonDTags = logicBlockTags.RemoveAll(DefaultAddedTag);
                                        if (datatype == "Bool")
                                        {
                                            tagList.RemoveAll(T =>
                                            {
                                                var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(T));
                                                return tag != null && (tag.Type.ToString().Equals("DigitalInput") || tag.Type.ToString().Equals("DigitalOutput"));
                                            });
                                        }
                                        else
                                        {
                                            tagList.RemoveAll(T =>
                                            {
                                                var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(T));
                                                return tag != null && (tag.Type.ToString().Equals("DigitalInput") || tag.Type.ToString().Equals("DigitalOutput")
                                                                       || tag.Type.ToString().Equals("AnalogInput") || tag.Type.ToString().Equals("AnalogInput")
                                                                       || tag.Type.ToString().Equals("UniversalInput") || tag.Type.ToString().Equals("UniversalOutput"));
                                            });
                                        }
                                        DataGridViewCell cell = datagridFunctionBlockOut.Rows[e.RowIndex].Cells[4];
                                        cell.Value = GetNextAddressAndUpdateTextBox(selectedValue, cell, "Outputs", "");
                                        break;
                                }
                                if (!tagList.Contains("-Select Tag Name-"))
                                {
                                    tagList.Insert(0, "-Select Tag Name-");
                                }
                                DataGridViewComboBoxCell comboBoxCellOutputTag = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[actcol].Cells["outputTag"];
                                comboBoxCellOutputTag.DataSource = tagList;
                                comboBoxCellOutputTag.Value = comboBoxCellOutputTag.Items[0];
                            }
                        }
                        else if (e.ColumnIndex == 3)
                        {
                            DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[actcol].Cells["outputTag"];
                            string selectedValue = comboBoxCell.Value?.ToString();
                            if (selectedValue != null && !selectedValue.Equals("-Select Tag Name-"))
                            {
                                datagridFunctionBlockOut.Rows[actcol].Cells["OPTagaddress"].Value = XMProValidator.GetTheAddressFromTag(selectedValue);
                            }

                        }
                        else if (e.ColumnIndex == 4)
                        {
                            TextBox textBox = new TextBox();
                            if (datagridFunctionBlockOut.Rows[actcol].Cells["OPTagaddress"].Value != null)
                            {
                                textBox.Text = datagridFunctionBlockOut.Rows[actcol].Cells["OPTagaddress"].Value.ToString();
                                textBox.Tag = datagridFunctionBlockOut.Rows[actcol].Cells["outdatatype"].Value.ToString();
                                textBox.Name = datagridFunctionBlockOut.Rows[actcol].Cells["output"].Value.ToString();

                            }
                            string outputType = datagridFunctionBlockOut.Rows[e.RowIndex].Cells[2].Value.ToString();
                            ValidateOperand(textBox, datagridFunctionBlockOut.Rows[actcol].Cells["outdatatype"].Value.ToString(), outputType, "Output");
                        }
                    }
                }

            }
            else
            {
                if (enableCellValueChanged)
                {
                    if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                    {
                        datagridFunctionBlockOut.BeginInvoke(new Action(() =>
                        {
                            if (!(comboBoxInstructionType.Text.Equals("Write_Read_PV") && datagridFunctionBlockOut.Rows[0].Cells[4].Value == datagridFunctionBlockIn.Rows[0].Cells[4].Value && e.RowIndex == 0))
                            {
                                DataGridViewCell cell = datagridFunctionBlockOut.Rows[e.RowIndex].Cells[4];
                                cell.Value = "";
                                string selectedValue = datagridFunctionBlockOut.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                                DataGridViewComboBoxCell OutputTags = (DataGridViewComboBoxCell)datagridFunctionBlockOut.Rows[e.RowIndex].Cells[3];
                                OutputTags.DataSource = null;
                                if (selectedValue != null)
                                {
                                    OutputTags.DataSource = OutPutTypeChangedTags(selectedValue.ToString(), e.RowIndex);
                                    OutputTags.Value = OutputTags.Items[0];
                                }
                                if (selectedValue.Equals("Memory Address Variable"))
                                {
                                    cell.Value = GetNextAddressAndUpdateTextBox(selectedValue, cell, "Outputs", comboBoxInstructionType.Text);
                                    OutputTags.Value = OutputTags.Items[0];
                                }
                            }
                        }));
                    }

                    else if (e.ColumnIndex == 4 && e.RowIndex >= 0)
                    {
                        ////Skip the addition of tags for specific instruction after copying all values from first input
                        if (comboBoxInstructionType.Text.Equals("Write_Read_PV") && datagridFunctionBlockOut.Rows[0].Cells[4].Value == datagridFunctionBlockIn.Rows[0].Cells[4].Value)
                            return;
                        datagridFunctionBlockOut.BeginInvoke(new Action(() =>
                        {
                            if (datagridFunctionBlockOut.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                               datagridFunctionBlockOut.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                            {
                                string selectedValue = datagridFunctionBlockOut.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                                string outputType = datagridFunctionBlockOut.Rows[e.RowIndex].Cells[2].Value.ToString();
                                if (selectedValue != null)
                                {
                                    DataGridViewCell cell = datagridFunctionBlockOut.Rows[e.RowIndex].Cells[4];
                                    string error = string.Empty;
                                    bool isvalidateTag = ValidateCellValueOutput(cell, out error, outputType);
                                    errorProviderFB.Clear();
                                    lblerror.Text = "";
                                    if (!isvalidateTag && !string.IsNullOrEmpty(error))
                                    {
                                        errorProviderFB.Clear();
                                        lblerror.Text = "";
                                        // by using these we set common error for dataGridView
                                        int rownumber = e.RowIndex;
                                        errorProviderFB.SetError(datagridFunctionBlockOut, error + " at Output No " + (rownumber + 1));
                                        lblerror.Text = error + " at Output No " + (rownumber + 1);
                                    }

                                }
                                if (comboBoxInstructionType.Text.ToString().Equals("UnPack"))
                                {
                                    string error = string.Empty;
                                    string startAdd = datagridFunctionBlockOut.Rows[0].Cells[4].Value.ToString();
                                    error = (startAdd.Contains(".") || startAdd.Contains("S3")) ? "User Memory address variable." : "No available address found";
                                    if (startAdd != "" && startAdd != "No available address found" && startAdd.Contains(":") && !startAdd.Contains(".") && !startAdd.StartsWith("S3"))
                                    {
                                        string[] AddressParts = startAdd.Split(':');
                                        int AddressPartSecond = int.Parse(AddressParts[1]) + 15;
                                        string LastlogicalAddress = $"{AddressParts[0]}:{AddressPartSecond:000}";
                                        int usedAddCount = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")).Where(T => int.Parse(T.LogicalAddress.Split(':')[1]) >= int.Parse(startAdd.Split(':')[1]) && int.Parse(T.LogicalAddress.Split(':')[1]) <= int.Parse(LastlogicalAddress.Split(':')[1])).Count();
                                        if (usedAddCount > 0)
                                        {
                                            LastlogicalAddress = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")).OrderBy(T => T.LogicalAddress).Last().LogicalAddress;
                                        }
                                        List<XMIOConfig> usedTagList = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")).Where(T => int.Parse(T.LogicalAddress.Split(':')[1]) >= int.Parse(startAdd.Split(':')[1]) && int.Parse(T.LogicalAddress.Split(':')[1]) <= int.Parse(LastlogicalAddress.Split(':')[1])).OrderBy(T => T.LogicalAddress).ToList();
                                        DialogResult dialog = DialogResult.No;
                                        if (usedTagList.Count > 0)
                                        {
                                            dialog = MessageBox.Show("Next Some Address Are Already Used You Want To Change Starting Add", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                            string[] parts = usedTagList.Last().LogicalAddress.Split(':');
                                            int secondPart = int.Parse(parts[1]);
                                            int lastTagAdd = int.Parse(parts[1]) + 1;
                                            string nextUnusedTagAdd = $"{parts[0]}:{lastTagAdd:000}";
                                            if (dialog == DialogResult.OK)
                                            {
                                                datagridFunctionBlockOut.Rows[0].Cells[4].Value = nextUnusedTagAdd;
                                            }
                                        }
                                        if (usedTagList.Count == 0 || dialog == DialogResult.OK)
                                        {
                                            //ONLY FOR SHOWING NEXT 16 ADD IN BRACKET
                                            string[] parts = datagridFunctionBlockOut.Rows[0].Cells[4].Value.ToString().Split(':');
                                            int secondPart = int.Parse(parts[1]);
                                            int lastTagAdd = int.Parse(parts[1]) + 15;
                                            string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                                            if (secondPart + 15 > 1023)
                                            {
                                                lblerror.Text = "Max Tag-Range Excied";
                                                lblerror.ForeColor = Color.Red;
                                                lblerror.Visible = true;
                                            }
                                            else
                                            {
                                                lblerror.Text = "(" + datagridFunctionBlockOut.Rows[0].Cells[4].Value.ToString() + "..." + endAdd + ")";
                                                lblerror.ForeColor = Color.Red;
                                                lblerror.Visible = true;
                                            }
                                        }
                                        isUnPackOutputEdit = true;
                                    }
                                    else if (startAdd != "" || startAdd != "No available address found")
                                    {
                                        errorProviderFB.SetError(datagridFunctionBlockOut, error + " at Output No " + (e.RowIndex + 1));
                                        lblerror.Text = error + " at Output No " + (e.RowIndex + 1);
                                    }
                                }
                                if (comboBoxInstructionType.Text.ToString().Equals("UnPack") && edit)
                                {
                                    isPackOutputEdit = true;
                                }
                            }
                        }));
                    }
                }
            }
        }
        private bool ValidateCellValueOutput(DataGridViewCell cell, out string error, string outputTypeText)
        {
            bool validationSuccessful;
            string dataType = datagridFunctionBlockOut.Rows[cell.RowIndex].Cells[1].Value.ToString();
            string outputType = outputTypeText;
            string address = cell.Value.ToString();
            InstructionTypeDeserializer selectedInstruction = ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem);
            if (selectedInstruction.Text == "EXP") dataType = "Real";
            string Instruction = comboBoxInstructionType.SelectedItem.ToString();
            if (string.IsNullOrEmpty(address))
            {
                if (cell.RowIndex == 0)
                {
                    validationSuccessful = false;
                    error = "Output address cannot be blank";
                    return validationSuccessful;
                }
                else
                {
                    error = String.Empty;
                    validationSuccessful = true;

                }
            }
            //Adding check for the Pack instruction for checking the first output as word
            else if (selectedInstruction.Text == "Pack")
            {
                error = "Invalid Word address";
                validationSuccessful = true;
                XMIOConfig outputTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == cell.Value.ToString()).FirstOrDefault();
                if (outputTag != null && outputTag.Label != "Word")
                {
                    validationSuccessful = false;
                    return validationSuccessful;
                }
                else if (outputTag == null)
                {
                    outputTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == cell.Value.ToString() + ".00").FirstOrDefault();
                    if (outputTag != null)
                    {
                        validationSuccessful = true;
                        return validationSuccessful;
                    }
                }
                else
                {
                    if (address.IsValidWordAddress())
                    {
                        validationSuccessful = address.IsValidOutputWordAddress(outputType, xm.LoadedProject.Tags.Where(R => R.Type.ToString().Contains("Output")).ToList());
                        if (validationSuccessful)
                        {
                            if (xm.LoadedProject.Tags.Where(R => R.LogicalAddress == cell.Value.ToString() && R.IoList == Core.Types.IOListType.Default && !R.Tag.Contains("WRITE")).Count() > 0)
                            {
                                validationSuccessful = false;
                                return validationSuccessful;
                            }
                        }
                    }
                }
            }
            //Adding Check for the UnPack Instruction to adding First Output as a bit address
            else if (selectedInstruction.Text == "UnPack")
            {
                error = "Invalid Bit Address";
                validationSuccessful = address.IsValidOutputBitAddress(outputType, xm.LoadedProject.Tags.Where(R => R.LogicalAddress.Contains(".") && R.Type.ToString().Contains("Output")).ToList());
            }
            else
                switch (dataType)
                {
                    case "Bool":
                    // Timer data types
                    case "TON":
                    case "RTON":
                    case "TOFF":
                    case "TP":
                    // Counter data types
                    case "CTU":
                    case "CTD":
                        error = "Invalid Bit Address";
                        validationSuccessful = address.IsValidOutputBitAddress(outputType, xm.LoadedProject.Tags.Where(R => (R.LogicalAddress.Contains(".") || R.Mode == "Digital") && R.Type.ToString().Contains("Output")).ToList());
                        break;
                    case "Real":
                        if (outputType == "Memory Address Variable" || outputType == "On-board" || xm.LoadedProject.CPUDatatype.Equals("Real"))
                        {
                            error = "Invalid Word address for Real data type";
                            validationSuccessful = address.IsValidRealWordAddress();
                        }
                        else
                        {
                            error = "Invalid OutPut Type for Real data type";
                            validationSuccessful = false;
                        }
                        break;
                    case "DINT":
                        if (outputType == "Memory Address Variable")
                        {
                            error = "Invalid Word address for DINT data type";
                            validationSuccessful = address.IsValidDINTWordAddress();
                        }
                        else
                        {
                            error = "Invalid OutPut Type for DINT data type";
                            validationSuccessful = false;
                        }
                        break;
                    case "UDINT":
                        if (outputType == "Memory Address Variable")
                        {
                            error = "Invalid Word address for UDINT data type";
                            validationSuccessful = address.IsValidUDINTWordAddress();
                        }
                        else
                        {
                            error = "Invalid OutPut Type for UDINT data type";
                            validationSuccessful = false;
                        }
                        break;
                    case "Byte":
                    case "Double Word":
                    case "Int":
                        if (outputType == "Memory Address Variable")
                        {
                            error = $"Invalid {dataType} address for {dataType} data type";
                            validationSuccessful = address.IsValidByteAddress(dataType);
                        }
                        else
                        {
                            error = $"Invalid OutPut Type for {dataType} data type";
                            validationSuccessful = false;
                        }
                        break;
                    default:
                        error = "Invalid " + dataType + " address";
                        if (address.IsValidWordAddress(dataType))
                        {
                            validationSuccessful = address.IsValidOutputWordAddress(outputType, xm.LoadedProject.Tags.Where(R => R.Type.ToString().Contains("Output")).ToList());
                            if (validationSuccessful)
                            {
                                if (xm.LoadedProject.Tags.Where(R => R.LogicalAddress == cell.Value.ToString() && R.IoList == Core.Types.IOListType.Default && !R.Tag.Contains("WRITE")).Count() > 0)
                                {
                                    validationSuccessful = false;
                                }
                            }
                        }
                        else
                        {
                            validationSuccessful = false;
                        }
                        break;
                }

            if (!validationSuccessful)
            {
                if (udfbname != "")
                {
                    UDFBInfo uDFB = xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == udfbname).FirstOrDefault();
                    if (uDFB.UDFBlocks.Where(b => b.Text == cell.Value.ToString() && b.DataType == dataType.ToString()).Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }

            }
            ///Adding Logic For the Checking Tags Location
            string CurrentLogicalBlock = xm.CurrentScreen.Split('#')[1];
            //check is a logic block or UDFB
            bool isLogicBlock = xm.LoadedProject.Blocks.Where(t => t.Type.Equals("LogicBlock")).Any(t => t.Name.Equals(CurrentLogicalBlock.Split(' ')[0]));
            bool isUDFBBlock = XMPS.Instance.LoadedProject.UDFBInfo.Any(t => t.UDFBName.Equals(CurrentLogicalBlock.Split(' ')[0]));

            string output1Address = cell.Value.ToString();
            XMIOConfig EnteredTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == output1Address).FirstOrDefault();
            if (EnteredTag != null)
            {
                if (isLogicBlock && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !XMPS.Instance.PlcModels.Contains(EnteredTag.Model.Split('_')[0]))))
                {
                    if (EnteredTag.Model != "User Defined Tags" && EnteredTag.Model != "")
                    {
                        error = "Used Tag is Not Found in Current Logic Block";
                        return false;
                    }
                }
                else if (isUDFBBlock && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !XMPS.Instance.PlcModels.Contains(EnteredTag.Model.Split('_')[0]))))
                {
                    string UdfbName = CurrentLogicalBlock.Split(' ')[0];
                    if (EnteredTag.Model.ToString() != UdfbName + " Tags")
                    {
                        error = "Used Tag is Not Found in Current UDFB Block";
                        return false;
                    }
                }
            }
            var addedDataType = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == cell.Value.ToString()).FirstOrDefault();
            if (addedDataType != null)
            {
                bool fbDataType = dataType == "TON" || dataType == "TOFF" || dataType == "RTON" || dataType == "TP" || dataType == "CTU" || dataType == "CTD";

                if (!fbDataType && selectedInstruction.Text != "Pack" && selectedInstruction.Text != "UnPack")
                {
                    if (addedDataType.Model != "" || (addedDataType.Label == dataType))
                    {
                        error = string.Empty;
                    }
                    else
                    {
                        error = "Data type not matching need to select " + dataType + " and this logical address has datatype " + addedDataType.Label;
                    }
                }
            }
            return validationSuccessful;
        }
        private bool DefaultAddedTag(string LogicalAddress)
        {
            if (xm.LoadedProject.Tags.Where(T => T.Tag == LogicalAddress && T.ReadOnly && T.IoList == Core.Types.IOListType.Default).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnFBAddnew_Click(object sender, EventArgs e)
        {
            if (comboBoxInstructionType.Text == "UDFB")
            {
                string currentScreenName = XMPS.Instance.CurrentScreen.Split('#')[0];
                if (currentScreenName.Contains("UDFLadderForm") && comboBoxInstructionType.Text.Equals("UDFB"))
                {
                    MessageBox.Show($"Not allowed to add UDFB instruction inside the UDFB logic", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ValidEntry()) return;

                if (lblerror.Text.Length != 0)
                {
                    MessageBox.Show("Resolve all the issues before adding the details ! Open issue : " + lblerror.ToString(), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (hasLadders && !checkBoxEnable.Checked)
                {
                    if (MessageBox.Show("Not selecting Enable will remove all Ladder Components added, are you sure you want to continue", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }
                }
                ApplicationRung AppRecs = new ApplicationRung();
                AppRecs.WindowName = xm.CurrentScreen.ToString();
                AppRecs.Name = AppRecs.WindowName;
                AppRecs.LineNumber = 999;
                string optype = "";
                int i = 0;
                Outputs.Clear();
                Inputs.Clear();
                foreach (DataGridViewRow dr in datagridFunctionBlockOut.Rows)
                {
                    if (dr.Cells["OPTagaddress"].Value != null && dr.Cells[2].EditedFormattedValue?.ToString() != "")
                    {
                        string address = dr.Cells["OPTagaddress"].Value.ToString();
                        XMIOConfig tag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == address).FirstOrDefault();
                        if (tag == null)
                        {
                            string dataType = dr.Cells["outdatatype"].Value.ToString();
                            bool isadded = CheckAndAddTag(address, dataType, dr.Cells[0].Value.ToString());
                            if (isadded)
                            {
                                XMIOConfig newTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == address).FirstOrDefault();
                                //Outputs.Add(new Tuple<int, string>(i + 1, newTag != null ? newTag.LogicalAddress : "-"));
                                AppRecs.Outputs.Add($"Output{i + 1}", newTag != null ? newTag.LogicalAddress : "-");
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            //Outputs.Add(new Tuple<int, string>(i + 1, dr.Cells["OPTagaddress"].Value != null ? dr.Cells["OPTagaddress"].Value.ToString() : "-"));
                            AppRecs.Outputs.Add($"Output{i + 1}", dr.Cells["OPTagaddress"].Value != null ? dr.Cells["OPTagaddress"].Value.ToString() : "-");
                        }
                        if (i == 0) optype = dr.Cells[2].EditedFormattedValue != null ? dr.Cells[2].EditedFormattedValue?.ToString() : "-";
                    }
                    i++;
                }
                i = 0;
                foreach (DataGridViewRow dr in datagridFunctionBlockIn.Rows)
                {
                    if (dr.Cells["Operand_Type"].Value != null)
                    {
                        if (dr.Cells["Operand_Type"].Value.ToString() == "Negation Operand")
                        {
                            string address = dr.Cells["Tag_Address"].Value.ToString();
                            XMIOConfig tag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == address).FirstOrDefault();
                            if (tag == null)
                            {
                                string dataType = dr.Cells["Datatype"].Value.ToString();

                                TextBox txtaddresscheck = new TextBox();
                                txtaddresscheck.Visible = false;
                                txtaddresscheck.Text = address;
                                txtaddresscheck.Tag = dataType;

                                if (!ValidateAddressOperand(txtaddresscheck, out string chkerror, dataType, "Input"))
                                {
                                    MessageBox.Show("Invalid " + dataType + " Address " + address, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                bool isadded = CheckAndAddTag(address, dataType, dr.Cells[0].Value.ToString());
                                if (isadded)
                                {
                                    XMIOConfig newTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == address).FirstOrDefault();
                                    AppRecs.Inputs.Add($"Input{i + 1}", newTag != null ? "~" + newTag.LogicalAddress : "-");
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                AppRecs.Inputs.Add($"Input{i + 1}", dr.Cells["Tag_Address"].Value != null ? "~" + dr.Cells["Tag_Address"].Value.ToString() : "-");
                            }
                        }

                        else if (dr.Cells["Operand_Type"].Value.ToString() == "Numeric Operand")
                        {
                            AppRecs.Inputs.Add($"Input{i + 1}", dr.Cells["Tag_Address"].Value != null ? dr.Cells["Tag_Address"].Value.ToString() : "-");
                        }
                        else
                        {
                            string address = dr.Cells["Tag_Address"].Value.ToString();

                            XMIOConfig tag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == address).FirstOrDefault();
                            if (tag == null)
                            {
                                string dataType = dr.Cells["Datatype"].Value.ToString();
                                TextBox txtaddresscheck = new TextBox();
                                txtaddresscheck.Visible = false;
                                txtaddresscheck.Text = address;
                                txtaddresscheck.Tag = dataType;

                                if (!ValidateAddressOperand(txtaddresscheck, out string chkerror, dataType, "Input"))
                                {
                                    MessageBox.Show("Invalid " + dataType + " Address " + address, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                bool isadded = CheckAndAddTag(address, dataType, dr.Cells[0].Value.ToString());
                                if (isadded)
                                {
                                    XMIOConfig newTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == address).FirstOrDefault();
                                    AppRecs.Inputs.Add($"Input{i + 1}", newTag != null ? newTag.LogicalAddress : "-");
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                AppRecs.Inputs.Add($"Input{i + 1}", dr.Cells["Tag_Address"].Value != null ? dr.Cells["Tag_Address"].Value.ToString() : "-");
                            }
                        }
                    }
                    i++;
                }

                AppRecs.OpCodeNm = comboBoxInstruction.SelectedItem.ToString();
                AppRecs.OpCode = "9999";
                AppRecs.OutPutType_NM = optype;
                AppRecs.Enable = checkBoxEnable.Checked ? "Enabled" : "-";
                AppRecs.OutputType = OutputType.List.Find(T => T.Text == optype).ID.ToString();
                AppRecs.DataType_Nm = comboBoxDataType.SelectedItem.ToString();
                AppRecs.DataType = $"{((DataType)comboBoxDataType.SelectedItem).ID:X4}";
                AppRecs.TC_Name = (TcName != null && TcName != "" && TcName != "-" && TcName.StartsWith("FB")) ? TcName : IncreaseTimerCounter("FB");
                //xm.LoadedProject.LogicRungs.Remove(AppRecs);
                xm.LoadedProject.LogicRungs.RemoveAll(d => d.TC_Name == AppRecs.TC_Name && d.OpCode == AppRecs.OpCode);
                xm.LoadedProject.AddRung(AppRecs);
            }
            else
            {
                if (!ValidateRows())
                {
                    MessageBox.Show("Please resolve the errors first", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (hasLadders && !checkBoxEnable.Checked)
                {
                    if (MessageBox.Show("Not selecting Enable will remove all Ladder Components added, are you sure you want to continue", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (cmbTopic.Visible)
                {
                    if (cmbTopic.SelectedIndex == 0)
                    {
                        MessageBox.Show("Please select valid topic", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (!CheckDatatype())
                {
                    if (!comboBoxInstructionType.Text.StartsWith("Timer") && !comboBoxInstructionType.Text.StartsWith("Counter") && !comboBoxInstructionType.Text.Contains("Pack"))
                    {
                        MessageBox.Show("Data Type selected is not maching with selected Tag", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (datagridFunctionBlockOut.Rows.Count > 1)
                {
                    var Logicname = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == (string)datagridFunctionBlockOut.Rows[1].Cells[4].Value && !d.Retentive).FirstOrDefault();
                    if (Logicname != null && this.comboBoxDataType.Text == "RTON")
                    {
                        DialogResult dialogResult = MessageBox.Show("Tag logical address is already used and not retentive", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Logicname.Retentive = true;
                            Logicname.RetentiveAddress = CommonFunctions.GetRetentiveAddress(datagridFunctionBlockOut.Rows[1].Cells[4].Value.ToString(), "");
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            return;
                        }

                    }
                }

                if (CheckAndAddInputOutputTags())
                {
                    //Addding Condition for the Pack Instruction to Fix next 15 address by using Current Address
                    if ((comboBoxInstructionType.SelectedItem.ToString() == "Pack" && !isPackOutputEdit && (isPackInputEdit || !edit))
                        || (comboBoxInstructionType.SelectedItem.ToString() == "Pack" && (isPackOutputEdit || isPackInputEdit)))
                    {
                        string firstLogicalAdd = datagridFunctionBlockIn.Rows[0].Cells[4].Value.ToString();
                        if (Convert.ToInt32(firstLogicalAdd.Split(':')[1]) + 15 > 1023)
                        {
                            lblerror.Text = "Max Tag Limit Exceed";
                            return;
                        }
                        XMIOConfig firstTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == firstLogicalAdd).FirstOrDefault();
                        if (firstTag == null)
                        {
                            string tc_Name = "";
                            string extraName = string.Empty;
                            if (edit)
                            {
                                LadderElement ld = LadderDesign.ClickedElement;
                                tc_Name = !ld.Attributes["TCName"].ToString().StartsWith("PK") ? IncreaePackInstructorCounter("Pack") : ld.Attributes["TCName"].ToString();
                                int usedTagCount = xm.LoadedProject.Tags.Where(T => T.ActualName != null && T.ActualName.StartsWith(tc_Name)).Any() ? xm.LoadedProject.Tags.Where(T => T.ActualName != null && T.ActualName.StartsWith(tc_Name)).Count() : 0;
                                int totalNo = usedTagCount / 16;
                                if (totalNo > 0)
                                    extraName = "_" + totalNo.ToString();
                                _curRungTcName = tc_Name;
                                tc_instuction = tc_Name;
                            }
                            TagsUserControl userControl = new TagsUserControl(firstLogicalAdd, tc_Name + extraName, "Pack");
                        }
                        else
                        {
                            MessageBox.Show("Tag is Already Used", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    //Adding Condition for the Adding Next 15 Boolean tag for the output in UnPack Instruction
                    if ((comboBoxInstructionType.SelectedItem.ToString() == "UnPack" && !isUnPackInputEdit && (isUnPackOutputEdit || !edit))
                        || (comboBoxInstructionType.SelectedItem.ToString() == "UnPack" && (isUnPackInputEdit && isUnPackOutputEdit)))
                    {
                        string firstLogicalAdd = datagridFunctionBlockOut.Rows[0].Cells[4].Value.ToString();
                        if (Convert.ToInt32(firstLogicalAdd.Split(':')[1]) + 15 > 1023)
                        {
                            lblerror.Text = "Max Tag Limit Exceed";
                            return;
                        }
                        XMIOConfig firstTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == firstLogicalAdd).FirstOrDefault();
                        if (firstTag == null)
                        {
                            string tc_Name = "";
                            string extraName = string.Empty;
                            if (edit)
                            {
                                LadderElement ld = LadderDesign.ClickedElement;
                                tc_Name = !ld.Attributes["TCName"].ToString().StartsWith("UPK") ? IncreaePackInstructorCounter("UnPack") : ld.Attributes["TCName"].ToString();
                                int usedTagCount = xm.LoadedProject.Tags.Where(T => T.ActualName != null && T.ActualName.StartsWith(tc_Name)).Any() ? xm.LoadedProject.Tags.Where(T => T.ActualName != null && T.ActualName.StartsWith(tc_Name)).Count() : 0;
                                int totalNo = usedTagCount / 16;
                                if (totalNo > 0)
                                    extraName = "_" + totalNo.ToString();
                                _curRungTcName = tc_Name;
                                tc_instuction = tc_Name;
                            }
                            TagsUserControl userControl = new TagsUserControl(firstLogicalAdd, tc_Name + extraName, "UnPack");
                        }
                        else
                        {
                            MessageBox.Show("Tag is Already Used", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    string type = comboBoxInstructionType.SelectedItem.ToString();
                    if (!edit)
                    {
                        if (Counters.ContainsKey(((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).Text))
                        {
                            string selectedInstruction = ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).Text;

                            var currentOpCode = Convert.ToInt32(((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).ID, 16) +
                                            Convert.ToInt32(((DataTypeDesilizer)comboBoxDataType.SelectedItem).ID, 16);
                            string currentDcode = null;
                            string newopcode = currentDcode is null ? $"{currentOpCode:X4}" : "0" + currentDcode;
                            bool safe = CheckCountOfFunctionBlock(newopcode);
                            if (!safe)
                            {
                                MessageBox.Show("Maximum Number of Rungs of Exceeds for this instructions for this type: " + selectedInstruction.ToString(), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                AddRows(Linenumber, _curRungTcName, edit);
                            }
                        }
                        else
                        {
                            AddRows(Linenumber, _curRungTcName, edit);
                        }
                    }
                    else
                    {
                        AddRows(Linenumber, _curRungTcName, edit);
                    }
                }
                else
                {
                    return;
                }

            }

            xm.MarkProjectModified(true);
            this.ParentForm.Close();
            this.ParentForm.DialogResult = DialogResult.OK;
        }

        private bool CheckforDuplicateLogicalAddress()
        {
            List<string> listParts = new List<string>();

            DataGridViewRowCollection coll = datagridFunctionBlockIn.Rows;
            for (int i = 0; i < coll.Count; i++)
            {
                if (coll[i].Cells[4].Value != null && coll[i].Cells[4].Value.ToString() != "" && coll[i].Cells[3].Value.ToString() == "-Select Tag Name-")
                    listParts.Add(coll[i].Cells[4].Value.ToString());
            }

            DataGridViewRowCollection colO = datagridFunctionBlockOut.Rows;
            for (int i = 0; i < colO.Count; i++)
            {
                if (colO[i].Cells[4].Value != null && colO[i].Cells[4].Value.ToString() != "")
                    listParts.Add(colO[i].Cells[4].Value.ToString());
            }
            // Return the value
            return listParts.Count > listParts.Distinct().Count() ? false : true;
        }

        private bool ValidateRows()
        {
            string Instruction = comboBoxInstructionType.SelectedItem.ToString();
            string Operation = ((InstructionTypeDeserializer)(comboBoxInstruction.SelectedItem)).Text;

            InstructionTypeDeserializer instruction = xm.instructionsList.FirstOrDefault(t => t.Text.Equals(Operation));

            foreach (DataGridViewRow row in datagridFunctionBlockIn.Rows)
            {
                bool isRequired = instruction.InputsOutputs.FirstOrDefault(t => t.Id == (row.Index + 1) && t.Type.Equals("Input")).IsRequired;
                if (isRequired && !instruction.Text.Equals("MES_PID") && (datagridFunctionBlockIn.Rows[row.Index].Cells[4].Value == null || string.IsNullOrEmpty(datagridFunctionBlockIn.Rows[row.Index].Cells[4].Value.ToString())))
                {
                    errorProviderFB.SetError(datagridFunctionBlockIn, $"Input no {row.Index + 1} is compulsory for this instruction");
                    lblerror.Text = $"Input no {row.Index + 1} is compulsory for this instruction";
                    return false;
                }

                string selectedValue = "";
                string OpreandType = "";
                if (datagridFunctionBlockIn.Rows[row.Index].Cells[4].Value != null && selectedValue != null)
                {
                    selectedValue = datagridFunctionBlockIn.Rows[row.Index].Cells[4].Value.ToString();
                    OpreandType = datagridFunctionBlockIn.Rows[row.Index].Cells[2].Value.ToString();
                    DataGridViewComboBoxCell InputTags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[row.Index].Cells[3];
                    if (OpreandType == "Numeric Operand")
                    {
                        DataGridViewCell cell = datagridFunctionBlockIn.Rows[row.Index].Cells[4];
                        string error = string.Empty;
                        bool isvalidateTag = ValidateCellValue(cell, out error, OpreandType);
                        errorProviderFB.Clear();
                        lblerror.Text = "";
                        if (comboBoxInstruction.Text == "DIV" && datagridFunctionBlockIn.Rows[row.Index].Cells[0].Value.ToString() == "Input 2" && double.Parse(datagridFunctionBlockIn.Rows[row.Index].Cells[4].Value.ToString()) == 0)
                        {
                            errorProviderFB.SetError(datagridFunctionBlockIn, " Diviser can't be 0 for " + datagridFunctionBlockIn.Rows[row.Index].Cells[0].Value.ToString());
                            return false;
                        }
                        if (!isvalidateTag && !string.IsNullOrEmpty(error))
                        {
                            errorProviderFB.Clear();
                            lblerror.Text = "";
                            int rownumber = row.Index;
                            errorProviderFB.SetError(datagridFunctionBlockIn, error + " at Input No " + (rownumber + 1));
                            lblerror.Text = error;
                            return false;
                        }
                    }
                    else if (OpreandType == "Negation Operand" || OpreandType == "Normal Operand" || OpreandType == "Select Operand Type")
                    {
                        DataGridViewCell cell = datagridFunctionBlockIn.Rows[row.Index].Cells[4];
                        string error = string.Empty;
                        bool isvalidateTag = ValidateCellValue(cell, out error, OpreandType);
                        errorProviderFB.Clear();
                        lblerror.Text = "";
                        if (!cell.Value.ToString().Contains(":") && comboBoxInstruction.Text == "Pack")
                        {
                            lblerror.Text = "Please add valid tag for input";
                            return false;
                        }
                        if (!isvalidateTag && !string.IsNullOrEmpty(error))
                        {
                            errorProviderFB.Clear();
                            lblerror.Text = "";
                            int rownumber = row.Index;
                            errorProviderFB.SetError(datagridFunctionBlockIn, error + " at Input No " + (rownumber + 1));
                            lblerror.Text = error;
                            return false;
                        }
                    }
                }
            }

            foreach (DataGridViewRow row in datagridFunctionBlockOut.Rows)
            {
                bool isRequired = instruction.InputsOutputs.FirstOrDefault(t => t.Id == (row.Index + 1) && t.Type.Equals("Output")).IsRequired;
                if (isRequired && (datagridFunctionBlockOut.Rows[row.Index].Cells[4].Value == null || string.IsNullOrEmpty(datagridFunctionBlockOut.Rows[row.Index].Cells[4].Value.ToString())))
                {
                    errorProviderFB.SetError(datagridFunctionBlockOut, $"Output no {row.Index + 1} is compulsory for this instruction");
                    lblerror.Text = $"Output no {row.Index + 1} is compulsory for this instruction";
                    return false;
                }
                string selectedValue = "";
                string outputType = "";
                if (datagridFunctionBlockOut.Rows[row.Index].Cells[4].Value != null && selectedValue != null)
                {
                    selectedValue = datagridFunctionBlockOut.Rows[row.Index].Cells[4].Value.ToString();
                    outputType = datagridFunctionBlockOut.Rows[row.Index].Cells[2].Value.ToString();
                    DataGridViewCell cell = datagridFunctionBlockOut.Rows[row.Index].Cells[4];
                    string error = string.Empty;
                    if (row.Index == 1 && comboBoxInstruction.Text == "MQTT Publish" || comboBoxInstruction.Text == "MQTT Subscribe")
                    {
                        if (cell.Value != null && cell.Value.ToString() != "" && edit)
                        {
                            return true;
                        }
                    }
                    errorProviderFB.Clear();
                    lblerror.Text = "";
                    //check if for Unapack instruction [Not allowed to used udfb variable name]
                    if (!selectedValue.Contains(":") && comboBoxInstruction.Text == "UnPack")
                    {
                        lblerror.Text = "Please add valid tag for output";
                        return false;
                    }
                    bool isvalidateTag = ValidateCellValueOutput(cell, out error, outputType);

                    if (!isvalidateTag && !string.IsNullOrEmpty(error))
                    {
                        errorProviderFB.Clear();
                        lblerror.Text = "";
                        int rownumber = row.Index;
                        errorProviderFB.SetError(datagridFunctionBlockOut, error + " at Output No " + (rownumber + 1));
                        lblerror.Text = error;
                        return false;
                    }

                }
                if (row.Index == 0 && (selectedValue == null || selectedValue == ""))
                {
                    errorProviderFB.Clear();
                    lblerror.Text = "";
                    int rownumber = row.Index;
                    errorProviderFB.SetError(datagridFunctionBlockOut, "Output value cannot be null at Output No" + (rownumber + 1));
                    lblerror.Text = "Output value cannot be null at Output No" + (rownumber + 1);
                    return false;
                }
                if (row.Index == 1 && comboBoxInstructionType.Text == "TimerRTON" && (selectedValue == null || selectedValue == ""))
                {
                    errorProviderFB.Clear();
                    lblerror.Text = "";
                    int rownumber = row.Index;
                    errorProviderFB.SetError(datagridFunctionBlockOut, "Output value cannot be null at Output No" + (rownumber + 1));
                    lblerror.Text = "Output value cannot be null at Output No" + (rownumber + 1);
                    return false;
                }
            }
            return true;
        }

        private void AddRows(int linenumber, string counterValue, bool Edit)
        {
            bool insertafter = false;
            try
            {
                if (!Edit && linenumber == 0)
                {
                    var getlinenum = xm.LoadedProject.LogicRungs.Where(R => R.WindowName == xm.CurrentScreen.ToString());
                    if (getlinenum.Count() == 0)
                        linenumber = 1;
                    else
                        linenumber = getlinenum.Max(R => R.LineNumber) + 1;
                }
                else if (!Edit)
                {
                    insertafter = true;
                    var ApplicationRecsE = xm.LoadedProject.LogicRungs.Where(d => d.WindowName == xm.CurrentScreen.ToString() && d.LineNumber > linenumber).OrderBy(o => o.LineNumber);
                    foreach (ApplicationRung ApRec in ApplicationRecsE)
                    {
                        ApRec.LineNumber += 1;
                    }
                    linenumber = linenumber + 1;
                }
                string instruction = ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).Text;
                var opcode = Convert.ToInt32(((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).ID, 16) +
                                Convert.ToInt32(((DataTypeDesilizer)comboBoxDataType.SelectedItem).ID, 16);


                string Dcode = null;
                if (comboBoxInstructionType.Text.Contains("DataConversion"))
                {
                    Dcode = (((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).ID.Substring(3, 1) + ((DataTypeDesilizer)comboBoxDataType.SelectedItem).ID.Substring(2, 1)) + ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).ID.Substring(6, 1).ToString();
                    if (((DataTypeDesilizer)comboBoxDataType.SelectedItem).Text == "DINT")
                    {
                        Dcode = null;
                        //Dcode = (((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).ID.Substring(3, 1) + ((DataTypeDesilizer)comboBoxDataType.SelectedItem).ID.Substring(2, 2)) + ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).ID.Substring(6, 1).ToString();
                        Dcode = (((DataTypeDesilizer)comboBoxDataType.SelectedItem).ID.Substring(2, 2) + ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).ID.Substring(6, 1).ToString());
                    }
                }

                ApplicationRung AppRecs = new ApplicationRung();
                AppRecs.WindowName = xm.CurrentScreen.ToString();
                AppRecs.Name = AppRecs.WindowName;
                AppRecs.LineNumber = linenumber;
                if (Edit)
                {
                    counterValue = tc_instuction;
                }
                if (counterValue == "" || counterValue == null)
                {
                    AppRecs.TC_Name = IncreaseTimerCounter(instruction);
                    //for Adding TC_Name for the Pack Instruction FB
                    if (comboBoxInstruction.Text.Equals("Pack"))
                    {
                        AppRecs.TC_Name = IncreaePackInstructorCounter(instruction);
                    }
                    if (comboBoxInstruction.Text.Equals("UnPack"))
                    {
                        AppRecs.TC_Name = IncreaePackInstructorCounter(instruction);
                    }
                    if (Edit & AppRecs.TC_Name != "-")
                    {
                        ResetTimerCounterIndex(xm.LoadedProject.LogicRungs.Where(R => R.WindowName == xm.CurrentScreen.ToString() && R.LineNumber == Linenumber).Select(R1 => R1.OpCodeNm).FirstOrDefault(), xm.LoadedProject.LogicRungs.Where(R => R.WindowName == xm.CurrentScreen.ToString() && R.LineNumber == Linenumber).Select(R1 => R1.TC_Name).FirstOrDefault());
                    }
                }
                else
                {
                    string oldOPcode = sel_opcode;
                    string newcode = Dcode is null ? $"{opcode:X4}" : "0" + Dcode;
                    if (newcode == oldOPcode)
                    {
                        string newTcName = IncreaseTimerCounter(instruction, false);
                        if (newTcName != "-")
                            AppRecs.TC_Name = counterValue;
                        //Addition Additional Checks for the edit Pack and Unpack Instruction
                        else if (newTcName == "-" && (oldOPcode == "0390" || oldOPcode == "03A2" || oldOPcode == "03B2" || oldOPcode == "03C2"))
                            AppRecs.TC_Name = counterValue;
                        else
                            AppRecs.TC_Name = "-";
                    }
                    else
                    {
                        if (instruction.Equals("Pack") || instruction.Equals("UnPack"))
                            AppRecs.TC_Name = IncreaePackInstructorCounter(instruction);
                        else
                            AppRecs.TC_Name = IncreaseTimerCounter(instruction);
                    }
                }  // -----------> Adding of new input and new Output
                AppRecs.OutPutType_NM = (string)datagridFunctionBlockOut.Rows[0].Cells[2].FormattedValue;
                AppRecs.OutputType = $"{(OutputType.List.FirstOrDefault(item => item.ToString() == datagridFunctionBlockOut.Rows[0].Cells[2].FormattedValue.ToString())?.ID ?? 0):X2}";
                AppRecs.DataType_Nm = ((DataTypeDesilizer)comboBoxDataType.SelectedItem).Text.ToString();
                AppRecs.DataType = $"{Convert.ToInt32(((DataTypeDesilizer)comboBoxDataType.SelectedItem).ID, 16):X4}";
                AppRecs.Enable = checkBoxEnable.Checked ? "Enabled" : "-";

                //Adding Random Hardcoded Values For Mqtt FB
                if (instruction.Contains("MQTT Publish"))
                {
                    AppRecs.Outputs.Add("Output1", datagridFunctionBlockOut.Rows[0].Cells[4].Value.ToString());
                    AppRecs.Inputs.Add("Input1", "PUB.Topic");
                    AppRecs.Inputs.Add("Input2", "PUB.Payload");
                    AppRecs.Inputs.Add("Input3", "PUB.Qos");
                    AppRecs.Inputs.Add("Input4", "PUB.RetainFlag");
                    AppRecs.Inputs.Add("Input5", "-");
                    AppRecs.OpCodeNm = comboBoxInstruction.Text.ToString();
                    AppRecs.OpCode = Dcode is null ? $"{opcode:X4}" : "0" + Dcode;
                    AppRecs.Outputs.Add("Output2", cmbTopic.SelectedValue.ToString());
                    AppRecs.Outputs.Add("Output3", "-");
                    if (!edit || sel_opcode != AppRecs.OpCode) AppRecs.TC_Name = IncreaseMQTTCounter(instruction);
                }
                else if (instruction.Contains("MQTT Subscribe"))
                {
                    AppRecs.Outputs.Add("Output1", datagridFunctionBlockOut.Rows[0].Cells[4].Value.ToString());
                    AppRecs.Inputs.Add("Input1", "SUB.Topic");
                    AppRecs.Inputs.Add("Input2", "SUB.Qos");
                    AppRecs.Inputs.Add("Input3", "-");
                    AppRecs.Inputs.Add("Input4", "-");
                    AppRecs.Inputs.Add("Input5", "-");
                    AppRecs.OpCodeNm = comboBoxInstruction.Text.ToString();
                    AppRecs.OpCode = Dcode is null ? $"{opcode:X4}" : "0" + Dcode;
                    AppRecs.Outputs.Add("Output2", cmbTopic.SelectedValue.ToString());
                    AppRecs.Outputs.Add("Output3", "-");
                    if (!edit || sel_opcode != AppRecs.OpCode) AppRecs.TC_Name = IncreaseMQTTCounter(instruction);
                }
                else if (instruction.Equals("NULL"))
                {
                    if ((datagridFunctionBlockIn.RowCount == 0))
                        AppRecs.Inputs.Add("Input1", "NULL");
                    else
                        if (datagridFunctionBlockIn.Rows[0].Cells[4].Value.ToString() == "" || datagridFunctionBlockIn.Rows[0].Cells[4].Value.ToString() == "NULL")
                        AppRecs.Inputs.Add("Input1", "NULL");
                    else
                    {
                        AppRecs.Inputs.Add("Input1", datagridFunctionBlockIn.Rows[0].Cells[4].Value.ToString());
                        //AppRecs.Inputs.Add("Input2", "NULL");
                    }
                    for (int i = 0; i < datagridFunctionBlockOut.Rows.Count; i++)
                    {
                        string key = $"Output{i + 1}";
                        AppRecs.Outputs.Add(key, GetCellValue(datagridFunctionBlockOut, i, 4));
                    }
                    AppRecs.OpCodeNm = comboBoxInstruction.Text.ToString();
                    // Set final OpCode with fallback to Dcode
                    AppRecs.OpCode = Dcode is null ? $"{opcode:X4}" : "0" + Dcode;
                }
                else if (comboBoxInstructionType.Text.Equals("ReadProperty"))
                {
                    for (int i = 0; i < datagridFunctionBlockIn.Rows.Count; i++)
                    {
                        string key = $"Input{i + 1}";
                        AppRecs.Inputs.Add(key, GetInputValue(datagridFunctionBlockIn, i));
                    }
                    AppRecs.OpCodeNm = comboBoxInstruction.Text.Equals("MES_PID") ? IncreasePIDInstructionCounter() : comboBoxInstruction.Text.ToString();
                    AppRecs.OpCode = Dcode is null ? $"{opcode:X4}" : "0" + Dcode;

                    for (int i = 0; i < datagridFunctionBlockOut.Rows.Count; i++)
                    {
                        string key = $"Output{i + 1}";
                        object outputValue = GetCellValue(datagridFunctionBlockOut, i, 4);
                        AppRecs.Outputs.Add(key, outputValue.Equals("-") ? "A5:999" : outputValue);
                    }
                }
                else
                {
                    for (int i = 0; i < datagridFunctionBlockOut.Rows.Count; i++)
                    {
                        string key = $"Output{i + 1}";
                        AppRecs.Outputs.Add(key, GetCellValue(datagridFunctionBlockOut, i, 4));
                    }

                    AppRecs.OpCodeNm = comboBoxInstruction.Text.Equals("MES_PID") ? IncreasePIDInstructionCounter() : comboBoxInstruction.Text.ToString();
                    AppRecs.OpCode = Dcode is null ? $"{opcode:X4}" : "0" + Dcode;

                    for (int i = 0; i < datagridFunctionBlockIn.Rows.Count; i++)
                    {
                        string key = $"Input{i + 1}";
                        AppRecs.Inputs.Add(key, GetInputValue(datagridFunctionBlockIn, i));
                    }
                }
                if (edit && (AppRecs.TC_Name != "-" || _curRungTcName.StartsWith("MES_PID_")))
                {
                    if (_curRungTcName.StartsWith("MES_PID_"))
                        xm.LoadedProject.LogicRungs.RemoveAll(d => d.OpCodeNm == _curRungTcName && d.OpCode == AppRecs.OpCode);
                    else
                        xm.LoadedProject.LogicRungs.RemoveAll(d => d.TC_Name == AppRecs.TC_Name && d.OpCode == AppRecs.OpCode);
                }
                xm.LoadedProject.AddRung(AppRecs);
                if (!edit && !insertafter)
                {
                    Form frmTemp = xm.LoadedScreens[AppRecs.WindowName];
                    ((IXMForm)frmTemp).OnShown();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "XM-Pro PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private string IncreasePIDInstructionCounter()
        {
            return "MES_PID_#";
        }

        private object GetCellValue(DataGridView datagridFunctionBlockOut, int rowIndex, int cellIndex)
        {
            if (datagridFunctionBlockOut.Rows.Count > rowIndex)
            {
                var cellValue = (string)datagridFunctionBlockOut.Rows[rowIndex].Cells[cellIndex].Value;
                return string.IsNullOrEmpty(cellValue) ? "-" : cellValue.Trim();
            }
            return "-";
        }

        private object GetInputValue(DataGridView datagridFunctionBlockIn, int rowIndex)
        {
            if (datagridFunctionBlockIn.Rows.Count > rowIndex)
            {
                var cellValue = (string)datagridFunctionBlockIn.Rows[rowIndex].Cells[4].Value;
                if (!string.IsNullOrEmpty(cellValue))
                {
                    cellValue = cellValue.Trim();
                    var operand = (string)datagridFunctionBlockIn.Rows[rowIndex].Cells[2].Value;
                    return operand == "Negation Operand" ? "~" + cellValue : cellValue;
                }
                else if (string.IsNullOrEmpty(cellValue) && comboBoxInstruction.Text.Equals("MES_PID"))
                {
                    return 0;
                }
            }
            return "-";
        }

        private void ResetTimerCounterIndex(string instruction, string TCName)
        {
            var TimerCounterRecs = xm.LoadedProject.LogicRungs.Where(R => R.OpCodeNm == instruction).OrderBy(o => o.TC_Name);
            foreach (ApplicationRung TCRec in TimerCounterRecs)
            {
                if (TCRec.TC_Name != null && TCRec.TC_Name != "-" && TCName != "-")
                {
                    int TcNum = Convert.ToInt32(Regex.Match(TCRec.TC_Name, @"\d+").Value);
                    if (Convert.ToInt32(Regex.Match(TCRec.TC_Name, @"\d+").Value) > Convert.ToInt32(Regex.Match(TCName, @"\d+").Value))
                    {
                        TCRec.TC_Name = TCRec.TC_Name.Substring(0, 1) + (TcNum - 1);
                    }
                }
            }
        }
        private string IncreaseMQTTCounter(string instruction)
        {
            if (instruction == "MQTT Publish")
            {
                var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCodeNm == instruction);
                if (code != null && code.Count() > 0)
                {
                    var maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", "")));
                    int maxPackNo = Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1;
                    return "PUB" + maxPackNo;
                }
                return "PUB1";
            }
            else
            {
                var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCodeNm == instruction);
                if (code != null && code.Count() > 0)
                {
                    var maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", "")));
                    int maxPackNo = Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1;
                    return "SUB" + maxPackNo;
                }
                return "SUB1";
            }
        }
        private string IncreaePackInstructorCounter(string instruction)
        {
            if (instruction == "Pack")
            {
                var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCodeNm == instruction);
                if (code != null && code.Count() > 0)
                {
                    var maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", "")));
                    int maxPackNo = Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1;
                    return "PK" + maxPackNo;
                }
                return "PK1";
            }
            else
            {
                var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCodeNm == instruction);
                if (code != null && code.Count() > 0)
                {
                    var maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", "")));
                    int maxPackNo = Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1;
                    return "UPK" + maxPackNo;
                }
                return "UPK1";
            }
        }

        private bool CheckCountOfFunctionBlock(string counterValue)
        {
            var disallowedTcNames = new List<string> { "UDFB", "Pack", "UnPack", "MQTT" };
            var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCode == counterValue);
            if (disallowedTcNames.Contains(comboBoxInstructionType.Text.ToString()))
            {
                if (code.Count() > 0)
                {
                    var maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", ""))); // code.Max(C => C.TC_Name);
                    string newtccode = "TC:" + maxcode.Replace(Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value).ToString(), (Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1).ToString());
                    string opcodenm = code.Select(R => R.OpCodeNm).FirstOrDefault();
                    if ((Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1) > Counters[opcodenm].Maximum)
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;
        }

        private bool CheckDatatype()
        {
            string datatype = comboBoxDataType.Text.ToString();
            bool done = true;
            foreach (DataGridViewRow row in datagridFunctionBlockIn.Rows)
            {
                if (datagridFunctionBlockIn.Rows[row.Index].Cells[4].Value != null)
                {
                    datatype = row.Cells[1].Value.ToString();
                    if (done && row.Cells[4].Value.ToString().Length > 0)
                    {
                        if (row.Cells[4].Value.ToString().Contains(":"))
                        {
                            if (xm.LoadedProject.Tags.Where(A => A.LogicalAddress == row.Cells[4].Value.ToString() && (A.Model == "" || A.Model == null)).Count() > 0)
                            {
                                var IOLabel = (XMIOConfig)xm.LoadedProject.Tags.Where(A => A.LogicalAddress == row.Cells[4].Value.ToString()).First();
                                if (datatype != IOLabel.Label.ToString())
                                {
                                    done = false;
                                }
                            }
                        }
                    }
                }
            }
            return done;
        }

        private bool CheckAndAddInputOutputTags()
        {
            bool done = true;
            foreach (DataGridViewRow row in datagridFunctionBlockIn.Rows)
            {
                if (comboBoxInstructionType.SelectedItem.ToString() != "Pack")
                {
                    bool isadded = checkedAndAddLogicalAddress(row, "Inputs");
                    if (!isadded)
                        return isadded;
                }
            }
            foreach (DataGridViewRow row in datagridFunctionBlockOut.Rows)
            {
                if (comboBoxInstructionType.SelectedItem.ToString() != "UnPack")
                {
                    bool isadded = checkedAndAddLogicalAddress(row, "Outputs");
                    if (!isadded)
                        return isadded;
                }
            }
            return done;
        }

        private bool checkedAndAddLogicalAddress(DataGridViewRow row, string type)
        {
            if (type == "Inputs")
            {
                int columnIndex = datagridFunctionBlockIn.Columns.Count - 1;
                if (row.Cells[columnIndex].Value != null && row.Cells[columnIndex].Value.ToString() != "")
                {
                    string LogicalAddress = row.Cells[columnIndex].Value.ToString();
                    string OperandType = row.Cells[2].Value.ToString();
                    if (LogicalAddress.Contains(":") || !OperandType.Equals("Numeric Operand"))
                    {
                        if (row.Cells[columnIndex].Value != null && !string.IsNullOrEmpty(row.Cells[columnIndex].Value.ToString()))
                        {
                            XMIOConfig tag = null;
                            if (!LogicalAddress.Contains(":") && udfbname != "")
                            {
                                if (comboBoxInstructionType.Text.Equals("ReadProperty") && (comboBoxInstruction.Text.Equals("Notification")
                                       || comboBoxInstruction.Text.Equals("Device") || comboBoxInstruction.Text.Equals("Schedule")))
                                {
                                    return CheckForBacnetObjectName(LogicalAddress);
                                }

                                UDFBInfo uDFB = xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == udfbname).FirstOrDefault();
                                if (uDFB.UDFBlocks.Where(b => b.Text == LogicalAddress && b.DataType == row.Cells[1].Value.ToString()).Count() != 0)
                                {
                                    errorProviderFB.SetError(datagridFunctionBlockIn, null);
                                    return true;
                                }
                                else
                                {
                                    errorProviderFB.SetError(datagridFunctionBlockIn, null);
                                    return false;
                                }
                            }
                            else
                            {
                                tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress == LogicalAddress).FirstOrDefault();
                            }

                            if (tag == null)
                            {
                                if (LogicalAddress.StartsWith("F2:") || LogicalAddress.StartsWith("S3:") || LogicalAddress.StartsWith("W4:") || LogicalAddress.StartsWith("P5:") || LogicalAddress.StartsWith("T6:") || LogicalAddress.StartsWith("C7:"))
                                {
                                    string currentLoadedScreen = xm.CurrentScreen.ToString();
                                    XMProForm tempForm = new XMProForm();
                                    tempForm.StartPosition = FormStartPosition.CenterParent;
                                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                    tempForm.Text = "Add New Address Added in Logic for ( " + row.Cells[0].Value.ToString() + " )";
                                    TagsUserControl userControl = null;
                                    InstructionTypeDeserializer selectedInstruction = ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem);
                                    if (row.Cells[columnIndex].Value.ToString() == LogicalAddress && !currentLoadedScreen.StartsWith("UDFLadder") && selectedInstruction.OutputDataTypes.Count() > 0)
                                    {
                                        userControl = new TagsUserControl(isRetentive, LogicalAddress, row.Cells[1].Value.ToString());
                                    }
                                    else if (currentLoadedScreen.StartsWith("UDFLadder"))
                                    {
                                        string[] splitResult = currentLoadedScreen.Split(new string[] { "#" }, StringSplitOptions.None);
                                        string udfbName = splitResult[1];
                                        string actualUdfbName = udfbName.Replace("Logic", "Tags");
                                        userControl = new TagsUserControl(LogicalAddress, "", actualUdfbName, row.Cells[1].Value.ToString());
                                    }
                                    else
                                    {
                                        string datatype = row.Cells[1].Value.ToString();
                                        //Change DataType of Input Address of UnPackInstruction
                                        if (row.Index == 0 && (selectedInstruction.Text.ToString() == "UnPack" && row.Cells[columnIndex].Value.ToString().StartsWith("W4")))
                                        {
                                            datatype = "Word";
                                        }
                                        userControl = new TagsUserControl(isRetentive, LogicalAddress, datatype);
                                    }
                                    tempForm.Height = userControl.Height + 25;
                                    tempForm.Width = userControl.Width;
                                    tempForm.Controls.Add(userControl);
                                    var frmTemp = this.ParentForm as frmMain;

                                    DialogResult result = tempForm.ShowDialog(frmTemp);
                                    if (result == DialogResult.OK)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {

                                    //adding for checking object name for the Notification Device and Schedule instruction from the ReadProperty Function block.
                                    if (comboBoxInstructionType.Text.Equals("ReadProperty") && (comboBoxInstruction.Text.Equals("Notification")
                                        || comboBoxInstruction.Text.Equals("Device") || comboBoxInstruction.Text.Equals("Schedule")))
                                    {
                                        return CheckForBacnetObjectName(LogicalAddress);
                                    }

                                    if (xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith(LogicalAddress)).Count() > 0)
                                    {
                                        errorProviderFB.SetError(datagridFunctionBlockIn, null);
                                        return true;
                                    }
                                    else
                                    {
                                        int rowNum = row.Index;
                                        errorProviderFB.SetError(datagridFunctionBlockIn, "Invalid Logical Address Selected it is not added in Tags/Devices on Input" + (rowNum) + 1);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                errorProviderFB.SetError(datagridFunctionBlockIn, null);
                                return true;
                            }
                        }
                    }
                    else if (!LogicalAddress.Contains(":") && OperandType.Equals("Numeric Operand"))
                    {
                        return true;
                    }
                }
                else
                    return true;
            }
            else if (type == "Outputs")
            {
                int columnIndex = datagridFunctionBlockOut.Columns.Count - 1;
                if (row.Cells[columnIndex].Value != null && row.Cells[columnIndex].Value.ToString() != "")
                {
                    string LogicalAddress = row.Cells[columnIndex].Value.ToString();
                    string OperandType = row.Cells[2].Value.ToString();
                    if (LogicalAddress.Contains(":") || OperandType.Equals("Memory Address Variable"))
                    {
                        if (row.Cells[columnIndex].Value != null && !string.IsNullOrEmpty(row.Cells[columnIndex].Value.ToString()))
                        {
                            XMIOConfig tag = null;
                            if (!LogicalAddress.Contains(":") && udfbname != "")
                            {
                                UDFBInfo uDFB = xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == udfbname).FirstOrDefault();
                                if (uDFB.UDFBlocks.Where(b => b.Text == LogicalAddress && b.DataType == row.Cells[1].Value.ToString()).Count() != 0)
                                {
                                    errorProviderFB.SetError(datagridFunctionBlockIn, null);
                                    return true;
                                }
                                else
                                {
                                    errorProviderFB.SetError(datagridFunctionBlockIn, null);
                                    return false;
                                }
                            }
                            else
                            {
                                tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress == LogicalAddress).FirstOrDefault();
                            }
                            if (tag == null)
                            {
                                if (LogicalAddress.StartsWith("F2") || LogicalAddress.StartsWith("S3") || LogicalAddress.StartsWith("W4") || LogicalAddress.StartsWith("P5") || LogicalAddress.StartsWith("T6") || LogicalAddress.StartsWith("C7"))
                                {
                                    string currentLoadedScreen = xm.CurrentScreen.ToString();
                                    XMProForm tempForm = new XMProForm();
                                    tempForm.StartPosition = FormStartPosition.CenterParent;
                                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                    tempForm.Text = "Add New Address Added in Logic for ( " + row.Cells[0].Value.ToString() + " )";
                                    TagsUserControl userControl = null;
                                    InstructionTypeDeserializer selectedInstruction = ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem);
                                    if (row.Cells[columnIndex].Value.ToString() == LogicalAddress && !currentLoadedScreen.StartsWith("UDFLadder") && selectedInstruction.OutputDataTypes.Count() > 0)
                                    {
                                        userControl = new TagsUserControl(isRetentive, LogicalAddress, row.Cells[1].Value.ToString());

                                    }
                                    else if (currentLoadedScreen.StartsWith("UDFLadder"))
                                    {
                                        string[] splitResult = currentLoadedScreen.Split(new string[] { "#" }, StringSplitOptions.None);
                                        string udfbName = splitResult[1];
                                        string actualUdfbName = udfbName.Replace("Logic", "Tags");
                                        userControl = new TagsUserControl(LogicalAddress, "", actualUdfbName, row.Cells[1].Value.ToString());
                                    }
                                    else
                                    {
                                        // if Tag is Retentive then to check checkbox default chkIsChecked 
                                        if (comboBoxInstructionType.Text.Equals("TimerRTON") && row.Index == 1)
                                        {
                                            isRetentive = true;
                                        }

                                        //in EXP block show real instead of byte at output1 if address start with P5
                                        string datatype = row.Cells[1].Value.ToString();
                                        if (row.Index == 0 && (selectedInstruction.Text.ToString() == "EXP" && row.Cells[columnIndex].Value.ToString().StartsWith("P5")))
                                        {
                                            datatype = "Real";
                                        }
                                        //Change DataType of Output Address of PackInstruction
                                        if (row.Index == 0 && (selectedInstruction.Text.ToString() == "Pack" && row.Cells[columnIndex].Value.ToString().StartsWith("P5")))
                                        {
                                            datatype = "Word";
                                        }
                                        userControl = new TagsUserControl(isRetentive, LogicalAddress, datatype);

                                    }
                                    tempForm.Height = userControl.Height + 25;
                                    tempForm.Width = userControl.Width;
                                    tempForm.Controls.Add(userControl);
                                    var frmTemp = this.ParentForm as frmMain;

                                    DialogResult result = tempForm.ShowDialog(frmTemp);
                                    if (result == DialogResult.OK)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith(LogicalAddress)).Count() > 0)
                                    {
                                        errorProviderFB.SetError(datagridFunctionBlockOut, null);
                                        return true;
                                    }
                                    else
                                    {
                                        int rowNum = row.Index;
                                        errorProviderFB.SetError(datagridFunctionBlockOut, "Invalid Logical Address Selected it is not added in Tags/Devices on Input" + (rowNum) + 1);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                errorProviderFB.SetError(datagridFunctionBlockOut, null);
                                return true;
                            }

                        }
                    }
                    else
                        return true;
                }
                else
                    return true;
            }
            else
            {

                errorProviderFB.SetError(datagridFunctionBlockIn, null);
                return true;
            }
            return false;
        }

        private bool CheckForBacnetObjectName(string LogicalAddress)
        {
            bool isThere = false;
            switch (comboBoxInstruction.Text)
            {
                case "Notification":
                    isThere = xm.LoadedProject.BacNetIP.Notifications.Any(t => t.ObjectName.Equals(LogicalAddress));
                    if (!isThere)
                    {
                        errorProviderFB.SetError(datagridFunctionBlockIn, "Invalid object name for notification bacNet object at input no" + 1);
                        return false;
                    }
                    return true;
                case "Device":
                    isThere = xm.LoadedProject.BacNetIP.Device.ObjectName.Equals(LogicalAddress);
                    if (!isThere)
                    {
                        errorProviderFB.SetError(datagridFunctionBlockIn, "Invalid object name for Device bacNet object at input no" + 1);
                        return false;
                    }
                    return true;
                case "Schedule":
                    isThere = xm.LoadedProject.BacNetIP.Schedules.Any(t => t.ObjectName.Equals(LogicalAddress));
                    if (!isThere)
                    {
                        errorProviderFB.SetError(datagridFunctionBlockIn, "Invalid object name for Schedule bacNet object at input no" + 1);
                        return false;
                    }
                    return true;
            }
            return isThere;
        }

        private bool CheckAndAddTag(string address, string dataType, string strtype)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "Add New Address Added in Logic for ( " + strtype + " )";
            TagsUserControl userControl;
            Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);

            userControl = new TagsUserControl(isRetentive, address, dataType);
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK)
            {
                return true;
            }
            if (result == DialogResult.Cancel)
            {
                tempForm.Close();
            }
            return false;
        }

        private bool ValidEntry()
        {
            foreach (DataGridViewRow dr in datagridFunctionBlockOut.Rows)
            {
                if (dr.Cells["OPTagaddress"].Value != null && dr.Cells[2].EditedFormattedValue?.ToString() != "")
                {
                    if (dr.Cells[4].EditedFormattedValue?.ToString() == "")
                    {
                        MessageBox.Show("All Outputs are not entered", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    //adding validation for validating output values.
                    TextBox textBox = new TextBox();
                    if (datagridFunctionBlockOut.Rows[dr.Index].Cells["OPTagaddress"].Value != null)
                    {
                        textBox.Text = datagridFunctionBlockOut.Rows[dr.Index].Cells["OPTagaddress"].Value.ToString();
                        textBox.Tag = datagridFunctionBlockOut.Rows[dr.Index].Cells["outdatatype"].Value.ToString();
                        textBox.Name = datagridFunctionBlockOut.Rows[dr.Index].Cells["output"].Value.ToString();

                    }
                    string outputType = datagridFunctionBlockOut.Rows[dr.Index].Cells[2].Value.ToString();
                    ValidateOperand(textBox, datagridFunctionBlockOut.Rows[dr.Index].Cells["outdatatype"].Value.ToString(), outputType, "Output");
                    if (lblerror.Text != "")
                        return false;
                }
                else if (dr.Cells["OPTagaddress"].Value == null)
                {
                    MessageBox.Show("All Outputs are not entered", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            foreach (DataGridViewRow dr in datagridFunctionBlockIn.Rows)
            {
                if (dr.Cells["Operand_Type"].Value != null && dr.Cells[2].EditedFormattedValue?.ToString() != "")
                {
                    if (dr.Cells[4].EditedFormattedValue?.ToString() == "")
                    {
                        MessageBox.Show("All Inputs are not entered", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                }
                //adding validation for validating the input values.
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[dr.Index].Cells["Operand_Type"];
                string selectedValue = comboBoxCell.Value?.ToString();
                DataGridViewCell cell = datagridFunctionBlockIn.Rows[dr.Index].Cells[4];
                TextBox textBox = new TextBox();
                if (datagridFunctionBlockIn.Rows[dr.Index].Cells["Tag_Address"].Value != null)
                {
                    textBox.Text = datagridFunctionBlockIn.Rows[dr.Index].Cells["Tag_Address"].Value.ToString();
                    textBox.Tag = datagridFunctionBlockIn.Rows[dr.Index].Cells["datatype"].Value.ToString();
                    textBox.Name = datagridFunctionBlockIn.Rows[dr.Index].Cells["Input_1"].Value.ToString();

                }
                ValidateOperand(textBox, datagridFunctionBlockIn.Rows[dr.Index].Cells["Operand_Type"].Value.ToString(), "", "Input");
                if (lblerror.Text != "")
                    return false;
            }
            return true;
        }

        private void comboBoxInstruction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxInstructionType.Text == "UDFB")
            {
                this.labelTopic.Visible = false;
                this.cmbTopic.Visible = false;
                if (comboBoxInstructionType.SelectedIndex == -1) return;
                datagridFunctionBlockIn.AllowUserToAddRows = false;
                datagridFunctionBlockIn.DataSource = null;
                datagridFunctionBlockIn.Rows.Clear();
                string instruction = comboBoxInstructionType.SelectedItem.ToString();
                //Data Type for DataTypeComboBox
                Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);
                comboBoxDataType.DataSource = selectedInstruction.SupportedDataTypes;
                AddInputandOutPut();

                if (instruction.Contains("Timer"))
                {
                    datagridFunctionBlockIn.Rows.Add();
                    datagridFunctionBlockIn.Rows.Add();

                    int rowCount = 2;
                    for (int i = 0; i < rowCount; i++)
                    {
                        int Input_1 = 0;
                        int Operand_Type = 1;
                        int Tag_For_Operand = 2;
                        int Tag_Address = 3;

                        List<string> operand = new List<string>();
                        operand.Add("Normal Operand");
                        operand.Add("Negation Operand");
                        operand.Add("Numeric Operand");
                        datagridFunctionBlockIn.Rows[i].Cells[Input_1].Value = "Input" + $"{i + 1}";
                        DataGridViewComboBoxCell operandtype = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[Operand_Type];
                        operandtype.DataSource = operand;
                        DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[Tag_For_Operand];
                        tags.DataSource = XMProValidator.FillTagOperands("Bool");
                        datagridFunctionBlockIn.Rows[i].Cells[Tag_Address].Value = "";
                    }
                }
                else if (instruction.Equals("Logical") || instruction.Equals("Arithmetic"))
                {
                    datagridFunctionBlockIn.Rows.Add();
                    datagridFunctionBlockIn.Rows.Add();
                    datagridFunctionBlockIn.Rows.Add();
                    datagridFunctionBlockIn.Rows.Add();
                    int rowCount = 4;
                    for (int i = 0; i < rowCount; i++)
                    {
                        int Input_1 = 0;
                        int Operand_Type = 1;
                        int Tag_For_Operand = 2;
                        int Tag_Address = 3;

                        List<string> operand = new List<string>();
                        operand.Add("Normal Operand");
                        operand.Add("Negation Operand");
                        operand.Add("Numeric Operand");

                        datagridFunctionBlockIn.Rows[i].Cells[Input_1].Value = "Input" + $"{i + 1}";
                        DataGridViewComboBoxCell operandtype = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[Operand_Type];
                        operandtype.DataSource = operand;
                        DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[Tag_For_Operand];
                        tags.DataSource = XMProValidator.FillTagOperands("Bool");
                        tags.Value = tags.Items[0];
                        datagridFunctionBlockIn.Rows[i].Cells[Tag_Address].Value = "";
                    }
                }
                else if (instruction.Equals("Scale"))
                {
                    int row = 5;
                    List<string> operand = new List<string>();
                    operand.Add("Normal Operand");
                    operand.Add("Negation Operand");
                    operand.Add("Numeric Operand");
                    for (int i = 0; i < row; i++)
                    {
                        datagridFunctionBlockIn.Rows.Add();
                        datagridFunctionBlockIn.Rows[i].Cells[0].Value = "Input" + $"{i + 1}";
                        DataGridViewComboBoxCell operandtype = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells[1];
                        operandtype.DataSource = operand;
                        DataGridViewComboBoxCell tags = (DataGridViewComboBoxCell)datagridFunctionBlockIn.Rows[i].Cells["Operand_Type"];
                        tags.DataSource = XMProValidator.FillTagOperands("Bool");
                        datagridFunctionBlockIn.Rows[i].Cells["Tag_For_Operand"].Value = "";
                    }
                }

            }
            else
            {
                this.labelTopic.Visible = false;
                this.cmbTopic.Visible = false;
                if (comboBoxInstructionType.Text == "MQTT")
                {
                    this.labelTopic.Visible = true;
                    this.cmbTopic.Visible = true;
                    FillMQTTTopicDetails();
                }
                //InstructionType selectedInstructionType = ((InstructionType)comboBoxInstructionType.SelectedValue);
                //XmlSerializer serializer = new XmlSerializer(typeof(InstructionsWrapper));
                //InstructionsWrapper instructionsWrapper = null;
                //string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\NewInstructionFormat.xml");
                //using (var reader = new StreamReader(filePath))
                //{
                //    instructionsWrapper = (InstructionsWrapper)serializer.Deserialize(reader);
                //}

                GetSupportedDataTypes(((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).Text);
                AddInputandOutPut();

            }

        }

        private void FillMQTTTopicDetails()
        {
            string selectedInstruction = ((InstructionTypeDeserializer)comboBoxInstruction.SelectedItem).Text;
            var PubRequest = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
            var SubRequest = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
            List<TopicList> _topicList = new List<TopicList>();
            TopicList.List.Clear();
            TopicList defultTopicList = new TopicList
            {
                ID = 0,
                Text = "Select Topic"
            };
            TopicList.List.Add(defultTopicList);
            if (PubRequest != null && selectedInstruction.ToString() == "MQTT Publish")
            {
                foreach (var item in PubRequest)
                {
                    TopicList tpList = new TopicList
                    {
                        ID = item.keyvalue,
                        Text = item.topic
                    };
                    TopicList.List.Add(tpList);
                }
            }
            if (SubRequest != null && selectedInstruction.ToString() == "MQTT Subscribe")
            {
                foreach (var item in SubRequest)
                {
                    TopicList tpList = new TopicList
                    {
                        ID = item.key,
                        Text = item.topic
                    };
                    TopicList.List.Add(tpList);
                }

            }
            cmbTopic.DataSource = null;
            cmbTopic.DataSource = TopicList.List;
            cmbTopic.DisplayMember = "Text";
            cmbTopic.ValueMember = "ID";
        }

        /// <summary>
        /// Initialise timer counter with minimum and maximum bounds
        /// </summary>
        private void InitializeTimerCounters()
        {
            Counters.Add("FB", new Counter { Instruction = "FB", CurrentPosition = 0, Maximum = 255 });

            foreach (InstructionTypeDeserializer instruction in xm.instructionsList)
            {
                if (instruction.TcNameDetails != null && !string.IsNullOrEmpty(instruction.TcNameDetails.Instruction))
                {
                    Counters.Add(instruction.Text, new Counter { Instruction = instruction.TcNameDetails.Instruction, CurrentPosition = instruction.TcNameDetails.CurrentPosition, Maximum = instruction.TcNameDetails.Maximum });
                }
            }

        }

        /// <summary>
        /// Increase timer counter depending on the selected type of Instruction 
        /// </summary>
        /// <param name="instruction">Selected Instruction.</param>
        /// <returns>Conter Value for the Instruction.</returns>
        private string IncreaseTimerCounter(string instruction, bool check = false)
        {
            string timerCounterLabel = "-";

            if (comboBoxInstructionType.Text == "UDFB")
            {
                if (Counters.ContainsKey(instruction))
                {

                    var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCode.Equals("9999") && R.TC_Name != "-");
                    if (code != null && code.Count() > 0)
                    {
                        var maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", ""))); // code.Max(C => C.TC_Name);
                        Counters[instruction].CurrentPosition = Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1;
                    }

                    if ((Counters[instruction].CurrentPosition <= Counters[instruction].Maximum))
                    {
                        timerCounterLabel = instruction + "" + Counters[instruction].CurrentPosition;
                        ++Counters[instruction].CurrentPosition;
                    }
                    else
                        throw new TimerMaxLimitExceedException(instruction);
                }
            }
            else
            {
                if (Counters.ContainsKey(instruction))
                {
                    timerCounterLabel = Counters[instruction].Instruction + "#";
                }
            }

            return timerCounterLabel;
        }

        private void datagridFunctionBlockOut_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Check if the current cell is in the desired column
            var currentCell = datagridFunctionBlockOut.CurrentCell;
            if (currentCell != null && (currentCell.ColumnIndex == 2 || currentCell.ColumnIndex == 3))
            {
                // Commit the cell value to trigger the ValueChanged event
                if (datagridFunctionBlockOut.IsCurrentCellDirty)
                {
                    datagridFunctionBlockOut.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void datagridFunctionBlockIn_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void datagridFunctionBlockOut_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void comboBoxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (enableCellValueChanged)
            {
                if (comboBoxInstructionType.Text != "UDFB" && comboBoxInstruction.Text != "XMPS2000.LadderLogic.InstructionTypeDeserializer" && comboBoxInstruction.Text != "")
                {
                    datagridFunctionBlockIn.AllowUserToAddRows = false;
                    datagridFunctionBlockOut.AllowUserToAddRows = false;
                    if (comboBoxInstruction.Text != "XMPS2000.LadderLogic.InstructionTypeDeserializer")
                    {
                        BindDataSourceToGridView();
                    }
                }
            }
        }

        private void datagridFunctionBlockOut_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox comboBox)
            {
                // Remove the previous event handler, if any
                comboBox.SelectedIndexChanged -= ComboBoxOutPutGrid_SelectedIndexChanged;
                // Add a new event handler for the ComboBox within the DataGridView
                comboBox.SelectedIndexChanged += ComboBoxOutPutGrid_SelectedIndexChanged;
            }
        }
        private void ComboBoxOutPutGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                int selectedIndex = comboBox.SelectedIndex;

                //stop the other code until updating values in last cell from data GridView
                int currentColumnIndex = datagridFunctionBlockOut.CurrentCell.ColumnIndex;
                if (currentColumnIndex == 3)
                {
                    datagridFunctionBlockOut.BeginInvoke(new Action(() =>
                    {
                        int currentRowIndex = datagridFunctionBlockOut.CurrentCell.RowIndex;

                        object cellValue = datagridFunctionBlockOut.Rows[currentRowIndex].Cells[currentColumnIndex].Value;

                        if (currentColumnIndex == 3)
                        {
                            if (cellValue != null)
                            {
                                string LogicalAdd = XMProValidator.GetTheAddressFromTag(cellValue.ToString());
                                DataGridViewCell cell = datagridFunctionBlockOut.Rows[currentRowIndex].Cells[4];
                                cell.Value = LogicalAdd;
                            }
                        }
                    }));
                }
            }
        }

        private List<string> OutPutTypeChangedTags(string SelectedoutputType, int RowIndex)
        {
            //Copy From applicationUserControl OutputTypeSelectedIndexChnaged
            string stringName = xm.CurrentScreen.ToString();
            string[] parts = stringName.Split('#');
            string formName = parts[0];
            string logicBlockName = parts[1];

            string outputType = SelectedoutputType;
            string dataType = datagridFunctionBlockOut.Rows[RowIndex].Cells[1].Value.ToString();
            string actualDataType = dataType;
            List<string> tagList = new List<string> { };
            tagList.Add("-Select Tag Name-");
            if (comboBoxInstruction.SelectedItem == null)
                return null;
            if (comboBoxInstructionType.SelectedItem.ToString() == "Limit Alarm" || comboBoxInstruction.SelectedItem.ToString().EndsWith("BOOL"))
                dataType = "Bool";
            if (comboBoxInstructionType.Text.ToString().Equals("TimerRTON") && RowIndex == 1)
                dataType = "Word-Rentive";
            //Changes for the showing output Tag for the DataConversion
            string instructionType = comboBoxInstructionType.SelectedItem.ToString();
            if (stringName.Contains("UDFLadderForm"))
            {
                if ((outputType.Equals("On-board") || outputType.Equals("Remote") || outputType.Equals("Expansion")) && (comboBoxInstructionType.Text.ToString().Equals("UnPack") || comboBoxInstruction.Text.ToString().Equals("Pack")))
                    return new List<string>() { "-Select Tag Name-" };
                if (comboBoxInstruction.Text.ToString().Equals("Pack") && outputType.Equals("Memory Address Variable"))
                {
                    var TagOP1 = XMProValidator.FillTagOperands("Pack-Word", udfbname);
                    var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                    tagList.AddRange(TagOP1);
                    tagList.RemoveAll(T =>
                    {
                        var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(T));
                        return tag != null && (tag.Type.ToString().Equals("DigitalInput") || tag.Type.ToString().Equals("DigitalOutput"));
                    });
                    tagList.Remove("-Select Tag Name-");
                    return tagList;
                }
                else if (comboBoxInstruction.Text.ToString().Equals("UnPack") && outputType.Equals("Memory Address Variable"))
                {
                    var TagOP1 = XMProValidator.FillTagOperands("Pack-Bool", udfbname);
                    //Remove Tags with Default IO List Type
                    var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                    tagList.AddRange(TagOP1);
                    tagList.RemoveAll(T =>
                    {
                        var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(T));
                        return tag != null && (tag.Type.ToString().Equals("DigitalInput") || tag.Type.ToString().Equals("DigitalOutput"));
                    });
                    tagList.Remove("-Select Tag Name-");
                    return tagList;
                }
                List<string> outputTagList = FillOutputTagOperands(outputType, dataType, logicBlockName.Split(' ')[0]);
                return outputTagList;
            }

            else if (!comboBoxInstructionType.Text.ToString().Equals("Pack") && !comboBoxInstructionType.Text.ToString().Equals("UnPack"))
            {
                if (comboBoxInstruction.SelectedItem == null)
                    return null;
                if (comboBoxInstructionType.SelectedItem.ToString() == "Limit Alarm" || comboBoxInstruction.SelectedItem.ToString().EndsWith("BOOL"))
                    dataType = "Bool";

                switch (outputType)
                {
                    case "On-board":
                        if (dataType == "Bool")
                        {
                            if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
                            {
                                tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output")
                                && L.LogicalAddress.Contains(".")
                                && L.IoList == Core.Types.IOListType.OnBoardIO && !(L.Label.EndsWith("_OR") || L.Label.EndsWith("_OL"))).Select(L => L.Tag).ToList());
                            }
                            else
                            {
                                tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output")
                                && L.LogicalAddress.Contains(".")
                                && L.IoList == Core.Types.IOListType.OnBoardIO).Select(L => L.Tag).ToList());
                            }                          
                        }                         
                        if ((xm.PlcModel == "XM-17-ADT" || xm.PlcModel == "XM-17-ADT-E" || xm.PlcModel == "XBLD-17") && (dataType == "Word"))
                            tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output")
                            && !L.LogicalAddress.Contains(".") && L.Type == Core.Types.IOType.AnalogOutput
                            && L.IoList == Core.Types.IOListType.OnBoardIO).Select(L => L.Tag).ToList());
                        else if (xm.LoadedProject.CPUDatatype.Equals("Real") && dataType == "Real")
                            tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output")
                            && !L.LogicalAddress.Contains(".") && L.Type == Core.Types.IOType.AnalogOutput
                            && L.IoList == Core.Types.IOListType.OnBoardIO).Select(L => L.Tag).ToList());
                        break;
                    case "Remote":
                        tagList.AddRange(xm.LoadedProject.Tags
                            .Where(L => L.Type.ToString().Contains("Output")
                                        && L.IoList == Core.Types.IOListType.RemoteIO
                                        && ((dataType == "Bool" && (L.LogicalAddress.Contains(".") || (L.Mode != null && L.Mode.Equals("Digital"))))
                                            || (dataType == "Word" && !xm.LoadedProject.CPUDatatype.Equals("Real") && !L.LogicalAddress.Contains(".") && (L.Mode == null || !L.Mode.Equals("Digital")))
                                            || (dataType == "Real" && xm.LoadedProject.CPUDatatype.Equals("Real") && !L.LogicalAddress.Contains(".") && (L.Mode == null || !L.Mode.Equals("Digital")))))
                            .Select(L => L.Tag)
                            .ToList());
                        break;
                    case "Expansion":
                        tagList.AddRange(xm.LoadedProject.Tags
                            .Where(L => L.Type.ToString().Contains("Output")
                                        && L.IoList == Core.Types.IOListType.ExpansionIO
                                        && ((dataType == "Bool" && (L.LogicalAddress.Contains(".") || (L.Mode != null && L.Mode.Equals("Digital"))))
                                            || (dataType == "Word" && !xm.LoadedProject.CPUDatatype.Equals("Real") && !L.LogicalAddress.Contains(".") && (L.Mode == null || !L.Mode.Equals("Digital")))
                                            || (dataType == "Real" && xm.LoadedProject.CPUDatatype.Equals("Real") && !L.LogicalAddress.Contains(".") && (L.Mode == null || !L.Mode.Equals("Digital")))))
                            .Select(L => L.Tag)
                            .ToList());
                        if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
                        {
                            tagList.RemoveAll(T =>
                            {
                                var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(T));
                                return tag != null && tag.Label != null && (tag.Label.EndsWith("OR") || tag.Label.EndsWith("OL"));
                            });
                        }
                        //tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO && !L.LogicalAddress.Contains(".")).Select(L => L.Tag).ToList());
                        break;
                    case "Memory Address Variable":
                        var TagOP1 = XMProValidator.FillTagOperands(dataType, udfbname);
                        var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                        tagList.AddRange(TagOP1);
                        tagList.Remove("-Select Tag Name-");
                        if (dataType == "Bool")
                        {
                            tagList.RemoveAll(T =>
                            {
                                var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(T));
                                return tag != null && (tag.Type.ToString().Equals("DigitalInput") || tag.Type.ToString().Equals("DigitalOutput"));
                            });
                        }
                        else
                        {
                            tagList.RemoveAll(T =>
                            {
                                var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(T));
                                return tag != null && (tag.Type.ToString().Equals("DigitalInput") || tag.Type.ToString().Equals("DigitalOutput")
                                                       || tag.Type.ToString().Equals("AnalogInput") || tag.Type.ToString().Equals("AnalogOutput")
                                                       || tag.Type.ToString().Equals("UniversalInput") || tag.Type.ToString().Equals("UniversalOutput"));
                            });
                        }
                        break;
                }
                return tagList;
            }
            else
            {
                if ((!comboBoxInstruction.Text.ToString().Equals("Pack") || !comboBoxInstruction.Text.ToString().Equals("UnPack")) && (SelectedoutputType.Equals("On-board") || SelectedoutputType.Equals("Remote") || SelectedoutputType.ToString().Equals("Expansion")))
                {
                    tagList.Clear();
                    tagList.Add("-Select Tag Name-");
                }
                else
                {
                    if (comboBoxInstruction.Text.ToString().Equals("Pack"))
                    {
                        var TagOP1 = XMProValidator.FillTagOperands("Pack-Word", udfbname);
                        var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                        tagList.AddRange(TagOP1);
                        tagList.RemoveAll(T => T.StartsWith("DigitalInput_DI") || T.StartsWith("DigitalOutput_DO") || T.StartsWith("Analog") || T.StartsWith("Universal"));
                        tagList.Remove("-Select Tag Name-");
                    }
                    else if (comboBoxInstruction.Text.ToString().Equals("UnPack"))
                    {
                        var TagOP1 = XMProValidator.FillTagOperands("Pack-Bool", udfbname);
                        //Remove Tags with Default IO List Type
                        var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                        tagList.AddRange(TagOP1);
                        tagList.Remove("-Select Tag Name-");
                    }
                }
            }

            if (comboBoxInstruction.Text.ToString().Equals("EXP") && (SelectedoutputType.Equals("On-board") || SelectedoutputType.Equals("Remote") || SelectedoutputType.Equals("Expansion")))
            {
                tagList.Clear();
                tagList.Add("-Select Tag Name-");
            }
            else if (comboBoxInstruction.Text.ToString().Equals("EXP") && SelectedoutputType.Equals("Memory Address Variable"))
            {
                tagList.AddRange(xm.LoadedProject.Tags.Where(T => T.Label.ToString() == "Real").Select(N => N.Tag).ToList());
            }
            return tagList;
        }
        private List<string> FillOutputTagOperands(string outputType, string dataType, string udfbName)
        {
            List<string> tagList = new List<string> { };
            tagList.Add("-Select Tag Name-");
            switch (outputType)
            {
                case "On-board":
                    if (dataType == "Bool")
                    {
                        var taglistoutput = xm.LoadedProject.Tags.Where(L => ((L.Type.ToString().Contains("Output") && !L.Type.ToString().StartsWith("Analog") && !L.Type.ToString().StartsWith("Universal"))
                        || ((L.Tag.EndsWith("_OR") || L.Tag.EndsWith("_OL")) && (L.Type.ToString().StartsWith("AnalogOutput") || L.Type.ToString().StartsWith("UniversalOutput"))))
                                && !L.Type.ToString().StartsWith("AnalogInput") && L.IoList == Core.Types.IOListType.OnBoardIO).ToList();
                        var tag = taglistoutput.Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                        tagList.AddRange(tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    }
                    if (xm.PlcModel == "XM-17-ADT" && dataType == "Word")
                        tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.Type == Core.Types.IOType.AnalogOutput && L.IoList == Core.Types.IOListType.OnBoardIO).Select(L => L.Tag).ToList());
                    break;
                case "Remote":
                    var taglistRemote = xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.RemoteIO).ToList();
                    var tagRemote = taglistRemote.Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    tagList.AddRange(tagRemote.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    break;
                case "Expansion":
                    var taglistExpansion = xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO).ToList();
                    var tagExpansion = taglistExpansion.Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    if (dataType == "Bool")
                        tagList.AddRange(tagExpansion.Where(t => t.LogicalAddress.Contains('.') || t.Mode == "Digital").Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    else
                        tagList.AddRange(tagExpansion.Where(t => !t.LogicalAddress.Contains('.') && t.Mode != "Digital").Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    break;
                case "Memory Address Variable":
                    //// adding comment for these for not showing userDefined Tags into the UDFB Block
                    var tagMemory = xm.LoadedProject.Tags.Where(d => d.Model == udfbName + " Tags"|| d.Model is null|| XMPS.Instance.PlcModels.Contains(d.Model)/*|| d.Model == ""*/);
                    if (dataType == "Word-Rentive" || dataType == "Word")
                    {
                        tagList.AddRange(tagMemory.Where(L => (L.LogicalAddress.StartsWith("W4") || L.LogicalAddress.StartsWith("C7") || L.LogicalAddress.StartsWith("T6")) && L.Label == "Word").Select(L => L.Tag).ToList());
                        UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.FirstOrDefault(d => d.UDFBName == udfbName);
                        if (udfbinfo != null)
                        {
                            tagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == "Word").Select(d => d.Text).ToList());
                        }
                    }
                    else
                    {
                        tagList.AddRange(tagMemory.Where(L => L.Label == dataType).Select(L => L.Tag).ToList());
                        UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                        if (udfbinfo != null)
                        {
                            tagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == dataType).Select(d => d.Text).ToList());
                        }
                    }
                    break;
            }
            var Tags = tagList.RemoveAll(DefaultAddedTag);
            return tagList;
        }
    }

}
public partial class TopicList
{
    public int ID { get; set; }
    public string Text { get; set; }

    public static readonly List<TopicList> List = new List<TopicList>() {
    new TopicList { ID=0, Text="Select Topic"}};

}
