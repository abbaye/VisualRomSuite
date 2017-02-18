using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using VRS.UI.Controls;
using VRS.UI.Controls.WOutlookBar;

namespace VRS.UI {
	#region public class ViewStyle_EventArgs

	public class ViewStyle_EventArgs {
		private string m_PropertyName  = "";
		private object m_PropertyValue = null;

		public ViewStyle_EventArgs(string propertyName,object popertyValue) {
			m_PropertyName  = propertyName;
			m_PropertyValue = popertyValue;
		}

		#region Properties Implementation

		public string PropertyName {
			get{ return m_PropertyName; }
		}

		public object PropertyValue {
			get{ return m_PropertyValue; }
		}

		#endregion

	}

	#endregion

	/// <summary>
	/// 
	/// </summary>
	public delegate void ViewStyleChangedEventHandler(object sender,ViewStyle_EventArgs e);


	/// <summary>
	/// Summary description for ViewStyle.
	/// </summary>
	[DesignerSerializer(typeof(ViewStyleSerializer), typeof(CodeDomSerializer))]
	public class ViewStyle {
		public event ViewStyleChangedEventHandler StyleChanged = null;
		
		private  static ViewStyle m_ViewStyle = null;

		private Color      m_ControlBackColor         = Color.FromKnownColor(KnownColor.Control);
		private Color      m_BorderColor              = Color.DarkGray;
		private Color      m_BorderHotColor           = Color.Black;
		private Color      m_ButtonColor              = Color.FromKnownColor(KnownColor.Control);
		private Color      m_ButtonHotColor           = Color.FromArgb(182,193,214);
		private Color      m_ButtonPressedColor       = Color.FromArgb(210,218,232);
		private Color      m_EditColor                = Color.White;
		private Color      m_EditFocusedColor         = Color.Beige;
		private Color      m_EditReadOnlyColor        = Color.FromArgb(228,224,220);
		private Color      m_EditDisabledColor        = Color.Gainsboro;
		private Color      m_FlashColor               = Color.Pink;
		private Color      m_BarColor                 = Color.FromKnownColor(KnownColor.Control);
		private Color      m_BarHotColor              = Color.FromArgb(182,193,214);
		private Color      m_BarTextColor             = Color.Black;
		private Color      m_BarHotTextColor          = Color.Black;
		private Color      m_BarPressedColor          = Color.FromArgb(210,218,232);
		private Color      m_BarBorderColor           = Color.DarkGray;
		private Color      m_BarHotBorderColor        = Color.Black;
		private Color      m_BarClientAreaColor       = Color.FromArgb(128,128,128);
		private Color      m_BarItemSelectedColor     = Color.Silver;
		private Color      m_BarItemSelectedTextColor = Color.White;
		//		private Color      m_BarItemSelectedBordrerColor = 
		private Color      m_BarItemHotColor          = Color.FromArgb(182,193,214);
		private Color      m_BarItemPressedColor      = Color.FromArgb(210,218,232);
		private Color      m_BarItemBorderHotColor    = Color.Black;
		private Color      m_BarItemTextColor         = Color.White;
		private Color      m_BarItemHotTextColor      = Color.White;
		private ItemsStyle m_BarItemsStyle            = ItemsStyle.IconSelect;
		private Color      m_TextColor                = Color.Black;
		private Color      m_Text3DColor              = Color.White;
		private Color      m_TextShadowColor          = Color.DarkGray;
		private TextStyle  m_TextStyle                = TextStyle.Text3D | TextStyle.Shadow;
		private bool       m_ReadOnly                 = false;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ViewStyle() {			
		}

		#region Events handling

		#endregion

		
		#region function CopyTo

