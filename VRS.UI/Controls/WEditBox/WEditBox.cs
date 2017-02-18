using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace VRS.UI.Controls {	
	/// <summary>
	/// EditBox control.
	/// </summary>
	public class WEditBox : WControlBase {
		private WTextBoxBase m_pTextBox;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components = null;

		public event EventHandler				EnterKeyPressed		= null;
		public new event WValidate_EventHandler Validate			= null;

		//private bool          m_Modified      = false;
		private bool          m_ReadOnly      = false;
		private int           m_FlasCounter   = 0;
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public WEditBox() {
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

			m_pTextBox.LostFocus += new System.EventHandler(this.m_pTextBox_OnLostFocus);
			m_pTextBox.GotFocus  += new System.EventHandler(this.m_pTextBox_OnGotFocus);

			this.BackColor = Color.White;
		}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.m_pTextBox = new VRS.UI.Controls.WTextBoxBase();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.m_pTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// m_pTextBox
			// 
			this.m_pTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.m_pTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_pTextBox.DecimalPlaces = 2;
			this.m_pTextBox.DecMaxValue = 999999999;
			this.m_pTextBox.DecMinValue = -999999999;
			this.m_pTextBox.Location = new System.Drawing.Point(3, 2);
			this.m_pTextBox.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.m_pTextBox.Name = "m_pTextBox";
			this.m_pTextBox.Size = new System.Drawing.Size(94, 13);
			this.m_pTextBox.TabIndex = 0;
			this.m_pTextBox.Text = "";
			this.m_pTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_pTextBox_KeyDown);
			this.m_pTextBox.TextChanged += new System.EventHandler(this.m_pTextBox_TextChanged);
			// 
			// timer1
			// 
			this.timer1.Interval = 150;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// WEditBox
			// 
			this.Controls.Add(this.m_pTextBox);
			this.Name = "WEditBox";
			this.Size = new System.Drawing.Size(100, 20);
			this.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			((System.ComponentModel.ISupportInitialize)(this.m_pTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		#region Events handling
				
		#region function m_pTextBox_OnGotFocus

		private void m_pTextBox_OnGotFocus(object sender, System.EventArgs e) {
			this.BackColor = m_ViewStyle.EditFocusedColor;			
		}

		#endregion

		#region function m_pTextBox_OnLostFocus

		private void m_pTextBox_OnLostFocus(object sender, System.EventArgs e) {
			this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);

			OnValidate();

			DrawControl(this.ContainsFocus);
		}

		#endregion

		#region function m_pTextBox_TextChanged

		private void m_pTextBox_TextChanged(object sender, System.EventArgs e) {
			base.OnTextChanged(new System.EventArgs());
		}

		#endregion

		
		#region function timer1_Tick

		private void timer1_Tick(object sender, System.EventArgs e) {
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

		protected override void OnViewStyleChanged(ViewStyle_EventArgs e) {
			switch(e.PropertyName) {
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

		
		#region function ProcessDialogKey

		protected override bool ProcessDialogKey(System.Windows.Forms.Keys keyData) {
			System.Windows.Forms.Keys key = keyData;
			if(key == System.Windows.Forms.Keys.Enter){
				this.OnEnterKeyPressed();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		#endregion


		#region override OnEndedInitialize

		public override void OnEndedInitialize() {
			this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);

			if(this.Text.Length == 0){
				this.Text = "";
			}
		}

		#endregion

		#region override OnEnabledChanged

		protected override void OnEnabledChanged(EventArgs e) {
			base.OnEnabledChanged(e);
			
			//		m_pTextBox.Enabled = this.Enabled;

			this.BackColor = m_ViewStyle.GetEditColor(this.ReadOnly,this.Enabled);
		}

		#endregion


		#region Public functions

		#region function FlashControl
		
		public void FlashControl() {
			if(!timer1.Enabled){
				m_FlasCounter  = 0;
				timer1.Enabled = true;
			}
		}
		
		#endregion
		
		#endregion
		
		#region Properties Implementation
		
		#region Color stuff
		
		/// <summary>
		/// 
		/// </summary>
		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public new Color BackColor {
			get{ return base.BackColor; }

			set{
				base.BackColor     = value;
				m_pTextBox.BackColor = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public override Color ForeColor {
			get{ return base.ForeColor; }

			set{
				base.ForeColor     = value;
				m_pTextBox.ForeColor = value;
			}
		}

		#endregion

		
		/// <summary>
		/// 
		/// </summary>
		public WEditBox_Mask Mask {
			get{ return m_pTextBox.Mask; }

			set{ m_pTextBox.Mask = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int DecimalPlaces {
			get{ return m_pTextBox.DecimalPlaces; }

			set{ m_pTextBox.DecimalPlaces = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int MaxLength {
			get{ return m_pTextBox.MaxLength; }

			set{ m_pTextBox.MaxLength = value; }
		}

		public int DecMinValue {
			get{ return m_pTextBox.DecMinValue; }

			set{ m_pTextBox.DecMinValue = value; }
		}

		public int DecMaxValue {
			get{ return m_pTextBox.DecMaxValue; }

			set{ m_pTextBox.DecMaxValue = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public char PasswordChar {
			get{ return m_pTextBox.PasswordChar; }

			set { m_pTextBox.PasswordChar = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public HorizontalAlignment TextAlign {
			get{ return m_pTextBox.TextAlign; }

			set{ m_pTextBox.TextAlign = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Multiline {
			get{ return m_pTextBox.Multiline; }

			set{ 
				m_pTextBox.Multiline = value; 
				m_pTextBox.AcceptsReturn = true;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public new int Height {
			get{ return base.Height; }

			set{
				if(value > m_pTextBox.Height + 1){
					base.Height = value;
						
					if(!this.Multiline){
						int yPos = (value - m_pTextBox.Height) / 2;
						m_pTextBox.Top = yPos;
					}
					else{
						m_pTextBox.Top    = 2;
						m_pTextBox.Height = this.Height - 4;
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public new Size Size {
			get{ return base.Size; }

			set{
				if(value.Height > m_pTextBox.Height + 1){					
					base.Size = value;

					if(!this.Multiline){
						int yPos = (value.Height - m_pTextBox.Height) / 2;
						m_pTextBox.Top = yPos;
					}
					else{
						m_pTextBox.Top    = 2;
						m_pTextBox.Height = this.Height - 4;
					}
				}				
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool ReadOnly {
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
		[
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public override string Text {
			get{ return m_pTextBox.Text; }

			set{ m_pTextBox.Text = value; }
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public decimal DecValue {
			get{ return m_pTextBox.DecValue; }

			set{ m_pTextBox.DecValue = value; }
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public bool IsModified {
			get{ return m_pTextBox.Modified; }

			set{ m_pTextBox.Modified = value; }
		}

		#endregion
	
		#region Events Implementation

		#region function OnEnterKeyPressed

		protected virtual void OnEnterKeyPressed() {	
			System.EventArgs oArg = new System.EventArgs();

			if(this.EnterKeyPressed != null){
				this.EnterKeyPressed(this, oArg);
			}			
		}

		#endregion


		#region function OnValidate

		protected virtual void OnValidate() {	
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

		private void m_pTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			base.OnKeyDown(e);
		}
		
		#endregion
						
	}
}
