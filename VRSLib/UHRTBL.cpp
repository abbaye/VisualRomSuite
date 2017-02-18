/*
Objets : UHRTBL.H, UHRTBL.CPP
Programmé : Meradrin & Khenshin
Version : 1.98 Beta
Commentaire : Version conçu pour Extraction et Insertion.
			  Sous-norme TBL.
*/
using namespace std;
#include <fstream.h>
#include "uhrtbl.h"

TableTBL::TableTBL(char Chemin[])
{
	if(Chemin[0] != '\0')
		OuvrirTBL(Chemin);
}

void TableTBL::OuvrirTBL(char Chemin[])
{
	int PTBL8 = 0, PTBL16 = 0, x;
	char Temp[1024];

	ifstream FTBL;

	MaxTable8bit = Nombre8bit(Chemin);
	MaxTable16bit = Nombre16bit(Chemin);

	TBL8 = new Table8bit[MaxTable8bit];
	TBL16 = new Table16bit[MaxTable16bit];

	FTBL.open(Chemin);

	while(FTBL.getline(Temp,1023,'\n'))
	{
		if(Temp[2] == '=' && Temp[0] != '\0' && Temp[1] != '\0')
		{
			TBL8[PTBL8].Valeur = ConvHex(0,2,Temp);
			TBL8[PTBL8].Largeur = LargeurChaine(3,Temp);
			TBL8[PTBL8].Chaine = new char[TBL8[PTBL8].Largeur];
			
			for(x = 0; x < LargeurChaine(3,Temp); x++)
				TBL8[PTBL8].Chaine[x] = Temp[x + 3];
			
			PTBL8++;
		}

		if(Temp[4] == '=' && Temp[0] != '\0' && Temp[1] != '\0' && Temp[2] != '\0' && Temp[3] != '\0')
		{
			TBL16[PTBL16].Valeur = ConvHex(0,4,Temp);
			TBL16[PTBL16].Largeur = LargeurChaine(5,Temp);
			TBL16[PTBL16].Chaine = new char[TBL16[PTBL16].Largeur];
			
			for(x = 0; x < LargeurChaine(5,Temp); x++)
				TBL16[PTBL16].Chaine[x] = Temp[x + 5];
			
			PTBL16++;
		}

		switch(Temp[0])
		{
		case '*':
			for(x = 1; Temp[x] >= '0' && Temp[x] <= '9' || Temp[x] >= 'a' && Temp[x] <= 'f' || Temp[x] >= 'A' && Temp[x] <= 'F';x++);
			
			if(x == 3)
			{
				TBL8[PTBL8].Valeur = ConvHex(1,2,Temp);
				TBL8[PTBL8].Largeur = 3;
				TBL8[PTBL8].Chaine = new char[TBL8[PTBL8].Largeur];
				TBL8[PTBL8].Chaine = "\n";
				
				PTBL8++;
			}
			else
			{
				TBL16[PTBL16].Valeur = ConvHex(1,4,Temp);
				TBL16[PTBL16].Largeur = 3;
				TBL16[PTBL16].Chaine = new char[TBL16[PTBL16].Largeur = 3];
				TBL16[PTBL16].Chaine = "\n";
				
				PTBL16++;
			}

			break;
		case '/':
			for(x = 1; Temp[x] >= '0' && Temp[x] <= '9' || Temp[x] >= 'a' && Temp[x] <= 'f' || Temp[x] >= 'A' && Temp[x] <= 'F';x++);
						
			if(x == 3)
			{
				TBL8[PTBL8].Valeur = ConvHex(1,2,Temp);
				TBL8[PTBL8].Largeur = 10;
				TBL8[PTBL8].Chaine = new char[TBL8[PTBL8].Largeur];
				TBL8[PTBL8].Chaine = "<FIN>\n\n";
				
				PTBL8++;
			}
			else
			{
				TBL16[PTBL16].Valeur = ConvHex(1,4,Temp);
				TBL16[PTBL16].Largeur = 10;
				TBL16[PTBL16].Chaine = new char[TBL16[PTBL16].Largeur];
				TBL16[PTBL16].Chaine = "<FIN>\n\n";
				
				PTBL16++;
			}
			break;
		case '\\':
			for(x = 1; Temp[x] >= '0' && Temp[x] <= '9' || Temp[x] >= 'a' && Temp[x] <= 'f' || Temp[x] >= 'A' && Temp[x] <= 'F';x++);
						
			if(x == 3)
			{
				TBL8[PTBL8].Valeur = ConvHex(1,2,Temp);
				TBL8[PTBL8].Largeur = 7;
				TBL8[PTBL8].Chaine = new char[TBL8[PTBL8].Largeur];
				TBL8[PTBL8].Chaine = "<NP>\n";
				
				PTBL8++;
			}
			else
			{
				TBL16[PTBL16].Valeur = ConvHex(1,4,Temp);
				TBL16[PTBL16].Largeur = 7;
				TBL16[PTBL16].Chaine = new char[TBL16[PTBL16].Largeur];
				TBL16[PTBL16].Chaine = "<NP>\n";
				
				PTBL16++;
			}
			break;
		}
	}

	OuvrirTBLIns(Chemin);

	return;
}

