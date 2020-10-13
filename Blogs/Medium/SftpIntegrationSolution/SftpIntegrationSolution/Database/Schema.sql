CREATE TABLE [dbo].[Person]
(
	[Id] [bigint] PRIMARY KEY CLUSTERED IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](64) NOT NULL,
	[LastName] [nvarchar](64) NOT NULL,
	[SSN] [nvarchar](128) NOT NULL,
	[CreatedDateUtc] [datetime2](5) NOT NULL
)
ON [PRIMARY];