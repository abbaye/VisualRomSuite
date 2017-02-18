/**********************************************************************
*	INCLUDE
**********************************************************************/
#include <stdlib.h>
#include <conio.h>
#include <stdio.h>
#include <ctype.h>
#include <string.h>

/**********************************************************************
*	STRUCTURE
**********************************************************************/
struct n64Byte
  {
    char left[1];
    char right[1];
  };

/**** byteswap ********************************************************
*  Return :
*           1 : Completer avec succes
*          -1 : Erreur Fichier Entrer (!in)
*          -2 : Erreur Fichier Sorti (!out)
**********************************************************************/
int byteswap(char sfile_in[], char sfile_out[])
{
  //variables
    n64Byte temp_in;   //IN
    n64Byte temp_out;  //OUT
    long x;            //Compteur

  //Ouvre les Fichier d'entrer sorti
    FILE *in  = fopen(sfile_in, "r+b"); //Fichier Entrer
      if (in=NULL) return -1; 
    FILE *out = fopen(sfile_out, "wb"); //Fichier Sorti
      if (out=NULL) return -2; 
    
  //Block de code servant a inverser l'ordre des bytes
    for (x=0; x<sizeof(in); x++)
    {
      //Lecture dans le fichier IN
        fread(&temp_in,sizeof(n64Byte),1,in);

      //Reorder les byte (inverse)
        strcpy(temp_out.left, temp_in.right);
        strcpy(temp_out.right, temp_in.left);

      //Ecrit dans le fichier OUT
        fwrite(&temp_out,sizeof(n64Byte),1,in);
    };

  //ferme les fichier
    fclose(in);
    fclose(out);

  //Sorti avec succes
    return 1;
}
