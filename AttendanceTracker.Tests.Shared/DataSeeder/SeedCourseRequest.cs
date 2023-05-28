using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Tests.Shared.DataSeeder
{
    public class SeedCourseRequest : DataSeederRequest<Course_DTO>
    {
        public SeedCourseRequest(string? courseCode = null, string? subjectCode = null, string? name = null) 
        {
            CourseCode = courseCode;
            SubjectCode = subjectCode;
            Name = name;
        }

        public string? CourseCode { get; set; }
        public string? SubjectCode { get; set; }
        public string? Name{ get; set; }

        public override async Task<Course_DTO> ExecuteAsync(DataSeeder dataSeeder)
        {
            if (string.IsNullOrWhiteSpace(CourseCode)) CourseCode = _randomStringFactory.RandomStringLettersOrNumbers(CourseCodeConstants.MaxLength);

            if (string.IsNullOrWhiteSpace(SubjectCode)) SubjectCode = (await new SeedSubjectRequest().ExecuteAsync(dataSeeder)).SubjectCode;
            
            if (string.IsNullOrWhiteSpace(Name)) Name = _randomStringFactory.RandomStringLettersOrNumbers();

            return await dataSeeder.SeedFetchAndQueueForDeletionAsync(
                new InsertCourse(CourseCode, SubjectCode, Name),
                new GetCourseByCourseCode(CourseCode),
                new DeleteCourse(CourseCode)
            );
        }
    }
}
