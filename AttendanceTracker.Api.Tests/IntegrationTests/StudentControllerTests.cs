using AttendanceTracker.Api.Controllers;
using AttendanceTracker.Api.Tests.TestHelpers;
using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using GenFu;

namespace AttendanceTracker.Api.Tests.IntegrationTests
{
    public class StudentControllerTests : ControllerTest
    {
        private readonly StudentController _controller;

        public StudentControllerTests() => _controller = new(_orchestrator);

        [Fact]
        public async Task InsertStudent_Given_ValidRequest_Should_InsertStudent()
        {
            var request = A.New<InsertStudentRequest>();

            var student = await _controller.InsertStudent(request);

            await _controller.DeleteStudent(student.StudentCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(student);

                Assert.Equal(request.FirstName, student.FirstName);
                Assert.Equal(request.LastName, student.LastName);
                Assert.Equal(request.DateOfBirth, student.DateOfBirth);
            });
        }
    }
}
