﻿CREATE TABLE [dbo].[Todo]
(
	[Id] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL PRIMARY KEY, 
	[Name] VARCHAR(50) NOT NULL, 
	[Description] NVARCHAR(500) NULL, 
	[Priority] INT NOT NULL, 
	[Responsible] NVARCHAR(50) NULL, 
	[Deadline] DATETIME2 NULL, 
	[Status] NVARCHAR(50) NULL, 
	[Category] INT NULL, 
	[ParentId] UNIQUEIDENTIFIER NULL,
	[LastModified] DATETIME2 NULL
)
