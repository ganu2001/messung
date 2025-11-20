using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Configuration;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.LadderLogic;
using XMPS2000.Resource_File;
using XMPS2000.UndoRedoGridLayout;

namespace XMPS2000
{
    public partial class ModbusTCPClientUserControl : UserControl
    {
        private MODBUSTCPClient_Slave dataSource;
        private string _slaveName;
        private string currentLogicalAddress;
        XMPS xm;
        //UndoRedo
        private ModbusTCPClientManager ModbusTCPClientManager = new ModbusTCPClientManager();
        private bool isEdit = false;
        private MODBUSTCPClient_Slave oldSlavedate;
        private string selectedMultiplicationFactor;
        public ModbusTCPClientUserControl()
        {
            xm = XMPS.Instance;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            InitializeComponent();
            ////Change names of Labels, take names from LabelNames Resource file
            this.lblSlaveIpAdd.Text = LabelNames.SlaveIpAdd;
            this.lblPort.Text = LabelNames.port;
            this.lblPortRange.Text = LabelNames.PortRange;
            this.lblPolling.Text = LabelNames.polling;
            this.lblPollingRange.Text = LabelNames.pollingrange;
            this.lblDeviceId.Text = LabelNames.DeviceId;
            this.lblDeviceIdRange.Text = LabelNames.DeviceIdRange;
            this.lblAddress.Text = LabelNames.Address;
            this.lblAddressRange.Text = LabelNames.AddressRange;
            this.lblLength.Text = LabelNames.Length;
            this.lblLengthRange.Text = LabelNames.LengthRange;
            this.lblVariable.Text = LabelNames.Variable;
            this.lblFunctionCode.Text = LabelNames.FunctionCode;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List;           
        }

        public ModbusTCPClientUserControl(string slaveName)
        {
            xm = XMPS.Instance;
            _slaveName = slaveName;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            InitializeComponent();
            ////Change names of Labels, take names from LabelNames Resource file
            this.lblSlaveIpAdd.Text = LabelNames.SlaveIpAdd;
            this.lblPort.Text = LabelNames.port;
            this.lblPortRange.Text = LabelNames.PortRange;
            this.lblPolling.Text = LabelNames.polling;
            this.lblPollingRange.Text = LabelNames.pollingrange;
            this.lblDeviceId.Text = LabelNames.DeviceId;
            this.lblDeviceIdRange.Text = LabelNames.DeviceIdRange;
            this.lblAddress.Text = LabelNames.Address;
            this.lblAddressRange.Text = LabelNames.AddressRange;
            this.lblLength.Text = LabelNames.Length;
            this.lblLengthRange.Text = LabelNames.LengthRange;
            this.lblVariable.Text = LabelNames.Variable;
            this.lblFunctionCode.Text = LabelNames.FunctionCode;
            TagEnable.SelectedIndexChanged -=TagEnable_SelectedIndexChanged;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List;
            TagEnable.SelectedIndexChanged += TagEnable_SelectedIndexChanged;
        }

        public void copyDataBind(string slaveName, MODBUSTCPClient_Slave mODBUSTCPClient_Slave)
        {
            _slaveName = slaveName;
            this.IPAddress.Text = mODBUSTCPClient_Slave.ServerIPAddress.ToString();
            this.Port.Value = mODBUSTCPClient_Slave.Port;
            this.Polling.Value = mODBUSTCPClient_Slave.Polling;
            this.DeviceId.Value = mODBUSTCPClient_Slave.DeviceId;
            this.Address.Value = mODBUSTCPClient_Slave.Address;
            this.Length.Value = mODBUSTCPClient_Slave.Length;
            this.textBoxVariable.Text = mODBUSTCPClient_Slave.Variable;
            this.TagEnable.Text = mODBUSTCPClient_Slave.Tag;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List;
            List<ModbusFunctionCode> functionCodeList = ModbusFunctionCode.List;
            ModbusFunctionCode selectedFunctionCode = (ModbusFunctionCode)functionCodeList.Where(T => T.Text == mODBUSTCPClient_Slave.Functioncode).FirstOrDefault();
            comboBoxFunctionCode.SelectedIndex = ModbusFunctionCode.List.IndexOf(selectedFunctionCode);
            this.TagEnable.SelectedIndex = TagEnable.Items.IndexOf(mODBUSTCPClient_Slave.Tag);
            this.cmbmultiplicationfact.SelectedIndex = cmbmultiplicationfact.Items.IndexOf(mODBUSTCPClient_Slave.MultiplicationFactor ?? "1");
        }

