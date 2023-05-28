using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.InstructorTests
{
    public class DeleteInstructorTests : DataTest
    {
        [Fact]
        public async Task DeleteInstructor_Given_InstructorDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteInstructor(RandomString()));

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task DeleteInstructor_Given_InstructorIsDeleted_ShouldReturn_OneRowAffected()
        {
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteInstructor(existingInstructor.InstructorCode));

            Assert.Equal(1, rowsAffected);
        }
    }
}
