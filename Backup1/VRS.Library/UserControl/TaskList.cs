using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.Library.UserControl
{
	/// <summary>
	/// Description résumée de TaskList.
	/// </summary>
	public class TaskList : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TreeView treeView1;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TaskList()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitForm

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
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(568, 184);
			this.treeView1.TabIndex = 0;
			// 
			// TaskList
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.treeView1});
			this.Name = "TaskList";
			this.Size = new System.Drawing.Size(568, 184);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
