namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class GetStudentsByName : DataTest
    {
        [Fact]
        public async Task GetStudentsByName_Given_NoFirstOrLastName_ShouldReturn_EmptyList()
        {
            await _dataAccess.ExecuteAsync(_requestFactory.InsertStudent());

            var result = await _dataAccess.FetchListAsync(_requestFactory.GetStudentsByName(null, null));

            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetStudentsByName_Given_LastName_ShouldReturn_StudentsWithLastName()
        {
            var lastNameToSearchBy = "LastNameTest";

            var firstStudent = _requestFactory.InsertStudent(studentCode: _testString[..10], lastName: lastNameToSearchBy);
            var secondStudent = _requestFactory.InsertStudent(studentCode: _testString[..9], lastName: lastNameToSearchBy);

            await _dataAccess.ExecuteAsync(_requestFactory.InsertStudent());
            await _dataAccess.ExecuteAsync(firstStudent);
            await _dataAccess.ExecuteAsync(secondStudent);

            var result = await _dataAccess.FetchListAsync(_requestFactory.GetStudentsByName(lastName: lastNameToSearchBy));

            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());
            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent(firstStudent.Student.StudentCode));
            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent(secondStudent.Student.StudentCode));

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
            var firstNameToSearchBy = "FirstNameTest";

            var firstStudent = _requestFactory.InsertStudent(studentCode: _testString[..10], firstName: firstNameToSearchBy);
            var secondStudent = _requestFactory.InsertStudent(studentCode: _testString[..9], firstName: $"{firstNameToSearchBy} The Second");

            await _dataAccess.ExecuteAsync(_requestFactory.InsertStudent());
            await _dataAccess.ExecuteAsync(firstStudent);
            await _dataAccess.ExecuteAsync(secondStudent);

            var result = await _dataAccess.FetchListAsync(_requestFactory.GetStudentsByName(firstName: firstNameToSearchBy));

            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());
            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent(firstStudent.Student.StudentCode));
            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent(secondStudent.Student.StudentCode));

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(result);

                Assert.True(result.All(_ => _.FirstName.Contains(firstNameToSearchBy)));
                Assert.Equal(2, result.Count());
            });
        }
    }
}
