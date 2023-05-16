namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class GetStudentsByName : IDataRequest<Student_DTO>
    {
        public GetStudentsByName(string? firstName = null, string? lastName = null)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public object? GetParameters() => new { FirstName, LastName };

        public string GetSql() =>
        @"
            IF (@FirstName IS NOT NULL OR @LastName IS NOT NULL)

                SELECT * FROM [dbo].[Student] WITH(NOLOCK)

                    WHERE (FirstName LIKE '%' + @FirstName + '%' OR @FirstName IS NULL)

                      AND (LastName LIKE '%' + @LastName + '%' OR @LastName IS NULL)
        ";
    }
}
