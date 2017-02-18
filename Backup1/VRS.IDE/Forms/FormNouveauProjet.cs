using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using VRS.Library.Projet;

using VRS.IDE.Resource; 
using VRS.Library.IO;
using VRS.Library.Table;
using VRS.Library.Table.TBL;
using VRS.Library.Console.SuperNintendo;

namespace VRS.IDE.Forms {
	/// <summary>
	/// Description résumée de FormNouveauProjet.
	/// </summary>
	public class FormNouveauProjet : System.Windows.Forms.Form {
		/// <summary>
		/// Gestion des Resources
		/// </summary>
		Resources _res; 

		/// <summary>
		/// Access a la boite de dialogue principal
		/// </summary>
		FormMain _mainDialog;

		/// <summary>
		/// Non de projet jamais modifié
		/// </summary>
		private bool UserHaveAllReadyProjectName = false;

		/// <summary>
		/// si true les bookmark qui sont inscrit dans la TBL seront ajouter au projet
		/// </summary>
		private bool _UseTBLBookMark = false;

		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Button cmdCreateProject;
		private System.Windows.Forms.Label lblProjectName;
		private System.Windows.Forms.Button cmdFindFolder;
		private System.Windows.Forms.Label lblProjectPath;
		private System.Windows.Forms.Button cmdFindRom;
		private System.Windows.Forms.Label lblRomFile;
		private VRS.Library.UserControl.Ligne3d ligne3d1;
		private System.Windows.Forms.Label lblTBL;
		private System.Windows.Forms.Button cmdFindTBL;
		private VRS.Library.UserControl.GradientLabel GradLabelProjectInfo;
		private VRS.Library.UserControl.GradientLabel GradLabelProjectType;
		private System.Windows.Forms.ImageList ListImageProject;
		private System.Windows.Forms.ListView lvProjectType;
		private VRS.UI.Controls.WEditBox txtProjectName;
		private VRS.UI.Controls.WEditBox txtProjectBaseFolder;
		private VRS.UI.Controls.WEditBox txtRomPath;
		private VRS.UI.Controls.WEditBox txtTBLPath; 
		private System.ComponentModel.IContainer components;


