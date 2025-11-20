using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;

namespace XMPS2000.Bacnet
{
    public partial class FormNetworkPort : Form
    {
        private BacNetIP bacNetIP;
        private readonly bool _isReadOnly;
        private int _lastValidBacNetIPModeIndex = 0;
        private int _previousModeIndex = 0;

        public FormNetworkPort(bool isReadOnly = false)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            XMPS.Instance.BacNetCurrentScreen = "FormNetworkPort";
            _isReadOnly = isReadOnly;
            _previousModeIndex = 0;
            ApplyReadOnly();
        }

        private void ApplyReadOnly()
        {
            if (_isReadOnly)
            {
                textObjectName.Enabled = false;
                textDescription.Enabled = false;
                numericNetworkNumber.Enabled = false;
                comboBacNetIPMode.Enabled = false;
                textIPAddress.Enabled = false;
                numericBacNetUDPPort.Enabled = false;
                textSubnetMask.Enabled = false;
                textDefaultGateway.Enabled = false;
                textDNSServer.Enabled = false;
                checkBoxDHCP.Enabled = false;
                textFDBBMDIP.Enabled = false;
                numericFDBBMDPort.Enabled = false;
                numericFDSubscriptionLifetime.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                //btnSaveFD.Enabled = false;
            }
        }

        private void AssignValues()
        {
            bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            NetworkPort networkPort = bacNetIP.NetworkPort ?? new NetworkPort();

            if (networkPort != null)
            {
                labelObjIdentifier.Text = "NetworkPort : 0";
                labelObjType.Text = networkPort.ObjectType ?? "56";
                textObjectName.Text = string.IsNullOrEmpty(networkPort.ObjectName)? "Network_port": (networkPort.ObjectName.Equals("Network Port", StringComparison.OrdinalIgnoreCase)
                               ? "Network_Port": networkPort.ObjectName); textDescription.Text = networkPort.Description ?? "";
                labelNetworkType.Text = "IPv4";

                numericNetworkNumber.Value = !string.IsNullOrEmpty(networkPort.NetworkNumber)? (decimal)int.Parse(networkPort.NetworkNumber): 1;

                int modeIndex = 0; 
                if (!string.IsNullOrEmpty(networkPort.BacnetIPMode))
                {
                    if (int.TryParse(networkPort.BacnetIPMode, out int parsedMode))
                    {
                        modeIndex = parsedMode + 1;

                        if (modeIndex == 2)
                        {
                            modeIndex = _lastValidBacNetIPModeIndex > 0? _lastValidBacNetIPModeIndex: 1;
                        }
                    }
                }
                comboBacNetIPMode.SelectedIndex = modeIndex;
                if (modeIndex != 0)_lastValidBacNetIPModeIndex = modeIndex;
                textIPAddress.Text = networkPort.IPAddress ?? "192.168.15.60";
                textSubnetMask.Text = networkPort.IPSubnetMask ?? "0.0.0.0";
                textDefaultGateway.Text = networkPort.IPDefaultGateway ?? "0.0.0.0";
                textDNSServer.Text = networkPort.IPDNSServer ?? "8.8.8.8";

                numericBacNetUDPPort.Value = !string.IsNullOrEmpty(networkPort.BacnetIPUDPPort)? (decimal)int.Parse(networkPort.BacnetIPUDPPort): 47808;

                checkBoxDHCP.Checked = !string.IsNullOrEmpty(networkPort.IPDHCPEnable) && networkPort.IPDHCPEnable == "1";

                textFDBBMDIP.Text = networkPort.FDBBMDIP ?? "";
                if (!string.IsNullOrEmpty(networkPort.FDBBMDPort) && int.TryParse(networkPort.FDBBMDPort, out int port))
                    numericFDBBMDPort.Value = port;
                else
                    numericFDBBMDPort.Value = 47808;

                numericFDSubscriptionLifetime.Value = !string.IsNullOrEmpty(networkPort.FDSubscriptionLifetime)? (decimal)int.Parse(networkPort.FDSubscriptionLifetime): 5000;
            }
        }

