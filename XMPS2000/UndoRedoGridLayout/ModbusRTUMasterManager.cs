using System.Collections.Generic;
using System.Linq;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.UndoRedoGridLayout
{
    internal class ModbusRTUMasterManager
    {
        public static List<MODBUSRTUMaster_Slave> ModbusRTUMasterSlaves { get; private set; } = new List<MODBUSRTUMaster_Slave>();
        private static Stack<List<MODBUSRTUMaster_Slave>> undoStack = new Stack<List<MODBUSRTUMaster_Slave>>();
        private static Stack<List<MODBUSRTUMaster_Slave>> redoStack = new Stack<List<MODBUSRTUMaster_Slave>>();

        public ModbusRTUMasterManager()
        {

        }

        public ModbusRTUMasterManager(List<MODBUSRTUMaster_Slave> intialList)
        {
            undoStack.Clear();
            redoStack.Clear();
            ModbusRTUMasterSlaves = intialList.Select(p => new MODBUSRTUMaster_Slave
            {
                Polling = p.Polling,
                DeviceId = p.DeviceId,
                Address = p.Address,
                Length = p.Length,
                Variable = p.Variable,
                Tag = p.Tag,
                FunctionCode = p.FunctionCode,
                Name = p.Name,
                DisablingVariables = p.DisablingVariables,
                MultiplicationFactor = p.MultiplicationFactor
            }).ToList();
        }
        public void SaveState()
        {
            var deepCopy = ModbusRTUMasterSlaves.Select(p => new MODBUSRTUMaster_Slave
            {
                Polling = p.Polling,
                DeviceId = p.DeviceId,
                Address = p.Address,
                Length = p.Length,
                Variable = p.Variable,
                Tag = p.Tag,
                FunctionCode = p.FunctionCode,
                Name = p.Name,
                DisablingVariables = p.DisablingVariables,
                MultiplicationFactor = p.MultiplicationFactor
            }).ToList();
            undoStack.Push(deepCopy);
            redoStack.Clear();
        }
        public void AddMODBUSRTUSlave(MODBUSRTUMaster_Slave mODBUSRTUMasterSlaves_Slave)
        {
            SaveState();
            ModbusRTUMasterSlaves.Add(mODBUSRTUMasterSlaves_Slave);
        }

        public void DeleteMODBUSRTUSlave(MODBUSRTUMaster_Slave mODBUSRTUMasterSlaves_Slave)
        {
            SaveState();
            ModbusRTUMasterSlaves.RemoveAll(t => t.Name == mODBUSRTUMasterSlaves_Slave.Name);
        }
        public void UpdateMODBUSRTUSlave(MODBUSRTUMaster_Slave oldSlave, MODBUSRTUMaster_Slave newSlave)
        {
            SaveState();
            var existing = ModbusRTUMasterSlaves.FirstOrDefault(p => p.Name == oldSlave.Name);
            if (existing != null)
            {
                int index = ModbusRTUMasterSlaves.IndexOf(existing);
                if(index != -1)
                {
                    ModbusRTUMasterSlaves[index] = new MODBUSRTUMaster_Slave 
                    {
                        Polling = newSlave.Polling,
                        DeviceId = newSlave.DeviceId,
                        Address = newSlave.Address,
                        Length = newSlave.Length,
                        Variable = newSlave.Variable,
                        Tag = newSlave.Tag,
                        FunctionCode = newSlave.FunctionCode,
                        Name = newSlave.Name,
                        DisablingVariables = newSlave.DisablingVariables,
                        MultiplicationFactor = newSlave.MultiplicationFactor
                    };
                }
                // correct the sequnce of slaves
                for (int i = 0; i < ModbusRTUMasterSlaves.Count; i++)
                {
                    ModbusRTUMasterSlaves[i].Name = $"MODBUSRTUMasterSlave{(i + 1).ToString("00")}";
                }
            }
        }
        public void UpdateDisablingVariable(MODBUSRTUMaster_Slave slave, string disableVariableValue)
        {
            SaveState();
            var existing = ModbusRTUMasterSlaves.FirstOrDefault(p => p.Address == slave.Address);
            if (existing != null)
            {
                existing.DisablingVariables = disableVariableValue;
            }
        }
        public List<MODBUSRTUMaster_Slave> Undo()
        {
            if (undoStack.Count > 0)
            {
                var deepCopy = ModbusRTUMasterSlaves.Select(p => new MODBUSRTUMaster_Slave
                {
                    Polling = p.Polling,
                    DeviceId = p.DeviceId,
                    Address = p.Address,
                    Length = p.Length,
                    Variable = p.Variable,
                    Tag = p.Tag,
                    FunctionCode = p.FunctionCode,
                    Name = p.Name,
                    DisablingVariables = p.DisablingVariables,
                    MultiplicationFactor = p.MultiplicationFactor
                }).ToList();
                redoStack.Push(deepCopy);

                ModbusRTUMasterSlaves = undoStack.Pop()
                    .Select(p => new MODBUSRTUMaster_Slave
                    {
                        Polling = p.Polling,
                        DeviceId = p.DeviceId,
                        Address = p.Address,
                        Length = p.Length,
                        Variable = p.Variable,
                        Tag = p.Tag,
                        FunctionCode = p.FunctionCode,
                        Name = p.Name,
                        DisablingVariables = p.DisablingVariables,
                        MultiplicationFactor = p.MultiplicationFactor
                    }).ToList();
                // correct the sequnce of slaves
                for (int i = 0; i < ModbusRTUMasterSlaves.Count; i++)
                {
                    ModbusRTUMasterSlaves[i].Name = $"MODBUSRTUMasterSlave{(i + 1).ToString("00")}";
                }
                return ModbusRTUMasterSlaves;
            }
            return null;
        }
        public List<MODBUSRTUMaster_Slave> Redo()
        {
            if (redoStack.Count > 0)
            {
                var deepCopy = ModbusRTUMasterSlaves.Select(p => new MODBUSRTUMaster_Slave
                {
                    Polling = p.Polling,
                    DeviceId = p.DeviceId,
                    Address = p.Address,
                    Length = p.Length,
                    Variable = p.Variable,
                    Tag = p.Tag,
                    FunctionCode = p.FunctionCode,
                    Name = p.Name,
                    DisablingVariables = p.DisablingVariables,
                    MultiplicationFactor = p.MultiplicationFactor
                }).ToList();
                undoStack.Push(deepCopy);

                ModbusRTUMasterSlaves = redoStack.Pop()
                    .Select(p => new MODBUSRTUMaster_Slave
                    {
                        Polling = p.Polling,
                        DeviceId = p.DeviceId,
                        Address = p.Address,
                        Length = p.Length,
                        Variable = p.Variable,
                        Tag = p.Tag,
                        FunctionCode = p.FunctionCode,
                        Name = p.Name,
                        DisablingVariables = p.DisablingVariables,
                        MultiplicationFactor = p.MultiplicationFactor
                    }).ToList();
                // correct the sequnce of slaves
                for (int i = 0; i < ModbusRTUMasterSlaves.Count; i++)
                {
                    ModbusRTUMasterSlaves[i].Name = $"MODBUSRTUMasterSlave{(i + 1).ToString("00")}";
                }
                return ModbusRTUMasterSlaves;
            }
            return null;
        }
    }
}
