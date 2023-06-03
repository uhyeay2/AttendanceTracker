using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentAttendanceOccurenceTests
{
    public class IsStudentAttendanceOccurenceExistingTests : DataTest
    {
        [Fact]
        public async Task IsStudentAttendanceOccurenceExisting_Given_StudentAttendanceOccurenceNotExisting_ShouldReturn_False()
        {
            var result = await _dataAccess.FetchAsync(new IsStudentAttendanceOccurenceExisting(Guid.NewGuid()));

            Assert.False(result);
        }

        [Fact]
        public async Task IsStudentAttendanceOccurenceExisting_Given_StudentAttendanceOccurenceIsExisting_ShouldReturn_True()
        {
            var existingStudentAttendanceOccurence = await SeedAsync(new SeedStudentAttendanceOccurenceRequest());

            var result = await _dataAccess.FetchAsync(new IsStudentAttendanceOccurenceExisting(existingStudentAttendanceOccurence.Guid));

            Assert.True(result);
        }
    }
}
