
using System.Collections.Generic;
namespace XMPS2000.Core.BacNet
{
    public class AnalogIOV
    {

        public string ObjectIdentifier { get; set; }
        public string InstanceNumber { get; set; }
        public string ObjectType { get; set; }
        public string DeviceType { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public string LogicalAddress { get; set; }
        public string Units { get; set; }
        public List<string> RecentUnits { get; set; } = new List<string>(); // Initialize to avoid null
        public decimal MinPresValue { get; set; }
        public decimal MaxPresValue { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public bool OutOfRangeEnabled { get; set; }
        public string RelinquishDefault { get; set; }
        public string COVIncrement { get; set; }
        public int EventDetectionEnable { get; set; }
        public long TimeDelay { get; set; }
        public long TimeDelayNormal { get; set; }
        public int NotificationClass { get; set; }
        public decimal HighLimit { get; set; }
        public decimal LowLimit { get; set; }
        public decimal Deadband { get; set; }
        public int LimitEnable { get; set; }
        public int EventEnable { get; set; }
        public int NotifyType { get; set; }
        public bool isEdited { get; set; }
        public bool IsEnable { get; set; }
        public int BinaryAValue { get; set; }

        public AnalogIOV(string objectIdentifier, string instanceNumber, string objectType, string deviceType, string objectName, string logicalAddress, bool isEnable = false, string tagType = "")
        {
            ObjectIdentifier = objectIdentifier;
            InstanceNumber = instanceNumber;
            ObjectType = objectType;
            DeviceType = deviceType;
            ObjectName = objectName;
            LogicalAddress = logicalAddress;
            IsEnable = isEnable;
            if (!string.IsNullOrEmpty(tagType) && tagType.StartsWith("Universal"))
            {
                MaxPresValue = 10000;
                COVIncrement = "1";
            }
            if (!string.IsNullOrEmpty(tagType) && tagType.StartsWith("Analog"))
            {
                MaxPresValue = 4095;
                COVIncrement = "1";
            }
        }
        public AnalogIOV()
        {

        }

    }
}
