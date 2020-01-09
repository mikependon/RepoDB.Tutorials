CREATE DATABASE [SchoolDB];
GO

USE [SchoolDB];
GO

CREATE TABLE [dbo].[Student]
(
    [Id] INT IDENTITY(1,1)
	, [TeacherId] INT 
    , [Name] NVARCHAR(128) NOT NULL
    , CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED ([Id] ASC )
)
ON [PRIMARY];
GO

CREATE TABLE [dbo].[Teacher]
(
    [Id] INT IDENTITY(1,1)
    , [Name] NVARCHAR(128) NOT NULL
    , CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED ([Id] ASC )
)
ON [PRIMARY];
GO

INSERT INTO [dbo].[Teacher]
(
	[Name]
)
VALUES
('Lurlene Laury')
, ('Wynell Kort')
, ('Alexa Dempsey')
, ('Loan Goggins')
, ('Lien Lange')
, ('Veronika Hershey')
, ('Erasmo Milo')
, ('Columbus Hadden')
, ('Lena Cendejas')
, ('Shawana Bono')
, ('Morton Jourdan')
, ('Myesha Griffin')
, ('Cassi Pelayo')
, ('Shelly Chouinard')
, ('Gabrielle Cloninger')
, ('Brandee Rominger')
, ('Kimberly Blackmore')
, ('Efren Wey')
, ('Fabiola Douse')
, ('Heath Sessums');
