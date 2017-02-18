using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using VRS.Library.Table;
using VRS.Library.Table.TBL;
using VRS.Library.Convertion;
using VRS.Library.IO;
using VRS.Library.Console.SuperNintendo;
using VRS.Library.Projet;
using VRS.Library.Win32;

namespace VRS.IDE {
	/// <summary>
	/// Description résumée de Form1.
	/// </summary>
	public class frmTest : System.Windows.Forms.Form {
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private VRS.Library.UserControl.DecimalTextBox decimalTextBox1;
		private VRS.Library.UserControl.Ligne3d ligne3d1;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Button button9;
		private VRS.Library.UserControl.HexaTextBox hexaTextBox1;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private VRS.Library.UserControl.TextControl textControl1;
		private System.Windows.Forms.Button button13;
		private System.Windows.Forms.Button button14;
		private System.Windows.Forms.TextBox textBox7;
		private VRS.Library.UserControl.HexaFileShow hexaFileShow1;
		private System.Windows.Forms.Button button15;
		private System.Windows.Forms.Button button16;
		private System.Windows.Forms.Button button17;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.Button button18;
		private System.Windows.Forms.Button button19;
		private System.Windows.Forms.Button button20;
		private VRS.Library.UserControl.ProjectExplorer projectExplorer1;
		private System.Windows.Forms.Button button21;
		private System.Windows.Forms.Button button22;
		private System.ComponentModel.IContainer components;

