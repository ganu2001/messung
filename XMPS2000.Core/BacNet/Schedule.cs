using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XMPS2000.Core.BacNet
{
    public class Schedule
    {
        public string ObjectIdentifier { get; set; }
        public string InstanceNumber { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public int ScheduleValue { get; set; }
        public bool IsEnable { get; set; }
        public bool AnyCheck { get; set; }
        public EffectivePeriod StartDate { get; set; }
        public EffectivePeriod EndDate { get; set; }
        public string ScheduleType { get; set; }
        public string NoOfDaySelected { get; set; }
        public List<EventParticularDay> EventParticularDays { get; set; }
        public List<SpecialEvent> specialEvents { get; set; }
        public string LogicalAddress { get; set; }
        public bool Nullvalue { get; set; }
        public string Value { get; set; }

        public Schedule()
        {
            EventParticularDays = new List<EventParticularDay>();
        }

        public Schedule(string objectIdentifier, string instanceNumber, string objectType, string objectName, string description, string logicalAddress, int scheduleType)
        {
            ObjectIdentifier = objectIdentifier;
            InstanceNumber = instanceNumber;
            ObjectType = objectType;
            ObjectName = objectName;
            Description = description;
            LogicalAddress = logicalAddress;
            ScheduleValue = scheduleType;
            AnyCheck = true;

            StartDate = new EffectivePeriod()
            {
                Date = DateTime.Now,
                DayOfWeek = (int)DateTime.Now.DayOfWeek
            };
            EndDate = new EffectivePeriod()
            {
                Date = DateTime.Now.AddYears(5),
                DayOfWeek = (int)DateTime.Now.AddYears(5).DayOfWeek
            };
        }
    }
    public class EffectivePeriod
    {
        public DateTime Date { get; set; }
        public int DayOfWeek { get; set; }
    }

    public class EventParticularDay
    {
        public int NoOfEventParticularDay { get; set; }
        public string DayName { get; set; }
        public List<EventValue> EventValues { get; set; }

        public EventParticularDay()
        {
            EventValues = new List<EventValue>();
        }
    }

    public class EventValue
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double Value { get; set; }
    }

    [XmlInclude(typeof(DateEvent))]
    [XmlInclude(typeof(DateRangeEvent))]
    [XmlInclude(typeof(WeekAndDayEvent))]
    [XmlInclude(typeof(CalendarReference))]
    public abstract class SpecialEvent
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Type { get; set; }
        public int EventId { get; set; }
        public List<EventValue> EventValues;
        public SpecialEvent()
        {
            EventValues = new List<EventValue>();
        }
    }
    public class DateEvent : SpecialEvent
    {
        public DateTime StartDate { get; set; }
        public bool isWeekDayAny { get; set; }
        public bool isDayAny { get; set; }
        public bool isMonthAny { get; set; }
        public bool isYearAny { get; set; }
    }
    public class DateRangeEvent : SpecialEvent
    {
        public DateTime StartDate { get; set; }
        public bool isStartDateAny { get; set; }
        public DateTime EndDate { get; set; }
        public bool isEndDateAny { get; set; }
    }
    public class WeekAndDayEvent : SpecialEvent
    {
        public int MonthValue { get; set; }
        public int WeekValue { get; set; }
        public int DayValue { get; set; }
    }
    public class CalendarReference : SpecialEvent
    {
        public string CalendarObjectName { get; set; }
        public int CalendarInstaceNumber { get; set; }
    }

}
