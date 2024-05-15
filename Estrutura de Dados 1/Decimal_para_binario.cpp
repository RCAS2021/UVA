#include <stdio.h>
#define MAX 64

struct pilhaEstatica{
	int valor[MAX];
	int topo; 
};
struct pilhaEstatica pilha;

void reiniciarTopo();
void empilhar(int);
void remover();
void exibir();
void binario(int);

int main(void){
	
	int num, op;
		
	reiniciarTopo();
			
				printf("Digite o inteiro que deseja ser convertido em binario:\n");
				scanf("%d", &num);
				binario(num);
				exibir();
			
		
		}	
	
	

void reiniciarTopo(){
	pilha.topo = -1;
}

void empilhar(int num){
	if(pilha.topo == (MAX-1)){
		printf("\nA pilha esta cheia!\n");
	}
	else{
		pilha.topo++;
		pilha.valor[pilha.topo] = num;
	}
}

void remover(){
	if(pilha.topo == -1){
		printf("\nA pilha esta vazia!\n");
	}
	else{
		pilha.topo--;
	}
}

void exibir(){
	int i;
	printf("Conversao:");
	for(i=pilha.topo;i>=0;i--){
		printf("%d", pilha.valor[i]);
	}
}

void binario(int num){
	reiniciarTopo();
	int i;
	while(num != 0){
	
	if(num % 2 == 0){
		empilhar(0);
	}
	else if (num%2 == 1){
		empilhar(1);
	}
	num = num / 2;
	}
}











