using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClassList
{
    [Serializable]
    public class Application
    {
        private ArrayList _appProjects;

        private string _appSignature = "Configuration Software";

        private string _appFileType = "Application.dat";

        private string _appProduct = "Prizm";

        private byte _appMajorVersion = 4;

        private byte _appMinorVersion = 0;

        private short _appBuild = 0;

        private byte _appCRC = 0;

        private bool _appAuthenticateUsers = true;

        private bool _appAuditTrail = true;

        private string _appLanguage = "";

        private string _appLastUser = "";

        private string _appPassword = "";

        private bool _appRememberMe = false;

        private int _appUserID = 0;

        private bool _appRtToCreateNewProj = false;

        private DateTime _appDateTime;

        //private ClassList.Application.LoginInfo _appLoginInfo;

        private ArrayList _appLoginList;

        private ArrayList _appUserList;

        //	protected UserManager _appUserManager;

        //	private HIOLibrary _appHIOLibrary;

        //	protected AccessLvlUserMGR _accessLvlMgr = new AccessLvlUserMGR();

        //	private bool _appDirtyFlag = false;

        //	private Process _appBatchProcess;

        //	private Process _appSimulationProcess;

        //	private ArrayList _appEv3ModelNames;

        //	private ArrayList _appEv3ModelBMPNames;

        //	private ArrayList _appRecentProjectPaths;

        //	private bool _prev_g_Support_IEC_Ladder = false;

        //	private CommonConstants.ProductData _prevProductDataInfo;

        //	private string _prev_g_ProjectPath = "";

        //	private int ImportScreenSelectedProjectID = -1;

        //	private int ImportScreenTagIDCount = 0;

        //	private int ImportScreenNodeIDCount = 0;

        //	public bool AuditTrail
        //	{
        //		get
        //		{
        //			return this._appAuditTrail;
        //		}
        //		set
        //		{
        //			this._appAuditTrail = value;
        //		}
        //	}

        //	public bool AuthenticateUser
        //	{
        //		get
        //		{
        //			return this._appAuthenticateUsers;
        //		}
        //		set
        //		{
        //			this._appAuthenticateUsers = value;
        //		}
        //	}

        //	public bool DirtyFlag
        //	{
        //		get
        //		{
        //			return this._appDirtyFlag;
        //		}
        //		set
        //		{
        //			this._appDirtyFlag = value;
        //		}
        //	}

        //	public ArrayList Ev3ModelBMPNames
        //	{
        //		get
        //		{
        //			return this._appEv3ModelBMPNames;
        //		}
        //	}

        //	public ArrayList Ev3ModelNames
        //	{
        //		get
        //		{
        //			return this._appEv3ModelNames;
        //		}
        //	}

        //	public string Language
        //	{
        //		get
        //		{
        //			return this._appLanguage;
        //		}
        //		set
        //		{
        //			this._appLanguage = value;
        //		}
        //	}

        //	public DateTime LastLoginDateTime
        //	{
        //		get
        //		{
        //			return this._appDateTime;
        //		}
        //		set
        //		{
        //			this._appDateTime = value;
        //		}
        //	}

        //	public string LastUser
        //	{
        //		get
        //		{
        //			return this._appLastUser;
        //		}
        //		set
        //		{
        //			this._appLastUser = value;
        //		}
        //	}

        //	public ClassList.Application.LoginInfo LoginInformation
        //	{
        //		get
        //		{
        //			return this._appLoginInfo;
        //		}
        //		set
        //		{
        //			this._appLoginInfo = value;
        //		}
        //	}

        //	public ArrayList LoginList
        //	{
        //		get
        //		{
        //			return this._appLoginList;
        //		}
        //		set
        //		{
        //			this._appLoginList = value;
        //		}
        //	}

        //	public string Password
        //	{
        //		get
        //		{
        //			return this._appPassword;
        //		}
        //		set
        //		{
        //			this._appPassword = value;
        //		}
        //	}

        //	public ArrayList RecentProjectPath
        //	{
        //		get
        //		{
        //			return this._appRecentProjectPaths;
        //		}
        //	}

        //	public bool RememberMe
        //	{
        //		get
        //		{
        //			return this._appRememberMe;
        //		}
        //		set
        //		{
        //			this._appRememberMe = value;
        //		}
        //	}

        //	public bool RightToCreateNewProject
        //	{
        //		get
        //		{
        //			return this._appRtToCreateNewProj;
        //		}
        //		set
        //		{
        //			this._appRtToCreateNewProj = value;
        //		}
        //	}

        //	public int UserID
        //	{
        //		get
        //		{
        //			return this._appUserID;
        //		}
        //		set
        //		{
        //			this._appUserID = value;
        //		}
        //	}

        //	public ArrayList UserList
        //	{
        //		get
        //		{
        //			return this._appUserList;
        //		}
        //		set
        //		{
        //			this._appUserList = value;
        //		}
        //	}

        public Application()
        {
            this._appProjects = new ArrayList();
            this._appLoginList = new ArrayList();
            this._appUserList = new ArrayList();
        }

        //	private void _appBatchProcess_Exited(object sender, EventArgs e)
        //	{
        //		try
        //		{
        //			string str = "Simulation.exe";
        //			this._appSimulationProcess.EnableRaisingEvents = false;
        //			this._appSimulationProcess.StartInfo.FileName = str;
        //			this._appSimulationProcess.Start();
        //		}
        //		catch (Win32Exception win32Exception1)
        //		{
        //			Win32Exception win32Exception = win32Exception1;
        //			if (win32Exception.NativeErrorCode == 2)
        //			{
        //				ExceptionLogger.Operationslog(win32Exception, CoreConstStrings.ExFileNotFound, CoreConstStrings.ExGlobalErrorHdr);
        //			}
        //		}
        //	}

        //	public void AddAccessLevel_ChangeLoginScreenPassword(int pProjectID, int pProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddAccessLevel_ChangeLoginScreenPassword(pProductID);
        //		}
        //	}

        //	public void AddAccessLevel_LoginScreen(int pProjectID, int pProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddAccessLevel_LoginScreen(pProductID);
        //		}
        //	}

        //	public void AddAccessLvlUser(int pProjectID, CommonConstants.AccessLevelUserData accessLvlUserData)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AddAccessLvlUser(accessLvlUserData);
        //		}
        //	}

        //	public void AddActionIDInUndoStack(int piProjectId, ActionID pActionID)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddActionIDInUndoStack(pActionID);
        //		}
        //	}

        //	public AddAlarmTag AddAlarm(CommonConstants.AlarmInfo pobjAlarm, int pProjectID)
        //	{
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		AddAlarmTag addAlarmTag = AddAlarmTag.None;
        //		if (projectIndex != -1)
        //		{
        //			addAlarmTag = ((Project)this._appProjects[projectIndex]).AddAlarm(pobjAlarm);
        //		}
        //		return addAlarmTag;
        //	}

        //	public void AddClearBookmark(int pProjectID, int piScreenNumber, bool blAddOrClearBookmark, bool blClearAllBookmarkFlag)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AddClearBookmark(piScreenNumber, blAddOrClearBookmark, blClearAllBookmarkFlag);
        //		}
        //	}

        //	public void AddConfigureEthernetSetting_Screen(int pProjectID, int pProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddConfigureEthernetSetting_Screen(pProductID);
        //		}
        //	}

        //	public void AddDefaultPopUpscrens(int pProjectID, int ProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddDefaultPopUpscrens(ProductID);
        //		}
        //	}

        //	public void AddDefaultTagGroup(int pProjectID, int pGrpID, string PGroupnm)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddDefaultTagGroup(pGrpID, PGroupnm);
        //		}
        //	}

        //	public void AddEndInstruction(int pProjectId, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddEndInstruction(pScreenNumber);
        //		}
        //	}

        //	public void AddFactoryScreens_AfterConversion(int pProjectID, int ProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddFactoryScreens_AfterConversion(ProductID);
        //		}
        //	}

        //	public AddNode AddG9SPNode(int pProjectID, CommonConstants.G9SPNodeInfo pG9SPNodeInfo)
        //	{
        //		AddNode addNode;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		addNode = (projectIndex == -1 ? AddNode.None : ((Project)this._appProjects[projectIndex]).AddG9SPNode(pG9SPNodeInfo));
        //		return addNode;
        //	}

        //	public void AddImportedLanguageData(int ProjectID, List<CommonConstants.LanguageInformation> objLang)
        //	{
        //		int projectIndex = this.GetProjectIndex(ProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AddImportedLanguageData(objLang);
        //		}
        //	}

        //	public void AddImportedScreen(int pProjectID, ClassList.Screen objScreenInfo, string ScreenName, ushort ScreenNumber, ArrayList ImportErrorsList, short LangCount)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddImportedScreen(objScreenInfo, ((ProjectPrizm3)this._appProjects[projectIndex]).ProjectPath, ScreenName, ScreenNumber, ImportErrorsList, LangCount);
        //		}
        //	}

        //	public void AddNew_PasswordScreen(int pProjectID, int ProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddNew_PasswordScreen(ProductID);
        //		}
        //	}

        //	public void AddNewEmail(int pProjectID, string pEmailName, int pNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddNewEmail(pEmailName, pNumber);
        //		}
        //	}

        //	public void AddNewLadderScreen(int pProjectID, CommonConstants.ScreenInfo pScreenInfo, int BlockType, int IECBlockType, string strBlockName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AddNewLadderScreen(pScreenInfo, BlockType, IECBlockType, strBlockName);
        //		}
        //	}

        //	public void AddNewProjectInRecentList(string pProjectName, string pProjectPath, string pstrProduct, bool pblCloseApplication)
        //	{
        //		DataTable dataTable;
        //		string str = "";
        //		int num = 0;
        //		int count = 0;
        //		int num1 = 0;
        //		if (!CommonConstants.dsRecentProjectsData.Tables.Contains("ProjectData"))
        //		{
        //			dataTable = new DataTable("ProjectData");
        //			dataTable.Columns.Add("Project");
        //			dataTable.Columns.Add("Path");
        //			dataTable.Columns.Add("Product");
        //			CommonConstants.dsRecentProjectsData.Tables.Add(dataTable);
        //		}
        //		count = CommonConstants.dsRecentProjectsData.Tables["ProjectData"].Rows.Count;
        //		while (num < count)
        //		{
        //			if (pProjectPath.Equals(CommonConstants.dsRecentProjectsData.Tables["ProjectData"].Rows[num].ItemArray[1].ToString()))
        //			{
        //				if (string.Compare(pProjectPath, CommonConstants.dsRecentProjectsData.Tables["ProjectData"].Rows[num].ItemArray[1].ToString(), true) == 0)
        //				{
        //					num1 = 1;
        //					break;
        //				}
        //			}
        //			num++;
        //		}
        //		if (num1 == 1)
        //		{
        //			CommonConstants.dsRecentProjectsData.Tables["ProjectData"].Rows.RemoveAt(num);
        //		}
        //		else if (CommonConstants.dsRecentProjectsData.Tables["ProjectData"].Rows.Count >= 4)
        //		{
        //			CommonConstants.dsRecentProjectsData.Tables["ProjectData"].Rows.RemoveAt(CommonConstants.dsRecentProjectsData.Tables["ProjectData"].Rows.Count - 1);
        //		}
        //		DataRow dataRow = CommonConstants.dsRecentProjectsData.Tables["ProjectData"].NewRow();
        //		dataRow["Project"] = pProjectName;
        //		dataRow["Path"] = pProjectPath;
        //		dataRow["Product"] = pstrProduct;
        //		CommonConstants.dsRecentProjectsData.Tables["ProjectData"].Rows.InsertAt(dataRow, 0);
        //		int num2 = 0;
        //		int num3 = 0;
        //		if (!CommonConstants.dsRecentProjectsData.Tables.Contains("LastCloseProjectPath"))
        //		{
        //			dataTable = new DataTable("LastCloseProjectPath");
        //			dataTable.Columns.Add("Project");
        //			dataTable.Columns.Add("Path");
        //			CommonConstants.dsRecentProjectsData.Tables.Add(dataTable);
        //		}
        //		count = CommonConstants.dsRecentProjectsData.Tables["LastCloseProjectPath"].Rows.Count;
        //		if (num3 < count)
        //		{
        //			str = CommonConstants.dsRecentProjectsData.Tables["LastCloseProjectPath"].Rows[num3].ItemArray[1].ToString();
        //			num2 = 1;
        //		}
        //		if (num2 == 1)
        //		{
        //			CommonConstants.dsRecentProjectsData.Tables["LastCloseProjectPath"].Rows.RemoveAt(num3);
        //		}
        //		dataRow = CommonConstants.dsRecentProjectsData.Tables["LastCloseProjectPath"].NewRow();
        //		dataRow["Project"] = pProjectName;
        //		dataRow["Path"] = pProjectPath;
        //		CommonConstants.dsRecentProjectsData.Tables["LastCloseProjectPath"].Rows.InsertAt(dataRow, 1);
        //	}

        //	public void AddNewScreen(int pProjectID, CommonConstants.ScreenInfo pScreenInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AddNewScreen(pScreenInfo);
        //		}
        //	}

        //	public AddNode AddNode(int pProjectID, CommonConstants.NodeInfo pNodeInfo)
        //	{
        //		AddNode addNode;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		addNode = (projectIndex == -1 ? AddNode.None : ((Project)this._appProjects[projectIndex]).AddNode(pNodeInfo));
        //		return addNode;
        //	}

        //	public void AddNode(CommonConstants.NodeInfo pNodeInfo)
        //	{
        //		((Project)this._appProjects[0]).AddNode(pNodeInfo);
        //	}

        //	public AddDefaultTagError AddPropertyGridTagData(int piProjectId, CommonConstants.PropertyGridAddTagData pPropertyGridAddTagData)
        //	{
        //		AddDefaultTagError addDefaultTagError = AddDefaultTagError.InvalidePrefix;
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			addDefaultTagError = ((ProjectPrizm3)this._appProjects[projectIndex]).AddPropertyGridTagData(pPropertyGridAddTagData);
        //		}
        //		return addDefaultTagError;
        //	}

        //	public void AddScreenSaver_PasswordScreeen(int pProjectID, int pProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddScreenSaver_PasswordScreeen(pProductID);
        //		}
        //	}

        //	public void AddTag(int pProjectID, CommonConstants.Prizm3TagStructure pTagStructure, CommonConstants.Prizm3BlockStructure pBlockStructure, bool pblUndefinedTag, int piUndefinedTag)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AddTag(pTagStructure, pBlockStructure, this.GetProductID(pProjectID), pblUndefinedTag, piUndefinedTag);
        //		}
        //	}

        //	public void AddTag(CommonConstants.Prizm3TagStructure pTagStructure, CommonConstants.Prizm3BlockStructure pBlockStructure, bool pblUndefinedTag, int piUndefinedTag)
        //	{
        //		((Project)this._appProjects[0]).AddTag(pTagStructure, pBlockStructure, this.GetProductID(((Project)this._appProjects[0]).ProjectID), pblUndefinedTag, piUndefinedTag);
        //	}

        //	public void AddTag_Download(int pProjectID, CommonConstants.Prizm3TagStructure pTagStructure, CommonConstants.Prizm3BlockStructure pBlockStructure, bool pblUndefinedTag, int piUndefinedTag)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AddTag_Download(pTagStructure, pBlockStructure, this.GetProductID(pProjectID), pblUndefinedTag, piUndefinedTag);
        //		}
        //	}

        //	public int AddTag_ScreenImport(int pProjectID, CommonConstants.Prizm3TagStructure pTagStructure, CommonConstants.Prizm3BlockStructure pBlockStructure, bool pblUndefinedTag, int piUndefinedTag)
        //	{
        //		int num = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			num = ((Project)this._appProjects[projectIndex]).AddTag_ScreenImport(pProjectID, pTagStructure, pBlockStructure, this.GetProductID(pProjectID), pblUndefinedTag, piUndefinedTag);
        //		}
        //		return num;
        //	}

        //	public void AddTagForLadder(int pProjectID, CommonConstants.Prizm3TagStructure pTagStructure, CommonConstants.Prizm3BlockStructure pBlockStructure)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AddTagForLadder(pTagStructure, pBlockStructure, this.GetProductID(pProjectID));
        //		}
        //	}

        //	public void AddTagGroup(int pProjectID, string PGroupnm)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddTagGroup(PGroupnm);
        //		}
        //	}

        //	public CommonConstants.Prizm3TagStructure AddUndefinedTags(int pProjectID, CommonConstants.Prizm3TagStructure pObjTagStruct)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			pObjTagStruct = ((Project)this._appProjects[projectIndex]).AddUndefinedTags(pObjTagStruct);
        //		}
        //		return pObjTagStruct;
        //	}

        //	public void AddUser(CommonConstants.UserData puserData)
        //	{
        //		this._appUserManager.AddUser(puserData);
        //		this._appLoginInfo.iUserID = puserData._userID;
        //		this._appLoginInfo.strUserName = puserData._userName;
        //		this._appLoginInfo.strPwd = puserData._userPassword;
        //		this._appLoginList.Add(this._appLoginInfo);
        //	}

        //	public void AlignBottom(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AlignBottom(pScreenNumber);
        //		}
        //	}

        //	public void AlignLeft(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AlignLeft(pScreenNumber);
        //		}
        //	}

        //	public void AlignMiddleHorizontally(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AlignMiddleHorizontally(pScreenNumber);
        //		}
        //	}

        //	public void AlignMiddleHorizontallywrtShape(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AlignMiddleHorizontallywrtShape(pScreenNumber);
        //		}
        //	}

        //	public void AlignMiddleVertically(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AlignMiddleVertically(pScreenNumber);
        //		}
        //	}

        //	public void AlignMiddleVerticallywrtShape(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AlignMiddleVerticallywrtShape(pScreenNumber);
        //		}
        //	}

        //	public void AlignRight(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AlignRight(pScreenNumber);
        //		}
        //	}

        //	public void AlignTop(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).AlignTop(pScreenNumber);
        //		}
        //	}

        //	public void AssignDefaultNodeName(int newModelID)
        //	{
        //		((ProjectPrizm3)this._appProjects[0]).AssignDefaultNodeName(newModelID);
        //	}

        //	public void AssignStratonDatatype(int pProjectID, string TagName, string TagDataType)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AssignStratonDatatype(TagName, TagDataType);
        //		}
        //	}

        //	public void AssignTagIDToSelShapes(int pProjectID, string pstrTagAddress, int piScreenNumber)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((ProjectPrizm3)this._appProjects[projectIndex]).GetSelectedObjShapeID(piScreenNumber);
        //			for (int i = 0; i < arrayLists.Count; i++)
        //			{
        //				if (((DrawingObjects)arrayLists[i] == DrawingObjects.SINGLEBARGRAPH ? false : (DrawingObjects)arrayLists[i] != DrawingObjects.ANALOGMETER))
        //				{
        //					((ProjectPrizm3)this._appProjects[projectIndex]).AssignTagIDToSelectedShapePressedTasks(pstrTagAddress, piScreenNumber);
        //				}
        //				else
        //				{
        //					((ProjectPrizm3)this._appProjects[projectIndex]).AssignTagIDToSelShapes(pstrTagAddress, piScreenNumber);
        //				}
        //			}
        //		}
        //	}

        //	public void Batch_Entry_Screen(int pProjectID, int pProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AddBatchEntryScreen(pProductID);
        //		}
        //	}

        //	public void BreakGroup(int pProjectID, int piScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).BreakGroup(piScreenNumber);
        //		}
        //	}

        //	public void ChangeFont(int pProjectID, Font pobjFont, byte pbtShapeFont)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).ChangeFont(pobjFont, pbtShapeFont);
        //		}
        //	}

        //	public DeleteTagMessage CheckDeleteTag(int pProjectID, CommonConstants.Prizm3TagStructure pstrucTagInformation)
        //	{
        //		DeleteTagMessage deleteTagMessage = DeleteTagMessage.None;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			deleteTagMessage = ((ProjectPrizm3)this._appProjects[projectIndex]).CheckDeleteTag(pstrucTagInformation);
        //		}
        //		return deleteTagMessage;
        //	}

        //	public DeleteTagMessage CheckDeleteTagforXYPlot(int pProjectID, CommonConstants.Prizm3TagStructure pstrucTagInformation)
        //	{
        //		DeleteTagMessage deleteTagMessage = DeleteTagMessage.None;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			deleteTagMessage = ((ProjectPrizm3)this._appProjects[projectIndex]).CheckDeleteTagforXYPlot(pstrucTagInformation);
        //		}
        //		return deleteTagMessage;
        //	}

        //	public bool CheckDuplicateG9SPEthernetSettings(int pProjectID, byte[] pG9SPSrcAddress, byte[] pG9SPDestAddress, uint pIpaddress, string pnodename, bool pAddOrEdit)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).CheckDuplicateG9SPEthernetSettings(pG9SPSrcAddress, pG9SPDestAddress, pIpaddress, pnodename, pAddOrEdit);
        //		}
        //		return flag;
        //	}

        //	public bool CheckGlobalTaskTags(int pProjectID, ArrayList objArrList)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).CheckGlobalTaskTags(objArrList);
        //		}
        //		return flag;
        //	}

        //	public void CheckObjectsForThisTag(int pProjectID, CommonConstants.Prizm3TagStructure pTagStruct)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).CheckObjectsForThisTag(pTagStruct);
        //		}
        //	}

        //	public DeleteTagMessage CheckUsedTag(int pProjectID, CommonConstants.Prizm3TagStructure pstrucTagInformation)
        //	{
        //		DeleteTagMessage deleteTagMessage = DeleteTagMessage.None;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			deleteTagMessage = ((ProjectPrizm3)this._appProjects[projectIndex]).CheckUsedTag(pstrucTagInformation);
        //		}
        //		return deleteTagMessage;
        //	}

        //	public bool CheckWhileShowingScreenTaskTags(int pProjectID, ArrayList objArrList, ArrayList objScreenList)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).CheckWhileShowingScreenTaskTags(objArrList, objScreenList);
        //		}
        //		return flag;
        //	}

        //	public void ClearBlocklistAndTagNames(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).ClearBlocklistAndTagNames();
        //		}
        //	}

        //	public ArrayList Compile(int pProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).Compile();
        //		}
        //		return arrayLists;
        //	}

        //	public bool Compile_Ladder(int pProjectID, ref ArrayList arrErrorList, ref CommonConstants.PlcModuleHeaderInfo objHeaderInfo, ref ArrayList objListIOAllocation)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).Compile_Ladder(ref arrErrorList, ref objHeaderInfo, ref objListIOAllocation));
        //		return flag;
        //	}

        //	public bool Compile_stLadder(int pProjectID, ref ArrayList arrErrorList, ref CommonConstants.PlcModuleHeaderInfo objHeaderInfo, ref ArrayList objListIOAllocation, string strFolder, ArrayList tempList)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).Compile_stLadder(ref arrErrorList, ref objHeaderInfo, ref objListIOAllocation, strFolder, tempList));
        //		return flag;
        //	}

        //	public void ConfigureGrid(int pProjectId, Size pszAlphanumericGridSize, Size pszTouchGridSize)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ConfigureGrid(pszAlphanumericGridSize, pszTouchGridSize);
        //		}
        //	}

        //	public void Conversion_DeleteUnSupportedTags(CommonConstants.ProductConversionParameters prodConvrtPreferences, ref List<int> lstConvertAppTagId, ref ArrayList listDeleteVMDB)
        //	{
        //		if (((Project)this._appProjects[0]).ProjectID != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[0]).Conversion_DeleteUnSupportedTags(prodConvrtPreferences, ref lstConvertAppTagId, ref listDeleteVMDB);
        //		}
        //	}

        //	public int ConvertApplication(CommonConstants.ProductConversionParameters prodConvrtPreferences, List<int> lstConvertAppTagId)
        //	{
        //		if (((Project)this._appProjects[0]).ProjectID != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[0]).ConvertApplication(prodConvrtPreferences, lstConvertAppTagId);
        //		}
        //		return 0;
        //	}

        //	public void ConvertColorValues(int pProjectID, int Source, int Dest)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ConvertColorValues(Source, Dest);
        //		}
        //	}

        //	public void CopyFileToUSBFolder(int piProjectID, int piProductID, ProjectInfo pProjectInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).CopyFileToUSBFolder_FlexiPanel(piProductID, pProjectInfo);
        //		}
        //	}

        //	public void CopyRungLine(int pProjectID, int pScreenNumber, int Type)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).CopyRungLine(pScreenNumber, Type);
        //		}
        //	}

        //	public void CopyScreenAcrossProjects(int pSourceProjectID, int pTargetProjectID, List<ClassList.Screen> pScreenList, List<ClassList.Screen> pExistingScreenList)
        //	{
        //		int tagID;
        //		ArrayList arrayLists = new ArrayList();
        //		List<CommonConstants.Prizm3TagStructure> prizm3TagStructures = new List<CommonConstants.Prizm3TagStructure>();
        //		CommonConstants.NodeInfo nodeInformation = new CommonConstants.NodeInfo();
        //		int projectIndex = this.GetProjectIndex(pTargetProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).CopyScreenAcrossProjects(pScreenList, pExistingScreenList);
        //		}
        //		int num = this.GetProjectIndex(pSourceProjectID);
        //		for (int i = 0; i < pScreenList.Count; i++)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetTagIDList((int)pScreenList[i].ScreenNumber);
        //			if (num != -1)
        //			{
        //				prizm3TagStructures = ((Project)this._appProjects[num]).GetTagInformation(arrayLists);
        //			}
        //			for (int j = 0; j < prizm3TagStructures.Count; j++)
        //			{
        //				int item = prizm3TagStructures[j]._NodeID;
        //				nodeInformation = ((Project)this._appProjects[num]).GetNodeInformation(item);
        //				CommonConstants.Prizm3TagStructure tagName = new CommonConstants.Prizm3TagStructure();
        //				tagName = prizm3TagStructures[j];
        //				if ((prizm3TagStructures[j]._BitNumber <= 0 ? false : !prizm3TagStructures[j]._TagAddress.Contains("_")))
        //				{
        //					tagName = prizm3TagStructures[j];
        //					tagName._TagAddress = string.Concat(prizm3TagStructures[j]._TagAddress, "_", this.GetBitNumberStringValues(Convert.ToInt32(prizm3TagStructures[j]._BitNumber)));
        //					tagName._TagName = this.GetTagName(tagName._TagAddress, pSourceProjectID);
        //				}
        //				if (((Project)this._appProjects[projectIndex]).IsNodePresent(nodeInformation._btPLCCode, nodeInformation._btPLCModel))
        //				{
        //					tagName._NodeName = nodeInformation._strName;
        //					if (!((Project)this._appProjects[projectIndex]).IsTagAddressPresent(tagName._TagAddress, tagName))
        //					{
        //						MessageBox.Show(string.Concat(tagName._TagAddress, " not present in new project. It will be added as undefined tag."));
        //						tagID = ((Project)this._appProjects[projectIndex]).AddUndefinedTags(tagName)._TagTagID;
        //					}
        //					else
        //					{
        //						MessageBox.Show(string.Concat(tagName._TagAddress, " tag address is already present in new project."));
        //						tagID = ((Project)this._appProjects[projectIndex]).GetTagID(tagName._TagAddress);
        //					}
        //					((Project)this._appProjects[projectIndex]).AssignNewTagID(tagName._TagTagID, tagID, (int)pScreenList[i].ScreenNumber);
        //				}
        //				else
        //				{
        //					MessageBox.Show("No matching PLC. Add PLC and add tag.");
        //				}
        //			}
        //		}
        //		this.CopyScreenTaskTagID(pSourceProjectID, pTargetProjectID, pScreenList, pExistingScreenList);
        //	}

        //	private void CopyScreenTaskTagID(int pSourceProjectID, int pTargetProjectID, List<ClassList.Screen> pScreenList, List<ClassList.Screen> pExistingScreenList)
        //	{
        //		int tagID1;
        //		List<int> nums = new List<int>();
        //		ArrayList arrayLists = new ArrayList();
        //		List<CommonConstants.Prizm3TagStructure> prizm3TagStructures = new List<CommonConstants.Prizm3TagStructure>();
        //		CommonConstants.NodeInfo nodeInformation = new CommonConstants.NodeInfo();
        //		int projectIndex = this.GetProjectIndex(pSourceProjectID);
        //		int num = this.GetProjectIndex(pTargetProjectID);
        //		for (int i = 0; i < pScreenList.Count; i++)
        //		{
        //			nums = ((Project)this._appProjects[num]).GetTaskIDList((int)pScreenList[i].ScreenNumber);
        //			for (int j = 0; j < nums.Count; j++)
        //			{
        //				tagID1 = TaskManager.GetTagID1(Convert.ToInt32(nums[j]));
        //				if ((tagID1 == -1 ? false : !arrayLists.Contains(tagID1)))
        //				{
        //					arrayLists.Add(tagID1);
        //				}
        //				tagID1 = TaskManager.GetTagID2(Convert.ToInt32(nums[j]));
        //				if ((tagID1 == -1 ? false : !arrayLists.Contains(tagID1)))
        //				{
        //					arrayLists.Add(tagID1);
        //				}
        //			}
        //			if (projectIndex != -1)
        //			{
        //				prizm3TagStructures = ((Project)this._appProjects[projectIndex]).GetTagInformation(arrayLists);
        //			}
        //			for (int k = 0; k < prizm3TagStructures.Count; k++)
        //			{
        //				int item = prizm3TagStructures[k]._NodeID;
        //				nodeInformation = ((Project)this._appProjects[projectIndex]).GetNodeInformation(item);
        //				CommonConstants.Prizm3TagStructure tagName = new CommonConstants.Prizm3TagStructure();
        //				tagName = prizm3TagStructures[k];
        //				if ((prizm3TagStructures[k]._BitNumber <= 0 ? false : !prizm3TagStructures[k]._TagAddress.Contains("_")))
        //				{
        //					tagName = prizm3TagStructures[k];
        //					tagName._TagAddress = string.Concat(prizm3TagStructures[k]._TagAddress, "_", this.GetBitNumberStringValues(Convert.ToInt32(prizm3TagStructures[k]._BitNumber)));
        //					tagName._TagName = this.GetTagName(tagName._TagAddress, pSourceProjectID);
        //				}
        //				if (((Project)this._appProjects[num]).IsNodePresent(nodeInformation._btPLCCode, nodeInformation._btPLCModel))
        //				{
        //					if (!((Project)this._appProjects[num]).IsTagAddressPresent(tagName._TagAddress, tagName))
        //					{
        //						MessageBox.Show(string.Concat(tagName._TagAddress, " not present in new project. It will be added as undefined tag."));
        //						tagID1 = ((Project)this._appProjects[num]).AddUndefinedTags(tagName)._TagTagID;
        //					}
        //					else
        //					{
        //						tagID1 = ((Project)this._appProjects[num]).GetTagID(tagName._TagAddress);
        //					}
        //					TaskManager.AssignTagID(tagName._TagTagID, tagID1, nums);
        //				}
        //				else
        //				{
        //					MessageBox.Show("No matching PLC. Add PLC and add tag.");
        //				}
        //			}
        //		}
        //		projectIndex = this.GetProjectIndex(pSourceProjectID);
        //	}

        //	public void CreateDefaultGloabalKeys(int pProjectID, int ProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).InitGlobalKeysList();
        //			((ProjectPrizm3)this._appProjects[projectIndex]).CreateDefaultGlobalKeyTaskForNewProject(ProductID);
        //		}
        //	}

        //	public void CreateETH_SETTING(int pProjectID, int piProductId, ProjectInfo pProjectInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).CreateETH_SETTING(piProductId, pProjectInfo);
        //		}
        //	}

        //	public void CreateFirmWare(int pProjectID, int piProductId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).CreateFirmWare(piProductId);
        //		}
        //	}

        //	public void CreateLadderFromUploadedData(int pProjectId, CommonConstants.PlcModuleHeaderInfo objHeaderInfo, ArrayList objListIOAllocation, string strLdrFileName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).CreateLadderFromUploadedData(objHeaderInfo, objListIOAllocation, strLdrFileName);
        //		}
        //	}

        //	public void CreateTouchKeysForLocalKeys(int pProjectID, int Width, int height, Graphics g)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).CreateTouchKeysForLocalKeys(Width, height, g);
        //		}
        //	}

        //	public void CreateZipFileFromUploadedProject(int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).CreateZipFileFromUploadedProject();
        //		}
        //	}

        //	public void DeleteAccessLvlUser(int pProjectID, CommonConstants.AccessLevelUserData accessLvlUserData)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DeleteAccessLvlUser(accessLvlUserData);
        //		}
        //	}

        //	public void DeleteAlarm(uint pSelAlarmId, int pProjectId)
        //	{
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DeleteAlarm(pSelAlarmId);
        //		}
        //	}

        //	public void DeleteAllNodesOnPort(int pProjectID, int port, ArrayList objTagIDList, int RegID, int CoilID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeleteAllNodesOnPort(port, objTagIDList, RegID, CoilID);
        //		}
        //	}

        //	public void DeleteEmail(int pProjectID, string pEmailName, int pNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeleteEmail(pEmailName, pNumber);
        //		}
        //	}

        //	public void DeleteNode(int pProjectID, string pstrNodeName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DeleteNode(pstrNodeName);
        //		}
        //	}

        //	public void DeleteNode(int pProjectID, string pstrNodeName, byte pbtPort)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DeleteNode(pstrNodeName, pbtPort);
        //		}
        //	}

        //	public void DeletePopUpScreens(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeletePopupScreens();
        //		}
        //	}

        //	public void DeleteRungLine(int pProjectId, int screenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeleteRungLine(screenNumber);
        //		}
        //	}

        //	public DeleteScreenMessage DeleteScreen(int pProjectID, int pScreenNumber, ref StringBuilder pListOfAssociatedScreens)
        //	{
        //		DeleteScreenMessage deleteScreenMessage = DeleteScreenMessage.None;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			deleteScreenMessage = ((Project)this._appProjects[projectIndex]).DeleteScreen(pScreenNumber, ref pListOfAssociatedScreens);
        //		}
        //		return deleteScreenMessage;
        //	}

        //	public void DeleteScreen_Usage(int pProjectID, int pScreenNumber, ref StringBuilder pListOfAssociatedScreens, ref ArrayList scrObjListVarInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeleteScreen_Usage(pScreenNumber, ref pListOfAssociatedScreens, ref scrObjListVarInfo);
        //		}
        //	}

        //	public void DeleteScreenKeys(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeleteScreenKeys();
        //		}
        //	}

        //	public void DeleteScreenNumber(int pProjectID, int piScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DeleteScreenNumber(piScreenNumber);
        //		}
        //	}

        //	public DeleteTagMessage DeleteTag(int pProjectID, string pstrTagAddress, string pstrTagName)
        //	{
        //		DeleteTagMessage deleteTagMessage = DeleteTagMessage.None;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			deleteTagMessage = ((ProjectPrizm3)this._appProjects[projectIndex]).DeleteTag(pstrTagAddress, pstrTagName);
        //		}
        //		return deleteTagMessage;
        //	}

        //	public void DeleteTag(int piProjectIndex)
        //	{
        //		((Project)this._appProjects[piProjectIndex]).DeleteTag();
        //	}

        //	public void DeleteTag_FL100(int pProjectID, int tagID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeleteTag_FL100(tagID);
        //		}
        //	}

        //	public bool DeleteTagGroup(int pProjectID, int pKey)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		bool flag = false;
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeleteTagGroup(pKey);
        //		}
        //		return flag;
        //	}

        //	public void DeleteUserData(string puserName)
        //	{
        //		this._appUserManager.DeleteUser(this._appUserManager.FindUser(puserName));
        //		int num = 0;
        //		while (num < this.LoginList.Count)
        //		{
        //			if (!(puserName == ((ClassList.Application.LoginInfo)this.LoginList[num]).strUserName))
        //			{
        //				num++;
        //			}
        //			else
        //			{
        //				this._appLoginList.RemoveAt(num);
        //				break;
        //			}
        //		}
        //	}

        //	public void DeleteWebScreenOnProductConversion(int pProjectID, int ScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeleteWebScreenOnProductConversion(ScreenNumber);
        //		}
        //	}

        //	public void DeselectAllTagCheckBox(int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DeselectAllTagCheckBox(pProjectId);
        //		}
        //	}

        //	public void DisplayCSVFormat(string Filename, int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DisplayLoggerDataCSV(Filename);
        //		}
        //	}

        //	public void DisplayCSVFormatNew()
        //	{
        //		int projectIndex = this.GetProjectIndex(CommonConstants.projectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DisplayLoggerDataCSV_New("Logger.BIN", CommonConstants.Path);
        //		}
        //	}

        //	public void DisplayHistAlarmCSVFormat(string Filename, int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DisplayHistAlarmDataCSV(Filename);
        //		}
        //	}

        //	public void DrawPreview(Graphics pG, int pDivide, int pScreenNumber)
        //	{
        //	}

        //	public void DrawShapes(Graphics pG, int pProjectID, int pScreenNumber, Form pFormObject)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DrawShapes(pG, pScreenNumber, pFormObject);
        //		}
        //	}

        //	public void DrawShapes(Graphics pG, int pProjectID, int pScreenNumber, List<uint> pPreviewAssocaitedList, Form pFormObject)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DrawShapes(pG, pScreenNumber, pPreviewAssocaitedList, pFormObject);
        //		}
        //	}

        //	public void DuplicateEmail(int pProjectID, string pEmailName, int pNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DuplicateEmail(pEmailName, pNumber);
        //		}
        //	}

        //	public void DuplicateLadderBlock(int pProjectID, string pScreenName, int pScreenNumber, int pIndex)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DuplicateLadderBlock(pScreenName, pScreenNumber, pIndex);
        //		}
        //	}

        //	public void DuplicateScreen(int pProjectID, string pScreenName, int pScreenNumber, int pIndex)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DuplicateScreen(pScreenName, pScreenNumber, pIndex);
        //		}
        //	}

        //	public void EditAccessLvlUser(int pProjectID, CommonConstants.AccessLevelUserData accessLvlUserData)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).EditAccessLvlUser(accessLvlUserData);
        //		}
        //	}

        //	public AddAlarmTag EditAlarm(CommonConstants.AlarmInfo pobjAlarm, int pProjectId, uint pSelAlarmId, int pPrevBitNo, int BtnClick)
        //	{
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		AddAlarmTag addAlarmTag = AddAlarmTag.None;
        //		if (projectIndex != -1)
        //		{
        //			addAlarmTag = ((Project)this._appProjects[projectIndex]).EditAlarm(pobjAlarm, pSelAlarmId, pPrevBitNo, BtnClick);
        //		}
        //		return addAlarmTag;
        //	}

        //	public AddNode EditNodeAddrOperatorPanelSettings(byte pCom1Address, byte pCom2Address, byte pCom3Address, int pProjectID)
        //	{
        //		AddNode addNode;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex == -1)
        //		{
        //			addNode = AddNode.NodeAddressPresent;
        //		}
        //		else
        //		{
        //			AddNode addNode1 = ((Project)this._appProjects[projectIndex]).EditNodeAddrOperatorPanelSettings(pCom1Address, pCom2Address, pCom3Address);
        //			addNode = addNode1;
        //		}
        //		return addNode;
        //	}

        //	public byte ExecuteBatchFile(string pProjectName, int pProjectID)
        //	{
        //		byte num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? Convert.ToByte(SimulationErrorType.BATCH_FILE_NOT_PRESENT) : ((ProjectPrizm3)this._appProjects[projectIndex]).ExecuteBatchFile(pProjectName, pProjectID));
        //		return num;
        //	}

        //	public void ExporObjects(int piProjectId, CommonConstants.ExportObjectsInfo pExportObjectsInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ExporObjects(pExportObjectsInfo);
        //		}
        //	}

        //	public void Export(string filepath, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Export(filepath);
        //		}
        //	}

        //	public void ExportTagInfo(CommonConstants.ExportTagInfo pobjExportTagInfo, int piProjectId, int piProductId)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ExportTagInfo(pobjExportTagInfo, piProductId);
        //		}
        //	}

        //	public void FillAlarmLanguageList(int pProjectID, List<CommonConstants.LanguageInformation> _almMgrLangList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).FillAlarmLanguageList(_almMgrLangList);
        //		}
        //	}

        //	public void FillDefaultTagList(int pProjectID, ref ArrayList tempList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).FillDefaultTagList(ref tempList);
        //		}
        //	}

        //	public void FillPattern(int pProjectID, BrushType pBrushType, object pBrushValue, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).FillPattern(pBrushType, pBrushValue, pScreenNumber);
        //		}
        //	}

        //	~Application()
        //	{
        //		this._appProjects.Clear();
        //		this._appLoginList.Clear();
        //		this._appUserList.Clear();
        //		this._appEv3ModelNames.Clear();
        //		this._appEv3ModelBMPNames.Clear();
        //		this._appRecentProjectPaths.Clear();
        //		this._appProjects = null;
        //		this._appLoginList = null;
        //		this._appUserList = null;
        //		this._appEv3ModelNames = null;
        //		this._appEv3ModelNames = null;
        //		this._appEv3ModelBMPNames = null;
        //		this._appRecentProjectPaths = null;
        //	}

        //	public void FindIn_Instruction(int pProjectID, string strFind, uint flags, ref ArrayList objList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).FindIn_Instruction(strFind, flags, ref objList);
        //	}

        //	public void GenerateHTMLPages(string _prizmMDISelProjectNameWithPath, int pProjectID, ushort scrNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GenerateHTMLPages(_prizmMDISelProjectNameWithPath, scrNumber);
        //		}
        //	}

        //	public List<KeyValuePair<string, int>> GetAccessLvlHomeScreenNamesList(int pProjectID)
        //	{
        //		List<KeyValuePair<string, int>> keyValuePairs = new List<KeyValuePair<string, int>>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			keyValuePairs = ((Project)this._appProjects[projectIndex]).GetAccLvlHomScrNamesLst();
        //		}
        //		return keyValuePairs;
        //	}

        //	public SortableBindingList<AccessLvlUser> GetAccessLvlUserName(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		return ((Project)this._appProjects[projectIndex]).GetAccessLvlUserName();
        //	}

        //	public void GetAddressList(int pProjectId, ref ArrayList objList, string InstructionName, int Add, int pScreenNumber, ref ArrayList objFindList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetAddressList(ref objList, InstructionName, Add, pScreenNumber, ref objFindList);
        //		}
        //	}

        //	public List<int> GetAfterHidingTaskIDList(int pScreenNumber, int pProjectID)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetAfterHidingTaskIDList(pScreenNumber);
        //		}
        //		return nums;
        //	}

        //	public CommonConstants.AlarmConfigInfo GetAlarmConfigInfo(int ProjectID)
        //	{
        //		CommonConstants.AlarmConfigInfo projectAlarmConfigInfo = new CommonConstants.AlarmConfigInfo();
        //		int projectIndex = 0;
        //		projectIndex = this.GetProjectIndex(ProjectID);
        //		projectAlarmConfigInfo = ((ProjectPrizm3)this._appProjects[projectIndex]).ProjectAlarmConfigInfo;
        //		return projectAlarmConfigInfo;
        //	}

        //	public List<CommonConstants.AlarmInfo> GetAlarmInformation(int pProjectID)
        //	{
        //		List<CommonConstants.AlarmInfo> alarmInfos = new List<CommonConstants.AlarmInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			alarmInfos = ((Project)this._appProjects[projectIndex]).GetAlarmInfo();
        //		}
        //		return alarmInfos;
        //	}

        //	public List<CommonConstants.AlarmInformation> GetAlarmInformationForPrinting(int pProjectID)
        //	{
        //		List<CommonConstants.AlarmInformation> alarmInformations = new List<CommonConstants.AlarmInformation>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			alarmInformations = ((ProjectPrizm3)this._appProjects[projectIndex]).GetAlarmInformationForPrinting();
        //		}
        //		return alarmInformations;
        //	}

        //	public List<CommonConstants.AlarmParameters> GetAlarmParameters(int piProjectId)
        //	{
        //		List<CommonConstants.AlarmParameters> alarmParameters = new List<CommonConstants.AlarmParameters>();
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			alarmParameters = ((ProjectPrizm3)this._appProjects[projectIndex]).GetAlarmParameters();
        //		}
        //		return alarmParameters;
        //	}

        //	public AlarmType GetAlarmType(int piProjectId)
        //	{
        //		AlarmType alarmType;
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		alarmType = (projectIndex == -1 ? AlarmType.Consicutive_Words_16 : ((ProjectPrizm3)this._appProjects[projectIndex]).ProjectAlarmType);
        //		return alarmType;
        //	}

        //	public List<Email> GetAllEmailList(int pProjectID)
        //	{
        //		List<Email> emails;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		emails = (projectIndex == -1 ? new List<Email>() : ((ProjectPrizm3)this._appProjects[projectIndex]).GetAllEmailList());
        //		return emails;
        //	}

        //	public ArrayList GetAllProjectID()
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		for (int i = 0; i < this._appProjects.Count; i++)
        //		{
        //			arrayLists.Add(((Project)this._appProjects[i]).ProjectID);
        //		}
        //		return arrayLists;
        //	}

        //	public List<CommonConstants.ScreenInfo> GetAllScreensInfoExceptFactory(int pProjectID)
        //	{
        //		List<CommonConstants.ScreenInfo> screenInfos = new List<CommonConstants.ScreenInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			screenInfos = ((Project)this._appProjects[projectIndex]).GetAllScreensInfoExceptFactory();
        //		}
        //		return screenInfos;
        //	}

        //	public List<CommonConstants.ScreenInfo> GetAllWebScreensInfoExceptFactory(int pProjectID)
        //	{
        //		List<CommonConstants.ScreenInfo> screenInfos = new List<CommonConstants.ScreenInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			screenInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetAllWebScreensInfoExceptFactory();
        //		}
        //		return screenInfos;
        //	}

        //	public Size GetAlphanumericGridSize(int pProjectId)
        //	{
        //		Size alphanumericGridSize = new Size();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			alphanumericGridSize = ((ProjectPrizm3)this._appProjects[projectIndex]).GetAlphanumericGridSize();
        //		}
        //		return alphanumericGridSize;
        //	}

        //	public bool GetAnalogConfigInputEnable(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputEnable);
        //		return flag;
        //	}

        //	public bool GetAnalogConfigOutputEnable(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputEnable);
        //		return flag;
        //	}

        //	public List<CommonConstants.AnalogInputConfiguration> GetAnalogInputConfigurationlist(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		List<CommonConstants.AnalogInputConfiguration> analogInputConfigurations = new List<CommonConstants.AnalogInputConfiguration>();
        //		if (projectIndex != -1)
        //		{
        //			for (int i = 0; i < ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration.Count; i++)
        //			{
        //				CommonConstants.AnalogInputConfiguration analogInputConfiguration = new CommonConstants.AnalogInputConfiguration()
        //				{
        //					AdjustingByte1 = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].AdjustingByte1,
        //					BaudRate = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].BaudRate,
        //					DataBit = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].DataBit,
        //					DeviceId = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].DeviceId,
        //					InputCalibration = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputCalibration,
        //					InputChannelDependentTypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputChannelDependentTypeIndex,
        //					InputChannelIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputChannelIndex,
        //					InputChannelNo = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputChannelNo,
        //					InputChannelTypeChar = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputChannelTypeChar,
        //					InputChannelTypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputChannelTypeIndex,
        //					InputDataRegister1TypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputDataRegister1TypeIndex,
        //					InputDataRegister1Value = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputDataRegister1Value,
        //					InputDataRegister2TypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputDataRegister2TypeIndex,
        //					InputDataRegister2Value = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputDataRegister2Value,
        //					InputDataRegister3TypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputDataRegister3TypeIndex,
        //					InputDataRegister3Value = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputDataRegister3Value,
        //					InputDataRegister4TypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputDataRegister4TypeIndex,
        //					InputDataRegister4Value = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].InputDataRegister4Value,
        //					NormalizationFactor = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].NormalizationFactor,
        //					Parity = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].Parity,
        //					ReservedBytes = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].ReservedBytes,
        //					StopBit = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration[i].StopBit
        //				};
        //				analogInputConfigurations.Add(analogInputConfiguration);
        //			}
        //		}
        //		return analogInputConfigurations;
        //	}

        //	public List<CommonConstants.AnalogOutputConfiguration> GetAnalogOutputConfigurationlist(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		List<CommonConstants.AnalogOutputConfiguration> analogOutputConfigurations = new List<CommonConstants.AnalogOutputConfiguration>();
        //		if (projectIndex != -1)
        //		{
        //			for (int i = 0; i < ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration.Count; i++)
        //			{
        //				CommonConstants.AnalogOutputConfiguration analogOutputConfiguration = new CommonConstants.AnalogOutputConfiguration()
        //				{
        //					AdjustingByte3 = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].AdjustingByte3,
        //					AdjustingByte4 = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].AdjustingByte4,
        //					OutputCalibration = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputCalibration,
        //					OutputChannelDependentTypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputChannelDependentTypeIndex,
        //					OutputChannelIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputChannelIndex,
        //					OutputChannelNo = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputChannelNo,
        //					OutputChannelTypeChar = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputChannelTypeChar,
        //					OutputChannelTypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputChannelTypeIndex,
        //					OutputDataRegister1TypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputDataRegister1TypeIndex,
        //					OutputDataRegister1Value = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputDataRegister1Value,
        //					OutputDataRegister2TypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputDataRegister2TypeIndex,
        //					OutputDataRegister2Value = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputDataRegister2Value,
        //					OutputDataRegister3TypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputDataRegister3TypeIndex,
        //					OutputDataRegister3Value = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputDataRegister3Value,
        //					OutputDataRegister4TypeIndex = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputDataRegister4TypeIndex,
        //					OutputDataRegister4Value = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].OutputDataRegister4Value,
        //					ReservedBytes = ((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration[i].ReservedBytes
        //				};
        //				analogOutputConfigurations.Add(analogOutputConfiguration);
        //			}
        //		}
        //		return analogOutputConfigurations;
        //	}

        //	public CommonConstants.ApplicationInformation GetApplicationInformation(int pProjectID)
        //	{
        //		CommonConstants.ApplicationInformation applicationInformation = new CommonConstants.ApplicationInformation();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			applicationInformation = ((ProjectPrizm3)this._appProjects[projectIndex]).GetApplicationInformation();
        //		}
        //		return applicationInformation;
        //	}

        //	public ArrayList GetAssociatedTemplateScreenInfo(ArrayList listImportScreens, int ImportProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(ImportProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((ProjectPrizm3)this._appProjects[projectIndex]).GetAssociatedTemplateScreenInfo(listImportScreens);
        //		}
        //		return arrayLists;
        //	}

        //	public uint GetAvailablememorySizeWithDataLogger(int pProjectID)
        //	{
        //		uint availablememorySizeWithDataLogger = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			availablememorySizeWithDataLogger = ((ProjectPrizm3)this._appProjects[projectIndex]).GetAvailablememorySizeWithDataLogger();
        //		}
        //		return availablememorySizeWithDataLogger;
        //	}

        //	public List<int> GetBaseScreenNumbersList(int pProjectID)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((Project)this._appProjects[projectIndex]).GetBaseScreenNumbersList();
        //		}
        //		return nums;
        //	}

        //	public List<int> GetBeforeShowingTaskIDList(int pScreenNumber, int pProjectID)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetBeforeShowingTaskIDList(pScreenNumber);
        //		}
        //		return nums;
        //	}

        //	private string GetBitNumberStringValues(int piNumber)
        //	{
        //		string str;
        //		str = (piNumber.ToString().Length >= 2 ? piNumber.ToString() : string.Concat("0", piNumber.ToString()));
        //		return str;
        //	}

        //	public string GetBlockComment(int piBlockIndex)
        //	{
        //		return this._appHIOLibrary.BlockComment[piBlockIndex];
        //	}

        //	public int GetBlockCount(int pProjectID, int type)
        //	{
        //		int num;
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((ProjectPrizm3)this._appProjects[projectIndex]).GetBlockCount(type));
        //		return num;
        //	}

        //	public CommonConstants.Prizm3BlockStructure GetBlockInformation(int pProjectId, int piTagId)
        //	{
        //		CommonConstants.Prizm3BlockStructure blockInformation = new CommonConstants.Prizm3BlockStructure();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			blockInformation = ((ProjectPrizm3)this._appProjects[projectIndex]).GetBlockInformation(piTagId);
        //		}
        //		return blockInformation;
        //	}

        //	public ArrayList GetBlockList(int pProjectID, int type)
        //	{
        //		ArrayList arrayLists;
        //		ArrayList arrayLists1 = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		arrayLists = (projectIndex == -1 ? arrayLists1 : ((ProjectPrizm3)this._appProjects[projectIndex]).GetBlockList(type));
        //		return arrayLists;
        //	}

        //	public string GetBlockName(int piBlockIndex)
        //	{
        //		return this._appHIOLibrary.BlockName[piBlockIndex];
        //	}

        //	public string GetBlockName(int pProjectID, string strName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		return ((ProjectPrizm3)this._appProjects[projectIndex]).GetBlockName(strName);
        //	}

        //	public int GetBlockNumber(int piBlockIndex)
        //	{
        //		return this._appHIOLibrary.BlockNo[piBlockIndex];
        //	}

        //	public DataTable GetBlockTagInformation(int pProjectID)
        //	{
        //		DataTable dataTable = new DataTable();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			dataTable = ((Project)this._appProjects[projectIndex]).GetBlockTagInformation();
        //		}
        //		return dataTable;
        //	}

        //	public int GetBreakPointCount(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		return ((ProjectPrizm3)this._appProjects[projectIndex]).GetBreakPointCount();
        //	}

        //	public bool GetBreakPointInfo(int pProjectID, int screenNumber, ref long address)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		bool breakPointInfo = ((ProjectPrizm3)this._appProjects[projectIndex]).GetBreakPointInfo(screenNumber, ref address);
        //		return breakPointInfo;
        //	}

        //	public void GetBreakPointList(int pProjectID, ref ArrayList objList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).GetBreakPointList(ref objList);
        //	}

        //	public List<bool> GetBroadcastForceCommandFlags(int pProjectID, byte pbtPort)
        //	{
        //		List<bool> broadcastForceCommandFlags;
        //		List<bool> flags = new List<bool>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex == -1)
        //		{
        //			flags.Add(false);
        //			flags.Add(false);
        //			broadcastForceCommandFlags = flags;
        //		}
        //		else
        //		{
        //			broadcastForceCommandFlags = ((ProjectPrizm3)this._appProjects[projectIndex]).GetBroadcastForceCommandFlags(pbtPort);
        //		}
        //		return broadcastForceCommandFlags;
        //	}

        //	public CommonConstants.BroadcastSettings GetBroadcastSettings(int pProjectID, byte pbtPort)
        //	{
        //		CommonConstants.BroadcastSettings broadcastSetting;
        //		CommonConstants.BroadcastSettings broadcastSetting1 = new CommonConstants.BroadcastSettings();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		broadcastSetting = (projectIndex == -1 ? broadcastSetting1 : ((ProjectPrizm3)this._appProjects[projectIndex]).GetBroadcastSettings(pbtPort));
        //		return broadcastSetting;
        //	}

        //	public int GetColorSupportedValue(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? -1 : ((Project)this._appProjects[projectIndex]).ColorSupportedValue);
        //		return num;
        //	}

        //	public ArrayList GetCommNodeInfo(int pProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetCommNodeInfo();
        //		}
        //		return arrayLists;
        //	}

        //	public DataTable GetContactGroups(int pProjectID)
        //	{
        //		DataTable contactGroups;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex == -1)
        //		{
        //			contactGroups = null;
        //		}
        //		else
        //		{
        //			contactGroups = ((ProjectPrizm3)this._appProjects[projectIndex]).GetContactGroups();
        //		}
        //		return contactGroups;
        //	}

        //	public int GetDataLogClearBitTagId(int pProjectID)
        //	{
        //		int dataLogClearBitTagId = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			dataLogClearBitTagId = ((ProjectPrizm3)this._appProjects[projectIndex]).GetDataLogClearBitTagId();
        //		}
        //		return dataLogClearBitTagId;
        //	}

        //	public byte GetDataLoggerLoggingType(int pProjectId)
        //	{
        //		byte dataLoggerLoggingTypeInfo = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			dataLoggerLoggingTypeInfo = ((ProjectPrizm3)this._appProjects[projectIndex]).GetDataLoggerLoggingTypeInfo();
        //		}
        //		return dataLoggerLoggingTypeInfo;
        //	}

        //	public byte GetDataLoggerLoggingTypeExternal(int pProjectId)
        //	{
        //		byte dataLoggerLoggingTypeExternal = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			dataLoggerLoggingTypeExternal = ((ProjectPrizm3)this._appProjects[projectIndex]).GetDataLoggerLoggingTypeExternal();
        //		}
        //		return dataLoggerLoggingTypeExternal;
        //	}

        //	public byte GetDataLoggerMemoryFull(int pProjectId)
        //	{
        //		byte dataloggerMemFullInformation = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			dataloggerMemFullInformation = ((ProjectPrizm3)this._appProjects[projectIndex]).GetDataloggerMemFullInformation();
        //		}
        //		return dataloggerMemFullInformation;
        //	}

        //	public byte GetDataLoggerMemorySize(int pProjectId)
        //	{
        //		byte dataloggerMemSizeInformation = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			dataloggerMemSizeInformation = ((ProjectPrizm3)this._appProjects[projectIndex]).GetDataloggerMemSizeInformation();
        //		}
        //		return dataloggerMemSizeInformation;
        //	}

        //	public ArrayList GetDataMonitorInfo(int pProjectID)
        //	{
        //		ArrayList arrayLists;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		arrayLists = (projectIndex == -1 ? new ArrayList() : ((ProjectPrizm3)this._appProjects[projectIndex]).GetDataMonitorInfo());
        //		return arrayLists;
        //	}

        //	public ArrayList GetDataMonitorList(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		return ((ProjectPrizm3)this._appProjects[projectIndex]).GetDataMonitorList();
        //	}

        //	public bool GetDedugStepInfo(int pProjectID, int screenNumber, ref CommonConstants.DebugStepInfo objInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		bool dedugStepInfo = ((ProjectPrizm3)this._appProjects[projectIndex]).GetDedugStepInfo(screenNumber, ref objInfo);
        //		return dedugStepInfo;
        //	}

        //	public void GetDetailsOfBlockListToImport(ref ArrayList ArrBlockList, string strFileName, ref bool IsLadderSupported, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ReadLadderImport_Block(ref ArrBlockList, strFileName, ref IsLadderSupported);
        //		}
        //	}

        //	public int GetDeviceSafeRemovelBitTagId(int pProjectID)
        //	{
        //		int deviceSafeRemovelBitTagId = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			deviceSafeRemovelBitTagId = ((ProjectPrizm3)this._appProjects[projectIndex]).GetDeviceSafeRemovelBitTagId();
        //		}
        //		return deviceSafeRemovelBitTagId;
        //	}

        //	public bool GetDownLoadTagName(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).DownLoadTagName);
        //		return flag;
        //	}

        //	public List<DriverInformation> GetDriverInformation(int pProjectID)
        //	{
        //		List<DriverInformation> driverInformations = new List<DriverInformation>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			driverInformations = ((ProjectPrizm3)this._appProjects[projectIndex]).GetDriverInformation();
        //		}
        //		return driverInformations;
        //	}

        //	public Email GetEmail(int pProjectID, string pEmailName)
        //	{
        //		Email email;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		email = (projectIndex == -1 ? new Email() : ((ProjectPrizm3)this._appProjects[projectIndex]).GetEmail(pEmailName));
        //		return email;
        //	}

        //	public EmailConfigInfo GetEmailConfiguration(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		return ((ProjectPrizm3)this._appProjects[projectIndex]).GetEmailConfiguration();
        //	}

        //	public bool GetEraseDataLoggerMemory(int pProjectID)
        //	{
        //		bool eraseDataLoggerMemory = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			eraseDataLoggerMemory = ((ProjectPrizm3)this._appProjects[projectIndex]).EraseDataLoggerMemory;
        //		}
        //		return eraseDataLoggerMemory;
        //	}

        //	public CommonConstants.EthernetSettings GetEthernetSettings(int pProjectID)
        //	{
        //		CommonConstants.EthernetSettings ethernetsettings = new CommonConstants.EthernetSettings()
        //		{
        //			_DHCP = false,
        //			_DownloadPort = new decimal(5000),
        //			_IPAdderess = "192.168.0.254",
        //			_SubnetMask = "255.255.255.0",
        //			_DefaultGateway = "0.0.0.0",
        //			_MonitoringPort = new decimal(1100)
        //		};
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			ethernetsettings = ((ProjectPrizm3)this._appProjects[projectIndex]).Ethernetsettings;
        //		}
        //		return ethernetsettings;
        //	}

        //	public byte GetExpansionModuleType(int pProjectID)
        //	{
        //		byte expansionModuleType;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex == -1)
        //		{
        //			expansionModuleType = 0;
        //		}
        //		else
        //		{
        //			expansionModuleType = ((ProjectPrizm3)this._appProjects[projectIndex]).ExpansionModuleType;
        //		}
        //		return expansionModuleType;
        //	}

        //	public uint GetFileSize(int pProjectID, string _strprizmMDISelProjectNameWithPath)
        //	{
        //		uint num = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			num = ((ProjectPrizm3)this._appProjects[projectIndex]).GetfileSize(_strprizmMDISelProjectNameWithPath);
        //		}
        //		return num;
        //	}

        //	public int GetFirstBlock_ScreenNumber(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((Project)this._appProjects[projectIndex]).GetFirstBlock_ScreenNumber());
        //		return num;
        //	}

        //	public int GetFrequencyTagTagId(int pProjectId)
        //	{
        //		int frequencyTagsTagId = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			frequencyTagsTagId = ((ProjectPrizm3)this._appProjects[projectIndex]).GetFrequencyTagsTagId();
        //		}
        //		return frequencyTagsTagId;
        //	}

        //	public int GetFrequencyTagTagIdExernal(int pProjectId)
        //	{
        //		int frequencyTagsTagIdExternal = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			frequencyTagsTagIdExternal = ((ProjectPrizm3)this._appProjects[projectIndex]).GetFrequencyTagsTagIdExternal();
        //		}
        //		return frequencyTagsTagIdExternal;
        //	}

        //	public CommonConstants.structFTPInfo GetFTPInformation(int pProjectID)
        //	{
        //		CommonConstants.structFTPInfo fTPInformation = new CommonConstants.structFTPInfo();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			fTPInformation = ((ProjectPrizm3)this._appProjects[projectIndex]).GetFTPInformation();
        //		}
        //		return fTPInformation;
        //	}

        //	public CommonConstants.G9SPNodeInfo GetG9SPNodeInfoByComPortNodeName(int pProjectID, byte piComPort, string pstrNodeName)
        //	{
        //		CommonConstants.G9SPNodeInfo g9SPNodeInfoByComPortNodeName = new CommonConstants.G9SPNodeInfo();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			g9SPNodeInfoByComPortNodeName = ((ProjectPrizm3)this._appProjects[projectIndex]).GetG9SPNodeInfoByComPortNodeName(piComPort, pstrNodeName);
        //		}
        //		return g9SPNodeInfoByComPortNodeName;
        //	}

        //	public CommonConstants.G9SPNodeInfo GetG9SPNodeInformation(int pProjectID, string pstrNodeName)
        //	{
        //		CommonConstants.G9SPNodeInfo g9SPNodeInformation = new CommonConstants.G9SPNodeInfo();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			g9SPNodeInformation = ((ProjectPrizm3)this._appProjects[projectIndex]).GetG9SPNodeInformation(pstrNodeName);
        //		}
        //		return g9SPNodeInformation;
        //	}

        //	public List<GlobalKeys> GetGlobalKeysList(int pProjectID)
        //	{
        //		List<GlobalKeys> globalKeys = new List<GlobalKeys>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			globalKeys = ((ProjectPrizm3)this._appProjects[projectIndex]).GetGlobalKeysList();
        //		}
        //		return globalKeys;
        //	}

        //	public List<string> GetGlobalLadderBlockNames(int pProjectId)
        //	{
        //		List<string> strs = new List<string>();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			strs = (this._appProjects[projectIndex] as ProjectPrizm3).GetGlobalLadderBlockNames();
        //		}
        //		return strs;
        //	}

        //	public List<int> GetGlobalTaskIDList(int pProjectID)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetGlobalTaskIDList();
        //		}
        //		return nums;
        //	}

        //	public int[] GetGroupControlBitTagId(int pProjectID)
        //	{
        //		int[] groupControlBitTagId = new int[50];
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			groupControlBitTagId = ((ProjectPrizm3)this._appProjects[projectIndex]).GetGroupControlBitTagId();
        //		}
        //		return groupControlBitTagId;
        //	}

        //	public byte GetGroupNoFromLoggedTagsID(int pProjectID, int piTagId, out int piTagIndex)
        //	{
        //		piTagIndex = 0;
        //		byte groupNoFromLoggedTagsID = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			groupNoFromLoggedTagsID = ((ProjectPrizm3)this._appProjects[projectIndex]).GetGroupNoFromLoggedTagsID(piTagId, out piTagIndex);
        //		}
        //		return groupNoFromLoggedTagsID;
        //	}

        //	public byte GetGroupNoFromLoggedTagsIDExternal(int pProjectID, int piTagId, out int piTagIndex)
        //	{
        //		piTagIndex = 0;
        //		byte groupNoFromLoggedTagsIDExternal = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			groupNoFromLoggedTagsIDExternal = ((ProjectPrizm3)this._appProjects[projectIndex]).GetGroupNoFromLoggedTagsIDExternal(piTagId, out piTagIndex);
        //		}
        //		return groupNoFromLoggedTagsIDExternal;
        //	}

        //	public string GetHMINodeName(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		return ((Project)this._appProjects[projectIndex]).GetHMINodeName();
        //	}

        //	public byte GetHWStructProtocolOnPortValue(int pProjectId, byte pbtPort)
        //	{
        //		byte num;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		num = (projectIndex == -1 ? Convert.ToByte(0) : ((ProjectPrizm3)this._appProjects[projectIndex]).GetHWStructProtocolOnPortValue(pbtPort));
        //		return num;
        //	}

        //	public List<byte> GetHWStructureSpecialPLCInfo(int pProjectID)
        //	{
        //		List<byte> nums = new List<byte>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetHWStructureSpecialPLCInfo();
        //		}
        //		return nums;
        //	}

        //	public List<CommonConstants.LanguageInformation> GetImportedLanguageList(int importProjectId)
        //	{
        //		List<CommonConstants.LanguageInformation> languageInformations = new List<CommonConstants.LanguageInformation>();
        //		int projectIndex = this.GetProjectIndex(importProjectId);
        //		if (projectIndex != -1)
        //		{
        //			languageInformations = ((Project)this._appProjects[projectIndex]).GetImportedLanguageList();
        //		}
        //		return languageInformations;
        //	}

        //	public List<CommonConstants.ScreenInfo> GetImportedScreenInformation(int pProjectID)
        //	{
        //		List<CommonConstants.ScreenInfo> screenInfos = new List<CommonConstants.ScreenInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			screenInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetImportedScreenInformation();
        //		}
        //		return screenInfos;
        //	}

        //	public void GetImportScreenData(string pstrFileName, ref int ImportProjectID)
        //	{
        //		if ((this._appProjects.Count <= 0 ? false : CommonConstants.ImportScreenFlag))
        //		{
        //			this._prev_g_Support_IEC_Ladder = CommonConstants.g_Support_IEC_Ladder;
        //			this._prevProductDataInfo = CommonConstants.ProductDataInfo;
        //			this._prev_g_ProjectPath = CommonConstants.g_ProjectPath;
        //			CommonConstants.ImportTempDefaultRegTagName = CommonConstants.DefaultRegTagName;
        //			CommonConstants.ImportTempDefaultRegTagId = CommonConstants.DefaultRegTagId;
        //			CommonConstants.ImportTempDefaultBitTagName = CommonConstants.DefaultBitTagName;
        //			CommonConstants.ImportTempDefaultBitTagId = CommonConstants.DefaultBitTagId;
        //			CommonConstants.ImportScreenIsProductVertical = CommonConstants._isProductVertical;
        //			this.ImportScreenSelectedProjectID = CommonConstants.SelectedProjectID;
        //			this.ImportScreenTagIDCount = Tag.TagIDCount;
        //			this.ImportScreenNodeIDCount = Node.NodeIDCount;
        //		}
        //		ProjectPrizm3 projectPrizm3 = new ProjectPrizm3();
        //		this._appProjects.Insert(0, projectPrizm3);
        //		((Project)this._appProjects[0]).Projecttype = ProjectType.Prizm3;
        //		((Project)this._appProjects[0]).ProjectPath = pstrFileName;
        //		ImportProjectID = ((Project)this._appProjects[0]).ProjectID;
        //		if (File.Exists(pstrFileName))
        //		{
        //			((Project)this._appProjects[0]).DateLastEdited = File.GetLastWriteTime(pstrFileName).ToShortDateString();
        //			Project item = (Project)this._appProjects[0];
        //			object[] hours = new object[5];
        //			TimeSpan timeOfDay = File.GetLastWriteTime(pstrFileName).TimeOfDay;
        //			hours[0] = timeOfDay.Hours;
        //			hours[1] = ":";
        //			timeOfDay = File.GetLastWriteTime(pstrFileName).TimeOfDay;
        //			hours[2] = timeOfDay.Minutes;
        //			hours[3] = ":";
        //			timeOfDay = File.GetLastWriteTime(pstrFileName).TimeOfDay;
        //			hours[4] = timeOfDay.Seconds;
        //			item.TimeLastEdited = string.Concat(hours);
        //		}
        //		projectPrizm3._projectprizm3DownloadPercentage += new ProjectPrizm3.DownloadPercentage(this.GetPercentage);
        //		projectPrizm3._projectprizm3DownloadStatus += new ProjectPrizm3.DownloadStatus(this.serial__deviceDownloadStatus);
        //		projectPrizm3._projectprizm3ImportProgressValue += new ProjectPrizm3.ImportProgressValue(this.ProjectPrizm3__projectprizm3ImportProgressValue);
        //		projectPrizm3._projectPrizm3ExportProgressValue += new ProjectPrizm3.ExportProgressValue(this.ProjectPrizm3__projectPrizm3ExportProgressValue);
        //		projectPrizm3._exportObjectProgressBar += new ProjectPrizm3.ExportObjectProgressBar(this.ProjectPrizm3__exportObjectProgressBar);
        //		projectPrizm3._evntAddTagInStratonVMDB += new ProjectPrizm3.AddTagInStratonVMDB(this.ProjectPrizm3__evntAddTagInStratonVMDB);
        //		projectPrizm3._evntAddNodeStatusTagOnImport += new ProjectPrizm3.AddNodeStatusTagOnImport(this.ProjectPrizm3__evntAddNodeStatusTagOnImport);
        //		if (projectPrizm3.Read(pstrFileName) != 0)
        //		{
        //			((ProjectPrizm3)this._appProjects[0]).RemoveTagSelectionInfoObject();
        //			this._appProjects.RemoveAt(0);
        //			ProjectTagInformation.RemoveProject(ImportProjectID);
        //			projectPrizm3 = null;
        //		}
        //	}

        //	public ClassList.Screen GetImportScreenData(int pProjectID, int ScreenNumber)
        //	{
        //		ClassList.Screen screen = new ClassList.Screen();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			screen = ((ProjectPrizm3)this._appProjects[projectIndex]).GetImportScreenData(ScreenNumber);
        //		}
        //		return screen;
        //	}

        //	public void GetInstructionList(int pProjectId, ref ArrayList objList, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetInstructionList(ref objList, pScreenNumber);
        //		}
        //	}

        //	public void GetLadderBlockName_ForUsedTag(int pProjectId, string strTagAddress, ref string strBlockName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetLadderBlockName_ForUsedTag(strTagAddress, ref strBlockName);
        //		}
        //	}

        //	public void GetLadderBlocksPrintInfo(int pProjectID, ref ArrayList objArrayList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).GetLadderBlocksPrintInfo(ref objArrayList);
        //		}
        //	}

        //	public void GetLadderInfo(int pProjectId, int ScreenNumber, ref CommonConstants.LadderScreenInfo objLadderInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetLadderInfo(ScreenNumber, ref objLadderInfo);
        //		}
        //	}

        //	public void GetLAdderNewPageHeight(int pProjectID, int ScreenNumber, Point ptEnd, ref int PageHeight)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).GetLAdderNewPageHeight(ScreenNumber, ptEnd, ref PageHeight);
        //		}
        //	}

        public void GetLadderScreenName(int pProjectID, ref ArrayList objList)
        {
            int projectIndex = this.GetProjectIndex(pProjectID);
            //if (projectIndex != -1)
            //{
            //    (this._appProjects[projectIndex]).GetLadderScreenName(ref objList);
            //}
        }

        public void GetLadderScreenName(ref ArrayList objList)
        {
            //this._projectScreenManager.GetLadderScreenName(ref objList);

            string str;
            str = "string";
            objList.Add(str);
        }

        //	public List<CommonConstants.LanguageInformation> GetLanguageList(int pProjectId)
        //	{
        //		List<CommonConstants.LanguageInformation> languageInformations;
        //		List<CommonConstants.LanguageInformation> languageInformations1 = new List<CommonConstants.LanguageInformation>();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		languageInformations = (projectIndex == -1 ? languageInformations1 : ((Project)this._appProjects[projectIndex]).GetLanguageList());
        //		return languageInformations;
        //	}

        //	public List<CommonConstants.LoggerGroupInfo> GetLoggerGroupsInformation(int pProjectId)
        //	{
        //		List<CommonConstants.LoggerGroupInfo> loggerGroupInfos = new List<CommonConstants.LoggerGroupInfo>();
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			loggerGroupInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetLoggerGroupsInformation();
        //		}
        //		return loggerGroupInfos;
        //	}

        //	public List<CommonConstants.LoggerGroupInfoExternal> GetLoggerGroupsInformationExternal(int pProjectId)
        //	{
        //		List<CommonConstants.LoggerGroupInfoExternal> loggerGroupInfoExternals = new List<CommonConstants.LoggerGroupInfoExternal>();
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			loggerGroupInfoExternals = ((ProjectPrizm3)this._appProjects[projectIndex]).GetLoggerGroupsInformationExternal();
        //		}
        //		return loggerGroupInfoExternals;
        //	}

        //	public List<int> GetLoggerGroupTags(int pProjectId, int gindex)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetLoggerGroupTagsInformation(gindex);
        //		}
        //		return nums;
        //	}

        //	public List<int> GetLoggerGroupTagsExternal(int pProjectId, int gindex)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetLoggerGroupTagsInformationExternal(gindex);
        //		}
        //		return nums;
        //	}

        //	public List<int> GetLoggerIDList(int pProjectID)
        //	{
        //		List<int> nums;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		nums = (projectIndex == -1 ? new List<int>() : ((ProjectPrizm3)this._appProjects[projectIndex]).GetLoggerTagIDList());
        //		return nums;
        //	}

        //	public List<int> GetLoggerTagIDListExternal(int pProjectID)
        //	{
        //		List<int> nums;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		nums = (projectIndex == -1 ? new List<int>() : ((ProjectPrizm3)this._appProjects[projectIndex]).GetLoggerTagIDListExternal());
        //		return nums;
        //	}

        //	public int[] GetLoggingBitTagId(int pProjectID)
        //	{
        //		int[] loggingBitTagId = new int[4];
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			loggingBitTagId = ((ProjectPrizm3)this._appProjects[projectIndex]).GetLoggingBitTagId();
        //		}
        //		return loggingBitTagId;
        //	}

        //	public int[] GetLoggingBitTagIdExternal(int pProjectID)
        //	{
        //		int[] loggingBitTagIdExternal = new int[50];
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			loggingBitTagIdExternal = ((ProjectPrizm3)this._appProjects[projectIndex]).GetLoggingBitTagIdExternal();
        //		}
        //		return loggingBitTagIdExternal;
        //	}

        //	public void GetLogicBlockInfo(ref ArrayList objList, int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetLogicBlockInfo(ref objList);
        //		}
        //	}

        //	public void GetMatching_OperandInfoList(int pProjectID, int ScreenNumber, int Type, string str, ref ArrayList objList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).GetMatching_OperandInfoList(ScreenNumber, Type, str, ref objList);
        //		}
        //	}

        //	public int GetMemoryUsedRegTagId(int pProjectID)
        //	{
        //		int memoryUsedRegTagId = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			memoryUsedRegTagId = ((ProjectPrizm3)this._appProjects[projectIndex]).GetMemoryUsedRegTagId();
        //		}
        //		return memoryUsedRegTagId;
        //	}

        //	public void GetMITSQSettings(int pProjectID, ref CommonConstants.MITQSettings objSettings)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetMITSQSettings(ref objSettings);
        //		}
        //	}

        //	public ArrayList GetModbusSlaveComData(int pProjectId)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((ProjectPrizm3)this._appProjects[projectIndex]).GetModbusSlaveComData();
        //		}
        //		return arrayLists;
        //	}

        //	public ArrayList GetModelNames(int pProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetModelNames();
        //		}
        //		return arrayLists;
        //	}

        //	public bool GetNewProjectStatus(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsNewProject);
        //		return flag;
        //	}

        //	public int GetNextStAddress(int pProjectID, string pPrefix)
        //	{
        //		int nextStAddress = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nextStAddress = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNextStAddress(pPrefix);
        //		}
        //		return nextStAddress;
        //	}

        //	public bool GetNextStepObjectID(int pProjectID, int screenNumber, long StepAddress, ref int ID, ref long NextStepAddr)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		bool nextStepObjectID = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNextStepObjectID(screenNumber, StepAddress, ref ID, ref NextStepAddr);
        //		return nextStepObjectID;
        //	}

        //	public CommonConstants.NodeInfo GetNode_ScreenImport(int pProjectID, CommonConstants.NodeInfo pNodeinfo)
        //	{
        //		CommonConstants.NodeInfo nodeScreenImport = new CommonConstants.NodeInfo()
        //		{
        //			_iNodeId = -1
        //		};
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeScreenImport = ((Project)this._appProjects[projectIndex]).GetNode_ScreenImport(pNodeinfo);
        //		}
        //		return nodeScreenImport;
        //	}

        //	public int GetNodeAddress(int piProjectID, int piNodeId)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectID);
        //		return ((Project)this._appProjects[projectIndex]).GetNodeAddress(piNodeId);
        //	}

        //	public ArrayList GetNodeBlockList(int pIndex)
        //	{
        //		return ((Project)this._appProjects[pIndex]).GetNodeBlockList();
        //	}

        //	public int GetNodeCountForThisPort(int pProjectID, byte pbtPort)
        //	{
        //		int nodeCountForThisPort = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeCountForThisPort = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNodeCountForThisPort(pbtPort);
        //		}
        //		return nodeCountForThisPort;
        //	}

        //	public void GetNodeDetails(int pProjectId, ref ArrayList objList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).GetNodeDetails(ref objList);
        //		}
        //	}

        //	public int GetNodeId(byte pbtPlcCode, int pProjectId)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		num = (projectIndex == -1 ? -1 : ((Project)this._appProjects[projectIndex]).GetNodeID(pbtPlcCode));
        //		return num;
        //	}

        //	public int GetNodeID(int pProjectID, string pstrNodeName)
        //	{
        //		int nodeID = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeID = ((Project)this._appProjects[projectIndex]).GetNodeID(pstrNodeName);
        //		}
        //		return nodeID;
        //	}

        //	public List<int> GetNodeIDList(int pProjectID)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((Project)this._appProjects[projectIndex]).GetNodeIDList();
        //		}
        //		return nums;
        //	}

        //	public List<int> GetNodeIDList(int pProjectID, List<string> pNodeNames)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((Project)this._appProjects[projectIndex]).GetNodeIDList(pNodeNames);
        //		}
        //		return nums;
        //	}

        //	public CommonConstants.NodeInfo GetNodeInfoByComPortNodeName(int pProjectID, byte piComPort, string pstrNodeName)
        //	{
        //		CommonConstants.NodeInfo nodeInfoByComPortNodeName = new CommonConstants.NodeInfo();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeInfoByComPortNodeName = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNodeInfoByComPortNodeName(piComPort, pstrNodeName);
        //		}
        //		return nodeInfoByComPortNodeName;
        //	}

        //	public CommonConstants.NodeInfo GetNodeInformation(int pProjectID, string pstrNodeName)
        //	{
        //		CommonConstants.NodeInfo nodeInformation = new CommonConstants.NodeInfo();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeInformation = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNodeInformation(pstrNodeName);
        //		}
        //		return nodeInformation;
        //	}

        //	public List<CommonConstants.NodeInfo> GetNodeInformationByComPort(int pProjectID, byte pbtComPort)
        //	{
        //		List<CommonConstants.NodeInfo> nodeInfos = new List<CommonConstants.NodeInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNodeInformationPort(pbtComPort);
        //		}
        //		return nodeInfos;
        //	}

        //	public List<CommonConstants.NodeInformation> GetNodeInformationforPrinting(int pProjectID, int piProductID)
        //	{
        //		List<CommonConstants.NodeInformation> nodeInformations = new List<CommonConstants.NodeInformation>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeInformations = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNodeInformationforPrinting(piProductID);
        //		}
        //		return nodeInformations;
        //	}

        //	public CommonConstants.NodeInfo GetNodeInformationPort(int pProjectID, byte piComPortNode)
        //	{
        //		CommonConstants.NodeInfo nodeInfo;
        //		List<CommonConstants.NodeInfo> nodeInfos = new List<CommonConstants.NodeInfo>();
        //		CommonConstants.NodeInfo nodeInfo1 = new CommonConstants.NodeInfo();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNodeInformationPort(piComPortNode);
        //		}
        //		nodeInfo = (nodeInfos.Count == 0 ? nodeInfo1 : nodeInfos[0]);
        //		return nodeInfo;
        //	}

        //	public List<CommonConstants.NodeInfo> GetNodeList(int pProjectID)
        //	{
        //		List<CommonConstants.NodeInfo> nodeInfos = new List<CommonConstants.NodeInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNodeList();
        //		}
        //		return nodeInfos;
        //	}

        //	public string GetNodeNameFromNodeID(int pProjectID, int pNodeid)
        //	{
        //		string nodeNameFromNodeID = "";
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeNameFromNodeID = ((ProjectPrizm3)this._appProjects[projectIndex]).GetNodeNameFromNodeID(pNodeid);
        //		}
        //		return nodeNameFromNodeID;
        //	}

        //	public ArrayList GetNodeNames(int pProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetNodeNames();
        //		}
        //		return arrayLists;
        //	}

        //	public int GetNodePort(int piProjectID, int piNodeId)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectID);
        //		return ((Project)this._appProjects[projectIndex]).GetNodePort(piNodeId);
        //	}

        //	public ArrayList GetObjectList(int pProjectID, int piScreenNumber)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetObjectList(piScreenNumber);
        //		}
        //		return arrayLists;
        //	}

        //	public ArrayList GetObjectList_InRectangle(int pProjectID, int pScreenNumber, System.Drawing.Rectangle objRectArea)
        //	{
        //		ArrayList arrayLists;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		arrayLists = (projectIndex == -1 ? new ArrayList() : ((Project)this._appProjects[projectIndex]).GetObjectList_InRectangle(pScreenNumber, objRectArea));
        //		return arrayLists;
        //	}

        //	public ArrayList GetObjectTagInformation(int pProjectID, string pstrScreenName, string pstrObjectName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		ArrayList arrayLists = new ArrayList();
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetObjectTagInformation(pstrScreenName, pstrObjectName);
        //		}
        //		return arrayLists;
        //	}

        //	public int[] GetOpenScreenList(int pProjectID)
        //	{
        //		int[] openScreenList;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex == -1)
        //		{
        //			openScreenList = null;
        //		}
        //		else
        //		{
        //			openScreenList = ((Project)this._appProjects[projectIndex]).GetOpenScreenList();
        //		}
        //		return openScreenList;
        //	}

        //	public void GetOperandAddressList(int pProjectId, ref ArrayList objList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetOperandAddressList(ref objList);
        //		}
        //	}

        //	public void GetOperandAddressList(int pProjectId, ref ArrayList objList, int ScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetOperandAddressList(ref objList, ScreenNumber);
        //		}
        //	}

        //	public bool GetOperandInfo(int pProjectId, int pScreenNumber, Point pt, int which, ref CommonConstants.LadderOperandInfo objLadderOperandInfo)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).GetOperandInfo(pScreenNumber, pt, which, ref objLadderOperandInfo));
        //		return flag;
        //	}

        //	public void GetOperandNameList(int pProjectId, ref ArrayList objList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetOperandNameList(ref objList);
        //		}
        //	}

        //	public void GetOperandRectList(int pProjectId, int pScreenNumber, ref ArrayList objList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetOperandRectList(pScreenNumber, ref objList);
        //		}
        //	}

        //	public bool GetOrientation(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).Orientation);
        //		return flag;
        //	}

        //	public bool GetOverlappingFlag(int pProjectId)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsOverlappingAllowed);
        //		return flag;
        //	}

        //	public void GetPercentage(float pPercentage)
        //	{
        //		this._applicationDownloadPercentage(pPercentage);
        //	}

        //	public void GetPLCModuleInfo(int pProjectID, ref CommonConstants.PlcModuleHeaderInfo objHeaderInfo, ref ArrayList objListModuleInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetPLCModuleInfo(ref objHeaderInfo, ref objListModuleInfo);
        //		}
        //	}

        //	public List<string> GetPopUpScreenNamesList(int pProjectID)
        //	{
        //		List<string> strs = new List<string>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			strs = ((Project)this._appProjects[projectIndex]).GetPopUpScreenNamesList();
        //		}
        //		return strs;
        //	}

        //	public List<int> GetPopUpScreenNumbersList(int pProjectID)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((Project)this._appProjects[projectIndex]).GetPopUpScreenNumbersList();
        //		}
        //		return nums;
        //	}

        //	public List<string> GetPowerOnLadderBlockNames(int pProjectId)
        //	{
        //		List<string> strs = new List<string>();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			strs = (this._appProjects[projectIndex] as ProjectPrizm3).GetPowerOnLadderBlockNames();
        //		}
        //		return strs;
        //	}

        //	public List<int> GetPowerOnTaskIDList(int pProjectID)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPowerOnTaskIDList();
        //		}
        //		return nums;
        //	}

        //	public List<int> GetPressedTaskIDList(int pScreenNumber, int pProjectID, int pShapeIndex)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPressedTaskIDList(pScreenNumber, pShapeIndex);
        //		}
        //		return nums;
        //	}

        //	public List<int> GetPressTaskIDList(int pScreenNumber, int pProjectID, int pShapeIndex)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPressTaskIDList(pScreenNumber, pShapeIndex);
        //		}
        //		return nums;
        //	}

        //	public int GetPrevProductID(int pProjectId)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		num = (projectIndex == -1 ? 0 : ((ProjectPrizm3)this._appProjects[projectIndex]).PrevProductID);
        //		return num;
        //	}

        //	public byte GetPrintDuration(int pProjectId)
        //	{
        //		byte printDuration = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			printDuration = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPrintDuration();
        //		}
        //		return printDuration;
        //	}

        //	public CommonConstants.PrinterPortSettings GetPrinterPortSettings(Port pPort, int pProjectID)
        //	{
        //		CommonConstants.PrinterPortSettings printerPortSettings = new CommonConstants.PrinterPortSettings();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			printerPortSettings = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPrinterPortSettings(pPort);
        //		}
        //		return printerPortSettings;
        //	}

        //	public List<CommonConstants.PrintPropertiesInfo> GetPrintPropertiesInformation(int pProjectId)
        //	{
        //		List<CommonConstants.PrintPropertiesInfo> printPropertiesInfos = new List<CommonConstants.PrintPropertiesInfo>();
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			printPropertiesInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPrintPropertiesInformation();
        //		}
        //		return printPropertiesInfos;
        //	}

        //	public List<CommonConstants.PrintTagsInformation> GetPrintTagsInformation(int pProjectId, int GroupIndex)
        //	{
        //		List<CommonConstants.PrintTagsInformation> printTagsInformations = new List<CommonConstants.PrintTagsInformation>();
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			printTagsInformations = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPrintTagsInformation(GroupIndex);
        //		}
        //		return printTagsInformations;
        //	}

        //	public List<CommonConstants.PrintTagsInformation> GetPrintTagsInformationExternal(int pProjectId, int GroupIndex)
        //	{
        //		List<CommonConstants.PrintTagsInformation> printTagsInformations = new List<CommonConstants.PrintTagsInformation>();
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			printTagsInformations = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPrintTagsInformationExternal(GroupIndex);
        //		}
        //		return printTagsInformations;
        //	}

        //	public List<CommonConstants.PrizmUnitInformation> GetPrizmUnitsettings(int pProjectID, int piProductId)
        //	{
        //		List<CommonConstants.PrizmUnitInformation> prizmUnitInformations = new List<CommonConstants.PrizmUnitInformation>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			prizmUnitInformations = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPrizmUnitsettings(piProductId);
        //		}
        //		return prizmUnitInformations;
        //	}

        //	public byte GetPrizmVersion(int pProjectId)
        //	{
        //		byte prizmVersion;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex == -1)
        //		{
        //			prizmVersion = 0;
        //		}
        //		else
        //		{
        //			prizmVersion = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPrizmVersion();
        //		}
        //		return prizmVersion;
        //	}

        //	public int GetProductID(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? -1 : ((ProjectPrizm3)this._appProjects[projectIndex]).GetProductID());
        //		return num;
        //	}

        //	public bool GetProductInfo(int pProjectID, ref byte[] s_ProductInfo, int pProductID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).GetProductInfo(ref s_ProductInfo, pProductID));
        //		return flag;
        //	}

        //	public bool GetProductInfo(int pProjectID, ref byte[] s_ProductInfo, int pProductID, string Mode, string SEPort)
        //	{
        //		bool flag;
        //		string mode = Mode;
        //		string sEPort = SEPort;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).GetProductInfo(ref s_ProductInfo, pProductID, mode, sEPort));
        //		return flag;
        //	}

        //	public string GetProjectAuthor(int pProjectID)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).ProjectAuthor);
        //		return str;
        //	}

        //	public string GetProjectDateLastEdited(int pProjectID)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).DateLastEdited);
        //		return str;
        //	}

        //	public string GetProjectDescription(int pProjectID)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).ProjectDescription);
        //		return str;
        //	}

        //	public int GetProjectID(int piProjectIndex)
        //	{
        //		if (piProjectIndex > this._appProjects.Count - 1)
        //		{
        //			piProjectIndex = this._appProjects.Count - 1;
        //		}
        //		return ((Project)this._appProjects[piProjectIndex]).ProjectID;
        //	}

        private int GetProjectIndex(int piProjectID)
        {
            int num;
            int num1 = 0;
            while (true)
            {
                if (num1 >= this._appProjects.Count)
                {
                    num = -1;
                    break;
                }
                //else if ((this._appProjects[num1]).ProjectID != piProjectID)
                //{
                //    num1++;
                //}
                else
                {
                    num = num1;
                    break;
                }
            }
            return num;
        }

        //	public CommonConstants.MemoryStatus GetProjectMemoryStatus(string _strprizmMDISelProjectNameWithPath, int pProjectId)
        //	{
        //		CommonConstants.MemoryStatus projectMemoryStatus = new CommonConstants.MemoryStatus();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			projectMemoryStatus = ((ProjectPrizm3)this._appProjects[projectIndex]).GetProjectMemoryStatus(_strprizmMDISelProjectNameWithPath);
        //		}
        //		return projectMemoryStatus;
        //	}

        //	public NodeManager GetProjectNodeManagerInfo(int pProjectID)
        //	{
        //		NodeManager nodeManager = new NodeManager();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeManager = ((Project)this._appProjects[projectIndex]).GetProjectNodeManagerInfo();
        //		}
        //		return nodeManager;
        //	}

        //	public string GetProjectPassword(int pProjectID)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).ProjectPassword);
        //		return str;
        //	}

        //	public string GetProjectPath(int pProjectID)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).ProjectPath);
        //		return str;
        //	}

        //	public TagManager GetProjectTagManagerInfo(int pProjectID)
        //	{
        //		TagManager tagManager = new TagManager();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			tagManager = ((Project)this._appProjects[projectIndex]).GetProjectTagManagerInfo();
        //		}
        //		return tagManager;
        //	}

        //	public string GetProjectTimeLastEdited(int pProjectID)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).TimeLastEdited);
        //		return str;
        //	}

        //	public string GetProjectTitle(int pProjectID)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).ProjectTitle);
        //		return str;
        //	}

        //	public ProjectType GetProjectType(int pProjectID)
        //	{
        //		ProjectType projectType;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		projectType = (projectIndex == -1 ? ProjectType.Invalid : ((Project)this._appProjects[projectIndex]).Projecttype);
        //		return projectType;
        //	}

        //	public List<CommonConstants.PropertyGridNodeInfo> GetPropertyGridNodeInfo(int piProjectId)
        //	{
        //		List<CommonConstants.PropertyGridNodeInfo> propertyGridNodeInfos = new List<CommonConstants.PropertyGridNodeInfo>();
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			propertyGridNodeInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPropertyGridNodeInfo();
        //		}
        //		return propertyGridNodeInfos;
        //	}

        //	public List<CommonConstants.PropertyGridRegCoilType> GetPropertyGridRegisterCoilType(int piProjectId)
        //	{
        //		List<CommonConstants.PropertyGridRegCoilType> propertyGridRegCoilTypes = new List<CommonConstants.PropertyGridRegCoilType>();
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			propertyGridRegCoilTypes = ((ProjectPrizm3)this._appProjects[projectIndex]).GetPropertyGridRegCoilTypeList();
        //		}
        //		return propertyGridRegCoilTypes;
        //	}

        //	public ArrayList GetProtocol(int pProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetProtocol();
        //		}
        //		return arrayLists;
        //	}

        //	public List<CommonConstants.NodeInfo> GetProtocolInformation(int pProjectID)
        //	{
        //		List<CommonConstants.NodeInfo> nodeInfos = new List<CommonConstants.NodeInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetProtocolInformation();
        //		}
        //		return nodeInfos;
        //	}

        //	public bool GetProtocolLastAdderess(int pProjectID, byte pbtPort, out int piLastAdderess)
        //	{
        //		bool protocolLastAdderess = false;
        //		piLastAdderess = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			protocolLastAdderess = ((ProjectPrizm3)this._appProjects[projectIndex]).GetProtocolLastAdderess(pbtPort, out piLastAdderess);
        //		}
        //		return protocolLastAdderess;
        //	}

        //	public string GetProtocolName(int pProjectID, byte pbtPort)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).GetProtocolName(pbtPort));
        //		return str;
        //	}

        //	public byte GetProtocolonPort(int piProjectID, byte pbtPort)
        //	{
        //		byte protocolonPort;
        //		int projectIndex = this.GetProjectIndex(piProjectID);
        //		if (projectIndex == -1)
        //		{
        //			protocolonPort = 0;
        //		}
        //		else
        //		{
        //			protocolonPort = ((ProjectPrizm3)this._appProjects[projectIndex]).GetProtocolonPort(pbtPort);
        //		}
        //		return protocolonPort;
        //	}

        //	public ActionID GetRedoActionID(int pProjectID, int piScreenNumber)
        //	{
        //		ActionID actionID;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		actionID = (projectIndex == -1 ? ActionID.None : ((Project)this._appProjects[projectIndex]).GetRedoActionID(piScreenNumber));
        //		return actionID;
        //	}

        //	public List<int> GetReleasedTaskIDList(int pScreenNumber, int pProjectID, int pShapeIndex)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetReleasedTaskIDList(pScreenNumber, pShapeIndex);
        //		}
        //		return nums;
        //	}

        //	public void GetRightClickedPt_LineRungRectArea(int pProjectID, int pScreenNumber, int which, ref System.Drawing.Rectangle objRect)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).GetRightClickedPt_LineRungRectArea(pScreenNumber, which, ref objRect);
        //		}
        //	}

        //	public ClassList.Screen GetScreenDataForHistorical(int pProjectID, int ScreenNumber)
        //	{
        //		ClassList.Screen screen = new ClassList.Screen();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			screen = ((ProjectPrizm3)this._appProjects[projectIndex]).GetScreenDataForHistorical(ScreenNumber);
        //		}
        //		return screen;
        //	}

        //	public void GetScreenDetails(int pProjectId, ref ArrayList objList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetScreenDetails(ref objList);
        //		}
        //	}

        //	public List<CommonConstants.ScreenInfo> GetScreenInformation(int pProjectID)
        //	{
        //		List<CommonConstants.ScreenInfo> screenInfos = new List<CommonConstants.ScreenInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			screenInfos = ((Project)this._appProjects[projectIndex]).GetScreenInformation();
        //		}
        //		return screenInfos;
        //	}

        //	public CommonConstants.ScreenInfo GetScreenInformation(int pProjectID, int piScreenNo)
        //	{
        //		CommonConstants.ScreenInfo screenInformation = new CommonConstants.ScreenInfo();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			screenInformation = ((Project)this._appProjects[projectIndex]).GetScreenInformation(piScreenNo);
        //		}
        //		return screenInformation;
        //	}

        //	public List<CommonConstants.ScreenInformation> GetScreenInformationForPrinting(int pProjectID)
        //	{
        //		List<CommonConstants.ScreenInformation> screenInformations = new List<CommonConstants.ScreenInformation>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			screenInformations = ((ProjectPrizm3)this._appProjects[projectIndex]).GetScreenInformation();
        //		}
        //		return screenInformations;
        //	}

        //	public List<GlobalKeys> GetScreenKeys(int pProjectID, ushort puiScreenNumber)
        //	{
        //		List<GlobalKeys> globalKeys = new List<GlobalKeys>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			globalKeys = ((ProjectPrizm3)this._appProjects[projectIndex]).GetScreenKeys(puiScreenNumber);
        //		}
        //		return globalKeys;
        //	}

        //	public List<GlobalKeys> GetScreenKeysList(int pProjectID, int pScreenNumber)
        //	{
        //		List<GlobalKeys> globalKeys = new List<GlobalKeys>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			globalKeys = ((ProjectPrizm3)this._appProjects[projectIndex]).GetScreenKeysList(pScreenNumber);
        //		}
        //		return globalKeys;
        //	}

        //	public List<CommonConstants.ScreenLadderTaskInfo> GetScreenLadderTaskInformation(int pProjectId)
        //	{
        //		List<CommonConstants.ScreenLadderTaskInfo> screenLadderTaskInfos = new List<CommonConstants.ScreenLadderTaskInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			screenLadderTaskInfos = (this._appProjects[projectIndex] as ProjectPrizm3).GetScreenLadderInfo();
        //		}
        //		return screenLadderTaskInfos;
        //	}

        //	public CommonConstants.ScreenMemoryStatus GetScreenMemoryStatus(int piProjectId, ushort pusScreenNumber)
        //	{
        //		CommonConstants.ScreenMemoryStatus screenMemoryStatus = new CommonConstants.ScreenMemoryStatus();
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			screenMemoryStatus = ((ProjectPrizm3)this._appProjects[projectIndex]).GetScreenMemoryStatus(pusScreenNumber);
        //		}
        //		return screenMemoryStatus;
        //	}

        //	public List<string> GetScreenNamesList(int pProjectID)
        //	{
        //		List<string> strs = new List<string>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			strs = ((Project)this._appProjects[projectIndex]).GetScreenNamesList();
        //		}
        //		return strs;
        //	}

        //	public void GetScreenNamesList(int pProjectID, ref List<string> arrScreenNamesList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).GetScreenNamesList(ref arrScreenNamesList);
        //		}
        //	}

        //	public int GetScreenNumber(int pProjectID, string name)
        //	{
        //		int num;
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((ProjectPrizm3)this._appProjects[projectIndex]).GetScreenNumber(name));
        //		return num;
        //	}

        //	public List<int> GetScreenNumbersList(int pProjectID)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((Project)this._appProjects[projectIndex]).GetScreenNumbersList();
        //		}
        //		return nums;
        //	}

        //	public int GetScreenObjectCount(int pProjectID, string pstrScreenName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		int screenObjectCount = 0;
        //		if (projectIndex != -1)
        //		{
        //			screenObjectCount = ((Project)this._appProjects[projectIndex]).GetScreenObjectCount(pstrScreenName);
        //		}
        //		return screenObjectCount;
        //	}

        //	public ArrayList GetScreenObjectList(int pProjectID, string pstrScreenName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		ArrayList arrayLists = new ArrayList();
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetScreenObjectList(pstrScreenName);
        //		}
        //		return arrayLists;
        //	}

        //	public void GetScreenSaverSettings(int pProjectID, ref CommonConstants.ScreenSaverSettings objSettings)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetScreenSaverSettings(ref objSettings);
        //		}
        //	}

        //	public bool GetScreenSaverStatus(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).EnableScreenSaver);
        //		return flag;
        //	}

        //	public ushort GetScreenSaverTime(int pProjectID)
        //	{
        //		ushort screenSaverTime;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex == -1)
        //		{
        //			screenSaverTime = 0;
        //		}
        //		else
        //		{
        //			screenSaverTime = ((ProjectPrizm3)this._appProjects[projectIndex]).ScreenSaverTime;
        //		}
        //		return screenSaverTime;
        //	}

        //	public ArrayList GetScreenTagInformation(int pProjectID, string pstrScreenName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		ArrayList arrayLists = new ArrayList();
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetScreenTagInformation(pstrScreenName);
        //		}
        //		return arrayLists;
        //	}

        //	public ArrayList GetScreenTagInformation(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		ArrayList arrayLists = new ArrayList();
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetScreenTagInformation();
        //		}
        //		return arrayLists;
        //	}

        //	public int GetSelectedObjectCount(int pProjectIndex, int piScreenNumber)
        //	{
        //		return 0;
        //	}

        //	public ArrayList GetSelectedObjectList(int pProjectID, int piScreenNumber)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetSelectedObjectList(piScreenNumber);
        //		}
        //		return arrayLists;
        //	}

        //	public List<ClassList.Screen> GetSelectedScreensInformation(int pProjectID, List<int> pScreenNumberList)
        //	{
        //		List<ClassList.Screen> screens = new List<ClassList.Screen>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			screens = ((Project)this._appProjects[projectIndex]).GetSelectedScreensInformation(pScreenNumberList);
        //		}
        //		return screens;
        //	}

        //	public CommonConstants.UniversalSerialDriver GetSerialDriverStructure(CommonConstants.NodeInfo pobjNodeInfo, int pProjectID)
        //	{
        //		CommonConstants.UniversalSerialDriver universalSerialDriver;
        //		CommonConstants.UniversalSerialDriver universalSerialDriver1 = new CommonConstants.UniversalSerialDriver();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		universalSerialDriver = (projectIndex == -1 ? universalSerialDriver1 : ((ProjectPrizm3)this._appProjects[projectIndex]).GetSerialDriverStructure(pobjNodeInfo));
        //		return universalSerialDriver;
        //	}

        //	public CommonConstants.UniversalSerialDriverIEC GetSerialDriverStructureIEC(CommonConstants.NodeInfo pobjNodeInfo, int pProjectID)
        //	{
        //		CommonConstants.UniversalSerialDriverIEC universalSerialDriverIEC;
        //		CommonConstants.UniversalSerialDriverIEC universalSerialDriverIEC1 = new CommonConstants.UniversalSerialDriverIEC();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		universalSerialDriverIEC = (projectIndex == -1 ? universalSerialDriverIEC1 : ((ProjectPrizm3)this._appProjects[projectIndex]).GetSerialDriverStructureIEC(pobjNodeInfo));
        //		return universalSerialDriverIEC;
        //	}

        //	public void GetSerialIOInfo(int Type, int pProjectID, ref CommonConstants.PlcModuleHeaderInfo objHeaderInfo, ref ArrayList objListModuleInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetSerialIOInfo(Type, ref objHeaderInfo, ref objListModuleInfo);
        //		}
        //	}

        //	public ArrayList GetSFCBlockList(int pProjectID, int type)
        //	{
        //		ArrayList arrayLists;
        //		ArrayList arrayLists1 = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		arrayLists = (projectIndex == -1 ? arrayLists1 : ((ProjectPrizm3)this._appProjects[projectIndex]).GetSFCBlockList(type));
        //		return arrayLists;
        //	}

        //	public bool GetSnapToGridFlag(int pProjectId)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).GetSnapToGrid());
        //		return flag;
        //	}

        //	public void GetSubroutineBlockNamesList(ref ArrayList prizmMDIBlockNameList, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		((Project)this._appProjects[projectIndex]).GetSubroutineBlockNamesList(ref prizmMDIBlockNameList);
        //	}

        //	public ArrayList GetSystemMemoryData(int pProjectID)
        //	{
        //		ArrayList arrayLists;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		arrayLists = (projectIndex == -1 ? new ArrayList() : ((ProjectPrizm3)this._appProjects[projectIndex]).GetSystemMemoryData());
        //		return arrayLists;
        //	}

        //	public string GetTagAddress(int pProjectID, int piTagID)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).GetTagAddress(piTagID));
        //		return str;
        //	}

        //	public void GetTagAddressListForCommunication(int pProjectID, int pScreenNumber, System.Drawing.Rectangle rectClient, ref ArrayList objArraylist)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).GetTagAddressListForCommunication(pScreenNumber, rectClient, ref objArraylist);
        //		}
        //	}

        //	public ArrayList GetTagAssociatedScreenList(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		ArrayList arrayLists = new ArrayList();
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetTagAssociatedScreenNames();
        //		}
        //		return arrayLists;
        //	}

        //	public SortableBindingList<stTagGroup> GetTagGroups(int pProjectID)
        //	{
        //		SortableBindingList<stTagGroup> sortableBindingList = new SortableBindingList<stTagGroup>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			sortableBindingList = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTagGroups();
        //		}
        //		return sortableBindingList;
        //	}

        //	public int GetTagID(int pProjectID, string pstrTagAddress)
        //	{
        //		int tagID = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			tagID = ((Project)this._appProjects[projectIndex]).GetTagID(pstrTagAddress);
        //		}
        //		return tagID;
        //	}

        //	public int GetTagID(int pProjectID, string pstrTagAddress, CommonConstants.LadderOperandInfo objOperandInfo)
        //	{
        //		int tagID = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			tagID = ((Project)this._appProjects[projectIndex]).GetTagID(pstrTagAddress, objOperandInfo);
        //		}
        //		return tagID;
        //	}

        //	public int GetTagIDByTagName(int pProjectID, string pstrTagName)
        //	{
        //		int tagIDByTagName = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			tagIDByTagName = ((Project)this._appProjects[projectIndex]).GetTagIDByTagName(pstrTagName);
        //		}
        //		return tagIDByTagName;
        //	}

        //	public int GetTagIDByTagNameforAnimation(int pProjectID, string pstrTagName)
        //	{
        //		int tagIDByTagNameForAnimation = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			tagIDByTagNameForAnimation = ((Project)this._appProjects[projectIndex]).GetTagIDByTagNameForAnimation(pstrTagName);
        //		}
        //		return tagIDByTagNameForAnimation;
        //	}

        //	public ArrayList GetTagIDList(int pProjectID, ArrayList pTagInfoList)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetTagIDList(pTagInfoList);
        //		}
        //		return arrayLists;
        //	}

        //	public CommonConstants.Prizm3TagStructure getTagInfoByTagName(int pProjectID, string pTagnm)
        //	{
        //		CommonConstants.Prizm3TagStructure prizm3TagStructure;
        //		CommonConstants.Prizm3TagStructure prizm3TagStructure1 = new CommonConstants.Prizm3TagStructure();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		prizm3TagStructure = (projectIndex == -1 ? prizm3TagStructure1 : ((ProjectPrizm3)this._appProjects[projectIndex]).getTagInfoByTagName(pTagnm));
        //		return prizm3TagStructure;
        //	}

        //	public CommonConstants.Prizm3TagStructure GetTagInformation(int pProjectID, string pstrTagAddress, string pstrNodeName, byte pbtTagBytes, byte pbtHighLow)
        //	{
        //		CommonConstants.Prizm3TagStructure tagInformation = new CommonConstants.Prizm3TagStructure();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			tagInformation = ((Project)this._appProjects[projectIndex]).GetTagInformation(pstrTagAddress, pstrNodeName, pbtTagBytes, pbtHighLow);
        //		}
        //		return tagInformation;
        //	}

        //	public ArrayList GetTagInformation(int pProjectID, int piNodeID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetTagInformation(piNodeID);
        //		}
        //		return arrayLists;
        //	}

        //	public ArrayList GetTagInformation(int pProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetTagInfo();
        //		}
        //		return arrayLists;
        //	}

        //	public ArrayList GetTagInformation()
        //	{
        //		return ((Project)this._appProjects[0]).GetTagInfo();
        //	}

        //	public ArrayList GetTagInformation_download()
        //	{
        //		return ((Project)this._appProjects[0]).GetTagInfo_download();
        //	}

        //	public ArrayList GetTagInformation_Download(int pProjectID, int piNodeID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetTagInformation_Download(piNodeID);
        //		}
        //		return arrayLists;
        //	}

        //	public List<CommonConstants.Prizm3TagStructure> GetTagInformationByNode(int piProjectId, int piNodeId)
        //	{
        //		List<CommonConstants.Prizm3TagStructure> prizm3TagStructures = new List<CommonConstants.Prizm3TagStructure>();
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			prizm3TagStructures = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTagInformationByNode(piNodeId);
        //		}
        //		return prizm3TagStructures;
        //	}

        //	public CommonConstants.Prizm3TagStructure GetTagInformationByTagId(int pProjectId, int piTagId)
        //	{
        //		CommonConstants.Prizm3TagStructure tagInformation = new CommonConstants.Prizm3TagStructure();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			tagInformation = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTagInformation(piTagId);
        //		}
        //		return tagInformation;
        //	}

        //	public string GetTagName(int pProjectID, int piTagID, int piBlockNumber)
        //	{
        //		string str;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		str = (projectIndex == -1 ? "" : ((Project)this._appProjects[projectIndex]).GetTagName(piTagID, piBlockNumber));
        //		return str;
        //	}

        //	private string GetTagName(string pstrTagAddress, int piProjectID)
        //	{
        //		string str = "";
        //		ArrayList arrayLists = new ArrayList();
        //		ArrayList arrayLists1 = new ArrayList();
        //		ArrayList tagNames = new ArrayList();
        //		arrayLists = this.GetTagInformation(piProjectID);
        //		arrayLists1 = this.SortCapCharData(arrayLists);
        //		tagNames = this.GetTagNames(piProjectID);
        //		arrayLists.TrimToSize();
        //		arrayLists1.TrimToSize();
        //		tagNames.TrimToSize();
        //		int count = arrayLists1.Count;
        //		int num = arrayLists1.IndexOf(pstrTagAddress);
        //		if (num == -1)
        //		{
        //			arrayLists1 = this.SortSmallCharData(arrayLists);
        //			num = arrayLists1.IndexOf(pstrTagAddress);
        //			if (num != -1)
        //			{
        //				str = tagNames[count + num - 1].ToString();
        //			}
        //		}
        //		else
        //		{
        //			str = tagNames[num].ToString();
        //		}
        //		return str;
        //	}

        //	public string GetTagName(int piTagId, int pProjectID)
        //	{
        //		string tagName = "";
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			tagName = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTagName(piTagId);
        //		}
        //		return tagName;
        //	}

        //	public ArrayList GetTagNames(int pProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetTagNames();
        //		}
        //		return arrayLists;
        //	}

        //	public int GetTagTableSize(int pProjectID)
        //	{
        //		int tagTableSize = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			tagTableSize = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTagTableSize();
        //		}
        //		return tagTableSize;
        //	}

        //	public ArrayList GetTagUsageInformation(int pProjectID, int piTagID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTagUsageInformation(piTagID);
        //		}
        //		arrayLists.TrimToSize();
        //		return arrayLists;
        //	}

        //	public void GetTagUsageInfoStraton(int pProjectID, string BlockName, ref ArrayList parrTagUsageList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetTagUsageInfoStraton(BlockName, ref parrTagUsageList);
        //		}
        //	}

        //	public ArrayList GetTemplateScreenInformation(int pProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTemplateScreenInformation();
        //		}
        //		return arrayLists;
        //	}

        //	public int[] GettimeTagsTagId(int pProjectId)
        //	{
        //		int[] numArray = new int[12];
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			numArray = ((ProjectPrizm3)this._appProjects[projectIndex]).GettimeTagsTagId();
        //		}
        //		return numArray;
        //	}

        //	public int GetTotalBaseScreenCount(int pProjectID)
        //	{
        //		int totalBaseScreenCount = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			totalBaseScreenCount = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTotalBaseScreenCount();
        //		}
        //		return totalBaseScreenCount;
        //	}

        //	public int GetTotalEmailScreens(int pProjectID)
        //	{
        //		return 0;
        //	}

        //	public int GetTotalHIOBlock()
        //	{
        //		return this._appHIOLibrary.TotalBlocks;
        //	}

        //	public int GetTotalNoOfNodes(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((Project)this._appProjects[projectIndex]).GetTotalNoOfNodes());
        //		return num;
        //	}

        //	public int GetTotalNumberOfBlocks(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((Project)this._appProjects[projectIndex]).GetTotalNumberOfBlocks());
        //		return num;
        //	}

        //	public int GetTotalNumberOfScreens(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((Project)this._appProjects[projectIndex]).GetTotalNumberOfScreens());
        //		return num;
        //	}

        //	public int GetTotalNumberOfTags(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((Project)this._appProjects[projectIndex]).GetTotalNumberOfTags());
        //		return num;
        //	}

        //	public int GetTotalNumberOfUsers(int pProjectID)
        //	{
        //		int num;
        //		num = (this.GetProjectIndex(pProjectID) == -1 ? 0 : Convert.ToInt32(this._appUserManager.TotalUsers));
        //		return num;
        //	}

        //	public int GetTotalScreens(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((Project)this._appProjects[projectIndex]).GetTotalScreens());
        //		return num;
        //	}

        //	public int GetTotalTemplateScreenCount(int pProjectID)
        //	{
        //		int totalTemplateScreenCount = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			totalTemplateScreenCount = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTotalTemplateScreenCount();
        //		}
        //		return totalTemplateScreenCount;
        //	}

        //	public int GetTotalWebScreens(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((Project)this._appProjects[projectIndex]).GetTotalWebScreens());
        //		return num;
        //	}

        //	public Size GetTouchGridSize(int pProjectId)
        //	{
        //		Size touchGridSize = new Size();
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			touchGridSize = ((ProjectPrizm3)this._appProjects[projectIndex]).GetTouchGridSize();
        //		}
        //		return touchGridSize;
        //	}

        //	public string GetUndefinedTagAddress(int pProjectID, int piUndefinedTagID)
        //	{
        //		string undefinedTagAddress = "";
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			undefinedTagAddress = ((Project)this._appProjects[projectIndex]).GetUndefinedTagAddress(piUndefinedTagID);
        //		}
        //		return undefinedTagAddress;
        //	}

        //	public int GetUndefinedTagID(int pProjectID, string pstrTagAddress)
        //	{
        //		int undefinedTagID = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			undefinedTagID = ((Project)this._appProjects[projectIndex]).GetUndefinedTagID(pstrTagAddress);
        //		}
        //		return undefinedTagID;
        //	}

        //	public ArrayList GetUndefinedTagList(int pProjectID)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			arrayLists = ((Project)this._appProjects[projectIndex]).GetUndefinedTagList();
        //		}
        //		return arrayLists;
        //	}

        //	public ActionID GetUndoActionID(int pProjectID, int pScreenNumber)
        //	{
        //		ActionID actionID;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		actionID = (projectIndex == -1 ? ActionID.None : ((Project)this._appProjects[projectIndex]).GetUndoActionID(pScreenNumber));
        //		return actionID;
        //	}

        //	public List<CommonConstants.NodeInfo> GetUniqueProtocolInfo(int pProjectID)
        //	{
        //		List<CommonConstants.NodeInfo> nodeInfos = new List<CommonConstants.NodeInfo>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nodeInfos = ((ProjectPrizm3)this._appProjects[projectIndex]).GetUniqueProtocolInfo();
        //		}
        //		return nodeInfos;
        //	}

        //	public void GetUploadedEthernetSettings(int pProjectId, string FileName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetUploadedEthernetSettings(FileName);
        //		}
        //	}

        //	public int GetUsedTagCount(int pProjectID)
        //	{
        //		int num;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		num = (projectIndex == -1 ? 0 : ((Project)this._appProjects[projectIndex]).GetUsedTagCount());
        //		return num;
        //	}

        //	public ArrayList GetUsedTagList(int pProjectID, ArrayList _usedTagsList)
        //	{
        //		ArrayList arrayLists;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		arrayLists = (projectIndex == -1 ? ((ProjectPrizm3)this._appProjects[projectIndex]).GetDefaultTagList(_usedTagsList) : ((ProjectPrizm3)this._appProjects[projectIndex]).GetDefaultTagList(_usedTagsList));
        //		return arrayLists;
        //	}

        //	public ArrayList GetUserAccessLevel()
        //	{
        //		return this._appUserManager.UserAccessLevel;
        //	}

        //	public ArrayList GetUserAdditionalRights()
        //	{
        //		return this._appUserManager.UserAdditionalRights;
        //	}

        //	public CommonConstants.UserData GetUserData(string puserName)
        //	{
        //		CommonConstants.UserData userData = this._appUserManager.GetUserData(this._appUserManager.FindUser(puserName));
        //		return userData;
        //	}

        //	public CommonConstants.UserData GetUserData(int pUserID)
        //	{
        //		CommonConstants.UserData userData = this._appUserManager.GetUserData(this._appUserManager.FindUser(pUserID));
        //		return userData;
        //	}

        //	public ArrayList GetUserID()
        //	{
        //		return this._appUserManager.UserIDList;
        //	}

        //	public ArrayList GetUserName()
        //	{
        //		return this._appUserManager.UserName;
        //	}

        //	public ArrayList GetUserType()
        //	{
        //		return this._appUserManager.UserType;
        //	}

        //	public void GetUSSDriverSettings(int pProjectID, ref CommonConstants.USSDriverInfo objUSSSettings)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).GetUSSDriverSettings(ref objUSSSettings);
        //		}
        //	}

        //	public string GetValidUserName(int pUserID)
        //	{
        //		CommonConstants.UserData userData = new CommonConstants.UserData();
        //		userData = this.GetUserData(pUserID);
        //		return userData._userName;
        //	}

        //	public List<int> GetWhileShowingTaskIDList(int pScreenNumber, int pProjectID)
        //	{
        //		List<int> nums = new List<int>();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			nums = ((ProjectPrizm3)this._appProjects[projectIndex]).GetWhileShowingTaskIDList(pScreenNumber);
        //		}
        //		return nums;
        //	}

        //	public void Gwy_AddBlock(int pProjectId, CommonConstants.GwyBlockInfo objBlockInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_AddBlock(objBlockInfo);
        //		}
        //	}

        //	public void Gwy_AddNode(int pProjectId, int ProductID, int Port, int Plc_code, int Model, int nodeAddress, string strPortName, string strNodeName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_AddNode(ProductID, Port, Plc_code, Model, nodeAddress, strPortName, strNodeName);
        //	}

        //	public void Gwy_ClearGlobalTaskList(int pProjectId, ref ArrayList tagIdList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_ClearGlobalTaskList(ref tagIdList);
        //		}
        //	}

        //	public void Gwy_ClearPowerOnTaskList(int pProjectId, ref ArrayList tagIdList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_ClearPowerOnTaskList(ref tagIdList);
        //		}
        //	}

        //	public void Gwy_CreateDnld_Info(int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_CreateDnld_Info();
        //	}

        //	public void Gwy_GetBlockInfo(int pProjectId, int BlockIndex, ref CommonConstants.GwyBlockInfo objblockInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_GetBlockInfo(BlockIndex, ref objblockInfo);
        //		}
        //	}

        //	public List<CommonConstants.GwyBlockInfo> Gwy_GetBlockList(int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		return ((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_GetBlockList();
        //	}

        //	public void Gwy_GetCwInfo(int pProjectId, int Index, ref CommonConstants.GwyContolWordInfo objCwInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_GetCwInfo(Index, ref objCwInfo);
        //		}
        //	}

        //	public List<CommonConstants.GwyContolWordInfo> Gwy_GetCwList(int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		return ((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_GetCwList();
        //	}

        //	public CommonConstants.GwyErrorBitInfo Gwy_GetErrorBitInfo(int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		return ((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_GetErrorBitInfo();
        //	}

        //	public void Gwy_InsertBlock(int pProjectId, int Index, CommonConstants.GwyBlockInfo objBlockInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_InsertBlock(Index, objBlockInfo);
        //		}
        //	}

        //	public void Gwy_RemoveBlock(int pProjectId, int Index)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_RemoveBlock(Index);
        //		}
        //	}

        //	public void Gwy_SetBlockInfo(int pProjectId, int BlockIndex, CommonConstants.GwyBlockInfo objblockInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_SetBlockInfo(BlockIndex, objblockInfo);
        //		}
        //	}

        //	public void Gwy_SetCwInfo(int pProjectId, int Index, CommonConstants.GwyContolWordInfo objCwInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_SetCwInfo(Index, objCwInfo);
        //		}
        //	}

        //	public void Gwy_SetErrorBitInfo(int pProjectId, CommonConstants.GwyErrorBitInfo objbitInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_SetErrorBitInfo(objbitInfo);
        //	}

        //	public void Gwy_UpdateGlobalTaskIDList(int pProjectId, int taskID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_UpdateGlobalTaskIDList(taskID);
        //		}
        //	}

        //	public void Gwy_UpdateNodeAddress(int pProjectId, int OldNodeAdd, int NewNodeAdd, int Port)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_UpdateNodeAddress(OldNodeAdd, NewNodeAdd, Port);
        //	}

        //	public void Gwy_UpdatePowerOnTaskIDList(int pProjectId, int taskID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Gwy_UpdatePowerOnTaskIDList(taskID);
        //		}
        //	}

        //	public void HorizontalFlip(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).HorizontalFlip(pScreenNumber);
        //		}
        //	}

        //	public void Import(string filepath, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Import(filepath);
        //		}
        //	}

        //	public void ImportListOfBlockSelected(string strFileName, int pProjectID, ArrayList SelectedBlockNames)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ReadImportInstructionToBlock(strFileName, SelectedBlockNames);
        //		}
        //	}

        //	public bool ImportObjects(int piProjectId, CommonConstants.ImportObjectsInfo pImportObjectsInfo)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		flag = (projectIndex == -1 ? true : ((ProjectPrizm3)this._appProjects[projectIndex]).ImportObjectsInfo(pImportObjectsInfo));
        //		return flag;
        //	}

        //	public bool ImportProjectTask(int pProjectID, FileStream objFileStream, ref CommonConstants.ImportTaskInfo objTaskInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		bool flag = ((ProjectPrizm3)this._appProjects[projectIndex]).ImportProjectTask(objFileStream, ref objTaskInfo);
        //		return flag;
        //	}

        //	public void InitPLCModuleInfo(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).InitPLCModuleInfo();
        //		}
        //	}

        //	public void InitSerialIOInfo(int Type, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).InitSerialIOInfo(Type);
        //		}
        //	}

        //	public void InitSystemMemoryData(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).InitSystemMemoryData();
        //		}
        //	}

        //	public void InsertDeleteRungLine(int pProjectId, int OpType, int screenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).InsertDeleteRungLine(OpType, screenNumber);
        //		}
        //	}

        //	public void InsertTagName(int pProjectID, int piTagNameIndex, string pstrTagName, bool pblFlagUpdate)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).InsertTagName(piTagNameIndex, pstrTagName, pblFlagUpdate);
        //		}
        //	}

        //	public void InsertTagName(int piTagNameIndex, string pstrTagName, bool pblFlagUpdate)
        //	{
        //		((Project)this._appProjects[0]).InsertTagName(piTagNameIndex, pstrTagName, pblFlagUpdate);
        //	}

        //	public bool IsAccelerateScanTime(int pProjectID)
        //	{
        //		bool flag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).IsAccelerateScanTime();
        //		}
        //		return flag;
        //	}

        //	public bool IsAssociatedWithAnyScreen(int pProjectID, int pScreenNumber, ref StringBuilder pListOfAssociatedScreens)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsAssociatedWithAnyScreen(pScreenNumber, ref pListOfAssociatedScreens));
        //		return flag;
        //	}

        //	public bool IsBlockNameDefined(int pProjectID, string strBlockName)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsBlockNameDefined(strBlockName));
        //		return flag;
        //	}

        //	public bool IsBreakPointDefined(int pProjectID, int screenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		return ((ProjectPrizm3)this._appProjects[projectIndex]).IsBreakPointDefined(screenNumber);
        //	}

        //	public bool IsDataEntryObjectAssigned(int piProjectId, int pScreenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsDataEntryObjectAssigned(pScreenNumber));
        //		return flag;
        //	}

        //	public bool IsDataLoggerGroupPresentinTask(int pProjectID, int piGroupNo)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsDataLoggerGroupPresentinTask(piGroupNo));
        //		return flag;
        //	}

        //	public bool IsDataLoggerGroupPresentinTaskExternal(int pProjectID, int piGroupNo)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsDataLoggerGroupPresentinTaskExternal(piGroupNo));
        //		return flag;
        //	}

        //	public bool IsEndInstructionDefined(int pProjectID, ref string BlockName)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsEndInstructionDefined(ref BlockName));
        //		return flag;
        //	}

        //	public bool IsEraseDataLoggerMemory(int pProjectID)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).IsEraseDataLoggerMemory();
        //		}
        //		return flag;
        //	}

        //	public bool IsEv3Product(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsEv3Product());
        //		return flag;
        //	}

        //	public byte IsFlagDirty()
        //	{
        //		byte num;
        //		bool flag = false;
        //		int num1 = 0;
        //		while (num1 < this._appProjects.Count)
        //		{
        //			if (!((Project)this._appProjects[num1]).IsProjectFlagDirty())
        //			{
        //				num1++;
        //			}
        //			else
        //			{
        //				flag = true;
        //				break;
        //			}
        //		}
        //		if (!(!this._appUserManager.DirtyFlag ? true : !flag))
        //		{
        //			num = Convert.ToByte(DirtyFlagType.BOTH);
        //		}
        //		else if (!this._appUserManager.DirtyFlag)
        //		{
        //			num = (!flag ? Convert.ToByte(DirtyFlagType.NONE) : Convert.ToByte(DirtyFlagType.PROJECT));
        //		}
        //		else
        //		{
        //			num = Convert.ToByte(DirtyFlagType.USER);
        //		}
        //		return num;
        //	}

        //	public bool IsFlagDirty(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		return ((Project)this._appProjects[projectIndex]).IsProjectFlagDirty();
        //	}

        //	public bool IsHistoricalTrendPresent(int pProjectId)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 || !(this._appProjects[projectIndex] as ProjectPrizm3).IsHistoricalTrendPresent() ? false : true);
        //		return flag;
        //	}

        //	public bool IsHMIScreenExists(int pProjectId, int screenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsHMIScreenExists(screenNumber));
        //		return flag;
        //	}

        //	public bool IsKeypadWithTagObject_Defined(int pProjectID, int ScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		return ((ProjectPrizm3)this._appProjects[projectIndex]).IsKeypadWithTagObject_Defined(ScreenNumber);
        //	}

        //	public bool IsLadderscreenExists(int pProjectId, int screenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsLadderscreenExists(screenNumber));
        //		return flag;
        //	}

        //	public bool IsLoggingModeKeyTask(int pProjectID, int piGroupNo)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).IsLoggingModeKeyTask(piGroupNo);
        //		}
        //		return flag;
        //	}

        //	public bool IsLoggingModeKeyTaskExternal(int pProjectID, int piGroupNo)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).IsLoggingModeKeyTaskExternal(piGroupNo);
        //		}
        //		return flag;
        //	}

        //	public bool IsNewProject(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsNewProject);
        //		return flag;
        //	}

        //	public bool IsNodeAddressPresent(int pProjectID, ushort pNodeAdd, string pNodeName, byte pPort, byte pPLCCode)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsNodeAddressPresent(pNodeAdd, pNodeName, pPort, pPLCCode));
        //		return flag;
        //	}

        //	public bool IsNodeContainsTag(int pProjectID, string pstrNodeName)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsNodeContainsTag(pstrNodeName));
        //		return flag;
        //	}

        //	public bool IsNodeNamePresent(int pProjectID, string pstrNodeName)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsNodeNamePresent(pstrNodeName));
        //		return flag;
        //	}

        //	public bool IsObjectPresent(int pProjectID, List<int> pilstScreenNumber)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).IsObjectPresent(pilstScreenNumber);
        //		}
        //		return flag;
        //	}

        //	public bool IsOldToshibaInverterNodePresent(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsOldToshibaInverterNodePresent());
        //		return flag;
        //	}

        //	public bool IsPageEmpty(int pProjectID, int ScreenNumber, System.Drawing.Rectangle objRect)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsPageEmpty(ScreenNumber, objRect));
        //		return flag;
        //	}

        //	public bool IsPatternSupported(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).PatternSupported);
        //		return flag;
        //	}

        //	public bool IsPointInObject(int pProjectID, int pScreenNumber, Point Pt, ref int ShapeID, ref ArrayList objArrayList)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsPointInObject(pScreenNumber, Pt, ref ShapeID, ref objArrayList));
        //		return flag;
        //	}

        //	public bool IsPrintingTagsPresent(int pProjectID)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).IsPrintingTagsPresent();
        //		}
        //		return flag;
        //	}

        //	public bool IsPrintingTagsPresent(int pProjectID, int piGroupNo)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).IsPrintingTagsPresent(piGroupNo);
        //		}
        //		return flag;
        //	}

        //	public void IsProjectDirtyFlagUpload(int pProjectID, bool bvalue)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).DirtyFlag = bvalue;
        //		}
        //	}

        //	public bool IsProjectFlagDirty(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsProjectFlagDirty());
        //		return flag;
        //	}

        //	public bool IsProjectPresent(string pstrProjectPathName)
        //	{
        //		bool flag;
        //		int num = 0;
        //		while (true)
        //		{
        //			if (num >= this._appProjects.Count)
        //			{
        //				flag = false;
        //				break;
        //			}
        //			else if (!(((Project)this._appProjects[num]).ProjectPath == pstrProjectPathName))
        //			{
        //				num++;
        //			}
        //			else
        //			{
        //				flag = true;
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public bool IsPtInCommentArea(int pProjectId, int screenNumber, Point pt, ref int runNo, ref System.Drawing.Rectangle rectComment, ref string strComment)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsPtInCommentArea(screenNumber, pt, ref runNo, ref rectComment, ref strComment));
        //		return flag;
        //	}

        //	public bool IsPtInOperandAddressArea(int pProjectId, int screenNumber, Point pt, ref CommonConstants.LadderOperandInfo objLadderOperandInfo)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsPtInOperandAddressArea(screenNumber, pt, ref objLadderOperandInfo));
        //		return flag;
        //	}

        //	public bool IsSameProtocolExceedConvertModel(int pProjectID, string strCopySourceFrom)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsSameProtocolExceedConvertModel(strCopySourceFrom));
        //		return flag;
        //	}

        //	public bool IsScreenAlreadyPresent(int pProjectID, List<ClassList.Screen> pScreenList, ref List<ClassList.Screen> pExistingScreenList)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((Project)this._appProjects[projectIndex]).IsScreenAlreadyPresent(pScreenList, ref pExistingScreenList);
        //		}
        //		return flag;
        //	}

        //	public bool IsScreenFlagDirty(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsScreenFlagDirty());
        //		return flag;
        //	}

        //	public bool IsScreenNamePresent(int piProjectId, string pstrScreenName)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsScreenNamePresent(pstrScreenName));
        //		return flag;
        //	}

        //	public bool IsScreenNamePresent(int piProjectId, string pstrScreenName, int piCurrentScreenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsScreenNamePresent(pstrScreenName, piCurrentScreenNumber));
        //		return flag;
        //	}

        //	public bool IsScreenNumberPresent(int piProjectId, int pintScreenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsScreenNumberPresent(pintScreenNumber));
        //		return flag;
        //	}

        //	public bool IsScreenPresentinProject(int piProjectId, string pstrScreenName, int ScreenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsScreenPresentinProject(pstrScreenName, ScreenNumber));
        //		return flag;
        //	}

        //	public bool IsScreenPresentinProjectProceca(int piProjectId, int ScreenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsScreenPresentinProjectProceca(ScreenNumber));
        //		return flag;
        //	}

        //	public bool IsScreenUsedInAnyTask(int pProjectID, int pScreenNumber)
        //	{
        //		bool flag = false;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag |= ((ProjectPrizm3)this._appProjects[projectIndex]).IsScreenUsedInGlobalKeysTask(pScreenNumber);
        //			flag |= ((ProjectPrizm3)this._appProjects[projectIndex]).IsScreenUsedInScreenTask(pScreenNumber);
        //		}
        //		return flag;
        //	}

        //	public bool IsScreenUsedInAnyTasks(int pProjectID, int pScreenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsScreenUsedInAnyTasks(pScreenNumber));
        //		return flag;
        //	}

        //	public bool IsScreenUsedInPowerOnTask(int pProjectID, int pScreenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsScreenUsedInPowerOnTask(pScreenNumber));
        //		return flag;
        //	}

        //	public bool IsSimulationAlreadyRunning()
        //	{
        //		bool flag;
        //		if (!(this._appSimulationProcess.StartInfo.FileName != ""))
        //		{
        //			flag = false;
        //		}
        //		else if (!this._appSimulationProcess.HasExited)
        //		{
        //			Process[] processesByName = Process.GetProcessesByName("Simulation");
        //			int num = 0;
        //			while (num < (int)processesByName.Length)
        //			{
        //				Process process = processesByName[num];
        //				if ((process.Id != this._appSimulationProcess.Id || !(process.MainModule.FileName == this._appSimulationProcess.MainModule.FileName) ? true : !(process.MainWindowHandle != IntPtr.Zero)))
        //				{
        //					num++;
        //				}
        //				else
        //				{
        //					flag = true;
        //					return flag;
        //				}
        //			}
        //			flag = false;
        //		}
        //		else
        //		{
        //			flag = false;
        //		}
        //		return flag;
        //	}

        //	public bool IsSystemTag(int pProjectID, string pstrTagAddress)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsSystemTag(pstrTagAddress));
        //		return flag;
        //	}

        //	public bool IsTagAddressPresent(int pProjectID, string pstrTagAddress, CommonConstants.Prizm3TagStructure pTagStructure)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsTagAddressPresent(pstrTagAddress, pTagStructure));
        //		return flag;
        //	}

        //	public bool IsTagId_Present(int pProjectID, int TagId)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsTagIdPresent(TagId));
        //		return flag;
        //	}

        //	public bool IsTagNameDownload(int pProjectID)
        //	{
        //		bool flag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			flag = ((ProjectPrizm3)this._appProjects[projectIndex]).IsTagNameDownload();
        //		}
        //		return flag;
        //	}

        //	public bool IsTagNamePresent(int pProjectID, string pstrTagName)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsTagNamePresent(pstrTagName));
        //		return flag;
        //	}

        //	public bool IsTagPresentInHistoricalTrend(int pProjectId, int pTagId)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 || !(this._appProjects[projectIndex] as ProjectPrizm3).IsTagPresentInHistoricalTrend(pTagId) ? false : true);
        //		return flag;
        //	}

        //	public bool IsTagUsedinDataLogger(int pProjectID, int piTagId)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsTagUsedinDataLogger(piTagId));
        //		return flag;
        //	}

        //	public bool IsTagUsedinDataLoggerExternal(int pProjectID, int piTagId)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).IsTagUsedinDataLoggerExternal(piTagId));
        //		return flag;
        //	}

        //	public bool IsUsedTag(int pProjectID, string pstrTagAddress)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsUsedTag(pstrTagAddress));
        //		return flag;
        //	}

        //	public bool IsVariableBlockSize(int pProjectID)
        //	{
        //		bool flag;
        //		DataTable dataTable = new DataTable();
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).IsVariableBlockSize());
        //		return flag;
        //	}

        //	public void KillSimulationExecutable()
        //	{
        //		if (File.Exists(this._appSimulationProcess.StartInfo.FileName) && !this._appSimulationProcess.HasExited)
        //		{
        //			this._appSimulationProcess.Kill();
        //		}
        //	}

        //	public void MakeEqualHeight(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).MakeEqualHeight(pScreenNumber);
        //		}
        //	}

        //	public void MakeEqualSize(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).MakeEqualSize(pScreenNumber);
        //		}
        //	}

        //	public void MakeEqualWidth(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).MakeEqualWidth(pScreenNumber);
        //		}
        //	}

        //	public void MakeGroup(int pProjectID, int piScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).MakeGroup(piScreenNumber);
        //		}
        //	}

        //	public void ModifyBreakPoint(int pProjectID, int screenNumber, int ID, bool shBreak, bool shStep)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).ModifyBreakPoint(screenNumber, ID, shBreak, shStep);
        //	}

        //	public void ModifyTagForBlockSize(int pProjectID, ArrayList objTagIDList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ModifyTagForBlockSize(objTagIDList);
        //		}
        //	}

        //	public void MoveBackword(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).MoveBackword(pScreenNumber);
        //		}
        //	}

        //	public void MoveForword(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).MoveForword(pScreenNumber);
        //		}
        //	}

        //	public void MoveToFront(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).MoveToFront(pScreenNumber);
        //		}
        //	}

        //	public void NewProject(bool pFlag)
        //	{
        //		((Project)this._appProjects[0]).IsNewProject = pFlag;
        //	}

        //	public void NewProject(int pProjectID, bool pFlag)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).IsNewProject = pFlag;
        //		}
        //	}

        //	public int noOfShapesInScreen(int pProjectID, int pScreenNumber)
        //	{
        //		int num = 0;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			num = ((Project)this._appProjects[projectIndex]).noOfShapesInScreen(pScreenNumber);
        //		}
        //		return num;
        //	}

        //	public void OnEthernetDownload(int pProjectID, DownloadData pDownloadData, string pComNoOrIpAddress, int pProductID, string pFileName, int piResponseTimeOut, byte[] _setupFrameMode)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Download(pDownloadData, pComNoOrIpAddress, pFileName, pProductID, piResponseTimeOut, _setupFrameMode);
        //		}
        //	}

        //	public int OnEthernetUpload(int pProjectID, DownloadData pDownloadData, string pComNoOrIpAddress, string pFileName, int pProductID, int pBaudRate, byte pParity, byte pByteSize, byte pStopBits)
        //	{
        //		int num = -1;
        //		Ethernet ethernet = new Ethernet(Convert.ToInt16(pProductID));
        //		ethernet.SetIpAndPort(pComNoOrIpAddress, CommonConstants.ETHERNET_PORT_NUMBER);
        //		ethernet._deviceDownloadEthernetPercentage += new Ethernet.DownloadPercentage(this.GetPercentage);
        //		ethernet._deviceDownloadEthernetStatus += new Ethernet.DownloadStatus(this.serial__deviceDownloadStatus);
        //		ethernet.ResponseTimeOut = pBaudRate;
        //		ethernet.OperationType = CommunicationOperationType.UPLOAD;
        //		if (ethernet.Connect() == 0)
        //		{
        //			CommonConstants.TEMP_DOWNLOAD_FILENAME = pFileName;
        //			num = (ethernet.ReceiveFile(pFileName, Convert.ToInt32(pDownloadData)) != 0 ? -1 : 0);
        //		}
        //		return num;
        //	}

        //	public void OnMouseDown(object pSender, MouseEventArgs pEvent, int pScreenNumber, int pProjectID, Form f)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).OnMouseDown(pSender, pEvent, pScreenNumber, f);
        //		}
        //	}

        //	public void OnMouseMove(object pSender, MouseEventArgs pEvent, int pScreenNumber, int pProjectID, Form f)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).OnMouseMove(pSender, pEvent, pScreenNumber, f);
        //		}
        //	}

        //	public void OnMouseUp(object pSender, MouseEventArgs pEvent, int pScreenNumber, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).OnMouseUp(pSender, pEvent, pScreenNumber);
        //		}
        //	}

        //	public int OnProjectNew(ProjectType pProjectType, int pProductID, string pstrProjectPath, bool blOrientation)
        //	{
        //		int num;
        //		if (pProjectType != ProjectType.Prizm3)
        //		{
        //			num = -1;
        //		}
        //		else
        //		{
        //			ProjectPrizm3 projectPrizm3 = new ProjectPrizm3();
        //			if (!CommonConstants.IsProductPLC(pProductID))
        //			{
        //				if ((pProductID == 2001 || pProductID == 2002 ? false : pProductID != 2003))
        //				{
        //					if (!CommonConstants.IsProductGWY_K22(pProductID))
        //					{
        //						projectPrizm3.CreateDefaultAppTaskForNewProject();
        //					}
        //				}
        //			}
        //			if (!CommonConstants._isProductVertical)
        //			{
        //				projectPrizm3.Orientation = this.SetVerticalByte(pProductID);
        //			}
        //			else
        //			{
        //				projectPrizm3.Orientation = blOrientation;
        //			}
        //			projectPrizm3.SetOrientation(projectPrizm3.Orientation);
        //			int modelSeriesProductId = CommonConstants.GetModelSeriesProductId(pProductID);
        //			if ((modelSeriesProductId == 501 || modelSeriesProductId == 502 || modelSeriesProductId == 503 || modelSeriesProductId == 505 || modelSeriesProductId == 507 || modelSeriesProductId == 509 || modelSeriesProductId == 500 || modelSeriesProductId == 508 || modelSeriesProductId == 721 || modelSeriesProductId == 526 || modelSeriesProductId == 3503 || modelSeriesProductId == 3801 || modelSeriesProductId == 3504 || modelSeriesProductId == 5706 || modelSeriesProductId == 5707 || modelSeriesProductId == 5708 || modelSeriesProductId == 5709 || modelSeriesProductId == 5713 || modelSeriesProductId == 5712 || modelSeriesProductId == 5710 || modelSeriesProductId == 5711 ? true : CommonConstants.IsProductFlexiPanels(modelSeriesProductId)))
        //			{
        //				projectPrizm3.CreateDefaultGlobalKeyTaskForNewProject(pProductID);
        //			}
        //			projectPrizm3._projectprizm3DownloadPercentage += new ProjectPrizm3.DownloadPercentage(this.GetPercentage);
        //			projectPrizm3._projectprizm3DownloadStatus += new ProjectPrizm3.DownloadStatus(this.serial__deviceDownloadStatus);
        //			projectPrizm3._projectprizm3ImportProgressValue += new ProjectPrizm3.ImportProgressValue(this.ProjectPrizm3__projectprizm3ImportProgressValue);
        //			projectPrizm3._projectPrizm3ExportProgressValue += new ProjectPrizm3.ExportProgressValue(this.ProjectPrizm3__projectPrizm3ExportProgressValue);
        //			projectPrizm3._exportObjectProgressBar += new ProjectPrizm3.ExportObjectProgressBar(this.ProjectPrizm3__exportObjectProgressBar);
        //			projectPrizm3._evntAddTagInStratonVMDB += new ProjectPrizm3.AddTagInStratonVMDB(this.ProjectPrizm3__evntAddTagInStratonVMDB);
        //			projectPrizm3._evntAddNodeStatusTagOnImport += new ProjectPrizm3.AddNodeStatusTagOnImport(this.ProjectPrizm3__evntAddNodeStatusTagOnImport);
        //			projectPrizm3._importSlaveTagAtCom1Com2 += new ProjectPrizm3.ImportSlaveTagAtCom1Com2(this.ProjectPrizm3__importSlaveTagAtCom1Com2);
        //			projectPrizm3._importSlaveTagAtEthernet += new ProjectPrizm3.ImportSlaveTagAtEthernet(this.ProjectPrizm3__importSlaveTagAtEthernet);
        //			projectPrizm3._importModbusSlaveTagAtCom1Com2 += new ProjectPrizm3.ImportModbusSlaveTagAtCom1Com2(this.ProjectPrizm3__importModbusSlaveTagAtCom1Com2);
        //			projectPrizm3._importModbusSlaveTagAtEthernet += new ProjectPrizm3.ImportModbusSlaveTagAtEthernet(this.ProjectPrizm3__importModbusSlaveTagAtEthernet);
        //			projectPrizm3._importNodeSelection += new ProjectPrizm3.ImportNodeSelection(this.ProjectPrizm3__importNodeSelection);
        //			projectPrizm3._ShowImportTags += new ProjectPrizm3.ShowImportTags(this.ProjectPrizm3__ShowImportTags);
        //			this._appProjects.Insert(0, projectPrizm3);
        //			((Project)this._appProjects[0]).Projecttype = ProjectType.Prizm3;
        //			((Project)this._appProjects[0]).ProjectPath = pstrProjectPath;
        //			projectPrizm3.NewProject((short)pProductID);
        //			num = 0;
        //		}
        //		return num;
        //	}

        //	public int OnProjectOpen(string pstrFileName, bool blUpload)
        //	{
        //		int num = 0;
        //		ProjectPrizm3 projectPrizm3 = new ProjectPrizm3();
        //		this._appProjects.Insert(0, projectPrizm3);
        //		((Project)this._appProjects[0]).Projecttype = ProjectType.Prizm3;
        //		((Project)this._appProjects[0]).ProjectPath = pstrFileName;
        //		if (File.Exists(pstrFileName))
        //		{
        //			((Project)this._appProjects[0]).DateLastEdited = File.GetLastWriteTime(pstrFileName).ToShortDateString();
        //			Project item = (Project)this._appProjects[0];
        //			object[] hours = new object[5];
        //			TimeSpan timeOfDay = File.GetLastWriteTime(pstrFileName).TimeOfDay;
        //			hours[0] = timeOfDay.Hours;
        //			hours[1] = ":";
        //			timeOfDay = File.GetLastWriteTime(pstrFileName).TimeOfDay;
        //			hours[2] = timeOfDay.Minutes;
        //			hours[3] = ":";
        //			timeOfDay = File.GetLastWriteTime(pstrFileName).TimeOfDay;
        //			hours[4] = timeOfDay.Seconds;
        //			item.TimeLastEdited = string.Concat(hours);
        //		}
        //		projectPrizm3._projectprizm3DownloadPercentage += new ProjectPrizm3.DownloadPercentage(this.GetPercentage);
        //		projectPrizm3._projectprizm3DownloadStatus += new ProjectPrizm3.DownloadStatus(this.serial__deviceDownloadStatus);
        //		projectPrizm3._projectprizm3ImportProgressValue += new ProjectPrizm3.ImportProgressValue(this.ProjectPrizm3__projectprizm3ImportProgressValue);
        //		projectPrizm3._projectPrizm3ExportProgressValue += new ProjectPrizm3.ExportProgressValue(this.ProjectPrizm3__projectPrizm3ExportProgressValue);
        //		projectPrizm3._exportObjectProgressBar += new ProjectPrizm3.ExportObjectProgressBar(this.ProjectPrizm3__exportObjectProgressBar);
        //		projectPrizm3._evntAddTagInStratonVMDB += new ProjectPrizm3.AddTagInStratonVMDB(this.ProjectPrizm3__evntAddTagInStratonVMDB);
        //		projectPrizm3._evntAddNodeStatusTagOnImport += new ProjectPrizm3.AddNodeStatusTagOnImport(this.ProjectPrizm3__evntAddNodeStatusTagOnImport);
        //		projectPrizm3._importSlaveTagAtCom1Com2 += new ProjectPrizm3.ImportSlaveTagAtCom1Com2(this.ProjectPrizm3__importSlaveTagAtCom1Com2);
        //		projectPrizm3._importSlaveTagAtEthernet += new ProjectPrizm3.ImportSlaveTagAtEthernet(this.ProjectPrizm3__importSlaveTagAtEthernet);
        //		projectPrizm3._importModbusSlaveTagAtCom1Com2 += new ProjectPrizm3.ImportModbusSlaveTagAtCom1Com2(this.ProjectPrizm3__importModbusSlaveTagAtCom1Com2);
        //		projectPrizm3._importModbusSlaveTagAtEthernet += new ProjectPrizm3.ImportModbusSlaveTagAtEthernet(this.ProjectPrizm3__importModbusSlaveTagAtEthernet);
        //		projectPrizm3._importNodeSelection += new ProjectPrizm3.ImportNodeSelection(this.ProjectPrizm3__importNodeSelection);
        //		projectPrizm3._ShowImportTags += new ProjectPrizm3.ShowImportTags(this.ProjectPrizm3__ShowImportTags);
        //		num = (blUpload ? projectPrizm3.UploadFileOpen(pstrFileName) : projectPrizm3.Read(pstrFileName));
        //		if (num != 0)
        //		{
        //			((ProjectPrizm3)this._appProjects[0]).RemoveTagSelectionInfoObject();
        //			this._appProjects.RemoveAt(0);
        //			ProjectTagInformation.RemoveProject(CommonConstants.SelectedProjectID);
        //			projectPrizm3 = null;
        //		}
        //		return num;
        //	}

        //	public int OnProjectSave(string pstrFileName, int pProjectID, bool pResetDirtyFlag)
        //	{
        //		int num = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			num = ((ProjectPrizm3)this._appProjects[projectIndex]).WriteXML(pstrFileName);
        //			if (pResetDirtyFlag)
        //			{
        //				((Project)this._appProjects[projectIndex]).ResetDirtyFlag();
        //			}
        //		}
        //		return num;
        //	}

        //	public int OnProjectSave_Download(string pstrFileName, int pProjectID, bool pResetDirtyFlag)
        //	{
        //		int num = -1;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			num = ((Project)this._appProjects[projectIndex]).Write(pstrFileName);
        //			if (pResetDirtyFlag)
        //			{
        //				((Project)this._appProjects[projectIndex]).ResetDirtyFlag();
        //			}
        //		}
        //		return num;
        //	}

        //	public void OnSerialDownLoad(int pProjectID, DownloadData pDownloadData, string pComNoOrIpAddress, string pFileName, int pProductID, int pBaudRate, byte pParity, byte pByteSize, byte pStopBits, byte[] _setupFrameMode)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Download(pDownloadData, pComNoOrIpAddress, pFileName, pProductID, pBaudRate, pParity, pByteSize, pStopBits, _setupFrameMode);
        //		}
        //	}

        //	public int OnSerialUpload_Ladder(int pProjectID, DownloadData pDownloadData, string pComNoOrIpAddress, string pFileName, int pBaudRate, byte pParity, byte pByteSize, byte pStopBits)
        //	{
        //		byte[] numArray = new byte[10];
        //		int num = -1;
        //		Serial serial = new Serial(pComNoOrIpAddress, pBaudRate, pParity, pByteSize, pStopBits, numArray);
        //		serial._deviceDownloadPercentage += new Serial.DownloadPercentage(this.GetPercentage);
        //		serial._deviceDownloadStatus += new Serial.DownloadStatus(this.serial__deviceDownloadStatus);
        //		if (serial.Connect() == 0)
        //		{
        //			if ((pDownloadData & DownloadData.Application) == DownloadData.Application)
        //			{
        //				num = serial.ReceiveFile_UploadLadder(pFileName, Convert.ToByte(187));
        //				if (num == 0)
        //				{
        //					num = 0;
        //				}
        //				else if (num != 2)
        //				{
        //					num = (num != 3 ? -1 : 3);
        //				}
        //				else
        //				{
        //					num = 2;
        //				}
        //			}
        //			if ((pDownloadData & DownloadData.Ladder) == DownloadData.Ladder)
        //			{
        //				if (!CommonConstants.bUploadFromGUI)
        //				{
        //					num = serial.ReceiveFile_UploadLadder(pFileName, Convert.ToByte(CommonConstants.LADDER_UPLD_FILEID));
        //					if (num == 0)
        //					{
        //						num = 0;
        //					}
        //					else if (num != 2)
        //					{
        //						num = (num != 3 ? -1 : 3);
        //					}
        //					else
        //					{
        //						num = 2;
        //					}
        //				}
        //				else
        //				{
        //					num = serial.ReceiveFile_UploadLadder(pFileName, Convert.ToByte(CommonConstants.LADDER_UPLD_FILEID));
        //					if (num == 0)
        //					{
        //						num = 0;
        //					}
        //					else if (num != 2)
        //					{
        //						num = (num != 3 ? -1 : 3);
        //					}
        //					else
        //					{
        //						num = 2;
        //					}
        //				}
        //			}
        //		}
        //		return num;
        //	}

        //	public int OnSeriaUpload(int pProjectID, DownloadData pDownloadData, string pComNoOrIpAddress, string pFileName, int pProductID, int pBaudRate, byte pParity, byte pByteSize, byte pStopBits)
        //	{
        //		byte[] numArray = new byte[10];
        //		int num = -1;
        //		Serial serial = new Serial(pComNoOrIpAddress, pBaudRate, pParity, pByteSize, pStopBits, numArray);
        //		serial._deviceDownloadPercentage += new Serial.DownloadPercentage(this.GetPercentage);
        //		serial._deviceDownloadStatus += new Serial.DownloadStatus(this.serial__deviceDownloadStatus);
        //		if (serial.Connect() == 0)
        //		{
        //			if ((pDownloadData & DownloadData.Application) == DownloadData.Application)
        //			{
        //				CommonConstants.TEMP_DOWNLOAD_FILENAME = pFileName;
        //				num = (serial.ReceiveFile(pFileName, Convert.ToInt32(pDownloadData)) != 0 ? -1 : 0);
        //			}
        //		}
        //		return num;
        //	}

        //	public void OnUSBDownload(int pProjectID, DownloadData pDownloadData, string pComNoOrIpAddress, int pProductID, string pFileName, byte[] _setupFrameMode)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Download(pDownloadData, pFileName, pProductID, pComNoOrIpAddress, _setupFrameMode);
        //		}
        //	}

        //	public void OnUSBExpansionDownload(int pProjectID, int pProductID, string pFileName, byte[] _setupFrameMode)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ExpansionDownload(pFileName, pProductID, _setupFrameMode);
        //		}
        //	}

        //	public void OnUSBExpansionDownload_AnalogModule(int pProjectID, int pProductID, string pFileName1, string pFileName2, byte byteFirst, byte byteSecond, byte[] _setupFrameMode)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ExpansionDownload_Analog(pFileName1, pFileName2, byteFirst, byteSecond, pProductID, _setupFrameMode);
        //		}
        //	}

        //	public int OnUSBUpload(int pProjectID, DownloadData pDownloadData, string pFileName)
        //	{
        //		int num = -1;
        //		USB uSB = new USB((short)this.GetProductID(pProjectID));
        //		uSB._deviceDownloadUSBPercentage += new USB.DownloadPercentage(this.GetPercentage);
        //		uSB._deviceDownloadUSBStatus += new USB.DownloadStatus(this.serial__deviceDownloadStatus);
        //		if (uSB.Connect_ForFirstTime() == 0)
        //		{
        //			if ((pDownloadData & DownloadData.Application) == DownloadData.Application)
        //			{
        //				CommonConstants.TEMP_DOWNLOAD_FILENAME = pFileName;
        //				num = (uSB.ReceiveFile(pFileName, Convert.ToInt32(pDownloadData)) != 0 ? -1 : 0);
        //			}
        //		}
        //		return num;
        //	}

        //	public int OnUSBUpload_Ladder(int pProjectID, DownloadData pDownloadData, string pFileName)
        //	{
        //		int num = -1;
        //		USB uSB = new USB((short)this.GetProductID(pProjectID));
        //		uSB._deviceDownloadUSBPercentage += new USB.DownloadPercentage(this.GetPercentage);
        //		uSB._deviceDownloadUSBStatus += new USB.DownloadStatus(this.serial__deviceDownloadStatus);
        //		if (uSB.Connect_ForFirstTime() == 0)
        //		{
        //			if ((pDownloadData & DownloadData.Application) == DownloadData.Application)
        //			{
        //				num = uSB.ReceiveFile_UploadLadder(pFileName, Convert.ToByte(187));
        //				if (num != 0)
        //				{
        //					num = (num != 2 ? -1 : 2);
        //				}
        //				else
        //				{
        //					num = 0;
        //				}
        //			}
        //			if ((pDownloadData & DownloadData.Ladder) == DownloadData.Ladder)
        //			{
        //				if (!CommonConstants.bUploadFromGUI)
        //				{
        //					num = uSB.ReceiveFile_UploadLadder(pFileName, Convert.ToByte(CommonConstants.LADDER_UPLD_FILEID));
        //					if (num != 0)
        //					{
        //						num = (num != 2 ? -1 : 2);
        //					}
        //					else
        //					{
        //						num = 0;
        //					}
        //				}
        //				else
        //				{
        //					num = uSB.ReceiveFile_UploadLadder(pFileName, Convert.ToByte(CommonConstants.LADDER_UPLD_FILEID));
        //					if (num != 0)
        //					{
        //						num = (num != 2 ? -1 : 2);
        //					}
        //					else
        //					{
        //						num = 0;
        //					}
        //				}
        //			}
        //		}
        //		return num;
        //	}

        //	public bool Optimize_Ladder(int pProjectID)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).Optimize_Ladder());
        //		return flag;
        //	}

        //	public int ProjectCount()
        //	{
        //		int num;
        //		num = (this._appProjects != null ? this._appProjects.Count : 0);
        //		return num;
        //	}

        //	private void ProjectPrizm3__evntAddNodeStatusTagOnImport(string pNodename)
        //	{
        //		this._evntAddNodeStatusTagOnImport(pNodename);
        //	}

        //	private bool ProjectPrizm3__evntAddTagInStratonVMDB(string pstrPrefix, byte btStratonBlockType, string InitialValue, string GroupName, CommonConstants.Prizm3TagStructure pobjTagdata)
        //	{
        //		return this._evntAddTagInStratonVMDB(pstrPrefix, btStratonBlockType, InitialValue, GroupName, pobjTagdata);
        //	}

        //	private void ProjectPrizm3__exportObjectProgressBar(int piObjCount, int piTotalObjects, string pstrScreenName)
        //	{
        //		this._applicationExportObjectsProgressBar(piObjCount, piTotalObjects, pstrScreenName);
        //	}

        //	private int ProjectPrizm3__importModbusSlaveTagAtCom1Com2(string port)
        //	{
        //		return this._importModbusSlaveTagAtCom1Com2(port);
        //	}

        //	private int ProjectPrizm3__importModbusSlaveTagAtEthernet(string port)
        //	{
        //		return this._importModbusSlaveTagAtEthernet(port);
        //	}

        //	private string ProjectPrizm3__importNodeSelection(ArrayList arr)
        //	{
        //		return this._importNodeSelection(arr);
        //	}

        //	private int ProjectPrizm3__importSlaveTagAtCom1Com2(string port)
        //	{
        //		return this._importSlaveTagAtCom1Com2(port);
        //	}

        //	private int ProjectPrizm3__importSlaveTagAtEthernet(string port)
        //	{
        //		return this._importSlaveTagAtEthernet(port);
        //	}

        //	private void ProjectPrizm3__projectPrizm3ExportProgressValue(int piProgressValue, int piTotalTags, bool pblExportFinished)
        //	{
        //		this._applicationExportProgressValue(piProgressValue, piTotalTags, pblExportFinished);
        //	}

        //	private void ProjectPrizm3__projectprizm3ImportProgressValue(int piProgressValue, int piTotalNodeTag)
        //	{
        //		this._applicationImportProgressValue(piProgressValue, piTotalNodeTag);
        //	}

        //	private void ProjectPrizm3__ShowImportTags(ArrayList _totalTagList)
        //	{
        //		this._ShowImportTags(_totalTagList);
        //	}

        //	public void PropertyChange(int piProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).PropertyChange();
        //		}
        //	}

        //	public void ReadApplicationFile()
        //	{
        //		try
        //		{
        //			FileStream fileStream = null;
        //			fileStream = new FileStream(this._appFileType, FileMode.Open, FileAccess.Read);
        //			fileStream.Seek((long)0, SeekOrigin.Begin);
        //			this.ReadApplicationFileHeader(fileStream);
        //			this.AuthenticateUser = Convert.ToBoolean(this.ReadData(fileStream));
        //			this.AuditTrail = Convert.ToBoolean(this.ReadData(fileStream));
        //			this.Language = Convert.ToString(this.ReadData(fileStream));
        //			this.UserID = Convert.ToInt32(this.ReadData(fileStream));
        //			this.LastUser = Convert.ToString(this.ReadData(fileStream));
        //			this.Password = Convert.ToString(this.ReadData(fileStream));
        //			this.RememberMe = Convert.ToBoolean(this.ReadData(fileStream));
        //			this.LastLoginDateTime = Convert.ToDateTime(this.ReadData(fileStream));
        //			fileStream.Close();
        //		}
        //		catch
        //		{
        //		}
        //	}

        //	public void ReadApplicationFileHeader(FileStream fp)
        //	{
        //		for (int i = 0; i < 7; i++)
        //		{
        //			this.ReadData(fp);
        //		}
        //	}

        //	public object ReadData(FileStream fp)
        //	{
        //		byte[] numArray = new byte[4];
        //		byte[] numArray1 = new byte[2];
        //		object obj = new object();
        //		UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
        //		fp.Read(numArray, 0, 4);
        //		CommonConstants.MAKEINT(numArray);
        //		byte num = Convert.ToByte(fp.ReadByte());
        //		if (num == Convert.ToByte(TypeConstants.STRING))
        //		{
        //			byte num1 = Convert.ToByte(fp.ReadByte());
        //			byte[] numArray2 = new byte[num1];
        //			fp.Read(numArray2, 0, (int)num1);
        //			obj = unicodeEncoding.GetString(numArray2);
        //		}
        //		if (num == Convert.ToByte(TypeConstants.BYTE))
        //		{
        //			obj = Convert.ToByte(fp.ReadByte());
        //		}
        //		if (num == Convert.ToByte(TypeConstants.SHORT))
        //		{
        //			fp.Read(numArray1, 0, 2);
        //			obj = CommonConstants.MAKEWORD(numArray1[0], numArray1[1]);
        //		}
        //		if (num == Convert.ToByte(TypeConstants.USHORT))
        //		{
        //			fp.Read(numArray1, 0, 2);
        //			obj = CommonConstants.MAKEWORD(numArray1[0], numArray1[1]);
        //		}
        //		if (num == Convert.ToByte(TypeConstants.INT))
        //		{
        //			fp.Read(numArray, 0, 4);
        //			obj = CommonConstants.MAKEINT(numArray);
        //		}
        //		if (num == Convert.ToByte(TypeConstants.UINT))
        //		{
        //			fp.Read(numArray, 0, 4);
        //			obj = CommonConstants.MAKEINT(numArray);
        //		}
        //		if (num == Convert.ToByte(TypeConstants.BOOLEAN))
        //		{
        //			obj = Convert.ToBoolean(fp.ReadByte());
        //		}
        //		return obj;
        //	}

        //	public void ReadHIOData()
        //	{
        //		this._appHIOLibrary.ReadHIOLibrary();
        //	}

        //	public void ReadLoginFile()
        //	{
        //		FileStream fileStream = null;
        //		fileStream = new FileStream("Login.dat", FileMode.Open, FileAccess.Read);
        //		fileStream.Seek((long)0, SeekOrigin.Begin);
        //		try
        //		{
        //			try
        //			{
        //				while (fileStream.Position != fileStream.Length)
        //				{
        //					this._appLoginInfo.iUserID = Convert.ToInt32(this.ReadData(fileStream));
        //					this._appLoginInfo.strUserName = Convert.ToString(this.ReadData(fileStream));
        //					this._appLoginInfo.strPwd = Convert.ToString(this.ReadData(fileStream));
        //					this._appLoginList.Add(this._appLoginInfo);
        //				}
        //			}
        //			catch (Exception exception)
        //			{
        //			}
        //		}
        //		finally
        //		{
        //			if (fileStream != null)
        //			{
        //				fileStream.Close();
        //			}
        //		}
        //	}

        //	public void ReadUserData()
        //	{
        //		this._appUserManager.ReadUsersFile();
        //		this.UserList = this._appUserManager.UserList;
        //	}

        //	public void RedoAction(int pProjectID, int piScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).RedoAction(piScreenNumber);
        //		}
        //	}

        //	public void Refresh_BlockNameIn_CallInstruction(int pProjectID, string strOldBlockName, string strNewBlockName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).Refresh_BlockNameIn_CallInstruction(strOldBlockName, strNewBlockName);
        //		}
        //	}

        //	public void Refresh_LadderInstruction_Step(int pProjectID, int pScreenNumber, int Type, string TagAddress, double TagValue, System.Drawing.Rectangle objClientRect)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).Refresh_LadderInstruction_Step(pScreenNumber, Type, TagAddress, TagValue, objClientRect);
        //		}
        //	}

        //	public void Refresh_LadderInstruction_TagValue(int pProjectID, int pScreenNumber, int Type, string TagAddress, double TagValue)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).Refresh_LadderInstruction_TagValue(pScreenNumber, Type, TagAddress, TagValue);
        //		}
        //	}

        //	public void Refresh_LadderInstruction_TagValueNew(int pProjectID, int pScreenNumber, int Type, string TagAddress, double TagValue, System.Drawing.Rectangle objClientRect)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).Refresh_LadderInstruction_TagValueNew(pScreenNumber, Type, TagAddress, TagValue, objClientRect);
        //		}
        //	}

        //	public void RefreshPixelInfo(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).RefreshPixelInfo();
        //		}
        //	}

        //	public void RemoveAllBreakPoints(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).RemoveAllBreakPoints();
        //	}

        //	public void RemoveOldScreenSaver_PasswordScreeen(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).RemoveOldScreenSaver_PasswordScreeen(pProjectID, pScreenNumber);
        //		}
        //	}

        //	public void RemoveProject(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			if (File.Exists("DeveloperDebug.txt"))
        //			{
        //				StreamReader streamReader = new StreamReader("DeveloperDebug.txt");
        //				if (streamReader.ReadLine() == "4")
        //				{
        //					if (!CommonConstants.IsProductSupports2Color(CommonConstants.ProductDataInfo.iProductID))
        //					{
        //						((ProjectPrizm3)this._appProjects[projectIndex]).RemoveUnusedImagesFromProject(((Project)this._appProjects[projectIndex]).ProjectPath);
        //					}
        //				}
        //				streamReader.Close();
        //			}
        //			IDataObject dataObject = Clipboard.GetDataObject();
        //			dataObject.GetFormats();
        //			if (Clipboard.ContainsData("System.Collections.ArrayList"))
        //			{
        //				if (dataObject.GetDataPresent("System.Collections.ArrayList"))
        //				{
        //					Clipboard.Clear();
        //				}
        //			}
        //			((ProjectPrizm3)this._appProjects[projectIndex]).RemoveTagSelectionInfoObject();
        //			this._appProjects.RemoveAt(projectIndex);
        //			ProjectTagInformation.RemoveProject(pProjectID);
        //			GC.Collect();
        //		}
        //	}

        //	public void RemoveRefrenceofTemplateScreenFromImportScreenList(ClassList.Screen objScreenInfo, int ImportProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(ImportProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).RemoveRefrenceofTemplateScreenFromImportScreenList(objScreenInfo);
        //		}
        //	}

        //	public void RemoveUnSupportedObjects(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).RemoveUnSupportedObjects();
        //		}
        //	}

        //	public bool Replace_DataMonitor_VarName(int pProjectID, string strOld, string strNew)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).Replace_DataMonitor_VarName(strOld, strNew));
        //		return flag;
        //	}

        //	public void ReplaceNode(int pProjectID, string pstrPrevNodeName, CommonConstants.NodeInfo pNodeInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).ReplaceNode(pstrPrevNodeName, pNodeInfo);
        //		}
        //	}

        //	public bool ReplaceString(int pProjectID, int ScreenNumber, int Type, string str, string strReplace, int pos)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).ReplaceString(ScreenNumber, Type, str, strReplace, pos));
        //		return flag;
        //	}

        //	public void ReplaceTag(CommonConstants.AlarmInfo objAlarm, int pProjectId, string sReplace, uint uiSelAlarmId)
        //	{
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).ReplaceTag(objAlarm, sReplace, uiSelAlarmId);
        //		}
        //	}

        //	public void ReReadScreen(int pProjectID, int scrNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if ((projectIndex == -1 ? false : CommonConstants.ProjectReadVersion >= 69))
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ReReadScreen(scrNumber);
        //		}
        //	}

        //	public void ResetBreakPointInfo(int pProjectID, int type)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).ResetBreakPointInfo(type);
        //	}

        //	public void ResetImportScreenData(int ImportProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(ImportProjectID);
        //		if (projectIndex != -1)
        //		{
        //			CommonConstants.g_Support_IEC_Ladder = this._prev_g_Support_IEC_Ladder;
        //			CommonConstants.ProductDataInfo = this._prevProductDataInfo;
        //			CommonConstants.g_ProjectPath = this._prev_g_ProjectPath;
        //			CommonConstants.DefaultRegTagName = CommonConstants.ImportTempDefaultRegTagName;
        //			CommonConstants.DefaultRegTagId = CommonConstants.ImportTempDefaultRegTagId;
        //			CommonConstants.DefaultBitTagName = CommonConstants.ImportTempDefaultBitTagName;
        //			CommonConstants.DefaultBitTagId = CommonConstants.ImportTempDefaultBitTagId;
        //			CommonConstants.IsXMLFileSaveRoutine = CommonConstants.ImportScreenXmlRoutine;
        //			CommonConstants._isProductVertical = CommonConstants.ImportScreenIsProductVertical;
        //			CommonConstants.SelectedProjectID = this.ImportScreenSelectedProjectID;
        //			this._appProjects.RemoveAt(projectIndex);
        //		}
        //	}

        //	public void RestoreDefault(int pProjectID, int IActiveScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).RestoreDefault(IActiveScreenNumber);
        //		}
        //	}

        //	public void RestoreProjectDirtyFlag(int pProjectID, bool flag)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).RestoreDirtyFlag(flag);
        //		}
        //	}

        //	public void SaveLanguageList(List<CommonConstants.LanguageInformation> pLanguageList, int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SaveLanguageList(pLanguageList);
        //		}
        //	}

        //	public void ScreenDoubleClick(object pSender, EventArgs pEvent, int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).ScreenDoubleClick(pSender, pScreenNumber);
        //		}
        //	}

        //	public void ScreenDragDrop(object pSender, DragEventArgs pEvent, int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).ScreenDragDrop(pEvent, pScreenNumber);
        //		}
        //	}

        //	public void ScreenDragEnter(object pSender, DragEventArgs pEvent, int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).ScreenDragEnter(pEvent, pScreenNumber);
        //		}
        //	}

        //	public void ScreenKeyDown(object pSender, KeyEventArgs e, int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).ScreenKeyDown(e, pScreenNumber);
        //		}
        //	}

        //	public bool ScreenKeyUp(object pSender, KeyEventArgs pEvent, int pProjectID, int pScreenNumber)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex == -1)
        //		{
        //			flag = false;
        //		}
        //		else
        //		{
        //			((Project)this._appProjects[projectIndex]).SetProcessID(pProjectID, pScreenNumber);
        //			flag = ((Project)this._appProjects[projectIndex]).ScreenKeyUp(pEvent, pScreenNumber);
        //		}
        //		return flag;
        //	}

        //	public void SelectedLanguageChange(int pProjectIndex, int pLanguageIndex)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectIndex);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SelectedLanguageChange(pLanguageIndex);
        //		}
        //	}

        //	public void SelectErrorObject(int pProjectID, uint pErrorSourceID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SelectErrorObject(pErrorSourceID, pScreenNumber);
        //		}
        //	}

        //	public void SendToBack(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SendToBack(pScreenNumber);
        //		}
        //	}

        //	public void serial__deviceDownloadStatus(int pMessage)
        //	{
        //		this._applicationDownloadStatus(pMessage);
        //	}

        //	public void SetAccelerateScanTime(int pProjectID, byte pbtAccelerateScanTime)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetAccelerateScanTime(pbtAccelerateScanTime);
        //		}
        //	}

        //	public void SetAlarmConfigInformation(int piProjectId, CommonConstants.AlarmConfigInfo pObjAlarmConfigInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ProjectAlarmConfigInfo = pObjAlarmConfigInfo;
        //		}
        //	}

        //	public void SetAlarmParameters(int piProjectId, string[] parrstrY, string[] parrstrN, string[] parrstrYes, string[] parrstrNo)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetAlarmParameters(parrstrY, parrstrN, parrstrYes, parrstrNo);
        //		}
        //	}

        //	public void SetAlarmType(int piProjectId, AlarmType pObjAlarmType)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ProjectAlarmType = pObjAlarmType;
        //		}
        //	}

        //	public void SetAnalogConfiguration(int piProjectID, List<CommonConstants.AnalogInputConfiguration> lstAnalogInputConfig, List<CommonConstants.AnalogOutputConfiguration> lstAnalogOutputConfig, bool InputEnable, bool OutputEnable)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputEnable = InputEnable;
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputEnable = OutputEnable;
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AnalogInputConfiguration = lstAnalogInputConfig;
        //			((ProjectPrizm3)this._appProjects[projectIndex]).AnalogOutputConfiguration = lstAnalogOutputConfig;
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DirtyFlag = true;
        //		}
        //	}

        //	public void SetAssociatedScreenInfo(List<CommonConstants.AssociatedScreenInfo> pobjAssoScreenInfo, int pProjectID)
        //	{
        //		this.DirtyFlag = true;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetAssociatedScreenInfo(pobjAssoScreenInfo);
        //		}
        //	}

        //	public void SetBaseScreenHeightWidth(int pProjectId, short shBottomRightX, short shBottomRightY)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetBaseScreenHeightWidth(shBottomRightX, shBottomRightY);
        //		}
        //	}

        //	public void SetBlockNames_ForCallInstructions(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetBlockNames_ForCallInstructions();
        //		}
        //	}

        //	public void SetBreakPointInfo(int pProjectID, int screenNumber, uint ID, int type, bool b1, bool b2, long address)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).SetBreakPointInfo(screenNumber, ID, type, b1, b2, address);
        //	}

        //	public void SetBroadcastSettings(CommonConstants.BroadcastSettings pobjBroadcastSettings, byte pbtPort, int pProjectID, List<bool> plstFlags)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetBroadcastSettings(pobjBroadcastSettings, pbtPort, plstFlags);
        //		}
        //	}

        //	public void SetDataEntryObjectKeypadSettings(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataEntryObjectKeypadSettings();
        //		}
        //	}

        //	public void SetDataLogClearBitTagId(int pIntarrDataLogClearTagId, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataLogClearBitTagId(pIntarrDataLogClearTagId);
        //		}
        //	}

        //	public void SetDataLoggerEraseMemoryEvent(int pProjectID, bool pblEraseMemory)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DataLoggerEraseMemoryEvent = pblEraseMemory;
        //		}
        //	}

        //	public void SetDataLoggerGroupsInformation(int pProjectId, List<CommonConstants.LoggerGroupInfo> LoggerGroups)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataLoggerLoggerGroupsInformation(LoggerGroups);
        //		}
        //	}

        //	public void SetDataLoggerGroupsInformationExternal(int pProjectId, List<CommonConstants.LoggerGroupInfoExternal> LoggerGroups)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataLoggerLoggerGroupsInformationExternal(LoggerGroups);
        //		}
        //	}

        //	public void SetDataLoggerLoggedTagsInformation(int pProjectId, int[,] piGroupIdTagId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataLoggerLogTagList(piGroupIdTagId);
        //		}
        //	}

        //	public void SetDataLoggerLoggedTagsInformationExternal(int pProjectId, int[,] piGroupIdTagId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataLoggerLogTagListExternal(piGroupIdTagId);
        //		}
        //	}

        //	public void SetDataLoggerLoggingType(int pProjectId, byte LoggingType)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataloggerLoggingType(LoggingType);
        //		}
        //	}

        //	public void SetDataLoggerLoggingTypeExternal(int pProjectId, byte LoggingType)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataloggerLoggingTypeExternal(LoggingType);
        //		}
        //	}

        //	public void SetDataLoggerMemoryFull(int pProjectId, byte MemoryFull)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataloggerMemFullInformation(MemoryFull);
        //		}
        //	}

        //	public void SetDataLoggerMemorySize(int pProjectId, byte MemorySize)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataLoggerMemSizeInformation(MemorySize);
        //		}
        //	}

        //	public void SetDataMonitorInfo(int pProjectID, ArrayList objListData)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDataMonitorInfo(objListData);
        //		}
        //	}

        //	public void SetDataMonitorList(int pProjectID, ArrayList objtagList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).SetDataMonitorList(objtagList);
        //	}

        //	public void SetDeviceNodeName_OldProjects(int newModelID)
        //	{
        //		((ProjectPrizm3)this._appProjects[0]).SetDeviceNodeName_OldProjects(newModelID);
        //	}

        //	public void SetDeviceSafeRemovelBitTagId(int pIntarrDeviceSafeRemoveTagId, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetDeviceSafeRemovelBitTagId(pIntarrDeviceSafeRemoveTagId);
        //		}
        //	}

        //	public void SetDownLoadTagNameFlag(bool pDownLoadTagName)
        //	{
        //	}

        //	public void SetEmailConfiguration(EmailConfigInfo pEmConf, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetEmailConfiguration(pEmConf);
        //		}
        //	}

        //	public void SetEmailContacts(int pProjectID, string pEmailName, List<EmailContactInfo> pListOfToContacts, List<EmailContactInfo> pListOfCcContacts, List<EmailContactInfo> pListOfBccContacts, string pSubject, string pBody, bool pDataLog, bool pAlarmData)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetEmailContacts(pEmailName, pListOfToContacts, pListOfCcContacts, pListOfBccContacts, pSubject, pBody, pDataLog, pAlarmData);
        //		}
        //	}

        //	public void SetEraseDataLogger(int pProjectID, byte pbtDataEraseDataMemory)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetEraseDataLoggerMemory(pbtDataEraseDataMemory);
        //		}
        //	}

        //	public void SetEraseDataLoggerMemory(int pProjectID, bool pblEraseMemory)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).EraseDataLoggerMemory = pblEraseMemory;
        //		}
        //	}

        //	public void SetEthernetSettings(int pProjectID, CommonConstants.EthernetSettings pEthernetSettings)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).Ethernetsettings = pEthernetSettings;
        //		}
        //	}

        //	public void SetExpansionModuleType(int pProjectID, byte pbtExpansionModuletype)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ExpansionModuleType = pbtExpansionModuletype;
        //		}
        //	}

        //	public void SetFactoryScreenTaskForFP(int piProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetFactoryScreenTaskForFP();
        //		}
        //	}

        //	public void SetFactoryScreenTaskList(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetFactoryScreenTaskForFP();
        //		}
        //	}

        //	public void SetFillColor(int pProjectID, Color pColor, int piScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetFillColor(pColor, piScreenNumber);
        //		}
        //	}

        //	public void SetFloatFormat(bool pHighWordLowWord, ProjectType pProjectType)
        //	{
        //		if (pProjectType == ProjectType.Prizm3)
        //		{
        //			((ProjectPrizm3)this._appProjects[0]).HighWordLowWordFloatFormat = pHighWordLowWord;
        //			((ProjectPrizm3)this._appProjects[0]).LowWordHighWordFloatFormat = !pHighWordLowWord;
        //		}
        //	}

        //	public void SetFloatFormatfor8bytePort1(int pProjectID, byte pbtHighWordLowWord)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetFloatFormatfor8bytePort1(pbtHighWordLowWord);
        //		}
        //	}

        //	public void SetFloatFormatfor8bytePort2(int pProjectID, byte pbtHighWordLowWord)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetFloatFormatfor8bytePort2(pbtHighWordLowWord);
        //		}
        //	}

        //	public void SetFloatFormatPort1(int pProjectID, byte pbtHighWordLowWord)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetFloatFormatPort1(pbtHighWordLowWord);
        //		}
        //	}

        //	public void SetFloatFormatPort2(int pProjectID, byte pbtHighWordLowWord)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetFloatFormatPort2(pbtHighWordLowWord);
        //		}
        //	}

        //	public void SetFocusRungNumber(int pProjectID, int pScreenNumber, int RungNO)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetFocusRungNumber(pScreenNumber, RungNO);
        //		}
        //	}

        //	public void SetFrequencyTagTagId(int pProjectId, int pTagId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetFrequencyTagTagId(pTagId);
        //		}
        //	}

        //	public void SetFrequencyTagTagIdExternal(int pProjectId, int pTagId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetFrequencyTagTagIdExternal(pTagId);
        //		}
        //	}

        //	public void SetFTPInformation(int pProjectID, CommonConstants.structFTPInfo pstFTPInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetFTPInformation(pstFTPInfo);
        //		}
        //	}

        //	public void SetGlobalKeysList(int pProjectID, List<GlobalKeys> pGlobalKeysList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetGlobalKeysList(pGlobalKeysList);
        //		}
        //	}

        //	public void SetGlobalOnOff(int pProjectIndex, bool pState)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectIndex);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetGlobalOnOff(pState);
        //		}
        //	}

        //	public void SetGroupControlBitTagId(int[] pIntarrGroupControlBitTagId, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetGroupControlBitTagId(pIntarrGroupControlBitTagId);
        //		}
        //	}

        //	public void SetHardwareStructure(int pProjectId, byte pbtPort, byte pbtIBMSerialPLCType)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetHardwareStructure(pbtPort, pbtIBMSerialPLCType);
        //		}
        //	}

        //	public void SetHWStructureSpecialPLCInfo(int pProjectID, List<byte> plstSpecialPLCInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetHWStructureSpecialPLCInfo(plstSpecialPLCInfo);
        //		}
        //	}

        //	public void SetImportTagDataInfo(int pProjectIndex, CommonConstants.ImportTagInfo ImportTag, int piProductId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectIndex);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetImportTagDataInfo(ImportTag, piProductId);
        //		}
        //	}

        //	public void SetIntFormatPort1(int pProjectID, byte pbtHighWordLowWord)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetByteFormatPort1(pbtHighWordLowWord);
        //		}
        //	}

        //	public void SetIntFormatPort2(int pProjectID, byte pbtHighWordLowWord)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetByteFormatPort2(pbtHighWordLowWord);
        //		}
        //	}

        //	public void SetIsExportTagStatus(int pProjectId, bool pstrIsTagExportStatus, int piTagId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetIsExportTagStatus(pstrIsTagExportStatus, piTagId);
        //		}
        //	}

        //	public void SetLadderInfo(int pProjectId, int ScreenNumber, CommonConstants.LadderScreenInfo objLadderInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetLadderInfo(ScreenNumber, objLadderInfo);
        //		}
        //	}

        //	public void SetLAdderInstructionsTagNamesFromTagID(int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetLAdderInstructionsTagNamesFromTagID();
        //		}
        //	}

        //	public void SetLineColor(int pProjectID, Color pColor, int piScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetLineColor(pColor, piScreenNumber);
        //		}
        //	}

        //	public void SetLineWidth(int pProjectID, LineWidth pLineWidth, int piScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetLineWidth(pLineWidth, piScreenNumber);
        //		}
        //	}

        //	public void SetLoggingBitTagId(int[] pIntarrLoggingBitTagId, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetLoggingBitTagsTagId(pIntarrLoggingBitTagId);
        //		}
        //	}

        //	public void SetLoggingBitTagIdExternal(int[] pIntarrLoggingBitTagId, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetLoggingBitTagsTagIdExternal(pIntarrLoggingBitTagId);
        //		}
        //	}

        //	public void SetmemoryUesdRegTagId(int pIntarrMemoryUsedTagId, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetmemoryUesdRegTagId(pIntarrMemoryUsedTagId);
        //		}
        //	}

        //	public void SetMITSQSettings(int pProjectID, CommonConstants.MITQSettings objSettings)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetMITSQSettings(objSettings);
        //		}
        //	}

        //	public void SetModbusSlaveComData(int pProjectId, Hashtable ObjCom1, Hashtable ObjCom2, Hashtable ObjCom3)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetModbusSlaveComData(ObjCom1, ObjCom2, ObjCom3);
        //		}
        //	}

        //	private void SetModelSpecificData()
        //	{
        //		DataRow dataRow;
        //		int i;
        //		DataRow[] dataRowArray = CommonConstants.dsRecentProjectList.Tables["ModelData"].Select("model<>''");
        //		DataRow[] dataRowArray1 = dataRowArray;
        //		for (i = 0; i < (int)dataRowArray1.Length; i++)
        //		{
        //			DataRow dataRow1 = dataRowArray1[i];
        //			this._appEv3ModelNames.Add(dataRow1["model"].ToString());
        //			this._appEv3ModelBMPNames.Add(dataRow1["BMPFile"].ToString());
        //		}
        //		this._appEv3ModelNames.TrimToSize();
        //		this._appEv3ModelBMPNames.TrimToSize();
        //		if (CommonConstants.dsRecentProjectsData.Tables.Contains("ProjectData"))
        //		{
        //			dataRowArray = CommonConstants.dsRecentProjectsData.Tables["ProjectData"].Select("Project<>''");
        //			dataRowArray1 = dataRowArray;
        //			for (i = 0; i < (int)dataRowArray1.Length; i++)
        //			{
        //				dataRow = dataRowArray1[i];
        //				this._appRecentProjectPaths.Add(dataRow["Path"].ToString());
        //			}
        //			this._appRecentProjectPaths.TrimToSize();
        //		}
        //		if (CommonConstants.dsRecentProjectsData.Tables.Contains("LastCloseProjectPath"))
        //		{
        //			dataRowArray = CommonConstants.dsRecentProjectsData.Tables["LastCloseProjectPath"].Select("Project<>''");
        //			dataRowArray1 = dataRowArray;
        //			for (i = 0; i < (int)dataRowArray1.Length; i++)
        //			{
        //				dataRow = dataRowArray1[i];
        //				CommonConstants._lastCloseProjectPath = dataRow["Path"].ToString();
        //			}
        //		}
        //		dataRowArray = null;
        //	}

        //	public void SetNewScreenName(int piProjectId, string pstrScreenName, int piCurrentScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetScreenName(pstrScreenName, piCurrentScreenNumber);
        //		}
        //	}

        //	public void SetNodePort(int pProjectID, int PLCCode, int port)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetNodePort(PLCCode, port);
        //		}
        //	}

        //	public void SetOpenScreenList(int[] pOpenScreenList, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetOpenScreenList(pOpenScreenList);
        //		}
        //	}

        //	public void SetOrientation(int pProjectID, bool blOrientation)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		((ProjectPrizm3)this._appProjects[projectIndex]).SetOrientation(blOrientation);
        //	}

        //	public void SetOverlappingFlag(int pProjectId, bool Flag)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).IsOverlappingAllowed = Flag;
        //		}
        //	}

        //	public void SetPatternColor(int pProjectID, Color pColor, int piScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetPatternColor(pColor, piScreenNumber);
        //		}
        //	}

        //	public void SetPLCModuleInfo(int pProjectID, CommonConstants.PlcModuleHeaderInfo objHeaderInfo, ArrayList objListModuleInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetPLCModuleInfo(objHeaderInfo, objListModuleInfo);
        //		}
        //	}

        //	public void SetPowerOnAndGlobalTasksIDList(int pProjectID, List<int> pPowerOnTaskIDList, List<int> pGlobalTaskIDList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetPowerOnAndGlobalTasksIDList(pPowerOnTaskIDList, pGlobalTaskIDList);
        //		}
        //	}

        //	public void SetPrevProductID(int pProjectId, int pProductID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).PrevProductID = pProductID;
        //		}
        //	}

        //	public void SetPrintDuration(int pProjectId, byte PrintDuration)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetPrintDuration(PrintDuration);
        //		}
        //	}

        //	public void SetPrinterPortSettings(CommonConstants.PrinterPortSettings pPrinterPortSettings, Port pPort, int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetPrinterPortSettings(pPrinterPortSettings, pPort);
        //		}
        //	}

        //	public void SetPrintPropertiesInformation(int pProjectId, List<CommonConstants.PrintPropertiesInfo> PrintPropertiesInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetPrintPropertiesInformation(PrintPropertiesInfo);
        //		}
        //	}

        //	public void SetPrintTagsInformation(int pProjectId, int gIndex, List<CommonConstants.PrintTagsInformation> PrintTagsInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetPrintTagsInformation(gIndex, PrintTagsInfo);
        //		}
        //	}

        //	public void SetPrintTagsInformationExternal(int pProjectId, int gIndex, List<CommonConstants.PrintTagsInformation> PrintTagsInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetPrintTagsInformationExternal(gIndex, PrintTagsInfo);
        //		}
        //	}

        //	public void SetProductId(int pProjectId, int pProductId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetProductID(pProductId);
        //			((ProjectPrizm3)this._appProjects[projectIndex]).DirtyFlag = true;
        //		}
        //	}

        //	public void SetProjectInformation(int piProjectID, string pTitle, string pAuthor, string pPassword, string pDateLastEdited, string pTimeLastEdited, string pDescription)
        //	{
        //		int projectIndex = this.GetProjectIndex(piProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetProjectInformation(pTitle, pAuthor, pPassword, pDateLastEdited, pTimeLastEdited, pDescription);
        //		}
        //	}

        //	public void SetProjectPath(int pProjectID, string pstrNewProjectPath)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).ProjectPath = pstrNewProjectPath;
        //		}
        //	}

        //	public void SetRightToCreateNewProject(int pValidUserID)
        //	{
        //		CommonConstants.UserData userData = new CommonConstants.UserData();
        //		userData = this.GetUserData(pValidUserID);
        //		this._appRtToCreateNewProj = userData._userPermToCreateNewProject;
        //	}

        //	public void SetScreenDirtyFlag(int pProjectID, int scrNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetScreenDirtyFlag(scrNumber);
        //		}
        //	}

        //	public void SetScreenKeysList(int pProjectID, int pScreenNumber, List<GlobalKeys> pScreenKeysList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetScreenKeysList(pScreenNumber, pScreenKeysList);
        //		}
        //	}

        //	public void setScreenSaver(bool pEnableScreenSaver, ushort pScreenSaverTime, ProjectType pProjectType)
        //	{
        //		if (pProjectType == ProjectType.Prizm3)
        //		{
        //			((ProjectPrizm3)this._appProjects[0]).EnableScreenSaver = pEnableScreenSaver;
        //			((ProjectPrizm3)this._appProjects[0]).ScreenSaverTime = pScreenSaverTime;
        //		}
        //	}

        //	public void SetScreenSaverSettings(int pProjectID, CommonConstants.ScreenSaverSettings objSettings)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetScreenSaverSettings(objSettings);
        //		}
        //	}

        //	public void SetScreenTasksListIDList(int pProjectID, int pScreenNumber, List<int> pBeforeShowingTaskIDList, List<int> pWhileShowingTaskIDList, List<int> pAfterHidingTaskIDList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetScreenTasksListIDList(pScreenNumber, pBeforeShowingTaskIDList, pWhileShowingTaskIDList, pAfterHidingTaskIDList);
        //		}
        //	}

        //	public void SetScreenWidthHeight(int pProjectID, int ProductID, int Width, int height, float zoomH, float zoomY, Graphics g, int colorSource, int colorDest)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetScreenWidthHeight(ProductID, Width, height, zoomH, zoomY, g, colorSource, colorDest);
        //		}
        //	}

        //	public void SetSerialDriverSettings(int pProjectID, CommonConstants.UniversalSerialDriver pobjSerialDriver, byte pbtPort)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetSerialDriverSettings(pobjSerialDriver, pbtPort);
        //		}
        //	}

        //	public void SetSerialDriverSettingsIEC(int pProjectID, CommonConstants.UniversalSerialDriverIEC pobjSerialDriver, byte pbtPort)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetSerialDriverSettingsIEC(pobjSerialDriver, pbtPort);
        //		}
        //	}

        //	public void SetSerialIOInfo(int Type, int pProjectID, CommonConstants.PlcModuleHeaderInfo objHeaderInfo, ArrayList objListModuleInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetSerialIOInfo(Type, objHeaderInfo, objListModuleInfo);
        //		}
        //	}

        //	public void SetShowAlphanumericGrid(int pProjectId, bool pblShowAlphanumericGrid)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetShowAlphanumericGrid(pblShowAlphanumericGrid);
        //		}
        //	}

        //	public void SetShowTouchGrid(int pProjectId, bool pblShowTouchGrid)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetShowTouchGrid(pblShowTouchGrid);
        //		}
        //	}

        //	public void SetSnapToGrid(int pProjectId, bool pblSnapToGridFlag)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetSnapToGridFlag(pblSnapToGridFlag);
        //		}
        //	}

        //	public void SetState(int pProjectIndex, int pState, bool pIncrDecr)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectIndex);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetState(pState, pIncrDecr);
        //		}
        //	}

        //	public void SetSystemMemoryData(int pProjectID, ArrayList objListData)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetSystemMemoryData(objListData);
        //		}
        //	}

        //	public void SetTagIDForOperandAddress(int pProjectId, string strAddress, int TagID, int NoOfBytes)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetTagIDForOperandAddress(strAddress, TagID, NoOfBytes);
        //		}
        //	}

        //	public void SetTagIDFromName_ForUndefinedTag(int pProjectId, string strAddress, string strName, int TagID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetTagIDFromName_ForUndefinedTag(strAddress, strName, TagID);
        //		}
        //	}

        //	public void SetTagInfo(int piProjectIndex, int piTagID, int piNodePort, int piNodeAddress, int piBlockNumber, int piTagNumber)
        //	{
        //	}

        //	public void SetTagName(int pProjectId, string pstrTagName, int piTagId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetTagName(pstrTagName, piTagId);
        //		}
        //	}

        //	public void SetTagNameDownload(int pProjectID, byte pbtTagNameDownload)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetTagNameDownload(pbtTagNameDownload);
        //		}
        //	}

        //	public void SetTagValue(int pProjectID, string TagName, string tagValue)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetTagValue(TagName, tagValue);
        //		}
        //	}

        //	public void SetTextColor(int pProjectID, Color pColor, int piScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SetTextColor(pColor, piScreenNumber);
        //		}
        //	}

        //	public void SetTimerLadderTags(int pProjectID, Hashtable TimerTags, ArrayList OtherBlockTags)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetTimerLadderTags(TimerTags, OtherBlockTags);
        //		}
        //	}

        //	public void SetTimeTagsId(int pProjectId, int[] TimeTagsId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetTimeTagsTagId(TimeTagsId);
        //		}
        //	}

        //	public void SetTouchScreenTasksListIDList(int pProjectID, int pScreenNumber, int pShapeIndex, List<int> pPressTaskIDList, List<int> pPressedTaskIDList, List<int> pReleasedTaskIDList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetTouchScreenTasksListIDList(pScreenNumber, pShapeIndex, pPressTaskIDList, pPressedTaskIDList, pReleasedTaskIDList);
        //		}
        //	}

        //	public void SetUserName(int pUserID)
        //	{
        //		CommonConstants.UserData userData = new CommonConstants.UserData();
        //		userData = this.GetUserData(pUserID);
        //		AuditTrails.UserName = userData._userName;
        //	}

        //	public void SetUSSDriverSettings(int pProjectID, CommonConstants.USSDriverInfo objUSSSettings)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).SetUSSDriverSettings(objUSSSettings);
        //		}
        //	}

        //	private bool SetVerticalByte(int ProductId)
        //	{
        //		bool flag = false;
        //		if (ProductId == 1256)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public void Show16PointLinearization(int piProjectIndex)
        //	{
        //		((ProjectPrizm3)this._appProjects[piProjectIndex]).Show16PointLinearization();
        //	}

        //	public bool ShowAlphanumericGrid(int pProjectId)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).ShowAlphanumericGrid());
        //		return flag;
        //	}

        //	public void ShowCommentArea(int pProjectId, int ScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).ShowCommentArea(ScreenNumber);
        //		}
        //	}

        //	public bool ShowTouchGrid(int pProjectId)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		flag = (projectIndex == -1 ? false : ((ProjectPrizm3)this._appProjects[projectIndex]).ShowTouchGrid());
        //		return flag;
        //	}

        //	private ArrayList SortCapCharData(ArrayList parrAddressData)
        //	{
        //		parrAddressData.TrimToSize();
        //		ArrayList arrayLists = new ArrayList();
        //		for (int i = 0; i < parrAddressData.Count; i++)
        //		{
        //			if (char.IsUpper(((CommonConstants.Prizm3TagStructure)parrAddressData[i])._TagAddress.Substring(0, 1), 0))
        //			{
        //				int num = Convert.ToInt32(((CommonConstants.Prizm3TagStructure)parrAddressData[i])._BitNumber);
        //				if (num == 0)
        //				{
        //					arrayLists.Add(((CommonConstants.Prizm3TagStructure)parrAddressData[i])._TagAddress.ToString());
        //				}
        //				else
        //				{
        //					arrayLists.Add(string.Concat(((CommonConstants.Prizm3TagStructure)parrAddressData[i])._TagAddress.ToString(), "_", this.GetBitNumberStringValues(num)));
        //				}
        //			}
        //		}
        //		arrayLists.Sort();
        //		arrayLists.TrimToSize();
        //		return arrayLists;
        //	}

        //	private ArrayList SortSmallCharData(ArrayList parrAddressData)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		for (int i = 0; i < parrAddressData.Count; i++)
        //		{
        //			if (char.IsLower(((CommonConstants.Prizm3TagStructure)parrAddressData[i])._TagAddress.Substring(0, 1), 0))
        //			{
        //				arrayLists.Add(((CommonConstants.Prizm3TagStructure)parrAddressData[i])._TagAddress.ToString());
        //			}
        //		}
        //		arrayLists.Sort();
        //		arrayLists.TrimToSize();
        //		return arrayLists;
        //	}

        //	public void SpaceEvenHorizontally(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SpaceEvenHorizontally(pScreenNumber);
        //		}
        //	}

        //	public void SpaceEvenVertically(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SpaceEvenVertically(pScreenNumber);
        //		}
        //	}

        //	public void SwapTagName(int pProjectID, int piFirstTagNameIndex, int piSecondTagNameIndex)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).SwapTagNameIndex(piFirstTagNameIndex, piSecondTagNameIndex);
        //		}
        //	}

        //	public bool SystemTagCheck(int pProjectID, CommonConstants.Prizm3TagStructure pobjTagStructure)
        //	{
        //		bool flag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		flag = (projectIndex == -1 ? false : ((Project)this._appProjects[projectIndex]).SystemTagCheck(pobjTagStructure));
        //		return flag;
        //	}

        //	public void UndoAction(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).UndoAction(pScreenNumber);
        //		}
        //	}

        //	public void UpdateAlarmLanguageList(int pProjectID, List<CommonConstants.LanguageInformation> _almMngrLangList)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).UpdateAlarmLanguageList(_almMngrLangList);
        //		}
        //	}

        //	public void UpdateAllDataEntryScreenNumber(int pProjectID, ushort pOldScreenNumber, ushort pNewScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).UpdateAllDataEntryScreenNumber(pOldScreenNumber, pNewScreenNumber);
        //		}
        //	}

        //	public AddNode UpdateAllNodeOnSamePort(string pstrNodeName, int pProjectID, CommonConstants.NodeInfo pNodeInfo)
        //	{
        //		AddNode addNode = AddNode.None;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			addNode = ((Project)this._appProjects[projectIndex]).UpdateAllNodeOnSamePort(pstrNodeName, pNodeInfo);
        //		}
        //		return addNode;
        //	}

        //	public void UpdateBlockName_InCallInstruction(int pProjectId, string strBlkName, string strNewName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).UpdateBlockName_InCallInstruction(strBlkName, strNewName);
        //		}
        //	}

        //	public AddNode UpdateG9SPNode(string pstrNodeName, int pProjectID, CommonConstants.G9SPNodeInfo pNodeInfo)
        //	{
        //		AddNode addNode = AddNode.None;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			addNode = ((Project)this._appProjects[projectIndex]).UpdateG9SPNode(pstrNodeName, pNodeInfo);
        //		}
        //		return addNode;
        //	}

        //	public AddNode UpdateNode(string pstrNodeName, int pProjectID, CommonConstants.NodeInfo pNodeInfo)
        //	{
        //		AddNode addNode = AddNode.None;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			addNode = ((Project)this._appProjects[projectIndex]).UpdateNode(pstrNodeName, pNodeInfo);
        //		}
        //		return addNode;
        //	}

        //	public void UpdateOperandAddress(int pProjectId, int pScreenNumber, CommonConstants.LadderOperandInfo objLadderOperandInfo)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).UpdateOperandAddress(pScreenNumber, objLadderOperandInfo);
        //		}
        //	}

        //	public AddNode UpdateProtocol(string pstrProtocolName, string pstrModelName, string pstrPrevModel, int pProjectID, CommonConstants.NodeInfo pNodeInfo)
        //	{
        //		AddNode addNode = AddNode.None;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			addNode = ((Project)this._appProjects[projectIndex]).UpdateProtocol(pstrProtocolName, pstrModelName, pstrPrevModel, pNodeInfo);
        //		}
        //		return addNode;
        //	}

        //	public void UpdateRungCommentText(int pProjectId, int screenNumber, int RungNo, string strCommentText)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).UpdateRungCommentText(screenNumber, RungNo, strCommentText);
        //		}
        //	}

        //	public void UpdateScreen(int pProjectID, CommonConstants.ScreenInfo pScreenInfo, int pIndex)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).UpdateScreen(pScreenInfo, pIndex);
        //		}
        //	}

        //	public void UpdateScreen_DirtyFlag(int pProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).UpdateScreen_DirtyFlag();
        //		}
        //	}

        //	public void UpdateScreenName(int pProjectID, string pstrOldScreenName, string pstrNewScreenName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).UpdateScreenName(pstrOldScreenName, pstrNewScreenName);
        //		}
        //	}

        //	public void UpdateSerialDriverSettings(int pProjectID, CommonConstants.UniversalSerialDriver pobjSerialDriver, byte pbtPort, bool pblUpdateFlag)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).UpdateSerialDriverSettings(pobjSerialDriver, pbtPort, pblUpdateFlag);
        //		}
        //	}

        //	public void UpdateSerialDriverSettingsIEC(int pProjectID, CommonConstants.UniversalSerialDriverIEC pobjSerialDriver, byte pbtPort, bool pblUpdateFlag)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).UpdateSerialDriverSettingsIEC(pobjSerialDriver, pbtPort, pblUpdateFlag);
        //		}
        //	}

        //	public UpdateTag UpdateTagData(int pProjectID, CommonConstants.Prizm3TagStructure pTagStruct, string pstrTagAddress)
        //	{
        //		UpdateTag updateTag;
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		updateTag = (projectIndex == -1 ? UpdateTag.NONE : ((Project)this._appProjects[projectIndex]).UpdateTagData(pTagStruct, pstrTagAddress));
        //		return updateTag;
        //	}

        //	public void UpdateTagGroup(int pProjectID, int pKey, string pNewGrpnm)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).UpdateTagGroup(pKey, pNewGrpnm);
        //		}
        //	}

        //	public void UpdateTagInfo_LadderInstructions(int pProjectId, string strOldAddress, string strNewAddress, string strNewName)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).UpdateTagInfo_LadderInstructions(strOldAddress, strNewAddress, strNewName);
        //		}
        //	}

        //	public void updateTemplateScreenInfoImportScreen(ClassList.Screen OldScreen, ushort ScreenNumber, string screenName, int ImportProjectID)
        //	{
        //		int projectIndex = this.GetProjectIndex(ImportProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((ProjectPrizm3)this._appProjects[projectIndex]).updateTemplateScreenInfoImportScreen(OldScreen, ScreenNumber, screenName);
        //		}
        //	}

        //	public void UpdateTextForLanguage(int pProjectId)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectId);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).UpdateTextForLangauge();
        //		}
        //	}

        //	public void UpdateUserData(string puserName, CommonConstants.UserData puserData)
        //	{
        //		this._appUserManager.EditUser(this._appUserManager.FindUser(puserName), puserData);
        //		int num = 0;
        //		while (num < this.LoginList.Count)
        //		{
        //			if (!(puserName == ((ClassList.Application.LoginInfo)this.LoginList[num]).strUserName))
        //			{
        //				num++;
        //			}
        //			else
        //			{
        //				this._appLoginInfo.iUserID = puserData._userID;
        //				this._appLoginInfo.strUserName = puserData._userName;
        //				this._appLoginInfo.strPwd = puserData._userPassword;
        //				this._appLoginList.RemoveAt(num);
        //				this._appLoginList.Insert(num, this._appLoginInfo);
        //				break;
        //			}
        //		}
        //	}

        //	public void UpdateUserNamePassword(int pUserID)
        //	{
        //		CommonConstants.UserData userData = new CommonConstants.UserData();
        //		userData = this.GetUserData(pUserID);
        //		this.LastUser = userData._userName;
        //		this.Password = userData._userPassword;
        //	}

        //	public void UpdateUserPassword()
        //	{
        //		this._appUserManager.UserList = this.UserList;
        //	}

        //	public void UseAsDefault(int pProjectID, int IActiveScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).UseAsDefault(IActiveScreenNumber);
        //		}
        //	}

        //	public void VerticalFlip(int pProjectID, int pScreenNumber)
        //	{
        //		int projectIndex = this.GetProjectIndex(pProjectID);
        //		if (projectIndex != -1)
        //		{
        //			((Project)this._appProjects[projectIndex]).VerticalFlip(pScreenNumber);
        //		}
        //	}

        //	public void WriteApplicationFile()
        //	{
        //		FileStream fileStream = null;
        //		try
        //		{
        //			fileStream = new FileStream(this._appFileType, FileMode.Create, FileAccess.Write);
        //			this.WriteApplicationFileHeader(fileStream);
        //			this.WriteData(fileStream, Convert.ToInt32(ApplicationFileConstants.AUTHENTICATEUSERS), Convert.ToByte(TypeConstants.BOOLEAN), this.AuthenticateUser);
        //			this.WriteData(fileStream, Convert.ToInt32(ApplicationFileConstants.AUDITTRAILS), Convert.ToByte(TypeConstants.BOOLEAN), this.AuditTrail);
        //			this.WriteData(fileStream, Convert.ToInt32(ApplicationFileConstants.LANGUAGE), Convert.ToByte(TypeConstants.STRING), this.Language);
        //			this.WriteData(fileStream, Convert.ToInt32(ApplicationFileConstants.USERID), Convert.ToByte(TypeConstants.INT), this.UserID);
        //			this.WriteData(fileStream, Convert.ToInt32(ApplicationFileConstants.LASTUSER), Convert.ToByte(TypeConstants.STRING), this.LastUser);
        //			this.WriteData(fileStream, Convert.ToInt32(ApplicationFileConstants.PASSWORD), Convert.ToByte(TypeConstants.STRING), this.Password);
        //			this.WriteData(fileStream, Convert.ToInt32(ApplicationFileConstants.REMEMBERME), Convert.ToByte(TypeConstants.BOOLEAN), this.RememberMe);
        //			this.WriteData(fileStream, Convert.ToInt32(ApplicationFileConstants.DATETIMELASTLOGIN), Convert.ToByte(TypeConstants.STRING), this.LastLoginDateTime);
        //			fileStream.Close();
        //		}
        //		catch (IOException oException)
        //		{
        //			MessageBox.Show(oException.Message);
        //		}
        //	}

        //	public void WriteApplicationFileHeader(FileStream fp)
        //	{
        //		this.WriteData(fp, Convert.ToInt32(FileHeaderConstants.SIGNATURE), Convert.ToByte(TypeConstants.STRING), this._appSignature);
        //		this.WriteData(fp, Convert.ToInt32(FileHeaderConstants.FILETYPE), Convert.ToByte(TypeConstants.STRING), this._appFileType);
        //		this.WriteData(fp, Convert.ToInt32(FileHeaderConstants.PRODUCT), Convert.ToByte(TypeConstants.STRING), this._appProduct);
        //		this.WriteData(fp, Convert.ToInt32(FileHeaderConstants.MAJORVERSION), Convert.ToByte(TypeConstants.BYTE), this._appMajorVersion);
        //		this.WriteData(fp, Convert.ToInt32(FileHeaderConstants.MINORVERSION), Convert.ToByte(TypeConstants.BYTE), this._appMinorVersion);
        //		this.WriteData(fp, Convert.ToInt32(FileHeaderConstants.BUILD), Convert.ToByte(TypeConstants.SHORT), this._appBuild);
        //		this.WriteData(fp, Convert.ToInt32(FileHeaderConstants.CRC), Convert.ToByte(TypeConstants.BYTE), this._appCRC);
        //	}

        //	public void WriteData(FileStream fp, int pTagID, byte pTagType, object pTagValue)
        //	{
        //		byte[] numArray = new byte[4];
        //		byte[] numArray1 = new byte[2];
        //		byte[] numArray2 = new byte[8];
        //		UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
        //		CommonConstants.BREAKINT(numArray, pTagID);
        //		fp.Write(numArray, 0, 4);
        //		if (pTagType == Convert.ToByte(TypeConstants.STRING))
        //		{
        //			fp.WriteByte(Convert.ToByte(TypeConstants.STRING));
        //			fp.WriteByte(Convert.ToByte(unicodeEncoding.GetByteCount(Convert.ToString(pTagValue))));
        //			byte[] bytes = new byte[unicodeEncoding.GetByteCount(Convert.ToString(pTagValue))];
        //			bytes = unicodeEncoding.GetBytes(Convert.ToString(pTagValue));
        //			fp.Write(bytes, 0, unicodeEncoding.GetByteCount(Convert.ToString(pTagValue)));
        //		}
        //		else if (pTagType == Convert.ToByte(TypeConstants.BYTE))
        //		{
        //			fp.WriteByte(Convert.ToByte(TypeConstants.BYTE));
        //			fp.WriteByte(Convert.ToByte(pTagValue));
        //		}
        //		else if (pTagType == Convert.ToByte(TypeConstants.SHORT))
        //		{
        //			fp.WriteByte(Convert.ToByte(TypeConstants.SHORT));
        //			CommonConstants.BREAKWORD(numArray1, Convert.ToInt16(pTagValue));
        //			fp.Write(numArray1, 0, 2);
        //		}
        //		else if (pTagType == Convert.ToByte(TypeConstants.USHORT))
        //		{
        //			fp.WriteByte(Convert.ToByte(TypeConstants.USHORT));
        //			CommonConstants.BREAKWORD(numArray1, Convert.ToInt16(pTagValue));
        //			fp.Write(numArray1, 0, 2);
        //		}
        //		else if (pTagType == Convert.ToByte(TypeConstants.INT))
        //		{
        //			fp.WriteByte(Convert.ToByte(TypeConstants.INT));
        //			CommonConstants.BREAKINT(numArray, Convert.ToInt32(pTagValue));
        //			fp.Write(numArray, 0, 4);
        //		}
        //		else if (pTagType == Convert.ToByte(TypeConstants.UINT))
        //		{
        //			fp.WriteByte(Convert.ToByte(TypeConstants.UINT));
        //			CommonConstants.BREAKINT(numArray, Convert.ToInt32(pTagValue));
        //			fp.Write(numArray, 0, 4);
        //		}
        //		else if (pTagType == Convert.ToByte(TypeConstants.LONG))
        //		{
        //			fp.WriteByte(Convert.ToByte(TypeConstants.LONG));
        //		}
        //		else if (pTagType != Convert.ToByte(TypeConstants.ULONG))
        //		{
        //			fp.WriteByte(Convert.ToByte(TypeConstants.BOOLEAN));
        //			fp.WriteByte(Convert.ToByte(pTagValue));
        //		}
        //		else
        //		{
        //			fp.WriteByte(Convert.ToByte(TypeConstants.ULONG));
        //		}
        //	}

        //	public void WriteLoginFile()
        //	{
        //		FileStream fileStream = null;
        //		fileStream = new FileStream("Login.dat", FileMode.Create, FileAccess.Write);
        //		for (int i = 0; i < this.LoginList.Count; i++)
        //		{
        //			this.WriteData(fileStream, Convert.ToInt32(UserFileConstants.USERID), Convert.ToByte(TypeConstants.UINT), ((ClassList.Application.LoginInfo)this.LoginList[i]).iUserID);
        //			this.WriteData(fileStream, Convert.ToInt32(UserFileConstants.USERNAME), Convert.ToByte(TypeConstants.STRING), ((ClassList.Application.LoginInfo)this.LoginList[i]).strUserName);
        //			this.WriteData(fileStream, Convert.ToInt32(UserFileConstants.PASSWORD), Convert.ToByte(TypeConstants.STRING), ((ClassList.Application.LoginInfo)this.LoginList[i]).strPwd);
        //		}
        //		fileStream.Close();
        //	}

        //	public void WriteUserData()
        //	{
        //		this._appUserManager.WriteUsersFile();
        //		this._appUserManager.DirtyFlag = false;
        //	}

        //	public event ClassList.Application.ActivateSimulationWindow _applicationactivateSimulationWindow;

        //	public event ClassList.Application.CloseSimulationWindow _applicationcloseSimulationWindow;

        //	public event ClassList.Application.DownloadPercentage _applicationDownloadPercentage;

        //	public event ClassList.Application.DownloadStatus _applicationDownloadStatus;

        //	public event ClassList.Application.ExportObjectsProgressBar _applicationExportObjectsProgressBar;

        //	public event ClassList.Application.ExportProgressValue _applicationExportProgressValue;

        //	public event ClassList.Application.ImportProgressValue _applicationImportProgressValue;

        //	public event ClassList.Application.AddNodeStatusTagOnImport _evntAddNodeStatusTagOnImport;

        //	public event ClassList.Application.AddTagInStratonVMDB _evntAddTagInStratonVMDB;

        //	public event ClassList.Application.ImportModbusSlaveTagAtCom1Com2 _importModbusSlaveTagAtCom1Com2;

        //	public event ClassList.Application.ImportModbusSlaveTagAtEthernet _importModbusSlaveTagAtEthernet;

        //	public event ClassList.Application.ImportNodeSelection _importNodeSelection;

        //	public event ClassList.Application.ImportSlaveTagAtCom1Com2 _importSlaveTagAtCom1Com2;

        //	public event ClassList.Application.ImportSlaveTagAtEthernet _importSlaveTagAtEthernet;

        //	public event ClassList.Application.ShowImportTags _ShowImportTags;

        //	public delegate void ActivateSimulationWindow();

        //	public delegate void AddNodeStatusTagOnImport(string pNodename);

        //	public delegate bool AddTagInStratonVMDB(string pstrPrefix, byte btStratonBlockType, string InitialValue, string GroupName, CommonConstants.Prizm3TagStructure pobjTagdata);

        //	public delegate void CloseSimulationWindow();

        //	public delegate void DownloadPercentage(float pPercentage);

        //	public delegate void DownloadStatus(int pMessage);

        //	public delegate void ExportObjectsProgressBar(int piObjCount, int piTotalObjects, string pstrScreenName);

        //	public delegate void ExportProgressValue(int piProgressValue, int piTotalTags, bool pblExportFinished);

        //	public delegate int ImportModbusSlaveTagAtCom1Com2(string port);

        //	public delegate int ImportModbusSlaveTagAtEthernet(string port);

        //	public delegate string ImportNodeSelection(ArrayList arr);

        //	public delegate void ImportProgressValue(int piProgressValue, int piTotalNodeTag);

        //	public delegate int ImportSlaveTagAtCom1Com2(string port);

        //	public delegate int ImportSlaveTagAtEthernet(string port);

        //	public struct LoginInfo
        //	{
        //		public int iUserID;

        //		public string strUserName;

        //		public string strPwd;
        //	}

        //	public delegate void ShowImportTags(ArrayList _totalTagList);
    }
}