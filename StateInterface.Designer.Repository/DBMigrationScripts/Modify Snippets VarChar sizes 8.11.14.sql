USE [ISISDesigner]
GO

ALTER TABLE [TransactionSnippetField]
DROP CONSTRAINT FK_TransactionSnippetField_TransactionSnippet
GO

IF OBJECT_ID('TransactionSnippet_New', 'U') IS NOT NULL
	DROP TABLE [TransactionSnippet_New]

CREATE TABLE [TransactionSnippet_New]
(
	[Id] INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TransactionSnippet2 PRIMARY KEY CLUSTERED,
	[RecordsCenter_Id] INT NULL CONSTRAINT FK_TransactionSnippet_RecordsCenter2 FOREIGN KEY ([RecordsCenter_Id]) REFERENCES [RecordsCenter] ([Id]),
	[Created] DATETIME NULL,
	[Updated] DATETIME NULL,
	[TokenName] VARCHAR(50) NOT NULL,
	[TransactionDefinition] VARCHAR(2000),
	[Criteria] VARCHAR(512),
	[IncludePrefixAndSuffix] BIT NOT NULL CONSTRAINT DV_TransactionSnippet_IncludePrefixAndSuffix2 DEFAULT 1,
	[Description] VARCHAR(100)
)
GO
SET IDENTITY_INSERT [TransactionSnippet_New] OFF
SET IDENTITY_INSERT [TransactionSnippet_New] ON

INSERT INTO [TransactionSnippet_New] (Id, RecordsCenter_Id, Created, Updated, TokenName, TransactionDefinition, Criteria, IncludePrefixAndSuffix, [Description])
SELECT * FROM [TransactionSnippet]

SET IDENTITY_INSERT [TransactionSnippet_New] OFF

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TransactionSnippet')
	DROP TABLE [TransactionSnippet]
GO

EXEC sp_rename 'TransactionSnippet_New', 'TransactionSnippet'
EXEC sp_rename 'PK_TransactionSnippet2', 'PK_TransactionSnippet'
EXEC sp_rename 'FK_TransactionSnippet_RecordsCenter2', 'FK_TransactionSnippet_RecordsCenter'
EXEC sp_rename 'DV_TransactionSnippet_IncludePrefixAndSuffix2', 'DV_TransactionSnippet_IncludePrefixAndSuffix'

ALTER TABLE [TransactionSnippetField]
ADD CONSTRAINT FK_TransactionSnippetField_TransactionSnippet FOREIGN KEY ([TransactionSnippet_Id]) REFERENCES [TransactionSnippet]([Id])

USE [ISISDesigner]
GO

IF OBJECT_ID('TransactionSnippetField_New', 'U') IS NOT NULL
	DROP TABLE [TransactionSnippetField_New]
CREATE TABLE [TransactionSnippetField_New]
(
	[Id] INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TransactionSnippetField2 PRIMARY KEY CLUSTERED,
	[TransactionSnippet_Id] INT NULL CONSTRAINT FK_TransactionSnippetField_TransactionSnippet2 FOREIGN KEY ([TransactionSnippet_Id]) REFERENCES [TransactionSnippet]([Id]),
	[TagName] VARCHAR(50) NOT NULL,
	[Prefix] VARCHAR(250) NULL,
	[Suffix] VARCHAR(250) NULL,
	[ToolTip] VARCHAR(100) NULL,
	[MakeUpperCase] BIT NULL,
	[AcceptCarriageReturns] BIT NULL,
	[Length] INT NOT NULL,
	[PadCharacterDec] INT NULL,
	[TrimInputToLength] INT NULL,
	[DefaultValue] VARCHAR(50) NULL,
	[TransformFormat] VARCHAR(50) NULL,
	[FormatMask_Id] INT NULL,
	[Frequency] INT NULL,
	[Separator] VARCHAR(5) NULL
)

SET IDENTITY_INSERT [TransactionSnippetField_New] ON

INSERT INTO [TransactionSnippetField_New] 
	(Id, 
	TransactionSnippet_Id, 
	TagName, 
	Prefix, 
	Suffix, 
	Tooltip, 
	MakeUpperCase,
	[AcceptCarriageReturns], 
	[Length], 
	[PadCharacterDec], 
	[TrimInputToLength], 
	[DefaultValue], 
	[TransformFormat], 
	[FormatMask_Id], 
	[Frequency], 
	[Separator])
SELECT Id, 
	TransactionSnippet_Id, 
	TagName, 
	Prefix, 
	Suffix, 
	Tooltip, 
	MakeUpperCase,
	0, 
	[Length], 
	[PadCharacterDec], 
	[TrimInputToLength], 
	[DefaultValue], 
	[TransformFormat], 
	[FormatMask_Id], 
	[Frequency], 
	[Separator] FROM [TransactionSnippetField]

SET IDENTITY_INSERT [TransactionSnippetField_New] OFF

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TransactionSnippetField')
	DROP TABLE [TransactionSnippetField]
GO

EXEC sp_rename 'TransactionSnippetField_New', 'TransactionSnippetField'
EXEC sp_rename 'PK_TransactionSnippetField2', 'PK_TransactionSnippetField'
EXEC sp_rename 'FK_TransactionSnippetField_TransactionSnippet2', 'FK_TransactionSnippetField_TransactionSnippet'

USE [ISISDesigner]
GO

IF OBJECTPROPERTY(OBJECT_ID('[UK_RecordsCenter_TokenName]'), 'IsConstraint') = 1
	/****** Object:  Index [UK_RecordsCenter_TokenName]    Script Date: 8/8/2014 11:40:51 AM ******/
	ALTER TABLE [dbo].[TransactionSnippet] DROP CONSTRAINT [UK_RecordsCenter_TokenName]
GO

/****** Object:  Index [UK_RecordsCenter_TokenName]    Script Date: 8/8/2014 11:40:51 AM ******/
ALTER TABLE [dbo].[TransactionSnippet] ADD  CONSTRAINT [UK_RecordsCenter_TokenName] UNIQUE NONCLUSTERED 
(
	[RecordsCenter_Id] ASC,
	[TokenName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF OBJECTPROPERTY(OBJECT_ID('[DV_UPDATED]'), 'IsConstraint') = 1
	ALTER TABLE [dbo].[TransactionSnippet] DROP CONSTRAINT [DV_UPDATED];
GO

IF OBJECTPROPERTY(OBJECT_ID('[DV_CREATED]'), 'IsConstraint') = 1
	ALTER TABLE [dbo].[TransactionSnippet] DROP CONSTRAINT [DV_CREATED];
GO

ALTER TABLE [dbo].[TransactionSnippet] ADD CONSTRAINT [DV_UPDATED] DEFAULT (GETUTCDATE()) FOR [UPDATED];
GO
ALTER TABLE [dbo].[TransactionSnippet] ADD CONSTRAINT [DV_CREATED] DEFAULT (GETUTCDATE()) FOR [CREATED];
GO

