using AttendanceTracker.Application.RequestHandlers.StudentHandlers;

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

        [Fact]
        public async Task InsertStudent_Given_DateOfBirthNotProvided_ShouldThrow_ValidationFailedException()
        {
            var insertRequest = new InsertStudentRequest()
            {
                FirstName = "FirstName",
                LastName = "LastName",
            };

            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertStudent(insertRequest));
        }

        [Fact]
        public async Task InsertStudent_Given_ValidRequest_ShouldReturn_StudentInserted()
        {
            var expected = new InsertStudentRequest("FirstName", "LastName", new DateTime(2023, 5, 29));

            var result = await _controller.InsertStudent(expected);

            await _controller.DeleteStudent(result.StudentCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.FirstName, result.FirstName);
                Assert.Equal(expected.LastName, result.LastName);
                Assert.Equal(expected.DateOfBirth, result.DateOfBirth);
            });
        }
    }
}
