USE [RepoDB]
GO
SET IDENTITY_INSERT [dbo].[SignalSource] ON 
GO
INSERT [dbo].[SignalSource] ([Id], [Name]) VALUES (1, N'Agent/Technician')
GO
INSERT [dbo].[SignalSource] ([Id], [Name]) VALUES (2, N'Default/System')
GO
INSERT [dbo].[SignalSource] ([Id], [Name]) VALUES (3, N'Serverless/Functions')
GO
INSERT [dbo].[SignalSource] ([Id], [Name]) VALUES (4, N'Mobile Application')
GO
INSERT [dbo].[SignalSource] ([Id], [Name]) VALUES (5, N'Web Application')
GO
INSERT [dbo].[SignalSource] ([Id], [Name]) VALUES (6, N'Desktop Application')
GO
SET IDENTITY_INSERT [dbo].[SignalSource] OFF
GO
SET IDENTITY_INSERT [dbo].[SignalType] ON 
GO
INSERT [dbo].[SignalType] ([Id], [Name]) VALUES (1, N'System/Reset')
GO
INSERT [dbo].[SignalType] ([Id], [Name]) VALUES (2, N'Velocity')
GO
INSERT [dbo].[SignalType] ([Id], [Name]) VALUES (3, N'Temperature')
GO
INSERT [dbo].[SignalType] ([Id], [Name]) VALUES (4, N'Windspeed')
GO
INSERT [dbo].[SignalType] ([Id], [Name]) VALUES (5, N'Distance')
GO
INSERT [dbo].[SignalType] ([Id], [Name]) VALUES (6, N'Height')
GO
INSERT [dbo].[SignalType] ([Id], [Name]) VALUES (7, N'Weight')
GO
SET IDENTITY_INSERT [dbo].[SignalType] OFF
GO
ALTER TABLE [dbo].[Signal] ADD  CONSTRAINT [DF_Signal_CreatedDateUtc]  DEFAULT (getutcdate()) FOR [CreatedDateUtc]
GO
