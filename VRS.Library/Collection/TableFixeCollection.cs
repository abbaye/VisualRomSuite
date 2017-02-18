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
    public class TableFixeCollection : CollectionWithEvents {
        public TableFixeFile Add(TableFixeFile value) {
			base.List.Add(value as object);

			return value;
		}

        public void AddRange(TableFixeFile[] values) {
            foreach (TableFixeFile item in values)
				Add(item);
		}

        public void Remove(TableFixeFile value) {
			base.List.Remove(value as object);
		}

        public void Insert(int index, TableFixeFile value) {
			base.List.Insert(index, value as object);
		}

        public bool Contains(TableFixeFile value) {
            foreach (TableFixeFile s in base.List)
				if (value.Equals(s))
					return true;

			return false;
		}

        public bool Contains(TableFixeCollection values) {
            foreach (TableFixeFile c in values) {
				if (Contains(c))
					return true;
			}

			return false;
		}

        public TableFixeFile this[int index] {
            get { return (base.List[index] as TableFixeFile); }
		}

        public int IndexOf(TableFixeFile value) {
			return base.List.IndexOf(value);
		}
	}
}
