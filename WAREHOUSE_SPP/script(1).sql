USE [master]
GO
/****** Object:  Database [DVSTestFrame]    Script Date: 12/26/2017 2:23:04 PM ******/
CREATE DATABASE [DVSTestFrame] ON  PRIMARY 
( NAME = N'DVSTestFrame', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DVSTestFrame.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DVSTestFrame_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DVSTestFrame_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DVSTestFrame] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DVSTestFrame].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DVSTestFrame] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DVSTestFrame] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DVSTestFrame] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DVSTestFrame] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DVSTestFrame] SET ARITHABORT OFF 
GO
ALTER DATABASE [DVSTestFrame] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DVSTestFrame] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DVSTestFrame] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DVSTestFrame] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DVSTestFrame] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DVSTestFrame] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DVSTestFrame] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DVSTestFrame] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DVSTestFrame] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DVSTestFrame] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DVSTestFrame] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DVSTestFrame] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DVSTestFrame] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DVSTestFrame] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DVSTestFrame] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DVSTestFrame] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DVSTestFrame] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DVSTestFrame] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DVSTestFrame] SET RECOVERY FULL 
GO
ALTER DATABASE [DVSTestFrame] SET  MULTI_USER 
GO
ALTER DATABASE [DVSTestFrame] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DVSTestFrame] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DVSTestFrame', N'ON'
GO
USE [DVSTestFrame]
GO
/****** Object:  StoredProcedure [dbo].[APP_GET_LOGIN]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[APP_GET_LOGIN] @USERNAME NVARCHAR(100)
, @PASSWORD NVARCHAR(MAX) AS BEGIN

    SELECT
      * 
    FROM
      U_User U 
    WHERE
      U.UserName = @USERNAME 
      AND U.[Password] = @PASSWORD 
      AND IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[APP_GET_MENU_SYSTEM]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[APP_GET_MENU_SYSTEM] (@USER_ID INT) AS BEGIN DECLARE @GROUPSYS INT 
SELECT
  @GROUPSYS = UT.UserTypeGroup 
FROM
  U_UserType UT 
  INNER JOIN U_User U 
    ON U.UserGroupID = UT.ID 
    AND U.ID = @USER_ID IF (@GROUPSYS = 99) BEGIN 
SELECT
  * 
FROM
  U_Menu M 
WHERE
  (M.IsDelete IS NULL OR M.IsDelete = 0) END ELSE BEGIN 
SELECT DISTINCT
  M.* 
FROM
  U_Menu M 
  INNER JOIN U_User_Role UR 
    ON M.ID = UR.MenuID 
  INNER JOIN U_UserType UT 
    ON UT.IsDelete = 0 
    AND UT.ID = UR.UserTypeID 
  INNER JOIN U_User U 
    ON U.UserGroupID = UT.ID 
    AND U.ID = @USER_ID 
WHERE
  (M.IsDelete IS NULL OR M.IsDelete = 0) END END
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterials_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ExportWarehousingMaterials_GetAll]
@FromDate datetime,
@ToDate datetime
as
begin
SELECT w.[Id]
      ,w.[WarehouseId]
      ,wh.WarehouseCode
      ,wh.WarehouseName
      ,w.WarehouseIdTo
      ,wht.WarehouseName as WarehouseNameTo
      ,CONVERT(VarChar(50), w.ImplementationDates, 103) as ImplementationDates
      ,w.[IsImport]      
      ,w.[WarehousingMaterialsCode]
      ,w.[IsConfirm]
      ,w.SchoolId
      ,s.SchoolsName
      ,CASE WHEN w.[IsConfirm]=1 THEN N'Đã xuất kho' else N'Chưa xuất kho' end ConfirmName
  FROM [WarehousingMaterials]w
  inner join dbo.Warehouse wh
  on (wh.Id=w.WarehouseId and ISNULL(wh.IsDelete,0)=0)
   inner join dbo.Warehouse wht
  on (wht.Id=w.WarehouseIdTo and ISNULL(wht.IsDelete,0)=0)
  inner join dbo.Schools s
  ON (s.Id=w.SchoolId AND ISNULL(s.IsDelete,0)=0)
  where ISNULL(w.isdelete,0)=0 AND isnull(w.IsImport,0)=0
  AND (w.ImplementationDates >=@FromDate)
  AND (w.ImplementationDates <=@ToDate);     
end
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterials_GetByFoodMenuCode]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ExportWarehousingMaterials_GetByFoodMenuCode]
@FoodMenuCode varchar(50)
as
begin
select [Id]
		  ,[FoodMenuCode]
		  ,[FoodMenuId]
		  ,[NumberFood]
		  ,[MaterialId]
		  ,[MaterialCode]
		  ,[MaterialName]
		  ,[UnitName]
		  ,(SUM(QuantityOne)+SUM(QuantityTwo)+SUM(QuantityOrther)) as Quantity
from
(
	select 
		   fm.[Id]
		  ,fm.[FoodMenuCode]
		  ,fd.[FoodMenuId]
		  ,fm.[NumberFood]
		  ,qf.[MaterialId]
		  ,m.[MaterialCode]
		  ,m.[MaterialName]
		  ,u.[UnitName]
		  ,IsNull(qf.[QuantitativeOne],0)*isnull(fm.[NumberFood],0) as QuantityOne
		  ,IsNull(qf.[QuantitativeTwo],0)*isnull(fm.[NumberFood],0) as QuantityTwo
		  ,IsNull(qf.[QuantitativeOrther],0)*isnull(fm.[NumberFood],0) as QuantityOrther
	from dbo.FoodMenu fm
	inner join dbo.FoodMenu_Detail fd
	on (fm.Id=fd.FoodMenuId AND Isnull(fd.IsDelete,0)=0)
	Inner join dbo.QuantitativeFood qf
	on (qf.FoodId=fd.FoodId and ISNULL(qf.isDelete,0)=0)
	inner join dbo.Material m
	ON  (qf.MaterialId=m.Id and ISNULL(m.isDelete,0)=0)
	inner join dbo.Unit u
	ON  (u.Id=m.UnitId and ISNULL(u.isDelete,0)=0)
	where fm.FoodMenuCode=@FoodMenuCode
)t
group by [Id]
		  ,[FoodMenuCode]
		  ,[FoodMenuId]
		  ,[NumberFood]
		  ,[MaterialId]
		  ,[MaterialCode]
		  ,[MaterialName]
		  ,[UnitName]
end
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterials_GetBySchoolAnDate]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ExportWarehousingMaterials_GetBySchoolAnDate]
 @SchoolId decimal(18,0)
,@DateMenu datetime
as
begin
select [Id]
		  ,[FoodMenuCode]
		  ,[FoodMenuId]
		  ,[NumberFood]
		  ,[MaterialId]
		  ,[MaterialCode]
		  ,[MaterialName]
		  ,[UnitName]
		  ,[NumberFoodTeacher]
		  ,[NumberFoodStudent]
		  ,[FoodCode]
		  ,[FoodName]
		  ,[FoodId]
		  ,(NumberFood*(SUM(QuantityOne)+SUM(QuantityTwo)+SUM(QuantityOrther))) as Quantity	
		  ,0 as MaterialsFifoId	  
		  ,0 as Price
from
(
	select 
		   fm.[Id]
		  ,fm.[FoodMenuCode]
		  ,fd.[FoodMenuId]
		  ,qf.[MaterialId]
		  ,m.[MaterialCode]
		  ,m.[MaterialName]
		  ,u.[UnitName]
		  ,f.[FoodCode]
		  ,f.[FoodName]
		  ,fd.[FoodId]
		  ,Isnull(fd.[NumberFoodTeacher],0) as NumberFoodTeacher
		  ,Isnull(fd.[NumberFoodStudent],0) as NumberFoodStudent
		  ,Isnull(fd.[NumberFoodTeacher],0)+ Isnull(fd.[NumberFoodStudent],0) as NumberFood		 
		  ,IsNull(qf.[QuantitativeOne],0) as QuantityOne
		  ,IsNull(qf.[QuantitativeTwo],0) as QuantityTwo
		  ,IsNull(qf.[QuantitativeOrther],0) as QuantityOrther
	from dbo.FoodMenu fm
	inner join dbo.FoodMenu_Detail fd
	on (fm.Id=fd.FoodMenuId AND Isnull(fd.IsDelete,0)=0)
	Inner join dbo.QuantitativeFood qf
	on (qf.FoodId=fd.FoodId and ISNULL(qf.isDelete,0)=0)
	inner join dbo.Material m
	ON  (qf.MaterialId=m.Id and ISNULL(m.isDelete,0)=0)
	inner join dbo.Unit u
	ON  (u.Id=m.UnitId and ISNULL(u.isDelete,0)=0)
	inner join dbo.Food f
	ON (f.Id=fd.FoodId ANd ISNULL(f.IsDelete,0)=0)	
	where fm.SchoolsId=@SchoolId AND fd.DateMenu=@DateMenu and isnull(fm.IsDelete,0)=0
)t
group by [Id]
		  ,[FoodMenuCode]
		  ,[FoodMenuId]
		  ,[NumberFood]
		  ,[MaterialId]
		  ,[MaterialCode]
		  ,[MaterialName]
		  ,[UnitName]
		  ,[NumberFoodTeacher]
		  ,[NumberFoodStudent]
		  ,[FoodCode]
		  ,[FoodName]
		  ,[FoodId]
end
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterials_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ExportWarehousingMaterials_Insert]
			@WarehouseId decimal(18,0)
           ,@WarehouseIdTo decimal(18,0)
           ,@ImplementationDates datetime
           ,@WarehousingMaterialsCode varchar(50)
           ,@IsConfirm int
           ,@IsImport int
           ,@CreateBy varchar(50)
           ,@SchoolId numeric(18,0)
As
begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTableWareHouse]
		@colName = N'WarehousingMaterialsCode',
		@tableName = N'WarehousingMaterials',
		@IsImport = 0,
		@strCode = @strCode OUTPUT
INSERT INTO  [WarehousingMaterials]
           ([WarehouseId]
           ,[WarehouseIdTo]
           ,[ImplementationDates]
           ,[WarehousingMaterialsCode]
           ,[IsConfirm]
           ,[IsImport]
           ,[CreateBy]
           ,[CreateDate]
           ,[SchoolId])
     VALUES
           (
		    @WarehouseId
           ,@WarehouseIdTo
           ,@ImplementationDates
           ,@strCode
           ,@IsConfirm
           ,@IsImport
           ,@CreateBy
           ,GetDate()
           ,@SchoolId)
           IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =@@IDENTITY;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
   End;
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterials_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ExportWarehousingMaterials_Update]
 @Id decimal(18,0)
,@WarehouseId decimal(18,0)
,@WarehouseIdTo decimal(18,0)
,@ImplementationDates datetime
,@IsConfirm int
,@ModifiedBy varchar(50)
,@SchoolId numeric(18,0)
As
begin
declare @p_count numeric(18, 0)=0;

Update[WarehousingMaterials]
Set  [WarehouseId]=@WarehouseId
	,[WarehouseIdTo]=@WarehouseIdTo
	,[ImplementationDates]=@ImplementationDates
	,[IsConfirm]=@IsConfirm
	,[SchoolId]=@SchoolId
	 ,[ModifiedBy]=@ModifiedBy
      ,[ModifiedDate]=GETDATE()
	where Id=@Id;
           IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
   End;
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterialsById]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ExportWarehousingMaterialsById]
@WarehousingMaterialsId numeric(18,0)
as
begin
select M.MaterialCode
,M.MaterialName
,wd.Quantity
,U.UnitCode
,U.UnitName
,wd.MaterialId
,w.SchoolId
,wd.FoodId
,0 as MaterialsFifoId
,0 as Price
from dbo.WarehousingMaterialsDetail wd
inner join dbo.WarehousingMaterials w
ON (wd.WarehousingMaterialsId=w.Id AND isnull(w.IsDelete,0)=0)
inner join dbo.Material M
ON(M.Id=wd.MaterialId AND ISNULL(M.IsDelete,0)=0)
inner join dbo.Unit U
ON (M.UnitId=U.Id AND ISNULL(U.IsDelete,0)=0)
Where Isnull(wd.IsDelete,0)=0 AND IsNull(W.IsImport,0)=0
AND wd.WarehousingMaterialsId=@WarehousingMaterialsId;
END;
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterialsById_Excecl]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterialsById]    Script Date: 11/21/2017 08:57:12 ******/
CREATE proc [dbo].[ExportWarehousingMaterialsById_Excecl]
@WarehousingMaterialsId numeric(18,0)
as
begin
select 
ROW_NUMBER() OVER(ORDER BY wd.FoodId DESC) OrderNo,
M.MaterialCode
,M.MaterialName
,wd.Quantity as Import
,U.UnitCode
,U.UnitName
,wd.MaterialId
,w.SchoolId
,wd.FoodId
,(CASE WHEN wd.FoodId=0 then N'Hàng thêm' else f.FoodName end) as FoodName
from dbo.WarehousingMaterialsDetail wd
inner join dbo.WarehousingMaterials w
ON (wd.WarehousingMaterialsId=w.Id AND isnull(w.IsDelete,0)=0)
inner join dbo.Material M
ON(M.Id=wd.MaterialId AND ISNULL(M.IsDelete,0)=0)
inner join dbo.Unit U
ON (M.UnitId=U.Id AND ISNULL(U.IsDelete,0)=0)
Left join dbo.Food f
ON (f.Id=wd.FoodId AND ISNULL(f.IsDelete,0)=0)
Where Isnull(wd.IsDelete,0)=0 AND IsNull(W.IsImport,0)=0
AND wd.WarehousingMaterialsId=@WarehousingMaterialsId;
END;
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterialsDetail_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[ExportWarehousingMaterialsDetail_Delete]
@WarehousingMaterialsId numeric(18,0)
,@ModifiedBy varchar(50) 
As
BEGIN
declare @p_count numeric(18, 0)=0;
UPDATE [WarehousingMaterialsDetail]
   SET 
       [IsDelete] = 1
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate]=GETDATE()
      where WarehousingMaterialsId=@WarehousingMaterialsId
      AND ISNULL(IsDelete,0)=0;
			SET @p_count =1;
   select @p_count;
