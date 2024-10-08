CREATE VIEW [dbo].[vw_DiskUseDaysLeft]
AS
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 01		2008-09-30	BT		Initial version
-- 02		2012-07-23	BT		Added DISTINCT to joins
-- ****************************************************************************
SELECT
	CONVERT(varchar(30),DS9.[LinkedServer]) AS [LinkedServer], 
	CONVERT(varchar(5),DS9.[Drive]) AS [Drive], 
	CONVERT(varchar(9),DS6.[FreeMax]) AS [FreeMax], 
	CONVERT(varchar(19),DS6.[MaxDate]) AS [MaxDate], 
	CONVERT(varchar(9),DS5.[Free8]) AS [Free8], 
	CONVERT(varchar(19),DS5.[Min8Date]) AS [Min8Date], 
	CONVERT(varchar(5),CONVERT(INT,MaxDate-Min8Date)) AS Days8, 
	CONVERT(varchar(9),Free8-FreeMax) AS [Grow8], 
	CONVERT(VARCHAR(9),
		CONVERT(INT,
			ROUND(
				FreeMax /
				(CONVERT(dec(8,2),(Free8-FreeMax))) *
				CONVERT(INT,MaxDate-Min8Date)
			,0)
		)
	) AS [DaysLeft8],
	CONVERT(VARCHAR(9),DS4.[Free30]) AS [Free30],  
	CONVERT(VARCHAR(19),DS4.[Min30Date]) AS [Min30Date],  
	CONVERT(VARCHAR(9),CONVERT(INT,MaxDate-Min30Date)) AS [Days30], 
	CONVERT(VARCHAR(9),Free30-FreeMax) AS [Grow30],
	CONVERT(VARCHAR(9),
		CONVERT(INT,
			ROUND(
				FreeMax /
				(CONVERT(dec(8,2),(Free30-FreeMax))) *
				CONVERT(INT,MaxDate-Min30Date)
			,0)
		)
	) AS [DaysLeft30]
FROM [dbo].[tbl_DiskSpaces] DS9
INNER JOIN
	(SELECT DISTINCT DS1.LinkedServer, DS1.Drive, DS1.FreeSpaceMB AS Free30, DS1.DateLogged AS Min30Date
	FROM [dbo].[tbl_DiskSpaces] DS1
	WHERE DateLogged = (SELECT MIN(DateLogged)
							FROM [dbo].[tbl_DiskSpaces]
							WHERE DateLogged > getdate() -30))DS4
ON DS9.LinkedServer = DS4.LinkedServer and DS9.Drive = DS4.Drive
INNER JOIN
	(SELECT DISTINCT DS2.LinkedServer, DS2.Drive, DS2.FreeSpaceMB AS Free8, DS2.DateLogged AS Min8Date
	FROM [dbo].[tbl_DiskSpaces] DS2
	WHERE DateLogged = (SELECT MIN(DateLogged)
							FROM [dbo].[tbl_DiskSpaces]
							WHERE DateLogged > getdate() -8)) DS5
ON DS9.LinkedServer = DS5.LinkedServer and DS9.Drive = DS5.Drive
INNER JOIN
	(SELECT DISTINCT DS3.LinkedServer, DS3.Drive, DS3.FreeSpaceMB AS FreeMax, DS3.DateLogged AS MaxDate
	FROM [dbo].[tbl_DiskSpaces] DS3
	WHERE DateLogged = (SELECT MAX(DateLogged)
							FROM [dbo].[tbl_DiskSpaces])) DS6
ON DS9.LinkedServer = DS6.LinkedServer and DS9.Drive = DS6.Drive
WHERE Free30-FreeMax > 0  --when days is negative, diskspace has increased
	AND Free8-FreeMax > 0  --when days is zero, there was no grow
	AND ((CONVERT(INT,
			Round(
				FreeMax /
				(CONVERT(dec(8,2),(Free8-FreeMax))) *
				CONVERT(INT,MaxDate-Min8Date)
			,0)
		) < 30 --8Day period must be smaller than 30 to report
	AND CONVERT(INT,
			Round(
				FreeMax /
				(CONVERT(dec(8,2),(Free30-FreeMax))) *
				CONVERT(INT,MaxDate-Min30Date)
			,0)
		) < 30)  --30 day period must be smaller than 30 to report
	OR (CONVERT(INT,
			Round(
				FreeMax /
				(CONVERT(dec(8,2),(Free8-FreeMax))) *
				CONVERT(INT,MaxDate-Min8Date)
			,0)
		) < 10
	OR CONVERT(INT,
			Round(
				FreeMax /
				(CONVERT(dec(8,2),(Free30-FreeMax))) *
				CONVERT(INT,MaxDate-Min30Date)
			,0)
		) < 10))  --If any period falls below 10 days: report, no matter what the other period is.
GROUP BY DS9.LinkedServer, DS9.Drive, DS6.FreeMax, DS6.MaxDate, DS5.Free8, DS5.Min8Date, DS4.Free30, DS4.Min30Date;
