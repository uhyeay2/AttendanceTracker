using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.SubjectTests
{
    public class IsSubjectCodeExistingTests : DataTest
    {
        [Fact]
        public async Task IsSubjectCodeExisting_Given_CodeNotExisting_ShouldReturn_False()
        {
            Assert.False(await _dataAccess.FetchAsync(new IsSubjectCodeExisting(RandomString())));
        }

        [Fact]
        public async Task IsSubjectCodeExisting_Given_CodeIsExisting_ShouldReturn_True()
        {
            var subject = await GetSeededSubjectAsync();

            Assert.True(await _dataAccess.FetchAsync(new IsSubjectCodeExisting(subject.SubjectCode)));
        }
    }
}
