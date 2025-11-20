using System;
using System.Runtime.InteropServices;

namespace WinAPI
{
	public class Ole
	{
		public Ole()
		{
		}

		[DllImport("ole32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int OleInitialize(IntPtr pvReserved);
	}
}