using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentAttendanceOccurenceTests
{
    public class GetStudentAttendanceOccurenceTests : DataTest
    {
        [Fact]
        public async Task GetStudentAttendanceOccurence_Given_OccurenceDoesNotExist_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetStudentAttendanceOccurence(Guid.NewGuid()));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetStudentAttendanceOccurence_Given_OccurenceExists_ShouldReturn_StudentAttendanceOccurence()
        {
            var existingOccurence = await SeedAsync(new SeedStudentAttendanceOccurenceRequest());

            var result = await _dataAccess.FetchAsync(new GetStudentAttendanceOccurence(existingOccurence.Guid));

            Assert.NotNull(result);
        }
    }
}
