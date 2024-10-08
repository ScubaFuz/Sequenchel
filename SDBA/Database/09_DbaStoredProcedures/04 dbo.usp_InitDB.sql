CREATE  PROCEDURE [dbo].[usp_InitDB]
WITH ENCRYPTION
AS 
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 01		2008-09-30	BT		Initial version
--
-- ****************************************************************************
BEGIN

	select  CAST(o.name AS VARCHAR(32)) AS NAME
		, i.rows
	from  sysobjects o 
	inner join sysindexes i 
		on (o.id = i.id)
	where   i.indid < 2
		and OBJECTPROPERTY(o.id, N'IsMSShipped') <> 1 
		and   OBJECTPROPERTY(o.id, N'IsSystemTable') = 0
		and o.xtype='U'
	order by o.name

	exec usp_disable_all_const_in_db
	exec usp_disable_all_Triggers_in_db
	exec usp_Truncate_all_Tables_in_db
	exec usp_enable_all_const_in_db
	exec usp_enable_all_Triggers_in_db

	select  CAST(o.name AS VARCHAR(32)) AS NAME
		, i.rows
	from  sysobjects o 
	inner join sysindexes i 
		on (o.id = i.id)
	where   i.indid < 2
		and OBJECTPROPERTY(o.id, N'IsMSShipped') <> 1 
		and   OBJECTPROPERTY(o.id, N'IsSystemTable') = 0
		and o.xtype='U'
	order by o.name
END
;