using System;
using System.IO;
using System.Collections;
using System.Text;
using System.ComponentModel;

using VRS.Library.Projet;

namespace VRS.Library.Table.TBL {
	/// <summary>
	/// Cet objet représente un fichier TBL avec tous sais entré et valeur
	/// </summary>
	public class TBLStream {
		/// <summary>Chemin vers le fichier (path)</summary>
		private string _FileName;
		/// <summary>Tableau de DTE représentant tous les les entrée du fichier</summary>
		private ArrayList _DTE = new ArrayList();

		//HACK: Corrige un bug de conception pour retourner la key du projet pour la tbl selectioner
        public string key = "";

		/// <summary> Liste des favoris dans la TBL</summary>
		private ArrayList _Favoris = new ArrayList();

		/// <summary>Commentaire du fichier TBL</summary>
		//		private string _Commentaire = "";
		
		#region Constructeurs
		/// <summary>
		/// Constructeur principal
		/// </summary>
		public TBLStream() {
			//			this._Commentaire = "";
			this._FileName= "";
			this._DTE.Clear();
		}	

		/// <summary>
		/// Constructeur permétant de chargé le fichier DTE
		/// </summary>
		/// <param name="FileName"></param>
		public TBLStream(string FileName) {
			//			this._Commentaire = "";
			this._DTE.Clear();

			//Chargé le fichier dans l'objet
			//if (File.Exists(FileName)){
			this._FileName = FileName; 
			//TODO: Charger le fichier dans la collection		
			//}//else
			//TODO: cree le fichier TBL
		}
		#endregion

		#region Indexer
		/// <summary>
		/// Indexeur permetant de travailler sur les DTE contenue dans TBL a la facons d'un tableau.
		/// </summary>
		public DTE this [int index] {   // declaration de indexer 
			get {
				// verifie la limite de l'index
				if (index < 0 || index > this._DTE.Count)
					throw new IndexOutOfRangeException("Cette item n'existe pas");  
				else
					return (DTE)this._DTE[index];
			}
			set { 
				if (!(index < 0 || index >= this._DTE.Count))
					this._DTE[index] = value;
			}
		}
		#endregion

		#region Méthodes
		/// <summary>
		/// Vérifie le nombre d'entrée valide dans le fichier (methode static)
		/// </summary>
		/// <returns>Retourne le nombre d'entrée valide dans le fichier</returns>
		//public static int isValid(string Filename){
		//	return 0;
		//}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		public void Dispose() {
			this._DTE.Clear();
			this._FileName = "";
		}

		/// <summary>
		/// Trouver une entré dans la table de jeu qui corestpond a la valeur hexa
		/// </summary>
		/// <param name="hex">Valeur hexa a rechercher dans la TBL</param>
		/// <param name="showSpecialValue">Afficher les valeurs de fin de block et de ligne</param>
		/// <returns></returns>
		public string FindTBLMatch(string hex, bool showSpecialValue){
			string rtn = "#";
			DTE dte;
			for (int i=0; i < this._DTE.Count; i++){
				dte = (DTE)this._DTE[i];
				if (dte.Entry == hex){
					rtn = dte.Value;
					break;
				}

				if (showSpecialValue){
					if (dte.Entry == ("/" + hex)){
						rtn = "<end>";
						break;
					}else if (dte.Entry == ("*" + hex)){
						rtn = "<ln>";
						break;
					}
				}
			}

			return rtn;
		}

		/// <summary>
		/// Trouver une entré dans la table de jeu qui corestpond a la valeur hexa
		/// </summary>
		/// <param name="hex">Valeur hexa a rechercher dans la TBL</param>
		/// <param name="showSpecialValue">Afficher les valeurs de fin de block et de ligne</param>
		/// <returns></returns>
		public string FindTBLMatch(string hex){
			string rtn = "#";
			DTE dte;
			for (int i=0; i < this._DTE.Count; i++){
				dte = (DTE)this._DTE[i];
				if (dte.Entry == hex){
					rtn = dte.Value;
					break;
				}
			}

			return rtn;
		}

