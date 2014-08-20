USE [ISISDesigner]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TransactionSnippetField')
	DROP TABLE [TransactionSnippetField]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TransactionSnippet')
	DROP TABLE [TransactionSnippet]
GO

CREATE TABLE [TransactionSnippet]
(
	[Id] INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TransactionSnippet PRIMARY KEY CLUSTERED,
	[RecordsCenter_Id] INT NULL CONSTRAINT FK_TransactionSnippet_RecordsCenter FOREIGN KEY ([RecordsCenter_Id]) REFERENCES [RecordsCenter] ([Id]),
	[Created] DATETIME NULL,
	[Updated] DATETIME NULL,
	[TokenName] VARCHAR(50) NOT NULL,
	[TransactionDefinition] VARCHAR(MAX),
	[Criteria] VARCHAR(MAX),
	[IncludePrefixAndSuffix] BIT NOT NULL CONSTRAINT DV_TransactionSnippet_IncludePrefixAndSuffix DEFAULT 1,
	[Description] VARCHAR(50)
)
GO

CREATE TABLE [TransactionSnippetField]
(
	[Id] INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TransactionSnippetField PRIMARY KEY CLUSTERED,
	[TransactionSnippet_Id] INT NULL CONSTRAINT FK_TransactionSnippetField_TransactionSnippet FOREIGN KEY ([TransactionSnippet_Id]) REFERENCES [TransactionSnippet]([Id]),
	[TagName] VARCHAR(50) NOT NULL,
	[Prefix] VARCHAR(50) NULL,
	[Suffix] VARCHAR(50) NULL,
	[ToolTip] VARCHAR(50) NULL,
	[MakeUpperCase] BIT NULL,
	[Length] INT NOT NULL,
	[PadCharacterDec] INT NULL,
	[TrimInputToLength] INT NULL,
	[DefaultValue] VARCHAR(MAX) NULL,
	[TransformFormat] VARCHAR(50) NULL,
	[FormatMask_Id] INT NULL,
	[Frequency] INT NULL,
	[Separator] VARCHAR(5) NULL
)