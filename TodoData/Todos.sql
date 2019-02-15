CREATE TABLE [dbo].[Todos]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	[Name] VARCHAR(50) NOT NULL, 
	[Description] VARCHAR(500) NULL, 
	[Priority] INT NOT NULL, 
	[Responsible] VARCHAR(50) NULL, 
	[Deadline] DATETIME NULL, 
	[Status] VARCHAR(50) NULL, 
	[Category] INT NULL, 
	[ParentId] INT NULL, 
)