		/// <summary>
		/// Copies instance viewStyle to destination vieStyle.
		/// If destViewStyle is ReadOnly, values isn't copied !
		/// </summary>
		/// <param name="destView">ViewStyle where to copy.</param>
		public void CopyTo(ViewStyle destViewStyle) {
			destViewStyle.BorderColor              = this.BorderColor;
			destViewStyle.BorderHotColor           = this.BorderHotColor;
			destViewStyle.ButtonColor              = this.ButtonColor;
			destViewStyle.ButtonHotColor           = this.ButtonHotColor;
			destViewStyle.ButtonPressedColor       = this.ButtonPressedColor;
			destViewStyle.ControlBackColor         = this.ControlBackColor;
			//	destViewStyle.ControlForeColor         = this.ControlForeColor;
			destViewStyle.EditColor                = this.EditColor;
			destViewStyle.EditFocusedColor         = this.EditFocusedColor;
			destViewStyle.EditReadOnlyColor        = this.EditReadOnlyColor;
			destViewStyle.EditDisabledColor        = this.EditDisabledColor;
			destViewStyle.BarColor                 = this.BarColor;   
			destViewStyle.BarHotColor              = this.BarHotColor;   
			destViewStyle.BarTextColor             = this.BarTextColor;  
			destViewStyle.BarHotTextColor          = this.BarHotTextColor;  
			destViewStyle.BarPressedColor          = this.BarPressedColor;     
			destViewStyle.BarBorderColor           = this.BarBorderColor;     
			destViewStyle.BarHotBorderColor        = this.BarHotBorderColor;    
			destViewStyle.BarClientAreaColor       = this.BarClientAreaColor; 
			destViewStyle.BarItemSelectedColor     = this.BarItemSelectedColor;
			destViewStyle.BarItemSelectedTextColor = this.BarItemSelectedTextColor;
			destViewStyle.BarItemHotColor          = this.BarItemHotColor;     
			destViewStyle.BarItemPressedColor      = this.BarItemPressedColor;  
			destViewStyle.BarItemBorderHotColor    = this.BarItemBorderHotColor;
			destViewStyle.BarItemTextColor         = this.BarItemTextColor; 
			destViewStyle.BarItemHotTextColor      = this.BarItemHotTextColor;
			destViewStyle.BarItemsStyle            = this.BarItemsStyle;
			destViewStyle.TextColor                = this.TextColor;
			destViewStyle.Text3DColor              = this.Text3DColor;
			destViewStyle.TextShadowColor          = this.TextShadowColor;
			destViewStyle.FlashColor               = this.FlashColor;
		}

		#endregion

		#region function CopyFrom

		/// <summary>
		/// Copies ViewStyle from source ViewStyle.
		/// Copies values even if, ViewStyle is ReadOnly !
		/// </summary>
		/// <param name="sourceViewStyle"></param>
		public void CopyFrom(ViewStyle sourceViewStyle) {
			bool readOnly = m_ReadOnly;

			if(m_ReadOnly){
				m_ReadOnly = false;
			}

			this.BorderColor              = sourceViewStyle.BorderColor; 
			this.BorderHotColor           = sourceViewStyle.BorderHotColor; 
			this.ButtonColor              = sourceViewStyle.ButtonColor; 
			this.ButtonHotColor           = sourceViewStyle.ButtonHotColor; 
			this.ButtonPressedColor       = sourceViewStyle.ButtonPressedColor;
			this.ControlBackColor         = sourceViewStyle.ControlBackColor; 
			//	this.ControlForeColor         = sourceViewStyle.ControlForeColor; 
			this.EditColor                = sourceViewStyle.EditColor; 
			this.EditFocusedColor         = sourceViewStyle.EditFocusedColor;
			this.EditReadOnlyColor        = sourceViewStyle.EditReadOnlyColor;
			this.EditDisabledColor        = sourceViewStyle.EditDisabledColor;
			this.BarColor                 = sourceViewStyle.BarColor;
			this.BarHotColor              = sourceViewStyle.BarHotColor;
			this.BarTextColor             = sourceViewStyle.BarTextColor;
			this.BarHotTextColor          = sourceViewStyle.BarHotTextColor;
			this.BarPressedColor          = sourceViewStyle.BarPressedColor;
			this.BarBorderColor           = sourceViewStyle.BarBorderColor;
			this.BarHotBorderColor        = sourceViewStyle.BarHotBorderColor;
			this.BarClientAreaColor       = sourceViewStyle.BarClientAreaColor;
			this.BarItemSelectedColor     = sourceViewStyle.BarItemSelectedColor;
			this.BarItemSelectedTextColor = sourceViewStyle.BarItemSelectedTextColor;
			this.BarItemHotColor          = sourceViewStyle.BarItemHotColor; 
			this.BarItemPressedColor      = sourceViewStyle.BarItemPressedColor;
			this.BarItemBorderHotColor    = sourceViewStyle.BarItemBorderHotColor;
			this.BarItemTextColor         = sourceViewStyle.BarItemTextColor;
			this.BarItemHotTextColor      = sourceViewStyle.BarItemHotTextColor;
			this.BarItemsStyle            = sourceViewStyle.BarItemsStyle;
			this.TextColor                = sourceViewStyle.TextColor;
			this.Text3DColor              = sourceViewStyle.Text3DColor;
			this.TextShadowColor          = sourceViewStyle.TextShadowColor;
			this.FlashColor               = sourceViewStyle.FlashColor; 	
		
			m_ReadOnly = readOnly;
		}

