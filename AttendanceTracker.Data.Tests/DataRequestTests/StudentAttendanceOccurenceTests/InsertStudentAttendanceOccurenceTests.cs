using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentAttendanceOccurenceTests
{
    public class InsertStudentAttendanceOccurenceTests : DataTest
    {
        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_StudentDoesNotExist_ShouldReturn_NoRowsUpdated()
        {
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            var request = new InsertStudentAttendanceOccurence(studentCode: RandomString(), courseScheduledGuid: existingCourseScheduled.Guid,
                                                               guid: Guid.NewGuid(), dateOfOccurence: DateTime.Today, notes: RandomString(), isExcused: true);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_CourseScheduledDoesNotExist_ShouldReturn_NoRowsUpdated()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var request = new InsertStudentAttendanceOccurence(studentCode: existingStudent.StudentCode, courseScheduledGuid: Guid.NewGuid(),
                                                               guid: Guid.NewGuid(), dateOfOccurence: DateTime.Today, notes: RandomString(), isExcused: true);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_ValidRequest_ShouldInsert_AttendanceOccurence()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            var existingStudentCourseScheduled = await SeedAsync(new SeedStudentCourseScheduledRequest(existingStudent.StudentCode, existingCourseScheduled.Guid));

            var request = new InsertStudentAttendanceOccurence(studentCode: existingStudent.StudentCode, courseScheduledGuid: existingCourseScheduled.Guid,
                                                               guid: Guid.NewGuid(), dateOfOccurence: DateTime.Today, notes: RandomString(), isExcused: true);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            var recordInserted = await _dataAccess.FetchAsync(new GetStudentAttendanceOccurence(request.Guid));

            await _dataAccess.ExecuteAsync(new DeleteStudentAttendanceOccurence(request.Guid));

            Assert.Multiple(() =>
            {
                Assert.True(rowsAffected.AnyRowsAreUpdated());

                Assert.Equal(request.Notes, recordInserted.Notes);
                Assert.Equal(request.DateOfOccurence, recordInserted.DateOfOccurence);
                Assert.Equal(request.IsExcused, recordInserted.IsExcused);
            });
        }
    }
}
