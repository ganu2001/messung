using System;
using System.Runtime.InteropServices;
using WinAPI;

namespace W5
{
	public class Edit
	{
		protected IntPtr m_hWnd = new IntPtr();

		public Edit()
		{
		}

		protected virtual int _Execute(string[] argv)
		{
			return 0;
		}

		private int _Execute(string szCmd)
		{
			return this._Execute(new string[] { szCmd });
		}

		private int _Execute(string szCmd, string szParam)
		{
			return this._Execute(new string[] { szCmd, szParam });
		}

		private int _Execute(string szCmd, string szParam1, string szParam2)
		{
			string[] strArrays = new string[] { szCmd, szParam1, szParam2 };
			return this._Execute(strArrays);
		}

		private int _Execute(string szCmd, string szParam1, string szParam2, string szParam3)
		{
			string[] strArrays = new string[] { szCmd, szParam1, szParam2, szParam3 };
			return this._Execute(strArrays);
		}

		private string _ExecuteString(string szCmd)
		{
			string[] strArrays = new string[] { szCmd };
			return Marshal.PtrToStringAnsi((IntPtr)this._Execute(strArrays));
		}

		private string _ExecuteString(string szCmd, string szParam)
		{
			string[] strArrays = new string[] { szCmd, szParam };
			return Marshal.PtrToStringAnsi((IntPtr)this._Execute(strArrays));
		}

		private string _ExecuteString(string szCmd, string szParam1, string szParam2)
		{
			string[] strArrays = new string[] { szCmd, szParam1, szParam2 };
			return Marshal.PtrToStringAnsi((IntPtr)this._Execute(strArrays));
		}

		private string _ExecuteString(string szCmd, string szParam1, string szParam2, string szParam3)
		{
			string[] strArrays = new string[] { szCmd, szParam1, szParam2, szParam3 };
			return Marshal.PtrToStringAnsi((IntPtr)this._Execute(strArrays));
		}

		protected virtual IntPtr _GetClassName()
		{
			return IntPtr.Zero;
		}

		public void ActiveStep(bool bParam1)
		{
			this._Execute("ActiveStep", bParam1.ToString());
		}

		public void AddFileTypes(uint ui32Param1)
		{
			this._Execute("AddFileTypes", ui32Param1.ToString());
		}

		public bool AddPrg(ushort ui16Param1)
		{
			bool flag = this._Execute("AddPrg", ui16Param1.ToString()) != 0;
			return flag;
		}

		public bool AdjustFolio(int iParam1, int iParam2)
		{
			bool flag = this._Execute("AdjustFolio", iParam1.ToString(), iParam2.ToString()) != 0;
			return flag;
		}

		public void Align(int iParam1, int iParam2)
		{
			this._Execute("Align", iParam1.ToString(), iParam2.ToString());
		}

		public void AlignCoils()
		{
			this._Execute("AlignCoils");
		}

		public void ArrangeColumns()
		{
			this._Execute("ArrangeColumns");
		}

		public void AutoDeclareInst(bool bParam1)
		{
			this._Execute("AutoDeclareInst", bParam1.ToString());
		}

		public void AutoDeclareSymbol(bool bParam1)
		{
			this._Execute("AutoDeclareSymbol", bParam1.ToString());
		}

		public bool CanActivate()
		{
			return this._Execute("CanActivate") != 0;
		}

		public bool CanAddPrg(ushort ui16Param1)
		{
			bool flag = this._Execute("CanAddPrg", ui16Param1.ToString()) != 0;
			return flag;
		}

		public bool CanAlign()
		{
			return this._Execute("CanAlign") != 0;
		}

		public bool CanAlignCoils()
		{
			return this._Execute("CanAlignCoils") != 0;
		}

		public bool CanChangeBkColor()
		{
			return this._Execute("CanChangeBkColor") != 0;
		}

		public bool CanChangeProperties()
		{
			return this._Execute("CanChangeProperties") != 0;
		}

		public bool CanCheck(int iParam1)
		{
			bool flag = this._Execute("CanCheck", iParam1.ToString()) != 0;
			return flag;
		}

		public bool CanCheckTree()
		{
			return this._Execute("CanCheckTree") != 0;
		}

		public bool CanClear()
		{
			return this._Execute("CanClear") != 0;
		}

		public bool CanClearNetwork()
		{
			return this._Execute("CanClearNetwork") != 0;
		}

		public bool CanClearRow()
		{
			return this._Execute("CanClearRow") != 0;
		}

		public bool CanCmdPrg(ushort ui16Param1)
		{
			bool flag = this._Execute("CanCmdPrg", ui16Param1.ToString()) != 0;
			return flag;
		}

		public bool CanCollapse()
		{
			return this._Execute("CanCollapse") != 0;
		}

		public bool CanCollapseAll()
		{
			return this._Execute("CanCollapseAll") != 0;
		}

		public bool CanConnect()
		{
			return this._Execute("CanConnect") != 0;
		}

		public bool CanCopy()
		{
			return this._Execute("CanCopy") != 0;
		}

		public bool CanCopyCol(int iParam1)
		{
			bool flag = this._Execute("CanCopyCol", iParam1.ToString()) != 0;
			return flag;
		}

		public bool CanCopyPrg()
		{
			return this._Execute("CanCopyPrg") != 0;
		}

		public bool CanCreateFile(uint ui32Param1)
		{
			bool flag = this._Execute("CanCreateFile", ui32Param1.ToString()) != 0;
			return flag;
		}

		public bool CanCreateItem(int iParam1, int iParam2)
		{
			bool flag = this._Execute("CanCreateItem", iParam1.ToString(), iParam2.ToString()) != 0;
			return flag;
		}

		public bool CanCreateUDFB()
		{
			return this._Execute("CanCreateUDFB") != 0;
		}

		public bool CanCut()
		{
			return this._Execute("CanCut") != 0;
		}

		public bool CanDecrease()
		{
			return this._Execute("CanDecrease") != 0;
		}

		public bool CanDisplayFBDOrder()
		{
			return this._Execute("CanDisplayFBDOrder") != 0;
		}

		public bool CanEdit()
		{
			return this._Execute("CanEdit") != 0;
		}

		public bool CanEditParameters()
		{
			return this._Execute("CanEditParameters") != 0;
		}

		public bool CanEditProperties()
		{
			return this._Execute("CanEditProperties") != 0;
		}

		public bool CanEditSelCode()
		{
			return this._Execute("CanEditSelCode") != 0;
		}

		public bool CanEnterSelRef()
		{
			return this._Execute("CanEnterSelRef") != 0;
		}

		public bool CanExecCommand(int iParam1)
		{
			bool flag = this._Execute("CanExecCommand", iParam1.ToString()) != 0;
			return flag;
		}

		public bool CanExpand()
		{
			return this._Execute("CanExpand") != 0;
		}

		public bool CanExpandAll()
		{
			return this._Execute("CanExpandAll") != 0;
		}

		public bool CanExport()
		{
			return this._Execute("CanExport") != 0;
		}

		public bool CanExportTree()
		{
			return this._Execute("CanExportTree") != 0;
		}

		public bool CanFormatProgram()
		{
			return this._Execute("CanFormatProgram") != 0;
		}

		public bool CanHexDisplay()
		{
			return this._Execute("CanHexDisplay") != 0;
		}

		public bool CanImport()
		{
			return this._Execute("CanImport") != 0;
		}

		public bool CanImportTree()
		{
			return this._Execute("CanImportTree") != 0;
		}

		public bool CanIncrease()
		{
			return this._Execute("CanIncrease") != 0;
		}

		public bool CanInsert()
		{
			return this._Execute("CanInsert") != 0;
		}

		public bool CanInsertCoil()
		{
			return this._Execute("CanInsertCoil") != 0;
		}

		public bool CanInsertCol(int iParam1)
		{
			bool flag = this._Execute("CanInsertCol", iParam1.ToString()) != 0;
			return flag;
		}

		public bool CanInsertComment()
		{
			return this._Execute("CanInsertComment") != 0;
		}

		public bool CanInsertContactAfter()
		{
			return this._Execute("CanInsertContactAfter") != 0;
		}

		public bool CanInsertContactBefore()
		{
			return this._Execute("CanInsertContactBefore") != 0;
		}

