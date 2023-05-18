using AttendanceTracker.Data.SqlGeneration;

namespace AttendanceTracker.Data.Tests.SqlGenerationTests
{
    public class InsertTests
    {
        [Fact]
        public void Insert_Given_ColumnsAndValues_Should_GenerateExpectedCommand()
        {
            var table = "Student";
            
            var columnsAndValues = new[]
            {
                ("FirstName", "@FirstName"),
                ("MiddleName", "@MiddleName"),
                ("LastName", "@LastName")
            };

            var expected = $"INSERT INTO {table} ( FirstName, MiddleName, LastName ) VALUES ( @FirstName, @MiddleName, @LastName )";

            var sql = Insert.IntoTable(table, columnsAndValues);

            Assert.Equal(expected, sql);
        }

        [Fact]
        public void Insert_Given_ColumnsWithoutValues_Should_GenerateExpectedCommand()
        {
            var table = "Instructor";

            var columns = new[] { "FirstName", "LastName", "Email" };

            var expected = $"INSERT INTO {table} ( FirstName, LastName, Email ) VALUES ( @FirstName, @LastName, @Email )";

            var sql = Insert.IntoTable(table, columns);

            Assert.Equal(expected, sql);
        }
    }
}
