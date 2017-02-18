/**********************************************************************
*	INCLUDE														
**********************************************************************/
#include <fstream.h>
#include <stdlib.h>
#include "uhrtbl.h"
#include "dte.h"

/**********************************************************************
*	PROTOTYPE														
**********************************************************************/
struct        TableauPointeur;
void _stdcall ExtractionSimplePointeur(int, char[], char[], char[], int, int, int, int, int, int);
int  _stdcall InsertionSimplePointeur(int, char[], char[], char[], int, int, int, int, int, int);
void          ChargerTexte(unsigned char *, int[2], char[], int);
void          ChargementPointeur(TableauPointeur *,int, int[2], char[]);
void          TriePointeur(TableauPointeur[], int);
bool          VerificationPT(int, TableauPointeur[], int, int, int[2]);
void          AjustementTablePointeur(int, int, int, unsigned char[]);
int           ConvPT(int , int);
int           ConvNumTexte(char [], int);

/**********************************************************************
*	STRUCTURE														
**********************************************************************/
struct TableauPointeur
{
	unsigned int Pointeur;
	int Numero;
};

/**********************************************************************
*	ExtractionSimplePointeur									
**********************************************************************/		 
void _stdcall ExtractionSimplePointeur(int Type, char PathTBL[], char PathROM[], char PathTEXTE[], int DebutTexte, int FinTexte, int DebutPointeur, int FinPointeur, int AjustementPlus, int AjustementMoin)
{
	unsigned char *ChargementTexte;
	unsigned int Position = 0;
	int PositionTableauPointeur[2] = {DebutPointeur, FinPointeur}, 
		PositionTexte[2] = {DebutTexte, FinTexte},
		Ajustement[2] = {AjustementPlus, AjustementMoin},
		NumeroPT = 0,
		MaxTexte,
		NombrePointeur;
	TableauPointeur	*TablePointeur;
		
	ofstream Texte;
	
	// Chargement du textes
	MaxTexte = (unsigned int) (PositionTexte[1] - PositionTexte[0] + 1);
	ChargementTexte = new unsigned char[MaxTexte + 1];
	ChargerTexte(ChargementTexte,PositionTexte,PathROM,MaxTexte);

	// Chargement du tableau de pointeur
	switch(Type)
	 {
		case 1:
		case 2:
			NombrePointeur = (PositionTableauPointeur[1] - PositionTableauPointeur[0] + 1) / 2;
			TablePointeur = new TableauPointeur[NombrePointeur];
			break;
		case 3:
		case 4:
		case 5:
			NombrePointeur = (PositionTableauPointeur[1] - PositionTableauPointeur[0] + 1) / 3;
			TablePointeur = new TableauPointeur[NombrePointeur];
			break;
	 }

	if(Type != 0)
		ChargementPointeur(TablePointeur,Type,PositionTableauPointeur,PathROM);
	
	// Ouverture de la table
	TableTBL Table(PathTBL);
	
	Texte.open(PathTEXTE,ios::trunc);
			
	while(Position < ((unsigned int) MaxTexte))
	{
		if(Type != 0)
			while(VerificationPT(Position + PositionTexte[0],TablePointeur,NumeroPT,Type,Ajustement) == true)
				Texte << "<PT" 
					  << (TablePointeur[NumeroPT].Numero + 1 < 1000 ? "0" : "")
					  << (TablePointeur[NumeroPT].Numero + 1 < 100 ? "0" : "") 
					  << (TablePointeur[NumeroPT].Numero + 1 < 10 ? "0" : "")
					  << TablePointeur[NumeroPT++].Numero + 1 << ">" << endl;
		
		Position = Position + Table.VerificationExt(ChargementTexte, Position);
		Texte << Table.Retour;
	}

	Texte.close();
	
	Table.FermerTBL();

	delete [] ChargementTexte;
	
	if(Type != 0)
		delete [] TablePointeur;

	return;
}

/**********************************************************************
*	ChargerTexte   												  
**********************************************************************/
void ChargerTexte(unsigned char *Texte, int Position[2], char PathROM[], int Max)
{
	
	ifstream FichierROM;
	
	FichierROM.open(PathROM,ios::binary);
	
	FichierROM.seekg(Position[0]);
	FichierROM.read(Texte, Max);

	FichierROM.close();

	Texte[Max] = '\0';

	return;
}

