CREATE TABLE [dbo].[StudentAttendanceOccurence]
(
	[Id] INT NOT NULL PRIMARY KEY CLUSTERED IDENTITY(1, 1),
	[StudentId] INT NOT NULL REFERENCES Student(Id),
	[AttendenceOccurenceId] INT NOT NULL REFERENCES AttendanceOccurence(Id)
)
