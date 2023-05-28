DROP PROCEDURE IF EXISTS [dbo].[SeedTable_StudentCourseScheduled] 
GO

CREATE PROCEDURE [dbo].[SeedTable_StudentCourseScheduled] AS
BEGIN

	PRINT 'Populating StudentCourseScheduled records in [dbo].[StudentCourseScheduled]';

	IF OBJECT_ID('tempdb.dbo.#dbo_StudentCourseScheduled') IS NOT NULL DROP TABLE #dbo_StudentCourseScheduled;

	SELECT 
		[Id], [StudentId], [CourseScheduledId] 
	INTO #dbo_StudentCourseScheduled 
	FROM [dbo].[StudentCourseScheduled] 
	WHERE 0 = 1;

	SET IDENTITY_INSERT #dbo_StudentCourseScheduled ON;

	INSERT INTO #dbo_StudentCourseScheduled 
		( [Id], [StudentId], [CourseScheduledId] )
	SELECT 
		  [Id], [StudentId], [CourseScheduledId] 
	FROM 
	(  VALUES
		  ( 1,  1, 1)
		, ( 2,  2, 1)
		, ( 3,  3, 1)
		, ( 4,  4, 1)
		, ( 5,  5, 1)
		, ( 6,  6, 1)
		, ( 7,  7, 1)
		, ( 8,  8, 1)
		, ( 9,  9, 1)
		, (10, 10, 1)
		, (11,  1, 2)
		, (12,  2, 2)
		, (13,  3, 2)
		, (14,  4, 2)
		, (15,  5, 2)
		, (16,  6, 2)
		, (17,  7, 2)
		, (18,  8, 2)
		, (19,  9, 2)
		, (20, 10, 2)
		, (21,  1, 3)
		, (22,  2, 3)
		, (23,  3, 3)
		, (24,  4, 3)
		, (25,  5, 3)
		, (26,  6, 3)
		, (27,  7, 3)
		, (28,  8, 3)
		, (29,  9, 3)
		, (30, 10, 3)
		
	) AS v ( [Id], [StudentId], [CourseScheduledId] );

	SET IDENTITY_INSERT #dbo_StudentCourseScheduled OFF;
		
	SET IDENTITY_INSERT [dbo].[StudentCourseScheduled] ON;

	WITH cte_data as 
		(SELECT 
			[Id], [StudentId], [CourseScheduledId] 
		FROM #dbo_StudentCourseScheduled)
	MERGE [dbo].[StudentCourseScheduled] as t
		USING cte_data as s ON t.[Id] = s.[Id]
		WHEN NOT MATCHED BY TARGET THEN
			INSERT 
				( [Id], [StudentId], [CourseScheduledId] )
			VALUES 
				( s.[Id], s.[StudentId], s.[CourseScheduledId] )
		WHEN MATCHED 
			THEN UPDATE SET 
				[StudentId] = s.[StudentId], 
				[CourseScheduledId] = s.[CourseScheduledId]
	;

	SET IDENTITY_INSERT [dbo].StudentCourseScheduled OFF;

	DROP TABLE #dbo_StudentCourseScheduled;

	PRINT 'Finished Populating StudentCourseScheduleds in dbo.StudentCourseScheduled'
END
GO

EXEC [dbo].[SeedTable_StudentCourseScheduled];

DROP PROCEDURE IF EXISTS [dbo].[SeedTable_StudentCourseScheduled] 
GO
