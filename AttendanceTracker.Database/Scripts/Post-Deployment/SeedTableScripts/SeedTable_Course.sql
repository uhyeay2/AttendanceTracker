DROP PROCEDURE IF EXISTS [dbo].[SeedTable_Course] 
GO

CREATE PROCEDURE [dbo].[SeedTable_Course] AS
BEGIN

	PRINT 'Populating Courses in [dbo].[Course]';

	IF OBJECT_ID('tempdb.dbo.#dbo_Course') IS NOT NULL DROP TABLE #dbo_Course;

	SELECT 
		[Id], [SubjectId], [CourseCode], [Name] 
	INTO #dbo_Course 
	FROM [dbo].[Course] 
	WHERE 0 = 1;

	SET IDENTITY_INSERT #dbo_Course ON;

	INSERT INTO #dbo_Course 
		( [Id], [SubjectId], [CourseCode], [Name] )
	SELECT 
		  [Id], [SubjectId], [CourseCode], [Name] 
	FROM 
	(  VALUES

		    (1, 1, N'MAT-095851', N'Algebra 1')
		  , (2, 1, N'MAT-428608', N'Algebra 2')
		  , (3, 1, N'MAT-998521', N'Geometry 1')
		  , (4, 1, N'MAT-816779', N'Geometry 2')
		  , (5, 2, N'ENG-758566', N'Intro To English')
		  , (6, 2, N'ENG-571804', N'Intermediate English')
		  , (7, 2, N'ENG-065291', N'Advanced English')
		  , (8, 5, N'SPA-143867', N'Intro To Spanish')
		  , (9, 5, N'SPA-354634', N'Intermediate Spanish')
		  , (10, 5, N'SPA-117716', N'Advanced Spanish')
		  , (11, 6, N'FRE-032350', N'Intro To French')
		  , (12, 6, N'FRE-805664', N'Intermediate French')
		  , (13, 6, N'FRE-524345', N'Advanced French')
		  , (14, 3, N'HIS-670542', N'US History 1')
		  , (15, 3, N'HIS-154258', N'US History 2')
		  , (16, 3, N'HIS-100727', N'World History 1')
		  , (17, 3, N'HIS-651366', N'World History 2')
		  , (18, 4, N'SCI-686855', N'Biology 1')
		  , (19, 4, N'SCI-129516', N'Biology 2')
		  , (20, 4, N'SCI-531743', N'Astronomy 1')
		  , (21, 4, N'SCI-477811', N'Astronomy 2')
		
	) AS v ( [Id], [SubjectId], [CourseCode], [Name] );

	SET IDENTITY_INSERT #dbo_Course OFF;
		
	SET IDENTITY_INSERT [dbo].[Course] ON;

	WITH cte_data as 
		(SELECT 
			[Id], [SubjectId], [CourseCode], [Name] 
		FROM #dbo_Course)
	MERGE [dbo].[Course] as t
		USING cte_data as s ON t.[Id] = s.[Id]
		WHEN NOT MATCHED BY TARGET THEN
			INSERT 
				([Id], [SubjectId], [CourseCode], [Name])
			VALUES 
				(s.[Id], s.[SubjectId], s.[CourseCode], s.[Name])
		WHEN MATCHED 
			THEN UPDATE SET 
				[SubjectId] = s.[SubjectId], 
				[CourseCode] = s.[CourseCode],
				[Name] = s.[Name]
	;

	SET IDENTITY_INSERT [dbo].Course OFF;

	DROP TABLE #dbo_Course;

	PRINT 'Finished Populating Courses in dbo.Course'
END
GO

EXEC [dbo].[SeedTable_Course];

DROP PROCEDURE IF EXISTS [dbo].[SeedTable_Course] 
GO
