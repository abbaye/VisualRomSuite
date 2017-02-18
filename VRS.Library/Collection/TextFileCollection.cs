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
    public class TextFileCollection : CollectionWithEvents {
        public TextFile Add(TextFile value) {
			base.List.Add(value as object);

			return value;
		}

        public void AddRange(TextFile[] values) {
            foreach (TextFile item in values)
				Add(item);
		}

        public void Remove(TextFile value) {
			base.List.Remove(value as object);
		}

        public void Insert(int index, TextFile value) {
			base.List.Insert(index, value as object);
		}

        public bool Contains(TextFile value) {
            foreach (TextFile s in base.List)
				if (value.Equals(s))
					return true;

			return false;
		}

        public bool Contains(TextFileCollection values) {
            foreach (TextFile c in values) {
				if (Contains(c))
					return true;
			}

			return false;
		}

        public TextFile this[int index] {
            get { return (base.List[index] as TextFile); }
		}

        public int IndexOf(TextFile value) {
			return base.List.IndexOf(value);
		}
	}
}
