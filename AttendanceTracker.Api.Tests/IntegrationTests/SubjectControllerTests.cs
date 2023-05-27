using AttendanceTracker.Application.RequestHandlers.SubjectHandlers;

namespace AttendanceTracker.Api.Tests.IntegrationTests
{
    public class SubjectControllerTests : ControllerTest
    {
        private readonly SubjectController _controller;

        public SubjectControllerTests() => _controller = new(_orchestrator);

        [Fact]
        public async Task GetSubjectByCode_Given_SubjectIsInserted_Should_ReturnSubject()
        {
            var insertSubjectRequest = new InsertSubjectRequest() { SubjectCode = "INS", Name = "Insert Gets Fetched Test Subject" };

            var insertResult = await _controller.InsertSubject(insertSubjectRequest);

            var getByCodeResult = await _controller.GetSubjectBySubjectCode(insertSubjectRequest.SubjectCode);

            await _controller.DeleteSubject(insertSubjectRequest.SubjectCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(insertResult);
                Assert.Equal(insertSubjectRequest.Name, insertResult.Name);
                Assert.Equal(insertSubjectRequest.SubjectCode, insertResult.SubjectCode);

                Assert.NotNull(getByCodeResult);
                Assert.Equal(insertSubjectRequest.Name, getByCodeResult.Name);
                Assert.Equal(insertSubjectRequest.SubjectCode, getByCodeResult.SubjectCode);
            });
        }

        [Fact]
        public async Task GetAllSubjects_Given_RequestIsCalled_Should_ReturnSubjects()
        {
            var insertSubjectRequests = new List<InsertSubjectRequest>()
            {
                new() {SubjectCode = "AAA", Name = "GetAllSubject 1"},
                new() {SubjectCode = "BBB", Name = "GetAllSubject 2"},
                new() {SubjectCode = "CCC", Name = "GetAllSubject 3"}
            };

            foreach (var request in insertSubjectRequests)
            {
                await _controller.InsertSubject(request);
            }

            var allSubjects = await _controller.GetAllSubjects();

            foreach (var request in insertSubjectRequests)
            {
                await _controller.DeleteSubject(request.SubjectCode);
            }

            Assert.True(allSubjects.Count() >= insertSubjectRequests.Count);
        }

        [Fact]
        public async Task IsSubjectCodeExisting_Given_SubjectCodeNotExisting_Should_ReturnFalse()
        {
            Assert.False(await _controller.IsSubjectCodeExisting("FAKE"));
        }

        [Fact]
        public async Task IsSubjectCodeExisting_Given_SubjectCodeIsExisting_Should_ReturnTrue()
        {
            var insertSubjectRequest = A.New<InsertSubjectRequest>();

            insertSubjectRequest.SubjectCode = "EXIST";

            await _controller.InsertSubject(insertSubjectRequest);

            var exists = await _controller.IsSubjectCodeExisting(insertSubjectRequest.SubjectCode);

            await _controller.DeleteSubject(insertSubjectRequest.SubjectCode);

            Assert.True(exists);
        }
    }
}