		#endregion


		#region function GetEditColor

		public Color GetEditColor(bool readOnly,bool enabled) {
			// Normal edit color wanted
			if(!readOnly && enabled){
				return this.EditColor;
			}
			else{
				// ReadOnly edit color wanted
				if(readOnly && enabled){
					return this.EditReadOnlyColor;
				}
				else{ // Disabled edit color wanted
					return this.EditDisabledColor;
				}
			}
		}

		#endregion

		#region function GetBorderColor

		public Color GetBorderColor(bool hot) {
			if(hot){
				return m_BorderHotColor;
			}
			else{
				return m_BorderColor;
			}
		}

		#endregion

		#region function GetButtonColor

		public Color GetButtonColor(bool hot,bool pressed) {
			if(hot){
				if(pressed){
					return m_ViewStyle.ButtonPressedColor;
				}
				else{
					return m_ViewStyle.ButtonHotColor;
				}
			}
			else{
				return m_ViewStyle.ButtonColor;
			}
		}

		#endregion

		
		#region function RestoreDefault

		public void RestoreDefault() {
			this.BorderColor              = Color.DarkGray;     
			this.BorderHotColor           = Color.Black;
			this.ButtonColor              = Color.FromKnownColor(KnownColor.Control);
			this.ButtonHotColor           = Color.FromArgb(182,193,214);
			this.ButtonPressedColor       = Color.FromArgb(210,218,232);
			this.ControlBackColor         = Color.FromKnownColor(KnownColor.Control);
			//		this.ControlForeColor         =;
			this.EditColor                = Color.White;
			this.EditFocusedColor         = Color.Beige;
			this.EditReadOnlyColor        = Color.FromArgb(228,224,220);
			this.EditDisabledColor        = Color.Gainsboro;
			this.FlashColor               = Color.Pink;
			this.BarColor                 = Color.FromKnownColor(KnownColor.Control);
			this.BarHotColor              = Color.FromArgb(182,193,214);
			this.BarTextColor             = Color.Black;
			this.BarHotTextColor          = Color.Black;
			this.BarPressedColor          = Color.FromArgb(210,218,232);
			this.BarBorderColor           = Color.DarkGray;
			this.BarHotBorderColor        = Color.Black;
			this.BarClientAreaColor       = Color.FromArgb(128,128,128);
			this.BarItemSelectedColor     = Color.Silver;
			this.BarItemSelectedTextColor = Color.White;
			//	this.BarItemSelectedBordrerColor = 
			this.BarItemHotColor          = Color.FromArgb(182,193,214);
			this.BarItemPressedColor      = Color.FromArgb(210,218,232);
			this.BarItemBorderHotColor    = Color.Black;
			this.BarItemTextColor         = Color.White;
			this.BarItemHotTextColor      = Color.White;
			this.BarItemsStyle            = ItemsStyle.IconSelect;
			this.TextColor                = Color.Black;
			this.Text3DColor              = Color.White;
			this.TextShadowColor          = Color.DarkGray;
			//	this.TextStyle                = TextStyle.Text3D | TextStyle.Shadow;
		}

		#endregion

		#region function SaveToXml

