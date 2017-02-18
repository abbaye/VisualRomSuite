using System;
using System.Drawing;

namespace VRS.UI.Controls.WOutlookBar
{
	public enum HittedObject
	{
		Bar  = 1,
		Item = 2,
		UpScrollButton = 3,
		DownScrollButton = 4,
		None = 5,
	}

	/// <summary>
	/// Summary description for HitInfo.
	/// </summary>
	public class HitInfo
	{
		private HittedObject m_HittedObject = HittedObject.None;
		private Bar          m_HittedBar    = null;
		private Item         m_HittedItem   = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hitPoint"></param>
		/// <param name="outlookBar"></param>
		public HitInfo(Point hitPoint,WOutlookBar outlookBar)
		{
			Bar activeBar = outlookBar.ActiveBar;

			//--- First look if bar was hitted -------//
			foreach(Bar bar in outlookBar.Bars){
				if(bar.BarRect.Contains(hitPoint)){
					m_HittedObject = HittedObject.Bar;
					m_HittedBar    = bar;				
					return;
				}
			}
			//----------------------------------------//

			//--- look if scroll buttons hitted ------//
			if(outlookBar.IsUpScrollBtnVisible && outlookBar.UpScrollBtnRect.Contains(hitPoint)){
				m_HittedObject = HittedObject.UpScrollButton;
				return;
			}

            if(outlookBar.IsDownScrollBtnVisible && outlookBar.DownScrollBtnRect.Contains(hitPoint)){
				m_HittedObject = HittedObject.DownScrollButton;
				return;
			}
			//----------------------------------------//

			//--- look if bar item hitted -----------//
			if(activeBar != null){
				for(int it = activeBar.FirstVisibleIndex;it<activeBar.Items.Count;it++){
					Item item = activeBar.Items[it];
					if(item.Bounds.Contains(hitPoint) && activeBar.BarClientRect.Contains(hitPoint)){
						m_HittedObject = HittedObject.Item;
						m_HittedItem   = item;
						return;
					}
				}
			}
			//----------------------------------------//
		}


		#region function Compare

		public bool Compare(HitInfo hitInfo)
		{
			if(hitInfo == null){
				return false;
			}

			if(hitInfo.HittedObject == this.HittedObject){

		//		if(this.HittedObject == HittedObject.DownScrollButton || this.HittedObject == HittedObject.DownScrollButton){
		//			return true;
		//		}

				if(hitInfo.HittedObject == HittedObject.Bar){
					if(m_HittedBar != null && hitInfo.HittedBar != null && object.ReferenceEquals(hitInfo.HittedBar,this.HittedBar)){
						return true;
					}
				}

				if(hitInfo.HittedObject == HittedObject.Item){
					if(m_HittedItem != null && hitInfo.HittedItem != null && object.ReferenceEquals(hitInfo.HittedItem,this.HittedItem)){
						return true;
					}
				}
			}			
			
			return false;			
		}

		#endregion

		
		#region Properties Implementation

		public HittedObject HittedObject
		{
			get{ return m_HittedObject; }
		}

		public Bar HittedBar
		{
			get{ return m_HittedBar;}
		}

		public Item HittedItem
		{
			get{ return m_HittedItem; }
		}

		#endregion

	}
}
