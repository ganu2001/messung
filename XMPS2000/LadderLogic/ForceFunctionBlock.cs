using LadderDrawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.LadderLogic;
using XMPS2000.Core.Types;

namespace XMPS2000.LadderLogic
{
    public partial class ForceFunctionBlock : UserControl
    {
        private readonly PLCForceFunctionality forceFunctionality = PLCForceFunctionality.Instance;
        public int maxheight = 400;
        public int maxWidth = 400;
        //adding variable to check selected function block is from HSIO or normal FB
        private bool isHSIO = false;
        private Dictionary<string, bool> _modifiedControls = new Dictionary<string, bool>();
        private LadderElement currentElement = new LadderElement();
        public ForceFunctionBlock(LadderElement selectedElement)
        {
            currentElement = selectedElement;
            CreateGroupBox(selectedElement);
            InitializeComponent();
        }

        public ForceFunctionBlock(HSIO hSIO)
        {
            isHSIO = true;
            OnlineMonitoringHelper.HoldOnlineMonitor = true;
            GetHSIODetails(hSIO.HSIOBlocks, hSIO.HSIOFunctionBlockName);
            InitializeComponent();
        }
        public ForceFunctionBlock()
        {

        }
        /// <summary>
        /// Show HSIO Data 
        /// </summary>
        /// <param name="hSIOBlocks"></param>
        private void GetHSIODetails(List<HSIOFunctionBlock> hSIOBlocks, string hSIOBlockName)
        {
            List<string> tags = new List<string>();
            int x = 10;
            int y = 10;
            int i = 0;
            int newx = 0;
            int maxy = 0;

            //Getting old values of address which are used in HSIO function block.
            Dictionary<string, string> _AddressValues = new Dictionary<string, string>();
            GettingPrevHSIOTagsValues(hSIOBlockName, ref _AddressValues);

            foreach (HSIOFunctionBlock hSIO in hSIOBlocks)
            {
                if (hSIO.Text.Equals("Error"))
                    continue;
                string input = hSIO.Value;
                bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");
                if (input != "???" && containsAlphabetic)
                {
                    x = 10 + newx;
                    y = y + 25;

                    string tagName = input.Contains(":") ? XMProValidator.GetTheTagnameFromAddress(input.Replace("~", "")) : tagName = hSIO.Value.Replace("~", "");
                    /// Get the tag in variable 
                    XMIOConfig tag = XMPS.Instance.LoadedProject.Tags.Where(t => t.Tag == tagName.ToString()).FirstOrDefault();

                    //// Show tag name and logical addaress 
                    Label tagname = new Label();
                    tagname.AutoSize = true;
                    tagname.Location = new Point(x, y);
                    tagname.Text = tag.Tag.ToString() + " : " + tag.LogicalAddress.ToString();
                    this.Controls.Add(tagname);

                    //// Show type / text of attribute input or output
                    y = y + 25;
                    Label attname = new Label();
                    attname.AutoSize = true;
                    attname.Location = new Point(x, y);
                    attname.Text = "Enter Value ";
                    this.Controls.Add(attname);

                    //// Add text box to accept value from user
                    x = x + 100;
                    TextBox txtinput = new TextBox();
                    txtinput.Name = "txtinput" + i.ToString();
                    txtinput.Location = new Point(x, y);
                    txtinput.Text = txtinput.Name.ToString();
                    txtinput.Tag = tag.LogicalAddress;
                    txtinput.Text = txtinput?.Tag != null && _AddressValues?.ContainsKey(txtinput.Tag.ToString()) == true
                                    ? _AddressValues[txtinput.Tag.ToString()]
                                    : (tag?.Tag != null && _AddressValues?.ContainsKey(tag.Tag.ToString()) == true
                                    ? _AddressValues[tag.Tag.ToString()]
                                    : "0");

                    txtinput.Validating += new System.ComponentModel.CancelEventHandler(this.txtinput_Validating);
                    txtinput.TextChanged += TextBox_TextChanged;
                    this.Controls.Add(txtinput);
                    _modifiedControls[txtinput.Name] = false;
                    x = x + txtinput.Width + 20;

                    Button btnForce = new Button();
                    btnForce.Location = new Point(x, y);
                    btnForce.Text = "Force value";
                    btnForce.Name = "btnForce" + i.ToString();
                    this.Controls.Add(btnForce);
                    btnForce.Click += new System.EventHandler(this.buttonForce_Click);
                    x = x + btnForce.Width + 20;

                    Button btnunForce = new Button();
                    btnunForce.Location = new Point(x, y);
                    btnunForce.Name = "btnunForce" + i.ToString();
                    btnunForce.Text = "Unforce value";
                    btnunForce.Width = 100;
                    btnunForce.Click += new System.EventHandler(this.buttonUnForce_Click);
                    this.Controls.Add(btnunForce);
                    x = x + btnunForce.Width + 20;


                    i++;
                    if (i == 10)
                    {
                        newx = newx + 420;
                        maxy = y;
                        y = 10;
                    }

                }
            }

            maxWidth = x + 450;
            y = maxy > 0 ? maxy + 40 : y + 40;
            maxWidth = newx > 0 ? newx + 450 : x + 120;
            x = 120;
            Button btnForceAll = new Button();
            btnForceAll.Location = new Point(x, y);
            btnForceAll.Text = "Force All";
            btnForceAll.Tag = "Force All";
            this.Controls.Add(btnForceAll);
            btnForceAll.Click += new System.EventHandler(this.buttonForceAllHSIO_Click);
            x = x + btnForceAll.Width + 20;

            Button btnunForceAll = new Button();
            btnunForceAll.Location = new Point(x, y);
            btnunForceAll.Text = "Unforce All";
            btnunForceAll.Tag = "Unforce All";
            this.Controls.Add(btnunForceAll);
            btnunForceAll.Click += new System.EventHandler(this.buttonUnForceAll_Click);
            x = x + btnunForceAll.Width + 20;
            maxheight = btnunForceAll.Top + btnunForceAll.Height;

            this.Height = y + 25;
            this.Width = maxWidth;
            maxheight = btnunForceAll.Top + btnunForceAll.Height + 75;

        }

