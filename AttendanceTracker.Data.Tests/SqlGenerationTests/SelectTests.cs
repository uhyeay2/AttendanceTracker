using AttendanceTracker.Data.SqlGeneration;

namespace AttendanceTracker.Data.Tests.SqlGenerationTests
{
    public class SelectTests
    {
        [Theory]
        [InlineData("Course", null, "SELECT * FROM [dbo].[Course] WITH(NOLOCK)")]
        [InlineData("Student", "FirstName", "SELECT FirstName FROM [dbo].[Student] WITH(NOLOCK)")]
        [InlineData("Student", "FirstName, LastName", "SELECT FirstName, LastName FROM [dbo].[Student] WITH(NOLOCK)")]
        [InlineData("Student", "Student.FirstName as StudentFirstName", "SELECT Student.FirstName as StudentFirstName FROM [dbo].[Student] WITH(NOLOCK)")]
        public void SelectFromTable_Given_ColumnsIsNullOrHasValue_Should_GenerateExpectedQuery(string table, string columns, string expected)
        {
            Assert.Equal(expected, Select.FromTable(table, columns));
        }

        [Theory]
        [InlineData("Instructor", true, "SELECT * FROM [dbo].[Instructor] WITH(NOLOCK)")]
        [InlineData("Student", false, "SELECT * FROM [dbo].[Student]")]
        public void SelectFromTable_Given_WithNoLockHasValue_Should_GenerateExpectedQuery(string table, bool withNoLock, string expected)
        {
            Assert.Equal(expected, Select.FromTable(table, withNoLock: withNoLock));
        }

        [Theory]
        [InlineData("Student", null, "SELECT * FROM [dbo].[Student] WITH(NOLOCK)")]
        [InlineData("Student", "StudentCode = @StudentCode", "SELECT * FROM [dbo].[Student] WITH(NOLOCK) WHERE StudentCode = @StudentCode")]
        [InlineData("Student", "FirstName = @FirstName AND LastName = @LastName", "SELECT * FROM [dbo].[Student] WITH(NOLOCK) WHERE FirstName = @FirstName AND LastName = @LastName")]
        public void SelectFromTable_Given_WhereIsNullOrHasValue_Should_GenerateExpectedQuery(string table, string where, string expected)
        {
            Assert.Equal(expected, Select.FromTable(table, where: where));
        }

        [Theory]
        [InlineData("Student", null, true, null, "SELECT * FROM [dbo].[Student] WITH(NOLOCK)")]
        [InlineData("Student", "Student.FirstName", true, null, "SELECT Student.FirstName FROM [dbo].[Student] WITH(NOLOCK)")]
        [InlineData("Instructor", "Instructor.FirstName", false, null, "SELECT Instructor.FirstName FROM [dbo].[Instructor]")]
        [InlineData("Student", "Student.FirstName", false, "FirstName = @FirstName", "SELECT Student.FirstName FROM [dbo].[Student] WHERE FirstName = @FirstName")]
        [InlineData("Student", "", true, "FirstName = @FirstName", "SELECT * FROM [dbo].[Student] WITH(NOLOCK) WHERE FirstName = @FirstName")]
        public void SelectFromTable_Given_AllParameters_Should_GenerateExpectedQuery(string table, string columns, bool withNoLock, string where, string expected)
        {
            Assert.Equal(expected, Select.FromTable(table, columns, withNoLock, where));
        }

        [Theory]
        [InlineData("Student", "StudentCode = @StudentCode", "SELECT CASE WHEN EXISTS ( SELECT * FROM [dbo].[Student] WITH(NOLOCK) WHERE StudentCode = @StudentCode ) THEN 1 ELSE 0 END")]
        [InlineData("Instructor", "FirstName = @FirstName", "SELECT CASE WHEN EXISTS ( SELECT * FROM [dbo].[Instructor] WITH(NOLOCK) WHERE FirstName = @FirstName ) THEN 1 ELSE 0 END")]
        public void SelectExists_Given_WhereCondition_Should_GenerateExpectedQuery(string table, string where, string expected)
        {
            Assert.Equal(expected, Select.Exists(table, where));
        }

        [Theory]
        [InlineData("Course", "LEFT JOIN Subject ON Subject.Id = Course.SubjectId", "Subject.Name, Subject.Id", true, "CourseCode = @CourseCode",
                    "SELECT Subject.Name, Subject.Id FROM [dbo].[Course] WITH(NOLOCK) LEFT JOIN Subject ON Subject.Id = Course.SubjectId WHERE CourseCode = @CourseCode")]
        [InlineData("Course", "LEFT JOIN Subject ON Subject.Id = Course.SubjectId", "Subject.Name, Subject.Id", false, "CourseCode = @CourseCode",
                    "SELECT Subject.Name, Subject.Id FROM [dbo].[Course] LEFT JOIN Subject ON Subject.Id = Course.SubjectId WHERE CourseCode = @CourseCode")]
        [InlineData("Course", "LEFT JOIN Subject ON Subject.Id = Course.SubjectId", "Subject.Name, Subject.Id", false, null,
                    "SELECT Subject.Name, Subject.Id FROM [dbo].[Course] LEFT JOIN Subject ON Subject.Id = Course.SubjectId")]
        [InlineData("Course", "LEFT JOIN Subject ON Subject.Id = Course.SubjectId", null, false, null,
                    "SELECT * FROM [dbo].[Course] LEFT JOIN Subject ON Subject.Id = Course.SubjectId")]
        public void JoinFromTable_GivenAllValues_ShouldReturn_ExpectedQuery(string table, string join, string columns, bool withNoLock, string? where, string expected)
        {
            Assert.Equal(expected, Select.JoinFromTable(table, join, columns, withNoLock, where));
        }

        [Theory]
        [InlineData("Student", 1, 10, "Id", "Student.Name, Student.StudentCode", true, "Name LIKE '%son%'",
                    "SELECT Student.Name, Student.StudentCode FROM [dbo].[Student] WITH(NOLOCK) WHERE Name LIKE '%son%' ORDER BY Id  OFFSET 0 * 10 ROWS FETCH NEXT 10 ROWS ONLY")]
        [InlineData("Student", 2, 5, "Name", null, false, null,
                    "SELECT * FROM [dbo].[Student] ORDER BY Name  OFFSET 1 * 5 ROWS FETCH NEXT 5 ROWS ONLY")]
        [InlineData("Student", -1, -1, "Name", null, false, null,
                    "SELECT * FROM [dbo].[Student] ORDER BY Name  OFFSET 0 * 10 ROWS FETCH NEXT 10 ROWS ONLY")]
        public void PaginatedFromTable_GivenAllValues_ShouldReturn_ExpectedQuery(string table, int pageNumber, int recordsPerPage, string orderBy, string columns, bool withNoLock, string where, string expected)
        {
            Assert.Equal(expected, Select.PaginatedFromTable(table, pageNumber, recordsPerPage, orderBy, columns, withNoLock, where));
        }
    }
}