		public frmTest() {
			InitializeComponent();
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			this.button1 = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.decimalTextBox1 = new VRS.Library.UserControl.DecimalTextBox();
			this.ligne3d1 = new VRS.Library.UserControl.Ligne3d();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.button9 = new System.Windows.Forms.Button();
			this.hexaTextBox1 = new VRS.Library.UserControl.HexaTextBox();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button12 = new System.Windows.Forms.Button();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.textControl1 = new VRS.Library.UserControl.TextControl();
			this.button13 = new System.Windows.Forms.Button();
			this.button14 = new System.Windows.Forms.Button();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.hexaFileShow1 = new VRS.Library.UserControl.HexaFileShow();
			this.button15 = new System.Windows.Forms.Button();
			this.button16 = new System.Windows.Forms.Button();
			this.button17 = new System.Windows.Forms.Button();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.button18 = new System.Windows.Forms.Button();
			this.button19 = new System.Windows.Forms.Button();
			this.button20 = new System.Windows.Forms.Button();
			this.projectExplorer1 = new VRS.Library.UserControl.ProjectExplorer();
			this.button21 = new System.Windows.Forms.Button();
			this.button22 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(16, 184);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(104, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "Charger la TBL";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(8, 16);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(120, 160);
			this.listBox1.TabIndex = 1;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(16, 248);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(72, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "Convertir";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 224);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(72, 20);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "textBox1";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(160, 120);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(120, 24);
			this.button3.TabIndex = 5;
			this.button3.Text = "Extraire Data";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(152, 16);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox2.Size = new System.Drawing.Size(272, 96);
			this.textBox2.TabIndex = 6;
			this.textBox2.Text = "textBox2";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(24, 288);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(88, 20);
			this.textBox3.TabIndex = 7;
			this.textBox3.Text = "textBox3";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(24, 312);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(88, 24);
			this.button4.TabIndex = 8;
			this.button4.Text = "S --> Hexa (xx)";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(704, 16);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox4.Size = new System.Drawing.Size(144, 88);
			this.textBox4.TabIndex = 9;
			this.textBox4.Text = "textBox4";
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(712, 112);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(104, 23);
			this.button5.TabIndex = 10;
			this.button5.Text = "StringHexa";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(384, 328);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(104, 23);
			this.button6.TabIndex = 11;
			this.button6.Text = "SnesInfo FF2";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(384, 304);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(104, 20);
			this.textBox5.TabIndex = 12;
			this.textBox5.Text = "textBox5";
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(144, 192);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(128, 24);
			this.button7.TabIndex = 14;
			this.button7.Text = "SnesRom.isValid() ?";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(712, 160);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(96, 24);
			this.button8.TabIndex = 15;
			this.button8.Text = "HashFile";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// decimalTextBox1
			// 
			this.decimalTextBox1.Location = new System.Drawing.Point(312, 272);
			this.decimalTextBox1.Name = "decimalTextBox1";
			this.decimalTextBox1.Size = new System.Drawing.Size(80, 20);
			this.decimalTextBox1.TabIndex = 17;
			this.decimalTextBox1.Valeur = ((long)(67));
			// 
			// ligne3d1
			// 
			this.ligne3d1.CouleurLigne1 = System.Drawing.Color.Gray;
			this.ligne3d1.CouleurLigne2 = System.Drawing.Color.White;
			this.ligne3d1.Location = new System.Drawing.Point(32, 360);
			this.ligne3d1.Name = "ligne3d1";
			this.ligne3d1.Size = new System.Drawing.Size(312, 3);
			this.ligne3d1.TabIndex = 18;
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(136, 264);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(104, 20);
			this.textBox6.TabIndex = 19;
			this.textBox6.Text = "textBox6";
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(136, 288);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(104, 23);
			this.button9.TabIndex = 20;
			this.button9.Text = "Insert Data";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// hexaTextBox1
			// 
			this.hexaTextBox1.DecimalValue = ((long)(46));
			this.hexaTextBox1.HexaValue = "2E";
			this.hexaTextBox1.Location = new System.Drawing.Point(288, 240);
			this.hexaTextBox1.Maximum = ((long)(255));
			this.hexaTextBox1.Minimum = ((long)(45));
			this.hexaTextBox1.Name = "hexaTextBox1";
			this.hexaTextBox1.Size = new System.Drawing.Size(144, 20);
			this.hexaTextBox1.TabIndex = 21;
			// 
			// button10
			// 
			this.button10.Location = new System.Drawing.Point(296, 192);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(112, 24);
			this.button10.TabIndex = 22;
			this.button10.Text = "AppOptions Test";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Location = new System.Drawing.Point(464, 8);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(88, 32);
			this.button11.TabIndex = 23;
			this.button11.Text = "Create + Save Project";
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// button12
			// 
			this.button12.Location = new System.Drawing.Point(576, 8);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(96, 32);
			this.button12.TabIndex = 24;
			this.button12.Text = "LoadProjet FF2";
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(464, 48);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(208, 216);
			this.propertyGrid1.TabIndex = 25;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// textControl1
			// 
			this.textControl1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textControl1.Location = new System.Drawing.Point(256, 240);
			this.textControl1.Name = "textControl1";
			this.textControl1.Size = new System.Drawing.Size(16, 96);
			this.textControl1.TabIndex = 27;
			this.textControl1.TextDirection = VRS.Library.UserControl.Direction.Vertical;
			this.textControl1.Texte = "TextControl";
			// 
			// button13
			// 
			this.button13.Location = new System.Drawing.Point(208, 392);
			this.button13.Name = "button13";
			this.button13.Size = new System.Drawing.Size(80, 24);
			this.button13.TabIndex = 29;
			this.button13.Text = "Load ff2.smc";
			this.button13.Click += new System.EventHandler(this.button13_Click);
			// 
			// button14
			// 
			this.button14.Location = new System.Drawing.Point(128, 392);
			this.button14.Name = "button14";
			this.button14.Size = new System.Drawing.Size(75, 24);
			this.button14.TabIndex = 30;
			this.button14.Text = "SetAdresse";
			this.button14.Click += new System.EventHandler(this.button14_Click);
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(24, 392);
			this.textBox7.Name = "textBox7";
			this.textBox7.TabIndex = 31;
			this.textBox7.Text = "81c0";
			// 
			// hexaFileShow1
			// 
			this.hexaFileShow1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.hexaFileShow1.Location = new System.Drawing.Point(16, 424);
			this.hexaFileShow1.Name = "hexaFileShow1";
			this.hexaFileShow1.Position = "0";
			this.hexaFileShow1.Size = new System.Drawing.Size(872, 128);
			this.hexaFileShow1.TabIndex = 32;
			this.hexaFileShow1.UseTBL = false;
			// 
			// button15
			// 
			this.button15.Location = new System.Drawing.Point(664, 520);
			this.button15.Name = "button15";
			this.button15.Size = new System.Drawing.Size(80, 24);
			this.button15.TabIndex = 29;
			this.button15.Text = "Load ff2.smc";
			// 
			// button16
			// 
			this.button16.Location = new System.Drawing.Point(568, 520);
			this.button16.Name = "button16";
			this.button16.Size = new System.Drawing.Size(75, 24);
			this.button16.TabIndex = 30;
			this.button16.Text = "SetAdresse";
			// 
			// button17
			// 
			this.button17.Location = new System.Drawing.Point(560, 520);
			this.button17.Name = "button17";
			this.button17.Size = new System.Drawing.Size(75, 24);
			this.button17.TabIndex = 30;
			this.button17.Text = "SetAdresse";
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(472, 520);
			this.textBox8.Name = "textBox8";
			this.textBox8.TabIndex = 31;
			this.textBox8.Text = "81c0";
			// 
			// button18
			// 
			this.button18.Location = new System.Drawing.Point(656, 520);
			this.button18.Name = "button18";
			this.button18.Size = new System.Drawing.Size(80, 24);
			this.button18.TabIndex = 29;
			this.button18.Text = "Load ff2.smc";
			// 
			// button19
			// 
			this.button19.Location = new System.Drawing.Point(352, 120);
			this.button19.Name = "button19";
			this.button19.Size = new System.Drawing.Size(104, 23);
			this.button19.TabIndex = 33;
			this.button19.Text = "Test TBL property";
			this.button19.Click += new System.EventHandler(this.button19_Click);
			// 
			// button20
			// 
			this.button20.Location = new System.Drawing.Point(296, 392);
			this.button20.Name = "button20";
			this.button20.Size = new System.Drawing.Size(144, 23);
			this.button20.TabIndex = 34;
			this.button20.Text = "Load ff2.smc (TBL mode)";
			this.button20.Click += new System.EventHandler(this.button20_Click);
			// 
			// projectExplorer1
			// 
			this.projectExplorer1.Location = new System.Drawing.Point(504, 280);
			this.projectExplorer1.Name = "projectExplorer1";
			this.projectExplorer1.Projet = null;
			this.projectExplorer1.Size = new System.Drawing.Size(96, 128);
			this.projectExplorer1.StringHexa = "Binaire";
			this.projectExplorer1.StringProjet = "Projet";
			this.projectExplorer1.StringTable = "Tables";
			this.projectExplorer1.StringText = "Textes";
			this.projectExplorer1.TabIndex = 35;
			// 
			// button21
			// 
			this.button21.Location = new System.Drawing.Point(152, 320);
			this.button21.Name = "button21";
			this.button21.TabIndex = 36;
			this.button21.Text = "ShellAbout";
			this.button21.Click += new System.EventHandler(this.button21_Click);
			// 
			// button22
			// 
			this.button22.Location = new System.Drawing.Point(704, 200);
			this.button22.Name = "button22";
			this.button22.Size = new System.Drawing.Size(104, 23);
			this.button22.TabIndex = 37;
			this.button22.Text = "FolderDialog";
			this.button22.Click += new System.EventHandler(this.button22_Click);
			// 
			// frmTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(904, 566);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button22,
																		  this.button21,
																		  this.projectExplorer1,
																		  this.button20,
																		  this.button19,
																		  this.textBox7,
																		  this.button14,
																		  this.button13,
																		  this.hexaFileShow1,
																		  this.textControl1,
																		  this.propertyGrid1,
																		  this.button12,
																		  this.button11,
																		  this.button10,
																		  this.hexaTextBox1,
																		  this.button9,
																		  this.textBox6,
																		  this.button1,
																		  this.button3,
																		  this.ligne3d1,
																		  this.decimalTextBox1,
																		  this.button8,
																		  this.button7,
																		  this.textBox5,
																		  this.button6,
																		  this.button5,
																		  this.textBox4,
																		  this.button4,
																		  this.textBox3,
																		  this.textBox2,
																		  this.textBox1,
																		  this.button2,
																		  this.listBox1,
																		  this.button15,
																		  this.button16,
																		  this.button17,
																		  this.textBox8,
																		  this.button18});
			this.Name = "frmTest";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e) {
			listBox1.Items.Clear();

			TBLStream myTBL = new TBLStream();

			//Chargement de la TBL
			myTBL.Load(@"e:\ff2.tbl");

			this.Text = "" + myTBL.Length; //Total d'entre

			//myTBL[1].Entry = "33"; // modification pour tester l'indexer

			//remplir la list
			for (int i=0; i< myTBL.Length; i++ ){
				if (myTBL[i].Type != DTEType.EndBlock &&
					myTBL[i].Type != DTEType.EndLine){
					listBox1.Items.Add ( myTBL[i].Entry + "=" + myTBL[i].Value);
				}
				else
					listBox1.Items.Add ( myTBL[i].Entry);
			}

			//enregistrer dans un autre fichier
			myTBL.Save(@"e:\test.txt");

			//total de DTE
			this.Text = Convert.ToString(myTBL.TotalASCII );

			//Cleen object
			myTBL.Dispose();
		}

