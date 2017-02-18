using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using VRS.Library.Table.TBL;
using VRS.Library.IO;
using VRS.Library.Convertion;

namespace VRS.Library.UserControl.FixeTableEditor {
	/// <summary>
	/// Description résumée de TableFixeEntry.
	/// </summary>
	public class TableFixeEntry : System.Windows.Forms.UserControl {
		private System.Windows.Forms.Label label1;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private TBLStream _tbl = null; //Table de jeu
		private string _file = ""; //Chemin du fichier a ouvrir
		private int _length;
		private VRS.UI.Controls.WEditBox txtValue; //Taille de l'entrer		
		private long _position; //Position dans le fichier

		public TableFixeEntry() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			this.txtValue.ViewStyle.BorderColor = Color.White;
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtValue = new VRS.UI.Controls.WEditBox();
			((System.ComponentModel.ISupportInitialize)(this.txtValue)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Silver;
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "0x00000000";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtValue.DecimalPlaces = 2;
			this.txtValue.DecMaxValue = 999999999;
			this.txtValue.DecMinValue = -999999999;
			this.txtValue.Location = new System.Drawing.Point(88, 0);
			this.txtValue.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.txtValue.MaxLength = 32767;
			this.txtValue.Multiline = false;
			this.txtValue.Name = "txtValue";
			this.txtValue.PasswordChar = '\0';
			this.txtValue.ReadOnly = false;
			this.txtValue.Size = new System.Drawing.Size(176, 20);
			this.txtValue.TabIndex = 0;
			this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtValue.UseStaticViewStyle = false;
			// 
			// TableFixeEntry
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "TableFixeEntry";
			this.Size = new System.Drawing.Size(264, 20);
			this.Resize += new System.EventHandler(this.TableFixeEntry_Resize);
			((System.ComponentModel.ISupportInitialize)(this.txtValue)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void TableFixeEntry_Resize(object sender, System.EventArgs e) {
			//this.Height = 20; 
		}

		/// <summary>
		/// Decode l'entrée suivant les propriétés de l'objet
		/// </summary>
		public void Decode(){
			string DataBrut = ExtractData.AspireString(PositionHexa, 
				Convertir.DecimalToHexa(this._position + this._length -1), this._file, true) ;
						
			string[] Data = Convertir.StringHexa_LittleEndian(DataBrut, true).Trim().Split(new char[]{' '});
			string DecodedDTE = "";
			string Find = "";

			for(int i=0; i<Data.Length; i++){
				Find = this._tbl.FindTBLMatch(Data[i], true);
				if (Find  == "#")
					DecodedDTE += "<" + Data[i] + ">";
				else
					DecodedDTE += Find;
			}

			txtValue.Text = DecodedDTE.TrimEnd(' ');
		}

		/// <summary>
		/// Verifie si les données du controls sont valide avant la reinsertion
		/// </summary>
		/// <returns>Return True si les donnée sont valide </returns>
		public bool ValidateData(){
			return true;
		}

		/// <summary>
		/// Longueur
		/// </summary>
		public int Length{
			get{
				return this._length;
			}
			set{
				if(value > 0) //Temporaire
					this._length = value;
			}
		}

		/// <summary>
		/// Fichier sur lequel l'objet va travailler
		/// </summary>
		public string FileName{
			get{
				return this._file;
			}
			set{
				if(System.IO.File.Exists(value))
					this._file = value;
			}
		}

		/// <summary>
		/// Table de jeu
		/// </summary>
		public TBLStream TBL{
			get{
				return this._tbl;
			}
			set{
				this._tbl = value;
			}
		}

		#region Propriétes Position
		/// <summary>
		/// Donne la position Suivante
		/// </summary>
		/// <remarks>Length doit être >0</remarks> 
		public long NextPosition{
			get{
				if (this._length > 0)
					return this._position + this._length;
				else
					return 0;
			}
		}

		/// <summary>
		/// Donne la position précédante
		/// </summary>
		/// <remarks>Length doit être >0</remarks> 
		public long PreviousPosition{
			get{
				if (this._length > 0)
					return this._position + this._length;
				else
					return 0;
			}
		}

		/// <summary>
		/// Position dans le fichier
		/// </summary>
		public long Position{
			get{
				return this._position;
			}
			set{
				if(value > 0){ //Temporaire
					this._position = value;
					label1.Text = "0x" + String.Format("{0, 8:0}", Convertir.DecimalToHexa(value)).Trim();
				}
			}
		}

		/// <summary>
		/// Position dans le fichier
		/// </summary>
		public string PositionHexa{
			get{
				return Convertir.DecimalToHexa(this._position);
			}
			set{
				if(Convertir.HexaToDecimal(value) > 0){ //Temporaire
					this._position = Convertir.HexaToDecimal(value);
					label1.Text = "0x" + String.Format("{0, 8:0}", value).Trim();
				}
			}
		}
		#endregion
	}
}
