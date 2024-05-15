//Aluno: Rafael Cruz Arantes da Silva
#include <stdio.h>
#include <stdlib.h>

int fib(int num) 
{	
	if(num < 2){
		return num;
	}
	else{
		return fib(num-1)  + fib(num-2); // recursividade, a função chama a si mesma.
	}
}

int main(){
	int num;
	printf("Digite o limite da sequencia: ");
	scanf("%d",&num);
	printf("%d\n",fib(num));	
	return 0;
}

