namespace XMPS2000.Core.BacNet
{
    public class BinaryIOV
    {
        public string ObjectIdentifier { get; set; }
        public string InstanceNumber { get; set; }
        public string ObjectType { get; set; }
        public string DeviceType { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public string LogicalAddress { get; set; }
        public int Polarity { get; set; }
        public int RelinquishDefault { get; set; }
        public int EventDetectionEnable { get; set; }
        public long TimeDelay { get; set; }
        public long TimeDelayNormal { get; set; }
        public int NotificationClass { get; set; }
        public int EventEnable { get; set; }
        public int NotifyType { get; set; }
        public string ActiveText { get; set; }
        public string InactiveText { get; set; }
        public int FeedbackValue { get; set; }
        public string TagValue { get; set; }
        public bool IsEnable { get; set; }
        public int BinaryBValue { get; set; }
        public string SelectedFeedbackText { get; set; }
        public BinaryIOV(string objectIdentifier, string instanceNumber, string objectType, string deviceType, string objectName, string logicalAddress, bool isEnable = false)
        {
            ObjectIdentifier = objectIdentifier;
            InstanceNumber = instanceNumber;
            ObjectType = objectType;
            DeviceType = deviceType;
            ObjectName = objectName;
            LogicalAddress = logicalAddress;
            IsEnable = isEnable;
        }
        public BinaryIOV() { }
    }
}
