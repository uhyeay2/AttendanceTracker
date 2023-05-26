DROP PROCEDURE IF EXISTS [dbo].[SeedTable_Instructor] 
GO

CREATE PROCEDURE [dbo].[SeedTable_Instructor] AS
BEGIN

	PRINT 'Populating Instructors in [dbo].[Instructor]';

	IF OBJECT_ID('tempdb.dbo.#dbo_Instructor') IS NOT NULL DROP TABLE #dbo_Instructor;

	SELECT 
		[Id], [InstructorCode], [FirstName], [LastName] 
	INTO #dbo_Instructor 
	FROM [dbo].[Instructor] 
	WHERE 0 = 1;

	SET IDENTITY_INSERT #dbo_Instructor ON;

	INSERT INTO #dbo_Instructor 
		( [Id], [InstructorCode], [FirstName], [LastName] )
	SELECT 
		  [Id], [InstructorCode], [FirstName], [LastName] 
	FROM 
	(  VALUES

		  (1, N'HIL2476', N'Juan', N'Hill')
		, (2, N'MAR1253', N'Bailey', N'Martin')
		, (3, N'IGI6085', N'Jennifer', N'Iginla')
		, (4, N'KIN5451', N'Victor', N'King')
		, (5, N'LOP3133', N'Lindsey', N'Lopez')
		
	) AS v ( [Id], [InstructorCode], [FirstName], [LastName] );

	SET IDENTITY_INSERT #dbo_Instructor OFF;

	SET IDENTITY_INSERT [dbo].[Instructor] ON;

	WITH cte_data as 
		(SELECT 
			[Id], [InstructorCode], [FirstName], [LastName] 
		FROM #dbo_Instructor)
	MERGE [dbo].[Instructor] as t
		USING cte_data as s ON t.[Id] = s.[Id]
		WHEN NOT MATCHED BY TARGET THEN
			INSERT 
				([Id], [InstructorCode], [FirstName], [LastName])
			VALUES 
				(s.[Id], s.[InstructorCode], s.[FirstName], s.[LastName])
		WHEN MATCHED 
			THEN UPDATE SET 
				[InstructorCode] = s.[InstructorCode], 
				[FirstName] = s.[FirstName], 
				[LastName] = s.[LastName]
	;

	SET IDENTITY_INSERT [dbo].Instructor OFF;

	DROP TABLE #dbo_Instructor;

	PRINT 'Finished Populating Instructors in dbo.Instructor'
END
GO

EXEC [dbo].[SeedTable_Instructor];

DROP PROCEDURE IF EXISTS [dbo].[SeedTable_Instructor] 
GO
