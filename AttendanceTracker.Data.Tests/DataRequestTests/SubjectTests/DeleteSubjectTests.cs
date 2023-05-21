using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.SubjectTests
{
    public class DeleteSubjectTests : DataTest
    {
        [Fact]
        public async Task DeleteSubject_Given_SubjectDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteSubject(RandomString()));

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task DeleteSubject_Given_SubjectIsDeleted_ShouldReturn_OneRowAffected()
        {
            var subject = await GetSeededSubjectAsync();

            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteSubject(subject.SubjectCode));

            Assert.Equal(1, rowsAffected);
        }
    }
}
