using System;
using System.ComponentModel;

namespace VRS.Library.Projet {
	/// <summary>
	/// Description résumée de Folder.
	/// TODO: Finir cette classe
	/// </summary>
	public sealed class Folder {
		private string _name = "";

		public Folder() {

		}

		[Category("Divers")]
		public string Name{
			get{
				return this._name; 
			}
			set{
				this._name = value;
			}
		}
	}
}
