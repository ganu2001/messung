using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using XMPS2000.Core.App;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.LadderLogic;
using XMPS2000.Core.Serializer;
using File = System.IO.File;

namespace XMPS2000.Core
{
    public class XMPS
    {
        private RecentProjects _recentProjects;
        private SerializeDeserialize<RecentProjects> _sdRecentProjects;
        private SerializeDeserialize<ProjectTemplates> _sdProjectTemplates;
        private ProjectTemplates _projectTemplates;
        private RecentProject _currentProject;
        private const string _RECENTPROJECT_FILE_PATH = @"MessungSystems\XMPS2000\RecentProjects.xml";
        private const string _PROJECTTEMPLATES_FILE_PATH = @"MessungSystems\XMPS2000\ProjectTemplates\Templates.xml";
        private const string _DEVICESIMAGES_FILE_PATH = @"MessungSystems\XMPS2000\Devices";
        private static XMPS instance = null;
        private XMProject _loadedProject = null;
        private string _currentScreen = string.Empty;
        private string _bacnetScreen = string.Empty;
        private bool _showSaveMessage = false;
        private List<string> _screensToNavigate;
        private Dictionary<string, TreeNode> _screensTreeNode;
        private Dictionary<string, Form> _loadedScreens;
        private bool currentProjectModified;
        private bool currentProjectDownloaded;
        public bool onlinemonitoring = false;
        public bool isforced = false;
        public bool isCompilied = false;
        public string DefaultPath = "";
        public bool presentInMain = false;
        private string _utilityVersion = "2.145";
        private int _plcModuleType = 1;
        private int _noOfFilesDownload = 3;
        public bool errorInCompiler = false;
        public string uploadExtPath = "";
        private string _plcModel;
        private string _plcstatus;
        public int _noMcodeFrames;
        public int _noCcodeFrames;
        public int _noBcodeFrames;
        public int _noCnfFrames;
        public string _connectedIPAddress;

        private List<string> _forcedvalues;
        public static List<Tuple<string, string>> _findList;
        public static List<Tuple<string, string>> _findDevicesList;
        public static List<Tuple<string, string>> _findInMainBlock;
        public static List<WatchDogEntries> _entries;
        public Dictionary<string, List<KeyValuePair<int, string>>> UDFBCompileTimeErrors = new Dictionary<string, List<KeyValuePair<int, string>>>();
        public Dictionary<string, List<KeyValuePair<int, string>>> MQTTCompileTimeErrors = new Dictionary<string, List<KeyValuePair<int, string>>>();
        public Dictionary<string, List<KeyValuePair<int, string>>> InstructionCheckErrors = new Dictionary<string, List<KeyValuePair<int, string>>>();
        private static readonly List<string> _plcModels = RemoteModule.List.Select(T => T.Name).Concat(XMPS.Instance.ProjectTemplates.Templates.Select(T => T.PlcName.ToString())).ToList();
        public List<InstructionTypeDeserializer> instructionsList = new List<InstructionTypeDeserializer>();
        public List<MemoryAllocation> MemoryAllocation = new List<MemoryAllocation>();
        // for create sequencial TC Name Count
        public Dictionary<string, Counter> tcNamesCount = new Dictionary<string, Counter>();
        public TreeView instructionTreeNodes = new TreeView();
        public DeviceMemory DeviceMemory = new DeviceMemory();
        public NodeInfo SelectedNode = new NodeInfo();

        private string _tagForCrossreference;


        public string tagForCrossReference { get => _tagForCrossreference; set => _tagForCrossreference = value; }
        public string PlcModel { get => _plcModel; set => _plcModel = value; }

