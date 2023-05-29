namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.SubjectControllerTests
{
    public class DeleteSubjectTests : BaseSubjectControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task DeleteSubject_Given_InvalidRequest_ShouldThrow_ValidationFailedException(string subjectCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.DeleteSubject(subjectCode));
        }

        [Fact]
        public async Task DeleteSubject_Given_SubjectCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.DeleteSubject(RandomString()));
        }

        [Fact]
        public async Task DeleteSubject_Given_SubjectCodeIsExisting_ShouldDelete_ExistingSubject()
        {
            var existingSubject = await SeedAsync(new SeedSubjectRequest());

            var existsBeforeDeleting = await _controller.IsSubjectCodeExisting(existingSubject.SubjectCode);

            await _controller.DeleteSubject(existingSubject.SubjectCode);

            var existsAfterDeleting = await _controller.IsSubjectCodeExisting(existingSubject.SubjectCode);

            Assert.Multiple(() =>
            {
                Assert.True(existsBeforeDeleting);

                Assert.False(existsAfterDeleting);
            });
        }
    }
}
