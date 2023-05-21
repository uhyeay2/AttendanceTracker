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

            // Insert Student w/ LastName
            _ = await GetSeededStudentAsync(lastName: lastNameToSearchBy);
            _ = await GetSeededStudentAsync(lastName: lastNameToSearchBy);

            // Insert Third Student - Not using LastName
            _ = await GetSeededStudentAsync();

            // Expect only two records back
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

            _ = await GetSeededStudentAsync(firstName: firstNameToSearchBy);
            _ = await GetSeededStudentAsync(firstName: firstNameToSearchBy);

            // Insert Third Student - Not using FirstName
            _ = await GetSeededStudentAsync();

            // Expect only two records back
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
