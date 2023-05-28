using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class DeleteStudentTests : DataTest
    {
        [Fact]
        public async Task DeleteStudent_Given_StudentDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudent(RandomString()));

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task DeleteStudent_Given_StudentIsDeleted_ShouldReturn_OneRowAffected()
        {
            var student = await SeedAsync(new SeedStudentRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudent(student.StudentCode));

            Assert.Equal(1, rowsAffected);
        }
    }
}
