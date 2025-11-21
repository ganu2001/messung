using LadderEditorLib.DInterpreter;
using LadderEditorLib.MementoDesign;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.Serializer;
using XMPS2000.Core.Types;
using XMPS2000.LadderLogic;

namespace XMPS2000
{
    public class ProjectUpdate : Form
    {
        private GroupBox groupBox1;
        private Label lblSelectDevice;
        private Label lblDeviceModel;
        private Button btnModify;
        private ComboBox comboBox1;
        private TextBox txtDeviceModel;
        private GroupBox groupBox2;
        private Button btnClose;
        private XMPS xm;
        public string UpdateModel { get; set; }
        public string currentModel { get; set; }
        List<XMIOConfig> IOTags;
        List<XMIOConfig> CurrentTag;
        List<string> data;
        string CurrentModel;
        string UpdatingModel;
        public LadderWindow _objParent;
        public bool IsProjectUpdate = false;
        FindAndReplaceLogic findAndReplaceLogic;
        public ProjectUpdate()
        {
            InitializeComponent();
            this.xm = XMPS.Instance;
            this.StartPosition = FormStartPosition.CenterScreen;
            string deviceModel = xm.PlcModel;
            txtDeviceModel.Text = deviceModel;
            var xBldItems = new List<string> { "XBLD-14", "XBLD-17" };
            var otherItems = new List<string> { "XM-14-DT", "XM-14-DT-HIO", "XM-17-ADT" };
            var xBldProItems = new List<string> { "XBLD-14E", "XBLD-17E" };
            var xmproProItems = new List<string> { "XM-14-DT-E", "XM-14-DT-HIO-E", "XM-17-ADT-E" };

            if (txtDeviceModel.Text.Equals("XBLD-14") || txtDeviceModel.Text.Equals("XBLD-17"))
            {
                // Loop through xBldItems and add to ComboBox if not already in txtDeviceModel.Text
                foreach (var item in xBldItems)
                {
                    if (!txtDeviceModel.Text.Contains(item))
                    {
                        comboBox1.Items.Add(item);
                    }
                }
            }
            else if (txtDeviceModel.Text.Equals("XBLD-14E") || txtDeviceModel.Text.Equals("XBLD-17E"))
            {
                // Loop through xBldProItems and add to ComboBox if not already in txtDeviceModel.Text
                foreach (var item in xBldProItems)
                {
                    if (!txtDeviceModel.Text.Contains(item))
                    {
                        comboBox1.Items.Add(item);
                    }
                }
            }
            else if (xmproProItems.Contains(txtDeviceModel.Text))
            {
                foreach (var item in xmproProItems.Concat(otherItems))
                {
                    if (!txtDeviceModel.Text.Equals(item))
                    {
                        comboBox1.Items.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in otherItems.Concat(xmproProItems))
                {
                    if (!txtDeviceModel.Text.Equals(item))
                    {
                        comboBox1.Items.Add(item);
                    }
                }
            }
            comboBox1.SelectedIndex = 0;
            findAndReplaceLogic = new FindAndReplaceLogic(xm, _objParent);
        }
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnModify = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtDeviceModel = new System.Windows.Forms.TextBox();
            this.lblSelectDevice = new System.Windows.Forms.Label();
            this.lblDeviceModel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnModify);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.txtDeviceModel);
            this.groupBox1.Controls.Add(this.lblSelectDevice);
            this.groupBox1.Controls.Add(this.lblDeviceModel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modify Device";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(503, 79);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(86, 29);
            this.btnModify.TabIndex = 4;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(127, 80);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(361, 28);
            this.comboBox1.TabIndex = 3;
            // 
            // txtDeviceModel
            // 
            this.txtDeviceModel.Enabled = false;
            this.txtDeviceModel.Location = new System.Drawing.Point(127, 43);
            this.txtDeviceModel.Name = "txtDeviceModel";
            this.txtDeviceModel.Size = new System.Drawing.Size(361, 26);
            this.txtDeviceModel.TabIndex = 2;
            // 
            // lblSelectDevice
            // 
            this.lblSelectDevice.AutoSize = true;
            this.lblSelectDevice.Location = new System.Drawing.Point(19, 80);
            this.lblSelectDevice.Name = "lblSelectDevice";
            this.lblSelectDevice.Size = new System.Drawing.Size(111, 20);
            this.lblSelectDevice.TabIndex = 1;
            this.lblSelectDevice.Text = "Select device :";
            // 
            // lblDeviceModel
            // 
            this.lblDeviceModel.AutoSize = true;
            this.lblDeviceModel.Location = new System.Drawing.Point(18, 43);
            this.lblDeviceModel.Name = "lblDeviceModel";
            this.lblDeviceModel.Size = new System.Drawing.Size(112, 20);
            this.lblDeviceModel.TabIndex = 0;
            this.lblDeviceModel.Text = "Device model :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Location = new System.Drawing.Point(13, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(598, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(502, 67);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 27);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ProjectUpdate
            // 
            this.ClientSize = new System.Drawing.Size(619, 296);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectUpdate";
            this.ShowIcon = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to take backup of older project", "XMPS2000", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var _projectName = xm.LoadedProject.ProjectName;
                ForceUserControl.setValueDic.Clear();
                var projectPath = xm.LoadedProject.ProjectPath;
                var backupPath = Path.ChangeExtension(projectPath, ".xmprjbkp");
                try
                {
                    if (File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                    }
                    if (File.Exists(projectPath))
                    {
                        var projectContent = File.ReadAllText(projectPath);
                        File.WriteAllText(backupPath, projectContent, Encoding.Unicode);
                    }
                    else
                    {
                        MessageBox.Show("Original project file does not exist.");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"An error occurred while creating the backup file: {ex.Message}");
                }
            }
            CurrentModel = txtDeviceModel.Text;
            UpdatingModel = comboBox1.SelectedItem.ToString();
            CurrentTag = xm.LoadedProject.Tags.ToList();
            IOTags = CurrentTag.Where(r => r.IoList.ToString() == "OnBoardIO").ToList();
            data = CurrentTag.Select(a => a.LogicalAddress).ToList();
            if (CurrentModel == "XBLD-17" || CurrentModel == "XM-17-ADT" || CurrentModel == "XM-17-ADT-E" || CurrentModel == "XBLD-17E")
            {
                FindingtagsInRungsAndDevices(UpdatingModel, CurrentModel);
            }
            else
            {
                CallingMethods(UpdatingModel, CurrentModel);
            }
            XMPS.Instance.LoadedProject.PlcModel = UpdatingModel;
            //additionally checking 
            if (XMPS.Instance.LoadedProject.PlcModel == "XM-17-ADT" || XMPS.Instance.LoadedProject.PlcModel == "XM-14-DT" || XMPS.Instance.LoadedProject.PlcModel == "XM-14-DT-HIO" || XMPS.Instance.LoadedProject.PlcModel == "XM-17-ADT-E" || XMPS.Instance.LoadedProject.PlcModel == "XM-14-DT-E" || XMPS.Instance.LoadedProject.PlcModel == "XM-14-DT-HIO-E")
            {
                xm.LoadedProject.Tags.Where(T => T.Type != IOType.DigitalInput && T.Type != IOType.UniversalInput).ToList()
                 .ForEach(T =>
                 {
                     T.IsEnableInputFilter = false;
                     T.InpuFilterValue = string.Empty;
                 });

                if (CurrentModel == "XM-14-DT-HIO" || CurrentModel == "XM-14-DT-HIO-E")
                {
                    xm.LoadedProject.Tags.Where(T => T.IoList == IOListType.OnBoardIO && T.Type == IOType.DigitalInput && !T.IsEnableInputFilter).ToList()
                 .ForEach(T =>
                 {
                     T.IsEnableInputFilter = true;
                     T.InpuFilterValue = "10";
                 });
                }
            }
            if (xm.LoadedProject.PlcModel == "XM-14-DT-HIO" || xm.LoadedProject.PlcModel == "XM-14-DT-HIO-E")
            {
                xm.LoadedProject.Tags.Where(T => T.Type == IOType.DigitalInput && T.IoList == IOListType.OnBoardIO && (T.Label == "DI5" || T.Label == "DI7")).ToList()
                .ForEach(T =>
                {
                    T.IsEnableInputFilter = true;
                    T.InpuFilterValue = "10";
                    T.Mode = "Digital Input";
                });
            }
            if ((CurrentModel == "XM-14-DT-HIO" || CurrentModel == "XM-14-DT-HIO-E") && (UpdatingModel != "XM-14-DT-HIO" && UpdatingModel != "XM-14-DT-HIO-E"))
            {
                xm.LoadedProject.Tags.Where(T => T.Type == IOType.DigitalInput && T.IoList == IOListType.OnBoardIO && (T.Label == "DI5" || T.Label == "DI7")).ToList()
                    .ForEach(T =>
                    {
                        T.Mode = string.Empty;
                    });
            }

        }
        private void FindingtagsInRungsAndDevices(string updatingModel, string currentModel)
        {
            var inputAddressList = new HashSet<string>();
            var inputValuesToCheck = new HashSet<string> { "I1:001", "I1:002", "Q0:001" };
            var blocks = xm.LoadedProject.Blocks;
            var devices = xm.LoadedProject.Devices;
            // Extract addresses from blocks
            foreach (var block in blocks)
            {
                foreach (var element in block.Elements)
                {
                    int startIndex = element.IndexOf('[');
                    int endIndex = element.IndexOf(']');
                    if (startIndex == -1 || endIndex == -1 || startIndex >= endIndex)
                        continue;
                    string curRung = element.Substring(startIndex, endIndex - startIndex + 1);
                    var inputs = curRung.Split(' ').Where(x => x.Contains("IN:") || x.Contains("OP:"))
                                                     .Select(x => x.Replace("IN:", "").Replace("OP:", ""));
                    foreach (var input in inputs)
                    {
                        var cleanedInput = input.Replace("]", string.Empty);
                        inputAddressList.Add(cleanedInput);
                    }
                }
            }
            // Extract addresses from devices
            foreach (var device in devices)
            {
                string deviceType = device.GetType().Name;
                var deviceProperties = device.GetType().GetProperties();
                foreach (var property in deviceProperties)
                {
                    if (property.PropertyType.IsGenericType &&
                        property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var items = property.GetValue(device) as IEnumerable;
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                string logicalAddress = GetTheLogicalAddressFromTag(item.GetType().GetProperty("Tag").GetValue(item) as string);
                                inputAddressList.Add(logicalAddress);
                            }
                        }
                    }
                    else if (property.Name == "Tag")
                    {
                        string logicalAddress = GetTheLogicalAddressFromTag(property.GetValue(device) as string);
                        inputAddressList.Add(logicalAddress);
                    }
                }
            }
            // Check for any available analog address
            bool isAnyAddressAvailable = inputValuesToCheck.Overlaps(inputAddressList);
            if (isAnyAddressAvailable)
            {
                xm.LoadedProject.CommonAddresses = inputAddressList.Intersect(inputValuesToCheck).ToList();
            }

            if (!((currentModel == "XM-17-ADT" && updatingModel == "XM-17-ADT-E") || (currentModel == "XM-17-ADT-E" && updatingModel == "XM-17-ADT")))
            {
                foreach (string _address in inputValuesToCheck)
                {
                    XMIOConfig temptag = new XMIOConfig();
                    temptag.LogicalAddress = "???";
                    temptag.Tag = "???";
                    xm.LoadedProject.Tags.Add(temptag);
                    findAndReplaceLogic.blkWiseCount.Clear();
                    findAndReplaceLogic.FindForEntireLogicBlock(_address, true);
                    findAndReplaceLogic.ReplaceAllText(_address, "???", true, null, true);
                    findingAndReplaceAnalogTagFromDevices(_address, temptag);
                    FindAndReplace._findList.Clear();
                    xm.LoadedProject.Tags.Remove(temptag);
                }
            }

            CallingMethods(updatingModel, currentModel);
        }
        //Getting logical address from tagname
        public string GetTheLogicalAddressFromTag(string TagName)
        {
            XMPS xm = XMPS.Instance;
            if (TagName == "-Select Tag Name-" || TagName == null || TagName == "")
            {
                return "";
            }
            else
            {
                //adding check for not take commeted tag as a input when changing selected index change.
                var LogicalAddress = CurrentTag.Where(d => d.Tag == TagName && !(d.LogicalAddress.StartsWith("'"))).
                                                           Select(d => d.LogicalAddress).FirstOrDefault();
                LogicalAddress = LogicalAddress is null ? TagName : LogicalAddress;
                return LogicalAddress.ToString();
            }
        }
        private void CallingMethods(string UpdatingModel, string CurrentModel)
        {
            AddTags(UpdatingModel, CurrentModel);
            UpdateSystemTags(UpdatingModel, CurrentModel);
            UpdateModel = UpdatingModel;
            currentModel = CurrentModel;
            this.DialogResult = DialogResult.OK;
        }
        //Update System tags As per Plc Module
        private void UpdateSystemTags(string updatingModel, string CurrentModel)
        {
            xm.LoadedProject.Tags.RemoveAll(t => t.LogicalAddress.StartsWith("S3"));
            xm.LoadedProject.Tags.AddRange(CommonFunctions.GetSystemTagList(updatingModel));
        }
        //Adding Tags as per PLC model in updating project.
        public void AddTags(string updatingModel, string currentModel)
        {
            string fixpath = "MessungSystems\\XMPS2000\\ProjectTemplates";
            string newFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString(),
                                              fixpath, updatingModel, updatingModel + ".plc");
            SerializeDeserialize<XMProject> sdXMProject = new SerializeDeserialize<XMProject>();
            var mdata = sdXMProject.DeserializeData(newFilePath);
            var newTags = mdata.Tags.Where(t => t.IoList.ToString() == "OnBoardIO").ToList();
            XMPS.Instance.LoadedProject.DiagnosticParametersEnabled = mdata.DiagnosticParametersEnabled;
            var currentTags = xm.LoadedProject.Tags.Where(t => t.IoList.ToString() == "OnBoardIO").ToList();
            var currentTagAddresses = currentTags.Select(t => t.LogicalAddress).ToHashSet();
            xm.PlcModel = updatingModel;
            bool isSameXM17 = (currentModel == "XM-17-ADT" && updatingModel == "XM-17-ADT-E") || (currentModel == "XM-17-ADT-E" && updatingModel == "XM-17-ADT");
            if (newTags.Count == currentTags.Count || isSameXM17)
            {
                SetModesAndModelOfTags(updatingModel);
            }
            else
            {
                List<XMIOConfig> InputTags = xm.LoadedProject.Tags.Where(t => t.Type == IOType.AnalogInput || t.Type == IOType.UniversalInput || t.Type == IOType.DigitalInput || t.Label.Contains("_O")).Where(t => t.IoList != IOListType.OnBoardIO).ToList();
                List<XMIOConfig> OutputTags = xm.LoadedProject.Tags.Where(t => t.Type == IOType.AnalogOutput || t.Type == IOType.UniversalOutput || t.Type == IOType.DigitalOutput).Where(t => t.IoList != IOListType.OnBoardIO).Where(t => !t.Label.Contains("_O")).ToList();
                var missingTags = newTags.Where(t => !currentTagAddresses.Contains(t.LogicalAddress)).ToList();
                updateMQTTtagName();
                if (newTags.Count > currentTags.Count)
                {
                    if (xm.LoadedProject.Tags.Where(t => t.IoList == IOListType.ExpansionIO ||
                                                         t.IoList == IOListType.RemoteIO).Any())
                    {
                        xm.LoadedProject.Blocks.RemoveAll(T => T.Type.Equals("InterruptLogicBlock"));
                        xm.LoadedProject.MainLadderLogic.RemoveAll(T => T.StartsWith("InterruptLogicBlock"));
                        UpdateAnalogAddresses(InputTags, 3, OutputTags, 1);
                        RemoveAndAddTags(InputTags, OutputTags);
                    }
                    xm.LoadedProject.Tags.AddRange(missingTags);
                    //for updating onBoardAnalog Calibaration address.
                    if (XMPS.Instance.LoadedProject.DiagnosticParametersEnabled)
                    {
                        BacNetObjectHelper.AddDignosticTags(false, updatingModel, "Analog Input");
                    }
                    //below logic for adding or removing onBoard AIAO tags.
                    if (updatingModel == "XBLD-17" || updatingModel == "XBLD-17E")
                    {
                        if (xm.LoadedProject.BacNetIP != null)
                        {
                            missingTags.Reverse();
                            foreach (string add in missingTags.Select(t => t.LogicalAddress))
                            {
                                string objectIdentifier = add.StartsWith("Q0:001") ? "Analog Output:0" : add.StartsWith("I1:001") ? "Analog Input:0" : "Analog Input:1";
                                string instanceNumber = (Convert.ToInt32(add.Split(':')[1]) - 1).ToString();
                                string objectType = add.StartsWith("I") ? "0:Analog Input" : "1:Analog Output";
                                string tag = missingTags.Where(t => t.LogicalAddress.Equals(add)).Select(t => t.Tag).FirstOrDefault();
                                string tagType = add.StartsWith("I") ? "AnalogInput" : "AnalogOutput";
                                xm.LoadedProject.BacNetIP.AnalogIOValues.Insert(0, new AnalogIOV(objectIdentifier, instanceNumber, objectType, objectIdentifier, tag, add, false, tagType));
                            }
                        }
                    }

                }
                else if (newTags.Count < currentTags.Count)
                {
                    var newTagAddresses = newTags.Select(t => t.LogicalAddress).ToHashSet();
                    var tagsToRemove = currentTags.Where(t => !newTagAddresses.Contains(t.LogicalAddress)).ToList();
                    var expansionAddresses = InputTags.Where(t => t.IoList.ToString() == "ExpansionIO" || t.IoList.ToString() == "RemoteIO").ToList();
                    // Shift tags by -3
                    if (!XMPS.Instance.LoadedProject.DiagnosticParametersEnabled)
                    {
                        var outputAddresses = (xm.LoadedProject.Tags.Where(tag => tag.Label.EndsWith("_OR") || tag.Label.EndsWith("_OL")).Select(tag => tag.LogicalAddress)).ToList();

                        xm.LoadedProject.Tags.RemoveAll(input => outputAddresses.Contains(input.LogicalAddress));
                        InputTags.RemoveAll(input => outputAddresses.Contains(input.LogicalAddress));
                        OutputTags.RemoveAll(input => outputAddresses.Contains(input.LogicalAddress));
                    }
                    if (expansionAddresses.Count() > 0)
                        UpdateAnalogAddresses(InputTags, -3, OutputTags, -1);
                    RemoveAndAddTags(InputTags, OutputTags);
                    foreach (var tag in tagsToRemove)
                    {
                        xm.LoadedProject.Tags.Remove(tag);
                    }
                    if ((updatingModel == "XBLD-14" || updatingModel == "XBLD-14E") && !tagsToRemove.Any(t => xm.LoadedProject.Tags.Any(a => a.LogicalAddress.Equals(t.LogicalAddress))))
                    {
                        xm.LoadedProject.BacNetIP.AnalogIOValues.RemoveAll(t => tagsToRemove.Select(a => a.LogicalAddress).ToList().Contains(t.LogicalAddress));
                    }
                    else
                    {
                        if (xm.LoadedProject.BacNetIP != null)
                            xm.LoadedProject.BacNetIP.AnalogIOValues.RemoveAll(t => tagsToRemove.Select(a => a.LogicalAddress).ToList().Contains(t.LogicalAddress) && t.InstanceNumber.Length == 1);
                    }
                }
                SetModesAndModelOfTags(updatingModel);
            }
            xm.FindList.Clear();
            xm.IsProjectModified();
        }

        private void updateMQTTtagName()
        {
            foreach (var pr in XMPS.Instance.LoadedProject.Devices.OfType<Publish>()
            .SelectMany(p => p.PubRequest)
            .Where(pr => !string.IsNullOrEmpty(pr.Tag) && pr.Tag.Contains(":")))
            {
                var matchingTag = XMPS.Instance.LoadedProject.Tags
                    .Where(T => T.LogicalAddress == pr.Tag)
                    .Select(T => T.Tag)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(matchingTag))
                {
                    pr.Tag = matchingTag;
                }
            }
            foreach (var sr in XMPS.Instance.LoadedProject.Devices.OfType<Subscribe>()
            .SelectMany(p => p.SubRequest)
            .Where(sr => !string.IsNullOrEmpty(sr.Tag) && sr.Tag.Contains(":")))
            {
                var matchingTag = XMPS.Instance.LoadedProject.Tags
                    .Where(T => T.LogicalAddress == sr.Tag)
                    .Select(T => T.Tag)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(matchingTag))
                {
                    sr.Tag = matchingTag;
                }
            }
        }

        private void RemoveAndAddTags(List<XMIOConfig> InputTags, List<XMIOConfig> OutputTags)
        {
            xm.LoadedProject.Tags.RemoveAll(t => t.Type == IOType.AnalogInput || t.Type == IOType.UniversalInput || t.Type == IOType.AnalogOutput || t.Type == IOType.UniversalOutput);
            // Add updated input and output tags
            xm.LoadedProject.Tags.AddRange(InputTags.Where(t => t.Type == IOType.AnalogInput || t.Type == IOType.UniversalInput || t.Type == IOType.AnalogOutput || t.Type == IOType.UniversalOutput));
            xm.LoadedProject.Tags.AddRange(OutputTags.Where(t => t.Type == IOType.AnalogInput || t.Type == IOType.UniversalInput || t.Type == IOType.AnalogOutput || t.Type == IOType.UniversalOutput));
        }
        //Update analog addresses as per PLC model if Expansion is added 
        private void UpdateAnalogAddresses(List<XMIOConfig> inputTags, int inputShift, List<XMIOConfig> outputTags, int outputShift)
        {

            foreach (string nextModel in xm.LoadedProject.Tags.Where(t => t.Model != null && (t.IoList.ToString() == "ExpansionIO" || t.IoList.ToString() == "RemoteIO")).OrderBy(t => t.LogicalAddress).Select(t => t.Model).Distinct())
            {
                int analogIO = xm.LoadedProject.Tags.Where(t => t.Model == nextModel && t.Type != IOType.DigitalOutput && t.Type != IOType.DigitalInput).Count();
                List<XMIOConfig> curInputTags = inputTags.Where(tag => tag.Model == nextModel).OrderBy(tag => ExtractTagNumber(tag.LogicalAddress)).ToList();
                List<XMIOConfig> curOutputTags = outputTags.Where(tag => tag.Model == nextModel).OrderBy(tag => ExtractTagNumber(tag.LogicalAddress)).ToList();
                IsProjectUpdate = true;
                if (XMPS.Instance.LoadedProject.DiagnosticParametersEnabled && analogIO > 0)
                {
                    bool alreadyDignostic = curInputTags.Where(t => t.Tag.EndsWith("_OR") || t.Tag.EndsWith("_OL")).Count() == 0;
                    string tagType = curInputTags.Where(t => t.Model == nextModel && t.Type.ToString().Contains("Input")).Select(t => t.Type).FirstOrDefault().ToString() == "AnalogInput" ? "Analog Input" : xm.LoadedProject.Tags.Where(t => t.Model == nextModel && t.Type.ToString().Contains("Input")).Select(t => t.Type).FirstOrDefault().ToString();
                    xm.LoadedProject.Tags.AddRange(curInputTags);
                    xm.LoadedProject.Tags.AddRange(curOutputTags);
                    BacNetObjectHelper.AddDignosticTags(true, nextModel, tagType);
                    // Get tags for the next model
                    var newTags = xm.LoadedProject.Tags.Where(t => t.Model == nextModel).ToList();
                    // Add only tags that don't already exist in inputTags
                    foreach (var tag in newTags)
                    {
                        // Check if the tag doesn't already exist in inputTags
                        if (!inputTags.Any(t => t.LogicalAddress == tag.LogicalAddress)) // Assuming tags have an Id property for comparison
                        {
                            // Add the tag to inputTags
                            inputTags.Add(tag);
                        }
                    }
                    if (alreadyDignostic)
                        inputShift = inputShift > 0 ? inputShift + 1 : inputShift - 1;
                }
                else
                    inputShift = (inputShift > 0) ? inputShift : analogIO > 0 ? inputShift - 1 : inputShift;
                curInputTags.Reverse();
                curOutputTags.Reverse();
                inputTags.RemoveAll(input => outputTags.Any(output => output.LogicalAddress == input.LogicalAddress));

            }
            // Update input tags
            UpdateTags(inputTags, inputShift, IOType.AnalogInput);
            // Update output tags
            UpdateTags(outputTags, outputShift, IOType.AnalogOutput);

            if (outputShift > 0)
            {
                foreach (XMIOConfig optag in inputTags.Where(t => t.Type.Equals(IOType.AnalogOutput) && (t.Tag.EndsWith("_OR") || t.Tag.EndsWith("_OL"))))
                {
                    string oldTagName = optag.Tag;
                    string maintagnm = outputTags.Where(t => t.Label == optag.Label.ToString().Replace("_OR", "").Replace("_OL", "")).Select(t => t.Tag).FirstOrDefault();
                    findAndReplaceLogic.blkWiseCount.Clear();
                    findAndReplaceLogic.FindForEntireLogicBlock(oldTagName, IsProjectUpdate);
                    optag.Tag = maintagnm + "" + optag.Label.Substring(optag.Label.Trim().Length - 3, 3);
                    findAndReplaceLogic.ReplaceAllText(oldTagName, optag.Tag, IsProjectUpdate, optag);
                    findingAndReplaceAnalogTagFromDevices(oldTagName, optag);
                    FindAndReplace._findList.Clear();
                }
            }
        }
        private int ExtractTagNumber(string tagName)
        {
            // Assuming tagName is in the format "I1:xxx", extract the numeric part after the colon
            var numericPart = tagName.Split(':').LastOrDefault();
            return int.TryParse(numericPart, out int tagNumber) ? tagNumber : 0;
        }
        //update tag then find old tag used on screen and replace it with new updated tag
        private void UpdateTags(List<XMIOConfig> tags, int shift, IOType ioType)
        {
            if (shift > 0) tags.Reverse();
            foreach (var tag in tags)
            {
                XMIOConfig tempTag = new XMIOConfig
                {
                    IoList = tag.IoList,
                    LogicalAddress = tag.LogicalAddress,
                    Tag = tag.Tag,
                    Model = tag.Model,
                    Type = tag.Type
                };
                xm.LoadedProject.Tags.RemoveAll(t => t.LogicalAddress == tempTag.LogicalAddress && t.Model == tempTag.Model);
                xm.LoadedProject.Tags.Add(tempTag);
                var oldTagName = tag.Tag;
                findAndReplaceLogic.blkWiseCount.Clear();
                var findingBlockCount = xm.LoadedProject.Blocks.Count();
                if (findingBlockCount == 1)
                {
                    //findAndReplaceLogic.FindNext(oldTagName, "Current LogicBlock", IsProjectUpdate);
                    findAndReplaceLogic.FindForEntireLogicBlock(oldTagName, IsProjectUpdate);
                }
                else
                {
                    findAndReplaceLogic.FindForEntireLogicBlock(oldTagName, IsProjectUpdate);
                }
                if (xm.LoadedProject.BacNetIP != null)
                {
                    if (xm.LoadedProject.BacNetIP.BinaryIOValues.Any(t => t.LogicalAddress.Equals(tag.LogicalAddress)))
                    {

                        xm.LoadedProject.BacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(tag.LogicalAddress)).LogicalAddress =
                            IncrementTag(tag.LogicalAddress, shift) + (tag.LogicalAddress.Contains(".") ? "." + tag.LogicalAddress.Split('.')[1] : "");
                    }
                    if (xm.LoadedProject.BacNetIP.AnalogIOValues.Any(t => t.LogicalAddress.Equals(tag.LogicalAddress)))
                    {
                        xm.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(tag.LogicalAddress)).LogicalAddress =
                            IncrementTag(tag.LogicalAddress, shift) + (tag.LogicalAddress.Contains(".") ? "." + tag.LogicalAddress.Split('.')[1] : "");
                        string type = xm.LoadedProject.Tags.Where(t => t.LogicalAddress.Equals(tag.LogicalAddress)).Select(T => T.Type.ToString()).FirstOrDefault();
                        //if (type == "AnalogInput" || type == "AnalogOutput")
                        if ((ioType == IOType.AnalogInput && tag.Type == IOType.AnalogInput) || (ioType == IOType.AnalogOutput && tag.Type == IOType.AnalogOutput))
                        {
                            string newObjectName = IncrementTagSuffix(tag.Tag, shift < 0 ? shift + 1 : shift).ToString();
                            var analogIO = xm.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.ObjectName.Equals(tag.Tag));
                            if (analogIO != null)
                            {
                                analogIO.ObjectName = newObjectName;
                            }
                            //xm.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.ObjectName.Equals(tag.Tag)).ObjectName = newObjwctName;
                        }
                    }
                }
                tag.LogicalAddress = IncrementTag(tag.LogicalAddress, shift) + (tag.LogicalAddress.Contains(".") ? "." + tag.LogicalAddress.Split('.')[1] : "");

                if ((ioType == IOType.AnalogInput && tag.Type == IOType.AnalogInput) ||
                    (ioType == IOType.AnalogOutput && tag.Type == IOType.AnalogOutput))
                {
                    tag.Tag = IncrementTagSuffix(tag.Tag, shift < 0 ? shift + 1 : shift);
                }
                xm.LoadedProject.Tags.Add(tag);
                tag.LogicalAddress = "$" + tag.LogicalAddress;
                tag.Tag = "$" + tag.Tag;
                findAndReplaceLogic.ReplaceAllText(oldTagName, tag.Tag, IsProjectUpdate, tag);
                findingAndReplaceAnalogTagFromDevices(oldTagName, tag);
                FindAndReplace._findList.Clear();
                xm.LoadedProject.Tags.Remove(tempTag);
            }
        checkandmodifytags:
            List<XMIOConfig> modifiedtags = xm.LoadedProject.Tags.Where(t => t.Tag.StartsWith("$")).ToList();
            foreach (XMIOConfig tag in modifiedtags)
            {
                XMIOConfig tempTag = new XMIOConfig
                {
                    IoList = tag.IoList,
                    LogicalAddress = tag.LogicalAddress,
                    Tag = tag.Tag,
                    Model = tag.Model,
                    Type = tag.Type
                };
                xm.LoadedProject.Tags.RemoveAll(t => t.LogicalAddress == tempTag.LogicalAddress && t.Model == tempTag.Model);
                xm.LoadedProject.Tags.Add(tempTag);
                var oldTagName = tag.Tag;
                findAndReplaceLogic.blkWiseCount.Clear();
                var findingBlockCount = xm.LoadedProject.Blocks.Count();
                if (findingBlockCount == 1)
                {
                    //findAndReplaceLogic.FindNext(oldTagName, "Current LogicBlock", IsProjectUpdate);
                    findAndReplaceLogic.FindForEntireLogicBlock(oldTagName, IsProjectUpdate);
                }
                else
                {
                    findAndReplaceLogic.FindForEntireLogicBlock(oldTagName, IsProjectUpdate);
                }
                tag.LogicalAddress = tag.LogicalAddress.Replace("$", "");
                tag.Tag = tag.Tag.Replace("$", "");
                xm.LoadedProject.Tags.RemoveAll(t => t.LogicalAddress == tag.LogicalAddress);
                xm.LoadedProject.Tags.Add(tag);
                findAndReplaceLogic.ReplaceAllText(oldTagName, tag.Tag, IsProjectUpdate, tag);
                findingAndReplaceAnalogTagFromDevices(oldTagName, tag);
                FindAndReplace._findList.Clear();
                xm.LoadedProject.Tags.Remove(tempTag);

            }

            if (xm.LoadedProject.Tags.Where(t => t.Tag.StartsWith("$")).Any())
                goto checkandmodifytags;
        }
        //update tag then find old tag used in Devices and replace it with new updated tag
        private void findingAndReplaceAnalogTagFromDevices(string FindcmbText, XMIOConfig tag)
        {
            string ReplaceText = tag.Tag;
            var devices = xm.LoadedProject.Devices;
            foreach (var device in devices)
            {
                string deviceType = device.GetType().Name;
                var deviceProperties = device.GetType().GetProperties();
                foreach (var property in deviceProperties)
                {
                    if (property.PropertyType.IsGenericType &&
                        property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var items = property.GetValue(device) as IEnumerable;
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                ///logicalAddress = "???";
                                string logicalAddress = GetTheLogicalAddressFromTag(item.GetType().GetProperty("Tag").GetValue(item) as string);

                                if (logicalAddress.Contains(":"))
                                {
                                    var tagName = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == logicalAddress).Select(T => T.Tag).FirstOrDefault();
                                }
                                else
                                {
                                    if (logicalAddress == FindcmbText)
                                    {
                                        item.GetType().GetProperty("Tag").SetValue(item, ReplaceText);
                                        var NewTagName = xm.LoadedProject.Tags.Where(T => T.Tag == ReplaceText).Select(T => T.LogicalAddress).FirstOrDefault();
                                        var tagNameProperty = item?.GetType().GetProperty("tagname") ?? item?.GetType().GetProperty("Tag");
                                        if (tagNameProperty != null)
                                        {
                                            string itemTagName = tagNameProperty?.GetValue(item)?.ToString();
                                            if (item.GetType().GetProperty("Variable") != null)
                                                item.GetType().GetProperty("Variable").SetValue(item, NewTagName);
                                        }
                                    }
                                }
                                if (logicalAddress != null && ((logicalAddress == FindcmbText && tag.LogicalAddress == "???") || tag.LogicalAddress != "???"))
                                {
                                    var variableProperty = item.GetType().GetProperty("Variable");
                                    var reqProperty = item.GetType().GetProperty("req");
                                    if (variableProperty != null)
                                    {

                                        if (variableProperty.GetValue(item).ToString() == FindcmbText || item.GetType().GetProperty("Tag").GetValue(item).ToString() == FindcmbText)
                                        {
                                            item.GetType().GetProperty("Variable").SetValue(item, tag.LogicalAddress == "???" ? "" : logicalAddress);
                                            if (tag.LogicalAddress == "???" || tag.LogicalAddress == "" || logicalAddress == "")
                                                item.GetType().GetProperty("Tag").SetValue(item, "");
                                        }
                                    }
                                    else if (reqProperty != null)
                                    {
                                        var tagNameProperty = item?.GetType().GetProperty("tagname") ?? item?.GetType().GetProperty("Tag");
                                        if (tagNameProperty != null)
                                        {
                                            string itemTagName = tagNameProperty?.GetValue(item)?.ToString();
                                            if (itemTagName.Replace("$", "") == tag.LogicalAddress)
                                                tagNameProperty.SetValue(item, tag.LogicalAddress);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //Increament logical address as per PLC model to project update
        private string IncrementTag(string tag, int shift)
        {
            // Split the tag into its prefix and numeric part
            var parts = tag.Split(':');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Tag format is incorrect");
            }
            var prefix = parts[0];
            if (int.TryParse(parts[1].Split('.')[0], out int number))
            {
                // Shift the numeric part
                number += shift;
                return $"{prefix}:{number:D3}"; // D3 ensures the number is zero-padded to 3 digits
            }
            else
            {
                throw new ArgumentException("Tag number part is not a valid integer");
            }
        }
        //Increament Analog address tagname if expansion added in Project
        private string IncrementTagSuffix(string tag, int shift)
        {
            int index = tag.Length - 1;
            while (index >= 0 && !char.IsDigit(tag[index]))
            {
                index--;
            }
            if (index == -1)
            {
                return tag;
            }
            if (index < 0)
            {
                throw new ArgumentException("Tag does not contain a numeric suffix");
            }

            // Extract the numeric part
            int numberEnd = index;
            while (index >= 0 && char.IsDigit(tag[index]))
            {
                index--;
            }

            // Split the tag into prefix, numeric part, and trailing part
            string prefix = tag.Substring(0, index + 1); // Part before the numeric part
            string numberPart = tag.Substring(index + 1, numberEnd - index); // The numeric part
            string trailingPart = tag.Substring(numberEnd + 1); // Any part after the number

            if (int.TryParse(numberPart, out int number))
            {
                // Shift the numeric part
                number += shift;

                // Return the updated tag, combining the prefix, incremented number, and trailing part
                return $"{prefix}{number}{trailingPart}";
            }
            else
            {
                throw new ArgumentException("Tag number part is not a valid integer");
            }
        }
        //Setting the model and model name of tags as per updating PLCModel.
        private void SetModesAndModelOfTags(string updatingModel)
        {
            var IOtags = xm.LoadedProject.Tags.Where(r => r.IoList.ToString() == "OnBoardIO").ToList();
            var digitalInputLabels = new HashSet<string> { "DI0", "DI1", "DI2", "DI3", "DI4", "DI6" };
            var digitalOutputLabels = new HashSet<string> { "DO0", "DO1" };
            foreach (var tag in IOtags)
            {
                tag.Model = updatingModel;
            }
            if (updatingModel == "XM-14-DT-HIO" || updatingModel == "XM-14-DT-HIO-E")
            {
                foreach (var tag in IOtags.OfType<XMIOConfig>())
                {
                    if (string.IsNullOrEmpty(tag.Mode))
                    {
                        if (tag.Type == Core.Types.IOType.DigitalInput && digitalInputLabels.Contains(tag.Label))
                        {
                            tag.Mode = "Digital Input";
                        }
                        else if (tag.Type == Core.Types.IOType.DigitalOutput && digitalOutputLabels.Contains(tag.Label))
                        {
                            tag.Mode = "Digital Output";
                        }
                    }
                }
                //Clearing the Digital Input Filter and Filter Value of DI5 and DI7 
                var onBoardTags = xm.LoadedProject.Tags.Where(t => t.IoList == IOListType.OnBoardIO && (t.Label.Equals("DI5") || t.Label.Equals("DI7"))).ToList();
                foreach (var tag in onBoardTags)
                {
                    tag.IsEnableInputFilter = false;
                    tag.InpuFilterValue = string.Empty;
                }
            }
            else
            {
                foreach (var tag in IOtags.OfType<XMIOConfig>())
                {
                    if (!string.IsNullOrEmpty(tag.Mode) &&
                        (tag.Type == Core.Types.IOType.DigitalInput || tag.Type == Core.Types.IOType.DigitalOutput) &&
                        (digitalInputLabels.Contains(tag.Label) || digitalOutputLabels.Contains(tag.Label)))
                    {
                        tag.Mode = string.Empty;
                    }
                }
                //Adding filter details the Digital Input Filter and Filter Value of DI5 and DI7 
                if (updatingModel == "XM-14-DT" || updatingModel == "XM-17-ADT" || updatingModel == "XM-14-DT-E" || updatingModel == "XM-17-ADT-E")
                {
                    var onBoardTags = xm.LoadedProject.Tags.Where(t => t.IoList == IOListType.OnBoardIO && (t.Label.Equals("DI5") || t.Label.Equals("DI7"))).ToList();
                    foreach (var tag in onBoardTags)
                    {
                        tag.IsEnableInputFilter = true;
                        tag.InpuFilterValue = "10";
                    }
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
