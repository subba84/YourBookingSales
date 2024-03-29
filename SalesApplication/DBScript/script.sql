USE [master]
GO
/****** Object:  Database [BUSBOOKING]    Script Date: 18-03-2023 09:27:36 ******/
CREATE DATABASE [BUSBOOKING]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BUSBOOKING', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BUSBOOKING.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BUSBOOKING_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BUSBOOKING_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BUSBOOKING] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BUSBOOKING].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BUSBOOKING] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BUSBOOKING] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BUSBOOKING] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BUSBOOKING] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BUSBOOKING] SET ARITHABORT OFF 
GO
ALTER DATABASE [BUSBOOKING] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BUSBOOKING] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BUSBOOKING] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BUSBOOKING] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BUSBOOKING] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BUSBOOKING] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BUSBOOKING] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BUSBOOKING] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BUSBOOKING] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BUSBOOKING] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BUSBOOKING] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BUSBOOKING] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BUSBOOKING] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BUSBOOKING] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BUSBOOKING] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BUSBOOKING] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BUSBOOKING] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BUSBOOKING] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BUSBOOKING] SET  MULTI_USER 
GO
ALTER DATABASE [BUSBOOKING] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BUSBOOKING] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BUSBOOKING] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BUSBOOKING] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BUSBOOKING] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BUSBOOKING] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BUSBOOKING] SET QUERY_STORE = OFF
GO
USE [BUSBOOKING]
GO
/****** Object:  Table [dbo].[RenewalHistory]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RenewalHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesEntryId] [int] NULL,
	[TransactionId] [varchar](30) NULL,
	[PaidType] [varchar](30) NULL,
	[ServiceTypeId] [int] NULL,
	[ServiceType] [varchar](50) NULL,
	[ServiceAmount] [varchar](20) NULL,
	[OtherType] [varchar](30) NULL,
	[StatusId] [int] NULL,
	[StatusName] [varchar](50) NULL,
	[Comments] [varchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [varchar](30) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](30) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [varchar](30) NULL,
	[DeletedByName] [varchar](128) NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_RenewalHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesEntry]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesEntry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](256) NULL,
	[EmailId] [nvarchar](256) NULL,
	[MobileNumber] [varchar](20) NULL,
	[Address] [nvarchar](max) NULL,
	[Landmark] [varchar](256) NULL,
	[StateId] [int] NULL,
	[StateName] [varchar](256) NULL,
	[CityId] [int] NULL,
	[CityName] [varchar](256) NULL,
	[ServiceTypeId] [int] NULL,
	[ServiceType] [varchar](30) NULL,
	[ServiceAmount] [varchar](20) NULL,
	[TransactionId] [varchar](30) NULL,
	[PaidType] [varchar](30) NULL,
	[OtherType] [varchar](128) NULL,
	[StatusId] [int] NULL,
	[StatusName] [varchar](30) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](20) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [varchar](20) NULL,
	[DeletedByName] [varchar](128) NULL,
	[DeletedOn] [datetime] NULL,
	[ActivationKey] [varchar](50) NULL,
	[SeriesNumber] [nvarchar](100) NULL,
	[Password] [varchar](100) NULL,
	[ActivatedUpto] [datetime] NULL,
	[IsUnderRenewel] [bit] NOT NULL,
	[RenewelStatusId] [int] NULL,
	[RenewelStatusName] [varchar](30) NULL,
	[SMSPack] [varchar](10) NULL,
	[InvoiceType] [varchar](10) NULL,
	[CompanyGSTId] [nvarchar](20) NULL,
	[SMSAmount] [varchar](20) NULL,
	[SMSGST] [varchar](10) NULL,
	[TotalGSTAmount] [varchar](20) NULL,
	[TotalAmount] [varchar](20) NULL,
	[PanNumber] [varchar](20) NULL,
	[SalesGST] [varchar](20) NULL,
	[SMSCount] [int] NULL,
	[NoofLogins] [int] NULL,
 CONSTRAINT [PK_SalesEntry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[RenewalHistoryView]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[RenewalHistoryView]
 As
  
  select RH.Id,RH.SalesEntryId,Rh.TransactionId,RH.ServiceAmount,RH.PaidType,RH.CreatedByName,RH.CreatedBy,RH.CreatedOn,
  SE.Name,SE.MobileNumber,SE.StateName,SE.CityName,SE.ActivatedUpto
  
  from RenewalHistory RH 
  inner join SalesEntry SE on RH.SalesEntryId=SE.Id and RH.IsActive=1  and RH.StatusId=9
GO
/****** Object:  Table [dbo].[CityMaster]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [varchar](128) NULL,
	[StateId] [int] NULL,
	[StateName] [varchar](128) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](20) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [varchar](20) NULL,
	[DeletedByName] [varchar](128) NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_CityMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyLogin]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyLogin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_CompanyLogin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customerdetails]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customerdetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NULL,
	[CustomerName] [varchar](50) NULL,
	[Age] [int] NULL,
	[gender] [varchar](50) NULL,
	[Mobilenumber] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[modifieddate] [datetime] NOT NULL,
	[Activeinactive] [bit] NOT NULL,
 CONSTRAINT [PK_Customerdetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [varchar](150) NULL,
	[EmailId] [varchar](150) NULL,
	[Password] [varchar](30) NULL,
	[MobileNumber] [varchar](20) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreateByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](20) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
	[DeleteBy] [varchar](20) NULL,
	[DeleteByName] [varchar](128) NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeInRole]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeInRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[EmployeeName] [varchar](128) NULL,
	[EmailId] [varchar](150) NULL,
	[RoleId] [int] NULL,
	[RoleName] [varchar](128) NULL,
	[Priority] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](20) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [varchar](20) NULL,
	[DeletedByName] [varchar](128) NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_EmployeeInRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Error]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Error](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Error] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](10) NULL,
	[ErrorPath] [nvarchar](max) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_Error] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[SalesEntryId] [int] NULL,
	[CompanyGSTId] [varchar](50) NULL,
	[OwnGSTNumber] [varchar](50) NULL,
	[StateName] [varchar](256) NULL,
	[TotalAmount] [varchar](20) NULL,
	[TotalGSTAmount] [varchar](20) NULL,
	[PanNumber] [varchar](20) NULL,
	[InvoiceType] [varchar](50) NULL,
	[ActivationUpto] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](20) NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceLineItem]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceLineItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NULL,
	[ServiceType] [varchar](128) NULL,
	[SalesAmount] [varchar](20) NULL,
	[GSTAmount] [varchar](20) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](10) NULL,
	[ChangedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_InvoiceLineItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobwork]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobwork](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobType] [varchar](50) NULL,
	[JobDetails] [varchar](max) NULL,
	[IsRunning] [bit] NULL,
	[IsSuccess] [bit] NULL,
	[FailedMessage] [varchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [varchar](30) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](30) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
 CONSTRAINT [PK_Jobwork] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobWorkMonitor]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobWorkMonitor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobType] [varchar](50) NULL,
	[IsRunning] [bit] NOT NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [varchar](30) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](30) NULL,
	[ChangedByName] [varchar](128) NULL,
 CONSTRAINT [PK_JobWorkMonitor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentHistory]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesEntryId] [int] NULL,
	[ServiceTypeId] [int] NULL,
	[ServiceTypeName] [varchar](30) NULL,
	[ServiceAmount] [varchar](20) NULL,
	[TransactionId] [varchar](20) NULL,
	[PaidType] [varchar](30) NULL,
	[OtherType] [varchar](30) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](20) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [varchar](20) NULL,
	[DeletedByName] [varchar](128) NULL,
	[DeletedOn] [datetime] NULL,
	[ActivatedUpto] [datetime] NULL,
 CONSTRAINT [PK_PaymentHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NULL,
	[Priority] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](20) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesAudit]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesAudit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SaleEntrId] [int] NULL,
	[StatusId] [int] NULL,
	[StatusName] [varchar](30) NULL,
	[EmployeeNumber] [varchar](20) NULL,
	[EmployeeName] [varchar](128) NULL,
	[Comments] [varchar](max) NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_SalesAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[Type] [varchar](50) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SettingsMaster]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingsMaster](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_SettingsMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMSHistory]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMSHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmailId] [varchar](128) NULL,
	[Name] [varchar](256) NULL,
	[MobileNumber] [varchar](20) NULL,
	[Address] [varchar](max) NULL,
	[JourneyFrom] [varchar](256) NULL,
	[JourneyTo] [varchar](256) NULL,
	[TicketNumber] [varchar](50) NULL,
	[SeatNumbers] [varchar](512) NULL,
	[DepartureOn] [datetime] NULL,
	[ArrivalOn] [datetime] NULL,
	[IsSMSSent] [bit] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [varchar](30) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](30) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [varchar](30) NULL,
	[DeletedByName] [varchar](128) NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_SMSHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateMaster]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](128) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedByName] [varchar](128) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [varchar](20) NULL,
	[ChangedByName] [varchar](128) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [varchar](20) NULL,
	[DeletedByName] [varchar](128) NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_StateMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblbookticket]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblbookticket](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [bigint] NOT NULL,
	[Fromlocation] [varchar](100) NULL,
	[Tolocation] [varchar](100) NULL,
	[Dateofjourney] [date] NULL,
	[Mobilenumber] [varchar](50) NULL,
	[Emailid] [varchar](50) NULL,
	[Departuretime] [varchar](50) NULL,
	[Arrivaltime] [varchar](50) NULL,
	[Busnumber] [varchar](50) NULL,
	[Seatnumber] [varchar](50) NULL,
	[Bustype] [varchar](50) NULL,
	[Ticketfare] [decimal](18, 0) NULL,
	[GST] [decimal](18, 0) NULL,
	[Servicechqarge] [decimal](18, 0) NULL,
	[Totalfare] [decimal](18, 0) NULL,
	[Operatorname] [varchar](50) NULL,
	[Operatorcntno] [varchar](50) NULL,
	[Boardingpoint] [varchar](50) NULL,
	[CreatedBy] [varchar](100) NULL,
	[ModifiedBy] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[Refoundamount] [decimal](18, 0) NULL,
 CONSTRAINT [PK_tblbookticket] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHirebusbykm]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHirebusbykm](
	[HirebusbykmID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NULL,
	[Nameofhirer] [varchar](100) NULL,
	[Bookingdate] [date] NULL,
	[Address] [varchar](max) NULL,
	[Partyphonumbers] [varchar](max) NULL,
	[Particularsofjourney] [varchar](max) NULL,
	[Startingfrom] [varchar](100) NULL,
	[Goingto] [varchar](100) NULL,
	[Startingtime] [varchar](100) NULL,
	[Startingdate] [date] NULL,
	[Endingtime] [varchar](100) NULL,
	[Endingdate] [date] NULL,
	[Vehicle] [varchar](100) NULL,
	[Vehicletype] [varchar](100) NULL,
	[Noofvehicles] [bigint] NULL,
	[Vehiclenumbers] [varchar](max) NULL,
	[Seetingcapacity] [bigint] NULL,
	[Roadtax] [decimal](18, 0) NULL,
	[Noofkm] [decimal](18, 0) NULL,
	[Priceperkm] [decimal](18, 0) NULL,
	[Hireamount] [decimal](18, 0) NULL,
	[Advance] [decimal](18, 0) NULL,
	[Driverperday] [decimal](18, 0) NULL,
	[Balance] [decimal](18, 0) NULL,
	[Waitingcharges] [decimal](18, 0) NULL,
	[Per] [varchar](50) NULL,
	[Remarks] [varchar](max) NULL,
	[CreatedBy] [varchar](100) NULL,
	[ModifiedBy] [varchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
	[RefundAmount] [decimal](18, 0) NULL,
	[NoOfDrivers] [int] NULL,
	[GST] [decimal](18, 1) NULL,
 CONSTRAINT [PK_tblHirebusbykm] PRIMARY KEY CLUSTERED 
(
	[HirebusbykmID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblhirrerbus]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblhirrerbus](
	[HirerID] [bigint] IDENTITY(1,1) NOT NULL,
	[companyid] [bigint] NULL,
	[Nameofhirer] [varchar](100) NULL,
	[Bookingdate] [date] NULL,
	[Address] [varchar](max) NULL,
	[Partysphnno] [varchar](max) NULL,
	[Particularsofjourney] [varchar](max) NULL,
	[Startingfrom] [varchar](100) NULL,
	[Goingto] [varchar](100) NULL,
	[Startingtime] [varchar](50) NULL,
	[Startingdate] [datetime] NULL,
	[Endingtime] [varchar](50) NULL,
	[Vehicle] [varchar](50) NULL,
	[Vehicletype] [varchar](50) NULL,
	[Endingdate] [datetime] NULL,
	[Noofvehicles] [bigint] NULL,
	[Vehiclenumber] [varchar](50) NULL,
	[Seatingcapacity] [bigint] NULL,
	[Hireamount] [decimal](18, 0) NULL,
	[Roadtax] [decimal](18, 0) NULL,
	[Advance] [decimal](18, 0) NULL,
	[Driversbhattaperday] [decimal](18, 0) NULL,
	[Balance] [decimal](18, 0) NULL,
	[Waitingcharges] [decimal](18, 0) NULL,
	[Per] [varchar](50) NULL,
	[Remarks] [varchar](max) NULL,
	[CreatedBy] [varchar](100) NULL,
	[ModifiedBy] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [bit] NULL,
	[RefundAmount] [decimal](18, 0) NULL,
	[NoOfDrivers] [int] NULL,
	[GST] [decimal](18, 1) NULL,
 CONSTRAINT [PK_tblhirrerbus] PRIMARY KEY CLUSTERED 
(
	[HirerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPackagebookingdomestic]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPackagebookingdomestic](
	[PBDID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [bigint] NULL,
	[Custmername] [varchar](100) NULL,
	[Mobilenumber] [varchar](50) NULL,
	[IDProof] [varchar](100) NULL,
	[IdProofNumber] [nvarchar](100) NULL,
	[Address] [varchar](max) NULL,
	[Startingdate] [date] NULL,
	[Endingdate] [date] NULL,
	[Sightseeingby] [varchar](100) NULL,
	[Numberofadults] [bigint] NULL,
	[numberofchildren] [bigint] NULL,
	[HotelRequired] [varchar](50) NULL,
	[Hotel] [varchar](50) NULL,
	[HotelType] [varchar](50) NULL,
	[Roomtype] [varchar](50) NULL,
	[Noofrooms] [bigint] NULL,
	[Paclageparticulars] [varchar](max) NULL,
	[Startingtime] [varchar](50) NULL,
	[Endingtime] [varchar](50) NULL,
	[Amountforadults] [decimal](18, 0) NULL,
	[Amountforchildren] [decimal](18, 0) NULL,
	[Amount] [decimal](18, 0) NULL,
	[Gst] [decimal](18, 0) NULL,
	[Servicecharges] [decimal](18, 0) NULL,
	[Advance] [decimal](18, 0) NULL,
	[Balance] [decimal](18, 0) NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
	[Createddate] [datetime] NULL,
	[Modifieddate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[Refoundamount] [decimal](18, 0) NULL,
	[Remarks] [varchar](256) NULL,
 CONSTRAINT [PK_tblPackagebookingdomestic] PRIMARY KEY CLUSTERED 
(
	[PBDID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblpackagebookinginternational]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblpackagebookinginternational](
	[PBIID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NULL,
	[Custmername] [varchar](100) NULL,
	[Mobilenumber] [varchar](50) NULL,
	[Idproof] [varchar](50) NULL,
	[IdProofNumber] [nvarchar](100) NULL,
	[Address] [varchar](max) NULL,
	[Startingdate] [date] NULL,
	[Endingdate] [date] NULL,
	[Sightseeingby] [varchar](50) NULL,
	[Noofadults] [bigint] NULL,
	[Noofchildren] [bigint] NULL,
	[Hotelrequired] [varchar](50) NULL,
	[Hoteltype] [varchar](50) NULL,
	[Roomtype] [varchar](50) NULL,
	[Noofrooms] [bigint] NULL,
	[Packageparticulars] [varchar](max) NULL,
	[Startingtime] [varchar](50) NULL,
	[Endingtime] [varchar](50) NULL,
	[Amountforadults] [decimal](18, 0) NULL,
	[Amountforchildren] [decimal](18, 0) NULL,
	[Amount] [decimal](18, 0) NULL,
	[GST] [decimal](18, 0) NULL,
	[Servicecharge] [decimal](18, 0) NULL,
	[Advance] [decimal](18, 0) NULL,
	[Balance] [decimal](18, 0) NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[Refoundamount] [decimal](18, 0) NULL,
	[Remarks] [varchar](256) NULL,
 CONSTRAINT [PK_tblpackagebookinginternational] PRIMARY KEY CLUSTERED 
(
	[PBIID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[LoginId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NULL,
	[Company] [varchar](max) NULL,
	[Username] [varchar](max) NULL,
	[Mobilenumber] [varchar](max) NULL,
	[Password] [varchar](50) NULL,
	[LoginCount] [int] NULL,
	[Email] [nvarchar](128) NULL,
	[SMSCount] [int] NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[LoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customerdetails] ADD  CONSTRAINT [DF_Customerdetails_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Customerdetails] ADD  CONSTRAINT [DF_Customerdetails_modifieddate]  DEFAULT (getdate()) FOR [modifieddate]
GO
ALTER TABLE [dbo].[Customerdetails] ADD  CONSTRAINT [DF_Customerdetails_status]  DEFAULT ((1)) FOR [Activeinactive]
GO
ALTER TABLE [dbo].[SalesEntry] ADD  CONSTRAINT [DF_SalesEntry_IsUnderRenewel]  DEFAULT ((0)) FOR [IsUnderRenewel]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[SettingsMaster] ADD  CONSTRAINT [DF_SettingsMaster_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[SettingsMaster] ADD  CONSTRAINT [DF_SettingsMaster_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[SettingsMaster] ADD  CONSTRAINT [DF_SettingsMaster_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[tblbookticket] ADD  CONSTRAINT [DF_tblbookticket_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[tblHirebusbykm] ADD  CONSTRAINT [DF_tblHirebusbykm_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblHirebusbykm] ADD  CONSTRAINT [DF_tblHirebusbykm_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[tblHirebusbykm] ADD  CONSTRAINT [DF_tblHirebusbykm_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[tblPackagebookingdomestic] ADD  CONSTRAINT [DF_tblPackagebookingdomestic_Createddate]  DEFAULT (getdate()) FOR [Createddate]
GO
ALTER TABLE [dbo].[tblPackagebookingdomestic] ADD  CONSTRAINT [DF_tblPackagebookingdomestic_Modifieddate]  DEFAULT (getdate()) FOR [Modifieddate]
GO
ALTER TABLE [dbo].[tblPackagebookingdomestic] ADD  CONSTRAINT [DF_tblPackagebookingdomestic_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[tblpackagebookinginternational] ADD  CONSTRAINT [DF_tblpackagebookinginternational_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblpackagebookinginternational] ADD  CONSTRAINT [DF_tblpackagebookinginternational_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[tblpackagebookinginternational] ADD  CONSTRAINT [DF_tblpackagebookinginternational_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  StoredProcedure [dbo].[cancelPackagedetails]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[cancelPackagedetails](@HireId bigint,@flag varchar(100),@RefundAmount decimal(18,0))
as
begin
declare @hirebusref decimal(18,0),@kmref decimal(18,0);
if(@flag='Domestic')
begin
  --set @hirebusref=(select Amount from tblPackagebookingdomestic where PBDID=@HireId)
  --set @RefundAmount=(@hirebusref-@RefundAmount)
   update tblPackagebookingdomestic set Status=0,Refoundamount=@RefundAmount where PBDID=@HireId
   end
   else if(@flag='International')
    begin
	--set @hirebusref=(select Amount from tblpackagebookinginternational where PBIID=@HireId)
 -- set @RefundAmount=(@hirebusref-@RefundAmount)
   update tblpackagebookinginternational set Status=0,Refoundamount=@RefundAmount  where PBIID=@HireId
    end
	end
GO
/****** Object:  StoredProcedure [dbo].[getCompanyLogins]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[getCompanyLogins](@companyId bigint)
as
begin

select * from CompanyLogin where CompanyId=@companyId  

end
GO
/****** Object:  StoredProcedure [dbo].[GetHeader]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE Proc [dbo].[GetHeader](@companyId bigint)
  as
   begin
    select * from Settings where CompanyId=@companyId and Status=1
	end
GO
/****** Object:  StoredProcedure [dbo].[Gethirebyamount]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



   CREATE proc [dbo].[Gethirebyamount](@id bigint)
as
begin
   select *,(select Description from Settings where id=4) as Header from tblhirrerbus where HirerID=@id
end
GO
/****** Object:  StoredProcedure [dbo].[GethirebyamountForReport]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   CREATE proc [dbo].[GethirebyamountForReport](@Companyid bigint)
as
begin
   select * from tblhirrerbus where companyid=@Companyid
end
GO
/****** Object:  StoredProcedure [dbo].[Gethirebykm]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Gethirebykm](@id bigint)
as

begin
  select * from tblHirebusbykm where HirebusbykmID=@id

end
GO
/****** Object:  StoredProcedure [dbo].[GethirebykmForReport]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

   CREATE proc [dbo].[GethirebykmForReport](@Companyid bigint)
as
begin
   select * from tblHirebusbykm where companyid=@Companyid
end
GO
/****** Object:  StoredProcedure [dbo].[Gethirefullcancel]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	
 CREATE  proc [dbo].[Gethirefullcancel](@id bigint,@flag varchar(100))
as
begin

  if(@flag='Cancelhirebus')
  begin
   select HirerID as ID
	 ,companyid
	 ,Nameofhirer
	 ,Bookingdate
	 ,Address
	 ,Partysphnno
	 ,Particularsofjourney
	 ,Startingfrom
	 ,Goingto
	 ,Startingtime
	 ,Startingdate
	 ,Endingtime
	 ,Vehicle
	 ,Vehicletype
	 ,Endingdate
	 ,Noofvehicles
	 ,Vehiclenumber
	 ,Seatingcapacity
	 ,Hireamount
	 ,Roadtax  
	 ,Advance 
	 ,Driversbhattaperday
	 ,Balance
	 ,Waitingcharges  
	 ,Per 
	 ,Remarks  
	 ,CreatedBy 
	 ,ModifiedBy  
	 ,CreatedDate  
	 ,ModifiedDate  
	 ,Status
   
   
   ,(select Description from Settings  where id=4
   --and CompanyId=4
   ) as Header,(select Description from Settings  where id=1
   --and CompanyId=4
   ) as TC
    from tblhirrerbus where HirerID=@id and Status<>0
   end
   else if(@flag='Cancelhirebusbykm')
   begin
     select HirebusbykmID as ID
	 
	 ,companyid
	 ,Nameofhirer
	 ,Bookingdate
	 ,Address
	 ,Partyphonumbers  as Partysphnno
	 ,Particularsofjourney
	 ,Startingfrom
	 ,Goingto
	 ,Startingtime
	 ,Startingdate
	 ,Endingtime
	 ,Vehicle
	 ,Vehicletype
	 ,Endingdate
	 ,Noofvehicles
	 ,Vehiclenumbers as Vehiclenumber
	 ,Seetingcapacity as Seatingcapacity
	 ,Hireamount
	 ,Roadtax  
	 ,Advance 
	 ,Driverperday as Driversbhattaperday
	 ,Balance
	 ,Waitingcharges  
	 ,Per 
	 ,Remarks  
	 ,CreatedBy 
	 ,ModifiedBy  
	 ,CreatedDate  
	 ,ModifiedDate  
	 ,Status
	 	 ,(select Description from Settings  where id=4
   --and CompanyId=4
   ) as Header
   ,(select Description from Settings  where id=1
   --and CompanyId=4
   ) as TC
	  from tblHirebusbykm where HirebusbykmID=@id and Status<>0
   end

end


GO
/****** Object:  StoredProcedure [dbo].[Getids]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Getids](@flag varchar(100))
as
begin
if(@flag='hirebus')
begin
select top 1 HirerID as id,Status from tblhirrerbus order by HirerID desc
end
else if(@flag='busbykm')
begin
select top 1 HirebusbykmID  as id,Status from tblHirebusbykm order by HirebusbykmID desc
end
else if(@flag='domestic')
begin
select top 1 PBDID  as id,Status from tblPackagebookingdomestic order by PBDID desc
end
else if(@flag='international')
begin
select top 1 PBIID  as id,Status from tblpackagebookinginternational order by PBIID desc
end
end
GO
/****** Object:  StoredProcedure [dbo].[GetPackageDomesticForReport]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

   CREATE proc [dbo].[GetPackageDomesticForReport](@Companyid bigint)
as
begin
   select * from tblPackagebookingdomestic where companyid=@Companyid
end
GO
/****** Object:  StoredProcedure [dbo].[GetPackageInternationalForReport]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   CREATE proc [dbo].[GetPackageInternationalForReport](@Companyid bigint)
as
begin
   select * from tblpackagebookinginternational where companyid=@Companyid
end
GO
/****** Object:  StoredProcedure [dbo].[getPassword]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[getPassword](@mobilenumber varchar(50))
as
begin

select * from tblUser where Mobilenumber=@mobilenumber  

end
GO
/****** Object:  StoredProcedure [dbo].[GetTicketBooking]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE proc [dbo].[GetTicketBooking](@Id bigint, @companyId bigint)  
as  
begin  
select Id,CompanyID ,Fromlocation,Tolocation ,Dateofjourney ,Mobilenumber  
    ,Emailid ,Departuretime ,Arrivaltime ,Busnumber ,Seatnumber ,Bustype ,Ticketfare,GST   
  ,Servicechqarge,Totalfare ,Operatorname ,Operatorcntno ,Boardingpoint,CreatedBy,ModifiedBy,Refoundamount,  
  CreatedDate,ModifiedDate,Status,(select Description from Settings where id=3) as busttc,  
 (select Description from Settings where id=4   
 --and CompanyId=4  
 ) as Header from tblbookticket where Id=@id  and CompanyID=@companyId
   end  
  
GO
/****** Object:  StoredProcedure [dbo].[getticketcustlist]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


   CREATE proc [dbo].[getticketcustlist](@Id bigint)
as
begin
select Id,userId,
	CustomerName,
	Age,
	gender,
	Mobilenumber,Activeinactive from Customerdetails where userId=@id and Activeinactive<>0
   end
GO
/****** Object:  StoredProcedure [dbo].[GetTicketsForReport]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

    CREATE Proc [dbo].[GetTicketsForReport]
  ( @companyID bigint)
  as
  begin
   select *, (select count(*) from Customerdetails where tbt.Id=userId) as NoofSeats, (select top 1 (CustomerName) from Customerdetails where tbt.Id=userId group by CustomerName) as CustomerName  
   from tblbookticket tbt where tbt.CompanyID=@companyID
   end
GO
/****** Object:  StoredProcedure [dbo].[getusers]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[getusers](@mobilenumber varchar(50),@password varchar(50))
as
begin

--select * from tblUser where Mobilenumber=@mobilenumber and Password=@password
select * from SalesEntry where Mobilenumber=@mobilenumber and Password=@password and SYSDATETIME()< ActivatedUpto


end
GO
/****** Object:  StoredProcedure [dbo].[Packagebookingdetails]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[Packagebookingdetails](@id bigint ,@flag varchar(100))
as
begin 

if(@flag='Domestic')
begin
select
     PBDID  as ID,CompanyId,Custmername,Mobilenumber ,Idproof,IdProofNumber,Address,Startingdate,Endingdate,Sightseeingby , Numberofadults as Noofadults,numberofchildren as  Noofchildren 
	 ,Hotelrequired ,Hoteltype ,Roomtype,Noofrooms  ,Paclageparticulars as Packageparticulars,Startingtime ,Endingtime ,Amountforadults,Amountforchildren 
	 ,Amount ,GST  , Servicecharges  as Servicecharge ,Advance ,Balance ,CreatedBy ,ModifiedBy ,CreatedDate,ModifiedDate,Status
	 ,(select Description from Settings where id=4) as Header,(select Description from Settings where id=7) as TC,Refoundamount,Remarks
	 from tblPackagebookingdomestic where PBDID=@id

end
else if(@flag='International')
begin
	select
     PBIID as ID,CompanyId,Custmername,Mobilenumber ,Idproof,IdProofNumber ,Address,Startingdate,Endingdate,Sightseeingby ,Noofadults,Noofchildren,Hotelrequired,Hoteltype,Roomtype 
	 ,Noofrooms ,Packageparticulars,Startingtime ,Endingtime ,Amountforadults ,Amountforchildren ,Amount ,GST ,Servicecharge  ,Advance ,Balance 
	 ,CreatedBy ,ModifiedBy ,CreatedDate,ModifiedDate,Status
	 ,(select Description from Settings where id=4) as Header,(select Description from Settings where id=7) as TC,Refoundamount,Remarks
	 from tblPackagebookinginternational where PBIID=@id 
	end

end

GO
/****** Object:  StoredProcedure [dbo].[Updatehireamountandkm]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

   
CREATE proc [dbo].[Updatehireamountandkm](@HireId bigint,@flag varchar(100),@RefundAmount decimal(18,0))
as
begin
declare @hirebusref decimal(18,0),@kmref decimal(18,0);
if(@flag='Cancelhirebus')
begin
  --set @hirebusref=(select Balance from tblhirrerbus where HirerID=@HireId)
  --set @RefundAmount=(@hirebusref-@RefundAmount)
   update tblhirrerbus set Status=0,RefundAmount=@RefundAmount,ModifiedDate=GETDATE() where HirerID=@HireId
   end
   else if(@flag='Cancelhirebusbykm')
    begin
	--set @hirebusref=(select Balance from tblHirebusbykm where HirebusbykmID=@HireId)
  --set @RefundAmount=(@hirebusref-@RefundAmount)
   update tblHirebusbykm set Status=0,RefundAmount=@RefundAmount,ModifiedDate=GETDATE()  where HirebusbykmID=@HireId
    end
	end


GO
/****** Object:  StoredProcedure [dbo].[Updatepartialticketcancel]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Updatepartialticketcancel](@id bigint,@RefundAmount decimal(18,0),@Seatnumber varchar(100),@cancelseats varchar(100))
as
begin
declare @amount decimal(18,0)
set @amount=(select isnull(Refoundamount,0) from tblbookticket where id=@id)
set @amount=sum(@amount+@RefundAmount)
if(@Seatnumber is not null and @Seatnumber<>'')
begin

update tblbookticket set seatnumber=@Seatnumber,Refoundamount=@amount where id=@id
end
else
 begin
   update tblbookticket set  seatnumber=@Seatnumber,Status=0,Refoundamount=@amount where id=@id
   update Customerdetails set Activeinactive=0 where userId=@id
 end


end
GO
/****** Object:  StoredProcedure [dbo].[UpdatePassword]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create Proc [dbo].[UpdatePassword](
@MobileNumber varchar(50),
@OldPassword varchar(50),
@NewPassword varchar(50))
 As
  Begin
   Update SalesEntry set Password=@NewPassword where MobileNumber=@MobileNumber and Password=@OldPassword and IsActive=1
   end
GO
/****** Object:  StoredProcedure [dbo].[updateSMSCount]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[updateSMSCount]
 ( @mobilenumber varchar(50),
 @SmsCount int
 )
  as 
   begin
     update SalesEntry set SMSCount=@SmsCount where mobilenumber=@mobilenumber and isActive=1
	 end
GO
/****** Object:  StoredProcedure [dbo].[USP_Gethirebusorkmdata]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
  
CREATE proc [dbo].[USP_Gethirebusorkmdata](@id  bigint,@days int,@amountorkm varchar(50),@flag varchar(100),@CompanyId bigint )  
as  
begin  
  if(@amountorkm='Amount')  
  begin  
if(@id<>0 and @id is not null)  
begin  
 select HirerID ,Nameofhirer,Partysphnno,Startingfrom,Goingto,Status,'Amount' as flag from tblhirrerbus  where HirerID=@id    and companyid=@CompanyId
 end  
 else if(@flag='cancel')   
 begin  
  select HirerID,Nameofhirer,Partysphnno,Startingfrom,Goingto,Status,'Amount' as flag from tblhirrerbus  where  Startingdate>= DATEADD(day,@days, GETDATE())  and companyid=@CompanyId
 end  
  else   
 begin  
  select HirerID,Nameofhirer,Partysphnno,Startingfrom,Goingto,Status,'Amount' as flag from tblhirrerbus  where  Startingdate>= DATEADD(day,@days, GETDATE()) and Status=0   and companyid=@CompanyId
 end  
  end  
  else  
   begin  
   if(@amountorkm='Kilometer')  
  begin  
     if(@id<>0 and @id is not null)  
begin  
 select  HirebusbykmID as HirerID,Nameofhirer,Partyphonumbers as Partysphnno,Startingfrom,Goingto,Status,'Kilometer' as flag  from tblHirebusbykm  where HirebusbykmID=@id   and companyid=@CompanyId 
 end  
 else if(@days is not null)  
 begin  
  select  HirebusbykmID as HirerID,Nameofhirer,Partyphonumbers as Partysphnno,Startingfrom,Goingto,Status,'Kilometer' as flag  from tblHirebusbykm  where  Startingdate>= DATEADD(day,@days, GETDATE()) and companyid=@CompanyId 
 end  
  else if(@flag='cancel')  
 begin  
  select  HirebusbykmID as HirerID,Nameofhirer,Partyphonumbers as Partysphnno,Startingfrom,Goingto,Status,'Kilometer' as flag  from tblHirebusbykm  where  Startingdate>= DATEADD(day,@days, GETDATE()) and Status=0  and companyid=@CompanyId
 end  
  
   end  
end  
if(@amountorkm is null and @days<>0)  
begin  
 select HirerID,Nameofhirer,Partysphnno,Startingfrom,Goingto,Status,'Amount' as flag from tblhirrerbus  where  Startingdate>= DATEADD(day,@days, GETDATE())  and companyid=@CompanyId
union  
  select  HirebusbykmID as HirerID,Nameofhirer,Partyphonumbers as Partysphnno,Startingfrom,Goingto,Status,'Kilometer' as flag  from tblHirebusbykm  where    
  Startingdate >= DATEADD(day,@days, GETDATE())  and companyid=@CompanyId
end  
else if(@flag='cancel')  
begin  
 select HirerID,Nameofhirer,Partysphnno,Startingfrom,Goingto,Status,'Amount' as flag from tblhirrerbus  where  Startingdate>= DATEADD(day,@days, GETDATE()) and Status=0  and companyid=@CompanyId
union  
  select  HirebusbykmID as HirerID,Nameofhirer,Partyphonumbers as Partysphnno,Startingfrom,Goingto,Status,'Kilometer' as flag  from tblHirebusbykm  where    
  Startingdate >= DATEADD(day,@days, GETDATE()) and Status=0  and companyid=@CompanyId
end  
else  
begin  
 select HirerID,Nameofhirer,Partysphnno,Startingfrom,Goingto,Status,'Amount' as flag from tblhirrerbus   where companyid=@CompanyId
union  
  select  HirebusbykmID as HirerID,Nameofhirer,Partyphonumbers as Partysphnno,Startingfrom,Goingto,Status,'Kilometer' as flag  from tblHirebusbykm   where  companyid=@CompanyId
end  
  
end  
GO
/****** Object:  StoredProcedure [dbo].[USP_GetPackagedata]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[USP_GetPackagedata](@id  bigint,@days int,@SearchPackage varchar(50),@flag varchar(100),@companyId bigint)
as
begin
  if(@SearchPackage='Domestic')
  begin
     if(@id<>0 and @id is not null)
begin
 select PBDID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'Domestic' as flag from tblPackagebookingdomestic  where   PBDID=@id  and CompanyID=@companyId
 end
 else if(@flag='cancel')
 begin
  select PBDID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'Domestic' as flag from tblPackagebookingdomestic  where  Startingdate >= DATEADD(day,@days, GETDATE())  and CompanyID=@companyId
 end
  else 
 begin
  select PBDID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'Domestic' as flag from tblPackagebookingdomestic  where  Startingdate >= DATEADD(day,@days, GETDATE())  and CompanyID=@companyId
  and Status=0
 end
  end
  else
   begin
   if(@SearchPackage='International')
  begin
     if(@id<>0 and @id is not null)
begin
 select  PBIID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'International' as flag from tblpackagebookinginternational  where  PBIID=@id   and CompanyID=@companyId
 end
 else  if(@flag='cancel')
 begin
  select  PBIID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'International' as flag  from tblpackagebookinginternational  where  Startingdate >= DATEADD(day,@days, GETDATE()) and Status=0  and CompanyID=@companyId
 end
  else 
 begin
  select  PBIID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'International' as flag  from tblpackagebookinginternational  where  Startingdate >= DATEADD(day,@days, GETDATE())   and CompanyID=@companyId
 end
   end
end
if(@SearchPackage is null  and @flag<>'cancel')
begin
  select PBDID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'Domestic' as flag  from tblPackagebookingdomestic  where  Startingdate >= DATEADD(day,@days, GETDATE())  and CompanyID=@companyId
  union
  select  PBIID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'International' as flag  from tblpackagebookinginternational  where  Startingdate >= DATEADD(day,@days, GETDATE())  and CompanyID=@companyId
end
else if(@flag='cancel' and @SearchPackage is null)
begin
   select PBDID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'Domestic' as flag  from tblPackagebookingdomestic  where  Startingdate >= DATEADD(day,@days, GETDATE()) and Status=0  and CompanyID=@companyId
  union
  select  PBIID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'International' as flag from tblpackagebookinginternational  where  Startingdate >= DATEADD(day,@days, GETDATE()) and Status=0  and CompanyID=@companyId
end
else if(@SearchPackage is null  and @flag is null and @days<>0)
begin
 select PBDID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'Domestic' as flag  from tblPackagebookingdomestic  where  Startingdate >= DATEADD(day,@days, GETDATE())   and CompanyID=@companyId
  union
  select  PBIID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'International' as flag from tblpackagebookinginternational  where  Startingdate >= DATEADD(day,@days, GETDATE())  and CompanyID=@companyId
end
else if(@SearchPackage is null  and @flag is null and @days=0 and @id=0)
begin
  select PBDID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'Domestic' as flag  from tblPackagebookingdomestic     where CompanyID=@companyId
  union
  select  PBIID as ID,Custmername,Mobilenumber,Startingdate,Endingdate,Status,'International' as flag from tblpackagebookinginternational   where CompanyID=@companyId

end
end



GO
/****** Object:  StoredProcedure [dbo].[USP_Gettblbookticketdata]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[USP_Gettblbookticketdata](@id  bigint,@days int,@flag varchar(100),@companyid  bigint)
as
begin
if(@id<>0 and @id is not null)
begin
 select * from tblbookticket  where id=@id  order by id desc
 end
 else if(@flag='cancel' and @days<>0) 
 begin
  select * from tblbookticket  where  Dateofjourney>= DATEADD(day,@days, GETDATE()) and Status=0 and CompanyID=@companyid  order by id desc
 end
  else  if(@flag is null and @days=0 and @id=0)
 begin
  select * from tblbookticket where  CompanyID=@companyid  order by id desc
 end
 else
  begin
  select * from tblbookticket  where  Dateofjourney>= DATEADD(day,@days, GETDATE()) and  CompanyID=@companyid  order by id desc
  end
 

end
GO
/****** Object:  StoredProcedure [dbo].[USP_insertCompanyLogin]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[USP_insertCompanyLogin](

  @CompanyId bigint  )
   as
begin
BEGIN TRAN

BEGIN TRY
insert into  CompanyLogin(CompanyId,IsActive)values( @CompanyId,1)
	    COMMIT TRAN
END TRY
BEGIN CATCH
  ROLLBACK TRAN
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[USP_insertCustomerdetails]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[USP_insertCustomerdetails](

  @Id bigint,
	@userId bigint,
	@CustomerName varchar(50),
	@Age int,
	@gender varchar(50),
	@Mobilenumber varchar(50),
	@CreatedBy varchar(50),
	@ModifiedBy  varchar(50),
	@CreatedDate datetime,
	@modifieddate datetime,
	@Activeinactive bit)
   as
begin
BEGIN TRAN

BEGIN TRY
insert into  Customerdetails(userId,CustomerName,Age,gender,Mobilenumber,
			  CreatedBy,ModifiedBy,Activeinactive)values( 
	          @userId,@CustomerName,@Age,@gender,@Mobilenumber,@CreatedBy,@ModifiedBy ,@Activeinactive)
	    COMMIT TRAN
END TRY
BEGIN CATCH
  ROLLBACK TRAN
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[USP_insertsettings]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[USP_insertsettings](

  @CompanyId bigint ,
  @description nvarchar(max),
  @CreatedBy varchar(50),
  @ModifiedBy  varchar(50),
 @Type varchar(50)
  )
   as
begin
BEGIN TRAN

BEGIN TRY
insert into  Settings(CompanyId,Description,CreatedBy, ModifiedBy,CreatedDate,ModifiedDate,Status,Type)
          values( @CompanyId,@description,@CreatedBy,@ModifiedBy,GETDATE(),GETDATE(),1,@Type)
	    COMMIT TRAN
END TRY
BEGIN CATCH
  ROLLBACK TRAN
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[USP_insertupdatehirebusbooking]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[USP_insertupdatehirebusbooking](

    @HirerID   bigint,
	@companyid   bigint,
	@Nameofhirer   varchar(100),
	@Bookingdate   date,
	@Address   varchar(max),
	@Partysphnno   varchar(max),
	@Particularsofjourney   varchar(max),
	@Startingfrom   varchar(100),
	@Goingto   varchar(100),
	@Startingtime   varchar(50),
	@Startingdate   datetime,
	@Endingtime   varchar(50),
	@Endingdate   datetime,
	@Vehicle   varchar(50),
	@Vehicletype   varchar(50),
	@Noofvehicles   bigint,
	@Vehiclenumber   varchar(50),
	@Seatingcapacity   bigint,
	@Hireamount   decimal(18, 0),
	@Roadtax   decimal (18, 0),
	@Advance   decimal (18, 0),
	@Driversbhattaperday   decimal(18, 0),
	@Balance   decimal(18, 0),
	@Waitingcharges   decimal(18, 0),
	@Per   varchar (50),
	@Remarks   varchar(max),
	@CreatedBy   varchar(100),
	@ModifiedBy   varchar(100),
	@CreatedDate   datetime,
	@ModifiedDate   datetime,
	@Status   bit,
	@noofDrivers int,
	@GST decimal(18,1),
    @optID bigint output ,
	@msg varchar(max) output)   
   
   as
begin
BEGIN TRAN

BEGIN TRY
insert into  tblhirrerbus(companyid,Nameofhirer,Bookingdate,Address,Partysphnno 
	 ,Particularsofjourney,Startingfrom,Goingto,Startingtime,Startingdate,Endingtime,Endingdate  
	 ,Vehicle,Vehicletype,Noofvehicles,Vehiclenumber,Seatingcapacity,Hireamount,Roadtax  
	 ,Advance,Driversbhattaperday,Balance,Waitingcharges,Per,Remarks,CreatedBy,ModifiedBy  
	 ,CreatedDate,ModifiedDate,Status,NoOfDrivers,GST)values( 
	 @companyid,@Nameofhirer,@Bookingdate,@Address,@Partysphnno,@Particularsofjourney 
	 ,@Startingfrom,@Goingto,@Startingtime,@Startingdate,@Endingtime,@Endingdate,@Vehicle  
	 ,@Vehicletype,@Noofvehicles,@Vehiclenumber,@Seatingcapacity,@Hireamount  
	 ,@Roadtax,@Advance,@Driversbhattaperday,@Balance,@Waitingcharges  
	 ,@Per,@Remarks,@CreatedBy,@ModifiedBy,GETDATE(),GETDATE(),1,@noofDrivers,@GST) 
		 set @msg='Record inserted Succesfully !'
	
	set @optID=scope_identity();
	    COMMIT TRAN
END TRY
BEGIN CATCH
  ROLLBACK TRAN
END CATCH
end


GO
/****** Object:  StoredProcedure [dbo].[USP_insertupdateHirebusbykm]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
  
CREATE proc [dbo].[USP_insertupdateHirebusbykm](  
  
    @HirebusbykmID  bigint  
 ,@CompanyId bigint  
 ,@Nameofhirer varchar(100)   
 ,@Bookingdate  date  
 ,@Address varchar(max)  
 ,@Partyphonumbers varchar(max)  
 ,@Particularsofjourney varchar(max)  
 ,@Startingfrom varchar(100)  
 ,@Goingto varchar(100)  
 ,@Startingtime varchar(100)  
 ,@Startingdate date  
 ,@Endingtime varchar(100)  
 ,@Endingdate date  
 ,@Vehicle  varchar(100)  
 ,@Vehicletype  varchar(100)  
 ,@Noofvehicles  bigint  
 ,@Vehiclenumbers  varchar(100)  
 ,@Seetingcapacity  bigint  
 ,@Roadtax  decimal(18,0)  
 ,@Noofkm  decimal(18,0)  
 ,@Priceperkm   decimal(18,0)  
 ,@Hireamount  decimal(18,0)  
 ,@Advance  decimal(18,0)  
 ,@Driverperday  decimal(18,0)  
 ,@Balance  decimal(18,0)  
 ,@Waitingcharges  decimal(18,0)  
 ,@Per  varchar(100)  
 ,@Remarks  varchar(max)  
 ,@CreatedBy  varchar(100)  
 ,@ModifiedBy  varchar(100)  
 ,@Status bit,  
 @NoOfDrivers int,
 @GST decimal(18,1),
 @optID bigint output ,  
 @msg varchar(max) output)    
   as  
begin  
BEGIN TRAN  
  
BEGIN TRY  
insert into  tblHirebusbykm(CompanyId,Nameofhirer,Bookingdate,Address,Partyphonumbers, Particularsofjourney,  
              Startingfrom,Goingto,Startingtime,Startingdate,Endingtime,Endingdate,Vehicle,Vehicletype,Noofvehicles,vehiclenumbers,  
     Seetingcapacity,Roadtax,Noofkm,Priceperkm,Hireamount,Advance,Driverperday,Balance,Waitingcharges,Per, Remarks,  
     CreatedBy,ModifiedBy,Status,NoOfDrivers,GST)values(   
           @CompanyId,@Nameofhirer ,@Bookingdate,@Address,@Partyphonumbers,@Particularsofjourney ,@Startingfrom ,@Goingto ,@Startingtime ,@Startingdate,   
           @Endingtime ,@Endingdate,@Vehicle  ,@Vehicletype  ,@Noofvehicles  ,@Vehiclenumbers,@Seetingcapacity ,@Roadtax  ,@Noofkm  ,@Priceperkm  ,@Hireamount ,@Advance,  
           @Driverperday  ,@Balance  ,@Waitingcharges ,@Per ,@Remarks ,@CreatedBy ,@ModifiedBy ,1,@NoOfDrivers,@GST)  
   set @msg='Record inserted Succesfully !'  
  
 set @optID=scope_identity();  
     COMMIT TRAN  
END TRY  
BEGIN CATCH  
  ROLLBACK TRAN  
END CATCH  
end  
GO
/****** Object:  StoredProcedure [dbo].[USP_insertupdatehirrerbooking]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[USP_insertupdatehirrerbooking](

    @HirerID   bigint,
	@companyid   bigint,
	@Nameofhirer   varchar(100),
	@Bookingdate   date,
	@Address   varchar(max),
	@Partysphnno   varchar(max),
	@Particularsofjourney   varchar(max),
	@Startingfrom   varchar(100),
	@Goingto   varchar(100),
	@Startingtime   varchar(50),
	@Startingdate   datetime,
	@Endingtime   varchar(50),
	@Endingdate   datetime,
	@Vehicle   varchar(50),
	@Vehicletype   varchar(50),
	@Noofvehicles   bigint,
	@Vehiclenumber   varchar(50),
	@Seatingcapacity   bigint,
	@Hireamount   decimal(18, 0),
	@Roadtax   decimal (18, 0),
	@Advance   decimal (18, 0),
	@Driversbhattaperday   decimal(18, 0),
	@Balance   decimal(18, 0),
	@Waitingcharges   decimal(18, 0),
	@Per   varchar (50),
	@Remarks   varchar(max),
	@CreatedBy   varchar(100),
	@ModifiedBy   varchar(100),
	@CreatedDate   datetime,
	@ModifiedDate   datetime,
	@Status   bit,
    @optID bigint output ,
	@msg varchar(max) output)   
   
   as
begin
BEGIN TRAN

BEGIN TRY
insert into  tblhirrerbus(companyid,Nameofhirer,Bookingdate,Address,Partysphnno 
	 ,Particularsofjourney,Startingfrom,Goingto,Startingtime,Startingdate,Endingtime,Endingdate  
	 ,Vehicle,Vehicletype,Noofvehicles,Vehiclenumber,Seatingcapacity,Hireamount,Roadtax  
	 ,Advance,Driversbhattaperday,Balance,Waitingcharges,Per,Remarks,CreatedBy,ModifiedBy  
	 ,CreatedDate,ModifiedDate,Status)values( 
	 @companyid,@Nameofhirer,@Bookingdate,@Address,@Partysphnno,@Particularsofjourney 
	 ,@Startingfrom,@Goingto,@Startingtime,@Startingdate,@Endingtime,@Endingdate,@Vehicle  
	 ,@Vehicletype,@Noofvehicles,@Vehiclenumber,@Seatingcapacity,@Hireamount  
	 ,@Roadtax,@Advance,@Driversbhattaperday,@Balance,@Waitingcharges  
	 ,@Per,@Remarks,@CreatedBy,@ModifiedBy,GETDATE(),GETDATE(),1) 
		 set @msg='Record inserted Succesfully !'
	
	set @optID=scope_identity();
	    COMMIT TRAN
END TRY
BEGIN CATCH
  ROLLBACK TRAN
END CATCH
end


GO
/****** Object:  StoredProcedure [dbo].[USP_insertupdatesettings]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  
CREATE proc [dbo].[USP_insertupdatesettings](@areatype varchar(50),@description varchar(max),@companyId bigint)  
as  
begin  
  
 update Settings set Description=@description,ModifiedDate=GETDATE(),CompanyId=@companyId where Type=@areatype  
  
end  
GO
/****** Object:  StoredProcedure [dbo].[USP_insertupdateticketbooking]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[USP_insertupdateticketbooking]
(
    @ID  bigint,
	@CompanyID  bigint  
	,@Fromlocation  varchar (100)  
	,@Tolocation  varchar (100)  
	,@Dateofjourney  date   
	,@Mobilenumber  varchar (50)  
	,@Emailid  varchar (50)  
	,@Departuretime  varchar (50)  
	,@Arrivaltime  varchar (50)  
	,@Busnumber  varchar (50)  
	,@Seatnumber  varchar (50)  
	,@Bustype  varchar (50)  
	,@Ticketfare  decimal (18, 0)  
	,@GST  decimal (18, 0)  
	,@Servicechqarge  decimal (18, 0)  
	,@Totalfare  decimal (18, 0)  
	,@Operatorname  varchar (50)  
	,@Operatorcntno  varchar (50)  
	,@Boardingpoint  varchar (50)  
	,@CreatedBy  varchar (100)  
	,@ModifiedBy  varchar (100)  
	,@Status  bit,
	@optID bigint output ,
	@msg varchar(max) output)   
   
   as
begin
BEGIN TRAN

BEGIN TRY

	insert into  tblbookticket(CompanyID ,Fromlocation,Tolocation ,Dateofjourney ,Mobilenumber
	   ,Emailid ,Departuretime ,Arrivaltime ,Busnumber ,Seatnumber ,Bustype ,Ticketfare,GST 
  ,Servicechqarge,Totalfare ,Operatorname ,Operatorcntno ,Boardingpoint,CreatedBy,ModifiedBy,CreatedDate,ModifiedDate)values(@CompanyID ,@Fromlocation,@Tolocation ,@Dateofjourney ,@Mobilenumber
	   ,@Emailid ,@Departuretime ,@Arrivaltime ,@Busnumber ,@Seatnumber ,@Bustype ,@Ticketfare,@GST 
  ,@Servicechqarge,@Totalfare ,@Operatorname ,@Operatorcntno ,@Boardingpoint,@CreatedBy,@ModifiedBy,GETDATE(),GETDATE()) 
		 set @msg='Record inserted Succesfully !'
	
	set @optID=SCOPE_IDENTITY();
	    COMMIT TRAN
END TRY
BEGIN CATCH
  ROLLBACK TRAN
END CATCH
end

GO
/****** Object:  StoredProcedure [dbo].[USP_Packagebookingdomestic]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[USP_Packagebookingdomestic](                                                                  
 @PBDID bigint
	,@CompanyId bigint
	,@Custmername  varchar(100) 
	,@Mobilenumber  varchar(50)
	,@IDProof  varchar(100)
	,@Address  varchar(max) 
	,@Startingdate  date
	,@Endingdate  date
	,@Sightseeingby  varchar(100) 
	,@Numberofadults  bigint
	,@numberofchildren bigint
	,@HotelRequired  varchar(50)
	,@Hotel  varchar(50)
	,@HotelType  varchar(50)
	,@Roomtype  varchar(50)
	,@Noofrooms  bigint
	,@Paclageparticulars varchar(max) 
	,@Startingtime varchar(50)
	,@Endingtime varchar(50)
	,@Amountforadults   decimal(18, 0)
	,@Amountforchildren decimal(18, 0) 
	,@Amount decimal(18, 0)
	,@Gst decimal(18, 0)
	,@Servicecharges decimal(18, 0)
	,@Advance  decimal(18, 0) 
	,@Balance  decimal(18, 0)
	,@CreatedBy  varchar(50)
    ,@ModifiedBy varchar(50)
	,@Createddate datetime
	,@Modifieddate datetime
	,@Status bit,
	@IdProofNumber varchar(100),
	@Remarks varchar(256),
	@optID bigint output ,
	@msg varchar(max) output)

	as
	begin
BEGIN TRAN

BEGIN TRY
 insert into  tblPackagebookingdomestic(CompanyID,Custmername ,Mobilenumber,IDProof,IdProofNumber  ,Address,Startingdate ,Endingdate ,sightseeingby
,Numberofadults,numberofchildren
,HotelRequired ,Hotel  ,HotelType ,Roomtype  ,Noofrooms ,Paclageparticulars 
,Startingtime ,Endingtime,Amountforadults,Amountforchildren ,Amount ,Gst,Servicecharges,Advance   ,Balance  ,CreatedBy  
,ModifiedBy,Createddate,Modifieddate ,Status,Remarks)values(
@CompanyId,@Custmername,@mobilenumber,@IDProof,@IdProofNumber,@Address ,@Startingdate,@Endingdate ,@Sightseeingby ,@Numberofadults 
,@numberofchildren,@HotelRequired  ,@Hotel  ,@HotelType ,@Roomtype ,@Noofrooms  ,@Paclageparticulars ,@Startingtime
,@Endingtime,@Amountforadults  	,@Amountforchildren ,@Amount,@Gst,@Servicecharges ,@Advance  ,@Balance ,@CreatedBy 
 ,@ModifiedBy,GETDATE(),GETDATE() ,1,@Remarks
)
 set @msg='Record inserted Succesfully !'

	set @optID=scope_identity();
	    COMMIT TRAN
END TRY
BEGIN CATCH
  ROLLBACK TRAN
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[USP_Packagebookinginternational]    Script Date: 18-03-2023 09:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[USP_Packagebookinginternational](                                                                  
    @PBIID   bigint,
	@CompanyId   bigint,
	@Custmername   varchar(100),
	@Mobilenumber   varchar (50),
	@Idproof   varchar (50)  ,
	@Address   varchar (max)  ,
	@Startingdate   date   ,
	@Endingdate   date   ,
	@Sightseeingby   varchar (50)  ,
	@Noofadults   bigint   ,
	@Noofchildren   bigint   ,
	@Hotelrequired   varchar (50)  ,
	@Hoteltype   varchar (50)  ,
	@Roomtype   varchar (50)  ,
	@Noofrooms   bigint   ,
	@Packageparticulars   varchar (max)  ,
	@Startingtime   varchar (50)  ,
	@Endingtime   varchar (50)  ,
	@Amountforadults   bigint   ,
	@Amountforchildren   bigint   ,
	@Amount   bigint   ,
	@GST   bigint   ,
	@Servicecharge   bigint   ,
	@Advance   bigint   ,
	@Balance   bigint   ,
	@CreatedBy varchar(50),
	@ModifiedBy varchar(50),	
	@IdProofNumber varchar(100),
	@Remarks varchar(256),
	@optID bigint output ,
	@msg varchar(max) output)
	as
	begin
BEGIN TRAN

BEGIN TRY
 insert into  tblpackagebookinginternational(
	 CompanyId,
	 Custmername,
	 Mobilenumber,
	 Idproof,
	 IdProofNumber,
	 Address,
	 Startingdate,
	 Endingdate,
	 Sightseeingby,
	 Noofadults,
	 Noofchildren,
	 Hotelrequired,
	 Hoteltype,
	 Roomtype,
	 Noofrooms,
	 Packageparticulars,
	 Startingtime,
	 Endingtime,
	 Amountforadults,
	 Amountforchildren,
	 Amount,
	 GST,
	 Servicecharge,
	 Advance,
	 Balance,
	 CreatedBy,MOdifiedBy,
	 Status,
	 Remarks
	 )values(
     @CompanyId,
	 @Custmername,
	 @Mobilenumber,
	 @Idproof,
	 @IdProofNumber,
	 @Address,
	 @Startingdate,
	 @Endingdate,
	 @Sightseeingby,
	 @Noofadults,
	 @Noofchildren,
	 @Hotelrequired,
	 @Hoteltype,
	 @Roomtype,
	 @Noofrooms,
	 @Packageparticulars,
	 @Startingtime,
	 @Endingtime,
	 @Amountforadults,
	 @Amountforchildren,
	 @Amount,
	 @GST,
	 @Servicecharge,
	 @Advance,
	 @Balance,
	 @CreatedBy,@ModifiedBy,1,@Remarks
)
 set @msg='Record inserted Succesfully !'

	set @optID=scope_identity();
	    COMMIT TRAN
END TRY
BEGIN CATCH
  ROLLBACK TRAN
END CATCH
end
GO
USE [master]
GO
ALTER DATABASE [BUSBOOKING] SET  READ_WRITE 
GO
