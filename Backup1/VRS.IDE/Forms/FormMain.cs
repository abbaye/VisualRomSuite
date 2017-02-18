using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Crownwood.Magic.Menus;
using Crownwood.Magic.Common;
using Crownwood.Magic.Controls;
using Crownwood.Magic.Docking;

using VRS.IDE.Resource;
using VRS.Library;
using VRS.Library.UserControl;
using VRS.Library.UserControl.TextExtractor;
using VRS.Library.UserControl.FixeTableEditor;
using VRS.Library.UserControl.TableEditor;
using VRS.Library.IO;
using VRS.Library.Projet;
using VRS.Library.Win32;
using VRS.Library.Table.TBL;

namespace VRS.IDE.Forms {
	/// <summary>
	/// Formulaire Principal de l'application
	/// </summary>
	public class FormMain : System.Windows.Forms.Form {
		
		#region Zone de déclaration public
		/// <summary>
		/// Style Visuel de VRS
		/// </summary>
		private VisualStyle _Style = VisualStyle.IDE;

		/// <summary>
		/// Menu Principal de VRS
		/// </summary>
		private MenuControl _MenuPrincipal;

		/// <summary>
		/// Manager pour les form Docking
		/// </summary>
		private DockingManager _DockManager;

		/// <summary>
		/// Culture de l'application (Gestion des ressouces)
		/// </summary>
		private Resources _res;

		/// <summary>
		/// Extracteur de texte
		/// </summary>
		private TextExtractor _TextExtractor = null;

		/// <summary>
		/// Recherche de chaine
		/// </summary>
		private StringFinder _StringSearch = null;

		/// <summary>
		/// Explorateur de projet
		/// </summary>		
		public ProjectExplorer _ProjetExplorer;
		private Content prjExplorer;

		/// <summary>
		/// Liste des Tâches
		/// </summary>		
		public TaskList _TaskList;
		private Content prjTaskList;

		/// <summary>
		/// Propriété des objets
		/// </summary>
		public PropertyGrid _PropertyObject;
		private Content prjProperty;

		/// <summary>
		/// Affichage hexadecimal d'un fichier et sous sa forme decoder avec un TBL
		/// </summary>
		private HexaFileShow _HexaFileShow;
		private Content prjHexaFileShow;

		/// <summary>
		/// Sorti de Visual Rom Suite
		/// </summary>
		private TextEditor _AppOut;
		private Content prjOut;

		/// <summary>
		/// Emulateur
		/// </summary>
		private Process _Emulator = null;

		/// <summary>
		/// Menu Favoris
		/// </summary>
		private MenuCommand topBookMark;

		/// <summary>
		/// Menus Projet
		/// </summary>
		private MenuCommand topProject;
		private System.Windows.Forms.Timer timerTabGroupLeaf;

		/// <summary>
		/// Menu Compilation
		/// </summary>
		private MenuCommand topCompil;
		#endregion

		private System.Windows.Forms.StatusBar statusBarMain;
		private System.Windows.Forms.ImageList ImageListMain;
		private System.ComponentModel.IContainer components;
		internal TD.SandBar.ToolBarContainer bottomSandBarDock;
		internal TD.SandBar.ToolBarContainer leftSandBarDock;
		internal TD.SandBar.ToolBarContainer rightSandBarDock;
		internal TD.SandBar.ToolBarContainer topSandBarDock;
		private TD.SandBar.ButtonItem tbbStandardNewProject;
		private TD.SandBar.ToolBar tbStandard;
		private TD.SandBar.ButtonItem tbbStandardSave;
		private TD.SandBar.ButtonItem tbbStandardSaveAll;
		private TD.SandBar.ButtonItem tbbStandardOpen;
		private TD.SandBar.ButtonItem tbbStandardCut;
		private TD.SandBar.ButtonItem tbbStandardCopy;
		private TD.SandBar.ButtonItem tbbStandardPaste;
		private TD.SandBar.MenuBar menuBar1;
		private TD.SandBar.MenuBarItem menuBarItem1;
		private TD.SandBar.MenuBarItem menuBarItem2;
		private TD.SandBar.MenuBarItem menuBarItem3;
		private TD.SandBar.MenuBarItem menuBarItem4;
		private Crownwood.Magic.Controls.TabbedGroups tabGroup;
		internal TD.SandBar.SandBarManager toolbarManager;
		private TD.SandBar.ToolBar toolBar1;
		private TD.SandBar.ToolBar tbProject;
		private TD.SandBar.ButtonItem tbbProjectStartEmulator;
		private TD.SandBar.ButtonItem tbbProjectBuildProject;
        private TD.SandBar.ComboBoxItem tbbProjectComboEmulator;
		private TD.SandBar.MenuBarItem menuBarItem5;
		
