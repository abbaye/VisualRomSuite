/* simple utlity made to expand SNES roms
 * coded by John Bissell a.k.a. hight1mes
 */

#include <stdio.h>
#include <stdlib.h>

int DONE = 0;

int PrintMenu();
void ExpandRom(unsigned long int size, char *file);

int main(int argc, char *argv[])
{
    unsigned int long custom;
    int choice;

    if (argc < 2)
    {
        fprintf(stderr, "Usage: %s [snes-rom]\n",  argv[0]);
        exit(1);
    }

    printf("JABBA v.1.0 / Coded by hight1mes (royalblu@silcom.com)\n\n");
    printf("Expanding: %s\n\n", argv[1]);

    while (!DONE)
    {
        choice = PrintMenu();
        switch (choice)
        {
            case 1 : ExpandRom(524800, argv[1]);
                     break;
            case 2 : ExpandRom(1049088, argv[1]);
                     break;
            case 3 : ExpandRom(1573376, argv[1]);
                     break;
            case 4 : ExpandRom(2097664, argv[1]);
                     break;
            case 5 : ExpandRom(2621952, argv[1]);
                     break;
            case 6 : ExpandRom(3146240, argv[1]);
                     break;
            case 7 : printf("\nHow many bytes would you like to expand? ");
                     scanf("%ld", &custom);
                     ExpandRom(custom, argv[1]);
                     break;
            case 8 : DONE = 1;
                     break;
            default : printf("That is not a valid choice\n");
                      break;
        }
    }

    printf("\nThank you for using JABBA\n");
    return 0;
}


int PrintMenu()
{
    int choice;

    printf("WHAT DO YOU WANT TO DO?\n\n");
    printf("\t1. Expand ROM 0.5 MB\n");
    printf("\t2. Expand ROM 1.0 MB\n");
    printf("\t3. Expand ROM 1.5 MB\n");
    printf("\t4. Expand ROM 2.0 MB\n");
    printf("\t5. Expand ROM 2.5 MB\n");
    printf("\t6. Expand ROM 3.0 MB\n");
    printf("\t7. Custom ROM Expansion\n");
    printf("\t8. Quit\n\n");
    printf("WHAT IS YOUR CHOICE? ");
    scanf("%d", &choice);
    return (choice);
}

void ExpandRom(unsigned long int size, char *file)
{
    FILE *FP;
    char *buff;

    buff = (char *) malloc(size * sizeof(char));
 
    if (buff == NULL)
    {
        fprintf(stderr, "Error no memory was allocated!\n");
        exit(1);
    }

    FP = fopen(file, "ab"); 

    if (FP == NULL)
    {
        fprintf(stderr, "Error opening file!\n");
        exit(1);
    }

    if ( ( fwrite(buff, sizeof(char), size, FP) ) != size)
    {
        fprintf(stderr, "Error expanding file!\n");
        exit(1);
    }

    fclose(FP);
    printf("\n%s has been expanded by %ld bytes\n\n", file, size);
}