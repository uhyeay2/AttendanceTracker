using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class IsStudentCodeExistingTests : DataTest
    {
        [Fact]
        public async Task IsStudentCodeExisting_Given_StudentCodeNotTaken_ShouldReturn_False()
        {
            Assert.False(await _dataAccess.FetchAsync(new IsStudentCodeExisting(RandomString())));
        }

        [Fact]
        public async Task IsStudentCodeExisting_Given_StudentCodeIsTaken_ShouldReturn_True()
        {
            var student = await _dataSeeder.NewStudent();

            Assert.True(await _dataAccess.FetchAsync(new IsStudentCodeExisting(student.StudentCode)));
        }
    }
}
