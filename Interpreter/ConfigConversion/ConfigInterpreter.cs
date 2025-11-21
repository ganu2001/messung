using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Types;

namespace Interpreter.ConfigConversion
{
    public class ConfigInterpreter
    {
        private XMProject _loadedProject;
        public ConfigInterpreter(XMPS xm)
        {
            _loadedProject = xm.LoadedProject;
        }
        public byte[] GetSettingsByteArray()
        {
            List<byte> configSettingsByteArray = new List<byte>();
            configSettingsByteArray.Add(Convert.ToByte('#'));
            List<byte> comSettingsByteArray = getCOMSettingsByteArray();
            List<byte> ethernetSettingsByteArray = getEthernetSettingsByteArray();
            List<byte> plcModelByteArray = getPLCModelBytes();
            configSettingsByteArray.AddRange(comSettingsByteArray);
            configSettingsByteArray.AddRange(ethernetSettingsByteArray);
            configSettingsByteArray.AddRange(plcModelByteArray);

            return configSettingsByteArray.ToArray();
        }
        private List<byte> getCOMSettingsByteArray()
        {
            COMDevice data = (COMDevice)_loadedProject.Devices.Where(d => d.GetType().Name == "COMDevice").FirstOrDefault();

            //List<string> comSettingsHex = new List<string>();
            List<byte> comSettingsBytes = new List<byte>();

            // enum to dictionary
            Dictionary<string, int> getEnumToDict<T>()
            {
                Dictionary<string, int> newDict = new Dictionary<string, int>();
                for (var i = 0; i < Enum.GetNames(typeof(T)).Length; i++)
                {
                    var a = Enum.ToObject(typeof(T), i + 1).ToString();
                    newDict.Add(a, i);
                }
                return newDict;
            }

            // 1 byte
            var baudRate = "_" + data.BaudRate.ToString();
            var baudRateDict = getEnumToDict<COMBaudRate>();
            var baudRateIndex = baudRateDict[baudRate];
            var baudRateByte = byte.Parse(baudRateIndex.ToString());

            // 1 byte
            var dataLength = "_" + data.DataLength.ToString();
            var dataLengthDict = getEnumToDict<COMDataLength>();
            var dataLengthIndex = dataLengthDict[dataLength];
            var dataLengthByte = byte.Parse(dataLengthIndex.ToString());

            // 1 byte
            var stopBit = "_" + data.StopBit.ToString();
            var stopBitDict = getEnumToDict<COMStopBit>();
            var stopBitIndex = stopBitDict[stopBit];
            var stopBitByte = byte.Parse(stopBitIndex.ToString());

            // 1 byte
            var parity = data.Parity.ToString();
            var parityDict = getEnumToDict<COMParity>();
            var parityIndex = parityDict[parity];
            var parityByte = byte.Parse(parityIndex.ToString());

            // 2 bytes
            var sendDelayBytes = BitConverter.GetBytes(Convert.ToInt16(data.SendDelay));

            // 2 bytes
            byte[] minInterfaceByte = new byte[] { };
            if (data.MinimumInterface.ToString().Contains('.'))
            {
                minInterfaceByte = BitConverter.GetBytes(Convert.ToInt16(data.MinimumInterface.ToString().Replace(".", string.Empty)));
            } else
            {
                minInterfaceByte = BitConverter.GetBytes(Convert.ToInt16(data.MinimumInterface.ToString()));
            }

            comSettingsBytes.Add(byte.Parse("49"));
            comSettingsBytes.Add(baudRateByte);
            comSettingsBytes.Add(dataLengthByte);
            comSettingsBytes.Add(stopBitByte);
            comSettingsBytes.Add(parityByte);
            comSettingsBytes.AddRange(sendDelayBytes);
            comSettingsBytes.AddRange(minInterfaceByte);

            // converting byte array to hex string array
            //comSettingsHex = BitConverter.ToString(comSettingsBytes.ToArray()).Split('-').ToList();

            return comSettingsBytes;
        }

        private List<byte> getEthernetSettingsByteArray()
        {
            Ethernet data = (Ethernet)_loadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();

            //List<string> ethernetSettingsHex = new List<string>();
            List<byte> ethernetSettingsBytes = new List<byte>();

            // 1 byte
            var useDHCPServerByte = byte.Parse(data.UseDHCPServer ? "1" : "0");

            // 1 byte each
            var ipAddress = data.EthernetIPAddress.ToString().Split('.');
            var ipAddressByte1 = byte.Parse(ipAddress[0]);
            var ipAddressByte2 = byte.Parse(ipAddress[1]);
            var ipAddressByte3 = byte.Parse(ipAddress[2]);
            var ipAddressByte4 = byte.Parse(ipAddress[3]);

            // 1 byte each
            var subnet = data.EthernetSubNet.ToString().Split('.');
            var subnetByte1 = byte.Parse(subnet[0]);
            var subnetByte2 = byte.Parse(subnet[1]);
            var subnetByte3 = byte.Parse(subnet[2]);
            var subnetByte4 = byte.Parse(subnet[3]);

            // 1 byte each
            var gateway = data.EthernetGetWay.ToString().Split('.');
            var gatewayByte1 = byte.Parse(gateway[0]);
            var gatewayByte2 = byte.Parse(gateway[1]);
            var gatewayByte3 = byte.Parse(gateway[2]);
            var gatewayByte4 = byte.Parse(gateway[3]);

            // 2 bytes
            var portBytes = BitConverter.GetBytes(Convert.ToInt16(data.Port));

            ethernetSettingsBytes.Add(byte.Parse("50"));
            ethernetSettingsBytes.Add(useDHCPServerByte);
            ethernetSettingsBytes.Add(ipAddressByte1);
            ethernetSettingsBytes.Add(ipAddressByte2);
            ethernetSettingsBytes.Add(ipAddressByte3);
            ethernetSettingsBytes.Add(ipAddressByte4);
            ethernetSettingsBytes.Add(subnetByte1);
            ethernetSettingsBytes.Add(subnetByte2);
            ethernetSettingsBytes.Add(subnetByte3);
            ethernetSettingsBytes.Add(subnetByte4);
            ethernetSettingsBytes.Add(gatewayByte1);
            ethernetSettingsBytes.Add(gatewayByte2);
            ethernetSettingsBytes.Add(gatewayByte3);
            ethernetSettingsBytes.Add(gatewayByte4);
            ethernetSettingsBytes.AddRange(portBytes);

            // converting byte array to hex string array
            //ethernetSettingsHex = BitConverter.ToString(ethernetSettingsBytes.ToArray()).Split('-').ToList();

            return ethernetSettingsBytes;
        }

        private List<byte> getPLCModelBytes()
        {
            string data = XMPS.Instance.PlcModel;

            List<byte> plcModelBytes = new List<byte>();
            byte model;
            switch (data)
            {
                case "XM100": model = 1;
                              break;
                case "XM-14DT": model = 1;
                                break;
                case "XM-17-ADT": model = 2;
                                  break;
                default: model = 0;
                         break;
            }

            plcModelBytes.Add(byte.Parse("51"));
            plcModelBytes.Add(model);

            return plcModelBytes;
        }
    }
}
