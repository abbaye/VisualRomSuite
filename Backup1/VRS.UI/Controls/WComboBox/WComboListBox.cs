using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.UI.Controls
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class WComboListBox : System.Windows.Forms.ListBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WComboListBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			this.BorderStyle = BorderStyle.None;
		}

		#region function Dispose

		/// <summary> 
		/// Clean up any resources being used.
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

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion


		#region function PostMessage

		public void PostMessage(ref Message m)
		{
			base.WndProc(ref m);
		}

		#endregion


		#region override WndProc

		protected override void WndProc(ref Message m)
		{			
			if(m.Msg == (int)Msgs.WM_LBUTTONUP){
				WComboPopUp frm = (WComboPopUp)this.FindForm();
				frm.RaiseSelectionChanged();
				frm.Close();
				return;
			}

			if(m.Msg == (int)Msgs.WM_LBUTTONDOWN){
				return;
			}

			if(m.Msg == (int)Msgs.WM_MBUTTONDOWN){
				return;
			}


			base.WndProc(ref m);
		}

		#endregion

	}
}
