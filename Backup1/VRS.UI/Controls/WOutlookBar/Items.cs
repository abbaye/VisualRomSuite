using System;
using System.Collections;

namespace VRS.UI.Controls.WOutlookBar
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class Items : ArrayList
	{		
		private Bar m_pBar = null;

		public Items(Bar ownerBar) : base()
		{
			m_pBar = ownerBar;
		}


		#region function Add

		public Item Add(string caption,int imageIndex)
		{
			Item item = new Item(this);
			item.Caption = caption;
			item.ImageIndex = imageIndex;

			base.Add(item);
			this.Bar.Bars.WOutlookBar.UpdateAll();

			return item;
		}

		#endregion

		public new Item this[int nIndex]
		{
			get{ return (Item)base[nIndex]; }
		}
	

		#region Properties Implementation

		#region Internal Properties

		internal Bar Bar
		{
			get{ return m_pBar; }
		}

		#endregion

		#endregion

	}
}