		/// <summary>
		/// Trouver une entré dans la table de jeu qui corestpond a la valeur hexa
		/// </summary>
		/// <param name="hex">Valeur hexa a rechercher dans la TBL</param>
		/// <param name="showSpecialValue">Afficher les valeurs de fin de block et de ligne</param>
		/// <returns></returns>
		public string FindTBLMatch(string hex, bool showSpecialValue, bool NotShowDTE){
			string rtn = "#";
			DTE dte;
			for (int i=0; i < this._DTE.Count; i++){
				dte = (DTE)this._DTE[i];

				if (dte.Entry == hex){
					if (NotShowDTE){
						if (dte.Type == DTEType.DualTitleEncoding){						
							break;
						}
						else{
							rtn = dte.Value;
							break;
						}
					}
					else{
						rtn = dte.Value;					
						break;
					}
				}

				if (showSpecialValue){
					if (dte.Entry == ("/" + hex)){
						rtn = "<end>";
						break;
					}else if (dte.Entry == ("*" + hex)){
						rtn = "<ln>";
						break;
					}
				}
			}

			return rtn;
		}

		/// <summary>
		/// Chargé le fichier dans l'objet
		/// </summary>
		/// <returns>Retoune vrai si le fichier est bien charger</returns>
		public bool Load(){	
			//Vide la collection
			this._DTE.Clear();
			//ouverture du fichier

			if (!File.Exists(this._FileName)){
				FileStream fs = File.Create(this._FileName); 
				fs.Close();
			}

			StreamReader TBLFile = new StreamReader(this._FileName, Encoding.ASCII); 
			
			if (TBLFile.BaseStream.CanRead){
				//lecture du fichier jusqua la fin et séparation par ligne
				char[] sepEndLine = {'\n'}; //Fin de ligne
				char[] sepEqual = {'='}; //Fin de ligne
				string[] line = TBLFile.ReadToEnd().Split(sepEndLine);
                			
				//remplir la collection de DTE : this._DTE
				for (int i =0; i <line.Length; i++){
					//parser le ligne
					string[] info = line[i].Split(sepEqual);  

					//ajout a la collection (ne prend pas encore en charge le Japonais)
					DTE dte = new DTE();
					try{
						switch (info[0].Length){
							case 2:
								if (info[1].Length == 2)
									dte = new DTE(info[0], info[1].Substring(0, info[1].Length -1), DTEType.ASCII);
								else
									dte = new DTE(info[0], info[1].Substring(0, info[1].Length -1), DTEType.DualTitleEncoding);
								break;
							case 4: // >2								
								dte = new DTE(info[0], info[1].Substring(0, info[1].Length -1), DTEType.MultipleTitleEncoding);
								break;
							default:
								continue;
						}
					}
					catch(IndexOutOfRangeException){
						switch (info[0].Substring(0,1)){
							case @"/":
								dte = new DTE(info[0].Substring(0, info[0].Length -1), "", DTEType.EndBlock);
								break;							
							case @"*":
								dte = new DTE(info[0].Substring(0, info[0].Length -1), "", DTEType.EndLine);
								break;
								//case @"\":
							default:
								continue;
						}
					}	
					catch(ArgumentOutOfRangeException) { //Du a une entre qui a 2 = de suite... EX:  XX==
						dte = new DTE(info[0], "=", DTEType.DualTitleEncoding);
					}
					
					this._DTE.Add(dte);
				}
			
				TBLFile.Close();

				//Chargement des bookmark
				LoadBookMark();
				return true;
			}else{
				return false;
			}
		}

		/// <summary>
		/// Chargé le fichier dans l'objet en lui passant le chemin du fichier en parametre
		/// </summary>
		/// <returns>Retoune vrai si le fichier est bien charger</returns>
		public bool Load(string FileName){
			//Cleen Object !
			this.Dispose();

			this._FileName = FileName; 
			
			return this.Load();
		}

