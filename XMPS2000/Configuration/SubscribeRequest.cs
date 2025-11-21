using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.LadderLogic;

namespace XMPS2000.Configuration
{

    public class SuscribeRequest : Form
    {
        XMPS xm;
        private Label lblTopic;
        private Label lblKeyname;
        private Label lblVariable;
        private TextBox textTopic;
        private TextBox textKeyname;
        private ComboBox comboBoxTaglist;
        private TextBox textNewtag;
        private Label lblString;
        private Button btnSave;
        ErrorProvider errorProvider;
        public string topicPublish = "";
        List<string> tags = new List<string>();
        public string Keyname;
        public string Tagname;
        internal int keyValue;
        internal int TopicId;
        private bool isEditOn = false;

        public SuscribeRequest(int Topic_Id)
        {
            xm = XMPS.Instance;
            errorProvider = new ErrorProvider();
            InitializeComponent();
            this.textTopic.Text = xm.GetTopicName("Subscribe", Topic_Id);
            TopicId = Topic_Id;
            tags.Add("Select Tag Name");
            if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
            {
                tags.AddRange(xm.LoadedProject.Tags.Where(t => !(t.Tag.EndsWith("OR") || t.Tag.EndsWith("OL"))).Select(t => t.Tag).ToList());
            }
            else
            {
                tags.AddRange(xm.LoadedProject.Tags.Select(t => t.Tag).ToList());
            }
            this.comboBoxTaglist.DataSource = tags;
            var sublist = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
            keyValue = sublist.Where(s => s.key == Topic_Id).Select(s => s.SubRequest.Count()).FirstOrDefault();
        }

        public SuscribeRequest(SubscribeRequest SR, string topicName = "", bool isEdit = false)
        {
            this.isEditOn = false;
            xm = XMPS.Instance;
            errorProvider = new ErrorProvider();
            InitializeComponent();
            tags.AddRange(xm.LoadedProject.Tags.Select(t => t.Tag).ToList());
            this.comboBoxTaglist.DataSource = tags;

            this.textTopic.Text = topicName == "" ? xm.GetTopicName("Subscribe", SR.topic) : topicName;
            this.textKeyname.Text = SR.req;
            if (SR.Tag != null)
            {
                if (!SR.Tag.Contains(":"))
                {
                    this.comboBoxTaglist.Text = SR.Tag;
                    this.textNewtag.Text = XMProValidator.GetTheAddressFromTag(SR.Tag);
                }
                else
                {
                    this.comboBoxTaglist.Text = XMProValidator.GetTheTagnameFromAddress(SR.Tag);
                    this.textNewtag.Text = SR.Tag;
                }
            }

            TopicId = SR.topic;
            keyValue = SR.key;
            this.isEditOn = isEdit;
        }

        public SuscribeRequest(SubscribeRequest SR, int topicId, string topicName = "")
        {
            this.isEditOn = false;
            xm = XMPS.Instance;
            errorProvider = new ErrorProvider();
            InitializeComponent();
            tags.AddRange(xm.LoadedProject.Tags.Select(t => t.Tag).ToList());
            this.comboBoxTaglist.DataSource = tags;

            this.textTopic.Text = topicName == "" ? xm.GetTopicName("Subscribe", SR.topic) : topicName;
            this.textKeyname.Text = SR.req;
            this.comboBoxTaglist.Text = SR.Tag;
            this.textNewtag.Text = XMProValidator.GetTheAddressFromTag(SR.Tag);
            ////for Whole Block of Sub copy
            TopicId = topicId;
            keyValue = SR.key;
        }


