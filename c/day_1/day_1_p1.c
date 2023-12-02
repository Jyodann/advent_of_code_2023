#include <stdio.h>

#include <stdlib.h>
#include <string.h>

#define NUMBER_ZERO '0'
#define NUMBER_NINE '9'
#define NEW_LINE '\n'
#define RESET_INT -1

int main()
{
    int ch;
    int total = 0;
    FILE *fptr;
    fptr = fopen("input", "r");
    int first_int = RESET_INT, second_int = RESET_INT;

    while (!feof(fptr))
    {
        ch = fgetc(fptr);

        /* code */
        if (ch >= NUMBER_ZERO && ch <= NUMBER_NINE)
        {
            // Detect for an int:
            if (first_int == RESET_INT)
            {
                first_int = ch - NUMBER_ZERO;
            }
            else
            {
                second_int = ch - NUMBER_ZERO;
            }
        }

        if (ch == NEW_LINE || ch == EOF)
        {
            if (first_int == RESET_INT)
            {
                continue;
            //printf("First: %d, Second: %d\n", first_int, second_int);
            }

            if (second_int == RESET_INT)
            {
                // This means that there isnt a 2nd number in the sequence
                total += (first_int * 10) + first_int;
            }
            else
            {
                total += ((first_int) * 10) + second_int;
            }

            
            printf("First Int: %d, Second Int: %d Total: %d\n", first_int, second_int, total);

            first_int = second_int = RESET_INT;
            
        }

    }
    printf("Total: %d", total);
    fclose(fptr);
}