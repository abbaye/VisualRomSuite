/**********************************************************************
*	INCLUDES
**********************************************************************/
#include <stdio.h>

/**********************************************************************
*	Prodedure : gen_table			
*		Description : Genere une table de crc en fonction d'un polynome
**********************************************************************/
void gen_table(unsigned long * crc_table, unsigned long poly)                
{
		//Variable
    unsigned long crc;
    int	i, j;
    

		for (i = 0; i < 256; i++)
		{
			crc = i;
			for (j = 8; j > 0; j--)
				if (crc & 1) 
					crc = (crc >> 1) ^ poly;
				else 
					crc >>= 1;
		      
			crc_table[i] = crc;
		}
}

/**********************************************************************
*	Procedure : get_crc
*		Description : Retourne le CRC du fichier
**********************************************************************/
	unsigned long get_crc( FILE *f, unsigned long * crc_table) 
	{
			//Variable
			register unsigned long crc = 0xFFFFFFFF;
			int my_char;

			//Core
			while ((my_char = getc(f)) != EOF)
					crc = ((crc>>8) & 0x00FFFFFF) ^ crc_table[ (crc^my_char) & 0xFF ];

			//Valeur de retour
			return(crc ^ 0xFFFFFFFF);
	}

/**********************************************************************
*	Fonction : GiveCRC32		
*		Description : Donne le CRC2 du fichier passer en parametre
**********************************************************************/
unsigned long GiveCRC32(char * filename)
{	
	//Variable
	FILE * f;

	//Ouvre le Fichier et s'il a une erreur , retourne 0
	if((f= fopen(filename,"rb")) == NULL) return 0;
	
	//Un Polynome Quelquonc
	unsigned long my_poly = 0xCA679140L;
	
	//Un tableau qui contient les crc
	unsigned long my_crc_table[256];
	
	//genere la table de crc
	gen_table(my_crc_table, my_poly);

	// ferme le fichier
	fclose(f);

	return get_crc(f, my_crc_table);
}
