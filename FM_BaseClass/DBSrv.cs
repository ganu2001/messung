namespace K5
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using WinAPI;

    public class DBSrv
    {
        public const int K5DB_OK = 0;
        public const int K5DBERR_INTERNAL = 1;
        public const int K5DBERR_SAMERENAME = 2;
        public const int K5DBERR_CREATEPROJECT = 3;
        public const int K5DBERR_UNKNOWNCLIENT = 4;
        public const int K5DBERR_UNKNOWNPROJECT = 5;
        public const int K5DBERR_PROJECTSHARED = 6;
        public const int K5DBERR_DELETEPROJECT = 7;
        public const int K5DBERR_RENAMEPROJECT = 8;
        public const int K5DBERR_COPYPROJECT = 9;
        public const int K5DBERR_UNKNOWNPROGRAM = 10;
        public const int K5DBERR_DUPPROGNAME = 11;
        public const int K5DBERR_BADPROGNAME = 12;
        public const int K5DBERR_DELPARENTPROG = 13;
        public const int K5DBERR_PROGLOCKED = 14;
        public const int K5DBERR_BADPRGMOVE = 15;
        public const int K5DBERR_TOOMANYPROGS = 0x10;
        public const int K5DBERR_BADPROGSECTION = 0x11;
        public const int K5DBERR_BADPROGLANGUAGE = 0x12;
        public const int K5DBERR_UNKNOWNGROUP = 0x13;
        public const int K5DBERR_CANTDELGROUP = 20;
        public const int K5DBERR_GROUPLOCKED = 0x15;
        public const int K5DBERR_CANTDELIOVAR = 0x16;
        public const int K5DBERR_UNKNOWNVAR = 0x17;
        public const int K5DBERR_VARLOCKED = 0x18;
        public const int K5DBERR_CANTRENIOVAR = 0x19;
        public const int K5DBERR_DUPVARNAME = 0x1a;
        public const int K5DBERR_BADVARNAME = 0x1b;
        public const int K5DBERR_UNKNOWNTYPE = 0x1c;
        public const int K5DBERR_BADIOTYPE = 0x1d;
        public const int K5DBERR_IOVARDIM = 30;
        public const int K5DBERR_TYPEDIM = 0x1f;
        public const int K5DBERR_BADDIM = 0x20;
        public const int K5DBERR_NOSTRINGLEN = 0x21;
        public const int K5DBERR_BADSTRINGLEN = 0x22;
        public const int K5DBERR_CANTCHANGEATTR = 0x23;
        public const int K5DBERR_CANTCHANGERO = 0x24;
        public const int K5DBERR_CANTALIASMEMVAR = 0x25;
        public const int K5DBERR_CANTINITFB = 0x26;
        public const int K5DBERR_CANTINITARRAY = 0x27;
        public const int K5DBERR_BADINITVALUE = 40;
        public const int K5DBERR_NOFBIOINIT = 0x29;
        public const int K5DBERR_FBIOVARDIM = 0x2a;
        public const int K5DBERR_UNKNOWNOBJECT = 0x2b;
        public const int K5DBERR_BADCOMMLANGUAGE = 0x2c;
        public const int K5DBERR_NOVARMLINECOMM = 0x2d;
        public const int K5DBERR_NORETAININST = 0x2e;
        public const int K5DBERR_EXPCANTCREATE = 0x2f;
        public const int K5DBERR_IMPCANTREAD = 0x30;
        public const int K5DBERR_IMPBADFILE = 0x31;
        public const int K5DBERR_IMPCREATEPROG = 50;
        public const int K5DBERR_EXPHIDEUDFB = 0x33;
        public const int K5DBERR_EXPHIDENOCODE = 0x34;
        public const int K5DBERR_HOTENABLED = 0x35;
        public const int K5DBERR_XV = 0x36;
        public const int K5DBERR_DLGASKCREATEVAR = 0x37;
        public const int K5DBERR_DLGTYPE = 0x38;
        public const int K5DBERR_DLGYES = 0x39;
        public const int K5DBERR_DLGNO = 0x3a;
        public const int K5DBERR_DLGCANCEL = 0x3b;
        public const int K5DBERR_DLGCREATEVARKO = 60;
        public const int K5DBERR_CANTDELFOLDER = 0x3d;
        public const int K5DBERR_CANTRENFOLDER = 0x3e;
        public const int K5DBERR_CANTMOVECHILDTOFOLDER = 0x3f;
        public const int K5DBERR_CANTMOVETOPFOLDER = 0x40;
        public const int K5DBERR_CANTMOVEFOLDERSAME = 0x41;
        public const int K5DBERR_CANTMOVEFOLDERRECURSE = 0x42;
        public const int K5DBERR_FILELOCKED = 0x43;
        public const int K5DBERR_BADFILEEXT = 0x44;
        public const int K5DBERR_DUPFILENAME = 0x45;
        public const int K5DBERR_BADFILENAME = 70;
        public const int K5DBERR_CRYPTED = 0x47;
        public const int K5DBERR_DLGASKVAR = 0x48;
        public const int K5DBERR_DLGASKCREATE = 0x49;
        public const int K5DBERR_DLGASKRENAME = 0x4a;
        public const int K5DBERR_DLGWHERE = 0x4b;
        public const int K5DBERR_BADENUM = 0x4c;
        public const int K5DBERR_DUPTYPENAME = 0x4d;
        public const int K5DBERR_DLGVARS = 0x4e;
        public const int K5DBERR_DLGKWS = 0x4f;
        public const int K5DBERR_DLGNOGROUP = 80;
        public const int K5DBERR_DLGALL = 0x51;
        public const int K5DBOBJ_CLIENT = 1;
        public const int K5DBOBJ_PROJECT = 2;
        public const int K5DBOBJ_PROGRAM = 3;
        public const int K5DBOBJ_GROUP = 4;
        public const int K5DBOBJ_TYPE = 5;
        public const int K5DBOBJ_VAR = 6;
        public const int K5DBOBJ_UNKNOWN = 7;
        public const int K5DBOBJ_FOLDER = 8;
        public const int K5DBOBJ_FILE = 9;
        public const uint K5DBCNX_SELFNOTIF = 1;
        public const uint K5DBEV_PRJRENAMED = 0x10001;
        public const uint K5DBEV_PRJRELOADED = 0x10002;
        public const uint K5DBSECTION_BEGIN = 1;
        public const uint K5DBSECTION_SFC = 2;
        public const uint K5DBSECTION_END = 4;
        public const uint K5DBSECTION_CHILD = 8;
        public const uint K5DBSECTION_UDFB = 0x10;
        public const uint K5DBSECTION_MAIN = 7;
        public const uint K5DBSECTION_PROG = 15;
        public const uint K5DBSECTION_ANY = 0xff;
        public const int K5DBLANGUAGE_SFC = 1;
        public const int K5DBLANGUAGE_ST = 2;
        public const int K5DBLANGUAGE_FBD = 4;
        public const int K5DBLANGUAGE_LD = 8;
        public const int K5DBLANGUAGE_IL = 0x10;
        public const int K5DBLANGUAGE_ANY = 0x1f;
        public const uint K5DBEV_PRGNEW = 0x20001;
        public const uint K5DBEV_PRGDUPLICATED = 0x20002;
        public const uint K5DBEV_PRGRENAMED = 0x20003;
        public const uint K5DBEV_PRGDELETED = 0x20004;
        public const uint K5DBEV_PRGMOVED = 0x20005;
        public const uint K5DBEV_PRGCOPIED = 0x20006;
        public const uint K5DBEV_PRGLOCKED = 0x20007;
        public const uint K5DBEV_PRGCHANGED = 0x20008;
        public const uint K5DBEV_PRGUNLOCKED = 0x20009;
        public const uint K5DBEV_PRGVARCHANGED = 0x2000a;
        public const uint K5DBTYPE_INVALID = 0x1000;
        public const uint K5DBTYPE_BASIC = 1;
        public const uint K5DBTYPE_STRING = 2;
        public const uint K5DBTYPE_IO = 4;
        public const uint K5DBTYPE_CSTRUCT = 8;
        public const uint K5DBTYPE_STDFB = 0x10;
        public const uint K5DBTYPE_CFB = 0x20;
        public const uint K5DBTYPE_UDFB = 0x40;
        public const uint K5DBTYPE_FB = 240;
        public const uint K5DBTYPE_ENUM = 0x100;
        public const uint K5DBTYPE_BITFIELD = 0x200;
        public const uint K5DBTYPE_SINGLE = 0x301;
        public const uint K5DBEV_TYPENEW = 0x50001;
        public const uint K5DBEV_TYPERENAMED = 0x50003;
        public const uint K5DBEV_TYPEDELETED = 0x50004;
        public const uint K5DBEV_TYPECHANGED = 0x50007;
        public const string K5DBGROUPNAME_GLOBAL = "(Global)";
        public const string K5DBGROUPNAME_RETAIN = "(Retain)";
        public const int K5DBGROUP_GLOBAL = 1;
        public const int K5DBGROUP_RETAIN = 2;
        public const int K5DBGROUP_INPUT = 3;
        public const int K5DBGROUP_OUTPUT = 4;
        public const int K5DBGROUP_CPXINPUT = 5;
        public const int K5DBGROUP_CPXOUTPUT = 6;
        public const int K5DBGROUP_LOCAL = 7;
        public const int K5DBGROUP_UDFB = 8;
        public const uint K5DBEV_GROUPNEW = 0x40001;
        public const uint K5DBEV_GROUPRENAMED = 0x40002;
        public const uint K5DBEV_GROUPDELETED = 0x40003;
        public const uint K5DBEV_GROUPLOCKED = 0x40004;
        public const uint K5DBEV_GROUPCHANGED = 0x40005;
        public const uint K5DBEV_GROUPUNLOCKED = 0x40006;
        public const uint K5DBEV_GROUPMOVED = 0x40007;
        public const uint K5DBEV_GROUPOWNED = 0x40008;
        public const uint K5DBEV_GROUPRELEASED = 0x40009;
        public const uint K5DBVAR_READONLY = 1;
        public const uint K5DBVAR_INPUT = 2;
        public const uint K5DBVAR_OUTPUT = 4;
        public const uint K5DBVAR_FBINPUT = 8;
        public const uint K5DBVAR_FBOUTPUT = 0x10;
        public const uint K5DBVAR_EXTERN = 0x20;
        public const uint K5DBVAR_INOUT = 0x40;
        public const uint K5DBVAR_ADDED = 0x100;
        public const uint K5DBVAR_DELETED = 0x200;
        public const uint K5DBVAR_LOCALRETAIN = 0x400;
        public const uint K5DBEV_VARNEW = 0x70001;
        public const uint K5DBEV_VARRENAMED = 0x70002;
        public const uint K5DBEV_VARDELETED = 0x70003;
        public const uint K5DBEV_VARLOCKED = 0x70004;
        public const uint K5DBEV_VARCHANGED = 0x70005;
        public const uint K5DBEV_VARUNLOCKED = 0x70006;
        public const uint K5DBEV_VARMOVED = 0x70007;
        public const uint K5DBEV_VARBEGINMOVE = 0x70008;
        public const uint K5DBEV_VARENDMOVE = 0x70009;
        public const uint K5DBEV_K5PROPCHANGED = 0x80001;
        public const uint K5DBEV_EXTPROPCHANGED = 0x80002;
        public const int K5DBCOMM_SHORT = 1;
        public const int K5DBCOMM_LONG = 2;
        public const int K5DBCOMM_MULTILINE = 3;
        public const uint K5DBEV_COMMLANGCHANGED = 0x90001;
        public const uint K5DBEV_COMMCHANGED = 0x90003;
        public const uint K5DBVSEL_NOTEXTSEL = 1;
        public const uint K5DBVSEL_CREATEVAR = 2;
        public const uint K5DBVSEL_DEBUG = 4;
        public const uint K5DBVSEL_FOCUSEDIT = 8;
        public const uint K5DBVSEL_DEFAULTPOS = 0x10;
        public const uint K5DBVSEL_COMPLETE = 0x20;
        public const uint K5DBVSEL_SHOWALWAYS = 0x40;
        public const uint K5DBVSEL_CREATEONLY = 0x80;
        public const uint K5DBVSEL_FILTERTYPE = 0x100;
        public const uint K5DBVSEL_FROMLIST = 0x200;
        public const uint K5DBVSEL_GLOBALONLY = 0x400;
        public const uint K5DBVSEL_NOEXPAND = 0x800;
        public const uint K5DBVSEL_CREATEGLOBAL = 0x1000;
        public const uint K5DBVSEL_APPLYOPTIONS = 0x80000000;

        [DllImport("K5IS.dll", EntryPoint = "K5IS_Activate", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Activate(uint hProject, bool value);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_CanCreateProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint CanCreateProgram(uint hClient, uint hProject, uint dwLanguage, uint dwSection, uint hParent, string szProgName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_CanRenameVar", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint CanRenameVar(uint hClient, uint hProject, uint hGroup, uint hVar, string szNewName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_CanSetVarDesc", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint CanSetVarDesc(uint hClient, uint hProject, uint hGroup, uint hVar, uint hType, uint dwDim, uint dwStringLength, uint dwFlags);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_CloseProject", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseProject(uint hClient, uint hProject);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_Connect", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint Connect(IntPtr hwndCallback, uint msgCallback, uint dwFlags, uint dwData, string szClientName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_CopyProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint CopyProgram(uint hClient, uint hProject, uint hPrg, uint hSection, uint hParent, string szPgm);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_CreateProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint CreateProgram(uint hClient, uint hProject, uint dwLanguage, uint dwSection, uint hParent, string szProgName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_CreateVar", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint CreateVar(uint hClient, uint hProject, uint hGroup, uint dwPosID, uint hType, uint dwDim, uint dwStringLength, uint dwFlags, string szVarName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_DeleteProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint DeleteProgram(uint hClient, uint hProject, uint hProgram);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_DeleteVar", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint DeleteVar(uint hClient, uint hProject, uint hGroup, uint hVar);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_Disconnect", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Disconnect(uint hClient);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_ExportProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ExportProgram(uint hClient, uint hProject, uint hPgm, string szPath);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_FindGroup", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint FindGroup(uint hClient, uint hProject, string szGroupName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_FindProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint FindProgram(uint hClient, uint hProject, string szProgName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_FindType", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint FindType(uint hClient, uint hProject, string szType);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_FindVarInGroup", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint FindVarInGroup(uint hClient, uint hProject, uint hGroup, string szVarName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetComment", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetComment(uint hClient, uint hProject, uint hObject, uint dwCommType, StringBuilder szBuffer, uint dwBufSize);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetCommentLength", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetCommentLength(uint hClient, uint hProject, uint hObject, uint dwCommType);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetEqvPath", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetEqvPath(uint hClient, uint hProject, uint bCommon, uint hProgram, StringBuilder szPath);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetGroupDesc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetGroupDesc(uint hClient, uint hProject, uint hGroup, ref uint dwGroupStyle, ref uint hIOType, ref uint dwSlot, ref uint dwSubSlot, ref uint nbVar);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetGroups", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetGroups(uint hClient, uint hProject, uint[] arrGroups);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetKindOfObject", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetKindOfObject(uint dwID);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetNbGroup", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetNbGroup(uint hClient, uint hProject);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetNbProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetNbProgram(uint hClient, uint hProject, uint dwSection);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetNbType", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetNbType(uint hClient, uint hProject);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetNbVar", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetNbVar(uint hClient, uint hProject, uint hGroup);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetProgramDesc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetProgramDesc(uint hClient, uint hProject, uint hProgram, ref uint dwLanguage, ref uint dwSection, ref uint hParent);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetProgramPath", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetProgramPath(uint hClient, uint hProject, uint hProgram, StringBuilder szPath);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetPrograms", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetPrograms(uint hClient, uint hProject, uint dwSection, uint[] arrPrg);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetProperty", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetProperty(uint hClient, uint hProject, uint hVar, uint ID);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetSerBuffer", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetSerBuffer(uint hClient);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetTypeDesc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetTypeDesc(uint hClient, uint hProject, uint hType, ref uint dwFlags);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetTypes", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetTypes(uint hClient, uint hProject, uint[] arrTypes);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetVarDesc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetVarDesc(uint hClient, uint hProject, uint hGroup, uint hVar, ref uint hType, ref uint dwDim, ref uint dwStringLength, ref uint dwFlags);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetVarInitValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetVarInitValue(uint hClient, uint hProject, uint hGroup, uint hVar, IntPtr ptrValue);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetVars", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetVars(uint hClient, uint hProject, uint hGroup, uint[] arrVars);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_GetVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetVersion();
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_ImportProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ImportProgram(uint hClient, uint hProject, string szFilePath, string szPath);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_IsObjectLocked", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint IsObjectLocked(uint dwID);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_LockProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint LockProgram(uint hClient, uint hProject, uint hProgram);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_MoveProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint MoveProgram(uint hClient, uint hProject, uint hProgram, uint type, uint hValue);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_OpenProject", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint OpenProject(uint hClient, string szProjectPath);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_RegisterProgramChanges", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint RegisterProgramChanges(uint hClient, uint hProject, uint hProgram);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_RealeaseSerBuffer", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReleaseSerBuffer(uint hClient);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_RenameProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint RenameProgram(uint hClient, uint hProject, uint hProgram, string szNewName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_RenameVar", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint RenameVar(uint hClient, uint hProject, uint hGroup, uint hVar, string szNewName);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SaveProgramChanges", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SaveProgramChanges(uint hClient, uint hProject, uint hProgram);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SaveProject", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SaveProject(uint hClient, uint hProject);
        [DllImport("W5EditRes.dll", EntryPoint = "W5RES_SelBlockEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SelBlockEx(IntPtr hClient, string path, string name, ref int pin, ref int pout, ref uint ID, ref bool pinst, ref bool pobject);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SelectVar", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SelectVar(uint hClient, uint hProject, string szText, ref str_K5DBvsel k5dbvsel, ref uint phVar);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SetComment", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SetComment(uint hClient, uint hProject, uint hObject, uint dwCommType, string szValue);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SetProgramOnCallFlag", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SetProgramOnCallFlag(uint hClient, uint hProject, uint hProgram, uint dwOnCall);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SetProjectModified", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SetProjectModified(uint hClient, uint hProject);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SetProperty", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SetProperty(uint hClient, uint hProject, uint hVar, uint ID, string value);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SetVarDesc", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SetVarDesc(uint hClient, uint hProject, uint hGroup, uint hVar, uint hType, uint dwDim, uint dwStringLength, uint dwFlags);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SetVarDimensions", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SetVarDimensions(uint hClient, uint hProject, uint hGroup, uint hVar, uint size, IntPtr ptrDim);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_SetVarInitValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SetVarInitValue(uint hClient, uint hProject, uint hGroup, uint hVar, string szValue);
        [DllImport("K5DBSrv.dll", EntryPoint = "K5DB_UnlockProgram", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint UnlockProgram(uint hClient, uint hProject, uint hProgram);

        [StructLayout(LayoutKind.Sequential)]
        public struct str_K5DBvsel
        {
            public uint hParentGroup;
            public  uint hPrefType;
            public IntPtr hwndParent;
            public Types.POINT ptPos;
            public uint dwOptions;
        }
    }
}