using System;
using System.Runtime.InteropServices;

namespace WinAPI
{
	public class CWnd
	{
		public const uint WS_CHILD = 1073741824;

		public const uint WS_VISIBLE = 268435456;

		public const uint WS_BORDER = 32768;

		public CWnd()
		{
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int width, int height, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
	}
}