		public byte[] SaveToXml() {
			byte[] retVal = null;

			DataSet ds = new DataSet("dsViewStyle");
			DataTable dt = ds.Tables.Add("ViewStyle");

			dt.Columns.Add("BorderColor",Type.GetType("System.String"));
			dt.Columns.Add("BorderHotColor",Type.GetType("System.String"));
			dt.Columns.Add("ButtonColor",Type.GetType("System.String"));
			dt.Columns.Add("ButtonHotColor",Type.GetType("System.String"));
			dt.Columns.Add("ButtonPressedColor",Type.GetType("System.String"));
			dt.Columns.Add("ControlBackColor",Type.GetType("System.String"));
			dt.Columns.Add("ControlForeColor",Type.GetType("System.String"));
			dt.Columns.Add("EditColor",Type.GetType("System.String"));
			dt.Columns.Add("EditFocusedColor",Type.GetType("System.String"));
			dt.Columns.Add("EditReadOnlyColor",Type.GetType("System.String"));
			//
			dt.Columns.Add("FlashColor",Type.GetType("System.String"));
			dt.Columns.Add("BarColor",Type.GetType("System.String"));
			dt.Columns.Add("BarHotColor",Type.GetType("System.String"));
			dt.Columns.Add("BarTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarHotTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarPressedColor",Type.GetType("System.String"));
			dt.Columns.Add("BarBorderColor",Type.GetType("System.String"));
			dt.Columns.Add("BarHotBorderColor",Type.GetType("System.String"));
			dt.Columns.Add("BarClientAreaColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemSelectedColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemSelectedTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemSelectedBorder",Type.GetType("System.String"));
			dt.Columns.Add("BarItemHotColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemPressedColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemBorderHotColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemHotTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemsStyle",Type.GetType("System.String"));
			dt.Columns.Add("TextColor",Type.GetType("System.String"));
			dt.Columns.Add("Text3DColor",Type.GetType("System.String"));
			dt.Columns.Add("TextShadowColor",Type.GetType("System.String"));
			//	dt.Columns.Add("TextStyle",Type.GetType("System.String"));
		

			DataRow dr = dt.NewRow();
			dr["BorderColor"]              = this.BorderColor.ToArgb();
			dr["BorderHotColor"]           = this.BorderHotColor.ToArgb();
			dr["ButtonColor"]              = this.ButtonColor.ToArgb();
			dr["ButtonHotColor"]           = this.ButtonHotColor.ToArgb();
			dr["ButtonPressedColor"]       = this.ButtonPressedColor.ToArgb();
			dr["ControlBackColor"]         = this.ControlBackColor.ToArgb();
			dr["ControlForeColor"]         = this.ControlForeColor.ToArgb();
			dr["EditColor"]                = this.EditColor.ToArgb();
			dr["EditFocusedColor"]         = this.EditFocusedColor.ToArgb();
			dr["EditReadOnlyColor"]        = this.EditReadOnlyColor.ToArgb();
			//
			dr["FlashColor"]               = this.FlashColor.ToArgb();
			dr["BarColor"]                 = this.BarColor.ToArgb();
			dr["BarHotColor"]              = this.BarHotColor.ToArgb();
			dr["BarTextColor"]             = this.BarTextColor.ToArgb();
			dr["BarHotTextColor"]          = this.BarHotTextColor.ToArgb();
			dr["BarPressedColor"]          = this.BarPressedColor.ToArgb();
			dr["BarBorderColor"]           = this.BarBorderColor.ToArgb();
			dr["BarHotBorderColor"]        = this.BarHotBorderColor.ToArgb();
			dr["BarClientAreaColor"]       = this.BarClientAreaColor.ToArgb();
			dr["BarItemSelectedColor"]     = this.BarItemSelectedColor.ToArgb();
			dr["BarItemSelectedTextColor"] = this.BarItemSelectedTextColor.ToArgb();
			//		dr["BarItemSelectedBorder"]    = this.BarItemSelectedBorder.ToArgb();
			dr["BarItemHotColor"]          = this.BarItemHotColor.ToArgb();
			dr["BarItemPressedColor"]      = this.BarItemPressedColor.ToArgb();
			dr["BarItemBorderHotColor"]    = this.BarItemBorderHotColor.ToArgb();
			dr["BarItemTextColor"]         = this.BarItemTextColor.ToArgb();
			dr["BarItemHotTextColor"]      = this.BarItemHotTextColor.ToArgb();
			dr["BarItemsStyle"]            = Convert.ToInt32(this.BarItemsStyle);
			dr["FlashColor"]               = this.FlashColor.ToArgb();
			dr["TextColor"]                = this.TextColor.ToArgb();
			dr["Text3DColor"]              = this.Text3DColor.ToArgb();
			dr["TextShadowColor"]          = this.TextShadowColor.ToArgb();
			//	dr["TextStyle"]                = this.TextStyle;

			dt.Rows.Add(dr);

			MemoryStream strm = new MemoryStream();
			ds.WriteXml(strm,XmlWriteMode.IgnoreSchema);
			retVal = strm.ToArray();
			strm.Close();

			ds.Dispose();

			return retVal;
		}

		#endregion

		#region function LoadFromXml

