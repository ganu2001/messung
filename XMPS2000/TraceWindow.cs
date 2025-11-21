using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.LadderLogic;
using static XMPS2000.TraceConfiguration;

namespace XMPS2000
{
    public partial class TraceWindow : Form, IXMForm
    {
        XMPS xm;
        private Chart chart;
        private DataGridView variableDataGridView;
        public List<VariableSettings> variableSettingsList = new List<VariableSettings>();
        public Dictionary<string, NodeSettings> nodeSettings = new Dictionary<string, NodeSettings>();
        private List<XMIOConfig> tags = new List<XMIOConfig>();
        Dictionary<string, string> _AddressValues = new Dictionary<string, string>();
        private Timer timer;
        private double time = 1;
        private Button btnPause;
        private HScrollBar hScrollBar;
        bool autoScaleX = false;
        bool autoScaleY = false;
        double xMin = 0, xMax = 0, yMin = 0, yMax = 0, YFix = 10, XFix = 10, YMin = 0, YMaxInterval = 0;
        private bool isPaused = false;
        public bool isOKbutton = false;
        private bool isPauseResumeBtnClik = false;

        public TraceWindow()
        {
            InitializeComponent();
            xm = XMPS.Instance;
            InitializeChart();
            InitializeDataGridView();
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        public void OnShown()
        {
        }
        private void InitializeChart()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1;
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Dock = System.Windows.Forms.DockStyle.None;
            chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorGrid.LineWidth = 1;
            chartArea1.AxisX.MajorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.LineColor = Color.LightGray;
            chartArea1.AxisX.MinorGrid.LineWidth = 1;
            chartArea1.AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Maximum = 10D;
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.MajorTickMark.Enabled = true;
            chartArea1.AxisX.MinorTickMark.Enabled = true;
            chartArea1.AxisX.MajorGrid.Interval = 1D;
            chartArea1.AxisX.MinorGrid.Interval = 0.2D;
            chartArea1.AxisX.LabelStyle.Format = "0's'";
            chartArea1.AxisX.ArrowStyle = AxisArrowStyle.Triangle;

            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MajorGrid.LineWidth = 1;
            chartArea1.AxisY.MajorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = Color.LightGray;
            chartArea1.AxisY.MinorGrid.LineWidth = 1;
            chartArea1.AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartArea1.AxisY.Minimum = -40D;
            chartArea1.AxisY.Maximum = 40D;
            chartArea1.AxisY.Interval = 10D;
            chartArea1.AxisY.MajorTickMark.Enabled = true;
            chartArea1.AxisY.MinorTickMark.Enabled = true;
            chartArea1.AxisY.MajorGrid.Interval = 10D;
            chartArea1.AxisY.MinorGrid.Interval = 2D;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.ArrowStyle = AxisArrowStyle.Triangle;
            chartArea1.BackColor = Color.LightYellow;
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(840, 600);
            this.chart.TabIndex = 0;
            // 
            // TraceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 600);
            this.Controls.Add(this.chart);
            this.Name = "TraceWindow";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.hScrollBar = new HScrollBar();
            this.hScrollBar.Location = new Point(0, 602);
            this.hScrollBar.Width = 840;
            this.hScrollBar.Minimum = 0;
            this.hScrollBar.Maximum = 100;
            this.hScrollBar.Scroll += new ScrollEventHandler(HScrollBar_Scroll);
            this.Controls.Add(this.hScrollBar);
            this.ResumeLayout(false);
        }