void TableTBL::FermerTBL(void)
{
	delete [] TBL8;
	delete [] TBL16;
	delete [] TBL8Ins;
	delete [] TBL16Ins;

	return;
}

int TableTBL::VerificationExt(unsigned char Valeur[], int Position)
{
	int Plus = 0, x;
	unsigned short TempTotal16bit = (Valeur[Position] * 256)  + Valeur[Position + 1];
	unsigned char TempTotal8bit = Valeur[Position];
	char Val[2] = {TempTotal8bit / 16,TempTotal8bit % 16};
	
	for(x = 0; x < MaxTable16bit; x++)
		if(TempTotal16bit == TBL16[x].Valeur)
		{
			Retour = TBL16[x].Chaine;
			Plus = 2;
		}
	
	if(Plus != 2)
		for(x = 0; x < MaxTable8bit; x++)
			if(TempTotal8bit == TBL8[x].Valeur)
			{
				Retour = TBL8[x].Chaine;
				Plus = 1;
			}
	
	if(Plus == 0)
	{
		Plus = 1;
		
		Inconnu[0] = '<';
				
		if(Val[0] < 10)
			Inconnu[1] = '0' + Val[0];
		else
			Inconnu[1] = 'A' + (Val[0]-10);
		
		if(Val[1] < 10)
			Inconnu[2] = '0' + Val[1];
		else
			Inconnu[2] = 'A' + (Val[1]-10);
		
		Inconnu[3] = '>';
		Inconnu[4] = '\0';
		
		Retour = Inconnu;
	}

	return Plus;
}

int TableTBL::Nombre8bit(char Chemin[])
{
	char Temp[1024];
	int Nombre = 0;

	ifstream FTBL;

	FTBL.open(Chemin);
	
	while(FTBL.getline(Temp,99,'\n'))
	{
		if(Temp[2] == '=' && Temp[0] != '\0' && Temp[1] != '\0')
			Nombre++;

		switch(Temp[0])
		{
		case '\\':
		case '/':
		case '*':
			for(int x = 1; Temp[x] >= '0' && Temp[x] <= '9' || Temp[x] >= 'a' && Temp[x] <= 'f' || Temp[x] >= 'A' && Temp[x] <= 'F';x++);
			
			if(x == 3)
				Nombre++;
		}

	}


	FTBL.close();

	return Nombre;
}

int TableTBL::Nombre16bit(char Chemin[])
{
	char Temp[1024];
	int Nombre = 0;

	ifstream FTBL;

	FTBL.open(Chemin);
	
	while(FTBL.getline(Temp,99,'\n'))
	{
		if(Temp[4] == '=' && Temp[0] != '\0' && Temp[1] != '\0' && Temp[2] != '\0' && Temp[3] != '\0' )
			Nombre++;

		switch(Temp[0])
		{
		case '\\':
		case '/':
		case '*':
			for(int x = 1; Temp[x] >= '0' && Temp[x] <= '9' || Temp[x] >= 'a' && Temp[x] <= 'f' || Temp[x] >= 'A' && Temp[x] <= 'F';x++);
			
			if(x == 5)
				Nombre++;
		}
	}

	FTBL.close();

	return Nombre;
}

