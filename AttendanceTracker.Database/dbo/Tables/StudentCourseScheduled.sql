CREATE TABLE [dbo].[StudentCourseScheduled]
(
	[Id] INT NOT NULL PRIMARY KEY CLUSTERED IDENTITY(1, 1),
	[StudentId] INT NOT NULL REFERENCES Student(Id),
	[CourseScheduledId] INT NOT NULL REFERENCES CourseScheduled(Id),
)
