using System;
using System.ComponentModel;
using System.Net;

namespace XMPS2000.Bacnet
{
    partial class FormNetworkPort
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelNetworkNumberRange = new System.Windows.Forms.Label();
            this.labelBacNetUDPPortRange = new System.Windows.Forms.Label();
            this.labelNetworkType = new System.Windows.Forms.Label();
            this.labelObjType = new System.Windows.Forms.Label();
            this.labelObjIdentifier = new System.Windows.Forms.Label();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.textObjectName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBoxDHCP = new System.Windows.Forms.CheckBox();
            this.textDNSServer = new System.Windows.Forms.TextBox();
            this.textDefaultGateway = new System.Windows.Forms.TextBox();
            this.textSubnetMask = new System.Windows.Forms.TextBox();
            this.textIPAddress = new System.Windows.Forms.TextBox();
            this.comboBacNetIPMode = new System.Windows.Forms.ComboBox();
            this.numericBacNetUDPPort = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.numericNetworkNumber = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();

            this.groupBox2 = new System.Windows.Forms.GroupBox();
            //this.btnSaveFD = new System.Windows.Forms.Button();
            this.labelFDSubscriptionLifetimeRange = new System.Windows.Forms.Label();
            this.numericFDSubscriptionLifetime = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.numericFDBBMDPort = new XMPS2000.Core.Base.Helpers.MyNumericUpDown();
            this.textFDBBMDIP = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBacNetUDPPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNetworkNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFDSubscriptionLifetime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFDBBMDPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();

            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelNetworkNumberRange);
            this.groupBox1.Controls.Add(this.labelBacNetUDPPortRange);
            this.groupBox1.Controls.Add(this.labelNetworkType);
            this.groupBox1.Controls.Add(this.labelObjType);
            this.groupBox1.Controls.Add(this.labelObjIdentifier);
            this.groupBox1.Controls.Add(this.textDescription);
            this.groupBox1.Controls.Add(this.textObjectName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.checkBoxDHCP);
            this.groupBox1.Controls.Add(this.textDNSServer);
            this.groupBox1.Controls.Add(this.textDefaultGateway);
            this.groupBox1.Controls.Add(this.textSubnetMask);
            this.groupBox1.Controls.Add(this.textIPAddress);
            this.groupBox1.Controls.Add(this.comboBacNetIPMode);
            this.groupBox1.Controls.Add(this.numericBacNetUDPPort);
            this.groupBox1.Controls.Add(this.numericNetworkNumber);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(550, 480);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "";

            // 
            // labelNetworkNumberRange
            // 
            this.labelNetworkNumberRange.AutoSize = true;
            this.labelNetworkNumberRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelNetworkNumberRange.Location = new System.Drawing.Point(470, 237);
            this.labelNetworkNumberRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNetworkNumberRange.Name = "labelNetworkNumberRange";
            this.labelNetworkNumberRange.Size = new System.Drawing.Size(77, 16);
            this.labelNetworkNumberRange.TabIndex = 52;
            this.labelNetworkNumberRange.Text = "(1-65535)";

            // 
            // labelBacNetUDPPortRange
            // 
            this.labelBacNetUDPPortRange.AutoSize = true;
            this.labelBacNetUDPPortRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelBacNetUDPPortRange.Location = new System.Drawing.Point(470, 205);
            this.labelBacNetUDPPortRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBacNetUDPPortRange.Name = "labelBacNetUDPPortRange";
            this.labelBacNetUDPPortRange.Size = new System.Drawing.Size(77, 16);
            this.labelBacNetUDPPortRange.TabIndex = 51;
            this.labelBacNetUDPPortRange.Text = "(0-65535)";

            // 
            // labelNetworkType
            // 
            this.labelNetworkType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNetworkType.Location = new System.Drawing.Point(199, 165);
            this.labelNetworkType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNetworkType.Name = "labelNetworkType";
            this.labelNetworkType.Size = new System.Drawing.Size(265, 24);
            this.labelNetworkType.TabIndex = 49;
            this.labelNetworkType.Text = "IPv4";

            // 
            // labelObjType
            // 
            this.labelObjType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjType.Location = new System.Drawing.Point(199, 60);
            this.labelObjType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjType.Name = "labelObjType";
            this.labelObjType.Size = new System.Drawing.Size(265, 24);
            this.labelObjType.TabIndex = 45;
            this.labelObjType.Text = "56";

            // 
            // labelObjIdentifier
            // 
            this.labelObjIdentifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelObjIdentifier.Location = new System.Drawing.Point(199, 23);
            this.labelObjIdentifier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelObjIdentifier.Name = "labelObjIdentifier";
            this.labelObjIdentifier.Size = new System.Drawing.Size(265, 24);
            this.labelObjIdentifier.TabIndex = 43;
            this.labelObjIdentifier.Text = "56 : 0";

            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(199, 130);
            this.textDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textDescription.MaxLength = 25;
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(264, 22);
            this.textDescription.TabIndex = 4;
            this.textDescription.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textDescription.TextChanged += new System.EventHandler(this.textDescription_TextChanged);
            this.textDescription.Validating += new System.ComponentModel.CancelEventHandler(this.textDescription_Validating);

            // 
            // textObjectName
            // 
            this.textObjectName.Location = new System.Drawing.Point(199, 95);
            this.textObjectName.Margin = new System.Windows.Forms.Padding(4);
            this.textObjectName.MaxLength = 25;
            this.textObjectName.Name = "textObjectName";
            this.textObjectName.Size = new System.Drawing.Size(264, 22);
            this.textObjectName.TabIndex = 3;
            this.textObjectName.Text = "Network_port";
            this.textObjectName.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textObjectName.TextChanged += new System.EventHandler(this.textObjectName_TextChanged);
            this.textObjectName.Validating += new System.ComponentModel.CancelEventHandler(this.textObjectName_Validating);

            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Object Identifier";

            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 100);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 16);
            this.label7.TabIndex = 37;
            this.label7.Text = "Object Name";

            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 65);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 16);
            this.label8.TabIndex = 38;
            this.label8.Text = "Object Type";

            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 135);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 16);
            this.label9.TabIndex = 39;
            this.label9.Text = "Description";

            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 170);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 16);
            this.label10.TabIndex = 49;
            this.label10.Text = "Network Type";

            // 
            // textIPAddress
            // 
            this.textIPAddress.Location = new System.Drawing.Point(199, 304);
            this.textIPAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textIPAddress.Name = "textIPAddress";
            this.textIPAddress.Size = new System.Drawing.Size(265, 22);
            this.textIPAddress.TabIndex = 14;
            this.textIPAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPTextBox_KeyPress);
            this.textIPAddress.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textIPAddress.TextChanged += new System.EventHandler(this.IPTextBox_TextChanged);
            this.textIPAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textIPAddress_Validating);

            // 
            // textSubnetMask
            // 
            this.textSubnetMask.Location = new System.Drawing.Point(199, 336);
            this.textSubnetMask.Margin = new System.Windows.Forms.Padding(4);
            this.textSubnetMask.Name = "textSubnetMask";
            this.textSubnetMask.Size = new System.Drawing.Size(265, 22);
            this.textSubnetMask.TabIndex = 15;
            this.textSubnetMask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPTextBox_KeyPress);
            this.textSubnetMask.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textSubnetMask.TextChanged += new System.EventHandler(this.IPTextBox_TextChanged);
            this.textSubnetMask.Validating += new System.ComponentModel.CancelEventHandler(this.textSubnetMask_Validating);
            // 
            // textDefaultGateway
            // 
            this.textDefaultGateway.Location = new System.Drawing.Point(199, 368);
            this.textDefaultGateway.Margin = new System.Windows.Forms.Padding(4);
            this.textDefaultGateway.Name = "textDefaultGateway";
            this.textDefaultGateway.Size = new System.Drawing.Size(265, 22);
            this.textDefaultGateway.TabIndex = 16;
            this.textDefaultGateway.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPTextBox_KeyPress);
            this.textDefaultGateway.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textDefaultGateway.TextChanged += new System.EventHandler(this.IPTextBox_TextChanged);
            this.textDefaultGateway.Validating += new System.ComponentModel.CancelEventHandler(this.textDefaultGateway_Validating);
            // 
            // textDNSServer
            // 
            this.textDNSServer.Location = new System.Drawing.Point(199, 400);
            this.textDNSServer.Margin = new System.Windows.Forms.Padding(4);
            this.textDNSServer.Name = "textDNSServer";
            this.textDNSServer.Size = new System.Drawing.Size(265, 22);
            this.textDNSServer.TabIndex = 17;
            this.textDNSServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPTextBox_KeyPress);
            this.textDNSServer.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textDNSServer.TextChanged += new System.EventHandler(this.IPTextBox_TextChanged);
            this.textDNSServer.Validating += new System.ComponentModel.CancelEventHandler(this.textDNSServer_Validating);

            // 
            // comboBacNetIPMode
            // 
            this.comboBacNetIPMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBacNetIPMode.FormattingEnabled = true;
            this.comboBacNetIPMode.Items.AddRange(new object[] {
    "Select Mode",
    "FOREIGN",
    "BBMD"});
            this.comboBacNetIPMode.Location = new System.Drawing.Point(199, 266);
            this.comboBacNetIPMode.Name = "comboBacNetIPMode";
            this.comboBacNetIPMode.Size = new System.Drawing.Size(265, 24);
            this.comboBacNetIPMode.TabIndex = 13;
            this.comboBacNetIPMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBacNetIPMode.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBacNetIPMode_DrawItem);
            this.comboBacNetIPMode.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.comboBacNetIPMode.SelectedIndexChanged += new System.EventHandler(this.comboBacNetIPMode_SelectedIndexChanged);

            // 
            // numericBacNetUDPPort
            // 
            this.numericBacNetUDPPort.Location = new System.Drawing.Point(199, 202);
            this.numericBacNetUDPPort.Margin = new System.Windows.Forms.Padding(4);
            this.numericBacNetUDPPort.Maximum = new decimal(new int[] {
    65535,
    0,
    0,
    0});
            this.numericBacNetUDPPort.Name = "numericBacNetUDPPort";
            this.numericBacNetUDPPort.Size = new System.Drawing.Size(265, 22);
            this.numericBacNetUDPPort.TabIndex = 11;
            this.numericBacNetUDPPort.Value = new decimal(new int[] {
    47808,
    0,
    0,
    0});
            this.numericBacNetUDPPort.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.numericBacNetUDPPort.Validating += new System.ComponentModel.CancelEventHandler(this.numericBacNetUDPPort_Validating);

            // 
            // numericNetworkNumber
            // 
            this.numericNetworkNumber.Location = new System.Drawing.Point(199, 234);
            this.numericNetworkNumber.Margin = new System.Windows.Forms.Padding(4);
            this.numericNetworkNumber.Maximum = new decimal(new int[] {
    65535,
    0,
    0,
    0});
            this.numericNetworkNumber.Minimum = new decimal(new int[] {
    1,
    0,
    0,
    0});
            this.numericNetworkNumber.Name = "numericNetworkNumber";
            this.numericNetworkNumber.Size = new System.Drawing.Size(265, 22);
            this.numericNetworkNumber.TabIndex = 10;
            this.numericNetworkNumber.Value = new decimal(new int[] {
    1,
    0,
    0,
    0});
            this.numericNetworkNumber.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.numericNetworkNumber.Validating += new System.ComponentModel.CancelEventHandler(this.numericNetworkNumber_Validating);

            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 437);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 16);
            this.label13.TabIndex = 8;
            this.label13.Text = "IP DHCP Enable :";

            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 403);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 16);
            this.label12.TabIndex = 7;
            this.label12.Text = "IP DNS Server :";

            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 371);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(132, 16);
            this.label11.TabIndex = 6;
            this.label11.Text = "IP Default Gateway :";

            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 339);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "IP Subnet Mask :";

            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 307);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "IP Address :";

            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 269);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "BACnet IP Mode :";

            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 205);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "BACnet IP UDP Port :";

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 237);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Network Number :";

            // 
            // checkBoxDHCP
            // 
            this.checkBoxDHCP.AutoSize = true;
            this.checkBoxDHCP.Checked = true;
            this.checkBoxDHCP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDHCP.Location = new System.Drawing.Point(199, 434);
            this.checkBoxDHCP.Name = "checkBoxDHCP";
            this.checkBoxDHCP.Size = new System.Drawing.Size(97, 20);
            this.checkBoxDHCP.TabIndex = 18;
            this.checkBoxDHCP.Text = "";
            this.checkBoxDHCP.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.checkBoxDHCP.UseVisualStyleBackColor = true;

            // 
            // groupBox2
            // 
            //this.groupBox2.Controls.Add(this.btnSaveFD);
            this.groupBox2.Controls.Add(this.numericFDSubscriptionLifetime);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.numericFDBBMDPort);
            this.groupBox2.Controls.Add(this.textFDBBMDIP);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.labelFDSubscriptionLifetimeRange);
            this.groupBox2.Location = new System.Drawing.Point(570, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 280);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FD BBMD Address";
            //// 
            //// btnSaveFD
            //// 
            //this.btnSaveFD.Location = new System.Drawing.Point(110, 180);
            //this.btnSaveFD.Margin = new System.Windows.Forms.Padding(4);
            //this.btnSaveFD.Name = "btnSaveFD";
            //this.btnSaveFD.Size = new System.Drawing.Size(150, 31);
            //this.btnSaveFD.TabIndex = 19;
            //this.btnSaveFD.Text = "Save";
            //this.btnSaveFD.UseVisualStyleBackColor = true;            
            //this.btnSaveFD.Click += new System.EventHandler(this.btnSaveFD_Click);
            // 
            // textFDBBMDIP
            // 
            this.textFDBBMDIP.Location = new System.Drawing.Point(23, 45);
            this.textFDBBMDIP.Margin = new System.Windows.Forms.Padding(4);
            this.textFDBBMDIP.Name = "textFDBBMDIP";
            this.textFDBBMDIP.Size = new System.Drawing.Size(330, 22);
            this.textFDBBMDIP.TabIndex = 17;
            this.textFDBBMDIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPTextBox_KeyPress);
            this.textFDBBMDIP.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.textFDBBMDIP.TextChanged += new System.EventHandler(this.IPTextBox_TextChanged);
            this.textFDBBMDIP.Validating += new System.ComponentModel.CancelEventHandler(this.textFDBBMDIP_Validating);

            // 
            // numericFDBBMDPort
            // 
            this.numericFDBBMDPort.Location = new System.Drawing.Point(23, 95);
            this.numericFDBBMDPort.Margin = new System.Windows.Forms.Padding(4);
            this.numericFDBBMDPort.Maximum = new decimal(new int[] {
    65535,
    0,
    0,
    0});
            this.numericFDBBMDPort.Name = "numericFDBBMDPort";
            this.numericFDBBMDPort.Size = new System.Drawing.Size(330, 22);
            this.numericFDBBMDPort.TabIndex = 18;
            this.numericFDBBMDPort.Value = new decimal(new int[] {
    47808,
    0,
    0,
    0});
            this.numericFDBBMDPort.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.numericFDBBMDPort.Validating += new System.ComponentModel.CancelEventHandler(this.numericFDBBMDPort_Validating);
            // 
            // labelFDSubscriptionLifetimeRange
            // 
            this.labelFDSubscriptionLifetimeRange.AutoSize = true;
            this.labelFDSubscriptionLifetimeRange.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelFDSubscriptionLifetimeRange.Location = new System.Drawing.Point(360, 145);
            this.labelFDSubscriptionLifetimeRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFDSubscriptionLifetimeRange.Name = "labelFDSubscriptionLifetimeRange";
            this.labelFDSubscriptionLifetimeRange.Size = new System.Drawing.Size(77, 16);
            this.labelFDSubscriptionLifetimeRange.TabIndex = 52;
            this.labelFDSubscriptionLifetimeRange.Text = "(0-65535)";

            // 
            // numericFDSubscriptionLifetime
            // 
            this.numericFDSubscriptionLifetime.Location = new System.Drawing.Point(23, 145);
            this.numericFDSubscriptionLifetime.Margin = new System.Windows.Forms.Padding(4);
            this.numericFDSubscriptionLifetime.Maximum = new decimal(new int[] {
    65535,
    0,
    0,
    0});
            this.numericFDSubscriptionLifetime.Name = "numericFDSubscriptionLifetime";
            this.numericFDSubscriptionLifetime.Size = new System.Drawing.Size(330, 22);
            this.numericFDSubscriptionLifetime.TabIndex = 12;
            this.numericFDSubscriptionLifetime.Value = new decimal(new int[] {
    5000,
    0,
    0,
    0});
            this.numericFDSubscriptionLifetime.TextChanged += new System.EventHandler(BacNetValidator.ControlChanged);
            this.numericFDSubscriptionLifetime.Validating += new System.ComponentModel.CancelEventHandler(this.numericFDSubscriptionLifetime_Validating);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 125);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(157, 16);
            this.label14.TabIndex = 9;
            this.label14.Text = "FD Subscription Lifetime :";

            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label15.Location = new System.Drawing.Point(23, 25);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 16);
            this.label15.TabIndex = 15;
            this.label15.Text = "IP Address";

            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label16.Location = new System.Drawing.Point(23, 75);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 16);
            this.label16.TabIndex = 16;
            this.label16.Text = "Port Number";

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(367, 520);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 31);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.Location = new System.Drawing.Point(497, 520);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 31);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;

            // 
            // FormNetworkPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(1056, 570);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNetworkPort";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBacNetUDPPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNetworkNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFDSubscriptionLifetime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFDBBMDPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelNetworkType;
        private System.Windows.Forms.Label labelObjType;
        private System.Windows.Forms.Label labelObjIdentifier;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.TextBox textObjectName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox2;
        private Core.Base.Helpers.MyNumericUpDown numericFDSubscriptionLifetime;
        private System.Windows.Forms.Label label14;
        private Core.Base.Helpers.MyNumericUpDown numericFDBBMDPort;
        private System.Windows.Forms.TextBox textFDBBMDIP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkBoxDHCP;
        private System.Windows.Forms.TextBox textDNSServer;
        private System.Windows.Forms.TextBox textDefaultGateway;
        private System.Windows.Forms.TextBox textSubnetMask;
        private System.Windows.Forms.TextBox textIPAddress;
        private System.Windows.Forms.ComboBox comboBacNetIPMode;
        //private System.Windows.Forms.Button btnSaveFD;
        private Core.Base.Helpers.MyNumericUpDown numericBacNetUDPPort;
        private Core.Base.Helpers.MyNumericUpDown numericNetworkNumber;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelNetworkNumberRange;
        private System.Windows.Forms.Label labelBacNetUDPPortRange;
        private System.Windows.Forms.Label labelFDSubscriptionLifetimeRange;
    }
}