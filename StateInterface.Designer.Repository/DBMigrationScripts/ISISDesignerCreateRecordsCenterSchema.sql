/*
Run this script on:

        (local).OlympiaISISDesigner    -  This database will be modified

to synchronize it with:

        (local).ISISDesigner

You are recommended to back up your database before running this script

Script created by SQL Compare version 10.2.0 from Red Gate Software Ltd at 3/11/2013 1:46:33 PM

*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Dropping constraints from [dbo].[FieldCategory]'
GO
ALTER TABLE [dbo].[FieldCategory] DROP CONSTRAINT [PK_FieldCategory]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[FieldState]'
GO
ALTER TABLE [dbo].[FieldState] DROP CONSTRAINT [PK_FieldState]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping constraints from [dbo].[RequestFormState]'
GO
ALTER TABLE [dbo].[RequestFormState] DROP CONSTRAINT [PK_RequestFormState]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[RequestFormState]'
GO
DROP TABLE [dbo].[RequestFormState]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[FieldState]'
GO
DROP TABLE [dbo].[FieldState]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Dropping [dbo].[FieldCategory]'
GO
DROP TABLE [dbo].[FieldCategory]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[RequestForm]'
GO
ALTER TABLE [dbo].[RequestForm] ADD
[RecordsCenter_Id] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[Field]'
GO
ALTER TABLE [dbo].[Field] ADD
[RecordsCenter_Id] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[Header]'
GO
ALTER TABLE [dbo].[Header] ADD
[RecordsCenter_Id] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[OptionList]'
GO
ALTER TABLE [dbo].[OptionList] ADD
[RecordsCenter_Id] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[RecordsCenter]'
GO
CREATE TABLE [dbo].[RecordsCenter]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[NetPort] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_System] on [dbo].[RecordsCenter]'
GO
ALTER TABLE [dbo].[RecordsCenter] ADD CONSTRAINT [PK_System] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[RecordsCenterState]'
GO
CREATE TABLE [dbo].[RecordsCenterState]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[RecordsCenter_Id] [int] NULL,
[State_Id] [int] NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_SystemState] on [dbo].[RecordsCenterState]'
GO
ALTER TABLE [dbo].[RecordsCenterState] ADD CONSTRAINT [PK_SystemState] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO

IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
SET IDENTITY_INSERT [dbo].[RecordsCenter] ON 
GO
INSERT [dbo].[RecordsCenter] ([Id], [Name], [Description], [NetPort]) VALUES (1, N'ACCESS', N'Washingiton Records System', N'StxEtxNetport')
GO
INSERT [dbo].[RecordsCenter] ([Id], [Name], [Description], [NetPort]) VALUES (2, N'LEDS', N'Oregon Records System', N'DmppNetport')
GO
SET IDENTITY_INSERT [dbo].[RecordsCenter] OFF
GO

IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
UPDATE dbo.RequestForm SET [RecordsCenter_Id] = 1
UPDATE dbo.Field SET [RecordsCenter_Id] = 1
UPDATE dbo.Header SET [RecordsCenter_Id] = 1
UPDATE dbo.OptionList SET [RecordsCenter_Id] = 1

IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
