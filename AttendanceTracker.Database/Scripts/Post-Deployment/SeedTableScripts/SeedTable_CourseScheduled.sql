DROP PROCEDURE IF EXISTS [dbo].[SeedTable_CourseScheduled] 
GO

CREATE PROCEDURE [dbo].[SeedTable_CourseScheduled] AS
BEGIN

	PRINT 'Populating CourseScheduled records in [dbo].[CourseScheduled]';

	IF OBJECT_ID('tempdb.dbo.#dbo_CourseScheduled') IS NOT NULL DROP TABLE #dbo_CourseScheduled;

	SELECT 
		[Id], [Guid], [CourseId], [InstructorId], [StartDate], [EndDate] 
	INTO #dbo_CourseScheduled 
	FROM [dbo].[CourseScheduled] 
	WHERE 0 = 1;

	SET IDENTITY_INSERT #dbo_CourseScheduled ON;

	INSERT INTO #dbo_CourseScheduled 
		( [Id], [Guid], [CourseId], [InstructorId], [StartDate], [EndDate] )
	SELECT 
		  [Id], [Guid], [CourseId], [InstructorId], [StartDate], [EndDate] 
	FROM 
	(  VALUES
		  (1, N'a07aaecf-2b3c-4aca-81ca-2f0019b89ec0',  1, 1, CAST(N'2023-05-28T00:00:00.000' AS DateTime), CAST(N'2023-05-28T00:00:00.000' AS DateTime))
		, (2, N'777d47c3-b4a6-4151-b9de-4c5b4ebe9b5c',  5, 2, CAST(N'2023-05-28T00:00:00.000' AS DateTime), CAST(N'2023-05-28T00:00:00.000' AS DateTime))
		, (3, N'e76faf09-fd93-4dff-9766-ec2be7dacb7f', 18, 3, CAST(N'2023-05-28T00:00:00.000' AS DateTime), CAST(N'2023-05-28T00:00:00.000' AS DateTime))
		
	) AS v ( [Id], [Guid], [CourseId], [InstructorId], [StartDate], [EndDate] );

	SET IDENTITY_INSERT #dbo_CourseScheduled OFF;
		
	SET IDENTITY_INSERT [dbo].[CourseScheduled] ON;

	WITH cte_data as 
		(SELECT 
			[Id], [Guid], [CourseId], [InstructorId], [StartDate], [EndDate] 
		FROM #dbo_CourseScheduled)
	MERGE [dbo].[CourseScheduled] as t
		USING cte_data as s ON t.[Id] = s.[Id]
		WHEN NOT MATCHED BY TARGET THEN
			INSERT 
				( [Id], [Guid], [CourseId], [InstructorId], [StartDate], [EndDate] )
			VALUES 
				( s.[Id], s.[Guid], s.[CourseId], s.[InstructorId], s.[StartDate] , s.[EndDate] )
		WHEN MATCHED 
			THEN UPDATE SET 
				[Guid] = s.[Guid], 
				[CourseId] = s.[CourseId],
				[InstructorId] = s.[InstructorId],
				[StartDate] = s.[StartDate],
				[EndDate] = s.[EndDate]
	;

	SET IDENTITY_INSERT [dbo].CourseScheduled OFF;

	DROP TABLE #dbo_CourseScheduled;

	PRINT 'Finished Populating CourseScheduleds in dbo.CourseScheduled'
END
GO

EXEC [dbo].[SeedTable_CourseScheduled];

DROP PROCEDURE IF EXISTS [dbo].[SeedTable_CourseScheduled] 
GO