		private void button2_Click(object sender, System.EventArgs e) {
			textBox1.Text = Convertir.DecimalToHexa(10);
		}

		private void button3_Click(object sender, System.EventArgs e) {
			//textBox2.Text = ExtractData.Aspire("0D", "FF", @"d:\cookies.txt");
			//textBox2.Text = ExtractData.AspireString(@"test.txt");
			this.Text = textBox2.Text.Length.ToString();
		}

		private void button4_Click(object sender, System.EventArgs e) {
			textBox3.Text = Convertir.Hexa("s");
		}

		private void button5_Click(object sender, System.EventArgs e) {
			//textBox4.Text = Convertir.StringHexa("Convert", true);
			string temp = ExtractData.AspireString("0D", "FFF", @"d:\cookies.txt");
			textBox4.Text = Convertir.StringHexa(temp, true);
		}

		private void button6_Click(object sender, System.EventArgs e) {
			//SnesRom rom = new SnesRom("ff2.smc");
			OpenFileDialog file = new OpenFileDialog();

			file.ShowDialog(this);

			if (File.Exists(file.FileName)){
				SnesRom rom = new SnesRom(file.FileName);
				textBox5.Text = rom.RomName;
				MessageBox.Show("Fichier SNES Valide : " + SnesRom.isValid(file.FileName).ToString());
			}
		}

