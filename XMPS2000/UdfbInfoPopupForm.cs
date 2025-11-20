using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Text;
using XMPS2000.Core;

namespace XMPS2000
{
    public partial class UdfbInfoPopupForm : Form
    {
        private string udfbName;

        public UdfbInfoPopupForm(string udfbName)
        {
            InitializeComponent();
            this.udfbName = udfbName;
            lblTitle.Text = $"UDFB: {udfbName}";

            // Load the data
            LoadIOList(udfbName);
            LoadTagList(udfbName);
        }

        private void LoadIOList(string udfbName)
        {
            lvIO.View = View.Details;
            lvIO.Columns.Clear();
            lvIO.Items.Clear();

            lvIO.Columns.Add("Type", 150);
            lvIO.Columns.Add("Variable Name", 150);
            lvIO.Columns.Add("UDFB Name", 150);
            lvIO.Columns.Add("Required", 150);
            lvIO.Columns.Add("DataType", -2);

            try
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string modelFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD")? "XBLDLibraries": "XMLibraries";
                string fileName = $"{udfbName} Logic.csv";
                string csvPath = Path.Combine(basePath, "MessungSystems", "XMPS2000", "Library", modelFolder, fileName);
                if (!File.Exists(csvPath))
                {
                    MessageBox.Show($"CSV file not found: {csvPath}", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var lines = File.ReadAllLines(csvPath);

                foreach (var line in lines)
                {
                    if (line.StartsWith("DataType:"))
                    {
                        string dataType = GetValue(line, "DataType");
                        string text = GetValue(line, "Text");
                        string name = GetValue(line, "Name");
                        string type = GetValue(line, "Type");

                        var item = new ListViewItem(new[] {
                    type ?? "N/A",
                    text ?? "N/A",
                    name ?? "N/A",
                    "Yes",
                    dataType ?? "N/A"
                });

                        if (type?.ToLower().Contains("input") == true)
                            item.BackColor = System.Drawing.Color.FromArgb(245, 251, 255);
                        else if (type?.ToLower().Contains("output") == true)
                            item.BackColor = System.Drawing.Color.FromArgb(255, 251, 245);

                        lvIO.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load I/O information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AutoResizeColumns()
        {
            if (lvIO.Columns.Count == 0) return;

            // Get the available width (subtract scrollbar width if needed)
            int availableWidth = lvIO.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;

            // Define proportional widths for each column (should add up to 1.0)
            double[] columnProportions = { 0.15, 0.30, 0.25, 0.15, 0.15 }; // Adjust these as needed

            for (int i = 0; i < lvIO.Columns.Count; i++)
            {
                int columnWidth = (int)(availableWidth * columnProportions[i]);
                lvIO.Columns[i].Width = columnWidth;
            }
        }

        // Add this event handler to handle ListView resize
        private void lvIO_Resize(object sender, EventArgs e)
        {
            AutoResizeColumns();
        }
        private string GetValue(string line, string key)
        {
            foreach (var part in line.Split(' '))
            {
                if (part.StartsWith(key + ":", StringComparison.OrdinalIgnoreCase))
                {
                    return part.Substring(key.Length + 1);
                }
            }
            return null;
        }

        private bool IsRequiredField(XElement io)
        {
            string isRequired = (string)io.Element("IsRequired");
            return isRequired == "1" || isRequired?.ToLower() == "true";
        }

        private void LoadTagList(string udfbName)
        {
            lvTags.Columns.Clear();
            lvTags.Items.Clear();

            lvTags.Columns.Add("Logical Address", 120);
            lvTags.Columns.Add("Tag Name", 120);
            lvTags.Columns.Add("DataType", 100);
            lvTags.Columns.Add("Initial Value", 100);
            lvTags.Columns.Add("Retentive", 80);
            lvTags.Columns.Add("Retentive Address", 120);
            lvTags.Columns.Add("Show Logical Address", 140);

            try
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string modelFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD")? "XBLDLibraries": "XMLibraries";
                string fileName = $"{udfbName} Logic.csv";
                string csvPath = Path.Combine(basePath, "MessungSystems", "XMPS2000", "Library", modelFolder, fileName);
                if (!File.Exists(csvPath))
                {
                    MessageBox.Show("CSV file not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var lines = File.ReadAllLines(csvPath);

                foreach (var line in lines)
                {
                    if (line.ToLower().Contains("tag:"))
                    {
                        string logicalAddress = GetCsvValue(line, "LogicalAddress");
                        string tagName = GetCsvValue(line, "Tag");
                        string dataType = GetCsvValue(line, "Label");
                        string initialValue = GetCsvValue(line, "InitialValue");
                        string retentive = GetCsvValue(line, "Retentive");
                        string retentiveAddress = GetCsvValue(line, "RetentiveAddress");
                        string showLogicalAddress = GetCsvValue(line, "ShowLogicalAddress");

                        var item = new ListViewItem(new[]
                        {
                    logicalAddress ?? "",
                    tagName ?? "",
                    dataType ?? "",
                    initialValue ?? "",
                    retentive ?? "",
                    retentiveAddress ?? "",
                    showLogicalAddress ?? ""
                });

                        lvTags.Items.Add(item);
                    }
                }

                if (lvTags.Items.Count == 0)
                {
                    lvTags.Items.Add(new ListViewItem(new[] { "No tags found in this UDFB." }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tags: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCsvValue(string line, string key)
        {
            foreach (var part in line.Split(','))
            {
                string trimmedPart = part.Trim();
                if (trimmedPart.StartsWith(key + ":", StringComparison.OrdinalIgnoreCase))
                {
                    return trimmedPart.Substring(key.Length + 1).Trim(); 
                }
            }
            return "";
        }

    }

}