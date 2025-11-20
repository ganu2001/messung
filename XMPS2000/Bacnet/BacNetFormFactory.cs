using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.LadderLogic;

namespace XMPS2000.Bacnet
{
    internal class BacNetFormFactory
    {
        internal static bool ValidateObjectName(string ObjectName, string ObjectType)
        {
            BacNetIP bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            string logicalAddress = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag.Equals(ObjectName)).Select(t => t.LogicalAddress).FirstOrDefault();
            if (XMPS.Instance.LoadedProject.Tags.Any(t => t.Tag.Equals(ObjectName.ToString())) && ObjectType != "Tag")
                return true;
            if (bacNetIP == null)
                return false;
            if ((bacNetIP.BinaryIOValues?.Any(t => t.ObjectName != null && t.LogicalAddress != null &&
                 t.ObjectName.Equals(ObjectName.ToString()) && !t.LogicalAddress.Equals(logicalAddress)) ?? false) ||
                 (bacNetIP.AnalogIOValues?.Any(t => t.ObjectName != null && t.LogicalAddress != null &&
                 t.ObjectName.Equals(ObjectName.ToString()) && !t.LogicalAddress.Equals(logicalAddress)) ?? false) ||
                 (bacNetIP.MultistateValues?.Any(t => t.ObjectName != null && t.LogicalAddress != null &&
                 t.ObjectName.Equals(ObjectName.ToString()) && !t.LogicalAddress.Equals(logicalAddress)) ?? false) ||
                 (bacNetIP.Schedules?.Any(t => t.ObjectName != null && t.LogicalAddress != null &&
                 t.ObjectName.Equals(ObjectName.ToString()) && !t.LogicalAddress.Equals(logicalAddress)) ?? false) ||
                 (bacNetIP.Calendars?.Any(t => t.ObjectName != null &&
                 t.ObjectName.Equals(ObjectName.ToString())) ?? false) ||
                 (bacNetIP.Notifications?.Any(t => t.ObjectName != null &&
                 t.ObjectName.Equals(ObjectName.ToString())) ?? false) ||
                 (bacNetIP.Device?.ObjectName != null &&
                 bacNetIP.Device.ObjectName.Equals(ObjectName.ToString())))

                return true;
            return false;
        }

        public Form GetForm(string cellValue, string objectType)
        {
            if (objectType == "Device")
            {
                return new FormDevice( XMPS.Instance.PlcStatus == "LogIn");
            }
            else if (objectType == "NetworkPort")
            {
                return new FormNetworkPort(XMPS.Instance.PlcStatus == "LogIn");
            }
            else if (objectType == "Notification Class")
            {
                return new FormNotification(cellValue, XMPS.Instance.PlcStatus == "LogIn");
            }
            else if (objectType == "Schedule")
            {
                return new FormSchedule(cellValue, XMPS.Instance.PlcStatus == "LogIn");
            }
            else if (objectType == "Calendar")
            {
                return new FormCalendar(cellValue, XMPS.Instance.PlcStatus == "LogIn");
            }
            else
            {
                XMIOConfig ioconfig = XMProValidator.GetTagFromTagName(cellValue);

                if (ioconfig != null)
                {
                    return GetFormByTag(ioconfig, objectType);
                }
            }
            return null;
        }

        private Form GetFormByTag(XMIOConfig ioconfig, string objectType)
        {
            if (objectType.StartsWith("Analog"))
            {
                return new FormAnalogBacNet(ioconfig, XMPS.Instance.PlcStatus == "LogIn");
            }
            else if (objectType == "BinaryValue")
            {
                return new FormDigitalBacNet(ioconfig, XMPS.Instance.PlcStatus == "LogIn");
            }
            else if (objectType == "MultiStateValue")
            {
                return new FormMultiStateBacNet(ioconfig, XMPS.Instance.PlcStatus == "LogIn");
            }
            else
            {
                return new FormDigitalBacNet(ioconfig, XMPS.Instance.PlcStatus == "LogIn");
            }
        }

    }
}
