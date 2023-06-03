CREATE TABLE [dbo].[StudentAttendanceOccurence]
(
	[Id] INT NOT NULL PRIMARY KEY CLUSTERED IDENTITY(1, 1),
	[StudentCourseScheduledId] INT NOT NULL REFERENCES StudentCourseScheduled(Id),
	[AttendanceOccurenceId] INT NOT NULL REFERENCES AttendanceOccurence(Id)
)
