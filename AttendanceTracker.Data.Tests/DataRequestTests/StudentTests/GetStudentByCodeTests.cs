using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class GetStudentByCodeTests : DataTest
    {
        [Fact]
        public async Task GetStudentByCode_Given_StudentDoesNotExist_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetStudentByCode(RandomString())));
        }

        [Fact]
        public async Task GetStudentByCode_Given_StudentExists_ShouldReturn_Student()
        {
            var expected = await GetSeededStudentAsync();

            var actual = await _dataAccess.FetchAsync(new GetStudentByCode(expected.StudentCode));

            Assert.Multiple(() =>
            {
                Assert.NotNull(actual);

                Assert.Equal(expected.StudentCode, actual.StudentCode);
                Assert.Equal(expected.DateOfBirth, actual.DateOfBirth);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
            });
        }
    }
}
