using System.Collections.Generic;
using System.Linq;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.Types;

namespace XMPS2000.UndoRedoGridLayout
{
    internal class ModbusTCPClientManager
    {

        public static List<MODBUSTCPClient_Slave> ModbusTCPClientMasterSlaves { get; private set; } = new List<MODBUSTCPClient_Slave>();
        private static Stack<List<MODBUSTCPClient_Slave>> undoStack = new Stack<List<MODBUSTCPClient_Slave>>();
        private static Stack<List<MODBUSTCPClient_Slave>> redoStack = new Stack<List<MODBUSTCPClient_Slave>>();

        public ModbusTCPClientManager()
        {

        }

        public ModbusTCPClientManager(List<MODBUSTCPClient_Slave> intialList)
        {
            undoStack.Clear();
            redoStack.Clear();
            ModbusTCPClientMasterSlaves = intialList.Select(p => new MODBUSTCPClient_Slave
            {
                Name = p.Name,
                ServerIPAddress = p.ServerIPAddress,
                Port = p.Port,
                Polling = p.Polling,
                DeviceId = p.DeviceId,
                Address = p.Address,
                Length = p.Length,
                Tag = p.Tag,
                Variable = p.Variable,
                Functioncode = p.Functioncode,
                MultiplicationFactor = p.MultiplicationFactor
            }).ToList();
        }
        public void SaveState()
        {
            var deepCopy = ModbusTCPClientMasterSlaves.Select(p => new MODBUSTCPClient_Slave
            {
                Name = p.Name,
                ServerIPAddress = p.ServerIPAddress,
                Port = p.Port,
                Polling = p.Polling,
                DeviceId = p.DeviceId,
                Address = p.Address,
                Length = p.Length,
                Tag = p.Tag,
                Variable = p.Variable,
                Functioncode = p.Functioncode,
                MultiplicationFactor= p.MultiplicationFactor
            }).ToList();

            undoStack.Push(deepCopy);
            redoStack.Clear();
        }
        public void AddMODBUSTCPClientSlave(MODBUSTCPClient_Slave mODBUSTCPClient_Slave)
        {
            SaveState();
            ModbusTCPClientMasterSlaves.Add(mODBUSTCPClient_Slave);
        }

        public void DeleteMODBUSTCPClientSlave(MODBUSTCPClient_Slave mODBUSTCPClient_Slave)
        {
            SaveState();
            ModbusTCPClientMasterSlaves.RemoveAll(t => t.Name == mODBUSTCPClient_Slave.Name);
        }
        public void UpdateMODBUSTCPClientSlave(MODBUSTCPClient_Slave oldSlave, MODBUSTCPClient_Slave newSlave)
        {
            SaveState();
            var existing = ModbusTCPClientMasterSlaves.FirstOrDefault(p => p.Name == oldSlave.Name);
            if (existing != null)
            {
                int index = ModbusTCPClientMasterSlaves.IndexOf(existing);
                if(index != -1)
                {
                    ModbusTCPClientMasterSlaves[index] = new MODBUSTCPClient_Slave 
                    {
                        Name = newSlave.Name,
                        ServerIPAddress = newSlave.ServerIPAddress,
                        Port = newSlave.Port,
                        Polling = newSlave.Polling,
                        DeviceId = newSlave.DeviceId,
                        Address = newSlave.Address,
                        Length = newSlave.Length,
                        Tag = newSlave.Tag,
                        Variable = newSlave.Variable,
                        Functioncode = newSlave.Functioncode,
                        MultiplicationFactor = newSlave.MultiplicationFactor
                    };
                }
                // correct the sequnce of slaves
                for (int i = 0; i < ModbusTCPClientMasterSlaves.Count; i++)
                {
                    ModbusTCPClientMasterSlaves[i].Name = $"MODBUSTCPClientSlave{(i + 1).ToString("00")}";
                }
            }
        }
        public List<MODBUSTCPClient_Slave> Undo()
        {
            if (undoStack.Count > 0)
            {
                var deepCopy = ModbusTCPClientMasterSlaves.Select(p => new MODBUSTCPClient_Slave
                {
                    Name = p.Name,
                    ServerIPAddress = p.ServerIPAddress,
                    Port = p.Port,
                    Polling = p.Polling,
                    DeviceId = p.DeviceId,
                    Address = p.Address,
                    Length = p.Length,
                    Tag = p.Tag,
                    Variable = p.Variable,
                    Functioncode = p.Functioncode,
                    MultiplicationFactor = p.MultiplicationFactor
                }).ToList();
                redoStack.Push(deepCopy);

                ModbusTCPClientMasterSlaves = undoStack.Pop()
                    .Select(p => new MODBUSTCPClient_Slave
                    {
                        Name = p.Name,
                        ServerIPAddress = p.ServerIPAddress,
                        Port = p.Port,
                        Polling = p.Polling,
                        DeviceId = p.DeviceId,
                        Address = p.Address,
                        Length = p.Length,
                        Variable = p.Variable,
                        Tag = p.Tag,
                        Functioncode = p.Functioncode,
                        MultiplicationFactor = p.MultiplicationFactor
                    }).ToList();
                // correct the sequnce of slaves
                for (int i = 0; i < ModbusTCPClientMasterSlaves.Count; i++)
                {
                    ModbusTCPClientMasterSlaves[i].Name = $"MODBUSTCPClientSlave{(i + 1).ToString("00")}";
                }
                return ModbusTCPClientMasterSlaves;
            }
            return null;
        }
        public List<MODBUSTCPClient_Slave> Redo()
        {
            if (redoStack.Count > 0)
            {
                var deepCopy = ModbusTCPClientMasterSlaves.Select(p => new MODBUSTCPClient_Slave
                {
                    Name = p.Name,
                    ServerIPAddress = p.ServerIPAddress,
                    Port = p.Port,
                    Polling = p.Polling,
                    DeviceId = p.DeviceId,
                    Address = p.Address,
                    Length = p.Length,
                    Tag = p.Tag,
                    Variable = p.Variable,
                    Functioncode = p.Functioncode,
                    MultiplicationFactor = p.MultiplicationFactor
                }).ToList();
                undoStack.Push(deepCopy);

                ModbusTCPClientMasterSlaves = redoStack.Pop()
                    .Select(p => new MODBUSTCPClient_Slave
                    {
                        Name = p.Name,
                        ServerIPAddress = p.ServerIPAddress,
                        Port = p.Port,
                        Polling = p.Polling,
                        DeviceId = p.DeviceId,
                        Address = p.Address,
                        Length = p.Length,
                        Tag = p.Tag,
                        Variable = p.Variable,
                        Functioncode = p.Functioncode,
                        MultiplicationFactor = p.MultiplicationFactor
                    }).ToList();
                // correct the sequnce of slaves
                for (int i = 0; i < ModbusTCPClientMasterSlaves.Count; i++)
                {
                    ModbusTCPClientMasterSlaves[i].Name = $"MODBUSTCPClientSlave{(i + 1).ToString("00")}";
                }
                return ModbusTCPClientMasterSlaves;
            }
            return null;
        }
    }
}
