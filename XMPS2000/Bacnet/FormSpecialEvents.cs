using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;

namespace XMPS2000.Bacnet
{
    public partial class FormSpecialEvents : Form
    {
        private BacNetIP bacNetIP;
        private readonly string currentSpeicalEvent;
        private int SelectedTimeRow = -1;
        public SpecialEvent sepicalEvent;
        private List<SpecialEvent> specialsEvents;
        private int currentObjectEventId;
        private bool isEditMode = false;
        public DateTime StartDate;
        public DateTime EndDate;
        //save current ScheduleObject Type
        private string scheduletype;

        public FormSpecialEvents(string currentEvent, ref List<SpecialEvent> currentTypeEvents, string valueType, SpecialEvent currentObjectData = null)
        {
            InitializeComponent();        
            InitializeCustomComponents();
            bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            scheduletype = valueType;
            BindDesingAsPerEventType(currentEvent);
            specialsEvents = currentTypeEvents;
            if (currentObjectData != null)
            {
                AssingValues(currentObjectData);
                currentObjectEventId = currentObjectData.EventId;
                isEditMode = true;
            }
            currentSpeicalEvent = currentEvent;
        }

        private void AssingValues(SpecialEvent currentObjectData)
        {
            this.textBoxName.Text = currentObjectData.Name;
            this.comboBoxEventsType.Text = currentObjectData.Type;
            this.textBoxPriority.Text = currentObjectData.Priority.ToString();

            switch (currentObjectData.Type)
            {
                case "Date":
                    DateEvent date = (DateEvent)currentObjectData;
                    this.dateRangeSDWeekDay.Checked = date.isWeekDayAny;
                    this.dateRangeSDDay.Checked = date.isDayAny;
                    this.dateRangeSDMonth.Checked = date.isMonthAny;
                    this.dateRangeSDYear.Checked = date.isYearAny;
                    this.daTimePickerStartSelecDate.Value = date.StartDate;
                    break;
                case "Date Range":
                    DateRangeEvent dateRange = (DateRangeEvent)currentObjectData;
                    this.textBoxName.Text = dateRange.Name;
                    this.dateRangeStartDateAny.Checked = dateRange.isStartDateAny;
                    this.comboBoxEventsType.Text = dateRange.Type;
                    this.textBoxPriority.Text = dateRange.Priority.ToString();
                    this.daTimePickerStartSelecDate.Value = dateRange.StartDate;
                    this.daTimePickerEndSelecDate.Value = dateRange.EndDate;
                    this.dateRangeEndDateAny.Checked = dateRange.isEndDateAny;
                    break;
                case "Week And Day":
                    WeekAndDayEvent weekAndDay = (WeekAndDayEvent)currentObjectData;
                    this.textBoxName.Text = weekAndDay.Name;
                    this.comboBoxEventsType.Text = weekAndDay.Type;
                    this.textBoxPriority.Text = weekAndDay.Priority.ToString();
                    AssingWeekAndDayValues(weekAndDay.MonthValue, weekAndDay.WeekValue, weekAndDay.DayValue);
                    break;
                case "Calendar Reference":
                    CalendarReference calendarReference = (CalendarReference)currentObjectData;
                    this.textBoxCalendarInstaceNo.Text = calendarReference.CalendarInstaceNumber.ToString();
                    this.ComboBoxCalendarObjects.Text = calendarReference.CalendarObjectName;
                    break;
            }
            foreach (EventValue eventValue in currentObjectData.EventValues)
            {
                dataGridViewTimeSlots.Rows.Add(eventValue.StartTime, eventValue.EndTime, eventValue.Value);
            }
            SortTimeSlotsInAscendidng();
        }

        private void AssingWeekAndDayValues(int monthValue, int weekValue, int dayValue)
        {
            ComboBoxMonth.SelectedIndex = monthValue == 255 ? 0 : monthValue;
            ComboBoxWeek.SelectedIndex = weekValue == 255 ? 0 : weekValue;
            ComboBoxWeekDay.SelectedIndex = dayValue == 255 ? 0 : dayValue;
        }

