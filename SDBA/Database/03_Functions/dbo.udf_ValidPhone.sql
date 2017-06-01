CREATE FUNCTION [dbo].[udf_ValidPhone] (@Phone varchar (100))
RETURNS BIT
AS
-- *****************************************************************************
-- Auteur       : Bart Thieme
-- Doel         : Filter valid Phone numbers
-- *****************************************************************************
-- Versie Datum    Auteur Beschrijving
-- ****** ******** ****** ******************************************************
-- 01     20170522 BT     Nieuw
-- *****************************************************************************

BEGIN
	DECLARE @Return as int = 0, @Remain as varchar(100)
	IF @Phone is null return 0

	SET @Phone = LTRIM(RTRIM(@Phone)) -- remove leading and trailing blanks
	SET @Phone = REPLACE(@Phone,' ','') -- remove blanks
	SET @Phone = REPLACE(@Phone,'(','') -- remove brackets
	SET @Phone = REPLACE(@Phone,')','') -- remove brackets
	IF LEN(@Phone) <= 2 RETURN(0) -- nothing to validate
	IF patindex ('[-]%', @Phone) > 0 RETURN(0)   -- Valid but cannot be starting character

	DECLARE @GoodStrings VARCHAR(100)
	SET @GoodStrings = '0123456789-'

	;WITH CTE AS
	(
	  SELECT SUBSTRING(@GoodStrings, 1, 1) AS [String], 1 AS [Start], 1 AS [Counter]
	  UNION ALL
	  SELECT SUBSTRING(@GoodStrings, [Start] + 1, 1) AS [String], [Start] + 1, [Counter] + 1 
	  FROM CTE 
	  WHERE [Counter] < LEN(@GoodStrings)
	)
	SELECT @Phone = REPLACE(@Phone, CTE.[String], '') FROM CTE

	IF LEN(@Phone) = 0 RETURN(1) 
	RETURN(0)

END
;

GO
