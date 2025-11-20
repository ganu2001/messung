using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XMPS2000.Core.Devices;
using XMPS2000.Core;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.IO;

namespace XMPS2000.Configuration
{
    public class MqttSettingsUserControl : UserControl
    {
        private MQTTForm dataSource;
        XMPS xm;


        private GroupBox groupBox1;
        private Label DataType;
        private Label Value;
        private Label lblName;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox hostname;
        private Label label9;
        private Button btnOk;
        private TextBox _cleanSession;
        private TextBox _passWord;
        private TextBox _userName;
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;

        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;

        private Button BtnPrivateKey;
        private Button btnClientCertificate;
        private Button btnCA3Certificate;
        private TextBox PrivateKey;
        private TextBox ClientCertificate;
        private TextBox CA3Certificate;


        ErrorProvider errorProvider;
        ToolTip tp = new ToolTip();
        private ContextMenuStrip contextMenuStrip2;
        private ContextMenuStrip contextMenuStrip3;
        private Core.Base.Helpers.MyNumericUpDown _keepAlive;
        private ComboBox Port;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private Button btnLicensePath;
        private TextBox txtLicensePath;
        private Label lbllicense;
        private Label label19;
        private Label label21;
        private TextBox textClientId;
        private Label lblClientId;
        List<int> items;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textClientId = new System.Windows.Forms.TextBox();
            this.lblClientId = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnLicensePath = new System.Windows.Forms.Button();
            this.txtLicensePath = new System.Windows.Forms.TextBox();
            this.lbllicense = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.ComboBox();
            this._keepAlive = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.BtnPrivateKey = new System.Windows.Forms.Button();
            this.btnClientCertificate = new System.Windows.Forms.Button();
            this.btnCA3Certificate = new System.Windows.Forms.Button();
            this.PrivateKey = new System.Windows.Forms.TextBox();
            this.ClientCertificate = new System.Windows.Forms.TextBox();
            this.CA3Certificate = new System.Windows.Forms.TextBox();
            this.txtLicensePath.TextChanged += txtLicensePath_TextChanged;
            this.CA3Certificate.TextChanged += txtLicensePath_TextChanged; // or create separate handler
            this.ClientCertificate.TextChanged += txtLicensePath_TextChanged;
            this.PrivateKey.TextChanged += txtLicensePath_TextChanged;
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this._cleanSession = new System.Windows.Forms.TextBox();
            this._passWord = new System.Windows.Forms.TextBox();
            this._userName = new System.Windows.Forms.TextBox();
            this.hostname = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DataType = new System.Windows.Forms.Label();
            this.Value = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._keepAlive)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.textClientId);
            this.groupBox1.Controls.Add(this.lblClientId);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.btnLicensePath);
            this.groupBox1.Controls.Add(this.txtLicensePath);
            this.groupBox1.Controls.Add(this.lbllicense);
            this.groupBox1.Controls.Add(this.Port);
            this.groupBox1.Controls.Add(this._keepAlive);
            this.groupBox1.Controls.Add(this.BtnPrivateKey);
            this.groupBox1.Controls.Add(this.btnClientCertificate);
            this.groupBox1.Controls.Add(this.btnCA3Certificate);
            this.groupBox1.Controls.Add(this.PrivateKey);
            this.groupBox1.Controls.Add(this.ClientCertificate);
            this.groupBox1.Controls.Add(this.CA3Certificate);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this._cleanSession);
            this.groupBox1.Controls.Add(this._passWord);
            this.groupBox1.Controls.Add(this._userName);
            this.groupBox1.Controls.Add(this.hostname);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DataType);
            this.groupBox1.Controls.Add(this.Value);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 425);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MQTT Configuration";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(365, 95);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 45;
            this.label21.Text = "String (100)";
            // 
            // textClientId
            // 
            this.textClientId.Location = new System.Drawing.Point(176, 92);
            this.textClientId.Name = "textClientId";
            this.textClientId.Size = new System.Drawing.Size(145, 20);
            this.textClientId.TabIndex = 44;
            this.textClientId.Validating += new System.ComponentModel.CancelEventHandler(this.textClientId_Validating);
            // 
            // lblClientId
            // 
            this.lblClientId.AutoSize = true;
            this.lblClientId.Location = new System.Drawing.Point(44, 95);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(47, 13);
            this.lblClientId.TabIndex = 43;
            this.lblClientId.Text = "Client ID";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(365, 340);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 13);
            this.label19.TabIndex = 42;
            this.label19.Text = "(Select Path)";
            // 
            // btnLicensePath
            // 
            this.btnLicensePath.Location = new System.Drawing.Point(297, 334);
            this.btnLicensePath.Name = "btnLicensePath";
            this.btnLicensePath.Size = new System.Drawing.Size(25, 21);
            this.btnLicensePath.TabIndex = 41;
            this.btnLicensePath.Text = "...";
            this.btnLicensePath.UseVisualStyleBackColor = true;
            this.btnLicensePath.Click += new System.EventHandler(this.btnLicensePath_Click);
            errorProvider.SetIconPadding(txtLicensePath, 22);
            // 
            // txtLicensePath
            // 
            this.txtLicensePath.Location = new System.Drawing.Point(175, 334);
            this.txtLicensePath.Name = "txtLicensePath";
            this.txtLicensePath.ReadOnly = true;
            this.txtLicensePath.Size = new System.Drawing.Size(125, 20);
            this.txtLicensePath.TabIndex = 40;
            this.txtLicensePath.Validating += new System.ComponentModel.CancelEventHandler(this.txtLicensePath_Validating);
            // 
            // lbllicense
            // 
            this.lbllicense.AutoSize = true;
            this.lbllicense.Location = new System.Drawing.Point(44, 334);
            this.lbllicense.Name = "lbllicense";
            this.lbllicense.Size = new System.Drawing.Size(44, 13);
            this.lbllicense.TabIndex = 39;
            this.lbllicense.Text = "License";
            this.btnLicensePath.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnLicensePath_MouseMOve);
            // 
            // Port
            // 
            this.Port.DisplayMember = "1883,8883";
            this.Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Port.FormattingEnabled = true;
            this.Port.Location = new System.Drawing.Point(176, 119);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(146, 21);
            this.Port.TabIndex = 38;
            this.Port.ValueMember = "1883,8883";
            this.Port.SelectedIndexChanged += new System.EventHandler(this.Port_SelectedIndexChanged);
            this.Port.Validating += new System.ComponentModel.CancelEventHandler(this.Port_Validating);
            // 
            // _keepAlive
            // 
            this._keepAlive.Location = new System.Drawing.Point(175, 300);
            this._keepAlive.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this._keepAlive.Name = "_keepAlive";
            this._keepAlive.Size = new System.Drawing.Size(146, 20);
            this._keepAlive.TabIndex = 37;
            this._keepAlive.Validating += new System.ComponentModel.CancelEventHandler(this._keepAlive_Validating);
            // 
            // BtnPrivateKey
            // 
            this.BtnPrivateKey.Location = new System.Drawing.Point(296, 249);
            this.BtnPrivateKey.Name = "BtnPrivateKey";
            this.BtnPrivateKey.Size = new System.Drawing.Size(25, 20);
            this.BtnPrivateKey.TabIndex = 33;
            this.BtnPrivateKey.Text = "...";
            this.BtnPrivateKey.UseVisualStyleBackColor = true;
            this.BtnPrivateKey.Click += new System.EventHandler(this.BtnPrivateKey_Click);
            this.BtnPrivateKey.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BtnPrivateKey_MouseMove);
            errorProvider.SetIconPadding(PrivateKey, 22);
            // 
            // btnClientCertificate
            // 
            this.btnClientCertificate.Location = new System.Drawing.Point(296, 223);
            this.btnClientCertificate.Name = "btnClientCertificate";
            this.btnClientCertificate.Size = new System.Drawing.Size(25, 20);
            this.btnClientCertificate.TabIndex = 32;
            this.btnClientCertificate.Text = "...";
            this.btnClientCertificate.UseVisualStyleBackColor = true;
            this.btnClientCertificate.Click += new System.EventHandler(this.btnClientCertificate_Click);
            this.btnClientCertificate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnClientCertificate_MouseMove);
            errorProvider.SetIconPadding(ClientCertificate, 22);
            // 
            // btnCA3Certificate
            // 
            this.btnCA3Certificate.Location = new System.Drawing.Point(297, 200);
            this.btnCA3Certificate.Name = "btnCA3Certificate";
            this.btnCA3Certificate.Size = new System.Drawing.Size(25, 21);
            this.btnCA3Certificate.TabIndex = 31;
            this.btnCA3Certificate.Text = "...";
            this.btnCA3Certificate.UseVisualStyleBackColor = true;
            this.btnCA3Certificate.Click += new System.EventHandler(this.CA3Certificate_Click);
            this.btnCA3Certificate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCA3Certificate_MouseMove);
            errorProvider.SetIconPadding(CA3Certificate, 22);
            // 
            // PrivateKey
            // 
            this.PrivateKey.Location = new System.Drawing.Point(175, 249);
            this.PrivateKey.Name = "PrivateKey";
            this.PrivateKey.ReadOnly = true;
            this.PrivateKey.Size = new System.Drawing.Size(125, 20);
            this.PrivateKey.TabIndex = 30;
            this.PrivateKey.Validating += new System.ComponentModel.CancelEventHandler(this.PrivateKey_Validating);
            // 
            // ClientCertificate
            // 
            this.ClientCertificate.Location = new System.Drawing.Point(175, 223);
            this.ClientCertificate.Name = "ClientCertificate";
            this.ClientCertificate.ReadOnly = true;
            this.ClientCertificate.Size = new System.Drawing.Size(125, 20);
            this.ClientCertificate.TabIndex = 29;
            this.ClientCertificate.Validating += new System.ComponentModel.CancelEventHandler(this.ClientCertificate_Validating);
            // 
            // CA3Certificate
            // 
            this.CA3Certificate.Location = new System.Drawing.Point(175, 200);
            this.CA3Certificate.Name = "CA3Certificate";
            this.CA3Certificate.ReadOnly = true;
            this.CA3Certificate.Size = new System.Drawing.Size(125, 20);
            this.CA3Certificate.TabIndex = 28;
            this.CA3Certificate.Validating += new System.ComponentModel.CancelEventHandler(this.CA3Certificate_Validating);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(365, 302);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(144, 13);
            this.label18.TabIndex = 27;
            this.label18.Text = "(Optional)  Range : 0 to 3600";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(365, 275);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(85, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "(Optional)  (Bool)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(365, 255);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 13);
            this.label16.TabIndex = 25;
            this.label16.Text = "(Select Path)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(365, 226);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "(Select Path)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(365, 200);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "(Select Path)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(365, 177);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "(Optional)  String(50)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(365, 147);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "(Optional) String(50)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(365, 122);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "(1883 or 8883)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(365, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "String (100)";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(332, 380);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // _cleanSession
            // 
            this._cleanSession.Location = new System.Drawing.Point(176, 272);
            this._cleanSession.Name = "_cleanSession";
            this._cleanSession.Size = new System.Drawing.Size(146, 20);
            this._cleanSession.TabIndex = 16;
            this._cleanSession.Validating += new System.ComponentModel.CancelEventHandler(this._cleanSession_Validating);
            // 
            // _passWord
            // 
            this._passWord.Location = new System.Drawing.Point(175, 174);
            this._passWord.Name = "_passWord";
            this._passWord.Size = new System.Drawing.Size(146, 20);
            this._passWord.TabIndex = 15;
            this._passWord.Validating += new System.ComponentModel.CancelEventHandler(this._passWord_Validating);
            // 
            // _userName
            // 
            this._userName.Location = new System.Drawing.Point(175, 147);
            this._userName.Name = "_userName";
            this._userName.Size = new System.Drawing.Size(146, 20);
            this._userName.TabIndex = 14;
            this._userName.Validating += new System.ComponentModel.CancelEventHandler(this._userName_Validating);
            // 
            // hostname
            // 
            this.hostname.Location = new System.Drawing.Point(175, 68);
            this.hostname.Name = "hostname";
            this.hostname.Size = new System.Drawing.Size(146, 20);
            this.hostname.TabIndex = 12;
            this.hostname.Validating += new System.ComponentModel.CancelEventHandler(this.hostname_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 302);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Keep Alive";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 272);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Clean Session";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Private Key";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Client Certificate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "CA3 Certificate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Hostname";
            // 
            // DataType
            // 
            this.DataType.AutoSize = true;
            this.DataType.Location = new System.Drawing.Point(365, 34);
            this.DataType.Name = "DataType";
            this.DataType.Size = new System.Drawing.Size(57, 13);
            this.DataType.TabIndex = 2;
            this.DataType.Text = "Data Type";
            // 
            // Value
            // 
            this.Value.AutoSize = true;
            this.Value.Location = new System.Drawing.Point(172, 34);
            this.Value.Name = "Value";
            this.Value.Size = new System.Drawing.Size(34, 13);
            this.Value.TabIndex = 1;
            this.Value.Text = "Value";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(44, 34);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.ShowReadOnly = true;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            // 
            // MqttSettingsUserControl
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.groupBox1);
            this.Name = "MqttSettingsUserControl";
            this.Size = new System.Drawing.Size(547, 436);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._keepAlive)).EndInit();
            this.ResumeLayout(false);

        }
        public MqttSettingsUserControl()
        {
            InitializeComponent();
            SetCertificateControlsState(false);
            items = new List<int> { 1883, 8883 };
            this.Port.DataSource = items;
            xm = XMPS.Instance;
            dataSource = new MQTTForm();

        }
        public MqttSettingsUserControl(MQTTForm mqtt)
        {
            InitializeComponent();
            dataSource = new MQTTForm();
            dataSource = mqtt;
            this.hostname.Text = mqtt.hostname;
            this.textClientId.Text = mqtt.clientId;
            this._userName.Text = mqtt.username;
            this._passWord.Text = mqtt.password;
            this.CA3Certificate.Text = mqtt.ca3certificate;
            this.ClientCertificate.Text = mqtt.clientCertificate;
            this.PrivateKey.Text = mqtt.clientKey;
            this._cleanSession.Text = mqtt.cleanSession;
            this._keepAlive.Value = mqtt.keepAlive;
            items = new List<int> { 1883, 8883 };
            this.Port.DataSource = items;
            this.Port.Text = mqtt.port.ToString();
            if (mqtt.port.ToString() == "1883")
                SetCertificateControlsState(false);
            this.txtLicensePath.Text = mqtt.license;

        }
        private void CA3Certificate_Click(object sender, EventArgs e)
        {
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                CA3Certificate.Text = selectedFilePath; // Assuming the TextBox is named "textBoxFilePath"
            }

            if (string.IsNullOrWhiteSpace(CA3Certificate.Text))
            {
                errorProvider.SetError(btnCA3Certificate, "CA3 Certificate is required.");
                return;
            }
            else
            {
                errorProvider.SetError(btnCA3Certificate, "");
            }
        }


        private void btnClientCertificate_Click(object sender, EventArgs e)
        {
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                ClientCertificate.Text = selectedFilePath; // Assuming the TextBox is named "textBoxFilePath"
            }
            if (string.IsNullOrWhiteSpace(ClientCertificate.Text))
            {
                errorProvider.SetError(btnClientCertificate, "CA3 Certificate is required.");
                return;
            }
            else
            {
                errorProvider.SetError(btnClientCertificate, "");
            }

        }

        private void BtnPrivateKey_Click(object sender, EventArgs e)
        {
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                PrivateKey.Text = selectedFilePath; // Assuming the TextBox is named "textBoxFilePath"
            }
            if (string.IsNullOrWhiteSpace(PrivateKey.Text))
            {
                errorProvider.SetError(BtnPrivateKey, "CA3 Certificate is required.");
                return;
            }
            else
            {
                errorProvider.SetError(BtnPrivateKey, "");
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                if (Port.SelectedItem.ToString() != "1883")
                {
                    if (string.IsNullOrWhiteSpace(CA3Certificate.Text))
                    {
                        errorProvider.SetError(btnCA3Certificate, "CA3 Certificate is required.");
                        return;
                    }
                    else
                    {
                        errorProvider.SetError(btnCA3Certificate, "");
                    }

                    if (string.IsNullOrWhiteSpace(ClientCertificate.Text))
                    {
                        errorProvider.SetError(btnClientCertificate, "Client Certificate is required.");
                        return;
                    }
                    else
                    {
                        errorProvider.SetError(btnClientCertificate, "");
                    }
                    if (string.IsNullOrWhiteSpace(PrivateKey.Text))
                    {
                        errorProvider.SetError(BtnPrivateKey, "Private Key is required.");
                        return;
                    }
                    else
                    {
                        errorProvider.SetError(BtnPrivateKey, "");
                    }
                }

                if (string.IsNullOrEmpty(hostname.Text))
                {
                    errorProvider.SetError(hostname, "hostname should be not null");
                    return;
                }
                else
                {
                    errorProvider.SetError(hostname, "");
                }
                if(string.IsNullOrEmpty(textClientId.Text))
                {
                    errorProvider.SetError(textClientId, "Client Id should be not null");
                    return;
                }
                else
                {
                    errorProvider.SetError(hostname, "");
                }
                if (string.IsNullOrWhiteSpace(txtLicensePath.Text))
                {
                    errorProvider.SetError(txtLicensePath, "License is required");
                    return;
                }
                else
                {
                    errorProvider.SetError(txtLicensePath, "");
                }
                dataSource.hostname = this.hostname.Text;
                dataSource.clientId = this.textClientId.Text;
                dataSource.port = Convert.ToInt32(this.Port.Text.ToString());
                dataSource.username = this._userName.Text;
                dataSource.password = this._passWord.Text;
                dataSource.ca3certificate = this.CA3Certificate.Text;
                dataSource.clientCertificate = this.ClientCertificate.Text;
                dataSource.license = this.txtLicensePath.Text;
                dataSource.clientKey = this.PrivateKey.Text;
                dataSource.cleanSession = this._cleanSession.Text;
                dataSource.keepAlive = Convert.ToInt32(this._keepAlive.Text.ToString());
                XMPS.Instance.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MQTTForm");
                XMPS.Instance.LoadedProject.Devices.Add(dataSource);
                XMPS.Instance.MarkProjectModified(true);
                this.ParentForm.Close();
                this.ParentForm.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }


        private void _userName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = _userName.Text;
            if (input.Length > 50)
            {
                errorProvider.SetError(_userName, "Username should be either less than 50.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(_userName, "");
            }
        }

        private void _passWord_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = _passWord.Text;
            if (input.Length > 50)
            {
                errorProvider.SetError(_passWord, "Password should be either less than 50.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(_passWord, "");
            }
        }

        private void _cleanSession_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = _cleanSession.Text;
            if (input != "")
            {
                if (input != "1" && input != "0")
                {
                    errorProvider.SetError(_cleanSession, "input should be either 0 or 1");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(_cleanSession, "");
                }
            }
        }

        private void _keepAlive_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int value = (int)_keepAlive.Value;
            if (value < 0 && value > 3600)
            {
                errorProvider.SetError(_keepAlive, "Range should be either 0 or 3600.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(_keepAlive, "");
            }
        }



        private void hostname_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (! string.IsNullOrEmpty(hostname.Text) && hostname.Text.Length > 100)
            {
                errorProvider.SetError(hostname, "hostname should be less than 100 character.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(hostname, "");
            }
        }

        private void PrivateKey_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Port.SelectedItem != null && Port.SelectedItem.ToString() == "8883" && !string.IsNullOrWhiteSpace(PrivateKey.Text))
            {
                FileInfo fileInfo = new FileInfo(PrivateKey.Text);
                if (!fileInfo.Exists)
                {
                    errorProvider.SetError(PrivateKey, "Private Key does not exist.");
                    e.Cancel = true;
                    return;
                }
                if (fileInfo.Extension.ToLower() != ".pem" && fileInfo.Extension.ToLower() != ".crt" && fileInfo.Extension.ToLower() != ".key")
                {
                    errorProvider.SetError(PrivateKey, "Private Key must be a .pem, .crt, or .key file.");
                    e.Cancel = true;
                    return;
                }
                errorProvider.SetError(PrivateKey, "");
            }
        }

        private void CA3Certificate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Port.SelectedItem != null && Port.SelectedItem.ToString() == "8883" && !string.IsNullOrWhiteSpace(CA3Certificate.Text))
            {
                FileInfo fileInfo = new FileInfo(CA3Certificate.Text);
                if (!fileInfo.Exists)
                {
                    errorProvider.SetError(CA3Certificate, "CA3 Certificate does not exist.");
                    e.Cancel = true;
                    return;
                }
                if (fileInfo.Extension.ToLower() != ".pem" && fileInfo.Extension.ToLower() != ".crt" && fileInfo.Extension.ToLower() != ".key")
                {
                    errorProvider.SetError(CA3Certificate, "CA3 Certificate must be a .pem, .crt, or .key file.");
                    e.Cancel = true;
                    return;
                }
                errorProvider.SetError(CA3Certificate, "");
            }
        }

        private void ClientCertificate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Port.SelectedItem != null && Port.SelectedItem.ToString() == "8883" && !string.IsNullOrWhiteSpace(ClientCertificate.Text))
            {
                FileInfo fileInfo = new FileInfo(ClientCertificate.Text);
                if (!fileInfo.Exists)
                {
                    errorProvider.SetError(ClientCertificate, "Client Certificate does not exist.");
                    e.Cancel = true;
                    return;
                }
                if (fileInfo.Extension.ToLower() != ".pem"  && fileInfo.Extension.ToLower() != ".crt" && fileInfo.Extension.ToLower() != ".key")
                {
                    errorProvider.SetError(ClientCertificate, "Client Certificate must be a .pem, .crt, or .key file.");
                    e.Cancel = true;
                    return;
                }
                errorProvider.SetError(ClientCertificate, "");
            }
        }

        private void btnCA3Certificate_MouseMove(object sender, MouseEventArgs e)
        {
            string path = CA3Certificate.Text != null ? CA3Certificate.Text : null;

            tp.SetToolTip(btnCA3Certificate, path);
        }
        private void btnLicensePath_MouseMOve(object sender, EventArgs e)
        {
            string path = txtLicensePath.Text != null ? txtLicensePath.Text : null;
            tp.SetToolTip(btnLicensePath, path);
        }
        private void Port_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var port = Port.Text;
            if (Port.Text == "1883" || Port.Text =="8883")
            {
                errorProvider.SetError(Port, "");
                e.Cancel = false;
            }
            else
            {
                errorProvider.SetError(Port, "Port number is either 1883 or 8883");
                e.Cancel = true;
            }
        }

        private void btnClientCertificate_MouseMove(object sender, MouseEventArgs e)
        {
            string path = ClientCertificate.Text != null ? ClientCertificate.Text : null;

            tp.SetToolTip(btnClientCertificate, path);
        }

        private void BtnPrivateKey_MouseMove(object sender, MouseEventArgs e)
        {
            string path = PrivateKey.Text != null ? PrivateKey.Text : null;

            tp.SetToolTip(BtnPrivateKey, path);
        }

        private void btnLicensePath_Click(object sender, EventArgs e)
        {
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                txtLicensePath.Text = selectedFilePath;
            }
        }

        private void textClientId_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textClientId.Text) && textClientId.Text.Length > 100)
            {
                errorProvider.SetError(textClientId, "Client Id should be less than 100 character.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(textClientId, "");
            }
        }

        private void Port_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Port.SelectedItem.ToString() == "1883")
            {
                SetCertificateControlsState(false);
            }
            else
            {
                SetCertificateControlsState(true);
            }
        }
        private void SetCertificateControlsState(bool isEnabled)
        {
            // Enable or disable controls
            CA3Certificate.Enabled = isEnabled;
            btnCA3Certificate.Enabled = isEnabled;
            ClientCertificate.Enabled = isEnabled;
            btnClientCertificate.Enabled = isEnabled;
            PrivateKey.Enabled = isEnabled;
            BtnPrivateKey.Enabled = isEnabled;

            if (!isEnabled)
            {
                // Clear error messages for these fields
                errorProvider.SetError(CA3Certificate, "");
                errorProvider.SetError(ClientCertificate, "");
                errorProvider.SetError(PrivateKey, "");

                // Optionally clear values if necessary
                CA3Certificate.Text = "";
                ClientCertificate.Text = "";
                PrivateKey.Text = "";
            }
        }

        private void txtLicensePath_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string licensePath = txtLicensePath.Text.Trim();

            if (string.IsNullOrEmpty(licensePath))
            {
                errorProvider.SetError(txtLicensePath, "License is required.");
                return;
            }

            FileInfo fileInfo = new FileInfo(licensePath);
            if (!fileInfo.Exists)
            {
                errorProvider.SetError(txtLicensePath, "License file does not exist.");
                e.Cancel = true;
                return;
            }
            if (fileInfo.Extension.ToLower() != ".txt")
            {
                errorProvider.SetError(txtLicensePath, "License must be a .txt file");
                e.Cancel = true;
                return;
            }
            errorProvider.SetError(txtLicensePath, "");
        }

        private void txtLicensePath_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren(ValidationConstraints.Enabled);
        }
    }
}
