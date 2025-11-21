using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using XMPS2000.Core;

namespace XMPS2000
{
    public partial class ImportLibraryForm : Form
    {
        private string libraryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MessungSystems", "XMPS2000", "Library");
        public ImportLibraryForm()
        {
            InitializeComponent();
            this.Load += ImportLibraryForm_Load;
            btnImport.Click += BtnImport_Click;
            btnClose.Click += BtnClose_Click;
        }
        private void ImportLibraryForm_Load(object sender, EventArgs e)
        {
            LoadAvailableLibraries();
        }
        private void LoadAvailableLibraries()
        {
            dgvLibraries.Rows.Clear();
            dgvLibraries.ReadOnly = true;
            dgvLibraries.AllowUserToResizeColumns = false;
            dgvLibraries.AllowUserToResizeRows = false;
            if (XMPS.Instance.LoadedProject == null)
            {
                UpdateStatus("No project loaded. Please open or create a project first.");
                return;
            }
            string modelFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD") ?"XBLDLibraries": "XMLibraries";
            string fullLibraryPath = Path.Combine(libraryPath, modelFolder);
            if (!Directory.Exists(fullLibraryPath))Directory.CreateDirectory(fullLibraryPath);
            string[] csvFiles = Directory.GetFiles(fullLibraryPath, "*.csv");
            var udfbFiles = new List<string>();
            foreach (string file in csvFiles)
            {
                try
                {
                    var firstLines = File.ReadLines(file).Take(15);
                    bool isUdfb = firstLines.Any(l => l.Trim().Equals("Block Type :- UDFB Block", StringComparison.OrdinalIgnoreCase));

                    if (!isUdfb)
                        continue; 
                    udfbFiles.Add(file);
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (fileName.EndsWith("Logic", StringComparison.OrdinalIgnoreCase))
                        fileName = fileName.Substring(0, fileName.Length - "Logic".Length).Trim();

                    dgvLibraries.Rows.Add(fileName);
                }
                catch
                {
                    // Ignore any unreadable file
                }
            }

            if (udfbFiles.Count == 0)
            {
                dgvLibraries.BackgroundColor = Color.White;
                dgvLibraries.DefaultCellStyle.BackColor = Color.White;
                UpdateStatus("No libraries found in directory");
                return;
            }

            UpdateStatus($"{udfbFiles.Count} UDFB library files available");
        }
        private void BtnImport_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "CSV Library Files (*.csv)|*.csv";
            openFileDialog.Title = "Select a UDFB CSV Library File";
            if (XMPS.Instance.LoadedProject == null)
            {
                return;
            }
            string modelFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD")? "XBLDLibraries": "XMLibraries";
            string fullLibraryPath = Path.Combine(libraryPath, modelFolder);
            openFileDialog.InitialDirectory = fullLibraryPath;
            openFileDialog.CheckFileExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.ValidateNames = true;

            var beforeImport = Directory.GetFiles(fullLibraryPath, "*.csv").Select(Path.GetFileName).ToList();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedFile = openFileDialog.FileName;
                    string[] firstLines = File.ReadLines(selectedFile).Take(15).ToArray();

                    bool isUdfb = firstLines.Any(line => line.Trim().Equals("Block Type :- UDFB Block", StringComparison.OrdinalIgnoreCase));
                    bool isLogic = firstLines.Any(line => line.Trim().Equals("Block Type :- Logic Block", StringComparison.OrdinalIgnoreCase));

                    if (!isUdfb || isLogic)
                    {
                        MessageBox.Show("Only UDFB library CSV files can be imported.\nThis file is not a valid UDFB file.",
                            "Invalid Library File",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string destFile = Path.Combine(fullLibraryPath, Path.GetFileName(selectedFile));
                    string[] newLines = File.ReadAllLines(selectedFile);
                    List<string> newContentLines = new List<string>(newLines);
                    bool hasChanges = true;
                    if (File.Exists(destFile))
                    {
                        string[] oldLines = File.ReadAllLines(destFile);
                        List<string> oldContentLines = new List<string>(oldLines);
                        hasChanges = !newContentLines.SequenceEqual(oldContentLines, StringComparer.Ordinal);

                        DialogResult result = MessageBox.Show($"Library \"{Path.GetFileName(destFile)}\" already exists.\nDo you want to replace it?","Confirm Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            FileSecurity fileSecurity = File.GetAccessControl(destFile);
                            fileSecurity.AddAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));
                            File.SetAccessControl(destFile, fileSecurity);

                            if (File.Exists(destFile))
                            {
                                FileAttributes attributes = File.GetAttributes(destFile);
                                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                                {
                                    File.SetAttributes(destFile, attributes & ~FileAttributes.ReadOnly);
                                }
                            }
                            File.WriteAllLines(destFile, newContentLines);
                        }
                        else if (result == DialogResult.No)
                        {
                            UpdateStatus("Import cancelled.");
                            return;
                        }
                    }
                    else
                    {
                        File.Copy(selectedFile, destFile);
                    }
                    LoadAvailableLibraries();
                    var afterImport = Directory.GetFiles(fullLibraryPath, "*.csv").Select(Path.GetFileName).ToList();
                    string newlyAdded = afterImport.Except(beforeImport, StringComparer.OrdinalIgnoreCase).FirstOrDefault();

                    if (!string.IsNullOrEmpty(newlyAdded))
                    {
                        string name = Path.GetFileNameWithoutExtension(newlyAdded);
                        if (name.EndsWith("Logic", StringComparison.OrdinalIgnoreCase))
                            name = name.Substring(0, name.Length - "Logic".Length).Trim();
                        UpdateStatus($"{name} Library: Imported successfully");
                    }
                    else
                    {
                        UpdateStatus("Library replaced successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing file:\n{ex.Message}", "Import Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private Timer statusTimer = new Timer();
        private void UpdateStatus(string message)
        {
            Label statusLabel = this.Controls.Find("lblStatus", true)[0] as Label;
            if (statusLabel != null)
            {
                statusLabel.Text = message;

                statusLabel.Font = new Font(statusLabel.Font.FontFamily, 9, FontStyle.Bold);

                if (message.Contains("successfully") || message == "Import cancelled.")
                {
                    statusTimer.Interval = 5000;
                    statusTimer.Tick -= (s, e) => { };
                    statusTimer.Tick += (s, e) =>
                    {
                        statusLabel.Text = string.Empty;
                        statusTimer.Stop();
                    };
                    statusTimer.Start();
                }
            }
        }
    }
}
