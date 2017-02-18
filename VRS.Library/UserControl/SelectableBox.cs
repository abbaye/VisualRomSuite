using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using VRS.Library.Win32;

namespace VRS.Library.UserControl {
	/// <summary>
	/// Description résumée de SelectablePictureBox.
	/// </summary>
	public class SelectableBox : System.Windows.Forms.UserControl {
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SelectableBox() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			//Applique un style sur le control
			this.SetStyle(ControlStyles.Selectable, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);

			//Creation de la bordure autour du control
			StaticBorder.ThinBorder(this.Handle.ToInt32(), true);
			
		}

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			// 
			// SelectableBox
			// 
			this.Name = "SelectableBox";
			this.Size = new System.Drawing.Size(200, 160);

		}
		#endregion

	}
}
