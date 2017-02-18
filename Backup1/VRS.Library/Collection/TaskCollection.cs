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
    public class TaskCollection : CollectionWithEvents {
        public Task Add(Task value) {
			base.List.Add(value as object);

			return value;
		}

        public void AddRange(Task[] values) {
            foreach (Task item in values)
				Add(item);
		}

        public void Remove(Task value) {
			base.List.Remove(value as object);
		}

        public void Insert(int index, Task value) {
			base.List.Insert(index, value as object);
		}

        public bool Contains(Task value) {
            foreach (Task s in base.List)
				if (value.Equals(s))
					return true;

			return false;
		}

        public bool Contains(TaskCollection values) {
            foreach (Task c in values) {
				if (Contains(c))
					return true;
			}

			return false;
		}

        public Task this[int index] {
            get { return (base.List[index] as Task); }
		}

        public int IndexOf(Task value) {
			return base.List.IndexOf(value);
		}
	}
}
