using System;

namespace VRS.Library.Table.TBL {
	/// <summary>
	/// Objet représentant un DTE.
	/// </summary>
	public class DTE {
		/// <summary>Valeur du DTE</summary>
		private string  _Value;
		/// <summary>Nom du DTE</summary>
		private string  _Entry;
		/// <summary>Type de DTE</summary>
		private DTEType _Type;
		
		#region Constructeurs
		/// <summary>
		/// Constructeur principal
		/// </summary>
		public DTE(){
			this._Entry = "";
			this._Type = DTEType.Invalid;
			this._Value = "";
		}

		/// <summary>
		/// Contructeur permetant d'ajouter une entrée et une valeur
		/// </summary>
		/// <param name="Entry">Nom du DTE</param>
		/// <param name="Value">Valeur du DTE</param>
		public DTE(string Entry, string Value) {
			this._Entry = Entry.ToUpper();
			this._Value = Value;
			this._Type = DTEType.DualTitleEncoding;
		}

		/// <summary>
		/// Contructeur permetant d'ajouter une entrée, une valeur et une description
		/// </summary>
		/// <param name="Entry">Nom du DTE</param>
		/// <param name="Value">Valeur du DTE</param>
		/// <param name="Description">Description du DTE</param>
		/// <param name="Type">Type de DTE</param>
		public DTE(string Entry, string Value, DTEType Type) {
			this._Entry = Entry.ToUpper();
			this._Value = Value;
			this._Type = Type;
		}
		#endregion

		#region Propriétés
		/// <summary>
		/// Nom du DTE
		/// </summary>
		public string Entry{
			set{
				this._Entry = value.ToUpper();
			}
			get{
				return this._Entry;
			}			
		}

		/// <summary>
		/// Valeur du DTE
		/// </summary>
		public string Value{
			set{
				this._Value = value;
			}
			get{
				return this._Value;
			}			
		}

		/// <summary>
		/// Type de DTE
		/// </summary>
		public DTEType Type{
			set{
				this._Type = value;
			}
			get{
				return this._Type;
			}			
		}
		#endregion

		#region Méthodes
		/// <summary>
		/// Cette fonction permet de retourner le DTE sous forme : [Entry]=[Valeur]
		/// </summary>
		/// <returns>Retourne le DTE sous forme : [Entry]=[Valeur]</returns>
		public override string ToString() {
			if (this._Type != DTEType.EndBlock &&
				this._Type != DTEType.EndLine)
				return this._Entry + "=" + this._Value;
			else
				return this._Entry;
		}
		#endregion

        #region Methodes Static
        public static DTEType TypeDTE(DTE DTEValue) {
            try{
                switch (DTEValue._Entry.Length) {
                    case 2:
                        if (DTEValue._Value.Length == 2)
                            return DTEType.ASCII;
                        else
                            return DTEType.DualTitleEncoding;
                    case 4: // >2								
                        return DTEType.MultipleTitleEncoding;
                }
            }
            catch (IndexOutOfRangeException) {
                switch (DTEValue._Entry) {
                    case @"/":
                        return DTEType.EndBlock;
                    case @"*":
                        return DTEType.EndLine;
                    //case @"\":
                }
            }
            catch (ArgumentOutOfRangeException) { //Du a une entre qui a 2 = de suite... EX:  XX==
                return DTEType.DualTitleEncoding;
            }

            return DTEType.Invalid;
        }

        public static DTEType TypeDTE(string DTEValue) {
            try {
                if (DTEValue.Length == 1)
                    return DTEType.ASCII;
                else if (DTEValue.Length == 2)
                    return DTEType.DualTitleEncoding;
                else if (DTEValue.Length > 2)
                    return DTEType.MultipleTitleEncoding;
            }
            catch (IndexOutOfRangeException) {
                switch (DTEValue) {
                    case @"/":
                        return DTEType.EndBlock;
                    case @"*":
                        return DTEType.EndLine;
                    //case @"\":
                }
            }
            catch (ArgumentOutOfRangeException) { //Du a une entre qui a 2 = de suite... EX:  XX==
                return DTEType.DualTitleEncoding;
            }

            return DTEType.Invalid;
        }
        #endregion
    }
}