        public List<string> PlcModels { get => _plcModels; }
        public string PlcStatus { get => _plcstatus; set => _plcstatus = value; }
        public List<string> Forcedvalues { get => _forcedvalues; set => _forcedvalues = value; }
        //Create List For Findlist
        public List<Tuple<string, string>> FindList { get => _findList; set => _findList = value; }
        public List<Tuple<string, string>> FindDevicesList { get => _findDevicesList; set => _findDevicesList = value; }
        public List<Tuple<string, string>> FindInMainBlockList { get => _findInMainBlock; set => _findInMainBlock = value; }
        public List<WatchDogEntries> Entries { get => _entries; set => _entries = value; }
        private XMPS()
        {
            this._plcModel = string.Empty;
            Forcedvalues = new List<string> { };
            FindList = new List<Tuple<string, string>> { };
            _connectedIPAddress = "";
            var pathRP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _RECENTPROJECT_FILE_PATH);
            var pathPT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _PROJECTTEMPLATES_FILE_PATH);
            var pathDI = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _DEVICESIMAGES_FILE_PATH);

            string chkPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MessungSystems\XMPS2000");
            if (!Directory.Exists(chkPath)) Directory.CreateDirectory(chkPath);
            chkPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MessungSystems\XMPS2000\XM Projects");
            if (!Directory.Exists(chkPath)) Directory.CreateDirectory(chkPath);
            if (!Directory.Exists(pathRP.Replace("\\RecentProjects.xml", "")))
            {
                string file = System.IO.Directory.GetParent((System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).ToString())).ToString() + "\\RecentProjects.xml";
                if (!File.Exists(file))
                {
                    MessageBox.Show("File not found at " + file + " Will search at " + Application.UserAppDataPath.ToString());
                    file = Application.UserAppDataPath.ToString();
                }
                File.Copy(file, Path.Combine(pathRP.Replace("\\RecentProjects.xml", ""), Path.GetFileName(file)));
                string TempletPath = pathRP.Replace("\\RecentProjects.xml", "\\ProjectTemplates");

                if (!Directory.Exists(TempletPath))
                {
                    Directory.CreateDirectory(TempletPath);
                    CloneDirectory(System.IO.Directory.GetParent((System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).ToString())).ToString() + "\\ProjectTemplates", TempletPath);
                }
                string DevicesPath = pathRP.Replace("\\RecentProjects.xml", "\\Devices");
                if (!Directory.Exists(DevicesPath))
                {
                    Directory.CreateDirectory(DevicesPath);
                    CloneDirectory(System.IO.Directory.GetParent((System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).ToString())).ToString() + "\\Devices", DevicesPath);
                }
                string defaultPath = pathRP.Replace("\\RecentProjects.xml", "\\XM Projects");
                Directory.CreateDirectory(defaultPath);
                DefaultPath = defaultPath;
            }
            _loadedScreens = new Dictionary<string, Form>();
            _sdRecentProjects = new SerializeDeserialize<RecentProjects>();
            _sdProjectTemplates = new SerializeDeserialize<ProjectTemplates>();
            _currentScreen = string.Empty;
            if (!File.Exists(pathRP))
            {
                string file = System.IO.Directory.GetParent((System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).ToString())).ToString() + "\\RecentProjects.xml";
                if (File.Exists(file))
                    File.Copy(file, Path.Combine(pathRP.Replace("\\RecentProjects.xml", ""), Path.GetFileName(file)));
                else
                    File.Copy(file, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "RecentProjects.xml"));
            }
            _recentProjects = _sdRecentProjects.DeserializeData(pathRP);
            _projectTemplates = _sdProjectTemplates.DeserializeData(pathPT);

            _currentProject = new RecentProject();
            _screensToNavigate = new List<string>();
            _screensTreeNode = new Dictionary<string, TreeNode>();
            if (DefaultPath == "") DefaultPath = pathRP.Replace("\\RecentProjects.xml", "\\XM Projects");
        }
        private static void CloneDirectory(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = Path.GetFileName(directory);
                if (!Directory.Exists(Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                }
                CloneDirectory(directory, Path.Combine(dest, dirName));
            }
            foreach (var file in Directory.GetFiles(root))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
            }

        }

        public void SetCurrentProject(RecentProject project)
        {
            _currentProject = project;
        }

        public void LoadCurrentProject()
        {
            if (_currentProject.ProjectPath != string.Empty)
                _loadedProject = ProjectHelper.LoadXMProject(_currentProject.ProjectPath);
            // handling ladder window on loading new project
            List<string> keys = new List<string>(_loadedScreens.Keys);
            PlcModuleType = GetPlcModelCode(LoadedProject.PlcModel);
            // if (_currentScreen.Contains("Ladder"))
            if (keys.Any())
            {
                _currentScreen = string.Empty;
            }
            foreach (var key in keys)
            {
                if (keys.Any())
                {
                    _loadedScreens.Remove(key);
                }
            }
        }
        public string GetHexAddress(string address)
        {
            if (address == null || address == "") return "0";
            bool isbit = address.Contains('.');
            int partaddress = Convert.ToInt16(address.Substring(address.IndexOf(':') + 1, address.Contains('.') ? address.Split(':')[1].IndexOf('.') : address.Split(':')[1].Length));
            int bitvalue = isbit ? Convert.ToInt16(address.Substring(address.IndexOf('.') + 1)) : 0;
            MemoryAllocation mo = MemoryAllocation.Where(m => address.StartsWith(m.Initial) && partaddress >= m.StartAddress && partaddress <= m.Limit && m.IsBit == isbit).FirstOrDefault();
            if (mo is null) return "0";
            if (isbit)
                return (Convert.ToUInt32(mo.StartHexAddress, 16) + (mo.AddLength * (((partaddress * mo.BitValue) + bitvalue) - mo.StartAddress))).ToString("X");
            else
                return (Convert.ToUInt32(mo.StartHexAddress, 16) + (mo.AddLength * (partaddress - mo.StartAddress))).ToString("X");
        }

        public string GetHexAddressForOnlineMonoitoring(string address)
        {
            if (address == null) return "0";
            long addressvalue = 0;
            bool isbit = address.Contains('.');
            int partaddress = Convert.ToInt16(address.Substring(address.IndexOf(':') + 1, address.Contains('.') ? address.Split(':')[1].IndexOf('.') : address.Split(':')[1].Length));
            int bitvalue = isbit ? Convert.ToInt16(address.Substring(address.IndexOf('.') + 1)) : 0;
            MemoryAllocation mo = MemoryAllocation.Where(m => address.StartsWith(m.Initial) && partaddress >= m.StartAddress && partaddress <= m.Limit && m.IsBit == isbit).FirstOrDefault();
            if (mo is null) return "0";
            if (isbit)
                addressvalue = (Int32.Parse(mo.OMStartHexAddress) + (mo.OMAddLength * (((partaddress * mo.BitValue) + bitvalue) - mo.StartAddress)));
            else
                addressvalue = (Int32.Parse(mo.OMStartHexAddress) + (mo.OMAddLength * (partaddress - mo.StartAddress)));
            return addressvalue.ToString("X");
        }
        private int GetPlcModelCode(string plcModel)
        {
            switch (plcModel)
            {
                case "XM-14-DT-HIO":
                    return 3;
                case "XM-14-DT":
                    return 1;
                case "XM-17-ADT":
                    return 2;
                case "XBLD-14":
                    return 4;
                case "XBLD-17":
                    return 5;
                default: return 0;
            }
        }
        public void CreateNewProject(ProjectInfo newProject, bool isSaveAs)
        {
            var template = _projectTemplates.Templates.Where(t => t.PlcName == newProject.PLCModel).FirstOrDefault();
            PlcModuleType = GetPlcModelCode(newProject.PLCModel);
            if (template != null)
            {
                RecentProject project = new RecentProject();
                if (isSaveAs)
                {
                    var tempProject = ProjectHelper.LoadXMProject(_currentProject.ProjectPath);
                    project = ProjectHelper.SaveAsProject(newProject.ProjectPath, template, tempProject);
                }
                else
                {
                    project = ProjectHelper.CreateNewProject(newProject.ProjectPath, template);
                }
                UpdateRecentProjects(project);
                SetCurrentProject(project);
            }
        }
        public void MarkProjectModified(bool val)
        {
            this.currentProjectModified = val;
            if (val && !_currentProject.ProjectName.Contains("*"))
            {
                _currentProject.ProjectName = _currentProject.ProjectName + "*";
            }
            else
            {
                _currentProject.ProjectName = _currentProject.ProjectName.Replace("*", "");
            }
            if (val) MarkProjectDownloaded(false);
        }

        public void MarkProjectDownloaded(bool val)
        {
            this.currentProjectDownloaded = val;
        }

        public bool IsProjectModified()
        {
            return currentProjectModified;
        }
        public bool IsProjectDownloaded()
        {
            return currentProjectDownloaded;
        }
        public void SaveCurrentProject()
        {
            ProjectHelper.SaveXMProject(this.LoadedProject);
            MarkProjectModified(false);
        }


        public int DownloadTFTPMcodeFile(string ipaddress)
        {

            string filepath = this.LoadedProject.ProjectPath;
            string filename = filepath.Replace(filepath.Split('\\').Last(), "McodeVersion.txt");
            int _mCnt = ProjectHelper.DownloadTFTPFile(filename, ipaddress);
            MarkProjectDownloaded(true);
            return _mCnt;
        }
        public int DownloadTFTPCcodeFile(string ipaddress)
        {
            string filepath = this.LoadedProject.ProjectPath;
            string filename = filepath.Replace(filepath.Split('\\').Last(), "CcodeVersion.txt");
            int _ccnt = ProjectHelper.DownloadTFTPFile(filename, ipaddress);
            MarkProjectDownloaded(true);
            return _ccnt;
        }
        public int DownloadTFTPMqttFile(string ipaddress)
        {
            string filepath = this.LoadedProject.ProjectPath;
            string filename = filepath.Replace(filepath.Split('\\').Last(), "MQTT_CNF.txt");
            int cnfcnt = ProjectHelper.DownloadTFTPFile(filename, ipaddress);
            MarkProjectDownloaded(true);
            return cnfcnt;
        }
        public void DownloadTFTPFile(string ipaddress)
        {
            //// Download Mcode.text file
            string filepath = this.LoadedProject.ProjectPath;
            string filename = filepath.Replace(filepath.Split('\\').Last(), "McodeVersion.txt");
            ProjectHelper.DownloadTFTPFile(filename, ipaddress);

            Thread.Sleep(15000);

            //// Download Ccode.text
            filepath = this.LoadedProject.ProjectPath;
            filename = filepath.Replace(filepath.Split('\\').Last(), "CcodeVersion.txt");
            ProjectHelper.DownloadTFTPFile(filename, ipaddress);

            Thread.Sleep(15000);

            //// Download MQTT_CNF.text
            filepath = this.LoadedProject.ProjectPath;
            filename = filepath.Replace(filepath.Split('\\').Last(), "MQTT_CNF.txt");
            ProjectHelper.DownloadTFTPFile(filename, ipaddress);

            MarkProjectDownloaded(true);
        }

        private void ZippingFile(string filename, string filepath)
        {
            //to store the value of folderapth   
            string FolderPathToZip = filepath + "project";
            if (Directory.Exists(FolderPathToZip))
            {
                foreach (string file in Directory.GetFiles(FolderPathToZip))
                    File.Delete(file);
                Directory.Delete(FolderPathToZip);
            }
            Directory.CreateDirectory(FolderPathToZip);
            File.Copy(Path.Combine(filepath, filename.Trim() + ".xmprj"), Path.Combine(FolderPathToZip, "project.xmprj"));
            //To create unique file name with date and time with nanoseconds.  
            string ZipFileName = filepath + "project.zip";
            try
            {
                //To check whether D:\Backup folder exists or not.  
                //If not exists this will create a BACKUP folder.  
                if (Directory.Exists(filepath)) { }
                else
                {
                    Directory.CreateDirectory(filepath);
                }
                //Delete the zip file if already exists
                if (File.Exists(ZipFileName))
                    File.Delete(ZipFileName);
                //TO create a zip file.  
                ZipFile.CreateFromDirectory(FolderPathToZip, ZipFileName);
            }
            catch (Exception)
            {
                //If system throw any exception message box will display "SOME ERROR"  
                MessageBox.Show("Some Error");
            }
            //Display successfully created message to the user.  
        }
        public void UpdateRecentProjects(RecentProject recentProject)
        {
            var pathRP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _RECENTPROJECT_FILE_PATH);
            _recentProjects.Projects.Add(recentProject);
            var rpFile = _sdRecentProjects.SerializeData(_recentProjects);
            File.Delete(pathRP);
            File.WriteAllText(pathRP, rpFile, Encoding.Unicode);
            _recentProjects = _sdRecentProjects.DeserializeData(pathRP);
        }

        public static XMPS Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XMPS();
                }
                return instance;
            }
        }

        public RecentProjects RecentProjects { get => _recentProjects; set => _recentProjects = value; }
        public RecentProject CurrentProjectData { get => _currentProject; set { } }
        public XMProject LoadedProject { get => _loadedProject; set { } }
        public string CurrentScreen { get => _currentScreen; set => _currentScreen = value; }
        public string BacNetCurrentScreen { get => _bacnetScreen; set => _bacnetScreen = value; }
        public bool ShowSaveMessage { get => _showSaveMessage; set => _showSaveMessage = value; }
        public Dictionary<string, Form> LoadedScreens { get => _loadedScreens; set => _loadedScreens = value; }
        public List<string> ScreensToNavigate { get => _screensToNavigate; set => _screensToNavigate = value; }
        public Dictionary<string, TreeNode> ScreensTreeNode { get => _screensTreeNode; set => _screensTreeNode = value; }
        public ProjectTemplates ProjectTemplates { get => _projectTemplates; set => _projectTemplates = value; }
        public string UtilityVersion { get => _utilityVersion; set => _utilityVersion = value; }
        public int PlcModuleType { get => _plcModuleType; set => _plcModuleType = value; }
        public int NoOfFilesDownload { get => _noOfFilesDownload; set => _noOfFilesDownload = value; }
        public void UploadTFTPSourceFile(string ipaddress)
        {

            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MessungSystems\XMPS2000\XM Projects\tempfolder");  //this.LoadedProject.ProjectPath;
            if (Directory.Exists(filepath))
                DeleteFolderAndContents(filepath);
            Directory.CreateDirectory(filepath);
            string filename = filepath + "\\project.zip"; // filepath.Replace(filepath.Split('\\').Last(), "project.zip");
            ProjectHelper.UploadTFTPFile(filename, ipaddress);
            ZipFile.ExtractToDirectory(filename, filepath);


        }


        /// <summary>
        /// Checks if folder exists, deletes all files inside, and then deletes the folder
        /// </summary>
        /// <param name="folderPath">Path to the folder to be cleaned up</param>
        /// <returns>True if operation was successful, false otherwise</returns>
        public static bool DeleteFolderAndContents(string folderPath)
        {
            try
            {
                // Check if folder exists
                if (!Directory.Exists(folderPath))
                    return false;
                // Check if folder has any files
                string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
                string[] directories = Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories);
                // Delete the entire directory tree (files and subdirectories)
                Directory.Delete(folderPath, true);
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Access denied: {ex.Message}", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show($"Directory not found: {ex.Message}", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (IOException ex)
            {
                MessageBox.Show($"IO error occurred: {ex.Message}", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Zip and download file to PLC
        /// </summary>
        /// <param name="ipaddress"></param>
        public void DownloadTFTPProjectFile(string ipaddress)
        {
            // Download .xmprj file
            string filepath = this.LoadedProject.ProjectPath;
            string filename = filepath.Split('\\').Last().Replace(".xmprj", "");
            filepath = filepath.Replace(filepath.Split('\\').Last(), "");
            ZippingFile(filename, filepath);
            ProjectHelper.DownloadTFTPFile(filepath + "project.zip", ipaddress);
            MarkProjectDownloaded(true);
        }
        public string GetTopicName(string Type, int TopicId)
        {
            XMPS xm = XMPS.Instance;
            if (Type == "Publish")
            {
                var pubdata = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>();
                return pubdata.Where(r => r.keyvalue == TopicId).Select(r => r.topic).FirstOrDefault();
            }
            else
            {
                var subdata = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>();
                return subdata.Where(r => r.key == TopicId).Select(r => r.topic).FirstOrDefault();

            }
        }

        public int DownloadTFTPBCodeFile(string ipaddress)
        {
            string filepath = this.LoadedProject.ProjectPath;
            string filename = filepath.Replace(filepath.Split('\\').Last(), "Bcode.txt");
            int bCodecnt = ProjectHelper.DownloadTFTPFile(filename, ipaddress);
            MarkProjectDownloaded(true);
            return bCodecnt;
        }
    }
}
