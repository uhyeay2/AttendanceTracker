using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class GetStudentsByNameTests : DataTest
    {
        [Fact]
        public async Task GetStudentsByName_Given_NoFirstOrLastName_ShouldReturn_EmptyList()
        {
            _ = await GetSeededStudentAsync();

            var result = await _dataAccess.FetchListAsync(new GetStudentsByName(null, null));

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetStudentsByName_Given_LastName_ShouldReturn_StudentsWithLastName()
        {
            var lastNameToSearchBy = RandomString();

            await GetSeededStudentAsync();
            await GetSeededStudentAsync(lastName: lastNameToSearchBy);
            await GetSeededStudentAsync(lastName: lastNameToSearchBy);

            // Expect only the two records w/ lastNameToSearchBy to be returned
            var result = await _dataAccess.FetchListAsync(new GetStudentsByName(lastName: lastNameToSearchBy));

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(result);

                Assert.True(result.All(_ => _.LastName == lastNameToSearchBy));
                Assert.Equal(2, result.Count());
            });
        }

        [Fact]
        public async Task GetStudentsByName_Given_FirstName_ShouldReturn_StudentsWithFirstName()
        {
            var firstNameToSearchBy = RandomString();

            await GetSeededStudentAsync();
            await GetSeededStudentAsync(firstName: firstNameToSearchBy);
            await GetSeededStudentAsync(firstName: firstNameToSearchBy);

            // Expect only the two records w/ firstNameToSearchBy to be returned
            var result = await _dataAccess.FetchListAsync(new GetStudentsByName(firstName: firstNameToSearchBy));

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(result);

                Assert.True(result.All(_ => _.FirstName.Contains(firstNameToSearchBy)));
                Assert.Equal(2, result.Count());
            });
        }
    }
}