		public bool CanInsertContactParallel()
		{
			return this._Execute("CanInsertContactParallel") != 0;
		}

		public bool CanInsertFB()
		{
			return this._Execute("CanInsertFB") != 0;
		}

		public bool CanInsertFBAfter()
		{
			return this._Execute("CanInsertFBAfter") != 0;
		}

		public bool CanInsertFBBefore()
		{
			return this._Execute("CanInsertFBBefore") != 0;
		}

		public bool CanInsertFBParallel()
		{
			return this._Execute("CanInsertFBParallel") != 0;
		}

		public bool CanInsertFFLDItem(int iParam1)
		{
			bool flag = this._Execute("CanInsertFFLDItem", iParam1.ToString()) != 0;
			return flag;
		}

		public bool CanInsertFile()
		{
			return this._Execute("CanInsertFile") != 0;
		}

		public bool CanInsertHorz()
		{
			return this._Execute("CanInsertHorz") != 0;
		}

		public bool CanInsertJump()
		{
			return this._Execute("CanInsertJump") != 0;
		}

		public bool CanInsertMasterPort()
		{
			return this._Execute("CanInsertMasterPort") != 0;
		}

		public bool CanInsertNetwork()
		{
			return this._Execute("CanInsertNetwork") != 0;
		}

		public bool CanInsertRung()
		{
			return this._Execute("CanInsertRung") != 0;
		}

		public bool CanInsertSlaveRequest()
		{
			return this._Execute("CanInsertSlaveRequest") != 0;
		}

		public bool CanInsertStructure()
		{
			return this._Execute("CanInsertStructure") != 0;
		}

		public bool CanInsertSymbol()
		{
			return this._Execute("CanInsertSymbol") != 0;
		}

		public bool CanInsertText()
		{
			return this._Execute("CanInsertText") != 0;
		}

		public bool CanMoveBottom()
		{
			return this._Execute("CanMoveBottom") != 0;
		}

		public bool CanMoveDown()
		{
			return this._Execute("CanMoveDown") != 0;
		}

		public bool CanMovePrg(ushort ui16Param1)
		{
			bool flag = this._Execute("CanMovePrg", ui16Param1.ToString()) != 0;
			return flag;
		}

		public bool CanMoveStructure(ushort ui16Param1)
		{
			bool flag = this._Execute("CanMoveStructure", ui16Param1.ToString()) != 0;
			return flag;
		}

		public bool CanMoveTop()
		{
			return this._Execute("CanMoveTop") != 0;
		}

		public bool CanMoveUp()
		{
			return this._Execute("CanMoveUp") != 0;
		}

		public bool CanOpenPrg()
		{
			return this._Execute("CanOpenPrg") != 0;
		}

		public bool CanPaste()
		{
			return this._Execute("CanPaste") != 0;
		}

		public bool CanPrint()
		{
			return this._Execute("CanPrint") != 0;
		}

		public bool CanRecordSampling()
		{
			return this._Execute("CanRecordSampling") != 0;
		}

		public bool CanRedo()
		{
			return this._Execute("CanRedo") != 0;
		}

		public bool CanRemoveCol(int iParam1)
		{
			bool flag = this._Execute("CanRemoveCol", iParam1.ToString()) != 0;
			return flag;
		}

		public bool CanRemoveComment()
		{
			return this._Execute("CanRemoveComment") != 0;
		}

		public bool CanRenameCol(int iParam1)
		{
			bool flag = this._Execute("CanRenameCol", iParam1.ToString()) != 0;
			return flag;
		}

		public bool CanRenamePrg()
		{
			return this._Execute("CanRenamePrg") != 0;
		}

		public bool CanRenumber()
		{
			return this._Execute("CanRenumber") != 0;
		}

		public bool CanResize()
		{
			return this._Execute("CanResize") != 0;
		}

		public bool CanRotateCorners()
		{
			return this._Execute("CanRotateCorners") != 0;
		}

		public bool CanSave()
		{
			return this._Execute("CanSave") != 0;
		}

		public bool CanSaveValues()
		{
			return this._Execute("CanSaveValues") != 0;
		}

		public bool CanSelectAll()
		{
			return this._Execute("CanSelectAll") != 0;
		}

		public bool CanSendReceipe(int iParam1)
		{
			bool flag = this._Execute("CanSendReceipe", iParam1.ToString()) != 0;
			return flag;
		}

		public bool CanSetGrid()
		{
			return this._Execute("CanSetGrid") != 0;
		}

		public bool CanSetTab()
		{
			return this._Execute("CanSetTab") != 0;
		}

		public bool CanSetupSampling()
		{
			return this._Execute("CanSetupSampling") != 0;
		}

		public bool CanSetZoom()
		{
			return this._Execute("CanSetZoom") != 0;
		}

		public bool CanSort(int iParam1)
		{
			bool flag = this._Execute("CanSort", iParam1.ToString()) != 0;
			return flag;
		}

		public bool CanStartSampling()
		{
			return this._Execute("CanStartSampling") != 0;
		}

		public bool CanStopSampling()
		{
			return this._Execute("CanStopSampling") != 0;
		}

		public bool CanSwapGlobalRet()
		{
			return this._Execute("CanSwapGlobalRet") != 0;
		}

		public bool CanSwapItemStyle()
		{
			return this._Execute("CanSwapItemStyle") != 0;
		}

		public bool CanSwapStyle()
		{
			return this._Execute("CanSwapStyle") != 0;
		}

		public bool CanUncheck()
		{
			return this._Execute("CanUncheck") != 0;
		}

		public bool CanUndo()
		{
			return this._Execute("CanUndo") != 0;
		}

		public bool CanViewInfo()
		{
			return this._Execute("CanViewInfo") != 0;
		}

		public void ChangeBkColor()
		{
			this._Execute("ChangeBkColor");
		}

		public void ChangeSetup()
		{
			this._Execute("ChangeSetup");
		}

		public void Check(int iParam1)
		{
			this._Execute("Check", iParam1.ToString());
		}

		public bool CheckAllProp()
		{
			return this._Execute("CheckAllProp") != 0;
		}

		public string CheckTree()
		{
			return this._ExecuteString("CheckTree");
		}

		public void Clear()
		{
			this._Execute("Clear");
		}

		public bool ClearNetwork()
		{
			return this._Execute("ClearNetwork") != 0;
		}

		public bool ClearRow()
		{
			return this._Execute("ClearRow") != 0;
		}

		public bool CmdPrg(ushort ui16Param1)
		{
			bool flag = this._Execute("CmdPrg", ui16Param1.ToString()) != 0;
			return flag;
		}

		public void Collapse()
		{
			this._Execute("Collapse");
		}

		public void CollapseAll()
		{
			this._Execute("CollapseAll");
		}

		public bool Connect()
		{
			return this._Execute("Connect") != 0;
		}

		public void Copy()
		{
			this._Execute("Copy");
		}

		public void CopyBitmap(bool bParam1)
		{
			this._Execute("CopyBitmap", bParam1.ToString());
		}

		public bool CopyCol(int iParam1)
		{
			bool flag = this._Execute("CopyCol", iParam1.ToString()) != 0;
			return flag;
		}

		public virtual bool CreateDico(uint dwStyle, int x, int y, int width, int height, IntPtr hWndParent, uint iID)
		{
			return false;
		}

		public bool CreateFile(uint ui32Param1)
		{
			bool flag = this._Execute("CreateFile", ui32Param1.ToString()) != 0;
			return flag;
		}

		public bool CreateItem(int iParam1, int iParam2)
		{
			bool flag = this._Execute("CreateItem", iParam1.ToString(), iParam2.ToString()) != 0;
			return flag;
		}

		public bool CreateUDFB()
		{
			return this._Execute("CreateUDFB") != 0;
		}

		public bool CreateWnd(uint dwStyle, int x, int y, int width, int height, IntPtr hWndParent, uint iID)
		{
			IntPtr intPtr = new IntPtr((long)iID);
			string stringAnsi = Marshal.PtrToStringAnsi(this._GetClassName());
			this.m_hWnd = CWnd.CreateWindowEx(0, stringAnsi, "", 1342210048, 0, 0, 100, 100, hWndParent, intPtr, IntPtr.Zero, IntPtr.Zero);
			return true;
		}

