using LadderDrawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XMPS2000.Configuration;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.LadderLogic;

namespace XMPS2000.LadderLogic
{
    public partial class ApplicationUserControl : UserControl
    {
        private Dictionary<string, Counter> Counters = new Dictionary<string, Counter>();
        List<ApplicationRung> AppData = new List<ApplicationRung>();
        ApplicationRung _curRung = new ApplicationRung();
        public int Linenumber = 0;
        public bool isEnabled = false;
        public string SelectedInstruction;
        public string SelectedInstructionType;
        public string tc_instuction;
        public string sel_opcode;
        public bool isRetentive = false;
        // new variable for set retentive
        XMPS xm;
        public bool edit;
        public string udfbname;
        public List<Tuple<int, string>> Inputs;
        public List<Tuple<int, string>> Outputs;
        public bool hasLadders = false;
        private int datatype_prv_sel_index;

        //Creating Static Field For TopicName
        public static string _topicSelected;

        /// <summary>
        /// Constructor used to intialise all the components and fill the DataType, Output Type and Instruction Type from the List
        /// </summary>

        public ApplicationUserControl(XMProForm parentForm)
        {
            xm = XMPS.Instance;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            InitializeComponent();

            InitializeTimerCounters();
            AddUDFBInformation();
            comboBoxDataType.DataSource = DataType.List;
            comboBoxOutputType.DataSource = OutputType.List;
            comboBoxInstructionType.DataSource = InstructionType.List;
        }

        private void AddUDFBInformation()
        {
            if (InstructionType.List.Where(u => u.Text == "UDFB").Count() == 0)
            {
                InstructionType instructionType = new InstructionType();
                instructionType.Text = "UDFB";
                instructionType.ID = 900;
                InstructionType.List.Add(instructionType);

                foreach (UDFBInfo uDFBInfo in xm.LoadedProject.UDFBInfo)
                {
                    List<UserDefinedFunctionBlock> uDFBDetails = uDFBInfo.UDFBlocks;
                    Instruction instruction = new Instruction
                    {
                        ID = 1200,
                        Text = uDFBInfo.UDFBName,
                        InstructionType = 900,
                    };
                    var bi = uDFBInfo.UDFBlocks.Where(b => b.Type == "Input");
                    if (bi.Count() > 0)
                    {
                        instruction.Operand1Label = bi.Skip(0).Select(s => s.Text).FirstOrDefault();
                        instruction.Operand1Enabled = true;
                    }
                    if (bi.Count() > 1)
                    {
                        instruction.Operand2Label = bi.Skip(1).Select(s => s.Text).FirstOrDefault();
                        instruction.Operand2Enabled = true;
                    }
                    else
                    {
                        instruction.Operand2Enabled = false;
                    }
                    if (bi.Count() > 2)
                    {
                        instruction.Operand3Label = bi.Skip(2).Select(s => s.Text).FirstOrDefault();
                        instruction.Operand3Enabled = true;
                    }
                    else
                        instruction.Operand3Enabled = false;

                    if (bi.Count() > 3)
                    {
                        instruction.Operand4Label = bi.Skip(3).Select(s => s.Text).FirstOrDefault();
                        instruction.Operand4Enabled = true;
                    }
                    else
                        instruction.Operand4Enabled = false;

                    var bo = uDFBInfo.UDFBlocks.Where(b => b.Type == "Output");
                    if (bo.Count() > 0) instruction.Output1Label = bo.Skip(0).Select(s => s.Text).FirstOrDefault();
                    if (bo.Count() > 1) instruction.Output2Label = bo.Skip(1).Select(s => s.Text).FirstOrDefault();
                    if (bo.Count() > 2) instruction.Output3Label = bo.Skip(2).Select(s => s.Text).FirstOrDefault();

                    List<DataType> dataTypes = new List<DataType> { };
                    foreach (string datatype in uDFBInfo.UDFBlocks.Select(b => b.DataType).Distinct())
                        dataTypes.AddRange(DataType.List.Where(d => d.Text.ToString() == datatype));
                    instruction.SupportedDataTypes.Clear();
                    instruction.SupportedDataTypes.AddRange(dataTypes);

                    Instruction.List.Add(instruction);

                }
            }

        }
        /// <summary>
        /// This methos will get the CSV file and transfert the data to datatable and then take each row and get respective non csv columns like .
        /// DataType_Nm,OpCodeNm etc. and then save the data to the class object and then pass whole class to the database
        /// </summary>
        private void ApplicationUserControl_Load(object sender, EventArgs e)
        {
            cmbTopic.Visible = false;
            labelPackAdd.Visible = false;
            labelUnpackAdd.Visible = false;
            OperationType1.SelectedIndex = 0;
            OperationType2.SelectedIndex = 0;
            OperationType3.SelectedIndex = 0;
            OperationType4.SelectedIndex = 0;
            OperationType5.SelectedIndex = 0;
            OperationEnable.SelectedIndex = 0;
            labelNegation1.Visible = false;
            labelNegation2.Visible = false;
            labelNegation3.Visible = false;
            labelNegation4.Visible = false;
            labelNegation5.Visible = false;
            labelNegationEnable.Visible = false;
            textBoxEnable.Enabled = false;
            TagEnable.Enabled = false;
            Output2.Enabled = false;
            this.ClearInputControls();
            if (SelectedInstructionType != "" && SelectedInstructionType != null)
            {
                comboBoxInstructionType.Text = SelectedInstructionType;
                label3.Visible = false;
                comboBoxInstructionType.Visible = false;
            }
            else
            {
                label3.Visible = true;
                comboBoxInstructionType.Visible = true;

            }
            if (SelectedInstruction != "" && SelectedInstruction != null)
            {
                comboBoxInstruction.Text = SelectedInstruction;
                label4.Visible = false;
                comboBoxInstruction.Visible = false;
            }
            else
            {
                label4.Visible = true;
                comboBoxInstruction.Visible = true;
            }
            if (label3.Visible == false)
            {
                label2.Top -= 20;
                comboBoxDataType.Top -= 20;
                checkBoxEnable.Top -= 20;
                TagEnable.Top -= 20;
                OperationEnable.Top -= 20;
                labelNegationEnable.Top -= 20;
                textBoxEnable.Top -= 20;
                groupBox2.Height -= 20;
                groupBox1.Top -= 20;
                groupBox3.Top -= 20;
                groupBox4.Top -= 20;
                this.Height -= 20;
            }
            CheckandFillTags();
            if (edit == true)
            {
                //var ApplicationData = xm.LoadedProject.LogicRungs.Where(d => d.WindowName == xm.CurrentScreen.ToString() && d.LineNumber == Linenumber).FirstOrDefault();
                //if (ApplicationData != null)
                //{
                //}
                fillControlsWithSelectedRow(_curRung);
            }
            if (Enabled) checkBoxEnable.Checked = true;

        }

        public void SetValues(ApplicationRung rung)
        {
            //if (Linenumber != 0 && edit == true)
            //{
            //    var ApplicationData = xm.LoadedProject.LogicRungs.Where(d => d.WindowName == xm.CurrentScreen.ToString() && d.LineNumber == Linenumber).FirstOrDefault();
            //    if (ApplicationData != null)
            //    {
            //    }
            //}
            _curRung = rung;
            fillControlsWithSelectedRow(rung);
        }

        /// <summary>
        /// On the selection of Data Type change the Input type drop down and other drop downs
        /// </summary>
        private void comboBoxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDataType.SelectedItem == null)
                return;
            string Instruction = "";
            // On selection of a new datatype, clear up existing data from input controls
            if (comboBoxDataType.SelectedIndex != datatype_prv_sel_index) ClearInputControls();

