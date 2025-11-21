using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Base.Helpers;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.Core.App
{
    internal interface IBcodeFileGeneration
    {
        void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData);

    }
    public class DeviceBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var device = bacNetIP.Device;
            if (device != null && device.IsEnable)
            {
                bCodeData.Add("8513");
                bCodeData.Add(device.InstanceNumber);
                if (device.ObjectType == null) device.ObjectType = " ";
                bCodeData.Add(device.ObjectType.Split(':')[0].ToString());
                bCodeData.Add((device.APDUTimeout == null || string.IsNullOrEmpty(device.APDUTimeout)) ? "6000" : device.APDUTimeout);
                bCodeData.Add(device.APDUSegmentTimout);
                bCodeData.Add(device.APDURetries);
                bCodeData.Add($"\"{device.Location}\"");
                bCodeData.Add(string.IsNullOrWhiteSpace(device.DayLightSavingStatus) ? "0" : device.DayLightSavingStatus.Trim());
                bCodeData.Add(device.UTCOffset ?? "0");
                bCodeData.Add($"\"{device.ObjectName}\"");
                bCodeData.Add($"\"{device.Description}\"");
            }
        }
    }
    public class AnalogInputBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var analogInputs = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("0") && t.IsEnable)
                               .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
            bCodeData.Add("8514");
            bCodeData.Add(analogInputs.Count.ToString());

            foreach (var analogIOV in analogInputs)
            {
                AddAnalogInputData(bCodeData, analogIOV);
            }
        }

        private void AddAnalogInputData(List<string> bCodeData, AnalogIOV analogIOV)
        {
            bCodeData.Add(analogIOV.InstanceNumber);
            bCodeData.Add(analogIOV.EventDetectionEnable.ToString());
            if (analogIOV.ObjectType == null) analogIOV.ObjectType = " ";
            bCodeData.Add(analogIOV.ObjectType.Split(':')[0].ToString());
            if (analogIOV.Units == null) analogIOV.Units = "95: no-units";
            bCodeData.Add(analogIOV.Units.Split(':')[0].ToString());
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.MinPresValue.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.MaxPresValue.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.MinValue.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.MaxValue.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.COVIncrement?.ToString() == null ? "0" : analogIOV.COVIncrement.ToString()));
            bCodeData.Add(analogIOV.TimeDelay.ToString());
            bCodeData.Add(analogIOV.TimeDelayNormal.ToString());
            bCodeData.Add(analogIOV.NotificationClass.ToString());
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.HighLimit.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.LowLimit.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.Deadband.ToString()));
            bCodeData.Add(analogIOV.LimitEnable.ToString());
            bCodeData.Add(analogIOV.EventEnable.ToString());
            bCodeData.Add(analogIOV.NotifyType.ToString());
            bCodeData.Add(uint.Parse(XMPS.Instance.GetHexAddress(analogIOV.LogicalAddress.ToString()), System.Globalization.NumberStyles.HexNumber).ToString());
            bCodeData.Add($"\"{analogIOV.ObjectName}\"");
            bCodeData.Add($"\"{analogIOV.Description}\"");
        }

    }
    public class AnalogOutputBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var analogOutputs = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("1") && t.IsEnable)
                                .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
            bCodeData.Add("8515");
            bCodeData.Add(analogOutputs.Count.ToString());

            foreach (var analogIOV in analogOutputs)
            {
                AddAnalogOutputData(bCodeData, analogIOV);
            }
        }

        private void AddAnalogOutputData(List<string> bCodeData, AnalogIOV analogIOV)
        {
            bCodeData.Add(analogIOV.InstanceNumber);
            bCodeData.Add(analogIOV.EventDetectionEnable.ToString());
            if (analogIOV.ObjectType == null) analogIOV.ObjectType = " ";
            bCodeData.Add(analogIOV.ObjectType.Split(':')[0].ToString());
            if (analogIOV.Units == null) analogIOV.Units = "95: no-units";
            bCodeData.Add(analogIOV.Units.Split(':')[0].ToString());
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.MinPresValue.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.MaxPresValue.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV?.RelinquishDefault?.ToString() ?? "0"));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.COVIncrement?.ToString() == null ? "0.00" : analogIOV.COVIncrement.ToString()));
            bCodeData.Add(analogIOV.TimeDelay.ToString());
            bCodeData.Add(analogIOV.TimeDelayNormal.ToString());
            bCodeData.Add(analogIOV.NotificationClass.ToString());
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.HighLimit.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.LowLimit.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.Deadband.ToString()));
            bCodeData.Add(analogIOV.LimitEnable.ToString());
            bCodeData.Add(analogIOV.EventEnable.ToString());
            bCodeData.Add(analogIOV.NotifyType.ToString());
            bCodeData.Add(uint.Parse(XMPS.Instance.GetHexAddress(analogIOV.LogicalAddress.ToString()), System.Globalization.NumberStyles.HexNumber).ToString());
            bCodeData.Add($"\"{analogIOV.ObjectName}\"");
            bCodeData.Add($"\"{analogIOV.Description}\"");
        }
    }
    public class AnalogValueBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var analogOutputs = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("2") && t.IsEnable)
                                .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
            bCodeData.Add("8516");
            bCodeData.Add(analogOutputs.Count.ToString());

            foreach (var analogIOV in analogOutputs)
            {
                AddAnalogValueData(bCodeData, analogIOV);
            }
        }

        private void AddAnalogValueData(List<string> bCodeData, AnalogIOV analogIOV)
        {
            bCodeData.Add(analogIOV.InstanceNumber);
            bCodeData.Add(analogIOV.EventDetectionEnable.ToString());
            if (analogIOV.ObjectType == null) analogIOV.ObjectType = " ";
            bCodeData.Add(analogIOV.ObjectType.Split(':')[0].ToString());
            if (analogIOV.Units == null) analogIOV.Units = "95: no-units";
            bCodeData.Add(analogIOV.Units.Split(':')[0].ToString());
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.MinPresValue.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.MaxPresValue.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.COVIncrement?.ToString() == null ? "0.00" : analogIOV.COVIncrement.ToString()));
            bCodeData.Add(analogIOV.TimeDelay.ToString());
            bCodeData.Add(analogIOV.TimeDelayNormal.ToString());
            bCodeData.Add(analogIOV.NotificationClass.ToString());
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.HighLimit.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.LowLimit.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.Deadband.ToString()));
            bCodeData.Add(analogIOV.LimitEnable.ToString());
            bCodeData.Add(analogIOV.EventEnable.ToString());
            bCodeData.Add(analogIOV.NotifyType.ToString());
            bCodeData.Add(uint.Parse(XMPS.Instance.GetHexAddress(analogIOV.LogicalAddress.ToString()), System.Globalization.NumberStyles.HexNumber).ToString());
            // bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV.RelinquishDefault.ToString()));
            bCodeData.Add(XMProBaseValidator.ConvertToRealValue(analogIOV?.RelinquishDefault?.ToString() ?? "0"));
            bCodeData.Add($"\"{analogIOV.ObjectName}\"");
            bCodeData.Add($"\"{analogIOV.Description}\"");
        }
    }
    public class BinaryInputBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var binaryInputs = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("3") && t.IsEnable)
                                        .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
            bCodeData.Add("8517");
            bCodeData.Add(binaryInputs.Count.ToString());
            foreach (var binaryIOV in binaryInputs)
            {
                AddBinaryInputData(bCodeData, binaryIOV);
            }
        }

        private void AddBinaryInputData(List<string> bCodeData, BinaryIOV binaryIOV)
        {
            bCodeData.Add(binaryIOV.InstanceNumber);
            bCodeData.Add(binaryIOV.EventDetectionEnable.ToString());
            if (binaryIOV.ObjectType == null) binaryIOV.ObjectType = " ";
            bCodeData.Add(binaryIOV.ObjectType.Split(':')[0].ToString());
            bCodeData.Add(binaryIOV.Polarity.ToString());
            bCodeData.Add(binaryIOV.TimeDelay.ToString());
            bCodeData.Add(binaryIOV.TimeDelayNormal.ToString());
            bCodeData.Add(binaryIOV.FeedbackValue.ToString());
            bCodeData.Add(binaryIOV.NotificationClass.ToString());
            bCodeData.Add(binaryIOV.EventEnable.ToString());
            bCodeData.Add(binaryIOV.NotifyType.ToString());
            bCodeData.Add(uint.Parse(XMPS.Instance.GetHexAddress(binaryIOV.LogicalAddress.ToString()), System.Globalization.NumberStyles.HexNumber).ToString());
            bCodeData.Add($"\"{binaryIOV.InactiveText ?? "false"}\"");
            bCodeData.Add($"\"{binaryIOV.ActiveText ?? "true"}\"");
            bCodeData.Add($"\"{binaryIOV.ObjectName}\"");
            bCodeData.Add($"\"{binaryIOV.Description}\"");
        }
    }
    public class BinaryOutputBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var binaryInputs = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("4") && t.IsEnable)
                               .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
            bCodeData.Add("8518");
            bCodeData.Add(binaryInputs.Count.ToString());
            foreach (var binaryIOV in binaryInputs)
            {
                AddBinaryOutputData(bCodeData, binaryIOV);
            }
        }

        private void AddBinaryOutputData(List<string> bCodeData, BinaryIOV binaryIOV)
        {
            bCodeData.Add(binaryIOV.InstanceNumber);
            bCodeData.Add(binaryIOV.EventDetectionEnable.ToString());
            if (binaryIOV.ObjectType == null) binaryIOV.ObjectType = " ";
            bCodeData.Add(binaryIOV.ObjectType.Split(':')[0].ToString());
            bCodeData.Add(binaryIOV.RelinquishDefault.ToString());
            bCodeData.Add(binaryIOV.Polarity.ToString());
            bCodeData.Add(binaryIOV.TimeDelay.ToString());
            bCodeData.Add(binaryIOV.TimeDelayNormal.ToString());
            if ((binaryIOV.FeedbackValue == 1 || binaryIOV.FeedbackValue == 0) && (binaryIOV.TagValue == "" || binaryIOV.TagValue == null))
            {
                bCodeData.Add(binaryIOV.FeedbackValue.ToString());
            }
            else
            {
                bCodeData.Add(binaryIOV.TagValue == "" ? "0" : uint.Parse(XMPS.Instance.GetHexAddress(binaryIOV.TagValue.ToString()), System.Globalization.NumberStyles.HexNumber).ToString());
            }
            bCodeData.Add(binaryIOV.NotificationClass.ToString());
            bCodeData.Add(binaryIOV.EventEnable.ToString());
            bCodeData.Add(binaryIOV.NotifyType.ToString());
            bCodeData.Add(uint.Parse(XMPS.Instance.GetHexAddress(binaryIOV.LogicalAddress.ToString()), System.Globalization.NumberStyles.HexNumber).ToString());
            bCodeData.Add($"\"{binaryIOV.InactiveText ?? "false"}\"");
            bCodeData.Add($"\"{binaryIOV.ActiveText ?? "true"}\"");
            bCodeData.Add($"\"{binaryIOV.ObjectName}\"");
            bCodeData.Add($"\"{binaryIOV.Description}\"");
        }
    }
    public class BinaryValueBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var binaryInputs = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("5") && t.IsEnable)
                               .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList(); ;
            bCodeData.Add("8519");
            bCodeData.Add(binaryInputs.Count.ToString());
            foreach (var binaryIOV in binaryInputs)
            {
                AddBinaryValueData(bCodeData, binaryIOV);
            }
        }

        private void AddBinaryValueData(List<string> bCodeData, BinaryIOV binaryIOV)
        {
            bCodeData.Add(binaryIOV.InstanceNumber);
            bCodeData.Add(binaryIOV.EventDetectionEnable.ToString());
            if (binaryIOV.ObjectType == null) binaryIOV.ObjectType = " ";
            bCodeData.Add(binaryIOV.ObjectType.Split(':')[0].ToString());
            bCodeData.Add(binaryIOV.RelinquishDefault.ToString());
            bCodeData.Add(binaryIOV.TimeDelay.ToString());
            bCodeData.Add(binaryIOV.TimeDelayNormal.ToString());
            bCodeData.Add(binaryIOV.FeedbackValue.ToString());
            bCodeData.Add(binaryIOV.NotificationClass.ToString());
            bCodeData.Add(binaryIOV.EventEnable.ToString());
            bCodeData.Add(binaryIOV.NotifyType.ToString());
            bCodeData.Add(uint.Parse(XMPS.Instance.GetHexAddress(binaryIOV.LogicalAddress.ToString()), System.Globalization.NumberStyles.HexNumber).ToString());
            bCodeData.Add($"\"{binaryIOV.InactiveText ?? "false"}\"");
            bCodeData.Add($"\"{binaryIOV.ActiveText ?? "true"}\"");
            bCodeData.Add($"\"{binaryIOV.ObjectName}\"");
            bCodeData.Add($"\"{binaryIOV.Description}\"");
        }

    }
    public class MultiStateValuesBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var multiStateValues = bacNetIP.MultistateValues.Where(t => t.ObjectType.Split(':')[0].Equals("19") && t.IsEnable)
                                               .OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
            bCodeData.Add("8521");
            bCodeData.Add(multiStateValues.Count.ToString());
            foreach (var multiStateValue in multiStateValues)
            {
                MultiStateValuesData(bCodeData, multiStateValue);
            }
        }

        private void MultiStateValuesData(List<string> bCodeData, MultistateIOV multiStateValue)
        {
            bCodeData.Add(multiStateValue.InstanceNumber);
            if (multiStateValue.ObjectType == null) multiStateValue.ObjectType = " ";
            bCodeData.Add(multiStateValue.ObjectType.Split(':')[0].ToString());
            bCodeData.Add(multiStateValue.NumberOfStates.ToString());
            bCodeData.Add(multiStateValue.EventDetectionEnable.ToString());
            bCodeData.Add(multiStateValue.TimeDelay.ToString());
            bCodeData.Add(multiStateValue.TimeDelayNormal.ToString());
            int totalAlarmValues = multiStateValue.AlarmValues?.Count ?? 1;
            bCodeData.Add(totalAlarmValues.ToString());
            if (multiStateValue.AlarmValues != null && multiStateValue.AlarmValues.Count > 0)
            {
                foreach (var alarmValue in multiStateValue.AlarmValues)
                {
                    bCodeData.Add(alarmValue.ToString());
                }
            }
            else
            {
                bCodeData.Add(multiStateValue.AlarmValue.ToString());
            }
            bCodeData.Add(multiStateValue.NotificationClass.ToString());
            bCodeData.Add(multiStateValue.EventEnable.ToString());
            bCodeData.Add(multiStateValue.NotifyType.ToString());
            bCodeData.Add(uint.Parse(XMPS.Instance.GetHexAddress(multiStateValue.LogicalAddress.ToString()), System.Globalization.NumberStyles.HexNumber).ToString());
            if (multiStateValue.States != null)
            {
                foreach (State state in multiStateValue.States.OrderBy(t => t.StateNumber))
                {
                    bCodeData.Add($"\"{state.StateValue}\"");
                }
            }
            bCodeData.Add(Convert.ToString(multiStateValue.RelinquishDefault + 1));
            bCodeData.Add($"\"{multiStateValue.ObjectName}\"");
            bCodeData.Add($"\"{multiStateValue.Description}\"");
        }
    }
    public class CalendarObjectBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var calendars = bacNetIP.Calendars.Where(t => t.IsEnable).OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
            bCodeData.Add("8520");
            bCodeData.Add(calendars.Count.ToString());
            foreach (var schedule in calendars)
            {
                CalendarObjectData(bCodeData, schedule);
            }
        }

        private void CalendarObjectData(List<string> bCodeData, Calendar calendar)
        {
            bCodeData.Add(calendar.InstanceNumber);
            bCodeData.Add(calendar.ObjectType.Split(':')[0].ToString());
            if (calendar.Events != null && calendar.Events.Count > 0)
            {
                bCodeData.Add(calendar.Events.Count().ToString());
                //add special events data
                AddSpecialEvents(bCodeData, calendar);
            }
            else
            {
                //adding "0" if no special event added on calendar object.
                bCodeData.Add("0");
            }
            bCodeData.Add($"\"{calendar.ObjectName}\"");
            bCodeData.Add($"\"{calendar.Description}\"");
        }

        private void AddSpecialEvents(List<string> bCodeData, Calendar calendar)
        {
            foreach (var specialEvent in calendar.Events.OrderBy(t => t.EventId))
            {
                bCodeData.Add(GetSpecialEventType(specialEvent.Type));

                switch (specialEvent.Type)
                {
                    case "Date":
                        SpecialEventHelper.AddDateEvent(bCodeData, (DateEvent)specialEvent, true);
                        break;
                    case "Date Range":
                        SpecialEventHelper.AddDateRangeEvent(bCodeData, (DateRangeEvent)specialEvent, true);
                        break;
                    case "Week And Day":
                        SpecialEventHelper.AddWeekAndDayEvent(bCodeData, (WeekAndDayEvent)specialEvent, true);
                        break;
                }
            }

        }
        private string GetSpecialEventType(string type)
        {
            switch (type)
            {
                case "Date":
                    return "0";
                case "Date Range":
                    return "1";
                case "Week And Day":
                    return "2";
                default:
                    return "4";
            }
        }
    }
    public class ScheduleObjectBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var schedules = bacNetIP.Schedules.Where(t => t.IsEnable).OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
            bCodeData.Add("8523");
            bCodeData.Add(schedules.Count.ToString());
            foreach (var schedule in schedules)
            {
                SchedulesObjectData(bCodeData, schedule);
            }
        }

        private void SchedulesObjectData(List<string> bCodeData, Schedule schedule)
        {
            bCodeData.Add(schedule.InstanceNumber);
            bCodeData.Add(schedule.ObjectType.Split(':')[0].ToString());
            bCodeData.Add(schedule.ScheduleValue.ToString());

            if (schedule.StartDate != null && schedule.EndDate != null)
            {
                AddEffectivePeriod(bCodeData, schedule.StartDate, schedule.AnyCheck);
                AddEffectivePeriod(bCodeData, schedule.EndDate, schedule.AnyCheck);
            }
            //add weekly 
            if (schedule.EventParticularDays != null && schedule.EventParticularDays.Count > 0)
                AddWeeklySelection(bCodeData, schedule);
            else
            {
                bCodeData.Add("b0");
                bCodeData.Add("0");
            }
            //add special events
            if (schedule.specialEvents != null && schedule.specialEvents.Count > 0)
                AddSpecialEvents(bCodeData, schedule);
            else
            {
                bCodeData.Add("b1");
                bCodeData.Add("0");
            }
            bCodeData.Add(uint.Parse(XMPS.Instance.GetHexAddress(schedule.LogicalAddress.ToString()), System.Globalization.NumberStyles.HexNumber).ToString());
            bCodeData.Add(schedule.Nullvalue ? "1" : "0");
            if (schedule.Value == " 1" || schedule.Value == " 0")
            {
                bCodeData.Add(string.IsNullOrWhiteSpace(schedule.Value) ? "0" : schedule.Value.Trim());
            }
            else
            {
                string value = string.IsNullOrWhiteSpace(schedule.Value?.ToString()) ? "0" : schedule.Value.ToString();
                bCodeData.Add(XMProBaseValidator.ConvertToRealValue(value));
            }
            bCodeData.Add($"\"{schedule.ObjectName}\"");
            bCodeData.Add($"\"{schedule.Description}\"");
        }
        private void AddWeeklySelection(List<string> bCodeData, Schedule schedule)
        {
            if (schedule.EventParticularDays == null || schedule.EventParticularDays.Count == 0)
                return;

            bCodeData.Add("b0");
            bCodeData.Add(schedule.NoOfDaySelected.ToString());

            foreach (var eventPerDay in schedule.EventParticularDays)
            {
                bCodeData.Add(eventPerDay.NoOfEventParticularDay.ToString());

                foreach (var eventTimeValues in eventPerDay.EventValues)
                {
                    SpecialEventHelper.AddEventTimeValues(bCodeData, eventTimeValues, schedule.ScheduleValue.ToString());
                }
            }
        }
        private void AddEffectivePeriod(List<string> bCodeData, EffectivePeriod period, bool isAnyChecked)
        {
            if (isAnyChecked)
            {
                bCodeData.Add("255");
                bCodeData.Add("255");
                bCodeData.Add("255");
                bCodeData.Add("255");
            }
            else
            {
                bCodeData.Add(period.Date.Year.ToString());
                bCodeData.Add(period.Date.Month.ToString());
                bCodeData.Add(period.Date.Day.ToString());
                bCodeData.Add(SpecialEventHelper.GetDayOfWeekNumber(period.Date.DayOfWeek));
            }
        }
        private void AddSpecialEvents(List<string> bCodeData, Schedule schedule)
        {
            bCodeData.Add("b1");
            bCodeData.Add(schedule.specialEvents.Count.ToString());

            foreach (var specialEvent in schedule.specialEvents.OrderBy(t => t.EventId))
            {
                bCodeData.Add(GetSpecialEventType(specialEvent.Type));

                switch (specialEvent.Type)
                {
                    case "Date":
                        SpecialEventHelper.AddDateEvent(bCodeData, (DateEvent)specialEvent);
                        break;
                    case "Date Range":
                        SpecialEventHelper.AddDateRangeEvent(bCodeData, (DateRangeEvent)specialEvent);
                        break;
                    case "Week And Day":
                        SpecialEventHelper.AddWeekAndDayEvent(bCodeData, (WeekAndDayEvent)specialEvent);
                        break;
                    case "Calendar Reference":
                        SpecialEventHelper.AddCalendarReferenceEvent(bCodeData, (CalendarReference)specialEvent);
                        break;
                }
            }
        }
        private string GetSpecialEventType(string type)
        {
            switch (type)
            {
                case "Date":
                    return "0";
                case "Date Range":
                    return "1";
                case "Week And Day":
                    return "2";
                case "Calendar Reference":
                    return "4";
                default:
                    return "";
            }
        }
    }
    public class NetworkPortBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var networkPort = bacNetIP.NetworkPort;
            if (networkPort != null && networkPort.IsEnable)
            {
                bCodeData.Add("8524");
                bCodeData.Add(string.IsNullOrWhiteSpace(networkPort.NetworkNumber) ? "1" : networkPort.NetworkNumber);
                int modeValue = 0;
                if (!string.IsNullOrWhiteSpace(networkPort.BacnetIPMode))
                {
                    if (int.TryParse(networkPort.BacnetIPMode, out int parsedMode))
                    {
                        if (parsedMode == 0)
                            modeValue = 1;
                        else if (parsedMode == -1)
                            modeValue = 0;
                        else
                            modeValue = 1;
                    }
                }
                bCodeData.Add(modeValue.ToString());
                bCodeData.AddRange((string.IsNullOrWhiteSpace(networkPort.IPAddress) ? "192.168.15.60" : networkPort.IPAddress).ToString().Split('.'));
                bCodeData.Add(string.IsNullOrWhiteSpace(networkPort.BacnetIPUDPPort) ? "47808" : networkPort.BacnetIPUDPPort);
                bCodeData.AddRange((string.IsNullOrWhiteSpace(networkPort.IPSubnetMask) ? "0.0.0.0" : networkPort.IPSubnetMask).ToString().Split('.'));
                bCodeData.AddRange((string.IsNullOrWhiteSpace(networkPort.IPDefaultGateway) ? "0.0.0.0" : networkPort.IPDefaultGateway).ToString().Split('.'));
                bCodeData.AddRange((string.IsNullOrWhiteSpace(networkPort.IPDNSServer) ? "8.8.8.8" : networkPort.IPDNSServer).ToString().Split('.'));
                bCodeData.Add(networkPort.IPDHCPEnable == "1" ? "1" : "0");
                bCodeData.AddRange((string.IsNullOrWhiteSpace(networkPort.FDBBMDIP) ? "0.0.0.0" : networkPort.FDBBMDIP).ToString().Split('.'));
                bCodeData.Add(string.IsNullOrWhiteSpace(networkPort.FDBBMDPort) ? "47808" : networkPort.FDBBMDPort);
                bCodeData.Add(string.IsNullOrWhiteSpace(networkPort.FDSubscriptionLifetime) ? "5000" : networkPort.FDSubscriptionLifetime);
                //bCodeData.Add(string.IsNullOrWhiteSpace(networkPort.ObjectIdentifier) ? "0" : networkPort.ObjectIdentifier.Split(':')[1]);
                string objectType = "56";
                if (!string.IsNullOrWhiteSpace(networkPort.ObjectType))
                {
                    if (networkPort.ObjectType.Contains(":"))
                    {
                        objectType = networkPort.ObjectType.Split(':')[0].Trim();
                    }
                    else
                    {
                        objectType = networkPort.ObjectType;
                    }
                }
                bCodeData.Add(objectType);
                bCodeData.Add($"\"{(string.IsNullOrWhiteSpace(networkPort.ObjectName) ? "" : networkPort.ObjectName)}\"");
                bCodeData.Add($"\"{(string.IsNullOrWhiteSpace(networkPort.Description) ? "" : networkPort.Description)}\"");

            }
        }
    }
    public class ModbusRTUMultiplicationBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var modbusRTUMaster = (MODBUSRTUMaster)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
            bCodeData.Add("8525");
            if (modbusRTUMaster == null || !modbusRTUMaster.Slaves.Any())
            {
                bCodeData.Add("0");
                return;
            }
            var slavesWithFactors = modbusRTUMaster.Slaves.OrderBy(s => s.Name).ToList();
            bCodeData.Add(slavesWithFactors.Count.ToString());
            foreach (var slave in slavesWithFactors)
            {
                string factorCode = GetMultiplicationFactorCode(slave.MultiplicationFactor, slave.FunctionCode);
                bCodeData.Add(factorCode);
            }
        }

        private string GetMultiplicationFactorCode(string multiplicationFactor, string functionCode)
        {
            if (!IsMultiplicationFactorSupported(functionCode))
            {
                return "1";
            }
            if (string.IsNullOrWhiteSpace(multiplicationFactor))
            {
                return "1";
            }

            string cleanFactor = multiplicationFactor.Trim();

            if (cleanFactor == "1") return "1";
            else if (cleanFactor == "0.1") return "2";
            else if (cleanFactor == "0.01") return "3";
            else if (cleanFactor == "0.001") return "4";
            else if (cleanFactor == "0.0001") return "5";
            else if (cleanFactor == "0.00001") return "6";
            else if (cleanFactor == "10") return "7";
            else if (cleanFactor == "100") return "8";
            else if (cleanFactor == "1000") return "9";
            else if (cleanFactor == "10000") return "10";
            else return "1";
        }

        private bool IsMultiplicationFactorSupported(string functionCode)
        {
            if (string.IsNullOrWhiteSpace(functionCode))
                return false;
            string numericCode = ExtractNumericFunctionCode(functionCode);
            string[] supportedFunctionCodes = { "03", "04", "06", "10" };
            return supportedFunctionCodes.Contains(numericCode);
        }

        private string ExtractNumericFunctionCode(string functionCode)
        {
            if (string.IsNullOrWhiteSpace(functionCode))
                return functionCode;

            int startIndex = functionCode.IndexOf('(');
            int endIndex = functionCode.IndexOf(')');
            if (startIndex >= 0 && endIndex > startIndex)
            {
                string codeInParentheses = functionCode.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
                if (codeInParentheses == "16")
                    return "10";
                if (codeInParentheses.Length == 1)
                    return "0" + codeInParentheses;
                else if (codeInParentheses.Length == 2)
                    return codeInParentheses;
            }
            string digits = new string(functionCode.Where(char.IsDigit).ToArray());
            if (!string.IsNullOrEmpty(digits))
            {
                if (digits == "16")
                    return "10";
                return digits.Length == 1 ? "0" + digits : digits;
            }

            return functionCode;
        }
    }
    public class ModbusTCPClientMultiplicationBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var modbusTCPClient = (MODBUSTCPClient)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
            bCodeData.Add("8526");

            if (modbusTCPClient == null || !modbusTCPClient.Slaves.Any())
            {
                bCodeData.Add("0");
                return;
            }
            var slavesWithFactors = modbusTCPClient.Slaves.OrderBy(s => s.Name).ToList();
            bCodeData.Add(slavesWithFactors.Count.ToString());
            foreach (var slave in slavesWithFactors)
            {
                string factorCode = GetMultiplicationFactorCode(slave.MultiplicationFactor, slave.Functioncode);
                bCodeData.Add(factorCode);
            }
        }

        private string GetMultiplicationFactorCode(string multiplicationFactor, string functionCode)
        {
            if (!IsMultiplicationFactorSupported(functionCode))
            {
                return "1";
            }
            if (string.IsNullOrWhiteSpace(multiplicationFactor))
            {
                return "1";
            }
            string cleanFactor = multiplicationFactor.Trim();
            if (cleanFactor == "1") return "1";
            else if (cleanFactor == "0.1") return "2";
            else if (cleanFactor == "0.01") return "3";
            else if (cleanFactor == "0.001") return "4";
            else if (cleanFactor == "0.0001") return "5";
            else if (cleanFactor == "0.00001") return "6";
            else if (cleanFactor == "10") return "7";
            else if (cleanFactor == "100") return "8";
            else if (cleanFactor == "1000") return "9";
            else if (cleanFactor == "10000") return "10";
            else return "1";
        }

        private bool IsMultiplicationFactorSupported(string functionCode)
        {
            if (string.IsNullOrWhiteSpace(functionCode))
                return false;
            string numericCode = ExtractNumericFunctionCode(functionCode);
            string[] supportedFunctionCodes = { "03", "04", "06", "10" };
            return supportedFunctionCodes.Contains(numericCode);
        }

        private string ExtractNumericFunctionCode(string functionCode)
        {
            if (string.IsNullOrWhiteSpace(functionCode))
                return functionCode;
            int startIndex = functionCode.IndexOf('(');
            int endIndex = functionCode.IndexOf(')');
            if (startIndex >= 0 && endIndex > startIndex)
            {
                string codeInParentheses = functionCode.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
                if (codeInParentheses == "16")
                    return "10";
                if (codeInParentheses.Length == 1)
                    return "0" + codeInParentheses;
                else if (codeInParentheses.Length == 2)
                    return codeInParentheses;
            }
            string digits = new string(functionCode.Where(char.IsDigit).ToArray());
            if (!string.IsNullOrEmpty(digits))
            {
                if (digits == "16")
                    return "10";
                return digits.Length == 1 ? "0" + digits : digits;
            }
            return functionCode;
        }
    }
    public class SpecialEventHelper
    {
        public static void AddDateEvent(List<string> bCodeData, DateEvent dateEvent, bool isCalendar = false)
        {
            bCodeData.Add(dateEvent.isYearAny ? "255" : dateEvent.StartDate.Year.ToString());
            bCodeData.Add(dateEvent.isMonthAny ? "255" : dateEvent.StartDate.Month.ToString());
            bCodeData.Add(dateEvent.isDayAny ? "255" : dateEvent.StartDate.Day.ToString());
            bCodeData.Add(dateEvent.isWeekDayAny ? "255" : GetDayOfWeekNumber(dateEvent.StartDate.DayOfWeek));
            bCodeData.Add(dateEvent.Priority.ToString());
            if (!isCalendar)
            {
                bCodeData.Add(dateEvent.EventValues.Count.ToString());

                foreach (var eventValue in dateEvent.EventValues)
                {
                    AddEventTimeValues(bCodeData, eventValue, null);
                }
            }

        }
        public static void AddDateRangeEvent(List<string> bCodeData, DateRangeEvent dateRangeEvent, bool isCalendar = false)
        {
            //start date
            if (dateRangeEvent.isStartDateAny)
            {
                bCodeData.Add("255");
                bCodeData.Add("255");
                bCodeData.Add("255");
                bCodeData.Add("255");
            }
            else
            {
                bCodeData.Add(dateRangeEvent.StartDate.Year.ToString());
                bCodeData.Add(dateRangeEvent.StartDate.Month.ToString());
                bCodeData.Add(dateRangeEvent.StartDate.Day.ToString());
                bCodeData.Add(GetDayOfWeekNumber(dateRangeEvent.StartDate.DayOfWeek));
            }
            //end Date
            if (dateRangeEvent.isEndDateAny)
            {
                bCodeData.Add("255");
                bCodeData.Add("255");
                bCodeData.Add("255");
                bCodeData.Add("255");
            }
            else
            {
                bCodeData.Add(dateRangeEvent.EndDate.Year.ToString());
                bCodeData.Add(dateRangeEvent.EndDate.Month.ToString());
                bCodeData.Add(dateRangeEvent.EndDate.Day.ToString());
                bCodeData.Add(GetDayOfWeekNumber(dateRangeEvent.EndDate.DayOfWeek));
            }
            //priority
            bCodeData.Add(dateRangeEvent.Priority.ToString());
            if (!isCalendar)
            {
                bCodeData.Add(dateRangeEvent.EventValues.Count.ToString());

                foreach (var eventValue in dateRangeEvent.EventValues)
                {
                    AddEventTimeValues(bCodeData, eventValue, null);
                }
            }
        }
        public static void AddWeekAndDayEvent(List<string> bCodeData, WeekAndDayEvent weekAndDayEvent, bool isCalendar = false)
        {
            bCodeData.Add(weekAndDayEvent.MonthValue.ToString());
            bCodeData.Add(weekAndDayEvent.WeekValue.ToString());
            bCodeData.Add(weekAndDayEvent.DayValue.ToString());
            bCodeData.Add(weekAndDayEvent.Priority.ToString());
            if (!isCalendar)
            {
                bCodeData.Add(weekAndDayEvent.EventValues.Count.ToString());
                foreach (var eventValue in weekAndDayEvent.EventValues)
                {
                    AddEventTimeValues(bCodeData, eventValue, null);
                }
            }
        }
        public static void AddCalendarReferenceEvent(List<string> bCodeData, CalendarReference calendarReference)
        {
            bCodeData.Add(calendarReference.CalendarInstaceNumber.ToString());
            bCodeData.Add(calendarReference.Priority.ToString());
            bCodeData.Add(calendarReference.EventValues.Count.ToString());
            foreach (var eventValue in calendarReference.EventValues)
            {
                AddEventTimeValues(bCodeData, eventValue, null);
            }
        }
        public static void AddEventTimeValues(List<string> bCodeData, EventValue eventTimeValues, string Scheduletype)
        {
            var startTime = TimeSpan.ParseExact(eventTimeValues.StartTime, @"hh\:mm\:ss", null);
            bCodeData.Add(startTime.Hours.ToString());
            bCodeData.Add(startTime.Minutes.ToString());
            bCodeData.Add(startTime.Seconds.ToString());

            var endTime = TimeSpan.ParseExact(eventTimeValues.EndTime, @"hh\:mm\:ss", null);
            bCodeData.Add(endTime.Hours.ToString());
            bCodeData.Add(endTime.Minutes.ToString());
            bCodeData.Add(endTime.Seconds.ToString());

            if ((eventTimeValues.Value == 1 || eventTimeValues.Value == 0) && Scheduletype == "0")
            {
                bCodeData.Add(eventTimeValues.Value.ToString());
            }
            else
            {
                bCodeData.Add(XMProBaseValidator.ConvertToRealValue(eventTimeValues.Value.ToString()));
            }
        }
        public static string GetDayOfWeekNumber(DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Sunday ? "7" : ((int)dayOfWeek).ToString();
        }
    }
    public class NotificationsBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var notificationValues = bacNetIP.Notifications.Where(t => t.IsEnable).OrderBy(t => Convert.ToInt32(t.InstanceNumber)).ToList();
            bCodeData.Add("8522");
            bCodeData.Add(notificationValues.Count.ToString());
            foreach (var notification in notificationValues)
            {
                NotificationsData(bCodeData, notification);
            }
        }

        private void NotificationsData(List<string> bCodeData, Notification notificationData)
        {
            bCodeData.Add(notificationData.InstanceNumber);
            if (notificationData.ObjectType == null) notificationData.ObjectType = " ";
            bCodeData.Add(notificationData.ObjectType.Split(':')[0].ToString());
            bCodeData.Add(notificationData.OffNormalPriority.ToString());
            bCodeData.Add(notificationData.FaultPriority.ToString());
            bCodeData.Add(notificationData.NormalPriority.ToString());
            bCodeData.Add(notificationData.AckRequired.ToString());
            bCodeData.Add(notificationData.Recipients.Count().ToString());
            foreach (Recipient recipient in notificationData.Recipients)
            {
                bCodeData.Add(CalcWeekDays((recipient.DaysofWeek.ToString()).ToString()));
                bCodeData.Add(recipient.StartTime.Split(':')[0].ToString());
                bCodeData.Add(recipient.StartTime.Split(':')[1].ToString());
                bCodeData.Add(recipient.StartTime.Split(':')[2].ToString());
                bCodeData.Add(recipient.EndTime.Split(':')[0].ToString());
                bCodeData.Add(recipient.EndTime.Split(':')[1].ToString());
                bCodeData.Add(recipient.EndTime.Split(':')[2].ToString());
                bCodeData.Add(recipient.ProcessIdentifier.ToString());
                bCodeData.Add(recipient.Notification.ToString());
                bCodeData.Add(recipient.RecipientType.ToString() == "Device" ? "0" : "-");
                bCodeData.Add(recipient.DeviceInstance.ToString());
            }
            bCodeData.Add($"\"{notificationData.ObjectName}\"");
            bCodeData.Add($"\"{notificationData.Description}\"");

        }

        private string CalcWeekDays(string DaysSting)
        {
            string[] actweekdays = DaysSting.Split(',');
            string binaryOP = "";
            binaryOP = binaryOP + (DaysSting.Contains("Mon") ? "1" : "0");
            binaryOP = binaryOP + (DaysSting.Contains("Tue") ? "1" : "0");
            binaryOP = binaryOP + (DaysSting.Contains("Wed") ? "1" : "0");
            binaryOP = binaryOP + (DaysSting.Contains("Thu") ? "1" : "0");
            binaryOP = binaryOP + (DaysSting.Contains("Fri") ? "1" : "0");
            binaryOP = binaryOP + (DaysSting.Contains("Sat") ? "1" : "0");
            binaryOP = binaryOP + (DaysSting.Contains("Sun") ? "1" : "0");
            return Convert.ToInt32(binaryOP, 2).ToString();
        }
    }

    public class ResistanceTableBcode : IBcodeFileGeneration
    {
        public void CalculateBcode(BacNetIP bacNetIP, List<string> bCodeData)
        {
            var allTables = XMPS.Instance.LoadedProject.ResistanceTables;
            var allEntries = XMPS.Instance.LoadedProject.ResistanceValues.Where(r => !r.IsDeletedRequest).ToList();
            var resistanceTables = allTables .Select(t => new {
                TableName = t.Name,
                Entries = allEntries.Where(e => e.Name == t.Name).Select(e => new {
                    Resistance = e.Resistance,
                    Output = e.output
                })
                .ToList()
            })
                 .ToList();
            int tablecount = XMPS.Instance.LoadedProject.ResistanceTables.Count();
            bCodeData.Add("8527");
            bCodeData.Add(tablecount.ToString());
            foreach (var table in resistanceTables)
            {
                AddResistanceTableData(bCodeData, table);
            }
        }

        int tableNumber = 1;

        private void AddResistanceTableData(List<string> bCodeData, dynamic table)
        {
            bCodeData.Add(tableNumber.ToString());
            int maxEntries = 20;
            if (table.Entries != null && table.Entries.Count > 0)
            {
                int count = 0;
                foreach (var entry in table.Entries)
                {
                    bCodeData.Add(XMProBaseValidator.ConvertToRealValue(entry.Resistance.ToString()));
                    bCodeData.Add(XMProBaseValidator.ConvertToRealValue(entry.Output.ToString()));
                    count++;
                    if (count >= maxEntries)
                        break;
                }
                int remaining = maxEntries - count;
                for (int i = 0; i < remaining; i++)
                {
                    bCodeData.Add("0");
                    bCodeData.Add("0");
                }
            }
            else
            {
                for (int i = 0; i < maxEntries; i++)
                {
                    bCodeData.Add("0");
                    bCodeData.Add("0");
                }
            }
            tableNumber++;
        }
    }
    internal class BcodeBuilder
    {
        private readonly List<IBcodeFileGeneration> _bcodeObjects = new List<IBcodeFileGeneration>();
        private readonly List<string> _bCodeData = new List<string>();
        private readonly BacNetIP _bacNetIP;

        public BcodeBuilder(BacNetIP bacNetIP)
        {
            _bacNetIP = bacNetIP;
        }

        public BcodeBuilder AddObjects(IBcodeFileGeneration bcodeObject)
        {
            _bcodeObjects.Add(bcodeObject);
            return this;
        }

        public List<string> Build()
        {
            //SOF
            _bCodeData.Add("36");
            Ethernet ethernet = (Ethernet)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
            _bCodeData.Add(ethernet.NetworkNo.ToString());
            _bCodeData.Add(ethernet.Port.ToString());
            string totalObjectCount = (_bacNetIP.BinaryIOValues.Where(t => t.IsEnable).Count() +
                                       _bacNetIP.AnalogIOValues.Where(t => t.IsEnable).Count() +
                                       _bacNetIP.MultistateValues.Where(t => t.IsEnable).Count() +
                                       _bacNetIP.Calendars.Where(t => t.IsEnable).Count() +
                                       _bacNetIP.Schedules.Where(t => t.IsEnable).Count() +
                                       _bacNetIP.Notifications.Where(t => t.IsEnable).Count() +
                                       (_bacNetIP.Device.IsEnable ? 1 : 0) +
                                       (_bacNetIP.NetworkPort.IsEnable ? 1 : 0)).ToString();
            _bCodeData.Add(totalObjectCount);
            foreach (var bcodeObject in _bcodeObjects)
            {
                bcodeObject.CalculateBcode(_bacNetIP, _bCodeData);
            }

            //EOF
            _bCodeData.Add("35");
            return _bCodeData;
        }
        private int GetModbusSlaveCount()
        {
            var modbusRTUMaster = (MODBUSRTUMaster)XMPS.Instance.LoadedProject.Devices
                .Where(d => d.GetType().Name == "MODBUSRTUMaster")
                .FirstOrDefault();

            return modbusRTUMaster?.Slaves.Count ?? 0; // MODBUSRTUMaster_Slave doesn't have IsEnable
        }
        private int GetModbusTCPClientSlaveCount()
        {
            var modbusTCPClient = (MODBUSTCPClient)XMPS.Instance.LoadedProject.Devices
                .Where(d => d.GetType().Name == "MODBUSTCPClient")
                .FirstOrDefault();

            return modbusTCPClient?.Slaves.Count ?? 0;
        }
    }
    internal class BcodeGeneration
    {
        public void CalculateBcodeFile()
        {
            BacNetIP bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            var bcodeBuilder = new BcodeBuilder(bacNetIP);
            bcodeBuilder.AddObjects(new DeviceBcode());
            bcodeBuilder.AddObjects(new AnalogInputBcode());
            bcodeBuilder.AddObjects(new AnalogOutputBcode());
            bcodeBuilder.AddObjects(new AnalogValueBcode());
            bcodeBuilder.AddObjects(new BinaryInputBcode());
            bcodeBuilder.AddObjects(new BinaryOutputBcode());
            bcodeBuilder.AddObjects(new BinaryValueBcode());
            bcodeBuilder.AddObjects(new CalendarObjectBcode());
            bcodeBuilder.AddObjects(new MultiStateValuesBcode());
            bcodeBuilder.AddObjects(new NotificationsBcode());
            bcodeBuilder.AddObjects(new ScheduleObjectBcode());
            bcodeBuilder.AddObjects(new NetworkPortBcode());
            bcodeBuilder.AddObjects(new ModbusRTUMultiplicationBcode());
            bcodeBuilder.AddObjects(new ModbusTCPClientMultiplicationBcode());
            bcodeBuilder.AddObjects(new ResistanceTableBcode());
            List<string> bCodeData = bcodeBuilder.Build();
            string filepath = XMPS.Instance.CurrentProjectData.ProjectPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            string newfilepath = XMPS.Instance.CurrentProjectData.ProjectPath.Replace(filepath, "Bcode.txt");
            using (StreamWriter sw = new StreamWriter(newfilepath))
            {
                XMPS.Instance.LoadedProject.BCodeCRC = 0;
                foreach (string i in bCodeData)
                {
                    XMPS.Instance.LoadedProject.BCodeCRC ^= XMProBaseValidator.ConvertStringToLong(i);
                    sw.WriteLine(i);
                }
            }
        }

    }
}
