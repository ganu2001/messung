//using iTextSharp.text.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.LadderLogic;

namespace XMPS2000.Bacnet
{
    public partial class FormSchedule : Form, IXMForm
    {
        private BacNetIP bacNetIP;
        private List<EventParticularDay> eventParticularDay;
        private List<SpecialEvent> localspecialEvents;
        private Schedule schedule;
        public DateTime startDate;
        public DateTime endDate;
        private readonly bool _isReadOnly;
        public FormSchedule(string currentObject, bool isReadOnly = false)
        {
            InitializeComponent();
            foreach (DataGridViewColumn column in dataGridViewSpecialEvents.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.groupBoxSpecialEvents.Visible = false;
            eventParticularDay = new List<EventParticularDay>();
            localspecialEvents = new List<SpecialEvent>();
            AssignValues(currentObject);
            XMPS.Instance.LoadedProject.isChanged = false;
            XMPS.Instance.BacNetCurrentScreen = "FormSchedule";
            InitializePriorityContextMenu();
            _isReadOnly = isReadOnly;
            dataGridViewSpecialEvents.UserDeletingRow += dataGridViewSpecialEvents_DeletingRow;
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
                comboBoxScheduleValue.Enabled = false;
                btnSave.Enabled = false;
                textBoxTotalNoOfEvent.Enabled = false;
                dateTimePickerStartDate.Enabled = false;
                dateTimePickerEndDate.Enabled = false;
                checkBoxAny.Enabled = false;
                comboBoxScheduleType.Enabled = false;
                dataGridViewSpecialEvents.Enabled = false;
                comboBoxEventType.Enabled = false;
                GbValue.Enabled = false;
            }
        }
        private ContextMenuStrip priorityContextMenu;
        private void InitializePriorityContextMenu()
        {
            priorityContextMenu = new ContextMenuStrip();
            var moveUpItem = new ToolStripMenuItem("Up: Higher Priority");
            moveUpItem.Click += MoveUpItem_Click;
            priorityContextMenu.Items.Add(moveUpItem);

            var moveDownItem = new ToolStripMenuItem("Down: Lower Priority");
            moveDownItem.Click += MoveDownItem_Click;
            priorityContextMenu.Items.Add(moveDownItem);

            dataGridViewSpecialEvents.ContextMenuStrip = priorityContextMenu;
        }
        private void AssignValues(string currentObject)
        {
            bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            schedule = bacNetIP.Schedules.Where(t => t.ObjectName.Equals(currentObject)).FirstOrDefault();
            if (schedule != null)
            {
                this.textBoxObjectIdentifier.Text = schedule.ObjectIdentifier;
                this.textBoxInstanceNumber.Text = schedule.InstanceNumber;
                this.textBoxObjectType.Text = schedule.ObjectType;
                this.textBoxObjectName.Text = schedule.ObjectName;
                this.textBoxDescription.Text = schedule.Description;
                if (textObjectName.Text=="0"|| textObjectName.Text == "1")
                {
                    this.textObjectName.Text = string.IsNullOrWhiteSpace(schedule.Value) ? "0" : schedule.Value;
                }
                this.textObjectName.Text = string.IsNullOrWhiteSpace(schedule.Value) ? " " : schedule.Value;
                this.ChkNull.Checked = schedule.Nullvalue;
                this.checkBoxAny.Checked = schedule.AnyCheck;
                this.labelTagName.Text = XMProValidator.GetTheTagnameFromAddress(schedule.LogicalAddress) + " - " + schedule.LogicalAddress;
                if (schedule.StartDate != null)
                    this.dateTimePickerStartDate.Value = schedule.StartDate.Date;
                if (schedule.EndDate != null)
                    this.dateTimePickerEndDate.Value = schedule.EndDate.Date;
                this.comboBoxScheduleValue.SelectedIndex = schedule.ScheduleValue;
                if (schedule.ScheduleType != null)
                {
                    comboBoxScheduleType.SelectedIndexChanged -= comboBoxScheduleType_SelectedIndexChanged;
                    this.comboBoxScheduleType.SelectedIndex = 0;
                    comboBoxScheduleType.SelectedIndexChanged += comboBoxScheduleType_SelectedIndexChanged;
                }
                //Save eventParticular Days for local handling
                if (schedule.EventParticularDays != null && schedule.EventParticularDays.Count > 0)
                {
                    eventParticularDay.AddRange(schedule.EventParticularDays);
                    this.groupBoxSpecialEvents.Visible = true;
                    dataGridViewSpecialEvents.Rows.Add("Weekly");
                    HideSpecialEventParameters(false);
                }
                //save specialEvents information for local handling
                if (schedule.specialEvents != null)
                {
                    this.groupBoxSpecialEvents.Visible = true;
                    //additional check if old project contains Week and Day special event and values are 0;
                    foreach (SpecialEvent specialEvent in schedule.specialEvents)
                    {
                        if (specialEvent.Type == "Week And Day")
                        {
                            var weekAndDayEvent = (WeekAndDayEvent)specialEvent;
                            weekAndDayEvent.MonthValue = weekAndDayEvent.MonthValue == 0 ? 255 : weekAndDayEvent.MonthValue;
                            weekAndDayEvent.WeekValue = weekAndDayEvent.WeekValue == 0 ? 255 : weekAndDayEvent.WeekValue;
                            weekAndDayEvent.DayValue = weekAndDayEvent.DayValue == 0 ? 255 : weekAndDayEvent.DayValue;
                        }
                    }
                    localspecialEvents.AddRange(schedule.specialEvents);
                    HideSpecialEventParameters(true);
                    dataGridViewSpecialEvents.Rows.Clear();
                    UpdateInEventGrid(schedule.specialEvents);
                }
            }
        }

