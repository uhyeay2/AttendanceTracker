namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentControllerTests
{
    public class InsertStudentTests : BaseStudentControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task InsertStudent_Given_FirstNameNotProvided_ShouldThrow_ValidationFailedException(string firstName)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertStudent(new(firstName, "LastName", DateTime.Now)));
        }

        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task InsertStudent_Given_LastNameNotProvided_ShouldThrow_ValidationFailedException(string lastName)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertStudent(new("FirstName", lastName, DateTime.Now)));
        }
    }
}
