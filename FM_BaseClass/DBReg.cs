using System;
using System.Runtime.InteropServices;
using System.Text;

namespace K5
{
	public class DBReg
	{
		public const int K5DBREGOBJ_BLOCK = 1;

		public const int K5DBREGOBJ_IO = 2;

		public const int K5DBREGOBJ_PROF = 3;

		public const int K5DBREGOBJ_TYPE = 4;

		public const int K5DBREGOBJ_IOD = 5;

		public const uint K5DBREG_STDOP = 1;

		public const uint K5DBREG_STDFUNC = 2;

		public const uint K5DBREG_STDFB = 4;

		public const uint K5DBREG_CFUNC = 32;

		public const uint K5DBREG_CFB = 64;

		public const uint K5DBREG_UDFB = 1024;

		public const uint K5DBREG_ALL = 4095;

		public const uint K5DBREG_FUNC = 34;

		public const uint K5DBREG_FB = 1092;

		public const int K5DBREG_BOOL = 1;

		public const int K5DBREG_DINT = 2;

		public const int K5DBREG_REAL = 3;

		public const int K5DBREG_LREAL = 4;

		public const int K5DBREG_BYTE = 5;

		public const int K5DBREG_WORD = 6;

		public const int K5DBREG_LINT = 7;

		public const int K5DBREG_TIME = 8;

		public const int K5DBREG_String = 9;

		public const int K5DBREG_ANY = 10;

		public const int K5DBREG_USINT = 11;

		public const int K5DBREG_UINT = 12;

		public const int K5DBREG_UDINT = 13;

		public const int K5DBREG_ULINT = 14;

		public const int K5DBREG_COMPLEX = 15;

		public const int K5DBREGCOMM_SINGLELINE = 1;

		public const int K5DBREGCOMM_MULTILINE = 2;

		public DBReg()
		{
		}

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBREG_BlockCanBeExtended", ExactSpelling=false)]
		public static extern bool BlockCanBeExtended(string szBlockName);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_FindBlock", ExactSpelling=false)]
		public static extern uint FindBlock(string szName);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetBlockDesc", ExactSpelling=false)]
		public static extern IntPtr GetBlockDesc(uint dwBlock, ref uint dwKind, ref uint dwLibNo, ref uint dwNbInput, ref uint dwNbOutput);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetBlockInput", ExactSpelling=false)]
		public static extern IntPtr GetBlockInput(uint dwBlock, uint dwPin, ref uint dwType);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetBlockLibName", ExactSpelling=false)]
		public static extern IntPtr GetBlockLibName(uint dwType);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetBlockOutput", ExactSpelling=false)]
		public static extern IntPtr GetBlockOutput(uint dwBlock, uint dwPin, ref uint dwType);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetBlockPinTypeName", ExactSpelling=false)]
		public static extern IntPtr GetBlockPinTypeName(uint dwType);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetBlocks", ExactSpelling=false)]
		public static extern uint GetBlocks(uint dwMask, uint[] hBlocks);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetComment", ExactSpelling=false)]
		public static extern uint GetComment(uint dwCommType, uint dwObjectType, string szObjectName, StringBuilder szBuffer, uint dwBufLen);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetCommentLength", ExactSpelling=false)]
		public static extern uint GetCommentLength(uint dwCommType, uint dwObjectType, string szObjectName);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetNbBlock", ExactSpelling=false)]
		public static extern uint GetNbBlock(uint dwMask);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetTypeName", ExactSpelling=false)]
		public static extern IntPtr GetTypeName(uint dwType);

		[DllImport("K5DBReg.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5DBReg_GetVersion", ExactSpelling=false)]
		public static extern int GetVersion();
	}
}