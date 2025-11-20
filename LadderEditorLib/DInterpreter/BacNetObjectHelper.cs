using System.Collections.Generic;
using XMPS2000.Core.BacNet;
using XMPS2000.Core;
using System.Linq;
using XMPS2000.Core.Base;
using XMPS2000.Core.Types;
namespace LadderEditorLib.DInterpreter
{
    public class BacNetObjectHelper
    {
        public static void AddBinaryIOV(ref BacNetIP bacNetIP, string logicalAddress, string tag, HashSet<string> existingAddresses, ref int inputCounter, ref int outputCounter, int? exreNodeCount = null)
        {
            if (existingAddresses.Contains(logicalAddress)) return;

            string inputPrefix = exreNodeCount.HasValue ? $"{exreNodeCount}" : string.Empty;
            string outputPrefix = exreNodeCount.HasValue ? $"{exreNodeCount}" : string.Empty;

            string inputCounterFormatted = exreNodeCount.HasValue ? inputCounter.ToString("D2") : inputCounter.ToString();
            string outputCounterFormatted = exreNodeCount.HasValue ? outputCounter.ToString("D2") : outputCounter.ToString();

            string objectIdentifier = logicalAddress.StartsWith("I") ? $"Binary Input:{inputPrefix}{inputCounterFormatted}" : $"Binary Output:{outputPrefix}{outputCounterFormatted}";
            string instanceNumber = logicalAddress.StartsWith("I") ? $"{inputPrefix}{inputCounterFormatted}" : $"{outputPrefix}{outputCounterFormatted}";
            string objectType = logicalAddress.StartsWith("Q") ? "4:Binary Output" : logicalAddress.StartsWith("I") ? "3:Binary Input" : "5:Binary Value";

            bacNetIP.BinaryIOValues.Add(new BinaryIOV(objectIdentifier, instanceNumber, objectType, objectIdentifier, tag, logicalAddress, false));
        }

        public static void AddAnalogIOV(ref BacNetIP bacNetIP, string logicalAddress, string tag, HashSet<string> existingAddresses, ref int inputCounter, ref int outputCounter, string tagType, int? exreNodeCount = null)
        {
            if (existingAddresses.Contains(logicalAddress)) return;

            string inputPrefix = exreNodeCount.HasValue ? $"{exreNodeCount}" : string.Empty;
            string outputPrefix = exreNodeCount.HasValue ? $"{exreNodeCount}" : string.Empty;

            string inputCounterFormatted = exreNodeCount.HasValue ? inputCounter.ToString("D2") : inputCounter.ToString();
            string outputCounterFormatted = exreNodeCount.HasValue ? outputCounter.ToString("D2") : outputCounter.ToString();

            string typePrefix = "Analog";
            string ioType = logicalAddress.StartsWith("I") ? "Input" : "Output";
            string prefix = logicalAddress.StartsWith("I") ? inputPrefix : outputPrefix;
            string counterFormatted = logicalAddress.StartsWith("I") ? inputCounterFormatted : outputCounterFormatted;
            string typeSuffix = logicalAddress.StartsWith("Q") ? "Output" : logicalAddress.StartsWith("I") ? "Input" : "Value";
            int typeCode = logicalAddress.StartsWith("Q") ? 1 : logicalAddress.StartsWith("I") ? 0 : 2;

            string objectIdentifier = $"{typePrefix} {ioType}:{prefix}{counterFormatted}";
            string instanceNumber = $"{prefix}{counterFormatted}";
            string objectType = $"{typeCode}:{typePrefix} {typeSuffix}";
            string currentTagType = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(tag)).Type.ToString();
            bacNetIP.AnalogIOValues.Add(new AnalogIOV(objectIdentifier, instanceNumber, objectType, objectIdentifier, tag, logicalAddress, false, currentTagType));
        }