        private void GettingPrevHSIOTagsValues(string hSIOBlockName, ref Dictionary<string, string> addressValues)
        {
            List<string> _OldListTagName;
            Dictionary<string, Tuple<string, AddressDataTypes>> OldCurBlockAddressInfo;
            List<string> _systemtagsAddress = new List<string>();
            List<string> _ListTagName = new List<string>();
            List<AddressDataTypes> _Type = new List<AddressDataTypes>();
            OnlineMonitoringHelper omh = OnlineMonitoringHelper.Instance;
            Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddressInfo = new Dictionary<string, Tuple<string, AddressDataTypes>>();

            List<string> modeTagName = new List<string>();
            List<string> directionTagName = new List<string>();
            HSIO hSIO1 = XMPS.Instance.LoadedProject.HsioBlock.Where(hsio => hsio.HSIOFunctionBlockName == hSIOBlockName).FirstOrDefault();
            directionTagName.Add(hSIO1.HSIOFunctionBlockType == "Input" ? hSIO1.HSIOBlocks[9].Value.Replace("~", "") : "");
            modeTagName.Add(hSIO1.HSIOFunctionBlockType == "Input" ? hSIO1.HSIOBlocks[10].Value : "");
            foreach (HSIOFunctionBlock hSIOFunction in hSIO1.HSIOBlocks)
            {
                if (hSIOFunction.Value != "???")
                {
                    string LogicalAdd = XMProValidator.GetTheAddressFromTag(hSIOFunction.Value.Replace("~", ""));
                    if (!_systemtagsAddress.Contains(LogicalAdd) && LogicalAdd.Contains(":"))
                    {
                        string tagName1 = "";
                        string tagNameofLogicalAdd = XMProValidator.GetTheTagnameFromAddress(hSIOFunction.Value.Replace("~", ""));
                        bool showLogicalAddress = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == tagNameofLogicalAdd).Select(T => T.ShowLogicalAddress).FirstOrDefault();
                        if (showLogicalAddress)
                        {
                            tagName1 = hSIOFunction.Value.Replace("~", "");
                        }
                        else
                        {
                            tagName1 = tagNameofLogicalAdd != null ? tagNameofLogicalAdd : hSIOFunction.Value.Replace("~", "");
                        }
                        _systemtagsAddress.Add(LogicalAdd);
                        _ListTagName.Add(tagName1.Replace("~", ""));
                        _Type.Add(omh.GetAddressTypeOf(hSIOFunction.DataType));
                    }
                }
            }

