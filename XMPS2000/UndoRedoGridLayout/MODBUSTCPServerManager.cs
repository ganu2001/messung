using System.Collections.Generic;
using System.Linq;
using XMPS2000.Core.Devices;

namespace XMPS2000.UndoRedoGridLayout
{
    internal class MODBUSTCPServerManager
    {
        public static List<MODBUSTCPServer_Request> ModbusTCPServerMasterSlaves { get; private set; } = new List<MODBUSTCPServer_Request>();
        private static Stack<List<MODBUSTCPServer_Request>> undoStack = new Stack<List<MODBUSTCPServer_Request>>();
        private static Stack<List<MODBUSTCPServer_Request>> redoStack = new Stack<List<MODBUSTCPServer_Request>>();

        public MODBUSTCPServerManager()
        {

        }

        public MODBUSTCPServerManager(List<MODBUSTCPServer_Request> intialList)
        {
            undoStack.Clear();
            redoStack.Clear();
            ModbusTCPServerMasterSlaves = intialList.Select(p => new MODBUSTCPServer_Request
            {
                Name = p.Name,
                Port = p.Port,
                Address = p.Address,
                Length = p.Length,
                Tag = p.Tag,
                Variable = p.Variable,
                FunctionCode = p.FunctionCode
            }).ToList();
        }
        public void SaveState()
        {
            var deepCopy = ModbusTCPServerMasterSlaves.Select(p => new MODBUSTCPServer_Request
            {
                Name = p.Name,
                Port = p.Port,
                Address = p.Address,
                Length = p.Length,
                Tag = p.Tag,
                Variable = p.Variable,
                FunctionCode = p.FunctionCode
            }).ToList();

            undoStack.Push(deepCopy);
            redoStack.Clear();
        }
        public void AddMODBUSTCPServerSlave(MODBUSTCPServer_Request mODBUSTCPServer_Request)
        {
            SaveState();
            ModbusTCPServerMasterSlaves.Add(mODBUSTCPServer_Request);
        }

        public void DeleteMODBUSTCPServerSlave(MODBUSTCPServer_Request mODBUSTCPServer_Request)
        {
            SaveState();
            ModbusTCPServerMasterSlaves.RemoveAll(t => t.Name == mODBUSTCPServer_Request.Name);
        }
        public void UpdateMODBUSTCPServerSlave(MODBUSTCPServer_Request oldSlave, MODBUSTCPServer_Request newSlave)
        {
            SaveState();
            var existing = ModbusTCPServerMasterSlaves.FirstOrDefault(p => p.Name == oldSlave.Name);
            if (existing != null)
            {
                int index = ModbusTCPServerMasterSlaves.IndexOf(existing);
                if (index != -1)
                {
                    // Replace the object at the same index
                    ModbusTCPServerMasterSlaves[index] = new MODBUSTCPServer_Request
                    {
                        Name = newSlave.Name,
                        Port = newSlave.Port,
                        Address = newSlave.Address,
                        Length = newSlave.Length,
                        Tag = newSlave.Tag,
                        Variable = newSlave.Variable,
                        FunctionCode = newSlave.FunctionCode
                    };
                }
                // Maintain the correct sequence of requests
                for (int i = 0; i < ModbusTCPServerMasterSlaves.Count; i++)
                {
                    ModbusTCPServerMasterSlaves[i].Name = $"MODBUSTCPServerRequest{(i + 1).ToString("00")}";
                }
            }
        }
        public List<MODBUSTCPServer_Request> Undo()
        {
            if (undoStack.Count > 0)
            {
                var deepCopy = ModbusTCPServerMasterSlaves.Select(p => new MODBUSTCPServer_Request
                {
                    Name = p.Name,
                    Port = p.Port,
                    Address = p.Address,
                    Length = p.Length,
                    Tag = p.Tag,
                    Variable = p.Variable,
                    FunctionCode = p.FunctionCode
                }).ToList();

                redoStack.Push(deepCopy);
                ModbusTCPServerMasterSlaves = undoStack.Pop()
                    .Select(p => new MODBUSTCPServer_Request
                    {
                        Name = p.Name,
                        Port = p.Port,
                        Address = p.Address,
                        Length = p.Length,
                        Tag = p.Tag,
                        Variable = p.Variable,
                        FunctionCode = p.FunctionCode
                    }).ToList();
                // correct the sequnce of requests
                for (int i = 0; i < ModbusTCPServerMasterSlaves.Count; i++)
                {
                    ModbusTCPServerMasterSlaves[i].Name = $"MODBUSTCPServerRequest{(i + 1).ToString("00")}";
                }
                return ModbusTCPServerMasterSlaves;
            }
            return null;
        }
        public List<MODBUSTCPServer_Request> Redo()
        {
            if (redoStack.Count > 0)
            {
                var deepCopy = ModbusTCPServerMasterSlaves.Select(p => new MODBUSTCPServer_Request
                {
                    Name = p.Name,
                    Port = p.Port,
                    Address = p.Address,
                    Length = p.Length,
                    Tag = p.Tag,
                    Variable = p.Variable,
                    FunctionCode = p.FunctionCode
                }).ToList();

                undoStack.Push(deepCopy);

                ModbusTCPServerMasterSlaves = redoStack.Pop()
                    .Select(p => new MODBUSTCPServer_Request
                    {
                        Name = p.Name,
                        Port = p.Port,
                        Address = p.Address,
                        Length = p.Length,
                        Tag = p.Tag,
                        Variable = p.Variable,
                        FunctionCode = p.FunctionCode
                    }).ToList();
                // correct the sequnce of requests
                for (int i = 0; i < ModbusTCPServerMasterSlaves.Count; i++)
                {
                    ModbusTCPServerMasterSlaves[i].Name = $"MODBUSTCPServerRequest{(i + 1).ToString("00")}";
                }
                return ModbusTCPServerMasterSlaves;
            }
            return null;
        }
    }
}
