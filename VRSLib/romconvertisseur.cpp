/**********************************************************************
*	INCLUDE																														  *
**********************************************************************/
#include <stdio.h>
#include <io.h>
#include <fcntl.h>
#include <string.h>
#include <sys\stat.h>

/**********************************************************************
*	MACRO																															  *
**********************************************************************/
#define SUCCES 1
#define ERR_PREMFILE 2
#define ERR_ABIME 3
#define ERR_FORMAT 4
#define ERR_DEST 5
#define ERR_OPEN 6

/**********************************************************************
*	PROTOTYPE																												    *
**********************************************************************/
int _stdcall convert(char *file, long sizeheader);
int checkheader(int file, long sizeheader);


/**********************************************************************
*	FONCTION : CONVERT																								  *
**********************************************************************/
int _stdcall convert(char *file, long sizeheader)
{	

	struct stat fileinfo;
	int mainfile;
  int outfile;
  int nextfile;
	int nbfile=1;
  int tempint;
  int isheader;
  int format;
  char tempchar;
  char header[4096];
  char buffer[4096];
  char *temp;
	
	temp=file;

  mainfile = open(file, O_RDONLY | O_BINARY);
  
	if(!mainfile)
	{
    close(mainfile);
    return ERR_PREMFILE;
	}

  isheader=checkheader(mainfile, sizeheader);

  if(isheader==ERR_ABIME)
	{
    close(mainfile);
    return ERR_ABIME;
	}

  tempchar='.';
  
	if(file[strlen(file)-2]==tempchar)
	{
    //memcpy(tempchar,"1",1);
    tempchar='1';
    
		if(file[strlen(file)-1]==tempchar)
       format=1;
		 else
			 format=0;
	}
	else format=0;
	// Vérifie si c'est un .1
	if(!format)
	{
	// Pas un ".1"
			close(mainfile);
			return ERR_FORMAT;
	}
	else
	{
	//C'est un point ".1", compte le nombre de fichier
		int check=1;
		int nextfile;
		
		tempint=nbfile;

	while (check)
	{
	 //itoa(tempint+1,&tempchar,10);
	 tempchar=48+tempint+1;
	 temp[strlen(file)-1]=tempchar;
	 nextfile=open(temp, O_RDONLY);

	 if(nextfile>-1)
	 {
		close(nextfile);
		tempint++;
	 }
	 else check=0;
	}

	nbfile=tempint;
	}
		
	// On a compté le nombre de fichiers
  // Il faut créer le fichier de destination

  char *out;

  // Crée le nom du fichier
  out=file;
  tempchar='s';
  out[strlen(file)-1]=tempchar;

  outfile = open(out,O_WRONLY | O_TRUNC | O_CREAT);
  if(!outfile)
	{
   close(mainfile);
   close(outfile);
   return ERR_DEST;
	}

  // Copie le header
  if(isheader)
  {
   read(mainfile,header,sizeheader);
   write(outfile,header,sizeheader);
	}

  close(mainfile);
	
	temp=file;
	// Copie tout le reste
  for(int i=1;i<=tempint;i++)
	{
   //itoa(i,&tempchar,10);
   tempchar=48+i;
   temp[strlen(file)-1]=tempchar;
   nextfile=open(temp, O_RDONLY | O_BINARY);
   if(nextfile)
	 {
     switch(checkheader(nextfile, sizeheader))
		 {
      case 1 :
				{
         lseek(nextfile,sizeheader,SEEK_SET);
				 break;
				}
      case ERR_ABIME :
				{
         close(nextfile);
         close(outfile);
         return ERR_ABIME;
				}
		 }
    
		 while(eof(nextfile)==0)
		 {
      read(nextfile,buffer,4096);
      write(outfile,buffer,4096);
		 }
		 
		 close(nextfile);
	 }
   else
	 {
    close(nextfile);
    close(outfile);
    return ERR_OPEN;
	 }
	}
	
	close(outfile);
	
	return SUCCES;
}

/**********************************************************************
*	FONCTION : CHECKHEADER																						  *
**********************************************************************/
int checkheader(int file, long sizeheader)
{
 struct stat fileinfo;
 int isheader;

 fstat(file, &fileinfo);
 
 if((fileinfo.st_size%4096==0)? 0 : 1)
 {
 	if(fileinfo.st_size%4096==sizeheader)
   	isheader=1;
   else
	 {
   	// Fichier abimé car taille bizarre
       return ERR_ABIME;
	 }
 }
 else isheader=0;

 return isheader;
}