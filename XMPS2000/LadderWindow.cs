using ClassList;
using K5;
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using W5;
using WinAPI;
using XMPS1000.Core;
using Application = ClassList.Application;

namespace XMPS1000
{
    public partial class LadderWindow : Form, IXMForm
    {
        private Edit m_Ctrl = null;
        public uint m_Active_ID = 0;
        private int _dpiX = 0;
        private int _dpiY = 32;
        XMPS xm;
        //private Edit m_Ctrl = new Edit();
        private bool _ShowLocal = false;
        private Edit m_Ctrl_Action = new Edit();
        private Edit m_Ctrl_P0 = new Edit();
        private Edit m_Ctrl_P1 = new Edit();
        private Edit m_Ctrl_N = new Edit();
        private Edit m_Ctrl_Notes = new Edit();
        private Edit m_Ctrl_Condition = new Edit();
        public string ScreenName { get; private set; }
        public delegate void RestoreDirtyFlag();
        private RestoreDirtyFlag restoreFlag;
        private IECBlockType _IECBlockType = IECBlockType.LD;
        private CommonConstants.SfcItemInfo m_ItemInfo;
        private SplitContainer splitContainer1;
        private SortableBindingList<StratonBlock> ListStratonBlockInstances;
        private DataGridView dataGridView1;
        private ArrayList _listOfIECDatatypes = new ArrayList();
        private ArrayList _listOfIECBlocks = new ArrayList();
        private string[] _listOfDataTypes = new string[] { "BOOL", "SINT", "USINT", "BYTE", "INT", "UINT", "WORD", "DINT", "UDINT", "DWORD", "LINT", "REAL", "LREAL", "TIME", "Retentive Registers", "STRING" };
        private static Application _application;
        private int _SelProjectID;

        private string blockName;
        private string blockInfo;

