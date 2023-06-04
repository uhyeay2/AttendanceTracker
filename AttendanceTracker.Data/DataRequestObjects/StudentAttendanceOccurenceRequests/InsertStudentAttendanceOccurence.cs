namespace AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests
{
    public class InsertStudentAttendanceOccurence : IDataRequest
    {
        public InsertStudentAttendanceOccurence(string studentCode, Guid courseScheduledGuid, Guid guid, DateTime dateOfOccurence, string notes, bool isExcused)
        {
            StudentCode = studentCode;
            CourseScheduledGuid = courseScheduledGuid;
            Guid = guid;
            DateOfOccurence = dateOfOccurence;
            Notes = notes;
            IsExcused = isExcused;
        }

        public string StudentCode { get; set; }

        public Guid CourseScheduledGuid { get; set; }

        public Guid Guid { get; set; }

        public DateTime DateOfOccurence { get; set; }

        public string Notes { get; set; }

        public bool IsExcused { get; set; }


        public object? GetParameters() => this;

        public string GetSql() => 
        $@"
            DECLARE @StudentCourseScheduledId INT = (
                {Select.JoinFromTable(TableNames.StudentCourseScheduled, 
                    $@" 
                        JOIN {TableNames.Student} ON {TableNames.Student}.Id = {TableNames.StudentCourseScheduled}.StudentId 
                        JOIN {TableNames.CourseScheduled} ON {TableNames.CourseScheduled}.Id = {TableNames.StudentCourseScheduled}.CourseScheduledId
                    ", 
                    columns: $"{TableNames.StudentCourseScheduled}.Id",
                    where: "StudentCode = @StudentCode AND Guid = @CourseScheduledGuid")} )            

            IF (@StudentCourseScheduledId IS NOT NULL)
            BEGIN

            {Insert.IntoTable(TableNames.AttendanceOccurence, "Guid", "DateOfOccurence", "Notes", "IsExcused")}

            DECLARE @AttendanceOccurenceId INT = ( SELECT SCOPE_IDENTITY() )

            {Insert.IntoTable(TableNames.StudentAttendanceOccurence, "StudentCourseScheduledId", "AttendanceOccurenceId")}

            END
        ";
    }
}
