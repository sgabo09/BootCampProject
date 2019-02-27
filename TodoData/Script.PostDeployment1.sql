/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT [dbo].[Category] ([CategoryId], [CreationDate], [LastModified], [Name]) VALUES ('2015b663-d7ea-4662-bbb3-4c4efc075371', CAST(N'2019-02-22T10:48:29.1648641' AS DateTime2), CAST(N'2019-02-22T10:48:29.1648641' AS DateTime2), N'EPIC')
GO
INSERT [dbo].[Category] ([CategoryId], [CreationDate], [LastModified], [Name]) VALUES ('2015b663-d7ea-4662-bbb3-4c4efc075372', CAST(N'2019-02-26T10:48:29.1648641' AS DateTime2), CAST(N'2019-02-26T13:48:29.1648641' AS DateTime2), N'TASK')
GO
INSERT [dbo].[Category] ([CategoryId], [CreationDate], [LastModified], [Name]) VALUES ('2015b663-d7ea-4662-bbb3-4c4efc075373', CAST(N'2019-02-26T14:48:29.1648641' AS DateTime2), CAST(N'2019-02-26T14:48:29.1648641' AS DateTime2), N'BUG')
GO
INSERT [dbo].[Todo] ([Id], [CreationDate], [LastModified], [Name], [Description], [Priority], [Responsible], [Deadline], [Status], [CategoryId], [ParentId], [IsDeleted]) VALUES (N'7dddfd22-f70d-41ae-9040-293c81acb049', CAST(N'2019-02-22T10:48:29.1648641' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Landing page', N'Home page with header menu', 1, N'Johnny', CAST(N'2019-03-12T10:00:00.0000000' AS DateTime2), N'To Do', '2015b663-d7ea-4662-bbb3-4c4efc075371', N'00000000-0000-0000-0000-000000000000', 0)
GO
INSERT [dbo].[Todo] ([Id], [CreationDate], [LastModified], [Name], [Description], [Priority], [Responsible], [Deadline], [Status], [CategoryId], [ParentId], [IsDeleted]) VALUES (N'406024a9-750c-41e3-9cd7-320f27414b87', CAST(N'2019-02-22T10:33:29.0435381' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Facebook share feature', N'Preview', 2, N'Jack', CAST(N'2019-03-15T10:00:00.0000000' AS DateTime2), N'In Progress', '2015b663-d7ea-4662-bbb3-4c4efc075371', N'00000000-0000-0000-0000-000000000000', 1)
GO
INSERT [dbo].[Todo] ([Id], [CreationDate], [LastModified], [Name], [Description], [Priority], [Responsible], [Deadline], [Status], [CategoryId], [ParentId], [IsDeleted]) VALUES (N'2015b663-d7ea-4662-bbb3-4c4efc07537c', CAST(N'2019-02-22T10:53:39.0273667' AS DateTime2), CAST(N'2019-02-22T10:54:54.1155611' AS DateTime2), N'CSS', N'Some nice lookin'' CSS', 2, N'Johnny', CAST(N'2019-03-10T10:00:00.0000000' AS DateTime2), N'To Do', '2015b663-d7ea-4662-bbb3-4c4efc075372', N'7dddfd22-f70d-41ae-9040-293c81acb049', 0)
GO
