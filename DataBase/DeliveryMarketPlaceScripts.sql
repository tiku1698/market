USE [master]
GO
/****** Object:  Database [DeliveryMarketplace]    Script Date: 01-09-2022 17:13:40 ******/
CREATE DATABASE [DeliveryMarketplace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DeliveryMarketplace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DeliveryMarketplace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DeliveryMarketplace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DeliveryMarketplace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DeliveryMarketplace] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DeliveryMarketplace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DeliveryMarketplace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET ARITHABORT OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DeliveryMarketplace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DeliveryMarketplace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DeliveryMarketplace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DeliveryMarketplace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET RECOVERY FULL 
GO
ALTER DATABASE [DeliveryMarketplace] SET  MULTI_USER 
GO
ALTER DATABASE [DeliveryMarketplace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DeliveryMarketplace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DeliveryMarketplace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DeliveryMarketplace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DeliveryMarketplace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DeliveryMarketplace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DeliveryMarketplace', N'ON'
GO
ALTER DATABASE [DeliveryMarketplace] SET QUERY_STORE = OFF
GO
USE [DeliveryMarketplace]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 01-09-2022 17:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[PickupAddress] [nvarchar](50) NOT NULL,
	[DeliveryAddress] [nvarchar](50) NOT NULL,
	[PickupLoc] [nvarchar](max) NOT NULL,
	[DeliverLoc] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[DriverID] [int] NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 01-09-2022 17:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Feedback] [nvarchar](max) NULL,
 CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 01-09-2022 17:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01-09-2022 17:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PhoneNumber] [bigint] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([BookingID], [ProductID], [UserID], [PickupAddress], [DeliveryAddress], [PickupLoc], [DeliverLoc], [Status], [Price], [DriverID]) VALUES (1, 2, 2, N'Test', N'Test', N'{  "lat": -33.85081974222516,  "lng": 151.04590158462526}', N'{  "lat": -33.8517463978769,  "lng": 151.0488627433777}', N'Rejected', CAST(50 AS Decimal(18, 0)), 3)