/**********************************************************************
*	ChargementPointeur												
**********************************************************************/
void ChargementPointeur(TableauPointeur *TablePointeur,int Type, int Position[2], char PathROM[])
{
	int NombrePointeur, x;
	
	ifstream FichierROM(PathROM, ios::binary);

	switch(Type)
	{
	case 1:
	case 2:
		NombrePointeur = (Position[1] - Position[0] + 1) / 2;

		for(x = 0; x < NombrePointeur; x++)
		{
			FichierROM.seekg(Position[0] + (x*2));
			FichierROM.read(reinterpret_cast<char*>(&TablePointeur[x].Pointeur),4);
			TablePointeur[x].Pointeur = TablePointeur[x].Pointeur % 65536;
			TablePointeur[x].Numero = x;
		} 
		break;
	case 3:
	case 4:
	case 5:
		NombrePointeur = (Position[1] - Position[0] + 1) / 3;

		for(x = 0; x < NombrePointeur; x++)
		{
			FichierROM.seekg(Position[0] + (x*3));
			FichierROM.read(reinterpret_cast<char*>(&TablePointeur[x].Pointeur),4);
			TablePointeur[x].Pointeur = TablePointeur[x].Pointeur % 16777216;
			TablePointeur[x].Numero = x;
		}
		break;
	}

	TriePointeur(TablePointeur,NombrePointeur);

	FichierROM.close();

	return;
}

/**********************************************************************
*	TriePointeur  											
**********************************************************************/
void TriePointeur(TableauPointeur TablePointeur[], int NombrePointeur)
{
	int Temp;

	for(int x = 0; x < NombrePointeur - 1; x++)
		for(int y = 0; y < NombrePointeur - 1; y++)
			if(TablePointeur[y].Pointeur > TablePointeur[y + 1].Pointeur)
			{
				Temp = TablePointeur[y].Pointeur;
				TablePointeur[y].Pointeur = TablePointeur[y + 1].Pointeur;
				TablePointeur[y + 1].Pointeur = Temp;
				Temp = TablePointeur[y].Numero;
				TablePointeur[y].Numero = TablePointeur[y + 1].Numero;
				TablePointeur[y + 1].Numero = Temp;
			}

	return;
}

/**********************************************************************
*	VerificationPT												
**********************************************************************/
bool VerificationPT(int Position, TableauPointeur TablePointeur[], int NumeroPT, int Type, int Ajustement[2])
{ 
	bool Pointeur = false;

	switch(Type)
	{
	case 1:
		if((((Position - ((unsigned int) (Ajustement[0] - Ajustement[1]))) % 0x8000) + 0x8000) == TablePointeur[NumeroPT].Pointeur)
			Pointeur = true;
		break;
	case 2:
		if((Position - ((unsigned int) (Ajustement[0] - Ajustement[1]))) % 0x10000 == TablePointeur[NumeroPT].Pointeur)
			Pointeur = true;
		break;
	case 3:
		if((((Position - ((unsigned int) (Ajustement[0] - Ajustement[1]))) / 0x8000) * 0x10000) + (((Position - ((unsigned int) (Ajustement[0] + Ajustement[1]))) % 0x8000) + 0x8000) == TablePointeur[NumeroPT].Pointeur)
			Pointeur = true;
		break;
	case 4:
		if((Position - ((unsigned int) (Ajustement[0] - Ajustement[1])) + 0xC00000) == TablePointeur[NumeroPT].Pointeur)
			Pointeur = true;
		break;
	case 5:
		if(0x800000 + (((Position - ((unsigned int) (Ajustement[0] - Ajustement[1]))) / 0x8000) * 0x10000) + (((Position - ((unsigned int) (Ajustement[0] - Ajustement[1]))) % 0x8000) + 0x8000) == TablePointeur[NumeroPT].Pointeur)
			Pointeur = true;
		break;
	}

	return Pointeur;
}

