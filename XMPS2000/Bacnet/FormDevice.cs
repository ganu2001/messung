using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;

namespace XMPS2000.Bacnet
{

    public partial class FormDevice : Form
    {
        private BacNetIP bacNetIP;
        private readonly bool _isReadOnly;
        public FormDevice(bool isReadOnly = false)
        {
            InitializeComponent();
            LoadUTCOffsets();
            AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            XMPS.Instance.BacNetCurrentScreen = "FormDevice";
            _isReadOnly = isReadOnly;
            ApplyReadOnly();
            AutoResizeComboBoxDropDownWidth(cmb_UTCOffset);
        }
        private void ApplyReadOnly()
        {
            if (_isReadOnly)
            {
                textInstanceNo.Enabled = false;
                textObjectName.Enabled = false;
                textDescription.Enabled = false;
                numericAPDUTimeout.Enabled = false;
                numericAPDUSegment.Enabled = false;
                numericNoofAPDU.Enabled = false;
                textLocation.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                groupBox3.Enabled = false;
                cmb_UTCOffset.Enabled = false;
            }
        }
        private void LoadUTCOffsets()
        {
            var timeZones = new List<string>
            {
                "(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi",
                "(UTC-12:00) International Date Line West",
                "(UTC-11:00) Coordinated Universal Time-11",
                "(UTC-10:00) Hawaii",
                "(UTC-09:00) Alaska",
                "(UTC-08:00) Baja California",
                "(UTC-08:00) Pacific Time (US & Canada)",
                "(UTC-07:00) Arizona",
                "(UTC-07:00) Chihuahua, La Paz, Mazatlan",
                "(UTC-07:00) Mountain Time (US & Canada)",
                "(UTC-06:00) Central America",
                "(UTC-06:00) Central Time (US & Canada)",
                "(UTC-06:00) Guadalajara, Mexico City, Monterrey",
                "(UTC-06:00) Saskatchewan",
                "(UTC-05:00) Bogota, Lima, Quito, Rio Branco",
                "(UTC-05:00) Eastern Time (US & Canada)",
                "(UTC-05:00) Indiana (East)",
                "(UTC-04:30) Caracas",
                "(UTC-04:00) Asuncion",
                "(UTC-04:00) Atlantic Time (Canada)",
                "(UTC-04:00) Cuiaba",
                "(UTC-04:00) Georgetown, La Paz, Manaus, San Juan",
                "(UTC-04:00) Santiago",
                "(UTC-03:30) Newfoundland",
                "(UTC-03:00) Brasilia",
                "(UTC-03:00) Buenos Aires",
                "(UTC-03:00) Cayenne, Fortaleza",
                "(UTC-03:00) Greenland",
                "(UTC-03:00) Montevideo",
                "(UTC-03:00) Salvador",
                "(UTC-02:00) Coordinated Universal Time-02",
                "(UTC-01:00) Azores",
                "(UTC-01:00) Cape Verde Is.",
                "(UTC) Casablanca",
                "(UTC) Coordinated Universal Time",
                "(UTC) Dublin, Edinburgh, Lisbon, London",
                "(UTC) Monrovia, Reykjavik",
                "(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienne",
                "(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague",
                "(UTC+01:00) Brussels, Copenhagen, Madrid, Paris",
                "(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb",
                "(UTC+01:00) West Central Africa",
                "(UTC+01:00) Windhoek",
                "(UTC+02:00) Amman",
                "(UTC+02:00) Athens, Bucharest",
                "(UTC+02:00) Beirut",
                "(UTC+02:00) Cairo",
                "(UTC+02:00) Damascus",
                "(UTC+02:00) Harare, Pretoria",
                "(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius",
                "(UTC+02:00) Istanbul",
                "(UTC+02:00) Jerusalem",
                "(UTC+02:00) Kaliningrad (RTZ 1)",
                "(UTC+02:00) Tripoli",
                "(UTC+03:00) Baghdad",
                "(UTC+03:00) Kuwait, Riyadh",
                "(UTC+03:00) Minsk",
                "(UTC+03:00) Moscow, St. Petersburg, Volgograd (RTZ 2)",
                "(UTC+03:00) Nairobi",
                "(UTC+03:30) Tehran",
                "(UTC+04:00) Abu Dhabi, Muscat",
                "(UTC+04:00) Baku",
                "(UTC+04:00) Izhevsk, Samara (RTZ 3)",
                "(UTC+04:00) Port Louis",
                "(UTC+04:00) Tbilisi",
                "(UTC+04:00) Yerevan",
                "(UTC+04:30) Kabul",
                "(UTC+05:00) Ashgabat, Tashkent",
                "(UTC+05:00) Ekaterinburg (RTZ 4)",
                "(UTC+05:00) Islamabad, Karachi",
                "(UTC+05:30) Sri Jayawardenepura",
                "(UTC+05:45) Kathmandu",
                "(UTC+06:00) Astana",
                "(UTC+06:00) Dhaka",
                "(UTC+06:00) Novosibirsk (RTZ 5)",
                "(UTC+06:30) Yangon (Rangoon)",
                "(UTC+07:00) Bangkok, Hanoi, Jakarta",
                "(UTC+07:00) Krasnoyarsk (RTZ 6)",
                "(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi",
                "(UTC+08:00) Irkutsk (RTZ 7)",
                "(UTC+08:00) Kuala Lumpur, Singapore",
                "(UTC+08:00) Perth",
                "(UTC+08:00) Taipei",
                "(UTC+08:00) UlaanBaatar",
                "(UTC+09:00) Osaka, Sapporo, Tokyo",
                "(UTC+09:00) Seoul",
                "(UTC+09:00) Yakutsk (RTZ 8)",
                "(UTC+09:30) Adelaide",
                "(UTC+09:30) Darwin",
                "(UTC+10:00) Brisbane",
                "(UTC+10:00) Canberra, Melbourne, Sydney",
                "(UTC+10:00) Guam, Port Moresby",
                "(UTC+10:00) Hobart",
                "(UTC+10:00) Magadan",
                "(UTC+10:00) Vladivostok, Magadan (RTZ 9)",
                "(UTC+11:00) Chokurdakh (RTZ 10)",
                "(UTC+11:00) Solomon Is., New Caledonia",
                "(UTC+12:00) Anadyr, Petropavlovsk-Kamchatsky (RTZ 11)",
                "(UTC+12:00) Auckland, Wellington",
                "(UTC+12:00) Coordinated Universal Time+12",
                "(UTC+12:00) Fiji",
                "(UTC+13:00) Nuku'alofa",
                "(UTC+13:00) Samoa",
                "(UTC+14:00) Kiritimati Island"
            };

            cmb_UTCOffset.DataSource = timeZones;
        }
        private void AutoResizeComboBoxDropDownWidth(ComboBox comboBox)
        {
            int maxWidth = 0;
            using (Graphics g = comboBox.CreateGraphics())
            {
                foreach (var item in comboBox.Items)
                {
                    // Measure the text width of each item
                    int itemWidth = (int)g.MeasureString(item.ToString(), comboBox.Font).Width;
                    if (itemWidth > maxWidth)
                    {
                        maxWidth = itemWidth;
                    }
                }
            }
            comboBox.DropDownWidth = maxWidth;
        }
        private void AssignValues()
        {
            bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            Devices device = bacNetIP.Device;
            if (device != null)
            {
                labelObjIdentifier.Text = device.ObjectIdentifier;
                textInstanceNo.Text = device.InstanceNumber;
                labelObjType.Text = device.ObjectType;
                textObjectName.Text = device.ObjectName;
                textDescription.Text = device.Description;
                textModelName.Text = XMPS.Instance.LoadedProject.PlcModel.EndsWith("Pro10E") ? "XBLDPro-10E" : "XBLD-Pro10";
                numericAPDUTimeout.Value = !string.IsNullOrEmpty(device.APDUTimeout) ? (decimal)int.Parse(device.APDUTimeout) : 6000;
                numericAPDUSegment.Value = !string.IsNullOrEmpty(device.APDUSegmentTimout) ? (decimal)int.Parse(device.APDUSegmentTimout) : 5000;
                numericNoofAPDU.Value = !string.IsNullOrEmpty(device.APDURetries) ? (decimal)int.Parse(device.APDURetries) : 3;
                textLocation.Text = device.Location;
                string dls = device.DayLightSavingStatus?.Trim().ToLowerInvariant();
                if (dls == "1")
                {
                    ChkFalse.Checked = false;
                    ChkTrue.Checked = true;
                }
                else
                {
                    ChkFalse.Checked = true;
                    ChkTrue.Checked = false;
                }
                int offsetValue = 0;
                if (!string.IsNullOrWhiteSpace(device.UTCOffset))
                {
                    offsetValue = Convert.ToInt32(device.UTCOffset);
                    if (offsetValue == -330)
                    {
                        offsetValue = 0;
                    }
                }
                cmb_UTCOffset.SelectedIndex = offsetValue;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.AssignValues();
            XMPS.Instance.LoadedProject.isChanged = false;
            ((FormBacNet)this.ParentForm).RefreshGridView();
        }
        private void textInstanceNo_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToUInt64(textInstanceNo.Text) >= 0 && Convert.ToUInt64(textInstanceNo.Text) <= 4194302)
            {
                e.Cancel = false;
                errorProvider.SetError(textInstanceNo, null);
            }
            else
            {
                e.Cancel = true;
                errorProvider.SetError(textInstanceNo, "Entered value should be in range of 0 to 4194302");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges(sender, e);
        }
        private void Cmb_UTCOffset_MouseWheel(object sender, MouseEventArgs e)
        {
            var combo = sender as ComboBox;

            if (combo != null && combo.Focused)
            {
                int newIndex = combo.SelectedIndex - Math.Sign(e.Delta);

                newIndex = Math.Max(0, Math.Min(combo.Items.Count - 1, newIndex));
                combo.SelectedIndex = newIndex;

                return; 
            }

            XMPS.Instance.LoadedProject.isChanged = true;
        }
        private bool SaveChanges(object sender, EventArgs e)
        {
            string oldName = XMPS.Instance.LoadedProject.BacNetIP.Device.ObjectName;
            if (BacNetFormFactory.ValidateObjectName(textObjectName.Text.ToString().Trim(), "Device") && oldName != textObjectName.Text.ToString().Trim())
            {
                MessageBox.Show("Object name is already used, change the name and try again ...", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please resolve the errors first", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Devices device = new Devices();
            bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            labelObjIdentifier.Text = labelObjType.Text.ToString().Split(':')[1].ToString() + " : " + textInstanceNo.Text.ToString();
            device.ObjectIdentifier = labelObjIdentifier.Text;
            device.InstanceNumber = textInstanceNo.Text;
            device.ObjectType = labelObjType.Text;
            device.ObjectName = textObjectName.Text.Trim();
            device.Description = textDescription.Text;
            device.APDUTimeout = numericAPDUTimeout.Value.ToString();
            device.APDUSegmentTimout = numericAPDUSegment.Value.ToString();
            device.APDURetries = numericNoofAPDU.Value.ToString();
            device.Location = textLocation.Text;
            device.IsEnable = bacNetIP.Device.IsEnable;
            string DayLightSavingStatus = "";
            if (ChkFalse.Checked)
            {
                DayLightSavingStatus = "0";
            }
            else if (ChkTrue.Checked)
            {
               
                DayLightSavingStatus = "1";
            }

            device.DayLightSavingStatus = DayLightSavingStatus;
            device.UTCOffset = Convert.ToString(cmb_UTCOffset.SelectedIndex);
            bacNetIP.Device = device;
            MessageBox.Show("Device infomation updated", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Refresh the Main BacNet Grid;
            XMPS.Instance.LoadedProject.isChanged = false;
            if ((FormBacNet)this.ParentForm != null)
                ((FormBacNet)this.ParentForm).RefreshGridView();
            else
            {
                var formBacNetInstance = Application.OpenForms
                              .OfType<FormBacNet>()
                              .FirstOrDefault(f => f.Name == "FormBacNet");
                formBacNetInstance?.RefreshGridView();
            }
            return true;
        }

        //private void textObjectName_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (textObjectName.Text.Length >= 20 && !char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void textDescription_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (textDescription.Text.Length >= 20 && !char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void textInstanceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textInstanceNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textInstanceNo.Text))
            {
                textInstanceNo.Text = "2000";
                textInstanceNo.SelectionStart = textInstanceNo.Text.Length;
            }
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
                //e.Cancel = true;
                errorProvider.SetError(textObjectName, "Please resolve the errors first");
            }
        }

        private void labelObjType_TextChanged(object sender, EventArgs e)
        {
            if (labelObjType.Text.Length > 25)
            {
                errorProvider.SetError(labelObjType, "Object Name must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(labelObjType, null);
            }
        }

        private void textObjectName_Validating(object sender, CancelEventArgs e)
        {
            string text = textObjectName.Text;

            // Length check
            if (!string.IsNullOrWhiteSpace(text) && text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Maximum 25 characters allowed.");
                return;
            }

            // Empty check
            if (string.IsNullOrWhiteSpace(text))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Name cannot be empty.");
                return;
            }
            if (char.IsDigit(textObjectName.Text[0]))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Tag name cannot start with a number.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(textObjectName.Text.Trim()) && textObjectName.Text.Trim().Any(ch => !(char.IsLetterOrDigit(ch) || ch == '_' || ch != ' ')))
            {
                e.Cancel = true;
                errorProvider.SetError(textObjectName, "Only letters, digits, underscore (_) and spaces are allowed.");
            }
            // Special character check (allow letters, digits, spaces, and underscore)
            foreach (char ch in textObjectName.Text.Trim())
            {
                if (!char.IsLetterOrDigit(ch) && ch != 95 && ch != 3 && ch != 22) // Allowed characters
                {
                    e.Cancel = true;
                    errorProvider.SetError(textObjectName, "Invalid character detected. Only letters, digits, and underscore (_) are allowed.");
                    return;
                }
            }
            // Clear error if valid
            errorProvider.SetError(textObjectName, "");
        }

        private void textDescription_Validating(object sender, CancelEventArgs e)
        {
            //for checking length
            if (!string.IsNullOrWhiteSpace(textDescription.Text) && textDescription.Text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textDescription, "Please resolve the errors first");
            }
        }

        private void textDescription_TextChanged(object sender, EventArgs e)
        {
            if (textDescription.Text.Length > 25)
            {
                errorProvider.SetError(textDescription, "Object Name must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(textDescription, null);
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkFalse.Checked)
            {
                ChkTrue.Checked = false;
            }
            else if (!ChkTrue.Checked)
            {
                ChkFalse.Checked = true; // Prevent deselecting both
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTrue.Checked)
            {
                ChkFalse.Checked = false;
            }
            else if (!ChkFalse.Checked)
            {
                ChkTrue.Checked = true; // Prevent deselecting both
            }
        }
    }
}