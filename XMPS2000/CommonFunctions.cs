using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Types;
using static XMPS2000.ApplicationMemory;

namespace XMPS2000
{
    public class CommonFunctions
    {
        static XMPS xm;
        static string UpdateRetAdd = "";
        static int realadded = 0;

        /// <summary>
        /// Provide Padding of 000 to Retentive Address counter
        /// </summary>
        /// <param name="str"></param> Number to be padded
        /// <param name="len"></param> Required amount of 0's in the format like for 001 we need to provide 3
        /// <returns></returns>
        public static string padding(string str, int len)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str == "")
                {
                    if (len == 2)
                    {
                        str = "00";
                    }
                    else
                        str = "000";

                }
                else
                {
                    if (len == 2)
                    {
                        str = int.Parse(str).ToString("00");
                    }
                    else
                        str = int.Parse(str).ToString("000");
                }
            }
            return str;
        }
        public static string GetExpantionErrorMessage(List<Tuple<string, string>> errorStatus)
        {
            try
            {
                // Return empty string for null input
                if (errorStatus == null)
                {
                    return string.Empty;
                }

                StringBuilder errorMessage = new StringBuilder();

                // Safe access to xm and its properties
                if (XMPS.Instance.LoadedProject?.Tags == null)
                {
                    return string.Empty;
                }

                try
                {
                    // Use safe sorting and conversion to list
                    List<Tuple<string, string>> sortedErrors;
                    try
                    {
                        sortedErrors = errorStatus.OrderBy(t => t?.Item1 ?? string.Empty).ToList();
                    }
                    catch (Exception)
                    {
                        // Fallback to original list if sorting fails
                        sortedErrors = new List<Tuple<string, string>>(errorStatus);
                    }

                    foreach (Tuple<string, string> tplTag in sortedErrors)
                    {
                        // Skip null entries
                        if (tplTag == null || string.IsNullOrEmpty(tplTag.Item1))
                        {
                            continue;
                        }

                        try
                        {
                            // Safely find the tag
                            XMIOConfig currentTag = XMPS.Instance.LoadedProject.Tags
                                .Where(T => T != null && T.LogicalAddress == tplTag.Item1)
                                .FirstOrDefault();

                            if (currentTag == null || currentTag.EnumValues == null)
                            {
                                continue;
                            }

                            // Safely get enum value with null check on Item2
                            //string tagValue = tplTag.Item2 ?? string.Empty;
                            if (XMPS.Instance.LoadedProject.ExpansionErrorType == "Old")
                            {
                                // Safely get enum value with null check on Item2
                                string tagValue = tplTag.Item2 ?? string.Empty;
                                // Safely check enum values
                                string enumName = currentTag.EnumValues
                                     .Where(T => T != null && T.Value != null && T.Value.ToString().Equals(tagValue))
                                     .Select(T => T.ValueName)
                                     .FirstOrDefault();
                                // Add to error message if enum name is valid and not OK
                                if (!string.IsNullOrEmpty(enumName) && enumName != "OK")
                                {
                                    errorMessage.Append("{ " + currentTag.Tag + " : " + enumName + "}");
                                }
                            }
                            else
                            {
                                string tagValue = Int32.TryParse(tplTag.Item2, out _) ? CommonFunctions.GetBinaryValue(Convert.ToInt32(tplTag.Item2)).Substring(0, 5) : string.Empty;
                                if (tplTag.Item2.ToString() != "0")
                                {
                                    string errorText = CommonFunctions.GetPositionsOfOnes(tagValue);
                                    // Add to error message with expansion name and number of device
                                    if (!string.IsNullOrEmpty(errorText) && errorText != "OK")
                                    {
                                        errorMessage.Append("{ " + currentTag.Tag + " in following expansions : " + errorText + "}");
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // Skip this tag if any exception occurs
                            continue;
                        }
                    }

                    return errorMessage.ToString();
                }
                catch (Exception)
                {
                    // Return empty string if any general exception occurs
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                // Global exception handler - silent failure
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the message and decode it in words format to show in error
        /// </summary>
        /// <param name="fiveDigitString"></param>
        /// <returns>String of error message to be shown on ribbon</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetPositionsOfOnes(string fiveDigitString)
        {
            // Validate input
            if (string.IsNullOrEmpty(fiveDigitString) || fiveDigitString.Length != 5)
            {
                throw new ArgumentException("Input must be a 5-digit string");
            }

            // Check if all characters are digits
            if (!fiveDigitString.All(char.IsDigit))
            {
                throw new ArgumentException("Input must contain only digits");
            }

            // Array to convert position index to ordinal words
            string[] ordinalNumbers = { "EXPN1", "EXPN2", "EXPN3", "EXPN4", "EXPN5" };

            List<string> positions = new List<string>();

            // Find positions where digit is '1'
            for (int i = 0; i < fiveDigitString.Length; i++)
            {
                if (fiveDigitString[i] == '1')
                {
                    positions.Add(ordinalNumbers[i]);
                }
            }

            // Return the result based on number of positions found
            if (positions.Count == 0)
            {
                return "No ones found";
            }
            else if (positions.Count == 1)
            {
                return positions[0];
            }
            else if (positions.Count == 2)
            {
                return $"{positions[0]} , {positions[1]}";
            }
            else
            {
                // For more than 2 positions, join with commas and 'and' before the last item
                return string.Join(", ", positions.Take(positions.Count - 1)) + " and " + positions.Last();
            }
        }
        public static string GetBinaryValue(int value)
        {
            // Convert to binary string and pad to 8 bits for demonstration
            string binary = Convert.ToString(value, 2).PadLeft(8, '0');
            // Insert a space every 4 bits for grouping (optional)
            return string.Join("", Enumerable.Range(0, binary.Length / 4)
                .Select(i => binary.Substring(i * 4, 4)));
        }
        /// <summary>
        /// Get Retentive Address for Logical Address
        /// </summary>
        /// <param name="LogicalAddress"></param>Logical Address entered by the user
        /// <returns></returns> Currusponding Retentive Address
        /// <crosschk></crosschk> This variable is used to varify if we are executing for the purpose of checking or for getting actual address
        public static string GetRetentiveAddress(string LogicalAddress, string datatype, int crosschk = 0)
        {
            xm = XMPS.Instance;
            String RetAddress;
            if (LogicalAddress.StartsWith("F2") || LogicalAddress.Contains("."))
            {
                RetAddress = "X8";
            }
            else if ((LogicalAddress.StartsWith("W4") || LogicalAddress.StartsWith("P5")) && (datatype == "Double Word" || datatype == "Real" || datatype == "DINT" || datatype == "UDINT"))
            {
                RetAddress = "Z10";
            }
            else
            {
                RetAddress = "Y9";
            }
            if (crosschk == 1)
            {
                RetAddress = RetAddress + ":" + padding(GetMaxRetAdd(RetAddress).ToString(), 3);
                return RetAddress;
            }
            if (RetAddress.Contains("Z10") ? GetMaxRetAdd(RetAddress) > 255 : GetMaxRetAdd(RetAddress) > 999 && !LogicalAddress.StartsWith("P5"))
            {
                return ("Retentive Address Going Beyond Range, Can't Add New Address ");
            }
            if (RetAddress.Contains("Z10") ? GetMaxRetAdd(RetAddress) > 255 : GetMaxRetAdd(RetAddress) > 999 && LogicalAddress.StartsWith("P5") && realadded == 0)
            {
                return ("Retentive Address Going Beyond Range, Can't Add New Address ");
            }
            RetAddress = RetAddress + ":" + padding(GetMaxRetAdd(RetAddress).ToString(), 3);
            if (!CheckIfRetentiveAddressExists(RetAddress) || RetAddress == UpdateRetAdd)
            {

                int next = 1;
            checkagain: RetAddress = RetAddress.Substring(0, 2).ToString() + ":" + padding((Convert.ToInt32(GetMaxRetAdd(RetAddress.Substring(0, 2))) + next).ToString(), 3);
                if (Convert.ToInt32(RetAddress.Substring(3, 3).ToString()) > 999 && !LogicalAddress.StartsWith("P5"))
                {
                    return ("Retentive Address Going Beyond Range, Can't Add New Address ");

                }
                if (Convert.ToInt32(RetAddress.Substring(3, 3).ToString()) > 255 && LogicalAddress.StartsWith("P5") && realadded == 0)
                {
                    return ("Retentive Address Going Beyond Range, Can't Add New Address ");
                }
                if (!CheckIfRetentiveAddressExists(RetAddress) || RetAddress == UpdateRetAdd)
                {
                    ++next;
                    goto checkagain;

                }
                else
                {
                    return RetAddress;
                }

            }
            else
            {
                return RetAddress;
            }
        }

        public static bool CheckIfRetentiveAddressExists(string RetentiveAddress)
        {
            try
            {
                var ExistingRetentiveAdd = (XMIOConfig)xm.LoadedProject.Tags.Where(d => d.RetentiveAddress == RetentiveAddress).FirstOrDefault();
                if (ExistingRetentiveAdd != null)
                {
                    return false;
                }
                else return true;

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        internal static void UpdatePrecedingRetentiveAddresses(string retentiveAddress, List<XMIOConfig> exNodeTags = null)
        {
            if (!retentiveAddress.Contains(":"))
                return;

            xm = XMPS.Instance;
            int newRAddress;
            string GeneratedRA;
            List<XMIOConfig> Retentive = new List<XMIOConfig>();
            int countToRemove = exNodeTags != null ? exNodeTags.Count : 1;
            int startSubstringIndex;
            if (retentiveAddress.IndexOf(":") == 2)
            {
                startSubstringIndex = 3;
            }
            else
            {
                startSubstringIndex = 4;
            }
            Retentive = xm.LoadedProject.Tags
            .Where(d =>
                !string.IsNullOrEmpty(d.RetentiveAddress) &&
                d.RetentiveAddress.StartsWith(retentiveAddress.Substring(0, startSubstringIndex)) &&
                Convert.ToInt32(d.RetentiveAddress.Substring(startSubstringIndex)) > Convert.ToInt32(retentiveAddress.Substring(startSubstringIndex))
            )
            .OrderBy(x => x.Key)
            .ToList();

            foreach (var Tag in Retentive)
            {
                newRAddress = Convert.ToInt32(Tag.RetentiveAddress.Substring(startSubstringIndex)) - countToRemove;
                GeneratedRA = retentiveAddress.Substring(0, startSubstringIndex) + padding(newRAddress.ToString(), 3);
                Tag.RetentiveAddress = GeneratedRA;
            }

        }

        public static int GetMaxRetAdd(string RetentiveAddress)
        {
            try
            {
                xm = XMPS.Instance;
                int count;
                //
                var Tags = xm.LoadedProject.Tags.Where(d => d.RetentiveAddress != null && d.RetentiveAddress.StartsWith(RetentiveAddress)).ToList();
                count = Tags.Count > 0 ? Tags.Max(d => Convert.ToInt32(Regex.Replace(d.RetentiveAddress.Substring(2), "[^0-9]", ""))) + 1 : 0;
                return count;

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

        }

        public static void UpdateTagNames(string logicalAddress, string tag)
        {
            var modBUSRTUMaster = (MODBUSRTUMaster)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
            if (modBUSRTUMaster != null)
            {
                var slaves = modBUSRTUMaster.Slaves.Where(m => m.Variable == logicalAddress);
                foreach (var vslave in slaves)
                {
                    vslave.Tag = tag;
                }
            }
            var modBUSTCPServer = (MODBUSTCPServer)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
            var Requests = modBUSTCPServer.Requests.Where(m => m.Variable == logicalAddress);
            foreach (var vslave in Requests)
            {
                vslave.Tag = tag;
            }
            var modBUSTCPClient = (MODBUSTCPClient)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
            var slavesTcpClient = modBUSTCPClient.Slaves.Where(m => m.Variable == logicalAddress);
            foreach (var vslave in slavesTcpClient)
            {
                vslave.Tag = tag;
            }
            var moDBUSRTUSlaves = (MODBUSRTUSlaves)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
            if (moDBUSRTUSlaves != null)
            {
                var slavesRTU = moDBUSRTUSlaves.Slaves.Where(m => m.Variable == logicalAddress);
                foreach (var vslave in slavesRTU)
                {
                    vslave.Tag = tag;
                }
            }

        }

        public static List<XMIOConfig> GetSystemTagList(string model)
        {
            List<XMIOConfig> systemtags = new List<XMIOConfig>();
            string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\ProjectTemplates\" + model + "\\" + model + ".plc");
            // Load XML file
            XDocument xdoc = XDocument.Load(filePath);
            // Iterate through root elements
            foreach (var systemobjects in xdoc.Root.Elements("SystemTagsList"))
            {
                foreach (var systemtag in systemobjects.Elements("Tag"))
                {
                    XMIOConfig DefaultTag = new XMIOConfig();
                    DefaultTag.Tag = systemtag.Elements("DefaultTags").FirstOrDefault().Value.ToString();
                    DefaultTag.LogicalAddress = systemtag.Elements("StatusRegister").FirstOrDefault().Value.ToString();
                    DefaultTag.Label = systemtag.Elements("Datatype").FirstOrDefault().Value.ToString();
                    DefaultTag.Key = XMPS.Instance.LoadedProject.Tags.Count() + 1;
                    DefaultTag.Type = IOType.DataType;
                    DefaultTag.IoList = IOListType.Default;
                    DefaultTag.InitialValue = systemtag.Elements("InitialValue").FirstOrDefault().Value.ToString();
                    DefaultTag.Editable = systemtag.Elements("Editable").FirstOrDefault().Value.ToString().Equals("true") ? true : false;
                    DefaultTag.ReadOnly = systemtag.Elements("ReadOnly").FirstOrDefault() != null ? systemtag.Elements("ReadOnly").FirstOrDefault().Value.ToString().Equals("true") ? true : false : false;
                    int i = 0;
                //EnumValue1
                nextenumval:
                    var value = systemtag.Elements($"EnumValue{i + 1}").FirstOrDefault();
                    if (value != null)
                    {
                        DefaultTag.EnumValues.Add(ConverttoEnum(value.Value));
                        i++;
                        goto nextenumval;
                    }
                    systemtags.Add(DefaultTag);
                    //xm.LoadedProject.Tags.Add(DefaultTag);
                }
            }
            return systemtags;
        }

        public static EnumValue ConverttoEnum(string actEnumValue)
        {
            EnumValue enumValue = new EnumValue();
            string[] parts = actEnumValue.Split(',');
            enumValue.Value = Convert.ToInt32(parts[0]);
            enumValue.ValueName = parts[1];
            return enumValue;
        }


        public static double CalculateTotalAddressMemory()
        {
            try
            {
                double totalAddressMemory = 0;
                var project = XMPS.Instance.LoadedProject;
                if (project == null)
                {
                    throw new InvalidOperationException("Project not loaded");
                }
                // Calculate and add details for different tag types
                List<MemoryAllocation> memoryValues = XMPS.Instance.MemoryAllocation;
                totalAddressMemory += AddTagTypeMemoryUsage("Q0", memoryValues.Where(v => v.Initial == "Q0").Select(v => v.BytesRequired).FirstOrDefault());
                totalAddressMemory += AddTagTypeMemoryUsage("I1", memoryValues.Where(v => v.Initial == "I1").Select(v => v.BytesRequired).FirstOrDefault());
                totalAddressMemory += AddTagTypeMemoryUsage("F2", memoryValues.Where(v => v.Initial == "F2").Select(v => v.BytesRequired).FirstOrDefault());
                totalAddressMemory += AddTagTypeMemoryUsage("W4", memoryValues.Where(v => v.Initial == "W4").Select(v => v.BytesRequired).FirstOrDefault());
                totalAddressMemory += AddTagTypeMemoryUsage("W4", memoryValues.Where(v => v.Initial == "P5").Select(v => v.BytesRequired).FirstOrDefault(),true);
                totalAddressMemory += AddTagTypeMemoryUsage("P5", memoryValues.Where(v => v.Initial == "P5").Select(v => v.BytesRequired).FirstOrDefault());
                totalAddressMemory += AddTagTypeMemoryUsage("T6", memoryValues.Where(v => v.Initial == "T6").Select(v => v.BytesRequired).FirstOrDefault());
                totalAddressMemory += AddTagTypeMemoryUsage("C7", memoryValues.Where(v => v.Initial == "C7").Select(v => v.BytesRequired).FirstOrDefault());
                return totalAddressMemory;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error calculating total address memory", ex);
            }
        }

        public static double AddTagTypeMemoryUsage(string prefix, double bytesPerTag, bool isdouble = false)
        {
            try
            {
                if (string.IsNullOrEmpty(prefix))
                {
                    throw new ArgumentException("Tag prefix cannot be null or empty");
                }

                var project = XMPS.Instance?.LoadedProject;
                if (project?.Tags == null)
                {
                    return 0; // Return 0 if no tags available
                }
                int count;
                if (prefix == "W4" && isdouble)
                    count = project.Tags
                                 .Where(t => t != null &&
                                        t.LogicalAddress != null &&
                                        t.LogicalAddress.StartsWith(prefix) &&
                                       t.LogicalAddress.Length >= 6 && (t.Label == "DINT" || t.Label == "Double Word")).Select(t => t.LogicalAddress.Split('.').First()).Distinct()
                                 .Count();
                else
                    count = project.Tags
                         .Where(t => t != null &&
                                t.LogicalAddress != null &&
                                t.LogicalAddress.StartsWith(prefix) &&
                               t.LogicalAddress.Length >= 6 && t.Label != "DINT" && t.Label != "Double Word").Select(t => t.LogicalAddress.Split('.').First()).Distinct()
                         .Count();

                double memoryUsage = count * bytesPerTag;
                //_appDetails.Add(new AppDetails($"{prefix}:000 data", memoryUsage));
                return memoryUsage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating memory for tag type {prefix}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Return 0 on error
            }
        }

        public static double CalculateRetentiveMemory()
        {
            try
            {
                double totalRetentiveMemory = 0;
                var project = XMPS.Instance?.LoadedProject;

                if (project?.Tags == null)
                {
                    return 0; // Return 0 if no tags available
                }
                List<MemoryAllocation> memoryValues = XMPS.Instance.MemoryAllocation;
                // Calculate memory for different retentive tag types
                totalRetentiveMemory += project.Tags
                    .Count(t => t != null &&
                           t.RetentiveAddress != null &&
                           t.RetentiveAddress.StartsWith("X")) * ((double)memoryValues.Where(v => v.Initial.StartsWith("X")).Select(v => v.BytesRequired).FirstOrDefault());

                totalRetentiveMemory += project.Tags
                    .Count(t => t != null &&
                           t.RetentiveAddress != null &&
                           t.RetentiveAddress.StartsWith("Y")) * ((double)memoryValues.Where(v => v.Initial.StartsWith("Y")).Select(v => v.BytesRequired).FirstOrDefault());

                totalRetentiveMemory += project.Tags
                    .Count(t => t != null &&
                           t.RetentiveAddress != null &&
                           t.RetentiveAddress.StartsWith("Z")) * ((double)memoryValues.Where(v => v.Initial.StartsWith("Z")).Select(v => v.BytesRequired).FirstOrDefault());

                return totalRetentiveMemory;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error calculating retentive memory", ex);
            }
        }

        public static bool IsRealValue(string logicalAddress)
        {
            string datatype = XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == logicalAddress.ToString() && L.Type == IOType.DataType).Select(T => T.Label).FirstOrDefault();
            if (datatype == null)
                datatype = logicalAddress.ToString().Contains(".") ? "Bool" : XMPS.Instance.LoadedProject.CPUDatatype;
            if (datatype == "Real")
                return true;
            else
                return false;
        }
        public static string GetEasyConnection(string connectedIp)
        {
            if (connectedIp != "")
                return "Network Error";
            else
                return "Unable to connect, Select the device from Easy Connection & Retry...";
        }
        public static double CalculateProgramMemory()
        {
            // Read application data from CSV
            string appCsvPath = GetApplicationCsvPath();

            // Validate CSV file exists
            if (!File.Exists(appCsvPath))
            {
                throw new FileNotFoundException($"Application CSV file not found at path: {appCsvPath}");
            }

            DataSet appDataSet = ReadCsvFile(appCsvPath);

            if (appDataSet?.Tables == null || appDataSet.Tables.Count == 0)
            {
                throw new ArgumentException("Invalid application dataset");
            }
            return appDataSet.Tables[0].Rows.Count * 68;
        }
        private static string GetApplicationCsvPath()
        {
            try
            {
                string projectPath = XMPS.Instance.LoadedProject.ProjectPath;
                string projectName = XMPS.Instance.LoadedProject.ProjectName;

                if (string.IsNullOrEmpty(projectPath))
                {
                    throw new InvalidOperationException("Project path is empty");
                }

                if (string.IsNullOrEmpty(projectName))
                {
                    throw new InvalidOperationException("Project name is empty");
                }

                string directoryPath = Path.GetDirectoryName(projectPath);
                if (string.IsNullOrEmpty(directoryPath))
                {
                    throw new InvalidOperationException($"Could not extract directory from path: {projectPath}");
                }

                string fileName = projectPath.Replace(".xmprj", "") + "App.csv";

                return Path.Combine(directoryPath, fileName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to construct application CSV path", ex);
            }
        }

        private static DataSet ReadCsvFile(string filePath)
        {
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileName(filePath);

                if (string.IsNullOrEmpty(directoryPath) || string.IsNullOrEmpty(fileName))
                {
                    throw new ArgumentException($"Invalid file path: {filePath}");
                }
                //directoryPath = Path.Combine(directoryPath , fileName);
                string connectionString = $"Provider=Microsoft.Jet.OleDb.4.0; Data Source={directoryPath}; Extended Properties=\"Text;HDR=YES;FMT=Delimited\"";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM [{fileName}]", connection))
                        {
                            DataSet dataSet = new DataSet("Temp");
                            adapter.Fill(dataSet);

                            if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                            {
                                //throw new DataException("CSV file contains no data");
                            }

                            return dataSet;
                        }
                    }
                    catch (OleDbException dbEx)
                    {
                        throw new DataException($"Database error reading CSV file: {dbEx.Message}", dbEx);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex) when (!(ex is DataException))
            {
                throw new ApplicationException($"Error reading CSV file {filePath}", ex);
            }
        }
    }

}
