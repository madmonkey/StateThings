USE [ISISDesigner]
GO

/****** Object:  Table [dbo].[Applications]    Script Date: 7/24/2014 2:55:06 PM ******/
if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Applications')
	DROP TABLE [dbo].[Applications]
GO

/****** Object:  Table [dbo].[Application]    Script Date: 7/24/2014 2:55:06 PM ******/
if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Application')
	DROP TABLE [dbo].[Application]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 7/24/2014 2:55:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Application](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) ON [PRIMARY]

GO

USE [ISISDesigner]
GO

/****** Object:  Table [dbo].[AssociatedApplications]    Script Date: 7/24/2014 2:55:11 PM ******/
if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'AssociatedApplications')
	DROP TABLE [dbo].[AssociatedApplications]
GO

/****** Object:  Table [dbo].[RequestFormApplication]    Script Date: 7/24/2014 2:55:11 PM ******/
if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'RequestFormApplication')
	DROP TABLE [dbo].[RequestFormApplication]
GO

/****** Object:  Table [dbo].[RequestFormApplication]    Script Date: 7/24/2014 2:55:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RequestFormApplication](
	[Application_Id] [int] NOT NULL,
	[RequestForm_Id] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[RequestForm_Id] ASC,
	[Application_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
 
) ON [PRIMARY]

GO

INSERT INTO [dbo].[Application] ([Name], [Description])
VALUES 
('CAD', 'OneSolutionCAD'),
('MCT', 'OneSolutionMCT'),
('RMS', 'OneSolutionRMS');
GO

USE [ISISDesigner]
GO

if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'UserPermission')
DROP TABLE [dbo].[UserPermission]
GO
if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'UserRole')
DROP TABLE [dbo].[UserRole]
GO

if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Permission')
DROP TABLE [dbo].[Permission]
GO

if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Role')
DROP TABLE [dbo].[Role]
GO

if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'User')
DROP TABLE [dbo].[User]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Permission]    Script Date: 7/28/2014 11:47:18 AM ******/
CREATE TABLE [dbo].[Permission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [ISISDesigner]
GO

/****** Object:  Table [dbo].[Role]    Script Date: 7/28/2014 11:47:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [ISISDesigner]
GO

/****** Object:  Table [dbo].[User]    Script Date: 7/28/2014 11:48:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[UserPermission]    Script Date: 7/28/2014 11:48:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserPermission](
	[Permission_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK8CB9742930C8DF6E] FOREIGN KEY([User_id])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK8CB9742930C8DF6E]
GO

ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK8CB97429D9556243] FOREIGN KEY([Permission_id])
REFERENCES [dbo].[Permission] ([Id])
GO

ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK8CB97429D9556243]
GO

USE [ISISDesigner]
GO

/****** Object:  Table [dbo].[UserRole]    Script Date: 7/28/2014 12:00:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserRole](
	[User_id] [int] NOT NULL,
	[Role_id] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK297C0BDE30C8DF6E] FOREIGN KEY([User_id])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK297C0BDE30C8DF6E]
GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK297C0BDE8D252094] FOREIGN KEY([Role_id])
REFERENCES [dbo].[Role] ([Id])
GO

ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK297C0BDE8D252094]
GO

INSERT INTO [dbo].[User] ([LoginName], [Name])
VALUES 
('lkm\thomas.monico', 'Thomas Monico'),
('lkm\camilo.motta', 'Camilo Motta'),
('lkm\chase.flavin', 'Chase Flavin'),
('lkm\leonardo.leal', 'Leo Leal'),
('lkm\claudia.cortes', 'Claudia Cortes'),
('lkm\bruce.webb', 'Bruce Webb'),
('lkm\randall.thornton', 'Randy Thornton'),
('lkm\nick.recchi', 'Nick Recchi'),
('lkm\linda.ohbayashi', 'Linda Ohbayashi'),
('lkm\julio.castaneda', 'Julio Castaneda'),
('lkm\jose.figueiredo', 'Jose Figueiredo'),
('lkm\j.p.morales', 'J.P Morales'),
('lkm\andrew.delpreore', 'Andrew Del Preore'),
('lkm\kim.do', 'Kim Do'),
('lkm\rajani.desai', 'Rajani Desai'),
('lkm\tom.flynn', 'Thomas Flynn')
GO

INSERT INTO [dbo].[Role] ([Name], [Description])
VALUES 
('StateConnectTeam','StateConnectTeam')
GO

INSERT INTO [dbo].[UserRole] ([User_id], [Role_id])
SELECT Id, (Select Id from [Role] Where Name = 'StateConnectTeam') FROM [User]
GO
