using System;

namespace VRS.Library.Projet {
	/// <summary>
	/// Represente un fichier HexaSnapShot.
	/// </summary>
	public sealed class HexaSnapShot {

		private string _Name;
		private string _Description;
		private bool   _InsertAtCompil;
		private string _RelativePath;
		private string _StartPosition;

		/// <summary>
		/// Cle permetant de retrouver l'objet dans le projet
		/// </summary>
		public string Key;

		public HexaSnapShot() {

		}

		public string Description{
			get{
				return this._Description; 
			}
			set{
				this._Description = value;
			}
		}

		public string StartPosition{
			get{
				return this._StartPosition; 
			}
			set{
				this._StartPosition = value;
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

		public bool InsertAtCompil{
			get{
				return this._InsertAtCompil; 
			}
			set{
				this._InsertAtCompil = value;
			}
		}

		public string RelativePath{
			get{
				return this._RelativePath; 
			}
			set{
				this._RelativePath = value;
			}
		}
	}
}
