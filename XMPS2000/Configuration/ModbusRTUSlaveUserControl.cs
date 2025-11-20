using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.LadderLogic;
using XMPS2000.Resource_File;
using XMPS2000.UndoRedoGridLayout;

namespace XMPS2000.Configuration
{
    public partial class ModbusRTUSlaveUserControl : UserControl
    {

        private string currentLogicalAddress;
        private MODBUSRTUSlaves_Slave dataSource;
        XMPS xm;
        private string _slaveName;
        ErrorProvider errorProvider;

        //UndoRedo
        private ModbusRTUSlaveManager ModbusRTUSlaveManager = new ModbusRTUSlaveManager();
        private bool isEdit = false;
        private MODBUSRTUSlaves_Slave oldSlavedate;
        public ModbusRTUSlaveUserControl(string slaveName)
        {
            xm = XMPS.Instance;
            _slaveName = slaveName;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            InitializeComponent();
            errorProvider =new ErrorProvider();
            ////Change names of Labels, take names from LabelNames Resource file
            this.lblAddress.Text = LabelNames.Address;
            this.Address.Text = LabelNames.comtoutrange;
            this.lblLength.Text = LabelNames.Length;
            this.lblVariable.Text = LabelNames.Variable;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List.Where(M => M.ID < 5).ToList();
        }


        public ModbusRTUSlaveUserControl()
        {
            xm = XMPS.Instance;
            //_slaveName = slaveName;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            InitializeComponent();

            ////Change names of Labels, take names from LabelNames Resource file
            this.lblAddress.Text = LabelNames.Address;
            this.Address.Text = LabelNames.comtoutrange;
            this.lblLength.Text = LabelNames.Length;
            this.Length.Text = LabelNames.LengthRange;
            this.lblVariable.Text = LabelNames.Variable;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List.Where(M => M.ID <5).ToList();
        }

        private void ModbusRTUSlaveUserControl_Load(object sender, EventArgs e)
        {
            DataBind();
        }


