using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.RequestHandlers.CourseHandlers
{
    public class InsertCourseRequest : IRequest<Course>, IValidatable
    {
        public string SubjectCode { get; set; } = null!;
        public string CourseName { get; set; } = null!;

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(SubjectCode, nameof(SubjectCode))
                .AddFailureIfNullOrWhiteSpace(CourseName, nameof(CourseName))
            .IsValidWhenNoFailures();
    }

    internal class InsertCourseHandler : DataOrchestratorHandler<InsertCourseRequest, Course>
    {
        public InsertCourseHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<Course> HandleRequestAsync(InsertCourseRequest request)
        {
            var courseCode = await _orchestrator.GetResponseAsync<GetUniqueCourseCodeRequest, string>(new(request.SubjectCode));

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertCourse(courseCode, request.CourseName));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw new ExpectationFailedException(nameof(InsertCourseRequest));
            }

            var course = await _dataAccess.FetchAsync(new GetCourseByCourseCode(courseCode));

            return new Course()
            {
                CourseCode = course.CourseCode,
                Name = course.Name
            };
        }
    }
}
