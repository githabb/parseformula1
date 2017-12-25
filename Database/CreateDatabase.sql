USE [master]
GO

/****** Object:  Database [ParsingRace]    Script Date: 26.12.2017 0:40:20 ******/
CREATE DATABASE [ParsingRace]
GO

USE [ParsingRace]
GO
/****** Object:  Table [dbo].[Pilot]    Script Date: 26.12.2017 0:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pilot](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[ShortName] [nvarchar](10) NOT NULL,
	[TeamId] [int] NOT NULL,
 CONSTRAINT [PK_Pilot_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Race]    Script Date: 26.12.2017 0:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Race](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RaceName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Race] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Result]    Script Date: 26.12.2017 0:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Result](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Laps] [int] NOT NULL,
	[Time] [time](7) NULL,
	[Retired] [time](7) NOT NULL,
	[Pts] [int] NOT NULL,
	[Pos] [nvarchar](5) NOT NULL,
	[PilotId] [int] NOT NULL,
	[RaceId] [int] NOT NULL,
 CONSTRAINT [PK_Result] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Team]    Script Date: 26.12.2017 0:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Car] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Pilot]  WITH CHECK ADD  CONSTRAINT [FK_Pilot_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Id])
GO
ALTER TABLE [dbo].[Pilot] CHECK CONSTRAINT [FK_Pilot_Team]
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [FK_Result_Pilot] FOREIGN KEY([PilotId])
REFERENCES [dbo].[Pilot] ([Id])
GO
ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [FK_Result_Pilot]
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [FK_Result_Race] FOREIGN KEY([RaceId])
REFERENCES [dbo].[Race] ([Id])
GO
ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [FK_Result_Race]
GO