		public void Cut()
		{
			this._Execute("Cut");
		}

		public void DebugProject(uint ui32Param1, bool bParam2)
		{
			this._Execute("DebugProject", ui32Param1.ToString(), bParam2.ToString());
		}

		public void Decrease()
		{
			this._Execute("Decrease");
		}

		public void DeselectAll()
		{
			this._Execute("DeselectAll");
		}

		public void DisplayConfirmation(bool bParam1)
		{
			this._Execute("DisplayConfirmation", bParam1.ToString());
		}

		public uint DisplayFBDOrder()
		{
			return (uint)this._Execute("DisplayFBDOrder");
		}

		public void DisplayFolio(bool bParam1)
		{
			this._Execute("DisplayFolio", bParam1.ToString());
		}

		public void DisplayIO(bool bParam1)
		{
			this._Execute("DisplayIO", bParam1.ToString());
		}

		public void EditInPlace(bool bParam1)
		{
			this._Execute("EditInPlace", bParam1.ToString());
		}

		public void EditParameters()
		{
			this._Execute("EditParameters");
		}

		public void EditPropAfterInsertVar(bool bParam1)
		{
			this._Execute("EditPropAfterInsertVar", bParam1.ToString());
		}

		public bool EditProperties()
		{
			return this._Execute("EditProperties") != 0;
		}

		public void Empty()
		{
			this._Execute("Empty");
		}

		public void EmptyUndoStack()
		{
			this._Execute("EmptyUndoStack");
		}

		public void EnableAutoScroll(bool bParam1)
		{
			this._Execute("EnableAutoScroll", bParam1.ToString());
		}

		public void EnableCopy(bool bParam1)
		{
			this._Execute("EnableCopy", bParam1.ToString());
		}

		public void EnableDragnDrop(bool bParam1)
		{
			this._Execute("EnableDragnDrop", bParam1.ToString());
		}

		public void EnablePulse(bool bParam1)
		{
			this._Execute("EnablePulse", bParam1.ToString());
		}

		public void EnableSyntax(bool bParam1)
		{
			this._Execute("EnableSyntax", bParam1.ToString());
		}

		public void EnsureSelVisible()
		{
			this._Execute("EnsureSelVisible");
		}

		public void EnsureVisible(IntPtr ptrParam1)
		{
			this._Execute("EnsureVisible", ptrParam1.ToString());
		}

		public void EnterSelRef()
		{
			this._Execute("EnterSelRef");
		}

		public void EVTChanged()
		{
			this._Execute("EVTChanged");
		}

		public bool ExecCommand(int iParam1)
		{
			bool flag = this._Execute("ExecCommand", iParam1.ToString()) != 0;
			return flag;
		}

		public void Expand()
		{
			this._Execute("Expand");
		}

		public void ExpandAll()
		{
			this._Execute("ExpandAll");
		}

		public void ExpandItem(IntPtr ptrParam1, bool bParam2)
		{
			this._Execute("ExpandItem", ptrParam1.ToString(), bParam2.ToString());
		}

		public void Export()
		{
			this._Execute("Export");
		}

		public void ExportComment()
		{
			this._Execute("ExportComment");
		}

		public void ExportTree()
		{
			this._Execute("ExportTree");
		}

		public void FilterGroup(string sParam1)
		{
			this._Execute("FilterGroup", sParam1);
		}

		public int FindReplace(string sParam1, string sParam2, uint ui32Param3, uint ui32Param4, IntPtr ptrParam5)
		{
			string[] strArrays = new string[] { "FindReplace", sParam1, sParam2, ui32Param3.ToString(), ui32Param4.ToString(), ptrParam5.ToString() };
			return this._Execute(strArrays);
		}

		public void FocusGroup(string sParam1)
		{
			this._Execute("FocusGroup", sParam1);
		}

		public void FocusTree()
		{
			this._Execute("FocusTree");
		}

		public void FocusVar(string sParam1, string sParam2)
		{
			this._Execute("FocusVar", sParam1, sParam2);
		}

		public void FormatProgram()
		{
			this._Execute("FormatProgram");
		}

		public bool GetActivation()
		{
			return this._Execute("GetActivation") != 0;
		}

		public string GetAllProps(string sParam1)
		{
			return this._ExecuteString("GetAllProps", sParam1);
		}

		public string GetBkpList()
		{
			return this._ExecuteString("GetBkpList");
		}

		public int GetCaret()
		{
			return this._Execute("GetCaret");
		}

		public int GetCellHeight()
		{
			return this._Execute("GetCellHeight");
		}

		public int GetCellWidth()
		{
			return this._Execute("GetCellWidth");
		}

		public IntPtr GetChildItem(IntPtr ptrParam1)
		{
			return (IntPtr)this._Execute("GetChildItem", ptrParam1.ToString());
		}

		public uint GetColItemParam(int iParam1)
		{
			return (uint)this._Execute("GetColItemParam", iParam1.ToString());
		}

		public string GetColItemText(int iParam1)
		{
			return this._ExecuteString("GetColItemText", iParam1.ToString());
		}

		public ushort GetColType(int iParam1)
		{
			return (ushort)this._Execute("GetColType", iParam1.ToString());
		}

		public int GetColWidth(int iParam1)
		{
			return this._Execute("GetColWidth", iParam1.ToString());
		}

		public string GetCommand(int iParam1)
		{
			return this._ExecuteString("GetCommand", iParam1.ToString());
		}

		public string GetCommandLine()
		{
			return this._ExecuteString("GetCommandLine");
		}

		public string GetCommentNote(int iParam1)
		{
			return this._ExecuteString("GetCommentNote", iParam1.ToString());
		}

		public int GetContentHeight()
		{
			return this._Execute("GetContentHeight");
		}

		public int GetContentWidth()
		{
			return this._Execute("GetContentWidth");
		}

		public string GetCSVFormat()
		{
			return this._ExecuteString("GetCSVFormat");
		}

		public int GetCurPos()
		{
			return this._Execute("GetCurPos");
		}

		public int GetCurrentCol()
		{
			return this._Execute("GetCurrentCol");
		}

		public ushort GetDisplay()
		{
			return (ushort)this._Execute("GetDisplay");
		}

		public bool GetEditMode()
		{
			return this._Execute("GetEditMode") != 0;
		}

		public string GetError()
		{
			return this._ExecuteString("GetError");
		}

		public ushort GetExColType(int iParam1)
		{
			return (ushort)this._Execute("GetExColType", iParam1.ToString());
		}

		public string GetFB()
		{
			return this._ExecuteString("GetFB");
		}

		public uint GetFileID()
		{
			return (uint)this._Execute("GetFileID");
		}

		public uint GetFileSection()
		{
			return (uint)this._Execute("GetFileSection");
		}

		public int GetFirstChar()
		{
			return this._Execute("GetFirstChar");
		}

		public IntPtr GetFirstItem()
		{
			return (IntPtr)this._Execute("GetFirstItem");
		}

		public IntPtr GetFirstSel()
		{
			return (IntPtr)this._Execute("GetFirstSel");
		}

		public uint GetFolderID()
		{
			return (uint)this._Execute("GetFolderID");
		}

		public uint GetGroupID()
		{
			return (uint)this._Execute("GetGroupID");
		}

		public IntPtr GetHandle()
		{
			return this.m_hWnd;
		}

		public int GetHeight()
		{
			return this._Execute("GetHeight");
		}

		public string GetInstance()
		{
			return this._ExecuteString("GetInstance");
		}

		public string GetInterfaces()
		{
			return this._ExecuteString("GetInterfaces");
		}

		public uint GetItemData(IntPtr ptrParam1)
		{
			return (uint)this._Execute("GetItemData", ptrParam1.ToString());
		}

		public uint GetItemGUID(int iParam1, int iParam2)
		{
			uint num = (uint)this._Execute("GetItemGUID", iParam1.ToString(), iParam2.ToString());
			return num;
		}

		public uint GetItemID()
		{
			return (uint)this._Execute("GetItemID");
		}

		public uint GetItemSubType()
		{
			return (uint)this._Execute("GetItemSubType");
		}

