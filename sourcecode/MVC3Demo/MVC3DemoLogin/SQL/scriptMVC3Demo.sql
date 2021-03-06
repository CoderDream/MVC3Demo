/****** Object:  Table [dbo].[User]    Script Date: 05/11/2017 11:33:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](40) NOT NULL,
	[Password] [nvarchar](40) NOT NULL,
	[Phone] [nvarchar](40) NOT NULL,
	[Residential] [int] NOT NULL,
	[FloorNo] [int] NOT NULL,
	[UnitNo] [int] NOT NULL,
	[DoorplateNo] [int] NOT NULL,
	[SubmitTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([UserID], [UserName], [Password], [Phone], [Residential], [FloorNo], [UnitNo], [DoorplateNo], [SubmitTime]) VALUES (2, N'abc', N'123456', N'12345678913', 1, 2, 3, 4, CAST(0x0000A77000BB624E AS DateTime))
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  StoredProcedure [dbo].[prPager]    Script Date: 05/11/2017 11:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[prPager]
	@varIdentityName NVARCHAR(200), --主键名称
	@intPageSize INT, ----每页记录数
	@intCurrentCount INT, ----当前记录数量（页码*每页记录数）
	@varTableName NVARCHAR(200), ----表名称
	@varWhere NVARCHAR(800), ----查询条件
	@intTotalCount INT OUTPUT ----记录总数
AS
	DECLARE @sqlSelect    NVARCHAR(2000) ----局部变量（sql语句），查询记录集
	DECLARE @sqlGetCount  NVARCHAR(2000) ----局部变量（sql语句），取出记录集总数
	
	
	SET @sqlSelect = 'SELECT * FROM  ' 
	    + '    (SELECT ROW_NUMBER()  OVER( ORDER BY ' + @varIdentityName +
	    ' DESC) AS RowNumber,* '
	    + '        FROM ' + @varTableName 
	    + '        WHERE ' + @varWhere 
	    + '     ) as  T2 ' 
	    + ' WHERE T2.RowNumber<= ' + STR(@intCurrentCount + @intPageSize) +
	    ' AND T2.RowNumber>' + STR(@intCurrentCount) 
	
	SET @sqlGetCount = 'SELECT @intTotalCount = COUNT(*) FROM ' + @varTableName 
	    + ' WHERE ' + @varWhere
	
	
	EXEC (@sqlSelect) 
	EXEC SP_EXECUTESQL @sqlGetCount,
	     N'@intTotalCount INT OUTPUT',
	     @intTotalCount OUTPUT
GO
/****** Object:  Table [dbo].[Address]    Script Date: 05/11/2017 11:33:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Address] ON
INSERT [dbo].[Address] ([AddressID], [Name], [Type]) VALUES (1, N'水域天际', 1)
INSERT [dbo].[Address] ([AddressID], [Name], [Type]) VALUES (2, N'5栋', 2)
INSERT [dbo].[Address] ([AddressID], [Name], [Type]) VALUES (3, N'2单元', 3)
INSERT [dbo].[Address] ([AddressID], [Name], [Type]) VALUES (4, N'603室', 4)
SET IDENTITY_INSERT [dbo].[Address] OFF
/****** Object:  Default [DF__User__SubmitTime__014935CB]    Script Date: 05/11/2017 11:33:02 ******/
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [SubmitTime]
GO
