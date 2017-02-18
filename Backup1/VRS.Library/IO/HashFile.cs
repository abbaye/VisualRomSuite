using System;
using System.IO;
using System.Security.Cryptography;

using VRS.Library.Convertion;

namespace VRS.Library.IO {
	/// <summary>
	/// Description résumée de HashFile.
	/// </summary>
	public class HashFile {
		public HashFile() {

		}

		public static string GetMD5Hexa(string FileName){

			if (File.Exists(FileName)){
				MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
				FileStream file = new FileStream(FileName, FileMode.Open);
   
				byte[] table = md5.ComputeHash(file); 
				 
				file.Close();

				return Convertir.StringHexa(table.ToString());				
			}else
				return "";
		}
	}
}
