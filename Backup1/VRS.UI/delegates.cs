using System;

namespace VRS.UI
{
	public interface IWValidate
	{
		bool Validate();
	}

	/// <summary>
	/// 
	/// </summary>
	public class WValidate_EventArgs
	{
		private string m_ControlName    = "";
		private object m_Value          = null;
		private bool   m_IsValid        = true;
		private bool   m_AllowMoveFocus = true;
		private bool   m_Flash          = true;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="controlName"></param>
		/// <param name="controlValue"></param>
		public WValidate_EventArgs(string controlName,object controlValue)
		{
			m_ControlName = controlName;
			m_Value       = controlValue;
		}

		#region Properties Implementation

		/// <summary>
		/// Control name which raised event.
		/// </summary>
		public string ControlName
		{
			get{ return m_ControlName; }
		}

		/// <summary>
		/// Controls value.
		/// </summary>
		public object Value
		{
			get{ return m_Value; }
		}

		/// <summary>
		/// Set false if not validated successfully.
		/// </summary>
		public bool IsValid
		{
			get{ return m_IsValid; }

			set{ m_IsValid = value; }
		}

		/// <summary>
		/// If false, control doesn't allow to move focus to next contol if validation failed.
		/// </summary>
		public bool AllowMoveFocus
		{
			get{ return m_AllowMoveFocus; }

			set{ m_AllowMoveFocus = value; }
		}

		/// <summary>
		/// If true, bilnks control if validation failed.
		/// </summary>
		public bool FlashControl
		{
			get{ return m_Flash; }

			set{ m_Flash = value; }
		}

		#endregion

	}

	/// <summary>
	/// 
	/// </summary>
	public delegate void WValidate_EventHandler(object sender,WValidate_EventArgs e);
}
