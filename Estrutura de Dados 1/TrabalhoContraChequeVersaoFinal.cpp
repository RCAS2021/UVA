/*



ENTRADA: **ok



número de filhos** ok



nome do funcionário** ok



data (mês e ano)** ok



função do funcionário** ok



código (matricula do funcionário)** ok



salário bruto** ok





PROCESSAMENTO: *ok



fgts** ok



ir** ok



inss** ok



total descontos** ok



total acréscimo** ok



total vencimentos (?)



valor liquido** ok





SAIDA:** ok



valor salário familia** ok

A Partir de 01/01/2019

(Portaria Ministério da Economia 09/2019)

R$ 907,77

R$ 46,54

R$ 907,78 a R$ 1.364,43

R$ 32,80

http://www.guiatrabalhista.com.br/guia/salario_familia.htm



nome do funcionário** ok



data (mês e ano)** ok



função do funcionário** ok



código (matricula do funcionário)** ok



salário do mês** ok



fgts ** ok= Salario total - % do fgts Atualmente, a alíquota padrão é de 8% que deve ser aplicada ao valor total recebido pelo empregado. E no caso de funcionários contratados como jovem aprendiz, a alíquota a ser aplicada é de 2%. https://www.jornalcontabil.com.br/como-calcular-fgts-aprenda-como-fazer-de-forma-simples/



ir ** ok= salario bruto - inss = salario a ser diminuido o IRRF



1ª faixa: 7,5% para bases de R$ 1.903,99 a R$ 2.826,65;



2ª faixa: 15% para bases de R$ 2.826,66 a R$ 3.751,05;



3ª faixa: 22,5% para bases de R$ 3.751,06 a R$ 4.664,68;



4ª faixa: 27,5% para bases a partir de R$ 4.664,69.





1ª faixa: R$ 142,80;



2ª faixa: R$ 354,80;



3ª faixa: R$ 636,13;



4ª faixa: R$ 869,36.





R$ 2.280,41 x 7,5% = R$ 171,03;



R$ 171,03 – R$ 142,80 = R$ 28,23 de tributo retido.







inss ** ok= Com a soma dos vencimentos, você tem a base para o desconto da contribuição previdenciária. As alíquotas vão de 8% a 11%, com um limite de R$ 604,44. Confira abaixo as faixas da contribuição:



Salário bruto até R$ 1.659,38 – 8% de INSS;



Salário bruto de R$ 1.659,39 a R$ 2.765,66 – 9% de INSS;



Salário bruto de R$ 2.765,67 a R$ 5.531,31 – 11% de INSS;



A partir de R$ 5.531,32 – R$ 604,44 de INSS.



Salario Bruto - % do INSS



https://blog.convenia.com.br/como-calcular-irrf-na-folha-de-pagamento/



total desconto** ok 



total vencimentos (?)



valor liquido** ok 



*/







#include <stdlib.h>



#include <stdio.h>



#include <string.h>



float calcINSS(float salIni){

	float descontoINSS;

	float salarioINSS;

	if(salIni > 5531.31 ){



			descontoINSS = 604.44;

			}



		else 

			if (salIni > 2765.66){

			descontoINSS = (salIni*(0.11));

			}



		else 

			if (salIni > 1659.38 ){



			descontoINSS = (salIni*(0.09));

			}

			

		else{



			descontoINSS = salIni*(0.08);

		}

	printf("desconto INSS: %2.2f\n",descontoINSS);	

	return (descontoINSS);

}



