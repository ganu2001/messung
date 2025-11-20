using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;
using XMPS2000.Bacnet;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Types;
using XMPS2000.LadderLogic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMPS2000
{
    public partial class FormBacNet : Form, IXMForm
    {
        private readonly string currentFormName;
        private BacNetIP bacNetIP;
        public bool isAddObject = false;
        private Label labelObjectType;
        public int currentObjectRowIndex = -1;
        private Form frmhardware = null;

        public FormBacNet(string formName, bool stateobject = false)
        {
            InitializeComponent();
            this.currentFormName = formName;
            isAddObject = stateobject;
            // Initialize labelObjectType
            labelObjectType = new Label
            {
                AutoSize = true,
                BackColor = Color.FromArgb(255, 240, 240, 240),
                Location = new Point(10, 10)
            };
            splitContainer1.Panel2.Controls.Add(labelObjectType);
            AddHeaderCheckBox();
            AddHeaderViewCountButton();
        }

        private void AddHeaderViewCountButton()
        {
            System.Windows.Forms.Button buttonViewCount = new System.Windows.Forms.Button();
            buttonViewCount.Size = new Size(50, 20);
            buttonViewCount.BackColor = Color.Transparent;
            buttonViewCount.Text = "Count";
            Point headerCellLocation = dgbacknet.GetCellDisplayRectangle(0, -1, true).Location;

            buttonViewCount.Location = new Point(headerCellLocation.X + 240, headerCellLocation.Y + 1);
            dgbacknet.Controls.Add(buttonViewCount);
            buttonViewCount.Click += (s, e) =>
            {
                BacNetObjectInformation bacNetObjectInformation = new BacNetObjectInformation();
                bacNetObjectInformation.FormBorderStyle = FormBorderStyle.FixedDialog;
                bacNetObjectInformation.ShowDialog();
            };

        }

        private void AddHeaderCheckBox()
        {
            CheckBox headerCheckBox = new CheckBox();
            headerCheckBox.Size = new Size(15, 15);
            headerCheckBox.BackColor = Color.Transparent;
            headerCheckBox.Name = "SelectAllCheckBox";
            Point headerCellLocation = dgbacknet.GetCellDisplayRectangle(0, -1, true).Location;
           // headerCheckBox.Checked = false;
            headerCheckBox.Location = new Point(headerCellLocation.X + 25, headerCellLocation.Y + 4);            
            dgbacknet.Controls.Add(headerCheckBox);
            headerCheckBox.CheckedChanged += HeaderCheckBoxChecked;
           
        }
        private void HeaderCheckBoxChecked(object sender, EventArgs e)
        {

            if (sender is CheckBox checkBox)
            {
                if (XMPS.Instance.PlcStatus != "LogIn")
                {
                    SelectObjectAsPerObject(this.currentFormName, checkBox.Checked);
                }
              
            }
            return;
        }

        private void SelectObjectAsPerObject(string currentFormName, bool isChecked)
        {

            if (!isChecked)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to disable all tags?", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    dgbacknet.Controls.OfType<CheckBox>().FirstOrDefault(c => c.Name == "SelectAllCheckBox").Checked = true;
                    return;
                }
            }

            DeviceModel systemConfiguration = XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
            var ethernetDevices = systemConfiguration.Templates
                ?.Where(template => template.Ethernet != null)
                .ToList();
            var bacNetDevice = ethernetDevices?.SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices)
                               .FirstOrDefault(device => device.Name == "BacNet");

            switch (currentFormName)
            {
                case "BACNET IP":
                    //For BinaryInput Output
                    ChangeBinaryInput(bacNetDevice, isChecked);
                    //For BinaryValue
                    ChangeBinaryValue(bacNetDevice, isChecked);
                    //For AnalogInput Output
                    ChangeAnalogInputOutput(bacNetDevice, isChecked);
                    //For AnalogValue
                    ChangeAnalogValue(bacNetDevice, isChecked);
                    //For Mulitistate 
                    ChangeMultistate(bacNetDevice, isChecked);
                    //For Calendar
                    ChangeCalendar(bacNetDevice, isChecked);
                    //For Schedule
                    ChangeSchedule(bacNetDevice, isChecked);
                    //For Notification
                    ChangeNotification(bacNetDevice, isChecked);
                    break;
                case "Hardware IO's":
                    ChangeBinaryInput(bacNetDevice, isChecked);
                    ChangeAnalogInputOutput(bacNetDevice, isChecked);
                    break;
                case "Binary Value":
                    ChangeBinaryValue(bacNetDevice, isChecked);
                    break;
                case "Analog Value":
                    ChangeAnalogValue(bacNetDevice, isChecked);
                    break;
                case "Multistate Value":
                    ChangeMultistate(bacNetDevice, isChecked);
                    break;
                case "Schedule":
                    ChangeSchedule(bacNetDevice, isChecked);
                    break;
                case "Calendar":
                    ChangeCalendar(bacNetDevice, isChecked);
                    break;
                case "Notification Class":
                    ChangeNotification(bacNetDevice, isChecked);
                    break;
                default:
                    break;

            }
            OnShown();
        }

        private void ChangeNotification(DeviceDetials bacNetDevice, bool isChecked)
        {
            var propertyNotification = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "Notification Class");
            if (propertyNotification == null)
            {
                propertyNotification = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "Notification");
            }
            
            if (isChecked)
            {
                var notificationObjects = bacNetIP.Notifications.Where(t => Convert.ToInt32(t.InstanceNumber) > 0).OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Take(propertyNotification.MaxCount).ToList();
                notificationObjects.ForEach(t => t.IsEnable = isChecked);
                propertyNotification.CurrentCount = notificationObjects.Count;
            }
            else
            {
                var notificationObjects = bacNetIP.Notifications.Where(t => Convert.ToInt32(t.InstanceNumber) > 0).OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
                notificationObjects.ForEach(t => t.IsEnable = isChecked);
                propertyNotification.CurrentCount = 0;
            }
        }

        private void ChangeSchedule(DeviceDetials bacNetDevice, bool isChecked)
        {
            var propertySchedule = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "Schedule");
            if (isChecked)
            {
                var scheduleObjects = bacNetIP.Schedules.OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Take(propertySchedule.MaxCount).ToList();
                scheduleObjects.ForEach(t => t.IsEnable = isChecked);
                propertySchedule.CurrentCount = scheduleObjects.Count;
            }
            else
            {
                var scheduleObjects = bacNetIP.Schedules.ToList();
                scheduleObjects.ForEach(t => t.IsEnable = isChecked);
                propertySchedule.CurrentCount = 0;
            }
        }

        private void ChangeCalendar(DeviceDetials bacNetDevice, bool isChecked)
        {
            var propertyCalendar = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "Calendar");
            if (isChecked)
            {
                var calendarObjects = bacNetIP.Calendars.OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Take(propertyCalendar.MaxCount).ToList();
                calendarObjects.ForEach(t => t.IsEnable = isChecked);
                propertyCalendar.CurrentCount = calendarObjects.Count;
            }
            else
            {
                var calendarObjects = bacNetIP.Calendars.ToList();
                calendarObjects.ForEach(t => t.IsEnable = isChecked);
                propertyCalendar.CurrentCount = 0;
            }
        }

        private void ChangeMultistate(DeviceDetials bacNetDevice, bool isChecked)
        {
            var propertyMultistate = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "MultiStateValue");
            if (isChecked)
            {
                var allMultistateValues = bacNetIP.MultistateValues.Where(t => t.ObjectType.Split(':')[0].Equals("19")).OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
                int alreadyEnabledCount = allMultistateValues.Count(t => t.IsEnable);
                int remainingSlots = propertyMultistate.MaxCount - alreadyEnabledCount;

                if (remainingSlots > 0)
                {
                    var tagsToEnable = allMultistateValues.Where(t => !t.IsEnable).Take(remainingSlots).ToList();

                    tagsToEnable.ForEach(t => t.IsEnable = true);
                    propertyMultistate.CurrentCount = alreadyEnabledCount + tagsToEnable.Count;
                }
                else
                {
                    propertyMultistate.CurrentCount = Math.Min(alreadyEnabledCount, propertyMultistate.MaxCount);
                }
            }
            else
            {
                var multistateObjects = bacNetIP.MultistateValues.Where(t => t.ObjectType.Split(':')[0].Equals("19")).ToList();
                multistateObjects.ForEach(t => t.IsEnable = false);
                propertyMultistate.CurrentCount = 0;
            }

        }

        private void ChangeAnalogValue(DeviceDetials bacNetDevice, bool isChecked)
        {
            var propertyAnalogValue = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "AnalogValue");
            if (isChecked)
            {
                var allAnalogValues = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("2")).OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
                int alreadyEnabledCount = allAnalogValues.Count(t => t.IsEnable);
                int remainingSlots = propertyAnalogValue.MaxCount - alreadyEnabledCount;
                if (remainingSlots > 0)
                {
                    var tagsToEnable = allAnalogValues.Where(t => !t.IsEnable).Take(remainingSlots).ToList();
                    tagsToEnable.ForEach(t => t.IsEnable = true);
                    propertyAnalogValue.CurrentCount = alreadyEnabledCount + tagsToEnable.Count;
                }
                else
                {
                    propertyAnalogValue.CurrentCount = Math.Min(alreadyEnabledCount, propertyAnalogValue.MaxCount);
                }
            }
            else
            {
                var analogValues = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("2")).ToList();
                analogValues.ForEach(t => t.IsEnable = false);
                propertyAnalogValue.CurrentCount = 0;
            }
        }

        private void ChangeAnalogInputOutput(DeviceDetials bacNetDevice, bool isChecked)
        {
            var propertyAnalogInput = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "AnalogInput");
            if (isChecked)
            {
                var analogInputs = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("0"))
                              .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Take(propertyAnalogInput.MaxCount).ToList();
                analogInputs.ForEach(t => t.IsEnable = isChecked);
                propertyAnalogInput.CurrentCount = analogInputs.Count;
            }
            else
            {
                var analogInputs = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("0")).ToList();
                analogInputs.ForEach(t => t.IsEnable = isChecked);
                propertyAnalogInput.CurrentCount = 0;
            }

            var propertyAnalogOutput = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "AnalogOutput");
            if (isChecked)
            {
                var analogOutputs = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("1"))
                                  .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Take(propertyAnalogOutput.MaxCount).ToList();
                analogOutputs.ForEach(t => t.IsEnable = isChecked);
                propertyAnalogOutput.CurrentCount = analogOutputs.Count;
            }
            else
            {
                var analogOutputs = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("1")).ToList();
                analogOutputs.ForEach(t => t.IsEnable = isChecked);
                propertyAnalogOutput.CurrentCount = 0;
            }

        }

        private void ChangeBinaryValue(DeviceDetials bacNetDevice, bool isChecked)
        {
            var propertyBinaryValue = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "BinaryValue");
            if (isChecked)
            {
                var allBinaryValues = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("5")).OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
                int alreadyEnabledCount = allBinaryValues.Count(t => t.IsEnable);
                int remainingSlots = propertyBinaryValue.MaxCount - alreadyEnabledCount;
                if (remainingSlots > 0)
                {
                    var tagsToEnable = allBinaryValues.Where(t => !t.IsEnable).Take(remainingSlots).ToList();
                    tagsToEnable.ForEach(t => t.IsEnable = true);
                    propertyBinaryValue.CurrentCount = alreadyEnabledCount + tagsToEnable.Count;
                }
                else
                {
                    propertyBinaryValue.CurrentCount = Math.Min(alreadyEnabledCount, propertyBinaryValue.MaxCount);
                }
            }
            else
            {
                var binaryValues = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("5")).ToList();
                binaryValues.ForEach(t => t.IsEnable = false);
                propertyBinaryValue.CurrentCount = 0;
            }
        }

        private void ChangeBinaryInput(DeviceDetials bacNetDevice, bool isChecked)
        {
            var propertyBinaryInput = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "BinaryInput");
            if (isChecked)
            {
                var binaryInputs = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("3"))
                              .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Take(propertyBinaryInput.MaxCount).ToList();
                binaryInputs.ForEach(t => t.IsEnable = isChecked);
                propertyBinaryInput.CurrentCount = binaryInputs.Count;
            }
            else
            {
                var binaryInputs = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("3")).ToList();
                binaryInputs.ForEach(t => t.IsEnable = isChecked);
                propertyBinaryInput.CurrentCount = 0;
            }

            var propertyBinaryOutput = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "BinaryOutput");
            if (isChecked)
            {
                var binaryOutputs = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("4"))
                              .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Take(propertyBinaryOutput.MaxCount).ToList();
                binaryOutputs.ForEach(t => t.IsEnable = isChecked);
                propertyBinaryOutput.CurrentCount = binaryOutputs.Count;
            }
            else
            {
                var binaryOutputs = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("4")).ToList();
                binaryOutputs.ForEach(t => t.IsEnable = isChecked);
                propertyBinaryOutput.CurrentCount = 0;
            }
        }
        private void dgbacknet_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgbacknet.IsCurrentCellDirty && dgbacknet.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgbacknet.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        public void OnShown()
        {
            bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null) bacNetIP = new BacNetIP();

            //Creating None type notification object for showing in notification dropdown.
            if (!bacNetIP.Notifications.Any(t => t.InstanceNumber.Equals("0") && t.ObjectName.Equals("None")))
            {
                Notification noneOption = new Notification { ObjectName = "None", InstanceNumber = "0", IsEnable = false };
                noneOption.Recipients = new List<Recipient>();
                bacNetIP.Notifications.Insert(0, noneOption);
            }
            //Getting check box form the dgbacknet controls.
            CheckBox checkBox = dgbacknet.Controls.OfType<CheckBox>().FirstOrDefault(c => c.Name == "SelectAllCheckBox");
            int isEnableCount = 0;
            //Maintain the previous Scrollbar Position.
            int previousScrollPosition = dgbacknet.FirstDisplayedScrollingRowIndex;
            dgbacknet.Rows.Clear();
            var bacNetTagFactory = new BacNetTagFactory();
            if (currentFormName.Equals("BACNET IP") || currentFormName.Equals("Device"))
            {
                dgbacknet.Rows.Add(bacNetIP.Device.IsEnable, $"{bacNetIP.Device.ObjectName} ({bacNetIP.Device.InstanceNumber})", "Device");
            }
            object[] networkPortRow = null;
            if (bacNetIP.NetworkPort != null && (currentFormName.Equals("BACNET IP") || currentFormName.Equals("Network Port")))
            {
                string networkPortName = string.IsNullOrEmpty(bacNetIP.NetworkPort.ObjectName)? "Network_port": (bacNetIP.NetworkPort.ObjectName.Equals("Network Port", StringComparison.OrdinalIgnoreCase)
                        ? "Network_Port": bacNetIP.NetworkPort.ObjectName);
                networkPortRow = new object[]
                {
                    bacNetIP.NetworkPort.IsEnable, $"{networkPortName} ({bacNetIP.NetworkPort.InstanceNumber})", "NetworkPort"
                };
            }
            object tags = bacNetTagFactory.GetBacNetTags(currentFormName);
            if (tags is List<XMIOConfig>)
            {
                foreach (XMIOConfig tag in tags as List<XMIOConfig>)
                {
                    string logicalAddress = tag.LogicalAddress;
                    string objetType = GetObjectType(tag);
                    bool isEnable = CheckIsEnable(logicalAddress, objetType);
                    if (isEnable)
                        isEnableCount++;
                    string instanceNumber = GetInstanceNumberFromObjectName(logicalAddress, objetType);
                    dgbacknet.Rows.Add(isEnable, tag.Tag.ToString() + $"({instanceNumber})", objetType, GetAbbrivations(objetType, instanceNumber));
                }
            }
            else
            {
                foreach (string objectName in tags as HashSet<string>)
                {
                    string objetType = objectName.Contains("$") ? objectName.Split('$')[1].ToString() : currentFormName;
                    string objectText = objectName.Contains("$") ? objectName.Split('$')[0].ToString() : objectName;

                    XMIOConfig Taginfo = XMPS.Instance.LoadedProject.Tags.Where(t => t.Tag == objectText).FirstOrDefault();
                    if (Taginfo != null)
                    {
                        objetType = GetObjectType(Taginfo);
                        objectText = Taginfo.LogicalAddress.ToString();
                    }
                    bool isEnable = CheckIsEnable(objectText, objetType);
                    if (isEnable)
                        isEnableCount++;
                    string instanceNumber = GetInstanceNumberFromObjectName(objectText, objetType);
                    if (objectText == "Notification" || objectText == "Notification Class")
                    {
                        objectText = "Notification_Class";
                    }
                    dgbacknet.Rows.Add(isEnable, Taginfo == null ? objectText + $"({instanceNumber})" : Taginfo.Tag + $"({instanceNumber})", objetType, GetAbbrivations(objetType, instanceNumber));
                }
            }
            if (networkPortRow != null)
            {
                dgbacknet.Rows.Add(networkPortRow);
            }
            ChangeSelectAllCheckBoxCond();

            // for reopen the property window for particular selected bacnet object.
            if (currentObjectRowIndex > -1 && (dgbacknet.Rows.Count > currentObjectRowIndex))
            {
                dgbacknet.CurrentCell = dgbacknet.Rows[currentObjectRowIndex].Cells[1];
                DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(1, currentObjectRowIndex);
                if (previousScrollPosition >= 0 && previousScrollPosition < dgbacknet.Rows.Count)
                {
                    dgbacknet.FirstDisplayedScrollingRowIndex = previousScrollPosition;
                }
                updateLabel(args);
            }
            else
            {
                splitContainer1.Panel2.Controls.Clear();
            }
        }
        private void updateLabel(DataGridViewCellEventArgs args)
        {

            string currentObjectType = dgbacknet.Rows[args.RowIndex].Cells[2].Value.ToString();
            if (labelObjectType == null)
            {
                labelObjectType = new Label
                {
                    AutoSize = true,
                    BackColor = Color.FromArgb(255, 240, 240, 240),
                    Location = new Point(10, 10),

                };
                splitContainer1.Panel2.Controls.Add(labelObjectType);
            }

            labelObjectType.Text = currentObjectType;
            labelObjectType.Visible = true; // Explicitly set visible when updating

            if (splitContainer1.Panel2.Controls.Count > 0)
            {
                if (dgbacknet.CurrentCell.Value == null) return;
                BacNetFormFactory formFactory = new BacNetFormFactory();
                string cellValue = dgbacknet.CurrentCell.Value.ToString();
                cellValue = GetObjectNameWithouInstanceNumber(cellValue);

                // Call cellClick directly to show the form
                dgbacknet_CellClick(dgbacknet, args);
            }
            labelObjectType.BringToFront();
        }
        private object GetAbbrivations(string objetType, string instanceNumber)
        {
            switch (objetType)
            {
                case "BinaryInput":
                    return "BI:" + instanceNumber;
                case "BinaryOutput":
                    return "BO:" + instanceNumber;
                case "AnalogInput":
                    return "AI:" + instanceNumber;
                case "AnalogOutput":
                    return "AO:" + instanceNumber;
                case "AnalogValue":
                    return "AV:" + instanceNumber;
                case "BinaryValue":
                    return "BV:" + instanceNumber;
                case "Schedule":
                    return "SCH:" + instanceNumber;
                case "Calendar":
                    return "CAL:" + instanceNumber;
                case "Notification Class":
                    return "NC:" + instanceNumber;
                case "MultiStateValue":
                    return "MV:" + instanceNumber;
                default:
                    return "";
            }
        }
        private string GetInstanceNumberFromObjectName(string objectText, string objetType)
        {
            switch (objetType)
            {
                case "AnalogInput":
                case "AnalogOutput":
                case "AnalogValue":
                    var analogObject = bacNetIP.AnalogIOValues.Where(t => t.LogicalAddress.Equals(objectText)).FirstOrDefault();
                    return analogObject != null ? analogObject.InstanceNumber : string.Empty;
                case "BinaryInput":
                case "BinaryOutput":
                case "BinaryValue":
                    var binaryObject = bacNetIP.BinaryIOValues.Where(t => t.LogicalAddress.Equals(objectText)).FirstOrDefault();
                    return binaryObject != null ? binaryObject.InstanceNumber : string.Empty;
                case "MultiStateValue":
                    var multistateObject = bacNetIP.MultistateValues.Where(t => t.LogicalAddress.Equals(objectText)).FirstOrDefault();
                    return multistateObject != null ? multistateObject.InstanceNumber : string.Empty; ;
                case "Device":
                    return bacNetIP.Device != null ? bacNetIP.Device.InstanceNumber : string.Empty;
                case "Network Port":   
                    return bacNetIP.NetworkPort != null ? bacNetIP.NetworkPort.InstanceNumber : string.Empty;
                case "Schedule":
                    var scheduleObject = bacNetIP.Schedules.Where(t => t.ObjectName.Equals(objectText)).FirstOrDefault();
                    return scheduleObject != null ? scheduleObject.InstanceNumber : string.Empty;
                case "Calendar":
                    var calendarObject = bacNetIP.Calendars.Where(t => t.ObjectName.Equals(objectText)).FirstOrDefault();
                    return calendarObject != null ? calendarObject.InstanceNumber : string.Empty;
                case "Notification Class":
                    var notificationObject = bacNetIP.Notifications.Where(t => t.ObjectName.Equals(objectText)).FirstOrDefault();
                    return notificationObject != null ? notificationObject.InstanceNumber : string.Empty;
                default:
                    return string.Empty;
            }
        }
        private bool CheckIsEnable(string logicalAddress, string objetType)
        {
            switch (objetType)
            {
                case "AnalogInput":
                case "AnalogOutput":
                case "AnalogValue":
                    var analogObject = bacNetIP.AnalogIOValues.Where(t => t.LogicalAddress.Equals(logicalAddress)).FirstOrDefault();
                    return analogObject != null ? analogObject.IsEnable : true;
                case "BinaryInput":
                case "BinaryOutput":
                case "BinaryValue":
                    var binaryObject = bacNetIP.BinaryIOValues.Where(t => t.LogicalAddress.Equals(logicalAddress)).FirstOrDefault();
                    return binaryObject != null ? binaryObject.IsEnable : true;
                case "MultiStateValue":
                    var multistateObject = bacNetIP.MultistateValues.Where(t => t.LogicalAddress.Equals(logicalAddress)).FirstOrDefault();
                    return multistateObject != null ? multistateObject.IsEnable : true;
                case "Device":
                    return bacNetIP.Device != null ? bacNetIP.Device.IsEnable : true;
                case "Network Port":   
                    return bacNetIP.NetworkPort != null ? bacNetIP.NetworkPort.IsEnable : true;
                case "Schedule":
                    var scheduleObject = bacNetIP.Schedules.Where(t => t.ObjectName.Equals(logicalAddress)).FirstOrDefault();
                    return scheduleObject != null ? scheduleObject.IsEnable : true;
                case "Calendar":
                    var calendarObject = bacNetIP.Calendars.Where(t => t.ObjectName.Equals(logicalAddress)).FirstOrDefault();
                    return calendarObject != null ? calendarObject.IsEnable : true;
                case "Notification Class":
                    var notificationObject = bacNetIP.Notifications.Where(t => t.ObjectName.Equals(logicalAddress)).FirstOrDefault();
                    return notificationObject != null ? notificationObject.IsEnable : true;
                default:
                    return true;
            }
        }
        private string GetObjectType(XMIOConfig tag)
        {
            switch (tag.Type)
            {
                case IOType.AnalogInput:
                    return "AnalogInput";
                case IOType.AnalogOutput:
                    return "AnalogOutput";
                case IOType.DigitalInput:
                    return "BinaryInput";
                case IOType.DigitalOutput:
                    return "BinaryOutput";
                case IOType.UniversalInput:
                case IOType.UniversalOutput:
                    return GetObjectTypeForUniversal(tag.LogicalAddress);
                case IOType.DataType:
                    if (tag.LogicalAddress.StartsWith("F2"))
                        return "BinaryValue";
                    if ((tag.LogicalAddress.StartsWith("P5") || tag.LogicalAddress.StartsWith("W4")) && bacNetIP.AnalogIOValues.Where(t => t.LogicalAddress.Equals(tag.LogicalAddress)).Count() > 0)
                        return "AnalogValue";
                    if (tag.LogicalAddress.StartsWith("W4"))
                        return "MultiStateValue";
                    else
                        return string.Empty;
                default:
                    return string.Empty;
            }
        }
        private string GetObjectTypeForUniversal(string logicalAddress)
        {
            if (bacNetIP.BinaryIOValues.Where(t => t.LogicalAddress.Equals(logicalAddress)).Any())
                return logicalAddress.StartsWith("I") ? "BinaryInput" : "BinaryOutput";
            else
                return logicalAddress.StartsWith("I") ? "AnalogInput" : "AnalogOutput";
        }
        private void dgbacknet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3) return;

            bool proceed = BacNetValidator.CheckAndPromptSaveChanges();
            if (!proceed) return;
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                // Refresh the grid to update the visual state of the checkbox
                bool currentValue = (bool)dgbacknet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string objectName = dgbacknet.Rows[e.RowIndex].Cells[1].Value.ToString();
                objectName = GetObjectNameWithouInstanceNumber(objectName);
                string objectType = dgbacknet.Rows[e.RowIndex].Cells[2].Value.ToString();
                // Filter Ethernet devices
                DeviceModel systemConfiguration = XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
                var ethernetDevices = systemConfiguration.Templates
                    ?.Where(template => template.Ethernet != null)
                    .ToList();
                var bacNetDevice = ethernetDevices?.SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices)
                                   .FirstOrDefault(device => device.Name == "BacNet");
                if (!objectType.Equals("Device") && !objectType.Equals("NetworkPort"))
                {
                    var propertyToUpdate = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == objectType);
                    if (propertyToUpdate == null)
                    {
                        propertyToUpdate = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "Notification" || p.Name == "Notification_Class");
                    }
                    if (currentValue)
                    {
                        if (XMPS.Instance.PlcStatus=="LogIn")
                        {

                            return;
                        }
                        var result = MessageBox.Show("Are you sure you want to disable this tag?", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                        if (propertyToUpdate != null)
                        {
                            propertyToUpdate.CurrentCount -= 1;
                        }
                        ChangeEnableState(objectName, objectType, false);
                          
                    }
                    else
                    {
                        if (propertyToUpdate.CurrentCount >= propertyToUpdate.MaxCount)
                        {
                            MessageBox.Show($"Maximum limit reached for {objectType}.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (propertyToUpdate != null)
                        {
                            if (XMPS.Instance.PlcStatus == "LogIn")
                            {

                                return;
                            }
                            propertyToUpdate.CurrentCount += 1;
                        }
                    }
                    // Toggle the checkbox value
                    dgbacknet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !(bool)dgbacknet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    bool isChecked = (bool)dgbacknet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    //device checkbox  user should not allowed to uncheck it
                    dgbacknet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = objectType.Equals("Device") ? true : isChecked;
                    ChangeEnableState(objectName, objectType, isChecked);
                    splitContainer1.Panel2.Controls.Clear();
                    dgbacknet.RefreshEdit();
                    //Changing the SelectAllObject Check box condition.
                    ChangeSelectAllCheckBoxCond();
                }
            }
            if (dgbacknet.CurrentCell == null || dgbacknet.CurrentCell.Value == null || dgbacknet.CurrentCell.ColumnIndex != 1 || e.RowIndex == -1)
                return;
            splitContainer1.Panel2.Controls.Clear();
            if (frmhardware != null && !frmhardware.IsDisposed)
            {
                frmhardware.Dispose();
            }
            BacNetFormFactory formFactory = new BacNetFormFactory();
            string cellValue = dgbacknet.CurrentCell.Value.ToString();
            cellValue = GetObjectNameWithouInstanceNumber(cellValue);
            string currentObjectType = dgbacknet.Rows[e.RowIndex].Cells[2].Value.ToString();
            frmhardware = formFactory.GetForm(cellValue, currentObjectType);
            frmhardware.TopLevel = false;
            frmhardware.Dock = DockStyle.Fill;
            frmhardware.AutoScroll = true;

            labelObjectType = new Label
            {
                Text = currentObjectType,
                AutoSize = true,
                BackColor = Color.FromArgb(255, 240, 240, 240),
                Location = new Point(10, 10),
                Font = new System.Drawing.Font(labelObjectType?.Font.FontFamily ?? FontFamily.GenericSansSerif, 8, FontStyle.Bold)
            };
            splitContainer1.Panel2.Controls.Add(labelObjectType);

            frmhardware.Show();
            splitContainer1.Panel2.Controls.Add(frmhardware);
            XMPS.Instance.LoadedProject._previousIndex = dgbacknet.CurrentCell.RowIndex;
            currentObjectRowIndex = e.ColumnIndex == 1 ? e.RowIndex : currentObjectRowIndex;
            frmhardware.Show();
        }

        private void ChangeSelectAllCheckBoxCond()
        {
            CheckBox checkBox = dgbacknet.Controls.OfType<CheckBox>().FirstOrDefault(c => c.Name == "SelectAllCheckBox");
            if (checkBox == null) return;

            checkBox.CheckedChanged -= HeaderCheckBoxChecked;
            DeviceModel systemConfiguration = XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
            var ethernetDevices = systemConfiguration.Templates?.Where(template => template.Ethernet != null).ToList();
            var bacNetDevice = ethernetDevices?.SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "BacNet");
            switch (this.currentFormName)
            {
                case "Schedule":
                    checkBox.Checked = bacNetIP.Schedules.Count > 0 && bacNetIP.Schedules.All(t => t.IsEnable);
                    break;
                case "Calendar":
                    checkBox.Checked = bacNetIP.Calendars.Count > 0 && bacNetIP.Calendars.All(t => t.IsEnable);
                    break;
                case "Notification Class":
                    var validNotifications = bacNetIP.Notifications
                        .Where(t => Convert.ToInt32(t.InstanceNumber) > 0)
                        .ToList();

                    checkBox.Checked = validNotifications.Count > 0 && validNotifications.All(t => t.IsEnable);
                    break;
                case "Binary Value":
                    var propertyBinaryValue = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "BinaryValue");
                    var binaryVals = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("5")).ToList();
                    var enabledBinaryCount = binaryVals.Count(t => t.IsEnable);
                    var maxAllowed = Math.Min(propertyBinaryValue.MaxCount, binaryVals.Count);
                    checkBox.Checked = enabledBinaryCount >= maxAllowed && maxAllowed > 0;
                    break;
                case "Analog Value":
                    var propertyAnalogValue = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "AnalogValue");
                    var analogVals = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("2")).ToList();
                    var enabledAnalogCount = analogVals.Count(t => t.IsEnable);
                    var maxAnalogAllowed = Math.Min(propertyAnalogValue.MaxCount, analogVals.Count);
                    checkBox.Checked = enabledAnalogCount >= maxAnalogAllowed && maxAnalogAllowed > 0;
                    break;
                case "Hardware IO's":
                    var binaryIO = bacNetIP.BinaryIOValues.Where(t =>
                        t.ObjectType.Split(':')[0].Equals("3")|| t.ObjectType.Split(':')[0].Equals("4")).ToList();
                    var analogIO = bacNetIP.AnalogIOValues.Where(t =>
                        t.ObjectType.Split(':')[0].Equals("0")|| t.ObjectType.Split(':')[0].Equals("1")).ToList();

                    bool allBinaryIOEnabled = binaryIO.Count == 0 || binaryIO.All(t => t.IsEnable);
                    bool allAnalogIOEnabled = analogIO.Count == 0 || analogIO.All(t => t.IsEnable);
                    checkBox.Checked = allBinaryIOEnabled && allAnalogIOEnabled;
                    break;
                case "Multistate Value":
                    var propertyMultistate = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == "MultiStateValue");
                    var multistateVals = bacNetIP.MultistateValues.Where(t => t.ObjectType.Split(':')[0].Equals("19")).ToList();
                    var enabledMultistateCount = multistateVals.Count(t => t.IsEnable);
                    var maxMultistateAllowed = Math.Min(propertyMultistate.MaxCount, multistateVals.Count);
                    checkBox.Checked = enabledMultistateCount >= maxMultistateAllowed && maxMultistateAllowed > 0;
                    break;
            }
            checkBox.CheckedChanged += HeaderCheckBoxChecked;
        }

        internal void RefreshGridView()
        {
            if (labelObjectType != null)
            {
                labelObjectType.Visible = false;
            }
            OnShown();
        }
        private void ChangeEnableState(string objectName, string objectType, bool isChecked)
        {
            switch (objectType)
            {
                case "AnalogInput":
                case "AnalogOutput":
                case "AnalogValue":
                    var analogObject = bacNetIP.AnalogIOValues.FirstOrDefault(t => t.ObjectName.Equals(objectName));
                    if (analogObject != null)
                    {
                        analogObject.IsEnable = isChecked;
                    }
                    break;

                case "BinaryInput":
                case "BinaryOutput":
                case "BinaryValue":
                    var binaryObject = bacNetIP.BinaryIOValues.FirstOrDefault(t => t.ObjectName.Equals(objectName));
                    if (binaryObject != null)
                    {
                        binaryObject.IsEnable = isChecked;
                    }
                    break;

                case "MultiStateValue":
                    var multistateObject = bacNetIP.MultistateValues.FirstOrDefault(t => t.ObjectName.Equals(objectName));
                    if (multistateObject != null)
                    {
                        multistateObject.IsEnable = isChecked;
                    }
                    break;
                case "Device":
                    bacNetIP.Device.IsEnable = true;
                    break;
                case "Network Port":                  
                    bacNetIP.NetworkPort.IsEnable = true; 
                    break;
                case "Schedule":
                    var scheduleObject = bacNetIP.Schedules.FirstOrDefault(t => t.ObjectName.Equals(objectName));
                    if (scheduleObject != null)
                    {
                        scheduleObject.IsEnable = isChecked;
                    }
                    break;
                case "Calendar":
                    var calendarObject = bacNetIP.Calendars.FirstOrDefault(t => t.ObjectName.Equals(objectName));
                    if (calendarObject != null)
                    {
                        calendarObject.IsEnable = isChecked;
                    }
                    break;
                case "Notification Class":
                    var notificationObject = bacNetIP.Notifications.FirstOrDefault(t => t.ObjectName.Equals(objectName));
                    if (notificationObject != null)
                    {
                        notificationObject.IsEnable = isChecked;
                    }
                    break;

                default:
                    break;
            }
        }
        private void dgbacknet_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                if (XMPS.Instance.PlcStatus=="LogIn")
                {
                    return;
                }
                //if there is not any cell selected.
                if (dgbacknet.CurrentCell == null || dgbacknet.CurrentCell.ColumnIndex == 3)
                    return;
                string tag = dgbacknet.CurrentCell.Value.ToString();
                string objectType = dgbacknet.CurrentRow.Cells[2].Value.ToString();
                if (dgbacknet.CurrentCell.ColumnIndex == 1)
                    tag = GetObjectNameWithouInstanceNumber(tag);
                XMIOConfig ioconfig = XMProValidator.GetTagFromTagName(tag);
                if ((ioconfig != null && ioconfig.Type == Core.Types.IOType.DataType) || (dgbacknet.Rows[dgbacknet.CurrentCell.RowIndex].Cells[2].Value.ToString() == "Notification Class" && dgbacknet.CurrentCell.ColumnIndex == 1))
                    cntxForBacnet.Show(dgbacknet, new Point(e.X, e.Y));
                else if ((objectType.Equals("Schedule") || objectType.Equals("Calendar")) && dgbacknet.CurrentCell.ColumnIndex == 1)
                    cntxForBacnet.Show(dgbacknet, new Point(e.X, e.Y));
            }
        }
        private string GetObjectNameWithouInstanceNumber(string tag)
        {
            int lastBracketIndex = tag.LastIndexOf('(');
            tag = tag.Substring(0, lastBracketIndex);
            return tag;
        }
        private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string objectType = dgbacknet.CurrentRow.Cells[2].Value.ToString();
            string objectName = dgbacknet.CurrentCell.Value.ToString();
            bool isChecked = (bool)dgbacknet.CurrentRow.Cells[0].Value;
            DeleteBacNetObject(objectType, objectName);
            if (isChecked)
            {
                DeviceModel systemConfiguration = XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
                var ethernetDevices = systemConfiguration.Templates
                    ?.Where(template => template.Ethernet != null)
                    .ToList();
                var bacNetDevice = ethernetDevices?.SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices)
                                   .FirstOrDefault(device => device.Name == "BacNet");

                var propertyToUpdate = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == objectType);
                if (propertyToUpdate != null)
                {
                    propertyToUpdate.CurrentCount -= 1;
                }
            }
            XMPS.Instance.LoadedProject.isChanged = false;
            splitContainer1.Panel2.Controls.Clear();
            currentObjectRowIndex = -1;
            OnShown();
        }
        public void IsusedInMultiSatate()
        {
            int value = dgbacknet.CurrentRow.Index;
            foreach (var record in XMPS.Instance.LoadedProject.BacNetIP.MultistateValues)
            {
                if (record.BinaryValue == value + 1)
                {
                    record.BinaryValue = 0;
                    break;
                }
            }
        }
        public void IsusedInAnalog()
        {
            int value = dgbacknet.CurrentRow.Index;
            foreach (var record in XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues)
            {
                if (record.BinaryAValue == value + 1)
                {
                    record.BinaryAValue = 0;
                    break;
                }
            }
        }
        private void DeleteBacNetObject(string objectType, string objectName)
        {
            objectName = GetObjectNameWithouInstanceNumber(objectName);
            switch (objectType)
            {
                case "BinaryValue":
                    bacNetIP.BinaryIOValues.RemoveAll(t => t.ObjectName == objectName);
                    RemoveXMConfigTag(objectName);
                    IsusedInAnalog();
                    IsusedInMultiSatate();
                    break;
                case "AnalogValue":
                    bacNetIP.AnalogIOValues.RemoveAll(t => t.ObjectName == objectName);
                    RemoveXMConfigTag(objectName);
                    break;
                case "MultiStateValue":
                    bacNetIP.MultistateValues.RemoveAll(t => t.ObjectName == objectName);
                    RemoveXMConfigTag(objectName);
                    break;
                case "Schedule":
                    bacNetIP.Schedules.RemoveAll(t => t.ObjectName == objectName);
                    break;
                case "Notification Class":
                    var notification = bacNetIP.Notifications.FirstOrDefault(t => t.ObjectName == objectName);
                    var usedList = CheckNotificationObjectUses(notification.InstanceNumber.ToString());
                    if (usedList.Any())
                    {
                        var message = $"Notification object \"{objectName}\" is already used in:\n\n";

                        foreach (var item in usedList)
                        {
                            message += $"{item.Item2},\n";
                        }

                        MessageBox.Show(message, "XMPS2000",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        bacNetIP.Notifications.RemoveAll(t => t.ObjectName == objectName);
                    break;
                case "Calendar":
                    var calendar = bacNetIP.Calendars.Where(t => t.ObjectName == objectName).FirstOrDefault();
                    bool isUsedInSchedule = bacNetIP.Schedules.Any(schedule => schedule.specialEvents != null &&
                                        schedule.specialEvents.OfType<CalendarReference>().Any(specialEvent =>
                                        specialEvent.CalendarObjectName == objectName));
                    if (!isUsedInSchedule)
                        bacNetIP.Calendars.RemoveAll(t => t.ObjectName == objectName);
                    else
                        MessageBox.Show($"{objectName} these calendar object already used in schedule", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    break;
            }
        }
        private void RemoveXMConfigTag(string objectName)
        {
            DialogResult result = MessageBox.Show("Object is Deleted you want to delete also form user definded tags", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
                ((frmGridLayout)this.Parent.Controls["frmGridLayout"]).DeleteFromBacNetObject(objectName);
        }

        private List<(string, string)> CheckNotificationObjectUses(string instanceNumber)
        {
            var result = new List<(string, string)>();

            var analogObjects = bacNetIP.AnalogIOValues.Where(t => t.NotificationClass.ToString().Equals(instanceNumber));
            foreach (var obj in analogObjects)
                result.Add((obj.ObjectType.Split(':')[1], obj.ObjectName));
            var binaryObjects = bacNetIP.BinaryIOValues.Where(t => t.NotificationClass.ToString().Equals(instanceNumber));
            foreach (var obj in binaryObjects)
                result.Add((obj.ObjectType.Split(':')[1], obj.ObjectName));
            var multistateObjects = bacNetIP.MultistateValues.Where(t => t.NotificationClass.ToString().Equals(instanceNumber));
            foreach (var obj in multistateObjects)
                result.Add((obj.ObjectType.Split(':')[1], obj.ObjectName));

            return result;
        }
    }
}