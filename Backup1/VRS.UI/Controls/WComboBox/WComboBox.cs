using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.UI.Controls
{
	/// <summary>
	/// ComboBox control.
	/// </summary>
	[DefaultEvent("SelectedIndexChanged"),]
	public class WComboBox : WButtonEdit
	{
		private System.ComponentModel.IContainer components = null;
		
		public event System.EventHandler SelectedIndexChanged = null;

		private WComboPopUp m_WComboPopUp   = null;
		private int         m_DropDownWidth = 100;
		private int         m_VisibleItems  = 10;
		private WComboItems m_WComboItems   = null;
		private WComboItem  m_SelectedItem  = null;
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public WComboBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call	

			m_pTextBox.LostFocus += new System.EventHandler(this.m_pTextBox_OnLostFocus);

			m_WComboItems = new WComboItems(this);
			m_DrawGrayImgForReadOnly = false;

			m_DropDownWidth = this.Width;
		
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
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// m_pTextBox
			// 
			this.m_pTextBox.ProccessMessage += new VRS.UI.Controls.WMessage_EventHandler(this.m_pTextBox_ProccessMessage);
			// 
			// WComboBox
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_pTextBox});
			this.Name = "WComboBox";
			this.ViewStyle.BorderColor = System.Drawing.Color.DarkGray;
			this.ViewStyle.BorderHotColor = System.Drawing.Color.Black;
			this.ViewStyle.ButtonColor = System.Drawing.SystemColors.Control;
			this.ViewStyle.ButtonHotColor = System.Drawing.Color.FromArgb(((System.Byte)(182)), ((System.Byte)(193)), ((System.Byte)(214)));
			this.ViewStyle.ButtonPressedColor = System.Drawing.Color.FromArgb(((System.Byte)(210)), ((System.Byte)(218)), ((System.Byte)(232)));
			this.ViewStyle.ControlBackColor = System.Drawing.SystemColors.Control;
			this.ViewStyle.EditColor = System.Drawing.Color.White;
			this.ViewStyle.EditDisabledColor = System.Drawing.Color.Gainsboro;
			this.ViewStyle.EditFocusedColor = System.Drawing.Color.Beige;
			this.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			this.ViewStyle.FlashColor = System.Drawing.Color.Pink;
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		#region Events handling

		#region function OnPopUp_SelectionChanged

		private void OnPopUp_SelectionChanged(object sender,WComboSelChanged_EventArgs e)
		{
			this.Text = e.Text;
			m_SelectedItem = e.Item;

			OnSelectedIndexChanged();
		}

		#endregion

		#region function OnPopUp_Closed

		private void OnPopUp_Closed(object sender,System.EventArgs e)
		{
			m_DroppedDown = false;
			m_WComboPopUp.Dispose();
			Invalidate(false);

			if(!this.ContainsFocus){
				this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);
			}
		}

		#endregion


		#region function m_pTextBox_ProccessMessage

		private bool m_pTextBox_ProccessMessage(object sender,ref System.Windows.Forms.Message m)
		{
			if(m_DroppedDown && m_WComboPopUp != null && IsNeeded(ref m)){
				
				// Forward message to PopUp Form
				m_WComboPopUp.PostMessage(ref m);
				return true;
			}

			return false;
		}

		#endregion

		#region function m_pTextBox_OnLostFocus

		private void m_pTextBox_OnLostFocus(object sender, System.EventArgs e)
		{
			if(m_DroppedDown && m_WComboPopUp != null&& !m_WComboPopUp.ClientRectangle.Contains(m_WComboPopUp.PointToClient(Control.MousePosition))){
				m_WComboPopUp.Close();
				m_DroppedDown = false;
			}
		}

		#endregion

		#endregion


		#region override OnButtonPressed
        
		protected override void OnButtonPressed()
		{
			if(m_DroppedDown){
				return;
			}	
		
			ShowPopUp();			
		}

		#endregion

		#region override OnPlusKeyPressed
        
		protected override void OnPlusKeyPressed()
		{
			if(m_DroppedDown){
				return;
			}	
		
			ShowPopUp();			
		}

		#endregion

		#region override OnSizeChanged

		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged(e);

			if(this.DesignMode){
				m_DropDownWidth = this.Width;
			}
		}

		#endregion

		
		#region function ShowPopUp

		private void ShowPopUp()
		{
			Point pt = new Point(this.Left,this.Bottom + 1);
			m_WComboPopUp = new WComboPopUp(this,m_ViewStyle,m_WComboItems.ToArray(),m_VisibleItems,this.Text,m_DropDownWidth);
			m_WComboPopUp.Location = this.Parent.PointToScreen(pt);
			m_WComboPopUp.SelectionChanged += new SelectionChangedHandler(this.OnPopUp_SelectionChanged);
			m_WComboPopUp.Closed += new System.EventHandler(this.OnPopUp_Closed);
	
			User32.ShowWindow(m_WComboPopUp.Handle,4);

			m_WComboPopUp.m_Start = true;
			m_DroppedDown = true;
		}

		#endregion


		#region function SelectItemByTag

		public void SelectItemByTag(object tag)
		{
			if(tag == null){
				return;
			}

			int index = 0;
			foreach(WComboItem it in this.Items){
				if(it.Tag.ToString() == tag.ToString()){
					this.SelectedIndex = index;
				}

				index++;
			}
		}

		#endregion

		
		#region function IsNeeded

		private bool IsNeeded(ref  System.Windows.Forms.Message m)
		{
			if(m.Msg == (int)Msgs.WM_MOUSEWHEEL){
				return true;
			}

			if(m.Msg == (int)Msgs.WM_KEYUP || m.Msg == (int)Msgs.WM_KEYDOWN){
				return true;
			}

			if(m.Msg == (int)Msgs.WM_CHAR){
				return true;
			}

			return false;
		}

		#endregion

						
		#region Properties Implementation

		public int DropDownWidth
		{
			get{ return m_DropDownWidth; }

			set{ m_DropDownWidth = value; }
		}

		public int VisibleItems
		{
			get{ return m_VisibleItems; }

			set{ m_VisibleItems = value; }
		}

		public WComboItems Items
		{
			get{ return m_WComboItems; }
		}

		public WComboItem SelectedItem
		{
			get{ return m_SelectedItem; }
		}

		public int SelectedIndex
		{
			get{
				if(this.SelectedItem != null){
					return Items.IndexOf(this.SelectedItem); 
				}
				else{
					return -1;
				}
			}

			set{
				if(value > -1 && value < this.Items.Count){
					m_SelectedItem = this.Items[value];
					this.Text = m_SelectedItem.Text;
				}
			}
		}

		#endregion

		#region Events Implementation

		private void OnSelectedIndexChanged()
		{
			if(SelectedIndexChanged != null){
				SelectedIndexChanged(this,new System.EventArgs());
			}
		}

		#endregion

	}
}
