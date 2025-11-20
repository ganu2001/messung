using System;
using System.Collections.Generic;
using System.Linq;
using XMPS2000.Core;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMPS2000.Bacnet
{
    internal class BacNetTagFactory
    {
        public object GetBacNetTags(string formName)
        {
            switch (formName)
            {
                case "BACNET IP":
                    return GetBACNETIPTags();
                case "Hardware IO's":
                    return GetOnBoardExAddObjects();
                case "Binary Value":
                    return GetBinaryValueTags();
                case "Analog Value":
                    return GetAnalogValueTags();
                case "Multistate Value":
                    return GetMultiStateValueTags();
                case "Schedule":
                    return GetScheduleObjects();
                case "Calendar":
                    return GetCalendarObjects();
                case "Notification Class":
                    return GetNotificationObjects();
                default:
                    return GetDefaultTags();
            }
        }

        private HashSet<string> GetNotificationObjects()
        {
            var bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null)
            {
                return new HashSet<string>();
            }

            return new HashSet<string>(
                bacNetIP.Notifications.Where(t => Convert.ToInt32(t.InstanceNumber) > 0).OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Select(t => t.ObjectName.ToString()));
        }

        private HashSet<string> GetCalendarObjects()
        {
            var bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null)
            {
                return new HashSet<string>();
            }

            return new HashSet<string>(

        bacNetIP.Calendars.OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Select(t => t.ObjectName.ToString()));

        }
        private List<XMIOConfig> GetMultiStateValueTags()
        {
            var multiStateTag = GetUsedMultiStateTags();

            return XMPS.Instance.LoadedProject.Tags
                .Where(t => multiStateTag.Contains(t.LogicalAddress))
                .ToList();
        }

        private HashSet<string> GetUsedMultiStateTags()
        {
            var bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null)
            {
                return new HashSet<string>();
            }

            return new HashSet<string>(
                bacNetIP.MultistateValues
                    .Where(t => t.LogicalAddress.StartsWith("W4")).OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                    .Select(t => t.LogicalAddress)
            );
        }

        private List<XMIOConfig> GetAnalogValueTags()
        {
            var analogTag = GetUsedAnlalogTags();

            return XMPS.Instance.LoadedProject.Tags
                .Where(t => analogTag.Contains(t.LogicalAddress))
                .ToList();
        }

        private HashSet<string> GetScheduleObjects()
        {
            var bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null)
            {
                return new HashSet<string>();
            }

            return new HashSet<string>(

         bacNetIP.Schedules.OrderBy(t => Convert.ToInt32(t.InstanceNumber)).Select(t => t.ObjectName.ToString()));
        }

        public static void AddTagtoBacNetObject(string TagName, string LogicalAddress, string TagLabel, IOType TagType, string TagMode, bool isFromMultistateSection)
        {
            BacNetIP bacNetIP = XMPS.Instance.LoadedProject.BacNetIP ?? new BacNetIP();
            //bool isFromMultistateSection = this.Tag?.ToString() == "Multistate Value";
            string address = LogicalAddress.ToString();
            string tagName = TagName.ToString();
            if (LogicalAddress.ToString().StartsWith("F2"))
            {
                BinaryIOV isAlreadyInsert = bacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(LogicalAddress));
                if (isAlreadyInsert == null)
                {
                    int binaryValueObjCount = bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Equals("5:Binary Value")).Count() > 0 ? bacNetIP.BinaryIOValues.Where(t => t.ObjectType.Equals("5:Binary Value")).Max(t => Convert.ToInt32(t.InstanceNumber)) + 1 : 0;
                    bacNetIP.BinaryIOValues.Add(new BinaryIOV("Binary Value:" + binaryValueObjCount.ToString(), binaryValueObjCount.ToString(), "5:Binary Value", "Binary Value:" + binaryValueObjCount.ToString(), TagName.ToString(), LogicalAddress.ToString()));
                }
                else
                {
                    isAlreadyInsert.ObjectName = TagName.ToString();
                }
            }
            else if (LogicalAddress.ToString().StartsWith("P5"))
            {
                AnalogIOV isAlreadyInsert = bacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(LogicalAddress));
                if (isAlreadyInsert == null)
                {
                    int analogValueObjCount = bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Equals("2:Analog Value")).Count() > 0 ? bacNetIP.AnalogIOValues.Where(t => t.ObjectType.Equals("2:Analog Value")).Max(t => Convert.ToInt32(t.InstanceNumber)) + 1 : 0;
                    bacNetIP.AnalogIOValues.Add(new AnalogIOV("Analog Value:" + analogValueObjCount.ToString(), analogValueObjCount.ToString(), "2:Analog Value", "Analog Value:" + analogValueObjCount.ToString(), TagName.ToString(), LogicalAddress.ToString(), false, "Analog"));
                }
                else
                {
                    isAlreadyInsert.ObjectName = TagName.ToString();
                }
            }
            else if (LogicalAddress.ToString().StartsWith("W4") &&
       !(TagLabel.Equals("Double Word", StringComparison.OrdinalIgnoreCase) ||
         TagLabel.Equals("DINT", StringComparison.OrdinalIgnoreCase) ||
         TagLabel.Equals("UDINT", StringComparison.OrdinalIgnoreCase) ||
         TagLabel.Equals("Byte", StringComparison.OrdinalIgnoreCase) ||
         TagLabel.Equals("INT", StringComparison.OrdinalIgnoreCase)))
            {
                // Only create Multistate Value if explicitly from Multistate section

                MultistateIOV existingMultistate = bacNetIP.MultistateValues
                    .FirstOrDefault(t => t.LogicalAddress.Equals(address));

                if (existingMultistate == null)
                {
                    int multistateValueObjCount = bacNetIP.MultistateValues
                        .Where(t => t.ObjectType.Equals("19:Multistate Value"))
                        .Select(t => int.Parse(t.InstanceNumber))
                        .DefaultIfEmpty(-1)
                        .Max() + 1;

                    bacNetIP.MultistateValues.Add(new MultistateIOV(
                        "Multistate Value:" + multistateValueObjCount,
                        multistateValueObjCount.ToString(),
                        "19:Multistate Value",
                        tagName,
                        address,
                        0  // Number of states
                    ));
                    AnalogIOV existingAnalog = bacNetIP.AnalogIOValues
                        .FirstOrDefault(t => t.LogicalAddress.Equals(address));
                    if (existingAnalog != null)
                    {
                        bacNetIP.AnalogIOValues.Remove(existingAnalog);
                    }
                }
                else
                {
                    existingMultistate.ObjectName = tagName;
                }

            }
            else if (TagType == IOType.DigitalInput || TagType == IOType.DigitalOutput ||
                    TagType == IOType.AnalogInput || TagType == IOType.AnalogOutput ||
                    TagType == IOType.UniversalInput || TagType == IOType.UniversalOutput)
            {
                AnalogIOV isAlreadyInsertAnalog = bacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(LogicalAddress));
                BinaryIOV isAlreadyInsertBinary = bacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(LogicalAddress));
                if (isAlreadyInsertAnalog != null)
                {
                    isAlreadyInsertAnalog.ObjectName = TagName.ToString();
                }
                if (isAlreadyInsertBinary != null)
                {
                    isAlreadyInsertBinary.ObjectName = TagName.ToString();
                }
            }

            //changing the object type for UI/UO on base of MODE Selection
            if (TagType.ToString().StartsWith("Universal"))
                ProcessUniversalIO(ref bacNetIP, LogicalAddress, TagType, TagName, TagMode);
        }

        private static void ProcessUniversalIO(ref BacNetIP bacNetIP, string LogicalAddress, IOType TagType, string TagName, string TagMode)
        {
            var textBoxTypeText = TagType.ToString();
            var comboBoxModeValue = TagMode.ToString();

            if (!textBoxTypeText.StartsWith("Universal"))
            {
                return;
            }

            bool isAlreadyInsert = bacNetIP.BinaryIOValues.Any(t => t.LogicalAddress.Equals(LogicalAddress));
            if (comboBoxModeValue.Equals("Digital"))
            {
                if (!isAlreadyInsert)
                {
                    //first Time when change the Mode to Digital
                    int oldInstanceNumber = Convert.ToInt32(bacNetIP.AnalogIOValues.FirstOrDefault(a => a.LogicalAddress.Equals(LogicalAddress)).InstanceNumber);
                    bacNetIP.AnalogIOValues.RemoveAll(t => t.LogicalAddress.Equals(LogicalAddress));
                    ProcessBinaryIO(bacNetIP, textBoxTypeText, oldInstanceNumber, TagName, LogicalAddress);
                }
                else
                {
                    //Edit already added Universal Object
                    BinaryIOV binaryIOV = bacNetIP.BinaryIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(LogicalAddress));
                    binaryIOV.ObjectName = TagName.ToString();
                }
            }
            else
            {
                if (isAlreadyInsert)
                {
                    //if mode change from digital to any other Mode.
                    int oldInstanceNumber = Convert.ToInt32(bacNetIP.BinaryIOValues.FirstOrDefault(a => a.LogicalAddress.Equals(LogicalAddress)).InstanceNumber);

                    bacNetIP.BinaryIOValues.RemoveAll(t => t.LogicalAddress.Equals(LogicalAddress));
                    ProcessAnalogIO(bacNetIP, textBoxTypeText, oldInstanceNumber, TagName, LogicalAddress);
                }
                else
                {
                    //Edit already added Universal Object
                    AnalogIOV analogIOV = bacNetIP.AnalogIOValues.FirstOrDefault(t => t.LogicalAddress.Equals(LogicalAddress));
                    analogIOV.ObjectName = TagName.ToString();
                }
            }
        }

        private static void ProcessAnalogIO(BacNetIP bacNetIP, string textBoxTypeText, int InstanceNo, string TagName, string LogicalAddress)
        {
            if (textBoxTypeText.Equals("UniversalInput"))
            {
                string objectIdentifier = $"Analog Input:" + InstanceNo.ToString("D3");
                bacNetIP.AnalogIOValues.Add(new AnalogIOV(objectIdentifier, InstanceNo.ToString("D3"), "0:Analog Input", objectIdentifier, TagName.ToString(), LogicalAddress.ToString(), false));
            }
            else
            {
                string objectIdentifier = $"Analog Output:" + InstanceNo.ToString("D3");
                bacNetIP.AnalogIOValues.Add(new AnalogIOV(objectIdentifier, InstanceNo.ToString("D3"), "1:Analog Output", objectIdentifier, TagName.ToString(), LogicalAddress.ToString(), false));
            }
        }

        private static void ProcessBinaryIO(BacNetIP bacNetIP, string textBoxTypeText, int instaceNo, string TagName, string LogicalAddress)
        {
            if (textBoxTypeText.Equals("UniversalInput"))
            {
                string objectIdentifier = $"Binary Input";
                bacNetIP.BinaryIOValues.Add(new BinaryIOV(objectIdentifier, instaceNo.ToString("D3"), "3:Binary Input", objectIdentifier, TagName.ToString(), LogicalAddress.ToString(), false));
            }
            else
            {
                string objectIdentifier = $"Binary Output";
                bacNetIP.BinaryIOValues.Add(new BinaryIOV(objectIdentifier, instaceNo.ToString("D3"), "4:Binary Output", objectIdentifier, TagName.ToString(), LogicalAddress.ToString(), false));
            }
        }

        private HashSet<string> GetUsedAnlalogTags()
        {
            var bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null)
            {
                return new HashSet<string>();
            }

            return new HashSet<string>(
                bacNetIP.AnalogIOValues
                    .Where(t => t.LogicalAddress.StartsWith("P5") || t.LogicalAddress.StartsWith("W4")).OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                    .Select(t => t.LogicalAddress)
            );
        }

        public HashSet<string> GetBACNETIPTags()
        {
            HashSet<string> tagname = new HashSet<string>();
            var tags = GetOnBoardExAddObjects();
            var binaryTag = GetUsedBinaryTags();
            var analogTag = GetUsedAnlalogTags();
            var mulstateTag = GetUsedMultiStateTags();

            var filteredTags = XMPS.Instance.LoadedProject.Tags
                              .Where(t => !t.LogicalAddress.StartsWith("Q") && !t.LogicalAddress.StartsWith("I")).ToList();
            tagname.UnionWith(tags.Select(t => t.Tag + "$BinaryValue").ToList());
            tagname.UnionWith(filteredTags.Where(t => binaryTag.Contains(t.LogicalAddress)).Select(t => t.Tag + "$BinaryValue").ToList());
            tagname.UnionWith(filteredTags.Where(t => analogTag.Contains(t.LogicalAddress)).Select(t => t.Tag + "$AnalogValue").ToList());
            tagname.UnionWith(filteredTags.Where(t => mulstateTag.Contains(t.LogicalAddress)).Select(t => t.Tag + "$MultiStateValue").ToList());
            tagname.UnionWith(GetScheduleObjects().Select(item => item + "$Schedule"));
            tagname.UnionWith(GetCalendarObjects().Select(item => item + "$Calendar"));
            tagname.UnionWith(GetNotificationObjects().Select(item => item + "$Notification Class"));
            return tagname;
        }

        private List<XMIOConfig> GetOnBoardExAddObjects()
        {
            var tags = new List<XMIOConfig>();
            var binaryOnBoards = XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.
                                Where(t => t.LogicalAddress.Contains(".") && (t.LogicalAddress.StartsWith("Q") || t.LogicalAddress.StartsWith("I"))).Select(t => t.LogicalAddress).ToList();
            var analogsOnBoards = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Where(t => t.LogicalAddress.StartsWith("Q") || t.LogicalAddress.StartsWith("I"))
                                    .Select(t => new { t.LogicalAddress, t.InstanceNumber }).ToList();
            analogsOnBoards.AddRange(XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(t => !t.LogicalAddress.Contains(".") && (t.LogicalAddress.StartsWith("Q") || t.LogicalAddress.StartsWith("I")))
                                    .Select(t => new { t.LogicalAddress, t.InstanceNumber }).ToList());

            analogsOnBoards = analogsOnBoards
                                .GroupBy(t => t.InstanceNumber.ToString().Length == 1 ? ("00" + t.InstanceNumber.ToString()).Substring(0, 2) : (t.InstanceNumber.ToString().Length > 3 ? t.InstanceNumber.ToString().Substring(0, 3) : t.InstanceNumber.ToString().Substring(0, 2)))
                                .SelectMany(group => group
                                .OrderBy(t => t.LogicalAddress.StartsWith("I") ? 0 : 1)
                                .ThenBy(t => t.LogicalAddress.Split(':')[1])).ToList();
            //adding binary on-boards expansion bacNet objects
            foreach (string binaryAdd in binaryOnBoards)
            {
                tags.Add(XMPS.Instance.LoadedProject.Tags
                    .FirstOrDefault(t => t.LogicalAddress.Equals(binaryAdd)));
            }

            //adding analog on-boards expansion bacNet objects
            foreach (string logicalAdd in analogsOnBoards.Select(t => t.LogicalAddress).ToList())
            {
                tags.Add(XMPS.Instance.LoadedProject.Tags
                      .FirstOrDefault(t => t.LogicalAddress.Equals(logicalAdd)));
            }

            return tags;
        }

        public List<XMIOConfig> GetBinaryValueTags()
        {

            var binaryTag = GetUsedBinaryTags();

            return XMPS.Instance.LoadedProject.Tags
                .Where(t => binaryTag.Contains(t.LogicalAddress))
                .ToList();
        }
        private HashSet<string> GetUsedBinaryTags()
        {
            var bacNetIP = XMPS.Instance.LoadedProject.BacNetIP;
            if (bacNetIP == null)
            {
                return new HashSet<string>();
            }

            return new HashSet<string>(
                bacNetIP.BinaryIOValues
                    .Where(t => t.LogicalAddress.StartsWith("F2")).OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                    .Select(t => t.LogicalAddress)
            );
        }
        public List<XMIOConfig> GetDefaultTags()
        {
            return new List<XMIOConfig>();
        }

    }
}
