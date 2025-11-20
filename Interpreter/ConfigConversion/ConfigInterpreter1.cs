using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS2000;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;

namespace Interpreter.ConfigConversion
{
    public class ConfigInterpreter1
    {
        private XMProject _loadedProject;
        public ConfigInterpreter1(XMPS xm)
        {
            _loadedProject = xm.LoadedProject;
        }

        #region PLC On Board
        private List<byte> GetPLCOnBoard()
        {
            List<byte> OnBoard = new List<byte> { };

            List<Mode> AnalogModes = Mode.List;
            var OnBoardIOForMode = _loadedProject.Tags.Where(s => s.IoList == XMPS2000.Core.Types.IOListType.OnBoardIO && s.Type.ToString().StartsWith("Analog")).ToList();
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
            OnBoard.Add(byte.Parse(_loadedProject.Tags.Where(s => s.IoList == XMPS2000.Core.Types.IOListType.OnBoardIO && s.Type == XMPS2000.Core.Types.IOType.DigitalInput).Count().ToString()));
            OnBoard.Add(byte.Parse(_loadedProject.Tags.Where(s => s.IoList == XMPS2000.Core.Types.IOListType.OnBoardIO && s.Type == XMPS2000.Core.Types.IOType.DigitalOutput).Count().ToString()));
            OnBoard.Add(byte.Parse(_loadedProject.Tags.Where(s => s.IoList == XMPS2000.Core.Types.IOListType.OnBoardIO && s.Type == XMPS2000.Core.Types.IOType.AnalogInput).Count().ToString()));
            OnBoard.Add(byte.Parse(_loadedProject.Tags.Where(s => s.IoList == XMPS2000.Core.Types.IOListType.OnBoardIO && s.Type == XMPS2000.Core.Types.IOType.AnalogOutput).Count().ToString()));
            return OnBoard;
        }
        #endregion 
        #region Expansion IO
        private List<byte> GetExpansionIO()
        {
            List<byte> ExpansionIO = new List<byte> { };
            var AllModels = _loadedProject.Tags.GroupBy(e => e.Model).Select(grp => grp.First());
            var Expansion = AllModels.Where(s => s.IoList == XMPS2000.Core.Types.IOListType.ExpansionIO).ToList();
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
            var remote = AllModels.Where(s => s.IoList == XMPS2000.Core.Types.IOListType.RemoteIO).ToList();
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
    }

}
