#include <stdio.h>
#include <stdlib.h>
#define tam 8

int cont = 0;

int somaPos(int vet[tam],int n)
{
	int soma = 0;
	int contador = 0;
	if(n != 0){
		soma = somaPos(vet, n-1);
		cont++;
	}
	if(vet[n]> 0){
		return soma=soma+vet[n];
		cont++;
	}
	else{
		return soma;
	}	
}

int main(int argc, char *argv[]) {
    int soma = 0;
    int vet[tam] = {20,-30,15,-10,30,-20,-30,30};  
    cont++;
    soma = somaPos(vet,tam-1);   
    printf("+++++++++++++++++++++++++++++++++++++++++++\n");
	printf("Nome: Rafael Cruz Arantes da Silva\n");
	printf("Matricula: 20191105905\n");
	printf("Questao 1 - B\n");
	printf("+++++++++++++++++++++++++++++++++++++++++++\n");
	printf("Resultado da soma: %d\n",soma);
	printf("Vetor: [%d",vet[0]);
	for(int i = 1; i<tam;i++){
	printf(", %d",vet[i]);;
	}
	printf("]");
	printf("\n");
	printf("Contador FOR: 0\n");
	printf("Contador IF: %d\n",cont);
	printf("Total: %d Unidades de Tempo\n",cont);
	printf("+++++++++++++++++++++++++++++++++++++++++++\n");
	
    return 0;
}
