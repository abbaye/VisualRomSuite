using System;

namespace VRS.Library.Projet
{
	/// <summary>
	/// Type de projet (Seulement Super Nintendo pour le moment)
	/// </summary>
	public enum ProjectType{
		SuperNintendo = 0
	}

	/// <summary>
	/// Type de fichier (a completer)
	/// </summary>
	public enum FileType{
		Texte,
		DTE,
		Table,
	}

	/// <summary>
	/// Mode d'extraction / insertion
	/// </summary>
	public enum TextMode{
		_16Bits = 0,
		_24Bits = 1,
	}
	
	/// <summary>
	/// Priorité des tâches
	/// </summary>
	public enum TaskPriority{
		Faible,
		Normal,
		Haute
	}

	/// <summary>
	/// Type d'erreur lors du chargement et enregistrement du project
	/// </summary>
	public enum ProjectError{
		FileNotFound,
		TableSectionError,
		HexaSnapShotSectionError,
		NoError,					//Aucune erreur
        TexteSectionError,
		BookmarkSectionError,
		TaskListSectionError,
        ProjectSectionLoadError,
		FixeTableSectionError,
		UnknowError					//Type d'erreur inconnu
	}
}
