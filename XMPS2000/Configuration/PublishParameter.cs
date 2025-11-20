using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.LadderLogic;
using XMPS2000.UndoRedoGridLayout;

namespace XMPS2000.Configuration
{
    internal class PublishParameter : Form
    {
        public static string res = "ok";
        public List<string> PublishList { get; set; }
        private Label labelTopic;
        private Label labelPayload;
        private Label labelQos;
        private Label labelRetainFlag;
        private TextBox textBoxTopic;
        XMPS xm;
        private System.ComponentModel.IContainer components;
        private Button btnaddPubReq;
        ErrorProvider errorProvider;
        private ComboBox comboBoxQOS;
        private Button btnOk;
        private Label labelQosMessage;
        private Label labelRetainFlagMess;
        private Label labelTopicMessage;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        public int c;
        public int countvalue = 0;
        public String TextTopicName = "";
        public int RequestCount;
        public List<PubRequest> publishRequests = new List<PubRequest>();
        private ComboBox comboBoxRetainFlag;
        public int Keyvalue;
        //UndoRedo Publish
        private PublishManager publishManager = new PublishManager();
        private bool isEdit = false;
        private Publish oldPublish;
        public PublishParameter()
        {
            xm = XMPS.Instance;
            InitializeComponent();
            publishRequests.Clear();
            var subscribe = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
            Keyvalue = subscribe.Count == 0 ? 1 : subscribe.Max(c => c.keyvalue) + 1;
        }

