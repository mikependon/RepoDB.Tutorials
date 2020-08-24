CREATE TABLE [dbo].[Person]
(
	[Id] [bigint] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Age] [int] NULL,
	[Address] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL
)
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
