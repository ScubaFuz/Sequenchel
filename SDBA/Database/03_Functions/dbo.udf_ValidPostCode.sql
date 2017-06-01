CREATE FUNCTION [dbo].[udf_ValidPostCode] (@PostCode varchar (10), @CountryCode int, @CountryCodeNL int)
RETURNS varchar(10)
AS
-- *****************************************************************************
-- Auteur       : Bart Thieme
-- Doel         : Format valid Postal Codes
-- *****************************************************************************
-- Versie Datum    Auteur Beschrijving
-- ****** ******** ****** ******************************************************
-- 01     20170522 BT     Nieuw
-- *****************************************************************************

BEGIN
	SET @PostCode = LTRIM(RTRIM(@PostCode)) -- remove leading and trailing blanks
	IF @Countrycode = 159 
		BEGIN
			SET @PostCode = REPLACE(@PostCode,' ','')
		END

	IF patindex('[0-9][0-9][0-9][0-9][A-Z][A-Z]', @PostCode) > 0 AND @Countrycode = @CountryCodeNL 
		BEGIN
			SET @PostCode = LEFT(@PostCode,4) + ' ' + RIGHT(@PostCode,2)
		END

	IF patindex('[0-9][0-9][0-9][0-9]_[A-Z][A-Z]', @PostCode) > 0 AND @Countrycode = @CountryCodeNL 
		BEGIN
			SET @PostCode = LEFT(@PostCode,4) + ' ' + RIGHT(@PostCode,2)
		END

--	if patindex('[0-9][0-9][0-9][0-9] [A-Z][A-Z]', @PostCode) > 0 AND @Countrycode = @CountryCodeNL RETURN @PostCode
	RETURN @PostCode
END
;

GO
