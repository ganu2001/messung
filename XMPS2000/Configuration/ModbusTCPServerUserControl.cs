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
    public partial class ModbusTCPServerUserControl : UserControl
    {
        private MODBUSTCPServer_Request dataSource;
        private string _reqName;
        private string currentLogicalAddress;
        XMPS xm;

        //UndoRedo
        private MODBUSTCPServerManager modbusTCPServerManager = new MODBUSTCPServerManager();
        private bool isEdit = false;
        private MODBUSTCPServer_Request oldSlavedate;

        public ModbusTCPServerUserControl(string reqName)
        {
            xm = XMPS.Instance;
            _reqName = reqName;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            InitializeComponent();
            ////Change names of Labels, take names from LabelNames Resource file
            this.labelDeviceId.Text = LabelNames.DeviceId;
            this.labelDeviceIdRange.Text = LabelNames.DeviceIdRange;
            this.lblAddress.Text = LabelNames.Address;
            this.lblAddressRange.Text = LabelNames.AddressRange;
            this.lblLength.Text = LabelNames.Length;
            this.lblLengthRange.Text = LabelNames.LengthRange;
            this.lblPort.Text = LabelNames.port;
            this.lblPortRange.Text = LabelNames.PortRange;
            this.lblVariable.Text = LabelNames.Variable;
            this.lblFunctionCode.Text = LabelNames.FunctionCode;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List.Where(M => M.ID < 5).ToList().Select(M => M.Text.Replace("Read ", "").Split('(').First()).ToList();
        }

        public void copyDataBind(MODBUSTCPServer_Request mODBUSTCPServer_Request)
        {
            this.Port.Value = mODBUSTCPServer_Request.Port;
            this.Address.Value = mODBUSTCPServer_Request.Address;
            this.Length.Value = mODBUSTCPServer_Request.Length;
            this.textBoxVariable.Text = mODBUSTCPServer_Request.Variable;
            this.TagEnable.Text = mODBUSTCPServer_Request.Tag;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List.Where(M => M.ID < 5).ToList().Select(M => M.Text.Replace("Read ", "").Split('(').First()).ToList();
            List<string> functionCodeList = ModbusFunctionCode.List.Where(M => M.ID < 5).ToList().Select(M => M.Text.Replace("Read ", "").Split('(').First()).ToList();
            comboBoxFunctionCode.SelectedIndex = functionCodeList.IndexOf(mODBUSTCPServer_Request.FunctionCode);
            this.TagEnable.SelectedIndex = TagEnable.Items.IndexOf(mODBUSTCPServer_Request.Tag);
        }
        private void ModbusTCPServerUserControl_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        private void DataBind()
        {
            var modBUSTCPServer = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
            if (modBUSTCPServer != null)
            {
                var requests = modBUSTCPServer.Requests;

                this.dataSource = (MODBUSTCPServer_Request)requests.Where(s => s.Name == _reqName).FirstOrDefault();
                if (this.dataSource != null)
                {
                    this.Address.Value = this.dataSource.Address;
                    this.Length.Value = this.dataSource.Length;
                    this.Port.Value = this.dataSource.Port;
                    this.comboBoxFunctionCode.Text = this.dataSource.FunctionCode.ToString();
                    this.textBoxVariable.Text = this.dataSource.Variable.ToString();
                    this.TagEnable.SelectedIndex = TagEnable.Items.IndexOf(this.dataSource.Tag);

                    oldSlavedate = new MODBUSTCPServer_Request
                    {
                        Name = this.dataSource.Name,
                        Port = this.dataSource.Port,
                        Address = this.dataSource.Address,
                        Length = this.dataSource.Length,
                        Tag = this.dataSource.Tag,
                        Variable = this.dataSource.Variable,
                        FunctionCode = this.dataSource.FunctionCode
                    };
                    isEdit = true;
                }
            }
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
            type = ModbusFunctionCode.List.Where(M => M.Text.Replace("Read ", "").Split('(').First() == comboBoxFunctionCode.SelectedItem.ToString()).ToList().Select(M => M.ID).FirstOrDefault().ToString();
            if (type == "1" || type == "2" || type == "5" || type == "F")
            {
                // Check validation for bit address
                ValidateBitAddress(textBoxVariable, e, "Inavalid bit address or value for Enable field");
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
                    //MessageBox.Show("Variable tag is added in Tags", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        FillComboBoxDetails();
                        return;
                    }
                }
            }



        }

        private void comboBoxFunctionCode_Validating(object sender, CancelEventArgs e)
        {
            // Check for select value in the function code combobox according to this required validation changes 
            textBoxVariable_Validating(sender, e);
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
                var modBUSTCPServer = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                if (modBUSTCPServer.Requests.Where(M => M.Variable == textBoxVariable.Text.ToString() && M.Name != _reqName && M.FunctionCode == comboBoxFunctionCode.Text.ToString()).Count() > 0)
                {
                    MessageBox.Show("Variable tag is already used in another request kindly select another variable tag", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ///<>
                ///For the Not Allow to Repeate Address Value In Modbus Requests.
                ///
                var AddressValueChecked = modBUSTCPServer.Requests.Where(M => M.Address.ToString() == Address.Text.ToString() && M.Name != _reqName && M.FunctionCode == comboBoxFunctionCode.Text.ToString()).FirstOrDefault();
                if (AddressValueChecked != null)
                {
                    MessageBox.Show("Address Values is already used Please Used Next Address Value", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int maxCount = (int)(xm.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null)
                                          .SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MODBUSTCPServer").MaxCount);

                if (modBUSTCPServer.Requests.Where(M => M.Name != _reqName).Count() >= maxCount)
                {
                    MessageBox.Show("Maximum limit of Modbus TCP Server is reached", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ///<>
                ///Check for the if if the Request contain Double Word 
                ///
                string AddValue = Address.Text.ToString();
                int CurrentAddValue = int.Parse(AddValue);
                int preAddValue = CurrentAddValue - 1;
                int nextAddValue = CurrentAddValue + 1;
                var AddressValueCheckedPrev = modBUSTCPServer.Requests.Where(M => M.Address.ToString() == preAddValue.ToString() && M.FunctionCode == comboBoxFunctionCode.Text.ToString() && M.Name != _reqName).FirstOrDefault();
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
                            int type = GetFunctionCode(comboBoxFunctionCode.SelectedItem.ToString());
                            bool isbooltype = (type == 01 || type == 02 || type == 05 || type == 15);
                            if ((TagisDoubleWord || TagisReal) && !isbooltype)
                            {
                                MessageBox.Show("Address Values Already Used", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }

                //check if Next Address is use Double Word tag 
                var AddressValueCheckedNext = modBUSTCPServer.Requests.Where(M => M.Address.ToString() == nextAddValue.ToString() && M.FunctionCode == comboBoxFunctionCode.Text.ToString() && M.Name != _reqName).FirstOrDefault();
                if (AddressValueCheckedNext != null)
                {
                    if (textBoxVariable.Text.StartsWith("Q")|| textBoxVariable.Text.StartsWith("I"))
                    {

                    }
                    else
                    {

                        XMIOConfig usedTagInRequest = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
                        if(usedTagInRequest != null)
                        {
                            bool TagisDoubleWord = usedTagInRequest.Label == "Double Word" ? true : false;
                            bool TagisReal = usedTagInRequest.Label == "Real" ? true : false;
                            int type = GetFunctionCode(comboBoxFunctionCode.SelectedItem.ToString());
                            bool isbooltype = (type == 01 || type == 02 || type == 05 || type == 15);
                            if ((TagisDoubleWord || TagisReal) && !isbooltype)
                            {
                                MessageBox.Show("Next Address Value Already Used", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }

                if (modBUSTCPServer != null)
                {
                    modBUSTCPServer.AddRequest(
                        this._reqName,
                        (int)this.Address.Value,
                        (int)this.Length.Value,
                        (int)this.Port.Value,
                        this.textBoxVariable.Text,
                        (int)this.DeviceId.Value,
                        this.TagEnable.Text.ToString(),
                        comboBoxFunctionCode.Text
                        );

                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSTCPServer");
                    xm.LoadedProject.Devices.Add(modBUSTCPServer);
                    if (!isEdit)
                    {
                        modbusTCPServerManager.AddMODBUSTCPServerSlave(new MODBUSTCPServer_Request
                        {
                            Name = this._reqName,
                            Port = (int)this.Port.Value,
                            Address = (int)this.Address.Value,
                            Length = (int)this.Length.Value,
                            Tag = this.TagEnable.Text.ToString(),
                            Variable = this.textBoxVariable.Text,
                            FunctionCode = this.comboBoxFunctionCode.Text
                        });
                    }
                    else
                    {
                        modbusTCPServerManager.UpdateMODBUSTCPServerSlave(oldSlavedate, new MODBUSTCPServer_Request
                        {
                            Name = this._reqName,
                            Port = (int)this.Port.Value,
                            Address = (int)this.Address.Value,
                            Length = (int)this.Length.Value,
                            Tag = this.TagEnable.Text.ToString(),
                            Variable = this.textBoxVariable.Text,
                            FunctionCode = this.comboBoxFunctionCode.Text
                        });
                    }
                }
                xm.MarkProjectModified(true);
                this.ParentForm.Close();
                this.ParentForm.DialogResult = DialogResult.OK;

                xm.LoadedProject.NewAddedTagIndex = modBUSTCPServer.Requests.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Try-Again error : " + ex.Message.ToString(), "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private int GetFunctionCode(string functionCode)
        {
            switch (functionCode)
            {
                case "Coil ":
                    return 01;
                case "Discrete Input ":
                    return 02;
                case "Single Coil ":
                    return 05;
                case "Multiple Coils ":
                    return 15;
                default:
                    return -1;
            }
        }
        private void comboBoxFunctionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxDetails();
        }

        private void FillComboBoxDetails()
        {
            string type; 
            type = ModbusFunctionCode.List.Where(M => M.Text.Replace("Read ", "").Split('(').First() == comboBoxFunctionCode.SelectedItem.ToString()).ToList().Select(M => M.ID).FirstOrDefault().ToString();
            if (type == "1" || type == "2" || type == "5" || type == "F")
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

        private void Address_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }

        private void Length_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }

        private void Port_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }
    }
}
