CREATE PROCEDURE [dbo].[usp_ConfigHandle]
	@Action nchar(3),
	@Category nvarchar(50) = NULL,
	@ConfigId int = NULL,
	@ConfigName nvarchar(50) = NULL,
	@ConfigValue nvarchar(100) = NULL,
	@DateChange smalldatetime = NULL,
	@Remarks nvarchar(250) = NULL
WITH ENCRYPTION
AS

-- ****************************************************************************
-- comment: sp_password
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose		
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2010-01-01	BT		Initial Version
-- 2.0		2013-09-22	BT		Replaced Delete with inactivate
--								Replaced IF/OR with ISNULL
-- ****************************************************************************

IF @Action = 'Upd'
	BEGIN
		IF ISNULL(@ConfigId,0) <> 0
			BEGIN
				UPDATE [dbo].[tbl_Configuration]
				   SET [Category] = @Category
					  ,[ConfigName] = @ConfigName
					  ,[ConfigValue] = @ConfigValue
					  ,[DateChange] = @DateChange
					  ,[Remarks] = @Remarks
				 WHERE [ConfigId] = @ConfigId
			END
	END

IF @Action = 'Ins'
	BEGIN
		INSERT INTO [dbo].[tbl_Configuration]
			([Category]
			,[ConfigName]
			,[ConfigValue]
			,[DateChange]
			,[Remarks])
		VALUES
			(@Category
			,@ConfigName
			,@ConfigValue
			,@DateChange
			,@Remarks)
	END

IF @Action = 'Get'
	BEGIN
		SELECT [ConfigId]
			,[Category]
			,[ConfigName]
			,[ConfigValue]
			,[DateChange]
			,[Remarks]
		FROM [dbo].[tbl_Configuration]
		WHERE [Active] = 1
			AND ISNULL(@ConfigId,0) IN ([ConfigId],0)
			AND ISNULL(@Category,'') IN ([Category],'')
			AND ISNULL(@ConfigName,'') IN ([ConfigName],'')
	END

IF @Action = 'Del'
	BEGIN
		IF NOT (@ConfigId IS NULL OR @ConfigId = 0)
			BEGIN
				UPDATE [dbo].[tbl_Configuration]
				SET [Active] = 0
				WHERE [ConfigId] = @ConfigId
			END
	END

IF @Action = 'Smt'
	BEGIN

		DECLARE @Return int
		SET @Return = (SELECT [ConfigId]
						FROM [dbo].[tbl_Configuration]
						WHERE [Category] = @Category 
							AND [ConfigName] = @ConfigName
							AND [Active] = 1)

		IF ISNULL(@Return,0) = 0
			BEGIN
				--Insert
				SET @ConfigId = NULL
				EXEC [dbo].[usp_ConfigHandle] 'Ins', @Category, @ConfigId, @ConfigName, @ConfigValue, @DateChange, @Remarks
			END
		ELSE
			BEGIN
				--IF @Return > 0
				--	BEGIN
						--Update
						EXEC [dbo].[usp_ConfigHandle] 'Upd', @Category, @Return, @ConfigName, @ConfigValue, @DateChange, @Remarks
					--END
			END
	END
;
