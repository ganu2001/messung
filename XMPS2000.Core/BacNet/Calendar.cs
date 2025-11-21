
using System.Collections.Generic;

namespace XMPS2000.Core.BacNet
{
    public class Calendar
    {
        public string ObjectIdentifier { get; set; }
        public string InstanceNumber { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
        public List<SpecialEvent> Events { get; set; }
        
        public Calendar()
        {
            Events = new List<SpecialEvent>();
        }
        public Calendar(string objectIdentifier, string instanceNumber, string objectType, string objectName, string description)
        {
            ObjectIdentifier = objectIdentifier;
            InstanceNumber = instanceNumber;
            ObjectType = objectType;
            ObjectName = objectName;
            Description = description;
        }
    }
}
