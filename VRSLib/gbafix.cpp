/**********************************************************************
*	gbafix.cpp																					  						  *
**********************************************************************/

/*
  API        : Corection du Header dans les Rom GBA (Game Boy Advance)
  Patch Par  : Khenshin	(DEREK TREMBLAY)
	Date       : Juillet 2001
	Réviser    : Février 2002
*/

/**********************************************************************
*	Pragmas																														  *
**********************************************************************/
#pragma pack(1)

/**********************************************************************
*	Include																														  *
**********************************************************************/
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <string.h>

/**********************************************************************
*	Typedefs																													  *
**********************************************************************/
typedef struct
{
	unsigned long	 start_code;		   // B instruction
	unsigned char	 logo[0xA0-0x04];
	char			     title[0xC];			 // 0xC (longueur 12 octets)
	unsigned long	 game_code;
	unsigned short maker_code;
	unsigned char	 fixed;			       // 0x96
	unsigned char	 unit_code;		     // 0x00
	unsigned char	 device_type;	     // 0x80
	unsigned char	 unused[7];
	unsigned char	 game_version;	   // 0x00
	unsigned char	 complement;		   // 800000A0..800000BC
	unsigned short checksum;
} Header;

/**********************************************************************
*	Variables 																												  *
**********************************************************************/
Header header;
bool patch = false;
bool patch_required = false;
unsigned short checksum_without_header = 0;

const Header good_header =
{
	//Start_code
		0xEA00002E,
	//Logo
		0x24,0xFF,0xAE,0x51,0x69,0x9A,0xA2,0x21,0x3D,0x84,0x82,0x0A,0x84,0xE4,0x09,0xAD,
		0x11,0x24,0x8B,0x98,0xC0,0x81,0x7F,0x21,0xA3,0x52,0xBE,0x19,0x93,0x09,0xCE,0x20,
		0x10,0x46,0x4A,0x4A,0xF8,0x27,0x31,0xEC,0x58,0xC7,0xE8,0x33,0x82,0xE3,0xCE,0xBF,
		0x85,0xF4,0xDF,0x94,0xCE,0x4B,0x09,0xC1,0x94,0x56,0x8A,0xC0,0x13,0x72,0xA7,0xFC,
		0x9F,0x84,0x4D,0x73,0xA3,0xCA,0x9A,0x61,0x58,0x97,0xA3,0x27,0xFC,0x03,0x98,0x76,
		0x23,0x1D,0xC7,0x61,0x03,0x04,0xAE,0x56,0xBF,0x38,0x84,0x00,0x40,0xA7,0x0E,0xFD,
		0xFF,0x52,0xFE,0x03,0x6F,0x95,0x30,0xF1,0x97,0xFB,0xC0,0x85,0x60,0xD6,0x80,0x25,
		0xA9,0x63,0xBE,0x03,0x01,0x4E,0x38,0xE2,0xF9,0xA2,0x34,0xFF,0xBB,0x3E,0x03,0x44,
		0x78,0x00,0x90,0xCB,0x88,0x11,0x3A,0x94,0x65,0xC0,0x7C,0x63,0x87,0xF0,0x3C,0xAF,
		0xD6,0x25,0xE4,0x8B,0x38,0x0A,0xAC,0x72,0x21,0xD4,0xF8,0x07,
	//Title
		0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
	//Game code
		0x00000000,
	//Maker code
		0x3130,
	//Fixed
		0x96,
	//Unit_code
		0x00,
	//Device type
		0x80,
	//Unused
		0x00,0x00,0x00,0x00,0x00,0x00,0x00,
	//Game version
		0x00,
	//Complement
		0x00,
	//Checksum
		0x0000,
};

/**********************************************************************
*	HeaderComplement  																								  *
**********************************************************************/
char HeaderComplement()
{
	//Variables
		char c = 0;
		char *p = (char *)&header + 0xA0;
	//Core
		for (int n=0; n<0xBD-0xA0; n++)
			c += *p++;
	//Valeur de retour	
		return -(0x19+c);
}

/**********************************************************************
*	HeaderChecksum 			        																			  *
**********************************************************************/	
unsigned short HeaderChecksum()
{
	//Variables
		unsigned short c = 0;
		unsigned char *p = (unsigned char *)&header;
	//Core
		for (int n=0; n<sizeof(header); n++)
			c += *p++;
	//Valeur de retour
		return -c;
}


/**********************************************************************
*	RepairGBArom  			        																			  *
**********************************************************************/
int _stdcall RepairGBArom	(char sfile[])
{		 
	//Variables
		unsigned char c;
		char s[256];
		char *begin=s;
		char *value;
		char *fin = NULL;
		unsigned long number = strtoul(value, &fin, 10);
	//Ouvre le Fichier ROM
		FILE *in = fopen(sfile, "r+b");
	//Detection des Erreurs a l'ouverture du fichier
		if (!in) return -2; //Fichier mal ouvert			
	//Initialise le Header
		fread(&header, sizeof(header), 1, in);
	//Prend le Checksum sans le Header
		while (1)
		{			 			
			if (!fread(&c, 1, 1, in))	break;
			checksum_without_header += c;		
		}																					
	//Assigne value
		fseek(in,160,0);
		fgets(value, 13, in);
	//Start Code
		header.start_code = 0x0000002E + (number & 0x01FFFFFF)/4 - 1;
  //Nom Interne de la Rom
		strncpy(header.title, value, sizeof(header.title));	// set
	//Game Code
		header.game_code = number;
	//Maker Code
		header.maker_code = (unsigned short)number;
	//Version du jeu
		header.game_version = (unsigned char)number;
	//Copy logo
		memcpy(header.logo, good_header.logo, sizeof(header.logo));
	//Update complement Check & Total checksum
		header.complement = (unsigned char)header.checksum; //= 0;
		header.complement = HeaderComplement();
		header.checksum = checksum_without_header + HeaderChecksum();
	//Patch Header
		fwrite(&header, sizeof(header), 1, in);
	//fermeture du fichier
	  fclose(in);	
	//Valeur de retour et fin de la fonction
		return 0;	//Tous sais bien passer
}