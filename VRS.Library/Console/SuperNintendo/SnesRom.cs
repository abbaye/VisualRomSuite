using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

using VRS.Library.IO;
using VRS.Library.Convertion;

namespace VRS.Library.Console.SuperNintendo {
	/// <summary>
	/// Objet representant une ROM Super Nintendo
	/// </summary>
	public class SnesRom {
		//Variables membres		
		private string _FileName;		// Chemin du Fichiers ROM sur le disque
		private string _RomName;		// Nom interne de la rom
		private string _Version;		// Version de la rom
		private string _Checksum;		// CheckSum
		private string _CompChecksum;	// Complement du CheckSum
		private string _ResetVector;	// Rom Reset
		private string _NmiVblVector;	// Vecteur				
		private int	   _PostitionName;	// Position du nom dans la rom
		private RomType _RomType;		// Type de ROM
		private FormatAffichage			 _FormatAffichage;	// Format d'affichage
		private InformationCartouche	 _InfoCart;			// Type de cartouche
		private ROMSize					 _RomSizeMB;		// Taille en MegaOctet de la Rom
		private RAMSize					 _RamSizeKB;		// Taille en KiloOctet de la RAM Cartouche
		private Country					 _Country;			// Pays de manufacturation
		private Compagnie				 _Compagnie;		// Code de licence
		private string					 _MD5;
        
		#region Constructeurs
		/// <summary>
		/// 
		/// </summary>
		public SnesRom() {
		
		}

		/// <summary>
		/// Charger les informations sur le rom en mémoire
		/// </summary>
		/// <param name="FileName">Chemin vers le fichier rom (path)</param>
		public SnesRom(string FileName){
			//Charger la rom dans l'objet
			if (File.Exists(FileName)){
				this._FileName = FileName;
				this.Load();
			}
			else
				throw new FileNotFoundException();
		}
		#endregion

