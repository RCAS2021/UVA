//Aluno: Rafael Cruz Arantes da Silva
#include<stdio.h>
#include<stdlib.h>

typedef struct No{ // declarando a estrutura.
    int num;
    struct No *esq;
    struct No *dir;
}Arv;

int vazia(Arv *a){ // função para checar se a arvore esta vazia.
    if(a == NULL)
        return 1;
    else
        return 0;
}

Arv* inicializar(){ // função para inicializar a árvore.
    return NULL;
}
    
Arv* insere(Arv* a, int valor){ // função para inserir um nó.
    if(vazia(a)){
        a = (Arv*) malloc(sizeof(Arv));
        a->num = valor;
        a->esq = NULL;
        a->dir = NULL;
        printf("Valor inserido: %d\n",valor);
    }
    	else
    	{
        	if(valor < a->num){
            a->esq = insere(a->esq, valor); // recursividade, função chama a si mesma.
        	}
        	else if(valor > a->num){
        	a->dir = insere(a->dir, valor); // recursividade, função chama a si mesma.
			}
    	}
    return a;
}

Arv *busca(Arv *a, int valor){ // funçao para buscar se há um nó na árvore.
    if(vazia(a)){
    	printf("Valor nao encontrado!\nO valor buscado: %d\n\n",valor);
        return NULL;
    }
    
    else
    {
	if(a->num == valor){
    	printf("Valor encontrado!\nO valor buscado: %d\n\n",valor);
		}
    
    else if(a->num > valor)
    	 busca(a->esq, valor); // recursividade, função chama a si mesma.
    
    else if(a->num < valor)
        busca(a->dir, valor); // recursividade, função chama a si mesma.
    }
    return a;
}

Arv* maior(Arv* a){ 
	while(a->dir != NULL){
        	a = a->dir;
			}
	return a;
}
Arv* removevalor(Arv* raiz, int valor){ // função para remover um nó.
    if(vazia(raiz)) {
    	printf("Valor nao encontrado.\n\n");
        return raiz;
    }
    if(valor < raiz->num)
        raiz->esq= removevalor(raiz->esq, valor); // recursividade, função chama a si mesma.
    else
        if(valor > raiz->num)
            raiz->dir = removevalor(raiz->dir, valor); // recursividade, função chama a si mesma.
        else
        {
            if(raiz->esq == NULL){
            	printf("Valor removido!\n\n");
                Arv* t = raiz;
            	raiz = raiz->dir;
            	free(t);
            }
            else
                if(raiz->dir == NULL){
                	printf("Valor removido!\n\n");
                    Arv* t = raiz;
            		raiz = raiz->esq;
            		free(t);
                }
            else
            	if((raiz->dir != NULL)&&(raiz->esq != NULL)){
            			Arv *t = maior(raiz->esq);
            			int v = t->num;
            			removevalor(raiz,v); // recursividade, função chama a si mesma.
            			raiz->num = v;
            			}		
        }
    
    return raiz;
}



void imprimir(Arv* a){ // função para imprimir a árvore.
    if (!vazia(a)){
        imprimir(a->esq); // recursividade, função chama a si mesma.
        printf("%d\n", a->num);
        imprimir(a->dir); // recursividade, função chama a si mesma.
    }
}
int main(){
    Arv* a;
    a = inicializar();
    int op;
    int valor = 0;
    a = insere(a, 30);
    a = insere(a, 45);
    a = insere(a, 56);
    a = insere(a, 15);
    a = insere(a, 06);
    a = insere(a, 76);
    a = busca(a,45);
    a = busca(a,30);
    a = busca(a,6);
    // menu
    do{
    printf("*******************\n1- INSERIR\n2- BUSCAR\n3- IMPRIMIR\n4- DELETAR VALOR\n0- SAIR\n*******************\nDigite a Opcao: ");
    scanf("%d", &op);
    switch (op){
        case 1:
            printf("\nDigite o valor para inserir: ");
            scanf("%d", &valor);
            a = insere(a, valor);
            printf("\n");
            break;
        case 2:
            printf("\nDigite o valor para buscar: ");
            scanf("%d", &valor);
            a = busca(a,valor);
            break;
        case 3:
            if(vazia(a)){
            printf("\nArvore vazia.\n\n");
            }
            	else{
                printf("\nResultado: \n");
                imprimir(a);
                printf("\n");
            	}
            break;
        case 4:
            printf("\nDigite o valor para remover: ");
            scanf("%d", &valor);
            a = removevalor(a, valor);
            break;
            case 0:
            break;
       	default:
        printf("\nOpcao Invalida.\n\n");
        break;
        }
    }while(op != 0);
    free(a);
    return 0;
}
