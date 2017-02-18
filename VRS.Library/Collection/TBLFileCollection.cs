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
    public class TBLFileCollection : CollectionWithEvents {
        public TBLFile Add(TBLFile value) {
			base.List.Add(value as object);

			return value;
		}

        public void AddRange(TBLFile[] values) {
            foreach (TBLFile item in values)
				Add(item);
		}

        public void Remove(TBLFile value) {
			base.List.Remove(value as object);
		}

        public void Insert(int index, TBLFile value) {
			base.List.Insert(index, value as object);
		}

        public bool Contains(TBLFile value) {
            foreach (TBLFile s in base.List)
				if (value.Equals(s))
					return true;

			return false;
		}

        public bool Contains(TBLFileCollection values) {
            foreach (TBLFile c in values) {
				if (Contains(c))
					return true;
			}

			return false;
		}

        public TBLFile this[int index] {
            get { return (base.List[index] as TBLFile); }
		}

        public int IndexOf(TBLFile value) {
			return base.List.IndexOf(value);
		}
	}
}
