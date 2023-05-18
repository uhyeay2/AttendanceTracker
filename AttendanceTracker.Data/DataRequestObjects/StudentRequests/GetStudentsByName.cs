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
            Select.FromTable(TableNames.Student, 
            where: @"
                (@FirstName IS NOT NULL OR @LastName IS NOT NULL) AND
                (@FirstName IS NULL OR FirstName LIKE '%' + @FirstName + '%' ) AND
                (@LastName  IS NULL OR LastName  LIKE '%' + @LastName  + '%' )
            ");
    }
}
