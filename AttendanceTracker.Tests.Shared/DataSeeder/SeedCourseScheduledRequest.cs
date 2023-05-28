using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;

namespace AttendanceTracker.Tests.Shared.DataSeeder
{
    public class SeedCourseScheduledRequest : DataSeederRequest<CourseScheduled_DTO>
    {
        public SeedCourseScheduledRequest(Guid? guid = null, string? courseCode = null, string? instructorCode = null, DateTime? startDate = null, DateTime? endDate = null) 
        {
            Guid = guid;
            CourseCode = courseCode;
            InstructorCode = instructorCode;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Guid? Guid { get; set; }
        public string? CourseCode { get; set; }
        public string? InstructorCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public override async Task<CourseScheduled_DTO> ExecuteAsync(DataSeeder dataSeeder)
        {
            if (Guid == null) Guid = System.Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(CourseCode)) CourseCode = (await new SeedCourseRequest().ExecuteAsync(dataSeeder)).CourseCode;

            if (string.IsNullOrWhiteSpace(InstructorCode)) InstructorCode = (await new SeedInstructorRequest().ExecuteAsync(dataSeeder)).InstructorCode;

            if (!StartDate.HasValue) StartDate = DateTime.Now.AddDays(-10);

            if (!EndDate.HasValue) EndDate = DateTime.Now.AddDays(10);

            return await dataSeeder.SeedFetchAndQueueForDeletionAsync(
                new InsertCourseScheduled(Guid.Value, CourseCode, InstructorCode, StartDate.Value, EndDate.Value),
                new GetCourseScheduledByGuid(Guid.Value),
                new DeleteCourseScheduled(Guid.Value)
            );
        }
    }
}
