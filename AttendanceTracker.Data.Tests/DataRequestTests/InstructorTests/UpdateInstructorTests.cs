using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.InstructorTests
{
    public class UpdateInstructorTests : DataTest
    {
        [Fact]
        public async Task UpdateInstructor_Given_InstructorDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateInstructor(RandomString()));

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task UpdateInstructor_Given_InstructorExists_ShouldReturn_One()
        {
            var Instructor = await SeedAsync(new SeedInstructorRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateInstructor(Instructor.InstructorCode));

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task UpdateInstructor_Given_NewValuesProvided_ShouldUpdate_RecordWithMatchingInstructorCode()
        {
            var seededInstructor = await SeedAsync(new SeedInstructorRequest());

            var expected = new UpdateInstructor(seededInstructor.InstructorCode, firstName: "NewFirstName", lastName: "NewLastName");

            // execute UpdateInstructor Data Transaction
            await _dataAccess.ExecuteAsync(expected);

            var actual = await _dataAccess.FetchAsync(new GetInstructorByCode(expected.Code));

            Assert.Multiple(() =>
            {
                Assert.NotNull(actual);

                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
            });
        }
    }
}
