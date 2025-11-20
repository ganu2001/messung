
using System;
using System.Windows.Forms;

namespace XMPS2000
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MenuProject = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuProjectNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuProjectOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuProjectSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuProjectSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuProjectExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEditFindNReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.crossRefrenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEditconvertApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuView = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewDInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewCompErr = new System.Windows.Forms.ToolStripMenuItem();
            this.parallelWatchWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traceWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModePLCMode = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModeLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModeLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModeDnldProject = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModeUpldProject = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModeDnldSourceCode = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModeUpldSourceCode = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModeOfflineSim = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModePLCStart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModePLCStop = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModeCompile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModePLCResetwarm = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModePLCResetCold = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModePLCResetOrigin = new System.Windows.Forms.ToolStripMenuItem();
            this.rTCSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuModeUpdateFirmware = new System.Windows.Forms.ToolStripMenuItem();
            this.forceUnforceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mQTTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelpIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelpContents = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelpSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelpUsrManual = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tspMain = new System.Windows.Forms.ToolStrip();
            this.strpBtnNewProject = new System.Windows.Forms.ToolStripButton();
            this.strpBtnOpenProject = new System.Windows.Forms.ToolStripButton();
            this.strpBtnSaveProject = new System.Windows.Forms.ToolStripButton();
            this.strpBtnCloseProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.strpBtnDownloadProject = new System.Windows.Forms.ToolStripButton();
            this.strpBtnCompile = new System.Windows.Forms.ToolStripButton();
            this.strpBtnLogin = new System.Windows.Forms.ToolStripButton();
            this.strpBtnLogout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.strpBtnUploadProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.strpBtnCut = new System.Windows.Forms.ToolStripButton();
            this.strpBtnCopy = new System.Windows.Forms.ToolStripButton();
            this.strpBtnPaste = new System.Windows.Forms.ToolStripButton();
            this.strpBtnSelect = new System.Windows.Forms.ToolStripButton();
            this.strpBtnUndo = new System.Windows.Forms.ToolStripButton();
            this.strpBtnRedo = new System.Windows.Forms.ToolStripButton();
            this.strpBtnDelete = new System.Windows.Forms.ToolStripButton();
            this.strpBtnPrvScreen = new System.Windows.Forms.ToolStripButton();
            this.strpBtnNxtScreen = new System.Windows.Forms.ToolStripButton();
            this.strpBtnFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.strpBtnHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.MQTTScreenName = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.CurrentNodeName = new System.Windows.Forms.ToolStripLabel();
            this.strpBtnOnlineMonitor = new System.Windows.Forms.ToolStripButton();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.tssStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splcMain = new System.Windows.Forms.SplitContainer();
            this.tblLeftPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tvProjects = new System.Windows.Forms.TreeView();
            this.imgListTreeview = new System.Windows.Forms.ImageList(this.components);
            this.btnPin = new System.Windows.Forms.Button();
            this.lblProjects = new System.Windows.Forms.Label();
            this.splcInner = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxError = new System.Windows.Forms.GroupBox();
            this.buttonErrorClose = new System.Windows.Forms.Button();
            this.textBoxError = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tvBlocks = new System.Windows.Forms.TreeView();
            this.strpBtnZoomPercent = new System.Windows.Forms.ToolStripButton();
            this.imgListToolbar = new System.Windows.Forms.ImageList(this.components);
            this.ctmMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAddBlock = new System.Windows.Forms.ToolStripMenuItem();


            this.tsmRequestAddReq = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddSlave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddRemoteIO = new System.Windows.Forms.ToolStripComboBox();
            this.tsmAddExpansionIO = new System.Windows.Forms.ToolStripComboBox();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRenameBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddTag = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddUDFB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditUDFB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteUDFB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.addPublishBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CntxaddSusBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.CntxAddMQTTForm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExportTags = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImportTags = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExportLogicBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImportLogicBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddObject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddResiTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteKey = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.OnlineMonitorTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.tsmAddResiValues = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditResistanceTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteResistanceTable = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.tspMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcMain)).BeginInit();
            this.splcMain.Panel1.SuspendLayout();
            this.splcMain.Panel2.SuspendLayout();
            this.splcMain.SuspendLayout();
            this.tblLeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcInner)).BeginInit();
            this.splcInner.Panel1.SuspendLayout();
            this.splcInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxError.SuspendLayout();
            this.ctmMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuProject,
            this.MenuEdit,
            this.MenuView,
            this.MenuModePLCMode,
            this.toolsToolStripMenuItem,
            this.MenuHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainMenu.Size = new System.Drawing.Size(908, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "MainMenu";
            // 
            // MenuProject
            // 
            this.MenuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuProjectNew,
            this.MenuProjectOpen,
            this.toolStripMenuItem1,
            this.MenuProjectSave,
            this.MenuProjectSaveAs,
            this.toolStripMenuItem2,
            this.MenuProjectExit});
            this.MenuProject.Name = "MenuProject";
            this.MenuProject.Size = new System.Drawing.Size(56, 20);
            this.MenuProject.Text = "Project";
            // 
            // MenuProjectNew
            // 
            this.MenuProjectNew.Image = ((System.Drawing.Image)(resources.GetObject("MenuProjectNew.Image")));
            this.MenuProjectNew.Name = "MenuProjectNew";
            this.MenuProjectNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MenuProjectNew.Size = new System.Drawing.Size(186, 22);
            this.MenuProjectNew.Text = "New Project";
            this.MenuProjectNew.Click += new System.EventHandler(this.MenuProjectNew_Click);
            // 
            // MenuProjectOpen
            // 
            this.MenuProjectOpen.Image = ((System.Drawing.Image)(resources.GetObject("MenuProjectOpen.Image")));
            this.MenuProjectOpen.Name = "MenuProjectOpen";
            this.MenuProjectOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuProjectOpen.Size = new System.Drawing.Size(186, 22);
            this.MenuProjectOpen.Text = "Open Project";
            this.MenuProjectOpen.Click += new System.EventHandler(this.MenuProjectOpen_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 6);
            // 
            // MenuProjectSave
            // 
            this.MenuProjectSave.Image = ((System.Drawing.Image)(resources.GetObject("MenuProjectSave.Image")));
            this.MenuProjectSave.Name = "MenuProjectSave";
            this.MenuProjectSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuProjectSave.Size = new System.Drawing.Size(186, 22);
            this.MenuProjectSave.Text = "Save Project";
            this.MenuProjectSave.Click += new System.EventHandler(this.MenuProjectSave_Click);
            // 
            // MenuProjectSaveAs
            // 
            this.MenuProjectSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("MenuProjectSaveAs.Image")));
            this.MenuProjectSaveAs.Name = "MenuProjectSaveAs";
            this.MenuProjectSaveAs.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.MenuProjectSaveAs.Size = new System.Drawing.Size(186, 22);
            this.MenuProjectSaveAs.Text = "Save Project As";
            this.MenuProjectSaveAs.Click += new System.EventHandler(this.MenuProjectSaveAs_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(183, 6);
            // 
            // MenuProjectExit
            // 
            this.MenuProjectExit.Image = ((System.Drawing.Image)(resources.GetObject("MenuProjectExit.Image")));
            this.MenuProjectExit.Name = "MenuProjectExit";
            this.MenuProjectExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.MenuProjectExit.Size = new System.Drawing.Size(186, 22);
            this.MenuProjectExit.Text = "Exit";
            this.MenuProjectExit.Click += new System.EventHandler(this.MenuProjectExit_Click);
            // 
            // MenuEdit
            // 
            this.MenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuEditUndo,
            this.MenuEditRedo,
            this.MenuEditCut,
            this.MenuEditCopy,
            this.MenuEditPaste,
            this.MenuEditDelete,
            this.MenuEditFindNReplace,
            this.crossRefrenceToolStripMenuItem,
            this.MenuEditconvertApplication});
            this.MenuEdit.Name = "MenuEdit";
            this.MenuEdit.Size = new System.Drawing.Size(39, 20);
            this.MenuEdit.Text = "Edit";
            // 
            // MenuEditUndo
            // 
            this.MenuEditUndo.Name = "MenuEditUndo";
            this.MenuEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MenuEditUndo.Size = new System.Drawing.Size(206, 22);
            this.MenuEditUndo.Text = "Undo";
            this.MenuEditUndo.Click += new System.EventHandler(this.MenuEditUndo_Click);
            // 
            // MenuEditRedo
            // 
            this.MenuEditRedo.Name = "MenuEditRedo";
            this.MenuEditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.MenuEditRedo.Size = new System.Drawing.Size(206, 22);
            this.MenuEditRedo.Text = "Redo";
            this.MenuEditRedo.Click += new System.EventHandler(this.MenuEditRedo_Click);
            // 
            // MenuEditCut
            // 
            this.MenuEditCut.Name = "MenuEditCut";
            this.MenuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.MenuEditCut.Size = new System.Drawing.Size(206, 22);
            this.MenuEditCut.Text = "Cut";
            this.MenuEditCut.Click += new System.EventHandler(this.MenuEditCut_Click);
            // 
            // MenuEditCopy
            // 
            this.MenuEditCopy.Name = "MenuEditCopy";
            this.MenuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.MenuEditCopy.Size = new System.Drawing.Size(206, 22);
            this.MenuEditCopy.Text = "Copy";
            this.MenuEditCopy.Click += new System.EventHandler(this.MenuEditCopy_Click);
            // 
            // MenuEditPaste
            // 
            this.MenuEditPaste.Name = "MenuEditPaste";
            this.MenuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.MenuEditPaste.Size = new System.Drawing.Size(206, 22);
            this.MenuEditPaste.Text = "Paste";
            this.MenuEditPaste.Click += new System.EventHandler(this.MenuEditPaste_Click);
            // 
            // MenuEditDelete
            // 
            this.MenuEditDelete.Name = "MenuEditDelete";
            this.MenuEditDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.MenuEditDelete.Size = new System.Drawing.Size(206, 22);
            this.MenuEditDelete.Text = "Delete";
            this.MenuEditDelete.Click += new System.EventHandler(this.MenuEditDelete_Click);
            // 
            // MenuEditFindNReplace
            // 
            this.MenuEditFindNReplace.Name = "MenuEditFindNReplace";
            this.MenuEditFindNReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.MenuEditFindNReplace.Size = new System.Drawing.Size(206, 22);
            this.MenuEditFindNReplace.Text = "Find And Replace";
            this.MenuEditFindNReplace.Click += new System.EventHandler(this.MenuEditFindNReplace_Click);
            // 
            // crossRefrenceToolStripMenuItem
            // 
            this.crossRefrenceToolStripMenuItem.Name = "crossRefrenceToolStripMenuItem";
            this.crossRefrenceToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.crossRefrenceToolStripMenuItem.Text = "Cross Reference";
            this.crossRefrenceToolStripMenuItem.Click += new System.EventHandler(this.crossRefrenceToolStripMenuItem_Click);
            // 
            // MenuEditconvertApplication
            // 
            this.MenuEditconvertApplication.Name = "MenuEditconvertApplication";
            this.MenuEditconvertApplication.Size = new System.Drawing.Size(206, 22);
            this.MenuEditconvertApplication.Text = "Convert Application";
            this.MenuEditconvertApplication.Click += new System.EventHandler(this.MenuEditconvertApplication_Click);
            // 
            // MenuView
            // 
            this.MenuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuViewDInfo,
            this.MenuViewProject,
            this.MenuViewCompErr,
            this.parallelWatchWindowToolStripMenuItem,
            this.easyScanToolStripMenuItem,
            this.traceWindowToolStripMenuItem});
            this.MenuView.Name = "MenuView";
            this.MenuView.Size = new System.Drawing.Size(44, 20);
            this.MenuView.Text = "View";
            // 
            // MenuViewDInfo
            // 
            this.MenuViewDInfo.Name = "MenuViewDInfo";
            this.MenuViewDInfo.Size = new System.Drawing.Size(196, 22);
            this.MenuViewDInfo.Text = "Device Info";
            this.MenuViewDInfo.Click += new System.EventHandler(this.MenuViewDInfo_Click);
            // 
            // MenuViewProject
            // 
            this.MenuViewProject.Name = "MenuViewProject";
            this.MenuViewProject.Size = new System.Drawing.Size(196, 22);
            this.MenuViewProject.Text = "Project Window";
            this.MenuViewProject.Click += new System.EventHandler(this.ProjectWindow_Click);
            // 
            // MenuViewCompErr
            // 
            this.MenuViewCompErr.Name = "MenuViewCompErr";
            this.MenuViewCompErr.Size = new System.Drawing.Size(196, 22);
            this.MenuViewCompErr.Text = "Compile Error Screen";
            this.MenuViewCompErr.Click += new System.EventHandler(this.MenuViewCompErr_Click);
            // 
            // parallelWatchWindowToolStripMenuItem
            // 
            this.parallelWatchWindowToolStripMenuItem.Name = "parallelWatchWindowToolStripMenuItem";
            this.parallelWatchWindowToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.parallelWatchWindowToolStripMenuItem.Text = "Parallel Watch Window";
            this.parallelWatchWindowToolStripMenuItem.Click += new System.EventHandler(this.parallelWatchWindowToolStripMenuItem_Click_1);
            // 
            // easyScanToolStripMenuItem
            // 
            this.easyScanToolStripMenuItem.Name = "easyScanToolStripMenuItem";
            this.easyScanToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.easyScanToolStripMenuItem.Text = "Easy Connection";
            this.easyScanToolStripMenuItem.Click += new System.EventHandler(this.easyScan_Click);
            // 
            // traceWindowToolStripMenuItem
            // 
            this.traceWindowToolStripMenuItem.Name = "traceWindowToolStripMenuItem";
            this.traceWindowToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.traceWindowToolStripMenuItem.Text = "Trace Window";
            this.traceWindowToolStripMenuItem.Click += new System.EventHandler(this.traceWindowToolStripMenuItem_Click);
            // 
            // MenuModePLCMode
            // 
            this.MenuModePLCMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuModeLogin,
            this.MenuModeLogout,
            this.MenuModeDnldProject,
            this.MenuModeUpldProject,
            this.MenuModeDnldSourceCode,
            this.MenuModeUpldSourceCode,
            this.MenuModeOfflineSim,
            this.MenuModePLCStart,
            this.MenuModePLCStop,
            this.MenuModeCompile,
            this.MenuModePLCResetwarm,
            this.MenuModePLCResetCold,
            this.MenuModePLCResetOrigin,
            this.rTCSettingToolStripMenuItem,
            this.MenuModeUpdateFirmware,
            this.forceUnforceMenu});
            this.MenuModePLCMode.Name = "MenuModePLCMode";
            this.MenuModePLCMode.Size = new System.Drawing.Size(50, 20);
            this.MenuModePLCMode.Text = "Mode";
            // 
            // MenuModeLogin
            // 
            this.MenuModeLogin.Name = "MenuModeLogin";
            this.MenuModeLogin.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.MenuModeLogin.Size = new System.Drawing.Size(244, 22);
            this.MenuModeLogin.Text = "Login";
            this.MenuModeLogin.Click += new System.EventHandler(this.MenuModeLogin_Click);
            // 
            // MenuModeLogout
            // 
            this.MenuModeLogout.Name = "MenuModeLogout";
            this.MenuModeLogout.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.MenuModeLogout.Size = new System.Drawing.Size(244, 22);
            this.MenuModeLogout.Text = "Logout";
            this.MenuModeLogout.Click += new System.EventHandler(this.MenuModeLogout_Click);
            // 
            // MenuModeDnldProject
            // 
            this.MenuModeDnldProject.Name = "MenuModeDnldProject";
            this.MenuModeDnldProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F11)));
            this.MenuModeDnldProject.Size = new System.Drawing.Size(244, 22);
            this.MenuModeDnldProject.Text = "Download Application";
            this.MenuModeDnldProject.Click += new System.EventHandler(this.MenuModeDnldProject_Click);
            // 
            // MenuModeUpldProject
            // 
            this.MenuModeUpldProject.Name = "MenuModeUpldProject";
            this.MenuModeUpldProject.Size = new System.Drawing.Size(244, 22);
            this.MenuModeUpldProject.Text = "Upload Application";
            this.MenuModeUpldProject.Visible = false;
            this.MenuModeUpldProject.Click += new System.EventHandler(this.MenuModeUpldProject_Click);
            // 
            // MenuModeDnldSourceCode
            // 
            this.MenuModeDnldSourceCode.Name = "MenuModeDnldSourceCode";
            this.MenuModeDnldSourceCode.Size = new System.Drawing.Size(244, 22);
            this.MenuModeDnldSourceCode.Text = "Download Source Code";
            this.MenuModeDnldSourceCode.Click += new System.EventHandler(this.MenuModeDnldSourceCode_Click);
            // 
            // MenuModeUpldSourceCode
            // 
            this.MenuModeUpldSourceCode.Name = "MenuModeUpldSourceCode";
            this.MenuModeUpldSourceCode.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F12)));
            this.MenuModeUpldSourceCode.Size = new System.Drawing.Size(244, 22);
            this.MenuModeUpldSourceCode.Text = "Upload Source Code";
            this.MenuModeUpldSourceCode.Click += new System.EventHandler(this.MenuModeUpldSourceCode_Click);
            // 
            // MenuModeOfflineSim
            // 
            this.MenuModeOfflineSim.Enabled = false;
            this.MenuModeOfflineSim.Name = "MenuModeOfflineSim";
            this.MenuModeOfflineSim.Size = new System.Drawing.Size(244, 22);
            this.MenuModeOfflineSim.Text = "Offline Simulation";
            // 
            // MenuModePLCStart
            // 
            this.MenuModePLCStart.Name = "MenuModePLCStart";
            this.MenuModePLCStart.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F8)));
            this.MenuModePLCStart.Size = new System.Drawing.Size(244, 22);
            this.MenuModePLCStart.Text = "Run PLC";
            this.MenuModePLCStart.Click += new System.EventHandler(this.MenuModePLCStart_Click);
            // 
            // MenuModePLCStop
            // 
            this.MenuModePLCStop.Name = "MenuModePLCStop";
            this.MenuModePLCStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9)));
            this.MenuModePLCStop.Size = new System.Drawing.Size(244, 22);
            this.MenuModePLCStop.Text = "Stop PLC";
            this.MenuModePLCStop.Click += new System.EventHandler(this.MenuModePLCStop_Click);
            // 
            // MenuModeCompile
            // 
            this.MenuModeCompile.Name = "MenuModeCompile";
            this.MenuModeCompile.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.MenuModeCompile.Size = new System.Drawing.Size(244, 22);
            this.MenuModeCompile.Text = "Compile";
            this.MenuModeCompile.Visible = false;
            this.MenuModeCompile.Click += new System.EventHandler(this.MenuModeCompile_Click);
            // 
            // MenuModePLCResetwarm
            // 
            this.MenuModePLCResetwarm.Name = "MenuModePLCResetwarm";
            this.MenuModePLCResetwarm.Size = new System.Drawing.Size(244, 22);
            this.MenuModePLCResetwarm.Text = "Reset Warm";
            this.MenuModePLCResetwarm.Click += new System.EventHandler(this.MenuModePLCResetwarm_Click);
            // 
            // MenuModePLCResetCold
            // 
            this.MenuModePLCResetCold.Name = "MenuModePLCResetCold";
            this.MenuModePLCResetCold.Size = new System.Drawing.Size(244, 22);
            this.MenuModePLCResetCold.Text = "Reset Cold";
            this.MenuModePLCResetCold.Click += new System.EventHandler(this.MenuModePLCResetCold_Click);
            // 
            // MenuModePLCResetOrigin
            // 
            this.MenuModePLCResetOrigin.Name = "MenuModePLCResetOrigin";
            this.MenuModePLCResetOrigin.Size = new System.Drawing.Size(244, 22);
            this.MenuModePLCResetOrigin.Text = "Reset Origin";
            this.MenuModePLCResetOrigin.Click += new System.EventHandler(this.MenuModePLCResetOrigin_Click);
            // 
            // rTCSettingToolStripMenuItem
            // 
            this.rTCSettingToolStripMenuItem.Name = "rTCSettingToolStripMenuItem";
            this.rTCSettingToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.rTCSettingToolStripMenuItem.Text = "RTC Setting";
            this.rTCSettingToolStripMenuItem.Click += new System.EventHandler(this.rTCSettingToolStripMenuItem_Click);
            // 
            // MenuModeUpdateFirmware
            // 
            this.MenuModeUpdateFirmware.Name = "MenuModeUpdateFirmware";
            this.MenuModeUpdateFirmware.Size = new System.Drawing.Size(244, 22);
            this.MenuModeUpdateFirmware.Text = "Update Firmware";
            this.MenuModeUpdateFirmware.Click += new System.EventHandler(this.MenuModeUpdateFirmware_Click);
            // 
            // forceUnforceMenu
            // 
            this.forceUnforceMenu.Name = "forceUnforceMenu";
            this.forceUnforceMenu.ShortcutKeyDisplayString = "";
            this.forceUnforceMenu.Size = new System.Drawing.Size(244, 22);
            this.forceUnforceMenu.Text = "Force/Unforce - Space";
            this.forceUnforceMenu.Click += new System.EventHandler(this.forceUnforceMenu_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licenseToolStripMenuItem,
            this.memoryToolStripMenuItem,
            this.importToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mQTTToolStripMenuItem});
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.licenseToolStripMenuItem.Text = "License Manager";
            this.licenseToolStripMenuItem.Visible = false;
            // 
            // mQTTToolStripMenuItem
            // 
            this.mQTTToolStripMenuItem.Name = "mQTTToolStripMenuItem";
            this.mQTTToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.mQTTToolStripMenuItem.Text = "MQTT";
            this.mQTTToolStripMenuItem.Click += new System.EventHandler(this.mQTTToolStripMenuItem_Click);
            // 
            // memoryToolStripMenuItem
            // 
            this.memoryToolStripMenuItem.Name = "memoryToolStripMenuItem";
            this.memoryToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.memoryToolStripMenuItem.Text = "Memory";
            this.memoryToolStripMenuItem.Click += new System.EventHandler(this.memoryToolStripMenuItem_Click);
            // 
            // MenuHelp
            // 
            this.MenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuHelpIndex,
            this.MenuHelpContents,
            this.MenuHelpSearch,
            this.MenuHelpUsrManual,
            this.MenuHelpAbout});
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(44, 20);
            this.MenuHelp.Text = "Help";
            // 
            // MenuHelpIndex
            // 
            this.MenuHelpIndex.Name = "MenuHelpIndex";
            this.MenuHelpIndex.Size = new System.Drawing.Size(180, 22);
            this.MenuHelpIndex.Text = "Index";
            this.MenuHelpIndex.Click += new System.EventHandler(this.MenuHelpIndex_Click);
            // 
            // MenuHelpContents
            // 
            this.MenuHelpContents.Name = "MenuHelpContents";
            this.MenuHelpContents.Size = new System.Drawing.Size(180, 22);
            this.MenuHelpContents.Text = "Contents";
            // 
            // MenuHelpSearch
            // 
            this.MenuHelpSearch.Name = "MenuHelpSearch";
            this.MenuHelpSearch.Size = new System.Drawing.Size(180, 22);
            this.MenuHelpSearch.Text = "Search";
            // 
            // MenuHelpUsrManual
            // 
            this.MenuHelpUsrManual.Name = "MenuHelpUsrManual";
            this.MenuHelpUsrManual.Size = new System.Drawing.Size(180, 22);
            this.MenuHelpUsrManual.Text = "User Manual";
            this.MenuHelpUsrManual.Click += new System.EventHandler(this.MenuHelpUsrManual_Click);
            // 
            // MenuHelpAbout
            // 
            this.MenuHelpAbout.Name = "MenuHelpAbout";
            this.MenuHelpAbout.Size = new System.Drawing.Size(180, 22);
            this.MenuHelpAbout.Text = "About XMPS - 2000";
            this.MenuHelpAbout.Click += new System.EventHandler(this.MenuHelpAbout_Click);
            // 
            // tspMain
            // 
            this.tspMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tspMain.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.tspMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.strpBtnNewProject,
            this.strpBtnOpenProject,
            this.strpBtnSaveProject,
            this.strpBtnCloseProject,
            this.toolStripSeparator1,
            this.strpBtnDownloadProject,
            this.strpBtnCompile,
            this.strpBtnLogin,
            this.strpBtnLogout,
            this.toolStripSeparator3,
            this.strpBtnUploadProject,
            this.toolStripSeparator2,
            this.strpBtnCut,
            this.strpBtnCopy,
            this.strpBtnPaste,
            this.strpBtnSelect,
            this.strpBtnUndo,
            this.strpBtnRedo,
            this.strpBtnDelete,
            this.strpBtnPrvScreen,
            this.strpBtnNxtScreen,
            this.strpBtnFind,
            this.toolStripSeparator4,
            this.strpBtnHelp,
            this.toolStripSeparator6,
            this.MQTTScreenName,
            this.toolStripSeparator7,
            this.toolStripRefresh,
            this.toolStripSeparator8,
            this.CurrentNodeName});
            this.tspMain.Location = new System.Drawing.Point(0, 24);
            this.tspMain.Name = "tspMain";
            this.tspMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tspMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tspMain.Size = new System.Drawing.Size(908, 32);
            this.tspMain.TabIndex = 1;
            this.tspMain.Text = "toolStrip1";
            this.tspMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tspMain_ItemClicked);
            // 
            // strpBtnNewProject
            // 
            this.strpBtnNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnNewProject.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnNewProject.Image")));
            this.strpBtnNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnNewProject.Name = "strpBtnNewProject";
            this.strpBtnNewProject.Size = new System.Drawing.Size(29, 29);
            this.strpBtnNewProject.Text = "StrpBtnNewProject";
            this.strpBtnNewProject.ToolTipText = "New Project    Ctrl+N";
            // 
            // strpBtnOpenProject
            // 
            this.strpBtnOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnOpenProject.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnOpenProject.Image")));
            this.strpBtnOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnOpenProject.Name = "strpBtnOpenProject";
            this.strpBtnOpenProject.Size = new System.Drawing.Size(29, 29);
            this.strpBtnOpenProject.Text = "strpBtnOpenProejct";
            this.strpBtnOpenProject.ToolTipText = "Open Project    Ctrl+O";
            // 
            // strpBtnSaveProject
            // 
            this.strpBtnSaveProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnSaveProject.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnSaveProject.Image")));
            this.strpBtnSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnSaveProject.Name = "strpBtnSaveProject";
            this.strpBtnSaveProject.Size = new System.Drawing.Size(29, 29);
            this.strpBtnSaveProject.Text = "strpBtnSaveProejct";
            this.strpBtnSaveProject.ToolTipText = "Save Project    Ctrl+S ";
            this.strpBtnSaveProject.Click += new System.EventHandler(this.strpBtnSaveProject_Click);
            // 
            // strpBtnCloseProject
            // 
            this.strpBtnCloseProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnCloseProject.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnCloseProject.Image")));
            this.strpBtnCloseProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnCloseProject.Name = "strpBtnCloseProject";
            this.strpBtnCloseProject.Size = new System.Drawing.Size(29, 29);
            this.strpBtnCloseProject.Text = "strpBtnCloseProejct";
            this.strpBtnCloseProject.ToolTipText = "Close Project";
            this.strpBtnCloseProject.Click += new System.EventHandler(this.strpBtnCloseProject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // strpBtnDownloadProject
            // 
            this.strpBtnDownloadProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnDownloadProject.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnDownloadProject.Image")));
            this.strpBtnDownloadProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnDownloadProject.Name = "strpBtnDownloadProject";
            this.strpBtnDownloadProject.Size = new System.Drawing.Size(29, 29);
            this.strpBtnDownloadProject.Text = "toolStripButton6";
            this.strpBtnDownloadProject.ToolTipText = "Download Project     Ctrl+F11";
            this.strpBtnDownloadProject.Click += new System.EventHandler(this.strpBtnDownloadProject_Click);
            // 
            // strpBtnCompile
            // 
            this.strpBtnCompile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnCompile.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnCompile.Image")));
            this.strpBtnCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnCompile.Name = "strpBtnCompile";
            this.strpBtnCompile.Size = new System.Drawing.Size(29, 29);
            this.strpBtnCompile.Text = "Compile";
            this.strpBtnCompile.ToolTipText = "Compile      F11";
            this.strpBtnCompile.Click += new System.EventHandler(this.strpBtnCompile_Click);
            // 
            // strpBtnLogin
            // 
            this.strpBtnLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnLogin.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnLogin.Image")));
            this.strpBtnLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnLogin.Name = "strpBtnLogin";
            this.strpBtnLogin.Size = new System.Drawing.Size(29, 29);
            this.strpBtnLogin.Text = "Login";
            this.strpBtnLogin.ToolTipText = "Login  F8";
            this.strpBtnLogin.Click += new System.EventHandler(this.strpBtnLogin_Click);
            // 
            // strpBtnLogout
            // 
            this.strpBtnLogout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnLogout.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnLogout.Image")));
            this.strpBtnLogout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnLogout.Name = "strpBtnLogout";
            this.strpBtnLogout.Size = new System.Drawing.Size(29, 29);
            this.strpBtnLogout.Text = "Logout";
            this.strpBtnLogout.ToolTipText = "Logout  F9";
            this.strpBtnLogout.Click += new System.EventHandler(this.strpBtnLogout_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // strpBtnUploadProject
            // 
            this.strpBtnUploadProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnUploadProject.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnUploadProject.Image")));
            this.strpBtnUploadProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnUploadProject.Name = "strpBtnUploadProject";
            this.strpBtnUploadProject.Size = new System.Drawing.Size(29, 29);
            this.strpBtnUploadProject.Text = "strpBtnUploadProject";
            this.strpBtnUploadProject.ToolTipText = "Upload Project";
            this.strpBtnUploadProject.Visible = false;
            this.strpBtnUploadProject.Click += new System.EventHandler(this.strpBtnUploadProject_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            this.toolStripSeparator2.Visible = false;
            // 
            // strpBtnCut
            // 
            this.strpBtnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnCut.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnCut.Image")));
            this.strpBtnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnCut.Name = "strpBtnCut";
            this.strpBtnCut.Size = new System.Drawing.Size(29, 29);
            this.strpBtnCut.Text = "Cut";
            this.strpBtnCut.ToolTipText = "Cut    Ctrl+X";
            this.strpBtnCut.Click += new System.EventHandler(this.strpBtnCut_Click);
            // 
            // strpBtnCopy
            // 
            this.strpBtnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnCopy.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnCopy.Image")));
            this.strpBtnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnCopy.Name = "strpBtnCopy";
            this.strpBtnCopy.Size = new System.Drawing.Size(29, 29);
            this.strpBtnCopy.Text = "Copy";
            this.strpBtnCopy.ToolTipText = "Copy    Ctrl+C";
            this.strpBtnCopy.Click += new System.EventHandler(this.strpBtnCopy_Click);
            // 
            // strpBtnPaste
            // 
            this.strpBtnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnPaste.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnPaste.Image")));
            this.strpBtnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnPaste.Name = "strpBtnPaste";
            this.strpBtnPaste.Size = new System.Drawing.Size(29, 29);
            this.strpBtnPaste.Text = "Paste";
            this.strpBtnPaste.ToolTipText = "Paste    Ctrl+V";
            this.strpBtnPaste.Click += new System.EventHandler(this.strpBtnPaste_Click);
            // 
            // strpBtnSelect
            // 
            this.strpBtnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnSelect.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnSelect.Image")));
            this.strpBtnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnSelect.Name = "strpBtnSelect";
            this.strpBtnSelect.Size = new System.Drawing.Size(29, 29);
            this.strpBtnSelect.Text = "Select";
            // 
            // strpBtnUndo
            // 
            this.strpBtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnUndo.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnUndo.Image")));
            this.strpBtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnUndo.Name = "strpBtnUndo";
            this.strpBtnUndo.Size = new System.Drawing.Size(29, 29);
            this.strpBtnUndo.Text = "toolStripButton2";
            this.strpBtnUndo.ToolTipText = "Undo    Ctrl+Z";
            this.strpBtnUndo.Click += new System.EventHandler(this.strpBtnUndo_Click);
            // 
            // strpBtnRedo
            // 
            this.strpBtnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnRedo.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnRedo.Image")));
            this.strpBtnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnRedo.Name = "strpBtnRedo";
            this.strpBtnRedo.Size = new System.Drawing.Size(29, 29);
            this.strpBtnRedo.Text = "toolStripButton3";
            this.strpBtnRedo.ToolTipText = "Redo    Ctrl+Y";
            this.strpBtnRedo.Click += new System.EventHandler(this.strpBtnRedo_Click);
            // 
            // strpBtnDelete
            // 
            this.strpBtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnDelete.Image")));
            this.strpBtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnDelete.Name = "strpBtnDelete";
            this.strpBtnDelete.Size = new System.Drawing.Size(29, 29);
            this.strpBtnDelete.Text = "toolStripButton4";
            this.strpBtnDelete.ToolTipText = "Delete     Ctrl+Q";
            this.strpBtnDelete.Click += new System.EventHandler(this.strpBtnDelete_Click);
            // 
            // strpBtnPrvScreen
            // 
            this.strpBtnPrvScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnPrvScreen.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnPrvScreen.Image")));
            this.strpBtnPrvScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnPrvScreen.Name = "strpBtnPrvScreen";
            this.strpBtnPrvScreen.Size = new System.Drawing.Size(29, 29);
            this.strpBtnPrvScreen.Text = "toolStripButton5";
            this.strpBtnPrvScreen.ToolTipText = "Previous Screen";
            this.strpBtnPrvScreen.Click += new System.EventHandler(this.strpBtnPrvScreen_Click);
            // 
            // strpBtnNxtScreen
            // 
            this.strpBtnNxtScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnNxtScreen.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnNxtScreen.Image")));
            this.strpBtnNxtScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnNxtScreen.Name = "strpBtnNxtScreen";
            this.strpBtnNxtScreen.Size = new System.Drawing.Size(29, 29);
            this.strpBtnNxtScreen.Text = "toolStripButton6";
            this.strpBtnNxtScreen.ToolTipText = "Next Screen";
            this.strpBtnNxtScreen.Click += new System.EventHandler(this.strpBtnNxtScreen_Click);
            // 
            // strpBtnFind
            // 
            this.strpBtnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnFind.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnFind.Image")));
            this.strpBtnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnFind.Name = "strpBtnFind";
            this.strpBtnFind.Size = new System.Drawing.Size(29, 29);
            this.strpBtnFind.Text = "Find";
            this.strpBtnFind.ToolTipText = "Find     Ctrl+F";
            this.strpBtnFind.Click += new System.EventHandler(this.strpBtnFind_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // strpBtnHelp
            // 
            this.strpBtnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnHelp.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnHelp.Image")));
            this.strpBtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnHelp.Name = "strpBtnHelp";
            this.strpBtnHelp.Size = new System.Drawing.Size(29, 29);
            this.strpBtnHelp.Text = "Help";
            this.strpBtnHelp.Click += new System.EventHandler(this.strpBtnHelp_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 32);
            // 
            // MQTTScreenName
            // 
            this.MQTTScreenName.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.MQTTScreenName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MQTTScreenName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MQTTScreenName.DoubleClickEnabled = true;
            this.MQTTScreenName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MQTTScreenName.ForeColor = System.Drawing.Color.Blue;
            this.MQTTScreenName.Image = ((System.Drawing.Image)(resources.GetObject("MQTTScreenName.Image")));
            this.MQTTScreenName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MQTTScreenName.Name = "MQTTScreenName";
            this.MQTTScreenName.Size = new System.Drawing.Size(23, 29);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 32);
            this.toolStripSeparator7.Visible = false;
            // 
            // toolStripRefresh
            // 
            this.toolStripRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRefresh.Name = "toolStripRefresh";
            this.toolStripRefresh.Size = new System.Drawing.Size(50, 29);
            this.toolStripRefresh.Text = "Refresh";
            this.toolStripRefresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 32);
            // 
            // CurrentNodeName
            // 
            this.CurrentNodeName.Name = "CurrentNodeName";
            this.CurrentNodeName.Size = new System.Drawing.Size(0, 29);
            // 
            // strpBtnOnlineMonitor
            // 
            this.strpBtnOnlineMonitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnOnlineMonitor.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnOnlineMonitor.Image")));
            this.strpBtnOnlineMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnOnlineMonitor.Name = "strpBtnOnlineMonitor";
            this.strpBtnOnlineMonitor.Size = new System.Drawing.Size(29, 29);
            this.strpBtnOnlineMonitor.Text = "Online Monitoring";
            this.strpBtnOnlineMonitor.Click += new System.EventHandler(this.strpBtnOnlineMonitor_Click);
            // 
            // statusMain
            // 
            this.statusMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStatusLabel});
            this.statusMain.Location = new System.Drawing.Point(0, 494);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(908, 22);
            this.statusMain.TabIndex = 6;
            this.statusMain.Text = "statusStrip1";
            // 
            // tssStatusLabel
            // 
            this.tssStatusLabel.Name = "tssStatusLabel";
            this.tssStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // splcMain
            // 
            this.splcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splcMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splcMain.IsSplitterFixed = true;
            this.splcMain.Location = new System.Drawing.Point(0, 56);
            this.splcMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splcMain.Name = "splcMain";
            // 
            // splcMain.Panel1
            // 
            this.splcMain.Panel1.Controls.Add(this.tblLeftPanel);
            this.splcMain.Panel1.Controls.Add(this.btnPin);
            this.splcMain.Panel1.Controls.Add(this.lblProjects);
            this.splcMain.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splcMain.Panel1MinSize = 20;
            // 
            // splcMain.Panel2
            // 
            this.splcMain.Panel2.Controls.Add(this.splcInner);
            this.splcMain.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splcMain.Size = new System.Drawing.Size(908, 438);
            this.splcMain.SplitterDistance = 225;
            this.splcMain.SplitterWidth = 5;
            this.splcMain.TabIndex = 7;
            // 
            // tblLeftPanel
            // 
            this.tblLeftPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLeftPanel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tblLeftPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblLeftPanel.ColumnCount = 1;
            this.tblLeftPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLeftPanel.Controls.Add(this.tvProjects, 0, 0);
            this.tblLeftPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.tblLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLeftPanel.Location = new System.Drawing.Point(0, 30);
            this.tblLeftPanel.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tblLeftPanel.Name = "tblLeftPanel";
            this.tblLeftPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tblLeftPanel.RowCount = 2;
            this.tblLeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tblLeftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tblLeftPanel.Size = new System.Drawing.Size(225, 408);
            this.tblLeftPanel.TabIndex = 1;
            this.tblLeftPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblLeftPanel_MouseDown);
            this.tblLeftPanel.MouseEnter += new System.EventHandler(this.tblLeftPanel_MouseEnter);
            this.tblLeftPanel.MouseLeave += new System.EventHandler(this.tblLeftPanel_MouseLeave);
            this.tblLeftPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tblLeftPanel_MouseMove);
            this.tblLeftPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tblLeftPanel_MouseUp);
            // 
            // tvProjects
            // 
            this.tvProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProjects.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvProjects.HideSelection = false;
            this.tvProjects.ImageIndex = 0;
            this.tvProjects.ImageList = this.imgListTreeview;
            this.tvProjects.Location = new System.Drawing.Point(3, 3);
            this.tvProjects.Margin = new System.Windows.Forms.Padding(2);
            this.tvProjects.Name = "tvProjects";
            this.tvProjects.SelectedImageIndex = 0;
            this.tvProjects.Size = new System.Drawing.Size(219, 377);
            this.tvProjects.TabIndex = 0;
            this.tvProjects.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvProjects_AfterLabelEdit);
            this.tvProjects.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvProjects_BeforeExpand);
            this.tvProjects.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvProjects_DrawNode);
            this.tvProjects.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProjects_NodeMouseClick);
            this.tvProjects.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProjects_NodeMouseDoubleClick);
            // 
            // imgListTreeview
            // 
            this.imgListTreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListTreeview.ImageStream")));
            this.imgListTreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListTreeview.Images.SetKeyName(0, "Folder");
            this.imgListTreeview.Images.SetKeyName(1, "OpenFolder");
            // 
            // btnPin
            // 
            this.btnPin.BackColor = System.Drawing.Color.GhostWhite;
            this.btnPin.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPin.Image = ((System.Drawing.Image)(resources.GetObject("btnPin.Image")));
            this.btnPin.Location = new System.Drawing.Point(176, 1);
            this.btnPin.Margin = new System.Windows.Forms.Padding(2);
            this.btnPin.Name = "btnPin";
            this.btnPin.Size = new System.Drawing.Size(38, 30);
            this.btnPin.TabIndex = 2;
            this.btnPin.UseVisualStyleBackColor = false;
            this.btnPin.Click += new System.EventHandler(this.btnPin_Click);
            // 
            // lblProjects
            // 
            this.lblProjects.BackColor = System.Drawing.Color.LightSkyBlue;
            this.lblProjects.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProjects.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjects.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblProjects.Location = new System.Drawing.Point(0, 0);
            this.lblProjects.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProjects.Name = "lblProjects";
            this.lblProjects.Size = new System.Drawing.Size(225, 30);
            this.lblProjects.TabIndex = 1;
            this.lblProjects.Text = "Loaded Projects";
            this.lblProjects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProjects.Click += new System.EventHandler(this.lblProjects_Click);
            // 
            // splcInner
            // 
            this.splcInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splcInner.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splcInner.IsSplitterFixed = true;
            this.splcInner.Location = new System.Drawing.Point(0, 0);
            this.splcInner.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splcInner.Name = "splcInner";
            // 
            // splcInner.Panel1
            // 
            this.splcInner.Panel1.Controls.Add(this.splitContainer1);
            this.splcInner.Panel1.Controls.Add(this.splitter1);
            this.splcInner.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splcInner.Panel2
            // 
            this.splcInner.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splcInner.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splcInner.Size = new System.Drawing.Size(679, 438);
            this.splcInner.SplitterDistance = 652;
            this.splcInner.SplitterWidth = 2;
            this.splcInner.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxError);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(649, 438);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(-16, 2);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(15, 19);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.Location = new System.Drawing.Point(-31, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 19);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // groupBoxError
            // 
            this.groupBoxError.Controls.Add(this.buttonErrorClose);
            this.groupBoxError.Controls.Add(this.textBoxError);
            this.groupBoxError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxError.Location = new System.Drawing.Point(0, 0);
            this.groupBoxError.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxError.Name = "groupBoxError";
            this.groupBoxError.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxError.Size = new System.Drawing.Size(649, 96);
            this.groupBoxError.TabIndex = 1;
            this.groupBoxError.TabStop = false;
            this.groupBoxError.Text = "Output";
            // 
            // buttonErrorClose
            // 
            this.buttonErrorClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonErrorClose.Location = new System.Drawing.Point(625, 2);
            this.buttonErrorClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonErrorClose.Name = "buttonErrorClose";
            this.buttonErrorClose.Size = new System.Drawing.Size(18, 28);
            this.buttonErrorClose.TabIndex = 1;
            this.buttonErrorClose.Text = "X";
            this.buttonErrorClose.UseVisualStyleBackColor = true;
            this.buttonErrorClose.Click += new System.EventHandler(this.buttonErrorClose_Click);
            // 
            // textBoxError
            // 
            this.textBoxError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxError.Location = new System.Drawing.Point(2, 15);
            this.textBoxError.Multiline = true;
            this.textBoxError.Name = "textBoxError";
            this.textBoxError.ReadOnly = true;
            this.textBoxError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxError.Size = new System.Drawing.Size(645, 79);
            this.textBoxError.TabIndex = 0;
            this.textBoxError.WordWrap = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 438);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // tvBlocks
            // 
            this.tvBlocks.AllowDrop = true;
            this.tvBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvBlocks.LineColor = System.Drawing.Color.Empty;
            this.tvBlocks.Location = new System.Drawing.Point(0, 0);
            this.tvBlocks.Name = "tvBlocks";
            this.tvBlocks.Size = new System.Drawing.Size(7, 372);
            this.tvBlocks.TabIndex = 0;
            this.tvBlocks.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvBlocks_ItemDrag);
            this.tvBlocks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvBlocks_MouseDoubleClick);
            // 
            // strpBtnZoomPercent
            // 
            this.strpBtnZoomPercent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.strpBtnZoomPercent.Image = ((System.Drawing.Image)(resources.GetObject("strpBtnZoomPercent.Image")));
            this.strpBtnZoomPercent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.strpBtnZoomPercent.Name = "strpBtnZoomPercent";
            this.strpBtnZoomPercent.Size = new System.Drawing.Size(29, 29);
            this.strpBtnZoomPercent.Text = "toolStripButton9";
            this.strpBtnZoomPercent.ToolTipText = "Zoom % Selected";
            // 
            // imgListToolbar
            // 
            this.imgListToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListToolbar.ImageStream")));
            this.imgListToolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListToolbar.Images.SetKeyName(0, "NewProject");
            this.imgListToolbar.Images.SetKeyName(1, "OpenProject");
            this.imgListToolbar.Images.SetKeyName(2, "SaveProject");
            this.imgListToolbar.Images.SetKeyName(3, "CloseProject");
            this.imgListToolbar.Images.SetKeyName(4, "Download");
            this.imgListToolbar.Images.SetKeyName(5, "Upload");
            this.imgListToolbar.Images.SetKeyName(6, "ZoomIn");
            this.imgListToolbar.Images.SetKeyName(7, "ZoomOut");
            this.imgListToolbar.Images.SetKeyName(8, "Compile");
            this.imgListToolbar.Images.SetKeyName(9, "Login");
            this.imgListToolbar.Images.SetKeyName(10, "Logout");
            this.imgListToolbar.Images.SetKeyName(11, "OnlineMonitoring");
            this.imgListToolbar.Images.SetKeyName(12, "Cut");
            this.imgListToolbar.Images.SetKeyName(13, "Copy");
            this.imgListToolbar.Images.SetKeyName(14, "Paste");
            this.imgListToolbar.Images.SetKeyName(15, "Select");
            this.imgListToolbar.Images.SetKeyName(16, "Undo");
            this.imgListToolbar.Images.SetKeyName(17, "Redo");
            this.imgListToolbar.Images.SetKeyName(18, "Delete");
            this.imgListToolbar.Images.SetKeyName(19, "Prev");
            this.imgListToolbar.Images.SetKeyName(20, "Next");
            this.imgListToolbar.Images.SetKeyName(21, "search.png");
            this.imgListToolbar.Images.SetKeyName(22, "Help");
            this.imgListToolbar.Images.SetKeyName(23, "PLCStop");
            this.imgListToolbar.Images.SetKeyName(24, "PLCStart");
            this.imgListToolbar.Images.SetKeyName(25, "Simulation");
            this.imgListToolbar.Images.SetKeyName(26, "PrintPreview");
            this.imgListToolbar.Images.SetKeyName(27, "Exit");
            this.imgListToolbar.Images.SetKeyName(28, "PageSetup");
            this.imgListToolbar.Images.SetKeyName(29, "Print");
            this.imgListToolbar.Images.SetKeyName(30, "SaveProjectAs");
            this.imgListToolbar.Images.SetKeyName(31, "Info");
            this.imgListToolbar.Images.SetKeyName(32, "ProjectView");
            this.imgListToolbar.Images.SetKeyName(33, "Error");
            // 
            // ctmMain
            // 
            this.ctmMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctmMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAddBlock,
            this.tsmAddResiTable,
            this.tsmAddResiValues,
            this.tsmEditResistanceTable,
            this.tsmDeleteResistanceTable,
            this.tsmRequestAddReq,
            this.tsmAddSlave,
            this.tsmAddRemoteIO,
            this.tsmAddExpansionIO,
            this.tsmDelete,
            this.tsmRenameBlock,
            this.tsmDeleteBlock,
            this.tsmAddTag,
            this.tsmAddUDFB,
            this.tsmEditUDFB,
            this.tsmDeleteUDFB,
            this.tsmAddDevice,
            this.addPublishBlockToolStripMenuItem,
            this.CntxaddSusBlock,
            this.CntxAddMQTTForm,
            this.tsmExportTags,
            this.tsmImportTags,
            this.tsmExportLogicBlock,
            this.tsmImportLogicBlock,
            this.tsmAddObject});
            this.ctmMain.Name = "ctmMain";
            this.ctmMain.Size = new System.Drawing.Size(185, 520);
            // 
            // tsmAddBlock
            // 
            this.tsmAddBlock.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmAddBlock.Name = "tsmAddBlock";
            this.tsmAddBlock.Size = new System.Drawing.Size(184, 22);
            this.tsmAddBlock.Text = "Add Block";
            this.tsmAddBlock.Click += new System.EventHandler(this.tsmAddBlock_Click);
            // 
            // tsmAddResiTable
            // 
            this.tsmAddResiTable.Name = "tsmAddResiTable";
            this.tsmAddResiTable.Size = new System.Drawing.Size(225, 24);
            this.tsmAddResiTable.Text = "Add Resistance Table";
            this.tsmAddResiTable.Click += new System.EventHandler(this.tsmAddResiTable_Click);
            // 
            // tsmAddResiValues
            // 
            this.tsmAddResiValues.Name = "tsmAddResiValues";
            this.tsmAddResiValues.Size = new System.Drawing.Size(225, 24);
            this.tsmAddResiValues.Text = "Add Resistance Values";
            this.tsmAddResiValues.Click += new System.EventHandler(this.tsmAddResiValues_Click);

            // 
            // tsmEditResistanceTable
            // 
            this.tsmEditResistanceTable.Name = "tsmEditResistanceTable";
            this.tsmEditResistanceTable.Size = new System.Drawing.Size(225, 24);
            this.tsmEditResistanceTable.Text = "Edit Table Name";
            this.tsmEditResistanceTable.Click += new System.EventHandler(this.tsmEditResistanceTable_Click);
            // 
            // tsmDeleteResistanceTable
            // 
            this.tsmDeleteResistanceTable.Name = "tsmDeleteResistanceTable";
            this.tsmDeleteResistanceTable.Size = new System.Drawing.Size(225, 24);
            this.tsmDeleteResistanceTable.Text = "Delete Resistance Table";
            this.tsmDeleteResistanceTable.Click += new System.EventHandler(this.tsmDeleteResistanceTable_Click);
            // 				  
            // tsmRequestAddReq
            // 
            this.tsmRequestAddReq.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmRequestAddReq.Name = "tsmRequestAddReq";
            this.tsmRequestAddReq.Size = new System.Drawing.Size(184, 22);
            this.tsmRequestAddReq.Text = "Add Request";
            this.tsmRequestAddReq.Click += new System.EventHandler(this.tsmRequestAddReq_Click);
            // 
            // tsmAddSlave
            // 
            this.tsmAddSlave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmAddSlave.Name = "tsmAddSlave";
            this.tsmAddSlave.Size = new System.Drawing.Size(184, 22);
            this.tsmAddSlave.Text = "Add Slave";
            this.tsmAddSlave.Click += new System.EventHandler(this.tsmAddSlave_Click);
            // 
            // tsmAddRemoteIO
            // 
            this.tsmAddRemoteIO.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmAddRemoteIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsmAddRemoteIO.Name = "tsmAddRemoteIO";
            this.tsmAddRemoteIO.Size = new System.Drawing.Size(121, 23);
            this.tsmAddRemoteIO.SelectedIndexChanged += new System.EventHandler(this.tsmAddRemoteIO_SelectedIndexChanged);
            // 
            // tsmAddExpansionIO
            // 
            this.tsmAddExpansionIO.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmAddExpansionIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsmAddExpansionIO.Name = "tsmAddExpansionIO";
            this.tsmAddExpansionIO.Size = new System.Drawing.Size(121, 23);
            this.tsmAddExpansionIO.SelectedIndexChanged += new System.EventHandler(this.tsmAddExpansionIO_SelectedIndexChanged);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(184, 22);
            this.tsmDelete.Text = "Delete Node";
            this.tsmDelete.Click += new System.EventHandler(this.tsmDelete_Click);
            // 
            // tsmRenameBlock
            // 
            this.tsmRenameBlock.Name = "tsmRenameBlock";
            this.tsmRenameBlock.Size = new System.Drawing.Size(184, 22);
            this.tsmRenameBlock.Text = "Rename Block";
            this.tsmRenameBlock.Click += new System.EventHandler(this.tsmRenameBlock_Click);
            // 
            // tsmDeleteBlock
            // 
            this.tsmDeleteBlock.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmDeleteBlock.Name = "tsmDeleteBlock";
            this.tsmDeleteBlock.Size = new System.Drawing.Size(184, 22);
            this.tsmDeleteBlock.Text = "Delete Block";
            this.tsmDeleteBlock.Click += new System.EventHandler(this.tsmDeleteBlock_Click);
            // 
            // tsmAddTag
            // 
            this.tsmAddTag.Name = "tsmAddTag";
            this.tsmAddTag.Size = new System.Drawing.Size(184, 22);
            this.tsmAddTag.Text = "Add Tag";
            this.tsmAddTag.Click += new System.EventHandler(this.tsmAddTag_Click);
            // 
            // tsmAddUDFB
            // 
            this.tsmAddUDFB.Name = "tsmAddUDFB";
            this.tsmAddUDFB.Size = new System.Drawing.Size(184, 22);
            this.tsmAddUDFB.Text = "Add UDFB";
            this.tsmAddUDFB.Click += new System.EventHandler(this.tsmAddUDFB_Click);
            // 
            // tsmEditUDFB
            // 
            this.tsmEditUDFB.Name = "tsmEditUDFB";
            this.tsmEditUDFB.Size = new System.Drawing.Size(184, 22);
            this.tsmEditUDFB.Text = "Edit UDFB";
            this.tsmEditUDFB.Click += new System.EventHandler(this.tsmEditUDFB_Click);
            // 
            // tsmDeleteUDFB
            // 
            this.tsmDeleteUDFB.Name = "tsmDeleteUDFB";
            this.tsmDeleteUDFB.Size = new System.Drawing.Size(184, 22);
            this.tsmDeleteUDFB.Text = "Delete UDFB";
            this.tsmDeleteUDFB.Click += new System.EventHandler(this.tsmDeleteUDFB_Click);
            // 
            // tsmAddDevice
            // 
            this.tsmAddDevice.Name = "tsmAddDevice";
            this.tsmAddDevice.Size = new System.Drawing.Size(184, 22);
            this.tsmAddDevice.Text = "Add Device";
            this.tsmAddDevice.Click += new System.EventHandler(this.tsmAddDevice_Click);
            // 
            // addPublishBlockToolStripMenuItem
            // 
            this.addPublishBlockToolStripMenuItem.Name = "addPublishBlockToolStripMenuItem";
            this.addPublishBlockToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.addPublishBlockToolStripMenuItem.Text = "Add Publish Block";
            this.addPublishBlockToolStripMenuItem.Click += new System.EventHandler(this.tsmAddPublish);
            // 
            // CntxaddSusBlock
            // 
            this.CntxaddSusBlock.Name = "CntxaddSusBlock";
            this.CntxaddSusBlock.Size = new System.Drawing.Size(184, 22);
            this.CntxaddSusBlock.Text = "Add Subscribe Block";
            this.CntxaddSusBlock.Click += new System.EventHandler(this.CntxaddSusBlock_Click);
            // 
            // CntxAddMQTTForm
            // 
            this.CntxAddMQTTForm.Name = "CntxAddMQTTForm";
            this.CntxAddMQTTForm.Size = new System.Drawing.Size(184, 22);
            this.CntxAddMQTTForm.Text = "MQTT Configuration";
            this.CntxAddMQTTForm.Click += new System.EventHandler(this.CntxAddMQTTForm_Click);
            // 
            // tsmExportTags
            // 
            this.tsmExportTags.Name = "tsmExportTags";
            this.tsmExportTags.Size = new System.Drawing.Size(184, 22);
            this.tsmExportTags.Text = "Export Tags";
            this.tsmExportTags.Click += new System.EventHandler(this.tsmExportTags_Click);
            // 
            // tsmImportTags
            // 
            this.tsmImportTags.Name = "tsmImportTags";
            this.tsmImportTags.Size = new System.Drawing.Size(184, 22);
            this.tsmImportTags.Text = "Import Tags";
            this.tsmImportTags.Click += new System.EventHandler(this.tsmImportTags_Click);
            // 
            // tsmExportLogicBlock
            // 
            this.tsmExportLogicBlock.Name = "tsmExportLogicBlock";
            this.tsmExportLogicBlock.Size = new System.Drawing.Size(184, 22);
            this.tsmExportLogicBlock.Text = "Export LogicBlock";
            this.tsmExportLogicBlock.Click += new System.EventHandler(this.tsmExportLogicBlock_Click);
            // 
            // tsmImportLogicBlock
            // 
            this.tsmImportLogicBlock.Name = "tsmImportLogicBlock";
            this.tsmImportLogicBlock.Size = new System.Drawing.Size(184, 22);
            this.tsmImportLogicBlock.Text = "Import LogicBlock";
            this.tsmImportLogicBlock.Click += new System.EventHandler(this.tsmImportLogicBlock_Click);
            // 
            // tsmAddObject
            // 
            this.tsmAddObject.Name = "tsmAddObject";
            this.tsmAddObject.Size = new System.Drawing.Size(184, 22);
            this.tsmAddObject.Text = "Add Object";
            this.tsmAddObject.Click += new System.EventHandler(this.tsmAddObject_Click);
            // tsmDeleteKey
            // 
            this.tsmDeleteKey.Name = "tsmDeleteKey";
            this.tsmDeleteKey.Size = new System.Drawing.Size(182, 22);
            this.tsmDeleteKey.Text = "Delete";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 475);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(908, 19);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // OnlineMonitorTimer
            // 
            this.OnlineMonitorTimer.Tick += new System.EventHandler(this.OnlineMonitorTimer_Tick);
            // 
            // tsmAddResiValues
            // 
            this.tsmAddResiValues.Name = "tsmAddResiValues";
            this.tsmAddResiValues.Size = new System.Drawing.Size(190, 22);
            this.tsmAddResiValues.Text = "Add Resistance Values";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importToolStripMenuItem.Text = "Import Library";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 516);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.splcMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.tspMain);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmMain";
            this.Text = "XMPS 2000";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.tspMain.ResumeLayout(false);
            this.tspMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.splcMain.Panel1.ResumeLayout(false);
            this.splcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcMain)).EndInit();
            this.splcMain.ResumeLayout(false);
            this.tblLeftPanel.ResumeLayout(false);
            this.splcInner.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcInner)).EndInit();
            this.splcInner.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxError.ResumeLayout(false);
            this.groupBoxError.PerformLayout();
            this.ctmMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuProject;
        private System.Windows.Forms.ToolStripMenuItem MenuProjectNew;
        private System.Windows.Forms.ToolStripMenuItem MenuProjectOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenuProjectSave;
        private System.Windows.Forms.ToolStripMenuItem MenuProjectSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;




        private System.Windows.Forms.ToolStripMenuItem MenuProjectExit;
        private System.Windows.Forms.ToolStripMenuItem MenuEdit;
        private System.Windows.Forms.ToolStripMenuItem MenuEditUndo;
        private System.Windows.Forms.ToolStripMenuItem MenuEditRedo;
        private System.Windows.Forms.ToolStripMenuItem MenuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem MenuEditPaste;
        private System.Windows.Forms.ToolStripMenuItem MenuEditCut;
        private System.Windows.Forms.ToolStripMenuItem MenuEditDelete;
        private System.Windows.Forms.ToolStripMenuItem MenuEditFindNReplace;
        private System.Windows.Forms.ToolStripMenuItem MenuView;
        private System.Windows.Forms.ToolStripMenuItem MenuViewDInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuViewProject;
        private System.Windows.Forms.ToolStripMenuItem MenuViewCompErr;
        private System.Windows.Forms.ToolStripMenuItem MenuModePLCMode;
        private System.Windows.Forms.ToolStripMenuItem MenuModeLogin;
        private System.Windows.Forms.ToolStripMenuItem MenuModeLogout;
        private System.Windows.Forms.ToolStripMenuItem MenuModeDnldProject;
        private System.Windows.Forms.ToolStripMenuItem MenuModeUpldProject;
        private System.Windows.Forms.ToolStripMenuItem MenuModeOfflineSim;
        private System.Windows.Forms.ToolStripMenuItem MenuModePLCStart;
        private System.Windows.Forms.ToolStripMenuItem MenuModePLCStop;
        private System.Windows.Forms.ToolStripMenuItem MenuModeCompile;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpIndex;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpContents;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpSearch;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpUsrManual;
        private System.Windows.Forms.ToolStrip tspMain;
        private System.Windows.Forms.ToolStripButton strpBtnNewProject;
        private System.Windows.Forms.ToolStripButton strpBtnOpenProject;
        private System.Windows.Forms.ToolStripButton strpBtnSaveProject;
        private System.Windows.Forms.ToolStripButton strpBtnCloseProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton strpBtnUploadProject;
        private System.Windows.Forms.ToolStripButton strpBtnDownloadProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton strpBtnZoomPercent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton strpBtnCompile;
        private System.Windows.Forms.ToolStripButton strpBtnLogin;
        private System.Windows.Forms.ToolStripButton strpBtnLogout;
        private System.Windows.Forms.ToolStripButton strpBtnOnlineMonitor;
        private System.Windows.Forms.ToolStripButton strpBtnHelp;
        private System.Windows.Forms.ToolStripButton strpBtnCut;
        private System.Windows.Forms.ToolStripButton strpBtnCopy;
        private System.Windows.Forms.ToolStripButton strpBtnPaste;
        private System.Windows.Forms.ToolStripButton strpBtnSelect;
        private System.Windows.Forms.ToolStripButton strpBtnUndo;
        private System.Windows.Forms.ToolStripButton strpBtnRedo;
        private System.Windows.Forms.ToolStripButton strpBtnDelete;
        private System.Windows.Forms.ToolStripButton strpBtnPrvScreen;
        private System.Windows.Forms.ToolStripButton strpBtnNxtScreen;
        private System.Windows.Forms.ToolStripButton strpBtnFind;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.SplitContainer splcMain;
        private System.Windows.Forms.TableLayoutPanel tblLeftPanel;
        public System.Windows.Forms.TreeView tvProjects;
        private System.Windows.Forms.SplitContainer splcInner;
        private System.Windows.Forms.ImageList imgListTreeview;
        private System.Windows.Forms.ImageList imgListToolbar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip ctmMain;
        private System.Windows.Forms.ToolStripMenuItem tsmAddBlock;
        private System.Windows.Forms.TreeView tvBlocks;
        private System.Windows.Forms.ToolStripMenuItem tsmRequestAddReq;
        private System.Windows.Forms.ToolStripMenuItem tsmAddSlave;
        private System.Windows.Forms.ToolStripComboBox tsmAddRemoteIO;
        private System.Windows.Forms.ToolStripComboBox tsmAddExpansionIO;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteBlock;
        private System.Windows.Forms.ToolStripMenuItem tsmRenameBlock;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox textBoxError;
        private System.Windows.Forms.GroupBox groupBoxError;
        private System.Windows.Forms.Button buttonErrorClose;
        private System.Windows.Forms.ToolStripStatusLabel tssStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem tsmAddTag;
        private System.Windows.Forms.ToolStripMenuItem MenuModePLCResetwarm;
        private System.Windows.Forms.ToolStripMenuItem MenuModePLCResetCold;
        private System.Windows.Forms.ToolStripMenuItem MenuModePLCResetOrigin;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer OnlineMonitorTimer;
        private System.Windows.Forms.ToolStripMenuItem rTCSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmAddDevice;
        private System.Windows.Forms.ToolStripMenuItem crossRefrenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuModeDnldSourceCode;
        private System.Windows.Forms.ToolStripMenuItem MenuModeUpldSourceCode;
        private System.Windows.Forms.ToolStripMenuItem MenuModeUpdateFirmware;
        private System.Windows.Forms.ToolStripMenuItem tsmAddUDFB;
        private System.Windows.Forms.ToolStripMenuItem tsmEditUDFB;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmAddModbus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnPin;
        private System.Windows.Forms.Label lblProjects;
        private System.Windows.Forms.ToolStripMenuItem parallelWatchWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteUDFB;
        private System.Windows.Forms.ToolStripMenuItem addPublishBlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CntxaddSusBlock;
        private System.Windows.Forms.ToolStripMenuItem CntxAddMQTTForm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton MQTTScreenName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmImportTags;
        private System.Windows.Forms.ToolStripMenuItem tsmExportTags;
        private System.Windows.Forms.ToolStripMenuItem tsmExportLogicBlock;
        private System.Windows.Forms.ToolStripMenuItem tsmImportLogicBlock;
        private ToolStripMenuItem tsmAddObject;
        private System.Windows.Forms.ToolStripMenuItem easyScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mQTTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.ToolStripMenuItem traceWindowToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator8;
        public ToolStripLabel CurrentNodeName;
        private ToolStripMenuItem MenuEditconvertApplication;
        private ToolStripMenuItem forceUnforceMenu;
        private ToolStripMenuItem tsmDeleteKey;
        private ToolStripMenuItem tsmAddResiTable;
        private ToolStripMenuItem tsmAddResiValues;
        private ToolStripMenuItem tsmEditResistanceTable;
        private ToolStripMenuItem tsmDeleteResistanceTable;
        private ToolStripMenuItem importToolStripMenuItem;
    }
}