        public static void IncrementCounters(XMPS2000.Core.Types.IOType type, ref int binaryInputCounter, ref int binaryOutputCounter, ref int analogInputCounter, ref int analogOutputCounter)
        {
            switch (type)
            {
                case XMPS2000.Core.Types.IOType.DigitalInput:
                    binaryInputCounter++;
                    break;
                case XMPS2000.Core.Types.IOType.AnalogInput:
                case XMPS2000.Core.Types.IOType.UniversalInput:
                    analogInputCounter++;
                    break;
                case XMPS2000.Core.Types.IOType.DigitalOutput:
                    binaryOutputCounter++;
                    break;
                case XMPS2000.Core.Types.IOType.AnalogOutput:
                case XMPS2000.Core.Types.IOType.UniversalOutput:
                    analogOutputCounter++;
                    break;
            }
        }

        public static void ClearBacNetObject(List<string> data)
        {
            BacNetIP bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null) return;

            DeviceModel systemConfiguration = XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault();
            var ethernetDevices = systemConfiguration.Templates
                ?.Where(template => template.Ethernet != null)
                .ToList();
            var bacNetDevice = ethernetDevices?.SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices)
                               .FirstOrDefault(device => device.Name == "BacNet");

            if (bacNetIP.AnalogIOValues.Count > 0)
            {
                foreach (var d in data)
                {
                    var bacnetObject = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(d));
                    if (bacnetObject != null && bacnetObject.IsEnable)
                    {
                        string objectType = bacnetObject.ObjectType.Split(':')[1].Trim();
                        objectType = objectType.Replace(" ", "");

                        var propertyToUpdate = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == objectType);
                        if (propertyToUpdate != null)
                        {
                            propertyToUpdate.CurrentCount -= 1;
                        }
                    }
                }
                bacNetIP.AnalogIOValues.RemoveAll(t => data.Contains(t.LogicalAddress));
            }
            if (bacNetIP.BinaryIOValues.Count > 0)
            {
                foreach (var d in data)
                {
                    var bacnetObject = XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(d));
                    if (bacnetObject != null && bacnetObject.IsEnable)
                    {
                        string objectType = bacnetObject.ObjectType.Split(':')[1].Trim();
                        objectType = objectType.Replace(" ", "");

                        var propertyToUpdate = bacNetDevice.Properties?.FirstOrDefault(p => p.Name == objectType);
                        if (propertyToUpdate != null)
                        {
                            propertyToUpdate.CurrentCount -= 1;
                        }
                    }
                }
                bacNetIP.BinaryIOValues.RemoveAll(t => data.Contains(t.LogicalAddress));
            }
        }

        public static string GetMaxAnalogInstanceNumber(ref BacNetIP bacNetIP)
        {
            if (bacNetIP.AnalogIOValues.Count() > 0 && XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD"))
            {
                return bacNetIP.AnalogIOValues.Select(ai => new { ai, tag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(ai.LogicalAddress)) }).Where(x => x.tag != null && 
                (x.tag.IoList.ToString() == "ExpansionIO" || x.tag.IoList.ToString() == "RemoteIO")).Select(x => int.Parse(x.ai.InstanceNumber)).DefaultIfEmpty(0).Max().ToString();
            }
            return "0";
        }

        internal static string GetMaxBinaryInstaceNumber(ref BacNetIP bacNetIP)
        {
            if (bacNetIP.BinaryIOValues.Count() > 0 && XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD"))
            {
                return bacNetIP.BinaryIOValues.Select(ai => new {ai,tag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(ai.LogicalAddress))}).Where(x => x.tag != null &&
                (x.tag.IoList.ToString() == "ExpansionIO" || x.tag.IoList.ToString() == "RemoteIO")).Select(x => int.Parse(x.ai.InstanceNumber)).DefaultIfEmpty(0).Max().ToString();
            }
            return "0";
        }

        public static void AddDignosticTags(bool fromExpansion, string modelName, string tagType)
        {
            if (DiagnosticTagsAlreadyExist(modelName, fromExpansion))
            {
                return;
            }
            List<XMIOConfig> childTags = new List<XMIOConfig>();
            var inputCount = XMPS.Instance.LoadedProject.Tags
                .Where(t => (tagType.Equals("Analog Input") ? t.Type.ToString() == "AnalogInput" : t.Type.ToString() == "UniversalInput")
                && (fromExpansion ? t.IoList == XMPS2000.Core.Types.IOListType.ExpansionIO
                : t.IoList == XMPS2000.Core.Types.IOListType.OnBoardIO)
                && (tagType.Equals("Analog Input") ? t.Type == XMPS2000.Core.Types.IOType.AnalogInput : t.Type == IOType.UniversalInput)
                && t.Model.Equals(modelName)).ToList();

            var outputCount = XMPS.Instance.LoadedProject.Tags
                .Where(t => (tagType.Equals("Analog Input") ? t.Type.ToString() == "AnalogOutput" : t.Type.ToString() == "UniversalOutput")
                 && (fromExpansion ? t.IoList == XMPS2000.Core.Types.IOListType.ExpansionIO
                : t.IoList == XMPS2000.Core.Types.IOListType.OnBoardIO)
                && (tagType.Equals("Analog Input") ? t.Type == XMPS2000.Core.Types.IOType.AnalogOutput : t.Type == IOType.UniversalOutput) && t.Model.Equals(modelName)).ToList();

            int diagWordAddress = inputCount
                    .Where(t => (tagType.Equals("Analog Input") ? t.Type.ToString() == "AnalogInput" : t.Type.ToString() == "UniversalInput")).ToList()
                    .Select(t => int.Parse(t.LogicalAddress.Split(':')[1].Split('.')[0]))
                    .DefaultIfEmpty(0)
                    .Max() + 1;
            int nextBit = 0;
            for (int i = 0; i < inputCount.Count; i++)
            {
                string orTag = inputCount[i].Tag + "_OR";
                childTags.Add(new XMIOConfig
                {
                    Model = inputCount[i].Model,
                    Label = inputCount[i].Label + "_OR",
                    LogicalAddress = $"{"I1"}:{diagWordAddress.ToString("D3")}.{nextBit.ToString("D2")}",
                    Tag = orTag,
                    IoList = inputCount[i].IoList,
                    Type = inputCount[i].Type,
                    Mode = inputCount[i].Mode,
                    Key = XMPS.Instance.LoadedProject.Tags.Max(k => k.Key) + 1,
                    IsEnableInputFilter = false,
                    InpuFilterValue = string.Empty
                });

                nextBit++;
                string olTag = inputCount[i].Tag + "_OL";
                childTags.Add(new XMIOConfig
                {
                    Model = inputCount[i].Model,
                    Label = inputCount[i].Label + "_OL",
                    LogicalAddress = $"{"I1"}:{diagWordAddress.ToString("D3")}.{(nextBit).ToString("D2")}",
                    Tag = olTag,
                    IoList = inputCount[i].IoList,
                    Type = inputCount[i].Type,
                    Mode = inputCount[i].Mode,
                    Key = XMPS.Instance.LoadedProject.Tags.Max(k => k.Key) + 1,
                    IsEnableInputFilter = false,
                    InpuFilterValue = string.Empty
                });
                nextBit++;
            }
            for (int i = 0; i < outputCount.Count; i++)
            {
                string orTag = outputCount[i].Tag + "_OR";
                childTags.Add(new XMIOConfig
                {
                    Model = outputCount[i].Model,
                    Label = outputCount[i].Label + "_OR",
                    LogicalAddress = $"{"I1"}:{diagWordAddress.ToString("D3")}.{nextBit.ToString("D2")}",
                    Tag = orTag,
                    IoList = outputCount[i].IoList,
                    Type = outputCount[i].Type,
                    Mode = outputCount[i].Mode,
                    Key = XMPS.Instance.LoadedProject.Tags.Max(k => k.Key) + 1,
                    IsEnableInputFilter = false,
                    InpuFilterValue = string.Empty
                });
                nextBit++;
            }
            XMPS.Instance.LoadedProject.Tags.AddRange(childTags);
            XMPS.Instance.LoadedProject.HasDiagnosticTags = true;
        }
        public static bool DiagnosticTagsAlreadyExist(string modelName, bool fromExpansion)
        {
            var modelTags = XMPS.Instance.LoadedProject.Tags
                .Where(t => t.Model == modelName &&
                           (fromExpansion ? t.IoList == IOListType.ExpansionIO : t.IoList == IOListType.OnBoardIO))
                .ToList();

            bool hasDiagnosticTags = modelTags.Any(t =>
                t.Tag.EndsWith("_OR") || t.Tag.EndsWith("_OL"));

            return hasDiagnosticTags;
        }
    }
}
