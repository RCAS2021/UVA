#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct contato{
    char nome[35], telefone[15];
    int dia, mes, ano;
    struct contato *prox, *ant;

}contato;
int estaVazia(contato **inicio){
    if(*inicio == NULL){
        return 1;
    }
    else return 0;
}
void adicionar(contato **inicio){
    contato *novo, *aux = *inicio;
    novo = (contato*)malloc(sizeof(contato));
    printf("Digite o nome a ser adicionado: ");
    fgets(novo->nome,35,stdin);
    printf("Digite o telefone a ser adicionado: ");
    fgets(novo->telefone,15 ,stdin);
	printf("insira o dia do nascimento: ");
    scanf("%d",&novo->dia);
    if((novo->dia > 31)||(novo->dia < 1)){
    	printf("Dia invalido! Digite 1 novamente se quiser tentar adicionar!\n");
	}
	else{
    printf("insira o mes do nascimento: ");
    scanf("%d",&novo->mes);
    if((novo->mes > 12)||(novo->mes<1)){
    	printf("Mes invalido! Digite 1 novamente se quiser tentar adicionar!\n");
	}
	else{
    printf("insira o ano do nascimento: ");
    scanf("%d",&novo->ano);
    if (*inicio == NULL){
        novo->ant = NULL;
        novo->prox = NULL;
        *inicio = novo;
    }else{
        while(aux->prox != NULL){
            aux = aux->prox;
        }
        novo->ant = aux;
        novo->prox = NULL;
        aux->prox = novo;
    	}	
		}
	}
}
void remover(contato **inicio, char nome[]){
    if(estaVazia(inicio)){
        printf("lista vazia\n");
        return;
    }
    contato *atual, *aux;
    atual = *inicio;
    while(strcmp(nome,atual->nome) != 0){
        atual = atual->prox;
        if(atual == NULL){
            printf("contato inexistente\n");
            return;
        }
    }
    if(atual->ant == NULL && atual->prox == NULL){
        *inicio = NULL;
    }
    else if(atual->ant == NULL){
        aux = atual->prox;
        aux->ant = NULL;
        *inicio = aux;
    }
    else if(atual->prox == NULL){
        aux = atual->ant;
        aux->prox = NULL;
    }
    else{
        (atual->ant)->prox = atual->prox;
        (atual->prox)->ant = atual->ant;
    }
    printf("Contato removido:%s\n",atual->nome);
    free(atual);
}
void buscar(contato **inicio, char nome[]){
    if(estaVazia(inicio)){
        printf("lista vazia\n");
        return;
    }
    contato *atual;
    atual = *inicio;
    while(strcmp(atual->nome,nome) != 0){
        atual = atual->prox;
        if(atual == NULL){
            printf("contato inexistente\n");
            return;
        }
    }
    printf("data do nascimento do contato: %d/%d/%d\n",atual->dia,atual->mes,atual->ano);
}
void aniversariantes(contato **inicio, int mes){
    contato *atual;
    atual = *inicio;
    int i = 0;
    printf("-Aniversariantes do mes-\n", mes);
    while(atual != NULL){
        if(atual->mes == mes){
            printf("nome do aniversariante: %sdia do aniversario: %d\n",atual->nome,atual->dia);
            i++;
        }
        atual = atual->ant;
    }
    if(i==0){
        printf("nenhum contato faz aniversario no mes escolhido\n");
    }
    return;
}
int main()
{
    int escolha, mes;
    char nome[35];
    contato *inicio;
    inicio = NULL;
    do{
    	printf("----------------------------Escolha----------------------------");
        printf("\n1-Adicionar contato\n2-Remover contato\n3-Buscar data de nascimento\n4-Aniversariantes do mes\n5-Sair\n");
        scanf("%d",&escolha);
        fflush(stdin);
        switch(escolha){
            case 1:
                adicionar(&inicio);
                break;
            case 2:
                printf("Nome do contato: ");
                fgets(nome,35,stdin);
                remover(&inicio,nome);
                break;
            case 3:
                printf("Nome do contato: ");
                fgets(nome,35,stdin);
                buscar(&inicio,nome);
                break;
            case 4:
                printf("Digite o mes: ");
                scanf("%d",&mes);
                aniversariantes(&inicio,mes);
                break;
            case 5:
                return 0;
            default:
                printf("escolha invalida");
        }
    }while(escolha !=5);
    return 0;
}
