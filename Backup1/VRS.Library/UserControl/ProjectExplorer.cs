using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

using VRS.Library.Projet;
using VRS.Library.Console.SuperNintendo;
using VRS.Library.Table.TBL;

namespace VRS.Library.UserControl {
	/// <summary>
	/// Description résumée de ProjectExplorer.
	/// </summary>
	public class ProjectExplorer : System.Windows.Forms.UserControl {
		public event EventHandler ExplorerDoubleClick;
		public event EventHandler ExplorerClick;
		public event MouseEventHandler RightClick;
		public event TreeViewEventHandler SelectionChange;

		private System.Windows.Forms.ImageList ListImageToolBar;
		private System.Windows.Forms.ToolBar tbExplorer;
		private System.Windows.Forms.ToolBarButton toolBarButtonRefresh;
		private System.Windows.Forms.TreeView tvProject;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Projet charger dans l'explorateur
		/// </summary>
		Project _Projet = null;

		/// <summary>
		/// Nom du fichier projet
		/// </summary>
		private string _FileName = "";

		/// <summary>
		/// Chaine de caractere servant dans l'explorateur
		/// </summary>
		private string _String_TBL = "Tables";
		private string _String_Texte = "Textes";
		private string _String_Hexa = "Binaire";
		private string _String_Projet = "Projet";
		public System.Windows.Forms.ImageList ListImageTreeView;	

