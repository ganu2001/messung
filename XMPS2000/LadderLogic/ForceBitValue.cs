using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;

namespace XMPS2000.LadderLogic
{
    public partial class ForceBitValue : UserControl
    {
        private readonly PLCForceFunctionality forceFunctionality = PLCForceFunctionality.Instance;
        string Logical_Address;
        public ForceBitValue(string logicalAddress)
        {
            InitializeComponent();
            OnlineMonitoringHelper.HoldOnlineMonitor = true;
            Logical_Address = logicalAddress;
        }

        private void btnTrue_Click(object sender, EventArgs e)
        {
            ForceValue(true);
        }

        private void ForceValue(bool value)
        {
            CommonForceFunctionality(value);
            this.ParentForm.Close();

        }

        private void btnFalse_Click(object sender, EventArgs e)
        {
            ForceValue(false);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            ForceValue(true);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            ForceValue(false);
        }

        private void ForceBitValue_Leave(object sender, EventArgs e)
        {
            OnlineMonitoringHelper.HoldOnlineMonitor = false;
        }

        private void btnUnforce_Click(object sender, EventArgs e)
        {
            UnForceValue();
        }

        private void UnForceValue()
        {
            CommonUnForceFunctionality();
            this.ParentForm.Close();
        }
        public void CommonUnForceFunctionality()
        {
            OnlineMonitoringHelper.HoldOnlineMonitor = true;
            PLCCommunications pLCCommunications = new PLCCommunications();
            if (pLCCommunications.GetIPAddress() != "Error")
                PLCForceFunctionality.Tftpaddress = pLCCommunications.Tftpaddress.ToString();
            else
            {
                string errmsg = XMPS2000.CommonFunctions.GetEasyConnection(XMPS.Instance._connectedIPAddress);
                MessageBox.Show(errmsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string tagname = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == Logical_Address).Select(t => t.Tag).FirstOrDefault();
            XMPS.Instance.Forcedvalues.RemoveAll(l => l == tagname);
            MessageBox.Show(forceFunctionality.CreateAndSendFrame(Logical_Address, "0", false), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OnlineMonitoringHelper.HoldOnlineMonitor = false;
        }
        public void CommonForceFunctionality(bool value)
        {
            try
            {
                OnlineMonitoringHelper.HoldOnlineMonitor = true;
                PLCCommunications pLCCommunications = new PLCCommunications();
                if (pLCCommunications.GetIPAddress() != "Error")
                    PLCForceFunctionality.Tftpaddress = pLCCommunications.Tftpaddress.ToString();
                else
                {
                    string errmsg = XMPS2000.CommonFunctions.GetEasyConnection(XMPS.Instance._connectedIPAddress);
                    MessageBox.Show(errmsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                XMPS.Instance.Forcedvalues.Add(XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == Logical_Address).Select(t => t.Tag).FirstOrDefault());
                MessageBox.Show(forceFunctionality.CreateAndSendFrame(Logical_Address, value == true ? "1" : "0"), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnlineMonitoringHelper.HoldOnlineMonitor = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                OnlineMonitoringHelper.HoldOnlineMonitor = false;
            }

        }
    }
}
