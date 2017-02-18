using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.Library.UserControl {
	/// <summary>
	/// Description résumée de Ligne3d.
	/// </summary>
	public class Ligne3d : System.Windows.Forms.UserControl {
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Couleur de la ligne 1
		/// </summary>
		private Color _ColorLigne1 = Color.Gray;
		/// <summary>
		/// Couleur de la ligne 2
		/// </summary>
		private Color _ColorLigne2 = Color.White;

		public Ligne3d() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			//Resize
			this.OnResize(EventArgs.Empty);
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

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			// 
			// Ligne3d
			// 
			this.Name = "Ligne3d";
			this.Size = new System.Drawing.Size(120, 16);
			this.Resize += new System.EventHandler(this.Ligne3d_Resize);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Ligne3d_Paint);

		}
		#endregion

		private void Ligne3d_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			//Crayon
			Pen ColorPen1 = new Pen(this._ColorLigne1, 1);
			Pen ColorPen2 = new Pen(this._ColorLigne2, 1);

			//Point (Ligne1)
			Point X1 = new Point(0 , 0);
			Point Y1 = new Point(Width, 0);
 
			//Point (Ligne1)
			Point X2 = new Point(0 , 1);
			Point Y2 = new Point(Width, 1);

			//Dessiner le graphique			
			e.Graphics.DrawLine(ColorPen1, X1, Y1);
			e.Graphics.DrawLine(ColorPen2, X2, Y2);
		}

		private void Ligne3d_Resize(object sender, System.EventArgs e) {
			this.Height = 3;
		}

		/// <summary>
		/// Couleur de la Ligne 1
		/// </summary>
		[
		Category("Couleur"),
		DefaultValue(0),
		Description("Couleur de la ligne 1")
		]	
		public Color CouleurLigne1 {
			get {
				return this._ColorLigne1;
			}
			set {
				this._ColorLigne1 = value;
			}
		}

		/// <summary>
		/// Couleur de la Ligne 2
		/// </summary>
		[
		Category("Couleur"),
		DefaultValue(0),
		Description("Couleur de la ligne 2")
		]	
		public Color CouleurLigne2 {
			get {
				return this._ColorLigne2;
			}
			set {
				this._ColorLigne2 = value;
			}
		}
	}
}