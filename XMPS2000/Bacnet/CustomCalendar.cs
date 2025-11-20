using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;

namespace XMPS2000.Bacnet
{
    public partial class CustomCalendar : Form
    {
        private List<DataGridViewCell> selectedCells = new List<DataGridViewCell>();
        public HashSet<string> selectedDays = new HashSet<string>();
        public List<EventParticularDay> eventParticularDays;
        private List<EventParticularDay> prevSelectedDayData;
        private int selectedRowIndex = -1;
        private TimeSpan originalStartTime;
        private TimeSpan originalEndTime;
        private bool isSelectedDaysGrid = false;
        private List<SelectedDayData> selectedDayDataList;
        private string currentScheduleType;

        List<Rectangle> selectedRangeRectangles = new List<Rectangle>();
        public CustomCalendar(List<EventParticularDay> particularDays, string vscheduletype)
        {
            InitializeComponent();
            InitializeCalendarGridView();
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                     System.Reflection.BindingFlags.NonPublic |
                     System.Reflection.BindingFlags.Instance |
                     System.Reflection.BindingFlags.SetProperty,
                    null, gridViewCalendar, new object[] { true });

            ShowSaveAndEditOptions(false);
            selectedDayDataList = new List<SelectedDayData>();
            prevSelectedDayData = particularDays;
            currentScheduleType = vscheduletype;
            this.Shown += (sender, e) => ShowSelectedDayOnCalendar(particularDays);
            comboBoxEventValue.Visible = vscheduletype == "Boolean" ? true : false;
            textBoxValue.Visible = vscheduletype == "Boolean" ? false : true;
            textBoxValue.Text = "0";
        }
        private void ShowSelectedDayOnCalendar(List<EventParticularDay> particularDays)
        {
            if (particularDays != null)
            {
                foreach (EventParticularDay dayEvent in particularDays)
                {
                    string day = dayEvent.DayName;
                    int columnIndex = GetColumnIndexFromDay(day);
                    if (columnIndex == -1)
                    {
                        continue;
                    }

                    foreach (var eventValue in dayEvent.EventValues.OrderBy(t => t.StartTime))
                    {
                        TimeSpan startTime = TimeSpan.ParseExact(eventValue.StartTime, @"hh\:mm\:ss", null);
                        TimeSpan endTime = TimeSpan.ParseExact(eventValue.EndTime, @"hh\:mm\:ss", null);
                        if (endTime == TimeSpan.Zero)
                        {
                            endTime = new TimeSpan(23, 59, 59);
                        }
                        int startHour = startTime.Hours;
                        int endHour = endTime.Hours;
                        int endMinutes = endTime.Minutes;
                        int endSeconds = endTime.Seconds;
                        bool isMultiDaySelected = (endHour - startHour > 1);
                        int startRowIndex = -1;
                        int endRowIndex = -1;

                        // Variables for creating rectangle
                        int topRow = -1;
                        int bottomRow = -1;
                        // Check if endTime has any minutes or seconds
                        if (endMinutes > 0 || endSeconds > 0)
                        {
                            endRowIndex = endHour;
                            bottomRow = endRowIndex;
                        }
                        else
                        {
                            endRowIndex = endHour - 1;
                            bottomRow = endRowIndex;
                        }


                        foreach (DataGridViewRow row in gridViewCalendar.Rows)
                        {
                            TimeSpan rowTime = TimeSpan.ParseExact(row.Cells[0].Value.ToString().Split('-')[0], @"hh\:mm", null);
                            int rowHour = rowTime.Hours;

                            if (rowHour == startHour && startRowIndex == -1)
                            {
                                startRowIndex = row.Index;
                                topRow = row.Index;
                            }
                            //if (rowHour == endHour)
                            //{
                            //    endRowIndex = row.Index;
                            //    bottomRow = row.Index; // Set the bottom row for rectangle
                            //}

                            if (rowHour >= startHour && (rowHour < endHour || endRowIndex == 23))
                            {
                                row.Cells[columnIndex].Style.BackColor = Color.Yellow;
                                selectedCells.Add(row.Cells[columnIndex]);
                            }
                            else if (rowHour == startHour && rowHour == endHour)
                            {
                                row.Cells[columnIndex].Style.BackColor = Color.Yellow;
                            }
                        }
                        int selectedDayRowIndex = datagridSelectedDays.Rows.Count;
                        SelectedDayData selectedDayData =new SelectedDayData(day, startTime.ToString(@"hh\:mm"), endTime.ToString(@"hh\:mm"), columnIndex, startRowIndex, endRowIndex, isMultiDaySelected, selectedDayRowIndex);
                        selectedDayDataList.Add(selectedDayData);

                        // Create and store rectangle for the selection
                        if (topRow != -1 && bottomRow != -1)
                        {
                            Rectangle selectionRectangle = new Rectangle(
                                gridViewCalendar.GetCellDisplayRectangle(columnIndex, topRow, true).Left,
                                gridViewCalendar.GetCellDisplayRectangle(columnIndex, topRow, true).Top,
                                gridViewCalendar.GetCellDisplayRectangle(columnIndex, bottomRow, true).Right - gridViewCalendar.GetCellDisplayRectangle(columnIndex, topRow, true).Left,
                                gridViewCalendar.GetCellDisplayRectangle(columnIndex, bottomRow, true).Bottom - gridViewCalendar.GetCellDisplayRectangle(columnIndex, topRow, true).Top
                            );

                            //Add new rectangle in Rectangle list.
                            selectedRangeRectangles.Add(selectionRectangle);
                            // Add the selection to the DataGridView for display
                            datagridSelectedDays.Rows.Add(day, startTime.ToString(@"hh\:mm\:ss"), endTime.ToString(@"hh\:mm\:ss"), eventValue.Value, selectionRectangle, selectedDayData);
                        }
                    }
                }
                //repainting the grid with rectangle border.
                gridViewCalendar.Invalidate();
            }
        }
        private int GetColumnIndexFromDay(string day)
        {
            var dayToIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
                              { { "sunday", 1 }, { "monday", 2 }, { "tuesday", 3 }, { "wednesday", 4 },
                                { "thursday", 5 },{ "friday", 6 }, { "saturday", 7 }};

            if (dayToIndex.TryGetValue(day, out int index))
            {
                return index;
            }

            return -1;
        }
        private void InitializeCalendarGridView()
        {
            //Set up the DataGridView
            gridViewCalendar.ColumnCount = 8;
            gridViewCalendar.Columns[0].Name = "Time";
            gridViewCalendar.Columns[1].Name = "Sunday";
            gridViewCalendar.Columns[2].Name = "Monday";
            gridViewCalendar.Columns[3].Name = "Tuesday";
            gridViewCalendar.Columns[4].Name = "Wednesday";
            gridViewCalendar.Columns[5].Name = "Thursday";
            gridViewCalendar.Columns[6].Name = "Friday";
            gridViewCalendar.Columns[7].Name = "Saturday";

            gridViewCalendar.Columns[0].Width = 80;
            gridViewCalendar.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 1; i < 8; i++)
            {
                gridViewCalendar.Columns[i].Width = 80;
                gridViewCalendar.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in gridViewCalendar.Columns)
            {
                column.HeaderCell.Style.Font = new Font(gridViewCalendar.Font, FontStyle.Bold);
            }
            for (int hour = 0; hour < 24; hour++)
            {
                TimeSpan startTime = new TimeSpan(hour, 0, 0);
                TimeSpan endTime = startTime.Add(new TimeSpan(1, 0, 0));

                string formattedTime = $"{startTime.ToString(@"hh\:mm")}-{endTime.ToString(@"hh\:mm")}";
                gridViewCalendar.Rows.Add(formattedTime);
            }
            foreach (DataGridViewRow row in gridViewCalendar.Rows)
            {
                row.Cells[0].Style.BackColor = Color.YellowGreen;
            }
        }
        private void ResetMaxAndMinDateRange()
        {
            //StartTimePicker.MinDate = DateTimePicker.MinimumDateTime;
            //StartTimePicker.MaxDate = DateTimePicker.MaximumDateTime;
            //EndTimePicker.MinDate = DateTimePicker.MinimumDateTime;
            //EndTimePicker.MaxDate = DateTimePicker.MaximumDateTime;
        }
        private void gridViewCalendar_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridViewSelectedCellCollection selectedCells = gridViewCalendar.SelectedCells;
                DataGridViewCell firstCell = selectedCells[0];
                (string day, string time) firstCellData = GetDataForCheck(firstCell);


                if (selectedDayDataList.Count > 0 && firstCellData != (null, null))
                {
                    SelectedDayData selectedDayData = GetParticularDayEvent(firstCellData.day, firstCell);

                    if (selectedDayData != null)
                    {
                        bool isAlreadySelected = CheckIsAlreadyAddInEvent(selectedDayData, firstCellData.time, firstCell.ColumnIndex);
                        if (isAlreadySelected)
                            return;
                    }
                    if (selectedDayDataList.Count(t => t.Day.Equals(firstCellData.day)) >= 10 && (XMPS.Instance.PlcModel.Equals("XBLD-14E") || XMPS.Instance.PlcModel.Equals("XBLD-17E")))
                    {
                        MessageBox.Show("Maximum limit for event on per day reached", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (selectedCells.Count > 0)
                {
                    // Process each column separately
                    var groupedByColumns = selectedCells.Cast<DataGridViewCell>().GroupBy(cell => cell.ColumnIndex)
                                                        .OrderBy(group => group.Key);
                    //if cells selected from Time slot column.
                    if (groupedByColumns.Any(t => t.Key == 0))
                    {
                        MessageBox.Show($"Invalid time slots selection", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    foreach (var columnGroup in groupedByColumns)
                    {
                        // Get the start and end row for this column
                        int startRow = columnGroup.Min(cell => cell.RowIndex);
                        int endRow = columnGroup.Max(cell => cell.RowIndex);
                        int columnIndex = columnGroup.Key;

                        string dayName = gridViewCalendar.Columns[columnIndex].Name;
                        string startTime = gridViewCalendar.Rows[startRow].Cells[0].Value.ToString().Split('-')[0];
                        string endTime = gridViewCalendar.Rows[endRow].Cells[0].Value.ToString().Split('-')[0];
                        TimeSpan calculateEndTime = TimeSpan.Parse(endTime);
                        TimeSpan endTimeSpan = calculateEndTime.Add(new TimeSpan(1, 0, 0));
                        //for end time is selected is 23:00 - 00:00
                        if (endTimeSpan.Days == 1)
                        {
                            endTimeSpan = new TimeSpan(23, 59, 59);
                        }
                        string correctEndTime = endTimeSpan.ToString(@"hh\:mm");
                        // Calculate the bounding rectangle for this column
                        Rectangle topLeftCellRect = gridViewCalendar.GetCellDisplayRectangle(columnIndex, startRow, true);
                        Rectangle bottomRightCellRect = gridViewCalendar.GetCellDisplayRectangle(columnIndex, endRow, true);

                        // Create the rectangle around the selected cells in this column
                        Rectangle selectionRectangle = new Rectangle(
                            topLeftCellRect.Left,
                            topLeftCellRect.Top,
                            bottomRightCellRect.Right - topLeftCellRect.Left,
                            bottomRightCellRect.Bottom - topLeftCellRect.Top);

                        // Check if this rectangle overlaps with any existing rectangles
                        bool isOverlap = selectedRangeRectangles.Any(existingRect => selectionRectangle.IntersectsWith(existingRect));

                        if (isOverlap)
                        {
                            MessageBox.Show($"Time slot already selected on the event of {dayName}", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        DialogResult result = MessageBox.Show($"Do you want to create an event on {dayName} from {startTime} to {correctEndTime}?",
                                                              "XMPS2000", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            // Add the event for this column
                            var existingEntry = selectedDayDataList
                                .FirstOrDefault(data => data.Day == dayName && data.StartTime == startTime && data.EndTime == correctEndTime && data.ColumnIndex == columnIndex);

                            if (existingEntry == null)
                            {
                                int selectedDayRowIndex = datagridSelectedDays.Rows.Count;
                                // Add the new selection
                                bool isMultiDaySelected = (endRow - startRow) > 0;
                                SelectedDayData selectedDayData = new SelectedDayData(dayName, startTime, correctEndTime, columnIndex, startRow, endRow, isMultiDaySelected, selectedDayRowIndex);
                                selectedDayDataList.Add(selectedDayData);
                                // Update the selected days DataGridView
                                datagridSelectedDays.Rows.Add(dayName, startTime, correctEndTime, string.Empty, selectionRectangle, selectedDayData);

                                // Set the background color to yellow for the selected range in this column
                                for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
                                {
                                    gridViewCalendar.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Yellow;
                                }

                                // add rectangle in Rectangle List.
                                selectedRangeRectangles.Add(selectionRectangle);
                            }
                        }
                    }

                    //redrawing the grid with new rectangle border accross the selection of cells.
                    gridViewCalendar.Invalidate();
                }
            }
        }

        private SelectedDayData GetParticularDayEvent(string day, DataGridViewCell gridViewCell)
        {
            var particularDays = selectedDayDataList
                .Where(t => t.Day.Equals(day) && t.ColumnIndex == gridViewCell.ColumnIndex)
                .ToList();

            var singleTimeDay = particularDays
                .Where(t => t.StartRowIndex == gridViewCell.RowIndex && t.EndRowIndex == gridViewCell.RowIndex)
                .FirstOrDefault();

            if (singleTimeDay != null)
            {
                return singleTimeDay;
            }
            else
            {
                var multiSlotDay = particularDays
                    .Where(t => t.StartRowIndex <= gridViewCell.RowIndex && t.EndRowIndex >= gridViewCell.RowIndex)
                    .FirstOrDefault();

                return multiSlotDay;
            }
        }
        private bool CheckIsAlreadyAddInEvent(SelectedDayData selectedDayData, string time, int columnIndex)
        {
            TimeSpan selectedTime = TimeSpan.Parse(time);
            TimeSpan startTime = TimeSpan.Parse(selectedDayData.StartTime);
            TimeSpan endTime = TimeSpan.Parse(selectedDayData.EndTime);

            int selectedDayGridRowIndex = selectedDayData.SelectedDayGridRowIndex;

            // Check if the selected time falls within the event time range
            if (selectedTime.Hours >= startTime.Hours && selectedTime.Hours <= endTime.Hours)
            {
                // shown an message to remove selected cells from the side grid view.
                DialogResult result = MessageBox.Show(
                    $"For remove the event on {selectedDayData.Day} from {selectedDayData.StartTime} to {selectedDayData.EndTime} please click on delete button",
                    "XMPS2000",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        private Rectangle FindRectangleForSelectedRange(int columnIndex, int startRowIndex, int endRowIndex)
        {
            foreach (Rectangle rect in selectedRangeRectangles)
            {
                // Get the rectangle bounds for the selected cells
                Rectangle cellRectangle = gridViewCalendar.GetCellDisplayRectangle(columnIndex, startRowIndex, true);
                Rectangle endCellRectangle = gridViewCalendar.GetCellDisplayRectangle(columnIndex, endRowIndex, true);

                if (rect.Left == cellRectangle.Left &&
                    rect.Top == cellRectangle.Top &&
                    rect.Right == endCellRectangle.Right &&
                    rect.Bottom == endCellRectangle.Bottom)
                {
                    return rect;
                }
            }

            return Rectangle.Empty;
        }

        private (string day, string time) GetDataForCheck(DataGridViewCell firstCell)
        {
            int dayColumnIndex = firstCell.ColumnIndex;
            int timeRowIndex = firstCell.RowIndex;

            if (dayColumnIndex > 0)
            {
                string dayName = gridViewCalendar.Columns[dayColumnIndex].Name;
                string time = gridViewCalendar.Rows[timeRowIndex].Cells[0].Value.ToString().Split('-')[0];

                return (dayName, time);
            }
            return (null, null);
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex != -1)
            {
                TimeSpan newStartTime = new TimeSpan(StartTimePicker.Value.Hour, StartTimePicker.Value.Minute, StartTimePicker.Value.Second);
                TimeSpan newEndTime = new TimeSpan(EndTimePicker.Value.Hour, EndTimePicker.Value.Minute, EndTimePicker.Value.Second);
                string newValue = textBoxValue.Visible ? textBoxValue.Text.ToString() : comboBoxEventValue.Text.Equals("True") ? "1" : "0";
                string selectedDay = textBoxSelectedDays.Text;

                if (newEndTime <= newStartTime)
                {
                    MessageBox.Show("End time cannot be less than or equal to start time.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataGridViewRow selectedRow = datagridSelectedDays.Rows[selectedRowIndex];
                SelectedDayData selectedDayData = selectedDayDataList.Where(t => t.SelectedDayGridRowIndex == selectedRowIndex).FirstOrDefault();
                Rectangle newRectangle = new Rectangle();
                if (selectedDayData != null)
                {
                    bool isStartTimeChanged = newStartTime.Hours != TimeSpan.ParseExact(selectedDayData.StartTime, @"hh\:mm", null).Hours;
                    bool isEndTimeChanged = newEndTime.Hours != TimeSpan.ParseExact(selectedDayData.EndTime, @"hh\:mm", null).Hours;
                    if (isStartTimeChanged || isEndTimeChanged)
                    {
                        // Remove the rectangle associated with the selected cells
                        Rectangle selectedRectangle = FindRectangleForSelectedRange(
                            selectedDayData.ColumnIndex,
                            selectedDayData.StartRowIndex,
                            selectedDayData.EndRowIndex);

                        if (selectedRectangle != Rectangle.Empty)
                        {
                            selectedRangeRectangles.Remove(selectedRectangle);
                        }
                        // Clear old selection form Calendar Grid
                        for (int i = selectedDayData.StartRowIndex; i <= selectedDayData.EndRowIndex; i++)
                        {
                            gridViewCalendar.Rows[i].Cells[selectedDayData.ColumnIndex].Style.BackColor = Color.White;
                        }

                        // Update the times in the selectedDayData
                        selectedDayData.StartTime = newStartTime.ToString(@"hh\:mm");
                        selectedDayData.EndTime = newEndTime.ToString(@"hh\:mm");

                        // Calculate new row indices based on new times
                        int startRowIndex = -1;
                        int endRowIndex = -1;
                        foreach (DataGridViewRow row in gridViewCalendar.Rows)
                        {
                            TimeSpan rowTime = TimeSpan.ParseExact(row.Cells[0].Value.ToString().Split('-')[0], @"hh\:mm", null);
                            int rowHour = rowTime.Hours;

                            if (rowHour == newStartTime.Hours)
                            {
                                startRowIndex = row.Index;
                            }
                            if (rowHour == newEndTime.Hours)
                            {
                                endRowIndex = row.Index;
                            }
                        }
                        // Apply new selection on calendar grid
                        for (int i = startRowIndex; i <= endRowIndex; i++)
                        {
                            if ((i >= startRowIndex && (i < endRowIndex || endRowIndex == 23)) || (startRowIndex == endRowIndex))
                                gridViewCalendar.Rows[i].Cells[selectedDayData.ColumnIndex].Style.BackColor = Color.Yellow;
                        }
                        endRowIndex = (newEndTime.Minutes > 0 || newEndTime.Seconds > 0) ? endRowIndex : endRowIndex - 1;
                        selectedDayData.StartRowIndex = startRowIndex;
                        selectedDayData.EndRowIndex = endRowIndex;
                        // Get the display rectangles for the first and last cells in the range
                        Rectangle startCellRectangle = gridViewCalendar.GetCellDisplayRectangle(selectedDayData.ColumnIndex, startRowIndex, true);
                        Rectangle endCellRectangle = gridViewCalendar.GetCellDisplayRectangle(selectedDayData.ColumnIndex, endRowIndex, true);

                        // Creating new Rectangle as per changed start or end time from selectedDay grid.
                         newRectangle = new Rectangle(
                            startCellRectangle.Left,
                            startCellRectangle.Top,
                            startCellRectangle.Width,
                            endCellRectangle.Bottom - startCellRectangle.Top
                        );

                        // Add the new rectangle to the list of rectangles.
                        selectedRangeRectangles.Add(newRectangle);
                    }
                }
                UpdateSelectedRowInGridView(selectedRowIndex, newStartTime, newEndTime, newValue, newRectangle, selectedDayData);
                BacNetValidator.ControlChanged(null, null);
            }
        }
        private void ShowSaveAndEditOptions(bool value)
        {
            comboBoxEventValue.Enabled = value;
            StartTimePicker.Enabled = value;
            EndTimePicker.Enabled = value;         
            textBoxSelectedDays.Enabled = value;
            btnsave.Enabled = value;
        }
        private void CustomCalendar_Load(object sender, EventArgs e)
        {
            StartTimePicker.Format = DateTimePickerFormat.Custom;
            StartTimePicker.CustomFormat = "HH:mm:ss";
            StartTimePicker.ShowUpDown = true;
            StartTimePicker.Value = DateTime.Today;

            EndTimePicker.Format = DateTimePickerFormat.Custom;
            EndTimePicker.CustomFormat = "HH:mm:ss";
            EndTimePicker.ShowUpDown = true;
            EndTimePicker.Value = DateTime.Today;
        }
        private void datagridSelectedDays_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 6)
                {
                    // Get the rectangle data from the hidden column
                    Rectangle rectangleToDelete = (Rectangle)datagridSelectedDays.Rows[e.RowIndex].Cells["RectangleData"].Value;

                    // Remove the rectangle from the selectedRangeRectangles list
                    selectedRangeRectangles.Remove(rectangleToDelete);


                    SelectedDayData selectedDay = (SelectedDayData)datagridSelectedDays.Rows[e.RowIndex].Cells["SelectedData"].Value;

                    //TimeSpan selectedTime = TimeSpan.Parse(time);
                    TimeSpan startTime = TimeSpan.Parse(selectedDay.StartTime);
                    TimeSpan endTime = TimeSpan.Parse(selectedDay.EndTime);

                    int selectedDayGridRowIndex = selectedDay.SelectedDayGridRowIndex;

                    if (startTime.Hours >= startTime.Hours && endTime.Hours <= endTime.Hours)
                    {
                        // Change the background color of the cells in the range to default color
                        for (int rowIndex = selectedDay.StartRowIndex; rowIndex <= selectedDay.EndRowIndex; rowIndex++)
                        {
                            gridViewCalendar.Rows[rowIndex].Cells[selectedDay.ColumnIndex].Style.BackColor = Color.White;
                        }
                        datagridSelectedDays.Rows.RemoveAt(selectedDayGridRowIndex);

                        // Remove the data from selectedDayDataList
                        selectedDayDataList.Remove(selectedDay);

                        var remainingSelectedDays = selectedDayDataList.Where(t => t.SelectedDayGridRowIndex > selectedDayGridRowIndex).ToList();
                        //change the selectedDaysRowIndex from selectedDayDataList
                        foreach (SelectedDayData dayData in remainingSelectedDays)
                        {
                            dayData.SelectedDayGridRowIndex = selectedDayGridRowIndex;
                            selectedDayGridRowIndex++;
                        }
                    }
                    //datagridselectedDays selected row.
                    selectedRowIndex = -1;
                    gridViewCalendar.Invalidate();

                    return;
                }

                ShowSaveAndEditOptions(true);
                isSelectedDaysGrid = true;
                //datagridselectedDays selected row.
                selectedRowIndex = e.RowIndex;
                ResetMaxAndMinDateRange();
                string day = datagridSelectedDays.Rows[e.RowIndex].Cells["Day"].Value.ToString();
                DataGridViewRow row = datagridSelectedDays.Rows[e.RowIndex];

                string startTimeValue = row.Cells["StartDate"].Value.ToString();
                string endTimeValue = row.Cells["EndDate"].Value.ToString();

                if (startTimeValue.Length == 5)
                {
                    originalStartTime = TimeSpan.ParseExact(startTimeValue, @"hh\:mm", null);
                }
                else
                {
                    originalStartTime = TimeSpan.ParseExact(startTimeValue, @"hh\:mm\:ss", null);
                }
                if (endTimeValue.Length == 5)
                {
                    originalEndTime = TimeSpan.ParseExact(endTimeValue, @"hh\:mm", null);
                }
                else
                {
                    originalEndTime = TimeSpan.ParseExact(endTimeValue, @"hh\:mm\:ss", null);
                }

                if (row.Cells["Value"].Value != null)
                {
                    if (currentScheduleType != "Boolean")
                        textBoxValue.Text = row.Cells["Value"].Value.ToString() == null ? string.Empty : row.Cells["Value"].Value.ToString();
                    else
                        comboBoxEventValue.SelectedIndex = row.Cells["Value"].Value.ToString().Equals("1") ? 0 : 1;
                }
                if (originalEndTime == TimeSpan.Zero)
                {
                    originalEndTime = new TimeSpan(23, 59, 59);
                }
                DateTime dummyDate = DateTime.Today;
                //StartTimePicker.MinDate = dummyDate.Add(originalStartTime);
                //StartTimePicker.MaxDate = dummyDate.Add(originalEndTime).AddSeconds(-1);
                //EndTimePicker.MinDate = dummyDate.Add(originalStartTime).AddSeconds(1);
                //EndTimePicker.MaxDate = dummyDate.Add(originalEndTime);

                StartTimePicker.Value = dummyDate.Add(originalStartTime);
                EndTimePicker.Value = dummyDate.Add(originalEndTime);

                textBoxSelectedDays.Text = day;
            }
        }
        private void UpdateSelectedRowInGridView(int rowIndex, TimeSpan newStartTime, TimeSpan newEndTime, string newValue,Rectangle rectangle, SelectedDayData selectedDayData)
        {
            if (rowIndex >= 0 && rowIndex < datagridSelectedDays.Rows.Count)
            {
                DataGridViewRow row = datagridSelectedDays.Rows[rowIndex];
                row.Cells["StartDate"].Value = newStartTime.ToString(@"hh\:mm\:ss");
                row.Cells["EndDate"].Value = newEndTime.ToString(@"hh\:mm\:ss");
                row.Cells["Value"].Value = newValue;
                row.Cells["RectangleData"].Value = rectangle;
                row.Cells["SelectedData"].Value = selectedDayData;
            }
            gridViewCalendar.Invalidate();
        }
        private void StartTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (isSelectedDaysGrid)
            {
                DateTime baseDate = DateTime.Today;
                DateTime selectedStartTime = StartTimePicker.Value;
                TimeSpan selectedTimeSpan = selectedStartTime.TimeOfDay;

                if (selectedTimeSpan > originalEndTime.Add(TimeSpan.FromSeconds(-1)))
                {
                    MessageBox.Show("Start time must be within the original one-hour range.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    StartTimePicker.Value = baseDate.Add(originalStartTime);
                }
            }
        }
        private void EndTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (isSelectedDaysGrid)
            {
                DateTime baseDate = DateTime.Today;
                DateTime selectedEndTime = EndTimePicker.Value;
                TimeSpan selectedTimeSpan = selectedEndTime.TimeOfDay;

                //if (selectedTimeSpan < originalStartTime.Add(TimeSpan.FromSeconds(1)) || selectedTimeSpan > originalEndTime)
                //{
                //    MessageBox.Show("End time must be within the original one-hour range.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    //EndTimePicker.Value = baseDate.Add(originalEndTime);
                //}
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You Want Save All Data for Selected Date", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                eventParticularDays = new List<EventParticularDay>();
                string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };


                foreach (string day in daysOfWeek)
                {
                    var particularDayCount = GetParticularDayFromGrid(day).ToList();
                    int count = particularDayCount.Count;

                    if (count > 0)
                    {
                        EventParticularDay eventParticularDay = new EventParticularDay
                        {
                            NoOfEventParticularDay = count,
                            DayName = day
                        };

                        foreach (DataGridViewRow row in particularDayCount)
                        {
                            string startTimeValue = row.Cells["StartDate"].Value.ToString();
                            string endTimeValue = row.Cells["EndDate"].Value.ToString();
                            TimeSpan startTime, endTime;
                            if (startTimeValue.Length == 5)
                            {
                                startTime = TimeSpan.ParseExact(startTimeValue + ":00", @"hh\:mm\:ss", null);
                            }
                            else
                            {
                                startTime = TimeSpan.ParseExact(startTimeValue, @"hh\:mm\:ss", null);
                            }
                            if (endTimeValue.Length == 5)
                            {
                                endTime = TimeSpan.ParseExact(endTimeValue + ":00", @"hh\:mm\:ss", null);
                            }
                            else
                            {
                                endTime = TimeSpan.ParseExact(endTimeValue, @"hh\:mm\:ss", null);
                            }
                            double value = 0;

                            if (row.Cells["Value"].Value != null &&
                                double.TryParse(row.Cells["Value"].Value.ToString(), out var parsedValue))
                            {
                                value = parsedValue;
                            }

                            if (startTime.Hours == 23 && endTime.Hours == 0)
                            {
                                endTime = new TimeSpan(23, 59, 59);
                            }

                            EventValue eventValue = new EventValue
                            {
                                StartTime = startTime.ToString(@"hh\:mm\:ss"),
                                EndTime = endTime.ToString(@"hh\:mm\:ss"),
                                Value = value
                            };
                            eventParticularDay.EventValues.Add(eventValue);
                        }
                        eventParticularDays.Add(eventParticularDay);
                        selectedDays.Add(day);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                eventParticularDays = prevSelectedDayData;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int rowIndex = datagridSelectedDays.Rows.Count - 1; rowIndex >= 0; rowIndex--)
            {
                var row = datagridSelectedDays.Rows[rowIndex];

                if (row != null &&
                    row.Cells.Count > 3 &&
                    row.Cells[3].Value != null)
                {
                    string value = row.Cells[3].Value.ToString();

                    if (value == "" && value != "0")
                    {
                        if (row.Cells["RectangleData"].Value is Rectangle rectangleToDelete)
                        {
                            selectedRangeRectangles.Remove(rectangleToDelete);
                        }

                        if (row.Cells["SelectedData"].Value is SelectedDayData selectedDay)
                        {
                            selectedDayDataList.Remove(selectedDay);

                            for (int i = selectedDay.StartRowIndex; i <= selectedDay.EndRowIndex; i++)
                            {
                                var cell = gridViewCalendar.Rows[i].Cells[selectedDay.ColumnIndex];
                                cell.Style.BackColor = Color.White;
                                cell.Selected = false;
                            }
                        }
                        datagridSelectedDays.Rows.RemoveAt(rowIndex);
                    }
                }
            }

            gridViewCalendar.ClearSelection();
            gridViewCalendar.Invalidate();

        }

        private List<DataGridViewRow> GetParticularDayFromGrid(string day)
        {
            return datagridSelectedDays.Rows.Cast<DataGridViewRow>()
                        .Where(row => row.Cells["Day"].Value.ToString() == day)
                        .ToList();
        }

        private void textBoxValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateBacNetInput(textBoxValue, e, "Real");
        }
        /// <summary>
        /// Validate operands depending on the type of control 
        /// </summary>
        /// <param name="control">Name of the control from whoes validate this call is generated.</param>
        /// <param name="e">This parameter will specify whether to add or update the line.</param>
        /// <returns>Describe return value.</returns>
        private void ValidateBacNetInput(TextBox control, CancelEventArgs e, string dataType)
        {
            bool validationSuccessful = true;
            string error = string.Empty;
            if (!string.IsNullOrEmpty(control.Text))
                validationSuccessful = BacNetValidator.ValidateBacNetInputVal(control.Text, out error, dataType);
            e.Cancel = !validationSuccessful;
            errorProvider.SetError(control, validationSuccessful ? null : error);
        }

        private void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '-' || textBoxValue.Text.Contains("-")) && (e.KeyChar != '.' || textBoxValue.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private void gridViewCalendar_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Perform the usual painting for cells
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

            // Draw all stored rectangles
            if (selectedRangeRectangles.Count > 0)
            {
                using (Pen pen = new Pen(Color.Black, 3))
                {
                    foreach (var rect in selectedRangeRectangles)
                    {
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                }
            }

            e.Handled = true; // Tell the grid that the cell painting is handled
        }
    }
    public class SelectedDayData
    {
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int ColumnIndex { get; set; }
        public int StartRowIndex { get; set; }
        public int EndRowIndex { get; set; }
        public bool MultiDaysSelected { get; set; }
        public int SelectedDayGridRowIndex { get; set; }

        public SelectedDayData(string day, string startTime, string endTime, int columnIndex, int startRowIndex, int endRowIndex, bool multidaySelected, int selectedDayRowIndex)
        {
            Day = day;
            StartTime = startTime;
            EndTime = endTime;
            ColumnIndex = columnIndex;
            StartRowIndex = startRowIndex;
            EndRowIndex = endRowIndex;
            MultiDaysSelected = multidaySelected;
            SelectedDayGridRowIndex = selectedDayRowIndex;
        }
    }
}

