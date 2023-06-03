using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class GetStudentByIdTests : DataTest
    {
        [Fact]
        public async Task GetStudentById_Given_StudentDoesNotExist_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetStudentById(int.MinValue)));
        }

        [Fact]
        public async Task GetStudentById_Given_StudentExists_ShouldReturn_Student()
        {
            var expected = await SeedAsync(new SeedStudentRequest());

            var actual = await _dataAccess.FetchAsync(new GetStudentById(expected.Id));

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
