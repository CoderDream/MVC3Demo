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
    @varIdentityName VARCHAR(200), --��������
	@intPageSize INT, ----ÿҳ��¼��
	@intCurrentCount INT, ----��ǰ��¼������ҳ��*ÿҳ��¼����
	@varTableName VARCHAR(200), ----������
	@varWhere VARCHAR(800), ----��ѯ����
	@intTotalCount INT OUTPUT ----��¼����
AS
	DECLARE @sqlSelect    NVARCHAR(2000) ----�ֲ�������sql��䣩����ѯ��¼��
	DECLARE @sqlGetCount  NVARCHAR(2000) ----�ֲ�������sql��䣩��ȡ����¼������
	
	
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


