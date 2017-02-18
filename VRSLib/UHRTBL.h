/*
Objets : UHRTBL.H, UHRTBL.CPP
Programmé : Meradrin
Modifier par : Khenshin
Version : 1.98 Beta
Commentaire : Version conçu pour Extraction et Insertion.
			        Sous-norme TBL.Standard
*/

#ifndef UHRTBL_H
#define UHRTBL_H

struct Table8bit
{
	unsigned char Valeur;
	char *Chaine;
	int Largeur;
};

struct Table16bit
{
	unsigned short Valeur;
	char *Chaine;
	int Largeur;
};

class TableTBL
{
public:
	TableTBL(char [] = "");

	void OuvrirTBL(char[]);
	void FermerTBL(void);
	int VerificationExt(unsigned char[], int);
	int VerificationIns(char[], int);

	char *Retour;
	char RetourIns[3];
		
private:
	int Nombre8bit(char[]);
	int Nombre16bit(char[]);
	int LongeurChaine(int, char[]);
	int ConvHex(int, int, char[]);
	int Exposant(int, int);
	int LargeurChaine(int, char[]);
	void OuvrirTBLIns(char []);
	void TrieTable(void);
	bool CompareChaine(char[], int, char[], int, int);

	Table8bit *TBL8;
	Table16bit *TBL16;
	Table8bit *TBL8Ins;
	Table16bit *TBL16Ins;
	int MaxTable8bit;
	int MaxTable16bit;
	int MaxTable8bitIns;
	int MaxTable16bitIns;
	char Inconnu[6];
};

#endif