        private void InitializeDataGridView()
        {
            this.variableDataGridView = new DataGridView();
            this.SuspendLayout();
            // 
            // variableDataGridView
            // 
            this.variableDataGridView.RowHeadersVisible = false;
            this.variableDataGridView.ColumnHeadersVisible = false;
            this.variableDataGridView.ReadOnly = true;
            this.variableDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.variableDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.variableDataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "Color", Width = 30},
                new DataGridViewTextBoxColumn { Name = "Variable", Width = 85},
                new DataGridViewTextBoxColumn { Name = "RealTimeValue", Width = 75}
            });
            this.variableDataGridView.ScrollBars = ScrollBars.Both;
            this.variableDataGridView.Location = new Point(842, 70); // Adjust the location as needed
            this.variableDataGridView.Name = "variableDataGridView";
            this.variableDataGridView.Size = new Size(190, 135); // Adjust the size as needed
            this.variableDataGridView.TabIndex = 1;
            this.variableDataGridView.Enabled = true;
            this.variableDataGridView.ReadOnly = true;
            this.variableDataGridView.SelectionChanged += VariableDataGridView_SelectionChanged;
            this.btnPause = new Button();
            this.btnPause.Location = new Point(842, 220);
            this.btnPause.Text = "Pause";
            this.btnPause.Size = new Size(60, 22);
            this.btnPause.Click += BtnPause_Click;
            this.Controls.Add(this.variableDataGridView);
            this.Controls.Add(this.btnPause);
            this.ResumeLayout(false);
        }

        private void VariableDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in variableDataGridView.SelectedCells)
            {
                if (cell.ColumnIndex == variableDataGridView.Columns["Color"].Index)
                {
                    cell.Selected = false;
                }
            }
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            isPaused = btnPause.Text.Equals("Pause") ? true : false;
            isPauseResumeBtnClik = !isPauseResumeBtnClik;
            TogglePauseState();
        }
        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (autoScaleX)
            {
                double viewRange = 11; // This is the range of the X axis you want to view at a time
                double newMin = hScrollBar.Value;
                double newMax = newMin + viewRange;

                if (newMax > newMin)
                {
                    chart.ChartAreas[0].AxisX.Minimum = newMin;
                    chart.ChartAreas[0].AxisX.Maximum = newMax;
                    UpdateDataGridViewWithSeriesPoints(newMin, newMax);
                }
            }
        }
        private void UpdateDataGridViewWithSeriesPoints(double xMin, double xMax)
        {
            variableDataGridView.Rows.Clear();
            foreach (var variable in variableSettingsList)
            {
                var series = chart.Series[variable.VariableName];
                DataGridViewRow row = (DataGridViewRow)variableDataGridView.Rows[0].Clone();
                row.Cells[0].Style.BackColor = Color.FromKnownColor(variable.GroupColor);
                row.Cells[1].Value = variable.VariableName;
                variableDataGridView.Rows.Add(row);
            }
        }
        private void linklblAddVariable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isOKbutton = false;
                isPaused = false;
                TogglePauseState();
                TraceConfiguration trace = new TraceConfiguration(ref variableSettingsList, ref nodeSettings);
                DialogResult status = trace.ShowDialog();
                if (status == DialogResult.OK)
                {
                    isOKbutton = true;
                    variableDataGridView.Rows.Clear();
                    this.chart.Invalidate();
                    UpdateDataGridView();
                    UpdateChartSeries();
                }
                else
                {
                    isOKbutton = true;
                    UpdateDataGridView();
                    UpdateChartSeries();
                }
                //isPaused = true;
                TogglePauseState();
            }
        }
        private void TogglePauseState()
        {
            //btnPause.Text = isPaused ? "Resume" : "Pause";
            btnPause.Text = btnPause.Text.Equals("Pause") ? "Resume" : "Pause";
        }
        private void UpdateDataGridView()
        {
            if (isOKbutton)
            {
                variableDataGridView.Rows.Clear();
                tags.Clear();
                foreach (var variable in variableSettingsList)
                {
                    Color color = Color.FromKnownColor(variable.GroupColor);
                    DataGridViewRow row = (DataGridViewRow)variableDataGridView.Rows[0].Clone();
                    row.Cells[1].Value = variable.VariableName;
                    tags.Add(xm.LoadedProject.Tags.Where(t => t.Tag == variable.VariableName).FirstOrDefault());
                    row.Cells[0].Style.BackColor = color;
                    variableDataGridView.Rows.Add(row);
                    this.variableDataGridView.ClearSelection();
                    this.variableDataGridView.Refresh();
                    this.variableDataGridView.CurrentCell = null;
                }
            }
        }
        private void UpdateChartSeries()
        {
            if (isOKbutton)
            {
                if (nodeSettings.ContainsKey("Time axis (x)"))
                {
                    var xSettings = nodeSettings["Time axis (x)"];
                    autoScaleX = xSettings.Auto;
                    if (!autoScaleX)
                    {
                        xMin = xSettings.Minimum;
                        xMax = xSettings.Maximum;
                        chart.ChartAreas[0].AxisX.Minimum = xMin;
                        chart.ChartAreas[0].AxisX.Maximum = xMax;
                    }
                }
                if (nodeSettings.ContainsKey("Y axis"))
                {
                    var ySettings = nodeSettings["Y axis"];
                    autoScaleY = ySettings.Auto;
                    if (!autoScaleY)
                    {
                        yMin = ySettings.Minimum;
                        yMax = ySettings.Maximum;
                        chart.ChartAreas[0].AxisY.Minimum = yMin;
                        chart.ChartAreas[0].AxisY.Maximum = yMax;
                    }
                }
                //Setting Axix name
                if (nodeSettings.ContainsKey("Time axis (x)") && nodeSettings.ContainsKey("Y axis"))
                {
                    chart.ChartAreas[0].AxisX.Title = nodeSettings["Time axis (x)"].Name;
                    chart.ChartAreas[0].AxisY.Title = nodeSettings["Y axis"].Name;
                }
                //this.chart.Series.Clear();
                //this.chart.Invalidate();
                foreach (var variable in variableSettingsList)
                {
                    var existingSeries = chart.Series.FirstOrDefault(s => s.Name == variable.VariableName);
                    if(existingSeries == null)
                    {
                        Series series = new Series
                        {
                            Name = variable.VariableName,
                            ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), variable.GraphType),
                            Color = Color.FromKnownColor(variable.GroupColor),
                            ChartArea = "ChartArea1",
                            BorderWidth = variable.SeriesWidth
                        };
                        chart.Series.Add(series);
                    }
                    else
                    {
                        var newChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), variable.GraphType);
                        if (existingSeries.ChartType != newChartType)
                        {
                            existingSeries.ChartType = newChartType;
                        }
                        var newColor = Color.FromKnownColor(variable.GroupColor);
                        if (existingSeries.Color != newColor)
                        {
                            existingSeries.Color = newColor;
                        }
                        if (existingSeries.BorderWidth != variable.SeriesWidth)
                        {
                            existingSeries.BorderWidth = variable.SeriesWidth;
                        }
                    }
                }
                foreach (Series s in chart.Series)
                {
                    if (s.ChartType == SeriesChartType.Bar)
                    {
                        s.ChartType = SeriesChartType.Column;
                    }
                }
                this.chart.Invalidate();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckPLCStatus();
            if (isOKbutton)
            {
                try
                {
                    chart.ChartAreas[0].BackColor = Color.LightYellow;
                    tagValueForOnlineMonirtoring(tags);
                    if (isPaused) return;
                    time += 0.1;
                    int i = 1;
                    if (autoScaleX && time > xMax)
                    {
                        chart.ChartAreas[0].AxisX.Minimum += 0.1;
                    }
                    if (autoScaleX)
                    {
                        chart.ChartAreas[0].AxisX.Maximum = time + 0.1;
                        chart.ChartAreas[0].AxisY.Maximum = YFix;
                        if (XFix > 10)
                            chart.ChartAreas[0].AxisX.Minimum = XFix - 10;
                        hScrollBar.Maximum = (int)Math.Ceiling(time);
                        if (time > 10)
                        {
                            hScrollBar.Value = (int)(time - 10);
                        }
                    }
                    else
                    {
                        if (time > xMax)
                        {
                            time = xMax;  // Halt if time exceeds the maximum limit
                        }
                        //isOKbutton = false;
                        TogglePauseState();
                    }
                    foreach (var variable in variableSettingsList)
                    {
                        var series = chart.Series[variable.VariableName];
                        _AddressValues.TryGetValue(variable.VariableName, out string addressValue);
                        double rawValue = Convert.ToDouble(addressValue);

                        // Apply the offset and multiplier for Y-axis (vertical scaling)
                        double yOffset = 0;
                        double yMultiplier = 1;
                        if (nodeSettings.ContainsKey("Y axis"))
                        {
                            yOffset = nodeSettings["Y axis"].Offset;
                            yMultiplier = nodeSettings["Y axis"].Multiplier;
                        }
                        double scaledValue = (rawValue * yMultiplier) + yOffset;
                        foreach (DataGridViewRow row in variableDataGridView.Rows)
                        {
                            if (row.Cells[1].Value.ToString() == variable.VariableName)
                            {
                                row.Cells[2].Value = scaledValue;
                                break;
                            }
                        }
                        if (double.IsInfinity(scaledValue) || Math.Abs(scaledValue) >= 1e9)
                        {
                            continue;
                        }
                        // Apply the offset and multiplier for X-axis (horizontal scaling)
                        double xOffset = 0;
                        double xMultiplier = 1;
                        if (nodeSettings.ContainsKey("Time axis (x)"))
                        {
                            xOffset = nodeSettings["Time axis (x)"].Offset;
                            xMultiplier = nodeSettings["Time axis (x)"].Multiplier;
                        }
                        double scaledTime = (time * xMultiplier) + xOffset;
                        if (!autoScaleX && (scaledTime < xMin || scaledTime > xMax)) continue;
                        if (!autoScaleY && (scaledValue < yMin || scaledValue > yMax)) continue;
                        if (autoScaleY)
                        {
                            if (scaledValue > YFix)
                            {
                                double diff = scaledValue - YFix;
                                YFix += diff + YFix / 8;
                            }
                            if (scaledValue < 0)
                            {
                                if (scaledValue < YMin)
                                {
                                    YMin = scaledValue - (YFix / 10);
                                }
                            }
                            chart.ChartAreas[0].AxisY.Minimum = (int)YMin;
                            chart.ChartAreas[0].AxisY.Maximum = (int)YFix + YFix / 10;
                            chart.ChartAreas[0].AxisY.Interval = (int)(YFix - chart.ChartAreas[0].AxisY.Minimum) / 8;
                            chart.ChartAreas[0].AxisY.MajorGrid.Interval = (int)(YFix - chart.ChartAreas[0].AxisY.Minimum) / 8;
                            YMaxInterval = (int)(YFix - chart.ChartAreas[0].AxisY.Minimum) / 8;
                            chart.ChartAreas[0].AxisY.MinorGrid.Interval = YMaxInterval / 5;
                        }
                        else
                        {
                            chart.ChartAreas[0].AxisY.Interval = (int)yMax / 8;
                            chart.ChartAreas[0].AxisY.MajorGrid.Interval = (int)yMax / 8;
                        }
                        if (autoScaleX)
                        {
                            if (scaledTime > XFix)
                            {
                                double diff = scaledTime - XFix;
                                XFix += diff + 1;
                                xMax = XFix;
                            }
                            chart.ChartAreas[0].AxisX.Maximum = XFix + 0.5;
                        }
                        if (XMPS.Instance.LoadedProject._plcStatus != "PLC is in Stop mode")
                            series.Points.AddXY(scaledTime, scaledValue);
                        i++;
                    }
                    // Refresh the chart
                    chart.Invalidate();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        private void CheckPLCStatus()
        {
            if (XMPS.Instance.LoadedProject._plcStatus == "PLC is in Stop mode")
            {
                isPaused = true;
                btnPause.Text = "Resume";
            }
            else if (XMPS.Instance.LoadedProject._plcStatus == "PLC is in Run Mode" && btnPause.Text.Equals("Resume") && !isPauseResumeBtnClik)
            {
                isPaused = false;
                btnPause.Text = "Pause";
            }
        }
        private void tagValueForOnlineMonirtoring(List<XMIOConfig> tags)
        {
            if (XMPS.Instance.PlcStatus == "LogIn")
            {
                var Tag = tags;
                // 1. activeAddress">List of addresses for online  ---> Pass Logical Address from index to Ending List Element
                List<string> _systemtagsAddress = new List<string>();  //Logical Address
                List<string> _ListTagName = new List<string>();       // Tagname
                List<AddressDataTypes> _Type = new List<AddressDataTypes>();
                OnlineMonitoringHelper omh = OnlineMonitoringHelper.Instance;
                Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddressInfo = new Dictionary<string, Tuple<string, AddressDataTypes>>();

                for (int i = 0; i < Tag.Count; i++)
                {
                    _systemtagsAddress.Add(Tag[i].LogicalAddress.ToString());
                    _ListTagName.Add(Tag[i].Tag.ToString());
                    _Type.Add(omh.GetAddressTypeOf(Tag[i].Label));
                }
                for (int j = 0; j < _systemtagsAddress.Count; j++)
                {
                    CurBlockAddressInfo.Add(_ListTagName[j], Tuple.Create(_systemtagsAddress[j], _Type[j]));
                }
                // 2. name="addressInfoDic">Dictionary with current Logic Block tags ----> (Tagname,Address,Type)
                // 3. AddressValues ----------> tagname ,""
                _AddressValues.Clear();
                foreach (string AddressValue in _ListTagName)
                { _AddressValues.Add(AddressValue, ""); }

                bool isPingOk = XMProValidator.CheckPing();
                if (!isPingOk)
                {
                    ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain")).DisableOnlineModeFromHSIO();
                    return;
                }
                OnlineMonitoring onlineMonitoring = OnlineMonitoring.GetInstance();
                onlineMonitoring.GetValues(_ListTagName, ref CurBlockAddressInfo, ref _AddressValues, out string Result);
            }
        }
    }
}