		#region Méthodes
		/// <summary>
		/// Chargé en mémoire le fichier Rom spécifier par this._FileName
		/// </summary>
		/// <returns>Return true si le fichier est bien chargé</returns>
		public bool Load(){
			if (File.Exists(this._FileName)){
				if (SnesRom.isValid(this._FileName) ){ //Verifie si le fichier Rom valide
					//Variable de Verification pour determiner de quel type est la rom a charg
					string CheckLow1   = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("81DC", "81DD", this.FileName, true));
					string CheckLow2   = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("81DE", "81DF", this.FileName, true));
					string CheckLow1b  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("7FDC", "7FDD", this.FileName, true));
					string CheckLow2b  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("7FDE", "7FDF", this.FileName, true));
					string CheckHigh1  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("101DC", "101DD", this.FileName, true));
					string CheckHigh2  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("101DE", "101DF", this.FileName, true));
					string CheckHigh1b = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("FFDC", "FFDD", this.FileName, true));
					string CheckHigh2b = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("FFDE", "FFDF", this.FileName, true));
					long ValCheckLowb  = Convertir.HexaToDecimal(CheckLow1b) + Convertir.HexaToDecimal(CheckLow2b); 
					long ValCheckHigh  = Convertir.HexaToDecimal(CheckHigh1) + Convertir.HexaToDecimal(CheckHigh2);
					long ValCheckHighb = Convertir.HexaToDecimal(CheckHigh1b)+ Convertir.HexaToDecimal(CheckHigh2b);
					long ValCheckLow   = Convertir.HexaToDecimal(CheckLow1)  + Convertir.HexaToDecimal(CheckLow2);
			
					//Extraire les informations sur la rom dependant du type de rom
					if(ValCheckLow == 65535){ //LowRom 1
						this._RomType = RomType.LowRom_School_1;
						this._RomName = ExtractData.AspireString("81C0", "81D4", this.FileName).TrimEnd(new char[]{' '});
						this._PostitionName = (int)Convertir.HexaToDecimal("81C0");
						this._InfoCart = (InformationCartouche)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("81D6", "81D6", this.FileName, true)));
						this._RomSizeMB = (ROMSize)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("81D7", "81D7", this.FileName, true)));
						this._RamSizeKB = (RAMSize)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("81D8", "81D8", this.FileName, true)));
						this._Country = (Country)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("81D9", "81D9", this.FileName, true)));
						this._Compagnie = (Compagnie)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("81DA", "81DA", this.FileName, true)));
						this._Version      = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("81DB", "81DB", this.FileName, true));
						this._CompChecksum = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("81DC", "81DD", this.FileName, true));
						this._Checksum     = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("81DE", "81DF", this.FileName, true));
						this._NmiVblVector = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("81EA", "81EB", this.FileName, true));
						this._ResetVector  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("81FC", "81FD", this.FileName, true));
					}else if(ValCheckLowb == 65535){ //LowRom 2
						this._RomType = RomType.LowRom_School_2;
						this._RomName = ExtractData.AspireString("7FC0", "7FD4", this.FileName).TrimEnd(new char[]{' '});
						this._PostitionName = (int)Convertir.HexaToDecimal("7FC0");
						this._InfoCart = (InformationCartouche)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("7FD6", "7FD6", this.FileName, true)));
						this._RomSizeMB = (ROMSize)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("7FD7", "7FD7", this.FileName, true)));
						this._RamSizeKB = (RAMSize)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("7FD8", "7FD8", this.FileName, true)));
						this._Country = (Country)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("7FD9", "7FD9", this.FileName, true)));
						this._Compagnie = (Compagnie)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("7FDA", "7FDA", this.FileName, true)));
						this._Version      = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("7FDB", "7FDB", this.FileName, true));
						this._CompChecksum = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("7FDC", "7FDD", this.FileName, true));
						this._Checksum     = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("7FDE", "7FDF", this.FileName, true));
						this._NmiVblVector = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("7FEA", "7FEB", this.FileName, true));
						this._ResetVector  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("7FFC", "7FFD", this.FileName, true));
					}else if(ValCheckHigh == 65535){ // HighRom
						this._RomType = RomType.HighRom;
						this._RomName = ExtractData.AspireString("101C0", "101D4", this.FileName).TrimEnd(new char[]{' '});
						this._PostitionName = (int)Convertir.HexaToDecimal("101C0");
						this._InfoCart = (InformationCartouche)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("101D6", "101D6", this.FileName, true)));
						this._RomSizeMB = (ROMSize)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("101D7", "101D7", this.FileName, true)));
						this._RamSizeKB = (RAMSize)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("101D8", "101D8", this.FileName, true)));
						this._Country = (Country)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("101D9", "101D9", this.FileName, true)));
						this._Compagnie = (Compagnie)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("101DA", "101DA", this.FileName, true)));
						this._Version      = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("101DB", "101DB", this.FileName, true));
						this._CompChecksum = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("101DC", "101DD", this.FileName, true));
						this._Checksum     = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("101DE", "101DF", this.FileName, true));
						this._NmiVblVector = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("101EA", "101EB", this.FileName, true));
						this._ResetVector  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("101FC", "101FD", this.FileName, true));
					}else if(ValCheckHighb == 65535){ // HighRom
						this._RomType = RomType.HighRom;
						this._RomName = ExtractData.AspireString("FFC0", "FFD4", this.FileName).TrimEnd(new char[]{' '});
						this._PostitionName = (int)Convertir.HexaToDecimal("FFC0");
						this._InfoCart = (InformationCartouche)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("FFD6", "FFD6", this.FileName, true)));
						this._RomSizeMB = (ROMSize)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("FFD7", "FFD7", this.FileName, true)));
						this._RamSizeKB = (RAMSize)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("FFD8", "FFD8", this.FileName, true)));
						this._Country = (Country)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("FFD9", "FFD9", this.FileName, true)));
						this._Compagnie = (Compagnie)
							Convertir.HexaToDecimal(
							Convertir.StringHexa_LittleEndian(
							ExtractData.AspireString("FFDA", "FFDA", this.FileName, true)));
						this._Version      = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("FFDB", "FFDB", this.FileName, true));
						this._CompChecksum = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("FFDC", "FFDD", this.FileName, true));
						this._Checksum     = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("FFDE", "FFDF", this.FileName, true));
						this._NmiVblVector = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("FFEA", "FFEB", this.FileName, true));
						this._ResetVector  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("FFFC", "FFFD", this.FileName, true));						
					}

					//Format d'affichage
					if (this._Country.ToString() == "Japon" ||
						this._Country.ToString() == "USA")
						this._FormatAffichage = FormatAffichage.NTSC;
					else
						this._FormatAffichage = FormatAffichage.PAL;

					//MD5 hashing
					this._MD5 = HashFile.GetMD5Hexa(this._FileName); 

					#region DEBUG INFOS : Informations sur la Rom
