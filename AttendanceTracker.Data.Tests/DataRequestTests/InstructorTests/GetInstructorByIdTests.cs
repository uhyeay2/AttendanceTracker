using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.InstructorTests
{
    public class GetInstructorByIdTests : DataTest
    {
        [Fact]
        public async Task GetInstructorById_Given_InstructorDoesNotExist_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetInstructorById(int.MaxValue)));
        }

        [Fact]
        public async Task GetInstructorById_Given_InstructorExists_ShouldReturn_Instructor()
        {
            var expected = await SeedAsync(new SeedInstructorRequest());

            var actual = await _dataAccess.FetchAsync(new GetInstructorById(expected.Id));

            Assert.Multiple(() =>
            {
                Assert.NotNull(actual);

                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
            });
        }
    }
}