            if (comboBoxInstructionType.SelectedItem != null) Instruction = comboBoxInstructionType.SelectedItem.ToString();
            if ((!string.Equals(((DataType)comboBoxDataType.SelectedItem).Text, "Bool")) && (!Instruction.StartsWith("Timer")) && (!Instruction.Equals("Counter")))
            {
                labelNegation1.Visible = false;
                labelNegation2.Visible = false;
                labelNegation3.Visible = false;
                labelNegation4.Visible = false;
                labelNegationEnable.Visible = false;

                if (OperationType1.Items.Count > 2)
                    OperationType1.Items.RemoveAt(2);
                if (OperationType2.Items.Count > 2)
                    OperationType2.Items.RemoveAt(2);
                if (OperationType3.Items.Count > 2)
                    OperationType3.Items.RemoveAt(2);
                if (OperationType4.Items.Count > 2)
                    OperationType4.Items.RemoveAt(2);
                if (OperationType5.Items.Count > 2)
                    OperationType5.Items.RemoveAt(2);
                if (!OperationEnable.Items.Contains("Negation Operand"))
                    OperationEnable.Items.Insert(2, "Negation Operand");
            }
            else
            {
                if (!OperationType1.Items.Contains("Negation Operand"))
                    OperationType1.Items.Insert(2, "Negation Operand");
                if (!OperationType2.Items.Contains("Negation Operand"))
                    OperationType2.Items.Insert(2, "Negation Operand");
                if (!OperationType3.Items.Contains("Negation Operand"))
                    OperationType3.Items.Insert(2, "Negation Operand");
                if (!OperationType4.Items.Contains("Negation Operand"))
                    OperationType4.Items.Insert(2, "Negation Operand");
                if (!OperationType5.Items.Contains("Negation Operand"))
                    OperationType5.Items.Insert(2, "Negation Operand");
                if (!OperationEnable.Items.Contains("Negation Operand"))
                    OperationEnable.Items.Insert(2, "Negation Operand");
            }
            if (Instruction.Equals("Counter"))
            {
                if (OperationType3.Items.Count > 2)
                    OperationType3.Items.RemoveAt(2);
            }
            if (Instruction.StartsWith("Timer"))
            {
                if (OperationType2.Items.Count > 2)
                    OperationType2.Items.RemoveAt(2);
            }
            CheckandFillTags();
            //comboBoxOutputType.SelectedIndex = comboBoxOutputType.SelectedIndex == -1 ? comboBoxOutputType.SelectedIndex = 0 : comboBoxOutputType.SelectedIndex;
            if (comboBoxInstructionType.SelectedIndex != -1)
                FillOutputTags();
        }

        private void CheckandFillTags()
        {

            string Instruction = comboBoxInstructionType.Text.ToString();
            bool flip = false;
            Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);

            //Tuple<string, string> Topiclist ;
            List<Tuple<string, string>> Topiclist = new List<Tuple<string, string>> { };
            checkBoxEnable.Checked = isEnabled;
            if (Instruction.Contains("Timer") || Instruction.Contains("Counter"))
            {
                flip = true;
            }
            if (comboBoxInstruction.Text != "")
            {
                if (textBoxOp1Address.Enabled == true)
                {
                    if (flip)
                    {
                        TagOperand1.DataSource = XMProValidator.FillTagOperands("Bool", udfbname);
                    }
                    else if (Instruction == "Pack")
                    {
                        var TagOP1 = XMProValidator.FillTagOperands("Pack-Bool", udfbname);
                        var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                        TagOperand1.DataSource = TagOP1;
                    }
                    else if (Instruction == "UnPack")
                    {
                        var TagOP1 = XMProValidator.FillTagOperands("Pack-Word", udfbname);
                        //var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                        TagOperand1.DataSource = TagOP1;
                    }
                    else
                    {
                        string screenName = xm.CurrentScreen;
                        if (comboBoxDataType.SelectedItem != null)
                            TagOperand1.DataSource = screenName.StartsWith("UDFLadderForm") ? XMProValidator.FillTagOperands(((DataType)comboBoxDataType.SelectedItem).Text, udfbname) : XMProValidator.FillTagOperands(((DataType)comboBoxDataType.SelectedItem).Text);

                        var TagOP1 = XMProValidator.FillTagOperands(((DataType)comboBoxDataType.SelectedItem).Text);

                        DataType d1 = (DataType)comboBoxDataType.SelectedItem;                                  //Selected Datatype

                        if (selectedInstruction.Text == "EXP")
                        {
                            if (d1.Text == "Byte")
                            {
                                List<string> tagList = new List<string> { };
                                tagList.Add("-Select Tag Name-");
                                //TagOperand1.DataSource = tagList;
                                tagList.AddRange(xm.LoadedProject.Tags.Where(T => (T.LogicalAddress.StartsWith("I") || T.LogicalAddress.StartsWith("Q")) && !T.LogicalAddress.Contains(".")).Select(T => T.Tag).ToList());
                                //TagOperand1.DataSource = tagList;
                            }
                            else if (d1.Text == "Word")
                                TagOperand1.DataSource = TagOP1.Where(T => !T.StartsWith("D")).ToList();
                        }


                        //Exponential For Word Do not take Digital Input as I1 & For Byte Consider Q,I1 Address
                    }

                }
                if (textBoxOp2Address.Enabled == true)
                {
                    if (flip && Instruction.Contains("Counter"))
                    {
                        TagOperand2.DataSource = XMProValidator.FillTagOperands("Bool", udfbname);
                    }
                    else if (Instruction.Contains("RTON"))
                    {
                        TagOperand2.DataSource = XMProValidator.FillTagOperands("Bool", udfbname);
                    }
                    else
                    {
                        if (comboBoxDataType.SelectedItem != null)
                            TagOperand2.DataSource = XMProValidator.FillTagOperands(((DataType)comboBoxDataType.SelectedItem).Text, udfbname);
                    }
                }
                if (textBoxOp3Address.Enabled == true)
                {
                    if (Instruction.Contains("RTON"))
                    {
                        TagOperand3.DataSource = XMProValidator.FillTagOperands("Word");
                    }
                    if (comboBoxDataType.SelectedItem != null && !(Instruction.Contains("RTON")))
                        TagOperand3.DataSource = XMProValidator.FillTagOperands(((DataType)comboBoxDataType.SelectedItem).Text, udfbname);
                }
                if (textBoxOp4Address.Enabled == true)
                {
                    if (comboBoxDataType.SelectedItem != null)
                        TagOperand4.DataSource = XMProValidator.FillTagOperands(((DataType)comboBoxDataType.SelectedItem).Text, udfbname);
                }
                if (textBoxOp5Address.Enabled == true)
                {
                    TagOperand5.DataSource = XMProValidator.FillTagOperands(((DataType)comboBoxDataType.SelectedItem).Text, udfbname);
                }
                if (textBoxEnable.Enabled == true)
                {
                    TagEnable.Enabled = true;
                    OperationEnable.Enabled = true;
                    TagEnable.DataSource = XMProValidator.FillTagOperands("Bool", udfbname);
                }
                else
                {
                    TagEnable.Enabled = false;
                    OperationEnable.Enabled = false;
                }
                if (textBoxOutputAddress.Enabled == true)
                {
                    TagOutput1.Enabled = true;

                    if ((selectedInstruction.OutputDataTypes.Count() == 1 && selectedInstruction.OutputDataTypes.Where(R => R.ToString().Equals("Bool")).Count() > 0 && Instruction.ToString() != "Scale") || flip)
                    {
                        TagOutput1.DataSource = XMProValidator.FillTagOperands("Bool", udfbname);
                    }
                    //for Pack Instruction Allow only Word Address in output1
                    else if (Instruction == "Pack")
                    {
                        List<string> tagList = new List<string> { };
                        tagList.Add("-Select Tag Name-");
                        if (comboBoxOutputType.Text == "Memory Address Variable")
                        {
                            var TagOP1 = XMProValidator.FillTagOperands("Pack-Word", udfbname);
                            var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                            TagOutput1.DataSource = TagOP1;
                        }
                        else
                        {
                            TagOutput1.DataSource = tagList;
                        }

                    }
                    else if (Instruction == "UnPack")
                    {
                        List<string> tagList = new List<string> { };
                        tagList.Add("-Select Tag Name-");
                        if (comboBoxOutputType.Text == "Memory Address Variable")
                        {
                            var TagOP1 = XMProValidator.FillTagOperands("Pack-Bool", udfbname);
                            //var TagOP1 = XMProValidator.FillTagOperands(selectedInstruction.OutputDataTypes.Select(D => D.Text).FirstOrDefault(), udfbname);
                            //Remove Tags with Default IO List Type
                            var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                            TagOutput1.DataSource = TagOP1;
                        }
                        else
                        {
                            TagOutput1.DataSource = tagList;
                        }
                    }
                    //Setting Bool for Scale Output1
                    else if (Instruction.Contains("Scale") || Instruction.Contains("Arithmetic"))
                    {
                        if (selectedInstruction.Text.ToString() == "EXP")
                        {
                            List<string> tagList = new List<string> { };
                            tagList.Add("-Select Tag Name-");
                            if (comboBoxOutputType.Text == "Memory Address Variable")
                            {
                                tagList.AddRange(xm.LoadedProject.Tags.Where(T => T.Label.ToString() == "Real").Select(N => N.Tag).ToList());
                                TagOutput1.DataSource = tagList;
                            }
                            else
                            {
                                TagOutput1.DataSource = tagList;
                            }


                        }
                        //TagOutput1.DataSource = xm.LoadedProject.Tags.Where(T => T.Label.ToString() == "Real").Select(N => N.Tag).ToList();//XMProValidator.FillTagOperands("Real");
                        else
                            TagOutput1.DataSource = XMProValidator.FillTagOperands("Bool");
                    }
                    else if (Instruction.Contains("MQTT"))
                    {
                        var PubRequest = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
                        //Publish PubRequest = (Publish)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").OrderBy(d => d.Name).FirstOrDefault();
                        //Subscribe SubRequest = (Subscribe)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").OrderBy(d => d.Name).FirstOrDefault();
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
                    else
                    {
                        if (selectedInstruction.OutputDataTypes.Count() == 1)
                        {
                            //Get All Tags with Word Addresses
                            var TagOP1 = XMProValidator.FillTagOperands(selectedInstruction.OutputDataTypes.Select(D => D.Text).FirstOrDefault(), udfbname);
                            //Remove Tags with Default IO List Type
                            var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                            //Bind the inproved list
                            TagOutput1.DataSource = TagOP1;
                        }
                        else
                        {
                            //Get All Tags with Word Addresses
                            var TagOP1 = XMProValidator.FillTagOperands(((DataType)comboBoxDataType.SelectedItem).Text, udfbname);
                            //Remove Tags with Default IO List Type
                            var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                            //Bind the inproved list
                            if (NonDTags > 1)
                                TagOP1.AddRange(xm.LoadedProject.Tags.Where(T => T.IoList == Core.Types.IOListType.Default && T.Tag.StartsWith("WRITE")).Select(N => N.Tag).ToList());
                            TagOutput1.DataSource = TagOP1;
                        }

                    }
                }
                else
                {
                    TagOutput1.Enabled = false;
                }
                //Checking for the Udfb LOGIC SCREEN

                string stringName = xm.CurrentScreen.ToString();
                string[] parts = stringName.Split('#');
                string formName = parts[0];
                string logicBlockName = parts[1];
                if (Output2.Enabled == true)
                {
                    TagOutput2.Enabled = true;
                    if (Instruction != "Timer TOFF" && Instruction != "Timer TON" && Instruction != "Timer TP" && Instruction != "Timer RTON" && Instruction != "Counter")
                    {
                        if (selectedInstruction.OutputDataTypes.Where(R => R.ToString().Equals("Bool")).Count() > 0 && Instruction.ToString() != "Scale")
                        {
                            TagOutput2.DataSource = XMProValidator.FillTagOperands("Bool");
                        }
                        else if (Instruction.ToString() == "Scale")
                        {
                            if (stringName.Contains("UDFLadderForm"))
                            {
                                var outputAdd = XMProValidator.FillTagOperands("Real", udfbname);
                                var NonDTags = outputAdd.RemoveAll(DefaultAddedTag);
                                TagOutput2.DataSource = outputAdd;
                            }
                            else
                            {
                                TagOutput2.DataSource = XMProValidator.FillTagOperands("Real");
                            }
                        }
                        else if (Instruction.ToString() == "RTON")
                        {
                            var Tag = xm.LoadedProject.Tags.Where((d) => d.LogicalAddress.Contains(":") || d.LogicalAddress.StartsWith("W4") && d.LogicalAddress.StartsWith("C7") && d.LogicalAddress.StartsWith("T6") && d.Retentive == true);//Checking the enter is retentive
                            TagOutput2.DataSource = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("W4") || T.LogicalAddress.StartsWith("C7") || T.LogicalAddress.StartsWith("T6") && T.Retentive);

                        }
                    }
                    else if (Instruction == "Timer RTON")
                    {
                        //List<XMIOConfig> retentiveTagList = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("W4") && T.Retentive).ToList();
                        TagOutput2.DataSource = XMProValidator.FillTagOperands("Word-Rentive"); ;
                        //var T = xm.LoadedProject.Tags.Where((d) => d.LogicalAddress.Equals(Output2.Text.ToString()));
                    }
                    else
                    {
                        if (stringName.Contains("UDFLadderForm"))
                        {
                            var outputAdd = XMProValidator.FillTagOperands("TCAddress", udfbname);
                            var NonDTags = outputAdd.RemoveAll(DefaultAddedTag);
                            TagOutput2.DataSource = outputAdd;
                        }
                        else
                        {
                            TagOutput2.DataSource = XMProValidator.FillTagOperands("TCAddress");
                        }
                    }

                }
                else
                {
                    TagOutput2.Enabled = false;
                    Output2.Text = "";
                }
                if (Output3.Enabled == true)                                             //Scale Output 3 is ---> Bool
                {
                    if (Instruction.ToString() == "Scale")
                    {
                        if (stringName.Contains("UDFLadderForm"))
                        {
                            var outputAdd = XMProValidator.FillTagOperands("Bool", udfbname);
                            var NonDTags = outputAdd.RemoveAll(DefaultAddedTag);
                            TagOutput3.DataSource = outputAdd;
                            TagOutput3.Enabled = true;
                        }
                        else
                        {
                            TagOutput3.DataSource = XMProValidator.FillTagOperands("Bool");
                            TagOutput3.Enabled = true;
                        }
                    }
                }
                else
                {
                    TagOutput3.Enabled = false;
                    Output3.Text = "";
                }
            }
        }

        private bool DefaultAddedTag(string LogicalAddress)
        {
            if (xm.LoadedProject.Tags.Where(T => T.Tag == LogicalAddress && T.IoList == Core.Types.IOListType.Default).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void comboBoxInstructionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeInstructionType();
            comboBoxInstruction_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// Intruction Type selection change event 
        /// </summary>
        private void ChangeInstructionType()
        {
            InstructionType selectedInstructionType = ((InstructionType)comboBoxInstructionType.SelectedValue);
            comboBoxInstruction.DataSource = Instruction.List.Where(
                i => i.InstructionType == selectedInstructionType.ID).ToList();
            comboBoxInstruction.DisplayMember = "Text";
            comboBoxInstruction.ValueMember = "ID";
            CheckandFillTags();
            checkBoxEnable.Checked = isEnabled;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XM-Pro PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Please select valid topic", "XM-Pro PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (!CheckDatatype())
            {
                if (!comboBoxInstructionType.Text.StartsWith("Timer") && !comboBoxInstructionType.Text.StartsWith("Counter") && !comboBoxInstructionType.Text.Contains("Pack"))
                {
                    MessageBox.Show("Data Type selected is not maching with selected Tag", "XM-Pro PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (!AreMinimumInputsRequiredSupplied())
            {
                MessageBox.Show("Please enter minimum no. of inputs required for this instruction", "XM-Pro PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var Logicname = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == this.Output2.Text && !d.Retentive).FirstOrDefault();
            if (Logicname != null && this.comboBoxDataType.Text == "RTON")
            {
                DialogResult dialogResult = MessageBox.Show("Tag logical address is already used and not retentive", "XM-Pro PLC", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    Logicname.Retentive = true;
                    Logicname.RetentiveAddress = CommonFunctions.GetRetentiveAddress(this.Output2.Text,"");
                }
                else if (dialogResult == DialogResult.No)
                {

                }
                return;
            }
            else
            {

            }
            //Addding Condition for the Pack Instruction to Fix next 15 address by using Current Address
            if (comboBoxInstructionType.SelectedItem.ToString() == "Pack")
            {
                string firstLogicalAdd = textBoxOp1Address.Text.ToString();
                XMIOConfig firstTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == firstLogicalAdd).FirstOrDefault();
                if (firstTag == null)
                {
                    string tc_Name = "";
                    string extraName = string.Empty;
                    if (edit)
                    {
                        LadderElement ld = LadderDesign.ClickedElement;
                        tc_Name = ld.Attributes["TCName"].ToString();
                        int usedTagCount = xm.LoadedProject.Tags.Where(T => T.ActualName != null && T.ActualName.StartsWith(tc_Name)).Any() ? xm.LoadedProject.Tags.Where(T => T.ActualName != null && T.ActualName.StartsWith(tc_Name)).Count() : 0;
                        int totalNo = usedTagCount / 16;
                        if (totalNo > 0)
                            extraName ="_"+totalNo.ToString();
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
            if (comboBoxInstructionType.SelectedItem.ToString() == "UnPack")
            {
                string firstLogicalAdd = textBoxOutputAddress.Text.ToString();
                XMIOConfig firstTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == firstLogicalAdd).FirstOrDefault();
                if (firstTag == null)
                {
                    string tc_Name = "";
                    string extraName = string.Empty;
                    if (edit)
                    {
                        LadderElement ld = LadderDesign.ClickedElement;
                        tc_Name = ld.Attributes["TCName"].ToString();
                        int usedTagCount = xm.LoadedProject.Tags.Where(T => T.ActualName != null && T.ActualName.StartsWith(tc_Name)).Any() ? xm.LoadedProject.Tags.Where(T => T.ActualName != null && T.ActualName.StartsWith(tc_Name)).Count() : 0;
                        int totalNo = usedTagCount / 16;
                        if (totalNo > 0)
                            extraName = "_" + totalNo.ToString();
                    }
                    TagsUserControl userControl = new TagsUserControl(firstLogicalAdd, tc_Name + extraName, "UnPack");
                }
                else
                {
                    MessageBox.Show("Tag is Already Used", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            try
            {
                if (checkAndAddAddressToTag())
                {
                    string type = comboBoxInstructionType.SelectedItem.ToString();
                    if (!edit)
                    {
                        if (type.StartsWith("Timer") || type.StartsWith("Counter"))
                        {
                            Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);
                            var currentOpCode = ((((Instruction)comboBoxInstruction.SelectedItem).ID + ((DataType)comboBoxDataType.SelectedItem).ID));
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
                                AddRows(Linenumber, _curRung.TC_Name, edit);
                            }
                        }
                        else
                        {
                            AddRows(Linenumber, _curRung.TC_Name, edit);
                        }
                    }
                    else
                    {
                        AddRows(Linenumber, _curRung.TC_Name, edit);
                    }
                }
                else
                {
                    return;
                }
                xm.MarkProjectModified(true);
                this.ParentForm.Close();
                this.ParentForm.DialogResult = DialogResult.OK;
                this.ClearInputControls();
                comboBoxInstructionType.SelectedIndex = 0;

            }
            catch (TimerMaxLimitExceedException timerException)
            {
                MessageBox.Show(timerException.Message, "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckDatatype()
        {
            string datatype = comboBoxDataType.Text.ToString();
            bool done = true;
            if (done && textBoxOp1Address.TextLength > 0)
            {
                if (textBoxOp1Address.Text.Contains(":"))
                {
                    if (xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOp1Address.Text.ToString() && (A.Model == "" || A.Model == null)).Count() > 0)
                    {
                        var IOLabel = (XMIOConfig)xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOp1Address.Text.ToString()).First();
                        if (datatype != IOLabel.Label.ToString())
                        {
                            done = false;
                        }
                    }
                }
            }
            if (done && textBoxOp2Address.TextLength > 0)
            {
                if (textBoxOp2Address.Text.Contains(":"))
                {
                    if (xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOp2Address.Text.ToString() && (A.Model == "" || A.Model == null)).Count() > 0)
                    {
                        var IOLabel = (XMIOConfig)xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOp2Address.Text.ToString()).First();
                        if (datatype != IOLabel.Label.ToString())
                        {
                            done = false;
                        }
                    }
                }
            }
            if (done && textBoxOp3Address.TextLength > 0)
            {
                if (textBoxOp3Address.Text.Contains(":"))
                {
                    if (xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOp3Address.Text.ToString() && (A.Model == "" || A.Model == null)).Count() > 0)
                    {
                        var IOLabel = (XMIOConfig)xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOp3Address.Text.ToString()).First();
                        if (datatype != IOLabel.Label.ToString())
                        {
                            done = false;
                        }
                    }
                }
            }
            if (done && textBoxOp4Address.TextLength > 0)
            {
                if (textBoxOp4Address.Text.Contains(":"))
                {
                    if (xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOp4Address.Text.ToString() && (A.Model == "" || A.Model == null)).Count() > 0)
                    {
                        var IOLabel = (XMIOConfig)xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOp4Address.Text.ToString()).First();
                        if (datatype != IOLabel.Label.ToString())
                        {
                            done = false;
                        }
                    }
                }
            }
            if (done && textBoxOutputAddress.TextLength > 0)
            {
                //if (textBoxOutputAddress.Text.Contains(":"))
                //{
                //    if (xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOutputAddress.Text.ToString() && (A.Model == "" || A.Model == null)).Count() > 0)
                //    {
                //        var IOLabel = (XMIOConfig)xm.LoadedProject.Tags.Where(A => A.LogicalAddress == textBoxOutputAddress.Text.ToString()).First();
                //        if (datatype != IOLabel.Label.ToString())
                //        {
                //            done = false;
                //        }
                //    }
                //}
            }
            if (done && Output2.TextLength > 0)
            {
                //if (Output2.Text.Contains(":"))
                //{
                //    if (xm.LoadedProject.Tags.Where(A => A.LogicalAddress == Output2.Text.ToString() && (A.Model == "" || A.Model == null)).Count() > 0)
                //    {
                //        var IOLabel = (XMIOConfig)xm.LoadedProject.Tags.Where(A => A.LogicalAddress == Output2.Text.ToString()).First();
                //        if (datatype != IOLabel.Label.ToString())
                //        {
                //            done = false;
                //        }
                //    }
                //}
            }
            //-----> Add Output 3
            return done;
        }

        private bool checkAndAddAddressToTag()
        {
            bool done = true;
            if (textBoxEnable.TextLength > 0)
            {
                if (textBoxEnable.Text.Contains(":")) done = CheckandAddLogicalAddress(textBoxEnable.Text.ToString(), textBoxEnable);
            }
            if (done && textBoxOp1Address.TextLength > 0)
            {
                if (textBoxOp1Address.Text.Contains(":")) done = CheckandAddLogicalAddress(textBoxOp1Address.Text.ToString(), textBoxOp1Address);
            }
            if (done && textBoxOp2Address.TextLength > 0)
            {
                if (textBoxOp2Address.Text.Contains(":")) done = CheckandAddLogicalAddress(textBoxOp2Address.Text.ToString(), textBoxOp2Address);
            }
            if (done && textBoxOp3Address.TextLength > 0)
            {
                if (textBoxOp3Address.Text.Contains(":")) done = CheckandAddLogicalAddress(textBoxOp3Address.Text.ToString(), textBoxOp3Address);
            }
            if (done && textBoxOp4Address.TextLength > 0)
            {
                if (textBoxOp4Address.Text.Contains(":")) done = CheckandAddLogicalAddress(textBoxOp4Address.Text.ToString(), textBoxOp4Address);
            }
            if (done && textBoxOp5Address.TextLength > 0)
            {
                if (textBoxOp5Address.Text.Contains(":")) done = CheckandAddLogicalAddress(textBoxOp5Address.Text.ToString(), textBoxOp5Address);
            }
            if (done && textBoxOutputAddress.TextLength > 0)
            {
                if (textBoxOutputAddress.Text.Contains(":")) done = CheckandAddLogicalAddress(textBoxOutputAddress.Text.ToString(), textBoxOutputAddress);
            }
            if (done && Output2.TextLength > 0)
            {
                if (Output2.Text.Contains(":")) done = CheckandAddLogicalAddress(Output2.Text.ToString(), Output2);
            }
            //Add Output 3 ---->
            if (done && Output3.TextLength > 0)
            {
                if (Output3.Text.Contains(":")) done = CheckandAddLogicalAddress(Output3.Text.ToString(), Output2);
            }
            return done;
        }

        private bool CheckandAddLogicalAddress(string LogicalAddress, Control control)
        {
            var tag = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == LogicalAddress).FirstOrDefault();
            if (tag == null)
            {
                if (LogicalAddress.StartsWith("F2") || LogicalAddress.StartsWith("S3") || LogicalAddress.StartsWith("W4") || LogicalAddress.StartsWith("P5") || LogicalAddress.StartsWith("T6") || LogicalAddress.StartsWith("C7"))
                {
                    string currentLoadedScreen = xm.CurrentScreen.ToString();
                    XMProForm tempForm = new XMProForm();
                    tempForm.StartPosition = FormStartPosition.CenterParent;
                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    tempForm.Text = "Add New Address Added in Logic";
                    TagsUserControl userControl;
                    Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);
                    if (textBoxOutputAddress.Text == LogicalAddress && selectedInstruction.OutputDataTypes.Count() > 0)
                    {
                        userControl = new TagsUserControl(isRetentive, LogicalAddress, selectedInstruction.OutputDataTypes.FirstOrDefault().ToString());

                    }
                    else if (currentLoadedScreen.StartsWith("UDFLadder"))
                    {
                        string[] splitResult = currentLoadedScreen.Split(new string[] { "#" }, StringSplitOptions.None);
                        string udfbName = splitResult[1];
                        string actualUdfbName = udfbName.Replace("Logic", "Tags");
                        userControl = new TagsUserControl(LogicalAddress, "", actualUdfbName, comboBoxDataType.Text.ToString());
                    }
                    else
                    {
                        // if Tag is Retentive then to check checkbox default chkIsChecked 
                        // var s = xm.LoadedProject.Tags.Where(A Output2.Text.GetType().Equals("Word"));
                        if (comboBoxInstructionType.Text.Equals("Timer RTON") && LogicalAddress.Equals(Output2.Text.ToString()))
                        {
                            isRetentive = true;
                        }

                        //in EXP block show real instead of byte at output1 if address start with P5
                        string datatype = comboBoxDataType.Text.ToString();
                        if (control.Text == textBoxOutputAddress.Text && (selectedInstruction.Text.ToString() == "EXP" && textBoxOutputAddress.Text.StartsWith("P5")))
                        {
                            datatype = "Real";
                        }
                        //Change DataType of Output Address of PackInstruction
                        if (control.Text == textBoxOutputAddress.Text && (selectedInstruction.Text.ToString() == "Pack" && textBoxOutputAddress.Text.StartsWith("W4")))
                        {
                            datatype = "Word";
                        }
                        //Change DataType of Input Address of UnPackInstruction
                        if (control.Text == textBoxOp1Address.Text && (selectedInstruction.Text.ToString() == "UnPack" && textBoxOp1Address.Text.StartsWith("W4")))
                        {
                            datatype = "Word";
                        }
                        if (control.Text.StartsWith("F2"))
                            datatype = "Bool";

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
                        errorProvider.SetError(control, null);
                        return true;
                    }
                    else
                    {
                        errorProvider.SetError(control, "Invalid Logical Address Selected it is not added in Tags/Devices");
                        return false;
                    }
                }
            }
            else
            {

                errorProvider.SetError(control, null);
                return true;
            }

        }


        /// <summary>
        /// Add rows to database by adding the values to class object of type ApplicationRung
        /// </summary>
        /// <param name="linenumber">Integer this parameter will tell at what line record should get added 
        /// this be differant on click of different buttons like on Insert After click it will take the Line number which 
        /// is selected before and add one to it and then send this line number and like wise.</param>
        /// <param name="counterValue">This parameter is used in specific conditions like Time or Counter instruction is Updated or Insert After Click.</param>
        /// <param name="addingNewRow">This parameter will specify whether to add or update the line.</param>
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

                // applicationRecs = (ApplicationRung)xm.LoadedProject.LogicRungs.Add ;

                string instruction = ((Instruction)comboBoxInstruction.SelectedItem).Text;

                var opcode = ((((Instruction)comboBoxInstruction.SelectedItem).ID + ((DataType)comboBoxDataType.SelectedItem).ID));
                string Dcode = null;
                if (comboBoxInstructionType.Text.Contains("Data Conversion"))
                {
                    Dcode = (((Instruction)comboBoxInstruction.SelectedItem).ID.ToString("X").Substring(0, 1) + ((DataType)comboBoxDataType.SelectedItem).ID.ToString("X")) + ((Instruction)comboBoxInstruction.SelectedItem).ID.ToString("X").Substring(3, 1).ToString();
                }

                ApplicationRung AppRecs = new ApplicationRung();
                AppRecs.WindowName = xm.CurrentScreen.ToString();
                AppRecs.Name = AppRecs.WindowName;
                AppRecs.LineNumber = linenumber;
                if (Edit)
                {
                    //counterValue = xm.LoadedProject.LogicRungs.Where(R => R.WindowName == xm.CurrentScreen.ToString() && R.LineNumber == Linenumber && R.OpCode == $"{opcode:X4}").Select(R1 => R1.TC_Name).FirstOrDefault();
                    counterValue = tc_instuction;
                }
                if (counterValue == "" || counterValue == null)
                {
                    AppRecs.TC_Name = IncreaseTimerCounter(instruction);
                    //for Adding TC_Name for the Pack Instruction FB
                    if (comboBoxInstruction.SelectedItem.ToString().Equals("Pack"))
                    {
                        AppRecs.TC_Name = IncreaePackInstructorCounter(instruction);
                    }
                    if (comboBoxInstruction.SelectedItem.ToString().Equals("UnPack"))
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
                    string oldOPcode = sel_opcode; // xm.LoadedProject.LogicRungs.Where(R => R.WindowName == xm.CurrentScreen.ToString() && R.LineNumber == Linenumber).Select(R1 => R1.OpCodeNm).FirstOrDefault();
                    string newcode = Dcode is null ? $"{opcode:X4}" : "0" + Dcode;
                    if (newcode == oldOPcode)
                    {
                        string newTcName = IncreaseTimerCounter(instruction, false);
                        if (newTcName != "-")
                            AppRecs.TC_Name = counterValue;
                        //Addition Additional Checks for the edit Pack and Unpack Instruction
                        else if (newTcName == "-" && (oldOPcode == "0390" || oldOPcode == "03A2" || oldOPcode == "03B2"))
                            AppRecs.TC_Name = counterValue;
                        else
                            AppRecs.TC_Name = "-";
                    }
                    else
                        AppRecs.TC_Name = comboBoxInstructionType.Text.Contains("MQTT") ? IncreaseMQTTCounter(instruction) : IncreaseTimerCounter(instruction);
                }  // -----------> Adding of new input and new Output
                AppRecs.OutPutType_NM = comboBoxOutputType.SelectedItem.ToString();
                AppRecs.OutputType = $"{((OutputType)comboBoxOutputType.SelectedItem).ID:X2}";
                AppRecs.DataType_Nm = comboBoxDataType.SelectedItem.ToString();
                AppRecs.DataType = $"{((DataType)comboBoxDataType.SelectedItem).ID:X4}";
                AppRecs.Enable = checkBoxEnable.Checked ? "Enabled" : "-";

                //Adding Random Hardcoded Values For Mqtt FB
                AppRecs.Comments = textBoxComment.Text.Replace(',', ' ');
                if (edit && AppRecs.TC_Name != "-")
                {
                    xm.LoadedProject.LogicRungs.RemoveAll(d => d.TC_Name == AppRecs.TC_Name && d.OpCode == AppRecs.OpCode);
                }
                // Add or Update records to SQLlite DB 
                xm.LoadedProject.AddRung(AppRecs);
                //applicationRecs.AddRungs(AppRecs);
                if (!edit && !insertafter)
                {
                    Form frmTemp = xm.LoadedScreens[AppRecs.WindowName];
                    ((IXMForm)frmTemp).OnShown();
                }
                //frmTemp.Dispose();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "XM-Pro PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
            var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCode == counterValue);
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

        /// <summary>
        /// Add Retentive Variable if it is not included in Retentive Variable Block
        /// </summary>
        /// <param name="RetAddress"></param> Retentive Address declared by user
        private void checkandaddRetentiveVariable(string RetAddress)
        {
            //if (RetAddress.StartsWith("X8") || RetAddress.StartsWith("Y9"))
            //{
            //    if (SQLiteDataAccess.CheckIfRetentiveAddressExists(RetAddress))
            //    {
            //        RetentiveAddressList RetAdd = new RetentiveAddressList();
            //        RetAdd.Ret_Key = Convert.ToInt32(SQLiteDataAccess.GetRetentiveVariableKey());
            //        RetAdd.Logical_Address = "-";
            //        RetAdd.RetAdd = RetAddress.ToString();
            //        SQLiteDataAccess.saveRetentiveAddressrecords(RetAdd);
            //    }
            //}
        }

        /// <summary>
        /// Bind the datatable to the grid
        /// </summary>        
        private void allocate()
        {
        }

        /// <summary>
        /// Fill Controls with the data from the selected row of grid
        /// </summary>

        private void fillControlsWithSelectedRow(ApplicationRung Rung)
        {
            var dataTable = Rung; // applicationRecs.ApplicationRungs.Select(R => R.LineNumber == Linenumber); // SQLiteDataAccess.ToDataTable(AppData);
            ApplicationRung selectedRow = (ApplicationRung)dataTable;
            //if (dataTable.Count() <= rowIndex)
            //    selectedRow = dataTable.Select(R => R.GetType().Name == Linenumber) ;
            //else
            //    selectedRow = dataTable.Rows[rowIndex];

            //if (selectedRow.RowState == DataRowState.Deleted ||
            //    selectedRow.RowState == DataRowState.Detached)
            //    return;

            comboBoxOutputType.SelectedIndex = OutputType.List.FindIndex(
                i => i.ID == Convert.ToInt32(selectedRow.OutputType.ToString()));

            var dataTypeInteger = Int32.Parse(selectedRow.DataType.ToString(), System.Globalization.NumberStyles.HexNumber);

            // Read Op Code from current Row
            var opCodeInHex = selectedRow.OpCode.ToString();
            int instructionCode = 0;
            //Adding Check for DataConversion Instruction after Copy Paste Scenario
            //if (opCodeInHex.ToString().StartsWith("02") && (selectedRow.Input2 == "-" || selectedRow.Input2 == "") && char.IsLetter(opCodeInHex.ToString().Substring(2, 1).First()))
            //{
            //    instructionCode = Int32.Parse(opCodeInHex.Replace(opCodeInHex.Substring(2, 1), "00"), System.Globalization.NumberStyles.HexNumber);
            //}
            //else
            //{
            //    // Convert the op code from hex to decimal integer
            //    var opCodeInteger = Int32.Parse(opCodeInHex, System.Globalization.NumberStyles.HexNumber);
            //    // Subtract data type value from OpCode to obtain instruciton code value
            //    instructionCode = opCodeInteger - dataTypeInteger;
            //}
            Instruction selectedInstruction = Instruction.List.Find(i => i.ID == instructionCode);
            comboBoxInstructionType.SelectedIndex = selectedInstruction.InstructionType;
            //disabling Input1 and Output1 for Pack and Unpacked Instruction
            if (opCodeInHex == "0390" && edit)
            {
                comboBoxInstructionType.Enabled = false;
            }
            if (opCodeInHex == "03A2" && edit)
            {
                comboBoxInstructionType.Enabled = false;
            }
            ChangeInstructionType();
            comboBoxInstruction.SelectedValue = instructionCode;
            comboBoxInstruction.SelectedText = selectedInstruction.Text;
            //if (comboBoxInstruction.SelectedText != "")

            // Combox not selecting by index because there is no data source attached to it
            comboBoxDataType.DataSource = selectedInstruction.SupportedDataTypes;
            comboBoxDataType.SelectedIndex = selectedInstruction.SupportedDataTypes.FindIndex(i => i.ID == Int32.Parse(selectedRow.DataType.ToString(), System.Globalization.NumberStyles.HexNumber));

            if (!string.IsNullOrEmpty(selectedRow.Enable.ToString().TrimStart('-')))
                checkBoxEnable.Checked = true;
            else
                checkBoxEnable.Checked = false;
            textBoxEnable.Text = selectedRow.Enable.ToString().TrimStart('-').TrimStart('~');
            if (selectedRow.Enable.ToString().StartsWith("~"))
            {
                OperationEnable.SelectedIndex = 2;
                labelNegationEnable.Visible = true;
            }
            else if (string.IsNullOrEmpty(selectedRow.Enable.ToString()) ||
                selectedRow.Enable.ToString().Equals("-") ||
                selectedRow.Enable.ToString().Contains(':'))
            {
                OperationEnable.SelectedIndex = 0;
                labelNegationEnable.Visible = false;
            }
            else
            {
                OperationEnable.SelectedIndex = 1;
                labelNegationEnable.Visible = false;
            }

            //textBoxOutputAddress.Text = selectedRow.Output1.ToString();
            //if (!selectedRow.Output2.Contains("-")) Output2.Text = selectedRow.Output2.ToString();
            //if (!selectedRow.Output3.Contains("-")) Output3.Text = selectedRow.Output3.ToString();
            //if (!selectedInstruction.Text.StartsWith("MQTT"))
            //{
            //    textBoxOp1Address.Text = selectedRow.Input1.ToString().Equals("-") ? selectedRow.Input1.ToString().TrimStart('-') : selectedRow.Input1.ToString().TrimStart('~');
            //    textBoxOp2Address.Text = selectedRow.Input2.ToString().Equals("-") ? selectedRow.Input2.ToString().TrimStart('-') : selectedRow.Input2.ToString().TrimStart('~');
            //    textBoxOp3Address.Text = selectedRow.Input3.ToString().Equals("-") ? selectedRow.Input3.ToString().TrimStart('-') : selectedRow.Input3.ToString().TrimStart('~');
            //    textBoxOp4Address.Text = selectedRow.Input4.ToString().Equals("-") ? selectedRow.Input4.ToString().TrimStart('-') : selectedRow.Input4.ToString().TrimStart('~');
            //    textBoxOp5Address.Text = selectedRow.Input5.ToString().Equals("-") ? selectedRow.Input5.ToString().TrimStart('-') : selectedRow.Input5.ToString().TrimStart('~');
            //    //textBoxComment.Text = selectedRow.Comments.ToString();

            //    if (selectedRow.Input1.ToString().StartsWith("~"))
            //    {
            //        OperationType1.SelectedIndex = 2;
            //        labelNegation1.Visible = true;
            //    }
            //    else if (string.IsNullOrEmpty(selectedRow.Input1.ToString()) ||
            //        selectedRow.Input1.ToString().Equals("-") ||
            //        selectedRow.Input1.ToString().Contains(':'))
            //    {
            //        OperationType1.SelectedIndex = 0;
            //        labelNegation1.Visible = false;
            //    }
            //    else
            //    {
            //        OperationType1.SelectedIndex = 1;
            //        labelNegation1.Visible = false;
            //    }

            //    if (selectedRow.Input2.ToString().StartsWith("~"))
            //    {
            //        OperationType2.SelectedIndex = 2;
            //        labelNegation2.Visible = true;
            //    }
            //    else if (string.IsNullOrEmpty(selectedRow.Input2.ToString()) ||
            //        selectedRow.Input2.ToString().Equals("-") ||
            //        selectedRow.Input2.ToString().Contains(':'))
            //    {
            //        OperationType2.SelectedIndex = 0;
            //        labelNegation2.Visible = false;
            //    }
            //    else
            //    {
            //        OperationType2.SelectedIndex = 1;
            //        labelNegation2.Visible = false;
            //    }

            //    if (selectedRow.Input3.ToString().StartsWith("~"))
            //    {
            //        OperationType3.SelectedIndex = 2;
            //        labelNegation3.Visible = true;
            //    }
            //    else if (string.IsNullOrEmpty(selectedRow.Input3.ToString()) ||
            //        selectedRow.Input3.ToString().Equals("-") ||
            //        selectedRow.Input3.ToString().Contains(':'))
            //    {
            //        OperationType3.SelectedIndex = 0;
            //        labelNegation3.Visible = false;
            //    }
            //    else
            //    {
            //        OperationType3.SelectedIndex = 1;
            //        labelNegation3.Visible = false;
            //    }

            //    if (selectedRow.Input4.ToString().StartsWith("~"))
            //    {
            //        OperationType4.SelectedIndex = 2;
            //        labelNegation4.Visible = true;
            //    }
            //    else if (string.IsNullOrEmpty(selectedRow.Input4.ToString()) ||
            //        selectedRow.Input4.ToString().Equals("-") ||
            //        selectedRow.Input4.ToString().Contains(':'))
            //    {
            //        OperationType4.SelectedIndex = 0;
            //        labelNegation4.Visible = false;
            //    }
            //    else
            //    {
            //        OperationType4.SelectedIndex = 1;
            //        labelNegation4.Visible = false;
            //    }
            //    //Entry for Input 5 ----->
            //    if (selectedRow.Input5.ToString().StartsWith("~"))
            //    {
            //        OperationType5.SelectedIndex = 2;
            //        labelNegation5.Visible = true;
            //    }
            //    else if (string.IsNullOrEmpty(selectedRow.Input5.ToString()) ||
            //        selectedRow.Input5.ToString().Equals("-") ||
            //        selectedRow.Input5.ToString().Contains(':'))
            //    {
            //        OperationType5.SelectedIndex = 0;
            //        labelNegation5.Visible = false;
            //    }
            //    else
            //    {
            //        OperationType5.SelectedIndex = 1;
            //        labelNegation5.Visible = false;
            //    }
            //}
            //comboBoxOutputType.SelectedIndex = OutputType.List.FindIndex(
            //    i => i.ID == Convert.ToInt32(selectedRow.OutputType.ToString()));

            ////textBoxOutputAddress.Text = selectedRow.Output1.ToString();
            //if (!selectedRow.Output2.Contains("-") && !selectedInstruction.Text.StartsWith("MQTT")) Output2.Text = selectedRow.Output2.ToString();
            //if (selectedInstruction.Text.StartsWith("MQTT") && selectedRow.Output2.ToString() != "-") SetTopicValue(selectedRow.Output2.ToString());
            //if (!selectedRow.Output3.Contains("-")) Output3.Text = selectedRow.Output3.ToString();
            //textBoxOutputAddress.Text = selectedRow.Output1.ToString();
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

            //int index = 0;
            //foreach(TopicList topicList in cmbTopic.Items)
            //{
            //    if (topicList.ID.ToString().Equals(ID))
            //        cmbTopic.SelectedIndex = index;
            //    index++;
            //}
        }

        private void comboBoxNegOption1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OperationType1.SelectedIndex == 2)
                labelNegation1.Visible = true;
            else
                labelNegation1.Visible = false;
            // Use following sequence of switching focus to cause re-validation of operand
            if (OperationType1.SelectedIndex == 1)
            {
                TagOperand1.Enabled = false;
                textBoxOp1Address.Focus();
            }
            else
            {
                TagOperand1.Enabled = true;
                TagOperand1.Focus();
            }
        }

        private void comboBoxNegOption2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OperationType2.SelectedIndex == 2)
                labelNegation2.Visible = true;
            else
                labelNegation2.Visible = false;
            // Use following sequence of switching focus to cause re-validation of operand
            if (OperationType2.SelectedIndex == 1)
            {
                TagOperand2.Enabled = false;
                textBoxOp2Address.Focus();
            }
            else
            {
                TagOperand2.Enabled = true;
                TagOperand2.Focus();
            }
        }

        private void comboBoxNegOption3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OperationType3.SelectedIndex == 2)
                labelNegation3.Visible = true;
            else
                labelNegation3.Visible = false;
            // Use following sequence of switching focus to cause re-validation of operand
            if (OperationType3.SelectedIndex == 1)
            {
                TagOperand3.Enabled = false;
                textBoxOp3Address.Focus();
            }
            else
            {
                TagOperand3.Enabled = true;
                TagOperand3.Focus();
            }


        }

        private void comboBoxNegOption4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OperationType4.SelectedIndex == 2)
                labelNegation4.Visible = true;
            else
                labelNegation4.Visible = false;

            // Use following sequence of switching focus to cause re-validation of operand
            if (OperationType4.SelectedIndex == 1)
            {
                TagOperand4.Enabled = false;
                textBoxOp4Address.Focus();
            }
            else
            {
                TagOperand4.Enabled = true;
                TagOperand4.Focus();
            }



        }

        /// <summary>
        /// Initialise timer counter with minimum and maximum bounds
        /// </summary>
        private void InitializeTimerCounters()
        {
            //Counters.Add("0.01s TON", new Counter { Instruction = "0.01s TON", CurrentPosition = 0, StartingPosition = 0, Maximum = 49 });
            //Counters.Add("0.1s TON", new Counter { Instruction = "0.1s TON", CurrentPosition = 50, StartingPosition = 50, Maximum = 199 });
            //Counters.Add("1s TON", new Counter { Instruction = "1s TON", CurrentPosition = 200, StartingPosition = 200, Maximum = 255 });
            //Counters.Add("0.01s TOFF", new Counter { Instruction = "0.01s TOFF", CurrentPosition = 0, StartingPosition = 0, Maximum = 49 });
            //Counters.Add("0.1s TOFF", new Counter { Instruction = "0.1s TOFF", CurrentPosition = 50, StartingPosition = 50, Maximum = 199 });
            //Counters.Add("1s TOFF", new Counter { Instruction = "1s TOFF", CurrentPosition = 200, StartingPosition = 200, Maximum = 255 });
            //Counters.Add("0.01s TP", new Counter { Instruction = "0.01s TP", CurrentPosition = 0, StartingPosition = 0, Maximum = 49 });
            //Counters.Add("0.1s TP", new Counter { Instruction = "0.1s TP", CurrentPosition = 50, StartingPosition = 50, Maximum = 199 });
            //Counters.Add("1s TP", new Counter { Instruction = "1s TP", CurrentPosition = 200, StartingPosition = 200, Maximum = 255 });

            //Counters.Add("CTU", new Counter { Instruction = "CTU", CurrentPosition = 0, StartingPosition = 0, Maximum = 127 });
            //Counters.Add("CTD", new Counter { Instruction = "CTD", CurrentPosition = 128, StartingPosition = 128, Maximum = 255 });

            //Counters.Add("0.01s RTON", new Counter { Instruction = "0.01s RTON", CurrentPosition = 256, StartingPosition = 256, Maximum = 270 });
            //Counters.Add("0.1s RTON", new Counter { Instruction = "0.1s RTON", CurrentPosition = 271, StartingPosition = 271, Maximum = 285 });
            //Counters.Add("1s RTON", new Counter { Instruction = "1s RTON", CurrentPosition = 286, StartingPosition = 286, Maximum = 300 });
            //Counters.Add("1m RTON", new Counter { Instruction = "1m RTON", CurrentPosition = 301, StartingPosition = 301, Maximum = 305 });
        }

        /// <summary>
        /// Increase timer counter depending on the selected type of Instruction 
        /// </summary>
        /// <param name="instruction">Selected Instruction.</param>
        /// <returns>Conter Value for the Instruction.</returns>
        private string IncreaseTimerCounter(string instruction, bool check = false)
        {
            string timerCounterLabel = "-";

            if (Counters.ContainsKey(instruction))
            {

                var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCodeNm == instruction);
                if (code != null && code.Count() > 0)
                {
                    var maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", ""))); // code.Max(C => C.TC_Name);
                    Counters[instruction].CurrentPosition = Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1;
                }

                if ((Counters[instruction].CurrentPosition <= Counters[instruction].Maximum) || !check)
                {
                    timerCounterLabel = Counters[instruction].ToString();
                    ++Counters[instruction].CurrentPosition;
                }
                else
                    throw new TimerMaxLimitExceedException(instruction);
            }
            else
            {
                if (instruction.Contains("MQTT")) return "SUB1";
            }
            return timerCounterLabel;
        }

        /// <summary>
        /// Decrease timer counter depending on the selected type of Instruction 
        /// </summary>        
        /// <param name="instruction">Selected Instruction.</param>
        /// <param name="counterValue">Counter Value for the Instruction.</param>
        private void DecreaseTimerCounter(string instruction, string counterValue)
        {
            if (Counters.ContainsKey(instruction))
            {
                --Counters[instruction].CurrentPosition;

                int deletedCounterValue = Convert.ToInt32(counterValue.TrimStart('T', 'C'));

                foreach (ApplicationRung row in AppData)
                {
                    if (row.OpCodeNm.ToString().Equals(instruction))
                    {
                        int counterValueForRow = Convert.ToInt32(row.TC_Name.ToString().TrimStart('T', 'C'));

                        if (counterValueForRow > deletedCounterValue)
                        {
                            --counterValueForRow;
                            row.TC_Name = instruction.StartsWith("C")
                                ? "C" + counterValueForRow.ToString()   // For Counter instructions i.e. CTD and CTU
                                : "T" + counterValueForRow.ToString();  // For Timer instructions

                            int linenumber = 0;
                            string TC_Name = "";
                            linenumber = Convert.ToInt32(row.LineNumber);
                            TC_Name = row.TC_Name.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validate operands depending on the type of control 
        /// </summary>
        /// <param name="control">Name of the control from whoes validate this call is generated.</param>
        /// <param name="e">This parameter will specify whether to add or update the line.</param>
        /// <returns>Describe return value.</returns>
        private void ValidateOperand(Control control, CancelEventArgs e)
        {
            bool validationSuccessful = true;
            string error = string.Empty;

            if (string.IsNullOrEmpty(control.Text))     // Allow untouched or emptied operands.
            {
                validationSuccessful = true;
            }
            else if (comboBoxInstructionType.SelectedItem.ToString() == "UnPack")
            {
                string address = textBoxOp1Address.Text;
                XMIOConfig tag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == address).FirstOrDefault();
                if (tag != null && tag.Label != "Word")
                {
                    error = "Invalid Word Address";
                    validationSuccessful = false;
                }
            }
            else
            {
                int operandType = GetOperandTypeForControl(control);
                switch (operandType)
                {

                    case 0:     // Normal i.e. Address
                    case 2:     // Negation Operand
                        validationSuccessful = ValidateAddressOperand(control, out error);
                        break;
                    case 1:     // Numeric Operand
                        validationSuccessful = ValidateNumericOperand(control, out error);
                        break;
                }
            }

            if (validationSuccessful)
            {
                e.Cancel = false;
                errorProvider.SetError(control, null);
            }
            else
            {
                if (udfbname != "")
                {
                    UDFBInfo uDFB = xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == udfbname).FirstOrDefault();
                    if (uDFB.UDFBlocks.Where(b => b.Text == control.Text && b.DataType == comboBoxDataType.Text.ToString()).Count() == 0)
                    {
                        e.Cancel = true;
                        errorProvider.SetError(control, error);
                    }
                    else
                    {
                        e.Cancel = false;
                        errorProvider.SetError(control, null);
                    }
                }
                else
                {
                    e.Cancel = true;
                    errorProvider.SetError(control, error);
                }

            }
            ///<>
            ///Adding Logic For the Checking Tags Location
            string CurrentLogicalBlock = xm.CurrentScreen.Split('#')[1];
            string input1Address = control.Text;
            XMIOConfig EnteredTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == input1Address).FirstOrDefault();
            if (EnteredTag != null)
            {
                if (CurrentLogicalBlock.Contains("LogicBlock") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !EnteredTag.Model.Contains("XM"))))
                {
                    if (EnteredTag.Model != "User Defined Tags" && EnteredTag.Model != "")
                    {
                        e.Cancel = true;
                        error = "Used Tag is Not Found in Current Logic Block";
                        errorProvider.SetError(control, error);
                    }
                }
                else if (CurrentLogicalBlock.EndsWith("Logic") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !EnteredTag.Model.Contains("XM"))))
                {
                    string UdfbName = CurrentLogicalBlock.Split(' ')[0];
                    if (EnteredTag.Model.ToString() != UdfbName + " Tags")
                    {
                        e.Cancel = true;
                        error = "Used Tag is Not Found in Current UDFB Block";
                        errorProvider.SetError(control, error);
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

            switch (((DataType)comboBoxDataType.SelectedItem).Text)
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
                // Timer data types
                case "TON":
                case "TOFF":
                case "TP":
                    if (control.Name.Equals("textBoxOp1Address"))
                    {
                        if (!number.Equals("1") && !number.Equals("0"))
                        {
                            error = "Invalid Bit Value";
                            return false;
                        }
                    }
                    else if (number.StartsWith("-") || !uint.TryParse(number, out _) || Convert.ToInt64(number) > 65535)
                    {
                        error = "Invalid Number";
                        return false;
                    }
                    break;
                // Counter data types
                case "CTU":
                case "CTD":
                    if (control.Name.Equals("textBoxOp3Address"))
                    {
                        if (number.StartsWith("-") || !ushort.TryParse(number, out _))
                        {
                            error = "Invalid Word Number";
                            return false;
                        }
                    }
                    else if (!number.Equals("1") && !number.Equals("0"))
                    {
                        error = "Invalid Bit Value";
                        return false;
                    }
                    break;
            }
            error = string.Empty;
            //if (comboBoxInstruction.SelectedItem.ToString() == "LIMIT")
            //{
            //    int val;
            //    if (control.Name.Equals("textBoxOp2Address"))
            //    {
            //        if (int.TryParse(textBoxOp1Address.Text.ToString(), out val))
            //        {
            //            if (textBoxOp1Address.TextLength > 1)
            //            {
            //                if (Convert.ToDecimal(textBoxOp2Address.Text.ToString()) > Convert.ToDecimal(textBoxOp1Address.Text.ToString()))
            //                {
            //                    //  error = "Invalid Limit Value";
            //                    return true;
            //                }
            //            }
            //        }
            //        if (int.TryParse(textBoxOp3Address.Text.ToString(), out val))
            //        {
            //            if (textBoxOp3Address.TextLength > 1)
            //            {
            //                if ((Convert.ToDecimal(textBoxOp2Address.Text.ToString()) < Convert.ToDecimal(textBoxOp3Address.Text.ToString())))
            //                {
            //                  //  error = "Invalid Limit Value";
            //                    return true;
            //                }
            //            }
            //        }
            //        if (int.TryParse(textBoxOp3Address.Text.ToString(), out val))
            //        {
            //            if (textBoxOp3Address.TextLength > 1)
            //            {
            //                if ((Convert.ToDecimal(textBoxOp2Address.Text.ToString()) < Convert.ToDecimal(textBoxOp3Address.Text.ToString())))
            //                {
            //                    //  error = "Invalid Limit Value";
            //                    return true;
            //                }
            //            }
            //        }
            //        else
            //            return true;
            //    }

            //}
            return true;
        }


        /// <summary>
        /// Validating Address type of operands
        /// </summary>
        /// <param name="control">Send the nmae of control.</param>        
        /// <returns>True if operand is valid else false.</returns>
        /// <returns>Error decription as String.</returns>
        private bool ValidateAddressOperand(Control control, out string error)
        {
            string address = control.Text;
            if (address == "-") address = control.Text;
            bool validationSuccessful;

            string dataType = ((DataType)comboBoxDataType.SelectedItem).Text;
            string Instruction = comboBoxInstructionType.SelectedItem.ToString();

            // Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);
            switch (dataType)
            {
                case "Bool":
                    error = "Invalid Bit Address";
                    validationSuccessful = address.IsValidBitAddress();
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
                case "Byte":
                    error = "Invalid Word address for Byte data type";
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
                // RTON data types
                case "RTON":
                    if (control.Name.Equals("textBoxOp1Address") || control.Name.Equals("textBoxOp2Address"))
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
                case "Pack":
                    if (control.Name.Equals("textBoxOp1Address"))
                    {
                        error = "Invalid Bit address";
                        validationSuccessful = address.IsValidBitAddress();
                    }
                    else
                    {
                        error = "Invalid Word addresss";
                        validationSuccessful = address.IsValidWordAddress();
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
                        error = "Invalid Word address";
                        validationSuccessful = address.IsValidWordAddress();
                    }
                    break;
            }

            if (validationSuccessful)
            {
                //var addedDataType = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == control.Text).FirstOrDefault();
                //if (addedDataType == null || addedDataType.Model != "" || (addedDataType.Label == null || addedDataType.Label == dataType))
                //{
                //    error = string.Empty;
                //    return true;
                //}
                //else
                //{
                //    error = "Data type not matching need to select " + dataType + " and this logical address has datatype " + addedDataType;
                //    return false;
                //}
                bool fbDataType = dataType == "TON" || dataType == "TOFF" || dataType == "RTON" || dataType == "TP" || dataType == "CTU" || dataType == "CTD";
                var addedDataType = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == control.Text).FirstOrDefault();
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

        /// <summary>
        /// Get the operand type of of control
        /// </summary>
        /// <param name="control">Send the nmae of control.</param>        
        /// <returns>Index of the selected operand type.</returns>
        private int GetOperandTypeForControl(Control control)
        {
            int operandType = 0;

            switch (control.Name)
            {
                case "textBoxOp1Address":
                    operandType = OperationType1.SelectedIndex;
                    break;
                case "textBoxOp2Address":
                    operandType = OperationType2.SelectedIndex;
                    break;
                case "textBoxOp3Address":
                    operandType = OperationType3.SelectedIndex;
                    break;
                case "textBoxOp4Address":
                    operandType = OperationType4.SelectedIndex;
                    break;
                case "textBoxOp5Address":
                    operandType = OperationType5.SelectedIndex;
                    break;
            }

            return operandType;
        }

        /// <summary>
        /// Validate the output Address depending on the control and the datatype and Out put type selected 
        /// </summary>
        /// <param name="control">Send the name of control.</param>        
        /// <param name="e">Cancel event handler.</param>        
        private void ValidateOutputAddress(Control control, CancelEventArgs e)
        {
            bool validationSuccessful;
            string error;
            string dataType = comboBoxDataType.SelectedItem != null ? ((DataType)comboBoxDataType.SelectedItem).Text : "";
            string outputType = ((OutputType)comboBoxOutputType.SelectedItem).Text;
            string address = control.Text;
            Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);
            if (selectedInstruction.Text == "EXP") dataType = "Real";
            //if (!SQLiteDataAccess.CheckIfRetentiveAddressExists(control.Text))
            //{
            //    address = SQLiteDataAccess.ReturnLogicalAddressOfRetetentiveAddress(control.Text);
            //}
            string Instruction = comboBoxInstructionType.SelectedItem.ToString();
            if (string.IsNullOrEmpty(address))
            {
                validationSuccessful = false;
                error = "Output address cannot be blank";
            }
            //Adding check for the Pack instruction for checking the first output as word
            else if (selectedInstruction.Text == "Pack")
            {
                error = "Invalid Word address";
                validationSuccessful = true;
                XMIOConfig outputTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == control.Text).FirstOrDefault();
                if (outputTag != null && outputTag.Label != "Word")
                {
                    validationSuccessful = false;
                }
                else
                {
                    if (address.IsValidWordAddress())
                    {
                        validationSuccessful = address.IsValidOutputWordAddress(outputType, xm.LoadedProject.Tags.Where(R => R.Type.ToString().Contains("Output")).ToList());
                        if (validationSuccessful)
                        {
                            if (xm.LoadedProject.Tags.Where(R => R.LogicalAddress == control.Text && R.IoList == Core.Types.IOListType.Default).Count() > 0)
                            {
                                validationSuccessful = false;
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

            else if (selectedInstruction.OutputDataTypes.Count == 1)
            {
                if (selectedInstruction.OutputDataTypes.Where(R => R.ToString().Equals("Bool")).Count() > 0)
                {
                    error = "Invalid Bit Address";
                    validationSuccessful = address.IsValidOutputBitAddress(outputType, xm.LoadedProject.Tags.Where(R => R.LogicalAddress.Contains(".") && R.Type.ToString().Contains("Output")).ToList());
                }
                else if (selectedInstruction.OutputDataTypes.Where(R => R.ToString().Equals("Real")).Count() > 0)
                {
                    if (outputType == "Memory Address Variable")
                    {
                        error = "Invalid Word address for Real data type";
                        validationSuccessful = address.IsValidRealWordAddress();
                    }
                    else
                    {
                        error = "Invalid OutPut Type for Real data type";
                        validationSuccessful = false;
                    }
                }
                else
                {
                    error = "Invalid Word address";
                    if (address.IsValidWordAddress())
                    {
                        validationSuccessful = address.IsValidOutputWordAddress(outputType, xm.LoadedProject.Tags.Where(R => R.Type.ToString().Contains("Output")).ToList());
                        if (validationSuccessful)
                        {
                            if (xm.LoadedProject.Tags.Where(R => R.LogicalAddress == control.Text && R.IoList == Core.Types.IOListType.Default).Count() > 0)
                            {
                                validationSuccessful = false;
                            }
                        }
                    }
                    else
                    {
                        validationSuccessful = false;
                    }
                }
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
                        validationSuccessful = address.IsValidOutputBitAddress(outputType, xm.LoadedProject.Tags.Where(R => R.LogicalAddress.Contains(".") && R.Type.ToString().Contains("Output")).ToList());
                        break;
                    case "Real":
                        if (outputType == "Memory Address Variable")
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
                        if (outputType == "Memory Address Variable")
                        {
                            error = "Invalid Word address for Byte data type";
                            validationSuccessful = address.IsValidByteAddress(dataType);
                        }
                        else
                        {
                            error = "Invalid OutPut Type for Byte data type";
                            validationSuccessful = false;
                        }
                        break;
                    default:
                        error = "Invalid Word address";
                        if (address.IsValidWordAddress())
                        {
                            validationSuccessful = address.IsValidOutputWordAddress(outputType, xm.LoadedProject.Tags.Where(R => R.Type.ToString().Contains("Output")).ToList());
                            if (validationSuccessful)
                            {
                                //if (xm.LoadedProject.Tags.Where(R => R.LogicalAddress == control.Text && R.IoList == Core.Types.IOListType.Default && !R.Tag.StartsWith("WRITE")).Count() > 0)
                                //{
                                //    validationSuccessful = false;
                                //}
                            }
                        }
                        else
                        {
                            validationSuccessful = false;
                        }
                        break;
                }

            if (validationSuccessful)
            {
                // Check if this output address has been already used... 
                foreach (ApplicationRung row in AppData)
                {
                    //string data = row.Output1.ToString();
                    //if (address.Equals(data))
                    //{
                    //    e.Cancel = true;
                    //    errorProvider.SetError(control, "Output address has been already used in Line No. " + row.LineNumber.ToString());
                    //    break;
                    //}
                }
                e.Cancel = false;
                errorProvider.SetError(control, null);
            }
            else
            {
                if (udfbname != "")
                {
                    UDFBInfo uDFB = xm.LoadedProject.UDFBInfo.Where(u => u.UDFBName == udfbname).FirstOrDefault();
                    if (uDFB.UDFBlocks.Where(b => b.Text == control.Text && b.DataType == comboBoxDataType.Text.ToString()).Count() == 0)
                    {
                        e.Cancel = true;
                        errorProvider.SetError(control, error);
                    }
                    else
                    {
                        e.Cancel = false;
                        errorProvider.SetError(control, null);
                    }
                }
                else
                {
                    e.Cancel = true;
                    errorProvider.SetError(control, error);
                }
            }
            ///Adding Logic For the Checking Tags Location
            string CurrentLogicalBlock = xm.CurrentScreen.Split('#')[1];
            string output1Address = control.Text;
            XMIOConfig EnteredTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == output1Address).FirstOrDefault();
            if (EnteredTag != null)
            {
                if (CurrentLogicalBlock.Contains("LogicBlock") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !EnteredTag.Model.Contains("XM"))))
                {
                    if (EnteredTag.Model != "User Defined Tags" && EnteredTag.Model != "")
                    {
                        e.Cancel = true;
                        error = "Used Tag is Not Found in Current Logic Block";
                        errorProvider.SetError(control, error);
                    }
                }
                else if (CurrentLogicalBlock.EndsWith("Logic") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !EnteredTag.Model.Contains("XM"))))
                {
                    string UdfbName = CurrentLogicalBlock.Split(' ')[0];
                    if (EnteredTag.Model.ToString() != UdfbName + " Tags")
                    {
                        e.Cancel = true;
                        error = "Used Tag is Not Found in Current UDFB Block";
                        errorProvider.SetError(control, error);
                    }
                }
            }
            var addedDataType = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == control.Text).FirstOrDefault();
            if (addedDataType != null)
            {
                bool fbDataType = dataType == "TON" || dataType == "TOFF" || dataType == "RTON" || dataType == "TP" || dataType == "CTU" || dataType == "CTD";

                if (!fbDataType && selectedInstruction.Text != "Pack" && selectedInstruction.Text != "UnPack")
                {
                    bool isOnBoard = addedDataType.IoList == Core.Types.IOListType.OnBoardIO;
                    //if (/*addedDataType.Model != "" ||*/ ((addedDataType.Label == dataType) || (isOnBoard && dataType=="Bool" && addedDataType.Type==Core.Types.IOType.DigitalOutput)) &&(comboBoxInstructionType.Text != "Data Conversion"))
                    //{
                    //    error = string.Empty;
                    //    e.Cancel = false;
                    //    //return true;
                    //}
                    if (validationSuccessful && (comboBoxInstructionType.Text != "Data Conversion"))
                    {
                        error = string.Empty;
                        e.Cancel = false;
                        //return true;
                    }
                    else
                    {
                        if (selectedInstruction.OutputDataTypes.Count == 1 || comboBoxInstructionType.Text == "Data Conversion")
                        {
                            string InstructionName = comboBoxInstruction.Text;
                            string dataconversionOutputDataType = selectedInstruction.OutputDataTypes.FirstOrDefault().ToString();
                            if ((addedDataType.IoList != Core.Types.IOListType.NIL && validationSuccessful) || (addedDataType.Label == dataconversionOutputDataType))
                            {
                                error = string.Empty;
                                e.Cancel = false;
                                //return true;
                            }
                            else
                            {
                                e.Cancel = true;
                                error = "Data type not matching need to select " + dataType + " and this logical address has datatype " + addedDataType.Label;
                                errorProvider.SetError(control, error);
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            //error = "Data type not matching need to select " + dataType + " and this logical address has datatype " + addedDataType.Label;
                            error = "Invalid Bit Address";
                            errorProvider.SetError(control, error);
                        }
                        //return false;
                    }
                }
            }
        }
        /// <summary>
        /// Validate the output Address depending on the control and the datatype and Out put tyep selected 
        /// </summary>
        /// <param name="control">Send the nmae of control.</param>        
        /// <param name="e">Cancel event handler.</param>        
        private void ValidateOutputAddressWithoutOutputType(Control control, CancelEventArgs e)
        {
            bool validationSuccessful;
            string error;
            string dataType = comboBoxDataType.SelectedItem != null ? ((DataType)comboBoxDataType.SelectedItem).Text : "";
            string address = control.Text;
            Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);
            string Instruction = comboBoxInstructionType.SelectedItem.ToString();
            if (string.IsNullOrEmpty(address))
            {
                validationSuccessful = false;
                error = "Output address cannot be blank";
            }
            else if (selectedInstruction.OutputDataTypes.Count == 1)
            {
                if (selectedInstruction.OutputDataTypes.Where(R => R.ToString().Equals("Bool")).Count() > 0)
                {
                    error = "Invalid Bit Address";
                    validationSuccessful = address.IsValidBitAddress();
                }
                else if (selectedInstruction.OutputDataTypes.Where(R => R.ToString().Equals("Real")).Count() > 0)
                {
                    error = "Invalid Word address for Real data type";
                    validationSuccessful = address.IsValidRealWordAddress();
                }
                else
                {
                    error = "Invalid Word address";
                    if (address.IsValidWordAddress())
                    {
                        validationSuccessful = address.IsValidWordAddress();
                        if (validationSuccessful)
                        {
                            if (xm.LoadedProject.Tags.Where(R => R.LogicalAddress == control.Text && R.IoList == Core.Types.IOListType.Default).Count() > 0)
                            {
                                validationSuccessful = false;
                            }
                        }
                    }
                    else
                    {
                        validationSuccessful = false;
                    }
                }
            }
            else
                switch (dataType)
                {
                    case "Bool":
                    // Timer data types
                    case "TON":
                    case "TOFF":
                    case "TP":
                    // Counter data types
                    case "CTU":
                    case "CTD":
                        error = "Invalid Bit Address";
                        validationSuccessful = address.IsValidBitAddress();
                        break;
                    case "Real":
                        error = "Invalid Word address for Real data type";
                        validationSuccessful = address.IsValidRealWordAddress();
                        break;
                    default:
                        error = "Invalid Word address";
                        if (address.IsValidWordAddress())
                        {
                            validationSuccessful = address.IsValidWordAddress();
                            if (validationSuccessful)
                            {
                                if (xm.LoadedProject.Tags.Where(R => R.LogicalAddress == control.Text && R.IoList == Core.Types.IOListType.Default).Count() > 0)
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

            if (validationSuccessful)
            {
                // Check if this output address has been already used... 
                foreach (ApplicationRung row in AppData)
                {
                    //string data = row.Output1.ToString();
                    //if (address.Equals(data))
                    //{
                    //    e.Cancel = true;
                    //    errorProvider.SetError(control, "Output address has been already used in Line No. " + row.LineNumber.ToString());
                    //    break;
                    //}
                }
                e.Cancel = false;
                errorProvider.SetError(control, null);
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(control, error);
            }
        }

        private void textBoxOutputAddress_Validating(object sender, CancelEventArgs e)
        {
            string instructionType = ((InstructionType)comboBoxInstructionType.SelectedItem).Text;

            if (instructionType == "Scale" && textBoxOutputAddress.Text == "")
            {
                return;
            }
            ValidateOutputAddress(textBoxOutputAddress, e);
        }

        private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnable.Checked)
            {
                textBoxEnable.Enabled = true;
                OperationEnable.SelectedIndex = 0;
                TagEnable.Enabled = true;
                OperationEnable.Enabled = true;
            }
            else
            {
                textBoxEnable.Clear();
                textBoxEnable.Enabled = false;
                TagEnable.Enabled = false;
                OperationEnable.Enabled = false;

            }
            //CheckandFillTags();
        }

        /// <summary>
        /// Clear all the input boxes and reset drop downs
        /// </summary>
        private void ClearInputControls()
        {
            checkBoxEnable.Checked = false;
            textBoxOp1Address.Clear();
            errorProvider.SetError(textBoxOp1Address, "");
            textBoxOp2Address.Clear();
            errorProvider.SetError(textBoxOp2Address, "");
            textBoxOp3Address.Clear();
            errorProvider.SetError(textBoxOp3Address, "");
            textBoxOp4Address.Clear();
            errorProvider.SetError(textBoxOp4Address, "");
            textBoxOp5Address.Clear();
            errorProvider.SetError(textBoxOp4Address, "");
            textBoxOutputAddress.Clear();
            errorProvider.SetError(textBoxOutputAddress, "");
            Output2.Clear();
            errorProvider.SetError(Output2, "");
            Output3.Clear();
            errorProvider.SetError(Output3, "");
            textBoxComment.Clear();
            errorProvider.SetError(textBoxComment, "");
            textBoxEnable.Clear();
            errorProvider.SetError(textBoxEnable, "");
            textBoxComment.Clear();
            OperationType1.SelectedIndex = 0;
            OperationType2.SelectedIndex = 0;
            OperationType3.SelectedIndex = 0;
            OperationType4.SelectedIndex = 0;
            OperationType5.SelectedIndex = 0;
            //Reset Output type to default 
            comboBoxOutputType.SelectedIndex = 0;
        }

        private void comboBoxInstruction_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Retrieve the selected instruction 
            Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);

            // Populate supported data types for this instruction
            comboBoxDataType.DataSource = selectedInstruction.SupportedDataTypes;

            // Set the labels dynamically according to selected instruction
            labelOutput.Text = selectedInstruction.Output1Label;
            labelOutput2.Text = selectedInstruction.Output2Label;
            labelOutput3.Text = selectedInstruction.Output3Label;
            labelOP1.Text = selectedInstruction.Operand1Label;
            labelOP2.Text = selectedInstruction.Operand2Label;
            labelOP3.Text = selectedInstruction.Operand3Label;
            labelOP4.Text = selectedInstruction.Operand4Label;
            labelOP5.Text = selectedInstruction.Operand5LAbel;

            // Enable / Disable the input operands according to selected instruction
            textBoxOp1Address.Enabled = selectedInstruction.Operand1Enabled;
            textBoxOp2Address.Enabled = selectedInstruction.Operand2Enabled;
            textBoxOp3Address.Enabled = selectedInstruction.Operand3Enabled;
            textBoxOp4Address.Enabled = selectedInstruction.Operand4Enabled;
            textBoxOp5Address.Enabled = selectedInstruction.Operand5Enabled;
            lblTopic.Enabled = selectedInstruction.TopicEnabled;


            OperationType1.Enabled = selectedInstruction.Operand1Enabled;
            OperationType2.Enabled = selectedInstruction.Operand2Enabled;
            OperationType3.Enabled = selectedInstruction.Operand3Enabled;
            OperationType4.Enabled = selectedInstruction.Operand4Enabled;
            OperationType5.Enabled = selectedInstruction.Operand5Enabled;

            TagOperand1.Enabled = selectedInstruction.Operand1Enabled;
            TagOperand2.Enabled = selectedInstruction.Operand2Enabled;
            TagOperand3.Enabled = selectedInstruction.Operand3Enabled;
            TagOperand4.Enabled = selectedInstruction.Operand4Enabled;
            TagOperand5.Enabled = selectedInstruction.Operand5Enabled;

            // Enable / Disable the output controls according to selected instruction
            textBoxOutputAddress.Enabled = selectedInstruction.Output1Enabled;
            Output2.Enabled = selectedInstruction.Output2Enabled;
            Output3.Enabled = selectedInstruction.Output3Enabled;
            checkBoxEnable.Checked = isEnabled;
            if (selectedInstruction.ToString().StartsWith("MQTT"))
            {
                cmbTopic.Visible = true;
                lblTopic.Visible = true;
            }
            else
            {
                cmbTopic.Visible = false;
                lblTopic.Visible = false;
            }
            comboBoxDataType_SelectedIndexChanged(sender, e);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //AppData = applicationRecs.ApplicationRungs;
            /* string instructionType = ((InstructionType)comboBoxInstructionType.SelectedItem).Text;
             if (instructionType == "RTON" && Output2.Text.StartsWith("W") && Output2.Text.Contains(":"))
             {
                 isRetentive = true;

             }
             chkIsRetentive.Checked = isRetentive? true: false;*/
            //Set Retentive for the RTON instruction type
        }

        private void textBoxOp1Address_Validating(object sender, CancelEventArgs e)
        {
            ValidateOperand(textBoxOp1Address, e);
        }

        private void textBoxOp2Address_Validating(object sender, CancelEventArgs e)
        {
            // Don't allow divistion by zero if it's a Arithmetic - DIV instruction
            if (((InstructionType)comboBoxInstructionType.SelectedItem).Text.Equals("Arithmetic") &&
                ((Instruction)comboBoxInstruction.SelectedItem).Text.Equals("DIV") &&
                textBoxOp2Address.Text.Equals("0"))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxOp2Address, "Divisor cannot be Zero");
            }
            else
                ValidateOperand(textBoxOp2Address, e);
            if (textBoxOp1Address.TextLength == 0 && textBoxOp2Address.TextLength > 0)
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxOp1Address, "Input 1 Empty");
            }
        }

        private void textBoxOp3Address_Validating(object sender, CancelEventArgs e)
        {
            ValidateOperand(textBoxOp3Address, e);
            if (textBoxOp2Address.TextLength == 0 && textBoxOp3Address.TextLength > 0)
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxOp2Address, "Input 2 Empty");
            }
        }

        private void textBoxOp4Address_Validating(object sender, CancelEventArgs e)
        {
            ValidateOperand(textBoxOp4Address, e);
            if (textBoxOp3Address.TextLength == 0 && textBoxOp4Address.TextLength > 0)
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxOp3Address, "Input 3 Empty");
            }
        }

        private void textBoxEnable_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxEnable.Enabled)     // Validate only Enable text box is editable.
            {
                //string dataType = ((DataType)comboBoxDataType.SelectedItem).Text;
                //string instructionType = ((InstructionType)comboBoxInstructionType.SelectedItem).Text;
                //string instruction = ((Instruction)comboBoxInstruction.SelectedItem).Text;

                //if(instructionType == "Arithmetic" && instruction == "EXP" && comboBoxOutputType.Text =="On-board")
                //{

                //}

                e.Cancel = false;
                //if (OperationEnable.SelectedIndex == 1)
                //{
                //    if (textBoxEnable.Text.Equals("0") || textBoxEnable.Text.Equals("1"))
                //    {
                //        e.Cancel = false;
                //        errorProvider.SetError(textBoxEnable, null);
                //    }
                //    else
                //    {
                //        e.Cancel = true;
                //        errorProvider.SetError(textBoxEnable, "Inavalid Boolean Value for Enable field");
                //    }
                //}
                //else if (OperationEnable.SelectedIndex == 2 || OperationEnable.SelectedIndex == 0)
                //{
                //    if (textBoxEnable.Text.IsValidBitAddress())
                //    {
                //        e.Cancel = false;
                //        errorProvider.SetError(textBoxEnable, null);
                //    }
                //    else
                //    {
                //        e.Cancel = true;
                //        errorProvider.SetError(textBoxEnable, "Inavalid Bit Address for Enable field");
                //    }
                //}
            }
        }

        private void Output2_Validating(object sender, CancelEventArgs e)
        {
            string dataType = ((DataType)comboBoxDataType.SelectedItem).Text;
            string instructionType = ((InstructionType)comboBoxInstructionType.SelectedItem).Text;
            if (Output2.Enabled && (dataType == "TON" || dataType == "TOFF" || dataType == "TP" || dataType == "CTU" || dataType == "CTD"))     // Validate only if Outpu2 is enabled
            {
                string regExString;
                //string instructionType = ((InstructionType)comboBoxInstructionType.SelectedItem).Text;
                string OutputType = comboBoxOutputType.SelectedItem.ToString();

                // Let's setup regular expression for validating Word address. Acceptable format e.g. T6:000 to T6:255 or C7:000 to C7:255
                // set starting string to 'T6:' for Timer instruction and to 'C7:' for Counter instruction
                if (Output2.Text.StartsWith("W4"))
                {
                    regExString = "^(W4)";
                    //regExString += ":([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$" /* :000 to :255 */;
                    regExString += ":((0[0-9][0-9]|[1-9][0-9][0-9]|100[0-9]|101[0-9]|102[0-3]))$";   ///* :000 to :1023 */;


                }
                else
                {
                    regExString = "^(P5|T6|C7)";
                    regExString += ":([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$" /* :000 to :255 */;
                }
                Regex regEx2 = new Regex(regExString);
                if (Output2.Text == "")
                {
                    e.Cancel = false;
                    errorProvider.SetError(Output2, null);
                }
                else if (!regEx2.IsMatch(Output2.Text))
                {
                    e.Cancel = true;
                    errorProvider.SetError(Output2, "Invalid word address");
                    return;
                }
                else
                {
                    // Validation successful...
                    e.Cancel = false;
                    errorProvider.SetError(Output2, null);
                }
            }
            else if (dataType == "RTON")
            {
                string regExString;
                //string instructionType = ((InstructionType)comboBoxInstructionType.SelectedItem).Text;
                string OutputType = comboBoxOutputType.SelectedItem.ToString();

                // Let's setup regular expression for validating Word address. Acceptable format e.g. T6:000 to T6:255 or C7:000 to C7:255
                // set starting string to 'T6:' for Timer instruction and to 'C7:' for Counter instruction               
                regExString = "^(W4|P5|T6|C7)";
                regExString += ":([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$" /* :000 to :255*/ ;

                Regex regEx2 = new Regex(regExString);
                if (Output2.Text != "")
                {
                    e.Cancel = false;
                    errorProvider.SetError(Output2, null);
                }
                else if (!regEx2.IsMatch(Output2.Text))
                {
                    e.Cancel = true;
                    errorProvider.SetError(Output2, "Invalid word address");
                    return;
                }
                else
                {
                    // Validation successful...
                    e.Cancel = false;
                    errorProvider.SetError(Output2, null);
                }
            }
            else if (Output2.Enabled)
            {
                if (instructionType == "Scale" && Output2.Text.StartsWith("P5"))
                {
                    return;
                }
                ValidateOutputAddressWithoutOutputType(Output2, e);
            }
            ///<>
            ///Adding Logic For the Checking Tags Location
            string CurrentLogicalBlock = xm.CurrentScreen.Split('#')[1];
            string output2Address = Output2.Text;
            XMIOConfig EnteredTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == output2Address).FirstOrDefault();
            if (EnteredTag != null)
            {
                if (CurrentLogicalBlock.Contains("LogicBlock") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !EnteredTag.Model.Contains("XM"))))
                {
                    if (EnteredTag.Model != "User Defined Tags" && EnteredTag.Model != "")
                    {
                        e.Cancel = true;
                        errorProvider.SetError(Output2, "Used Tag is Not Found in Current Logic Block");
                    }
                }
                else if (CurrentLogicalBlock.EndsWith("Logic") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !EnteredTag.Model.Contains("XM"))))
                {
                    string UdfbName = CurrentLogicalBlock.Split(' ')[0];
                    if (EnteredTag.Model.ToString() != UdfbName + " Tags")
                    {
                        e.Cancel = true;
                        errorProvider.SetError(Output2, "Used Tag is Not Found in Current UDFB Block");
                    }
                }
            }
        }
        /// <summary>
        /// Check whether minimum required input are supplied or not for the given operations
        /// </summary>
        /// <returns> Return True if valid no of inputs are selected or return False </returns>

        private bool AreMinimumInputsRequiredSupplied()
        {
            string Instruction = comboBoxInstructionType.SelectedItem.ToString();
            string Operation = comboBoxInstruction.SelectedItem.ToString();
            int count = 0;

            if (textBoxOp1Address.TextLength > 0)
                ++count;

            if (textBoxOp2Address.TextLength > 0)
                ++count;

            if (textBoxOp3Address.TextLength > 0)
                ++count;

            if (textBoxOp4Address.TextLength > 0)
                ++count;

            if (textBoxOp5Address.TextLength > 0)
                ++count;

            if (Instruction == "Arithmetic" && Operation == "EXP")
            {
                return true;
            }
            if (((Instruction == "Logical" && Operation != "NOT") || (Instruction == "Arithmetic" && Operation != "MOV") || (Instruction == "Compare") || (Instruction == "Bit Shift") || (Instruction == "Flipflop")) && (count < 2))
            {
                return false;
            }
            else if ((Instruction == "Edge Detector" || Instruction == "Swap" || Instruction == "Data Conversion" || (Instruction == "Arithmetic" && Operation == "MOV") || (Instruction == "Logical" && Operation == "NOT")) && (count < 1))
            {
                return false;
            }
            else if (((Instruction == "Counter") || (Instruction == "Limit") || (Instruction == "Timer RTON") || (Instruction == "Limit Alarm")) && (count < 3))
            {
                return false;
            }
            else if (((Instruction == "Timer TON") || (Instruction == "Timer TOFF") || (Instruction == "Timer TP")) && (count < 2))
            {
                return false;
            }
            else if ((Instruction == "Scale") && (count < 5))
            {
                return false;
            }
            else if ((Instruction == "Pack" || Instruction == "UnPack") && (count == 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void comboBoxOutputType_Validating(object sender, CancelEventArgs e)
        {
            textBoxOutputAddress_Validating(sender, e);

            Output2_Validating(sender, e);
        }

        /// <summary>
        /// Padd control with preceding 0's for validation purpose
        /// </summary>
        /// <param name="str">Send the string which is about to be padded</param>
        /// <param name="len">set the length of padding</param>
        /// <returns> Return the padded string</returns>

        public string padding(string str, int len)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str == "")
                {
                    if (len == 2)
                    {
                        str = "00";
                    }
                    else
                        str = "000";

                }
                else
                {
                    if (len == 2)
                    {
                        str = int.Parse(str).ToString("00");
                    }
                    else
                        str = int.Parse(str).ToString("000");
                }
            }
            return str;
        }
        private void btnValidator_Click(object sender, EventArgs e)
        {
            try
            {
                bool Realpaas = false, BitPass = false, WordPass = false;
                string[] InputBitAddress = { "Q0", "I1", "F2" }, InputWordAddress = { "Q0", "I1", "W4" }, OutputBitAddress = { "Q0", "F2" }, OutputWordAddress = { "Q0", "W4" }, Output2WordAddress = { "W4", "P5", "T6", "C7" };
                string Iaddress, OAddress;
                Random number = new Random();
                for (int dtype = 0; dtype < comboBoxInstructionType.Items.Count; dtype++)
                {
                    comboBoxInstructionType.SelectedIndex = dtype;

                    for (int itype = 0; itype < comboBoxInstruction.Items.Count; itype++)
                    {
                        comboBoxInstruction.SelectedIndex = itype;

                        for (int dttype = 0; dttype < comboBoxDataType.Items.Count; dttype++)
                        {
                            comboBoxInstructionType.SelectedIndex = dtype;
                            comboBoxInstruction.SelectedIndex = itype;
                            comboBoxDataType.SelectedIndex = dttype;
                            if (comboBoxDataType.SelectedItem.ToString() == "Real")
                            {
                                for (int addcnt = 0; addcnt < 256; addcnt++)
                                {
                                    comboBoxInstructionType.SelectedIndex = dtype;
                                    comboBoxInstruction.SelectedIndex = itype;
                                    comboBoxDataType.SelectedIndex = dttype;
                                    if (textBoxOp1Address.Enabled == true) textBoxOp1Address.Text = "P5:" + padding((number.Next(0, 255).ToString()), 3);
                                    if (textBoxOp2Address.Enabled == true) textBoxOp2Address.Text = "P5:" + padding((number.Next(0, 255).ToString()), 3);
                                    if (textBoxOp3Address.Enabled == true) textBoxOp3Address.Text = "P5:" + padding((number.Next(0, 255).ToString()), 3);
                                    if (textBoxOp4Address.Enabled == true) textBoxOp4Address.Text = "P5:" + padding((number.Next(0, 255).ToString()), 3);

                                    if (comboBoxInstructionType.SelectedItem.ToString() != "Compare")
                                    {
                                        comboBoxOutputType.Text = "Memory Address Variable";
                                        textBoxOutputAddress.Text = "P5:" + padding(addcnt.ToString(), 3);
                                        buttonAdd_Click(sender, e);
                                    }
                                    else
                                    {
                                        if (addcnt == 0)
                                        {
                                            for (int addpt = 0; addpt < 16; addpt++)
                                            {
                                                comboBoxInstructionType.SelectedIndex = dtype;
                                                comboBoxInstruction.SelectedIndex = itype;
                                                comboBoxDataType.SelectedIndex = dttype;
                                                if (textBoxOp1Address.Enabled == true) textBoxOp1Address.Text = "P5:" + padding((number.Next(0, 255).ToString()), 3);
                                                if (textBoxOp2Address.Enabled == true) textBoxOp2Address.Text = "P5:" + padding((number.Next(0, 255).ToString()), 3);
                                                if (textBoxOp3Address.Enabled == true) textBoxOp3Address.Text = "P5:" + padding((number.Next(0, 255).ToString()), 3);
                                                if (textBoxOp4Address.Enabled == true) textBoxOp4Address.Text = "P5:" + padding((number.Next(0, 255).ToString()), 3);
                                                comboBoxOutputType.Text = "On-board";
                                                textBoxOutputAddress.Text = "Q0:" + padding(addcnt.ToString(), 3) + "." + padding(addpt.ToString(), 2);
                                                buttonAdd_Click(sender, e);
                                                if (addpt == 5) break;
                                            }
                                        }
                                    }
                                    if (Realpaas == true) addcnt = addcnt + 50;
                                }
                                Realpaas = true;
                            }
                            else if ((comboBoxInstructionType.SelectedItem.ToString() == "Compare") || (comboBoxDataType.SelectedItem.ToString() == "Bool" || comboBoxDataType.SelectedItem.ToString() == "CTU" || comboBoxDataType.SelectedItem.ToString() == "CTD" || comboBoxDataType.SelectedItem.ToString() == "TON" || comboBoxDataType.SelectedItem.ToString() == "TOFF" || comboBoxDataType.SelectedItem.ToString() == "TP"))
                            {
                                for (int addcnt = 0; addcnt < 256; addcnt++)
                                {
                                shiftloop:

                                    comboBoxInstructionType.SelectedIndex = dtype;
                                    comboBoxInstruction.SelectedIndex = itype;
                                    comboBoxDataType.SelectedIndex = dttype;
                                    for (int addpt = 0; addpt < 16; addpt++)
                                    {
                                        comboBoxInstructionType.SelectedIndex = dtype;
                                        comboBoxInstruction.SelectedIndex = itype;
                                        comboBoxDataType.SelectedIndex = dttype;
                                        string str1 = addpt.ToString();
                                        str1 = padding(str1, 2);
                                        if (comboBoxInstructionType.SelectedItem.ToString() == "Compare" && comboBoxDataType.SelectedItem.ToString() != "Bool")
                                        {
                                            if (textBoxOp1Address.Enabled == true) textBoxOp1Address.Text = InputWordAddress[number.Next(0, 2)] + ":" + padding((number.Next(0, 255).ToString()), 3);
                                            if (textBoxOp2Address.Enabled == true) textBoxOp2Address.Text = InputWordAddress[number.Next(0, 2)] + ":" + padding((number.Next(0, 255).ToString()), 3);
                                            if (textBoxOp3Address.Enabled == true) textBoxOp3Address.Text = InputWordAddress[number.Next(0, 2)] + ":" + padding((number.Next(0, 255).ToString()), 3);
                                            if (textBoxOp4Address.Enabled == true) textBoxOp4Address.Text = InputWordAddress[number.Next(0, 2)] + ":" + padding((number.Next(0, 255).ToString()), 3);
                                        }
                                        else
                                        {
                                            Iaddress = InputBitAddress[number.Next(0, 2)];
                                            if (Iaddress.Equals("F2"))
                                            {
                                                if (textBoxOp1Address.Enabled == true) textBoxOp1Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3);
                                                if (textBoxOp2Address.Enabled == true) textBoxOp2Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3);
                                                if (textBoxOp3Address.Enabled == true) textBoxOp3Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3);
                                                if (textBoxOp4Address.Enabled == true) textBoxOp4Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3);
                                            }
                                            else
                                            {
                                                if (textBoxOp1Address.Enabled == true) textBoxOp1Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3) + "." + padding((number.Next(0, 15).ToString()), 2);
                                                if ((comboBoxInstructionType.SelectedItem.ToString()).StartsWith("Timer"))
                                                {
                                                    if (textBoxOp2Address.Enabled == true) textBoxOp2Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3);
                                                }
                                                else
                                                {
                                                    if (textBoxOp2Address.Enabled == true) textBoxOp2Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3) + "." + padding((number.Next(0, 15).ToString()), 2);
                                                }
                                                if (comboBoxInstructionType.SelectedItem.ToString() == "Counter")
                                                {
                                                    if (textBoxOp3Address.Enabled == true) textBoxOp3Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3);
                                                }
                                                else
                                                {
                                                    if (textBoxOp3Address.Enabled == true) textBoxOp3Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3) + "." + padding((number.Next(0, 15).ToString()), 2);
                                                }

                                                if (textBoxOp4Address.Enabled == true) textBoxOp4Address.Text = Iaddress + ":" + padding((number.Next(0, 255).ToString()), 3) + "." + padding((number.Next(0, 15).ToString()), 2);
                                            }

                                        }

                                        if (Output2.Enabled == true) Output2.Text = "C7:" + padding((number.Next(0, 255).ToString()), 3);
                                        if (addcnt == 0 && addpt < 6) comboBoxOutputType.Text = "On-board";
                                        if (addcnt == 0 && addpt > 5)
                                        {
                                            comboBoxOutputType.Text = "Remote";
                                            addcnt = addcnt + 2;
                                            goto shiftloop;
                                        }

                                        OAddress = OutputBitAddress[number.Next(0, 1)];
                                        if (addcnt > 1) comboBoxOutputType.Text = "Remote";
                                        if (OAddress.Equals("F2"))
                                        {
                                            comboBoxOutputType.Text = "Memory Address Variable";
                                            textBoxOutputAddress.Text = OAddress + ":" + padding(addcnt.ToString(), 3);
                                        }
                                        else
                                        {
                                            textBoxOutputAddress.Text = OAddress + ":" + padding(addcnt.ToString(), 3) + "." + padding(addpt.ToString(), 2);
                                        }

                                        buttonAdd_Click(sender, e);
                                    }

                                    if (addcnt > 3 && BitPass == true) addcnt = addcnt + 50;
                                }

                                if (BitPass == false) BitPass = true;
                            }

                            else
                            {
                                for (int addcnt = 1; addcnt < 256; addcnt++)
                                {
                                    string str2 = addcnt.ToString();

                                    for (int i = 0; i < str2.Length; i++)
                                    {
                                        if (str2 == "")
                                        {
                                            str2 = "000";
                                        }
                                        else
                                        {
                                            str2 = int.Parse(str2).ToString("000");
                                        }

                                    }
                                    comboBoxInstructionType.SelectedIndex = dtype;
                                    comboBoxInstruction.SelectedIndex = itype;
                                    comboBoxDataType.SelectedIndex = dttype;
                                    if (textBoxOp1Address.Enabled == true) textBoxOp1Address.Text = InputWordAddress[number.Next(0, 2)] + ":" + padding((number.Next(0, 255).ToString()), 3);
                                    if (textBoxOp2Address.Enabled == true) textBoxOp2Address.Text = InputWordAddress[number.Next(0, 2)] + ":" + padding((number.Next(0, 255).ToString()), 3);
                                    if (textBoxOp3Address.Enabled == true) textBoxOp3Address.Text = InputWordAddress[number.Next(0, 2)] + ":" + padding((number.Next(0, 255).ToString()), 3);
                                    if (textBoxOp4Address.Enabled == true) textBoxOp4Address.Text = InputWordAddress[number.Next(0, 2)] + ":" + padding((number.Next(0, 255).ToString()), 3);
                                    if (addcnt == 1) comboBoxOutputType.Text = "On-board";
                                    if (addcnt > 1) comboBoxOutputType.Text = "Remote";
                                    OAddress = OutputWordAddress[number.Next(0, 1)];
                                    if (OAddress.Equals("W4")) comboBoxOutputType.Text = "Memory Address Variable";
                                    textBoxOutputAddress.Text = OAddress + ":" + str2;
                                    buttonAdd_Click(sender, e);
                                    if (WordPass == true) addcnt = addcnt + 50;
                                }
                                if (WordPass == false) WordPass = true;
                            }
                            comboBoxInstructionType.SelectedIndex = dtype;
                            comboBoxInstruction.SelectedIndex = itype;
                        }
                    }
                    comboBoxInstructionType.SelectedIndex = dtype;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "XM-Pro PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void TagOperand1_SelectedIndexChanged(object sender, EventArgs e)
        {

            textBoxOp1Address.Text = XMProValidator.GetTheAddressFromTag(TagOperand1.Text.ToString());
            //For the Showing All Used Tag Address for the Pack and Unpacked Instruction
            if (comboBoxInstructionType.Text.ToString().Equals("Pack"))
            {
                AllPackAddress();
            }
            else if (comboBoxInstructionType.Text.ToString().Equals("UnPack"))
            {
                AllUnPackAddress();
            }
        }



        private void TagOperand2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxOp2Address.Text = XMProValidator.GetTheAddressFromTag(TagOperand2.Text.ToString());
        }

        private void TagOperand3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxOp3Address.Text = XMProValidator.GetTheAddressFromTag(TagOperand3.Text.ToString());
        }

        private void TagOperand4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxOp4Address.Text = XMProValidator.GetTheAddressFromTag(TagOperand4.Text.ToString());
        }

        private void TagEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxEnable.Text = XMProValidator.GetTheAddressFromTag(TagEnable.Text.ToString());
        }

        private void TagOutput1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxOutputAddress.Text = XMProValidator.GetTheAddressFromTag(TagOutput1.Text.ToString());
            //For the Showing All Used Tag Address for the Pack and Unpacked Instruction
            if (comboBoxInstructionType.Text.ToString().Equals("Pack"))
            {
                AllPackAddress();
            }
            else if (comboBoxInstructionType.Text.ToString().Equals("UnPack"))
            {
                AllUnPackAddress();
            }

        }

        private void TagOutput2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Output2.Text = XMProValidator.GetTheAddressFromTag(TagOutput2.Text.ToString());
        }

        private void OperationEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OperationEnable.SelectedIndex == 2)
                labelNegationEnable.Visible = true;
            else
                labelNegationEnable.Visible = false;
            // Use following sequence of switching focus to cause re-validation of operand
            if (OperationEnable.SelectedIndex == 1)
            {
                TagEnable.Enabled = false;
                textBoxEnable.Focus();
            }
            else
            {
                TagEnable.Enabled = true;
                TagEnable.Focus();
            }
        }

        private void comboBoxDataType_Enter(object sender, EventArgs e)
        {
            datatype_prv_sel_index = comboBoxDataType.SelectedIndex;
        }

        private void TagOperand5_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxOp5Address.Text = XMProValidator.GetTheAddressFromTag(TagOperand5.Text.ToString());

        }

        private void TagOutput3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Output3.Text = XMProValidator.GetTheAddressFromTag(TagOutput3.Text.ToString());
        }

        private void Output3_Validating(object sender, CancelEventArgs e)
        {
            string dataType = ((DataType)comboBoxDataType.SelectedItem).Text;
            string instructionType = ((InstructionType)comboBoxInstructionType.SelectedItem).Text;
            if (Output3.Enabled)
            {
                if ((instructionType == "Scale" && Output3.Text.StartsWith("Q0:")) || (instructionType == "Scale" && Output3.Text == ""))
                {
                    return;
                }
                //else if (instructionType == "Scale" && Output3.Text == "")
                //{
                //    return;
                //}
                else
                {
                    bool validationSuccessful = Output3.Text.IsValidBitAddress();
                    if (!validationSuccessful)
                        errorProvider.SetError(Output3, "Invalid Bit Address");
                    else
                        errorProvider.SetError(Output3, "");
                }
                ///Adding Logic For the Checking Tags Location
                string CurrentLogicalBlock = xm.CurrentScreen.Split('#')[1];
                string output3Address = Output3.Text;
                XMIOConfig EnteredTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == output3Address).FirstOrDefault();
                if (EnteredTag != null)
                {
                    if (CurrentLogicalBlock.Contains("LogicBlock") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !EnteredTag.Model.Contains("XM"))))
                    {
                        if (EnteredTag.Model != "User Defined Tags" && EnteredTag.Model != "")
                        {
                            errorProvider.SetError(Output3, "Used Tag is Not Found in Current Logic Block");
                        }
                    }
                    else if (CurrentLogicalBlock.EndsWith("Logic") && (EnteredTag != null && (EnteredTag.Model != null && EnteredTag.Model != "" && !EnteredTag.Model.Contains("XM"))))
                    {
                        string UdfbName = CurrentLogicalBlock.Split(' ')[0];
                        if (EnteredTag.Model.ToString() != UdfbName + " Tags")
                        {
                            errorProvider.SetError(Output3, "Used Tag is Not Found in Current UDFB Block");
                        }
                    }
                }
            }
        }

        private void textBoxOp5Address_Validating(object sender, CancelEventArgs e)
        {
            ValidateOperand(textBoxOp5Address, e);
            if (textBoxOp5Address.TextLength == 0 && textBoxOp5Address.TextLength > 0)
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxOp5Address, "Input 5 Empty");
            }
        }

        private void comboBoxOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string instruction = ((Instruction)comboBoxInstruction.SelectedItem).Text;
            if (comboBoxInstruction.SelectedItem == null)
                return;
            //Changes for the showing output Tag for the DataConversion
            FillOutputTags();
        }

        private void FillOutputTags()
        {
            string stringName = xm.CurrentScreen.ToString();
            string[] parts = stringName.Split('#');
            string formName = parts[0];
            string logicBlockName = parts[1];
            string outputType = comboBoxOutputType.SelectedItem.ToString();
            string dataType = comboBoxDataType.SelectedItem.ToString();
            string actualDataType = comboBoxDataType.SelectedItem.ToString();
            // string instruction = ((Instruction)comboBoxInstruction.SelectedItem).Text;
            List<string> tagList = new List<string> { };
            tagList.Add("-Select Tag Name-");
            string instructionType = comboBoxInstructionType.SelectedItem.ToString();
            if (comboBoxInstructionType.SelectedItem.ToString() == "Limit Alarm" || comboBoxInstructionType.SelectedItem.ToString() == "Compare" || comboBoxInstructionType.SelectedItem.ToString() == "Counter" || comboBoxInstructionType.SelectedItem.ToString().StartsWith("Timer") || comboBoxInstructionType.SelectedItem.ToString().Equals("Scale") || comboBoxInstruction.SelectedItem.ToString().EndsWith("BOOL"))
                dataType = "Bool";
            if (stringName.Contains("UDFLadderForm"))
            {
                List<string> outputTagList = FillOutputTagOperands(outputType, dataType, logicBlockName.Split(' ')[0]);
                TagOutput1.DataSource = outputTagList;
                return;
            }
            else if (comboBoxInstructionType.Text.ToString().Equals("MQTT"))
            {
                //List<string> tagList = new List<string> { };
                //tagList.Add("-Select Tag Name-");
                if (comboBoxOutputType.Text == "Memory Address Variable")
                {
                    tagList.AddRange(xm.LoadedProject.Tags.Where(T => (T.LogicalAddress.StartsWith("F"))).Select(T => T.Tag).ToList());
                    TagOutput1.DataSource = tagList;
                }
                else if (comboBoxOutputType.Text == "Expansion")
                {
                    tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO).Select(L => L.Tag).ToList());
                    TagOutput1.DataSource = tagList;
                }
                else if (comboBoxOutputType.Text == "Remote")
                {
                    tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.RemoteIO).Select(L => L.Tag).ToList());
                    TagOutput1.DataSource = tagList;
                }
                else
                {
                    tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.OnBoardIO).Select(L => L.Tag).ToList());
                    TagOutput1.DataSource = tagList;
                }
                return;
            }
            else if (!comboBoxInstructionType.Text.ToString().Equals("Pack") && !comboBoxInstructionType.Text.ToString().Equals("UnPack"))
            {
                if (comboBoxInstruction.SelectedItem == null)
                    return;
                if (comboBoxInstructionType.SelectedItem.ToString() == "Limit Alarm" || comboBoxInstructionType.SelectedItem.ToString() == "Compare" || comboBoxInstructionType.SelectedItem.ToString() == "Counter" || comboBoxInstructionType.SelectedItem.ToString().StartsWith("Timer") || comboBoxInstructionType.SelectedItem.ToString().Equals("Scale") || comboBoxInstruction.SelectedItem.ToString().EndsWith("BOOL") || comboBoxInstructionType.SelectedItem.ToString() == "MQTT")
                    dataType = "Bool";

                switch (outputType)
                {
                    case "On-board":
                        tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.OnBoardIO).Select(L => L.Tag).ToList());
                        //tagList.AddRange(xm.LoadedProject.Tags.Where(t => t.LogicalAddress.Contains(".")&& t.LogicalAddress.StartsWith("Q") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                        //tagList.Sort();
                        break;
                    case "Remote":
                        tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.RemoteIO).Select(L => L.Tag).ToList());
                        break;
                    case "Expansion":
                        tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO).Select(L => L.Tag).ToList());
                        break;
                    case "Memory Address Variable":
                        Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);
                        if (comboBoxInstructionType.Text == "Data Conversion")
                        {
                            dataType = selectedInstruction.OutputDataTypes.FirstOrDefault().ToString();
                        }
                        var TagOP1 = XMProValidator.FillTagOperands(dataType, udfbname);
                        //var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                        //var NonOnBoard = TagOP1.Remove(T=>T.io);
                        tagList.AddRange(TagOP1);
                        tagList.Remove("-Select Tag Name-");
                        //bool fbDataType = actualDataType == "TON" || actualDataType == "TOFF" || actualDataType == "RTON" || actualDataType == "TP" || actualDataType == "CTU" || actualDataType == "CTD";
                        if (dataType == "Bool")
                        {
                            tagList.RemoveAll(T => T.StartsWith("DigitalInput_DI") || T.StartsWith("DigitalOutput_DO"));
                        }
                        //tagList.AddRange(xm.LoadedProject.Tags.Where(L => L.Label == dataType).Select(L => L.Tag).ToList());
                        break;
                }
                TagOutput1.DataSource = tagList;


            }
            else
            {
                if ((!comboBoxInstruction.Text.ToString().Equals("Pack") || !comboBoxInstruction.Text.ToString().Equals("UnPack")) && (comboBoxOutputType.Text.ToString().Equals("On-board") || comboBoxOutputType.Text.ToString().Equals("Remote") || comboBoxOutputType.Text.ToString().Equals("Expansion")))
                {
                    //TagOutput1.Items.Clear();
                    tagList.Clear();
                    tagList.Add("-Select Tag Name-");
                }
                else
                {
                    if (comboBoxInstruction.Text.ToString().Equals("Pack"))
                    {
                        var TagOP1 = XMProValidator.FillTagOperands("Pack-Word", udfbname);
                        var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                        TagOutput1.DataSource = TagOP1;
                        tagList.AddRange(TagOP1);
                        tagList.Remove("-Select Tag Name-");
                    }
                    else if (comboBoxInstruction.Text.ToString().Equals("UnPack"))
                    {
                        var TagOP1 = XMProValidator.FillTagOperands("Pack-Bool", udfbname);
                        //var TagOP1 = XMProValidator.FillTagOperands(selectedInstruction.OutputDataTypes.Select(D => D.Text).FirstOrDefault(), udfbname);
                        //Remove Tags with Default IO List Type
                        var NonDTags = TagOP1.RemoveAll(DefaultAddedTag);
                        TagOutput1.DataSource = TagOP1;
                        tagList.AddRange(TagOP1);
                        tagList.Remove("-Select Tag Name-");
                    }
                }
            }

            if (comboBoxInstruction.Text.ToString().Equals("EXP") && (comboBoxOutputType.Text.ToString().Equals("On-board") || comboBoxOutputType.Text.ToString().Equals("Remote") || comboBoxOutputType.Text.ToString().Equals("Expansion")))
            {
                //TagOutput1.Items.Clear();
                tagList.Clear();
                tagList.Add("-Select Tag Name-");
            }
            else if (comboBoxInstruction.Text.ToString().Equals("EXP") && comboBoxOutputType.Text.ToString().Equals("Memory Address Variable"))
            {
                tagList.AddRange(xm.LoadedProject.Tags.Where(T => T.Label.ToString() == "Real").Select(N => N.Tag).ToList());
            }
            TagOutput1.DataSource = tagList;
        }

        private void textBoxOp1Address_Leave(object sender, EventArgs e)
        {
            if (comboBoxInstructionType.Text.ToString().Equals("Pack"))
            {
                AllPackAddress();
            }
        }

        private void AllPackAddress()
        {
            if (comboBoxInstructionType.Text.ToString().Equals("Pack"))
            {
                string startAdd = textBoxOp1Address.Text.ToString();
                if (startAdd != "")
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
                            textBoxOp1Address.Text = nextUnusedTagAdd;
                        }
                    }

                    if (usedTagList.Count == 0 || dialog == DialogResult.OK)
                    {
                        //ONLY FOR SHOWING IN BRACKETS
                        string[] parts = textBoxOp1Address.Text.ToString().Split(':');
                        int secondPart = int.Parse(parts[1]);
                        int lastTagAdd = int.Parse(parts[1]) + 15;
                        string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                        if (secondPart + 15 > 1023)
                        {
                            labelPackAdd.Text = "Max Tag-Range Excied";
                            labelPackAdd.ForeColor = Color.Red;
                            labelPackAdd.Visible = true;
                        }
                        else
                        {
                            labelPackAdd.Text = "(" + textBoxOp1Address.Text.ToString() + "..." + endAdd + ")";
                            labelPackAdd.ForeColor = Color.Red;
                            labelPackAdd.Visible = true;
                        }
                    }
                }
            }
        }
        private void textBoxOutputAddress_Leave(object sender, EventArgs e)
        {
            if (comboBoxInstructionType.Text.ToString().Equals("UnPack"))
            {
                AllUnPackAddress();
            }
        }
        private void AllUnPackAddress()
        {
            if (comboBoxInstructionType.Text.ToString().Equals("UnPack"))
            {
                string startAdd = textBoxOutputAddress.Text.ToString();
                if (startAdd != "")
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
                            textBoxOutputAddress.Text = nextUnusedTagAdd;
                        }
                    }
                    if (usedTagList.Count == 0 || dialog == DialogResult.OK)
                    {
                        //ONLY FOR SHOWING NEXT 16 ADD IN BRACKET
                        string[] parts = textBoxOutputAddress.Text.ToString().Split(':');
                        int secondPart = int.Parse(parts[1]);
                        int lastTagAdd = int.Parse(parts[1]) + 15;
                        string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                        if (secondPart + 15 > 1023)
                        {
                            labelPackAdd.Text = "Max Tag-Range Excied";
                            labelPackAdd.ForeColor = Color.Red;
                            labelPackAdd.Visible = true;
                        }
                        else
                        {
                            labelUnpackAdd.Text = "(" + textBoxOutputAddress.Text.ToString() + "..." + endAdd + ")";
                            labelUnpackAdd.ForeColor = Color.Red;
                            labelUnpackAdd.Visible = true;
                        }
                    }
                }
            }
        }


        private List<string> FillOutputTagOperands(string outputType, string dataType, string udfbName)
        {
            List<string> tagList = new List<string> { };
            tagList.Add("-Select Tag Name-");
            switch (outputType)
            {
                case "On-board":
                    var taglistoutput = xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.OnBoardIO).ToList();
                    var tag = taglistoutput.Where(d => d.Model == udfbName + " Tags" || d.Model is null || d.Model.StartsWith("XM") || d.Model == "");
                    tagList.AddRange(tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    break;
                case "Remote":
                    var taglistRemote = xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.RemoteIO).ToList();
                    var tagRemote = taglistRemote.Where(d => d.Model == udfbName + " Tags" || d.Model is null || d.Model.StartsWith("XM") || d.Model == "");
                    tagList.AddRange(tagRemote.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    break;
                case "Expansion":
                    var taglistExpansion = xm.LoadedProject.Tags.Where(L => L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO).ToList();
                    var tagExpansion = taglistExpansion.Where(d => d.Model == udfbName + " Tags" || d.Model is null || d.Model.StartsWith("XM") || d.Model == "");
                    tagList.AddRange(tagExpansion.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    break;
                case "Memory Address Variable":
                    var tagMemory = xm.LoadedProject.Tags.Where(d => d.Model == udfbName + " Tags" || d.Model is null || d.Model.StartsWith("XM") || d.Model == "");
                    tagList.AddRange(tagMemory.Where(L => L.Label == dataType).Select(L => L.Tag).ToList());
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    tagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == dataType).Select(d => d.Text).ToList());
                    break;
            }
            var Tags = tagList.RemoveAll(DefaultAddedTag);
            return tagList;

        }

        private void btnAddUDFB_Click(object sender, EventArgs e)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "Add New Address Added in Logic";
            FunctionBlockInputsAndOutputs userControl;
            Instruction selectedInstruction = ((Instruction)comboBoxInstruction.SelectedItem);
            userControl = new FunctionBlockInputsAndOutputs();
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK)
            {
                Inputs = userControl.Inputs;
                Outputs = userControl.Outputs;
                // return true;
                this.ParentForm.DialogResult = DialogResult.OK;

            }
            else
            {
                //return false;
                this.ParentForm.DialogResult = DialogResult.Cancel;

            }
            this.ParentForm.Close();

        }

        private void cmbTopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            //xm.LoadedProject.TopicSelected = cmbTopic.SelectedItem.ToString();
        }

        private void cmbTopic_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FillOutputTags();
        }
    }
}
