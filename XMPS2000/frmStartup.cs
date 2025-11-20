using ClassList;
using ComponentList;
using ScreenInformation;
using W5;
using K5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace XMPS1000
{
    public partial class frmStartup : Form
    {
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private ProjectExplorerWindow m_ProjectExplorer;
        private BlocksWindow m_Blocks;

        public frmStartup()
        {
            InitializeComponent();
            CreateStandardControls();
            this.dockPanel.Theme = vS2015BlueTheme;
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void opendocument(string formname)
        {

            XMProForm xMProForm = new XMProForm();
            switch (formname.ToString().Trim())
            {
                case "System Configuration":
                    xMProForm.Controls.Add(new COMSettingsUserControl(xMProForm) { Name = "ConfigurationUserControl" });
                    xMProForm.Text = formname;
                    break;
                case "Ethernet":
                    xMProForm.Controls.Add(new EthernetSettingsUserControl(xMProForm) { Name = "ConfigurationUserControl" });
                    xMProForm.Text = formname;
                    break;
                case "Modbus RTU Master":
                    xMProForm.Controls.Add(new ModbusRTUUserControl(xMProForm) { Name = "ConfigurationUserControl" });
                    xMProForm.Text = formname;
                    break;
                case "Modbus TCP Server":
                    xMProForm.Controls.Add(new ModbusTCPServerUserControl(xMProForm) { Name = "ConfigurationUserControl" });
                    xMProForm.Text = formname;
                    break;
                case "Modbus TCP Client":
                    xMProForm.Controls.Add(new ModbusTCPClientUserControl(xMProForm) { Name = "ConfigurationUserControl" });
                    xMProForm.Text = formname;
                    break;
            }
            xMProForm.Height = 500;
            xMProForm.Width = 500;
            xMProForm.ControlBox = false;
            xMProForm.ShowDialog();
        }

        public static void OpenForm(string formname)
        {
            if (formname.ToString().Trim() == "System Configuration" || formname.ToString().Trim() == "Ethernet" || formname.ToString().Trim() == "Modbus RTU Master" || formname.ToString().Trim() == "Modbus TCP Server" || formname.ToString().Trim() == "Modbus TCP Client")
            {
                Instance.opendocument(formname);
            }
        }
        public static frmStartup Instance
        {
            get { return instance; }
        }

        private static frmStartup instance = new frmStartup();

        private void CreateStandardControls()
        {
            m_ProjectExplorer = new ProjectExplorerWindow();
            m_Blocks = new BlocksWindow();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            m_ProjectExplorer.Show(dockPanel);

        }

        private void frmStartup_Load(object sender, EventArgs e)
        {


            string configFile = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        private void frmStartup_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "DockPanel.config");
            if (m_bSaveLayout)
                dockPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(ProjectExplorerWindow).ToString())
                return m_ProjectExplorer;
            else if (persistString == typeof(BlocksWindow).ToString())
                return m_Blocks;
            else
            {
                // DummyDoc overrides GetPersistString to add extra information into persistString.
                // Any DockContent may override this value to add any needed information for deserialization.
                return null;
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectForm projectPathandName = new NewProjectForm();
            projectPathandName.ShowDialog();
            string folderpath = projectPathandName.FilePath;
            //InstructionType selectedInstructionType = ((InstructionType)ComboBox1.SelectedValue);

            LadderWindow ladderDoc = CreateNewDocument();
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                ladderDoc.MdiParent = this;
                ladderDoc.Show();
            }
            else
                ladderDoc.Show(dockPanel);

            m_Blocks.Show(dockPanel);

            ScreenForm scr1 = new ScreenForm();
            scr1.Show();
        }

        private LadderWindow CreateNewDocument()
        {
            LadderWindow ladderDoc = new LadderWindow();

            int count = 1;
            string text = $"Block Diagram{count}";
            while (FindDocument(text) != null)
            {
                count++;
                text = $"Block Diagram{count}";
            }

            ladderDoc.Text = text;
            return ladderDoc;
        }

        private IDockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }


        private void MenuProjectNew_Click(object sender, EventArgs e)
        {

            NewProjectForm projectPathandName = new NewProjectForm();
            projectPathandName.ShowDialog();
            string folderpath = projectPathandName.FilePath;
            //InstructionType selectedInstructionType = ((InstructionType)ComboBox1.SelectedValue);

            //LadderWindow ladderDoc = CreateNewDocument();
            //if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            //{
            //    ladderDoc.MdiParent = this;
            //    ladderDoc.Show();
            //}
            //else
            //    ladderDoc.Show(dockPanel);

            //m_Blocks.Show(dockPanel);

            //ScreenForm scr1 = new ScreenForm();
            //scr1.Show();


            //LadderWindow ladderDoc = CreateNewDocument();
            //if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            //{
            //    ladderDoc.MdiParent = this;
            //    ladderDoc.Show();
            //}
            //else
            //    ladderDoc.Show(dockPanel);

            //m_Blocks.Show(dockPanel);

            // ScreenForm scr1 = new ScreenForm();
            //scr1.Show();

        }

        private void MenuProjectExit_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void strpBtnCompile_Click(object sender, EventArgs e)
        {

        }

        private void strpBtnRedo_Click(object sender, EventArgs e)
        {

        }
    }
}
