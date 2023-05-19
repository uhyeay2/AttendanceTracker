using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Models;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentHandlerTests
{
    public class GetStudentsByNameHandlerTests : HandlerTest
    {
        private readonly GetStudentsByNameHandler _handler;

        public GetStudentsByNameHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetSudentsByName_Given_NoResultsFound_ShouldThrow_DoesNotExistException()
        {
            SetupFetchListAsync<GetStudentsByName, Student_DTO>(Enumerable.Empty<Student_DTO>());

            await Assert.ThrowsAnyAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task GetStudentsByName_Given_StudentsFound_ShouldReturn_StudentsFound()
        {
            var dto = GenFu.GenFu.ListOf<Student_DTO>();

            var expected = dto.Select(_ =>
                new Student(studentCode: _.StudentCode, firstName: _.FirstName, lastName: _.LastName, dateOfBirth: _.DateOfBirth));

            SetupFetchListAsync<GetStudentsByName, Student_DTO>(dto);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Equivalent(expected, result);
        }
    }
}
