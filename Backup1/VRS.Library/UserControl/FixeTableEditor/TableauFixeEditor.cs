using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using VRS.Library.Table.TBL;

namespace VRS.Library.UserControl.FixeTableEditor {
	/// <summary>
	/// Description résumée de TableauFixe.
	/// </summary>
	public class TableauFixeEditor : System.Windows.Forms.UserControl {
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel ItemPanel;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TableauFixeEditor() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			//VRS.Library.Win32.StaticBorder.ThinBorder(PanelTableau.Handle.ToInt32(), true);

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
            this.button1 = new System.Windows.Forms.Button();
            this.ItemPanel = new System.Windows.Forms.Panel();
            this.ItemPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(252, 291);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 24);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ItemPanel
            // 
            this.ItemPanel.AutoScroll = true;
            this.ItemPanel.Controls.Add(this.button1);
            this.ItemPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemPanel.Location = new System.Drawing.Point(0, 0);
            this.ItemPanel.Name = "ItemPanel";
            this.ItemPanel.Size = new System.Drawing.Size(528, 344);
            this.ItemPanel.TabIndex = 7;
            // 
            // TableauFixeEditor
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.ItemPanel);
            this.Name = "TableauFixeEditor";
            this.Size = new System.Drawing.Size(528, 344);
            this.ItemPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e) {
			TBLStream tbl = new TBLStream();
			tbl.Load(@"C:\Programmation\FINAL FANTASY 2 - vrsprojet\Final Fantasy 2 us.tbl");

			ItemPanel.SuspendLayout();

			//Vide le tableau
			ItemPanel.Controls.Clear();

			TableFixeEntry[] entry = new TableFixeEntry[50]; 
			long HexaPos = 492041; //temp var
			for (int i=50 -1 ; i > -1  ; i--){
				entry[i] = new TableFixeEntry();

				entry[i].Length = 9;
				entry[i].Position = HexaPos; 
				entry[i].FileName = @"C:\Programmation\FINAL FANTASY 2 - vrsprojet\Final Fantasy 2 - Version 1.1 (US).smc";
				entry[i].TBL = tbl;

				HexaPos = entry[i].NextPosition;
				entry[i].Decode();
 
				entry[i].Dock = DockStyle.Top;
				
			}
			
			
			ItemPanel.Controls.AddRange(entry) ;

			ItemPanel.ResumeLayout();
			ItemPanel.AutoScrollMinSize = new Size(0, this.Height - 100);

			
		}
	}
}