        private void InitializeComponent()
        {
            this.lblTopic = new System.Windows.Forms.Label();
            this.lblKeyname = new System.Windows.Forms.Label();
            this.lblVariable = new System.Windows.Forms.Label();
            this.textTopic = new System.Windows.Forms.TextBox();
            this.textKeyname = new System.Windows.Forms.TextBox();
            this.comboBoxTaglist = new System.Windows.Forms.ComboBox();
            this.textNewtag = new System.Windows.Forms.TextBox();
            this.lblString = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTopic
            // 
            this.lblTopic.AutoSize = true;
            this.lblTopic.Location = new System.Drawing.Point(19, 15);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new System.Drawing.Size(34, 13);
            this.lblTopic.TabIndex = 0;
            this.lblTopic.Text = "Topic";
            // 
            // lblKeyname
            // 
            this.lblKeyname.AutoSize = true;
            this.lblKeyname.Location = new System.Drawing.Point(19, 48);
            this.lblKeyname.Name = "lblKeyname";
            this.lblKeyname.Size = new System.Drawing.Size(57, 13);
            this.lblKeyname.TabIndex = 1;
            this.lblKeyname.Text = "Key_name";
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Location = new System.Drawing.Point(19, 82);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(45, 13);
            this.lblVariable.TabIndex = 2;
            this.lblVariable.Text = "Variable";
            // 
            // textTopic
            // 
            this.textTopic.Enabled = false;
            this.textTopic.Location = new System.Drawing.Point(82, 12);
            this.textTopic.Name = "textTopic";
            this.textTopic.Size = new System.Drawing.Size(138, 20);
            this.textTopic.TabIndex = 1;
            // 
            // textKeyname
            // 
            this.textKeyname.Location = new System.Drawing.Point(82, 44);
            this.textKeyname.Name = "textKeyname";
            this.textKeyname.Size = new System.Drawing.Size(138, 20);
            this.textKeyname.TabIndex = 2;
            this.textKeyname.Validating += new System.ComponentModel.CancelEventHandler(this.textKeyname_Validating);
            // 
            // comboBoxTaglist
            // 
            this.comboBoxTaglist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTaglist.FormattingEnabled = true;
            this.comboBoxTaglist.Location = new System.Drawing.Point(82, 78);
            this.comboBoxTaglist.Name = "comboBoxTaglist";
            this.comboBoxTaglist.Size = new System.Drawing.Size(138, 21);
            this.comboBoxTaglist.TabIndex = 3;
            this.comboBoxTaglist.SelectedIndexChanged += new System.EventHandler(this.comboBoxTaglist_SelectedIndexChanged);
            // 
            // textNewtag
            // 
            this.textNewtag.Location = new System.Drawing.Point(235, 78);
            this.textNewtag.Name = "textNewtag";
            this.textNewtag.Size = new System.Drawing.Size(113, 20);
            this.textNewtag.TabIndex = 4;
            this.textNewtag.Validating += new System.ComponentModel.CancelEventHandler(this.textNewtag_Validating);
            // 
            // lblString
            // 
            this.lblString.AutoSize = true;
            this.lblString.Location = new System.Drawing.Point(235, 48);
            this.lblString.Name = "lblString";
            this.lblString.Size = new System.Drawing.Size(50, 13);
            this.lblString.TabIndex = 9;
            this.lblString.Text = "string(16)";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(235, 124);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SuscribeRequest
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(355, 151);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblString);
            this.Controls.Add(this.textNewtag);
            this.Controls.Add(this.comboBoxTaglist);
            this.Controls.Add(this.textKeyname);
            this.Controls.Add(this.textTopic);
            this.Controls.Add(this.lblVariable);
            this.Controls.Add(this.lblKeyname);
            this.Controls.Add(this.lblTopic);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SuscribeRequest";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }
            if (textKeyname.Text.Trim().Length > 16)
            {
                errorProvider.SetError(textKeyname, "The topic Length must be less than or equal to 16");
                return;
            }
            if (string.IsNullOrEmpty(textKeyname.Text))
            {
                errorProvider.SetError(textKeyname, "The topic Length must be less than or equal to 16");
                return;
            }