//					#if DEBUG //Informations de debuggage
//						Debug.WriteLine("DEBUG INFOS START : VRS.Library.Console.SuperNintendo.SnesRom.Load()");
//						Debug.WriteLine("CheckLow1        :" + CheckLow1);
//						Debug.WriteLine("CheckLow2        :" + CheckLow2);
//						Debug.WriteLine("CheckLow1b       :" + CheckLow1b);
//						Debug.WriteLine("CheckLow2b       :" + CheckLow2b);
//						Debug.WriteLine("CheckHigh1       :" + CheckHigh1);
//						Debug.WriteLine("CheckHigh2       :" + CheckHigh2);
//						Debug.WriteLine("CheckHigh1b      :" + CheckHigh1b);
//						Debug.WriteLine("CheckHigh2b      :" + CheckHigh2b);
//						Debug.WriteLine("ValCheckLowb     :" + ValCheckLowb);
//						Debug.WriteLine("ValCheckHigh     :" + ValCheckHigh);
//						Debug.WriteLine("ValCheckHighb    :" + ValCheckHighb);
//						Debug.WriteLine("ValCheckLow      :" + ValCheckLow);
//						Debug.WriteLine("this._RomName    :" + this._RomName);
//						Debug.WriteLine("this._InfoCart   :" + this._InfoCart.ToString());
//						Debug.WriteLine("this._RomSizeMB  :" + this._RomSizeMB.ToString());
//						Debug.WriteLine("this._RamSizeKB  :" + this._RamSizeKB.ToString());
//						Debug.WriteLine("this._Country    :" + this._Country.ToString());
//						Debug.WriteLine("this._Compagnie  :" + this._Compagnie.ToString());
//						Debug.WriteLine("this._version    : 1." + this._Version);
//						Debug.WriteLine("this._CompChecksum :" + this._CompChecksum);
//						Debug.WriteLine("this._Checksum :" + this._Checksum);
//						Debug.WriteLine("this._ResetVector :" + this._ResetVector);
//						Debug.WriteLine("this._NmiVblVector :" + this._NmiVblVector);
//						Debug.WriteLine("this._FormatAffichage :" + this._FormatAffichage);
//						Debug.WriteLine("this._MD5 :" + this._MD5);
//						Debug.WriteLine("DEBUG INFOS END : VRS.Library.Console.SuperNintendo.SnesRom.Load()");
//					#endif
					#endregion
					
					return true;
				}else
					return false;
			}
			else
				throw new FileNotFoundException(); 
		}

		/// <summary>
		/// Verifie que le fichier Rom est valide
		/// </summary>
		/// <param name="FileName">Chemin vers le fichier (path)</param>
		/// <returns>Retourne true si le fichier rom est un fichier de type Super Nintendo</returns>
		public static bool isValid(string FileName){
			if (File.Exists(FileName)){
				//Variable de Verification pour determiner de quel type est la rom a charge
				string CheckLow1   = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("81DC", "81DD", FileName, true));
				string CheckLow2   = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("81DE", "81DF", FileName, true));
				string CheckLow1b  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("7FDC", "7FDD", FileName, true));
				string CheckLow2b  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("7FDE", "7FDF", FileName, true));
				string CheckHigh1  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("101DC", "101DD", FileName, true));
				string CheckHigh2  = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("101DE", "101DF", FileName, true));
				string CheckHigh1b = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("FFDC", "FFDD", FileName, true));
				string CheckHigh2b = Convertir.StringHexa_LittleEndian(ExtractData.AspireString("FFDE", "FFDF", FileName, true));
				long ValCheckLowb  = Convertir.HexaToDecimal(CheckLow1b) + Convertir.HexaToDecimal(CheckLow2b); 
				long ValCheckHigh  = Convertir.HexaToDecimal(CheckHigh1) + Convertir.HexaToDecimal(CheckHigh2);
				long ValCheckHighb = Convertir.HexaToDecimal(CheckHigh1b)+ Convertir.HexaToDecimal(CheckHigh2b);
				long ValCheckLow   = Convertir.HexaToDecimal(CheckLow1)  + Convertir.HexaToDecimal(CheckLow2);
			
				//Fichier Valide ?
				if (ValCheckLow == 65535)   return true; //LowRom 1					
				if (ValCheckLowb == 65535)  return true; //LowRom 2					
				if (ValCheckHigh == 65535)  return true; //HighRom
				if (ValCheckHighb == 65535) return true; //HighRom

				return false; //Fichier non valide
			}
			else
				return false; //Fichier non trouver donc pa valide
		}

		/// <summary>
		/// Chargé en mémoire le fichier Rom spécifier par FileName
		/// </summary>
		/// <param name="FileName">Chemin vers le fichier rom (path)</param>
		/// <returns>Return true si le fichier est bien chargé</returns>
		public bool Load(string FileName){
			if (File.Exists(FileName)){
				this._FileName = FileName; 
				return this.Load();
			}
			else
				throw new FileNotFoundException();
		}

		/// <summary>
		/// Enregistrement des modifications dans la rom
		/// </summary>
		/// <returns></returns>
		//public bool SaveModification(){
		//	return true;
		//}
		#endregion

		#region Propriétés
		/// <summary>
		/// 
		/// </summary>
		public string CheckSum{
			get{
				return this._Checksum;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ComplementCheckSum{
			get{
				return this._CompChecksum;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Country Pays{
			get{
				return this._Country; 
			}
		}

		/// <summary>
		/// Specifie le chemin du fichier a charger (path)
		/// </summary>
		[ReadOnly(true)]
		public string FileName{
			get{
				return this._FileName;
			}
			set{
				if (File.Exists(FileName)){
					this._FileName = value;
					this.Load(value);
				}
				else
					throw new FileNotFoundException();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public RomType TypeRom{
			get{
				return this._RomType; 
			}
		}

		/// <summary>
		/// Nom interne du jeux dans le fichier ROM.
		/// </summary>
		public string RomName{
			get{
				return this._RomName;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public RAMSize RamLength_KiloByte{
			get{
				return this._RamSizeKB; 
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public FormatAffichage Format{
			get{
				return this._FormatAffichage; 
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Version{
			get{
				return "1." + this._Version;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public InformationCartouche CartInfo{
			get{
				return this._InfoCart;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public long Length(){
			if (File.Exists(this._FileName)){
				FileInfo myFile = new FileInfo(this._FileName);
				return myFile.Length;
			}else
				return 0;			
		}

		/// <summary>
		/// 
		/// </summary>
		public string NmiVblVector{
			get{
				return this._NmiVblVector;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ResetVector{
			get{
				return this._ResetVector; 
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string MD5Hash{
			get{				
				return this._MD5;
			}
		}
		#endregion

	}
}
