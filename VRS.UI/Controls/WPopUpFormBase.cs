using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VRS.UI.Controls
{
	/// <summary>
	/// Summary description for Form6.
	/// </summary>
	public class WPopUpFormBase : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

		public bool     m_Start      = false;
		private Control m_Parent     = null;
		private Form    m_ParentForm = null;
		private Form    m_MdiParent  = null;
		
		/// <summary>
		/// Designer support.
		/// </summary>
		public WPopUpFormBase()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="parent"></param>
		public WPopUpFormBase(Control parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			m_Parent = parent;
			m_ParentForm = m_Parent.FindForm();
			
			if(Form.ActiveForm != null && Form.ActiveForm.IsMdiContainer){
				m_MdiParent = Form.ActiveForm;

				m_MdiParent.LostFocus += new System.EventHandler(this.ParentLostFocus);
				m_MdiParent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
				m_MdiParent.Move      += new System.EventHandler(this.ParentFormMoved);
			}
						
			m_ParentForm.LostFocus += new System.EventHandler(this.ParentLostFocus);
			m_ParentForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
			m_ParentForm.Move      += new System.EventHandler(this.ParentFormMoved);

			if(!object.ReferenceEquals(m_Parent.Parent,m_ParentForm)){
				m_Parent.Parent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
			}

        }

		#region function Dispose

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				m_Parent.LostFocus -= new System.EventHandler(this.ParentLostFocus);
				m_ParentForm.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
				m_ParentForm.Move -= new System.EventHandler(this.ParentFormMoved);

				if(!object.ReferenceEquals(m_Parent.Parent,m_ParentForm)){
					m_Parent.Parent.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
				}

				if(m_MdiParent != null){
					m_MdiParent.LostFocus -= new System.EventHandler(this.ParentLostFocus);
					m_MdiParent.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.ParentFormMouseClick);
					m_MdiParent.Move      -= new System.EventHandler(this.ParentFormMoved);
				}

				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// WPopUpFormBase
			// 
			this.AutoScale = false;
			this.ClientSize = new System.Drawing.Size(176, 96);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WPopUpFormBase";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Form6";

		}
		#endregion


		#region Events handling

		#region function ParentFormMouseClick

		private void ParentFormMouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.Close();
		}

		#endregion

		#region function ParentLostFocus

		private void ParentLostFocus(object sender, System.EventArgs e)
		{
			if(m_Start){
				this.Close();
			}
		}

		#endregion

		#region function ParentFormMoved

		private void ParentFormMoved(object sender, System.EventArgs e)
		{
			if(m_Start){
				this.Close();
			}
		}

		#endregion

		#endregion


		#region virtual PostMessage

		public virtual void PostMessage(ref Message m)
		{
			
		}

		#endregion

		
		#region override WndProc

		protected override void WndProc(ref Message m)
		{				
			if(m.Msg == (int)Msgs.WM_MOUSEACTIVATE){
				// Do not allow then mouse down to activate the window, but eat 
				// the message as we still want the mouse down for processing
				m.Result = (IntPtr)MouseActivateFlags.MA_NOACTIVATE;
				return;
			}

			base.WndProc(ref m);			
		}
		
		#endregion

		#region override Property CreateParams

		protected override CreateParams CreateParams 
		{
			get{
				// Extend the CreateParams property 
				CreateParams cp = base.CreateParams;

				// Update the window Style.
				cp.ExStyle |= (int)WindowExStyles.WS_EX_TOPMOST; 

				return cp;
			}
		}

		#endregion
		
	}
}
