using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

using VRS.Library.Convertion;
using VRS.Library.IO;
using VRS.Library.Table.TBL;
using VRS.Library.Table;

namespace VRS.Library.UserControl {
	/// <summary>
	/// Description résumée de HexaFileShow.
	/// </summary>
	public class HexaFileShow : System.Windows.Forms.UserControl {
		public event MouseEventHandler RightClick;
		public event EventHandler ExplorerClick;

		/// <summary>
		/// Nom du fichier chargé
		/// </summary>
		private string _FileName;
        
		/// <summary>
		/// Position courrante
		/// </summary>
		private string _CurrentPosition = "0";

		/// <summary>
		/// Largeur des colones
		/// </summary>
		private int _ColumnSize = 16;

		/// <summary>
		/// Total de ligne a l'ecran
		/// </summary>
		private long _TotalLine = 0;

		/// <summary>
		/// Afficher les caractere special
		/// </summary>
		private bool _ShowSpecial = true;

		/// <summary>
		/// Afficher les MTE
		/// </summary>
		private bool _ShowMTE = true;

        /// <summary>
        /// Afficher les Couleur
        /// </summary>
        private bool _ShowColor = true;

        /// <summary>
        /// Mode de positionement Decimale
        /// </summary>
        private bool _DecimalMode = false;

		/// <summary>
		/// Afficher les MTE
		/// </summary>
		private bool _NotShowDTE = false;

		/// <summary>
		/// Taile du fichier chargé
		/// </summary>
		private long _FileLen = 0;

		/// <summary>
		/// Un fichier est charger dans le control
		/// </summary>
		private bool _isLoaded = false;

		/// <summary>
		/// Table de jeux TBL qui sera charger dans le projet
		/// </summary>
		private TBLStream _TBL = new TBLStream();

		/// <summary>
		/// Si true : RightClick sur picData
		/// Si False: RightClick sur PicDataDecoded
		/// </summary>
		public bool _PicDataRightClick = false;

		/// <summary>
		/// Le control utilise la TBL charger ou non
		/// </summary>
		private bool _isUseTBL = false;
		private System.Windows.Forms.ImageList ListImageToolBar;
        private VRS.Library.UserControl.HexaTextBox htbPosition;
		private System.Windows.Forms.ToolBarButton toolBarButtonRefresh;
		private System.Windows.Forms.ToolBarButton toolBarButtonSep;
		private System.Windows.Forms.ToolBarButton toolBarShowAscii;
		private System.Windows.Forms.ToolBarButton toolBarButtonShowSpecial;
		private System.Windows.Forms.VScrollBar sbValue;
		private System.Windows.Forms.ToolBarButton toolBarButtonSep2;
		private VRS.Library.UserControl.TBLCombo tblCombo1;
		private System.Windows.Forms.ToolBarButton toolBarButtonShowMTE;
		public VRS.Library.UserControl.SelectableBox picDecodedData;
		private VRS.Library.UserControl.SelectableBox picAdresse;
		public VRS.Library.UserControl.SelectableBox picData;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.NumericUpDown upDownCol;
		private System.Windows.Forms.ToolBarButton toolBarButtonNotShowDTE;
		private System.Windows.Forms.ToolBarButton toolBarButtonSep1;
        private ToolBar tbFileShow;
        private ToolBarButton toolBarButtonShowColor;
        private ToolBarButton toolBarButtonDecHexa;
        private DecimalTextBox dtbPosition;
		private System.ComponentModel.IContainer components;

