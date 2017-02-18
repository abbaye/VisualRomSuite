using System;
using System.Windows.Forms;
using VRS;
using VRS.IDE.Forms;

namespace VRS.IDE {
	/// <summary>
	/// Classe principal du projet Visual Rom Suite
	/// </summary>
	public class MainClasse {
		/// <summary>
		/// Point d'entrée principal de l'application Visual Rom Suite.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new FormMain()); //Principal			
			//Application.Run(new frmTest());  //Form de Teste
		}
	}
}