        private string GetIPAddressFromTextBox(TextBox textBox)
        {
            string ipText = textBox.Text?.Trim();
            if (string.IsNullOrEmpty(ipText))return "";
            return ipText;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            ((FormBacNet)this.ParentForm).RefreshGridView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges(sender, e);
        }

        private bool SaveChanges(object sender, EventArgs e)
        {
            string oldName = XMPS.Instance.LoadedProject.BacNetIP.NetworkPort?.ObjectName ?? "";
            if (BacNetFormFactory.ValidateObjectName(textObjectName.Text.Trim(), "NetworkPort") && oldName != textObjectName.Text.Trim())
            {
                MessageBox.Show("Object name is already used, change the name and try again ...","XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateAllIPAddresses())
            {
                MessageBox.Show("Please fix all IP address errors before saving","XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first","XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            NetworkPort networkPort = new NetworkPort();
            bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;

            labelObjIdentifier.Text = "NetworkPort : 0";
            networkPort.ObjectIdentifier = labelObjIdentifier.Text;
            networkPort.ObjectType = labelObjType.Text;
            networkPort.ObjectName = textObjectName.Text;
            networkPort.InstanceNumber = "0";
            networkPort.Description = textDescription.Text;
            networkPort.NetworkType = "IPv4";
            networkPort.NetworkNumber = numericNetworkNumber.Value.ToString();

            int saveMode = comboBacNetIPMode.SelectedIndex - 1;
            if (saveMode == 1)
            {
                saveMode = _lastValidBacNetIPModeIndex - 1;
                if (saveMode < -1) saveMode = -1;
            }
            networkPort.BacnetIPMode = saveMode.ToString();

            networkPort.IPAddress = GetIPAddressFromTextBox(textIPAddress);
            networkPort.IPSubnetMask = GetIPAddressFromTextBox(textSubnetMask);
            networkPort.IPDefaultGateway = GetIPAddressFromTextBox(textDefaultGateway);
            networkPort.IPDNSServer = GetIPAddressFromTextBox(textDNSServer);
            networkPort.BacnetIPUDPPort = numericBacNetUDPPort.Value.ToString();
            networkPort.IPDHCPEnable = checkBoxDHCP.Checked ? "1" : "0";
            networkPort.FDBBMDIP = GetIPAddressFromTextBox(textFDBBMDIP);
            networkPort.FDBBMDPort = numericFDBBMDPort.Value.ToString();
            networkPort.FDSubscriptionLifetime = numericFDSubscriptionLifetime.Value.ToString();

            networkPort.IsEnable = true;

            bacNetIP.NetworkPort = networkPort;

            MessageBox.Show("Network Port information updated","XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);

            XMPS.Instance.LoadedProject.isChanged = false;

            //var formBacNetInstance = Application.OpenForms.OfType<FormBacNet>().FirstOrDefault(f => f.Name == "FormBacNet");
            ((FormBacNet)this.ParentForm).RefreshGridView();

            return true;
        }
        private bool ValidateAllIPAddresses()
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(textIPAddress.Text) && !IsValidIPAddress(textIPAddress.Text))
            {
                errorProvider.SetError(textIPAddress, "Please enter a valid IP address");
                isValid = false;
            }

            if (!string.IsNullOrEmpty(textSubnetMask.Text) && !IsValidIPAddress(textSubnetMask.Text))
            {
                errorProvider.SetError(textSubnetMask, "Please enter a valid subnet mask");
                isValid = false;
            }

            if (!string.IsNullOrEmpty(textDefaultGateway.Text) && !IsValidIPAddress(textDefaultGateway.Text))
            {
                errorProvider.SetError(textDefaultGateway, "Please enter a valid gateway address");
                isValid = false;
            }

            if (!string.IsNullOrEmpty(textDNSServer.Text) && !IsValidIPAddress(textDNSServer.Text))
            {
                errorProvider.SetError(textDNSServer, "Please enter a valid DNS server address");
                isValid = false;
            }

            if (!string.IsNullOrEmpty(textFDBBMDIP.Text) && !IsValidIPAddress(textFDBBMDIP.Text))
            {
                errorProvider.SetError(textFDBBMDIP, "Please enter a valid FD BBMD IP address");
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateFDFields()
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(textFDBBMDIP.Text) && !IsValidIPAddress(textFDBBMDIP.Text))
            {
                errorProvider.SetError(textFDBBMDIP, "Please enter a valid FD BBMD IP address");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(textFDBBMDIP, null);
            }

            if (numericFDBBMDPort.Value < 0 || numericFDBBMDPort.Value > 65535)
            {
                errorProvider.SetError(numericFDBBMDPort, "FD BBMD Port must be between 0 and 65535.");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(numericFDBBMDPort, null);
            }

            if (numericFDSubscriptionLifetime.Value < 0 || numericFDSubscriptionLifetime.Value > 65535)
            {
                errorProvider.SetError(numericFDSubscriptionLifetime, "FD Subscription Lifetime must be between 0 and 65535.");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(numericFDSubscriptionLifetime, null);
            }

            return isValid;
        }
        private void textObjectName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textObjectName.Text) && textObjectName.Text.Length > 25)
            {
                errorProvider.SetError(textObjectName, "Object Name must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(textObjectName, null);
            }
            if (string.IsNullOrEmpty(textObjectName.Text))
            {
                errorProvider.SetError(textObjectName, "Object name cannot be empty");
            }
        }

        private void textObjectName_Validating(object sender, CancelEventArgs e)
        {
            string text = textObjectName.Text.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Name cannot be empty.");
                return;
            }
            if (text.Length < 3)
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Name must be at least 3 characters long.");
                return;
            }
            if (text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Name cannot be longer than 25 characters.");
                return;
            }
            if (char.IsDigit(text[0]))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Name cannot start with a number.");
                return;
            }
            foreach (char ch in text)
            {
                if (!char.IsLetterOrDigit(ch) && ch != '_')
                {
                    e.Cancel = true;
                    errorProvider.SetError(textObjectName, "Only letters, digits, and underscore (_) are allowed. Spaces are not allowed.");
                    return;
                }
            }
            errorProvider.SetError(textObjectName, "");
        }

        private void textDescription_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textDescription.Text) && textDescription.Text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textDescription, "Maximum 25 characters allowed.");
            }
            else
            {
                errorProvider.SetError(textDescription, null);
            }
        }

        private void textDescription_TextChanged(object sender, EventArgs e)
        {
            if (textDescription.Text.Length > 25)
            {
                errorProvider.SetError(textDescription, "Description must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(textDescription, null);
            }
        }

        private bool IsValidIPAddress(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return true; 

            string[] parts = ipAddress.Split('.');
            if (parts.Length != 4) 
                return false;

            foreach (string part in parts)
            {
                if (!int.TryParse(part, out int value))
                    return false;

                if (value < 0 || value > 255)
                    return false;
            }

            return true;
        }

        private void ValidateIPAddressField(TextBox textBox, CancelEventArgs e, string fieldName)
        {
            string ipText = textBox.Text?.Trim();

            if (!string.IsNullOrEmpty(ipText) && !IsValidIPAddress(ipText))
            {
                e.Cancel = true;
                errorProvider.SetError(textBox, $"Please enter a valid {fieldName}");
            }
            else
            {
                errorProvider.SetError(textBox, null);
            }
        }

        private void textIPAddress_Validating(object sender, CancelEventArgs e)
        {
            ValidateIPAddressField((TextBox)sender, e, "IP address");
        }

        private void textSubnetMask_Validating(object sender, CancelEventArgs e)
        {
            ValidateIPAddressField((TextBox)sender, e, "subnet mask");
        }

        private void textDefaultGateway_Validating(object sender, CancelEventArgs e)
        {
            ValidateIPAddressField((TextBox)sender, e, "gateway address");
        }

        private void textDNSServer_Validating(object sender, CancelEventArgs e)
        {
            ValidateIPAddressField((TextBox)sender, e, "DNS server address");
        }

        private void textFDBBMDIP_Validating(object sender, CancelEventArgs e)
        {
            ValidateIPAddressField((TextBox)sender, e, "FD BBMD IP address");
        }

        private void numericNetworkNumber_Validating(object sender, CancelEventArgs e)
        {
            if (numericNetworkNumber.Value < 1 || numericNetworkNumber.Value > 65535)
            {
                e.Cancel = true;
                errorProvider.SetError(numericNetworkNumber, "Network Number must be between 1 and 65535.");
            }
            else
            {
                errorProvider.SetError(numericNetworkNumber, null);
            }
        }

        private void numericBacNetUDPPort_Validating(object sender, CancelEventArgs e)
        {
            if (numericBacNetUDPPort.Value < 0 || numericBacNetUDPPort.Value > 65535)
            {
                e.Cancel = true;
                errorProvider.SetError(numericBacNetUDPPort, "UDP Port must be between 0 and 65535.");
            }
            else
            {
                errorProvider.SetError(numericBacNetUDPPort, null);
            }
        }

        private void numericFDBBMDPort_Validating(object sender, CancelEventArgs e)
        {
            if (numericFDBBMDPort.Value < 0 || numericFDBBMDPort.Value > 65535)
            {
                e.Cancel = true;
                errorProvider.SetError(numericFDBBMDPort, "FD BBMD Port must be between 0 and 65535.");
            }
            else
            {
                errorProvider.SetError(numericFDBBMDPort, null);
            }
        }

        private void numericFDSubscriptionLifetime_Validating(object sender, CancelEventArgs e)
        {
            if (numericFDSubscriptionLifetime.Value < 0 || numericFDSubscriptionLifetime.Value > 65535)
            {
                e.Cancel = true;
                errorProvider.SetError(numericFDSubscriptionLifetime, "FD Subscription Lifetime must be between 0 and 65535.");
            }
            else
            {
                errorProvider.SetError(numericFDSubscriptionLifetime, null);
            }
        }

        private void comboBacNetIPMode_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index >= 0)
            {
                string itemText = comboBacNetIPMode.Items[e.Index].ToString();
                Brush brush;

                if (e.Index == 2)
                {
                    brush = Brushes.Gray;
                }
                else
                {
                    brush = Brushes.Black;
                }

                e.Graphics.DrawString(itemText, e.Font, brush, e.Bounds);
            }

            e.DrawFocusRectangle();
        }

        private void comboBacNetIPMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = comboBacNetIPMode.SelectedIndex;

            if (currentIndex == 2)
            {
                comboBacNetIPMode.SelectedIndex = _lastValidBacNetIPModeIndex;
            }
            else
            {
                _lastValidBacNetIPModeIndex = currentIndex;
            }
        }

        public void RefreshFormData()
        {
            AssignValues();
        }

        private void IPTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
                return;
            }

            if (char.IsDigit(e.KeyChar))
            {
                string currentText = textBox.Text;
                int cursorPos = textBox.SelectionStart;

                int lastDotIndex = cursorPos > 0 ? currentText.LastIndexOf('.', cursorPos - 1) : -1;
                int octetStart = lastDotIndex + 1;
                int octetLength = cursorPos - octetStart;

                if (octetLength >= 3 && textBox.SelectionLength == 0)
                {
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar == '.' && textBox.Text.Count(c => c == '.') >= 3)
            {
                e.Handled = true;
                return;
            }
        }

        private void IPTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Length > 15)
            {
                textBox.Text = textBox.Text.Substring(0, 15);
                textBox.SelectionStart = textBox.Text.Length;
            }

            string[] octets = textBox.Text.Split('.');
            foreach (string octet in octets)
            {
                if (!string.IsNullOrEmpty(octet) && int.TryParse(octet, out int value))
                {
                    if (value > 255)
                    {
                        errorProvider.SetError(textBox, "Each octet must be between 0-255");
                        return;
                    }
                }
            }

            errorProvider.SetError(textBox, null);
        }
    }
}
