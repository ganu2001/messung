
using System.Collections.Generic;

namespace XMPS2000.Core.BacNet
{
    public class MultistateIOV
    {

        public string ObjectIdentifier { get; set; }
        public string InstanceNumber { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public string LogicalAddress { get; set; }
        public long NumberOfStates { get; set; }
        public int EventDetectionEnable { get; set; }
        public long TimeDelay { get; set; }
        public long TimeDelayNormal { get; set; }
        public long AlarmValue { get; set; }
        public List<long> AlarmValues { get; set; } = new List<long>();

        public int NotificationClass { get; set; }
        public int EventEnable { get; set; }
        public int NotifyType { get; set; }
        public bool IsEnable { get; set; }
        public List<State> States { get; set; }
        public int BinaryValue { get; set; }
        public int RelinquishDefault { get; set; }
        public MultistateIOV(string objectIdentifier, string instanceNumber, string objectType, string objectName, string logicalAddress, long alarmValue)
        {
            ObjectIdentifier = objectIdentifier;
            InstanceNumber = instanceNumber;
            ObjectType = objectType;
            ObjectName = objectName;
            LogicalAddress = logicalAddress;
            AlarmValue = 0;
            if (alarmValue > 0)
            {
                AlarmValues.Add(alarmValue);
            }
            States = new List<State> { new State(1, "state text 1") };
            NumberOfStates = 1;
        }
        public MultistateIOV()
        {
            States = new List<State>();
            AlarmValues = new List<long>();
            AlarmValue = 0;
        }

    }
    public class State
    {
        public int StateNumber { get; set; }
        public string StateValue { get; set; }

        public State() { }

        public State(int stateNumber, string stateValue)
        {
            StateNumber = stateNumber;
            StateValue = stateValue;
        }
    }

}
