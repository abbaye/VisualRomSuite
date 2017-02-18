using System;

namespace VRS.UI.Controls.WOutlookBar
{
	/// <summary>
	/// Summary description for ItemClicked_EventArgs.
	/// </summary>
	public class ItemClicked_EventArgs
	{
		private Item m_Item = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public ItemClicked_EventArgs(Item item)
		{
			m_Item = item;
		}


		#region Properties Implementation

		public Item Item
		{
			get{ return m_Item; }
		}

		#endregion

	}
}
