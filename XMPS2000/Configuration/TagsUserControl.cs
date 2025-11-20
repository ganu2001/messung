using iTextSharp.text.pdf;
using LadderDrawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using XMPS2000.Bacnet;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Types;
using XMPS2000.LadderLogic;
using XMPS2000.Resource_File;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMPS2000.Configuration
{
    public partial class TagsUserControl : UserControl
    {
        private XMIOConfig dataSource;
        private bool isEdited = false;
        public string TagText { set; get; }
        public string LogicalAddressForModbus { get; set; }

        private string OnBordExpansionIOTagName;

        XMPS xm;
        string RetentiveAddress = "";
        //For the HSIO Mode Selection
        List<string> InputModes = new List<string>();
        List<string> OutputModes = new List<string>();

        string AutoLogicalAddress = "";
        //New var for set Retentive
        public TagsUserControl(bool isCheckboxCheckedDefault, string LogicalAddress, string SelectedDataType = "")
        {
            InitializeComponent();
            xm = XMPS.Instance;
            comboBoxMode.DataSource = Mode.List;
            dataSource = new XMIOConfig();
            DataBind(LogicalAddress, SelectedDataType);
            this.lblModel.Text = LabelNames.PLCModel;
            this.lblLabel.Text = LabelNames.Label;
            this.lblLogicalAddress.Text = LabelNames.Logical_Address;
            this.lblTag.Text = LabelNames.Tag;
            this.lblIOList.Text = LabelNames.IOList;
            this.lblType.Text = LabelNames.Type;
            this.lblInitialValue.Text = LabelNames.InitialValue;
            this.chkIsRetentive.Text = LabelNames.IsRetentive;
            this.lblMode.Text = LabelNames.Mode;
            comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || T.ID == 12).ToList();
            DataBind(LogicalAddress, SelectedDataType);
            CrossCheckDataType(SelectedDataType);
            DataBind(LogicalAddress, SelectedDataType);
            //this.textBoxLabel.Enabled = false;
            this.textBoxLogicalAddress.Enabled = false;
            if (isCheckboxCheckedDefault)
            {
                chkIsRetentive.Checked = true;
                chkIsRetentive.Enabled = false;
            }
            else
            {
                chkIsRetentive.Checked = false;

            }
            //To select retentive checkbox if the arrival tag is retentive 
            //Adding Model Name at the time of create Tags form Logical Blocks.
            textBoxLogicalAddress.Enabled = false;
            string currentScreenName = xm.CurrentScreen.Split('#')[0];
            if (currentScreenName.Equals("LadderForm"))
                this.textBoxModel.Text = "User Defined Tags";
            ShowingEnableFilterControls(false);
            if (!xm.LoadedProject.PlcModel.Equals("XBLD-14E") && !xm.LoadedProject.PlcModel.Equals("XBLD-17E"))
                textBoxTag.MaxLength = 20;
            else
                textBoxTag.MaxLength = 25;
        }
        public TagsUserControl()
        {
            InitializeComponent();
        }
        public TagsUserControl(string LogicalAddress, string SelectedDataType = "")
        {
            InitializeComponent();
            xm = XMPS.Instance;
            comboBoxMode.DataSource = Mode.List;
            dataSource = new XMIOConfig();
            if (SelectedDataType.EndsWith("Logic"))
            {

                string actualUdfbName = SelectedDataType.Replace("Logic", "Tags");
                DataBind(LogicalAddress);
                this.lblModel.Text = LabelNames.PLCModel;
                this.lblLabel.Text = LabelNames.Label;
                this.lblLogicalAddress.Text = LabelNames.Logical_Address;
                this.lblTag.Text = LabelNames.Tag;
                this.lblIOList.Text = LabelNames.IOList;
                this.lblType.Text = LabelNames.Type;
                this.lblInitialValue.Text = LabelNames.InitialValue;
                this.chkIsRetentive.Text = LabelNames.IsRetentive;
                this.lblMode.Text = LabelNames.Mode;
                comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || (T.ID == 12)).ToList();
                this.textBoxModel.Text = actualUdfbName;
                //AutoLogicalAddress = XMProValidator.GetNextAddress(SelectedDataType);
                //this.textBoxLogicalAddress.Text = AutoLogicalAddress;
            }
            else
            {
                DataBind(LogicalAddress, SelectedDataType);
                this.lblModel.Text = LabelNames.PLCModel;
                this.lblLabel.Text = LabelNames.Label;
                this.lblLogicalAddress.Text = LabelNames.Logical_Address;
                this.lblTag.Text = LabelNames.Tag;
                this.lblIOList.Text = LabelNames.IOList;
                this.lblType.Text = LabelNames.Type;
                this.lblInitialValue.Text = LabelNames.InitialValue;
                this.chkIsRetentive.Text = LabelNames.IsRetentive;
                this.lblMode.Text = LabelNames.Mode;
                comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || (T.ID == 12)).ToList();
                DataBind(LogicalAddress, SelectedDataType);
                CrossCheckDataType(SelectedDataType);
            }
            ShowingEnableFilterControls(false);
            if (!xm.LoadedProject.PlcModel.Equals("XBLD-14E") && !xm.LoadedProject.PlcModel.Equals("XBLD-17E"))
                textBoxTag.MaxLength = 20;
            else
                textBoxTag.MaxLength = 25;

        }
        public TagsUserControl(string TagName, XMIOConfig CopyTag)
        {
            InitializeComponent();
            xm = XMPS.Instance;
            comboBoxMode.DataSource = Mode.List;
            dataSource = new XMIOConfig();
            DataBind(TagName);
            LblNames();
            this.textBoxModel.Text = CopyTag.Model.ToString();
            this.textBoxLabel.Text = CopyTag.Label;
            this.textBoxLogicalAddress.Text = CopyTag.LogicalAddress.ToString();
            this.textBoxTag.Text = CopyTag.Tag.ToString();
            this.textBoxInitialValue.Text = CopyTag.InitialValue.ToString();
            this.chkIsRetentive.Checked = CopyTag.Retentive ? true : false;
            this.ChkShowLogicalAddress.Checked = CopyTag.ShowLogicalAddress ? true : false;
            comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || (T.ID == 12)).ToList();
            DataType dataType1 = DataType.List.Where(T => T.Text.Equals(CopyTag.Label)).FirstOrDefault();
            int index1 = comboBoxIOType.Items.IndexOf(dataType1);
            this.comboBoxIOType.SelectedIndex = index1;
            int index = -1;
            foreach (DataType dataType in DataType.List)
            {
                if (dataType.ToString() == CopyTag.Label)
                {
                    index = DataType.List.IndexOf(dataType);
                    break;
                }
            }
            if (index >= 0 && index < comboBoxIOType.Items.Count)
            {
                comboBoxIOType.SelectedIndex = index;
            }
            ShowingEnableFilterControls(false);
            if (!xm.LoadedProject.PlcModel.Equals("XBLD-14E") && !xm.LoadedProject.PlcModel.Equals("XBLD-17E"))
                textBoxTag.MaxLength = 20;
            else
                textBoxTag.MaxLength = 25;
        }

        public TagsUserControl(int value, string TagAddress)
        {
            string SelectedDataType = "Bool";
            InitializeComponent();
            xm = XMPS.Instance;
            comboBoxMode.DataSource = Mode.List;
            dataSource = new XMIOConfig();
            DataBind("$");
            LblNames();
            this.textBoxLogicalAddress.Text = TagAddress;
            comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || (T.ID == 12)).ToList();
            if (TagAddress.StartsWith("P5"))
                comboBoxIOType.Text = "Real";
            else if (TagAddress.StartsWith("F2"))
                comboBoxIOType.Text = "Bool";
            else if (TagAddress.StartsWith("W4"))
                comboBoxIOType.Text = "Word";
            CrossCheckDataType(SelectedDataType);
            this.textBoxLogicalAddress.Text = TagAddress;
            this.textBoxModel.Text = "User Defined Tags";
            ShowingEnableFilterControls(false);
            if (!xm.LoadedProject.PlcModel.Equals("XBLD-14E") && !xm.LoadedProject.PlcModel.Equals("XBLD-17E"))
                textBoxTag.MaxLength = 20;
            else
                textBoxTag.MaxLength = 25;
        }
        public TagsUserControl(string TagName)
        {
            string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\HSIOMode.xml");
            // Create a new XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode root = xmlDoc.DocumentElement;
            XmlNodeList itemNodes = root.SelectNodes("Item");

            foreach (XmlNode itemNode in itemNodes)
            {
                // Access the <Key> and <Value> elements within each <Item> element
                XmlNode keyNode = itemNode.SelectSingleNode("Key");
                XmlNode valueNode = itemNode.SelectSingleNode("Value");

                if (keyNode != null && valueNode != null)
                {
                    string key = keyNode.InnerText;
                    string value = valueNode.InnerText;
                    if (key == "Input")
                    {
                        InputModes.Add(value);
                    }
                    else if (key == "Output")
                    {
                        OutputModes.Add(value);
                    }
                }
            }

            string SelectedDataType = "Bool";
            InitializeComponent();
            xm = XMPS.Instance;
            comboBoxMode.DataSource = Mode.List;
            dataSource = new XMIOConfig();
            DataBind(TagName);

            this.lblModel.Text = LabelNames.PLCModel;
            this.lblLabel.Text = LabelNames.Label;
            this.lblLogicalAddress.Text = LabelNames.Logical_Address;
            this.lblTag.Text = LabelNames.Tag;
            this.lblIOList.Text = LabelNames.IOList;
            this.lblType.Text = LabelNames.Type;
            this.lblInitialValue.Text = LabelNames.InitialValue;
            this.chkIsRetentive.Text = LabelNames.IsRetentive;
            this.ChkShowLogicalAddress.Text = LabelNames.IsBtnShowLogical;
            this.lblMode.Text = LabelNames.Mode;
            comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || T.ID == 12).ToList();
            DataBind(TagName);

            //Retantive tag present in retentive Function block is not editable
            XMIOConfig tempTagsList = xm.LoadedProject.Tags.Where(r => r.Tag == TagName).FirstOrDefault();
            List<LadderElement> isthere = tempTagsList != null ? XMProValidator.GetRTONTimerRungs(tempTagsList.LogicalAddress) : new List<LadderElement>();
            bool retentiveTick = false;
            if (chkIsRetentive.Checked == true)
            {
                if (isthere != null && isthere.Count > 0)
                {
                    retentiveTick = true;
                }
            }

            chkIsRetentive.Enabled = retentiveTick ? false : true;
            CrossCheckDataType(SelectedDataType);


            this.textBoxTag.Enabled = (dataSource.LogicalAddress != null && dataSource.LogicalAddress.StartsWith("S3")) ? false : true;
            this.chkIsRetentive.Enabled = (dataSource.LogicalAddress != null && dataSource.LogicalAddress.StartsWith("S3")) ? false : true;
            this.ChkShowLogicalAddress.Enabled = (dataSource.LogicalAddress != null && dataSource.LogicalAddress.StartsWith("S3")) ? false : true;
            if (tempTagsList != null)
                if (tempTagsList.Model != "" && (tempTagsList.Type.Equals(XMPS2000.Core.Types.IOType.DigitalInput) || tempTagsList.Type.Equals(XMPS2000.Core.Types.IOType.AnalogInput)))
                {
                    chkIsRetentive.Enabled = false;
                }
            isEdited = true;
            //Adding Model Name for the Tags 
            string currentScreenName = xm.CurrentScreen.Split('#')[0];
            if (currentScreenName.Equals("LadderForm") || currentScreenName.Equals("MainForm"))
                this.textBoxModel.Text = "User Defined Tags";
            OnBordExpansionIOTagName = TagName;
            if (!xm.LoadedProject.PlcModel.Equals("XBLD-14E") && !xm.LoadedProject.PlcModel.Equals("XBLD-17E"))
                textBoxTag.MaxLength = 20;
            else
                textBoxTag.MaxLength = 25;
        }

        public TagsUserControl(string TagName, string Datatype, int valueconst)
        {
            InitializeComponent();
            xm = XMPS.Instance;
            comboBoxMode.DataSource = Mode.List;
            dataSource = new XMIOConfig();

            DataBind(TagName);

            this.lblModel.Text = LabelNames.PLCModel;
            this.lblLabel.Text = LabelNames.Label;
            this.lblLogicalAddress.Text = LabelNames.Logical_Address;
            this.lblTag.Text = LabelNames.Tag;
            this.lblIOList.Text = LabelNames.IOList;
            this.lblType.Text = LabelNames.Type;
            this.lblInitialValue.Text = LabelNames.InitialValue;
            this.chkIsRetentive.Text = LabelNames.IsRetentive;
            this.ChkShowLogicalAddress.Text = LabelNames.IsBtnShowLogical;
            this.lblMode.Text = LabelNames.Mode;
            comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || T.ID > 11).ToList();
            comboBoxIOType.Text = Datatype;
            DataBind(TagName);
            comboBoxIOType.Text = Datatype;


            CrossCheckDataType(Datatype);
            comboBoxIOType.Text = Datatype;
            textBoxModel.Text = "User Defined Tags";
            ShowingEnableFilterControls(false);
            if (!xm.LoadedProject.PlcModel.Equals("XBLD-14E") && !xm.LoadedProject.PlcModel.Equals("XBLD-17E"))
                textBoxTag.MaxLength = 20;
            else
                textBoxTag.MaxLength = 25;
        }
        private void LblNames()
        {
            this.lblModel.Text = LabelNames.PLCModel;
            this.lblLabel.Text = LabelNames.Label;
            this.lblLogicalAddress.Text = LabelNames.Logical_Address;
            this.lblTag.Text = LabelNames.Tag;
            this.lblIOList.Text = LabelNames.IOList;
            this.lblType.Text = LabelNames.Type;
            this.lblInitialValue.Text = LabelNames.InitialValue;
            this.chkIsRetentive.Text = LabelNames.IsRetentive;
            this.ChkShowLogicalAddress.Text = LabelNames.IsBtnShowLogical;
            this.lblMode.Text = LabelNames.Mode;
        }
        public TagsUserControl(string LogicalAddress, string TagName, string UDFBName, string dataType)
        {
            string SelectedDataType = dataType;
            this.Tag = UDFBName;
            InitializeComponent();
            if (UDFBName == "User Defined Tags" || UDFBName == "Multistate Value" || UDFBName == "Binary Value" || UDFBName == "Analog Value")
            {
                this.autoaddcheckbox.Visible = true;
                this.lblNumberOfTag.Visible = true;
                this.TagCount.Visible = true;
            }
            xm = XMPS.Instance;
            comboBoxMode.DataSource = Mode.List;
            dataSource = new XMIOConfig();
            if (LogicalAddress == "")
            {
                DataBind(TagName);
            }
            else
            {
                DataBind(LogicalAddress, SelectedDataType);
            }
            this.lblModel.Text = LabelNames.PLCModel;
            this.lblLabel.Text = LabelNames.Label;
            this.lblLogicalAddress.Text = LabelNames.Logical_Address;
            this.lblTag.Text = LabelNames.Tag;
            this.lblIOList.Text = LabelNames.IOList;
            this.lblType.Text = LabelNames.Type;
            this.lblInitialValue.Text = LabelNames.InitialValue;
            this.chkIsRetentive.Text = LabelNames.IsRetentive;
            this.ChkShowLogicalAddress.Text = LabelNames.IsBtnShowLogical;
            this.lblMode.Text = LabelNames.Mode;
            comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || (T.ID == 12)).ToList();
            if (LogicalAddress == "")
            {
                DataBind(TagName);
            }
            else
            {
                DataBind(LogicalAddress, SelectedDataType);
            }
            CrossCheckDataType(SelectedDataType);
            //adding additional check for the for checking(C7 || T6) address replace by W4
            string instructionType = string.Empty;
            if (LogicalAddress.StartsWith("C7"))
                instructionType = "Counter";
            else if (LogicalAddress.StartsWith("T6"))
                instructionType = "Timer";

            AutoLogicalAddress = XMProValidator.GetNextAddress(this.textBoxLabel.Text, instructionType);
            this.textBoxLogicalAddress.Text = (AutoLogicalAddress == null || AutoLogicalAddress == "") ? LogicalAddress : AutoLogicalAddress;
            if (UDFBName != "")
            {
                if (!UDFBName.EndsWith("Tags"))
                {
                    this.comboBoxIOType.Enabled = false;
                }
                textBoxModel.Text = UDFBName.EndsWith("Tags") ? UDFBName : "User Defined Tags";
            }
            this.textBoxTag.TabIndex = 0;
            comboBoxIOType.Text = dataType;
            ShowingEnableFilterControls(false);
            if (!xm.LoadedProject.PlcModel.Equals("XBLD-14E") && !xm.LoadedProject.PlcModel.Equals("XBLD-17E"))
                textBoxTag.MaxLength = 20;
            else
                textBoxTag.MaxLength = 25;
        }

        private void CrossCheckDataType(string SelectedDataType)
        {
            string DT = comboBoxIOType.Text.ToString();
            if (DT == "" || DT == "Bool")
            {
                if (textBoxLogicalAddress.Text.StartsWith("F2") || textBoxLogicalAddress.Text.Contains(".") || SelectedDataType == "Bool")        // Need to Check -->
                {
                    comboBoxIOType.Text = "Bool";
                }
                else if (textBoxLogicalAddress.Text.StartsWith("P5"))
                {
                    comboBoxIOType.Text = "Real";
                }
                else if (textBoxLogicalAddress.Text.StartsWith("W4") && (SelectedDataType == "DINT" || SelectedDataType == "UDINT"))
                {
                    comboBoxIOType.Text = SelectedDataType;
                }
                else
                {
                    comboBoxIOType.Text = "Word";
                }
            }
            else if (comboBoxIOType.Text.ToString() != "")
            {
                comboBoxIOType.Text = DT;
            }

            if ((((textBoxLogicalAddress.Text.StartsWith("F2") || textBoxLogicalAddress.Text.Contains(".")) && comboBoxIOType.Text != "Bit") || textBoxLogicalAddress.Text == "") && SelectedDataType == null)
            {
                comboBoxIOType.Text = "Bool";
            }
            else if (SelectedDataType == "Bool" && !(DT == "" || DT == "Bool"))
            {
                comboBoxIOType.Text = DT;
                textBoxLabel.Text = DT;
            }
            else
            {
                bool fbDataType = SelectedDataType == "TON" || SelectedDataType == "TOFF" || SelectedDataType == "RTON" || SelectedDataType == "TP" || SelectedDataType == "CTU" || SelectedDataType == "CTD";
                if ((textBoxLabel.Text.StartsWith("UDI") || textBoxLabel.Text.StartsWith("DI") || textBoxLabel.Text.StartsWith("DO") || textBoxLabel.Text.StartsWith("AI") || textBoxLabel.Text.StartsWith("AO")) && !fbDataType)
                {
                    comboBoxIOType.Text = comboBoxIOType.Text;
                    textBoxLabel.Text = textBoxLabel.Text;
                }
            }

        }

        private void DataBind(string LogicalAddress, string Datatype)
        {
            var tag = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == LogicalAddress).FirstOrDefault();
            if (tag != null)
            {
                this.dataSource = tag;//xm.LoadedProject.Tags[tag.];
                this.textBoxModel.Text = dataSource.Model;

                this.textBoxLabel.Text = dataSource.Label;
                if (textBoxModel.Text.ToString() == "")
                {
                    comboBoxIOType.Text = dataSource.Label;
                }
                this.textBoxLogicalAddress.Text = dataSource.LogicalAddress;
                this.textBoxTag.Text = dataSource.Tag;
                this.textBoxIOList.Text = dataSource.IoList.ToString();
                this.textBoxIOList.Tag = dataSource.Key.ToString();
                this.textBoxType.Text = dataSource.Type.ToString();
                this.comboBoxMode.Text = String.IsNullOrEmpty(dataSource.Mode) ? "" : dataSource.Mode.ToString();
                this.textBoxInitialValue.Text = String.IsNullOrEmpty(dataSource.InitialValue) ? "" : dataSource.InitialValue.ToString();
                if (this.textBoxLogicalAddress.Text.StartsWith("I"))
                {
                    chkIsRetentive.Enabled = false;
                    textBoxInitialValue.Enabled = false;
                }
                else
                {
                    chkIsRetentive.Enabled = true;
                    textBoxInitialValue.Enabled = true;
                }

                this.chkIsRetentive.Checked = dataSource.Retentive;
                if (dataSource.Retentive) RetentiveAddress = dataSource.RetentiveAddress;
                if (dataSource.Type.ToString().Contains("Analog"))
                {
                    comboBoxMode.Visible = true;
                    lblMode.Visible = true;
                }
                else
                {
                    comboBoxMode.Visible = false;
                    lblMode.Visible = false;
                }
            }
            else if (LogicalAddress.Length > 0)
            {
                this.textBoxLogicalAddress.Text = LogicalAddress;
                this.textBoxLogicalAddress.Enabled = true;
                this.textBoxIOList.Text = IOListType.NIL.ToString();
                this.textBoxType.Text = IOType.DataType.ToString();
                comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || T.ID == 12).ToList();
                comboBoxIOType.Text = Datatype;
                comboBoxIOType.Visible = true;
                if (Datatype != "") comboBoxIOType.Enabled = false;
                if (this.textBoxLogicalAddress.Text.StartsWith("I"))
                {
                    chkIsRetentive.Enabled = false;
                    textBoxInitialValue.Enabled = false;
                }
                else
                {
                    chkIsRetentive.Enabled = true;
                    textBoxInitialValue.Enabled = true;
                }
                if (textBoxType.ToString().Contains("Analog"))
                {
                    comboBoxMode.Visible = true;
                    lblMode.Visible = true;
                }
                else
                {
                    comboBoxMode.Visible = false;
                    lblMode.Visible = false;
                }
                this.textBoxLogicalAddress.Text = LogicalAddress;

            }
            else
            {
                this.comboBoxIOType.Visible = true;
                chkIsRetentive.Enabled = true;
                textBoxInitialValue.Enabled = true;
                comboBoxMode.Visible = false;
                lblMode.Visible = false;
                this.textBoxLogicalAddress.Enabled = true;
            }

        }
        public void BindResistancetable()
        {
            int resistanceIndex = ModeUI.List.FindIndex(m => m.Text == "Resistance");
            int pt100Index = ModeUI.List.FindIndex(m => m.Text == "PT100");

            if (resistanceIndex >= 0 && pt100Index > resistanceIndex)
            {
                ModeUI.List.RemoveRange(resistanceIndex + 1, pt100Index - resistanceIndex - 1);

                var resistanceTables = XMPS.Instance.LoadedProject.ResistanceTables;

                if (resistanceTables != null && resistanceTables.Count > 0)
                {
                    int insertIndex = resistanceIndex + 1;
                    int idCounter = 100;

                    foreach (var table in resistanceTables)
                    {
                        ModeUI.List.Insert(insertIndex++, new ModeUI
                        {
                            ID = (idCounter++).ToString(),
                            Text = table.Name
                        });
                    }
                }
            }
            comboBoxMode.DataSource = null;
            comboBoxMode.DataSource = ModeUI.List;
        }
        private void DataBind(string Tag)
        {
            var tag = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.Tag == Tag && !(d.LogicalAddress.StartsWith("'"))).FirstOrDefault();
            if (tag != null)
            {
                this.dataSource = tag;
                this.textBoxModel.Text = dataSource.Model;

                this.textBoxLabel.Text = dataSource.Label;

                if (textBoxModel.Text.ToString() == "")
                {
                    comboBoxIOType.Text = dataSource.Label;
                }
                this.textBoxLogicalAddress.Text = dataSource.LogicalAddress;
                this.textBoxTag.Text = dataSource.Tag;
                this.textBoxIOList.Text = dataSource.IoList.ToString();
                this.textBoxIOList.Tag = dataSource.Key.ToString();
                this.textBoxType.Text = dataSource.Type.ToString();
                this.comboBoxMode.Text = String.IsNullOrEmpty(dataSource.Mode) ? "" : dataSource.Mode.ToString();
                this.textBoxInitialValue.Text = String.IsNullOrEmpty(dataSource.InitialValue) ? "" : dataSource.InitialValue.ToString();

                if (this.textBoxLogicalAddress.Text.StartsWith("I"))
                {
                    chkIsRetentive.Enabled = false;
                    textBoxInitialValue.Enabled = false;
                }
                else
                {
                    chkIsRetentive.Enabled = true;
                    textBoxInitialValue.Enabled = true;

                }
                this.chkIsRetentive.Checked = dataSource.Retentive;
                this.ChkShowLogicalAddress.Checked = dataSource.ShowLogicalAddress;

                if (dataSource.Retentive) RetentiveAddress = dataSource.RetentiveAddress;
                if (textBoxIOList.Text != "RemoteIO" && (dataSource.Type.ToString().Contains("Analog") || dataSource.Type.ToString().Contains("Universal")))
                {
                    comboBoxMode.Visible = true;
                    lblMode.Visible = true;
                }
                else if ((dataSource.Model == "XM-14-DT-HIO" || dataSource.Model == "XM-14-DT-HIO-E") && dataSource.IoList == Core.Types.IOListType.OnBoardIO)
                {

                    if (tag.Label == "DI0" || tag.Label == "DI1" || tag.Label == "DI2" || tag.Label == "DI3" || tag.Label == "DI4" || tag.Label == "DI5" || tag.Label == "DI6" || tag.Label == "DI7" || tag.Label == "DO0" || tag.Label == "DO1")
                    {
                        comboBoxMode.Visible = true;
                        lblMode.Visible = true;
                    }
                    else
                    {
                        comboBoxMode.Visible = false;
                        lblMode.Visible = false;
                        checkBoxEnableInputFilter.Visible = false;
                        textBoxInFilValue.Visible = false;
                        labelDigitalFilter.Visible = false;
                        return;
                    }
                    string[] parts = dataSource.LogicalAddress.Split('.');
                    int parsedValue = int.Parse(parts[1]);
                    if (dataSource.Type == Core.Types.IOType.DigitalInput)
                    {
                        if (tag.Label == "DI5" || tag.Label == "DI7")
                        {
                            comboBoxMode.DataSource = new List<string> { "Digital Input" };
                            comboBoxMode.DropDownStyle = ComboBoxStyle.DropDownList;
                            lblMode.Visible = true;
                            checkBoxEnableInputFilter.Visible = true;
                            textBoxInFilValue.Visible = true;
                            labelDigitalFilter.Visible = true;
                            checkBoxEnableInputFilter.Checked = dataSource.IsEnableInputFilter;
                            textBoxInFilValue.Text = string.IsNullOrEmpty(dataSource.InpuFilterValue) ? "10" : dataSource.InpuFilterValue;
                            textBoxInFilValue.Enabled = dataSource.IsEnableInputFilter;
                            return;
                        }
                        if (parsedValue % 2 == 0 || parts[1] == "00")
                        {
                            if (parsedValue == 4 || parsedValue == 6)
                            {
                                InputModes.Clear();
                                InputModes.Add("Digital Input");
                                InputModes.Add("Interrupt");
                                comboBoxMode.DataSource = InputModes;
                            }
                            else
                            {
                                if (parsedValue == 2)
                                {
                                    InputModes.Remove("Interrupt");
                                }
                                comboBoxMode.DataSource = InputModes;
                            }
                        }
                        else if (parsedValue % 2 != 0)
                        {
                            if (dataSource.Mode.Contains("B Count"))
                            {
                                if (parsedValue > 0)
                                {
                                    int addressOfPrevTag = int.Parse(parts[1]) - 1;
                                    string PrevAddLogicalAdd = $"{parts[0]}.{addressOfPrevTag:00}";
                                    XMIOConfig PrevTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == PrevAddLogicalAdd).FirstOrDefault();
                                    if (PrevTag.Mode.Contains("Count"))
                                    {
                                        comboBoxMode.DropDownStyle = ComboBoxStyle.DropDownList;
                                        comboBoxMode.DataSource = new List<string> { dataSource.Mode };
                                    }
                                    else
                                    {
                                        comboBoxMode.DataSource = new List<string> { InputModes[0], InputModes[1], InputModes[5], InputModes[6] };
                                        lblMode.Visible = true;
                                    }
                                }

                            }
                            else
                            {
                                if (parsedValue == 3)
                                {
                                    comboBoxMode.DataSource = new List<string> { InputModes[0], InputModes[1], InputModes[5], InputModes[6] };
                                    this.comboBoxMode.Text = String.IsNullOrEmpty(dataSource.Mode) ? "" : dataSource.Mode.ToString();
                                }
                                else
                                {
                                    comboBoxMode.DataSource = new List<string> { InputModes[0], InputModes[1], InputModes[6] };
                                    this.comboBoxMode.Text = String.IsNullOrEmpty(dataSource.Mode) ? "" : dataSource.Mode.ToString();
                                }
                                lblMode.Visible = true;
                            }
                        }
                        else
                        {
                            //for OutPut Tags
                            comboBoxMode.DataSource = new List<string>
                                            {
                                                InputModes.First(),
                                                InputModes.ElementAt(1),
                                                InputModes.Last()
                                            };
                        }
                    }
                    else
                    {
                        comboBoxMode.DataSource = OutputModes;
                    }
                    lblMode.Visible = true;
                }
                else
                {
                    comboBoxMode.Visible = false;
                    lblMode.Visible = false;
                }


            }
            else if (Tag.Length == 1 && Tag == "$")
            {
                this.comboBoxIOType.Visible = true;
                comboBoxIOType.Enabled = true;
                chkIsRetentive.Enabled = true;
                ChkShowLogicalAddress.Enabled = true;
                textBoxInitialValue.Enabled = true;
                comboBoxMode.Visible = false;
                lblMode.Visible = false;
                this.textBoxLogicalAddress.Enabled = true;
            }
            else if (Tag.Length > 0)
            {
                this.textBoxTag.Text = Tag;
                this.textBoxIOList.Text = IOListType.NIL.ToString();
                this.textBoxType.Text = IOType.DataType.ToString();
                comboBoxIOType.DataSource = DataType.List.Where(T => T.ID < 6 || (T.ID > 11 && T.ID < 14)).ToList();
                comboBoxIOType.Text = "Bool";
                comboBoxIOType.Visible = true;
                comboBoxIOType.Enabled = false;
                comboBoxMode.Visible = false;
                lblMode.Visible = false;
                this.textBoxLogicalAddress.Enabled = true;
            }

            else
            {
                this.comboBoxIOType.Visible = true;
                comboBoxIOType.Text = "Bool";
                comboBoxIOType.Enabled = false;
                chkIsRetentive.Enabled = true;
                ChkShowLogicalAddress.Enabled = true;
                textBoxInitialValue.Enabled = true;
                comboBoxMode.Visible = false;
                lblMode.Visible = false;
                this.textBoxLogicalAddress.Enabled = true;
            }
            if (textBoxType.Text.Contains("UniversalInput"))
            {
                BindResistancetable();
            }

            else if (textBoxType.Text.Contains("UniversalOutput"))
            {
                comboBoxMode.DataSource = ModeUO.List;
            }
            ///For all default tags disable Tagname, Retentive check and Show Logical Address check

            this.textBoxTag.Enabled = (dataSource.LogicalAddress != null && dataSource.LogicalAddress.StartsWith("S3")) ? false : true;


            this.chkIsRetentive.Enabled = (dataSource.LogicalAddress != null && dataSource.LogicalAddress.StartsWith("S3")) ? false : true;
            this.ChkShowLogicalAddress.Enabled = (dataSource.LogicalAddress != null && dataSource.LogicalAddress.StartsWith("S3")) ? false : true;
            ////for Digital Filter 
            if (xm.LoadedProject.IsEditableDigitalFilter)
            {
                bool isDigitalInput = dataSource.Type == IOType.DigitalInput;
                bool isUniversalInput = dataSource.Type == IOType.UniversalInput;
                bool isExpansionIO = dataSource.IoList == IOListType.ExpansionIO;
                bool isOnBoardOrExpansion = dataSource.IoList == IOListType.OnBoardIO || isExpansionIO;
                bool isModeDigitalInput = dataSource.Mode != null && dataSource.Mode.Equals("Digital Input");
                bool isModeStartsWithDigital = dataSource.Mode != null && dataSource.Mode.ToString().StartsWith("Digital");
                bool condition =
                    (isDigitalInput && isOnBoardOrExpansion && dataSource.Model != "XM-14-DT-HIO") ||
                    (isDigitalInput && dataSource.Model == "XM-14-DT-HIO" && isModeDigitalInput) ||
                    (isUniversalInput && dataSource.IoList == IOListType.ExpansionIO && isModeStartsWithDigital);

                if (condition && dataSource.LogicalAddress != null)
                {
                    ShowingEnableFilterControls(true);
                    textBoxInFilValue.Enabled = dataSource.IsEnableInputFilter;
                    textBoxInFilValue.Text = !string.IsNullOrEmpty(dataSource.InpuFilterValue) ? dataSource.InpuFilterValue
                                             : "10";
                    checkBoxEnableInputFilter.Checked = dataSource.IsEnableInputFilter;
                }
                else
                {
                    ShowingEnableFilterControls(false);
                }
            }
            else
            {
                ShowingEnableFilterControls(false);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int numericCount = 0;
            int addedaddress = 0;
            string origionalTagNm = "";
            string acttagname = xm.LoadedProject.Tags.Where(d => d.LogicalAddress == textBoxLogicalAddress.Text.ToString()).Select(t => t.Tag).FirstOrDefault();
            if (autoaddcheckbox.Checked && TagCount.Value > 1)
            {
                AddMultipleTagsFast(
                    Convert.ToInt32(TagCount.Value),
                    textBoxTag.Text,
                    textBoxLogicalAddress.Text
                );
                return;
            }
            if (comboBoxIOType.Text == "Multistate Value")
            {
                this.Tag = "Multistate Value"; // Explicitly mark as multistate
            }
            if (autoaddcheckbox.Checked == true && TagCount.Value > 0)
                numericCount = Convert.ToInt32(TagCount.Value) - 1;
            autoadd:
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.textBoxLogicalAddress.TextLength < 1)
            {
                MessageBox.Show("Enter Proper Logical Address", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!string.IsNullOrEmpty(textBoxTag.Text) && textBoxTag.Text.Equals("NULL"))
            {
                MessageBox.Show("NULL as a Tag Name not allow please select alternate", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.textBoxTag.Text.ToString() == "" || this.textBoxTag.Text.ToString() == "???")                                                                                       //If TagText is Empty then 
            {
                MessageBox.Show("Add Tag Name First Before Proceeding Further", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateTextBoxInput(textBoxTag))
            {
                MessageBox.Show("Please correct the errors before saving.", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //checking tag name text is used as a any udfb text input or output.
            var matchingUDFBInfo = xm.LoadedProject.UDFBInfo.FirstOrDefault(t => t.UDFBlocks.Any(d => d.Text.Equals(this.textBoxTag.Text)));

            if (matchingUDFBInfo != null)
            {
                MessageBox.Show($"Tag name used in inputs or outputs for {matchingUDFBInfo.UDFBName} UDFB", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var Logicname = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.Tag == this.textBoxTag.Text && d.LogicalAddress != this.textBoxLogicalAddress.Text).FirstOrDefault();
            if (Logicname != null && !(Logicname.LogicalAddress.StartsWith("'")))
            {
                MessageBox.Show("Tag name is already used for Logical Address " + Logicname.LogicalAddress, "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (BacNetFormFactory.ValidateObjectName(this.textBoxTag.Text, "Tag"))
            {
                MessageBox.Show("Tag name is already used, change the name and try again ...", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var LogicId = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == this.textBoxLogicalAddress.Text).FirstOrDefault();
            if (LogicId != null && comboBoxIOType.Visible == true)
            {
                MessageBox.Show("Logical Address is already added", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataSource == null)
            {
                this.dataSource = new XMIOConfig();
            }
            dataSource.Model = this.textBoxModel.Text;
            dataSource.Label = this.textBoxLabel.Text;

            dataSource.LogicalAddress = this.textBoxLogicalAddress.Text;

            if (dataSource.LogicalAddress.StartsWith("W"))
            {
                string doubleWordAddress = this.textBoxLogicalAddress.Text;
                string[] parts = doubleWordAddress.Split(':');

                int secondPart = int.Parse(parts[1]);
                int previousSecondPart = secondPart - 1;
                int nextSecondPart = secondPart + 1;

                string previousAddress = $"{parts[0]}:{previousSecondPart:000}";
                string nextAddress = $"{parts[0]}:{nextSecondPart:000}";
                var previousAddressTag = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == previousAddress && (d.Label.Equals("Double Word") || d.Label.Equals("DINT") || d.Label.Equals("UDINT"))).FirstOrDefault();
                var nextAddressTag = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == nextAddress).FirstOrDefault();

                if (previousAddressTag != null)
                {
                    MessageBox.Show("Logical Address is already added", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (nextAddressTag != null)
                {
                    if (dataSource.Label.Equals("Double Word") || dataSource.Label.Equals("DINT") || dataSource.Label.Equals("UDINT"))
                    {
                        MessageBox.Show("Logical Address is already added", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
            dataSource.Tag = this.textBoxTag.Text.Trim();
            if (this.textBoxIOList.Tag == null)
            {
                this.textBoxIOList.Tag = (xm.LoadedProject.Tags.Max(R => R.Key) + 1).ToString();
            }
            dataSource.Key = Convert.ToInt32(this.textBoxIOList.Tag.ToString());
            if (this.textBoxIOList.Text == "")
            {
                dataSource.IoList = IOListType.NIL;
                dataSource.Type = IOType.DataType;
            }
            else
            {
                dataSource.IoList = (IOListType)Enum.Parse(typeof(IOListType), this.textBoxIOList.Text);
                dataSource.Type = (IOType)Enum.Parse(typeof(IOType), this.textBoxType.Text);
            }
            dataSource.InitialValue = this.textBoxInitialValue.Text.ToString();
            dataSource.Retentive = chkIsRetentive.Checked;
            dataSource.ShowLogicalAddress = ChkShowLogicalAddress.Checked;
            //saving Digital Input Data
            if (xm.LoadedProject.IsEditableDigitalFilter)
            {
                if ((dataSource.Type == IOType.DigitalInput && !comboBoxMode.SelectedValue.Equals("Digital Input") && dataSource.Model == "XM-14-DT-HIO")
                || (dataSource.Type == IOType.UniversalInput && !comboBoxMode.SelectedValue.ToString().StartsWith("Digital")))
                {
                    dataSource.IsEnableInputFilter = false;
                    dataSource.InpuFilterValue = string.Empty;
                }
                else
                {
                    dataSource.IsEnableInputFilter = checkBoxEnableInputFilter.Checked;
                    dataSource.InpuFilterValue = (dataSource.Type == IOType.DigitalInput || dataSource.Type == IOType.UniversalInput) ? textBoxInFilValue.Text : string.Empty;
                }
            }
            ///for DigitalInput (Enable Filter)
            if (dataSource.IoList == IOListType.ExpansionIO && dataSource.Type == IOType.DigitalInput && xm.LoadedProject.IsEditableDigitalFilter)
            {
                ChangeInputFilterInfo(dataSource);
            }
            //for UniversalInput(Enable Filter)
            if (dataSource.IoList == IOListType.ExpansionIO && dataSource.Type == IOType.UniversalInput && xm.LoadedProject.IsEditableDigitalFilter)
            {
                ChangeUniversalInputFilterInfo(dataSource);
            }
            //checking if current tag having childs calibration tags if then update tag name also of that tag
            if ((dataSource.IoList == IOListType.ExpansionIO || dataSource.IoList == IOListType.OnBoardIO)
                && (dataSource.Type.ToString().StartsWith("Universal") || dataSource.Type.ToString().StartsWith("Analog")) && xm.LoadedProject.DiagnosticParametersEnabled)
            {
                ChangeTagNameOfCalibrationChilds(dataSource);
            }

            if (chkIsRetentive.Checked && RetentiveAddress == "")
            {
                dataSource.RetentiveAddress = CommonFunctions.GetRetentiveAddress(this.textBoxLogicalAddress.Text, textBoxLabel.Text);
                if (dataSource.RetentiveAddress.StartsWith("Retentive Address Going Beyond Range"))
                {
                    dataSource.Retentive = false;
                    if (dataSource.RetentiveAddress.StartsWith("Retentive Address Going Beyond Range, Can't Add New Address "))
                    {
                        dataSource.Retentive = false;
                        dataSource.RetentiveAddress = string.Empty;
                    }
                    MessageBox.Show("Retentive Address Going Beyond Range, Can't Add New Address", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else if (chkIsRetentive.Checked && RetentiveAddress != "")
            {
                dataSource.RetentiveAddress = RetentiveAddress;
            }
            else
            {
                dataSource.RetentiveAddress = "";
            }
            if (comboBoxMode.Visible)
            {
                dataSource.Mode = comboBoxMode.Text.ToString();
                ChangeTheModeForCalibrationRequest(dataSource.Mode);
            }
            else
            {
                dataSource.Mode = "";
            }
            TagText = textBoxTag.Text;
            int tagIndex = xm.LoadedProject.Tags.IndexOf(dataSource);
            xm.LoadedProject.Tags.RemoveAll(d => d.LogicalAddress == dataSource.LogicalAddress);
            if (tagIndex != -1)
                xm.LoadedProject.Tags.Insert(tagIndex, dataSource);                                                                     //Tagwindow Undo
            else
                xm.LoadedProject.Tags.Add(dataSource);
            xm.LoadedProject.NewAddedTagIndex = dataSource.Model.Equals("User Defined Tags") ?
                                                 xm.LoadedProject.Tags.Where(T => T.Model == "User Defined Tags").Count() - 1 :
                                                 xm.LoadedProject.Tags.Where(T => T.Model == dataSource.Model).Count() - 1;
            this.LogicalAddressForModbus = this.textBoxLogicalAddress.Text;
            CheckAndValidateMain(acttagname);
            origionalTagNm = addedaddress == 0 ? this.textBoxTag.Text : origionalTagNm;
            ////<Check If Mode of Tags Changed>
            ModeChangedOfTag(dataSource.LogicalAddress, dataSource.Mode);
            CommonFunctions.UpdateTagNames(dataSource.LogicalAddress, dataSource.Tag);
            if (xm.LoadedProject.PlcModel != null && xm.LoadedProject.PlcModel.StartsWith("XBLD")) BacNetTagFactory.AddTagtoBacNetObject(this.textBoxTag.Text.ToString(), this.textBoxLogicalAddress.Text.ToString(), this.textBoxLabel.Text.ToString(), dataSource.Type, comboBoxMode.Text.ToString(), this.Tag?.ToString() == "Multistate Value");
            frmMain.UndoTags.Push(dataSource);
            if (numericCount > 0)
            {
                addedaddress++;
            NextAdd:
                string nextAddress = (comboBoxIOType.Text.Equals("Double Word") || comboBoxIOType.Text.Equals("DINT")) ? $"{this.textBoxLogicalAddress.Text.Split(':')[0]}:{int.Parse(this.textBoxLogicalAddress.Text.Split(':')[1]) + 2:000}" :
                                                                    $"{this.textBoxLogicalAddress.Text.Split(':')[0]}:{int.Parse(this.textBoxLogicalAddress.Text.Split(':')[1]) + 1:000}";
                var duplicate = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == nextAddress).FirstOrDefault();
                this.textBoxLogicalAddress.Text = nextAddress;
                string newTagName = origionalTagNm + "_" + addedaddress;
                this.textBoxTag.Text = newTagName;
                if (duplicate != null)
                {
                    goto NextAdd;
                }
                this.textBoxIOList.Tag = null;
                dataSource = null;
                numericCount--;

                goto autoadd;
            }

            if (!dataSource.Retentive && RetentiveAddress != "")
            {
                CommonFunctions.UpdatePrecedingRetentiveAddresses(RetentiveAddress);
            }
            xm.MarkProjectModified(true);
            if (xm.CurrentScreen.ToString().Equals("TagsForm#User Defined Tags"))
            {
                if (isEdited)
                {
                    this.ParentForm.Close();
                    this.ParentForm.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.textBoxLogicalAddress.Clear();
                    this.textBoxTag.Clear();
                    this.textBoxIOList.Tag = null;
                    this.chkIsRetentive.Checked = false;
                    this.ChkShowLogicalAddress.Checked = false;
                    this.dataSource = null;
                    this.ParentForm.Close();
                    this.ParentForm.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                this.ParentForm.Close();
                this.ParentForm.DialogResult = DialogResult.OK;
            }

        }

        private void ChangeTagNameOfCalibrationChilds(XMIOConfig dataSource)
        {
            XMPS.Instance.LoadedProject.Tags.Where(t => t.Model == dataSource.Model
                                   && t.Label.Split('_')[0] == dataSource.Label &&
                                   (t.Label.EndsWith("_OR") || t.Label.EndsWith("_OL"))).ToList().ForEach(T =>
                                   {
                                       T.Tag = dataSource.Tag + "_" + T.Label.Split('_')[1];
                                   });
        }

        private void ChangeUniversalInputFilterInfo(XMIOConfig dataSource)
        {
            var relatedExTags = xm.LoadedProject.Tags.Where(t => t.Model != null && t.Model.Equals(dataSource.Model)
                                                            && t.Type == IOType.UniversalInput
                                                            && !string.IsNullOrEmpty(t.Mode) && t.Mode.Equals("Digital")
                                                            && !t.Label.Contains("_OR") && !t.Label.Contains("_OL")).ToList();
            if (comboBoxMode.SelectedValue.ToString().Equals("Digital"))
            {
                foreach (XMIOConfig tag in relatedExTags)
                {
                    tag.IsEnableInputFilter = checkBoxEnableInputFilter.Checked;
                    tag.InpuFilterValue = textBoxInFilValue.Text;
                }
            }
        }

        private void ChangeInputFilterInfo(XMIOConfig dataSource)
        {
            var relatedExTags = xm.LoadedProject.Tags.Where(t => t.Model != null && t.Model.Equals(dataSource.Model) && t.Type == IOType.DigitalInput).ToList();
            foreach (XMIOConfig tag in relatedExTags)
            {
                tag.IsEnableInputFilter = checkBoxEnableInputFilter.Checked;
                tag.InpuFilterValue = textBoxInFilValue.Text;
            }
        }


        private void ChangeTheModeForCalibrationRequest(string mode)
        {
            if (!XMPS.Instance.LoadedProject.DiagnosticParametersEnabled)
                return;

            if ((dataSource.Type == Core.Types.IOType.AnalogInput || dataSource.Type == Core.Types.IOType.AnalogOutput)
                || (dataSource.Type == Core.Types.IOType.UniversalInput || dataSource.Type == Core.Types.IOType.UniversalOutput))
            {
                var matchingTags = XMPS.Instance.LoadedProject.Tags.Where(t => t.Model == dataSource.Model
                                  && t.Label.Split('_')[0] == dataSource.Label).ToList();
                foreach (var tag in matchingTags)
                {
                    tag.Mode = dataSource.Mode;
                }
            }
        }

        private bool ValidateTextBoxInput(Control textBox)
        {
            string text = textBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                errorProvider.SetError(textBox, "Tag name cannot be empty.");
                return false;
            }
            if (char.IsDigit(text[0]))
            {
                errorProvider.SetError(textBox, "Tag name cannot start with a numeric value.");
                return false;
            }
            foreach (char ch in text)
            {
                if (!char.IsLetterOrDigit(ch) && ch != 95 && ch != 3 && ch != 22) // Allowed characters
                {
                    errorProvider.SetError(textBox, "Invalid character detected. Only letters, digits, and underscore (_) are allowed.");
                    return false;
                }
            }
            errorProvider.SetError(textBox, "");
            return true;
        }
        private void CheckAndValidateMain(string acttagname)
        {
            if (string.IsNullOrEmpty(acttagname))
                return;
            if (xm.LoadedProject.MainLadderLogic.Where(t => t.Contains(acttagname)).Count() > 0)
            {
                for (int i = 0; i < xm.LoadedProject.MainLadderLogic.Count(); i++)
                {
                    string newvalue = xm.LoadedProject.MainLadderLogic[i].Replace('(' + acttagname + ')', '(' + textBoxTag.Text.ToString() + ')');
                    xm.LoadedProject.MainLadderLogic[i] = newvalue;
                }

            }
        }
        private void AddMultipleTagsFast(int count, string baseTagName, string startingAddress)
        {
            try
            {
                string[] addressParts = startingAddress.Split(':');
                string prefix = addressParts[0];
                int startNum = int.Parse(addressParts[1]);

                int maxKey = xm.LoadedProject.Tags.Count > 0
                           ? xm.LoadedProject.Tags.Max(t => t.Key)
                           : 0;

                // Get the actual selected data type
                DataType selectedDataType = comboBoxIOType.SelectedItem as DataType;
                bool isMultistate = selectedDataType?.Text == "Multistate Value" || this.Tag?.ToString() == "Multistate Value";

                var existingAddresses = new HashSet<string>(xm.LoadedProject.Tags.Select(t => t.LogicalAddress));
                var newTags = new List<XMIOConfig>(count);
                int currentNum = startNum;
                for (int i = 0; i < count; i++)
                {
                    if (currentNum >= TagCount.Maximum)
                    {
                        continue;
                    }
                    string currentAddress = $"{prefix}:{currentNum:000}";
                    string currentTagName = i == 0 ? baseTagName : $"{baseTagName}_{i}";
                    int increment = (comboBoxIOType.Text.Equals("Double Word", StringComparison.OrdinalIgnoreCase) ||
                        comboBoxIOType.Text.Equals("DINT", StringComparison.OrdinalIgnoreCase)) ? 2 : 1;

                    if (existingAddresses.Contains(currentAddress)) continue;
                    if (!this.ValidateChildren())
                    {
                        MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (currentAddress.Length < 1)
                    {
                        MessageBox.Show("Enter Proper Logical Address", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (currentTagName.Length > textBoxTag.MaxLength)
                    {
                        MessageBox.Show("Check the legnth of Tagname, it should be less than " + Convert.ToInt32(textBoxTag.MaxLength + 1).ToString(), "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (currentTagName == "" || currentTagName == "???")
                    {
                        MessageBox.Show("Add Tag Name First Before Proceeding Further", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!string.IsNullOrEmpty(textBoxTag.Text) && textBoxTag.Text.Equals("NULL"))
                    {
                        MessageBox.Show("NULL as a Tag Name not allow please select alternate", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!ValidateTextBoxInput(textBoxTag))
                    {
                        MessageBox.Show("Please correct the errors before saving.", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (currentAddress.StartsWith("W"))
                    {
                        string doubleWordAddress = currentAddress;
                        string[] parts = doubleWordAddress.Split(':');

                        int secondPart = int.Parse(parts[1]);
                        int previousSecondPart = secondPart - 1;
                        int nextSecondPart = secondPart + 1;

                        string previousAddress = $"{parts[0]}:{previousSecondPart:000}";
                        string nextAddressCheck = $"{parts[0]}:{nextSecondPart:000}";
                        var previousAddressTag = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == previousAddress && (d.Label.Equals("Double Word") || d.Label.Equals("DINT") || d.Label.Equals("UDINT"))).FirstOrDefault();
                        var nextAddressTag = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == nextAddressCheck).FirstOrDefault();

                        if (previousAddressTag != null)
                        {
                            MessageBox.Show("Logical Address is already added", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (nextAddressTag != null)
                        {
                            if (dataSource.Label.Equals("Double Word") || dataSource.Label.Equals("DINT") || dataSource.Label.Equals("UDINT"))
                            {
                                MessageBox.Show("Logical Address is already added", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                    }
                    // Check if tag name is used in any UDFB
                    var matchingUDFBInfo = xm.LoadedProject.UDFBInfo.FirstOrDefault(t => t.UDFBlocks.Any(d => d.Text.Equals(currentTagName)));
                    if (matchingUDFBInfo != null)
                    {
                        MessageBox.Show($"Tag name used in inputs or outputs for {matchingUDFBInfo.UDFBName} UDFB", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if base tag name already exists
                    var existingTag = xm.LoadedProject.Tags.FirstOrDefault(d => d.Tag == currentTagName && !d.LogicalAddress.StartsWith("'"));
                    if (existingTag != null)
                    {
                        MessageBox.Show("Tag name is already used for Logical Address " + existingTag.LogicalAddress, "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (BacNetFormFactory.ValidateObjectName(currentTagName, "Tag"))
                    {
                        MessageBox.Show("Tag name is already used, change the name and try again ...", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var LogicId = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.LogicalAddress == currentAddress).FirstOrDefault();
                    if (LogicId != null && comboBoxIOType.Visible == true)
                    {
                        MessageBox.Show("Logical Address is already added", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    newTags.Add(new XMIOConfig
                    {
                        Tag = currentTagName,
                        LogicalAddress = currentAddress,
                        Key = maxKey + 1 + i,
                        Model = this.textBoxModel.Text,
                        //Label = isMultistate ? "Multistate Value" : selectedDataType.Text,
                        Label = this.textBoxLabel.Text,
                        IoList = IOListType.NIL,
                        Type = IOType.DataType,
                        InitialValue = this.textBoxInitialValue.Text,
                        Retentive = this.chkIsRetentive.Checked,
                        ShowLogicalAddress = this.ChkShowLogicalAddress.Checked,
                        Mode = this.comboBoxMode.Visible ? this.comboBoxMode.Text : ""
                    });
                    currentNum += increment;
                }
                int finalTagCount = 0;
                foreach (XMIOConfig xMIOConfig in newTags)
                {
                    string retentiveAddress = string.Empty;
                    if (chkIsRetentive.Checked && RetentiveAddress == "")
                    {
                        xMIOConfig.RetentiveAddress = CommonFunctions.GetRetentiveAddress(xMIOConfig.LogicalAddress, textBoxLabel.Text);
                        if (xMIOConfig.RetentiveAddress.StartsWith("Retentive Address Going Beyond Range"))
                        {
                            MessageBox.Show(xMIOConfig.RetentiveAddress, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    if (xm.LoadedProject.PlcModel != null && xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                    {
                        BacNetIP bacNetIP = xm.LoadedProject.BacNetIP ?? new BacNetIP();
                        string address = xMIOConfig.LogicalAddress;
                        string tagName = xMIOConfig.Tag;
                        if (address.StartsWith("F2"))
                        {
                            // Handle binary values (unchanged)
                            var exists = bacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress == address);
                            if (exists == null)
                            {
                                int countBinary = bacNetIP.BinaryIOValues
                                    .Where(t => t.ObjectType == "5:Binary Value")
                                    .Select(t => int.Parse(t.InstanceNumber))
                                    .DefaultIfEmpty(-1)
                                    .Max() + 1;

                                bacNetIP.BinaryIOValues.Add(new BinaryIOV(
                                    $"Binary Value:{countBinary}",
                                    countBinary.ToString(),
                                    "5:Binary Value",
                                    $"Binary Value:{countBinary}",
                                    tagName,
                                    address
                                ));
                            }
                            else
                            {
                                exists.ObjectName = tagName;
                            }
                        }
                        else if (address.StartsWith("W4"))
                        {
                            string label = this.textBoxLabel.Text;
                            if (!(label.Equals("Double Word", StringComparison.OrdinalIgnoreCase) ||
                                  label.Equals("DINT", StringComparison.OrdinalIgnoreCase) ||
                                  label.Equals("UDINT", StringComparison.OrdinalIgnoreCase) ||
                                  label.Equals("Byte", StringComparison.OrdinalIgnoreCase) ||
                                  label.Equals("INT", StringComparison.OrdinalIgnoreCase)))
                            {
                                var existingMultistate = bacNetIP.MultistateValues
                                    .FirstOrDefault(t => t.LogicalAddress == address);

                                if (existingMultistate == null)
                                {
                                    int countMulti = bacNetIP.MultistateValues
                                        .Where(t => t.ObjectType == "19:Multistate Value")
                                        .Select(t => int.Parse(t.InstanceNumber))
                                        .DefaultIfEmpty(-1)
                                        .Max() + 1;

                                    bacNetIP.MultistateValues.Add(new MultistateIOV(
                                        $"Multistate Value:{countMulti}",
                                        countMulti.ToString(),
                                        "19:Multistate Value",
                                        tagName,
                                        address,
                                        0
                                    ));
                                }
                                else
                                {
                                    existingMultistate.ObjectName = tagName;
                                }
                            }
                        }
                        else if (address.StartsWith("P5"))
                        {
                            // Handle P5 addresses (unchanged)
                            var exists = bacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress == address);
                            if (exists == null)
                            {
                                int countAnalog = bacNetIP.AnalogIOValues
                                    .Where(t => t.ObjectType == "2:Analog Value")
                                    .Select(t => int.Parse(t.InstanceNumber))
                                    .DefaultIfEmpty(-1)
                                    .Max() + 1;

                                bacNetIP.AnalogIOValues.Add(new AnalogIOV(
                                    $"Analog Value:{countAnalog}",
                                    countAnalog.ToString(),
                                    "2:Analog Value",
                                    $"Analog Value:{countAnalog}",
                                    tagName,
                                    address,
                                    false,
                                    "Analog"
                                ));
                            }
                            else
                            {
                                exists.ObjectName = tagName;
                            }
                        }
                    }
                    xm.LoadedProject.Tags.Add(xMIOConfig);
                    finalTagCount++;
                }
                xm.MarkProjectModified(true);

                this.ParentForm.DialogResult = DialogResult.OK;
                this.ParentForm.Close();

                MessageBox.Show($"{finalTagCount} tags have been successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void ModeChangedOfTag(string LogicalAddress, string mode)
        {
            if (mode == "") return;
            XMIOConfig tag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == LogicalAddress).FirstOrDefault();
            if (tag.Type != Core.Types.IOType.AnalogInput && tag.Type != Core.Types.IOType.AnalogOutput && tag.Type != Core.Types.IOType.UniversalInput && tag.Type != Core.Types.IOType.UniversalOutput)
            {
                string[] parts = LogicalAddress.Split('.');
                int AddressPartSecond = int.Parse(parts[1]) + 1;
                string NextAddLogicalAdd = $"{parts[0]}.{AddressPartSecond:00}";
                XMIOConfig Nexttag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == NextAddLogicalAdd).FirstOrDefault();
                if (int.Parse(parts[1]) % 2 == 0 || parts[1] == "00")
                {
                    if (mode == "Up/Down Direction")
                    {
                        tag.Mode = "Up/Down -A Count";
                        Nexttag.Mode = "Up/Down -B Count";
                        if (Nexttag.Type == IOType.DigitalInput)
                        {
                            Nexttag.InpuFilterValue = string.Empty;
                            Nexttag.IsEnableInputFilter = false;
                        }
                    }
                    else if (mode == "Quadrature 2x Encoder")
                    {
                        tag.Mode = "Quadrature 2x Encoder -A Count";
                        Nexttag.Mode = "Quadrature 2x Encoder -B Count";
                        if (Nexttag.Type == IOType.DigitalInput)
                        {
                            Nexttag.InpuFilterValue = string.Empty;
                            Nexttag.IsEnableInputFilter = false;
                        }
                    }
                    else if (mode == "Quadrature 4x Encoder")
                    {
                        tag.Mode = "Quadrature 4x Encoder -A Count";
                        Nexttag.Mode = "Quadrature 4x Encoder -B Count";
                        if (Nexttag.Type == IOType.DigitalInput)
                        {
                            Nexttag.InpuFilterValue = string.Empty;
                            Nexttag.IsEnableInputFilter = false;
                        }
                    }
                    else
                    {
                        if (tag.Label.Contains("DI"))
                        {
                            if (tag.Label.Equals("DI4") || tag.Label.Equals("DI6"))
                            {

                            }
                            else
                            {
                                if (Nexttag.Mode.EndsWith("Direction") || Nexttag.Mode.EndsWith("Count"))
                                {
                                    Nexttag.Mode = "Digital Input";
                                    if (Nexttag.Type == IOType.DigitalInput)
                                    {
                                        Nexttag.InpuFilterValue = "10";
                                        Nexttag.IsEnableInputFilter = true;
                                    }
                                }
                            }
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
            if (control.Text.StartsWith("Q") || control.Text.StartsWith("I"))
            {
                e.Cancel = true;
                errorProvider.SetError(control, "Add Datatypes Only IO's are not added from here");
                return;
            }
            if (string.IsNullOrEmpty(control.Text))     // Allow untouched or emptied operands.
            {
                validationSuccessful = true;
            }
            else
            {
                validationSuccessful = ValidateAddressOperand(control, out error);
            }

            if (validationSuccessful)
            {
                e.Cancel = false;
                errorProvider.SetError(control, null);
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(control, error);
            }
        }

        /// <summary>
        /// Validating Address type of operands
        /// </summary>
        /// <param name="control">Send the nmae of control.</param>        
        /// <returns>True if operand is valid else false.</returns>
        /// <returns>Error decription as String.</returns>
        private bool ValidateAddressOperand(Control control, out string error)
        {
            if (string.IsNullOrEmpty(control.Text))     // Allow untouched or emptied operands.
            {
                error = "";
                return true;

            }
            string address = control.Text;

            if (address == "-") address = control.Text;
            bool validationSuccessful;

            string dataType = ((DataType)comboBoxIOType.SelectedItem).Text;
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
                default:
                    error = "Invalid Word address";
                    validationSuccessful = address.IsValidWordAddress();
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


        private void ValidateInitialValueForAddress(Control control, CancelEventArgs e, string errorMessageToDisplay)
        {
            if (comboBoxIOType.Visible == false && textBoxType.Text != IOType.DataType.ToString() && comboBoxIOType.Text == "")
            {
                if (textBoxLogicalAddress.Text.Contains(".") || textBoxLogicalAddress.Text.StartsWith("F2"))
                {
                    comboBoxIOType.Text = "Bool";
                }
                else if (textBoxLogicalAddress.Text.StartsWith("P"))
                {
                    comboBoxIOType.Text = "Real";
                }
                else
                {
                    comboBoxIOType.Text = "Word";
                }
            }
            string datatype = comboBoxIOType.Visible ? comboBoxIOType.Text.ToString() : textBoxLogicalAddress.Text.ToString().Contains('.') ? "Bool" : "Word";
            if (datatype.Equals("Real") && !string.IsNullOrEmpty(control.Text))
            {
                string value = control.Text.Trim();
                if (value.StartsWith(".") || value.EndsWith("."))
                {
                    e.Cancel = true;
                    errorProvider.SetError(control, "Invalid real number format.");
                    return;
                }
            }
            if (control.Text.IsValidInitialValueForAddress(textBoxLogicalAddress.Text.ToString(), datatype))
            {
                e.Cancel = false;
                errorProvider.SetError(control, null);
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(control, errorMessageToDisplay);
            }
        }

        private void textBoxInitialValue_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxInitialValue.Text != "")
            {
                ValidateInitialValueForAddress(textBoxInitialValue, e, "Invalid Initial value for selected Address");
            }

        }

        private void textBoxLogicalAddress_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxLogicalAddress.Enabled)
                ValidateOperand(textBoxLogicalAddress, e);
            TagCount.Maximum = XMProValidator.GetMaxLength(textBoxLogicalAddress.Text);
        }

        private void comboBoxIOType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxIOType.Visible)
            {
                this.textBoxLabel.Text = this.comboBoxIOType.SelectedItem.ToString();
                textBoxLogicalAddress.Text = XMProValidator.GetNextAddress(this.textBoxLabel.Text, "");
                TagCount.Maximum = XMProValidator.GetMaxLength(textBoxLogicalAddress.Text);
            }

        }

        public TagsUserControl(string LogicalAddress, string oldTagName, string count)
        {
            InitializeComponent();
            xm = XMPS.Instance;
            string[] AddressParts = LogicalAddress.Split(':');
            int AddressPartSecond = int.Parse(AddressParts[1]) + 15;
            string LastlogicalAddress = $"{AddressParts[0]}:{AddressPartSecond:000}";
            int usedAddCount = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")).Where(T => int.Parse(T.LogicalAddress.Split(':')[1]) >= int.Parse(LogicalAddress.Split(':')[1]) && int.Parse(T.LogicalAddress.Split(':')[1]) <= int.Parse(LastlogicalAddress.Split(':')[1])).Count();
            if (usedAddCount > 0)
            {
                LastlogicalAddress = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")).OrderBy(T => T.LogicalAddress).Last().LogicalAddress;
            }
            List<XMIOConfig> usedTagList = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")).Where(T => int.Parse(T.LogicalAddress.Split(':')[1]) >= int.Parse(LogicalAddress.Split(':')[1]) && int.Parse(T.LogicalAddress.Split(':')[1]) <= int.Parse(LastlogicalAddress.Split(':')[1])).OrderBy(T => T.LogicalAddress).ToList();
            DialogResult btn = DialogResult.No;
            string unUsedTagLogicalAdd = "";
            if (usedTagList.Count > 0)
            {
                btn = MessageBox.Show("Next Current Tag is Already Used You Want to Used Next Unused Tag ", "XMPS200", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                string lastUsedTagLogicalAdd = usedTagList.Last().LogicalAddress;
                string[] parts1 = lastUsedTagLogicalAdd.Split(':');
                int lastUsedTagLogicalAddsecondPart = int.Parse(parts1[1]);
                int lastUsedTagLogicalAddlastTagAdd = int.Parse(parts1[1]) + 1;
                unUsedTagLogicalAdd = $"{parts1[0]}:{lastUsedTagLogicalAddlastTagAdd:000}";
            }
            if (usedAddCount == 0 || btn == DialogResult.Yes)
            {
                if (LogicalAddress.Contains(":"))
                {

                    LogicalAddress = usedTagList.Count == 0 ? LogicalAddress : unUsedTagLogicalAdd;
                    // string firstLogicalAddress = LogicalAddress;
                    string[] parts = LogicalAddress.Split(':');

                    int secondPart = int.Parse(parts[1]);
                    int lastTagAdd = int.Parse(parts[1]) + 16;
                    int totalTagCount = xm.LoadedProject.Tags.Count();
                    string packFBTagNameStart = oldTagName;
                    if (count == "Pack")
                    {
                        packFBTagNameStart = oldTagName != "" ? oldTagName : "PK1";
                        //Checks for how many Pack Instuction are in the Logic Rung
                        var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCodeNm == "Pack");
                        if (code != null && code.Count() > 0 && oldTagName == "")
                        {
                            var maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", "")));
                            int maxPackNo = Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1;
                            packFBTagNameStart = "PK" + maxPackNo;
                        }
                    }
                    if (count == "UnPack")
                    {
                        packFBTagNameStart = oldTagName != "" ? oldTagName : "UPK1";
                        //Checks for how many Pack Instuction are in the Logic Rung
                        var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCodeNm == "UnPack");
                        if (code != null && code.Count() > 0 && oldTagName == "")
                        {
                            var maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", "")));
                            int maxPackNo = Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1;
                            packFBTagNameStart = "UPK" + maxPackNo;
                        }
                    }

                    XMIOConfig newTag;
                    XMIOConfig firstTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == LogicalAddress).FirstOrDefault();
                    if (firstTag == null)
                    {
                        int tagnum = 1;
                        for (int i = secondPart; i < lastTagAdd; i++)
                        {
                            string nextAddress = LogicalAddress;
                            newTag = new XMIOConfig();
                            newTag.IoList = IOListType.NIL;
                            newTag.Type = IOType.DataType;
                            string currentFormName = xm.CurrentScreen.Split('#')[0];
                            newTag.Model = currentFormName.Equals("UDFLadderForm") ? xm.CurrentScreen.Split('#')[1].Replace(" Logic", " Tags") : "User Defined Tags";
                            newTag.Label = "Bool";
                            newTag.LogicalAddress = nextAddress;
                            newTag.Tag = packFBTagNameStart + "_" + tagnum;
                            newTag.ActualName = packFBTagNameStart + "_" + tagnum;
                            newTag.Key = totalTagCount + 1;
                            xm.LoadedProject.Tags.Add(newTag);
                            tagnum = tagnum + 1;
                            int nextAddNo = i + 1;
                            totalTagCount = totalTagCount + 1;
                            LogicalAddress = $"{parts[0]}:{nextAddNo:000}";
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Address Already Used In Another Block", "XMPS200", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ShowingEnableFilterControls(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dataSource != null && !string.IsNullOrEmpty(dataSource.RetentiveAddress) && dataSource.RetentiveAddress.StartsWith("Retentive Address Going Beyond Range, Can't Add New Address "))
            {
                dataSource.Retentive = false;
                dataSource.RetentiveAddress = string.Empty;
            }
            this.ParentForm.Close();
            this.ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void checkBoxEnableInputFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableInputFilter.Checked)
                textBoxInFilValue.Enabled = true;
            else
            {
                textBoxInFilValue.Enabled = false;
                textBoxInFilValue.Text = "10";
            }
        }

        private void textBoxInFilValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits and backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxInFilValue_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxInFilValue.Text, out int value))
            {
                if (value < 1)
                    value = 1;
                else if (value > 20)
                    value = 20;

                textBoxInFilValue.Text = value.ToString();
            }
            else
            {
                textBoxInFilValue.Text = "10";
            }
        }

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataSource == null)
                return;
            if (!xm.LoadedProject.IsEditableDigitalFilter)
                return;
            if (!xm.LoadedProject.IsEditableDigitalFilter)
                return;
            if (dataSource.Type == IOType.DigitalInput && comboBoxMode.SelectedValue.Equals("Digital Input") && dataSource.Model == "XM-14-DT-HIO")
            {
                ShowingEnableFilterControls(true);
                textBoxInFilValue.Enabled = dataSource.IsEnableInputFilter;
                textBoxInFilValue.Text = !string.IsNullOrEmpty(dataSource.InpuFilterValue) ? dataSource.InpuFilterValue
                                         : "10";
                checkBoxEnableInputFilter.Checked = dataSource.IsEnableInputFilter;
            }
            //for Univer Input showing previous Filter value if Mode index set to Digital
            else if (dataSource.Type == IOType.UniversalInput
         && comboBoxMode.SelectedValue != null
         && comboBoxMode.SelectedValue.ToString().Equals("Digital"))
            {
                var prevData = xm.LoadedProject.Tags.FirstOrDefault(t => t.Model != null && t.Model.Equals(dataSource.Model) && !string.IsNullOrEmpty(t.Mode) && t.Mode.Equals("Digital") && t.IsEnableInputFilter);
                if (prevData != null)
                {
                    ShowingEnableFilterControls(true);
                    textBoxInFilValue.Text = prevData.InpuFilterValue;
                    checkBoxEnableInputFilter.Checked = prevData.IsEnableInputFilter;
                }
                else
                    ShowingEnableFilterControls(true);
            }
            else
            {
                ShowingEnableFilterControls(false);
            }
        }

        private void ShowingEnableFilterControls(bool value)
        {
            checkBoxEnableInputFilter.Visible = value;
            textBoxInFilValue.Visible = value;
            labelDigitalFilter.Visible = value;
        }
    }
}
