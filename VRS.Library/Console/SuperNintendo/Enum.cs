using System;

//Enumeration du Namespace VRS.Library.SuperNintendo
namespace VRS.Library.Console.SuperNintendo {
	/// <summary>
	/// Type de rom Super Nintendo
	/// </summary>
	public enum RomType{
		Aucune			= -1,
		LowRom_School_1	= 0,
		HighRom			= 1,
		LowRom_School_2	= 2,	
	}

	/// <summary>
	/// Taille de la memoire qui est dans la cartouche (KiloByte)
	/// </summary>
	public enum RAMSize{
		Inconnu	= -1,
		KB_0	=  0, //00
		KB_16	=  1, //01
		KB_32	=  2, //02
		KB_64	=  3, //03
		KB_128	=  4, //04
		KB_256	=  5  //05
	}

	/// <summary>
	/// Taille de la cartouche en  MegaByte
	/// </summary>
	public enum ROMSize{
		Inconnu				=  0,
		Deux_MB				=  8, //08
		Quatre_MB			=  9, //09
		Huit_MB				= 10, //0A
		Seize_MB			= 11, //0B
		Trente_Deux_MB		= 12, //0C
		Quarante_Huit_MB	= 13, //0D
	}

	/// <summary>
	/// Information sur la cartouche
	/// </summary>
	public enum InformationCartouche{
		Inconnu			= -1,
		ROM				=  0,
		ROM_RAM			=  1,
		ROM_SRAM		=  2,
		ROM_DSP1		=  3,
		ROM_DSP1_RAM	=  4,
		ROM_DSP1_SRAM	=  5,
		FX				=  6
	}
    
	/// <summary>
	/// Pays de manufacture
	/// </summary>
	public enum Country {
		Inconnu						= -1,
		Japon						= 0,  //"00"
		USA							= 1,  //"01"
		Europe_Oceanie_Asie			= 2,  //"02"
		Suède						= 3,  //"03"
		Finlande					= 4,  //"04"
		Danemark					= 5,  //"05"
		France						= 6,  //"06"
		Hollande					= 7,  //"07"
		Espagne						= 8,  //"08"
		Allemagne_Autriche_Suisse	= 9,  //"09"
		Italie						= 10, //"0A"
		Hong_Kong_Chine				= 11, //"0B"
		Indonésie					= 13, //"0C"
		Korée						= 14  //"0D"
	}

	/// <summary>
	/// Type d'affichage
	/// </summary>
	public enum FormatAffichage{
		NTSC = 0,
		PAL  = 1
	}

	/// <summary>
	/// Compagnie qui on des license Super Nintendo
	/// </summary>
	public enum Compagnie{
		Inconnu							= 0,
		Nintendo						= 1,
		Zamuse							= 5,
		Capcom							= 8,
		HOT_B							= 9,
		Jaleco							= 10,
		STORM							= 11,
		Mebio_Software					= 15,
		Gremlin_Graphics				= 18,
		COBRA_Team						= 21,
		Human_Field						= 22,
		Hudson_Soft_1					= 24,
		Yanoman							= 26,
		Tecmo_1							= 28,
		Forum							= 30,	
		Park_Place_Productions_VIRGIN	= 31,
		Tokai_Engineering_SUNSOFT		= 33,
		POW								= 34,
		Loriciel_Micro_World			= 35,
		Enix_1							= 38,
		Kemco_1							= 40,
		Seta_Co_Ltd						= 41,
		Visit_Co_Ltd					= 45,
		Square_Co_Ltd					= 51,
		HECT							= 53,
		Loriciel						= 61,
		Seika_Corp						= 64,
		UBI_Soft						= 65,
		Spectrum_Holobyte				= 71,
		Irem							= 73,
		Raya_Systems					= 75,
		Renovation_Pruducts				= 76,
		Malibu_Games					= 77,
		US_Gold							= 79,
		Absolute_Entertainment			= 80,
		Acclaim							= 81,
		Activision						= 82,
		American_Sammy					= 83,
		GameTek							= 84,
		Hi_Tech							= 85,
		LJN_Toys						= 86,
		Mindscape						= 90,
		Technos_Japan_Corp				= 93,
		American_Softworks_Corp			= 95,
		Titus							= 96,	
		Virgin_Games					= 97,
		Maxis							= 98,
		Ocean							= 103,
		Electronic_Arts					= 105,	 
		Laser_Beam						= 107,
		Elite							= 110,
		Electro_Brain					= 111,
		Infogrames						= 112,
		Interplay						= 113,
		LucasArts						= 114,
		Sculptured_Soft					= 115,
		STORM_Sales_Curve				= 117,
		THQ_Software					= 120,
		Accolade_Inc					= 121,
		Triffix_Entertainment			= 122,
		Microprose						= 124,
		Kemco_2							= 127,
		Namcot_Namco_Ltd_1				= 130,
		Koei_2							= 132,
		Tokuma_Shoten_Intermedia		= 134,
		DATAM_Polystar					= 136,
		Square_Europe					= 137,
		Bullet_Proof_Software			= 139,
		Vic_Tokai						= 140,
		i_Max							= 143,
		CHUN_Soft						= 145,
		Video_System_Co_Ltd				= 146,
		BEC								= 147,
		Kaneco							= 151,
		Pack_in_Video					= 153,
		Nichibutsu						= 154,
		TECMO_2							= 155,
		Imagineer_Co					= 156,
		Wolf_Team						= 160,
		Konami							= 164,
		k_Amusement						= 165,
		Takara							= 167,
		Technos_Jap						= 169,
		JVC								= 170,
		Toei_Animation					= 172,
		Toho							= 173,
		Namcot_Namco_Ltd_2				= 175,
		ASCII_Co_Activison				= 177,
		BanDai_America					= 178,
		Enix_2							= 180,
		Halken							= 182,
		SquareSoft						= 185,
		Culture_Brain					= 186,
		Sunsoft							= 187,
		Toshiba_EMI_System_Vision		= 188,
		Sony_Japan_Imagesoft			= 189,
		Sammy							= 191,
		Taito							= 192,
		Kemco_3							= 194,
		Square							= 195,
		NHK								= 196,
		Data_East						= 197,
		Tonkin_House					= 198,
		KOEI							= 200,
		Konami_USA						= 202,
		Meldac_KAZE						= 205,
		PONY_CANYON						= 206,
		Sotsu_Agency_1					= 207,
		Sofel							= 209,
		Quest_Corp						= 210,
		Sigma							= 211,
		Naxat							= 214,
		Capcom_Co_Ltd_2					= 216,
		Banpresto						= 217,
		Hiro							= 219,
		NCS								= 221,
		Human_Entertainment				= 222,
		Ringler_Studios					= 223,
		K_K_DCE_Jaleco					= 224,
		Sotsu_Agency_2					= 226,
		T_Esoft							= 228,
		EPOCH_Co_Ltd					= 229,
		Athena							= 231,
		Asmik							= 232,
		Natsume							= 233,
		King_A_Wave						= 234,
		Atlus							= 235,
		Sony_Music						= 236,
		Psygnosis_igs					= 238,
		Beam_Software					= 243,
		Tec_Magik						= 244,
		Hudson_Soft_2					= 255	
	}
}											  
