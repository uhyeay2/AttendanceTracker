namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class GetStudentByCodeTests : DataTest
    {
        [Fact]
        public async Task GetStudentByCode_Given_StudentDoesNotExist_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(_requestFactory.GetStudentByCode()));
        }

        [Fact]
        public async Task GetStudentByCode_Given_StudentExists_ShouldReturn_Student()
        {
            var insertRequest = _requestFactory.InsertStudent();

            await _dataAccess.ExecuteAsync(insertRequest);

            var result = await _dataAccess.FetchAsync(_requestFactory.GetStudentByCode());

            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());

            var expected = insertRequest.Student;

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.StudentCode, result.StudentCode);
                Assert.Equal(expected.DateOfBirth, result.DateOfBirth);
                Assert.Equal(expected.FirstName, result.FirstName);
                Assert.Equal(expected.LastName, result.LastName);
            });
        }
    }
}