		public string GetItemText(IntPtr ptrParam1, int iParam2)
		{
			string str = this._ExecuteString("GetItemText", ptrParam1.ToString(), iParam2.ToString());
			return str;
		}

		public uint GetItemType()
		{
			return (uint)this._Execute("GetItemType");
		}

		public string GetItemTypeSel()
		{
			return this._ExecuteString("GetItemTypeSel");
		}

		public string GetKey()
		{
			return this._ExecuteString("GetKey");
		}

		public int GetLastChar()
		{
			return this._Execute("GetLastChar");
		}

		public int GetLastKeyDown()
		{
			return this._Execute("GetLastKeyDown");
		}

		public string GetLink()
		{
			return this._ExecuteString("GetLink");
		}

		public string GetMacroNote(int iParam1)
		{
			return this._ExecuteString("GetMacroNote", iParam1.ToString());
		}

		public int GetNbCol()
		{
			return this._Execute("GetNbCol");
		}

		public int GetNbCommand()
		{
			return this._Execute("GetNbCommand");
		}

		public int GetNbHorzFolioEx()
		{
			return this._Execute("GetNbHorzFolioEx");
		}

		public int GetNbItem()
		{
			return this._Execute("GetNbItem");
		}

		public int GetNbPrg()
		{
			return this._Execute("GetNbPrg");
		}

		public int GetNbVertFolioEx()
		{
			return this._Execute("GetNbVertFolioEx");
		}

		public int GetNetItemSel()
		{
			return this._Execute("GetNetItemSel");
		}

		public int GetNetSel()
		{
			return this._Execute("GetNetSel");
		}

		public IntPtr GetNextLineItem(IntPtr ptrParam1)
		{
			return (IntPtr)this._Execute("GetNextLineItem", ptrParam1.ToString());
		}

		public IntPtr GetNextSel(IntPtr ptrParam1)
		{
			return (IntPtr)this._Execute("GetNextSel", ptrParam1.ToString());
		}

		public IntPtr GetNextSiblingItem(IntPtr ptrParam1)
		{
			return (IntPtr)this._Execute("GetNextSiblingItem", ptrParam1.ToString());
		}

		public IntPtr GetNextVisibleItem(IntPtr ptrParam1)
		{
			return (IntPtr)this._Execute("GetNextVisibleItem", ptrParam1.ToString());
		}

		public bool GetNoteDisplay()
		{
			return this._Execute("GetNoteDisplay") != 0;
		}

		public string GetParent()
		{
			return this._ExecuteString("GetParent");
		}

		public IntPtr GetParentItem(IntPtr ptrParam1)
		{
			return (IntPtr)this._Execute("GetParentItem", ptrParam1.ToString());
		}

		public IntPtr GetPrevLineItem(IntPtr ptrParam1)
		{
			return (IntPtr)this._Execute("GetPrevLineItem", ptrParam1.ToString());
		}

		public IntPtr GetPrevSiblingItem(IntPtr ptrParam1)
		{
			return (IntPtr)this._Execute("GetPrevSiblingItem", ptrParam1.ToString());
		}

		public uint GetPrgID()
		{
			return (uint)this._Execute("GetPrgID");
		}

		public string GetPrgName()
		{
			return this._ExecuteString("GetPrgName");
		}

		public string GetPrintableText()
		{
			return this._ExecuteString("GetPrintableText");
		}

		public uint GetProgramID()
		{
			return (uint)this._Execute("GetProgramID");
		}

		public string GetProgramName()
		{
			return this._ExecuteString("GetProgramName");
		}

		public uint GetProjectID()
		{
			return (uint)this._Execute("GetProjectID");
		}

		public string GetProjectPath()
		{
			return this._ExecuteString("GetProjectPath");
		}

		public string GetProperties()
		{
			return this._ExecuteString("GetProperties");
		}

		public string GetPropHeader(int iParam1)
		{
			return this._ExecuteString("GetPropHeader", iParam1.ToString());
		}

		public string GetPropName(int iParam1)
		{
			return this._ExecuteString("GetPropName", iParam1.ToString());
		}

		public int GetPropType(int iParam1)
		{
			return this._Execute("GetPropType", iParam1.ToString());
		}

		public string GetPropValue(int iParam1)
		{
			return this._ExecuteString("GetPropValue", iParam1.ToString());
		}

		public string GetRules()
		{
			return this._ExecuteString("GetRules");
		}

		public uint GetScreenID()
		{
			return (uint)this._Execute("GetScreenID");
		}

		public IntPtr GetSelectedItem()
		{
			return (IntPtr)this._Execute("GetSelectedItem");
		}

		public string GetSelGroupName()
		{
			return this._ExecuteString("GetSelGroupName");
		}

		public int GetSelPos()
		{
			return this._Execute("GetSelPos");
		}

		public int GetSelRefNum()
		{
			return this._Execute("GetSelRefNum");
		}

		public int GetSelSize()
		{
			return this._Execute("GetSelSize");
		}

		public string GetSelText()
		{
			return this._ExecuteString("GetSelText");
		}

		public int GetSelTextLength()
		{
			return this._Execute("GetSelTextLength");
		}

		public uint GetSelVarID()
		{
			return (uint)this._Execute("GetSelVarID");
		}

		public string GetSelVarName()
		{
			return this._ExecuteString("GetSelVarName");
		}

		public string GetSerialCol()
		{
			return this._ExecuteString("GetSerialCol");
		}

		public string GetSerialExpand()
		{
			return this._ExecuteString("GetSerialExpand");
		}

		public int GetSortedCol()
		{
			return this._Execute("GetSortedCol");
		}

		public ushort GetSortMethod()
		{
			return (ushort)this._Execute("GetSortMethod");
		}

		public string GetStepCode_Def(int iParam1)
		{
			return this._ExecuteString("GetStepCode_Def", iParam1.ToString());
		}

		public string GetStepCode_N(int iParam1)
		{
			return this._ExecuteString("GetStepCode_N", iParam1.ToString());
		}

		public string GetStepCode_P0(int iParam1)
		{
			return this._ExecuteString("GetStepCode_P0", iParam1.ToString());
		}

		public string GetStepCode_P1(int iParam1)
		{
			return this._ExecuteString("GetStepCode_P1", iParam1.ToString());
		}

		public int GetStepLanguageN(int iParam1)
		{
			return this._Execute("GetStepLanguageN", iParam1.ToString());
		}

		public int GetStepLanguageP0(int iParam1)
		{
			return this._Execute("GetStepLanguageP0", iParam1.ToString());
		}

		public int GetStepLanguageP1(int iParam1)
		{
			return this._Execute("GetStepLanguageP1", iParam1.ToString());
		}

		public string GetStepNote(int iParam1)
		{
			return this._ExecuteString("GetStepNote", iParam1.ToString());
		}

		public string GetSymbolName()
		{
			return this._ExecuteString("GetSymbolName");
		}

		public int GetSymbolPos()
		{
			return this._Execute("GetSymbolPos");
		}

		public bool GetTargetMark(IntPtr ptrParam1)
		{
			bool flag = this._Execute("GetTargetMark", ptrParam1.ToString()) != 0;
			return flag;
		}

		public string GetText()
		{
			return this._ExecuteString("GetText");
		}

		public int GetTextLenght()
		{
			return this._Execute("GetTextLenght");
		}

		public string GetTransCode(int iParam1)
		{
			return this._ExecuteString("GetTransCode", iParam1.ToString());
		}

		public int GetTransLanguage(int iParam1)
		{
			return this._Execute("GetTransLanguage", iParam1.ToString());
		}

		public string GetTransNote(int iParam1)
		{
			return this._ExecuteString("GetTransNote", iParam1.ToString());
		}

		public string GetTreeParent()
		{
			return this._ExecuteString("GetTreeParent");
		}

		public string GetTypeName()
		{
			return this._ExecuteString("GetTypeName");
		}

		public string GetUsedBlock()
		{
			return this._ExecuteString("GetUsedBlock");
		}

		public int GetValueInText()
		{
			return this._Execute("GetValueInText");
		}

		public int GetWidth()
		{
			return this._Execute("GetWidth");
		}

		public int GetZoom()
		{
			return this._Execute("GetZoom");
		}

