using System;
using System.Diagnostics;
using System.Windows.Forms; 
using System.IO;

using VRS.IDE;
using VRS.IDE.Resource;

namespace VRS {
	/// <summary>
	/// Options et configuration de l'application et Variable global
	/// </summary>
	public sealed class App {
		/// <summary>
		/// Chemin vers les resources
		/// </summary>
		public const string DefaultRessourcePath = "VRS.IDE.Resource.Strings";

        /// <summary>
        /// Language du logiciel
        /// </summary>
		public static Culture AppCulture = Culture.Francais; 

		/// <summary>
		/// Nom de l'application
		/// </summary>
		public static string Name = new Resources(App.AppCulture).GetString("AppName");

		/// <summary>
		/// Information sur la version de l'application
		/// </summary>
		public static FileVersionInfo VersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath); 

		/// <summary>
		/// Chemin envers l'executable de l'emulateur
		/// </summary>
		public static string EmulatorPath = Application.StartupPath + Path.DirectorySeparatorChar + @"bin\snes9x\snes9x.exe";

	}
}
