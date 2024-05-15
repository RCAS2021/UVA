#include <stdio.h>
#include <stdlib.h>
#define tam 10
// Busca sequencial
// O(n) -> numero total de elementos (n+1)/2 <- caso médio, pior caso -> são feitas N comparações 
int main(int argc, char *argv[]) {
    
    int vet[tam] = {1,2,3,4,5,6,7,8,9,10};
    bool achou = false;
    int procura;
    printf("Digite o numero a ser procurado\n");
    scanf("%procura",&procura);
    for(int i=0;i<tam && !achou;i++)
    {
        if(vet[i] == procura){
            achou = true;  
    	}
    }
    if(achou == true){
    	printf("Encontrado!");
	}
	else{
		printf("Nao encontrado!");
	}
    
   
    return 0;
}
