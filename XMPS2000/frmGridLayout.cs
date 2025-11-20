using LadderDrawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using XMPS2000.Configuration;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.Types;
using XMPS2000.LadderLogic;
using XMPS2000.UndoRedoGridLayout;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using CheckBox = System.Windows.Forms.CheckBox;
using Control = System.Windows.Forms.Control;
using Panel = System.Windows.Forms.Panel;
using TextBox = System.Windows.Forms.TextBox;
using XMPS2000.Core.App;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace XMPS2000
{
    public partial class frmGridLayout : Form, IXMForm
    {
        public static List<Object> copiedTag;
        public static List<Object> cutTag;
        private string formName = string.Empty;
        private string _filter = string.Empty;
        private XMPS xm = null;
        private int SelectedIndex = 0;
        int prevScrollIndex = 0;
        private int prevSelectedRowIndex = -1;
        //creating DataType List for the Reopen The Filter
        private List<string> currentFilterDataType = new List<string>();
        private List<XMIOConfig> filteredRowsData = new List<XMIOConfig>();

        private bool isdeleteFromBacNet = false;
        //UNDO REDO
        private PublishManager publishManager = new PublishManager();
        private SubscribeManager subscribeManager = new SubscribeManager();
        private ModbusRTUSlaveManager ModbusRTUSlaveManager = new ModbusRTUSlaveManager();
        private ModbusRTUMasterManager ModbusRTUMasterManager = new ModbusRTUMasterManager();
        private ModbusTCPClientManager modbusTCPClientManager = new ModbusTCPClientManager();
        private MODBUSTCPServerManager modbusTCPServerManager = new MODBUSTCPServerManager();
        public frmGridLayout(string formName)
        {
            if (formName.Contains("#"))
            {
                var info = formName.Split('#');
                _filter = info[1];
                formName = info[0];
            }
            this.formName = formName;
            this.xm = XMPS.Instance;
            InitializeComponent();
            splitContainer1.SplitterDistance = 7;
            splitContainer1.Panel1Collapsed = true;
            DataGridViewExtensions.EnableDoubleBuffering(grdMain);
            frmMain.GridDataChanged += OnDataChanged;

        }

        private void OnDataChanged(object sender, EventArgs e)
        {
            OnShown();
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        private void frmGridLayout_Shown(object sender, EventArgs e)
        {
            OnShown();
            omtimer.Stop();

        }

        public async void OnShown()
        {

            //Clearing FilterList;
            if (xm.LoadedProject.ClearFilter)
            {
                currentFilterDataType.Clear();
                filteredRowsData.Clear();
                xm.LoadedProject.ClearFilter = false;
            }
            prevScrollIndex = xm.LoadedProject.NewFocusIndex != 0 ? xm.LoadedProject.NewFocusIndex : prevScrollIndex;
            splitContainer1.Panel1Collapsed = true;
            BindingSource bs = new BindingSource();

            switch (this.formName)
            {
                case "COMDeviceForm":
                    {
                        COMDevice data = (COMDevice)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "COMDevice").FirstOrDefault();
                        List<COMDevice> gridData = new List<COMDevice>();
                        gridData.Add(data);
                        bs.DataSource = gridData;
                        grdMain.DataSource = bs;
                        break;
                    }

                case "EthernetForm":
                    {
                        Ethernet data = (Ethernet)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
                        List<Ethernet> gridData = new List<Ethernet>();
                        gridData.Add(data);
                        if (xm.LoadedProject.PlcModel != null && !xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                            bs.DataSource = gridData.Select(a => new { a.UseDHCPServer, a.EthernetIPAddress, a.EthernetSubNet, a.EthernetGetWay, a.Port, a.ChangeIPAddress, a.ChangeSubNet, a.ChangeGetWay });
                        else
                            bs.DataSource = gridData;
                        grdMain.DataSource = bs;
                        break;
                    }
                case "ModbusRequestForm":
                    {
                        MODBUSTCPServer data = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                        grdMain.DataSource = data.Requests.Where(r => r.Name == _filter).ToList();
                        break;
                    }
                case "LookUpTbl":
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Id", typeof(Guid));
                        dt.Columns.Add("Resistance (Ohm)");
                        dt.Columns.Add("Output Value");
                        if (xm.LoadedProject.ResistanceValues != null)
                        {
                            foreach (var item in xm.LoadedProject.ResistanceValues)
                            {
                                dt.Rows.Add(item.Id, item.Resistance, item.output);
                            }
                        }
                        grdMain.DataSource = dt;
                        grdMain.Columns["Id"].Visible = false;
                        grdMain.ColumnHeadersVisible = true;
                        grdMain.AllowUserToAddRows = false;
                        foreach (DataGridViewColumn column in grdMain.Columns)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        grdMain.CellBeginEdit += (s, ev) =>
                        {
                            if (ev.RowIndex < 2)
                                ev.Cancel = true;
                        };
                        break;
                    }
                case "ResistanceValue":
                    {
                        if (xm == null || xm.SelectedNode == null || xm.LoadedProject == null)
                            break;
                        string name = xm.SelectedNode.Info;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Id", typeof(Guid));
                        dt.Columns.Add("Resistance (Ohm)");
                        dt.Columns.Add("Output Value");

                        //dt.Columns.Add("Name Value");
                        if (xm.LoadedProject.ResistanceValues != null)
                        {
                            foreach (var item in xm.LoadedProject.ResistanceValues.Where(x => x.Name == name))
                            {
                                dt.Rows.Add(item.Id, item.Resistance, item.output);
                            }
                        }
                        grdMain.DataSource = dt;
                        grdMain.Columns["Id"].Visible = false;
                        grdMain.ColumnHeadersVisible = true;
                        grdMain.AllowUserToAddRows = false;
                        foreach (DataGridViewColumn column in grdMain.Columns)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }                     
                        this.BeginInvoke(new Action(() =>
                        {
                            SetFocusAndScrollPosition_Resistance();
                        }));
                        break;
                    }
                case "ModbusTCPSlaveForm":
                    {
                        MODBUSTCPClient data = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                        grdMain.DataSource = data.Slaves.Where(s => s.Name == _filter).ToList();
                        break;
                    }
                case "ModbusRTUSlaveForm":
                    {
                        MODBUSRTUMaster data = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                        grdMain.DataSource = data.Slaves.Where(s => s.Name == _filter).ToList();
                        break;
                    }
                case "MODBUSRTUMasterForm":
                    {
                        MODBUSRTUMaster data = xm.LoadedProject.Devices.OfType<MODBUSRTUMaster>().FirstOrDefault();
                        var slaves = data?.Slaves ?? new List<MODBUSRTUMaster_Slave>();
                        bs.DataSource = slaves.OrderBy(a =>
                        {
                            if (a.Name.StartsWith("MODBUSRTUMasterSlave") && int.TryParse(a.Name.Substring("MODBUSRTUMasterSlave".Length), out int index))
                            {
                                return index;
                            }
                            return 0;
                        }).ToList();
                        foreach (var slave in slaves)
                        {
                            string code = slave.FunctionCode.ToString();
                            if (string.IsNullOrEmpty(slave.MultiplicationFactor) &&
                                (code.Contains("03") || code.Contains("04") || code.Contains("06") || code.Contains("16")))
                            {
                                slave.MultiplicationFactor = "1";
                            }
                        }
                        grdMain.DataSource = bs;

                        string projectName = XMPS.Instance?.LoadedProject?.PlcModel ?? "";
                        if (grdMain.Columns.Contains("MultiplicationFactor"))
                            grdMain.Columns["MultiplicationFactor"].Visible = !projectName.StartsWith("XM", StringComparison.OrdinalIgnoreCase);

                        SetFocusAndScrollPosition();
                        break;
                    }
                case "MODBUSRTUSlavesForm":
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Slave ID", typeof(int));
                        dt.Rows.Add(xm.LoadedProject.SlaveID);
                        bs.DataSource = dt;
                        grdMain.DataSource = bs;
                        grdMain.ColumnHeadersVisible = true;
                        grdMain.RowHeadersVisible = true;
                        grdMain.ReadOnly = false;

                        grdMain.AllowUserToAddRows = false;
                        grdMain.AllowUserToDeleteRows = false;
                        grdMain.AllowUserToResizeRows = false;
                        grdMain.AllowUserToResizeColumns = false;

                        bool canEdit = XMPS.Instance.PlcStatus != "LogIn";

                        foreach (DataGridViewColumn column in grdMain.Columns)
                        {
                            column.ReadOnly = column.Name != "Slave ID" || !canEdit;
                        }
                        if (grdMain.Rows.Count > 0)
                        {
                            grdMain.Rows[0].DefaultCellStyle.Font = new System.Drawing.Font(grdMain.Font, FontStyle.Bold);
                        }
                        grdMain.CellEndEdit += (s, e) =>
                        {
                            if (e.ColumnIndex == grdMain.Columns["Slave ID"].Index)
                            {
                                try
                                {
                                    int newSlaveId = Convert.ToInt32(grdMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                                    if (newSlaveId < 1 || newSlaveId > 247)
                                    {
                                        MessageBox.Show("Slave ID must be between 1 and 247.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        grdMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = xm.LoadedProject.SlaveID;
                                        return;
                                    }
                                    xm.LoadedProject.SlaveID = newSlaveId;
                                }
                                catch
                                {
                                    MessageBox.Show("Please enter a valid numeric Slave ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    grdMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = xm.LoadedProject.SlaveID;
                                }
                            }
                        };
                        grdMain.CellBeginEdit += (s, e) =>
                        {
                            if (XMPS.Instance.PlcStatus == "LogIn")
                            {
                                e.Cancel = true;
                                return;
                            }
                        };

                        break;
                    }
                case "MODBUSTCPClientForm":
                    {
                        MODBUSTCPClient data = (MODBUSTCPClient)xm.LoadedProject.Devices
                            .Where(d => d.GetType().Name == "MODBUSTCPClient")
                            .FirstOrDefault();

                        bs.DataSource = data.Slaves;
                        grdMain.DataSource = data.Slaves
                            .OrderBy(a => int.Parse(a.Name.Substring("MODBUSTCPClientSlave".Length)))
                            .ToList();

                        string projectName = XMPS.Instance?.LoadedProject?.PlcModel ?? "";

                        if (projectName.StartsWith("XM", StringComparison.OrdinalIgnoreCase))
                        {
                            if (grdMain.Columns.Contains("MultiplicationFactor"))
                                grdMain.Columns["MultiplicationFactor"].Visible = false;
                        }
                        else
                        {
                            var slavesWithNullMF = data.Slaves.Where(s => string.IsNullOrEmpty(s.MultiplicationFactor)).ToList();
                            foreach (var slave in slavesWithNullMF)
                            {
                                string code = slave.Functioncode.ToString();
                                if (code.Contains("03") || code.Contains("04") || code.Contains("06") || code.Contains("16"))
                                {
                                    slave.MultiplicationFactor = "1";
                                }
                            }

                            if (grdMain.Columns.Contains("MultiplicationFactor"))
                                grdMain.Columns["MultiplicationFactor"].Visible = true;
                        }

                        SetFocusAndScrollPosition();
                        break;
                    }

                case "MODBUSTCPServerForm":
                    {
                        MODBUSTCPServer data = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                        bs.DataSource = data.Requests;
                        grdMain.DataSource = data.Requests.OrderBy(a => int.Parse(a.Name.Substring("MODBUSTCPServerRequest".Length))).ToList();
                        SetFocusAndScrollPosition();
                        break;
                    }
                case "Mqtt Form":
                    {
                        MQTTForm data = (MQTTForm)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MQTTForm").FirstOrDefault();
                        List<MQTTForm> gridData = new List<MQTTForm>();
                        if (data != null) gridData.Add(data);
                        bs.DataSource = gridData;
                        grdMain.DataSource = bs;
                        break;
                    }
                case "MQTT Publish":
                case "MQTT PublishForm":
                    {
                        List<int> hrows = new List<int>();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("TOPIC");
                        dt.Columns.Add("Qos");
                        dt.Columns.Add("Retain Flag");
                        dt.Columns.Add("Key");

                        var data = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList().OrderBy(d => d.keyvalue);
                        foreach (Publish pubdata in data)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = pubdata.topic.ToString();
                            dr[1] = pubdata.qos.ToString();
                            dr[2] = pubdata.retainflag.ToString();
                            dr[3] = pubdata.keyvalue;
                            dt.Rows.Add(dr);
                            hrows.Add(dt.Rows.Count);
                            int set = 0;
                            foreach (PubRequest pr in pubdata.PubRequest.OrderBy(d => d.Keyvalue))
                            {
                                if (set == 0)
                                {
                                    DataRow dhpr = dt.NewRow();
                                    dhpr[0] = "";
                                    dhpr[1] = "Key Name";
                                    dhpr[2] = "Variable";
                                    dt.Rows.Add(dhpr);
                                    hrows.Add(dt.Rows.Count);
                                    set++;
                                }
                                DataRow dpr = dt.NewRow();
                                dpr[0] = "";
                                dpr[1] = pr.req;
                                string tagName = string.Empty;
                                if (!pr.Tag.Contains(":"))
                                {
                                    tagName = pr.Tag;
                                }
                                else
                                    tagName = XMProValidator.GetTheTagnameFromAddress(pr.Tag);
                                dpr[2] = tagName;
                                dpr[3] = pr.Keyvalue;
                                dt.Rows.Add(dpr);
                            }
                        }
                        grdMain.DataSource = null;

                        grdMain.DataSource = dt;
                        foreach (int rno in hrows)
                        {
                            if (grdMain[0, rno - 1].Value.ToString() != "")
                                grdMain[0, rno - 1].Style.BackColor = System.Drawing.Color.LightSkyBlue;
                            grdMain[1, rno - 1].Style.BackColor = System.Drawing.Color.LightSkyBlue;
                            grdMain[2, rno - 1].Style.BackColor = System.Drawing.Color.LightSkyBlue;
                        }
                        grdMain.Columns["Key"].Visible = false;
                        //set the cursor Position on topic before the last topic.
                        SetFocusAndScrollPosition();

                        grdMain.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                        grdMain.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                        grdMain.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                        break;
                    }
                case "MQTT SubscribeForm":
                    {
                        List<int> hrows = new List<int>();

                        DataTable dt = new DataTable();
                        dt.Columns.Add("TOPIC");
                        dt.Columns.Add("Qos");
                        dt.Columns.Add(" ");
                        dt.Columns.Add("Key");
                        var data = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList().OrderBy(d => d.key);
                        foreach (Subscribe subdata in data)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = subdata.topic.ToString();
                            dr[1] = subdata.qos.ToString();
                            dr[3] = subdata.key;
                            dt.Rows.Add(dr);
                            hrows.Add(dt.Rows.Count);
                            int set = 0;
                            foreach (SubscribeRequest sr in subdata.SubRequest.OrderBy(r => r.key))
                            {
                                if (set == 0)
                                {
                                    DataRow dhpr = dt.NewRow();
                                    dhpr[0] = "";
                                    dhpr[1] = "Key Name";
                                    dhpr[2] = "Variable";
                                    dt.Rows.Add(dhpr);
                                    hrows.Add(dt.Rows.Count);
                                    set++;
                                }

                                DataRow dsr = dt.NewRow();
                                dsr[0] = "";
                                dsr[1] = sr.req;
                                string tagName = string.Empty;
                                if (sr.Tag != null)
                                {
                                    if (!sr.Tag.Contains(":"))
                                    {
                                        tagName = sr.Tag;
                                    }
                                    else
                                        tagName = XMProValidator.GetTheTagnameFromAddress(sr.Tag);
                                }
                                dsr[2] = tagName;
                                dsr[3] = sr.key;
                                dt.Rows.Add(dsr);
                            }
                        }
                        grdMain.DataSource = null;
                        grdMain.DataSource = dt;
                        foreach (int rno in hrows)
                        {
                            if (grdMain[0, rno - 1].Value.ToString() != "")
                                grdMain[0, rno - 1].Style.BackColor = System.Drawing.Color.LightSkyBlue;
                            grdMain[1, rno - 1].Style.BackColor = System.Drawing.Color.LightSkyBlue;
                            grdMain[2, rno - 1].Style.BackColor = System.Drawing.Color.LightSkyBlue;
                        }
                        grdMain.Columns["Key"].Visible = false;
                        //set the cursor Position on topic before the last topic.
                        SetFocusAndScrollPosition();
                        grdMain.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                        grdMain.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                        grdMain.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                        break;
                    }
                case "System Tags":
                case "TagsForm":
                    {
                        var checkColumn = grdMain.Columns.Contains("Online Tag");
                        grdMain.SuspendLayout();
                        //grdMain.AutoGenerateColumns = false;
                        grdMain.AllowUserToOrderColumns = false;
                        grdMain.AllowUserToResizeRows = false;
                        grdMain.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        if (checkColumn)
                            grdMain.Columns.Remove("Online Tag");
                        if (_filter == "System Tags")
                        {
                            List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.Model == null || T.Model == "" && T.LogicalAddress.StartsWith("S3")).ToList();
                            //bs.DataSource = data.Select(e => new { e.LogicalAddress, e.Tag, Type = string.Format("{0}-{1}", e.Type, e.Label), e.InitialValue, e.Retentive, e.RetentiveAddress, e.ShowLogicalAddress }).OrderBy(e => e.LogicalAddress).ToList(); //.Select(d => d.LogicalAddress,d =>d.Tag) ;    // For Adding ShowLogicalAddress Updating Retentive Address
                            bs.DataSource = data.Select(e => new { e.LogicalAddress, e.Tag, DataType = string.Format(e.Label), e.InitialValue, e.Retentive, e.RetentiveAddress, e.ShowLogicalAddress }).OrderBy(e => e.LogicalAddress).ToList(); //.Select(d => d.LogicalAddress,d =>d.Tag) ;    // For Adding ShowLogicalAddress Updating Retentive Address
                            grdMain.DataSource = bs;
                            grdMain.Columns["LogicalAddress"].HeaderText = "LogicalAddress    ˅";
                            grdMain.Columns["Tag"].HeaderText = "Tag    ˅";
                            grdMain.Columns["DataType"].HeaderText = "DataType    ˅";
                            SetFocusAndScrollPosition();
                            if (filteredRowsData.Count > 0)
                            {
                                grdMain.DataSource = null;
                                grdMain.DataSource = filteredRowsData.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).OrderBy(t => t.LogicalAddress).ToList();
                            }
                            else
                            {
                                //for data type drop down
                                if (currentFilterDataType.Count > 0)
                                    ShowFilteredRows(currentFilterDataType);
                            }
                        }

                        else if (_filter == "User Defined Tags")
                        {
                            this.DoubleBuffered = true;

                            typeof(DataGridView).InvokeMember(
                                "DoubleBuffered",
                                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                                null,
                                grdMain,
                                new object[] { true });

                            await DisplayTags();

                            async Task DisplayTags()
                            {
                                List<XMIOConfig> initialData = LoadInitialUserDefinedTags();

                                DisplayUserDefinedTags(initialData);

                                await LoadAndDisplayRemainingUserDefinedTagsAsync();
                            }

                        }
                        else
                        {
                            List<XMIOConfig> data = xm.LoadedProject.Tags.OrderBy(r => r.Model).ThenBy(r => r.LogicalAddress).OrderBy(r => r.Key).ToList();
                            List<XMIOConfig> separatedData = data.Where(r => r.IoList.ToString() == _filter).ToList();
                            XMIOConfig firstElement = separatedData.Count > 0 ? separatedData.FirstOrDefault() : null;
                            if (_filter == "OnBoardIO")
                            {
                                if (firstElement.Model == "XM-14-DT-HIO" && _filter == "OnBoardIO")
                                {
                                    foreach (XMIOConfig tag in separatedData)
                                    {
                                        if (tag.Mode == null && tag.Type == Core.Types.IOType.DigitalInput && (tag.Label == "DI0" || tag.Label == "DI1" || tag.Label == "DI2" || tag.Label == "DI3"))
                                        {
                                            tag.Mode = "Digital Input";
                                        }
                                        else if (tag.Mode == null && tag.Type == Core.Types.IOType.DigitalOutput && (tag.Label == "DO0" || tag.Label == "DO1"))
                                        {
                                            tag.Mode = "Digital Output";
                                        }
                                    }
                                }
                            }
                            var enabledTags = XMPS.Instance.LoadedProject.BacNetIP != null ? new HashSet<string>(
                                XMPS.Instance.LoadedProject.BacNetIP?.AnalogIOValues?.Where(a => a.IsEnable)?.Select(a => a.ObjectName)
                                .Concat(XMPS.Instance.LoadedProject?.BacNetIP?.BinaryIOValues?.Where(b => b.IsEnable)?.Select(b => b.ObjectName))
                                .Concat(XMPS.Instance.LoadedProject?.BacNetIP?.MultistateValues?.Where(m => m.IsEnable)?.Select(m => m.ObjectName))
                                .Concat(XMPS.Instance.LoadedProject?.BacNetIP?.Notifications?.Where(n => n.IsEnable)?.Select(n => n.ObjectName))
                            ) : new HashSet<string>();
                            foreach (var item in xm.LoadedProject.Tags)
                            {
                                string tag = item.Tag;
                                bool BacnetDetails = enabledTags.Contains(tag);
                            }
                            grdMain.DataSource = separatedData.Select(e => new
                            {
                                e.Model,
                                e.Label,
                                e.LogicalAddress,
                                e.Tag,
                                e.IoList,
                                e.Type,
                                //DataType = e.Label,  // New DataType column
                                DataType = GetDataTypeForIO(e, xm.LoadedProject.CPUDatatype), // Modified to use project's CPU datatype
                                e.InitialValue,
                                e.Retentive,
                                e.RetentiveAddress,
                                e.ShowLogicalAddress,
                                DigitalFilter = (e.IsEnableInputFilter ? "☑ " + e.InpuFilterValue : string.Empty),
                                e.Mode,
                                BacnetDetails = enabledTags.Contains(e.Tag)

                            })
                             .Where(r => r.IoList.ToString() == _filter).ToList();
                            //bool isUniversalOutputOnly = separatedData.All(e => e.Type == Core.Types.IOType.UniversalOutput);

                            if (grdMain.RowCount == 0)
                            {
                                grdMain.DataSource = data.Select(e => new
                                {
                                    e.Model,
                                    e.Label,
                                    e.LogicalAddress,
                                    e.Tag,
                                    e.IoList,
                                    e.Type,
                                    DataType = GetDataTypeForIO(e, xm.LoadedProject.CPUDatatype),
                                    e.InitialValue,
                                    e.Retentive,
                                    e.RetentiveAddress,
                                    e.ShowLogicalAddress,
                                    DigitalFilter = (e.IsEnableInputFilter ? "☑ " + e.InpuFilterValue : string.Empty),
                                    e.Mode,
                                    BacnetDetails = enabledTags.Contains(e.Tag)
                                })
                                   .Where(r => r.Model != null && r.Model.ToString() == _filter).ToList();
                                if (grdMain.DataSource is IEnumerable<object> dataSource)
                                {
                                    var firstRow = dataSource.Cast<dynamic>().FirstOrDefault();

                                    if (firstRow != null)
                                    {
                                        string ioListValue = firstRow.IoList?.ToString();

                                        if (grdMain.Columns.Contains("BacnetDetails") && (_filter == "OnBoardIO" || ioListValue == "RemoteIO"))
                                        {
                                            grdMain.Columns["BacnetDetails"].Visible = false;
                                        }
                                    }
                                }
                            }
                            foreach (DataGridViewRow row in grdMain.Rows)
                            {
                                string label = row.Cells[1].Value.ToString();
                                if ((label != null && (label.EndsWith("_OR") || label.EndsWith("_OL"))) && xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                                {
                                    row.Visible = false;
                                }
                            }

                            // Make checkbox symbol look bigger by changing the font of the column
                            grdMain.Columns["DigitalFilter"].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Regular);

                            if (grdMain.FirstDisplayedScrollingRowIndex != -1 && xm.LoadedProject.NewAddedTagIndex > 0)
                            {
                                SetFocusAndScrollPosition();
                            }

                            if (!xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                            {
                                DataGridViewColumn dataGridViewColumn = grdMain.Columns["BacnetDetails"];
                                grdMain.Columns.Remove(dataGridViewColumn);
                            }
                        }
                    }
                    grdMain.ResumeLayout();
                    break;


                case "IOConfigForm":
                    {
                        // IO Config data

                        DataTable Dt = new DataTable();

                        Dt.Columns.Add("ConfigType");
                        Dt.Columns.Add("On-Board IO");
                        Dt.Columns.Add("Expansion");
                        Dt.Columns.Add("Total");

                        DataRow DrDI = Dt.NewRow();
                        DrDI["ConfigType"] = "Digital Input";
                        DrDI["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalInput && d.IoList == IOListType.OnBoardIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrDI["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalInput && d.IoList == IOListType.ExpansionIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrDI["Total"] = Convert.ToInt32(DrDI["On-Board IO"]) + Convert.ToInt32(DrDI["Expansion"]);
                        Dt.Rows.Add(DrDI);
                        DataRow DrDO = Dt.NewRow();
                        DrDO["ConfigType"] = "Digital Output";
                        DrDO["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalOutput && d.IoList == IOListType.OnBoardIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrDO["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.DigitalOutput && d.IoList == IOListType.ExpansionIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrDO["Total"] = Convert.ToInt32(DrDO["On-Board IO"]) + Convert.ToInt32(DrDO["Expansion"]);
                        Dt.Rows.Add(DrDO);
                        DataRow DrAI = Dt.NewRow();
                        DrAI["ConfigType"] = "Analog Input";
                        DrAI["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogInput && d.IoList == IOListType.OnBoardIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrAI["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogInput && d.IoList == IOListType.ExpansionIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrAI["Total"] = Convert.ToInt32(DrAI["On-Board IO"]) + Convert.ToInt32(DrAI["Expansion"]);
                        Dt.Rows.Add(DrAI);
                        DataRow DrAO = Dt.NewRow();
                        DrAO["ConfigType"] = "Analog Output";
                        DrAO["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogOutput && d.IoList == IOListType.OnBoardIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrAO["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.AnalogOutput && d.IoList == IOListType.ExpansionIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrAO["Total"] = Convert.ToInt32(DrAO["On-Board IO"]) + Convert.ToInt32(DrAO["Expansion"]);
                        Dt.Rows.Add(DrAO);
                        //Adding Universal Input
                        DataRow DrUI = Dt.NewRow();
                        DrUI["ConfigType"] = "Universal Input";
                        DrUI["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.UniversalInput && d.IoList == IOListType.OnBoardIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrUI["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.UniversalInput && d.IoList == IOListType.ExpansionIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrUI["Total"] = Convert.ToInt32(DrUI["On-Board IO"]) + Convert.ToInt32(DrUI["Expansion"]);
                        Dt.Rows.Add(DrUI);
                        //Adding Universal Output
                        DataRow DrUO = Dt.NewRow();
                        DrUO["ConfigType"] = "Universal Output";
                        DrUO["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.UniversalOutput && d.IoList == IOListType.OnBoardIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrUO["Expansion"] = xm.LoadedProject.Tags.Where(d => d.Type == IOType.UniversalOutput && d.IoList == IOListType.ExpansionIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrUO["Total"] = Convert.ToInt32(DrUO["On-Board IO"]) + Convert.ToInt32(DrUO["Expansion"]);
                        Dt.Rows.Add(DrUO);
                        DataRow DrTl = Dt.NewRow();
                        DrTl["ConfigType"] = "Total";
                        DrTl["On-Board IO"] = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.OnBoardIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrTl["Expansion"] = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.ExpansionIO && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                        DrTl["Total"] = Convert.ToInt32(DrTl["On-Board IO"]) + Convert.ToInt32(DrTl["Expansion"]);
                        Dt.Rows.Add(DrTl);

                        grdMain.DataSource = Dt;
                        break;
                    }
            }
            if (_filter == "UDFTags")
            {
                var checkColumn = grdMain.Columns.Contains("Online Tag");
                if (checkColumn)
                    grdMain.Columns.Remove("Online Tag");
                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.Model != null && T.Model == formName).ToList();
                bs.DataSource = data.Select(e => new { e.LogicalAddress, e.Tag, DataType = string.Format(e.Label), e.InitialValue, e.Retentive, e.RetentiveAddress, e.ShowLogicalAddress }).OrderBy(e => e.LogicalAddress).ToList(); //.Select(d => d.LogicalAddress,d =>d.Tag) ;    // For Adding ShowLogicalAddress Updating Retentive Address
                grdMain.DataSource = bs;
                grdMain.Columns["LogicalAddress"].HeaderText = "LogicalAddress    ˅";
                grdMain.Columns["Tag"].HeaderText = "Tag    ˅";
                grdMain.Columns["DataType"].HeaderText = "DataType    ˅";

                if (grdMain.Rows.Count > 0 && xm.LoadedProject.NewAddedTagIndex >= 0 && xm.LoadedProject.NewAddedTagIndex < grdMain.Rows.Count)
                {
                    grdMain.CurrentCell = grdMain.Rows[xm.LoadedProject.NewAddedTagIndex].Cells[0];
                    grdMain.FirstDisplayedScrollingRowIndex = xm.LoadedProject.NewAddedTagIndex;
                }
                else if (grdMain.Rows.Count > 0)
                {
                    grdMain.CurrentCell = grdMain.Rows[grdMain.Rows.Count - 1].Cells[0];
                    grdMain.FirstDisplayedScrollingRowIndex = grdMain.Rows.Count - 1;
                }
            }
            ShowTableOfParameters();
            grdMain.AutoResizeColumns();
            grdMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grdMain.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            grdMain.AllowUserToResizeColumns = true;

            if (grdMain.Columns.Contains("Name"))
                grdMain.Columns["Name"].Visible = false;
            if (grdMain.Columns.Contains("IsDeletedRequest"))
                grdMain.Columns["IsDeletedRequest"].Visible = false;
        }




        /// <summary>
        /// Reads an Excel file and converts it to a DataTable using Open XML SDK
        /// First row is treated as column headers
        /// </summary>
        /// <param name="filePath">Path to the Excel file</param>
        /// <param name="sheetName">Optional: Name of the sheet to read. If null, reads first sheet</param>
        /// <returns>DataTable containing the Excel data</returns>
        public static DataTable ExcelToDataTable(string filePath, string sheetName = null)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Excel file not found", filePath);
            }

            DataTable dt = new DataTable();

            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart workbookPart = document.WorkbookPart;

                    // Get the first sheet or specified sheet
                    Sheet sheet;
                    if (string.IsNullOrEmpty(sheetName))
                    {
                        sheet = workbookPart.Workbook.Descendants<Sheet>().First();
                    }
                    else
                    {
                        sheet = workbookPart.Workbook.Descendants<Sheet>()
                            .FirstOrDefault(s => s.Name == sheetName);

                        if (sheet == null)
                        {
                            throw new Exception($"Sheet '{sheetName}' not found");
                        }
                    }

                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                    var rows = sheetData.Elements<DocumentFormat.OpenXml.Spreadsheet.Row>().ToList();
                    if (rows.Count == 0)
                    {
                        return dt;
                    }

                    // First row as headers
                    var headerRow = rows[0];
                    foreach (Cell cell in headerRow.Elements<Cell>())
                    {
                        string headerValue = GetCellValue(cell, workbookPart);
                        dt.Columns.Add(headerValue);
                    }

                    // Data rows
                    for (int i = 1; i < rows.Count; i++)
                    {
                        DataRow dataRow = dt.NewRow();
                        var cells = rows[i].Elements<Cell>().ToList();

                        for (int j = 0; j < cells.Count && j < dt.Columns.Count; j++)
                        {
                            string value = GetCellValue(cells[j], workbookPart);
                            dataRow[j] = value;
                        }

                        dt.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading Excel file: {ex.Message}", ex);
            }

            return dt;
        }

        private static string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell == null || cell.CellValue == null)
            {
                return string.Empty;
            }

            string value = cell.CellValue.InnerText;

            // Handle shared strings
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                SharedStringTablePart stringTablePart = workbookPart.SharedStringTablePart;
                if (stringTablePart != null)
                {
                    value = stringTablePart.SharedStringTable
                        .ElementAt(int.Parse(value)).InnerText;
                }
            }

            return value;
        }
        private void ShowTableOfParameters()
        {
            // First, remove any existing label controls from Panel2
            foreach (Control ctrl in this.splitContainer1.Panel2.Controls.OfType<Label>().ToList())
            {
                this.splitContainer1.Panel2.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            foreach (Control ctrl in this.splitContainer1.Panel2.Controls.OfType<DataGridView>().ToList())
            {
                if (ctrl != grdMain)
                {
                    this.splitContainer1.Panel2.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }
            }

            string filename = this.formName.Replace("Form", "Mapping.xlsx");
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MessungSystems\XMPS2000\" + filename);
            if (!File.Exists(filepath)) return;

            grdMain.Dock = DockStyle.Top;
            grdMain.Height = 50;


            Label tblname = new Label();
            tblname.Text = "Modbus Slave Mapping";
            tblname.BackColor = System.Drawing.Color.LightSteelBlue;
            tblname.Font = new System.Drawing.Font(tblname.Font.FontFamily, 25, FontStyle.Bold);
            tblname.BackColor = System.Drawing.Color.LightSteelBlue;
            tblname.ForeColor = System.Drawing.Color.Black;
            tblname.ForeColor = System.Drawing.Color.Black;
            tblname.Height = 50;
            tblname.AutoSize = true;


            DataGridView mappingGrid = new DataGridView();

            mappingGrid.Top = 50;

            mappingGrid.Dock = DockStyle.Fill;
            mappingGrid.BackgroundColor = System.Drawing.Color.White;
            mappingGrid.BorderStyle = BorderStyle.Fixed3D;
            mappingGrid.AllowUserToAddRows = false;
            mappingGrid.AllowUserToDeleteRows = false;
            mappingGrid.AllowUserToResizeRows = false;
            mappingGrid.ReadOnly = true;
            mappingGrid.RowHeadersVisible = false;
            mappingGrid.ColumnHeadersVisible = true;
            mappingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mappingGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mappingGrid.MultiSelect = false;
            mappingGrid.AllowUserToResizeColumns = false;
            mappingGrid.AllowUserToOrderColumns = false;
            mappingGrid.EnableHeadersVisualStyles = false;
            mappingGrid.DefaultCellStyle.SelectionBackColor = mappingGrid.DefaultCellStyle.BackColor;
            mappingGrid.DefaultCellStyle.SelectionForeColor = mappingGrid.DefaultCellStyle.ForeColor;
            
            DataTable excelData = ExcelToDataTable(filepath);
            for (int c = 0; c < excelData.Columns.Count; c++)
            {
                mappingGrid.Columns.Add(excelData.Columns[c].ToString().Replace(" ", ""), excelData.Columns[c].ToString());
                mappingGrid.Columns[c].Width = 100;
                for (int r = 0; r < excelData.Rows.Count; r++)
                {
                    if (c == 0)
                    {
                        mappingGrid.Rows.Add(excelData.Rows[r].ItemArray[c].ToString());
                    }
                    else
                        mappingGrid.Rows[r].Cells[c].Value = excelData.Rows[r].ItemArray[c].ToString();
                    if (excelData.Rows[r].ItemArray[c].ToString().Contains("\n"))
                    {
                        mappingGrid.Rows[r].Height = 25 * (excelData.Rows[r].ItemArray[c].ToString().Split('\n').Count());
                    }
                }
            }
            mappingGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
            mappingGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(mappingGrid.Font.FontFamily, 10, FontStyle.Bold);
            mappingGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mappingGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            mappingGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            mappingGrid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            mappingGrid.ColumnHeadersHeight = 50;

            mappingGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            mappingGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            mappingGrid.Font = new System.Drawing.Font(mappingGrid.Font.FontFamily, 11, FontStyle.Regular);
            mappingGrid.RowTemplate.Height = 40;

            for (int i = 0; i < mappingGrid.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    mappingGrid.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
                    mappingGrid.Rows[i].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
                }
                else
                {
                    mappingGrid.Rows[i].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
                }
                mappingGrid.Rows[i].DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            }

            mappingGrid.Location = new Point(0, grdMain.Bottom + 45);
            mappingGrid.Width = this.splitContainer1.Panel2.Width;
            mappingGrid.Height = this.splitContainer1.Panel2.Height - grdMain.Height - 5;
            mappingGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AutoResizeDataGridView(mappingGrid);
            tblname.Location = new Point((mappingGrid.Width - 50) - tblname.PreferredWidth, grdMain.Bottom + 5);
            this.splitContainer1.Panel2.Controls.Add(tblname);
            this.splitContainer1.Panel2.Controls.Add(mappingGrid);
            mappingGrid.ClearSelection();
            mappingGrid.CurrentCell = null;

            this.Refresh();
        }

        /// <summary>
        /// Auto-resize DataGridView columns and rows to fit content
        /// </summary>
        /// <param name="gridView">The DataGridView to resize</param>
        /// <param name="mode">Column auto-size mode (default: AllCells)</param>
        public static void AutoResizeDataGridView(DataGridView gridView,
            DataGridViewAutoSizeColumnsMode mode = DataGridViewAutoSizeColumnsMode.AllCells)
        {
            try
            {
                // CRITICAL: Enable text wrapping first (required for multi-line cells)
                gridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                gridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set alignment for better readability
                gridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

                // Disable auto-size modes temporarily for better performance
                gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                gridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                // Auto-size all columns based on content
                gridView.AutoResizeColumns(mode);

                // IMPORTANT: Manually resize each row to handle multi-line text
                foreach (DataGridViewRow row in gridView.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        // Calculate preferred height based on cell content
                        int maxHeight = row.GetPreferredHeight(row.Index,
                            DataGridViewAutoSizeRowMode.AllCells, true);
                        row.Height = maxHeight;
                    }
                }

                // Optional: Set minimum column width for readability
                foreach (DataGridViewColumn column in gridView.Columns)
                {
                    if (column.Width < 150)
                    {
                        column.Width = 150;
                    }
                }

                // Adjust header height to fit multi-line headers
                gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

                // Ensure grid is refreshed
                gridView.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error resizing DataGridView: {ex.Message}", ex);
            }
        }



        public void SetFocusAndScrollPosition_Resistance()
        {
            try
            {
                if (grdMain.Rows.Count == 0)
                    return;

                int focusIndex = xm.LoadedProject.NewAddedTagIndex;
                focusIndex = Math.Min(Math.Max(0, focusIndex), grdMain.Rows.Count - 1);
                this.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        grdMain.ClearSelection();

                        int firstVisibleColumnIndex = GetFirstVisibleColumnIndex();

                        if (focusIndex >= 0 && focusIndex < grdMain.Rows.Count && firstVisibleColumnIndex >= 0)
                        {
                            var row = grdMain.Rows[focusIndex];
                            if (row.Visible)
                            {
                                grdMain.CurrentCell = row.Cells[firstVisibleColumnIndex];
                                row.Selected = true;
                                grdMain.FirstDisplayedScrollingRowIndex = focusIndex;
                            }
                            else
                            {
                                for (int i = focusIndex; i >= 0; i--)
                                {
                                    if (grdMain.Rows[i].Visible)
                                    {
                                        grdMain.CurrentCell = grdMain.Rows[i].Cells[firstVisibleColumnIndex];
                                        grdMain.Rows[i].Selected = true;
                                        grdMain.FirstDisplayedScrollingRowIndex = i;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exInner)
                    {
                        if (grdMain.Rows.Count > 0)
                        {
                            int firstVisibleColumnIndex = GetFirstVisibleColumnIndex();
                            if (firstVisibleColumnIndex >= 0)
                            {
                                grdMain.CurrentCell = grdMain.Rows[0].Cells[firstVisibleColumnIndex];
                                grdMain.Rows[0].Selected = true;
                                grdMain.FirstDisplayedScrollingRowIndex = 0;
                            }
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                if (grdMain.Rows.Count > 0)
                {
                    int firstVisibleColumnIndex = GetFirstVisibleColumnIndex();
                    if (firstVisibleColumnIndex >= 0)
                    {
                        grdMain.CurrentCell = grdMain.Rows[0].Cells[firstVisibleColumnIndex];
                        grdMain.Rows[0].Selected = true;
                        grdMain.FirstDisplayedScrollingRowIndex = 0;
                    }
                }
            }
        }
        private int GetFirstVisibleColumnIndex()
        {
            for (int i = 0; i < grdMain.Columns.Count; i++)
            {
                if (grdMain.Columns[i].Visible)
                    return i;
            }
            return -1;
        }
        private string GetDataTypeForIO(XMIOConfig ioConfig, string cpuDatatype)
        {
            // Check if logical address contains a dot
            if (ioConfig.LogicalAddress.Contains("."))
            {
                return "Bool";
            }
            // For universal outputs, show the configured type
            if (ioConfig.Type == IOType.UniversalOutput || ioConfig.Type == IOType.UniversalInput)
            {
                return cpuDatatype; // Or specific logic for universal outputs
            }
            // For analog/universal inputs, use the project's CPU datatype
            if (ioConfig.Type == IOType.AnalogInput || ioConfig.Type == IOType.AnalogOutput)
            {
                return cpuDatatype;
            }
            // Default case
            return ioConfig.Label;
        }
        private void AddSelectCheckboxColumn()
        {
            const string checkBoxColumnName = "BacNetDetails";
            if (grdMain.Columns.Contains(checkBoxColumnName))
            {
                DataGridViewColumn oldColumn = grdMain.Columns["BacNetDetails"];
                grdMain.Columns.Remove(oldColumn);
            }
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = checkBoxColumnName,
                HeaderText = "BacNetDetails",
                ReadOnly = false,
                TrueValue = true,
                FalseValue = false,
                ThreeState = false
            };
            grdMain.Columns.Insert(grdMain.Columns.Count, checkBoxColumn);
        }
        private void SetCheckboxValues()
        {
            foreach (DataGridViewRow row in grdMain.Rows)
            {
                if (row.IsNewRow) continue;
                string tag = row.Cells["Tag"]?.Value?.ToString();
                if (!string.IsNullOrWhiteSpace(tag))
                {
                    bool isTagEnabled = xm.LoadedProject.BacNetIP != null ?
                        XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Any(a => a.ObjectName == tag && a.IsEnable) ||
                        XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Any(b => b.ObjectName == tag && b.IsEnable) ||
                        XMPS.Instance.LoadedProject.BacNetIP.MultistateValues.Any(m => m.ObjectName == tag && m.IsEnable) ||
                        XMPS.Instance.LoadedProject.BacNetIP.Notifications.Any(n => n.ObjectName == tag && n.IsEnable)
                        : false;
                    if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                        row.Cells["BacNetDetails"].Value = isTagEnabled;
                }
                else
                {
                    if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                        row.Cells["BacNetDetails"].Value = false;
                }
            }
        }
        private void SetFocusAndScrollPosition()
        {
            if (grdMain.Rows.Count == 0)
                return;

            try
            {
                int focusIndex = xm.LoadedProject.NewAddedTagIndex;
                focusIndex = Math.Min(Math.Max(0, focusIndex), grdMain.Rows.Count - 1);

                int targetScrollIndex;

                // If selecting the last row, scroll to it
                if (focusIndex == grdMain.Rows.Count - 1)
                {
                    targetScrollIndex = focusIndex;
                }
                else if (prevScrollIndex == 0 && (xm.CurrentScreen.ToString().Contains("MQTT Publish") || xm.CurrentScreen.ToString().Contains("MQTT Subscribe")))
                {
                    targetScrollIndex = focusIndex;
                }
                else
                {
                    targetScrollIndex = Math.Min(prevScrollIndex, grdMain.Rows.Count - 1);
                }

                this.BeginInvoke(new Action(() =>
                {
                    grdMain.ClearSelection();

                    // Ensure the row and cell are visible before selecting
                    if (grdMain.Rows[focusIndex].Visible && grdMain.Columns[0].Visible)
                    {
                        grdMain.CurrentCell = grdMain.Rows[focusIndex].Cells[0];
                        grdMain.Rows[focusIndex].Selected = true;
                    }

                    grdMain.FirstDisplayedScrollingRowIndex = targetScrollIndex;
                }));
            }
            catch
            {
                if (grdMain.Rows.Count > 0)
                {
                    grdMain.CurrentCell = grdMain.Rows[0].Cells[0];
                    grdMain.Rows[0].Selected = true;
                    grdMain.FirstDisplayedScrollingRowIndex = 0;
                }
            }
        }

        private List<XMIOConfig> LoadInitialUserDefinedTags()
        {
            return xm.LoadedProject.Tags
                .Where(D => !D.LogicalAddress.StartsWith("S3") && D.Model.Equals("") ||
                            (D.Model == "User Defined Tags" && D.IoList.ToString() != "OnBoardIO" &&
                            D.IoList.ToString() != "ExpansionIO" && D.IoList.ToString() != "RemoteIO"))
                .OrderBy(D => D.Key)
                .Take(30)
                .ToList();

        }
        private async Task LoadAndDisplayRemainingUserDefinedTagsAsync()
        {
            var remainingData = await Task.Run(() => xm.LoadedProject.Tags
            .Where(D => !D.LogicalAddress.StartsWith("S3") && D.Model.Equals("") ||
                    (D.Model == "User Defined Tags" && D.IoList.ToString() != "OnBoardIO" &&
                    D.IoList.ToString() != "ExpansionIO" && D.IoList.ToString() != "RemoteIO"))
                   .OrderBy(D => D.Key)
                   .Skip(30)
                   .ToList());

            DisplayUserDefinedTags(remainingData, true);
            ShowingFilterIcons();

            if (grdMain.FirstDisplayedScrollingRowIndex != -1 && xm.LoadedProject.NewAddedTagIndex > 0)
            {
                SetFocusAndScrollPosition();
            }
            else
                SelectedIndex = 0;
            if (grdMain.FirstDisplayedScrollingRowIndex != -1 && SelectedIndex > 0)
                grdMain.CurrentCell = grdMain.Rows[SelectedIndex - 1].Cells[0];
            try
            {
                if (filteredRowsData.Count > 0)
                {
                    grdMain.DataSource = null;
                    grdMain.DataSource = filteredRowsData.OrderBy(t => t.Key).Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                    if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                    {
                        AddSelectCheckboxColumn();
                        SetCheckboxValues();
                    }
                }
                else
                {
                    //for data type drop down
                    if (currentFilterDataType.Count > 0)
                        ShowFilteredRows(currentFilterDataType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void DisplayUserDefinedTags(List<XMIOConfig> data, bool displayAll = false)
        {
            if (grdMain.InvokeRequired)
            {
                grdMain.Invoke(new Action<List<XMIOConfig>, bool>(DisplayUserDefinedTags), data, displayAll);
            }
            else
            {
                var initialData = LoadInitialUserDefinedTags();
                grdMain.DataSource = initialData.Select(e => new { /*e.Model, e.Label,*/ e.LogicalAddress, e.Tag, /*e.IoList,*/ DataType = string.Format(e.Label),/*e.Type*/ e.InitialValue, e.Retentive, e.RetentiveAddress, e.ShowLogicalAddress/*, e.Mode*/ })/*.Where(r => r.Model != null && r.Model.ToString() == _filter)*/.ToList();
                // Add remaining tags to the existing data source
                if (displayAll)
                {
                    var allData = initialData.Concat(data).ToList();
                    grdMain.DataSource = allData.Select(e => new { /*e.Model, e.Label,*/ e.LogicalAddress, e.Tag, /*e.IoList,*/ DataType = string.Format(e.Label),/*e.Type*/ e.InitialValue, e.Retentive, e.RetentiveAddress, e.ShowLogicalAddress/*, e.Mode*/ })/*.Where(r => r.Model != null && r.Model.ToString() == _filter)*/.ToList();

                }
            }
            if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
            {
                AddSelectCheckboxColumn();
                SetCheckboxValues();
            }
        }
        private void grdMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (XMPS.Instance.PlcStatus != "LogIn")
            {
                if (e.RowIndex >= 0)
                {
                    string currentFormName = this.formName.EndsWith("Tags") ? "UDFB Tags" : this.formName;
                    switch (currentFormName)
                    {
                        case "COMDeviceForm":
                            {
                                XMProForm tempForm = new XMProForm();
                                tempForm.StartPosition = FormStartPosition.CenterParent;
                                tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                tempForm.Text = "RS485 Settings";
                                COMSettingsUserControl userControl = new COMSettingsUserControl();
                                tempForm.Height = userControl.Height + 25;
                                tempForm.Width = userControl.Width;
                                tempForm.Controls.Add(userControl);
                                var frmTemp = this.ParentForm as frmMain;

                                DialogResult result = tempForm.ShowDialog(frmTemp);
                                if (result == DialogResult.OK)
                                {
                                    OnShown();
                                }
                                break;
                            }

                        case "EthernetForm":
                            {
                                XMProForm tempForm = new XMProForm();
                                tempForm.StartPosition = FormStartPosition.CenterParent;
                                tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                tempForm.Text = "Ethernet Settings";
                                EthernetSettingsUserControl userControl = new EthernetSettingsUserControl();
                                tempForm.Height = userControl.Height + 25;
                                tempForm.Width = userControl.Width;
                                tempForm.Controls.Add(userControl);
                                var frmTemp = this.ParentForm as frmMain;

                                DialogResult result = tempForm.ShowDialog(frmTemp);
                                if (result == DialogResult.OK)
                                {
                                    OnShown();
                                }
                                break;
                            }
                        case "ResistanceValue":
                            {
                                xm.LoadedProject.NewAddedTagIndex = grdMain.CurrentRow.Index;
                                if (e.RowIndex >= 0 && grdMain != null && grdMain.Rows.Count > e.RowIndex)
                                {
                                    // Find the Resistance column safely
                                    var column1 = grdMain.Columns
                                        .Cast<DataGridViewColumn>()
                                        .FirstOrDefault(c => c.HeaderText.Trim().Equals("Resistance (Ohm)", StringComparison.OrdinalIgnoreCase));
                                    var column2 = grdMain.Columns
                                        .Cast<DataGridViewColumn>()
                                        .FirstOrDefault(c => c.HeaderText.Trim().Equals("Output Value", StringComparison.OrdinalIgnoreCase));
                                    // Get resistance value
                                    string resistanceValue = grdMain.Rows[e.RowIndex].Cells[column1.Index].Value?.ToString() ?? "";
                                    string Output = grdMain.Rows[e.RowIndex].Cells[column2.Index].Value?.ToString() ?? "";
                                    // Open the form directly
                                    int rowIndex = e.RowIndex;
                                    string tableName = xm.SelectedNode?.Info;
                                    frmAddResistanceValue frm = new frmAddResistanceValue(resistanceValue, Output, rowIndex, tableName)
                                    {
                                        StartPosition = FormStartPosition.CenterParent
                                    };

                                    var frmTemp = this.ParentForm as frmMain;
                                    DialogResult result = frm.ShowDialog(frmTemp);

                                    if (result == DialogResult.OK)
                                    {
                                        OnShown();
                                    }
                                }
                                break;
                            }
                        case "Mqtt Form":
                            {
                                XMProForm tempForm = new XMProForm();
                                tempForm.StartPosition = FormStartPosition.CenterParent;
                                tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                tempForm.Text = "MQTT Configuration";
                                MqttSettingsUserControl userControl = new MqttSettingsUserControl();
                                MQTTForm mqttForm = (MQTTForm)xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "MQTTForm");
                                if (mqttForm != null)
                                {
                                    MqttSettingsUserControl userControl1 = new MqttSettingsUserControl(mqttForm);
                                    tempForm.Height = userControl1.Height + 25;
                                    tempForm.Width = userControl1.Width;
                                    tempForm.Controls.Add(userControl1);
                                    var frmTemp = this.ParentForm as frmMain;
                                    DialogResult result = tempForm.ShowDialog(frmTemp);
                                    if (result == DialogResult.OK)
                                    {
                                        OnShown();
                                    }
                                }
                                else
                                {
                                    tempForm.Height = userControl.Height + 25;
                                    tempForm.Width = userControl.Width;
                                    tempForm.Controls.Add(userControl);
                                    var frmTemp = this.ParentForm as frmMain;
                                    DialogResult result = tempForm.ShowDialog(frmTemp);
                                    if (result == DialogResult.OK)
                                    {
                                        OnShown();
                                    }
                                }

                                break;
                            }
                        case "MQTT PublishForm":
                            {
                                xm.LoadedProject.NewAddedTagIndex = grdMain.SelectedRows[0].Index;
                                prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                                XMProForm tempForm = new XMProForm();
                                tempForm.StartPosition = FormStartPosition.CenterParent;

                                int i = grdMain.SelectedRows[0].Index;
                                if (grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString() == "Key Name")
                                {
                                    MessageBox.Show("Invalid Attempt", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                int selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);
                                string seltopic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);

                                if (grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString() == "Key Name") return;
                                if (seltopic == "")
                                {
                                    int cindex = i;
                                    while (seltopic == "")
                                    {
                                        cindex--;
                                        seltopic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                        selkey = seltopic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value) : -1;
                                    }
                                    PubRequest pr = new PubRequest();
                                    var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                                    Publish pb = publist.Where(p => p.keyvalue == selkey).FirstOrDefault();
                                    pr = pb.PubRequest.Where(r => r.Keyvalue == Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value)).FirstOrDefault();
                                    tempForm.StartPosition = FormStartPosition.CenterParent;
                                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                    PublishRequest userControl = new PublishRequest(pr, "", true);
                                    userControl.Text = "Edit Publish Request";
                                    tempForm.Height = userControl.Height + 25;
                                    tempForm.Width = userControl.Width;
                                    DialogResult status = userControl.ShowDialog();
                                    if (status == DialogResult.OK)
                                    {
                                        publishManager.SaveState();
                                        pb.PubRequest.Remove(pr);
                                        pr.topic = selkey;
                                        pr.req = userControl.Keyname;
                                        pr.Tag = userControl.Tagname;
                                        pb.PubRequest.Add(pr);
                                        pb.PubRequest.Sort((s1, s2) => s1.Keyvalue.CompareTo(s2.Keyvalue));

                                    }
                                }
                                else
                                {
                                    var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                                    Publish pb = publist.Where(p => p.keyvalue == selkey).FirstOrDefault();
                                    tempForm.StartPosition = FormStartPosition.CenterParent;
                                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                    PublishParameter userControl = new PublishParameter(pb);
                                    userControl.Text = "Edit Publish Parameters";
                                    tempForm.Height = userControl.Height + 25;
                                    tempForm.Width = userControl.Width;
                                    DialogResult status = userControl.ShowDialog();
                                    xm.LoadedProject.NewAddedTagIndex = e.RowIndex;
                                }
                                OnShown();
                                break;
                            }
                        case "MQTT SubscribeForm":
                            {
                                xm.LoadedProject.NewAddedTagIndex = grdMain.SelectedRows[0].Index;
                                prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                                XMProForm tempForm = new XMProForm();
                                tempForm.StartPosition = FormStartPosition.CenterParent;

                                int i = grdMain.SelectedRows[0].Index;
                                if (grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString() == "Key Name")
                                {
                                    MessageBox.Show("Invalid Attempt", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                int selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);
                                string seltopic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);

                                if (grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString() == "Key Name") return;
                                if (seltopic == "")
                                {
                                    int cindex = i;
                                    while (seltopic == "")
                                    {
                                        cindex--;
                                        seltopic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                        selkey = seltopic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value) : -1;
                                    }
                                    SubscribeRequest sr = new SubscribeRequest();
                                    var sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                                    Subscribe sb = sublist.Where(p => p.key == selkey).FirstOrDefault();
                                    sr = sb.SubRequest.Where(r => r.key == Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value)).FirstOrDefault();
                                    tempForm.StartPosition = FormStartPosition.CenterParent;
                                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                    SuscribeRequest userControl = new SuscribeRequest(sr, "", true);
                                    userControl.Text = "Edit Subscribe Request";
                                    tempForm.Height = userControl.Height + 25;
                                    tempForm.Width = userControl.Width;
                                    DialogResult status = userControl.ShowDialog();
                                    if (status == DialogResult.OK)
                                    {
                                        subscribeManager.SaveState();
                                        sb.SubRequest.Remove(sr);
                                        sr.topic = selkey;
                                        sr.req = userControl.Keyname;
                                        sr.Tag = userControl.Tagname;
                                        sb.SubRequest.Add(sr);
                                        sb.SubRequest.Sort((s1, s2) => s1.key.CompareTo(s2.key));
                                    }
                                }
                                else
                                {
                                    var sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                                    Subscribe sb = sublist.Where(p => p.key == selkey).FirstOrDefault();
                                    tempForm.StartPosition = FormStartPosition.CenterParent;
                                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                    SuscribeParameter userControl = new SuscribeParameter(sb);
                                    userControl.Text = "Edit Subscribe Parameters";
                                    tempForm.Height = userControl.Height + 25;
                                    tempForm.Width = userControl.Width;
                                    DialogResult status = userControl.ShowDialog();
                                    xm.LoadedProject.NewAddedTagIndex = e.RowIndex;
                                }
                                OnShown();
                                break;
                            }
                        case "MODBUSTCPServerForm":
                        case "ModbusRequestForm":
                            {
                                XMProForm tempForm = new XMProForm();
                                tempForm.StartPosition = FormStartPosition.CenterParent;
                                tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                tempForm.Text = "Modbus TCP Server Settings";
                                var reqName = grdMain.Rows[e.RowIndex].Cells[grdMain.Columns["Name"].Index].Value.ToString();
                                ModbusTCPServerUserControl userControl = new ModbusTCPServerUserControl(reqName);
                                tempForm.Height = userControl.Height + 25;
                                tempForm.Width = userControl.Width;
                                tempForm.Controls.Add(userControl);
                                var frmTemp = this.ParentForm as frmMain;

                                DialogResult result = tempForm.ShowDialog(frmTemp);
                                if (result == DialogResult.OK)
                                {
                                    xm.LoadedProject.NewAddedTagIndex = e.RowIndex;
                                    prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                                    OnShown();
                                }
                                break;
                            }
                        case "MODBUSTCPClientForm":
                        case "ModbusTCPSlaveForm":
                            {
                                XMProForm tempForm = new XMProForm();
                                tempForm.StartPosition = FormStartPosition.CenterParent;
                                tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                tempForm.Text = "Modbus TCP Client Settings";
                                var slaveName = grdMain.Rows[e.RowIndex].Cells[grdMain.Columns["Name"].Index].Value.ToString();
                                ModbusTCPClientUserControl userControl = new ModbusTCPClientUserControl(slaveName);
                                tempForm.Height = userControl.Height + 25;
                                tempForm.Width = userControl.Width;
                                tempForm.Controls.Add(userControl);
                                var frmTemp = this.ParentForm as frmMain;

                                DialogResult result = tempForm.ShowDialog(frmTemp);
                                if (result == DialogResult.OK)
                                {
                                    xm.LoadedProject.NewAddedTagIndex = e.RowIndex;
                                    prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                                    OnShown();
                                }
                                break;
                            }
                        case "MODBUSRTUMasterForm":
                        case "ModbusRTUSlaveForm":
                            {
                                XMProForm tempForm = new XMProForm();
                                tempForm.StartPosition = FormStartPosition.CenterParent;
                                tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                                tempForm.Text = "Modbus RTU Master Settings";
                                var slaveName = grdMain.Rows[e.RowIndex].Cells[grdMain.Columns["Name"].Index].Value.ToString();
                                ModbusRTUUserControl userControl = new ModbusRTUUserControl(slaveName);
                                tempForm.Height = userControl.Height + 25;
                                tempForm.Width = userControl.Width;
                                tempForm.Controls.Add(userControl);
                                var frmTemp = this.ParentForm as frmMain;

                                DialogResult result = tempForm.ShowDialog(frmTemp);
                                if (result == DialogResult.OK)
                                {
                                    xm.LoadedProject.NewAddedTagIndex = e.RowIndex;
                                    prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                                    OnShown();
                                }
                                break;
                            }

                        //case "MODBUSRTUSlavesForm":
                        //    {
                        //        XMProForm tempForm = new XMProForm();
                        //        tempForm.StartPosition = FormStartPosition.CenterParent;
                        //        tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        //        tempForm.Text = "Modbus TCP Server Settings";
                        //        var slaveName = grdMain.Rows[e.RowIndex].Cells[grdMain.Columns["Name"].Index].Value.ToString();
                        //        ModbusRTUSlaveUserControl userControl = new ModbusRTUSlaveUserControl(slaveName);
                        //        tempForm.Height = userControl.Height + 25;
                        //        tempForm.Width = userControl.Width;
                        //        tempForm.Controls.Add(userControl);
                        //        var frmTemp = this.ParentForm as frmMain;

                        //        DialogResult result = tempForm.ShowDialog(frmTemp);
                        //        if (result == DialogResult.OK)
                        //        {
                        //            xm.LoadedProject.NewAddedTagIndex = e.RowIndex;
                        //            prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                        //            OnShown();
                        //        }
                        //        break;
                        //    }

                        case "TagsForm":
                        case "System Tags":
                        case "UDFB Tags":
                            {
                                if (XMPS.Instance.PlcStatus != "LogIn")
                                {
                                    string LogicalAddress = grdMain.Rows[e.RowIndex].Cells[grdMain.Columns["LogicalAddress"].Index].Value.ToString();
                                    xm.LoadedProject.NewAddedTagIndex = grdMain.SelectedRows[0].Index;
                                    prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                                    if (LogicalAddress.StartsWith("'"))
                                    {
                                        MessageBox.Show("Selected Tag is Commented Not editable", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    var chktag = (XMIOConfig)xm.LoadedProject.Tags.FirstOrDefault(L => L.LogicalAddress == LogicalAddress);
                                    if (chktag.Label.EndsWith("_OR") || chktag.Label.EndsWith("_OL"))
                                    {
                                        return;
                                    }
                                    if (chktag.IoList == IOListType.Default)
                                    {
                                        if (chktag.Editable == true)
                                        {
                                            OpenTagEditor(e.RowIndex);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Tag selected is Default Tag and Not Editable", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        return;
                                    }
                                    //// UDFB library validation here
                                    string selectedNode = this.formName;
                                    string normalizedNode = selectedNode.Replace(" Tags", "").Trim();
                                    string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MessungSystems", "XMPS2000", "Library");
                                    string librarySubFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD", StringComparison.OrdinalIgnoreCase)
                                        ? "XBLDLibraries"
                                        : "XMLibraries";
                                    string libraryPath = Path.Combine(basePath, librarySubFolder);
                                    string[] csvFiles = Directory.Exists(libraryPath) ? Directory.GetFiles(libraryPath, "*.csv") : Array.Empty<string>();
                                    List<string> udfbNames = XMPS.Instance.LoadedProject.UDFBInfo.Select(u => u.UDFBName).ToList();

                                    var fileNames = csvFiles.Select(Path.GetFileNameWithoutExtension)
                                        .Select(name => name.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                                            ? name.Substring(0, name.Length - 6).Trim()
                                            : name);

                                    bool isUdfbMatch = fileNames.Any(fileName => fileName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase) && udfbNames.Any(udfbName => udfbName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase)));

                                    if (isUdfbMatch)
                                    {
                                        // Check if a local copy with a DIFFERENT name exists
                                        string savedChoice = XMPS.Instance.LoadedProject.GetUDFBChoice(normalizedNode);
                                        string savedLocalCopyName = null;

                                        if (!string.IsNullOrEmpty(savedChoice) && savedChoice.StartsWith("CreateLocalCopy:"))
                                        {
                                            savedLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);
                                        }

                                        // Check if a local copy with a different name exists
                                        bool localCopyWithDifferentNameExists = !string.IsNullOrEmpty(savedLocalCopyName) &&
                                            !savedLocalCopyName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase) &&
                                            XMPS.Instance.LoadedProject.Blocks.Any(b =>
                                                b.Type == "UserFunctionBlock" &&
                                                b.Name.Equals(savedLocalCopyName, StringComparison.OrdinalIgnoreCase));

                                        // If library UDFB exists AND a local copy exists, it means library was re-imported
                                        // In this case, ignore saved choice and show popup
                                        if (localCopyWithDifferentNameExists)
                                        {
                                            // Library re-imported while local copy exists - ask user what to do
                                            using (var optionsForm = new UDFBEditOptionsForm(normalizedNode))
                                            {
                                                if (optionsForm.ShowDialog() == DialogResult.OK)
                                                {
                                                    if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                                    {
                                                        XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "EditMainFile");
                                                        OpenTagEditor(e.RowIndex);
                                                    }
                                                    else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                                    {
                                                        string newLocalCopyName = optionsForm.LocalCopyName;
                                                        XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "CreateLocalCopy:" + newLocalCopyName);
                                                        var frmTemp = this.ParentForm as frmMain;
                                                        frmTemp.CreateAndEditLocalCopy(null, normalizedNode, newLocalCopyName);
                                                    }
                                                }
                                            }
                                            return; // or appropriate exit for this context
                                        }

                                        if (!string.IsNullOrEmpty(savedChoice))
                                        {
                                            if (savedChoice == "EditMainFile")
                                            {
                                                OpenTagEditor(e.RowIndex);
                                            }
                                            else if (savedChoice.StartsWith("CreateLocalCopy:"))
                                            {
                                                // Extract the local copy name from saved choice
                                                string existingLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);

                                                // Check if that specific local copy exists
                                                bool specificLocalCopyExists = XMPS.Instance.LoadedProject.Blocks.Any(b =>
                                                    b.Type == "UserFunctionBlock" &&
                                                    b.Name.Equals(existingLocalCopyName, StringComparison.OrdinalIgnoreCase));

                                                if (specificLocalCopyExists)
                                                {
                                                    // The local copy exists, edit it directly
                                                    OpenTagEditor(e.RowIndex);
                                                }
                                                else
                                                {
                                                    // Local copy doesn't exist anymore, prompt again
                                                    XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "");

                                                    using (var optionsForm = new UDFBEditOptionsForm(normalizedNode))
                                                    {
                                                        if (optionsForm.ShowDialog() == DialogResult.OK)
                                                        {
                                                            if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                                            {
                                                                XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "EditMainFile");
                                                                OpenTagEditor(e.RowIndex);
                                                            }
                                                            else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                                            {
                                                                string recreatedLocalCopyName = optionsForm.LocalCopyName;
                                                                XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "CreateLocalCopy:" + recreatedLocalCopyName);
                                                                var frmTemp = this.ParentForm as frmMain;
                                                                frmTemp.CreateAndEditLocalCopy(null, normalizedNode, recreatedLocalCopyName);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            using (var optionsForm = new UDFBEditOptionsForm(normalizedNode))
                                            {
                                                if (optionsForm.ShowDialog() == DialogResult.OK)
                                                {
                                                    if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                                    {
                                                        XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "EditMainFile");
                                                        OpenTagEditor(e.RowIndex);
                                                    }
                                                    else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                                    {
                                                        string initialLocalCopyName = optionsForm.LocalCopyName;
                                                        XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "CreateLocalCopy:" + initialLocalCopyName);
                                                        var frmTemp = this.ParentForm as frmMain;
                                                        frmTemp.CreateAndEditLocalCopy(null, normalizedNode, initialLocalCopyName);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        OpenTagEditor(e.RowIndex);
                                    }
                                }
                                break;
                            }
                    }
                }
            }

        }
        private void OpenTagEditor(int rowIndex)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "Tags Settings";
            TagsUserControl userControl = new TagsUserControl(grdMain.Rows[rowIndex].Cells[grdMain.Columns["Tag"].Index].Value.ToString());
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK)
            {
                xm.LoadedProject.NewAddedTagIndex = grdMain.SelectedRows[0].Index;
                OnShown();
            }
        }
        public void deleteResistanceValue()
        {
            if (grdMain.SelectedRows.Count == 0)
                return;

            if (IsMainResistanceTable())
            {
                return;
            }
            int rowIndex1 = grdMain.SelectedRows[0].Index;

            if (rowIndex1 < 2)
            {
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete the selected record?",
                                          "Confirm Delete",
                                          MessageBoxButtons.YesNoCancel,
                                          MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;
            prevSelectedRowIndex = grdMain.CurrentCell?.RowIndex ?? 0;
            if (grdMain.DataSource is DataTable dt)
            {
                List<Guid> idsToRemove = new List<Guid>();

                foreach (DataGridViewRow selectedRow in grdMain.SelectedRows)
                {
                    int rowIndex = selectedRow.Index;

                    if (rowIndex < 2)
                        continue;

                    Guid id = (Guid)dt.Rows[rowIndex]["Id"];
                    idsToRemove.Add(id);

                    dt.Rows[rowIndex].Delete();
                }

                dt.AcceptChanges();

                if (xm.LoadedProject.ResistanceValues != null)
                {
                    foreach (var id in idsToRemove)
                    {
                        var toRemove = xm.LoadedProject.ResistanceValues
                            .FirstOrDefault(x => x.Id == id);

                        if (toRemove != null)
                            xm.LoadedProject.ResistanceValues.Remove(toRemove);
                    }
                }

                if (prevSelectedRowIndex >= grdMain.Rows.Count)
                    xm.LoadedProject.NewAddedTagIndex = grdMain.Rows.Count - 1;
                else
                    xm.LoadedProject.NewAddedTagIndex = prevSelectedRowIndex;
                grdMain.DataSource = dt;
                OnShown();
                SetFocusAndScrollPosition_Resistance();
            }
        }
        private bool IsMainResistanceTable()
        {
            return this.Name.Contains("MainResistance");
        }

        private void cntxdelete_Click(object sender, EventArgs e)
        {
            if (xm.CurrentScreen.StartsWith("frmAddResistanceValue") || xm.CurrentScreen.Contains("ResistanceValue"))
            {
                deleteResistanceValue();
            }
            else if (xm.CurrentScreen.StartsWith("LookUpTbl"))
            {
                return;
            }
            else
            {
                deleteTagAndModbus();
            }
        }
        public void DeleteFromBacNetObject(string objectName)
        {
            //this.isdeleteFromBacNet = true;
            //this.formName = "TagsForm";
            //this._filter = "User Defined Tags";
            //OnShown();
            ////check if any prev tag is selected on frmGridLayout
            //grdMain.Rows.Cast<DataGridViewRow>().ToList().ForEach(row => row.Selected = false);
            ////get row which tag is similar to objectName
            //var rowToSelect = grdMain.Rows.Cast<DataGridViewRow>()
            //                .FirstOrDefault(row => row.Cells["Tag"].Value != null && row.Cells["Tag"].Value.ToString() == objectName);
            //if (rowToSelect != null)
            //{
            //    rowToSelect.Selected = true;
            //}
            //deleteTagAndModbus();
            var tagToRemove = xm.LoadedProject.Tags.Where(t => t.Tag == objectName).FirstOrDefault();
            var tag = xm.LoadedProject.Tags.Where(t => t.Tag == objectName).Select(t => t.LogicalAddress).FirstOrDefault();
            DeletedTag(tag, tagToRemove);
            this.isdeleteFromBacNet = false;
        }
        public void deleteTagAndModbus()
        {
            if (xm.CurrentScreen.ToString().Contains("MODBUSRTUSlavesForm") || xm.CurrentScreen.ToString().Contains("LookUpTbl"))
            {
                return;
            }
            if (xm.CurrentScreen.Contains("#") && (xm.CurrentScreen.Split('#')[1].Equals("System Tags") || xm.PlcModels.Contains(xm.CurrentScreen.Split('#')[1].Split('_')[0])
                    || xm.CurrentScreen.Split('#')[1].Equals("OnBoardIO") || xm.CurrentScreen.Split('#')[1].Equals("ExpansionIO") || xm.CurrentScreen.Split('#')[1].Equals("RemoteIO")
                     || xm.CurrentScreen.Split('#')[1].Equals("IOConfig")) || xm.CurrentScreen.Equals("EthernetForm") || xm.CurrentScreen.Equals("COMDeviceForm"))
                return;

            bool isTopic = false;
            List<int> selectedcols = new List<int>();
            List<int> onlyForTopics = new List<int>();
            foreach (DataGridViewRow gr in grdMain.SelectedRows)
            {
                //checking for mqtt
                string topicName = gr.Cells[0].Value?.ToString();
                string qos = gr.Cells[1].Value?.ToString();
                string variable = gr.Cells[2].Value.ToString();
                if (((!string.IsNullOrEmpty(topicName) && (qos == "0" || qos == "1")) || (qos == "Key Name" && variable == "Variable"))
                    && (xm.CurrentScreen.ToString().Contains("MQTT Publish") || xm.CurrentScreen.ToString().Contains("MQTT Subscribe")))
                {
                    isTopic = true;
                    if (!string.IsNullOrEmpty(topicName) && !string.IsNullOrEmpty(gr.Cells[3].Value.ToString()))
                    {
                        onlyForTopics.Add(gr.Index);
                    }
                }
                selectedcols.Add(gr.Index);
            }
            if (isTopic && grdMain.SelectedRows.Count > 1)
            {
                selectedcols.Clear();
                selectedcols.AddRange(onlyForTopics);
            }
            if (xm.CurrentScreen.ToString().Contains("Tags") || isdeleteFromBacNet)
            {
                foreach (int i in selectedcols)
                {
                    var currentSelected = i;
                    var tags = grdMain.Rows[currentSelected].DataBoundItem.ToString().Split(',').First().Split('=').Last().Trim();
                    var tagToRemove = xm.LoadedProject.Tags.Where(x => x.LogicalAddress == tags).FirstOrDefault();
                    DeletedTag(tags, tagToRemove);
                    prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                }
                xm.LoadedProject.NewAddedTagIndex = selectedcols.Count > 0 ? (selectedcols.Min() > 0 ? selectedcols.Min() - 1 : 0) : 0;
            }
            else if (xm.CurrentScreen.ToString().Contains("MQTT Publish"))
            {
                foreach (int i in selectedcols)
                {
                    if (grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString() == "Key Name")
                    {
                        MessageBox.Show("Invalid Attempt", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string topic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                    int key = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);
                    if (topic != "")
                    {
                        List<Block> usedblocks = xm.LoadedProject.Blocks;
                        List<LadderElement> FB = new List<LadderElement>();
                        if (MessageBox.Show("Do you want to delete topic " + topic, "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            bool deleteable = false;
                            FB = CheckIsUsedInLadderLogic(key, "MQTT Publish");
                            if (FB.Count == 0)
                            {
                                deleteable = true;
                            }
                            else
                            {
                                MessageBox.Show("Delete function blocks having topic: " + topic + " Then try again", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                deleteable = false;
                                xm.LoadedProject.NewAddedTagIndex = i;
                            }

                            if (deleteable)
                            {

                                var publist = xm.LoadedProject.Devices.Where(p => p is Publish).Cast<Publish>().ToList();

                                Publish Delpub = publist.FirstOrDefault(n => n.keyvalue == key);

                                if (Delpub != null)
                                {
                                    publist.Remove(Delpub);
                                    xm.LoadedProject.Devices.Remove(Delpub);
                                }
                                publishManager.DeletePublish(Delpub);

                                MessageBox.Show(topic + " deleted sucessfully", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (i > 0)
                                {
                                    int currentIndex = i - 1;
                                    string prevTopic = grdMain.Rows[currentIndex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                    while (prevTopic == "")
                                    {
                                        currentIndex--;
                                        prevTopic = grdMain.Rows[currentIndex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                    }
                                    xm.LoadedProject.NewAddedTagIndex = currentIndex;
                                    prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                                }
                                else
                                {
                                    xm.LoadedProject.NewAddedTagIndex = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        int cindex = i;
                        int tpkkey = Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value);
                        while (topic == "")
                        {
                            cindex--;
                            topic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                            tpkkey = topic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value) : -1;
                        }
                        string keyname = grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString();
                        if (MessageBox.Show("Do you want to delete topic " + topic + " Key name " + keyname, "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            publishManager.SaveState();
                            var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                            Publish Delpub = publist.Where(n => n.keyvalue == tpkkey).FirstOrDefault();
                            Delpub.PubRequest.Remove(Delpub.PubRequest.Where(r => r.Keyvalue == key).FirstOrDefault());
                            xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Publish");
                            xm.LoadedProject.Devices.AddRange(publist);
                            MessageBox.Show(keyname + " deleted sucessfully", "XMPS 2000");
                        }
                        xm.LoadedProject.NewAddedTagIndex = cindex;
                        prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                    }
                }
            }
            else if (xm.CurrentScreen.ToString().Contains("MQTT Subscribe"))
            {
                foreach (int i in selectedcols)
                {
                    if (grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString() == "Key Name")
                    {
                        MessageBox.Show("Invalid Attempt", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string topic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                    int key = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);
                    if (topic != "")
                    {
                        List<Block> usedblocks = xm.LoadedProject.Blocks;
                        List<LadderElement> FB = new List<LadderElement>();
                        if (MessageBox.Show("Do you want to delete topic " + topic, "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            bool deleteable = false;
                            FB = CheckIsUsedInLadderLogic(key, "MQTT Subscribe");
                            if (FB.Count == 0)
                            {
                                deleteable = true;
                            }
                            else
                            {
                                MessageBox.Show("Delete function blocks having topic: " + topic + " Then try again", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                deleteable = false;
                                xm.LoadedProject.NewAddedTagIndex = i;
                            }

                            if (deleteable)
                            {
                                var sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                                Subscribe Delsub = sublist.FirstOrDefault(n => n.key == key);
                                if (Delsub != null)
                                {
                                    sublist.Remove(Delsub);
                                    xm.LoadedProject.Devices.Remove(Delsub);
                                }
                                subscribeManager.DeleteSubscribe(Delsub);

                                MessageBox.Show(topic + " deleted sucessfully", "XMPS 2000");
                                if (i > 0)
                                {
                                    int currentIndex = i - 1;
                                    string prevTopic = grdMain.Rows[currentIndex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                    while (prevTopic == "")
                                    {
                                        currentIndex--;
                                        prevTopic = grdMain.Rows[currentIndex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                    }
                                    xm.LoadedProject.NewAddedTagIndex = currentIndex;
                                    prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                                }
                                else
                                {
                                    xm.LoadedProject.NewAddedTagIndex = 0;
                                }

                            }
                        }
                    }
                    else
                    {
                        int cindex = i;
                        int tpkkey = Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value);
                        while (topic == "")
                        {
                            cindex--;
                            topic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                            tpkkey = topic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value) : -1;
                        }
                        string keyname = grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString();
                        if (MessageBox.Show("Do you want to delete topic " + topic + " Key name " + keyname, "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            subscribeManager.SaveState();
                            var sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                            Subscribe Delsub = sublist.Where(n => n.key == tpkkey).FirstOrDefault();
                            Delsub.SubRequest.Remove(Delsub.SubRequest.Where(r => r.key == key).FirstOrDefault());
                            xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Subscribe");
                            xm.LoadedProject.Devices.AddRange(sublist);
                            MessageBox.Show(keyname + " deleted sucessfully", "XMPS 2000");
                        }
                        xm.LoadedProject.NewAddedTagIndex = cindex;
                        prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                    }
                }
            }
            else if (xm.CurrentScreen.ToString().Contains("Mqtt Form"))
            {
                XMPS.Instance.LoadedProject.Devices.RemoveAll(d => d.GetType().Name == "MQTTForm");
                XMPS.Instance.MarkProjectModified(true);
            }
            else
            {
                if (xm.CurrentScreen.ToString().Contains("MODBUSRTUSlaves"))
                {
                    return;
                }
                if (!grdMain.Columns.Contains("Name"))
                {
                    return;
                }
                int nameindex = grdMain.Columns["Name"].Index;
                foreach (int i in selectedcols)
                {
                    var slaveName = grdMain.Rows[i].Cells[nameindex].Value.ToString();
                    if (slaveName.Contains("ModbusTCPSlave"))
                    {
                        var mainnode = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                        MODBUSTCPClient_Slave rtuslave = new MODBUSTCPClient_Slave();
                        rtuslave = mainnode.Slaves.Where(d => d.Name == slaveName).FirstOrDefault();
                        mainnode.Slaves.Remove(rtuslave);
                        prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                    }
                    else if (slaveName.Contains("MODBUSTCPServer"))
                    {
                        var mainnode = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                        MODBUSTCPServer_Request rtuslave = new MODBUSTCPServer_Request();
                        rtuslave = mainnode.Requests.Where(d => d.Name == slaveName).FirstOrDefault();
                        mainnode.Requests.Remove(rtuslave);
                        rtuslave.IsDeletedRequest = true;
                        modbusTCPServerManager.DeleteMODBUSTCPServerSlave(rtuslave);
                        prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                    }
                    else if (slaveName.Contains("ModbusRequestSlave"))
                    {
                        var mainnode = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                        MODBUSRTUMaster_Slave rtuslave = new MODBUSRTUMaster_Slave();
                        rtuslave = mainnode.Slaves.Where(d => d.Name == slaveName).FirstOrDefault();
                        mainnode.Slaves.Remove(rtuslave);
                    }
                    else if (slaveName.Contains("MODBUSTCPClient"))
                    {
                        var mainnode = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                        MODBUSTCPClient_Slave rtuslave = new MODBUSTCPClient_Slave();
                        rtuslave = mainnode.Slaves.Where(d => d.Name == slaveName).FirstOrDefault();
                        mainnode.Slaves.Remove(rtuslave);
                        rtuslave.IsDeletedRequest = true;
                        modbusTCPClientManager.DeleteMODBUSTCPClientSlave(rtuslave);
                        prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                    }
                    else if (slaveName.Contains("MODBUSRTUMaster"))
                    {
                        var mainnode = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                        MODBUSRTUMaster_Slave rtuslave = new MODBUSRTUMaster_Slave();
                        rtuslave = mainnode.Slaves.Where(d => d.Name == slaveName).FirstOrDefault();
                        mainnode.Slaves.Remove(rtuslave);
                        ModbusRTUMasterManager.DeleteMODBUSRTUSlave(rtuslave);
                        rtuslave.IsDeletedRequest = true;
                        prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                    }
                    //else if (slaveName.Contains("MODBUSRTUSlaves"))
                    //{
                    //    var mainnode = (MODBUSRTUSlaves)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
                    //    MODBUSRTUSlaves_Slave rtuslave = new MODBUSRTUSlaves_Slave();
                    //    rtuslave = mainnode.Slaves.Where(d => d.Name == slaveName).FirstOrDefault();
                    //    ModbusRTUSlaveManager.DeleteMODBUSRTUSlave(rtuslave);
                    //    mainnode.Slaves.Remove(rtuslave);
                    //    rtuslave.IsDeletedRequest = true;
                    //    prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                    //}
                }
                xm.LoadedProject.NewAddedTagIndex = selectedcols.Count > 0 ? selectedcols.Min() : 0;
                if (xm.LoadedProject.NewAddedTagIndex > 0)
                    xm.LoadedProject.NewAddedTagIndex = xm.LoadedProject.NewAddedTagIndex - 1;
            }
            filteredRowsData.Clear();
            //currentFilterDataType.Clear();
            OnShown();
        }

        private void DeletedTag(string tags, XMIOConfig tagToRemove)
        {
            if (!ValidateUDFBEditPermission("cut"))
                return;
            if (xm.Entries != null)
            {
                var matchingEntry = xm.Entries.FirstOrDefault(e => e.Tag == tagToRemove.Tag);

                if (matchingEntry != null)
                {
                    DialogResult result = MessageBox.Show(
                        $"Tag '{tagToRemove.Tag}' is present in the Parallel Watch List.\nDo you want to remove it from the list?",
                        "XMPS2000",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        xm.Entries.Remove(matchingEntry);
                    }
                    else
                        return;
                }
            }
            frmMain.DeleteElement.Push(tagToRemove);
            frmMain.RedoTags.Push(tagToRemove);
            bool IsUsedInHSIO = false;
            List<string> usedIn = new List<string>();
            List<XMIOConfig> tempTagsList = xm.LoadedProject.Tags.Where(r => r.LogicalAddress == tagToRemove.LogicalAddress).ToList();
            List<string> data = tempTagsList.Select(a => a.LogicalAddress).ToList();
            //adding check for the check if logical address are usd in any schedule object.
            if (xm.LoadedProject.BacNetIP != null)
            {
                Schedule isAnySchedule = XMProValidator.CheckInScheduleObject(tags);
                if (isAnySchedule != null)
                {
                    MessageBox.Show($"{tags} are already used in {isAnySchedule.ObjectName} schedule object", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string checkblock = XMProValidator.CheckInLogicalBlock(tags);
            if (!string.IsNullOrWhiteSpace(checkblock))
                usedIn.Add("Logical Block");

            var MainBlockTags = xm.LoadedProject.MainLadderLogic;
            if (MainBlockTags != null)
            {
                string tagName = xm.LoadedProject.Tags.Where(t => t.LogicalAddress.Equals(tags)).Select(T => T.Tag).FirstOrDefault();
                foreach (var rung in MainBlockTags)
                {
                    if (rung.Contains(tagName))
                    {
                        usedIn.Add("Main Logic Block");
                        break;
                    }
                }
            }

            MODBUSRTUMaster_Slave modbusRTU = XMProValidator.CheckInModbusRTUMaster(tags);
            if (modbusRTU != null)
                usedIn.Add("MODBUS RTU Master");

            MODBUSRTUSlaves_Slave modbusSlavesSlave = XMProValidator.CheckInModbusRTUSlavesSlave(tags);
            if (modbusSlavesSlave != null)
                usedIn.Add("MODBUS RTU Slave");

            MODBUSTCPClient_Slave modbusTCPClient = XMProValidator.CheckInModbusTCPClient(tags);
            if (modbusTCPClient != null)
                usedIn.Add("MODBUS TCP Client");

            MODBUSTCPServer_Request modbusTCPServer = XMProValidator.CheckInModbusServerRequest(tags);
            if (modbusTCPServer != null)
                usedIn.Add("MODBUS TCP Server");

            bool isPublishRequest = XMProValidator.CheckInPublishTopics(tags);
            if (isPublishRequest)
                usedIn.Add("Publish");

            bool isSubscribeRequest = XMProValidator.CheckInSubscribeTopics(tags);
            if (isSubscribeRequest)
                usedIn.Add("Subscribe");

            bool isInUseOtherThanHSIO = checkblock.Length <= 0 && modbusRTU == null && modbusSlavesSlave == null && modbusTCPClient == null && modbusTCPServer == null && !isPublishRequest && !isSubscribeRequest;
            string errorIn = modbusRTU != null ? "MODBUS RTU Master" : modbusSlavesSlave != null ? "MODBUS RTU Slave" : modbusTCPClient != null ? "MODBUS TCP Client" : modbusTCPServer != null ? "MODBUS TCP Server" : "Logical block";

            //check in HSIO blocks.
            if (xm.LoadedProject.HsioBlock != null)
            {
                var blocks = XMProValidator.CheckInHSIOBlocks(tags);
                if (blocks != null && blocks.Count > 0)
                {
                    usedIn.Add("HSIO Blocks");
                    IsUsedInHSIO = true;
                }
            }
            if (xm.LoadedProject.BacNetIP != null)
            {
                if (usedIn.Count() > 0)
                {
                    string newMessage = $"Tag is already used in: {string.Join(", ", usedIn)}\nDo you want to remove it?";
                    DialogResult dialogResult = MessageBox.Show(newMessage, "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (xm.LoadedProject.BacNetIP != null)
                        {
                            RemoveFromBacnet(tagToRemove);
                        }
                        xm.LoadedProject.Tags.Remove(tagToRemove);
                        CheckandUpdateMainBlock(tagToRemove);
                    }
                    else
                        return;
                }
                else if (usedIn.Count() == 0)
                {
                    xm.LoadedProject.Tags.Remove(tagToRemove);
                    RemoveFromBacnet(tagToRemove);
                    CheckandUpdateMainBlock(tagToRemove);
                }
            }
            else
            {
                if (usedIn.Count() > 0)
                {
                    string newMessage = $"Tag is already used in: {string.Join(", ", usedIn)}\nDo you want to remove it?";
                    DialogResult dialogResult = MessageBox.Show(newMessage, "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (IsUsedInHSIO)
                        {
                            var blocks = XMProValidator.CheckInHSIOBlocks(tags);
                            if (blocks != null && blocks.Count > 0)
                            {
                                foreach (var block in blocks)
                                {
                                    var hsioBlockToUpdates = block.HSIOBlocks.Where(a => a.Value.Equals(tags));
                                    foreach (var hsioBlockToUpdate in hsioBlockToUpdates)
                                    {
                                        if (hsioBlockToUpdate != null)
                                        {
                                            hsioBlockToUpdate.Value = "???";
                                        }
                                    }
                                }
                            }

                        }
                        xm.LoadedProject.Tags.Remove(tagToRemove);
                        CheckandUpdateMainBlock(tagToRemove);
                    }
                    else
                        return;
                }
                else if (usedIn.Count() == 0)
                {
                    xm.LoadedProject.Tags.Remove(tagToRemove);
                    CheckandUpdateMainBlock(tagToRemove);
                }
            }
            if (tagToRemove.Retentive && tagToRemove.RetentiveAddress.Contains(":"))
            {
                CommonFunctions.UpdatePrecedingRetentiveAddresses(tagToRemove.RetentiveAddress);
            }
        }

        private void RemoveFromBacnet(XMIOConfig tagToRemove)
        {
            DeviceModel systemConfiguration = XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
            var ethernetDevices = systemConfiguration.Templates
                ?.Where(template => template.Ethernet != null)
                .ToList();
            var bacNetDevice = ethernetDevices?.SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices)
                               .FirstOrDefault(device => device.Name == "BacNet");

            if (tagToRemove.LogicalAddress.StartsWith("P5"))
            {
                if (XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues != null && XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Count > 0)
                {
                    var bacnetObject = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(tagToRemove.LogicalAddress));
                    if (bacnetObject != null && bacnetObject.IsEnable)
                    {
                        string objectType = bacnetObject.ObjectType.Split(':')[1].Trim();
                        objectType = objectType.Replace(" ", "");
                        ChangeTheCurrentObjectCount(objectType, ref bacNetDevice);
                        XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Remove(bacnetObject);
                    }
                    else if (bacnetObject != null)
                        XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Remove(bacnetObject);
                }
            }
            if (tagToRemove.LogicalAddress.StartsWith("W4"))
            {
                // First check if it's a Multistate Value
                var multistateObject = XMPS.Instance.LoadedProject.BacNetIP.MultistateValues?.FirstOrDefault(t => t.LogicalAddress.Equals(tagToRemove.LogicalAddress));
                if (multistateObject != null)
                {
                    if (multistateObject.IsEnable)
                    {
                        string objectType = multistateObject.ObjectType.Split(':')[1].Trim();
                        objectType = objectType.Replace(" ", "");
                        objectType = objectType == "MultistateValue" ? "MultiStateValue" : objectType;
                        ChangeTheCurrentObjectCount(objectType, ref bacNetDevice);
                        XMPS.Instance.LoadedProject.BacNetIP.MultistateValues.Remove(multistateObject);
                    }
                    else
                    {
                        XMPS.Instance.LoadedProject.BacNetIP.MultistateValues.Remove(multistateObject);
                    }
                }
                else
                {
                    // Check if it's an Analog Value
                    var analogObject = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues?.FirstOrDefault(t => t.LogicalAddress.Equals(tagToRemove.LogicalAddress));
                    if (analogObject != null)
                    {
                        if (analogObject.IsEnable)
                        {
                            string objectType = analogObject.ObjectType.Split(':')[1].Trim();
                            objectType = objectType.Replace(" ", "");
                            ChangeTheCurrentObjectCount(objectType, ref bacNetDevice);
                            XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Remove(analogObject);
                        }
                        else
                        {
                            XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Remove(analogObject);
                        }
                    }
                }
            }
            if (tagToRemove.LogicalAddress.StartsWith("F2"))
            {
                if (XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues != null && XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Count > 0)
                {
                    var bacnetObject = XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(tagToRemove.LogicalAddress));
                    if (bacnetObject != null && bacnetObject.IsEnable)
                    {
                        string objectType = bacnetObject.ObjectType.Split(':')[1].Trim();
                        objectType = objectType.Replace(" ", "");
                        ChangeTheCurrentObjectCount(objectType, ref bacNetDevice);
                        XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Remove(bacnetObject);
                    }
                    else if (bacnetObject != null)
                        XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Remove(bacnetObject);
                }
            }
        }

        private void CheckandUpdateMainBlock(XMIOConfig tagToRemove)
        {
            if (tagToRemove == null)
                return;
            if (xm.LoadedProject.MainLadderLogic.Where(t => t.Contains(tagToRemove.Tag)).Count() > 0)
            {
                for (int i = 0; i < xm.LoadedProject.MainLadderLogic.Count(); i++)
                {
                    string newvalue = xm.LoadedProject.MainLadderLogic[i].Replace('(' + tagToRemove.Tag + ')', '(' + "???" + ')');
                    xm.LoadedProject.MainLadderLogic[i] = newvalue;
                }

            }
        }

        private void ChangeTheCurrentObjectCount(string objectType, ref DeviceDetials bacNetDevice)
        {
            var propertyToUpdate = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == objectType);
            if (propertyToUpdate != null)
            {
                propertyToUpdate.CurrentCount -= 1;
            }
        }
        private List<LadderElement> CheckIsUsedInLadderLogic(int key, string functionBlockType)
        {
            List<LadderElement> FB = new List<LadderElement>();
            var BlockCount = xm.LoadedProject.Blocks.Where(T => T.Type.Equals("LogicBlock") || T.Type.Equals("InterruptLogicBlock")).ToList();
            for (int B = 0; B < BlockCount.Count; B++)
            {
                var BlkList = BlockCount[B].Name;

                if (xm.LoadedScreens.ContainsKey($"LadderForm#{BlockCount[B].Name}"))
                {
                    LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"LadderForm#{BlockCount[B].Name}"];
                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();

                    for (int k = 0; k < LadderDrawing.LadderDesign.Active.Elements.Count(); k++)
                    {
                        for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[k].Elements.Count; j++)
                        {
                            LadderElement ld = LadderDrawing.LadderDesign.Active.Elements[k].Elements[j];
                            if (ld.CustomType == "LadderDrawing.FunctionBlock")
                            {
                                if (ld.Attributes["output2"].ToString().Equals(key.ToString()) && ld.Attributes["function_name"].ToString().Equals(functionBlockType))
                                {
                                    FB.Add(ld);
                                }
                            }
                        }
                    }
                }
            }
            return FB;
        }
        public void contexMenuReset(bool value)
        {
            pasteCntx.Visible = value;
            copyCntx.Visible = value;
            cutCntx.Visible = value;
            cntxdelete.Visible = value;
            cntxDisVar.Visible = value;
            cntxCommentTag.Visible = value;
            cntxUncommentTag.Visible = value;
            cntxremoveDisablingVariable.Visible = value;
            cntxAddRequest.Visible = value;

        }
        private void grdMain_MouseClick(object sender, MouseEventArgs e)
        {// Early return only for left clicks on MODBUSRTUSlaves
            if (xm.CurrentScreen.ToString().Contains("MODBUSRTUSlaves") && e.Button == MouseButtons.Left)
            {
                return;
            }
            if (XMPS.Instance.PlcStatus != "LogIn")
            {
                if (xm.CurrentScreen.ToString().Contains("MODBUS"))
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        string screenName = xm.CurrentScreen.ToString();
                        if (grdMain.SelectedRows.Count > 0)
                        {
                            if (grdMain.Columns.Contains("Name") &&
                        grdMain.SelectedRows[0].Cells["Name"].Value != null)
                            {
                                var slaveName = grdMain.Rows[grdMain.SelectedRows[0].Index].Cells[grdMain.Columns["Name"].Index].Value.ToString();
                                if ((slaveName.Contains("ModbusTCPSlave")) || (slaveName.Contains("MODBUSTCPServer")) || (slaveName.Contains("ModbusRequestSlave")) || (slaveName.Contains("MODBUSTCPClient")))
                                {
                                    contexMenuReset(true);
                                    cntxDisVar.Visible = false;
                                    cntxAddRequest.Visible = false;
                                    cntxremoveDisablingVariable.Visible = false;
                                    cntxCommentTag.Visible = false;
                                    cntxUncommentTag.Visible = false;
                                    tsmAddResiValues.Visible = false;
                                    cntxCrossReferance.Visible = false;
                                    cntxmain.Show(grdMain, new Point(e.X, e.Y));
                                }
                                else if (slaveName.Contains("MODBUSRTUMaster"))
                                {
                                    var disableVariable = grdMain.Rows[grdMain.SelectedRows[0].Index].Cells[grdMain.Columns["DisablingVariables"].Index].Value;
                                    contexMenuReset(true);
                                    cntxCommentTag.Visible = false;
                                    cntxAddRequest.Visible = false;
                                    tsmAddResiValues.Visible = false;
                                    cntxDisVar.Visible = ((disableVariable == null) || (disableVariable.ToString() == "0")) ? true : false;
                                    cntxremoveDisablingVariable.Visible = ((disableVariable != null) && (disableVariable.ToString() != "0")) ? true : false;
                                    cntxUncommentTag.Visible = false;
                                    cntxCrossReferance.Visible = false;
                                    cntxmain.Show(grdMain, new Point(e.X, e.Y));
                                }
                            }
                            else if (e.Button == MouseButtons.Right && ((screenName.Contains("MODBUSTCPServer")) || (screenName.Contains("MODBUSTCPClient")) || (screenName.Contains("MODBUSRTUMaster"))))
                            {
                                contexMenuReset(false);
                                cntxCrossReferance.Visible = false;
                                pasteCntx.Visible = (copiedTag != null && copiedTag.Count > 0 || cutTag != null && cutTag.Count > 0) ? true : false;
                                cntxmain.Show(grdMain, new Point(e.X, e.Y));
                            }
                        }
                    }
                }

                if ((xm.CurrentScreen.ToString().EndsWith("Tags") && !(xm.CurrentScreen.ToString().StartsWith("LadderForm"))))
                {
                    if (e.Button == MouseButtons.Right && grdMain.SelectedRows.Count > 0)
                    {
                        //show Uncommented Tag cntx for only Commented Tag on Tag Window
                        string selectedTagAddress = grdMain.SelectedRows[0].Cells[grdMain.Columns["LogicalAddress"].Index].FormattedValue.ToString();

                        if (grdMain.SelectedRows[0].Cells.Count > 0)
                        {
                            if (xm.LoadedProject.Tags.Where(T => T.LogicalAddress == grdMain.SelectedRows[0].Cells[grdMain.Columns["LogicalAddress"].Index].FormattedValue.ToString()).First().IoList != IOListType.Default && !(selectedTagAddress.StartsWith("'")))
                            {
                                contexMenuReset(true);
                                cntxAddRequest.Visible = false;
                                cntxCrossReferance.Visible = true;
                                cntxDisVar.Visible = false;
                                tsmAddResiValues.Visible = false;
                                cntxUncommentTag.Visible = false;
                                cntxremoveDisablingVariable.Visible = false;
                                cntxmain.Show(grdMain, new Point(e.X, e.Y));
                            }
                            else
                            {
                                contexMenuReset(false);
                                cntxCrossReferance.Visible = true;
                                tsmAddResiValues.Visible = false;
                                cntxUncommentTag.Visible = selectedTagAddress.StartsWith("'") ? true : false;
                                cntxmain.Show(grdMain, new Point(e.X, e.Y));
                            }
                        }
                    }
                    else if (grdMain.SelectedRows.Count <= 0 && e.Button == MouseButtons.Right)
                    {
                        contexMenuReset(false);
                        cntxCrossReferance.Visible = false;
                        tsmAddResiValues.Visible = false;
                        pasteCntx.Visible = (copiedTag != null && copiedTag.Count > 0 || cutTag != null && cutTag.Count > 0) ? true : false;
                        cntxmain.Show(grdMain, new Point(e.X, e.Y));
                    }
                    if (grdMain.SelectedRows.Count > 0)
                    {
                        if (grdMain.SelectedRows.Count > 1)
                            return;
                        foreach (DataGridViewRow gr in grdMain.SelectedRows)
                        {
                            string tag = gr.Cells[1].Value.ToString();
                            XMPS.Instance.tagForCrossReference = tag;
                        }
                    }
                }
                if ((xm.CurrentScreen.ToString().Contains("Mqtt Form")))
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        cntxCrossReferance.Visible = false;
                        contexMenuReset(false);
                        cntxdelete.Visible = grdMain.SelectedRows.Count > 0 ? true : false;
                        cntxmain.Show(grdMain, new Point(e.X, e.Y));
                        tsmAddResiValues.Visible = false;
                    }
                }
                if ((xm.CurrentScreen.ToString().Contains("MQTT Publish")) || (xm.CurrentScreen.ToString().Contains("MQTT Subscribe")))
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        string screenName = xm.CurrentScreen.ToString();
                        if (grdMain.SelectedRows.Count > 0)
                        {
                            var request = grdMain.Rows[grdMain.SelectedRows[0].Index].Cells[grdMain.Columns["TOPIC"].Index].Value.ToString();
                            if (grdMain.Rows[grdMain.SelectedRows[0].Index].Cells[grdMain.Columns["Qos"].Index].Value.ToString() != "Key Name")
                            {
                                contexMenuReset(false);
                                copyCntx.Visible = true;
                                cutCntx.Visible = true;
                                pasteCntx.Visible = true;
                                cntxdelete.Visible = true;
                                tsmAddResiValues.Visible = false;
                                cntxCrossReferance.Visible = false;
                                if (request != "") cntxAddRequest.Visible = true;

                                cntxmain.Show(grdMain, new Point(e.X, e.Y));
                            }
                        }
                        else
                        {
                            contexMenuReset(false);
                            tsmAddResiValues.Visible = false;
                            cntxCrossReferance.Visible = false;
                            pasteCntx.Visible = (copiedTag != null && copiedTag.Count > 0 || cutTag != null && cutTag.Count > 0) ? true : false;
                            cntxmain.Show(grdMain, new Point(e.X, e.Y));
                        }
                    }
                }
                if (e.Button == MouseButtons.Right && grdMain.SelectedRows.Count > 0 && xm.CurrentScreen.ToString().Contains("ResistanceValue"))
                {
                    contexMenuReset(false);
                    cntxCrossReferance.Visible = false;

                    cntxdelete.Visible = xm.CurrentScreen.ToString().Contains("ResistanceValue");

                    pasteCntx.Visible = (copiedTag != null && copiedTag.Count > 0 || cutTag != null && cutTag.Count > 0) ? true : false;                
                    copyCntx.Visible = true;
                    cntxmain.Show(grdMain, new Point(e.X, e.Y));
                    var hit = grdMain.HitTest(e.X, e.Y);
                    int rowIndex = hit.RowIndex;

                    if (rowIndex >= 0)
                    {
                        grdMain.ClearSelection();
                        grdMain.Rows[rowIndex].Selected = true;

                        // Only show Delete for rows 2 and above
                        cntxdelete.Visible = xm.CurrentScreen.ToString().Contains("ResistanceValue") && rowIndex >= 2;
                        cutCntx.Visible = xm.CurrentScreen.ToString().Contains("ResistanceValue") && rowIndex >= 2;
                    }
                    else
                    {
                        cntxdelete.Visible = false;
                    }
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left && grdMain.SelectedRows.Count > 0)
                {
                    if (xm.CurrentScreen.ToString().Contains("Tags"))
                    {
                        TagsOnlineMonitoring();
                        omtimer.Start();
                    }
                }
            }
        }

        public void StartOMTime()
        {
            omtimer.Start();
        }
        private void omtimer_Tick(object sender, EventArgs e)
        {
            if (xm.CurrentScreen.ToString().Contains("Tags") && grdMain.Columns.Contains("Tag"))
                TagsOnlineMonitoring();
            else if (XMPS.Instance.PlcStatus == "LogIn")
                CheckSystemTagStatus();

        }

        private void CheckSystemTagStatus()
        {
            List<string> _ListTagName = new List<string>();       // Tagname
            Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddressInfo = new Dictionary<string, Tuple<string, AddressDataTypes>>();
            Dictionary<string, string> _AddressValues = new Dictionary<string, string>();

            ///Send the status address in every cycle to check status of CPU
            string tagName = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == XMPS.Instance.LoadedProject.PLCStatusTag).Select(t => t.Tag).FirstOrDefault();
            if (tagName != null)
            {
                _ListTagName.Add(tagName);
                CurBlockAddressInfo[tagName] = System.Tuple.Create(XMPS.Instance.LoadedProject.PLCStatusTag, AddressDataTypes.WORD);
            }

            ///Send the error status address in every cycle to check status of CPU
            foreach (string errorTagAddress in XMPS.Instance.LoadedProject.ErrorStatusTags)
            {
                XMIOConfig errTagDtl = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == errorTagAddress).FirstOrDefault();
                if (Enum.GetNames(typeof(AddressDataTypes)).Any(name => name.Equals(errTagDtl.Label, StringComparison.OrdinalIgnoreCase)))
                {
                    _ListTagName.Add(errTagDtl.Tag);
                    CurBlockAddressInfo[errTagDtl.Tag] = System.Tuple.Create(errTagDtl.LogicalAddress, (AddressDataTypes)Enum.Parse(typeof(AddressDataTypes), errTagDtl.Label, true));
                }
            }

            //bool isPingOk = XMProValidator.CheckPing();
            //if(!isPingOk)
            //{
            //    ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain")).DisableOnlineModeFromHSIO();
            //    return;
            //}

            OnlineMonitoring onlineMonitoring = OnlineMonitoring.GetInstance();
            onlineMonitoring.GetValues(_ListTagName, ref CurBlockAddressInfo, ref _AddressValues, out string Result);
            frmMain fm = Application.OpenForms.OfType<frmMain>().FirstOrDefault();
            if (fm != null && tagName != null)
            {
                _AddressValues.TryGetValue(tagName, out string value);

                List<Tuple<string, string>> tplErrorValues = new List<Tuple<string, string>>();
                ///Send the error status address in every cycle to check status of CPU
                foreach (string errorTagAddress in XMPS.Instance.LoadedProject.ErrorStatusTags)
                {
                    XMIOConfig errTagDtl = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == errorTagAddress).FirstOrDefault();
                    if (Enum.GetNames(typeof(AddressDataTypes)).Any(name => name.Equals(errTagDtl.Label, StringComparison.OrdinalIgnoreCase)))
                    {
                        _AddressValues.TryGetValue(errTagDtl.Tag, out string errvalue);
                        tplErrorValues.Add(System.Tuple.Create(errorTagAddress, errvalue));
                    }
                }

                fm.CheckPLCStatus(value, tplErrorValues);
            }

        }

        private void TagsOnlineMonitoring()
        {
            try
            {
                if (grdMain.SelectedRows.Count > 0)
                {
                    if (!grdMain.Columns.Contains("Tag")) return;
                    string screenName = xm.CurrentScreen.ToString();
                    var Tag = filteredRowsData.Count > 0 ? filteredRowsData : xm.LoadedProject.Tags;
                    if (_filter == ("User Defined Tags"))
                    {
                        if (filteredRowsData.Count > 0)
                        {
                            Tag = filteredRowsData;
                        }
                        else
                        {
                            Tag = xm.LoadedProject.Tags.Where(D => !D.LogicalAddress.StartsWith("S3") && D.IoList.ToString() != "OnBoardIO" && D.IoList.ToString() != "ExpansionIO" && D.IoList.ToString() != "RemoteIO" && D.Model.ToString() == "User Defined Tags").ToList();
                            Tag = xm.LoadedProject.Tags.Where(D => !D.LogicalAddress.StartsWith("S3") && D.IoList.ToString() != "OnBoardIO" && D.IoList.ToString() != "ExpansionIO" && D.IoList.ToString() != "RemoteIO" && D.Model.ToString() == "User Defined Tags").OrderBy(D => D.Key).ToList();
                        }

                    }
                    else if (_filter == "System Tags")
                    {
                        Tag = filteredRowsData.Count > 0 ? filteredRowsData : XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("S3")).ToList();
                    }
                    else if (_filter == "ExpansionIO")
                    {
                        Tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.IoList == IOListType.ExpansionIO).ToList();
                    }
                    else if (_filter == "RemoteIO")
                    {
                        Tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.IoList == IOListType.RemoteIO).ToList();
                    }
                    else if (_filter == "OnBoardIO")
                    {
                        Tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.IoList == IOListType.OnBoardIO).ToList();
                    }
                    else if (_filter == "UDFTags")
                    {
                        Tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.Model == xm.CurrentScreen.Split('#')[0]).ToList();
                    }
                    else
                    {
                        Tag = XMPS.Instance.LoadedProject.Tags.Where(T => T.Model == _filter).ToList();
                    }
                    var checkColumn = grdMain.Columns.Contains("Online Tag");
                    if (XMPS.Instance.PlcStatus == "LogIn" && !checkColumn)
                        grdMain.Columns.Add("OnLine Tag", "Actual Value");
                    else if (XMPS.Instance.PlcStatus != "LogIn" && checkColumn)
                        grdMain.Columns.Remove("Online Tag");

                    //When PLc IS LoggedIn 

                    if (XMPS.Instance.PlcStatus == "LogIn" /*&& counter == 0*/ )
                    {
                        ///<>
                        ///Accessing Dyanamic GridRow
                        int index = grdMain.FirstDisplayedScrollingRowIndex;
                        ///<>
                        ///Now we are showing only first 33 Address at the time of Online Monitoring
                        //int index = grdMain.CurrentRow.Index;
                        int totalRowCount = Tag.Count;
                        const int rowSize = 33;
                        int lastIndexAdd = (index + rowSize) < totalRowCount ? (index + rowSize) : (index + (totalRowCount - index));
                        // 1. activeAddress">List of addresses for online  ---> Pass Logical Address from index to Ending List Element
                        List<string> _systemtagsAddress = new List<string>();  //Logical Address
                        List<string> _ListTagName = new List<string>();       // Tagname
                        List<AddressDataTypes> _Type = new List<AddressDataTypes>();
                        OnlineMonitoringHelper omh = OnlineMonitoringHelper.Instance;
                        Dictionary<string, Tuple<string, AddressDataTypes>> CurBlockAddressInfo = new Dictionary<string, Tuple<string, AddressDataTypes>>();
                        //int i = index; i < Tag.Count; i++
                        for (int i = index; i < lastIndexAdd; i++)
                        {
                            if (!Tag[i].LogicalAddress.StartsWith("'"))
                            {
                                _systemtagsAddress.Add(Tag[i].LogicalAddress.ToString());
                                _ListTagName.Add(Tag[i].Tag.ToString());
                                if (Tag[i].Type.ToString().Equals("DigitalInput") || Tag[i].Type.ToString().Equals("DigitalOutput") || Tag[i].LogicalAddress.ToString().Contains('.') || (Tag[i].Mode != null && Tag[i].Mode.ToString().Equals("Digital")))
                                {
                                    _Type.Add(omh.GetAddressTypeOf("Bool"));
                                }
                                else if (!xm.LoadedProject.CPUDatatype.Equals("Real") && (Tag[i].Type.ToString().Equals("AnalogInput") || Tag[i].Type.ToString().Equals("UniversalInput")
                                        || Tag[i].Type.ToString().Equals("AnalogOutput") || Tag[i].Type.ToString().Equals("UniversalOutput")))
                                {
                                    _Type.Add(omh.GetAddressTypeOf("Word"));
                                }
                                else if (xm.LoadedProject.CPUDatatype.Equals("Real") && (Tag[i].Type.ToString().Equals("AnalogInput") || Tag[i].Type.ToString().Equals("UniversalInput")
                                        || Tag[i].Type.ToString().Equals("AnalogOutput") || Tag[i].Type.ToString().Equals("UniversalOutput")))
                                {
                                    _Type.Add(omh.GetAddressTypeOf("Real"));
                                }
                                else
                                    _Type.Add(omh.GetAddressTypeOf(Tag[i].Label));
                            }

                        }
                        for (int j = 0; j < _systemtagsAddress.Count; j++)
                        {
                            CurBlockAddressInfo.Add(_ListTagName[j], System.Tuple.Create(_systemtagsAddress[j], _Type[j]));
                        }
                        ///Send the status address in every cycle to check status of CPU
                        string tagName = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == XMPS.Instance.LoadedProject.PLCStatusTag).Select(t => t.Tag).FirstOrDefault();
                        if (tagName != null)
                        {
                            _ListTagName.Add(tagName);
                            CurBlockAddressInfo[tagName] = System.Tuple.Create(XMPS.Instance.LoadedProject.PLCStatusTag, AddressDataTypes.WORD);
                        }
                        ///Send the error status address in every cycle to check status of CPU
                        foreach (string errorTagAddress in XMPS.Instance.LoadedProject.ErrorStatusTags)
                        {
                            XMIOConfig errTagDtl = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == errorTagAddress).FirstOrDefault();
                            if (Enum.GetNames(typeof(AddressDataTypes)).Any(name => name.Equals(errTagDtl.Label, StringComparison.OrdinalIgnoreCase)))
                            {
                                _ListTagName.Add(errTagDtl.Tag);
                                CurBlockAddressInfo[errTagDtl.Tag] = System.Tuple.Create(errTagDtl.LogicalAddress, (AddressDataTypes)Enum.Parse(typeof(AddressDataTypes), errTagDtl.Label, true));
                            }
                        }
                        // 2. name="addressInfoDic">Dictionary with current Logic Block tags ----> (Tagname,Address,Type)

                        // 3. AddressValues ----------> tagname ,""
                        Dictionary<string, string> _AddressValues = new Dictionary<string, string>();
                        _AddressValues.Clear();
                        foreach (string AddressValue in _ListTagName.Distinct())
                        { _AddressValues.Add(AddressValue, ""); }

                        //bool isPingOk = XMProValidator.CheckPing();
                        //if (!isPingOk)
                        //{
                        //    ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain")).DisableOnlineModeFromHSIO();
                        //    return;
                        //}
                        OnlineMonitoring onlineMonitoring = OnlineMonitoring.GetInstance();
                        onlineMonitoring.GetValues(_ListTagName, ref CurBlockAddressInfo, ref _AddressValues, out string Result);
                        //Optimization for the above two foreach loop
                        foreach (DataGridViewRow row in grdMain.Rows)
                        {
                            if (row.Cells["Tag"].Value is string key && _AddressValues.TryGetValue(key, out string value))
                            {
                                string LogicalAddress = row.Cells["LogicalAddress"].Value.ToString();
                                XMIOConfig currentTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == LogicalAddress).FirstOrDefault();
                                if (grdMain.Columns.Contains("DataType"))
                                {
                                    string type = LogicalAddress.Contains(".") ? row.Cells["Type"].Value.ToString() : row.Cells["DataType"].Value.ToString();
                                    if (type.Equals("Real", StringComparison.OrdinalIgnoreCase) || (xm.LoadedProject.CPUDatatype.Equals("Real") && (currentTag.IoList == IOListType.OnBoardIO || currentTag.IoList == IOListType.RemoteIO || currentTag.IoList == IOListType.ExpansionIO)))
                                    {
                                        if (double.TryParse(value, out double parsedValue))
                                        {
                                            string[] parts = value.Split('.');
                                            if (parts.Length == 2 && parts[1].Length == 1)
                                            {
                                                value = parsedValue.ToString("0.00");
                                            }
                                        }
                                    }
                                }
                                //Checking Tag having Enum Valuer or Not
                                string enumName = currentTag.EnumValues.Where(T => T.Value.ToString().Equals(value)).Select(T => T.ValueName).FirstOrDefault();
                                if (enumName != null && enumName != "")
                                {
                                    ///Add the binary value for error tags 
                                    if (xm.LoadedProject.ErrorStatusTags.Contains(LogicalAddress) && Int32.TryParse(value, out _) && xm.LoadedProject.ExpansionErrorType != "Old")
                                        enumName = CommonFunctions.GetBinaryValue(Convert.ToInt32(value)).Substring(0, 5);
                                    //showing Enum Value instead of Actual Value
                                    row.Cells[grdMain.Columns.Count - 1].Value = enumName;
                                }
                                else
                                {
                                    ///Add the binary value for error tags 
                                    if (xm.LoadedProject.ErrorStatusTags.Contains(LogicalAddress) && Int32.TryParse(value, out _) && xm.LoadedProject.ExpansionErrorType != "Old")
                                        value = CommonFunctions.GetBinaryValue(Convert.ToInt32(value)).Substring(0, 5);
                                    row.Cells[grdMain.Columns.Count - 1].Value = value;
                                }

                            }
                        }

                        frmMain fm = Application.OpenForms.OfType<frmMain>().FirstOrDefault();
                        if (fm != null && tagName != null)
                        {
                            _AddressValues.TryGetValue(tagName, out string value);
                            List<Tuple<string, string>> tplErrorValues = new List<Tuple<string, string>>();
                            ///Send the error status address in every cycle to check status of CPU
                            foreach (string errorTagAddress in XMPS.Instance.LoadedProject.ErrorStatusTags)
                            {
                                XMIOConfig errTagDtl = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == errorTagAddress).FirstOrDefault();
                                if (Enum.GetNames(typeof(AddressDataTypes)).Any(name => name.Equals(errTagDtl.Label, StringComparison.OrdinalIgnoreCase)))
                                {
                                    _AddressValues.TryGetValue(errTagDtl.Tag, out string errvalue);
                                    tplErrorValues.Add(System.Tuple.Create(errorTagAddress, errvalue));
                                }
                            }

                            fm.CheckPLCStatus(value, tplErrorValues);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //for disabaling variable
        private void cntxDisVar_Click(object sender, EventArgs e)
        {
            List<int> selectedcols = new List<int>();

            foreach (DataGridViewRow gr in grdMain.SelectedRows)
            {
                selectedcols.Add(gr.Index);
            }
            string variable = "";
            foreach (int i in selectedcols)
            {
                var currentrow = i;
                DataGridViewRow selectedRow = grdMain.Rows[i];
                variable = selectedRow.Cells["Name"].Value.ToString();
            }
            GenerateDisabalingVariable generateDisVariable = new GenerateDisabalingVariable();
            generateDisVariable.ShowDialog(this.ParentForm);
            DialogResult dialogResult = generateDisVariable.dialogResult;
            if (dialogResult == DialogResult.OK)
            {
                //add = value from the generateDisablingVariable Form.
                string add = (string)generateDisVariable.comboBoxBitAddress.SelectedValue;
                //for the Adding Tag From the Disable Variablel Form
                if (add == null)
                {
                    XMProForm tempForm = new XMProForm();
                    tempForm.StartPosition = FormStartPosition.CenterParent;
                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    tempForm.Text = "Add New Address Added in Logic";
                    string logicalAddFromDis = generateDisVariable.comboBoxBitAddress.Text;
                    TagsUserControl userControl = new TagsUserControl(0, logicalAddFromDis);
                    tempForm.Height = userControl.Height + 25;
                    tempForm.Width = userControl.Width;
                    tempForm.Controls.Add(userControl);
                    var frmTemp = this.ParentForm as frmMain;
                    tempForm.ShowDialog(frmTemp);
                    var modBUSRTUMaster1 = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                    var addDisableVariable1 = modBUSRTUMaster1.Slaves.FirstOrDefault(T => T.Variable == variable);

                    if (addDisableVariable1 != null)
                    {
                        addDisableVariable1.DisablingVariables = logicalAddFromDis;
                        ModbusRTUMasterManager.UpdateDisablingVariable(addDisableVariable1, logicalAddFromDis);
                    }
                }
                else
                {
                    //bit address to logical address
                    string logicalAdd = XMProValidator.GetTheAddressFromTag(add);
                    var modBUSRTUMaster = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                    var addDisableVariable = modBUSRTUMaster.Slaves.FirstOrDefault(T => T.Name == variable);

                    if (addDisableVariable != null)
                    {
                        addDisableVariable.DisablingVariables = logicalAdd;
                        ModbusRTUMasterManager.UpdateDisablingVariable(addDisableVariable, logicalAdd);
                    }
                }
            }
            prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
            OnShown();
        }
        public void copyResistanceValues()
        {
            try
            {
                if (grdMain.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select at least one resistance value to copy.",
                                    "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!xm.CurrentScreen.ToString().Contains("frmAddResistanceValue") &&
                    !xm.CurrentScreen.ToString().Contains("Resistance") &&
                    !xm.CurrentScreen.ToString().Contains("LookUpTbl"))
                {
                    return;
                }

                copiedTag = new List<object>();
                cutTag = new List<object>();
                cutTag.Clear();
                string currentTableName = xm.SelectedNode?.Info ?? string.Empty;

                List<int> selectedRows = new List<int>();
                foreach (DataGridViewRow row in grdMain.SelectedRows)
                    selectedRows.Add(row.Index);

                foreach (int i in selectedRows)
                {
                    if (i < 0 || i >= grdMain.Rows.Count)
                        continue;

                    var resistanceCell = grdMain.Rows[i].Cells["Resistance (Ohm)"].Value;
                    var outputCell = grdMain.Rows[i].Cells["Output Value"].Value;

                    if (resistanceCell == null || outputCell == null)
                        continue;

                    string resistanceValue = resistanceCell.ToString().Trim();
                    string outputValue = outputCell.ToString().Trim();

                    if (string.IsNullOrEmpty(resistanceValue) || string.IsNullOrEmpty(outputValue))
                        continue;

                    if (!double.TryParse(resistanceValue, out double resVal))
                    {
                        MessageBox.Show($"Invalid resistance value '{resistanceValue}' at row {i + 1}.",
                                        "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (!double.TryParse(outputValue, out double outVal))
                    {
                        MessageBox.Show($"Invalid output value '{outputValue}' at row {i + 1}.",
                                        "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    RESISTANCETable_Values newVal = new RESISTANCETable_Values
                    {
                        Resistance = resVal,
                        output = outVal,
                        Name = currentTableName
                    };

                    copiedTag.Add(newVal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while copying resistance values: {ex.Message}",
                                "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void copyCntx_Click(object sender, EventArgs e)
        {
            if (xm.CurrentScreen.ToString().Contains("LookUpTbl"))
            {
                return;
            }
            if (xm.CurrentScreen.ToString().Contains("frmAddResistanceValue") || xm.CurrentScreen.ToString().Contains("ResistanceValue"))
            {
                copyResistanceValues();
            }
            else
            {
                copyTagAndModbusRequests();
            }
        }
        public void copyTagAndModbusRequests()
        {
            if (xm.CurrentScreen.ToString().Contains("MODBUSRTUSlavesForm"))
            {
                return;
            }
            bool isTopic = false;
            List<int> selectedcols = new List<int>();
            foreach (DataGridViewRow gr in grdMain.SelectedRows)
            {
                //checking for mqtt
                string topicName = gr.Cells[0].Value.ToString();
                string qos = gr.Cells[1].Value.ToString();
                string variable = gr.Cells[2].Value.ToString();

                if (qos == "Key Name" && variable == "Variable" && (xm.CurrentScreen.ToString().Contains("MQTT Publish") || xm.CurrentScreen.ToString().Contains("MQTT Subscribe")))
                {
                    return;
                }

                if (((!string.IsNullOrEmpty(topicName) && (qos == "0" || qos == "1")) || (qos == "Key Name" && variable == "Variable"))
                    && (xm.CurrentScreen.ToString().Contains("MQTT Publish") || xm.CurrentScreen.ToString().Contains("MQTT Subscribe")))
                {
                    isTopic = true;
                }
                selectedcols.Add(gr.Index);
            }
            if (isTopic && grdMain.SelectedRows.Count > 1)
            {
                MessageBox.Show("Plese select single topic or requests", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            copiedTag = new List<Object>();
            cutTag = new List<object>();
            if (cutTag != null)
            {
                cutTag.Clear();
            }
            if (xm.CurrentScreen.ToString().Contains("Tags"))
            {

                foreach (int i in selectedcols)
                {
                    var currentSelected = i;
                    //Copy data from grid view;
                    var tags = grdMain.Rows[currentSelected].DataBoundItem.ToString().Split(',').First().Split('=').Last().Trim();
                    if (tags.StartsWith("'"))
                    {
                        MessageBox.Show("Please Select Uncommented Tag", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var copyTag = xm.LoadedProject.Tags.Where(x => x.LogicalAddress == tags).FirstOrDefault();

                    List<XMIOConfig> tempTagsList = xm.LoadedProject.Tags.Where(r => r.LogicalAddress == copyTag.LogicalAddress).ToList();
                    copiedTag.Add(copyTag);
                    List<string> data = tempTagsList.Select(a => a.LogicalAddress).ToList();
                }
            }
            else if (xm.CurrentScreen.Contains("MQTT Publish"))
            {
                foreach (int i in selectedcols)
                {
                    string topic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                    int selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value.ToString());
                    if (topic != "")
                    {
                        var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                        if (copiedTag.Count == 0)
                        {
                            copiedTag.Add(publist.Where(n => n.keyvalue == selkey).FirstOrDefault());
                        }
                        else
                        {
                            copiedTag.Clear();
                            copiedTag.Add(publist.Where(n => n.keyvalue == selkey).FirstOrDefault());
                        }
                        //Storing the Current Index of selected Topic 
                        xm.LoadedProject.NewAddedTagIndex = i;
                    }
                    else
                    {
                        int cindex = i;
                        selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);
                        while (topic == "")
                        {
                            cindex--;
                            topic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                            selkey = topic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value) : -1;
                        }
                        string keyname = grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString();

                        var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                        Publish Selpub = publist.Where(n => n.topic == topic && n.keyvalue == selkey).FirstOrDefault();
                        if (copiedTag.Count == 0)
                        {
                            copiedTag.Add(Selpub.PubRequest.Where(n => n.req == keyname).FirstOrDefault());
                        }
                        else
                        {
                            //copiedTag.Clear();
                            copiedTag.Add(Selpub.PubRequest.Where(n => n.req == keyname).FirstOrDefault());
                        }

                    }
                }
            }
            else if (xm.CurrentScreen.Contains("MQTT Subscribe"))
            {
                foreach (int i in selectedcols)
                {
                    string topic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                    int selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value.ToString());
                    if (topic != "")
                    {
                        var Sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                        if (copiedTag.Count == 0)
                        {
                            copiedTag.Add(Sublist.Where(n => n.key == selkey).FirstOrDefault());
                        }
                        else
                        {
                            //copiedTag.Clear();
                            copiedTag.Add(Sublist.Where(n => n.key == selkey).FirstOrDefault());
                        }

                    }
                    else
                    {
                        int cindex = i;
                        while (topic == "")
                        {
                            cindex--;
                            topic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                            selkey = topic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value.ToString()) : selkey;
                        }
                        string keyname = grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString();

                        var Sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                        Subscribe Selpub = Sublist.Where(n => n.key == selkey).FirstOrDefault();
                        if (copiedTag.Count == 0)
                        {
                            copiedTag.Add(Selpub.SubRequest.Where(n => n.key == Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value)).FirstOrDefault());
                        }
                        else
                        {
                            //copiedTag.Clear();
                            copiedTag.Add(Selpub.SubRequest.Where(n => n.key == Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value)).FirstOrDefault());
                        }

                    }
                }
            }
            else
            {
                if (grdMain.Columns == null || !grdMain.Columns.Contains("Name"))
                {
                    return;
                }
                try
                {
                    int nameindex = grdMain.Columns["Name"].Index;

                    foreach (int i in selectedcols)
                    {
                        if (i < 0 || i >= grdMain.Rows.Count || grdMain.Rows[i].Cells[nameindex].Value == null)
                        {
                            continue; // Skip invalid rows
                        }

                        var slaveName = grdMain.Rows[i].Cells[nameindex].Value.ToString();
                        if (string.IsNullOrEmpty(slaveName))
                        {
                            continue; // Skip empty names
                        }

                        // MODBUSRTUMaster case
                        if (slaveName.Contains("MODBUSRTUMaster"))
                        {
                            var mainnode = xm.LoadedProject.Devices?.OfType<MODBUSRTUMaster>().FirstOrDefault();
                            if (mainnode?.Slaves != null)
                            {
                                var rtuslave = mainnode.Slaves.FirstOrDefault(d => d.Name == slaveName);
                                if (rtuslave != null)
                                {
                                    copiedTag.Add(rtuslave);
                                }
                            }
                        }
                        else if (slaveName.Contains("MODBUSRTUSlaves"))
                        {
                            var mainnode = xm.LoadedProject.Devices?.OfType<MODBUSRTUSlaves>().FirstOrDefault();
                            if (mainnode?.Slaves != null)
                            {
                                var rtuslave = mainnode.Slaves.FirstOrDefault(d => d.Name == slaveName);
                                if (rtuslave != null)
                                {
                                    copiedTag.Add(rtuslave);
                                }
                            }
                        }
                        // MODBUSTCPClient case
                        else if (slaveName.Contains("MODBUSTCPClient"))
                        {
                            var mainnode = xm.LoadedProject.Devices?.OfType<MODBUSTCPClient>().FirstOrDefault();
                            if (mainnode?.Slaves != null)
                            {
                                var rtuslave = mainnode.Slaves.FirstOrDefault(d => d.Name == slaveName);
                                if (rtuslave != null)
                                {
                                    copiedTag.Add(rtuslave);
                                }
                            }
                        }
                        // MODBUSTCPServer case
                        else if (slaveName.Contains("MODBUSTCPServer"))
                        {
                            var mainnode = xm.LoadedProject.Devices?.OfType<MODBUSTCPServer>().FirstOrDefault();
                            if (mainnode?.Requests != null)
                            {
                                var rtuslave = mainnode.Requests.FirstOrDefault(d => d.Name == slaveName);
                                if (rtuslave != null)
                                {
                                    copiedTag.Add(rtuslave);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error if needed
                    System.Diagnostics.Debug.WriteLine($"Error in copy operation: {ex.Message}");
                }
            }
        }
        public void pasteResistanceValues()
        {
            try
            {
                string currentTableName = xm.SelectedNode?.Info ?? string.Empty;
                if (string.IsNullOrEmpty(currentTableName))
                {
                    MessageBox.Show("Please select a Resistance Table node first.",
                                    "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (XMPS.Instance.LoadedProject.ResistanceValues == null)
                    XMPS.Instance.LoadedProject.ResistanceValues = new List<RESISTANCETable_Values>();

                List<RESISTANCETable_Values> sourceList = null;
             
                if (cutTag != null && cutTag.OfType<RESISTANCETable_Values>().Any())
                {
                    sourceList = cutTag.OfType<RESISTANCETable_Values>().ToList();
                    cutTag.Clear();
                }
                else if (copiedTag != null && copiedTag.OfType<RESISTANCETable_Values>().Any())
                {
                    sourceList = copiedTag.OfType<RESISTANCETable_Values>()
                                          .Where(v => v.Name == currentTableName)
                                          .Reverse()
                                          .ToList();
                }

                if (sourceList == null || sourceList.Count == 0)
                    return;
                int addedCount = 0;
                foreach (var val in sourceList)
                {
                    if (XMPS.Instance.LoadedProject.ResistanceValues.Count(x => x.Name == currentTableName) >= 20)
                    {
                        MessageBox.Show("A resistance table can contain a maximum of 20 values.",
                                        "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }

                    RESISTANCETable_Values newVal = new RESISTANCETable_Values
                    {
                        Resistance = val.Resistance,
                        output = val.output,
                        Name = currentTableName
                    };

                    XMPS.Instance.LoadedProject.ResistanceValues.Add(newVal);
                    addedCount++;
                }
                OnShown(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while pasting resistance values: {ex.Message}",
                                "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pasteCntx_Click(object sender, EventArgs e)
        {
            if (xm.CurrentScreen.ToString().Contains("frmAddResistanceValue") || xm.CurrentScreen.ToString().Contains("ResistanceValue") || xm.CurrentScreen.ToString().Contains("LookUpTbl"))
            {
                pasteResistanceValues();
            }
            else
            {
                pasteTagAndModbusRequests();
            }
        }

        private bool ValidateUDFBEditPermission(string actionType = "edit")
        {
            string currentScreenName = xm.CurrentScreen.ToString();

            if (!currentScreenName.Contains("Tags"))
                return true;

            string selectedNode = this.formName;
            string normalizedNode = selectedNode.Replace(" Tags", "").Trim();
            string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MessungSystems", "XMPS2000", "Library");
            string librarySubFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD", StringComparison.OrdinalIgnoreCase)
                ? "XBLDLibraries"
                : "XMLibraries";
            string libraryPath = Path.Combine(basePath, librarySubFolder);
            string[] csvFiles = Directory.Exists(libraryPath) ? Directory.GetFiles(libraryPath, "*.csv") : Array.Empty<string>();
            List<string> udfbNames = XMPS.Instance.LoadedProject.UDFBInfo.Select(u => u.UDFBName).ToList();

            var fileNames = csvFiles.Select(Path.GetFileNameWithoutExtension)
                .Select(name => name.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                    ? name.Substring(0, name.Length - 6).Trim()
                    : name);

            bool isUdfbMatch = fileNames.Any(fileName => fileName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase) &&
                                udfbNames.Any(udfbName => udfbName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase)));

            if (!isUdfbMatch)
                return true;

            string savedChoice = XMPS.Instance.LoadedProject.GetUDFBChoice(normalizedNode);
            string savedLocalCopyName = null;

            if (!string.IsNullOrEmpty(savedChoice) && savedChoice.StartsWith("CreateLocalCopy:"))
            {
                savedLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);
            }

            bool localCopyWithDifferentNameExists = !string.IsNullOrEmpty(savedLocalCopyName) && !savedLocalCopyName.Equals(normalizedNode, StringComparison.OrdinalIgnoreCase) &&
                XMPS.Instance.LoadedProject.Blocks.Any(b => b.Type == "UserFunctionBlock" && b.Name.Equals(savedLocalCopyName, StringComparison.OrdinalIgnoreCase));

            // If library UDFB exists AND a local copy exists, it means library was re-imported
            // In this case, ignore saved choice and show popup
            if (localCopyWithDifferentNameExists)
            {
                using (var optionsForm = new UDFBEditOptionsForm(normalizedNode))
                {
                    if (optionsForm.ShowDialog() == DialogResult.OK)
                    {
                        if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                        {
                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "EditMainFile");
                            return true;
                        }
                        else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                        {
                            if (actionType == "cut")
                            {
                                MessageBox.Show("Cannot cut tags from a UDFB with 'CreateLocalCopy' option. Please create a local copy first.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            string newLocalCopyName = optionsForm.LocalCopyName;
                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "CreateLocalCopy:" + newLocalCopyName);
                            var frmTemp = this.ParentForm as frmMain;
                            frmTemp.CreateAndEditLocalCopy(null, normalizedNode, newLocalCopyName);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (!string.IsNullOrEmpty(savedChoice))
            {
                if (savedChoice == "EditMainFile")
                {
                    return true;
                }
                else if (savedChoice.StartsWith("CreateLocalCopy:"))
                {
                    string existingLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);

                    bool specificLocalCopyExists = XMPS.Instance.LoadedProject.Blocks.Any(b =>
                        b.Type == "UserFunctionBlock" &&
                        b.Name.Equals(existingLocalCopyName, StringComparison.OrdinalIgnoreCase));

                    if (specificLocalCopyExists)
                    {
                        return true;
                    }
                    else
                    {
                        XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "");

                        using (var optionsForm = new UDFBEditOptionsForm(normalizedNode))
                        {
                            if (optionsForm.ShowDialog() == DialogResult.OK)
                            {
                                if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                {
                                    XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "EditMainFile");
                                    return true;
                                }
                                else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                {
                                    if (actionType == "cut")
                                    {
                                        MessageBox.Show("Cannot cut tags from a UDFB with 'CreateLocalCopy' option. Please create a local copy first.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return false;
                                    }
                                    string recreatedLocalCopyName = optionsForm.LocalCopyName;
                                    XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "CreateLocalCopy:" + recreatedLocalCopyName);
                                    var frmTemp = this.ParentForm as frmMain;
                                    frmTemp.CreateAndEditLocalCopy(null, normalizedNode, recreatedLocalCopyName);
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                using (var optionsForm = new UDFBEditOptionsForm(normalizedNode))
                {
                    if (optionsForm.ShowDialog() == DialogResult.OK)
                    {
                        if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                        {
                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "EditMainFile");
                            return true;
                        }
                        else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                        {
                            if (actionType == "cut")
                            {
                                MessageBox.Show("Cannot cut tags from a UDFB with 'CreateLocalCopy' option. Please create a local copy first.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            string initialLocalCopyName = optionsForm.LocalCopyName;
                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedNode, "CreateLocalCopy:" + initialLocalCopyName);
                            var frmTemp = this.ParentForm as frmMain;
                            frmTemp.CreateAndEditLocalCopy(null, normalizedNode, initialLocalCopyName);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void ShowTagPasteDialogDirect(XMIOConfig xmTagForPaste)
        {
            TagsUserControl tagsUserControl = new TagsUserControl("$", xmTagForPaste);
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "Add User Defined IO Model";
            tempForm.Height = tagsUserControl.Height + 25;
            tempForm.Width = tagsUserControl.Width;
            tempForm.Controls.Add(tagsUserControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK && filteredRowsData.Count == 0)
            {
                int count = xm.LoadedProject.Tags.Where(T => T.Model == "User Defined Tags").Count();
                xm.LoadedProject.NewAddedTagIndex = count - 1;
            }
            else
            {
                currentFilterDataType.Clear();
                filteredRowsData.Clear();
            }
        }
        public void pasteTagAndModbusRequests()
        {
            if (copiedTag != null || cutTag != null)
            {
                copiedTag.Reverse();
                cutTag.Reverse();

                if (xm.CurrentScreen.ToString().Contains("Tags"))
                {
                    XMIOConfig xmTagForPaste = new XMIOConfig();
                    try
                    {
                        List<XMIOConfig> tags = new List<XMIOConfig>();
                        if (copiedTag.Count > 0 || cutTag.Count > 0)
                        {
                            if (copiedTag.Count > 0)
                            {
                                foreach (XMIOConfig item in copiedTag)
                                {
                                    tags.Add(item);
                                }
                            }
                            if (cutTag.Count > 0)
                            {
                                foreach (XMIOConfig item in cutTag)
                                {
                                    tags.Add(item);
                                }
                            }
                            if (!ValidateUDFBEditPermission("paste"))
                                return;

                            foreach (XMIOConfig tag in tags)
                            {
                                xmTagForPaste = tag;
                                ShowTagPasteDialogDirect(xmTagForPaste);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Attempt", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (xm.CurrentScreen.Contains("MQTT Publish"))
                {
                    if (copiedTag.Count == 0)
                        copiedTag.AddRange(cutTag);
                    if (copiedTag[0] == null) return;
                    if (copiedTag[0].GetType().Name == "PubRequest")
                    {
                        int i = grdMain.SelectedRows.Count == 0 ? 0 : grdMain.SelectedRows[0].Index;
                        if (grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString() == "Key Name")
                        {
                            MessageBox.Show("Invalid Attempt,Select Key or Topic before pasting", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        int selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);
                        string seltopic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                        int cindex = i;
                        while (seltopic == "")
                        {
                            cindex--;
                            seltopic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                            selkey = seltopic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value) : -1;
                        }
                        var chkpublist = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
                        Publish subtop = chkpublist.Where(s => s.keyvalue == selkey).FirstOrDefault();
                        var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                        // Getting selected Topic ID By Topic name and SeleKey
                        int ClickedtopicId = publist.Where(s => s.topic == seltopic && s.keyvalue == selkey).Select(s => s.keyvalue).FirstOrDefault();
                        PubRequest pubRequest = new PubRequest();
                        int copidTagTopicId = pubRequest.GetTopicId((PubRequest)copiedTag[0]);
                        XMProForm tempForm = new XMProForm();
                        tempForm.StartPosition = FormStartPosition.CenterParent;
                        tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        string screenName = xm.CurrentScreen.ToString();
                        PublishRequest newreq = null;
                        if (copiedTag.Count > 1)
                        {
                            copiedTag.Sort((obj1, obj2) =>
                            {
                                if (obj1 is PubRequest pubRequest1 && obj2 is PubRequest pubRequest2)
                                {
                                    return pubRequest1.Keyvalue.CompareTo(pubRequest2.Keyvalue);
                                }
                                return 0;
                            });
                        }
                        foreach (PubRequest pr in copiedTag)
                        {
                            publishManager.SaveState();
                            PubRequest copiedpr = new PubRequest();
                            newreq = new PublishRequest(pr, selkey);
                            tempForm.Text = "Add Publish Request";
                            tempForm.Height = newreq.Height + 25;
                            tempForm.Width = newreq.Width;
                            DialogResult result = newreq.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                string keyname = pr.req;

                                Publish Selpub = null;
                                if (cutTag.Count > 0)
                                {
                                    var firstRequest = (PubRequest)cutTag.FirstOrDefault();
                                    Selpub = publist.Where(T => T.keyvalue == selkey).FirstOrDefault();
                                }
                                else
                                {
                                    Selpub = publist.Where(n => n.keyvalue == selkey).FirstOrDefault();
                                }
                                copiedpr.topic = selkey;
                                copiedpr.Tag = XMProValidator.GetTheAddressFromTag(newreq.Tagname);
                                copiedpr.req = newreq.Keyname;
                                copiedpr.Keyvalue = cutTag.Count > 0 ? (Selpub.PubRequest.Count() > 0 ? Selpub.PubRequest.Max(k => k.Keyvalue) + 1 : pr.Keyvalue) : (Selpub.PubRequest.Count() > 0 ? Selpub.PubRequest.Max(k => k.Keyvalue) + 1 : Selpub.PubRequest.Count() + 1);
                                Selpub.PubRequest.Add(copiedpr);
                            }

                        }
                        xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Publish");
                        xm.LoadedProject.Devices.AddRange(publist);
                        xm.LoadedProject.NewAddedTagIndex = i;
                    }
                    else if (copiedTag[0].GetType().Name == "Publish")
                    {

                        Publish selectedpr = (Publish)copiedTag[0];
                        Publish copiedpr = new Publish();

                        var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                        copiedpr.topic = selectedpr.topic;
                        copiedpr.retainflag = selectedpr.retainflag;
                        copiedpr.qos = selectedpr.qos;
                        copiedpr.Type = selectedpr.Type;
                        copiedpr.keyvalue = cutTag.Count > 0 ? publist.Any(t => t.keyvalue == selectedpr.keyvalue) ? (publist.Max(k => k.keyvalue) + 1) : selectedpr.keyvalue : (publist.Count() > 0 ? publist.Max(k => k.keyvalue) + 1 : publist.Count() + 1); //== 0 ? 1 : publist.Max(k => k.keyvalue) 

                        XMProForm tempForm = new XMProForm();
                        tempForm.StartPosition = FormStartPosition.CenterParent;
                        tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        string screenName = xm.CurrentScreen.ToString();
                        PublishRequest newreq = null;

                        foreach (PubRequest pr in selectedpr.PubRequest)
                        {
                            newreq = new PublishRequest(pr, copiedpr.keyvalue, copiedpr.topic);
                            tempForm.Text = "Add Publish Request";
                            tempForm.Height = newreq.Height + 25;
                            tempForm.Width = newreq.Width;
                            DialogResult result = newreq.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                PubRequest newpr = new PubRequest();
                                newpr.topic = copiedpr.keyvalue;
                                newpr.req = newreq.Keyname;
                                newpr.Tag = XMProValidator.GetTheAddressFromTag(newreq.Tagname);
                                newpr.Keyvalue = pr.Keyvalue;

                                copiedpr.AddPublishRequest(newpr);
                            }
                        }
                        publist.Add(copiedpr);
                        xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Publish");
                        publishManager.AddPublish(copiedpr);
                        xm.LoadedProject.Devices.AddRange(publist);
                        if (publist != null)
                        {
                            xm.LoadedProject.NewAddedTagIndex = 0;
                            int tpkWithReq = publist.Take(publist.Count - 1).Where(p => p.PubRequest.Count > 0).Count();
                            xm.LoadedProject.NewAddedTagIndex = publist.Take(publist.Count - 1).SelectMany(P => P.PubRequest).Count() + (publist.Count() - 1) + tpkWithReq;
                        }
                    }
                    else if (copiedTag[0].GetType().Name == "Subscribe")
                    {
                        Subscribe selectedpr = (Subscribe)copiedTag[0];
                        Publish copiedpr = new Publish();

                        var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                        copiedpr.topic = selectedpr.topic;
                        copiedpr.retainflag = "0";
                        copiedpr.qos = selectedpr.qos;
                        copiedpr.Type = selectedpr.Type;
                        copiedpr.keyvalue = (publist.Count > 0 ? publist.Max(k => k.keyvalue) + 1 : publist.Count() + 1);

                        XMProForm tempForm = new XMProForm();
                        tempForm.StartPosition = FormStartPosition.CenterParent;
                        tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        string screenName = xm.CurrentScreen.ToString();
                        PublishRequest newreq = null;

                        foreach (SubscribeRequest pr in selectedpr.SubRequest)
                        {
                            PubRequest newpr = new PubRequest();
                            newpr.topic = pr.topic;
                            newpr.req = pr.req;
                            newpr.Tag = XMProValidator.GetTheAddressFromTag(pr.Tag);
                            newpr.Keyvalue = pr.key;


                            newreq = new PublishRequest(newpr, copiedpr.keyvalue, copiedpr.topic);
                            tempForm.Text = "Add Publish Request";
                            tempForm.Height = newreq.Height + 25;
                            tempForm.Width = newreq.Width;
                            DialogResult result = newreq.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                PubRequest newpr1 = new PubRequest();
                                newpr.topic = copiedpr.keyvalue;
                                newpr.req = newreq.Keyname;
                                newpr.Tag = XMProValidator.GetTheAddressFromTag(newreq.Tagname);
                                newpr.Keyvalue = pr.key;

                                copiedpr.AddPublishRequest(newpr);
                            }
                        }
                        publist.Add(copiedpr);
                        xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Publish");
                        publishManager.AddPublish(copiedpr);
                        xm.LoadedProject.Devices.AddRange(publist);
                        if (publist != null)
                        {
                            xm.LoadedProject.NewAddedTagIndex = 0;
                            int tpkWithReq = publist.Take(publist.Count - 1).Where(p => p.PubRequest.Count > 0).Count();
                            xm.LoadedProject.NewAddedTagIndex = publist.Take(publist.Count - 1).SelectMany(P => P.PubRequest).Count() + (publist.Count() - 1) + tpkWithReq;
                        }
                    }
                }
                else if (xm.CurrentScreen.Contains("MQTT Subscribe"))
                {
                    if (copiedTag.Count == 0)
                        copiedTag.AddRange(cutTag);
                    if (copiedTag[0] == null) return;
                    //Copy paste requests under one block
                    if (copiedTag[0].GetType().Name == "SubscribeRequest")
                    {
                        int i = grdMain.SelectedRows[0].Index;
                        string seltopic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                        int cindex = i;
                        if (grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString() == "Key Name")
                        {
                            MessageBox.Show("Invalid Attempt,Select Key or Topic before pasting", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        int selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);

                        while (seltopic == "")
                        {
                            cindex--;
                            seltopic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                            selkey = seltopic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value) : -1;
                        }
                        var Sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                        int ClickedTopicId = Sublist.Where(s => s.topic == seltopic).Select(s => s.key).FirstOrDefault();
                        SubscribeRequest SR = new SubscribeRequest();
                        int CopiedTopicId = SR.GetTopicId((SubscribeRequest)copiedTag[0]);

                        Subscribe Selpub = null;
                        if (cutTag.Count > 0)
                        {
                            var firstRequest = (SubscribeRequest)cutTag.FirstOrDefault();
                            Selpub = Sublist.Where(n => n.key == selkey).FirstOrDefault();
                        }
                        else
                        {
                            Selpub = Sublist.Where(n => n.key == selkey).FirstOrDefault();
                        }
                        XMProForm tempForm = new XMProForm();
                        tempForm.StartPosition = FormStartPosition.CenterParent;
                        tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        string screenName = xm.CurrentScreen.ToString();
                        SuscribeRequest newreq = null;
                        if (copiedTag.Count > 1)
                        {
                            copiedTag.Sort((obj1, obj2) =>
                            {
                                if (obj1 is SubscribeRequest subRequest1 && obj2 is SubscribeRequest subRequest2)
                                {
                                    return subRequest1.key.CompareTo(subRequest2.key);
                                }
                                return 0;
                            });
                        }
                        foreach (SubscribeRequest sr in copiedTag)
                        {
                            subscribeManager.SaveState();
                            SubscribeRequest copiedpr = new SubscribeRequest();
                            newreq = new SuscribeRequest(sr, selkey);
                            tempForm.Text = "Add Subscribe Request";
                            tempForm.Height = newreq.Height + 25;
                            tempForm.Width = newreq.Width;
                            DialogResult result = newreq.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                string keyname = copiedpr.req;
                                //copiedpr.topic = sr.topic;
                                copiedpr.topic = selkey;
                                copiedpr.Tag = XMProValidator.GetTheAddressFromTag(newreq.Tagname);
                                copiedpr.req = newreq.Keyname;
                                copiedpr.key = cutTag.Count > 0 ? (Selpub.SubRequest.Count > 0 ? Selpub.SubRequest.Max(K => K.key) + 1 : sr.key) : (Selpub.SubRequest.Count > 0 ? Selpub.SubRequest.Max(K => K.key) + 1 : Selpub.SubRequest.Count + 1);
                                Selpub.SubRequest.Add(copiedpr);

                            }
                        }

                        xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Subscribe");
                        xm.LoadedProject.Devices.AddRange(Sublist);
                        xm.LoadedProject.NewAddedTagIndex = i;
                    }
                    //Paste Whole Subscribe Block
                    else if (copiedTag[0].GetType().Name == "Subscribe" /*|| copiedTag[0].GetType().Name == "Publish"*/)
                    {
                        var sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();

                        Subscribe selectedpr = (Subscribe)copiedTag[0];
                        Subscribe copiedsr = new Subscribe();
                        copiedsr.topic = selectedpr.topic;
                        copiedsr.qos = selectedpr.qos;
                        copiedsr.Type = selectedpr.Type;
                        //copiedsr.key = sublist.Count() + 1;
                        copiedsr.key = cutTag.Count > 0 ? sublist.Any(t => t.key == selectedpr.key) ? (sublist.Max(k => k.key) + 1) : selectedpr.key : (sublist.Count() > 0 ? sublist.Max(k => k.key) + 1 : sublist.Count() + 1);
                        XMProForm tempForm = new XMProForm();
                        tempForm.StartPosition = FormStartPosition.CenterParent;
                        tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        string screenName = xm.CurrentScreen.ToString();
                        SuscribeRequest newreq = null;
                        foreach (SubscribeRequest sr in selectedpr.SubRequest)
                        {
                            //sr.topic = copiedsr.key;
                            newreq = new SuscribeRequest(sr, copiedsr.key, copiedsr.topic);
                            tempForm.Text = "Add Subscribe Request";
                            tempForm.Height = newreq.Height + 25;
                            tempForm.Width = newreq.Width;
                            DialogResult result = newreq.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                SubscribeRequest newSr = new SubscribeRequest();
                                newSr.topic = copiedsr.key;
                                newSr.req = newreq.Keyname;
                                newSr.Tag = XMProValidator.GetTheAddressFromTag(newreq.Tagname); ;
                                newSr.key = newreq.keyValue;
                                copiedsr.AddPublishRequest(newSr);
                            }

                        }
                        sublist.Add(copiedsr);
                        xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Subscribe");
                        subscribeManager.AddSubscribe(copiedsr);
                        xm.LoadedProject.Devices.AddRange(sublist);
                        if (sublist != null)
                        {
                            xm.LoadedProject.NewAddedTagIndex = 0;
                            int tpkWithReq = sublist.Take(sublist.Count - 1).Where(p => p.SubRequest.Count > 0).Count();
                            xm.LoadedProject.NewAddedTagIndex = sublist.Take(sublist.Count - 1).SelectMany(P => P.SubRequest).Count() + (sublist.Count() - 1) + tpkWithReq;
                        }
                    }
                    else if (copiedTag[0].GetType().Name == "Publish")
                    {
                        var sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();

                        Publish publish = (Publish)copiedTag[0];
                        Subscribe copiedsr = new Subscribe();
                        //add for the increase Topic Number if we copy whole Subscribe Topic
                        copiedsr.topic = publish.topic;
                        copiedsr.qos = publish.qos;
                        copiedsr.Type = publish.Type;
                        copiedsr.key = (sublist.Count > 0 ? sublist.Max(k => k.key) + 1 : sublist.Count() + 1);
                        XMProForm tempForm = new XMProForm();
                        tempForm.StartPosition = FormStartPosition.CenterParent;
                        tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        string screenName = xm.CurrentScreen.ToString();
                        SuscribeRequest newreq = null;
                        foreach (PubRequest pr in publish.PubRequest)
                        {
                            SubscribeRequest newSr1 = new SubscribeRequest();
                            newSr1.topic = copiedsr.key;
                            newSr1.req = pr.req;
                            newSr1.Tag = XMProValidator.GetTheAddressFromTag(pr.Tag);
                            newSr1.key = pr.Keyvalue;

                            newreq = new SuscribeRequest(newSr1, copiedsr.key, copiedsr.topic);
                            tempForm.Text = "Add Subscribe Request";
                            tempForm.Height = newreq.Height + 25;
                            tempForm.Width = newreq.Width;
                            DialogResult result = newreq.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                SubscribeRequest newSr = new SubscribeRequest();
                                newSr.topic = copiedsr.key;
                                newSr.req = newreq.Keyname;
                                newSr.Tag = XMProValidator.GetTheAddressFromTag(newreq.Tagname);
                                newSr.key = newreq.keyValue;
                                copiedsr.AddPublishRequest(newSr);
                            }
                        }
                        sublist.Add(copiedsr);
                        xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Subscribe");
                        subscribeManager.AddSubscribe(copiedsr);
                        xm.LoadedProject.Devices.AddRange(sublist);
                        if (sublist != null)
                        {
                            xm.LoadedProject.NewAddedTagIndex = 0;
                            int tpkWithReq = sublist.Take(sublist.Count - 1).Where(p => p.SubRequest.Count > 0).Count();
                            xm.LoadedProject.NewAddedTagIndex = sublist.Take(sublist.Count - 1).SelectMany(P => P.SubRequest).Count() + (sublist.Count() - 1) + tpkWithReq;
                        }
                    }
                }
                else
                {
                    var slaveName = "";
                    List<Object> tags = new List<Object>();
                    if (copiedTag.Count > 0 || cutTag.Count > 0)
                    {
                        if (copiedTag.Count > 0)
                        {
                            foreach (var item in copiedTag)
                            {
                                tags.Add(item);
                            }
                        }
                        if (cutTag.Count > 0)
                        {
                            foreach (var item in cutTag)
                            {
                                tags.Add(item);
                            }
                        }
                        foreach (var tag in tags)
                        {
                            slaveName = tag.ToString();
                            PasteAllTag(slaveName, tag);
                        }
                    }
                }
            }
            else
            {
                if (xm.CurrentScreen.Contains("MQTT"))
                {
                    MessageBox.Show("Please Copy TOPIC", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Please Copy Tag ", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            OnShown();
        }


        public void PasteAllTag(string slaveName, object tag)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            string screenName = xm.CurrentScreen.ToString();

            if (slaveName.Contains("MODBUSRTUMaster"))
            {
                if (screenName.Contains("MODBUSRTUMasterForm"))
                {

                    ModbusRTUUserControl mODBUSRTUMaster_Slave = new ModbusRTUUserControl();
                    MODBUSRTUMaster_Slave mODBUSRTUMaster = new MODBUSRTUMaster_Slave();

                    var modbusMasterList = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                    List<MODBUSRTUMaster_Slave> modbusMasterSlavesList = modbusMasterList.Slaves.OrderBy(M => int.Parse(M.Name.Substring("MODBUSRTUMasterSlave".Length))).ToList();

                    string copySlaveName = "";
                    if (copiedTag.Count > 0)
                    {
                        mODBUSRTUMaster = (MODBUSRTUMaster_Slave)tag;
                        if (modbusMasterSlavesList.Count == 0)
                        {
                            copySlaveName = "MODBUSRTUMasterSlave01";
                        }
                        else
                        {
                            MODBUSRTUMaster_Slave lastModbusRtu = modbusMasterSlavesList.Last();
                            string input = lastModbusRtu.Name;
                            string pattern = @"\d+";
                            var matches = Regex.Matches(input, pattern).Cast<Match>().Select(m => int.Parse(m.Value));
                            if (matches.Any())
                            {
                                int nameNum = matches.Max();
                                copySlaveName = $"MODBUSRTUMasterSlave{nameNum + 1:D2}";
                            }
                        }
                    }
                    else
                    {
                        mODBUSRTUMaster = (MODBUSRTUMaster_Slave)tag;
                        copySlaveName = mODBUSRTUMaster.Name;
                        MODBUSRTUMaster_Slave ms = modbusMasterSlavesList.Where(s => s.Name == mODBUSRTUMaster.Name).FirstOrDefault();
                        //Commenting these as per messung sugeest add all cut tags at the end
                        //required these logic if we need to paste the slave in-between list after cut.
                        if (ms == null)
                        {
                            copySlaveName = mODBUSRTUMaster.Name;
                        }
                        else
                        {
                            mODBUSRTUMaster = (MODBUSRTUMaster_Slave)tag;
                            MODBUSRTUMaster_Slave lastModbusRtu = modbusMasterSlavesList.Last();
                            string input = lastModbusRtu.Name;
                            string pattern = @"\d+";
                            var matches = Regex.Matches(input, pattern).Cast<Match>().Select(m => int.Parse(m.Value));
                            string nameNum = string.Join(", ", matches);
                            copySlaveName = Convert.ToInt32(nameNum) < 9 ? "MODBUSRTUMasterSlave0" + $"{Convert.ToInt32(nameNum) + 1}"
                                            : "MODBUSRTUMasterSlave" + $"{Convert.ToInt32(nameNum) + 1}";
                        }
                    }


                    mODBUSRTUMaster_Slave.CopyDataBind(copySlaveName, mODBUSRTUMaster);
                    tempForm.Text = "Modbus RTU Master Settings";
                    tempForm.Height = mODBUSRTUMaster_Slave.Height + 25;
                    tempForm.Width = mODBUSRTUMaster_Slave.Width;
                    tempForm.Controls.Add(mODBUSRTUMaster_Slave);
                    var frmTemp = this.ParentForm as frmMain;
                    tempForm.ShowDialog(frmTemp);
                }
                else
                {
                    MessageBox.Show("Invalid Attempt ", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ///RTUSLAVES
            else if (slaveName.Contains("MODBUSRTUSlaves"))
            {
                if (screenName.Contains("MODBUSRTUSlavesForm"))
                {

                    ModbusRTUSlaveUserControl mODBUSRTUMaster_Slave = new ModbusRTUSlaveUserControl();
                    MODBUSRTUSlaves_Slave mODBUSRTUMaster = new MODBUSRTUSlaves_Slave();

                    var modbusMasterList = (MODBUSRTUSlaves)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
                    List<MODBUSRTUSlaves_Slave> modbusMasterSlavesList = modbusMasterList.Slaves.OrderBy(M => M.Name).ToList();

                    string copySlaveName = "";
                    if (copiedTag.Count > 0)
                    {
                        mODBUSRTUMaster = (MODBUSRTUSlaves_Slave)tag;
                        if (modbusMasterSlavesList.Count == 0)
                        {
                            // If no slaves exist, assign the first name
                            copySlaveName = "MODBUSRTUSlavesSlave01";
                        }
                        else
                        {
                            MODBUSRTUSlaves_Slave lastModbusRtu = modbusMasterSlavesList.Last();
                            string input = lastModbusRtu.Name;
                            string pattern = @"\d+";

                            var matches = Regex.Matches(input, pattern)
                                               .Cast<Match>()
                                               .Select(m => int.Parse(m.Value));

                            if (matches.Any())
                            {
                                int nameNum = matches.Max();
                                copySlaveName = $"MODBUSRTUSlavesSlave{nameNum + 1:D2}";
                            }
                        }
                    }
                    else
                    {
                        mODBUSRTUMaster = (MODBUSRTUSlaves_Slave)tag;
                        copySlaveName = mODBUSRTUMaster.Name;
                        MODBUSRTUSlaves_Slave ms = modbusMasterSlavesList.Where(s => s.Name == mODBUSRTUMaster.Name).FirstOrDefault();
                        if (ms == null)
                        {
                            copySlaveName = mODBUSRTUMaster.Name;
                        }
                        else
                        {
                            mODBUSRTUMaster = (MODBUSRTUSlaves_Slave)tag;
                            MODBUSRTUSlaves_Slave lastModbusRtu = modbusMasterSlavesList.Last();
                            string input = lastModbusRtu.Name;
                            string pattern = @"\d+";
                            var matches = Regex.Matches(input, pattern).Cast<Match>().Select(m => int.Parse(m.Value));
                            string nameNum = string.Join(", ", matches);
                            copySlaveName = Convert.ToInt32(nameNum) < 9 ? "MODBUSRTUSlavesSlave0" + $"{Convert.ToInt32(nameNum) + 1}"
                                            : "MODBUSRTUSlavesSlave" + $"{Convert.ToInt32(nameNum) + 1}";
                        }
                    }



                    mODBUSRTUMaster_Slave.CopyDataBind(copySlaveName, mODBUSRTUMaster);
                    tempForm.Text = "Modbus TCP Server Settings";
                    tempForm.Height = mODBUSRTUMaster_Slave.Height + 25;
                    tempForm.Width = mODBUSRTUMaster_Slave.Width;
                    tempForm.Controls.Add(mODBUSRTUMaster_Slave);
                    var frmTemp = this.ParentForm as frmMain;
                    tempForm.ShowDialog(frmTemp);
                }
                else
                {
                    MessageBox.Show("Invalid Attempt ", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (slaveName.Contains("MODBUSTCPClient"))
            {
                if (screenName.Contains("MODBUSTCPClientForm"))
                {
                    MODBUSTCPClient_Slave ModbusTCPClient = new MODBUSTCPClient_Slave();
                    var modbusTCPClient = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                    List<MODBUSTCPClient_Slave> moodbusTCPClientList = modbusTCPClient.Slaves.OrderBy(M => int.Parse(M.Name.Substring("MODBUSTCPClientSlave".Length))).ToList();

                    ModbusTCPClientUserControl modbusTCPClientUserControl = new ModbusTCPClientUserControl();
                    string copySlaveName = "";
                    if (copiedTag.Count > 0)
                    {
                        ModbusTCPClient = (MODBUSTCPClient_Slave)tag;
                        if (moodbusTCPClientList.Count == 0)
                        {
                            copySlaveName = "MODBUSTCPClientSlave01";
                        }
                        else
                        {
                            MODBUSTCPClient_Slave lastModbusClientSlave = moodbusTCPClientList.Last();
                            string input = lastModbusClientSlave.Name;
                            string pattern = @"\d+";
                            var matches = Regex.Matches(input, pattern).Cast<Match>().Select(m => int.Parse(m.Value));
                            if (matches.Any())
                            {
                                int nameNum = matches.Max();
                                copySlaveName = $"MODBUSTCPClientSlave{nameNum + 1:D2}";
                            }
                        }
                    }
                    else
                    {
                        ModbusTCPClient = (MODBUSTCPClient_Slave)tag;
                        copySlaveName = ModbusTCPClient.Name;
                        MODBUSTCPClient_Slave ms = moodbusTCPClientList.Where(s => s.Variable == ModbusTCPClient.Variable).FirstOrDefault();
                        if (ms == null)
                        {
                            copySlaveName = ModbusTCPClient.Name;
                        }
                        else
                        {
                            ModbusTCPClient = (MODBUSTCPClient_Slave)tag;

                            MODBUSTCPClient_Slave lastModbusClientSlave = moodbusTCPClientList.Last();
                            string input = lastModbusClientSlave.Name;
                            string pattern = @"\d+";
                            var matches = Regex.Matches(input, pattern).Cast<Match>().Select(m => int.Parse(m.Value));
                            string nameNum = string.Join(", ", matches);
                            copySlaveName = Convert.ToInt32(nameNum) < 9 ? "MODBUSTCPClientSlave0" + $"{Convert.ToInt32(nameNum) + 1}"
                                            : "MODBUSTCPClientSlave" + $"{Convert.ToInt32(nameNum) + 1}";
                        }
                    }
                    modbusTCPClientUserControl.copyDataBind(copySlaveName, ModbusTCPClient);
                    tempForm.Text = "Modbus TCP Client Settings";
                    tempForm.Height = modbusTCPClientUserControl.Height + 25;
                    tempForm.Width = modbusTCPClientUserControl.Width;
                    tempForm.Controls.Add(modbusTCPClientUserControl);
                    var frmTemp = this.ParentForm as frmMain;
                    tempForm.ShowDialog(frmTemp);
                }
                else
                {
                    MessageBox.Show("Invalid Attempt", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (slaveName.Contains("MODBUSTCPServer"))
            {
                if (screenName.Contains("MODBUSTCPServerForm"))
                {
                    MODBUSTCPServer_Request mODBUSTCPServer_Request = new MODBUSTCPServer_Request();
                    var mODBUSTCPServer = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                    List<MODBUSTCPServer_Request> modbusTCPServer_RequestList = mODBUSTCPServer.Requests.OrderBy(M => int.Parse(M.Name.Substring("MODBUSTCPServerRequest".Length))).ToList();

                    string copySlaveName = "";
                    if (copiedTag.Count > 0)
                    {
                        mODBUSTCPServer_Request = (MODBUSTCPServer_Request)tag;
                        if (modbusTCPServer_RequestList.Count == 0)
                        {
                            copySlaveName = "MODBUSTCPServerRequest01";
                        }
                        else
                        {
                            MODBUSTCPServer_Request lastModbusRequest = modbusTCPServer_RequestList.Last();
                            string input = lastModbusRequest.Name;
                            string pattern = @"\d+";
                            var matches = Regex.Matches(input, pattern).Cast<Match>().Select(m => int.Parse(m.Value));
                            if (matches.Any())
                            {
                                int nameNum = matches.Max();
                                copySlaveName = $"MODBUSTCPServerRequest{nameNum + 1:D2}";
                            }
                        }
                    }
                    else
                    {
                        mODBUSTCPServer_Request = (MODBUSTCPServer_Request)tag;
                        copySlaveName = mODBUSTCPServer_Request.Name;
                        MODBUSTCPServer_Request mr = modbusTCPServer_RequestList.Where(s => s.Variable == mODBUSTCPServer_Request.Variable).FirstOrDefault();
                        if (mr == null)
                        {
                            copySlaveName = mODBUSTCPServer_Request.Name;
                        }
                        else
                        {
                            mODBUSTCPServer_Request = (MODBUSTCPServer_Request)tag;
                            MODBUSTCPServer_Request lastModbusRequest = modbusTCPServer_RequestList.Last();
                            string input = lastModbusRequest.Name;
                            string pattern = @"\d+";
                            var matches = Regex.Matches(input, pattern).Cast<Match>().Select(m => int.Parse(m.Value));
                            string nameNum = string.Join(", ", matches);
                            copySlaveName = Convert.ToInt32(nameNum) < 9 ? "MODBUSTCPServerRequest0" + $"{Convert.ToInt32(nameNum) + 1}"
                                            : "MODBUSTCPServerRequest" + $"{Convert.ToInt32(nameNum) + 1}";
                        }
                    }

                    ModbusTCPServerUserControl userControl = new ModbusTCPServerUserControl(copySlaveName);
                    userControl.copyDataBind(mODBUSTCPServer_Request);
                    tempForm.Text = "Modbus TCP Server Settings";
                    tempForm.Height = userControl.Height + 25;
                    tempForm.Width = userControl.Width;
                    tempForm.Controls.Add(userControl);
                    var frmTemp = this.ParentForm as frmMain;
                    tempForm.ShowDialog(frmTemp);
                }
                else
                {
                    MessageBox.Show("Invalid Attempt", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (slaveName.Contains("XMIOConfig"))
            {
                MessageBox.Show("Invalid Attempt", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cutCntx_Click(object sender, EventArgs e)
        {
            if (xm.CurrentScreen.ToString().Contains("frmAddResistanceValue") || xm.CurrentScreen.ToString().Contains("ResistanceValue") || xm.CurrentScreen.ToString().Contains("LookUpTbl"))
            {
                cutResistanceValues();
            }
            else
            {
                cutTagAndModbusRequests();
            }
           
        }
        public void cutResistanceValues()
        {
            try
            {
                if (grdMain.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select at least one resistance value to cut.",
                                    "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!xm.CurrentScreen.ToString().Contains("frmAddResistanceValue") &&
                    !xm.CurrentScreen.ToString().Contains("Resistance") &&
                    !xm.CurrentScreen.ToString().Contains("LookUpTbl"))
                {
                    return;
                }

                cutTag = new List<object>();
                copiedTag = new List<object>();
                string currentTableName = xm.SelectedNode?.Info ?? string.Empty;

                if (string.IsNullOrEmpty(currentTableName))
                {
                    return;
                }
                var allValuesForTable = XMPS.Instance.LoadedProject.ResistanceValues
                    .Where(x => x.Name == currentTableName)
                    .ToList();
                List<int> selectedRows = new List<int>();
                foreach (DataGridViewRow row in grdMain.SelectedRows)
                    selectedRows.Add(row.Index);
                foreach (int rowIndex in selectedRows.OrderByDescending(x => x))
                {
                    if (rowIndex < 2)
                        continue;

                    if (rowIndex < 0 || rowIndex >= grdMain.Rows.Count)
                        continue;

                    var resistanceCell = grdMain.Rows[rowIndex].Cells["Resistance (Ohm)"].Value;
                    var outputCell = grdMain.Rows[rowIndex].Cells["Output Value"].Value;

                    if (resistanceCell == null || outputCell == null)
                        continue;

                    if (!double.TryParse(resistanceCell.ToString().Trim(), out double resVal) ||
                        !double.TryParse(outputCell.ToString().Trim(), out double outVal))
                        continue;
                    RESISTANCETable_Values cutVal = new RESISTANCETable_Values
                    {
                        Resistance = resVal,
                        output = outVal,
                        Name = currentTableName
                    };
                    cutTag.Add(cutVal);
                    if (rowIndex - 2 < allValuesForTable.Count) 
                    {
                        var itemToRemove = allValuesForTable[rowIndex];
                        XMPS.Instance.LoadedProject.ResistanceValues.Remove(itemToRemove);
                        allValuesForTable.RemoveAt(rowIndex);
                    }
                }

                OnShown(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while cutting resistance values: {ex.Message}",
                                "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cutTagAndModbusRequests()
        {
            if (xm.CurrentScreen.ToString().Contains("MODBUSRTUSlavesForm") || xm.CurrentScreen.ToString().Contains("LookUpTbl"))
            {
                return;
            }
            bool isTopic = false;
            List<int> selectedcols = new List<int>();
            foreach (DataGridViewRow gr in grdMain.SelectedRows)
            {
                //checking for mqtt
                string topicName = gr.Cells[0].Value.ToString();
                string qos = gr.Cells[1].Value.ToString();
                string variable = gr.Cells[2].Value.ToString();

                if (qos == "Key Name" && variable == "Variable" && (xm.CurrentScreen.ToString().Contains("MQTT Publish") || xm.CurrentScreen.ToString().Contains("MQTT Subscribe")))
                {
                    return;
                }

                if (((!string.IsNullOrEmpty(topicName) && (qos == "0" || qos == "1")) || (qos == "Key Name" && variable == "Variable"))
                    && (xm.CurrentScreen.ToString().Contains("MQTT Publish") || xm.CurrentScreen.ToString().Contains("MQTT Subscribe")))
                {
                    isTopic = true;
                }
                selectedcols.Add(gr.Index);
            }
            if (isTopic && grdMain.SelectedRows.Count > 1)
            {
                MessageBox.Show("Plese select single topic or requests", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (xm.CurrentScreen.ToString().Contains("Tags"))
            {
                if (!ValidateUDFBEditPermission("cut"))
                    return;
            }
            copiedTag = new List<object>(1);
            if (copiedTag != null)
            {
                copiedTag.Clear();
            }
            if (selectedcols.Count == 0)
            {
                MessageBox.Show("There is No Tag Present", "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cutTagFromList(selectedcols);
            filteredRowsData.Clear();
            currentFilterDataType.Clear();
            OnShown();
        }

        private void cutTagFromList(List<int> selectedRow)
        {
            xm.LoadedProject.NewAddedTagIndex = 0;
            cutTag = new List<Object>();
            List<int> selectedcols = selectedRow;
            if (xm.CurrentScreen.ToString().Contains("Tags"))
            {

                foreach (int i in selectedcols)
                {
                    var currentSelected = i;
                    var tags = grdMain.Rows[currentSelected].DataBoundItem.ToString().Split(',').First().Split('=').Last().Trim();
                    if (tags.StartsWith("'"))
                    {
                        MessageBox.Show("Please Select Uncommented Tag", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var tagToCut = xm.LoadedProject.Tags.Where(x => x.LogicalAddress == tags).FirstOrDefault();
                    List<XMIOConfig> tempTagsList = xm.LoadedProject.Tags.Where(r => r.LogicalAddress == tagToCut.LogicalAddress).ToList();
                    List<string> data = tempTagsList.Select(a => a.LogicalAddress).ToList();

                    //adding check for the check if logical address are usd in any schedule object.
                    if (xm.LoadedProject.BacNetIP != null)
                    {
                        Schedule isAnySchedule = XMProValidator.CheckInScheduleObject(tags);
                        if (isAnySchedule != null)
                        {
                            MessageBox.Show($"{tags} are already used in {isAnySchedule.ObjectName} schedule object", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    MODBUSRTUMaster_Slave modbusRTU = XMProValidator.CheckInModbusRTUMaster(tags);

                    MODBUSRTUSlaves_Slave modbusSlavesSlave = XMProValidator.CheckInModbusRTUSlavesSlave(tags);

                    MODBUSTCPClient_Slave modbusTCPClient = XMProValidator.CheckInModbusTCPClient(tags);

                    MODBUSTCPServer_Request modbusTCPServer = XMProValidator.CheckInModbusServerRequest(tags);

                    bool isPublishRequest = XMProValidator.CheckInPublishTopics(tags);

                    bool isSubscribeRequest = XMProValidator.CheckInSubscribeTopics(tags);

                    string checkblock = XMProValidator.CheckInLogicalBlock(tags);

                    bool isInUseOtherThanHSIO = checkblock.Length <= 0 && modbusRTU == null && modbusSlavesSlave == null && modbusTCPClient == null && modbusTCPServer == null && !isPublishRequest && !isSubscribeRequest;
                    //check in HSIO blocks.
                    if (xm.LoadedProject.HsioBlock != null)
                    {
                        var blocks = XMProValidator.CheckInHSIOBlocks(tags);
                        if (blocks != null && blocks.Count() > 0)
                        {
                            string newMessage = !isInUseOtherThanHSIO ? "Tag is alredy used in block" : string.Empty;
                            DialogResult dialogResult = MessageBox.Show($"{newMessage} \n Tag are already used in HSIOs \n You Want to Remove it", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (dialogResult == DialogResult.Yes)
                            {
                                isInUseOtherThanHSIO = true;
                                foreach (var block in blocks)
                                {
                                    var hsioBlockToUpdates = block.HSIOBlocks.Where(a => a.Value.Equals(tags));
                                    foreach (var hsioBlockToUpdate in hsioBlockToUpdates)
                                    {
                                        if (hsioBlockToUpdate != null)
                                        {
                                            hsioBlockToUpdate.Value = "???";
                                        }
                                    }
                                }
                            }
                            else
                                return;
                        }
                    }
                    if (isInUseOtherThanHSIO)
                    {
                        cutTag.Add(tagToCut);
                        xm.LoadedProject.Tags.Remove(tagToCut);
                    }
                    else
                    {
                        if (MessageBox.Show("Tag are already used in block remove it from logic blocks and then try to delete this tag", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            cutTag.Add(tagToCut);
                            xm.LoadedProject.Tags.Remove(tagToCut);
                        }
                    }
                }
                return;
            }
            if (xm.CurrentScreen.ToString().Contains("MQTT"))
            {
                if (xm.CurrentScreen.Contains("MQTT Publish"))
                {
                    foreach (int i in selectedcols)
                    {
                        string topic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                        int topicKey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);
                        if (topic != "")
                        {
                            List<LadderElement> FB = CheckIsUsedInLadderLogic(topicKey, "MQTT Publish");
                            if (FB.Count != 0)
                            {
                                MessageBox.Show("Delete function blocks having topic: " + topic + " Then try again", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                            if (cutTag.Count == 0)
                            {
                                cutTag.Add(publist.Where(n => n.keyvalue == topicKey).FirstOrDefault());
                            }
                            else
                            {
                                //cutTag.Clear();
                                cutTag.Add(publist.Where(n => n.keyvalue == topicKey).FirstOrDefault());
                            }
                            publishManager.DeletePublish(publist.FirstOrDefault(n => n.keyvalue == topicKey));
                            xm.LoadedProject.Devices.Remove(publist.Where(n => n.keyvalue == topicKey).FirstOrDefault());
                            xm.LoadedProject.NewAddedTagIndex = 0;
                            string prevTopic = string.Empty;
                            int currentIndex = i;
                            while (i > 0 && prevTopic == string.Empty)
                            {
                                currentIndex--;
                                prevTopic = grdMain.Rows[currentIndex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                            }
                            xm.LoadedProject.NewAddedTagIndex = currentIndex;
                        }
                        else
                        {
                            int cindex = i;
                            while (topic == "")
                            {
                                cindex--;
                                topic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                topicKey = topic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value) : 0;

                            }
                            string keyname = grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString();
                            int keyid = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value);

                            var publist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
                            Publish Selpub = publist.Where(n => n.keyvalue == topicKey).FirstOrDefault();
                            if (cutTag.Count == 0)
                            {
                                cutTag.Add(Selpub.PubRequest.Where(n => n.Keyvalue == keyid).FirstOrDefault());
                            }
                            else
                            {
                                //cutTag.Clear();
                                cutTag.Add(Selpub.PubRequest.Where(n => n.Keyvalue == keyid).FirstOrDefault());
                            }
                            xm.LoadedProject.Devices.Remove(Selpub);
                            publishManager.SaveState();
                            Selpub.PubRequest.Remove(Selpub.PubRequest.Where(n => n.Keyvalue == keyid).FirstOrDefault());
                            xm.LoadedProject.Devices.Add(Selpub);
                            xm.LoadedProject.NewAddedTagIndex = cindex;
                        }
                    }
                }
                else if (xm.CurrentScreen.Contains("MQTT Subscribe"))
                {
                    foreach (int i in selectedcols)
                    {
                        string topic = grdMain.Rows[i].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                        int selkey = Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value.ToString());
                        if (topic != "")
                        {
                            List<LadderElement> FB = CheckIsUsedInLadderLogic(selkey, "MQTT Subscribe");
                            if (FB.Count != 0)
                            {
                                MessageBox.Show("Delete function blocks having topic: " + topic + " Then try again", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            var Sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                            if (cutTag.Count == 0)
                            {
                                cutTag.Add(Sublist.Where(n => n.key == selkey).FirstOrDefault());
                            }
                            else
                            {
                                //cutTag.Clear();
                                cutTag.Add(Sublist.Where(n => n.key == selkey).FirstOrDefault());
                            }
                            subscribeManager.DeleteSubscribe(Sublist.Where(n => n.key == selkey).FirstOrDefault());
                            xm.LoadedProject.Devices.Remove(Sublist.Where(n => n.key == selkey).FirstOrDefault());
                            xm.LoadedProject.NewAddedTagIndex = 0;
                            string prevTopic = string.Empty;
                            int currentIndex = i;
                            while (i > 0 && prevTopic == string.Empty)
                            {
                                currentIndex--;
                                prevTopic = grdMain.Rows[currentIndex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                            }
                            xm.LoadedProject.NewAddedTagIndex = currentIndex;
                        }
                        else
                        {
                            int cindex = i;
                            while (topic == "")
                            {
                                cindex--;
                                topic = grdMain.Rows[cindex].Cells[grdMain.Columns["Topic"].Index].Value.ToString();
                                selkey = topic != "" ? Convert.ToInt32(grdMain.Rows[cindex].Cells[grdMain.Columns["Key"].Index].Value.ToString()) : selkey;
                            }
                            string keyname = grdMain.Rows[i].Cells[grdMain.Columns["Qos"].Index].Value.ToString();

                            var Sublist = xm.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                            Subscribe Selpub = Sublist.Where(n => n.key == selkey).FirstOrDefault();
                            if (cutTag.Count == 0)
                            {
                                cutTag.Add(Selpub.SubRequest.Where(n => n.key == Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value)).FirstOrDefault());
                            }
                            else
                            {
                                //cutTag.Clear();
                                cutTag.Add(Selpub.SubRequest.Where(n => n.key == Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value)).FirstOrDefault());
                            }
                            xm.LoadedProject.Devices.Remove(Selpub);
                            subscribeManager.SaveState();
                            Selpub.SubRequest.Remove(Selpub.SubRequest.Where(n => n.key == Convert.ToInt32(grdMain.Rows[i].Cells[grdMain.Columns["Key"].Index].Value)).FirstOrDefault());
                            xm.LoadedProject.Devices.Add(Selpub);
                            xm.LoadedProject.NewAddedTagIndex = cindex;
                        }
                    }
                }

            }
            else
            {
                int nameindex = grdMain.Columns["Name"].Index;
                foreach (int i in selectedcols)
                {
                    var slaveName = grdMain.Rows[i].Cells[nameindex].Value.ToString();

                    if (slaveName.Contains("MODBUSTCPServer"))
                    {
                        var mainnode = (MODBUSTCPServer)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                        MODBUSTCPServer_Request rtuslave = new MODBUSTCPServer_Request();
                        rtuslave = mainnode.Requests.Where(d => d.Name == slaveName).FirstOrDefault();
                        cutTag.Add(rtuslave);
                        modbusTCPServerManager.DeleteMODBUSTCPServerSlave(rtuslave);
                        mainnode.Requests.Remove(rtuslave);
                    }

                    else if (slaveName.Contains("MODBUSTCPClient"))
                    {
                        var mainnode = (MODBUSTCPClient)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                        MODBUSTCPClient_Slave rtuslave = new MODBUSTCPClient_Slave();
                        rtuslave = mainnode.Slaves.Where(d => d.Name == slaveName).FirstOrDefault();
                        cutTag.Add(rtuslave);
                        modbusTCPClientManager.DeleteMODBUSTCPClientSlave(rtuslave);
                        mainnode.Slaves.Remove(rtuslave);
                    }
                    else if (slaveName.Contains("MODBUSRTUMaster"))
                    {
                        var mainnode = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                        MODBUSRTUMaster_Slave rtuslave = new MODBUSRTUMaster_Slave();
                        rtuslave = mainnode.Slaves.Where(d => d.Name == slaveName).FirstOrDefault();
                        cutTag.Add(rtuslave);
                        ModbusRTUMasterManager.DeleteMODBUSRTUSlave(rtuslave);
                        mainnode.Slaves.Remove(rtuslave);
                    }
                    else if (slaveName.Contains("MODBUSRTUSlaves"))
                    {
                        var mainnode = (MODBUSRTUSlaves)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
                        MODBUSRTUSlaves_Slave rtuslave = new MODBUSRTUSlaves_Slave();
                        rtuslave = mainnode.Slaves.Where(d => d.Name == slaveName).FirstOrDefault();
                        cutTag.Add(rtuslave);
                        ModbusRTUSlaveManager.DeleteMODBUSRTUSlave(rtuslave);
                        mainnode.Slaves.Remove(rtuslave);
                    }

                }
                xm.LoadedProject.NewAddedTagIndex = selectedcols.Count > 0 ? selectedcols.Min() : 0;
                if (xm.LoadedProject.NewAddedTagIndex > 0)
                    xm.LoadedProject.NewAddedTagIndex = xm.LoadedProject.NewAddedTagIndex - 1;
            }
        }

        private void cntxCommentTag_Click(object sender, EventArgs e)
        {
            List<int> selectedcols = new List<int>();
            if (grdMain.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please Select Single Tag For Comment", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateUDFBEditPermission("comment"))
                return;
            foreach (DataGridViewRow gr in grdMain.SelectedRows)
            {
                selectedcols.Add(gr.Index);
                gr.DefaultCellStyle.ForeColor = System.Drawing.Color.SpringGreen;

                string commentedLogicalAddress = "'";
                XMIOConfig tagToComment = null;
                if (xm.CurrentScreen.ToString().Contains("Tags"))
                {
                    foreach (int i in selectedcols)
                    {
                        var currentSelected = i;
                        var tags = grdMain.Rows[currentSelected].DataBoundItem.ToString().Split(',').First().Split('=').Last().Trim();
                        tagToComment = xm.LoadedProject.Tags.Where(x => x.LogicalAddress == tags).FirstOrDefault();
                        commentedLogicalAddress += tags;
                    }

                }
                string actualAddress = commentedLogicalAddress.Replace("'", "");
                if (xm.LoadedProject.BacNetIP != null)
                {
                    if (actualAddress.StartsWith("F2") && xm.LoadedProject.BacNetIP.BinaryIOValues.Any(t => t.LogicalAddress.Equals(actualAddress)))
                    {
                        MessageBox.Show($"{actualAddress} is assign for Binary value in BacNet section.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (actualAddress.StartsWith("P5") && xm.LoadedProject.BacNetIP.AnalogIOValues.Any(t => t.LogicalAddress.Equals(actualAddress)))
                    {
                        MessageBox.Show($"{actualAddress} is assign for Analog value in BacNet section.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (actualAddress.StartsWith("W4") && xm.LoadedProject.BacNetIP.MultistateValues.Any(t => t.LogicalAddress.Equals(actualAddress)))
                    {
                        MessageBox.Show($"{actualAddress} is assign for Multistate value in BacNet section.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (actualAddress.StartsWith("W4") && xm.LoadedProject.BacNetIP.AnalogIOValues.Any(t => t.LogicalAddress.Equals(actualAddress)))
                    {
                        MessageBox.Show($"{actualAddress} is assign for Analog value in BacNet section.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                var blocks = XMProValidator.CheckInHSIOBlocks(actualAddress);
                if (blocks != null && blocks.Count > 0)
                {
                    MessageBox.Show("Tag already used in HSIO block please update that first.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
                XMIOConfig commentedTag = new XMIOConfig();
                commentedTag.LogicalAddress = commentedLogicalAddress;
                commentedTag.Tag = tagToComment.Tag;
                commentedTag.Type = tagToComment.Type;
                commentedTag.Label = tagToComment.Label;
                commentedTag.InitialValue = tagToComment.InitialValue;
                commentedTag.Model = tagToComment.Model;
                commentedTag.IoList = tagToComment.IoList;
                commentedTag.Retentive = tagToComment.Retentive ? true : false;
                commentedTag.RetentiveAddress = tagToComment.RetentiveAddress;
                commentedTag.ShowLogicalAddress = tagToComment.ShowLogicalAddress ? true : false;
                commentedTag.Key = tagToComment.Key;
                xm.LoadedProject.Tags.RemoveAll(d => d.LogicalAddress == tagToComment.LogicalAddress);
                xm.LoadedProject.Tags.Add(commentedTag);
            }
            xm.LoadedProject.NewAddedTagIndex = grdMain.SelectedRows[0].Index;
            OnShown();
        }

        private void grdMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (xm.CurrentScreen.Contains("User Defined Tags") || xm.CurrentScreen.EndsWith("UDFTags"))
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = grdMain.Rows[e.RowIndex];
                    DataGridViewCell cell = row.Cells[e.ColumnIndex];
                    if (cell.OwningColumn.Name == "LogicalAddress")
                    {
                        string cellValue = cell.Value.ToString();
                        if (cellValue.StartsWith("'"))
                        {
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGray;
                        }
                        else
                        {
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                        }
                    }
                }
            }
        }

        private void cntxUncommentTag_Click(object sender, EventArgs e)
        {
            if (grdMain.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please Selecte Commented Tag Only", "XMPS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<int> selectedcols = new List<int>();
            foreach (DataGridViewRow gr in grdMain.SelectedRows)
            {
                selectedcols.Add(gr.Index);
                gr.DefaultCellStyle.ForeColor = System.Drawing.Color.SpringGreen;

                string unCommentedLogicalAddress = "";
                XMIOConfig tagToUncomment = null;
                if (xm.CurrentScreen.ToString().Contains("Tags"))
                {
                    foreach (int i in selectedcols)
                    {
                        var currentSelected = i;
                        var tags = grdMain.Rows[currentSelected].DataBoundItem.ToString().Split(',').First().Split('=').Last().Trim();
                        tagToUncomment = xm.LoadedProject.Tags.Where(x => x.LogicalAddress == tags.Replace("'", "")).FirstOrDefault();
                        unCommentedLogicalAddress += tags;
                    }

                }
                if (tagToUncomment != null)
                {
                    var checkedAddress = xm.LoadedProject.Tags.Where(d => d.LogicalAddress == tagToUncomment.LogicalAddress.Replace("'", ""));
                    if (checkedAddress != null)
                    {
                        MessageBox.Show("Logical Address already used", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var alreadyCommentedTag = xm.LoadedProject.Tags.Where(x => x.LogicalAddress == unCommentedLogicalAddress).FirstOrDefault();
                    XMIOConfig commentedTag = new XMIOConfig();
                    commentedTag.LogicalAddress = alreadyCommentedTag.LogicalAddress.Replace("'", "");
                    commentedTag.Tag = alreadyCommentedTag.Tag;
                    commentedTag.Type = alreadyCommentedTag.Type;
                    commentedTag.Label = alreadyCommentedTag.Label;
                    commentedTag.InitialValue = alreadyCommentedTag.InitialValue;
                    commentedTag.Model = alreadyCommentedTag.Model;
                    commentedTag.IoList = alreadyCommentedTag.IoList;
                    commentedTag.Retentive = alreadyCommentedTag.Retentive ? true : false;
                    commentedTag.RetentiveAddress = alreadyCommentedTag.RetentiveAddress;
                    commentedTag.ShowLogicalAddress = alreadyCommentedTag.ShowLogicalAddress ? true : false;
                    commentedTag.Key = alreadyCommentedTag.Key;
                    xm.LoadedProject.Tags.RemoveAll(d => d.LogicalAddress == alreadyCommentedTag.LogicalAddress);
                    xm.LoadedProject.Tags.Add(commentedTag);
                }
            }
            prevScrollIndex = grdMain.FirstDisplayedScrollingRowIndex;
            xm.LoadedProject.NewAddedTagIndex = grdMain.SelectedRows[0].Index;
            OnShown();
        }

        private void cntxRemoveDisVar(object sender, EventArgs e)
        {
            List<int> selectedcols = new List<int>();

            foreach (DataGridViewRow gr in grdMain.SelectedRows)
            {
                selectedcols.Add(gr.Index);
            }
            string variable = "";
            string disableVariable = "";
            foreach (int i in selectedcols)
            {
                var currentrow = i;
                DataGridViewRow selectedRow = grdMain.Rows[i];
                variable = selectedRow.Cells["Variable"].Value.ToString();
                disableVariable = selectedRow.Cells["DisablingVariables"].Value.ToString();
            }
            //bit address to logical address
            string logicalAdd = XMProValidator.GetTheAddressFromTag(variable);
            var modBUSRTUMaster = (MODBUSRTUMaster)xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
            var addDisableVariable = modBUSRTUMaster.Slaves.FirstOrDefault(T => T.Variable == variable);

            if (addDisableVariable != null)
            {
                addDisableVariable.DisablingVariables = "0";
            }

            OnShown();
        }
        private void tsmAddResiValues_Click(object sender, EventArgs e)
        {
            if (grdMain.Rows.Count >= 20)
            {
                MessageBox.Show("You cannot add more than 20 resistance values.",
                                "XMPS 2000",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            using (var form = new frmAddResistanceValue())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {

                }
            }
            OnShown();
        }
        private void grdMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Delete && XMPS.Instance.PlcStatus != "LogIn")
            {
                string currentScreen = XMPS.Instance.CurrentScreen;
                if (currentScreen.Contains("frmAddResistanceValue") || currentScreen.Contains("ResistanceValue"))
                {
                    deleteResistanceValue();
                }
                else if (currentScreen.StartsWith("LookUpTbl"))
                {
                    return;
                }
                else
                {
                    deleteTagAndModbus();
                }
            }
        }

        private void cntxAddRequest_Click(object sender, EventArgs e)
        {
            xm.LoadedProject.NewAddedTagIndex = grdMain.SelectedRows[0].Index;
            DataGridViewRow gr = grdMain.SelectedRows[0];
            if (xm.CurrentScreen.ToString().Contains("MQTT Publish"))
            {
                var lstPublish = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
                var mpublist = lstPublish.Where(p => p.keyvalue == Convert.ToInt32(gr.Cells[3].EditedFormattedValue)).FirstOrDefault();
                int maxMQTTReqCount = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null)
                                         .SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MQTT").MaxCount ?? 0);
                if (mpublist.PubRequest.Count() >= maxMQTTReqCount)
                {
                    MessageBox.Show("Maximum limit of request addition is reached, requests can't be added", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                XMProForm tempForm = new XMProForm();
                tempForm.StartPosition = FormStartPosition.CenterParent;
                tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                tempForm.Text = "ADD PUBLISH REQUEST";
                PublishRequest userControl = new PublishRequest(Convert.ToInt32(gr.Cells[3].EditedFormattedValue));
                tempForm.Height = userControl.Height + 25;
                tempForm.Width = userControl.Width;
                userControl.Text = "ADD PUBLISH REQUEST";
                DialogResult status = userControl.ShowDialog();
                if (status == DialogResult.OK)
                {
                    publishManager.SaveState();
                    PubRequest pr = new PubRequest();
                    pr.topic = Convert.ToInt32(gr.Cells[3].EditedFormattedValue);
                    pr.req = userControl.Keyname;
                    pr.Tag = XMProValidator.GetTheAddressFromTag(userControl.Tagname);
                    //publishRequests.Add(pr);
                    var Listpublish = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
                    var mpublish = Listpublish.Where(p => p.keyvalue == Convert.ToInt32(gr.Cells[3].EditedFormattedValue)).FirstOrDefault();
                    if (mpublish != null)
                    {
                        xm.LoadedProject.Devices.Remove(mpublish);
                    }
                    //pr.Keyvalue = mpublish.PubRequest.Count + 1;
                    pr.Keyvalue = mpublish.PubRequest.Count > 0 ? mpublish.PubRequest.Max(k => k.Keyvalue) + 1 : 1;
                    Publish obj = new Publish();
                    obj = mpublish;
                    obj.AddPublishRequest(pr);
                    publishManager.UpdateOnlyForRequest(obj);
                    XMPS.Instance.LoadedProject.Devices.Add(obj);
                }
            }
            else if (xm.CurrentScreen.ToString().Contains("MQTT Subscribe"))
            {
                var lstSubList = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                var msublist = lstSubList.Where(p => p.key == Convert.ToInt32(gr.Cells[3].EditedFormattedValue)).FirstOrDefault();
                int maxMQTTReqCount = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null)
                                         .SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MQTT").MaxCount ?? 0);
                if (msublist.SubRequest.Count() >= maxMQTTReqCount)
                {
                    MessageBox.Show("Maximum limit of request addition is reached, requests can't be added", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                XMProForm tempForm = new XMProForm();
                tempForm.StartPosition = FormStartPosition.CenterParent;
                tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                tempForm.Text = "ADD SUBSCRIBE REQUEST";
                SuscribeRequest userControl = new SuscribeRequest(Convert.ToInt32(gr.Cells[3].EditedFormattedValue.ToString()));
                tempForm.Height = userControl.Height + 25;
                tempForm.Width = userControl.Width;
                userControl.Text = "ADD SUBSCRIBE REQUEST";
                DialogResult status = userControl.ShowDialog();
                if (status == DialogResult.OK)
                {
                    subscribeManager.SaveState();
                    SubscribeRequest sr = new SubscribeRequest();
                    sr.topic = Convert.ToInt32(gr.Cells[3].EditedFormattedValue.ToString());
                    sr.req = userControl.Keyname;
                    sr.Tag = XMProValidator.GetTheAddressFromTag(userControl.Tagname);
                    var Listsubscribe = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                    var msubscribe = Listsubscribe.Where(p => p.key == Convert.ToInt32(gr.Cells[3].EditedFormattedValue.ToString())).FirstOrDefault();
                    if (msubscribe != null)
                    {
                        xm.LoadedProject.Devices.Remove(msubscribe);
                    }
                    sr.key = msubscribe.SubRequest.Count > 0 ? msubscribe.SubRequest.Max(k => k.key) + 1 : 1;
                    Subscribe obj = new Subscribe();
                    obj = msubscribe;
                    obj.AddPublishRequest(sr);
                    subscribeManager.UpdateOnlyForRequest(obj);
                    XMPS.Instance.LoadedProject.Devices.Add(obj);
                }
            }
            OnShown();
        }

        private void grdMain_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && (xm.CurrentScreen.EndsWith("User Defined Tags") || xm.CurrentScreen.EndsWith("System Tags") || xm.CurrentScreen.EndsWith("UDFTags")))
            {
                DataGridViewColumn clickedColumn = grdMain.Columns[e.ColumnIndex];
                ShowFilteringOptions(clickedColumn);
            }
        }
        private void ShowFilteringOptions(DataGridViewColumn column)
        {
            ContextMenuStrip filterMenu = new ContextMenuStrip();
            string currentScreen = xm.CurrentScreen.Split('#')[1].ToString();
            List<string> currentDataTypes = null;
            if (currentScreen.Equals("System Tags"))
            {
                currentDataTypes = xm.LoadedProject.Tags.Where(T => T.Model == null || T.Model == "" && T.LogicalAddress.StartsWith("S3")).Select(T => T.Label).Distinct().ToList();
            }
            else if (currentScreen.Equals("User Defined Tags"))
            {
                currentDataTypes = xm.LoadedProject.Tags.Where(T => !T.LogicalAddress.StartsWith("S3") && T.Model.Equals("") ||
                            (T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                            T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO")).Select(T => T.Label).Distinct().ToList();
            }
            else if (currentScreen.Equals("UDFTags"))
            {
                currentDataTypes = xm.LoadedProject.Tags.Where(T => T.Model != null && T.Model == xm.CurrentScreen.Split('#')[0].ToString()).Select(T => T.Label).Distinct().ToList();
            }

            TextBox searchBox = new TextBox();
            searchBox.Top = 0;
            searchBox.Width = 140;
            searchBox.TextChanged += (sender, e) =>
            {

            };

            Button seachButton = new Button();
            seachButton.Text = "Load";
            seachButton.Top = searchBox.Top;
            seachButton.Left = searchBox.Right + 5;
            seachButton.Click += (sender, e) =>
            {
                string dataType = searchBox.Text;
                List<string> allDataType = DataType.List.Where(T => T.ID < 6 || T.ID > 11).Select(T => T.Text).ToList();
                if (allDataType.Any(t => t.ToLower().Equals(dataType.ToLower())))
                {
                    currentFilterDataType.Clear();
                    List<string> selectedDataType = DataType.List.Where(T => T.ID < 6 || T.ID > 11).Where(t => t.Text.ToLower().Equals(dataType.ToLower())).Select(t => t.Text).ToList();
                    currentFilterDataType.AddRange(selectedDataType);
                    ShowFilteredRows(selectedDataType);
                    filterMenu.Close();
                }
                else
                {
                    MessageBox.Show("Please enter valid datatype", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            };
            if (column.Index == 0)
            {
                if (grdMain.Columns.Contains("Online Tag"))
                    grdMain.Columns.Remove("Online Tag");
                List<string> LogicalAddress = GetAllLogicalAddressAndTagNames(column.Index);
                FilterFormPopUp filterFormPop = new FilterFormPopUp(LogicalAddress);
                ToolStripControlHost host = new ToolStripControlHost(filterFormPop);
                if (currentFilterDataType.Count == 0)
                {
                    OnShown();
                }
                host.AutoSize = true;
                filterMenu.Items.Add(host);
                host.Margin = Padding.Empty;
                host.Padding = Padding.Empty;
                filterMenu.Show(Cursor.Position);
                filterFormPop.SelectedValueLogicalAddChanged += (sender, selectedValue) =>
                {
                    if (grdMain.Columns.Contains("Online Tag"))
                        grdMain.Columns.Remove("Online Tag");
                    if (currentScreen.Equals("System Tags"))
                    {
                        selectedValue.ToUpper();
                        if (selectedValue != null && selectedValue != "")
                        {
                            if (currentFilterDataType.Count > 0)
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => (T.Model == null || T.Model == "") && T.LogicalAddress.StartsWith(selectedValue) && currentFilterDataType.Contains(T.Label)).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).OrderBy(t => t.LogicalAddress).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                            else
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => (T.Model == null || T.Model == "") && T.LogicalAddress.StartsWith(selectedValue)).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).OrderBy(t => t.LogicalAddress).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                        }
                    }
                    else if (currentScreen.Equals("User Defined Tags"))
                    {
                        if (grdMain.Columns.Contains("Online Tag"))
                            grdMain.Columns.Remove("Online Tag");
                        selectedValue.ToUpper();
                        if (selectedValue != null && selectedValue != "")
                        {
                            if (currentFilterDataType.Count > 0)
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith(selectedValue) && T.Model != null && T.Model.Equals("") && currentFilterDataType.Contains(T.Label) ||
                                        (T.Model != null && T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                                        T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO" && T.LogicalAddress.StartsWith(selectedValue) && currentFilterDataType.Contains(T.Label))).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                            else
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith(selectedValue) && T.Model != null && T.Model.Equals("") ||
                                        (T.Model != null && T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                                        T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO" && T.LogicalAddress.StartsWith(selectedValue))).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                        }
                    }
                    else if (currentScreen.Equals("UDFTags"))
                    {
                        if (grdMain.Columns.Contains("Online Tag"))
                            grdMain.Columns.Remove("Online Tag");
                        selectedValue.ToUpper();
                        if (selectedValue != null && selectedValue != "")
                        {
                            if (currentFilterDataType.Count > 0)
                            {
                                // ShowFilteredRows(currentFilterDataType, selectedValue);
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith(selectedValue) && currentFilterDataType.Contains(T.Label) && T.Model != null && T.Model.Equals("") ||
                                        (T.Model != null && T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                                        T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO" && T.LogicalAddress.StartsWith(selectedValue) && currentFilterDataType.Contains(T.Label))).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                            else
                            {
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith(selectedValue) && T.Model != null && T.Model.Equals("") ||
                                        (T.Model != null && T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                                        T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO" && T.LogicalAddress.StartsWith(selectedValue))).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                        }
                    }
                    ShowingFilterIcons();
                };
                filterFormPop.SelectedIndexLogicalAddChanged += (sender, selectedIndex) =>
                {
                    filteredRowsData.Clear();
                    if (grdMain.Columns.Contains("Online Tag"))
                        grdMain.Columns.Remove("Online Tag");
                    grdMain.DataSource = null;
                    List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith(selectedIndex)).ToList();
                    grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).OrderBy(t => t.LogicalAddress).ToList();
                    filteredRowsData.AddRange(data);
                    if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                    {
                        AddSelectCheckboxColumn();
                        SetCheckboxValues();
                    }
                    ShowingFilterIcons();
                };
            }
            if (column.Index == 1)
            {
                if (grdMain.Columns.Contains("Online Tag"))
                    grdMain.Columns.Remove("Online Tag");
                List<string> LogicalAddress = GetAllLogicalAddressAndTagNames(column.Index);
                FilterFormPopUp filterFormPop = new FilterFormPopUp(LogicalAddress, column.Index);
                ToolStripControlHost host = new ToolStripControlHost(filterFormPop);
                if (currentFilterDataType.Count == 0)
                {
                    OnShown();
                }
                host.AutoSize = true;
                filterMenu.Items.Add(host);
                host.Margin = Padding.Empty;
                host.Padding = Padding.Empty;
                filterMenu.Show(Cursor.Position);
                filterFormPop.SelectedValueTagsChanged += (sender, selectedValue) =>
                {
                    if (currentScreen.Equals("System Tags"))
                    {
                        if (grdMain.Columns.Contains("Online Tag"))
                            grdMain.Columns.Remove("Online Tag");
                        selectedValue.ToUpper();
                        if (selectedValue != null && selectedValue != "")
                        {
                            if (currentFilterDataType.Count > 0)
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => (T.Model == null || T.Model == "") && T.Tag.StartsWith(selectedValue) && currentFilterDataType.Contains(T.Label)).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).OrderBy(t => t.LogicalAddress).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                            else
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => (T.Model == null || T.Model == "") && T.Tag.StartsWith(selectedValue)).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).OrderBy(t => t.LogicalAddress).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                        }
                    }
                    else if (currentScreen.Equals("User Defined Tags"))
                    {
                        if (grdMain.Columns.Contains("Online Tag"))
                            grdMain.Columns.Remove("Online Tag");
                        selectedValue.ToUpper();
                        if (selectedValue != null && selectedValue != "")
                        {
                            if (currentFilterDataType.Count > 0)
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.Tag.StartsWith(selectedValue) && T.Model.Equals("") && T.Model != null && currentFilterDataType.Contains(T.Label) ||
                                        (T.Model != null && T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                                        T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO" && T.Tag.StartsWith(selectedValue) && currentFilterDataType.Contains(T.Label))).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                            else
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.Tag.StartsWith(selectedValue) && T.Model != null && T.Model.Equals("") ||
                                        (T.Model != null && T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                                        T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO" && T.Tag.StartsWith(selectedValue))).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                        }
                    }
                    else if (currentScreen.Equals("UDFTags"))
                    {
                        if (grdMain.Columns.Contains("Online Tag"))
                            grdMain.Columns.Remove("Online Tag");
                        selectedValue.ToUpper();
                        if (selectedValue != null && selectedValue != "")
                        {
                            if (currentFilterDataType.Count > 0)
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.Tag.StartsWith(selectedValue) && T.Model != null && T.Model.Equals("") && currentFilterDataType.Contains(T.Label) ||
                                        (T.Model != null && T.Model == $"{xm.CurrentScreen.Split(' ')[0]} Tags" && T.IoList.ToString() != "OnBoardIO" &&
                                        T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO" && T.Tag.StartsWith(selectedValue) && currentFilterDataType.Contains(T.Label))).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                            else
                            {
                                filteredRowsData.Clear();
                                grdMain.DataSource = null;
                                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.Tag.StartsWith(selectedValue) && T.Model != null && T.Model.Equals("") ||
                                        (T.Model != null && T.Model == $"{xm.CurrentScreen.Split(' ')[0]} Tags" && T.IoList.ToString() != "OnBoardIO" &&
                                        T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO" && T.Tag.StartsWith(selectedValue))).ToList();
                                grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                                filteredRowsData.AddRange(data);
                                if (data == null || data.Count == 0)
                                    OnShown();
                            }
                        }
                    }
                    ShowingFilterIcons();
                };
                filterFormPop.SelectedIndexTagsChnaged += (sender, selectedIndex) =>
                {
                    if (grdMain.Columns.Contains("Online Tag"))
                        grdMain.Columns.Remove("Online Tag");
                    filteredRowsData.Clear();
                    grdMain.DataSource = null;
                    List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.Tag.Equals(selectedIndex)).ToList();
                    grdMain.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).OrderBy(t => t.LogicalAddress).ToList();
                    filteredRowsData.AddRange(data);
                    if (xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                    {
                        AddSelectCheckboxColumn();
                        SetCheckboxValues();
                    }
                    ShowingFilterIcons();
                };
            }
            Panel panel = new Panel();
            if (column.Index == 2)
            {
                panel.Controls.Add(searchBox);
                panel.Controls.Add(seachButton);
                panel.AutoSize = true;
                int bottom = 0;
                foreach (string dataType in currentDataTypes)
                {

                    CheckBox ch1 = new CheckBox();
                    ch1.Text = dataType;
                    ch1.Top = searchBox.Bottom + 10 + bottom;
                    bottom += 24;
                    if (currentFilterDataType.Contains(dataType))
                        ch1.Checked = true;
                    panel.Controls.Add(ch1);
                }
                //adding OK button and its event to adding filter over the Tags
                Button okButton = new Button();
                okButton.Text = "OK";
                okButton.Top = bottom + 34;
                List<CheckBox> selectedCheckBoxes = new List<CheckBox>();
                okButton.Click += (sender, e) =>
                {
                    foreach (Control control in panel.Controls)
                    {
                        if (control is CheckBox checkBox && checkBox.Checked)
                        {
                            selectedCheckBoxes.Add(checkBox);
                        }
                    }
                    if (selectedCheckBoxes.Count > 0)
                    {
                        currentFilterDataType.Clear();
                        List<string> selectedDataType = selectedCheckBoxes.Select(T => T.Text).ToList();
                        currentFilterDataType.AddRange(selectedDataType);
                        ShowFilteredRows(selectedDataType);
                    }
                    else
                    {
                        currentFilterDataType.Clear();
                        filteredRowsData.Clear();
                        OnShown();
                    }
                    filterMenu.Close();
                };

                //adding Cancel button an its event to close the filter form
                Button cancelButton = new Button();
                cancelButton.Text = "Cancel";
                cancelButton.Top = okButton.Top;
                cancelButton.Left = okButton.Right + 10;
                cancelButton.Click += (sender, e) =>
                {
                    //currentFilterDataType.Clear();
                    filterMenu.Close();
                };

                panel.Controls.Add(okButton);
                panel.Controls.Add(cancelButton);
                ToolStripControlHost host = new ToolStripControlHost(panel);
                host.AutoSize = true;
                host.Margin = Padding.Empty;
                host.Padding = Padding.Empty;
                filterMenu.Items.Add(host);
                filterMenu.Show(Cursor.Position);
            }

        }

        private List<string> GetAllLogicalAddressAndTagNames(int index)
        {
            List<string> data = new List<string>();
            string currentScreen = xm.CurrentScreen.Split('#')[1].ToString();
            if (currentFilterDataType.Count > 0)
            {
                if (currentScreen.Equals("System Tags"))
                {
                    if (index == 0)
                        data = xm.LoadedProject.Tags.Where(T => (T.Model == null || T.Model == "") && T.LogicalAddress.StartsWith("S3") && currentFilterDataType.Contains(T.Label)).Select(T => T.LogicalAddress).ToList();
                    else
                        data = xm.LoadedProject.Tags.Where(T => (T.Model == null || T.Model == "") && T.LogicalAddress.StartsWith("S3") && currentFilterDataType.Contains(T.Label)).Select(T => T.Tag).ToList();
                }
                else if (currentScreen.Equals("User Defined Tags"))
                {
                    if (index == 0)
                    {
                        data = xm.LoadedProject.Tags.Where(T => !T.LogicalAddress.StartsWith("S3") && T.Model.Equals("") && currentFilterDataType.Contains(T.Label) ||
                            (T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                            T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO") && currentFilterDataType.Contains(T.Label)).Select(T => T.LogicalAddress).ToList();
                    }
                    else
                    {
                        data = xm.LoadedProject.Tags.Where(T => !T.LogicalAddress.StartsWith("S3") && T.Model.Equals("") && currentFilterDataType.Contains(T.Label) ||
                            (T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                            T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO") && currentFilterDataType.Contains(T.Label)).Select(T => T.Tag).ToList();
                    }

                }
                else if (currentScreen.Equals("UDFTags"))
                {
                    if (index == 0)
                        data = xm.LoadedProject.Tags.Where(T => T.Model != null && T.Model == xm.CurrentScreen.Split('#')[0].ToString() && currentFilterDataType.Contains(T.Label)).Select(T => T.LogicalAddress).ToList();
                    else
                        data = xm.LoadedProject.Tags.Where(T => T.Model != null && T.Model == xm.CurrentScreen.Split('#')[0].ToString() && currentFilterDataType.Contains(T.Label)).Select(T => T.Tag).ToList();
                }

            }
            else
            {
                if (currentScreen.Equals("System Tags"))
                {
                    if (index == 0)
                        data = xm.LoadedProject.Tags.Where(T => (T.Model == null || T.Model == "") && T.LogicalAddress.StartsWith("S3")).Select(T => T.LogicalAddress).ToList();
                    else
                        data = xm.LoadedProject.Tags.Where(T => (T.Model == null || T.Model == "") && T.LogicalAddress.StartsWith("S3")).Select(T => T.Tag).ToList();
                }
                else if (currentScreen.Equals("User Defined Tags"))
                {
                    if (index == 0)
                    {
                        data = xm.LoadedProject.Tags.Where(T => !T.LogicalAddress.StartsWith("S3") && T.Model.Equals("") ||
                            (T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                            T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO")).Select(T => T.LogicalAddress).ToList();
                    }
                    else
                    {
                        data = xm.LoadedProject.Tags.Where(T => !T.LogicalAddress.StartsWith("S3") && T.Model.Equals("") ||
                            (T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                            T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO")).Select(T => T.Tag).ToList();
                    }

                }
                else if (currentScreen.Equals("UDFTags"))
                {
                    if (index == 0)
                        data = xm.LoadedProject.Tags.Where(T => T.Model != null && T.Model == xm.CurrentScreen.Split('#')[0].ToString()).Select(T => T.LogicalAddress).ToList();
                    else
                        data = xm.LoadedProject.Tags.Where(T => T.Model != null && T.Model == xm.CurrentScreen.Split('#')[0].ToString()).Select(T => T.Tag).ToList();
                }
            }
            return data;
        }

        private void ShowFilteredRows(List<string> selectedDataType)
        {
            if (grdMain.Columns.Contains("Online Tag"))
                grdMain.Columns.Remove("Online Tag");
            string currentScreen = xm.CurrentScreen.Split('#')[1].ToString();
            BindingSource bs = new BindingSource();
            if (currentScreen.Equals("System Tags"))
            {
                filteredRowsData.Clear();
                grdMain.DataSource = null;
                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => (T.Model == null || T.Model == "") && T.LogicalAddress.StartsWith("S3") && selectedDataType.Contains(T.Label)).ToList();
                bs.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).OrderBy(t => t.LogicalAddress).ToList();
                grdMain.DataSource = bs;
                filteredRowsData.AddRange(data);
            }
            else if (currentScreen.Equals("User Defined Tags"))
            {
                filteredRowsData.Clear();
                grdMain.DataSource = null;
                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => !T.LogicalAddress.StartsWith("S3") && selectedDataType.Contains(T.Label) && T.Model.Equals("") ||
                        (T.Model == "User Defined Tags" && T.IoList.ToString() != "OnBoardIO" &&
                        T.IoList.ToString() != "ExpansionIO" && T.IoList.ToString() != "RemoteIO" && selectedDataType.Contains(T.Label))).ToList();
                bs.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).ToList();
                grdMain.DataSource = bs;
                filteredRowsData = data;
            }
            else if (currentScreen.Equals("UDFTags"))
            {
                filteredRowsData.Clear();
                List<XMIOConfig> data = xm.LoadedProject.Tags.Where(T => T.Model != null && T.Model == xm.CurrentScreen.Split('#')[0].ToString() && selectedDataType.Contains(T.Label)).ToList();
                bs.DataSource = data.Select(t => new { t.LogicalAddress, t.Tag, DataType = string.Format(t.Label), t.InitialValue, t.Retentive, t.RetentiveAddress, t.ShowLogicalAddress }).OrderBy(t => t.LogicalAddress).ToList();
                grdMain.DataSource = bs;
                filteredRowsData = data;
            }
            if (xm.LoadedProject.PlcModel.StartsWith("XBLD") && xm.SelectedNode.Info.Contains("User Defined Tags"))
            {
                AddSelectCheckboxColumn();
                SetCheckboxValues();
            }
            ShowingFilterIcons();
        }

        private void ShowingFilterIcons()
        {
            grdMain.Columns["LogicalAddress"].HeaderText = "LogicalAddress    ˅";
            grdMain.Columns["Tag"].HeaderText = "Tag    ˅";
            grdMain.Columns["DataType"].HeaderText = "DataType    ˅";
        }

        private void cntxCrossReferance_Click(object sender, EventArgs e)
        {
            if (grdMain.SelectedRows.Count > 0)
            {
                if (!ValidateUDFBEditPermission("view"))
                    return;
                string tagName = grdMain.SelectedRows[0].Cells["Tag"].Value?.ToString();
                frmMain fm = Application.OpenForms.OfType<frmMain>().FirstOrDefault();
                if (fm != null)
                {
                    fm.InvokeCrossReference(tagName);
                }
            }
        }

        private void frmGridLayout_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain.GridDataChanged -= OnDataChanged;
        }
    }

    public static class DataGridViewExtensions
    {
        public static void EnableDoubleBuffering(this DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgv, new object[] { true });

            // Enable additional styles needed for double buffering
            dgv.GetType().InvokeMember("SetStyle",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
                null, dgv, new object[] { ControlStyles.OptimizedDoubleBuffer |
                                      ControlStyles.AllPaintingInWmPaint |
                                      ControlStyles.UserPaint, true });

            // Update the control styles
            dgv.GetType().InvokeMember("UpdateStyles",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
                null, dgv, null);
        }
    }
}