using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.UI.Controls
{
	/// <summary>
	/// 
	/// </summary>
	public delegate void ButtonPressedEventHandler(object sender,System.EventArgs e);
    
	/// <summary>
	/// Button edit control(TextBox + Button).
	/// </summary>
	[DefaultEvent("ButtonPressed"),]
	public class WButtonEdit : WControlBase
	{
		protected WTextBoxBase m_pTextBox;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

	    public event ButtonPressedEventHandler ButtonPressed   = null;
		public event ButtonPressedEventHandler EnterKeyPressed = null;
		public event ButtonPressedEventHandler PlusKeyPressed  = null;
		public new event WValidate_EventHandler    Validate        = null;

		private int    m_ButtonWidth        = 18;
		private Icon   m_ButtonIcon         = null;
		private bool   m_ReadOnly           = false;
		private bool   m_AcceptsPlussKey    = true;
		private bool   m_Modified           = false;
		private int    m_FlasCounter        = 0;

		protected bool m_DrawGrayImgForReadOnly = true;
		protected bool m_DroppedDown            = false;
				
		/// <summary>
		/// Default constructor.
		/// </summary>
		public WButtonEdit()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
					
			m_pTextBox.LostFocus += new System.EventHandler(this.m_pTextBox_OnLostFocus);
			m_pTextBox.GotFocus  += new System.EventHandler(this.m_pTextBox_OnGotFocus);

			m_ButtonIcon = Core.LoadIcon("down.ico");

			this.BackColor = Color.White;
		}

		#region function Dispose

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null){
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
			this.components = new System.ComponentModel.Container();
			this.m_pTextBox = new VRS.UI.Controls.WTextBoxBase();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.m_pTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// m_pTextBox
			// 
			this.m_pTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			this.m_pTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_pTextBox.DecimalPlaces = 2;
			this.m_pTextBox.DecMaxValue = 999999999;
			this.m_pTextBox.DecMinValue = -999999999;
			this.m_pTextBox.Location = new System.Drawing.Point(3, 2);
			this.m_pTextBox.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.m_pTextBox.Name = "m_pTextBox";
			this.m_pTextBox.Size = new System.Drawing.Size(86, 13);
			this.m_pTextBox.TabIndex = 0;
			this.m_pTextBox.Text = "";
			this.m_pTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_pTextBox_KeyUp);
			this.m_pTextBox.TextChanged += new System.EventHandler(this.m_pTextBox_TextChanged);
			// 
			// timer1
			// 
			this.timer1.Interval = 150;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// WButtonEdit
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_pTextBox});
			this.Name = "WButtonEdit";
			this.Size = new System.Drawing.Size(118, 20);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WButtonEdit_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WButtonEdit_MouseDown);
			((System.ComponentModel.ISupportInitialize)(this.m_pTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		#region Events handling
						
		#region function WButtonEdit_MouseMove

		private void WButtonEdit_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.Invalidate(false);
		}

		#endregion


		#region function OnMouseUp

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{	
			base.OnMouseUp(e);

			if(e.Button == MouseButtons.Left && IsMouseInButtonRect()){
				OnButtonPressed();
			}
		}

		#endregion

		#region function WButtonEdit_MouseDown

		private void WButtonEdit_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(IsMouseInButtonRect()){
				this.Invalidate(false);
			}
		}

		#endregion


		#region function m_pTextBox_OnGotFocus

		private void m_pTextBox_OnGotFocus(object sender, System.EventArgs e)
		{
			this.BackColor = m_ViewStyle.EditFocusedColor;		
		//	this.OnValidate();
		}

		#endregion

		#region function m_pTextBox_OnLostFocus

		private void m_pTextBox_OnLostFocus(object sender, System.EventArgs e)
		{
			if(!m_DroppedDown){
				this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);
			}			
		}

		#endregion

		#region function m_pTextBox_TextChanged

		private void m_pTextBox_TextChanged(object sender, System.EventArgs e)
		{
			base.OnTextChanged(new System.EventArgs());
		}

		#endregion

		#region function m_pTextBox_KeyUp

		private void m_pTextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.OnKeyUp(e);
		}

		#endregion

				
		#region function timer1_Tick

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			if(m_pTextBox.BackColor == this.BackColor){
				m_pTextBox.BackColor = m_ViewStyle.FlashColor;
			}
			else{
				m_pTextBox.BackColor = this.BackColor;
			}
			
			m_FlasCounter++;

			if(m_FlasCounter > 8){
				m_pTextBox.BackColor = this.BackColor;
				timer1.Enabled = false;
			}
		}

		#endregion


		#region function OnViewStyleChanged
		
		protected override void OnViewStyleChanged(ViewStyle_EventArgs e)
		{
			switch(e.PropertyName)
			{
				case "EditColor":
					this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);
					break;
				
				case "EditReadOnlyColor":
					this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);
					break;
			
				case "EditDisabledColor":
					this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);
					break;
			}

			this.Invalidate(false);
		}
	
		#endregion

		#endregion

				
		#region function DrawControl
		
		protected override void DrawControl(Graphics g,bool hot)
		{
			Rectangle rectButton = GetButtonRect();
			
			//----- Draw border around control -------------------------//
			bool border_hot = hot;
			Painter.DrawBorder(g,m_ViewStyle,this.ClientRectangle,border_hot);
			//-----------------------------------------------------------//

			//----- Draw button ----------------------------------------------------//
			bool btn_hot     = (IsMouseInButtonRect() && hot) || m_DroppedDown;
			bool btn_pressed = IsMouseInButtonRect() && Control.MouseButtons == MouseButtons.Left && hot;

			Painter.DrawButton(g,m_ViewStyle,rectButton,border_hot,btn_hot,btn_pressed);
			//----- End of button drawing ------------------------------------------//
			
			//---- Draw icon --------------------------------------------//
			if(m_ButtonIcon != null){				
				Rectangle rectI  = new Rectangle(rectButton.Left+1,rectButton.Top,rectButton.Width-2,rectButton.Height-2);
				bool      grayed = !this.Enabled || (this.ReadOnly && m_DrawGrayImgForReadOnly);
				Painter.DrawIcon(g,m_ButtonIcon,rectI,grayed,btn_pressed);
			}
			//-------------------------------------------------------------//			
		}

		#endregion

	
		#region function ProcessDialogKey

		protected override bool ProcessDialogKey(System.Windows.Forms.Keys keyData)
		{
			System.Windows.Forms.Keys key = keyData;
			if(key == System.Windows.Forms.Keys.Enter){
				this.OnEnterKeyPressed();
				return true;
			}
			if(key == System.Windows.Forms.Keys.Add){
				OnPlusKeyPressed();
				return true;
			}

			return base.ProcessDialogKey(keyData);
		}

		#endregion

		#region override OnKeyUp