		public void LoadFromXml(byte[] viewStyleXML) {
			try {
				MemoryStream strm = new MemoryStream(viewStyleXML);
				DataSet ds = new DataSet();
				ds.ReadXml(strm);
				strm.Close();

				DataRow dr = ds.Tables["ViewStyle"].Rows[0];

				this.BorderColor              = Color.FromArgb(Convert.ToInt32(dr["BorderColor"]));
				this.BorderHotColor           = Color.FromArgb(Convert.ToInt32(dr["BorderHotColor"]));
				this.ButtonColor              = Color.FromArgb(Convert.ToInt32(dr["ButtonColor"]));
				this.ButtonHotColor           = Color.FromArgb(Convert.ToInt32(dr["ButtonHotColor"]));
				this.ButtonPressedColor       = Color.FromArgb(Convert.ToInt32(dr["ButtonPressedColor"]));
				this.ControlBackColor         = Color.FromArgb(Convert.ToInt32(dr["ControlBackColor"]));
				//	this.ControlForeColor         = Color.FromArgb(Convert.ToInt32(dr["ControlForeColor"]));
				this.EditColor                = Color.FromArgb(Convert.ToInt32(dr["EditColor"]));
				this.EditFocusedColor         = Color.FromArgb(Convert.ToInt32(dr["EditFocusedColor"]));
				this.EditReadOnlyColor        = Color.FromArgb(Convert.ToInt32(dr["EditReadOnlyColor"]));
				//
				this.BarColor                 = Color.FromArgb(Convert.ToInt32(dr["BarColor"]));
				this.BarHotColor              = Color.FromArgb(Convert.ToInt32(dr["BarHotColor"]));
				this.BarTextColor             = Color.FromArgb(Convert.ToInt32(dr["BarTextColor"]));
				this.BarHotTextColor          = Color.FromArgb(Convert.ToInt32(dr["BarHotTextColor"]));
				this.BarPressedColor          = Color.FromArgb(Convert.ToInt32(dr["BarPressedColor"]));
				this.BarBorderColor           = Color.FromArgb(Convert.ToInt32(dr["BarBorderColor"]));
				this.BarHotBorderColor        = Color.FromArgb(Convert.ToInt32(dr["BarHotBorderColor"]));
				this.BarClientAreaColor       = Color.FromArgb(Convert.ToInt32(dr["BarClientAreaColor"]));
				this.BarItemSelectedColor     = Color.FromArgb(Convert.ToInt32(dr["BarItemSelectedColor"]));
				this.BarItemSelectedTextColor = Color.FromArgb(Convert.ToInt32(dr["BarItemSelectedTextColor"]));
				//		this.BarItemSelectedBorder    = Color.FromArgb(Convert.ToInt32(dr["BarItemSelectedBorder"]));
				this.BarItemHotColor          = Color.FromArgb(Convert.ToInt32(dr["BarItemHotColor"]));
				this.BarItemPressedColor      = Color.FromArgb(Convert.ToInt32(dr["BarItemPressedColor"]));
				this.BarItemBorderHotColor    = Color.FromArgb(Convert.ToInt32(dr["BarItemBorderHotColor"]));
				this.BarItemTextColor         = Color.FromArgb(Convert.ToInt32(dr["BarItemTextColor"]));
				this.BarItemHotTextColor      = Color.FromArgb(Convert.ToInt32(dr["BarItemHotTextColor"]));
				this.BarItemsStyle            = (ItemsStyle)Convert.ToInt32(dr["BarItemsStyle"]);
				this.FlashColor               = Color.FromArgb(Convert.ToInt32(dr["FlashColor"]));
				this.TextColor                = Color.FromArgb(Convert.ToInt32(dr["TextColor"]));
				this.Text3DColor              = Color.FromArgb(Convert.ToInt32(dr["Text3DColor"]));
				this.TextShadowColor          = Color.FromArgb(Convert.ToInt32(dr["TextShadowColor"]));
				//		this.TextStyle                = Color.FromArgb(Convert.ToInt32(dr["TextStyle"]));

				ds.Dispose();
			}
			catch(Exception x) {
				MessageBox.Show(x.Message);
				RestoreDefault();
			}
		}

		#endregion


		#region function MustSerialize

