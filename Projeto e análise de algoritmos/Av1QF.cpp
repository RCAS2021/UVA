#include <stdio.h>
#include <stdlib.h>

int cont = 0;

int main(int argc, char *argv[]) {
    int tam1 = 0;
    int tam2 = 0;
    int tam3 = 0;
    int tam4 = 0;
    int cont = 0;
    int somaprod;
    printf("Digite o numero de linhas da matriz 1: ");
    scanf("%d",&tam1);
    printf("Digite o numero de colunas matriz 1: ");
    scanf("%d",&tam2);
    printf("Digite o numero de linhas da matriz 2: ");
    scanf("%d",&tam3);
    printf("Digite o numero de colunas matriz 2: ");
    scanf("%d",&tam4);
    if(tam2 == tam3){
    int mat1[tam1][tam2];
    int mat2[tam3][tam4];
    int mat3[tam1][tam4];
    for (int i = 0; i<tam1;i++){
    	for(int j = 0; j<tam2;j++){
    	printf("Digite o elemento da linha %d coluna %d da matriz 1: ",i+1,j+1);
    	scanf("%d",&mat1[i][j]);
    	} 
	}
	for (int i = 0; i<tam3;i++){
    	for(int j = 0; j<tam4;j++){
    	printf("Digite o elemento da linha %d coluna %d da matriz 2: ",i+1,j+1);
    	scanf("%d",&mat2[i][j]);
    	} 
	}
	for(int i = 0;i<tam1;i++){
		for(int j = 0; j<tam4;j++){
			
			somaprod = 0;
			for(int k=0; k<tam1;k++){
				cont++;
				somaprod = somaprod + (mat1[i][k]*mat2[k][j]);
			}
			mat3[i][j]=somaprod;		
		}
		
	}
	printf("+++++++++++++++++++++++++++++++++++++++++++\n");
	printf("Nome: Rafael Cruz Arantes da Silva\n");
	printf("Matricula: 20191105905\n");
	printf("Questao 5 - Avaliacao Final\n");
	printf("+++++++++++++++++++++++++++++++++++++++++++\n");
	printf("Matriz resultante: \n");
	for(int i=0;i<tam1;i++){
		for(int j = 0; j<tam4;j++){
			printf("%d ",mat3[i][j]);
		}
		printf("\n");
	}
	printf("Contador: %d",cont);
	}
	else{
		printf("Tamanhos das matrizes nao possibilitam a multiplicacao de matrizes!");
	}
	// complexidade n³;
    return 0;
}

