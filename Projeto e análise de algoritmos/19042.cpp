#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#define tam 10

int buscaBinariaRec(int vet[tam], int prim, int ult, int procura)
{
    int i = (prim + ult) / 2;

    if (prim > ult) {
        return -1;
    }

    if (vet[i] == procura) {
        return i;
    }

    if (vet[i] < procura) {
        return buscaBinariaRec(vet, i + 1, ult, procura);

    } else {
        return buscaBinariaRec(vet, prim, i - 1, procura);
    }
}
int main (int argc, char* argv[])
{
    int vet[tam] = {8,2,1,5,6,4,7,9,10,3};
    
	int procura;
	int teste = -1;
    printf("Digite o numero a ser procurado\n");
    scanf("%procura",&procura);
    
    teste = buscaBinariaRec(vet, 0, tam - 1, procura);
    
    if(teste == 0){
    	printf("Encontrado");
	}
	else{
    printf("Nao encontrado!");     
}

    return 0;
}
