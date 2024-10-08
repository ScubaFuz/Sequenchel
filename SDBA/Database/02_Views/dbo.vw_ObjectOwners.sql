CREATE VIEW [dbo].[vw_ObjectOwners]
AS
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2015-07-20	BT		Initial version
-- ****************************************************************************

SELECT [ObjectOwnerID]
      ,[LinkedServer]
      ,[DatabaseName]
      ,[ObjectName]
      ,[Owner]
      ,[Type]
      ,CASE WHEN [Type] = 'Database Owner' THEN 'EXEC (''USE [' + [DatabaseName] + ']; EXEC dbo.sp_changedbowner @loginame = N''''sa'''', @map = false;'') AT [' + [LinkedServer] + '] ' ELSE '' END AS CorrectCommand
      ,[DateStart]
      ,[DateStop]
  FROM [dbo].[tbl_ObjectOwners]
;