        public PublishParameter(Publish varpublish)
        {
            xm = XMPS.Instance;
            InitializeComponent();

            publishRequests.Clear();
            textBoxTopic.Text = varpublish.topic;
            comboBoxRetainFlag.Text = varpublish.retainflag;
            comboBoxQOS.Text = varpublish.qos;
            publishRequests = new List<PubRequest>(varpublish.PubRequest.Select(r => new PubRequest
            {
                Keyvalue = r.Keyvalue,
                req = r.req,
                Tag = r.Tag
            }));
            Keyvalue = varpublish.keyvalue;
            oldPublish = new Publish
            {
                PubRequest = new List<PubRequest>(varpublish.PubRequest.Select(r => new PubRequest
                {
                    Keyvalue = r.Keyvalue,
                    req = r.req,
                    Tag = r.Tag
                })),
                topic = varpublish.topic,
                retainflag = varpublish.retainflag,
                qos = varpublish.qos,
                keyvalue = varpublish.keyvalue
            };
            isEdit = true;
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelTopic = new System.Windows.Forms.Label();
            this.labelPayload = new System.Windows.Forms.Label();
            this.labelQos = new System.Windows.Forms.Label();
            this.labelRetainFlag = new System.Windows.Forms.Label();
            this.textBoxTopic = new System.Windows.Forms.TextBox();
            this.btnaddPubReq = new System.Windows.Forms.Button();
            this.comboBoxQOS = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.labelTopicMessage = new System.Windows.Forms.Label();
            this.labelRetainFlagMess = new System.Windows.Forms.Label();
            this.labelQosMessage = new System.Windows.Forms.Label();
            this.comboBoxRetainFlag = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // labelTopic
            // 
            this.labelTopic.AutoSize = true;
            this.labelTopic.Location = new System.Drawing.Point(57, 39);
            this.labelTopic.Name = "labelTopic";
            this.labelTopic.Size = new System.Drawing.Size(34, 13);
            this.labelTopic.TabIndex = 0;
            this.labelTopic.Text = "Topic";
            // 
            // labelPayload
            // 
            this.labelPayload.AutoSize = true;
            this.labelPayload.Location = new System.Drawing.Point(57, 70);
            this.labelPayload.Name = "labelPayload";
            this.labelPayload.Size = new System.Drawing.Size(45, 13);
            this.labelPayload.TabIndex = 1;
            this.labelPayload.Text = "Payload";
            // 
            // labelQos
            // 
            this.labelQos.AutoSize = true;
            this.labelQos.Location = new System.Drawing.Point(57, 104);
            this.labelQos.Name = "labelQos";
            this.labelQos.Size = new System.Drawing.Size(30, 13);
            this.labelQos.TabIndex = 3;
            this.labelQos.Text = "QOS";
            // 
            // labelRetainFlag
            // 
            this.labelRetainFlag.AutoSize = true;
            this.labelRetainFlag.Location = new System.Drawing.Point(57, 142);
            this.labelRetainFlag.Name = "labelRetainFlag";
            this.labelRetainFlag.Size = new System.Drawing.Size(61, 13);
            this.labelRetainFlag.TabIndex = 4;
            this.labelRetainFlag.Text = "Retain Flag";
            // 
            // textBoxTopic
            // 
            this.textBoxTopic.Location = new System.Drawing.Point(148, 39);
            this.textBoxTopic.Name = "textBoxTopic";
            this.textBoxTopic.Size = new System.Drawing.Size(100, 20);
            this.textBoxTopic.TabIndex = 1;
            this.textBoxTopic.Validating += new System.ComponentModel.CancelEventHandler(this.textTopic_Validating);
            // 
            // btnaddPubReq
            // 
            this.btnaddPubReq.Location = new System.Drawing.Point(148, 65);
            this.btnaddPubReq.Name = "btnaddPubReq";
            this.btnaddPubReq.Size = new System.Drawing.Size(100, 23);
            this.btnaddPubReq.TabIndex = 2;
            this.btnaddPubReq.Text = "Add PUB REQ";
            this.btnaddPubReq.UseVisualStyleBackColor = true;
            this.btnaddPubReq.Click += new System.EventHandler(this.addPubReq_Click);
            // 
            // comboBoxQOS
            // 
            this.comboBoxQOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQOS.FormattingEnabled = true;
            this.comboBoxQOS.Items.AddRange(new object[] {
            0,
            1});
            this.comboBoxQOS.Location = new System.Drawing.Point(148, 101);
            this.comboBoxQOS.Name = "comboBoxQOS";
            this.comboBoxQOS.Size = new System.Drawing.Size(100, 21);
            this.comboBoxQOS.TabIndex = 3;
            this.comboBoxQOS.SelectedIndexChanged += new System.EventHandler(this.QOScomboBox_SelectedIndexChanged);
            this.comboBoxQOS.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxQOS_Validating);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(173, 177);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labelTopicMessage
            // 
            this.labelTopicMessage.AutoSize = true;
            this.labelTopicMessage.Location = new System.Drawing.Point(288, 45);
            this.labelTopicMessage.Name = "labelTopicMessage";
            this.labelTopicMessage.Size = new System.Drawing.Size(52, 13);
            this.labelTopicMessage.TabIndex = 11;
            this.labelTopicMessage.Text = "String(16)";
            // 
            // labelRetainFlagMess
            // 
            this.labelRetainFlagMess.AutoSize = true;
            this.labelRetainFlagMess.Location = new System.Drawing.Point(288, 142);
            this.labelRetainFlagMess.Name = "labelRetainFlagMess";
            this.labelRetainFlagMess.Size = new System.Drawing.Size(34, 13);
            this.labelRetainFlagMess.TabIndex = 12;
            this.labelRetainFlagMess.Text = "(Bool)";
            // 
            // labelQosMessage
            // 
            this.labelQosMessage.AutoSize = true;
            this.labelQosMessage.Location = new System.Drawing.Point(288, 104);
            this.labelQosMessage.Name = "labelQosMessage";
            this.labelQosMessage.Size = new System.Drawing.Size(40, 13);
            this.labelQosMessage.TabIndex = 13;
            this.labelQosMessage.Text = "(0 or 1)";
            // 
            // comboBoxRetainFlag
            // 
            this.comboBoxRetainFlag.FormattingEnabled = true;
            this.comboBoxRetainFlag.Items.AddRange(new object[] {
            0,
            1});
            this.comboBoxRetainFlag.Location = new System.Drawing.Point(148, 134);
            this.comboBoxRetainFlag.Name = "comboBoxRetainFlag";
            this.comboBoxRetainFlag.Size = new System.Drawing.Size(100, 21);
            this.comboBoxRetainFlag.TabIndex = 14;
            this.comboBoxRetainFlag.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxRetainFlag_Validating);
            // 
            // PublishParameter
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(366, 228);
            this.Controls.Add(this.comboBoxRetainFlag);
            this.Controls.Add(this.labelQosMessage);
            this.Controls.Add(this.labelRetainFlagMess);
            this.Controls.Add(this.labelTopicMessage);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.comboBoxQOS);
            this.Controls.Add(this.btnaddPubReq);
            this.Controls.Add(this.textBoxTopic);
            this.Controls.Add(this.labelRetainFlag);
            this.Controls.Add(this.labelQos);
            this.Controls.Add(this.labelPayload);
            this.Controls.Add(this.labelTopic);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PublishParameter";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void addPubReq_Click(object sender, EventArgs e)
        {

            int maxMQTTReqCount = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null)
                                         .SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MQTT").MaxCount ?? 0);
            if (publishRequests.Count >= maxMQTTReqCount)
            {
                MessageBox.Show("Maximum limit of request addition is reached, Key name can't be added", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string topic = this.textBoxTopic.Text;
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "ADD PUBLISH REQUEST";
            PublishRequest userControl = new PublishRequest(Keyvalue);
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            userControl.Text = "ADD PUBLISH REQUEST";
            DialogResult status = userControl.ShowDialog();
            if (status == DialogResult.OK)
            {
                PubRequest pr = new PubRequest();
                pr.topic = Keyvalue;
                pr.req = userControl.Keyname;
                pr.Tag = XMProValidator.GetTheAddressFromTag(userControl.Tagname);
                //pr.tag = userControl.Tagname;
                pr.Keyvalue = publishRequests.Count;
                if (publishRequests.Where(P => P.req == userControl.Keyname).Any())
                {
                    MessageBox.Show("Duplicate keyname found, Key name can't be added", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    publishRequests.Add(pr);
                }
            }
            c++;
            // }

            RequestCount = c;
        }

        private void QOScomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = (int)comboBoxQOS.SelectedIndex;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateMQTT())
            {
                if (textBoxTopic.Text.Trim().Length > 16)
                {
                    errorProvider.SetError(textBoxTopic, "The topic Length must be less than or equal to 16");
                    return;
                }
                TextTopicName = textBoxTopic.Text;
                var Listpublish = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
                var mpublish = Listpublish.Where(p => p.keyvalue == Keyvalue).FirstOrDefault();
                Publish obj = new Publish();
                if (mpublish != null)
                {
                    xm.LoadedProject.Devices.Remove(mpublish);
                }
                obj.AddPublish(this.textBoxTopic.Text, this.comboBoxQOS.Text, this.comboBoxRetainFlag.Text, Keyvalue);

                foreach (PubRequest pubRequest in publishRequests)
                {
                    obj.AddPublishRequest(pubRequest);
                }
                XMPS.Instance.LoadedProject.Devices.Add(obj);
                if (!isEdit)
                    publishManager.AddPublish(obj);
                else
                {
                    publishManager.UpdatePublish(oldPublish, obj);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                //set the cursor Position on topic before the last topic.
                if (xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().Count() > 1)
                {
                    int tpkWithReq = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().
                        Take(xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().Count() - 1).Where(p => p.PubRequest.Count > 0).Count();

                    xm.LoadedProject.NewAddedTagIndex = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().
                        Take(xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().Count() - 1).SelectMany(P => P.PubRequest).Count()
                        + (xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().Count() - 1) + tpkWithReq;
                }
            }
        }



        private void textTopic_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (/*string.IsNullOrEmpty(textBoxTopic.Text) ||*/ textBoxTopic.TextLength > 16)
            {
                errorProvider.SetError(textBoxTopic, "The topic length must be less than or equal to 16");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(textBoxTopic, null);
                e.Cancel = false;
            }
        }

        private void textRetainFlag_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            bool isValid = false;

            if (textBox.Text.ToLower() == "true" || textBox.Text.ToLower() == "false" || textBox.Text.ToLower() == "1" || textBox.Text.ToLower() == "0")
            {
                isValid = true;
                errorProvider.SetError(textBox, isValid ? "" : "Please enter valid value");
            }
            else
            {
                errorProvider.SetError(textBox, null);
                e.Cancel = false;
            }
        }

        private void comboBoxQOS_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedValue = comboBox.Text;

            if (selectedValue != "0" && selectedValue != "1")
            {
                e.Cancel = true;
                errorProvider.SetError(comboBoxQOS, "Please enter either 0 or 1.");
            }
            else
            {
                errorProvider.SetError(comboBoxQOS, null);
                e.Cancel = false;
            }
        }

        public bool ValidateMQTT()
        {
            bool isValid = true;
            if (textBoxTopic.Text == "" || textBoxTopic.Text.Length > 16)
            {
                errorProvider.SetError(textBoxTopic, "The topic length must be less than or equal to 16");
                isValid = false;
            }
            if (comboBoxQOS.Text != "0" && comboBoxQOS.Text != "1")
            {
                errorProvider.SetError(comboBoxQOS, "Please enter either 0 or 1.");
                isValid = false;
            }
            string flag = comboBoxRetainFlag.Text;
            if (flag == "0" || flag == "1" || flag == "true" || flag == "false")
            {

            }
            else
            {
                errorProvider.SetError(comboBoxRetainFlag, "Please enter 'true','false', 1, 0");
                isValid = false;
            }
            return isValid;
        }

        private void comboBoxRetainFlag_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string flag = comboBoxRetainFlag.Text;
            if (flag == "0" || flag == "1" || flag == "true" || flag == "false")
            {
                errorProvider.SetError(comboBoxRetainFlag, null);
                e.Cancel = false;
            }
            else
            {
                errorProvider.SetError(comboBoxRetainFlag, "Please enter 'true','false', 1, 0");
                e.Cancel = true;
            }
        }
    }
}