            _OldListTagName = _ListTagName;
            for (int j = 0; j < _systemtagsAddress.Count; j++)
            {
                CurBlockAddressInfo.Add(_ListTagName[j], Tuple.Create(_systemtagsAddress[j], _Type[j]));
            }
            OldCurBlockAddressInfo = CurBlockAddressInfo;
            foreach (string AddressValue in _OldListTagName)
            { addressValues.Add(AddressValue, ""); }
            if (!XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD"))
            {
                bool isPingOk = XMProValidator.CheckPing();
                if (!isPingOk)
                {
                    ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain")).DisableOnlineModeFromHSIO();
                    return;
                }
            }
            OnlineMonitoring onlineMonitoring = OnlineMonitoring.GetInstance();
            onlineMonitoring.GetValues(_OldListTagName, ref OldCurBlockAddressInfo, ref addressValues, out string Result);
        }

        private void buttonForceAllHSIO_Click(object sender, EventArgs e)
        {
            bool anyForced = false;
            var forceButtons = this.Controls
                .OfType<Button>()
                .Where(btn => btn.Name.StartsWith("btnForce") && btn.Text != "Force All")
                .ToList();
            foreach (Button btn in forceButtons)
            {
                string textBoxName = btn.Name.Replace("btnForce", "txtinput");
                if (_modifiedControls.ContainsKey(textBoxName) && _modifiedControls[textBoxName])
                {
                    TextBox textBox = this.Controls[textBoxName] as TextBox;
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        MessageBox.Show("Cannot force all values: One or more fields are empty.",
                                       "XMPS 2000",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            foreach (Button btn in forceButtons)
            {
                string textBoxName = btn.Name.Replace("btnForce", "txtinput");

                if (_modifiedControls.ContainsKey(textBoxName) && _modifiedControls[textBoxName])
                {
                    TextBox textBox = this.Controls[textBoxName] as TextBox;
                    string logicalAddress = textBox.Tag.ToString();
                    string value = logicalAddress.StartsWith("P") && !textBox.Text.Contains(".")
                        ? textBox.Text.Trim() + ".00"
                        : textBox.Text;

                    CommonFunctionToForceTag(logicalAddress, value, true, false); // suppress popup
                    _modifiedControls[textBoxName] = false;
                    anyForced = true;
                }
            }

            if (anyForced)
            {
                MessageBox.Show("All modified values have been successfully forced.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No values have changed since last force operation.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddAllPackAndUnpack(string value, ref List<LadderDrawing.Attribute> tags)
        {
            string firstAdd = value.Replace("~", "");
            string[] parts = firstAdd.Split(':');
            int lastAddPart = int.Parse(parts[1]);
            int lastTagAdd = int.Parse(parts[1]) + 15;
            string attname = tags.Where(t => t.Value.ToString() == value).Select(t => t.Name).FirstOrDefault();
            attname = attname.Substring(0, attname.Length - 1);
            for (int i = int.Parse(parts[1]); i <= lastTagAdd; i++)
            {
                LadderDrawing.Attribute attribute = new LadderDrawing.Attribute();
                attribute.Name = attname;
                attribute.Value = $"{parts[0]}:{i.ToString("D3")}";
                if (attribute.Value.ToString().Trim() != value.Trim()) // Ignore original value
                    tags.Add(attribute);
            }
        }
        private void CreateGroupBox(LadderElement selectedElement)
        {
            //creating panel and adding all the attribute control over the panel.
            Panel basePane = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };
            int x = 10;
            int y = 10;
            int i = 0;
            int newx = 0;
            int maxy = 0;
            string functionnm = selectedElement.Attributes["function_name"].ToString();
            functionnm = functionnm.StartsWith("MES_PID_") ? "MES_PID" : functionnm;
            var udfbInfo = XMPS.Instance.LoadedProject.UDFBInfo
                  .FirstOrDefault(u => u.UDFBName.IndexOf(functionnm, StringComparison.OrdinalIgnoreCase) >= 0);
            InstructionTypeDeserializer instruction = XMPS.Instance.instructionsList.FirstOrDefault(t => t.Text.Equals(functionnm));
            bool changeText = ((functionnm.Equals("Pack") || functionnm.Equals("UnPack") || functionnm.StartsWith("MQTT")) || instruction == null)
                              ? false : true;
            List<LadderDrawing.Attribute> myinlist = selectedElement.Attributes.Where(t => t.Name.StartsWith("in") && t.Value != null && !t.Value.ToString().EndsWith("-")
                                                     && !t.Value.ToString().Equals("") && !t.Value.ToString().Equals("A5:999") && !t.Value.ToString().Equals("NULL"))
                                                    .Concat(selectedElement.Attributes.Where(t => t.Name.StartsWith("out") && t.Value != null && !t.Value.ToString().EndsWith("-")
                                                    && !t.Value.ToString().Equals("") && !t.Value.ToString().Equals("A5:999") && !t.Value.ToString().Equals("NULL")))
                                                    .ToList();

            if (functionnm == "Pack")
            {
                LadderDrawing.Attribute newatt = selectedElement.Attributes.Where(t => (t.Name.StartsWith("input1"))).FirstOrDefault();
                AddAllPackAndUnpack(newatt.Value.ToString(), ref myinlist);
                myinlist = myinlist.OrderBy(t => t.Name).OrderBy(t => t.Value).ToList();
            }
            else if (functionnm == "UnPack")
            {
                LadderDrawing.Attribute newatt = selectedElement.Attributes.Where(t => (t.Name.StartsWith("output1"))).FirstOrDefault();
                AddAllPackAndUnpack(newatt.Value.ToString(), ref myinlist);
                myinlist = myinlist.OrderByDescending(t => t.Name).OrderByDescending(t => t.Value).ToList();
            }
            int input = 1;
            int output = 1;
            foreach (LadderDrawing.Attribute attrbt in myinlist)
            {
                if (attrbt != null && !int.TryParse(attrbt.Value.ToString(), out _) && !double.TryParse(attrbt.Value.ToString(), out _)) //Discard out paramenter using _ enter the loop only if the attribute is not null and not numeric value i.e. Tag is used
                {
                    if ((functionnm == "Notification" || functionnm == "Device") && i == 0)
                        goto SkipTextAddition;
                    x = 10 + newx;
                    y = y + 25;

                    /// Get the tag in variable 
                    XMIOConfig tag = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == attrbt.Value.ToString().Replace("~", "")).FirstOrDefault();

                    //// Show tag name and logical addaress 
                    Label tagname = new Label();
                    tagname.AutoSize = true;
                    tagname.Location = new Point(x, y);
                    tagname.Text = tag?.Tag.ToString() + " : " + attrbt.Value.ToString();
                    basePane.Controls.Add(tagname);

                    string type = attrbt.Name.ToString().StartsWith("input") ? "Input" : "Output";
                    //// Show type / text of attribute input or output
                    y = y + 25;
                    Label attname = new Label();
                    attname.AutoSize = true;
                    attname.Location = new Point(x, y);
                    //showing first 20 character from the Input or Output Text.
                    input = type == "Input" ? int.Parse(attrbt.Name.Replace("input", "") == "" ? "0" : attrbt.Name.Replace("input", "")) : input;
                    output = type == "Output" ? int.Parse(attrbt.Name.Replace("output", "") == "" ? "0" : attrbt.Name.Replace("output", "")) : output;
                    string newText = changeText ? instruction.InputsOutputs.FirstOrDefault(t => t.Type.Equals(type) && t.Id == (type == "Input" ? input : output)).Text : attrbt.Name.ToString().Replace("input", "IN");
                    attname.Text = newText.Length > 20 ? newText.Substring(0, 20) : newText;
                    if (attname.Text.Contains("IN"))
                        attname.Text = GetUDFBlockText("Input", attname.Text, udfbInfo);
                    else if (attname.Text.Contains("output"))
                        attname.Text = GetUDFBlockText("Output", attname.Text, udfbInfo);

                    basePane.Controls.Add(attname);
                    if (type == "Input")
                        input++;
                    else
                        output++;
                    //// Add text box to accept value from user

                    x = (functionnm == "Pack" || functionnm == "UnPack" || functionnm.StartsWith("MES_PID") || selectedElement.Attributes["OpCode"].ToString().Equals("9999")) ? x + 100 : x + 150;
                    TextBox txtinput = new TextBox();
                    txtinput.Name = "txtinput" + i.ToString();
                    txtinput.Location = new Point(x, y);
                    txtinput.Text = txtinput.Name.ToString();
                    txtinput.Tag = tag == null ? attrbt.Value : tag.LogicalAddress;
                    if (functionnm == "Schedule" && tag == null)
                    {
                        txtinput.Tag = XMPS.Instance.LoadedProject.BacNetIP.Schedules.Where(s => s.ObjectName == attrbt.Value.ToString().Trim()).Select(s => s.LogicalAddress).FirstOrDefault();
                        tag = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == txtinput.Tag.ToString().Replace("~", "")).FirstOrDefault();
                    }
                    //txtinput.Tag = tag == null ? XMPS.Instance.LoadedProject.Tags.Where(t => t.Tag == ).Select(t => t.LogicalAddress).FirstOrDefault() : tag.LogicalAddress;
                    txtinput.Text = (type.Equals("Input") && (functionnm.Equals("MQTT Publish") || functionnm.Equals("MQTT Subscribe"))) ? "0" : OnlineMonitoringStatus.AddressValues.ContainsKey(txtinput.Tag.ToString()) ? OnlineMonitoringStatus.AddressValues[txtinput.Tag.ToString()]
                                  : OnlineMonitoringStatus.AddressValues.ContainsKey(tag.Tag.ToString()) ? OnlineMonitoringStatus.AddressValues[tag.Tag] : "0"; //"0"; 

                    txtinput.Validating += new System.ComponentModel.CancelEventHandler(this.txtinput_Validating);
                    txtinput.TextChanged += TextBox_TextChanged;
                    if (XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == txtinput.Tag.ToString()).Select(t => t.ReadOnly).FirstOrDefault())
                        txtinput.Enabled = false;
                    basePane.Controls.Add(txtinput);

                    _modifiedControls[txtinput.Name] = false;
                    x = x + txtinput.Width + 20;

                    Button btnForce = new Button();
                    btnForce.Location = new Point(x, y);
                    btnForce.Text = "Force value";
                    btnForce.Name = "btnForce" + i.ToString();
                    btnForce.Tag = attrbt.Value.ToString();
                    basePane.Controls.Add(btnForce);
                    btnForce.Click += new System.EventHandler(this.buttonForce_Click);
                    x = x + btnForce.Width + 20;

                    Button btnunForce = new Button();
                    btnunForce.Location = new Point(x, y);
                    btnunForce.Name = "btnunForce" + i.ToString();
                    btnunForce.Text = "Unforce value";
                    btnunForce.Width = 100;
                    btnunForce.Tag = attrbt.Value.ToString();
                    btnunForce.Click += new System.EventHandler(this.buttonUnForce_Click);

                    basePane.Controls.Add(btnunForce);
                    x = x + btnunForce.Width + 20;
                SkipTextAddition:
                    i++;
                    //showing 10 inputs or outputs on one column
                    if (i % 10 == 0)
                    {
                        newx = newx + 480;
                        maxy = y;
                        y = 10;
                    }

                }

            }
            y = maxy > 0 ? maxy + 40 : y + 40;
            x = 120;
            Button btnForceAll = new Button();
            btnForceAll.Location = new Point(x, y);
            btnForceAll.Text = "Force All";
            btnForceAll.Tag = "Force All";
            basePane.Controls.Add(btnForceAll);
            btnForceAll.Click += new System.EventHandler(this.buttonForceAll_Click);
            x = x + btnForceAll.Width + 20;

            Button btnunForceAll = new Button();
            btnunForceAll.Location = new Point(x, y);
            btnunForceAll.Text = "Unforce All";
            btnunForceAll.Tag = "Unforce All";
            basePane.Controls.Add(btnunForceAll);
            btnunForceAll.Click += new System.EventHandler(this.buttonUnForceAll_Click);
            x = x + btnunForceAll.Width + 20;

            maxWidth = basePane.Width + 800;
            maxheight = btnunForceAll.Top + btnunForceAll.Height + 75;
            this.Controls.Add(basePane);

        }
        private string GetUDFBlockText(string type, string attnameText, UDFBInfo udfbInfo)
        {
            if (udfbInfo == null || string.IsNullOrEmpty(attnameText))
                return attnameText;

            var match = Regex.Match(attnameText, @"\d+");
            if (!match.Success)
                return attnameText;

            int index = int.Parse(match.Value);
            if (index <= 0)
                return attnameText;

            return udfbInfo.UDFBlocks
                .Where(block => block.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                .Skip(index - 1)
                .Select(block => block.Text)
                .FirstOrDefault() ?? attnameText;
        }
        private void buttonUnForceAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show(forceFunctionality.SendUnforceAllFrame(), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonUnForce_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string name = button.Name;
            name = name.Replace("btnunForce", "txtinput");
            TextBox textBox = isHSIO ? this.Controls[name] as TextBox : this.Controls[0].Controls[name] as TextBox;
            string logicalAddress = textBox.Tag.ToString();
            string value = textBox.Tag.ToString().StartsWith("P") && !textBox.Text.Contains(".") ? textBox.Text.Trim() + ".00" : textBox.Text;
            CommonFunctionToForceTag(logicalAddress, "0", false);
        }
        public void CommonFunctionToForceTag(string Address, string value, bool isforce = true, bool showPopup = true)
        {
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
            if (!XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress == Address))
                return;
            if (isforce)
            {
                if (!XMPS.Instance.Forcedvalues.Contains(XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == Address).Select(t => t.Tag).FirstOrDefault()))
                    XMPS.Instance.Forcedvalues.Add(XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == Address).Select(t => t.Tag).FirstOrDefault());
            }
            else
                XMPS.Instance.Forcedvalues.Remove(XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == Address.ToString()).Select(t => t.Tag).FirstOrDefault());

