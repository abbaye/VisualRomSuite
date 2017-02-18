/********************************************************************** 
*	Description : Implementation des fonctions dans le fichier DTE.CPP
*	  			  des classes DTEType et DTEVector Pour UHR
*																																			*
*	Cree par     : Crispysix			
*	Date				 : MAI 2001						
*	modifier par : Meradrin	(ALEXANDRE LALANCETTE)	
*	Date				 : 21 Juin 2001			
*	modifier par : Khenshin (DEREK TREMBLAY)	
*	Date				 : 22 Juin 2001							
**********************************************************************/

/**********************************************************************
*	INCLUDE																														  *
**********************************************************************/
#include "dte.h"																											 
																																			 
/**********************************************************************
*	CLASSE: DTETYPE													
**********************************************************************/

/* Implementation des fonctions membres de la classe DTEType */

DTEType::DTEType(const char * chaine):
occurences(1),taille((unsigned char)strlen(chaine))
{

	texte = new char[taille+1];
	strcpy(texte,chaine);

}

DTEType::DTEType(const char * chaine,unsigned char _taille):
occurences(1),taille((unsigned char)_taille)
{

	texte = new char[taille+1];
	strcpy(texte,chaine);

}

DTEType::DTEType(const DTEType& copie):
taille(copie.taille)
{

	texte = new char[taille+1];
	strcpy(texte,copie.texte);

}

DTEType::~DTEType()
{

	delete [] texte;

}

DTEType& DTEType::operator=(const DTEType &r)
{

	delete [] texte;
	texte = new char[r.taille+1];
	strcpy(texte,r.texte);
	taille = r.taille;
	occurences = r.occurences;
	return * this;

}

/**********************************************************************
*	OPERATEUR DE SORTI DE FLUX <<									  *
**********************************************************************/
ostream& operator<<(ostream&f,const DTEType &r)
{

	//f << '\"' << r.DTE() << "\" est répété " << r.NombreOccurences() << " fois";
	f << '\"' << r.DTE() << "\" = " << r.NombreOccurences();
	return f;

}

/**********************************************************************
*	CLASSE: DTEVECTOR												  *
**********************************************************************/

/* Implementation des fonctions membres de la classe DTEVector */	

DTEVector::DTEVector(const char * chaine,unsigned _taille):
Taille_DTE(_taille),Nombre_DTE(1)
{

	Ensemble_DTE = new DTEType[1];
	Ensemble_DTE[0] = DTEType(chaine,(unsigned char)_taille);

}

DTEVector::~DTEVector()
{

	delete [] Ensemble_DTE;

}

bool DTEVector::Existe(const char * chaine) const
{
	for(int i = 0;i < Nombre_DTE;i++)
		if(!strcmp(chaine,Ensemble_DTE[i].DTE()))
		{

			Ensemble_DTE[i].inc();
			return true;
			break;
		}

	return false;

}

int DTEVector::Ajouter(const char * chaine)
{
	int i;
	for(i = 0;i<Taille_DTE;i++)
		if(!isprint(chaine[i]) || chaine[i]=='<' || chaine[i]=='>') 
			return i + 1;
	if(this->Existe(chaine)) 
		return EXISTE;
	//if((Nombre_DTE) == LIM_SEGMENT) return 0;
	DTEType * Temporaire = new DTEType[Nombre_DTE];
	for(i = 0;i<Nombre_DTE;i++)
		Temporaire[i] = Ensemble_DTE[i];
	delete [] Ensemble_DTE;
	Ensemble_DTE = new DTEType[Nombre_DTE+1];
	for(i=0;i < Nombre_DTE;i++)
		Ensemble_DTE[i] = Temporaire[i];
	Ensemble_DTE[Nombre_DTE] = DTEType(chaine);
	Nombre_DTE++;
	delete [] Temporaire;
	return 0;

}

DTEType DTEVector::operator [](unsigned numero) const
{

	if(numero < Nombre_DTE)
	return Ensemble_DTE[numero];
	return static_cast<DTEType> (NULL);

}

/**********************************************************************
*	VECTEUR: TRIER									
**********************************************************************/

void DTEVector::Trier()
{
 int Pointeur, Base, Pivot;
 DTEType EltTemporaire;
 for(Pivot = 1;Pivot <= Nombre_DTE;Pivot = (3*Pivot)+1);
	for(;Pivot>0;Pivot/=3)
		{

			 for(Pointeur = Pivot;Pointeur < Nombre_DTE; Pointeur++)
				{

					Base = Pointeur-Pivot;
					EltTemporaire = Ensemble_DTE[Pointeur];
					while((Ensemble_DTE[Base].NombreOccurences() > EltTemporaire.NombreOccurences()) && (Base>=Pivot-1))
						{

							Ensemble_DTE[Base+Pivot] = Ensemble_DTE[Base];
							Base -= Pivot;

						}
					Ensemble_DTE[Base+Pivot] = EltTemporaire;

				}

		}

}

