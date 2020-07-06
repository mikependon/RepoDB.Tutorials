CREATE TABLE [dbo].[Person]
(
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Age] [int] NULL,
	[ExtendedInfo] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDateUtc] [datetime2](5) NOT NULL,
 CONSTRAINT [PK__Person__3214EC0721084EB2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_CreatedDateUtc]  DEFAULT (getutcdate()) FOR [CreatedDateUtc]
GO


