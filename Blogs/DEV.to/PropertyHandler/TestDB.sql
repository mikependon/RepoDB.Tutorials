CREATE DATABASE [TestDB];
GO

USE [TestDB];
GO

CREATE TABLE [dbo].[Person]
(
	[Id] BIGINT PRIMARY KEY IDENTITY(1, 1)
	, [Name] NVARCHAR(128)
	, [Age] INT
	, [ExtendedInfo] NVARCHAR(MAX)
	, [CreatedDateUtc] DATETIME2(5)
)
ON [PRIMARY];
GO