        private void BindDesingAsPerEventType(string currevent)
        {
            this.comboBoxEventsType.Text = currevent;
            this.comboBoxEventsType.Enabled = false;
            //changing the value type
            if (scheduletype == "Numeric")
            {
                comboBoxValue.Visible = false;
                textBoxEventValue.Location = new System.Drawing.Point(71, 13);
                textBoxEventValue.Size = new System.Drawing.Size(121, 21);
                textBoxEventValue.Text = "1";
            }
            else
            {
                //hiding text box for Boolean ScheduleType
                textBoxEventValue.Visible = false;
            }
            switch (currevent)
            {
                case "Date":
                    this.dateRangeStartDateAny.Visible = false;
                    this.groupBoxWeekAndDay.Visible = false;
                    this.groupBoxEndDate.Visible = false;
                    this.groupBoxReferences.Visible = false;
                    this.groupBoxStartDate.Text = "";
                    break;
                case "Date Range":
                    this.dateRangeSDWeekDay.Visible = false;
                    this.dateRangeSDMonth.Visible = false;
                    this.dateRangeSDDay.Visible = false;
                    this.dateRangeSDYear.Visible = false;
                    this.groupBoxWeekAndDay.Visible = false;
                    this.groupBoxReferences.Visible = false;
                    break;
                case "Week And Day":
                    this.groupBoxEndDate.Visible = false;
                    this.groupBoxStartDate.Visible = false;
                    this.groupBoxReferences.Visible = false;
                    this.groupBoxWeekAndDay.Text = "";
                    this.groupBoxWeekAndDay.Location = new System.Drawing.Point(13, 112);
                    break;
                case "Calendar Reference":
                    this.groupBoxEndDate.Visible = false;
                    this.groupBoxStartDate.Visible = false;
                    this.groupBoxWeekAndDay.Visible = false;
                    this.groupBoxReferences.Location = new System.Drawing.Point(13, 112);
                    List<string> calendarObjects = bacNetIP.Calendars.OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Select(t => t.ObjectName).ToList();
                    this.ComboBoxCalendarObjects.DataSource = calendarObjects;
                    break;
            }
        }

        private void InitializeCustomComponents()
        {
            dateTimePickerStartTime.Format = DateTimePickerFormat.Custom;
            dateTimePickerStartTime.CustomFormat = "HH:mm:ss";
            dateTimePickerStartTime.ShowUpDown = true;
            dateTimePickerStartTime.Value = DateTime.Today;

            dateTimePickerEndTime.Format = DateTimePickerFormat.Custom;
            dateTimePickerEndTime.CustomFormat = "HH:mm:ss";
            dateTimePickerEndTime.ShowUpDown = true;
            dateTimePickerEndTime.Value = DateTime.Today;
        }
        private void DaTimePickerStartSelecDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = daTimePickerStartSelecDate.Value;
            numericUpDownStartDay.Value = selectedDate.Day;
            comboBoxStartMonth.SelectedIndex = selectedDate.Month - 1;
            string selectedYear = selectedDate.Year.ToString();
            if (!comboBoxStartYear.Items.Contains(selectedYear))
            {
                comboBoxStartYear.Items.Add(selectedYear);
            }
            comboBoxStartYear.SelectedItem = selectedYear;
            comboBoxStartWeekDay.SelectedIndex = (int)selectedDate.DayOfWeek;
        }

        private void DaTimePickerEndSelecDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = daTimePickerEndSelecDate.Value;
            numericUpDownEndDay.Value = selectedDate.Day;
            comboBoxEndMonth.SelectedIndex = selectedDate.Month - 1;
            string selectedYear = selectedDate.Year.ToString();
            if (!comboBoxEndYear.Items.Contains(selectedYear))
            {
                comboBoxEndYear.Items.Add(selectedYear);
            }
            comboBoxEndYear.SelectedItem = selectedYear;
            comboBoxEndWeekDay.SelectedIndex = (int)selectedDate.DayOfWeek;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            string buttonAction = this.buttonAdd.Text; // Save current text before changing it
            string EvntType = comboBoxEventsType.Text;

            TimeSpan newStartTime = new TimeSpan(dateTimePickerStartTime.Value.Hour, dateTimePickerStartTime.Value.Minute, dateTimePickerStartTime.Value.Second);
            TimeSpan newEndTime = new TimeSpan(dateTimePickerEndTime.Value.Hour, dateTimePickerEndTime.Value.Minute, dateTimePickerEndTime.Value.Second);

