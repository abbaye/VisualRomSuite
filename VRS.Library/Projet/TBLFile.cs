using System;

namespace VRS.Library.Projet {
	/// <summary>
	/// Fichier Projet TBL
	/// </summary>
	public sealed class TBLFile {
		private string _RelativePath;
		private string _Description; 
		private string _Name;
		private bool _Default;

		/// <summary>
		/// cle pour retrouver l'objet dans le projet
		/// </summary>
		public string Key;		

		public TBLFile() {

		}

		public string RelativePath{
			get{
				return this._RelativePath; 
			}
			set{
				this._RelativePath = value;
			}
		}

		public string Description{
			get{
				return this._Description; 
			}
			set{
				this._Description = value;
			}
		}

		public string Name{
			get{
				return this._Name; 
			}
			set{
				this._Name = value;
			}
		}

		public bool Default{
			get{
				return this._Default; 
			}
			set{
				this._Default = value;
			}
		}
	}
}
