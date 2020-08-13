﻿USE [master]
GO
/****** Object:  Database [DomainSalesPortalDB]    Script Date: 8/13/2020 3:08:58 PM ******/
CREATE DATABASE [DomainSalesPortalDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DomainSalesPortalDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\DomainSalesPortalDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DomainSalesPortalDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\DomainSalesPortalDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DomainSalesPortalDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DomainSalesPortalDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DomainSalesPortalDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DomainSalesPortalDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DomainSalesPortalDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DomainSalesPortalDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DomainSalesPortalDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DomainSalesPortalDB] SET  MULTI_USER 
GO
ALTER DATABASE [DomainSalesPortalDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DomainSalesPortalDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DomainSalesPortalDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DomainSalesPortalDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DomainSalesPortalDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DomainSalesPortalDB] SET QUERY_STORE = OFF
GO
USE [DomainSalesPortalDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [DomainSalesPortalDB]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 8/13/2020 3:08:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Surname] [nvarchar](150) NOT NULL,
	[FullName] [nvarchar](300) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[RegisteredDate] [datetime] NOT NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Domains]    Script Date: 8/13/2020 3:08:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Domains](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsActive] [bit] NULL,
	[CustomerId] [int] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[NS1] [nvarchar](500) NOT NULL,
	[NS2] [nvarchar](500) NOT NULL,
	[LastChange] [datetime] NOT NULL,
	[ExpiredDate] [datetime] NOT NULL,
	[IsFavourite] [bit] NOT NULL,
 CONSTRAINT [PK_Domains] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Domains]  WITH CHECK ADD  CONSTRAINT [FK_Domains_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Domains] CHECK CONSTRAINT [FK_Domains_Customers]
GO
USE [master]
GO
ALTER DATABASE [DomainSalesPortalDB] SET  READ_WRITE 
GO