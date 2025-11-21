using System.Collections.Generic;
using System.Linq;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.UndoRedoGridLayout
{
    internal class ModbusRTUSlaveManager
    {
        public static List<MODBUSRTUSlaves_Slave> ModbusRTUSlaves { get; private set; } = new List<MODBUSRTUSlaves_Slave>();
        private static Stack<List<MODBUSRTUSlaves_Slave>> undoStack = new Stack<List<MODBUSRTUSlaves_Slave>>();
        private static Stack<List<MODBUSRTUSlaves_Slave>> redoStack = new Stack<List<MODBUSRTUSlaves_Slave>>();

        public ModbusRTUSlaveManager()
        {

        }

        public ModbusRTUSlaveManager(List<MODBUSRTUSlaves_Slave> intialList)
        {
            undoStack.Clear();
            redoStack.Clear();
            ModbusRTUSlaves = intialList.Select(p => new MODBUSRTUSlaves_Slave
            {
                Address = p.Address,
                Length = p.Length,
                Variable = p.Variable,
                Tag = p.Tag,
                FunctionCode = p.FunctionCode,
                Name = p.Name
            }).ToList();
        }
        public void SaveState()
        {
            var deepCopy = ModbusRTUSlaves.Select(p => new MODBUSRTUSlaves_Slave
            {
                Address = p.Address,
                Length = p.Length,
                Variable = p.Variable,
                Tag = p.Tag,
                FunctionCode = p.FunctionCode,
                Name = p.Name
            }).ToList();

            undoStack.Push(deepCopy);
            redoStack.Clear();
        }
        public void AddMODBUSRTUSlave(MODBUSRTUSlaves_Slave mODBUSRTUSlaves_Slave)
        {
            SaveState();
            ModbusRTUSlaves.Add(mODBUSRTUSlaves_Slave);
        }

        public void DeleteMODBUSRTUSlave(MODBUSRTUSlaves_Slave mODBUSRTUSlaves_Slave)
        {
            SaveState();
            ModbusRTUSlaves.RemoveAll(t => t.Name == mODBUSRTUSlaves_Slave.Name);
        }
        public void UpdateMODBUSRTUSlave(MODBUSRTUSlaves_Slave oldSlave, MODBUSRTUSlaves_Slave newSlave)
        {
            SaveState();
            var existing = ModbusRTUSlaves.FirstOrDefault(p => p.Name == oldSlave.Name);
            if (existing != null)
            {
                int index = ModbusRTUSlaves.IndexOf(existing);
                if(index != -1)
                {
                    ModbusRTUSlaves[index] = new MODBUSRTUSlaves_Slave 
                    {
                        Address = newSlave.Address,
                        Length = newSlave.Length,
                        Variable = newSlave.Variable,
                        Tag = newSlave.Tag,
                        FunctionCode = newSlave.FunctionCode,
                        Name = newSlave.Name
                    };
                }
                // correct the sequnce of slaves
                for (int i = 0; i < ModbusRTUSlaves.Count; i++)
                {
                    ModbusRTUSlaves[i].Name = $"MODBUSRTUSlavesSlave{(i + 1).ToString("00")}";
                }
            }
        }
        public List<MODBUSRTUSlaves_Slave> Undo()
        {
            if (undoStack.Count > 0)
            {
                var deepCopy = ModbusRTUSlaves.Select(p => new MODBUSRTUSlaves_Slave
                {
                    Address = p.Address,
                    Length = p.Length,
                    Variable = p.Variable,
                    Tag = p.Tag,
                    FunctionCode = p.FunctionCode,
                    Name = p.Name
                }).ToList();
                redoStack.Push(deepCopy);

                ModbusRTUSlaves = undoStack.Pop()
                    .Select(p => new MODBUSRTUSlaves_Slave
                    {
                        Address = p.Address,
                        Length = p.Length,
                        Variable = p.Variable,
                        Tag = p.Tag,
                        FunctionCode = p.FunctionCode,
                        Name = p.Name
                    }).ToList();
                // correct the sequnce of slaves
                for (int i = 0; i < ModbusRTUSlaves.Count; i++)
                {
                    ModbusRTUSlaves[i].Name = $"MODBUSRTUSlavesSlave{(i + 1).ToString("00")}";
                }
                return ModbusRTUSlaves;
            }
            return null;
        }
        public List<MODBUSRTUSlaves_Slave> Redo()
        {
            if (redoStack.Count > 0)
            {
                var deepCopy = ModbusRTUSlaves.Select(p => new MODBUSRTUSlaves_Slave
                {
                    Address = p.Address,
                    Length = p.Length,
                    Variable = p.Variable,
                    Tag = p.Tag,
                    FunctionCode = p.FunctionCode,
                    Name = p.Name
                }).ToList();
                undoStack.Push(deepCopy);

                ModbusRTUSlaves = redoStack.Pop()
                    .Select(p => new MODBUSRTUSlaves_Slave
                    {
                        Address = p.Address,
                        Length = p.Length,
                        Variable = p.Variable,
                        Tag = p.Tag,
                        FunctionCode = p.FunctionCode,
                        Name = p.Name
                    }).ToList();
                // correct the sequnce of slaves
                for (int i = 0; i < ModbusRTUSlaves.Count; i++)
                {
                    ModbusRTUSlaves[i].Name = $"MODBUSRTUSlavesSlave{(i + 1).ToString("00")}";
                }
                return ModbusRTUSlaves;
            }
            return null;
        }
    }
}
