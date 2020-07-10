## Motivação

Codigo elaborado durante processo seletivo em para vaga nível Pleno/Senior, o teste engloba questões de desenvolvimento .NET C# em Console, algoritmos SQL e resolução de Bugs.



###Orientações 

1. Executar o script Script_Gera_Base_Prova.sql antes de iniciar a prova.
2. Salvar o arquivo excel Arquivo_Importacao.csv em uma pasta que você tenha permissões de leitura.



##Questões .Net C#

1.	Crie um método para importação dos dados do arquivo Arquivo_Importacao.csv na tabela “TB_DADOS_CLIENTE”; 
2. 	Criar um método que execute a Procedure “SP_RETORNA_DADOS_CLIENTE” e exiba o resultado. Obs.: A procedure está apresentando erro, ou seja, será necessário corrigir a mesma para que a aplicação funcione;
3.  Desenvolva um algoritmo que receba um número e retorne verdadeiro se for par ou falso se for ímpar. Obs.: Se o candidato optar por operações aritméticas, a única operação aritmética permitida é a soma (+), NÃO É PERMITIDO USAR MOD (%). Se for utilizado a questão invalidada;
4.  Um anagrama é a transposição de letras de palavra ou frase para formar uma outra palavra ou frase diferente (Natércia, de Catarina; amor, de Roma; Célia, de Alice; etc.). Desenvolva um algoritmo que receba duas Strings e retorne, verdadeiro se for um Anagrama, caso contrário, retorne falso. NÃO É PERMITIDO FAZER UM PALÍNDROMO.

##Questões SQL

1.	A procedure “SP_ERRO_DE_LOGICA” está com um ERRO DE LÓGICA. Corrija a mesma;
2.	Crie uma tabela com o nome “TB_DADOS” na base de dados com as seguintes colunas:
      ● ID_DADO (Inteiro e com auto incremento – preenchimento obrigatório – Chave Primária);
      ● NM_DADO (Texto com tamanho 100 – preenchimento obrigatório);
      ● BT_ATIVO (Booleano – preenchimento obrigatório);
      ● NM_USUARIO_CADASTRO (Texto com tamanho 1000 – preenchimento obrigatório);
      ● DT_CADASTRO (Data e hora – preenchimento NÃO obrigatório);
3.	Crie uma procedure com o nome “SP_INSERE_DADOS” que receba os valores da tabela criada na questão 2. A procedure deverá ATUALIZAR o registro caso o mesmo exista, caso contrário, deverá INSERIR o registro;      
4.	Criar uma view com o nome “VW_RETORNA_DADOS” que retorne os dados ATIVOS da tabela criada na questão 2;
5.	Criar uma função com o nome “FN_RETORNA_DIA_SEMANA” que receba uma data e retorne o dia da semana correspondente POR EXTENSO (segunda, terça, etc.);
6.	Tendo como base a tabela “TB_PROCESSAMENTO” (disponível no banco de dados), crie um algoritmo em SQL que irá dividir os contratos pendentes de execução em N lotes. Exemplo, se houver 100 contratos pendentes na tabela e o parâmetro for dividir em 10 lotes, a cada 10 registros na base, irá aumentar o número do lote.


## Contato ##
Linkedin: https://www.linkedin.com/in/guilhermebatistamagalhaes/
