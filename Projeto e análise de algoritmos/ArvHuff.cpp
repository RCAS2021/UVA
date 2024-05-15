
//Aluno: Rafael Cruz Arantes da Silva
//Matricula: 20191105905
//Atividade Final - Arvore Huffman

#include <stdio.h>
#include <stdlib.h>
 
#define tamArv 100
 
struct NoMinHeap {
    char data;
    unsigned freq;
    struct NoMinHeap *esq, *dir;
};
 
struct MinHeap {
    
	unsigned tam;
    
	unsigned cap;
    
    struct NoMinHeap** vet;
};
 
struct NoMinHeap* novoNo(char data, unsigned freq)
{
    struct NoMinHeap* temp = (struct NoMinHeap*)malloc(
        sizeof(struct NoMinHeap));
 
    temp->esq = temp->dir = NULL;
    temp->data = data;
    temp->freq = freq;
 
    return temp;
}
 

struct MinHeap* criarMinHeap(unsigned cap)
 
{
 
    struct MinHeap* minHeap
        = (struct MinHeap*)malloc(sizeof(struct MinHeap));
 
    minHeap->tam = 0;
 
    minHeap->cap = cap;
 
    minHeap->vet = (struct NoMinHeap**)malloc(
        minHeap->cap * sizeof(struct NoMinHeap*));
    return minHeap;
}
 

void trocaNosMinHeap(struct NoMinHeap** a,
                     struct NoMinHeap** b)
 
{
 
    struct NoMinHeap* t = *a;
    *a = *b;
    *b = t;
}
 
void minHeapify(struct MinHeap* minHeap, int idx)
 
{
 
    int menor = idx;
    int esq = 2 * idx + 1;
    int dir = 2 * idx + 2;
 
    if (esq < minHeap->tam && minHeap->vet[esq]->freq < minHeap->vet[menor]->freq)
        menor = esq;
 
    if (dir < minHeap->tam && minHeap->vet[dir]->freq < minHeap->vet[menor]->freq)
        menor = dir;
 
    if (menor != idx) {
        trocaNosMinHeap(&minHeap->vet[menor],
                        &minHeap->vet[idx]);
        minHeapify(minHeap, menor);
    }
}
 
int isSizeOne(struct MinHeap* minHeap) // checa se o tamanho do heap é 1
{
 
    return (minHeap->tam == 1);
}
 
struct NoMinHeap* extraiMin(struct MinHeap* minHeap)
 
{
 
    struct NoMinHeap* temp = minHeap->vet[0];
    minHeap->vet[0] = minHeap->vet[minHeap->tam - 1];
 
    --minHeap->tam;
    minHeapify(minHeap, 0);
 
    return temp;
}
 
void insereMinHeap(struct MinHeap* minHeap,struct NoMinHeap* NoMinHeap)
 
{
    ++minHeap->tam;
    int i = minHeap->tam - 1;
 
    while (i && NoMinHeap->freq < minHeap->vet[(i - 1) / 2]->freq) {
        minHeap->vet[i] = minHeap->vet[(i - 1) / 2];
        i = (i - 1) / 2;
    }
 
    minHeap->vet[i] = NoMinHeap;
}
 
void constroiMinHeap(struct MinHeap* minHeap)
 
{
 
    int n = minHeap->tam - 1;
    int i;
 
    for (i = (n - 1) / 2; i >= 0; --i)
        minHeapify(minHeap, i);
}
 
void imprimeVet(int vet[], int n)
{
    int i;
    for (i = 0; i < n; ++i)
        printf("%d", vet[i]);
 
    printf("\n");
}
 
int testeFolha(struct NoMinHeap* root)
{
    return !(root->esq) && !(root->dir);
}

struct MinHeap* criarConstruirMinHeap(char data[],int freq[], int tam) 
{
    struct MinHeap* minHeap = criarMinHeap(tam);
 
    for (int i = 0; i < tam; ++i)
        minHeap->vet[i] = novoNo(data[i], freq[i]);
 
    minHeap->tam = tam;
    constroiMinHeap(minHeap);
 
    return minHeap;
}
 

struct NoMinHeap* constroiArvoreHuffman(char data[],int freq[], int tam)
{
    struct NoMinHeap *esq, *dir, *topo;
    struct MinHeap* minHeap = criarConstruirMinHeap(data, freq, tam);
 
    while (!isSizeOne(minHeap)) {
 

        esq = extraiMin(minHeap);
        dir = extraiMin(minHeap);
 
        topo = novoNo('$', esq->freq + dir->freq);
 
        topo->esq = esq;
        topo->dir = dir;
 
        insereMinHeap(minHeap, topo);
    }
 
    return extraiMin(minHeap);
}

void imprimeHuffman(struct NoMinHeap* root, int vet[],int topo)
{
 
    if (root->esq) {
        vet[topo] = 0;
        imprimeHuffman(root->esq, vet, topo + 1);
    }
 
    if (root->dir) {
 
        vet[topo] = 1;
        imprimeHuffman(root->dir, vet, topo + 1);
    }
    if (testeFolha(root)) {
 
        printf("%c: ", root->data);
        imprimeVet(vet, topo);
    }
}
 

void HuffmanCod(char data[], int freq[], int tam)
 
{
    struct NoMinHeap* root = constroiArvoreHuffman(data, freq, tam);

    int vet[tamArv], topo = 0;
 
    imprimeHuffman(root, vet, topo);
}
 
int main()
{
 
    char vet[] = { 'a', 'b', 'c', 'd', 'e', 'f' };
    int freq[] = { 5, 9, 12, 13, 16, 45 };
 
    int tam = sizeof(vet) / sizeof(vet[0]);
 
    HuffmanCod(vet, freq, tam);
 
    return 0;
}
