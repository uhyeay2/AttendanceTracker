using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentHandlerTests
{
    public class GetAllStudentsPaginatedHandlerTests : HandlerTest
    {
        private readonly GetAllStudentsPaginatedHandler _handler;

        public GetAllStudentsPaginatedHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetAllStudentsPaginated_Given_NoStudentsFound_ShouldThrow_DoesNotExistException()
        {
            SetupFetchListAsync<GetAllStudentsPaginated, Student_DTO>(Enumerable.Empty<Student_DTO>());

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task GetAllStudentsPaginated_Given_StudentsFound_ShouldReturn_StudentsAsDomainObject()
        {
            var studentDTOs = A.ListOf<Student_DTO>();

            var expected = studentDTOs.Select(_ => _.AsStudent());

            SetupFetchListAsync<GetAllStudentsPaginated, Student_DTO>(studentDTOs);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(result);

                Assert.Equal(result.Select(_ => _.StudentCode), expected.Select(_ => _.StudentCode));
            });
        }
    }
}
