using AttendanceTracker.Data.SqlGeneration;

namespace AttendanceTracker.Data.Tests.SqlGenerationTests
{
    public class DeleteTests
    {
        [Theory]
        [InlineData("Student", "1 = 1", "DELETE FROM [dbo].[Student] WHERE 1 = 1")]
        [InlineData("Instructor", "FirstName = @FirstName", "DELETE FROM [dbo].[Instructor] WHERE FirstName = @FirstName")]
        public void DeleteFromTable_Given_TableAndWhereCondition_Should_ReturnExpectedCommand(string table, string whereCondition, string expected)
        {
            var sql = Delete.FromTable(table, whereCondition);

            Assert.Equal(expected, sql);
        }
    }
}
