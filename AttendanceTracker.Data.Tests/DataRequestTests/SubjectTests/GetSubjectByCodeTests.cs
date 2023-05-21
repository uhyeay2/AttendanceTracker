using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.SubjectTests
{
    public class GetSubjectByCodeTests :DataTest
    {
        [Fact]
        public async Task GetSubjectByCode_Given_SubjectNotExisting_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetSubjectByCode(RandomString()));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetSubjectByCode_Given_SubjectIsExisting_Should_ReturnSubject()
        {
            var expected = await _dataSeeder.NewSubject();

            var result = await _dataAccess.FetchAsync(new GetSubjectByCode(expected.SubjectCode));

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.Id, result.Id);
                Assert.Equal(expected.Name, result.Name);
                Assert.Equal(expected.SubjectCode, result.SubjectCode);
            });
        }
    }
}
