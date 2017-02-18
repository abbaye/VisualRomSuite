/**********************************************************************
*	IPS PATCH MAKER 
**********************************************************************/

/**********************************************************************
*	INCLUDE														
**********************************************************************/
#include <windows.h>

/**********************************************************************
*	MACRO														
**********************************************************************/
#define EXPORT __export
#define EOF -1

/**********************************************************************
*	VARIABLE GLOBAL														
**********************************************************************/
	static unsigned char *buffer;
	static unsigned long address = 0;
	static unsigned long count = 0;
	static BOOL userle = TRUE;

/**********************************************************************
*	STRUCTURE														
**********************************************************************/
	typedef struct FILEINFO
	{
	   HANDLE hFile;
	   DWORD nBufCount;
	   DWORD nBufSize;
	   BOOL bEof;
	   BYTE lpBuffer[1024];
	} FILEINFO, *LPFILEINFO;

	static HANDLE ips;
	static LPFILEINFO uf, cf;

/**********************************************************************
*	PROTOTYPES												
**********************************************************************/
	static      LPFILEINFO WINAPI LoadFile(LPCSTR lpszFilename);
	static void WINAPI ReloadBuffer(LPFILEINFO f);
	static int  WINAPI ReadByte(LPFILEINFO f);
	static void WINAPI flushBuf(void);
	static void WINAPI addByte(unsigned char c);
	static void WINAPI WriteBlock(DWORD Address,DWORD Length,LPBYTE Data);
	static void WINAPI WriteBlockRLE(DWORD Address,DWORD RunLength,BYTE RunByte);
	//BOOL WINAPI EXPORT MakePatch(LPCSTR UnchangedFile,LPCSTR ChangedFile,LPCSTR OutputFile,LPSTR ErrorMsg,BOOL bUseRle);
	BOOL WINAPI EXPORT MakePatch(LPCSTR UnchangedFile,LPCSTR ChangedFile,LPCSTR OutputFile,BOOL bUseRle);

/**********************************************************************
*	LOAD FILE	
* 	Description : Ouvre un fichier en lecture												
*		Type				: API
**********************************************************************/
static LPFILEINFO WINAPI LoadFile(LPCSTR lpszFilename)
{
   LPFILEINFO f;

   // allocate FILEINFO
   f = (LPFILEINFO)GlobalAlloc(GPTR,sizeof(FILEINFO));
   if(!f) return NULL;

   // open file
   f->hFile = CreateFile(lpszFilename, GENERIC_READ, FILE_SHARE_READ, NULL,OPEN_EXISTING, 0, NULL);
   if(f->hFile == INVALID_HANDLE_VALUE) return NULL;

   ReloadBuffer(f);

   return f;
}

/**********************************************************************
*	RELOAD BUFFER	
* 	Description : Relod le buffer du fichier											
*		Type				: API
**********************************************************************/
// reloads the read buffer of a file
static void WINAPI ReloadBuffer(LPFILEINFO f)
{
   // refill the buffer
   ReadFile(f->hFile,f->lpBuffer,1024,&f->nBufSize,NULL);
   f->bEof = (f->nBufSize == 0);
   f->nBufCount = 0;
}

/**********************************************************************
*	CLOSE FILE	
* 	Description : Ferme un fichier										
*		Type				: API
**********************************************************************/
static void WINAPI CloseFile(LPFILEINFO f)
{
   CloseHandle(f->hFile);
   GlobalFree(f);
}

/**********************************************************************
*	READ BYTE	
* 	Description : Lis un Byte dans un fichier												
*		Type				: API
**********************************************************************/
static int WINAPI ReadByte(LPFILEINFO f)
{
   int byte;

   if(f->bEof) return EOF;

   // grab byte from buffer
   byte = f->lpBuffer[f->nBufCount++];
   if(f->nBufCount >= f->nBufSize) ReloadBuffer(f);

   return byte;
}

/**********************************************************************
*	FLUSH BUFFER	
* 	Description : Ecrit un block [address, length, data] dans le fichier IPS.
*		Type				: API
**********************************************************************/
static void WINAPI flushBuf(void)
{
   unsigned long start, end;
   int i, j, runcount;
   unsigned char runbyte;

   if(count)
	 {
      // if block is < size 3, ecrit normale
      if(count <= 3 || !userle)
				WriteBlock(address,count,buffer);
      else if(count <= 16)
				{ 
					 // if moins que 16, check pour la compression RLE
					 runbyte = buffer[0];
					 for(i = 0; i < count; i++)
						 if(buffer[i] != runbyte) break;

					 // Compresser seulement si le block entier est le meme.
							if(i == 16) WriteBlockRLE(address,count,runbyte);
								else WriteBlock(address,count,buffer);
				}
			else
				{ // sinon compresser tous les block > 16 dup
					 start = end = address;
					 for(i = 0; i < count; i++, end++)
					 {
								// check pour la compression RLE 
								if((i + 16) < count
									 && buffer[i] == buffer[i + 1]
									 && buffer[i] == buffer[i + 16])
									{
										 runbyte = buffer[i];
										 runcount = 2;

										 for(j = i + 2; j < count; j++, runcount++)
												if(buffer[j] != runbyte) break;
        
										 if(runcount >= 16)
											 {
													WriteBlock(start,end - start,buffer + start - address);
													WriteBlockRLE(i + address,runcount,runbyte);
													start = end + runcount;
													end = start - 1;
													i += runcount - 1;
											 }
									}
						 }
					 WriteBlock(start,end - start,buffer + start - address);
				}

      count = 0;
   }
}

