using System;
using System.Resources;
using System.Globalization;
using System.Threading;
using VRS;

namespace VRS.IDE.Resource {
	/// <summary>
	/// Description résumée de Ressources.
	/// </summary>
	public class Resources {
		ResourceManager _resManager;
		Culture			_cult = Culture.Francais;		

		public Resources(Culture cult) {
			//Chargement de la culture du logiciel			
			SetCulture(cult);

			this._resManager = new ResourceManager(App.DefaultRessourcePath, typeof(App).Assembly);
		}

		public string GetString(string Name){
			return _resManager.GetString(Name);
		}

		public void SetCulture(Culture Cult){
			//TODO: charger le type de resource (Anglais ou Francais)
			switch(Cult){
				case Culture.Francais:
					Thread.CurrentThread.CurrentUICulture  = new CultureInfo(""); //Culture par defaut
					break;
			}

			this._cult = Cult;
		}
	}
}
