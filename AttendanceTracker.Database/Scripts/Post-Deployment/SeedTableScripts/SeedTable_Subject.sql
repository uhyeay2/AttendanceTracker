DROP PROCEDURE IF EXISTS [dbo].[SeedTable_Subject] 
GO

CREATE PROCEDURE [dbo].[SeedTable_Subject] AS
BEGIN

	PRINT 'Populating Subjects in [dbo].[Subject]';

	IF OBJECT_ID('tempdb.dbo.#dbo_Subject') IS NOT NULL DROP TABLE #dbo_Subject;

	SELECT 
		[Id], [SubjectCode], [Name] 
	INTO #dbo_Subject 
	FROM [dbo].[Subject] 
	WHERE 0 = 1;

	SET IDENTITY_INSERT #dbo_Subject ON;

	INSERT INTO #dbo_Subject 
		( [Id], [SubjectCode], [Name] )
	SELECT 
		  [Id], [SubjectCode], [Name] 
	FROM 
	(  VALUES

		  (1, N'MAT', N'Math')
		, (2, N'ENG', N'English')
		, (3, N'HIS', N'History')
		, (4, N'SCI', N'Science')
		, (5, N'SPA', N'Spanish')
		, (6, N'FRE', N'French')
		
	) AS v ( [Id], [SubjectCode], [Name] );

	SET IDENTITY_INSERT #dbo_Subject OFF;

	SET IDENTITY_INSERT [dbo].[Subject] ON;

	WITH cte_data as 
		(SELECT 
			[Id], [SubjectCode], [Name] 
		FROM #dbo_Subject)
	MERGE [dbo].[Subject] as t
		USING cte_data as s ON t.[Id] = s.[Id]
		WHEN NOT MATCHED BY TARGET THEN
			INSERT 
				([Id], [SubjectCode], [Name])
			VALUES 
				(s.[Id], s.[SubjectCode], s.[Name])
		WHEN MATCHED 
			THEN UPDATE SET 
				[SubjectCode] = s.[SubjectCode], 
				[Name] = s.[Name]
	;

	SET IDENTITY_INSERT [dbo].Subject OFF;

	DROP TABLE #dbo_Subject;

	PRINT 'Finished Populating Subjects in dbo.Subject'
END
GO

EXEC [dbo].[SeedTable_Subject];

DROP PROCEDURE IF EXISTS [dbo].[SeedTable_Subject] 
GO