		public FormNouveauProjet(Resources res, FormMain frm) {
			//Resources
			this._res = res;

			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//Cree un access a la boite FormMain
			this._mainDialog = frm;

			//Chargement de du language 
			LoadLanguage();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormNouveauProjet));
			this.cmdClose = new System.Windows.Forms.Button();
			this.cmdCreateProject = new System.Windows.Forms.Button();
			this.lblProjectName = new System.Windows.Forms.Label();
			this.lblProjectPath = new System.Windows.Forms.Label();
			this.cmdFindFolder = new System.Windows.Forms.Button();
			this.cmdFindRom = new System.Windows.Forms.Button();
			this.lblRomFile = new System.Windows.Forms.Label();
			this.lblTBL = new System.Windows.Forms.Label();
			this.cmdFindTBL = new System.Windows.Forms.Button();
			this.ligne3d1 = new VRS.Library.UserControl.Ligne3d();
			this.GradLabelProjectInfo = new VRS.Library.UserControl.GradientLabel();
			this.GradLabelProjectType = new VRS.Library.UserControl.GradientLabel();
			this.lvProjectType = new System.Windows.Forms.ListView();
			this.ListImageProject = new System.Windows.Forms.ImageList(this.components);
			this.txtProjectName = new VRS.UI.Controls.WEditBox();
			this.txtProjectBaseFolder = new VRS.UI.Controls.WEditBox();
			this.txtRomPath = new VRS.UI.Controls.WEditBox();
			this.txtTBLPath = new VRS.UI.Controls.WEditBox();
			((System.ComponentModel.ISupportInitialize)(this.txtProjectName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtProjectBaseFolder)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRomPath)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTBLPath)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdClose
			// 
			this.cmdClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(448, 317);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.TabIndex = 0;
			this.cmdClose.Text = "DialogueDefault_Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// cmdCreateProject
			// 
			this.cmdCreateProject.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.cmdCreateProject.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCreateProject.Location = new System.Drawing.Point(328, 317);
			this.cmdCreateProject.Name = "cmdCreateProject";
			this.cmdCreateProject.Size = new System.Drawing.Size(112, 23);
			this.cmdCreateProject.TabIndex = 1;
			this.cmdCreateProject.Text = "CreateProject";
			this.cmdCreateProject.Click += new System.EventHandler(this.cmdCreateProject_Click);
			// 
			// lblProjectName
			// 
			this.lblProjectName.Location = new System.Drawing.Point(24, 186);
			this.lblProjectName.Name = "lblProjectName";
			this.lblProjectName.Size = new System.Drawing.Size(144, 16);
			this.lblProjectName.TabIndex = 5;
			this.lblProjectName.Text = "ProjectName";
			this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblProjectPath
			// 
			this.lblProjectPath.Location = new System.Drawing.Point(24, 210);
			this.lblProjectPath.Name = "lblProjectPath";
			this.lblProjectPath.Size = new System.Drawing.Size(144, 16);
			this.lblProjectPath.TabIndex = 6;
			this.lblProjectPath.Text = "ProjectPath";
			this.lblProjectPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmdFindFolder
			// 
			this.cmdFindFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFindFolder.Location = new System.Drawing.Point(472, 208);
			this.cmdFindFolder.Name = "cmdFindFolder";
			this.cmdFindFolder.Size = new System.Drawing.Size(32, 20);
			this.cmdFindFolder.TabIndex = 7;
			this.cmdFindFolder.Text = "...";
			this.cmdFindFolder.Click += new System.EventHandler(this.cmdFindFolder_Click);
			// 
			// cmdFindRom
			// 
			this.cmdFindRom.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFindRom.Location = new System.Drawing.Point(472, 232);
			this.cmdFindRom.Name = "cmdFindRom";
			this.cmdFindRom.Size = new System.Drawing.Size(32, 20);
			this.cmdFindRom.TabIndex = 8;
			this.cmdFindRom.Text = "...";
			this.cmdFindRom.Click += new System.EventHandler(this.cmdFindRom_Click);
			// 
			// lblRomFile
			// 
			this.lblRomFile.Location = new System.Drawing.Point(24, 234);
			this.lblRomFile.Name = "lblRomFile";
			this.lblRomFile.Size = new System.Drawing.Size(144, 16);
			this.lblRomFile.TabIndex = 9;
			this.lblRomFile.Text = "Rom";
			this.lblRomFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTBL
			// 
			this.lblTBL.Location = new System.Drawing.Point(24, 258);
			this.lblTBL.Name = "lblTBL";
			this.lblTBL.Size = new System.Drawing.Size(144, 16);
			this.lblTBL.TabIndex = 12;
			this.lblTBL.Text = "TBL";
			this.lblTBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmdFindTBL
			// 
			this.cmdFindTBL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFindTBL.Location = new System.Drawing.Point(472, 256);
			this.cmdFindTBL.Name = "cmdFindTBL";
			this.cmdFindTBL.Size = new System.Drawing.Size(32, 20);
			this.cmdFindTBL.TabIndex = 11;
			this.cmdFindTBL.Text = "...";
			this.cmdFindTBL.Click += new System.EventHandler(this.cmdFindTBL_Click);
			// 
			// ligne3d1
			// 
			this.ligne3d1.CouleurLigne1 = System.Drawing.Color.Gray;
			this.ligne3d1.CouleurLigne2 = System.Drawing.Color.White;
			this.ligne3d1.Location = new System.Drawing.Point(16, 304);
			this.ligne3d1.Name = "ligne3d1";
			this.ligne3d1.Size = new System.Drawing.Size(504, 3);
			this.ligne3d1.TabIndex = 13;
			// 
			// GradLabelProjectInfo
			// 
			this.GradLabelProjectInfo.CouleurDroite = System.Drawing.SystemColors.Control;
			this.GradLabelProjectInfo.CouleurGauche = System.Drawing.SystemColors.InactiveCaptionText;
			this.GradLabelProjectInfo.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GradLabelProjectInfo.Location = new System.Drawing.Point(16, 152);
			this.GradLabelProjectInfo.Name = "GradLabelProjectInfo";
			this.GradLabelProjectInfo.Size = new System.Drawing.Size(512, 24);
			this.GradLabelProjectInfo.TabIndex = 6;
			this.GradLabelProjectInfo.Text = "ProjectInfo";
			this.GradLabelProjectInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GradLabelProjectType
			// 
			this.GradLabelProjectType.CouleurDroite = System.Drawing.SystemColors.Control;
			this.GradLabelProjectType.CouleurGauche = System.Drawing.SystemColors.InactiveCaptionText;
			this.GradLabelProjectType.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GradLabelProjectType.Location = new System.Drawing.Point(16, 16);
			this.GradLabelProjectType.Name = "GradLabelProjectType";
			this.GradLabelProjectType.Size = new System.Drawing.Size(512, 24);
			this.GradLabelProjectType.TabIndex = 14;
			this.GradLabelProjectType.Text = "ProjectType";
			this.GradLabelProjectType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lvProjectType
			// 
			this.lvProjectType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvProjectType.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvProjectType.HideSelection = false;
			this.lvProjectType.LargeImageList = this.ListImageProject;
			this.lvProjectType.Location = new System.Drawing.Point(16, 48);
			this.lvProjectType.Name = "lvProjectType";
			this.lvProjectType.Size = new System.Drawing.Size(512, 97);
			this.lvProjectType.TabIndex = 15;
			// 
			// ListImageProject
			// 
			this.ListImageProject.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.ListImageProject.ImageSize = new System.Drawing.Size(32, 32);
			this.ListImageProject.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListImageProject.ImageStream")));
			this.ListImageProject.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// txtProjectName
			// 
			this.txtProjectName.DecimalPlaces = 2;
			this.txtProjectName.DecMaxValue = 999999999;
			this.txtProjectName.DecMinValue = -999999999;
			this.txtProjectName.Location = new System.Drawing.Point(168, 184);
			this.txtProjectName.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.txtProjectName.MaxLength = 70;
			this.txtProjectName.Multiline = false;
			this.txtProjectName.Name = "txtProjectName";
			this.txtProjectName.PasswordChar = '\0';
			this.txtProjectName.ReadOnly = false;
			this.txtProjectName.Size = new System.Drawing.Size(208, 20);
			this.txtProjectName.TabIndex = 16;
			this.txtProjectName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtProjectName.UseStaticViewStyle = false;
			this.txtProjectName.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			// 
			// txtProjectBaseFolder
			// 
			this.txtProjectBaseFolder.DecimalPlaces = 2;
			this.txtProjectBaseFolder.DecMaxValue = 999999999;
			this.txtProjectBaseFolder.DecMinValue = -999999999;
			this.txtProjectBaseFolder.Location = new System.Drawing.Point(168, 208);
			this.txtProjectBaseFolder.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.txtProjectBaseFolder.MaxLength = 70;
			this.txtProjectBaseFolder.Multiline = false;
			this.txtProjectBaseFolder.Name = "txtProjectBaseFolder";
			this.txtProjectBaseFolder.PasswordChar = '\0';
			this.txtProjectBaseFolder.ReadOnly = true;
			this.txtProjectBaseFolder.Size = new System.Drawing.Size(296, 20);
			this.txtProjectBaseFolder.TabIndex = 17;
			this.txtProjectBaseFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtProjectBaseFolder.UseStaticViewStyle = false;
			this.txtProjectBaseFolder.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			// 
			// txtRomPath
			// 
			this.txtRomPath.DecimalPlaces = 2;
			this.txtRomPath.DecMaxValue = 999999999;
			this.txtRomPath.DecMinValue = -999999999;
			this.txtRomPath.Location = new System.Drawing.Point(168, 232);
			this.txtRomPath.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.txtRomPath.MaxLength = 70;
			this.txtRomPath.Multiline = false;
			this.txtRomPath.Name = "txtRomPath";
			this.txtRomPath.PasswordChar = '\0';
			this.txtRomPath.ReadOnly = true;
			this.txtRomPath.Size = new System.Drawing.Size(296, 20);
			this.txtRomPath.TabIndex = 18;
			this.txtRomPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtRomPath.UseStaticViewStyle = false;
			this.txtRomPath.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			// 
			// txtTBLPath
			// 
			this.txtTBLPath.DecimalPlaces = 2;
			this.txtTBLPath.DecMaxValue = 999999999;
			this.txtTBLPath.DecMinValue = -999999999;
			this.txtTBLPath.Location = new System.Drawing.Point(168, 256);
			this.txtTBLPath.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.txtTBLPath.MaxLength = 70;
			this.txtTBLPath.Multiline = false;
			this.txtTBLPath.Name = "txtTBLPath";
			this.txtTBLPath.PasswordChar = '\0';
			this.txtTBLPath.ReadOnly = true;
			this.txtTBLPath.Size = new System.Drawing.Size(296, 20);
			this.txtTBLPath.TabIndex = 19;
			this.txtTBLPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtTBLPath.UseStaticViewStyle = false;
			this.txtTBLPath.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			// 
			// FormNouveauProjet
			// 
			this.AcceptButton = this.cmdCreateProject;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(538, 352);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.txtTBLPath,
																		  this.txtRomPath,
																		  this.txtProjectBaseFolder,
																		  this.lvProjectType,
																		  this.GradLabelProjectType,
																		  this.ligne3d1,
																		  this.lblTBL,
																		  this.cmdFindTBL,
																		  this.lblRomFile,
																		  this.cmdFindRom,
																		  this.cmdFindFolder,
																		  this.lblProjectPath,
																		  this.lblProjectName,
																		  this.cmdCreateProject,
																		  this.cmdClose,
																		  this.GradLabelProjectInfo,
																		  this.txtProjectName});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormNouveauProjet";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.Load += new System.EventHandler(this.FormNouveauProjet_Load);
			((System.ComponentModel.ISupportInitialize)(this.txtProjectName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtProjectBaseFolder)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRomPath)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTBLPath)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Méthodes
		private void LoadLanguage() {
			const string prefix = "FormNewProjet_";

			cmdCreateProject.Text = _res.GetString(prefix + cmdCreateProject.Text); 
			cmdClose.Text = _res.GetString(cmdClose.Text); 
			this.Text = _res.GetString(prefix + this.Text);
			lblProjectName.Text = _res.GetString(prefix + lblProjectName.Text);
			lblProjectPath.Text = _res.GetString(prefix + lblProjectPath.Text);
			lblRomFile.Text = _res.GetString(prefix + lblRomFile.Text);
			lblTBL.Text = _res.GetString(prefix + lblTBL.Text);
			GradLabelProjectInfo.Text = _res.GetString(prefix + GradLabelProjectInfo.Text);
			GradLabelProjectType.Text = _res.GetString(prefix + GradLabelProjectType.Text);

		}
		#endregion

		private void cmdClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void FormNouveauProjet_Load(object sender, System.EventArgs e) {
			txtProjectBaseFolder.Text = Directory.GetCurrentDirectory();

			lvProjectType.Items.Add(_res.GetString("ProjectTypeSuperNintendo"), 0);
		}

		private void cmdFindFolder_Click(object sender, System.EventArgs e) {
			OpenFolderDialog fld = new OpenFolderDialog(_res.GetString("FormNewProjet_FolderDialogProjectPath"));
			string folder = fld.GetFolder();
			if (folder != "")
				txtProjectBaseFolder.Text = folder;
		}

		private void cmdFindRom_Click(object sender, System.EventArgs e) {
			//Obtenir le fichier a ouvrir
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Title = _res.GetString("OpenDialog_OpenRomTitle");
			openFile.Filter = _res.GetString("OpenDialog_OpenRomFilter");
			openFile.CheckFileExists = true;
			openFile.CheckPathExists = true;
			openFile.ShowDialog(this);
			string FileName = openFile.FileName;

			if (File.Exists(FileName)){
				if (SnesRom.isValid(FileName)){
					SnesRom snes = new SnesRom(FileName);
					
					//Nom du projet
					if (!UserHaveAllReadyProjectName || txtProjectName.Text == "")
						txtProjectName.Text = snes.RomName;

                    txtRomPath.Text = FileName;
				}
				else
					MessageBox.Show(this, this._res.GetString("InvalidSnesFile"),
						this._res.GetString("Erreur"),
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
			}
		}

		private void textBox2_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			UserHaveAllReadyProjectName = true;
		}

		private void cmdFindTBL_Click(object sender, System.EventArgs e) {
			//Obtenir le fichier a ouvrir
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Title = _res.GetString("OpenDialog_OpenRomTitle");
			openFile.Filter = _res.GetString("OpenDialog_OpenTBLFilter");
			openFile.CheckFileExists = true;
			openFile.CheckPathExists = true;
			openFile.ShowDialog(this);
			string FileName = openFile.FileName;

			if (File.Exists(FileName)){
				TBLStream tbl = new TBLStream(FileName);
				tbl.Load();
 
				this._UseTBLBookMark = false;

				if (tbl.Length > 0){
					txtTBLPath.Text = FileName;
					if (tbl.BookMark.Count > 0){
						if (MessageBox.Show(this, this._res.GetString("FormNewProjet_TBLHaveBookMark"), 
							App.Name, 
							MessageBoxButtons.YesNo, 
							MessageBoxIcon.Question) == DialogResult.Yes)
							this._UseTBLBookMark = true;
						else
							this._UseTBLBookMark = false;
					}
				}
				else{
					MessageBox.Show(this, this._res.GetString("InvalidTBLFile"),
						this._res.GetString("Erreur"),
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				}
			}
		}

		private void cmdCreateProject_Click(object sender, System.EventArgs e) {
			if (txtProjectBaseFolder.Text != "" &&
				txtProjectName.Text != "" &&
				txtRomPath.Text != "" &&
				txtTBLPath.Text != ""){
				//Ferme le projet presentement ouvert dans FormMain
				this._mainDialog._ProjetExplorer.CloseProject();

				//Creation d'un projet
				Project projet = new Project();
				projet.CreateProject(this.txtProjectName.Text,
					this.txtProjectBaseFolder.Text,
					this.txtRomPath.Text,
					this.txtTBLPath.Text);


				//Ajout des favoris
				if (this._UseTBLBookMark){
					TBLStream tbl = new TBLStream(this.txtTBLPath.Text);
					tbl.Load();
					Favoris fav;
					for (int i=0; i< tbl.BookMark.Count; i++){
						fav = (Favoris)tbl.BookMark[i];
						fav.File = Path.GetFileName(this.txtRomPath.Text); 
						fav.Key = "mark" + projet.Favoris.Count; 
						projet.Favoris.Add(fav); 
					}

					projet.Save();
				}

				//ouverture du projet dans l'explorateur de projet
				this._mainDialog.OpenProject(projet.FileName);
				
				//Fermeture de la boite de dialogue
				this.Close();
			}else{
				MessageBox.Show(this, this._res.GetString("FormNewProjet_CreateEnterAllInfo"), 
					this._res.GetString("Erreur"), 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Information); 
			}
		}
	}
}
