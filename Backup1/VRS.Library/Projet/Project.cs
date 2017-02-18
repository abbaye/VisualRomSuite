using System;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections;
using VRS.Library.IO;
using VRS.Library.Console.SuperNintendo;
using VRS.Library.Table.TBL;
using VRS.Library.Collections;

using System.ComponentModel;

namespace VRS.Library.Projet {
	/// <summary>
	/// Classe pour gérer les projet VRS
	/// </summary>
	public sealed class Project {
		private string _Name = "";
		private string _FileName = "";
		private string _Author = "";
		private string _Description = "";
		private string _InternalRomName = "";
		private string _OutputPath = "debug";
		private string _MD5Key = "";
		private string _Email = "";
		private bool   _RLECompress = false;
		private string _HomePage = "";
		private string _RomFile = "";
		private string _WorkRomFile = "";
		private ProjectType _ProjectType = ProjectType.SuperNintendo; 

		#region Collections
		/// <summary>
		/// Collection de Fichier Texte
		/// </summary>
        private TextFileCollection _TextFileArray = new TextFileCollection();

		/// <summary>
		/// Collection de Fichier TBL
		/// </summary>
		private TBLFileCollection _TBLFileArray = new TBLFileCollection();

		/// <summary>
		/// Collection de Favoris (Bookmark)
		/// </summary>
        private FavorisCollection _FavorisFileArray = new FavorisCollection();	
	
		/// <summary>
		/// Collection HexaSnapShot
		/// </summary>
        private HexaSnapShotCollection _HexaSnapShotArray = new HexaSnapShotCollection();	

		/// <summary>
		/// Collection Table Fixe
		/// </summary>
        private TableFixeCollection _FixeTableArray = new TableFixeCollection();	

		/// <summary>
		/// Collection Liste de Tâches
		/// </summary>
        private TaskCollection _TaskArray = new TaskCollection();	
		#endregion

		#region Constructeurs
		/// <summary>
		/// Constructeur par defaut
		/// </summary>
		public Project() {

		}

		/// <summary>
		/// Constructeur permettant d'initialiser un fichier
		/// </summary>
		/// <param name="FileName">Chemin vers le fichier projet</param>
		public Project(string FileName){
			//if (File.Exists(FileName))
			this._FileName = FileName; 
		}

		/// <summary>
		/// Constructeur permettant d'initialiser un fichier
		/// </summary>
		/// <param name="FileName">Chemin vers le fichier projet</param>
		public Project(string FileName, bool LoadFile){
			//if (File.Exists(FileName))
			this._FileName = FileName; 

			//Charger le projet
			if (LoadFile)
				Load();
		}
		#endregion

