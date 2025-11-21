using System.Collections.Generic;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.Core.BacNet
{
    public class BacNetIP
    {
        public string BacNetType;

        public Devices Device { get; set; }
        public NetworkPort NetworkPort { get; set; }
        public List<AnalogIOV> AnalogIOValues { get; set; }
        public List<RESISTANCETable_Values> ResistanceValue { get; set; }
        public List<BinaryIOV> BinaryIOValues { get; set; }

        public List<MultistateIOV> MultistateValues { get; set; }
        public List<Notification> Notifications { get; set; }

        public List<Schedule> Schedules { get ; set ; }
        public List<Calendar> Calendars { get; set; }
        public BacNetIP()
        {
            Device = new Devices();
            NetworkPort = new NetworkPort();
            AnalogIOValues = new List<AnalogIOV>();
            BinaryIOValues = new List<BinaryIOV>();
            MultistateValues = new List<MultistateIOV>();
            Schedules = new List<Schedule>();
            Calendars = new List<Calendar>();
            Notifications = new List<Notification>();
            ResistanceValue= new List<RESISTANCETable_Values>();
        }
    }

    
}