		public ProjectExplorer() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();	
			
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProjectExplorer));
			this.ListImageToolBar = new System.Windows.Forms.ImageList(this.components);
			this.tbExplorer = new System.Windows.Forms.ToolBar();
			this.toolBarButtonRefresh = new System.Windows.Forms.ToolBarButton();
			this.tvProject = new System.Windows.Forms.TreeView();
			this.ListImageTreeView = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// ListImageToolBar
			// 
			this.ListImageToolBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.ListImageToolBar.ImageSize = new System.Drawing.Size(16, 16);
			this.ListImageToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListImageToolBar.ImageStream")));
			this.ListImageToolBar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tbExplorer
			// 
			this.tbExplorer.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tbExplorer.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.toolBarButtonRefresh});
			this.tbExplorer.Divider = false;
			this.tbExplorer.DropDownArrows = true;
			this.tbExplorer.ImageList = this.ListImageToolBar;
			this.tbExplorer.Location = new System.Drawing.Point(0, 0);
			this.tbExplorer.Name = "tbExplorer";
			this.tbExplorer.ShowToolTips = true;
			this.tbExplorer.Size = new System.Drawing.Size(184, 26);
			this.tbExplorer.TabIndex = 1;
			this.tbExplorer.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbExplorer_ButtonClick);
			// 
			// toolBarButtonRefresh
			// 
			this.toolBarButtonRefresh.ImageIndex = 0;
			this.toolBarButtonRefresh.Tag = "refresh";
			// 
			// tvProject
			// 
			this.tvProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tvProject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvProject.HideSelection = false;
			this.tvProject.ImageList = this.ListImageTreeView;
			this.tvProject.Location = new System.Drawing.Point(0, 23);
			this.tvProject.Name = "tvProject";
			this.tvProject.ShowRootLines = false;
			this.tvProject.Size = new System.Drawing.Size(184, 249);
			this.tvProject.TabIndex = 2;
			this.tvProject.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProject_AfterExpand);
			this.tvProject.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProject_AfterCollapse);
			this.tvProject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvProject_MouseUp);
			this.tvProject.DoubleClick += new System.EventHandler(this.tvProject_DoubleClick);
			this.tvProject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProject_AfterSelect);
			// 
			// ListImageTreeView
			// 
			this.ListImageTreeView.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.ListImageTreeView.ImageSize = new System.Drawing.Size(16, 16);
			this.ListImageTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListImageTreeView.ImageStream")));
			this.ListImageTreeView.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// ProjectExplorer
			// 
			this.Controls.Add(this.tvProject);
			this.Controls.Add(this.tbExplorer);
			this.Name = "ProjectExplorer";
			this.Size = new System.Drawing.Size(184, 272);
			this.ResumeLayout(false);

		}
		#endregion

		#region Property
		/// <summary>
		/// Obtenir le Nom du fichier chargé
		/// </summary>
		public string FileName{
			get{
				return this._FileName;
			}
		}

		[Category("String")]
		public string StringProjet{
			get{
				return this._String_Projet;
			}
			set{
				this._String_Projet = value;
			}
		}

		[Category("String")]
		public string StringTable{
			get{
				return this._String_TBL;
			}
			set{
				this._String_TBL = value;
			}
		}

		[Category("String")]
		public string StringText{
			get{
				return this._String_Texte;
			}
			set{
				this._String_Texte = value;
			}
		}

		[Category("String")]
		public string StringHexa{
			get{
				return this._String_Hexa;
			}
			set{
				this._String_Hexa = value;
			}
		}

		/// <summary>
		/// Projet presentement Chargé
		/// </summary>
		public Project Projet{
			get{
				return this._Projet; 
			}
			set{
				this._Projet = value;
			}
		}    
  
		/// <summary>
		/// Obtenir le type de noeud selectionné
		/// </summary>
		public ExplorerNodeType SelectedItemType{
			get{
				if(tvProject.SelectedNode != null){
					if (tvProject.SelectedNode.Tag.ToString() == "root")
						return ExplorerNodeType.Root;

					if (tvProject.SelectedNode.Tag.ToString() == "rom")
						return ExplorerNodeType.Rom;

					if (tvProject.SelectedNode.Tag.ToString() == "workrom")
						return ExplorerNodeType.WorkRom;

					TreeNode parent = tvProject.SelectedNode.Parent;

					switch (parent.Tag.ToString()){										
						case "tbl":
							return ExplorerNodeType.TBLFile; 						
						case "hexasnap":
							return ExplorerNodeType.HexaFile; 						
						case "text":
							return ExplorerNodeType.TextFile; 
						case "folder":
							return ExplorerNodeType.Folder; 
						default:
							return ExplorerNodeType.Nothing; //Rien de selectionner						
					}
				}else
					return ExplorerNodeType.Nothing;
			}
		}

		/// <summary>
		/// Obtenir l'objet selectionné
		/// (TODO : a optimiser bientot)
		/// </summary>
		/// <remarks>
		/// ExplorerNodeType.HexaFile = objet de type : VRS.Library.Projet.HexaSnapShot
		/// ExplorerNodeType.TBLFile = objet de type : VRS.Library.Projet.TBLStream
		/// ExplorerNodeType.Root = objet de type : VRS.Library.Projet.Project
		/// ExplorerNodeType.Nothing = objet de type : null
		/// </remarks> 
		public object GetSelectedObject(){
			SnesRom snes = null;

			switch (this.SelectedItemType){
				case ExplorerNodeType.HexaFile:
					HexaSnapShot hexsnap = null;
					for (int i =0; i< this._Projet.HexaSnapShot.Count; i++){
						if (((HexaSnapShot)this._Projet.HexaSnapShot[i]).Key == tvProject.SelectedNode.Tag.ToString())
							hexsnap = (HexaSnapShot)this.Projet.HexaSnapShot[i];
					}
					return hexsnap;
				case ExplorerNodeType.TBLFile:
					TBLStream tbl = null;
					for (int i =0; i< this._Projet.Tables.Count; i++){
						if (((TBLFile)this._Projet.Tables[i]).Key == tvProject.SelectedNode.Tag.ToString()){
							string path = this._Projet.ProjectPath  + Path.DirectorySeparatorChar + ((TBLFile)this.Projet.Tables[i]).RelativePath;  
							tbl = new TBLStream(path);
							tbl.key = tvProject.SelectedNode.Tag.ToString();
							tbl.Load();							
						}							
					}
					return tbl;
				case ExplorerNodeType.TextFile:
					TextFile txt = null;
					for (int i =0; i< this._Projet.Textes.Count; i++){
						if (((TextFile)this._Projet.Textes[i]).key == tvProject.SelectedNode.Tag.ToString())
							txt = (TextFile)this.Projet.Textes[i];
					}
					return txt;
				case ExplorerNodeType.Root:
					return this._Projet;
				case ExplorerNodeType.Folder:
					//TODO: a finir
					Folder dir = new Folder();
					return dir;
				case ExplorerNodeType.Rom:
					snes = new SnesRom(this._Projet.ProjectPath + @"\" + this._Projet.RomFile);
					snes.Load();
					return snes;
				case ExplorerNodeType.WorkRom:
					snes = new SnesRom(this._Projet.ProjectPath + @"\" + this._Projet.WorkRomFile);
					snes.Load();
					return snes;
				default:
					return null;
			}
		}
		#endregion

		#region Methodes
		/// <summary>
		/// Ferme le projet et reinitialise a zero le control
		/// </summary>
		public void CloseProject(){
			this._FileName = "";
			this._Projet = null;
			
			//vide le treeview
			tvProject.Nodes.Clear();
		}

		/// <summary>
		/// Enregistre le projet
		/// </summary>
		public void SaveProject(){
			if (this._Projet != null)
				this._Projet.Save();
		}

		/// <summary>
		/// Mets en gras la TBL selectionner
		/// </summary>
		public void SetBoldSelectedTBL(){
			TreeNodeCollection nodes = null;
			TreeNode node = null;
						
			nodes = tvProject.SelectedNode.Parent.Nodes;
			 
			for (int i=0; i<nodes.Count; i++){
				node = nodes[i];
				node.NodeFont = new Font(this.Font.Name, 8, FontStyle.Regular);
			}

			tvProject.SelectedNode.NodeFont = new Font(this.Font.Name, 8, FontStyle.Bold);
		}

        public ProjectError RefreshExplorer(){
            return LoadProject(this._FileName, true);
        }

		/// <summary>
		/// Charger un projet dans l'explorateur de projet
		/// </summary>
		/// <param name="FileName">Chemin du fichier a charger</param>
		/// <returns>
		/// Retourne une enumeration du type ProjectError qui 
		/// dicte le type d'erreur rencontré lors du chargement
		/// </returns>
		public ProjectError LoadProject(string FileName, bool Refresh){
			ProjectError returnValue = ProjectError.NoError;

			if (File.Exists(FileName)){
				if (!Refresh){
					//Efface le projet courrant
					CloseProject();

					this._FileName = FileName;

					//Cree un nouveau projet
					this._Projet = new Project(FileName);
					returnValue= this._Projet.Load();					   
				}
				else{
					this.tvProject.Nodes.Clear();
					returnValue = ProjectError.NoError;
				}

				//Creation des nodes pour le treeview du ProjectExplorer	
				if (returnValue == ProjectError.NoError){

					tvProject.BeginUpdate();

					//Creation des rom
					string ProjectPath = this._Projet.ProjectPath;
					string romPath = ProjectPath + @"\" + this._Projet.RomFile;
					string romWorkPath = ProjectPath + @"\" + this._Projet.WorkRomFile;

					SnesRom snes = new SnesRom(ProjectPath + @"\" + this._Projet.RomFile);
					snes.Load();

					TreeNode RomNode = new TreeNode(snes.RomName);
					RomNode.ImageIndex = 6;
					RomNode.SelectedImageIndex = 6;
					RomNode.Tag = "rom";

					snes.Load(romWorkPath);
					TreeNode WorkRomNode = new TreeNode(snes.RomName);
					WorkRomNode.ImageIndex = 7;
					WorkRomNode.SelectedImageIndex = 7;
					WorkRomNode.Tag = "workrom";

					//Creation des fichier Table					
					TreeNode TableNode = new TreeNode(this._String_TBL, MakeTableNode());
					TableNode.ImageIndex = 2;
					TableNode.SelectedImageIndex = 2; 
					TableNode.Tag = "tbl";
										                    
					//Creation des Fichier Texte					
					TreeNode TextNode = new TreeNode(this._String_Texte, MakeTextNodes());
					TextNode.SelectedImageIndex = 2;
					TextNode.ImageIndex = 2;
					TextNode.Tag = "text";

					//Creation des Fichier Binaire					
					TreeNode HexaNode = new TreeNode(this._String_Hexa, MakeHexaSnapShotNode());
					HexaNode.ImageIndex = 2;
					HexaNode.SelectedImageIndex = 2;
					HexaNode.Tag = "hexasnap";

					//Ajoute la racine au tree view
					TreeNode[] RootChild = new TreeNode[5]{RomNode, WorkRomNode, TextNode, TableNode, HexaNode};
					TreeNode root = new TreeNode(this._String_Projet + " - '" + this._Projet.Name + "'", RootChild);
					root.ImageIndex = 0;
					root.Tag = "root";
					root.Expand(); 
					tvProject.Nodes.Add(root);

					tvProject.EndUpdate();

					//Fin de la fonction
					return returnValue;
				}
				else
					return returnValue; //Fin de la fonction
			}else
				return ProjectError.FileNotFound; //Fin de la fonction
		}

		private TreeNode[] MakeTableNode(){
			int TableCount = this._Projet.Tables.Count;
			TreeNode[] TableNodes = new TreeNode[TableCount];
			TBLFile tblFile = new TBLFile(); 

			for (int i=0; i < TableCount; i++){
				tblFile = (TBLFile)this._Projet.Tables[i];
				TableNodes[i] = new TreeNode(); 
				TableNodes[i].Text = tblFile.Name; 
				TableNodes[i].ImageIndex = 4;
				TableNodes[i].SelectedImageIndex = 4;
				TableNodes[i].Tag = tblFile.Key;

				if (tblFile.Default) 
					TableNodes[i].NodeFont = new Font(this.Font.Name, 8, FontStyle.Bold);
			}

			return TableNodes; 
		}

		private TreeNode[] MakeTextNodes(){
			int textCount = this._Projet.Textes.Count;
			TreeNode[] TextNodes = new TreeNode[textCount];
			TextFile txtFile = new TextFile(); 
			for (int i=0; i < textCount; i++){
				txtFile = (TextFile)this._Projet.Textes[i];
				TextNodes[i] = new TreeNode(); 
				TextNodes[i].Text = txtFile.Name; 
				TextNodes[i].ImageIndex = 3;
				TextNodes[i].SelectedImageIndex = 3;
				TextNodes[i].Tag = txtFile.key;
			}

			return TextNodes;
		}

		private TreeNode[] MakeHexaSnapShotNode(){
			int hexaCount = this._Projet.HexaSnapShot.Count;
			TreeNode[] HexaNodes = new TreeNode[hexaCount];
			HexaSnapShot HexaFile = new HexaSnapShot(); 
			for (int i=0; i < hexaCount; i++){
				HexaFile = (HexaSnapShot)this._Projet.HexaSnapShot[i];
				HexaNodes[i] = new TreeNode(); 
				HexaNodes[i].Text = HexaFile.Name; 
				HexaNodes[i].ImageIndex = 5;
				HexaNodes[i].SelectedImageIndex = 5;
				HexaNodes[i].Tag = HexaFile.Key;
			}

			return HexaNodes;
		}
		#endregion

		private void tbExplorer_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			
			if (this._Projet != null)				
				switch (e.Button.Tag.ToString()){
					case "refresh": //Rafraichi le control
						LoadProject(this._FileName, true);
						break;
				}

		}

		private void tvProject_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Ouvrir le folder
			if ((string)e.Node.Tag != "root")
				e.Node.ImageIndex = 1;
		}

		private void tvProject_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//fermer le folder
			if ((string)e.Node.Tag != "root")
				e.Node.ImageIndex = 2;
		}

		private void tvProject_DoubleClick(object sender, System.EventArgs e) {
			ExplorerDoubleClick(sender, e); //Evenement DoubleClick
		}

		private void tvProject_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Si le boutton droit est activer alors lancer l'event RightClick et selectionne la node
			try{
				tvProject.SelectedNode = tvProject.GetNodeAt(e.X, e.Y); //Selectionne la node qui se trouve au position (X,Y) de la souris

				if (e.Button == MouseButtons.Right)
					RightClick(sender, e);
				else //Lance l'evenement ExplorerClick
					ExplorerClick(sender, e);
			}catch{}
		}

		private void tvProject_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//TODO: Creation des barres d'outils en fonction de la node choisi

			//Lance l'evenement qui dicte que le node a été changé
            SelectionChange(sender, e);
		}
	}
}

