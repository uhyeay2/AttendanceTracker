 namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.SubjectControllerTests
{
    public class IsSubjectCodeExistingTests : BaseSubjectControllerTest
    {
        [Fact]
        public async Task IsSubjectCodeExisting_Given_SubjectCodeNotExisting_ShouldReturn_False()
        {
            var result = await _controller.IsSubjectCodeExisting(RandomString());

            Assert.False(result);
        }

        [Fact]
        public async Task IsSubjectCodeExisting_Given_SubjectCodeExisting_ShouldReturn_True()
        {
            var existingSubject = await SeedAsync(new SeedSubjectRequest());

            var result = await _controller.IsSubjectCodeExisting(existingSubject.SubjectCode);

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task IsSubjectCodeExisting_Given_InvalidRequest_ShouldThrow_ValidationFailedException(string subjectCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.IsSubjectCodeExisting(subjectCode));
        }
    }
}
