USE [QLMTC]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/14/2023 8:27:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[Usename] [nvarchar](60) NOT NULL,
	[Password] [nvarchar](60) NOT NULL,
	[Role] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/14/2023 8:27:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](15) NOT NULL,
	[Description] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12/14/2023 8:27:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](40) NOT NULL,
	[ContactName] [nvarchar](30) NULL,
	[ContactTitle] [nvarchar](30) NULL,
	[Address] [nvarchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order Details]    Script Date: 12/14/2023 8:27:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order Details](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Quantity] [smallint] NOT NULL,
	[Discount] [real] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/14/2023 8:27:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[RequiredDate] [datetime] NULL,
	[ShippedDate] [datetime] NULL,
	[Freight] [money] NULL,
	[ShipName] [nvarchar](40) NULL,
	[ShipAddress] [nvarchar](60) NULL,
	[ShipCity] [nvarchar](15) NULL,
	[ShipRegion] [nvarchar](15) NULL,
	[ShipPostalCode] [nvarchar](10) NULL,
	[ShipCountry] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/14/2023 8:27:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](40) NOT NULL,
	[CategoryID] [int] NULL,
	[Desscription] [ntext] NULL,
	[QuantityPerUnit] [nvarchar](20) NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [money] NULL,
	[UnitsInStock] [smallint] NULL,
	[UnitsOnOrder] [smallint] NULL,
	[ReorderLevel] [smallint] NULL,
	[Discontinued] [bit] NULL,
	[Picture] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountID], [CustomerID], [Usename], [Password], [Role]) VALUES (24, 3, N'nvm@gmail.com', N'12', 1)
