using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.LadderLogic;
using XMPS2000.User_Control;
using Control = System.Windows.Forms.Control;
using GroupBox = System.Windows.Forms.GroupBox;
using Label = System.Windows.Forms.Label;
using Panel = System.Windows.Forms.Panel;
using ToolTip = System.Windows.Forms.ToolTip;

namespace XMPS2000
{
    public partial class frmHSIOConfigeration : Form, IXMForm
    {
        string SelectedTabPageName = "";
        public List<CustomLinkLabel> customLinkLabels = new List<CustomLinkLabel>();
        public ToolTip HSIOToolTip;
        public ToolTip labelText;
        private bool istoolTipShowing = false;
        public string firstBoxInterruptBlock = "";
        public string secondBoxInterruptBlock = "";
        public string thirdBoxInterruptBlock = "";
        public string fourthBoxInterruptBlock = "";
        private Dictionary<string, string> _AddressValues;
        private Dictionary<string, Tuple<string, AddressDataTypes>> OldCurBlockAddressInfo;
        private List<string> _OldListTagName;
        private List<string> OldmodeTagName;
        private List<string> OlddirectionTagName;
        private string CurrentHSIOFunctionBlock = "";
        private string CureentInputTextName = "";
        private string CurrentOMHSFb = "";
        private Control CurrentFBForToolTip = null;
        private static Dictionary<int, string> ErrorCodes = new Dictionary<int, string>{
        { 0, "NO_Error" }, { 1, "Bias_F_>_Set_F" }, { 2, "Stop_F_>_Set_F" },{ 3, "Slow_F_>_Set_F" },
        { 4, "Accel_P_>_Total_P" }, { 5, "Decel_P_>_Total_P" }, { 6, "Slow_P_>_Total_P" },{ 7, "Comp_>_Comp_H" },
        { 8, "Comp_L_>_Comp_H" }, { 9, "Set_F_>_Max_F" }, { 10, "Bias_F_>_Max_F" },{ 11, "Slow_F_>_Max_F" },{ 12, "Stop_F_>_Max_F" }};

        public frmHSIOConfigeration()
        {
            InitializeComponent();
            HSIOToolTip = new ToolTip();
            labelText = new ToolTip();
            TabPage tabPage = Tabcntrl.SelectedTab;
            //Adding interrupts Logic blocks Names into the Interrupt ComboBox
            List<string> interruptsName = new List<string>();
            interruptsName.Add("None");
            interruptsName.AddRange(XMPS.Instance.LoadedProject.Blocks.Where(T => T.Type == "InterruptLogicBlock").Select(T => T.Name).ToList());
            this.comboBoxSelectInLogicBlock1.DataSource = interruptsName.ToList();
            this.comboBoxSelectInLogicBlock2.DataSource = interruptsName.ToList();
            this.comboBoxSelectInLogicBlock3.DataSource = interruptsName.ToList();
            this.comboBoxSelectInLogicBlock4.DataSource = interruptsName.ToList();
            this.comboBoxSelectInLogicBlock1.SelectedIndex = GetIndexOfInterruptBlk(XMPS.Instance.LoadedProject.FirstInterruptBlock);
            this.comboBoxSelectInLogicBlock2.SelectedIndex = GetIndexOfInterruptBlk(XMPS.Instance.LoadedProject.SecondInterruptBlock);
            this.comboBoxSelectInLogicBlock3.SelectedIndex = GetIndexOfInterruptBlk(XMPS.Instance.LoadedProject.ThirdInterruptBlock);
            this.comboBoxSelectInLogicBlock4.SelectedIndex = GetIndexOfInterruptBlk(XMPS.Instance.LoadedProject.FourthInterruptBlock);
            this.comboBoxType1.SelectedIndex = 0;
            this.comboBoxType2.SelectedIndex = 0;
            this.comboBoxType3.SelectedIndex = 0;
            this.comboBoxType4.SelectedIndex = 0;
            string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\HSIODesign.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode root = xmlDoc.SelectSingleNode("/Root");
            XmlNodeList itemNodes = root.SelectNodes("HSIOFunctionBlockDraw");
            DesignHSIOBlocks(itemNodes);
            SaveHSIOFunctionBlock(itemNodes);
        }

        private int GetIndexOfInterruptBlk(string firstInterruptBlock)
        {
            switch (firstInterruptBlock)
            {
                case "Interrupt_Logic_Block01":
                    return 1;
                case "Interrupt_Logic_Block02":
                    return 2;
                case "Interrupt_Logic_Block03":
                    return 3;
                case "Interrupt_Logic_Block04":
                    return 4;
                default: return 0;
            }
        }

        private void DesignHSIOBlocks(XmlNodeList itemNodes)
        {
            DrawDesign(itemNodes);
        }

        public frmHSIOConfigeration(string HSIOname)
        {
            InitializeComponent();
        }
        public void OnShown()
        {
            HSIOsOMtimer.Start();
            //Redesing the HSIO Function Blocks.
            string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\HSIODesign.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode root = xmlDoc.SelectSingleNode("/Root");
            XmlNodeList itemNodes = root.SelectNodes("HSIOFunctionBlockDraw");
            DesignHSIOBlocks(itemNodes);
        }

