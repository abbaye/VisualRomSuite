//using System.IO;
//using VRS.Library.Convertion;

//namespace VRS.Library.UserControl
//{
//    /// <summary>
//    /// Description résumée de TextEditor.
//    /// </summary>
//    public class TextEditor : System.Windows.Forms.UserControl {
//		/// <summary>
//		/// Chemin vers le fichier;
//		/// </summary>
//		private string _FileName = "";

//		//private AxCodeMax.AxCodeMax axTextEdit;
        
//		/// <summary> 
//		/// Variable nécessaire au concepteur.
//		/// </summary>
//		private System.ComponentModel.Container components = null;

//		public TextEditor() {
//			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
//			InitializeComponent();

//			//Configurer l'editeur
//			ConfigEditor();

//		}

//		/// <summary> 
//		/// Nettoyage des ressources utilisées.
//		/// </summary>
//		protected override void Dispose( bool disposing ) {
//			if( disposing ) {
//				if(components != null) {
//					components.Dispose();
//				}
//			}
//			base.Dispose( disposing );
//		}

//		#region Component Designer generated code
//		/// <summary> 
//		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
//		/// le contenu de cette méthode avec l'éditeur de code.
//		/// </summary>
//		private void InitializeComponent() {
//			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TextEditor));
//			this.axTextEdit = new AxCodeMax.AxCodeMax();
//			((System.ComponentModel.ISupportInitialize)(this.axTextEdit)).BeginInit();
//			this.SuspendLayout();
//			// 
//			// axTextEdit
//			// 
//			this.axTextEdit.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.axTextEdit.Location = new System.Drawing.Point(0, 0);
//			this.axTextEdit.Name = "axTextEdit";
//			this.axTextEdit.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTextEdit.OcxState")));
//			this.axTextEdit.Size = new System.Drawing.Size(368, 232);
//			this.axTextEdit.TabIndex = 0;
//			// 
//			// TextEditor
//			// 
//			this.Controls.Add(this.axTextEdit);
//			this.Name = "TextEditor";
//			this.Size = new System.Drawing.Size(368, 232);
//			((System.ComponentModel.ISupportInitialize)(this.axTextEdit)).EndInit();
//			this.ResumeLayout(false);

//		}
//		#endregion

//		public void LoadFile(string Filename){
//			if (File.Exists(Filename)){
//				axTextEdit.OpenFile(Filename);
//				this._FileName = Filename;
//			}
//		}

//		public string FileName{
//			get{
//				return this._FileName;
//			}
//		}

//		/// <summary>
//		/// Obtien ou défini un valeur permettant de blocker l'édition du control
//		/// </summary>
//		public bool Locked{
//			get{
//				return axTextEdit.ReadOnly;
//			}
//			set{
//				axTextEdit.ReadOnly = value;
//			}
//		}

//		public void Save(){
//			if (this._FileName != "")
//				axTextEdit.SaveFile(this.FileName, false);
//		}

//		public void SaveAs(string FileName){
//			axTextEdit.SaveFile(FileName, false);
//			this._FileName = FileName; 
//		}

//		public void ClearUndo(){
//			axTextEdit.ClearUndoBuffer();
//		}

//		public void ConfigEditor(){
//			axTextEdit.SmoothScrolling = false;

//			//Couleur de l'editeur
//			axTextEdit.SetColor(cmColorItem.cmClrLeftMargin, Convertir.RGB(150, 150, 150));
//			axTextEdit.SetColor(cmColorItem.cmClrBookmarkBk, Convertir.RGB(150, 150, 150));
//			axTextEdit.SetColor(cmColorItem.cmClrKeyword, Convertir.RGB(0, 0, 150));
//			axTextEdit.SetColor(cmColorItem.cmClrLineNumber, Convertir.RGB(255,255, 255));
//			axTextEdit.SetColor(cmColorItem.cmClrLineNumberBk, Convertir.RGB(150, 150, 150));
//			axTextEdit.SetColor(cmColorItem.cmClrOperator, Convertir.RGB(0, 0, 255));
//			axTextEdit.SetColor(cmColorItem.cmClrComment, Convertir.RGB(0, 150, 0));						
//		}

//		public void AddLine(string Text){
//			axTextEdit.InsertLine( axTextEdit.LineCount -1, Text);
//		}

//		public bool VerticalScrollVisible{
//			get{
//				return axTextEdit.VScrollVisible;
//			}
//			set{
//				axTextEdit.VScrollVisible = value;
//			}
//		}

//		public bool HorizontalScrollVisible{
//			get{
//				return axTextEdit.HScrollVisible;
//			}
//			set{
//				axTextEdit.HScrollVisible = value;
//			}
//		}
		
//		/// <summary>
//		/// obtient ou defini le type de bordure du control
//		/// </summary>
//		public TextEditorBorder TEBorderStyle{
//			get{				
//				switch (axTextEdit.BorderStyle){
//					case cmBorderStyle.cmBorderStatic:
//						return TextEditorBorder.Static;
//					case cmBorderStyle.cmBorderThin:
//						return TextEditorBorder.Thin;
//					default:
//						return TextEditorBorder.Node; 
//				}
//			}
//			set{
//				switch (value){
//					case TextEditorBorder.Static:
//						axTextEdit.BorderStyle = cmBorderStyle.cmBorderStatic;
//						break;
//					case TextEditorBorder.Thin:
//						axTextEdit.BorderStyle = cmBorderStyle.cmBorderThin;
//						break;
//					default:
//						axTextEdit.BorderStyle = cmBorderStyle.cmBorderNone;
//						break;
//				}				
//			}
//		}
//	}
//}
