using System;
using System.ComponentModel;

namespace VRS.Library.Projet {
	/// <summary>
	/// Objet representant une Tâche
	/// </summary>
	public sealed class Task {
		private int			_Line;
		private string		_File;
		private string		_Description;
		private TaskPriority _Priority;
		private bool			_TaskComplete;
		public string		Key;

		public Task() {
			
		}

		public string File{
			get{
				return this._File; 
			}
			set{
				this._File = value;
			}
		}

		public int Line{
			get{
				return this._Line; 
			}
			set{
				this._Line = value;
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

		public TaskPriority Priority{
			get{
				return this._Priority; 
			}
			set{
				this._Priority = value;
			}
		}

		public bool TaskComplete{
			get{
				return this._TaskComplete; 
			}
			set{
				this._TaskComplete = value;
			}
		}
	}
}