        private void HSIOsOfflineMonitoring()
        {
            string selectedTagPageName = Tabcntrl.SelectedTab.Text;
            string tabPageName = selectedTagPageName.Contains("Output") ? "tabOutput" : "Input";
            TabPage tabPage = Tabcntrl.TabPages[tabPageName];

            string panelName = selectedTagPageName.Contains("Input") ? "inputMainPanel" : "outputMainPanel";
            Panel panel = tabPage.Controls.Find(panelName, true).FirstOrDefault() as System.Windows.Forms.Panel;

            if (panel != null)
            {
                foreach (Control GroupBoxes in panel.Controls)
                {
                    if (GroupBoxes is System.Windows.Forms.GroupBox groupBox)
                    {
                        if ((XMPS.Instance.PlcStatus == "LogIn" && groupBox.Text != CurrentOMHSFb) || (XMPS.Instance.PlcStatus == "LogOut"))
                        {
                            GroupBoxes.BackColor = Color.White;
                            foreach (Control cntrls in GroupBoxes.Controls)
                            {
                                if (cntrls is LinkLabel)
                                {
                                    cntrls.Text = cntrls.Text.Split('[')[0].Replace(" ", "");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Tabcntrl_Click(object sender, EventArgs e)
        {

        }
        private void DrawDesign(XmlNodeList itemNodes)
        {
            this.Input.Controls.Clear();
            this.tabOutput.Controls.Clear();
            Panel inputMainPanel = new Panel();
            inputMainPanel.Name = "inputMainPanel";
            inputMainPanel.Size = new Size(1200, 850);
            inputMainPanel.Dock = DockStyle.Fill;
            inputMainPanel.AutoScroll = true;

            Panel outputMainPanel = new Panel();
            outputMainPanel.Name = "outputMainPanel";
            outputMainPanel.Size = new Size(1200, 850);
            outputMainPanel.Dock = DockStyle.Fill;
            outputMainPanel.AutoScroll = true;

            int inputCounter = 0;
            int outputCounter = 0;

            foreach (XmlNode itemNode in itemNodes)
            {
                SelectedTabPageName = itemNode.SelectSingleNode("HSIOFunctionBlockType").InnerText;
                string blockName = itemNode.SelectSingleNode("HSIOFunctionBlockName").InnerText;
                if (SelectedTabPageName.Contains("Input"))
                {
                    GroupBox groupBox = CreateGroupBox(blockName, itemNode);

                    // Calculate the Y-coordinate based on the inputCounter
                    int yCoordinate = 10 + inputCounter * 365;

                    groupBox.Location = new System.Drawing.Point(150, yCoordinate);
                    groupBox.Size = new System.Drawing.Size(800, 350);

                    inputMainPanel.Controls.Add(groupBox);
                    inputCounter++;
                }

                if (SelectedTabPageName.Contains("Output"))
                {
                    GroupBox groupBox = CreateGroupBox(blockName, itemNode);
                    groupBox.Size = new System.Drawing.Size(800, 450);

                    int yCoordinate = 10 + outputCounter * (groupBox.Height + 10);

                    groupBox.Location = new System.Drawing.Point(150, yCoordinate);

                    outputMainPanel.Controls.Add(groupBox);
                    outputCounter++;
                }
            }

            this.Input.Controls.Add(inputMainPanel);
            this.tabOutput.Controls.Add(outputMainPanel);
        }

        private GroupBox CreateGroupBox(string blockName, XmlNode itemNode)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Text = blockName;
            groupBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainGroupBoxMouseMove);
            groupBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainGroupBoxMouseClikck);
            if (SelectedTabPageName == "Input")
            {
                //All the Input Names and Data Type
                List<string> linkLabelsHSIInputsText = new List<string>();
                List<string> linkLabelsHSIOutoutsText = new List<string>();

                List<string> linkLabelsHSIInputsDataTypes = new List<string>();
                List<string> linkLabelsHSIoutputsDataTypes = new List<string>();
                groupBox.Size = new System.Drawing.Size(350, 130);

                // Calculate the size of the square as half of the GroupBox
                int squareSize = groupBox.Width;
                // Create a square
                Panel square = new Panel();
                square.BorderStyle = BorderStyle.FixedSingle; // Set the color of the square
                square.Size = new Size(190, 240);
                square.Location = new Point((groupBox.Width) / 2 + 120, (groupBox.Height) / 2 - 20);
                Label HSIOName = new Label();

                HSIOName.Text = itemNode.SelectSingleNode("HSIOFunctionBlockName").InnerText;
                HSIOName.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                HSIOName.TextAlign = ContentAlignment.MiddleCenter;
                HSIOName.Dock = DockStyle.None;
                HSIOName.AutoSize = false;
                HSIOName.Left = 40;

                //square.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SqureMouseMove);
                square.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SqureMouseClick);
                square.MouseEnter += new EventHandler(this.squareMouseEnter);
                square.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SquareMouseDoubleClick);
                square.MouseLeave += new EventHandler(this.SquareMouseLeve);

                square.Controls.Add(HSIOName);
                int x = 2;
                int Y = 40;
                int yOffset = 21;
                XmlNode hsiodrawingsNode = itemNode.SelectSingleNode("HSIODrawings");
                foreach (XmlNode hsiodrawingNode in hsiodrawingsNode)
                {
                    string type = hsiodrawingNode.SelectSingleNode("Type").InnerText;
                    Label Parameters = new Label();
                    if (type.Contains("Input"))
                    {
                        Parameters.Text = hsiodrawingNode.SelectSingleNode("Text").InnerText;
                        Parameters.AutoSize = true;
                        string dataType = hsiodrawingNode.SelectSingleNode("DataType").InnerText;
                        linkLabelsHSIInputsText.Add(Parameters.Text);
                        linkLabelsHSIInputsDataTypes.Add(dataType);
                        Parameters.Font = new Font(FontFamily.GenericSansSerif, 8);
                        Parameters.Location = new System.Drawing.Point(x, Y);
                        square.Controls.Add(Parameters);
                    }
                    if (type.Contains("Output"))
                    {
                        Label RightLabel = new Label();
                        RightLabel.Text = hsiodrawingNode.SelectSingleNode("Text").InnerText;
                        string dataType = hsiodrawingNode.SelectSingleNode("DataType").InnerText;
                        linkLabelsHSIOutoutsText.Add(RightLabel.Text);
                        linkLabelsHSIoutputsDataTypes.Add(dataType);
                        RightLabel.Location = new System.Drawing.Point(x + Parameters.Width + 10, Y - 185); // Adjust the position as needed
                        square.Controls.Add(RightLabel);
                    }

                    Y = Y + yOffset;
                }
                groupBox.Controls.Add(square);
                //Getting Data For HSIO From Project File
                string HSIOFunctionBlockName = itemNode.SelectSingleNode("HSIOFunctionBlockName").InnerText;
                List<HSIOFunctionBlock> hsioBlocks = XMPS.Instance.LoadedProject.HsioBlock.Where(hsio => hsio.HSIOFunctionBlockName == HSIOFunctionBlockName).SelectMany(hsio => hsio.HSIOBlocks).ToList();
                for (int j = 0; j < linkLabelsHSIInputsDataTypes.Count(); j++)
                {
                    LinkLabel labelI = new LinkLabel();
                    labelI.Controls.Clear();
                    labelI.Font = new Font(labelI.Font.FontFamily, 10); // Set font size to 10
                    labelI.Location = new Point(55, (groupBox.Height) / 6 * (j + 4));
                    labelI.AutoSize = true;
                    labelI.ForeColor = Color.Black;
                    labelI.LinkColor = Color.Black;
                    labelI.RightToLeft = System.Windows.Forms.RightToLeft.No;
                    labelI.LinkBehavior = LinkBehavior.NeverUnderline;
                    string ActualValue = hsioBlocks.Count() > 0 ? hsioBlocks.Where(T => T.Text == linkLabelsHSIInputsText[j]).Select(T => T.Value).FirstOrDefault().ToString() : "???";
                    if (j == 1 && HSIOFunctionBlockName.StartsWith("HSI"))
                    {
                        string hsno = HSIOFunctionBlockName.Split('-')[1];
                        string input = ActualValue;
                        bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");
                        if (containsAlphabetic && !ActualValue.Equals("???"))
                        {
                            string actualTagValue = ActualValue;
                            bool isNegationPresent = false;
                            if (ActualValue.StartsWith("~"))
                            {
                                actualTagValue = actualTagValue.Replace("~", "");
                                isNegationPresent = true;
                            }
                            string tagName = (isNegationPresent && !ActualValue.Contains(":")) ? actualTagValue : XMProValidator.GetTheTagnameFromAddress(actualTagValue);
                            bool showLogicalAddress = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == tagName).Select(T => T.ShowLogicalAddress).FirstOrDefault();
                            if (showLogicalAddress)
                            {
                                if (!isNegationPresent)
                                    labelI.Text = ActualValue != "" ? ActualValue : "???";
                                else
                                {
                                    if (ActualValue.Contains(":"))
                                        labelI.Text = ActualValue != "" ? ActualValue : "???";
                                    else
                                    {
                                        string logicalAddress = XMProValidator.GetTheAddressFromTag(tagName);
                                        labelI.Text = tagName != "" ? "~" + logicalAddress : ActualValue;
                                    }
                                }
                            }
                            else
                            {
                                if (isNegationPresent)
                                    labelI.Text = ActualValue != "" ? tagName != null ? "~" + tagName : ActualValue : "???";
                                else
                                    labelI.Text = ActualValue != "" ? tagName != null ? tagName : ActualValue : "???";
                            }
                        }
                        else
                            labelI.Text = ActualValue != "???" ? ActualValue : $"DigitalInput_DI{Convert.ToInt16(hsno) - 1}";
                    }
                    else
                    {
                        string actualTagValue = ActualValue;
                        bool isNegationPresent = false;
                        if (ActualValue.StartsWith("~"))
                        {
                            actualTagValue = actualTagValue.Replace("~", "");
                            isNegationPresent = true;
                        }
                        string tagName = (isNegationPresent && !ActualValue.Contains(":")) ? actualTagValue : XMProValidator.GetTheTagnameFromAddress(actualTagValue);
                        bool showLogicalAddress = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == tagName).Select(T => T.ShowLogicalAddress).FirstOrDefault();
                        if (showLogicalAddress)
                        {
                            if (!isNegationPresent)
                                labelI.Text = ActualValue != "" ? ActualValue : "???";
                            else
                            {
                                if (ActualValue.Contains(":"))
                                    labelI.Text = ActualValue != "" ? ActualValue : "???";
                                else
                                {
                                    string logicalAddress = XMProValidator.GetTheAddressFromTag(tagName);
                                    labelI.Text = tagName != "" ? "~" + logicalAddress : ActualValue;
                                }
                            }
                        }
                        else
                        {
                            if (isNegationPresent)
                                labelI.Text = ActualValue != "" ? tagName != null ? "~" + tagName : ActualValue : "???";
                            else
                                labelI.Text = ActualValue != "" ? tagName != null ? tagName : ActualValue : "???";
                        }
                    }
                    labelI.Left -= labelI.Text.Length - 5;
                    labelI.Tag = "bit";
                    CustomLinkLabel customLabel = new CustomLinkLabel
                    {
                        LinkLabel = labelI,
                        Index = j,
                        Type = "Input",
                        Text = linkLabelsHSIInputsText[j],
                        DataType = linkLabelsHSIInputsDataTypes[j],
                    };
                    customLinkLabels.Add(customLabel);
                    labelI.Click += new System.EventHandler(this.Hyperlinkclicked);
                    groupBox.Controls.Add(labelI);

                    int attachmentY = labelI.Top + (labelI.Height / 2) + 2;

                    if (labelI.Text == "???")
                    {
                        groupBox.Paint += (sender, e) =>
                        {
                            using (Pen pen = new Pen(Color.Black))
                            {
                                e.Graphics.DrawLine(pen, square.Left - 10, attachmentY, square.Left, attachmentY);
                            }
                        };
                    }
                    else
                    {
                        groupBox.Paint += (sender, e) =>
                        {
                            using (Pen pen = new Pen(Color.Black))
                            {
                                e.Graphics.DrawLine(pen, square.Left - 15, attachmentY, square.Left, attachmentY);
                            }
                        };
                    }
                }

                for (int j = linkLabelsHSIInputsDataTypes.Count(); j < linkLabelsHSIInputsDataTypes.Count() + linkLabelsHSIoutputsDataTypes.Count(); j++)
                {
                    LinkLabel label = new LinkLabel();
                    label.Font = new Font(label.Font.FontFamily, 10);
                    label.AutoSize = true;
                    label.LinkBehavior = LinkBehavior.NeverUnderline;
                    label.Location = new Point(groupBox.Width + 150, (groupBox.Height) / 6 * (j - 5));
                    label.ForeColor = Color.Black;
                    label.LinkColor = Color.Black;
                    string ActualValue = hsioBlocks.Count() > 0 ? hsioBlocks.Where(T => T.Text == linkLabelsHSIOutoutsText[j - linkLabelsHSIoutputsDataTypes.Count() - 1]).Select(T => T.Value).FirstOrDefault().ToString() : "???";

                    string actualTagValue = ActualValue;
                    bool isNegationPresent = false;
                    if (ActualValue.StartsWith("~"))
                    {
                        actualTagValue = actualTagValue.Replace("~", "");
                        isNegationPresent = true;
                    }
                    string tagName = (isNegationPresent && !ActualValue.Contains(":")) ? actualTagValue : XMProValidator.GetTheTagnameFromAddress(actualTagValue);
                    bool showLogicalAddress = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == tagName).Select(T => T.ShowLogicalAddress).FirstOrDefault();
                    if (showLogicalAddress)
                    {
                        if (!isNegationPresent)
                            label.Text = ActualValue != "" ? ActualValue : "???";
                        else
                        {
                            if (ActualValue.Contains(":"))
                                label.Text = ActualValue != "" ? ActualValue : "???";
                            else
                            {
                                string logicalAddress = XMProValidator.GetTheAddressFromTag(tagName);
                                label.Text = tagName != "" ? "~" + logicalAddress : ActualValue;
                            }
                        }
                    }
                    else
                    {
                        if (isNegationPresent)
                            label.Text = ActualValue != "" ? tagName != null ? "~" + tagName : ActualValue : "???";
                        else
                            label.Text = ActualValue != "" ? tagName != null ? tagName : ActualValue : "???";
                    }
                    CustomLinkLabel customLabel = new CustomLinkLabel
                    {
                        LinkLabel = label,
                        Index = j,
                        Type = "Output",
                        Text = linkLabelsHSIOutoutsText[j - linkLabelsHSIoutputsDataTypes.Count() - 1],
                        DataType = linkLabelsHSIoutputsDataTypes[j - linkLabelsHSIoutputsDataTypes.Count() - 1],
                    };
                    customLinkLabels.Add(customLabel);
                    label.Tag = "Bool";
                    label.Click += new System.EventHandler(this.Hyperlinkclicked);
                    groupBox.Controls.Add(label);
                    int attachmentY = label.Top + (label.Height / 2) + 2;
                    groupBox.Paint += (sender, e) =>
                    {
                        using (Pen pen = new Pen(Color.Black))
                        {
                            e.Graphics.DrawLine(pen, square.Right, attachmentY, label.Left, attachmentY);
                        }
                    };
                }
            }
            else if (SelectedTabPageName == "Output")
            {
                //Add Inputs and Outputs Texts
                List<string> linkLabelsHSOInputsText = new List<string>();
                List<string> linkLabelsHSOOutputsText = new List<string>();

                List<string> linkLabelsHSOInputsDataTypes = new List<string>();
                List<string> linkLabelsHSOOutputsDataTypes = new List<string>();

                groupBox.Size = new System.Drawing.Size(350, 130);

                // Calculate the size of the square as half of the GroupBox
                int squareSize = groupBox.Width;

                // Create a square
                Panel square = new Panel();
                square.BorderStyle = BorderStyle.FixedSingle; // Set the color of the square
                square.Size = new Size(190, 330);
                square.Location = new Point((groupBox.Width) / 2 + 120, (groupBox.Height) / 2 - 20);
                Label HSIOName = new Label();

                HSIOName.Text = itemNode.SelectSingleNode("HSIOFunctionBlockName").InnerText;
                HSIOName.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                HSIOName.TextAlign = ContentAlignment.MiddleCenter;
                HSIOName.Dock = DockStyle.None;
                HSIOName.AutoSize = false;
                HSIOName.Left = 40;
                square.MouseEnter += new EventHandler(this.squareMouseEnter);
                square.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SqureMouseClick);
                square.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SquareMouseDoubleClick);
                square.MouseLeave += new EventHandler(this.SquareMouseLeve);
                square.Controls.Add(HSIOName);
                int x = 2;
                int Y = 40;
                int yOffset = 21;
                XmlNode hsiodrawingsNode = itemNode.SelectSingleNode("HSIODrawings");
                foreach (XmlNode hsiodrawingNode in hsiodrawingsNode)
                {
                    string type = hsiodrawingNode.SelectSingleNode("Type").InnerText;
                    Label Parameters = new Label();
                    if (type.Contains("Input"))
                    {
                        Parameters.Text = hsiodrawingNode.SelectSingleNode("Text").InnerText;
                        Parameters.AutoSize = true;
                        string dataType = hsiodrawingNode.SelectSingleNode("DataType").InnerText;
                        linkLabelsHSOInputsText.Add(Parameters.Text);
                        linkLabelsHSOInputsDataTypes.Add(dataType);
                        Parameters.Font = new Font(FontFamily.GenericSansSerif, 8);
                        Parameters.Location = new System.Drawing.Point(x, Y);
                        square.Controls.Add(Parameters);
                    }
                    if (type.Contains("Output"))
                    {
                        Label RightLabel = new Label();
                        RightLabel.Text = hsiodrawingNode.SelectSingleNode("Text").InnerText;
                        string dataType = hsiodrawingNode.SelectSingleNode("DataType").InnerText;
                        linkLabelsHSOOutputsText.Add(RightLabel.Text);
                        linkLabelsHSOOutputsDataTypes.Add(dataType);
                        RightLabel.Location = new System.Drawing.Point(x + Parameters.Width + 10, Y - 275); // Adjust the position as needed
                        square.Controls.Add(RightLabel);
                    }

                    Y = Y + yOffset;
                }
                groupBox.Controls.Add(square);
                //Getting all the Data From Project File For HSIO-OutPut Block
                string HSIOFunctionBlockName = itemNode.SelectSingleNode("HSIOFunctionBlockName").InnerText;
                List<HSIOFunctionBlock> hsioBlocks = XMPS.Instance.LoadedProject.HsioBlock.Where(hsio => hsio.HSIOFunctionBlockName == HSIOFunctionBlockName).SelectMany(hsio => hsio.HSIOBlocks).ToList();
                if(HSIOFunctionBlockName.Equals("HSO-1") || HSIOFunctionBlockName.Equals("HSO-2"))
                {
                    int inputNo = hsioBlocks.Where(t => t.Type.StartsWith("Input")).Count();
                    if(inputNo == 13 && !hsioBlocks.Any(t => t.Text == "Reset"))
                    {
                        HSIOFunctionBlock hsioReset = new HSIOFunctionBlock("Input14", "Reset", "Bit", "???");
                        hsioBlocks.Add(hsioReset);
                    }
                }
                // Create labels
                for (int j = 0; j < linkLabelsHSOInputsDataTypes.Count(); j++)
                {
                    LinkLabel labelI = new LinkLabel();
                    labelI.Font = new Font(labelI.Font.FontFamily, 10); // Set font size to 10
                    labelI.Location = new Point(130 - labelI.Width, (groupBox.Height) / 6 * (j + 4));
                    labelI.AutoSize = true;
                    labelI.ForeColor = Color.Black;
                    labelI.LinkColor = Color.Black;
                    labelI.LinkBehavior = LinkBehavior.NeverUnderline;
                    string ActualValue = hsioBlocks.Count() > 0 ? hsioBlocks.Where(T => T.Text == linkLabelsHSOInputsText[j]).Select(T => T.Value).FirstOrDefault().ToString() : "???";
                    if (j == 1 && HSIOFunctionBlockName.StartsWith("HSO"))
                    {
                        string hsno = HSIOFunctionBlockName.Split('-')[1];
                        string input = ActualValue;
                        bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");
                        if (containsAlphabetic && !ActualValue.Equals("???"))
                        {
                            string actualTagValue = ActualValue;
                            bool isNegationPresent = false;
                            if (ActualValue.StartsWith("~"))
                            {
                                actualTagValue = actualTagValue.Replace("~", "");
                                isNegationPresent = true;
                            }
                            string tagName = (isNegationPresent && !ActualValue.Contains(":")) ? actualTagValue : XMProValidator.GetTheTagnameFromAddress(actualTagValue);
                            bool showLogicalAddress = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == tagName).Select(T => T.ShowLogicalAddress).FirstOrDefault();
                            if (showLogicalAddress)
                            {
                                if (!isNegationPresent)
                                    labelI.Text = ActualValue != "" ? ActualValue : "???";
                                else
                                {
                                    if (ActualValue.Contains(":"))
                                        labelI.Text = ActualValue != "" ? ActualValue : "???";
                                    else
                                    {
                                        string logicalAddress = XMProValidator.GetTheAddressFromTag(tagName);
                                        labelI.Text = tagName != "" ? "~" + logicalAddress : ActualValue;
                                    }
                                }
                            }
                            else
                            {
                                if (isNegationPresent)
                                    labelI.Text = ActualValue != "" ? tagName != null ? "~" + tagName : ActualValue : "???";
                                else
                                    labelI.Text = ActualValue != "" ? tagName != null ? tagName : ActualValue : "???";
                            }
                        }
                        else
                            labelI.Text = ActualValue != "???" ? ActualValue : $"DigitalOutput_DO{Convert.ToInt16(hsno) - 1}";
                    }
                    else
                    {
                        string actualTagValue = ActualValue;
                        bool isNegationPresent = false;
                        if (ActualValue.StartsWith("~"))
                        {
                            actualTagValue = actualTagValue.Replace("~", "");
                            isNegationPresent = true;
                        }
                        string tagName = (isNegationPresent && !ActualValue.Contains(":")) ? actualTagValue : XMProValidator.GetTheTagnameFromAddress(actualTagValue);
                        bool showLogicalAddress = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == tagName).Select(T => T.ShowLogicalAddress).FirstOrDefault();
                        if (showLogicalAddress)
                        {
                            if (!isNegationPresent)
                                labelI.Text = ActualValue != "" ? ActualValue : "???";
                            else
                            {
                                if (ActualValue.Contains(":"))
                                    labelI.Text = ActualValue != "" ? ActualValue : "???";
                                else
                                {
                                    string logicalAddress = XMProValidator.GetTheAddressFromTag(tagName);
                                    labelI.Text = tagName != "" ? "~" + logicalAddress : ActualValue;
                                }
                            }
                        }
                        else
                        {
                            if (isNegationPresent)
                                labelI.Text = ActualValue != "" ? tagName != null ? "~" + tagName : ActualValue : "???";
                            else
                                labelI.Text = ActualValue != "" ? tagName != null ? tagName : ActualValue : "???";
                        }
                    }
                    labelI.Left -= labelI.Text.Length - 5;
                    labelI.Tag = "bit";
                    CustomLinkLabel customLabel = new CustomLinkLabel
                    {
                        LinkLabel = labelI,
                        Index = j,
                        Type = "Input",
                        Text = linkLabelsHSOInputsText[j],
                        DataType = linkLabelsHSOInputsDataTypes[j],
                    };
                    customLinkLabels.Add(customLabel);
                    labelI.Click += new System.EventHandler(this.Hyperlinkclicked);
                    groupBox.Controls.Add(labelI);

                    int attachmentY = labelI.Top + (labelI.Height / 2) + 2;
                    if (labelI.Text == "???")
                    {
                        groupBox.Paint += (sender, e) =>
                        {
                            using (Pen pen = new Pen(Color.Black))
                            {
                                e.Graphics.DrawLine(pen, square.Left - 10, attachmentY, square.Left, attachmentY);
                            }
                        };
                    }
                    else
                    {
                        groupBox.Paint += (sender, e) =>
                        {
                            using (Pen pen = new Pen(Color.Black))
                            {
                                e.Graphics.DrawLine(pen, square.Left - 10, attachmentY, square.Left, attachmentY);
                            }
                        };
                    }
                }

                for (int j = linkLabelsHSOInputsDataTypes.Count(); j < linkLabelsHSOInputsDataTypes.Count() + linkLabelsHSOOutputsDataTypes.Count(); j++)
                {
                    LinkLabel label = new LinkLabel();
                    label.Font = new Font(label.Font.FontFamily, 10); // Set font size to 10
                    label.AutoSize = true;
                    label.LinkBehavior = LinkBehavior.NeverUnderline;
                    label.Location = new Point(groupBox.Width + 150, (groupBox.Height) / 6 * (j - 5) - 85);
                    string ActualValue = hsioBlocks.Count() > 0 ? hsioBlocks.Where(T => T.Text == linkLabelsHSOOutputsText[j - linkLabelsHSOInputsDataTypes.Count()]).Select(T => T.Value).FirstOrDefault().ToString() : "???";
                    string actualTagValue = ActualValue;
                    bool isNegationPresent = false;
                    if (ActualValue.StartsWith("~"))
                    {
                        actualTagValue = actualTagValue.Replace("~", "");
                        isNegationPresent = true;
                    }
                    string tagName = (isNegationPresent && !ActualValue.Contains(":")) ? actualTagValue : XMProValidator.GetTheTagnameFromAddress(actualTagValue);
                    bool showLogicalAddress = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == tagName).Select(T => T.ShowLogicalAddress).FirstOrDefault();
                    if (showLogicalAddress)
                    {
                        if (!isNegationPresent)
                            label.Text = ActualValue != "" ? ActualValue : "???";
                        else
                        {
                            if (ActualValue.Contains(":"))
                                label.Text = ActualValue != "" ? ActualValue : "???";
                            else
                            {
                                string logicalAddress = XMProValidator.GetTheAddressFromTag(tagName);
                                label.Text = tagName != "" ? "~" + logicalAddress : ActualValue;
                            }
                        }
                    }
                    else
                    {
                        if (isNegationPresent)
                            label.Text = ActualValue != "" ? tagName != null ? "~" + tagName : ActualValue : "???";
                        else
                            label.Text = ActualValue != "" ? tagName != null ? tagName : ActualValue : "???";
                    }
                    label.ForeColor = Color.Black;
                    label.LinkColor = Color.Black;
                    label.Tag = "Bool";
                    CustomLinkLabel customLabel = new CustomLinkLabel
                    {
                        LinkLabel = label,
                        Index = j,
                        Type = "Output",
                        Text = linkLabelsHSOOutputsText[j - linkLabelsHSOInputsDataTypes.Count()],
                        DataType = linkLabelsHSOOutputsDataTypes[j - linkLabelsHSOInputsDataTypes.Count()],
                    };
                    label.Click += new System.EventHandler(this.Hyperlinkclicked);
                    customLinkLabels.Add(customLabel);
                    groupBox.Controls.Add(label);
                    int attachmentY = label.Top + (label.Height / 2) + 2;
                    groupBox.Paint += (sender, e) =>
                    {
                        using (Pen pen = new Pen(Color.Black))
                        {
                            e.Graphics.DrawLine(pen, square.Right, attachmentY, label.Left, attachmentY);
                        }
                    };
                }
            }

            return groupBox;
        }

        private void SquareMouseLeve(object sender, EventArgs e)
        {
            HSToolTipTimer.Stop();
        }

        private void squareMouseEnter(object sender, EventArgs e)
        {
            labelText.Active = false;
            HSIOToolTip.Active = true;
            HSIOToolTip.InitialDelay = 2000;
            if (sender is Panel panel)
            {
                Control control = panel.Controls[0];
                this.CurrentFBForToolTip = control;
                HSToolTipTimer.Start();
            }
        }

        private void SquareMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HSToolTipTimer.Stop();
            HSIOToolTip.RemoveAll();
            HSIOToolTip.Active = false;
            this.istoolTipShowing = false;
            if (XMPS.Instance.PlcStatus == "LogIn")
            {
                if (sender is Control control)
                {
                    string HSFBName = control.Controls[0].Text;
                    List<HSIOFunctionBlock> hsioBlocks = XMPS.Instance.LoadedProject.HsioBlock.Where(hsio => hsio.HSIOFunctionBlockName == HSFBName).SelectMany(hsio => hsio.HSIOBlocks).ToList();
                    HSIO hSIO = new HSIO();
                    hSIO.HSIOFunctionBlockName = HSFBName;
                    hSIO.HSIOFunctionBlockType = HSFBName.Contains("I") ? "Input" : "Output";
                    hSIO.HSIOBlocks = hsioBlocks;

                    XMProForm tempForm = new XMProForm();

                    tempForm.StartPosition = FormStartPosition.CenterParent;
                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;

                    ForceFunctionBlock tempForceControl = new ForceFunctionBlock(hSIO);
                    tempForm.Text = "Force functionality";
                    tempForm.Height = tempForceControl.maxheight + 100;
                    tempForm.Width = tempForceControl.Width + tempForm.DesktopBounds.Width - tempForm.DisplayRectangle.Width;

                    tempForm.Controls.Add(tempForceControl);
                    var frmTemp = this.ParentForm as frmMain;
                    DialogResult result = tempForm.ShowDialog(frmTemp);
                    if (result == DialogResult.OK || result == DialogResult.Cancel)
                    {
                        OnlineMonitoringHelper.HoldOnlineMonitor = false;
                    }
                }
            }

        }

        private void MainGroupBoxMouseClikck(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HSIOToolTip.Active = false;
            this.istoolTipShowing = false;
            labelText.Active = false;

        }

        private void SqureMouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HSToolTipTimer.Stop();
            HSIOToolTip.RemoveAll();
            HSIOToolTip.Active = false;
            this.istoolTipShowing = false;
            labelText.Active = false;
            if (XMPS.Instance.PlcStatus == "LogIn")
            {
                if (sender is Panel panelHSIO)
                {
                    Control control = panelHSIO.Controls[0];
                    this.CurrentOMHSFb = control.Text;
                    HSIOsOnlineMonitoringHelpers(control.Text);
                }
            }
        }

        private void MainGroupBoxMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HSIOToolTip.Active = false;
            this.istoolTipShowing = false;
            labelText.Active = false;
        }
        private void GetHSIOToolTipText(Control control)
        {
            List<HSIOFunctionBlock> hsioBlocks = XMPS.Instance.LoadedProject.HsioBlock.Where(hsio => hsio.HSIOFunctionBlockName == control.Text).SelectMany(hsio => hsio.HSIOBlocks).ToList();

            string hsioToolTipText = control.Text + "\n";
            foreach (HSIOFunctionBlock hSIO in hsioBlocks)
            {
                string LogicalAdd = XMProValidator.GetTheAddressFromTag(hSIO.Value);
                string tagName = XMProValidator.GetTheTagnameFromAddress(hSIO.Value);
                string toolTipTagName = tagName != null ? tagName : hSIO.Value;
                hsioToolTipText += $"{hSIO.Text}:\t{hSIO.DataType}:\t{toolTipTagName}:\t{LogicalAdd}\n";
            }
            HSIOToolTip.Show(hsioToolTipText, control);
        }

        private void Hyperlinkclicked(object sender, EventArgs e)
        {
            if (XMPS.Instance.PlcStatus != "LogIn")
            {
                LinkLabel label = new LinkLabel();
                label = (LinkLabel)sender;
                Control parentControl = label.Parent;
                string ParentName = parentControl.Text;
                string SelectedDataType = "";
                string PrevAddress = "";
                string SelectedIoType = "";
                string InputtextName = "";
                if (sender is LinkLabel clickedLabel)
                {
                    CustomLinkLabel customLabel = customLinkLabels.FirstOrDefault(cl => cl.LinkLabel == clickedLabel);

                    if (customLabel != null)
                    {
                        PrevAddress = customLabel.LinkLabel.Text;
                        int index = customLabel.Index;
                        string DataType = customLabel.DataType;
                        string Type = customLabel.Type;
                        SelectedDataType = DataType;
                        SelectedIoType = customLabel.Type;
                        InputtextName = customLabel.Text;
                    }
                }
                this.CurrentHSIOFunctionBlock = ParentName;
                this.CureentInputTextName = InputtextName;
                string LinkLabelText = ShowPopUP(SelectedDataType, PrevAddress, ParentName);
                label.Text = LinkLabelText.StartsWith("~") ? "~" + LinkLabelText.Replace("~", "") : LinkLabelText;

                string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\HSIODesign.xml");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                XmlNode root = xmlDoc.SelectSingleNode("/Root");
                XmlNodeList itemNodes = root.SelectNodes("HSIOFunctionBlockDraw");
                SaveHSIOFunctionBlock(itemNodes);
            }
        }

        public string ShowPopUP(string dataType, string curtext, string HSIOName)
        {
            curtext = curtext == "???" ? "" : curtext;
            HSIOAutoComplete hSIOAutoComplete = new HSIOAutoComplete(dataType, curtext, HSIOName, CurrentHSIOFunctionBlock, CureentInputTextName);
            DialogResult result = hSIOAutoComplete.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (hSIOAutoComplete.EnteredText != null)
                    return hSIOAutoComplete.EnteredText;
                else
                    return "???";
            }
            else
            {
                return curtext == "" ? "???" : curtext;
            }
        }
        public void SaveHSIOFunctionBlock(XmlNodeList itemNodes)
        {
            List<HSIO> hSIOs = new List<HSIO>();
            foreach (XmlNode node in itemNodes)
            {
                HSIO hsio = new HSIO();
                hsio.HSIOFunctionBlockName = node.SelectSingleNode("HSIOFunctionBlockName").InnerText;
                hsio.HSIOFunctionBlockType = node.SelectSingleNode("HSIOFunctionBlockType").InnerText;
                List<HSIOFunctionBlock> hSIOFunctions = new List<HSIOFunctionBlock>();
                XmlNode hsiodrawingsNode = node.SelectSingleNode("HSIODrawings");
                List<LinkLabel> linkLabels = GetInputAndOutputData(hsio.HSIOFunctionBlockName, hsio.HSIOFunctionBlockType);
                int i = 0;
                foreach (XmlNode hsiodrawingNode in hsiodrawingsNode)
                {
                    HSIOFunctionBlock hSIOFunction = new HSIOFunctionBlock();
                    hSIOFunction.Type = hsiodrawingNode.SelectSingleNode("Type").InnerText;
                    hSIOFunction.Text = hsiodrawingNode.SelectSingleNode("Text").InnerText;
                    hSIOFunction.DataType = hsiodrawingNode.SelectSingleNode("DataType").InnerText;
                    //storing the Logical Address into HSIO function block.
                    string input = linkLabels[i].Text.Split('[')[0].Replace(" ", "");
                    bool containsAlphabetic = Regex.IsMatch(input, "[a-zA-Z]");
                    if (!linkLabels[i].Text.Split('[')[0].Replace(" ", "").StartsWith("???") && containsAlphabetic)
                    {
                        string logicalAddress = XMProValidator.GetTheAddressFromTag(linkLabels[i].Text.Split('[')[0].Replace(" ", ""));
                        hSIOFunction.Value = logicalAddress;
                    }
                    else
                    {
                        hSIOFunction.Value = linkLabels[i].Text.Split('[')[0].Replace(" ", "");
                    }
                    hSIOFunctions.Add(hSIOFunction);
                    i = i + 1;
                }
                hsio.HSIOBlocks = hSIOFunctions;
                hSIOs.Add(hsio);
                XMPS.Instance.LoadedProject.HsioBlock.RemoveAll(T => T.HSIOFunctionBlockName == hsio.HSIOFunctionBlockName);
                XMPS.Instance.LoadedProject.HsioBlock.Add(hsio);
            }
        }

        private List<LinkLabel> GetInputAndOutputData(string hSIOFunctionBlockName, string hSIOFunctionBlockType)
        {
            string tabPageName = hSIOFunctionBlockType == "Output" ? "tabOutput" : hSIOFunctionBlockType;
            TabPage tabPage = Tabcntrl.TabPages[tabPageName];

            string panelName = hSIOFunctionBlockType == "Input" ? "inputMainPanel" : "outputMainPanel";
            Panel panel = tabPage.Controls.Find(panelName, true).FirstOrDefault() as System.Windows.Forms.Panel;

            if (panel != null)
            {
                string targetGroupBoxName = hSIOFunctionBlockName;

                foreach (Control control in panel.Controls)
                {
                    if (control is System.Windows.Forms.GroupBox groupBox && groupBox.Text == targetGroupBoxName)
                    {
                        string groupBoxText = groupBox.Text;
                        List<LinkLabel> linkLabels = groupBox.Controls.OfType<LinkLabel>().ToList();

                        return linkLabels;
                    }
                }
            }
            return null;
        }

        private void comboBoxSelectInLogicBlock1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddInterruptBlkInMainLogicalBlk();
        }

        private void AddInterruptBlkInMainLogicalBlk()
        {
            firstBoxInterruptBlock = comboBoxSelectInLogicBlock1.Text;
            secondBoxInterruptBlock = comboBoxSelectInLogicBlock2.Text;
            thirdBoxInterruptBlock = comboBoxSelectInLogicBlock3.Text;
            fourthBoxInterruptBlock = comboBoxSelectInLogicBlock4.Text;
            if (XMPS.Instance.CurrentScreen.Contains("HSIOConfigForm"))
            {
                XMPS.Instance.LoadedProject.FirstInterruptBlock = comboBoxSelectInLogicBlock1.Text;
                XMPS.Instance.LoadedProject.SecondInterruptBlock = comboBoxSelectInLogicBlock2.Text;
                XMPS.Instance.LoadedProject.ThirdInterruptBlock = comboBoxSelectInLogicBlock3.Text;
                XMPS.Instance.LoadedProject.FourthInterruptBlock = comboBoxSelectInLogicBlock4.Text;
            }
        }

        private void comboBoxSelectInLogicBlock2_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddInterruptBlkInMainLogicalBlk();
        }

        public void HSIOsOnlineMonitoringHelpers(string HSIOFunctionName)
        {
            if (HSIOFunctionName != "")
            {
                if (XMPS.Instance.CurrentScreen.ToString().Contains("HSIOConfig"))
                {
                    List<string> _systemtagsAddress = new List<string>();
                    List<string> _ListTagName = new List<string>();
                    List<AddressDataTypes> _Type = new List<AddressDataTypes>();
                    OnlineMonitoringHelper omh = OnlineMonitoringHelper.Instance;
                    Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddressInfo = new Dictionary<string, Tuple<string, AddressDataTypes>>();

                    List<string> modeTagName = new List<string>();
                    List<string> directionTagName = new List<string>();
                    HSIO hSIO = XMPS.Instance.LoadedProject.HsioBlock.Where(hsio => hsio.HSIOFunctionBlockName == HSIOFunctionName).FirstOrDefault();
                    directionTagName.Add(hSIO.HSIOFunctionBlockType == "Input" ? hSIO.HSIOBlocks[9].Value.Replace("~", "") : "");
                    modeTagName.Add(hSIO.HSIOFunctionBlockType == "Input" ? hSIO.HSIOBlocks[10].Value : "");
                    foreach (HSIOFunctionBlock hSIOFunction in hSIO.HSIOBlocks)
                    {
                        if (hSIOFunction.Value != "???")
                        {
                            string LogicalAdd = XMProValidator.GetTheAddressFromTag(hSIOFunction.Value.Replace("~", ""));
                            if (!_systemtagsAddress.Contains(LogicalAdd) && LogicalAdd.Contains(":"))
                            {
                                string tagName = "";
                                string tagNameofLogicalAdd = XMProValidator.GetTheTagnameFromAddress(hSIOFunction.Value.Replace("~", ""));
                                bool showLogicalAddress = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == tagNameofLogicalAdd).Select(T => T.ShowLogicalAddress).FirstOrDefault();
                                if (showLogicalAddress)
                                {
                                    tagName = hSIOFunction.Value.Replace("~", "");
                                }
                                else
                                {
                                    tagName = tagNameofLogicalAdd != null ? tagNameofLogicalAdd : hSIOFunction.Value.Replace("~", "");
                                }
                                _systemtagsAddress.Add(LogicalAdd);
                                _ListTagName.Add(tagName.Replace("~", ""));
                                _Type.Add(omh.GetAddressTypeOf(hSIOFunction.DataType));
                            }
                        }
                    }

                    OldmodeTagName = modeTagName;
                    OlddirectionTagName = directionTagName;
                    _OldListTagName = _ListTagName;
                    for (int j = 0; j < _systemtagsAddress.Count; j++)
                    {
                        CurBlockAddressInfo.Add(_ListTagName[j], Tuple.Create(_systemtagsAddress[j], _Type[j]));
                    }
                    OldCurBlockAddressInfo = CurBlockAddressInfo;
                    _AddressValues = new Dictionary<string, string>();
                    foreach (string AddressValue in _OldListTagName)
                    { _AddressValues.Add(AddressValue, ""); }

                    bool isPingOk = XMProValidator.CheckPing();
                    if (!isPingOk)
                    {
                        ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain")).DisableOnlineModeFromHSIO();
                        return;
                    }
                    OnlineMonitoring onlineMonitoring = OnlineMonitoring.GetInstance();
                    onlineMonitoring.GetValues(_OldListTagName, ref OldCurBlockAddressInfo, ref _AddressValues, out string Result);
                   
                    string selectedTagPageName = Tabcntrl.SelectedTab.Text;
                    string tabPageName = selectedTagPageName.Contains("Output") ? "tabOutput" : "Input";
                    TabPage tabPage = Tabcntrl.TabPages[tabPageName];

                    string panelName = selectedTagPageName.Contains("Input") ? "inputMainPanel" : "outputMainPanel";
                    Panel panel = tabPage.Controls.Find(panelName, true).FirstOrDefault() as System.Windows.Forms.Panel;

                    if (panel != null)
                    {
                        foreach (Control GroupBoxes in panel.Controls)
                        {
                            if (GroupBoxes.Text.Equals(this.CurrentOMHSFb))
                            {
                                GroupBoxes.BackColor = Color.WhiteSmoke;
                                if (GroupBoxes is System.Windows.Forms.GroupBox groupBox)
                                {
                                    foreach (Control cntrls in GroupBoxes.Controls)
                                    {
                                        if (cntrls is LinkLabel)
                                        {
                                            if (cntrls.Text != "???")
                                            {
                                                //changing the BackGround color of link Lable
                                                cntrls.ForeColor = Color.Green;
                                                bool containsF = cntrls.Text.EndsWith("F");
                                                if (containsF)
                                                    cntrls.Text.Replace("F", "");
                                                cntrls.ForeColor = Color.Black;
                                                bool isNegationPresent = cntrls.Text.StartsWith("~") ? true : false;
                                                cntrls.Text = cntrls.Text.Split('[')[0].Replace(" ", "");
                                                if (cntrls.Text.StartsWith("~"))
                                                {
                                                    cntrls.Text = cntrls.Text.Replace("~", "");
                                                }
                                                string LogicalAdd = XMProValidator.GetTheAddressFromTag(cntrls.Text.Split('[')[0].Replace(" ", ""));
                                                //Checking for the Numeric Entry
                                                XMIOConfig tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == LogicalAdd).FirstOrDefault();
                                                if (LogicalAdd.Contains(":"))
                                                {
                                                    string value = string.Empty;
                                                    if (tag.ShowLogicalAddress == true && cntrls.Text == LogicalAdd && isNegationPresent)
                                                    {
                                                        _AddressValues.TryGetValue(tag.Tag, out value);
                                                    }
                                                    else
                                                    {
                                                        _AddressValues.TryGetValue(cntrls.Text.Split('[')[0].Replace(" ", ""), out value);
                                                    }
                                                    cntrls.Text = cntrls.Text.Split('[')[0];
                                                    //Check for the Showing Logical Address Tags
                                                    string tagName = XMProValidator.GetTheTagnameFromAddress(cntrls.Text);
                                                    bool isForced = XMPS.Instance.Forcedvalues.Contains(tagName != null ? tagName : cntrls.Text);
                                                    if (!isNegationPresent)
                                                    {
                                                        bool isdirectionTag = OlddirectionTagName.Contains(LogicalAdd);
                                                        if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "1" && !isdirectionTag)
                                                            cntrls.Text += isForced ? " [ " + "■" + " ]" + " F " : " [ " + "■" + " ]";
                                                        else if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "0" && !isdirectionTag)
                                                            cntrls.Text += isForced ? " [ " + "□" + " ]" + " F " : " [ " + "□" + " ]";
                                                        else if (isdirectionTag && cntrls.TabIndex != 10)
                                                        {
                                                            if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "1")
                                                                cntrls.Text += isForced ? " [ " + "■" + " ]" + " F " : " [ " + "■" + " ]";
                                                            else if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "0")
                                                                cntrls.Text += isForced ? " [ " + "□" + " ]" + " F " : " [ " + "□" + " ]";
                                                        }
                                                        else if (isdirectionTag && cntrls.TabIndex == 10)
                                                        {
                                                            if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "1")
                                                                cntrls.Text += isForced ? " [ " + "TRUE" + " ]" + " F" : " [ " + "TRUE" + " ]";
                                                            else if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "0")
                                                                cntrls.Text += isForced ? " [ " + "FALSE" + " ]" + " F" : " [ " + "FALSE" + " ]";
                                                        }
                                                        else
                                                        {
                                                            if (cntrls.Text != "???" || cntrls.Text != "")
                                                            {
                                                                //Check for the Showing Logical Address Tags
                                                                string logicalAddress = XMProValidator.GetTheAddressFromTag(cntrls.Text);
                                                                if (OldmodeTagName.Contains(logicalAddress) && cntrls.TabIndex == 11)
                                                                {
                                                                    string modeOutputText = GetModeOutputName(value);
                                                                    cntrls.Text += isForced ? " [ " + modeOutputText + " ]" + " F" : " [ " + modeOutputText + " ]";
                                                                }
                                                                else if((tabPageName == "Input" && cntrls.TabIndex == 17) || (tabPageName == "tabOutput" && cntrls.TabIndex == 16)) 
                                                                {
                                                                    string errorCodeName = GetErrorCodeName(value);
                                                                    cntrls.Text += isForced ? " [ " + errorCodeName + " ]" + " F" : " [ " + errorCodeName + " ]";
                                                                }
                                                                else
                                                                {
                                                                    cntrls.Text += isForced ? " [ " + value + " ]" + " F" : " [ " + value + " ]";
                                                                }
                                                            }
                                                            else
                                                                cntrls.Text += isForced ? " [ " + value + " ]" + " F" : " [ " + value + " ]";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        // Checking for Negation address 
                                                        bool isdirectionTag = false;
                                                        if (tag.ShowLogicalAddress == true && cntrls.Text == LogicalAdd && isNegationPresent)
                                                        {
                                                            isdirectionTag = OlddirectionTagName.Contains(tag.Tag);
                                                        }
                                                        else
                                                        {
                                                            isdirectionTag = OlddirectionTagName.Contains(cntrls.Text);
                                                        }
                                                        if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "1" && !isdirectionTag && cntrls.TabIndex == 10)
                                                            cntrls.Text = isForced ? "~" + cntrls.Text + " [ " + "□" + " ]" + " F" : "~" + cntrls.Text + " [ " + "□" + " ]";
                                                        else if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "0" && !isdirectionTag && cntrls.TabIndex == 10)
                                                            cntrls.Text = isForced ? "~" + cntrls.Text + " [ " + "■" + " ]" + " F" : "~" + cntrls.Text + " [ " + "■" + " ]";
                                                        else if (isdirectionTag && cntrls.TabIndex == 10)
                                                        {
                                                            if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "0")
                                                                cntrls.Text = isForced ? "~" + cntrls.Text + " [ " + "TRUE" + " ]" + " F" : "~" + cntrls.Text + " [ " + "TRUE" + " ]";
                                                            else if ((LogicalAdd.StartsWith("F") || LogicalAdd.StartsWith("I") || LogicalAdd.StartsWith("Q")) && value == "1")
                                                                cntrls.Text = isForced ? "~" + cntrls.Text + " [ " + "FALSE" + " ]" + " F" : "~" + cntrls.Text + " [ " + "FALSE" + " ]";
                                                        }
                                                        else
                                                        {
                                                            if (value == "1")
                                                                cntrls.Text = isForced ? "~" + cntrls.Text + " [ " + "□" + " ]" + " F" : "~" + cntrls.Text + " [ " + "□" + " ]";
                                                            else
                                                                cntrls.Text = isForced ? "~" + cntrls.Text + " [ " + "■" + " ]" + " F" : "~" + cntrls.Text + " [ " + "■" + " ]";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    string checkingDirection = GroupBoxes.Controls[10].Text;
                                                    string checkingMode = GroupBoxes.Controls[11].Text;
                                                    if (checkingMode == cntrls.Text && cntrls.TabIndex == 11 && tabPageName == "Input")
                                                    {
                                                        if (cntrls.Text != "???" || cntrls.Text != "")
                                                        {
                                                            if (OldmodeTagName.Contains(cntrls.Text))
                                                            {
                                                                string modeOutputText = GetModeOutputName(checkingMode);
                                                                cntrls.Text += " [ " + modeOutputText + " ]";
                                                            }
                                                        }
                                                    }
                                                    else if (checkingDirection == cntrls.Text && cntrls.TabIndex == 10 && tabPageName == "Input")
                                                    {
                                                        if (cntrls.Text != "???" || cntrls.Text != "")
                                                        {
                                                            if (checkingDirection == "0")
                                                                cntrls.Text += " [ " + "FALSE" + " ]";
                                                            else if (checkingDirection == "1")
                                                                cntrls.Text += " [ " + "TRUE" + " ]";
                                                        }
                                                    }
                                                    else if ((tabPageName == "Input" && cntrls.TabIndex == 17) || (tabPageName == "tabOutput" && cntrls.TabIndex == 16))
                                                    {
                                                        string errorCodeName = GetErrorCodeName(cntrls.Text);
                                                        cntrls.Text += " [ " + errorCodeName + " ]";
                                                    }
                                                    else
                                                    {
                                                        //taking both HSI and HSO bit addresses indexes.
                                                        HashSet<int> bitIndex = (tabPageName == "Input")
                                                            ? new HashSet<int>() { 1, 2, 3, 4, 5, 13, 14, 15, 16 }
                                                            : new HashSet<int>() { 1, 3, 4, 5, 14, 16 };

                                                        if (bitIndex.Contains(cntrls.TabIndex))
                                                        {
                                                            cntrls.Text += cntrls.Text == "0" ? " [ □ ]" : cntrls.Text == "1" ? " [ ■ ]" : "";
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                cntrls.Text = cntrls.Text.Split('[')[0].Replace(" ", "");
                                                //cntrls.Text += " [ " + " ]";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                HSIOsOfflineMonitoring();
            }
        }

        private string GetModeOutputName(string value)
        {
            if (value == "1")
                return "Up";
            else if (value == "2")
                return "Down";
            else if (value == "3")
                return "Up/Down";
            else if (value == "4")
                return "Quadrature 2X";
            else if (value == "5")
                return "Quadrature 4X";
            else
                return value;

        }
        private string GetErrorCodeName(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            string errocode = string.Empty;
            ErrorCodes.TryGetValue(Convert.ToInt32(value), out errocode);
            return !string.IsNullOrEmpty(errocode) ? errocode : value;
        }

        private void HSIOsOMtimer_Tick(object sender, EventArgs e)
        {
            if (!OnlineMonitoringStatus.isOnlineMonitoring && !string.IsNullOrEmpty(CurrentOMHSFb))
                this.CurrentOMHSFb = "";

            if (XMPS.Instance.PlcStatus == "LogIn" && OnlineMonitoringStatus.isOnlineMonitoring)
            {
                HSIOsOnlineMonitoringHelpers(CurrentOMHSFb);
            }
            else if(!OnlineMonitoringStatus.isOnlineMonitoring && CurrentOMHSFb != "" && XMPS.Instance.PlcStatus != "LogOut")
            {
                this.CurrentOMHSFb = "";
                ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain")).DisableOnlineModeFromHSIO();
            }
            else
            {
                this.CurrentOMHSFb = "";
                HSIOsOfflineMonitoring();
            }

            if (XMPS.Instance.PlcStatus == "LogIn")
                CheckSystemTagStatus();
        }
        private void CheckSystemTagStatus()
        {
            List<string> _ListTagName = new List<string>();       // Tagname
            Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddressInfo = new Dictionary<string, Tuple<string, AddressDataTypes>>();
            Dictionary<string, string> _AddressValues = new Dictionary<string, string>();

            ///Send the status address in every cycle to check status of CPU
            string tagName = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == XMPS.Instance.LoadedProject.PLCStatusTag).Select(t => t.Tag).FirstOrDefault();
            if (tagName != null)
            {
                _ListTagName.Add(tagName);
                CurBlockAddressInfo[tagName] = Tuple.Create(XMPS.Instance.LoadedProject.PLCStatusTag, AddressDataTypes.WORD);
            }
            ///Send the error status address in every cycle to check status of CPU
            foreach (string errorTagAddress in XMPS.Instance.LoadedProject.ErrorStatusTags)
            {
                XMIOConfig errTagDtl = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == errorTagAddress).FirstOrDefault();
                if (Enum.GetNames(typeof(AddressDataTypes)).Any(name => name.Equals(errTagDtl.Label, StringComparison.OrdinalIgnoreCase)))
                {
                    _ListTagName.Add(errTagDtl.Tag);
                    CurBlockAddressInfo[errTagDtl.Tag] = Tuple.Create(errTagDtl.LogicalAddress, (AddressDataTypes)Enum.Parse(typeof(AddressDataTypes), errTagDtl.Label, true));
                }
            }
            bool isPingOk = XMProValidator.CheckPing();
            if (!isPingOk)
            {
                ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain")).DisableOnlineModeFromHSIO();
                return;
            }
            OnlineMonitoring onlineMonitoring = OnlineMonitoring.GetInstance();
            onlineMonitoring.GetValues(_ListTagName, ref CurBlockAddressInfo, ref _AddressValues, out string Result);
            frmMain fm = Application.OpenForms.OfType<frmMain>().FirstOrDefault();
            if (fm != null && tagName != null)
            {
                _AddressValues.TryGetValue(tagName, out string value);
                List<Tuple<string, string>> tplErrorValues = new List<Tuple<string, string>>();
                ///Send the error status address in every cycle to check status of CPU
                foreach (string errorTagAddress in XMPS.Instance.LoadedProject.ErrorStatusTags)
                {
                    XMIOConfig errTagDtl = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == errorTagAddress).FirstOrDefault();
                    if (Enum.GetNames(typeof(AddressDataTypes)).Any(name => name.Equals(errTagDtl.Label, StringComparison.OrdinalIgnoreCase)))
                    {
                        string errvalue = OnlineMonitoringStatus.AddressValues.ContainsKey(errTagDtl.Tag.ToString()) ? OnlineMonitoringStatus.AddressValues[errTagDtl.Tag.ToString()]
                                                          : OnlineMonitoringStatus.AddressValues[errorTagAddress];
                        tplErrorValues.Add(Tuple.Create(errorTagAddress, errvalue));
                    }
                }

                fm.CheckPLCStatus(value, tplErrorValues);
            }

        }
        private void Tabcntrl_TabIndexChanged(object sender, EventArgs e)
        {
            this.CurrentOMHSFb = "";
        }

        private void comboBoxSelectInLogicBlock3_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddInterruptBlkInMainLogicalBlk();
        }

        private void comboBoxSelectInLogicBlock4_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddInterruptBlkInMainLogicalBlk();
        }

        private void HSToolTipTimer_Tick(object sender, EventArgs e)
        {
            if (!istoolTipShowing)
            {
                GetHSIOToolTipText(CurrentFBForToolTip);
                istoolTipShowing = true;
            }
        }
    }
}
