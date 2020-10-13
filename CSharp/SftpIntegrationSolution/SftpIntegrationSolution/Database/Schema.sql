CREATE TABLE [dbo].[Person2]
(
	[Id] [bigint] PRIMARY KEY CLUSTERED IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Gender] [nvarchar](16) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Age] [int] NULL,
	[ExtendedInfo] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDateUtc] [datetime2](5) NOT NULL
)
ON [PRIMARY];