        private void DataBind()
        {
            var modBUSRTUMaster = (MODBUSRTUSlaves)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
            if (modBUSRTUMaster != null)
            {
                var slaves = modBUSRTUMaster.Slaves;

                this.dataSource = (MODBUSRTUSlaves_Slave)slaves.Where(s => s.Name == _slaveName).FirstOrDefault();
                if (this.dataSource != null)
                {
                    this.Address.Value = this.dataSource.Address;
                    this.Length.Value = this.dataSource.Length;
                    this.textBoxVariable.Text = this.dataSource.Variable.ToString();
                    this.comboBoxFunctionCode.Text = this.dataSource.FunctionCode.ToString();
                    this.textBoxVariable.Text = this.dataSource.Variable;
                    this.TagEnable.SelectedIndex = TagEnable.Items.IndexOf(this.dataSource.Tag);
                    oldSlavedate = new MODBUSRTUSlaves_Slave
                    {
                        Address = dataSource.Address,
                        Length = dataSource.Length,
                        Variable = dataSource.Variable,
                        Tag = dataSource.Tag,
                        FunctionCode = dataSource.FunctionCode,
                        Name = dataSource.Name
                    };
                    isEdit = true;
                }
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


        private void comboBoxFunctionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTagDropDown();
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


        /// <summary>
        /// Check if the entered Word Address is valid or not and raise error if any
        /// </summary>
        /// <param name="control"></param> Control for which the check is required
        /// <param name="e"></param> Cancel Event Handler
        /// <param name="errorMessageToDisplay"></param> Error Message to show
        private void ValidateWordAddress(Control control, CancelEventArgs e, string errorMessageToDisplay)
        {
            var checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();

            if (errorProvider == null)
            {
                errorProvider = new ErrorProvider();
            }
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
                if (errorProvider == null)
                {
                   errorProvider = new ErrorProvider();
                }

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

        private void textBoxVariable_Validating(object sender, CancelEventArgs e)
        {
            string type;
            // Check for select value in the function code combobox according to this required validation changes 
            type = $"{((ModbusFunctionCode)comboBoxFunctionCode.SelectedItem).ID:X2}";
            if (textBoxVariable.Text == "") return;

                    if (type == "01" || type == "02" || type == "05" || type == "0F")
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

        private void comboBoxFunctionCode_Validating(object sender, CancelEventArgs e)
        {
            // Check for select value in the function code combobox according to this required validation changes 
            textBoxVariable_Validating(sender, e);
        }

        private void Address_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }

        private void Length_Leave(object sender, EventArgs e)
        {
            XMProValidator.checkvalue(sender);
        }


        private int GetFunctionCode(string functionCode)
        {
            switch (functionCode)
            {
                case "Read Coil (01)":
                    return 01;
                case "Read Discrete Input (02)":
                    return 02;
                case "Write Single Coil (05)":
                    return 05;
                case "Write Multiple Coils (15)":
                    return 15;
                default:
                    return -1;
            }
        }


        public void CopyDataBind(string slaveName, MODBUSRTUSlaves_Slave mODBUSRTUSlaves_Slave)
        {
            
            _slaveName = slaveName;
            this.Address.Value = mODBUSRTUSlaves_Slave.Address;
            this.Length.Value = mODBUSRTUSlaves_Slave.Length;
            this.textBoxVariable.Text = mODBUSRTUSlaves_Slave.Variable;
            this.TagEnable.Text = mODBUSRTUSlaves_Slave.Tag;
            comboBoxFunctionCode.DataSource = ModbusFunctionCode.List;
            List<ModbusFunctionCode> functionCodeList = ModbusFunctionCode.List;
            ModbusFunctionCode selectedFunctionCode = (ModbusFunctionCode)functionCodeList.Where(T => T.Text == mODBUSRTUSlaves_Slave.FunctionCode).FirstOrDefault();
            comboBoxFunctionCode.SelectedIndex = ModbusFunctionCode.List.IndexOf(selectedFunctionCode);
            this.TagEnable.SelectedIndex = TagEnable.Items.IndexOf(mODBUSRTUSlaves_Slave.Tag);

        }

        private void btnSave_Click(object sender, EventArgs e)
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
                var moDBUSRTUSlaves = (MODBUSRTUSlaves)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
                if (moDBUSRTUSlaves.Slaves.Where(M => M.Variable == textBoxVariable.Text.ToString() && M.Name != _slaveName && M.FunctionCode == comboBoxFunctionCode.Text.ToString()).Count() > 0)
                {
                    MessageBox.Show("Variable tag is already used in another request kindly select another variable tag", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ///<>
                ///For the Not Allow to Repeate Address Value In Modbus Requests.
                ///
                var AddressValueChecked = moDBUSRTUSlaves.Slaves.Where(M => M.Address.ToString() == Address.Text.ToString() && M.Name != _slaveName && M.FunctionCode == comboBoxFunctionCode.Text.ToString()).FirstOrDefault();
                if (AddressValueChecked != null)
                {
                    MessageBox.Show("Address Values is already used Please Used Next Address Value", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ///<>
                ///Check for the if if the Request contain Double Word 
                ///
                string AddValue = Address.Text.ToString();
                int CurrentAddValue = int.Parse(AddValue);
                int preAddValue = CurrentAddValue - 1;
                int nextAddValue = CurrentAddValue + 1;
                var AddressValueCheckedPrev = moDBUSRTUSlaves.Slaves.Where(M => M.Address.ToString() == preAddValue.ToString() &&  M.FunctionCode == comboBoxFunctionCode.Text && M.Name != _slaveName).FirstOrDefault();
                if (AddressValueCheckedPrev != null)
                {
                    if (AddressValueCheckedPrev.Variable.StartsWith("Q0:"))
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
                var AddressValueCheckedNext = moDBUSRTUSlaves.Slaves.Where(M => M.Address.ToString() == nextAddValue.ToString() &&  M.FunctionCode == comboBoxFunctionCode.Text && M.Name != _slaveName).FirstOrDefault();
                if (AddressValueCheckedNext != null)
                {
                    XMIOConfig usedTagInRequest = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textBoxVariable.Text.ToString()).FirstOrDefault();
                    if (usedTagInRequest != null) 
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
                if (moDBUSRTUSlaves != null)
                {
                    moDBUSRTUSlaves.AddSlave(
                        this._slaveName,
                        (int)this.Address.Value,
                        (int)this.Length.Value,
                        this.textBoxVariable.Text,
                        this.TagEnable.Text,
                        this.comboBoxFunctionCode.Text
                        );

                    xm.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MODBUSRTUSlaves");
                    xm.LoadedProject.Devices.Add(moDBUSRTUSlaves);
                    if(!isEdit)
                    {
                        ModbusRTUSlaveManager.AddMODBUSRTUSlave(new MODBUSRTUSlaves_Slave {
                                 Name = this._slaveName,
                                 Address = (int)this.Address.Value,
                                 Length = (int)this.Length.Value,
                                 Tag = this.TagEnable.Text,
                                 FunctionCode = this.comboBoxFunctionCode.Text,
                                 Variable = this.textBoxVariable.Text
                            });
                    }
                    else
                    {
                       ModbusRTUSlaveManager.UpdateMODBUSRTUSlave(oldSlavedate, new MODBUSRTUSlaves_Slave
                       {
                           Name = this._slaveName,
                           Address = (int)this.Address.Value,
                           Length = (int)this.Length.Value,
                           Tag = this.TagEnable.Text,
                           FunctionCode = this.comboBoxFunctionCode.Text,
                           Variable = this.textBoxVariable.Text
                       });
                    }
                }
                xm.MarkProjectModified(true);
                this.ParentForm.Close();
                this.ParentForm.DialogResult = DialogResult.OK;
                xm.LoadedProject.NewAddedTagIndex = moDBUSRTUSlaves.Slaves.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

    }
}
