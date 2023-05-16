namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class InsertStudent : IDataRequest
    {
        public InsertStudent(Student student) => Student = student;

        public Student Student { get; set; }

        public object? GetParameters() => new { Student.StudentCode, Student.FirstName, Student.LastName, Student.DateOfBirth };

        public string GetSql() => 
            "INSERT INTO [dbo].[Student] (StudentCode, FirstName, LastName, DateOfBirth) VALUES ( @StudentCode, @FirstName, @LastName, @DateOfBirth )";
    }
}
