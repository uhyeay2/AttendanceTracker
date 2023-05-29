namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.SubjectControllerTests
{
    public class InsertSubjectTests : BaseSubjectControllerTest
    {
        [Fact]
        public async Task InsertSubject_Given_SubjectCodeAlreadyExists_ShouldThrow_AlreadyExistsException()
        {
            var existingSubject = await SeedAsync(new SeedSubjectRequest(subjectCode: "SUB"));

            await Assert.ThrowsAsync<AlreadyExistsException>(async () => await _controller.InsertSubject(new(existingSubject.SubjectCode, RandomString())));
        }
       
        [Theory]
        [InlineData("AA")]
        [InlineData("AB")]
        [InlineData("A")]
        [InlineData("B")]
        public async Task InsertSubject_Given_SubjectCode_IsTooShort_ShouldThrow_ValidationException(string subjectCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertSubject(new(subjectCode, RandomString())));
        }

        [Theory]
        [InlineData("ABCDEF")]
        [InlineData("MaxIsFive")]
        [InlineData("TheseTooLong")]
        public async Task InsertSubject_Given_SubjectCode_IsTooLong_ShouldThrow_ValidationException(string subjectCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertSubject(new(subjectCode, RandomString())));
        }

        [Theory]
        [InlineData("123")]
        [InlineData("AB3")]
        [InlineData("&&&")]
        [InlineData("AB&")]
        public async Task InsertSubject_Given_SubjectCode_ContainsNonLetter_ShouldThrow_ValidationException(string subjectCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertSubject(new(subjectCode, RandomString())));
        }

        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task InsertSubject_Given_SubjectName_IsNotProvided_ShouldThrow_ValidationException(string subjectName)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertSubject(new("ZZZ", subjectName)));
        }

        [Theory]
        [InlineData("ZZZ", "Valid Request")]
        [InlineData("ZZZZ", "Should Get")]
        [InlineData("ZZZZZ", "Inserted And Returned")]
        public async Task InsertSubject_Given_ValidRequest_ShouldReturn_SubjectInserted(string subjectCode, string subjectName)
        {
            var result = await _controller.InsertSubject(new(subjectCode, subjectName));

            await _controller.DeleteSubject(subjectCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(subjectCode, result.SubjectCode);
                Assert.Equal(subjectName, result.Name);
            });
        }
    }
}