		public FormMain() {
			//Creation de l'objet ressources ainsi que la culture du logiciel
			this._res = new Resources(App.AppCulture); 	
			
			// Requis pour la prise en charge du Concepteur Windows Forms
			InitializeComponent();

			//Creation du menu principal
			this._MenuPrincipal = CreateMenu();

			//Nom de l'application
			this.Text = App.Name;

			//Creation de la langue pour le tabgroup
			LoadTabGroupLanguage();

			//Creation des langues pour les toolbars
			MakeToolBarText();

			//Creation des forms Docking
			CreateDockingForm();	
		
			//TEMP: Choisi l'option Snes9x dans la liste des émulateur
			tbbProjectComboEmulator.ControlText  = "Snes9x";
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.ImageListMain = new System.Windows.Forms.ImageList(this.components);
            this.statusBarMain = new System.Windows.Forms.StatusBar();
            this.timerTabGroupLeaf = new System.Windows.Forms.Timer(this.components);
            this.toolbarManager = new TD.SandBar.SandBarManager();
            this.bottomSandBarDock = new TD.SandBar.ToolBarContainer();
            this.leftSandBarDock = new TD.SandBar.ToolBarContainer();
            this.rightSandBarDock = new TD.SandBar.ToolBarContainer();
            this.topSandBarDock = new TD.SandBar.ToolBarContainer();
            this.tbStandard = new TD.SandBar.ToolBar();
            this.tbbStandardNewProject = new TD.SandBar.ButtonItem();
            this.tbbStandardOpen = new TD.SandBar.ButtonItem();
            this.tbbStandardSave = new TD.SandBar.ButtonItem();
            this.tbbStandardSaveAll = new TD.SandBar.ButtonItem();
            this.tbbStandardCut = new TD.SandBar.ButtonItem();
            this.tbbStandardCopy = new TD.SandBar.ButtonItem();
            this.tbbStandardPaste = new TD.SandBar.ButtonItem();
            this.tbProject = new TD.SandBar.ToolBar();
            this.tbbProjectStartEmulator = new TD.SandBar.ButtonItem();
            this.tbbProjectComboEmulator = new TD.SandBar.ComboBoxItem();
            this.tbbProjectBuildProject = new TD.SandBar.ButtonItem();
            this.menuBar1 = new TD.SandBar.MenuBar();
            this.menuBarItem1 = new TD.SandBar.MenuBarItem();
            this.menuBarItem2 = new TD.SandBar.MenuBarItem();
            this.menuBarItem3 = new TD.SandBar.MenuBarItem();
            this.menuBarItem4 = new TD.SandBar.MenuBarItem();
            this.menuBarItem5 = new TD.SandBar.MenuBarItem();
            this.tabGroup = new Crownwood.Magic.Controls.TabbedGroups();
            this.toolBar1 = new TD.SandBar.ToolBar();
            this.topSandBarDock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageListMain
            // 
            this.ImageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListMain.ImageStream")));
            this.ImageListMain.TransparentColor = System.Drawing.Color.Magenta;
            this.ImageListMain.Images.SetKeyName(0, "");
            this.ImageListMain.Images.SetKeyName(1, "");
            this.ImageListMain.Images.SetKeyName(2, "");
            this.ImageListMain.Images.SetKeyName(3, "");
            this.ImageListMain.Images.SetKeyName(4, "");
            this.ImageListMain.Images.SetKeyName(5, "");
            this.ImageListMain.Images.SetKeyName(6, "");
            this.ImageListMain.Images.SetKeyName(7, "");
            this.ImageListMain.Images.SetKeyName(8, "");
            this.ImageListMain.Images.SetKeyName(9, "");
            this.ImageListMain.Images.SetKeyName(10, "");
            this.ImageListMain.Images.SetKeyName(11, "");
            this.ImageListMain.Images.SetKeyName(12, "");
            this.ImageListMain.Images.SetKeyName(13, "");
            this.ImageListMain.Images.SetKeyName(14, "");
            this.ImageListMain.Images.SetKeyName(15, "");
            this.ImageListMain.Images.SetKeyName(16, "");
            this.ImageListMain.Images.SetKeyName(17, "");
            this.ImageListMain.Images.SetKeyName(18, "");
            this.ImageListMain.Images.SetKeyName(19, "");
            this.ImageListMain.Images.SetKeyName(20, "");
            this.ImageListMain.Images.SetKeyName(21, "");
            this.ImageListMain.Images.SetKeyName(22, "");
            this.ImageListMain.Images.SetKeyName(23, "");
            this.ImageListMain.Images.SetKeyName(24, "");
            this.ImageListMain.Images.SetKeyName(25, "");
            // 
            // statusBarMain
            // 
            this.statusBarMain.Location = new System.Drawing.Point(0, 512);
            this.statusBarMain.Name = "statusBarMain";
            this.statusBarMain.Size = new System.Drawing.Size(904, 22);
            this.statusBarMain.TabIndex = 1;
            this.statusBarMain.Text = "Visual Rom Suite ... (Alpha)";
            // 
            // timerTabGroupLeaf
            // 
            this.timerTabGroupLeaf.Enabled = true;
            this.timerTabGroupLeaf.Interval = 200;
            this.timerTabGroupLeaf.Tick += new System.EventHandler(this.timerTabGroupLeaf_Tick);
            // 
            // toolbarManager
            // 
            this.toolbarManager.BottomContainer = this.bottomSandBarDock;
            this.toolbarManager.LeftContainer = this.leftSandBarDock;
            this.toolbarManager.OwnerForm = this;
            this.toolbarManager.RightContainer = this.rightSandBarDock;
            this.toolbarManager.TopContainer = this.topSandBarDock;
            // 
            // bottomSandBarDock
            // 
            this.bottomSandBarDock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomSandBarDock.Location = new System.Drawing.Point(0, 534);
            this.bottomSandBarDock.Manager = this.toolbarManager;
            this.bottomSandBarDock.Name = "bottomSandBarDock";
            this.bottomSandBarDock.Size = new System.Drawing.Size(904, 0);
            this.bottomSandBarDock.TabIndex = 31;
            // 
            // leftSandBarDock
            // 
            this.leftSandBarDock.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftSandBarDock.Location = new System.Drawing.Point(0, 27);
            this.leftSandBarDock.Manager = this.toolbarManager;
            this.leftSandBarDock.Name = "leftSandBarDock";
            this.leftSandBarDock.Size = new System.Drawing.Size(0, 485);
            this.leftSandBarDock.TabIndex = 30;
            // 
            // rightSandBarDock
            // 
            this.rightSandBarDock.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightSandBarDock.Location = new System.Drawing.Point(904, 27);
            this.rightSandBarDock.Manager = this.toolbarManager;
            this.rightSandBarDock.Name = "rightSandBarDock";
            this.rightSandBarDock.Size = new System.Drawing.Size(0, 485);
            this.rightSandBarDock.TabIndex = 32;
            // 
            // topSandBarDock
            // 
            this.topSandBarDock.Controls.Add(this.tbStandard);
            this.topSandBarDock.Controls.Add(this.tbProject);
            this.topSandBarDock.Dock = System.Windows.Forms.DockStyle.Top;
            this.topSandBarDock.Location = new System.Drawing.Point(0, 0);
            this.topSandBarDock.Manager = this.toolbarManager;
            this.topSandBarDock.Name = "topSandBarDock";
            this.topSandBarDock.Size = new System.Drawing.Size(904, 27);
            this.topSandBarDock.TabIndex = 33;
            // 
            // tbStandard
            // 
            this.tbStandard.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.tbbStandardNewProject,
            this.tbbStandardOpen,
            this.tbbStandardSave,
            this.tbbStandardSaveAll,
            this.tbbStandardCut,
            this.tbbStandardCopy,
            this.tbbStandardPaste});
            this.tbStandard.Closable = false;
            this.tbStandard.Guid = new System.Guid("edd62e66-cb13-4284-b24f-3d4a42551c86");
            this.tbStandard.ImageList = this.ImageListMain;
            this.tbStandard.Location = new System.Drawing.Point(2, 0);
            this.tbStandard.Name = "tbStandard";
            this.tbStandard.Size = new System.Drawing.Size(198, 27);
            this.tbStandard.TabIndex = 0;
            this.tbStandard.Text = "Standard";
            this.tbStandard.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.tbStandard_ButtonClick);
            // 
            // tbbStandardNewProject
            // 
            this.tbbStandardNewProject.BuddyMenu = null;
            this.tbbStandardNewProject.Icon = null;
            this.tbbStandardNewProject.ImageIndex = 0;
            // 
            // tbbStandardOpen
            // 
            this.tbbStandardOpen.BeginGroup = true;
            this.tbbStandardOpen.BuddyMenu = null;
            this.tbbStandardOpen.Icon = null;
            this.tbbStandardOpen.ImageIndex = 10;
            // 
            // tbbStandardSave
            // 
            this.tbbStandardSave.BuddyMenu = null;
            this.tbbStandardSave.Icon = null;
            this.tbbStandardSave.ImageIndex = 8;
            // 
            // tbbStandardSaveAll
            // 
            this.tbbStandardSaveAll.BuddyMenu = null;
            this.tbbStandardSaveAll.Icon = null;
            this.tbbStandardSaveAll.ImageIndex = 7;
            // 
            // tbbStandardCut
            // 
            this.tbbStandardCut.BeginGroup = true;
            this.tbbStandardCut.BuddyMenu = null;
            this.tbbStandardCut.Enabled = false;
            this.tbbStandardCut.Icon = null;
            this.tbbStandardCut.ImageIndex = 23;
            // 
            // tbbStandardCopy
            // 
            this.tbbStandardCopy.BuddyMenu = null;
            this.tbbStandardCopy.Enabled = false;
            this.tbbStandardCopy.Icon = null;
            this.tbbStandardCopy.ImageIndex = 21;
            // 
            // tbbStandardPaste
            // 
            this.tbbStandardPaste.BuddyMenu = null;
            this.tbbStandardPaste.Enabled = false;
            this.tbbStandardPaste.Icon = null;
            this.tbbStandardPaste.ImageIndex = 24;
            // 
            // tbProject
            // 
            this.tbProject.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.tbbProjectStartEmulator,
            this.tbbProjectComboEmulator,
            this.tbbProjectBuildProject});
            this.tbProject.DockOffset = 1;
            this.tbProject.Enabled = false;
            this.tbProject.Guid = new System.Guid("a18ef599-3e51-4310-9f3c-b2a2929aa982");
            this.tbProject.ImageList = this.ImageListMain;
            this.tbProject.Location = new System.Drawing.Point(202, 0);
            this.tbProject.Name = "tbProject";
            this.tbProject.Size = new System.Drawing.Size(159, 27);
            this.tbProject.TabIndex = 1;
            this.tbProject.Text = "";
            this.tbProject.Visible = false;
            this.tbProject.ButtonClick += new TD.SandBar.ToolBar.ButtonClickEventHandler(this.tbProject_ButtonClick);
            // 
            // tbbProjectStartEmulator
            // 
            this.tbbProjectStartEmulator.BuddyMenu = null;
            this.tbbProjectStartEmulator.Icon = null;
            this.tbbProjectStartEmulator.ImageIndex = 12;
            // 
            // tbbProjectComboEmulator
            // 
            this.tbbProjectComboEmulator.ControlWidth = 80;
            this.tbbProjectComboEmulator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbbProjectComboEmulator.Items.AddRange(new object[] {
            "Snes9x",
            "Zsnes"});
            this.tbbProjectComboEmulator.Padding.Left = 1;
            this.tbbProjectComboEmulator.Padding.Right = 1;
            // 
            // tbbProjectBuildProject
            // 
            this.tbbProjectBuildProject.BeginGroup = true;
            this.tbbProjectBuildProject.BuddyMenu = null;
            this.tbbProjectBuildProject.Icon = null;
            this.tbbProjectBuildProject.ImageIndex = 25;
            // 
            // menuBar1
            // 
            this.menuBar1.Buttons.AddRange(new TD.SandBar.ToolbarItemBase[] {
            this.menuBarItem1,
            this.menuBarItem2,
            this.menuBarItem3,
            this.menuBarItem4,
            this.menuBarItem5});
            this.menuBar1.Guid = new System.Guid("e4ee2650-d3af-4298-869c-dcc49502eaa7");
            this.menuBar1.ImageList = null;
            this.menuBar1.Location = new System.Drawing.Point(2, 0);
            this.menuBar1.Name = "menuBar1";
            this.menuBar1.Size = new System.Drawing.Size(904, 24);
            this.menuBar1.TabIndex = 1;
            // 
            // menuBarItem1
            // 
            this.menuBarItem1.Icon = null;
            this.menuBarItem1.Text = "&File";
            // 
            // menuBarItem2
            // 
            this.menuBarItem2.Icon = null;
            this.menuBarItem2.Text = "&Edit";
            // 
            // menuBarItem3
            // 
            this.menuBarItem3.Icon = null;
            this.menuBarItem3.Text = "&View";
            // 
            // menuBarItem4
            // 
            this.menuBarItem4.Icon = null;
            this.menuBarItem4.Text = "&Window";
            // 
            // menuBarItem5
            // 
            this.menuBarItem5.Icon = null;
            this.menuBarItem5.Text = "&Help";
            // 
            // tabGroup
            // 
            this.tabGroup.AllowDrop = true;
            this.tabGroup.AtLeastOneLeaf = true;
            this.tabGroup.CloseMenuText = "";
            this.tabGroup.CloseShortcut = System.Windows.Forms.Shortcut.None;
            this.tabGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGroup.Location = new System.Drawing.Point(0, 27);
            this.tabGroup.MoveNextMenuText = "";
            this.tabGroup.MoveNextShortcut = System.Windows.Forms.Shortcut.None;
            this.tabGroup.MovePreviousMenuText = "";
            this.tabGroup.MovePreviousShortcut = System.Windows.Forms.Shortcut.None;
            this.tabGroup.Name = "tabGroup";
            this.tabGroup.NewHorizontalMenuText = "";
            this.tabGroup.NewVerticalMenuText = "";
            this.tabGroup.ProminentLeaf = null;
            this.tabGroup.ProminentMenuText = "";
            this.tabGroup.ProminentShortcut = System.Windows.Forms.Shortcut.None;
            this.tabGroup.RebalanceMenuText = "";
            this.tabGroup.RebalanceShortcut = System.Windows.Forms.Shortcut.None;
            this.tabGroup.ResizeBarColor = System.Drawing.SystemColors.Control;
            this.tabGroup.Size = new System.Drawing.Size(904, 485);
            this.tabGroup.SplitHorizontalShortcut = System.Windows.Forms.Shortcut.None;
            this.tabGroup.SplitVerticalShortcut = System.Windows.Forms.Shortcut.None;
            this.tabGroup.TabIndex = 34;
            // 
            // toolBar1
            // 
            this.toolBar1.DockLine = 1;
            this.toolBar1.Guid = new System.Guid("ab1f641b-986e-4483-81fd-ae018c1387d7");
            this.toolBar1.ImageList = null;
            this.toolBar1.Location = new System.Drawing.Point(2, 26);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.Size = new System.Drawing.Size(23, 23);
            this.toolBar1.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(904, 534);
            this.Controls.Add(this.tabGroup);
            this.Controls.Add(this.leftSandBarDock);
            this.Controls.Add(this.rightSandBarDock);
            this.Controls.Add(this.topSandBarDock);
            this.Controls.Add(this.statusBarMain);
            this.Controls.Add(this.bottomSandBarDock);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(760, 544);
            this.Name = "FormMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormMain_Closing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.topSandBarDock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabGroup)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Menus Principal
		/// <summary>
		/// Créé les menus de l'application
		/// </summary>
		private MenuControl CreateMenu(){	
			
			//Creation de l'objet pour les menus
			MenuControl menuPrincipal = new MenuControl();

			//Propriétés du menu
			menuPrincipal.Style = this._Style; 
			menuPrincipal.Dock = DockStyle.Top;
			menuPrincipal.Animate = Animate.System;
			menuPrincipal.AutoScroll = true;
									
			//Ajouter les menus : Top Menu
			MenuCommand topFile = new MenuCommand(_res.GetString("MenuFichier"));
			MenuCommand topEdit = new MenuCommand(_res.GetString("MenuEdition"));
			MenuCommand topView = new MenuCommand(_res.GetString("MenuAffichage"));
			topBookMark = new MenuCommand(_res.GetString("MenuBookMark"));
			topProject = new MenuCommand(_res.GetString("MenuProjet"));
			topProject.Visible = false;
			topCompil = new MenuCommand(_res.GetString("MenuGénérer"));		
			topCompil.Visible = false;
			MenuCommand topTools = new MenuCommand(_res.GetString("MenuOutils"));
			MenuCommand topHelp = new MenuCommand(_res.GetString("MenuAides"));
			topBookMark = new MenuCommand(_res.GetString("MenuBookMark"));
			this.topBookMark.Visible = false;
			menuPrincipal.MenuCommands.AddRange(new MenuCommand[]{topFile, 
																	 topEdit, 
																	 topView, 
																	 topProject, 
																	 topCompil, 
																	 topBookMark,
																	 topTools, 
																	 topHelp});
			//Separateur de menu
			MenuCommand FileSeparator = new MenuCommand("-");

			#region Menu : Fichier
			//Nouveau
			MenuCommand FileNew = new MenuCommand(_res.GetString("MenuFichierNouveau"));
			MenuCommand FileNewProject = new MenuCommand(_res.GetString("MenuFichierNouveauProjet"), new EventHandler(File_NewProject));
			MenuCommand FileNewFile = new MenuCommand(_res.GetString("MenuFichierNouveauFichier"), new EventHandler(File_NewFile));
			FileNewProject.Shortcut = Shortcut.CtrlN;
			FileNewProject.ImageList = ImageListMain;
			FileNewProject.ImageIndex = 0;
			FileNewFile.ImageList = ImageListMain;
			FileNewFile.ImageIndex = 11;
			FileNewFile.Enabled= false;
			FileNew.MenuCommands.AddRange(new MenuCommand[]{FileNewProject, FileNewFile});

			//Ouvrir
			MenuCommand FileOpen = new MenuCommand(_res.GetString("MenuFichierOuvrir"));
			MenuCommand FileOpenProject = new MenuCommand(_res.GetString("MenuFichierOuvrirProjet"), new EventHandler(File_OpenProject));
			MenuCommand FileOpenFile = new MenuCommand(_res.GetString("MenuFichierOuvrirFichier"), new EventHandler(File_OpenFile));
			FileOpenProject.Shortcut = Shortcut.CtrlO;
			FileOpenProject.ImageList = ImageListMain;
			FileOpenProject.ImageIndex = 1;
			FileOpenFile.ImageList = ImageListMain;
			FileOpenFile.ImageIndex = 10;
			FileOpenFile.Enabled = false;
			FileOpen.MenuCommands.AddRange(new MenuCommand[]{FileOpenProject, FileOpenFile});
			
			MenuCommand FileClose = new MenuCommand(_res.GetString("MenuFichierFermer"), new EventHandler(File_Close));
			MenuCommand FileCloseAll = new MenuCommand(_res.GetString("MenuFichierFermerTous"), new EventHandler(File_CloseAll));
			FileCloseAll.ImageList = ImageListMain;
			FileCloseAll.ImageIndex = 9;

			MenuCommand FileSave = new MenuCommand(_res.GetString("MenuFichierEnregistrer"), new EventHandler(Save_File));
			MenuCommand FileSaveAs = new MenuCommand(_res.GetString("MenuFichierEnregistrerSous"), new EventHandler(Save_As));
			MenuCommand FileSaveAll = new MenuCommand(_res.GetString("MenuFichierEnregistrerTous"), new EventHandler(Save_All));
			FileSaveAll.Shortcut = Shortcut.CtrlShiftS;
			FileSaveAll.ImageList = ImageListMain;
			FileSaveAll.ImageIndex = 7;
			FileSave.Shortcut = Shortcut.CtrlS;
			FileSave.ImageList = ImageListMain;
			FileSave.ImageIndex = 8;
			
			MenuCommand FileQuit = new MenuCommand(_res.GetString("MenuFichierQuitter"), new EventHandler(File_Quit));
			topFile.MenuCommands.AddRange(new MenuCommand[]{FileNew, FileOpen, FileSeparator, FileClose, FileCloseAll, 
															   FileSeparator, FileSave, FileSaveAs,
															   FileSaveAll, FileSeparator, FileQuit});
			#endregion

			#region Menu : Affichage
			//Creation des menu
			MenuCommand ViewExplorer = new MenuCommand(_res.GetString("MenuAffichageExplorateurProjet"), ImageListMain, 5, new EventHandler(View_Explorer));
			MenuCommand ViewProperty = new MenuCommand(_res.GetString("MenuAffichagePropriétés"), ImageListMain, 2 ,new EventHandler(View_Property));
			MenuCommand ViewOutput = new MenuCommand(_res.GetString("MenuAffichageSortie"), ImageListMain, 4 ,new EventHandler(View_AppOutput));
			MenuCommand ViewHexaFileShow = new MenuCommand(_res.GetString("MenuAffichageHexaFileShow"),ImageListMain, 3, new EventHandler(View_HexaFileShow));
			MenuCommand ViewTaskList = new MenuCommand(_res.GetString("MenuAffichageTaskList"), ImageListMain, 6, new EventHandler(View_TaskList));
			
			//Menu pour retablit l'interface par defaut
			MenuCommand ViewDefaultUI = new MenuCommand(_res.GetString("MenuAffichageDefaultInterface"), ImageListMain, 19, new EventHandler(View_DefaultUI));

			//Configuration des menus
			ViewExplorer.Shortcut = Shortcut.F5;
			ViewOutput.Shortcut = Shortcut.CtrlShiftO;	
			ViewProperty.Shortcut = Shortcut.F4;
			ViewHexaFileShow.Shortcut = Shortcut.CtrlShiftH;
			ViewTaskList.Shortcut = Shortcut.CtrlShiftK;

			topView.MenuCommands.AddRange(new MenuCommand[]{ViewExplorer, 
															   ViewProperty, 
															   ViewOutput,
															   ViewTaskList,
															   ViewHexaFileShow, 
															   FileSeparator, 
															   ViewDefaultUI});
			#endregion

			#region Menu : Generer (Build)
			//Creation des menu			
			MenuCommand BuildProjectGeneration = new MenuCommand(_res.GetString("popExplorer_Build"), ImageListMain, 18, new EventHandler(OnBuildProject));
			MenuCommand BuildCleenProject = new MenuCommand(_res.GetString("popExplorer_CleenProject"), new EventHandler(OnCleenProject));
			MenuCommand BuildOptions = new MenuCommand(_res.GetString("BuildOptions"), new EventHandler(Build_Options));
			
			//Configuration des menus
			BuildProjectGeneration.Shortcut = Shortcut.CtrlShiftB;

			topCompil.MenuCommands.AddRange(new MenuCommand[]{BuildProjectGeneration, 
																 BuildCleenProject,
																 FileSeparator, 
																 BuildOptions});
			#endregion
	
			#region Menu : Aide
			MenuCommand HelpAbout = new MenuCommand(_res.GetString("MenuHelpAbout"), new EventHandler(Help_About));
			topHelp.MenuCommands.AddRange(new MenuCommand[]{HelpAbout});
			#endregion

            #region Menu : Projet
            MenuCommand ProjectAddTBL = new MenuCommand(_res.GetString("MenuProjectAddTBL"), new EventHandler(MenuProject_AddTBL));
            //MenuCommand ToolsOptionsVRSThingy = new MenuCommand(_res.GetString("MenuToolsOptionsVRSThingy"), new EventHandler(Tools_OptionsVRSThingy));
            topProject.MenuCommands.AddRange(new MenuCommand[] { ProjectAddTBL});
            #endregion

			#region Menu : Outils
			MenuCommand ToolsOptions = new MenuCommand(_res.GetString("MenuToolsOptions"), new EventHandler(Tools_Options));
            MenuCommand ToolsOptionsVRSThingy = new MenuCommand(_res.GetString("MenuToolsOptionsVRSThingy"), new EventHandler(Tools_OptionsVRSThingy));
            topTools.MenuCommands.AddRange(new MenuCommand[] { ToolsOptionsVRSThingy, FileSeparator, ToolsOptions });
			#endregion
			

			//Ajouter au formulaire le menu
			Controls.Add(menuPrincipal);

			//Valeur de retour
			return menuPrincipal;
		}

		public void File_Quit(object sender, EventArgs e) {
			//Quiter Application
			Application.Exit();
		}

		public void File_NewProject(object sender, EventArgs e) {
			new FormNouveauProjet(this._res, this).ShowDialog(this);
		}

		public void File_NewFile(object sender, EventArgs e) {
			
		}

        public void MenuProject_AddTBL(object sender, EventArgs e)
        {
            OpenFileDialog OpenTBL = new OpenFileDialog();
            OpenTBL.Title = _res.GetString("OpenDialog_AddTBLTitle");
            OpenTBL.Filter = _res.GetString("OpenDialog_OpenTBLFilter");
            OpenTBL.CheckFileExists = true;
            OpenTBL.CheckPathExists = true;
            OpenTBL.ShowDialog(this);

            if (OpenTBL.FileName != "") {
                this._ProjetExplorer.Projet.AjouterTBL(OpenTBL.FileName);
                this._ProjetExplorer.RefreshExplorer();
            }
		}

		public void BookMark_Click(object sender, EventArgs e) {
			//Recherche du bookmark dans le projet
			MenuCommand mc = sender as MenuCommand; 

			if (this._ProjetExplorer.Projet != null){
				Project prj = this._ProjetExplorer.Projet;
				Favoris fav = null;
				for (int i=0; i< prj.Favoris.Count; i++){
					fav = (Favoris)prj.Favoris[i];
					if (fav.Key == mc.Tag.ToString()){
						prjHexaFileShow.BringToFront();
						this._DockManager.ShowContent(prjHexaFileShow);
						this._HexaFileShow.Position = fav.Position;						
						break;
					}
				}
			}
		}
		
		public void View_AppOutput(object sender, EventArgs e) {
			prjOut.BringToFront();
			this._DockManager.ShowContent(prjOut);
		}

		public void View_TaskList(object sender, EventArgs e) {
			prjTaskList.BringToFront();
			this._DockManager.ShowContent(prjTaskList);
		}

		public void View_HexaFileShow(object sender, EventArgs e) {
			prjHexaFileShow.BringToFront();
			this._DockManager.ShowContent(prjHexaFileShow);
			this._HexaFileShow.RefreshData();
		}

		public void Tools_Options(object sender, EventArgs e) {
            new FormOptions(this._res, this).ShowDialog(this);
		}

        public void Tools_OptionsVRSThingy(object sender, EventArgs e)
        {
            //Start info
            ProcessStartInfo si = new ProcessStartInfo("VRSThingy.exe");
            Process vrsthingy;
            if (si != null)
            {
                si.WindowStyle = ProcessWindowStyle.Normal;
                //si.Arguments 
                si.CreateNoWindow = false;
                si.ErrorDialogParentHandle = this.Handle;
                si.ErrorDialog = true;
            }

            //Demare l'emulateur
            vrsthingy = new Process();
            vrsthingy.StartInfo = si;
            vrsthingy.Start();
		}

		public void Help_About(object sender, EventArgs e) {
			new AboutBox(this.Handle.ToInt32(), this.Icon.Handle.ToInt32(), App.Name, App.VersionInfo.FileVersion).Show();
		}

		public void File_OpenProject(object sender, EventArgs e) {
			//Ouvrir un projet
			this.OpenProject();
		}

		public void File_OpenFile(object sender, EventArgs e) {

		}

		public void View_DefaultUI(object sender, EventArgs e) {
			LoadDefaultUIConfig();
		}

		/// <summary>
		/// Fermer le fichier courrant
		/// </summary>		
		public void File_Close(object sender, EventArgs e) {
			try{
				Crownwood.Magic.Controls.TabControl tc = null;

				if (tabGroup.ActiveLeaf != null)
					tc = tabGroup.ActiveLeaf.GroupControl as Crownwood.Magic.Controls.TabControl;
            				
				if (tc != null) {
					// Esce qu'il y a un tab de selectioner ?
					if (tc.SelectedTab != null) {
						// Detruire la tab
						tc.TabPages.Remove(tc.SelectedTab);
					}
				}	
			}catch{}
		}

		/// <summary>
		/// Fermer tous les fichiers et le projet
		/// </summary>		
		public void File_CloseAll(object sender, EventArgs e) {
			//Fermer tous les modules ouvert
			//TODO: Demander pour enregistrer les fichiers
			try{
				//Suprimer les favoris
				CleenBookMark();

				//cache les menus
				topProject.Visible = false;
				topCompil.Visible  = false;
				
				//desactive les barres d'outils
				tbProject.Enabled = false;
				tbProject.Visible = false;
				
				//Ferme le projet
				this._ProjetExplorer.CloseProject();
				this._PropertyObject.SelectedObject = null;
				this._HexaFileShow.CloseFile();
				//TODO: inviter a enregistrer les fichiers
				this.tabGroup.ActiveLeaf.TabPages.Clear();
			}catch{}
		}

		public void Save_All(object sender, EventArgs e) {
			//TODO: A finir cette fonction pour le moment elle enregistre seulement le projet ouvert
			if (this._ProjetExplorer.Projet != null)
				this._ProjetExplorer.Projet.Save();
		}

		/// <summary>
		/// Enregistrer le fichier ouvert
		/// </summary>		
		public void Save_File(object sender, EventArgs e) {
			try{
				// Créé un access a la premiere page du group
				TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;

				switch (GetEditorType()){
					case ModuleType.TBLEditor:
						((EditeurTBL)tgl.TabPages[0].Control).Save();
						AddOutPut(this._res.GetString("FileSaveMSG") + " : '" + tgl.TabPages[0].Title + "'");
						break;
					case ModuleType.HexaEditor:
						((HexaEditor)tgl.TabPages[0].Control).Save();
						AddOutPut(this._res.GetString("FileSaveMSG") + " : '" + tgl.TabPages[0].Title + "'");
						break;
				}
			}
			catch(Exception exp){
				AddOutPut("Erreur: " + exp.Message);				
			}
		}

		public void BookMarkGestionnaire_Click(object sender, EventArgs e) {
			//Chargement du gestionnaire
			new FormBookMarkManager(this._res, this).ShowDialog(); 
		}

		public void Save_As(object sender, EventArgs e) {
			
		}

		/// <summary>
		/// Afficher l'explorateur de fichier
		/// </summary>		
		public void View_Explorer(object sender, EventArgs e) {
			prjExplorer.BringToFront();
			this._DockManager.ShowContent(prjExplorer);		
		}

		/// <summary>
		/// Afficher les propriétés
		/// </summary>
		public void View_Property(object sender, EventArgs e) {
			prjProperty.BringToFront();
			this._DockManager.ShowContent(prjProperty);
			
		}
		#endregion

		#region Dockings
		/// <summary>
		/// Creation des formulaires docking
		/// </summary>
		private void CreateDockingForm(){
			// creation de l'instance du docking manager
			this._DockManager = new DockingManager(this, this._Style);

			// Dedini les inner/outer object pour des operation correct sur les docking
			this._DockManager.InnerControl = this.tabGroup;
			this._DockManager.OuterControl = this.statusBarMain;

			//Creation des objets de l'interface 
			this._ProjetExplorer = new ProjectExplorer();
			this._PropertyObject = new PropertyGrid();
			this._HexaFileShow = new HexaFileShow();
			this._AppOut = new TextEditor();
			this._TaskList = new TaskList();
 
			//creation des boite contenant les objet a afficher
			prjExplorer		= this._DockManager.Contents.Add(this._ProjetExplorer, _res.GetString("DockingExplorateurProjet"), ImageListMain, 5);			 
			prjProperty		= this._DockManager.Contents.Add(this._PropertyObject, _res.GetString("DockingPropriétés"), ImageListMain, 2);
			prjHexaFileShow = this._DockManager.Contents.Add(this._HexaFileShow, _res.GetString("DockingHexaFileShow"), ImageListMain, 3);
			prjOut			= this._DockManager.Contents.Add(this._AppOut, _res.GetString("DockingOut"), ImageListMain, 4);
			prjTaskList		= this._DockManager.Contents.Add(this._TaskList, _res.GetString("DockingTaskList"), ImageListMain, 6);

			//Initialise la taille des objets
			prjExplorer.DisplaySize = new Size(250, 200);
			prjProperty.DisplaySize = new Size(250, 200);			
			prjHexaFileShow.DisplaySize = new Size(300, 250);
			prjOut.DisplaySize = new Size(300, 250);
			prjTaskList.DisplaySize = new Size(300, 200);

			//Configure HexaFileShow
			this._HexaFileShow.RightClick += new MouseEventHandler(HexaFileShow_RightClick); 

			//Configure la Propertygrid
			this._PropertyObject.PropertyValueChanged += new PropertyValueChangedEventHandler(PropertyGrid_ValueChanged);
 
			//Configure la sorti du logiciel
			this._AppOut.Locked = true;
			this._AppOut.VerticalScrollVisible = true;
			this._AppOut.HorizontalScrollVisible = false;
			this._AppOut.TEBorderStyle = TextEditorBorder.Thin;
			
			//Configure l'explorateur de projet
			this._ProjetExplorer.StringHexa   = _res.GetString("ProjectExplorer_FichierHexa");
			this._ProjetExplorer.StringProjet = _res.GetString("ProjectExplorer_Project");
			this._ProjetExplorer.StringTable  = _res.GetString("ProjectExplorer_FichierTBL");
			this._ProjetExplorer.StringText   = _res.GetString("ProjectExplorer_FichierTexte");
			this._ProjetExplorer.ExplorerDoubleClick += new EventHandler(ProjectExplorer_DoubleClick);  
			this._ProjetExplorer.ExplorerClick += new EventHandler(ProjectExplorer_Click);  
			this._ProjetExplorer.RightClick += new MouseEventHandler(ProjectExplorer_RightClick); 
			this._ProjetExplorer.SelectionChange += new TreeViewEventHandler(ProjectExplorer_SelectionChange); 

			//Bind les objets sur la form
			WindowContent wcRight = this._DockManager.AddContentWithState(prjExplorer, State.DockRight);
			this._DockManager.AddContentToZone(prjProperty, wcRight.ParentZone, 1);
			WindowContent wcBottom = this._DockManager.AddContentWithState(prjHexaFileShow, State.DockBottom);
			wcBottom.Contents.Add(prjOut);
			wcBottom.Contents.Add(prjTaskList);
		}		
		#endregion

		#region Modules
		protected void CreationEditeurTexte(string FileName) {        
			// Créé un access a la premiere page page du group
			TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;
            
			//Creation de l<editeur de texte
			TextEditor textEdit = new TextEditor();

			// Cree la page
			Crownwood.Magic.Controls.TabPage tp1 = 
				new Crownwood.Magic.Controls.TabPage(Path.GetFileName(FileName), textEdit, this._ProjetExplorer.ListImageTreeView, 3);
			            
			tp1.Tag = ModuleType.TexteEditor;
			tp1.Selected = true;

			// ajouter la page au groupe
			tgl.TabPages.Add(tp1);

			if (File.Exists(FileName))
				textEdit.LoadFile(FileName); 

		}

		protected void CreationTextExtractor() {
			// Créé un access a la premiere page page du group
			TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;
			bool exist = false;

			//Verifie que l'extracteur n'est pas deja ouvert
			foreach (Crownwood.Magic.Controls.TabPage page in tgl.TabPages){				
				if ((ModuleType)page.Tag == ModuleType.Extractor){
					exist = true;
					page.Selected = true;
				}
			}

			//Creation de l'extracteur de textes si il n'existe pas
			if (!exist){				           
				//Creation de l'extracteur de texte
				this._TextExtractor = new TextExtractor();

				// Cree la page
				Crownwood.Magic.Controls.TabPage tp1 = 
					new Crownwood.Magic.Controls.TabPage(this._res.GetString("TextExtractorTitle"), this._TextExtractor, this.ImageListMain, 13);

				tp1.Selected = true;
				tp1.Tag = ModuleType.Extractor;
						            
				// ajouter la page au groupe
				tgl.TabPages.Add(tp1);
			}
		}

		protected void CreationStringSearch() {
			// Créé un access a la premiere page page du group
			TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;
			bool exist = false;

			//Verifie que l'extracteur n'est pas deja ouvert
			foreach (Crownwood.Magic.Controls.TabPage page in tgl.TabPages){
				if ((ModuleType)page.Tag == ModuleType.StringSearch){
					exist = true;
					page.Selected = true;
				}
			}

			//Creation de l'extracteur de textes si il n'existe pas
			if (!exist){				           
				//Creation de l'extracteur de texte
				this._StringSearch = new StringFinder();
				this._StringSearch.Projet = this._ProjetExplorer.Projet;
				this._StringSearch.FileName = this._ProjetExplorer.Projet.ProjectPath + 
					Path.DirectorySeparatorChar + this._ProjetExplorer.Projet.RomFile; 

				//Strings (localisation)
				this._StringSearch.String_TextToFind   = this._res.GetString("StringFinder_TextToFind"); 
				this._StringSearch.String_NoItemFound  = this._res.GetString("StringFinder_NoStringFound"); 
				this._StringSearch.String_ColValue	   = this._res.GetString("StringFinder_ColValue"); 
				this._StringSearch.String_ColPosition  = this._res.GetString("StringFinder_ColPosition"); 
				this._StringSearch.String_TBL		   = this._res.GetString("StringFinder_TBL"); 
				this._StringSearch.String_Quality	   = this._res.GetString("StringFinder_Quality"); 
				this._StringSearch.String_MinLenght	   = this._res.GetString("StringFinder_MinLenght");
				this._StringSearch.String_MatchCase	   = this._res.GetString("StringFinder_MatchCase");
				this._StringSearch.String_Range		   = this._res.GetString("StringFinder_Range");
				this._StringSearch.String_Best		   = this._res.GetString("StringFinder_Best");
				this._StringSearch.String_Low		   = this._res.GetString("StringFinder_Low");
				this._StringSearch.String_Small		   = this._res.GetString("StringFinder_Small");
				this._StringSearch.String_Huge		   = this._res.GetString("StringFinder_Huge");
				this._StringSearch.String_TotalItem	   = this._res.GetString("StringFinder_TotalItem");
				this._StringSearch.String_GradLabelParam  = this._res.GetString("StringFinder_GradLabelParam");

				// Cree la page
				Crownwood.Magic.Controls.TabPage tp1 = 
					new Crownwood.Magic.Controls.TabPage(this._res.GetString("StringSearchTitle"), this._StringSearch, this.ImageListMain, 14);

				tp1.Tag = ModuleType.StringSearch;

				tp1.Selected = true;
						            
				// ajouter la page au groupe
				tgl.TabPages.Add(tp1);
			}
		}

		protected void CreationEditeurHexa(string FileName) {        
			// Créé un access a la premiere page page du group
			TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;
            
			//Creation de l<editeur de texte
			HexaEditor HexEdit = new HexaEditor();

			// Cree la page
			Crownwood.Magic.Controls.TabPage tp1 = 
				new Crownwood.Magic.Controls.TabPage(Path.GetFileName(FileName), HexEdit, this._ProjetExplorer.ListImageTreeView, 5);
			            
			tp1.Tag = ModuleType.HexaEditor;

			tp1.Selected = true;

			// ajouter la page au groupe
			tgl.TabPages.Add(tp1);

			if (File.Exists(FileName))
				HexEdit.LoadFile(FileName); 

		}

		protected void CreationEditeurFixe() {        
			// Créé un access a la premiere page page du group
			TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;
            
			//Creation de l<editeur de texte
			TableauFixeEditor FixeEdit = new TableauFixeEditor();

			// Cree la page
			Crownwood.Magic.Controls.TabPage tp1 = 
				new Crownwood.Magic.Controls.TabPage("FixeEdit", FixeEdit, this._ProjetExplorer.ListImageTreeView, 8);
			            
			tp1.Tag = ModuleType.FixeEditor;

			tp1.Selected = true;

			// ajouter la page au groupe
			tgl.TabPages.Add(tp1);
		}

		protected void CreationEditeurTBL(string FileName) {        
			// Créé un access a la premiere page page du group
			TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;
            
			//Creation de l<editeur de texte
			VRS.Library.UserControl.TableEditor.EditeurTBL TBLEdit = new VRS.Library.UserControl.TableEditor.EditeurTBL();

			// Cree la page
			Crownwood.Magic.Controls.TabPage tp1 = 
				new Crownwood.Magic.Controls.TabPage(Path.GetFileName(FileName), TBLEdit, this._ProjetExplorer.ListImageTreeView, 4);

			tp1.Tag = ModuleType.TBLEditor;
            
			tp1.Selected = true;

			// ajouter la page au groupe
			tgl.TabPages.Add(tp1);			

			if (File.Exists(FileName))
				TBLEdit.LoadFile(FileName); 

		}

