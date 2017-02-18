using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VRS.Library.UserControl {
	/// <summary>
	/// Direction que le texte sera ecrit
	/// </summary>
	public enum Direction{
		Horizontal,
		Vertical
	}

	/// <summary>
	/// Description résumée de FadeTexte.
	/// </summary>
	public class TextControl : System.Windows.Forms.UserControl {
		/// <summary>
		/// Direction que prendra l'affichage du texte
		/// </summary>
		private Direction _direction = Direction.Horizontal;

		/// <summary>
		/// Texte du control
		/// </summary>
		private string _Text = "TextControl"; 
		
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TextControl() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();
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
			// TextControl
			// 
			this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "TextControl";
			this.Size = new System.Drawing.Size(136, 24);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FadeTexte_Paint);

		}
		#endregion

		/// <summary>
		/// Evenement pour dessiner le texte sur le control
		/// </summary>
		private void FadeTexte_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			//Point d'origine
			PointF pt = new PointF(0,0);
			
			//Dessiner en fonction de la direction
			if(this._direction == Direction.Vertical){
				StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
				e.Graphics.DrawString(this._Text, this.Font, Brushes.Blue, pt, sf);
			}else
				e.Graphics.DrawString(this._Text, this.Font, Brushes.Blue, pt);
		
		}

		/// <summary>
		/// Propriétés : Direction du texte
		/// </summary>
		[
		Category("Control"),
		DefaultValue(Direction.Horizontal),
		Description("Direction du texte")
		]
		public Direction TextDirection{
			get{
				return this._direction;
			}
			set{
				this._direction = value;				
			}
		}

		/// <summary>
		/// Propriétés : Texte à afficher
		/// </summary>
		[
		Category("Control"),
		Description("Texte à afficher")
		]
		public string Texte{
			get{
				return this._Text;
			}
			set{
				this._Text = value;				
			}
		}
	}
}


