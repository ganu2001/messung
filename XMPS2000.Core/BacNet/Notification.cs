using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace XMPS2000.Core.BacNet
{
    public class Notification
    {
        public string ObjectIdentifier { get; set; }
        public string InstanceNumber { get; set; }
        public string ObjectType { get; set; }
        public string DeviceType { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
        public int OffNormalPriority { get; set; }
        public int FaultPriority { get; set; }
        public int NormalPriority { get; set; }
        public int AckRequired { get; set; }

        public List<Recipient> Recipients { get; set; }

        public Notification(string objectIdentifier, string instanceNumber, string objectType, string deviceType, string objectName)
        {
            ObjectIdentifier = objectIdentifier;
            InstanceNumber = instanceNumber;
            ObjectType = objectType;
            DeviceType = deviceType;
            ObjectName = objectName;
            IsEnable = true;
            Recipients = new List<Recipient>();
        }
        public Notification()
        {

        }
    }

    public class Recipient
    {
        public string Name { get; set; }
        public string DaysofWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ProcessIdentifier { get; set; }
        public string Notification { get; set; }
        public string RecipientType { get; set; }
        public string DeviceInstance { get; set; }
        public int Transition { get; set; }

        public Recipient()
        {

        }
    }
}