		public bool GetZOrderStruct(int iParam1, IntPtr ptrParam2)
		{
			bool flag = this._Execute("GetZOrderStruct", iParam1.ToString(), ptrParam2.ToString()) != 0;
			return flag;
		}

		public void Goto(string sParam1)
		{
			this._Execute("Goto", sParam1);
		}

		public void GotoXY(uint ui32Param1, uint ui32Param2)
		{
			this._Execute("GotoXY", ui32Param1.ToString(), ui32Param2.ToString());
		}

		public bool HasBreakpoint()
		{
			return this._Execute("HasBreakpoint") != 0;
		}

		public bool HasSelection()
		{
			return this._Execute("HasSelection") != 0;
		}

		public bool HasTracepoint()
		{
			return this._Execute("HasTracepoint") != 0;
		}

		public string Help(string sParam1)
		{
			return this._ExecuteString("Help", sParam1);
		}

		public string HelpOn(string sParam1)
		{
			return this._ExecuteString("HelpOn", sParam1);
		}

		public void HideArrangeCol()
		{
			this._Execute("HideArrangeCol");
		}

		public void HideScroll(bool bParam1)
		{
			this._Execute("HideScroll", bParam1.ToString());
		}

		public void Import()
		{
			this._Execute("Import");
		}

		public void ImportComment()
		{
			this._Execute("ImportComment");
		}

		public void ImportTree()
		{
			this._Execute("ImportTree");
		}

		public void Increase()
		{
			this._Execute("Increase");
		}

		public void InsertCnv()
		{
			this._Execute("InsertCnv");
		}

		public void InsertCoil()
		{
			this._Execute("InsertCoil");
		}

		public int InsertCol(int iParam1, string sParam2, ushort ui16Param3, ushort ui16Param4)
		{
			string[] str = new string[] { "InsertCol", iParam1.ToString(), sParam2, ui16Param3.ToString(), ui16Param4.ToString() };
			return this._Execute(str);
		}

		public void InsertComment()
		{
			this._Execute("InsertComment");
		}

		public void InsertContactAfter()
		{
			this._Execute("InsertContactAfter");
		}

		public void InsertContactBefore()
		{
			this._Execute("InsertContactBefore");
		}

		public void InsertContactParallel()
		{
			this._Execute("InsertContactParallel");
		}

		public void InsertDiv()
		{
			this._Execute("InsertDiv");
		}

		public void InsertFB(string sParam1)
		{
			this._Execute("InsertFB", sParam1);
		}

		public void InsertFBAfter()
		{
			this._Execute("InsertFBAfter");
		}

		public void InsertFBBefore()
		{
			this._Execute("InsertFBBefore");
		}

		public void InsertFBParallel()
		{
			this._Execute("InsertFBParallel");
		}

		public bool InsertFFLDItem(int iParam1)
		{
			bool flag = this._Execute("InsertFFLDItem", iParam1.ToString()) != 0;
			return flag;
		}

		public void InsertFile(string sParam1)
		{
			this._Execute("InsertFile", sParam1);
		}

		public void InsertHorz()
		{
			this._Execute("InsertHorz");
		}

		public void InsertInitStep()
		{
			this._Execute("InsertInitStep");
		}

		public IntPtr InsertItem(bool bParam1, string sParam2)
		{
			IntPtr intPtr = (IntPtr)this._Execute("InsertItem", bParam1.ToString(), sParam2);
			return intPtr;
		}

		public IntPtr InsertItemEx(string sParam1, IntPtr ptrParam2, IntPtr ptrParam3)
		{
			IntPtr intPtr = (IntPtr)this._Execute("InsertItemEx", sParam1, ptrParam2.ToString(), ptrParam3.ToString());
			return intPtr;
		}

		public void InsertJump()
		{
			this._Execute("InsertJump");
		}

		public void InsertMacro()
		{
			this._Execute("InsertMacro");
		}

		public void InsertMacroBody()
		{
			this._Execute("InsertMacroBody");
		}

		public void InsertMainDiv()
		{
			this._Execute("InsertMainDiv");
		}

		public void InsertMasterPort()
		{
			this._Execute("InsertMasterPort");
		}

		public void InsertNetwork()
		{
			this._Execute("InsertNetwork");
		}

		public void InsertRung()
		{
			this._Execute("InsertRung");
		}

		public void InsertSlaveRequest()
		{
			this._Execute("InsertSlaveRequest");
		}

		public void InsertStep()
		{
			this._Execute("InsertStep");
		}

		public void InsertStructure()
		{
			this._Execute("InsertStructure");
		}

		public void InsertSymbol(string sParam1)
		{
			this._Execute("InsertSymbol", sParam1);
		}

		public void InsertText(int iParam1, int iParam2, string sParam3)
		{
			this._Execute("InsertText", iParam1.ToString(), iParam2.ToString(), sParam3);
		}

		public void InsertTrans()
		{
			this._Execute("InsertTrans");
		}

		public void InsertVarAfterInsertFB(bool bParam1)
		{
			this._Execute("InsertVarAfterInsertFB", bParam1.ToString());
		}

		public bool IsAutoScroll()
		{
			return this._Execute("IsAutoScroll") != 0;
		}

		public bool IsCheck(int iParam1)
		{
			bool flag = this._Execute("IsCheck", iParam1.ToString()) != 0;
			return flag;
		}

		public bool IsDebug()
		{
			return this._Execute("IsDebug") != 0;
		}

		public bool IsDicoEnable(bool bParam1, bool bParam2, bool bParam3)
		{
			bool flag = this._Execute("IsDicoEnable", bParam1.ToString(), bParam2.ToString(), bParam3.ToString()) != 0;
			return flag;
		}

		public bool IsDisplayFolio()
		{
			return this._Execute("IsDisplayFolio") != 0;
		}

		public bool IsEmpty()
		{
			return this._Execute("IsEmpty") != 0;
		}

		public bool IsEnable()
		{
			return this._Execute("IsEnable") != 0;
		}

		public bool IsGridVisible()
		{
			return this._Execute("IsGridVisible") != 0;
		}

		public bool IsHexDisplay()
		{
			return this._Execute("IsHexDisplay") != 0;
		}

		public bool IsItemExpanded(IntPtr ptrParam1)
		{
			bool flag = this._Execute("IsItemExpanded", ptrParam1.ToString()) != 0;
			return flag;
		}

		public bool IsLoaded()
		{
			return this._Execute("IsLoaded") != 0;
		}

		public bool IsModified()
		{
			return this._Execute("IsModified") != 0;
		}

		public bool IsOutOfDate()
		{
			return this._Execute("IsOutOfDate") != 0;
		}

		public bool IsPrintGraphic()
		{
			return this._Execute("IsPrintGraphic") != 0;
		}

		public bool IsPrintText()
		{
			return this._Execute("IsPrintText") != 0;
		}

		public bool IsReadOnly()
		{
			return this._Execute("IsReadOnly") != 0;
		}

		public bool IsRecording()
		{
			return this._Execute("IsRecording") != 0;
		}

		public uint IsSelComment()
		{
			return (uint)this._Execute("IsSelComment");
		}

		public int IsSelMacro()
		{
			return this._Execute("IsSelMacro");
		}

		public int IsSelStep()
		{
			return this._Execute("IsSelStep");
		}

		public int IsSelTrans()
		{
			return this._Execute("IsSelTrans");
		}

		public bool IsSortAscending()
		{
			return this._Execute("IsSortAscending") != 0;
		}

		public int IsStep(int iParam1, int iParam2)
		{
			int num = this._Execute("IsStep", iParam1.ToString(), iParam2.ToString());
			return num;
		}

		public bool IsStepLock(int ptrParam1)
		{
			bool flag = this._Execute("IsStepLock", ptrParam1.ToString()) != 0;
			return flag;
		}

		public int IsTrans(int iParam1, int iParam2)
		{
			int num = this._Execute("IsTrans", iParam1.ToString(), iParam2.ToString());
			return num;
		}

		public bool IsTransLock(int ptrParam1)
		{
			bool flag = this._Execute("IsTransLock", ptrParam1.ToString()) != 0;
			return flag;
		}

		public bool IsUndef()
		{
			return this._Execute("IsUndef") != 0;
		}

