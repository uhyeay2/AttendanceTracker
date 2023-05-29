namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.SubjectControllerTests
{
    public class GetSubjectByCodeTests : BaseSubjectControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task GetSubjectByCode_Given_CodeNotProvided_ShouldThrow_ValidationFailedException(string subjectCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetSubjectBySubjectCode(subjectCode));
        }

        [Fact]
        public async Task GetSubjectByCode_Given_SubjectCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.GetSubjectBySubjectCode("DDD"));
        }

        [Fact]
        public async Task GetSubjectByCode_Given_SubjectCodeExisting_ShouldReturn_Subject()
        {
            var existingSubject = await SeedAsync(new SeedSubjectRequest());

            var result = await _controller.GetSubjectBySubjectCode(existingSubject.SubjectCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(existingSubject.Name, result.Name);
                Assert.Equal(existingSubject.SubjectCode, result.SubjectCode);
            });
        }
    }
}
