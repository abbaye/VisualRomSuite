using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.UI.Controls
{
	/// <summary>
	/// SpinEdit control.
	/// </summary>
	public class WSpinEdit : WControlBase
	{		
		private WTextBoxBase m_pTextBox;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		
		private LeftRight m_ButtonsAlign   = LeftRight.Right;
		private int       m_ButtonWidth    = 14;
		private Icon      m_UpButtonIcon   = null;
		private Icon      m_DownButtonIcon = null;
		private bool      m_ReadOnly       = false;
		private int       m_FlasCounter    = 0;
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public WSpinEdit()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
		
			m_pTextBox.LostFocus += new System.EventHandler(this.m_pTextBox_OnLostFocus);
			m_pTextBox.GotFocus  += new System.EventHandler(this.m_pTextBox_OnGotFocus);
			
			m_UpButtonIcon   = Core.LoadIcon("up.ico");
			m_DownButtonIcon = Core.LoadIcon("down.ico");

			this.BackColor     = Color.White;					
			this.Mask          = WEditBox_Mask.Numeric; 
			this.DecimalPlaces = 0;
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
			this.m_pTextBox.Location = new System.Drawing.Point(3, 3);
			this.m_pTextBox.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.m_pTextBox.Name = "m_pTextBox";
			this.m_pTextBox.Size = new System.Drawing.Size(95, 13);
			this.m_pTextBox.TabIndex = 0;
			this.m_pTextBox.Text = "textBox1";
			this.m_pTextBox.TextChanged += new System.EventHandler(this.m_pTextBox_TextChanged);
			// 
			// timer1
			// 
			this.timer1.Interval = 150;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// WSpinEdit
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_pTextBox});
			this.Name = "WSpinEdit";
			this.Size = new System.Drawing.Size(118, 20);
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
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WSpinEdit_MouseUp);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WSpinEdit_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WSpinEdit_MouseDown);
			((System.ComponentModel.ISupportInitialize)(this.m_pTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		#region Events handling
		
		#region function WSpinEdit_MouseUp

		private void WSpinEdit_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(this.Enabled && !this.ReadOnly && e.Button == MouseButtons.Left && IsMouseInButtonRect()){
	//			OnButtonPressed();
				
				Rectangle upButtonRect   = GetUpButtonRect();
				Rectangle downButtonRect = GetUpButtonRect();

				if(upButtonRect.Contains(this.PointToClient(Control.MousePosition))){
					decimal val = Core.ConvertToDeciaml(this.Text);
							val++;
					if(val <= this.DecMaxValue){
						this.Text = val.ToString();
					}
				}
				else{
					decimal val = Core.ConvertToDeciaml(this.Text);
					val--;
					if(val >= this.DecMinValue){
						this.Text = val.ToString();
					}
				}
			}
		}

		#endregion

		#region function WSpinEdit_MouseDown

		private void WSpinEdit_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(IsMouseInButtonRect()){
				this.Invalidate(false);				
			}
		}

		#endregion

		#region function WSpinEdit_MouseMove

		private void WSpinEdit_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(IsMouseInButtonRect()){
				this.Invalidate(false);
			}
		}

		#endregion


		#region function m_pTextBox_OnLostFocus

		private void m_pTextBox_OnLostFocus(object sender, System.EventArgs e)
		{
			this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);
		}

		#endregion

		#region function m_pTextBox_OnGotFocus

		private void m_pTextBox_OnGotFocus(object sender, System.EventArgs e)
		{
			this.BackColor   = m_ViewStyle.EditFocusedColor;
		}

		#endregion

		#region function m_pTextBox_TextChanged

		private void m_pTextBox_TextChanged(object sender, System.EventArgs e)
		{
			base.OnTextChanged(new System.EventArgs());
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
			Point mPoint = Control.MousePosition;
					
			//----- Draw border around control -------------------------//
			bool border_hot = hot;
			Painter.DrawBorder(g,m_ViewStyle,this.ClientRectangle,border_hot);
			//-----------------------------------------------------------//

			//----- Draw buttons -----------------------------------------------------------//
			Rectangle upButtonRect   = GetUpButtonRect();
			Rectangle downButtonRect = GetDownButtonRect();

			bool btn_hot         = (IsMouseInButtonRect() && this.Enabled);
			bool btnUp_pressed   = IsMouseInButtonRect() && Control.MouseButtons == MouseButtons.Left && upButtonRect.Contains(this.PointToClient(mPoint));
			bool btnDown_pressed = IsMouseInButtonRect() && Control.MouseButtons == MouseButtons.Left && downButtonRect.Contains(this.PointToClient(mPoint));;

			Painter.DrawButton(g,m_ViewStyle,upButtonRect  ,border_hot,btn_hot,btnUp_pressed);
			Painter.DrawButton(g,m_ViewStyle,downButtonRect,border_hot,btn_hot,btnDown_pressed);
			//----- End of buttons drawing ----------------------------------------------------//

			//---- Draw icons --------------------------------------------//
			if(m_UpButtonIcon != null && m_DownButtonIcon != null){
			
				if(m_ButtonWidth < this.Height + 2){					
					upButtonRect   = new Rectangle(upButtonRect.X,upButtonRect.Y + (upButtonRect.Height - upButtonRect.Width)/2 + 1,upButtonRect.Width,upButtonRect.Height);
					downButtonRect = new Rectangle(downButtonRect.X,downButtonRect.Y + (downButtonRect.Height - downButtonRect.Width)/2 + 1,downButtonRect.Width,downButtonRect.Height);
				}				
				
				//------ Adjust Icon sizes and location ----------------------------------//	
				downButtonRect.Location = new Point(downButtonRect.X,downButtonRect.Y - 1);
				downButtonRect.Height   = downButtonRect.Width;

				upButtonRect.Location = new Point(upButtonRect.X,upButtonRect.Y - 2);
				upButtonRect.Height   = upButtonRect.Width;

				bool grayed = !this.Enabled || this.ReadOnly;
				
				Painter.DrawIcon(g,m_UpButtonIcon  ,upButtonRect  ,grayed,btnUp_pressed);
				Painter.DrawIcon(g,m_DownButtonIcon,downButtonRect,grayed,btnDown_pressed);
			}
			//-------------------------------------------------------------//		
		}

		#endregion


		#region function IsMouseInButtonRect

		private bool IsMouseInButtonRect()
		{
			Rectangle rectButton = GetButtonsRect();
			Point mPos = Control.MousePosition;
			if(rectButton.Contains(this.PointToClient(mPos))){
				return true;
			}
			else{
				return false;
			}
		}

		#endregion

		#region function GetButtonsRect
	
		public Rectangle GetButtonsRect()
		{
			if(m_ButtonsAlign == LeftRight.Right){
				Rectangle rectButton = new Rectangle(this.Width - m_ButtonWidth,1,m_ButtonWidth - 1,this.Height - 2);
				return rectButton;
			}
			else{
				Rectangle rectButton = new Rectangle(1,1,m_ButtonWidth - 1,this.Height - 2);
				return rectButton;
			}
		}

		#endregion

		#region function GetUpButtonRect

		private Rectangle GetUpButtonRect()
		{
			Rectangle rectButton = GetButtonsRect();
			Rectangle retVal = new Rectangle(rectButton.X,rectButton.Y,rectButton.Width,rectButton.Height/2);
			return retVal;
		}

		#endregion

		#region function GetDownButtonRect

		private Rectangle GetDownButtonRect()
		{
			Rectangle rectButton = GetButtonsRect();
			Rectangle retVal = new Rectangle(rectButton.X,rectButton.Height/2 + 1,rectButton.Width,rectButton.Height/2);
			return retVal;
		}

		#endregion


		#region override OnEndedInitialize

		public override void OnEndedInitialize()
		{
			this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);

			if(this.Text.Length == 0){
				this.Text = "";
			}
		}

		#endregion


		#region Public functions

		public void FlashControl()
		{
			if(!timer1.Enabled){
				m_FlasCounter  = 0;
				timer1.Enabled = true;
			}
		}

		#endregion
								
		#region Properties Implementaion

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
		public LeftRight ButtonsAlign
		{
			get{ return m_ButtonsAlign; }

			set{ 
				m_ButtonsAlign = value;
				
				// Buttons left
				if(value == LeftRight.Left){
					m_pTextBox.Left = this.Width - m_pTextBox.Width - 3;
				}
				else{
					m_pTextBox.Left = 3;
				}

				Invalidate(false);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool ReadOnly
		{
			get{ return m_ReadOnly; }

			set{ 
				m_ReadOnly          = value;
				m_pTextBox.ReadOnly = value;
			}
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

			set{ m_pTextBox.Text = value; }
		}

		public decimal DecValue
		{
			get{ return m_pTextBox.DecValue; }

			set{ m_pTextBox.DecValue = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public WEditBox_Mask Mask
		{
			get{ return m_pTextBox.Mask; }

			set{ m_pTextBox.Mask = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int DecimalPlaces
		{
			get{ return m_pTextBox.DecimalPlaces; }

			set{ m_pTextBox.DecimalPlaces = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int MaxLength
		{
			get{ return m_pTextBox.MaxLength; }

			set{ m_pTextBox.MaxLength = value; }
		}

		public int DecMinValue
		{
			get{ return m_pTextBox.DecMinValue; }

			set{ m_pTextBox.DecMinValue = value; }
		}

		public int DecMaxValue
		{
			get{ return m_pTextBox.DecMaxValue; }

			set{ m_pTextBox.DecMaxValue = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public HorizontalAlignment TextAlign
		{
			get{ return m_pTextBox.TextAlign; }

			set{ m_pTextBox.TextAlign = value; }
		}

		#endregion

		#region Events Implementation

		#region function OnButtonPressed

		protected virtual void OnButtonPressed() 
		{	
			// Raises the ladu change event; 	
			System.EventArgs oArg = new System.EventArgs();

	//		if(this.ButtonPressed != null){
	//			this.ButtonPressed(this, oArg);
	//		}						
		}

		#endregion

		#endregion
		
	}
}