		#region Methodes
		/// <summary>
		/// Creation d'un projet
		/// </summary>
		/// <param name="Name">Nom du projet</param>
		/// <param name="Path">Repertoire du projet</param>
		/// <param name="RomFileName">Chemin vers le fichier rom a utilisé</param>		
		/// <returns></returns>
		public bool CreateProject(string ProjectName, string Path, string RomFileName, string TBLFileName){
			if (!File.Exists(RomFileName)) return false;
			if (!File.Exists(TBLFileName)) return false;

			if (SnesRom.isValid(RomFileName)){
				//creation  des repertoire de projet
				Directory.CreateDirectory(Path + @"\" + ProjectName);

				//Nom du fichier projet
				this._FileName = Path + @"\" + ProjectName + @"\" + ProjectName + ".vrsproj";
				string prjPath = Path + @"\" + ProjectName;
				this._Name = ProjectName;

				//Copie des fichiers repertoire de projet
				string romfilename = System.IO.Path.GetFileName(RomFileName);
				string workromfilename = System.IO.Path.GetFileNameWithoutExtension(RomFileName) + ".work";
				string destFile = prjPath + @"\" + romfilename;
				string destFileWork = prjPath + @"\" + workromfilename;
				string tblfile = prjPath + @"\" + System.IO.Path.GetFileName(TBLFileName);
                
				File.Copy (RomFileName, destFile, true);
				File.Copy (RomFileName, destFileWork , true);
				File.Copy (TBLFileName, tblfile , true);
			
				this._RomFile = romfilename;
				this._WorkRomFile = workromfilename; 
			
				//MD5
				SnesRom snes = new SnesRom(romfilename);
				this._InternalRomName = snes.RomName;
				this._MD5Key = snes.MD5Hash;

				//Ajout de la TBL au projet
				TBLFile tbl = new TBLFile();
				tbl.Key = "tbl" + Convert.ToString(this._TBLFileArray.Count);
				tbl.Name = System.IO.Path.GetFileNameWithoutExtension(TBLFileName);
				tbl.RelativePath = System.IO.Path.GetFileName(TBLFileName);
				tbl.Default = true;
				this._TBLFileArray.Add(tbl); 
 
				//Enregistrement du projet
				Save();
			
				//Fin de la fonction
				return true;
			}else
				return false;
		}

		/// <summary>
		/// Chargement du fichier projet
		/// </summary>
		/// <returns>Retourn un ProjectError pour determiner le type d'erreur lors du chargement du projet</returns>
		public ProjectError Load(){
			try{
				if (File.Exists(this.FileName)){
					//Creation du reader
					XmlDocument doc = new XmlDocument();
					XmlTextReader reader = new XmlTextReader(this._FileName);
			
					//Lecture du document
					doc.Load(reader);
			
					//Lecture des informations sur le projet
					XmlNodeList nodes = doc.ChildNodes.Item(0).ChildNodes ;
					XmlNode node = nodes.Item(0);
			
					try{
						this._Author = node.Attributes["Author"].Value;
						this._Description = node.Attributes["Description"].Value;
						this._MD5Key = node.Attributes["MD5Key"].Value;
						this._Name = node.Attributes["Name"].Value;
						this._OutputPath = node.Attributes["OutputPath"].Value;
						this._Email = node.Attributes["Email"].Value;
						this._RLECompress = Convert.ToBoolean(node.Attributes["RLECompress"].Value);
						this._HomePage = node.Attributes["HomePage"].Value;
						this._RomFile = node.Attributes["RomFile"].Value;
						this._WorkRomFile = node.Attributes["WorkRomFile"].Value;

						//Type de projet
						switch (node.Attributes["ProjectType"].Value){
							case "SuperNintendo":
								this._ProjectType = ProjectType.SuperNintendo;
								break;
							default:
								this._ProjectType = ProjectType.SuperNintendo;
								break;
						}
					}
					catch{
						return ProjectError.ProjectSectionLoadError;
					}

				#region Chargement des Fichiers textes
					//Lecture des includes : TextFile
					int Total = 0;
					XmlNodeList IncludeNodes = nodes.Item(0).ChildNodes.Item(0).ChildNodes;
					node = IncludeNodes[0];
					try{
						Total = Convert.ToInt32(node.Attributes["Count"].Value); //Total de fichier texte
						if (Total > 0) {
							XmlNodeList Textnodes = node.ChildNodes; 
							XmlNode Textnode; 
							TextFile txtFile;
				
							//Chargement de tous les fichier texte
							for (int i=0; i< Total; i++){
								Textnode = Textnodes[i];
								txtFile = new TextFile();
								txtFile.Name = Textnode.Attributes["Name"].Value;
								txtFile.RelativePath = Textnode.Attributes["RelativePath"].Value;
								txtFile.Extraction_TextBank_Start = Textnode.Attributes["Extraction_TextBank_Start"].Value;
								txtFile.Extraction_TextBank_Stop = Textnode.Attributes["Extraction_TextBank_Stop"].Value;
								txtFile.Extraction_PointeurBank_Start = Textnode.Attributes["Extraction_PointeurBank_Start"].Value;
								txtFile.Extraction_PointeurBank_Stop = Textnode.Attributes["Extraction_PointeurBank_Stop"].Value;
								txtFile.Extraction_HeaderAdjustement_Moins = Textnode.Attributes["Extraction_HeaderAdjustement_Moins"].Value;
								txtFile.Extraction_HeaderAdjustement_Plus = Textnode.Attributes["Extraction_HeaderAdjustement_Plus"].Value;
								txtFile.Insertion_TextBankStart = Textnode.Attributes["insertion_TextBankStart"].Value;
								txtFile.Insertion_TextBankStop = Textnode.Attributes["insertion_TextBankStop"].Value;
								txtFile.Insertion_PointeurBankStart  = Textnode.Attributes["insertion_PointeurBankStart"].Value;
								txtFile.Insertion_PointeurBankStop = Textnode.Attributes["insertion_PointeurBankStop"].Value;
								txtFile.Insertion_HeaderAdjustementMoins  = Textnode.Attributes["insertion_HeaderAdjustementMoins"].Value;
								txtFile.Insertion_HeaderAdjustementPlus = Textnode.Attributes["insertion_HeaderAdjustementPlus"].Value;
								txtFile.AucunPointeur = Convert.ToBoolean(Textnode.Attributes["AucunPointeur"].Value);
								txtFile.Description = Textnode.Attributes["Description"].Value;
								txtFile.InsertAtCompil = Convert.ToBoolean(Textnode.Attributes["InsertAtCompil"].Value);
								txtFile.TablePath = Textnode.Attributes["TablePath"].Value;
								txtFile.IsTextPointeurTable = Convert.ToBoolean(Textnode.Attributes["isTextPointeurTable"].Value);
								txtFile.key = "txt" + Convert.ToString(this._TextFileArray.Count);

								switch (Textnode.Attributes["Mode"].Value){
									case "_16Bits":
										txtFile.Mode = TextMode._16Bits;
										break;
									case "_24Bits":
										txtFile.Mode = TextMode._24Bits;
										break;
									default:
										txtFile.Mode = TextMode._16Bits;
										break;
								}

								this._TextFileArray.Add(txtFile);
							}
						}
					}
					catch{
						return ProjectError.ProjectSectionLoadError; 
					}
				#endregion

				#region Chargement des fichiers HexaSnapShot
					//Lecture des includes : HexaSnapShot
					node = IncludeNodes[1];
					Total = Convert.ToInt32(node.Attributes["Count"].Value); //Total de fichier texte
					try{
						if (Total > 0) {
							XmlNodeList HexaSnapNodes = node.ChildNodes; 
							XmlNode HexaSnapNode; 				
							HexaSnapShot hexfile;

							//Chargement de tous les fichier Hexasnap
							for (int i=0; i< Total; i++){
								HexaSnapNode = HexaSnapNodes[i];
								hexfile = new HexaSnapShot();
								hexfile.Description = HexaSnapNode.Attributes["Description"].Value;
								hexfile.StartPosition = HexaSnapNode.Attributes["StartPosition"].Value;
								hexfile.Name = HexaSnapNode.Attributes["Name"].Value;
								hexfile.RelativePath = HexaSnapNode.Attributes["RelativePath"].Value;
								hexfile.InsertAtCompil = Convert.ToBoolean(HexaSnapNode.Attributes["InsertAtCompil"].Value);
								hexfile.Key = "hex" + Convert.ToString(this._HexaSnapShotArray.Count);
								this._HexaSnapShotArray.Add(hexfile);
							} 
						}
					}
					catch{
						return ProjectError.HexaSnapShotSectionError;
					}
				#endregion

				#region Chargement des fichiers Table TBL
					//Lecture des includes : TableFile
					node = IncludeNodes[2];
					Total = Convert.ToInt32(node.Attributes["Count"].Value); //Total de fichier texte
					try{
						if (Total > 0) {
							XmlNodeList tblNodes = node.ChildNodes; 
							XmlNode tblNode; 				
							TBLFile tbl;

							//Chargement de tous les fichier TBL
							for (int i=0; i< Total; i++){
								tblNode = tblNodes[i];
								tbl = new TBLFile();
								tbl.Description = tblNode.Attributes["Description"].Value;
								tbl.Name = tblNode.Attributes["Name"].Value;
								tbl.RelativePath = tblNode.Attributes["RelativePath"].Value;
								tbl.Key = "tbl" + Convert.ToString(this._TBLFileArray.Count);	
								tbl.Default = Convert.ToBoolean(tblNode.Attributes["Default"].Value);
								this._TBLFileArray.Add(tbl);
							} 
						}
					}
					catch{
						return ProjectError.TableSectionError; 
					}
				#endregion

				#region Chargement des Tableaux a largeur fixe
					//Lecture des includes : TableFixe
					node = IncludeNodes[3];
					Total = Convert.ToInt32(node.Attributes["Count"].Value); //Total de fichier texte
					try{
						if (Total > 0) {
							XmlNodeList FixeNodes = node.ChildNodes; 
							XmlNode FixeNode; 				
							TableFixeFile fixefile ;

							//Chargement de tous les fichier TBL
							for (int i=0; i< Total; i++){
								FixeNode = FixeNodes[i];
								fixefile = new TableFixeFile();
								fixefile.Description = FixeNode.Attributes["Description"].Value;
								fixefile.TableauName = FixeNode.Attributes["TableauName"].Value;
								fixefile.Largeur   = Convert.ToInt16(FixeNode.Attributes["Largeur"].Value);
								fixefile.Position  = FixeNode.Attributes["Position"].Value;
								fixefile.EmptyChar = FixeNode.Attributes["EmptyChar"].Value;
								fixefile.TableName = FixeNode.Attributes["TableName"].Value;
								fixefile.TotalCase = Convert.ToInt16(FixeNode.Attributes["TotalCase"].Value);
								fixefile.Key = "tbl" + Convert.ToString(this._TBLFileArray.Count);
								this._FixeTableArray.Add(fixefile);
							} 					
						}
					}
					catch{
						return ProjectError.FixeTableSectionError;
					}
				#endregion

				#region Chargement des favoris (bookmarks)
					//Bookmark nodes
					XmlNodeList BookMarkNodes = nodes.Item(0).ChildNodes.Item(1).ChildNodes; 
					Total = BookMarkNodes.Count; 
					try{
						if (Total > 0) {
							XmlNodeList MarkNodes = BookMarkNodes;
							XmlNode MarkNode;
							Favoris fav;

							//Chargement de tous les Bookmark dans le projet
							for (int i=0; i< Total; i++){
								MarkNode = MarkNodes[i];
								fav = new Favoris();
								fav.Position = MarkNode.Attributes["Position"].Value;
								fav.Name = MarkNode.Attributes["Name"].Value;
								fav.File = MarkNode.Attributes["File"].Value;
								fav.Key  = "mark" + Convert.ToString(this._FavorisFileArray.Count);
								this._FavorisFileArray.Add(fav);
							} 
						}
					}
					catch{
						return ProjectError.BookmarkSectionError; 
					}
				#endregion

				#region Chargement de la liste de tâches
					//TaskList nodes
					XmlNodeList TaskListNodes = nodes.Item(0).ChildNodes.Item(2).ChildNodes; 
					Total = TaskListNodes.Count; 
					try{
						if (Total > 0) {
							XmlNodeList TaskNodes = TaskListNodes;
							XmlNode TaskNode;
							Task task;

							//Chargement de tous les Bookmark dans le projet
							for (int i=0; i< Total; i++){
								TaskNode = TaskNodes[i];
								task = new Task();
								task.File = TaskNode.Attributes["File"].Value;
								task.Description = TaskNode.Attributes["Description"].Value;
								task.Line = Convert.ToInt32(TaskNode.Attributes["Line"].Value);					
								task.TaskComplete = Convert.ToBoolean(TaskNode.Attributes["TaskComplete"].Value);
								task.Key  = "task" + Convert.ToString(this._TaskArray.Count);

								switch (TaskNode.Attributes["Priority"].Value){
									case "Faible":
										task.Priority = TaskPriority.Faible;
										break;
									case "Normal":
										task.Priority = TaskPriority.Normal;
										break;
									case "Haute":
										task.Priority = TaskPriority.Haute;
										break;
									default:
										task.Priority = TaskPriority.Normal;
										break;
								}
								this._TaskArray.Add(task);
							} 
						}
					}
					catch{
						return ProjectError.TaskListSectionError; 
					}
				#endregion

					//fin de la fonction
					return ProjectError.NoError; //Aucune erreur
				}else
					return ProjectError.FileNotFound; // Si aucun fichier charger
			}
			catch{
				return ProjectError.UnknowError;
			}
		}

		/// <summary>
		/// Fermer le projet
		/// </summary>
		//public void Close(){}


		/// <summary>
		/// Defini un Table par defaut
		/// </summary>
		/// <param name="TBLKey">Clef de la Table dans le projet</param>
		public void SetDefaultTBL(string TBLKey){
			TBLFile tbl;
			for (int i=0; i< this._TBLFileArray.Count; i++){
				tbl = this._TBLFileArray[i] as TBLFile;
				if (tbl != null){
					if (tbl.Key == TBLKey)
						tbl.Default = true;
					else
						tbl.Default = false;
				}
			}
		}

		/// <summary>
		/// Enregistrement du fichier projet
		/// </summary>
		/// <returns></returns>
		public ProjectError Save(){
			try{
				XmlTextWriter myWriter = new XmlTextWriter(this._FileName, Encoding.Default);  
				myWriter.Formatting = Formatting.Indented;
			
				//Header avec la version
				myWriter.WriteStartElement("VisualRomSuiteProject");
				myWriter.WriteAttributeString("Version", "0.1");

				myWriter.WriteStartElement("VRSProj");
				myWriter.WriteAttributeString("Author", this._Author);
				myWriter.WriteAttributeString("Description", this._Description);
				myWriter.WriteAttributeString("MD5Key", this._MD5Key);
				myWriter.WriteAttributeString("Name", this._Name);
				myWriter.WriteAttributeString("OutputPath", this._OutputPath);
				myWriter.WriteAttributeString("Email", this._Email);
				myWriter.WriteAttributeString("RLECompress", Convert.ToString(this._RLECompress));
				myWriter.WriteAttributeString("HomePage", this._HomePage);
				myWriter.WriteAttributeString("RomFile", this._RomFile);
				myWriter.WriteAttributeString("WorkRomFile", this._WorkRomFile);
				myWriter.WriteAttributeString("ProjectType", this._ProjectType.ToString());

				//Include
				myWriter.WriteStartElement("Include");		
				//Fichier Texte
				myWriter.WriteStartElement("TextFile");
				myWriter.WriteAttributeString("Count", Convert.ToString(this._TextFileArray.Count));
				if (this._TextFileArray.Count > 0){
					for(int i=0; i<this._TextFileArray.Count; i++){
						TextFile textFile = ((TextFile)this._TextFileArray[i]);				
						myWriter.WriteStartElement("File");
						myWriter.WriteAttributeString("RelativePath", textFile.RelativePath); 
						myWriter.WriteAttributeString("Type", FileType.Texte.ToString()); 
						myWriter.WriteAttributeString("Extraction_TextBank_Start", textFile.Extraction_TextBank_Start);
						myWriter.WriteAttributeString("Extraction_TextBank_Stop", textFile.Extraction_TextBank_Stop);
						myWriter.WriteAttributeString("Extraction_PointeurBank_Start", textFile.Extraction_PointeurBank_Start);
						myWriter.WriteAttributeString("Extraction_PointeurBank_Stop", textFile.Extraction_PointeurBank_Stop);
						myWriter.WriteAttributeString("Extraction_HeaderAdjustement_Plus", textFile.Extraction_HeaderAdjustement_Plus);
						myWriter.WriteAttributeString("Extraction_HeaderAdjustement_Moins", textFile.Extraction_HeaderAdjustement_Moins);
						myWriter.WriteAttributeString("insertion_TextBankStart", textFile.Insertion_TextBankStart);
						myWriter.WriteAttributeString("insertion_TextBankStop ", textFile.Insertion_TextBankStop);
						myWriter.WriteAttributeString("insertion_PointeurBankStart", textFile.Insertion_PointeurBankStart);
						myWriter.WriteAttributeString("insertion_PointeurBankStop", textFile.Insertion_PointeurBankStop);
						myWriter.WriteAttributeString("insertion_HeaderAdjustementPlus", textFile.Insertion_HeaderAdjustementPlus);
						myWriter.WriteAttributeString("insertion_HeaderAdjustementMoins", textFile.Insertion_HeaderAdjustementMoins);
						myWriter.WriteAttributeString("AucunPointeur", Convert.ToString(textFile.AucunPointeur));
						myWriter.WriteAttributeString("Description", textFile.Description);
						myWriter.WriteAttributeString("Name", textFile.Name);
						myWriter.WriteAttributeString("InsertAtCompil", Convert.ToString(textFile.InsertAtCompil));
						myWriter.WriteAttributeString("TablePath", textFile.TablePath);
						myWriter.WriteAttributeString("Mode", textFile.Mode.ToString());
						myWriter.WriteAttributeString("isTextPointeurTable", Convert.ToString(textFile.IsTextPointeurTable));
						myWriter.WriteEndElement();	
					}		
				}
				myWriter.WriteEndElement();

				//Fichier HexaSnapShot
				myWriter.WriteStartElement("HexaSnapShot");
				myWriter.WriteAttributeString("Count", Convert.ToString(this._HexaSnapShotArray.Count));
				if (this._HexaSnapShotArray.Count > 0){
					for(int i=0; i<this._HexaSnapShotArray.Count; i++){
						HexaSnapShot snap = ((HexaSnapShot)this._HexaSnapShotArray[i]);
						myWriter.WriteStartElement("SnapShot");
						myWriter.WriteAttributeString("Name", snap.Name);
						myWriter.WriteAttributeString("RelativePath", snap.RelativePath); 
						myWriter.WriteAttributeString("Description", snap.Description); 
						myWriter.WriteAttributeString("StartPosition", snap.StartPosition);
						myWriter.WriteAttributeString("InsertAtCompil", Convert.ToString(snap.InsertAtCompil));
						myWriter.WriteEndElement();	
					}		
				}
				myWriter.WriteEndElement();

				//Fichier TBL
				myWriter.WriteStartElement("TableFile");
				myWriter.WriteAttributeString("Count", Convert.ToString(this._TBLFileArray.Count));
				if (this._TBLFileArray.Count > 0){
					for(int i=0; i<this._TBLFileArray.Count; i++){
						TBLFile tblFile = ((TBLFile)this._TBLFileArray[i]);
						myWriter.WriteStartElement("TBL");
						myWriter.WriteAttributeString("Type", FileType.Table.ToString());
						myWriter.WriteAttributeString("RelativePath", tblFile.RelativePath); 
						myWriter.WriteAttributeString("Description", tblFile.Description); 
						myWriter.WriteAttributeString("Name", tblFile.Name);
						myWriter.WriteAttributeString("Default", tblFile.Default.ToString());
						myWriter.WriteEndElement();	
					}		
				}
				myWriter.WriteEndElement();

				//Fichier TBL
				myWriter.WriteStartElement("TableauFixe");
				myWriter.WriteAttributeString("Count", Convert.ToString(this._FixeTableArray.Count));
				if (this._FixeTableArray.Count > 0){
					for(int i=0; i<this._FixeTableArray.Count; i++){
						TableFixeFile fixe = ((TableFixeFile)this._FixeTableArray[i]);
						myWriter.WriteStartElement("Fixe");
						myWriter.WriteAttributeString("TableName", fixe.TableName);
						myWriter.WriteAttributeString("Largeur", Convert.ToString(fixe.Largeur)); 
						myWriter.WriteAttributeString("TotalCase", Convert.ToString(fixe.TotalCase)); 
						myWriter.WriteAttributeString("EmptyChar", fixe.EmptyChar);
						myWriter.WriteAttributeString("Position", fixe.Position); 
						myWriter.WriteAttributeString("TableauName", fixe.TableauName);
						myWriter.WriteAttributeString("Description", fixe.Description); 
					
						myWriter.WriteEndElement();	
					}		
				}
				myWriter.WriteEndElement();

				//Fermeture du tag Include
				myWriter.WriteEndElement();	

				//Adresse Favoris
				myWriter.WriteStartElement("BookMark");
				myWriter.WriteAttributeString("Count", Convert.ToString(this._FavorisFileArray.Count));
				if (this._FavorisFileArray.Count > 0){
					for(int i=0; i<this._FavorisFileArray.Count; i++){
						Favoris fav = ((Favoris)this._FavorisFileArray[i]);
						myWriter.WriteStartElement("Mark");
						myWriter.WriteAttributeString("Name", fav.Name);
						myWriter.WriteAttributeString("Position", fav.Position); 
						myWriter.WriteAttributeString("File", fav.File); 
						myWriter.WriteEndElement();	
					}		
				}
				myWriter.WriteEndElement();

				//Taches
				myWriter.WriteStartElement("TaskList");
				myWriter.WriteAttributeString("Count", Convert.ToString(this._TaskArray.Count));
				if (this._TaskArray.Count > 0){
					for(int i=0; i<this._TaskArray.Count; i++){
						Task task = ((Task)this._TaskArray[i]);
						myWriter.WriteStartElement("Task");
						myWriter.WriteAttributeString("Line", Convert.ToString(task.Line));
						myWriter.WriteAttributeString("File", task.File); 
						myWriter.WriteAttributeString("Description", task.Description); 
						myWriter.WriteAttributeString("Priority", task.Priority.ToString()); 
						myWriter.WriteAttributeString("TaskComplete", Convert.ToString(task.TaskComplete)); 
						myWriter.WriteEndElement();	
					}		
				}
				myWriter.WriteEndElement();

				//Fermeture du tag VRSProj
				myWriter.WriteEndElement();

				//Fermeture du tag Header
				myWriter.WriteEndElement();

				//Fermeture du fichier
				myWriter.Close();	
			
				return ProjectError.NoError;
			}
			catch{
				return ProjectError.UnknowError;
			}
		}

        public void AjouterTBL(string filename)
        {
            string tblfile = ProjectPath + @"\" + System.IO.Path.GetFileName(filename);

            try {
                File.Copy(filename, tblfile, true);
            }catch{
            }

            //Ajout de la TBL au projet
            TBLFile tbl = new TBLFile();
            tbl.Key = "tbl" + Convert.ToString(this._TBLFileArray.Count);
            tbl.Name = System.IO.Path.GetFileNameWithoutExtension(filename);
            tbl.RelativePath = System.IO.Path.GetFileName(filename);
            tbl.Default = false;
            this._TBLFileArray.Add(tbl); 
        }

        public void DeleteTBL(string key) {
            //Recherche l<index avec la key
            TBLFile tbl;
            for (int i = 0; i <= _TBLFileArray.Count; i++){
                tbl = (TBLFile)this._TBLFileArray[i];
                if (tbl.Key == key) {
                    _TBLFileArray.RemoveAt(i);
                    if (File.Exists(ProjectPath + "\\" + tbl.RelativePath))
                        File.Delete(ProjectPath + "\\" + tbl.RelativePath);

                }
            }
        }
		#endregion
	
		#region Array Property
		/// <summary>
		/// Liste de tâches
		/// </summary>	
		[Browsable(false)]
        public TaskCollection Taches {
			get{
				return this._TaskArray; 
			}
			set{
				this._TaskArray = value; 
			}
		}

		/// <summary>
		/// Liste de Favoris
		/// </summary>		
		[Browsable(false)]
        public FavorisCollection Favoris {
			get{
				return this._FavorisFileArray; 
			}
			set{
				this._FavorisFileArray = value; 
			}
		}
		/// <summary>
		/// Liste de Tableau Fixe
		/// </summary>		
		[Browsable(false)]
        public TableFixeCollection TableFixe {
			get{
				return this._FixeTableArray; 
			}
			set{
				this._FixeTableArray = value; 
			}
		}

		/// <summary>
		/// Liste de SnapShot Binaire
		/// </summary>
		[Browsable(false)]
		public HexaSnapShotCollection HexaSnapShot{
			get{
				return this._HexaSnapShotArray; 
			}
			set{
				this._HexaSnapShotArray = value; 
			}
		}

		/// <summary>
		/// Liste de Table (TBL)
		/// </summary>
		[Browsable(false)]
		public TBLFileCollection Tables{
			get{
				return this._TBLFileArray; 
			}
			set{
				this._TBLFileArray = value; 
			}
		}

		/// <summary>
		/// Liste de Fichier textes
		/// </summary>
		[Browsable(false)]
        public TextFileCollection Textes {
			get{
				return this._TextFileArray; 
			}
			set{
				this._TextFileArray = value; 
			}
		}
		#endregion

		#region Project Property
		/// <summary>
		/// 
		/// </summary>
		[Description("Nom du projet"), Category("Projet Informations")]
		public string Name{
			get{
				return this._Name; 
			}
			set{
				this._Name = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[ReadOnly(true)]
		[Description("Chemin vers le fichier du projet"), Category("Projet Informations")]
		public string FileName{
			get{
				return this._FileName; 
			}
			set{
				this._FileName = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Repertoire de travaille du projet"), Category("Projet Informations")]
		public string ProjectPath{
			get{
				return Path.GetDirectoryName(this._FileName);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Auteur du projet"), Category("Projet Informations")]
		public string Author{
			get{
				return this._Author; 
			}
			set{
				this._Author = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Description du projet"), Category("Projet Informations")]
		public string Description{
			get{
				return this._Description; 
			}
			set{
				this._Description = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Répertoire de sortie pour le fichier IPS"), Category("Projet Informations")]
		public string OutPutPath{
			get{
				return this._OutputPath; 
			}
			set{
				this._OutputPath = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Clé unique représantant le fichier de jeu (ROM)"), Category("Projet Informations")]
		public string MD5Key{
			get{
				return this._MD5Key; 
			}			
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Couriel du créateur du projet"), Category("Projet Informations")]
		public string Email{
			get{
				return this._Email; 
			}
			set{
				this._Email = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Visual Rom Suite doit t'il utiliser la compression RLE lors de génération du projet ?"), Category("Projet Informations")]
		public bool RLECompress{
			get{
				return this._RLECompress; 
			}
			set{
				this._RLECompress = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Fichier Rom sur lequel Visual Rom Suite travaille"), Category("Projet Informations")]
		public string WorkRomFile{
			get{
				return this._WorkRomFile; 
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Description("Fichier rom utilisé"), Category("Projet Informations")]
		public string RomFile{
			get{
				return this._RomFile; 
			}
		}

		/// <summary>
		/// Page d'accueil du créateur du projet
		/// </summary>
		[Description("Page d'accueil du créateur du projet"), Category("Projet Informations")]
		public string HomePage{
			get{
				return this._HomePage; 
			}
			set{
				this._HomePage = value;
			}
		}
		#endregion

	}
}