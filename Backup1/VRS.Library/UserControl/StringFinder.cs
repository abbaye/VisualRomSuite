using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Threading; 

using VRS.Library.Projet;
using VRS.Library.Convertion;
using VRS.Library.Table.TBL;

namespace VRS.Library.UserControl {
	/// <summary>
	/// Description résumée de StringFinder.
	/// </summary>
	public class StringFinder : System.Windows.Forms.UserControl {
		/// <summary>
		/// Fichier dans lequel rechercher
		/// </summary>
		private string _FileName;

		/// <summary>
		/// Flag qui verifie si l'operation a été annulé
		/// </summary>
		private bool _Cancel = false;

		/// <summary>
		/// Strings de messages
		/// </summary>
		private string _String_NoItemFound = "Aucune chaîne n'a été trouvé en ce qui correspond à votre recherche.";
		private string _String_TotalItem = "Chaînes trouvés";

		private System.Windows.Forms.ImageList ListImageToolBar;
		private AxprjVbHex.AxHexEd HexEdit;
		private System.Windows.Forms.ListView lvFind;
		private System.Windows.Forms.ProgressBar pb;
		private System.Windows.Forms.ColumnHeader colHeadPos;
		private System.Windows.Forms.ColumnHeader colHeadValue;
		private System.Windows.Forms.ToolBarButton tbButtonFind;
		private System.Windows.Forms.ToolBarButton tblButtonStop;
		private System.Windows.Forms.ToolBar toolBarFind;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private VRS.Library.UserControl.GradientLabel GradLabelParam;
		private System.Windows.Forms.Panel panelParam;
		private VRS.Library.UserControl.TBLCombo tblCombo1;
		private VRS.UI.Controls.WEditBox txtTextToFind;
		private System.Windows.Forms.TrackBar trackBarQuality;
		private System.Windows.Forms.Label lblTextToFind;
		private System.Windows.Forms.Label lblTBL;
		private System.Windows.Forms.Label lblQuality;
		private System.Windows.Forms.Label lblMinLen;
		private System.Windows.Forms.Label lblMatchCase;
		private VRS.Library.UserControl.RangeBar rngbar;
		private System.Windows.Forms.TrackBar trackBarTaille;
		private VRS.Library.UserControl.HexaTextBox htbMin;
		private VRS.Library.UserControl.HexaTextBox htbMax;
		private System.Windows.Forms.Label lblRange;
		private System.Windows.Forms.ToolTip toolTipTrackBar;
		private System.Windows.Forms.Label lblBest;
		private System.Windows.Forms.Label lblLow;
		private System.Windows.Forms.Label lblHuge;
		private System.Windows.Forms.Label lblSmall;
		private System.ComponentModel.IContainer components;

		public StringFinder() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			trackBarTaille.Value  = 3;
			trackBarQuality.Value = 3;

			htbMax.Height = 23;
			htbMin.Height = 23;