int TableTBL::LongeurChaine(int Position, char Chaine[])
{
	for(int x = Position; Chaine[x] != '\0'; x++);

	return x;
}

int TableTBL::ConvHex(int Position, int Largeur, char Chaine[])
{
	int Total = 0;

	for(int x = 0; x < Largeur; x++)
	{
		if(Chaine[Position + x] >= '0' && Chaine[Position + x] <= '9')
			Total = Total + ((Chaine[Position+x] - '0') * Exposant(16,Largeur - x - 1));

		if(Chaine[Position + x] >= 'a' && Chaine[Position + x] <= 'z')
			Total = Total + ((Chaine[Position+x] - 'a' + 10) * Exposant(16,Largeur - x - 1));

		if(Chaine[Position + x] >= 'A' && Chaine[Position + x] <= 'Z')
			Total = Total + ((Chaine[Position+x] - 'A' + 10) * Exposant(16,Largeur - x - 1));
	}

	return Total;
}

int TableTBL::Exposant(int x, int y)
{
	int Total = 1;

	for(int Tour = 1; Tour <= y; Tour++)
		Total = Total * x;

	return Total;
}

int TableTBL::LargeurChaine(int Position, char Chaine[])
{
	for(int Largeur = 0; Chaine[Position+Largeur] != '\0'; Largeur++);

	return Largeur + 1;
}

void TableTBL::TrieTable(void)
{
	char *Chaine;
	unsigned short Valeur16bit;
	unsigned char Valeur8bit;
	int Largeur, x, y;

	for(x = 0; x < MaxTable8bitIns - 1; x++)
		for(y = 0; y < MaxTable8bitIns - 1; y++)
			if(TBL8Ins[y].Largeur < TBL8Ins[y+1].Largeur)
			{
				Largeur = TBL8Ins[y].Largeur;
				TBL8Ins[y].Largeur = TBL8Ins[y+1].Largeur;
				TBL8Ins[y+1].Largeur = Largeur;
				Chaine = TBL8Ins[y].Chaine;
				TBL8Ins[y].Chaine = TBL8Ins[y+1].Chaine;
				TBL8Ins[y+1].Chaine = Chaine;
				Valeur8bit = TBL8Ins[y].Valeur;
				TBL8Ins[y].Valeur = TBL8Ins[y+1].Valeur;
				TBL8Ins[y+1].Valeur = Valeur8bit;
			}

	for(x = 0; x < MaxTable16bitIns - 1; x++)
		for(y = 0; y < MaxTable16bitIns - 1; y++)
			if(TBL16Ins[y].Largeur < TBL16Ins[y+1].Largeur)
			{
				Largeur = TBL16Ins[y].Largeur;
				TBL16Ins[y].Largeur = TBL16Ins[y+1].Largeur;
				TBL16Ins[y+1].Largeur = Largeur;
				Chaine = TBL16Ins[y].Chaine;
				TBL16Ins[y].Chaine = TBL16Ins[y+1].Chaine;
				TBL16Ins[y+1].Chaine = Chaine;
				Valeur16bit = TBL16Ins[y].Valeur;
				TBL16Ins[y].Valeur = TBL16Ins[y+1].Valeur;
				TBL16Ins[y+1].Valeur = Valeur16bit;
			}
	return;
}

