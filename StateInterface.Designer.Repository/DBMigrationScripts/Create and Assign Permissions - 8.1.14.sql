USE [ISISDesigner]
GO
DELETE FROM [dbo].[UserPermission]
GO 
DELETE FROM [dbo].[Permission]
GO 
INSERT INTO [dbo].[Permission] ([PermissionName], [Description])
VALUES 
('DesignView','Can View Design'),
('DesignManage','Can Manage Design'),
('CertifyView','Can View Certify'),
('CertifyManage','Can Manage Certify')
GO
DELETE FROM [dbo].[UserPermission]
GO
INSERT INTO [dbo].[UserPermission]([Permission_id], [User_id])
SELECT [dbo].[Permission].[Id] as [Permission_id], [dbo].[User].Id as [User_Id] from [dbo].[Permission] cross join [dbo].[User] 
