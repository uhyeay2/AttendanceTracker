using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class GetStudentsByNameTests : DataTest
    {
        [Fact]
        public async Task GetStudentsByName_Given_NoFirstOrLastName_ShouldReturn_EmptyList()
        {
            await _dataSeeder.NewStudent();

            var result = await _dataAccess.FetchListAsync(new GetStudentsByName(null, null));

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetStudentsByName_Given_LastName_ShouldReturn_StudentsWithLastName()
        {
            var lastNameToSearchBy = "SearchWithLastName";

            // Insert Student w/ LastName
            await _dataSeeder.NewStudent(lastName: lastNameToSearchBy);
            await _dataSeeder.NewStudent(lastName: lastNameToSearchBy);

            // Insert Third Student - Not using LastName
            await _dataSeeder.NewStudent(lastName: "NotAMatchingName");

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
            var firstNameToSearchBy = "SearchWithFirstName";

            await _dataSeeder.NewStudent(firstName: firstNameToSearchBy);
            await _dataSeeder.NewStudent(firstName: firstNameToSearchBy);

            // Insert Third Student - Not using FirstName
            await _dataSeeder.NewStudent(firstName: "NotAMatchingName");

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
