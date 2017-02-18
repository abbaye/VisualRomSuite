using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace VRS.UI.Controls.WOutlookBar
{
	public enum ItemsStyle
	{
		FullSelect = 1,
		IconSelect = 2,
		UseDefault = 7,
	}

	/// <summary>
	/// Summary description for Bar.
	/// </summary>
	public class Bar
	{
		private HorizontalAlignment m_TextAlign = HorizontalAlignment.Center;
		private HorizontalAlignment m_ItemsTextAlign = HorizontalAlignment.Center;
		private string     m_Caption             = "";		
		private Color      m_TextColor           = Color.Black;
		private Font       m_Font                = null;
		private object     m_Tag                 = null;
		private Items      m_pItems              = null;
		private Color      m_ItemsTextColor      = Color.Black;
		private Font       m_ItemsFont           = null;
		private ItemsStyle m_ItemsStyle          = ItemsStyle.UseDefault;
		private Color      m_BarBackColor        = Color.Coral;
		private Color      m_ItemsBackColor      = Color.Aqua;
		private Bars       m_pBars               = null;   
		private Rectangle  m_BarRect;
		private Rectangle  m_BarClientRect;
		private int        m_FirstVisibleItem    = 0;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="bars"></param>
		public Bar(Bars bars)
		{
			m_pItems    = new Items(this);
			m_pBars     = bars;
			m_Font      = (Font)bars.WOutlookBar.Font.Clone();
			m_ItemsFont = (Font)bars.WOutlookBar.Font.Clone();
		}


		#region function OnBarNeedsUpdate

		public void OnBarNeedsUpdate()
		{
			m_pBars.WOutlookBar.UpdateAll();
		}

		#endregion

		internal bool ItemFullyVisible(Item item)
		{
			return m_BarClientRect.Contains(item.Bounds);
		}


		#region function IsItemVisible

		public bool IsItemVisible(Item item)
		{
			if(m_pBars.WOutlookBar.ActiveBar.Items.Contains(item)){
				if(item.Index >= m_FirstVisibleItem){
					return true;
				}
			}
			
			return false;			
		}

		#endregion

		#region Properties Implementation

		public string Caption
		{
			get{ return m_Caption; }

			set{
				m_Caption = value;
				OnBarNeedsUpdate();
			}
		}

		public HorizontalAlignment TextAlign
		{
			get{ return m_TextAlign; }

			set{
				m_TextAlign = value;
				OnBarNeedsUpdate();
			}
		}

		public object Tag
		{
			get{ return m_Tag; }

			set{ m_Tag = value; }
		}

		public Font Font
		{
			get{ return m_Font; }

			set{
				m_Font = value;
				OnBarNeedsUpdate();
			}
		}

		public int Index
		{
			get{ return m_pBars.IndexOf(this); }
		}

		[Editor(typeof(ItemsCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public Items Items
		{
			get{ return m_pItems; }
		}

		public HorizontalAlignment ItemsTextAlign
		{
			get{ return m_ItemsTextAlign; }

			set{
				m_ItemsTextAlign = value;
				OnBarNeedsUpdate();
			}
		}

		public ItemsStyle ItemsStyle
		{
			get{ return m_ItemsStyle; }

			set{ m_ItemsStyle = value; }
		}

		public Font ItemsFont
		{
			get{ return m_ItemsFont; }

			set{
				m_ItemsFont = value;
				OnBarNeedsUpdate();
			}
		}


		#region internal Properties

		internal Rectangle BarRect
		{
			get{ return m_BarRect; }

			set{ m_BarRect = value; }
		}

		internal Rectangle BarClientRect
		{
			get{ return m_BarClientRect; }

			set{ m_BarClientRect = value; }
		}

		internal int FirstVisibleIndex
		{
			get{ return m_FirstVisibleItem; }

			set{ m_FirstVisibleItem = value; }
		}

		internal bool IsLastVisible
		{
			get{
				if(this.Items.Count < 1 || (this.Items[this.Items.Count-1].Bounds.Bottom < this.BarClientRect.Bottom)){
					return true;
				}
				else{
					return false;
				}
			}
		}

		internal Bars Bars
		{
			get{ return m_pBars; }
		}

		internal StringFormat BarStringFormat
		{
			get{
				StringFormat format = new StringFormat();
				format.LineAlignment = StringAlignment.Center;

				if(this.ItemsTextAlign == HorizontalAlignment.Center){				
					format.Alignment = StringAlignment.Center;				
				}
				
				if(this.ItemsTextAlign == HorizontalAlignment.Left){
					format.Alignment = StringAlignment.Near;
				}

				if(this.ItemsTextAlign == HorizontalAlignment.Right){
					format.Alignment = StringAlignment.Far;
				}

				return format;
			}
		}

		internal StringFormat ItemsStringFormat
		{
			get{
				StringFormat format = new StringFormat();
				format.LineAlignment = StringAlignment.Near;

				if(this.ItemsTextAlign == HorizontalAlignment.Center){				
					format.Alignment = StringAlignment.Center;				
				}
				
				if(this.ItemsTextAlign == HorizontalAlignment.Left){
					format.Alignment = StringAlignment.Near;
				}

				if(this.ItemsTextAlign == HorizontalAlignment.Right){
					format.Alignment = StringAlignment.Far;
				}

				return format;
			}
		}

		#endregion

		#endregion

	}
}
