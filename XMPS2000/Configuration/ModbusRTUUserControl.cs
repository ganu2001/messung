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
    public partial class ModbusRTUUserControl : UserControl
    {
        private MODBUSRTUMaster_Slave dataSource;
        private string _slaveName;
        private string _disableVariable;
        private string currentLogicalAddress;
        XMPS xm;
        //UndoRedo
        private ModbusRTUMasterManager ModbusRTUSlaveManager = new ModbusRTUMasterManager();
        private bool isEdit = false;
        private MODBUSRTUMaster_Slave oldSlavedate;
        private string selectedMultiplicationFactor;
        public ModbusRTUUserControl(string slaveName)
        {
            xm = XMPS.Instance;
            _slaveName = slaveName;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            InitializeComponent();

            ////Change names of Labels, take names from LabelNames Resource file
            this.lblpolling.Text = LabelNames.polling;
            this.lblpollingrange.Text = LabelNames.pollingrange;
            this.labelDeviceId.Text = LabelNames.DeviceId;
            this.labelDeviceIdRange.Text = LabelNames.DeviceIdRange;
            this.lblAddress.Text = LabelNames.Address;
            this.lblAddressRange.Text = LabelNames.comtoutrange;
            this.lblLength.Text = LabelNames.Length;
            this.lblLengthRange.Text = LabelNames.LengthRange;
            this.lblVariable.Text = LabelNames.Variable;
            this.lblFunctionCode.Text = LabelNames.FunctionCode;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List;
        }
        public ModbusRTUUserControl()
        {
            xm = XMPS.Instance;
            //  _slaveName = slaveName;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            InitializeComponent();

            ////Change names of Labels, take names from LabelNames Resource file
            this.lblpolling.Text = LabelNames.polling;
            this.lblpollingrange.Text = LabelNames.pollingrange;
            this.labelDeviceId.Text = LabelNames.DeviceId;
            this.labelDeviceIdRange.Text = LabelNames.DeviceIdRange;
            this.lblAddress.Text = LabelNames.Address;
            this.lblAddressRange.Text = LabelNames.comtoutrange;
            this.lblLength.Text = LabelNames.Length;
            this.lblLengthRange.Text = LabelNames.LengthRange;
            this.lblVariable.Text = LabelNames.Variable;
            this.lblFunctionCode.Text = LabelNames.FunctionCode;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List;
        }

        private void ModbusRTUUserControl_Load(object sender, EventArgs e)
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
            this.lblLengthRange.Text = "(Range: 1 to 100)";
        }

        private bool IsMultiplicationFactorAllowed()
        {
            try
            {
                string projectName = xm.LoadedProject?.PlcModel ?? "";
                return projectName == "XBLD-14E" || projectName == "XBLD-17E";
            }
            catch
            {
                return false;
            }
        }
        private void DataBind()
        {
            var modBUSRTUMaster = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
            if (modBUSRTUMaster != null)
            {
                var slaves = modBUSRTUMaster.Slaves;

                this.dataSource = (MODBUSRTUMaster_Slave)slaves.Where(s => s.Name == _slaveName).FirstOrDefault();
                if (this.dataSource != null)
                {
                    this.Address.Value = this.dataSource.Address;
                    this.Length.Value = this.dataSource.Length;
                    this.Polling.Value = this.dataSource.Polling;
                    this.textBoxVariable.Text = this.dataSource.Variable.ToString();
                    this.comboBoxFunctionCode.Text = this.dataSource.FunctionCode.ToString();
                    this.DeviceId.Value = this.dataSource.DeviceId;
                    this.textBoxVariable.Text = this.dataSource.Variable;
                    this._disableVariable = this.dataSource.DisablingVariables;
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
                    isEdit = true;
                    oldSlavedate = new MODBUSRTUMaster_Slave
                    {
                        Polling = dataSource.Polling,
                        DeviceId = dataSource.DeviceId,
                        Address = dataSource.Address,
                        Length = dataSource.Length,
                        Variable = dataSource.Variable,
                        Tag = dataSource.Tag,
                        FunctionCode = dataSource.FunctionCode,
                        Name = dataSource.Name,
                        DisablingVariables = dataSource.DisablingVariables,
                        MultiplicationFactor = dataSource.MultiplicationFactor 
                    };
                    isEdit = true;
                }
            }
        }

        public void CopyDataBind(string slaveName, MODBUSRTUMaster_Slave mODBUSRTUMaster)
        {
            _disableVariable = mODBUSRTUMaster.DisablingVariables;
            _slaveName = slaveName;
            this.Address.Value = mODBUSRTUMaster.Address;
            this.Length.Value = mODBUSRTUMaster.Length;
            this.Polling.Value = mODBUSRTUMaster.Polling;
            this.DeviceId.Value = mODBUSRTUMaster.DeviceId;
            this.textBoxVariable.Text = mODBUSRTUMaster.Variable;
            this.TagEnable.Text = mODBUSRTUMaster.Tag;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List;
            List<ModbusFunctionCode> functionCodeList = ModbusFunctionCode.List;
            ModbusFunctionCode selectedFunctionCode = (ModbusFunctionCode)functionCodeList.Where(T => T.Text == mODBUSRTUMaster.FunctionCode).FirstOrDefault();
            comboBoxFunctionCode.SelectedIndex = ModbusFunctionCode.List.IndexOf(selectedFunctionCode);
            this.TagEnable.SelectedIndex = TagEnable.Items.IndexOf(mODBUSRTUMaster.Tag);
            this.cmbmultiplicationfact.SelectedIndex = cmbmultiplicationfact.Items.IndexOf(mODBUSRTUMaster.MultiplicationFactor ?? "1");
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
                var modBUSRTUMaster = (MODBUSRTUMaster)xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "MODBUSRTUMaster");
                if (modBUSRTUMaster == null)
                {
                    if (xm.LoadedProject.RS485Mode == "Master")
                    {
                        modBUSRTUMaster = new MODBUSRTUMaster();
                        xm.LoadedProject.Devices.Add(modBUSRTUMaster);
                    }
                    else
                    {
                        MessageBox.Show("MODBUS RTU Master is not configured...");
                        return;
                    }
                }
                int currentFunctionCodeId = ((ModbusFunctionCode)comboBoxFunctionCode.SelectedItem).ID;
                bool isAllowedFunctionCode = currentFunctionCodeId == 0x05 || currentFunctionCodeId == 0x06 || currentFunctionCodeId == 0x0F || currentFunctionCodeId == 0x10;
                if (isAllowedFunctionCode)
                {
                    if (modBUSRTUMaster.Slaves.Where(M => M.Variable == textBoxVariable.Text.ToString() && M.Name != _slaveName && M.FunctionCode == comboBoxFunctionCode.Text.ToString() && M.DeviceId == (int)DeviceId.Value).Count() > 0)
                    {
                        MessageBox.Show("Variable tag is already used in another request for the same device ID, kindly select another variable tag", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    if (modBUSRTUMaster.Slaves.Where(M => M.Variable == textBoxVariable.Text.ToString() && M.Name != _slaveName && M.FunctionCode == comboBoxFunctionCode.Text.ToString()).Count() > 0)
                    {
                        MessageBox.Show("Variable tag is already used in another request kindly select another variable tag", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                ///<>
                ///For the Not Allow to Repeate Address Value In Modbus Requests.
                ///
                int newAddress = (int)Address.Value;
                int newLength = (int)Length.Value;

                foreach (var slave in modBUSRTUMaster.Slaves.Where(s => s.Name != _slaveName && s.DeviceId == (int)DeviceId.Value))
                {
                    int existingAddress = slave.Address;
                    int existingLength = slave.Length;

                    int rangeStart = existingAddress;
                    int rangeEnd = existingLength;

                    bool sameAddress = newAddress == existingAddress;
                    bool addressOverlap = newAddress >= rangeStart && newAddress < rangeEnd && !sameAddress;

                    if (sameAddress && slave.FunctionCode == comboBoxFunctionCode.Text)
                    {
                        MessageBox.Show($"Address {newAddress} is already used by another slave.", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (addressOverlap && slave.FunctionCode == comboBoxFunctionCode.Text)
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
                var AddressValueCheckedPrev = modBUSRTUMaster.Slaves.Where(M => M.Address.ToString() == preAddValue.ToString() && M.DeviceId.ToString() == DeviceId.Text.ToString() && M.FunctionCode == comboBoxFunctionCode.Text && M.Name != _slaveName).FirstOrDefault();
                if (AddressValueCheckedPrev != null)
                {
                    if (AddressValueCheckedPrev.Variable.StartsWith("Q") || AddressValueCheckedPrev.Variable.StartsWith("I"))
                    {

                    }
                    else
                    {
                        XMIOConfig usedTagInRequest = xm.LoadedProject.Tags.Where(T => T.Tag == AddressValueCheckedPrev.Tag.ToString()).FirstOrDefault();
                        if(usedTagInRequest != null)
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
                ///Check length validation 
                int chktype = XMProValidator.GetFunctionCode(comboBoxFunctionCode.SelectedItem.ToString());
                if (this.Length.Value != 1 && (chktype == 5 || chktype == 6))
                {
                    MessageBox.Show("Length is required to be 1 for " + comboBoxFunctionCode.SelectedItem.ToString(), "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int maxCount = (int)(xm.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.RS485 != null)
                                          .SelectMany(t => t.RS485.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MODBUSRTUMaster").MaxCount);

                if (modBUSRTUMaster.Slaves.Where(M => M.Name != _slaveName).Count() >= maxCount)
                {
                    MessageBox.Show("Maximum limit of Modbus RTU is reached", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //check if Next Address is use Double Word tag 
                var AddressValueCheckedNext = modBUSRTUMaster.Slaves.Where(M => M.Address.ToString() == nextAddValue.ToString() && M.DeviceId.ToString() == DeviceId.Text.ToString() && M.FunctionCode == comboBoxFunctionCode.Text && M.Name != _slaveName).FirstOrDefault();
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
                
                if (modBUSRTUMaster != null)
                {
                    modBUSRTUMaster.AddSlave(
                        this._slaveName,
                        (int)this.Polling.Value,
                        (int)this.DeviceId.Value,
                        (int)this.Address.Value,
                        (int)this.Length.Value,
                        this.textBoxVariable.Text,
                        this.TagEnable.Text,
                        this.comboBoxFunctionCode.Text,
                        this._disableVariable,
                        selectedMultiplicationFactor
                        );

                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSRTUMaster");
                    xm.LoadedProject.Devices.Add(modBUSRTUMaster);
                    if (!isEdit)
                    {
                        ModbusRTUSlaveManager.AddMODBUSRTUSlave(new MODBUSRTUMaster_Slave
                        {
                            Polling = (int)this.Polling.Value,
                            DeviceId = (int)this.DeviceId.Value,
                            Address = (int)this.Address.Value,
                            Length = (int)this.Length.Value,
                            Variable = this.textBoxVariable.Text,
                            Tag = this.TagEnable.Text,
                            FunctionCode = comboBoxFunctionCode.Text,
                            Name = this._slaveName,
                            DisablingVariables = this._disableVariable,
                            MultiplicationFactor = selectedMultiplicationFactor
                        });
                    }
                    else
                    {
                        ModbusRTUSlaveManager.UpdateMODBUSRTUSlave(oldSlavedate, new MODBUSRTUMaster_Slave
                        {
                            Polling = (int)this.Polling.Value,
                            DeviceId = (int)this.DeviceId.Value,
                            Address = (int)this.Address.Value,
                            Length = (int)this.Length.Value,
                            Variable = this.textBoxVariable.Text,
                            Tag = this.TagEnable.Text,
                            FunctionCode = comboBoxFunctionCode.Text,
                            Name = this._slaveName,
                            DisablingVariables = this._disableVariable,
                            MultiplicationFactor = this.selectedMultiplicationFactor
                        });
                    }
                }
                xm.MarkProjectModified(true);
                this.ParentForm.Close();
                this.ParentForm.DialogResult = DialogResult.OK;
                xm.LoadedProject.NewAddedTagIndex = modBUSRTUMaster.Slaves.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Try-Again : "+ ex.Message , "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        
        /// <summary>
        /// Validate if the entered Bit Address is as per requirenments or not
        /// </summary>
        /// <param name="control"></param> Control for which the check is required
        /// <param name="e"></param> Cancel Event Handler
        /// <param name="errorMessageToDisplay"></param> Error to show
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

        /// <summary>
        /// Check if the entered Word Address is valid or not and raise error if any
        /// </summary>
        /// <param name="control"></param> Control for which the check is required
        /// <param name="e"></param> Cancel Event Handler
        /// <param name="errorMessageToDisplay"></param> Error Message to show
        private void ValidateWordAddress(Control control, CancelEventArgs e, string errorMessageToDisplay)
        {
            var checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
            if (checktag != null)
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

        private void textBoxVariable_Validating(object sender, CancelEventArgs e)
        {
            string type;
            // Check for select value in the function code combobox according to this required validation changes 
            type = $"{((ModbusFunctionCode)comboBoxFunctionCode.SelectedItem).ID:X2}";
            if (type == "05")
            {
                // Check validation for bit address
                ValidateBitAddress(textBoxVariable, e, "Inavalid bit address or value for Enable field");
            }
            else if(type == "01" || type == "02" || type == "0F")
            {
                ValidateBoolWordAddress(textBoxVariable, e, "Inavalid Logical address for Enable field");
            }
            else
            {
                // Check validation for word address   
                ValidateWordAddress(textBoxVariable, e, "Inavalid word address or value for Enable field");
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
                    //for creating Tag from Modbus Window
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
                    FillTagDropDown();
                    return;
                }
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

        private void comboBoxFunctionCode_Validating(object sender, CancelEventArgs e)
        {
            // Check for select value in the function code combobox according to this required validation changes 
            textBoxVariable_Validating(sender, e);
        }

        private void comboBoxFunctionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTagDropDown();

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
        
        private void FillTagDropDown()
        {
            string type;
            // Check for select value in the function code combobox according to this required validation changes 
            type = $"{((ModbusFunctionCode)comboBoxFunctionCode.SelectedItem).ID:X2}";
            if (type == "01" || type == "02" || type == "05" || type == "0F")
            {
                TagEnable.DataSource = XMProValidator.FillTagOperandsForModbus("Bool");
            }
            else
            {
                TagEnable.DataSource = XMProValidator.FillTagOperandsForModbus("Word");
            }
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
        }

        private void Length_Validating(object sender, CancelEventArgs e)
        {    
                if (Length.Value < 1 || Length.Value > 100)
                {
                errorProvider.SetError(Length, "Length must be between 1 to 100.");

                e.Cancel = true;
                return;
            }                  
        }
         private void button1_Click(object sender, EventArgs e)
        {
            selectedMultiplicationFactor = string.IsNullOrEmpty(cmbmultiplicationfact.Text) ? " 1" : cmbmultiplicationfact.Text;
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

        private void Polling_Leave(object sender, EventArgs e)
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

