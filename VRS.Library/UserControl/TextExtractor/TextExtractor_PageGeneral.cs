using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VRS.Library.UserControl.TextExtractor
{
	/// <summary>
	/// Description résumée de TextExtractor_PageGeneral.
	/// </summary>
	public class TextExtractor_PageGeneral : System.Windows.Forms.Form
	{
		private VRS.UI.Controls.WEditBox wEditBox1;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TextExtractor_PageGeneral()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.wEditBox1 = new VRS.UI.Controls.WEditBox();
			((System.ComponentModel.ISupportInitialize)(this.wEditBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// wEditBox1
			// 
			this.wEditBox1.DecimalPlaces = 2;
			this.wEditBox1.DecMaxValue = 999999999;
			this.wEditBox1.DecMinValue = -999999999;
			this.wEditBox1.Location = new System.Drawing.Point(80, 80);
			this.wEditBox1.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.wEditBox1.MaxLength = 32767;
			this.wEditBox1.Multiline = false;
			this.wEditBox1.Name = "wEditBox1";
			this.wEditBox1.PasswordChar = '\0';
			this.wEditBox1.ReadOnly = false;
			this.wEditBox1.Size = new System.Drawing.Size(100, 20);
			this.wEditBox1.TabIndex = 0;
			this.wEditBox1.Text = "wEditBox1";
			this.wEditBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.wEditBox1.UseStaticViewStyle = false;
			this.wEditBox1.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			// 
			// TextExtractor_PageGeneral
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 294);
			this.Controls.Add(this.wEditBox1);
			this.Name = "TextExtractor_PageGeneral";
			this.Text = "TextExtractor_PageGeneral";
			((System.ComponentModel.ISupportInitialize)(this.wEditBox1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
