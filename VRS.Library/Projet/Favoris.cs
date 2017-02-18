using System;

namespace VRS.Library.Projet {
	/// <summary>
	/// Represente une adresse favorite dans s'une ROM
	/// </summary>
	public sealed class Favoris {
		public string Position;
		public string Name;
		public string File;
		public string Key;

		public Favoris(){

		}

		public Favoris(string Name, string Position, string File, string key) {
			this.Position = Position;
			this.Name = Name;
			this.File = File;
            this.Key = key;
		}
	}
}
