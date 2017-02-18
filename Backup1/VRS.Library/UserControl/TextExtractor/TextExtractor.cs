using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Crownwood.Magic.Controls;

namespace VRS.Library.UserControl.TextExtractor {
	/// <summary>
	/// Description résumée de TextExtractor.
	/// </summary>
	public class TextExtractor : System.Windows.Forms.UserControl {
		private Crownwood.Magic.Controls.TabControl tabOptions; 

		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TextExtractor() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			//Initialise les pages
			SetupPage();			
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
			this.tabOptions = new Crownwood.Magic.Controls.TabControl();
			this.SuspendLayout();
			// 
			// tabOptions
			// 
			this.tabOptions.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiBox;
			this.tabOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabOptions.IDEPixelArea = true;
			this.tabOptions.IDEPixelBorder = true;
			this.tabOptions.Name = "tabOptions";
			this.tabOptions.PositionTop = true;
			this.tabOptions.Size = new System.Drawing.Size(632, 240);
			this.tabOptions.TabIndex = 21;
			// 
			// TextExtractor
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabOptions});
			this.Name = "TextExtractor";
			this.Size = new System.Drawing.Size(632, 400);
			this.ResumeLayout(false);

		}
		#endregion

		private void SetupPage(){
			Crownwood.Magic.Controls.TabPage page = null;
			
			page = new Crownwood.Magic.Controls.TabPage("dddd", new TextExtractor_PageGeneral());
			
			page.Selected = true;

			tabOptions.TabPages.Add(page);

		}
	}
}
