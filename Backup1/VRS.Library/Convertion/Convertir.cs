using System;
using System.Text;

namespace VRS.Library.Convertion
{
	/// <summary>
	/// Description résumée de Convertion.
	/// </summary>
	public sealed class Convertir
	{
		/// <summary>
		/// Convertir des Valeur decimal en Hexadecimal
		/// </summary>
		/// <param name="DecimalNumber">Valeur a convertir</param>
		/// <returns>Retoune une chaine sous forme hexadecimal</returns>
		public static string DecimalToHexa(long DecimalNumber){
			try{
				return Convert.ToString(DecimalNumber, 16).ToUpper(); 
			}
			catch{
				return "-1"; //Erreur de conversion
			}
		}

		/// <summary>
		/// Convertir en un nombre hexadecimal en decimal 
		/// </summary>
		/// <param name="HexaString">Chaine sous forme hexadicimal</param>
		/// <returns>Retourne un nombre decimal representant la valeur decimal</returns>
		public static long HexaToDecimal(string HexaString){
			try{
				return Convert.ToInt32(HexaString, 16); 
			}
			catch{
				return -1; //Erreur de conversion
			}
		}

		/// <summary>
		/// Converir un tableau de chars en une string
		/// </summary>
		/// <param name="chaine">Tableau de chars</param>
		/// <returns>Chaine de caractere (string)</returns>
		public static string CharArrayToString(char[] chaine){
			String myString = new String(chaine);
			
			return myString.ToString();
		}

		/// <summary>
		/// Converir un tableau de chars en une string
		/// </summary>
		/// <param name="chaine">Tableau de chars</param>
		/// <returns>Chaine de caractere (string)</returns>
		public static string CharArrayToString_7bit(char[] chaine){
			string buffer = "";
			int lenght = chaine.Length;

			for (int i=0; i<lenght; i++ ){
				buffer += Convert.ToString(chaine[i]);
			}
			return buffer;
		}

		/// <summary>
		/// Convertir une chaine sour forme XX XX XX ... hexadécimal
		/// </summary>
		/// <param name="chaine">chaine de caracteres</param>
		/// <returns>Retourne une chaine sous forme XXXXXX ... chaque XX represente 1 char sous forme hexa</returns>
		public static string StringHexa(string chaine){
			return StringHexa(chaine, false);
		}
        
		/// <summary>
		/// Convertir une chaine sour forme XX XX XX ... hexadécimal
		/// </summary>
		/// <param name="chaine">Tableau de char</param>
		/// <returns>Retourne une chaine sous forme XX XX XX ... chaque XX represente 1 char sous forme hexa</returns>
		public static string StringHexa(char[] chaine){
			string buffer = CharArrayToString(chaine);
			return StringHexa(buffer);
		}

		/// <summary>
		/// Convertir une chaine sour forme XX XX XX ... hexadécimal
		/// </summary>
		/// <param name="chaine">Tableau de char</param>
		/// <param name="AddSpace">Ajouté une espace entre chaque lettre</param>
		/// <returns>Retourne une chaine sous forme XX XX XX ... chaque XX represente 1 char sous forme hexa</returns>
		public static string StringHexa(char[] chaine, bool AddSpace){
			string buffer = CharArrayToString(chaine);
			return StringHexa(buffer, AddSpace);
		}

		/// <summary>
		/// Convertir une chaine sour forme XX XX XX ... hexadécimal
		/// </summary>
		/// <param name="chaine">chaine de caracteres</param>
		/// <param name="AddSpace">Ajouté une espace entre chaque lettre</param>
		/// <returns>Retourne une chaine sous forme XX XX XX ... chaque XX represente 1 char sous forme hexa</returns>
		public static string StringHexa(string chaine, bool AddSpace){
			string buffer = "";

			if (!AddSpace)
				for (int i=0; i< chaine.Length; i++)
					buffer += Hexa(chaine[i].ToString());
			else
				for (int i=0; i< chaine.Length; i++)
					buffer += Hexa(chaine[i].ToString()) + " ";


			return buffer.Trim();
		}

		/// <summary>
		/// Convertir une lettre en sa valeur hexa (xx)  
		/// - 7 bits ANSI
		/// </summary>
		/// <remarks>
		/// La fonction a besion d'un débuggage
		/// Elle ne retourne pas les bonne valuer  quand les données ne son pas des valeurs ASCII
		/// </remarks> 
		/// <param name="Lettre">Un caractere</param>
		/// <returns>Retourne une chaine sous forme XX representant une valeur hexadecimal</returns>
		public static string Hexa(string Lettre) {
			return ((byte)Lettre[0]).ToString("X2");
		}

		/// <summary>
		/// Convertir une chaine sour forme XX XX XX ... hexadécimal a partir d'une chaine "Unicode Little Endian"
		/// </summary>
		/// <param name="UnicodeString">Chaine au format "Unicode Little Endian"</param>
		/// <returns>Retourne une chaine sous forme XXXXXX ... chaque XX represente 1 char sous forme hexa</returns>
		public static string StringHexa_LittleEndian(string UnicodeString){
			return StringHexa_LittleEndian(UnicodeString, false) ;
		}

		/// <summary>
		/// Convertir une chaine sour forme XX XX XX ... hexadécimal a partir d'une chaine "Unicode Little Endian"
		/// </summary>
		/// <param name="UnicodeString">Chaine au format "Unicode Little Endian"</param>
		/// <param name="AddSpace">Ajouté une espace entre chaque lettre</param>
		/// <returns>Retourne une chaine sous forme XXXXXX ... chaque XX represente 1 char sous forme hexa</returns>
		public static string StringHexa_LittleEndian(string UnicodeString, bool AddSpace){
			if (UnicodeString != null){
				byte[] bytes = ASCIIEncoding.Unicode.GetBytes(UnicodeString);

				StringBuilder builder = new StringBuilder(bytes.Length);

				if (AddSpace)
					for (int i=0; i<bytes.Length /2; i++)
						builder.Append(bytes[i].ToString("X2") + " ");
				else
					for (int i=0; i<bytes.Length /2; i++)
						builder.Append(bytes[i].ToString("X2"));

				return builder.ToString();
			}else
				return "";
		}

		/// <summary>
		/// Obtenir le code RGB a partir des valeur Rouge, Vert Bleu
		/// </summary>
		/// <param name="R">Rouge (entre 0 et 255)</param>
		/// <param name="G">Vert (entre 0 et 255)</param>
		/// <param name="B">Bleu (entre 0 et 255)</param>
		/// <returns>Retourne la valeur RGB</returns>
		public static uint RGB(long R, long G, long B){
			string hex = DecimalToHexa(R) +	DecimalToHexa(G) + DecimalToHexa(B);
			
			return (uint)HexaToDecimal(hex);
		}
	}
}