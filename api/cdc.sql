USE NeZoviReg
GO 


--enable CDC on the db level
EXEC sys.sp_cdc_enable_db  
GO


--verication of the previous step
SELECT name, is_cdc_enabled
FROM sys.databases WHERE database_id = DB_ID();

--enable  CDC on the Table level
EXEC sys.sp_cdc_enable_table  
@source_schema = N'dbo',  
@source_name   = N'User',  
@role_name     = N'sa',  
@supports_net_changes = 1  
GO

EXEC sys.sp_cdc_enable_table  
@source_schema = N'dbo',  
@source_name   = N'RegUser',  
@role_name     = N'sa',  
@supports_net_changes = 1  
GO

EXEC sys.sp_cdc_enable_table  
@source_schema = N'dbo',  
@source_name   = N'RegUserRole',  
@role_name     = N'sa',  
@supports_net_changes = 1  
GO

--usage examples
SELECT * FROM [cdc].[dbo_User_CT] GO
SELECT * FROM [cdc].[dbo_RegUser_CT] GO
SELECT * FROM [cdc].[dbo_RegUserRole_CT] GO

GO

--disable CDC on the Table leve
EXEC sys.sp_cdc_disable_table  
@source_schema = N'dbo',  
@source_name   = N'User',  
@capture_instance = N'dbo_User'  
GO  

EXEC sys.sp_cdc_disable_table  
@source_schema = N'dbo',  
@source_name   = N'RegUser',  
@capture_instance = N'dbo_RegUser'  
GO 

EXEC sys.sp_cdc_disable_table  
@source_schema = N'dbo',  
@source_name   = N'RegUserRole',  
@capture_instance = N'dbo_RegUserRole'  
GO 

--verication of the previous step
SELECT name, is_cdc_enabled
FROM sys.databases WHERE database_id = DB_ID();



--USE [NeZoviReg]
--GO

--declare @begin binary(10),
--@end binary(10);
--set @begin = sys.fn_cdc_get_min_lsn('dbo.RegUser');
--set @end = sys.fn_cdc_get_max_lsn();

--SELECT * FROM [cdc].[fn_cdc_get_all_changes_dbo_RegUser] (
--    @begin,
--    @end
--  ,N'ALL')
--GO
