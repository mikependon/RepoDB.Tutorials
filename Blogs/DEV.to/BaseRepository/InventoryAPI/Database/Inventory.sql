CREATE DATABASE [Inventory];
GO

USE [Inventory];
GO

CREATE TABLE [dbo].[Customer]
(
    [Id] BIGINT IDENTITY(1,1) 
    , [Name] NVARCHAR(128) NOT NULL
    , [Address] NVARCHAR(MAX)
    , CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC )
)
ON [PRIMARY];
GO