/*
		protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if(e.KeyData == System.Windows.Forms.Keys.Enter){
				this.OnEnterKeyPressed();
				e.Handled = true;
			}
		}
*/
		#endregion

	
		#region function IsMouseInButtonRect

		protected bool IsMouseInButtonRect()
		{
			Rectangle rectButton = GetButtonRect();
			Point mPos = Control.MousePosition;
			if(rectButton.Contains(this.PointToClient(mPos))){
				return true;
			}
			else{
				return false;
			}
		}

		#endregion

		#region fucntion GetButtonRect
	
		public Rectangle GetButtonRect()
		{
			Rectangle rectButton = new Rectangle(this.Width - m_ButtonWidth,1,m_ButtonWidth - 1,this.Height - 2);
			return rectButton;
		}

		#endregion


		#region override OnEndedInitialize

		public override void OnEndedInitialize()
		{
			this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);
			m_pTextBox.Width = this.Width - m_ButtonWidth - 7;
		}

		#endregion

		#region override OnEnabledChanged

		protected override void OnEnabledChanged(System.EventArgs e)
		{
			base.OnEnabledChanged(e);

		//	m_pTextBox.Enabled = this.Enabled;

			this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);
		}

		#endregion
		
		
		#region Public Functions

		public void FlashControl()
		{
			if(!timer1.Enabled){
				m_FlasCounter  = 0;
				timer1.Enabled = true;
			}
		}

		#endregion

		#region Properties Implementation

		#region Color stuff

		/// <summary>
		/// 
		/// </summary>
		public override Color ForeColor
		{
			get{ return base.ForeColor; }

			set{ 
				base.ForeColor       = value;
				m_pTextBox.ForeColor = value;
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public override Color BackColor
		{
			get{ return base.BackColor; }

			set{
				base.BackColor       = value;
				m_pTextBox.BackColor = value;
				Invalidate(false);
			}
		}

		
		#endregion

				
		/// <summary>
		/// 
		/// </summary>
		public new Size Size
		{
			get{ return base.Size; }

			set{
				if(value.Height > m_pTextBox.Height + 1){					
					base.Size = value;

					int yPos = (value.Height - m_pTextBox.Height) / 2;
					m_pTextBox.Top = yPos;					
				}				
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int ButtonWidth
		{
			get{ return m_ButtonWidth; }

			set{				
				m_ButtonWidth    = value;
				m_pTextBox.Width = this.Width - m_ButtonWidth - m_pTextBox.Left - 3;
				this.Invalidate();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public Icon ButtonIcon
		{
			get{ return m_ButtonIcon; }

			set{ m_ButtonIcon = value; }
		}

		/// <summary>
		/// True, if value is modified.
		/// </summary>
		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public bool IsModified
		{
			get{ return m_pTextBox.Modified; }

			set{ m_pTextBox.Modified = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool AcceptsPlussKey
		{
			get{ return m_AcceptsPlussKey; }

			set{ m_AcceptsPlussKey = value; }
		}
        		
		/// <summary>
		/// 
		/// </summary>
		public bool ReadOnly
		{
			get{ return m_ReadOnly; }

			set{ 
				m_ReadOnly = value;
				m_pTextBox.ReadOnly = value;

				if(value){
					this.BackColor = m_ViewStyle.EditReadOnlyColor;
				}
				else{
					if(this.ContainsFocus){
						this.BackColor = m_ViewStyle.EditFocusedColor;
					}
					else{
						this.BackColor = m_ViewStyle.EditColor;
					}
				}
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public int MaxLength
		{
			get{ return m_pTextBox.MaxLength; }

			set{ m_pTextBox.MaxLength = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		[
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public override string Text
		{
			get{ return m_pTextBox.Text; }

			set{
				m_pTextBox.Text = value;
			}
		}

		[
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		]
		public DateTime DateValue
		{
			get{ return m_pTextBox.DateValue; }

			set{ m_pTextBox.DateValue = value; }
		}
		
		public WEditBox_Mask Mask
		{
			get{ return m_pTextBox.Mask; }

			set{
				m_pTextBox.Mask = value;
			}
		}

		#endregion

		#region Events Implementation

		#region function OnButtonPressed

		protected virtual void OnButtonPressed() 
		{
			// Raises the ladu change event; 	
			System.EventArgs oArg = new System.EventArgs();

			if(this.ButtonPressed != null && !this.ReadOnly && this.Enabled){
				this.ButtonPressed(this, oArg);
			}
		}

		#endregion

		#region function OnEnterKeyPressed

		protected virtual void OnEnterKeyPressed() 
		{
			// Raises the ladu change event; 	
			System.EventArgs oArg = new System.EventArgs();

			if(this.EnterKeyPressed != null){
				this.EnterKeyPressed(this, oArg);
			}
		}

		#endregion

		#region function OnPlusKeyPressed

		protected virtual void OnPlusKeyPressed() 
		{				
			System.EventArgs oArg = new System.EventArgs();

			// Raise event
			if(this.PlusKeyPressed != null && !this.ReadOnly && this.Enabled){
				this.PlusKeyPressed(this, oArg);
			}			
		}

		#endregion


		#region function OnValidate

		protected virtual void OnValidate() 
		{	
			// Raises the Validate change event; 	
			WValidate_EventArgs oArg = new WValidate_EventArgs(this.Name,this.Text);

			if(this.Validate != null){
				this.Validate(this, oArg);
			}
			
			//---- If validation failed ----//
			if(!oArg.IsValid){
				if(oArg.FlashControl){
					this.FlashControl();
				}

				if(!oArg.AllowMoveFocus){
					this.Focus();
				}
			}
			//------------------------------//						
		}

		#endregion
															
		#endregion
				
	}
}
