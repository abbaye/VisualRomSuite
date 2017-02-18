using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VRS.Library.Win32 {
	/// <summary>
	/// Affiche la boite : A propos de ... de windows
	/// </summary>
	public class AboutBox {
		[DllImport("shell32.dll")]
		private static extern long ShellAbout(int hWnd, string szApp, string szOtherStuff, int hIcon);

		private string _Title = Application.ProductName;
		private int   _FormHandle = 0;
		private int   _IconHandle = 0;
		private string _Version   = Application.ProductVersion;

		public AboutBox(int FormHandle, int IconHandle, string Title, string Version) {
			this._FormHandle = FormHandle;
			this._IconHandle = IconHandle;
            this._Title		 = Title;
			this._Version	 = Version;
		}

		public AboutBox(int FormHandle, int IconHandle) {
			this._FormHandle = FormHandle;
			this._IconHandle = IconHandle;
		}

		public long Show(){
			return ShellAbout(this._FormHandle, this._Title + "#" + Environment.GetEnvironmentVariable("OS") , this._Title + " v." + this._Version, this._IconHandle);
		}
	}
}