		public bool MustSerialize(string propertyName,object value) {
			bool retVal = false;

			if(value == null){
				return false;
			}

			switch(propertyName) {
				case "BorderColor":
					if(ViewStyle.staticViewStyle.BorderColor != (Color)value){
						retVal = true;
					}
					break;
				case "BorderHotColor":
					if(ViewStyle.staticViewStyle.BorderHotColor != (Color)value){
						retVal = true;
					}
					break;
				case "ButtonColor":
					if(ViewStyle.staticViewStyle.ButtonColor != (Color)value){
						retVal = true;
					}
					break;
				case "ButtonHotColor":
					if(ViewStyle.staticViewStyle.ButtonHotColor != (Color)value){
						retVal = true;
					}
					break;
				case "ButtonPressedColor":
					if(ViewStyle.staticViewStyle.ButtonPressedColor != (Color)value){
						retVal = true;
					}
					break;
				case "ControlBackColor":
					if(ViewStyle.staticViewStyle.ControlBackColor != (Color)value){
						retVal = true;
					}
					break;
				case "ControlForeColor":
					if(ViewStyle.staticViewStyle.ControlForeColor != (Color)value){
						retVal = true;
					}
					break;
				case "EditColor":
					if(ViewStyle.staticViewStyle.EditColor != (Color)value){
						retVal = true;
					}
					break;
				case "EditFocusedColor":
					if(ViewStyle.staticViewStyle.EditFocusedColor != (Color)value){
						retVal = true;
					}
					break;
				case "EditReadOnlyColor":
					if(ViewStyle.staticViewStyle.EditReadOnlyColor != (Color)value){
						retVal = true;
					}
					break;
				case "EditDisabledColor":
					if(ViewStyle.staticViewStyle.EditDisabledColor != (Color)value){
						retVal = true;
					}
					break;
				case "BarColor":
					if(ViewStyle.staticViewStyle.BarColor != (Color)value){
						retVal = true;
					}   
					break;
				case "BarHotColor":
					if(ViewStyle.staticViewStyle.BarHotColor != (Color)value){
						retVal = true;
					}   
					break;
				case "BarTextColor":
					if(ViewStyle.staticViewStyle.BarTextColor != (Color)value){
						retVal = true;
					}  
					break;
				case "BarHotTextColor":
					if(ViewStyle.staticViewStyle.BarHotTextColor != (Color)value){
						retVal = true;
					} 
					break;
				case "BarPressedColor":
					if(ViewStyle.staticViewStyle.BarPressedColor != (Color)value){
						retVal = true;
					}     
					break;
				case "BarBorderColor":
					if(ViewStyle.staticViewStyle.BarBorderColor != (Color)value){
						retVal = true;
					}    
					break;
				case "BarHotBorderColor":
					if(ViewStyle.staticViewStyle.BarHotBorderColor != (Color)value){
						retVal = true;
					}    
					break;
				case "BarClientAreaColor":
					if(ViewStyle.staticViewStyle.BarClientAreaColor != (Color)value){
						retVal = true;
					} 
					break;
				case "BarItemSelectedColor":
					if(ViewStyle.staticViewStyle.BarItemSelectedColor != (Color)value){
						retVal = true;
					}
					break;
				case "BarItemSelectedTextColor":
					if(ViewStyle.staticViewStyle.BarItemSelectedTextColor != (Color)value){
						retVal = true;
					}
					break;
				case "BarItemHotColor":
					if(ViewStyle.staticViewStyle.BarItemHotColor != (Color)value){
						retVal = true;
					}     
					break;
				case "BarItemPressedColor":
					if(ViewStyle.staticViewStyle.BarItemPressedColor != (Color)value){
						retVal = true;
					}  
					break;
				case "BarItemBorderHotColor":
					if(ViewStyle.staticViewStyle.BarItemBorderHotColor != (Color)value){
						retVal = true;
					}
					break;
				case "BarItemTextColor":
					if(ViewStyle.staticViewStyle.BarItemTextColor != (Color)value){
						retVal = true;
					}
					break;
				case "BarItemHotTextColor":
					if(ViewStyle.staticViewStyle.BarItemHotTextColor != (Color)value){
						retVal = true;
					}
					break;
				case "BarItemsStyle":
					if(ViewStyle.staticViewStyle.BarItemsStyle != (ItemsStyle)value){
						retVal = true;
					}
					break;
				case "FlashColor":
					if(ViewStyle.staticViewStyle.FlashColor != (Color)value){
						retVal = true;
					}
					break;
				case "TextColor":
					if(ViewStyle.staticViewStyle.TextColor != (Color)value){
						retVal = true;
					}
					break;
				case "Text3DColor":
					if(ViewStyle.staticViewStyle.Text3DColor != (Color)value){
						retVal = true;
					}
					break;
				case "TextShadowColor":
					if(ViewStyle.staticViewStyle.TextShadowColor != (Color)value){
						retVal = true;
					}
					break;
			}

			return retVal;
		}

