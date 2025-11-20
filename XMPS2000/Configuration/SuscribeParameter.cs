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
    internal class SuscribeParameter : Form
    {
        public static string res = "ok";
        public List<string> PublishList { get; set; }
        private Label labelTopic;
        private Label labelQos;
        private TextBox textBoxTopic;
        XMPS xm;
        private System.ComponentModel.IContainer components;
        ErrorProvider errorProvider;
        private ComboBox comboBoxQOS;
        private Button btnOk;
        private Label labelQosMessage;
        private Label labelTopicMessage;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        public int c;
        public String TextTopicName = "";
        public int RequestCount;
        private Button buttonReceivedMess;
        private Label labelReceivedMess;
        public List<SubscribeRequest> subscribeRequests = new List<SubscribeRequest>();
        public int keyvalue;
        //UndoRedoSubscribe
        private SubscribeManager suscribeManager = new SubscribeManager();
        private bool isEdit = false;
        private Subscribe oldSubscribe;
        public SuscribeParameter()
        {
            xm = XMPS.Instance;
            InitializeComponent();
            subscribeRequests.Clear();
            var subscribe = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
            keyvalue = subscribe.Count == 0 ? 1 : subscribe.Max(c => c.key) + 1;
        }

        public SuscribeParameter(Subscribe SR)
        {
            xm = XMPS.Instance;
            InitializeComponent();
            subscribeRequests.Clear();
            textBoxTopic.Text = SR.topic;
            comboBoxQOS.Text = SR.qos;
            subscribeRequests = new List<SubscribeRequest>(SR.SubRequest.Select(r => new SubscribeRequest
            {
                key = r.key,
                req = r.req,
                Tag = r.Tag
            }));
            keyvalue = SR.key;
            oldSubscribe = new Subscribe
            {
                SubRequest = new List<SubscribeRequest>(SR.SubRequest.Select(r => new SubscribeRequest
                {
                    key = r.key,
                    req = r.req,
                    Tag = r.Tag
                })),
                topic = SR.topic,
                qos = SR.qos,
                key = SR.key
            };
            isEdit = true;
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelTopic = new System.Windows.Forms.Label();
            this.labelQos = new System.Windows.Forms.Label();
            this.textBoxTopic = new System.Windows.Forms.TextBox();
            this.comboBoxQOS = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.labelTopicMessage = new System.Windows.Forms.Label();
            this.labelQosMessage = new System.Windows.Forms.Label();
            this.labelReceivedMess = new System.Windows.Forms.Label();
            this.buttonReceivedMess = new System.Windows.Forms.Button();
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
            this.labelTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTopic.Location = new System.Drawing.Point(25, 39);
            this.labelTopic.Name = "labelTopic";
            this.labelTopic.Size = new System.Drawing.Size(34, 13);
            this.labelTopic.TabIndex = 8;
            this.labelTopic.Text = "Topic";
            // 
            // labelQos
            // 
            this.labelQos.AutoSize = true;
            this.labelQos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQos.Location = new System.Drawing.Point(25, 75);
            this.labelQos.Name = "labelQos";
            this.labelQos.Size = new System.Drawing.Size(30, 13);
            this.labelQos.TabIndex = 7;
            this.labelQos.Text = "QOS";
            // 
            // textBoxTopic
            // 
            this.textBoxTopic.Location = new System.Drawing.Point(145, 39);
            this.textBoxTopic.Name = "textBoxTopic";
            this.textBoxTopic.Size = new System.Drawing.Size(122, 20);
            this.textBoxTopic.TabIndex = 1;
            this.textBoxTopic.Validating += new System.ComponentModel.CancelEventHandler(this.textTopic_Validating);
            // 
            // comboBoxQOS
            // 
            this.comboBoxQOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQOS.FormattingEnabled = true;
            this.comboBoxQOS.Items.AddRange(new object[] {
            0,
            1});
            this.comboBoxQOS.Location = new System.Drawing.Point(145, 72);
            this.comboBoxQOS.Name = "comboBoxQOS";
            this.comboBoxQOS.Size = new System.Drawing.Size(122, 21);
            this.comboBoxQOS.TabIndex = 2;
            this.comboBoxQOS.SelectedIndexChanged += new System.EventHandler(this.QOScomboBox_SelectedIndexChanged);
            this.comboBoxQOS.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxQOS_Validating);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(237, 144);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labelTopicMessage
            // 
            this.labelTopicMessage.AutoSize = true;
            this.labelTopicMessage.Location = new System.Drawing.Point(280, 42);
            this.labelTopicMessage.Name = "labelTopicMessage";
            this.labelTopicMessage.Size = new System.Drawing.Size(52, 13);
            this.labelTopicMessage.TabIndex = 6;
            this.labelTopicMessage.Text = "String(16)";
            // 
            // labelQosMessage
            // 
            this.labelQosMessage.AutoSize = true;
            this.labelQosMessage.Location = new System.Drawing.Point(280, 75);
            this.labelQosMessage.Name = "labelQosMessage";
            this.labelQosMessage.Size = new System.Drawing.Size(40, 13);
            this.labelQosMessage.TabIndex = 5;
            this.labelQosMessage.Text = "(0 or 1)";
            // 
            // labelReceivedMess
            // 
            this.labelReceivedMess.AutoSize = true;
            this.labelReceivedMess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReceivedMess.Location = new System.Drawing.Point(25, 111);
            this.labelReceivedMess.Name = "labelReceivedMess";
            this.labelReceivedMess.Size = new System.Drawing.Size(104, 13);
            this.labelReceivedMess.TabIndex = 4;
            this.labelReceivedMess.Text = "Received Messages";
            // 
            // buttonReceivedMess
            // 
            this.buttonReceivedMess.Location = new System.Drawing.Point(145, 106);
            this.buttonReceivedMess.Name = "buttonReceivedMess";
            this.buttonReceivedMess.Size = new System.Drawing.Size(122, 23);
            this.buttonReceivedMess.TabIndex = 3;
            this.buttonReceivedMess.Text = "Received Messages";
            this.buttonReceivedMess.UseVisualStyleBackColor = true;
            this.buttonReceivedMess.Click += new System.EventHandler(this.buttonReceivedMess_Click);
            // 
            // SuscribeParameter
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(337, 186);
            this.Controls.Add(this.buttonReceivedMess);
            this.Controls.Add(this.labelReceivedMess);
            this.Controls.Add(this.labelQosMessage);
            this.Controls.Add(this.labelTopicMessage);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.comboBoxQOS);
            this.Controls.Add(this.textBoxTopic);
            this.Controls.Add(this.labelQos);
            this.Controls.Add(this.labelTopic);
            this.Name = "SuscribeParameter";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void QOScomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = (int)comboBoxQOS.SelectedIndex;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateSubscribe())
            {
                if (textBoxTopic.Text.Trim().Length > 16)
                {
                    errorProvider.SetError(textBoxTopic, "The topic Length must be less than or equal to 16");
                    return;
                }
                TextTopicName = textBoxTopic.Text;
                var subscribe = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                var msublish = subscribe.Where(p => p.key == keyvalue).FirstOrDefault();
                if (msublish != null)
                {
                    xm.LoadedProject.Devices.Remove(msublish);
                }
                Subscribe obj = new Subscribe();
                obj.AddSubscribe(this.textBoxTopic.Text, this.comboBoxQOS.Text, keyvalue);
                foreach (SubscribeRequest subRequest in subscribeRequests)
                {
                    obj.AddPublishRequest(subRequest);
                }
                XMPS.Instance.LoadedProject.Devices.Add(obj);
                this.DialogResult = DialogResult.OK;
                this.Close();
                //set the cursor Position on topic before the last topic.
                if (xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().Count() > 1)
                {
                    int tpkWithReq = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().
                        Take(xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().Count() - 1).Where(p => p.SubRequest.Count > 0).Count();

                    xm.LoadedProject.NewAddedTagIndex = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().
                        Take(xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().Count() - 1).SelectMany(P => P.SubRequest).Count()
                        + (xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().Count() - 1) + tpkWithReq;
                }
                if (!isEdit)
                    suscribeManager.AddSubscribe(obj);
                else
                    suscribeManager.UpdateSubscribe(oldSubscribe, obj);
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
                e.Cancel = false;
                errorProvider.SetError(textBoxTopic, null);
            }
        }

        private void buttonReceivedMess_Click(object sender, EventArgs e)
        {
            int maxMQTTReqCount = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null)
                                         .SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MQTT").MaxCount ?? 0);
            if (subscribeRequests.Count >= maxMQTTReqCount)
            {
                MessageBox.Show("Maximum limit of request addition is reached, Key name can't be added", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string topic = this.textBoxTopic.Text;
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "ADD SUBSCRIBE REQUEST";
            SuscribeRequest userControl = new SuscribeRequest(keyvalue);
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            userControl.Text = "ADD SUBSCRIBE REQUEST";
            DialogResult status = userControl.ShowDialog();
            if (status == DialogResult.OK)
            {
                SubscribeRequest sr = new SubscribeRequest();
                sr.topic = keyvalue;
                sr.req = userControl.Keyname;
                sr.Tag = XMProValidator.GetTheAddressFromTag(userControl.Tagname);
                sr.key = subscribeRequests.Count;
                if (subscribeRequests.Where(S => S.req == userControl.Keyname).Any())
                {
                    MessageBox.Show("Duplicate keyname found, Key name can't be added", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    subscribeRequests.Add(sr);
                }
            }
            c++;
            //}

            RequestCount = c;
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
                e.Cancel = true;
                errorProvider.SetError(comboBoxQOS, null);
            }
        }

        public bool ValidateSubscribe()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(textBoxTopic.Text) || textBoxTopic.TextLength > 16)
            {
                errorProvider.SetError(textBoxTopic, "The topic length must be less than or equal to 16");
                isValid = false;
            }
            if (comboBoxQOS.Text != "0" && comboBoxQOS.Text != "1")
            {
                errorProvider.SetError(comboBoxQOS, "Please enter either 0 or 1.");
                isValid = false;
            }

            return isValid;
        }

    }
}
