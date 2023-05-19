using AttendanceTracker.Data.SqlGeneration;

namespace AttendanceTracker.Data.Tests.SqlGenerationTests
{
    public class UpdateTests
    {
        [Fact]
        public void CoalesceUpdate_Given_ColumnsAndValues_Should_GenerateExpectedCommand()
        {
            var table = "Student";

            var columnsAndValues = new[] { ("FirstName", "@FirstName"), ("LastName", "@LastName") };

            var where = "StudentCode = @StudentCode";

            var expected = "UPDATE [dbo].[Student] SET FirstName = COALESCE(@FirstName, FirstName), LastName = COALESCE(@LastName, LastName) WHERE StudentCode = @StudentCode";

            var sql = Update.CoalesceTable(table, where, columnsAndValues);

            Assert.Equal(expected, sql);
        }

        [Fact]
        public void CoalesceUpdate_Given_OnlyColumnNames_Should_GenerateExpectedCommand()
        {
            var table = "Student";

            var columnsWithMatchingParameterNames = new[] { "FirstName", "LastName" };

            var where = "StudentCode = @StudentCode";

            var expected = "UPDATE [dbo].[Student] SET FirstName = COALESCE(@FirstName, FirstName), LastName = COALESCE(@LastName, LastName) WHERE StudentCode = @StudentCode";

            var sql = Update.CoalesceTable(table, where, columnsWithMatchingParameterNames);

            Assert.Equal(expected, sql);
        }
    }
}
