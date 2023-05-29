namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.SubjectControllerTests
{
    public class GetAllSubjectsTests : BaseSubjectControllerTest
    {        
        [Fact]
        public async Task GetAllSubjects_Should_ReturnExistingSubjects()
        {
            var existingSubjectOne = await SeedAsync(new SeedSubjectRequest());
            var existingSubjectTwo = await SeedAsync(new SeedSubjectRequest());
            var existingSubjectThree = await SeedAsync(new SeedSubjectRequest());

            var results = await _controller.GetAllSubjects();

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(results);

                Assert.Contains(existingSubjectOne.SubjectCode, results.Select(_ => _.SubjectCode));
                Assert.Contains(existingSubjectTwo.SubjectCode, results.Select(_ => _.SubjectCode));
                Assert.Contains(existingSubjectThree.SubjectCode, results.Select(_ => _.SubjectCode));
            });
        }
    }
}