INSERT [dbo].[Account] ([AccountID], [CustomerID], [Usename], [Password], [Role]) VALUES (27, 6, N'dungnthe153489', N'123', 0)
INSERT [dbo].[Account] ([AccountID], [CustomerID], [Usename], [Password], [Role]) VALUES (29, 8, N'vuong@gmail.com', N'12', 0)
INSERT [dbo].[Account] ([AccountID], [CustomerID], [Usename], [Password], [Role]) VALUES (30, NULL, N'vuong123@gmail.com', N'1234', 0)
INSERT [dbo].[Account] ([AccountID], [CustomerID], [Usename], [Password], [Role]) VALUES (31, NULL, N'thaypn@gmail.com', N'12', 0)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (1, N'Milk tea', N'Trà sữa ngon, dịch vụ tốt.')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (2, N'Poplar seeds', N'Hạt dương dừa ngon, dịch vụ tốt.')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (3, N'Highland coffee', N'Cafe bán chạy, ngon, dịch vụ tốt.')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (4, N'Other', N'Các mặt khác ngon, dịch vụ tốt.')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address]) VALUES (3, N'nvm@gmail.com', N'dung', NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address]) VALUES (6, N'', NULL, NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address]) VALUES (8, N'vuong@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address]) VALUES (9, N'vuong123@gmail.com', NULL, NULL, NULL)
INSERT [dbo].[Customers] ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address]) VALUES (11, N'dang cap chat', N'hehe', N'hehe', N'vippro')
INSERT [dbo].[Customers] ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address]) VALUES (12, N'dang cap chat', N'hehe', N'hehe', N'vippro')
INSERT [dbo].[Customers] ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address]) VALUES (13, N'gg', N'gg', N'gg', N'ggg')
INSERT [dbo].[Customers] ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address]) VALUES (15, N'Tuyenpg@gmail.com', N'tuyentuyen', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
INSERT [dbo].[Order Details] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (43, 1, 25.0000, 1, 0)
INSERT [dbo].[Order Details] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (43, 7, 25.0000, 2, 0)
INSERT [dbo].[Order Details] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (44, 1, 25.0000, 1, 0)
INSERT [dbo].[Order Details] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (44, 2, 25.0000, 1, 0)
INSERT [dbo].[Order Details] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (44, 7, 25.0000, 2, 0)
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (1, NULL, NULL, NULL, CAST(N'2023-06-28T00:00:00.000' AS DateTime), NULL, NULL, N'tran phu chuong my ha noi', NULL, NULL, NULL, N'Vietnam')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (2, NULL, NULL, NULL, CAST(N'2023-06-30T00:00:00.000' AS DateTime), NULL, NULL, N'tran phu chuong my ha noi', NULL, NULL, NULL, N'Vietnam')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (3, NULL, NULL, NULL, CAST(N'2023-06-30T00:00:00.000' AS DateTime), NULL, NULL, N'tran phu chuong my ha noi', NULL, NULL, NULL, N'Vietnam')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (4, NULL, CAST(N'2023-06-26T15:35:00.000' AS DateTime), NULL, CAST(N'2023-06-30T00:00:00.000' AS DateTime), NULL, NULL, N'tran phu chuong my ha noi', NULL, NULL, NULL, N'Vietnam')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (5, NULL, CAST(N'2023-06-26T15:41:00.000' AS DateTime), NULL, CAST(N'2023-06-28T00:00:00.000' AS DateTime), NULL, NULL, N'tran phu chuong my ha noi', NULL, NULL, NULL, N'Vietnam')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (6, NULL, CAST(N'2023-06-26T15:44:00.000' AS DateTime), NULL, CAST(N'2023-06-30T00:00:00.000' AS DateTime), NULL, NULL, N'tran phu chuong my ha noi', NULL, NULL, NULL, N'Vietnam')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (7, NULL, CAST(N'2023-06-26T15:45:00.000' AS DateTime), NULL, CAST(N'2023-06-29T00:00:00.000' AS DateTime), NULL, NULL, N'tran phu chuong my ha noi', NULL, NULL, NULL, N'Vietnam')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (8, NULL, NULL, NULL, CAST(N'2023-06-19T15:47:00.000' AS DateTime), NULL, NULL, N'tran phu chuong my ha noi', NULL, NULL, NULL, N'Vietnam')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (12, 3, CAST(N'2023-06-07T15:53:00.000' AS DateTime), NULL, CAST(N'2023-06-27T00:00:00.000' AS DateTime), NULL, NULL, N'tran phu chuong my ha noi', NULL, NULL, NULL, N'Vietnam')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (43, 8, CAST(N'2023-07-07T00:35:00.000' AS DateTime), NULL, CAST(N'2023-07-24T00:00:00.000' AS DateTime), 10.0000, N'', N'ggg', N'', NULL, NULL, N'hhhh')
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) VALUES (44, 8, CAST(N'2023-07-06T00:35:00.000' AS DateTime), NULL, CAST(N'2023-07-19T00:00:00.000' AS DateTime), 10.0000, N'', N'hhhhhh', N'', NULL, NULL, N'gggg')
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [Desscription], [QuantityPerUnit], [Quantity], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [Picture]) VALUES (1, N'Trà sữa trân châu', 1, N'Ngon giá hợp lý đi đôi với chất lượngssss', NULL, 11, 25.0000, NULL, NULL, NULL, NULL, N'https://pozaatea.vn/wp-content/uploads/2021/08/3.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [Desscription], [QuantityPerUnit], [Quantity], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [Picture]) VALUES (2, N'Trà sữa trân châu loại một', 1, N'Ngon giá hợp lý đi đôi với chất lượng', NULL, 1, 25.0000, NULL, NULL, NULL, 1, N'https://pozaatea.vn/wp-content/uploads/2021/08/3.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [Desscription], [QuantityPerUnit], [Quantity], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [Picture]) VALUES (3, N'Trà sữa trân châu loại vip', 1, N'Ngon giá hợp lý đi đôi với chất lượng', NULL, 2, 25.0000, NULL, NULL, NULL, 1, N'https://pozaatea.vn/wp-content/uploads/2021/08/3.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [Desscription], [QuantityPerUnit], [Quantity], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [Picture]) VALUES (7, N'Hạt dương dừa loại vip', 2, N'Ngon giá hợp lý đi đôi với chất lượng', NULL, 2, 25.0000, NULL, NULL, NULL, NULL, N'https://bizweb.dktcdn.net/thumb/1024x1024/100/345/470/products/d1b5581f-ba86-4aa9-bcc1-1bcf5196a10e.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [Desscription], [QuantityPerUnit], [Quantity], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [Picture]) VALUES (8, N'Hạt dương dừa loại 1', 3, N'Ngon giá hợp lý đi đôi với chất lượng alooo', NULL, 10, 45.0000, NULL, NULL, NULL, NULL, N'https://bizweb.dktcdn.net/thumb/1024x1024/100/345/470/products/d1b5581f-ba86-4aa9-bcc1-1bcf5196a10e.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [Desscription], [QuantityPerUnit], [Quantity], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [Picture]) VALUES (12, N'tra sua new hot', 4, N'oke lam nhe mn', NULL, 5, 25.0000, NULL, NULL, NULL, NULL, N'https://e7.pngegg.com/pngimages/149/993/png-clipart-milk-tea-illustration-bubble-tea-coffee-milk-white-tea-pearl-milk-tea-white-food-thumbnail.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [Desscription], [QuantityPerUnit], [Quantity], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [Picture]) VALUES (18, N'aaaa', 2, N'Ngon lắm mọi người thử đi', NULL, 12, 20000.0000, NULL, NULL, NULL, NULL, N'https://pozaatea.vn/wp-content/uploads/2021/08/3.png')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Order Details]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[Order Details]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