        /// <summary>
        /// Check if the Bit Address entered by the user is Valid or not
        /// </summary>
        /// <param name="control"></param> Name of Control 
        /// <param name="e"></param> Cancel Event Handler
        /// <param name="errorMessageToDisplay"></param>Error to be shown
        private void ValidateBitAddress(Control control, CancelEventArgs e, string errorMessageToDisplay)
        {
            var checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
            if (checktag != null)
                if ((checktag.Label == "Bool" || textBoxVariable.Text.Contains('.')))
                {
                    if (control.Text.IsValidBitAddress())
                    {
                        e.Cancel = false;
                        errorProvider.SetError(control, null);
                    }
                }
                else
                {
                    e.Cancel = true;
                    errorProvider.SetError(control, errorMessageToDisplay);
                }
        }

        private void ValidateBoolWordAddress(Control control, CancelEventArgs e, string errorMessageToDisplay)
        {
            var checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
            if (checktag != null)
                if ((checktag.Label == "Bool" || textBoxVariable.Text.Contains('.')))
                {
                    if (control.Text.IsValidBoolWordAddressForModBus())
                    {
                        e.Cancel = false;
                        errorProvider.SetError(control, null);
                    }
                }
                else
                {
                    e.Cancel = true;
                    errorProvider.SetError(control, errorMessageToDisplay);
                }
        }
        /// <summary>
        /// Check if the Word Address entered by the user is Valid or not
        /// </summary>
        /// <param name="control"></param> Name of Control 
        /// <param name="e"></param> Cancel Event Handler
        /// <param name="errorMessageToDisplay"></param>Error to be shown
        private void ValidateWordAddress(Control control, CancelEventArgs e, string errorMessageToDisplay)
        {
            var checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
            if (checktag != null)
            {
                if (checktag.Label != "Bool")
                {
                    if (control.Text.IsValidWordAddressForModBus())
                    {
                        e.Cancel = false;
                        errorProvider.SetError(control, null);
                    }
                }
                else
                {
                    e.Cancel = true;
                    errorProvider.SetError(control, errorMessageToDisplay);
                }
            }
        }


        private void textBoxVariable_Validating(object sender, CancelEventArgs e)
        {
            string type;
            // Check for select value in the function code combobox according to this required validation changes 
            type = $"{((ModbusFunctionCode)comboBoxFunctionCode.SelectedItem).ID:X2}";
            if (type == "05")
            {
                // Check validation for bit address
                ValidateBitAddress(textBoxVariable, e, "Invalid bit address or value for Enable field");
            }
            else if (type == "01" || type == "02" || type == "0F")
            {
                ValidateBoolWordAddress(textBoxVariable, e, "Invalid Logical address for Enable field");
            }
            else
            {
                // Check validation for word address   
                ValidateWordAddress(textBoxVariable, e, "Invalid word address or value for Enable field");
            }

            if (!e.Cancel)
            {
                var checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
                if (checktag != null)
                    this.TagEnable.Text = checktag.Tag.ToString();
                else if (!textBoxVariable.Text.Contains('.') && (textBoxVariable.Text.StartsWith("Q0") || textBoxVariable.Text.StartsWith("I1")))
                {
                    checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString() + ".00").FirstOrDefault();
                    this.TagEnable.Text = "";
                }
                else
                {
                    string newTag = "";
                    if (textBoxVariable.Text != "")
                    {
                        if (MessageBox.Show("Variable Tag is not Added You Want to Add", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            XMProForm tempForm = new XMProForm();
                            tempForm.StartPosition = FormStartPosition.CenterParent;
                            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                            tempForm.Text = "Add New Address Added in Logic";
                            newTag = textBoxVariable.Text;
                            TagsUserControl userControl = new TagsUserControl(0, newTag);
                            tempForm.Height = userControl.Height + 25;
                            tempForm.Width = userControl.Width;
                            tempForm.Controls.Add(userControl);
                            var frmTemp = this.ParentForm as frmMain;
                            DialogResult dialogResult = tempForm.ShowDialog(frmTemp);
                            if (dialogResult == DialogResult.OK)
                            {
                                this.currentLogicalAddress = userControl.LogicalAddressForModbus;
                            }
                        }
                    }
                    FillComboBoxDetails();
                    return;
                }
            }
        }
        private void comboBoxFunctionCode_Validating(object sender, CancelEventArgs e)
        {
            // Check for select value in the function code combobox according to this required validation changes 
            textBoxVariable_Validating(sender, e);
        }