//		protected void CreationEditeurTBL(string FileName) {        
//			// Créé un access a la premiere page page du group
//			TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;
//            
//			//Creation de l<editeur de texte
//			TBLEditor TBLEdit = new TBLEditor();
//
//			// Cree la page
//			Crownwood.Magic.Controls.TabPage tp1 = 
//				new Crownwood.Magic.Controls.TabPage(Path.GetFileName(FileName), TBLEdit, this._ProjetExplorer.ListImageTreeView, 4);
//
//			tp1.Tag = ModuleType.TBLEditor;
//            
//			tp1.Selected = true;
//
//			// ajouter la page au groupe
//			tgl.TabPages.Add(tp1);			
//
//			if (File.Exists(FileName))
//				TBLEdit.LoadFile(FileName); 
//
//		}
		#endregion

		#region Explorateur de projet
		private void OpenRom(){			
			string path = this._ProjetExplorer.Projet.ProjectPath +
				Path.DirectorySeparatorChar +
				this._ProjetExplorer.Projet.RomFile;
			CreationEditeurHexa(path);
		}

		private void OpenRomWork(){
			string path = this._ProjetExplorer.Projet.ProjectPath +
				Path.DirectorySeparatorChar +
				this._ProjetExplorer.Projet.WorkRomFile;
			CreationEditeurHexa(path);
		}

		private void ProjectExplorer_SelectionChange(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			try{
				if (this._ProjetExplorer.SelectedItemType != ExplorerNodeType.Nothing){
					object obj = this._ProjetExplorer.GetSelectedObject();
					if (obj != null)
						this._PropertyObject.SelectedObject = obj; 
				}else
					this._PropertyObject.SelectedObject = null;
			}catch{
				this._PropertyObject.SelectedObject = null;
			}
		}

		public void ProjectExplorer_RightClick(object sender, MouseEventArgs e){
			// Create the popup menu object
			PopupMenu popup = new PopupMenu();

			// apparence du menu
			popup.Style = this._Style;

			//Suffixe pour les resouces
			const string suffix = "popExplorer_";

			//Separateur de menu
			MenuCommand MenuSeparator = new MenuCommand("-");

			//Evenement qui se produit quand la souris passe au decu du menu
			popup.Selected += new CommandHandler(PopExplorer_OnSelected);
			popup.Deselected += new CommandHandler(PopExplorer_OnDeselected);
			
			//Creation des objet qui sont commun a plusieur
			MenuCommand popOpen = new MenuCommand(_res.GetString(suffix + "Open"), this.ImageListMain, 10,  new EventHandler(PopExplorer_Open));					
			MenuCommand popOpenWith = new MenuCommand(_res.GetString(suffix + "OpenWith"), new EventHandler(PopExplorer_OpenWith));
			MenuCommand popRomExecute = new MenuCommand(_res.GetString(suffix + "Executer"), this.ImageListMain, 12,  new EventHandler(PopExplorer_Start));
			MenuCommand popProperty = new MenuCommand(_res.GetString(suffix + "Property"), this.ImageListMain, 2,  new EventHandler(PopExplorer_Property));
			MenuCommand popDelete = new MenuCommand(_res.GetString(suffix + "Delete"), this.ImageListMain, 17,  new EventHandler(PopExplorer_Delete));
			MenuCommand popValidFile = new MenuCommand(_res.GetString(suffix + "ValidFile"), new EventHandler(PopExplorer_ValidFile));
			MenuCommand popInsert = new MenuCommand(_res.GetString(suffix + "Insert"), new EventHandler(PopExplorer_Insert));

			//Configuration des menus
			popOpenWith.Enabled = false;

			//Creation et affichage du menu
			switch (this._ProjetExplorer.SelectedItemType){
				case ExplorerNodeType.Rom:
					#region PopupMenu : ROM
					//Cree le menu
					MenuCommand popRomExtractText = new MenuCommand(_res.GetString(suffix + "ExtractText"), this.ImageListMain, 13,  new EventHandler(PopExplorer_ExtractText));
					MenuCommand popRomSearchString = new MenuCommand(_res.GetString(suffix + "SearchString"), this.ImageListMain, 14,  new EventHandler(PopExplorer_SearchString));
					
					// defini la liste des menus
					popup.MenuCommands.AddRange(new MenuCommand[]{popOpen, popOpenWith, MenuSeparator, 
																	 popRomExecute, MenuSeparator,
																	 popRomExtractText, popRomSearchString, MenuSeparator, 
																	 popProperty});
					#endregion
					break;
				case ExplorerNodeType.WorkRom:
					#region PopupMenu : WorkRom
					//Cree le menu					
					MenuCommand popRomCreateNewFixeTable = new MenuCommand(_res.GetString(suffix + "NewFixeTable"), this.ImageListMain, 15,  new EventHandler(PopExplorer_NewFixeTable));
					MenuCommand popRomCreateSavePoint = new MenuCommand(_res.GetString(suffix + "NewSavePoint"), this.ImageListMain, 16,  new EventHandler(PopExplorer_NewSavePoint));
															
					// defini la liste des menus
					popup.MenuCommands.AddRange(new MenuCommand[]{popOpen, popOpenWith, MenuSeparator, 
																	 popRomExecute, MenuSeparator, 
																	 popRomCreateNewFixeTable, popRomCreateSavePoint, MenuSeparator, 
																	 popProperty});
					#endregion
					break;
				case ExplorerNodeType.TextFile:
					#region PopupMenu : TextFile
					//Cree le menu					
					MenuCommand popReinitializeToZero = new MenuCommand(_res.GetString(suffix + "ReinitializeToZero"), new EventHandler(PopExplorer_ReinitializeToZero));
					MenuCommand popOptimizeDTE = new MenuCommand(_res.GetString(suffix + "OptimizeDTE"), new EventHandler(PopExplorer_OptimizeDTE));

					// defini la liste des menus 
					popup.MenuCommands.AddRange(new MenuCommand[]{popOpen, popOpenWith, MenuSeparator, 
																	 popReinitializeToZero, popOptimizeDTE, popInsert, popValidFile, MenuSeparator, 
																	 popDelete, MenuSeparator,
																	 popProperty});
					#endregion
					break;
				case ExplorerNodeType.Root: //Project
					#region PopupMenu : Projet
					//Cree le menu
					MenuCommand popSaveProject = new MenuCommand(_res.GetString(suffix + "SaveProject"), ImageListMain, 8, new EventHandler(PopExplorer_SaveProject));
					MenuCommand popBuild = new MenuCommand(_res.GetString(suffix + "Build"), ImageListMain, 18, new EventHandler(OnBuildProject));
					MenuCommand popAddFile = new MenuCommand(_res.GetString(suffix + "AddFile"), new EventHandler(PopExplorer_AddFile));
					MenuCommand popCleenProject = new MenuCommand(_res.GetString(suffix + "CleenProject"), new EventHandler(OnCleenProject));
					MenuCommand popReinitializeProject = new MenuCommand(_res.GetString(suffix + "ReinitializeProject"), new EventHandler(PopExplorer_ReinitializeProject));
					MenuCommand popCloseProject = new MenuCommand(_res.GetString(suffix + "CloseProject"), ImageListMain, 9, new EventHandler(PopExplorer_CloseProject));

					// defini la liste des menus 
					popup.MenuCommands.AddRange(new MenuCommand[]{popBuild, popCleenProject, popReinitializeProject, MenuSeparator,
																	 popAddFile, MenuSeparator,
																	 popSaveProject, popCloseProject, MenuSeparator,																	 
																	 popProperty});
					#endregion
					break;
				case ExplorerNodeType.TBLFile:
					#region PopupMenu : TBL
					MenuCommand popSetAsDefaultTBL = new MenuCommand(_res.GetString(suffix + "SetAsDefaultTBL"), new EventHandler(PopExplorer_SetAsDefaultTBL));

					// defini la liste des menus 
					popup.MenuCommands.AddRange(new MenuCommand[]{popOpen, popOpenWith, MenuSeparator, 
																	 popSetAsDefaultTBL, popValidFile, MenuSeparator, 
																	 popDelete, MenuSeparator,
																	 popProperty});
					#endregion
					break;
				case ExplorerNodeType.HexaFile:
					#region PopupMenu : HexaFile
					popup.MenuCommands.AddRange(new MenuCommand[]{popOpen, popOpenWith, MenuSeparator, 
																	 popInsert, MenuSeparator, popProperty});
 
					#endregion
					break;
			}

			if (popup.MenuCommands.Count > 0)
				popup.TrackPopup(this._ProjetExplorer.PointToScreen(new Point(e.X, e.Y + 15)));			
		}

		/// <summary>
		/// Ce produit quand la souris passe au decu du menu
		/// </summary>
		public void PopExplorer_OnSelected(MenuCommand mc) {
			//this._AppOut.AddLine("Enter : " + mc.Text);
		}

		/// <summary>
		/// Ce produit quand la souris quite le menu
		/// </summary>		
		public void PopExplorer_OnDeselected(MenuCommand mc) {
			//this._AppOut.AddLine("Quit : " + mc.Text);
		}


		/// <summary>
		/// Click sur le menu Open
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void PopExplorer_Open(object sender, EventArgs e) {
			string path;
			
			switch (this._ProjetExplorer.SelectedItemType){
				case ExplorerNodeType.Rom: //Fichier original
					OpenRom();
					break;
				case ExplorerNodeType.WorkRom: //Fichier de travaille
					OpenRomWork();
					break;
				case ExplorerNodeType.TBLFile: //Fichier TBL
					
					path = ((TBLStream)this._ProjetExplorer.GetSelectedObject()).FileName;
					CreationEditeurTBL(path);
					break;	
			}
		}

		public void PopExplorer_OpenWith(object sender, EventArgs e) {

		}

		public void PopExplorer_SetAsDefaultTBL(object sender, EventArgs e) {
			TBLStream tbl = (TBLStream)this._ProjetExplorer.GetSelectedObject(); 

			this._ProjetExplorer.Projet.SetDefaultTBL(tbl.key);	
	
			//Met en gras(bold) la bonne tbl
			this._ProjetExplorer.SetBoldSelectedTBL();
		}

		public void PopExplorer_ValidFile(object sender, EventArgs e) {

		}

		public void PopExplorer_SaveProject(object sender, EventArgs e) {

		}

		public void PopExplorer_AddFile(object sender, EventArgs e) {

		}

		public void PopExplorer_ReinitializeProject(object sender, EventArgs e) {

		}

		public void PopExplorer_CloseProject(object sender, EventArgs e) {

		}

		public void PopExplorer_Delete(object sender, EventArgs e) {
            if (MessageBox.Show(this, this._res.GetString("ConfirmationSuppressionTBL"), this._res.GetString("AppName"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                this._ProjetExplorer.Projet.DeleteTBL(((TBLStream)_ProjetExplorer.GetSelectedObject()).key);
                this._ProjetExplorer.RefreshExplorer();
            }
		}

		public void PopExplorer_Insert(object sender, EventArgs e) {
			
		}

		public void PopExplorer_ReinitializeToZero(object sender, EventArgs e) {
			
		}

		public void PopExplorer_OptimizeDTE(object sender, EventArgs e) {
			
		}

		public void PopExplorer_Property(object sender, EventArgs e) {
			prjProperty.BringToFront();
			this._DockManager.ShowContent(prjProperty);		
		}

		public void PopExplorer_Start(object sender, EventArgs e) {
			if (this._ProjetExplorer.Projet != null){		
				Project prj = this._ProjetExplorer.Projet;

				/*if (this._Emulator != null)
					if (!this._Emulator.HasExited){
						
						if (MessageBox.Show("ddd") == DialogResult.Yes)
							this._Emulator.Kill(); 
					}
				*/
				switch (this._ProjetExplorer.SelectedItemType){
					case ExplorerNodeType.Rom: 
						LaunchEmulator(prj.ProjectPath + Path.DirectorySeparatorChar + prj.RomFile);
						break;
					case ExplorerNodeType.WorkRom: 
						LaunchEmulator(prj.ProjectPath + Path.DirectorySeparatorChar + prj.WorkRomFile);
						break;
				}				
					
			}
			
		}

		public void PopExplorer_SearchString(object sender, EventArgs e) {
			CreationStringSearch();
		}

		public void PopExplorer_ExtractText(object sender, EventArgs e) {
			CreationTextExtractor();
		}

		public void PopExplorer_NewFixeTable(object sender, EventArgs e) {
			CreationEditeurFixe();
		}

		public void PopExplorer_NewSavePoint(object sender, EventArgs e) {
			
		}
		/// <summary>
		/// Ce produit quand un double click a été effectué sur l'explorateur de projet
		/// </summary>		
		public void ProjectExplorer_DoubleClick(object sender, EventArgs e) {
			string path = "";

			switch (this._ProjetExplorer.SelectedItemType) {
				case ExplorerNodeType.HexaFile:
					path = this._ProjetExplorer.Projet.ProjectPath +
						Path.DirectorySeparatorChar +
						((HexaSnapShot)this._ProjetExplorer.GetSelectedObject()).RelativePath;
					CreationEditeurHexa(path);					
					break;
				case ExplorerNodeType.TextFile:
					path = this._ProjetExplorer.Projet.ProjectPath +
						Path.DirectorySeparatorChar +
						((TextFile)this._ProjetExplorer.GetSelectedObject()).RelativePath;
					CreationEditeurTexte(path);
					break;
				case ExplorerNodeType.TBLFile:
					path = ((TBLStream)this._ProjetExplorer.GetSelectedObject()).FileName;
					CreationEditeurTBL(path);
					break;	
				case ExplorerNodeType.Rom:
					OpenRom();
					break;
				case ExplorerNodeType.WorkRom:
					OpenRomWork();
					break;
			}
		}

		/// <summary>
		/// Ce produit quand un double click a été effectué sur l'explorateur de projet
		/// </summary>		
		public void ProjectExplorer_Click(object sender, EventArgs e) {

		}		
		#endregion

		#region Fonctions Diverse
		/// <summary>
		/// Ajouter du texte a la sortie du logiciel
		/// </summary>
		/// <param name="Text">Texte a afficher</param>
		public void AddOutPut(string Text){
			this._AppOut.AddLine(Text);
		}

		/// <summary>
		/// Donne le type de l'editeur présentement sélectionné
		/// </summary>
		public ModuleType GetEditorType(){
			try{
				// Créé un access a la premiere page du group
				TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;

				return (ModuleType)tgl.TabPages[0].Tag;
			}catch{
				return ModuleType.Nothing;
			}
		}

		/// <summary>
		/// Chargement des texte de menu pour le tabgroup
		/// </summary>
		private void LoadTabGroupLanguage(){
            tabGroup.CloseMenuText = this._res.GetString("TabGroupClose");
			tabGroup.MoveNextMenuText = this._res.GetString("TabGroupMoveNext");
			tabGroup.MovePreviousMenuText = this._res.GetString("TabGroupMovePrevious");
			tabGroup.NewHorizontalMenuText = this._res.GetString("TabGroupNewHorizontal");
			tabGroup.NewVerticalMenuText = this._res.GetString("TabGroupNewVertical");
			tabGroup.ProminentMenuText = this._res.GetString("TabGroupProminent"); 
			tabGroup.RebalanceMenuText = this._res.GetString("TabGroupRebalance"); 
		}

		/// <summary>
		/// Lancement de l'emulateur
		/// </summary>
		/// <param name="RomPath">Chemin de la rom a ouvrir</param>
		public void LaunchEmulator(string RomPath){			
			//Start info
			ProcessStartInfo si = new ProcessStartInfo(App.EmulatorPath);
			si.WindowStyle = ProcessWindowStyle.Normal; 
			si.Arguments = "\"" + RomPath + "\"";
			si.CreateNoWindow = false;
			si.ErrorDialogParentHandle = this.Handle;
			si.ErrorDialog = true;

			//Demare l'emulateur
			this._Emulator = new Process();
			this._Emulator.StartInfo = si;			
			this._Emulator.Start();
		}

		/// <summary>
		/// Charger un projet dans l'application
		/// </summary>
		private void OpenProject(){
			//Obtenir le fichier a ouvrir
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Title = _res.GetString("OpenDialog_OpenProjectTitle");
			openFile.Filter = _res.GetString("OpenDialog_OpenProjectFilter");
			openFile.CheckFileExists = true;
			openFile.CheckPathExists = true;
			openFile.ShowDialog(this);
			string FileName = openFile.FileName;

			OpenProject(FileName);
		}

		/// <summary>
		/// Charger un projet dans l'application
		/// </summary>
		public void OpenProject(string FileName){
			if (File.Exists(FileName)){
				//Charger le projet
				ProjectError err = this._ProjetExplorer.LoadProject(FileName, false);

				if (err == ProjectError.NoError){
					//Chargement et initialisation du HexaFileShow
					LoadHexaFileShow(); 

					//Chargement des bookmarks
					LoadBookMark();

					//Affichage des menus
					topProject.Visible = true;
					topCompil.Visible = true;

					//Active les barres d'outils
					tbProject.Enabled = true;
					tbProject.Visible = true;
				}
				//else Erreur de chargement du projet
			}
		}

		/// <summary>
		/// Chargement des menus Favoris
		/// </summary>
		public void LoadBookMark(){
			if (this._ProjetExplorer.Projet != null){
				//Supprimer tous le menu
				this.topBookMark.MenuCommands.Clear();

				//Affiche le menu des favoris
				this.topBookMark.Visible = true;

				//Creation du menu de gestionaire de favoris
				MenuCommand mnuGestionaire = new MenuCommand(_res.GetString("MenuBookMarkGestionnaire"), new EventHandler(BookMarkGestionnaire_Click));
				topBookMark.MenuCommands.AddRange(new MenuCommand[]{mnuGestionaire, new MenuCommand("-")});
				
				//Load les BookMark
				Project prj = this._ProjetExplorer.Projet;
				MenuCommand mnu = null;
				Favoris fav = null;
				for (int i=0; i < prj.Favoris.Count; i++){
					fav = (Favoris)prj.Favoris[i];
					mnu = new MenuCommand(fav.Name, new EventHandler(BookMark_Click)); 
					mnu.Tag = fav.Key; 
					topBookMark.MenuCommands.Add(mnu);
				}
			}
		}

		/// <summary>
		/// Charge dans les resources les parametre par defaut de l'interface selon la langue
		/// </summary>
		private void LoadDefaultUIConfig(){
			//TODO: Ajout du support pour l'anglais
			Stream strm = Type.GetType("VRS.IDE.Forms.FormMain").Assembly.GetManifestResourceStream("VRS.IDE.Resource.Config.xml");
			this._DockManager.LoadConfigFromStream(strm);
		}

		private void CleenBookMark(){
			this.topBookMark.MenuCommands.Clear();
			this.topBookMark.Visible = false;
		}

		private void LoadHexaFileShow(){
			if (this._ProjetExplorer.Projet != null){
				//Load HexaFileShow
				string rom = this._ProjetExplorer.Projet.ProjectPath + 
					Path.DirectorySeparatorChar + this._ProjetExplorer.Projet.RomFile;
				TBLFile tbl = GetDefaultTBL();
				string DefaultRomPath = "";
				if (tbl != null){
					DefaultRomPath = this._ProjetExplorer.Projet.ProjectPath + 
						Path.DirectorySeparatorChar + tbl.RelativePath;
					this._HexaFileShow.LoadFile(rom, DefaultRomPath);
					this._HexaFileShow.Projet = this._ProjetExplorer.Projet;  
				}else
					this._HexaFileShow.LoadFile(rom);
				
			}
		}

		/// <summary>
		/// Verifie si un fichier est deja ouvert dans le projet
		/// </summary>
		/// <param name="FileName">Nom du fichier (relatif au projet)</param>
		/// <returns>retoune vrai si le fichier est deja ouvert et faux si le fichier n'est pas ouvert</returns>
		private bool FileOpen(string FileName){
			TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;

			for (int i=0; i< tgl.TabPages.Count; i++){
				if (tgl.TabPages[i].Title == FileName)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Obtenir la Table de jeux par defaut du projet
		/// </summary>
		/// <returns>retoune un objet de type TBLFile</returns>
		/// <remarks>Renvoi un null si il na aucune table n'est trouvé dans le projet opu si aucun projet est ouvert</remarks>
		private TBLFile GetDefaultTBL(){
			Project projet = this._ProjetExplorer.Projet;
			TBLFile tbl = null;
 
			if (projet != null){
				if (projet.Tables.Count > 0){
					for (int i=0; i< projet.Tables.Count; i++){
						tbl = (TBLFile)projet.Tables[i];
						if (tbl.Default == true)							
							return tbl; //table par defaut trouvé
					}

					//Aucune table par defaut alors mettre la premiere TBL par defaut
					tbl = (TBLFile)projet.Tables[0];
					tbl.Default = true;
					return tbl;
				}
				else
					return null; //Aucune table dans le projet
			}

			//pas de projet ouvert donc pas de TBL non plus
			return null;				
		}
		#endregion

		#region Extracteur de Texte

		#endregion

		#region HexaFileShow
		public void HexaFileShow_RightClick(object sender, MouseEventArgs e){
			if (this._ProjetExplorer.Projet != null){
				// Create the popup menu object
				PopupMenu popup = new PopupMenu();

				// apparence du menu
				popup.Style = this._Style;

				//Suffixe pour les resouces
				const string suffix = "popHexaFileShow_";

				//Separateur de menu
				MenuCommand MenuSeparator = new MenuCommand("-");

				//Evenement qui se produit quand la souris passe au decu du menu
				popup.Selected += new CommandHandler(PopExplorer_OnSelected);
				popup.Deselected += new CommandHandler(PopExplorer_OnDeselected);
			
				//Creation des objet qui sont commun a plusieur
				MenuCommand popCopyPosition = new MenuCommand(_res.GetString(suffix + "CopyPos"), this.ImageListMain, 21,  new EventHandler(PopHexaFileShow_CopiePosition));
				MenuCommand popJumpToPosition = new MenuCommand(_res.GetString(suffix + "GotoPos"), this.ImageListMain, 22,  new EventHandler(PopHexaFileShow_JumpToPosition));
				MenuCommand popAddBookMark = new MenuCommand(_res.GetString(suffix + "AddBookMark"), this.ImageListMain, 20,  new EventHandler(PopHexaFileShow_AddBookMark));
			
				// defini la liste des menus
				popup.MenuCommands.AddRange(new MenuCommand[]{popJumpToPosition, popCopyPosition, MenuSeparator,
																 popAddBookMark, this.topBookMark});

				if (popup.MenuCommands.Count > 0)
					if (this._HexaFileShow._PicDataRightClick)
						popup.TrackPopup(this._HexaFileShow.picData.PointToScreen(new Point(e.X, e.Y)));
					else
						popup.TrackPopup(this._HexaFileShow.picDecodedData.PointToScreen(new Point(e.X, e.Y)));
			}				
		}


		public void PopHexaFileShow_AddBookMark(object sender, EventArgs e) {
			string response = InputBox.Show(App.Name, this._res.GetString("InputBox_AddBookMark"), "BookMark" + (this._ProjetExplorer.Projet.Favoris.Count + 1));
			
			if (response != null){
				Favoris fav = new Favoris(response, this._HexaFileShow.Position, this._ProjetExplorer.Projet.RomFile, "mark" + DateTime.Now.Ticks.ToString());
				this._ProjetExplorer.Projet.Favoris.Add(fav);
				LoadBookMark();
			}
		}

		public void PopHexaFileShow_CopiePosition(object sender, EventArgs e) {
			Clipboard.SetDataObject(this._HexaFileShow.Position, true);
		}

		public void PopHexaFileShow_JumpToPosition(object sender, EventArgs e) {
			string response = InputBox.Show(App.Name, this._res.GetString("InputBox_Jumptopos"),  this._HexaFileShow.Position);
			
			if (response != null){
				this._HexaFileShow.Position = response;
			}	
		}	
		#endregion

		#region Generation du project
		public void OnBuildProject(object sender, EventArgs e) {

		}

		public void OnCleenProject(object sender, EventArgs e) {

		}

		public void Build_Options(object sender, EventArgs e) {
			new FormBuildOptions().ShowDialog(this);
		}
		#endregion

		#region Timers
		private void timerTabGroupLeaf_Tick(object sender, System.EventArgs e) {
			// Créé un access a la premiere page page du group
			TabGroupLeaf tgl = tabGroup.RootSequence[0] as TabGroupLeaf;

			this.tabGroup.Visible = (tgl.TabPages.Count > 0);
			
		}
		#endregion

		#region Toolbars
		/// <summary>
		/// Applique le texte selon la langue du logiciel
		/// </summary>
		private void MakeToolBarText() {
			//
			//ToolBar Standard
			//
			tbbStandardNewProject.ToolTipText  = this._res.GetString("NewProject"); 
			//Separator
			tbbStandardOpen.ToolTipText  = this._res.GetString("OpenFile"); 
			tbbStandardSave.ToolTipText  = this._res.GetString("Save"); 
			tbbStandardSaveAll.ToolTipText  = this._res.GetString("SaveAll"); 
			//Separator
			tbbStandardCopy.ToolTipText  = this._res.GetString("Copy"); 
			tbbStandardCut.ToolTipText  = this._res.GetString("Cut"); 
			tbbStandardPaste.ToolTipText  = this._res.GetString("Paste"); 

			//
			//Toolbar Project
			//
			tbProject.Text = this._res.GetString("Project");
			// 
			tbbProjectBuildProject.ToolTipText = this._res.GetString("build");
			tbbProjectStartEmulator.ToolTipText = this._res.GetString("LaunchEmulator");
			tbbProjectComboEmulator.ToolTipText = this._res.GetString("ChooseEmul");
		} 
 
		/// <summary>
		/// Click sur la barre d'outil Standard
		/// </summary>		
		private void tbStandard_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e) {
			TD.SandBar.ButtonItemBase button = (TD.SandBar.ButtonItemBase)e.Item; 
			
			//Click sur Bouton couper
			if (button  == tbbStandardNewProject){
				File_NewProject(new object(), EventArgs.Empty);
			}

			//Click sur Bouton couper
			if (button  == tbbStandardOpen){
				File_OpenProject(new object(), EventArgs.Empty);
			}

			//Click sur Bouton couper
			if (button  == tbbStandardCut){
				
			}

			//Click sur Bouton coller
			if (button  == tbbStandardPaste ){
				
			}

			//Click sur Bouton copier
			if (button  == tbbStandardCopy ){
				
			}

			//Click sur Bouton enregistrer
			if (button  == tbbStandardSave){
				Save_File(new object(), EventArgs.Empty); //Enregistrer le fichier en cours  
			}

			//Click sur Bouton enregistrer tout
			if (button  == tbbStandardSaveAll){
				Save_All(new object(), EventArgs.Empty); //enregistrer tout les fichier ouvert et le projet
			}
		}

		/// <summary>
		/// Click sur la barre d'outil Emulator
		/// </summary>	
		private void tbProject_ButtonClick(object sender, TD.SandBar.ToolBarItemEventArgs e) {
			try{
				TD.SandBar.ButtonItemBase button = (TD.SandBar.ButtonItemBase)e.Item; 
			
				//Click sur Bouton start emulator
				if (button  == tbbProjectStartEmulator){
					//Lancer le projet
				}

				//Click sur Bouton build project
				if (button  == tbbProjectBuildProject){
					//Generer le projet
				}
			}catch{}
		}
		#endregion

		#region PropertyObject Event
		private void PropertyGrid_ValueChanged(object s, PropertyValueChangedEventArgs e){
			this._ProjetExplorer.LoadProject(this._ProjetExplorer.FileName, true);
		}
		#endregion
		
		private void FormMain_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//this._DockManager.SaveConfigToFile("uiConfig.xml");
		}

		private void FormMain_Load(object sender, System.EventArgs e) {
			//if (File.Exists("uiConfig.xml"))
			//	this._DockManager.LoadConfigFromFile("uiConfig.xml");
		}

	}
}


