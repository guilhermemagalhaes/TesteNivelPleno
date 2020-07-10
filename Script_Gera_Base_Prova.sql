USE Prova

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'TB_DADOS_CLIENTE')
	DROP TABLE TB_DADOS_CLIENTE

GO

CREATE TABLE TB_DADOS_CLIENTE(
	NR_CLIENTE INT,
	TX_CPF VARCHAR(20),
	NM_CLIENTE VARCHAR(200),
	DT_NASC DATETIME)

GO

------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'TB_PROCESSAMENTO')
	DROP TABLE TB_PROCESSAMENTO

GO

CREATE TABLE TB_PROCESSAMENTO(
	NR_CONTRATO BIGINT,
	BT_EXECUTADO BIT,
	NR_LOTE INT)

GO

DECLARE @QTD INT = 1

WHILE @QTD <= 1000
BEGIN
	INSERT INTO TB_PROCESSAMENTO VALUES(@QTD, 0, NULL)
	SET @QTD = @QTD + 1
END

GO


------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'SP_RETORNA_DADOS_CLIENTE')
	DROP PROCEDURE SP_RETORNA_DADOS_CLIENTE

GO

CREATE PROCEDURE SP_RETORNA_DADOS_CLIENTE
AS
BEGIN

	SELECT
		NR_CLIENTE,
		TX_CPF,
		NM_CLIENTE,
		DT_NASC
	INTO #TEMP_DADOS
	FROM TB_DADOS_CLIENTE

	DECLARE @SSQL VARCHAR(MAX) = '
	SELECT *
	FROM TEMP_DADOS'

	EXEC(@SSQL)

END

GO


------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'SP_ERRO_DE_LOGICA')
	DROP PROCEDURE SP_ERRO_DE_LOGICA

GO

CREATE PROCEDURE SP_ERRO_DE_LOGICA
AS
BEGIN

	DECLARE @QTD INT = 1

	CREATE TABLE #TEMP_DADOS(NR_CLIENTE INT)

	WHILE @QTD <= 10
	BEGIN

		INSERT INTO #TEMP_DADOS VALUES(@QTD)

	END

END

GO


------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------


IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'TB_DADOS')
	DROP TABLE TB_DADOS

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'SP_INSERE_DADOS')
	DROP PROCEDURE SP_INSERE_DADOS

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'VW_RETORNA_DADOS')
	DROP VIEW VW_RETORNA_DADOS

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'FN_RETORNA_DIA_SEMANA')
	DROP FUNCTION FN_RETORNA_DIA_SEMANA


------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------


IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'TB_CUSTOMER')
	DROP TABLE TB_CUSTOMER

GO

CREATE TABLE TB_CUSTOMER(
	NM_COMPANY VARCHAR(200),
	NM_CONTACT VARCHAR(200),
	NM_COUNTRY VARCHAR(200))

GO