namespace XMPS2000.Core.BacNet
{
    public class Devices
    {

        public string ObjectIdentifier { get; set; }
        public string InstanceNumber { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
        public string APDUTimeout { get; set; }
        public string APDUSegmentTimout { get; set; }
        public string APDURetries { get; set; }
        public string Location { get; set; }
        public string DayLightSavingStatus { get; set; }
        public string UTCOffset { get; set; }
        public Devices(string obj_identifier, string inst_number, string obj_type, string Obj_name, string description, bool isEnable)
        {
            ObjectIdentifier = obj_identifier;
            InstanceNumber = inst_number;
            ObjectType = obj_type;
            ObjectName = Obj_name;
            Description = description;
            IsEnable = isEnable;
        }
        public Devices()
        {

        }
    }
}
