using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;

namespace XMPS2000.Bacnet
{
    public partial class FormCalendar : Form
    {
        private string currentObject;
        private BacNetIP bacNetIP;
        private Calendar calendar;
        private int currentEditObjId = -1;
        private bool isEdit = false;
        private int selectedRowIndex = -1;
        private readonly bool _isReadOnly;
        public FormCalendar(string currentObjectName, bool isReadOnly = false)
        {
            InitializeComponent();
            IntializeCustomComponent();
            currentObject = currentObjectName;
            AssignValues(currentObjectName);
            XMPS.Instance.LoadedProject.isChanged = false;
            XMPS.Instance.BacNetCurrentScreen = "FormCalendar";
            _isReadOnly = isReadOnly;
            ApplyReadOnly();
        }
        private void ApplyReadOnly()
        {
            if (_isReadOnly)
            {
                textBoxObjectIdentifier.Enabled = false;
                textBoxInstanceNumber.Enabled = false;
                textBoxObjectType.Enabled = false;
                textBoxObjectName.Enabled = false;
                textBoxDescription.Enabled = false;
                comboBoxEventsType.Enabled = false;
                btnSave.Enabled = false;
                dataGridViewEventsInfo.Enabled = false;
            }
        }
        private void AssignValues(string currentObjectName)
        {
            bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            calendar = bacNetIP.Calendars.Where(t => t.ObjectName.Equals(currentObject)).FirstOrDefault();
            if (calendar != null)
            {
                this.textBoxObjectIdentifier.Text = calendar.ObjectIdentifier;
                this.textBoxInstanceNumber.Text = calendar.InstanceNumber;
                this.textBoxObjectType.Text = calendar.ObjectType;
                this.textBoxObjectName.Text = calendar.ObjectName;
                this.textBoxDescription.Text = calendar.Description;

                if(calendar.Events != null && calendar.Events.Count > 0)
                {
                    dataGridViewEventsInfo.Visible = true;
                    dataGridViewEventsInfo.Rows.Clear();
                    UpdateInEventGrid(calendar.Events);
                }
                else
                {
                    dataGridViewEventsInfo.Visible = false;
                }
            }
        }

        private void IntializeCustomComponent()
        {
            groupBoxStartAndEndDate.Visible = false;
            groupBoxWeekAndDay.Visible = false;
            groupBox1.Visible = false;
            groupBoxEndDate.Visible = false;
            groupBoxStartDate.Visible = false;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxEventsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDesingAsPerEventType(comboBoxEventsType.Text);
        }

        private void BindDesingAsPerEventType(string currentEvent)
        {
            ClearPreviousData();
            groupBox1.Visible = true;
            TextBoxName.Text = string.Empty;
            TextBoxPriority.Text = string.Empty;
            groupBoxStartAndEndDate.Visible = true;
            switch (currentEvent)
            {
                case "Date":
                    this.dateRangeStartDateAny.Visible = false;
                    this.dateEventWeekDayAny.Visible = true;
                    this.dateEventDayAny.Visible = true;
                    this.dateEventMonthAny.Visible = true;
                    this.dateEventYearAny.Visible = true;
                    this.groupBoxStartDate.Visible = true;
                    this.groupBoxWeekAndDay.Visible = false;
                    this.groupBoxEndDate.Visible = false;
                    this.groupBoxStartDate.Text = "";
                    break;
                case "Date Range":
                    this.dateEventWeekDayAny.Visible = false;
                    this.dateEventDayAny.Visible = false;
                    this.dateEventMonthAny.Visible = false;
                    this.dateEventYearAny.Visible = false;
                    this.groupBoxStartDate.Visible = true;
                    this.groupBoxEndDate.Visible = true;
                    this.groupBoxWeekAndDay.Visible = false;
                    this.dateRangeStartDateAny.Visible = true;
                    break;
                case "Week And Day":
                    this.groupBoxWeekAndDay.Visible = true;
                    this.groupBoxEndDate.Visible = false;
                    this.groupBoxStartDate.Visible = false;
                    this.groupBoxWeekAndDay.Text = "";
                    this.groupBoxWeekAndDay.Location = new System.Drawing.Point(427, 74);
                    break;
            }
        }
        private void ClearPreviousData()
        {
            DaTimePickerStartSelecDate.ValueChanged -= DaTimePickerStartSelecDate_ValueChanged;
            DaTimePickerEndSelecDate.ValueChanged -= DaTimePickerEndSelecDate_ValueChanged;
            DaTimePickerStartSelecDate.Value = DateTime.Now;
            DaTimePickerEndSelecDate.Value = DateTime.Now;
            // Reset visibility of controls to false
            this.dateRangeStartDateAny.Checked = false;
            this.dateRangeEndDateAny.Checked = false;
            this.dateEventWeekDayAny.Checked = false;
            this.dateEventDayAny.Checked = false;
            this.dateEventMonthAny.Checked = false;
            this.dateEventYearAny.Checked = false;
            this.groupBoxStartDate.Visible = false;
            this.groupBoxEndDate.Visible = false;
            this.groupBoxWeekAndDay.Visible = false;

            TextBoxName.Text = string.Empty;
            TextBoxPriority.Text = string.Empty;

            ComboBoxStartMonth.SelectedIndex = -1;
            ComboBoxEndMonth.SelectedIndex = -1;
            ComboBoxStartYear.SelectedIndex = -1;
            ComboBoxEndYear.SelectedIndex = -1;
            ComboBoxStartWeekDay.SelectedIndex = -1;
            ComboBoxEndWeekDay.SelectedIndex = -1;

            ComboBoxWeekDay.SelectedIndex = -1;
            ComboBoxWeek.SelectedIndex = -1;
            ComboBoxMonth.SelectedIndex = -1;

            DaTimePickerStartSelecDate.ValueChanged += DaTimePickerStartSelecDate_ValueChanged;
            DaTimePickerEndSelecDate.ValueChanged += DaTimePickerEndSelecDate_ValueChanged;
           
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges(sender, e);
        }

