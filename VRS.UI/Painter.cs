using System;
using System.Drawing;
using System.Windows.Forms;
using VRS.UI.Controls;

namespace VRS.UI {
	/// <summary>
	/// Summary description for Painter.
	/// </summary>
	public class Painter {
		public Painter() {			
		}

		public static void DrawButton(Graphics g,ViewStyle viewStyle,Rectangle buttonRect,bool border_hot,bool btn_hot,bool btn_pressed) {
			if(btn_hot){
				if(btn_pressed){
					g.FillRectangle(new SolidBrush(viewStyle.ButtonPressedColor),buttonRect);
				}
				else{
					g.FillRectangle(new SolidBrush(viewStyle.ButtonHotColor),buttonRect);
				}
			}
			else{
				g.FillRectangle(new SolidBrush(viewStyle.ButtonColor),buttonRect);
			}

			//----- Draw border around button ----------------------------//
			// Append borders to button
			buttonRect = new Rectangle(buttonRect.X-1,buttonRect.Y-1,buttonRect.Width+1,buttonRect.Height+1);
			if(border_hot || btn_hot || btn_pressed){
				g.DrawRectangle(new Pen(viewStyle.BorderHotColor),buttonRect);
			}
			else{
				g.DrawRectangle(new Pen(viewStyle.BorderColor),buttonRect);
			}
		}

		public static void DrawBorder(Graphics g,ViewStyle viewStyle,Rectangle controlRect,bool hot) {
			controlRect = new Rectangle(controlRect.X,controlRect.Y,controlRect.Width - 1,controlRect.Height - 1);

			if(hot){
				g.DrawRectangle(new Pen(viewStyle.BorderHotColor),controlRect);
			}
			else{
				g.DrawRectangle(new Pen(viewStyle.BorderColor),controlRect);
			}
		}

		public static void DrawArea(Graphics g,Rectangle drawRect,Color borderColor,Color fillColor) {

		}

		public static void DrawIcon(Graphics g,Icon icon,Rectangle drawRect,bool grayed,bool pushed) {
			// If Graphics or Icon isn't valid,
			// just skip this function.
			if(g == null || icon == null){
				return;
			}

			// Icon pushed state, update icon location.
			if(pushed){
				drawRect.Location = new Point(drawRect.X + 1,drawRect.Y + 1);
			}

			//----- Draw Icon ---
			if(grayed){
				// Draw grayed icon
				Size s = new Size(drawRect.Size.Width-1,drawRect.Size.Height-1);
				ControlPaint.DrawImageDisabled(g,new Bitmap(icon.ToBitmap(),s),drawRect.X,drawRect.Y,Color.Transparent);
			}
			else{
				// Draw normal icon
				g.DrawIcon(icon,drawRect);
			}
		}


	}
}
