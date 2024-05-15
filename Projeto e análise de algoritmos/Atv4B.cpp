#include <stdio.h>
#include <stdlib.h>

//Aluno: Rafael Cruz Arantes da Silva
//Matricula: 20191105905
//Atividade4 B
#define tam 5
int main()
{
  int vet[tam];
  int i,max,min;

  for (i=0; i<tam; i++ )
    	{
      		printf ("\nDigite o elemento[%d] = ", i);
      		scanf ("%d", &vet[i]);
    	}

  for (i = 0; i < tam; i++) 
  {
    if (i == 0) 
      max = min = vet[i];
    else
	{
      if (vet[i] > max) max = vet[i];
      else if (vet[i] < min) min = vet[i];
    }
  }

  printf("max = %d, min = %d\n", max, min);

  return 0;
}
