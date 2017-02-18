using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.UI.Controls {
	public delegate bool WMessage_EventHandler(object sender, ref Message m);

	/// <summary>
	/// Summary description for WControlBase.
	/// </summary>	
	[System.ComponentModel.ToolboxItem(false)]
	public class WControlBase : System.Windows.Forms.UserControl,ISupportInitialize {
		protected ViewStyle m_ViewStyle          = null;
		protected bool      m_UseStaticViewStyle = false;
		protected bool      m_Initing            = false;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WControlBase() : base() {		
			SetStyle(ControlStyles.ResizeRedraw,true);
			SetStyle(ControlStyles.DoubleBuffer  | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint,true);

			m_ViewStyle = new ViewStyle();

			m_ViewStyle.StyleChanged += new ViewStyleChangedEventHandler(this.OnViewStyleChanged);
			ViewStyle.staticViewStyle.StyleChanged += new ViewStyleChangedEventHandler(this.OnStaticViewStyleChanged);
		}		

		#region function Dispose

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if(disposing) {
				ViewStyle.staticViewStyle.StyleChanged -= new ViewStyleChangedEventHandler(this.OnStaticViewStyleChanged);

				//	if(components != null){
				//		components.Dispose();
				//	}
			}
			base.Dispose( disposing );
		}

		#endregion


		#region Events handling

		#region function ChildCtrlMouseLeave
		
		protected void ChildCtrlMouseLeave(object sender,System.EventArgs e) {
			DrawControl(this.ContainsFocus);
		}
				
		#endregion

		#region function ChildCtrlMouseEnter

		protected void ChildCtrlMouseEnter(object sender,System.EventArgs e) {
			DrawControl(true);
		}

		#endregion


		#region function OnViewStyleChanged(2)

		private void OnStaticViewStyleChanged(object sender,ViewStyle_EventArgs e) {	
			if(m_UseStaticViewStyle){
				m_ViewStyle.CopyFrom(ViewStyle.staticViewStyle);
			}
		}

		private void OnViewStyleChanged(object sender,ViewStyle_EventArgs e) {
			if(m_Initing){
				return;
			}

			OnViewStyleChanged(e);
		}

		#endregion
		
		#endregion


		#region function OnPaint

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			base.OnPaint(e);

			bool allowHot = (this.Enabled && !this.DesignMode) && !(this.IsMouseInControl && Control.MouseButtons == MouseButtons.Left && !this.ContainsFocus);
			bool hot = (this.IsMouseInControl || this.ContainsFocus) && allowHot;
			DrawControl(e.Graphics,hot);
		}

		#endregion


		protected virtual void DrawControl(bool hot) {
			using(Graphics g = this.CreateGraphics()){
				DrawControl(g,hot);
			}
		}

		protected virtual void DrawControl(Graphics g,bool hot) {
			//----- Draw border around control -------------------------//
			Painter.DrawBorder(g,m_ViewStyle,this.ClientRectangle,hot);
			//-----------------------------------------------------------//
		}

		#region virtual OnViewStyleChanged

		protected virtual void OnViewStyleChanged(ViewStyle_EventArgs e) {
			this.Invalidate(false);
		}

		#endregion

		#region virtual OnEndedInitialize

		public virtual void OnEndedInitialize() {
		}

		#endregion

		protected override void OnMouseEnter(System.EventArgs e) {
			base.OnMouseEnter(e);
			DrawControl(true);
		}

		protected override void OnMouseLeave(System.EventArgs e) {
			base.OnMouseLeave(e);
			DrawControl(this.ContainsFocus);
		}

		protected override void OnGotFocus(System.EventArgs e) {
			base.OnLostFocus(e);
			DrawControl(this.ContainsFocus);
		}

		protected override void OnLostFocus(System.EventArgs e) {
			base.OnLostFocus(e);
			DrawControl(this.ContainsFocus);
		}

		#region override OnControlAdded

		protected override void OnControlAdded(ControlEventArgs e) {
			base.OnControlAdded(e);

			e.Control.MouseEnter += new System.EventHandler(this.ChildCtrlMouseEnter);
			e.Control.MouseLeave += new System.EventHandler(this.ChildCtrlMouseLeave);
		}

		#endregion


		#region Properties Implementation

		[
		TypeConverter(typeof(ExpandableObjectConverter)),
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Category("View style")
		]
		public virtual ViewStyle ViewStyle {
			get{ return m_ViewStyle; }
		}

		[		
		Category("View style")
		]
		public virtual bool UseStaticViewStyle {
			get{ return m_UseStaticViewStyle; }

			set {
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

		public bool IsMouseInControl {
			get{
				Point mPos  = Control.MousePosition;
				bool retVal = this.ClientRectangle.Contains(this.PointToClient(mPos));
				return retVal;
			}
		}

		#endregion

		#region ISupportInitialize Implementation

		public void BeginInit() {
			m_Initing = true;
		}

		public void EndInit() {
			m_Initing = false;
			OnEndedInitialize();
		}

		#endregion
		
	}
}