		public void KeepFBDSelect(bool bParam1)
		{
			this._Execute("KeepFBDSelect", bParam1.ToString());
		}

		public string ListUpdateUDFB()
		{
			return this._ExecuteString("ListUpdateUDFB");
		}

		public void Load(string sParam1)
		{
			this._Execute("Load", sParam1);
		}

		public void LoadExpand(string sParam1)
		{
			this._Execute("LoadExpand", sParam1);
		}

		public void LocateError(string sParam1)
		{
			this._Execute("LocateError", sParam1);
		}

		public void Lock(bool bParam1)
		{
			this._Execute("Lock", bParam1.ToString());
		}

		public void LockBinding(bool bParam1)
		{
			this._Execute("LockBinding", bParam1.ToString());
		}

		public void LockComment(bool bParam1, int ptrParam2)
		{
			this._Execute("LockComment", bParam1.ToString(), ptrParam2.ToString());
		}

		public void LockConfig(bool bParam1)
		{
			this._Execute("LockConfig", bParam1.ToString());
		}

		public void LockItemType(bool bParam1, uint ui32Param2, uint ui32Param3, string sParam4)
		{
			string[] str = new string[] { "LockItemType", bParam1.ToString(), ui32Param2.ToString(), ui32Param3.ToString(), sParam4 };
			this._Execute(str);
		}

		public void LockMacro(bool bParam1, int ptrParam2)
		{
			this._Execute("LockMacro", bParam1.ToString(), ptrParam2.ToString());
		}

		public void LockStep(bool bParam1, int ptrParam2)
		{
			this._Execute("LockStep", bParam1.ToString(), ptrParam2.ToString());
		}

		public void LockTrans(bool bParam1, int ptrParam2)
		{
			this._Execute("LockTrans", bParam1.ToString(), ptrParam2.ToString());
		}

		public void MoveAfter(uint ui32Param1, uint ui32Param2)
		{
			this._Execute("MoveAfter", ui32Param1.ToString(), ui32Param2.ToString());
		}

		public void MoveBefore(uint ui32Param1, uint ui32Param2)
		{
			this._Execute("MoveBefore", ui32Param1.ToString(), ui32Param2.ToString());
		}

		public void MoveBottom()
		{
			this._Execute("MoveBottom");
		}

		public void MoveDown()
		{
			this._Execute("MoveDown");
		}

		public IntPtr MoveItemAfter(IntPtr ptrParam1, IntPtr ptrParam2)
		{
			IntPtr intPtr = (IntPtr)this._Execute("MoveItemAfter", ptrParam1.ToString(), ptrParam2.ToString());
			return intPtr;
		}

		public bool MovePrg(ushort ui16Param1)
		{
			bool flag = this._Execute("MovePrg", ui16Param1.ToString()) != 0;
			return flag;
		}

		public void MoveStructure(ushort ui16Param1)
		{
			this._Execute("MoveStructure", ui16Param1.ToString());
		}

		public void MoveTop()
		{
			this._Execute("MoveTop");
		}

		public void MoveUp()
		{
			this._Execute("MoveUp");
		}

		public void MoveWindow(int iX, int iY, int iWidth, int iHeight)
		{
			CWnd.MoveWindow(this.m_hWnd, iX, iY, iWidth, iHeight, true);
		}

		public bool NeedSetup()
		{
			return this._Execute("NeedSetup") != 0;
		}

		public bool NeedUpdateUDFB()
		{
			return this._Execute("NeedUpdateUDFB") != 0;
		}

		public void NewFile()
		{
			this._Execute("NewFile");
		}

		public int NextPosItem(bool bParam1)
		{
			return this._Execute("NextPosItem", bParam1.ToString());
		}

		public void NotifChanges(bool bParam1)
		{
			this._Execute("NotifChanges", bParam1.ToString());
		}

		public int PaintToDC(IntPtr ptrParam1, int iParam2, int iParam3, int iParam4, int iParam5)
		{
			string[] str = new string[] { "PaintToDC", ptrParam1.ToString(), iParam2.ToString(), iParam3.ToString(), iParam4.ToString(), iParam5.ToString() };
			return this._Execute(str);
		}

		public void Paste()
		{
			this._Execute("Paste");
		}

		public void PrintFolio(int iParam1, int iParam2, string sParam3)
		{
			this._Execute("PrintFolio", iParam1.ToString(), iParam2.ToString(), sParam3);
		}

		public IntPtr PrintFolioEx(int iParam1, int iParam2, IntPtr ptrParam3, int iParam4, int iParam5)
		{
			string[] str = new string[] { "PrintFolioEx", iParam1.ToString(), iParam2.ToString(), ptrParam3.ToString(), iParam4.ToString(), iParam5.ToString() };
			return (IntPtr)this._Execute(str);
		}

		public bool PrintGetFolio(int iParam1, int iParam2)
		{
			bool flag = this._Execute("PrintGetFolio", iParam1.ToString(), iParam2.ToString()) != 0;
			return flag;
		}

		public int PrintGetNbHorzFolio()
		{
			return this._Execute("PrintGetNbHorzFolio");
		}

		public int PrintGetNbSymbols(int iParam1, int iParam2)
		{
			int num = this._Execute("PrintGetNbSymbols", iParam1.ToString(), iParam2.ToString());
			return num;
		}

		public int PrintGetNbVertFolio()
		{
			return this._Execute("PrintGetNbVertFolio");
		}

		public string PrintGetSymbols(int iParam1, int iParam2)
		{
			string str = this._ExecuteString("PrintGetSymbols", iParam1.ToString(), iParam2.ToString());
			return str;
		}

		public void PrintSetProperty(int iParam1, int ptrParam2)
		{
			this._Execute("PrintSetProperty", iParam1.ToString(), ptrParam2.ToString());
		}

		public void PromptInstance(bool bParam1)
		{
			this._Execute("PromptInstance", bParam1.ToString());
		}

		public void PromptVarname(bool bParam1)
		{
			this._Execute("PromptVarname", bParam1.ToString());
		}

		public void Redo()
		{
			this._Execute("Redo");
		}

		public void Refresh()
		{
			this._Execute("Refresh");
		}

		public void ReloadBitmap()
		{
			this._Execute("ReloadBitmap");
		}

		public void RemoveAllBkp()
		{
			this._Execute("RemoveAllBkp");
		}

		public void RemoveBkp(string sParam1)
		{
			this._Execute("RemoveBkp", sParam1);
		}

		public bool RemoveCol(int iParam1)
		{
			bool flag = this._Execute("RemoveCol", iParam1.ToString()) != 0;
			return flag;
		}

		public void RemoveComment()
		{
			this._Execute("RemoveComment");
		}

		public bool RemoveProject(uint ui32Param1)
		{
			bool flag = this._Execute("RemoveProject", ui32Param1.ToString()) != 0;
			return flag;
		}

		public bool RenameCol(int iParam1)
		{
			bool flag = this._Execute("RenameCol", iParam1.ToString()) != 0;
			return flag;
		}

		public void Renumber()
		{
			this._Execute("Renumber");
		}

		public void ResetStepPos()
		{
			this._Execute("ResetStepPos");
		}

		public void ResetValues()
		{
			this._Execute("ResetValues");
		}

		public void Resize(int iParam1, int iParam2)
		{
			this._Execute("Resize", iParam1.ToString(), iParam2.ToString());
		}

		public void RestoreSel()
		{
			this._Execute("RestoreSel");
		}

		public void RotateCorners(int iParam1)
		{
			this._Execute("RotateCorners", iParam1.ToString());
		}

		public void Save(string sParam1)
		{
			this._Execute("Save", sParam1);
		}

		public void SaveExpand(string sParam1)
		{
			this._Execute("SaveExpand", sParam1);
		}

		public void SaveSel()
		{
			this._Execute("SaveSel");
		}

		public bool SaveValues(int iParam1, string sParam2)
		{
			bool flag = this._Execute("SaveValues", iParam1.ToString(), sParam2) != 0;
			return flag;
		}

		public int SearchIn(IntPtr ptrParam1)
		{
			return this._Execute("SearchIn", ptrParam1.ToString());
		}

		public void SelectAll()
		{
			this._Execute("SelectAll");
		}

