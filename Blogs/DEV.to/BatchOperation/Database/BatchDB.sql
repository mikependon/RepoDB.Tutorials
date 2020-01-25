CREATE DATABASE [BatchDB];
GO

USE [BatchDB];
GO

CREATE TABLE [dbo].[Customer]
(
	[Id] INT PRIMARY KEY IDENTITY(1, 1)
	, [Name] NVARCHAR(128)
	, [SSN] NVARCHAR(64)
	, [Address] NVARCHAR(256)
	, [CreatedUtc] DATETIME2(5)
	, [ModifiedUtc] DATETIME2(5)
);
GO