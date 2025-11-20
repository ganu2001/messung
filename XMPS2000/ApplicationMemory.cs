using iTextSharp.text.log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.Types;

namespace XMPS2000
{
    public partial class ApplicationMemory : Form, IXMForm
    {
        private const int PROGRAM_MEMORY_PER_ROW = 68;
        private int AVAILABLE_PROGRAM_MEMORY = (int)XMPS.Instance.DeviceMemory.AvlblProgMemory ;
        private int AVAILABLE_ADDRESS_MEMORY = (int)XMPS.Instance.DeviceMemory.AvlblAddressMemory;
        private int AVAILABLE_RETENTIVE_MEMORY = (int)XMPS.Instance.DeviceMemory.AvlblRetentiveMemory;
        private int AVAILABLE_MODBUS_RTU_MASTER_MEMORY = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.RS485 != null).SelectMany(t => t.RS485.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MODBUSRTUMaster").MaxCount ?? 0);
        private int AVAILABLE_MODBUS_TCP_CLIENT_MEMORY = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null).SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MODBUSTCPClient").MaxCount ?? 0);
        private int AVAILABLE_MODBUS_TCP_SERVER_MEMORY = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null).SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MODBUSTCPServer")?.MaxCount ?? 0);
        private int AVAILABLE_MODBUS_RTU_SLAVE_MEMORY = XMPS.Instance.LoadedProject.CPUDatatype == "Real" ? 0 : 100;
        private const int AVAILABLE_MQTT_MEMORY = 200;

        
        private readonly DataTable _mainTable = new DataTable();
        private readonly List<AppDetails> _appDetails = new List<AppDetails>();

        public ApplicationMemory()
        {
            InitializeComponent();

            try
            {
                OnShown();
            }
            catch (Exception ex)
            {
                HandleException("Error initializing application memory view", ex);
            }
        }

        public void OnShown()
        {
            try
            {
                InitializeMainTable();
                GetMemoryAllocationSummary();
                ConfigureGridsAndChart();
            }
            catch (Exception ex)
            {
                HandleException("Error showing application memory data", ex);
            }
        }

        private void HandleException(string message, Exception ex)
        {
            // Show user-friendly message
            MessageBox.Show(
                $"{message}. {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void InitializeMainTable()
        {
            _mainTable.Columns.Add("Type");
            _mainTable.Columns.Add("Total Memory (bytes)");
            _mainTable.Columns.Add("Inuse Memory (bytes)");
            _mainTable.Columns.Add("Free Memory (bytes)");
        }

        private void ConfigureGridsAndChart()
        {
            if (_appDetails == null || _mainTable == null)
            {
                throw new InvalidOperationException("Memory data tables not initialized properly");
            }

            GrdDetails.DataSource = _appDetails;
            GrdSummary.DataSource = _mainTable;

        }

        private void GetMemoryAllocationSummary()
        {
            try
            {
                _mainTable.Rows.Clear();
                _appDetails.Clear();

                // Validate project is loaded
                if (XMPS.Instance?.LoadedProject == null)
                {
                    throw new InvalidOperationException("No project is currently loaded");
                }
                // Calculate memory usage
                double programMemory = CommonFunctions.CalculateProgramMemory();
                double totalAddressMemory = CommonFunctions.CalculateTotalAddressMemory();
                double totalRetentiveMemory = CommonFunctions.CalculateRetentiveMemory();
                // Add rows to the memory table
                AddMemoryTableRows(programMemory, totalAddressMemory, totalRetentiveMemory);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to calculate memory allocation", ex);
            }
        }





        private void AddMemoryTableRows(double programMemory, double totalAddressMemory, double totalRetentiveMemory)
        {
            try
            {
                if (_mainTable == null)
                {
                    throw new InvalidOperationException("Main table not initialized");
                }

                double totalMemoryInUse = programMemory + totalAddressMemory + totalRetentiveMemory;

                // Add rows for different memory types using a more efficient approach
                AddMemoryRow("Program Memory (bytes)", AVAILABLE_PROGRAM_MEMORY, programMemory);
                AddMemoryRow("User Address Memory (bytes)", AVAILABLE_ADDRESS_MEMORY, totalAddressMemory);
                AddMemoryRow("Retentive Memory (bytes)", AVAILABLE_RETENTIVE_MEMORY, totalRetentiveMemory);

                var rtuMaster = XMPS.Instance.LoadedProject?.Devices?.FirstOrDefault(d => d.GetType().Name == "MODBUSRTUMaster") as MODBUSRTUMaster;
                int rtuMasterCount = rtuMaster?.Slaves?.Count() ?? 0;
                AddMemoryRow("Modbus RTU Master (requests)", AVAILABLE_MODBUS_RTU_MASTER_MEMORY, rtuMasterCount);

                var tcpClient = XMPS.Instance.LoadedProject?.Devices?.FirstOrDefault(d => d.GetType().Name == "MODBUSTCPClient") as MODBUSTCPClient;
                int tcpClientCount = tcpClient?.Slaves?.Count() ?? 0;
                AddMemoryRow("Modbus TCP Client (requests)", AVAILABLE_MODBUS_TCP_CLIENT_MEMORY, tcpClientCount);

                var tcpServer = XMPS.Instance.LoadedProject?.Devices?.FirstOrDefault(d => d.GetType().Name == "MODBUSTCPServer") as MODBUSTCPServer;
                int tcpServerCount = tcpServer?.Requests?.Count() ?? 0;
                AddMemoryRow("Modbus TCP Server (requests)", AVAILABLE_MODBUS_TCP_SERVER_MEMORY, tcpServerCount);

                var rtuSlave = XMPS.Instance.LoadedProject?.Devices?.FirstOrDefault(d => d.GetType().Name == "MODBUSRTUSlaves") as MODBUSRTUSlaves;
                int rtuSlaveCount = rtuSlave?.Slaves?.Count() ?? 0;
                AddMemoryRow("Modbus RTU Slave (requests)", AVAILABLE_MODBUS_RTU_SLAVE_MEMORY, rtuSlaveCount);

                int mqttClientCount = XMPS.Instance.LoadedProject?.Devices?.Count(d => d.GetType().Name == "Subscribe" || d.GetType().Name == "Publish") ?? 0;
                AddMemoryRow("MQTT Client (requests) (pub+sub)", AVAILABLE_MQTT_MEMORY, mqttClientCount);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding memory table rows", ex);
            }
        }

        private void AddMemoryRow(string type, int totalMemory, double usedMemory)
        {
            DataRow row = _mainTable.NewRow();
            row["Type"] = type;
            row["Total Memory (bytes)"] = totalMemory;
            row["Inuse Memory (bytes)"] = usedMemory;
            row["Free Memory (bytes)"] = totalMemory - usedMemory;
            _mainTable.Rows.Add(row);
        }
        public class AppDetails
        {
            public string Type { get; set; }
            public double Allocation { get; set; }

            public AppDetails(string type, double allocation)
            {
                Type = !string.IsNullOrEmpty(type) ? type : throw new ArgumentNullException(nameof(type));
                Allocation = allocation;
            }
        }
    }
}