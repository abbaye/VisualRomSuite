using System;
using CodeMax;

namespace VRS.Library.UserControl
{
	/// <summary>
	/// Type de noeud selectionn� dans l'explorateur de projet
	/// </summary>
	public enum ExplorerNodeType{
		TextFile,
		HexaFile,
		TBLFile,
		Root,
		Nothing,
		Folder,
		Rom,
		WorkRom
	}

	/// <summary>
	/// Type de bordure de l'editeur de texte
	/// </summary>
	public enum TextEditorBorder{
		Thin,
		Node,
		Static
	}
}
