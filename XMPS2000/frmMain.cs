using Interpreter.MCodeConversion;
using LadderDrawing;
using LadderDrawing.UserControls;
using LadderEditorLib.DInterpreter;
using LadderEditorLib.UserControls;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using XMPS2000.Bacnet;
using XMPS2000.Configuration;
using XMPS2000.Core;
using XMPS2000.Core.App;
using XMPS2000.Core.BacNet;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices;
using XMPS2000.Core.Devices.Slaves;
using XMPS2000.Core.LadderLogic;
using XMPS2000.Core.Types;
using XMPS2000.Interpreter;
using XMPS2000.LadderLogic;
using XMPS2000.UndoRedoGridLayout;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
//public delegate void CrossReferenceEventHandler(string tagname, object sender, EventArgs e);

namespace XMPS2000
{
    public partial class frmMain : Form
    {
        XMPS xm;
        private Dictionary<string, Counter> Counters = new Dictionary<string, Counter>();
        private const string PROJECT_NODE_DEFAULT_NAME = "Current Project";
        public static Environment.SpecialFolder DataPath = Environment.SpecialFolder.ApplicationData;
        string appdatapath = Application.UserAppDataPath.ToString();
        private List<string> compilationErrors = new List<string>();
        //Skipped Tag List while importing tags
        private List<Tuple<XMIOConfig, string>> skippedTagsList = new List<Tuple<XMIOConfig, string>>();
        //Stacklist
        //Undo Redo For                                                     ==> TagWindow
        public static Stack<Object> UndoTags = new Stack<Object>(5);
        public static Stack<Object> RedoTags = new Stack<Object>(5);
        public static Stack<object> DeleteElement = new Stack<object>(5);
        public static Stack<RESISTANCETable_Values> UndoResistance = new Stack<RESISTANCETable_Values>();
        public static Stack<RESISTANCETable_Values> RedoResistance = new Stack<RESISTANCETable_Values>();
        public static Stack<RESISTANCETable_Values> DeleteResistance = new Stack<RESISTANCETable_Values>();
        public static bool IsDelete = false;                           //Delete Element Is Present
        public static bool AfterDelete = false;
        //ADDING ELEMENT FOR THE CHECKING 
        //Adding for getting Last Added Logical Block
        private string LogicalblockName = "";
        //For the adjusting left side loaded projects panel
        private bool isResizing = false;
        private Point lastMouseLocation;
        private const int resizeBorderWidth = 5;
        public int deltaXAxis = 0;
        public int newCalculatedWidth = 0;
        private int initialSplitterDistance = 0;
        private int onlinemonitoringIter = 0;

        private int lastSplitterDistance = 225;
        //UNDOREDO 
        private PublishManager publishManager;
        private SubscribeManager subscribeManager;
        private ModbusRTUSlaveManager modbusRTUSlaveManager;
        private ModbusRTUMasterManager modbusRTUMasterManager;
        private ModbusTCPClientManager modbusTCPClientManager;
        private MODBUSTCPServerManager modbusTCPServerManager;
        private StatusCircleMenuItem statusIndicator;
        private ToolStripSeparator showstatus;

        //for Import logic block with old name
        private TreeNode currentAddedNode;
        //for showing selected grid row.
        private int erroredRungNo = 0;
        public static event EventHandler GridDataChanged;
        public frmMain()
        {
            InitializeComponent();
            InitializeToolbar();
            InitializeMenuBar();
            InitializeTimerCounters();
            // Add status indicator to the right side of the menu
            showstatus = new ToolStripSeparator();
            statusIndicator = new StatusCircleMenuItem();
            statusIndicator.Alignment = ToolStripItemAlignment.Left;
            statusIndicator.Width = 50;
            statusIndicator.Text = "ShowStatus";
            tspMain.Items.Add(showstatus);
            tspMain.Items.Add(statusIndicator);
            StatusShow(false);
            // Set initial status (replace with your condition logic)
        }
        public static void OnGridDataChanged()
        {
            GridDataChanged?.Invoke(null, EventArgs.Empty);
        }
        private void StatusShow(bool status)
        {
            showstatus.Visible = status;
            statusIndicator.Visible = status;
        }
        #region FormEvents

        private void frmMain_Load(object sender, EventArgs e)
        {



            tsmAddRemoteIO.ComboBox.Items.Clear();
            tsmAddExpansionIO.ComboBox.Items.Clear();
            tsmAddRemoteIO.ComboBox.Items.AddRange(RemoteModule.List.FindAll(x => x.IOType.Equals("Remote I/O")).ToArray());
            tsmAddExpansionIO.ComboBox.Items.AddRange(RemoteModule.List.FindAll(x => x.IOType.Equals("Expansion I/O")).ToArray());
            MenuModePLCStop.Enabled = false;
            MenuModeLogout.Enabled = false;
            traceWindowToolStripMenuItem.Enabled = false;
            MenuModePLCStart.Enabled = false;
            MenuModePLCResetOrigin.Enabled = false;
            MenuModePLCResetCold.Enabled = false;
            MenuModePLCResetwarm.Enabled = false;

            strpBtnSelect.Enabled = false;


            MenuModeDnldProject.Enabled = false;
            MenuModeDnldSourceCode.Enabled = false;
            rTCSettingToolStripMenuItem.Enabled = false;
            strpBtnCompile.Enabled = false;
            strpBtnUndo.Enabled = true;
            strpBtnRedo.Enabled = true;
            strpBtnDelete.Enabled = false;
            strpBtnFind.Enabled = false;
            strpBtnHelp.Enabled = false;
            strpBtnLogout.Enabled = false;
            strpBtnOnlineMonitor.Enabled = false;
            strpBtnCompile.Enabled = false;
            //strpBtnZoomIn.Enabled = false;
            //strpBtnZoomOut.Enabled = false;
            strpBtnZoomPercent.Enabled = false;
            //strpBtnZoomPercent1.Enabled = false;
            forceUnforceMenu.Enabled = false;
            xm = XMPS.Instance;
            string[] args = System.Environment.GetCommandLineArgs();
            if (args.Count() > 1)
            // Program opened from project file
            {
                try
                {

                    if (System.IO.File.Exists(args[1]))
                    {
                        RecentProject project = new RecentProject();
                        project.ProjectPath = args[1];
                        project.ProjectName = args[1].Substring(args[1].LastIndexOf('\\') + 1);
                        xm.SetCurrentProject(project);
                        LoadCurrentProject();
                        AddSystemTags();
                        xm.LoadedProject.ProjectName = project.ProjectName.Replace(" ", "");
                        xm.LoadedProject.ProjectPath = project.ProjectPath;
                        Application.OpenForms[1].Text = $"XMPS 2000 {project.ProjectName.Replace(" ", "").Replace(".xmprj", "")}";

                        strpBtnPaste.Enabled = true;
                        strpBtnCut.Enabled = true;
                        strpBtnCopy.Enabled = true;
                        strpBtnUndo.Enabled = true;
                        strpBtnRedo.Enabled = true;
                        forceUnforceMenu.Enabled = false;
                        strpBtnDelete.Enabled = true;
                    }

                }
                catch { }
            }
            else
            {
                ShowOrActivateForm("InitialForm");
                RenderBaseTreeNodes();
                strpBtnLogin.Enabled = false;
                MenuModeLogin.Enabled = false;
                strpBtnSaveProject.Enabled = false;
                strpBtnCloseProject.Enabled = false;
                strpBtnDownloadProject.Enabled = false;
                strpBtnPaste.Enabled = false;
                strpBtnCut.Enabled = false;
                strpBtnCopy.Enabled = false;
                traceWindowToolStripMenuItem.Enabled = false;
                strpBtnCompile.Enabled = false;
                strpBtnUndo.Enabled = false;
                strpBtnRedo.Enabled = false;
                forceUnforceMenu.Enabled = false;
                strpBtnDelete.Enabled = false;

                //strpBtnFind.Enabled = false;
            }
        }

        bool isExiting = false;
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExiting)
                return; 

