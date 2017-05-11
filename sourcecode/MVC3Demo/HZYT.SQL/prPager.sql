USE [HZYT_Soft]
GO

/****** Object:  StoredProcedure [dbo].[prPager]    Script Date: 01/04/2012 17:37:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[prPager]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[prPager]
GO

USE [HZYT_Soft]
GO

/****** Object:  StoredProcedure [dbo].[prPager]    Script Date: 01/04/2012 17:37:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[prPager]
    @varIdentityName VARCHAR(200), --主键名称
	@intPageSize INT, ----每页记录数
	@intCurrentCount INT, ----当前记录数量（页码*每页记录数）
	@varTableName VARCHAR(200), ----表名称
	@varWhere VARCHAR(800), ----查询条件
	@intTotalCount INT OUTPUT ----记录总数
AS
	DECLARE @sqlSelect    NVARCHAR(2000) ----局部变量（sql语句），查询记录集
	DECLARE @sqlGetCount  NVARCHAR(2000) ----局部变量（sql语句），取出记录集总数
	
	
	SET @sqlSelect = 'SELECT * FROM  ' 
	    + '    (SELECT ROW_NUMBER()  OVER( ORDER BY ' + @varIdentityName + 
	    ') AS RowNumber,* '
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


