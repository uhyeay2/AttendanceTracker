using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class IsStudentCodeTakenTests : DataTest
    {
        [Fact]
        public async Task IsStudentCodeTaken_Given_StudentCodeNotTaken_ShouldReturn_False()
        {
            Assert.False(await _dataAccess.FetchAsync(new IsStudentCodeTaken(RandomString())));
        }

        [Fact]
        public async Task IsStudentCodeTaken_Given_StudentCodeIsTaken_ShouldReturn_True()
        {
            var student = await _dataSeeder.NewStudent();

            Assert.True(await _dataAccess.FetchAsync(new IsStudentCodeTaken(student.StudentCode)));
        }
    }
}
