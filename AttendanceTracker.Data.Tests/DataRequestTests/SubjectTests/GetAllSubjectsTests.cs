using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;
using AttendanceTracker.Data.DataTransferObjects;

namespace AttendanceTracker.Data.Tests.DataRequestTests.SubjectTests
{
    public class GetAllSubjectsTests : DataTest
    {
        [Fact]
        public async Task GetAllSubjects_Given_SubjectsExist_Should_ReturnSubjects()
        {
            var subjects = new List<Subject_DTO>();

            for (int i = 0; i < 3; i++)
            {
                subjects.Add(await GetSeededSubjectAsync());
            }

            var result = await _dataAccess.FetchListAsync(new GetAllSubjects());

            Assert.True(subjects.All(_ => result.Select(r => r.Id).Contains(_.Id)));
        }
    }
}
