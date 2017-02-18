using System;
using System.IO;
using System.Text;

using VRS.Library.Convertion;

namespace VRS.Library.IO {
	/// <summary>
	/// Description résumée de ExtractData.
	/// </summary>
	public sealed class ExtractData {
		public static string AspireString(string pStartPos, string pStopPos, string FileName, bool LittleEndian){
			int startPos = (int)Convertir.HexaToDecimal(pStartPos);
			int stopPos = (int)Convertir.HexaToDecimal(pStopPos);
			string FinalData = "";
			int ExtractLenght = stopPos - startPos + 1;
			
            if (ExtractLenght > 0 )
				if (File.Exists(FileName)){
					FileStream file = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
					BinaryReader reader = null;
					
					if (LittleEndian)
						reader = new BinaryReader(file, Encoding.Unicode); 
					else
						reader = new BinaryReader(file);

					//Extraction des donner
					if (reader.BaseStream.CanRead){
						//Positione au debut
						reader.BaseStream.Seek(startPos, SeekOrigin.Begin);
						
						char[] data = reader.ReadChars(ExtractLenght);

						FinalData = Convertir.CharArrayToString(data); 
					}

					reader.Close();
					file.Close();
				}		

			return FinalData;
		}

		public static string AspireString(string StartPos, string StopPos, string FileName){
			return AspireString(StartPos, StopPos, FileName, false);
		}

		public static string AspireString(string FileName){
			FileInfo info = new FileInfo(FileName); 
   
			return AspireString("0", Convertir.DecimalToHexa(info.Length), FileName);   
		}
	}
}
