using System;
using System.Runtime.InteropServices;
using System.CodeDom;

namespace VRS.Library.Win32 {
	/// <summary>
	/// Classe contenant des methode static pour la creation de bordure au tours de control
	/// </summary>	
	public sealed class StaticBorder {
		//Constante 
		private const short GWL_EXSTYLE = (-20);
		private const short WS_EX_CLIENTEDGE = 0x200;
		private const int WS_EX_STATICEDGE = 0x20000;
		private const short SWP_FRAMECHANGED = 0x20;
		private const short SWP_NOMOVE = 0x2;
		private const short SWP_NOOWNERZORDER = 0x200;
		private const short SWP_NOSIZE = 0x1;
		private const short SWP_NOZORDER = 0x4;

		//Fonction Externe
		[DllImport("user32.dll")]
		private static extern int SetWindowLong(int hWnd, int nIndex, int dwNewLong);		
		[DllImport("user32.dll")]
		private static extern int SetWindowPos(int hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);		
		[DllImport("user32.dll")]
		private static extern int GetWindowLong(int hWnd, int nIndex);

		/// <summary>
		/// Cree un bordure static au tour d'un control contenant un Handle
		/// </summary>
		/// <param name="lhWnd">Handle du control</param>
		/// <param name="bState">Creation d'une bordure static (true)</param>		
		public static int ThinBorder(int lhWnd, bool bState){
			//Variable		
			int rtnVal = 0;
		
			//Récupère le style actuel
			rtnVal = GetWindowLong(lhWnd, GWL_EXSTYLE);
		
			//Crée un nouveau style selon bState
			if (!bState)
				//Enlève le look Office 2000
				rtnVal = rtnVal | WS_EX_CLIENTEDGE & ~WS_EX_STATICEDGE;
			else
				//Crée le style Office 2000
				rtnVal = rtnVal | WS_EX_STATICEDGE & ~WS_EX_CLIENTEDGE;
				  
			//Applique la modification
			SetWindowLong(lhWnd, GWL_EXSTYLE, rtnVal);
			SetWindowPos(lhWnd, 0, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOOWNERZORDER | SWP_NOZORDER | SWP_FRAMECHANGED);

			return rtnVal;
		}		
	}
}
