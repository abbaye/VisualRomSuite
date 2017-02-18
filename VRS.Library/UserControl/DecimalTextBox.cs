using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.Library.UserControl
{
	/// <summary>
	/// Description résumée de DecimalTextBox.
	/// </summary>
	public class DecimalTextBox : System.Windows.Forms.UserControl
	{
        public event KeyEventHandler TBKeyDown;

		private System.Windows.Forms.TextBox DecBox;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DecimalTextBox()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			//Resize
			this.OnResize(EventArgs.Empty);
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

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.DecBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DecBox
            // 
            this.DecBox.BackColor = System.Drawing.SystemColors.Window;
            this.DecBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DecBox.Location = new System.Drawing.Point(0, 0);
            this.DecBox.Name = "DecBox";
            this.DecBox.Size = new System.Drawing.Size(80, 20);
            this.DecBox.TabIndex = 2;
            this.DecBox.Text = "0";
            this.DecBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecBox_KeyPress);
            this.DecBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DecBox_KeyDown);
            // 
            // DecimalTextBox
            // 
            this.Controls.Add(this.DecBox);
            this.Name = "DecimalTextBox";
            this.Resize += new System.EventHandler(this.DecimalTextBox_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void DecimalTextBox_Resize(object sender, System.EventArgs e) {
			this.Height = DecBox.Height;
			DecBox.Width = this.Width - DecBox.Left;
		}

		private void DecBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			char key = e.KeyChar;
			
			if( key >= '0' && key <= '9')
				e.Handled = false;				
			else if( key == '\b') //Backspace
				e.Handled = false;
			else
				e.Handled = true;
		}

		[
		Category("Valeur"),
		DefaultValue(0)
		]	
		public long Valeur{
			get{
				return Convert.ToInt32(DecBox.Text);
			}
			set{
				DecBox.Text = Convert.ToString(value);
			}
		}

        private void DecBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
            TBKeyDown(sender, e);
        }
	}
}