			//Small Border
			VRS.Library.Win32.StaticBorder.ThinBorder(lvFind.Handle.ToInt32(), true) ;
		}

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StringFinder));
			this.HexEdit = new AxprjVbHex.AxHexEd();
			this.lvFind = new System.Windows.Forms.ListView();
			this.colHeadPos = new System.Windows.Forms.ColumnHeader();
			this.colHeadValue = new System.Windows.Forms.ColumnHeader();
			this.tbButtonFind = new System.Windows.Forms.ToolBarButton();
			this.toolBarFind = new System.Windows.Forms.ToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.tblButtonStop = new System.Windows.Forms.ToolBarButton();
			this.ListImageToolBar = new System.Windows.Forms.ImageList(this.components);
			this.pb = new System.Windows.Forms.ProgressBar();
			this.GradLabelParam = new VRS.Library.UserControl.GradientLabel();
			this.panelParam = new System.Windows.Forms.Panel();
			this.htbMax = new VRS.Library.UserControl.HexaTextBox();
			this.lblRange = new System.Windows.Forms.Label();
			this.rngbar = new VRS.Library.UserControl.RangeBar();
			this.lblMatchCase = new System.Windows.Forms.Label();
			this.lblMinLen = new System.Windows.Forms.Label();
			this.lblBest = new System.Windows.Forms.Label();
			this.lblLow = new System.Windows.Forms.Label();
			this.tblCombo1 = new VRS.Library.UserControl.TBLCombo();
			this.lblTBL = new System.Windows.Forms.Label();
			this.txtTextToFind = new VRS.UI.Controls.WEditBox();
			this.lblQuality = new System.Windows.Forms.Label();
			this.lblTextToFind = new System.Windows.Forms.Label();
			this.trackBarQuality = new System.Windows.Forms.TrackBar();
			this.lblHuge = new System.Windows.Forms.Label();
			this.lblSmall = new System.Windows.Forms.Label();
			this.trackBarTaille = new System.Windows.Forms.TrackBar();
			this.htbMin = new VRS.Library.UserControl.HexaTextBox();
			this.toolTipTrackBar = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.HexEdit)).BeginInit();
			this.panelParam.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tblCombo1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTextToFind)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarTaille)).BeginInit();
			this.SuspendLayout();
			// 
			// HexEdit
			// 
			this.HexEdit.Enabled = true;
			this.HexEdit.Location = new System.Drawing.Point(328, 280);
			this.HexEdit.Name = "HexEdit";
			this.HexEdit.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("HexEdit.OcxState")));
			this.HexEdit.Size = new System.Drawing.Size(288, 104);
			this.HexEdit.TabIndex = 3;
			this.HexEdit.TabStop = false;
			this.HexEdit.Visible = false;
			// 
			// lvFind
			// 
			this.lvFind.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvFind.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					 this.colHeadPos,
																					 this.colHeadValue});
			this.lvFind.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvFind.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lvFind.FullRowSelect = true;
			this.lvFind.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvFind.HideSelection = false;
			this.lvFind.Location = new System.Drawing.Point(0, 184);
			this.lvFind.MultiSelect = false;
			this.lvFind.Name = "lvFind";
			this.lvFind.Size = new System.Drawing.Size(776, 104);
			this.lvFind.TabIndex = 2;
			this.lvFind.View = System.Windows.Forms.View.Details;
			// 
			// colHeadPos
			// 
			this.colHeadPos.Text = "POS";
			this.colHeadPos.Width = 87;
			// 
			// colHeadValue
			// 
			this.colHeadValue.Text = "VALUE";
			// 
			// tbButtonFind
			// 
			this.tbButtonFind.ImageIndex = 0;
			this.tbButtonFind.Tag = "Search";
			// 
			// toolBarFind
			// 
			this.toolBarFind.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBarFind.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						   this.tbButtonFind,
																						   this.toolBarButton1,
																						   this.tblButtonStop});
			this.toolBarFind.ButtonSize = new System.Drawing.Size(23, 22);
			this.toolBarFind.Divider = false;
			this.toolBarFind.DropDownArrows = true;
			this.toolBarFind.ImageList = this.ListImageToolBar;
			this.toolBarFind.Location = new System.Drawing.Point(0, 0);
			this.toolBarFind.Name = "toolBarFind";
			this.toolBarFind.ShowToolTips = true;
			this.toolBarFind.Size = new System.Drawing.Size(776, 26);
			this.toolBarFind.TabIndex = 1;
			this.toolBarFind.Wrappable = false;
			this.toolBarFind.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarFind_ButtonClick);
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tblButtonStop
			// 
			this.tblButtonStop.Enabled = false;
			this.tblButtonStop.ImageIndex = 1;
			this.tblButtonStop.Tag = "StopSearch";
			// 
			// ListImageToolBar
			// 
			this.ListImageToolBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.ListImageToolBar.ImageSize = new System.Drawing.Size(16, 16);
			this.ListImageToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListImageToolBar.ImageStream")));
			this.ListImageToolBar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pb
			// 
			this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pb.Location = new System.Drawing.Point(536, 3);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(232, 20);
			this.pb.TabIndex = 4;
			// 
			// GradLabelParam
			// 
			this.GradLabelParam.CouleurDroite = System.Drawing.SystemColors.Control;
			this.GradLabelParam.CouleurGauche = System.Drawing.SystemColors.InactiveCaptionText;
			this.GradLabelParam.Dock = System.Windows.Forms.DockStyle.Top;
			this.GradLabelParam.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GradLabelParam.Location = new System.Drawing.Point(0, 26);
			this.GradLabelParam.Name = "GradLabelParam";
			this.GradLabelParam.Size = new System.Drawing.Size(776, 16);
			this.GradLabelParam.TabIndex = 17;
			this.GradLabelParam.Text = "PARAM";
			this.GradLabelParam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panelParam
			// 
			this.panelParam.AutoScroll = true;
			this.panelParam.Controls.Add(this.htbMax);
			this.panelParam.Controls.Add(this.lblRange);
			this.panelParam.Controls.Add(this.rngbar);
			this.panelParam.Controls.Add(this.lblMatchCase);
			this.panelParam.Controls.Add(this.lblMinLen);
			this.panelParam.Controls.Add(this.lblBest);
			this.panelParam.Controls.Add(this.lblLow);
			this.panelParam.Controls.Add(this.tblCombo1);
			this.panelParam.Controls.Add(this.lblTBL);
			this.panelParam.Controls.Add(this.txtTextToFind);
			this.panelParam.Controls.Add(this.lblQuality);
			this.panelParam.Controls.Add(this.lblTextToFind);
			this.panelParam.Controls.Add(this.trackBarQuality);
			this.panelParam.Controls.Add(this.lblHuge);
			this.panelParam.Controls.Add(this.lblSmall);
			this.panelParam.Controls.Add(this.trackBarTaille);
			this.panelParam.Controls.Add(this.htbMin);
			this.panelParam.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelParam.Location = new System.Drawing.Point(0, 42);
			this.panelParam.Name = "panelParam";
			this.panelParam.Size = new System.Drawing.Size(776, 142);
			this.panelParam.TabIndex = 18;
			// 
			// htbMax
			// 
			this.htbMax.DecimalValue = ((long)(0));
			this.htbMax.Enabled = false;
			this.htbMax.HexaValue = "0";
			this.htbMax.Location = new System.Drawing.Point(592, 56);
			this.htbMax.Minimum = ((long)(0));
			this.htbMax.Name = "htbMax";
			this.htbMax.Size = new System.Drawing.Size(96, 20);
			this.htbMax.TabIndex = 32;
			// 
			// lblRange
			// 
			this.lblRange.BackColor = System.Drawing.SystemColors.Control;
			this.lblRange.Enabled = false;
			this.lblRange.Location = new System.Drawing.Point(8, 56);
			this.lblRange.Name = "lblRange";
			this.lblRange.Size = new System.Drawing.Size(136, 20);
			this.lblRange.TabIndex = 30;
			this.lblRange.Text = "SEARCHINTERVAL";
			this.lblRange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// rngbar
			// 
			this.rngbar.DivisionNum = 10;
			this.rngbar.Enabled = false;
			this.rngbar.HeightOfBar = 10;
			this.rngbar.HeightOfMark = 24;
			this.rngbar.HeightOfTick = 3;
			this.rngbar.InnerColor = System.Drawing.Color.Blue;
			this.rngbar.Location = new System.Drawing.Point(152, 54);
			this.rngbar.Name = "rngbar";
			this.rngbar.Orientation = VRS.Library.UserControl.RangeBar.RangeBarOrientation.horizontal;
			this.rngbar.RangeMaximum = 5;
			this.rngbar.RangeMinimum = 3;
			this.rngbar.ScaleOrientation = VRS.Library.UserControl.RangeBar.TopBottomOrientation.bottom;
			this.rngbar.Size = new System.Drawing.Size(344, 24);
			this.rngbar.TabIndex = 19;
			this.rngbar.TotalMaximum = 10;
			this.rngbar.TotalMinimum = 0;
			this.rngbar.RangeChanging += new VRS.Library.UserControl.RangeBar.RangeChangedEventHandler(this.rngbar_RangeChanging);
			this.rngbar.RangeChanged += new VRS.Library.UserControl.RangeBar.RangeChangedEventHandler(this.rngbar_RangeChanged);
			// 
			// lblMatchCase
			// 
			this.lblMatchCase.BackColor = System.Drawing.SystemColors.Control;
			this.lblMatchCase.Location = new System.Drawing.Point(408, 8);
			this.lblMatchCase.Name = "lblMatchCase";
			this.lblMatchCase.Size = new System.Drawing.Size(128, 20);
			this.lblMatchCase.TabIndex = 29;
			this.lblMatchCase.Text = "MATCHCASE";
			this.lblMatchCase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMinLen
			// 
			this.lblMinLen.BackColor = System.Drawing.SystemColors.Control;
			this.lblMinLen.Location = new System.Drawing.Point(8, 104);
			this.lblMinLen.Name = "lblMinLen";
			this.lblMinLen.Size = new System.Drawing.Size(136, 20);
			this.lblMinLen.TabIndex = 26;
			this.lblMinLen.Text = "MINLENGHT";
			this.lblMinLen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBest
			// 
			this.lblBest.BackColor = System.Drawing.SystemColors.Control;
			this.lblBest.Location = new System.Drawing.Point(504, 80);
			this.lblBest.Name = "lblBest";
			this.lblBest.Size = new System.Drawing.Size(72, 20);
			this.lblBest.TabIndex = 24;
			this.lblBest.Text = "BEST";
			this.lblBest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLow
			// 
			this.lblLow.BackColor = System.Drawing.SystemColors.Control;
			this.lblLow.Location = new System.Drawing.Point(152, 80);
			this.lblLow.Name = "lblLow";
			this.lblLow.Size = new System.Drawing.Size(56, 20);
			this.lblLow.TabIndex = 23;
			this.lblLow.Text = "LOW";
			this.lblLow.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// tblCombo1
			// 
			this.tblCombo1.AcceptsPlussKey = true;
			this.tblCombo1.BackColor = System.Drawing.Color.White;
			this.tblCombo1.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("tblCombo1.ButtonIcon")));
			this.tblCombo1.ButtonWidth = 15;
			this.tblCombo1.DropDownWidth = 248;
			this.tblCombo1.Location = new System.Drawing.Point(152, 32);
			this.tblCombo1.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.tblCombo1.MaxLength = 32767;
			this.tblCombo1.Name = "tblCombo1";
			this.tblCombo1.ReadOnly = true;
			this.tblCombo1.SelectedIndex = -1;
			this.tblCombo1.Size = new System.Drawing.Size(248, 20);
			this.tblCombo1.TabIndex = 22;
			this.tblCombo1.UseStaticViewStyle = false;
			this.tblCombo1.ViewStyle.BarBorderColor = System.Drawing.Color.DarkGray;
			this.tblCombo1.ViewStyle.BarClientAreaColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(128)));
			this.tblCombo1.ViewStyle.BarColor = System.Drawing.SystemColors.Control;
			this.tblCombo1.ViewStyle.BarHotBorderColor = System.Drawing.Color.Black;
			this.tblCombo1.ViewStyle.BarHotColor = System.Drawing.Color.FromArgb(((System.Byte)(182)), ((System.Byte)(193)), ((System.Byte)(214)));
			this.tblCombo1.ViewStyle.BarHotTextColor = System.Drawing.Color.Black;
			this.tblCombo1.ViewStyle.BarItemBorderHotColor = System.Drawing.Color.Black;
			this.tblCombo1.ViewStyle.BarItemHotColor = System.Drawing.Color.FromArgb(((System.Byte)(182)), ((System.Byte)(193)), ((System.Byte)(214)));
			this.tblCombo1.ViewStyle.BarItemHotTextColor = System.Drawing.Color.White;
			this.tblCombo1.ViewStyle.BarItemPressedColor = System.Drawing.Color.FromArgb(((System.Byte)(210)), ((System.Byte)(218)), ((System.Byte)(232)));
			this.tblCombo1.ViewStyle.BarItemSelectedColor = System.Drawing.Color.Silver;
			this.tblCombo1.ViewStyle.BarItemSelectedTextColor = System.Drawing.Color.White;
			this.tblCombo1.ViewStyle.BarItemsStyle = VRS.UI.Controls.WOutlookBar.ItemsStyle.IconSelect;
			this.tblCombo1.ViewStyle.BarItemTextColor = System.Drawing.Color.White;
			this.tblCombo1.ViewStyle.BarPressedColor = System.Drawing.Color.FromArgb(((System.Byte)(210)), ((System.Byte)(218)), ((System.Byte)(232)));
			this.tblCombo1.ViewStyle.BarTextColor = System.Drawing.Color.Black;
			this.tblCombo1.ViewStyle.BorderColor = System.Drawing.Color.DarkGray;
			this.tblCombo1.ViewStyle.BorderHotColor = System.Drawing.Color.Black;
			this.tblCombo1.ViewStyle.ButtonColor = System.Drawing.SystemColors.Control;
			this.tblCombo1.ViewStyle.ButtonHotColor = System.Drawing.Color.FromArgb(((System.Byte)(182)), ((System.Byte)(193)), ((System.Byte)(214)));
			this.tblCombo1.ViewStyle.ButtonPressedColor = System.Drawing.Color.FromArgb(((System.Byte)(210)), ((System.Byte)(218)), ((System.Byte)(232)));
			this.tblCombo1.ViewStyle.ControlBackColor = System.Drawing.SystemColors.Control;
			this.tblCombo1.ViewStyle.EditColor = System.Drawing.Color.White;
			this.tblCombo1.ViewStyle.EditDisabledColor = System.Drawing.Color.Gainsboro;
			this.tblCombo1.ViewStyle.EditFocusedColor = System.Drawing.Color.Beige;
			this.tblCombo1.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			this.tblCombo1.ViewStyle.FlashColor = System.Drawing.Color.Pink;
			this.tblCombo1.ViewStyle.Text3DColor = System.Drawing.Color.White;
			this.tblCombo1.ViewStyle.TextColor = System.Drawing.Color.Black;
			this.tblCombo1.ViewStyle.TextShadowColor = System.Drawing.Color.DarkGray;
			this.tblCombo1.VisibleItems = 10;
			// 
			// lblTBL
			// 
			this.lblTBL.BackColor = System.Drawing.SystemColors.Control;
			this.lblTBL.Location = new System.Drawing.Point(8, 32);
			this.lblTBL.Name = "lblTBL";
			this.lblTBL.Size = new System.Drawing.Size(136, 20);
			this.lblTBL.TabIndex = 21;
			this.lblTBL.Text = "TBL";
			this.lblTBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtTextToFind
			// 
			this.txtTextToFind.DecimalPlaces = 2;
			this.txtTextToFind.DecMaxValue = 999999999;
			this.txtTextToFind.DecMinValue = -999999999;
			this.txtTextToFind.Location = new System.Drawing.Point(152, 8);
			this.txtTextToFind.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
			this.txtTextToFind.MaxLength = 32767;
			this.txtTextToFind.Multiline = false;
			this.txtTextToFind.Name = "txtTextToFind";
			this.txtTextToFind.PasswordChar = '\0';
			this.txtTextToFind.ReadOnly = false;
			this.txtTextToFind.Size = new System.Drawing.Size(248, 20);
			this.txtTextToFind.TabIndex = 20;
			this.txtTextToFind.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.txtTextToFind.UseStaticViewStyle = false;
			this.txtTextToFind.ViewStyle.BarBorderColor = System.Drawing.Color.DarkGray;
			this.txtTextToFind.ViewStyle.BarClientAreaColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(128)));
			this.txtTextToFind.ViewStyle.BarColor = System.Drawing.SystemColors.Control;
			this.txtTextToFind.ViewStyle.BarHotBorderColor = System.Drawing.Color.Black;
			this.txtTextToFind.ViewStyle.BarHotColor = System.Drawing.Color.FromArgb(((System.Byte)(182)), ((System.Byte)(193)), ((System.Byte)(214)));
			this.txtTextToFind.ViewStyle.BarHotTextColor = System.Drawing.Color.Black;
			this.txtTextToFind.ViewStyle.BarItemBorderHotColor = System.Drawing.Color.Black;
			this.txtTextToFind.ViewStyle.BarItemHotColor = System.Drawing.Color.FromArgb(((System.Byte)(182)), ((System.Byte)(193)), ((System.Byte)(214)));
			this.txtTextToFind.ViewStyle.BarItemHotTextColor = System.Drawing.Color.White;
			this.txtTextToFind.ViewStyle.BarItemPressedColor = System.Drawing.Color.FromArgb(((System.Byte)(210)), ((System.Byte)(218)), ((System.Byte)(232)));
			this.txtTextToFind.ViewStyle.BarItemSelectedColor = System.Drawing.Color.Silver;
			this.txtTextToFind.ViewStyle.BarItemSelectedTextColor = System.Drawing.Color.White;
			this.txtTextToFind.ViewStyle.BarItemsStyle = VRS.UI.Controls.WOutlookBar.ItemsStyle.IconSelect;
			this.txtTextToFind.ViewStyle.BarItemTextColor = System.Drawing.Color.White;
			this.txtTextToFind.ViewStyle.BarPressedColor = System.Drawing.Color.FromArgb(((System.Byte)(210)), ((System.Byte)(218)), ((System.Byte)(232)));
			this.txtTextToFind.ViewStyle.BarTextColor = System.Drawing.Color.Black;
			this.txtTextToFind.ViewStyle.BorderColor = System.Drawing.Color.DarkGray;
			this.txtTextToFind.ViewStyle.BorderHotColor = System.Drawing.Color.Black;
			this.txtTextToFind.ViewStyle.ButtonColor = System.Drawing.SystemColors.Control;
			this.txtTextToFind.ViewStyle.ButtonHotColor = System.Drawing.Color.FromArgb(((System.Byte)(182)), ((System.Byte)(193)), ((System.Byte)(214)));
			this.txtTextToFind.ViewStyle.ButtonPressedColor = System.Drawing.Color.FromArgb(((System.Byte)(210)), ((System.Byte)(218)), ((System.Byte)(232)));
			this.txtTextToFind.ViewStyle.ControlBackColor = System.Drawing.SystemColors.Control;
			this.txtTextToFind.ViewStyle.EditColor = System.Drawing.Color.White;
			this.txtTextToFind.ViewStyle.EditDisabledColor = System.Drawing.Color.Gainsboro;
			this.txtTextToFind.ViewStyle.EditFocusedColor = System.Drawing.Color.Beige;
			this.txtTextToFind.ViewStyle.EditReadOnlyColor = System.Drawing.Color.White;
			this.txtTextToFind.ViewStyle.FlashColor = System.Drawing.Color.Pink;
			this.txtTextToFind.ViewStyle.Text3DColor = System.Drawing.Color.White;
			this.txtTextToFind.ViewStyle.TextColor = System.Drawing.Color.Black;
			this.txtTextToFind.ViewStyle.TextShadowColor = System.Drawing.Color.DarkGray;
			this.txtTextToFind.EnterKeyPressed += new System.EventHandler(this.txtTextToFind_EnterKeyPressed);
			// 
			// lblQuality
			// 
			this.lblQuality.BackColor = System.Drawing.SystemColors.Control;
			this.lblQuality.Location = new System.Drawing.Point(8, 80);
			this.lblQuality.Name = "lblQuality";
			this.lblQuality.Size = new System.Drawing.Size(136, 20);
			this.lblQuality.TabIndex = 18;
			this.lblQuality.Text = "QUALITY";
			this.lblQuality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTextToFind
			// 
			this.lblTextToFind.BackColor = System.Drawing.SystemColors.Control;
			this.lblTextToFind.Location = new System.Drawing.Point(8, 8);
			this.lblTextToFind.Name = "lblTextToFind";
			this.lblTextToFind.Size = new System.Drawing.Size(136, 20);
			this.lblTextToFind.TabIndex = 17;
			this.lblTextToFind.Text = "TEXTTOFIND";
			this.lblTextToFind.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// trackBarQuality
			// 
			this.trackBarQuality.AutoSize = false;
			this.trackBarQuality.Location = new System.Drawing.Point(208, 78);
			this.trackBarQuality.Minimum = 2;
			this.trackBarQuality.Name = "trackBarQuality";
			this.trackBarQuality.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.trackBarQuality.Size = new System.Drawing.Size(296, 24);
			this.trackBarQuality.TabIndex = 7;
			this.trackBarQuality.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarQuality.Value = 2;
			// 
			// lblHuge
			// 
			this.lblHuge.BackColor = System.Drawing.SystemColors.Control;
			this.lblHuge.Location = new System.Drawing.Point(504, 104);
			this.lblHuge.Name = "lblHuge";
			this.lblHuge.Size = new System.Drawing.Size(72, 20);
			this.lblHuge.TabIndex = 28;
			this.lblHuge.Text = "HUGE";
			this.lblHuge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSmall
			// 
			this.lblSmall.BackColor = System.Drawing.SystemColors.Control;
			this.lblSmall.Location = new System.Drawing.Point(152, 104);
			this.lblSmall.Name = "lblSmall";
			this.lblSmall.Size = new System.Drawing.Size(56, 20);
			this.lblSmall.TabIndex = 27;
			this.lblSmall.Text = "SMALL";
			this.lblSmall.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// trackBarTaille
			// 
			this.trackBarTaille.AutoSize = false;
			this.trackBarTaille.Location = new System.Drawing.Point(208, 102);
			this.trackBarTaille.Maximum = 100;
			this.trackBarTaille.Minimum = 3;
			this.trackBarTaille.Name = "trackBarTaille";
			this.trackBarTaille.Size = new System.Drawing.Size(296, 24);
			this.trackBarTaille.TabIndex = 25;
			this.trackBarTaille.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarTaille.Value = 3;
			this.trackBarTaille.Scroll += new System.EventHandler(this.trackBarTaille_Scroll);
			// 
			// htbMin
			// 
			this.htbMin.DecimalValue = ((long)(0));
			this.htbMin.Enabled = false;
			this.htbMin.HexaValue = "0";
			this.htbMin.Location = new System.Drawing.Point(504, 56);
			this.htbMin.Minimum = ((long)(0));
			this.htbMin.Name = "htbMin";
			this.htbMin.Size = new System.Drawing.Size(88, 20);
			this.htbMin.TabIndex = 31;
			// 
			// toolTipTrackBar
			// 
			this.toolTipTrackBar.AutoPopDelay = 500;
			this.toolTipTrackBar.InitialDelay = 500;
			this.toolTipTrackBar.ReshowDelay = 100;
			// 
			// StringFinder
			// 
			this.Controls.Add(this.lvFind);
			this.Controls.Add(this.panelParam);
			this.Controls.Add(this.GradLabelParam);
			this.Controls.Add(this.pb);
			this.Controls.Add(this.HexEdit);
			this.Controls.Add(this.toolBarFind);
			this.Name = "StringFinder";
			this.Size = new System.Drawing.Size(776, 288);
			this.Resize += new System.EventHandler(this.StringFinder_Resize);
			((System.ComponentModel.ISupportInitialize)(this.HexEdit)).EndInit();
			this.panelParam.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tblCombo1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTextToFind)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarQuality)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarTaille)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ExtractString(int MinTaille, long StartPos, long StopPos, short Performance, string FindString){
			if (File.Exists(this._FileName)){
				if (File.Exists(tblCombo1.SelectedItem.Tag.ToString())){
					//Vide la liste de rechercher
					lvFind.Items.Clear();

					//Ouverture du fichier
					FileInfo info = new FileInfo(this._FileName); 
					HexEdit.Load(ref this._FileName, (int)info.Length);

					TBLStream tbl = new TBLStream(tblCombo1.SelectedItem.Tag.ToString());
					tbl.Load();

					int ChunkSize = Performance * 10000;
					long DataLen = StopPos - StartPos;

					if (DataLen > 0){
						int i = (int)StartPos;
						pb.Maximum = (int)DataLen;
 
						string buffer = "";
						string[] bufferSplittedHexa;
						string dte = "";
						string str = "";
						long StrPos = 0;

						//Commancement de la recherche des chaine
						do{
							if (this._Cancel) break;

							buffer = HexEdit.GetDataChunk(i);
							bufferSplittedHexa = Convertir.StringHexa_LittleEndian(buffer, true).Trim().Split(new char[]{' '}); 							
							for(int j =0; j< bufferSplittedHexa.Length; j++) {								
								dte = tbl.FindTBLMatch(bufferSplittedHexa[j]);
								if (dte != "#") 
									str += dte;
								else{
									if (str.IndexOf(FindString, 0) != -1){
										if (str.Length >= MinTaille){
											ListViewItem item = new ListViewItem(Convertir.DecimalToHexa(StrPos));
											item.SubItems.Add(str);
											lvFind.Items.Add(item);											
										}
									}
                                    str = "";
									StrPos = i + j + 1;									
								}
								
								if ((i + j) > DataLen) 
									break;
								
								if (j % 10000 == 0)									
									pb.Value = i + j;					
							}
							//Assignation
							i += ChunkSize;
						}
						while(i < DataLen);

						//Reinitialise a 0
						this.pb.Value = 0;
						this._Cancel = false;
						tbButtonFind.Enabled = true;
						tblButtonStop.Enabled = false; 
						panelParam.Enabled = true;

                        if (lvFind.Items.Count ==0)
							MessageBox.Show(this, this._String_NoItemFound,
								"Message",
								MessageBoxButtons.OK,
								MessageBoxIcon.Information);  
						else
							MessageBox.Show(this, lvFind.Items.Count + " " + this._String_TotalItem,
								"Message",
								MessageBoxButtons.OK,
								MessageBoxIcon.Information);  
						
					} // Datalen > 0
				} // TBL
			} //Filename
		}

		private void StringFinder_Resize(object sender, System.EventArgs e) {
			colHeadValue.Width = lvFind.Width - colHeadPos.Width - 30; 
		}

		private void StartFind(){
			ExtractString(trackBarTaille.Value, htbMin.DecimalValue, htbMax.DecimalValue, (short)trackBarQuality.Value, txtTextToFind.Text ); 
		}

		private void toolBarFind_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			switch (e.Button.Tag.ToString()){
				case "Search":
					LaunchSearch();
					break;
				case "StopSearch":
					tblButtonStop.Enabled = false; 
					tbButtonFind.Enabled = true;
					this._Cancel = true;
					break;
			}
		}
        
		private void rngbar_RangeChanging(object sender, System.EventArgs e) {
			this.htbMin.DecimalValue = this.rngbar.RangeMinimum;		
			this.htbMax.DecimalValue = this.rngbar.RangeMaximum;
		}

		private void rngbar_RangeChanged(object sender, System.EventArgs e) {
			this.htbMin.DecimalValue = this.rngbar.RangeMinimum;		
			this.htbMax.DecimalValue = this.rngbar.RangeMaximum;
		}

		private void trackBarTaille_Scroll(object sender, System.EventArgs e) {
			this.toolTipTrackBar.SetToolTip(this.trackBarTaille, trackBarTaille.Value.ToString());
		}

		private void LaunchSearch(){
			this._Cancel = false;
			panelParam.Enabled = false;
			tblButtonStop.Enabled = true; 
			tbButtonFind.Enabled = false;

			//Ajustement de la taille minimum
			try{
				if (trackBarTaille.Value > txtTextToFind.Text.Length)
					trackBarTaille.Value = txtTextToFind.Text.Length;
			}catch{
				trackBarTaille.Value = trackBarTaille.Minimum;
			}
 
			//Demarage du nouveau thread
			ThreadStart myThreadStart = new ThreadStart(this.StartFind);  
			new Thread(myThreadStart).Start(); 					
		}

		private void txtTextToFind_EnterKeyPressed(object sender, System.EventArgs e) {
			LaunchSearch();
		}

		public string FileName{
			get{
				return this._FileName; 
			}
			set{
				this._FileName = value;
				FileInfo fi = new FileInfo(this._FileName);				
				rngbar.TotalMaximum = (int)fi.Length;
				rngbar.TotalMinimum = 0;
				rngbar.SelectRange(0, (int)fi.Length);
				htbMax.DecimalValue = (int)fi.Length;
			}
		}

		public Project Projet{
			set{				
				tblCombo1.Projet = value;
			}
		}

		#region Strings pour la localisation
		[Category("Strings")]
		[DefaultValue("Texte à Rechercher")] 
		public string String_TextToFind{
			get{
				return lblTextToFind.Text; 
			}
			set{
				lblTextToFind.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Position")] 
		public string String_ColPosition{
			get{
				return colHeadPos.Text; 
			}
			set{
				colHeadPos.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Param")] 
		public string String_GradLabelParam{
			get{
				return GradLabelParam.Text; 
			}
			set{
				GradLabelParam.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Valeur")] 
		public string String_ColValue{
			get{
				return colHeadValue.Text; 
			}
			set{
				colHeadValue.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Aucune chaîne n'a été trouvé en ce qui correspond à votre recherche.")] 
		public string String_NoItemFound{
			get{
				return this._String_NoItemFound; 
			}
			set{
				this._String_NoItemFound = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Chaînes trouvés")] 
		public string String_TotalItem{
			get{
				return this._String_TotalItem; 
			}
			set{
				this._String_TotalItem = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Tables de jeux")] 
		public string String_TBL{
			get{
				return this.lblTBL.Text; 
			}
			set{
				this.lblTBL.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Qualité de la recherche")] 
		public string String_Quality{
			get{
				return this.lblQuality.Text; 
			}
			set{
				this.lblQuality.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Taille Minimum")] 
		public string String_MinLenght{
			get{
				return this.lblMinLen.Text; 
			}
			set{
				this.lblMinLen.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("(Sensible à la case)")] 
		public string String_MatchCase{
			get{
				return this.lblMatchCase.Text; 
			}
			set{
				this.lblMatchCase.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Interval de recherche")] 
		public string String_Range{
			get{
				return this.lblRange.Text; 
			}
			set{
				this.lblRange.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Meuilleur")] 
		public string String_Best{
			get{
				return this.lblBest.Text; 
			}
			set{
				this.lblBest.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Basse")] 
		public string String_Low{
			get{
				return this.lblLow.Text; 
			}
			set{
				this.lblLow.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Petite")] 
		public string String_Small{
			get{
				return this.lblSmall.Text; 
			}
			set{
				this.lblSmall.Text = value;
			}
		}

		[Category("Strings")]
		[DefaultValue("Grande")] 
		public string String_Huge{
			get{
				return this.lblHuge.Text; 
			}
			set{
				this.lblHuge.Text = value;
			}
		}
		#endregion
	}
}