		private void button7_Click(object sender, System.EventArgs e) {
			MessageBox.Show(SnesRom.isValid(@"d:\cookies.txt").ToString());
		}

		private void Form1_Load(object sender, System.EventArgs e) {
			
		}

		private void button8_Click(object sender, System.EventArgs e) {
			MessageBox.Show(this, HashFile.GetMD5Hexa(@"d:\cookies.txt"));
		}

		private void button9_Click(object sender, System.EventArgs e) {
			//InsertData.WriteBinary("test.bin", 1, textBox6.Text.ToCharArray());  
		}

		private void button10_Click(object sender, System.EventArgs e) {			
			MessageBox.Show(App.Name);
		}

		private void button11_Click(object sender, System.EventArgs e) {
			Project myProject = new Project();
			TextFile txt = new TextFile();
			HexaSnapShot hexa = new HexaSnapShot();	
			TBLFile tbl = new TBLFile();
			TableFixeFile fixe = new TableFixeFile(); 
			Favoris mark = new Favoris();
			Task task = new Task(); 
                                                       
			myProject.CreateProject("FF2", @"c:", "ff2.smc", "ff2.tbl");

			for(int i=0; i<4; i++){
				myProject.Textes.Add(txt); 
				myProject.HexaSnapShot.Add(hexa);
				myProject.Tables.Add(tbl);
				myProject.TableFixe.Add(fixe);
				myProject.Favoris.Add(mark);
				myProject.Taches.Add(task);
			}

			myProject.Save();
		}

		private void button12_Click(object sender, System.EventArgs e) {
			Project myProject = new Project(@"C:\FF2\FF2.vrsproj");
			ProjectError err = myProject.Load();
			
			if (err != ProjectError.NoError)
				MessageBox.Show(err.ToString());
			else{				
				propertyGrid1.SelectedObject = myProject;
				MessageBox.Show("Nombre de Tables"  + myProject.Tables.Count);
			}			
		}

		private void button13_Click(object sender, System.EventArgs e) {
			hexaFileShow1.LoadFile("ff2.smc");
		}

		private void button14_Click(object sender, System.EventArgs e) {
			hexaFileShow1.Position = textBox7.Text;			 
		}

		private void button19_Click(object sender, System.EventArgs e) {
			TBLStream tbl = new TBLStream("ff2.tbl");
			tbl.Load();
			propertyGrid1.SelectedObject = tbl;
		}

		private void button20_Click(object sender, System.EventArgs e) {
			hexaFileShow1.LoadFile("ff2.smc", "ff2.tbl");
		}

		private void button21_Click(object sender, System.EventArgs e) {			 
			new AboutBox(this.Handle.ToInt32(), this.Icon.Handle.ToInt32(), App.Name, App.VersionInfo.FileVersion).Show();
		}

		private void button22_Click(object sender, System.EventArgs e) {
			OpenFolderDialog fol = new OpenFolderDialog("dddd");
			MessageBox.Show(fol.GetFolder());
		}
	}
}
