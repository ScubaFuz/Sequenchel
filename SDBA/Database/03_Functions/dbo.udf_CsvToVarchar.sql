CREATE Function [dbo].[udf_CsvToVarchar] ( @Array varchar(4000),@separator char(1) = ',')
returns @IntTable table (IntValue nvarchar(4000))
AS
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	Convert comma seperated list to VARCHAR table
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2008-09-30	BT		Initial version
-- 2.0		2014-12-24	BT		Made separator flexible
-- ****************************************************************************
begin
        IF COALESCE(@separator,'') = '' set @separator = ','
        declare @separator_position int
        declare @array_value varchar(4000)
        set @array = @array + @separator
        while patindex('%' + @separator + '%' , @array) <> 0
        begin
          select @separator_position =  patindex('%' + @separator + '%' , @array)
          select @array_value = left(@array, @separator_position - 1)
                Insert @IntTable
                Values (Cast(
							Replace(
									Replace(
											RTrim(
													LTrim(@array_value)
													)
											, Char(13), '')
									, Char(10), '')
							as nvarchar(100))
						)
          select @array = stuff(@array, 1, @separator_position, '')
        end
        return
end;
