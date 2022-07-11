USE [Demo]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateToDoCompletedStatus]    Script Date: 11-07-2022 12:31:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spUpdateToDoCompletedStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spUpdateToDoCompletedStatus]
GO
/****** Object:  StoredProcedure [dbo].[spSaveToDo]    Script Date: 11-07-2022 12:31:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSaveToDo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spSaveToDo]
GO
/****** Object:  StoredProcedure [dbo].[spGetToDoList]    Script Date: 11-07-2022 12:31:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spGetToDoList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spGetToDoList]
GO
/****** Object:  StoredProcedure [dbo].[spGetToDoDetailByID]    Script Date: 11-07-2022 12:31:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spGetToDoDetailByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spGetToDoDetailByID]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteToDo]    Script Date: 11-07-2022 12:31:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spDeleteToDo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spDeleteToDo]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_TODO_created_date]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TODO] DROP CONSTRAINT [DF_TODO_created_date]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_TODO_is_completed]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TODO] DROP CONSTRAINT [DF_TODO_is_completed]
END
GO
/****** Object:  Table [dbo].[TODO]    Script Date: 11-07-2022 12:31:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TODO]') AND type in (N'U'))
DROP TABLE [dbo].[TODO]
GO
/****** Object:  Table [dbo].[TODO]    Script Date: 11-07-2022 12:31:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TODO](
	[todo_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](200) NOT NULL,
	[is_completed] [bit] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[modified_date] [datetime] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TODO] ON 
GO
INSERT [dbo].[TODO] ([todo_id], [title], [is_completed], [created_date], [modified_date]) VALUES (30, N'Make a call', 1, CAST(N'2022-07-10T16:00:42.443' AS DateTime), CAST(N'2022-07-10T10:33:51.950' AS DateTime))
GO
INSERT [dbo].[TODO] ([todo_id], [title], [is_completed], [created_date], [modified_date]) VALUES (34, N'Book a flight to NY', 0, CAST(N'2022-07-10T16:04:48.567' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[TODO] OFF
GO
ALTER TABLE [dbo].[TODO] ADD  CONSTRAINT [DF_TODO_is_completed]  DEFAULT ((0)) FOR [is_completed]
GO
ALTER TABLE [dbo].[TODO] ADD  CONSTRAINT [DF_TODO_created_date]  DEFAULT (getdate()) FOR [created_date]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteToDo]    Script Date: 11-07-2022 12:31:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Company:		  
		TRIRID

	Author:        
		Malay Dhandha

	Description:   
		Delete To D

	Changes:
		
		No		Date			Author				Description
		--		-----------		-------------		--------------------------
		1.		July 09, 2022	Malay Dhandha		Created
	
*/
CREATE PROCEDURE [dbo].[spDeleteToDo]
	@todo_id		INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION

		
		DELETE FROM [TODO] WHERE todo_id = @todo_id
		

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT>0
			ROLLBACK TRANSACTION
		
		DECLARE @errormessage NVARCHAR(MAX);
		DECLARE @errorserverity INT;
		DECLARE @errorstate INT;
		
		SELECT @errormessage=ERROR_MESSAGE(),@errorserverity=ERROR_SEVERITY(),@errorstate=ERROR_STATE();
		RAISERROR(@errormessage,@errorserverity,@errorstate);
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spGetToDoDetailByID]    Script Date: 11-07-2022 12:31:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* 
	
	Company:		  
		TRIRID

	Author:        
		Malay Dhandha

	Description:   
		Get To Do Details

	Parameters:
		No available
	
	Examples:
		EXEC [dbo].[spGetToDoDetailByID] @todo_id = 1
			

	Changes:
		
		No		Date			Author				Description
		--		-----------		-------------		--------------------------
		1.		July 09, 2022	Malay Dhandha		Created
	
*/

CREATE PROCEDURE [dbo].[spGetToDoDetailByID]
	@todo_id	INT
