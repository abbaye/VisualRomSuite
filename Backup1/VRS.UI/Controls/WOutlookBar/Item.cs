using System;
using System.Drawing;
using System.ComponentModel;

namespace VRS.UI.Controls.WOutlookBar
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class Item
	{
		private string    m_Caption    = "";
		private object    m_Tag        = null;
		private int       m_ImageIndex = -1;
		private bool      m_Enabled    = true;
		private bool      m_AllowStuck = true;
		private Items     m_pItems     = null;
		private Rectangle m_Bounds;
	
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Item(Items items)
		{
			m_pItems     = items;
			m_AllowStuck = m_pItems.Bar.Bars.WOutlookBar.AllowItemsStuck;
		}


		#region function OnItemNeedsUpdate

		public void OnItemNeedsUpdate()
		{
			m_pItems.Bar.Bars.WOutlookBar.UpdateAll();
		}

		#endregion


		#region Properties Implementation
		
		public string Caption
		{
			get{ return m_Caption; }

			set{ 
				m_Caption = value; 
				OnItemNeedsUpdate();
			}
		}

		public object Tag
		{
			get{ return m_Tag; }

			set{ m_Tag = value; }
		}
		
		[DefaultValue(-1)]
		public int ImageIndex
		{
			get{ return m_ImageIndex; }

			set{ 
				m_ImageIndex = value; 
				OnItemNeedsUpdate();
			}
		}

		public int Index
		{
			get{ return (m_pItems != null ? m_pItems.IndexOf(this) : -1); }
		}

		public bool AllowStuck
		{
			get{ return m_AllowStuck; }

			set{ 
				m_AllowStuck = value; 
				OnItemNeedsUpdate();
			}
		}

		public Bar Bar
		{
			get{ return m_pItems.Bar; }
		}


		#region Internal Properties

		internal Items Items
		{
			get{ return m_pItems; }
		}
	
		internal Rectangle Bounds
		{
			get{ return m_Bounds; }

			set{ m_Bounds = value; }
		}

		internal WOutlookBar WOutlookBar
		{
			get{ return m_pItems.Bar.Bars.WOutlookBar; }
		}

		internal ViewStyle ViewStyle
		{
			get{ return m_pItems.Bar.Bars.WOutlookBar.ViewStyle; }
		}

		#endregion

		#endregion

	}
}
