using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using VRS.Library.Convertion;

namespace VRS.Library.UserControl {
	/// <summary>
	/// Description résumée de HexaTextBox.
	/// </summary>
	public class HexaTextBox : System.Windows.Forms.UserControl {
		public event KeyEventHandler TBKeyDown;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox HexaBox;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Valuer minimum
		/// </summary>
		private long _minValue = 0x0;
		/// <summary>
		/// Valeur maximum
		/// </summary>
		private long _maxValue = 0xFFFFFFFF;

		public HexaTextBox() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitForm

			this.Height = HexaBox.Height;
			HexaBox.Width = this.Width - HexaBox.Left;

			SetStyle(ControlStyles.FixedHeight,true);
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
			this.label1 = new System.Windows.Forms.Label();
			this.HexaBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "0x";
			// 
			// HexaBox
			// 
			this.HexaBox.BackColor = System.Drawing.SystemColors.Window;
			this.HexaBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.HexaBox.Location = new System.Drawing.Point(24, 0);
			this.HexaBox.Name = "HexaBox";
			this.HexaBox.Size = new System.Drawing.Size(80, 20);
			this.HexaBox.TabIndex = 1;
			this.HexaBox.Text = "0";
			this.HexaBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HexaBox_KeyDown);
			this.HexaBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexaBox_KeyPress);
			// 
			// HexaTextBox
			// 
			this.Controls.Add(this.HexaBox);
			this.Controls.Add(this.label1);
			this.Name = "HexaTextBox";
			this.Size = new System.Drawing.Size(144, 24);
			this.Resize += new System.EventHandler(this.HexaTextBox_Resize);
			this.Leave += new System.EventHandler(this.HexaTextBox_Leave);
			this.ResumeLayout(false);

		}
		#endregion

		private void HexaTextBox_Resize(object sender, System.EventArgs e) {
			this.Height = HexaBox.Height;
			HexaBox.Width = this.Width - HexaBox.Left;
		}

		private void HexaBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			
			char key = e.KeyChar;
			
			if( key >= '0' && key <= '9')
				e.Handled = false;				
			else if (key >= 'a' && key <= 'f')
				e.Handled = false;				
			else if( key == '\b') //Backspace
				e.Handled = false;
			else
				e.Handled = true;
		}

		private void HexaTextBox_Leave(object sender, System.EventArgs e) {
			//Verifie si le total est superieurs ou inferieux au limite du control
			long val = Convertir.HexaToDecimal(HexaBox.Text);
			
			if (val > this._maxValue)
				HexaBox.Text = Convertir.DecimalToHexa(this._maxValue);
				
			if (val < this._minValue)
				HexaBox.Text = Convertir.DecimalToHexa(this._minValue);
		}

		private void HexaBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			TBKeyDown(sender, e);
		}

		#region Propriétés
		/// <summary>
		/// Valeur Hexadécimal
		/// </summary>
		[
		 Category("Valeur"),
		 DefaultValue(0),
		 Description("Valeur hexadécimal du control") 
		]		
		public string HexaValue{
			get{
				return HexaBox.Text;
			}
			set{
				long val = Convertir.HexaToDecimal(value); 

				if ((val <= this._maxValue) && (val >= this._minValue))
					if (Convertir.HexaToDecimal(value) > -1)
						HexaBox.Text = value;
			}
		}

		/// <summary>
		/// Valeur décimal
		/// </summary>
		[
		 Category("Valeur"),
		 DefaultValue(0),
		 Description("Valeur décimal du control") 
		]
		public long DecimalValue{
			get{
				return Convertir.HexaToDecimal(HexaBox.Text);
			}
			set{
				if ((value <= this._maxValue) && (value >= this._minValue))
					HexaBox.Text = Convertir.DecimalToHexa(value);
			}
		}

		/// <summary>
		/// Valeur décimal
		/// </summary>
		[
		 Category("Valeur"),
		 DefaultValue(0xFFFFFFFF),
		 Description("Valeur maximum du control")
		]
		public long Maximum{
			get{
				return this._maxValue;
			}
			set{
				if (value <=  0xFFFFFFFF && value > this._minValue)
					this._maxValue = value;
			}
		}

		/// <summary>
		/// Valeur décimal
		/// </summary>
		[
		 Category("Valeur"),
		 DefaultValue(0x0),
		 Description("Valeur minimum du control")
		]
		public long Minimum{
			get{
				return this._minValue;
			}
			set{
				if(value >= 0x0 && value < this._maxValue)
					this._minValue = value;
			}
		}
		#endregion
	}
}
