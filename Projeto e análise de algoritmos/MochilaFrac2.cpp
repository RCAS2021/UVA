#include <stdio.h>
#include <stdlib.h>

//Aluno: Rafael Cruz Arantes da Silva
//Matricula: 20191105905
//Atividade Final - Mochila fracionária
 
void mochila(int n, float peso[], float valor[], float cap) {
   float x[20], vTot = 0;
   int i, j, u;
   u = cap;
 
   for (i = 0; i < n; i++)
      x[i] = 0.0;
 
   for (i = 0; i < n; i++) {
      if (peso[i] > u)// Se o peso for maior do que a capacidade
         break;
      else {
         x[i] = 1.0;
         vTot = vTot + valor[i];
         u = u - peso[i];
      }
   }
 
   if (i < n)
      x[i] = u / peso[i];
 
   vTot = vTot + (x[i] * valor[i]);
 
   printf("\nVetor resultante: ");
   for (i = 0; i < n; i++)
      printf("%f\t", x[i]);
 
   printf("\nValor maximo: %f", vTot);
 
}
 
int main() {
   float peso[20], valor[20], cap;
   int num, i, j;
   float vp[20], temp; //vp = valor/peso
 
   printf("\nQuantidade de objetos: ");
   scanf("%d", &num);
 
   printf("\nPesos e valores de cada objeto: ");
   for (i = 0; i < num; i++) {
      scanf("%f %f", &peso[i], &valor[i]);
   }
 
   printf("\nCapacidade da mochila: ");
   scanf("%f", &cap);
 
   for (i = 0; i < num; i++) {
      vp[i] = valor[i] / peso[i];
   }
 
   for (i = 0; i < num; i++) {
      for (j = i + 1; j < num; j++) {
         if (vp[i] < vp[j]) {
            temp = vp[j];
            vp[j] = vp[i];
            vp[i] = temp;
 
            temp = peso[j];
            peso[j] = peso[i];
            peso[i] = temp;
 
            temp = valor[j];
            valor[j] = valor[i];
            valor[i] = temp;
         }
      }
   }
 
   mochila(num, peso, valor, cap);
   return(0);
}
