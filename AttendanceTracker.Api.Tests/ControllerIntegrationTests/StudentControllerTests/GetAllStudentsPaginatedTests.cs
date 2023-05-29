namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentControllerTests
{
    public class GetAllStudentsPaginatedTests : BaseStudentControllerTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task GetAllStudentsPaginated_Given_PageNumberLessThanOne_ShouldThrow_ValidationFailedException(int pageNumber)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetAllStudentsPaginated(pageNumber, recordsPerPage: 10));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task GetAllStudentsPaginated_Given_RecordsPerPageLessThanOne_ShouldThrow_ValidationFailedException(int recordsPerPage)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetAllStudentsPaginated(pageNumber: 1, recordsPerPage));
        }

        [Theory]
        [InlineData(151)]
        [InlineData(175)]
        [InlineData(200)]
        public async Task GetAllStudentsPaginated_Given_RecordsPerPageMoreThan150_ShouldThrow_ValidationFailedException(int recordsPerPage)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetAllStudentsPaginated(pageNumber: 1, recordsPerPage));
        }

        [Fact]
        public async Task GetAllStudentsPaginated_Given_ValidRequest_ShouldReturn_ExpectedNumberOfStudents()
        {
            const int numberOfStudentsToSeed = 8;
            const int pageNumber = 1;
            const int recordsPerPage = 2;

            for (int i = 0; i < numberOfStudentsToSeed; i++)
            {
                await SeedAsync(new SeedStudentRequest());
            }

            var allStudents = await _controller.GetAllStudentsPaginated(1, numberOfStudentsToSeed);

            var firstPage = await _controller.GetAllStudentsPaginated(pageNumber, recordsPerPage);

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(allStudents); 
                Assert.Equal(numberOfStudentsToSeed, allStudents.Count());

                var expectedStudentsOnFirstPage = allStudents.Take(recordsPerPage);

                Assert.NotEmpty(firstPage);
                Assert.Equal(recordsPerPage, firstPage.Count());
                Assert.True(expectedStudentsOnFirstPage.Select(_ => _.StudentCode).All(firstPage.Select(_ => _.StudentCode).Contains));
            });
        }
    }
}
