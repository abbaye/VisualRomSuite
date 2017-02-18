using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VRS.UI.Controls
{
	/// <summary>
	/// 
	/// </summary>
	public delegate void SelectionChangedHandler(object sender,WComboSelChanged_EventArgs e);

	#region public class WComboSelChanged_EventArgs

	public class WComboSelChanged_EventArgs
	{
		private object m_SelectedValue;

		public WComboSelChanged_EventArgs(object selectedValue)
		{
			m_SelectedValue = selectedValue;
		}

		#region Properties Implementation

		public string Text
		{
			get{ return m_SelectedValue.ToString(); }
		}

		public WComboItem Item
		{
			get{
				if(m_SelectedValue != null){
					return (WComboItem)m_SelectedValue;
				}
				else{
					return null;
				}
			}
		}

		#endregion

	}

	#endregion


	public class WComboPopUp : VRS.UI.Controls.WPopUpFormBase
	{
		private WComboListBox m_pListBox;
		private System.ComponentModel.IContainer components = null;

		public event SelectionChangedHandler SelectionChanged = null;

		private ViewStyle m_pViewStyle = null;
        		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="viewStyle"></param>
		/// <param name="listBoxItems"></param>
		/// <param name="visibleItems"></param>
		public WComboPopUp(Control parent,ViewStyle viewStyle,object[] listBoxItems,int visibleItems,string selectedText,int dropDownWidth) : base(parent)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

			// Store view style
			m_pViewStyle = viewStyle;

			if(visibleItems > listBoxItems.Length){
				visibleItems = listBoxItems.Length;
			}

			this.Width  = dropDownWidth;
			this.Height = (m_pListBox.ItemHeight * visibleItems)+2;

			m_pListBox.BackColor = viewStyle.EditColor;
					       
			// Add items to listbox
			m_pListBox.Items.AddRange(listBoxItems);
			
			int index = m_pListBox.FindStringExact(selectedText);
			if(index > -1){
				m_pListBox.SelectedIndex = index;
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
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_pListBox = new VRS.UI.Controls.WComboListBox();
			this.SuspendLayout();
			// 
			// m_pListBox
			// 
			this.m_pListBox.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.m_pListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_pListBox.IntegralHeight = false;
			this.m_pListBox.Location = new System.Drawing.Point(1, 1);
			this.m_pListBox.Name = "m_pListBox";
			this.m_pListBox.Size = new System.Drawing.Size(118, 70);
			this.m_pListBox.TabIndex = 0;
			this.m_pListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_pListBox_KeyUp);
			this.m_pListBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseMove);
			// 
			// WComboPopUp
			// 
			this.ClientSize = new System.Drawing.Size(120, 72);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_pListBox});
			this.Name = "WComboPopUp";
			this.ResumeLayout(false);

		}
		#endregion


		#region Events handling

		#region function m_pListBox_MouseMove
		
		private void listBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int index = m_pListBox.IndexFromPoint(e.X,e.Y);

			if(m_pListBox.SelectedIndex != index){
				m_pListBox.SelectedIndex = index;
			}
		}

		#endregion

		#region function m_pListBox_KeyUp

		private void m_pListBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Escape){
				this.Close();
			}

			if(e.KeyData == Keys.Enter){
				OnSelectionChanged();
				this.Close();
			}
		}

		#endregion

		#endregion


		#region function OnPaint

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle rect = new Rectangle(this.ClientRectangle.Location,new Size(this.ClientRectangle.Width - 1,this.ClientRectangle.Height - 1));
			Pen pen = new Pen(m_pViewStyle.BorderHotColor);

			e.Graphics.DrawRectangle(pen,rect);
		}

		#endregion

		#region function PostMessage

		public override void PostMessage(ref Message m)
		{
			Message msg = new Message();
			msg.HWnd    = m_pListBox.Handle;
			msg.LParam  = m.LParam;
			msg.Msg     = m.Msg;
			msg.Result  = m.Result;
			msg.WParam  = m.WParam;

			// Forward message to ListBox
			m_pListBox.PostMessage(ref msg);
		}

		#endregion

        
		#region Public Functions

		public void RaiseSelectionChanged()
		{
			OnSelectionChanged();
		}
		
		#endregion

		#region Events Implementation

		private void OnSelectionChanged()
		{
			if(m_pListBox.SelectedItem != null){
				WComboSelChanged_EventArgs oArgs = new WComboSelChanged_EventArgs(m_pListBox.SelectedItem);

				if(this.SelectionChanged != null){
					this.SelectionChanged(this,oArgs);
				}
			}
		}

		#endregion

	}
}

