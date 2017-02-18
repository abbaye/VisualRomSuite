using System;
using System.Windows.Forms.Design;

namespace VRS.Library.IO {

	public class OpenFolderDialog : System.Windows.Forms.Design.FolderNameEditor {
		//référence à System.Design.dll et donc à System.Drawing.dll
		FolderBrowser myFolderBrowser; 

		private string _Description = "Directory";

		public OpenFolderDialog(string Message) {
			this.Description = Message; 
			myFolderBrowser = new FolderBrowser();   
		} 

		public string GetFolder() {
			myFolderBrowser.Description = this._Description; 
			myFolderBrowser.ShowDialog();

			return myFolderBrowser.DirectoryPath;

		}

		public string Description{
			get{
				return this._Description; 
			}
			set{
				this._Description = value;
			}
		}

	}

}