using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using VRS.Library.Table.TBL;

namespace VRS.Library.UserControl.TableEditor {
	/// <summary>
	/// Description résumée de EditeurTBL.
	/// </summary>
	public class EditeurTBL : System.Windows.Forms.UserControl {

		/// <summary>
		/// chemin du fichier chargé dans la TBL
		/// </summary>
		private string _filename = "";
		private System.Windows.Forms.Panel ItemPanel;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EditeurTBL() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			//Creation de la liste d'item
			///this._Item = new ArrayList();

			//Creation d'une Bordure
			//VRS.Library.Win32.StaticBorder.ThinBorder(picHeader.Handle.ToInt32(), true);
		}

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			this.ItemPanel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// ItemPanel
			// 
			this.ItemPanel.AutoScroll = true;
			this.ItemPanel.BackColor = System.Drawing.SystemColors.Window;
			this.ItemPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemPanel.Location = new System.Drawing.Point(0, 0);
			this.ItemPanel.Name = "ItemPanel";
			this.ItemPanel.Size = new System.Drawing.Size(416, 272);
			this.ItemPanel.TabIndex = 2;
			// 
			// EditeurTBL
			// 
			this.Controls.Add(this.ItemPanel);
			this.Name = "EditeurTBL";
			this.Size = new System.Drawing.Size(416, 272);
			this.ResumeLayout(false);

		}
		#endregion

		public void LoadFile(string filename){
			//Creation l'objet TBL
			TBLStream myTBL = new TBLStream();
			
			//Charger la table
			if (myTBL.Load(filename)){
				//Vide la liste
				//lvTable.Items.Clear();

				this._filename = filename; 

				//this._FileName = FileName;
				this.ItemPanel.SuspendLayout();
				//remplir la list
				TBLEntry[] entry = new TBLEntry[myTBL.Length];
				int j =0;
				//for (int i=0; i< myTBL.Length; i++ ){
				for (int i=myTBL.Length -1; i> -1 ; i-- ){
					entry[i] = new TBLEntry();
			
					//Dock TOP pour un auto resize
					entry[i].Dock = DockStyle.Top; 
			
					//Remplir le control
					entry[i].Entry = myTBL[j].Entry;
					entry[i].Value = myTBL[j].Value;

					entry[i].Type = myTBL[j].Type;

					//Creation du tag pour la recherche
					entry[i].Tag = "entry" + this.ItemPanel.Controls.Count;  

					j++;
				}
				
				this.ItemPanel.Controls.AddRange(entry);
				
				this.ItemPanel.ResumeLayout();
				AddItem("", "");

				this.OnResize(EventArgs.Empty);
			}	
		}

		public void AddItem(string Entry, string Value){
			TBLEntry entry = new TBLEntry();
			
			//Dock TOP pour un auto resize
			entry.Dock = DockStyle.Top; 
			
			//Remplir le control
			entry.Entry = Entry;
			entry.Value = Value;

			//Creation du tag pour la recherche
			entry.Tag = "entry" + this.ItemPanel.Controls.Count;  

			//Ajoute les controls
			this.ItemPanel.Controls.Add(entry);			

			//Ajoute a la fin de la liste
			entry.BringToFront();
		}

		/// <summary>
		/// Enregistre la TBL
		/// </summary>
		/// <param name="filename">Nom du fichier</param>
		public void Save(string filename){
			TBLStream tbl = new TBLStream(filename);
			TBLEntry entry = null;

			for(int i=ItemPanel.Controls.Count; i > 0; i--){
				entry = (TBLEntry)ItemPanel.Controls[i -1];
				tbl.Add(new DTE(entry.Entry,entry.Value));
			}

			tbl.Save();
		}

		public void Save(){
			this.Save(this._filename);
		}

	}
}
