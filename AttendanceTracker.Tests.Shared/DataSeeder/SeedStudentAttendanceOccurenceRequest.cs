using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;

namespace AttendanceTracker.Tests.Shared.DataSeeder
{
    public class SeedStudentAttendanceOccurenceRequest : DataSeederRequest<StudentAttendanceOccurence_DTO>
    {
        public SeedStudentAttendanceOccurenceRequest(string? studentCode = null, Guid? courseScheduledGuid = null, Guid? guid = null, DateTime? dateOfOccurence = null, string? notes = null, bool? isExcused = null)
        {
            StudentCode = studentCode;
            CourseScheduledGuid = courseScheduledGuid;
            Guid = guid;
            DateOfOccurence = dateOfOccurence;
            Notes = notes;
            IsExcused = isExcused;
        }

        public string? StudentCode { get; set; }

        public Guid? CourseScheduledGuid { get; set; }

        public Guid? Guid { get; set; }

        public DateTime? DateOfOccurence { get; set; }

        public string? Notes { get; set; }

        public bool? IsExcused { get; set; }


        public override async Task<StudentAttendanceOccurence_DTO> ExecuteAsync(DataSeeder dataSeeder)
        {
            if(string.IsNullOrWhiteSpace(StudentCode)) StudentCode = (await new SeedStudentRequest().ExecuteAsync(dataSeeder)).StudentCode;

            CourseScheduledGuid ??= (await new SeedCourseScheduledRequest().ExecuteAsync(dataSeeder)).Guid;

            await new SeedStudentCourseScheduledRequest(StudentCode, CourseScheduledGuid).ExecuteAsync(dataSeeder);

            Guid ??= System.Guid.NewGuid();

            DateOfOccurence ??= DateTime.Now;

            if (string.IsNullOrWhiteSpace(Notes)) Notes = _randomStringFactory.RandomStringLettersOrNumbers();

            IsExcused ??= true;

            return await dataSeeder.SeedFetchAndQueueForDeletionAsync(
                new InsertStudentAttendanceOccurence(StudentCode, CourseScheduledGuid.Value, Guid.Value, DateOfOccurence.Value, Notes, IsExcused.Value),
                new GetStudentAttendanceOccurence(Guid.Value),
                new DeleteStudentAttendanceOccurence(Guid.Value));
        }
    }
}
