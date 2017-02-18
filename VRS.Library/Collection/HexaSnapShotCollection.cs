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
    public class HexaSnapShotCollection : CollectionWithEvents {
        public HexaSnapShot Add(HexaSnapShot value) {
			base.List.Add(value as object);

			return value;
		}

        public void AddRange(HexaSnapShot[] values) {
            foreach (HexaSnapShot item in values)
				Add(item);
		}

        public void Remove(HexaSnapShot value) {
			base.List.Remove(value as object);
		}

        public void Insert(int index, HexaSnapShot value) {
			base.List.Insert(index, value as object);
		}

        public bool Contains(HexaSnapShot value) {
            foreach (HexaSnapShot s in base.List)
				if (value.Equals(s))
					return true;

			return false;
		}

        public bool Contains(HexaSnapShotCollection values) {
            foreach (HexaSnapShot c in values) {
				if (Contains(c))
					return true;
			}

			return false;
		}

        public HexaSnapShot this[int index] {
            get { return (base.List[index] as HexaSnapShot); }
		}

        public int IndexOf(HexaSnapShot value) {
			return base.List.IndexOf(value);
		}
	}
}
