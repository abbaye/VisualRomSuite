using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.UI.Controls
{
	[Flags]
	public enum TextStyle
	{
		Text3D = 0,
		Shadow = 1,
	}

	/// <summary>
	/// Label control.
	/// </summary>
	public class WLabel : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		protected ViewStyle         m_ViewStyle          = null;
		protected bool              m_UseStaticViewStyle = false;
		private HorizontalAlignment m_TextAlignment      = HorizontalAlignment.Center;
		private string              m_Text               = "";		
		private Color               m_BorderColor        = Color.DarkGray;
		private Color               m_BorderHotColor     = Color.Green;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WLabel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

			SetStyle(ControlStyles.ResizeRedraw,true);
			SetStyle(ControlStyles.DoubleBuffer,true);
			SetStyle(ControlStyles.AllPaintingInWmPaint,true);
			SetStyle(ControlStyles.Selectable,false);

			m_ViewStyle = new ViewStyle();
			
			ViewStyle.staticViewStyle.StyleChanged += new ViewStyleChangedEventHandler(this.OnStaticViewStyleChanged);
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
			// 
			// WLabel
			// 
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "WLabel";
			this.Size = new System.Drawing.Size(150, 24);

		}
		#endregion


		#region Events handling

		#region function OnViewStyleChanged

		private void OnStaticViewStyleChanged(object sender,ViewStyle_EventArgs e)
		{	
			if(m_UseStaticViewStyle){
				m_ViewStyle.CopyFrom(ViewStyle.staticViewStyle);
			}

			this.Invalidate();
		}

		#endregion

		#endregion


		#region function OnPaint

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			StringFormat format  = new StringFormat();
			format.LineAlignment = StringAlignment.Center;

			if(m_TextAlignment == HorizontalAlignment.Left){
				format.Alignment = StringAlignment.Near;
			}

			if(m_TextAlignment == HorizontalAlignment.Center){
				format.Alignment = StringAlignment.Center;
			}

			if(m_TextAlignment == HorizontalAlignment.Right){
				format.Alignment = StringAlignment.Far;
			}

			Rectangle txtRect = this.ClientRectangle;

	//		if(m_Shadow){
				// Draw Shadow
				Rectangle sRect = new Rectangle(new Point(txtRect.X+2,txtRect.Y+2),txtRect.Size);
				e.Graphics.DrawString(this.Text,this.Font,new SolidBrush(m_ViewStyle.TextShadowColor),sRect,format);
	//		}

	//		if(m_Effect3D){
				// Draw 3d effect
				Rectangle dRect = new Rectangle(new Point(txtRect.X+1,txtRect.Y+1),txtRect.Size);
				e.Graphics.DrawString(this.Text,this.Font,new SolidBrush(m_ViewStyle.Text3DColor),dRect,format);
	//		}

			// Draw normal text
			e.Graphics.DrawString(m_Text,this.Font,new SolidBrush(m_ViewStyle.TextColor),txtRect,format);
		}

		#endregion


		#region function IsMouseInButtonRect

		private bool IsMouseInControl()
		{
			Rectangle rectButton = this.ClientRectangle;
			Point mPos = Control.MousePosition;
			if(rectButton.Contains(this.PointToClient(mPos))){
				return true;
			}
			else{
				return false;
			}
		}

		#endregion

		
		#region Properties Implementation

		[
		TypeConverter(typeof(ExpandableObjectConverter)),
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Category("View style")
		]
		public virtual ViewStyle ViewStyle
		{
			get{ return m_ViewStyle; }
		}

		[		
		Category("View style")
		]
		public virtual bool UseStaticViewStyle
		{
			get{ return m_UseStaticViewStyle; }

			set
			{
				m_UseStaticViewStyle = value; 

				if(value){
					ViewStyle.staticViewStyle.CopyTo(m_ViewStyle);
					m_ViewStyle.ReadOnly = true;
				}
				else{
					m_ViewStyle.ReadOnly = false;
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
		public override string Text
		{
			get{ return m_Text; }

			set{
				m_Text = value;
				this.Invalidate();
			}
		}

		public HorizontalAlignment TextAlign
		{
			get{ return m_TextAlignment; }

			set{
				m_TextAlignment = value;
				this.Invalidate();
			}
		}

		#endregion

	}
}