            if (this.textNewtag != null)
            {
                this.comboBoxTaglist.Text = "";
            }
            if (this.textNewtag != null && this.textNewtag.Text != "")
            {
                var sublist = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                Subscribe subtop = sublist.Where(s => s.key == TopicId).FirstOrDefault();
                if (subtop != null)
                {
                    int maxMQTTReqCount = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null)
                                      .SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MQTT").MaxCount ?? 0);
                    if (subtop.SubRequest.Count() >= maxMQTTReqCount && !isEditOn)
                    {
                        MessageBox.Show("Maximum limit of request addition is reached, Key name can't be added", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (subtop.SubRequest.Where(r => r.req == textKeyname.Text && r.key != keyValue).Count() > 0 && isEditOn)
                    {
                        MessageBox.Show("Duplicate keyname found, Key name can't be added", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (subtop.SubRequest.Where(r => r.req == textKeyname.Text.ToString() && r.topic == TopicId).Count() > 0 && !isEditOn)
                    {
                        MessageBox.Show("Duplicate keyname found, Key name can't be added", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                var checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textNewtag.Text.ToString()).FirstOrDefault();
                if (checktag != null)
                    this.comboBoxTaglist.Text = checktag.Tag.ToString();
                Keyname = this.textKeyname.Text;
                Tagname = this.comboBoxTaglist.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please add variable tag", "XMPS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void textNewtag_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                var checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textNewtag.Text.ToString()).FirstOrDefault();
                if (checktag != null)
                {
                    this.comboBoxTaglist.Text = checktag.Tag.ToString();
                }
                else if (!textNewtag.Text.Contains('.') && (textNewtag.Text.StartsWith("Q0") || textNewtag.Text.StartsWith("I1")))
                {
                    checktag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == textNewtag.Text.ToString() + ".00").FirstOrDefault();
                    this.comboBoxTaglist.Text = "";
                }
                else if (!CheckIsValidAddress(textNewtag.Text.ToString()))
                {
                    e.Cancel = true;
                    errorProvider.SetError(textNewtag, "Not Valid Logical Address");
                }
                else
                {
                    string newTag = "";
                    if (textNewtag.Text != "")
                    {
                        if (MessageBox.Show("Variable Tag is not Added You Want to Add", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            XMProForm tempForm = new XMProForm();
                            tempForm.StartPosition = FormStartPosition.CenterParent;
                            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                            tempForm.Text = "Add New Address Added in Logic";
                            newTag = textNewtag.Text;
                            TagsUserControl userControl = new TagsUserControl(0, newTag);
                            tempForm.Height = userControl.Height + 25;
                            tempForm.Width = userControl.Width;
                            tempForm.Controls.Add(userControl);
                            var frmTemp = this.ParentForm as frmMain;
                            DialogResult status = tempForm.ShowDialog(frmTemp);
                            if (status == DialogResult.OK)
                            {
                                tags.Add("Select Tag Name");
                                tags.AddRange(xm.LoadedProject.Tags.Select(t => t.Tag).ToList());
                                this.comboBoxTaglist.DataSource = null;
                                this.comboBoxTaglist.DataSource = tags;
                                textNewtag.Text = XMProValidator.GetTheAddressFromTag(userControl.TagText);
                                string tagName = XMProValidator.GetTheTagnameFromAddress(textNewtag.Text);
                                comboBoxTaglist.SelectedIndex = comboBoxTaglist.Items.IndexOf(tagName);
                                newTag = textNewtag.Text;
                            }
                        }
                    }
                    else
                    {
                        e.Cancel = false;
                        errorProvider.SetError(textNewtag, "Please Add Tag");
                    }
                    this.textNewtag.Text = newTag;
                    this.comboBoxTaglist.Text = XMProValidator.GetTheTagnameFromAddress(textNewtag.Text); ;
                    return;

                }
            }
        }
        private bool CheckIsValidAddress(string tag)
        {
            if (tag.Contains(":") && (tag.StartsWith("F2") || tag.StartsWith("W4") || tag.StartsWith("P5") || tag.StartsWith("T6") || tag.StartsWith("C7")))
            {
                return true;
            }
            return false;
        }
        private void comboBoxTaglist_SelectedIndexChanged(object sender, EventArgs e)
        {
            textNewtag.Text = comboBoxTaglist.Text == "Select Tag Name" ? null : XMProValidator.GetTheAddressFromTag(comboBoxTaglist.Text.ToString());
        }

        private void textKeyname_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (textKeyname.Text.Length > 16)
            {
                errorProvider.SetError(textKeyname, "The topic Length must be less than or equal to 16");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textKeyname, null);
            }
        }


    }
}