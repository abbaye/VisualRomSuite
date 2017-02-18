using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

using VRS.Library.Projet;
using VRS.UI.Controls;

namespace VRS.Library.UserControl {
	/// <summary>
	/// Description résumée de TBLCombo.
	/// </summary>
	public class TBLCombo : VRS.UI.Controls.WComboBox {
		/// <summary>
		/// Projet a utilisé
		/// </summary>
		private Project _Projet = null;

		/// <summary>
		/// Permet de savoir si le control ce rafraichi automatiquement
		/// </summary>
		private bool _AutoRefresh = true;
        
		private System.Windows.Forms.Timer timerAutoRefresh;
		private System.ComponentModel.IContainer components;

		public TBLCombo() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			//Lecture seul
			this.ReadOnly = true;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TBLCombo));
			this.timerAutoRefresh = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.m_pTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// m_pTextBox
			// 
			this.m_pTextBox.Location = new System.Drawing.Point(3, 5);
			this.m_pTextBox.Size = new System.Drawing.Size(179, 13);
			// 
			// timerAutoRefresh
			// 
			this.timerAutoRefresh.Enabled = true;
			this.timerAutoRefresh.Interval = 2000;
			this.timerAutoRefresh.Tick += new System.EventHandler(this.timerAutoRefresh_Tick);
			// 
			// TBLCombo
			// 
			this.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("$this.ButtonIcon")));
			this.ButtonWidth = 15;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_pTextBox});
			this.DropDownWidth = 200;
			this.Name = "TBLCombo";
			this.Size = new System.Drawing.Size(200, 24);
			this.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			((System.ComponentModel.ISupportInitialize)(this.m_pTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public void AddTBL(string Name, string FileName){
			WComboItem item = new WComboItem(Name, FileName);   

			this.Items.Add(item);
			this.SelectedIndex = this.Items.Count -1;
		}
		
		/// <summary>
		/// Rafraichir la selection
		/// </summary>
		public override void Refresh(){
			if (this._Projet != null){
				this.Items.Clear();
				
				WComboItem item = null;
				TBLFile tbl = null;

				for (int i=0; i< this._Projet.Tables.Count; i++){
					tbl = (TBLFile)this._Projet.Tables[i];
					item = new WComboItem(tbl.Name, 
						this._Projet.ProjectPath + Path.DirectorySeparatorChar + tbl.RelativePath);   
					this.Items.Add(item);

					//Selectionne la TBL par defaut
					if (tbl.Default)
						this.SelectedIndex = this.Items.Count -1; 
				}				
			}else
				this.Items.Clear();
		}

		/// <summary>
		/// Projet a utilisé
		/// </summary>
		[Browsable(false)]
		public Project Projet{
			set{
				this._Projet = value;
				Refresh();
			}
		}

		[
		DefaultValue(true),
		Category("Paramètres")
		]			
		public bool AutoRefresh{
			get{
				return this._AutoRefresh; 
			}
			set{
				this._AutoRefresh = value;
			}
		}

		[
		DefaultValue(2000),
		Category("Paramètres")
		]
		public int RefreshInterval{
			get{
				return this.timerAutoRefresh.Interval; 
			}
			set{
				this.timerAutoRefresh.Interval = value;
			}
		}

		private void timerAutoRefresh_Tick(object sender, System.EventArgs e) {			
			if (this._Projet !=  null){
				if (this._AutoRefresh){
					if (this._Projet.Tables.Count < this.Items.Count ||
						this._Projet.Tables.Count > this.Items.Count)
						Refresh(); 
				}
			}
		}
	}
}