		public void SelectItem(uint ui32Param1, bool bParam2)
		{
			this._Execute("SelectItem", ui32Param1.ToString(), bParam2.ToString());
		}

		public void SelectItems(int iParam1, IntPtr ptrParam2, bool bParam3)
		{
			this._Execute("SelectItems", iParam1.ToString(), ptrParam2.ToString(), bParam3.ToString());
		}

		public bool SendReceipe(int iParam1)
		{
			bool flag = this._Execute("SendReceipe", iParam1.ToString()) != 0;
			return flag;
		}

		public void SetActivation(bool bParam1)
		{
			this._Execute("SetActivation", bParam1.ToString());
		}

		public void SetAutoConnectVariable(bool bParam1)
		{
			this._Execute("SetAutoConnectVariable", bParam1.ToString());
		}

		public void SetAutoEdit(uint ui32Param2)
		{
			this._Execute("SetAutoEdit", ui32Param2.ToString());
		}

		public void SetBGColor(int ptrParam1, int ptrParam2)
		{
			this._Execute("SetBGColor", ptrParam1.ToString(), ptrParam2.ToString());
		}

		public void SetBkp(string sParam1)
		{
			this._Execute("SetBkp", sParam1);
		}

		public bool SetBkpEx(string sParam1, uint ui32Param2)
		{
			bool flag = this._Execute("SetBkpEx", sParam1, ui32Param2.ToString()) != 0;
			return flag;
		}

		public bool SetCallback(int iParam1, IntPtr ptrParam2, IntPtr ptrParam3)
		{
			bool flag = this._Execute("SetCallback", iParam1.ToString(), ptrParam2.ToString(), ptrParam3.ToString()) != 0;
			return flag;
		}

		public void SetCellHeight(int iParam1)
		{
			this._Execute("SetCellHeight", iParam1.ToString());
		}

		public void SetCellWidth(int iParam1)
		{
			this._Execute("SetCellWidth", iParam1.ToString());
		}

		public void SetChildOffset(int iParam1)
		{
			this._Execute("SetChildOffset", iParam1.ToString());
		}

		public void SetColItemParam(int iParam1, uint ui32Param2)
		{
			this._Execute("SetColItemParam", iParam1.ToString(), ui32Param2.ToString());
		}

		public void SetColItemText(int iParam1, string sParam2)
		{
			this._Execute("SetColItemText", iParam1.ToString(), sParam2);
		}

		public void SetColWidth(int iParam1, int iParam2)
		{
			this._Execute("SetColWidth", iParam1.ToString(), iParam2.ToString());
		}

		public void SetCommentNote(int iParam1, string sParam2)
		{
			this._Execute("SetCommentNote", iParam1.ToString(), sParam2);
		}

		public void SetContents(int iParam1, string sParam2)
		{
			this._Execute("SetContents", iParam1.ToString(), sParam2);
		}

		public void SetCurrentCol(int iParam1)
		{
			this._Execute("SetCurrentCol", iParam1.ToString());
		}

		public void SetCurrentFB(string sParam1, uint ui32Param2, int iParam3, int iParam4, bool bParam5, bool bParam6)
		{
			string[] strArrays = new string[] { "SetCurrentFB", sParam1, ui32Param2.ToString(), iParam3.ToString(), iParam4.ToString(), bParam5.ToString(), bParam6.ToString() };
			this._Execute(strArrays);
		}

		public void SetDebug(bool bParam1)
		{
			this._Execute("SetDebug", bParam1.ToString());
		}

		public void SetDisplay(ushort ui16Param1)
		{
			this._Execute("SetDisplay", ui16Param1.ToString());
		}

		public void SetEditMode(bool bParam1)
		{
			this._Execute("SetEditMode", bParam1.ToString());
		}

		public void SetEnable(bool bParam1)
		{
			this._Execute("SetEnable", bParam1.ToString());
		}

		public void SetErrorMode(bool bParam1)
		{
			this._Execute("SetErrorMode", bParam1.ToString());
		}

		public void SetFB(string sParam1, uint ui32Param2, int iParam3, int iParam4, bool bParam5, bool bParam6)
		{
			string[] strArrays = new string[] { "SetFB", sParam1, ui32Param2.ToString(), iParam3.ToString(), iParam4.ToString(), bParam5.ToString(), bParam6.ToString() };
			this._Execute(strArrays);
		}

		public void SetFilter(uint ui32Param1)
		{
			this._Execute("SetFilter", ui32Param1.ToString());
		}

		public void SetFirstNetworkTag(int iParam1)
		{
			this._Execute("SetFirstNetworkTag", iParam1.ToString());
		}

		public void SetGraphicProperties(string sParam1, string sParam2)
		{
			this._Execute("SetGraphicProperties", sParam1, sParam2);
		}

		public void SetGrid(bool bParam1)
		{
			this._Execute("SetGrid", bParam1.ToString());
		}

		public void SetHeightSummaryPrint(int iParam1)
		{
			this._Execute("SetHeightSummaryPrint", iParam1.ToString());
		}

		public void SetHorzVarSize(int iParam1)
		{
			this._Execute("SetHorzVarSize", iParam1.ToString());
		}

		public void SetID(uint ui32Param1)
		{
			this._Execute("SetID", ui32Param1.ToString());
		}

		public void SetInstance(string sParam1)
		{
			this._Execute("SetInstance", sParam1);
		}

		public bool SetItemBold(IntPtr ptrParam1, bool bParam2)
		{
			bool flag = this._Execute("SetItemBold", ptrParam1.ToString(), bParam2.ToString()) != 0;
			return flag;
		}

		public bool SetItemColor(IntPtr ptrParam1, int ptrParam2)
		{
			bool flag = this._Execute("SetItemColor", ptrParam1.ToString(), ptrParam2.ToString()) != 0;
			return flag;
		}

		public void SetItemData(IntPtr ptrParam1, uint ui32Param2)
		{
			this._Execute("SetItemData", ptrParam1.ToString(), ui32Param2.ToString());
		}

		public bool SetItemText(IntPtr ptrParam1, int iParam2, string sParam3)
		{
			bool flag = this._Execute("SetItemText", ptrParam1.ToString(), iParam2.ToString(), sParam3) != 0;
			return flag;
		}

		public void SetLinePerComment(int iParam1)
		{
			this._Execute("SetLinePerComment", iParam1.ToString());
		}

		public void SetLocalSel(uint ui32Param1)
		{
			this._Execute("SetLocalSel", ui32Param1.ToString());
		}

		public void SetMacroNote(int iParam1, string sParam2)
		{
			this._Execute("SetMacroNote", iParam1.ToString(), sParam2);
		}

		public void SetModified(bool bParam1)
		{
			this._Execute("SetModified", bParam1.ToString());
		}

		public void SetNoteDisplay(bool bParam1)
		{
			this._Execute("SetNoteDisplay", bParam1.ToString());
		}

		public void SetPageWidth(int iParam1)
		{
			this._Execute("SetPageWidth", iParam1.ToString());
		}

		public void SetParent(string sParam1)
		{
			this._Execute("SetParent", sParam1);
		}

		public void SetPrinter(int iParam1, int iParam2, int iParam3, int iParam4)
		{
			string[] str = new string[] { "SetPrinter", iParam1.ToString(), iParam2.ToString(), iParam3.ToString(), iParam4.ToString() };
			this._Execute(str);
		}

		public void SetPrinterSetup(IntPtr ptrParam1)
		{
			this._Execute("SetPrinterSetup", ptrParam1.ToString());
		}

		public void SetProgramName(string sParam1)
		{
			this._Execute("SetProgramName", sParam1);
		}

		public bool SetProjectPath(string sParam1)
		{
			return this._Execute("SetProjectPath", sParam1) != 0;
		}

		public int SetPrompt(bool bParam1, string sParam2)
		{
			return this._Execute("SetPrompt", bParam1.ToString(), sParam2);
		}

		public bool SetPropDesc(int iParam1, string sParam2)
		{
			bool flag = this._Execute("SetPropDesc", iParam1.ToString(), sParam2) != 0;
			return flag;
		}

		public bool SetPropEnum(int iParam1, string sParam2)
		{
			bool flag = this._Execute("SetPropEnum", iParam1.ToString(), sParam2) != 0;
			return flag;
		}

