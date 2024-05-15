#include <stdio.h>
#include <stdlib.h>
#define tam 2
//Aluno: Rafael Cruz Arantes da Silva
//Matricula: 20191105905
//Atividade2 D
int main(int argc, char *argv[]) {
    
    int mat[tam][tam];
    int procura;
    bool achou = false;
    for (int i=0; i<tam; i++ )
	{
    	for (int j=0; j<tam; j++ )
    	{
      		printf ("\nElemento[%d][%d] = ", i, j);
      		scanf ("%d", &mat[ i ][ j ]);
    	}
	}
	printf("Digite o numero a ser procurado\n");
    scanf("%procura",&procura);
    for(int i=0;i<=tam;i++)
    {
    	for(int j=0;j<=tam;j++){
        	if(mat[i][j] == procura)//Operação Fundamental
			{
            	achou = true;
    		}
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
