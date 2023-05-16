namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class UpdateStudentTests : DataTest
    {
        [Fact]
        public async Task UpdateStudent_Given_StudentDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(_requestFactory.UpdateStudent());

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task UpdateStudent_Given_StudentExists_ShouldReturn_One()
        {
            await _dataAccess.ExecuteAsync(_requestFactory.InsertStudent());

            var rowsAffected = await _dataAccess.ExecuteAsync(_requestFactory.UpdateStudent());

            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task UpdateStudent_Given_NewValuesProvided_ShouldUpdate_RecordWithMatchingStudentCode()
        {
            await _dataAccess.ExecuteAsync(_requestFactory.InsertStudent());

            var updateRequest = _requestFactory.UpdateStudent(firstName: "NewFirstName", lastName: "NewLastName", dateOfBirth: DateTime.Today.AddDays(1));

            await _dataAccess.ExecuteAsync(updateRequest);

            var recordAfterUpdating = await _dataAccess.FetchAsync(_requestFactory.GetStudentByCode());

            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());

            Assert.Multiple(() =>
            {
                Assert.NotNull(recordAfterUpdating);

                Assert.Equal(updateRequest.FirstName, recordAfterUpdating.FirstName);
                Assert.Equal(updateRequest.LastName, recordAfterUpdating.LastName);
                Assert.Equal(updateRequest.DateOfBirth, recordAfterUpdating.DateOfBirth);
            });
        }
    }
}
