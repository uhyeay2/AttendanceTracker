using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.InstructorTests
{
    public class IsInstructorCodeExistingTests : DataTest
    {
        [Fact]
        public async Task IsInstructorCodeExisting_Given_InstructorCodeNotTaken_ShouldReturn_False()
        {
            Assert.False(await _dataAccess.FetchAsync(new IsInstructorCodeExisting(RandomString())));
        }

        [Fact]
        public async Task IsInstructorCodeExisting_Given_InstructorCodeIsTaken_ShouldReturn_True()
        {
            var existingInstructor = await GetSeededInstructorAsync();

            Assert.True(await _dataAccess.FetchAsync(new IsInstructorCodeExisting(existingInstructor.InstructorCode)));
        }
    }
}
