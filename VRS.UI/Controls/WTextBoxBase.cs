using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace VRS.UI.Controls
{
	/// <summary>
	/// Summary description for WTextBoxBase.
	/// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class WTextBoxBase : System.Windows.Forms.TextBox, ISupportInitialize
	{
		public event WMessage_EventHandler ProccessMessage;
		
		private WEditBox_Mask m_WEditBox_Mask = WEditBox_Mask.Text;
		private int           m_DecPlaces     = 2;
		private int           m_DecMinValue   = -999999999;
		private int           m_DecMaxValue   =  999999999;
		private bool          m_Initing       = false;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WTextBoxBase()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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

		#region override WndProc

		protected override void WndProc(ref Message m)
		{	
			if(!OnWndProc(ref m)){				
				base.WndProc(ref m);
			}
		}

		#endregion

		#region override OnKeyPress

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			switch(m_WEditBox_Mask)
			{
				case WEditBox_Mask.Numeric:
					e.Handled = !IsNeedeKey_Numeric(e.KeyChar);
					break;

				case WEditBox_Mask.Date:
					e.Handled = Handle_Date(e.KeyChar);
					break;
				default:
					break;
			}
						
			base.OnKeyPress(e);
		}

		#endregion

		#region override OnKeyDown

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if(this.Mask == WEditBox_Mask.Date && e.KeyData == Keys.Delete){
				e.Handled = true;
			}

			base.OnKeyDown(e);
		}

		#endregion

		#region override OnTextChanged

		protected override void OnTextChanged(System.EventArgs e)
		{
			if(m_WEditBox_Mask == WEditBox_Mask.Numeric){
				if(this.Text.StartsWith("-")){
					this.ForeColor = Color.Red;
				}
				else{
					this.ForeColor = Color.Black;
				}
			}

			base.OnTextChanged(e);
		}
		
		#endregion

		
		#region function Handle_Date

		private bool Handle_Date(char keyChar)
		{
			if(this.ReadOnly){
				return true;
			}

			int curIndex   = this.SelectionStart;
			string text    = this.Text;
			int monthIndex = 0;
			int dayIndex   = 3;
            			
			if(curIndex >= 10 || text.Length < 10){
				return true;
			}

			//--- Get if day->month->year or month->day->year ---//
			DateTime t     = new DateTime(2000,2,1);
			string   tStr  = t.ToShortDateString();
			if(tStr.IndexOf("1") > tStr.IndexOf("2")){
				dayIndex   = 3;
				monthIndex = 0;
			}
			else{
				dayIndex   = 0;
				monthIndex = 3;
			}
			//--------------------------------------------------//
			
			//--- Digit was pressed -------------------------------------//
			if(char.IsDigit(keyChar)){
			
				string nText = text.Insert(curIndex,keyChar.ToString());
					   nText = nText.Remove(curIndex+1,1);

				int newCursPos = ++curIndex;
					
				//--- If cursor in month block -------------------------//
				if(curIndex >= monthIndex && curIndex < monthIndex + 2){
					int mPos1Val = Convert.ToInt32(nText.Substring(monthIndex,1));
					int mPos2Val = Convert.ToInt32(nText.Substring(monthIndex + 1,1));
				
					// eg. |13 = 03 ('3' was pressed, '|' = cursor position where key pressed)
					if(mPos1Val > 1){
						nText = nText.Insert(monthIndex,"0");
						nText = nText.Remove(monthIndex + 1,1);

						nText = nText.Insert(monthIndex + 1,mPos1Val.ToString());
						nText = nText.Remove(monthIndex + 2,1);

						mPos2Val = mPos1Val;
						mPos1Val = 0;

						newCursPos = monthIndex + 3;
					}
	
					// eg. |13 = 10 ('1' was pressed, '|' = cursor position where key pressed)
					if(mPos1Val == 1 && mPos2Val > 2  && monthIndex == curIndex-1){
						nText = nText.Insert(monthIndex + 1,"0");
						nText = nText.Remove(monthIndex + 2,1);						
					}

					// eg. |00 = 01 ('0' was pressed, '|' = cursor position where key pressed)
					if(mPos1Val == 0 && mPos2Val == 0){
						nText = nText.Insert(monthIndex + 1,"1");
						nText = nText.Remove(monthIndex + 2,1);
					}
				}
				//----------------------------------------------------------------//

				//--- If cursor in day block ------------------------------------//
				if(curIndex >= dayIndex && curIndex < dayIndex + 2){
					int dPos1Val = Convert.ToInt32(nText.Substring(dayIndex,1));
					int dPos2Val = Convert.ToInt32(nText.Substring(dayIndex + 1,1));
					int month    = Convert.ToInt32(nText.Substring(monthIndex,2));
					int year     = Convert.ToInt32(nText.Substring(6,4));
				
					int maxDayPos1Val = 2;
					int maxDayPos2Val = 0;
					if(DateTime.DaysInMonth(year,month) >= 30){
						maxDayPos1Val = 3;
						maxDayPos2Val = DateTime.DaysInMonth(year,month)-30;
					}
					else{
						maxDayPos2Val = DateTime.DaysInMonth(year,month)-20;
					}

					// eg. |41 = 04 ('4' was pressed, '|' = cursor position where key pressed)
					if(dPos1Val > maxDayPos1Val){
						nText = nText.Insert(dayIndex,"0");
						nText = nText.Remove(dayIndex + 1,1);

						nText = nText.Insert(dayIndex + 1,dPos1Val.ToString());
						nText = nText.Remove(dayIndex + 2,1);

						dPos2Val = dPos1Val;
						dPos1Val = 0;

						newCursPos = dayIndex + 3;
					}

					// eg. |33 = 30 ('3' was pressed, '|' = cursor position where key pressed)
					if(dPos1Val == maxDayPos1Val && dPos2Val > maxDayPos2Val && dayIndex == curIndex-1){
						nText = nText.Insert(dayIndex + 1,"0");
						nText = nText.Remove(dayIndex + 2,1);
					}

					// eg. |00 = 01 ('0' was pressed, '|' = cursor position where key pressed)
					if(dPos1Val == 0 && dPos2Val == 0){
						nText = nText.Insert(dayIndex + 1,"1");
						nText = nText.Remove(dayIndex + 2,1);
					}

				}
				//----------------------------------------------------//

				if(IsDateOk(nText)){
					this.Text = nText;

					if(newCursPos < 9 && (nText.Substring(newCursPos,1) == "," || nText.Substring(newCursPos,1) == "." || nText.Substring(newCursPos,1) == "/")){
						newCursPos++;
					}
					this.SelectionStart = newCursPos;
				}
			}

			//---- Char was pressed -----------------------//
			else{
				string kChar = keyChar.ToString();
				if(kChar == "," || kChar == "."){

					if(curIndex > 0 && (text.Substring(curIndex-1,1) != "," || text.Substring(curIndex-1,1) != "." || text.Substring(curIndex-1,1) != "/")){
						if(text.IndexOf(".",curIndex) > -1){
							this.SelectionStart = text.IndexOf(".",curIndex) + 1;
						}
					}
				}
			}

			return true;
		}

		#endregion

		#region function IsNeedeKey_Numeric

		private bool IsNeedeKey_Numeric(char pressedChar)
		{
			if(this.ReadOnly){
				return true;
			}

			char decSep = Convert.ToChar(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
			string val         = this.Text;
			bool   isSeparator = val.IndexOf(decSep) > -1;
			int    length      = val.Length;
			int    curPos      = this.SelectionStart;
			int    sepPos      = val.IndexOf(decSep);

			//--- Clear selection if any 
			if(this.SelectionLength > 0){
				val = val.Remove(this.SelectionStart,this.SelectionLength);
				length = val.Length;
			}
			
			// If char is number.
			if(char.IsDigit(pressedChar)){
                
				// Don't allow to add '-' inside number.
				// Avoiding following numbers '6-12.00'.
				if(val.IndexOf("-") > -1 && val.IndexOf("-") > curPos-1){
					return false;
				}
								
				// If number starts with '0' and pressedChar = '0', don't allow to enter number.
				// Avoiding following numbers '06'.				
				if(((val.StartsWith("0") && curPos == 1) || (val.StartsWith("-0") && curPos == 2))){
					return false;
				}
					
				// Check that decimal places aren't exceeded.
				if(isSeparator)
				{
					// If current position is inside deciaml places '_,x|xx'.
					if(curPos > sepPos){
						if((curPos - sepPos) > m_DecPlaces){
							return false;
						}

						// If there is maximun number of decimal places.
						if((length - sepPos) > m_DecPlaces){
							string newVal = val.Remove(curPos,1);
							       newVal = newVal.Insert(curPos,pressedChar.ToString());

							this.Text = newVal;
							this.SelectionStart = curPos + 1; 
							return false;
						}
					}
				}

				// If '0' was pressed.
				if(pressedChar == '0'){					
					if(val.StartsWith("-") && length > 1 && curPos == 1){
						return false;					
					}

					if(length > 0 && curPos == 0){
						return false;	
					}
				}

				//---- Check that minimum and maximum isn't exceeded -----//
				string nText = val.Insert(curPos,pressedChar.ToString());
				decimal decVal = Core.ConvertToDeciaml(nText);
				if(decVal > m_DecMaxValue || decVal < m_DecMinValue)
				{
					return false;
				}
				//----------------------------------------------------------//

				return true;
			}
			//------------- Decimal separator '. / ,' or '-' pressed ----------//
			else{
				
				// Decimal separator pressed and there isn't any.
				if(pressedChar == decSep && !isSeparator && m_DecPlaces > 0){
					// Check that decimal places aren't exceeded,
					// when setting decimal place in the middle on number.
					// eg. 232,312 and allowed decimal places = 2.
					if(length - curPos > m_DecPlaces){
						return false;
					}

					return true;
				}

				// If '-' pressed
				if(pressedChar == '-'){
					if(!val.StartsWith("-") && curPos == 0){
						return true;
					}
				}

				// BackSpace pressed.
				if(pressedChar == '\b'){
					return true;
				}

				return false;
			}
		}

		#endregion

		#region function Handle_Ip

		private void Handle_Ip()
		{
		}

		#endregion


		#region function IsDateOk

		private bool IsDateOk(string date)
		{
			try
			{
				DateTime dummy = Convert.ToDateTime(date);
				return true;
			}
			catch(Exception x)
			{
				return false;
			}
		}

		#endregion

		#region function DateToString

		private string DateToString(DateTime val)
		{
			string sep = System.Globalization.DateTimeFormatInfo.CurrentInfo.DateSeparator;
			string format = "";

			//--- Get if day->month->year or month->day->year ---//
			DateTime t     = new DateTime(2000,2,1);
			string   tStr  = t.ToShortDateString();					
			if(tStr.IndexOf("1") > tStr.IndexOf("2")){
				format = "MM" + sep + "dd" + sep + "yyyy";
			}
			else{
				format = "dd" + sep + "MM" + sep + "yyyy";
			}
			//--------------------------------------------------//

			return val.ToString(format);
		}

		#endregion


		#region Properties Implementation

		/// <summary>
		/// 
		/// </summary>
		public WEditBox_Mask Mask
		{
			get{ return m_WEditBox_Mask; }

			set{ 
				m_WEditBox_Mask = value;
				this.Text = "";
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int DecimalPlaces
		{
			get{ return m_DecPlaces; }

			set{ m_DecPlaces = value; }
		}
		
		[
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		]
		public DateTime DateValue
		{
			get{
				if(this.Mask == WEditBox_Mask.Date){
					return Convert.ToDateTime(this.Text);
				}

				return DateTime.Today;
			}

			set{
				if(this.Mask == WEditBox_Mask.Date){
					this.Text = DateToString(value);
				}
			}
		}

		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public decimal DecValue
		{
			get{ 
				if(this.Mask == WEditBox_Mask.Numeric){
					return Convert.ToDecimal(this.Text);
				}
				else{
					return 0;
				}
			}

			set{
				if(this.Mask == WEditBox_Mask.Numeric){
					this.Text = value.ToString("f" + m_DecPlaces.ToString());
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
			get{ return base.Text; }

			set{
				string val = value;
				if(this.Mask == WEditBox_Mask.Numeric){
					if(val.Length == 0){
						val = "0";
					}

					char decSep = Convert.ToChar(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
					val = val.Replace(',',decSep);
					val = val.Replace('.',decSep);
					val = Core.ConvertToDeciaml(val).ToString("f" + m_DecPlaces.ToString());
				}

				if(this.Mask == WEditBox_Mask.Date){
					if(!IsDateOk(val)){
						base.Text = DateToString(DateTime.Today);
					}
					else{
						base.Text = DateToString(Convert.ToDateTime(val));
					}

					return;
				}

				base.Text = val;
			}
		}

		public int DecMinValue
		{
			get{ return m_DecMinValue; }

			set{
				m_DecMinValue = value;
			}
		}

		public int DecMaxValue
		{
			get{ return m_DecMaxValue; }

			set{
				m_DecMaxValue = value;
			}
		}

		#endregion

		#region Events Implementation

		public virtual bool OnWndProc(ref Message m)
		{
			if(ProccessMessage != null){
				return ProccessMessage(this,ref m);
			}

			return false;
		}

		#endregion

		#region ISupportInitialize Implementation

		public void BeginInit()
		{
			m_Initing = true;
		}

		public void EndInit()
		{
			m_Initing = false;
		
			if(this.Mask == WEditBox_Mask.Date){				
				string val = this.Text;

				if(!IsDateOk(this.Text)){
					val = "01.01.2002";
				}

				string sep = System.Globalization.DateTimeFormatInfo.CurrentInfo.DateSeparator;
					   val = val.Replace(".",sep);
					   val = val.Replace("/",sep);

				this.Text = val;
			}
		}

		#endregion

	}
}
