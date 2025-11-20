using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Devices;

namespace XMPS2000
{
    public partial class RtcForm : Form
    {

        public RtcForm()
        {
            InitializeComponent();
          //  dateTimePicker1.Value = DateTime.Now;
            
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            { 
             DateTime Date1;
             Date1 = DateTime.Today;
             // Date1 = dateTimePicker1.Value.D;
             // dateTimePicker1.Value = Date1.Date;
             //dateTimePicker1 = ;
             dtpickerDate.Value = Date1;
             dtpickerTime.Value = DateTime.Now;                                   
            }
        }
        private void UploadRtcBtn_Click(object sender, EventArgs e)
        {
           byte[] FrameData = new byte[12];
            int frmIndex = 0;
            int getcrc = 0;

            //Send Day, Month, Year to RtcFrame
            FrameData[frmIndex] = 0xF9;
            frmIndex++;
            FrameData[frmIndex] = 0x08;
            frmIndex++;
            FrameData[frmIndex] = 0xEC;
            frmIndex++;
            getcrc = Convert.ToInt32(0xEC);
            string day = dtpickerDate.Value.Day.ToString("X");
            FrameData[frmIndex] = Convert.ToByte(day, 16);
            getcrc = (getcrc ^ Convert.ToInt32(dtpickerDate.Value.Day.ToString("X"), 16));
            frmIndex++;
            string month =  dtpickerDate.Value.Month.ToString("X");
            FrameData[frmIndex] = Convert.ToByte(dtpickerDate.Value.Month.ToString("X"),16);
            getcrc = (getcrc ^ Convert.ToInt32(dtpickerDate.Value.Month.ToString("X"), 16));
            frmIndex++;
            string Year = dtpickerDate.Value.Year.ToString();
            int Yearbit = Convert.ToInt32(Year.Substring(2, 2).ToString());      
            string hex = Yearbit.ToString("X");  
            FrameData[frmIndex] = Convert.ToByte(Yearbit);
            getcrc = (getcrc ^ Convert.ToInt32(hex, 16));
            frmIndex++;
            //Send HH, MM, SS
            string hour = dtpickerTime.Value.Hour.ToString("X");
            FrameData[frmIndex] = Convert.ToByte(hour, 16);
            getcrc = (getcrc ^ Convert.ToInt32(dtpickerTime.Value.Hour.ToString("X"), 16));
            frmIndex++;
            string minute = dtpickerTime.Value.Minute.ToString("X");
            FrameData[frmIndex] = Convert.ToByte(minute,16);
            getcrc = (getcrc ^ Convert.ToInt32(dtpickerTime.Value.Minute.ToString("X"), 16));
            frmIndex++;
            string second = dtpickerTime.Value.Second.ToString("X");
            FrameData[frmIndex] = Convert.ToByte(second, 16);
            getcrc = (getcrc ^ Convert.ToInt32(dtpickerTime.Value.Second.ToString("X"), 16));
            frmIndex++;
            int format = (dtpickerTime.Value.Hour > 12) ? 1 : 0;             //HexConvert ??? 
            format.ToString("X");
            FrameData[frmIndex] = Convert.ToByte(format);
            getcrc = (getcrc ^ Convert.ToInt32(format.ToString(), 16));
            frmIndex++;
            int fincrc = (getcrc ^ Convert.ToInt32("97", 16));
            FrameData[frmIndex] = Convert.ToByte(fincrc);
            frmIndex++;
            FrameData[frmIndex] = 0xF8;
            string ipaddress;
            PLCCommunications pLCCommunications = new PLCCommunications();
            if (pLCCommunications.GetIPAddress() == "Error")
            {
                MessageBox.Show("Invalid Ip Address,Select the machine to connect and retry...", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                ipaddress = pLCCommunications.Tftpaddress;
            int response = pLCCommunications.ConnectRTc(ipaddress, FrameData,true);
            if (response == 170)///Reply sent when updated sucessfully
            {
                MessageBox.Show("RTC time updated sucessfully", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (response == 187)///Reply sent when error in updating
            {
                MessageBox.Show("Error while updating RTC time in PLC", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else ///Reply sent when error 
            {
                MessageBox.Show("Error while updating RTC time in PLC", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // pLCCommunications.RtcSetting()

        }
    }
}
