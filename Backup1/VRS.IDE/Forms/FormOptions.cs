using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using VRS.IDE.Resource;

namespace VRS.IDE.Forms {
	/// <summary>
	/// Description résumée de FromOptions.
	/// </summary>
	public class FormOptions : System.Windows.Forms.Form {
		/// <summary>
		/// Gestion des Resources
		/// </summary>
		Resources _res = null; 

		/// <summary>
		/// Access a la boite de dialogue principal
		/// </summary>
		FormMain _mainDialog = null;
		private VRS.Library.UserControl.GradientLabel GradLabelProjectType;
		private VRS.Library.UserControl.Ligne3d ligne3d1;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Button cmdOK;
		private VRS.UI.Controls.WOutlookBar.WOutlookBar OptionsBars;
		private System.Windows.Forms.ImageList ListImageBars;
		private System.ComponentModel.IContainer components; 

		public FormOptions(Resources res, FormMain main) {
			this._res = res;
			this._mainDialog = main;

			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//Creation de la  bar d'options
			CreateOptionsBars();

			//Chargement des langues
			LoadLanguage();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.GradLabelProjectType = new VRS.Library.UserControl.GradientLabel();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdClose = new System.Windows.Forms.Button();
			this.ligne3d1 = new VRS.Library.UserControl.Ligne3d();
			this.OptionsBars = new VRS.UI.Controls.WOutlookBar.WOutlookBar();
			this.ListImageBars = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// GradLabelProjectType
			// 
			this.GradLabelProjectType.CouleurDroite = System.Drawing.SystemColors.Control;
			this.GradLabelProjectType.CouleurGauche = System.Drawing.SystemColors.InactiveCaptionText;
			this.GradLabelProjectType.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GradLabelProjectType.Location = new System.Drawing.Point(176, 16);
			this.GradLabelProjectType.Name = "GradLabelProjectType";
			this.GradLabelProjectType.Size = new System.Drawing.Size(520, 24);
			this.GradLabelProjectType.TabIndex = 15;
			this.GradLabelProjectType.Text = "OPTIONS";
			this.GradLabelProjectType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmdOK
			// 
			this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdOK.Location = new System.Drawing.Point(544, 437);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(72, 24);
			this.cmdOK.TabIndex = 16;
			this.cmdOK.Text = "DialogueDefault_OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdClose
			// 
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(624, 437);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.TabIndex = 17;
			this.cmdClose.Text = "DialogueDefault_Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// ligne3d1
			// 
			this.ligne3d1.CouleurLigne1 = System.Drawing.Color.Gray;
			this.ligne3d1.CouleurLigne2 = System.Drawing.Color.White;
			this.ligne3d1.Location = new System.Drawing.Point(16, 424);
			this.ligne3d1.Name = "ligne3d1";
			this.ligne3d1.Size = new System.Drawing.Size(680, 3);
			this.ligne3d1.TabIndex = 18;
			// 
			// OptionsBars
			// 
			this.OptionsBars.AllowItemsStuck = true;
			this.OptionsBars.ImageList = this.ListImageBars;
			this.OptionsBars.Location = new System.Drawing.Point(16, 16);
			this.OptionsBars.Name = "OptionsBars";
			this.OptionsBars.Size = new System.Drawing.Size(152, 400);
			this.OptionsBars.TabIndex = 19;
			this.OptionsBars.UseStaticViewStyle = true;
			// 
			// ListImageBars
			// 
			this.ListImageBars.ImageSize = new System.Drawing.Size(16, 16);
			this.ListImageBars.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FormOptions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(714, 472);
			this.ControlBox = false;
			this.Controls.Add(this.ligne3d1);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.GradLabelProjectType);
			this.Controls.Add(this.OptionsBars);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.Load += new System.EventHandler(this.FormOptions_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void LoadLanguage() {
			const string prefix = "FormOptions_";

			this.Text = _res.GetString(prefix + this.Text);
			cmdClose.Text = _res.GetString(cmdClose.Text);
			cmdOK.Text = _res.GetString(cmdOK.Text);
		}

		private void cmdClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void FormOptions_Load(object sender, System.EventArgs e) {
			
		}

		private void CreateOptionsBars(){
			//Creation de la bars
			OptionsBars.Bars.Add(this._res.GetString("FormOptionsBars")); 
		}
	}
}
