using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using VRS.Library.Table;

namespace VRS.Library.UserControl.TableEditor {
	/// <summary>
	/// Description résumée de TBLEntry.
	/// </summary>
	public class TBLEntry : System.Windows.Forms.UserControl {
		private DTEType _dteType = DTEType.Invalid;
		private System.Windows.Forms.ImageList ListImageTBL;
		private VRS.UI.Controls.WEditBox txtEntry;
		private VRS.UI.Controls.WEditBox txtValue;
		private System.Windows.Forms.PictureBox picType;
		private System.ComponentModel.IContainer components;

		public TBLEntry() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			this.txtValue.ViewStyle.BorderColor = System.Drawing.Color.White;
			this.txtEntry.ViewStyle.BorderColor = System.Drawing.Color.White;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TBLEntry));
			this.picType = new System.Windows.Forms.PictureBox();
			this.txtEntry = new VRS.UI.Controls.WEditBox();
			this.txtValue = new VRS.UI.Controls.WEditBox();
			this.ListImageTBL = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.txtEntry)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtValue)).BeginInit();
			this.SuspendLayout();
			// 
			// picType
			// 
			this.picType.BackColor = System.Drawing.Color.White;
			this.picType.Dock = System.Windows.Forms.DockStyle.Left;
			this.picType.Location = new System.Drawing.Point(0, 0);
			this.picType.Name = "picType";
			this.picType.Size = new System.Drawing.Size(20, 20);
			this.picType.TabIndex = 22;
			this.picType.TabStop = false;
			// 
			// txtEntry
			// 
			this.txtEntry.DecimalPlaces = 2;
			this.txtEntry.DecMaxValue = 999999999;
			this.txtEntry.DecMinValue = -999999999;
			this.txtEntry.Dock = System.Windows.Forms.DockStyle.Left;
			this.txtEntry.Location = new System.Drawing.Point(20, 0);
			this.txtEntry.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.txtEntry.MaxLength = 32767;
			this.txtEntry.Multiline = false;
			this.txtEntry.Name = "txtEntry";
			this.txtEntry.PasswordChar = '\0';
			this.txtEntry.ReadOnly = false;
			this.txtEntry.Size = new System.Drawing.Size(60, 20);
			this.txtEntry.TabIndex = 23;
			this.txtEntry.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtEntry.UseStaticViewStyle = false;
			this.txtEntry.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			this.txtEntry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTBLPath_KeyPress);
			this.txtEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEntry_KeyDown);
			this.txtEntry.Leave += new System.EventHandler(this.txtEntry_Leave);
			// 
			// txtValue
			// 
			this.txtValue.DecimalPlaces = 2;
			this.txtValue.DecMaxValue = 999999999;
			this.txtValue.DecMinValue = -999999999;
			this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtValue.Location = new System.Drawing.Point(80, 0);
			this.txtValue.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.txtValue.MaxLength = 32767;
			this.txtValue.Multiline = false;
			this.txtValue.Name = "txtValue";
			this.txtValue.PasswordChar = '\0';
			this.txtValue.ReadOnly = false;
			this.txtValue.Size = new System.Drawing.Size(192, 20);
			this.txtValue.TabIndex = 24;
			this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtValue.UseStaticViewStyle = false;
			this.txtValue.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			this.txtValue.Leave += new System.EventHandler(this.txtValue_Leave);
			// 
			// ListImageTBL
			// 
			this.ListImageTBL.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.ListImageTBL.ImageSize = new System.Drawing.Size(16, 16);
			this.ListImageTBL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListImageTBL.ImageStream")));
			this.ListImageTBL.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// TBLEntry
			// 
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.txtEntry);
			this.Controls.Add(this.picType);
			this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "TBLEntry";
			this.Size = new System.Drawing.Size(272, 20);
			((System.ComponentModel.ISupportInitialize)(this.txtEntry)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtValue)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void txtTBLPath_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
//			char key = e.KeyChar;
//			
//			if( key >= '0' && key <= '9')
//				e.Handled = false;				
//			else if (key >= 'a' && key <= 'f')
//				e.Handled = false;				
//			else if( key == '\b') //Backspace
//				e.Handled = false;
//			else
//				e.Handled = true;
		}

		private void txtEntry_Leave(object sender, System.EventArgs e) {
			checkType();
		}

		private void txtValue_Leave(object sender, System.EventArgs e) {
			checkType();
		}

		public string Value{
			get{
				return txtValue.Text;
			}
			set{
				txtValue.Text = value; 
			}
		}

		public string Entry{
			get{
				return txtEntry.Text;
			}
			set{
				txtEntry.Text = value; 
			}
		}

		public DTEType Type{			
			get{
				return this._dteType; 
			}
			set{
				switch(value){
					case DTEType.DualTitleEncoding:
						picType.Image = ListImageTBL.Images[0];
						this._dteType = DTEType.DualTitleEncoding;  
						break;
					case DTEType.ASCII:
						picType.Image = ListImageTBL.Images[2];
						this._dteType = DTEType.ASCII; 
						break;					
					case DTEType.MultipleTitleEncoding:
						picType.Image = ListImageTBL.Images[1];
						this._dteType = DTEType.MultipleTitleEncoding;
						break;
					case DTEType.EndBlock:
						picType.Image = ListImageTBL.Images[4];
						this._dteType = DTEType.EndBlock;
						break;
					case DTEType.EndLine:
						picType.Image = ListImageTBL.Images[4];
						this._dteType = DTEType.EndLine; 
						break;					
					case DTEType.Japonais:
						picType.Image = ListImageTBL.Images[3];
						this._dteType = DTEType.Japonais;
						break;
				}
			}
		}

		private void checkType(){
			if (txtEntry.Text.Length == 0 && txtValue.Text.Length == 0){
				picType.Image = ListImageTBL.Images[5];
				this._dteType = DTEType.Invalid;
				return;
			}

			try{
				switch (txtEntry.Text.Length){
					case 2:
						if (txtValue.Text.Length == 1){
							picType.Image = ListImageTBL.Images[2];
							this._dteType = DTEType.ASCII; 
						}
						else{
							picType.Image = ListImageTBL.Images[0];
							this._dteType = DTEType.DualTitleEncoding;
						}
						break;
					case 4: // >2								
						picType.Image = ListImageTBL.Images[1];
						this._dteType = DTEType.MultipleTitleEncoding;
						break;
				}
			}
			catch(IndexOutOfRangeException){
				switch (txtEntry.Text.Substring(0,1)){
					case @"/":
						picType.Image = ListImageTBL.Images[4];
						this._dteType = DTEType.EndBlock;
						break;							
					case @"*":
						picType.Image = ListImageTBL.Images[4];
						this._dteType = DTEType.EndLine;
						break;
						//case @"\":
				}
			}	
			catch(ArgumentOutOfRangeException) { //Du a une entre qui a 2 = de suite... EX:  XX==
				picType.Image = ListImageTBL.Images[0];
				this._dteType = DTEType.DualTitleEncoding;
			}
		}

		private void txtEntry_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			Keys key = e.KeyData;
			
			if( key >= Keys.D0 && key <= Keys.D9)
				e.Handled = false;				
			else if (key >= Keys.A && key <= Keys.F)
				e.Handled = false;				
			else if( key == Keys.Back) //Backspace
				e.Handled = false;
			else
				e.Handled = true;
		}
	}
}
