using System;
using System.Runtime.InteropServices;

namespace VRS.UI
{
	/// <summary>
	/// Summary description for User32.
	/// </summary>
	public class User32
	{
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern int ShowWindow(IntPtr hWnd, short cmdShow);

		public User32()
		{
		}
	}
}
