using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class UpdateStudentTests : DataTest
    {
        [Fact]
        public async Task UpdateStudent_Given_StudentDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateStudent("StudentCodeNotExisting"));

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task UpdateStudent_Given_StudentExists_ShouldReturn_One()
        {
            var student = await _dataSeeder.NewStudent();

            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateStudent(student.StudentCode));

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task UpdateStudent_Given_NewValuesProvided_ShouldUpdate_RecordWithMatchingStudentCode()
        {
            var student = await _dataSeeder.NewStudent(firstName: "OriginalFirstName", lastName: "OriginalLastName", dateOfBirth: DateTime.Today);

            var expected = new UpdateStudent(student.StudentCode, firstName: "NewFirstName", lastName: "NewLastName", dateOfBirth: DateTime.Today.AddDays(1));

            // execute UpdateStudent Data Transaction
            await _dataAccess.ExecuteAsync(expected);

            var actual = await _dataAccess.FetchAsync(new GetStudentByCode(expected.Code));

            Assert.Multiple(() =>
            {
                Assert.NotNull(actual);

                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
                Assert.Equal(expected.DateOfBirth, actual.DateOfBirth);
            });
        }
    }
}