            string result = forceFunctionality.CreateAndSendFrame(Address, value, isforce);
            if (showPopup)
            {
                MessageBox.Show(result, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            _modifiedControls[textBox.Name] = true;
        }
        private void buttonForceAll_Click(object sender, EventArgs e)
        {
            bool anyForced = false;
            var forceButtons = this.Controls[0].Controls
                .OfType<Button>()
                .Where(btn => btn.Name.StartsWith("btnForce") && btn.Text != "Force All")
                .ToList();
            foreach (Button btn in forceButtons)
            {
                string textBoxName = btn.Name.Replace("btnForce", "txtinput");
                if (_modifiedControls.ContainsKey(textBoxName) && _modifiedControls[textBoxName])
                {
                    TextBox textBox = this.Controls[0].Controls[textBoxName] as TextBox;
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        MessageBox.Show("Cannot force all values: One or more fields are empty.",
                                       "XMPS 2000",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            foreach (Button btn in forceButtons)
            {
                string textBoxName = btn.Name.Replace("btnForce", "txtinput");

                if (_modifiedControls.ContainsKey(textBoxName) && _modifiedControls[textBoxName])
                {
                    TextBox textBox = this.Controls[0].Controls[textBoxName] as TextBox;
                    string logicalAddress = textBox.Tag.ToString();
                    string value = CommonFunctions.IsRealValue(logicalAddress) && !textBox.Text.Contains(".")
                        ? textBox.Text.Trim() + ".00"
                        : textBox.Text;

                    CommonFunctionToForceTag(logicalAddress, value, true, false); // suppress popup
                    _modifiedControls[textBoxName] = false;
                    anyForced = true;
                }
            }

            if (anyForced)
            {
                MessageBox.Show("All modified values have been successfully forced.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No values have changed since last force operation.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void buttonForce_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string textBoxName = button.Name.Replace("btnForce", "txtinput");
            TextBox textBox = this.Controls[textBoxName] as TextBox;
            if (textBox == null) textBox = this.Controls[0].Controls[textBoxName] as TextBox;
            if (!textBox.Enabled)
            {
                MessageBox.Show("Cannot force value: The field is read only.",
                             "XMPS 2000",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error);
                return;
            }
            if (!ValidateControl(textBox))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Cannot force value: The field is empty.",
                               "XMPS 2000",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }
            string logicalAddress = textBox.Tag.ToString();
            string value = CommonFunctions.IsRealValue(logicalAddress) && !textBox.Text.Contains(".")
                ? textBox.Text.Trim() + ".00"
                : textBox.Text;

            if (_modifiedControls.ContainsKey(textBoxName))
            {
                _modifiedControls[textBoxName] = false;
            }

            CommonFunctionToForceTag(logicalAddress, value); // default showPopup = true
        }

        private bool ValidateControl(Control control)
        {
            if (control == null)
                return false;
            var args = new CancelEventArgs();
            this.txtinput_Validating(control, args);
            return !args.Cancel;
        }
        private bool IsRealValue(string logicalAddress)
        {
            string datatype = XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == logicalAddress.ToString() && L.Type == IOType.DataType).Select(T => T.Label).FirstOrDefault();
            if (datatype == null)
                datatype = logicalAddress.ToString().Contains(".") ? "Bool" : XMPS.Instance.LoadedProject.CPUDatatype;
            if (datatype == "Real")
                return true;
            else
                return false;
        }

        private void txtinput_Validating(object sender, CancelEventArgs e)
        {
            //ValidateOperand(sender, e);
            bool validationSuccessful = true;
            string error = string.Empty;
            validationSuccessful = ValidateNumericOperand((TextBox)sender, out error);
            e.Cancel = !validationSuccessful;
            errorProvider.SetError((TextBox)sender, validationSuccessful ? null : error);
        }

        private bool ValidateNumericOperand(Control control, out string error)
        {
            string TagAddress = control.Tag.ToString();
            string number = control.Text;
            string datatype = XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == control.Tag.ToString() && L.Type == IOType.DataType).Select(T => T.Label).FirstOrDefault();
            if (datatype == null)
                datatype = control.Tag.ToString().Contains(".") ? "Bool" : XMPS.Instance.LoadedProject.CPUDatatype;
            if (currentElement.Attributes["function_name"].ToString() == "W_P_V" && control.Name == "txtinput1" && this.Controls[0].Controls[6].Text == "Priority")
            {
                 
                error = "Invalid numeric value, Please enter a number from 1 to 16, excluding 6.";
                if (Int32.TryParse(number, out int value))
                    return value >= 1 && value <= 16 && value != 6;
                else
                    return false;
            }
            if (number != "")
            {
                return XMProValidator.ValidateNumericInputOperand(number, datatype, out error, TagAddress);
            }
            error = string.Empty;
            return true;
        }
    }
}