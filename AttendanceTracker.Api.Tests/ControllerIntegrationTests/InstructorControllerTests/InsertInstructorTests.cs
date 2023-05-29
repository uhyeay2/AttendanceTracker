using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;

namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.InstructorControllerTests
{
    public class InsertInstructorTests : BaseInstructorControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task InsertInstructor_Given_FirstNameNotProvided_ShouldThrow_ValidationFailedException(string firstName)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertInstructor(new(firstName, "LastName")));
        }

        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task InsertInstructor_Given_LastNameNotProvided_ShouldThrow_ValidationFailedException(string lastName)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertInstructor(new("FirstName", lastName)));
        }
      
        [Fact]
        public async Task InsertInstructor_Given_ValidRequest_ShouldReturn_InstructorInserted()
        {
            var expected = new InsertInstructorRequest("FirstName", "LastName");

            var result = await _controller.InsertInstructor(expected);

            await _controller.DeleteInstructor(result.InstructorCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.FirstName, result.FirstName);
                Assert.Equal(expected.LastName, result.LastName);
            });
        }
    }
}
