using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace VRSThingy {
	/// <summary>
	/// Description résumée de Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form {
		private VRS.Library.UserControl.HexaFileShow hexaFileShow1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem mnuOpenRom;
		private System.Windows.Forms.MenuItem mnuOpenFile;
		private System.Windows.Forms.MenuItem mnuExit;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1() {
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.hexaFileShow1 = new VRS.Library.UserControl.HexaFileShow();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuOpenRom = new System.Windows.Forms.MenuItem();
			this.mnuOpenFile = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.mnuExit = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// hexaFileShow1
			// 
			this.hexaFileShow1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hexaFileShow1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.hexaFileShow1.Location = new System.Drawing.Point(0, 0);
			this.hexaFileShow1.Name = "hexaFileShow1";
			this.hexaFileShow1.Position = "0";
			this.hexaFileShow1.ShowMTE = false;
			this.hexaFileShow1.Size = new System.Drawing.Size(920, 398);
			this.hexaFileShow1.TabIndex = 33;
			this.hexaFileShow1.UseTBL = false;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuOpenRom,
																					  this.mnuOpenFile,
																					  this.menuItem5,
																					  this.mnuExit});
			this.menuItem1.Text = "Fichier";
			// 
			// mnuOpenRom
			// 
			this.mnuOpenRom.Index = 0;
			this.mnuOpenRom.Text = "Ouvrir un fichier";
			this.mnuOpenRom.Click += new System.EventHandler(this.mnuOpenRom_Click);
			// 
			// mnuOpenFile
			// 
			this.mnuOpenFile.Index = 1;
			this.mnuOpenFile.Text = "Ajouter table (Thingy)";
			this.mnuOpenFile.Click += new System.EventHandler(this.mnuOpenFile_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "-";
			// 
			// mnuExit
			// 
			this.mnuExit.Index = 3;
			this.mnuExit.Text = "Quitter";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(920, 398);
			this.Controls.Add(this.hexaFileShow1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Visual Rom Suite Thingy";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new Form1());
		}

		private void mnuExit_Click(object sender, System.EventArgs e) {
			Application.Exit();
		}

		private void mnuOpenRom_Click(object sender, System.EventArgs e) {
			//Obtenir le fichier a ouvrir
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Title = "Choisir le fichier à ouvrir";
			openFile.Filter = "Tous les Fichiers (*.*)|*.*";
			openFile.CheckFileExists = true;
			openFile.CheckPathExists = true;
			openFile.ShowDialog(this);
			
			string filename = openFile.FileName;
			if (filename != ""){
				hexaFileShow1.LoadFile(filename);
			}
		}

		private void mnuOpenFile_Click(object sender, System.EventArgs e) {
			//Obtenir le fichier a ouvrir
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Title = "Choisir le fichier table (TBL) à ouvrir";
			openFile.Filter = "Fichiers Table (Thingy)|*tbl|Tous les Fichiers (*.*)|*.*";
			openFile.CheckFileExists = true;
			openFile.CheckPathExists = true;
			openFile.ShowDialog(this);
			
			string filename = openFile.FileName;
			if (filename != ""){
				hexaFileShow1.UseTBL = true;
				hexaFileShow1.AddTBL( Path.GetFileNameWithoutExtension(filename), filename);

			}
		}
	}
}
