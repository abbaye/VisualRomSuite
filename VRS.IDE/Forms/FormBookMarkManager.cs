using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using VRS.Library.Win32;
using VRS.IDE.Resource;
using VRS.Library.Projet; 

namespace VRS.IDE.Forms {
	/// <summary>
	/// Description résumée de BookMarkManager.
	/// </summary>
	public class FormBookMarkManager : System.Windows.Forms.Form {
		FormMain _Main = null;
		Resources _res = null;

		private VRS.Library.UserControl.Ligne3d ligne3d1;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.ListView lvBookMark;
		private VRS.Library.UserControl.GradientLabel GradLabelListFav;
		private System.Windows.Forms.ColumnHeader colHeadFavoris;
		private System.Windows.Forms.Button cmdDelete;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormBookMarkManager(Resources res, FormMain main) {
			this._Main = main;
			this._res = res;

			InitializeComponent();

			StaticBorder.ThinBorder(lvBookMark.Handle.ToInt32(), true);

			//Chargement des langues
			LoadLanguage();

			//Chargement de la liste de bookmark
			LoadBookMark();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			this.lvBookMark = new System.Windows.Forms.ListView();
			this.ligne3d1 = new VRS.Library.UserControl.Ligne3d();
			this.cmdClose = new System.Windows.Forms.Button();
			this.GradLabelListFav = new VRS.Library.UserControl.GradientLabel();
			this.colHeadFavoris = new System.Windows.Forms.ColumnHeader();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lvBookMark
			// 
			this.lvBookMark.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvBookMark.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.colHeadFavoris});
			this.lvBookMark.FullRowSelect = true;
			this.lvBookMark.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvBookMark.HideSelection = false;
			this.lvBookMark.Location = new System.Drawing.Point(8, 24);
			this.lvBookMark.MultiSelect = false;
			this.lvBookMark.Name = "lvBookMark";
			this.lvBookMark.Size = new System.Drawing.Size(208, 288);
			this.lvBookMark.TabIndex = 0;
			this.lvBookMark.View = System.Windows.Forms.View.Details;
			// 
			// ligne3d1
			// 
			this.ligne3d1.CouleurLigne1 = System.Drawing.Color.Gray;
			this.ligne3d1.CouleurLigne2 = System.Drawing.Color.White;
			this.ligne3d1.Location = new System.Drawing.Point(8, 320);
			this.ligne3d1.Name = "ligne3d1";
			this.ligne3d1.Size = new System.Drawing.Size(288, 3);
			this.ligne3d1.TabIndex = 21;
			// 
			// cmdClose
			// 
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(224, 328);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.TabIndex = 20;
			this.cmdClose.Text = "DialogueDefault_Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// GradLabelListFav
			// 
			this.GradLabelListFav.CouleurDroite = System.Drawing.SystemColors.Control;
			this.GradLabelListFav.CouleurGauche = System.Drawing.SystemColors.InactiveCaptionText;
			this.GradLabelListFav.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GradLabelListFav.Location = new System.Drawing.Point(8, 8);
			this.GradLabelListFav.Name = "GradLabelListFav";
			this.GradLabelListFav.Size = new System.Drawing.Size(208, 16);
			this.GradLabelListFav.TabIndex = 23;
			this.GradLabelListFav.Text = "FAVORIS";
			this.GradLabelListFav.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// colHeadFavoris
			// 
			this.colHeadFavoris.Text = "ColHeadFav";
			this.colHeadFavoris.Width = 193;
			// 
			// cmdDelete
			// 
			this.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdDelete.Location = new System.Drawing.Point(224, 24);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(75, 24);
			this.cmdDelete.TabIndex = 24;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// FormBookMarkManager
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(306, 360);
			this.ControlBox = false;
			this.Controls.Add(this.cmdDelete);
			this.Controls.Add(this.GradLabelListFav);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.lvBookMark);
			this.Controls.Add(this.ligne3d1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormBookMarkManager";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.Load += new System.EventHandler(this.FormBookMarkManager_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void FormBookMarkManager_Load(object sender, System.EventArgs e) {
		
		}
    		
		private void LoadLanguage() {
			const string prefix = "FormBookMarkManager_";

			this.Text = _res.GetString(prefix + this.Text);
			cmdClose.Text = _res.GetString(cmdClose.Text);
			cmdDelete.Text = _res.GetString(prefix + cmdDelete.Text);
			GradLabelListFav.Text = _res.GetString(prefix + GradLabelListFav.Text);
		}

		private void LoadBookMark() {
			if (this._Main._ProjetExplorer.Projet != null){
				
				//Load les BookMark
				Project prj = this._Main._ProjetExplorer.Projet;
				
				Favoris fav = null;
				ListViewItem item = null;
				for (int i=0; i < prj.Favoris.Count; i++){
					fav = (Favoris)prj.Favoris[i];

					item = new ListViewItem();
					item.Text = fav.Name;
					item.Tag = fav.Key; //ProjectKey
					lvBookMark.Items.Add(item);
				}
			}
		}

		/// <summary>
		/// Supprimer le Favoris selectionner
		/// </summary>		
		private void cmdDelete_Click(object sender, System.EventArgs e) {
		
			if (MessageBox.Show(this, this._res.GetString("FormBookMarkManager_DeleteMessage"), 
				this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
				
				//Recherche et supression du favoris				
				Project prj = this._Main._ProjetExplorer.Projet;
				if (prj != null){
					for (int i=0; i < prj.Favoris.Count; i++){
						if ( lvBookMark.SelectedItems[0].Tag.ToString() == ((Favoris)prj.Favoris[i]).Key){
							//Efface le favoris du projet
							prj.Favoris.RemoveAt(i);
						
							//Recharger les favoris dans FormMain
							this._Main.LoadBookMark();

							//Efface le favoris de la liste
							lvBookMark.Items.RemoveAt(lvBookMark.SelectedIndices[0]);
							break;
						}
					}
				}
			}
		}
	}
}