		#endregion


		#region Properties Implementation

		#region Border stuff

		public Color BorderColor {
			get{ return m_BorderColor; }

			set{ 
				if(!m_ReadOnly && m_BorderColor != value){
					m_BorderColor = value;
					OnStyleChanged("BorderColor",value);
				}
			}
		}

		public Color BorderHotColor {
			get{ return m_BorderHotColor; }

			set{ 
				if(!m_ReadOnly && m_BorderHotColor != value){
					m_BorderHotColor = value; 
					OnStyleChanged("BorderHotColor",value);
				}
			}
		}

		#endregion
		
		#region Button stuff

		public Color ButtonColor {
			get{ return m_ButtonColor; }

			set{ 
				if(!m_ReadOnly && m_ButtonColor != value){
					m_ButtonColor = value; 
					OnStyleChanged("ButtonColor",value);
				}
			}
		}

		public Color ButtonHotColor {
			get{ return m_ButtonHotColor; }

			set{ 
				if(!m_ReadOnly && m_ButtonHotColor != value){
					m_ButtonHotColor = value; 
					OnStyleChanged("ButtonHotColor",value);
				}
			}
		}

		public Color ButtonPressedColor {
			get{ return m_ButtonPressedColor; }

			set{ 
				if(!m_ReadOnly && m_ButtonPressedColor != value){
					m_ButtonPressedColor = value; 
					OnStyleChanged("ButtonPressedColor",value);
				}
			}
		}

		#endregion

		#region Control stuff

		public Color ControlBackColor {
			get{ return m_ControlBackColor; }

			set{
				if(!m_ReadOnly && m_ControlBackColor != value){
					m_ControlBackColor = value; 
					OnStyleChanged("ControlBackColor",value);
				}
			}
		}

		public Color ControlForeColor {
			get{ return Color.AliceBlue; }
		}

		public Color FlashColor {
			get{ return m_FlashColor; }

			set{ 
				if(!m_ReadOnly && m_FlashColor != value){
					m_FlashColor = value; 
					OnStyleChanged("FlashColor",value);
				}
			}
		}

		#endregion

		#region Edit stuff

		/// <summary>
		/// 
		/// </summary>
		public Color EditColor {
			get{ return m_EditColor; }

			set{ 
				if(!m_ReadOnly && m_EditColor != value){
					m_EditColor = value; 
					OnStyleChanged("EditColor",value);
				}
			}
		}

		public Color EditFocusedColor {
			get{ return m_EditFocusedColor; }

			set{ 
				if(!m_ReadOnly && m_EditFocusedColor != value){
					m_EditFocusedColor = value; 
					OnStyleChanged("EditFocusedColor",value);
				}
			}
		}

		public Color EditReadOnlyColor {
			get{ return m_EditReadOnlyColor; }

			set{ 
				if(!m_ReadOnly && m_EditReadOnlyColor != value){
					m_EditReadOnlyColor = value; 
					OnStyleChanged("EditReadOnlyColor",value);
				}
			}
		}

		public Color EditDisabledColor {
			get{ return m_EditDisabledColor; }

			set{ 
				if(!m_ReadOnly && m_EditDisabledColor != value){
					m_EditDisabledColor = value; 
					OnStyleChanged("EditDisabledColor",value);
				}
			}
		}

		#endregion

		#region Bar stuff