/**********************************************************************
*	WRITE BLOCK
* 	Description : Ecrit un block [address, length, data] dans le fichier IPS.
*		Type				: API
**********************************************************************/
static void WINAPI WriteBlock(DWORD Address,DWORD Length,LPBYTE Data)
{
   DWORD addr, nSize;
   WORD len;

   if(Length)
		 {
				// inverser les adresses LSB->MSB
				addr = ((Address >> 16) & 0xFF) | (Address & 0xFF00) | ((Address & 0xFF) << 16);
				WriteFile(ips,&addr,3,&nSize,NULL);
				// LSB -> MSB
				len = (WORD)(((Length >> 8) & 0xFF) | ((Length & 0xFF) << 8));
				WriteFile(ips,&len,2,&nSize,NULL);
				// ecrit le data
				WriteFile(ips,Data,Length,&nSize,NULL);
		 }
}

/**********************************************************************
*	WRITE BLOCK RLE	
* 	Description : Ecrit un block [address, runlength, runbyte] dans le fichier IPS.
*		Type				: API
**********************************************************************/
static void WINAPI WriteBlockRLE(DWORD Address,DWORD RunLength,BYTE RunByte)
{
   DWORD addr, nSize;
   WORD len;

   if(RunLength)
	 {
      // inverser les adresses LSB->MSB
      addr = ((Address >> 16) & 0xFF) | (Address & 0xFF00) | ((Address & 0xFF) << 16);
      WriteFile(ips,&addr,3,&nSize,NULL);
      // ecrit 0 au conteur
      len = 0;
      WriteFile(ips,&len,2,&nSize,NULL);
      // LSB -> MSB
      len = (WORD)(((RunLength >> 8) & 0xFF) | ((RunLength & 0xFF) << 8));
      WriteFile(ips,&len,2,&nSize,NULL);
      // ecrit le data
      WriteFile(ips,&RunByte,1,&nSize,NULL);
   }
}

/**********************************************************************
*	ADD BYTE	
* 	Description : Place un byte dans le block buffer
*		Type				: API
**********************************************************************/
static void WINAPI addByte(unsigned char c)
{
   if(count >= 65535L) flushBuf(), address += 65535L;
   buffer[count++] = c;
}

/**********************************************************************
*	MAKE PATCH
* 	Description : Fabrique une patch
*		Type				: API EXPORT
**********************************************************************/
BOOL __stdcall MakePatch(LPCSTR UnchangedFile,LPCSTR ChangedFile,LPCSTR OutputFile,BOOL bUseRle)
{
   int byte1, byte2;
   unsigned long offset;
   DWORD nSize;

   // réserve de la mémoire pour le buffer
   buffer = (unsigned char *)GlobalAlloc(GPTR,65536);
   if(!buffer)
		 {
				return FALSE;
		 }

   // charge le fichier non changer
   uf = LoadFile(UnchangedFile);
   if(!uf)
		 {
				//if(ErrorMsg) wsprintf(ErrorMsg,"%s: Ne peut trouver le fichier.",UnchangedFile);
				goto err0;
		 }

   // charge le fichier modifier
   cf = LoadFile(ChangedFile);
   if(!cf)
		 {
				//if(ErrorMsg) wsprintf(ErrorMsg,"%s: Ne peut trouver le fichier.",ChangedFile);
				goto err1;
		 }

   if(GetFileSize(cf,NULL) < GetFileSize(uf,NULL))
		 {
				//if(ErrorMsg) wsprintf(ErrorMsg,"La taile du fichier modifié est plus petite que la taille du fichier original.");
				goto err2;
		 }

   // créé le IPS de sortie
   ips = CreateFile(OutputFile,GENERIC_WRITE,FILE_SHARE_WRITE,NULL,CREATE_ALWAYS,0,NULL);
   if(ips == INVALID_HANDLE_VALUE)
		 {
				//if(ErrorMsg) wsprintf(ErrorMsg,"%s: Ne peut ecrire dans le fichier.",OutputFile);
				goto err2;
		 }

   // write signature
   WriteFile(ips,"PATCH",5,&nSize,NULL);

   userle = bUseRle;

   offset = 0;
   for(;;)
	 {
      // read byte from both files
      byte1 = ReadByte(uf);
      byte2 = ReadByte(cf);
      if(byte2 == EOF) break;

      offset++; // increment offset
      // compare the 2 bytes
      if(byte1 == EOF || byte1 != byte2)
				{
					 // si les 2 bytes ne sont pas égale, alors ajoute byte2 au block buffer
					 addByte((unsigned char)byte2);
				}
			else
				{
					 // si les bytes sont identiques
					 flushBuf(); // flush le buffer
					 address = offset; // ajuste les addresse
				}
   }
   flushBuf();

   // write EOF
   WriteFile(ips,"EOF",3,&nSize,NULL);

   CloseHandle(ips);
   CloseFile(uf);
   CloseFile(cf);
   GlobalFree(buffer);

   return TRUE;

err2:
   CloseFile(cf);
err1:
   CloseFile(uf);
err0:
   GlobalFree(buffer);

   return FALSE;
}