/**********************************************************************
*	InsertionSimplePointeur								  		
**********************************************************************/
int _stdcall InsertionSimplePointeur(int Type,
							 char PathTBL[], char PathROM[], char PathTEXTE[],
							 int DebutTexte, int FinTexte,
							 int DebutPointeur, int FinPointeur,
							 int AjustementPlus, int AjustementMoin)
{
	char *Texte;
	unsigned char *Donner, *TableauPointeur;
	int Position = 0,
		PositionDonner = 0,
		TailleFichier,
		erreur = 0;

	ifstream FichierTexte;
	ofstream FichierRom;

	//Chargement du texte en mémoire
	FichierTexte.open(PathTEXTE, ios::binary);

	FichierTexte.seekg(0,ios::end);
	TailleFichier = FichierTexte.tellg() + 1;
	Texte = new char[TailleFichier];
	
	FichierTexte.seekg(0,ios::beg);
	FichierTexte.read(Texte,TailleFichier);
	
	Texte[TailleFichier-1] = '\0';
	
	FichierTexte.close();

	//Ajustement des variables
	Donner = new unsigned char[FinTexte - DebutTexte + 1];
		
	if(Type != 0)
		TableauPointeur = new unsigned char[FinPointeur - DebutPointeur + 1];
				
	TableTBL Table(PathTBL);

	while(Position < TailleFichier && PositionDonner < (FinTexte - DebutTexte + 1))
	{
		if(Texte[Position] == '<' &&
		   Texte[Position + 1] == 'P' &&
		   Texte[Position + 2] == 'T' &&
		   (Texte[Position + 3] >= '0' && Texte[Position + 3] <= '9') &&
		   (Texte[Position + 4] >= '0' && Texte[Position + 4] <= '9') &&
		   (Texte[Position + 5] >= '0' && Texte[Position + 5] <= '9') &&
		   (Texte[Position + 6] >= '0' && Texte[Position + 6] <= '9') &&
		   Texte[Position + 7] >= '>')
		{
			AjustementTablePointeur(Type,ConvNumTexte(Texte,Position),PositionDonner + DebutTexte + AjustementPlus - AjustementMoin, TableauPointeur);
			Position = Position + 10;						
		}
		else
		{
			Position = Position + Table.VerificationIns(Texte, Position);
		
			if(Table.RetourIns[2] == 0)
			{
				Donner[PositionDonner++] = Table.RetourIns[0];
				Donner[PositionDonner++] = Table.RetourIns[1];
			}
			else
				Donner[PositionDonner++] = Table.RetourIns[0];
		}
	
	}

	FichierRom.open(PathROM,ios::binary | ios::ate);

	FichierRom.seekp(DebutTexte);
	FichierRom.write(Donner,FinTexte - DebutTexte + 1);

	FichierRom.seekp(DebutPointeur);
	FichierRom.write(TableauPointeur,FinPointeur - DebutPointeur + 1);

	FichierRom.close();

	Table.FermerTBL();

	if(PositionDonner >= (FinTexte - DebutTexte + 1))
		erreur = 1;
		
	delete [] Texte;
	delete [] Donner;

	if(Type != 0)
		delete [] TableauPointeur;
	
	return erreur;
}

/**********************************************************************
*	AjustementTablePointeur								  		
**********************************************************************/
void AjustementTablePointeur(int type, int num, int position,
							 unsigned char TBPointeur[])
{
	int coder,
		temppt;

	num = num - 1;
	
	temppt = ConvPT(type,position);

	if(type == 1 || type == 2)
	{
		coder = 2;
		TBPointeur[coder * num] = temppt % 256;
		TBPointeur[(coder * num) + 1] = temppt / 256;
	}
	else
	{
		coder = 3;
		TBPointeur[coder * num] = temppt % 256;
		TBPointeur[(coder * num) + 1] = (temppt / 256) % 256;
		TBPointeur[(coder * num) + 2] = temppt / 65536;
	}

	return;
}

/**********************************************************************
*	ConvPT														
**********************************************************************/
int ConvPT(int type,int position)
{
	switch(type)
	{
	case 1:
		position = position % 0x8000 + 0x8000;
		break;
	case 2:
		position = position % 0x10000;
		break;
	case 3:
		position = ((position / 0x8000) * 0x10000) + ((position  % 0x8000) + 0x8000);
		break;
	case 4:
		position = position + 0xc00000;
		break;
	case 5:
		position = ((position / 0x8000) * 0x10000) + ((position  % 0x8000) + 0x8000) + 0x800000;
		break;
	}

	return position;
}

/**********************************************************************
*	ConvNumTexte											
**********************************************************************/
int ConvNumTexte(char Texte[],int Position)
{
	int num;

	num = Texte[Position + 6] - '0';
	num = num + ((Texte[Position + 5] - '0') * 10);
	num = num + ((Texte[Position + 4] - '0') * 100);
	num = num + ((Texte[Position + 3] - '0') * 1000);

	return num;
}