        private void HideSpecialEventParameters(bool value)
        {
            this.textBoxTotalNoOfEvent.Visible = value;
            this.comboBoxEventType.Visible = value;
            this.lblTotalNoOfEvents.Visible = value;
            this.lblEventType.Visible = value;
        }

        public void OnShown()
        {
            throw new NotImplementedException();
        }

        private void comboBoxScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check Start and End Date values
            bool check = CheckDateDiff();
            if (!check)
                return;

            if (comboBoxScheduleType.Text == "Weekly")
            {
                OpenWeeklySheduleTypeForm();
            }
            else
            {
                this.groupBoxSpecialEvents.Visible = true;
                HideSpecialEventParameters(true);
                dataGridViewSpecialEvents.Rows.Clear();
                UpdateInEventGrid((schedule.specialEvents != null && schedule.specialEvents.Count > 0) ? schedule.specialEvents : localspecialEvents);
            }
        }

        private bool CheckDateDiff()
        {
            DateTime startDate = dateTimePickerStartDate.Value;
            DateTime endDate = dateTimePickerEndDate.Value;

            if (startDate.Date == endDate.Date)
            {
                MessageBox.Show("Effective Start Date and End Date cannot be the same.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (endDate.Date < startDate.Date)
            {
                MessageBox.Show("End Date cannot be earlier than Start Date.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void OpenWeeklySheduleTypeForm()
        {
            CustomCalendar customCalendar = new CustomCalendar(eventParticularDay, comboBoxScheduleValue.Text);

            int centerX = this.Left + (this.Width - 450) / 2;
            int centerY = this.Top + (this.Height - customCalendar.Height + 25) / 2;

            customCalendar.StartPosition = FormStartPosition.Manual;
            customCalendar.Location = new System.Drawing.Point(centerX, centerY);
            // Show the customCalendar form as a dialog
            DialogResult result = customCalendar.ShowDialog();
            if (result == DialogResult.OK)
            {
                eventParticularDay = customCalendar.eventParticularDays;
                dataGridViewSpecialEvents.Rows.Clear();
                this.groupBoxSpecialEvents.Visible = true;
                HideSpecialEventParameters(false);
                UpdateInEventGrid(localspecialEvents);
                BacNetValidator.ControlChanged(null, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges(sender, e);
        }
        private void ChkNull_CheckedChanged(object sender, EventArgs e)
        {
         //schedule.Nullvalue = ChkNull.Checked ;
        }
        private void dataGridViewSpecialEvents_DeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Index < 0) return;
            string eventName = e.Row.Cells[0]?.Value?.ToString();
            if (string.IsNullOrEmpty(eventName)) return;
            int eventId;
            if (!int.TryParse(e.Row.Cells[1]?.Value?.ToString(), out eventId))
                return;
            DialogResult result = MessageBox.Show($"You want to remove {eventName} from {schedule.ObjectName}", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                switch (eventName)
                {
                    case "Weekly":
                        if (schedule.EventParticularDays != null && schedule.EventParticularDays.Count > 0)
                            schedule.EventParticularDays.Clear();
                        eventParticularDay.Clear();
                        break;
                    case "Date":
                    case "Date Range":
                    case "Week And Day":
                    case "Calendar Reference":
                        if (schedule.specialEvents != null && schedule.specialEvents.Count > 0)
                            schedule.specialEvents.RemoveAll(t => t.EventId == eventId);
                        localspecialEvents.RemoveAll(t => t.EventId == eventId);
                        break;
                }
                BacNetValidator.ControlChanged(null, null);
            }
            else
            {
                e.Cancel = true;
            }
        }
        private bool SaveChanges(object sender, EventArgs e)
        {
            //Check Start and End Date values
            bool check = CheckDateDiff();
            if (!check)
                return false;

            if (!ValidateChildren())
            {
                MessageBox.Show("Please resolve error first.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var dublicateObject = bacNetIP.Schedules.Where(t => t.ObjectName.Equals(textBoxObjectName.Text.ToString().Trim())).FirstOrDefault();
            if (BacNetFormFactory.ValidateObjectName(textBoxObjectName.Text.Trim(), "Schedule") && dublicateObject.ObjectIdentifier != schedule.ObjectIdentifier)
            {
                MessageBox.Show("Tag name is already used, change the name and try again ...", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                schedule.ObjectName = textBoxObjectName.Text.Trim();
                schedule.Description = textBoxDescription.Text.Trim();
                schedule.ScheduleValue = comboBoxScheduleValue.Text.Equals("Numeric") ? 1 : 0;
                schedule.ScheduleType = comboBoxScheduleType.Text.Equals("Weekly") ? "bo" : "b1";
                schedule.Nullvalue = ChkNull.Checked;
                schedule.Value = textObjectName.Text;
                schedule.StartDate = new EffectivePeriod()
                {
                    Date = dateTimePickerStartDate.Value,
                    DayOfWeek = (int)dateTimePickerStartDate.Value.DayOfWeek
                };
                schedule.EndDate = new EffectivePeriod()
                {
                    Date = dateTimePickerEndDate.Value,
                    DayOfWeek = (int)dateTimePickerEndDate.Value.DayOfWeek
                };
                //saving eventParticular Day information 
                if (schedule.EventParticularDays == null)
                {
                    schedule.EventParticularDays = new List<EventParticularDay>();
                    schedule.EventParticularDays.AddRange(eventParticularDay);
                }

                if (eventParticularDay != null && schedule.EventParticularDays != null)
                {
                    schedule.NoOfDaySelected = ConvertToBinary(eventParticularDay.Select(t => t.DayName.ToString()).ToList());
                    schedule.EventParticularDays.Clear();
                    schedule.EventParticularDays.AddRange(eventParticularDay);
                }
                // saving special events information 
                if (localspecialEvents != null && schedule.specialEvents != null)
                {
                    schedule.specialEvents.Clear();
                    schedule.specialEvents.AddRange(localspecialEvents);
                }
                else if (schedule.specialEvents == null)
                {
                    schedule.specialEvents = new List<SpecialEvent>();
                    schedule.specialEvents.AddRange(localspecialEvents);
                }
                schedule.AnyCheck = checkBoxAny.Checked;
                bacNetIP.Schedules.Remove(bacNetIP.Schedules.Where(t => t.InstanceNumber.Equals(schedule.InstanceNumber)).FirstOrDefault());
                bacNetIP.Schedules.Add(schedule);
                MessageBox.Show("Schedule Information Updated Succesfully", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                return true;
            }
        }

        public string ConvertToBinary(List<string> selectedDays)
        {
            string[] daysOfWeek = { "Sunday", "Saturday", "Friday", "Thursday", "Wednesday", "Tuesday", "Monday" };
            Dictionary<string, int> dayMappings = daysOfWeek.Select((day, index) => new { Day = day, Index = index })
                                                  .ToDictionary(x => x.Day, x => 1 << x.Index);
            int binaryValue = 0;
            foreach (string day in selectedDays)
            {
                if (dayMappings.ContainsKey(day))
                {
                    binaryValue |= dayMappings[day];
                }
            }
            string binaryString = Convert.ToString(binaryValue, 2).PadLeft(7, '0');

            return binaryValue.ToString();
        }

        private void comboBoxEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenSpecialEventTypeForm(comboBoxEventType.Text, 0);
        }

        private void OpenSpecialEventTypeForm(string specialEventType, int eventId, bool isEdited = false)
        {
            string scheduleType = schedule.ScheduleValue == 1 ? "Numeric" : "Boolean";
            //adding check for Calendar Reference Special Event Type
            if (specialEventType.Equals("Calendar Reference") && bacNetIP.Calendars.Count() == 0)
            {
                MessageBox.Show("Calendar object not available", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Check Start and End Date values
            bool check = CheckDateDiff();
            if (!check)
                return;

            if (localspecialEvents == null)
                localspecialEvents = new List<SpecialEvent>();
            FormSpecialEvents specialEventForm;
            if (eventId > 0)
            {
                SpecialEvent currentObjectData = localspecialEvents.Where(t => t.EventId == eventId).FirstOrDefault();
                specialEventForm = new FormSpecialEvents(specialEventType, ref localspecialEvents, scheduleType, currentObjectData);
                if (specialEventType.Equals("Date"))
                {
                    specialEventForm.StartDate = ((DateEvent)currentObjectData).StartDate;
                    specialEventForm.EndDate = dateTimePickerEndDate.Value;
                }
                else if (specialEventType.Equals("Date Range"))
                {
                    specialEventForm.StartDate = ((DateRangeEvent)currentObjectData).StartDate;
                    specialEventForm.EndDate = ((DateRangeEvent)currentObjectData).EndDate;
                }
                else
                {
                    specialEventForm.StartDate = dateTimePickerStartDate.Value;
                    specialEventForm.EndDate = dateTimePickerEndDate.Value;
                }


            }
            else
            {
                specialEventForm = new FormSpecialEvents(specialEventType, ref localspecialEvents, scheduleType);
                specialEventForm.StartDate = dateTimePickerStartDate.Value;
                specialEventForm.EndDate = dateTimePickerEndDate.Value;
            }

            int centerX = this.Left + (this.Width + 350) / 2;
            int centerY = this.Top + (this.Height - specialEventForm.Height) / 2;

            specialEventForm.StartPosition = FormStartPosition.Manual;
            specialEventForm.Location = new System.Drawing.Point(centerX, centerY);

            DialogResult result = specialEventForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataGridViewSpecialEvents.Rows.Clear();
                UpdateInEventGrid(localspecialEvents);
            }
        }

        private void UpdateInEventGrid(List<SpecialEvent> Events)
        {
            if ((schedule.EventParticularDays != null && schedule.EventParticularDays.Count > 0) ||
                eventParticularDay != null && eventParticularDay.Count > 0)
            {
                dataGridViewSpecialEvents.Rows.Add("Weekly", 1, "Weekly Event");
            }
            int specialEventsCount = 0;
            if (Events != null)
            {
                foreach (SpecialEvent specialEvent in Events.OrderBy(t => t.Priority))
                {
                    AddInEventsGridView(specialEvent);
                    specialEventsCount++;
                }
            }
            this.comboBoxEventType.SelectedIndexChanged -= comboBoxEventType_SelectedIndexChanged;
            this.textBoxTotalNoOfEvent.Text = specialEventsCount.ToString();
            this.comboBoxEventType.SelectedIndexChanged += comboBoxEventType_SelectedIndexChanged;
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
                case "Calendar Reference":
                    var calendarRef = (CalendarReference)specialEvent;
                    startDate = calendarRef.CalendarInstaceNumber.ToString();
                    break;
            }
            dataGridViewSpecialEvents.Rows.Add(eventType, eventId, eventName, startDate, endDate);
        }

        private void DataGridViewSpecialEvents_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.Button == MouseButtons.Right)
            {
                dataGridViewSpecialEvents.ClearSelection();
                dataGridViewSpecialEvents.Rows[e.RowIndex].Selected = true;
                priorityContextMenu.Show(Cursor.Position);
            }
        }
        private void DataGridViewSpecialEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            else if (e.ColumnIndex == dataGridViewSpecialEvents.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                ScheduleEditClick(e.RowIndex);

            }
            else if (e.ColumnIndex == dataGridViewSpecialEvents.Columns["Remove"].Index && e.RowIndex >= 0)
            {
                ScheduleRemoveClick(e.RowIndex);
            }
        }
        private void MoveUpItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewSpecialEvents.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridViewSpecialEvents.SelectedCells[0].RowIndex;
                if (rowIndex > 0)
                {
                    SwapEvents(rowIndex, rowIndex - 1);
                }
            }
        }

        private void MoveDownItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewSpecialEvents.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridViewSpecialEvents.SelectedCells[0].RowIndex;
                if (rowIndex < dataGridViewSpecialEvents.Rows.Count - 1)
                {
                    SwapEvents(rowIndex, rowIndex + 1);
                }
            }
        }

        private void SwapEvents(int firstIndex, int secondIndex)
        {
            int firstEventId = Convert.ToInt32(dataGridViewSpecialEvents.Rows[firstIndex].Cells[1].Value);
            int secondEventId = Convert.ToInt32(dataGridViewSpecialEvents.Rows[secondIndex].Cells[1].Value);

            SpecialEvent firstEvent = localspecialEvents.FirstOrDefault(e => e.EventId == firstEventId);
            SpecialEvent secondEvent = localspecialEvents.FirstOrDefault(e => e.EventId == secondEventId);

            if (firstEvent != null && secondEvent != null)
            {
                int firstPriority = firstEvent.Priority;
                int secondPriority = secondEvent.Priority;

                DataGridViewRow row1 = dataGridViewSpecialEvents.Rows[firstIndex];
                DataGridViewRow row2 = dataGridViewSpecialEvents.Rows[secondIndex];

                object[] tempValues = new object[row1.Cells.Count];
                for (int i = 0; i < row1.Cells.Count; i++)
                {
                    tempValues[i] = row1.Cells[i].Value;
                    row1.Cells[i].Value = row2.Cells[i].Value;
                    row2.Cells[i].Value = tempValues[i];
                }

                firstEvent.Priority = secondPriority;
                secondEvent.Priority = firstPriority;

                if (schedule.specialEvents != null)
                {
                    SpecialEvent firstScheduleEvent = schedule.specialEvents.FirstOrDefault(e => e.EventId == firstEventId);
                    SpecialEvent secondScheduleEvent = schedule.specialEvents.FirstOrDefault(e => e.EventId == secondEventId);

                    if (firstScheduleEvent != null && secondScheduleEvent != null)
                    {
                        firstScheduleEvent.Priority = secondPriority;
                        secondScheduleEvent.Priority = firstPriority;
                    }
                }

                dataGridViewSpecialEvents.Refresh();
                BacNetValidator.ControlChanged(null, null);
            }
        }
        private void ScheduleEditClick(int rowIndex)
        {
            string eventType = dataGridViewSpecialEvents.Rows[rowIndex].Cells[0]?.Value?.ToString();
            int eventName = Convert.ToInt32(dataGridViewSpecialEvents.Rows[rowIndex].Cells[1]?.Value?.ToString());

            if (string.IsNullOrEmpty(eventType)) return;

            switch (eventType)
            {
                case "Weekly":
                    OpenWeeklySheduleTypeForm();
                    break;
                case "Date":
                case "Date Range":
                case "Week And Day":
                case "Calendar Reference":
                    OpenSpecialEventTypeForm(eventType, eventName, true);
                    break;
            }
        }

        private void ScheduleRemoveClick(int rowIndex)
        {
            string eventName = dataGridViewSpecialEvents.Rows[rowIndex].Cells[0]?.Value?.ToString();
            int eventId = Convert.ToInt32(dataGridViewSpecialEvents.Rows[rowIndex].Cells[1]?.Value?.ToString());
            if (string.IsNullOrEmpty(eventName)) return;

            DialogResult result = MessageBox.Show($"You want to remove {eventName} from {schedule.ObjectName}", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                switch (eventName)
                {
                    case "Weekly":
                        if (schedule.EventParticularDays != null && schedule.EventParticularDays.Count > 0)
                            schedule.EventParticularDays.Clear();
                        eventParticularDay.Clear();
                        break;
                    case "Date":
                    case "Date Range":
                    case "Week And Day":
                    case "Calendar Reference":
                        if (schedule.specialEvents != null && schedule.specialEvents.Count > 0)
                            schedule.specialEvents.RemoveAll(t => t.EventId == eventId);
                        localspecialEvents.RemoveAll(t => t.EventId == eventId);
                        RemoveAndReorderEvents(eventId);
                        break;
                }
                dataGridViewSpecialEvents.Rows.RemoveAt(rowIndex);
                dataGridViewSpecialEvents.Rows.Clear();
                UpdateInEventGrid(localspecialEvents);
                BacNetValidator.ControlChanged(null, null);
            }

        }
        private void RemoveAndReorderEvents(int eventId)
        {
            if (schedule.specialEvents != null)
            {
                schedule.specialEvents.RemoveAll(t => t.EventId == eventId);
            }
            if (localspecialEvents != null)
            {
                localspecialEvents.RemoveAll(t => t.EventId == eventId);
                this.textBoxTotalNoOfEvent.Text = localspecialEvents.Count.ToString();
            }
        }

        private void DateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            //adding next day in End Date 
            DateTime selectedStartDate = dateTimePickerStartDate.Value;
            DateTime endDate = dateTimePickerEndDate.Value;
            if (endDate.Date < selectedStartDate.Date)
            {
                DateTime nextDay = selectedStartDate.AddDays(1);
                dateTimePickerEndDate.Value = nextDay;
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
        //        if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text.Length >= 25)
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

        private void textBoxObjectName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //for checking length
            e.Cancel = false;
            errorProvider.SetError(textBoxObjectName, "");
            string text = textBoxObjectName?.Text?.Trim() ?? string.Empty;

            // Check if text is empty or whitespace
            if (string.IsNullOrWhiteSpace(text))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxObjectName, "Tag name cannot be empty or whitespace.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(textBoxObjectName.Text) && textBoxObjectName.Text.Length > 25)
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxObjectName, "Please resolve the errors first");
            }
            if (textBoxObjectName.Text == "")
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxObjectName, "Object Name is required");
            }
            if (!string.IsNullOrWhiteSpace(textBoxObjectName.Text.Trim()) && textBoxObjectName.Text.Trim().Any(ch => !(char.IsLetterOrDigit(ch) || ch == '_' || ch == ' ')))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxObjectName, "Only letters, digits, underscore (_) and spaces are allowed.");
            }
            if (char.IsDigit(textBoxObjectName.Text[0]))
            {
                e.Cancel = true;
                errorProvider.SetError(textBoxObjectName, "Tag name cannot start with a number.");
                return;
            }
            foreach (char ch in textBoxObjectName.Text.Trim())
            {
                if (!char.IsLetterOrDigit(ch) && ch != 95 && ch != 3 && ch != 22) // Allowed characters
                {
                    e.Cancel = true;
                    errorProvider.SetError(textBoxObjectName, "Invalid character detected. Only letters, digits, and underscore (_) are allowed.");
                    return;
                }
            }
        }

        private void checkBoxAny_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAny.Checked)
            {
                DateTime currentDate = DateTime.Now;
                DateTime endDateAny = currentDate.AddYears(5);
                dateTimePickerStartDate.Value = DateTime.Now;
                dateTimePickerEndDate.Value = endDateAny;
                dateTimePickerStartDate.Enabled = false;
                dateTimePickerEndDate.Enabled = false;
            }
            else
            {
                dateTimePickerStartDate.Enabled = true;
                dateTimePickerEndDate.Enabled = true;
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

        //private void textBoxDescription_Validating(object sender, CancelEventArgs e)
        //{
        //    //for checking length
        //    if (!string.IsNullOrWhiteSpace(textBoxDescription.Text) && textBoxDescription.Text.Length > 25)
        //    {
        //        e.Cancel = true;
        //        errorProvider.SetError(textBoxDescription, "Please resolve the errors first");
        //    }
        //}

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
        private void textObjectName_Validating(object sender, CancelEventArgs e)
        {
            string scheduleValue = comboBoxScheduleValue.Text.Trim();
            string input = textObjectName.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                errorProvider.SetError(textObjectName, "");
                return;
            }
            if (scheduleValue.Equals("Numeric", StringComparison.OrdinalIgnoreCase))
            {
                if (!double.TryParse(textObjectName.Text, out var value))
                {
                    errorProvider.SetError(textObjectName, "Enter a valid number.");
                    e.Cancel = true;
                    return;
                }
                if (value < -2147483648 || value > 2147483647)
                {
                    errorProvider.SetError(textObjectName, "Value must be between -2,147,483,648 and 2,147,483,647.");
                    e.Cancel = true;
                    return;
                }
                errorProvider.SetError(textObjectName, "");
            }
            else if (scheduleValue.Equals("Boolean", StringComparison.OrdinalIgnoreCase))
            {
                if (input != "0" && input != "1")
                {
                    errorProvider.SetError(textObjectName, "Only 0 or 1 is allowed for Boolean.");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(textObjectName, "");
                }
            }
            else
            {
                errorProvider.SetError(textObjectName, "");
            }
        }
    }
}
