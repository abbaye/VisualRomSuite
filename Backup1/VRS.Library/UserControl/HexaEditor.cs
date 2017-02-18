using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace VRS.Library.UserControl {
	/// <summary>
	/// Description résumée de HexaEditor.
	/// </summary>
	public class HexaEditor : System.Windows.Forms.UserControl {
		/// <summary>
		/// Nom du fichier charger
		/// </summary>
        string _FileName = "";
        private AxprjVbHex.AxHexEd axHexaEditor;
        
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HexaEditor() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitForm

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

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HexaEditor));
            this.axHexaEditor = new AxprjVbHex.AxHexEd();
            ((System.ComponentModel.ISupportInitialize)(this.axHexaEditor)).BeginInit();
            this.SuspendLayout();
            // 
            // axHexaEditor
            // 
            this.axHexaEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axHexaEditor.Enabled = true;
            this.axHexaEditor.Location = new System.Drawing.Point(0, 0);
            this.axHexaEditor.Name = "axHexaEditor";
            this.axHexaEditor.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axHexaEditor.OcxState")));
            this.axHexaEditor.Size = new System.Drawing.Size(519, 428);
            this.axHexaEditor.TabIndex = 2;
            // 
            // HexaEditor
            // 
            this.Controls.Add(this.axHexaEditor);
            this.Name = "HexaEditor";
            this.Size = new System.Drawing.Size(519, 428);
            ((System.ComponentModel.ISupportInitialize)(this.axHexaEditor)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Charger un fichier
		/// </summary>
		/// <param name="Filename">Nom du fichier</param>
		public void LoadFile(string Filename){
			if (File.Exists(Filename)){
				FileInfo fi = new FileInfo(Filename);

				axHexaEditor.Load(ref Filename, (int)fi.Length);
				this._FileName = Filename;
			}
		}

		/// <summary>
		/// Enregistrer les modification du fichier
		/// </summary>
		public void Save(){
			if (File.Exists(this._FileName)){
				axHexaEditor.Save();
			}
		}
	}
}