END
GO
/****** Object:  StoredProcedure [dbo].[ExportWarehousingMaterialsDetail_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ExportWarehousingMaterialsDetail_Insert]
 @WarehousingMaterialsId decimal(18,0)
,@MaterialId numeric(18,0)
,@Quantity numeric(18,4)
,@CreateBy varchar(50)
,@FoodId numeric(18,0)
,@PriceExport numeric(18,0)
as
begin
declare @p_count numeric(18, 0)=0;
INSERT INTO [WarehousingMaterialsDetail]
           ([WarehousingMaterialsId]
           ,[MaterialId]
           ,[Quantity]           
           ,[IsDelete]
           ,[CreateBy]
           ,[CreateDate]
           ,[FoodId]
           ,[PriceExport])
     VALUES
           (@WarehousingMaterialsId
			,@MaterialId
			,@Quantity
			,0
			,@CreateBy
			,GETDATE()
			,@FoodId
			,@PriceExport)
           IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =SCOPE_IDENTITY();
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end
GO
/****** Object:  StoredProcedure [dbo].[Food_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[Food_Delete] 
 @Id numeric(18, 0)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
Update [Food]
SET [IsDelete]=1
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           Where Id=@Id
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[Food_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Food_GetAll]
 @FoodCode  varchar(10)
,@FoodName nvarchar(300)
as
begin
 SELECT	 f.Id 		   
		,f.FoodCode    
		,f.FoodName    
		,f.FoodTypeId  
		,f.Remark      
		,f.RowVersion  
		,f.IsDelete    
		,f.CreateBy    
		,f.CreateDate  
		,f.ModifiedBy  
		,f.ModifiedDate
		,t.FoodTypesName
		FROM  Food 	f
		LEFT JOIN FoodTypes t
		ON (f.FoodTypeId=t.Id And ISNULL(t.IsDelete,0)=0)
		WHERE ISNULL(f.IsDelete,0) = 0
		AND (@FoodCode is null OR f.FoodCode=@FoodCode)
		AND (@FoodName is null OR f.FoodName=@FoodName)
		Order by FoodTypeId;                    
end;
GO
/****** Object:  StoredProcedure [dbo].[Food_GetAllByFoodType]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[Food_GetAllByFoodType]
 @FoodTypeId  int
as
begin
 SELECT	 f.Id 	
		,f.FoodName    
		FROM  Food 	f
		WHERE ISNULL(f.IsDelete,0) = 0
		AND (FoodTypeId=@FoodTypeId)                  
end;
GO
/****** Object:  StoredProcedure [dbo].[Food_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Food_Insert] 
 @FoodCode  varchar(10)
,@FoodName nvarchar(300)
,@FoodTypeId  int
,@Remark nvarchar(500)
,@CreateBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = N'FoodCode',
		@tableName = N'Food',
		@strCode = @strCode OUTPUT

INSERT INTO [Food]
           (
               [FoodCode]
			  ,[FoodName]
			  ,[FoodTypeId]
			  ,[Remark]
			  ,[CreateBy]
			  ,[CreateDate]
           )
     VALUES
           (   @strCode
			  ,@FoodName
			  ,@FoodTypeId
			  ,@Remark
			  ,@CreateBy
			  ,GETDATE()
			)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =SCOPE_IDENTITY();
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[Food_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[Food_Update]
  @Id numeric(18,0) 
 ,@FoodCode  varchar(10)
,@FoodName nvarchar(300)
,@FoodTypeId  int
,@Remark nvarchar(500)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
Update [Food]
SET         [FoodCode]=@FoodCode
           ,[FoodName]=@FoodName
           ,[FoodTypeId]=@FoodTypeId
           ,[Remark]=@Remark
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           Where Id=@Id
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodDetail_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[FoodDetail_Delete] 
			@FoodId numeric(18, 0) 
AS
begin
declare @p_count numeric(18, 0)=0;
Delete From FoodDetail Where FoodId=@FoodId
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodDetail_DeleteByFoodId]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[FoodDetail_DeleteByFoodId] 
 @FoodId numeric(18, 0)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
Update [FoodDetail]
SET [IsDelete]=1
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           Where FoodId=@FoodId
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodDetail_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[FoodDetail_GetAll]
as
begin
 SELECT	 * FROM  FoodDetail 	f
		WHERE ISNULL(f.IsDelete,0) = 0;                    
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodDetail_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[FoodDetail_Insert] 
 @FoodId  numeric(18,0)
,@MaterialId numeric(18,0)
,@CreateBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
INSERT INTO [FoodDetail]
           (
               [FoodId]
			  ,[MaterialId]
			  ,[CreateBy]
			  ,[CreateDate]
           )
     VALUES
           ( @FoodId
			,@MaterialId
			,@CreateBy
			,GETDATE()
			)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodMenu_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[FoodMenu_Delete]
@Id numeric(18,0)  
,@ModifiedBy varchar(50) 
As
BEGIN
declare @p_count numeric(18, 0)=0;
UPDATE [FoodMenu]
   SET 
       [IsDelete] = 1
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate]=GETDATE()
      where Id=@Id
      AND ISNULL(IsDelete,0)=0;
      IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
END
GO
/****** Object:  StoredProcedure [dbo].[FoodMenu_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Proc [dbo].[FoodMenu_GetAll]
 @FromDay  Date
,@ToDay Date
,@SchoolsId numeric(18, 0)
as
begin
 SELECT	 fm.Id 		    
		,fm.FoodMenuCode
		,fm.NumberFood 	
		,CONVERT(VarChar(50), fm.FromDate, 103) as FromDay
		,CONVERT(VarChar(50), fm.ToDate, 103) as ToDay
		,'' as DayChoose
		,fm.SchoolsId
		,(ISNULL(s.NumberStudentHighSchool,0)+ISNULL(s.NumberStudentJuniorHigh,0)+ISNULL(s.NumberStudentPrimary,0)) as NumberFoodStudent
		,(ISNULL(s.NumberTeacher,0)) as NumberFoodTeacher
		FROM  FoodMenu 	fm
		inner join Schools s
		ON (s.Id=fm.SchoolsId AND ISNULL(s.IsDelete,0)=0)
		WHERE ISNULL(fm.IsDelete,0) = 0
		AND (fm.SchoolsId=@SchoolsId)
		AND (fm.FromDate =@FromDay)
		AND (fm.ToDate =@ToDay);                    
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodMenu_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[FoodMenu_Insert]
  @FoodMenuCode nvarchar(50)
 ,@FromDate date
 ,@ToDate date 
 ,@SchoolsId numeric(18,0)  
,@NumberFood int
,@CreateBy varchar(50) 
as
Begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = N'FoodMenuCode',
		@tableName = N'FoodMenu',
		@strCode = @strCode OUTPUT
INSERT INTO [FoodMenu]
           (
           [FoodMenuCode]
		  ,[FromDate]
		  ,[ToDate]
		  ,[SchoolsId]
		  ,[NumberFood]
           ,[IsDelete]
           ,[CreateBy]
           ,[CreateDate]
           )
     VALUES
           (
           @strCode
           ,  @FromDate
             ,@ToDate 		
			,@SchoolsId
			,@NumberFood		
			,0 	
			,@CreateBy
			,GETDATE())
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =@@IDENTITY ;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end
GO
/****** Object:  StoredProcedure [dbo].[FoodMenu_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[FoodMenu_Update]
  @Id numeric(18,0)  
 ,@FromDate date
 ,@ToDate date 
 ,@SchoolsId numeric(18,0)  
,@NumberFood int
,@ModifiedBy varchar(50) 
as
Begin
declare @p_count numeric(18, 0)=0;
UPDATE  [FoodMenu]
   SET 
       [FromDate] = @FromDate
      ,[ToDate] = @ToDate
      ,[SchoolsId] = @SchoolsId
      ,[NumberFood] = @NumberFood      
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate] = GETDATE()
 WHERE Id=@id
 IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end
GO
/****** Object:  StoredProcedure [dbo].[FoodMenuDetail_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[FoodMenuDetail_Delete]
@FoodMenuId numeric(18,0)
,@ModifiedBy varchar(50) 
As
BEGIN
declare @p_count numeric(18, 0)=0;
UPDATE [FoodMenu_Detail]
   SET 
       [IsDelete] = 1
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate]=GETDATE()
      where FoodMenuId=@FoodMenuId
      AND ISNULL(IsDelete,0)=0;
			SET @p_count =1;
   select @p_count;
END
GO
/****** Object:  StoredProcedure [dbo].[FoodMenuDetail_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Proc [dbo].[FoodMenuDetail_GetAll]
 @FromDay  Date
,@ToDay Date
,@SchoolsId numeric(18,0)
as
begin
 SELECT	 fm.Id 		    
		,fm.FoodId   
		,f.FoodTypeId 
		,f.FoodName 
		,CONVERT(VarChar(50), fm.DateMenu, 103) as DateMenu
		,fm.NumberFoodTeacher
           ,fm.NumberFoodStudent
		FROM  FoodMenu_Detail 	fm
		Inner join Food f
		ON(fm.FoodId=f.Id AND ISNULL(f.IsDelete,0)=0)
		inner join FoodMenu m
		ON (m.SchoolsId=@SchoolsId ANd ISNULL(m.IsDelete,0)=0 and m.Id=fm.FoodMenuId)
		WHERE ISNULL(fm.IsDelete,0) = 0 
		AND (fm.DateMenu >=@FromDay)
		AND (fm.DateMenu <=@ToDay);                    
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodMenuDetail_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[FoodMenuDetail_Insert]
@FoodMenuId numeric(18,0),
 @DateMenu date 
,@FoodId numeric(18,0)  
,@CreateBy varchar(50)
,@NumberFoodTeacher int
,@NumberFoodStudent int 
as
Begin
declare @p_count numeric(18, 0)=0;
INSERT INTO [FoodMenu_Detail]
           (
           [FoodMenuId]
           ,[DateMenu]
           ,[FoodId]
           ,[IsDelete]
           ,[CreateBy]
           ,[CreateDate]
           ,[NumberFoodTeacher]
           ,[NumberFoodStudent]
           )
     VALUES
           (
           @FoodMenuId,
             @DateMenu 		
			,@FoodId		
			,0 	
			,@CreateBy
			,GETDATE()
			,@NumberFoodTeacher
           ,@NumberFoodStudent)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end
GO
/****** Object:  StoredProcedure [dbo].[FoodReality_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[FoodReality_Delete]
@Id numeric(18,0)  
,@ModifiedBy varchar(50) 
As
BEGIN
declare @p_count numeric(18, 0)=0;
UPDATE [FoodReality]
   SET 
       [IsDelete] = 1
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate]=GETDATE()
      where Id=@Id
      AND ISNULL(IsDelete,0)=0;
      IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
END
GO
/****** Object:  StoredProcedure [dbo].[FoodReality_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[FoodReality_GetAll]
@SchoolId numeric(18,0),
@DateFoodReality datetime
as
begin
select distinct
(Case When Isnull(FT.IsSession,0) =1 then N'Buổi sáng'
	 When Isnull(FT.IsSession,0) =2 Then N'Buổi trưa'
	 When Isnull(FT.IsSession,0) =3 Then N'Buổi xế'
	 When Isnull(FT.IsSession,0) =4 Then N'Buổi chiều' ENd) as SessionName,
fd.NumberFoodStudent,
fd.NumberFoodTeacher,
Fm.SchoolsId as SchoolId,
CONVERT(VarChar(50), fd.DateMenu, 103) as DateFoodReality,
ISNULL(fr.Id,0) as FoodRealityId,
(Case when ISNULL(fr.IsConfirm,0) >0 then 1 else 0 end)  as IsConfirm,
Isnull(fr.NumberFoodStudentReality,0)as NumberFoodStudentReality, 
ISNULL(fr.NumberFoodTeacherReality,0) as NumberFoodTeacherReality,
ISNULL(fr.PriceTeacherReality,0) as PriceTeacherReality,
ISNULL(fr.PriceStudentReality,0) as PriceStudentReality
from dbo.FoodMenu fm
inner join dbo.FoodMenu_Detail fd
ON(fd.FoodMenuId=fm.Id AND ISNULL(fd.IsDelete,0)=0)
Inner join Food F
ON(F.Id=fd.FoodId AND Isnull(F.IsDelete,0)=0)
Inner join dbo.FoodTypes FT
ON (FT.Id=F.FoodTypeId AND ISNULL(Ft.IsDelete,0)=0)
Left join dbo.FoodReality fr
ON(fr.SchoolId=fm.SchoolsId AND fr.DateFoodReality=fd.DateMenu)
WHERE isnull(fm.IsDelete,0)=0
AND fm.SchoolsId=@SchoolId
AND fd.DateMenu=@DateFoodReality
ENd;
GO
/****** Object:  StoredProcedure [dbo].[FoodReality_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[FoodReality_Insert]
@SchoolId numeric(18,0),
@DateFoodReality datetime,
@NumberFoodTeacher numeric(18,0),
@NumberFoodStudent numeric(18,0),
@NumberFoodTeacherReality numeric(18,0),
@NumberFoodStudentReality numeric(18,0),
@PriceStudentReality numeric(18,0),
@PriceTeacherReality numeric(18,0),
@IsConfirm int,
@CreateBy varchar(50)
as
Begin
declare @p_count numeric(18, 0)=0;
INSERT INTO [FoodReality]
           ([SchoolId]
           ,[DateFoodReality]
           ,[NumberFoodTeacher]
           ,[NumberFoodStudent]
           ,[NumberFoodTeacherReality]
           ,[NumberFoodStudentReality]
           ,[PriceStudentReality]
           ,[PriceTeacherReality]
           ,[IsConfirm]
           ,[CreateBy]
           ,[CreateDate])
     VALUES
           (@SchoolId,
			@DateFoodReality ,
			@NumberFoodTeacher ,
			@NumberFoodStudent ,
			@NumberFoodTeacherReality ,
			@NumberFoodStudentReality ,
			@PriceStudentReality ,
			@PriceTeacherReality ,
			@IsConfirm,
			@CreateBy,
			GETDATE())
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =SCOPE_IDENTITY();
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
   End;
GO
/****** Object:  StoredProcedure [dbo].[FoodReality_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[FoodReality_Update]
@Id numeric(18,0),  
@SchoolId numeric(18,0),
@DateFoodReality datetime,
@NumberFoodTeacher numeric(18,0),
@NumberFoodStudent numeric(18,0),
@NumberFoodTeacherReality numeric(18,0),
@NumberFoodStudentReality numeric(18,0),
@PriceStudentReality numeric(18,0),
@PriceTeacherReality numeric(18,0),
@IsConfirm int,
@ModifiedBy varchar(50)
as
Begin
declare @p_count numeric(18, 0)=0;
UPDATE  [FoodReality]
   SET 
            [SchoolId]=@SchoolId
           ,[DateFoodReality]=@DateFoodReality
           ,[NumberFoodTeacher]=@NumberFoodTeacher
           ,[NumberFoodStudent]=@NumberFoodStudent
           ,[NumberFoodTeacherReality]=@NumberFoodTeacherReality
           ,[NumberFoodStudentReality]=@NumberFoodStudentReality
           ,[PriceStudentReality]=@PriceStudentReality
           ,[PriceTeacherReality]=@PriceTeacherReality
           ,[IsConfirm]  =@IsConfirm    
		   ,[ModifiedBy] = @ModifiedBy
           ,[ModifiedDate] = GETDATE()
 WHERE Id=@id
 IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end
GO
/****** Object:  StoredProcedure [dbo].[FoodTypes_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[FoodTypes_Delete] 
 @Id numeric(18, 0)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
Update [FoodTypes]
SET [IsDelete]=1
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           Where Id=@Id
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodTypes_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[FoodTypes_GetAll]
as
begin
   SELECT	
  	Id				
  ,	FoodTypesCode    
  ,	FoodTypesName    
  ,	Remark           
  ,	RowVersion            
  FROM	FoodTypes 	
  WHERE	ISNULL(IsDelete,0) = 0	;
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodTypes_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[FoodTypes_Insert] 
 @FoodTypesCode  varchar(10)
,@FoodTypesName nvarchar(300)
,@Remark nvarchar(500)
,@CreateBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = N'FoodTypesCode',
		@tableName = N'FoodTypes',
		@strCode = @strCode OUTPUT
INSERT INTO [FoodTypes]
           (
            [FoodTypesCode]
           ,[FoodTypesName]
           ,[Remark]
           ,[CreateBy]
           ,[CreateDate]
           )
     VALUES
           ( @strCode
			,@FoodTypesName
			,@Remark
			,@CreateBy
			,GETDATE()
			)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[FoodTypes_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[FoodTypes_Update] 
 @Id numeric(18, 0)
,@FoodTypesCode  varchar(10)
,@FoodTypesName nvarchar(300)
,@Remark nvarchar(500)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
Update [FoodTypes]
SET [FoodTypesCode]=@FoodTypesCode
           ,[FoodTypesName]=@FoodTypesName
           ,[Remark]=@Remark
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           Where Id=@Id
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[GetCodeAllTable]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCodeAllTable]
(@colName AS VARCHAR(50), @tableName varchar(50),@strCode varchar(50) OUTPUT)
AS
BEGIN

	DECLARE @string VARCHAR(50)
	DECLARE @ParmDefinition NVARCHAR(500), @sqlQuery NVARCHAR(500);  
	SET @sqlQuery =N'SELECT @ID_OUT = MAX(RIGHT(T.'+ @colName +',9)) FROM ' + @tableName +' T '
	
	SET @ParmDefinition = N' @ID_OUT VARCHAR(50) OUTPUT '; 
	EXECUTE sp_executesql @sqlQuery, @ParmDefinition, @ID_OUT =  @string OUTPUT
	SET @string = RIGHT(@string,9)
	
	DECLARE @I INT, @position INT
	DECLARE @NEWCHAR VARCHAR(9), @FLGCHANGE BIT
	DECLARE @NEWID INT

	IF @string IS NULL OR LEN(@string) = 0
	BEGIN
		SET @string = 'A00000000'
	END
	SET @I = 0
	SET @position = 1

	-- GET NEW CURRENT ID OF STRING
	WHILE @position <= LEN(@string)  
	   BEGIN  
	   -- CHECK CHAR IS NUMBER OR CHAR
	   IF ASCII(SUBSTRING(@string, @position, 1)) >= 65 
	   BEGIN
			-- SET FLAG INDEX OF FIRST CHAR
			SET @I = @position
	   END 
	   SET @position = @position + 1  
	END;  
	

	-- SET NEW ID
	SET @NEWID = CAST(RIGHT(@string,LEN(@string)-@I) AS INT) + 1
	
	-- GET CURRENT CHAR INDEX OF CODE
	SET @NEWCHAR = SUBSTRING(@string, 0, @I + 1)
	
	-- CHECK ID AND SET NEW CHAR OF FIRST IN CODE
	IF LEN(@NEWID) > LEN(@string)-@I
	BEGIN
		SET @position = LEN(@NEWCHAR) - 1
		WHILE @position >= 0
		   BEGIN  
			IF ASCII(SUBSTRING(@NEWCHAR, @position + 1, 1)) + 1 > 90 -- IF CURRENT CHAR IS 'Z'
			BEGIN
				-- PROCESS NEW NEXT CHAR
				-- AAZ => ABA
				SET @NEWCHAR = SUBSTRING(@NEWCHAR, 0, @position + 1) +
				'A' +
				SUBSTRING(@NEWCHAR, @position + 2, LEN(@NEWCHAR) - 1) 
				SET @NEWID = 1
				IF @position = 0 -- CHECK IF CHAR 'Z' IN FIRST CODE
				BEGIN
					SET @FLGCHANGE = 1
				END
			END
			ELSE
			BEGIN
				-- SET NEW CHAR OF CODE
				-- AAA -> AAB
				SET @NEWCHAR =  
				SUBSTRING(@NEWCHAR, 0, @position + 1) +
				CHAR(ASCII(SUBSTRING(@NEWCHAR, @position + 1, 1)) + 1 ) + 
				SUBSTRING(@NEWCHAR, @position + 2, LEN(@NEWCHAR) - 1)
				BREAK
			END
		   SET @position = @position - 1
		END;  
	END

	IF @FLGCHANGE = 1
	BEGIN
		SET @NEWCHAR = 'A' + @NEWCHAR
		SET @I = @I + 1
	END 
	--- SET NEW CODE RETURN
	SET @string =  @NEWCHAR + RIGHT('00000000' + CAST(@NEWID as varchar(20)), LEN(@string)-@I)
	SET @strCode=@string;
	--SELECT @strCode;
END
GO
/****** Object:  StoredProcedure [dbo].[GetCodeAllTableWareHouse]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCodeAllTableWareHouse]
(@colName AS VARCHAR(50), @tableName varchar(50),@isImport VARCHAR,@strCode varchar(50) OUTPUT)
AS
BEGIN

	DECLARE @string VARCHAR(50)
	DECLARE @ParmDefinition NVARCHAR(500), @sqlQuery NVARCHAR(500);  
	SET @sqlQuery =N'SELECT @ID_OUT = MAX(RIGHT(T.'+ @colName +',9)) FROM ' + @tableName +' T WHERE ISIMPORT='+@isImport;
	--SET @sqlQuery =N'SELECT @ID_OUT =  MAX(RIGHT(T.WarehousingMaterialsCode,9)) FROM WarehousingMaterials T WHERE ISIMPORT='+@isImport;
	SET @ParmDefinition = N' @ID_OUT VARCHAR(50) OUTPUT '; 
	EXECUTE sp_executesql @sqlQuery, @ParmDefinition, @ID_OUT =  @string OUTPUT
	SET @string = RIGHT(@string,9)
	
	DECLARE @I INT, @position INT
	DECLARE @NEWCHAR VARCHAR(9), @FLGCHANGE BIT
	DECLARE @NEWID INT

	IF @string IS NULL OR LEN(@string) = 0
	BEGIN
		SET @string = 'A00000000'
	END
	SET @I = 0
	SET @position = 1

	-- GET NEW CURRENT ID OF STRING
	WHILE @position <= LEN(@string)  
	   BEGIN  
	   -- CHECK CHAR IS NUMBER OR CHAR
	   IF ASCII(SUBSTRING(@string, @position, 1)) >= 65 
	   BEGIN
			-- SET FLAG INDEX OF FIRST CHAR
			SET @I = @position
	   END 
	   SET @position = @position + 1  
	END;  
	

	-- SET NEW ID
	SET @NEWID = CAST(RIGHT(@string,LEN(@string)-@I) AS INT) + 1
	
	-- GET CURRENT CHAR INDEX OF CODE
	SET @NEWCHAR = SUBSTRING(@string, 0, @I + 1)
	
	-- CHECK ID AND SET NEW CHAR OF FIRST IN CODE
	IF LEN(@NEWID) > LEN(@string)-@I
	BEGIN
		SET @position = LEN(@NEWCHAR) - 1
		WHILE @position >= 0
		   BEGIN  
			IF ASCII(SUBSTRING(@NEWCHAR, @position + 1, 1)) + 1 > 90 -- IF CURRENT CHAR IS 'Z'
			BEGIN
				-- PROCESS NEW NEXT CHAR
				-- AAZ => ABA
				SET @NEWCHAR = SUBSTRING(@NEWCHAR, 0, @position + 1) +
				'A' +
				SUBSTRING(@NEWCHAR, @position + 2, LEN(@NEWCHAR) - 1) 
				SET @NEWID = 1
				IF @position = 0 -- CHECK IF CHAR 'Z' IN FIRST CODE
				BEGIN
					SET @FLGCHANGE = 1
				END
			END
			ELSE
			BEGIN
				-- SET NEW CHAR OF CODE
				-- AAA -> AAB
				SET @NEWCHAR =  
				SUBSTRING(@NEWCHAR, 0, @position + 1) +
				CHAR(ASCII(SUBSTRING(@NEWCHAR, @position + 1, 1)) + 1 ) + 
				SUBSTRING(@NEWCHAR, @position + 2, LEN(@NEWCHAR) - 1)
				BREAK
			END
		   SET @position = @position - 1
		END;  
	END

	IF @FLGCHANGE = 1
	BEGIN
		SET @NEWCHAR = 'A' + @NEWCHAR
		SET @I = @I + 1
	END 
	--- SET NEW CODE RETURN
	SET @string =  @NEWCHAR + RIGHT('00000000' + CAST(@NEWID as varchar(20)), LEN(@string)-@I)
	SET @strCode=@string;
	--SELECT @strCode;
END
GO
/****** Object:  StoredProcedure [dbo].[GroupMaterial_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GroupMaterial_Delete] 
 @Id numeric(18, 0)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
Update [GroupMaterial]
SET [IsDelete]=1
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           Where Id=@Id
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[GroupMaterial_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[GroupMaterial_GetAll]
as
begin
   SELECT	
  	Id				
  ,	GroupMaterialCode    
  ,	GroupMaterialName    
  ,	Remark           
  ,	RowVersion            
  FROM	GroupMaterial 	
  WHERE	ISNULL(IsDelete,0) = 0	;
end;
GO
/****** Object:  StoredProcedure [dbo].[GroupMaterial_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[GroupMaterial_Insert] 
 @GroupMaterialCode  varchar(10)
,@GroupMaterialName nvarchar(300)
,@Remark nvarchar(500)
,@CreateBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = N'GroupMaterialCode',
		@tableName = N'GroupMaterial',
		@strCode = @strCode OUTPUT
INSERT INTO [GroupMaterial]
           (
            [GroupMaterialCode]
           ,[GroupMaterialName]
           ,[Remark]
           ,[CreateBy]
           ,[CreateDate]
           )
     VALUES
           ( @strCode
			,@GroupMaterialName
			,@Remark
			,@CreateBy
			,GETDATE()
			)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[GroupMaterial_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[GroupMaterial_Update] 
@Id numeric(18, 0)
,@GroupMaterialCode  varchar(10)
,@GroupMaterialName nvarchar(300)
,@Remark nvarchar(500)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
UPdate [GroupMaterial]
       
           SET [GroupMaterialName]=@GroupMaterialName
           ,[GroupMaterialCode]=@GroupMaterialCode
           ,[Remark]=@Remark
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
         Where Id=@Id AND ISNULL(IsDelete,0)=0
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[ImportWarehousingMaterials_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ImportWarehousingMaterials_GetAll]
@FromDate datetime,
@ToDate datetime
as
begin
SELECT w.[Id]
      ,w.[WarehouseId]
      ,wh.WarehouseCode
      ,wh.WarehouseName
      ,CONVERT(VarChar(50), w.ImplementationDates, 103) as ImplementationDates
      ,w.[IsImport]      
      ,w.[WarehousingMaterialsCode]
      ,w.[IsConfirm]
      ,CASE WHEN w.[IsConfirm]=1 THEN N'Đã xác nhận' else N'Chưa xác nhận' end ConfirmName
  FROM [WarehousingMaterials]w
  inner join dbo.Warehouse wh
  on (wh.Id=w.WarehouseId and ISNULL(wh.IsDelete,0)=0)
  where ISNULL(w.isdelete,0)=0 ANd ISNULL(w.IsImport,0)=1
  AND (w.ImplementationDates >=@FromDate)
  AND (w.ImplementationDates <=@ToDate);     
end
GO
/****** Object:  StoredProcedure [dbo].[ImportWarehousingMaterials_GetByFoodMenuCode]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ImportWarehousingMaterials_GetByFoodMenuCode]
@FoodMenuCode varchar(50)
as
begin
select [Id]
		  ,[FoodMenuCode]
		  ,[FoodMenuId]
		  ,[NumberFood]
		  ,[MaterialId]
		  ,[MaterialCode]
		  ,[MaterialName]
		  ,[UnitName]
		  ,(SUM(QuantityOne)+SUM(QuantityTwo)+SUM(QuantityOrther)) as Quantity
		  ,0 as PriceReceived
from
(
	select 
		   fm.[Id]
		  ,fm.[FoodMenuCode]
		  ,fd.[FoodMenuId]
		  ,fm.[NumberFood]
		  ,qf.[MaterialId]
		  ,m.[MaterialCode]
		  ,m.[MaterialName]
		  ,u.[UnitName]
		  ,IsNull(qf.[QuantitativeOne],0)*isnull(fm.[NumberFood],0) as QuantityOne
		  ,IsNull(qf.[QuantitativeTwo],0)*isnull(fm.[NumberFood],0) as QuantityTwo
		  ,IsNull(qf.[QuantitativeOrther],0)*isnull(fm.[NumberFood],0) as QuantityOrther
	from dbo.FoodMenu fm
	inner join dbo.FoodMenu_Detail fd
	on (fm.Id=fd.FoodMenuId AND Isnull(fd.IsDelete,0)=0)
	Inner join dbo.QuantitativeFood qf
	on (qf.FoodId=fd.FoodId and ISNULL(qf.isDelete,0)=0)
	inner join dbo.Material m
	ON  (qf.MaterialId=m.Id and ISNULL(m.isDelete,0)=0)
	inner join dbo.Unit u
	ON  (u.Id=m.UnitId and ISNULL(u.isDelete,0)=0)
	where fm.FoodMenuCode=@FoodMenuCode
)t
group by [Id]
		  ,[FoodMenuCode]
		  ,[FoodMenuId]
		  ,[NumberFood]
		  ,[MaterialId]
		  ,[MaterialCode]
		  ,[MaterialName]
		  ,[UnitName]
end
GO
/****** Object:  StoredProcedure [dbo].[ImportWarehousingMaterials_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ImportWarehousingMaterials_Insert]
 @WarehouseId decimal(18,0)
,@ImplementationDates datetime
,@IsImport int
,@CreateBy varchar(50)
,@WarehousingMaterialsCode varchar(50)
,@IsConfirm int
As
begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTableWareHouse]
		@colName = N'WarehousingMaterialsCode',
		@tableName = N'WarehousingMaterials',
		@IsImport = 1,
		@strCode = @strCode OUTPUT
INSERT INTO [WarehousingMaterials]
           ([WarehouseId]
           ,[ImplementationDates]
           ,[IsImport]
           ,[IsDelete]
           ,[CreateBy]
           ,[CreateDate]
           ,[WarehousingMaterialsCode]
           ,[IsConfirm])
     VALUES
           ( @WarehouseId
			 ,@ImplementationDates
			 ,@IsImport
			 ,0
			 ,@CreateBy
			 ,GETDATE()
			 ,@strCode
			 ,@IsConfirm)
           IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =@@IDENTITY;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
   End;
GO
/****** Object:  StoredProcedure [dbo].[ImportWarehousingMaterials_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ImportWarehousingMaterials_Update]
 @Id decimal(18,0),
 @WarehouseId decimal(18,0)
,@ImplementationDates datetime
,@IsImport int
,@ModifiedBy varchar(50)
,@IsConfirm int
As
begin
declare @p_count numeric(18, 0)=0;
Update[WarehousingMaterials]
Set[WarehouseId]= @WarehouseId
           ,[ImplementationDates]=@ImplementationDates           
           ,[IsImport]=@IsImport
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           ,[IsConfirm]=@IsConfirm
	where Id=@Id;
           IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
   End;
GO
/****** Object:  StoredProcedure [dbo].[ImportWarehousingMaterialsById]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ImportWarehousingMaterialsById]
@WarehousingMaterialsId numeric(18,0)
as
begin
select M.MaterialCode
,M.MaterialName
,wd.Quantity
,U.UnitCode
,U.UnitName
,wd.MaterialId
,wd.PriceReceived
from dbo.WarehousingMaterialsDetail wd
inner join dbo.WarehousingMaterials w
ON (wd.WarehousingMaterialsId=w.Id AND isnull(w.IsDelete,0)=0)
inner join dbo.Material M
ON(M.Id=wd.MaterialId AND ISNULL(M.IsDelete,0)=0)
Left join dbo.Unit U
ON (M.UnitId=U.Id AND ISNULL(U.IsDelete,0)=0)
Where Isnull(wd.IsDelete,0)=0 AND IsNull(W.IsImport,0)=1
AND wd.WarehousingMaterialsId=@WarehousingMaterialsId;
END;
GO
/****** Object:  StoredProcedure [dbo].[ImportWarehousingMaterialsDetail_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[ImportWarehousingMaterialsDetail_Delete]
@WarehousingMaterialsId numeric(18,0)
,@ModifiedBy varchar(50) 
As
BEGIN
declare @p_count numeric(18, 0)=0;
UPDATE [WarehousingMaterialsDetail]
   SET 
       [IsDelete] = 1
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate]=GETDATE()
      where WarehousingMaterialsId=@WarehousingMaterialsId
      AND ISNULL(IsDelete,0)=0;
			SET @p_count =1;
   select @p_count;
END
GO
/****** Object:  StoredProcedure [dbo].[ImportWarehousingMaterialsDetail_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ImportWarehousingMaterialsDetail_Insert]
 @WarehousingMaterialsId decimal(18,0)
,@MaterialId numeric(18,0)
,@Quantity numeric(18,4)
,@CreateBy varchar(50)
,@PriceReceived numeric(18,4)
as
begin
declare @p_count numeric(18, 0)=0;
INSERT INTO [WarehousingMaterialsDetail]
           ([WarehousingMaterialsId]
           ,[MaterialId]
           ,[Quantity]           
           ,[IsDelete]
           ,[CreateBy]
           ,[CreateDate]
           ,[PriceReceived])
     VALUES
           (@WarehousingMaterialsId
			,@MaterialId
			,@Quantity
			,0
			,@CreateBy
			,GETDATE()
			,@PriceReceived)
           IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =SCOPE_IDENTITY();
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end
GO
/****** Object:  StoredProcedure [dbo].[Inventory_GetAllInventoryList]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Inventory_GetAllInventoryList]
@FromDate DateTime
,@ToDate DAtetime
,@InventoryCode varchar(10)
AS
BEGIN
select 
 CONVERT(VarChar(50), I.DateInventory, 103) as DateInventory
,I.WarehouseId
,I.IsFinish
,I.Id
,I.InventoryCode
,W.WarehouseCode
,W.WarehouseName
,(CAse when I.IsFinish=1 then N'Đã chốt' else N'Chưa chôt' end) as StatusName 
from dbo.Inventory I
Inner join dbo.Warehouse W
ON (W.Id=I.WarehouseId AND ISnull(W.IsDelete,0)=0)
where ISNULL(I.IsDelete,0)=0
AND (@InventoryCode is null OR I.InventoryCode=@InventoryCode)
AND (@FromDate is null OR I.DateInventory>=@FromDate)
AND (@ToDate is null OR I.DateInventory<=@ToDate)
END
GO
/****** Object:  StoredProcedure [dbo].[Inventory_GetListMetarialCalculated]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Inventory_GetListMetarialCalculated]
 @Date Date
,@warehouseId Numeric(18,0)
as
Begin
declare @DateMax Date;
set @DateMax=Isnull((select Max(DateInventory)
 from dbo.Inventory
 WHERE ISNULL(IsDelete,0)=0 AND ISNULL(IsFinish,0)=1
 AND WarehouseId=@warehouseId),'1990-01-01')

Select 
 MaterialId
,WarehouseId
,SUM(QuantityImport) as QuantityImport
,SUM(QuantityExport) as QuantityExport
,SUM(QuantityInventory) as QuantityInventory
,((SUM(QuantityInventory)+SUM(QuantityImport))-SUM(QuantityExport)) as QuantityExist
,M.MaterialCode
,M.MaterialName
from
(
---lay danh sach nhap kho
select   WD.MaterialId		
		,W.WarehouseId
		,isnull(WD.Quantity,0) as QuantityImport
		,0 as QuantityExport
		,0 as QuantityInventory
from dbo.WarehousingMaterials W
Inner join dbo.WarehousingMaterialsDetail WD
ON (W.Id = WD.WarehousingMaterialsId AND ISNULL(WD.IsDelete,0)=0)
WHERE ISNULL(W.IsConfirm,0)=1 
AND ISNULL(W.IsImport,0)=1 AND (W.WarehouseId=@warehouseId)
AND W.ImplementationDates > @DateMax 
AND W.ImplementationDates<@Date
----lay danh sach xuat kho
UNION 
select   WD.MaterialId		
		,W.WarehouseId
		,0 as QuantityImport		
		,isnull(WD.Quantity,0) as QuantityExport
		,0 as QuantityInventory		
from dbo.WarehousingMaterials W
Inner join dbo.WarehousingMaterialsDetail WD
ON (W.Id = WD.WarehousingMaterialsId AND ISNULL(WD.IsDelete,0)=0)
WHERE ISNULL(W.IsConfirm,0)=1 AND W.ImplementationDates > @DateMax AND W.ImplementationDates<@Date
AND ISNULL(W.IsImport,0)=0 AND (W.WarehouseId=@warehouseId)
----Lay danh sach ton kho
UNION
select 
 ID.MaterialId
,I.WarehouseId
,0 as QuantityImport
,0 as QuantityExPort
,ID.Quantity as QuantityInventory
 from dbo.Inventory I
INNER JOIN dbo.InventoryDetail ID
ON (I.Id=ID.InventoryId AND ISNULL(ID.IsDelete,0)=0)
WHERE ISNULL(I.IsDelete,0)=0 AND ISNULL(I.IsFinish,0)=1
AND I.WarehouseId=@warehouseId 
AND I.DateInventory<@Date
AND I.DateInventory>=@DateMax
)T
INNER JOIN dbo.Material M
ON (T.MaterialId=M.Id AND IsNull(M.IsDelete,0)=0)
Group by MaterialId
		,WarehouseId
		,M.MaterialCode
		,M.MaterialName
End
GO
/****** Object:  StoredProcedure [dbo].[Inventory_GetListMetarialCalculatedById]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Proc [dbo].[Inventory_GetListMetarialCalculatedById]
 @Id Numeric(18,0)
,@warehouseId Numeric(18,0)
as
Begin
declare @FromDate Date;
declare @ToDate Date;
set @FromDate=Isnull((select Max(DateInventory)
 from dbo.Inventory
 WHERE ISNULL(IsDelete,0)=0 AND ISNULL(IsFinish,0)=1
 AND WarehouseId=@warehouseId AND Id<@Id),'1990-01-01');


set @ToDate=(select DateInventory
 from dbo.Inventory
 WHERE ISNULL(IsDelete,0)=0  AND Id=@Id)
 
Select 
 MaterialId
,WarehouseId
,SUM(QuantityImport) as QuantityImport
,SUM(QuantityExport) as QuantityExport
,SUM(QuantityInventory) as QuantityInventory
,((SUM(QuantityInventory)+SUM(QuantityImport))-SUM(QuantityExport)) as QuantityExist
,M.MaterialCode
,M.MaterialName
from
(
---lay danh sach nhap kho
select   WD.MaterialId		
		,W.WarehouseId
		,isnull(WD.Quantity,0) as QuantityImport
		,0 as QuantityExport
		,0 as QuantityInventory
from dbo.WarehousingMaterials W
Inner join dbo.WarehousingMaterialsDetail WD
ON (W.Id = WD.WarehousingMaterialsId AND ISNULL(WD.IsDelete,0)=0)
WHERE ISNULL(W.IsConfirm,0)=1 
AND ISNULL(W.IsImport,0)=1 AND (W.WarehouseId=@warehouseId)
AND W.ImplementationDates > @FromDate 
AND W.ImplementationDates<@ToDate
----lay danh sach xuat kho
UNION 
select   WD.MaterialId		
		,W.WarehouseId
		,0 as QuantityImport		
		,isnull(WD.Quantity,0) as QuantityExport
		,0 as QuantityInventory		
from dbo.WarehousingMaterials W
Inner join dbo.WarehousingMaterialsDetail WD
ON (W.Id = WD.WarehousingMaterialsId AND ISNULL(WD.IsDelete,0)=0)
WHERE ISNULL(W.IsConfirm,0)=1 AND W.ImplementationDates > @FromDate AND W.ImplementationDates<@ToDate
AND ISNULL(W.IsImport,0)=0 AND (W.WarehouseId=@warehouseId)
----Lay danh sach ton kho
UNION
select 
 ID.MaterialId
,I.WarehouseId
,0 as QuantityImport
,0 as QuantityExPort
,ID.Quantity as QuantityInventory
 from dbo.Inventory I
INNER JOIN dbo.InventoryDetail ID
ON (I.Id=ID.InventoryId AND ISNULL(ID.IsDelete,0)=0)
WHERE ISNULL(I.IsDelete,0)=0 AND ISNULL(I.IsFinish,0)=1
AND I.WarehouseId=@warehouseId AND (I.Id=@Id)
)T
INNER JOIN dbo.Material M
ON (T.MaterialId=M.Id AND IsNull(M.IsDelete,0)=0)
Group by MaterialId
		,WarehouseId
		,M.MaterialCode
		,M.MaterialName
End
GO
/****** Object:  StoredProcedure [dbo].[Inventory_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Inventory_Insert]
  @DateInventory date 
 ,@WarehouseId numeric(18,0)  
 ,@CreateBy varchar(50)
 ,@IsFinish int 
as
Begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = N'InventoryCode',
		@tableName = N'Inventory',
		@strCode = @strCode OUTPUT
INSERT INTO [Inventory]
           (
            [InventoryCode]
           ,[WarehouseId]
           ,[DateInventory]
           ,[IsFinish]           
           ,[CreateBy]
           ,[CreateDate]
           )
     VALUES
           (
				@strCode
               ,@WarehouseId
			   ,@DateInventory
			   ,@IsFinish           
			   ,@CreateBy
			   ,getdate())
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =@@IDENTITY ;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end
GO
/****** Object:  StoredProcedure [dbo].[Inventory_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Inventory_Update]
  @Id numeric(18,0)
 ,@DateInventory date 
 ,@WarehouseId numeric(18,0)  
 ,@ModifiedBy varchar(50)
 ,@IsFinish int 
as
Begin
declare @p_count numeric(18, 0)=0;
UPDATE [Inventory]
SET
            [WarehouseId]=@WarehouseId
           ,[DateInventory]=@DateInventory
           ,[IsFinish]  =@IsFinish      
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           WHERE Id=@Id AND ISNULL(IsDelete,0)=0;
           
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =@Id ;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end
GO
/****** Object:  StoredProcedure [dbo].[InventoryDetail_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[InventoryDetail_Delete] 
 @InventoryId  numeric(18, 0)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
UPDATE [InventoryDetail]
SET IsDelete=1
	,ModifiedBy=@ModifiedBy
	,ModifiedDate=GETDATE()
	where InventoryId=@InventoryId AND ISNULL(IsDelete,0)=0
			  
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[InventoryDetail_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[InventoryDetail_Insert] 
 @InventoryId  numeric(18, 0)
,@MaterialId numeric(18, 0)
,@Quantity  numeric(18, 3)
,@CreateBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
INSERT INTO [InventoryDetail]
           (
               [InventoryId]
			  ,[MaterialId]
			  ,[Quantity]
			  ,[CreateBy]
			  ,[CreateDate]
           )
     VALUES
           (   @InventoryId
			  ,@MaterialId
			  ,@Quantity
			  ,@CreateBy
			  ,GETDATE()
			)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =SCOPE_IDENTITY();
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[Material_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[Material_Delete] 
			@Id numeric(18, 0) ,			
			@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
Update [Material]
SET         IsDelete=1
           ,ModifiedBy=@ModifiedBy
           ,ModifiedDate=GETDATE()
           Where Id=@Id
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[Material_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Material_GetAll]
@MaterialCode  varchar(10) ,
@MaterialName  nvarchar(250)
AS
Begin
SELECT m.Id,
0 as STATUS,
m.MaterialCode,
m.MaterialName,
m.Price,
m.UnitId,
m.SupplierId,
m.VAT,
m.Remark,
u.UnitName,
p.SupplierName,
m.GroupMaterialId,
G.GroupMaterialName
FROM dbo.Material m
LEFT JOIN dbo.Unit u
ON (u.Id=m.UnitId AND ISNULL(u.IsDelete,0)=0)
LEFT JOIN dbo.Supplier p
ON (p.Id=m.SupplierId AND ISNULL(p.IsDelete,0)=0)
LEFT join GroupMaterial G
ON (G.Id=M.GroupMaterialId AND ISNULL(G.ISDELETE,0)=0)
WHERE ISNULL(m.IsDelete,0)=0
AND (@MaterialCode is null OR m.MaterialCode Like '%' +@MaterialCode +'%')
AND (@MaterialName is null OR m.MaterialName Like '%'+ @MaterialName +'%')
ORder by g.GroupMaterialName;
End
GO
/****** Object:  StoredProcedure [dbo].[Material_GetAllByFood]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Material_GetAllByFood]
@foodId  varchar(10)
AS
Begin
SELECT m.Id,
CASE  WHEN fd.FoodId>0		   THEN 1  ELSE 0 END AS STATUS,
m.MaterialCode,
m.MaterialName,
u.UnitName,
'' as Remark
FROM dbo.Material m
LEFT JOIN dbo.Unit u
ON (u.Id=m.UnitId AND ISNULL(u.IsDelete,0)=0)
Left join FoodDetail fd
on (fd.FoodId=@foodId ANd fd.MaterialId=m.Id AND Isnull(fd.IsDelete,0)=0)
WHERE ISNULL(m.IsDelete,0)=0;
End
GO
/****** Object:  StoredProcedure [dbo].[Material_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Material_Insert]
			@MaterialCode  varchar(10) ,
            @MaterialName  nvarchar(250) ,
            @UnitId  numeric(18, 0) ,
            @SupplierId  numeric(18, 0) ,
            @VAT  numeric(18, 0) ,
            @Price  numeric(18, 3) ,
            @Remark  varchar(500) ,
            @CreateBy  varchar(50),
            @GroupMaterialId numeric(18, 0) 
AS
Begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = N'MaterialCode',
		@tableName = N'Material',
		@strCode = @strCode OUTPUT
INSERT INTO [Material]
           (
            [MaterialCode]
           ,[MaterialName]
           ,[UnitId]
           ,[SupplierId]
           ,[VAT]
           ,[Price]
           ,[Remark]
           ,[CreateBy]
           ,[CreateDate]
           ,[GroupMaterialId])
     VALUES
           (@strCode  ,
            @MaterialName  ,
            @UnitId  		,
            @SupplierId 	,
            @VAT  			,
            @Price  		, 
            @Remark  		,
            @CreateBy  		,
           GETDATE(),
           @GroupMaterialId)
           IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
   End
GO
/****** Object:  StoredProcedure [dbo].[Material_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Material_Update] 
			@Id numeric(18, 0) ,
			@MaterialCode  varchar(10) ,
            @MaterialName  nvarchar(250) ,
            @UnitId  numeric(18, 0) ,
            @SupplierId  numeric(18, 0) ,
            @VAT  numeric(18, 0) ,
            @Price  numeric(18, 3) ,
            @Remark  varchar(500) ,
			@ModifiedBy varchar(50),
			@GroupMaterialId numeric(18, 0)  
AS
begin
declare @p_count numeric(18, 0)=0;
Update [Material]
SET          MaterialCode=@MaterialCode
			,MaterialName=@MaterialName
			,UnitId=@UnitId
			,SupplierId=@SupplierId
			,VAT=@VAT
			,Price=@Price
			,Remark=@Remark
           ,ModifiedBy=@ModifiedBy
           ,ModifiedDate=GETDATE()
           ,GroupMaterialId=@GroupMaterialId
           Where Id=@Id
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[MaterialsFifo_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[MaterialsFifo_GetAll]
@WarehouseId numeric(18,0)
as
Begin
SELECT [Id]
	  ,(ISNULL(QuantityReceived,0)-ISNULL(QuantityDelivery,0)) as Quantity
      ,[MaterialId]     
      ,[PriceReceived] as Price
      ,ROW_NUMBER() OVER(ORDER BY DateFifo ASC) as OrderNo
  FROM  [MaterialsFifo]
  WHERE (ISNULL(QuantityReceived,0)-ISNULL(QuantityDelivery,0))>0
  AND WarehouseId=@WarehouseId
END
GO
/****** Object:  StoredProcedure [dbo].[MaterialsFifo_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC [dbo].[MaterialsFifo_Insert]
 @ImportWarehousingMaterialsId numeric(18,0)
,@DateFifo DateTime
,@MaterialId numeric(18,0)
,@QuantityReceived numeric(18,4)
,@PriceReceived numeric(18,4)
,@CreateBy varchar(50)
,@WarehouseId numeric(18,0)
as
Begin
declare @p_count numeric(18, 0)=0;
INSERT INTO [MaterialsFifo]
           (
             ImportWarehousingMaterialsId
            ,DateFifo 
			,MaterialId
			,QuantityReceived
			,PriceReceived
			,CreateBy
			,CreateDate
			,WarehouseId
           )
     VALUES
           ( 
			@ImportWarehousingMaterialsId
			,@DateFifo 
			,@MaterialId
			,@QuantityReceived
			,@PriceReceived
			,@CreateBy
			,GETDATE()
			,@WarehouseId)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =@@IDENTITY ;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
		select @p_count; 
END;
GO
/****** Object:  StoredProcedure [dbo].[MaterialsFifo_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC [dbo].[MaterialsFifo_Update]
 @Id numeric(18,0)
,@MaterialId numeric(18,0)
,@QuantityDelivery numeric(18,4)
,@ModifiedBy varchar(50)
,@ListIdReceived varchar(max)
,@WarehouseId numeric(18,0)
as
Begin
declare @p_count numeric(18, 0)=0;
UPDATE [MaterialsFifo]
SET	   MaterialId=@MaterialId
			,QuantityDelivery=ISNULL(QuantityDelivery,0)+@QuantityDelivery
			,ModifiedBy=@ModifiedBy
			,ModifiedDate=GETDATE()
			,ListIdReceived=ISNULL(ListIdReceived,'')+@ListIdReceived
     WHERE  Id=@Id AND (ISNULL(QuantityReceived,0)-ISNULL(QuantityDelivery,0))>0   
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1 ;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
		select @p_count;
END;
GO
/****** Object:  StoredProcedure [dbo].[QuantitativeFood_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[QuantitativeFood_GetAll] 
 @FoodId  numeric(18, 0)
,@FoodTypesId numeric(18, 0)
,@SchoolId numeric(18, 0)
AS
begin
	SELECT 
	 f.Id AS FoodId 
	,f.FoodCode 
	,f.FoodName 
	,(f.FoodName+'('+f.FoodCode+')') as FullNameFood  
	,m.Id AS MaterialId 
	,m.MaterialCode 
	,m.MaterialName 
	,Q.QuantitativeOrther 
	,Q.QuantitativeOne 
	,Q.QuantitativeTwo 
	,u.UnitName
	,Q.Price
	,Q.SchoolId
	,null as Remark
	,Q.Id as QuantitativeFoodId
	FROM Food f 
	INNER JOIN FoodDetail fd 
	ON (f.Id=fd.FoodId AND IsNull(fd.IsDelete,0)=0) 
	INNER JOIN Material m 
	ON (m.Id=fd.MaterialId AND IsNull(m.IsDelete,0)=0) 
	LEFT JOIN  dbo.Unit u
	ON (m.UnitId=u.Id AND IsNull(u.IsDelete,0)=0)
	LEFT JOIN QuantitativeFood q 
	ON (q.FoodId=f.Id AND q.MaterialId=m.Id AND IsNull(q.IsDelete,0)=0 AND (Q.SchoolId=@SchoolId)) 
	WHERE IsNull(f.IsDelete,0)=0
	AND (@FoodId=0 OR f.Id=@FoodId) 
	AND (@FoodTypesId=0 OR f.FoodTypeId=@FoodTypesId)
	 
end;
GO
/****** Object:  StoredProcedure [dbo].[QuantitativeFood_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[QuantitativeFood_Insert] 
 @FoodId  numeric(18, 0)
,@MaterialId numeric(18, 0)
,@QuantitativeOne numeric(18, 4)
,@QuantitativeTwo numeric(18, 4)
,@QuantitativeOrther numeric(18, 4)
,@Remark Nvarchar(250)
,@CreateBy varchar(50)
,@Price numeric(18, 4)
,@SchoolId numeric(18, 4)
AS
begin
declare @p_count numeric(18, 0)=0;
INSERT INTO [QuantitativeFood]
           (
            [FoodId]
           ,[MaterialId]
           ,[QuantitativeOne]
           ,[QuantitativeTwo]
           ,[QuantitativeOrther]
           ,[Remark]
           ,[CreateBy]
           ,[CreateDate]
           ,[Price]
           ,[SchoolId]
           )
     VALUES
           ( @FoodId
			,@MaterialId
			,@QuantitativeOne
			,@QuantitativeTwo
			,@QuantitativeOrther
			,@Remark
			,@CreateBy
			,GETDATE()
			,@Price
			,@SchoolId
			)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[QuantitativeFood_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[QuantitativeFood_Update] 
 @QuantitativeFoodId  numeric(18, 0)
,@FoodId  numeric(18, 0)
,@MaterialId numeric(18, 0)
,@QuantitativeOne numeric(18, 4)
,@QuantitativeTwo numeric(18, 4)
,@QuantitativeOrther numeric(18, 4)
,@Remark Nvarchar(250)
,@ModifiedBy varchar(50)
,@Price numeric(18, 4)
,@SchoolId numeric(18, 0)
AS
begin
declare @p_count numeric(18, 0)=0;
UPDATE [QuantitativeFood]
SET     FoodId=@FoodId
			,MaterialId=@MaterialId
			,QuantitativeOne=@QuantitativeOne
			,QuantitativeTwo=@QuantitativeTwo
			,QuantitativeOrther=@QuantitativeOrther
			,Remark=@Remark
			,ModifiedBy=@ModifiedBy
			,ModifiedDate=GETDATE()
			,Price=@Price
			,[SchoolId]=@SchoolId
			where Id=@QuantitativeFoodId;
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[ReportDeliveryVegetablesQuatitative_GetBySchoolId]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ReportDeliveryVegetablesQuatitative_GetBySchoolId]
 @Date  Date
,@SchoolsId numeric(18,0)
As
Begin
SELECT
 ROW_NUMBER() OVER(ORDER BY MaterialName ASC) as OrderNo,
 (CASE DATEPART(WEEKDAY,fmd.DateMenu)  
    WHEN 1 THEN N'Chủ  nhật' 
    WHEN 2 THEN N'Thứ Hai' 
    WHEN 3 THEN N'Thứ Ba' 
    WHEN 4 THEN N'Thứ Tư' 
    WHEN 5 THEN N'Thứ Năm' 
    WHEN 6 THEN N'Thứ Sáu' 
    WHEN 7 THEN N'Thứ Bảy' END) + ' '+CONVERT(VarChar(50), fmd.DateMenu, 103) as DateMenu
    ,fmd.NumberFoodStudent
    ,fmd.NumberFoodTeacher
    ,fmd.FoodId
    ,fd.MaterialId
    ,f.FoodName
    ,f.FoodCode
    ,m.MaterialCode
    ,m.MaterialName
    ,0 as QuantityPurchased
    ,U.UnitCode
    ,U.UnitName
    ,((isnull(fmd.NumberFoodStudent,0)+isnull(fmd.NumberFoodTeacher,0))*(Isnull(Q.QuantitativeOne,0)+Isnull(Q.QuantitativeOrther,0)+Isnull(Q.QuantitativeTwo,0))) as Quantitative
    ,Isnull(Q.Price,0) as Price
FROM dbo.FoodMenu_Detail fmd
inner join dbo.FoodMenu fm
on (fmd.FoodMenuId=fm.Id AND fm.SchoolsId=@SchoolsId AND ISNULL(fm.isDelete,0)=0) 
inner join dbo.Food f
on (f.Id=fmd.FoodId AND ISNULL(f.IsDelete,0)=0)
inner join dbo.FoodDetail fd
on (f.Id=fd.FoodId AND ISNULL(fd.IsDelete,0)=0)
inner join dbo.Material m
on (m.Id=fd.MaterialId AND IsNULL(m.IsDelete,0)=0 AND m.GroupMaterialId=1)
Left join dbo.QuantitativeFood Q
on (Q.MaterialId=m.Id AND Q.FoodId=f.Id ANd ISNULL(Q.IsDelete,0)=0 AND Q.SchoolId=@SchoolsId)
Left join dbo.Unit u
ON (U.Id=m.UnitId AND ISNULL(u.IsDelete,0)=0)
where IsNull(fmd.IsDelete,0)=0
AND (fmd.DateMenu =@Date)
AND (Isnull(Q.QuantitativeOne,0)+Isnull(Q.QuantitativeOrther,0)+Isnull(Q.QuantitativeTwo,0)>0)
order by fmd.DateMenu;                    
end;
GO
/****** Object:  StoredProcedure [dbo].[ReportExpenditure]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ReportExpenditure]
@FromDate Date,
@ToDate Date,
@SchoolsId numeric(18,0)
as
begin
select T.SessionName,T.SchoolsId,T.TongTienChi,
 CONVERT(VarChar(50), T.DateMenu, 103) as DateMenu,
(isnull([NumberFoodStudentReality],0)*isnull([PriceStudentReality],0)+isnull([NumberFoodTeacherReality],0)*isnull([PriceTeacherReality],0))as TongTienThu 
 from (
select fm.SchoolsId,
fd.DateMenu ,

(Case When Isnull(FT.IsSession,0) =1 then N'Buổi sáng'
	 When Isnull(FT.IsSession,0) =2 Then N'Buổi trưa'
	 When Isnull(FT.IsSession,0) =3 Then N'Buổi xế'
	 When Isnull(FT.IsSession,0) =4 Then N'Buổi chiều' ENd) as SessionName,
sum(Isnull(wmd.PriceExport,0) * isnull(wmd.Quantity,0)) as TongTienChi

from dbo.FoodMenu Fm
inner join dbo.FoodMenu_Detail Fd
ON (fm.Id=fd.FoodMenuId AND ISNULL(fd.IsDelete,0)=0)
inner join dbo.WarehousingMaterials wm
on (wm.ImplementationDates=fd.DateMenu  ANd wm.SchoolId=fm.SchoolsId AND Isnull(wm.IsImport,0)=0 AND ISNULL(wm.IsDelete,0)=0 AND isnull(wm.IsConfirm,0)=1)
inner join dbo.WarehousingMaterialsDetail wmd
on (wmd.WarehousingMaterialsId=wm.Id and wmd.FoodId=fd.FoodId and isnull(wmd.IsDelete,0)=0)
Inner join Food F
ON(F.Id=fd.FoodId AND Isnull(F.IsDelete,0)=0)
Inner join dbo.FoodTypes FT
ON (FT.Id=F.FoodTypeId AND ISNULL(Ft.IsDelete,0)=0)
where @FromDate<=fd.DateMenu ANd fd.DateMenu<=@ToDate
 and (@SchoolsId=0 OR fm.SchoolsId=@SchoolsId)  and isnull(fm.IsDelete,0)=0
group by fm.SchoolsId,
fd.DateMenu,
FT.IsSession
)T
inner join dbo.FoodReality fr
on (fr.DateFoodReality=T.DateMenu and fr.SchoolId=T.SchoolsId and isnull(fr.IsConfirm,0)=1)
end;
GO
/****** Object:  StoredProcedure [dbo].[ReportMaterialWeek_GetBySchoolId]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ReportMaterialWeek_GetBySchoolId]
@FromDay  Date
,@ToDay Date
,@SchoolsId numeric(18,0)
As
Begin
SELECT CASE DATEPART(WEEKDAY,fmd.DateMenu)  
    WHEN 1 THEN N'Chủ  nhật' 
    WHEN 2 THEN N'Thứ Hai' 
    WHEN 3 THEN N'Thứ Ba' 
    WHEN 4 THEN N'Thứ Tư' 
    WHEN 5 THEN N'Thứ Năm' 
    WHEN 6 THEN N'Thứ Sáu' 
    WHEN 7 THEN N'Thứ Bảy' END As WEEKNAMEDAY
    ,CONVERT(VarChar(50), fmd.DateMenu, 103) as DateMenu
    ,fmd.NumberFoodStudent
    ,fmd.NumberFoodTeacher
    ,fmd.FoodId
    ,fd.MaterialId
    ,f.FoodName
    ,f.FoodCode
    ,m.MaterialCode
    ,m.MaterialName
    ,0 as Loss
    ,U.UnitCode
    ,U.UnitName
    ,Isnull(Q.QuantitativeOne,0)+Isnull(Q.QuantitativeOrther,0)+Isnull(Q.QuantitativeTwo,0) as Quantitative
    ,Isnull(Q.Price,0) as Price
FROM dbo.FoodMenu_Detail fmd
inner join dbo.FoodMenu fm
on (fmd.FoodMenuId=fm.Id AND fm.SchoolsId=@SchoolsId AND ISNULL(fm.isDelete,0)=0) 
inner join dbo.Food f
on (f.Id=fmd.FoodId AND ISNULL(f.IsDelete,0)=0)
inner join dbo.FoodDetail fd
on (f.Id=fd.FoodId AND ISNULL(fd.IsDelete,0)=0)
inner join dbo.Material m
on (m.Id=fd.MaterialId AND IsNULL(m.IsDelete,0)=0)
Left join dbo.QuantitativeFood Q
on (Q.MaterialId=m.Id AND Q.FoodId=f.Id ANd ISNULL(Q.IsDelete,0)=0 AND Q.SchoolId=@SchoolsId)
Left join dbo.Unit u
ON (U.Id=m.UnitId AND ISNULL(u.IsDelete,0)=0)
where IsNull(fmd.IsDelete,0)=0
AND (fm.FromDate >=@FromDay)
AND (fm.ToDate <=@ToDay)
AND (Isnull(Q.QuantitativeOne,0)+Isnull(Q.QuantitativeOrther,0)+Isnull(Q.QuantitativeTwo,0)>0)
order by fmd.DateMenu;                    
end;
GO
/****** Object:  StoredProcedure [dbo].[ReportVegetablesQuatitative_GetBySchoolId]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ReportVegetablesQuatitative_GetBySchoolId]
 @FromDay  Date
,@ToDay Date
,@SchoolsId numeric(18,0)
As
Begin
SELECT
 (CASE DATEPART(WEEKDAY,fmd.DateMenu)  
    WHEN 1 THEN N'Chủ  nhật' 
    WHEN 2 THEN N'Thứ Hai' 
    WHEN 3 THEN N'Thứ Ba' 
    WHEN 4 THEN N'Thứ Tư' 
    WHEN 5 THEN N'Thứ Năm' 
    WHEN 6 THEN N'Thứ Sáu' 
    WHEN 7 THEN N'Thứ Bảy' END) + ' '+CONVERT(VarChar(50), fmd.DateMenu, 103) as DateMenu
    ,fmd.NumberFoodStudent
    ,fmd.NumberFoodTeacher
    ,fmd.FoodId
    ,fd.MaterialId
    ,f.FoodName
    ,f.FoodCode
    ,m.MaterialCode
    ,m.MaterialName
    ,0 as QuantityPurchased
    ,U.UnitCode
    ,U.UnitName
    ,((isnull(fmd.NumberFoodStudent,0)+isnull(fmd.NumberFoodTeacher,0))*(Isnull(Q.QuantitativeOne,0)+Isnull(Q.QuantitativeOrther,0)+Isnull(Q.QuantitativeTwo,0))) as Quantitative
    ,Isnull(Q.Price,0) as Price
FROM dbo.FoodMenu_Detail fmd
inner join dbo.FoodMenu fm
on (fmd.FoodMenuId=fm.Id AND fm.SchoolsId=@SchoolsId AND ISNULL(fm.isDelete,0)=0) 
inner join dbo.Food f
on (f.Id=fmd.FoodId AND ISNULL(f.IsDelete,0)=0)
inner join dbo.FoodDetail fd
on (f.Id=fd.FoodId AND ISNULL(fd.IsDelete,0)=0)
inner join dbo.Material m
on (m.Id=fd.MaterialId AND IsNULL(m.IsDelete,0)=0 AND m.GroupMaterialId=1)
Left join dbo.QuantitativeFood Q
on (Q.MaterialId=m.Id AND Q.FoodId=f.Id ANd ISNULL(Q.IsDelete,0)=0 AND Q.SchoolId=@SchoolsId)
Left join dbo.Unit u
ON (U.Id=m.UnitId AND ISNULL(u.IsDelete,0)=0)
where IsNull(fmd.IsDelete,0)=0
AND (fm.FromDate >=@FromDay)
AND (fm.ToDate <=@ToDay)
AND (Isnull(Q.QuantitativeOne,0)+Isnull(Q.QuantitativeOrther,0)+Isnull(Q.QuantitativeTwo,0)>0)
order by fmd.DateMenu;                    
end;
GO
/****** Object:  StoredProcedure [dbo].[S_Log_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE proc [dbo].[S_Log_Insert]
 @UserName varchar(50)
,@LogDate datetime
,@LogAction nvarchar(max)
,@LogLevel varchar(100)
,@Logger varchar(100)
,@FormName varchar(100)
,@LogContent nvarchar(max)
,@Exception nvarchar(max)
AS
BEGIN
declare @p_count numeric(18, 0)=0;
INSERT INTO [S_Logs]
           ([UserName]
           ,[LogDate]
           ,[LogAction]
           ,[LogLevel]
           ,[Logger]
           ,[FormName]
           ,[LogContent]
           ,[Exception])
     VALUES
           ( @UserName 		
			,@LogDate 		
			,@LogAction 	
			,@LogLevel 		
			,@Logger 		
			,@FormName 		
			,@LogContent 	
			,@Exception)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
END;
GO
/****** Object:  StoredProcedure [dbo].[School_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[School_Delete]
            @Id numeric(18,0) 
           ,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
   UPDATE [dbo].[Schools]
   SET     [IsDelete]=1
		  ,[ModifiedBy] = @ModifiedBy
		  ,[ModifiedDate] = GetDate()
	 WHERE Id=@Id;
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[School_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[School_Update]
            @Id numeric(18,0) 
		   ,@SchoolsCode varchar(10)
           ,@SchoolsName nvarchar(500)
           ,@Address nvarchar(500)
           ,@Phone varchar(50)
           ,@Fax varchar(50)
           ,@Email varchar(100)
           ,@Representative nvarchar(500)
           ,@NumberStudentPrimary numeric(18,0)
           ,@NumberStudentJuniorHigh numeric(18,0)
           ,@NumberStudentHighSchool numeric(18,0)
           ,@NumberTeacher numeric(18,0)          
           ,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
   UPDATE [dbo].[Schools]
   SET [SchoolsCode] = @SchoolsCode
		  ,[SchoolsName] = @SchoolsName
		  ,[Address] = @Address
		  ,[Phone] = @Phone
		  ,[Fax] = @Fax
		  ,[Email] = @Email
		  ,[Representative] = @Representative
		  ,[NumberStudentPrimary] = @NumberStudentPrimary
		  ,[NumberStudentJuniorHigh] = @NumberStudentJuniorHigh
		  ,[NumberStudentHighSchool] = @NumberStudentHighSchool
		  ,[NumberTeacher] = @NumberTeacher
		  ,[ModifiedBy] = @ModifiedBy
		  ,[ModifiedDate] = GetDate()
	 WHERE Id=@Id AND ISNULL(IsDelete,0)=0;
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[Schools_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Schools_GetAll]
as
begin
SELECT [Id]
      ,[SchoolsCode]
      ,[SchoolsName]
      ,[Address]
      ,[Phone]
      ,[Fax]
      ,[Email]
      ,[Representative]
      ,[NumberStudentPrimary]
      ,[NumberStudentJuniorHigh]
      ,[NumberStudentHighSchool]
      ,[NumberTeacher]
      ,[RowVersion]
      ,[IsDelete]
      ,[CreateBy]
      ,[CreateDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[Schools]
  where isnull(IsDelete,0)=0
  Order by SchoolsName
End
GO
/****** Object:  StoredProcedure [dbo].[Schools_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Schools_Insert]
			@SchoolsCode varchar(10)
           ,@SchoolsName nvarchar(500)
           ,@Address nvarchar(500)
           ,@Phone varchar(50)
           ,@Fax varchar(50)
           ,@Email varchar(100)
           ,@Representative nvarchar(500)
           ,@NumberStudentPrimary numeric(18,0)
           ,@NumberStudentJuniorHigh numeric(18,0)
           ,@NumberStudentHighSchool numeric(18,0)
           ,@NumberTeacher numeric(18,0)          
           ,@CreateBy varchar(50)
as
Begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = N'SchoolsCode',
		@tableName = N'Schools',
		@strCode = @strCode OUTPUT
INSERT INTO [Schools]
           (
			   [SchoolsCode]
			  ,[SchoolsName]
			  ,[Address]
			  ,[Phone]
			  ,[Fax]
			  ,[Email]
			  ,[Representative]
			  ,[NumberStudentPrimary]
			  ,[NumberStudentJuniorHigh]
			  ,[NumberStudentHighSchool]
			  ,[NumberTeacher]
			  ,[CreateBy]
			  ,[CreateDate]											
           )
     VALUES
           (
               @strCode
			  ,@SchoolsName
			  ,@Address
			  ,@Phone
			  ,@Fax
			  ,@Email
			  ,@Representative
			  ,@NumberStudentPrimary
			  ,@NumberStudentJuniorHigh
			  ,@NumberStudentHighSchool
			  ,@NumberTeacher
			  ,@CreateBy
			  ,GETDATE())
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =SCOPE_IDENTITY();
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end
GO
/****** Object:  StoredProcedure [dbo].[Supplier_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Supplier_Delete]
 @Id numeric(18,0) 
,@ModifiedBy varchar(50)
AS
BEGIN
declare @p_count numeric(18, 0)=0;
UPDATE [Supplier]
  SET IsDelete=1
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate]=GETDATE()
      
 WHERE Id=@Id AND ISNULL(IsDelete,0)=0;
 IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
END
GO
/****** Object:  StoredProcedure [dbo].[Supplier_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Supplier_GetAll]
AS
begin
SELECT [Id]
      ,[SupplierCode]
      ,[SupplierName]
      ,[IsDelete]
      ,[CreateBy]
      ,[CreateDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [Supplier]
  WHERE ISNULL(IsDelete,0)=0;
  end;
GO
/****** Object:  StoredProcedure [dbo].[Supplier_GetAllList]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Supplier_GetAllList]
@SupplierCode  [varchar](50),
@SupplierName [nvarchar](500)
as
begin
SELECT [Id]
      ,[SupplierCode]
      ,[SupplierName]
      ,[Address]
      ,[Phone]
      ,[Fax]
      ,[Email]
      ,[Representative]
      ,[TaxCode]
      ,[IsDelete]
      ,[CreateBy]
      ,[CreateDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[Supplier]
  WHERE ISNULL(IsDelete,0)=0
  AND (@SupplierCode is null OR SupplierCode=@SupplierCode)
  AND (@SupplierName is null OR SupplierName=@SupplierName)
END;
GO
/****** Object:  StoredProcedure [dbo].[Supplier_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Supplier_Insert]
 @SupplierCode varchar(50)
,@SupplierName nvarchar(500)
,@Address nvarchar(500)
,@Phone varchar(50)
,@Fax varchar(50)
,@Email varchar(100)
,@Representative nvarchar(500)
,@TaxCode nvarchar(50)
,@CreateBy varchar(50)
as
Begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = N'SupplierCode',
		@tableName = N'Supplier',
		@strCode = @strCode OUTPUT
INSERT INTO [Supplier]
           ([SupplierCode]
           ,[SupplierName]
           ,[Address]
           ,[Phone]
           ,[Fax]
           ,[Email]
           ,[Representative]
           ,[TaxCode]
                      ,[CreateBy]
           ,[CreateDate])
     VALUES
           ( @strCode
			,@SupplierName 
			,@Address 
			,@Phone
			,@Fax
			,@Email
			,@Representative
			,@TaxCode
			,@CreateBy
			,GETDATE())
           IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =SCOPE_IDENTITY();
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
END
GO
/****** Object:  StoredProcedure [dbo].[Supplier_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Supplier_Update]
 @Id numeric(18,0) 
,@SupplierCode varchar(50)
,@SupplierName nvarchar(500)
,@Address nvarchar(500)
,@Phone varchar(50)
,@Fax varchar(50)
,@Email varchar(100)
,@Representative nvarchar(500)
,@TaxCode nvarchar(50)
,@ModifiedBy varchar(50)
AS
BEGIN
declare @p_count numeric(18, 0)=0;
UPDATE [Supplier]
  SET [SupplierCode] = @SupplierCode
      ,[SupplierName] = @SupplierName
      ,[Address] = @Address
      ,[Phone] = @Phone
      ,[Fax] = @Fax
      ,[Email] = @Email
      ,[Representative] = @Representative
      ,[TaxCode] = @TaxCode
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate]=GETDATE()
 WHERE Id=@Id AND ISNULL(IsDelete,0)=0;
 IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
END
GO
/****** Object:  StoredProcedure [dbo].[U_MENU_DELETE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_MENU_DELETE] @ID int AS BEGIN BEGIN TRY UPDATE U_Menu 
SET
  IsDelete = 1 
WHERE
  ID = @ID 
SELECT
  1 END TRY BEGIN CATCH 
SELECT
  0 END CATCH END
GO
/****** Object:  StoredProcedure [dbo].[U_MENU_GETALL]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[U_MENU_GETALL] AS BEGIN 
SELECT
  ID
  , MenuCode
  , MenuName
  , MenuNamespaceClass
  , MenuClassName
  , MenuRemark
  , MenuIcon
  , MenuParentID
  , ParentID = ( 
    select
      MenuName 
    from
      U_Menu 
    where
      ID = m.MenuParentID
  ) 
FROM
  U_Menu m 
WHERE
  IsDelete = 0 END
GO
/****** Object:  StoredProcedure [dbo].[U_MENU_GETALL_MENU_PARENT]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[U_MENU_GETALL_MENU_PARENT] AS BEGIN 
SELECT
  MenuName
  , ID as MenuParentID 
FROM
  U_Menu 
WHERE
  IsDelete = 0 
  And MenuParentID = 0; 

END
GO
/****** Object:  StoredProcedure [dbo].[U_MENU_INSERT]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_MENU_INSERT] @MenuCode nvarchar(50)
, @MenuName nvarchar(500)
, @MenuNamespaceClass nvarchar(500)
, @MenuClassName nvarchar(500)
, @MenuRemark nvarchar(500)
, @MenuIcon nvarchar(500)
, @MenuParentID nvarchar(500) AS BEGIN BEGIN TRY 
INSERT 
INTO U_MENU( 
  MENUCODE
  , MENUNAME
  , MENUNAMESPACECLASS
  , MENUCLASSNAME
  , MENUREMARK
  , MENUICON
  , MENUPARENTID
) 
values ( 
  @MenuCode
  , @MenuName
  , @MenuNamespaceClass
  , @MenuClassName
  , @MenuRemark
  , @MenuIcon
  , @MenuParentID
) 
SELECT
  1 END TRY BEGIN CATCH 
SELECT
  0 END CATCH END
GO
/****** Object:  StoredProcedure [dbo].[U_MENU_UPDATE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_MENU_UPDATE] @MenuCode nvarchar(50)
, @MenuName nvarchar(500)
, @MenuNamespaceClass nvarchar(500)
, @MenuClassName nvarchar(500)
, @MenuRemark nvarchar(500)
, @MenuIcon nvarchar(500)
, @MenuParentID nvarchar(500)
, @ID int AS BEGIN BEGIN TRY UPDATE U_Menu 
SET
  MENUCODE = @MenuCode
  , MENUNAME = @MenuName
  , MENUNAMESPACECLASS = @MenuNamespaceClass
  , MENUCLASSNAME = @MenuClassName
  , MENUREMARK = @MenuRemark
  , MENUICON = @MenuIcon
  , MENUPARENTID = @MenuParentID 
WHERE
  ID = @ID 
SELECT
  1 END TRY BEGIN CATCH 
SELECT
  0 END CATCH END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_CHANGEPASSWORD]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USER_CHANGEPASSWORD] ( 
  @ID INT
  , @Passwordold NVARCHAR(MAX)
  , @Passwordnew nvarchar(max)
) AS BEGIN BEGIN TRY if exists ( 
  select
    * 
  from
    U_User 
  where
    Password = @Passwordold 
    and ID = @ID
) begin UPDATE U_User 
SET
  Password = @Passwordnew 
WHERE
  ID = @ID 
select
  1 end else 
select
  0 END TRY BEGIN CATCH 
SELECT
  0 END CATCH END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_CHECK_USERNAME]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[U_USER_CHECK_USERNAME] (@UserName nvarchar(50)) AS BEGIN IF ( 
  ( 
    SELECT
      COUNT(UserName) 
    FROM
      U_User 
    WHERE
      UserName = @UserName 
      AND (IsDelete IS NULL OR IsDelete = 0)
  ) > 0
) 
select
  1 ELSE 
select
  0 END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_DELETE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[U_USER_DELETE] (@ID INT) AS BEGIN UPDATE U_User 
SET
  IsDelete = 1 
where
  ID = @ID END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_DEPARTMENT_DELETE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USER_DEPARTMENT_DELETE] (@USER_DEPARTMENT_ID int) AS BEGIN BEGIN TRY UPDATE
 U_USER_DEPARTMENT 
SET
  IS_DELETE = 1 
WHERE
  [USER_DEPARTMENT_ID] = @USER_DEPARTMENT_ID 
select
  1; 

END TRY BEGIN CATCH 
select
  0; 

END CATCH END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_DEPARTMENT_GET_LIST_ALL]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USER_DEPARTMENT_GET_LIST_ALL] AS BEGIN 
SELECT
  * 
FROM
  U_USER_DEPARTMENT END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_DEPARTMENT_INSERT]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USER_DEPARTMENT_INSERT] ( 
  @USER_DEPARTMENT_ID int
  , @USER_DEPARTMENT_NAME nvarchar(256)
  , @USER_DEPARTMENT_DESCRIPTION nvarchar(1000)
  , @IS_DEFAULT bit
  , @IS_DELETE bit
  , @IS_FULL_SALARY bit
  , @ALTERD_BY int
  , @MODIFIED_BY int
) AS BEGIN BEGIN TRY 
INSERT 
INTO U_USER_DEPARTMENT( 
  [USER_DEPARTMENT_ID]
  , [USER_DEPARTMENT_NAME]
  , [USER_DEPARTMENT_DESCRIPTION]
  , [IS_DEFAULT]
  , [IS_DELETE]
  , [IS_FULL_SALARY]
  , [ALTERD_BY]
  , [MODIFIED_BY]
  , [ALTERD_DATE]
) 
VALUES ( 
  @USER_DEPARTMENT_ID
  , @USER_DEPARTMENT_NAME
  , @USER_DEPARTMENT_DESCRIPTION
  , @IS_DEFAULT
  , @IS_DELETE
  , @IS_FULL_SALARY
  , @ALTERD_BY
  , @MODIFIED_BY
  , GETDATE()
) 
select
  IDENT_CURRENT('U_USER_DEPARTMENT'); 

END TRY BEGIN CATCH 
select
  0; 

END CATCH END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_DEPARTMENT_UPDATE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USER_DEPARTMENT_UPDATE] ( 
  @USER_DEPARTMENT_ID int
  , @USER_DEPARTMENT_NAME nvarchar(256)
  , @USER_DEPARTMENT_DESCRIPTION nvarchar(1000)
  , @IS_DEFAULT bit
  , @IS_DELETE bit
  , @IS_FULL_SALARY bit
  , @ALTERD_BY int
  , @MODIFIED_BY int
) AS BEGIN BEGIN TRY UPDATE U_USER_DEPARTMENT 
SET
  [USER_DEPARTMENT_NAME] = @USER_DEPARTMENT_NAME
  , [USER_DEPARTMENT_DESCRIPTION] = @USER_DEPARTMENT_DESCRIPTION
  , [IS_DEFAULT] = @IS_DEFAULT
  , [IS_DELETE] = @IS_DELETE
  , [IS_FULL_SALARY] = @IS_FULL_SALARY
  , [ALTERD_BY] = @ALTERD_BY
  , [MODIFIED_BY] = @MODIFIED_BY
  , [MODIFIED_DATE] = GETDATE() 
WHERE
  [USER_DEPARTMENT_ID] = @USER_DEPARTMENT_ID 
select
  1; 

END TRY BEGIN CATCH 
select
  0; 

END CATCH END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_GET_LIST]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USER_GET_LIST] AS BEGIN 
SELECT
  Row_number() over (order by U.UserName) STT
  , U.*
  , UT.UserTypeName UserTypeName
  , A.AreaName AreaName 
FROM
  U_User U 
  LEFT JOIN U_UserType UT 
    ON UT.ID = U.UserGroupID 
  LEFT JOIN C_Area A 
    ON A.ID = U.AreaID 
WHERE
  U.IsDelete = 0 END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_GET_PASSWORD]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[U_USER_GET_PASSWORD] (@ID int) AS BEGIN 
SELECT
  Password 
FROM
  U_User 
WHERE
  ID = @ID END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_INSERT]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[U_USER_INSERT] ( 
  @UserName nvarchar(50)
  , @FullName nvarchar(500)
  , @Password nvarchar(max)
  , @AreaID int
  , @TypeId int
) as begin 
insert 
into U_User( 
  UserName
  , FullName
  , Password
  , AreaID
  , UserGroupID
) 
values ( 
  @UserName
  , @FullName
  , @Password
  , @AreaID
  , @TypeId
) end
GO
/****** Object:  StoredProcedure [dbo].[U_USER_ROLE_CHANGE_ROLE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USER_ROLE_CHANGE_ROLE] (@UserTypeId INT, @MenuId INT, @Status INT) AS BEGIN
 IF EXISTS ( 
  SELECT
    * 
  FROM
    U_User_Role 
  WHERE
    UserTypeID = @UserTypeId 
    AND MenuID = @MenuId
) BEGIN 
SELECT
  0 IF @Status = 0 BEGIN 
DELETE U_User_Role 
WHERE
  UserTypeID = @UserTypeId 
  AND MenuID = @MenuId END END ELSE BEGIN 
SELECT
  011 IF @Status = 1 BEGIN 
SELECT
  012 
INSERT 
INTO U_User_Role(UserTypeID, MenuID) 
VALUES (@UserTypeId, @MenuId) END END 
SELECT
  1 END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_ROLE_GET_USERTYPE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USER_ROLE_GET_USERTYPE] AS BEGIN 
SELECT
  ID
  , UserTypeCode
  , UserTypeName 
FROM
  U_UserType 
WHERE
  IsDelete = 0 END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_ROLE_GETMENUCONDITION]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[U_USER_ROLE_GETMENUCONDITION](@USERTYPEID INT) AS BEGIN 
SELECT
  M.ID
  , M.MENUCODE
  , M.MENUNAME
  , CASE 
    WHEN UR.ID IS NULL 
      THEN 0 
    ELSE 1 
    END STATUS 
FROM
  U_Menu M 
  LEFT JOIN U_User_Role UR 
    ON M.ID = UR.MenuID 
    AND UR.UserTypeID = @USERTYPEID 
WHERE
  M.IsDelete = 0 END
GO
/****** Object:  StoredProcedure [dbo].[U_USER_UPDATE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[U_USER_UPDATE] ( 
  @ID INT
  , @UserName nvarchar(50)
  , @FullName nvarchar(500)
  , @Password nvarchar(max)
  , @AreaID int
  , @TypeId int
) AS BEGIN UPDATE U_User 
SET
  UserName = @UserName
  , FullName = @FullName
  , Password = @Password
  , AreaID = @AreaID
  , UserGroupID = @TypeId 
WHERE
  ID = @ID END
GO
/****** Object:  StoredProcedure [dbo].[U_USERTYPE_DELETE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[U_USERTYPE_DELETE]@Id int AS BEGIN UPDATE U_UserType 
SET
  IsDelete = 1 
WHERE
  ID = @Id END
GO
/****** Object:  StoredProcedure [dbo].[U_USERTYPE_GET_LIST]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USERTYPE_GET_LIST] AS BEGIN 
SELECT
  * 
FROM
  U_UserType 
WHERE
  IsDelete = 0 END
GO
/****** Object:  StoredProcedure [dbo].[U_USERTYPE_GET_LIST_FILTER]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[U_USERTYPE_GET_LIST_FILTER] AS BEGIN 
SELECT
  * 
FROM
  U_UserType 
where
  U_UserType.IsDelete = 1 END
GO
/****** Object:  StoredProcedure [dbo].[U_USERTYPE_INSERT]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[U_USERTYPE_INSERT]@UserTypeCode nvarchar(50)
, @UserTypeName nvarchar(500)
, @UserTypeGroup int
, @UserTypeDescription nvarchar(MAX) AS if exists ( 
  select
    UserTypeCode 
  from
    U_UserType 
  where
    UserTypeCode = @UserTypeCode 
    and IsDelete = 0
) BEGIN 
select
  0 END else BEGIN 
INSERT 
INTO U_UserType( 
  UserTypeCode
  , UserTypeName
  , UserTypeGroup
  , UserTypeDescription
) 
VALUES ( 
  @UserTypeCode
  , @UserTypeName
  , @UserTypeGroup
  , @UserTypeDescription
) 
select
  1 END
GO
/****** Object:  StoredProcedure [dbo].[U_USERTYPE_UPDATE]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[U_USERTYPE_UPDATE]@Id int
, @UserTypeCode nvarchar(50)
, @UserTypeName nvarchar(500)
, @UserTypeGroup int
, @UserTypeDescription nvarchar(MAX) AS if exists ( 
  select
    UserTypeCode
    , ID 
  from
    U_UserType 
  where
    UserTypeCode = @UserTypeCode 
    and IsDelete = 0 
    and ID = @Id
) BEGIN UPDATE U_UserType 
SET
  UserTypeCode = @UserTypeCode
  , UserTypeName = @UserTypeName
  , UserTypeGroup = @UserTypeGroup
  , UserTypeDescription = @UserTypeDescription 
WHERE
  ID = @Id 
select
  1 END else BEGIN if exists ( 
    select
      UserTypeCode 
    from
      U_UserType 
    where
      UserTypeCode = @UserTypeCode 
      and IsDelete = 0
  ) 
select
  0 else UPDATE U_UserType 
SET
  UserTypeName = @UserTypeName
  , UserTypeGroup = @UserTypeGroup
  , UserTypeDescription = @UserTypeDescription 
WHERE
  ID = @Id 
select
  1 END
GO
/****** Object:  StoredProcedure [dbo].[Unit_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[Unit_Delete] 
 @Id numeric(18, 0)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
Update [Unit]
SET [IsDelete]=1
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           Where Id=@Id
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[Unit_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Unit_GetAll]
AS
Begin
SELECT [Id]
      ,[UnitCode]
      ,[UnitName]
      ,[Remark]
      ,[RowVersion]
      ,[IsDelete]
      ,[CreateBy]
      ,[CreateDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [Unit]
  where ISNULL(IsDelete,0)=0
  end;
GO
/****** Object:  StoredProcedure [dbo].[Unit_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Unit_Insert] 
 @UnitCode  varchar(10)
,@UnitName nvarchar(300)
,@Remark nvarchar(500)
,@CreateBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
INSERT INTO [Unit]
           (
            [UnitCode]
           ,[UnitName]
           ,[Remark]
           ,[CreateBy]
           ,[CreateDate]
           )
     VALUES
           ( @UnitCode
			,@UnitName
			,@Remark
			,@CreateBy
			,GETDATE()
			)
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[Unit_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[Unit_Update] 
 @Id numeric(18, 0)
,@UnitCode  varchar(10)
,@UnitName nvarchar(300)
,@Remark nvarchar(500)
,@ModifiedBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
Update [Unit]
SET [UnitCode]=@UnitCode
           ,[UnitName]=@UnitName
           ,[Remark]=@Remark
           ,[ModifiedBy]=@ModifiedBy
           ,[ModifiedDate]=GETDATE()
           Where Id=@Id
			IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
end;
GO
/****** Object:  StoredProcedure [dbo].[Warehouse_Delete]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Warehouse_Delete]
 @Id numeric(18,0)
,@ModifiedBy varchar(50)
as 
begin
declare @p_count numeric(18, 0)=0;
UPDATE  [Warehouse]
   SET IsDelete=1
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate] = GETDATE()
 WHERE Id=@Id AND ISNULL(ISDELETE,0)=0;
 IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
 end;
GO
/****** Object:  StoredProcedure [dbo].[Warehouse_GetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Warehouse_GetAll]
as
begin
SELECT [Id]
      ,[WarehouseCode]
      ,[WarehouseName]
      ,[Address]
      ,[Phone]
      ,[Fax]
      ,[Email]
      ,[Representative]
      ,[Remark]
      ,[RowVersion]
      ,[IsDelete]
      ,[CreateBy]
      ,[CreateDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[Warehouse]
  where ISNULL(IsDelete,0)=0
end
GO
/****** Object:  StoredProcedure [dbo].[Warehouse_Insert]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create PROC [dbo].[Warehouse_Insert]
 @WarehouseCode varchar(10)
,@WarehouseName nvarchar(500)
,@Address nvarchar(500)
,@Phone varchar(50)
,@Fax varchar(50)
,@Email varchar(100)
,@Representative nvarchar(500)
,@Remark nvarchar(max)
,@CreateBy varchar(50)
AS
begin
declare @p_count numeric(18, 0)=0;
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = N'WarehouseCode',
		@tableName = N'Warehouse',
		@strCode = @strCode OUTPUT
INSERT INTO [Warehouse]
           ([WarehouseCode]
           ,[WarehouseName]
           ,[Address]
           ,[Phone]
           ,[Fax]
           ,[Email]
           ,[Representative]
           ,[Remark]
           ,[CreateBy]
           ,[CreateDate])
     VALUES
           ( @strCode	
 		,@WarehouseName 	
			,@Address 			
			,@Phone 			
			,@Fax 				
,@Email 			
,@Representative 	
,@Remark 			
,@CreateBy 	
,GetDate()
)		
IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =SCOPE_IDENTITY();
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
   end;
GO
/****** Object:  StoredProcedure [dbo].[Warehouse_ListGetAll]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Warehouse_ListGetAll]
@WarehouseCode varchar(10)
,@WarehouseName Nvarchar(500)
as
begin
SELECT [Id]
      ,[WarehouseCode]
      ,[WarehouseName]
      ,[Address]
      ,[Phone]
      ,[Fax]
      ,[Email]
      ,[Representative]
      ,[Remark]
  FROM  [Warehouse]
Where ISNULL(IsDelete,0)=0
AND (@WarehouseCode is null OR WarehouseCode=@WarehouseCode)
AND (@WarehouseName is null OR WarehouseName like N'%'+@WarehouseCode+'%')
end
GO
/****** Object:  StoredProcedure [dbo].[Warehouse_Update]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Warehouse_Update]
 @Id numeric(18,0)
 ,@WarehouseCode nvarchar(10)
,@WarehouseName 		nvarchar(500)
,@Address 			nvarchar(500)
,@Phone 			varchar(50)
,@Fax 					varchar(50)
,@Email 			varchar(100)
,@Representative 		nvarchar(500)
,@Remark 			nvarchar(max)
,@ModifiedBy varchar(50)
as 
begin
declare @p_count numeric(18, 0)=0;
UPDATE  [Warehouse]
   SET [WarehouseCode] = @WarehouseCode
      ,[WarehouseName] = @WarehouseName
      ,[Address] = @Address
      ,[Phone] = @Phone
      ,[Fax] = @Fax
      ,[Email] = @Email
      ,[Representative] = @Representative
      ,[Remark] = @Remark
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate] = GETDATE()
 WHERE Id=@Id AND ISNULL(ISDELETE,0)=0;
 IF (@@ROWCOUNT > 0)
		BEGIN
			SET @p_count =1;
		END;
	ELSE 
		BEGIN
			SET @p_count = -1;
		END;
   select @p_count;
 end;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_FormatWithCommas]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
cREATE FUNCTION [dbo].[fn_FormatWithCommas] 
(
    -- Add the parameters for the function here
    @value varchar(50)
)
RETURNS varchar(50)
AS
BEGIN
    -- Declare the return variable here
    DECLARE @WholeNumber varchar(50) = NULL, @Decimal varchar(10) = '', @CharIndex int = charindex('.', @value)

    IF (@CharIndex > 0)
        SELECT @WholeNumber = SUBSTRING(@value, 1, @CharIndex-1), @Decimal = SUBSTRING(@value, @CharIndex, LEN(@value))
    ELSE
        SET @WholeNumber = @value

    IF(LEN(@WholeNumber) > 3)
        SET @WholeNumber = dbo.fn_FormatWithCommas(SUBSTRING(@WholeNumber, 1, LEN(@WholeNumber)-3)) + ',' + RIGHT(@WholeNumber, 3)



    -- Return the result of the function
    RETURN @WholeNumber + @Decimal

END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GenCode]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_GenCode] 
(
     @columnName varchar(50)
    ,@tableName varchar(50)
)
RETURNS varchar(50)
AS
BEGIN
DECLARE	@strCode varchar(50);
EXEC  [dbo].[GetCodeAllTable]
		@colName = @columnName,
		@tableName = @tableName,
		@strCode = @strCode OUTPUT;
		return @strCode;
ENd;
GO
/****** Object:  Table [dbo].[Food]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Food](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[FoodCode] [varchar](10) NOT NULL,
	[FoodName] [nvarchar](250) NOT NULL,
	[FoodTypeId] [numeric](18, 0) NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FoodDetail]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FoodDetail](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[FoodId] [numeric](18, 0) NOT NULL,
	[MaterialId] [numeric](18, 0) NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FoodMenu]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FoodMenu](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[FoodMenuCode] [nvarchar](50) NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
	[SchoolsId] [numeric](18, 0) NOT NULL,
	[NumberFood] [int] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FoodMenu_Detail]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FoodMenu_Detail](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[DateMenu] [date] NOT NULL,
	[FoodMenuId] [numeric](18, 0) NULL,
	[FoodId] [numeric](18, 0) NOT NULL,
	[NumberFoodTeacher] [int] NULL,
	[NumberFoodStudent] [int] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FoodReality]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FoodReality](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[SchoolId] [numeric](18, 0) NOT NULL,
	[DateFoodReality] [datetime] NOT NULL,
	[NumberFoodTeacher] [numeric](18, 0) NOT NULL,
	[NumberFoodStudent] [numeric](18, 0) NOT NULL,
	[NumberFoodTeacherReality] [numeric](18, 0) NOT NULL,
	[NumberFoodStudentReality] [numeric](18, 0) NOT NULL,
	[PriceStudentReality] [numeric](18, 0) NOT NULL,
	[PriceTeacherReality] [numeric](18, 0) NOT NULL,
	[IsConfirm] [bit] NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FoodTypes]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FoodTypes](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[FoodTypesCode] [varchar](10) NOT NULL,
	[FoodTypesName] [nvarchar](300) NULL,
	[IsSession] [int] NULL,
	[Remark] [nvarchar](500) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_FoodTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GroupMaterial]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GroupMaterial](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[GroupMaterialCode] [varchar](10) NOT NULL,
	[GroupMaterialName] [nvarchar](300) NULL,
	[Remark] [nvarchar](500) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Inventory](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[InventoryCode] [varchar](10) NULL,
	[WarehouseId] [varchar](5) NOT NULL,
	[DateInventory] [datetime] NOT NULL,
	[IsFinish] [bit] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InventoryDetail]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InventoryDetail](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[InventoryId] [numeric](18, 0) NULL,
	[MaterialId] [numeric](18, 0) NULL,
	[Quantity] [numeric](18, 3) NULL,
	[PriceReceived] [numeric](18, 0) NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Material]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Material](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[MaterialCode] [varchar](10) NOT NULL,
	[MaterialName] [nvarchar](250) NULL,
	[GroupMaterialId] [numeric](18, 0) NULL,
	[UnitId] [numeric](18, 0) NULL,
	[SupplierId] [numeric](18, 0) NULL,
	[VAT] [tinyint] NULL,
	[Price] [decimal](18, 3) NULL,
	[Remark] [nvarchar](500) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MaterialsFifo]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MaterialsFifo](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[DateFifo] [datetime] NOT NULL,
	[MaterialId] [numeric](18, 0) NULL,
	[QuantityReceived] [numeric](18, 4) NULL,
	[PriceReceived] [numeric](18, 0) NULL,
	[QuantityDelivery] [numeric](18, 4) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ListIdReceived] [varchar](max) NULL,
	[ImportWarehousingMaterialsId] [numeric](18, 0) NULL,
	[WarehouseId] [numeric](18, 0) NULL,
 CONSTRAINT [PK_MaterialsFifo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuantitativeFood]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuantitativeFood](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FoodId] [numeric](18, 0) NOT NULL,
	[MaterialId] [numeric](18, 0) NOT NULL,
	[SchoolId] [numeric](18, 0) NULL,
	[QuantitativeOne] [numeric](18, 4) NULL,
	[QuantitativeTwo] [numeric](18, 4) NULL,
	[QuantitativeOrther] [numeric](18, 4) NULL,
	[Price] [numeric](18, 4) NULL,
	[Remark] [nvarchar](500) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[S_Logs]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[S_Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[LogDate] [datetime] NULL,
	[LogAction] [nvarchar](max) NULL,
	[LogLevel] [varchar](100) NULL,
	[Logger] [varchar](100) NULL,
	[FormName] [varchar](100) NULL,
	[LogContent] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
 CONSTRAINT [PK_S_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Schools]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Schools](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[SchoolsCode] [varchar](10) NOT NULL,
	[SchoolsName] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[Representative] [nvarchar](500) NULL,
	[NumberStudentPrimary] [numeric](18, 0) NULL,
	[NumberStudentJuniorHigh] [numeric](18, 0) NULL,
	[NumberStudentHighSchool] [numeric](18, 0) NULL,
	[NumberTeacher] [numeric](18, 0) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[SupplierCode] [varchar](50) NULL,
	[SupplierName] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[Representative] [nvarchar](500) NULL,
	[TaxCode] [nvarchar](50) NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[U_Menu]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[U_Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuCode] [nvarchar](50) NOT NULL,
	[MenuName] [nvarchar](500) NULL,
	[MenuNamespaceClass] [nvarchar](500) NULL,
	[MenuClassName] [nvarchar](500) NULL,
	[MenuRemark] [nvarchar](500) NULL,
	[MenuIcon] [nvarchar](500) NULL,
	[MenuParentID] [int] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_U_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[U_User]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[U_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](500) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](500) NULL,
	[Phone] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[UserGroupID] [int] NULL,
	[IsSuperAdmin] [bit] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[AreaID] [int] NOT NULL,
 CONSTRAINT [PK_U_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[U_User_Role]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[U_User_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeID] [int] NULL,
	[MenuID] [int] NULL,
 CONSTRAINT [PK_U_User_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[U_UserType]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[U_UserType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeCode] [nvarchar](50) NOT NULL,
	[UserTypeName] [nvarchar](500) NULL,
	[UserTypeGroup] [int] NULL,
	[UserTypeDescription] [nvarchar](max) NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_U_UserType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Unit]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Unit](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[UnitCode] [varchar](10) NOT NULL,
	[UnitName] [nvarchar](300) NULL,
	[Remark] [nvarchar](500) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Warehouse](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[WarehouseCode] [varchar](10) NOT NULL,
	[WarehouseName] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[Representative] [nvarchar](500) NULL,
	[Remark] [nvarchar](max) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WarehousingMaterials]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WarehousingMaterials](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[WarehouseId] [decimal](18, 0) NOT NULL,
	[WarehouseIdTo] [decimal](18, 0) NULL,
	[IsConfirm] [bit] NULL,
	[ImplementationDates] [datetime] NULL,
	[WarehousingMaterialsCode] [varchar](50) NULL,
	[IsImport] [bit] NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[SchoolId] [decimal](18, 0) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WarehousingMaterialsDetail]    Script Date: 12/26/2017 2:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WarehousingMaterialsDetail](
	[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[WarehousingMaterialsId] [decimal](18, 0) NOT NULL,
	[MaterialId] [numeric](18, 0) NULL,
	[FoodId] [numeric](18, 0) NULL,
	[Quantity] [numeric](18, 4) NULL,
	[PriceReceived] [numeric](18, 0) NULL,
	[PriceExport] [numeric](18, 0) NULL,
	[RowVersion] [bigint] NULL,
	[IsDelete] [bit] NULL,
	[CreateBy] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 

INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(1 AS Decimal(18, 0)), N'A00000001', N'Bí xanh', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A650BE AS DateTime), N'Admin', CAST(0x0000A8500183A41A AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(2 AS Decimal(18, 0)), N'A00000002', N'bí đỏ', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A655CE AS DateTime), N'Admin', CAST(0x0000A84D00A6AABD AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(3 AS Decimal(18, 0)), N'A00000003', N'Bắp cải', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AB0BE8 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(4 AS Decimal(18, 0)), N'A00000004', N'Bầu', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AB4FD8 AS DateTime), N'Admin', CAST(0x0000A84D00AB8B31 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(5 AS Decimal(18, 0)), N'A00000005', N'Cải ngọt', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ABBD41 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(6 AS Decimal(18, 0)), N'A00000006', N'Cải xanh', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ABE6DF AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(7 AS Decimal(18, 0)), N'A00000007', N'Chua rau muống', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AC4595 AS DateTime), N'Admin', CAST(0x0000A85001826A10 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(8 AS Decimal(18, 0)), N'A00000008', N'Mây hồng', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AC9194 AS DateTime), N'Admin', CAST(0x0000A8500182C2E1 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(11 AS Decimal(18, 0)), N'A00000009', N'Chua thập cẩm', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AD490B AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(14 AS Decimal(18, 0)), N'A00000010', N'Cải chua', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ADC780 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(15 AS Decimal(18, 0)), N'A00000011', N'Soup', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AE1560 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(18 AS Decimal(18, 0)), N'A00000012', N'Cải thảo', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F456DA AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(19 AS Decimal(18, 0)), N'A00000013', N'Rau dền, mồng tơi nấu cua', CAST(2 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A85000F4AD93 AS DateTime), N'Admin', CAST(0x0000A85001815C60 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(20 AS Decimal(18, 0)), N'A00000014', N'Cà chua hến', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F4F304 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(21 AS Decimal(18, 0)), N'A00000015', N'Khoai mỡ', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F52E6F AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(22 AS Decimal(18, 0)), N'A00000016', N'Đậu hũ hẹ nấu thịt', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F58086 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(23 AS Decimal(18, 0)), N'A00000017', N'Lá giang', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F5DB42 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(24 AS Decimal(18, 0)), N'A00000018', N'Cải xoong ', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F6226C AS DateTime), N'Admin', CAST(0x0000A85000F691B2 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(26 AS Decimal(18, 0)), N'A00000019', N'Đu đủ', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F6EBAD AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(29 AS Decimal(18, 0)), N'A00000020', N'Su su', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F756E4 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(30 AS Decimal(18, 0)), N'A00000021', N'Gà kho sả', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F7A201 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(31 AS Decimal(18, 0)), N'A00000022', N'Đậu hũ nhồi thịt sốt cà', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F81D08 AS DateTime), N'Admin', CAST(0x0000A85000F8810E AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(32 AS Decimal(18, 0)), N'A00000023', N'Chả cá chiên thì là', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F8BD0C AS DateTime), N'Admin', CAST(0x0000A85000F8E6B9 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(33 AS Decimal(18, 0)), N'A00000024', N'Thịt kho thơm', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F91621 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(34 AS Decimal(18, 0)), N'A00000025', N'Thịt kho cà', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F93445 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(35 AS Decimal(18, 0)), N'A00000026', N'Đùi gà chiên giòn', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F9A428 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(36 AS Decimal(18, 0)), N'A00000027', N'Thịt kho rau củ', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA3FB9 AS DateTime), N'Admin', CAST(0x0000A85000FB1A80 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(37 AS Decimal(18, 0)), N'A00000028', N'Xíu mại sốt cà', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA4CE9 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(38 AS Decimal(18, 0)), N'A00000029', N'Cốt lết ram', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB4504 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(39 AS Decimal(18, 0)), N'A00000030', N'Cơm tấm sườn', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB8BD6 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(40 AS Decimal(18, 0)), N'A00000031', N'Ba rọi rim tép', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FBC177 AS DateTime), N'Admin', CAST(0x0000A85001071391 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(41 AS Decimal(18, 0)), N'A00000032', N'Chả lụa kho nước tương', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC0269 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(43 AS Decimal(18, 0)), N'A00000033', N'Sườn non ram mặn', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC618A AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(44 AS Decimal(18, 0)), N'A00000034', N'Cá basa muối sả chiên giòn', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC9E63 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(45 AS Decimal(18, 0)), N'A00000035', N'Cá basa fille chiên giòn', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD0C7A AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(46 AS Decimal(18, 0)), N'A00000036', N'Thịt kho trứng', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD34E8 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(49 AS Decimal(18, 0)), N'A00000037', N'Thịt kho trứng cút', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD940E AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(50 AS Decimal(18, 0)), N'A00000038', N'Cơm tấm chả trứng hấp', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FE6CF4 AS DateTime), N'Admin', CAST(0x0000A850010424F0 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(51 AS Decimal(18, 0)), N'A00000039', N'Xíu mại trứng cút', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104A8DA AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(52 AS Decimal(18, 0)), N'A00000040', N'Thịt kho đậu hũ', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104E0B5 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(53 AS Decimal(18, 0)), N'A00000041', N'Gà kho gừng', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001051644 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(54 AS Decimal(18, 0)), N'A00000042', N'Sườn non kho thơm', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001055039 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(55 AS Decimal(18, 0)), N'A00000043', N'Sườn non kho đậu hũ', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010582E3 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(56 AS Decimal(18, 0)), N'A00000044', N'Thịt kho cải chua', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500105BABD AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(57 AS Decimal(18, 0)), N'A00000045', N'Gà kho ngũ sắc', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001061B52 AS DateTime), N'Admin', CAST(0x0000A850010BEF67 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(58 AS Decimal(18, 0)), N'A00000046', N'Chả cá rim nước tương', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001076600 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(59 AS Decimal(18, 0)), N'A00000047', N'Đậu hũ sốt thịt bằm', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500107E58D AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(60 AS Decimal(18, 0)), N'A00000048', N'Cá trứng chiên, trứng chiên', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010839F8 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(61 AS Decimal(18, 0)), N'A00000049', N'Cơm tấm xá xíu, ốp la', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500108D884 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(62 AS Decimal(18, 0)), N'A00000050', N'Cơm tấm thịt ram, ốp la', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500109357F AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(63 AS Decimal(18, 0)), N'A00000051', N'Chả cá kho xì dầu', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001097CC0 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(64 AS Decimal(18, 0)), N'A00000052', N'Chả thịt', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500109D8B5 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(65 AS Decimal(18, 0)), N'A00000053', N'Bò hầm củ cải', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010A0D5A AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(66 AS Decimal(18, 0)), N'A00000054', N'Bò nấu đậu', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010A4D6D AS DateTime), N'Admin', CAST(0x0000A850010B5CE0 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(67 AS Decimal(18, 0)), N'A00000055', N'Bò hầm rau củ', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BB1A9 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(68 AS Decimal(18, 0)), N'A00000056', N'Bò xào chua ngọt', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C55D3 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(69 AS Decimal(18, 0)), N'A00000057', N'Bò xào hành', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C96EC AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(70 AS Decimal(18, 0)), N'A00000058', N'Cơm tấm chả trứng, xá xíu', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D5830 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(72 AS Decimal(18, 0)), N'A00000059', N'Chả cá kho trứng cút', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010DBA4E AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(73 AS Decimal(18, 0)), N'A00000060', N'Cà basa fille chiên sốt cà', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010DFB6C AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(74 AS Decimal(18, 0)), N'A00000061', N'Sườn nấu đậu', CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010E8651 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(75 AS Decimal(18, 0)), N'A00000062', N'Bông cải xào thập cẩm', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010F1DF9 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(76 AS Decimal(18, 0)), N'A00000063', N'Rau muống xào tỏi', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010F3962 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(77 AS Decimal(18, 0)), N'A00000064', N'Đậu đũa', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010F9679 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(78 AS Decimal(18, 0)), N'A00000065', N'Đậu que xào thập cẩm', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010FF7CE AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(79 AS Decimal(18, 0)), N'A00000066', N'Củ sắn xào cà rốt', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001101DE0 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(80 AS Decimal(18, 0)), N'A00000067', N'Su su', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500110356C AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(81 AS Decimal(18, 0)), N'A00000068', N'Bắp cải xào cà rốt', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001106C1F AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(82 AS Decimal(18, 0)), N'A00000069', N'Bầu', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850011081D6 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(83 AS Decimal(18, 0)), N'A00000070', N'Cải thìa', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850011096AD AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(84 AS Decimal(18, 0)), N'A00000071', N'Giá hẹ xào', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500110D36F AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(85 AS Decimal(18, 0)), N'A00000072', N'Giá xào mướp', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500110F141 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(86 AS Decimal(18, 0)), N'A00000073', N'Dưa leo xào hành cần', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500111593C AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(89 AS Decimal(18, 0)), N'A00000074', N'Dưa leo', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001119616 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(90 AS Decimal(18, 0)), N'A00000075', N'Bắp cải xào bắp non', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500111CE52 AS DateTime), N'Admin', CAST(0x0000A85001122A6E AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(91 AS Decimal(18, 0)), N'A00000076', N'Mướp xào giá', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500111CE79 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(92 AS Decimal(18, 0)), N'A00000077', N'Su hào ', CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001132742 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(93 AS Decimal(18, 0)), N'A00000078', N'Yaourt', CAST(4 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001137F05 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(94 AS Decimal(18, 0)), N'A00000079', N'Bánh canh thịt, trứng cút', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F896 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(95 AS Decimal(18, 0)), N'A00000080', N'Phở bò viên', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B4E AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(96 AS Decimal(18, 0)), N'A00000081', N'Bún mộc', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001382FCC AS DateTime), N'Admin', CAST(0x0000A8500138CDA0 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(97 AS Decimal(18, 0)), N'A00000082', N'Bún bò huế', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A2582 AS DateTime), N'Admin', CAST(0x0000A850013A6F1F AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(98 AS Decimal(18, 0)), N'A00000083', N'Nui nấu thịt', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE516 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(99 AS Decimal(18, 0)), N'A00000084', N'Bún riêu', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016ECA05 AS DateTime), N'Admin', CAST(0x0000A850016F9F8C AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(100 AS Decimal(18, 0)), N'A00000085', N'Canh bún', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707B97 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(101 AS Decimal(18, 0)), N'A00000086', N'Hủ tíu mì', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C7E AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(102 AS Decimal(18, 0)), N'A00000087', N'Hủ tíu nam vang', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001721DB4 AS DateTime), N'Admin', CAST(0x0000A85001726674 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(103 AS Decimal(18, 0)), N'A00000088', N'Hủ tíu bò kho', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001730DEA AS DateTime), N'Admin', CAST(0x0000A85001734A07 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(104 AS Decimal(18, 0)), N'A00000089', N'Mì xá xíu', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3D7 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(105 AS Decimal(18, 0)), N'A00000090', N'Bánh mì cari', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001741ECF AS DateTime), N'Admin', CAST(0x0000A8500175291F AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(106 AS Decimal(18, 0)), N'A00000091', N'Bún chả cá', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A96A AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(107 AS Decimal(18, 0)), N'A00000092', N'Bún chả cá', CAST(7 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B930 AS DateTime), N'Admin', CAST(0x0000A8500175CA89 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(108 AS Decimal(18, 0)), N'A00000093', N'Phở gà', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001768497 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(109 AS Decimal(18, 0)), N'A00000094', N'Bún thịt xào', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001772CD1 AS DateTime), N'Admin', CAST(0x0000A8500177A2CC AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(110 AS Decimal(18, 0)), N'A00000095', N'Dưa hấu', CAST(4 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177F68C AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(111 AS Decimal(18, 0)), N'A00000096', N'Chuối', CAST(4 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001780CD0 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(112 AS Decimal(18, 0)), N'A00000097', N'Rau câu', CAST(4 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500178BA74 AS DateTime), N'Admin', CAST(0x0000A85001791CEF AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(113 AS Decimal(18, 0)), N'A00000098', N'Rau câu', CAST(4 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500178BBD6 AS DateTime), N'Admin', CAST(0x0000A8500178CEB4 AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(114 AS Decimal(18, 0)), N'A00000099', N'Bò kho bánh mì', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C24 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(115 AS Decimal(18, 0)), N'A00000100', N'Hủ tíu mì xá xíu', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65B2 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(116 AS Decimal(18, 0)), N'A00000101', N'Gà nấu đậu', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017AE456 AS DateTime), N'Admin', CAST(0x0000A850017B56FC AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(118 AS Decimal(18, 0)), N'A00000102', N'Lagu bò, bánh mì', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017C7231 AS DateTime), NULL, NULL)
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(119 AS Decimal(18, 0)), N'A00000103', N'Bún thịt xào, chả giò', CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017D1A3C AS DateTime), N'Admin', CAST(0x0000A850017DE48F AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(120 AS Decimal(18, 0)), N'A00000104', N'Rau ngót, mướp nấu thịt', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001809DE5 AS DateTime), N'Admin', CAST(0x0000A8500183340A AS DateTime))
INSERT [dbo].[Food] ([Id], [FoodCode], [FoodName], [FoodTypeId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(121 AS Decimal(18, 0)), N'A00000105', N'Rau dền, mồng tơi nấu cua', CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001819E96 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Food] OFF
SET IDENTITY_INSERT [dbo].[FoodDetail] ON 

INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(5 AS Decimal(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A6AAC3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(6 AS Decimal(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(51 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A6AAC4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(7 AS Decimal(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(69 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A6AAC6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(8 AS Decimal(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(70 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A6AAC7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(12 AS Decimal(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AB0BEA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(13 AS Decimal(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AB0BEE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(15 AS Decimal(18, 0)), CAST(4 AS Numeric(18, 0)), CAST(52 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AB8B34 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(16 AS Decimal(18, 0)), CAST(4 AS Numeric(18, 0)), CAST(123 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AB8B35 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(17 AS Decimal(18, 0)), CAST(5 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ABBD43 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(18 AS Decimal(18, 0)), CAST(5 AS Numeric(18, 0)), CAST(62 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ABBD45 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(19 AS Decimal(18, 0)), CAST(6 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ABE6E1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(20 AS Decimal(18, 0)), CAST(6 AS Numeric(18, 0)), CAST(63 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ABE6E2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(21 AS Decimal(18, 0)), CAST(6 AS Numeric(18, 0)), CAST(64 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ABE701 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(29 AS Decimal(18, 0)), CAST(11 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AD490D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(30 AS Decimal(18, 0)), CAST(11 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AD490F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(31 AS Decimal(18, 0)), CAST(11 AS Numeric(18, 0)), CAST(83 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AD4910 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(32 AS Decimal(18, 0)), CAST(11 AS Numeric(18, 0)), CAST(87 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AD4912 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(33 AS Decimal(18, 0)), CAST(11 AS Numeric(18, 0)), CAST(89 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AD4914 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(34 AS Decimal(18, 0)), CAST(11 AS Numeric(18, 0)), CAST(123 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AD4918 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(35 AS Decimal(18, 0)), CAST(14 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ADC781 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(36 AS Decimal(18, 0)), CAST(14 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ADC783 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(37 AS Decimal(18, 0)), CAST(14 AS Numeric(18, 0)), CAST(86 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00ADC785 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(38 AS Decimal(18, 0)), CAST(15 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AE1562 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(39 AS Decimal(18, 0)), CAST(15 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AE1564 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(40 AS Decimal(18, 0)), CAST(15 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AE1566 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(41 AS Decimal(18, 0)), CAST(15 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AE1567 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(42 AS Decimal(18, 0)), CAST(15 AS Numeric(18, 0)), CAST(76 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AE1569 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(43 AS Decimal(18, 0)), CAST(15 AS Numeric(18, 0)), CAST(80 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AE156A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(44 AS Decimal(18, 0)), CAST(18 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F456DE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(45 AS Decimal(18, 0)), CAST(18 AS Numeric(18, 0)), CAST(124 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F456DF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(734 AS Decimal(18, 0)), CAST(19 AS Numeric(18, 0)), CAST(31 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A85001804C9B AS DateTime), N'Admin', CAST(0x0000A85001815C61 AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(172 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010424F2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(173 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010424F4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(174 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010424F6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(175 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010424F7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(176 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(71 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010424FB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(177 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010424FD AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(178 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(95 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010424FE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(179 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(126 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010424FF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(180 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001042501 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(181 AS Decimal(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001042503 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(182 AS Decimal(18, 0)), CAST(51 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104A8DC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(183 AS Decimal(18, 0)), CAST(51 AS Numeric(18, 0)), CAST(33 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104A8DE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(184 AS Decimal(18, 0)), CAST(51 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104A8E1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(185 AS Decimal(18, 0)), CAST(51 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104A8E2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(186 AS Decimal(18, 0)), CAST(51 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104A8E4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(187 AS Decimal(18, 0)), CAST(51 AS Numeric(18, 0)), CAST(98 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104A8E6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(188 AS Decimal(18, 0)), CAST(51 AS Numeric(18, 0)), CAST(126 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104A8E9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(189 AS Decimal(18, 0)), CAST(51 AS Numeric(18, 0)), CAST(127 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104A8EB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(190 AS Decimal(18, 0)), CAST(52 AS Numeric(18, 0)), CAST(34 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104E0B7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(191 AS Decimal(18, 0)), CAST(52 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104E0B9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(192 AS Decimal(18, 0)), CAST(52 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500104E0BA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(193 AS Decimal(18, 0)), CAST(53 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001051645 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(194 AS Decimal(18, 0)), CAST(53 AS Numeric(18, 0)), CAST(64 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001051647 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(195 AS Decimal(18, 0)), CAST(54 AS Numeric(18, 0)), CAST(19 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500105503C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(196 AS Decimal(18, 0)), CAST(54 AS Numeric(18, 0)), CAST(87 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500105503E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(197 AS Decimal(18, 0)), CAST(54 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001055040 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(198 AS Decimal(18, 0)), CAST(55 AS Numeric(18, 0)), CAST(19 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010582E5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(199 AS Decimal(18, 0)), CAST(55 AS Numeric(18, 0)), CAST(34 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010582E6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(200 AS Decimal(18, 0)), CAST(55 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010582E7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(201 AS Decimal(18, 0)), CAST(56 AS Numeric(18, 0)), CAST(86 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500105BABF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(202 AS Decimal(18, 0)), CAST(56 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500105BAC0 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(203 AS Decimal(18, 0)), CAST(56 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500105BAC1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(367 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(99 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B64 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(368 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(106 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B66 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(369 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(108 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B68 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(370 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(109 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B6A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(371 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B6C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(372 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B6D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(217 AS Decimal(18, 0)), CAST(40 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001071394 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(218 AS Decimal(18, 0)), CAST(40 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001071396 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(219 AS Decimal(18, 0)), CAST(40 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001071397 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(220 AS Decimal(18, 0)), CAST(40 AS Numeric(18, 0)), CAST(133 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001071399 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(221 AS Decimal(18, 0)), CAST(58 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001076601 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(222 AS Decimal(18, 0)), CAST(58 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001076603 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(223 AS Decimal(18, 0)), CAST(58 AS Numeric(18, 0)), CAST(100 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001076604 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(224 AS Decimal(18, 0)), CAST(59 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500107E58F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(225 AS Decimal(18, 0)), CAST(59 AS Numeric(18, 0)), CAST(34 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500107E590 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(226 AS Decimal(18, 0)), CAST(59 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500107E592 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(227 AS Decimal(18, 0)), CAST(59 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500107E593 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(228 AS Decimal(18, 0)), CAST(59 AS Numeric(18, 0)), CAST(98 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500107E595 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(229 AS Decimal(18, 0)), CAST(59 AS Numeric(18, 0)), CAST(104 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500107E596 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(230 AS Decimal(18, 0)), CAST(60 AS Numeric(18, 0)), CAST(5 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010839F9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(231 AS Decimal(18, 0)), CAST(60 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010839FB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(232 AS Decimal(18, 0)), CAST(60 AS Numeric(18, 0)), CAST(71 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010839FC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(233 AS Decimal(18, 0)), CAST(60 AS Numeric(18, 0)), CAST(83 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010839FE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(234 AS Decimal(18, 0)), CAST(60 AS Numeric(18, 0)), CAST(103 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010839FF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(235 AS Decimal(18, 0)), CAST(60 AS Numeric(18, 0)), CAST(104 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001083A01 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(236 AS Decimal(18, 0)), CAST(60 AS Numeric(18, 0)), CAST(105 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001083A02 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(237 AS Decimal(18, 0)), CAST(61 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500108D886 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(238 AS Decimal(18, 0)), CAST(61 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500108D887 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(239 AS Decimal(18, 0)), CAST(61 AS Numeric(18, 0)), CAST(71 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500108D889 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(240 AS Decimal(18, 0)), CAST(61 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500108D88A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(241 AS Decimal(18, 0)), CAST(61 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500108D88C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(242 AS Decimal(18, 0)), CAST(61 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500108D88D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(243 AS Decimal(18, 0)), CAST(62 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001093581 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(735 AS Decimal(18, 0)), CAST(19 AS Numeric(18, 0)), CAST(65 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A85001804C9D AS DateTime), N'Admin', CAST(0x0000A85001815C61 AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(736 AS Decimal(18, 0)), CAST(19 AS Numeric(18, 0)), CAST(67 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A85001804C9E AS DateTime), N'Admin', CAST(0x0000A85001815C61 AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(737 AS Decimal(18, 0)), CAST(19 AS Numeric(18, 0)), CAST(68 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A85001804CA0 AS DateTime), N'Admin', CAST(0x0000A85001815C61 AS DateTime))
GO
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(50 AS Decimal(18, 0)), CAST(20 AS Numeric(18, 0)), CAST(32 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F4F305 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(51 AS Decimal(18, 0)), CAST(20 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F4F307 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(52 AS Decimal(18, 0)), CAST(20 AS Numeric(18, 0)), CAST(84 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F4F308 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(53 AS Decimal(18, 0)), CAST(21 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F52E71 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(54 AS Decimal(18, 0)), CAST(21 AS Numeric(18, 0)), CAST(53 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F52E72 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(55 AS Decimal(18, 0)), CAST(21 AS Numeric(18, 0)), CAST(70 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F52E73 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(56 AS Decimal(18, 0)), CAST(22 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F58087 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(57 AS Decimal(18, 0)), CAST(22 AS Numeric(18, 0)), CAST(16 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F58089 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(58 AS Decimal(18, 0)), CAST(22 AS Numeric(18, 0)), CAST(61 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F5808B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(59 AS Decimal(18, 0)), CAST(22 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F5808C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(60 AS Decimal(18, 0)), CAST(23 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F5DB43 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(61 AS Decimal(18, 0)), CAST(23 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F5DB44 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(62 AS Decimal(18, 0)), CAST(23 AS Numeric(18, 0)), CAST(69 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F5DB46 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(63 AS Decimal(18, 0)), CAST(23 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F5DB47 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(64 AS Decimal(18, 0)), CAST(23 AS Numeric(18, 0)), CAST(83 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F5DB49 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(65 AS Decimal(18, 0)), CAST(23 AS Numeric(18, 0)), CAST(85 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F5DB4A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(69 AS Decimal(18, 0)), CAST(24 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F691B4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(70 AS Decimal(18, 0)), CAST(24 AS Numeric(18, 0)), CAST(125 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F691B6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(71 AS Decimal(18, 0)), CAST(26 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F6EBAF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(72 AS Decimal(18, 0)), CAST(26 AS Numeric(18, 0)), CAST(72 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F6EBB0 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(73 AS Decimal(18, 0)), CAST(26 AS Numeric(18, 0)), CAST(78 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F6EBB2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(74 AS Decimal(18, 0)), CAST(29 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F756E5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(75 AS Decimal(18, 0)), CAST(29 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F756E7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(76 AS Decimal(18, 0)), CAST(29 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F756E9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(77 AS Decimal(18, 0)), CAST(30 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F7A202 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(78 AS Decimal(18, 0)), CAST(30 AS Numeric(18, 0)), CAST(91 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F7A204 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(79 AS Decimal(18, 0)), CAST(30 AS Numeric(18, 0)), CAST(117 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F7A205 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(87 AS Decimal(18, 0)), CAST(31 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F88112 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(88 AS Decimal(18, 0)), CAST(31 AS Numeric(18, 0)), CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F88113 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(89 AS Decimal(18, 0)), CAST(31 AS Numeric(18, 0)), CAST(15 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F88116 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(90 AS Decimal(18, 0)), CAST(31 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F88117 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(91 AS Decimal(18, 0)), CAST(31 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F88119 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(92 AS Decimal(18, 0)), CAST(31 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F8811A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(93 AS Decimal(18, 0)), CAST(31 AS Numeric(18, 0)), CAST(98 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F8811B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(94 AS Decimal(18, 0)), CAST(31 AS Numeric(18, 0)), CAST(126 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F8811D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(95 AS Decimal(18, 0)), CAST(31 AS Numeric(18, 0)), CAST(127 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F8811F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(98 AS Decimal(18, 0)), CAST(32 AS Numeric(18, 0)), CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F8E6BC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(99 AS Decimal(18, 0)), CAST(32 AS Numeric(18, 0)), CAST(98 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F8E6BD AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(100 AS Decimal(18, 0)), CAST(32 AS Numeric(18, 0)), CAST(128 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F8E6C0 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(101 AS Decimal(18, 0)), CAST(33 AS Numeric(18, 0)), CAST(87 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F91623 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(102 AS Decimal(18, 0)), CAST(33 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F91624 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(103 AS Decimal(18, 0)), CAST(34 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F93447 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(104 AS Decimal(18, 0)), CAST(34 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F93449 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(105 AS Decimal(18, 0)), CAST(35 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F9A42A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(106 AS Decimal(18, 0)), CAST(35 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F9A431 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(107 AS Decimal(18, 0)), CAST(35 AS Numeric(18, 0)), CAST(30 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F9A433 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(108 AS Decimal(18, 0)), CAST(35 AS Numeric(18, 0)), CAST(98 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F9A434 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(109 AS Decimal(18, 0)), CAST(35 AS Numeric(18, 0)), CAST(103 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F9A435 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(110 AS Decimal(18, 0)), CAST(35 AS Numeric(18, 0)), CAST(104 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F9A437 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(111 AS Decimal(18, 0)), CAST(35 AS Numeric(18, 0)), CAST(105 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F9A439 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(131 AS Decimal(18, 0)), CAST(36 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB1A84 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(132 AS Decimal(18, 0)), CAST(36 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB1A8A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(133 AS Decimal(18, 0)), CAST(36 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB1A8D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(134 AS Decimal(18, 0)), CAST(36 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB1A90 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(135 AS Decimal(18, 0)), CAST(38 AS Numeric(18, 0)), CAST(17 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB4505 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(136 AS Decimal(18, 0)), CAST(39 AS Numeric(18, 0)), CAST(17 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB8BD8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(137 AS Decimal(18, 0)), CAST(39 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB8BD9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(138 AS Decimal(18, 0)), CAST(39 AS Numeric(18, 0)), CAST(71 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB8BDB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(120 AS Decimal(18, 0)), CAST(37 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA4CEB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(121 AS Decimal(18, 0)), CAST(37 AS Numeric(18, 0)), CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA4CEC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(122 AS Decimal(18, 0)), CAST(37 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA4CEE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(123 AS Decimal(18, 0)), CAST(37 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA4CEF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(124 AS Decimal(18, 0)), CAST(37 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA4CF1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(125 AS Decimal(18, 0)), CAST(37 AS Numeric(18, 0)), CAST(98 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA4CF3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(126 AS Decimal(18, 0)), CAST(37 AS Numeric(18, 0)), CAST(126 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA4CF4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(127 AS Decimal(18, 0)), CAST(37 AS Numeric(18, 0)), CAST(127 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FA4CF7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(139 AS Decimal(18, 0)), CAST(39 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FB8BDC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(342 AS Decimal(18, 0)), CAST(93 AS Numeric(18, 0)), CAST(47 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001137F07 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(343 AS Decimal(18, 0)), CAST(93 AS Numeric(18, 0)), CAST(49 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001137F09 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(344 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F899 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(143 AS Decimal(18, 0)), CAST(41 AS Numeric(18, 0)), CAST(11 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC026A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(144 AS Decimal(18, 0)), CAST(41 AS Numeric(18, 0)), CAST(100 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC026C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(145 AS Decimal(18, 0)), CAST(41 AS Numeric(18, 0)), CAST(122 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC026D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(146 AS Decimal(18, 0)), CAST(43 AS Numeric(18, 0)), CAST(19 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC618C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(147 AS Decimal(18, 0)), CAST(43 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC618F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(148 AS Decimal(18, 0)), CAST(43 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC6190 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(149 AS Decimal(18, 0)), CAST(44 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC9E65 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(150 AS Decimal(18, 0)), CAST(44 AS Numeric(18, 0)), CAST(90 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC9E66 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(151 AS Decimal(18, 0)), CAST(44 AS Numeric(18, 0)), CAST(91 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FC9E68 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(152 AS Decimal(18, 0)), CAST(45 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD0C7B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(153 AS Decimal(18, 0)), CAST(45 AS Numeric(18, 0)), CAST(102 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD0C7D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(154 AS Decimal(18, 0)), CAST(45 AS Numeric(18, 0)), CAST(103 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD0C7E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(155 AS Decimal(18, 0)), CAST(45 AS Numeric(18, 0)), CAST(104 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD0C80 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(156 AS Decimal(18, 0)), CAST(45 AS Numeric(18, 0)), CAST(105 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD0C81 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(157 AS Decimal(18, 0)), CAST(46 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD34EA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(158 AS Decimal(18, 0)), CAST(46 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD34EC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(159 AS Decimal(18, 0)), CAST(46 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD34ED AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(160 AS Decimal(18, 0)), CAST(49 AS Numeric(18, 0)), CAST(33 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD940F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(161 AS Decimal(18, 0)), CAST(49 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD9410 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(162 AS Decimal(18, 0)), CAST(49 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FD9412 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(345 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(24 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F89B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(346 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(33 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F89C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(347 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(42 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F89E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(348 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F8A0 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(349 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F8A2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(350 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(72 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F8A3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(351 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F8A5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(352 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F8A6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(353 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F8A8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(354 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F8A9 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(355 AS Decimal(18, 0)), CAST(94 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500114F8AB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(431 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE51C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(432 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(33 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE51E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(244 AS Decimal(18, 0)), CAST(62 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001093582 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(245 AS Decimal(18, 0)), CAST(62 AS Numeric(18, 0)), CAST(71 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001093584 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(246 AS Decimal(18, 0)), CAST(62 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001093586 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(247 AS Decimal(18, 0)), CAST(62 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001093587 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(248 AS Decimal(18, 0)), CAST(62 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001093589 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(249 AS Decimal(18, 0)), CAST(63 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001097CC1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(250 AS Decimal(18, 0)), CAST(63 AS Numeric(18, 0)), CAST(100 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001097CC3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(251 AS Decimal(18, 0)), CAST(63 AS Numeric(18, 0)), CAST(122 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001097CC5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(252 AS Decimal(18, 0)), CAST(64 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500109D8B7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(253 AS Decimal(18, 0)), CAST(64 AS Numeric(18, 0)), CAST(7 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500109D8B9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(254 AS Decimal(18, 0)), CAST(64 AS Numeric(18, 0)), CAST(71 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500109D8BB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(255 AS Decimal(18, 0)), CAST(64 AS Numeric(18, 0)), CAST(74 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500109D8BC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(256 AS Decimal(18, 0)), CAST(65 AS Numeric(18, 0)), CAST(36 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010A0D5C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(257 AS Decimal(18, 0)), CAST(65 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010A0D5D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(259 AS Decimal(18, 0)), CAST(66 AS Numeric(18, 0)), CAST(36 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010B5CE4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(260 AS Decimal(18, 0)), CAST(66 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010B5CE6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(261 AS Decimal(18, 0)), CAST(66 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010B5CE8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(262 AS Decimal(18, 0)), CAST(66 AS Numeric(18, 0)), CAST(135 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010B5CEA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(263 AS Decimal(18, 0)), CAST(66 AS Numeric(18, 0)), CAST(136 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010B5CEB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(264 AS Decimal(18, 0)), CAST(67 AS Numeric(18, 0)), CAST(36 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BB1AC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(265 AS Decimal(18, 0)), CAST(67 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BB1AE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(266 AS Decimal(18, 0)), CAST(67 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BB1B1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(267 AS Decimal(18, 0)), CAST(67 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BB1B5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(268 AS Decimal(18, 0)), CAST(57 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BEF6C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(269 AS Decimal(18, 0)), CAST(57 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BEF6D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(270 AS Decimal(18, 0)), CAST(57 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BEF6F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(271 AS Decimal(18, 0)), CAST(57 AS Numeric(18, 0)), CAST(59 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BEF71 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(272 AS Decimal(18, 0)), CAST(57 AS Numeric(18, 0)), CAST(80 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BEF73 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(273 AS Decimal(18, 0)), CAST(57 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BEF75 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(274 AS Decimal(18, 0)), CAST(57 AS Numeric(18, 0)), CAST(132 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010BEF77 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(275 AS Decimal(18, 0)), CAST(68 AS Numeric(18, 0)), CAST(6 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C55D4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(276 AS Decimal(18, 0)), CAST(68 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C55D6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(277 AS Decimal(18, 0)), CAST(68 AS Numeric(18, 0)), CAST(87 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C55D7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(278 AS Decimal(18, 0)), CAST(68 AS Numeric(18, 0)), CAST(93 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C55D9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(279 AS Decimal(18, 0)), CAST(68 AS Numeric(18, 0)), CAST(98 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C55DA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(280 AS Decimal(18, 0)), CAST(69 AS Numeric(18, 0)), CAST(6 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C96ED AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(281 AS Decimal(18, 0)), CAST(69 AS Numeric(18, 0)), CAST(72 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C96EF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(282 AS Decimal(18, 0)), CAST(69 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C96F0 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(283 AS Decimal(18, 0)), CAST(69 AS Numeric(18, 0)), CAST(84 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010C96F2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(284 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D5831 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(285 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D5832 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(286 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D5833 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(287 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D5835 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(288 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(71 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D5836 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(289 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D5839 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(290 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(95 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D583A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(291 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D5849 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(292 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(126 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D584A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(293 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(127 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D584B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(294 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D584D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(295 AS Decimal(18, 0)), CAST(70 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010D584E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(296 AS Decimal(18, 0)), CAST(72 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010DBA50 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(297 AS Decimal(18, 0)), CAST(72 AS Numeric(18, 0)), CAST(33 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010DBA52 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(298 AS Decimal(18, 0)), CAST(73 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010DFB72 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(299 AS Decimal(18, 0)), CAST(73 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010DFB73 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(300 AS Decimal(18, 0)), CAST(73 AS Numeric(18, 0)), CAST(98 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010DFB75 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(301 AS Decimal(18, 0)), CAST(74 AS Numeric(18, 0)), CAST(19 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010E8653 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(302 AS Decimal(18, 0)), CAST(74 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010E8655 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(303 AS Decimal(18, 0)), CAST(74 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010E865E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(304 AS Decimal(18, 0)), CAST(74 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010E865F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(305 AS Decimal(18, 0)), CAST(74 AS Numeric(18, 0)), CAST(136 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010E8661 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(306 AS Decimal(18, 0)), CAST(75 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010F1DFB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(307 AS Decimal(18, 0)), CAST(75 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010F1E04 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(308 AS Decimal(18, 0)), CAST(75 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010F1E05 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(309 AS Decimal(18, 0)), CAST(75 AS Numeric(18, 0)), CAST(94 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010F1E07 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(310 AS Decimal(18, 0)), CAST(76 AS Numeric(18, 0)), CAST(81 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010F3965 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(311 AS Decimal(18, 0)), CAST(77 AS Numeric(18, 0)), CAST(57 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010F967B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(312 AS Decimal(18, 0)), CAST(78 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010FF7CF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(313 AS Decimal(18, 0)), CAST(78 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010FF7D1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(314 AS Decimal(18, 0)), CAST(78 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010FF7D3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(315 AS Decimal(18, 0)), CAST(78 AS Numeric(18, 0)), CAST(59 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850010FF7D5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(316 AS Decimal(18, 0)), CAST(79 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001101DE2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(317 AS Decimal(18, 0)), CAST(79 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001101DE3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(318 AS Decimal(18, 0)), CAST(80 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500110356E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(319 AS Decimal(18, 0)), CAST(80 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500110356F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(320 AS Decimal(18, 0)), CAST(81 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001106C20 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(321 AS Decimal(18, 0)), CAST(81 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001106C22 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(322 AS Decimal(18, 0)), CAST(82 AS Numeric(18, 0)), CAST(52 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850011081D8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(323 AS Decimal(18, 0)), CAST(83 AS Numeric(18, 0)), CAST(77 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850011096AF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(324 AS Decimal(18, 0)), CAST(84 AS Numeric(18, 0)), CAST(61 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500110D371 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(325 AS Decimal(18, 0)), CAST(84 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500110D372 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(326 AS Decimal(18, 0)), CAST(85 AS Numeric(18, 0)), CAST(68 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500110F142 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(327 AS Decimal(18, 0)), CAST(85 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500110F144 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(328 AS Decimal(18, 0)), CAST(86 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500111593E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(329 AS Decimal(18, 0)), CAST(86 AS Numeric(18, 0)), CAST(84 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001115940 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(330 AS Decimal(18, 0)), CAST(86 AS Numeric(18, 0)), CAST(93 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001115941 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(331 AS Decimal(18, 0)), CAST(89 AS Numeric(18, 0)), CAST(93 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001119617 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(336 AS Decimal(18, 0)), CAST(90 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001122A72 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(337 AS Decimal(18, 0)), CAST(90 AS Numeric(18, 0)), CAST(68 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001122A74 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(334 AS Decimal(18, 0)), CAST(91 AS Numeric(18, 0)), CAST(68 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500111CE7A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(335 AS Decimal(18, 0)), CAST(91 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500111CE7C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(338 AS Decimal(18, 0)), CAST(90 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001122A76 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(339 AS Decimal(18, 0)), CAST(92 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001132746 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(340 AS Decimal(18, 0)), CAST(92 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001132748 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(341 AS Decimal(18, 0)), CAST(92 AS Numeric(18, 0)), CAST(58 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001132749 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(356 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(6 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B52 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(357 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B54 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(358 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(25 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B55 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(359 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(28 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B57 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(360 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(41 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B58 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(361 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(64 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B5A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(362 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(69 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B5C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(363 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(70 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B5E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(364 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(74 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B5F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(365 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B61 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(366 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(82 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B62 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(373 AS Decimal(18, 0)), CAST(95 AS Numeric(18, 0)), CAST(142 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001374B6F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(383 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDA6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(384 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(40 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDA7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(385 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(72 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDA9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(386 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDAB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(387 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(82 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDAD AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(388 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDAE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(389 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDB0 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(390 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDB2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(391 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDB4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(392 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(144 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDB5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(393 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(145 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDB7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(394 AS Decimal(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(149 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138CDB9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(412 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(6 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F24 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(413 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(11 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F25 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(414 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F28 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(415 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(25 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F2A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(416 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(43 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F2C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(417 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(70 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F2E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(418 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F30 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(419 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F32 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(420 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(82 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F34 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(421 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(87 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F37 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(422 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(91 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F38 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(423 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(116 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F3B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(424 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F3D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(425 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F40 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(426 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(146 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F41 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(427 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(147 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F43 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(428 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(148 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F55 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(429 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(150 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F56 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(430 AS Decimal(18, 0)), CAST(97 AS Numeric(18, 0)), CAST(151 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A6F58 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(433 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(45 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE520 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(434 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE522 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(435 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE526 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(436 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(72 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE529 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(437 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE52B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(438 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE52D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(439 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE52F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(440 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE530 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(441 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE533 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(442 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE535 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(443 AS Decimal(18, 0)), CAST(98 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016DE538 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(459 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(12 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F9F98 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(460 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F9F9A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(461 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(31 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F9F9C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(462 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(34 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F9FB6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(463 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(40 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F9FB8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(464 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F9FBB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(465 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(70 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA02F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(466 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA032 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(467 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA034 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(468 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA037 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(469 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA039 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(470 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA03A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(471 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(145 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA03D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(472 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(146 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA03F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(473 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(148 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA042 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(474 AS Decimal(18, 0)), CAST(99 AS Numeric(18, 0)), CAST(152 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850016FA044 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(475 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(12 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707B99 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(476 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707B9A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(477 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(31 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707B9D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(478 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(34 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707B9E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(479 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(43 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BA1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(480 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BA3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(481 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(70 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BA5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(482 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(81 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BA7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(483 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BAA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(484 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BAD AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(485 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BAF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(486 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BB1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(487 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(145 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BB3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(488 AS Decimal(18, 0)), CAST(100 AS Numeric(18, 0)), CAST(152 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001707BB5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(489 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C81 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(490 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(44 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C82 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(491 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(46 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C84 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(492 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(61 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C85 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(493 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C87 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(494 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C89 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(495 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C8A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(496 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C8C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(497 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C8E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(498 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C90 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(499 AS Decimal(18, 0)), CAST(101 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001713C91 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(514 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001726678 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(515 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(24 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500172667A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(516 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(33 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500172667B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(517 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(44 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500172667D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(518 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(61 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500172667E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(519 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001726680 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(520 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(84 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001726682 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(521 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001726683 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(522 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001726686 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(523 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001726687 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(524 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001726689 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(525 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(134 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500172668A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(526 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500172668C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(527 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500172668E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(528 AS Decimal(18, 0)), CAST(102 AS Numeric(18, 0)), CAST(153 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001726690 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(539 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(36 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A0A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(540 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(44 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A0B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(541 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A0D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(542 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(69 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A0E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(543 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(70 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A10 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(544 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A11 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(545 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(82 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A12 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(546 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(116 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A14 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(547 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A15 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(548 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(150 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A17 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(549 AS Decimal(18, 0)), CAST(103 AS Numeric(18, 0)), CAST(154 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001734A18 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(550 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3D8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(551 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(24 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3DA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(552 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(46 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3DC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(553 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(61 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3DD AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(554 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3DF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(555 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(84 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3E0 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(556 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3E2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(557 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3E4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(558 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3E5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(559 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3E7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(560 AS Decimal(18, 0)), CAST(104 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500173D3E8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(566 AS Decimal(18, 0)), CAST(105 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001752923 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(567 AS Decimal(18, 0)), CAST(105 AS Numeric(18, 0)), CAST(91 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001752924 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(568 AS Decimal(18, 0)), CAST(105 AS Numeric(18, 0)), CAST(120 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001752926 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(569 AS Decimal(18, 0)), CAST(105 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001752928 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(570 AS Decimal(18, 0)), CAST(105 AS Numeric(18, 0)), CAST(150 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175292A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(571 AS Decimal(18, 0)), CAST(105 AS Numeric(18, 0)), CAST(155 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175292B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(572 AS Decimal(18, 0)), CAST(105 AS Numeric(18, 0)), CAST(156 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175292D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(573 AS Decimal(18, 0)), CAST(105 AS Numeric(18, 0)), CAST(157 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175292E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(574 AS Decimal(18, 0)), CAST(105 AS Numeric(18, 0)), CAST(158 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001752930 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(575 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A96C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(576 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(40 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A96E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(577 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A96F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(578 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A971 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(579 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A972 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(580 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(87 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A974 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(581 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A981 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(582 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A984 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(583 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A986 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(584 AS Decimal(18, 0)), CAST(106 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500175A988 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(585 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B932 AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(586 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(40 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B93A AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(587 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B93B AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(588 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B93C AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(589 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B93E AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(590 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(87 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B93F AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(591 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B941 AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(592 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B942 AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(593 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B944 AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(594 AS Decimal(18, 0)), CAST(107 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500175B945 AS DateTime), N'Admin', CAST(0x0000A8500175CA8D AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(595 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(26 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001768499 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(596 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(27 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500176849B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(597 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(41 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500176849C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(598 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(64 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500176849E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(599 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(69 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684A1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(600 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(70 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684A3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(601 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(74 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684A4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(602 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684A6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(603 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684A8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(604 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(82 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684A9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(605 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(99 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684AB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(606 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(106 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684AD AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(607 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(108 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684AF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(608 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(109 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684B1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(609 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684B4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(610 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(142 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684B5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(611 AS Decimal(18, 0)), CAST(108 AS Numeric(18, 0)), CAST(143 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017684B7 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(626 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(40 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2D0 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(627 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2D3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(628 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2D6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(629 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2D8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(630 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2D9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(631 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(82 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2DB AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(632 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(93 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2DC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(633 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2DF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(634 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2E0 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(635 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2E2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(636 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2E4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(637 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2E6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(638 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2E8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(639 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(149 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2EA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(640 AS Decimal(18, 0)), CAST(109 AS Numeric(18, 0)), CAST(161 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177A2EC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(641 AS Decimal(18, 0)), CAST(110 AS Numeric(18, 0)), CAST(48 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177F68E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(642 AS Decimal(18, 0)), CAST(111 AS Numeric(18, 0)), CAST(47 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001780CD1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(653 AS Decimal(18, 0)), CAST(112 AS Numeric(18, 0)), CAST(114 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001791CF9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(654 AS Decimal(18, 0)), CAST(112 AS Numeric(18, 0)), CAST(157 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001791CFA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(655 AS Decimal(18, 0)), CAST(112 AS Numeric(18, 0)), CAST(158 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001791CFC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(656 AS Decimal(18, 0)), CAST(112 AS Numeric(18, 0)), CAST(163 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001791CFE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(657 AS Decimal(18, 0)), CAST(112 AS Numeric(18, 0)), CAST(164 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001791CFF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(648 AS Decimal(18, 0)), CAST(113 AS Numeric(18, 0)), CAST(114 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500178BBDA AS DateTime), N'Admin', CAST(0x0000A8500178CEB6 AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(649 AS Decimal(18, 0)), CAST(113 AS Numeric(18, 0)), CAST(157 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500178BBDC AS DateTime), N'Admin', CAST(0x0000A8500178CEB6 AS DateTime))
GO
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(650 AS Decimal(18, 0)), CAST(113 AS Numeric(18, 0)), CAST(158 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500178BBDF AS DateTime), N'Admin', CAST(0x0000A8500178CEB6 AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(651 AS Decimal(18, 0)), CAST(113 AS Numeric(18, 0)), CAST(163 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500178BBE1 AS DateTime), N'Admin', CAST(0x0000A8500178CEB6 AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(652 AS Decimal(18, 0)), CAST(113 AS Numeric(18, 0)), CAST(164 AS Numeric(18, 0)), NULL, NULL, 1, N'Admin', CAST(0x0000A8500178BBE3 AS DateTime), N'Admin', CAST(0x0000A8500178CEB6 AS DateTime))
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(658 AS Decimal(18, 0)), CAST(112 AS Numeric(18, 0)), CAST(165 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001791D01 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(659 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(36 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C27 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(660 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C29 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(661 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(69 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C2B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(662 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(70 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C2D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(663 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C2F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(664 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(82 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C31 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(665 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(116 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C34 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(666 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(120 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C36 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(667 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(150 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C37 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(668 AS Decimal(18, 0)), CAST(114 AS Numeric(18, 0)), CAST(154 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001799C39 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(669 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(23 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65B3 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(670 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(44 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65B5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(671 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(46 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65B6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(672 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(61 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65BA AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(673 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65BC AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(674 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(84 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65BE AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(675 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65BF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(676 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65C1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(677 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65C2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(678 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65C4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(679 AS Decimal(18, 0)), CAST(115 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017A65C6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(688 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B56FF AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(689 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(71 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B5702 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(690 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(72 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B5704 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(691 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B5705 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(692 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(120 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B5707 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(693 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B5709 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(694 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(135 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B570B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(695 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(136 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B570D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(696 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(167 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B570E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(697 AS Decimal(18, 0)), CAST(116 AS Numeric(18, 0)), CAST(168 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B5710 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(698 AS Decimal(18, 0)), CAST(118 AS Numeric(18, 0)), CAST(36 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017C7233 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(699 AS Decimal(18, 0)), CAST(118 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017C7234 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(700 AS Decimal(18, 0)), CAST(118 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017C7236 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(701 AS Decimal(18, 0)), CAST(118 AS Numeric(18, 0)), CAST(120 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017C723A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(702 AS Decimal(18, 0)), CAST(118 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017C723B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(703 AS Decimal(18, 0)), CAST(118 AS Numeric(18, 0)), CAST(140 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017C723D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(704 AS Decimal(18, 0)), CAST(118 AS Numeric(18, 0)), CAST(170 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017C723E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(719 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(40 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE492 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(720 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE494 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(721 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE495 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(722 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(71 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE497 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(723 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(75 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE498 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(724 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE49A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(725 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(93 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE49B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(726 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE49D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(727 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(97 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE49F AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(728 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(129 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE4A1 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(729 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(130 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE4A2 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(730 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(141 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE4A4 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(731 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(149 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE4A6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(732 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(162 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE4A8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(733 AS Decimal(18, 0)), CAST(119 AS Numeric(18, 0)), CAST(172 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A850017DE4A9 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(754 AS Decimal(18, 0)), CAST(120 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500183340D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(755 AS Decimal(18, 0)), CAST(120 AS Numeric(18, 0)), CAST(68 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500183340E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(740 AS Decimal(18, 0)), CAST(121 AS Numeric(18, 0)), CAST(31 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001819E98 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(741 AS Decimal(18, 0)), CAST(121 AS Numeric(18, 0)), CAST(65 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001819E99 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(742 AS Decimal(18, 0)), CAST(121 AS Numeric(18, 0)), CAST(67 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001819E9B AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(743 AS Decimal(18, 0)), CAST(121 AS Numeric(18, 0)), CAST(68 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001819E9D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(744 AS Decimal(18, 0)), CAST(7 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001826A13 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(745 AS Decimal(18, 0)), CAST(7 AS Numeric(18, 0)), CAST(79 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001826A14 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(746 AS Decimal(18, 0)), CAST(7 AS Numeric(18, 0)), CAST(81 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001826A16 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(747 AS Decimal(18, 0)), CAST(7 AS Numeric(18, 0)), CAST(82 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001826A19 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(748 AS Decimal(18, 0)), CAST(7 AS Numeric(18, 0)), CAST(83 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001826A1A AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(749 AS Decimal(18, 0)), CAST(7 AS Numeric(18, 0)), CAST(87 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001826A1C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(750 AS Decimal(18, 0)), CAST(7 AS Numeric(18, 0)), CAST(123 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001826A1D AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(751 AS Decimal(18, 0)), CAST(8 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500182C2E5 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(752 AS Decimal(18, 0)), CAST(8 AS Numeric(18, 0)), CAST(66 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500182C2E6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(753 AS Decimal(18, 0)), CAST(8 AS Numeric(18, 0)), CAST(84 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500182C2E8 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(756 AS Decimal(18, 0)), CAST(120 AS Numeric(18, 0)), CAST(173 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A85001833410 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(757 AS Decimal(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500183A41E AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(758 AS Decimal(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(50 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500183A422 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodDetail] ([Id], [FoodId], [MaterialId], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(759 AS Decimal(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(72 AS Numeric(18, 0)), NULL, NULL, NULL, N'Admin', CAST(0x0000A8500183A424 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[FoodDetail] OFF
SET IDENTITY_INSERT [dbo].[FoodTypes] ON 

INSERT [dbo].[FoodTypes] ([Id], [FoodTypesCode], [FoodTypesName], [IsSession], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(1 AS Decimal(18, 0)), N'A00000001', N'Món Mặn', 2, NULL, NULL, NULL, N'Admin', CAST(0x0000A82F00C00999 AS DateTime), N'Admin', CAST(0x0000A85000FF0C94 AS DateTime))
INSERT [dbo].[FoodTypes] ([Id], [FoodTypesCode], [FoodTypesName], [IsSession], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(2 AS Decimal(18, 0)), N'A00000002', N'Món Canh', 2, NULL, NULL, NULL, N'Admin', CAST(0x0000A82F00C01C70 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodTypes] ([Id], [FoodTypesCode], [FoodTypesName], [IsSession], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(3 AS Decimal(18, 0)), N'A00000003', N'Đồ Xào', 2, NULL, NULL, NULL, N'Admin', CAST(0x0000A82F00C029DB AS DateTime), N'Admin', CAST(0x0000A82F00C051B9 AS DateTime))
INSERT [dbo].[FoodTypes] ([Id], [FoodTypesCode], [FoodTypesName], [IsSession], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(4 AS Decimal(18, 0)), N'A00000004', N'Tráng Miệng', 2, NULL, NULL, NULL, N'Admin', CAST(0x0000A82F00C0414C AS DateTime), NULL, NULL)
INSERT [dbo].[FoodTypes] ([Id], [FoodTypesCode], [FoodTypesName], [IsSession], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(5 AS Decimal(18, 0)), N'A00000005', N'Ăn Xế', 3, NULL, NULL, NULL, N'Admin', CAST(0x0000A82F00C049D6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodTypes] ([Id], [FoodTypesCode], [FoodTypesName], [IsSession], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(6 AS Decimal(18, 0)), N'A00000006', N'Hàng kho', 1, NULL, NULL, NULL, N'Admin', CAST(0x0000A82F00C049D6 AS DateTime), NULL, NULL)
INSERT [dbo].[FoodTypes] ([Id], [FoodTypesCode], [FoodTypesName], [IsSession], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(7 AS Decimal(18, 0)), N'A00000007', N'Món nước', NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500113F94E AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[FoodTypes] OFF
SET IDENTITY_INSERT [dbo].[GroupMaterial] ON 

INSERT [dbo].[GroupMaterial] ([Id], [GroupMaterialCode], [GroupMaterialName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(1 AS Decimal(18, 0)), N'A00000001', N'Rau', NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00904045 AS DateTime), NULL, NULL)
INSERT [dbo].[GroupMaterial] ([Id], [GroupMaterialCode], [GroupMaterialName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(2 AS Decimal(18, 0)), N'A00000002', N'Ngũ cốc', NULL, NULL, NULL, N'Admin', CAST(0x0000A84C0090528D AS DateTime), NULL, NULL)
INSERT [dbo].[GroupMaterial] ([Id], [GroupMaterialCode], [GroupMaterialName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(3 AS Decimal(18, 0)), N'A00000003', N'Thịt, cá, trứng', NULL, NULL, NULL, N'Admin', CAST(0x0000A84C0090605B AS DateTime), N'Admin', CAST(0x0000A84C00A8F7E7 AS DateTime))
INSERT [dbo].[GroupMaterial] ([Id], [GroupMaterialCode], [GroupMaterialName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(4 AS Decimal(18, 0)), N'A00000004', N'Gia vị', NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00909B9D AS DateTime), NULL, NULL)
INSERT [dbo].[GroupMaterial] ([Id], [GroupMaterialCode], [GroupMaterialName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(5 AS Decimal(18, 0)), N'A00000005', N'Tráng miệng', NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00ABB929 AS DateTime), NULL, NULL)
INSERT [dbo].[GroupMaterial] ([Id], [GroupMaterialCode], [GroupMaterialName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(6 AS Decimal(18, 0)), N'A00000006', N'Dụng cụ vệ sinh', NULL, NULL, 1, N'Admin', CAST(0x0000A84D00A9963A AS DateTime), N'Admin', CAST(0x0000A84D00A9B2F5 AS DateTime))
SET IDENTITY_INSERT [dbo].[GroupMaterial] OFF
SET IDENTITY_INSERT [dbo].[Material] ON 

INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(1 AS Decimal(18, 0)), N'A00000001', N'Má đùi', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(25000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A75F59 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(2 AS Decimal(18, 0)), N'A00000002', N'Đùi tỏi 4-5 cái/kg', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, CAST(38000.000 AS Decimal(18, 3)), N'4-5 cái/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AB7B3F AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(3 AS Decimal(18, 0)), N'A00000003', N'Đùi', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AB68BC AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(4 AS Decimal(18, 0)), N'A00000004', N'Cá basa fille', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(10 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ADCF6F AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(5 AS Decimal(18, 0)), N'A00000005', N'Cá trứng', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), N'45-50 con/kg, 20 kg/thùng', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A7E319 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(6 AS Decimal(18, 0)), N'A00000006', N'Nạc mông Allana MS 45', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, CAST(102000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A7F32D AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(7 AS Decimal(18, 0)), N'A00000007', N'Chả cá sống', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(43000.000 AS Decimal(18, 3)), N'10kg/ b?ch', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A80AE3 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(8 AS Decimal(18, 0)), N'A00000008', N'Chả cá hấp 1', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(44000.000 AS Decimal(18, 3)), N'Bánh vuông (cây tròn) 500 gr', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A81ECB AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(9 AS Decimal(18, 0)), N'A00000009', N'Cá viên', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(44000.000 AS Decimal(18, 3)), N'140 viên/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A83205 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(10 AS Decimal(18, 0)), N'A00000010', N'Cá basa cắt khúc', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(10 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A844B0 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(11 AS Decimal(18, 0)), N'A00000011', N'Chả lụa', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(66000.000 AS Decimal(18, 3)), N'Lo?i 1 (500gr/cây)', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A85410 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(12 AS Decimal(18, 0)), N'A00000012', N'Chả quế hấp', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(58000.000 AS Decimal(18, 3)), N'bánh vuông 500gr', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A863CA AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(13 AS Decimal(18, 0)), N'A00000013', N'Lạp xưởng tươi', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(70000.000 AS Decimal(18, 3)), N'30 cây/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A8715C AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(14 AS Decimal(18, 0)), N'A00000014', N'Trứng gà', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(11 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), N'', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A8E533 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(15 AS Decimal(18, 0)), N'A00000015', N'Đậu hũ chiên', CAST(3 AS Numeric(18, 0)), CAST(17 AS Numeric(18, 0)), CAST(12 AS Numeric(18, 0)), NULL, CAST(1500.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84D00AEE2F9 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(16 AS Decimal(18, 0)), N'A00000016', N'Đậu hũ trắng', CAST(3 AS Numeric(18, 0)), CAST(17 AS Numeric(18, 0)), CAST(12 AS Numeric(18, 0)), NULL, CAST(1500.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84D00AECC19 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(17 AS Decimal(18, 0)), N'A00000017', N'Cốt lết', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), N'10 mi?ng/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A9DD51 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(18 AS Decimal(18, 0)), N'A00000018', N'Xúc xích heo', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(62000.000 AS Decimal(18, 3)), N'30 ho?c 50 cây/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AA6FC6 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(19 AS Decimal(18, 0)), N'A00000019', N'Sườn heo Animex', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), N'10kg/thùng', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AD0550 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(20 AS Decimal(18, 0)), N'A00000020', N'Sườn cốt lết', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AD980F AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(21 AS Decimal(18, 0)), N'A00000021', N'Xương đuôi', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A88502 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(22 AS Decimal(18, 0)), N'A00000022', N'Bắp giò', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ACF7ED AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(23 AS Decimal(18, 0)), N'A00000023', N'Xương giá', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AD232D AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(24 AS Decimal(18, 0)), N'A00000024', N'Xương ống', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AD3265 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(25 AS Decimal(18, 0)), N'A00000025', N'Xương bò', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AD44E8 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(26 AS Decimal(18, 0)), N'A00000026', N'Xương đầu gà', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AD5A5E AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(27 AS Decimal(18, 0)), N'A00000027', N'Ức gà phille', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(16 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ADEA18 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(28 AS Decimal(18, 0)), N'A00000028', N'Bò viên', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(60000.000 AS Decimal(18, 3)), N'80 viên/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00A79345 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(29 AS Decimal(18, 0)), N'A00000029', N'Chạo thịt', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(52000.000 AS Decimal(18, 3)), N'38-40 cây/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ACE4A1 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(30 AS Decimal(18, 0)), N'A00000030', N'Đùi tỏi 7-8 cái/kg', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, CAST(42000.000 AS Decimal(18, 3)), N'7-8 cái/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AD1522 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(31 AS Decimal(18, 0)), N'A00000031', N'Cua xay', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(13 AS Numeric(18, 0)), NULL, CAST(50000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AA2C05 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(32 AS Decimal(18, 0)), N'A00000032', N'Hến', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(13 AS Numeric(18, 0)), NULL, CAST(50000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AA42E9 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(33 AS Decimal(18, 0)), N'A00000033', N'Trứng cút', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(11 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AA5644 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(34 AS Decimal(18, 0)), N'A00000034', N'Đậu hũ viên chiên', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(11 AS Numeric(18, 0)), NULL, CAST(15000.000 AS Decimal(18, 3)), N'95 viên/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84D00AED408 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(35 AS Decimal(18, 0)), N'A00000035', N'Tim heo', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, CAST(0.000 AS Decimal(18, 3)), N'10kg/thùng', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AA9911 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(36 AS Decimal(18, 0)), N'A00000036', N'Nạm MS 11', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, CAST(86000.000 AS Decimal(18, 3)), N'18kg/thùng', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AAA852 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(37 AS Decimal(18, 0)), N'A00000037', N'Cá viên trứng cút', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(50000.000 AS Decimal(18, 3)), N'42-45 viên/kg (1/2 tr?ng)', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AAC1F0 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(38 AS Decimal(18, 0)), N'A00000038', N'Chạo cá', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(44000.000 AS Decimal(18, 3)), N'38-40 cây/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AAD5C0 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(39 AS Decimal(18, 0)), N'A00000039', N'Cá ngũ vị', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(44000.000 AS Decimal(18, 3)), N'120 mi?ng tam giác/kg', NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AAE3AF AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(40 AS Decimal(18, 0)), N'A00000040', N'Bún tươi', CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, CAST(6800.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ADAB47 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(41 AS Decimal(18, 0)), N'A00000041', N'Bánh phở', CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, CAST(7000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ADB9AE AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(42 AS Decimal(18, 0)), N'A00000042', N'Bánh canh', CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, CAST(7000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ABDE96 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(43 AS Decimal(18, 0)), N'A00000043', N'Bún bò', CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, CAST(7000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ABF327 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(44 AS Decimal(18, 0)), N'A00000044', N'Hủ tíu dốt', CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(15 AS Numeric(18, 0)), NULL, CAST(14000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00AC44FC AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(45 AS Decimal(18, 0)), N'A00000045', N'Nui', CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(14000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ACC348 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(46 AS Decimal(18, 0)), N'A00000046', N'Mì tươi', CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, CAST(15000.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ACD41C AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(47 AS Decimal(18, 0)), N'A00000047', N'Chuối', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(48 AS Decimal(18, 0)), N'A00000048', N'Dưa hấu', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(49 AS Decimal(18, 0)), N'A00000049', N'Yaourt bịch', CAST(5 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(7 AS Numeric(18, 0)), NULL, CAST(650.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84C00ABCC84 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(50 AS Decimal(18, 0)), N'A00000050', N'Bí xanh', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A832014BFC66 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(51 AS Decimal(18, 0)), N'A00000051', N'Bí đỏ', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(52 AS Decimal(18, 0)), N'A00000052', N'Bầu', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(53 AS Decimal(18, 0)), N'A00000053', N'Khoai mỡ', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(54 AS Decimal(18, 0)), N'A00000054', N'Bắp cải', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(55 AS Decimal(18, 0)), N'A00000055', N'Su su', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(56 AS Decimal(18, 0)), N'A00000056', N'Cà rốt', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(57 AS Decimal(18, 0)), N'A00000057', N'Đậu đũa', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(58 AS Decimal(18, 0)), N'A00000058', N'Su hào', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(59 AS Decimal(18, 0)), N'A00000059', N'Đậu que', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(60 AS Decimal(18, 0)), N'A00000060', N'Củ sắn', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(61 AS Decimal(18, 0)), N'A00000061', N'Hẹ lá', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(62 AS Decimal(18, 0)), N'A00000062', N'Cải ngọt', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(63 AS Decimal(18, 0)), N'A00000063', N'Cải xanh', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(64 AS Decimal(18, 0)), N'A00000064', N'Gừng', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(65 AS Decimal(18, 0)), N'A00000065', N'Mồng tơi', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(66 AS Decimal(18, 0)), N'A00000066', N'Cà chua', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(67 AS Decimal(18, 0)), N'A00000067', N'Rau dền', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(68 AS Decimal(18, 0)), N'A00000068', N'Mướp', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(69 AS Decimal(18, 0)), N'A00000069', N'Rau ôm', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(70 AS Decimal(18, 0)), N'A00000070', N'Ngò gai', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(71 AS Decimal(18, 0)), N'A00000071', N'Hành lá', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(72 AS Decimal(18, 0)), N'A00000072', N'Ngò rí', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(73 AS Decimal(18, 0)), N'A00000073', N'Tỏi', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(74 AS Decimal(18, 0)), N'A00000074', N'Hành tím củ', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(75 AS Decimal(18, 0)), N'A00000075', N'Hành tây', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(76 AS Decimal(18, 0)), N'A00000076', N'Củ dền', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(77 AS Decimal(18, 0)), N'A00000077', N'Cải thìa', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(78 AS Decimal(18, 0)), N'A00000078', N'Đu đủ xanh', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(79 AS Decimal(18, 0)), N'A00000079', N'Giá', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(80 AS Decimal(18, 0)), N'A00000080', N'Khoai tây', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(81 AS Decimal(18, 0)), N'A00000081', N'Rau muống', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(82 AS Decimal(18, 0)), N'A00000082', N'Rau quế', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(83 AS Decimal(18, 0)), N'A00000083', N'Me', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(84 AS Decimal(18, 0)), N'A00000084', N'Cần tàu', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(85 AS Decimal(18, 0)), N'A00000085', N'Lá giang', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(86 AS Decimal(18, 0)), N'A00000086', N'Cải chua', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(87 AS Decimal(18, 0)), N'A00000087', N'Thơm', CAST(1 AS Numeric(18, 0)), CAST(15 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, N'Admin', CAST(0x0000A84D00A894A6 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(88 AS Decimal(18, 0)), N'A00000088', N'Củ dền', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, 1, N'Admin', NULL, N'Admin', CAST(0x0000A84D00AE5ED9 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(89 AS Decimal(18, 0)), N'A00000089', N'Bạc hà', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(90 AS Decimal(18, 0)), N'A00000090', N'Sả bào', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(91 AS Decimal(18, 0)), N'A00000091', N'Sả xay', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(92 AS Decimal(18, 0)), N'A00000092', N'Nấm rơm', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(93 AS Decimal(18, 0)), N'A00000093', N'Dưa leo', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(94 AS Decimal(18, 0)), N'A00000094', N'Bông cải trắng', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, CAST(0.000 AS Decimal(18, 3)), NULL, NULL, NULL, N'Admin', NULL, NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(95 AS Decimal(18, 0)), N'A00000095', N'Trứng vịt', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(11 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AE126B AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(96 AS Decimal(18, 0)), N'A00000096', N'Nạc dăm', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AE3E13 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(97 AS Decimal(18, 0)), N'A00000097', N'Thịt nách', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AE54D4 AS DateTime), N'Admin', CAST(0x0000A84C00B0679F AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(98 AS Decimal(18, 0)), N'A00000098', N'Tương cà', CAST(4 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, NULL, NULL, N'Binh 5 lit', NULL, NULL, N'Admin', CAST(0x0000A84C00AE9207 AS DateTime), N'Admin', CAST(0x0000A84D00A8C913 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(99 AS Decimal(18, 0)), N'A00000099', N'Tương ớt', CAST(4 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, NULL, NULL, N'bình 5 lít', NULL, NULL, N'Admin', CAST(0x0000A84C00AE9456 AS DateTime), N'Admin', CAST(0x0000A84D00AF0BB7 AS DateTime))
GO
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(100 AS Decimal(18, 0)), N'A00000100', N'Nước tương bình', CAST(4 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, NULL, NULL, N'bình 5 lít', NULL, NULL, N'Admin', CAST(0x0000A84C00AEC596 AS DateTime), N'Admin', CAST(0x0000A84D00AF2A11 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(101 AS Decimal(18, 0)), N'A00000101', N'Dầu ăn Minh Huê', CAST(4 AS Numeric(18, 0)), CAST(13 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AED951 AS DateTime), N'Admin', CAST(0x0000A84D00AF36FC AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(102 AS Decimal(18, 0)), N'A00000102', N'Bột nghệ', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AF089E AS DateTime), N'Admin', CAST(0x0000A84C00B15088 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(103 AS Decimal(18, 0)), N'A00000103', N'Bột mì', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AF1CF6 AS DateTime), N'Admin', CAST(0x0000A84C00B15D2E AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(104 AS Decimal(18, 0)), N'A00000104', N'Bột năng', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AF2DE5 AS DateTime), N'Admin', CAST(0x0000A84C00B16FB0 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(105 AS Decimal(18, 0)), N'A00000105', N'Bột chiên giòn', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AF501A AS DateTime), N'Admin', CAST(0x0000A84C00B17A2C AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(106 AS Decimal(18, 0)), N'A00000106', N'Tương đen', CAST(4 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AF6833 AS DateTime), N'Admin', CAST(0x0000A84D00A8A4C8 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(107 AS Decimal(18, 0)), N'A00000107', N'Dầu hào', CAST(4 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AF80B8 AS DateTime), N'Admin', CAST(0x0000A84D00A8E191 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(108 AS Decimal(18, 0)), N'A00000108', N'Hoa quế', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AFA711 AS DateTime), N'Admin', CAST(0x0000A84C00B1B865 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(109 AS Decimal(18, 0)), N'A00000109', N'Thảo quả', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AFB9F6 AS DateTime), N'Admin', CAST(0x0000A84C00B1C3F3 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(110 AS Decimal(18, 0)), N'A00000110', N'Hạt nêm Knor', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AFFFDB AS DateTime), N'Admin', CAST(0x0000A84C00B1AD1D AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(111 AS Decimal(18, 0)), N'A00000111', N'Bột ngọt', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00B01575 AS DateTime), N'Admin', CAST(0x0000A84C00B14391 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(112 AS Decimal(18, 0)), N'A00000112', N'Muối', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00B026AC AS DateTime), N'Admin', CAST(0x0000A84C00B13714 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(113 AS Decimal(18, 0)), N'A00000113', N'Đường vàng', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00B03B22 AS DateTime), N'Admin', CAST(0x0000A84C00B18433 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(114 AS Decimal(18, 0)), N'A00000114', N'Đường trắng', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00B08135 AS DateTime), N'Admin', CAST(0x0000A84C00B18D87 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(115 AS Decimal(18, 0)), N'A00000115', N'Nước mắm', CAST(4 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00B0A674 AS DateTime), N'Admin', CAST(0x0000A84D00A83948 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(116 AS Decimal(18, 0)), N'A00000116', N'Hạt điều màu', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00B0C8E6 AS DateTime), N'Admin', CAST(0x0000A84C00B1A0DD AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(117 AS Decimal(18, 0)), N'A00000117', N'Bột cari', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00B0E23B AS DateTime), N'Admin', CAST(0x0000A84C00B19852 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(118 AS Decimal(18, 0)), N'A00000118', N'Bơ Tường An', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00B0FDBF AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(119 AS Decimal(18, 0)), N'A00000119', N'Bột chiên xù', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00B12A1D AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(120 AS Decimal(18, 0)), N'A00000120', N'Bánh mì', CAST(2 AS Numeric(18, 0)), CAST(11 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A815B9 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(121 AS Decimal(18, 0)), N'A00000121', N'Nước tương chai', CAST(4 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A8617D AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(122 AS Decimal(18, 0)), N'A00000122', N'Hắc xì dầu', CAST(4 AS Numeric(18, 0)), CAST(14 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A9004D AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(123 AS Decimal(18, 0)), N'A00000123', N'Tôm khô', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AB71DC AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(124 AS Decimal(18, 0)), N'A00000124', N'Cải thảo', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F3C920 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(125 AS Decimal(18, 0)), N'A00000125', N'Cải xoong', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F63D33 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(126 AS Decimal(18, 0)), N'A00000126', N'Nấm mèo', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F84360 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(127 AS Decimal(18, 0)), N'A00000127', N'Bún tàu', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F856E5 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(128 AS Decimal(18, 0)), N'A00000128', N'Thì là', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85000F8D044 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(129 AS Decimal(18, 0)), N'A00000129', N'Củ cải trắng', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FAFDEC AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(130 AS Decimal(18, 0)), N'A00000130', N'Ớt xay', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FE89B7 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(131 AS Decimal(18, 0)), N'A00000131', N'Ớt hiểm đỏ', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85000FED1E9 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(132 AS Decimal(18, 0)), N'A00000132', N'Bắp mỹ', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850010630D9 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(133 AS Decimal(18, 0)), N'A00000133', N'Tép ', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'120 con/kg', NULL, NULL, N'Admin', CAST(0x0000A8500106D31E AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(134 AS Decimal(18, 0)), N'A00000134', N'Tôm', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, N'80 con/kg', NULL, NULL, N'Admin', CAST(0x0000A8500106EBDB AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(135 AS Decimal(18, 0)), N'A00000135', N'Cà pas', CAST(4 AS Numeric(18, 0)), CAST(10 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850010A6BA0 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(136 AS Decimal(18, 0)), N'A00000136', N'Đậu ván', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850010AA3E7 AS DateTime), N'Admin', CAST(0x0000A850010E355D AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(137 AS Decimal(18, 0)), N'A00000137', N'Nạm trâu', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850010ABE2F AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(138 AS Decimal(18, 0)), N'A00000138', N'Dầu mè', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850010CAF36 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(139 AS Decimal(18, 0)), N'A00000139', N'Bắp non', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500112469A AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(140 AS Decimal(18, 0)), N'A00000140', N'Chanh', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001150C57 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(141 AS Decimal(18, 0)), N'A00000141', N'Salach caron', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001151F1A AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(142 AS Decimal(18, 0)), N'A00000142', N'Ớt sừng', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850011538A5 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(143 AS Decimal(18, 0)), N'A00000143', N'Hoa hồi', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850013772B3 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(144 AS Decimal(18, 0)), N'A00000144', N'Giò sống', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500138474C AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(145 AS Decimal(18, 0)), N'A00000145', N'Mắm tôm', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001385ED0 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(146 AS Decimal(18, 0)), N'A00000146', N'Bắp chuối bào', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001387278 AS DateTime), N'Admin', CAST(0x0000A85001391808 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(147 AS Decimal(18, 0)), N'A00000147', N'Mắm ruốc', CAST(4 AS Numeric(18, 0)), CAST(10 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850013877B2 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(148 AS Decimal(18, 0)), N'A00000148', N'Rau muống bào', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001388667 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(149 AS Decimal(18, 0)), N'A00000149', N'Rau thơm', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850013899D9 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(150 AS Decimal(18, 0)), N'A00000150', N'Sả cây', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A38F3 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(151 AS Decimal(18, 0)), N'A00000151', N'Rau râm', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850013A4643 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(152 AS Decimal(18, 0)), N'A00000152', N'Màu gạch tôm', CAST(4 AS Numeric(18, 0)), CAST(19 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F015B AS DateTime), N'Admin', CAST(0x0000A850016F81C4 AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(153 AS Decimal(18, 0)), N'A00000153', N'Cải xá bấu', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850017239EF AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(154 AS Decimal(18, 0)), N'A00000154', N'Gia vị bò kho', CAST(4 AS Numeric(18, 0)), CAST(18 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001732D36 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(155 AS Decimal(18, 0)), N'A00000155', N'Khoai lang vàng', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001744241 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(156 AS Decimal(18, 0)), N'A00000156', N'Nước cốt dừa', CAST(4 AS Numeric(18, 0)), CAST(16 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001745B23 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(157 AS Decimal(18, 0)), N'A00000157', N'Sữa đặc', CAST(4 AS Numeric(18, 0)), CAST(18 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500174730F AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(158 AS Decimal(18, 0)), N'A00000158', N'Sữa tươi không đường', CAST(4 AS Numeric(18, 0)), CAST(18 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850017489D0 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(159 AS Decimal(18, 0)), N'A00000159', N'Khoai lang tím', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001749F2A AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(160 AS Decimal(18, 0)), N'A00000160', N'Đường phèn', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500174C8E5 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(161 AS Decimal(18, 0)), N'A00000161', N'Diếp cá', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001774C04 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(162 AS Decimal(18, 0)), N'A00000162', N'Đậu phộng sống', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500177BEF0 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(163 AS Decimal(18, 0)), N'A00000163', N'Cà phê bột', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A85001784E12 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(164 AS Decimal(18, 0)), N'A00000164', N'Bột béo', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850017860CF AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(165 AS Decimal(18, 0)), N'A00000165', N'Bột rau câu dẻo', CAST(4 AS Numeric(18, 0)), CAST(18 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850017900E7 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(166 AS Decimal(18, 0)), N'A00000166', N'Rau húng cây', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500179B301 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(167 AS Decimal(18, 0)), N'A00000167', N'Đùi góc tư', CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B039B AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(168 AS Decimal(18, 0)), N'A00000168', N'Pate', CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B13EE AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(169 AS Decimal(18, 0)), N'A00000169', N'Cam sành', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850017B21D2 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(170 AS Decimal(18, 0)), N'A00000170', N'Gia vị lagu', CAST(4 AS Numeric(18, 0)), CAST(18 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850017BDDC0 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(171 AS Decimal(18, 0)), N'A00000171', N'Gia vị lẩu thái', CAST(4 AS Numeric(18, 0)), CAST(18 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A850017BE8DC AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(172 AS Decimal(18, 0)), N'A00000172', N'Chả giò', CAST(3 AS Numeric(18, 0)), CAST(22 AS Numeric(18, 0)), NULL, NULL, NULL, N'', NULL, NULL, N'Admin', CAST(0x0000A850017D3F3C AS DateTime), N'Admin', CAST(0x0000A850017DC67C AS DateTime))
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(173 AS Decimal(18, 0)), N'A00000173', N'Rau ngót', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500180B12C AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(174 AS Decimal(18, 0)), N'A00000174', N'Rau má', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500180BFEF AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(175 AS Decimal(18, 0)), N'A00000175', N'Tần ô', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500180D146 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(176 AS Decimal(18, 0)), N'A00000176', N'Cải bó xôi', CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8500180E1F1 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(177 AS Decimal(18, 0)), N'A00000177', N'Gạo', CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8510170B951 AS DateTime), NULL, NULL)
INSERT [dbo].[Material] ([Id], [MaterialCode], [MaterialName], [GroupMaterialId], [UnitId], [SupplierId], [VAT], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(178 AS Decimal(18, 0)), N'A00000178', N'Gạo tấm', CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A8510170CBD9 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Material] OFF
SET IDENTITY_INSERT [dbo].[MaterialsFifo] ON 

INSERT [dbo].[MaterialsFifo] ([Id], [DateFifo], [MaterialId], [QuantityReceived], [PriceReceived], [QuantityDelivery], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [ListIdReceived], [ImportWarehousingMaterialsId], [WarehouseId]) VALUES (CAST(1 AS Decimal(18, 0)), CAST(0x0000A81C00000000 AS DateTime), CAST(1 AS Numeric(18, 0)), CAST(39.6000 AS Numeric(18, 4)), CAST(7000 AS Numeric(18, 0)), CAST(19.8000 AS Numeric(18, 4)), NULL, NULL, N'Admin', CAST(0x0000A83B00F96B5C AS DateTime), N'Admin', CAST(0x0000A83B00FCB44A AS DateTime), N'2', CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[MaterialsFifo] ([Id], [DateFifo], [MaterialId], [QuantityReceived], [PriceReceived], [QuantityDelivery], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [ListIdReceived], [ImportWarehousingMaterialsId], [WarehouseId]) VALUES (CAST(2 AS Decimal(18, 0)), CAST(0x0000A81C00000000 AS DateTime), CAST(2 AS Numeric(18, 0)), CAST(6.6000 AS Numeric(18, 4)), CAST(5000 AS Numeric(18, 0)), CAST(3.3000 AS Numeric(18, 4)), NULL, NULL, N'Admin', CAST(0x0000A83B00F96B70 AS DateTime), N'Admin', CAST(0x0000A83B00FCB5BF AS DateTime), N'2', CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[MaterialsFifo] ([Id], [DateFifo], [MaterialId], [QuantityReceived], [PriceReceived], [QuantityDelivery], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [ListIdReceived], [ImportWarehousingMaterialsId], [WarehouseId]) VALUES (CAST(3 AS Decimal(18, 0)), CAST(0x0000A81C00000000 AS DateTime), CAST(26 AS Numeric(18, 0)), CAST(1650.0000 AS Numeric(18, 4)), CAST(5000 AS Numeric(18, 0)), CAST(1650.0000 AS Numeric(18, 4)), NULL, NULL, N'Admin', CAST(0x0000A83B00F96B8C AS DateTime), N'Admin', CAST(0x0000A83B00FCB8A8 AS DateTime), N'2', CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[MaterialsFifo] ([Id], [DateFifo], [MaterialId], [QuantityReceived], [PriceReceived], [QuantityDelivery], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [ListIdReceived], [ImportWarehousingMaterialsId], [WarehouseId]) VALUES (CAST(4 AS Decimal(18, 0)), CAST(0x0000A81C00000000 AS DateTime), CAST(55 AS Numeric(18, 0)), CAST(13.2000 AS Numeric(18, 4)), CAST(4000 AS Numeric(18, 0)), CAST(6.6000 AS Numeric(18, 4)), NULL, NULL, N'Admin', CAST(0x0000A83B00F96B9F AS DateTime), N'Admin', CAST(0x0000A83B00FCB8BC AS DateTime), N'2', CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[MaterialsFifo] ([Id], [DateFifo], [MaterialId], [QuantityReceived], [PriceReceived], [QuantityDelivery], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [ListIdReceived], [ImportWarehousingMaterialsId], [WarehouseId]) VALUES (CAST(5 AS Decimal(18, 0)), CAST(0x0000A81C00000000 AS DateTime), CAST(56 AS Numeric(18, 0)), CAST(6.6000 AS Numeric(18, 4)), CAST(4000 AS Numeric(18, 0)), CAST(3.3000 AS Numeric(18, 4)), NULL, NULL, N'Admin', CAST(0x0000A83B00F96BB5 AS DateTime), N'Admin', CAST(0x0000A83B00FCB8D1 AS DateTime), N'2', CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[MaterialsFifo] ([Id], [DateFifo], [MaterialId], [QuantityReceived], [PriceReceived], [QuantityDelivery], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [ListIdReceived], [ImportWarehousingMaterialsId], [WarehouseId]) VALUES (CAST(6 AS Decimal(18, 0)), CAST(0x0000A81C00000000 AS DateTime), CAST(57 AS Numeric(18, 0)), CAST(26.4000 AS Numeric(18, 4)), CAST(3000 AS Numeric(18, 0)), CAST(13.2000 AS Numeric(18, 4)), NULL, NULL, N'Admin', CAST(0x0000A83B00F96BCF AS DateTime), N'Admin', CAST(0x0000A83B00FCB8E9 AS DateTime), N'2', CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[MaterialsFifo] ([Id], [DateFifo], [MaterialId], [QuantityReceived], [PriceReceived], [QuantityDelivery], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [ListIdReceived], [ImportWarehousingMaterialsId], [WarehouseId]) VALUES (CAST(7 AS Decimal(18, 0)), CAST(0x0000A81C00000000 AS DateTime), CAST(58 AS Numeric(18, 0)), CAST(2640.0000 AS Numeric(18, 4)), CAST(2000 AS Numeric(18, 0)), CAST(1320.0000 AS Numeric(18, 4)), NULL, NULL, N'Admin', CAST(0x0000A83B00F96BE6 AS DateTime), N'Admin', CAST(0x0000A83B00FCB8FD AS DateTime), N'2', CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[MaterialsFifo] ([Id], [DateFifo], [MaterialId], [QuantityReceived], [PriceReceived], [QuantityDelivery], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [ListIdReceived], [ImportWarehousingMaterialsId], [WarehouseId]) VALUES (CAST(8 AS Decimal(18, 0)), CAST(0x0000A81C00000000 AS DateTime), CAST(59 AS Numeric(18, 0)), CAST(2706.0000 AS Numeric(18, 4)), CAST(10000 AS Numeric(18, 0)), CAST(1353.0000 AS Numeric(18, 4)), NULL, NULL, N'Admin', CAST(0x0000A83B00F96BFC AS DateTime), N'Admin', CAST(0x0000A83B00FCB90D AS DateTime), N'2', CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[MaterialsFifo] ([Id], [DateFifo], [MaterialId], [QuantityReceived], [PriceReceived], [QuantityDelivery], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [ListIdReceived], [ImportWarehousingMaterialsId], [WarehouseId]) VALUES (CAST(9 AS Decimal(18, 0)), CAST(0x0000A81C00000000 AS DateTime), CAST(85 AS Numeric(18, 0)), CAST(16830.0000 AS Numeric(18, 4)), CAST(10000 AS Numeric(18, 0)), CAST(16830.0000 AS Numeric(18, 4)), NULL, NULL, N'Admin', CAST(0x0000A83B00F96C19 AS DateTime), N'Admin', CAST(0x0000A83B00FCB91F AS DateTime), N'2', CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
SET IDENTITY_INSERT [dbo].[MaterialsFifo] OFF
SET IDENTITY_INSERT [dbo].[QuantitativeFood] ON 

INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (1, CAST(1 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), CAST(8.0000 AS Numeric(18, 4)), CAST(8.0000 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84F017AAD69 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (2, CAST(1 AS Numeric(18, 0)), CAST(50 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), CAST(8.0000 AS Numeric(18, 4)), CAST(8.0000 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84F017AAD6A AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (3, CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0020 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4E9F AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (4, CAST(2 AS Numeric(18, 0)), CAST(51 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0600 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EA1 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (5, CAST(3 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0020 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EA4 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (6, CAST(3 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0450 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EA6 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (7, CAST(4 AS Numeric(18, 0)), CAST(52 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0060 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EA9 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (8, CAST(5 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0020 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EAB AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (9, CAST(5 AS Numeric(18, 0)), CAST(62 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0450 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EAE AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (10, CAST(6 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0200 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EB3 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (11, CAST(6 AS Numeric(18, 0)), CAST(63 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0450 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EB7 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (12, CAST(15 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0020 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EB9 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (13, CAST(15 AS Numeric(18, 0)), CAST(54 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0100 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EBD AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (14, CAST(15 AS Numeric(18, 0)), CAST(55 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0250 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EDB AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (15, CAST(15 AS Numeric(18, 0)), CAST(56 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0100 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EDD AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (16, CAST(15 AS Numeric(18, 0)), CAST(76 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0050 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EDF AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (17, CAST(15 AS Numeric(18, 0)), CAST(80 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0050 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EE1 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (18, CAST(18 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0020 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EE4 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (19, CAST(18 AS Numeric(18, 0)), CAST(124 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0450 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EE6 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (20, CAST(50 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0020 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EEC AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (21, CAST(53 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.2350 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EEF AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (22, CAST(53 AS Numeric(18, 0)), CAST(64 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0020 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EF2 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (23, CAST(54 AS Numeric(18, 0)), CAST(19 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0600 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EF4 AS DateTime), NULL, NULL)
INSERT [dbo].[QuantitativeFood] ([Id], [FoodId], [MaterialId], [SchoolId], [QuantitativeOne], [QuantitativeTwo], [QuantitativeOrther], [Price], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (24, CAST(54 AS Numeric(18, 0)), CAST(96 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, CAST(0.0400 AS Numeric(18, 4)), NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A851016E4EF7 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[QuantitativeFood] OFF
SET IDENTITY_INSERT [dbo].[S_Logs] ON 

INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (1, N'Admin', CAST(0x0000A84D016F2F55 AS DateTime), N'grdView_MouseDown()', N'PROGRAM', N'frm_Inventory', N'frm_Inventory', N'#PROGRAM | IP: 192.168.1.3| USER: Admin | MSG:Object reference not set to an instance of an object.', N'Object reference not set to an instance of an object.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (2, N'Admin', CAST(0x0000A84D0172E402 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.3| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (3, N'Admin', CAST(0x0000A84D0172EB78 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.3| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (4, N'Admin', CAST(0x0000A84D017379D0 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.3| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (5, N'Admin', CAST(0x0000A84D017382A9 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.3| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (6, N'Admin', CAST(0x0000A84D01767D48 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.3| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (7, N'Admin', CAST(0x0000A85000F46C6A AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (8, N'Admin', CAST(0x0000A85000F6BFA9 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (9, N'Admin', CAST(0x0000A85000F769BF AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (10, N'Admin', CAST(0x0000A85000F77062 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (11, N'Admin', CAST(0x0000A85000FC7863 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (12, N'Admin', CAST(0x0000A85000FDAE99 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (13, N'Admin', CAST(0x0000A85000FDB354 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (14, N'Admin', CAST(0x0000A850010DDB11 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (15, N'Admin', CAST(0x0000A8500111BC17 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (16, N'Admin', CAST(0x0000A8500111C9CC AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.4| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (17, N'Admin', CAST(0x0000A850017C0933 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.7| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
INSERT [dbo].[S_Logs] ([Id], [UserName], [LogDate], [LogAction], [LogLevel], [Logger], [FormName], [LogContent], [Exception]) VALUES (18, N'Admin', CAST(0x0000A850018208F2 AS DateTime), N'Lưu', N'PROGRAM', N'popupFood', N'popupFood', N'#PROGRAM | IP: 192.168.1.7| USER: Admin | MSG:Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.', N'Exception of type ''DSofT.Framework.Logging.DSofTException'' was thrown.')
SET IDENTITY_INSERT [dbo].[S_Logs] OFF
SET IDENTITY_INSERT [dbo].[Schools] ON 

INSERT [dbo].[Schools] ([Id], [SchoolsCode], [SchoolsName], [Address], [Phone], [Fax], [Email], [Representative], [NumberStudentPrimary], [NumberStudentJuniorHigh], [NumberStudentHighSchool], [NumberTeacher], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(1 AS Decimal(18, 0)), N'A00000001', N'THCS Trần Quốc Toản', N'381 Lê Văn Việt, P. Tăng Nhơn Phú A, Q.9', N'01684591552', NULL, NULL, N'Nguyễn Tuấn Lộc', CAST(0 AS Numeric(18, 0)), CAST(1630 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, N'Admin', CAST(0x0000A84C0091D33C AS DateTime), N'Admin', CAST(0x0000A84C009444FE AS DateTime))
INSERT [dbo].[Schools] ([Id], [SchoolsCode], [SchoolsName], [Address], [Phone], [Fax], [Email], [Representative], [NumberStudentPrimary], [NumberStudentJuniorHigh], [NumberStudentHighSchool], [NumberTeacher], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(2 AS Decimal(18, 0)), N'A00000002', N'THCS Hoa Lư', N'48 Quang Trung, P. Tăng Nhơn Phú B, Q.9', N'01284204545', NULL, NULL, N'Lê Thị Minh Nguyệt', CAST(0 AS Numeric(18, 0)), CAST(1600 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, N'Admin', CAST(0x0000A84C0091D4C2 AS DateTime), N'Admin', CAST(0x0000A84C00942496 AS DateTime))
INSERT [dbo].[Schools] ([Id], [SchoolsCode], [SchoolsName], [Address], [Phone], [Fax], [Email], [Representative], [NumberStudentPrimary], [NumberStudentJuniorHigh], [NumberStudentHighSchool], [NumberTeacher], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(3 AS Decimal(18, 0)), N'A00000003', N'THCS Đặng Tấn Tài', N'Đường 128, khu phố 2, P.Phước Long A, Q.9', N'0902975000', NULL, NULL, N'Đặng Đình Đệ', CAST(0 AS Numeric(18, 0)), CAST(1030 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, N'Admin', CAST(0x0000A84C0092EF49 AS DateTime), N'Admin', CAST(0x0000A84C00940406 AS DateTime))
INSERT [dbo].[Schools] ([Id], [SchoolsCode], [SchoolsName], [Address], [Phone], [Fax], [Email], [Representative], [NumberStudentPrimary], [NumberStudentJuniorHigh], [NumberStudentHighSchool], [NumberTeacher], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(4 AS Decimal(18, 0)), N'A00000004', N'Tiểu Học Tân Phú', N'Số 7 đường 138, khu phố 2, P. Tân Phú, Q.9', N'0901836489', NULL, NULL, N'Phan Thanh Vũ', CAST(1240 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(60 AS Numeric(18, 0)), NULL, NULL, N'Admin', CAST(0x0000A84C00934546 AS DateTime), N'Admin', CAST(0x0000A84C0093E378 AS DateTime))
INSERT [dbo].[Schools] ([Id], [SchoolsCode], [SchoolsName], [Address], [Phone], [Fax], [Email], [Representative], [NumberStudentPrimary], [NumberStudentJuniorHigh], [NumberStudentHighSchool], [NumberTeacher], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(5 AS Decimal(18, 0)), N'A00000005', N'Tiểu Học Long Bình', N'Đường Nguyễn Xiển, ấp Cầu Ông Tàn, P. Long Bình, Q.9', N'0902424821', NULL, NULL, N'Nguyễn Thụy Ái Vy', CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), NULL, NULL, N'Admin', CAST(0x0000A84C0093ACA9 AS DateTime), N'Admin', CAST(0x0000A84C009458E1 AS DateTime))
SET IDENTITY_INSERT [dbo].[Schools] OFF
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(1 AS Numeric(18, 0)), N'A00000001', N'Công ty TNHH SX & TM Thực Phẩm Tân Vĩnh Phát', N'18/56 Phan Văn Hớn, ấp 7, Xã  Xuân Thới Thượng, Huyên Hóc Môn, Tp.HCM', N'0907756080', N'02836201680', NULL, N'Nguyên', NULL, NULL, N'Admin', CAST(0x0000A84C0095DE37 AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(2 AS Numeric(18, 0)), N'A00000002', N'Công ty Cổ Phần SX- TM Tài Tài', N'54 Nguyễn Thị Thử, ấp 5, Xuân Thới Thượng, Huyện Hóc Môn, Tp.HCM', N'', N'0862641061', NULL, N'', NULL, NULL, N'Admin', CAST(0x0000A84C0095E356 AS DateTime), N'Admin', CAST(0x0000A84C00976DA1 AS DateTime))
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(3 AS Numeric(18, 0)), N'A00000003', N'Công ty Cổ Phần Thực Phẩm Minh Kiệt', N'Lô B1.4-B1.9 khu nhà ở Thương Mại Đường Sắt Dĩ An, khu phố Thống Nhất 1, P. Dĩ An, Tỉnh Bình Dương', NULL, NULL, NULL, N'Thảo', NULL, NULL, N'Admin', CAST(0x0000A84C0098469B AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(4 AS Numeric(18, 0)), N'A00000004', N'Công ty TNHH Thực Phẩm Hoa Lan', NULL, NULL, NULL, NULL, N'Dung', NULL, NULL, N'Admin', CAST(0x0000A84C0098EDF1 AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(5 AS Numeric(18, 0)), N'A00000005', N'Công ty TNHH An An Bakery', N'184/45 Hoàng Hoa Thám, phường 5, quận Bình Thạnh, Tp.HCM', N'0933130525', NULL, N'ananbakery.com', N'', NULL, NULL, N'Admin', CAST(0x0000A84C0099CE41 AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(6 AS Numeric(18, 0)), N'A00000006', N'Công ty TNHH SX TM Tấn MInh', N'80/12/84 Dương Quảng Hàm, P.5, Q. Gò Vấp, Tp.HCM', NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C009A81B3 AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(7 AS Numeric(18, 0)), N'A00000007', N'Cơ sở Thực Phẩm Kim Liên', N'22/1/24A Nguyễn Văn Săng, P. Tân Sơn Nhì, Q. Tân Phú, Tp. HCM', NULL, NULL, NULL, N'Phan Thị Kim Liên', NULL, NULL, N'Admin', CAST(0x0000A84C009B4477 AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(8 AS Numeric(18, 0)), N'A00000008', N'Hộ Kinh Doanh Minh Hưng', N'Sạp P9B và P12, Chợ Đầu Mối Nông Sản Thực Phẩm Hóc Môn, ấp Mỹ Hòa 4, Xã Xuân Thới Đông, huyện Hóc Môn, Tp.HCM', NULL, NULL, NULL, NULL, N'0309913037', NULL, N'Admin', CAST(0x0000A84C009DD2AE AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(9 AS Numeric(18, 0)), N'A00000009', N'Hộ Kinh Doanh Minh Hưng', N'Sạp P9B và P12, Chợ Đầu Mối Nông Sản Thực Phẩm Hóc Môn, ấp Mỹ Hòa 4, Xã Xuân Thới Đông, huyện Hóc Môn, Tp.HCM', NULL, NULL, NULL, N'Chị Tám', N'0309913037', 1, N'Admin', CAST(0x0000A84C009E2319 AS DateTime), N'Admin', CAST(0x0000A84C009E45CF AS DateTime))
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(10 AS Numeric(18, 0)), N'A00000010', N'Chợ Bình Điền', NULL, NULL, NULL, NULL, N'Chị Phương', NULL, NULL, N'Admin', CAST(0x0000A84C00A7C767 AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(11 AS Numeric(18, 0)), N'A00000011', N'Sạp trứng chợ nhỏ', NULL, NULL, NULL, NULL, N'Nhi', NULL, NULL, N'Admin', CAST(0x0000A84C00A8D29C AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(12 AS Numeric(18, 0)), N'A00000012', N'Sạp đậu hũ chợ Phước Long A', NULL, NULL, NULL, NULL, N'Cô Hương', NULL, NULL, N'Admin', CAST(0x0000A84C00A937EE AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(13 AS Numeric(18, 0)), N'A00000013', N'Sạp hải sản chợ Phước Long A', NULL, NULL, NULL, NULL, N'Tuyết Nhung', NULL, NULL, N'Admin', CAST(0x0000A84C00AA09D5 AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(14 AS Numeric(18, 0)), N'A00000014', N'Cơ sở SX bún gần trường Tân Phú', NULL, NULL, NULL, NULL, N'Hương', NULL, NULL, N'Admin', CAST(0x0000A84C00AB3701 AS DateTime), NULL, NULL)
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(15 AS Numeric(18, 0)), N'A00000015', N'Công ty TNHH XD Hoài An', NULL, N'0908595435', NULL, NULL, N'Hảo', NULL, NULL, N'Admin', CAST(0x0000A84C00AC255D AS DateTime), N'Admin', CAST(0x0000A84C00AC8C12 AS DateTime))
INSERT [dbo].[Supplier] ([Id], [SupplierCode], [SupplierName], [Address], [Phone], [Fax], [Email], [Representative], [TaxCode], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(16 AS Numeric(18, 0)), N'A00000016', N'Tân Mỹ Châu', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84C00AD8291 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Supplier] OFF
SET IDENTITY_INSERT [dbo].[U_Menu] ON 

INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'frm_QuantitativeFoodV02', N'Định lượng món ăn', N'DSofT.FoodWarehouse.UI', N'frm_QuantitativeFoodV02', N'', N'clt_ico_default.png', 16, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'frm_Menu', N'Menu', N'DSofT.FoodWarehouse.UI', N'frm_Menu', N'', N'clt_ico_default.png', 9, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'frm_FoodTypes', N'Loại món ăn', N'DSofT.FoodWarehouse.UI', N'frm_FoodTypes', N'', N'clt_ico_default.png', 21, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'frm_UserRole', N'Role', N'DSofT.FoodWarehouse.UI', N'frm_UserRole', N'', N'clt_ico_default.png', 9, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'frm_UserType', N'User Type', N'DSofT.FoodWarehouse.UI', N'frm_UserType', N'', N'clt_ico_default.png', 9, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (6, N'frm_Material', N'Nguyên liệu', N'DSofT.FoodWarehouse.UI', N'frm_Material', N'', N'clt_ico_default.png', 21, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (7, N'frm_Food', N'Món ăn', N'DSofT.FoodWarehouse.UI', N'frm_Food', N'', N'clt_ico_default.png', 16, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (8, N'frm_FoodMenu', N'Thực đơn', N'DSofT.FoodWarehouse.UI', N'frm_FoodMenu', N'', N'clt_ico_default.png', 16, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (9, N'System', N'Hệ thống', N'DSofT.FoodWarehouse.UI', N'System', N'', N'', 0, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (10, N'frm_ImportWarehousingMaterials', N'Nhập kho', N'DSofT.FoodWarehouse.UI', N'frm_ImportWarehousingMaterials', N'', N'clt_ico_document.png', 16, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (12, N'frm_ReportMaterialWeek', N'Báo cáo Tiếp phẩm tuần', N'DSofT.FoodWarehouse.UI', N'frm_ReportMaterialWeek', NULL, N'clt_ico_document.png', 16, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (13, N'frm_School', N'Trường học', N'DSofT.FoodWarehouse.UI', N'frm_School', N'', N'clt_ico_default.png', 21, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (14, N'frm_ExportWarehousingMaterials', N'Xuất kho', N'DSofT.FoodWarehouse.UI', N'frm_ExportWarehousingMaterials', NULL, N'clt_ico_default.png', 16, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (15, N'frmUser', N'Người dùng', N'DSofT.FoodWarehouse.UI', N'frmUser', NULL, N'clt_ico_document.png', 9, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (16, N'ManageFood', N'Chức Năng', N'DSofT.FoodWarehouse.UI', N'ManageFood', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (17, N'frm_Warehouse', N'Danh mục Kho', N'DSofT.FoodWarehouse.UI', N'frm_Warehouse', N'', N'clt_ico_default.png', 21, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (18, N'frm_Supplier', N'Nhà cung cấp', N'DSofT.FoodWarehouse.UI', N'frm_Supplier', N'', N'clt_ico_default.png', 21, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (19, N'frm_GroupMaterial', N'Nhóm nguyên liệu', N'DSofT.FoodWarehouse.UI', N'frm_GroupMaterial', N'', N'clt_ico_default.png', 21, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (20, N'frm_Unit', N'Đơn vị tính', N'DSofT.FoodWarehouse.UI', N'frm_Unit', N'', N'clt_ico_default.png', 21, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (21, N'Category', N'Danh mục', N'DSofT.FoodWarehouse.UI', N'', N'', N'', 0, 0, 0, CAST(0x0000000000000000 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (22, N'frm_Inventory', N'Tồn kho', N'DSofT.FoodWarehouse.UI', N'frm_Inventory', NULL, N'clt_ico_default.png', 16, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[U_Menu] ([ID], [MenuCode], [MenuName], [MenuNamespaceClass], [MenuClassName], [MenuRemark], [MenuIcon], [MenuParentID], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (23, N'frm_FoodReality', N'Số lượng thực tế', N'DSofT.FoodWarehouse.UI', N'frm_FoodReality', NULL, N'clt_ico_default.png', 16, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[U_Menu] OFF
SET IDENTITY_INSERT [dbo].[U_User] ON 

INSERT [dbo].[U_User] ([ID], [UserName], [FullName], [Password], [Email], [Phone], [Address], [UserGroupID], [IsSuperAdmin], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate], [AreaID]) VALUES (3, N'Admin', N'Admin', N'F/rZs4D90Y+GvmLniR1RDA==', NULL, NULL, NULL, 1, 1, 0, NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[U_User] OFF
SET IDENTITY_INSERT [dbo].[U_User_Role] ON 

INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (1, 1, 1)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (2, 1, 2)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (3, 1, 5)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (4, 1, 3)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (5, 1, 4)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (6, 1, 6)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (7, 1, 7)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (8, 1, 8)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (9, 1, 10)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (10, 1, 12)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (11, 1, 13)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (12, 1, 14)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (13, 1, 16)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (14, 1, 9)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (15, 1, 15)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (16, 1, 17)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (17, 1, 18)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (18, 1, 19)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (19, 1, 20)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (20, 1, 21)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (21, 1, 22)
INSERT [dbo].[U_User_Role] ([ID], [UserTypeID], [MenuID]) VALUES (22, 1, 23)
SET IDENTITY_INSERT [dbo].[U_User_Role] OFF
SET IDENTITY_INSERT [dbo].[U_UserType] ON 

INSERT [dbo].[U_UserType] ([ID], [UserTypeCode], [UserTypeName], [UserTypeGroup], [UserTypeDescription], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Code1', N'1', 1, NULL, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[U_UserType] OFF
SET IDENTITY_INSERT [dbo].[Unit] ON 

INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(2 AS Decimal(18, 0)), N'Kg', N'Kg', N'1', 1, NULL, NULL, NULL, N'Admin', CAST(0x0000A832014E17FB AS DateTime))
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(3 AS Decimal(18, 0)), N'Gr', N'Gr', NULL, NULL, NULL, N'Admin', CAST(0x0000A83001723E01 AS DateTime), N'Admin', CAST(0x0000A832014E256A AS DateTime))
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(8 AS Decimal(18, 0)), N'Binh', N'Bình', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00897AA4 AS DateTime), N'Admin', CAST(0x0000A84D00A7B841 AS DateTime))
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(9 AS Decimal(18, 0)), N'Trung', N'Trứng', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A71630 AS DateTime), N'Admin', CAST(0x0000A84D00A7CE4C AS DateTime))
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(10 AS Decimal(18, 0)), N'Hu', N'Hủ', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A73079 AS DateTime), N'Admin', CAST(0x0000A84D00A7AFDE AS DateTime))
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(11 AS Decimal(18, 0)), N'O', N'Ổ', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A74D05 AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(12 AS Decimal(18, 0)), N'Cai', N'Cái', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A76C89 AS DateTime), N'Admin', CAST(0x0000A84D00A7A400 AS DateTime))
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(13 AS Decimal(18, 0)), N'Can', N'Can', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A79A1C AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(14 AS Decimal(18, 0)), N'Chai', N'Chai', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A7E69E AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(15 AS Decimal(18, 0)), N'Trai', N'Trái', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00A886DC AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(16 AS Decimal(18, 0)), N'lit', N'lít', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AE8C2D AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(17 AS Decimal(18, 0)), N'mieng', N'miếng', NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AEAB65 AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(18 AS Decimal(18, 0)), N'Hôp', N'Hộp', NULL, NULL, NULL, N'Admin', CAST(0x0000A8500115B3E0 AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(19 AS Decimal(18, 0)), N'Ong', N'Ống', NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F16BB AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(20 AS Decimal(18, 0)), N'Vi', N'Vĩ', NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F285A AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(21 AS Decimal(18, 0)), N'Loc', N'Lốc', NULL, NULL, NULL, N'Admin', CAST(0x0000A850016F4289 AS DateTime), NULL, NULL)
INSERT [dbo].[Unit] ([Id], [UnitCode], [UnitName], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(22 AS Decimal(18, 0)), N'Bich', N'Bịch', NULL, NULL, NULL, N'Admin', CAST(0x0000A850017D85EF AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Unit] OFF
SET IDENTITY_INSERT [dbo].[Warehouse] ON 

INSERT [dbo].[Warehouse] ([Id], [WarehouseCode], [WarehouseName], [Address], [Phone], [Fax], [Email], [Representative], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(1 AS Decimal(18, 0)), N'A00000001', N'Công cụ dụng cụ', NULL, NULL, NULL, NULL, N'', NULL, NULL, NULL, N'Admin', CAST(0x0000A8320178FB6B AS DateTime), N'Admin', CAST(0x0000A84C0090C97B AS DateTime))
INSERT [dbo].[Warehouse] ([Id], [WarehouseCode], [WarehouseName], [Address], [Phone], [Fax], [Email], [Representative], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(2 AS Decimal(18, 0)), N'A00000002', N'Nguyên vật liệu', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A836009701AE AS DateTime), N'Admin', CAST(0x0000A84C0090DDE5 AS DateTime))
INSERT [dbo].[Warehouse] ([Id], [WarehouseCode], [WarehouseName], [Address], [Phone], [Fax], [Email], [Representative], [Remark], [RowVersion], [IsDelete], [CreateBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (CAST(3 AS Decimal(18, 0)), N'A00000003', N'Thịt, cá', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Admin', CAST(0x0000A84D00AFADEE AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Warehouse] OFF
ALTER TABLE [dbo].[U_Menu] ADD  CONSTRAINT [DF_U_Menu_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[U_User] ADD  CONSTRAINT [DF_U_User_IsSuperAdmin]  DEFAULT ((0)) FOR [IsSuperAdmin]
GO
ALTER TABLE [dbo].[U_User] ADD  CONSTRAINT [DF_U_User_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[U_UserType] ADD  CONSTRAINT [DF_U_UserType_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 buổi sáng, 2 buổi trưa, 3 buổi xế, 4 buổi chiều' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FoodTypes', @level2type=N'COLUMN',@level2name=N'IsSession'
GO
USE [master]
GO
ALTER DATABASE [DVSTestFrame] SET  READ_WRITE 
GO
