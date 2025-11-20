using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using XMPS2000.Core;
using XMPS2000.Core.App;

namespace XMPS2000
{
    public partial class NewProjectForm : Form
    {
        public ProjectInfo projectInfo { get; set; }
        public string SaveAsModel = "";
        private XMPS xm;
        public NewProjectForm()
        {
            InitializeComponent();
            ddlModel.DrawMode = DrawMode.OwnerDrawFixed;
            ddlModel.DrawItem += ddlModel_DrawItem;
            ddlModel.SelectedIndexChanged += ddlModel_SelectedIndexChanged;
        }

        private void ddlModel_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidatePLCModel(ddlModel.Text);
        }

        private void TxtPath_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateProjectPath(TxtPath.Text);
        }

        private void TxtProjectName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidatePrajectName(TxtProjectName.Text);
        }
        private bool ValidatePLCModel(string sPLCModel)
        {
            bool bValid = false;

            if (sPLCModel == string.Empty || sPLCModel == "Select Model")
            {
                bValid = true;
                epNewProject.SetError(ddlModel, "Please select a PLC Model.");
            }
            else
            {
                epNewProject.SetError(ddlModel, "");
            }
            return bValid;
        }

        private bool ValidatePrajectName(string sProjectName)
        {
            bool bValid = false;

            if (sProjectName == string.Empty)
            {
                bValid = true;
                epNewProject.SetError(TxtProjectName, "Select Proper Path of Project.");
            }
            else
            {
                epNewProject.SetError(TxtProjectName, "");
            }
            return bValid;
        }

        private bool ValidateProjectPath(string sProjectPath)
        {
            bool bValid = false;
            if (sProjectPath == string.Empty && !Directory.Exists(sProjectPath))
            {
                bValid = true;
                epNewProject.SetError(TxtPath, "Select Proper Path of Project.");

            }
            else
            {
                epNewProject.SetError(TxtPath, "");
            }

            return bValid;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CreateNewProject();

        }

        private void CreateNewProject()
        {
            if (!string.IsNullOrEmpty(SaveAsModel) && !SaveAsModel.Equals(ddlModel.Text))
            {
                MessageBox.Show("Please choose the correct model.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidatePLCModel(ddlModel.Text) && !ValidatePrajectName(TxtProjectName.Text) && !ValidateProjectPath(TxtPath.Text))
            {
                projectInfo = new ProjectInfo();
                projectInfo.ProjectPath = Path.Combine(TxtPath.Text.ToString(), TxtProjectName.Text.ToString(), TxtProjectName.Text.ToString()) + ".xmprj";
                if (File.Exists(projectInfo.ProjectPath))
                {
                    DialogResult result = MessageBox.Show("A file with the same name exists on this path. Do you want to replace it?",
                                              "XMPS2000",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        File.Delete(projectInfo.ProjectPath);
                    }
                    else
                    {
                        return;
                    }
                }
                projectInfo.PLCModel = ddlModel.Text;
                this.Close();
            }
        }

        private void btnpath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.SelectedPath = TxtPath.Text;
            DialogResult result = folderDlg.ShowDialog();
            TxtPath.Text = folderDlg.SelectedPath.ToString();
        }

        public bool IsValidFileName(string expression, bool platformIndependent)
        {
            string sPattern = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
            if (platformIndependent)
            {
                sPattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";
            }
            return (Regex.IsMatch(expression, sPattern, RegexOptions.CultureInvariant));
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            projectInfo = new ProjectInfo();
            projectInfo.ProjectPath = string.Empty;
            projectInfo.PLCModel = string.Empty;
            this.Close();
        }

        private void NewProjectForm_Load(object sender, EventArgs e)
        {
            ProjectFormInitialisation();
        }

        private void ProjectFormInitialisation()
        {
            xm = XMPS.Instance;
            BindPLCModelDropDown();
            SetDefaultValues();
            if (SaveAsModel != "")
            {
                ddlModel.Text = SaveAsModel;
            }
        }

        private void SetDefaultValues()
        {
            string sDefaultPath = GetDefaultPath();
            var lastProjNumber = GetProjectSequence(sDefaultPath, false);
            FillProjectName(lastProjNumber, sDefaultPath, false);
        }

        private void FillProjectName(string lastProjNumber, string sDefaultPath, bool isXBLD)
        {
            ddlModel.SelectedIndex = ddlModel.SelectedIndex != -1 ? ddlModel.SelectedIndex : 0;
            TxtProjectName.Text = isXBLD ? "XBLDProject" + lastProjNumber : "XMProject" + lastProjNumber;
            TxtPath.Text = sDefaultPath;
            projectInfo = new ProjectInfo();
            projectInfo.ProjectPath = string.Empty;
            projectInfo.PLCModel = string.Empty;
        }

        private string GetProjectSequence(string sDefaultPath, bool isXBLD)
        {
            string isDir = Path.Combine(sDefaultPath.Replace("\\XMProject", ""), "XMProject");
            if (!Directory.Exists(isDir))
            {
                Directory.CreateDirectory(isDir);
            }
            var listDirectories = Directory.EnumerateDirectories(sDefaultPath).Where(d => d.Contains("XMProject") || d.Contains("XBLDProject")).Select(d => d).ToList();

            int temp = 0;
            int number = 0;
            foreach (var dir in listDirectories)
            {
                var projectDir = Path.GetFileName(dir);
                var success = isXBLD ? Int32.TryParse(projectDir.Replace("XBLDProject", ""), out number) : Int32.TryParse(projectDir.Replace("XMProject", ""), out number);
                temp = success ? (temp < number) ? number : temp : temp;
            }
            return (++temp).ToString("00");
        }
        Boolean ValidateXml(string xmlString)
        {
            try
            {
                return XDocument.Load(new StringReader(xmlString)) != null;
            }
            catch
            {
                return false;
            }
        }

        private string GetDefaultPath()
        {
            string sDefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000";
            string filePath = Path.Combine(sDefaultPath, "ProjectsPaths.xml");
            XmlDocument doc = new XmlDocument();
            // Check if the file exists
            if (File.Exists(filePath))
            {
                if (ValidateXml(doc.ToString()))
                    doc.Load(filePath);
                else
                {
                    File.Delete(filePath);
                    CreateXMLFile(doc);

                }
            }
            else
            {
                // Create a new XML file with the root element
                CreateXMLFile(doc);
            }
            // Select the parent node (Projects)
            XmlNode projectsNode = doc.SelectSingleNode("//DefaultPath");
            if (projectsNode == null)
            {
                // Create the Projects node if it doesn't exist
                projectsNode = doc.CreateElement("DefaultPath");
                doc.DocumentElement.AppendChild(projectsNode);
            }
            else
            {
                return projectsNode.InnerText;
            }
            // Select the ProjectPath node
            XmlNode projectPathNode = projectsNode.SelectSingleNode("DefaultPath");
            // Check if the ProjectPath node exists
            if (projectPathNode != null)
            {
                // Node exists, get its value
                return projectPathNode.InnerText;
            }
            else
            {
                // Node does not exist, create and add it under Projects node
                XmlNode newProjectPathNode = doc.CreateElement("ProjectPath");
                newProjectPathNode.InnerText = Path.Combine(sDefaultPath, "XM Projects"); // Set a default value or leave it empty
                projectsNode.AppendChild(newProjectPathNode);
                // Save the changes to the XML file
                doc.Save(filePath);
                return newProjectPathNode.InnerText;
            }
        }

        private void CreateXMLFile(XmlDocument doc)
        {
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement("Root");
            doc.AppendChild(root);
            doc.InsertBefore(xmlDeclaration, root);
        }

        private void BindPLCModelDropDown()
        {
            List<string> prefixes = new List<string>();
            try
            {
                string sDefaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MessungSystems\XMPS2000\ProjectTemplates\SetUpType.xml");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(sDefaultPath);
                XmlNodeList setupTypeNodes = xmlDoc.SelectNodes("/SetupTypeInfo/SetupType");

                //save the setup types values in prefiex list.
                foreach (XmlNode setupTypeNode in setupTypeNodes)
                {
                    string setupType = setupTypeNode.InnerText;
                    string prefix = setupType.Equals("XMPRO") ? "XM" :
                                    setupType.Equals("XBLD") ? "XB" : "X";
                    prefixes.Add(prefix);
                }
            }
            catch
            {
                //if any exception occured at the time of file reading then set XMPRO build by default
                prefixes.Add("XM");
            }
            //Adding New Project Templete
            ProjectTemplates _pTemplates = xm.ProjectTemplates;
            foreach (var template in _pTemplates.Templates.Where(t => prefixes.Any(prefix => t.PlcName.StartsWith(prefix))))
            {
                ddlModel.Items.Add(template.PlcName.ToString());
            }
        }
        private void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedText = ddlModel.Text;

            if (selectedText == "XBLD-14" || selectedText == "XBLD-17")
            {
                ddlModel.SelectedIndex = -1;
                return;
            }          
            if (selectedText == "Select Model") PopulateDefaultDeviceInfo();
            if (selectedText == "XM-14-DT-HIO")
                AnalogInputOutputsLabels(false);
            else
                AnalogInputOutputsLabels(true);

            string sDefaultPath = GetDefaultPath(); //  Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\XM Projects";
            var lastProjNumber = GetProjectSequence(sDefaultPath, selectedText.StartsWith("XBLD"));
            FillProjectName(lastProjNumber, sDefaultPath, selectedText.StartsWith("XBLD"));
            PopulateDeviceInfo(selectedText);
        }

        private void ddlModel_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox cb = sender as ComboBox;
            string itemText = cb.Items[e.Index].ToString();
            bool isDisabled = itemText == "XBLD-14" || itemText == "XBLD-17";

            // Determine if the item is selected
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            bool isEditing = (e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit;

            // Set background color
            Color backgroundColor = (cb.DroppedDown && isSelected)
                ? SystemColors.Highlight
                : cb.BackColor;

            // Set text color
            Brush textBrush = isDisabled
                ? (cb.DroppedDown && isSelected ? Brushes.LightGray : Brushes.Gray)
                : (cb.DroppedDown && isSelected ? Brushes.White : Brushes.Black);

            using (SolidBrush backgroundBrush = new SolidBrush(backgroundColor))
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

            e.Graphics.DrawString(itemText, e.Font, textBrush, e.Bounds);
        }


        private void PopulateDefaultDeviceInfo()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            lblPLCName.Text = "Not Selected";
            lblDigitalInput.Text = "0";
            lblDigitalOutput.Text = "0";
            lblAnalogInput.Text = "0";
            lblAnalogOutput.Text = "0";
            lblEthernet.Text = "No";
            lblExpansionSlots.Text = "No";
            lblComPorts.Text = "No";
        }
        private void PopulateDeviceInfo(string deviceModel)
        {
            var device = xm.ProjectTemplates.Templates.Where(x => x.PlcName == deviceModel).FirstOrDefault();
            if (device == null) return;
            lblPLCName.Text = device.PlcName;
            var imagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\ProjectTemplates\" + device.PlcName + "\\" + device.PlcName + ".jpg";
            pictureBox1.Image = Image.FromFile(imagePath);
            lblDigitalInput.Text = device.NoOfDigitalInput.ToString();
            lblDigitalOutput.Text = device.NoOfDigitalOutput.ToString();
            lblAnalogInput.Text = device.NoOfAnalogInput.ToString();
            lblAnalogOutput.Text = device.NoOfAnalogOutput.ToString();
            lblEthernet.Text = device.HasEthernet ? "Yes" : "No";
            lblExpansionSlots.Text = device.HasExpansionSlots ? "Yes" : "No";
            lblComPorts.Text = device.HasCommunicationPorts ? "Yes" : "No";
            if (device.PlcName == "XM-14-DT-HIO")
            {
                lblHSIOCount.Text = "(" + "" + "HSI : 4 " + " " + "HSO : 2 " + ")";
            }
        }

        private void AnalogInputOutputsLabels(bool value)
        {
            lblHSIOCount.Visible = value;
            lblanalogio.Visible = value;
            lblAnalogInput.Visible = value;
            lblAnalogOutput.Visible = value;
            label1.Visible = value;
            label2.Visible = value;
            lblHSIOCount.Visible = value == true ? false : true;
        }

        private void TxtProjectName_KeyPress(object sender, KeyPressEventArgs e)   // keypress pass char to control upon charcter keypress delete key does not represent charcter
        {
            // allow char ,digit  
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 3 && e.KeyChar != 22 && e.KeyChar != 95)
            {
                e.Handled = true;
            }
        }

        public void SetNewProject(string model)
        {
            ProjectFormInitialisation();
            if (model.StartsWith("XBLD"))
                ddlModel.SelectedIndex = 2;
            else
                ddlModel.SelectedIndex = 5;
            CreateNewProject();
        }
    }
}