		public bool SetPropEnumEdit(int iParam1, string sParam2)
		{
			bool flag = this._Execute("SetPropEnumEdit", iParam1.ToString(), sParam2) != 0;
			return flag;
		}

		public void SetProperties(string sParam1)
		{
			this._Execute("SetProperties", sParam1);
		}

		public bool SetPropHeader(int iParam1, string sParam2)
		{
			bool flag = this._Execute("SetPropHeader", iParam1.ToString(), sParam2) != 0;
			return flag;
		}

		public bool SetPropMaxInt(int iParam1, int iParam2)
		{
			bool flag = this._Execute("SetPropMaxInt", iParam1.ToString(), iParam2.ToString()) != 0;
			return flag;
		}

		public bool SetPropMaxLen(int iParam1, int iParam2)
		{
			bool flag = this._Execute("SetPropMaxLen", iParam1.ToString(), iParam2.ToString()) != 0;
			return flag;
		}

		public bool SetPropMinInt(int iParam1, int iParam2)
		{
			bool flag = this._Execute("SetPropMinInt", iParam1.ToString(), iParam2.ToString()) != 0;
			return flag;
		}

		public bool SetPropName(int iParam1, string sParam2)
		{
			bool flag = this._Execute("SetPropName", iParam1.ToString(), sParam2) != 0;
			return flag;
		}

		public bool SetPropReadOnly(int iParam1, bool bParam2)
		{
			bool flag = this._Execute("SetPropReadOnly", iParam1.ToString(), bParam2.ToString()) != 0;
			return flag;
		}

		public void SetPropSyntax(string sParam1)
		{
			this._Execute("SetPropSyntax", sParam1);
		}

		public bool SetPropType(int iParam1, int iParam2)
		{
			bool flag = this._Execute("SetPropType", iParam1.ToString(), iParam2.ToString()) != 0;
			return flag;
		}

		public bool SetPropValue(int iParam1, string sParam2)
		{
			bool flag = this._Execute("SetPropValue", iParam1.ToString(), sParam2) != 0;
			return flag;
		}

		public void SetReadOnly(bool bParam1)
		{
			this._Execute("SetReadOnly", bParam1.ToString());
		}

		public void SetRedraw(bool bParam1)
		{
			this._Execute("SetRedraw", bParam1.ToString());
		}

		public void SetRules(string sParam1)
		{
			this._Execute("SetRules", sParam1);
		}

		public void SetScreenID(uint ui32Param1)
		{
			this._Execute("SetScreenID", ui32Param1.ToString());
		}

		public void SetSerialCol(string sParam1)
		{
			this._Execute("SetSerialCol", sParam1);
		}

		public void SetSerialExpand(string sParam1)
		{
			this._Execute("SetSerialExpand", sParam1);
		}

		public void SetSFCSettings(IntPtr ptrParam1)
		{
			this._Execute("SetSFCSettings", ptrParam1.ToString());
		}

		public void SetSFCSettings2(uint ptrParam1)
		{
			this._Execute("SetSFCSettings2", ptrParam1.ToString());
		}

		public void SetSortCol(int iParam1, bool bParam2)
		{
			this._Execute("SetSortCol", iParam1.ToString(), bParam2.ToString());
		}

		public void SetStepCode_Def(int iParam1, string sParam2)
		{
			this._Execute("SetStepCode_Def", iParam1.ToString(), sParam2);
		}

		public void SetStepCode_N(int iParam1, string sParam2)
		{
			this._Execute("SetStepCode_N", iParam1.ToString(), sParam2);
		}

		public void SetStepCode_P0(int iParam1, string sParam2)
		{
			this._Execute("SetStepCode_P0", iParam1.ToString(), sParam2);
		}

		public void SetStepCode_P1(int iParam1, string sParam2)
		{
			this._Execute("SetStepCode_P1", iParam1.ToString(), sParam2);
		}

		public void SetStepNote(int iParam1, string sParam2)
		{
			this._Execute("SetStepNote", iParam1.ToString(), sParam2);
		}

		public void SetStepPos(string sParam1)
		{
			this._Execute("SetStepPos", sParam1);
		}

		public void SetStyle(uint ui32Param1)
		{
			this._Execute("SetStyle", ui32Param1.ToString());
		}

		public void SetSyntaxColoring(string sParam1)
		{
			this._Execute("SetSyntaxColoring", sParam1);
		}

		public void SetTab(int iParam1)
		{
			this._Execute("SetTab", iParam1.ToString());
		}

		public void SetText(string sParam1)
		{
			this._Execute("SetText", sParam1);
		}

		public void SetTimer()
		{
			this._Execute("SetTimer");
		}

		public void SetTooltipInfos(bool bParam1, IntPtr ptrParam2)
		{
			this._Execute("SetTooltipInfos", bParam1.ToString(), ptrParam2.ToString());
		}

		public void SetTracePoint(string sParam1)
		{
			this._Execute("SetTracePoint", sParam1);
		}

		public void SetTransCode(int iParam1, string sParam2)
		{
			this._Execute("SetTransCode", iParam1.ToString(), sParam2);
		}

		public void SetTransNote(int iParam1, string sParam2)
		{
			this._Execute("SetTransNote", iParam1.ToString(), sParam2);
		}

		public bool SetTreeIcon(IntPtr ptrParam1, int iParam2)
		{
			bool flag = this._Execute("SetTreeIcon", ptrParam1.ToString(), iParam2.ToString()) != 0;
			return flag;
		}

		public void SetUndef(bool bParam1)
		{
			this._Execute("SetUndef", bParam1.ToString());
		}

		public void SetUsed(string sParam1)
		{
			this._Execute("SetUsed", sParam1);
		}

		public void SetUserID(string sParam1)
		{
			this._Execute("SetUserID", sParam1);
		}

		public void SetValueInText(int iParam1)
		{
			this._Execute("SetValueInText", iParam1.ToString());
		}

		public void SetVertVarSize(int iParam1)
		{
			this._Execute("SetVertVarSize", iParam1.ToString());
		}

		public void SetWidthSummaryPrint(int iParam1)
		{
			this._Execute("SetWidthSummaryPrint", iParam1.ToString());
		}

		public void SetZoom(int iParam1, int iParam2)
		{
			this._Execute("SetZoom", iParam1.ToString(), iParam2.ToString());
		}

		public void Sort(int iParam1)
		{
			this._Execute("Sort", iParam1.ToString());
		}

		public void SortTree(ushort ui16Param1, bool bParam2)
		{
			this._Execute("SortTree", ui16Param1.ToString(), bParam2.ToString());
		}

		public void StartRecordSampling(string sParam1)
		{
			this._Execute("StartRecordSampling", sParam1);
		}

		public bool StartSampling()
		{
			return this._Execute("StartSampling") != 0;
		}

		public void StopRecordSampling()
		{
			this._Execute("StopRecordSampling");
		}

		public bool StopSampling()
		{
			return this._Execute("StopSampling") != 0;
		}

		public void SummaryPrint(bool bParam1, IntPtr ptrParam2)
		{
			this._Execute("SummaryPrint", bParam1.ToString(), ptrParam2.ToString());
		}

		public void SwapCollapse()
		{
			this._Execute("SwapCollapse");
		}

		public void SwapGlobalRet()
		{
			this._Execute("SwapGlobalRet");
		}

		public void SwapHexDisplay()
		{
			this._Execute("SwapHexDisplay");
		}

		public void SwapItemStyle()
		{
			this._Execute("SwapItemStyle");
		}

		public void SwapStyle()
		{
			this._Execute("SwapStyle");
		}

		public void Uncheck()
		{
			this._Execute("Uncheck");
		}

		public void Undo()
		{
			this._Execute("Undo");
		}

		public void UndoRedoSize(int iParam1)
		{
			this._Execute("UndoRedoSize", iParam1.ToString());
		}

		public void UpdateUDFB()
		{
			this._Execute("UpdateUDFB");
		}

		public void ViewInfo()
		{
			this._Execute("ViewInfo");
		}

		public void WrapRungs(bool bParam1)
		{
			this._Execute("WrapRungs", bParam1.ToString());
		}
	}
}