		[
		Category("OutlookBar"),
		]
		public Color BarColor {
			get{ return m_BarColor; }

			set{ 
				if(!m_ReadOnly && m_BarColor != value){
					m_BarColor = value; 
					OnStyleChanged("BarColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarHotColor {
			get{ return m_BarHotColor; }

			set{ 
				if(!m_ReadOnly && m_BarHotColor != value){
					m_BarHotColor = value; 
					OnStyleChanged("BarHotColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarTextColor {
			get{ return m_BarTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarTextColor != value){
					m_BarTextColor = value; 
					OnStyleChanged("BarTextColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarHotTextColor {
			get{ return m_BarHotTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarHotTextColor != value){
					m_BarHotTextColor = value; 
					OnStyleChanged("BarHotTextColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarPressedColor {
			get{ return m_BarPressedColor; }

			set{ 
				if(!m_ReadOnly && m_BarPressedColor != value){
					m_BarPressedColor = value; 
					OnStyleChanged("BarPressedColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarBorderColor {
			get{ return m_BarBorderColor; }

			set{ 
				if(!m_ReadOnly && m_BarBorderColor != value){
					m_BarBorderColor = value; 
					OnStyleChanged("BarBorderColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarHotBorderColor {
			get{ return m_BarHotBorderColor; }

			set{ 
				if(!m_ReadOnly && m_BarHotBorderColor != value){
					m_BarHotBorderColor = value; 
					OnStyleChanged("BarHotBorderColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarClientAreaColor {
			get{ return m_BarClientAreaColor; }

			set{ 
				if(!m_ReadOnly && m_BarClientAreaColor != value){
					m_BarClientAreaColor = value; 
					OnStyleChanged("BarClientAreaColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarItemSelectedColor {
			get{ return m_BarItemSelectedColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemSelectedColor != value){
					m_BarItemSelectedColor = value; 
					OnStyleChanged("BarItemSelectedColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarItemSelectedTextColor {
			get{ return m_BarItemSelectedTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemSelectedTextColor != value){
					m_BarItemSelectedTextColor = value; 
					OnStyleChanged("BarItemSelectedTextColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarItemHotColor {
			get{ return m_BarItemHotColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemHotColor != value){
					m_BarItemHotColor = value; 
					OnStyleChanged("BarItemHotColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarItemPressedColor {
			get{ return m_BarItemPressedColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemPressedColor != value){
					m_BarItemPressedColor = value; 
					OnStyleChanged("BarItemPressedColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarItemBorderHotColor {
			get{ return m_BarItemBorderHotColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemBorderHotColor != value){
					m_BarItemBorderHotColor = value; 
					OnStyleChanged("BarItemBorderHotColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarItemTextColor {
			get{ return m_BarItemTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemTextColor != value){
					m_BarItemTextColor = value; 
					OnStyleChanged("BarItemTextColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public Color BarItemHotTextColor {
			get{ return m_BarItemHotTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemHotTextColor != value){
					m_BarItemHotTextColor = value; 
					OnStyleChanged("BarItemHotTextColor",value);
				}
			}
		}

		[
		Category("OutlookBar"),
		]
		public ItemsStyle BarItemsStyle {
			get{ return m_BarItemsStyle; }

			set{ 
				if(!m_ReadOnly && m_BarItemsStyle != value){
					m_BarItemsStyle = value; 
					OnStyleChanged("BarItemsStyle",value);
				}
			}
		}

		#endregion

		#region Text stuff

		[
		Category("Text"),
		]
		public Color TextColor {
			get{ return m_TextColor; }

			set{
				if(!m_ReadOnly && m_TextColor != value){
					m_TextColor = value;
					OnStyleChanged("TextColor",value);
				}
			}
		}

		[
		Category("Text"),
		]
		public Color Text3DColor {
			get{ return m_Text3DColor; }

			set{
				if(!m_ReadOnly && m_Text3DColor != value){
					m_Text3DColor = value;
					OnStyleChanged("Text3DColor",value);
				}				
			}
		}

		[
		Category("Text"),
		]
		public Color TextShadowColor {
			get{ return m_TextShadowColor; }

			set{
				if(!m_ReadOnly && m_TextShadowColor != value){
					m_TextShadowColor = value;
					OnStyleChanged("TextShadowColor",value);
				}				
			}
		}

		[
		Category("Text"),
		]
		public TextStyle TextStyle {
			get{ return m_TextStyle; }
			/*
						set{
							if(!m_ReadOnly && m_TextStyle != value){
								m_TextStyle = value;
								OnStyleChanged("TextStyle",value);
							}	
						}*/
		}

		#endregion


		#region View stuff (this)

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public bool ReadOnly {
			get{ return m_ReadOnly; }

			set{ m_ReadOnly = value; }
		}

		#endregion

		#endregion

		#region Events Implementation

		protected void OnStyleChanged(string propertyName,object popertyValue) {
			// Raises the event; 	
			ViewStyle_EventArgs oArg = new ViewStyle_EventArgs(propertyName,popertyValue);

			if(this.StyleChanged != null){
				this.StyleChanged(this, oArg);
			}	
		}

		#endregion


		//------ Static functions / properties ----------//
		
		#region Static Properties

		public static ViewStyle staticViewStyle {
			get{
				if(m_ViewStyle == null){
					m_ViewStyle = new ViewStyle();
				}
				
				return m_ViewStyle;				
			}
		}

		#endregion

		//-----------------------------------------------//

	}
}
