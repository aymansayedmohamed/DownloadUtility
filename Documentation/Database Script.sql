USE [DownloadUtility]
GO
/****** Object:  Table [dbo].[DownloadedFiles]    Script Date: 1/29/2019 2:10:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DownloadedFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileRemotePath] [varchar](max) NULL,
	[LocalPath] [varchar](max) NULL,
	[ProcessingStatus] [int] NULL,
 CONSTRAINT [PK_DownloadedFiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessingStatus]    Script Date: 1/29/2019 2:10:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessingStatus](
	[Id] [int] NOT NULL,
	[Status] [varchar](20) NULL,
 CONSTRAINT [PK_ProcessingStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ProcessingStatus] ([Id], [Status]) VALUES (1, N'ReadyForProcessing')
INSERT [dbo].[ProcessingStatus] ([Id], [Status]) VALUES (2, N'Approved')
INSERT [dbo].[ProcessingStatus] ([Id], [Status]) VALUES (3, N'Rejected')
ALTER TABLE [dbo].[DownloadedFiles]  WITH CHECK ADD  CONSTRAINT [FK_DownloadedFiles_ProcessingStatus] FOREIGN KEY([ProcessingStatus])
REFERENCES [dbo].[ProcessingStatus] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DownloadedFiles] CHECK CONSTRAINT [FK_DownloadedFiles_ProcessingStatus]
GO
