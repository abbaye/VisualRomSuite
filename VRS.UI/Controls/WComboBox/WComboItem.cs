using System;
using System.Collections;

namespace VRS.UI.Controls
{
	/// <summary>
	/// ComboBox Item.
	/// </summary>
	public class WComboItem
	{
		private string m_Text = "";
		private object m_Tag  = null;

		public WComboItem(string text)
		{
			m_Text = text;
		}

		public WComboItem(string text,object tag) : this(text)
		{
			m_Tag = tag;
		}


		public override string ToString()
		{
			return m_Text;
		}
        
		#region Properties Implementation

		public string Text
		{
			get{ return m_Text; }

			set{ m_Text = value; }
		}

		public object Tag
		{
			get{ return m_Tag; }
			
			set{ m_Tag = value; }
		}

		#endregion

	}

	public class WComboItems : ArrayList
	{
		private WComboBox m_WComboBox = null;

		public WComboItems(WComboBox parent) : base()
		{
			m_WComboBox = parent;
		}


		public int Add(WComboItem item)
		{
			return base.Add(item);
		}

		public int Add(string text)
		{
			return Add(text,null);
		}

		public int Add(string text,object tag)
		{
			return base.Add(new WComboItem(text,tag));
		}


		public new WComboItem this[int nIndex]
		{
			get{ 				
				return (WComboItem)base[nIndex];
			}
		}
		
		public override void Clear()
		{
			base.Clear();

			m_WComboBox.Text = "";
		}
	}
}
