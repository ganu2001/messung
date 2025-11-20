using System;
using System.Runtime.InteropServices;

namespace W5
{
	public class EditLD : Edit
	{
		public EditLD()
		{
		}

		protected override int _Execute(string[] argv)
		{
			int num = EditLD.ExecuteCommand(this.m_hWnd, (int)argv.Length, argv);
			return num;
		}

		protected override IntPtr _GetClassName()
		{
			return EditLD.GetClassName();
		}

		[DllImport("W5EditLD.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5ExecuteCommand", ExactSpelling=false)]
		public static extern int ExecuteCommand(IntPtr hWnd, int argc, string[] argv);

		[DllImport("W5EditLD.dll", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.None, EntryPoint="K5GetClassName", ExactSpelling=false)]
		public static extern IntPtr GetClassName();
	}
}