AS
BEGIN
	SET NOCOUNT ON;

	      SELECT 
				 [todo_id]
				,[title]
				,[is_completed]
				,[created_date]
				,[modified_date]
	      FROM
		         [TODO]
	      WHERE
		         todo_id = @todo_id
END
GO
/****** Object:  StoredProcedure [dbo].[spGetToDoList]    Script Date: 11-07-2022 12:31:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/* 
	
	Company:		  
		TRIRID

	Author:        
		Malay Dhandha

	Description:   
		Get To Do List

	Parameters:
		No available
	
	Examples:
		EXEC [dbo].[spGetToDoList] 
			

	Changes:
		
		No		Date				Author				Description
		--		-----------			-------------		--------------------------
		1.		July 09, 2022		Malay Dhandha		Created
	
*/
CREATE PROCEDURE [dbo].[spGetToDoList]
AS 
 BEGIN 
 	 
 		SET NOCOUNT ON;

		
		SELECT 
				[todo_id]
			,[title]
			,[is_completed]
			,[created_date]
			,[modified_date]
		FROM
			[TODO]
		ORDER BY 
				[todo_id] DESC

		
 	
END
GO
/****** Object:  StoredProcedure [dbo].[spSaveToDo]    Script Date: 11-07-2022 12:31:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	
	
	Company:		  
		TRIRID

	Author:        
		Malay Dhandha

	Description:   
		Save To Do

	Changes:
		
		No		Date			Author				Description
		--		-----------		-------------		--------------------------
		1.		July 09, 2022	Malay Dhandha		Created
	
*/
CREATE PROCEDURE [dbo].[spSaveToDo]
	@todo_id		INT = NULL,
	@title			VARCHAR(200), 
	@is_completed   BIT = 0,
	@mode			CHAR(1)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION

		IF @mode='I'
		BEGIN
			INSERT INTO [TODO]
			(
				 [title]
				,[is_completed]
				,[created_date]
			)
			VALUES
			(
				 @title	   
				,@is_completed		
				,GETDATE()		
			)
		END
		ELSE IF @mode='U'
		BEGIN
			UPDATE 
				[TODO]
			SET
				 [title] = @title
				,[is_completed] = @is_completed
				,[modified_date] = GETUTCDATE()
			WHERE
			    todo_id = @todo_id
		END

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT>0
			ROLLBACK TRANSACTION
		
		DECLARE @errormessage NVARCHAR(MAX);
		DECLARE @errorserverity INT;
		DECLARE @errorstate INT;
		
		SELECT @errormessage=ERROR_MESSAGE(),@errorserverity=ERROR_SEVERITY(),@errorstate=ERROR_STATE();
		RAISERROR(@errormessage,@errorserverity,@errorstate);
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateToDoCompletedStatus]    Script Date: 11-07-2022 12:31:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Company:		  
		TRIRID

	Author:        
		Malay Dhandha

	Description:   
		Mark Completed or Not Completed

	Changes:
		
		No		Date			Author				Description
		--		-----------		-------------		--------------------------
		1.		July 09, 2022	Malay Dhandha		Created
	
*/
CREATE PROCEDURE [dbo].[spUpdateToDoCompletedStatus]
	@todo_id		INT,
	@is_completed   BIT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION

		
		UPDATE 
			[TODO]
		SET
			 [is_completed] = @is_completed
			,[modified_date] = GETUTCDATE()
		WHERE
			todo_id = @todo_id
		

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT>0
			ROLLBACK TRANSACTION
		
		DECLARE @errormessage NVARCHAR(MAX);
		DECLARE @errorserverity INT;
		DECLARE @errorstate INT;
		
		SELECT @errormessage=ERROR_MESSAGE(),@errorserverity=ERROR_SEVERITY(),@errorstate=ERROR_STATE();
		RAISERROR(@errormessage,@errorserverity,@errorstate);
	END CATCH
END
GO