        public LadderWindow(string formName, string nodeName)
        {
            InitializeComponent();
            base.MouseDoubleClick += new MouseEventHandler(this.LadderWindow_MouseDoubleClick);
            xm = XMPS.Instance;
            this.AutoScroll = true;
            this.AutoSize = false;
            base.MaximizeBox = true;
            this.blockInfo = formName;
            this.blockName = nodeName;
            CommonConstants.g_hDBClient = DBSrv.Connect(base.Handle, 1025U, 0U, 0U, "XMPS1000");
            CommonConstants.g_hDBProject = DBSrv.OpenProject(CommonConstants.g_hDBClient, xm.LoadedProject.ProjectPath.ToString().Replace(xm.CurrentProjectData.ProjectName, "IEC"));
            bool isActivate = DBSrv.Activate(CommonConstants.g_hDBProject, true);
            uint hPrg = DBSrv.CreateProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, 8U, 4U, (uint)((int)base.Handle), "Block1");
            uint numg = DBSrv.FindGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, "(Global)");
            CheckandLoadorRemoveVariables();
            //CommonConstants.SetActiveFormObject(this);
        }

        internal int FindReplace(string strFind, string strReplace, uint command, uint flags)
        {
            uint num = 0;
            uint num1 = 0;
            uint num2 = 0;
            uint num3 = 0;
            uint num4 = 0;
            IntPtr blockDesc = DBReg.GetBlockDesc(num, ref num1, ref num2, ref num3, ref num4);
            return (m_Ctrl.FindReplace(strFind, strReplace, command, flags, blockDesc));
        }

        internal void SetVar(string str1, string str2)
        {

        }


        public void GetBlockInstanceList(int Count)
        {
            unsafe
            {
                uint num = 0;
                uint num1 = 0;
                uint num2 = 0;
                uint num3 = 0;
                string stringAnsi = "";
                string str = "";
                string stringAnsi1 = "";
                uint num4 = 0;
                uint num5 = 0;
                uint num6 = 0;
                uint num7 = 0;
                uint num8 = 0;
                uint nbGroup = DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject);
                uint[] numArray = new uint[nbGroup];
                this.ListStratonBlockInstances.Clear();
                uint groups = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                for (int i = 0; (ulong)i < (ulong)groups; i++)
                {
                    uint nbVar = DBSrv.GetNbVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[i]);
                    uint[] numArray1 = new uint[nbVar];
                    uint vars = DBSrv.GetVars(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[i], numArray1);
                    for (int j = 0; (ulong)j < (ulong)vars; j++)
                    {
                        IntPtr varDesc = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[i], numArray1[j], ref num, ref num1, ref num2, ref num3);
                        if (varDesc != IntPtr.Zero)
                        {
                            str = Marshal.PtrToStringAnsi(varDesc);
                            DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
                            varDesc = DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, num, ref num1);
                            stringAnsi = Marshal.PtrToStringAnsi(varDesc);
                            if (Array.IndexOf<string>(this._listOfDataTypes, stringAnsi) < 0)
                            {
                                Count++;
                                varDesc = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[i], ref num4, ref num5, ref num6, ref num7, ref num8);
                                stringAnsi1 = Marshal.PtrToStringAnsi(varDesc);
                                this.ListStratonBlockInstances.Add(new StratonBlock(Count, str, stringAnsi, stringAnsi1));
                            }
                        }
                    }
                }
                Count = 0;
            }
        }
        public int _MDISelProjectID
        {
            get
            {
                return this._SelProjectID;
            }
            set
            {
                this._SelProjectID = value;
                CommonConstants.SelectedProjectID = value;
            }
        }
        private void addtag__getLadderScreenName(ref ArrayList objList)
        {
            //_application.GetLadderScreenName(this._MDISelProjectID, ref objList);
        }

        public ArrayList GetDataTypeList()
        {
            unsafe
            {
                ArrayList arrayLists = new ArrayList();
                uint num = 0;
                uint nbType = DBSrv.GetNbType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject);
                if (nbType != 0)
                {
                    uint[] numArray = new uint[nbType];
                    DBSrv.GetTypes(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                    for (int i = 0; (ulong)i < (ulong)nbType; i++)
                    {
                        num = numArray[i];
                        uint num1 = 0;
                        IntPtr typeDesc = DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, num, ref num1);
                        string stringAnsi = Marshal.PtrToStringAnsi(typeDesc);
                        int num2 = Array.IndexOf<string>(this._listOfDataTypes, stringAnsi);
                        if ((stringAnsi == null || !(stringAnsi != "?type?") ? false : num2 < 0))
                        {
                            arrayLists.Add(stringAnsi);
                        }
                    }
                }
                return arrayLists;
            }
        }

        private void AddBlockInstanceData()
        {
            this.ListStratonBlockInstances = new SortableBindingList<StratonBlock>();
            int num = 0;
            this._listOfIECDatatypes.Clear();
            this._listOfIECBlocks.Clear();
            this._listOfIECDatatypes = this.GetDataTypeList();
            this.addtag__getLadderScreenName(ref this._listOfIECBlocks);
            this._listOfIECBlocks.Add(CommonConstants.strGroupName_Global);
            this.GetBlockInstanceList(num);
            //this.dataGridView1.DataSource = null;
            //this.dataGridView1.DataSource = this.ListStratonBlockInstances;
        }

        private void LadderWindow_Load(object sender, EventArgs e)
        {
            this.m_Ctrl = new EditLD();
            this.m_Ctrl.CreateWnd(0x50008000, 0, 0, 100, 100, base.Handle, 0x3e8);
            this.m_Ctrl.Load(xm.CurrentProjectData.ProjectPath.ToString());
            this.m_Ctrl.InsertHorz();
            this.ScreenName = this.Name;
            uint num;
            uint num1 = DBSrv.FindProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, this.Name);
            if (num1 != 0)
            {
                num = DBSrv.DeleteProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, num1);
            }
            uint hPrg = DBSrv.CreateProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, 8U, 4U, (uint)((int)base.Handle), this.Name);
            this.m_Ctrl.SetID(hPrg);
            StringBuilder builder2 = new StringBuilder(0x105);
            uint num20 = DBSrv.GetEqvPath(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, 0, hPrg, builder2);
            DBSrv.SetProgramOnCallFlag(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hPrg, 1);
            AddBlockInstanceData();
            DBSrv.str_K5DBvsel bvsel;
            Types.POINT point;
            StringBuilder szPath = new StringBuilder(0x105);
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            DBSrv.GetProgramPath(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hPrg, szPath);
            //this.m_Ctrl.Load(szPath.ToString());
            string szText = "";
            uint phVar = 0;
            bvsel.hwndParent = base.Handle;
            bvsel.hParentGroup = 0;
            bvsel.hPrefType = 0;
            point.x = 0;
            point.y = 0;
            bvsel.ptPos = point;
            bvsel.dwOptions = 0x80001000;
            DBSrv.SelectVar(0, 0, szText, ref bvsel, ref phVar);
            this._ShowLocal = false;
            this.Cursor = Cursors.Arrow;
            AddBlockInstanceData();
            //Connect 

            /*
            this._screenformWidth = 1088; 
            this._screenformHeight =1088; 
            this._IECBlockType = IECBlockType.LD;
            this.AllowDrop = true;
            base.KeyPreview = true;
            this.Text = this.ScreenName;
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.AutoScroll = true;
            this.AutoSize = true;
            base.MaximizeBox = true;
            uint num2 = 0;
            DBSrv.str_K5DBvsel bvsel;
            Types.POINT point;
            this.Cursor = Cursors.WaitCursor;
            bool flag = false;
            this.m_Ctrl = new EditLD();
            flag = this.m_Ctrl.CreateWnd(0x50008000, 0, 0, 100, 100, base.Handle, 0x3e8);
            this.AutoScroll = false;
            //LoadForm(this._dpiX, this._dpiY, this.Bounds.Width, this.Bounds.Height, base.Handle);
            //Process.WaitForInputIdle();
            //m_Ctrl.DisplayFolio(true); 
            //m_Ctrl.SetRe5adOnly(true);
                
        }
            if (DBSrv.FindProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, this.ScreenName) == 0)
            {
                num2 = DBSrv.CreateProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, 8, 1, (uint)((int)base.Handle), this.ScreenName);
                //num2 = DBSrv.CreateProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, 8, 0x10, (uint)((int)base.Handle), this.ScreenName);
            }
            //if (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width == 800)
            //{
            //    this.m_Ctrl.SetCellWidth(base.Width / 11);
            //}
            //else
            //{
            //    this.m_Ctrl.SetCellWidth(base.Width / 15);
            //}
            string projectPath = "";
            //this.getCurrentProjectPath(ref projectPath);
            //this.m_Ctrl.Resize(this.Bounds.Width, this.Bounds.Height);
            base.Height = this._screenformHeight;
            base.Width = this._screenformWidth;
            //this.m_Ctrl.MoveWindow(this._dpiX, this._dpiY, this.Bounds.Width, this.Bounds.Height);
            this.m_Ctrl.SetProjectPath(xm.LoadedProject.ProjectPath.ToString());
            this.m_Ctrl.SetID(num2);
            StringBuilder szPath = new StringBuilder(0x105);
            DBSrv.GetProgramPath(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, num2, szPath);
            this.m_Ctrl.Load(szPath.ToString());
            string szText = "";
            uint phVar = 0;
            bvsel.hwndParent = base.Handle;
            bvsel.hParentGroup = 0;
            bvsel.hPrefType = 0;
            point.x = 0;
            point.y = 0;
            bvsel.ptPos = point;
            bvsel.dwOptions = 0x80001000;
            DBSrv.SelectVar(0, 0, szText, ref bvsel, ref phVar);
            this._ShowLocal = false;
            this.Cursor = Cursors.Arrow;
            */
            m_Ctrl.SetProjectPath(xm.CurrentProjectData.ProjectPath.ToString());
            string file = null;
            if (blockInfo.Contains("UDFLadder"))
            {
                var projectPath = xm.CurrentProjectData.ProjectPath.ToString().Replace(xm.CurrentProjectData.ProjectName.ToString(), string.Empty);
                string udfFolderPath = System.IO.Path.Combine(projectPath, "UDFs");
                if (Directory.Exists(udfFolderPath))
                {
                    file = System.IO.Path.Combine(udfFolderPath, blockName + ".src");
                }
            }
            else if (blockInfo.Contains("Ladder"))
            {
                file = xm.CurrentProjectData.ProjectPath.ToString().Replace(xm.CurrentProjectData.ProjectName.ToString(), blockName) + ".src";
            }
            if (blockName != "" && file != null && File.Exists(file))
            {
                this.m_Ctrl.Load(file);
            }
            else
            {
                this.m_Ctrl.Load(xm.CurrentProjectData.ProjectPath.ToString());
            }

        }

        public void GetItemInfo()
        {
            //if (this.m_ItemInfo.ItemType == 0)
            //{
            //    this.m_Ctrl.LockStep(false, this.m_ItemInfo.StepNumber);
            //}
            //else if (this.m_ItemInfo.ItemType == 1)
            //{
            //    this.m_Ctrl.LockTrans(false, this.m_ItemInfo.TransNumber);
            //}
            //else if (this.m_ItemInfo.ItemType == 2)
            //{
            //    this.m_Ctrl.LockComment(false, this.m_ItemInfo.CommentNumber);
            //}
            //if (this.m_Ctrl.IsSelStep() > 0)
            //{
            //    this.m_ItemInfo.ItemType = 0;
            //    this.m_ItemInfo.StepNumber = this.m_Ctrl.IsSelStep();
            //    this.m_ItemInfo.CodeAction = this.m_Ctrl.GetStepCode_Def(this.m_ItemInfo.StepNumber);
            //    this.m_ItemInfo.CodeP0 = this.m_Ctrl.GetStepCode_P0(this.m_ItemInfo.StepNumber);
            //    this.m_ItemInfo.LangP0 = !this.m_ItemInfo.CodeP0.Contains("#info=LDEDIT") ? (!this.m_ItemInfo.CodeP0.Contains("#info=WSED1EDT") ? 2 : 1) : 0;
            //    this.m_ItemInfo.CodeP1 = this.m_Ctrl.GetStepCode_P1(this.m_ItemInfo.StepNumber);
            //    this.m_ItemInfo.LangP1 = !this.m_ItemInfo.CodeP1.Contains("#info=LDEDIT") ? (!this.m_ItemInfo.CodeP1.Contains("#info=WSED1EDT") ? 2 : 1) : 0;
            //    this.m_ItemInfo.CodeN = this.m_Ctrl.GetStepCode_N(this.m_ItemInfo.StepNumber);
            //    this.m_ItemInfo.LangN = !this.m_ItemInfo.CodeN.Contains("#info=LDEDIT") ? (!this.m_ItemInfo.CodeN.Contains("#info=WSED1EDT") ? 2 : 1) : 0;
            //    this.m_ItemInfo.CodeNotes = this.m_Ctrl.GetStepNote(this.m_ItemInfo.StepNumber);
            //    this.m_Ctrl.LockStep(true, this.m_ItemInfo.StepNumber);
            //}
            //else if (this.m_Ctrl.IsSelTrans() > 0)
            //{
            //    this.m_ItemInfo.ItemType = 1;
            //    this.m_ItemInfo.TransNumber = this.m_Ctrl.IsSelTrans();
            //    this.m_ItemInfo.CodeCondition = this.m_Ctrl.GetTransCode(this.m_ItemInfo.TransNumber);
            //    this.m_ItemInfo.LangConition = !this.m_ItemInfo.CodeCondition.Contains("#info=LDEDIT") ? 2 : 0;
            //    this.m_ItemInfo.CodeNotes = this.m_Ctrl.GetTransNote(this.m_ItemInfo.TransNumber);
            //    this.m_Ctrl.LockTrans(true, this.m_ItemInfo.TransNumber);
            //}
            //else if (this.m_Ctrl.IsSelComment() != 0)
            //{
            //    this.m_ItemInfo.ItemType = 2;
            //    this.m_ItemInfo.CommentNumber = (int)this.m_Ctrl.IsSelComment();
            //    this.m_ItemInfo.CodeComment = this.m_Ctrl.GetCommentNote(this.m_ItemInfo.CommentNumber);
            //    this.m_Ctrl.LockComment(true, this.m_ItemInfo.CommentNumber);
            //}
        }

        //protected override void WndProc(ref Message m)
        //{
        //    IntPtr serBuffer;
        //    uint num12;
        //    uint num13;
        //    uint num14;
        //    uint num15;
        //    uint num16;
        //    uint[] numArray;
        //    uint num18;
        //    int num20;
        //    uint num22;
        //    byte num23;
        //    string str7;
        //    int num24;
        //    bool flag;
        //    uint pdwCnxStatus = 0;
        //    uint pdwAppStatus = 0;
        //    uint hType = 0;
        //    uint dwDim = 0;
        //    uint dwStringLength = 0;
        //    uint dwFlags = 0;
        //    string prefix = "";
        //    string objA = "";
        //    string str4 = "";
        //    byte blockType = 0;

        //    int msg = m.Msg;
        //    if (msg == 0x111)
        //    {
        //        string typeName;
        //        int num19;
        //        long symbolPos;
        //        Types.POINT point;
        //        string symbolName;
        //        uint wParam = (uint)((int)m.WParam);
        //        uint num9 = wParam & 0xffff;
        //        uint num10 = wParam >> 0x10;
        //        switch (num10)
        //        {
        //            case 1:
        //                goto TR_0000;
        //            case 2:
        //            case 4:
        //                goto TR_0000;
        //            case 3:
        //                goto TR_0000;

        //            case 5:
        //            case 6:
        //                num12 = 0;
        //                num13 = 0;
        //                num14 = 0;
        //                num15 = 0;
        //                num16 = 0;
        //                numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
        //                num19 = 0;
        //                num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
        //                num20 = 0;
        //                while (true)
        //                {
        //                    //flag = num20 < num18;
        //                    //if (flag)
        //                    //{
        //                    //    serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
        //                    //    str4 = Marshal.PtrToStringAnsi(serBuffer);
        //                    //    //if (str4 != this.ScreenName)
        //                    //    //{
        //                    //    //    num20++;
        //                    //    //    continue;
        //                    //    //}
        //                    //    num19 = num20;
        //                    //}
        //                    //DBSrv.str_K5DBvsel bvsel;
        //                    //bvsel.hwndParent = base.Handle;
        //                    //bvsel.hParentGroup = numArray[num19];
        //                    //typeName = this.m_Ctrl.GetTypeName();
        //                    //bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
        //                    //bvsel.hwndParent = base.Handle;
        //                    //symbolPos = this.m_Ctrl.GetSymbolPos();
        //                    //point.x = (int)(symbolPos & 0xffffL);
        //                    //point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
        //                    //bvsel.ptPos = point;
        //                    //bvsel.dwOptions = 0x4b;
        //                    //num22 = 0;
        //                    //symbolName = this.m_Ctrl.GetSymbolName();
        //                    //if ((num10 == 5) && ReferenceEquals(symbolName, null))
        //                    //{
        //                    //    symbolName = "" + ((char)this.m_Ctrl.GetFirstChar());
        //                    //}
        //                    //CommonConstants.g_SelectVar = true;
        //                    //if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
        //                    //{
        //                    //    goto TR_0000;
        //                    //}
        //                    //else
        //                    //{
        //                    //this.m_Ctrl.InsertText(0, 0, "I1:009.02");
        //                        this.m_Ctrl.InsertSymbol("I1:009.02");
        //                    //    //for (int i = 0; i < xm.LoadedProject.Tags.Count; i++)
        //                    //    //{
        //                    //    //    this.m_Ctrl.InsertSymbol(xm.LoadedProject.Tags[i].LogicalAddress.ToString());
        //                    //    //}
        //                        goto TR_0000;

        //                    //}
        //                    break;
        //                }
        //                break;

        //            case 7:
        //                this.m_Ctrl.SelectAll();
        //                this.m_Ctrl.InsertVarAfterInsertFB(true);
        //                this.m_Ctrl.SetAutoEdit(2);
        //                goto TR_0000;

        //            case 8:
        //                this.m_Active_ID = num9;
        //                goto TR_0000;

        //                //default:
        //                //    switch (wParam)
        //                //    {
        //                //        case 30:
        //                //            this.GetItemInfo();
        //                //            //this.tsAction_Click(null, null);
        //                //            break;

        //                //        case 0x1f:
        //                //            this.GetItemInfo();
        //                //            //this.tsNotes_Click(null, null);
        //                //            break;

        //                //        case 0x20:
        //                //            this.GetItemInfo();
        //                //            //this.tsP1_Click(null, null);
        //                //            break;

        //                //        case 0x21:
        //                //            this.GetItemInfo();
        //                //            //this.tsN_Click(null, null);
        //                //            break;

        //                //        case 0x22:
        //                //            this.GetItemInfo();
        //                //            //this.tsP0_Click(null, null);
        //                //            break;

        //                //        default:
        //                //            break;
        //                //    }
        //                //    goto TR_0000;
        //        }

        //    }
        //TR_0000:;
        //    base.WndProc(ref m);
        //}

        // TODO - Verify if following work 

        public event RestoreDirtyFlag _restoreDirtyFlag
        {
            add
            {
                RestoreDirtyFlag objA = this.restoreFlag;
                while (true)
                {
                    RestoreDirtyFlag comparand = objA;
                    RestoreDirtyFlag flag3 = comparand + value;
                    objA = Interlocked.CompareExchange<RestoreDirtyFlag>(ref this.restoreFlag, flag3, comparand);
                    if (ReferenceEquals(objA, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                RestoreDirtyFlag objA = this.restoreFlag;
                while (true)
                {
                    RestoreDirtyFlag comparand = objA;
                    RestoreDirtyFlag flag3 = comparand - value;
                    objA = Interlocked.CompareExchange<RestoreDirtyFlag>(ref this.restoreFlag, flag3, comparand);
                    if (ReferenceEquals(objA, comparand))
                    {
                        return;
                    }
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            IntPtr serBuffer;
            uint num12;
            uint num13;
            uint num14;
            uint num15;
            uint num16;
            uint[] numArray;
            uint num18;
            int num20;
            uint num22;
            byte num23;
            string str7;
            int num24;
            bool flag;
            uint wParam;
            uint pdwCnxStatus = 0;
            uint pdwAppStatus = 0;
            uint hType = 0;
            uint dwDim = 0;
            uint dwStringLength = 0;
            uint dwFlags = 0;
            string prefix = "";
            string objA = "";
            string str4 = "";
            byte blockType = 0;
            int msg = m.Msg;
            if (msg == 0x111)
            {
                string typeName;
                int num19;
                DBSrv.str_K5DBvsel bvsel;
                long symbolPos;
                Types.POINT point;
                string symbolName;
                wParam = (uint)((int)m.WParam);
                uint num9 = wParam & 0xffff;
                uint num10 = wParam >> 0x10;
                uint lParam = (uint)((int)m.LParam);
                if (num9 != 0x3e8)
                {
                    if ((num9 != 100) || CommonConstants.g_SelectVar)
                    {
                        byte[] buffer;
                        byte[] buffer2;
                        byte[] buffer3;
                        int num26;
                        string text;
                        if (num9 != 0x3e9)
                        {
                            if (num9 != 0x3ea)
                            {
                                if (num9 != 0x3eb)
                                {
                                    if (num9 != 0x3ec)
                                    {
                                        if (num9 != 0x3ed)
                                        {
                                            if (num9 != 0x3ee)
                                            {
                                                goto TR_0000;
                                            }
                                            else
                                            {
                                                buffer = new byte[4];
                                                buffer2 = new byte[2];
                                                buffer3 = new byte[2];
                                                text = "";
                                                wParam = num10;
                                                switch (wParam)
                                                {
                                                    case 5:
                                                    case 6:
                                                        num12 = 0;
                                                        num13 = 0;
                                                        num14 = 0;
                                                        num15 = 0;
                                                        num16 = 0;
                                                        numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                                                        num19 = 0;
                                                        num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                        num20 = 0;
                                                        while (true)
                                                        {
                                                            flag = num20 < num18;
                                                            if (flag)
                                                            {
                                                                serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                                                str4 = Marshal.PtrToStringAnsi(serBuffer);
                                                                if (str4 != this.ScreenName)
                                                                {
                                                                    num20++;
                                                                    continue;
                                                                }
                                                                num19 = num20;
                                                            }
                                                            bvsel.hwndParent = base.Handle;
                                                            bvsel.hParentGroup = numArray[num19];
                                                            typeName = this.m_Ctrl_Condition.GetTypeName();
                                                            bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                                            bvsel.hwndParent = base.Handle;
                                                            symbolPos = this.m_Ctrl_Condition.GetSymbolPos();
                                                            point.x = (int)(symbolPos & 0xffffL);
                                                            point.x += this.splitContainer1.Panel2.Left;
                                                            point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                                            bvsel.ptPos = point;
                                                            bvsel.dwOptions = 0x4b;
                                                            num22 = 0;
                                                            symbolName = this.m_Ctrl_Condition.GetSymbolName();
                                                            if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                                            {
                                                                symbolName = "" + ((char)this.m_Ctrl_Condition.GetFirstChar());
                                                            }
                                                            CommonConstants.g_SelectVar = true;
                                                            if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                                            {
                                                                goto TR_0000;
                                                            }
                                                            else
                                                            {
                                                                CommonConstants.g_SelectVar = false;
                                                                if (((this._IECBlockType == IECBlockType.ST) || ((this._IECBlockType == IECBlockType.IL) || (this._IECBlockType == IECBlockType.LD))) || (this._IECBlockType == IECBlockType.FBD))
                                                                {
                                                                    objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                                    if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                                                    {
                                                                        this.m_Ctrl_Condition.InsertSymbol(objA);
                                                                        goto TR_0000;
                                                                    }
                                                                }
                                                                if (num22 != 0)
                                                                {
                                                                    serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                                                    num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                                    num20 = 0;
                                                                }
                                                                else
                                                                {
                                                                    objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                                    this.m_Ctrl_Condition.InsertSymbol(objA);
                                                                    goto TR_0000;
                                                                }
                                                            }
                                                            break;
                                                        }
                                                        break;

                                                    case 7:
                                                        goto TR_0000;

                                                    case 8:
                                                        this.m_Active_ID = num9;
                                                        goto TR_0000;

                                                    default:
                                                        if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                                        {
                                                            text = this.m_Ctrl_Condition.GetText();
                                                            if (!ReferenceEquals(text, null))
                                                            {
                                                                this.m_Ctrl.SetTransCode(this.m_ItemInfo.TransNumber, text);
                                                                this.restoreFlag();
                                                            }
                                                        }
                                                        goto TR_0000;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            buffer = new byte[4];
                                            buffer2 = new byte[2];
                                            buffer3 = new byte[2];
                                            text = "";
                                            wParam = num10;
                                            if (wParam == 8)
                                            {
                                                this.m_Active_ID = num9;
                                            }
                                            else if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                            {
                                                text = this.m_Ctrl_Notes.GetText();
                                                if (!ReferenceEquals(text, null))
                                                {
                                                    if (this.m_ItemInfo.ItemType == 0)
                                                    {
                                                        this.m_Ctrl.SetStepNote(this.m_ItemInfo.StepNumber, text);
                                                    }
                                                    else if (this.m_ItemInfo.ItemType == 1)
                                                    {
                                                        this.m_Ctrl.SetTransNote(this.m_ItemInfo.TransNumber, text);
                                                    }
                                                    else if (this.m_ItemInfo.ItemType == 2)
                                                    {
                                                        this.m_Ctrl.SetCommentNote(this.m_ItemInfo.CommentNumber, text);
                                                    }
                                                    this.restoreFlag();
                                                }
                                            }
                                            goto TR_0000;
                                        }
                                    }
                                    else
                                    {
                                        buffer = new byte[4];
                                        buffer2 = new byte[2];
                                        buffer3 = new byte[2];
                                        num26 = 0;
                                        text = "";
                                        wParam = num10;
                                        switch (wParam)
                                        {
                                            case 5:
                                            case 6:
                                                num12 = 0;
                                                num13 = 0;
                                                num14 = 0;
                                                num15 = 0;
                                                num16 = 0;
                                                numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                                                num19 = 0;
                                                num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                num20 = 0;
                                                while (true)
                                                {
                                                    flag = num20 < num18;
                                                    if (flag)
                                                    {
                                                        serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                                        str4 = Marshal.PtrToStringAnsi(serBuffer);
                                                        if (str4 != this.ScreenName)
                                                        {
                                                            num20++;
                                                            continue;
                                                        }
                                                        num19 = num20;
                                                    }
                                                    bvsel.hwndParent = base.Handle;
                                                    bvsel.hParentGroup = numArray[num19];
                                                    typeName = this.m_Ctrl_N.GetTypeName();
                                                    bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                                    bvsel.hwndParent = base.Handle;
                                                    symbolPos = this.m_Ctrl_N.GetSymbolPos();
                                                    point.x = (int)(symbolPos & 0xffffL);
                                                    point.x += this.splitContainer1.Panel2.Left;
                                                    point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                                    bvsel.ptPos = point;
                                                    bvsel.dwOptions = 0x4b;
                                                    num22 = 0;
                                                    symbolName = this.m_Ctrl_N.GetSymbolName();
                                                    if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                                    {
                                                        symbolName = "" + ((char)this.m_Ctrl_N.GetFirstChar());
                                                    }
                                                    CommonConstants.g_SelectVar = true;
                                                    if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                                    {
                                                        goto TR_0000;
                                                    }
                                                    else
                                                    {
                                                        CommonConstants.g_SelectVar = false;
                                                        if (((this._IECBlockType == IECBlockType.ST) || ((this._IECBlockType == IECBlockType.IL) || (this._IECBlockType == IECBlockType.LD))) || (this._IECBlockType == IECBlockType.FBD))
                                                        {
                                                            objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                            if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                                            {
                                                                this.m_Ctrl_N.InsertSymbol(objA);
                                                                goto TR_0000;
                                                            }
                                                        }
                                                        if (num22 != 0)
                                                        {
                                                            serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                                            num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                            num20 = 0;
                                                        }
                                                        else
                                                        {
                                                            objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                            this.m_Ctrl_N.InsertSymbol(objA);
                                                            goto TR_0000;
                                                        }
                                                    }
                                                    break;
                                                }
                                                break;

                                            case 7:
                                                this.m_Ctrl_N.SetAutoEdit(2);
                                                goto TR_0000;

                                            case 8:
                                                this.m_Active_ID = num9;
                                                goto TR_0000;

                                            default:
                                                if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                                {
                                                    text = this.m_Ctrl_N.GetText();
                                                    if (!ReferenceEquals(text, null))
                                                    {
                                                        this.m_Ctrl.SetStepCode_N(this.m_ItemInfo.StepNumber, text);
                                                        this.restoreFlag();
                                                    }
                                                }
                                                goto TR_0000;
                                        }
                                        goto TR_010B;
                                    }
                                }
                                else
                                {
                                    buffer = new byte[4];
                                    buffer2 = new byte[2];
                                    buffer3 = new byte[2];
                                    num26 = 0;
                                    text = "";
                                    wParam = num10;
                                    switch (wParam)
                                    {
                                        case 5:
                                        case 6:
                                            num12 = 0;
                                            num13 = 0;
                                            num14 = 0;
                                            num15 = 0;
                                            num16 = 0;
                                            numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                                            num19 = 0;
                                            num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                            num20 = 0;
                                            while (true)
                                            {
                                                flag = num20 < num18;
                                                if (flag)
                                                {
                                                    serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                                    str4 = Marshal.PtrToStringAnsi(serBuffer);
                                                    if (str4 != this.ScreenName)
                                                    {
                                                        num20++;
                                                        continue;
                                                    }
                                                    num19 = num20;
                                                }
                                                bvsel.hwndParent = base.Handle;
                                                bvsel.hParentGroup = numArray[num19];
                                                typeName = this.m_Ctrl_P0.GetTypeName();
                                                bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                                bvsel.hwndParent = base.Handle;
                                                symbolPos = this.m_Ctrl_P0.GetSymbolPos();
                                                point.x = (int)(symbolPos & 0xffffL);
                                                point.x += this.splitContainer1.Panel2.Left;
                                                point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                                point.x += this.splitContainer1.Panel2.Left;
                                                bvsel.ptPos = point;
                                                bvsel.dwOptions = 0x4b;
                                                num22 = 0;
                                                symbolName = this.m_Ctrl_P0.GetSymbolName();
                                                if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                                {
                                                    symbolName = "" + ((char)this.m_Ctrl_P0.GetFirstChar());
                                                }
                                                CommonConstants.g_SelectVar = true;
                                                if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                                {
                                                    goto TR_0000;
                                                }
                                                else
                                                {
                                                    CommonConstants.g_SelectVar = false;
                                                    if (((this._IECBlockType == IECBlockType.ST) || (this._IECBlockType == IECBlockType.IL)) || (this._IECBlockType == IECBlockType.FBD))
                                                    {
                                                        objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                        if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                                        {
                                                            this.m_Ctrl_P0.InsertSymbol(objA);
                                                            goto TR_0000;
                                                        }
                                                    }
                                                    if (num22 != 0)
                                                    {
                                                        serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                                        num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                        num20 = 0;
                                                    }
                                                    else
                                                    {
                                                        objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                        this.m_Ctrl_P0.InsertSymbol(objA);
                                                        goto TR_0000;
                                                    }
                                                }
                                                break;
                                            }
                                            break;

                                        case 7:
                                            this.m_Ctrl_P0.SetAutoEdit(2);
                                            goto TR_0000;

                                        case 8:
                                            this.m_Active_ID = num9;
                                            goto TR_0000;

                                        default:
                                            if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                            {
                                                text = this.m_Ctrl_P0.GetText();
                                                if (!ReferenceEquals(text, null))
                                                {
                                                    this.m_Ctrl.SetStepCode_P0(this.m_ItemInfo.StepNumber, text);
                                                    this.restoreFlag();
                                                }
                                            }
                                            goto TR_0000;
                                    }
                                    goto TR_00DC;
                                }
                            }
                            else
                            {
                                text = "";
                                buffer = new byte[4];
                                buffer2 = new byte[2];
                                buffer3 = new byte[2];
                                num26 = 0;
                                wParam = num10;
                                switch (wParam)
                                {
                                    case 5:
                                    case 6:
                                        num12 = 0;
                                        num13 = 0;
                                        num14 = 0;
                                        num15 = 0;
                                        num16 = 0;
                                        numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                                        num19 = 0;
                                        num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                        num20 = 0;
                                        while (true)
                                        {
                                            flag = num20 < num18;
                                            if (flag)
                                            {
                                                serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                                str4 = Marshal.PtrToStringAnsi(serBuffer);
                                                if (str4 != this.ScreenName)
                                                {
                                                    num20++;
                                                    continue;
                                                }
                                                num19 = num20;
                                            }
                                            bvsel.hwndParent = base.Handle;
                                            bvsel.hParentGroup = numArray[num19];
                                            typeName = this.m_Ctrl_P1.GetTypeName();
                                            bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                            bvsel.hwndParent = base.Handle;
                                            symbolPos = this.m_Ctrl_P1.GetSymbolPos();
                                            point.x = (int)(symbolPos & 0xffffL);
                                            point.x += this.splitContainer1.Panel2.Left;
                                            point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                            bvsel.ptPos = point;
                                            bvsel.dwOptions = 0x4b;
                                            num22 = 0;
                                            symbolName = this.m_Ctrl_P1.GetSymbolName();
                                            if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                            {
                                                symbolName = "" + ((char)this.m_Ctrl_P1.GetFirstChar());
                                            }
                                            CommonConstants.g_SelectVar = true;
                                            if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                            {
                                                goto TR_0000;
                                            }
                                            else
                                            {
                                                CommonConstants.g_SelectVar = false;
                                                if ((this._IECBlockType == IECBlockType.ST) || (this._IECBlockType == IECBlockType.IL))
                                                {
                                                    objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                    if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                                    {
                                                        this.m_Ctrl_P1.InsertSymbol(objA);
                                                        goto TR_0000;
                                                    }
                                                }
                                                if (num22 != 0)
                                                {
                                                    serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                                    num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                    num20 = 0;
                                                }
                                                else
                                                {
                                                    objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                    this.m_Ctrl_P1.InsertSymbol(objA);
                                                    goto TR_0000;
                                                }
                                            }
                                            break;
                                        }
                                        break;

                                    case 7:
                                        this.m_Ctrl_P1.SetAutoEdit(2);
                                        goto TR_0000;

                                    case 8:
                                        this.m_Active_ID = num9;
                                        goto TR_0000;

                                    default:
                                        if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                        {
                                            text = this.m_Ctrl_P1.GetText();
                                            if (!ReferenceEquals(text, null))
                                            {
                                                this.m_Ctrl.SetStepCode_P1(this.m_ItemInfo.StepNumber, text);
                                                this.restoreFlag();
                                            }
                                        }
                                        goto TR_0000;
                                }
                                goto TR_00AD;
                            }
                        }
                        else
                        {
                            buffer = new byte[4];
                            buffer2 = new byte[2];
                            buffer3 = new byte[2];
                            num26 = 0;
                            wParam = num10;
                            if (wParam == 8)
                            {
                                this.m_Active_ID = num9;
                            }
                            else if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                            {
                                text = this.m_Ctrl_Action.GetText();
                                if (!ReferenceEquals(text, null))
                                {
                                    this.m_Ctrl.SetStepCode_Def(this.m_ItemInfo.StepNumber, text);
                                    this.restoreFlag();
                                }
                            }
                            goto TR_0000;
                        }
                    }
                    else
                    {
                        if (this._IECBlockType != IECBlockType.ST)
                        {
                            goto TR_0000;
                        }
                        else
                        {
                            num12 = 0;
                            num13 = 0;
                            num14 = 0;
                            num15 = 0;
                            num16 = 0;
                            num22 = 0;
                            numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                            num22 = (uint)((int)m.LParam);
                            if (num22 == 0)
                            {
                                goto TR_0000;
                            }
                            else
                            {
                                serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                num20 = 0;
                            }
                        }
                        goto TR_008A;
                    }
                }
                else
                {
                    if (this.m_Ctrl.IsModified())
                    {
                        //this.restoreFlag();
                    }
                    wParam = num10;
                    switch (wParam)
                    {
                        case 1:
                            if ((this._IECBlockType == IECBlockType.SFC) || (this._IECBlockType == IECBlockType.SFC_CHILD))
                            {
                                this.GetItemInfo();
                                if (this.m_Ctrl.IsSelStep() > 0)
                                {
                                    //this.tsAction_Click(null, null);
                                }
                                else if (this.m_Ctrl.IsSelTrans() > 0)
                                {
                                    //this.tsCondition_Click(null, null);
                                }
                                else if (this.m_Ctrl.IsSelComment() != 0)
                                {
                                    //this.tsNotes_Click(null, null);
                                }
                            }
                            goto TR_0000;

                        case 2:
                        case 4:
                            goto TR_0000;

                        case 3:
                            if ((this._IECBlockType == IECBlockType.ST) || (this._IECBlockType == IECBlockType.LD))
                            {
                                string str = "";
                                str = this.m_Ctrl.GetSelText();
                                //this.updateSTtools(str);
                            }
                            //this.screenMouseEventIECUndoRedo();
                            goto TR_0000;

                        case 5:
                        case 6:
                            num12 = 0;
                            num13 = 0;
                            num14 = 0;
                            num15 = 0;
                            num16 = 0;
                            numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                            num19 = 0;
                            num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                            num20 = 0;
                            while (true)
                            {
                                flag = num20 < num18;
                                if (flag)
                                {
                                    serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                    str4 = Marshal.PtrToStringAnsi(serBuffer);
                                    if (str4 != this.ScreenName)
                                    {
                                        num20++;
                                        continue;
                                    }
                                    num19 = num20;
                                }
                                bvsel.hwndParent = base.Handle;
                                bvsel.hParentGroup = numArray[num19];
                                typeName = this.m_Ctrl.GetTypeName();
                                bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                bvsel.hwndParent = base.Handle;
                                symbolPos = this.m_Ctrl.GetSymbolPos();
                                point.x = (int)(symbolPos & 0xffffL);
                                point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                bvsel.ptPos = point;
                                bvsel.dwOptions = 0x4b;
                                num22 = 0;
                                symbolName = this.m_Ctrl.GetSymbolName();
                                if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                {
                                    symbolName = "" + ((char)this.m_Ctrl.GetFirstChar());
                                }
                                CommonConstants.g_SelectVar = true;
                                if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                {
                                    goto TR_0000;
                                }
                                else
                                {
                                    CommonConstants.g_SelectVar = false;
                                    if (((this._IECBlockType == IECBlockType.ST) || ((this._IECBlockType == IECBlockType.IL) || (this._IECBlockType == IECBlockType.LD))) || (this._IECBlockType == IECBlockType.FBD))
                                    {
                                        objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                        //if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                        //{
                                            this.m_Ctrl.InsertSymbol(objA);
                                            goto TR_0000;
                                        //}
                                    }
                                    if (num22 != 0)
                                    {
                                        serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                        num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                        num20 = 0;
                                    }
                                    else
                                    {
                                        objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                        this.m_Ctrl.InsertSymbol(objA);
                                        goto TR_0000;
                                    }
                                }
                                break;
                            }
                            break;

                        case 7:
                            this.m_Ctrl.SetAutoEdit(2);
                            goto TR_0000;

                        case 8:
                            this.m_Active_ID = num9;
                            goto TR_0000;

                        default:
                            switch (wParam)
                            {
                                case 30:
                                    this.GetItemInfo();
                                    //this.tsAction_Click(null, null);
                                    break;

                                case 0x1f:
                                    this.GetItemInfo();
                                    //this.tsNotes_Click(null, null);
                                    break;

                                case 0x20:
                                    this.GetItemInfo();
                                    //this.tsP1_Click(null, null);
                                    break;

                                case 0x21:
                                    this.GetItemInfo();
                                    //this.tsN_Click(null, null);
                                    break;

                                case 0x22:
                                    this.GetItemInfo();
                                    //this.tsP0_Click(null, null);
                                    break;

                                default:
                                    break;
                            }
                            goto TR_0000;
                    }
                    goto TR_0057;
                }
            }
            else
            {
                if (msg == 0x402)
                {
                    uint lParam = (uint)((int)m.LParam);
                    wParam = (uint)((int)m.WParam);
                    if (wParam == 2)
                    {
                        string str = "";// Marshal.PtrToStringAnsi(MW.GetStatus(CommonConstants.g_hMWClient, CommonConstants.g_hMWProject, ref pdwCnxStatus, ref pdwAppStatus));
                        if (((str != "Unknown") && (str != "RUN")) && (str != "STOP"))
                        {
                            //this.refreshErrorListBox(str);
                        }
                        else if ((CommonConstants.g_MWStatus != str) && (((CommonConstants.g_MWStatus == "Unknown") || ((CommonConstants.g_MWStatus == "RUN") || (CommonConstants.g_MWStatus == "STOP"))) || (CommonConstants.g_MWStatus.Length == 0)))
                        {
                            CommonConstants.g_MWStatus = str;
                            //this._updateModedata();
                        }
                    }
                    else if (wParam == 5)
                    {
                        try
                        {
                            uint num30 = 0; // MW.GetObjDataLength(CommonConstants.g_hMWClient, CommonConstants.g_hMWProject, lParam);
                            flag = num30 == 0;
                            if (!flag)
                            {
                                byte[] destination = new byte[num30];
                                IntPtr pData = Marshal.AllocHGlobal(destination.Length);
                                //if (MW.GetObjData(CommonConstants.g_hMWClient, CommonConstants.g_hMWProject, lParam, pData) == 0)
                                //{
                                //    Marshal.Copy(pData, destination, 0, destination.Length);
                                //    if (destination[0] == 1)
                                //    {
                                //        if (CommonConstants.g_hobjMode != lParam)
                                //        {
                                //            if (((destination[3] == 0x52) || (destination[3] == 0x72)) || (destination[3] == 0x57))
                                //            {
                                //              //  this._updateDMdata(destination);
                                //            }
                                //        }
                                //        else if (destination[3] == 0x52)
                                //        {
                                //            string str = "";
                                //            int num31 = 0;
                                //            num31 = 0; // CommonConstants.MAKEWORD(destination[5], destination[6]);
                                //            if ((num31 == 2) || (num31 == 3))
                                //            {
                                //                str = "RUN";
                                //            }
                                //            else if (num31 == 1)
                                //            {
                                //                str = "HALT";
                                //            }
                                //            else if (num31 == 4)
                                //            {
                                //                str = "HOLD";
                                //            }
                                //            else if (num31 == 6)
                                //            {
                                //                str = "ERROR";
                                //            }
                                //            flag = str.Length <= 0;
                                //            if (!flag)
                                //            {
                                //                //this._refreshErrorListBox(str);
                                //                //this._updateModeTools(str);
                                //            }
                                //        }
                                //    }
                                //}
                                Marshal.FreeHGlobal(pData);
                            }
                        }
                        catch (Exception)
                        {
                        }
                        if (CommonConstants.g_hobjData == lParam)
                        {
                            CommonConstants.g_DM_Online = true;
                            CommonConstants.g_Status1 = false;
                        }
                        else if (CommonConstants.g_hobjData2 == lParam)
                        {
                            CommonConstants.g_Status2 = false;
                            CommonConstants.g_DM_Online = true;
                        }
                    }
                }
                goto TR_0000;
            }
            goto TR_0147;
        TR_0000:
            base.WndProc(ref m);
            return;
        TR_003D:
            num20++;
            goto TR_0057;
        TR_003E:
            this.m_Ctrl.InsertSymbol(objA);
            goto TR_003D;
        TR_0057:
            while (true)
            {
                flag = num20 < num18;
                if (!flag)
                {
                    break;
                }
                num23 = 0;
                serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
                if (serBuffer != IntPtr.Zero)
                {
                    objA = Marshal.PtrToStringAnsi(serBuffer);
                    DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
                    prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
                    str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
                    num23 = Convert.ToByte(dwStringLength);
                    //if (!CommonConstants.IsProductSupportStringDataType(CommonConstants.ProductDataInfo.iProductID) && (prefix == "STRING"))
                    //{
                    //    DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                    //    MessageBox.Show("For selected Flexipanel model STRING data type is not supported.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //}
                    //else if (CommonConstants.ProductsupportLREL(CommonConstants.ProductDataInfo.iProductID) || (prefix != "LREAL"))
                    //{
                    //    str7 = "";
                    //    if (((str4 != "(Global)") && (str4 != "(Retain)")) || !this._isPlcTagWithSameNamePresent(objA, ref str7))
                    //    {
                    //        if (str4 == "(Global)")
                    //        {
                    //            blockType = 0;
                    //        }
                    //        else if (str4 == "(Retain)")
                    //        {
                    //            blockType = 1;
                    //        }
                    //        else
                    //        {
                    //            blockType = 2;
                    //            CommonConstants.stGroupType_editPara = true;
                    //        }
                    //        if ((str4 != "(Retain)") && (str4 != "(Global)"))
                    //        {
                    //            if (prefix == "STRING")
                    //            {
                    //                num23 = 10;
                    //            }
                    //            this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
                    //            goto TR_003E;
                    //        }
                    //        else
                    //        {
                    //            if (prefix == "STRING")
                    //            {
                    //                num23 = 10;
                    //            }
                    //            num24 = 0;
                    //            num24 = this._addstTag(objA, prefix, blockType, num23, "0");
                    //            if ((str4 != "(Retain)") || (num24 != 3))
                    //            {
                    //                goto TR_003E;
                    //            }
                    //            else
                    //            {
                    //                DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                    //        MessageBox.Show("This variable name is used for PLC tag ( address: " + str7 + " ) \nHence this name can not be used.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //    }
                    //}
                    //else
                    //{
                    //    DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                    //    MessageBox.Show("For selected Flexipanel model LREAL data type is not supported.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //}
                    break;
                }
                goto TR_003D;
            }
            goto TR_0000;
        TR_0074:
            num20++;
            goto TR_008A;
        TR_0075:
            this.m_Ctrl.InsertSymbol(objA);
            goto TR_0074;
        TR_008A:
            while (true)
            {
                flag = num20 < num18;
                if (!flag)
                {
                    break;
                }
                num23 = 0;
                serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
                if (serBuffer != IntPtr.Zero)
                {
                    objA = Marshal.PtrToStringAnsi(serBuffer);
                    DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
                    prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
                    str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
                    num23 = Convert.ToByte(dwStringLength);
                    //if (!CommonConstants.IsProductSupportStringDataType(CommonConstants.ProductDataInfo.iProductID) && (prefix == "STRING"))
                    //{
                    //    DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                    //    MessageBox.Show("For selected Flexipanel model STRING data type is not supported.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //}
                    //else if (CommonConstants.ProductsupportLREL(CommonConstants.ProductDataInfo.iProductID) || (prefix != "LREAL"))
                    //{
                    //    str7 = "";
                    //    if (((str4 != "(Global)") && (str4 != "(Retain)")) || !this._isPlcTagWithSameNamePresent(objA, ref str7))
                    //    {
                    //        blockType = (str4 != "(Global)") ? ((str4 != "(Retain)") ? 2 : 1) : 0;
                    //        if ((str4 != "(Retain)") && (str4 != "(Global)"))
                    //        {
                    //            if (prefix == "STRING")
                    //            {
                    //                num23 = 10;
                    //            }
                    //            this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
                    //            goto TR_0075;
                    //        }
                    //        else
                    //        {
                    //            if (prefix == "STRING")
                    //            {
                    //                num23 = 10;
                    //            }
                    //            num24 = 0;
                    //            num24 = this._addstTag(objA, prefix, blockType, num23, "0");
                    //            if ((str4 != "(Retain)") || (num24 != 3))
                    //            {
                    //                goto TR_0075;
                    //            }
                    //            else
                    //            {
                    //                DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                    //        MessageBox.Show("This variable name is used for PLC tag ( address: " + str7 + " ) \nHence this name can not be used.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //    }
                    //}
                    //else
                    //{
                    //    DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                    //    MessageBox.Show("For selected Flexipanel model LREAL data type is not supported.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //}
                    break;
                }
                goto TR_0074;
            }
            goto TR_0000;
        TR_009D:
            num20++;
            goto TR_00AD;
        TR_009E:
            this.m_Ctrl_P1.InsertSymbol(objA);
            goto TR_009D;
        TR_00AD:
            while (true)
            {
                flag = num20 < num18;
                if (flag)
                {
                    num23 = 0;
                    serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
                    if (!(serBuffer != IntPtr.Zero))
                    {
                        goto TR_009D;
                    }
                    else
                    {
                        objA = Marshal.PtrToStringAnsi(serBuffer);
                        DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
                        prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
                        str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
                        num23 = Convert.ToByte(dwStringLength);
                        if (str4 == "(Global)")
                        {
                            blockType = 0;
                        }
                        else if (str4 == "(Retain)")
                        {
                            blockType = 1;
                        }
                        else
                        {
                            blockType = 2;
                            CommonConstants.stGroupType_editPara = true;
                        }
                        if ((str4 != "(Retain)") && (str4 != "(Global)"))
                        {
                            //this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
                            goto TR_009E;
                        }
                        else
                        {
                            num24 = 0;
                            //num24 = this._addstTag(objA, prefix, blockType, num23, "0");
                            if ((str4 != "(Retain)") || (num24 != 3))
                            {
                                goto TR_009E;
                            }
                            else
                            {
                                DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                            }
                        }
                    }
                }
                break;
            }
            goto TR_0000;
        TR_00CC:
            num20++;
            goto TR_00DC;
        TR_00CD:
            this.m_Ctrl_P0.InsertSymbol(objA);
            goto TR_00CC;
        TR_00DC:
            while (true)
            {
                flag = num20 < num18;
                if (flag)
                {
                    num23 = 0;
                    serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
                    if (!(serBuffer != IntPtr.Zero))
                    {
                        goto TR_00CC;
                    }
                    else
                    {
                        objA = Marshal.PtrToStringAnsi(serBuffer);
                        DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
                        prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
                        str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
                        num23 = Convert.ToByte(dwStringLength);
                        if (str4 == "(Global)")
                        {
                            blockType = 0;
                        }
                        else if (str4 == "(Retain)")
                        {
                            blockType = 1;
                        }
                        else
                        {
                            blockType = 2;
                            CommonConstants.stGroupType_editPara = true;
                        }
                        if ((str4 != "(Retain)") && (str4 != "(Global)"))
                        {
                            //this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
                            goto TR_00CD;
                        }
                        else
                        {
                            num24 = 0;
                            //num24 = this._addstTag(objA, prefix, blockType, num23, "0");
                            if ((str4 != "(Retain)") || (num24 != 3))
                            {
                                goto TR_00CD;
                            }
                            else
                            {
                                DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                            }
                        }
                    }
                }
                break;
            }
            goto TR_0000;
        TR_00FB:
            num20++;
            goto TR_010B;
        TR_00FC:
            this.m_Ctrl_N.InsertSymbol(objA);
            goto TR_00FB;
        TR_010B:
            while (true)
            {
                flag = num20 < num18;
                if (flag)
                {
                    num23 = 0;
                    serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
                    if (!(serBuffer != IntPtr.Zero))
                    {
                        goto TR_00FB;
                    }
                    else
                    {
                        objA = Marshal.PtrToStringAnsi(serBuffer);
                        DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
                        prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
                        str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
                        num23 = Convert.ToByte(dwStringLength);
                        if (str4 == "(Global)")
                        {
                            blockType = 0;
                        }
                        else if (str4 == "(Retain)")
                        {
                            blockType = 1;
                        }
                        else
                        {
                            blockType = 2;
                            CommonConstants.stGroupType_editPara = true;
                        }
                        if ((str4 != "(Retain)") && (str4 != "(Global)"))
                        {
                            //this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
                            goto TR_00FC;
                        }
                        else
                        {
                            num24 = 0;
                            //num24 = this._addstTag(objA, prefix, blockType, num23, "0");
                            if ((str4 != "(Retain)") || (num24 != 3))
                            {
                                goto TR_00FC;
                            }
                            else
                            {
                                DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                            }
                        }
                    }
                }
                break;
            }
            goto TR_0000;
        TR_0137:
            num20++;
            goto TR_0147;
        TR_0138:
            this.m_Ctrl_Condition.InsertSymbol(objA);
            goto TR_0137;
        TR_0147:
            while (true)
            {
                flag = num20 < num18;
                if (flag)
                {
                    num23 = 0;
                    serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
                    if (!(serBuffer != IntPtr.Zero))
                    {
                        goto TR_0137;
                    }
                    else
                    {
                        objA = Marshal.PtrToStringAnsi(serBuffer);
                        DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
                        prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
                        str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
                        num23 = Convert.ToByte(dwStringLength);
                        if (str4 == "(Global)")
                        {
                            blockType = 0;
                        }
                        else if (str4 == "(Retain)")
                        {
                            blockType = 1;
                        }
                        else
                        {
                            blockType = 2;
                            CommonConstants.stGroupType_editPara = true;
                        }
                        if ((str4 != "(Retain)") && (str4 != "(Global)"))
                        {
                            //this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
                            goto TR_0138;
                        }
                        else
                        {
                            num24 = 0;
                            //num24 = this._addstTag(objA, prefix, blockType, num23, "0");
                            if ((str4 != "(Retain)") || (num24 != 3))
                            {
                                goto TR_0138;
                            }
                            else
                            {
                                DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
                            }
                        }
                    }
                }
                break;
            }
            goto TR_0000;
        }

        private void screenMouseEventIECUndoRedo()
        {
            throw new NotImplementedException();
        }

        private void updateSTtools(string str)
        {
            throw new NotImplementedException();
        }

        /*
        protected override void WndProc(ref Message m)
        {
            IntPtr serBuffer;
            uint num12;
            uint num13;
            uint num14;
            uint num15;
            uint num16;
            uint[] numArray;
            uint num18;
            int num20;
            uint num22;
            byte num23;
            string str7;
            int num24;
            bool flag;
            //uint wParam;
            uint pdwCnxStatus = 0;
            uint pdwAppStatus = 0;
            uint hType = 0;
            uint dwDim = 0;
            uint dwStringLength = 0;
            uint dwFlags = 0;
            string prefix = "";
            string objA = "";
            string str4 = "";
            byte blockType = 0;
            int msg = m.Msg;

            if (msg == 0x111)
            {
                string typeName;
                int num19;
                DBSrv.str_K5DBvsel bvsel;
                long symbolPos;
                Types.POINT point;
                string symbolName;
                uint wParam = (uint)((int)m.WParam);
                uint num9 = wParam & 0xffff;
                uint num10 = wParam >> 0x10;
                uint lParam = (uint)((int)m.LParam);
                System.Diagnostics.Trace.WriteLine(m.Msg.ToString("X")
                    + "," + wParam.ToString("X")
                    + "," + lParam.ToString("X")
                    + "," + num9.ToString("X")
                    + "," + num10.ToString("X"));

                if (num9 != 0x3e8)
                {
                    if ((num9 != 100) || CommonConstants.g_SelectVar)
                    {
                        byte[] buffer;
                        byte[] buffer2;
                        byte[] buffer3;
                        int num26;
                        string text;
                        if (num9 != 0x3e9)
                        {
                            if (num9 != 0x3ea)
                            {
                                if (num9 != 0x3eb)
                                {
                                    if (num9 != 0x3ec)
                                    {
                                        if (num9 != 0x3ed)
                                        {
                                            if (num9 != 0x3ee)
                                            {
                                                goto TR_0000;
                                            }
                                            else
                                            {
                                                buffer = new byte[4];
                                                buffer2 = new byte[2];
                                                buffer3 = new byte[2];
                                                text = "";
                                                wParam = num10;
                                                switch (wParam)
                                                {
                                                    case 5:
                                                    case 6:
                                                        num12 = 0;
                                                        num13 = 0;
                                                        num14 = 0;
                                                        num15 = 0;
                                                        num16 = 0;
                                                        numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                                                        num19 = 0;
                                                        num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                        num20 = 0;
                                                        while (true)
                                                        {
                                                            flag = num20 < num18;
                                                            if (flag)
                                                            {
                                                                serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                                                str4 = Marshal.PtrToStringAnsi(serBuffer);
                                                                if (str4 != this.ScreenName)
                                                                {
                                                                    num20++;
                                                                    continue;
                                                                }
                                                                num19 = num20;
                                                            }
                                                            bvsel.hwndParent = base.Handle;
                                                            bvsel.hParentGroup = numArray[num19];
                                                            typeName = this.m_Ctrl_Condition.GetTypeName();
                                                            bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                                            bvsel.hwndParent = base.Handle;
                                                            symbolPos = this.m_Ctrl_Condition.GetSymbolPos();
                                                            point.x = (int)(symbolPos & 0xffffL);
                                                            point.x += this.splitContainer1.Panel2.Left;
                                                            point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                                            bvsel.ptPos = point;
                                                            bvsel.dwOptions = 0x4b;
                                                            num22 = 0;
                                                            symbolName = this.m_Ctrl_Condition.GetSymbolName();
                                                            if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                                            {
                                                                symbolName = "" + ((char)this.m_Ctrl_Condition.GetFirstChar());
                                                            }
                                                            CommonConstants.g_SelectVar = true;
                                                            if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                                            {
                                                                goto TR_0000;
                                                            }
                                                            else
                                                            {
                                                                CommonConstants.g_SelectVar = false;
                                                                if (((this._IECBlockType == IECBlockType.ST) || ((this._IECBlockType == IECBlockType.IL) || (this._IECBlockType == IECBlockType.LD))) || (this._IECBlockType == IECBlockType.FBD))
                                                                {
                                                                    objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                                    if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                                                    {
                                                                        this.m_Ctrl_Condition.InsertSymbol(objA);
                                                                        goto TR_0000;
                                                                    }
                                                                }
                                                                if (num22 != 0)
                                                                {
                                                                    serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                                                    num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                                    num20 = 0;
                                                                }
                                                                else
                                                                {
                                                                    objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                                    this.m_Ctrl_Condition.InsertSymbol(objA);
                                                                    goto TR_0000;
                                                                }
                                                            }
                                                            break;
                                                        }
                                                        break;

                                                    case 7:
                                                        goto TR_0000;

                                                    case 8:
                                                        this.m_Active_ID = num9;
                                                        goto TR_0000;

                                                    default:
                                                        if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                                        {
                                                            text = this.m_Ctrl_Condition.GetText();
                                                            if (!ReferenceEquals(text, null))
                                                            {
                                                                this.m_Ctrl.SetTransCode(this.m_ItemInfo.TransNumber, text);
                                                                this.restoreFlag();
                                                            }
                                                        }
                                                        goto TR_0000;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            buffer = new byte[4];
                                            buffer2 = new byte[2];
                                            buffer3 = new byte[2];
                                            text = "";
                                            wParam = num10;
                                            if (wParam == 8)
                                            {
                                                this.m_Active_ID = num9;
                                            }
                                            else if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                            {
                                                text = this.m_Ctrl_Notes.GetText();
                                                if (!ReferenceEquals(text, null))
                                                {
                                                    if (this.m_ItemInfo.ItemType == 0)
                                                    {
                                                        this.m_Ctrl.SetStepNote(this.m_ItemInfo.StepNumber, text);
                                                    }
                                                    else if (this.m_ItemInfo.ItemType == 1)
                                                    {
                                                        this.m_Ctrl.SetTransNote(this.m_ItemInfo.TransNumber, text);
                                                    }
                                                    else if (this.m_ItemInfo.ItemType == 2)
                                                    {
                                                        this.m_Ctrl.SetCommentNote(this.m_ItemInfo.CommentNumber, text);
                                                    }
                                                    this.restoreFlag();
                                                }
                                            }
                                            goto TR_0000;
                                        }
                                    }
                                    else
                                    {
                                        buffer = new byte[4];
                                        buffer2 = new byte[2];
                                        buffer3 = new byte[2];
                                        num26 = 0;
                                        text = "";
                                        wParam = num10;
                                        switch (wParam)
                                        {
                                            case 5:
                                            case 6:
                                                num12 = 0;
                                                num13 = 0;
                                                num14 = 0;
                                                num15 = 0;
                                                num16 = 0;
                                                numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                                                num19 = 0;
                                                num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                num20 = 0;
                                                while (true)
                                                {
                                                    flag = num20 < num18;
                                                    if (flag)
                                                    {
                                                        serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                                        str4 = Marshal.PtrToStringAnsi(serBuffer);
                                                        if (str4 != this.ScreenName)
                                                        {
                                                            num20++;
                                                            continue;
                                                        }
                                                        num19 = num20;
                                                    }
                                                    bvsel.hwndParent = base.Handle;
                                                    bvsel.hParentGroup = numArray[num19];
                                                    typeName = this.m_Ctrl_N.GetTypeName();
                                                    bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                                    bvsel.hwndParent = base.Handle;
                                                    symbolPos = this.m_Ctrl_N.GetSymbolPos();
                                                    point.x = (int)(symbolPos & 0xffffL);
                                                    point.x += this.splitContainer1.Panel2.Left;
                                                    point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                                    bvsel.ptPos = point;
                                                    bvsel.dwOptions = 0x4b;
                                                    num22 = 0;
                                                    symbolName = this.m_Ctrl_N.GetSymbolName();
                                                    if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                                    {
                                                        symbolName = "" + ((char)this.m_Ctrl_N.GetFirstChar());
                                                    }
                                                    CommonConstants.g_SelectVar = true;
                                                    if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                                    {
                                                        goto TR_0000;
                                                    }
                                                    else
                                                    {
                                                        CommonConstants.g_SelectVar = false;
                                                        if (((this._IECBlockType == IECBlockType.ST) || ((this._IECBlockType == IECBlockType.IL) || (this._IECBlockType == IECBlockType.LD))) || (this._IECBlockType == IECBlockType.FBD))
                                                        {
                                                            objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                            if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                                            {
                                                                this.m_Ctrl_N.InsertSymbol(objA);
                                                                goto TR_0000;
                                                            }
                                                        }
                                                        if (num22 != 0)
                                                        {
                                                            serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                                            num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                            num20 = 0;
                                                        }
                                                        else
                                                        {
                                                            objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                            this.m_Ctrl_N.InsertSymbol(objA);
                                                            goto TR_0000;
                                                        }
                                                    }
                                                    break;
                                                }
                                                break;

                                            case 7:
                                                //this.m_Ctrl.SelectAll();
                                                this.m_Ctrl.InsertVarAfterInsertFB(true);
                                                this.m_Ctrl.SetAutoEdit(2);
                                                goto TR_0000;

                                            case 8:
                                                this.m_Active_ID = num9;
                                                goto TR_0000;

                                            default:
                                                if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                                {
                                                    text = this.m_Ctrl_N.GetText();
                                                    if (!ReferenceEquals(text, null))
                                                    {
                                                        this.m_Ctrl.SetStepCode_N(this.m_ItemInfo.StepNumber, text);
                                                        this.restoreFlag();
                                                    }
                                                }
                                                goto TR_0000;
                                        }
                                        goto TR_010B;
                                    }
                                }
                                else
                                {
                                    buffer = new byte[4];
                                    buffer2 = new byte[2];
                                    buffer3 = new byte[2];
                                    num26 = 0;
                                    text = "";
                                    wParam = num10;
                                    switch (wParam)
                                    {
                                        case 5:
                                        case 6:
                                            num12 = 0;
                                            num13 = 0;
                                            num14 = 0;
                                            num15 = 0;
                                            num16 = 0;
                                            numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                                            num19 = 0;
                                            num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                            num20 = 0;
                                            while (true)
                                            {
                                                flag = num20 < num18;
                                                if (flag)
                                                {
                                                    serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                                    str4 = Marshal.PtrToStringAnsi(serBuffer);
                                                    if (str4 != this.ScreenName)
                                                    {
                                                        num20++;
                                                        continue;
                                                    }
                                                    num19 = num20;
                                                }
                                                bvsel.hwndParent = base.Handle;
                                                bvsel.hParentGroup = numArray[num19];
                                                typeName = this.m_Ctrl_P0.GetTypeName();
                                                bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                                bvsel.hwndParent = base.Handle;
                                                symbolPos = this.m_Ctrl_P0.GetSymbolPos();
                                                point.x = (int)(symbolPos & 0xffffL);
                                                point.x += this.splitContainer1.Panel2.Left;
                                                point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                                point.x += this.splitContainer1.Panel2.Left;
                                                bvsel.ptPos = point;
                                                bvsel.dwOptions = 0x4b;
                                                num22 = 0;
                                                symbolName = this.m_Ctrl_P0.GetSymbolName();
                                                if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                                {
                                                    symbolName = "" + ((char)this.m_Ctrl_P0.GetFirstChar());
                                                }
                                                CommonConstants.g_SelectVar = true;
                                                if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                                {
                                                    goto TR_0000;
                                                }
                                                else
                                                {
                                                    CommonConstants.g_SelectVar = false;
                                                    if (((this._IECBlockType == IECBlockType.ST) || (this._IECBlockType == IECBlockType.IL)) || (this._IECBlockType == IECBlockType.FBD))
                                                    {
                                                        objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                        if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                                        {
                                                            this.m_Ctrl_P0.InsertSymbol(objA);
                                                            goto TR_0000;
                                                        }
                                                    }
                                                    if (num22 != 0)
                                                    {
                                                        serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                                        num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                        num20 = 0;
                                                    }
                                                    else
                                                    {
                                                        objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                        this.m_Ctrl_P0.InsertSymbol(objA);
                                                        goto TR_0000;
                                                    }
                                                }
                                                break;
                                            }
                                            break;

                                        case 7:
                                            this.m_Ctrl_P0.SetAutoEdit(2);
                                            goto TR_0000;

                                        case 8:
                                            this.m_Active_ID = num9;
                                            goto TR_0000;

                                        default:
                                            if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                            {
                                                text = this.m_Ctrl_P0.GetText();
                                                if (!ReferenceEquals(text, null))
                                                {
                                                    this.m_Ctrl.SetStepCode_P0(this.m_ItemInfo.StepNumber, text);
                                                    this.restoreFlag();
                                                }
                                            }
                                            goto TR_0000;
                                    }
                                    goto TR_00DC;
                                }
                            }
                            else
                            {
                                text = "";
                                buffer = new byte[4];
                                buffer2 = new byte[2];
                                buffer3 = new byte[2];
                                num26 = 0;
                                wParam = num10;
                                switch (wParam)
                                {
                                    case 5:
                                    case 6:
                                        num12 = 0;
                                        num13 = 0;
                                        num14 = 0;
                                        num15 = 0;
                                        num16 = 0;
                                        numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                                        num19 = 0;
                                        num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                        num20 = 0;
                                        while (true)
                                        {
                                            flag = num20 < num18;
                                            if (flag)
                                            {
                                                serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                                str4 = Marshal.PtrToStringAnsi(serBuffer);
                                                if (str4 != this.ScreenName)
                                                {
                                                    num20++;
                                                    continue;
                                                }
                                                num19 = num20;
                                            }
                                            bvsel.hwndParent = base.Handle;
                                            bvsel.hParentGroup = numArray[num19];
                                            typeName = this.m_Ctrl_P1.GetTypeName();
                                            bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                            bvsel.hwndParent = base.Handle;
                                            symbolPos = this.m_Ctrl_P1.GetSymbolPos();
                                            point.x = (int)(symbolPos & 0xffffL);
                                            point.x += this.splitContainer1.Panel2.Left;
                                            point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                            bvsel.ptPos = point;
                                            bvsel.dwOptions = 0x4b;
                                            num22 = 0;
                                            symbolName = this.m_Ctrl_P1.GetSymbolName();
                                            if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                            {
                                                symbolName = "" + ((char)this.m_Ctrl_P1.GetFirstChar());
                                            }
                                            CommonConstants.g_SelectVar = true;
                                            if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                            {
                                                goto TR_0000;
                                            }
                                            else
                                            {
                                                CommonConstants.g_SelectVar = false;
                                                if ((this._IECBlockType == IECBlockType.ST) || (this._IECBlockType == IECBlockType.IL))
                                                {
                                                    objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                    if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                                    {
                                                        this.m_Ctrl_P1.InsertSymbol(objA);
                                                        goto TR_0000;
                                                    }
                                                }
                                                if (num22 != 0)
                                                {
                                                    serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                                    num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                                    num20 = 0;
                                                }
                                                else
                                                {
                                                    objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                                    this.m_Ctrl_P1.InsertSymbol(objA);
                                                    goto TR_0000;
                                                }
                                            }
                                            break;
                                        }
                                        break;

                                    case 7:
                                        this.m_Ctrl_P1.SetAutoEdit(2);
                                        goto TR_0000;

                                    case 8:
                                        this.m_Active_ID = num9;
                                        goto TR_0000;

                                    default:
                                        if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                                        {
                                            text = this.m_Ctrl_P1.GetText();
                                            if (!ReferenceEquals(text, null))
                                            {
                                                this.m_Ctrl.SetStepCode_P1(this.m_ItemInfo.StepNumber, text);
                                                this.restoreFlag();
                                            }
                                        }
                                        goto TR_0000;
                                }
                                goto TR_00AD;
                            }
                        }
                        else
                        {
                            buffer = new byte[4];
                            buffer2 = new byte[2];
                            buffer3 = new byte[2];
                            num26 = 0;
                            wParam = num10;
                            if (wParam == 8)
                            {
                                this.m_Active_ID = num9;
                            }
                            else if ((wParam == 20) && (!CommonConstants.g_IEC_OnLine && !CommonConstants.g_IEC_Simulation))
                            {
                                text = this.m_Ctrl_Action.GetText();
                                if (!ReferenceEquals(text, null))
                                {
                                    this.m_Ctrl.SetStepCode_Def(this.m_ItemInfo.StepNumber, text);
                                    this.restoreFlag();
                                }
                            }
                            goto TR_0000;
                        }
                    }
                    else
                    {
                        if (this._IECBlockType != IECBlockType.ST)
                        {
                            goto TR_0000;
                        }
                        else
                        {
                            num12 = 0;
                            num13 = 0;
                            num14 = 0;
                            num15 = 0;
                            num16 = 0;
                            num22 = 0;
                            numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                            num22 = (uint)((int)m.LParam);
                            if (num22 == 0)
                            {
                                goto TR_0000;
                            }
                            else
                            {
                                serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                num20 = 0;
                            }
                        }
                        goto TR_008A;
                    }
                }
                else
                {
                    if (this.m_Ctrl.IsModified())
                    {
                        //this.restoreFlag();
                    }
                    wParam = num10;
                    switch (wParam)
                    {
                        case 1:
                            //if ((this._IECBlockType == IECBlockType.SFC) || (this._IECBlockType == IECBlockType.SFC_CHILD))
                            //{
                            //    this.GetItemInfo();
                            //    if (this.m_Ctrl.IsSelStep() > 0)
                            //    {
                            //        this.tsAction_Click(null, null);
                            //    }
                            //    else if (this.m_Ctrl.IsSelTrans() > 0)
                            //    {
                            //        this.tsCondition_Click(null, null);
                            //    }
                            //    else if (this.m_Ctrl.IsSelComment() != 0)
                            //    {
                            //        this.tsNotes_Click(null, null);
                            //    }
                            //}
                            goto TR_0000;

                        case 2:
                        case 4:
                            goto TR_0000;

                        case 3:
                            //if ((this._IECBlockType == IECBlockType.ST) || (this._IECBlockType == IECBlockType.IL))
                            //{
                            //    string str = "";
                            //    str = this.m_Ctrl.GetSelText();
                            //    this._updateSTtools(str);
                            //}
                            // TODO
                            //this._screenMouseEventIECUndoRedo();
                            goto TR_0000;

                        case 5:
                        case 6:
                            num12 = 0;
                            num13 = 0;
                            num14 = 0;
                            num15 = 0;
                            num16 = 0;
                            numArray = new uint[DBSrv.GetNbGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject)];
                            num19 = 0;
                            num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                            num20 = 0;
                            while (true)
                            {
                                flag = num20 < num18;
                                if (flag)
                                {
                                    serBuffer = DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16);
                                    str4 = Marshal.PtrToStringAnsi(serBuffer);
                                    if (str4 != this.ScreenName)
                                    {
                                        num20++;
                                        continue;
                                    }
                                    num19 = num20;
                                }
                                bvsel.hwndParent = base.Handle;
                                bvsel.hParentGroup = numArray[num19];
                                typeName = this.m_Ctrl.GetTypeName();
                                bvsel.hPrefType = DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, typeName);
                                bvsel.hwndParent = base.Handle;
                                symbolPos = this.m_Ctrl.GetSymbolPos();
                                point.x = (int)(symbolPos & 0xffffL);
                                point.y = ((int)(symbolPos >> 0x10)) & 0xffff;
                                bvsel.ptPos = point;
                                bvsel.dwOptions = 0x4b;
                                num22 = 0;
                                symbolName = this.m_Ctrl.GetSymbolName();
                                if ((num10 == 5) && ReferenceEquals(symbolName, null))
                                {
                                    symbolName = "" + ((char)this.m_Ctrl.GetFirstChar());
                                }
                                CommonConstants.g_SelectVar = true;
                                if (!DBSrv.SelectVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, symbolName, ref bvsel, ref num22))
                                {
                                    goto TR_0000;
                                }
                                else
                                {
                                    CommonConstants.g_SelectVar = false;
                                    if (((this._IECBlockType == IECBlockType.ST) || ((this._IECBlockType == IECBlockType.IL) || (this._IECBlockType == IECBlockType.LD))) || (this._IECBlockType == IECBlockType.FBD))
                                    {
                                        objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                        //if (!ReferenceEquals(objA, null) && ((objA.Contains(".") || objA.Contains("[")) || objA.Contains("]")))
                                        //{
                                        this.m_Ctrl.InsertSymbol(objA);
                                        goto TR_0000;
                                        //}
                                    }
                                    if (num22 != 0)
                                    {
                                        serBuffer = DBSrv.GetSerBuffer(CommonConstants.g_hDBClient);
                                        num18 = DBSrv.GetGroups(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray);
                                        num20 = 0;
                                    }
                                    else
                                    {
                                        objA = Marshal.PtrToStringAnsi(DBSrv.GetSerBuffer(CommonConstants.g_hDBClient));
                                        this.m_Ctrl.InsertSymbol(objA);
                                        goto TR_0000;
                                    }
                                }
                                break;
                            }
                            break;

                        case 7:
                            //this.m_Ctrl.InsertVarAfterInsertFB(true);
                            //this.m_Ctrl.SetEnable(true);
                            //this.m_Ctrl.SetDisplay(2);
                            //DBReg.GetVersion();
                            ///uint nbBlock = DBReg.GetNbBlock(4095);
                            //DBReg. 
                            //uint[] numArray1 = new uint[nbBlock];
                            //DBReg.GetBlocks(4095, numArray1); 
                            //for (int i = 0; (ulong)i < (ulong)nbBlock; i++)
                            //{
                            //    uint num = numArray1[i];
                            //    string stringAnsi = Marshal.PtrToStringAnsi(DBReg.GetBlockLibName(num));
                            //}
                            this.m_Ctrl.SetAutoEdit(2);
                            goto TR_0000;

                        case 8:
                            this.m_Active_ID = num9;
                            goto TR_0000;

                        default:
                            switch (wParam)
                            {
                                case 30:
                                    this.GetItemInfo();
                                    //this.tsAction_Click(null, null);
                                    break;

                                case 0x1f:
                                    this.GetItemInfo();
                                    //this.tsNotes_Click(null, null);
                                    break;

                                case 0x20:
                                    this.GetItemInfo();
                                    //this.tsP1_Click(null, null);
                                    break;

                                case 0x21:
                                    this.GetItemInfo();
                                    //this.tsN_Click(null, null);
                                    break;

                                case 0x22:
                                    this.GetItemInfo();
                                    //this.tsP0_Click(null, null);
                                    break;

                                default:
                                    break;
                            }
                            goto TR_0000;
                    }
                    goto TR_0057;
                }
            }
            else
            {
                uint lParam = (uint)((int)m.LParam);
                uint wParam = (uint)((int)m.WParam);

                System.Diagnostics.Trace.WriteLine(m.Msg.ToString("X")
                    + "," + wParam.ToString("X")
                    + "," + lParam.ToString("X")
                    + ","
                    + ",");

                if (msg == 0x402)
                {
                    //    uint lParam = (uint)((int)m.LParam);
                    //    wParam = (uint)((int)m.WParam);
                    //    if (wParam == 2)
                    //    {
                    //        string str = Marshal.PtrToStringAnsi(MW.GetStatus(CommonConstants.g_hMWClient, CommonConstants.g_hMWProject, ref pdwCnxStatus, ref pdwAppStatus));
                    //        if (((str != "Unknown") && (str != "RUN")) && (str != "STOP"))
                    //        {
                    //            this._refreshErrorListBox(str);
                    //        }
                    //        else if ((CommonConstants.g_MWStatus != str) && (((CommonConstants.g_MWStatus == "Unknown") || ((CommonConstants.g_MWStatus == "RUN") || (CommonConstants.g_MWStatus == "STOP"))) || (CommonConstants.g_MWStatus.Length == 0)))
                    //        {
                    //            CommonConstants.g_MWStatus = str;
                    //            this._updateModedata();
                    //        }
                    //    }
                    //    else if (wParam == 5)
                    //    {
                    //        try
                    //        {
                    //            uint num30 = MW.GetObjDataLength(CommonConstants.g_hMWClient, CommonConstants.g_hMWProject, lParam);
                    //            flag = num30 == 0;
                    //            if (!flag)
                    //            {
                    //                byte[] destination = new byte[num30];
                    //                IntPtr pData = Marshal.AllocHGlobal(destination.Length);
                    //                if (MW.GetObjData(CommonConstants.g_hMWClient, CommonConstants.g_hMWProject, lParam, pData) == 0)
                    //                {
                    //                    Marshal.Copy(pData, destination, 0, destination.Length);
                    //                    if (destination[0] == 1)
                    //                    {
                    //                        if (CommonConstants.g_hobjMode != lParam)
                    //                        {
                    //                            if (((destination[3] == 0x52) || (destination[3] == 0x72)) || (destination[3] == 0x57))
                    //                            {
                    //                                this._updateDMdata(destination);
                    //                            }
                    //                        }
                    //                        else if (destination[3] == 0x52)
                    //                        {
                    //                            string str = "";
                    //                            int num31 = 0;
                    //                            num31 = CommonConstants.MAKEWORD(destination[5], destination[6]);
                    //                            if ((num31 == 2) || (num31 == 3))
                    //                            {
                    //                                str = "RUN";
                    //                            }
                    //                            else if (num31 == 1)
                    //                            {
                    //                                str = "HALT";
                    //                            }
                    //                            else if (num31 == 4)
                    //                            {
                    //                                str = "HOLD";
                    //                            }
                    //                            else if (num31 == 6)
                    //                            {
                    //                                str = "ERROR";
                    //                            }
                    //                            flag = str.Length <= 0;
                    //                            if (!flag)
                    //                            {
                    //                                this._refreshErrorListBox(str);
                    //                                this._updateModeTools(str);
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //                Marshal.FreeHGlobal(pData);
                    //            }
                    //        }
                    //        catch (Exception)
                    //        {
                    //        }
                    //        if (CommonConstants.g_hobjData == lParam)
                    //        {
                    //            CommonConstants.g_DM_Online = true;
                    //            CommonConstants.g_Status1 = false;
                    //        }
                    //        else if (CommonConstants.g_hobjData2 == lParam)
                    //        {
                    //            CommonConstants.g_Status2 = false;
                    //            CommonConstants.g_DM_Online = true;
                    //        }
                    //    }
                }
                goto TR_0000;
            }
            goto TR_0147;
        TR_0000:
            base.WndProc(ref m);
            return;
        TR_003D:
            num20++;
            goto TR_0057;
        TR_003E:
            this.m_Ctrl.InsertSymbol(objA);
            goto TR_003D;
        TR_0057:
            //while (true)
            //{
            //    flag = num20 < num18;
            //    if (!flag)
            //    {
            //        break;
            //    }
            //    num23 = 0;
            //    serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
            //    if (serBuffer != IntPtr.Zero)
            //    {
            //        objA = Marshal.PtrToStringAnsi(serBuffer);
            //        DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
            //        prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
            //        str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
            //        num23 = Convert.ToByte(dwStringLength);
            //        if (!CommonConstants.IsProductSupportStringDataType(CommonConstants.ProductDataInfo.iProductID) && (prefix == "STRING"))
            //        {
            //            DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //            MessageBox.Show("For selected Flexipanel model STRING data type is not supported.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        }
            //        else if (CommonConstants.ProductsupportLREL(CommonConstants.ProductDataInfo.iProductID) || (prefix != "LREAL"))
            //        {
            //            str7 = "";
            //            if (((str4 != "(Global)") && (str4 != "(Retain)")) || !this._isPlcTagWithSameNamePresent(objA, ref str7))
            //            {
            //                if (str4 == "(Global)")
            //                {
            //                    blockType = 0;
            //                }
            //                else if (str4 == "(Retain)")
            //                {
            //                    blockType = 1;
            //                }
            //                else
            //                {
            //                    blockType = 2;
            //                    CommonConstants.stGroupType_editPara = true;
            //                }
            //                if ((str4 != "(Retain)") && (str4 != "(Global)"))
            //                {
            //                    if (prefix == "STRING")
            //                    {
            //                        num23 = 10;
            //                    }
            //                    this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
            //                    goto TR_003E;
            //                }
            //                else
            //                {
            //                    if (prefix == "STRING")
            //                    {
            //                        num23 = 10;
            //                    }
            //                    num24 = 0;
            //                    num24 = this._addstTag(objA, prefix, blockType, num23, "0");
            //                    if ((str4 != "(Retain)") || (num24 != 3))
            //                    {
            //                        goto TR_003E;
            //                    }
            //                    else
            //                    {
            //                        DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //                MessageBox.Show("This variable name is used for PLC tag ( address: " + str7 + " ) \nHence this name can not be used.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //            }
            //        }
            //        else
            //        {
            //            DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //            MessageBox.Show("For selected Flexipanel model LREAL data type is not supported.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        }
            //        break;
            //    }
            //    goto TR_003D;
            //}
            goto TR_0000;
        TR_0074:
            num20++;
            goto TR_008A;
        TR_0075:
            this.m_Ctrl.InsertSymbol(objA);
            goto TR_0074;
        TR_008A:
            //while (true)
            //{
            //    flag = num20 < num18;
            //    if (!flag)
            //    {
            //        break;
            //    }
            //    num23 = 0;
            //    serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
            //    if (serBuffer != IntPtr.Zero)
            //    {
            //        objA = Marshal.PtrToStringAnsi(serBuffer);
            //        DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
            //        prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
            //        str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
            //        num23 = Convert.ToByte(dwStringLength);
            //        if (!CommonConstants.IsProductSupportStringDataType(CommonConstants.ProductDataInfo.iProductID) && (prefix == "STRING"))
            //        {
            //            DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //            MessageBox.Show("For selected Flexipanel model STRING data type is not supported.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        }
            //        else if (CommonConstants.ProductsupportLREL(CommonConstants.ProductDataInfo.iProductID) || (prefix != "LREAL"))
            //        {
            //            str7 = "";
            //            if (((str4 != "(Global)") && (str4 != "(Retain)")) || !this._isPlcTagWithSameNamePresent(objA, ref str7))
            //            {
            //                blockType = (str4 != "(Global)") ? ((str4 != "(Retain)") ? 2 : 1) : 0;
            //                if ((str4 != "(Retain)") && (str4 != "(Global)"))
            //                {
            //                    if (prefix == "STRING")
            //                    {
            //                        num23 = 10;
            //                    }
            //                    this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
            //                    goto TR_0075;
            //                }
            //                else
            //                {
            //                    if (prefix == "STRING")
            //                    {
            //                        num23 = 10;
            //                    }
            //                    num24 = 0;
            //                    num24 = this._addstTag(objA, prefix, blockType, num23, "0");
            //                    if ((str4 != "(Retain)") || (num24 != 3))
            //                    {
            //                        goto TR_0075;
            //                    }
            //                    else
            //                    {
            //                        DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //                MessageBox.Show("This variable name is used for PLC tag ( address: " + str7 + " ) \nHence this name can not be used.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //            }
            //        }
            //        else
            //        {
            //            DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //            MessageBox.Show("For selected Flexipanel model LREAL data type is not supported.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        }
            //        break;
            //    }
            //    goto TR_0074;
            //}
            goto TR_0000;
        TR_009D:
            num20++;
            goto TR_00AD;
        TR_009E:
            this.m_Ctrl.InsertSymbol(objA);
            goto TR_009D;
        TR_00AD:
            //while (true)
            //{
            //    flag = num20 < num18;
            //    if (flag)
            //    {
            //        num23 = 0;
            //        serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
            //        if (!(serBuffer != IntPtr.Zero))
            //        {
            //            goto TR_009D;
            //        }
            //        else
            //        {
            //            objA = Marshal.PtrToStringAnsi(serBuffer);
            //            DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
            //            prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
            //            str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
            //            num23 = Convert.ToByte(dwStringLength);
            //            if (str4 == "(Global)")
            //            {
            //                blockType = 0;
            //            }
            //            else if (str4 == "(Retain)")
            //            {
            //                blockType = 1;
            //            }
            //            else
            //            {
            //                blockType = 2;
            //                CommonConstants.stGroupType_editPara = true;
            //            }
            //            if ((str4 != "(Retain)") && (str4 != "(Global)"))
            //            {
            //                this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
            //                goto TR_009E;
            //            }
            //            else
            //            {
            //                num24 = 0;
            //                num24 = this._addstTag(objA, prefix, blockType, num23, "0");
            //                if ((str4 != "(Retain)") || (num24 != 3))
            //                {
            //                    goto TR_009E;
            //                }
            //                else
            //                {
            //                    DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //                }
            //            }
            //        }
            //    }
            //    break;
            //}
            goto TR_0000;
        TR_00CC:
            num20++;
            goto TR_00DC;
        TR_00CD:
            this.m_Ctrl.InsertSymbol(objA);
            goto TR_00CC;
        TR_00DC:
            //while (true)
            //{
            //    flag = num20 < num18;
            //    if (flag)
            //    {
            //        num23 = 0;
            //        serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
            //        if (!(serBuffer != IntPtr.Zero))
            //        {
            //            goto TR_00CC;
            //        }
            //        else
            //        {
            //            objA = Marshal.PtrToStringAnsi(serBuffer);
            //            DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
            //            prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
            //            str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
            //            num23 = Convert.ToByte(dwStringLength);
            //            if (str4 == "(Global)")
            //            {
            //                blockType = 0;
            //            }
            //            else if (str4 == "(Retain)")
            //            {
            //                blockType = 1;
            //            }
            //            else
            //            {
            //                blockType = 2;
            //                CommonConstants.stGroupType_editPara = true;
            //            }
            //            if ((str4 != "(Retain)") && (str4 != "(Global)"))
            //            {
            //                this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
            //                goto TR_00CD;
            //            }
            //            else
            //            {
            //                num24 = 0;
            //                num24 = this._addstTag(objA, prefix, blockType, num23, "0");
            //                if ((str4 != "(Retain)") || (num24 != 3))
            //                {
            //                    goto TR_00CD;
            //                }
            //                else
            //                {
            //                    DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //                }
            //            }
            //        }
            //    }
            //    break;
            //}
            goto TR_0000;
        TR_00FB:
            num20++;
            goto TR_010B;
        TR_00FC:
            this.m_Ctrl.InsertSymbol(objA);
            goto TR_00FB;
        TR_010B:
            //while (true)
            //{
            //    flag = num20 < num18;
            //    if (flag)
            //    {
            //        num23 = 0;
            //        serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
            //        if (!(serBuffer != IntPtr.Zero))
            //        {
            //            goto TR_00FB;
            //        }
            //        else
            //        {
            //            objA = Marshal.PtrToStringAnsi(serBuffer);
            //            DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
            //            prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
            //            str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
            //            num23 = Convert.ToByte(dwStringLength);
            //            if (str4 == "(Global)")
            //            {
            //                blockType = 0;
            //            }
            //            else if (str4 == "(Retain)")
            //            {
            //                blockType = 1;
            //            }
            //            else
            //            {
            //                blockType = 2;
            //                CommonConstants.stGroupType_editPara = true;
            //            }
            //            if ((str4 != "(Retain)") && (str4 != "(Global)"))
            //            {
            //                this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
            //                goto TR_00FC;
            //            }
            //            else
            //            {
            //                num24 = 0;
            //                num24 = this._addstTag(objA, prefix, blockType, num23, "0");
            //                if ((str4 != "(Retain)") || (num24 != 3))
            //                {
            //                    goto TR_00FC;
            //                }
            //                else
            //                {
            //                    DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //                }
            //            }
            //        }
            //    }
            //    break;
            //}
            goto TR_0000;
        TR_0137:
            num20++;
            goto TR_0147;
        TR_0138:
            this.m_Ctrl_Condition.InsertSymbol(objA);
            goto TR_0137;
        TR_0147:
            //while (true)
            //{
            //    flag = num20 < num18;
            //    if (flag)
            //    {
            //        num23 = 0;
            //        serBuffer = DBSrv.GetVarDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22, ref hType, ref dwDim, ref dwStringLength, ref dwFlags);
            //        if (!(serBuffer != IntPtr.Zero))
            //        {
            //            goto TR_0137;
            //        }
            //        else
            //        {
            //            objA = Marshal.PtrToStringAnsi(serBuffer);
            //            DBSrv.ReleaseSerBuffer(CommonConstants.g_hDBClient);
            //            prefix = Marshal.PtrToStringAnsi(DBSrv.GetTypeDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, hType, ref dwDim));
            //            str4 = Marshal.PtrToStringAnsi(DBSrv.GetGroupDesc(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], ref num12, ref num13, ref num14, ref num15, ref num16));
            //            num23 = Convert.ToByte(dwStringLength);
            //            if (str4 == "(Global)")
            //            {
            //                blockType = 0;
            //            }
            //            else if (str4 == "(Retain)")
            //            {
            //                blockType = 1;
            //            }
            //            else
            //            {
            //                blockType = 2;
            //                CommonConstants.stGroupType_editPara = true;
            //            }
            //            if ((str4 != "(Retain)") && (str4 != "(Global)"))
            //            {
            //                this._addstTag(this.ScreenName + "/" + objA, prefix, blockType, num23, "0");
            //                goto TR_0138;
            //            }
            //            else
            //            {
            //                num24 = 0;
            //                num24 = this._addstTag(objA, prefix, blockType, num23, "0");
            //                if ((str4 != "(Retain)") || (num24 != 3))
            //                {
            //                    goto TR_0138;
            //                }
            //                else
            //                {
            //                    DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, numArray[num20], num22);
            //                }
            //            }
            //        }
            //    }
            //    break;
            //}
            goto TR_0000;
        }
        */
        private void LadderWindow_Resize(object sender, EventArgs e)
        {
            CheckandLoadorRemoveVariables();
            ResizeWindow(this._dpiX, this._dpiY, this.Bounds.Width, this.Bounds.Height - 35);
        }

        private void CheckandLoadorRemoveVariables()
        {
            uint[] curvars = new uint[250];
            uint cnum = DBSrv.FindGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, "(Global)");
            uint ttl = DBSrv.GetVars(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, cnum, curvars);
            for (int cnt = 0; cnt < curvars.Length; cnt++)
            {
                uint num1 = DBSrv.DeleteVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, cnum, curvars[cnt]);

            }
            for (int i = 0; i < xm.LoadedProject.Tags.Count; i++)
            {
                string TagAddress = xm.LoadedProject.Tags[i].Tag == null ? xm.LoadedProject.Tags[i].LogicalAddress.ToString() : xm.LoadedProject.Tags[i].Tag.ToString();
                if (xm.LoadedProject.Tags[i].LogicalAddress.ToString().Contains("."))
                {
                    uint num = DBSrv.FindGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, "(Global)");
                    if (DBSrv.FindVarInGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, num, TagAddress) == 0)
                    {
                        uint num1 = DBSrv.CreateVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, num, UInt32.MaxValue, DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, "BOOL"), 0, 0, 0, TagAddress);
                    }
                }
                else
                {
                    uint num = DBSrv.FindGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, "(Global)");
                    if (DBSrv.FindVarInGroup(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, num, TagAddress) == 0)
                    {
                        uint num1 = DBSrv.CreateVar(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, num, UInt32.MaxValue, DBSrv.FindType(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, "WORD"), 0, 0, 0, TagAddress);
                    }

                }


            }
        }

        private void ResizeWindow(int dpiX, int dpiY, int width, int height)
        {
            if (m_Ctrl != null)
                m_Ctrl.MoveWindow(dpiX, dpiY, width, height);
        }

        private void tsbInsertContactParallal_Click(object sender, EventArgs e)
        {
            CommandInsertContactParallel();
        }

        private void tsbInsertContactBefore_Click(object sender, EventArgs e)
        {
            CommandInsertContactBefore();
        }


        public void CommandInsertCoil()
        {
            m_Ctrl.InsertCoil();
        }

        public void CommandInsertComment()
        {
            m_Ctrl.InsertComment();
        }

        public void CommandInsertContactAfter()
        {
            m_Ctrl.InsertContactAfter();

        }

        public void CommandInsertContactBefore()
        {
            m_Ctrl.InsertContactBefore();
        }

        public void CopyBlock()
        {
            m_Ctrl.Copy();
        }
        public void PasteBlock()
        {
            m_Ctrl.Paste();
        }
        public void CutBlock()
        {
            m_Ctrl.Cut();
        }
        public void RedoBlock()
        {
            m_Ctrl.Redo();
        }
        public void UndoBlock()
        {
            m_Ctrl.Undo();
        }
        public void CommandInsertContactParallel()
        {
            m_Ctrl.InsertContactParallel();
        }

        public void CommandInsertFBAfter()
        {
            m_Ctrl.InsertFBAfter();
        }

        public void CommandInsertFBBefore()
        {
            m_Ctrl.InsertFBBefore();
        }

        public void CommandInsertFBParallel()
        {
            m_Ctrl.InsertFBParallel();
        }

        public void CommandInsertHorizontalLine()
        {
            m_Ctrl.InsertHorz();

        }

        public void CommandZoom(int ZoomFactor)
        {
            m_Ctrl.SetZoom(0, ZoomFactor);
        }
        public void CommandInsertNewRung()
        {
            m_Ctrl.InsertRung();
        }

        public void CommandInsertJump()
        {
            m_Ctrl.InsertJump();
        }

        public void CommandAlignCoils()
        {
            m_Ctrl.AlignCoils();
        }

        public void CommandSwapItemStyle()
        {
            m_Ctrl.SwapItemStyle();
        }

        private void tsbInsertHorizontalLine_Click(object sender, EventArgs e)
        {
            CommandInsertHorizontalLine();
        }

        private void tsbSwapItemStyle_Click(object sender, EventArgs e)
        {
            CommandSwapItemStyle();
        }

        private void tsbInsertFBBefore_Click(object sender, EventArgs e)
        {
            CommandInsertFBBefore();
        }

        private void tsbInsertFBAfter_Click(object sender, EventArgs e)
        {
            CommandInsertFBAfter();
        }

        private void tsbInsertFBParallal_Click(object sender, EventArgs e)
        {
            CommandInsertFBParallel();
        }

        private void tsbInsertCoil_Click(object sender, EventArgs e)
        {
            CommandInsertCoil();
        }

        private void tsbInsertRung_Click(object sender, EventArgs e)
        {
            CommandInsertNewRung();
        }

        private void tsbInsertComment_Click(object sender, EventArgs e)
        {
            CommandInsertComment();
        }

        private void tsbInsertJump_Click(object sender, EventArgs e)
        {
            CommandInsertJump();
        }

        private void tsbAlignCoils_Click(object sender, EventArgs e)
        {
            CommandAlignCoils();
        }

        public void OnShown()
        {

        }

        private void tsbInsertContactAfter_Click(object sender, EventArgs e)
        {
            CommandInsertContactAfter();
        }

        private void tbcmdSave_Click(object sender, EventArgs e)
        {
            XMPS xm;
            xm = XMPS.Instance;
            m_Ctrl.Save(xm.CurrentProjectData.ProjectPath.ToString().Replace("xmprj", "link"));
        }

        public void SaveLadderWindows(string filePath)
        {
            m_Ctrl.Save(filePath);
        }

        internal void DeleteBlock()
        {
            m_Ctrl.Cut();
        }

        internal static void GetCSVFormat()
        {
            XMPS xm;
            xm = XMPS.Instance;
            //m_Ctrl.Save(xm.CurrentProjectData.ProjectPath.ToString().Replace(xm.CurrentProjectData.ProjectName.ToString(), xm.CurrentScreen.ToString()));
        }

        private void LadderWindow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("MouseDoubleClick");
        }

        private void LadderWindow_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("DoubleClick");
        }

        private void LadderWindow_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            MessageBox.Show("MouseDoubleClick");
        }


        public void AddBlockInfoTagData(int Count, string BlockName)
        {
            BlockInfo objBlockInfo = new BlockInfo(BlockName);
            uint num;
            uint num1 = DBSrv.FindProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, BlockName);
            if (num1 != 0)
            {
                num = DBSrv.DeleteProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, num1);
            }
            uint hPrg = DBSrv.CreateProgram(CommonConstants.g_hDBClient, CommonConstants.g_hDBProject, 8U, 4U, (uint)((int)base.Handle), BlockName);
            //    objTreeView.Nodes[Count].Nodes[objTreeView.Nodes[Count].Nodes.Count - 1].Tag = objBlockInfo;
        }

        private void LadderWindow_Activated(object sender, EventArgs e)
        {
            MessageBox.Show("LadderWindow_Activated");
        }

    }
}