		public HexaFileShow() {
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			htbPosition.Height = 23;			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HexaFileShow));
            this.ListImageToolBar = new System.Windows.Forms.ImageList(this.components);
            this.sbValue = new System.Windows.Forms.VScrollBar();
            this.tbFileShow = new System.Windows.Forms.ToolBar();
            this.toolBarButtonRefresh = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonDecHexa = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSep = new System.Windows.Forms.ToolBarButton();
            this.toolBarShowAscii = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSep1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonShowColor = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonShowMTE = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonNotShowDTE = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonShowSpecial = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSep2 = new System.Windows.Forms.ToolBarButton();
            this.upDownCol = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dtbPosition = new VRS.Library.UserControl.DecimalTextBox();
            this.picData = new VRS.Library.UserControl.SelectableBox();
            this.picAdresse = new VRS.Library.UserControl.SelectableBox();
            this.picDecodedData = new VRS.Library.UserControl.SelectableBox();
            this.tblCombo1 = new VRS.Library.UserControl.TBLCombo();
            this.htbPosition = new VRS.Library.UserControl.HexaTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCombo1)).BeginInit();
            this.SuspendLayout();
            // 
            // ListImageToolBar
            // 
            this.ListImageToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListImageToolBar.ImageStream")));
            this.ListImageToolBar.TransparentColor = System.Drawing.Color.Transparent;
            this.ListImageToolBar.Images.SetKeyName(0, "");
            this.ListImageToolBar.Images.SetKeyName(1, "");
            this.ListImageToolBar.Images.SetKeyName(2, "");
            this.ListImageToolBar.Images.SetKeyName(3, "");
            this.ListImageToolBar.Images.SetKeyName(4, "");
            this.ListImageToolBar.Images.SetKeyName(5, "");
            this.ListImageToolBar.Images.SetKeyName(6, "color.ico");
            this.ListImageToolBar.Images.SetKeyName(7, "0x.ico");
            // 
            // sbValue
            // 
            this.sbValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.sbValue.LargeChange = 64;
            this.sbValue.Location = new System.Drawing.Point(633, 26);
            this.sbValue.Name = "sbValue";
            this.sbValue.Size = new System.Drawing.Size(17, 173);
            this.sbValue.SmallChange = 16;
            this.sbValue.TabIndex = 8;
            this.sbValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbValue_Scroll);
            // 
            // tbFileShow
            // 
            this.tbFileShow.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tbFileShow.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonRefresh,
            this.toolBarButtonDecHexa,
            this.toolBarButtonSep,
            this.toolBarShowAscii,
            this.toolBarButtonSep1,
            this.toolBarButtonShowColor,
            this.toolBarButtonShowMTE,
            this.toolBarButtonNotShowDTE,
            this.toolBarButtonShowSpecial,
            this.toolBarButtonSep2});
            this.tbFileShow.ButtonSize = new System.Drawing.Size(23, 22);
            this.tbFileShow.Divider = false;
            this.tbFileShow.DropDownArrows = true;
            this.tbFileShow.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFileShow.ImageList = this.ListImageToolBar;
            this.tbFileShow.Location = new System.Drawing.Point(0, 0);
            this.tbFileShow.Name = "tbFileShow";
            this.tbFileShow.ShowToolTips = true;
            this.tbFileShow.Size = new System.Drawing.Size(650, 26);
            this.tbFileShow.TabIndex = 4;
            this.tbFileShow.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbFileShow_ButtonClick);
            // 
            // toolBarButtonRefresh
            // 
            this.toolBarButtonRefresh.ImageIndex = 0;
            this.toolBarButtonRefresh.Name = "toolBarButtonRefresh";
            this.toolBarButtonRefresh.Tag = "Refresh";
            // 
            // toolBarButtonDecHexa
            // 
            this.toolBarButtonDecHexa.ImageIndex = 7;
            this.toolBarButtonDecHexa.Name = "toolBarButtonDecHexa";
            this.toolBarButtonDecHexa.Pushed = true;
            this.toolBarButtonDecHexa.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.toolBarButtonDecHexa.Tag = "DecimalHexa";
            // 
            // toolBarButtonSep
            // 
            this.toolBarButtonSep.Name = "toolBarButtonSep";
            this.toolBarButtonSep.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarShowAscii
            // 
            this.toolBarShowAscii.ImageIndex = 3;
            this.toolBarShowAscii.Name = "toolBarShowAscii";
            this.toolBarShowAscii.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.toolBarShowAscii.Tag = "ShowAscii";
            // 
            // toolBarButtonSep1
            // 
            this.toolBarButtonSep1.Name = "toolBarButtonSep1";
            this.toolBarButtonSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonShowColor
            // 
            this.toolBarButtonShowColor.ImageIndex = 6;
            this.toolBarButtonShowColor.Name = "toolBarButtonShowColor";
            this.toolBarButtonShowColor.Pushed = true;
            this.toolBarButtonShowColor.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.toolBarButtonShowColor.Tag = "ShowColor";
            // 
            // toolBarButtonShowMTE
            // 
            this.toolBarButtonShowMTE.ImageIndex = 4;
            this.toolBarButtonShowMTE.Name = "toolBarButtonShowMTE";
            this.toolBarButtonShowMTE.Pushed = true;
            this.toolBarButtonShowMTE.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.toolBarButtonShowMTE.Tag = "ShowMTE";
            // 
            // toolBarButtonNotShowDTE
            // 
            this.toolBarButtonNotShowDTE.ImageIndex = 5;
            this.toolBarButtonNotShowDTE.Name = "toolBarButtonNotShowDTE";
            this.toolBarButtonNotShowDTE.Pushed = true;
            this.toolBarButtonNotShowDTE.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.toolBarButtonNotShowDTE.Tag = "NotShowDTE";
            // 
            // toolBarButtonShowSpecial
            // 
            this.toolBarButtonShowSpecial.ImageIndex = 2;
            this.toolBarButtonShowSpecial.Name = "toolBarButtonShowSpecial";
            this.toolBarButtonShowSpecial.Pushed = true;
            this.toolBarButtonShowSpecial.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.toolBarButtonShowSpecial.Tag = "ShowSpecial";
            // 
            // toolBarButtonSep2
            // 
            this.toolBarButtonSep2.Name = "toolBarButtonSep2";
            this.toolBarButtonSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // upDownCol
            // 
            this.upDownCol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.upDownCol.Location = new System.Drawing.Point(377, 1);
            this.upDownCol.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.upDownCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownCol.Name = "upDownCol";
            this.upDownCol.Size = new System.Drawing.Size(40, 22);
            this.upDownCol.TabIndex = 22;
            this.upDownCol.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.upDownCol.Enter += new System.EventHandler(this.upDownCol_Enter);
            this.upDownCol.ValueChanged += new System.EventHandler(this.upDownCol_ValueChanged);
            this.upDownCol.Leave += new System.EventHandler(this.upDownCol_Leave);
            this.upDownCol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.upDownCol_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(369, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(4, 20);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // dtbPosition
            // 
            this.dtbPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtbPosition.Location = new System.Drawing.Point(532, 0);
            this.dtbPosition.Name = "dtbPosition";
            this.dtbPosition.Size = new System.Drawing.Size(115, 20);
            this.dtbPosition.TabIndex = 24;
            this.dtbPosition.Valeur = ((long)(0));
            this.dtbPosition.Visible = false;
            this.dtbPosition.TBKeyDown += new System.Windows.Forms.KeyEventHandler(this.dtbPosition_TBKeyDown);
            // 
            // picData
            // 
            this.picData.BackColor = System.Drawing.Color.White;
            this.picData.Dock = System.Windows.Forms.DockStyle.Left;
            this.picData.Location = new System.Drawing.Point(72, 26);
            this.picData.Name = "picData";
            this.picData.Size = new System.Drawing.Size(328, 173);
            this.picData.TabIndex = 19;
            this.picData.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Control_MouseWheel);
            this.picData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picData_MouseUp);
            this.picData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.picData_KeyDown);
            // 
            // picAdresse
            // 
            this.picAdresse.BackColor = System.Drawing.Color.Gray;
            this.picAdresse.Dock = System.Windows.Forms.DockStyle.Left;
            this.picAdresse.Location = new System.Drawing.Point(0, 26);
            this.picAdresse.Name = "picAdresse";
            this.picAdresse.Size = new System.Drawing.Size(72, 173);
            this.picAdresse.TabIndex = 18;
            this.picAdresse.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Control_MouseWheel);
            this.picAdresse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.picAdresse_KeyDown);
            // 
            // picDecodedData
            // 
            this.picDecodedData.BackColor = System.Drawing.Color.White;
            this.picDecodedData.Location = new System.Drawing.Point(416, 26);
            this.picDecodedData.Name = "picDecodedData";
            this.picDecodedData.Size = new System.Drawing.Size(200, 102);
            this.picDecodedData.TabIndex = 16;
            this.picDecodedData.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Control_MouseWheel);
            this.picDecodedData.Paint += new System.Windows.Forms.PaintEventHandler(this.picDecodedData_Paint);
            this.picDecodedData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picDecodedData_MouseUp);
            this.picDecodedData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.picDecodedData_KeyDown);
            // 
            // tblCombo1
            // 
            this.tblCombo1.AcceptsPlussKey = true;
            this.tblCombo1.BackColor = System.Drawing.Color.White;
            this.tblCombo1.ButtonIcon = ((System.Drawing.Icon)(resources.GetObject("tblCombo1.ButtonIcon")));
            this.tblCombo1.ButtonWidth = 15;
            this.tblCombo1.DropDownWidth = 180;
            this.tblCombo1.Location = new System.Drawing.Point(186, 1);
            this.tblCombo1.Mask = VRS.UI.Controls.WEditBox_Mask.Text;
            this.tblCombo1.MaxLength = 32767;
            this.tblCombo1.Name = "tblCombo1";
            this.tblCombo1.ReadOnly = true;
            this.tblCombo1.SelectedIndex = -1;
            this.tblCombo1.Size = new System.Drawing.Size(180, 22);
            this.tblCombo1.TabIndex = 15;
            this.tblCombo1.UseStaticViewStyle = false;
            this.tblCombo1.VisibleItems = 10;
            this.tblCombo1.SelectedIndexChanged += new System.EventHandler(this.tblCombo1_SelectedIndexChanged);
            // 
            // htbPosition
            // 
            this.htbPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.htbPosition.DecimalValue = ((long)(0));
            this.htbPosition.HexaValue = "0";
            this.htbPosition.Location = new System.Drawing.Point(532, 0);
            this.htbPosition.Minimum = ((long)(0));
            this.htbPosition.Name = "htbPosition";
            this.htbPosition.Size = new System.Drawing.Size(115, 20);
            this.htbPosition.TabIndex = 9;
            this.htbPosition.TBKeyDown += new System.Windows.Forms.KeyEventHandler(this.htbPosition_TBKeyDown);
            // 
            // HexaFileShow
            // 
            this.Controls.Add(this.dtbPosition);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.upDownCol);
            this.Controls.Add(this.picData);
            this.Controls.Add(this.picAdresse);
            this.Controls.Add(this.picDecodedData);
            this.Controls.Add(this.tblCombo1);
            this.Controls.Add(this.htbPosition);
            this.Controls.Add(this.sbValue);
            this.Controls.Add(this.tbFileShow);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "HexaFileShow";
            this.Size = new System.Drawing.Size(650, 199);
            this.Resize += new System.EventHandler(this.HexaFileShow_Resize);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HexaFileShow_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.upDownCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCombo1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Nom du fichier chargé
		/// </summary>
		public string FileName{
			get{
				return this._FileName; 
			}
		}

		public void AddTBL(string Name, string FileName){
			tblCombo1.AddTBL(Name, FileName); 

			this._TBL.FileName = FileName;
			this._TBL.Load();
			RefreshData();
		}

		/// <summary>
		/// Charger un fichier dans le control avec un Table de jeu (TBL)
		/// </summary>
		/// <param name="Filename">Chemin vers le fichier ROM(path)</param>
		/// <param name="TBLFileName">Chemin vers le fichier TBL(path)</param>
		public void LoadFile(string Filename, string TBLFileName){
			if (File.Exists(Filename)){
				FileInfo fi = new FileInfo(Filename);
				this._FileName = Filename; 
				this._isLoaded = true;
				sbValue.Maximum = (int)fi.Length;
				this._FileLen = fi.Length;

				if (File.Exists(TBLFileName)){
					this._TBL = new TBLStream(TBLFileName);
					this._TBL.Load();
					this._isUseTBL = true;
				}

				LoadPosition("0");
			}
		}

		/// <summary>
		/// Charger un fichier dans le control
		/// </summary>
		/// <param name="Filename">Chemin vers le fichier (path)</param>
		public void LoadFile(string Filename){
			LoadFile(Filename, "");
		}

		private void LoadPosition(){
			LoadPosition(this._CurrentPosition);
		}
                
		private void LoadPosition(string Position){
            try {
                //Preparation de la fonctions
                long StartPos = Convertir.HexaToDecimal(Position);
                long hauteur = this.Font.Height;
                this._TotalLine = picAdresse.Height / hauteur; //Total de ligne a charger
                long DataLenght = this._TotalLine * _ColumnSize;
                string EndPos = Convertir.DecimalToHexa((StartPos + DataLenght) - 1);

                htbPosition.HexaValue = Position;
                dtbPosition.Valeur = htbPosition.DecimalValue;
                sbValue.Value = (int)Convertir.HexaToDecimal(Position);

                //Verifie que l'adresse de fin est bien au maximum de la fin
                if (Convertir.HexaToDecimal(EndPos) > this._FileLen)
                    EndPos = Convertir.DecimalToHexa(this._FileLen);

                //Extraction des données et preparation avant de les dessiners
                string DataBrut = ExtractData.AspireString(Position, EndPos, this._FileName, true);
                string DataView = ExtractData.AspireString(Position, EndPos, this._FileName, false);
                string[] Data = Convertir.StringHexa_LittleEndian(DataBrut, true).Trim().Split(new char[] { ' ' });
                
                string[] Adresse = new string[this._TotalLine];
                if (!this._DecimalMode) {
                    //Creation des chaines de position en hexadecimale
                    
                    long tempPos = StartPos;
                    Adresse[0] = String.Format("{0, 8:0}", Convertir.DecimalToHexa(tempPos));  //Premiere position
                    Adresse[0] = Adresse[0].Replace(' ', '0');
                    for (int i = 1; i < Adresse.Length; i++) {
                        tempPos += _ColumnSize;
                        Adresse[i] = String.Format("{0, 8:0}", Convertir.DecimalToHexa(tempPos));  //String.Format()
                        Adresse[i] = Adresse[i].Replace(' ', '0');
                    }
                }
                else {
                    //Creation des chaines de en decimale
                    long tempPos = StartPos;
                    Adresse[0] = String.Format("{0, 8:0}", tempPos);  //Premiere position
                    Adresse[0] = Adresse[0].Replace(' ', '0');
                    for (int i = 1; i < Adresse.Length; i++) {
                        tempPos += _ColumnSize;
                        Adresse[i] = String.Format("{0, 8:0}", tempPos);  //String.Format()
                        Adresse[i] = Adresse[i].Replace(' ', '0');
                    }
                }

                //Dessiner les position 
                Graphics gfxAdresse = picAdresse.CreateGraphics();
                gfxAdresse.CompositingQuality = CompositingQuality.HighSpeed;
                gfxAdresse.Clear(Color.Gray);
                PointF ptAdr = new PointF(0, 0);
                for (int i = 0; i < Adresse.Length; i++) {
                    gfxAdresse.DrawString(Adresse[i], this.Font, Brushes.White, ptAdr, StringFormat.GenericDefault);
                    ptAdr.Y += this.Font.Height;
                }


                //Dessiner le Data				
                Graphics gfxData = picData.CreateGraphics();
                gfxData.CompositingQuality = CompositingQuality.HighSpeed;
                gfxData.Clear(Color.White);
                PointF ptData = new PointF(0, 0);
                int cnt = 0;
                for (int i = 0; i < Adresse.Length; i++) {
                    for (int j = 0; j < _ColumnSize; j++) {
                        if (cnt % 2 == 0)
                            gfxData.DrawString(Data[cnt] + " ", this.Font, Brushes.Black, ptData, StringFormat.GenericDefault);
                        else
                            gfxData.DrawString(Data[cnt] + " ", this.Font, Brushes.Blue, ptData, StringFormat.GenericDefault);

                        ptData.X += 20;
                        cnt++;
                    }

                    ptData.X = 0;
                    ptData.Y += this.Font.Height;
                }

                //Affiche le Data en fonction de la TBL ou non
                if (this.UseTBL)
                    if (this._ShowColor)
                        DrawTBLDataColorDTE(Data, Adresse.Length);
                    else
                        DrawTBLData(Data, Adresse.Length);
                else
                    DrawSimpleData(DataView, Adresse.Length);

            }
            catch { }
		}

		/// <summary>
		/// Dessine seulement les données
		/// </summary>
		/// <param name="Data"></param>
		/// <param name="TotalLine"></param>
		private void DrawSimpleData(string Data, int TotalLine){
			//Dessiner les données (sans TBL)
			Graphics gfxDecoded = picDecodedData.CreateGraphics();
			gfxDecoded.CompositingQuality = CompositingQuality.HighSpeed;
			gfxDecoded.Clear(Color.White);
			PointF ptDecoded = new PointF(0,0);
			int cnt = 0;
			string line = "";
			for(int i=0; i< TotalLine; i++){
				for (int j =0; j < _ColumnSize; j++){
					line += Data[cnt].ToString();
					cnt++;
				}

				gfxDecoded.DrawString(line , this.Font, Brushes.Black, ptDecoded, StringFormat.GenericDefault);

				ptDecoded.X = 0;
				ptDecoded.Y += this.Font.Height;
				line = "";
			}
		}

		private void DrawTBLData(string[] Data, int TotalLine){
			//Dessiner les données (avec TBL)
			Graphics gfxDecoded = picDecodedData.CreateGraphics();
			gfxDecoded.CompositingQuality = CompositingQuality.HighSpeed;
			gfxDecoded.Clear(Color.White);
			PointF ptDecoded = new PointF(0,0);
			int cnt = 0;
			string line = "";
			string dte = "";

			if (this._ShowMTE){
				for(int i=0; i< TotalLine; i++){
					for (int j =0; j < _ColumnSize; j++){
						if (cnt + 1 < Data.Length)
							dte = this._TBL.FindTBLMatch(Data[cnt] + Data[cnt + 1], this._ShowSpecial, this._NotShowDTE);
						else
							dte = "#";

						if (dte == "#")
							dte = this._TBL.FindTBLMatch(Data[cnt], this._ShowSpecial, this._NotShowDTE);
 
						line += dte;
						cnt++;
					}
					
					gfxDecoded.DrawString(line , this.Font, Brushes.Black, ptDecoded, StringFormat.GenericDefault);

					ptDecoded.X = 0;
					ptDecoded.Y += this.Font.Height;
					line = "";
				}
			}else{
				for(int i=0; i< TotalLine; i++){
					for (int j =0; j < _ColumnSize; j++){
						line += this._TBL.FindTBLMatch(Data[cnt], this._ShowSpecial, this._NotShowDTE);
						cnt++;
					}
					gfxDecoded.DrawString(line , this.Font, Brushes.Black, ptDecoded, StringFormat.GenericDefault);

					ptDecoded.X = 0;
					ptDecoded.Y += this.Font.Height;
					line = "";
				}
			}
		}

        private void DrawTBLDataColorDTE(string[] Data, int TotalLine) {
            //Dessiner les données (avec TBL)
            Graphics gfxDecoded = picDecodedData.CreateGraphics();
            gfxDecoded.CompositingQuality = CompositingQuality.HighSpeed;
            gfxDecoded.Clear(Color.White);
            PointF ptDecoded = new PointF(0, 0);
            int cnt = 0;
            string dte = "";
            float OneCharLenght = gfxDecoded.MeasureString("I", this.Font).Width;

            if (this._ShowMTE) {
                for (int i = 0; i < TotalLine; i++) {
                    for (int j = 0; j < _ColumnSize; j++) {
                        if (cnt + 1 < Data.Length)
                            dte = this._TBL.FindTBLMatch(Data[cnt] + Data[cnt + 1], this._ShowSpecial, this._NotShowDTE);
                        else
                            dte = "#";

                        if (dte == "#")
                            dte = this._TBL.FindTBLMatch(Data[cnt], this._ShowSpecial, this._NotShowDTE);
                        
                        switch (DTE.TypeDTE(dte)){
                            case DTEType.DualTitleEncoding:
                                gfxDecoded.DrawString(dte, this.Font, Brushes.Red, ptDecoded, StringFormat.GenericDefault);
                                break;
                            case DTEType.MultipleTitleEncoding:
                                gfxDecoded.DrawString(dte, this.Font, Brushes.Blue, ptDecoded, StringFormat.GenericDefault);
                                break;
                            default:
                                gfxDecoded.DrawString(dte, this.Font, Brushes.Black, ptDecoded, StringFormat.GenericDefault);
                                break;
                        }
                        ptDecoded.X += (9 * dte.Length);  //gfxDecoded.MeasureString(dte, this.Font).Width;
                        cnt++;
                        
                    }                                       

                    ptDecoded.X = 0;
                    ptDecoded.Y += this.Font.Height;
                }
            }
            else {
                for (int i = 0; i < TotalLine; i++) {
                    for (int j = 0; j < _ColumnSize; j++) {
                        dte = this._TBL.FindTBLMatch(Data[cnt], this._ShowSpecial, this._NotShowDTE);

                        switch (DTE.TypeDTE(dte)) {
                            case DTEType.DualTitleEncoding:
                                gfxDecoded.DrawString(dte, this.Font, Brushes.Red, ptDecoded, StringFormat.GenericDefault);
                                break;
                            case DTEType.MultipleTitleEncoding:
                                gfxDecoded.DrawString(dte, this.Font, Brushes.Blue, ptDecoded, StringFormat.GenericDefault);
                                break;
                            default:
                                gfxDecoded.DrawString(dte, this.Font, Brushes.Black, ptDecoded, StringFormat.GenericDefault);
                                break;
                        }
                                                
                        ptDecoded.X += (9 * dte.Length);  //gfxDecoded.MeasureString(dte, this.Font).Width;
                        cnt++;
                    }

                    ptDecoded.X = 0;
                    ptDecoded.Y += this.Font.Height;
                }
            }
        }

		private void HexaFileShow_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			if (this._isLoaded)
				LoadPosition(this._CurrentPosition);
		}

		private void sbValue_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e) {
			if (this._isLoaded){
				this._CurrentPosition = Convertir.DecimalToHexa(sbValue.Value);
				LoadPosition(this._CurrentPosition); 
			}
		}

		private void HexaFileShow_Resize(object sender, System.EventArgs e) {
			picDecodedData.Top = tbFileShow.Bottom;
			picDecodedData.Height = this.Height - tbFileShow.Height;
			picDecodedData.Left = picData.Right; 
			picDecodedData.Width = this.Width - sbValue.Width - picDecodedData.Left;

			RefreshData();
		}

		/// <summary>
		/// Ferme le ficher
		/// </summary>
		public void CloseFile(){
			//Efface le data
			picAdresse.CreateGraphics().Clear(Color.Gray);
			picData.CreateGraphics().Clear(Color.White);
			picDecodedData.CreateGraphics().Clear(Color.White);

			this._FileLen = 0;
			this._FileName = "";
			this._isLoaded = false;
			this._CurrentPosition = "0";
			this.tblCombo1.Projet = null;
		}

		/// <summary>
		/// Rafraichir le control
		/// </summary>
		public void RefreshData(){
			if (this._isLoaded){				 
				ThreadStart myThreadStart = new ThreadStart(this.LoadPosition);
				Thread th = new Thread(myThreadStart);
				th.Start();
			}
		}

		private void tbFileShow_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			switch (e.Button.Tag.ToString()) {
				case "Refresh":
					RefreshData();
					break;
				case "ShowSpecial":
					this._ShowSpecial = e.Button.Pushed; 
					RefreshData();
					break;
				case "NotShowDTE":
					this._NotShowDTE = !e.Button.Pushed; 
					RefreshData();
					break;
				case "ShowAscii":
					this._isUseTBL = !e.Button.Pushed;
					toolBarButtonShowMTE.Enabled = !e.Button.Pushed;
					toolBarButtonShowSpecial.Enabled = !e.Button.Pushed;
					toolBarButtonNotShowDTE.Enabled = !e.Button.Pushed;
					RefreshData();
					break;
				case "ShowMTE":
					this._ShowMTE = e.Button.Pushed;
					RefreshData();
					break;
                case "ShowColor":
                    this._ShowColor = e.Button.Pushed;
                    RefreshData();
                    break;
                case "DecimalHexa":
                    this._DecimalMode = !e.Button.Pushed;
                    dtbPosition.Visible = !e.Button.Pushed;
                    htbPosition.Visible = e.Button.Pushed;
                    dtbPosition.Valeur = htbPosition.DecimalValue; 
                    RefreshData();
                    break;
			}
		}

		private void htbPosition_TBKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Return)
				if (this._isLoaded){
					LoadPosition(htbPosition.HexaValue);
					this._CurrentPosition = htbPosition.HexaValue;
				}
		}

		private void tblCombo1_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (tblCombo1.Items.Count > 0 ){
				string path = tblCombo1.SelectedItem.Tag.ToString();

				if (File.Exists(path)){
					this._TBL.FileName = path;
					this._TBL.Load();
					RefreshData();
				}
			}
		}

		private void picData_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			try{
				if (e.Button == MouseButtons.Right){
					this._PicDataRightClick = true;
					RightClick(sender, e);
				}
				else //Lance l'evenement ExplorerClick
					ExplorerClick(sender, e);
			}
			catch{
			}

		}

		private void picDecodedData_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			try{
				if (e.Button == MouseButtons.Right)	{				
					this._PicDataRightClick = false;
					RightClick(sender, e);
				}
				else //Lance l'evenement ExplorerClick
					ExplorerClick(sender, e);
			}
			catch{
			}
		}

		private void KeyCode(Keys keys){
			switch (keys){
				case Keys.PageDown:
					Position =  Convertir.DecimalToHexa( Convertir.HexaToDecimal(this._CurrentPosition) + (this._ColumnSize * this._TotalLine) );
					break;
				case Keys.PageUp:
					if (Position != "0")
						if (Convertir.HexaToDecimal(this._CurrentPosition) > (this._ColumnSize * this._TotalLine))
							Position =  Convertir.DecimalToHexa( Convertir.HexaToDecimal(this._CurrentPosition) - (this._ColumnSize * this._TotalLine) );
						else
							Position = "0";
					break;
				case Keys.Home:
					Position = "0";
					break;
				case Keys.End:
					Position = Convertir.DecimalToHexa(this.sbValue.Maximum);
					break;
			}
		}

		private void picDecodedData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			KeyCode(e.KeyCode); 
		}

		private void picData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			KeyCode(e.KeyCode);
		}

		private void picAdresse_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			KeyCode(e.KeyCode);
		}

		private void upDownCol_Leave(object sender, System.EventArgs e) {
			if (upDownCol.Value > upDownCol.Maximum)
				upDownCol.Value = upDownCol.Maximum;

			upDownCol.BackColor = tblCombo1.ViewStyle.EditColor; 
		}

		private void upDownCol_Enter(object sender, System.EventArgs e) {
			upDownCol.BackColor = tblCombo1.ViewStyle.EditFocusedColor; 
		}

		private void upDownCol_ValueChanged(object sender, System.EventArgs e) {
			this._ColumnSize = (int)upDownCol.Value;
			this.picData.Width = this._ColumnSize * 20 + 10;			
			this.sbValue.SmallChange = this._ColumnSize;
			
			this.OnResize(e);
		}

		private void upDownCol_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			e.Handled = true;
		}

		private void Control_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (this._isLoaded)
				if (e.Delta > 0){
					if (Position != "0")
						if (Convertir.HexaToDecimal(this._CurrentPosition) > this._ColumnSize)
							Position =  Convertir.DecimalToHexa( Convertir.HexaToDecimal(this._CurrentPosition) - this._ColumnSize);
						else
							Position = "0";
				}
				else
					Position =  Convertir.DecimalToHexa( Convertir.HexaToDecimal(this._CurrentPosition) + this._ColumnSize);
		
		}

		/// <summary>
		/// Definir/ Défini la position d'affichage
		/// </summary>
		/// <param name="Position">Position (Hexa)</param>
		public string Position{
			get{
				return this._CurrentPosition;
			}
			set{
				try{
					this._CurrentPosition = value;
					sbValue.Value = (int)Convertir.HexaToDecimal(value);
					LoadPosition(this._CurrentPosition); 
				}catch{}
			}
		}

		/// <summary>
		/// Taille du fichier
		/// </summary>
		public long Lenght{
			get{
				return this._FileLen; 
			}
		}

		/// <summary>
		/// Le control doit il utilisé la TBL chargé ?
		/// </summary>
		public bool UseTBL{
			get{
				return this._isUseTBL;
			}
			set{
				this._isUseTBL = value;
				RefreshData(); 
			}
		}

		/// <summary>
		/// Le control doit il utilisé la TBL chargé ?
		/// </summary>
		public bool ShowMTE{
			get{
				return this._ShowMTE;
			}
			set{
				this._ShowMTE = value;
				RefreshData(); 
			}
		}

        /// <summary>
        /// Objet contenant la Table de jeu charger dans le control
        /// </summary>
		public TBLStream TBL{
			get{
				return this._TBL;
			}
			set{
				this.TBL = value;
			}
		}

		/// <summary>
		/// Projet a utilisé
		/// </summary>
		[Browsable(false)]
		public VRS.Library.Projet.Project Projet{
			set{
				this.tblCombo1.Projet = value;
			}
		}

        private void picDecodedData_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void dtbPosition_TBKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return)
                if (this._isLoaded) {
                    htbPosition.DecimalValue = dtbPosition.Valeur;
                    LoadPosition(htbPosition.HexaValue);
                    this._CurrentPosition = htbPosition.HexaValue;
                }
        }

	}
}