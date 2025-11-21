using System.Data;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;

namespace XMPS2000.Bacnet
{
    public partial class BacNetObjectInformation : Form
    {
        public BacNetObjectInformation()
        {
            InitializeComponent();
            BindDataToGrid();
        }
        private void BindDataToGrid()
        {
            bacNetObjectInfoGrid.Rows.Clear();

            DeviceModel systemConfiguration = XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
            var ethernetDevices = systemConfiguration.Templates
                ?.Where(template => template.Ethernet != null)
                .ToList();

            var bacNetDevice = ethernetDevices?
                .SelectMany(t => t.Ethernet.TreeNodes)
                .SelectMany(node => node.Devices)
                .FirstOrDefault(device => device.Name == "BacNet");

            if (bacNetDevice != null)
            {
                int totalCurrentCount = 0;
                int totalMaxCount = 0;
                var hasNotificationClass = bacNetDevice.Properties.Any(p => p.Name == "Notification Class");
                var hasNotification = bacNetDevice.Properties.Any(p => p.Name == "Notification");
                if (hasNotification && !hasNotificationClass)
                {
                    foreach (var notificationProperty in bacNetDevice.Properties.Where(p => p.Name == "Notification"))
                    {
                        notificationProperty.Name = "Notification Class";
                    }
                }
                foreach (var property in bacNetDevice.Properties)
                {
                    // Add property details to the DataGridView
                    bacNetObjectInfoGrid.Rows.Add(property.Name, property.CurrentCount, property.MaxCount);
                    totalCurrentCount += property.CurrentCount;
                    totalMaxCount += property.MaxCount;
                }
                if (!bacNetDevice.Properties.Any(p => p.Name == "Network Port"))
                {
                    bacNetObjectInfoGrid.Rows.Add("Network Port", 1, 1);
                    totalCurrentCount++;
                    totalMaxCount++;
                }
                bacNetObjectInfoGrid.Rows.Add("Total", totalCurrentCount, totalMaxCount);
            }
        }
    }
}
