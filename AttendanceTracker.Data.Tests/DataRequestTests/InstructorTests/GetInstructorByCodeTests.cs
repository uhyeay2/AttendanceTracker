using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.InstructorTests
{
    public class GetInstructorByCodeTests : DataTest
    {
        [Fact]
        public async Task GetInstructorByCode_Given_InstructorDoesNotExist_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetInstructorByCode(RandomString())));
        }

        [Fact]
        public async Task GetInstructorByCode_Given_InstructorExists_ShouldReturn_Instructor()
        {
            var expected = await GetSeededInstructorAsync();

            var actual = await _dataAccess.FetchAsync(new GetInstructorByCode(expected.InstructorCode));

            Assert.Multiple(() =>
            {
                Assert.NotNull(actual);

                Assert.Equal(expected.InstructorCode, actual.InstructorCode);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
            });
        }
    }
}
