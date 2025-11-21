using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.Interpreter
{
    public class ConfigInterpreter1
    {
        private XMProject _loadedProject;
        public ConfigInterpreter1(XMPS xm)
        {
            _loadedProject = xm.LoadedProject;
        }
        public List<byte> GetHexValues()
        {
            List<byte> configSettingsHex = new List<byte> { };
            //PLC onboard IO setting & count
            configSettingsHex.AddRange(GetPLCOnBoard());
            //Expansion IO
            configSettingsHex.AddRange(GetExpansionIO());
            //Remote IO
            configSettingsHex.AddRange(GetRemoteIO());
            configSettingsHex.AddRange(GetModBusRTURequests());
            configSettingsHex.AddRange(GetModBusTCPServerRequests());
            configSettingsHex.AddRange(GetModBusTCPClientRequests());
            configSettingsHex.AddRange(getRetentiveAddressHex());
            configSettingsHex.AddRange(getInitialAddressHex());
            configSettingsHex.Add(Convert.ToByte('&'));
            //List<byte> InitialValueHex = GetModBusTCPServerRequests();
            //List<byte> InitialValueHex = GetModBusTCPClientRequests();
            //InitialValueHex.AddRange(getRetentiveAddressHex());
            //List<byte> InitialValueHex = getInitialAddressHex();
            //InitialValueHex.AddRange(GetModBusTCPClientRequests());

            return configSettingsHex;
        }
        #region PLC On Board
        private List<byte> GetPLCOnBoard()
        {
            List<byte> OnBoard = new List<byte> { };
            
            List<Mode> AnalogModes = Mode.List;
            var OnBoardIOForMode = _loadedProject.Tags.Where(s => s.IoList == Core.Types.IOListType.OnBoardIO && s.Type.ToString().StartsWith("Analog")).ToList();
            for (int cnt = 0; cnt < OnBoardIOForMode.Count; cnt++)
            {
                if (OnBoardIOForMode[cnt].Mode != null)
                {
                    OnBoard.Add(byte.Parse(AnalogModes.Where(f => f.Text != "" && f.Text != null && f.Text == OnBoardIOForMode[cnt].Mode.ToString()).Select(x => x.ID).First().ToString()));
                }
                else
                {
                    OnBoard.Add(0);
                }
            }
            OnBoard.Add(byte.Parse(_loadedProject.Tags.Where(s => s.IoList == Core.Types.IOListType.OnBoardIO && s.Type == Core.Types.IOType.DigitalInput).Count().ToString()));
            OnBoard.Add(byte.Parse(_loadedProject.Tags.Where(s => s.IoList == Core.Types.IOListType.OnBoardIO && s.Type == Core.Types.IOType.DigitalOutput).Count().ToString()));
            OnBoard.Add(byte.Parse(_loadedProject.Tags.Where(s => s.IoList == Core.Types.IOListType.OnBoardIO && s.Type == Core.Types.IOType.AnalogInput).Count().ToString()));
            OnBoard.Add(byte.Parse(_loadedProject.Tags.Where(s => s.IoList == Core.Types.IOListType.OnBoardIO && s.Type == Core.Types.IOType.AnalogOutput).Count().ToString()));
            return OnBoard;
        }
        #endregion 
        #region Expansion IO
        private List<byte> GetExpansionIO()
        {
            List<byte> ExpansionIO = new List<byte> { };
            var AllModels = _loadedProject.Tags.GroupBy(e => e.Model).Select(grp => grp.First());
            var Expansion = AllModels.Where(s => s.IoList == Core.Types.IOListType.ExpansionIO).ToList();
            for (int cnt = 0; cnt < Expansion.Count; cnt++)
            {
                if (cnt == 0)
                {
                    ExpansionIO.Add(byte.Parse(Expansion.Count.ToString()));
                    ExpansionIO.Add(0x35);
                }
                else
                {
                    ExpansionIO.Add(0x35);
                }
                ExpansionIO.Add(GetModelNo(Expansion[cnt].Model.ToString()));

                List<Mode> AnalogModes = Mode.List;
                var ExpansionIOForMode = _loadedProject.Tags.Where(s => s.Model == Expansion[cnt].Model.ToString() && s.Type.ToString().StartsWith("Analog")).ToList();
                for (int Ecnt = 0; Ecnt < ExpansionIOForMode.Count; Ecnt++)
                {
                    if (ExpansionIOForMode[Ecnt].Mode != null)
                    {
                        ExpansionIO.Add(byte.Parse(AnalogModes.Where(f => f.Text != "" && f.Text != null && f.Text == ExpansionIOForMode[Ecnt].Mode.ToString()).Select(x => x.ID).First().ToString()));
                    }
                    else
                    {
                        ExpansionIO.Add(0);
                    }
                }
                
                var ios = _loadedProject.Tags.Where(e => e.Model == Expansion[cnt].Model.ToString()).ToList();
                for (int io = 0; io < ios.Count; io++)
                {
                    string hexval = XMPS.Instance.GetHexAddress(ios[io].LogicalAddress.ToString());
                    ExpansionIO.AddRange(BitConverter.GetBytes(Int32.Parse(hexval, System.Globalization.NumberStyles.HexNumber)));
                }
            }

            return ExpansionIO;
        }
        #endregion 
        #region Remote IO
        private List<byte> GetRemoteIO()
        {
            List<byte> RemoteIOs = new List<byte> { };
            var AllModels = _loadedProject.Tags.GroupBy(e => e.Model).Select(grp => grp.First());
            var remote = AllModels.Where(s => s.IoList == Core.Types.IOListType.RemoteIO).ToList();
            for (int cnt = 0; cnt < remote.Count; cnt++)
            {
                if (cnt == 0)
                {
                    RemoteIOs.Add(byte.Parse(remote.Count.ToString()));
                    RemoteIOs.Add(0x36);
                }
                else
                {
                    RemoteIOs.Add(0x36);
                }

                RemoteIOs.Add(GetModelNo(remote[cnt].Model.ToString()));

                List<Mode> AnalogModes = Mode.List;
                var RemoteIOForMode = _loadedProject.Tags.Where(s => s.Model == remote[cnt].Model.ToString() && s.Type.ToString().StartsWith("Analog")).ToList();
                for (int Rcnt = 0; Rcnt < RemoteIOForMode.Count; Rcnt++)
                {
                    if (RemoteIOForMode[Rcnt].Mode != null)
                    {
                        RemoteIOs.Add(byte.Parse(AnalogModes.Where(f => f.Text != "" && f.Text != null && f.Text == RemoteIOForMode[Rcnt].Mode.ToString()).Select(x => x.ID).First().ToString()));
                    }
                    else
                    {
                        RemoteIOs.Add(0);
                    }
                }

                var ios = _loadedProject.Tags.Where(e => e.Model == remote[cnt].Model.ToString()).ToList();
                for (int io = 0; io < ios.Count; io++)
                {
                    string hexval = XMPS.Instance.GetHexAddress(ios[io].LogicalAddress.ToString());
                    RemoteIOs.AddRange(BitConverter.GetBytes(Int32.Parse(hexval, System.Globalization.NumberStyles.HexNumber)));
                }
            }

            return RemoteIOs;
        }

        private byte GetModelNo(string Model)
        {
            switch (Model)
            {
                case "XM-DO-4R": return 0x01;
                case "XM-DO-8R": return 0x02;
                case "XM-DO-16R": return 0x03;
                case "XM-DI-4": return 0x04;
                case "XM-DI-8": return 0x05;
                case "XM-DI-16": return 0x06;
                case "XM-AI-2": return 0x07;
                case "XM-AI-4": return 0x08;
                case "XM-AO-2": return 0x09;
                case "XM-AO-4": return 0x0A;
                case "XM-DI8-DO6T": return 0x0B;
                case "XM-DI8-DO6R": return 0x0C;
                case "XM-AI2-AO2": return 0x0D;
                case "XM-DO-4T": return 0x0E;
                case "XM-DO-8T": return 0x0F;
                case "XM-DO-16T": return 0x10;
                case "MOD-DO-4R": return 0x81;
                case "MOD-DO-8R": return 0x82;
                case "MOD-DO-16R": return 0x83;
                case "MOD-DI-4": return 0x84;
                case "MOD-DI-8": return 0x85;
                case "MOD-DI-16": return 0x86;
                case "MOD-AI-2": return 0x87;
                case "MOD-AI-4": return 0x88;
                case "MOD-AO-2": return 0x89;
                case "MOD-AO-4": return 0x8A;
                case "MOD-DI8-DO6": return 0x8B;
                case "MOD-AI2-AO2": return 0x8C;
                case "MOD-CFC-4": return 0x8D;
                case "MOD-DIM-2P": return 0x8E;
                case "MOD-DIM-4P": return 0x8F;
                case "MOD-DO-4RS": return 0x90;
                case "MOD-DO-8RS": return 0x91;
                case "MOD-DO-16RS": return 0x92;
                case "MOD-CTP-6": return 0x93;
                case "OTHERS": return 0x94;
            }

            return 0;
        }
        #endregion

        #region Inital Address
        private List<byte> getInitialAddressHex()
        {
            List<byte> InitialAddresssHex = new List<byte> { };
            List<XMIOConfig> data = _loadedProject.Tags;
            Int16 inputstr;
            DataTable dt = ToDataTable(data.Where(r => r.InitialValue != null && r.InitialValue.Length > 0).ToList());
            InitialAddresssHex.Add(0x3B);
            int RowCount = dt.Rows.Count;
            inputstr = Int16.Parse(RowCount.ToString("X"), System.Globalization.NumberStyles.HexNumber);
            InitialAddresssHex.AddRange(BitConverter.GetBytes(inputstr).ToList());
            foreach (DataRow dr in dt.Rows)
            {
                string hexval;
                hexval = XMPS.Instance.GetHexAddress(dr["LogicalAddress"].ToString());
                InitialAddresssHex.AddRange(BitConverter.GetBytes(Int32.Parse(hexval, System.Globalization.NumberStyles.HexNumber)));
                hexval = dr["InitialValue"].ToString();
                InitialAddresssHex.AddRange(BitConverter.GetBytes(Int32.Parse(hexval, System.Globalization.NumberStyles.HexNumber)));
            }
            return InitialAddresssHex;
        }

        /// <summary>
        /// Convert Data from List of objects to the Satatable
        /// </summary>
        /// <param name="data"></param>List of Class Objects to be converted from Class objects to Datatable
        /// <returns></returns>Convrted Datatable 
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        #endregion
        #region Retentive Address
        private List<byte> getRetentiveAddressHex()
        {
            List<byte> RetentiveAddresssHex = new List<byte> { };
            List<XMIOConfig> data = _loadedProject.Tags;
            Int16 inputstr;
            DataTable dt = ToDataTable(data.Where(r => r.RetentiveAddress != null).ToList());
            RetentiveAddresssHex.Add(0x3A);
            int RowCount = dt.Rows.Count;
            inputstr = Int16.Parse(RowCount.ToString("X"), System.Globalization.NumberStyles.HexNumber);
            RetentiveAddresssHex.AddRange(BitConverter.GetBytes(inputstr).ToList());
            foreach (DataRow dr in dt.Rows)
            {
                string hexval;
                hexval = XMPS.Instance.GetHexAddress(dr["LogicalAddress"].ToString());
                RetentiveAddresssHex.AddRange(BitConverter.GetBytes(Int32.Parse(hexval, System.Globalization.NumberStyles.HexNumber)));
                hexval = XMPS.Instance.GetHexAddress(dr["RetentiveAddress"].ToString());
                RetentiveAddresssHex.AddRange(BitConverter.GetBytes(Int32.Parse(hexval, System.Globalization.NumberStyles.HexNumber)));
            }
            return RetentiveAddresssHex;
        }
        #endregion
        #region Modbus TCP client requests
        private List<byte> GetModBusTCPClientRequests()
        {
            var modBUSTCPClient = (MODBUSTCPClient)_loadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
            List<byte> ModBUSTCPClient = new List<byte> { };
            Int16 inputstr;
            ModBUSTCPClient.Add(0x39);
            int SlaveCount = modBUSTCPClient.Slaves.Count;
            inputstr = Int16.Parse(SlaveCount.ToString("X"), System.Globalization.NumberStyles.HexNumber);
            ModBUSTCPClient.Add(byte.Parse(inputstr.ToString()));
            foreach (MODBUSTCPClient_Slave ModBUSTCPClientEnt in modBUSTCPClient.Slaves)
            {
                var ipAddress = ModBUSTCPClientEnt.ServerIPAddress.ToString().Split('.');
                ModBUSTCPClient.Add(byte.Parse(ipAddress[0]));
                ModBUSTCPClient.Add(byte.Parse(ipAddress[1]));
                ModBUSTCPClient.Add(byte.Parse(ipAddress[2]));
                ModBUSTCPClient.Add(byte.Parse(ipAddress[3]));
                ModBUSTCPClient.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSTCPClientEnt.Port)));
                ModBUSTCPClient.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSTCPClientEnt.Polling)));
                ModBUSTCPClient.Add(byte.Parse(ModBUSTCPClientEnt.DeviceId.ToString()));
                string hexval = XMPS.Instance.GetHexAddress(ModBUSTCPClientEnt.Variable.ToString());
                ModBUSTCPClient.AddRange(BitConverter.GetBytes(Int32.Parse(hexval, System.Globalization.NumberStyles.HexNumber)));
                ModBUSTCPClient.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSTCPClientEnt.Address)));
                ModBUSTCPClient.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSTCPClientEnt.Length)));
                var modbusFunctionCode = ModbusFunctionCode.List.Where(x => x.Text == ModBUSTCPClientEnt.Functioncode).FirstOrDefault();
                ModBUSTCPClient.Add(byte.Parse(modbusFunctionCode.ID.ToString()));
            }
            return ModBUSTCPClient;
        }
        #endregion
        #region Modbus TCP server requests
        private List<byte> GetModBusTCPServerRequests()
        {
            var modBUSTCPServer = (MODBUSTCPServer)_loadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
            List<byte> ModBUSTCPServer = new List<byte> { };
            Int16 inputstr;
            ModBUSTCPServer.Add(0x38);
            int SlaveCount = modBUSTCPServer.Requests.Count;
            inputstr = Int16.Parse(SlaveCount.ToString("X"), System.Globalization.NumberStyles.HexNumber);
            ModBUSTCPServer.Add(byte.Parse(inputstr.ToString()));
            foreach (MODBUSTCPServer_Request ModBUSTCPServerReq in modBUSTCPServer.Requests)
            {
                ModBUSTCPServer.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSTCPServerReq.Port)));
                string hexval = XMPS.Instance.GetHexAddress(ModBUSTCPServerReq.Variable.ToString());
                ModBUSTCPServer.AddRange(BitConverter.GetBytes(Int32.Parse(hexval, System.Globalization.NumberStyles.HexNumber)));
                ModBUSTCPServer.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSTCPServerReq.Address)));
                ModBUSTCPServer.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSTCPServerReq.Length)));
                var modbusFunctionCode = ModbusFunctionCode.List.Where(x => x.Text == ModBUSTCPServerReq.FunctionCode).FirstOrDefault();
                if (modbusFunctionCode != null) ModBUSTCPServer.Add(byte.Parse(modbusFunctionCode.ID.ToString()));
            }
            return ModBUSTCPServer;
        }
        #endregion Modbus TCP server requests
        #region Modbus RTU requests
        private List<byte> GetModBusRTURequests()
        {
            var mODBUSRTUMaster = (MODBUSRTUMaster)_loadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
            List<byte> ModBUSRTUMaster = new List<byte> { };
            Int16 inputstr;
            ModBUSRTUMaster.Add(0x37);
            int SlaveCount = mODBUSRTUMaster.Slaves.Count;
            inputstr = Int16.Parse(SlaveCount.ToString("X"), System.Globalization.NumberStyles.HexNumber);
            ModBUSRTUMaster.Add(byte.Parse(inputstr.ToString()));
            foreach (MODBUSRTUMaster_Slave ModBUSRTUSlave in mODBUSRTUMaster.Slaves)
            {
                ModBUSRTUMaster.Add(byte.Parse(ModBUSRTUSlave.DeviceId.ToString()));
                ModBUSRTUMaster.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSRTUSlave.Polling)));
                string hexval = XMPS.Instance.GetHexAddress(ModBUSRTUSlave.Variable.ToString());
                ModBUSRTUMaster.AddRange(BitConverter.GetBytes(Int32.Parse(hexval, System.Globalization.NumberStyles.HexNumber)));
                ModBUSRTUMaster.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSRTUSlave.Address)));
                ModBUSRTUMaster.AddRange(BitConverter.GetBytes(Convert.ToInt16(ModBUSRTUSlave.Length)));
                var modbusFunctionCode = ModbusFunctionCode.List.Where(x => x.Text == ModBUSRTUSlave.FunctionCode).FirstOrDefault();
                ModBUSRTUMaster.Add(byte.Parse(modbusFunctionCode.ID.ToString()));
            }
            return ModBUSRTUMaster;
        }
        #endregion Modbus RTU requests
    }
}
