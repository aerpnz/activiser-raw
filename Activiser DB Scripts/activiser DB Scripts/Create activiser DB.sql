use master
go
--drop database <activiser database, sysname, activiser>
--go
create database <activiser database, sysname, activiser> collate <database collation, , Latin1_General_CI_AS>
go
use <activiser database, sysname, activiser>
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE GatewayDuplicatedTask(
	TaskID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	TaskPropertySet XML NULL,
	EntityPropertySet XML NULL,
	UpdateTime datetime NOT NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_GatewayDuplicatedTask PRIMARY KEY CLUSTERED ( TaskID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE GatewayTask(
	TaskID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	TaskGroupID uniqueidentifier NOT NULL,
	Task XML NULL,
	NextTaskID uniqueidentifier NULL,
	State varchar(200) NULL,
	UpdateTime datetime NOT NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_GatewayTask PRIMARY KEY CLUSTERED ( TaskID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE GatewayException(
	ExceptionID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	Source varchar(100) NULL,
	Message nvarchar(max) NULL,
	Stacktrace nvarchar(max) NULL,
	Entity nvarchar(max) NULL,
	GatewayTaskState varchar(100) NULL,
	TaskExecutionState varchar(100) NULL,
	OccurenceTime datetime NULL DEFAULT (GETUTCDATE()),
	ExtraNote nvarchar(max) NULL,
	ClassType varchar(100) NULL,
	TaskID uniqueidentifier NULL,
 CONSTRAINT PK_GatewayException PRIMARY KEY CLUSTERED ( ExceptionID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE GatewayReturnValue(
	TaskID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	ReturnValue nvarchar(max) NULL,
	UpdateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_GatewayReturnValue PRIMARY KEY CLUSTERED ( TaskID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE JobStatus(
	JobStatusID int NOT NULL,
	Description nvarchar(50) NOT NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_JobStatus PRIMARY KEY CLUSTERED ( JobStatusID ASC )) 

GO

CREATE UNIQUE NONCLUSTERED INDEX IX_JobStatus_Description ON JobStatus 
( Description ASC
) 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE ServerProfile(
	ID int IDENTITY(1,1) NOT NULL,
	SystemID varchar(100) NULL,
	Activated bit NULL,
	Notes nvarchar(3000) NULL,
	ItemType char(1) NOT NULL DEFAULT 'D',
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_ServerProfile PRIMARY KEY CLUSTERED ( ID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	@Populate - pre-populate the audit tables with the current contents of the source tables
	@BeforeImageAuditing - for 'UPDATE' audit triggers, record the contents of the 'DELETED' table
	@AfterImageAuditing - for 'UPDATE' audit triggers, record the contents of the 'INSERTED' table
	@DryRun - don't actually ALTER  the triggers, just display the required SQL code
*/
CREATE PROCEDURE spCreateAuditTables(
	@Populate BIT = 0,
	@BeforeImageAuditing BIT = 0,
	@AfterImageAuditing BIT = 1,
	@DryRun BIT = 1,
	@SpecificTable nvarchar(255) = NULL
) AS

	SET NOCOUNT ON

	IF NOT @SpecificTable IS NULL
		DECLARE tablecursor CURSOR FOR 
		SELECT id,Name FROM dbo.sysobjects WHERE (xtype = 'U') 
			AND Name Like @SpecificTable
	ELSE
		DECLARE tablecursor CURSOR FOR 
		SELECT id,Name FROM dbo.sysobjects WHERE (xtype = 'U') 
		AND NOT (
			Name like 'Audit%' 
			OR Name like '%Log'
			OR Name like 'Gateway%'
			OR Name = 'dtproperties'
			OR Name like 'Trace%' 
		)

	DECLARE @TableName varchar(250) 
	DECLARE @AuditTableName nvarchar(255) 

	DECLARE @TableID int
	DECLARE @AuditTableID int

	DECLARE @CreateSQL varchar(8000)
	DECLARE @PopulateSQL varchar(8000)

	DECLARE @SourceColumns varchar(8000)
	DECLARE @InsertedSourceColumns varchar(8000)
	DECLARE @DeletedSourceColumns varchar(8000)
	 
	DECLARE @AuditTrigger varchar(8000)
	DECLARE @TriggerStart varchar(8000)
	DECLARE @TriggerEnd varchar(10)

	DECLARE @TriggerInsert varchar(8000)
	DECLARE @TriggerInsert2 varchar(8000)

	DECLARE @TriggerStartTemplate varchar(8000)
	DECLARE @TriggerStartInsertTemplate varchar(8000)
	DECLARE @TriggerEndInsertTemplate varchar(800)

	DECLARE @AuditTriggerTemplate varchar(8000)

	DECLARE @gottextcolumn int

	CREATE TABLE #ProblemFields
	(
		TableName VARCHAR(255),
		ColumnName VARCHAR(255),
		TypeName VARCHAR(20))

	SET @AuditTriggerTemplate = 'CREATE TRIGGER dbo.{##TABLENAME}PreventAlter ON dbo.{##TABLENAME}
INSTEAD OF DELETE, UPDATE
AS
BEGIN
	SET NOCOUNT ON
END'

    SET @TriggerStartTemplate = '
CREATE TRIGGER {##TABLENAME}Audit{##ACTION}
ON dbo.{##TABLENAME}
FOR {##ACTION}
AS

BEGIN
    SET NOCOUNT ON 
  
    DECLARE @action varchar(8)
    SET @action=''{##ACTION}''
'

	SET @TriggerStartInsertTemplate = '
    INSERT Audit{##TableName} 
	SELECT 
		NEWID() AS AuditGUID,
        getdate() As AuditCreatedDateTime, 
		@action As AuditAction,
		COLUMNS_UPDATED() AS AuditColumns,
        USER_NAME() As AuditUser,
        SYSTEM_USER As AuditWindowsUser,
        HOST_NAME() as AuditHostName,
        APP_NAME() as AuditProgramName'

	SET @TriggerEndInsertTemplate = '
    FROM {##SOURCE}
'
	SET @TriggerEnd = '
END
'

	DECLARE @err int

	SELECT @gottextcolumn = 0
	OPEN tablecursor
	FETCH NEXT FROM tablecursor INTO @TableID, @TableName 
	WHILE (@@FETCH_STATUS = 0) BEGIN
		SELECT @AuditTableName = 'Audit' + @TableName 

		IF exists (select * from dbo.sysobjects where id = object_id(@AuditTableName) and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		BEGIN
			PRINT 'DROP TABLE dbo.' +  @AuditTableName + ' ' 
			PRINT 'GO'
			IF @DryRun = 0 EXECUTE ('DROP TABLE ' +  @AuditTableName + '')
		END

		PRINT '-- Adding ' + @AuditTableName + '...'

        DECLARE ColumnCursor CURSOR FOR SELECT name, TYPE_NAME(xusertype), xprec, length, xscale FROM syscolumns WHERE id=@TableID AND Name <> 'AuditGUID'
        DECLARE @ColumnName sysname
        DECLARE @TypeName varchar(20)
        DECLARE @Precision varchar(20)
        DECLARE @Length int
        DECLARE @Scale varchar(10)
        DECLARE @DataType varchar(50)

        SET @CreateSQL = 'CREATE TABLE dbo.' + @AuditTableName + '
		(AuditGUID uniqueidentifier rowguidcol not null, 
		AuditCreatedDateTime datetime NULL, 
		AuditAction varchar(8) NULL, 
		AuditColumns varbinary(128) NULL,
		AuditUser varchar(128) NULL,
		AuditWindowsUser varchar(128) NULL, 
		AuditHostName varchar(128) NULL, 
		AuditProgramName varchar(128) NULL'

        SET @InsertedSourceColumns = ''
		SET @DeletedSourceColumns = ''
		SET @SourceColumns = ''

        OPEN ColumnCursor
        FETCH NEXT FROM ColumnCursor INTO @ColumnName, @TypeName, @Precision, @Length, @Scale
        WHILE @@FETCH_STATUS = 0 BEGIN
		    IF @TypeName = 'text' or @TypeName = 'ntext' or @TypeName = 'image' BEGIN
			    PRINT '-- !!!! Manual fix up possibly required for column: ' + @TableName + '.' + @ColumnName + ', Dataype: ' + @TypeName
			    INSERT #ProblemFields (TableName, ColumnName, TypeName) SELECT @TableName, @ColumnName, @TypeName
			    SELECT @gottextcolumn = 1
                SET @CreateSQL = @CreateSQL + ',
		' + @ColumnName + ' ' + @TypeName + ' NULL'
                SET @SourceColumns = @SourceColumns + ',
		' + @TableName + '.' + @ColumnName + ''
                SET @InsertedSourceColumns = @InsertedSourceColumns + ',
		' + @TableName + '.' + @ColumnName + ''
			    SET @DeletedSourceColumns = @DeletedSourceColumns + ',
		NULL /* ' + @ColumnName + ' */ '
		    END
		    ELSE BEGIN
                IF @TypeName = 'varchar' or @typename = 'char' or @typename = 'nvarchar' or @typename = 'nchar' or @typename='binary' or @typename='varbinary'
                BEGIN
					IF @Length <= 0 -- assume (MAX)
						SET @DataType = @TypeName + '(MAX)' -- length is bytes, which is different for nchar, nvarchar
                    ELSE BEGIN
						IF @typename = 'nvarchar' or @typename = 'nchar' 
							SET @DataType = @TypeName + '(' + cast(@Length / 2 as varchar) + ')' -- length is bytes, which is different for nchar, nvarchar
						ELSE
							SET @DataType = @TypeName + '(' + cast(@Length as varchar)  + ')' -- length is bytes, which is different for nchar, nvarchar
					END
                END
                ELSE
                BEGIN
                    IF @TypeName = 'decimal' or @typename = 'numeric'
                        SET @DataType = @TypeName + '(' + @Precision + ',' + @Scale + ')'
                    ELSE 
                        SET @DataType = @TypeName
                END
                SET @CreateSQL = @CreateSQL + ',
		' + @ColumnName + ' ' + @DataType + ' NULL'
		    	SET @SourceColumns = @SourceColumns  + ',
		' + @ColumnName + ''
                SET @InsertedSourceColumns = @InsertedSourceColumns + ',
		INSERTED.' + @ColumnName + ''
    			SET @DeletedSourceColumns = @DeletedSourceColumns + ',
		DELETED.' + @ColumnName + ''
            END
            FETCH NEXT FROM ColumnCursor INTO @ColumnName, @TypeName, @Precision, @Length, @Scale
        END
        CLOSE ColumnCursor
        DEALLOCATE ColumnCursor
 
		SET @CreateSQL = @CreateSQL + ')'
        PRINT @CreateSQL
		PRINT 'GO'
        IF @DryRun = 0 EXECUTE (@CreateSQL)

		-- If requested, pre-populate the audit table.
		-- not so useful if not doing 'before-image' auditing - where just the 'deleted' table is used,
		-- but fairly important for 'after-image' auditing, where the 'inserted' table is used.
		IF @Populate <> 0 BEGIN
			SELECT @PopulateSQL = 'INSERT dbo.' + @AuditTableName + ' 
		SELECT 
		NEWID() AS AuditGUID,
		getdate() as AuditCreatedDateTime, 
		''History'' as AuditAction, 
		NULL AS AuditColumns,
		USER_NAME() as AuditUser, 
		SYSTEM_USER as AuditWindowsUser, 
		HOST_NAME() as AuditHostName,
		APP_NAME() as AuditProgramName'
		+ @SourceColumns + '
		FROM ' + @TableName + ''
 
			PRINT 'Populating Audit table with initial data...'
			PRINT @PopulateSQL
			PRINT 'GO'
            IF @DryRun = 0 EXECUTE (@PopulateSQL)
		END

		-- Create trigger to disable fiddling with the Audit Table itself.
		SET @AuditTrigger = replace(@AuditTriggerTemplate,'{##TABLENAME}',@AuditTableName)
		PRINT @AuditTrigger
		PRINT 'GO'
		IF @DryRun = 0 EXECUTE (@AuditTrigger)
 
        IF EXISTS(SELECT * FROM sysobjects where xtype='TR' and name=@TableName + 'AuditInsert') BEGIN
			PRINT 'DROP TRIGGER ' + @TableName + 'AuditInsert'
			PRINT 'GO'
            	IF @DryRun = 0 EXECUTE('DROP TRIGGER ' + @TableName + 'AuditInsert')
		END
        IF EXISTS(SELECT * FROM sysobjects where xtype='TR' and name=@TableName + 'AuditDelete') BEGIN
			PRINT 'DROP TRIGGER ' + @TableName + 'AuditDelete'
			PRINT 'GO'
            IF @DryRun = 0 EXECUTE('DROP TRIGGER ' + @TableName + 'AuditDelete')
		END
        IF EXISTS(SELECT * FROM sysobjects where xtype='TR' and name=@TableName + 'AuditUpdate') BEGIN
			PRINT 'DROP TRIGGER ' + @TableName + 'AuditUpdate'
			PRINT 'GO'
            IF @DryRun = 0 EXECUTE('DROP TRIGGER ' + @TableName + 'AuditUpdate')
		END
 
		SET @TriggerStart = replace(replace(@TriggerStartTemplate,'{##TABLENAME}',@TableName), '{##ACTION}','INSERT')

		DECLARE @TriggerTable varchar(250)

		IF @gottextcolumn = 0 BEGIN
			SELECT @TriggerTable = 'INSERTED'
		END
		ELSE BEGIN
			SELECT @TriggerTable = 'INSERTED JOIN ' + @TableName 
			DECLARE pkCursor CURSOR FOR
				SELECT dbo.syscolumns.name
				FROM   dbo.sysobjects AS tables INNER JOIN
                       dbo.sysobjects AS primarykeys ON tables.id = primarykeys.parent_obj INNER JOIN
                       dbo.sysindexes ON tables.id = dbo.sysindexes.id AND primarykeys.name = dbo.sysindexes.name INNER JOIN
                       dbo.sysindexkeys ON tables.id = dbo.sysindexkeys.id AND dbo.sysindexes.indid = dbo.sysindexkeys.indid INNER JOIN
                       dbo.syscolumns ON tables.id = dbo.syscolumns.id AND dbo.sysindexkeys.colid = dbo.syscolumns.colid
				WHERE   (tables.xtype = 'U') AND (tables.id = @TableID) AND (primarykeys.xtype = 'PK')
			OPEN pkCursor
			-- DECLARE @ColumnName sysname
			DECLARE @Join nvarchar(2000)
			SELECT @Join = ''
			FETCH NEXT FROM pkCursor INTO @ColumnName 
			WHILE @@FETCH_STATUS = 0 BEGIN
				IF @Join = '' SELECT @Join = ' ON '
				SELECT @Join = @Join + 'INSERTED.' + @ColumnName + ' = ' + @TableName + '.' + @ColumnName
				FETCH NEXT FROM pkCursor INTO @ColumnName 
				IF @@FETCH_STATUS = 0 SELECT @Join = @Join + ' AND '
			END
			CLOSE pkCursor
			DEALLOCATE pkCursor
			IF @Join <> '' BEGIN
				SELECT @TriggerTable = @TriggerTable + ' ' + @Join
			END
		END
        SET @TriggerInsert = replace(@TriggerStartInsertTemplate,'{##TABLENAME}',@TableName)
			+ @InsertedSourceColumns + replace(@TriggerEndInsertTemplate,'{##SOURCE}', @TriggerTable)
 
        PRINT ( @TriggerStart + @TriggerInsert + @TriggerEnd)
		PRINT 'GO'
        IF @DryRun = 0 EXECUTE (@TriggerStart + @TriggerInsert + @TriggerEnd)

		SET @TriggerStart = replace(replace(@TriggerStartTemplate,'{##TABLENAME}',@TableName), '{##ACTION}','UPDATE')

		IF @BeforeImageAuditing <> 0
			SET @TriggerInsert = replace(@TriggerStartInsertTemplate,'{##TABLENAME}',@TableName)
				+ @DeletedSourceColumns + replace(@TriggerEndInsertTemplate,'{##SOURCE}', 'DELETED')
		ELSE
			SET @TriggerInsert = ''
		

		IF @AfterImageAuditing <> 0
			-- SET @TriggerInsert2 = replace(@TriggerStartInsertTemplate,'{##TABLENAME}',@TableName)
			SET @TriggerInsert2 = replace(@TriggerStartInsertTemplate,'{##TABLENAME}',@TableName)
				+ @InsertedSourceColumns + replace(@TriggerEndInsertTemplate,'{##SOURCE}', @TriggerTable)
		ELSE
			SET @TriggerInsert2 = ''

		PRINT ( @TriggerStart + @TriggerInsert + @TriggerInsert2 + @TriggerEnd)
		PRINT 'GO'
		IF @DryRun = 0 EXECUTE (@TriggerStart + @TriggerInsert + @TriggerInsert2 + @TriggerEnd)

	SET @TriggerStart = replace(replace(@TriggerStartTemplate,'{##TABLENAME}',@TableName), '{##ACTION}','DELETE')
		SET @TriggerInsert = replace(@TriggerStartInsertTemplate,'{##TABLENAME}',@TableName)
			+ @DeletedSourceColumns + replace(@TriggerEndInsertTemplate,'{##SOURCE}', 'DELETED')

        PRINT ( @TriggerStart + @TriggerInsert + @TriggerEnd)
		PRINT 'GO'
        IF @DryRun = 0 EXECUTE (@TriggerStart + @TriggerInsert + @TriggerEnd)
 
		FETCH NEXT FROM tablecursor INTO @TableID,@TableName 
		SELECT @gottextcolumn = 0
	END 
	CLOSE tablecursor 
	DEALLOCATE tablecursor

	IF @DryRun <> 0 PRINT '
-- Dry run completed successfully, refer to results for a list of Problem Fields'
	ELSE PRINT '
-- Audit table creation completed successfully, refer to results for a list of Problem Fields'
	SELECT * FROM #ProblemFields ORDER BY TableName, ColumnName

RETURN

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.spCreateTimeStampDefaults (
    @DryRun BIT = 0, 
    @Verbose BIT = 0,
    @Quiet BIT = 0,
	@SpecificTable NVARCHAR(255) = NULL
) AS

	SET Nocount ON

	IF @Quiet = 0 PRINT 'Generating timestamp defaults for relevant tables...'

	IF NOT @SpecificTable IS NULL
		DECLARE Tablecursor CURSOR FOR 
		SELECT id,Name FROM Dbo.Sysobjects WHERE (Xtype = 'U') 
			AND Name LIKE @SpecificTable
	ELSE
		DECLARE Tablecursor CURSOR FOR 
		SELECT id,Name FROM Dbo.Sysobjects WHERE (Xtype = 'U') 
		AND NOT (
			Name LIKE 'Audit%' 
			OR Name LIKE '%Log'
			OR Name LIKE 'Gateway%'
			OR Name = 'dtproperties'
			OR Name LIKE 'Trace%' 
		)

	DECLARE @TableName NVARCHAR(128) 
	DECLARE @TableID INT

	DECLARE @ConstraintName NVARCHAR(128)
	DECLARE @Constraint NVARCHAR(400)

	DECLARE @CreatedTemplate NVARCHAR(400)
	DECLARE @ModifiedTemplate NVARCHAR(400)

	DECLARE @Task NVARCHAR(4000)

	DECLARE @err INT
    DECLARE @timestampRows INT

	OPEN Tablecursor
	FETCH Next FROM Tablecursor INTO @TableID, @TableName 
	WHILE (@@FETCH_STATUS = 0) BEGIN

		SELECT @timestampRows=COUNT(*) FROM Syscolumns
			WHERE Id=@TableID 
				AND (name='ModifiedDateTime' OR name='CreatedDateTime') 
				AND (xtype=6 OR xtype=61)
        IF @timestampRows = 2 BEGIN

			IF @Quiet = 0 PRINT 'Setting defaults for table ' + @TableName + '...'
			
			SELECT @ConstraintName = defaults.name
			FROM sysconstraints INNER JOIN
				 (SELECT id, name FROM sysobjects WHERE (xtype = 'D')) AS defaults ON sysconstraints.constid = defaults.id INNER JOIN
				 (SELECT id, name FROM sysobjects WHERE (xtype = 'U')) AS tables ON tables.id = sysconstraints.id INNER JOIN
				 syscolumns ON syscolumns.id = sysconstraints.id AND syscolumns.colid = sysconstraints.colid
			WHERE tables.name = @TableName AND syscolumns.name = 'CreatedDateTime'
			
			IF NOT @ConstraintName IS NULL BEGIN
				SELECT @TASK = 'ALTER TABLE ' + @TableName + ' DROP CONSTRAINT ' +  @ConstraintName + '' 
				IF @Verbose = 1 BEGIN
					PRINT @TASK
					PRINT 'GO'
				END
				IF @DryRun = 0 EXECUTE (@TASK)
			END

			SELECT @Constraint = 'ALTER TABLE ' + @TableName  + ' ADD DEFAULT GETUTCDATE() FOR CreatedDateTime'
			IF @Verbose = 1 BEGIN
				PRINT ( @Constraint )
				PRINT 'GO'
            END
			IF @DryRun = 0 EXECUTE ( @Constraint )

			SELECT @ConstraintName = defaults.name
			FROM sysconstraints INNER JOIN
				 (SELECT id, name FROM sysobjects WHERE (xtype = 'D')) AS defaults ON sysconstraints.constid = defaults.id INNER JOIN
				 (SELECT id, name FROM sysobjects WHERE (xtype = 'U')) AS tables ON tables.id = sysconstraints.id INNER JOIN
				 syscolumns ON syscolumns.id = sysconstraints.id AND syscolumns.colid = sysconstraints.colid
			WHERE tables.name = @TableName AND syscolumns.name = 'ModifiedDateTime'

			IF NOT @ConstraintName IS NULL BEGIN
				SELECT @TASK = 'ALTER TABLE ' + @TableName + ' DROP CONSTRAINT ' +  @ConstraintName + '' 
				IF @Verbose = 1 BEGIN
					PRINT @TASK
					PRINT 'GO'
				END
				IF @DryRun = 0 EXECUTE (@TASK)
			END

			SELECT @Constraint = 'ALTER TABLE ' + @TableName  + ' ADD DEFAULT GETUTCDATE() FOR ModifiedDateTime'
			IF @Verbose = 1 BEGIN
				PRINT ( @Constraint )
				PRINT 'GO'
            END
			IF @DryRun = 0 EXECUTE ( @Constraint )
		END
		ELSE BEGIN
			IF @Quiet = 0 PRINT 'Skipping table : ' + @TableName + ' due to missing timestamp columns'
		END
 		FETCH Next FROM Tablecursor INTO @TableID,@TableName 
	END 
	CLOSE Tablecursor 
	DEALLOCATE Tablecursor
RETURN

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	@DryRun - don't actually DROP the triggers and tables, just display the required SQL code
*/

CREATE PROCEDURE spDropAuditTables(
	@DryRun BIT = 1,
	@SpecificTable nvarchar(255) = NULL
) AS

	SET NOCOUNT ON

	IF NOT @SpecificTable IS NULL
		DECLARE tablecursor CURSOR FOR 
		SELECT id,Name FROM dbo.sysobjects WHERE (xtype = 'U') 
			AND Name Like @SpecificTable
	ELSE
		DECLARE tablecursor CURSOR FOR 
		SELECT id,Name FROM dbo.sysobjects WHERE (xtype = 'U') 
		AND NOT (
			Name like 'Audit%' 
			OR Name like '%Log'
			OR Name like 'Gateway%'
			OR Name = 'dtproperties'
			OR Name like 'Trace%' 
		)

	DECLARE @TableName varchar(250) 
	DECLARE @AuditTableName nvarchar(255) 

	DECLARE @TableID int
	DECLARE @AuditTableID int

	DECLARE @AuditTrigger varchar(8000)

	OPEN tablecursor
	FETCH NEXT FROM tablecursor INTO @TableID, @TableName 
	WHILE (@@FETCH_STATUS = 0) BEGIN

	        IF EXISTS(SELECT * FROM sysobjects where xtype='TR' and name=@TableName + 'AuditInsert') BEGIN
			PRINT 'DROP TRIGGER ' + @TableName + 'AuditInsert'
			PRINT 'GO'
            		IF @DryRun = 0 EXECUTE('DROP TRIGGER ' + @TableName + 'AuditInsert')
		END
	        IF EXISTS(SELECT * FROM sysobjects where xtype='TR' and name=@TableName + 'AuditDelete') BEGIN
			PRINT 'DROP TRIGGER ' + @TableName + 'AuditDelete'
			PRINT 'GO'
        		IF @DryRun = 0 EXECUTE('DROP TRIGGER ' + @TableName + 'AuditDelete')
		END
        	IF EXISTS(SELECT * FROM sysobjects where xtype='TR' and name=@TableName + 'AuditUpdate') BEGIN
			PRINT 'DROP TRIGGER ' + @TableName + 'AuditUpdate'
			PRINT 'GO'
            		IF @DryRun = 0 EXECUTE('DROP TRIGGER ' + @TableName + 'AuditUpdate')
		END

		SELECT @AuditTableName = 'Audit' + @TableName 

		IF exists (select * from dbo.sysobjects where id = object_id(@AuditTableName) and OBJECTPROPERTY(id, N'IsUserTable') = 1)
		BEGIN
			-- Drop trigger for disabling fiddling with the Audit Table itself.
			SET @AuditTrigger = 'DROP TRIGGER dbo.' + @AuditTableName + 'PreventAlter'
			PRINT @AuditTrigger
			PRINT 'GO'
			IF @DryRun = 0 EXECUTE (@AuditTrigger)
	
			PRINT 'DROP TABLE ' +  @AuditTableName + ' ' 
			PRINT 'GO'
			IF @DryRun = 0 EXECUTE ('DROP TABLE dbo.' +  @AuditTableName + '')
		END

 		FETCH NEXT FROM tablecursor INTO @TableID, @TableName 
	END 
	CLOSE tablecursor 
	DEALLOCATE tablecursor

	IF @DryRun <> 0 PRINT '
-- Dry run completed successfully'
	ELSE PRINT '
-- Audit tables and triggers dropped'

RETURN

GO
CREATE RULE CheckPdaCustomFormType AS (@Value IN ('C', 'R', 'J', 'c','r','j'))

GO
CREATE RULE CheckPdaLabelPosition AS (@Value IN ('L', 'R', 'T', 'N', 'l','r','t','n'))

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE CustomLabel(
	CustomLabelID int IDENTITY(1,1) NOT NULL,
	FormName nvarchar(50) NOT NULL,
	FieldName nvarchar(50) NOT NULL,
	Label nvarchar(255) NULL,
	IsClientLabel bit NOT NULL,
	IsConsoleLabel bit NOT NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_CustomLabel PRIMARY KEY CLUSTERED ( CustomLabelID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE EventType(
	EventTypeID int NOT NULL,
	EventType varchar(25) NULL,
	EventDescription varchar(255) NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
CONSTRAINT PK_EventType PRIMARY KEY CLUSTERED ( EventTypeID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE CustomTerminology(
	CustomTerminologyID int IDENTITY(1,1) NOT NULL,
	Entity nvarchar(50) NOT NULL,
	EntityPlural nvarchar(50) NOT NULL,
	Name nvarchar(50) NOT NULL,
	PluralName nvarchar(50) NOT NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_CustomTerminology PRIMARY KEY NONCLUSTERED ( CustomTerminologyID ASC )) 
GO
CREATE UNIQUE CLUSTERED INDEX IX_CustomTerminology ON CustomTerminology (Entity ASC)
GO

CREATE DEFAULT DefaultPdaCustomFormType AS ('J')

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION GetServerTimeZone ()
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int

	-- Add the T-SQL statements to compute the return value here
	exec master.dbo.xp_regread 'HKEY_LOCAL_MACHINE', 'SYSTEM\CurrentControlSet\Control\TimeZoneInformation', 'ActiveTimeBias', @Result OUT

	-- Return the result of the function
	RETURN @Result

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION ToLocalTime 
(
	-- Add the parameters for the function here
	@value datetime
)
RETURNS datetime
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result datetime

	-- Add the T-SQL statements to compute the return value here
	declare @deltaGMT int 
	exec master.dbo.xp_regread 'HKEY_LOCAL_MACHINE', 'SYSTEM\CurrentControlSet\Control\TimeZoneInformation', 'ActiveTimeBias', @DeltaGMT OUT
	select @Result = dateadd(minute, -@deltaGMT, @value)

	-- Return the result of the function
	RETURN @Result

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION ToUniversalTime 
(
	-- Add the parameters for the function here
	@value datetime
)
RETURNS datetime
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result datetime

	-- Add the T-SQL statements to compute the return value here
	declare @deltaGMT int 
	exec master.dbo.xp_regread 'HKEY_LOCAL_MACHINE', 'SYSTEM\CurrentControlSet\Control\TimeZoneInformation', 'ActiveTimeBias', @DeltaGMT OUT
	select @Result = dateadd(minute, @deltaGMT, @value)

	-- Return the result of the function
	RETURN @Result

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE ErrorLog(
	ErrorLogID int IDENTITY(1,1) NOT NULL,
	LogDateTime datetime NOT NULL DEFAULT (GETUTCDATE()),
	ErrorDateTime datetime NULL DEFAULT (GETUTCDATE()),
	DeviceID varchar(100) NULL,
	ConsultantUID uniqueidentifier NULL,
	Source nvarchar(500) NULL,
	Message nvarchar(1000) NULL,
	TableName varchar(200) NULL,
	TableUID uniqueidentifier NULL,
	TableData nvarchar(max) NULL,
	ExceptionData nvarchar(max) NULL,
 CONSTRAINT PK_Error PRIMARY KEY CLUSTERED ( ErrorLogID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spCreateTimeStampTriggers (
    @DryRun BIT = 0, 
    @Verbose BIT = 0,
    @Quiet BIT = 0,
	@SpecificTable NVARCHAR(255) = NULL
) AS

	SET Nocount ON

	IF @Quiet = 0 PRINT 'Generating timestamp triggers for relevant tables...'

	IF NOT @SpecificTable IS NULL
		DECLARE Tablecursor CURSOR FOR 
		SELECT id,Name FROM Dbo.Sysobjects WHERE (Xtype = 'U') 
			AND Name LIKE @SpecificTable
	ELSE
		DECLARE Tablecursor CURSOR FOR 
		SELECT id,Name FROM Dbo.Sysobjects WHERE (Xtype = 'U') 
		AND NOT (
			Name LIKE 'Audit%' 
			OR Name LIKE '%Log'
			OR Name LIKE 'Gateway%'
			OR Name = 'dtproperties'
			OR Name LIKE 'Trace%' 
		)

	DECLARE @TableName NVARCHAR(128) 
	DECLARE @TableID INT

	DECLARE @TriggerName NVARCHAR(128)
	DECLARE @Trigger NVARCHAR(4000)

	DECLARE @UpdateTemplate NVARCHAR(4000)
	DECLARE @InsertTemplate NVARCHAR(4000)

	SET @InsertTemplate = '
CREATE TRIGGER dbo.{##TRIGGERNAME} ON dbo.{##TABLENAME}
   AFTER INSERT
	NOT FOR REPLICATION
AS 
BEGIN
	SET NOCOUNT ON;
	IF ( (SELECT trigger_nestlevel() ) > 1) RETURN
	DECLARE @now DATETIME
	SET @now=GETUTCDATE()
	IF (NOT UPDATE(ModifiedDateTime)) OR (NOT UPDATE(CreatedDateTime))
		UPDATE {##TABLENAME} SET ModifiedDateTime = @now, CreatedDateTime = @now FROM {##JOIN}
	ELSE
		UPDATE {##TABLENAME} SET ModifiedDateTime = @now, CreatedDateTime = @now FROM {##JOIN} WHERE Inserted.ModifiedDateTime IS NULL OR Inserted.CreatedDateTime IS NULL
END
'

	SET @UpdateTemplate = '
CREATE TRIGGER dbo.{##TRIGGERNAME} ON dbo.{##TABLENAME}
   AFTER UPDATE
	NOT FOR REPLICATION
AS 
BEGIN
	SET NOCOUNT ON;
	IF ( (SELECT trigger_nestlevel() ) > 1) RETURN
	DECLARE @now DATETIME
	SET @now=GETUTCDATE()
	IF (NOT UPDATE(ModifiedDateTime))
		UPDATE {##TABLENAME} SET ModifiedDateTime = @now FROM {##JOIN}
	ELSE
		UPDATE {##TABLENAME} SET ModifiedDateTime = @now FROM {##JOIN} WHERE Inserted.ModifiedDateTime IS NULL 
END
'

	DECLARE @err INT
    DECLARE @timestampRows INT

	OPEN Tablecursor
	FETCH Next FROM Tablecursor INTO @TableID, @TableName 
	WHILE (@@FETCH_STATUS = 0) BEGIN

		SELECT @timestampRows=COUNT(*) FROM Syscolumns WHERE Id=@TableID AND (name='ModifiedDateTime' OR name='CreatedDateTime') AND xtype=61
        IF @timestampRows = 2 BEGIN
			DECLARE @JOIN NVARCHAR(2000)
			DECLARE @ColumnName Sysname

			SELECT @JOIN = 'INSERTED JOIN ' + @TableName 
			DECLARE Pkcursor CURSOR FOR
				SELECT Dbo.Syscolumns.NAME
				FROM   Dbo.Sysobjects AS Tables INNER JOIN
					   Dbo.Sysobjects AS Primarykeys ON Tables.Id = Primarykeys.Parent_obj INNER JOIN
					   Dbo.Sysindexes ON Tables.Id = Dbo.Sysindexes.Id AND Primarykeys.NAME = Dbo.Sysindexes.NAME INNER JOIN
					   Dbo.Sysindexkeys ON Tables.Id = Dbo.Sysindexkeys.Id AND Dbo.Sysindexes.Indid = Dbo.Sysindexkeys.Indid INNER JOIN
					   Dbo.Syscolumns ON Tables.Id = Dbo.Syscolumns.Id AND Dbo.Sysindexkeys.Colid = Dbo.Syscolumns.Colid
				WHERE   (Tables.Xtype = 'U') AND (Tables.Id = @TableID) AND (Primarykeys.Xtype = 'PK')
			OPEN Pkcursor
			IF @@CURSOR_ROWS <> 0 SELECT @JOIN = @JOIN + ' ON '
			FETCH Next FROM Pkcursor INTO @ColumnName 
			WHILE @@FETCH_STATUS = 0 BEGIN
				SELECT @JOIN = @JOIN + 'INSERTED.' + @ColumnName + ' = dbo.' + @TableName + '.' + @ColumnName + ''
				FETCH Next FROM Pkcursor INTO @ColumnName 
				IF @@FETCH_STATUS = 0 SELECT @JOIN = @JOIN + ' AND '
			END
			CLOSE Pkcursor
			DEALLOCATE Pkcursor

			SELECT @TriggerName=Replace('TR_{##TABLENAME}_InsertTimestamp', '{##TABLENAME}', @TableName)
			IF EXISTS(SELECT * FROM Sysobjects WHERE Xtype='TR' AND NAME=@TriggerName) BEGIN
				IF @Verbose = 1 BEGIN
					PRINT 'DROP TRIGGER dbo.' +  @TriggerName + ' ' 
					PRINT 'GO'
				END
				IF @DryRun = 0 EXECUTE ('DROP TRIGGER ' +  @TriggerName + '')
			END

			SELECT @Trigger = Replace ( @InsertTemplate, '{##TABLENAME}', @TableName )
			SELECT @Trigger = Replace ( @Trigger, '{##TRIGGERNAME}', @TriggerName )
			SELECT @Trigger = Replace ( @Trigger, '{##JOIN}', @JOIN)
			IF @Quiet = 0 PRINT '-- Adding ' + @TriggerName + '...'
			IF @Verbose = 1 BEGIN
				PRINT ( @Trigger )
				PRINT 'GO'
            END
			IF @DryRun = 0 EXECUTE ( @Trigger )

			SELECT @TriggerName=Replace('TR_{##TABLENAME}_UpdateTimestamp', '{##TABLENAME}', @TableName)
			IF EXISTS(SELECT * FROM Sysobjects WHERE Xtype='TR' AND NAME=@TriggerName) BEGIN
				IF @Verbose = 1 BEGIN
					PRINT 'DROP TRIGGER dbo.' +  @TriggerName + ' ' 
					PRINT 'GO'
				END
				IF @DryRun = 0 EXECUTE ('DROP TRIGGER ' +  @TriggerName + '')
			END

			SELECT @Trigger = Replace ( @UpdateTemplate, '{##TABLENAME}', @TableName )
			SELECT @Trigger = Replace ( @Trigger, '{##TRIGGERNAME}', @TriggerName )
			SELECT @Trigger = Replace ( @Trigger, '{##JOIN}', @JOIN )

			IF @Quiet = 0 PRINT '-- Adding ' + @TriggerName + '...'
			IF @Verbose = 1 BEGIN
				PRINT ( @Trigger )
				PRINT 'GO'
            END
			IF @DryRun = 0 EXECUTE ( @Trigger )
		END
		ELSE BEGIN
			IF @Quiet = 0 PRINT 'Skipping table : ' + @TableName + ' due to missing timestamp columns'
		END
 		FETCH Next FROM Tablecursor INTO @TableID,@TableName 
	END 
	CLOSE Tablecursor 
	DEALLOCATE Tablecursor
 RETURN

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE CustomControlType(
	CustomControlTypeID tinyint NOT NULL,
	Description nvarchar(50) NOT NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_CustomControlType PRIMARY KEY CLUSTERED ( CustomControlTypeID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE RequestStatus(
	RequestStatusID int NOT NULL,
	Description nvarchar(50) NOT NULL,
	DisplayOrder int NULL DEFAULT (0),
	ColourRed tinyint NULL DEFAULT (0),
	ColourGreen tinyint NULL DEFAULT (0),
	ColourBlue tinyint NULL DEFAULT (0),
	IsClientStatus bit NOT NULL DEFAULT (0),
	IsClientMenuItem bit NOT NULL DEFAULT (0),
	IsReasonRequired bit NOT NULL DEFAULT (0),
	IsNewStatus bit NOT NULL DEFAULT (0),
	IsInProgressStatus bit NOT NULL DEFAULT (0),
	IsCompleteStatus bit NOT NULL DEFAULT (0),
	IsCancelledStatus bit NOT NULL DEFAULT (0),
	StateCode nvarchar(20) NULL,
	StatusCode nvarchar(20) NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_RequestStatus PRIMARY KEY CLUSTERED ( RequestStatusID ASC )) 

GO
CREATE DEFAULT DefaultPdaLabelPosition AS 'L'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE ClientSiteStatus(
	ClientSiteStatusID int NOT NULL,
	Description nvarchar(250) NOT NULL,
	IsActive bit NOT NULL DEFAULT (1),
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_ClientSiteStatus PRIMARY KEY CLUSTERED ( ClientSiteStatusID ASC )) 

GO
CREATE RULE CheckPdaControlPosition AS @Value IN ('L', 'R', 'F', 'l','r','f')

GO
CREATE DEFAULT DefaultPdaControlPosition AS 'F'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ryan C. Price
-- Create date: 2006-05-12
-- Description:	Get date portion of date/time value
-- =============================================
CREATE FUNCTION DateOnly
(@datetime datetime) RETURNS datetime AS
BEGIN
	DECLARE @dateonly datetime
	IF @datetime = NULL
		SET @dateonly = @datetime 
	ELSE
		SET @dateonly = cast(floor(cast(@datetime as float)) as datetime)
	RETURN @dateonly	
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ryan C. Price
-- Create date: 2006-05-12
-- Description:	Get time portion of date/time value
-- =============================================
CREATE FUNCTION TimeOnly
(@datetime datetime) RETURNS datetime AS
BEGIN
	DECLARE @dateonly datetime
	IF @datetime = NULL
		SET @dateonly = @datetime 
	ELSE
		SET @dateonly = @datetime - cast(floor(cast(@datetime as float)) as datetime)
	RETURN @dateonly	
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Consultant(
	ConsultantUID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	ConsultantID int NULL,
	ConsultantNumber nvarchar(100) NULL,
	Name nvarchar(250) NOT NULL,
	Password varchar(50) NULL,
	MinSyncTime int NULL DEFAULT (0),
	LastSyncTime datetime NULL,
	SyncTime datetime NULL,
	EmailAddress varchar(100) NULL,
	MobilePhone varchar(20) NULL,
	MobileAlert bit NOT NULL DEFAULT (0),
	IsActiviserUser bit NOT NULL DEFAULT (1),
	Employee bit NOT NULL DEFAULT (1),
	Engineer bit NOT NULL DEFAULT (0),
	Management bit NOT NULL DEFAULT (0),
	Administration bit NOT NULL DEFAULT (0),
	DomainLogon nvarchar(250) NULL,
	WorkstationName nvarchar(250) NULL,
	StartDate datetime NULL,
	FinishDate datetime NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	AutoNumber int IDENTITY(1,1) NOT NULL,
 CONSTRAINT PK_Consultant PRIMARY KEY NONCLUSTERED 
( ConsultantUID ASC )) 

GO

CREATE UNIQUE NONCLUSTERED INDEX IX_ConsultantID ON Consultant 
( ConsultantID ASC
) 
GO

CREATE UNIQUE CLUSTERED INDEX IX_ConsultantName ON Consultant 
( Name ASC
) 
GO

CREATE TRIGGER TR_Consultant_InsertConsultantID ON Consultant AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	UPDATE	Consultant
	SET     ConsultantID = INSERTED.AutoNumber
	FROM    Consultant INNER JOIN INSERTED ON Consultant.ConsultantUID = INSERTED.ConsultantUID
	WHERE   (Consultant.ConsultantID IS NULL) OR (Consultant.ConsultantID < 0)

	UPDATE  Consultant
	SET     ConsultantNumber = 'C' + REPLACE(STR(INSERTED.AutoNumber,4),' ', '0')
	FROM    Consultant INNER JOIN INSERTED ON Consultant.ConsultantUID = INSERTED.ConsultantUID
	WHERE   (Consultant.ConsultantNumber IS NULL) OR (Consultant.ConsultantNumber = '')
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE EventLog(
	EventLogID int IDENTITY(1,1) NOT NULL,
	UTCEventTime datetime NULL DEFAULT (GETUTCDATE()),
	ConsultantUID uniqueidentifier NULL,
	RequestUID uniqueidentifier NULL,
	JobUID uniqueidentifier NULL,
	ClientSiteUID uniqueidentifier NULL,
	EventTypeID int NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_EventLog PRIMARY KEY CLUSTERED ( EventLogID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE CustomControlData(
	CustomControlDataUID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	CustomControlUID uniqueidentifier NOT NULL,
	DataValue nvarchar(128) NOT NULL,
	DisplayValue nvarchar(255) NOT NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_CustomControlData PRIMARY KEY (CustomControlDataUID ASC )) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Job(
	JobUID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	JobID int NULL,
	JobNumber nvarchar(100) NULL,
	ConsultantJobID int NULL,
	RequestID int NULL,
	RequestUID uniqueidentifier NULL,
	ConsultantID int NULL,
	ConsultantUID uniqueidentifier NULL,
	ClientSiteID int NULL,
	ClientSiteUID uniqueidentifier NULL,
	JobDate datetime NULL,
	ReturnDate datetime NULL,
	StartTime datetime NULL,
	FinishTime datetime NULL,
	JobDetails nvarchar(3500) NULL,
	JobNotes nvarchar(max) NULL,
	MinutesTravelled int NULL DEFAULT (0),
	MinutesWorked int NULL DEFAULT (0),
	PremiumMinutesWorked int NULL DEFAULT (0),
	Signature varchar(max) NULL,
	Signatory nvarchar(50) NULL,
	Flag int NULL DEFAULT (0),
	JobStatusID int NULL DEFAULT (0),
	Equipment nvarchar(max) NULL,
	Email nvarchar(100) NULL,
	EmailStatus tinyint NULL DEFAULT (0),
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	Age  AS (DATEDIFF(DAY,StartTime,GETUTCDATE())),
	AutoNumber int IDENTITY(1,1) NOT NULL,
	CONSTRAINT PK_Job PRIMARY KEY NONCLUSTERED (JobUID ASC )
) 
GO

CREATE CLUSTERED INDEX IX_JobRequest ON Job (RequestUID ASC) 
GO

CREATE NONCLUSTERED INDEX IX_JobID ON Job (JobID ASC) 
GO

CREATE TRIGGER dbo.TR_Job_InsertJobID 
   ON  dbo.Job 
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	UPDATE	Job
	SET     JobID = INSERTED.AutoNumber
	FROM    Job INNER JOIN INSERTED ON Job.JobUID = INSERTED.JobUID
	WHERE   (Job.JobID IS NULL) OR (Job.JobID < 0)

	UPDATE	Job
	SET     JobNumber = 'J' + REPLACE(STR(INSERTED.AutoNumber,6),' ', '0')
	FROM    Job INNER JOIN INSERTED ON Job.JobUID = INSERTED.JobUID
	WHERE   (Job.JobNumber IS NULL) OR (Job.JobNumber = '')
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE InterestedConsultant(
	InterestedConsultantUID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	RequestUID uniqueidentifier NOT NULL,
	ConsultantUID uniqueidentifier NOT NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_InterestedConsultant PRIMARY KEY NONCLUSTERED (InterestedConsultantUID ASC),
 CONSTRAINT UC_InterestedConsultant UNIQUE CLUSTERED (RequestUID ASC, ConsultantUID ASC)) 

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Request(
	RequestUID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	RequestID int NULL,
	RequestNumber nvarchar(100) NULL,
	ConsultantRID int NULL,
	ConsultantStatusID int NULL,
	ClientSiteID int NULL,
	ClientSiteUID uniqueidentifier NULL,
	AssignedToID int NULL,
	AssignedToUID uniqueidentifier NULL,
	Contact nvarchar(100) NULL,
	LongDescription nvarchar(3500) NULL,
	ShortDescription nvarchar(200) NULL,
	RequestStatusID int NULL,
	NextActionDate datetime NULL,
	CompletedDate datetime NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	AutoNumber int IDENTITY(1,1) NOT NULL,
 CONSTRAINT PK_Request PRIMARY KEY NONCLUSTERED 
( RequestUID ASC )) 
GO

CREATE CLUSTERED INDEX IX_RequestNumber ON Request (RequestNumber ASC) 
GO

CREATE TRIGGER TR_Request_InsertRequestID ON  Request AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	UPDATE Request
	SET    RequestID = INSERTED.AutoNumber
	FROM   Request INNER JOIN INSERTED ON Request.RequestUID = INSERTED.RequestUID
	WHERE  (Request.RequestID IS NULL) OR (Request.RequestID < 0)

	UPDATE Request
	SET    RequestNumber = 'R' + REPLACE(STR(INSERTED.AutoNumber,5),' ', '0')
	FROM   Request INNER JOIN INSERTED ON Request.RequestUID = INSERTED.RequestUID
	WHERE  (Request.RequestNumber IS NULL) OR (Request.RequestNumber = '')
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE ClientSite(
	ClientSiteUID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	ClientSiteID int NULL,
	ClientSiteNumber nvarchar(100) NULL,
	SiteName nvarchar(250) NULL,
	SiteAddress nvarchar(250) NULL,
	SiteContactEmail nvarchar(100) NULL,
	SiteNotes nvarchar(2000) NULL,
	Contact nvarchar(100) NULL,
	ContactPhone1 nvarchar(50) NULL,
	ContactPhone2 nvarchar(50) NULL,
	Hold bit NULL DEFAULT(0),
	ClientSiteStatusID int NULL,
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	AutoNumber int IDENTITY(1,1) NOT NULL,
 CONSTRAINT PK_ClientSite PRIMARY KEY NONCLUSTERED 
( ClientSiteUID ASC )) 

GO

CREATE UNIQUE CLUSTERED INDEX IX_ClientSiteName ON ClientSite 
( SiteName ASC
) 
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_ClientSiteNumber ON ClientSite 
( ClientSiteNumber ASC
) 
GO

CREATE TRIGGER TR_ClientSite_InsertClientSiteID ON ClientSite AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	UPDATE	ClientSite
	SET     ClientSiteID = INSERTED.AutoNumber
	FROM    ClientSite INNER JOIN INSERTED ON ClientSite.ClientSiteUID = INSERTED.ClientSiteUID
	WHERE   (ClientSite.ClientSiteID IS NULL) OR (ClientSite.ClientSiteID < 0)

	UPDATE	ClientSite
	SET     ClientSiteNumber = 'S' + REPLACE(STR(INSERTED.AutoNumber,4),' ', '0')
	FROM    ClientSite INNER JOIN INSERTED ON ClientSite.ClientSiteUID = INSERTED.ClientSiteUID
	WHERE   (ClientSite.ClientSiteNumber IS NULL) OR (ClientSite.ClientSiteNumber = '')
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE ConsultantProfile(
	ID int IDENTITY(1,1) NOT NULL,
	ConsultantID int NULL,
	ConsultantUID uniqueidentifier NOT NULL,
	ItemType char(1) NOT NULL,
	ItemUID uniqueidentifier NOT NULL,
	ItemInt int NULL,
	ItemDate datetime NULL,
	ItemText varchar(250) NULL,
	ItemDeleted bit NOT NULL DEFAULT (0),
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_ConsultantProfile 
	PRIMARY KEY CLUSTERED(ConsultantUID,ItemType,ItemUID))
GO

CREATE INDEX IX_ConsultantProfile_Consultant ON ConsultantProfile(ConsultantUID)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW Jobs
AS
SELECT     *
FROM         dbo.Job

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW Requests
AS
SELECT     *
FROM         dbo.Request

GO
EXEC dbo.sp_addtype N'PdaCustomFormType', N'char(1)',N'not null'

GO
EXEC dbo.sp_bindefault @defname=N'DefaultPdaCustomFormType', @objname=N'PdaCustomFormType' , @futureonly='futureonly'
GO
EXEC dbo.sp_bindrule @rulename=N'CheckPdaCustomFormType', @objname=N'PdaCustomFormType' , @futureonly='futureonly'
GO
EXEC dbo.sp_addtype N'PdaLabelPosition', N'char(1)',N'not null'

GO
EXEC dbo.sp_bindefault @defname=N'DefaultPdaLabelPosition', @objname=N'PdaLabelPosition' , @futureonly='futureonly'
GO
EXEC dbo.sp_bindrule @rulename=N'CheckPdaLabelPosition', @objname=N'PdaLabelPosition' , @futureonly='futureonly'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ryan C. Price
-- Create date: 2006-05-12
-- Description:	Check Constraint for Request Status types
-- =============================================
CREATE FUNCTION CheckRequestStatusTypeBits()
RETURNS int
AS
BEGIN
	DECLARE @Result int
	SELECT @Result = Count(*) FROM RequestStatus 
		WHERE IsNewStatus = 1 or IsInProgressStatus = 1 or IsCompleteStatus = 1 or IsCancelledStatus = 1
	RETURN @Result
END

GO
EXEC dbo.sp_addtype N'PdaControlPosition', N'char(1)',N'not null'

GO
EXEC dbo.sp_bindefault @defname=N'DefaultPdaControlPosition', @objname=N'PdaControlPosition' , @futureonly='futureonly'
GO
EXEC dbo.sp_bindrule @rulename=N'CheckPdaControlPosition', @objname=N'PdaControlPosition' , @futureonly='futureonly'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW Consultants
AS
SELECT     *
FROM         dbo.Consultant

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE CustomForm(
	CustomFormUID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	CustomFormName nvarchar(255) NOT NULL,
	TableName nvarchar(128) NOT NULL,
	PrimaryKeyColumnName nvarchar(128) NOT NULL,
	ForeignKeyColumnName nvarchar(128) NOT NULL,
	FormType PdaCustomFormType NULL,
	ParentTableName nvarchar(128) NOT NULL DEFAULT (''),
	ParentPrimaryKeyColumnName nvarchar(128) NOT NULL,
	OneToMany tinyint NOT NULL DEFAULT (0),
	Priority tinyint NOT NULL DEFAULT (0),
	ParentFilter nvarchar(1000) NULL,
	LockWithParent bit NOT NULL DEFAULT (-1),
	IsReadOnly bit NOT NULL DEFAULT (0),
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_CustomForm PRIMARY KEY CLUSTERED ( CustomFormUID ASC )) 

GO

CREATE UNIQUE NONCLUSTERED INDEX IX_CustomFormName ON CustomForm 
( CustomFormName ASC,
	FormType ASC
) 
GO
EXEC dbo.sp_bindefault @defname=N'DefaultPdaCustomFormType', @objname=N'CustomForm.FormType' , @futureonly='futureonly'
GO
EXEC dbo.sp_bindrule @rulename=N'CheckPdaCustomFormType', @objname=N'CustomForm.FormType' , @futureonly='futureonly'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE CustomControl(
	CustomControlUID uniqueidentifier ROWGUIDCOL NOT NULL DEFAULT (newid()),
	CustomFormUID uniqueidentifier NOT NULL,
    FieldName nvarchar(128) NOT NULL,
	CustomControlTypeID tinyint NOT NULL DEFAULT (0) ,
	Label nvarchar(255) NULL,
	Sequence tinyint NOT NULL DEFAULT (0),
	SortPriority tinyint NOT NULL DEFAULT (0),
	Lines tinyint NOT NULL DEFAULT (1),
	MinimumValue int NOT NULL DEFAULT (0),
	MaximumValue int NOT NULL DEFAULT (0),
	DecimalPlaces tinyint NOT NULL DEFAULT (0),
	IsReadOnly bit NOT NULL DEFAULT (0),
	Position PdaControlPosition NOT NULL,
	WidthPercent tinyint NOT NULL DEFAULT (100),
	LabelPosition PdaLabelPosition NULL,
	ListDataSource nvarchar(128) NULL,
	ListValueColumn nvarchar(128) NULL,
	ListDisplayColumn nvarchar(128) NULL,
	ListData nvarchar(max) NULL,
	LockWithParent bit NOT NULL DEFAULT (-1),
	LabelWidthPercent tinyint NOT NULL DEFAULT (40),
	CreatedDateTime datetime NULL DEFAULT (GETUTCDATE()),
	ModifiedDateTime datetime NULL DEFAULT (GETUTCDATE()),
 CONSTRAINT PK_CustomControl PRIMARY KEY CLUSTERED ( CustomControlUID ASC )) 

GO
EXEC dbo.sp_bindefault @defname=N'DefaultPdaControlPosition', @objname=N'CustomControl.Position' , @futureonly='futureonly'
GO
EXEC dbo.sp_bindrule @rulename=N'CheckPdaControlPosition', @objname=N'CustomControl.Position' , @futureonly='futureonly'
GO
EXEC dbo.sp_bindefault @defname=N'DefaultPdaLabelPosition', @objname=N'CustomControl.LabelPosition' , @futureonly='futureonly'
GO
EXEC dbo.sp_bindrule @rulename=N'CheckPdaLabelPosition', @objname=N'CustomControl.LabelPosition' , @futureonly='futureonly'
GO
ALTER TABLE RequestStatus  WITH CHECK ADD  CONSTRAINT CK_RequestStatusBitsOK CHECK  ((IsNewStatus=(0) AND IsInProgressStatus=(0) AND IsCompleteStatus=(0) AND IsCancelledStatus=(0) OR IsNewStatus=(1) AND IsInProgressStatus=(0) AND IsCompleteStatus=(0) AND IsCancelledStatus=(0) OR IsNewStatus=(0) AND IsInProgressStatus=(1) AND IsCompleteStatus=(0) AND IsCancelledStatus=(0) OR IsNewStatus=(0) AND IsInProgressStatus=(0) AND IsCompleteStatus=(1) AND IsCancelledStatus=(0) OR IsNewStatus=(0) AND IsInProgressStatus=(0) AND IsCompleteStatus=(0) AND IsCancelledStatus=(1)))
GO
ALTER TABLE RequestStatus CHECK CONSTRAINT CK_RequestStatusBitsOK
GO
ALTER TABLE EventLog  WITH NOCHECK ADD  CONSTRAINT FK_EventLog_ClientSite FOREIGN KEY(ClientSiteUID)
REFERENCES ClientSite (ClientSiteUID)
NOT FOR REPLICATION 
GO
ALTER TABLE EventLog NOCHECK CONSTRAINT FK_EventLog_ClientSite
GO
ALTER TABLE EventLog  WITH NOCHECK ADD  CONSTRAINT FK_EventLog_Consultant FOREIGN KEY(ConsultantUID)
REFERENCES Consultant (ConsultantUID)
NOT FOR REPLICATION 
GO
ALTER TABLE EventLog NOCHECK CONSTRAINT FK_EventLog_Consultant
GO
ALTER TABLE EventLog  WITH CHECK ADD  CONSTRAINT FK_EventLog_EventType FOREIGN KEY(EventTypeID)
REFERENCES EventType (EventTypeID)
GO
ALTER TABLE EventLog CHECK CONSTRAINT FK_EventLog_EventType
GO
ALTER TABLE EventLog  WITH NOCHECK ADD  CONSTRAINT FK_EventLog_Job FOREIGN KEY(JobUID)
REFERENCES Job (JobUID)
NOT FOR REPLICATION 
GO
ALTER TABLE EventLog NOCHECK CONSTRAINT FK_EventLog_Job
GO
ALTER TABLE EventLog  WITH NOCHECK ADD  CONSTRAINT FK_EventLog_Request FOREIGN KEY(RequestUID)
REFERENCES Request (RequestUID)
NOT FOR REPLICATION 
GO
ALTER TABLE EventLog NOCHECK CONSTRAINT FK_EventLog_Request
GO
ALTER TABLE CustomControlData  WITH CHECK ADD  CONSTRAINT FK_CustomControlData_CustomControl FOREIGN KEY(CustomControlUID)
REFERENCES CustomControl (CustomControlUID)
GO
ALTER TABLE CustomControlData CHECK CONSTRAINT FK_CustomControlData_CustomControl
GO
ALTER TABLE Job  WITH CHECK ADD  CONSTRAINT FK_Job_ClientSite FOREIGN KEY(ClientSiteUID)
REFERENCES ClientSite (ClientSiteUID)
GO
ALTER TABLE Job CHECK CONSTRAINT FK_Job_ClientSite
GO
ALTER TABLE Job  WITH CHECK ADD  CONSTRAINT FK_Job_Consultant FOREIGN KEY(ConsultantUID)
REFERENCES Consultant (ConsultantUID)
GO
ALTER TABLE Job CHECK CONSTRAINT FK_Job_Consultant
GO
ALTER TABLE Job  WITH CHECK ADD  CONSTRAINT FK_Job_JobStatus FOREIGN KEY(JobStatusID)
REFERENCES JobStatus (JobStatusID)
ON UPDATE CASCADE
GO
ALTER TABLE Job CHECK CONSTRAINT FK_Job_JobStatus
GO
ALTER TABLE Job  WITH CHECK ADD  CONSTRAINT FK_Job_Request FOREIGN KEY(RequestUID)
REFERENCES Request (RequestUID)
GO
ALTER TABLE Job CHECK CONSTRAINT FK_Job_Request
GO
ALTER TABLE InterestedConsultant  WITH CHECK ADD  CONSTRAINT FK_InterestedConsultant_Consultant FOREIGN KEY(ConsultantUID)
REFERENCES Consultant (ConsultantUID)
GO
ALTER TABLE InterestedConsultant CHECK CONSTRAINT FK_InterestedConsultant_Consultant
GO
ALTER TABLE InterestedConsultant  WITH CHECK ADD  CONSTRAINT FK_InterestedConsultant_Request FOREIGN KEY(RequestUID)
REFERENCES Request (RequestUID)
GO
ALTER TABLE InterestedConsultant CHECK CONSTRAINT FK_InterestedConsultant_Request
GO
ALTER TABLE Request  WITH CHECK ADD  CONSTRAINT FK_Request_ClientSite FOREIGN KEY(ClientSiteUID)
REFERENCES ClientSite (ClientSiteUID)
GO
ALTER TABLE Request CHECK CONSTRAINT FK_Request_ClientSite
GO
ALTER TABLE Request  WITH CHECK ADD  CONSTRAINT FK_Request_Consultant FOREIGN KEY(AssignedToUID)
REFERENCES Consultant (ConsultantUID)
GO
ALTER TABLE Request CHECK CONSTRAINT FK_Request_Consultant
GO
ALTER TABLE Request  WITH CHECK ADD  CONSTRAINT FK_Request_ConsultantStatus FOREIGN KEY(ConsultantStatusID)
REFERENCES RequestStatus (RequestStatusID)
ON UPDATE CASCADE
GO
ALTER TABLE Request CHECK CONSTRAINT FK_Request_ConsultantStatus
GO
ALTER TABLE Request  WITH CHECK ADD  CONSTRAINT FK_Request_RequestStatus FOREIGN KEY(RequestStatusID)
REFERENCES RequestStatus (RequestStatusID)
GO
ALTER TABLE Request CHECK CONSTRAINT FK_Request_RequestStatus
GO
ALTER TABLE ClientSite  WITH CHECK ADD  CONSTRAINT FK_ClientSite_ClientSiteStatus FOREIGN KEY(ClientSiteStatusID)
REFERENCES ClientSiteStatus (ClientSiteStatusID)
GO
ALTER TABLE ClientSite CHECK CONSTRAINT FK_ClientSite_ClientSiteStatus
GO
ALTER TABLE ConsultantProfile  WITH CHECK ADD  CONSTRAINT FK_ConsultantProfile_Consultant FOREIGN KEY(ConsultantUID)
REFERENCES Consultant (ConsultantUID)
GO
ALTER TABLE ConsultantProfile CHECK CONSTRAINT FK_ConsultantProfile_Consultant
GO
ALTER TABLE CustomControl  WITH CHECK ADD  CONSTRAINT FK_CustomControl_CustomControlType FOREIGN KEY(CustomControlTypeID)
REFERENCES CustomControlType (CustomControlTypeID)
ON UPDATE CASCADE
GO
ALTER TABLE CustomControl CHECK CONSTRAINT FK_CustomControl_CustomControlType
GO
ALTER TABLE CustomControl  WITH CHECK ADD  CONSTRAINT FK_CustomControl_CustomForm FOREIGN KEY(CustomFormUID)
REFERENCES CustomForm (CustomFormUID)
GO
ALTER TABLE CustomControl CHECK CONSTRAINT FK_CustomControl_CustomForm
GO

CREATE VIEW viewActiveJobList
AS
SELECT     JobUID, RequestUID
FROM         dbo.Job
WHERE     (JobStatusID = 0) AND (NOT (RequestUID IS NULL)) OR
                      (ModifiedDateTime >= DATEADD(d, - 100, getutcdate())) AND (NOT (RequestUID IS NULL))
GO

CREATE VIEW viewActiveRequestJobList
AS
SELECT     dbo.Job.JobUID
FROM         dbo.Job LEFT OUTER JOIN
                      dbo.Request ON dbo.Request.RequestUID = dbo.Job.RequestUID LEFT OUTER JOIN
                      dbo.RequestStatus ON dbo.RequestStatus.RequestStatusID = dbo.Request.RequestStatusID
WHERE     (dbo.RequestStatus.IsInProgressStatus <> 0) AND (dbo.RequestStatus.IsClientStatus <> 0) OR
                      (dbo.RequestStatus.IsClientStatus <> 0) AND (dbo.RequestStatus.IsNewStatus <> 0) OR
                      (dbo.Request.ModifiedDateTime > DATEADD(d, - 100, getutcdate()))
GO

CREATE VIEW viewActiveRequestList
AS
SELECT     dbo.Request.RequestUID
FROM         dbo.Request LEFT OUTER JOIN
                          (SELECT DISTINCT RequestUID
                            FROM          dbo.viewActiveJobList) AS ActiveJobRequests ON dbo.Request.RequestUID = ActiveJobRequests.RequestUID LEFT OUTER JOIN
                      dbo.RequestStatus ON dbo.RequestStatus.RequestStatusID = dbo.Request.RequestStatusID
WHERE     (dbo.RequestStatus.IsInProgressStatus <> 0) AND (dbo.RequestStatus.IsClientStatus <> 0) OR
                      (dbo.RequestStatus.IsClientStatus <> 0) AND (dbo.RequestStatus.IsNewStatus <> 0) OR
                      (dbo.Request.ModifiedDateTime > DATEADD(d, - 100, getutcdate())) OR
                      (ActiveJobRequests.RequestUID IS NOT NULL)
GO

CREATE VIEW viewActiveRequestStatusIDs
AS
SELECT     RequestStatusID
FROM         dbo.RequestStatus
WHERE     (IsNewStatus = 1) OR
                      (IsInProgressStatus = 1)
GO

CREATE FUNCTION GetConsultantProfileItems 
(	
	@ConsultantUID UNIQUEIDENTIFIER, 
	@ItemType CHAR(1)
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT DISTINCT ItemUID, ItemInt, ItemDate, ItemText FROM ConsultantProfile WHERE ConsultantUID = @ConsultantUID AND ItemType=@ItemType AND ItemDeleted=0
)
GO

CREATE FUNCTION GetConsultantActiveRequests 
(	
	-- Add the parameters for the function here
	@ConsultantUid UNIQUEIDENTIFIER, 
	@ModifiedSince DATETIME = NULL
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT	Request.RequestUID
	FROM	Request LEFT OUTER JOIN
			RequestStatus ON Request.RequestStatusID = RequestStatus.RequestStatusID LEFT OUTER JOIN
            InterestedConsultant ON Request.RequestUID = InterestedConsultant.RequestUID AND InterestedConsultant.ConsultantUID = @ConsultantUID 
	WHERE	((Request.AssignedToUID = @ConsultantUID) OR (InterestedConsultant.RequestUID IS NOT NULL)) AND
			((@ModifiedSince IS NULL) OR (Request.ModifiedDateTime >= @ModifiedSince) OR (InterestedConsultant.ModifiedDateTime >= @ModifiedSince)) AND
			((RequestStatus.IsInProgressStatus <> 0) OR (RequestStatus.IsNewStatus <> 0))

)
GO

CREATE FUNCTION GetClientSiteRequestHistory 
(
	@ClientSiteUid uniqueidentifier, 
	@ConsultantUid uniqueidentifier, 
	@HistoryDays int,
	@HistoryNumber int
)
RETURNS TABLE
AS 
RETURN 
(
SELECT DISTINCT 
                      Request.RequestUID, Request.RequestID, Request.RequestNumber, Request.ConsultantRID, Request.ConsultantStatusID, Request.ClientSiteUID, 
                      Request.AssignedToUID, Request.Contact, Request.LongDescription, Request.ShortDescription, Request.RequestStatusID, Request.NextActionDate, 
                      Request.CompletedDate, Request.CreatedDateTime, Request.ModifiedDateTime
FROM         Request RIGHT OUTER JOIN
                          (SELECT     TOP (@HistoryNumber) Job.RequestUID, Job.StartTime
                            FROM          Job LEFT OUTER JOIN
                                                   dbo.GetConsultantProfileItems(@ConsultantUID, 'J') AS ConsultantJobItems ON Job.JobUID = ConsultantJobItems.ItemUID
                            WHERE      (Job.ClientSiteUID = @ClientSiteUID) AND (Job.RequestUID IS NOT NULL) AND (Job.JobStatusID <> 6) AND (Job.Age <= @HistoryDays) 
                                                   AND (ConsultantJobItems.ItemUID IS NULL)
                            ORDER BY Job.StartTime DESC) AS ConsultantJobItemRequests ON 
                      ConsultantJobItemRequests.RequestUID = Request.RequestUID LEFT OUTER JOIN
                      dbo.GetConsultantProfileItems(@ConsultantUID, 'R') AS ConsultantRequestItems ON Request.RequestUID = ConsultantRequestItems.ItemUID
WHERE     (ConsultantRequestItems.ItemUID IS NULL)
)
GO

CREATE FUNCTION GetClientSiteJobHistory 
(
	@ClientSiteUid uniqueidentifier, 
	@ConsultantUid uniqueidentifier, 
	@HistoryDays int,
	@HistoryNumber int
)
RETURNS TABLE 
AS
RETURN 
(
SELECT     TOP (@HistoryNumber) Job.JobUID, Job.JobID, Job.JobNumber, Job.ConsultantJobID, Job.RequestID, Job.RequestUID, Job.ConsultantID, 
                      Job.ConsultantUID, Job.ClientSiteID, Job.ClientSiteUID, Job.JobDate, Job.ReturnDate, Job.StartTime, Job.FinishTime, Job.JobDetails, Job.JobNotes, 
                      Job.MinutesTravelled, Job.MinutesWorked, Job.PremiumMinutesWorked, Job.Signature, Job.Signatory, Job.Flag, Job.JobStatusID, Job.Equipment, 
                      Job.Email, Job.EmailStatus, Job.CreatedDateTime, Job.ModifiedDateTime, Job.Age
FROM         Job LEFT OUTER JOIN
                      dbo.GetConsultantProfileItems(@ConsultantUID, 'J') AS ConsultantJobItems ON Job.JobUID = ConsultantJobItems.ItemUID
WHERE     (Job.ClientSiteUID = @ClientSiteUID) AND (Job.RequestUID IS NOT NULL) AND (Job.JobStatusID <> 6) AND (ConsultantJobItems.ItemUID IS NULL) AND 
                      (Job.Age <= @HistoryDays)
ORDER BY Job.StartTime DESC
)
GO

CREATE PROCEDURE dbo.spCreateTimeStampIndexes (
    @DryRun BIT = 0, 
    @Verbose BIT = 0,
    @Quiet BIT = 0,
	@SpecificTable NVARCHAR(255) = NULL
) AS

	SET Nocount ON

	IF @Quiet = 0 PRINT 'Generating timestamp indexes for relevant tables...'

	IF NOT @SpecificTable IS NULL
		DECLARE Tablecursor CURSOR FOR 
		SELECT id,Name FROM Dbo.Sysobjects WHERE (Xtype = 'U') 
			AND Name LIKE @SpecificTable
	ELSE
		DECLARE Tablecursor CURSOR FOR 
		SELECT id,Name FROM Dbo.Sysobjects WHERE (Xtype = 'U') 
		AND NOT (
			Name LIKE 'Audit%' 
--			OR Name LIKE '%Log'
			OR Name LIKE 'Gateway%'
			OR Name = 'dtproperties'
			OR Name LIKE 'Trace%' 
		)

	DECLARE @TableName NVARCHAR(128) 
	DECLARE @TableID INT

	DECLARE @IndexName NVARCHAR(128)

	DECLARE @Task NVARCHAR(4000)

	DECLARE @err INT
    DECLARE @timestampRows INT

	OPEN Tablecursor
	FETCH Next FROM Tablecursor INTO @TableID, @TableName 
	WHILE (@@FETCH_STATUS = 0) BEGIN
		SELECT @timestampRows=COUNT(*) FROM Syscolumns WHERE Id=@TableID AND (name='ModifiedDateTime') AND (xtype=6 OR xtype=61)

        IF @timestampRows = 1 BEGIN
			IF @Quiet = 0 PRINT 'Creating index for table ' + @TableName + '...'

			SELECT @IndexName = 'IX_' + @TableName + '_ModifiedDateTime'
			IF  EXISTS (SELECT * FROM dbo.sysindexes WHERE id = OBJECT_ID(@TableName) AND NAME = @IndexName) BEGIN
				SELECT @TASK = 'DROP INDEX ' + @TableName + '.' +  @IndexName + '' 
				IF @Verbose = 1 BEGIN
					PRINT @TASK
					PRINT 'GO'
				END
				IF @DryRun = 0 EXECUTE (@TASK)			
			END

			SELECT @Task = 'CREATE INDEX ' + @IndexName + ' ON ' + @TableName + '(ModifiedDateTime DESC)'
			IF @Verbose = 1 BEGIN
				PRINT ( @Task )
				PRINT 'GO'
            END
			IF @DryRun = 0 EXECUTE ( @Task )
		END
		ELSE BEGIN
			IF @Quiet = 0 PRINT 'Skipping table : ' + @TableName + ' due to missing timestamp column'
		END
 		FETCH Next FROM Tablecursor INTO @TableID,@TableName 
	END 
	CLOSE Tablecursor 
	DEALLOCATE Tablecursor
RETURN
GO

-- =============================================
-- Author:		Ryan C. Price
-- Create date: 12 January 2007
-- Description:	Calculate normal work time given start/stop times
-- =============================================
CREATE FUNCTION dbo.GetJobWorkTime 
(
	-- Add the parameters for the function here
	@StartDateTime datetime,
	@FinishDateTime datetime
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = DATEDIFF(minute, @StartDateTime, @FinishDateTime)

	-- Return the result of the function
	RETURN @Result

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ryan C. Price
-- Create date: 12 January 2007
-- Description:	Calculate premium work time given start/stop times
-- =============================================
CREATE FUNCTION dbo.GetJobPremiumWorkTime 
(
	-- Add the parameters for the function here
	@StartDateTime datetime,
	@FinishDateTime datetime
)
RETURNS int
AS
BEGIN
	DECLARE @Result int

	-- Add the T-SQL statements to compute the return value here
	-- DATEDIFF(minute, @StartDateTime, @FinishDateTime)
	SELECT @Result = 0 

	RETURN @Result
END
GO
-- EXECUTE spCreateTimeStampDefaults
-- GO
EXECUTE spCreateTimeStampTriggers
GO
EXECUTE spCreateTimeStampIndexes
GO

INSERT INTO JobStatus(JobStatusID, Description) 
	      SELECT 0, 'Draft'
	UNION SELECT 1, 'Completed' 
	UNION SELECT 2, 'Signed' 
	UNION SELECT 3, 'Completed and Synchronised' 
	UNION SELECT 4, 'Signed and Synchronised' 
	UNION SELECT 5, 'History'
	UNION SELECT 6, 'Request Status Change'
GO

INSERT INTO ClientSiteStatus (ClientSiteStatusID,Description,IsActive) SELECT 0, 'Active', 1 UNION SELECT 1, 'InActive', 0
GO

INSERT INTO EventType(EventTypeID,EventType,EventDescription) 
	      SELECT 1, 'INSERT', 'Request ID:{0} Inserted by {1} at {2} for {3}' 
	UNION SELECT 2, 'INSERT', 'Job ID: {0} Inserted by {1} at {2} for {3}' 
	UNION SELECT 3, 'UPDATE', 'Request ID:{0} Updated by {1} at {2} for {3}' 
	UNION SELECT 4, 'UPDATE', 'Job ID:{0} Updated by {1} at {2} for {3}' 
	UNION SELECT 5, 'UPDATE', '(Management Console) Job ID:{0} Updated by {1} at {2} for {3}' 
	UNION SELECT 6, 'UPDATE', '(Management Console) Request ID:{0} Updated by {1} at {2} for {3}'
GO

INSERT INTO CustomControlType (CustomControlTypeID,Description) 
	SELECT 0, 'Undefined' UNION	SELECT 1, 'Text Box' UNION SELECT 2, 'Check Box' UNION SELECT 3, 'Date' UNION SELECT 4, 'Time' UNION SELECT 5, 'Date & Time' UNION SELECT 6, 'Number' UNION SELECT 7, 'Drop-down List'
GO

INSERT INTO CustomTerminology (Entity,EntityPlural,Name,PluralName)
	SELECT 'Job', 'Jobs', 'Job', 'Jobs' UNION
	SELECT 'Request', 'Requests', 'Request', 'Requests' UNION
	SELECT 'Consultant', 'Consultants', 'Consultant', 'Consultants' UNION
	SELECT 'ClientSite', 'ClientSites', 'ClientSite', 'ClientSites'
GO

DECLARE @DomainLogon nvarchar(256)
SELECT @DomainLogon = rtrim(nt_domain) + '\' + rtrim(nt_username) FROM master..sysprocesses where spid = @@spid
INSERT INTO Consultant (ConsultantUID, ConsultantID, ConsultantNumber, Name, IsActiviserUser, Administration, Management, DomainLogon, Employee, Engineer, MobileAlert) VALUES ('00000000-0000-0000-0000-000000000000', 0, '0', 'activiser™ web service', 1, 1, 1, @DomainLogon,0,0,0)

GO

