using System;
using System.Windows.Forms;

namespace VRS.Library {
	/// <summary>
	/// Description résumée de InputBox.
	/// </summary>
	public class InputBox : System.Windows.Forms.Form {
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.Label lbl_prompt;
		private System.Windows.Forms.Button btn_ok;
		private VRS.UI.Controls.WEditBox txtB_response;
		private System.Windows.Forms.Button btn_cancel;

		public InputBox(string windowTitle, string question, string defaultText) {
			InitializeComponent();

			this.txtB_response.Text = defaultText;
			this.Text = windowTitle;
			this.lbl_prompt.Text = question;
		}

		public static string Show(string windowTitle, string question, string defaultText) {
			InputBox input = new InputBox(windowTitle, question, defaultText);

			try {
				if(input.ShowDialog() == DialogResult.OK) {
					return input.InputResponse;
				}
				else
					return null;
			}
			catch {
				return null;
			}
		}

		public string InputResponse {
			get{return this.txtB_response.Text;}
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
			this.lbl_prompt = new System.Windows.Forms.Label();
			this.btn_ok = new System.Windows.Forms.Button();
			this.btn_cancel = new System.Windows.Forms.Button();
			this.txtB_response = new VRS.UI.Controls.WEditBox();
			((System.ComponentModel.ISupportInitialize)(this.txtB_response)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl_prompt
			// 
			this.lbl_prompt.Location = new System.Drawing.Point(10, 13);
			this.lbl_prompt.Name = "lbl_prompt";
			this.lbl_prompt.Size = new System.Drawing.Size(270, 64);
			this.lbl_prompt.TabIndex = 1;
			// 
			// btn_ok
			// 
			this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btn_ok.Location = new System.Drawing.Point(284, 13);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new System.Drawing.Size(60, 22);
			this.btn_ok.TabIndex = 2;
			this.btn_ok.Text = "OK";
			// 
			// btn_cancel
			// 
			this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btn_cancel.Location = new System.Drawing.Point(284, 41);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new System.Drawing.Size(60, 22);
			this.btn_cancel.TabIndex = 3;
			this.btn_cancel.Text = "Annuler";
			this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
			// 
			// txtB_response
			// 
			this.txtB_response.DecimalPlaces = 2;
			this.txtB_response.DecMaxValue = 999999999;
			this.txtB_response.DecMinValue = -999999999;
			this.txtB_response.Location = new System.Drawing.Point(8, 88);
			this.txtB_response.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.txtB_response.MaxLength = 32767;
			this.txtB_response.Multiline = false;
			this.txtB_response.Name = "txtB_response";
			this.txtB_response.PasswordChar = '\0';
			this.txtB_response.ReadOnly = false;
			this.txtB_response.Size = new System.Drawing.Size(336, 20);
			this.txtB_response.TabIndex = 4;
			this.txtB_response.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtB_response.UseStaticViewStyle = false;
			this.txtB_response.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			// 
			// InputBox
			// 
			this.AcceptButton = this.btn_ok;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btn_cancel;
			this.ClientSize = new System.Drawing.Size(354, 120);
			this.Controls.Add(this.btn_cancel);
			this.Controls.Add(this.btn_ok);
			this.Controls.Add(this.lbl_prompt);
			this.Controls.Add(this.txtB_response);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputBox";
			this.ShowInTaskbar = false;
			((System.ComponentModel.ISupportInitialize)(this.txtB_response)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btn_cancel_Click(object sender, System.EventArgs e) {
		
		}

	}
}
