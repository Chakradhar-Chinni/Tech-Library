---SSMS---
	1. Get all column names of a table
	select COLUMN_NAME from information_schema.columns where table_name = 'JiraDataDetails'
	2. Create table with auto-increment
	CREATE TABLE table23( id INT IDENTITY(1,1) PRIMARY KEY,colC varchar(255),colZ varchar(255))

	
---Queries---
	1. Print distinct values in column 
	SELECT Distinct Bot_Process_Status FROM CBRS_Unprocessed
	2. Delete full column data
	UPDATE table_name SET column_name = NULL;
	3. Add new column
		ALTER TABLE table_name ADD column_name datatype;
	4. Delete existing column
		ALTER TABLE Customers
		DROP COLUMN ContactName;
	5. Take a backup of existing table
		SELECT * into newTableName FROM existingTableName
	6. DELETE table with data
		DROP TABLE tableName
	7. Delete a Stored Procedure
	8. Copy data between two tables
		a. For copying 1 column
	INSERT INTO FIS_AddvantageDB_15032024.dbo.table23 (colC) 
	select Description 
	FROM GlobalPlusDB.dbo.JiraUnassignedTicketDetails
		b. For copying 2 columns
	INSERT INTO databaseB.dbo.tableB (colY, colZ)
	SELECT colA, colB
	FROM databaseA.dbo.tableA;
	9. Create new table 
	CREATE TABLE table24 (DigitEntry VARCHAR(max), TicketId VARCHAR(50))
	10. Read SQL Server logs
		EXEC xp_readerrorlog;
	11. UPDATE Column name
		Sql Server: EXEC sp_rename 'dbo.table_name.old_column_name', 'new_column_name', 'COLUMN';
		App: ALTER TABLE table_name RENAME COLUMN old_name to new_name;
	12. Add new columns 
		ALTER TABLE table_name
		ADD column_name1 data_type1,
		ADD column_name2 data_type2;
	13. Reset auto increment columns after deleting full data in table, Here 0 means auto-inc starts from 1
		DBCC CHECKIDENT ('TABLE_NAME', RESEED, 0);
--Notes--
	1. SET UPDLOCK while doing any SQL operation so other transactions won't happen at same time
		UPDATE Table23 WITH (UPDLOCK)
	2. get list of stored procedures in database (check recently modified PROC)
		SELECT 
		    name AS ProcedureName,
		    SCHEMA_NAME(schema_id) AS SchemaName,
		    create_date AS CreationDate,
		    modify_date AS LastModifiedDate
		FROM sys.procedures ORDER BY LastModifiedDate DESC
