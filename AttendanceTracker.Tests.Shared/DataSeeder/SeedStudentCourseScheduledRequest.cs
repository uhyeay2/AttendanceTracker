using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;

namespace AttendanceTracker.Tests.Shared.DataSeeder
{
    public class SeedStudentCourseScheduledRequest : DataSeederRequest<StudentCourseScheduled_DTO>
    {
        public SeedStudentCourseScheduledRequest(string? studentCode = null, Guid? courseScheduledGuid = null)
        {
            StudentCode = studentCode;
            CourseScheduledGuid = courseScheduledGuid;
        }

        public string? StudentCode { get; set; }
        public Guid? CourseScheduledGuid { get; set; }

        public override async Task<StudentCourseScheduled_DTO> ExecuteAsync(DataSeeder dataSeeder)
        {
            if(string.IsNullOrWhiteSpace(StudentCode)) StudentCode = (await new SeedStudentRequest().ExecuteAsync(dataSeeder)).StudentCode;

            if(CourseScheduledGuid == null) CourseScheduledGuid = (await new SeedCourseScheduledRequest().ExecuteAsync(dataSeeder)).Guid;

            return await dataSeeder.SeedFetchAndQueueForDeletionAsync(
                new InsertStudentCourseScheduled(StudentCode, CourseScheduledGuid.Value),
                new GetStudentCourseScheduled(StudentCode, CourseScheduledGuid.Value),
                new DeleteStudentCourseScheduled(StudentCode, CourseScheduledGuid.Value)
            );
        }
    }
}