INSERT [dbo].[Bookings] ([BookingID], [ProductID], [UserID], [PickupAddress], [DeliveryAddress], [PickupLoc], [DeliverLoc], [Status], [Price], [DriverID]) VALUES (2, 1, 1, N'Test1', N'test2', N'{  "lat": -33.85000000000001,  "lng": 151.0442235944036}', N'{  "lat": -33.8461890611479,  "lng": 151.07009696934256}', N'Pending', CAST(123 AS Decimal(18, 0)), 3)
INSERT [dbo].[Bookings] ([BookingID], [ProductID], [UserID], [PickupAddress], [DeliveryAddress], [PickupLoc], [DeliverLoc], [Status], [Price], [DriverID]) VALUES (7, 3, 1, N'Test Address', N'Test Address', N'{  "lat": -33.850249487596905,  "lng": 151.0431335449219}', N'{  "lat": -33.85488269626114,  "lng": 151.0535619735718}', N'Pending', CAST(300 AS Decimal(18, 0)), 3)
INSERT [dbo].[Bookings] ([BookingID], [ProductID], [UserID], [PickupAddress], [DeliveryAddress], [PickupLoc], [DeliverLoc], [Status], [Price], [DriverID]) VALUES (10, 4, 1, N'A', N'B', N'{  "lat": -33.85021384655625,  "lng": 151.0412452697754}', N'{  "lat": -33.85488269626114,  "lng": 151.05068664550782}', N'Accepted', CAST(250 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Bookings] ([BookingID], [ProductID], [UserID], [PickupAddress], [DeliveryAddress], [PickupLoc], [DeliverLoc], [Status], [Price], [DriverID]) VALUES (11, 3, 1, N'C', N'D', N'{  "lat": -33.85053023499988,  "lng": 151.05231285095215}', N'{  "lat": -33.84989307652114,  "lng": 151.04699592590333}', N'Completed', CAST(300 AS Decimal(18, 0)), 3)
INSERT [dbo].[Bookings] ([BookingID], [ProductID], [UserID], [PickupAddress], [DeliveryAddress], [PickupLoc], [DeliverLoc], [Status], [Price], [DriverID]) VALUES (12, 3, 2, N'New', N'New', N'{  "lat": -33.8525304832691,  "lng": 151.04223232269288}', N'{  "lat": -33.854740139741985,  "lng": 151.04948501586915}', N'Accepted', CAST(250 AS Decimal(18, 0)), 4)
INSERT [dbo].[Bookings] ([BookingID], [ProductID], [UserID], [PickupAddress], [DeliveryAddress], [PickupLoc], [DeliverLoc], [Status], [Price], [DriverID]) VALUES (13, 1, 2, N'New Pick', N'New Drop', N'{  "lat": -33.852744323490576,  "lng": 151.04433517456056}', N'{  "lat": -33.8555598364778,  "lng": 151.0452793121338}', N'Pending', CAST(53 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Bookings] ([BookingID], [ProductID], [UserID], [PickupAddress], [DeliveryAddress], [PickupLoc], [DeliverLoc], [Status], [Price], [DriverID]) VALUES (14, 4, 2, N'Testing Pickup', N'Testing Delivery', N'{  "lat": -33.85448628507679,  "lng": 151.03995323181152}', N'{  "lat": -33.850570256293715,  "lng": 151.04373435974122}', N'Pending', CAST(16 AS Decimal(18, 0)), NULL)
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
SET IDENTITY_INSERT [dbo].[Feedbacks] ON 

INSERT [dbo].[Feedbacks] ([FeedbackID], [UserID], [Feedback]) VALUES (3, 2, N'Feedback Test')
INSERT [dbo].[Feedbacks] ([FeedbackID], [UserID], [Feedback]) VALUES (4, 2, N'ABC')
INSERT [dbo].[Feedbacks] ([FeedbackID], [UserID], [Feedback]) VALUES (5, 2, N'ABCD')
SET IDENTITY_INSERT [dbo].[Feedbacks] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [Name]) VALUES (1, N'Food Products')
INSERT [dbo].[Products] ([ProductID], [Name]) VALUES (2, N'Electronics Products')
INSERT [dbo].[Products] ([ProductID], [Name]) VALUES (3, N'Apparel Products')
INSERT [dbo].[Products] ([ProductID], [Name]) VALUES (4, N'Gift Products')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Role], [Name], [Email], [PhoneNumber], [Password], [Gender], [Address], [City], [State]) VALUES (1, N'Admin', N'Admin', N'Admin@gmail.com', 9876543210, N'123123', N'Male', N'Test', N'Ynr', N'Haryana')
INSERT [dbo].[Users] ([UserID], [Role], [Name], [Email], [PhoneNumber], [Password], [Gender], [Address], [City], [State]) VALUES (2, N'User', N'TestUser', N'User@Gmail.com', 1234567890, N'12345', N'Male', N'Test', N'Ynr', N'Haryana')
INSERT [dbo].[Users] ([UserID], [Role], [Name], [Email], [PhoneNumber], [Password], [Gender], [Address], [City], [State]) VALUES (3, N'Driver', N'TestDriver', N'Driver@Gmail.com', 9988776655, N'123456', N'Male', N'Test', N'Ynr', N'Haryana')
INSERT [dbo].[Users] ([UserID], [Role], [Name], [Email], [PhoneNumber], [Password], [Gender], [Address], [City], [State]) VALUES (4, N'Driver', N'NewDriver', N'New@gmail.com', 9988776644, N'101010', N'Male', N'Test', N'Ynr', N'Haryana')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Bookings] ADD  DEFAULT (NULL) FOR [DriverID]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Products]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Users]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_Users]
GO
USE [master]
GO
ALTER DATABASE [DeliveryMarketplace] SET  READ_WRITE 
GO
