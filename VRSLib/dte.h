/* Nouvelle implementation de classe de DTE Search -
Programmé par Crispysix - crispysix@wanadoo.fr
Si vous voulez utiliser ces classes dans un programme
envoyez moi un mail avant svp */

#ifndef __DTE_Search_
#define __DTE_Search_

#include <iostream.h>
#include <ctype.h>
#include <string.h>
#include <fstream.h>
#include <strstrea.h>

#define EXISTE -1
#define NB_RESULTATS 100
#define LIM_SEGMENT 4000

/* Classe de base contenant la DTE source */
class DTEType{

	friend ostream& operator<<(ostream&,const DTEType&);

private:

	char * texte;
	unsigned char taille;
	unsigned short occurences;

public:

	/* Constructeur par defaut en ligne, plus stable pour l'utilisation de
	l'operateur new par la suite */

	DTEType() 
	{
		texte=new char;
	}
	explicit DTEType(const char *);
	DTEType(const char *,unsigned char);
	DTEType(const DTEType&);
	~DTEType();
	DTEType& operator=(const DTEType&);
	/* Fonction d'acces en ligne */
	unsigned short NombreOccurences() const
	{
		return occurences;
	}
	unsigned Taille() const
	{
		return (unsigned)taille;
	}
	const char * DTE() const
	{
		return texte;
	}
	unsigned short inc()
	{
		return occurences++;
	}
};




/* Cette classe est déstiné a contenir un tableau de DTE 
pour un nombre d'octets que l'on aura prealablement choisi */

class DTEVector{

	friend ostream& operator<<(ostream&,const DTEVector&);

private:

	DTEType * Ensemble_DTE;
	unsigned Taille_DTE;
	unsigned Nombre_DTE;
	bool Existe(const char *) const;

public:

	DTEVector(const char *,unsigned);
	~DTEVector();
	int Ajouter(const char *);
	DTEType operator[](unsigned) const;
	void Trier();
	/* Fonctions d'acces en ligne */
	unsigned NombreDeDTE() const
	{
		return Nombre_DTE;
	}
	unsigned TailleDesDTE() const
	{
		return Taille_DTE;
	}

};

/*Fonctions permettant de trouver les meilleures combinaisons de DTE
Va mettre les plus frequentes occurences dans un fichier */


int _stdcall TrouverDTE(char [],char [],int);

bool TrouverDTEFichierSeulement(const char *,const char *,unsigned);

void OptimiserTexte();

#endif