            DialogResult result = CheckandSaveApp();

            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (xm != null && xm.Entries != null)
                xm.Entries.Clear();
            isExiting = true;
            Application.Exit();
        }

        #endregion
        #region Main Menu Events

        private void MenuProjectExit_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void MenuProjectNew_Click(object sender, EventArgs e)
        {
            NewProjectDialog();
        }

        private void MenuProjectOpen_Click(object sender, EventArgs e)
        {
            OpenProjectDialog();
        }

        #endregion
        #region Projects Tree View

        private void tvProjects_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e?.Node?.Tag == null)
                    return;

                NodeInfo niCurrent = e.Node.Tag as NodeInfo;
                if (niCurrent is null)
                    return;

                if (e.Button == MouseButtons.Right)
                {
                    ShowTreeContextMenu(niCurrent, e.Node, e.X, e.Y);
                    PerformTreeNodeDoubleClickActions(niCurrent, e.Node, e.Node.Text);
                }
                else
                {
                    PerformTreeNodeDoubleClickActions(niCurrent, e.Node, e.Node.Text);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tvProjects_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null)
            {
                e.Node.ImageIndex = 1;
            }

        }

        private void tvProjects_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvProjects.LabelEdit = false;
            //set to NewAddresTagIndex to 0
            if (xm.LoadedProject != null)
                xm.LoadedProject.NewAddedTagIndex = 0;
            if (erroredRungNo != 0)
                xm.LoadedProject.NewAddedTagIndex = erroredRungNo;
            NodeInfo niCurrent = e.Node.Tag as NodeInfo;
            if (e.Node.Text == "File")
                return;
            CurrentNodeName.Text = e.Node.Text;


            if (niCurrent is null) return;
            if (e.Button == MouseButtons.Right)
            {
                ShowTreeContextMenu(niCurrent, e.Node, e.X, e.Y);
            }
            else
            {
                if (e.Node.Text != "Recent Projects")
                    PerformTreeNodeActions(niCurrent, e.Node, e.Node.Text);
            }
        }

        #endregion
        private void MenuViewZoom_Click(object sender, EventArgs e)
        { }
        private void tspMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null)
            {
                string itemName = (e.ClickedItem as ToolStripButton).Tag.ToString();

                switch (itemName)
                {
                    case "NEW":
                        {
                            NewProjectDialog();
                            break;
                        }
                    case "OPEN":
                        {
                            OpenProjectDialog();
                            break;
                        }
                    case "SAVE":
                        {
                            break;
                        }
                    case "CLOSE":
                        {
                            break;
                        }
                    // Saparator
                    case "UPLOAD":
                        {
                            break;
                        }
                    case "DOWNLOAD":
                        {
                            break;
                        }
                    // Saparator
                    case "ZOOMIN":
                        {
                            break;
                        }
                    case "ZOOMOUT":
                        {
                            break;
                        }
                    // Saparator
                    case "COMPILE":
                        {
                            break;
                        }
                    case "LOGIN":
                        {
                            break;
                        }
                    case "LOGOUT":
                        {
                            break;
                        }
                    case "ONLINEMONITORING":
                        {
                            break;
                        }
                    // Saparator
                    case "CUT":
                        {
                            break;
                        }
                    case "COPY":
                        {
                            break;
                        }
                    case "PASTE":
                        {
                            break;
                        }
                    case "SELECT":
                        {
                            break;
                        }
                    // Saparator
                    case "UNDO":
                        {
                            break;
                        }
                    case "REDO":
                        {
                            break;
                        }
                    case "DELETE":
                        {
                            break;
                        }
                    case "NEXTSCREEN":
                        {
                            break;
                        }
                    case "PREVSCREEN":
                        {
                            break;
                        }
                    case "FIND":
                        {
                            break;
                        }
                    case "HELP":
                        {
                            break;
                        }
                }
            }
        }
        private void tsmAddResiValues_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProjects.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Please select a Resistance Table node first and try again.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NodeInfo selectedNodeInfo = selectedNode.Tag as NodeInfo;
            if (selectedNodeInfo == null || selectedNodeInfo.NodeType != NodeType.ListNode)
            {
                MessageBox.Show("Please select a valid Resistance Table node.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (xm.LoadedProject.ResistanceValues == null)
                xm.LoadedProject.ResistanceValues = new List<RESISTANCETable_Values>();

            string tableName =selectedNodeInfo.Info; 
            int countForTable = XMPS.Instance.LoadedProject.ResistanceValues
                .Count(rv => rv.Name == tableName);

            if (countForTable >= 20)
            {
                MessageBox.Show("Maximum of 20 resistance values allowed for this table.",
                                "XMPS 2000",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            xm.SelectedNode = selectedNodeInfo;

            using (var form = new frmAddResistanceValue())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
           OnGridDataChanged();         
        }
        private void tsmDeleteResistanceTable_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProjects.SelectedNode;
            if (selectedNode == null || selectedNode.Parent?.Text != "Resistance Lookup Table")
            {
                MessageBox.Show("Please select a resistance table to delete.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tableName = selectedNode.Text;
            try
            {
                var usedTags = XMPS.Instance.LoadedProject.Tags.Where(io => io.Mode == tableName &&!io.Label.EndsWith("_OR") &&
                   !io.Label.EndsWith("_OL")).ToList();
                if (usedTags.Any())
                {
                    string tagNames = string.Join(", ", usedTags.Select(t => t.Label));

                    MessageBox.Show(
                        $"You can't delete '{tableName}' because it is used in the following tags:\n\n{tagNames}",
                        "XMPS 2000",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                var tableToRemove = xm.LoadedProject.ResistanceTables.FirstOrDefault(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
                if (tableToRemove != null)
                {
                    xm.LoadedProject.ResistanceTables.Remove(tableToRemove);
                }
                if (xm.LoadedProject.ResistanceValues != null)
                {
                    xm.LoadedProject.ResistanceValues.RemoveAll(rv => rv.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
                }
                if (RESISTANCETable.TableNames.Contains(tableName))
                {
                    RESISTANCETable.TableNames.Remove(tableName);
                }
                xm.CurrentScreen = "ResistanceValue#";
                selectedNode.Remove();
                List<ModeUI> items = ModeUI.List;
                items.RemoveAll(m => m.Text == tableName);
                xm.MarkProjectModified(true);
                var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                curgridform.OnShown();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting resistance table: {ex.Message}","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsmEditResistanceTable_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProjects.SelectedNode;
            if (selectedNode == null || selectedNode.Parent?.Text != "Resistance Lookup Table")
            {
                MessageBox.Show("Please select a resistance table to edit.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string oldTableName = selectedNode.Text;
            string newTableName = Interaction.InputBox(
                "Enter new name for the resistance table:",
                "Edit Resistance Table",
                oldTableName,
                -1,
                -1
            );
            newTableName = newTableName.Trim();
            if (string.IsNullOrWhiteSpace(newTableName))
            {
                return;
            }
            string allowedPattern = @"^[_a-zA-Z][_a-zA-Z0-9]*$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(newTableName.Trim(), allowedPattern))
            {
                MessageBox.Show("Table name must start with a letter or underscore and contain only letters, numbers, and underscores (no spaces or special characters).",
                                "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newTableName == oldTableName)
            {
                return;
            }
            if (xm.LoadedProject.ResistanceTables.Any(t =>t.Name.Equals(newTableName, StringComparison.OrdinalIgnoreCase) && t.Name != oldTableName))
            {
                MessageBox.Show("A Resistance Table with this name already exists.","Duplicate Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var tableToUpdate = xm.LoadedProject.ResistanceTables.FirstOrDefault(t => t.Name.Equals(oldTableName, StringComparison.OrdinalIgnoreCase));
                if (tableToUpdate != null)
                {
                    tableToUpdate.Name = newTableName;
                }
                if (xm.LoadedProject.ResistanceValues != null)
                {
                    foreach (var rv in xm.LoadedProject.ResistanceValues)
                    {
                        if (rv.Name.Equals(oldTableName, StringComparison.OrdinalIgnoreCase))
                        {
                            rv.Name = newTableName;
                        }
                    }
                }
                if (RESISTANCETable.TableNames.Contains(oldTableName))
                {
                    RESISTANCETable.TableNames.Remove(oldTableName);
                    RESISTANCETable.TableNames.Add(newTableName);
                    var oldItem = ModeUI.List.FirstOrDefault(m => m.Text == oldTableName);
                    if (oldItem != null)
                    {
                        oldItem.Text = newTableName; 
                    }
                }
                foreach (var tag in xm.LoadedProject.Tags)
                {
                    if (tag.Mode != null && tag.Mode.Equals(oldTableName, StringComparison.OrdinalIgnoreCase))
                    {
                        tag.Mode = newTableName;
                    }
                }
                //foreach (var io in XMPS.Instance.LoadedProject.Tags)
                //{
                //    if (io.Mode == oldTableName)
                //        io.Mode = newTableName;
                //}
                selectedNode.Text = newTableName;
                if (selectedNode.Tag is NodeInfo nodeInfo)
                {
                    nodeInfo.Info = newTableName;
                }
                xm.MarkProjectModified(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating resistance table: {ex.Message}","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsmAddBlock_Click(object sender, EventArgs e)
        {
            AddNewLogicBlock(tvProjects.SelectedNode);
        }
        private void AddNewLogicBlock(TreeNode blockNode, string updateName = "")
        {
            NodeInfo niBlockNode = (NodeInfo)blockNode.Tag;
            var blockName = xm.LoadedProject.AddBlock(niBlockNode, updateName);
            NodeInfo newNodeInfo = new NodeInfo();
            newNodeInfo.NodeType = NodeType.BlockNode;
            if (niBlockNode.Info == "UserFunctionBlock")
            {
                newNodeInfo.Info = "UDFLadder";
            }
            else if (niBlockNode.Info == "HIBlock")
            {
                newNodeInfo.Info = "HILadder"; 
            }
            else
            {
                newNodeInfo.Info = "Ladder";
            }
            TreeNode newNode = new TreeNode(blockName);
            newNode.Tag = newNodeInfo;
            blockNode.Nodes.Add(newNode);
            currentAddedNode = newNode;
            xm.MarkProjectModified(true);
            this.LogicalblockName = blockName;
        }
        private void tsmRequestAddReq_Click(object sender, EventArgs e)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "Modbus TCP Server Settings";
            TreeNode ModbusNode = tvProjects.SelectedNode;
            NodeInfo niModBusReqNode = (NodeInfo)ModbusNode.Tag;
            var ModbusNodeName = xm.LoadedProject.GetRequestName(niModBusReqNode);
            ModbusTCPServerUserControl userControl = new ModbusTCPServerUserControl(ModbusNodeName);
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK)
            {
                NodeInfo newNodeInfo = new NodeInfo();
                newNodeInfo.NodeType = NodeType.DeviceNode;
                newNodeInfo.Info = "ModbusRequest";
                TreeNode newNode = new TreeNode(ModbusNodeName);
                newNode.Tag = newNodeInfo;
                //Remove Addition of Salves in Tree 
                PerformTreeNodeActions(niModBusReqNode, ModbusNode, niModBusReqNode.Info.ToString());
            }
            OnGridDataChanged();
        }
        private void tsmAddSlave_Click(object sender, EventArgs e)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            TreeNode ModbusNode = tvProjects.SelectedNode;
            NodeInfo niModBusSlaveNode = (NodeInfo)ModbusNode.Tag;
            var ModbusNodeName = xm.LoadedProject.GetSlaveName(niModBusSlaveNode);

            if (niModBusSlaveNode.Info == "MODBUSRTUMaster")
            {
                tempForm.Text = "Modbus RTU Master Settings";
                ModbusRTUUserControl userControl = new ModbusRTUUserControl(ModbusNodeName);
                tempForm.Height = userControl.Height + 25;
                tempForm.Width = userControl.Width;
                tempForm.Controls.Add(userControl);
            }
            else if (niModBusSlaveNode.Info == "MODBUSRTUSlaves")
            {
                tempForm.Text = "Modbus RTU Slave Setting.";
                ModbusRTUSlaveUserControl userControlSlave = new ModbusRTUSlaveUserControl(ModbusNodeName);
                tempForm.Height = userControlSlave.Height + 25;
                tempForm.Width = userControlSlave.Width;
                tempForm.Controls.Add(userControlSlave);
            }
            else
            {
                tempForm.Text = "Modbus TCP Client Settings";
                ModbusTCPClientUserControl userControl = new ModbusTCPClientUserControl(ModbusNodeName);
                tempForm.Height = userControl.Height + 25;
                tempForm.Width = userControl.Width;
                tempForm.Controls.Add(userControl);
            }
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK)
            {
                NodeInfo newNodeInfo = new NodeInfo();
                newNodeInfo.NodeType = NodeType.DeviceNode;
                if (niModBusSlaveNode.Info == "MODBUSRTUMaster")
                {
                    newNodeInfo.Info = "ModbusRTUSlave";
                }
                else if (niModBusSlaveNode.Info == "MODBUSRTUSlaves")
                {
                    newNodeInfo.Info = "MODBUSRTUSlaves";
                }
                else
                {
                    newNodeInfo.Info = "ModbusTCPSlave";
                }
                TreeNode newNode = new TreeNode(ModbusNodeName);
                newNode.Tag = newNodeInfo;
                //Remove Addition of Salves in Tree 
                PerformTreeNodeActions(niModBusSlaveNode, ModbusNode, niModBusSlaveNode.Info.ToString());
            }
            OnGridDataChanged();

        }
        private void tsmAddPublish(object sender, EventArgs e)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            TreeNode Mqttpublish = tvProjects.SelectedNode;
            NodeInfo niMqttPublishBlockNode = (NodeInfo)Mqttpublish.Tag;
            var MqttNodeName = xm.LoadedProject.GetMqttName(niMqttPublishBlockNode);
            int count = Mqttpublish.Nodes.Count;
            count++;
            //Adding MQTT topics
            if (niMqttPublishBlockNode.Info == "MQTT Publish")
            {
                tempForm.Text = "Add Publish Parameters";
                PublishParameter userControl = new PublishParameter();
                tempForm.Height = userControl.Height + 25;
                tempForm.Width = userControl.Width;
                userControl.Text = "Add Publish Parameters";
                DialogResult uresult = userControl.ShowDialog();
                if (uresult == DialogResult.OK)
                    PerformTreeNodeActions(niMqttPublishBlockNode, Mqttpublish, niMqttPublishBlockNode.Info.ToString());
            }
            ResetContextMenu();
        }
        public void save(bool showMessage = true)
        {
            this.Cursor = Cursors.WaitCursor;
            if (xm.LoadedProject != null)
            {
                if (!ValidateAndInitializeProject())
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (!ValidateBacnetObjects())
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                try
                {
                    SaveProject();
                    if (showMessage)
                        tssStatusLabel_msg("Project Saved Sucessfully", 3000, "LimeGreen");
                }
                catch (Exception ex)
                {
                    if (!xm.LoadedProject.isChanged)
                    {
                        MessageBox.Show($"Please resolve below issues\n {string.Join(Environment.NewLine, ex.Message)}", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tssStatusLabel_msg("Error while saving : ", 3000, "OrangeRed");
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }
        private void MenuProjectSave_Click(object sender, EventArgs e)
        {
            save();
        }
        private void strpBtnSaveProject_Click(object sender, EventArgs e)
        {
            if (!ValidateAndInitializeProject())
            {
                return;
            }
            if (!ValidateBacnetObjects())
            {
                return;
            }
            save();
        }
        private void MenuProjectSaveAs_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (!ValidateAndInitializeProject())
            {
                this.Cursor = Cursors.Default;
                return;
            }
            if (!ValidateBacnetObjects())
            {
                this.Cursor = Cursors.Default;
                return;
            }
            try
            {
                if (!SaveAsProject())
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Please resolve below issues\n {string.Join(Environment.NewLine, ex.Message)}", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return;
            }
            if (xm.LoadedProject != null)
            { tssStatusLabel_msg("Project Saved Sucessfully", 3000, "LimeGreen"); }          //Creating Flag to check if not true
            this.Cursor = Cursors.Default;
        }
        private void tsmAddRemoteIO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tsmAddRemoteIO.ComboBox.Text.ToString() != "")
            {
                if (tsmAddRemoteIO.ComboBox.Text.ToString() == "Other")
                {
                    XMProForm tempForm = new XMProForm();
                    tempForm.StartPosition = FormStartPosition.CenterParent;
                    tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    tempForm.Text = "Add Other Remote IO Model";
                    TreeNode IONode = tvProjects.SelectedNode;
                    NodeInfo niRemoteIONode = (NodeInfo)IONode.Tag;
                    var IONodeName = tsmAddRemoteIO.ComboBox.Text.ToString();
                    IOConfigAdd userControl = new IOConfigAdd();
                    tempForm.Height = userControl.Height + 25;
                    tempForm.Width = userControl.Width;
                    tempForm.Controls.Add(userControl);
                    var frmTemp = this.ParentForm as frmMain;
                    DialogResult result = tempForm.ShowDialog(frmTemp);
                    if (result == DialogResult.OK)
                    {
                        NodeInfo newNodeInfo = new NodeInfo();
                        newNodeInfo.NodeType = NodeType.ListNode;
                        newNodeInfo.Info = tsmAddRemoteIO.ComboBox.Text.ToString();
                        TreeNode newNode = new TreeNode(IONodeName);
                        newNode.Tag = newNodeInfo;
                        IONode.Nodes.Add(newNode);
                    }
                }
                else
                {
                    NodeInfo newNodeInfo = new NodeInfo();
                    newNodeInfo.NodeType = NodeType.ListNode;
                    TreeNode IONode = tvProjects.SelectedNode;
                    NodeInfo niRemoteIONode = (NodeInfo)IONode.Tag;
                    var IONodeName = tsmAddRemoteIO.ComboBox.Text.ToString();
                    var added = xm.LoadedProject.Tags.Where(d => d.Model != null && d.Model != "" && d.Model.ToString().Contains(IONodeName.ToString())).GroupBy(x => x.Model);
                    if (added.Count() == 0)
                    {
                        newNodeInfo.Info = tsmAddRemoteIO.ComboBox.Text.ToString();
                        IONodeName = tsmAddRemoteIO.ComboBox.Text.ToString();// xm.LoadedProject.GetSlaveName(niRemoteIONode);
                    }
                    else
                    {
                        int nextcounter = 1;
                        if (added.Count() > 1)
                        {
                            nextcounter = Convert.ToInt32(added.Max(a => a.Key).ToString().Replace(IONodeName.ToString() + "_", "")) + 1;
                        }
                        newNodeInfo.Info = tsmAddRemoteIO.ComboBox.Text.ToString() + "_" + nextcounter;
                        IONodeName = tsmAddRemoteIO.ComboBox.Text.ToString() + "_" + nextcounter;// xm.LoadedProject.GetSlaveName(niRemoteIONode);
                    }
                    string selectedModel = tsmAddRemoteIO.ComboBox.Text.ToString();
                    var model = RemoteModule.List.Find(x => x.Name.Equals(selectedModel));
                    if ((xm.LoadedProject.Tags.Where(d => d.Model != "" && d.IoList != IOListType.Default).Count() + model.SupportedTypesAndIOs[0].Units) > 80)
                    {
                        MessageBox.Show("Exceeding count of permitted IO's, can't add more than 80 IO's", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        TreeNode newNode = new TreeNode(IONodeName);
                        newNode.Tag = newNodeInfo;
                        IONode.Nodes.Add(newNode);
                    }
                }
            }
            ResetContextMenu();
        }
        private void tsmAddExpansionIO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tsmAddExpansionIO.ComboBox.Text.ToString() != "")
            {
                NodeInfo newNodeInfo = new NodeInfo();
                newNodeInfo.NodeType = NodeType.ListNode;
                TreeNode IONode = tvProjects.SelectedNode;
                NodeInfo niRemoteIONode = (NodeInfo)IONode.Tag;
                var IONodeName = tsmAddExpansionIO.ComboBox.Text.ToString();
                var added = xm.LoadedProject.Tags.Where(d => d.Model != null && d.Model != "" && d.Model.ToString().Contains(IONodeName.ToString())).GroupBy(x => x.Model);
                if (added.Count() == 0)
                {
                    newNodeInfo.Info = tsmAddExpansionIO.ComboBox.Text.ToString();
                    IONodeName = tsmAddExpansionIO.ComboBox.Text.ToString();
                }
                else
                {
                    int nextcounter = 1;
                    if (added.Count() > 1)
                    {
                        nextcounter = Convert.ToInt32(added.Max(a => a.Key).ToString().Replace(IONodeName.ToString() + "_", "")) + 1;
                    }
                    newNodeInfo.Info = tsmAddExpansionIO.ComboBox.Text.ToString() + "_" + nextcounter;
                    IONodeName = tsmAddExpansionIO.ComboBox.Text.ToString() + "_" + nextcounter;
                }
                string selectedModel = tsmAddExpansionIO.ComboBox.Text.ToString();
                var model = RemoteModule.List.Find(x => x.Name.Equals(selectedModel));
                if ((xm.LoadedProject.Tags.Where(d => d.Model != "" && d.IoList != IOListType.Default).Count() + model.SupportedTypesAndIOs[0].Units) > 80)
                {
                    MessageBox.Show("Exceeding count of permitted IO's, can't add more than 80 IO's", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    TreeNode newNode = new TreeNode(IONodeName);
                    newNode.Tag = newNodeInfo;
                    IONode.Nodes.Add(newNode);
                }
            }
            ResetContextMenu();
        }
        private void strpBtnPrvScreen_Click(object sender, EventArgs e)
        {
            NavigateToPrevious();
        }
        private void strpBtnNxtScreen_Click(object sender, EventArgs e)
        {
            NavigateToNext();
        }
        private void tsmDelete_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = tvProjects.SelectedNode;
            if (treeNode.Text == "MODBUS RTU Master")
            {
                var masters = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").ToList();

                foreach (var master in masters)
                {
                    if (master is MODBUSRTUMaster modbusMaster)
                    {
                        modbusMaster.Slaves.Clear();
                    }
                    xm.LoadedProject.Devices.Remove(master);
                }
                xm.LoadedProject.RS485Mode = null;
                xm.LoadedProject.SlaveID = 0;

                tvProjects.Nodes.Remove(treeNode);
                xm.MarkProjectModified(true);
            }
            else if (treeNode.Text == "MODBUS RTU Slaves")
            {
                var modBUSRTUSlaves = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").ToList();
                foreach (var slave in modBUSRTUSlaves)
                {
                    xm.LoadedProject.Devices.Remove(slave);
                }

                if (xm.LoadedProject.RS485Mode == "Slave")
                {
                    xm.LoadedProject.RS485Mode = null;
                    xm.LoadedProject.SlaveID = 0;
                }
                tvProjects.Nodes.Remove(treeNode);
                xm.MarkProjectModified(true);
            }

            // Show RS485 grid if no child under RS485
            var rs485Node = tvProjects.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "RS485");
            if (rs485Node != null && rs485Node.Nodes.Count == 0)
            {
                frmGridLayout grid = new frmGridLayout("COMDeviceForm");
                grid.Show();
            }
            else
            {
                List<XMIOConfig> tempTagsList = xm.LoadedProject.Tags.Where(r => r.Model != null && r.Model != "" && r.Model.ToString() == tvProjects.SelectedNode.Text).ToList();
                List<string> data = tempTagsList.Select(a => a.LogicalAddress).ToList();
                if (data.Count > 0 && !string.IsNullOrEmpty(data[0]) && data[0].Length >= 6)
                {
                    data.Add(data[0].Substring(0, 6)); // For delete word address
                }

                bool isInUsed = false;
                List<string> errorMessage = new List<string>();

                foreach (string tags in data.Distinct())
                {
                    //check in BacNetschedule object do not allow to  delete.
                    if (xm.LoadedProject.BacNetIP != null)
                    {
                        Schedule isAnySchedule = XMProValidator.CheckInScheduleObject(tags);
                        if (isAnySchedule != null)
                        {
                            MessageBox.Show($"{tags} are already used in {isAnySchedule.ObjectName} schedule object", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // MODBUS-related checks
                    ValidateModbus(tags, errorMessage, ref isInUsed);

                    // MQTT-related checks
                    ValidateMQTT(tags, errorMessage, ref isInUsed);

                    // Logical Block check
                    var logicalBlock = XMProValidator.CheckInLogicalBlock(tags);
                    if (!string.IsNullOrEmpty(logicalBlock))
                    {
                        errorMessage.Add($"From current node {tags} used in {logicalBlock}{Environment.NewLine}");
                        isInUsed = true;
                    }
                }
                foreach (string tags in data)
                {
                    if (xm.LoadedProject.HsioBlock != null)
                    {
                        var blocks = XMProValidator.CheckInHSIOBlocks(tags);
                        if (blocks != null && blocks.Count() > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show($"{string.Join(Environment.NewLine, errorMessage)}\n" +
                                $"Tag are already used in HSIOs You Want to Remove it \n" +
                                $"You want proceed to delete {tvProjects.SelectedNode.Text} block", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (dialogResult == DialogResult.Yes)
                            {
                                isInUsed = false;
                                foreach (var block in blocks)
                                {
                                    var hsioBlockToUpdates = block.HSIOBlocks.Where(a => a.Value.Equals(tags));
                                    foreach (var hsioBlockToUpdate in hsioBlockToUpdates)
                                    {
                                        if (hsioBlockToUpdate != null)
                                        {
                                            hsioBlockToUpdate.Value = "???";
                                        }
                                    }
                                }
                            }
                            else
                                return;
                        }
                    }
                }
                if (!isInUsed)
                {
                    BacNetObjectHelper.ClearBacNetObject(data);
                    DeleteNode(treeNode);
                }
                else
                {
                    if (MessageBox.Show($"{string.Join(Environment.NewLine, errorMessage)}\n You want proceed to delete {tvProjects.SelectedNode.Text} block", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        BacNetObjectHelper.ClearBacNetObject(data);
                        DeleteNode(treeNode);
                    }
                }
                TreeNode currentDelNode = tvProjects.SelectedNode;
                NodeInfo currentDelNodeInfo = (NodeInfo)currentDelNode.Tag;
                PerformTreeNodeActions(currentDelNodeInfo, currentDelNode, currentDelNodeInfo.Info.ToString());
                OnGridDataChanged();
            }
        }
        private void ValidateModbus(string tags, List<string> errorMessage, ref bool isInUsed)
        {
            var modbusRTU = XMProValidator.CheckInModbusRTUMaster(tags);
            if (modbusRTU != null)
            {
                errorMessage.Add($"From current node {tags} used in ModbusRTUMaster slave name {modbusRTU.Name}{Environment.NewLine}");
                isInUsed = true;
            }
            var modbusRTUSlaveSlaves = XMProValidator.CheckInModbusRTUSlavesSlave(tags);
            if (modbusRTUSlaveSlaves != null)
            {
                errorMessage.Add($"From current node {tags} used in ModbusRTUMaster slave name {modbusRTUSlaveSlaves.Name}{Environment.NewLine}");
                isInUsed = true;
            }
            var modbusTCPClient = XMProValidator.CheckInModbusTCPClient(tags);
            if (modbusTCPClient != null)
            {
                errorMessage.Add($"From current node {tags} used in MODBUSTCPClient slave name {modbusTCPClient.Name}{Environment.NewLine}");
                isInUsed = true;
            }

            var modbusTCPServer = XMProValidator.CheckInModbusServerRequest(tags);
            if (modbusTCPServer != null)
            {
                errorMessage.Add($"From current node {tags} used in MODBUSTCPServerRequest slave name {modbusTCPServer.Name}{Environment.NewLine}");
                isInUsed = true;
            }
        }
        private void ValidateMQTT(string tags, List<string> errorMessage, ref bool isInUsed)
        {
            if (XMProValidator.CheckInPublishTopics(tags))
            {
                errorMessage.Add($"From current node {tags} used in Publish Topic Request{Environment.NewLine}");
                isInUsed = true;
            }

            if (XMProValidator.CheckInSubscribeTopics(tags))
            {
                errorMessage.Add($"From current node {tags} used in Subscribe Topic Request{Environment.NewLine}");
                isInUsed = true;
            }
        }
        private void tsmDeleteBlock_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = tvProjects.SelectedNode;
            if ((((NodeInfo)treeNode.Tag).Info == "Ladder") && xm.LoadedProject.Blocks.Where(T => T.Type == "LogicBlock").Count() == 1)
            {
                MessageBox.Show("Atleast one block is required in the project, can't delete this block", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DeleteNode(treeNode);
            bool isTreeNodeDelete = true;
            string Blockname = tvProjects.SelectedNode.Text;
            var checkinmain = xm.LoadedProject.MainLadderLogic.Where(R => R.Contains(tvProjects.SelectedNode.Text)).FirstOrDefault();
            if (checkinmain != null && checkinmain.Count() > 0)
            {
                isTreeNodeDelete = false;
            }
            if (isTreeNodeDelete)
            {
                var logicBlocksRem = xm.LoadedProject.Blocks.Where(d => d.Type.Equals("LogicBlock")).ToList();
                foreach (Block b in logicBlocksRem)
                {
                    var checkinmain1 = xm.LoadedProject.MainLadderLogic.Where(R => R.Contains(b.Name)).FirstOrDefault();
                    string input = b.Name;
                    string pattern = @"^LogicBlock\d{2}$";

                    if (!Regex.IsMatch(input, pattern) && checkinmain1 != null && checkinmain1.Count() > 0)
                    {
                        break;
                    }
                }
            }
        }
        private void tvBlocks_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (tvBlocks.SelectedNode != null)
            {
                this.DoDragDrop(tvBlocks.SelectedNode.Text.ToString(), DragDropEffects.Copy);
            }
        }
        private void strpBtnHelp_Click(object sender, EventArgs e)
        {
            ConfigInterpreter1 config = new ConfigInterpreter1(xm);
            ConfigInterpreter configMain = new ConfigInterpreter(xm);
            List<byte> configSettingsHex = new List<byte> { };
            configSettingsHex.AddRange(configMain.GetSettingsByteArray());
            configSettingsHex.AddRange(config.GetHexValues());
            MessageBox.Show(string.Join("-", configSettingsHex), "Configuration Interpreter Output in Bytes");
        }
        private void strpBtnOnlineMonitor_Click(object sender, EventArgs e)
        {
            OnlineMonitor();
        }
        private void OnlineMonitorTimer_Tick(object sender, EventArgs e)
        {
            if (!OnlineMonitoringHelper.HoldOnlineMonitor)
            {
                ShowOnlineMonitor();
            }
        }
        public void ShowOnlineMonitor()
        {
            if (!(xm.CurrentScreen.StartsWith("LadderForm") || xm.CurrentScreen.StartsWith("MainForm")))
                return;
            int TARGET_CYCLE_TIME_MS = 100;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            if ((xm.LoadedProject.MainLadderLogic.Where(e => e.Contains(xm.CurrentScreen.Split('#')[1]) && !e.StartsWith("'")).Count() == 0) && XMPS.Instance.CurrentScreen.Split('#')[1] != "Main")
                return;
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect();
            if (xm.CurrentScreen.Contains("MainForm"))
                ShowOnlineMonitoringMainForm();
            else if (!omh.SendActiveRungAddress())
            {
                tssStatusLabel_show("Check connection, not able to connect to PLC, Logging out now", "Red");
                LogOutCommonMessage();
            }
            string logicaladdress = XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == XMPS.Instance.LoadedProject.PLCStatusTag).Select(t => t.Tag).FirstOrDefault();
            if (logicaladdress != null && xm.PlcStatus == "LogIn")
            {
                //string status = OnlineMonitoringStatus.AddressValues.ContainsKey(logicaladdress.ToString()) ? OnlineMonitoringStatus.AddressValues[logicaladdress.ToString()]
                //                                      : OnlineMonitoringStatus.AddressValues[logicaladdress];
                string status = OnlineMonitoringStatus.AddressValues.ContainsKey(logicaladdress.ToString())
                ? OnlineMonitoringStatus.AddressValues[logicaladdress.ToString()]
                : OnlineMonitoringStatus.AddressValues.ContainsKey(XMPS.Instance.LoadedProject.PLCStatusTag)
                    ? OnlineMonitoringStatus.AddressValues[XMPS.Instance.LoadedProject.PLCStatusTag]
                    : "Invalid";
                if (status != "Invalid" && status != "")
                {
                    List<Tuple<string, string>> tplErrorValues = new List<Tuple<string, string>>();
                    ///Send the error status address in every cycle to check status of CPU
                    foreach (string errorTagAddress in XMPS.Instance.LoadedProject.ErrorStatusTags)
                    {
                        XMIOConfig errTagDtl = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == errorTagAddress).FirstOrDefault();
                        if (Enum.GetNames(typeof(AddressDataTypes)).Any(name => name.Equals(errTagDtl.Label, StringComparison.OrdinalIgnoreCase)))
                        {
                            string errvalue = OnlineMonitoringStatus.AddressValues.ContainsKey(errTagDtl.Tag.ToString()) ? OnlineMonitoringStatus.AddressValues[errTagDtl.Tag.ToString()]
                                                          : OnlineMonitoringStatus.AddressValues[errorTagAddress];
                            tplErrorValues.Add(Tuple.Create(errorTagAddress, errvalue));
                        }
                    }
                    CheckPLCStatus(status, tplErrorValues);
                }
            }
            onlinemonitoringIter++;
            stopwatch.Stop();
            int delayTime = Math.Max(0, TARGET_CYCLE_TIME_MS - (int)stopwatch.ElapsedMilliseconds);
            if (delayTime > 0)
            {
                Thread.Sleep(delayTime);
            }
        }

        public void CheckPLCStatus(string status, List<Tuple<string, string>> ErrorStatus)
        {
            try
            {
                // Validate input parameters
                if (ErrorStatus == null)
                {
                    ErrorStatus = new List<Tuple<string, string>>();
                }

                // Default status and color if inputs are invalid
                string safeStatus = status ?? string.Empty;
                string color = "green";
                string expErrorMessage = string.Empty;

                try
                {
                    // Get expansion error message with failsafe
                    expErrorMessage = CommonFunctions.GetExpantionErrorMessage(ErrorStatus);
                }
                catch (Exception)
                {
                    // Silent failure - continue with empty error message
                    expErrorMessage = string.Empty;
                }

                // Safely check XMPS instance and LoadedProject
                bool validInstance = XMPS.Instance?.LoadedProject != null;
                string currentPlcStatus = validInstance ? XMPS.Instance.LoadedProject._plcStatus : string.Empty;

                // Process status based on PLC state
                if (safeStatus == "0" && (!validInstance || currentPlcStatus != safeStatus))
                {
                    safeStatus = "PLC is in Stop mode " + expErrorMessage;
                    color = !string.IsNullOrEmpty(expErrorMessage) ? "red" : "Gold";

                    // Only update status mode if it's the basic message
                    if (safeStatus.Trim().StartsWith("PLC is in Stop mode"))
                    {
                        try
                        {
                            UpdatePLCStatusMode(safeStatus);
                        }
                        catch (Exception)
                        {
                            // Silent failure for update method
                        }
                    }
                }
                else if (safeStatus == "1" && (!validInstance || currentPlcStatus != safeStatus))
                {
                    safeStatus = "PLC is in Run mode " + expErrorMessage;
                    color = !string.IsNullOrEmpty(expErrorMessage) ? "red" : "green";

                    // Only update status mode if it's the basic message (but check for prefix)
                    if (safeStatus.StartsWith("PLC is in Run mode"))
                    {
                        try
                        {
                            UpdatePLCStatusMode(safeStatus);
                        }
                        catch (Exception)
                        {
                            // Silent failure for update method
                        }
                    }
                }

                try
                {
                    // Call the status message method with failsafe
                    tssStatusLabel_msg(safeStatus, 3000, color);
                }
                catch (Exception)
                {
                    // Silent failure for status message method
                }

                try
                {
                    // Set blinking with failsafe
                    if (statusIndicator != null)
                    {
                        statusIndicator.IsBlinking = true;
                    }
                }
                catch (Exception)
                {
                    // Silent failure for blinking
                }
            }
            catch (Exception)
            {
                // Global exception handler
                // Last resort attempt to show something to the user
                try
                {
                    tssStatusLabel_msg("PLC Status unavailable", 3000, "gray");
                }
                catch
                {
                    // Completely silent failure if even this fails
                }
            }
        }




        private void UpdatePLCStatusMode(string status)
        {
            if (status.Contains("PLC is in Stop mode"))
            {
                MenuModePLCStart.Enabled = true;
                MenuModeLogout.Enabled = true;
                strpBtnLogout.Enabled = true;
                MenuModeLogin.Enabled = false;
                strpBtnLogin.Enabled = false;
                MenuModePLCStop.Enabled = false;
                MenuModePLCResetOrigin.Enabled = true;
                MenuModePLCResetCold.Enabled = true;
                MenuModePLCResetwarm.Enabled = true;
                MenuModeUpldSourceCode.Enabled = true;
                statusIndicator.Status = false;

            }
            else
            {
                MenuModePLCStart.Enabled = false;
                MenuModeLogout.Enabled = true;
                strpBtnLogout.Enabled = true;
                MenuModeLogin.Enabled = false;
                strpBtnLogin.Enabled = false;
                MenuModePLCStop.Enabled = true;
                MenuModePLCResetOrigin.Enabled = true;
                MenuModePLCResetCold.Enabled = true;
                MenuModePLCResetwarm.Enabled = true;
                MenuModeUpldSourceCode.Enabled = true;
                statusIndicator.Status = true;
            }

        }
        public void DisableOnlineModeFromHSIO()
        {
            LogOutCommonMessage();
        }

        private void LogOutCommonMessage()
        {
            OnlineMonitorTimer.Stop();
            OnlineMonitoringHelper.HoldOnlineMonitor = true;
            OnlineMonitoring.DestroyInstance();
            NormaliseMenuesPostLogout();
            string errmsg = XMPS2000.CommonFunctions.GetEasyConnection(xm._connectedIPAddress);
            tssStatusLabel_show(errmsg, "Red");
            MessageBox.Show(errmsg, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (currentUDFBElements.Item1 != null)
            {
                ResetUDFBOrigional();
            }
        }

        private bool OnlineButtonOn = false;
        OnlineMonitoringHelper omh = OnlineMonitoringHelper.Instance;
        public void OnlineMonitor()
        {
            if (!OnlineButtonOn)
            {
                if (xm.LoadedProject.MainLadderLogic.Where(b => !b.StartsWith("'") && b.Contains(xm.CurrentScreen.Split('#')[1])).Count() > 0)
                {
                    xm.onlinemonitoring = true;
                    LadderDrawing.OnlineMonitoringStatus.isOnlineMonitoring = true;
                    strpBtnOnlineMonitor.BackColor = Color.Green;
                    OnlineMonitorTimer.Start();
                    OnlineButtonOn = true;
                }
                else
                {
                    MessageBox.Show("This Block is not added in main block. Online monitoring not available for current block", "Online Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                xm.onlinemonitoring = false;
                LadderDrawing.OnlineMonitoringStatus.isOnlineMonitoring = false;
                strpBtnOnlineMonitor.BackColor = Color.White;
                OnlineMonitorTimer.Stop();
                OnlineButtonOn = false;
            }
        }
        private void strpBtnCut_Click(object sender, EventArgs e)
        {
            Cut();
            GetCanvasForUndoRedo();
        }
        private void strpBtnCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void strpBtnPaste_Click(object sender, EventArgs e)
        {
            Paste();
            LadderCanvas.RefreshCanvas();
            GetCanvasForUndoRedo();
        }
        private void strpBtnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }
        private void strpBtnRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }
        private void strpBtnDelete_Click(object sender, EventArgs e)
        {
            if (xm.CurrentScreen.Contains("LadderForm#"))
            {
                if (!(xm.PlcStatus == "LogIn"))
                {

                    var currentBlockForm = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
                    LadderCanvas curCanvas = currentBlockForm.getLadderEditor().getCanvas();
                    if (curCanvas.fullyRungElements.Count > 0)
                    {
                        int rungNo = 0;
                        foreach (LadderElement ladderElement in curCanvas.fullyRungElements)
                        {
                            rungNo++;
                            bool islast = curCanvas.fullyRungElements.Count == rungNo ? true : false;
                            LadderDesign.ClickedElement = ladderElement;
                            curCanvas.DeleteRungbyContextMenu(islast);
                            LadderDesign.ClickedElement = null;
                        }
                        curCanvas.Invalidate();
                        curCanvas.Update();
                        curCanvas.Refresh();
                        curCanvas.fullyRungElements.Clear();
                        curCanvas.selectedElements.Clear();
                    }
                    else
                        curCanvas.DeleteWithCounter(new KeyEventArgs(Keys.Delete));
                }
            }
            else if (xm.LoadedScreens[xm.CurrentScreen].GetType().ToString().Contains("frmGridLayout"))
            {
                if (xm.CurrentScreen.ToString().Contains("frmAddResistanceValue") || xm.CurrentScreen.ToString().Contains("ResistanceValue"))
                {
                    var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                    curgridform.deleteResistanceValue();
                }
                else
                {
                    var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                    curgridform.deleteTagAndModbus();
                }                  
            }
        }

        //For validating bacnet objects for old projects.
        private bool ValidateBacnetObjects()
        {
            if (xm.LoadedProject == null || XMPS.Instance.LoadedProject.BacNetIP == null)
                return true;
            var analogValues = XMPS.Instance.LoadedProject.BacNetIP.AnalogIOValues;
            if (analogValues != null)
            {
                foreach (var analog in analogValues)
                {
                    if (string.IsNullOrWhiteSpace(analog.RelinquishDefault) || analog.RelinquishDefault == "0" || !decimal.TryParse(analog.RelinquishDefault, out decimal relDefault))
                    {
                        continue;
                    }
                    if (relDefault < analog.MinPresValue || relDefault > analog.MaxPresValue)
                    {
                        MessageBox.Show($"Analog '{analog.ObjectName}': Relinquish Default value {relDefault} is out of range.","Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            var calendars = XMPS.Instance.LoadedProject.BacNetIP.Calendars;
            if (calendars != null && calendars.Count > 0)
            {
                foreach (var calendar in calendars)
                {
                    if (calendar?.Events != null && calendar.Events.Count > 0)
                    {
                        var duplicatePriorities = calendar.Events.GroupBy(e => e.Priority).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                        if (duplicatePriorities.Any())
                        {
                            string duplicateList = string.Join(", ", duplicatePriorities);
                            MessageBox.Show(
                                $"Calendar '{calendar.ObjectName}' contains duplicate priority values: {duplicateList}. " + $"Each event within a calendar must have a unique priority.","Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Error
                            );
                            return false;
                        }
                    }
                }
            }
            var schedules = XMPS.Instance.LoadedProject.BacNetIP.Schedules; 
            if (schedules != null && schedules.Count > 0)
            {
                foreach (var schedule in schedules)
                {
                    if (schedule?.specialEvents != null && schedule.specialEvents.Count > 0)
                    {
                        var timeSlots = new HashSet<string>();
                        foreach (var ev in schedule.specialEvents)
                        {
                            foreach (var slot in ev.EventValues)
                            {
                                string start = slot.StartTime;
                                string end = slot.EndTime;
                                string combo = start + "-" + end;
                                if (timeSlots.Contains(combo))
                                {
                                    MessageBox.Show($"Schedule '{schedule.ObjectName}' has duplicate time slot: {start} - {end}.", "Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Error
                                    );
                                    return false; 
                                }
                                timeSlots.Add(combo);
                            }
                        }
                    }
                }
            }
            return true; 
        }
        private bool CheckMissingUDFBs()
        {
            string baseLibraryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"MessungSystems", "XMPS2000", "Library");
            string libraryFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD")? "XBLDLibraries": "XMLibraries";
            string libraryPath = Path.Combine(baseLibraryPath, libraryFolder);
            List<string> missingUdfbs = new List<string>();
            var udfbUsages = XMPS.Instance.LoadedProject.LogicRungs.Where(r => !string.IsNullOrEmpty(r.OpCodeNm)).Select(r => r.OpCodeNm).Distinct().ToList();
            foreach (string udfbName in udfbUsages)
            {
                bool isUdfb = XMPS.Instance.LoadedProject.UDFBInfo.Any(u => u.UDFBName.Equals(udfbName, StringComparison.OrdinalIgnoreCase));
                if (isUdfb)
                {
                    string udfbFilePath = Path.Combine(libraryPath, $"{udfbName} Logic.csv");
                    if (!File.Exists(udfbFilePath))
                    {
                        missingUdfbs.Add(udfbName);
                    }
                }
            }
            if (missingUdfbs.Count > 0)
            {
                string missingList = string.Join("\n", missingUdfbs.Select(u => "- " + u));
                MessageBox.Show($"The following Library's are missing from the {libraryFolder} folder:\n{missingList}","Missing Library UDFB's",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true; 
        }
        private void Compile()
        {
            if (XMPS.Instance.instructionsList == null || XMPS.Instance.instructionsList.Count == 0)
            {
                MessageBox.Show("Please reload instruction file once", "XMPS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RefreshGridFormIfEditing("MODBUSRTUSlavesForm");
            xm.isCompilied = true;
            if (!validateWordAddresses())
                return;
            if (!ValidateAndInitializeProject())
            {
                this.Cursor = Cursors.Default;
                return;
            }
            if (!ValidateBacnetObjects())
            {
                this.Cursor = Cursors.Default;
                return;
            }
            if (xm.LoadedProject.Tags.Any(t => t.Retentive && !t.RetentiveAddress.Contains(":")))
            {
                MessageBox.Show("Please check retentive address of tags", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                textBoxError.Controls.Clear();
                SaveProject();
                ExportFromUDFB(null, false, true);
            }
            catch
            {
                tssStatusLabel_msg("Error while Compile : ", 3000, "OrangeRed");
            }
            xm.isCompilied = false;
        }
        public void ExportFromUDFB(string selectedNode = null, bool calledFromUI = false, bool exportAll = false)
        {
            try
            {
                save();
                string baseLibraryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"MessungSystems", "XMPS2000", "Library");
                string projectLibraryFolder = xm.LoadedProject.PlcModel.StartsWith("XBLD")? "XBLDLibraries": "XMLibraries";
                string libraryPath = Path.Combine(baseLibraryPath, projectLibraryFolder);
                List<string> udfbNames = xm.LoadedProject.UDFBInfo.Select(u => u.UDFBName).ToList();
                int exportedCount = 0;
                int skippedCount = 0;
                string changeSelectedNode = selectedNode;
                if (!string.IsNullOrEmpty(selectedNode))
                {
                    int lastSpaceIndex = selectedNode.LastIndexOf(' ');
                    changeSelectedNode = lastSpaceIndex >= 0 ? selectedNode.Substring(0, lastSpaceIndex) : selectedNode;
                }
                foreach (string udfbName in udfbNames)
                {
                    if (!exportAll && udfbName != changeSelectedNode) continue;
                    try
                    {
                        string blockNameWithLogic = udfbName + " Logic";
                        string csvFileName = blockNameWithLogic + ".csv";
                        string filePath = Path.Combine(libraryPath, csvFileName);
                        if (File.Exists(filePath))
                        {
                            var curBlock = xm.LoadedProject.Blocks.FirstOrDefault(u => u.Name.Equals(blockNameWithLogic, StringComparison.OrdinalIgnoreCase));
                            if (curBlock == null)
                            {
                                curBlock = xm.LoadedProject.Blocks.FirstOrDefault(u => u.Name.Equals(udfbName, StringComparison.OrdinalIgnoreCase));
                                if (curBlock == null)
                                {
                                    skippedCount++;
                                    continue; // Skip this UDFB if block not found
                                }
                            }
                            if (curBlock.Elements.Count == 0)
                            {
                                skippedCount++;
                                continue; // Skip empty blocks
                            }
                            List<string> curBlockRungs = curBlock.Elements;
                            List<string> curBlockComments = curBlock.Comments;
                            List<string> addressList = new List<string>();
                            if (curBlockRungs.Count > 0)
                            {
                                foreach (string currung in curBlockRungs)
                                {
                                    foreach (string inputs in currung.Split(' ').Where(x => x.Contains("IN:") || x.Contains("OP:")))
                                    {
                                        string opcode = currung.Substring(currung.IndexOf("OPC:") + 4, 4);
                                        if (inputs.Contains("IN:"))
                                        {
                                            addressList.Add(inputs.Replace("IN:", ""));
                                            if (opcode == "0390")
                                            {
                                                string[] parts = inputs.Replace("IN:", "").Replace("~", "").Replace("]", "").Replace(")", "").Replace(";", "").Split(':');
                                                if (parts.Length > 1 && int.TryParse(parts[1], out int secondPart))
                                                {
                                                    for (int j = 1; j < 16; j++)
                                                    {
                                                        int lastTagAdd = secondPart + j;
                                                        string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                                                        addressList.Add(endAdd);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            addressList.Add(inputs.Replace("OP:", ""));
                                            if (opcode == "03A2")
                                            {
                                                string[] parts = inputs.Replace("OP:", "").Replace("~", "").Replace("]", "").Replace(")", "").Replace(";", "").Split(':');
                                                if (parts.Length > 1 && int.TryParse(parts[1], out int secondPart))
                                                {
                                                    for (int j = 1; j < 16; j++)
                                                    {
                                                        int lastTagAdd = secondPart + j;
                                                        string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                                                        addressList.Add(endAdd);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (!xm.LoadedScreens.ContainsKey($"UDFLadderForm#{blockNameWithLogic}"))
                            {
                                skippedCount++;
                                continue;
                            }
                            LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{blockNameWithLogic}"];
                            LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
                            foreach (LadderElement ld in LadderDesign.Active.Elements)
                            {
                                foreach (LadderElement ld1 in ld.Elements)
                                {
                                    if (ld1.CustomType != "LadderDrawing.Comment" && ld1.CustomType != "LadderDrawing.FunctionBlock")
                                    {
                                        if (ld1.CustomType == "LadderDrawing.DummyParallelParent")
                                        {
                                            List<LadderElement> ld2 = ld1.Elements;
                                            foreach (LadderElement ld3 in ld2)
                                            {
                                                GetAddFromParalledElement(ld3, ref addressList);
                                            }
                                        }
                                        else if (ld1.CustomType == "LadderDrawing.Coil")
                                        {
                                            GetAddFromParalledElement(ld1, ref addressList);
                                        }
                                        else
                                        {
                                            if (ld1.Attributes.Any(t => t.Name.Equals("LogicalAddress")) && ld1.Attributes["LogicalAddress"].ToString() != "")
                                            {
                                                addressList.Add(ld1.Attributes["LogicalAddress"].ToString());
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(ld1.Attributes["caption"].ToString()) && !ld1.Attributes["caption"].ToString().Equals("???"))
                                                {
                                                    var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(ld1.Attributes["caption"].ToString()));
                                                    if (tag != null && !string.IsNullOrEmpty(tag.LogicalAddress))
                                                    {
                                                        addressList.Add(tag.LogicalAddress);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (File.Exists(filePath))
                            {
                                FileAttributes attributes = File.GetAttributes(filePath);
                                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                                {
                                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                                }
                            }
                            // Write the CSV file
                            using (StreamWriter writer = new StreamWriter(filePath, false))
                            {
                                writer.WriteLine("Block Type :- UDFB Block");
                                var currentUDFBData = xm.LoadedProject.UDFBInfo.FirstOrDefault(t => t.UDFBName.Equals(udfbName));

                                if (currentUDFBData != null)
                                {
                                    writer.WriteLine($"Inputs : {currentUDFBData.Inputs}");
                                    writer.WriteLine($"Outputs : {currentUDFBData.Outputs}");
                                    writer.WriteLine($"UDFBName : {currentUDFBData.UDFBName}");

                                    foreach (UserDefinedFunctionBlock ud in currentUDFBData.UDFBlocks)
                                    {
                                        writer.WriteLine($"DataType:{ud.DataType} Name:{ud.Name} Text:{ud.Text} Type:{ud.Type}");
                                    }
                                }
                                IEnumerable<(string rung, string comment)> data = curBlockRungs.Zip(curBlockComments, (rung, comment) => (rung, comment));
                                foreach ((string rung, string comment) in data)
                                {
                                    writer.WriteLine($"{rung}======{comment}");
                                }
                                writer.WriteLine("User Defined Tags");
                                List<XMIOConfig> userDefinedTags = new List<XMIOConfig>();
                                foreach (string logicalAdd in addressList)
                                {
                                    if (!logicalAdd.StartsWith("I") && !logicalAdd.StartsWith("Q") && logicalAdd != "")
                                    {
                                        XMIOConfig tag = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == logicalAdd.Replace("~", "").Replace("]", "").Replace(")", "").Replace(";", "")).FirstOrDefault();
                                        if (tag != null && !userDefinedTags.Contains(tag))
                                        {
                                            userDefinedTags.Add(tag);
                                        }
                                    }
                                }
                                var modelTags = xm.LoadedProject.Tags.Where(D => D.Model == udfbName + " Tags").ToList();
                                foreach (var modelTag in modelTags)
                                {
                                    if (!userDefinedTags.Contains(modelTag))
                                    {
                                        userDefinedTags.Add(modelTag);
                                    }
                                }
                                foreach (var tag in userDefinedTags)
                                {
                                    StringBuilder line = new StringBuilder();
                                    line.Append($"Model: {tag.Model}, ");
                                    line.Append($"Label: {tag.Label}, ");
                                    line.Append($"LogicalAddress: {tag.LogicalAddress}, ");
                                    line.Append($"Tag: {tag.Tag}, ");
                                    line.Append($"IOList: {tag.IoList}, ");
                                    line.Append($"Type: {tag.Type}, ");
                                    line.Append($"InitialValue: {tag.InitialValue}, ");
                                    line.Append($"Retentive: {tag.Retentive}, ");
                                    line.Append($"ShowLogicalAddress: {tag.ShowLogicalAddress}, ");
                                    line.Append($"RetentiveAddress: {tag.RetentiveAddress}, ");
                                    line.Append($"Mode: {tag.Mode}, ");
                                    line.Append($"Editable: {tag.Editable}, ");
                                    line.Append($"Key: {tag.Key}, ");
                                    line.Append($"ActualName: {tag.ActualName}");
                                    writer.WriteLine(line.ToString());
                                }
                            }
                            File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.ReadOnly);
                            exportedCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting UDFB '{udfbName}': {ex.Message}", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        skippedCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during UDFB export: {ex.Message}", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validateWordAddresses()
        {
            skippedTagsList.Clear();
            var tags = xm.LoadedProject.Tags.Where(t => t.LogicalAddress.StartsWith("W4")).Reverse().ToList();
            var lastTag = xm.LoadedProject.Tags.LastOrDefault();
            if (lastTag == null)
                return true;

            var duplicateTags = xm.LoadedProject.Tags.GroupBy(t => t.LogicalAddress).Where(g => g.Count() > 1).SelectMany(g => g).ToList();

            foreach (var tag in duplicateTags)
            {
                string modelName = tag.Model == "User Defined Tags" ? "User Defined Tags" : tag.Model;
                var matchingTags = xm.LoadedProject.Tags.Where(t => t.Model == modelName).OrderBy(d => d.Key);
                int rowNumber = matchingTags.Select((t, index) => new { t, index }).FirstOrDefault(x => x.t == tag)?.index ?? -1;
                skippedTagsList.Add(Tuple.Create(tag, $"Duplicate tag found on logical address {tag.LogicalAddress} at row no-{rowNumber}"));
            }

            foreach (var tag in tags)
            {
                // Skip if this tag is already flagged as a duplicate
                if (duplicateTags.Contains(tag))
                    continue;
                if (lastTag.LogicalAddress == tag.LogicalAddress)
                    continue;
                var previousAddress = XMProValidator.GetPreviousLogicalAddress(tag.LogicalAddress);
                var previousTag = tags.FirstOrDefault(t => t.LogicalAddress == previousAddress);
                if (!xm.LoadedProject.Tags.Any(T => T.LogicalAddress == previousAddress) && previousTag != null)
                {
                    continue;
                }

                if (previousTag != null && (previousTag.Label == "Double Word" || previousTag.Label == "DINT"))
                {
                    bool isValidTag = XMProValidator.ValidateImportingTag(tag.LogicalAddress);
                    if (!isValidTag)
                    {
                        string modelName = tag.Model == "User Defined Tags" ? "User Defined Tags" : tag.Model;
                        var matchingTags = xm.LoadedProject.Tags.Where(t => t.Model == modelName).OrderBy(D => D.Key);
                        int rowNumber = matchingTags.Select((t, index) => new { t, index })
                                                    .FirstOrDefault(x => x.t == tag)?.index ?? -1;
                        skippedTagsList.Add(Tuple.Create(tag, $"This tag is already occupied by the last {previousTag.Label} tag. at row no-{rowNumber - 1}"));
                    }
                }
            }
            if (skippedTagsList.Count > 0)
            {
                textBoxError.Controls.Clear();
                splitContainer1.Panel2Collapsed = false;
                skippedTagsList.Reverse();
                var existingForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "CompileErrors");
                if (existingForm != null)
                {
                    existingForm.Dispose(); // or existingForm.Close(), depending on intent
                }
                CompileErrors comperror = new CompileErrors();
                string[] errorMessages = skippedTagsList
                .Select(item => $"{(item.Item1.Model.Equals("User Defined Tags") ? "TagsForm" : "TagsForm@" + item.Item1.Model.Replace(" Tags", ""))} : {item.Item1.LogicalAddress}, {item.Item2}")
                .ToArray();

                comperror.ShowsErrorInListView(errorMessages, "Errors");
                comperror.TopLevel = false;
                comperror.Visible = true;
                textBoxError.Controls.Add(comperror);
                groupBoxError.Visible = true;
                comperror.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                comperror.Dock = DockStyle.Fill;
                comperror.Show();
                buttonErrorClose.Visible = true;
                groupBoxError.Text = "Output";
                xm.isCompilied = false;
                return false;
            }
            return true;
        }
        private bool ValidateAndInitializeProject()
        {
            if (xm.LoadedProject == null)
            {
                MessageBox.Show("No project loaded. Please open or create a project first.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (xm.LoadedProject._scanTime == 0)
            {
                MessageBox.Show("Task Cycle Time cannot be empty",
                               "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (xm.LoadedProject._timeRange == 0)
            {
                MessageBox.Show("Task Watchdog Time cannot be empty",
                               "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (xm.LoadedProject._scanTime < 5 || xm.LoadedProject._scanTime > 2000)
            {
                MessageBox.Show("Task Cycle Time must be between 5-2000 ms",
                               "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (xm.LoadedProject._timeRange < 2 || xm.LoadedProject._timeRange > 2000)
            {
                MessageBox.Show("Task Watchdog must be between 2-2000 ms",
                               "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (xm.LoadedProject._isEnable)
            {
                if (xm.LoadedProject._timeRange >= xm.LoadedProject._scanTime)
                {
                    MessageBox.Show("Task Watchdog must be less than Task Cycle Time",
                                   "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            var expansionCount = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.ExpansionIO).Select(d => d.Model).Distinct().Count();
            if (expansionCount > 5)
            {
                MessageBox.Show($"Maximum of 5 expansions allowed. This project has {expansionCount} expansions. Please remove expansions before proceeding.",
                               "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        private void strpBtnCompile_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject != null)
                Compile();
        }
        private void strpBtnUploadProject_Click(object sender, EventArgs e)
        {
            UploadProject();
        }
        private void tsmRenameBlock_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = tvProjects.SelectedNode;
            PerformTreeNodeActions((NodeInfo)treeNode.Tag, treeNode, treeNode.Text);
            if (tvProjects.SelectedNode.Parent.Text == "Logic Blocks")
            {
                tvProjects.LabelEdit = true;
                treeNode.BeginEdit();
            }
        }
        private void tvProjects_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label) && !_LoggedIn)
            {
                // adding checks for the not allow special character in logic block name.
                string allowedPattern = @"^[_a-zA-Z][_a-zA-Z0-9]*$";

                if (!Regex.IsMatch(e.Label.Trim(), allowedPattern))
                {
                    e.CancelEdit = true;
                    tssStatusLabel_msg("Block name must start with a letter or underscore and contain only letters, numbers, and underscores (no spaces or special characters).", 3000, "OrangeRed");
                    return;
                }
                string newName = e.Label.Trim();
                bool isInitialBlockRename = false;
                var flagNameIsPresent = xm.LoadedProject.Blocks.Where(b => b.Name == $"{newName}").Any() ? true : false; // Check if name already in use

                if (newName.Equals(tvProjects.SelectedNode.Text))
                {
                    e.CancelEdit = true;
                    return;
                }

                if (tvProjects.SelectedNode.Text == "LogicBlock01")
                {
                    isInitialBlockRename = xm.LoadedProject.Blocks.Where(b => b.Name == tvProjects.SelectedNode.Text).Select(b => b.Elements).Count() > 0 ? flagNameIsPresent : true;
                }
                if ((flagNameIsPresent || newName == "Logic Blocks" || isInitialBlockRename) && (!newName.Equals(tvProjects.SelectedNode.Text)))
                {
                    e.CancelEdit = true;
                    tssStatusLabel_msg(isInitialBlockRename == true ? "Kindly add data before renaming block" : "Block Name already in use", 3000, "OrangeRed");
                }
                else
                {
                    RenameLogicBlockNode(tvProjects.SelectedNode, newName, e);
                    //string selected_screeen = tvProjects.SelectedNode.Text;
                    //TreeNode treeNode = tvProjects.SelectedNode;
                    //tvProjects.LabelEdit = false;
                    //RenameNode(treeNode, newName);
                    //var oldScreeen = xm.LoadedScreens.Where(d => d.Key.Contains($"{selected_screeen}")).Select(d => d.Key).FirstOrDefault();
                    //xm.LoadedScreens.Add($"LadderForm#{newName}", xm.LoadedScreens.Where(d => d.Key.Contains($"{selected_screeen}")).Select(d => d.Value).FirstOrDefault());
                    //if (xm.CurrentScreen.Contains(oldScreeen))
                    //{
                    //    xm.CurrentScreen = $"LadderForm#{newName}";
                    //    ActivateForm(xm.CurrentScreen);
                    //}
                    //xm.LoadedScreens.Remove(oldScreeen);
                    //xm.ScreensToNavigate.Remove(oldScreeen);
                    //e.CancelEdit = true;
                    //e.Node.Text = newName;
                }
            }
            else
                e.CancelEdit = true;

            if (xm.CurrentScreen.StartsWith("MainForm"))
            {
                ActivateForm(xm.CurrentScreen);
            }
        }

        private void RenameLogicBlockNode(TreeNode selectedNode, string newName, NodeLabelEditEventArgs e)
        {
            string selected_screeen = selectedNode.Text;
            TreeNode treeNode = selectedNode;
            tvProjects.LabelEdit = false;
            RenameNode(treeNode, newName);
            var oldScreeen = xm.LoadedScreens.Where(d => d.Key.Contains($"{selected_screeen}")).Select(d => d.Key).FirstOrDefault();
            xm.LoadedScreens.Add($"LadderForm#{newName}", xm.LoadedScreens.Where(d => d.Key.Contains($"{selected_screeen}")).Select(d => d.Value).FirstOrDefault());
            if (xm.CurrentScreen.Contains(oldScreeen))
            {
                xm.CurrentScreen = $"LadderForm#{newName}";
                ActivateForm(xm.CurrentScreen);
            }
            xm.LoadedScreens.Remove(oldScreeen);
            xm.ScreensToNavigate.Remove(oldScreeen);
            e.CancelEdit = true;
            e.Node.Text = newName;
        }

        private void MenuEditUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }
        private void MenuEditRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }
        private void MenuEditCut_Click(object sender, EventArgs e)
        {
            Cut();
            GetCanvasForUndoRedo();
        }

        private void GetCanvasForUndoRedo()
        {
            if (xm.CurrentScreen.StartsWith("LadderForm"))
            {
                var currentBlockForm = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
                LadderCanvas ladderCanvas = currentBlockForm.getLadderEditor().getCanvas();
                ladderCanvas.getDesignView().SetStateForUndoRedo();
            }
        }
        /// <summary>
        /// Copy selected rungs from the canvas
        /// </summary>
        private void Copy(bool iscut = false)
        {
            if (xm.CurrentScreen.Contains("LadderForm#") && xm.PlcStatus != "LogIn")
            {
                var currentBlockForm = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
                LadderCanvas curCanvas = currentBlockForm.getLadderEditor().getCanvas();

                List<LadderElement> selectedElements = new List<LadderElement>();
                selectedElements = curCanvas.fullyRungElements;
                LadderDesign.ClickedElement = curCanvas.fullyRungElements.Count > 0 ? curCanvas.fullyRungElements.FirstOrDefault() : LadderDesign.ClickedElement;
                if (curCanvas.fullyRungElements.Count == 0)
                {
                    MessageBox.Show("Please Select rung First", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                (List<LadderElement> selectedRung, Dictionary<HashSet<LadderElement>, LadderElements> _parallelDict) = curCanvas.getDesignView().CopyElement(selectedElements);
                bool iscommented = CommetedCheck(selectedElements);
                if (!iscommented)
                {
                    if (selectedRung != null)
                    {
                        List<string> rungList = new List<string>();
                        List<string> commentList = new List<string>();
                        foreach (LadderElement element in selectedRung)
                        {
                            (string curRung, string curComment) = DInterpreter.RungToExpression(element, ref _parallelDict);
                            //Checking Copy Element of Pack and Unpack
                            rungList.Add(curRung);
                            commentList.Add(curComment);
                        }
                        DataCVX.CopyRung(rungList, commentList);
                        tssStatusLabel_msg(iscut ? $"Cutted {rungList.Count()} rung " : $"Copied {rungList.Count()} rung ", 1000, "LimeGreen");
                        if (!iscut)
                        {
                            curCanvas.selectedElements.Clear();
                            curCanvas.fullyRungElements.Clear();
                            LadderDesign.ClickedElement = null;
                        }
                    }
                }
                else
                {
                    curCanvas.selectedElements.Clear();
                    curCanvas.fullyRungElements.Clear();
                    LadderDesign.ClickedElement = null;
                    MessageBox.Show("Please Select UnCommeted Element OR UnComment Rung", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                curCanvas.Refresh();
            }
            else if (xm.LoadedScreens[xm.CurrentScreen].GetType().ToString().Contains("frmGridLayout"))
            {
                if (!ValidScreen())
                    return;
                if (xm.CurrentScreen.ToString().Contains("frmAddResistanceValue") || xm.CurrentScreen.ToString().Contains("ResistanceValue") || xm.CurrentScreen.ToString().Contains("LookUpTbl"))
                {
                    var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                    curgridform.copyResistanceValues();
                }
                else
                {
                    var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                    curgridform.copyTagAndModbusRequests();
                }                
            }
        }
        private bool CommetedCheck(List<LadderElement> selectedElements)
        {
            foreach (LadderElement ld in selectedElements)
            {
                foreach (LadderDrawing.Attribute attribute in ld.Attributes.ToList())
                {
                    if (attribute.Name == "isCommented")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Cut selected rungs from the canvas
        /// </summary>
        private void Cut()
        {
            if (xm.CurrentScreen.Contains("LadderForm") && xm.PlcStatus != "LogIn")
            {
                bool shouldReturn;
                if (HandleUDFBLibraryValidation(LadderDesign.ClickedElement, out shouldReturn))
                {
                    if (shouldReturn) return;
                }
                LadderCanvas curCanvas = ((LadderWindow)xm.LoadedScreens[xm.CurrentScreen]).getLadderEditor().getCanvas();
                List<LadderElement> selectedElements = new List<LadderElement>();
                if (curCanvas.fullyRungElements.Count == 0)
                {
                    MessageBox.Show("Please Select rung First", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                selectedElements = curCanvas.fullyRungElements;
                if (selectedElements.Count == 0)
                {
                    selectedElements.Add(LadderDesign.ClickedElement);
                }
                bool iscommented = CommetedCheck(selectedElements);
                if (iscommented)
                {
                    curCanvas.fullyRungElements.Clear();
                    curCanvas.selectedElements.Clear();
                    LadderDesign.ClickedElement = null;
                    MessageBox.Show("Please Select UnCommeted Element OR UnComment Rung", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Copy(true);
                foreach (LadderElement selectedElement in selectedElements)
                {
                    int countOfRetentiveAdd = 0;
                    string retentiveAdd = "";
                    string Output2 = "";
                    XMIOConfig OutputTag = new XMIOConfig();
                    foreach (LadderElement childelement in selectedElement.getRoot().Elements)
                    {
                        if (childelement.customDrawing.toString() == "FunctionBlock")
                        {
                            if (childelement.Attributes["TCName"].ToString() != "-" && childelement.Attributes["TCName"].ToString() != "")
                            {
                                if (childelement.Attributes["function_name"].ToString().Contains("RTON"))
                                {
                                    Output2 = childelement.Attributes["output2"].ToString();

                                    OutputTag = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output2);
                                    if (OutputTag != null)
                                    {
                                        countOfRetentiveAdd = countOfRetentiveAdd + 1;
                                    }
                                    if (OutputTag.Retentive)
                                    {
                                        retentiveAdd = OutputTag.RetentiveAddress.ToString();
                                    }
                                }
                                else
                                {
                                    var isthere1 = XMPS.Instance.LoadedProject.LogicRungs.ToList();
                                    foreach (var i in isthere1)
                                    {
                                        if (i.DataType_Nm == "RTON")
                                        {
                                            var output2 = childelement.Attributes["output2"].ToString();
                                            XMIOConfig outputTags = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output2);
                                            if (outputTags != null)
                                            {
                                                countOfRetentiveAdd = countOfRetentiveAdd + 1;
                                            }
                                        }
                                    }
                                    if (countOfRetentiveAdd == 1)
                                    {
                                        OutputTag.Retentive = false;
                                        OutputTag.RetentiveAddress = null;
                                    }
                                }
                            }
                            var isthere = xm.LoadedProject.LogicRungs.ToList();
                            foreach (var i in isthere)
                            {
                                if (i.DataType_Nm == "RTON")
                                {
                                    var output2 = childelement.Attributes["output2"].ToString();
                                    XMIOConfig outputTags = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output2);
                                    if (outputTags != null)
                                    {
                                        countOfRetentiveAdd = countOfRetentiveAdd + 1;
                                    }
                                }
                            }
                            if (countOfRetentiveAdd == 1)
                            {
                                OutputTag.Retentive = false;
                                OutputTag.RetentiveAddress = null;
                            }
                        }
                    }
                    if (selectedElement != null)
                        curCanvas.DeleteRung(selectedElement.getRoot());
                }
                curCanvas.fullyRungElements.Clear();
                curCanvas.selectedElements.Clear();
                LadderDesign.ClickedElement = null;
                curCanvas.Refresh();
            }
            else if (xm.LoadedScreens[xm.CurrentScreen].GetType().ToString().Contains("frmGridLayout"))
            {
                if (!ValidScreen())
                    return;
                if (xm.CurrentScreen.ToString().Contains("frmAddResistanceValue") || xm.CurrentScreen.ToString().Contains("ResistanceValue") || xm.CurrentScreen.ToString().Contains("LookUpTbl"))
                {
                    var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                    curgridform.cutResistanceValues();
                }
                else
                {
                    var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                    curgridform.cutTagAndModbusRequests();
                }              
            }
        }

        private bool ValidScreen()
        {
            if (xm.CurrentScreen.Contains("#") && (xm.CurrentScreen.Split('#')[1].Equals("System Tags") || xm.PlcModels.Contains(xm.CurrentScreen.Split('#')[1].Split('_')[0])
                    || xm.CurrentScreen.Split('#')[1].Equals("OnBoardIO") || xm.CurrentScreen.Split('#')[1].Equals("ExpansionIO") || xm.CurrentScreen.Split('#')[1].Equals("RemoteIO")
                     || xm.CurrentScreen.Split('#')[1].Equals("IOConfig")) || xm.CurrentScreen.Equals("EthernetForm") || xm.CurrentScreen.Equals("COMDeviceForm") || xm.CurrentScreen.Equals("Mqtt Form#"))
                return false;

            else
                return true;
        }
        public bool HandleUDFBLibraryValidation(LadderElement clickedElement, out bool shouldReturn)
        {
            shouldReturn = false;
            var currentBlockForm = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
            var frmTemp = currentBlockForm.ParentForm as frmMain;
            string selectedNode = frmTemp.tvProjects.SelectedNode.Text;

            string normalizedSelectedNode = selectedNode.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                ? selectedNode.Substring(0, selectedNode.Length - 6).Trim() : selectedNode;
            string currentScreenName = XMPS.Instance.CurrentScreen ?? string.Empty;
            if (!currentScreenName.Contains("UDFLadderForm"))
            {
                return false;
            }

            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string libraryRoot = Path.Combine(basePath, "MessungSystems", "XMPS2000", "Library");
            string modelFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD") ? "XBLDLibraries" : "XMLibraries";
            string libraryPath = Path.Combine(libraryRoot, modelFolder);

            List<string> udfbNames = XMPS.Instance.LoadedProject.UDFBInfo.Select(u => u.UDFBName).ToList();

            var fileNames = Directory.Exists(libraryPath)
                ? Directory.GetFiles(libraryPath, "*.csv")
                    .Select(Path.GetFileNameWithoutExtension)
                    .Select(name => name.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                        ? name.Substring(0, name.Length - 6).Trim()
                        : name)
                : Enumerable.Empty<string>();

            bool isUdfbMatch = fileNames.Any(fileName =>
                fileName.Equals(normalizedSelectedNode, StringComparison.OrdinalIgnoreCase) &&
                udfbNames.Any(udfbName => udfbName.Equals(normalizedSelectedNode, StringComparison.OrdinalIgnoreCase)));

            if (!isUdfbMatch) return false;
            string savedChoice = XMPS.Instance.LoadedProject.GetUDFBChoice(normalizedSelectedNode);
            if (string.IsNullOrEmpty(savedChoice))
            {
                using (var optionsForm = new UDFBEditOptionsForm(normalizedSelectedNode))
                {
                    if (optionsForm.ShowDialog() == DialogResult.OK)
                    {
                        if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                        {
                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "EditMainFile");
                            return true;
                        }
                        else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                        {
                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "CreateLocalCopy");
                            string localCopyName = optionsForm.LocalCopyName;
                            frmTemp.CreateAndEditLocalCopy(clickedElement, normalizedSelectedNode, localCopyName);
                            shouldReturn = true;
                            return true;
                        }
                    }
                    else
                    {
                        shouldReturn = true;
                        return true;
                    }
                }
            }
            else
            {
                if (savedChoice == "CreateLocalCopy")
                {
                    string localCopyName = normalizedSelectedNode + "_LocalCopy";
                    frmTemp.CreateAndEditLocalCopy(clickedElement, normalizedSelectedNode, localCopyName);
                    shouldReturn = true;
                    return true;
                }
                return true;
            }
            return false;
        }       
        /// <summary>
                 /// Paste copied rungs on the canvas
                 /// </summary>
        private void Paste()
        {
            InitializeTimerCounters();
            bool validrecord = true;
            if (xm.CurrentScreen.Contains("LadderForm#") && xm.PlcStatus != "LogIn" && DataCVX.CopyPresent)
            {
                bool shouldReturn;
                if (HandleUDFBLibraryValidation(LadderDesign.ClickedElement, out shouldReturn))
                {
                    if (shouldReturn) return;
                }
                if (DataCVX.CutTrue)
                {
                    ///Added Check for the Adding only 10 Rungs in Interrupt Logic Block
                    string currentScreenName = xm.CurrentScreen.Split('#')[1];
                    if (currentScreenName.StartsWith("Interrupt_Logic_Block") && (LadderDesign.Active.Elements.Count + DataCVX.CurrentRung.Count > 10))
                    {
                        MessageBox.Show("Maximum Limit of Rung For Interrupt Block Exceed", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (LadderDesign.Active.m_Height_dic.Count > 0 && (65535 - LadderDesign.Active.m_Height_dic.Last().Key) < 500)
                    {
                        MessageBox.Show("Maximum Limit of Rung is reached", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    LadderDesign.PasteElement();
                }
                else
                {
                    ///Added Check for the Adding only 10 Rungs in Interrupt Logic Block
                    string currentScreenName = xm.CurrentScreen.Split('#')[1];
                    if (currentScreenName.StartsWith("Interrupt_Logic_Block") && (LadderDesign.Active.Elements.Count + DataCVX.CurrentRung.Count > 10))
                    {
                        MessageBox.Show("Maximum Limit of Rung For Interrupt Block Exceed", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (LadderDesign.Active.m_Height_dic.Count > 0 && (65535 - LadderDesign.Active.m_Height_dic.Last().Key) < 500)
                    {
                        MessageBox.Show("Maximum Limit of Rung is reached", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var currentBlockForm = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
                    LadderCanvas curCanvas = currentBlockForm.getLadderEditor().getCanvas();
                    LadderDesign curDesign = curCanvas.getDesignView();
                    string op2 = "";
                    string fnname = "";
                    bool isPasteBetwn = LadderDesign.ClickedElement != null;
                    int index = curDesign.Elements.Count();
                    if (isPasteBetwn)
                    {
                        index = LadderDesign.ClickedElement.getRoot().Position.Index + 1;
                    }
                    (List<string> Rungs, List<string> Comments) = DataCVX.PasteRung();
                    for (int i = 0; i < Rungs.Count; i++)
                    {
                        string instructionName = XMProValidator.GettingInstructionNameFromRung(Rungs[i]);
                        if (!Rungs[i].Contains("OPC:9999") && !string.IsNullOrEmpty(instructionName) && xm.instructionsList.FirstOrDefault(t => t.Text.Equals(instructionName)) == null)
                        {
                            MessageBox.Show($"{instructionName} instruction type not found in current instruction list", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            validrecord = false;
                            return;
                        }
                        string rung = Rungs[i];
                        if (rung.Contains("TC:"))
                        {
                            ///Get old TC code entered in the copied string
                            string oldtc = rung.Substring(rung.IndexOf("TC:"));
                            oldtc = oldtc.Substring(0, oldtc.IndexOf(" "));
                            string opcodenm = rung.Substring(rung.IndexOf("FN"), rung.IndexOf("TC:") - rung.IndexOf("FN")).Replace("FN:", "").Replace("@", " ").TrimEnd();
                            fnname = rung.Split(' ').Where(x => x.Contains("DTN:")).First().ToString();
                            fnname = fnname.Replace("DTN:", "");
                            ///Get the opcode of the copied FB 
                            string opcode = rung.Substring(rung.IndexOf("OPC") + 4, 4);
                            ///Generate new tc code for this opcode
                            var code = xm.LoadedProject.LogicRungs.Where(R => R.OpCode == opcode);
                            ///Check if TC name is not - 
                            if (!oldtc.Contains("-") && oldtc.Length > 3)
                            {
                                var disOpcodes = new List<string>() { "0390", "03A2", "03B2", "03C2", "9999" };
                                //make tag retentive and create retentive address
                                if (opcodenm.Contains("RTON"))
                                {
                                    List<string> inputAddressList = new List<string>();
                                    foreach (string inputs in rung.Split(' ').Where(x => x.Contains("OP:") || x.Contains("OP:")))
                                    {
                                        if (inputs.Contains("OP:"))
                                        {
                                            if (inputAddressList.Count() == 1)
                                            {
                                                string secondOutputAdd = inputs.Replace("OP:", "");
                                                inputAddressList.Add(secondOutputAdd.Split(']')[0]);
                                            }
                                            else
                                            {
                                                inputAddressList.Add(inputs.Replace("OP:", ""));
                                            }
                                        }
                                    }
                                    foreach (var item in inputAddressList)
                                    {
                                        if (item.StartsWith("W4") || item.StartsWith("T6") || item.StartsWith("C7"))
                                        {
                                            op2 = item;
                                            var tag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == item).FirstOrDefault();
                                            if (!tag.Retentive)
                                            {
                                                tag.Retentive = true;
                                                tag.RetentiveAddress = CommonFunctions.GetRetentiveAddress(item, "");
                                            }
                                        }
                                    }
                                }
                                //for when the last PACK instruction delete or cut form the Ladder Window
                                string newtccode = "";
                                var maxcode = "";
                                int maxcounter = 0;
                                if (code.Count() != 0)
                                {
                                    if (disOpcodes.Contains(opcode))
                                        maxcode = code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", ""))); // code.Max(C => C.TC_Name);

                                    if (maxcode.StartsWith("FB") || opcodenm.Equals("Pack") || opcodenm.Equals("UnPack") || opcodenm.Equals("MQTT Publish") || opcodenm.Equals("MQTT Subscribe"))
                                    {
                                        newtccode = "TC:" + maxcode.Replace(Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value).ToString(), (Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1).ToString());
                                        opcodenm = code.Select(R => R.OpCodeNm).FirstOrDefault();
                                        maxcounter = opcodenm.Contains("Pack") || opcodenm.Contains("MQTT Publish") || opcodenm.Contains("MQTT Subscribe") ? 0 : maxcode.StartsWith("FB") ? Counters["FB"].Maximum : Counters[opcodenm].Maximum;
                                        maxcode = code.Count() == 0 ? oldtc.Replace(Convert.ToInt32(Regex.Match(oldtc.ToString(), @"\d+").Value).ToString(), Counters[opcodenm].CurrentPosition.ToString()) : code.Max(C => Regex.Replace(C.TC_Name, @"\d", "")) + code.Max(C => Convert.ToInt32(Regex.Replace(C.TC_Name, @"\D", ""))); // code.Max(C => C.TC_Name);
                                        newtccode = code.Count() == 0 ? oldtc.Replace(Convert.ToInt32(Regex.Match(oldtc.ToString(), @"\d+").Value).ToString(), Counters[opcodenm].CurrentPosition.ToString()) : "TC:" + maxcode.Replace(Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value).ToString(), (Convert.ToInt32(Regex.Match(maxcode.ToString(), @"\d+").Value) + 1).ToString());
                                    }
                                    else
                                    {
                                        maxcounter = Counters[opcodenm].Maximum;
                                        newtccode = "TC:" + Counters[opcodenm].Instruction + "#";
                                    }
                                }
                                else
                                {
                                    if (disOpcodes.Contains(opcode))
                                        newtccode = oldtc;
                                    else
                                        newtccode = "TC:" + Counters[opcodenm].Instruction + "#";
                                    maxcounter = 0;
                                    if (!opcodenm.Equals("Pack") && !opcodenm.Equals("MQTT Publish") && !opcodenm.Equals("MQTT Subscribe") && !opcodenm.Equals("UnPack") && !opcode.Equals("9999"))
                                    {
                                        maxcode = Counters[opcodenm].Instruction + Counters[opcodenm].CurrentPosition;
                                        maxcounter = opcodenm.Contains("Pack") || opcodenm.Contains("MQTT Publish") || opcodenm.Contains("MQTT Subscribe") ? 0 : maxcode.StartsWith("FB") ? Counters["FB"].Maximum : Counters[opcodenm].Maximum;
                                    }
                                    else if (opcode.Equals("9999"))
                                    {
                                        maxcode = Counters["FB"].Instruction + "0";
                                        maxcounter = Counters["FB"].Maximum;
                                    }
                                }
                                if (code.Count() == 0 && oldtc.Contains("PK"))
                                {
                                    newtccode = oldtc;
                                }
                                if (!newtccode.Contains("PK") && !newtccode.Contains("SUB") && !newtccode.Contains("PUB"))
                                {
                                    ///Update the TC code from copied FB to the new one
                                    Rungs[i] = rung.Replace(oldtc, newtccode);
                                    ApplicationRung AppRecs = new ApplicationRung();
                                    AppRecs.TC_Name = newtccode.Replace("TC:", "");
                                    AppRecs.OpCode = opcode;
                                    AppRecs.OpCodeNm = opcodenm;
                                    AppRecs.WindowName = currentBlockForm.ToString();
                                    AppRecs.Name = currentBlockForm.ToString();
                                    AppRecs.DataType_Nm = fnname;
                                    AppRecs.Outputs["Output2"] = op2;
                                    XMPS.Instance.LoadedProject.LogicRungs.Add(AppRecs);
                                }
                                else if (newtccode.Contains("PUB") || newtccode.Contains("SUB"))
                                {
                                    Rungs[i] = rung.Replace(oldtc, newtccode);
                                    ApplicationRung AppRecs = new ApplicationRung();
                                    AppRecs.TC_Name = newtccode.Replace("TC:", "");
                                    AppRecs.OpCode = opcode;
                                    AppRecs.OpCodeNm = opcodenm;
                                    AppRecs.WindowName = currentBlockForm.ToString();
                                    AppRecs.Name = currentBlockForm.ToString();
                                    AppRecs.DataType_Nm = fnname;
                                    AppRecs.Outputs["Output2"] = op2;
                                    XMPS.Instance.LoadedProject.LogicRungs.Add(AppRecs);
                                }
                                //Adding Condition for the Incresing TC Name If Copied Rung Contain PackFB
                                else if (newtccode.Contains("PK"))
                                {
                                    //Feching First/Starting Address for the OLD  Pack Instrction 
                                    string instuctioName = newtccode.Substring(newtccode.IndexOf(":") + 1, 2);
                                    string oldpackFirstIn = instuctioName == "PK" ? rung.Substring(rung.IndexOf("IN:"), 9) : rung.Substring(rung.IndexOf("OP:"), 9);
                                    string oldPackFirstLarge = instuctioName == "PK" ? rung.Substring(rung.IndexOf("IN:"), 10) : rung.Substring(rung.IndexOf("OP:"), 10);
                                    XMIOConfig firstInputTag;
                                    if (!oldPackFirstLarge.Contains(" ") && !oldPackFirstLarge.Contains(']'))
                                    {
                                        firstInputTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == (instuctioName == "PK" ? oldPackFirstLarge.Replace("IN:", "").Replace("~", "") : oldPackFirstLarge.Replace("OP:", ""))).FirstOrDefault();
                                    }
                                    else
                                    {
                                        firstInputTag = xm.LoadedProject.Tags.Where(T => T.LogicalAddress == (instuctioName == "PK" ? oldpackFirstIn.Replace("IN:", "").Replace("~", "") : oldpackFirstIn.Replace("OP:", ""))).FirstOrDefault();
                                    }
                                    if (firstInputTag == null)
                                    {
                                        //Fetch Last Add for the old PK Name
                                        string lastPackAdd = xm.LoadedProject.Tags.Where(T => T.ActualName != null && T.ActualName.StartsWith(code.Count() == 0 ? oldtc.Replace("TC:", "") : maxcode)).Select(T => T.ActualName)
                                          .OrderByDescending(D => int.Parse(D.Split('_')[1])).FirstOrDefault();
                                        //Get Tag from Actual Name 
                                        XMIOConfig lastTagPrevFB = xm.LoadedProject.Tags.Where(T => T.ActualName == lastPackAdd).FirstOrDefault();
                                        string lastTagPrevFBLogicAdd = lastPackAdd != null ? lastTagPrevFB.LogicalAddress : instuctioName == "PK" ? oldpackFirstIn.Replace("IN:", "") : rung.Substring(rung.IndexOf("OP:") + 3, 6);
                                        string[] partsOfAdd = lastTagPrevFBLogicAdd.Split(':');
                                        int secondPart = int.Parse(partsOfAdd[1]);
                                        int nextSecondPart = secondPart != 0 ? secondPart + 1 : secondPart + 0;
                                        string nextAddress = "";
                                        if (!oldPackFirstLarge.Contains(" "))
                                        {
                                            nextAddress = instuctioName == "PK" ? oldPackFirstLarge.Replace("IN:", "") : oldPackFirstLarge.Replace("OP:", "").Replace("]", "");
                                        }
                                        else
                                        {
                                            nextAddress = instuctioName == "PK" ? oldpackFirstIn.Replace("IN:", "") : oldpackFirstIn.Replace("OP:", "").Replace("]", "");
                                        }
                                        //Checking Maximum Bool Tag Limit Excide or Not ==1023
                                        if (secondPart + 15 > 1023)
                                        {
                                            MessageBox.Show("Reached Maximum Tag Limit", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        if (lastPackAdd == null)
                                        {
                                            if (instuctioName.Equals("PK"))
                                            {
                                                TagsUserControl tagsUser = new TagsUserControl(nextAddress, newtccode.Replace("TC:", ""), "Pack");
                                            }
                                            else
                                            {
                                                TagsUserControl tagsUser = new TagsUserControl(nextAddress, newtccode.Replace("TC:", ""), "UnPack");
                                            }
                                        }
                                        else
                                        {
                                            if (instuctioName.Equals("PK"))
                                            {
                                                TagsUserControl tagsUser = new TagsUserControl(nextAddress, "", "Pack");
                                            }
                                            else
                                            {
                                                TagsUserControl tagsUser = new TagsUserControl(nextAddress, "", "UnPack");
                                            }
                                        }
                                    }
                                    ApplicationRung AppRecs = new ApplicationRung();
                                    XMIOConfig newPackFbInput = xm.LoadedProject.Tags.Where(T => T.ActualName == newtccode.Replace("TC:", "") + "_1").FirstOrDefault();

                                    if (newPackFbInput != null || firstInputTag != null)
                                    {
                                        string FunctionName = rung.Substring(rung.IndexOf("FN:"), rung.IndexOf("TC") - rung.IndexOf("FN:"));
                                        string fnName = FunctionName.Replace("FN:", "");
                                        string oldfirstInAdd = "";
                                        if (!oldPackFirstLarge.Contains(" "))
                                        {
                                            oldfirstInAdd = fnName == "Pack " ? rung.Substring(rung.IndexOf("IN:"), 10) : rung.Substring(rung.IndexOf("OP:"), 10);
                                        }
                                        else
                                        {
                                            oldfirstInAdd = fnName == "Pack " ? rung.Substring(rung.IndexOf("IN:"), 9) : rung.Substring(rung.IndexOf("OP:"), 9);
                                        }
                                        string newPackFirstIn = fnName == "Pack " ? oldfirstInAdd.Replace("IN:", "") : oldfirstInAdd.Replace("OP:", "");
                                        rung = rung.Replace(oldtc, newtccode);
                                        //Replacing First/Starting Address for the New Pack Instrction
                                        if (instuctioName == "PK")
                                        {
                                            if (!oldPackFirstLarge.Contains(" "))
                                            {
                                                Rungs[i] = rung.Replace(oldPackFirstLarge, "IN:" + newPackFirstIn);
                                            }
                                            else
                                            {
                                                Rungs[i] = rung.Replace(oldpackFirstIn, "IN:" + newPackFirstIn);
                                            }
                                        }
                                        else
                                        {
                                            if (!oldPackFirstLarge.Contains(" "))
                                            {
                                                Rungs[i] = rung.Replace(oldPackFirstLarge, "OP:" + newPackFirstIn);
                                            }
                                            else
                                            {
                                                Rungs[i] = rung.Replace(oldpackFirstIn, "OP:" + newPackFirstIn);
                                            }
                                        }
                                        AppRecs.TC_Name = newtccode.Replace("TC:", "");
                                        AppRecs.OpCode = opcode;
                                        AppRecs.OpCodeNm = opcodenm;
                                        AppRecs.WindowName = currentBlockForm.ToString();
                                        AppRecs.Name = currentBlockForm.ToString();
                                        AppRecs.DataType_Nm = fnname;
                                        AppRecs.Outputs["Output2"] = op2;
                                        XMPS.Instance.LoadedProject.LogicRungs.Add(AppRecs);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    validrecord = false;
                                    MessageBox.Show("Maximum Number of Rungs of Exceeds for this instructions for this type: " + opcodenm, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    tssStatusLabel_msg($"Maximum Number of Rungs of Exceeds for this instructions for this type: " + opcodenm, 1000, "Red");
                                }
                            }

                            //adding for the PID, in PID instruction TC_Name not used. require to handle on Function Name.
                            else if (opcode.Equals("040E"))
                            {
                                string nextPidInstNum = string.Empty;
                                string maxUsedPidInstNum = string.Empty;
                                Rungs[i] = rung.Replace(opcodenm, "MES_PID_#");
                                ApplicationRung AppRecs = new ApplicationRung();
                                AppRecs.TC_Name = "-";
                                AppRecs.OpCode = opcode;
                                AppRecs.OpCodeNm = nextPidInstNum;
                                AppRecs.WindowName = currentBlockForm.ToString();
                                AppRecs.Name = currentBlockForm.ToString();
                                XMPS.Instance.LoadedProject.LogicRungs.Add(AppRecs);
                            }
                        }
                    }
                    if (validrecord)
                    {
                        LadderDesign PasteDesign = DesignDraw.GetDesign(ref curDesign, Rungs, Comments, index, isPasteBetwn ? "between" : "end");
                        curCanvas.setDesignView(PasteDesign);
                        tssStatusLabel_msg($"Rung pasted", 1000, "LimeGreen");
                    }
                    currentBlockForm.getLadderEditor().isPasted = true;
                    currentBlockForm.getLadderEditor().ReScale(true);
                    //curCanvas.getDesignView().SetStateForUndoRedo(); /// Add Copy paste Action in UndoStack for UndoRedo.
                    XMProValidator.ClearFullySelectedRungs();
                }
            }
            else if (xm.LoadedScreens[xm.CurrentScreen].GetType().ToString().Contains("frmGridLayout"))
            {
                if (xm.CurrentScreen.ToString().Contains("frmAddResistanceValue") || xm.CurrentScreen.ToString().Contains("ResistanceValue"))
                {
                    var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                    curgridform.pasteResistanceValues();
                }
                else
                {
                    var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                    curgridform.pasteTagAndModbusRequests();
                }                 
            }
        }
        private void Undo()
        {
            if (xm.PlcStatus == "LogIn") return;
            if (xm.LoadedScreens[xm.CurrentScreen].GetType().ToString().Contains("LadderLogic"))
            {
                bool shouldReturn;
                if (HandleUDFBLibraryValidation(LadderDesign.ClickedElement, out shouldReturn))
                {
                    if (shouldReturn) return;
                }
                LadderCanvas curCanvas = ((LadderWindow)xm.LoadedScreens[xm.CurrentScreen]).getLadderEditor().getCanvas();
                LadderDesign curDesign = curCanvas.getDesignView();
                curDesign.Undo();
                LadderDesign.ClickedElement = null;
                LadderDrawing.Global.ClearActive();
                ((LadderWindow)xm.LoadedScreens[xm.CurrentScreen]).getLadderEditor().ReScale();
                curCanvas.Update();
                curCanvas.Refresh();
            }
            else if (xm.LoadedScreens[xm.CurrentScreen].GetType().ToString().Contains("frmGridLayout"))
            {
                UndoGrid();
                var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                curgridform.OnShown();
            }
        }
        private void Redo()
        {
            if (xm.LoadedScreens[xm.CurrentScreen].GetType().ToString().Contains("LadderLogic"))
            {
                var currentBlockForm = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
                LadderCanvas curCanvas = currentBlockForm.getLadderEditor().getCanvas();
                LadderDesign curDesign = curCanvas.getDesignView();
                LadderEditorControl editor = currentBlockForm.getLadderEditor();
                curDesign.RedoNew();
                LadderDesign.ClickedElement = null;
                LadderDrawing.Global.ClearActive();
                currentBlockForm.getLadderEditor().ReScale();
                curCanvas.Update();
                curCanvas.Refresh();

            }
            else if (xm.LoadedScreens[xm.CurrentScreen].GetType().ToString().Contains("frmGridLayout"))
            {
                RedoGrid();
                var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                curgridform.OnShown();
            }
        }
        private void UndoGrid()
        {
            if (XMPS.Instance.CurrentScreen.Contains("MODBUSRTUMaster"))                 //RTU
            {
                List<MODBUSRTUMaster_Slave> updatedList = modbusRTUMasterManager.Undo();
                if (updatedList != null)
                {
                    var mainnode = (MODBUSRTUMaster)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                    mainnode.Slaves.Clear();
                    mainnode.Slaves.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("MODBUSRTUSlaves"))                 //RTU
            {
                List<MODBUSRTUSlaves_Slave> updatedList = modbusRTUSlaveManager.Undo();
                if (updatedList != null)
                {
                    var mainnode = (MODBUSRTUSlaves)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
                    mainnode.Slaves.Clear();
                    mainnode.Slaves.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }

            else if (XMPS.Instance.CurrentScreen.Contains("MODBUSTCPClient"))            //Client
            {
                List<MODBUSTCPClient_Slave> updatedList = modbusTCPClientManager.Undo();
                if (updatedList != null)
                {
                    var mainnode = (MODBUSTCPClient)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                    mainnode.Slaves.Clear();
                    mainnode.Slaves.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("MODBUSTCPServer"))           //Server
            {
                List<MODBUSTCPServer_Request> updatedList = modbusTCPServerManager.Undo();
                if (updatedList != null)
                {
                    var mainnode = (MODBUSTCPServer)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                    mainnode.Requests.Clear();
                    mainnode.Requests.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("User Defined Tags") || XMPS.Instance.CurrentScreen.Contains("UDFTags"))        //Tagwindow
            {
                if (DeleteElement.Count() > 0)
                {
                    frmMain frm = new frmMain();
                    frm.RedoGrid();
                    //Deleting The Delete Element
                    DeleteElement.Pop();
                }
                else
                {
                    if (UndoTags.Count() == 0)
                    {
                        MessageBox.Show("Stack is Empty Can't Perform Undo Operation in TagWindow");
                    }
                    else
                    {
                        var TagToRemove = UndoTags.Pop();

                        XMPS.Instance.LoadedProject.Tags.Remove((XMIOConfig)TagToRemove);
                        RedoTags.Push((XMIOConfig)TagToRemove);
                    }
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("MQTT PublishForm#MQTT Publish"))
            {
                List<Publish> updatedList = publishManager.Undo();
                if (updatedList != null)
                {
                    xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Publish");
                    xm.LoadedProject.Devices.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("MQTT SubscribeForm#MQTT Subscribe"))
            {
                List<Subscribe> updatedList = subscribeManager.Undo();
                if (updatedList != null)
                {
                    xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Subscribe");
                    xm.LoadedProject.Devices.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }

            }
            else if (XMPS.Instance.CurrentScreen.Contains("ResistanceValue"))
            {
                if (DeleteResistance.Count > 0)
                {
                    var resistanceToRestore = DeleteResistance.Pop();
                    XMPS.Instance.LoadedProject.ResistanceValues.Add(resistanceToRestore);
                    UndoResistance.Push(resistanceToRestore);
                }
                else if (UndoResistance.Count > 0)
                {
                    var resistanceToUndo = UndoResistance.Pop();
                    XMPS.Instance.LoadedProject.ResistanceValues.Remove(resistanceToUndo);
                    RedoResistance.Push(resistanceToUndo);
                }
                else
                {
                    return;
                }
            }
        }
        private void RedoGrid()
        {
            //Declare single flag variable DeleteUndoRedo
            if (XMPS.Instance.CurrentScreen.Contains("MODBUSRTUMaster"))                 //RTU
            {
                List<MODBUSRTUMaster_Slave> updatedList = modbusRTUMasterManager.Redo();
                if (updatedList != null)
                {
                    var mainnode = (MODBUSRTUMaster)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUMaster").FirstOrDefault();
                    mainnode.Slaves.Clear();
                    mainnode.Slaves.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("MODBUSRTUSlaves"))                 //RTU
            {
                List<MODBUSRTUSlaves_Slave> updatedList = modbusRTUSlaveManager.Redo();
                if (updatedList != null)
                {
                    var mainnode = (MODBUSRTUSlaves)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSRTUSlaves").FirstOrDefault();
                    mainnode.Slaves.Clear();
                    mainnode.Slaves.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("MODBUSTCPClient"))            //Client
            {
                List<MODBUSTCPClient_Slave> updatedList = modbusTCPClientManager.Redo();
                if (updatedList != null)
                {
                    var mainnode = (MODBUSTCPClient)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPClient").FirstOrDefault();
                    mainnode.Slaves.Clear();
                    mainnode.Slaves.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("MODBUSTCPServer"))           //Server
            {
                List<MODBUSTCPServer_Request> updatedList = modbusTCPServerManager.Redo();
                if (updatedList != null)
                {
                    var mainnode = (MODBUSTCPServer)XMPS.Instance.LoadedProject.Devices.Where(d => d.GetType().Name == "MODBUSTCPServer").FirstOrDefault();
                    mainnode.Requests.Clear();
                    mainnode.Requests.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("User Defined Tags") || XMPS.Instance.CurrentScreen.Contains("UDFTags"))        //Tagwindow
            {
                if (RedoTags.Count() == 0)
                {
                    MessageBox.Show("Stack is Empty Can't Perform Redo Operation in TagWindow");
                }
                else
                {
                    var TagToRemove = RedoTags.Pop();
                    XMIOConfig tagConfig = (XMIOConfig)TagToRemove;
                    var existingTag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress == tagConfig.LogicalAddress);
                    if (existingTag != null)
                    {
                        MessageBox.Show($"Tag with LogicalAddress {tagConfig.LogicalAddress} already exists. Please add new tag.", "Duplicate Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UndoTags.Push((XMIOConfig)TagToRemove); // Push back to maintain stack integrity
                        return;
                    }
                    XMPS.Instance.LoadedProject.Tags.Add((XMIOConfig)TagToRemove);
                    UndoTags.Push((XMIOConfig)TagToRemove);
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("MQTT PublishForm#MQTT Publish"))
            {
                List<Publish> updatedList = publishManager.Redo();
                if (updatedList != null)
                {
                    xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Publish");
                    xm.LoadedProject.Devices.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("MQTT SubscribeForm#MQTT Subscribe"))
            {
                List<Subscribe> updatedList = subscribeManager.Redo();
                if (updatedList != null)
                {
                    xm.LoadedProject.Devices.RemoveAll(p => p.GetType().Name == "Subscribe");
                    xm.LoadedProject.Devices.AddRange(updatedList);
                    xm.LoadedProject.NewAddedTagIndex = updatedList.Count - 1;
                }
            }
            else if (XMPS.Instance.CurrentScreen.Contains("ResistanceValue"))
            {
                if (RedoResistance.Count == 0)
                {
                    return;
                }
                else
                {
                    var resistanceToRedo = RedoResistance.Pop();
                    XMPS.Instance.LoadedProject.ResistanceValues.Add(resistanceToRedo);
                    UndoResistance.Push(resistanceToRedo);
                }
            }
        }
        private void MenuEditCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void MenuEditPaste_Click(object sender, EventArgs e)
        {
            Paste();
            LadderCanvas.RefreshCanvas();
            GetCanvasForUndoRedo();
        }
        private void MenuEditDelete_Click(object sender, EventArgs e)
        {
            if (xm.CurrentScreen.Contains("LadderForm#"))
            {
                if (!(xm.PlcStatus == "LogIn"))
                {
                    var currentBlockForm = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
                    LadderCanvas curCanvas = currentBlockForm.getLadderEditor().getCanvas();
                    if (curCanvas.fullyRungElements.Count > 0)
                    {
                        int rungNo = 0;
                        foreach (LadderElement ladderElement in curCanvas.fullyRungElements)
                        {
                            rungNo++;
                            bool islast = curCanvas.fullyRungElements.Count == rungNo ? true : false;
                            LadderDesign.ClickedElement = ladderElement;
                            curCanvas.DeleteRungbyContextMenu(islast);
                            LadderDesign.ClickedElement = null;
                        }
                        curCanvas.Invalidate();
                        curCanvas.Update();
                        curCanvas.Refresh();
                        curCanvas.fullyRungElements.Clear();
                        curCanvas.selectedElements.Clear();
                    }
                    else
                        curCanvas.DeleteWithCounter(new KeyEventArgs(Keys.Delete));
                }
            }
            else if (xm.LoadedScreens[xm.CurrentScreen].GetType().ToString().Contains("frmGridLayout"))
            {
                var curgridform = (frmGridLayout)xm.LoadedScreens[xm.CurrentScreen];
                curgridform.deleteTagAndModbus();
            }
        }
        private void strpBtnCloseProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ChangeLoadedProject(true))
                    return;
                this.tblLeftPanel.Visible = true;
                this.splcMain.SplitterDistance = 225;
                this.lblProjects.Dock = DockStyle.Top;
                this.tblLeftPanel.Width = 225;
                this.lblProjects.AutoSize = false;
                this.lblProjects.Size = new Size(226, 30);
                this.lblProjects.Location = new Point(1, 3);
                this.lblProjects.Text = "Loaded" + " " + "Projects";
                this.btnPin.Location = new Point(185, 1);
                if (ChangeLoadedProject())
                {
                    xm = XMPS.Instance;
                    ShowOrActivateForm("InitialForm");
                    strpBtnCloseProject.Enabled = false;
                    RenderBaseTreeNodes();
                    tsmAddRemoteIO.ComboBox.Items.Clear();
                    tsmAddExpansionIO.ComboBox.Items.Clear();
                    tsmAddRemoteIO.ComboBox.Items.AddRange(RemoteModule.List.FindAll(x => x.IOType.Equals("Remote I/O")).ToArray());
                    tsmAddExpansionIO.ComboBox.Items.AddRange(RemoteModule.List.FindAll(x => x.IOType.Equals("Expansion I/O")).ToArray());
                    strpBtnPrvScreen.Enabled = false;
                    strpBtnSaveProject.Enabled = false;
                    strpBtnLogin.Enabled = false;
                    strpBtnDownloadProject.Enabled = false;
                    MenuModeLogin.Enabled = false;
                    traceWindowToolStripMenuItem.Enabled = false;
                    MenuModeLogout.Enabled = false;
                    MenuModePLCStart.Enabled = false;
                    MenuModePLCStop.Enabled = false;
                    MenuModeDnldProject.Enabled = false;
                    MenuModeDnldProject.Enabled = false;
                    MenuModeDnldSourceCode.Enabled = false;
                    rTCSettingToolStripMenuItem.Enabled = false;
                    strpBtnCompile.Enabled = false;
                    strpBtnPaste.Enabled = false;
                    strpBtnCut.Enabled = false;
                    strpBtnCopy.Enabled = false;
                    strpBtnFind.Enabled = false;
                    strpBtnUndo.Enabled = false;
                    strpBtnRedo.Enabled = false;
                    forceUnforceMenu.Enabled = false;
                    strpBtnDelete.Enabled = false;
                    tssStatusLabel_msg("Project closed", 3000, "DodgerBlue");
                    this.Text = "XMPS 2000";
                }
                ForceUserControl.setValueDic.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Please resolve below issues\n {string.Join(Environment.NewLine, ex.Message)}", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        private void strpBtnFind_Click(object sender, EventArgs e)
        {
            if (xm.CurrentScreen.Contains("LadderForm"))
            {
                bool shouldReturn;
                bool isUdfb = HandleUDFBLibraryValidation(null, out shouldReturn);
                if (shouldReturn)
                {
                    return;
                }
                var existing = Application.OpenForms["FindAndReplace"];
                if (existing == null || existing.IsDisposed)
                    new FindAndReplace().Show();
                else
                    existing.BringToFront();
            }
            else
            {
                MessageBox.Show("Not valid", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void buttonErrorClose_Click(object sender, EventArgs e)
        {
            textBoxError.Text = "";
            splitContainer1.Panel2Collapsed = true;
        }
        private void MenuEditFindNReplace_Click(object sender, EventArgs e)
        {
            bool shouldReturn;
            bool isUdfb = HandleUDFBLibraryValidation(null, out shouldReturn);
            if (shouldReturn)
            {
                return;
            }
            if (xm.CurrentScreen.Contains("LadderForm"))
            {
                FindAndReplace findAndReplace = new FindAndReplace();
                findAndReplace.Show();
            }
            else
            {
                MessageBox.Show("Not valid", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void tsmAddTag_Click(object sender, EventArgs e)
        {
            AddNewUserDefinedTag();
        }
        private void MenuModeCompile_Click(object sender, EventArgs e)
        {
            if (xm.PlcStatus == "LogIn")
                return;
            Compile();
        }
        private void MenuViewCompErr_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !(splitContainer1.Panel2Collapsed);
            if (!splitContainer1.Panel2Collapsed && compilationErrors.Count > 0)
            {
                textBoxError.Text = string.Join(Environment.NewLine, "Compilation Errors:", string.Join(Environment.NewLine, compilationErrors));
            }
        }
        private void tvBlocks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeView T1 = (TreeView)sender;
            TreeNode TN = T1.SelectedNode;
            if (TN != null)
            {
                if (TN.Parent != null)
                {

                    //string parent = TN.Parent.ToString();
                    //string nodeinfo = TN.Name.ToString();
                    //XMProForm tempForm = new XMProForm();
                    //tempForm.StartPosition = FormStartPosition.CenterParent;
                    //tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    //ApplicationUserControl userControl = new ApplicationUserControl(tempForm);
                    //userControl.SelectedInstructionType = parent.ToString().Replace("TreeNode: ", "");
                    //userControl.SelectedInstruction = nodeinfo;
                    //tempForm.Text = "Add New Rung For " + parent.ToString().Replace("TreeNode: ", "") + " With Instruction " + nodeinfo;
                    //userControl.edit = false;
                    //tempForm.Height = userControl.Height;
                    //tempForm.Width = userControl.Width;
                    //tempForm.Controls.Add(userControl);
                    //var frmTemp = this.ParentForm as frmMain;
                    //DialogResult result = tempForm.ShowDialog(frmTemp);
                }
            }
        }
        private void RefreshGridFormIfEditing(string formNameContains)
        {
            try
            {
                if (XMPS.Instance.CurrentScreen.ToString().Contains(formNameContains))
                {
                    if (XMPS.Instance.LoadedScreens.ContainsKey(XMPS.Instance.CurrentScreen))
                    {
                        var curgridform = (frmGridLayout)XMPS.Instance.LoadedScreens[XMPS.Instance.CurrentScreen];
                        var grdMainField = typeof(frmGridLayout).GetField("grdMain",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (grdMainField?.GetValue(curgridform) is DataGridView grdMain)
                        {
                            if (grdMain.IsCurrentCellInEditMode)
                            {
                                grdMain.EndEdit();
                                grdMain.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            }
                        }
                        curgridform.OnShown();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Warning: Could not refresh {formNameContains}\n\nDetails: {ex.Message}","Refresh Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private async Task Download()
        {
            string Tftpaddress = "";
            RefreshGridFormIfEditing("MODBUSRTUSlavesForm");
            if (!ValidateAndInitializeProject())
            {
                return;
            }
            if (!ValidateBacnetObjects())
            {
                this.Cursor = Cursors.Default;
                return;
            }
            if (xm.LoadedProject.MainLadderLogic.Count() <= 0)
            {
                MessageBox.Show("Add logic in Main ladder logic to download the project", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Do You Want to Download the Application?", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                progressBar1.Visible = true;
                progressBar1.Value = 5;
                progressBar1.Update();
                UpdateProgressBarWithText("Connecting...", 2000);
                //Thread.Sleep(400);
                Task.Delay(400).Wait();
                this.Cursor = Cursors.WaitCursor;
                // Compile project and save it
                xm.isCompilied = true;
                try
                {
                    SaveProject();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error while save and compile");
                    xm.isCompilied = false;
                    progressBar1.Visible = false;
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (xm.LoadedProject.MainLadderLogic.Count() > 0 && xm.isCompilied)
                {
                    (string, string) result = ("", "");
                    PLCCommunications pLCCommunications = new PLCCommunications();

                    progressBar1.Value = 30;
                    progressBar1.Update();
                    UpdateProgressBarWithText("Inprogress.", 2000);
                    Task.Delay(400).Wait();
                    if (pLCCommunications.GetIPAddress() == "Error")
                    {
                        string errmsg = XMPS2000.CommonFunctions.GetEasyConnection(xm._connectedIPAddress);
                        result.Item1 = errmsg;
                        goto Exitprocess;
                    }
                    else
                        Tftpaddress = pLCCommunications.Tftpaddress.ToString();
                    pLCCommunications.ReplaceIPAddress();
                    Compile();
                    Byte[] response = pLCCommunications.PLCSyncLogin();
                    byte SOF = response[0];                            //sof
                    byte ProgramCRCAck = response[1];                 // Program CRC ack.
                    byte PlcModeAck = response[2];                  //Plc Mode Ack. (RUN/STOP)
                    byte ExpansionModuleAck = response[3];          //ExpansionModuleAck
                    byte ExpansionModuleIdAck = response[4];       //ExpansionModuleAckId Not set Ack
                    byte ExpansionAI_AO = response[5];            //ExpansionAI_AO_UI_UO Not set Ack
                    byte PLCModuleTypeAck = response[6];         //PLC Module Type Ack
                    byte CRC = response[7];                     //CRC
                    byte EOF = response[8];                    //EOF
                    byte FinalCRC = (byte)(ProgramCRCAck ^ PlcModeAck ^ ExpansionModuleAck ^ ExpansionModuleIdAck ^ ExpansionAI_AO ^ PLCModuleTypeAck ^ 151);
                    if (CRC == FinalCRC)
                    {
                        if (PLCModuleTypeAck.ToString() != "0")
                        {
                            //"AI &AO & Id not set"
                            tssStatusLabel_show("Module Mismatch, kindly check the module used in project and one you are using", "Red");
                            MessageBox.Show("Module Missmatch, kindly check the module used in project and one you are using", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto Exitprocess;
                        }
                        if (!pLCCommunications.CheckReadyToDownload("MCode"))
                        {
                            result.Item1 = "Machine is not ready for transfer";
                            goto Exitprocess;
                        }
                        WindowsInputBlocker.BlockAllInput();
                        /////Adding temporary block to allow users download file without download frame
                        result = pLCCommunications.DownlodMcodeFile();
                        if (!result.Item1.Contains("Mcode File Downloaded Successfully")) goto Exitprocess;
                        progressBar1.Value = 60;
                        progressBar1.Update();
                        UpdateProgressBarWithText(result.Item1.Contains("Mcode File Downloaded Successfully") ? "Inprogress.." : result.Item1, 2000);
                        Task.Delay(400).Wait();
                        if (!pLCCommunications.IsMachineReady("MCode"))
                        {
                            result.Item1 = "Machine is not ready for communication,Try downloading again...";
                            result.Item2 = "OrangeRed";
                            goto Exitprocess;
                        }
                        if (!pLCCommunications.CheckReadyToDownload("CCode"))
                        {
                            result.Item1 = "Machine is not ready for transfer";
                            goto Exitprocess;
                        }
                        result = pLCCommunications.DownlodCcodeFile();
                        if (!result.Item1.Contains("Ccode File Downloaded Successfully")) goto Exitprocess;
                        progressBar1.Value = 80;
                        progressBar1.Update();
                        UpdateProgressBarWithText(result.Item1.Contains("Ccode File Downloaded Successfully") ? "Inprogress..." : result.Item1, 2000);
                        Task.Delay(400).Wait();
                        if (!pLCCommunications.IsMachineReady("CCode"))
                        {
                            result.Item1 = "Machine is not ready for communication,Try downloading again...";
                            result.Item2 = "OrangeRed";
                            goto Exitprocess;
                        }
                        if (xm.LoadedProject.PlcModel != null && xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                        {
                            if (!pLCCommunications.CheckReadyToDownload("BCode"))
                            {
                                result.Item1 = "Machine is not ready for transfer";
                                goto Exitprocess;
                            }
                            result = pLCCommunications.DownlodBcodeFile();
                            if (!result.Item1.Contains("BCode File Downloaded Successfully")) goto Exitprocess;
                            progressBar1.Value = 85;
                            progressBar1.Update();
                            UpdateProgressBarWithText(result.Item1.Contains("BCode File Downloaded Successfully") ? "Inprogress..." : result.Item1, 2000);
                            Task.Delay(400).Wait();
                            if (!pLCCommunications.IsMachineReady("BCode"))
                            {
                                result.Item1 = "Machine is not ready for communication,Try downloading again...";
                                result.Item2 = "OrangeRed";
                                goto Exitprocess;
                            }
                            if (xm.LoadedProject.PlcModel.EndsWith("E"))
                            {
                                if (!DownloadConfig(pLCCommunications, out result.Item1, out result.Item2))
                                {
                                    goto Exitprocess;
                                }
                            }
                        }
                        else
                        {
                            if (!DownloadConfig(pLCCommunications, out result.Item1, out result.Item2))
                            {
                                goto Exitprocess;
                            }
                        }
                        WindowsInputBlocker.UnblockAllInput();
                        if (!result.Item1.Contains("Downloaded Successfully")) goto Exitprocess;
                        else if (result.Item1.Contains("Downloaded Successfully")) result.Item1 = "Downloaded Successfully";
                        progressBar1.Value = 100;
                        progressBar1.Update();
                        UpdateProgressBarWithText(result.Item1, 2000);
                        Task.Delay(200).Wait();
                        ///Send final frame to complete the dowlonad process
                        pLCCommunications.SendDownloadCompleteFrame();
                        pLCCommunications.ReplaceIPAddress();
                        CopyFilesToDownloadFolder();
                        WindowsInputBlocker.UnblockAllInput();
                        Task.Delay(200).Wait();
                    }

                Exitprocess:
                    WindowsInputBlocker.UnblockAllInput();
                    if (result.Item1 != "") MessageBox.Show(result.Item1, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 100;
                }
                else
                {
                    WindowsInputBlocker.UnblockAllInput();
                    if (!xm.isCompilied)
                        MessageBox.Show("Kindly clear compile errors and compile the code again", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Kindly add atleast one block in Main block to continue with PLC operations", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                progressBar1.Visible = false;
                this.Cursor = Cursors.Default;
                WindowsInputBlocker.UnblockAllInput();
                WindowsInputBlocker.UnblockAllAppWindows();

            }

        }

        private bool CopyFilesToDownloadFolder()
        {
            string filepath = XMPS.Instance.LoadedProject.ProjectPath;
            string mainFilePath = filepath.Replace(filepath.Split('\\').Last(), "");
            string fileFilter = "*.txt";
            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(mainFilePath) || !Directory.Exists(mainFilePath))
                    return false;

                // Create DownloadedFiles folder path
                string downloadedFilesPath = Path.Combine(mainFilePath, "DownloadedFiles");

                // Check if DownloadedFiles folder exists
                if (Directory.Exists(downloadedFilesPath))
                {
                    // Empty the existing folder
                    try
                    {
                        DirectoryInfo dir = new DirectoryInfo(downloadedFilesPath);

                        // Delete all files
                        foreach (FileInfo file in dir.GetFiles())
                            file.Delete();

                        // Delete all subdirectories
                        foreach (DirectoryInfo subDir in dir.GetDirectories())
                            subDir.Delete(true);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        return false;
                    }
                    catch (IOException)
                    {
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(downloadedFilesPath);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        return false;
                    }
                    catch (IOException)
                    {
                        return false;
                    }
                }

                // Get files to copy
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                string[] allFiles;

                try
                {
                    allFiles = Directory.GetFiles(mainFilePath, fileFilter, searchOption);
                }
                catch
                {
                    return false;
                }

                // Filter out files that are already in the DownloadedFiles folder (prevent copying into itself)
                string[] filesToCopy = allFiles.Where(file =>
                    !file.StartsWith(downloadedFilesPath, StringComparison.OrdinalIgnoreCase)).ToArray();

                if (filesToCopy.Length == 0)
                    return true; // Not an error, just nothing to do

                // Copy files
                int errorCount = 0;

                foreach (string sourceFile in filesToCopy)
                {
                    try
                    {
                        string fileName = Path.GetFileName(sourceFile);
                        string destinationFile = Path.Combine(downloadedFilesPath, fileName);

                        // Handle duplicate file names
                        destinationFile = GetUniqueFileName(destinationFile);

                        // Check if source file exists and is accessible
                        if (!File.Exists(sourceFile))
                            continue;

                        // Check file attributes (skip hidden/system files)
                        FileAttributes attributes = File.GetAttributes(sourceFile);
                        if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden ||
                            (attributes & FileAttributes.System) == FileAttributes.System)
                            continue;

                        // Copy the file
                        File.Copy(sourceFile, destinationFile, true);
                    }
                    catch
                    {
                        errorCount++;
                    }
                }

                return errorCount == 0; // Return true only if no errors occurred
            }
            catch
            {
                return false;
            }

        }

        // Helper function to handle duplicate file names
        private static string GetUniqueFileName(string filePath)
        {
            if (!File.Exists(filePath))
                return filePath;

            string directory = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            int counter = 1;
            string newFilePath;

            do
            {
                newFilePath = Path.Combine(directory, $"{fileName}_{counter}{extension}");
                counter++;
            } while (File.Exists(newFilePath) && counter < 1000); // Prevent infinite loop

            if (counter >= 1000)
            {
                // If we hit the limit, use timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                newFilePath = Path.Combine(directory, $"{fileName}_{timestamp}{extension}");
            }

            return newFilePath;
        }

        private bool DownloadConfig(PLCCommunications pLCCommunications, out string errorText, out string errorColor)
        {
            Task.Delay(400).Wait();
            errorColor = "OrangeRed";
            (string, string) mqttResult = ("", "");
            if (!pLCCommunications.CheckReadyToDownload("Config"))
            {
                errorText = "Machine is not ready for transfer";
                return false;
            }
            mqttResult = pLCCommunications.DownlodMqttFile();
            if (!mqttResult.Item1.Contains("MQTT CNF File Downloaded Successfully"))
            {
                errorText = mqttResult.Item1;
                return false;
            }
            progressBar1.Value = 90;
            progressBar1.Update();
            UpdateProgressBarWithText(mqttResult.Item1.Contains("MQTT CNF File Downloaded Successfully") ? "Inprogress..." : mqttResult.Item1, 2000);
            Task.Delay(400).Wait();
            if (!pLCCommunications.IsMachineReady("Config"))
            {
                errorText = "Machine is not ready for communication,Try downloading again...";
                return false;
            }
            errorText = mqttResult.Item1;
            return true;
        }

        private void LoadEasyScan()
        {
            EasyScan easyScan = new EasyScan();
            easyScan.Text = "Easy Connection";
            easyScan.ShowDialog();
        }

        private void UpdateProgressBarWithText(string displayText, int delayMilliseconds)
        {
            this.progressBar1.Refresh();
            using (Graphics g = this.progressBar1.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(displayText, progressBar1.Font);
                PointF location = new PointF(
                    (progressBar1.Width / 2) - (textSize.Width / 2),
                    (progressBar1.Height / 2) - (textSize.Height / 2)
                );
                Thread.Sleep(delayMilliseconds);

                g.DrawString(displayText, progressBar1.Font, Brushes.Black, location);
            }
        }

        private void MenuModeDnldProject_Click(object sender, EventArgs e)
        {
            Download().ConfigureAwait(false);
        }
        private void strpBtnDownloadProject_Click(object sender, EventArgs e)
        {
            Download().ConfigureAwait(false);
        }
        private void MenuModeLogin_Click(object sender, EventArgs e)
        {
            LoginToPLC();
            Thread.Sleep(2000);
            tssStatusLabel_show("");
        }
        private void MenuModePLCStart_Click(object sender, EventArgs e)
        {
            RunPLC();
        }
        private void MenuModePLCStop_Click(object sender, EventArgs e)
        {
            CommandStopPLC();
        }
        private void MenuModePLCResetwarm_Click(object sender, EventArgs e)
        {
            ResetPLC("Warm");
        }
        private void MenuModePLCResetCold_Click(object sender, EventArgs e)
        {
            ResetPLC("Cold");
        }
        private void MenuModePLCResetOrigin_Click(object sender, EventArgs e)
        {
            ResetPLC("Origin");
        }
        private void strpBtnLogin_Click(object sender, EventArgs e)
        {
            LoginToPLC();
            Thread.Sleep(2000);
            tssStatusLabel_show("");
        }
        private void strpBtnLogout_Click(object sender, EventArgs e)
        {
            LogOutOfPLC();
        }
        private void MenuModeLogout_Click(object sender, EventArgs e)
        {
            LogOutOfPLC();
        }
        #region Extended features for statusLabel

        // tssStatusLabel extended function tssStatusLabel_msg
        // Message dissapear after fadeAway seconds
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public async void tssStatusLabel_msg(string msg, int fadeAfter = 5000, string color = "control")
        {
            if (msg.Length < 1) return;
            tssStatusLabel.Text = msg.Length > 230 ? msg.Substring(0, 230) : msg;
            statusMain.BackColor = Color.FromName(color);
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await Task.Delay(fadeAfter, _cancellationTokenSource.Token);
                tssStatusLabel.Text = "";
                statusMain.BackColor = Color.FromName("control");
            }
            catch (TaskCanceledException) { }
        }
        // Display message and color
        public void tssStatusLabel_show(string msg, string color = "control")
        {
            tssStatusLabel.Text = msg.Length > 230 ? msg.Substring(0, 230) : msg;
            statusMain.BackColor = Color.FromName(color);
        }
        #endregion
        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            if (xm != null)
            {
                this.Text = " XMPS 2000 " + xm.CurrentProjectData.ProjectName.ToString().Replace(".xmprj", "");
            }
        }
        private void MenuModeUpldProject_Click(object sender, EventArgs e)
        {
            UploadProject();
        }
        private void MenuProjectPrint_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject != null)
            {
                string filepath = Environment.GetFolderPath(DataPath) + "\\MessungSystems\\Output.pdf";
                ProjectPrintting.GeneratePdfDocument(filepath);
                printPDF(filepath);
            }
            else
            {
                MessageBox.Show("Select the Project before Printting", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void MenuProjectPageSetup_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject != null)
            {
                PageSetupForm PageSetupForm = new PageSetupForm();
                PageSetupForm.ProjectPath = xm.LoadedProject.ProjectPath.ToString();
                PageSetupForm.ShowDialog();
                ProjectPrintting.pagesize = PageSetupForm.PageSize;
                ProjectPrintting.IsLandscape = PageSetupForm.IsLandscape;
                ProjectPrintting.LeftMargin = PageSetupForm.LeftMargin;
                ProjectPrintting.RightMargin = PageSetupForm.RightMargin;
                ProjectPrintting.TopMargin = PageSetupForm.TopMargin;
                ProjectPrintting.BottomMargin = PageSetupForm.BottomMargin;
                ProjectPrintting.Header = PageSetupForm.Header;
                ProjectPrintting.Footer = PageSetupForm.Footer;
                ProjectPrintting.TitleHeader = PageSetupForm.TitleHeader;
                ProjectPrintting.ProjectName = PageSetupForm.ProjectName;
                ProjectPrintting.CustomerName = PageSetupForm.CustomerName;
                ProjectPrintting.TitleDate = PageSetupForm.TitleDate;
                ProjectPrintting.Profile = PageSetupForm.Profile;
            }
            else
            {
                MessageBox.Show("Open The File To Print ....", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void MenuProjectPrintPreview_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject != null)
            {
                string filepath = Environment.GetFolderPath(DataPath) + "\\MessungSystems\\Output.pdf";
                ProjectPrintting.GeneratePdfDocument(filepath);
                PrintPreviewForm p = new PrintPreviewForm();
                p.ShowDialog();
            }
            else
            {
                MessageBox.Show("Open The File To Print ....", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //RTC Time & Date
        private void rTCSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RtcForm rtc = new RtcForm();
            rtc.ShowDialog();
        }
        private void tsmAddDevice_Click(object sender, EventArgs e)
        {
            if (tvProjects.SelectedNode.Text.ToString() == "RS485")
            {
                // Open the RS485 mode selection form
                RS485ModeSelectionForm modeForm = new RS485ModeSelectionForm
                {
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedToolWindow,
                    Text = "Add Device"
                };

                var frmTemp = this.ParentForm as frmMain;
                DialogResult result = modeForm.ShowDialog(frmTemp);
                if (result == DialogResult.OK)
                {
                    if (modeForm.SelectedMode == "Slave" && (modeForm.SlaveID < 1 || modeForm.SlaveID > 247))
                    {
                        MessageBox.Show("Invalid Slave ID. Please enter a value between 1 and 247.");
                        return;
                    }
                    TreeNode IONode = tvProjects.SelectedNode;

                    var modBUSRTUMaster = (MODBUSRTUMaster)xm.LoadedProject.Devices
                        .FirstOrDefault(d => d.GetType().Name == "MODBUSRTUMaster");

                    if (modBUSRTUMaster != null && modeForm.SelectedMode == "Slave")
                    {
                        MessageBox.Show("Cannot switch to Slave mode while Master node already exists. Please delete the Master node first.",
                            "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (modeForm.SelectedMode == "Master" && xm.LoadedProject.RS485Mode == "Slave")
                    {
                        MessageBox.Show("Cannot switch to Master mode while Slave node already exists. Please delete the Slave node first.",
                            "XMPS-2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (modeForm.SelectedMode == "Master")
                    {
                        if (modBUSRTUMaster == null)
                        {
                            modBUSRTUMaster = new MODBUSRTUMaster();
                            xm.LoadedProject.Devices.Add(modBUSRTUMaster);
                        }
                        xm.LoadedProject.RS485Mode = "Master";
                    }
                    else if (modeForm.SelectedMode == "Slave")
                    {
                        xm.LoadedProject.RS485Mode = "Slave";
                        xm.LoadedProject.SlaveID = modeForm.SlaveID;
                    }

                    for (int i = IONode.Nodes.Count - 1; i >= 0; i--)
                    {
                        if (IONode.Nodes[i].Text == "MODBUS RTU Master" ||
                            IONode.Nodes[i].Text == "MODBUS RTU Slaves")
                        {
                            IONode.Nodes.RemoveAt(i);
                        }
                    }

                    string newNodeText = "";
                    string nodeInfo = "";

                    if (modeForm.SelectedMode == "Master")
                    {
                        newNodeText = "MODBUS RTU Master";
                        nodeInfo = "MODBUSRTUMaster";
                    }
                    else if (modeForm.SelectedMode == "Slave")
                    {
                        newNodeText = "MODBUS RTU Slaves";
                        nodeInfo = "MODBUSRTUSlaves";
                    }

                    if (!string.IsNullOrEmpty(newNodeText))
                    {
                        TreeNode newNode = new TreeNode(newNodeText);
                        NodeInfo niNewNode = new NodeInfo();
                        niNewNode.NodeType = NodeType.DeviceNode;
                        niNewNode.Info = nodeInfo;
                        newNode.Tag = niNewNode;
                        IONode.Nodes.Add(newNode);

                        // Mark project as modified
                        xm.MarkProjectModified(true);
                    }
                }
            }
            else
            {
                XMProForm tempform = new XMProForm();
                tempform.StartPosition = FormStartPosition.CenterParent;
                tempform.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                AddDevice UserControl = new AddDevice(_devicetype.ToString());
                tempform.Text = "Adding New Device";
                tempform.Height = UserControl.Height + 40;
                tempform.Width = UserControl.Width + 40;
                tempform.Controls.Add(UserControl);
                var frmTemp = this.Parent as Form;
                DialogResult res = tempform.ShowDialog(frmTemp);
                if (res == DialogResult.OK)
                {
                    if (UserControl.DeviceAdded.ToString() != "")
                    {
                        if (UserControl.DeviceAdded.ToString() == "Other")
                        {
                            XMProForm tempForm = new XMProForm();
                            tempForm.StartPosition = FormStartPosition.CenterParent;
                            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                            tempForm.Text = "Add Other Remote IO Model";
                            TreeNode IONode = tvProjects.SelectedNode;
                            NodeInfo niRemoteIONode = (NodeInfo)IONode.Tag;
                            var IONodeName = UserControl.DeviceAdded.ToString();// xm.LoadedProject.GetSlaveName(niRemoteIONode);
                            IOConfigAdd userControl = new IOConfigAdd();
                            tempForm.Height = userControl.Height + 25;
                            tempForm.Width = userControl.Width;
                            tempForm.Controls.Add(userControl);
                            var frmTemp2 = this.ParentForm as frmMain;
                            DialogResult result = tempForm.ShowDialog(frmTemp2);
                            if (result == DialogResult.OK)
                            {
                                NodeInfo newNodeInfo = new NodeInfo();
                                newNodeInfo.NodeType = NodeType.ListNode;
                                newNodeInfo.Info = UserControl.DeviceAdded.ToString();
                                TreeNode newNode = new TreeNode(IONodeName);
                                newNode.Tag = newNodeInfo;
                                IONode.Nodes.Add(newNode);
                            }
                        }
                        else
                        {

                            NodeInfo newNodeInfo = new NodeInfo();
                            newNodeInfo.NodeType = NodeType.ListNode;
                            TreeNode IONode = tvProjects.SelectedNode;
                            NodeInfo niRemoteIONode = (NodeInfo)IONode.Tag;
                            var IONodeName = UserControl.DeviceAdded.ToString();
                            var added = xm.LoadedProject.Tags.Where(d => d.Model != null && d.Model != "" && d.Model.ToString().Contains(IONodeName.ToString())).GroupBy(x => x.Model);
                            if (added.Count() == 0)
                            {
                                newNodeInfo.Info = UserControl.DeviceAdded.ToString();
                                IONodeName = UserControl.DeviceAdded.ToString();
                            }
                            else
                            {
                                int nextcounter = 1;
                                if (added.Count() > 0)
                                {
                                    nextcounter = added.Select(a => a.Key).Where(k => k.Contains(IONodeName.ToString() + "_")).Count() > 0 ?
                                                  added.Select(a => a.Key).Where(k => k.Contains(IONodeName.ToString() + "_")).Select(k => int.Parse(k.Replace(IONodeName.ToString() + "_", ""))).Max() + 1
                                                 : nextcounter;
                                }
                                newNodeInfo.Info = UserControl.DeviceAdded.ToString() + "_" + nextcounter;
                                IONodeName = UserControl.DeviceAdded.ToString() + "_" + nextcounter; // xm.LoadedProject.GetSlaveName(niRemoteIONode);  
                            }
                            string selectedModel = UserControl.DeviceAdded.ToString();
                            var model = RemoteModule.List.Find(x => x.Name.Equals(selectedModel));
                            if (_devicetype != "Remote I/O")
                            {
                                var expansionCount = xm.LoadedProject.Tags.Where(d => d.IoList == IOListType.ExpansionIO).Select(d => d.Model).Distinct().Count();
                                bool isNewExpansion = !xm.LoadedProject.Tags.Any(d => d.Model == newNodeInfo.Info && d.IoList == IOListType.ExpansionIO);
                                if (isNewExpansion && expansionCount >= 5)
                                {
                                    MessageBox.Show("Maximum of 5 expansions allowed. This project already has the maximum number of expansions.", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            if (_devicetype != "Remote I/O")
                            {
                                int currentIOCount = xm.LoadedProject.Tags.Where(d => (d.Model != "" && d.Model != "User Defined Tags" && (d.Model != null && !d.Model.EndsWith("Tags"))) && d.IoList != IOListType.Default && !d.Label.EndsWith("_OR") && !d.Label.EndsWith("_OL")).Count();
                                int newIOCount = currentIOCount + model.SupportedTypesAndIOs[0].Units;
                                if (newIOCount > 128)
                                {
                                    MessageBox.Show("Exceeding count of permitted IO's, can't add more than 128 IO's", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            TreeNode newNode = new TreeNode(IONodeName);
                            newNode.Tag = newNodeInfo;
                            IONode.Nodes.Add(newNode);
                            AddDevice ad = new AddDevice(IONodeName);
                            if (_devicetype == "Remote I/O")
                                ad.AddRemoteExpansionIOs(model, IOListType.RemoteIO, newNodeInfo.Info);
                            else
                                ad.AddRemoteExpansionIOs(model, IOListType.ExpansionIO, newNodeInfo.Info);
                        }
                    }
                }
            }
            ResetContextMenu();
        }
        private void crossRefrenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeCrossReference("");
        }
        public void InvokeCrossReference(string tagname)
        {
            if (XMPS.Instance.tagForCrossReference != null)
            {
                tagname = XMPS.Instance.tagForCrossReference;
            }
            textBoxError.Controls.Clear();
            CrossRefrence cr;
            if (!string.IsNullOrEmpty(tagname))
            {
                cr = new CrossRefrence(tagname);
            }
            else
            {
                cr = new CrossRefrence();
            }
            if (cr.textBox1 != null)
            {
                splitContainer1.Panel2Collapsed = false;
                groupBoxError.Text = "Cross Refrence";
                cr.TopLevel = false;
                textBoxError.Controls.Add(cr);
                textBoxError.Height = 160;
                cr.Visible = true;
                groupBoxError.Visible = true;
                groupBoxError.Height = 140;
                cr.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                cr.Dock = DockStyle.Fill;
                cr.Show();
                cr.Width = groupBoxError.Width;
                if (cr.CrossRefrenceDGV1.Width < cr.Width - 80)
                    cr.CrossRefrenceDGV1.Width = cr.Width - 60;
                cr.Height = groupBoxError.Height;
                if (cr.Height < 135)
                    cr.CrossRefrenceDGV1.Height = 135;
                else cr.CrossRefrenceDGV1.Height = cr.Height - 55;
                groupBoxError.Resize += (s, args) =>
                {
                    cr.Width = groupBoxError.Width;
                    cr.Height = groupBoxError.Height;
                    cr.CrossRefrenceDGV1.Height = cr.Height - 55;
                    cr.CrossRefrenceDGV1.Width = cr.Width - 60;
                };
                XMPS.Instance.tagForCrossReference = string.Empty;

            }
            else
            {
                MessageBox.Show("Please Select Element before using cross reference", "Cross Reference Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public CrossReferenceEventHandler CrossReferenceEvent;
        private void DownloadSourceCode()
        {
            if (xm.LoadedProject == null)
            {
                MessageBox.Show("No project found to download to PLC, Load the project and try again", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            progressBar1.Visible = true;
            progressBar1.Value = 25;
            progressBar1.Update();
            // Compile project and save it
            xm.isCompilied = true;
            try
            {
                SaveProject();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while save and compile");
                xm.isCompilied = false;
                progressBar1.Visible = false;
                return;
            }
            xm.isCompilied = false;
            if (xm.LoadedProject.MainLadderLogic.Count() > 0)
            {
                PLCCommunications pLCCommunications = new PLCCommunications();
                var resultDownload = pLCCommunications.DownlodSourceCodeFiles();
                progressBar1.Value = 100;
                tssStatusLabel_msg(resultDownload.Item1, 3000, resultDownload.Item2);
                if (resultDownload.Item1 == "Source code is downloading")
                    MessageBox.Show("Source Code Downloaded Successfully....", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kindly add atleast one block in Main block to continue with PLC operations", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Visible = false;
        }
        private void MenuModeDnldSourceCode_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to Download the Source to the Target?", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                DownloadSourceCode();
            }
            else
            if (dialogResult == DialogResult.No)
            {
                return;
            }
        }


        private void MenuModeUpldSourceCode_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to Upload the Source to the Target?", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                UploadSourceCode();
            }
            else
            if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
        static string GetPlcModelSafe(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"File not found: {filePath}");
                }

                XDocument doc = XDocument.Load(filePath);

                if (doc.Root == null)
                {
                    throw new InvalidOperationException("XML file has no root element");
                }

                var plcModelAttribute = doc.Root.Attribute("PlcModel");
                if (plcModelAttribute == null)
                {
                    throw new InvalidOperationException("PlcModel attribute not found in root element");
                }

                return plcModelAttribute.Value;
            }
            catch (XmlException ex)
            {
                throw new InvalidOperationException($"Invalid XML format: {ex.Message}", ex);
            }
        }

        private void UploadSourceCode()
        {
            //DialogResult result = NewProjectDialog();
            if (ChangeLoadedProject())
            {
                string ipAddress = xm._connectedIPAddress;


                PLCCommunications pLCCommunications = new PLCCommunications();
                progressBar1.Visible = true;
                progressBar1.Value = 25;
                var resultUpload = pLCCommunications.UplodSourceCodeFiles();
                if (resultUpload.ToString().Contains("Source code is uploading"))
                {
                    NewProjectForm newProjectForm = new NewProjectForm();
                    string tempFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MessungSystems\XMPS2000\XM Projects\tempfolder\project.xmprj");
                    string model = GetPlcModelSafe(tempFilePath);
                    newProjectForm.SetNewProject(model);
                    CreateFilesForNewProject(newProjectForm, false, newProjectForm.projectInfo);
                    xm._connectedIPAddress = ipAddress;
                    string baseProjectPath = xm.LoadedProject.ProjectPath;
                    File.Delete(baseProjectPath);
                    File.Copy(tempFilePath.Replace(".zip", ".xmprj"), baseProjectPath);

                    progressBar1.Value = 100;
                    progressBar1.Visible = false;

                    UpdateProjectFile();
                    AddSystemTags();
                    MessageBox.Show("Source Code Uploaded Successfully....", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string error = resultUpload.ToString().Split(',').First();
                    error = error == "Exception of type 'XMPS2000.Core.App.TFTPClient+TFTPException' was thrown." ? "File not found in PLC, kincly check again" : error.Replace("(", "");
                    MessageBox.Show(error.ToString(), "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar1.Value = 100;
                    progressBar1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Operation Terminated", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuModeUpdateFirmware_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject != null)
            {
                if (xm.PlcStatus == "LogIn")
                {
                    if (MessageBox.Show("PLC in Loggedin mode need to Log out before updating firmware, Do you want to logout ?", "XMPS 2000", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        LogOutOfPLC();
                    else
                        return;
                }
            }
            PLCCommunications pLCCommunications = new PLCCommunications();
            var resultStop = pLCCommunications.UpdateFirmwares("");
            tssStatusLabel_msg(resultStop.Item1, 5000, resultStop.Item2);
        }
        private void InitializeTimerCounters()
        {
            Counters.Clear();
            Counters.Add("FB", new Counter { Instruction = "FB", CurrentPosition = 0, Maximum = 255 });

            foreach (InstructionTypeDeserializer instruction in XMPS.Instance.instructionsList)
            {
                if (instruction.TcNameDetails != null && !string.IsNullOrEmpty(instruction.TcNameDetails.Instruction))
                {
                    Counters.Add(instruction.Text, new Counter { Instruction = instruction.TcNameDetails.Instruction, CurrentPosition = instruction.TcNameDetails.CurrentPosition, Maximum = instruction.TcNameDetails.Maximum });
                }
            }
        }
        private void MenuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutbox = new AboutBox();
            aboutbox.ShowDialog();
        }
        private void MenuViewDInfo_Click(object sender, EventArgs e)
        {
            DeviceInfo deviceInfo = new DeviceInfo();
            deviceInfo.ShowDialog();
        }
        private void lblProjects_Click(object sender, EventArgs e)
        {
            this.btnPin.Visible = true;
            this.tblLeftPanel.Visible = true;
            if (splcMain.Panel2.Controls[0] is SplitContainer sc1 && lastSplitterDistance > sc1.SplitterDistance)
            {
                sc1.SplitterDistance = 150;
            }
            this.lblProjects.Dock = DockStyle.Top;
            this.tblLeftPanel.Width = 225;
            this.lblProjects.AutoSize = false;
            this.lblProjects.Size = new Size(226, 30);
            this.lblProjects.Location = new Point(1, 3);
            this.lblProjects.Text = "Loaded" + " " + "Projects";
            this.lblProjects.BackColor = Color.LightSkyBlue;
            this.btnPin.Location = new Point(lastSplitterDistance - 40, 1);
            this.splcMain.SplitterDistance = lastSplitterDistance /*225*/;
        }
        public bool IsUdfbFromibrary(string udfbName, bool isFromHVAC = false)
        {
            try
            {
                if (isFromHVAC)
                    return false;
                string normalizedName = udfbName.Replace(" Logic", "").Trim();
                string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"MessungSystems","XMPS2000","Library");
                string librarySubFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD",StringComparison.OrdinalIgnoreCase)? "XBLDLibraries": "XMLibraries";
                string libraryPath = Path.Combine(basePath, librarySubFolder);
                string[] csvFiles = Directory.Exists(libraryPath)? Directory.GetFiles(libraryPath, "*.csv"): Array.Empty<string>();

                var libNames = csvFiles
                    .Select(f => Path.GetFileNameWithoutExtension(f))
                    .Select(name => name.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)? name.Substring(0, name.Length - 6).Trim(): name);

                return libNames.Any(fileName => fileName.Equals(normalizedName, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return false;
            }
        }

        private void tsmAddUDFB_Click(object sender, EventArgs e)
        {
            XMProForm tempform = new XMProForm();
            tempform.StartPosition = FormStartPosition.CenterParent;
            tempform.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            UserDefinedFunctionBlockControl _functionBlockCntrl = new UserDefinedFunctionBlockControl();
            tempform.Text = "Adding New UDFB";
            tempform.Height = _functionBlockCntrl.Height + 40;
            tempform.Width = _functionBlockCntrl.Width + 40;
            tempform.Controls.Add(_functionBlockCntrl);
            var frmTemp = this.Parent as Form;
            DialogResult res = tempform.ShowDialog(frmTemp);
            if (res == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(_functionBlockCntrl.FunctionBlockName))
                {
                    string fbname = _functionBlockCntrl.FunctionBlockName;
                    if (IsUdfbFromibrary(fbname, false))
                    {
                        MessageBox.Show($"UDFB '{fbname}' already exists in library. You cannot create a duplicate.","UDFB Library Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CreateUDFBSubNodes(fbname);
                }
            }
        }
        private void SelectNodeByName(string nodeName)
        {
            TreeNode foundNode = FindNodeInAllRoots(nodeName);
            if (foundNode != null)
            {
                tvProjects.SelectedNode = foundNode;
                tvProjects.Focus();
                // Optional: Give focus to the TreeView
            }
        }


        // Enhanced method to search within all root nodes and their children
        private TreeNode FindNodeInAllRoots(string nodeName)
        {
            // Search through all root nodes
            foreach (TreeNode rootNode in tvProjects.Nodes)
            {
                // Use the recursive method to search the entire tree starting from each root
                TreeNode foundNode = FindNodeRecursive(rootNode, nodeName);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }

        // Recursive method that searches a node and all its descendants (n-level deep)
        private TreeNode FindNodeRecursive(TreeNode nodes, string nodeName)
        {
            foreach (TreeNode node in nodes.Nodes)
            {
                // Check current node
                if (node.Text == nodeName)
                {
                    return node;
                }

                // If current node has children, search them recursively
                if (node.Nodes.Count > 0)
                {
                    TreeNode foundNode = FindNodeRecursive(node, nodeName);
                    if (foundNode != null)
                    {
                        return foundNode;
                    }
                }
            }
            return null;
        }
        private TreeNode FindNodeByName(TreeNodeCollection nodes, string nodeName)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Name == nodeName)
                {
                    return node;
                }

                // Recursively search child nodes
                TreeNode childNode = FindNodeByName(node.Nodes, nodeName);
                if (childNode != null)
                {
                    return childNode;
                }
            }
            return null;
        }

        // Helper method to get all nodes (including root and all descendants)
        private IEnumerable<TreeNode> GetAllNodes(TreeNode rootNode)
        {
            yield return rootNode; foreach (TreeNode childNode in rootNode.Nodes)
            {
                foreach (TreeNode descendant in GetAllNodes(childNode))
                { yield return descendant; }
            }
        }
        private void CreateUDFBSubNodes(string fbname)
        {
            TreeNode mainNode = tvProjects.SelectedNode;
            xm.LoadedProject.AddUDFBBlock((NodeInfo)mainNode.Tag, fbname);
            NodeInfo niMainBlockNode = (NodeInfo)mainNode.Tag;
            var mainBlockName = fbname;
            NodeInfo newMainNodeInfo = new NodeInfo();
            TreeNode newMainNode = new TreeNode(mainBlockName);
            newMainNodeInfo.NodeType = NodeType.BlockNode;
            newMainNodeInfo.Info = "UDFB";
            newMainNode.Tag = newMainNodeInfo;
            mainNode.Nodes.Add(newMainNode);
            TreeNode blockNode = newMainNode;
            xm.LoadedProject.AddUDFBBlock((NodeInfo)blockNode.Tag, fbname + " Logic");
            NodeInfo niBlockNode = (NodeInfo)blockNode.Tag;
            var blockName = fbname + " Logic";
            NodeInfo newNodeInfo = new NodeInfo();
            newNodeInfo.NodeType = NodeType.BlockNode;
            if (niMainBlockNode.Info == "UserFunctionBlock")
            {
                NodeInfo tagniBlockNode = (NodeInfo)blockNode.Tag;
                var newblockName = blockName.Replace(" Logic", "") + " Tags";
                NodeInfo newTagNodeInfo = new NodeInfo();
                newTagNodeInfo.NodeType = NodeType.ListNode;
                newTagNodeInfo.Info = "UDFTags";
                TreeNode newTagNode = new TreeNode(newblockName);
                newTagNode.Tag = newTagNodeInfo;
                blockNode.Nodes.Add(newTagNode);
                newNodeInfo.NodeType = NodeType.BlockNode;
                newNodeInfo.Info = "UDFLadder";
            }
            else if (niBlockNode.Info == "HIBlock")
            {
                newNodeInfo.Info = "HILadder";
            }
            else
            {
                newNodeInfo.Info = "Ladder";
            }
            TreeNode newNode = new TreeNode(blockName);
            newNode.Tag = newNodeInfo;
            blockNode.Nodes.Add(newNode);
            xm.MarkProjectModified(true);
        }

        private void tsmEditUDFB_Click(object sender, EventArgs e)
        {
            string selectedNode = tvProjects.SelectedNode.Text;
            string normalizedSelectedNode = selectedNode.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase)
                ? selectedNode.Substring(0, selectedNode.Length - 6).Trim()
                : selectedNode;

            string baseLibraryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MessungSystems", "XMPS2000", "Library");
            string projectLibraryFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD") ? "XBLDLibraries" : "XMLibraries";
            string libraryPath = Path.Combine(baseLibraryPath, projectLibraryFolder);
            List<string> udfbNames = XMPS.Instance.LoadedProject.UDFBInfo.Select(u => u.UDFBName).ToList();

            var fileNames = Directory.GetFiles(libraryPath)
                .Select(Path.GetFileNameWithoutExtension)
                .Select(name => name.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase) ? name.Substring(0, name.Length - 6).Trim() : name);

            bool isUdfbMatch = fileNames.Any(fileName =>
                fileName.Equals(normalizedSelectedNode, StringComparison.OrdinalIgnoreCase) &&
                udfbNames.Any(udfbName => udfbName.Equals(normalizedSelectedNode, StringComparison.OrdinalIgnoreCase)));

            if (isUdfbMatch)
            {
                string savedChoice = XMPS.Instance.LoadedProject.GetUDFBChoice(normalizedSelectedNode);
                string savedLocalCopyName = null;
                if (!string.IsNullOrEmpty(savedChoice) && savedChoice.StartsWith("CreateLocalCopy:"))
                {
                    savedLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);
                }

                bool localCopyWithDifferentNameExists = !string.IsNullOrEmpty(savedLocalCopyName) && !savedLocalCopyName.Equals(normalizedSelectedNode, StringComparison.OrdinalIgnoreCase) &&
                    XMPS.Instance.LoadedProject.Blocks.Any(b =>b.Type == "UserFunctionBlock" && b.Name.Equals(savedLocalCopyName, StringComparison.OrdinalIgnoreCase));

                // If library UDFB exists AND a local copy exists, it means library was re-imported
                // In this case, ignore saved choice and show popup
                if (localCopyWithDifferentNameExists)
                {
                    using (var optionsForm = new UDFBEditOptionsForm(normalizedSelectedNode))
                    {
                        if (optionsForm.ShowDialog() == DialogResult.OK)
                        {
                            if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                            {
                                XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "EditMainFile");
                                ShowUDFBEditForm(normalizedSelectedNode);
                            }
                            else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                            {
                                string newLocalCopyName = optionsForm.LocalCopyName;
                                XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "CreateLocalCopy:" + newLocalCopyName);
                                CreateLocalCopyFromLibraryUDFB(normalizedSelectedNode, newLocalCopyName);
                            }
                        }
                    }
                    return;
                }

                if (string.IsNullOrEmpty(savedChoice))
                {
                    using (var optionsForm = new UDFBEditOptionsForm(normalizedSelectedNode))
                    {
                        if (optionsForm.ShowDialog() == DialogResult.OK)
                        {
                            if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                            {
                                XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "EditMainFile");
                                ShowUDFBEditForm(normalizedSelectedNode);
                            }
                            else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                            {
                                string initialLocalCopyName = optionsForm.LocalCopyName;
                                XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "CreateLocalCopy:" + initialLocalCopyName);
                                CreateLocalCopyFromLibraryUDFB(normalizedSelectedNode, initialLocalCopyName);
                            }
                        }
                    }
                }
                else
                {
                    // ==== Use previously saved choice directly ====
                    if (savedChoice == "EditMainFile")
                    {
                        ShowUDFBEditForm(normalizedSelectedNode);
                    }
                    else if (savedChoice.StartsWith("CreateLocalCopy:"))
                    {
                        string existingLocalCopyName = savedChoice.Substring("CreateLocalCopy:".Length);
                        bool specificLocalCopyExists = XMPS.Instance.LoadedProject.Blocks.Any(b =>
                            b.Type == "UserFunctionBlock" && b.Name.Equals(existingLocalCopyName, StringComparison.OrdinalIgnoreCase));

                        if (specificLocalCopyExists)
                        {
                            ShowUDFBEditForm(existingLocalCopyName);
                        }
                        else
                        {
                            XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "");

                            using (var optionsForm = new UDFBEditOptionsForm(normalizedSelectedNode))
                            {
                                if (optionsForm.ShowDialog() == DialogResult.OK)
                                {
                                    if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.EditMainFile)
                                    {
                                        XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "EditMainFile");
                                        ShowUDFBEditForm(normalizedSelectedNode);
                                    }
                                    else if (optionsForm.SelectedOption == UDFBEditOptionsForm.EditOption.CreateLocalCopy)
                                    {
                                        string recreatedLocalCopyName = optionsForm.LocalCopyName;
                                        XMPS.Instance.LoadedProject.SetUDFBChoice(normalizedSelectedNode, "CreateLocalCopy:" + recreatedLocalCopyName);
                                        CreateLocalCopyFromLibraryUDFB(normalizedSelectedNode, recreatedLocalCopyName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ShowUDFBEditForm(normalizedSelectedNode);
            }
        }
        private void CreateLocalCopyFromLibraryUDFB(string originalName, string localCopyName)
        {
            try
            {
                TreeNode selectedNode = tvProjects.SelectedNode;
                if (selectedNode == null)
                {
                    MessageBox.Show("Please select a UDFB node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TreeNode udfbNode = null;
                if (selectedNode.Text.EndsWith(" Logic", StringComparison.OrdinalIgnoreCase))
                {
                    udfbNode = selectedNode.Parent;
                }
                else
                {
                    udfbNode = selectedNode;
                }

                if (udfbNode == null || udfbNode.Parent == null)
                {
                    MessageBox.Show("Unable to locate the UDFB node in TreeView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TreeNode udfbParentNode = udfbNode.Parent;

                string logicBlockName = originalName + " Logic";
                int _blockIndex = XMPS.Instance.LoadedProject.Blocks.FindIndex(d => d.Name == logicBlockName);
                List<string> rungs = new List<string>();

                if (_blockIndex >= 0)
                {
                    rungs = new List<string>(XMPS.Instance.LoadedProject.Blocks[_blockIndex].Elements);
                }

                UDFBInfo uDFBInfo = XMPS.Instance.LoadedProject.UDFBInfo.FirstOrDefault(t => t.UDFBName.Equals(originalName));
                if (uDFBInfo != null)
                {
                    uDFBInfo.UDFBName = localCopyName;
                }

                udfbParentNode.Nodes.Remove(udfbNode);

                XMPS.Instance.LoadedProject.Blocks.RemoveAll(t => (t.Type.Equals("UDFB") || t.Type.Equals("UserFunctionBlock")) && t.Name.Equals(originalName + " Logic"));
                XMPS.Instance.LoadedProject.Blocks.RemoveAll(t => (t.Type.Equals("UDFB") || t.Type.Equals("UserFunctionBlock")) && t.Name.Equals(originalName));

                TreeNode tnUDFLocalBlock = new TreeNode(localCopyName);
                NodeInfo nitnUDFLocalBlock = new NodeInfo();
                nitnUDFLocalBlock.NodeType = NodeType.BlockNode;
                nitnUDFLocalBlock.Info = "UDFB";
                tnUDFLocalBlock.Tag = nitnUDFLocalBlock;

                TreeNode tnUDFTag = new TreeNode(localCopyName + " Tags");
                NodeInfo nitnUDFTag = new NodeInfo();
                nitnUDFTag.NodeType = NodeType.ListNode;
                nitnUDFTag.Info = "UDFTags";
                tnUDFTag.Tag = nitnUDFTag;
                tnUDFLocalBlock.Nodes.Add(tnUDFTag);

                TreeNode tnUDFLogic = new TreeNode(localCopyName + " Logic");
                NodeInfo nitnUDFLogic = new NodeInfo();
                nitnUDFLogic.NodeType = NodeType.BlockNode;
                nitnUDFLogic.Info = "UDFLadder";
                tnUDFLogic.Tag = nitnUDFLogic;
                tnUDFLocalBlock.Nodes.Add(tnUDFLogic);

                udfbParentNode.Nodes.Add(tnUDFLocalBlock);

                Block blk = new Block();
                blk.Name = localCopyName;
                blk.Type = "UserFunctionBlock";
                XMPS.Instance.LoadedProject.Blocks.Add(blk);

                Block blk1 = new Block();
                blk1.Name = localCopyName + " Logic";
                blk1.Type = "UDFB";
                XMPS.Instance.LoadedProject.Blocks.Add(blk1);

                int _blockIndexNew = XMPS.Instance.LoadedProject.Blocks.FindIndex(d => d.Name == localCopyName + " Logic");
                if (_blockIndexNew >= 0)
                {
                    XMPS.Instance.LoadedProject.Blocks[_blockIndexNew].Elements.Clear();
                    XMPS.Instance.LoadedProject.Blocks[_blockIndexNew].Elements.AddRange(rungs);
                }

                tvProjects.SelectedNode = tnUDFLocalBlock;
                tnUDFLocalBlock.Expand();

                save(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while creating local copy from library UDFB: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private TreeNode FindOrCreateUDFBParentNode()
        {
            // Search for existing UDFB parent node in the project tree
            if (tvProjects.Nodes.Count > 0)
            {
                TreeNode projectRoot = tvProjects.Nodes[0]; // Main project node

                foreach (TreeNode node in projectRoot.Nodes)
                {
                    if (node.Tag is NodeInfo nodeInfo)
                    {
                        // Check for UserFunctionBlock Info or UDFB text
                        if (nodeInfo.Info == "UserFunctionBlock" || node.Text == "UDFB")
                        {
                            return node;
                        }
                    }
                }

                // If not found, create it
                TreeNode tnUDFB = new TreeNode("UDFB");
                NodeInfo niUDFB = new NodeInfo();
                niUDFB.NodeType = NodeType.BlockNode;
                niUDFB.Info = "UserFunctionBlock";
                tnUDFB.Tag = niUDFB;
                projectRoot.Nodes.Add(tnUDFB);
                tnUDFB.Expand();

                return tnUDFB;
            }

            return null;
        }
        private void ShowUDFBEditForm(string udfbname)
        {
            XMProForm tempform = new XMProForm();
            tempform.StartPosition = FormStartPosition.CenterParent;
            tempform.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            UserDefinedFunctionBlockControl _functionBlockCntrl = new UserDefinedFunctionBlockControl(udfbname);
            tempform.Text = "Adding New UDFB";
            tempform.Height = _functionBlockCntrl.Height + 40;
            tempform.Width = _functionBlockCntrl.Width + 40;
            tempform.Controls.Add(_functionBlockCntrl);
            var frmTemp = this.Parent as Form;
            DialogResult res = tempform.ShowDialog(frmTemp);
            if (res == DialogResult.OK)
            {
                //after edit remove rung form copied rungs from current project.
                CheckAndRemoveRungFromCopied(udfbname);
                save(false);
            }

        }
        private void btnPin_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject != null)
            {
                if (this.tblLeftPanel.Width == 0)
                {
                    this.tblLeftPanel.Width = 225;
                    this.lblProjects.Width = 226;
                    this.btnPin.Left = 171;
                }
                else
                {
                    this.tblLeftPanel.Visible = false;
                    this.lblProjects.AutoSize = false;
                    this.lblProjects.BackColor = Color.SkyBlue;
                    this.lblProjects.Dock = DockStyle.None;
                    this.lblProjects.Size = new Size(20, 290);
                    this.lblProjects.Text = "Loaded" + "\u00A0" + "Projects";
                    this.lblProjects.Location = new Point(10, 3);
                    this.splcMain.SplitterDistance = 30;
                    this.btnPin.Location = new Point(185, 1);
                }
            }
            else
            {
                MessageBox.Show("Please Open the Project", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void parallelWatchWindowToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (xm.PlcStatus == "LogIn")
            {
                ParallelWindow _window = new ParallelWindow();
                _window.Show(this);      //Show  as a non - modal dialog
            }
            else
            {
                MessageBox.Show("Please Enable Login for using Parallel Watch Window", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void tsmDeleteUDFB_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = tvProjects.SelectedNode;
            UDFBInfo udfbinfo = (UDFBInfo)XMPS.Instance.LoadedProject.UDFBInfo.Where(u => u.UDFBName == treeNode.Text).FirstOrDefault();
            if (udfbinfo == null) return;
            int count = xm.LoadedProject.LogicRungs.Where(T => T.OpCodeNm.Equals(udfbinfo.UDFBName)).Count();
            if (count > 0)
            {
                List<string> getudfbrungs = GetUDFBUsedRungs(udfbinfo.UDFBName);
                string message = "This UDFB-Block is already used in:\n" + string.Join(Environment.NewLine, getudfbrungs);
                MessageBox.Show(message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DeleteUDFBFromProject(treeNode.Text);
                var logicBlock = xm.LoadedProject.Blocks.FirstOrDefault(b => b.Type.Equals("UDFB") && b.Name.Equals(udfbinfo.UDFBName + " Logic"));
                if (logicBlock != null)
                {
                    xm.LoadedProject.Blocks.Remove(logicBlock);
                }
                var userFunctionBlock = xm.LoadedProject.Blocks.FirstOrDefault(b => b.Type.Equals("UserFunctionBlock") && b.Name.Equals(udfbinfo.UDFBName));
                if (userFunctionBlock != null)
                {
                    xm.LoadedProject.Blocks.Remove(userFunctionBlock);
                }
                treeNode.Text += "-" + "UDFB";
                DeleteNode(treeNode);
            }
            //after edit remove rung form copied rungs from current project.
            CheckAndRemoveRungFromCopied(udfbinfo.UDFBName);
        }

        private void CheckAndRemoveRungFromCopied(string uDFBName)
        {
            List<string> udfbRungs = DataCVX.CurrentRung?.Where(t => t.Contains($"FN:{uDFBName}")).ToList();
            if (udfbRungs != null && udfbRungs.Count > 0)
            {
                foreach (string currentRung in udfbRungs)
                {
                    int rungIndex = DataCVX.CurrentRung.IndexOf(currentRung);
                    DataCVX.CurrentRungComment.RemoveAt(rungIndex);
                    DataCVX.CurrentRung.RemoveAt(rungIndex);
                    DataCVX.CopyPresent = false;
                }
            }
        }

        private List<string> GetUDFBUsedRungs(string udfbName, bool skipCommented = false)
        {
            List<string> rungs = new List<string>();
            List<Block> BlockCount = new List<Block>();
            BlockCount = xm.LoadedProject.Blocks.Where(t => t.Type == "LogicBlock" || t.Type == "InterruptLogicBlock").ToList();
            if (skipCommented)
            {
                BlockCount = BlockCount.Where(t => xm.LoadedProject.MainLadderLogic.Contains(t.Name)).ToList();
            }

            for (int B = 0; B < BlockCount.Count; B++)
            {
                if (xm.LoadedScreens.ContainsKey($"LadderForm#{BlockCount[B].Name}"))
                {
                    LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"LadderForm#{BlockCount[B].Name}"];
                    LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();

                    for (int k = 0; k < LadderDrawing.LadderDesign.Active.Elements.Count(); k++)
                    {
                        for (int j = 0; j < LadderDrawing.LadderDesign.Active.Elements[k].Elements.Count; j++)
                        {
                            LadderElement ld = LadderDrawing.LadderDesign.Active.Elements[k].Elements[j];
                            if (ld.Attributes["function_name"].ToString().Equals(udfbName))
                            {
                                if (skipCommented && ld.Attributes.Any(t => t.Name.Equals("isCommented")))
                                {
                                    break;
                                }
                                rungs.Add($"Logic Block {BlockCount[B].Name} rung No {k + 1}");
                            }
                        }
                    }
                }
            }
            return rungs;
        }
        /// <summary>
        /// Deletes a User-Defined Function Block (UDFB) from the project by its name.
        /// </summary>
        /// <param name="udfbname">The name of the User-Defined Function Block to be deleted.</param>
        private void DeleteUDFBFromProject(string udfbname)
        {
            var relatedOriginal = XMPS.Instance.LoadedProject.UDFBEditChoices.Select(entry => entry.Split('='))
                .Where(parts => parts.Length == 2 && parts[1].Equals("CreateLocalCopy", StringComparison.OrdinalIgnoreCase)).Select(parts => parts[0])
                .FirstOrDefault(orig => XMPS.Instance.LoadedProject.UDFBInfo.Any(u => u.UDFBName.Equals(udfbname, StringComparison.OrdinalIgnoreCase)));
            XMPS.Instance.LoadedProject.UDFBEditChoices.RemoveAll(x =>!string.IsNullOrEmpty(x) &&
                (x.StartsWith(udfbname + "=", StringComparison.OrdinalIgnoreCase) ||(!string.IsNullOrEmpty(relatedOriginal) && x.StartsWith(relatedOriginal + "=", StringComparison.OrdinalIgnoreCase))));
            XMPS.Instance.LoadedProject.Tags.RemoveAll(t => t.Model != null && t.Model.Equals($"{udfbname} Tags"));
            XMPS.Instance.LoadedProject.UDFBInfo.RemoveAll(u => u.UDFBName == udfbname);
            xm.LoadedProject.Blocks.RemoveAll(b => b.Type == "UserFunctionBlock" && b.Name == udfbname);
            xm.LoadedProject.Blocks.RemoveAll(b => (b.Type == "UDFB") && b.Name.Equals(udfbname + " Logic"));
            if (xm.LoadedScreens.ContainsKey($"UDFLadderForm#{udfbname} Logic"))
            {
                xm.LoadedScreens.Remove($"UDFLadderForm#{udfbname} Logic");
            }
            if (xm.LoadedScreens.ContainsKey($"{udfbname} Tags#UDFTags"))
            {
                xm.LoadedScreens.Remove($"{udfbname} Tags#UDFTags");
            }
        }

        private void MenuHelpIndex_Click(object sender, EventArgs e)
        {
            MessageBox.Show(appdatapath.ToString());
        }
        private void CntxaddSusBlock_Click(object sender, EventArgs e)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            TreeNode Mqttpublish = tvProjects.SelectedNode;
            NodeInfo niMqttPublishBlockNode = (NodeInfo)Mqttpublish.Tag;
            var MqttNodeName = xm.LoadedProject.GetMqttName(niMqttPublishBlockNode);
            int count = Mqttpublish.Nodes.Count;
            count++;
            //Adding MQTT topics
            if (niMqttPublishBlockNode.Info == "MQTT Subscribe")
            {
                tempForm.Text = "Add Subscribe Parameters";
                SuscribeParameter userControl = new SuscribeParameter();
                tempForm.Height = userControl.Height + 25;
                tempForm.Width = userControl.Width;
                userControl.Text = "Add Subscribe Parameters";
                DialogResult uresult = userControl.ShowDialog();
                if (uresult == DialogResult.OK)
                    PerformTreeNodeActions(niMqttPublishBlockNode, Mqttpublish, niMqttPublishBlockNode.Info.ToString());
            }
            ResetContextMenu();
        }
        private void CntxAddMQTTForm_Click(object sender, EventArgs e)
        {
            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = "MQTT Configuration";
            MqttSettingsUserControl userControl = new MqttSettingsUserControl();
            userControl.Text = "MQTT Configuration";
            MQTTForm mqttForm = (MQTTForm)xm.LoadedProject.Devices.FirstOrDefault(d => d.GetType().Name == "MQTTForm");
            TreeNode MQTTNode = tvProjects.SelectedNode;
            NodeInfo niMQTTNode = (NodeInfo)MQTTNode.Tag;
            tempForm.Height = userControl.Height + 25;
            tempForm.Width = userControl.Width;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK)
            {
                NodeInfo newNodeInfo = new NodeInfo();
                newNodeInfo.NodeType = NodeType.DeviceNode;
                newNodeInfo.Info = "Mqtt ";
                TreeNode newNode = new TreeNode();
                newNode.Tag = newNodeInfo;
                //Remove Addition of Salves in Tree 
                PerformTreeNodeActions(niMQTTNode, MQTTNode, niMQTTNode.Info.ToString());
            }
            ResetContextMenu();
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            string screenName = xm.CurrentScreen;
            if (xm.CurrentScreen.StartsWith("LadderForm"))
            {
                if (xm.LoadedScreens.ContainsKey($"{xm.CurrentScreen}"))
                {
                    LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"{xm.CurrentScreen}"];
                    _windowRef.getLadderEditor().ClearStatus();
                }
            }
        }
        private void tsmExportTags_Click(object sender, EventArgs e)
        {
            string currentScreen = tvProjects.SelectedNode.Parent.Text == "Tags" ? "Tags" : tvProjects.SelectedNode.Parent.Parent.Text;

            if (currentScreen != null)
            {
                List<XMIOConfig> userDefinedTags = null;
                if (currentScreen.Equals("Tags"))
                {
                    userDefinedTags = xm.LoadedProject.Tags.Where(D => !D.LogicalAddress.StartsWith("S3") && D.Model.Equals("") ||
                                 (D.Model == "User Defined Tags" && D.IoList.ToString() != "OnBoardIO" &&
                                 D.IoList.ToString() != "ExpansionIO" && D.IoList.ToString() != "RemoteIO")).OrderBy(D => D.Key).ToList();
                }
                else if (currentScreen.Equals("UDFB"))
                {
                    userDefinedTags = xm.LoadedProject.Tags.Where(D => D.Model == tvProjects.SelectedNode.Text.Replace("Logic", "Tags")).ToList();
                }
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                    saveFileDialog.Title = "Save File";
                    if (currentScreen.Equals("UDFTags"))
                        saveFileDialog.FileName = $"{xm.CurrentScreen.Split('#')[0]}.csv";
                    else
                        saveFileDialog.FileName = $"{xm.LoadedProject.ProjectName}_UDT.csv";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            writer.WriteLine("User Defined Tags");
                            writer.WriteLine("Model,Label,LogicalAddress,Tag,IOList,Type,InitialValue,Retentive,ShowLogicalAddress,RetentiveAddress,Mode,ActualName");

                            foreach (var tag in userDefinedTags)
                            {
                                writer.WriteLine($"{tag.Model},{tag.Label},{tag.LogicalAddress}," +
                                                 $"{tag.Tag},{tag.IoList},{tag.Type},{tag.InitialValue}," +
                                                 $"{tag.Retentive},{tag.ShowLogicalAddress},{tag.RetentiveAddress}," +
                                                 $"{tag.Mode},{tag.ActualName}");
                            }
                        }
                        MessageBox.Show($"Tags are save in {saveFileDialog.FileName} File", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show($"Error occurred at the time of Export tags.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void tsmImportTags_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = tvProjects.SelectedNode;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                openFileDialog.Title = "Open File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    List<XMIOConfig> userDefinedTags = new List<XMIOConfig>();

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read the first line and check if it matches "User Defined Tags"
                        string firstLine = reader.ReadLine();
                        if (firstLine != "User Defined Tags")
                        {
                            MessageBox.Show("Please select valid file for import tags", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains("Model,Label,LogicalAddress,Tag,IOList,Type,InitialValue,Retentive,ShowLogicalAddress,RetentiveAddress,Mode,ActualName"))
                                continue;

                            string[] parts = line.Split(',');
                            int key = xm.LoadedProject.Tags.Count() + 1;
                            XMIOConfig tag = new XMIOConfig
                            {
                                Model = parts[0],
                                Label = parts[1],
                                LogicalAddress = parts[2],
                                Tag = parts[3],
                                IoList = (IOListType)Enum.Parse(typeof(IOListType), parts[4]),
                                Type = (IOType)Enum.Parse(typeof(IOType), parts[5]),
                                InitialValue = parts[6],
                                Retentive = bool.Parse(parts[7]),
                                ShowLogicalAddress = bool.Parse(parts[8]),
                                RetentiveAddress = parts[9],
                                Mode = parts[10],
                                Editable = false,
                                Key = key,
                                ActualName = parts[11]
                            };

                            //Adding Check for the not Adding Dublicate Tag at the Time of Importing Tags form file.
                            if (xm.LoadedProject.Tags.Where(t => t.LogicalAddress == tag.LogicalAddress).Count() == 0)
                            {
                                bool IsValidtag = XMProValidator.ValidateImportingTag(tag.LogicalAddress);
                                if (IsValidtag)
                                {
                                    userDefinedTags.Add(tag);
                                }
                                else
                                {
                                    skippedTagsList.Add(Tuple.Create(tag, "This tag is already accupied by Last DWord tag."));
                                }
                            }
                            else
                            {
                                skippedTagsList.Add(Tuple.Create(tag, "This tag is already present in the current project"));
                            }
                            if (xm.LoadedProject.PlcModel != null && xm.LoadedProject.PlcModel.StartsWith("XBLD"))
                            {
                                BacNetTagFactory.AddTagtoBacNetObject(tag.Tag.ToString(), tag.LogicalAddress.ToString(), tag.Label.ToString(), tag.Type, tag.Mode.ToString(), false);
                            }
                        }
                    }
                    xm.LoadedProject.Tags.AddRange(userDefinedTags.OrderBy(t => t.Key));
                    PerformTreeNodeActions((NodeInfo)treeNode.Tag, treeNode, treeNode.Text);
                    var result = MessageBox.Show("Tags Added Succesfully", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Error occurred at the time of importing tags.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static string GetValue(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return "";
            }
            if (part.Contains("LogicalAddress"))
            {
                string[] splitPart = part.Trim().Split(':');
                return splitPart.Length > 1 ? string.Join(":", splitPart.Skip(1)).Trim() : splitPart[0].Trim();
            }
            else if (part.Contains("RetentiveAddress"))
            {
                string[] splitPart = part.Trim().Split(':');
                return splitPart.Length > 1 ? string.Join(":", splitPart.Skip(1)).Trim() : splitPart[0].Trim();
            }
            return part.Trim().Split(':')[1].Trim();
        }
        private void tsmExportLogicBlock_Click(object sender, EventArgs e)
        {
            save();
            string selectedLogicBlock = tvProjects.SelectedNode.Text;
            string currentBlockType = "Logic Block";
            selectedLogicBlock = tvProjects.SelectedNode.Parent.Text.Equals("UDFB") ? selectedLogicBlock + " Logic" : selectedLogicBlock;

            //Adding check for not allow to export empty logic block.
            if (xm.LoadedProject.Blocks.Where(x => x.Name == selectedLogicBlock).First().Elements.Count() == 0)
            {
                MessageBox.Show("Empty logic blocks are not exported.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> curBlockRungs = xm.LoadedProject.Blocks.Where(x => x.Name == selectedLogicBlock).First().Elements;
            List<string> curBlockComments = xm.LoadedProject.Blocks.Where(x => x.Name == selectedLogicBlock).First().Comments;
            List<string> addressList = new List<string>();
            if (curBlockRungs.Count > 0 && (tvProjects.SelectedNode.Parent.Text.Equals("Logic Blocks") || tvProjects.SelectedNode.Parent.Text.Equals("Interrupt Logic Blocks")))
            {
                foreach (string currung in curBlockRungs)
                {
                    foreach (string inputs in currung.Split(' ').Where(x => x.Contains("IN:") || x.Contains("OP:")))
                    {
                        string opcode = currung.Substring(currung.IndexOf("OPC:") + 4, 4);
                        if (inputs.Contains("IN:"))
                        {
                            addressList.Add(inputs.Replace("IN:", ""));
                            if (opcode == "0390")
                            {
                                string[] parts = inputs.Replace("IN:", "").Replace("~", "").Replace("]", "").Replace(")", "").Replace(";", "").Split(':');
                                int secondPart = int.Parse(parts[1]);
                                for (int j = 1; j < 16; j++)
                                {
                                    int lastTagAdd = int.Parse(parts[1]) + j;
                                    string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                                    addressList.Add(endAdd);
                                }
                            }
                        }
                        else
                        {
                            addressList.Add(inputs.Replace("OP:", ""));
                            if (opcode == "03A2")
                            {
                                string[] parts = inputs.Replace("OP:", "").Replace("~", "").Replace("]", "").Replace(")", "").Replace(";", "").Split(':');
                                int secondPart = int.Parse(parts[1]);
                                for (int j = 1; j < 16; j++)
                                {
                                    int lastTagAdd = int.Parse(parts[1]) + j;
                                    string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                                    addressList.Add(endAdd);
                                }
                            }
                        }
                    }
                }
            }
            //check if exporting logic block is udfb
            string parentNode = tvProjects.SelectedNode.Parent.Text;
            LadderWindow _windowRef = null;
            UDFBInfo currentUDFBData = null;
            if (parentNode.Equals("UDFB"))
            {
                if (!xm.LoadedScreens.ContainsKey($"UDFLadderForm#{selectedLogicBlock}"))
                {
                    MessageBox.Show("Please Open UDFB Logic Block", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                currentBlockType = "UDFB Block";
                currentUDFBData = xm.LoadedProject.UDFBInfo.Where(t => t.UDFBName.Equals(selectedLogicBlock.Replace(" Logic", ""))).FirstOrDefault();
                _windowRef = (LadderWindow)xm.LoadedScreens[$"UDFLadderForm#{selectedLogicBlock}"];
            }
            else
                _windowRef = (LadderWindow)xm.LoadedScreens[$"LadderForm#{selectedLogicBlock}"];
            LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
            if (tvProjects.SelectedNode.Parent.Text.Equals("Logic Blocks")
               || tvProjects.SelectedNode.Parent.Text.Equals("Interrupt Logic Blocks"))
            {
                foreach (LadderElement ld in LadderDesign.Active.Elements)
                {
                    foreach (LadderElement ld1 in ld.Elements)
                    {
                        if (ld1.CustomType != "LadderDrawing.Comment" && ld1.CustomType != "LadderDrawing.FunctionBlock")
                        {
                            if (ld1.CustomType == "LadderDrawing.DummyParallelParent")
                            {
                                List<LadderElement> ld2 = ld1.Elements;
                                foreach (LadderElement ld3 in ld2)
                                {
                                    GetAddFromParalledElement(ld3, ref addressList);
                                }
                            }
                            else if (ld1.CustomType == "LadderDrawing.Coil")
                            {
                                GetAddFromParalledElement(ld1, ref addressList);
                            }
                            else
                            {
                                if (ld1.Attributes.Any(t => t.Name.Equals("LogicalAddress")) && ld1.Attributes["LogicalAddress"].ToString() != "")
                                {
                                    addressList.Add(ld1.Attributes["LogicalAddress"].ToString());
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(ld1.Attributes["caption"].ToString()) && !ld1.Attributes["caption"].ToString().Equals("???"))
                                    {
                                        var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(ld1.Attributes["caption"].ToString()));
                                        if (tag != null && !string.IsNullOrEmpty(tag.LogicalAddress))
                                        {
                                            addressList.Add(tag.LogicalAddress);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = $"{selectedLogicBlock}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine($"Block Type :- {currentBlockType}");
                        if (parentNode.Equals("UDFB"))
                        {
                            writer.WriteLine($"Inputs : {currentUDFBData.Inputs}");
                            writer.WriteLine($"Outputs : {currentUDFBData.Outputs}");
                            writer.WriteLine($"UDFBName : {currentUDFBData.UDFBName}");
                            foreach (UserDefinedFunctionBlock ud in currentUDFBData.UDFBlocks)
                            {
                                writer.WriteLine($"DataType:{ud.DataType} Name:{ud.Name} Text:{ud.Text} Type:{ud.Type}");
                            }
                        }
                        else
                        {
                            writer.WriteLine($"OrigionalName : {GetOrigionalBlockName(selectedLogicBlock)}");
                        }

                        IEnumerable<(string rung, string comment)> data = curBlockRungs.Zip(curBlockComments, (rung, comment) => (rung, comment));

                        foreach ((string rung, string comment) in data)
                        {
                            writer.WriteLine($"{rung}======{comment}");
                        }
                        writer.WriteLine($"User Defined Tags");

                        List<XMIOConfig> userDefinedTags = new List<XMIOConfig>();
                        if (tvProjects.SelectedNode.Parent.Text.Equals("Logic Blocks")
                           || tvProjects.SelectedNode.Parent.Text.Equals("Interrupt Logic Blocks"))
                        {
                            foreach (string logicalAdd in addressList)
                            {
                                if (!logicalAdd.StartsWith("I") && !logicalAdd.StartsWith("Q") && logicalAdd != "")
                                {
                                    XMIOConfig tag = xm.LoadedProject.Tags.Where(t => t.LogicalAddress == logicalAdd.Replace("~", "").Replace("]", "").Replace(")", "").Replace(";", "")).FirstOrDefault();
                                    if (tag != null && !userDefinedTags.Contains(tag))
                                    {
                                        userDefinedTags.Add(tag);
                                    }
                                }
                            }
                        }
                        else if (tvProjects.SelectedNode.Parent.Text == "UDFB")
                        {
                            userDefinedTags = xm.LoadedProject.Tags.Where(D => D.Model == tvProjects.SelectedNode.Text + " Tags").ToList();
                        }

                        foreach (var tag in userDefinedTags)
                        {
                            StringBuilder line = new StringBuilder();
                            line.Append($"Model: {tag.Model}, ");
                            line.Append($"Label: {tag.Label}, ");
                            line.Append($"LogicalAddress: {tag.LogicalAddress}, ");
                            line.Append($"Tag: {tag.Tag}, ");
                            line.Append($"IOList: {tag.IoList}, ");
                            line.Append($"Type: {tag.Type}, ");
                            line.Append($"InitialValue: {tag.InitialValue}, ");
                            line.Append($"Retentive: {tag.Retentive}, ");
                            line.Append($"ShowLogicalAddress: {tag.ShowLogicalAddress}, ");
                            line.Append($"RetentiveAddress: {tag.RetentiveAddress}, ");
                            line.Append($"Mode: {tag.Mode}, ");
                            line.Append($"Editable: {tag.Editable}, ");
                            line.Append($"Key: {tag.Key}, ");
                            line.Append($"ActualName: {tag.ActualName}");
                            writer.WriteLine(line.ToString());
                        }
                    }
                    File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.ReadOnly);
                    MessageBox.Show($"{selectedLogicBlock} Data Successfully Stored Into {saveFileDialog.FileName}", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }

        private object GetOrigionalBlockName(string selectedLogicBlock)
        {
            if (selectedLogicBlock.Equals("Interrupt_Logic_Block01"))
                return "INTP_1";
            if (selectedLogicBlock.Equals("Interrupt_Logic_Block02"))
                return "INTP_2";
            if (selectedLogicBlock.Equals("Interrupt_Logic_Block03"))
                return "INTP_3";
            if (selectedLogicBlock.Equals("Interrupt_Logic_Block04"))
                return "INTP_4";
            return selectedLogicBlock;
        }

        private void GetAddFromParalledElement(LadderElement ld1, ref List<string> addList)
        {
            //addList.Add(ld1.Attributes["LogicalAddress"].ToString());
            if (ld1.Attributes.Any(t => t.Name.Equals("LogicalAddress")) && ld1.Attributes["LogicalAddress"].ToString() != "")
            {
                addList.Add(ld1.Attributes["LogicalAddress"].ToString());
            }
            else
            {
                if (!string.IsNullOrEmpty(ld1.Attributes["caption"].ToString()) && !ld1.Attributes["caption"].ToString().Equals("???"))
                {
                    var tag = xm.LoadedProject.Tags.FirstOrDefault(t => t.Tag.Equals(ld1.Attributes["caption"].ToString()));
                    if (tag != null && !string.IsNullOrEmpty(tag.LogicalAddress))
                    {
                        addList.Add(tag.LogicalAddress);
                    }
                }
            }
            if (ld1.Elements.Count > 0)
            {
                foreach (LadderElement element in ld1.Elements)
                {
                    GetAddFromParalledElement(element, ref addList);
                }
            }
        }
        private void tsmImportLogicBlock_Click(object sender, EventArgs e)
        {
            this.LogicalblockName = string.Empty;
            this.currentAddedNode = null;
            string selectedLogicBlock = tvProjects.SelectedNode.Text;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                openFileDialog.Title = "Open File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ImportLogicBlockAndUDFB(filePath, selectedLogicBlock);
                }
                else
                {
                    //Deleting the Added Logical block form the tree-View if don't importing loigcal block.
                    if (LogicalblockName != "" && tvProjects.SelectedNode.Text == "Logic Blocks")
                    {
                        RemoveLogicBlockNode(LogicalblockName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            GetCanvasForUndoRedo();
        }
        private bool IsLibraryFile(string filePath)
        {
            try
            {
                string baseLibraryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"MessungSystems", "XMPS2000", "Library");
                string libraryFolder = XMPS.Instance.LoadedProject.PlcModel.StartsWith("XBLD")? "XBLDLibraries": "XMLibraries";
                string libraryPath = Path.Combine(baseLibraryPath, libraryFolder);
                return filePath.StartsWith(libraryPath, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public void ImportLogicBlockAndUDFB(string filePath, string selectedLogicBlock, bool isFromHVAC = false, bool redirectToUDFBNode = false)
        {
            if (!tvProjects.SelectedNode.Text.Equals("UDFB") && selectedLogicBlock.Equals("UDFB"))
                SelectNodeByName("UDFB");
            if (!isFromHVAC && IsLibraryFile(filePath))
            {
                MessageBox.Show("Cannot access library directly.\nTo access library, please use the 'HVAC_AND_OTHERS' instruction node.",
                    "Library Access Restricted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            string originalName = string.Empty;
            List<string> importedCurBlockRungs = new List<string>();
            List<string> importedCurBlockComments = new List<string>();
            List<XMIOConfig> userDefinedTags = new List<XMIOConfig>();
            List<string> wrongAddressErrors = new List<string>();
            UDFBInfo udfbInfo = new UDFBInfo();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                bool tags = false;
                bool isUDFBImported = false;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Equals("Block Type :- UDFB Block") && tvProjects.SelectedNode.Text == "UDFB")
                    {
                        isUDFBImported = true;
                        continue;
                    }
                    if (line.StartsWith("OrigionalName : "))
                    {
                        originalName = line.Substring(line.IndexOf(":") + 1).Trim();
                    }
                    //not allow to user import UDFB block in normal Logical block.
                    if (line.Equals("Block Type :- UDFB Block") && (tvProjects.SelectedNode.Text == "Logic Blocks" || tvProjects.SelectedNode.Text.StartsWith("Interrupt_Logic_Block")))
                    {
                        MessageBox.Show($"You are trying to import UDFB Block not valid operation", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //Deleting the Added Logical block form the tree-View if don't importing loigcal block.
                        if (LogicalblockName != "" && tvProjects.SelectedNode.Text == "Logic Blocks")
                        {
                            RemoveLogicBlockNode(LogicalblockName);
                        }
                        return;
                    }
                    //not allow to user import normal block as UDFB block
                    if (line.Equals("Block Type :- Logic Block") && tvProjects.SelectedNode.Text == "UDFB")
                    {
                        MessageBox.Show("You are trying to import normal logic block as UDFB not valid operation", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //Creating UDFB Object
                    if (line.StartsWith("Inputs :") && isUDFBImported)
                    {
                        udfbInfo.Inputs = Convert.ToInt32(line.Substring(line.IndexOf(":") + 1).Trim());
                        continue;
                    }
                    if (line.StartsWith("Outputs :") && isUDFBImported)
                    {
                        udfbInfo.Outputs = Convert.ToInt32(line.Substring(line.IndexOf(":") + 1).Trim());
                        continue;
                    }
                    if (line.StartsWith("UDFBName :") && isUDFBImported)
                    {
                        udfbInfo.UDFBName = line.Substring(line.IndexOf(":") + 1).Trim();
                        if (!isFromHVAC && IsUdfbFromibrary(udfbInfo.UDFBName))
                        {
                            MessageBox.Show($"UDFB '{udfbInfo.UDFBName}' exists in Library. To access library, please use the 'HVAC_AND_OTHERS' instruction node.",
                            "Library Restriction",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            return;
                        }

                        bool isAlreadyPresent = XMPS.Instance.LoadedProject.UDFBInfo.Any(x => x.UDFBName.Equals(udfbInfo.UDFBName));
                        if (isAlreadyPresent)
                        {
                            MessageBox.Show("Same Name UDFB already present in current project", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        continue;
                    }
                    if (line.StartsWith("DataType:") && isUDFBImported)
                    {
                        string[] parts = line.Split(new[] { "DataType:", "Name:", "Text:", "Type:" }, StringSplitOptions.RemoveEmptyEntries);
                        UserDefinedFunctionBlock block = new UserDefinedFunctionBlock
                        {
                            DataType = parts[0].Trim(),
                            Name = parts[1].Trim(),
                            Text = parts[2].Trim(),
                            Type = parts[3].Trim()
                        };
                        udfbInfo.UDFBlocks.Add(block);
                        continue;
                    }

                    //Getting Tags
                    if (line.Equals("User Defined Tags"))
                    {
                        tags = true;
                    }
                    if (tags == false)
                    {
                        // Split each line into rung and comment based on the delimiter "======"
                        string[] parts = line.Split(new string[] { "======" }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            importedCurBlockRungs.Add(parts[0].Trim());
                            importedCurBlockComments.Add(parts[1].Trim());
                        }
                    }
                    List<string> duplicateWarnings = new List<string>();
                    if (tags)
                    {
                        List<string> duplicateMessages = new List<string>();

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(","))
                            {
                                string[] parts = line.Split(',');
                                XMIOConfig tag = new XMIOConfig
                                {
                                    Model = GetValue(parts[0]),
                                    Label = GetValue(parts[1]),
                                    LogicalAddress = GetValue(parts[2]),
                                    Tag = GetValue(parts[3]),
                                    IoList = (IOListType)Enum.Parse(typeof(IOListType), GetValue(parts[4])),
                                    Type = (IOType)Enum.Parse(typeof(IOType), GetValue(parts[5])),
                                    InitialValue = GetValue(parts[6]),
                                    Retentive = bool.Parse(GetValue(parts[7])),
                                    ShowLogicalAddress = bool.Parse(GetValue(parts[8])),
                                    RetentiveAddress = GetValue(parts[9]),
                                    Mode = GetValue(parts[10]),
                                    Editable = bool.Parse(GetValue(parts[11])),
                                    Key = int.Parse(GetValue(parts[12])),
                                    ActualName = GetValue(parts[13])
                                };
                                //Validation for Double Word
                                bool isValidDoubleWord = XMProValidator.ValidateDoubleWordTag(tag, ref wrongAddressErrors);
                                if (!isValidDoubleWord)
                                    continue;

                                bool isDuplicateTag = xm.LoadedProject.Tags.Any(t => t.Tag == tag.Tag);
                                bool isDuplicateLogicalAddress = xm.LoadedProject.Tags.Any(t => t.LogicalAddress == tag.LogicalAddress);

                                if (isFromHVAC)
                                {
                                    if (isDuplicateTag || isDuplicateLogicalAddress)
                                    {
                                        string duplicateInfo = "";
                                        if (isDuplicateTag)
                                            duplicateInfo += "- Duplicate Tag Name: " + tag.Tag + "\n";
                                        if (isDuplicateLogicalAddress)
                                            duplicateInfo += "- Duplicate Logical Address: " + tag.LogicalAddress + "\n";
                                        duplicateMessages.Add(duplicateInfo);
                                    }

                                    userDefinedTags.Add(tag);
                                }
                                else
                                {
                                    if (!isDuplicateTag && !isDuplicateLogicalAddress)
                                    {
                                        userDefinedTags.Add(tag);
                                    }
                                }
                            }
                        }
                        if (isFromHVAC && duplicateMessages.Any())
                        {
                            string message = "Warning: Duplicate entries found while importing HVAC UDFB\n\n";
                            message += string.Join("\n", duplicateMessages.Distinct());
                            message += "\n\nThe UDFB will still be imported. Please review duplicates.";
                            MessageBox.Show(message, "Duplicate Tag/Address Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                xm.LoadedProject.Tags.AddRange(userDefinedTags.OrderBy(t => t.Key));
            }
            if (tvProjects.SelectedNode.Text == "Logic Blocks")
            {
                bool isAlreadyPresent = XMPS.Instance.LoadedProject.Blocks.Where(t => t.Type.Equals("LogicBlock")).Any(t => t.Name.Equals(originalName));
                if (isAlreadyPresent)
                {
                    DialogResult result = MessageBox.Show($"the logic block name :- {originalName} is already present do you want to replace?", "XMPS2000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        ActivateForm("LadderForm" + "#" + originalName);
                        var currentBlockForm = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
                        LadderCanvas curCanvas = currentBlockForm.getLadderEditor().getCanvas();
                        LadderDesign.Active = curCanvas.getDesignView();
                        int rungNo = 0;
                        int currentRungCount = LadderDesign.Active.Elements.Count();
                        foreach (LadderElement ladderElement in LadderDesign.Active.Elements.ToList())
                        {
                            rungNo++;
                            bool islast = currentRungCount == rungNo ? true : false;
                            LadderDesign.ClickedElement = ladderElement.Elements.FirstOrDefault(t => t.CustomType == "LadderDrawing.Coil"
                                                           || t.CustomType == "LadderDrawing.Contact" || t.CustomType == "LadderDrawing.FunctionBlock");
                            curCanvas.DeleteRungbyContextMenu(islast);
                            LadderDesign.ClickedElement = null;
                        }

                    }
                    else
                    {
                        originalName = XMProValidator.GetNewNameForLogicBlock(originalName);
                        bool isAlreadyAdded = XMPS.Instance.LoadedProject.Blocks.Where(t => t.Type.Equals("LogicBlock")).Any(t => t.Name.Equals(originalName));
                        if (isAlreadyAdded)
                        {
                            ActivateForm("LadderForm" + "#" + originalName);
                            var currentBlockForm = (LadderWindow)xm.LoadedScreens[xm.CurrentScreen];
                            LadderCanvas curCanvas = currentBlockForm.getLadderEditor().getCanvas();
                            LadderDesign.Active = curCanvas.getDesignView();
                            int rungNo = 0;
                            int currentRungCount = LadderDesign.Active.Elements.Count();
                            foreach (LadderElement ladderElement in LadderDesign.Active.Elements.ToList())
                            {
                                rungNo++;
                                bool islast = currentRungCount == rungNo ? true : false;
                                LadderDesign.ClickedElement = ladderElement.Elements.FirstOrDefault(t => t.CustomType == "LadderDrawing.Coil"
                                                               || t.CustomType == "LadderDrawing.Contact" || t.CustomType == "LadderDrawing.FunctionBlock");
                                curCanvas.DeleteRungbyContextMenu(islast);
                                LadderDesign.ClickedElement = null;
                            }
                        }
                        else
                        {
                            AddNewLogicBlock(tvProjects.SelectedNode, originalName);
                            selectedLogicBlock = LogicalblockName;
                            ActiveCurrentScreeen(LogicalblockName);
                        }
                    }
                }
                else
                {
                    AddNewLogicBlock(tvProjects.SelectedNode, originalName);
                    selectedLogicBlock = LogicalblockName;
                    ActiveCurrentScreeen(LogicalblockName);
                }
            }
            if (selectedLogicBlock.StartsWith("Interrupt_Logic_Block"))
            {
                //Adding check for not allow to export empty logic block.
                if (importedCurBlockRungs.Count() == 0)
                {
                    MessageBox.Show("Empty logic blocks are not imported.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (importedCurBlockRungs.Count > 10)
                {
                    LadderWindow window = (LadderWindow)xm.LoadedScreens["LadderForm#" + selectedLogicBlock];
                    LadderDrawing.LadderDesign.Active = window.getLadderEditor().getCanvas().getDesignView();

                    if (LadderDrawing.LadderDesign.Active.Elements.Count() < 10)
                    {
                        int currentElement = LadderDrawing.LadderDesign.Active.Elements.Count();
                        if (currentElement == 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Inside the Interrupt Logic Block You can Add Only First 10 Rungs", "XMPS2000", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            if (dialogResult == DialogResult.OK)
                            {
                                DataCVX.CopyRung(importedCurBlockRungs.Take(10).ToList(), importedCurBlockComments.Take(10).ToList());
                                Paste();
                            }
                        }
                        else
                        {
                            int remainRungCount = 10 - currentElement;
                            DataCVX.CopyRung(importedCurBlockRungs.Take(remainRungCount).ToList(), importedCurBlockComments.Take(remainRungCount).ToList());
                            Paste();
                        }
                    }
                }
                else
                {
                    LadderWindow window = (LadderWindow)xm.LoadedScreens["LadderForm#" + selectedLogicBlock];
                    LadderDrawing.LadderDesign.Active = window.getLadderEditor().getCanvas().getDesignView();
                    int currentElement = LadderDrawing.LadderDesign.Active.Elements.Count();
                    int remainRungCount = 10 - currentElement;
                    DataCVX.CopyRung(importedCurBlockRungs.Take(remainRungCount).ToList(), importedCurBlockComments.Take(remainRungCount).ToList());
                    Paste();
                }
            }
            else
            {
                //Adding check for not allow to export empty logic block.
                if (importedCurBlockRungs.Count() == 0)
                {
                    MessageBox.Show("Empty logic blocks are not imported.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (LogicalblockName != "")
                    {
                        RemoveLogicBlockNode(LogicalblockName);
                    }
                    return;
                }

                if (tvProjects.SelectedNode.Text.Equals("UDFB"))
                {
                    XMPS.Instance.LoadedProject.UDFBInfo.Add(udfbInfo);
                    CreateUDFBSubNodes(udfbInfo.UDFBName);
                    ActiveCurrentScreeen(udfbInfo.UDFBName + " Logic", true);
                    DataCVX.CopyRung(importedCurBlockRungs, importedCurBlockComments);
                    Paste();
                    if (redirectToUDFBNode)
                    {
                        SelectNodeByName(udfbInfo.UDFBName + " Logic");
                    }
                }
                else
                {
                    DataCVX.CopyRung(importedCurBlockRungs, importedCurBlockComments);
                    Paste();
                }
            }
            this.LogicalblockName = "";
            string successMessage;
            if (isFromHVAC)
            {
                successMessage = "Library added successfully\n"+ "Note: \n"+ String.Join(Environment.NewLine, wrongAddressErrors);
            }
            else
            {
                successMessage = "Logic Block and All User Defined Tags Added Successfully\n"+ "Note: \n"+ String.Join(Environment.NewLine, wrongAddressErrors);
            }
            MessageBox.Show(successMessage, "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SaveProject();

        }

        private void RemoveLogicBlockNode(string logicalblockName)
        {
            xm.LoadedProject.Blocks.RemoveAll(d => d.Name == LogicalblockName);
            LadderEditorControl l1 = new LadderEditorControl();
            l1.ReScale();
            l1.Update();
            l1.Invalidate();
            ShowDefaultLogicalBlocks();
            foreach (TreeNode tree in tvProjects.Nodes[0].Nodes[0].Nodes[0].Nodes[0].Nodes)
            {
                if (tree.Text == LogicalblockName)
                {
                    tvProjects.Nodes.Remove(tree);
                }
            }
            xm.LoadedScreens.Remove("LadderForm" + "#" + LogicalblockName);
            this.LogicalblockName = "";
        }

        private void ActiveCurrentScreeen(string blockName, bool isUDFB = false)
        {
            LadderWindow frmLadder = new LadderWindow(blockName);
            frmLadder.MdiParent = this;
            frmLadder.TopLevel = false;
            frmLadder.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(frmLadder);
            frmLadder.Show();
            string ladderFormType = isUDFB ? "UDFLadderForm" : "LadderForm";
            AddToLoadedForms(ladderFormType + "#" + blockName, frmLadder);
            ActivateForm(ladderFormType + "#" + blockName);
            LoadCurrentBlock(ladderFormType + "#" + blockName);
        }

        private void MenuHelpUsrManual_Click(object sender, EventArgs e)
        {
            string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\InstructionInformationFile.chm");
            Help.ShowHelp(this, filePath, HelpNavigator.Topic, "Files/AND.htm");
        }
        private void easyScan_Click(object sender, EventArgs e)
        {
            LoadEasyScan();
        }

        public void ShowErroredLogicBlock(string logicBlockName, string rungNo, string LogicalAddress)
        {
            xm = XMPS.Instance;
            string olderScreen = xm.CurrentScreen;
            if (!olderScreen.Contains("LadderForm"))
            {
                try
                {
                    //getting logic block tree node by logicBlockName and performTreeNodeActions function to shown an instruction label.
                    TreeNode firstLogicBlock = tvProjects.Nodes[0].Nodes[0].Nodes[0].Nodes.
                    Cast<TreeNode>().SelectMany(node => node.Nodes.Cast<TreeNode>())
                    .FirstOrDefault(subNode => subNode.Text == logicBlockName);
                    PerformTreeNodeActions((NodeInfo)firstLogicBlock.Tag, firstLogicBlock, firstLogicBlock.Text);
                }
                catch
                {

                }

            }
            ActivateForm("LadderForm" + "#" + logicBlockName);
            string currentRungNo = rungNo.Split('g')[1].Trim();
            LadderWindow _windowRef = (LadderWindow)xm.LoadedScreens[$"LadderForm#{logicBlockName}"];
            LadderDrawing.LadderDesign.Active = _windowRef.getLadderEditor().getCanvas().getDesignView();
            LadderElement slectedErrorRung = LadderDrawing.LadderDesign.Active.Elements.Where(T => T.Position.Index == Convert.ToInt32(currentRungNo) - 1).FirstOrDefault();
            if (slectedErrorRung == null)
                return;
            _windowRef.getLadderEditor().ChaneVisibleRungs(new Point(slectedErrorRung.Position.X, slectedErrorRung.Position.Y - 20));
            _windowRef.getLadderEditor().getCanvas().ChaneCursorPosition(new Point(slectedErrorRung.Position.X, slectedErrorRung.Position.Y));

        }
        public void ShowErroredFrmMainGrid(string formName, int rowNumber)
        {
            TreeNode systemConfigNode = tvProjects.Nodes[0].Nodes[0].Nodes.Cast<TreeNode>()
                        .FirstOrDefault(n => n.Text == "System Configuration");
            erroredRungNo = rowNumber;
            xm.LoadedProject.NewFocusIndex = rowNumber;
            TreeNode targetNode = null;
            switch (formName)
            {
                case "MODBUS RTU Master":
                case "MODBUS RTU Slaves":
                case "MODBUS TCP Client":
                case "MODBUS TCP Server":
                    if (systemConfigNode != null)
                    {
                        targetNode = systemConfigNode.Nodes
                            .Cast<TreeNode>()
                            .Where(n => n.Text == "Ethernet" || n.Text == "RS485")
                            .SelectMany(n => n.Nodes.Cast<TreeNode>())
                            .FirstOrDefault(n => n.Text == formName);
                    }
                    if (targetNode != null)
                    {
                        tvProjects.SelectedNode = targetNode;
                        var e = new TreeNodeMouseClickEventArgs(targetNode, MouseButtons.Left, 1, 0, 0);
                        tvProjects_NodeMouseClick(tvProjects, e);
                    }
                    break;
                case "MQTT Subscribe":
                case "MQTT Publish":
                    if (systemConfigNode != null)
                    {
                        targetNode = systemConfigNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "Ethernet")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "MQTT")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == formName);
                    }
                    if (targetNode != null)
                    {
                        tvProjects.SelectedNode = targetNode;
                        var e = new TreeNodeMouseClickEventArgs(targetNode, MouseButtons.Left, 1, 0, 0);
                        tvProjects_NodeMouseClick(tvProjects, e);
                    }
                    break;
                case "Binary Value":
                    if (systemConfigNode != null)
                    {
                        targetNode = systemConfigNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "Ethernet")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "BACNET IP")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == formName);
                    }
                    if (targetNode != null)
                    {
                        tvProjects.SelectedNode = targetNode;
                        var e = new TreeNodeMouseClickEventArgs(targetNode, MouseButtons.Left, 1, 0, 0);
                        tvProjects_NodeMouseClick(tvProjects, e);
                    }
                    break;
                case "Multistate Value":
                    if (systemConfigNode != null)
                    {
                        targetNode = systemConfigNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "Ethernet")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "BACNET IP")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == formName);
                    }
                    if (targetNode != null)
                    {
                        tvProjects.SelectedNode = targetNode;
                        var e = new TreeNodeMouseClickEventArgs(targetNode, MouseButtons.Left, 1, 0, 0);
                        tvProjects_NodeMouseClick(tvProjects, e);
                    }
                    break;
                case "Analog Value":
                    if (systemConfigNode != null)
                    {
                        targetNode = systemConfigNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "Ethernet")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "BACNET IP")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == formName);
                    }
                    if (targetNode != null)
                    {
                        tvProjects.SelectedNode = targetNode;
                        var e = new TreeNodeMouseClickEventArgs(targetNode, MouseButtons.Left, 1, 0, 0);
                        tvProjects_NodeMouseClick(tvProjects, e);
                    }
                    break;
                case "Schedule":
                    if (systemConfigNode != null)
                    {
                        targetNode = systemConfigNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "Ethernet")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "BACNET IP")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == formName);
                    }
                    if (targetNode != null)
                    {
                        tvProjects.SelectedNode = targetNode;
                        var e = new TreeNodeMouseClickEventArgs(targetNode, MouseButtons.Left, 1, 0, 0);
                        tvProjects_NodeMouseClick(tvProjects, e);
                    }
                    break;
                case "Main":
                    {
                        targetNode = tvProjects.Nodes[0].Nodes[0].Nodes.Cast<TreeNode>()
                        .FirstOrDefault(n => n.Text == "Main");
                        tvProjects.SelectedNode = targetNode;
                        var e = new TreeNodeMouseClickEventArgs(targetNode, MouseButtons.Left, 1, 0, 0);
                        tvProjects_NodeMouseClick(tvProjects, e);
                    }
                    break;
                case "TagsForm":
                    {
                        targetNode = tvProjects.Nodes[0].Nodes[0].Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "Tags")?
                        .Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == "User Defined Tags");
                        tvProjects.SelectedNode = targetNode;
                        var e = new TreeNodeMouseClickEventArgs(targetNode, MouseButtons.Left, 1, 0, 0);
                        tvProjects_NodeMouseClick(tvProjects, e);
                    }
                    break;
                case null:
                    return;
            }
            if (formName.StartsWith("TagsForm@"))
            {
                targetNode = tvProjects.Nodes[0].Nodes[0].Nodes.Cast<TreeNode>()
                        .FirstOrDefault(n => n.Text == "Main").Nodes.Cast<TreeNode>().FirstOrDefault(t => t.Text == "UDFB")
                        .Nodes.Cast<TreeNode>().FirstOrDefault(t => t.Text.Equals(formName.Split('@')[1].Trim()))
                        .Nodes.Cast<TreeNode>().FirstOrDefault(t => t.Text.Equals(formName.Split('@')[1].Trim() + " Tags"));
                if (targetNode != null)
                {
                    tvProjects.SelectedNode = targetNode;
                    var e = new TreeNodeMouseClickEventArgs(targetNode, MouseButtons.Left, 1, 0, 0);
                    tvProjects_NodeMouseClick(tvProjects, e);
                }
            }
            erroredRungNo = 0;
            xm.LoadedProject.NewFocusIndex = 0;
            Compile();
        }
        public void OpenLogicBlockScreen(string screenName)
        {
            xm = XMPS.Instance;
            ActivateForm(screenName);
        }
        private void tblLeftPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isResizing)
            {
                tblLeftPanel.Cursor = IsNearBorder(e.Location) ? Cursors.VSplit : Cursors.Default;
                return;
            }

            if (e.Button == MouseButtons.Left && xm.LoadedProject != null)
            {
                int deltaX = e.Location.X - lastMouseLocation.X;
                newCalculatedWidth = tblLeftPanel.Width + deltaX;

                if (newCalculatedWidth >= tblLeftPanel.MinimumSize.Width)
                {
                    deltaXAxis = (initialSplitterDistance += deltaX);
                }

                lastMouseLocation = e.Location;
            }
        }

        private void tblLeftPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsNearBorder(e.Location))
            {
                isResizing = true;
                lastMouseLocation = e.Location;
                tblLeftPanel.Cursor = Cursors.VSplit;

                newCalculatedWidth = tblLeftPanel.Width;

                // storing the initial SplitterDistance
                initialSplitterDistance = splcMain.SplitterDistance;
            }
        }

        private void tblLeftPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isResizing && e.Button == MouseButtons.Left && xm.LoadedProject != null)
            {
                tblLeftPanel.SuspendLayout();
                splcMain.SuspendLayout();

                if (newCalculatedWidth >= tblLeftPanel.MinimumSize.Width)
                {
                    if ((newCalculatedWidth - this.splcMain.SplitterDistance > -1 || newCalculatedWidth <= this.splcMain.SplitterDistance) && deltaXAxis < 1040)
                    {
                        //tblLeftPanel.Width = newCalculatedWidth;
                        splcMain.SplitterDistance = deltaXAxis; // Apply the final SplitterDistance
                        lastSplitterDistance = splcMain.SplitterDistance;
                        if (splcMain.Panel2.Controls[0] is SplitContainer sc1 && sc1.SplitterDistance < 150)
                        {
                            sc1.SplitterDistance = 150;
                        }

                        if (deltaXAxis < 41)
                        {
                            tblLeftPanel.Visible = false;
                            lblProjects.AutoSize = false;
                            lblProjects.Dock = DockStyle.None;
                            lblProjects.Size = new Size(20, 290);
                            lblProjects.Text = "Loaded" + "\u00A0" + "Projects";
                            lblProjects.Location = new Point(10, 3);
                            splcMain.SplitterDistance = 35;
                            lastSplitterDistance = 225;
                            btnPin.Visible = false;
                        }
                    }
                    else
                    {
                        if (deltaXAxis > 1040)
                        {
                            //this.tblLeftPanel.Width = 225;
                            this.splcMain.SplitterDistance = 1040;
                            lastSplitterDistance = splcMain.SplitterDistance;
                            isResizing = false;
                        }
                    }
                    btnPin.Location = new Point(splcMain.SplitterDistance - 40, 1);
                }
                tblLeftPanel.ResumeLayout();
                splcMain.ResumeLayout();

                isResizing = false;
                tblLeftPanel.Cursor = Cursors.Default;
            }
        }

        private bool IsNearBorder(Point location)
        {
            return location.X <= resizeBorderWidth || location.X >= tblLeftPanel.Width - resizeBorderWidth;
        }

        private void tblLeftPanel_MouseEnter(object sender, EventArgs e)
        {
            tblLeftPanel.Cursor = Cursors.Default;
            if (IsNearBorder(((Control)sender).PointToClient(Cursor.Position)))
            {
                Cursor.Current = Cursors.VSplit;
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void tblLeftPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!isResizing)
            {
                tblLeftPanel.Cursor = Cursors.Default;
            }
        }
        private void mQTTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MQTTLicense mQTTLicense = new MQTTLicense();
            mQTTLicense.Show();
        }
        private void ProjectWindow_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject != null)
            {
                ProjectWindow projectWindow = new ProjectWindow();
                projectWindow.Text = "Project Window";
                projectWindow.Show();
            }
        }
        private void memoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject != null)
            {
                _isMemory = true;
                Compile();
                ApplicationMemory applicationMemory = new ApplicationMemory();
                DialogResult dialogResult = applicationMemory.ShowDialog();
            }
        }

        private void tsmAddObject_Click(object sender, EventArgs e)
        {

            bool proceed = BacNetValidator.CheckAndPromptSaveChanges();
            if (!proceed)
            {
                return;
            }
            string selectedNode = tvProjects.SelectedNode.Text;
            if (selectedNode.Equals("Notification Class"))
            {
                TreeNode IONode = tvProjects.SelectedNode;
                NodeInfo niRemoteIONode = (NodeInfo)IONode.Tag;
                FormNotification notification = new FormNotification("");
                notification.Height += 5;
                notification.Width += 5;
                notification.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = notification.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PerformTreeNodeActions(niRemoteIONode, IONode, niRemoteIONode.Info.ToString());
                }
            }
            else if (selectedNode.Equals("Schedule") || selectedNode.Equals("Calendar"))
            {
                OpenBacNetUserPopUp(selectedNode);
            }
            else
                AddNewUserDefinedTag();
        }

        private void OpenBacNetUserPopUp(string selectedNode)
        {
            TreeNode IONode = tvProjects.SelectedNode;
            NodeInfo niRemoteIONode = (NodeInfo)IONode.Tag;

            XMProForm tempForm = new XMProForm();
            tempForm.StartPosition = FormStartPosition.CenterParent;
            tempForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            tempForm.Text = $"Add New {selectedNode} Object";
            BacNetSubTypeUserControl userControl = new BacNetSubTypeUserControl(selectedNode);
            tempForm.Height = userControl.Height + 30;
            tempForm.Width = userControl.Width;
            tempForm.Controls.Add(userControl);
            var frmTemp = this.ParentForm as frmMain;
            DialogResult result = tempForm.ShowDialog(frmTemp);
            if (result == DialogResult.OK)
            {
                PerformTreeNodeActions(niRemoteIONode, IONode, niRemoteIONode.Info.ToString());
            }
        }

        private void traceWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject != null)
                ShowOrActivateForm("TraceWindow", treeNode: null, null); ;
        }

        private void MenuEditconvertApplication_Click(object sender, EventArgs e)
        {
            if (xm.LoadedProject == null)
                return;
            string UpdatedPlcModel = "";
            string oldPlcModel = "";
            ProjectUpdate projectUpdate = new ProjectUpdate();
            projectUpdate.Text = "Convert Application";
            DialogResult dialogResult = projectUpdate.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                UpdatedPlcModel = projectUpdate.UpdateModel;
                oldPlcModel = projectUpdate.currentModel;
                var curProjectNode = tvProjects.Nodes.Find("curProject", true).FirstOrDefault();
                if (curProjectNode != null)
                {
                    foreach (TreeNode node in curProjectNode.Nodes)
                    {
                        if (node.Text == "IO Configuration")
                        {
                            if (node.FirstNode.Text.Contains("Base"))
                            {
                                node.FirstNode.Text = "Base (" + UpdatedPlcModel + ")";
                            }
                            bool isHIOFamily_old = oldPlcModel == "XM-14-DT-HIO" || oldPlcModel == "XM-14-DT-HIO-E";
                            bool isHIOFamily_new = UpdatedPlcModel == "XM-14-DT-HIO" || UpdatedPlcModel == "XM-14-DT-HIO-E";
                            bool skipHSIOChange = isHIOFamily_old && isHIOFamily_new;
                          
                            //if update xm-14-HIO to other model and vice versa then add HSIO node
                            if (!skipHSIOChange && (UpdatedPlcModel == "XM-14-DT-HIO" || UpdatedPlcModel == "XM-14-DT-HIO-E"))
                            {
                                TreeNode HSIO = new TreeNode("HSIO Configuration");
                                NodeInfo HSIOconfig = new NodeInfo();
                                HSIOconfig.NodeType = NodeType.ListNode;
                                HSIOconfig.Info = "HSIOConfig";
                                HSIO.Tag = HSIOconfig;
                                node.FirstNode.Nodes.Add(HSIO);
                                TreeNode InterrupttnLogicBlocks = new TreeNode("Interrupt Logic Blocks");
                                NodeInfo InterruptniLogicBlocks = new NodeInfo();
                                InterruptniLogicBlocks.NodeType = NodeType.BlockNode;
                                InterruptniLogicBlocks.Info = "InterruptLogicBlock";
                                InterrupttnLogicBlocks.Tag = InterruptniLogicBlocks;
                                for (int i = 1; i <= 4; i++)
                                {
                                    TreeNode interruptLogicBlock = new TreeNode($"Interrupt_Logic_Block{i:00}");
                                    NodeInfo interruptLogicBlockInfo = new NodeInfo();
                                    interruptLogicBlockInfo.NodeType = NodeType.BlockNode;
                                    interruptLogicBlockInfo.Info = "Ladder";
                                    interruptLogicBlock.Tag = interruptLogicBlockInfo;
                                    InterrupttnLogicBlocks.Nodes.Add(interruptLogicBlock);
                                    // Adding interrupt blocks to the project
                                    Block interruptBlock = new Block();
                                    interruptBlock.Name = $"Interrupt_Logic_Block{i:00}";
                                    interruptBlock.Type = "InterruptLogicBlock";
                                    xm.LoadedProject.Blocks.Add(interruptBlock);
                                }
                                // Adding Interrupt Logical Blocks node into the Main Node
                                curProjectNode.Nodes[0].Nodes.Insert(1, InterrupttnLogicBlocks);
                            }
                            if (!skipHSIOChange && (oldPlcModel == "XM-14-DT-HIO" || oldPlcModel == "XM-14-DT-HIO-E"))
                            {
                                TreeNode hsioNode = node.FirstNode.Nodes[0];
                                if (hsioNode != null && hsioNode.Text == "HSIO Configuration")
                                {
                                    node.FirstNode.Nodes.Remove(hsioNode);
                                }
                                xm.LoadedProject.Blocks.RemoveAll(T => T.Type.Equals("InterruptLogicBlock"));
                                xm.LoadedProject.MainLadderLogic.RemoveAll(T => T.StartsWith("InterruptLogicBlock"));
                                xm.LoadedProject.HsioBlock.Clear();
                                xm.LoadedProject.FirstInterruptBlock = string.Empty;
                                xm.LoadedProject.SecondInterruptBlock = string.Empty;
                                xm.LoadedProject.ThirdInterruptBlock = string.Empty;
                                xm.LoadedProject.FourthInterruptBlock = string.Empty;
                                curProjectNode.Nodes[0].Nodes.RemoveAt(1);
                            }
                            xm.MarkProjectModified(true);
                            OnShown(e);
                            break;
                        }
                    }
                }
                MessageBox.Show("Application Converted successfully from " + oldPlcModel + " to " + UpdatedPlcModel, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    xm.SaveCurrentProject();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while saving or reloading converted project:\n{ex.Message}", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            OnGridDataChanged();
        }

        private void forceUnforceMenu_Click(object sender, EventArgs e)
        {
            LadderElement element = LadderDesign.ClickedElement;
            if (element == null)
                return;
            Type typeOf = element.customDrawing.GetType();
            if ((typeOf == typeof(Contact) || typeOf == typeof(Coil)) && XMPS.Instance.PlcStatus == "LogIn")
            {
                ForceBitValue forceBitValue = new ForceBitValue(element.Attributes["LogicalAddress"].ToString());
                bool sevalue = OnlineMonitoringStatus.AddressValues.ContainsKey(element.Attributes["LogicalAddress"].ToString())
                    ? Convert.ToBoolean(Convert.ToInt16(OnlineMonitoringStatus.AddressValues[element.Attributes["LogicalAddress"].ToString()]))
                    : OnlineMonitoringStatus.AddressValues.ContainsKey(element.Attributes["caption"].ToString())
                    ? Convert.ToBoolean(Convert.ToInt16(OnlineMonitoringStatus.AddressValues[element.Attributes["caption"].ToString()])) : false;
                forceBitValue.CommonForceFunctionality(!sevalue);
            }
        }
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (XMPS.Instance.PlcStatus == "LogIn")
                return;
            if (e.Control && e.KeyCode == Keys.I && xm.CurrentScreen.Contains("LadderForm"))
            {
                string blockNamePart = xm.CurrentScreen.Split('#').LastOrDefault();

                if (string.IsNullOrEmpty(blockNamePart)) return;
                Block currentBlock = xm.LoadedProject.Blocks
                                              .FirstOrDefault(block => block.Name.Contains(blockNamePart));
                if (currentBlock == null) return;
                if (xm.LoadedScreens[xm.CurrentScreen] is LadderWindow currentBlockForm)
                {
                    // Accessing private method of LadderWindow.
                    var methodInfo = typeof(LadderWindow).GetMethod("tsbInsertRung_Click",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    methodInfo?.Invoke(currentBlockForm, new object[] { sender, e });
                }
            }
        }

        private void tvProjects_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Text == "File")
            {
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, tvProjects.Font, e.Bounds, Color.Gray);
                return;
            }
            // Check if the node is selected
            if (e.Node.IsSelected)
            {
                // Set the background color for the selected node
                e.Graphics.FillRectangle(Brushes.DodgerBlue, e.Bounds);
                // Set the text color for the selected node
                TextRenderer.DrawText(e.Graphics, e.Node.Text, tvProjects.Font, e.Bounds, Color.White);
            }
            else
            {
                // Set the default background color for non-selected nodes
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                // Set the default text color for non-selected nodes
                TextRenderer.DrawText(e.Graphics, e.Node.Text, tvProjects.Font, e.Bounds, tvProjects.ForeColor);
            }

            // Draw the focus rectangle if the node has focus
            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
            }

        }

        private void tsmAddResiTable_Click(object sender, EventArgs e)
        {
            TreeNode IONode = tvProjects.SelectedNode;
            if (IONode == null)
            {
                MessageBox.Show("Please select a project node first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (IONode.Nodes.Count >= 5)
            {
                MessageBox.Show("Maximum 5 Resistance Tables allowed per project.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string tableName = "";
            string allowedPattern = @"^[_a-zA-Z][_a-zA-Z0-9]*$";

            while (true)
            {
                tableName = Interaction.InputBox(
                    "Enter the name of the resistance table:",
                    "Resistance Table Entry",
                    "",
                    -1,
                    -1
                ).Trim();
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(tableName, allowedPattern))
                {
                    MessageBox.Show("Table name must start with a letter or underscore and contain only letters, numbers, and underscores (no spaces or special characters).",
                                    "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }
                break;
            }
            if (xm.LoadedProject.ResistanceTables.Any(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("A Resistance Table with this name already exists.", "Duplicate Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NodeInfo newNodeInfo = new NodeInfo
            {
                NodeType = NodeType.ListNode,
                Info = tableName
            };
            TreeNode newNode = new TreeNode(tableName) { Tag = newNodeInfo };
            IONode.Nodes.Add(newNode);
            var newTable = new RESISTANCETable
            {
                Name = tableName
            };
            xm.LoadedProject.ResistanceTables.Add(newTable);
            RESISTANCETable.TableNames.Add(tableName);
            if (xm.LoadedProject.ResistanceValues == null)
                xm.LoadedProject.ResistanceValues = new List<RESISTANCETable_Values>();
            xm.LoadedProject.ResistanceValues.Add(new RESISTANCETable_Values
            {
                Name = tableName,
                Resistance = 1,
                output = 0

            });
            xm.LoadedProject.ResistanceValues.Add(new RESISTANCETable_Values
            {
                Name = tableName,
                Resistance = 100,
                output = 100
            });

            int resistanceIndex = ModeUI.List.FindIndex(m => m.Text == "Resistance");
            if (resistanceIndex >= 0)
            {
                int insertIndex = resistanceIndex + 1;
                int existingTablesCount = ModeUI.List.Count(m => int.TryParse(m.ID, out int id) && id >= 100);
                insertIndex += existingTablesCount;
                ModeUI.List.Insert(insertIndex, new ModeUI
                {
                    ID = (100 + existingTablesCount).ToString(),
                    Text = tableName
                });
            }
            xm.MarkProjectModified(true);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ImportLibraryForm importForm = new ImportLibraryForm())
            {
                importForm.StartPosition = FormStartPosition.CenterParent;
                importForm.ShowDialog(this); // Modal dialog with parent
            }
        }

    }

    public class StatusCircleMenuItem : ToolStripControlHost
    {
        private CircleControl _circleControl;

        public bool Status
        {
            get { return _circleControl.Status; }
            set { _circleControl.Status = value; }
        }
        public bool IsBlinking
        {
            get { return _circleControl.IsBlinking; }
            set { _circleControl.IsBlinking = value; }
        }
        public StatusCircleMenuItem() : base(new CircleControl())
        {
            _circleControl = Control as CircleControl;
        }

        // Inner control that does the actual drawing
        private class CircleControl : Control
        {
            private bool _status;
            private bool _isBlinking;
            private bool _isVisible = true;
            private int _frameCount = 0;
            private Thread _blinkThread;
            private bool _threadRunning;
            public bool Status
            {
                get { return _status; }
                set
                {
                    _status = value;
                    Invalidate();
                }
            }

            public bool IsBlinking
            {
                get { return _isBlinking; }
                set
                {
                    if (_isBlinking != value)
                    {
                        _isBlinking = value;

                        if (_isBlinking)
                        {
                            StartBlinking();
                        }
                        else
                        {
                            StopBlinking();
                            _isVisible = true;
                            SafeInvalidate();
                        }
                    }
                }
            }

            private void Application_Idle(object sender, EventArgs e)
            {
                // This will be called when the application is idle
                // We use it to trigger repaints for our animation
                if (_isBlinking)
                {
                    _frameCount++;
                    if (_frameCount >= 30) // Adjust speed by changing this number
                    {
                        _frameCount = 0;
                        _isVisible = !_isVisible;
                        Invalidate();
                    }
                }
            }



            private void StartBlinking()
            {
                if (_blinkThread != null && _blinkThread.IsAlive)
                {
                    return; // Thread already running
                }

                _threadRunning = true;
                _blinkThread = new Thread(BlinkThreadMethod)
                {
                    IsBackground = true
                };
                _blinkThread.Start();
            }

            private void StopBlinking()
            {
                _threadRunning = false;
                // Thread will terminate itself
            }

            private void BlinkThreadMethod()
            {
                while (_threadRunning)
                {
                    _isVisible = !_isVisible;
                    SafeInvalidate();

                    // Sleep for 500ms (half second) between blinks
                    Thread.Sleep(500);
                }
            }
            private void SafeInvalidate()
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(new Action(() => this.Invalidate()));
                    }
                    catch (ObjectDisposedException)
                    {
                        // Control might be disposed during shutdown
                    }
                }
            }
            public CircleControl()
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.ResizeRedraw, true);

                this.Size = new Size(16, 16); // Smaller size for menu
                this.BackColor = Color.Transparent;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Draw the circle outline
                using (Pen pen = new Pen(Color.Black, 1))
                {
                    g.DrawEllipse(pen, 0, 8, Width - 2, Height - 14);
                }
                // If blinking and in a non-visible state, don't fill
                if (!_isBlinking || _isVisible)
                {
                    // Fill the circle based on status
                    using (SolidBrush brush = new SolidBrush(_status ? Color.Green : Color.Red))
                    {
                        g.FillEllipse(brush, 0, 8, Width - 2, Height - 15);
                    }
                }
                //// Fill the circle based on status
                //using (SolidBrush brush = new SolidBrush(_status ? Color.Green : Color.Red))
                //{
                //    //Task.Delay(4000).Wait();
                //    g.FillEllipse(brush, 0, 5, Width - 2, Height - 12);

                //}
            }
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    // Clean up event subscription
                    Application.Idle -= Application_Idle;
                }
                base.Dispose(disposing);
            }
        }
    }


    public static class WindowsInputBlocker
    {
        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        public static extern bool BlockInput(bool fBlockIt);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        private static IntPtr[] blockedWindows;


        // Block ALL input system-wide (use carefully!)
        public static void BlockAllInput()
        {
            BlockInput(true);
        }

        public static void UnblockAllInput()
        {
            BlockInput(false);
        }

        // Block all application windows
        public static void BlockAllAppWindows()
        {
            var formsList = new List<Form>();
            foreach (Form form in Application.OpenForms)
            {
                formsList.Add(form);
            }

            blockedWindows = new IntPtr[formsList.Count];
            for (int i = 0; i < formsList.Count; i++)
            {
                blockedWindows[i] = formsList[i].Handle;
                EnableWindow(formsList[i].Handle, false);
            }
        }

        public static void UnblockAllAppWindows()
        {
            if (blockedWindows != null)
            {
                foreach (IntPtr handle in blockedWindows)
                {
                    EnableWindow(handle, true);
                }
                blockedWindows = null;
            }
        }
    }

}