/**********************************************************************
*	OPERATEUR DE SORTI DE FLUX <<
**********************************************************************/
ostream& operator<<(ostream &f,const DTEVector &r)
{

	for(int i = r.Nombre_DTE;(r.Nombre_DTE - NB_RESULTATS) < i && i; i--)
		f << r.Ensemble_DTE[i-1] << endl;

	return f;

}


/**********************************************************************
* Implementation de la fonction qui va placer les DTE trouvées dans 
* le fichier défini par la chaine NomFichierSortie 	
* Cette fonction fera peut etre l'objet de modification.
**********************************************************************/
																		 
/**********************************************************************
* Fonction d'optimisation utilisant un tampon memoire, cette version
* est plus rapide mais se revele plus couteuse en memoire, la solution
* serait de trouver un compromis en utilisant		
* un tampon memoire plus reduit.			
**********************************************************************/
																		 
/**********************************************************************
*	FONCTION API : TROUVERDTE	 								
**********************************************************************/
int _stdcall TrouverDTE(char NomFichierTexte[], char NomFichierSortie[],int TailleDTE)
{

	char Verification;
	unsigned offset(1),
		 TailleFichier;

  /////// Traitement d'erreur //////////
		if((TailleDTE<2 || TailleDTE>10))
		{
			return 1;
		}
  //////////////////////////////////////

	ifstream FichierTexte(NomFichierTexte,ios::in|ios::binary);
	FichierTexte.seekg(0,ios::end);
	TailleFichier = FichierTexte.tellg();
	FichierTexte.seekg(0);
	char * Donnees = new char[TailleFichier];
	FichierTexte.read(Donnees,TailleFichier);
	FichierTexte.close();
	char temp[15];
	temp[TailleDTE] = '\0';
	istrstream FluxTexte(Donnees,TailleFichier);
	FluxTexte.read(temp,TailleDTE);
	DTEVector Resultat(temp,TailleDTE);
	FluxTexte.seekg(FluxTexte.tellg()-1);
	
	while(FluxTexte) 
	{
	  Verification = FluxTexte.peek();
		
		if(Verification == '<')
		{
			FluxTexte.ignore(80,'>');
		}
		else if(Verification == '[')
		{	   
			FluxTexte.ignore(80,']');
		}

		FluxTexte.read(temp,TailleDTE);
		Resultat.Ajouter(temp);
		FluxTexte.seekg(FluxTexte.tellg()-1);
	}
	
	Resultat.Trier();
	ofstream FichierSortie(NomFichierSortie,ios::out);
	FichierSortie << Resultat;
	FichierSortie.close();
	
	delete [] Donnees;
	
	return 0;

}

/**********************************************************************
*	FONCTION API: OPTIMISATEUR TEXTE 					
**********************************************************************/
void OptimiserTexte()
{

	char NomFichierEntree[100],
		   NomFichierSortie[100];

	unsigned TailleDTE;
	ifstream LireDonnees("UHROPT.DAT",ios::in);
	LireDonnees >> NomFichierEntree 
				>> NomFichierSortie
				>> TailleDTE;
	LireDonnees.close();
	TrouverDTE(NomFichierEntree,NomFichierSortie,TailleDTE);

}

/* Fonction d'optimisation directe sur le fichier texte */

/*bool TrouverDTEFichierSeulement(const char * NomFichierTexte, const char * NomFichierSortie, unsigned TailleDTE)
{

	char Verification;
	if((TailleDTE<2 || TailleDTE>6))
		return 1;
	ifstream FichierTexte(NomFichierTexte,ios::in|ios::binary);
	char temp[15]; temp[TailleDTE] = '\0';
	FichierTexte.read(temp,TailleDTE);
	DTEVector Resultat(temp,TailleDTE);
	FichierTexte.seekg(FichierTexte.tellg()-1);
	while(FichierTexte) 
	{

		if((Verification=FichierTexte.peek())=='<')
			FichierTexte.ignore(80,'>');
		else if(Verification=='[')
			FichierTexte.ignore(80,']');
		FichierTexte.read(temp,TailleDTE);
		Resultat.Ajouter(temp);
		FichierTexte.seekg(FichierTexte.tellg()-1);

	}
	Resultat.Trier();
	ofstream FichierSortie(NomFichierSortie,ios::out);
	FichierSortie << Resultat;
	FichierSortie.close();
	FichierTexte.close();
	return 0;

}
*/

