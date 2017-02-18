using System;

namespace VRS.UI.Controls.WOutlookBar
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Class1
	{
		public Class1()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region Drawing stuff

		internal void DrawItem(Graphics g,bool hot,bool pressed)
		{
			Image image = null;
			if(this.ImageIndex > -1 && this.WOutlookBar.ImageList != null && this.ImageIndex < this.WOutlookBar.ImageList.Images.Count){
				image = this.WOutlookBar.ImageList.Images[0];
			}

			if(hot || this.Equals(this.WOutlookBar.StuckenItem)){
				Rectangle iRect = new Rectangle(this.Bounds.Location,this.Bounds.Size);

				ItemsStyle itemStyle = this.Bar.ItemsStyle;

				//--- If must use default item style ---------//
				//--- First load from ViewStyle
				if(this.Bar.ItemsStyle == ItemsStyle.UseDefault){
					itemStyle = this.ViewStyle.BarItemsStyle;				
				}
	
				//--- If ViewStyle retuned UseDefault, set IconSelect as default
				if(itemStyle == ItemsStyle.UseDefault){
					itemStyle = ItemsStyle.IconSelect;
				}
				//-------------------------------------------//
				
				//--- If item style is IconSelect
				if(itemStyle == ItemsStyle.IconSelect){
					iRect = new Rectangle(((this.Bounds.Width-32)/2)-1,this.Bounds.Y+2,34,34);
				}

				//------- Draw item -----------------------------------------------------------//
				//--- if not stucken(selected) item
				if(!this.Equals(this.WOutlookBar.StuckenItem)){
					if(pressed){
						g.FillRectangle(new SolidBrush(this.ViewStyle.BarItemPressedColor),iRect);
					}
					else{
						g.FillRectangle(new SolidBrush(this.ViewStyle.BarItemHotColor),iRect);
					}
				}
				else{ //---- If stucken(selected) item
					g.FillRectangle(new SolidBrush(this.ViewStyle.BarItemSelectedColor),iRect);
				}

				g.DrawRectangle(new Pen(this.ViewStyle.BarItemBorderHotColor),iRect);
				//----------------------------------------------------------------------------//
			}
			else{
				g.FillRectangle(new SolidBrush(this.ViewStyle.BarClientAreaColor),this.Bounds.X,this.Bounds.Y,this.Bounds.Width+1,this.Bounds.Height+1);
			}

			
			//---- Draw image ---------------------------//
			Rectangle imgRect = new Rectangle((this.Bounds.Width-32)/2,this.Bounds.Y+4,32,32);
			if(image != null){
				g.DrawImage(image,imgRect);
			}
			//--------------------------------------------//

			//---- Draw items text ---------------------------------------------------------------//
			Rectangle txtRect = new Rectangle(1,imgRect.Bottom + 3,this.Bounds.Width-2,this.Bounds.Bottom - imgRect.Bottom + 3);
			g.DrawString(this.Caption,this.Bar.ItemsFont,new SolidBrush(hot ? this.ViewStyle.BarItemHotTextColor : this.ViewStyle.BarItemTextColor),txtRect,this.Bar.ItemsStringFormat);
			//-------------------------------------------------------------------------------------//
			
		}

		#region funcion DrawNormal

		internal void DrawNormal()
		{
			using(Graphics g = this.WOutlookBar.CreateGraphics()){
				DrawItem(g,false,false);
			}
		}

		#endregion

		#endregion
	}
}