        private bool SaveChanges(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(textBoxObjectName.Text.ToString())))
            {
                MessageBox.Show("Object Name Required", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBoxObjectName.Text.Trim()) && textBoxObjectName.Text.Trim().Any(ch => !(char.IsLetterOrDigit(ch) || ch == '_' || ch != ' ')))
            {
                errorProvider.SetError(textBoxObjectName, "Only letters, digits, underscore (_) and spaces are allowed.");
                return false;
            }
            if (char.IsDigit(textBoxObjectName.Text[0]))
            {             
                errorProvider.SetError(textBoxObjectName, "Tag name cannot start with a number.");
                return false;
            }
            foreach (char ch in textBoxObjectName.Text.Trim())
            {
                if (!char.IsLetterOrDigit(ch) && ch != 95 && ch != 3 && ch != 22) // Allowed characters
                {                   
                    errorProvider.SetError(textBoxObjectName, "Invalid character detected. Only letters, digits, and underscore (_) are allowed.");
                    return false;
                }
            }
            string text = textBoxObjectName?.Text?.Trim() ?? string.Empty;

            // Check if text is empty or whitespace
            if (string.IsNullOrWhiteSpace(text))
            {
                //e.Cancel = true;
                errorProvider.SetError(textBoxObjectName, "Tag name cannot be empty or whitespace.");
                return false;
            }
            var dublicateObject = bacNetIP.Calendars.Where(t => t.ObjectName.Equals(textBoxObjectName.Text.ToString().Trim())).FirstOrDefault();
            if (BacNetFormFactory.ValidateObjectName(textBoxObjectName.Text.Trim(), "Calendar") && dublicateObject?.InstanceNumber != calendar.InstanceNumber)
            {
                MessageBox.Show("Object name is already used, change the name and try again ...", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                DialogResult result = MessageBox.Show($"You want to save information for {calendar.ObjectName}", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    //Check in schedule object is there use current calendar reference in any Calendar Reference special event.
                    CheckInScheduleIsCalendarUsed(calendar.ObjectName);
                    calendar.ObjectName = textBoxObjectName.Text.Trim();
                    calendar.Description = textBoxDescription.Text.Trim();

                    bacNetIP.Calendars.Remove(bacNetIP.Calendars.Where(t => t.InstanceNumber.Equals(calendar.InstanceNumber)).FirstOrDefault());
                    bacNetIP.Calendars.Add(calendar);
                    MessageBox.Show("Calendar Information Updated Succesfully", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XMPS.Instance.LoadedProject.isChanged = false;
                    if ((FormBacNet)this.ParentForm != null)
                        ((FormBacNet)this.ParentForm).RefreshGridView();
                    else
                    {
                        var formBacNetInstance = Application.OpenForms
                                      .OfType<FormBacNet>()
                                      .FirstOrDefault(f => f.Name == "FormBacNet");
                        formBacNetInstance?.RefreshGridView();
                    }
                }
                return true;
            }
        }

        private void CheckInScheduleIsCalendarUsed(string objectName)
        {
            Schedule scheduleReference = bacNetIP.Schedules.Where(schedule => schedule.specialEvents != null &&
                                         schedule.specialEvents.OfType<CalendarReference>().Any(specialEvent =>
                                         specialEvent.CalendarObjectName == objectName)).FirstOrDefault();
            //if is used in Schedule then change the calendarObjectName
            if (scheduleReference != null)
            {
                foreach (SpecialEvent specialEvent in scheduleReference.specialEvents)
                {
                    if (specialEvent.Type.Equals("Calendar Reference") && ((CalendarReference)specialEvent).CalendarObjectName.Equals(calendar.ObjectName))
                    {
                        ((CalendarReference)specialEvent).CalendarObjectName = textBoxObjectName.Text.ToString();
                    }
                }
            }
        }

        private void DaTimePickerStartSelecDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = DaTimePickerStartSelecDate.Value;
            NumericUpDownStartDay.Value = selectedDate.Day;
            ComboBoxStartMonth.SelectedIndex = selectedDate.Month - 1;
            string selectedYear = selectedDate.Year.ToString();
            if (!ComboBoxStartYear.Items.Contains(selectedYear))
            {
                ComboBoxStartYear.Items.Add(selectedYear);
            }
            ComboBoxStartYear.SelectedItem = selectedYear;
            ComboBoxStartWeekDay.SelectedIndex = (int)selectedDate.DayOfWeek;
        }

        private void DaTimePickerEndSelecDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = DaTimePickerEndSelecDate.Value;
            NumericUpDownEndDay.Value = selectedDate.Day;
            ComboBoxEndMonth.SelectedIndex = selectedDate.Month - 1;
            string selectedYear = selectedDate.Year.ToString();
            if (!ComboBoxEndYear.Items.Contains(selectedYear))
            {
                ComboBoxEndYear.Items.Add(selectedYear);
            }
            ComboBoxEndYear.SelectedItem = selectedYear;
            ComboBoxEndWeekDay.SelectedIndex = (int)selectedDate.DayOfWeek;
        }
        private void TextBoxName_Validating_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxName.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(TextBoxName, "Name Required.");
            }
            else
            {
                errorProvider.SetError(TextBoxName, "");
            }
        }
        private bool IsPriorityUnique(int priority, int currentEventId = -1)
        {
            if (calendar.Events == null) return true;
            return !calendar.Events.Any(e => e.Priority == priority && e.EventId != currentEventId);
        }
        private void TextBoxPriority_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int priority;

            if (string.IsNullOrWhiteSpace(TextBoxPriority.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(TextBoxPriority, "Priority is required.");
            }
            else if (!int.TryParse(TextBoxPriority.Text, out priority) || priority < 0 || priority > 255)
            {
                e.Cancel = true;
                errorProvider.SetError(TextBoxPriority, "Priority must be a number between 0 and 255.");
            }
            else if (!IsPriorityUnique(priority, currentEditObjId))
            {
                e.Cancel = true;
                errorProvider.SetError(TextBoxPriority, "Priority must be unique. This priority is already assigned to another event.");
            }
            else
            {
                errorProvider.SetError(TextBoxPriority, "");
            }
        }

        private void TextBoxPriority_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if(dataGridViewEventsInfo.Rows.Count >= 20 && !isEdit && (XMPS.Instance.PlcModel.Equals("XBLD-14E") || XMPS.Instance.PlcModel.Equals("XBLD-17E")))
            {
                MessageBox.Show("Maximum special event count reached", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidateChildren())
            {
                MessageBox.Show("Please Resolve Error First", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (((string.IsNullOrWhiteSpace(ComboBoxStartMonth.Text) || string.IsNullOrWhiteSpace(ComboBoxStartYear.Text)) && !dateRangeStartDateAny.Checked) && comboBoxEventsType.Text.ToString().Equals("Date Range"))
            {
                MessageBox.Show("Please select start Date.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (((string.IsNullOrWhiteSpace(ComboBoxStartMonth.Text) || string.IsNullOrWhiteSpace(ComboBoxStartYear.Text)) && !AllDateAnyOptions()) && comboBoxEventsType.Text.ToString().Equals("Date"))
            {
                MessageBox.Show("Please select start Date.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (calendar.Events == null)
            {
                calendar.Events = new List<SpecialEvent>();
            }
            int priority = Convert.ToInt32(TextBoxPriority.Text);
            if (!IsPriorityUnique(priority, currentEditObjId))
            {
                MessageBox.Show("Priority must be unique. This priority is already assigned to another event.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (((string.IsNullOrWhiteSpace(ComboBoxEndMonth.Text) || string.IsNullOrWhiteSpace(ComboBoxEndYear.Text)) && !dateRangeEndDateAny.Checked) && comboBoxEventsType.Text.ToString().Equals("Date Range"))
            {
                MessageBox.Show("Please select End Date.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            (bool, string) isEventSel = CheckIsEventSelected();
            if (!isEventSel.Item1 && comboBoxEventsType.Text.ToString().Equals("Week And Day"))
            {
                MessageBox.Show(isEventSel.Item2, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //check if Event Name already assign to other Event
            SpecialEvent prevEvent = calendar.Events.Where(t => t.Name.Equals(TextBoxName.Text.Trim())).FirstOrDefault();
            if ((prevEvent != null && currentEditObjId == -1) || (prevEvent != null && currentEditObjId != prevEvent.EventId))
            {
                MessageBox.Show("Event name already used in another event", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool checkDates = CheckDateDiff();
            if (!checkDates)
                return;
            else
            {
                SpecialEvent specialEvent = CreateSpecialEvent(comboBoxEventsType.Text.ToString());
                switch (comboBoxEventsType.Text.ToString())
                {
                    case "Date":
                        specialEvent = new DateEvent();
                        ((DateEvent)specialEvent).Name = TextBoxName.Text.Trim();
                        ((DateEvent)specialEvent).Type = comboBoxEventsType.Text;
                        ((DateEvent)specialEvent).Priority = Convert.ToInt32(TextBoxPriority.Text);
                        ((DateEvent)specialEvent).StartDate = DaTimePickerStartSelecDate.Value;
                        ((DateEvent)specialEvent).isWeekDayAny = dateEventWeekDayAny.Checked;
                        ((DateEvent)specialEvent).isDayAny = dateEventDayAny.Checked;
                        ((DateEvent)specialEvent).isMonthAny = dateEventMonthAny.Checked;
                        ((DateEvent)specialEvent).isYearAny = dateEventYearAny.Checked;
                        break;
                    case "Date Range":
                        ((DateRangeEvent)specialEvent).Name = TextBoxName.Text.Trim();
                        ((DateRangeEvent)specialEvent).Type = comboBoxEventsType.Text;
                        ((DateRangeEvent)specialEvent).Priority = Convert.ToInt32(TextBoxPriority.Text);
                        ((DateRangeEvent)specialEvent).StartDate = DaTimePickerStartSelecDate.Value;
                        ((DateRangeEvent)specialEvent).isStartDateAny = dateRangeStartDateAny.Checked;
                        ((DateRangeEvent)specialEvent).EndDate = DaTimePickerEndSelecDate.Value;
                        ((DateRangeEvent)specialEvent).isEndDateAny = dateRangeEndDateAny.Checked;
                        break;
                    case "Week And Day":
                        ((WeekAndDayEvent)specialEvent).Name = TextBoxName.Text.Trim();
                        ((WeekAndDayEvent)specialEvent).Type = comboBoxEventsType.Text;
                        ((WeekAndDayEvent)specialEvent).Priority = Convert.ToInt32(TextBoxPriority.Text);
                        ((WeekAndDayEvent)specialEvent).MonthValue = ComboBoxMonth.SelectedIndex == 0 ? 255 : ComboBoxMonth.SelectedIndex;
                        ((WeekAndDayEvent)specialEvent).WeekValue = ComboBoxWeek.SelectedIndex == 0 ? 255 : ComboBoxWeek.SelectedIndex;
                        ((WeekAndDayEvent)specialEvent).DayValue = ComboBoxWeekDay.SelectedIndex == 0 ? 255 : ComboBoxWeekDay.SelectedIndex;
                        break;
                }
                if (currentEditObjId > 0)
                {
                    calendar.Events.RemoveAll(t => t.EventId == currentEditObjId);
                    specialEvent.EventId = currentEditObjId;
                    dataGridViewEventsInfo.Rows.RemoveAt(selectedRowIndex);
                    currentEditObjId = -1;
                    selectedRowIndex = -1;
                    isEdit = false;
                }
                else
                    specialEvent.EventId = calendar.Events.Count > 0 ? calendar.Events.Max(t => t.EventId) + 1 : 1;
                calendar.Events.Add(specialEvent);
                dataGridViewEventsInfo.Rows.Clear();
                UpdateInEventGrid(calendar.Events);
                BacNetValidator.ControlChanged(null, null);
            }
        }
        private bool AllDateAnyOptions()
        {
            if(dateEventWeekDayAny.Checked && dateEventMonthAny.Checked && dateEventDayAny.Checked && dateEventYearAny.Checked)
            {
                return true;
            }
            return false;
        }
        private (bool, string) CheckIsEventSelected()
        {
            if (ComboBoxWeekDay.SelectedIndex == -1)
                return (false, "Please select WeekDay");
            if(ComboBoxWeek.SelectedIndex == -1)
                return (false, "Please select Week");
            if(ComboBoxMonth.SelectedIndex == -1)
                return (false, "Please select Month");
            return (true, string.Empty);
        }

        private void AddInEventsGridView(SpecialEvent specialEvent)
        {
            const string dateTimeFormat = "dd-MM-yyyy";
            string eventType = specialEvent.Type;
            int eventId = specialEvent.EventId;
            string eventName = specialEvent.Name;
            string startDate = string.Empty;
            string endDate = string.Empty;

            switch (specialEvent.Type)
            {
                case "Date":
                    var dateEvent = (DateEvent)specialEvent;
                    startDate = dateEvent.StartDate.ToString(dateTimeFormat);
                    break;

                case "Date Range":
                    var dateRangeEvent = (DateRangeEvent)specialEvent;
                    startDate = dateRangeEvent.StartDate.ToString(dateTimeFormat);
                    endDate = dateRangeEvent.EndDate.ToString(dateTimeFormat);
                    break;

                case "Week And Day":
                    var weekAndDayEvent = (WeekAndDayEvent)specialEvent;
                    startDate = $"{weekAndDayEvent.MonthValue} {weekAndDayEvent.WeekValue} {weekAndDayEvent.DayValue}";
                    break;
            }
            dataGridViewEventsInfo.Rows.Add(eventType, eventName, eventId, startDate, endDate);
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
                default:
                    return null;
            }
        }
        private bool CheckDateDiff()
        {
            DateTime startDate = DaTimePickerStartSelecDate.Value;
            DateTime endDate = DaTimePickerEndSelecDate.Value;

            if (endDate.Date < startDate.Date && comboBoxEventsType.Text.ToString().Equals("Date Range"))
            {
                MessageBox.Show("End Date cannot be earlier than Start Date.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void dataGridViewEventsInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewEventsInfo.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                ScheduleEditClick(e.RowIndex);
            }
            else if (e.ColumnIndex == dataGridViewEventsInfo.Columns["Remove"].Index && e.RowIndex >= 0)
            {
                ScheduleRemoveClick(e.RowIndex);
            }
        }

        private void ScheduleRemoveClick(int rowIndex)
        {
            string eventName = dataGridViewEventsInfo.Rows[rowIndex].Cells[0]?.Value?.ToString();
            int eventId = Convert.ToInt32(dataGridViewEventsInfo.Rows[rowIndex].Cells[2]?.Value?.ToString());
            if (string.IsNullOrEmpty(eventName)) return;

            DialogResult result = MessageBox.Show($"You want to remove {eventName} from {calendar.ObjectName}", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                calendar.Events.RemoveAll(t => t.EventId == eventId);
                dataGridViewEventsInfo.Rows.RemoveAt(rowIndex);
                RemoveAndReorderEvents(eventId);
                dataGridViewEventsInfo.Rows.Clear();
                UpdateInEventGrid(calendar.Events);
                BacNetValidator.ControlChanged(null, null);
            }
            //Reset the edit flag and currentEditObjectId if user open special event for edit mode
            //remove that event at same time.
            if(currentEditObjId == eventId)
            {
                currentEditObjId = -1;
                selectedRowIndex = -1;
                isEdit = false;
            }
        }

        private void UpdateInEventGrid(List<SpecialEvent> events)
        {
            dataGridViewEventsInfo.Visible = true;
            if (events != null)
            {
                foreach (SpecialEvent specialEvent in events.OrderBy(t => t.EventId))
                {
                    //additional check if old project contains Week and Day special event and values are 0;
                    if (specialEvent.Type == "Week And Day")
                    {
                        var weekAndDayEvent = (WeekAndDayEvent)specialEvent;
                        weekAndDayEvent.MonthValue = weekAndDayEvent.MonthValue == 0 ? 255 : weekAndDayEvent.MonthValue;
                        weekAndDayEvent.WeekValue = weekAndDayEvent.WeekValue == 0 ? 255 : weekAndDayEvent.WeekValue;
                        weekAndDayEvent.DayValue = weekAndDayEvent.DayValue == 0 ? 255 : weekAndDayEvent.DayValue;
                    }
                    AddInEventsGridView(specialEvent);
                }
            }
        }

        private void RemoveAndReorderEvents(int eventId)
        {
            // Update EventIds in calendar.Events
            if (calendar.Events != null && calendar.Events.Count > 0)
            {
                foreach (var specialEvent in calendar.Events.OrderBy(t => t.EventId))
                {
                    if (specialEvent.EventId > eventId)
                    {
                        specialEvent.EventId--;
                    }
                }
            }
        }

        private void ScheduleEditClick(int rowIndex)
        {
            string eventType = dataGridViewEventsInfo.Rows[rowIndex].Cells[0]?.Value?.ToString();
            int eventId = Convert.ToInt32(dataGridViewEventsInfo.Rows[rowIndex].Cells[2]?.Value?.ToString());

            if (string.IsNullOrEmpty(eventType)) return;
            isEdit = true;
            currentEditObjId = eventId;
            selectedRowIndex = rowIndex;
            ShowDataOnForm(eventType, eventId);
        }

        private void ShowDataOnForm(string eventType, int eventId)
        {
            SpecialEvent specialEvent = calendar.Events.Where(t => t.Type.Equals(eventType) && t.EventId == eventId).FirstOrDefault();
            if (specialEvent != null)
            {
                this.TextBoxName.Text = specialEvent.Name;
                this.comboBoxEventsType.Text = eventType;
                this.TextBoxPriority.Text = specialEvent.Priority.ToString();
                errorProvider.SetError(TextBoxPriority, "");

                switch (eventType)
                {
                    case "Date":
                        DateEvent date = (DateEvent)specialEvent;
                        this.TextBoxName.Text = date.Name;
                        this.TextBoxPriority.Text = date.Priority.ToString();
                        this.dateEventDayAny.Checked = date.isDayAny;
                        this.dateEventWeekDayAny.Checked = date.isWeekDayAny;
                        this.dateEventMonthAny.Checked = date.isMonthAny;
                        this.dateEventYearAny.Checked = date.isYearAny;
                        this.DaTimePickerStartSelecDate.Value = date.StartDate;
                        break;
                    case "Date Range":
                        DateRangeEvent dateRange = (DateRangeEvent)specialEvent;
                        this.TextBoxName.Text = dateRange.Name;
                        this.comboBoxEventsType.Text = dateRange.Type;
                        this.TextBoxPriority.Text = dateRange.Priority.ToString();
                        this.DaTimePickerStartSelecDate.Value = dateRange.StartDate;
                        this.DaTimePickerEndSelecDate.Value = dateRange.EndDate;
                        this.dateRangeStartDateAny.Checked = dateRange.isStartDateAny;
                        this.dateRangeEndDateAny.Checked = dateRange.isEndDateAny;
                        break;
                    case "Week And Day":
                        WeekAndDayEvent weekAndDay = (WeekAndDayEvent)specialEvent;
                        this.TextBoxName.Text = weekAndDay.Name;
                        this.comboBoxEventsType.Text = weekAndDay.Type;
                        this.TextBoxPriority.Text = weekAndDay.Priority.ToString();
                        AssingWeekAndDayValues(weekAndDay.MonthValue, weekAndDay.WeekValue, weekAndDay.DayValue);
                        break;
                }
                BacNetValidator.ControlChanged(null, null);
            }
        }
        private void AssingWeekAndDayValues(int monthValue, int weekValue, int dayValue)
        {
            ComboBoxMonth.SelectedIndex = monthValue == 255 ? 0 : monthValue;
            ComboBoxWeek.SelectedIndex = weekValue == 255 ? 0 : weekValue;
            ComboBoxWeekDay.SelectedIndex = dayValue == 255 ? 0 : dayValue;
        }

        private void DaTimePickerStartSelecDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DateTime selectedStartDate = DaTimePickerStartSelecDate.Value.Date;
            DateTime selectedEndDate = DaTimePickerEndSelecDate.Value.Date;
            if(calendar.Events != null)
            {
                bool dateEventConflict = calendar.Events.OfType<DateEvent>()
                           .Any(dateEvent => dateEvent.StartDate.Date == selectedStartDate);
                //getting the SpecialDateEvent by starDate
                SpecialEvent specialDateEvent = calendar.Events.OfType<DateEvent>()
                           .Where(dateEvent => dateEvent.StartDate.Date == selectedStartDate).FirstOrDefault();

                // Check for conflicts with DateRangeEvent
                bool dateRangeEventConflict = calendar.Events.OfType<DateRangeEvent>()
                             .Any(dateRangeEvent => dateRangeEvent.StartDate.Date == selectedStartDate && dateRangeEvent.EndDate.Date == selectedEndDate);

                //getting the SpecialDateRangeEvent by starDate
                SpecialEvent specialDateRangeEvent = calendar.Events.OfType<DateRangeEvent>()
                             .Where(dateRangeEvent => dateRangeEvent.StartDate.Date == selectedStartDate && dateRangeEvent.EndDate.Date == selectedEndDate).FirstOrDefault();

                if (dateEventConflict && comboBoxEventsType.Text.ToString().Equals("Date") && (!isEdit || currentEditObjId != specialDateEvent.EventId))
                {
                    e.Cancel = true;
                    errorProvider.SetError(DaTimePickerStartSelecDate, "Date Event Already Added on these start date");
                }
                else if (dateRangeEventConflict && comboBoxEventsType.Text.ToString().Equals("Date Range") && (!isEdit || currentEditObjId != specialDateRangeEvent.EventId) && !dateRangeEndDateAny.Checked && !dateRangeStartDateAny.Checked)
                {
                    e.Cancel = true;
                    errorProvider.SetError(DaTimePickerStartSelecDate, "Date Range Event Already Added on these start date");
                    errorProvider.SetError(DaTimePickerEndSelecDate, "Date Range Event Already Added on these end date");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(DaTimePickerStartSelecDate, String.Empty);
                    errorProvider.SetError(DaTimePickerEndSelecDate, String.Empty);
                }
            }
        }
        //private void textBoxObjectName_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    CommonTextBox_KeyPress(sender, e);
        //}
        //private void CommonTextBox_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    TextBox textBox = sender as TextBox;
        //    if (textBox != null)
        //    {
        //        if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text.Length >= 20)
        //        {
        //            if (e.KeyChar != (char)Keys.Back)
        //            {
        //                e.Handled = true;
        //            }
        //        }
        //    }
        //}
        //private void textBoxDescription_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    CommonTextBox_KeyPress(sender, e);
        //}

        private void TextBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TextBoxName != null)
            {
                if (!string.IsNullOrEmpty(TextBoxName.Text) && TextBoxName.Text.Length >= 20)
                {
                    if (e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            if (textBoxDescription.Text.Length > 25)
            {
                errorProvider.SetError(textBoxDescription, "Object Name must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(textBoxDescription, null);
            }
        }

        private void textBoxObjectName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxObjectName.Text) && textBoxObjectName.Text.Length > 25)
            {
                errorProvider.SetError(textBoxObjectName, "Object Name must not exceed 25 characters.");
            }
            else
            {
                errorProvider.SetError(textBoxObjectName, null);
            }
            if (string.IsNullOrEmpty(textBoxObjectName.Text))
            {
                //e.Cancel = true;
                errorProvider.SetError(textBoxObjectName, "Please resolve the errors first");
            }
        }

        private void textBoxObjectName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //for checking length
            if (!string.IsNullOrWhiteSpace(textBoxObjectName.Text) && textBoxObjectName.Text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxObjectName, "Please resolve the errors first");
            }
            if (textBoxObjectName.Text == "")
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxObjectName, "Please resolve the errors first");
            }            

        }
    }
}
