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
, ('Heath Sessums')
, ('Celine Goold')
, ('Alline Boxx')
, ('Beula Bayes')
, ('Lindsy Trimpe')
, ('Malissa Nason')
, ('Jillian Elsey')
, ('Queen Wacker')
, ('Lupita Grande')
, ('Letha Droz')
, ('Torie Stodola')
, ('Micha Pilson')
, ('Mose Mattix')
, ('Junko Polak')
, ('Helga Froehlich')
, ('Merle Mowrer')
, ('Dot Dever')
, ('Alise Moya')
, ('Camie Slocumb')
, ('Esta Cubbage')
, ('Barbra Sane')
, ('Trula Rothenberger')
, ('Sharell Riebel')
, ('Yolonda Schaal')
, ('Roman Montas')
, ('Young Orbison')
, ('German Inskeep')
, ('Lilli Lev')
, ('Tasha Brunkhorst')
, ('Thi Marmon')
, ('Nedra Dobson');