        private void ModbusTCPClientUserControl_Load(object sender, EventArgs e)
        {
            DataBind();
            button1.Visible = false;
            groupBox2.Visible = true;
            button1.Enabled = false;

            string selectedCode = ((ModbusFunctionCode)comboBoxFunctionCode.SelectedItem).ID.ToString("X2");

            if (selectedCode == "03" || selectedCode == "04" || selectedCode == "06" || selectedCode == "10")
            {
                groupBox2.Visible = true;
            }
            else if (selectedCode == "01" || selectedCode == "02" || selectedCode == "05" || selectedCode == "0F")
            {
                groupBox2.Visible = false;
            }
            string projectName = XMPS.Instance?.LoadedProject?.PlcModel ?? "";
            if (projectName.StartsWith("XM", StringComparison.OrdinalIgnoreCase))
            {
                groupBox2.Visible = false;
                button1.Visible = false;
                button1.Enabled = false;
            }
        }

        private void DataBind()
        {
            var modBUSTCPClient = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
            if (modBUSTCPClient != null)
            {
                var slaves = modBUSTCPClient.Slaves;

                this.dataSource = (MODBUSTCPClient_Slave)slaves.Where(s => s.Name == _slaveName).FirstOrDefault();
                if (this.dataSource != null)
                {
                    this.IPAddress.Text = this.dataSource.ServerIPAddress.ToString();
                    this.Polling.Value = this.dataSource.Polling;
                    this.DeviceId.Value = this.dataSource.DeviceId;
                    this.Address.Value = this.dataSource.Address;
                    this.Length.Value = this.dataSource.Length;
                    this.Port.Value = this.dataSource.Port;
                    this.comboBoxFunctionCode.Text = this.dataSource.Functioncode.ToString();
                    this.textBoxVariable.Text = this.dataSource.Variable.ToString();
                    this.TagEnable.SelectedIndex = TagEnable.Items.IndexOf(this.dataSource.Tag);

                    if (!string.IsNullOrEmpty(dataSource.MultiplicationFactor))
                    {
                        int index = cmbmultiplicationfact.Items.IndexOf(dataSource.MultiplicationFactor);
                        if (index >= 0)
                            cmbmultiplicationfact.SelectedIndex = index;
                        else
                            cmbmultiplicationfact.SelectedIndex = -1; 
                    }
                    else
                    {
                        cmbmultiplicationfact.SelectedIndex = -1; 
                    }


                    oldSlavedate = new MODBUSTCPClient_Slave
                    {
                        Name = dataSource.Name,
                        ServerIPAddress = dataSource.ServerIPAddress,
                        Port = dataSource.Port,
                        Polling = dataSource.Polling,
                        DeviceId = dataSource.DeviceId,
                        Address = dataSource.Address,
                        Length = dataSource.Length,
                        Tag = dataSource.Tag,
                        Variable = dataSource.Variable,
                        Functioncode = dataSource.Functioncode,
                        MultiplicationFactor = dataSource.MultiplicationFactor,
                    };
                    isEdit = true;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (!this.ValidateChildren())
                {
                    MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.textBoxVariable.Text == "")
                {
                    this.TagEnable.Text = "";
                }
                var checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
                if (checktag != null)
                    this.TagEnable.Text = checktag.Tag.ToString();
                else
                {
                    if (!textBoxVariable.Text.Contains('.'))
                    {
                        checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString() + ".00").FirstOrDefault();
                        if (checktag != null)
                        {
                            string currentTagAdd = textBoxVariable.Text;
                            this.TagEnable.Text = "-Select Tag Name-";
                            this.textBoxVariable.Text = currentTagAdd;
                        }
                        if (checktag == null)
                        {
                            MessageBox.Show("Variable tag is not added in Tags", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Variable tag is not added in Tags", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                }

                ///Check if the Tag selected is proper for the length selected 
                var taginfo = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
                if (taginfo != null)
                {
                    if (taginfo.Type == Core.Types.IOType.DataType)
                    {
                        if ((taginfo.Label == "Real" || taginfo.Label == "Double Word") && (Length.Value % 2) != 0)
                        {
                            MessageBox.Show("Variable tag is not matching with length selected", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                var modBUSTCPClient = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                if (modBUSTCPClient.Slaves.Where(M => M.Variable == textBoxVariable.Text.ToString() && M.Name != _slaveName && M.Functioncode == comboBoxFunctionCode.Text.ToString()).Count() > 0)
                {
                    MessageBox.Show("Variable tag is already used in another request kindly select another variable tag", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ///<>
                ///For the Not Allow to Repeate Address Value In Modbus Requests.
                ///
                int newAddress = (int)Address.Value;
                int newLength = (int)Length.Value;

                foreach (var slave in modBUSTCPClient.Slaves.Where(s => s.Name != _slaveName && s.DeviceId == (int)DeviceId.Value && s.ServerIPAddress.ToString() == IPAddress.IPAddress.ToString()))
                {
                    int existingAddress = slave.Address;
                    int existingLength = slave.Length;

                    int rangeStart = existingAddress;
                    int rangeEnd = existingLength;

                    bool sameAddress = newAddress == existingAddress;
                    bool addressOverlap = newAddress >= rangeStart && newAddress < rangeEnd && !sameAddress;

                    if (sameAddress && slave.Functioncode == comboBoxFunctionCode.Text)
                    {
                        MessageBox.Show($"Address {newAddress} is already used by another slave.", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (addressOverlap && slave.Functioncode == comboBoxFunctionCode.Text)
                    {
                        MessageBox.Show($"Address {newAddress} overlaps with an existing slave range {rangeStart}-{rangeEnd}.", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                ///<>
                ///Check for the if if the Request contain Double Word 
                ///
                string AddValue = Address.Text.ToString();
                int CurrentAddValue = int.Parse(AddValue);
                int preAddValue = CurrentAddValue - 1;
                int nextAddValue = CurrentAddValue + 1;
                var AddressValueCheckedPrev = modBUSTCPClient.Slaves.Where(M => M.Address.ToString() == preAddValue.ToString() && M.Functioncode == comboBoxFunctionCode.Text.ToString() && M.Name != _slaveName).FirstOrDefault();
                if (AddressValueCheckedPrev != null)
                {
                    if (AddressValueCheckedPrev.Variable.StartsWith("Q") || AddressValueCheckedPrev.Variable.StartsWith("I"))
                    {

                    }
                    else
                    {
                        XMIOConfig usedTagInRequest = xm.LoadedProject.Tags.Where(T => T.Tag == AddressValueCheckedPrev.Tag.ToString()).FirstOrDefault();
                        if (usedTagInRequest != null)
                        {
                            bool TagisDoubleWord = usedTagInRequest.Label == "Double Word" ? true : false;
                            bool TagisReal = usedTagInRequest.Label == "Real" ? true : false;
                            int type = XMProValidator.GetFunctionCode(comboBoxFunctionCode.SelectedItem.ToString());
                            bool isbooltype = (type == 01 || type == 02 || type == 05 || type == 15);
                            if ((TagisDoubleWord || TagisReal) && !isbooltype)
                            {
                                MessageBox.Show("Address Values Already Used", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                int chktype = XMProValidator.GetFunctionCode(comboBoxFunctionCode.SelectedItem.ToString());
                if (this.Length.Value != 1 && (chktype == 5 || chktype == 6))
                {
                    MessageBox.Show("Length is required to be 1 for " + comboBoxFunctionCode.SelectedItem.ToString(), "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //check if Next Address is use Double Word tag 
                var AddressValueCheckedNext = modBUSTCPClient.Slaves.Where(M => M.Address.ToString() == nextAddValue.ToString() && M.Functioncode == comboBoxFunctionCode.Text.ToString() && M.Name != _slaveName).FirstOrDefault();
                if (AddressValueCheckedNext != null)
                {
                    if (textBoxVariable.Text.StartsWith("Q") || textBoxVariable.Text.StartsWith("I"))
                    {

                    }
                    else
                    {
                        XMIOConfig usedTagInRequest = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
                        if (usedTagInRequest != null)
                        {
                            bool TagisDoubleWord = usedTagInRequest.Label == "Double Word" ? true : false;
                            bool TagisReal = usedTagInRequest.Label == "Real" ? true : false;
                            int type = XMProValidator.GetFunctionCode(comboBoxFunctionCode.SelectedItem.ToString());
                            bool isbooltype = (type == 01 || type == 02 || type == 05 || type == 15);
                            if ((TagisDoubleWord || TagisReal) && !isbooltype)
                            {
                                MessageBox.Show("Next Address Value Already Used", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                int maxCount = (int)(xm.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null)
                                          .SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MODBUSTCPClient").MaxCount);

                if (modBUSTCPClient.Slaves.Where(M => M.Name != _slaveName).Count() >= maxCount)
                {
                    MessageBox.Show("Maximum limit of Modbus TCP Client is reached", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }              
                if (modBUSTCPClient != null)
                {
                   modBUSTCPClient.AddSlave(
                        this._slaveName,
                        this.IPAddress.IPAddress,
                        Int32.Parse(this.Port.Value.ToString()),
                        Int32.Parse(this.Polling.Value.ToString()),
                        Int32.Parse(this.DeviceId.Value.ToString()),
                        Int32.Parse(this.Address.Value.ToString()),
                        Int32.Parse(this.Length.Value.ToString()),
                        this.textBoxVariable.Text.ToString(),
                        this.TagEnable.Text.ToString(),
                        comboBoxFunctionCode.Text,
                        selectedMultiplicationFactor
                    );

                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSTCPClient");
                    xm.LoadedProject.Devices.Add(modBUSTCPClient);
                    if (!isEdit)
                    {
                        ModbusTCPClientManager.AddMODBUSTCPClientSlave(new MODBUSTCPClient_Slave
                        {
                            Name = this._slaveName,
                            ServerIPAddress = this.IPAddress.IPAddress,
                            Port = Int32.Parse(this.Port.Value.ToString()),
                            Polling = Int32.Parse(this.Polling.Value.ToString()),
                            DeviceId = Int32.Parse(this.DeviceId.Value.ToString()),
                            Address = Int32.Parse(this.Address.Value.ToString()),
                            Length = Int32.Parse(this.Length.Value.ToString()),
                            Tag = this.TagEnable.Text.ToString(),
                            Variable = this.textBoxVariable.Text.ToString(),
                            Functioncode = comboBoxFunctionCode.Text,
                            MultiplicationFactor = selectedMultiplicationFactor
                        });
                    }
                    else
                    {
                        ModbusTCPClientManager.UpdateMODBUSTCPClientSlave(oldSlavedate, new MODBUSTCPClient_Slave
                        {
                            Name = this._slaveName,
                            ServerIPAddress = this.IPAddress.IPAddress,
                            Port = Int32.Parse(this.Port.Value.ToString()),
                            Polling = Int32.Parse(this.Polling.Value.ToString()),
                            DeviceId = Int32.Parse(this.DeviceId.Value.ToString()),
                            Address = Int32.Parse(this.Address.Value.ToString()),
                            Length = Int32.Parse(this.Length.Value.ToString()),
                            Tag = this.TagEnable.Text.ToString(),
                            Variable = this.textBoxVariable.Text.ToString(),
                            Functioncode = comboBoxFunctionCode.Text,
                            MultiplicationFactor = this.selectedMultiplicationFactor
                        });
                    }
                }
                xm.MarkProjectModified(true);
                this.ParentForm.Close();
                this.ParentForm.DialogResult = DialogResult.OK;
                xm.LoadedProject.NewAddedTagIndex = modBUSTCPClient.Slaves.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Try-Again error " + " : " + ex.Message.ToString(), "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private bool IsMultiplicationFactorAllowed()
        {
            try
            {
                // Check if current project name contains XBLD-14E or XBLD-17E
                string projectName = xm.LoadedProject?.PlcModel ?? "";
                return projectName=="XBLD-14E" || projectName=="XBLD-17E";
            }
            catch
            {
                return false;
            }
        }
        private void comboBoxFunctionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxDetails();

            button1.Visible = false;
            groupBox2.Visible = false;
                button1.Enabled = false;

            if (IsMultiplicationFactorAllowed())
            {
                string selectedCode = ((ModbusFunctionCode)comboBoxFunctionCode.SelectedItem).ID.ToString("X2");

                if (selectedCode == "03" || selectedCode == "04" || selectedCode == "06" || selectedCode == "10")
                {
                    selectedMultiplicationFactor = "1";

                    if (string.IsNullOrEmpty(cmbmultiplicationfact.Text) || cmbmultiplicationfact.SelectedIndex == -1)
                    {
                        cmbmultiplicationfact.SelectedIndex = 0;
                    }
                    button1.Visible = true;
                    button1.Enabled = true;
                }
            }
        }

        private void FillComboBoxDetails()
        {
            string type;
            // Check for select value in the function code combobox according to this required validation changes 
            type = $"{((ModbusFunctionCode)comboBoxFunctionCode.SelectedItem).ID:X2}";
            if (type == "01" || type == "02" || type == "05" || type == "0F")
            {
                if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
                {
                    TagEnable.DataSource = XMProValidator.FillTagOperandsForModbus("Bool").Where(t => !(t.ToString().Contains("OR") || t.ToString().Contains("OL")) && XMPS.Instance.PlcModel.StartsWith("XBLD")).ToList();
                }
                else
                {
                    TagEnable.DataSource = XMProValidator.FillTagOperandsForModbus("Bool");
                }
            }
            else
            {
                if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
                {
                    TagEnable.DataSource = XMProValidator.FillTagOperandsForModbus("Word").Where(t => !(t.ToString().Contains("OR") || t.ToString().Contains("OL")) && XMPS.Instance.PlcModel.StartsWith("XBLD")).ToList();
                }
                else
                {
                    TagEnable.DataSource = XMProValidator.FillTagOperandsForModbus("Word");
                }
            }

            var existingTags = ((IEnumerable<object>)TagEnable.DataSource).Select(t => t.ToString()).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();

            bool isBoolType = (type == "01" || type == "02" || type == "05" || type == "0F");

            var userDefinedTags = XMPS.Instance.LoadedProject.Tags.Where(t =>t.Tag != null && !string.IsNullOrEmpty(t.Tag.ToString()) &&((isBoolType &&(t.Label == "Bool" ||t.LogicalAddress.Contains(".") ||t.LogicalAddress.StartsWith("F2"))) ||(!isBoolType &&(t.Label == "Word" ||
            t.LogicalAddress.StartsWith("W4") || t.LogicalAddress.StartsWith("T6") || t.LogicalAddress.StartsWith("C7") || t.LogicalAddress.StartsWith("P5"))))).Select(t => t.Tag.ToString()).Distinct().OrderBy(t => t).ToList();

            var finalTags = existingTags.Union(userDefinedTags).Where(t => !string.IsNullOrWhiteSpace(t)).Distinct().ToList();

            TagEnable.DataSource = finalTags;
        }
        private void TagEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentLogicalAddress))
            {
                string logicalAdd = XMProValidator.GetTheAddressFromTag(TagEnable.Text);
                textBoxVariable.Text = (currentLogicalAddress == logicalAdd || logicalAdd == "") ? currentLogicalAddress : logicalAdd;
            }
            else
            {
                textBoxVariable.Text = XMProValidator.GetTheAddressFromTag(TagEnable.Text);
            }
            this.ValidateChildren();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedMultiplicationFactor= string.IsNullOrEmpty(cmbmultiplicationfact.Text) ? "1" : cmbmultiplicationfact.Text;
            button1.Visible = false;
            groupBox2.Visible = true;
            if (cmbmultiplicationfact.Items.Contains(selectedMultiplicationFactor))
            {
                cmbmultiplicationfact.SelectedItem = selectedMultiplicationFactor;
            }
            else
            {
                cmbmultiplicationfact.SelectedIndex = 0; 
            }
            cmbmultiplicationfact.SelectedItem = selectedMultiplicationFactor; 
        }

        private void cmdbtnOK_Click(object sender, EventArgs e)
        {
            if (cmbmultiplicationfact.SelectedItem != null)
            {
                selectedMultiplicationFactor = cmbmultiplicationfact.SelectedItem.ToString();
            }
            else
            {
                selectedMultiplicationFactor = "1";
            }

            groupBox2.Visible = true;
        }

        private void cmdbtnCancel_Click(object sender, EventArgs e)
        {
            cmbmultiplicationfact.SelectedItem = "1";
            selectedMultiplicationFactor = "1";
        }
        private void Port_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }

        private void Polling_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }

        private void DeviceId_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }

        private void Address_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }

        private void Length_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }
        private void cmbmultiplicationfact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbmultiplicationfact.SelectedItem != null)
            {
                selectedMultiplicationFactor = cmbmultiplicationfact.SelectedItem.ToString();
            }
        }
    }
}