CREATE VIEW [dbo].[vw_Servers]
AS
-- ****************************************************************************
-- Author	Bart Thieme
-- Purpose	
-- ****************************************************************************
-- Version	Date		Author	Description
-- *******	**********	******	***********************************************
-- 1.0		2015-02-25	BT		Initial version
-- 3.2		2015-06-17	BT		Added WindowsVersion
-- 3.4		2015-11-10	BT		Added Min & Max Server Memory
-- ****************************************************************************
SELECT [ServerID]
	,[LinkedServer]
	,[LocalServerName]
	,[DataSource]
	,[PromotionOn]
	,[MachineName]
	,[Domain]
	,[InstanceName]
	,[Edition]
	,[SQLServerComponents]
	,[SQLVersion]
	,[SQLVersionLong]
	,[SQLVersionText]
	,[SQLServerLicenseType]
	,[SPLevel]
	,[Collation]
	,[IsClustered]
	,[WindowsAuthentication]
	,[FileStreamEnabled]
	,[WindowsVersion]
	,[IP_Address]
	,[Port]
	,[Logical_CPU_Count]
	,[Hyperthread_Ratio]
	,[Physical_CPU_Count]
	,[Physical_Memory_MB]
	,[MinServerMemory]
	,[MaxServerMemory]
	,[Owner]
	,[Application]
	,[Location]
	,[Remarks]
	,[MonitorServer]
	,[MonitorContent]
	,[AutoAdded]
	,[Active]
	,[Available]
	,[LastFound]
	,[DateStart]
	,[DateStop]
FROM [dbo].[tbl_Servers]
WHERE COALESCE([DateStop],GETDATE()+1) > GETDATE();
