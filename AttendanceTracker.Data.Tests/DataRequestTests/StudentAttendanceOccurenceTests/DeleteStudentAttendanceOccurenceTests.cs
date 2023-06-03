using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentAttendanceOccurenceTests
{
    public class DeleteStudentAttendanceOccurenceTests : DataTest
    {
        [Fact]
        public async Task DeleteStudentAttendanceOccurence_Given_OccurenceDoesNotExist_ShouldReturn_NoRowsUpdated()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudentAttendanceOccurence(Guid.NewGuid()));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task DeleteStudentAttendanceOccurence_Given_OccurenceIsDeleted_ShouldReturn_RowsUpdated()
        {
            var existingStudentAttendanceOccurence = await SeedAsync(new SeedStudentAttendanceOccurenceRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudentAttendanceOccurence(existingStudentAttendanceOccurence.Guid));

            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }
    }
}