float calcIR(float descontoINSS,float salIni){

	float salarioINSS = salIni - descontoINSS;

	float descontoIR;

	if ((salarioINSS < 2826.65) && ( salarioINSS > 1903.99 )){



			descontoIR = (salarioINSS*(0.075));



			descontoIR = (descontoIR - 142.8);

			}



			else 



				if (salarioINSS < 3751.05 && salarioINSS > 2826.66  ){	



			descontoIR = (salarioINSS*(0.15));



			descontoIR = (descontoIR - 354.8);

				}



			else 

				if (salarioINSS < 4664.68 && salarioINSS > 3751.06  ){	



				descontoIR = (salarioINSS*(0.225));



				descontoIR = (descontoIR - 636.13);

				}



			else 

				if (salarioINSS > 4664.69  ){	



				descontoIR = (salarioINSS*(0.275));



				descontoIR = (descontoIR - 869.36);

				}

		

			else{



				descontoIR = 0;			

			}

	printf("desconto IRRF: %2.2f\n",descontoIR);

	return descontoIR;

}



float calcFGTS(float salIni,char func_func[30]){

	float descontoFGTS;

		if(strcmp("jovem aprendiz",func_func)==0){



				 descontoFGTS = salIni*(0.02);

				}

				

				else{



					descontoFGTS = salIni*(0.08);

				}

	printf("Desconto do FGTS: %2.2f\n",descontoFGTS);

	return descontoFGTS;

}



float calcDT(float descontoFGTS,float descontoIR,float descontoINSS){

	float descontoTot;

		descontoTot = descontoFGTS + descontoIR + descontoINSS;

		printf("Total de descontos: %2.2f\n",descontoTot);

		return descontoTot;

}



void calcST(float salIni,float descontoTot,int dependentes){

	float salarioFam;

	float salarioTot;

	salarioTot = salIni - descontoTot;

				

				if(salarioTot < 907.77){

					

					salarioFam = 46.54 * dependentes;

				}

				

				else 

					if(salarioTot > 1364.43){

						

					salarioFam=0;

				}

					

						else{

							salarioFam = 32.8 * dependentes;

						}



	salarioTot = salarioTot + salarioFam;

	printf("Salario Familia: %2.2f\nSalario Total: %2.2f",salarioFam,salarioTot);

	return;

}

typedef struct{

	int matr,dependentes;



	float salarioInicial,resultadoINSS,resultadoIR,resultadoFGTS,resultadoDT;



	char nome_funcionario[50] , funcao_funcionario[30], data[20];



}funcionario;



void mostra(funcionario funcionario){

		printf("\nNome do funcionario: %sFuncao do funcionario: %s\nNumero de Dependentes: %d\nData: %sMatricula do funcionario: %d\nSalario Base: %2.2f\n",funcionario.nome_funcionario,funcionario.funcao_funcionario,funcionario.dependentes,funcionario.data,funcionario.matr,funcionario.salarioInicial);

;

		funcionario.resultadoINSS = calcINSS(funcionario.salarioInicial);

		funcionario.resultadoIR = calcIR(funcionario.resultadoINSS,funcionario.salarioInicial);

		funcionario.resultadoFGTS = calcFGTS(funcionario.salarioInicial,funcionario.funcao_funcionario);

		funcionario.resultadoDT = calcDT(funcionario.resultadoFGTS,funcionario.resultadoIR,funcionario.resultadoINSS);

		calcST(funcionario.salarioInicial,funcionario.resultadoDT,funcionario.dependentes);

}



void pega(funcionario funcionario){

	

		printf("DIGITE O NOME DO FUNCIONARIO: ");



		fgets(funcionario.nome_funcionario,50,stdin);	



		printf("DIGITE A DATA: ");



		fgets(funcionario.data,20,stdin);



		printf("DIGITE A FUNCAO DO FUNCIONARIO: ");



		fgets(funcionario.funcao_funcionario,30,stdin);



		printf("DIGITE O SALARIO BASE: ");



		scanf("%f",&funcionario.salarioInicial);

		

		printf("DIGITE A QUANTIDADE DE DEPENDENTES: ");

		

		scanf("%d",&funcionario.dependentes);



		printf("DIGITE A MATRICULA DO FUNCIONARIO: ");



		scanf("%d",&funcionario.matr);



		strtok(funcionario.funcao_funcionario,"\n");

		mostra(funcionario);

}



int main(){



		funcionario funcionario;

		pega(funcionario);





    return 0;



}