void TableTBL::OuvrirTBLIns(char Chemin[])
{
	int PTBL8 = 0, PTBL16 = 0, x;
	char Temp[1024];

	ifstream FTBL;

	MaxTable8bitIns = Nombre8bit(Chemin);
	MaxTable16bitIns = Nombre16bit(Chemin);

	TBL8Ins = new Table8bit[MaxTable8bitIns];
	TBL16Ins = new Table16bit[MaxTable16bitIns];

	FTBL.open(Chemin);

	while(FTBL.getline(Temp,1023,'\n'))
	{
		if(Temp[2] == '=' && Temp[0] != '\0' && Temp[1] != '\0')
		{
			TBL8Ins[PTBL8].Valeur = ConvHex(0,2,Temp);
			TBL8Ins[PTBL8].Largeur = LargeurChaine(3,Temp);
			TBL8Ins[PTBL8].Chaine = new char[TBL8Ins[PTBL8].Largeur];
			
			for(x = 0; x < LargeurChaine(3,Temp); x++)
				TBL8Ins[PTBL8].Chaine[x] = Temp[x + 3];
			
			PTBL8++;
		}

		if(Temp[4] == '=' && Temp[0] != '\0' && Temp[1] != '\0' && Temp[2] != '\0' && Temp[3] != '\0')
		{
			TBL16Ins[PTBL16].Valeur = ConvHex(0,4,Temp);
			TBL16Ins[PTBL16].Largeur = LargeurChaine(5,Temp);
			TBL16Ins[PTBL16].Chaine = new char[TBL16Ins[PTBL16].Largeur];
			
			for(x = 0; x < LargeurChaine(5,Temp); x++)
				TBL16Ins[PTBL16].Chaine[x] = Temp[x + 5];
			
			PTBL16++;
		}

		switch(Temp[0])
		{
		case '*':
			for(x = 1; Temp[x] >= '0' && Temp[x] <= '9' || Temp[x] >= 'a' && Temp[x] <= 'f' || Temp[x] >= 'A' && Temp[x] <= 'F';x++);
			
			if(x == 3)
			{
				TBL8Ins[PTBL8].Valeur = ConvHex(1,2,Temp);
				TBL8Ins[PTBL8].Largeur = 3;
				TBL8Ins[PTBL8].Chaine = new char[TBL8Ins[PTBL8].Largeur];
				TBL8Ins[PTBL8].Chaine[0] = 13;
				TBL8Ins[PTBL8].Chaine[1] = 10;
				TBL8Ins[PTBL8].Chaine[2] = '\0';
				
				PTBL8++;
			}
			else
			{
				TBL16Ins[PTBL16].Valeur = ConvHex(1,4,Temp);
				TBL16Ins[PTBL16].Largeur = 3;
				TBL16Ins[PTBL16].Chaine = new char[TBL16Ins[PTBL16].Largeur = 3];
				TBL16Ins[PTBL16].Chaine[0] = 13;
				TBL16Ins[PTBL16].Chaine[1] = 10;
				TBL16Ins[PTBL16].Chaine[2] = '\0';
				
				PTBL16++;
			}

			break;
		case '/':
			for(x = 1; Temp[x] >= '0' && Temp[x] <= '9' || Temp[x] >= 'a' && Temp[x] <= 'f' || Temp[x] >= 'A' && Temp[x] <= 'F';x++);
						
			if(x == 3)
			{
				TBL8Ins[PTBL8].Valeur = ConvHex(1,2,Temp);
				TBL8Ins[PTBL8].Largeur = 10;
				TBL8Ins[PTBL8].Chaine = new char[TBL8Ins[PTBL8].Largeur];
				TBL8Ins[PTBL8].Chaine[0] = '<';
				TBL8Ins[PTBL8].Chaine[1] = 'F';
				TBL8Ins[PTBL8].Chaine[2] = 'I';
				TBL8Ins[PTBL8].Chaine[3] = 'N';
				TBL8Ins[PTBL8].Chaine[4] = '>';
				TBL8Ins[PTBL8].Chaine[5] = 13;
				TBL8Ins[PTBL8].Chaine[6] = 10;
				TBL8Ins[PTBL8].Chaine[7] = 13;
				TBL8Ins[PTBL8].Chaine[8] = 10;
				TBL8Ins[PTBL8].Chaine[9] = '\0';
				
				PTBL8++;
			}
			else
			{
				TBL16Ins[PTBL16].Valeur = ConvHex(1,4,Temp);
				TBL16Ins[PTBL16].Largeur = 10;
				TBL16Ins[PTBL16].Chaine = new char[TBL16Ins[PTBL16].Largeur];
				TBL16Ins[PTBL16].Chaine[0] = '<';
				TBL16Ins[PTBL16].Chaine[1] = 'F';
				TBL16Ins[PTBL16].Chaine[2] = 'I';
				TBL16Ins[PTBL16].Chaine[3] = 'N';
				TBL16Ins[PTBL16].Chaine[4] = '>';
				TBL16Ins[PTBL16].Chaine[5] = 13;
				TBL16Ins[PTBL16].Chaine[6] = 10;
				TBL16Ins[PTBL16].Chaine[7] = 13;
				TBL16Ins[PTBL16].Chaine[8] = 10;
				TBL16Ins[PTBL16].Chaine[9] = '\0';				
				
				PTBL16++;
			}
			break;
		case '\\':
			for(x = 1; Temp[x] >= '0' && Temp[x] <= '9' || Temp[x] >= 'a' && Temp[x] <= 'f' || Temp[x] >= 'A' && Temp[x] <= 'F';x++);
						
			if(x == 3)
			{
				TBL8Ins[PTBL8].Valeur = ConvHex(1,2,Temp);
				TBL8Ins[PTBL8].Largeur = 7;
				TBL8Ins[PTBL8].Chaine = new char[TBL8Ins[PTBL8].Largeur];
				TBL8Ins[PTBL8].Chaine[0] = '<';
				TBL8Ins[PTBL8].Chaine[1] = 'N';
				TBL8Ins[PTBL8].Chaine[2] = 'P';
				TBL8Ins[PTBL8].Chaine[3] = '>';
				TBL8Ins[PTBL8].Chaine[4] = 13;
				TBL8Ins[PTBL8].Chaine[5] = 10;
				TBL8Ins[PTBL8].Chaine[6] = '\0';
				
				PTBL8++;
			}
			else
			{
				TBL16Ins[PTBL16].Valeur = ConvHex(1,4,Temp);
				TBL16Ins[PTBL16].Largeur = 7;
				TBL16Ins[PTBL16].Chaine = new char[TBL16Ins[PTBL16].Largeur];
				TBL16Ins[PTBL16].Chaine[0] = '<';
				TBL16Ins[PTBL16].Chaine[1] = 'N';
				TBL16Ins[PTBL16].Chaine[2] = 'P';
				TBL16Ins[PTBL16].Chaine[3] = '>';
				TBL16Ins[PTBL16].Chaine[4] = 13;
				TBL16Ins[PTBL16].Chaine[5] = 10;
				TBL16Ins[PTBL16].Chaine[6] = '\0';
								
				PTBL16++;
			}
			break;
		}
	}

	TrieTable();
		
	return;
}