            if (buttonAction == "Add") // only check for new entries
            {
                if (EvntType == "Date" || EvntType == "Date Range" || EvntType == "Week And Day" || EvntType == "Calendar Reference")
                {
                    int currentEventCount = dataGridViewTimeSlots.Rows.Count;

                    if (currentEventCount >= 5)
                    {
                        MessageBox.Show("Maximum limit reached: Max 5 events per Special Event.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            foreach (DataGridViewRow row in dataGridViewTimeSlots.Rows)
            {
                if (row.Index == SelectedTimeRow)
                    continue;
                string start = row.Cells["StartTime"].Value?.ToString();
                string end = row.Cells["EndTime"].Value?.ToString();
                if (start == newStartTime.ToString(@"hh\:mm\:ss") && end == newEndTime.ToString(@"hh\:mm\:ss"))
                {
                    MessageBox.Show("A time slot with the same Start and End time already exists.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            string newValue = string.Empty;
            if (scheduletype == "Numeric")
                newValue = textBoxEventValue.Text.ToString();
            else
                newValue = comboBoxValue.Text.Equals("True") ? "1" : "0";

            //check value input parameter
            bool isValidVaule = CheckEventValue();
            if (!isValidVaule)
            {
                MessageBox.Show("Invalid Event value it should be between -2147483648 and 2147483647", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newEndTime <= newStartTime)
            {
                MessageBox.Show("End time cannot be less than or equal to start time.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (SelectedTimeRow >= 0)
                {
                    //Allow to edit time slots of events
                    DataGridViewRow row = dataGridViewTimeSlots.Rows[SelectedTimeRow];
                    row.Cells["StartTime"].Value = newStartTime.ToString(@"hh\:mm\:ss");
                    row.Cells["EndTime"].Value = newEndTime.ToString(@"hh\:mm\:ss");
                    row.Cells["Value"].Value = newValue;
                }
                else
                {
                    dataGridViewTimeSlots.Rows.Add(newStartTime.ToString(@"hh\:mm\:ss"), newEndTime.ToString(@"hh\:mm\:ss"), newValue);
                }
                dataGridViewTimeSlots.Sort(dataGridViewTimeSlots.Columns["StartTime"], System.ComponentModel.ListSortDirection.Ascending);
                SelectedTimeRow = -1;
                this.buttonAdd.Text = "Add"; // Set to Add after update is done
            }
        }
        private void SortTimeSlotsInAscendidng()
        {
            dataGridViewTimeSlots.Sort(dataGridViewTimeSlots.Columns["StartTime"],System.ComponentModel.ListSortDirection.Ascending);
        }

        private bool CheckEventValue()
        {
            if (scheduletype == "Numeric")
            {
                string input = textBoxEventValue.Text.Trim();

                if (double.TryParse(input, out double parsedValue))
                {
                    if (parsedValue >= -2147483648 && parsedValue <= 2147483647)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void DataGridViewTimeSlots_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTimeSlots.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                dataGridViewTimeSlots.Rows.RemoveAt(e.RowIndex);
                SelectedTimeRow = -1;
                this.buttonAdd.Text = "Add";
            }
        }
        private void TextBoxPriority_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private bool IsPriorityUnique(int priority)
        {
            if (isEditMode)
            {
                return !specialsEvents.Any(e => e.Priority == priority && e.EventId != currentObjectEventId);
            }
            return !specialsEvents.Any(e => e.Priority == priority);
        }
        private void TextBoxPriority_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int priority;

            if (string.IsNullOrWhiteSpace(textBoxPriority.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxPriority, "Priority is required.");
            }
            else if (!int.TryParse(textBoxPriority.Text, out priority) || priority < 0 || priority > 255)
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxPriority, "Priority must be a number between 0 and 255.");
            }
            else if (!IsPriorityUnique(priority))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxPriority, "Priority already exists.");
            }
            else
            {
                errorProvider.SetError(textBoxPriority, "");
            }
        }
        private void TextBoxName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxName, "Name Required.");
            }
            else
            {
                errorProvider.SetError(textBoxName, "");
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Please enter all mandatory fields properly", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(specialsEvents.Count() >= 20&& !isEditMode && (XMPS.Instance.PlcModel.Equals("XBLD-14E") || XMPS.Instance.PlcModel.Equals("XBLD-17E")))
            {
                MessageBox.Show("Maximum special event count reached", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            (bool, string) isEventSel = CheckIsEventSelected();
            if (!isEventSel.Item1 && comboBoxEventsType.Text.ToString().Equals("Week And Day"))
            {
                MessageBox.Show(isEventSel.Item2, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check if Event Name already assign to other Event
            SpecialEvent prevEvent = specialsEvents.Where(t => t.Name.Equals(textBoxName.Text.ToString())).FirstOrDefault();
            if ((prevEvent != null && currentObjectEventId == 0) || (prevEvent != null && currentObjectEventId != prevEvent.EventId))
            {
                MessageBox.Show("Event name already used in another event", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int newPriority = Convert.ToInt32(textBoxPriority.Text);
            if (!IsPriorityUnique(newPriority))
            {
                MessageBox.Show("Priority already exists.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (((string.IsNullOrWhiteSpace(comboBoxStartMonth.Text) || string.IsNullOrWhiteSpace(comboBoxStartYear.Text)) && !dateRangeStartDateAny.Checked) && (comboBoxEventsType.Text.ToString().Equals("Date Range")))
            {
                MessageBox.Show("Please select start Date.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (((string.IsNullOrWhiteSpace(comboBoxStartMonth.Text) || string.IsNullOrWhiteSpace(comboBoxStartYear.Text)) && !AllDateAnyOptions()) && comboBoxEventsType.Text.ToString().Equals("Date"))
            {
                MessageBox.Show("Please select start Date.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (((string.IsNullOrWhiteSpace(comboBoxEndMonth.Text) || string.IsNullOrWhiteSpace(comboBoxEndYear.Text)) && !dateRangeEndDateAny.Checked) && comboBoxEventsType.Text.ToString().Equals("Date Range"))
            {
                MessageBox.Show("Please select End Date.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //check Date Diff
            bool checkDate = CheckDateDiff();
            if (!checkDate)
                return;
            if (!ValidateChildren())
            {
                MessageBox.Show("Please Resolve Error First", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridViewTimeSlots.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Atleast One Event Value", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<EventValue> eventValues = new List<EventValue>();

            foreach (DataGridViewRow row in dataGridViewTimeSlots.Rows)
            {
                string startTime = row.Cells[0].Value?.ToString();
                string endTime = row.Cells[1].Value?.ToString();

                double value = 0;
                if (row.Cells[2].Value != null)
                {
                    double.TryParse(row.Cells[2].Value.ToString(), out value);
                }

                EventValue eventValue = new EventValue
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    Value = value
                };
                eventValues.Add(eventValue);
            }
            sepicalEvent = CreateSpecialEvent(currentSpeicalEvent);
            switch (currentSpeicalEvent)
            {
                case "Date":
                    sepicalEvent = new DateEvent();
                    ((DateEvent)sepicalEvent).Name = textBoxName.Text.Trim();
                    ((DateEvent)sepicalEvent).Type = comboBoxEventsType.Text;
                    ((DateEvent)sepicalEvent).Priority = Convert.ToInt32(textBoxPriority.Text);
                    ((DateEvent)sepicalEvent).StartDate = daTimePickerStartSelecDate.Value;
                    ((DateEvent)sepicalEvent).isWeekDayAny = dateRangeSDWeekDay.Checked;
                    ((DateEvent)sepicalEvent).isDayAny = dateRangeSDDay.Checked;
                    ((DateEvent)sepicalEvent).isMonthAny = dateRangeSDMonth.Checked;
                    ((DateEvent)sepicalEvent).isYearAny = dateRangeSDYear.Checked;
                    ((DateEvent)sepicalEvent).EventValues.AddRange(eventValues);
                    break;
                case "Date Range":
                    ((DateRangeEvent)sepicalEvent).isStartDateAny = dateRangeStartDateAny.Checked;
                    ((DateRangeEvent)sepicalEvent).Name = textBoxName.Text.Trim();
                    ((DateRangeEvent)sepicalEvent).Type = comboBoxEventsType.Text.Trim();
                    ((DateRangeEvent)sepicalEvent).Priority = Convert.ToInt32(textBoxPriority.Text);
                    ((DateRangeEvent)sepicalEvent).StartDate = daTimePickerStartSelecDate.Value;
                    ((DateRangeEvent)sepicalEvent).isEndDateAny = dateRangeEndDateAny.Checked;
                    ((DateRangeEvent)sepicalEvent).EndDate = daTimePickerEndSelecDate.Value;
                    ((DateRangeEvent)sepicalEvent).EventValues.AddRange(eventValues);
                    break;
                case "Week And Day":
                    ((WeekAndDayEvent)sepicalEvent).Name = textBoxName.Text.Trim();
                    ((WeekAndDayEvent)sepicalEvent).Type = comboBoxEventsType.Text.Trim();
                    ((WeekAndDayEvent)sepicalEvent).Priority = Convert.ToInt32(textBoxPriority.Text);
                    ((WeekAndDayEvent)sepicalEvent).MonthValue = ComboBoxMonth.SelectedIndex == 0 ? 255 : ComboBoxMonth.SelectedIndex;
                    ((WeekAndDayEvent)sepicalEvent).WeekValue = ComboBoxWeek.SelectedIndex == 0 ? 255 : ComboBoxWeek.SelectedIndex;
                    ((WeekAndDayEvent)sepicalEvent).DayValue = ComboBoxWeekDay.SelectedIndex == 0 ? 255 : ComboBoxWeekDay.SelectedIndex;
                    ((WeekAndDayEvent)sepicalEvent).EventValues.AddRange(eventValues);
                    break;
                case "Calendar Reference":
                    ((CalendarReference)sepicalEvent).Name = textBoxName.Text.Trim();
                    ((CalendarReference)sepicalEvent).Type = comboBoxEventsType.Text.Trim();
                    ((CalendarReference)sepicalEvent).Priority = Convert.ToInt32(textBoxPriority.Text);
                    ((CalendarReference)sepicalEvent).CalendarObjectName = ComboBoxCalendarObjects.Text;
                    ((CalendarReference)sepicalEvent).CalendarInstaceNumber = Convert.ToInt32(textBoxCalendarInstaceNo.Text.ToString());
                    ((CalendarReference)sepicalEvent).EventValues.AddRange(eventValues);
                    break;
            }
            //if object is an edit mode then assing old id
            if (currentObjectEventId > 0)
            {
                specialsEvents.RemoveAll(t => t.EventId == currentObjectEventId);
                sepicalEvent.EventId = currentObjectEventId;
            }
            else
                sepicalEvent.EventId = specialsEvents.Count > 0 ? specialsEvents.Max(t => t.EventId) + 1 : 1;
            specialsEvents.Add(sepicalEvent);
            BacNetValidator.ControlChanged(null, null);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool AllDateAnyOptions()
        {
            if (dateRangeSDWeekDay.Checked && dateRangeSDMonth.Checked && dateRangeSDDay.Checked && dateRangeSDYear.Checked)
            {
                return true;
            }
            return false;
        }

        private (bool, string) CheckIsEventSelected()
        {
            if (ComboBoxWeekDay.SelectedIndex == -1)
                return (false, "Please select WeekDay");
            if (ComboBoxWeek.SelectedIndex == -1)
                return (false, "Please select Week");
            if (ComboBoxMonth.SelectedIndex == -1)
                return (false, "Please select Month");
            return (true, string.Empty);
        }

        private bool CheckDateDiff()
        {
            DateTime startDate = daTimePickerStartSelecDate.Value;
            DateTime endDate = daTimePickerEndSelecDate.Value;

            if (startDate.Date == endDate.Date && currentSpeicalEvent.Equals("Date Range") && !dateRangeEndDateAny.Checked && !dateRangeStartDateAny.Checked)
            {
                MessageBox.Show("Start Date and End Date cannot be the same.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (endDate.Date < startDate.Date && currentSpeicalEvent.Equals("Date Range") && !dateRangeEndDateAny.Checked && !dateRangeStartDateAny.Checked)
            {
                MessageBox.Show("End Date cannot be earlier than Start Date.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private SpecialEvent CreateSpecialEvent(string currentSpecialEvent)
        {
            switch (currentSpecialEvent)
            {
                case "Date":
                    return new DateEvent();
                case "Date Range":
                    return new DateRangeEvent();
                case "Week And Day":
                    return new WeekAndDayEvent();
                case "Calendar Reference":
                    return new CalendarReference();
                default:
                    return null;
            }
        }

        private void DataGridViewTimeSlots_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewTimeSlots.Rows[e.RowIndex];
                SelectedTimeRow = e.RowIndex;
                this.buttonAdd.Text = "Update";
                string startTimeValue = row.Cells["StartTime"].Value.ToString();
                string endTimeValue = row.Cells["EndTime"].Value.ToString();
                if (scheduletype == "Numeric")
                    textBoxEventValue.Text = row.Cells["Value"].Value.ToString();
                else
                    comboBoxValue.SelectedIndex = row.Cells["Value"].Value.ToString() == "1" ? 0 : 1;

                TimeSpan startTime = TimeSpan.ParseExact(startTimeValue, @"hh\:mm\:ss", null);
                TimeSpan endTime = TimeSpan.ParseExact(endTimeValue, @"hh\:mm\:ss", null);

                // Set the DateTimePicker values to today's date plus the parsed TimeSpan values
                dateTimePickerStartTime.Value = DateTime.Today.Add(startTime);
                dateTimePickerEndTime.Value = DateTime.Today.Add(endTime);
            }
        }

        private void DaTimePickerStartSelecDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DateTime selectedStartDate = daTimePickerStartSelecDate.Value.Date;
            DateTime selectedEndDate = daTimePickerEndSelecDate.Value.Date;

            bool dateEventConflict = specialsEvents.OfType<DateEvent>()
                           .Any(dateEvent => dateEvent.StartDate.Date == selectedStartDate);

            // Check for conflicts with DateRangeEvent
            bool dateRangeEventConflict = specialsEvents.OfType<DateRangeEvent>()
                         .Any(dateRangeEvent => dateRangeEvent.StartDate.Date == selectedStartDate && dateRangeEvent.EndDate.Date == selectedEndDate);

            if (dateEventConflict && currentSpeicalEvent.Equals("Date") && !isEditMode)
            {
                e.Cancel = true;
                errorProvider.SetError(daTimePickerStartSelecDate, "Date Event Alredy Added on these start date");
            }
            else if (dateRangeEventConflict && currentSpeicalEvent.Equals("Date Range") && !isEditMode && !dateRangeEndDateAny.Checked && !dateRangeStartDateAny.Checked)
            {
                e.Cancel = true;
                errorProvider.SetError(daTimePickerStartSelecDate, "Date Range Event Alredy Added on these start date");
                errorProvider.SetError(daTimePickerEndSelecDate, "Date Range Event Alredy Added on these end date");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(daTimePickerStartSelecDate, String.Empty);
                errorProvider.SetError(daTimePickerEndSelecDate, String.Empty);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ComboBoxWeekDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxWeekDay.SelectedIndex != -1)
            {
                ComboBoxWeek.SelectedIndex = -1;
                ComboBoxMonth.SelectedIndex = -1;
            }
        }
        private void ComboBoxWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxWeek.SelectedIndex != -1)
            {
                ComboBoxWeekDay.SelectedIndex = -1;
                ComboBoxMonth.SelectedIndex = -1;
            }
        }
        private void ComboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxMonth.SelectedIndex != -1)
            {
                ComboBoxWeekDay.SelectedIndex = -1;
                ComboBoxWeek.SelectedIndex = -1;
            }
        }

        private void ComboBoxCalendarObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxCalendarObjects.SelectedIndex != -1)
            {
                string instanceNo = bacNetIP.Calendars
                .Where(t => t.ObjectName.Equals(ComboBoxCalendarObjects.Text))
                .Select(t => t.InstanceNumber)
                .FirstOrDefault();
                this.textBoxCalendarInstaceNo.Text = instanceNo;
            }
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxName != null)
            {
                if (!string.IsNullOrEmpty(textBoxName.Text) && textBoxName.Text.Length >= 20)
                {
                    if (e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        private void textBoxEventValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
