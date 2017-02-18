using System;
using System.Collections;
using System.IO;  
using System.Xml;
using System.Xml.Serialization;

using VRS.Library.Projet;

namespace VRS.Library.Collections {
	/// <summary>
	/// Collection d'employé.
	/// </summary>
    public class FavorisCollection : CollectionWithEvents {
        public Favoris Add(Favoris value) {
			base.List.Add(value as object);

			return value;
		}

        public void AddRange(Favoris[] values) {
            foreach (Favoris item in values)
				Add(item);
		}

        public void Remove(Favoris value) {
			base.List.Remove(value as object);
		}

        public void Insert(int index, Favoris value) {
			base.List.Insert(index, value as object);
		}

        public bool Contains(Favoris value) {
            foreach (Favoris s in base.List)
				if (value.Equals(s))
					return true;

			return false;
		}

        public bool Contains(FavorisCollection values) {
            foreach (Favoris c in values) {
				if (Contains(c))
					return true;
			}

			return false;
		}

        public Favoris this[int index] {
            get { return (base.List[index] as Favoris); }
		}

        public int IndexOf(Favoris value) {
			return base.List.IndexOf(value);
		}
	}
}