int TableTBL::VerificationIns(char Texte[], int Position)
{
	int Plus = 0, x;
	
	if(Texte[Position] == '<' &&
	   ((Texte[Position + 1] >= '0' && Texte[Position + 1] <= '9')
	   || (Texte[Position + 1] >= 'A' && Texte[Position + 1] <= 'F')) &&
	   ((Texte[Position + 2] >= '0' && Texte[Position + 2] <= '9')
	   || (Texte[Position + 2] >= 'A' && Texte[Position + 2] <= 'F')) &&
	   Texte[Position + 3] == '>')
	{
		Plus = 4;
		RetourIns[0] = ConvHex(Position + 1,2,Texte);
		RetourIns[1] = 0;
		RetourIns[2] = 1;
	}
	
	if(Plus == 0)
		for(x = 0; x < MaxTable16bitIns && Plus == 0; x++)
			if(CompareChaine(Texte,Position,TBL16Ins[x].Chaine,0,TBL16Ins[x].Largeur - 1) == true)
			{
				RetourIns[0] = TBL16Ins[x].Valeur / 256;
				RetourIns[1] = TBL16Ins[x].Valeur % 256;
				RetourIns[2] = 0;
				Plus = TBL16Ins[x].Largeur - 1;
			}
	
	if(Plus == 0)
		for(x = 0; x < MaxTable8bitIns && Plus == 0; x++)
			if(CompareChaine(Texte,Position,TBL8Ins[x].Chaine,0,TBL8Ins[x].Largeur - 1) == true)
			{
				RetourIns[0] = TBL8Ins[x].Valeur;
				RetourIns[1] = 0;
				RetourIns[2] = 1;
				Plus = TBL8Ins[x].Largeur - 1;
			}
	
	if(Plus == 0)
		Plus = 1;
	
	return Plus;
}

bool TableTBL::CompareChaine(char Chaine1[], int Position1, char Chaine2[], int Position2, int Longeur)
{
	bool Ok = true;
	
	for(int x = 0; x < Longeur && Ok == true; x++)
		if(Chaine1[Position1 + x] != Chaine2[Position2 + x])
			Ok = false;

	return Ok;
}
