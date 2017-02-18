using System;

namespace VRS.Library.Projet {
	/// <summary>
	/// Description résumée de TextFile.
	/// </summary>
	public sealed class TextFile {
		private string _Description;
		private string _Name;	
		private string _RelativePath;
		private string _Extraction_TextBank_Start;
		private string _Extraction_TextBank_Stop;
		private string _Extraction_PointeurBank_Start;
		private string _Extraction_PointeurBank_Stop;
		private string _Extraction_HeaderAdjustement_Plus;
		private string _Extraction_HeaderAdjustement_Moins;
		private string _insertion_TextBankStart;
		private string _insertion_TextBankStop;
		private string _insertion_PointeurBankStart;
		private string _insertion_PointeurBankStop;
		private string _insertion_HeaderAdjustementPlus;
		private string _insertion_HeaderAdjustementMoins;
		private string _TablePath;		
		private bool   _AucunPointeur;
		private bool _isTextPointeurTable;		
		private bool _InsertAtCompil;
		private TextMode _Mode;

		/// <summary>
		/// Cle permetant de retrouver l'objet dans le projet
		/// </summary>
		public string key;

		/// <summary>
		/// Constructeur par defaut
		/// </summary>
		public TextFile() {

		}

		public string Name{
			get{
				return this._Name; 
			}
			set{
				this._Name = value;
			}
		}

		public string Description{
			get{
				return this._Description; 
			}
			set{
				this._Description = value;
			}
		}

		public string RelativePath{
			get{
				return this._RelativePath; 
			}
			set{
				this._RelativePath = value;
			}
		}

		public string Extraction_TextBank_Start{
			get{
				return this._Extraction_TextBank_Start; 
			}
			set{
				this._Extraction_TextBank_Start = value;
			}
		}

		public string Extraction_TextBank_Stop{
			get{
				return this._Extraction_TextBank_Stop; 
			}
			set{
				this._Extraction_TextBank_Stop = value;
			}
		}

		public string Extraction_PointeurBank_Start{
			get{
				return this._Extraction_PointeurBank_Start; 
			}
			set{
				this._Extraction_PointeurBank_Start = value;
			}
		}

		public string Extraction_PointeurBank_Stop{
			get{
				return this._Extraction_PointeurBank_Stop; 
			}
			set{
				this._Extraction_PointeurBank_Stop = value;
			}
		}

		public string Extraction_HeaderAdjustement_Plus{
			get{
				return this._Extraction_HeaderAdjustement_Plus; 
			}
			set{
				this._Extraction_HeaderAdjustement_Plus = value;
			}
		}

		public string Extraction_HeaderAdjustement_Moins{
			get{
				return this._Extraction_HeaderAdjustement_Moins; 
			}
			set{
				this._Extraction_HeaderAdjustement_Moins = value;
			}
		}

		public string Insertion_TextBankStart{
			get{
				return this._insertion_TextBankStart; 
			}
			set{
				this._insertion_TextBankStart = value;
			}
		}

		public string Insertion_TextBankStop{
			get{
				return this._insertion_TextBankStop; 
			}
			set{
				this._insertion_TextBankStop = value;
			}
		}

		public string Insertion_PointeurBankStart{
			get{
				return this._insertion_PointeurBankStart; 
			}
			set{
				this._insertion_PointeurBankStart = value;
			}
		}

		public string Insertion_PointeurBankStop{
			get{
				return this._insertion_PointeurBankStop; 
			}
			set{
				this._insertion_PointeurBankStop = value;
			}
		}

		public string Insertion_HeaderAdjustementPlus{
			get{
				return this._insertion_HeaderAdjustementPlus; 
			}
			set{
				this._insertion_HeaderAdjustementPlus = value;
			}
		}

		public string Insertion_HeaderAdjustementMoins{
			get{
				return this._insertion_HeaderAdjustementMoins; 
			}
			set{
				this._insertion_HeaderAdjustementMoins = value;
			}
		}

		public string TablePath{
			get{
				return this._TablePath; 
			}
			set{
				this._TablePath = value;
			}
		}

		public bool AucunPointeur{
			get{
				return this._AucunPointeur; 
			}
			set{
				this._AucunPointeur = value;
			}
		}

		public bool IsTextPointeurTable{
			get{
				return this._isTextPointeurTable; 
			}
			set{
				this._isTextPointeurTable = value;
			}
		}

		public bool InsertAtCompil{
			get{
				return this._InsertAtCompil; 
			}
			set{
				this._InsertAtCompil = value;
			}
		}

		public TextMode Mode{
			get{
				return this._Mode; 
			}
			set{
				this._Mode = value;
			}
		}
	}
}