		private void LoadBookMark(){
			StreamReader TBLFile = new StreamReader(this._FileName, Encoding.ASCII); 

			Favoris fav = null;
			string[] lineSplited;
						
			if (TBLFile.BaseStream.CanRead){
				//lecture du fichier jusqua la fin et séparation par ligne
				char[] sepEndLine = {'\n'}; //Fin de ligne				
				string[] line = TBLFile.ReadToEnd().Split(sepEndLine);

				for (int i=0; i<line.Length; i++){
					try{
						if (line[i].Substring(0,1) == "("){
							fav = new Favoris();
							lineSplited = line[i].Split(new char[]{')'});
							fav.Name = lineSplited[1].Substring(0, lineSplited[1].Length -1);

							lineSplited = line[i].Split(new char[]{'h'});
							fav.Position = lineSplited[0].Substring(1, lineSplited[0].Length -1);
							this._Favoris.Add(fav);  
						}
					}
					catch{}
				}

			}
		}

		/// <summary>
		/// Enregistrer dans le fichier 
		/// </summary>
		/// <returns>Retourne vrai si le fichier à été bien enregistré</returns>
		public bool Save(){
			//ouverture du fichier
			FileStream myFile = new FileStream(this._FileName, FileMode.Create, FileAccess.Write); 	
			StreamWriter TBLFile = new StreamWriter(myFile, Encoding.ASCII); 
			
			if (TBLFile.BaseStream.CanWrite){
				DTE dte;

				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type != DTEType.EndBlock && 
						dte.Type != DTEType.EndLine){ //Si ce n'est pas une fin de ligne ou fin de block
						TBLFile.WriteLine(dte.Entry + "=" + dte.Value); 
					}else
						TBLFile.WriteLine(dte.Entry);
				}
			}
			//Ecriture de 2 saut de ligne a la fin du fichier. 
			//(obligatoire pour certain logiciel utilisant les TBL)
			TBLFile.WriteLine();
			TBLFile.WriteLine();

			//Ferme le fichier TBL
			TBLFile.Close();

			return true;
		}

		/// <summary>
		/// Enregistrer dans un autre fichier et attribu FileName a l'objet comme etant le fichier en cours.
		/// </summary>
		/// <param name="FileName">Nom du fichier sur lequel enregistrer la TBL</param>
		/// <returns>Retourne vrai si le fichier à été bien enregistré</returns>
		public bool SaveAs(string FileName){
			this._FileName = FileName;
			
			return this.Save();
		}

		/// <summary>
		/// Enregistrer dans un autre fichier.
		/// </summary>
		/// <param name="FileName">Nom du fichier sur lequel enregistrer la TBL</param>
		/// <returns>Retourne vrai si le fichier à été bien enregistré</returns>
		public bool Save(string FileName){
			//garder en le path dans une variable tempon
			string Filetmp = this._FileName;
			this._FileName = FileName;

			//Enregister le fichier
			bool rtn = this.Save(); 
			this._FileName = Filetmp;

			//Valeur de retour
			return rtn;
		}

		/// <summary>
		/// Ajouter un element a la collection
		/// </summary>
		/// <param name="dte">objet DTE a ajouter fans la collection</param>
		public void Add(DTE dte){
			this._DTE.Add(dte);
		}
		
		/// <summary>
		/// Effacer un element de la collection a partir d'un objet DTE
		/// </summary>
		/// <param name="dte"></param>
		public void Remove(DTE dte){
			this._DTE.Remove(dte);
		}

		/// <summary>
		/// Effacer un element de la collection avec son index dans la collection
		/// </summary>
		/// <param name="index">Index de l'element a effacer</param>
		public void Remove(int index){
			this._DTE.RemoveAt(index); 			 
		}

		/// <summary>
		/// Recherche un élément dans la TBL
		/// </summary>
		/// <param name="dte">Objet DTE a rechercher dans la TBL</param>
		/// <returns>Retourne la position ou ce trouve cette élément dans le tableau</returns>
		public int Find(DTE dte){
			return this._DTE.BinarySearch(dte);
		}

		/// <summary>
		/// Recherche un élément dans la TBL
		/// </summary>
		/// <param name="Entry">Entrée sous forme hexadécimal (XX)</param>
		/// <param name="Value">Valeur de l'entré</param>
		/// <returns>Retourne la position ou ce trouve cette élément dans le tableau</returns>
		public int Find(string Entry, string Value){
			DTE dte = new DTE(Entry, Value); 
			return this._DTE.BinarySearch(dte);
		}

		/// <summary>
		/// Recherche un élément dans la TBL
		/// </summary>
		/// <param name="Entry">Entrée sous forme hexadécimal (XX)</param>
		/// <param name="Value">Valeur de l'entré</param>		
		/// <param name="Type">Type de DTE</param>
		/// <returns>Retourne la position ou ce trouve cette élément dans le tableau</returns>
		public int Find(string Entry, string Value, DTEType Type){
			DTE dte = new DTE(Entry, Value, Type); 
			return this._DTE.BinarySearch(dte);
		}
		#endregion

		#region Propriétés
		/// <summary>
		/// Chemin d'acces au fichier (path) 
		/// La fonction load doit etre appeler pour rafaichir la fonction
		/// </summary>
		[ReadOnly(true)]
		public string FileName{
			get{
				return this._FileName;
			}
			set{
				this._FileName = value;
			}
		}

		/// <summary>
		/// Total d'élement dans l'objet TBL
		/// </summary>
		public int Length{
			get{
				return this._DTE.Count;
			}
		}

		/// <summary>
		/// Avoir acess au Bookmark
		/// </summary>
		[Browsable(false)]
		public ArrayList BookMark{
			get{
				return this._Favoris;
			}
		}
		
		/// <summary>
		/// Obtenir le total d'entré DTE dans la Table
		/// </summary>
		public short TotalDTE {
			get{
				DTE dte;
				short total =0;
				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type == DTEType.DualTitleEncoding){
						total++;
					}
				}

				return total;
			}
		}
        
		/// <summary>
		/// Obtenir le total d'entré MTE dans la Table
		/// </summary>
		public short TotalMTE {
			get{
				DTE dte;
				short total =0;
				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type == DTEType.MultipleTitleEncoding){
						total++;
					}
				}

				return total;
			}
		}

		/// <summary>
		/// Obtenir le total d'entré ASCII dans la Table
		/// </summary>
		public short TotalASCII {
			get{
				DTE dte;
				short total =0;
				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type == DTEType.ASCII){
						total++;
					}
				}

				return total;
			}
		}

		/// <summary>
		/// Obtenir le total d'entré Invalide dans la Table
		/// </summary>
		public short TotalInvalid {
			get{
				DTE dte;
				short total =0;
				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type == DTEType.Invalid){
						total++;
					}
				}

				return total;
			}
		}

		/// <summary>
		/// Obtenir le total d'entré Japonais dans la Table
		/// </summary>
		public short TotalJaponais {
			get{
				DTE dte;
				short total =0;
				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type == DTEType.Japonais){
						total++;
					}
				}

				return total;
			}
		}

		/// <summary>
		/// Obtenir le total d'entré Fin de ligne dans la Table
		/// </summary>
		public short TotalEndLine {
			get{
				DTE dte;
				short total =0;
				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type == DTEType.EndLine){
						total++;
					}
				}

				return total;
			}
		}

		/// <summary>
		/// Obtenir le total d'entré Fin de Block dans la Table
		/// </summary>
		public short TotalEndBlock {
			get{
				DTE dte;
				short total =0;
				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type == DTEType.EndBlock){
						total++;
						}
				}

				return total;
			}
		}

		/// <summary>
		/// Renvoi le caractere de fin de block
		/// </summary>
		public string EndBlock {
			get{
				DTE dte;
				string rtn = ""; //Valeur de retour
				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type == DTEType.EndBlock){
						rtn = dte.Entry;
						break;
					}
				}

				return rtn;
			}
		}

		/// <summary>
		/// Renvoi le caractere de fin de ligne
		/// </summary>
		public string EndLine {
			get{
				DTE dte;
				string rtn = ""; //Valeur de retour
				for (int i=0; i<this._DTE.Count; i++){
					dte = (DTE)this._DTE[i];
					if (dte.Type == DTEType.EndLine){
						rtn = dte.Entry;
						break;
					}
				}

				return rtn;
			}
		}
		#endregion

	}
}
