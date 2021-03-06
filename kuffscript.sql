USE [KuffVer1]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[TelephoneNumber] [nvarchar](max) NULL,
	[CityState] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[Company] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DataTypes]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ControlToRender] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.DataTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductPriceId] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[OrderItemPictureId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[PriceWithoutDiscount] [decimal](18, 2) NOT NULL,
	[PriceWithDiscount] [decimal](18, 2) NOT NULL,
	[ProductSummary] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderItemSpecifications]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItemSpecifications](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderItemId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.OrderItemSpecifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[PaymentId] [uniqueidentifier] NOT NULL,
	[ShipmentCostId] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[Date] [nvarchar](max) NULL,
	[IsPaid] [bit] NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[UserCommentsAboutOrder] [nvarchar](max) NULL,
	[FinalPriceWithShipmentCost] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payments]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [uniqueidentifier] NOT NULL,
	[PaymentMethodId] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[IsPayingDone] [bit] NOT NULL,
	[DateOfPayment] [nvarchar](max) NULL,
	[PayingAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductAvailabilities]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAvailabilities](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Date] [nvarchar](max) NULL,
	[IsAvailable] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ProductAvailabilities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductPictures]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPictures](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[FilePath] [nvarchar](max) NULL,
	[ContentType] [nvarchar](max) NULL,
	[FileExtension] [nvarchar](max) NULL,
	[PictureOrder] [int] NOT NULL,
	[IsForSummaryPreview] [bit] NOT NULL,
	[IsMainPicture] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ProductPictures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductPrices]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPrices](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Date] [nvarchar](max) NULL,
	[PriceWithoutDiscount] [decimal](18, 2) NOT NULL,
	[PriceWithDiscount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.ProductPrices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductPropertyValues]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPropertyValues](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[ProductTypePropertyId] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ProductPropertyValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductTypeId] [uniqueidentifier] NOT NULL,
	[DateOfAdding] [nvarchar](max) NULL,
	[Summary] [nvarchar](max) NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductTypeProperties]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTypeProperties](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductTypeId] [uniqueidentifier] NOT NULL,
	[DataTypeId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[IsUserDecision] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ProductTypeProperties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductTypes]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NOT NULL,
	[DateOfCreation] [nvarchar](max) NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ProductTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShipmentCosts]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentCosts](
	[Id] [uniqueidentifier] NOT NULL,
	[ShipmentMethodId] [uniqueidentifier] NOT NULL,
	[DepartureCity] [nvarchar](max) NULL,
	[DestinationCity] [nvarchar](max) NULL,
	[DateOfAddedShipmentCost] [nvarchar](max) NULL,
	[Cost] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.ShipmentCosts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShipmentMethods]    Script Date: 7/2/2018 9:41:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentMethods](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ShipmentMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderItems_dbo.Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_dbo.OrderItems_dbo.Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderItems_dbo.ProductPrices_ProductPriceId] FOREIGN KEY([ProductPriceId])
REFERENCES [dbo].[ProductPrices] ([Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_dbo.OrderItems_dbo.ProductPrices_ProductPriceId]
GO
ALTER TABLE [dbo].[OrderItemSpecifications]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderItemSpecifications_dbo.OrderItems_OrderItemId] FOREIGN KEY([OrderItemId])
REFERENCES [dbo].[OrderItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItemSpecifications] CHECK CONSTRAINT [FK_dbo.OrderItemSpecifications_dbo.OrderItems_OrderItemId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.ShipmentCosts_ShipmentCostId] FOREIGN KEY([ShipmentCostId])
REFERENCES [dbo].[ShipmentCosts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.ShipmentCosts_ShipmentCostId]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Payments_dbo.Orders_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_dbo.Payments_dbo.Orders_Id]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Payments_dbo.PaymentMethods_PaymentMethodId] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethods] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_dbo.Payments_dbo.PaymentMethods_PaymentMethodId]
GO
ALTER TABLE [dbo].[ProductAvailabilities]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductAvailabilities_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductAvailabilities] CHECK CONSTRAINT [FK_dbo.ProductAvailabilities_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[ProductPictures]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductPictures_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductPictures] CHECK CONSTRAINT [FK_dbo.ProductPictures_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[ProductPrices]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductPrices_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductPrices] CHECK CONSTRAINT [FK_dbo.ProductPrices_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[ProductPropertyValues]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductPropertyValues_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductPropertyValues] CHECK CONSTRAINT [FK_dbo.ProductPropertyValues_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[ProductPropertyValues]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductPropertyValues_dbo.ProductTypeProperties_ProductTypePropertyId] FOREIGN KEY([ProductTypePropertyId])
REFERENCES [dbo].[ProductTypeProperties] ([Id])
GO
ALTER TABLE [dbo].[ProductPropertyValues] CHECK CONSTRAINT [FK_dbo.ProductPropertyValues_dbo.ProductTypeProperties_ProductTypePropertyId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.ProductTypes_ProductTypeId] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductTypes] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.ProductTypes_ProductTypeId]
GO
ALTER TABLE [dbo].[ProductTypeProperties]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductTypeProperties_dbo.DataTypes_DataTypeId] FOREIGN KEY([DataTypeId])
REFERENCES [dbo].[DataTypes] ([Id])
GO
ALTER TABLE [dbo].[ProductTypeProperties] CHECK CONSTRAINT [FK_dbo.ProductTypeProperties_dbo.DataTypes_DataTypeId]
GO
ALTER TABLE [dbo].[ProductTypeProperties]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductTypeProperties_dbo.ProductTypes_ProductTypeId] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductTypes] ([Id])
GO
ALTER TABLE [dbo].[ProductTypeProperties] CHECK CONSTRAINT [FK_dbo.ProductTypeProperties_dbo.ProductTypes_ProductTypeId]
GO
ALTER TABLE [dbo].[ProductTypes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductTypes_dbo.Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[ProductTypes] CHECK CONSTRAINT [FK_dbo.ProductTypes_dbo.Categories_CategoryId]
GO
ALTER TABLE [dbo].[ShipmentCosts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ShipmentCosts_dbo.ShipmentMethods_ShipmentMethodId] FOREIGN KEY([ShipmentMethodId])
REFERENCES [dbo].[ShipmentMethods] ([Id])
GO
ALTER TABLE [dbo].[ShipmentCosts] CHECK CONSTRAINT [FK_dbo.ShipmentCosts_dbo.ShipmentMethods_ShipmentMethodId]
GO
