using iTextSharp.text;
using iTextSharp.xmp.impl;
using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.LadderLogic;

namespace XMPS2000.LadderLogic
{
    public static class XMProValidator
    {

        /// <summary>
        /// Funcntion to check if the entered Value for Bit Address it Valid or Noe
        /// </summary>
        /// <param name="address"></param>Value Entered By The User
        /// <returns></returns>Is the Bit Value Entered Valid
        public static bool IsValidBitAddress(this string address)
        {
            if (address == "No available address found" || string.IsNullOrEmpty(address) || !address.Contains(":")) return false;
            // Let's set up a regular expression for validating bit addresses
            string regExString1;

            // Since . is a special character, replace it with @ for temporary checking,
            // and also convert @ with another special char # so that it does not accept # and @ both
            address = address.Replace("@", "#");
            address = address.Replace(".", "@");

            // 1st let's set Starting digits for distinguishing Address Type. E.g., Q0 for output and I1 for input
            regExString1 = "^(Q0|I1):";
            // And then append a 3-digit word number i.e. 000 to 999
            regExString1 += "([0-9]{3})";
            // And then .00 to .15 digital bit numbers
            // Check for @ instead of . as we have already replaced the same in the code above
            regExString1 += "(@0[0-9]|@1[0-5])$";
            string regExString2;
            // Special handling of Flag addresses (it is a bit address without having .00 to .15 values at the end! e.g., F2:000 to F2:999)
            regExString2 = "^(F2|S3)"; /* starting string */
            //regExString3 += GetRegString(address);
            Regex regEx1 = new Regex(regExString1);
            Regex regEx2 = new Regex(regExString2);
            if (!regEx1.IsMatch(address) && !regEx2.IsMatch(address.Split(':')[0]))
            {
                if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && T.Mode == "Digital").Count() > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                if (regEx2.IsMatch(address.Split(':')[0]))
                {
                    string digitPart = address.Split(':')[1].ToString();
                    if (!Basevalidated(digitPart))
                        return false;
                    if (XMPS.Instance.MemoryAllocation.Where(m => m.Initial == address.Split(':')[0] && m.Limit > Convert.ToInt32(digitPart)).Count() > 0)
                        return true;
                    else
                        return false;
                }
                address = address.Replace("@", ".");
                var label = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address).Select(T => T.Label).FirstOrDefault();
                if (XMPS.Instance.LoadedProject.Tags.Any(T => T.LogicalAddress == address &&(label.EndsWith("OR") || label.EndsWith("OL"))&& XMPS.Instance.PlcModel.StartsWith("XBLD")))
                {
                    return false;
                }
                address = address.Replace(".", "@");
                if (XMPS.Instance.LoadedProject.Tags.Any(T => T.LogicalAddress == address && T.Label != "Bool"))
                    return false;
                else
                    return true;
            }
        }

        public static XMIOConfig GetTagFromTagName(string tag)
        {
            return XMPS.Instance.LoadedProject.Tags.Where(t => t.Tag == tag).FirstOrDefault();
        }



        ///
        /// Function to check if user have entered valid Word Address
        /// </summary>
        /// <param name="address"></param> Value entered by the user
        /// <returns></returns>Is the value enteted by the user valid Word Address
        /// 
        public static bool IsValidWordAddress(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            if (address == "No available address found" || string.IsNullOrEmpty(address) || !address.Contains(":")) return false;
            regExString = "^(Q0|S3|I1|W4|T6|C7)";
            //regExString += GetRegString(address);
            // And then append the 3 digit word number i.e. 000 to 255
            //regExString += "((0[0-9][0-9]|[1-9][0-9][0-9]|100[0-9]|101[0-9]|102[0-3]))$";   //
            Regex regEx2 = new Regex(regExString);
            string digitPart = address.Split(':')[1].ToString();
            if (!Basevalidated(digitPart))
                return false;
            if (!regEx2.IsMatch(address.Split(':')[0]))
            {
                return false;
            }
            else
            {
                if (XMPS.Instance.MemoryAllocation.Where(m => m.Initial == address.Split(':')[0] && Convert.ToInt32(digitPart) >= 0 && m.Limit > Convert.ToInt32(digitPart)).Count() > 0)
                {
                    if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && T.Label != "Word").Count() > 0)
                    {
                        if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address).Count() > 0 && (address.StartsWith("Q") || address.StartsWith("I")))
                        {
                            return true;
                        }
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return false;
            }
        }

        private static bool Basevalidated(string digitPart)
        {
            bool isValid = true;
            if (!int.TryParse(digitPart, out int validno))
                isValid = false;
            if (digitPart.Length < 3)
                isValid = false;
            if (validno < 0)
                isValid = false;
            if (digitPart.StartsWith("-"))
                isValid = false;
            if (validno < 1000 && digitPart.Length != 3)
                isValid = false;

            return isValid;
        }

        public static bool IsValidWordAddress(this string address, string datatype)
        {
            if (!address.Contains(':')) return false;
            // Let's setup regular expression for validating bit address
            string regExString;
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString = "^(Q0|S3|I1|W4|Y9|T6|C7)";
            // And then append the 3 digit word number i.e. 000 to 255
            string datatype1 = datatype == "Double Word" ? "UDINT" : datatype == "UDINT" ? "Double Word" : "";
            Regex regEx2 = new Regex(regExString);
            string digitPart = address.Split(':')[1].ToString();
            if (!Basevalidated(digitPart))
                return false;
            if (!regEx2.IsMatch(address.Split(':')[0]))
            {
                return false;
            }
            else
            {
                if (XMPS.Instance.MemoryAllocation.Where(m => m.Initial == address.Split(':')[0] && m.Limit > Convert.ToInt32(address.Split(':')[1].ToString())).Count() > 0)
                {
                    if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && T.Label != datatype && T.Label != datatype1).Count() > 0)
                    {
                        if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address).Count() > 0 && (address.StartsWith("Q") || address.StartsWith("I")))
                        {
                            return true;
                        }
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return false;
            }

        }
        /// <summary>
        /// Function to check if user have entered valid Word Address for ModBus include P5 Addresses
        /// </summary>
        /// <param name="address"></param> Value entered by the user
        /// <returns></returns>Is the value enteted by the user valid Word Address
        public static bool IsValidWordAddressForModBus(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString = "^(Q0|I1|S3|W4|Y9|T6|C7|P5):";
            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "((0[0-9][0-9]|[1-9][0-9][0-9]|100[0-9]|101[0-9]|102[0-3]))$";

            Regex regEx2 = new Regex(regExString);

            if (!regEx2.IsMatch(address))
                return false;
            else
                return true;
        }
        /// <summary>
        /// Function to check if user have entered valid Word Address for ModBus include P5 Addresses
        /// </summary>
        /// <param name="address"></param> Value entered by the user
        /// <returns></returns>Is the value enteted by the user valid Word Address
        public static bool IsValidBoolWordAddressForModBus(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString = "^(Q0|I1|S3|W4|Y9|T6|C7|F2):";
            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "((0[0-9][0-9]|[1-9][0-9][0-9]|100[0-9]|101[0-9]|102[0-3]))$";
            //For validating On-Boarad IO's || bit address.
            string regExString1;
            string logicalAddress = address;
            logicalAddress = logicalAddress.Replace("@", "#");
            logicalAddress = logicalAddress.Replace(".", "@");
            regExString1 = "^(Q0|I1):";
            regExString1 += "([0-9]{3})";
            regExString1 += "(@0[0-9]|@1[0-5])$";

            Regex regEx2 = new Regex(regExString);
            Regex regEx1 = new Regex(regExString1);

            if (!regEx2.IsMatch(address) && !regEx1.IsMatch(logicalAddress))
                return false;
            else
            {
                if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && (T.Label != "Word" && T.Label != "Bool")).Count() > 0)
                {
                    if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address).Count() > 0 && (address.StartsWith("Q") || address.StartsWith("I")))
                    {
                        return true;
                    }
                    return false;
                }
                else
                    return true;
            }
        }
        /// <summary>
        /// Function to check if the value enterd by the user for Retentive Address is Valid or not
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <returns></returns>Is the value entered by the user a valid Retentive Address
        public static bool IsValidRetentiveAddress(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString = "^(Q0|F2|W4|P5|T6|C7):";
            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$";
            Regex regEx2 = new Regex(regExString);
            // Let's setup regular expression for validating bit address
            string regExString1;
            //Since . is special character replace it with @ for temporary checking and also convert @ with another spectial char # so that it does not accept # and @ both
            address = address.Replace("@", "#");
            address = address.Replace(".", "@");
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString1 = "^Q0:";
            // And then append 3 digit word number i.e. 000 to 255
            regExString1 += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])";
            // And then .00 to .15 digital bit numbers 
            //Check for @ instead of . as we have already replaced the same in the code above
            regExString1 += "(@0[0-9]|@1[0-5])$";
            Regex regEx1 = new Regex(regExString1);

            if (!regEx1.IsMatch(address) && !regEx2.IsMatch(address))
                return false;
            else
                return true;
        }
        /// <summary>
        /// Function to check if the value enterd by the user for Retentive Address is Valid or not
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <returns></returns>Is the value entered by the user a valid Retentive Address
        public static bool IsValidInitialAddress(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString = "^(Q0|F2|W4|P5|X8|Y9):";
            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$";
            Regex regEx2 = new Regex(regExString);
            // Let's setup regular expression for validating bit address
            string regExString1;
            //Since . is special character replace it with @ for temporary checking and also convert @ with another spectial char # so that it does not accept # and @ both
            address = address.Replace("@", "#");
            address = address.Replace(".", "@");
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString1 = "^Q0:";
            // And then append 3 digit word number i.e. 000 to 255
            regExString1 += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])";
            // And then .00 to .15 digital bit numbers 
            //Check for @ instead of . as we have already replaced the same in the code above
            regExString1 += "(@0[0-9]|@1[0-5])$";
            Regex regEx1 = new Regex(regExString1);
            if (!regEx1.IsMatch(address) && !regEx2.IsMatch(address))
                return false;
            else
                return true;
        }
        /// <summary>
        /// Get the function code name from the description
        /// </summary>
        /// <param name="functionCode"></param>
        /// <returns>function code</returns>
        public static int GetFunctionCode(string functionCode)
        {
            switch (functionCode)
            {
                case "Read Coil (01)":
                    return 01;
                case "Read Discrete Input (02)":
                    return 02;
                case "Write Single Coil (05)":
                    return 05;
                case "Write Multiple Coils (15)":
                    return 15;
                case "Write Single Holding Register (06)":
                    return 6;
                case "Write Multiple Holding Registers (16)":
                    return 16;
                default:
                    return -1;
            }
        }
        public static string GetTheAddressFromTag(string TagName)
        {
            XMPS xm = XMPS.Instance;
            if (TagName == "-Select Tag Name-" || TagName == null || TagName == "")
            {
                return "";
            }
            else
            {
                //adding check for not take commeted tag as a input when changing selected index change.
                var LogicalAddress = xm.LoadedProject.Tags.Where(d => d.Tag == TagName && !(d.LogicalAddress.StartsWith("'"))).Select(d => d.LogicalAddress).FirstOrDefault();
                LogicalAddress = LogicalAddress is null ? TagName : LogicalAddress;
                return LogicalAddress.ToString();
            }
        }

        public static string GetNextAddress(string SelectedDataType, string instruction, List<string> addedAddresses = null)
        {
            XMPS xm = XMPS.Instance;
            string start = "";
            int NextAddCount;
            if (SelectedDataType == "Bool")
            {
                start = "F2";
            }
            else if (SelectedDataType == "Word" || SelectedDataType == "Byte" || SelectedDataType == "Double Word" || SelectedDataType == "TOF" || SelectedDataType == "TON" || SelectedDataType == "Int" || SelectedDataType == "DINT" || SelectedDataType == "UDINT")
            {
                start = (instruction == "Counter") ? "C7" : (instruction.Contains("Timer") ? "T6" : "W4");
            }
            else if (SelectedDataType == "Real")
            {
                start = "P5";
            }
            else
            {
                return null;
            }

            List<int> sam = new List<int>();
            List<int> addresses = new List<int>();

            var list = new List<int>();

            sam = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith(start)).Where(d => !d.LogicalAddress.Contains(".")).Select(d => int.Parse(d.LogicalAddress.Split(':')[1])).ToList();
            if (addedAddresses != null)
            {
                foreach (string address in addedAddresses)
                {
                    if (!address.StartsWith("Q") && !address.StartsWith("I") && !address.StartsWith("S3") && address.StartsWith(start))
                        sam.Add(int.Parse(address.Split(':')[1]));
                }

            }

            ///Add all the next tags which are od double data type
            List<int> dbllist = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith(start)).Where(d => d.Label == "Double Word" || d.Label == "DINT" || d.Label == "UDINT").Where(d => !d.LogicalAddress.Contains(".")).Select(d => int.Parse(d.LogicalAddress.Split(':')[1]) + 1).ToList();
            list.AddRange(sam);
            list.AddRange(dbllist);
        NextDoubleAdd:
            var result = 0;
            result = Enumerable.Range(0, Convert.ToInt32(GetMaxLength(start + ":000"))).Except(list).DefaultIfEmpty(-1).Min();
            //Added check for double word if 1st and 3rd address is used then 2nd will not take for double Word.
            string nextAvailableAddress = (result != -1) ? $"{start}:{result:D3}" : "No available address found";
            if (nextAvailableAddress != null && SelectedDataType == "Double Word")
            {
                string[] parts = nextAvailableAddress.Split(':');
                if (parts.Length > 1)
                {
                    if (int.TryParse(parts[1], out int result1))
                    {
                        NextAddCount = result1;
                        string nextAdd = $"{start}:{NextAddCount + 1:D3}";
                        var duplicate = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == nextAdd).FirstOrDefault();
                        if (duplicate != null)
                        {
                            list.Add(NextAddCount);
                            goto NextDoubleAdd;
                        }

                    }
                }
            }
            if (result != -1)
            {
                addresses.Add(int.Parse(nextAvailableAddress.Split(':')[1]));
            }
            return nextAvailableAddress;
        }
        public static string GetTheTagnameFromAddress(string Address)
        {
            XMPS xm = XMPS.Instance;
            if (Address == "-Select Tag Name-")
            {
                return "";
            }
            else
            {
                var Tagname = xm.LoadedProject.Tags.Where(d => d.LogicalAddress == Address).Select(d => d.Tag).FirstOrDefault();
                return Tagname?.ToString();
            }
        }

        public static List<string> FillTagOperands1(string DataType, string udfbName = "", string instructionName = "")
        {
            XMPS xm = XMPS.Instance;
            List<string> result1 = new List<string>();
            if (DataType == "Bool")
            {
                result1 = xm.LoadedProject.BacNetIP.Schedules.Where(t => t.ScheduleValue.Equals(0)).Select(t => t.ObjectName).Where(name => name != null).OrderBy(name => name).ToList();
            }
            else if (DataType == "Real")
            {
                result1 = xm.LoadedProject.BacNetIP.Schedules.Where(t => t.ScheduleValue.Equals(1)).Select(t => t.ObjectName).Where(name => name != null).OrderBy(name => name).ToList();
            }
            result1.Insert(0, "-Select Tag Name-");
            return result1;
        }

        public static List<string> FillTagOperands(string DataType, string udfbName = "")
        {
            XMPS xm = XMPS.Instance;
            List<string> result = new List<string>();
            List<string> TagList = new List<string>();
            if (udfbName is null || udfbName == "")
            {
                if (DataType == "Bool")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.IoList == Core.Types.IOListType.OnBoardIO && d.LogicalAddress.Contains(".") && (d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "") && (!d.Model.StartsWith("XBLD") || !(d.Label.EndsWith("_OR") || d.Label.EndsWith("_OL"))));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains('.') && (t.Label == DataType || t.Mode == "Digital")).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.AddRange(xm.LoadedProject.Tags.Where(t => t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.OnBoardIO && (!t.Model.StartsWith("XBLD") || !(t.Tag.EndsWith("_OR") || t.Tag.EndsWith("_OL")))).Select(t => t.Tag).Where(t => t != null).OrderBy(t => t).ToList());
                    TagList.Sort();
                }
                else if (DataType == "TCAddress")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.Retentive == false && (d.LogicalAddress.StartsWith("T") || d.LogicalAddress.StartsWith("C") || d.LogicalAddress.StartsWith("W") || d.LogicalAddress.StartsWith("P"))).Where(d => d.Label == "Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.Sort();

                }
                else if (DataType == "Pack-Bool")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (!d.LogicalAddress.StartsWith("S") && !d.LogicalAddress.StartsWith("Q") && !d.LogicalAddress.StartsWith("I") && (d.LogicalAddress.StartsWith("F2") || d.Label == "Bool")) && (d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == ""));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                }
                else if (DataType == "TCAddress")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("T") || d.LogicalAddress.StartsWith("C") || d.LogicalAddress.StartsWith("W") || d.LogicalAddress.StartsWith("P") && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);

                }
                else if (DataType == "TON" || DataType == "TOFF" || DataType == "TP" || DataType == "CTU" || DataType == "CTD")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.Retentive == false && (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && (!d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                }
                else if (DataType == "RTON")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.Retentive == true && (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && (!d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                }
                else if (DataType == "Word-Rentive")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") && d.Retentive == true || d.LogicalAddress.StartsWith("C7") && d.Retentive == true || d.LogicalAddress.StartsWith("T6") && d.Retentive);
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);

                }
                else if (DataType == "Word")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && (!d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    if (!xm.LoadedProject.CPUDatatype.Equals("Real"))
                        TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL && t.Mode != "Digital").Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.Sort();

                }
                else if (DataType == "Pack-Word")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && (!d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.Sort();

                }
                else if (DataType == "Double Word")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Double Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.Sort();

                }
                else if (DataType == "DINT")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "DINT").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.Sort();

                }
                else if (DataType == "UDINT")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "UDINT").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.Sort();

                }
                else if (DataType == "Byte")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("S3")) && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Byte").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.Sort();
                }
                else if (DataType == "PulseVelocity")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.IoList == Core.Types.IOListType.OnBoardIO && (d.Label.Equals("DO0") || d.Label.Equals("DO1")));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.Sort();
                }
                else
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.Label.Equals(DataType) && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    if (DataType == "Real" && xm.LoadedProject.CPUDatatype.Equals("Real"))
                        TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.AddRange(taglist);
                    TagList.Sort();

                }
                if (DataType != "Bool")
                {
                    if (TagList.Count == 0 && DataType == "Word")
                    {
                        var tag = xm.LoadedProject.Tags.Where(d => (d.IoList == Core.Types.IOListType.OnBoardIO) && (XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "") && !d.LogicalAddress.Contains("."));
                        var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                        TagList.AddRange(taglist);
                    }
                }
            }

            else
            {
                if (DataType == "Bool")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.Contains(".") || d.LogicalAddress.StartsWith("F2") || d.Label == "Bool").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                    TagList.AddRange(xm.LoadedProject.Tags.Where(t => t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.OnBoardIO).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.Sort();
                }
                else if (DataType == "Pack-Bool")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (!d.LogicalAddress.StartsWith("S") && !d.LogicalAddress.StartsWith("Q") && !d.LogicalAddress.StartsWith("I") && (d.LogicalAddress.StartsWith("F2") || d.Label == "Bool"))).Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                }
                else if (DataType == "TCAddress")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("T") || d.LogicalAddress.StartsWith("C") || d.LogicalAddress.StartsWith("W") || d.LogicalAddress.StartsWith("S"))).Where(d => d.Label == "Word").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());

                }
                else if (DataType == "Word")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                    if (!xm.LoadedProject.CPUDatatype.Equals("Real"))
                        TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL && t.Mode != "Digital").Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.Sort();

                }
                else if (DataType == "Pack-Word")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                    TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.Sort();

                }
                else if (DataType == "Double Word")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Double Word").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                    TagList.Sort();
                }
                else if (DataType == "TON" || DataType == "TOFF" || DataType == "TP" || DataType == "CTU" || DataType == "CTD")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                    TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.Sort();
                }
                else if (DataType == "RTON")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.Retentive == true && (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3")) && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                    TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.Sort();
                }
                else if (DataType == "Word-Rentive")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.Retentive == true && (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("T6")));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.Sort();
                }
                else if (DataType == "Pack-Word")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.Label == "Word" && (!d.LogicalAddress.StartsWith("'"))).Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL).Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                    TagList.Sort();
                }
                else if (DataType == "Word-Rentive")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") && d.Retentive == true || d.LogicalAddress.StartsWith("C7") && d.Retentive == true || d.LogicalAddress.StartsWith("T6") && d.Retentive).Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.Sort();

                }
                else if (DataType == "Real")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("P") || d.LogicalAddress.StartsWith("S3") && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Real").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                    if (xm.LoadedProject.CPUDatatype.Equals("Real"))
                        TagList.AddRange(xm.LoadedProject.Tags.Where(t => !t.LogicalAddress.Contains(".") && t.Model != "User Defined Tags" && t.IoList != Core.Types.IOListType.Default && t.IoList != Core.Types.IOListType.NIL && t.Mode != "Digital").Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList());
                }
                else if (DataType == "DINT")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "DINT").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                    TagList.Sort();

                }
                else if (DataType == "UDINT")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "UDINT").Where(d => d.Model == udfbName + " Tags" || d.Model is null || XMPS.Instance.PlcModels.Contains(d.Model));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.Sort();

                }
                else if (DataType == "PulseVelocity")
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.IoList == Core.Types.IOListType.OnBoardIO && (d.Label.Equals("DO0") || d.Label.Equals("DO1")));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    TagList.Sort();
                }
                else
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.Label.Equals(DataType) && d.Model != null && d.Model == udfbName + " Tags" && !(d.LogicalAddress.StartsWith("'")));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                    UDFBInfo udfbinfo = xm.LoadedProject.UDFBInfo.Where(d => d.UDFBName == udfbName).FirstOrDefault();
                    TagList.AddRange(udfbinfo.UDFBlocks.Where(d => d.DataType == DataType).Select(d => d.Text).ToList());
                    TagList.Sort();

                }
            }
            result.Add("-Select Tag Name-");
            result.AddRange(TagList);
            return result;
        }

        public static bool IsValidInitialValueForAddress(this string value, string address, string ValueType)
        {
            try
            {
                string dataType = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address).Select(T => T.Label).FirstOrDefault();
                ValueType = dataType != null ? dataType : ValueType;
                ValueType = DataType.List.Where(d => d.Text == ValueType).Count() > 0 ? ValueType : address.Contains('.') ? "Bool" : XMPS.Instance.LoadedProject.CPUDatatype;
                var tag = XMPS.Instance.LoadedProject.Tags.Where(e => e.LogicalAddress == address && e.Label.Equals("Word")).Select(e => e.Tag).FirstOrDefault();
                var IsTagChecked = XMPS.Instance.LoadedProject?.BacNetIP?.MultistateValues?.Where(e => e.LogicalAddress == address && e.IsEnable == true).Select(e => e.ObjectName).FirstOrDefault();
                var initialValue = XMPS.Instance.LoadedProject.BacNetIP?.MultistateValues?.Where(a => a.ObjectName != null && a.ObjectName == tag).Select(a => (int?)a.NumberOfStates).FirstOrDefault() ?? 1;
                bool result = false;
                switch (ValueType)
                {
                    case "Bool":
                        if (Convert.ToInt16(value) == 0 || Convert.ToInt16(value) == 1)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case "Byte":
                        if (value.StartsWith("-") || !byte.TryParse(value, out _))
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                        break;
                    case "Word":
                        if (XMPS.Instance.PlcModel.StartsWith("XBLD") && IsTagChecked != null)
                        {
                            if (int.TryParse(value, out int intValue) && intValue >= 1 && intValue <= 65536 && int.TryParse(initialValue.ToString(), out int maxValue) && intValue <= maxValue)
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            if (Enumerable.Range(0, 65536).Contains(Convert.ToInt32(value)))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        break;
                    case "Double Word":
                        if (value.StartsWith("-") || !uint.TryParse(value, out _))
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                        break;
                    case "Int":
                        if (Type.GetTypeCode(Convert.ToInt16(value).GetType()) == TypeCode.Int16)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case "DINT":
                        long resultDINT;
                        if (long.TryParse(value, out resultDINT))
                        {
                            if (!(resultDINT >= -2147483648 && resultDINT <= 2147483647))
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        break;
                    case "UDINT":
                        uint resultUdint;
                        if (uint.TryParse(value, out resultUdint))
                        {
                            if (value.StartsWith("-") || !(resultUdint >= 0 && resultUdint <= 4294967295))
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        break;
                    case "Real":
                        if (value.Contains("."))
                        {
                            if (decimal.Parse(value) != 0)
                            {
                                value = value.Replace(".", "");
                            }
                        }
                        if (Type.GetTypeCode(Convert.ToInt32(value).GetType()) == TypeCode.Int32)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Function to check if the value enterd by the user for Retentive Address is Valid or not
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <returns></returns>Is the value entered by the user a valid Retentive Address
        public static bool IsValidAddressInitial(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString = "^(Q0|F2|W4|P5|T6|C7):";
            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$";
            Regex regEx2 = new Regex(regExString);
            // Let's setup regular expression for validating bit address
            string regExString1;
            //Since . is special character replace it with @ for temporary checking and also convert @ with another spectial char # so that it does not accept # and @ both
            address = address.Replace("@", "#");
            address = address.Replace(".", "@");
            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString1 = "^Q0:";
            // And then append 3 digit word number i.e. 000 to 255
            regExString1 += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])";
            // And then .00 to .15 digital bit numbers 
            //Check for @ instead of . as we have already replaced the same in the code above
            regExString1 += "(@0[0-9]|@1[0-5])$";
            Regex regEx1 = new Regex(regExString1);

            if (!regEx1.IsMatch(address) && !regEx2.IsMatch(address))
                return false;
            else
                return true;
        }
        /// <summary>
        /// Function to check if the value enterd by the user for Memory Adress is Valid or not
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <returns></returns>Is the value entered by the user a valid Memory Address
        public static bool IsValidMemoryAddress(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;

            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString = "^(F2|W4|P5|S3|T6|C7):";

            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$";

            Regex regEx2 = new Regex(regExString);

            if (!regEx2.IsMatch(address))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Function to check if the Value entered by the user is valid word address but not the Floting Address used for Time, Counter Data types and Bit Shift,Arithmatic,Limit and Compare Instructions only
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <returns></returns>If the value entered by the user a valid Non Floating Word Address 
        public static bool IsValidNonFlotingWordAddress(this string address)
        {
            // Let's setup regular expression for validating bit address
            string regExString;

            // 1st let's set Starting digits for distinguishing Address Type. E.g. Q0 for output and I1 for input
            regExString = "^(Q0|I1|S3|W4|Y9|T6|C7):";
            // And then append the 3 digit word number i.e. 000 to 255
            regExString += "((0[0-9][0-9]|[1-9][0-9][0-9]|100[0-9]|101[0-9]|102[0-3]))$";

            Regex regEx2 = new Regex(regExString);

            if (!regEx2.IsMatch(address))
                return false;
            else
                return true;
        }


        /// <summary>
        /// Function to check if the value entered by the user is valid Bit Address for Output i.e. it should start with Q0 always
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <param name="outputType"></param>Output type selected by the user
        /// <returns></returns>If the Value entered is valid Bit Address for Output
        public static bool IsValidOutputBitAddress(this string address, string outputType, List<XMIOConfig> OutputAddresses)
        {
            XMIOConfig Validate = null;
            switch (outputType)
            {
                case "On-board":
                    Validate = OutputAddresses.Where(L => L.LogicalAddress == address && L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.OnBoardIO).FirstOrDefault();
                    break;
                case "Remote":
                    Validate = OutputAddresses.Where(L => L.LogicalAddress == address && L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.RemoteIO).FirstOrDefault();
                    break;
                case "Expansion":
                    Validate = OutputAddresses.Where(L => L.LogicalAddress == address && L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO).FirstOrDefault();
                    break;
                case "Memory Address Variable":
                    if (!IsValidBitAddress(address))
                    {
                        Validate = null;
                        if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && T.Model == null && T.Label == "Bool").Count() > 0)
                            Validate = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && T.Model == null && T.Label == "Bool").FirstOrDefault();
                    }
                    else
                    {
                        Validate = OutputAddresses.FirstOrDefault();
                    }
                    break;
                default:
                    return true;
            }



            if (Validate == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Function to check if the value entered by the user is valid word Address for Output i.e. it should start with Q0 always
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <param name="outputType"></param>Output type selected by the user
        /// <returns></returns>If the Value entered is valid word Address for Output
        public static bool IsValidOutputWordAddress(this string address, string outputType, List<XMIOConfig> OutputAddresses)
        {
            bool result = false;
            string regExString;
            XMIOConfig Validate = null;
            switch (outputType)
            {
                case "On-board":
                    Validate = OutputAddresses.Where(L => L.LogicalAddress == address && L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.OnBoardIO).FirstOrDefault();
                    if (Validate == null)
                    {
                        Validate = OutputAddresses.Where(L => L.LogicalAddress.StartsWith(address) && L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.OnBoardIO).FirstOrDefault();
                        if (Validate != null)
                        {
                            result = IsValidWordAddress(address);
                        }
                    }
                    break;
                case "Remote":
                    Validate = OutputAddresses.Where(L => L.LogicalAddress == address && L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.RemoteIO).FirstOrDefault();
                    if (Validate == null)
                    {
                        Validate = OutputAddresses.Where(L => L.LogicalAddress.StartsWith(address) && L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.RemoteIO).FirstOrDefault();
                        if (Validate != null)
                        {
                            result = IsValidWordAddress(address);
                        }
                    }
                    break;
                case "Expansion":
                    Validate = OutputAddresses.Where(L => L.LogicalAddress == address && L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO).FirstOrDefault();
                    if (Validate == null)
                    {
                        Validate = OutputAddresses.Where(L => L.LogicalAddress.StartsWith(address) && L.Type.ToString().Contains("Output") && L.IoList == Core.Types.IOListType.ExpansionIO).FirstOrDefault();
                        if (Validate != null)
                        {
                            result = IsValidWordAddress(address);
                        }
                    }
                    break;
                case "Memory Address Variable":
                    // Acceptable format e.g.W4:000 to W4:255
                    // Regular expression for matching 'W4:'
                    string w4Regex = string.Empty;
                    w4Regex = "^(W4|P5|T6|C7)";
                    //w4Regex += GetRegString(address);
                    // Create a Regex object for the 'W4:' pattern
                    Regex w4RegexPattern = new Regex(w4Regex);
                    string digitPart = address.Split(':')[1].ToString();
                    if (!Basevalidated(digitPart))
                        return false;
                    // Check if the input address matches the 'W4:' pattern
                    if (w4RegexPattern.IsMatch(address.Split(':')[0]))
                    {
                        // 'W4:' prefix is present in the address
                        if (XMPS.Instance.MemoryAllocation.Where(m => m.Initial == address.Split(':')[0] && m.Limit > Convert.ToInt32(digitPart)).Count() > 0)
                            result = true;
                        else
                            result = false;
                        break;
                    }
                    else
                    {
                        // 'W4:' prefix is not present in the address
                        result = false;
                    }

                    regExString =
                        "^(Y9|S3):" /* starting string (any of the 'W4:' or 'T6:' or 'C7:' ) */;             //Updating W4
                    regExString += "([01][0-9][0-9]|2[0-4][0-9]|25[0-5])$" /* 000 to 255 */;
                    Regex regEx2 = new Regex(regExString);
                    if (!regEx2.IsMatch(address))
                        result = false;
                    else
                        result = true;
                    break;
                default:
                    result = true;
                    break;
            }
            if (outputType != "Memory Address Variable")
            {
                if (Validate == null)
                    result = false;
                else
                    result = true;
            }
            return result;

        }

        /// <summary>
        /// Function to check if the value entered by user is valid Real Word Address
        /// </summary>
        /// <param name="address"></param>Value entered by the user
        /// <returns></returns>Is the value entered by the user valid Real Word Address
        public static bool IsValidRealWordAddress(this string address)
        {
            if (address == "No available address found" || string.IsNullOrEmpty(address) || !address.Contains(":")) return false;
            // Let's setup regular expression for validating real address. Acceptable format e.g. P5:000 to P5:255
            string regExString = "^(P5|S3|I1|Q0)";
            //regExString += GetRegString(address);
            string digitPart = address.Split(':')[1].ToString();
            if (!Basevalidated(digitPart))
                return false;
            Regex regEx2 = new Regex(regExString);
            if (!regEx2.IsMatch(address.Split(':')[0]))
            {
                return false;
            }
            else
            {
                if (XMPS.Instance.MemoryAllocation.Where(m => m.Initial == address.Split(':')[0] && m.Limit > Convert.ToInt32(digitPart)).Count() > 0)
                {
                    if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && T.Label != "Real").Count() > 0)
                    {
                        if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address).Count() > 0 && (address.StartsWith("Q") || address.StartsWith("I")))
                        {
                            return true;
                        }
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return false;
            }
        }
        private static int[] GetDigits(int number)
        {
            return number.ToString().Select(d => int.Parse(d.ToString())).ToArray();
        }
        private static string GetRegString(string address)
        {
            int limit = Convert.ToInt32(GetMaxLength(address)); // Replace with your dynamic limit
            int addressValue = Convert.ToInt32(address.Replace(".", "@").Split(':')[1].Split('@')[0]);
            int[] limitDigits = GetDigits(limit);
            int[] addressDigits = GetDigits(addressValue);

            int maxLength = limitDigits.Length;
            int[] allowedDigits = new int[maxLength];
            bool earlyeq = false;
            for (int i = 0; i < maxLength; i++)
            {
                if (limitDigits.Length == addressDigits.Length && limitDigits[0] == addressDigits[0] && !earlyeq)
                {
                    allowedDigits[i] = limitDigits[i];
                }
                else
                {
                    allowedDigits[i] = i == 0 ? limitDigits[i] - 1 : (i == maxLength - 1 && limitDigits.Length == addressDigits.Length && !earlyeq) ? limitDigits[i] - 1 : 9;
                    earlyeq = true;
                }
            }
            if (!earlyeq)
                allowedDigits[maxLength - 1]--; // Decrease the last digit by 1 to set the upper bound
            // Build the regex pattern
            string regexPattern = "";
            for (int i = 0; i < maxLength; i++)
            {
                regexPattern += (i == 0 && maxLength > 3) ? $"[0-{allowedDigits[i]}]?" : $"[0-{allowedDigits[i]}]";
            }
            regexPattern += "$";

            return regexPattern;
        }

        public static bool IsValidDINTWordAddress(this string address)
        {
            if (string.IsNullOrEmpty(address) || !address.Contains(':'))
                return false;
            // Let's setup regular expression for validating Word address. Acceptable format e.g. Q0:000 to Q0:255
            string regExString =
                "^(W4)" /* starting string */ ;
            //   "((0[0-9][0-9]|[1-9][0-9][0-9]|100[0-9]|101[0-9]|102[0-3]))$";

            Regex regEx2 = new Regex(regExString);
            string digitPart = address.Split(':')[1].ToString();
            if (!Basevalidated(digitPart))
                return false;
            if (!regEx2.IsMatch(address.Split(':')[0]))
            {
                return false;
            }
            else
            {
                if (XMPS.Instance.MemoryAllocation.Where(m => m.Initial == address.Split(':')[0] && Convert.ToInt32(digitPart) >= 0 && m.Limit > Convert.ToInt32(digitPart)).Count() > 0)
                {
                    if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && T.Label != "DINT").Count() > 0)
                        return false;
                    else
                        return true;
                }
                else
                    return false;
            }
        }
        public static bool IsValidByteAddress(this string address, string datatype)
        {
            string regExString = "^(W4|S3):"; /* starting string */                                      //S3 is real address
            regExString += "((0[0-9][0-9]|[1-9][0-9][0-9]|100[0-9]|101[0-9]|102[0-3]))$";   //
            Regex regEx2 = new Regex(regExString);
            string datatype1 = datatype == "Double Word" ? "UDINT" : datatype == "UDINT" ? "Double Word" : "";
            if (!regEx2.IsMatch(address))
            {
                return false;
            }
            else
            {
                if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && T.Label != datatype && T.Label != datatype1).Count() > 0)
                    return false;
                else
                    return true;
            }
        }
        public static bool IsValidUDINTWordAddress(this string address)
        {
            // Let's setup regular expression for validating Word address. Acceptable format e.g. Q0:000 to Q0:255
            string regExString =
                "^(W4)" /* starting string */ ;
            //   "((0[0-9][0-9]|[1-9][0-9][0-9]|100[0-9]|101[0-9]|102[0-3]))$";

            Regex regEx2 = new Regex(regExString);
            string digitPart = address.Split(':')[1].ToString();
            if (!Basevalidated(digitPart))
                return false;
            if (!regEx2.IsMatch(address.Split(':')[0]))
            {
                return false;
            }
            else
            {
                if (XMPS.Instance.MemoryAllocation.Where(m => m.Initial == address.Split(':')[0] && Convert.ToInt32(digitPart) >= 0 && m.Limit > Convert.ToInt32(digitPart)).Count() > 0)
                {

                    if (XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == address && T.Label != "UDINT" && T.Label != "Double Word").Count() > 0)
                        return false;
                    else
                        return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Function to pad Ip Addresses with .000 Formatting
        /// </summary>
        /// <param name="IPAddress"></param> IP Address Entered by the user
        /// <returns></returns>Formatted IP Address
        public static string FormatIPAddress(string IPAddress)
        {
            //Padding 000 in IP address field
            string IPAdd = IPAddress;
            //Split the string with considerring . as delimitter and store it in an string array
            string[] str = IPAdd.Split('.');
            //use for loop to check each part of array and pad it with 000
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == "")
                {
                    str[i] = "000";
                }
                else
                {
                    str[i] = int.Parse(str[i]).ToString("000");
                }
            }
            //Join the string with considerring . as delimitter
            IPAdd = string.Join(".", str);
            //return joined string
            return IPAdd;
        }

        public static void checkvalue(object sender)
        {
            string enteredtext = ((NumericUpDown)sender).Text;
            decimal MinVal = ((NumericUpDown)sender).Minimum;
            decimal MaxVal = ((NumericUpDown)sender).Maximum;
            if (enteredtext == "")
            {
                ((NumericUpDown)sender).Text = MinVal.ToString();
            }
            else if (MinVal > Convert.ToInt32(enteredtext))
            {
                ((NumericUpDown)sender).Text = MinVal.ToString();
            }
            else if (MaxVal < Convert.ToInt32(enteredtext))
            {
                ((NumericUpDown)sender).Text = MaxVal.ToString();
            }
        }

        public static List<string> FillTagOperandsForModbus(string DataType)
        {
            XMPS xm = XMPS.Instance;
            List<string> result = new List<string>();
            List<string> TagList = new List<string>();
            if (DataType == "Bool")
            {
                if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.Contains(".") || d.LogicalAddress.StartsWith("F2") || d.Label == "Bool") && !d.LogicalAddress.StartsWith("'") && !(d.Label != null && (d.Label.EndsWith("_OR") || d.Label.EndsWith("_OL"))));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                }
                else
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.Contains(".") || d.LogicalAddress.StartsWith("F2") || d.Label == "Bool" && !(d.LogicalAddress.StartsWith("'")));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                }      
            }
            else if (DataType == "Word")
            {
                List<XMIOConfig> anlogInputs = xm.LoadedProject.Tags.Where(d => d.IoList == Core.Types.IOListType.OnBoardIO || d.IoList == Core.Types.IOListType.ExpansionIO || d.IoList == Core.Types.IOListType.RemoteIO).Where(t => t.Type == Core.Types.IOType.AnalogInput || t.Type == Core.Types.IOType.AnalogOutput || t.Type == Core.Types.IOType.UniversalInput || t.Type == Core.Types.IOType.UniversalOutput).Where(t => !t.Label.EndsWith("_OR") && !t.Label.EndsWith("_OL")).ToList();
                List<XMIOConfig> tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("P5") || d.Label == "Word").Where(d => !(d.LogicalAddress.StartsWith("'"))).Where(d => !d.Label.Contains("_OR") && !d.Label.Contains("_OL")).ToList();
                tag.AddRange(anlogInputs);
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);
            }
            else if (DataType == "Bool-Word")
            {
                List<XMIOConfig> tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.Contains(".") || d.LogicalAddress.StartsWith("F2") || d.Label == "Bool" && !(d.LogicalAddress.StartsWith("'"))).ToList();
                List<XMIOConfig> anlogInputs = xm.LoadedProject.Tags.Where(d => d.IoList == Core.Types.IOListType.OnBoardIO || d.IoList == Core.Types.IOListType.ExpansionIO || d.IoList == Core.Types.IOListType.RemoteIO).Where(t => t.Type == Core.Types.IOType.AnalogInput || t.Type == Core.Types.IOType.AnalogOutput || t.Type == Core.Types.IOType.UniversalInput || t.Type == Core.Types.IOType.UniversalOutput).ToList();
                List<XMIOConfig> wordAddress = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("S3")) && d.Label == "Word").Where(d => !(d.LogicalAddress.StartsWith("'"))).ToList();
                tag.AddRange(wordAddress);
                tag.AddRange(anlogInputs);
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList().Distinct();
                TagList.AddRange(taglist);
            }
            else
            {
                var tag = xm.LoadedProject.Tags.Where(d => !d.LogicalAddress.Contains(".") && !d.LogicalAddress.StartsWith("F2") && !(d.LogicalAddress.StartsWith("'")));
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);
            }
            result.Add("-Select Tag Name-");
            result.AddRange(TagList);
            return result;
        }

        public static List<string> FillTagOperandsForSchedule(string DataType)
        {
            XMPS xm = XMPS.Instance;
            List<string> result = new List<string>();
            List<string> TagList = new List<string>();
            if (DataType == "Bool")
            {
                if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.LogicalAddress.Contains(".") || d.LogicalAddress.StartsWith("F2") || d.Label == "Bool") && !d.LogicalAddress.StartsWith("'")
                    && !(d.Label.EndsWith("_OR") || d.Label.EndsWith("_OL")));
                    var taglist = tag.Select(t => t.Tag).Where(t => t != null).OrderBy(t => t).ToList();
                    TagList.AddRange(taglist);
                }
                else
                {
                    var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.Contains(".") || d.LogicalAddress.StartsWith("F2") || d.Label == "Bool" && !(d.LogicalAddress.StartsWith("'")));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                }               
            }
            else if (DataType == "Real")
            {
                var tag = xm.LoadedProject.Tags.Where(d => !d.LogicalAddress.Contains(".") && !d.Label.Contains("Word")
                && !d.Label.Contains("Bool") && !d.Label.Contains("Byte") && !d.Label.Contains("DINT") && !d.Label.Contains("String") && !d.Label.Contains("Int") || d.LogicalAddress.StartsWith("P5")
                || d.Label == "Real" && !(d.LogicalAddress.StartsWith("'")));
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);
            }
            result.Add("-Select Tag Name-");
            result.AddRange(TagList);
            return result;
        }
        public static List<string> FillOutputAdddressForUDFB(string DataType)
        {
            XMPS xm = XMPS.Instance;
            List<string> result = new List<string>();
            List<string> TagList = new List<string>();
            TagList.Add("-Select Tag Name-");
            if (DataType == "Bool")
            {
                if (XMPS.Instance.PlcModel.StartsWith("XBLD"))
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.IoList == Core.Types.IOListType.OnBoardIO || d.Label == "Bool") && (d.Model is null || d.Model.StartsWith("User") || d.Model.StartsWith("XM") || d.Model == "" && !(d.Label.EndsWith("_OR") || d.Label.EndsWith("_OL")))).Where(d => d.Type.ToString().Contains("Output"));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                }
                else
                {
                    var tag = xm.LoadedProject.Tags.Where(d => (d.IoList == Core.Types.IOListType.OnBoardIO || d.Label == "Bool") && (d.Model is null || d.Model.StartsWith("User") || d.Model.StartsWith("XM") || d.Model == "")).Where(d => d.Type.ToString().Contains("Output"));
                    var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                    TagList.AddRange(taglist);
                }
                
            }
            else if (DataType == "TCAddress")
            {
                var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("T") || d.LogicalAddress.StartsWith("C") || d.LogicalAddress.StartsWith("W") || d.LogicalAddress.StartsWith("P")).Where(d => d.Label == "Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);

            }
            else if (DataType == "Word")
            {
                var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3") && (!d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);

            }
            else if (DataType == "Double Word" || DataType == "UDINT")
            {
                var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Double Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);

            }
            else if (DataType == "Real")
            {
                var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("P") || d.LogicalAddress.StartsWith("S3") && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Real").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);
            }
            else if (DataType == "TON" || DataType == "TOFF" || DataType == "TP" || DataType == "CTU" || DataType == "CTD" || DataType == "RTON")
            {
                var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") || d.LogicalAddress.StartsWith("T6") || d.LogicalAddress.StartsWith("C7") || d.LogicalAddress.StartsWith("S3") && (!d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Word").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);

            }
            else if (DataType == "Byte")
            {
                var tag = xm.LoadedProject.Tags.Where(d => d.LogicalAddress.StartsWith("W4") && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Label == "Byte").Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);
            }
            else
            {
                var tag = xm.LoadedProject.Tags.Where(d => d.Label.Equals(DataType) && !(d.LogicalAddress.StartsWith("'"))).Where(d => d.Model is null || d.Model.StartsWith("User") || XMPS.Instance.PlcModels.Contains(d.Model) || d.Model == "");
                var taglist = tag.Select(t => t.Tag).OrderBy(t => t).Where(t => t != null).ToList();
                TagList.AddRange(taglist);
            }
            return TagList;
        }

        public static bool ValidateUDFBOperad(string operandValue, string operandDataType, string operandType)
        {
            bool isValidOperandValue = true;
            if (operandValue.Contains(":"))
            {
                if (operandValue.Contains("A5:999"))
                    return true;
                switch (operandDataType)
                {
                    case "Bool":
                        isValidOperandValue = operandValue.IsValidBitAddress();
                        break;
                    case "Real":
                        isValidOperandValue = operandValue.IsValidRealWordAddress();
                        break;
                    case "DINT":
                        isValidOperandValue = operandValue.IsValidDINTWordAddress();
                        break;
                    case "UDINT":
                        isValidOperandValue = operandValue.IsValidUDINTWordAddress();
                        break;
                    case "Word":
                        isValidOperandValue = operandValue.IsValidWordAddress();
                        break;
                    case "Byte":
                    case "Double Word":
                    case "Int":
                        isValidOperandValue = operandValue.IsValidByteAddress(operandDataType);
                        break;
                }
            }
            else
            {
                return ValidateNumericInputOperand(operandValue, operandDataType, out _, null);
            }
            return isValidOperandValue;
        }
        public static bool ValidateNumericInputOperand(string number, string dataType, out string error, string Address)
        {
            var tag = XMPS.Instance.LoadedProject.BacNetIP?.MultistateValues?.Where(e => e.LogicalAddress == Address && e.IsEnable == true).Select(e => e.ObjectName).FirstOrDefault();
            int initialValue = XMPS.Instance.LoadedProject.BacNetIP?.MultistateValues?.Where(a => a.ObjectName != null && a.ObjectName == tag).Select(a => (int?)a.NumberOfStates).FirstOrDefault() ?? 65536;
            var tag1 = XMPS.Instance.LoadedProject?.Tags.Where(e => e.LogicalAddress == Address).Select(e => e.Tag).FirstOrDefault();
            var IsChecked = XMPS.Instance.LoadedProject?.BacNetIP?.AnalogIOValues.Where(e => e.LogicalAddress == Address && e.IsEnable == true).Select(e => e.ObjectName).FirstOrDefault();
            var MaxValue = XMPS.Instance.LoadedProject.BacNetIP?.AnalogIOValues?.Where(a => a.ObjectName != null && a.ObjectName == tag1).Select(a => (int?)a.MaxPresValue).FirstOrDefault() ?? 4095;
            var MinValue = XMPS.Instance.LoadedProject.BacNetIP?.AnalogIOValues?.Where(a => a.ObjectName != null && a.ObjectName == tag1).Select(a => (int?)a.MinPresValue).FirstOrDefault() ?? 0;
            var tagInfo = XMPS.Instance.LoadedProject?.Tags.Where(e => e.LogicalAddress == Address).Select(e => new { e.Mode, e.Type }).FirstOrDefault();
            string tagMode = tagInfo?.Mode;
            string tagIOType = tagInfo?.Type.ToString();
            if (XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD"))
            {
                if (!string.IsNullOrEmpty(tagMode) && tagMode.Equals("Digital", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(tagIOType) && (tagIOType.StartsWith("UniversalInput") || tagIOType.StartsWith("UniversalOutput")))
                {
                    if (number != "0" && number != "1")
                    {
                        error = "Invalid input value. Value does not match for Boolean data type";
                        return false;
                    }
                    else
                    {
                        error = string.Empty;
                        return true;
                    }
                }
            }
            switch (dataType)
            {
                case "Bool":
                    if (!number.Equals("1") && !number.Equals("0"))
                    {
                        error = "Invalid input value. Value does not match for Boolean data type";
                        return false;
                    }
                    break;
                case "Byte":
                    if (number.StartsWith("-") || !byte.TryParse(number, out _))
                    {
                        error = "Invalid input value. Value does not match for Byte data type";
                        return false;
                    }
                    break;
                case "Word":
                    if (tag != null && XMPS.Instance.PlcModel.StartsWith("XBLD"))
                    {
                        if (!int.TryParse(number, out int parsedValue) || parsedValue < 1 || parsedValue > initialValue)
                        {
                            error = $"Invalid input value. Value must be between 1 and {initialValue}.";
                            return false;
                        }
                    }
                    else if (number.StartsWith("-") || !ushort.TryParse(number, out _) || !ushort.TryParse(number, out ushort parsedValue) || parsedValue > 65536)
                    {
                        error = "Invalid input value. Value does not match for Word data type";
                        return false;
                    }
                    break;
                case "Double Word":
                    if (number.StartsWith("-") || !uint.TryParse(number, out _))
                    {
                        error = "Invalid input value. Value does not match for Double Word data type";
                        return false;
                    }
                    break;
                case "Int":
                    if (int.TryParse(number, out int parsedIntNumber))
                    {
                        if (parsedIntNumber < -32768 || parsedIntNumber > 32767)
                        {
                            error = "Invalid input value. Value does not match for Integer data type";
                            return false;
                        }
                    }
                    else
                    {
                        error = "Invalid input value. Value does not match for Integer data type";
                        return false;
                    }
                    break;
                case "Real":
                    if (Double.TryParse(number, out double parsedNumber))
                    {
                        if (tag1 != null && XMPS.Instance.PlcModel.StartsWith("XBLD") && IsChecked != null)
                        {
                            if (parsedNumber > MaxValue || parsedNumber < MinValue)
                            {
                                error = $"Invalid input value. This object is selected in BACnet objects and the value must be between {MinValue} to {MaxValue}";
                                return false;
                            }
                        }
                        else if (parsedNumber < -2147483648 || parsedNumber > 2147483647)
                        {
                            error = "Invalid input value. Value does not match for Real data type";
                            return false;
                        }
                    }
                    else
                    {
                        error = "Invalid input value. Value does not match for Real data type";
                        return false;
                    }
                    break;
                case "DINT":
                    long resultDINT;
                    if (long.TryParse(number, out resultDINT))
                    {
                        if (!(resultDINT >= -2147483648 && resultDINT <= 2147483647))
                        {
                            error = "Invalid input value. Value does not match for DINT data type";
                            return false;
                        }
                    }
                    else
                    {
                        error = "Invalid input value. Value does not match for DINT data type or is not Numeric";
                        return false;
                    }
                    break;
                case "UDINT":
                    uint resultUdint;
                    if (uint.TryParse(number, out resultUdint))
                    {
                        if (!(resultUdint >= 0 && resultUdint <= 4294967295))
                        {
                            error = "Invalid input value. Value does not match for UDINT data type";
                            return false;
                        }
                    }
                    else
                    {
                        error = "Invalid input value. Value does not match for UDINT data type or is not Numeric";
                        return false;
                    }
                    break;
                // Timer data types
                case "TON":
                case "TOFF":
                case "TP":
                    break;
                // Counter data types
                case "CTU":
                case "CTD":
                    break;
            }
            error = string.Empty;
            return true;
        }

        public static string CheckInLogicalBlock(string checktag)
        {
            XMPS xm = XMPS.Instance;
            var BlockCount = xm.LoadedProject.Blocks;
            string presentBlock = string.Empty;
            for (int B = 0; B < BlockCount.Count; B++)
            {
                string windowName = BlockCount[B].Type.Equals("UDFB") ? "UDFLadderForm#" : "LadderForm#";
                if (xm.LoadedScreens.ContainsKey($"{windowName}{BlockCount[B].Name}"))
                {
                    LadderWindow _windowRef = windowName.Equals("LadderForm#") ?
                                            (LadderWindow)xm.LoadedScreens[$"LadderForm#{BlockCount[B].Name}"] : (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{BlockCount[B].Name}"];
                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();

                    for (int k = 0; k < LadderDrawing.LadderDesign.Active.Elements.Count(); k++)
                    {
                        for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[k].Elements.Count; j++)
                        {
                            LadderElement ld = LadderDrawing.LadderDesign.Active.Elements[k].Elements[j];
                            if (ld.customDrawing.GetType().Name.Equals("Contact"))
                            {
                                if (ld.Attributes["LogicalAddress"].ToString().Equals(checktag))
                                {
                                    return BlockCount[B].Name;
                                }
                            }
                            else if (ld.customDrawing.GetType().Name.Equals("DummyParallelParent") || ld.customDrawing.GetType().Name.Equals("Coil"))
                            {
                                int countInparallel = 0;
                                if (ld.Attributes["LogicalAddress"].ToString().Equals(checktag))
                                {
                                    return BlockCount[B].Name;
                                }
                                else if (ld.Elements.Count > 0)
                                {
                                    CheckInChildElements(ld.Elements, checktag, ref countInparallel);
                                    presentBlock = countInparallel > 0 ? presentBlock + BlockCount[B].Name : "";
                                    if (presentBlock.Length > 0)
                                        return presentBlock;
                                }
                            }
                            else if (ld.customDrawing.GetType().Name.Equals("FunctionBlock"))
                            {
                                List<string> functionblkAdd = ld.Attributes.Where(t => t.Name.StartsWith("input") || t.Name.StartsWith("output")).ToList().Select(t => t.Value.ToString().Replace("~", "")).ToList();
                                if (ld.Attributes["function_name"].Equals("Pack") && ld.Attributes["input1"].ToString().Length > 3)
                                {
                                    string[] parts = ld.Attributes["input1"].ToString().Split(':');
                                    int lastTagAdd = int.Parse(parts[1]) + 15;
                                    string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                                    functionblkAdd.AddRange(XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")
                                                                   && Convert.ToInt32(T.LogicalAddress.Split(':')[1]) >= Convert.ToInt32(ld.Attributes["input1"].ToString().Split(':')[1])
                                                                   && Convert.ToInt32(T.LogicalAddress.Split(':')[1]) <= Convert.ToInt32(endAdd.Split(':')[1])).Select(t => t.LogicalAddress).ToList());
                                }
                                else if (ld.Attributes["function_name"].Equals("UnPack") && ld.Attributes["output1"].ToString().Length > 3)
                                {
                                    string[] parts = ld.Attributes["output1"].ToString().Split(':');
                                    int lastTagAdd = int.Parse(parts[1]) + 15;
                                    string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                                    functionblkAdd.AddRange(XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")
                                                                   && Convert.ToInt32(T.LogicalAddress.Split(':')[1]) >= Convert.ToInt32(ld.Attributes["output1"].ToString().Split(':')[1])
                                                                   && Convert.ToInt32(T.LogicalAddress.Split(':')[1]) <= Convert.ToInt32(endAdd.Split(':')[1])).Select(t => t.LogicalAddress).ToList());
                                }

                                if (functionblkAdd.Contains(checktag))
                                    return BlockCount[B].Name;
                            }
                        }
                    }
                }
            }
            return presentBlock;
        }
        private static void CheckInChildElements(LadderElements elements, string address, ref int elementCount)
        {
            foreach (var element in elements)
            {
                if (element.customDrawing.GetType().Name.Equals("Contact") || element.customDrawing.GetType().Name.Equals("CoilParallel"))
                {
                    if (element.Attributes["LogicalAddress"].ToString().Equals(address))
                    {
                        elementCount++;
                    }
                }
                if (element.Elements.Any())
                {
                    CheckInChildElements(element.Elements, address, ref elementCount);
                }
            }
        }
        public static MODBUSRTUMaster_Slave CheckInModbusRTUMaster(string address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            var modbusRTUNode = XMPS.Instance?.LoadedProject?.Devices?.FirstOrDefault(d => d.GetType().Name == "MODBUSRTUMaster") as MODBUSRTUMaster;
            if (modbusRTUNode == null)
            {
                return null;
            }
            return modbusRTUNode.Slaves?.FirstOrDefault(d => d.Variable == address);
        }
        public static MODBUSRTUSlaves_Slave CheckInModbusRTUSlavesSlave(string address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            var modBUSRTUMaster = XMPS.Instance.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "MODBUSRTUSlaves") as MODBUSRTUSlaves;
            if (modBUSRTUMaster == null)
            {
                return null;
            }
            return modBUSRTUMaster.Slaves?.FirstOrDefault(d => d.Variable == address);
        }
        public static MODBUSTCPClient_Slave CheckInModbusTCPClient(string address)
        {
            var modbusTCPClientNode = (MODBUSTCPClient)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
            return modbusTCPClientNode.Slaves.Where(d => d.Variable == address).FirstOrDefault();
        }
        public static MODBUSTCPServer_Request CheckInModbusServerRequest(string address)
        {
            string tagname = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress.Equals(address)).Select(T => T.Tag).FirstOrDefault();
            var modbusTCPServerNode = (MODBUSTCPServer)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
            return modbusTCPServerNode.Requests.FirstOrDefault(d => d.Variable == address || d.Variable == tagname);

        }

        public static bool CheckInPublishTopics(string address)
        {
            var publist = XMPS.Instance.LoadedProject.Devices.Where(p => p.GetType().Name == "Publish").Cast<Publish>().ToList();
            //return publist.SelectMany(T => T.PubRequest).Where(a => a.Tag.Contains(":") ? a.Tag.Equals(address) :
            //              a.Tag.Equals(((address.StartsWith("Q") || address.StartsWith("I")) && !address.Contains(".")) ?
            //                XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(address + ".00")).Tag
            //              : XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(address)).Tag)).Any();

            return publist.SelectMany(T => T.PubRequest).Where(a =>
            {
                if (string.IsNullOrEmpty(a?.Tag)) return false;

                if (a.Tag.Contains(":"))
                {
                    return a.Tag.Equals(address);
                }

                var searchAddress = ((address.StartsWith("Q") || address.StartsWith("I")) && !address.Contains("."))
                    ? address + ".00"
                    : address;

                var targetTag = XMPS.Instance?.LoadedProject?.Tags?
                    .FirstOrDefault(t => t.LogicalAddress.Equals(searchAddress))?.Tag;

                return targetTag != null && a.Tag.Equals(targetTag);
            }).Any();
        }

        public static bool CheckInSubscribeTopics(string address)
        {
            var sublist = XMPS.Instance.LoadedProject.Devices.Where(p => p.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
            return sublist.SelectMany(T => T.SubRequest).Where(a => a.Tag.Contains(":") ? a.Tag.Equals(address) :
                          a.Tag.Equals(((address.StartsWith("Q") || address.StartsWith("I")) && !address.Contains(".")) ?
                            XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(address + ".00"))?.Tag ?? ""
                          : XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(address))?.Tag ?? "")).Any();
        }

        public static Schedule CheckInScheduleObject(string address)
        {
            return XMPS.Instance.LoadedProject.BacNetIP.Schedules.FirstOrDefault(t => t.LogicalAddress == address);
        }
        public static List<HSIO> CheckInHSIOBlocks(string address)
        {
            return XMPS.Instance.LoadedProject.HsioBlock.Where(t => t.HSIOBlocks.Any(a => a.Value.Equals(address))).ToList();
        }

        public static List<LadderElement> GetRTONTimerRungs(string logicalAddress)
        {
            List<LadderElement> rtomTimers = new List<LadderElement>();
            XMPS xm = XMPS.Instance;
            var BlockCount = xm.LoadedProject.Blocks;
            string presentBlock = string.Empty;
            for (int B = 0; B < BlockCount.Count; B++)
            {
                string windowName = BlockCount[B].Type.Equals("UDFB") ? "UDFLadderForm#" : "LadderForm#";
                if (xm.LoadedScreens.ContainsKey($"{windowName}{BlockCount[B].Name}"))
                {
                    LadderWindow _windowRef = windowName.Equals("LadderForm#") ?
                                            (LadderWindow)xm.LoadedScreens[$"LadderForm#{BlockCount[B].Name}"] : (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{BlockCount[B].Name}"];
                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();

                    for (int k = 0; k < LadderDrawing.LadderDesign.Active.Elements.Count(); k++)
                    {
                        for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[k].Elements.Count; j++)
                        {
                            LadderElement ld = LadderDrawing.LadderDesign.Active.Elements[k].Elements[j];
                            if (ld.customDrawing.GetType().Name.Equals("FunctionBlock") && ld.Attributes["function_name"].ToString().Contains("RTON"))
                            {
                                string input3 = ld.Attributes["input3"].ToString();
                                string Output2 = ld.Attributes["output2"].ToString();
                                if (input3.Equals(logicalAddress) || Output2.Equals(logicalAddress))
                                    rtomTimers.Add(ld);
                            }
                        }
                    }
                }
            }
            return rtomTimers;
        }

        public static List<string> ValidateModbusRequestMQTTSchedule()
        {
            List<string> errorsInRequest = new List<string>();

            ////////////////////////MODBURTUMASTER/////////////////////////////////////

            var modbusRTUMaterSlaves = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSRTUMaster>();
            var modbusRTUMaterSlavesrequest = modbusRTUMaterSlaves.SelectMany(master => master.Slaves).OrderBy(a => int.Parse(a.Name.Substring("MODBUSRTUMasterSlave".Length))).ToList();
            //1.Tag not found
            errorsInRequest.AddRange(modbusRTUMaterSlavesrequest.Where(slave => string.IsNullOrEmpty(slave.Variable) ||
                             (!XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(slave.Variable)) &&
                             ((slave.Variable.StartsWith("Q") || slave.Variable.StartsWith("I")) &&
                             !slave.Variable.Contains(".") ? !XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress == slave.Variable + ".00") :
                             true)))
                             .Select(slave => $"MODBUS RTU Master: Tag {slave.Variable} is not found MODBUSRTUMaster_Slave Address of slave is {slave.Address} " +
                             $"at row no-{modbusRTUMaterSlavesrequest.IndexOf(slave)}"));
            // 2.Duplicate Variable
            errorsInRequest.AddRange(modbusRTUMaterSlavesrequest
                .Where(slave => !string.IsNullOrEmpty(slave.Variable))
                .Where(slave =>
                {
                    var matchingFunctionCode = ModbusFunctionCode.List.FirstOrDefault(fc => fc.Text.Trim() == slave.FunctionCode.Trim());
                    if (matchingFunctionCode == null) return false;
                    bool isWriteFunctionCode = matchingFunctionCode.ID == 0x05 || matchingFunctionCode.ID == 0x06 || matchingFunctionCode.ID == 0x0F || matchingFunctionCode.ID == 0x10;
                    return isWriteFunctionCode
                        ? modbusRTUMaterSlavesrequest.Any(s => s.Name != slave.Name && s.Variable == slave.Variable && s.FunctionCode == slave.FunctionCode && s.DeviceId == slave.DeviceId)
                        : modbusRTUMaterSlavesrequest.Any(s => s.Name != slave.Name && s.Variable == slave.Variable && s.FunctionCode == slave.FunctionCode);
                })
                .Select(slave =>
                {
                    var matchingFunctionCode = ModbusFunctionCode.List.FirstOrDefault(fc => fc.Text.Trim() == slave.FunctionCode.Trim());
                    bool isWriteFunctionCode = matchingFunctionCode.ID == 0x05 || matchingFunctionCode.ID == 0x06 || matchingFunctionCode.ID == 0x0F || matchingFunctionCode.ID == 0x10;
                    return isWriteFunctionCode
                        ? $"MODBUS RTU Master: Duplicate Variable '{slave.Variable}' with same function code and device ID found in MODBUSRTUMaster_Slave at row no-{modbusRTUMaterSlavesrequest.IndexOf(slave)}"
                        : $"MODBUS RTU Master: Duplicate Variable '{slave.Variable}' with same function code found in MODBUSRTUMaster_Slave at row no-{modbusRTUMaterSlavesrequest.IndexOf(slave)}";
                })
                .Distinct());
            // 3.Duplicate Address
            errorsInRequest.AddRange(modbusRTUMaterSlavesrequest
                .GroupBy(slave => new { slave.Address, slave.FunctionCode, slave.DeviceId })
                .Where(g => g.Count() > 1)
                .Select(g => $"MODBUS RTU Master : Duplicate Address '{g.Key}' found in MODBUSRTUMaster_Slave" +
                $"at row no-{modbusRTUMaterSlavesrequest.IndexOf(modbusRTUMaterSlavesrequest.FirstOrDefault(t => t.Address.Equals(g.Key.Address) && t.FunctionCode.Equals(g.Key.FunctionCode)))}"));

            // 4. Address Overlap Validation           
            var addressOverlapErrors = new List<string>();
            foreach (var slave in modbusRTUMaterSlavesrequest)
            {
                var overlappingSlaves = modbusRTUMaterSlavesrequest
                    .Where(s => s.Name != slave.Name && s.DeviceId == slave.DeviceId && s.FunctionCode == slave.FunctionCode)
                    .Where(s =>
                    {
                        int currentAddress = slave.Address;
                        int currentLength = slave.Length;
                        int existingAddress = s.Address;
                        int existingLength = s.Length;

                        int rangeStart = existingAddress;
                        int rangeEnd = existingLength;
                        bool addressOverlap = currentAddress >= rangeStart && currentAddress < rangeEnd && currentAddress != existingAddress;

                        return addressOverlap;
                    })
                    .ToList();
                if (overlappingSlaves.Any())
                {
                    var firstOverlap = overlappingSlaves.First();
                    int rangeStart = firstOverlap.Address;
                    int rangeEnd = firstOverlap.Length;
                    addressOverlapErrors.Add($"MODBUS RTU Master: Address {slave.Address} overlaps with existing slave range {rangeStart}-{rangeEnd} in MODBUSRTUMaster_Slave " + $"at row no-{modbusRTUMaterSlavesrequest.IndexOf(slave)}");
                }
            }
            errorsInRequest.AddRange(addressOverlapErrors.Distinct());

            ///////////////////////////MODBUSRTUSlaves//////////////////////////////////////

            var modbusRTUSlaves = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSRTUSlaves>();
            var modbusRTUSlavesrequest = modbusRTUSlaves.SelectMany(master => master.Slaves).OrderBy(a => int.Parse(a.Name.Substring("MODBUSRTUSlavesSlave".Length))).ToList();

            // Tag Not Found
            errorsInRequest.AddRange(modbusRTUSlavesrequest.Where(slave => string.IsNullOrEmpty(slave.Variable) ||
                          (!XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(slave.Variable)) &&
                          ((slave.Variable.StartsWith("Q") || slave.Variable.StartsWith("I")) &&
                          !slave.Variable.Contains(".") ? !XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress == slave.Variable + ".00") :
                          true)))
                          .Select(slave => $"MODBUS RTU Slaves : Tag {slave.Variable} is not found MODBUSRTUSlaves Address of slave is {slave.Address} " +
                          $" at row no-{modbusRTUSlavesrequest.IndexOf(slave)}"));
            // 1.Duplicate Variable
            errorsInRequest.AddRange(modbusRTUSlavesrequest
                .Where(slave => !string.IsNullOrEmpty(slave.Variable))
                .GroupBy(slave => new { slave.Variable, slave.FunctionCode })
                .Where(g => g.Count() > 1)
                .Select(g => $"MODBUS RTU Slaves : Duplicate Variable '{g.Key.Variable}' found in MODBUSRTUSlaves" +
                $"at row no-{modbusRTUSlavesrequest.IndexOf(modbusRTUSlavesrequest.FirstOrDefault(t => t.Variable.Equals(g.Key.Variable) && t.FunctionCode.Equals(g.Key.FunctionCode)))}"));

            // 2.Duplicate Address
            errorsInRequest.AddRange(modbusRTUSlavesrequest
                .GroupBy(slave => new { slave.Address, slave.FunctionCode })
                .Where(g => g.Count() > 1)
                .Select(g => $"MODBUS RTU Slaves : Duplicate Address '{g.Key}' found in MODBUSRTUSlaves" +
                $"at row no-{modbusRTUSlavesrequest.IndexOf(modbusRTUSlavesrequest.FirstOrDefault(t => t.Address.Equals(g.Key.Address) && t.FunctionCode.Equals(g.Key.FunctionCode)))}"));

            //////////////////////////MODBUSTCPClient//////////////////////////////////

            var modbusClients = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSTCPClient>();
            var clientRequests = modbusClients.SelectMany(master => master.Slaves).OrderBy(a => int.Parse(a.Name.Substring("MODBUSTCPClientSlave".Length))).ToList();

            // Tag Not Found
            errorsInRequest.AddRange(clientRequests.Where(slave => string.IsNullOrEmpty(slave.Variable) ||
                               (!XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(slave.Variable)) &&
                               ((slave.Variable.StartsWith("Q") || slave.Variable.StartsWith("I")) &&
                               !slave.Variable.Contains(".") ? !XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress == slave.Variable + ".00") : true)))
                               .Select(slave => $"MODBUS TCP Client : Tag {slave.Variable} is not found in MODBUSTCPClient Address of slave is {slave.Address}" +
                               $" at row no-{clientRequests.IndexOf(slave)}"));

            // 1.Duplicate Variable
            errorsInRequest.AddRange(clientRequests
                .Where(slave => !string.IsNullOrEmpty(slave.Variable))
                .GroupBy(slave => new { slave.Variable, slave.Functioncode })
                .Where(g => g.Count() > 1)
                .Select(g => $"MODBUS TCP Client : Duplicate Variable '{g.Key.Variable}' found in MODBUSTCPClient" +
                $"at row no-{clientRequests.IndexOf(clientRequests.FirstOrDefault(t => t.Variable.Equals(g.Key.Variable) && t.Functioncode.Equals(g.Key.Functioncode)))}"));

            // 2.Duplicate Address
            errorsInRequest.AddRange(clientRequests
                .GroupBy(slave => new { slave.Address, slave.Functioncode, slave.ServerIPAddress })
                .Where(g => g.Count() > 1)
                .Select(g => $"MODBUS TCP Client : Duplicate Address '{g.Key}' found in MODBUSTCPClient" +
                $"at row no-{clientRequests.IndexOf(clientRequests.FirstOrDefault(t => t.Address.Equals(g.Key.Address) && t.Functioncode.Equals(g.Key.Functioncode)))}"));

            // 3. Address Overlap Validation           
            var tcpAddressOverlapErrors = new List<string>();
            foreach (var slave in clientRequests)
            {
                var overlappingSlaves = clientRequests
                    .Where(s => s.Name != slave.Name && s.DeviceId == slave.DeviceId && s.Functioncode == slave.Functioncode && s.ServerIPAddress.ToString() == slave.ServerIPAddress.ToString())
                    .Where(s =>
                    {
                        int currentAddress = slave.Address;
                        int currentLength = slave.Length;
                        int existingAddress = s.Address;
                        int existingLength = s.Length;

                        int rangeStart = existingAddress;
                        int rangeEnd = existingLength; 
                        bool addressOverlap = currentAddress >= rangeStart && currentAddress < rangeEnd && currentAddress != existingAddress;

                        return addressOverlap;
                    })
                    .ToList();

                if (overlappingSlaves.Any())
                {
                    var firstOverlap = overlappingSlaves.First();
                    int rangeStart = firstOverlap.Address;
                    int rangeEnd = firstOverlap.Length; 
                    tcpAddressOverlapErrors.Add($"MODBUS TCP Client: Address {slave.Address} overlaps with existing slave range {rangeStart}-{rangeEnd} in MODBUSTCPClient " + $"at row no-{clientRequests.IndexOf(slave)}");
                }
            }
            errorsInRequest.AddRange(tcpAddressOverlapErrors.Distinct());
            //////////////////////////////////MODBUSTCPSERVER////////////////////////////////

            var modbusServers = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSTCPServer>();

            var requests = modbusServers.SelectMany(master => master.Requests).OrderBy(x => int.Parse(x.Name.Substring("MODBUSTCPServerRequest".Length))).ToList();

            // 1. Tag Not Found Validation
            errorsInRequest.AddRange(requests.Where(slave => string.IsNullOrEmpty(slave.Variable) ||
                               (!XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(slave.Variable)) &&
                               ((slave.Variable.StartsWith("Q") || slave.Variable.StartsWith("I")) &&
                               !slave.Variable.Contains(".") ? !XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress == slave.Variable + ".00") : true)))
                               .Select(slave => $"MODBUS TCP Server : Tag {slave.Variable} is not found, MODBUSTCPServer Address of slave is {slave.Address}" +
                               $" at row no-{requests.IndexOf(requests.FirstOrDefault(t => t.Name.Equals(slave.Name)))}"));

            // 2. Duplicate Variable Validation
            errorsInRequest.AddRange(requests
                .Where(slave => !string.IsNullOrEmpty(slave.Variable))
                .GroupBy(slave => new { slave.Variable, slave.FunctionCode })
                .Where(g => g.Count() > 1)
                .Select(g => $"MODBUS TCP Server : Duplicate Variable '{g.Key.Variable}' found in MODBUSTCPServer with FunctionCode '{g.Key.FunctionCode}'" +
                $"at row no-{requests.IndexOf(requests.FirstOrDefault(t => t.Variable.Equals(g.Key.Variable) && t.FunctionCode.Equals(g.Key.FunctionCode)))}"));


            // 3. Duplicate Address Validation
            errorsInRequest.AddRange(requests
                .GroupBy(slave => new { slave.Address, slave.FunctionCode })
                .Where(g => g.Count() > 1)
                .Select(g => $"MODBUS TCP Server : Duplicate Address '{g.Key}' found in MODBUSTCPServer" +
                $"at row no-{requests.IndexOf(requests.FirstOrDefault(t => t.Address.Equals(g.Key.Address) && t.FunctionCode.Equals(g.Key.FunctionCode)))}"));

            //validation Publish blocks
            int rowCounter = 0;
            foreach (var publish in XMPS.Instance.LoadedProject.Devices.OfType<Publish>().OrderBy(d => d.keyvalue))
            {
                bool hasInvalidRequest = publish.PubRequest.Any(pr =>
                    string.IsNullOrEmpty(pr.Tag) ||
                    (pr.Tag.Contains(":")
                        ? (!XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(pr.Tag)) &&
                           ((pr.Tag.StartsWith("Q") || pr.Tag.StartsWith("I")) &&
                            !pr.Tag.Contains(".") ? !XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress == pr.Tag + ".00") : true))
                        : !XMPS.Instance.LoadedProject.Tags.Any(t => t.Tag.Equals(pr.Tag)))
                );

                if (hasInvalidRequest)
                {
                    errorsInRequest.Add($"MQTT Publish : One or more invalid tags found in topic '{publish.topic}' at row no-{rowCounter}");
                }
                rowCounter += 2 + publish.PubRequest.Count;
            }

            // Check if every topic has unique request names inside each Publish block separately
            rowCounter = 0;
            foreach (var publish in XMPS.Instance.LoadedProject.Devices.OfType<Publish>().OrderBy(d => d.keyvalue))
            {
                var duplicateRequests = publish.PubRequest
                    .GroupBy(pr => pr.req)
                    .Where(g => g.Count() > 1 && !string.IsNullOrEmpty(g.Key));

                foreach (var dup in duplicateRequests)
                {
                    errorsInRequest.Add($"MQTT Publish : Duplicate request name '{dup.Key}' found in Publish request of Topic '{publish.topic}' at row no-{rowCounter}");
                }

                rowCounter += 2 + publish.PubRequest.Count;
            }

            //Validating Subscribe requests.
            rowCounter = 0;
            foreach (var subscribe in XMPS.Instance.LoadedProject.Devices.OfType<Subscribe>().OrderBy(d => d.key))
            {
                bool hasError = false;

                foreach (var request in subscribe.SubRequest)
                {
                    if (string.IsNullOrEmpty(request.Tag) ||
                        (request.Tag.Contains(":")
                            ? (!XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(request.Tag)) &&
                                ((request.Tag.StartsWith("Q") || request.Tag.StartsWith("I")) &&
                                !request.Tag.Contains(".") ? !XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress == request.Tag + ".00") : true))
                            : !XMPS.Instance.LoadedProject.Tags.Any(t => t.Tag.Equals(request.Tag))))
                    {
                        hasError = true;
                        break;
                    }
                }

                if (hasError)
                {
                    errorsInRequest.Add(
                        $"MQTT Subscribe : One or more requests under topic '{subscribe.topic}' have unassigned or invalid tags at row no-{rowCounter}");
                }

                rowCounter += 2 + subscribe.SubRequest.Count;
            }

            // Check if every topic has unique request names inside each Subscribe block separately
            rowCounter = 0;
            foreach (var subscribe in XMPS.Instance.LoadedProject.Devices.OfType<Subscribe>().OrderBy(d => d.key))
            {
                var duplicateGroups = subscribe.SubRequest
                    .GroupBy(pr => pr.req)
                    .Where(g => g.Count() > 1 && !string.IsNullOrEmpty(g.Key));

                foreach (var g in duplicateGroups)
                {
                    errorsInRequest.Add(
                        $"MQTT Subscribe : Duplicate request name '{g.Key}' found in Subscribe request of Topic '{subscribe.topic}' at row no-{rowCounter}");
                }
                rowCounter += 2 + subscribe.SubRequest.Count;
            }

            ///////////////////Apply the check of 24 char topic name for MQTT requests///////////////////
            //Validating topic name length of all Subscribe topic
            rowCounter = 0;
            foreach (var subscribe in XMPS.Instance.LoadedProject.Devices.OfType<Subscribe>().OrderBy(d => d.key))
            {
                if (subscribe.topic.ToString().Length > 16)
                {
                    errorsInRequest.Add(
                        $"MQTT Subscribe : Topic name is greater than 16 chars  Subscribe request name : {subscribe.topic} at row no-{rowCounter}");
                }
                rowCounter += 2 + subscribe.SubRequest.Count;
            }

            //Validating topic name length of all Publish topic
            rowCounter = 0;
            foreach (var publish in XMPS.Instance.LoadedProject.Devices.OfType<Publish>().OrderBy(d => d.keyvalue))
            {
                if (publish.topic.ToString().Length > 16)
                {
                    errorsInRequest.Add($"MQTT Publish : Topic name is greater than 16 chars topic name : {publish.topic} at row no-{rowCounter}");
                }

                rowCounter += 2 + publish.PubRequest.Count;
            }

            //Validating length of reqeust names of every request in Subscribe topic.
            rowCounter = 0;
            foreach (var subscribe in XMPS.Instance.LoadedProject.Devices.OfType<Subscribe>().OrderBy(d => d.key))
            {
                if (subscribe.SubRequest != null)
                {
                    foreach (var req in subscribe.SubRequest)
                    {
                        if (!string.IsNullOrEmpty(req.req) && req.req.Length > 16)
                        {
                            errorsInRequest.Add(
                                $"MQTT Subscribe : Request name is greater than 16 chars Subscribe request name : {req.req} at row no-{rowCounter}");
                        }
                    }
                }
                rowCounter += 2 + subscribe.SubRequest.Count;
            }

            //Validating length of reqeust names of every request in publish topic.
            rowCounter = 0;
            foreach (var publish in XMPS.Instance.LoadedProject.Devices.OfType<Publish>().OrderBy(d => d.keyvalue))
            {
                bool hasLongReq = publish.PubRequest.Any(pr => pr.req.ToString().Length > 16);

                if (hasLongReq)
                {
                    errorsInRequest.Add($"MQTT Publish : One or more request names exceed 16 characters in topic : {publish.topic} at row no-{rowCounter}");
                }

                rowCounter += 2 + publish.PubRequest.Count;
            }

            //Validating requst count of each topic in Publish Topic.
            rowCounter = 0;
            int maxMQTTReqCount = XMPS.Instance.LoadedProject.PlcModel.EndsWith("-E") ? 100 : 10;
            foreach (var publish in XMPS.Instance.LoadedProject.Devices.OfType<Publish>().OrderBy(d => d.keyvalue))
            {
                if (publish.PubRequest.Count > maxMQTTReqCount)
                {
                    errorsInRequest.Add(
                        $"MQTT Publish : Topic '{publish.topic}' has more than  " + maxMQTTReqCount.ToString() + " requests added, permitted requests are " + maxMQTTReqCount.ToString() + " only at row no-{rowCounter}");
                }
                rowCounter += 2 + publish.PubRequest.Count;
            }

            //Validating requst count of each topic in Subscribe Topic.
            rowCounter = 0;
            foreach (var subscribe in XMPS.Instance.LoadedProject.Devices.OfType<Subscribe>().OrderBy(d => d.key))
            {
                if (subscribe.SubRequest.Count > maxMQTTReqCount)
                {
                    errorsInRequest.Add(
                        $"MQTT Subscribe : Some Topics are having more than " + maxMQTTReqCount.ToString() + " requests added : {subscribe.topic}, permitted requests are " + maxMQTTReqCount.ToString() + " only at row no-{rowCounter}");
                }
                rowCounter += 2 + subscribe.SubRequest.Count;
            }

            ///////////////////////////////////////////////////////////////////////////////////
            ///Apply check for count on slaves and requests for modbus/////////////////////////
            int maxModbusTCPClientCount = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.Ethernet != null)
                                         .SelectMany(t => t.Ethernet.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MODBUSTCPClient").MaxCount ?? 0);
            if (XMPS.Instance.LoadedProject.Devices.OfType<MODBUSTCPClient>().Select(t => t.Slaves.Count()).FirstOrDefault() > maxModbusTCPClientCount) errorsInRequest.Add($"MODBUS TCP Client : request limit is crossed, maximum allowed limit is {maxModbusTCPClientCount} at row no-{0}");

            int maxModbusTCPServerCount = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?
                                         .Where(template => template.Ethernet != null).SelectMany(t => t.Ethernet.TreeNodes)
                                        .SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MODBUSTCPServer")?.MaxCount ?? 0);
            if (XMPS.Instance.LoadedProject.Devices.OfType<MODBUSTCPServer>().Select(t => t.Requests.Count()).FirstOrDefault() > maxModbusTCPServerCount) errorsInRequest.Add($"MODBUS TCP Server : Modbus TCP Server request limit is crossed, maximum allowed limit is {maxModbusTCPServerCount} at row no-{0}");

            int maxModbusRTUMasterCount = (int)(XMPS.Instance.LoadedProject.SystemConfiguration.Devices.FirstOrDefault()?.Templates?.Where(template => template.RS485 != null)
                                          .SelectMany(t => t.RS485.TreeNodes).SelectMany(node => node.Devices).FirstOrDefault(device => device.Name == "MODBUSRTUMaster").MaxCount ?? 0);
            if (XMPS.Instance.LoadedProject.Devices.OfType<MODBUSRTUMaster>().Select(t => t.Slaves.Count()).FirstOrDefault() > maxModbusRTUMasterCount) errorsInRequest.Add($"MODBUS RTU Master : request limit is crossed, maximum allowed limit is {maxModbusRTUMasterCount} at row no-{0}");

            if (XMPS.Instance.LoadedProject.Devices.OfType<MODBUSRTUSlaves>().Select(t => t.Slaves.Count()).FirstOrDefault() > 100) errorsInRequest.Add($"MODBUS RTU Slaves : request limit is crossed, maximum allowed limit is 100 at row no-{0}");

            ///////////////////////////////////////////////////////////////////////////////////
            if (XMPS.Instance.LoadedProject.BacNetIP != null)
            {
                errorsInRequest.AddRange(XMPS.Instance.LoadedProject.BacNetIP.Schedules.OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                            .Where(t => string.IsNullOrEmpty(t.LogicalAddress) || !XMPS.Instance.LoadedProject.Tags.Any(a => a.LogicalAddress.Equals(t.LogicalAddress)))
                            .Select(t => $"Schedule : Logical address is not assigned in schedule {t.ObjectName} at row no-" +
                            $"{XMPS.Instance.LoadedProject.BacNetIP.Schedules.IndexOf(t)}"));

                errorsInRequest.AddRange(XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                            .Where(t => t.ObjectName.Length > 25)
                            .Select(t => $"Binary Value: Length must be less than 25 characters at row no-" +
                            $"{XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.IndexOf(t)}"));

                errorsInRequest.AddRange(XMPS.Instance.LoadedProject.BacNetIP.MultistateValues.OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                            .Where(t => t.ObjectName.Length > 25)
                            .Select(t => $"Multistate Value: Length must be less than 25 characters at row no-" +
                            $"{XMPS.Instance.LoadedProject.BacNetIP.MultistateValues.IndexOf(t)}"));
                errorsInRequest.AddRange(XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                            .Where(t => t.ObjectName.Length > 25)
                            .Select(t => $"Analog Value: Length must be less than 25 characters at row no-" +
                            $"{XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.IndexOf(t)}"));
            }

            return errorsInRequest;
        }
        public static void CheckSystemTagsInProject()
        {
            List<XMIOConfig> tags = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress.StartsWith("S3")).ToList();
            foreach (XMIOConfig xMIO in tags)
            {
                string actualTagName = GetTheTagnameFromAddress(xMIO.LogicalAddress);

                var modbusrtu = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSRTUMaster>().SelectMany(t => t.Slaves.Where(a => a.Variable.Equals(xMIO.LogicalAddress)));
                foreach (MODBUSRTUMaster_Slave slave in modbusrtu)
                {
                    if (!slave.Tag.Equals(actualTagName))
                    {
                        slave.Tag = actualTagName;
                    }
                }

                var modbusrtuSlave = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSRTUSlaves>()
                 .SelectMany(master => master.Slaves.Where(slave => slave.Variable.Equals(xMIO.LogicalAddress)));

                foreach (MODBUSRTUSlaves_Slave rtuSlave in modbusrtuSlave)
                {
                    if (!rtuSlave.Tag.Equals(actualTagName))
                    {
                        rtuSlave.Tag = actualTagName;
                    }
                }

                var modbusTcpClient = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSTCPClient>()
                 .SelectMany(master => master.Slaves.Where(slave => slave.Variable.Equals(xMIO.LogicalAddress)));

                foreach (MODBUSTCPClient_Slave tcpClient in modbusTcpClient)
                {
                    if (!tcpClient.Tag.Equals(actualTagName))
                    {
                        tcpClient.Tag = actualTagName;
                    }
                }

                var modbusTcpServer = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSTCPServer>()
                      .SelectMany(master => master.Requests.Where(slave => slave.Variable.Equals(xMIO.LogicalAddress)));

                foreach (MODBUSTCPServer_Request tcpServerReq in modbusTcpServer)
                {
                    if (!tcpServerReq.Tag.Equals(actualTagName))
                    {
                        tcpServerReq.Tag = actualTagName;
                    }
                }

                var publishRequests = XMPS.Instance.LoadedProject.Devices.OfType<Publish>()
                            .SelectMany(p => p.PubRequest).Where(pr => pr.Tag.Contains(":") ? pr.Tag.Equals(xMIO.LogicalAddress) : pr.Tag.Equals(xMIO.Tag));
                foreach (PubRequest pr in publishRequests)
                {
                    pr.Tag = xMIO.Tag;
                }
                var subscribeRequests = XMPS.Instance.LoadedProject.Devices.OfType<Subscribe>()
                            .SelectMany(p => p.SubRequest).Where(pr => pr.Tag.Contains(":") ? pr.Tag.Equals(xMIO.LogicalAddress) : pr.Tag.Equals(xMIO.Tag));
                foreach (SubscribeRequest subscribeRequest in subscribeRequests)
                {
                    subscribeRequest.Tag = xMIO.Tag;
                }
            }
        }

        public static (HashSet<string>, bool) GetUDFBUsedVariables(string udfbName)
        {
            HashSet<string> udfbVariable = new HashSet<string>();
            bool isUdfbCalling = false;
            XMPS xm = XMPS.Instance;
            var BlockCount = xm.LoadedProject.Blocks.FirstOrDefault(t => t.Name.Equals($"{udfbName} Logic") && t.Type.Equals("UDFB"));
            string windowName = BlockCount.Type.Equals("UDFB") ? "UDFLadderForm#" : "LadderForm#";
            if (xm.LoadedScreens.ContainsKey($"{windowName}{BlockCount.Name}"))
            {
                LadderWindow _windowRef = windowName.Equals("LadderForm#") ?
                                        (LadderWindow)xm.LoadedScreens[$"LadderForm#{BlockCount.Name}"] : (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{BlockCount.Name}"];
                LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();

                for (int k = 0; k < LadderDrawing.LadderDesign.Active.Elements.Count(); k++)
                {
                    for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[k].Elements.Count; j++)
                    {
                        LadderElement ld = LadderDrawing.LadderDesign.Active.Elements[k].Elements[j];
                        if (ld.Attributes.Any(t => t.Name.Equals("isCommented")))
                            continue;
                        if (ld.customDrawing.GetType().Name.Equals("Contact"))
                        {
                            if (!string.IsNullOrEmpty(ld.Attributes["caption"].ToString()) && !ld.Attributes["caption"].ToString().Equals("-"))
                            {
                                string pattern = @"^-?\d+(\.\d+)?$";
                                bool isNumeric = Regex.IsMatch(ld.Attributes["caption"].ToString(), pattern);
                                if (isNumeric)
                                    continue;
                                bool tag = ld.Attributes["caption"].ToString().Contains(":") ?
                                           XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(ld.Attributes["caption"].ToString()))
                                           : XMPS.Instance.LoadedProject.Tags.Any(t => t.Tag.Equals(ld.Attributes["caption"].ToString()));
                                if (!tag)
                                {
                                    udfbVariable.Add(ld.Attributes["caption"].ToString());
                                }
                            }
                        }
                        else if (ld.customDrawing.GetType().Name.Equals("DummyParallelParent") || ld.customDrawing.GetType().Name.Equals("Coil"))
                        {
                            if (!string.IsNullOrEmpty(ld.Attributes["caption"].ToString()) && !ld.Attributes["caption"].ToString().Equals("-"))
                            {
                                string pattern = @"^-?\d+(\.\d+)?$";
                                bool isNumeric = Regex.IsMatch(ld.Attributes["caption"].ToString(), pattern);
                                if (isNumeric)
                                    continue;
                                bool tag = ld.Attributes["caption"].ToString().Contains(":") ?
                                           XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(ld.Attributes["caption"].ToString()))
                                           : XMPS.Instance.LoadedProject.Tags.Any(t => t.Tag.Equals(ld.Attributes["caption"].ToString()));
                                if (!tag)
                                {
                                    udfbVariable.Add(ld.Attributes["caption"].ToString());
                                }
                            }
                            if (ld.Elements.Count > 0)
                            {
                                CheckInChildUDFBElements(ld.Elements, ref udfbVariable);
                            }
                        }
                        else if (ld.customDrawing.GetType().Name.Equals("FunctionBlock"))
                        {
                            if (ld.Attributes["OpCode"].ToString().Equals("9999"))
                            {
                                isUdfbCalling = true;
                                return (new HashSet<string>(), isUdfbCalling);
                            }
                            if (XMPS.Instance.instructionsList.FirstOrDefault(t =>
                            t.Text.Equals(ld.Attributes["function_name"].ToString().StartsWith("MES_PID_") ? "MES_PID" : ld.Attributes["function_name"])) == null)
                            {
                                isUdfbCalling = true;
                                return (new HashSet<string> { $"{ld.Attributes["function_name"].ToString()} not found in current instruction types" }, isUdfbCalling);
                            }
                            List<string> functionblkAdd = ld.Attributes.Where(t => t.Name.StartsWith("input") || t.Name.StartsWith("output")).ToList().Select(t => t.Value.ToString().Replace("~", "")).ToList();
                            foreach (string caption in functionblkAdd)
                            {
                                if (!string.IsNullOrEmpty(caption) && !caption.Equals("-"))
                                {
                                    string pattern = @"^-?\d+(\.\d+)?$";
                                    bool isNumeric = Regex.IsMatch(caption, pattern);
                                    if (isNumeric || caption.StartsWith("I1") || caption.StartsWith("Q0") || caption.StartsWith("S3"))
                                        continue;
                                    if (caption.Equals("A5:999"))
                                        continue;
                                    //not checking object name of Notification Device and Scheduel Bacnet Object.
                                    if ((ld.Attributes["function_name"].ToString().Equals("Notification") || ld.Attributes["function_name"].ToString().Equals("Device")
                                        || ld.Attributes["function_name"].ToString().Equals("Schedule")) && functionblkAdd.IndexOf(caption) == 0)
                                    {
                                        continue;
                                    }

                                    if ((ld.Attributes["function_name"].ToString().Equals("MQTT Publish") || ld.Attributes["function_name"].ToString().Equals("MQTT Subscribe"))
                                    && (caption.StartsWith("PUB.") || caption.StartsWith("SUB.")))
                                    {
                                        continue;
                                    }
                                    bool tag = caption.Contains(":") ?
                                               XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(caption))
                                              : XMPS.Instance.LoadedProject.Tags.Any(t => t.Tag.Equals(caption));
                                    if (!tag)
                                    {
                                        udfbVariable.Add(caption);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return (udfbVariable, isUdfbCalling);
        }

        private static void CheckInChildUDFBElements(LadderElements elements, ref HashSet<string> udfbVariables)
        {
            foreach (var element in elements)
            {
                if (element.customDrawing.GetType().Name.Equals("Contact") || element.customDrawing.GetType().Name.Equals("CoilParallel"))
                {
                    string pattern = @"^\d+(\.\d+)?$";
                    bool isNumeric = Regex.IsMatch(element.Attributes["caption"].ToString(), pattern);
                    if (isNumeric)
                        continue;
                    bool tag = element.Attributes["caption"].ToString().Contains(":") ?
                               XMPS.Instance.LoadedProject.Tags.Any(t => t.LogicalAddress.Equals(element.Attributes["caption"].ToString()))
                               : XMPS.Instance.LoadedProject.Tags.Any(t => t.Tag.Equals(element.Attributes["caption"].ToString()));
                    if (!tag)
                    {
                        udfbVariables.Add(element.Attributes["caption"].ToString());
                    }
                }
                if (element.Elements.Any())
                {
                    CheckInChildUDFBElements(element.Elements, ref udfbVariables);
                }
            }
        }

        internal static decimal GetMaxLength(string LogicalAddress)
        {
            try
            {
                // Check if XMPS instance exists
                if (XMPS.Instance == null)
                    return 0;

                // Check if MemoryAllocation collection exists
                if (XMPS.Instance.MemoryAllocation == null)
                    return 0;

                // Check if LogicalAddress is valid
                if (string.IsNullOrEmpty(LogicalAddress) || LogicalAddress.Length < 2)
                    return 0;

                // Get the first 2 characters safely
                string addressPrefix = LogicalAddress.Substring(0, 2);

                // Filter and get matching memory allocations
                var matchingAllocations = XMPS.Instance.MemoryAllocation
                    .Where(m => m != null &&
                               !string.IsNullOrEmpty(m.Initial) &&
                               m.Initial.StartsWith(addressPrefix))
                    .ToList();

                // Check if any matching allocations exist
                if (!matchingAllocations.Any())
                    return 0;

                // Get the maximum limit safely
                return matchingAllocations.Max(m => m.Limit);
            }
            catch (Exception ex)
            {
                // Log the exception if you have logging
                // Logger.LogError($"Error getting max memory limit: {ex.Message}");
                return 0;
            }

            //return LogicalAddress.Contains(":") ? XMPS.Instance.MemoryAllocation.Where(m => m.Initial.StartsWith(LogicalAddress.Substring(0, 2))).Max(m => m.Limit) : 0;
        }

        internal static void ValidateInstructionWithInputsOutputs(List<string> curBlockRungs, ref List<string> checkInstructionRungs, UDFBInfo block = null)
        {
            foreach (string rung in curBlockRungs)
            {
                if (!rung.Contains("OPC:9999") && !rung.StartsWith("'") && rung.Contains("FN:"))
                {
                    string instructionName = GettingInstructionNameFromRung(rung);

                    string strsplit = rung.Split(new[] { "DT" }, StringSplitOptions.None)[0];
                    int inputCount = strsplit.Split(new[] { "IN:" }, StringSplitOptions.None).Skip(1).Count(part => !string.IsNullOrWhiteSpace(part));


                    if (XMPS.Instance.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionName)) == null)
                    {
                        checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} not found in current instruction types");
                        continue;
                    }
                    if (instructionName.Equals("NULL"))
                    {
                        inputCount = 0;
                    }
                    //Checking No.of inputs in current rung and actual input count
                    if (XMPS.Instance.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionName)).InputsOutputs.Count(t => t.Type.Equals("Input")) < inputCount
                        && !instructionName.Equals("MQTT Publish") && !instructionName.Equals("MQTT Subscribe"))
                    {
                        checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} inputs count not match with instruction declaration");
                        continue;
                    }

                    if (!instructionName.Equals("MQTT Publish") && !instructionName.Equals("MQTT Subscribe"))
                    {
                        for (int i = 0; i < inputCount; i++)
                        {
                            string operandValue = strsplit.Split(new[] { "IN:" }, StringSplitOptions.None)[i + 1];
                            if (!string.IsNullOrWhiteSpace(operandValue))
                            {
                                string operandDataType = XMPS.Instance.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionName)).InputsOutputs.FirstOrDefault(t => t.Id == i + 1 && t.Type.Equals("Input")).DataType;
                                if (operandDataType.Equals("Unknown"))
                                {
                                    operandDataType = GetDTNDataTypeValue(rung);
                                }
                                if (operandDataType.Equals("Schedule") || operandDataType.Equals("Notification") || operandDataType.Equals("Device"))
                                {
                                    switch (instructionName)
                                    {
                                        case "Notification Class":
                                            if (XMPS.Instance.LoadedProject.BacNetIP.Notifications.Any(t => t.ObjectName.Equals(operandValue.Trim())))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} function block Object name not found {i + 1}");
                                                continue;
                                            }
                                        case "Device":
                                            if (XMPS.Instance.LoadedProject.BacNetIP.Device.ObjectName.Equals(operandValue.Trim()))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} function block Object name not found {i + 1}");
                                                continue;
                                            }
                                        case "Schedule":
                                            if (XMPS.Instance.LoadedProject.BacNetIP.Schedules.Any(t => t.ObjectName.Equals(operandValue.Trim())))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} function block Object name not found {i + 1}");
                                                continue;
                                            }
                                    }
                                }
                                //adding extrac check for only if the operand value from the UDFB block
                                if (block != null && !operandValue.Contains(":") && !double.TryParse(operandValue.Trim(), out _))
                                {
                                    bool isValid = block.UDFBlocks.Any(s => s.Text.Equals(operandValue.Trim().Replace("~", "")) && s.DataType.Equals(operandDataType));
                                    if (isValid)
                                        continue;
                                    else
                                    {
                                        checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} function block input values not match with dataType on Input {i + 1}");
                                        continue;
                                    }
                                }
                                bool validatation = XMProValidator.ValidateUDFBOperad(operandValue.Trim().Replace("~", ""), operandDataType, "Input");
                                if (!validatation)
                                {
                                    checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} function block input values not match with dataType on Input {i + 1}");
                                    continue;
                                }
                            }
                        }
                    }

                    strsplit = rung.Split(new[] { "OPTN" }, StringSplitOptions.None)[1];
                    int outputCount = strsplit.Split(new[] { "OP:" }, StringSplitOptions.None).Skip(1).Count(part => !string.IsNullOrWhiteSpace(part) /*&& part.Trim().Split(' ')[0].Contains(":")*/);

                    if (instructionName.Equals("MQTT Publish") || instructionName.Equals("MQTT Subscribe"))
                        outputCount = 1;
                    //Checking No.of inputs in current rung and actual input count
                    if (XMPS.Instance.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionName)).InputsOutputs.Count(t => t.Type.Equals("Output")) < outputCount)
                    {
                        checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} output count not match with instruction declaration");
                        continue;
                    }
                    for (int i = 0; i < outputCount; i++)
                    {
                        try
                        {
                            string operandValue = strsplit.Split(new[] { "OP:" }, StringSplitOptions.None)[i + 1].Replace("]);", "");
                            if (!string.IsNullOrWhiteSpace(operandValue))
                            {
                                string operandDataType = XMPS.Instance.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionName)).InputsOutputs.FirstOrDefault(t => t.Id == i + 1 && t.Type.Equals("Output")).DataType;
                                if (operandDataType.Equals("Unknown"))
                                {
                                    operandDataType = XMPS.Instance.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionName)).OutputDataTypes.Count() > 0 ?
                                                      XMPS.Instance.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionName)).OutputDataTypes.FirstOrDefault().Text
                                                      : GetDTNDataTypeValue(rung);
                                }
                                //adding extrac check for ony if the is from the UDFB block
                                if (block != null && !operandValue.Contains(":") && !operandValue.All(char.IsDigit))
                                {
                                    bool isValid = block.UDFBlocks.Any(s => s.Text.Equals(operandValue.Trim()) && s.DataType.Equals(operandDataType));
                                    if (isValid)
                                        continue;
                                    else
                                    {
                                        checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} function block input values not match with dataType on Output {i + 1}");
                                        continue;
                                    }
                                }
                                bool validatation = ValidateUDFBOperad(operandValue.Trim(), operandDataType, "Output");
                                if (!validatation)
                                {
                                    checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} function block input values not match with dataType on Output {i + 1}");
                                    continue;
                                }
                            }
                        }
                        catch
                        {
                            checkInstructionRungs.Add($"Rung {curBlockRungs.IndexOf(rung) + 1}: for {instructionName} check input output configuration {i + 1}");
                            continue;
                        }
                    }
                }
            }
        }

        private static string GetDTNDataTypeValue(string rung)
        {
            string key = "DTN:";
            int startIndex = rung.IndexOf(key);
            if (startIndex != -1)
            {
                startIndex += key.Length;
                int endIndex = rung.IndexOf(" ", startIndex);
                if (endIndex == -1)
                {
                    endIndex = rung.IndexOf("]", startIndex);
                }
                string dtnValue = endIndex != -1 ? rung.Substring(startIndex, endIndex - startIndex) : rung.Substring(startIndex);
                return dtnValue.StartsWith("Double") ? "Double Word" : dtnValue;
            }
            return "Unknown";
        }

        public static string GettingInstructionNameFromRung(string rung)
        {

            if (!rung.Contains("FN:"))
                return string.Empty;
            int startIndexFN = rung.IndexOf("FN:");
            int endIndexFN = rung.IndexOf(" ", startIndexFN + 3);
            string instructionName = rung.Substring(startIndexFN, endIndexFN - startIndexFN);
            instructionName = instructionName.Split(':')[1];
            instructionName = instructionName.Equals("ANY@to@Dword") ? "ANY to DWORD" : instructionName;
            //Removing the PID instruction number from the function_name Attributes.
            instructionName = instructionName.StartsWith("MES_PID_") ? "MES_PID" : instructionName;
            instructionName = instructionName.Replace("@", " ");
            return instructionName;
        }

        internal static void ValidateInstructionData(InstructionTypeDeserializer instruction, ref List<string> validatingInstruction)
        {
            var disallowedTcNames = new List<string> { "FB", "PK", "UPK", "PUB", "SUB" };

            if (!Regex.IsMatch(instruction.ID, @"^0x[0-9A-Fa-f]+$") || (instruction.ID.Length > 6 && !instruction.InstructionType.Equals("DataConversion")))
            {
                validatingInstruction.Add($"Invalid Hexadecimal ID for {instruction.Text}");
            }

            //ValidateTcName Details
            if (instruction.TcNameDetails != null)
            {
                var tcName = instruction.TcNameDetails.Instruction;

                if (!Regex.IsMatch(tcName, @"^[A-Za-z]+$"))
                {
                    validatingInstruction.Add($"Only characters are allowed for TC Name. Check TC Name Details for {instruction.Text}");
                }

                if (tcName.Length > 3)
                {
                    validatingInstruction.Add($"Only three characters are allowed for TC Name. Check TC Name Details for {instruction.Text}");
                }

                if (disallowedTcNames.Contains(tcName, StringComparer.OrdinalIgnoreCase))
                {
                    validatingInstruction.Add($"The TC Name '{tcName}' is already in use. Check TC Name Details for {instruction.Text}");
                }
            }
            // Validate Inputs and Outputs
            var inputIds = instruction.InputsOutputs
                                      .Where(io => io.Type == "Input")
                                      .Select(io => io.Id)
                                      .ToList();

            var outputIds = instruction.InputsOutputs
                                       .Where(io => io.Type == "Output")
                                       .Select(io => io.Id)
                                       .ToList();

            if (!IsSequential(inputIds))
            {
                validatingInstruction.Add($"Input IDs are not sequential or contain duplicates in Instruction {instruction.Text}");
            }

            if (!IsSequential(outputIds))
            {
                validatingInstruction.Add($"Output IDs are not sequential or contain duplicates in Instruction {instruction.Text}");
            }
            if (instruction.InputsOutputs != null && instruction.InputsOutputs.Count(t => t.Type.Equals("Output")) == 0)
            {
                validatingInstruction.Add($"For the {instruction.Text} minimum one output required");
            }
            if (instruction.InputsOutputs != null && instruction.InputsOutputs.Count(t => t.Type.Equals("Input")) == 0 && !instruction.InstructionType.Equals("MQTT") && instruction.IsInputRequired)
            {
                validatingInstruction.Add($"For the {instruction.Text} minimum one input required");
            }
            //validating inputs and outputs dataType
            foreach (IOModel iomodel in instruction.InputsOutputs)
            {
                if (!DataTypeDesilizer.List.Any(t => t.Text.Equals(iomodel.DataType)) && !iomodel.DataType.Equals("Unknown")
                    && !iomodel.DataType.Equals("Schedule") && !iomodel.DataType.Equals("Notification") && !iomodel.DataType.Equals("Device"))
                {
                    validatingInstruction.Add($"Check data type for {iomodel.Type} no {iomodel.Id} Instruction {instruction.Text}");
                }
            }

            //checking supported Data Types
            if (instruction.SupportedDataTypes != null && instruction.SupportedDataTypes.Count > 0)
            {
                foreach (DataTypeDesilizer dataType in instruction.SupportedDataTypes)
                {
                    if (!Regex.IsMatch(dataType.ID, @"^0x[0-9A-Fa-f]+$") || dataType.ID.Length > 6)
                    {
                        validatingInstruction.Add($"Check data type ID for supported data type {dataType.Text} of Instruction {instruction.Text}");
                    }
                    if (!Regex.IsMatch(dataType.Text, @"^[A-Za-z]+$") && !dataType.Text.Equals("Double Word"))
                    {
                        validatingInstruction.Add($"Check data type Text for supported data type {dataType.Text} numeric value or special character or space not allow of Instruction {instruction.Text}");
                    }
                }
            }
            //checking outpus Data Types
            if (instruction.OutputDataTypes != null && instruction.OutputDataTypes.Count > 0)
            {
                foreach (DataTypeDesilizer dataType in instruction.OutputDataTypes)
                {
                    if (!Regex.IsMatch(dataType.ID, @"^0x[0-9A-Fa-f]+$") || dataType.ID.Length > 6)
                    {
                        validatingInstruction.Add($"Check data type ID for output data type {dataType.Text} of Instruction {instruction.Text}");
                    }
                    if (!Regex.IsMatch(dataType.Text, @"^[A-Za-z]+$") && !dataType.Text.Equals("Double Word"))
                    {
                        validatingInstruction.Add($"Check data type Text for output data type {dataType.Text} numeric value or special character or space not allow of Instruction {instruction.Text}");
                    }
                }
            }
        }
        private static bool IsSequential(List<int> ids)
        {
            if (ids.Count == 0) return true;
            ids.Sort();
            for (int i = 0; i < ids.Count; i++)
            {
                if (ids[i] != i + 1) return false;
            }
            return true;
        }

        public static bool ValidateImportingTag(string tagAddress)
        {
            var tags = XMPS.Instance.LoadedProject.Tags;
            var lastTag = tags.LastOrDefault();
            if (lastTag == null)
                return true;
            string[] tagAddressParts = tagAddress.Split(':');
            if (lastTag.Label == "Double Word" || lastTag.Label == "DINT")
            {
                if (tagAddressParts.Length == 2 &&
                int.TryParse(tagAddressParts[1], out int tagAddressValue) &&
                int.TryParse(lastTag.LogicalAddress.Split(':')[1], out int lastAddressValue))
                {
                    return (tagAddressValue - lastAddressValue) > 1;
                }
            }
            else if (tagAddress.StartsWith("W4") && tagAddressParts.Length == 2 &&
                    int.TryParse(tagAddressParts[1], out int previousTagNumber))
            {
                string findingTagAddress = $"{tagAddressParts[0]}:{(previousTagNumber - 1):D3}";
                var existingTag = tags.FirstOrDefault(t => t.LogicalAddress == findingTagAddress);

                if (existingTag?.Label == "Double Word" || existingTag?.Label == "DINT")
                    return false;
            }

            return true;
        }
        public static string GetPreviousLogicalAddress(string currentAddress)
        {
            var parts = currentAddress.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1], out int number))
            {
                return $"{parts[0]}:{(number - 1):D3}";
            }
            return string.Empty;
        }

        public static string GetNewNameForLogicBlock(string originalName)
        {
            //var isAlreadyPresentBlk = XMPS.Instance.LoadedProject.Blocks.Where(t => t.Type.Equals("LogicBlock") && t.Name.Equals(originalName));
            string newName = originalName + "_1";
            if (XMPS.Instance.LoadedProject.Blocks.Where(t => t.Type.Equals("LogicBlock")).Any(t => t.Name.Equals(newName)))
            {
                DialogResult result = MessageBox.Show($"{newName} name is already used in Logic block do you want to replace?", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    return newName;
                }
                else
                    return GetNewNameForLogicBlock(newName);
            }
            return newName;
        }

        public static bool ValidateDoubleWordTag(XMIOConfig tag, ref List<string> wrongAddressErrors)
        {
            string[] tagAddressParts = tag.LogicalAddress.Split(':');
            if (int.TryParse(tagAddressParts[1], out int address))
            {
                if (tag.Label.Equals("Word") || tag.Label.Equals("Int"))
                {
                    if (address > 0)
                    {
                        int previousAddress = address - 1;
                        string newAddress = $"{tagAddressParts[0]}:{previousAddress:D3}";
                        XMIOConfig oldAddTag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(newAddress));
                        if (oldAddTag != null && (oldAddTag.Label.Equals("Double Word") || oldAddTag.Label.Equals("DINT")))
                        {
                            wrongAddressErrors.Add($"{tag.LogicalAddress} is already occupied by {oldAddTag.Label} address {oldAddTag.LogicalAddress}");
                            return false;
                        }
                    }
                }
                else if (tag.Label.Equals("Double Word") || tag.Label.Equals("DINT"))
                {
                    int nextAddress = address + 1;
                    string newAddress = $"{tagAddressParts[0]}:{nextAddress:D3}";
                    XMIOConfig newAddTag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(newAddress));
                    if (newAddTag != null && newAddTag.Label.Equals("Word"))
                    {
                        wrongAddressErrors.Add($"for {tag.LogicalAddress} is already used as word address by {newAddTag.Tag}");
                        return false;
                    }
                }
            }

            return true;
        }

        public static List<LadderElement> GettigUDFBFunctionBlockRungs(string textUDFBName)
        {
            List<LadderElement> rungs = new List<LadderElement>();
            List<Block> BlockCount = new List<Block>();
            BlockCount = XMPS.Instance.LoadedProject.Blocks.Where(t => t.Type == "LogicBlock" || t.Type == "InterruptLogicBlock").ToList();

            for (int B = 0; B < BlockCount.Count; B++)
            {
                if (XMPS.Instance.LoadedScreens.ContainsKey($"LadderForm#{BlockCount[B].Name}"))
                {
                    LadderWindow _windowRef = (LadderWindow)XMPS.Instance.LoadedScreens[$"LadderForm#{BlockCount[B].Name}"];
                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();

                    for (int k = 0; k < LadderDrawing.LadderDesign.Active.Elements.Count(); k++)
                    {
                        for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[k].Elements.Count; j++)
                        {
                            LadderElement ld = LadderDrawing.LadderDesign.Active.Elements[k].Elements[j];
                            if (ld.Attributes["function_name"].ToString().Equals(textUDFBName))
                            {
                                rungs.Add(ld);
                            }
                        }
                    }
                }
            }
            return rungs;
        }
        public static string ArrangeTheTCNameDetails(string blockName, Dictionary<string, Counter> tcNameDetails)
        {
            List<string> errorMessage = new List<string>();
            string errorToMain = string.Empty;
            if (XMPS.Instance.LoadedScreens.ContainsKey($"LadderForm#{blockName}"))
            {
                LadderWindow _windowRef = (LadderWindow)XMPS.Instance.LoadedScreens[$"LadderForm#{blockName}"];

                for (int k = 0; k < _windowRef.getLadderEditor().getCanvas().getDesignView().Elements.Count(); k++)
                {
                    for (int j = 0; j < _windowRef.getLadderEditor().getCanvas().getDesignView().Elements[k].Elements.Count; j++)
                    {
                        LadderElement ld = _windowRef.getLadderEditor().getCanvas().getDesignView().Elements[k].Elements[j];
                        if (ld.Attributes["OpCode"].ToString().Equals("9999") && !ld.Attributes.Any(t => t.Name.Equals("isCommented")))
                        {
                            errorMessage = UpdateInsideUDFBLB(ld, ref tcNameDetails);
                            if (errorMessage != null && errorMessage.Count > 0)
                            {
                                _windowRef.Refresh();
                                errorToMain = blockName + "\n";
                                errorToMain += $"Rung {k + 1}: for " + errorMessage.First() + $"At the time of validating function block {ld.Attributes["function_name"].ToString()}";
                                return errorToMain;
                            }
                        }
                        else
                        {
                            if ((tcNameDetails.ContainsKey(ld.Attributes["function_name"].ToString()) ||
                                ld.Attributes["function_name"].ToString().StartsWith("MES_PID")) && !ld.Attributes.Any(t => t.Name.Equals("isCommented")))
                            {
                                errorMessage = UpdateTCDetails(ld, ref tcNameDetails);
                                if (errorMessage != null && errorMessage.Count > 0)
                                {
                                    _windowRef.Refresh();
                                    errorToMain = blockName + "\n";
                                    errorToMain += $"Rung {k + 1}: for " + errorMessage.First() + $"At the time of validating function block {ld.Attributes["function_name"].ToString()}";
                                    return errorToMain;
                                }
                            }
                        }
                    }

                }
                _windowRef.Refresh();
            }
            return string.Empty;
        }

        private static List<string> UpdateInsideUDFBLB(LadderElement ld, ref Dictionary<string, Counter> tcNameDetails)
        {
            List<string> errorsMessage = new List<string>();
            if (!XMPS.Instance.LoadedProject.UDFBInfo.Any(t => t.UDFBName.Equals(ld.Attributes["function_name"].ToString())))
            {
                errorsMessage.Add($"UDFB {ld.Attributes["function_name"].ToString()} not found in current project");
                return errorsMessage;
            }
            if (!XMPS.Instance.LoadedScreens.ContainsKey($"UDFLadderForm#{ld.Attributes["function_name"].ToString() + " Logic"}"))
            {
                return errorsMessage;
            }

            LadderWindow _windowRef = (LadderWindow)XMPS.Instance.LoadedScreens[$"UDFLadderForm#{ld.Attributes["function_name"].ToString() + " Logic"}"];
            for (int k = 0; k < _windowRef.getLadderEditor().getCanvas().getDesignView().Elements.Count(); k++)
            {
                for (int j = 0; j < _windowRef.getLadderEditor().getCanvas().getDesignView().Elements[k].Elements.Count; j++)
                {
                    LadderElement ld1 = _windowRef.getLadderEditor().getCanvas().getDesignView().Elements[k].Elements[j];
                    if ((tcNameDetails.ContainsKey(ld1.Attributes["function_name"].ToString()) ||
                        ld1.Attributes["function_name"].ToString().StartsWith("MES_PID_")) && !ld1.Attributes.Any(t => t.Name.Equals("isCommented")))
                    {
                        errorsMessage = UpdateTCDetails(ld1, ref tcNameDetails);
                        if (errorsMessage != null && errorsMessage.Count > 0)
                        {
                            _windowRef.Refresh();
                            return errorsMessage;
                        }
                    }
                }
            }
            _windowRef.Refresh();
            return errorsMessage;
        }

        private static List<string> UpdateTCDetails(LadderElement ld, ref Dictionary<string, Counter> tcNameDetails)
        {
            List<string> errorMessages = new List<string>();
            Counter counter = new Counter();
            if (ld.Attributes["function_name"].ToString().StartsWith("MES_PID_"))
                tcNameDetails.TryGetValue("MES_PID", out counter);
            else
                tcNameDetails.TryGetValue(ld.Attributes["function_name"].ToString(), out counter);
            if (counter.CurrentPosition > counter.Maximum)
            {
                errorMessages.Add($"Instruction {ld.Attributes["function_name"].ToString()} Max limit reach.");
                return errorMessages;
            }
            if (ld.Attributes["function_name"].ToString().StartsWith("MES_PID_"))
            {
                ld.Attributes["function_name"] = counter.Instruction + counter.CurrentPosition;
                tcNameDetails["MES_PID"] = new Counter()
                {
                    Instruction = counter.Instruction,
                    CurrentPosition = counter.CurrentPosition + 1,
                    Maximum = counter.Maximum
                };
            }
            else
            {
                ld.Attributes["TCName"] = counter.Instruction + counter.CurrentPosition;
                tcNameDetails[ld.Attributes["function_name"].ToString()] = new Counter()
                {
                    Instruction = counter.Instruction,
                    CurrentPosition = counter.CurrentPosition + 1,
                    Maximum = counter.Maximum
                };
            }
            return errorMessages;
        }

        internal static void ChangeUDFBVariableInUDFBLogicBlock(string textUDFBName, string oldVariable, string newVariable)
        {
            List<string> errorsMessage = new List<string>();
            if (!XMPS.Instance.LoadedProject.UDFBInfo.Any(t => t.UDFBName.Equals(textUDFBName)))
            {
                return;
            }
            if (!XMPS.Instance.LoadedScreens.ContainsKey($"UDFLadderForm#{textUDFBName + " Logic"}"))
            {
                return;
            }

            LadderWindow _windowRef = (LadderWindow)XMPS.Instance.LoadedScreens[$"UDFLadderForm#{textUDFBName + " Logic"}"];
            for (int k = 0; k < _windowRef.getLadderEditor().getCanvas().getDesignView().Elements.Count(); k++)
            {
                for (int j = 0; j < _windowRef.getLadderEditor().getCanvas().getDesignView().Elements[k].Elements.Count; j++)
                {
                    LadderElement ld1 = _windowRef.getLadderEditor().getCanvas().getDesignView().Elements[k].Elements[j];
                    if (ld1.customDrawing.GetType().Name.Equals("Contact"))
                    {
                        if (!string.IsNullOrEmpty(ld1.Attributes["caption"].ToString()) && ld1.Attributes["caption"].ToString().Equals(oldVariable))
                        {
                            ld1.Attributes["caption"] = newVariable;
                            ld1.Attributes["LogicalAddress"] = string.Empty;
                        }
                    }
                    else if (ld1.customDrawing.GetType().Name.Equals("DummyParallelParent") || ld1.customDrawing.GetType().Name.Equals("Coil"))
                    {
                        if (!string.IsNullOrEmpty(ld1.Attributes["caption"].ToString()) && ld1.Attributes["caption"].ToString().Equals(oldVariable))
                        {
                            ld1.Attributes["caption"] = newVariable;
                            ld1.Attributes["LogicalAddress"] = string.Empty;
                        }
                        if (ld1.Elements.Count > 0)
                        {
                            UpdateUDFBVaribleInChildElement(ld1.Elements, oldVariable, newVariable);
                        }
                    }
                    else if (ld1.customDrawing.GetType().Name.Equals("FunctionBlock"))
                    {
                        if (ld1.Attributes["OpCode"].ToString().Equals("9999"))
                        {
                            return;
                        }
                        foreach (LadderDrawing.Attribute attribute in ld1.Attributes)
                        {
                            if ((attribute.Name.StartsWith("input") || attribute.Name.StartsWith("output")) && attribute.Value.Equals(oldVariable))
                            {
                                attribute.Value = newVariable;
                            }
                        }

                    }
                }
            }
            _windowRef.Refresh();
        }

        private static void UpdateUDFBVaribleInChildElement(LadderElements elements, string oldVariable, string newVariable)
        {
            foreach (var element in elements)
            {
                if ((element.customDrawing.GetType().Name.Equals("Contact") || element.customDrawing.GetType().Name.Equals("CoilParallel")) && element.Attributes["caption"].ToString().Equals(oldVariable))
                {
                    element.Attributes["caption"] = newVariable;
                    element.Attributes["LogicalAddress"] = string.Empty;
                }
                if (element.Elements.Any())
                {
                    UpdateUDFBVaribleInChildElement(element.Elements, oldVariable, newVariable);
                }
            }
        }

        public static void ClearFullySelectedRungs()
        {
            var BlockCount = XMPS.Instance.LoadedProject.Blocks;
            foreach (Block block in BlockCount)
            {
                string windowName = block.Type.Equals("UDFB") ? "UDFLadderForm#" : "LadderForm#";
                if (XMPS.Instance.LoadedScreens.ContainsKey($"{windowName}{block.Name}"))
                {
                    LadderWindow _windowRef = windowName.Equals("LadderForm#") ?
                                            (LadderWindow)XMPS.Instance.LoadedScreens[$"LadderForm#{block.Name}"] : (LadderWindow)XMPS.Instance.LoadedScreens[$"UDFLadderForm#{block.Name}"];
                    _windowRef.getLadderEditor().getCanvas().fullyRungElements.Clear();
                }
            }
        }

        public static void ReplaceInHSIOFunctionBlock(string findcmbText, string replaceText)
        {
            string oldLogicAddress = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(findcmbText)).LogicalAddress;
            string updateLogicaAddress = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(replaceText)).LogicalAddress;

            if (!string.IsNullOrEmpty(oldLogicAddress) && !string.IsNullOrEmpty(updateLogicaAddress))
            {
                var blocks = XMPS.Instance.LoadedProject.HsioBlock.Where(t => t.HSIOBlocks.Any(a => a.Value.Equals(oldLogicAddress))).ToList();
                if (blocks != null && blocks.Count > 0)
                {
                    foreach (var block in blocks)
                    {
                        var hsioBlockToUpdates = block.HSIOBlocks.Where(a => a.Value.Equals(oldLogicAddress));
                        foreach (var hsioBlockToUpdate in hsioBlockToUpdates)
                        {
                            if (hsioBlockToUpdate != null && !hsioBlockToUpdate.Text.Equals("Input") && !hsioBlockToUpdate.Text.Equals("Output"))
                            {
                                hsioBlockToUpdate.Value = updateLogicaAddress;
                            }
                        }
                    }
                }
            }
        }
        public static void ReplaceTagInAllModbusReqSlave(List<TagAddressChange> changedAddressesList)
        {
            var addressMap = changedAddressesList.ToDictionary(x => x.OldAddress, x => x.NewAddress);
            var tagMap = XMPS.Instance.LoadedProject.Tags.ToDictionary(t => t.LogicalAddress, t => t.Tag);

            //MODBUSRTUMaster
            var modbusRTUMaterSlaves = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSRTUMaster>();
            var modbusRTUMaterSlavesrequest = modbusRTUMaterSlaves.SelectMany(master => master.Slaves).OrderBy(a => int.Parse(a.Name.Substring("MODBUSRTUMasterSlave".Length))).ToList();
            modbusRTUMaterSlavesrequest.Where(slave => !string.IsNullOrEmpty(slave.Variable) && slave.Variable.Contains(":")
                                        && addressMap.ContainsKey(slave.Variable)).ToList().ForEach(T =>
                                        {
                                            var newAddress = addressMap[T.Variable];
                                            T.Variable = newAddress;
                                            if (tagMap.TryGetValue(newAddress, out string tag))
                                            {
                                                T.Tag = tag;
                                            }
                                        });

            ///////////////////////////MODBUSRTUSlaves//////////////////////////////////////

            var modbusRTUSlaves = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSRTUSlaves>();
            var modbusRTUSlavesrequest = modbusRTUSlaves.SelectMany(master => master.Slaves).OrderBy(a => int.Parse(a.Name.Substring("MODBUSRTUSlavesSlave".Length))).ToList();

            // Tag Not Found
            modbusRTUSlavesrequest.Where(slave => !string.IsNullOrEmpty(slave.Variable) && slave.Variable.Contains(":")
                                        && addressMap.ContainsKey(slave.Variable)).ToList().ForEach(T =>
                                        {
                                            var newAddress = addressMap[T.Variable];
                                            T.Variable = newAddress;
                                            if (tagMap.TryGetValue(newAddress, out string tag))
                                            {
                                                T.Tag = tag;
                                            }
                                        });

            //////////////////////////MODBUSTCPClient//////////////////////////////////
            var modbusClients = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSTCPClient>();
            var clientRequests = modbusClients.SelectMany(master => master.Slaves).OrderBy(a => int.Parse(a.Name.Substring("MODBUSTCPClientSlave".Length))).ToList();

            // Tag Not Found
            clientRequests.Where(slave => !string.IsNullOrEmpty(slave.Variable) && slave.Variable.Contains(":")
                                        && addressMap.ContainsKey(slave.Variable)).ToList().ForEach(T =>
                                        {
                                            var newAddress = addressMap[T.Variable];
                                            T.Variable = newAddress;
                                            if (tagMap.TryGetValue(newAddress, out string tag))
                                            {
                                                T.Tag = tag;
                                            }
                                        });
            //////////////////////////////////MODBUSTCPSERVER////////////////////////////////

            var modbusServers = XMPS.Instance.LoadedProject.Devices.OfType<MODBUSTCPServer>();

            var requests = modbusServers.SelectMany(master => master.Requests).OrderBy(x => int.Parse(x.Name.Substring("MODBUSTCPServerRequest".Length))).ToList();

            // 1. Tag Not Found Validation
            requests.Where(slave => !string.IsNullOrEmpty(slave.Variable) && slave.Variable.Contains(":")
                                        && addressMap.ContainsKey(slave.Variable)).ToList().ForEach(T =>
                                        {
                                            var newAddress = addressMap[T.Variable];
                                            T.Variable = newAddress;
                                            if (tagMap.TryGetValue(newAddress, out string tag))
                                            {
                                                T.Tag = tag;
                                            }
                                        });
            //PUBLISH
            foreach (var publish in XMPS.Instance.LoadedProject.Devices.OfType<Publish>().OrderBy(d => d.keyvalue))
            {
                publish.PubRequest.Where(pr =>
                    !string.IsNullOrEmpty(pr.Tag) && pr.Tag.Contains(":")
                                        && addressMap.ContainsKey(pr.Tag)).ToList().ForEach(T =>
                                        {
                                            var newAddress = addressMap[T.Tag];
                                            T.Tag = newAddress;
                                        });
            }
            //Subscribe
            foreach (var subscribe in XMPS.Instance.LoadedProject.Devices.OfType<Subscribe>().OrderBy(d => d.key))
            {
                subscribe.SubRequest.Where(pr =>
                    !string.IsNullOrEmpty(pr.Tag) && pr.Tag.Contains(":")
                                        && addressMap.ContainsKey(pr.Tag)).ToList().ForEach(T =>
                                        {
                                            var newAddress = addressMap[T.Tag];
                                            T.Tag = newAddress;
                                        });
            }
            ///////////////////////////////////////////////////////////////////////////////////
            if (XMPS.Instance.LoadedProject.BacNetIP != null)
            {
                XMPS.Instance.LoadedProject.BacNetIP.Schedules.OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                            .Where(t => !string.IsNullOrEmpty(t.LogicalAddress) && addressMap.ContainsKey(t.LogicalAddress)).ToList()
                            .ForEach(T =>
                            {
                                var newAddress = addressMap[T.LogicalAddress];
                                T.LogicalAddress = newAddress;
                            });
                //Checking in Binary IO Values.
                XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                           .Where(t => !string.IsNullOrEmpty(t.LogicalAddress) && addressMap.ContainsKey(t.LogicalAddress)).ToList()
                           .ForEach(T =>
                           {
                               var newAddress = addressMap[T.LogicalAddress];
                               T.LogicalAddress = newAddress;
                           });
                //Checking in Analog IO Values.
                XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.OrderBy(t => Convert.ToInt32(t.InstanceNumber))
                          .Where(t => !string.IsNullOrEmpty(t.LogicalAddress) && addressMap.ContainsKey(t.LogicalAddress)).ToList()
                          .ForEach(T =>
                          {
                              var newAddress = addressMap[T.LogicalAddress];
                              T.LogicalAddress = newAddress;
                          });
            }
            //HSIO
            if (XMPS.Instance.LoadedProject.HsioBlock != null)
            {
                var blocks = XMPS.Instance.LoadedProject.HsioBlock.Where(t => t.HSIOBlocks.Any(a => addressMap.ContainsKey(a.Value))).ToList();
                if (blocks != null && blocks.Count > 0)
                {
                    foreach (var block in blocks)
                    {
                        var hsioBlockToUpdates = block.HSIOBlocks.Where(a => addressMap.ContainsKey(a.Value));
                        foreach (var hsioBlockToUpdate in hsioBlockToUpdates)
                        {
                            if (hsioBlockToUpdate != null && !hsioBlockToUpdate.Text.Equals("Input") && !hsioBlockToUpdate.Text.Equals("Output"))
                            {
                                hsioBlockToUpdate.Value = addressMap[hsioBlockToUpdate.Value];
                            }
                        }
                    }
                }
            }
        }

        public static void ReplaceInFunctionBlocks(List<TagAddressChange> changedAddressesList)
        {
            XMPS xm = XMPS.Instance;
            var BlockCount = xm.LoadedProject.Blocks;
            var addressMap = changedAddressesList.ToDictionary(x => x.OldAddress, x => x.NewAddress);
            for (int B = 0; B < BlockCount.Count; B++)
            {
                string windowName = BlockCount[B].Type.Equals("UDFB") ? "UDFLadderForm#" : "LadderForm#";
                if (xm.LoadedScreens.ContainsKey($"{windowName}{BlockCount[B].Name}"))
                {
                    LadderWindow _windowRef = windowName.Equals("LadderForm#") ?
                                            (LadderWindow)xm.LoadedScreens[$"LadderForm#{BlockCount[B].Name}"] : (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{BlockCount[B].Name}"];
                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();

                    for (int k = 0; k < LadderDrawing.LadderDesign.Active.Elements.Count(); k++)
                    {
                        for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[k].Elements.Count; j++)
                        {
                            LadderElement ld = LadderDrawing.LadderDesign.Active.Elements[k].Elements[j];
                            if (ld.customDrawing.GetType().Name.Equals("FunctionBlock"))
                            {
                                // First, collect all attributes that need to be updated
                                var attributesToUpdate = ld.Attributes
                                    .Where(attr => addressMap.ContainsKey(attr.Value.ToString()))
                                    .ToList();

                                // Then, apply changes
                                foreach (var attr in attributesToUpdate)
                                {
                                    attr.Value = addressMap[attr.Value.ToString()];
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void ReplaceInContactAndCoilElements(string oldAddress, string newAddress)
        {
            XMPS xm = XMPS.Instance;
            var BlockCount = xm.LoadedProject.Blocks;
            string presentBlock = string.Empty;
            for (int B = 0; B < BlockCount.Count; B++)
            {
                string windowName = BlockCount[B].Type.Equals("UDFB") ? "UDFLadderForm#" : "LadderForm#";
                if (xm.LoadedScreens.ContainsKey($"{windowName}{BlockCount[B].Name}"))
                {
                    LadderWindow _windowRef = windowName.Equals("LadderForm#") ?
                                            (LadderWindow)xm.LoadedScreens[$"LadderForm#{BlockCount[B].Name}"] : (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{BlockCount[B].Name}"];
                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();

                    for (int k = 0; k < LadderDrawing.LadderDesign.Active.Elements.Count(); k++)
                    {
                        for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[k].Elements.Count; j++)
                        {
                            LadderElement ld = LadderDrawing.LadderDesign.Active.Elements[k].Elements[j];
                            if (ld.customDrawing.GetType().Name.Equals("Contact"))
                            {
                                if (ld.Attributes["LogicalAddress"].ToString().Equals(oldAddress))
                                {
                                    ld.Attributes["LogicalAddress"] = newAddress;
                                }
                            }
                            else if (ld.customDrawing.GetType().Name.Equals("DummyParallelParent") || ld.customDrawing.GetType().Name.Equals("Coil"))
                            {
                                if (ld.Attributes["LogicalAddress"].ToString().Equals(oldAddress))
                                {
                                    ld.Attributes["LogicalAddress"] = newAddress;
                                }
                                else if (ld.Elements.Count > 0)
                                {
                                    CheckInChildElementsAndReplace(ld.Elements, oldAddress, newAddress);
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void CheckInChildElementsAndReplace(LadderElements elements, string oldAddress, string newAddress)
        {
            foreach (var element in elements)
            {
                if (element.customDrawing.GetType().Name.Equals("Contact") || element.customDrawing.GetType().Name.Equals("CoilParallel"))
                {
                    if (element.Attributes["LogicalAddress"].ToString().Equals(oldAddress))
                    {
                        element.Attributes["LogicalAddress"] = newAddress;
                    }
                }
                if (element.Elements.Any())
                {
                    CheckInChildElementsAndReplace(element.Elements, oldAddress, newAddress);
                }
            }
        }
        public static bool CheckPing()
        {
            Ping x = new Ping();
            string ipAddress = string.Empty;
            if (string.IsNullOrEmpty(XMPS.Instance._connectedIPAddress))
            {
                Ethernet ethernet = (Ethernet)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "Ethernet").FirstOrDefault();
                ipAddress = ethernet.EthernetIPAddress.ToString();
            }
            else
            {
                ipAddress = XMPS.Instance._connectedIPAddress;
            }
            PingReply reply = x.Send(IPAddress.Parse(ipAddress));
            if (reply.Status != IPStatus.Success)
            {
                return false;
            }
            return true;
        }

        public static List<string> FillBacnetObjectNames(string text, string instruction = "")
        {
            List<string> objectNames = new List<string>();
            objectNames.Add("-Select Tag Name-");
            switch (text)
            {
                case "AI":
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("0")).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "AO":
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("1")).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "AV":
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("2")).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "BI":
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("3")).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "BO":
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("4")).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "BV":
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("5")).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "MSV":
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.MultistateValues.OrderBy(t => t.InstanceNumber).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "Notification":
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.Notifications.OrderBy(t => t.InstanceNumber).Where(t => Convert.ToInt32(t.InstanceNumber) > 0).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "Device":
                    objectNames.Add(XMPS.Instance.LoadedProject.BacNetIP.Device.ObjectName);
                    return objectNames;
                case "Schedule":
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.Schedules.OrderBy(t => t.InstanceNumber).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "Bool":
                    if (instruction == "R_P_V") objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("3")).Select(t => t.ObjectName).ToList());
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("4")).Select(t => t.ObjectName).ToList());
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.BinaryIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("5")).Select(t => t.ObjectName).ToList());
                    return objectNames;
                case "Real":
                    if (instruction == "R_P_V") objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("0")).Select(t => t.ObjectName).ToList());
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("1")).Select(t => t.ObjectName).ToList());
                    objectNames.AddRange(XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues.Where(t => t.ObjectType.Split(':')[0].Equals("2")).Select(t => t.ObjectName).ToList());
                    return objectNames;
                default:
                    return objectNames;
            }
        }
    }
}
