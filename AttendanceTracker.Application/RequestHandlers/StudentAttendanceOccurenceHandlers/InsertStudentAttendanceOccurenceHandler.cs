using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers
{
    public class InsertStudentAttendanceOccurenceRequest : RequiredStudentCodeAndCourseScheduledGuidRequest<StudentAttendanceOccurence>
    {
        public InsertStudentAttendanceOccurenceRequest() { }

        public InsertStudentAttendanceOccurenceRequest(string studentCode, Guid courseScheduledGuid, DateTime dateOfOccurence, string notes, bool isExcused) : base(studentCode, courseScheduledGuid)
        {
            DateOfOccurence = dateOfOccurence;
            Notes = notes;
            IsExcused = isExcused;
        }

        public DateTime DateOfOccurence { get; set; }

        public string Notes { get; set; } = null!;

        public bool IsExcused { get; set; }

        public override bool IsValid(out List<string> validationFailures) =>
             Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(StudentCode, nameof(StudentCode))
                .AddFailureIfEmpty(CourseScheduledGuid, nameof(CourseScheduledGuid))
                .AddFailureIfDateTimeIsMinValue(DateOfOccurence, nameof(DateOfOccurence))
                .AddFailureIfNullOrWhiteSpace(Notes, nameof(Notes))
            .IsValidWhenNoFailures();
    }

    internal class InsertStudentAttendanceOccurenceHandler : DataOrchestratorHandler<InsertStudentAttendanceOccurenceRequest, StudentAttendanceOccurence>
    {
        public InsertStudentAttendanceOccurenceHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<StudentAttendanceOccurence> HandleRequestAsync(InsertStudentAttendanceOccurenceRequest request)
        {
            var attendanceGuid = Guid.NewGuid();

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudentAttendanceOccurence(request.StudentCode, request.CourseScheduledGuid, attendanceGuid, request.DateOfOccurence, request.Notes, request.IsExcused));

            if (rowsAffected.AnyRowsAreUpdated())
            {
                return await _orchestrator.GetResponseAsync<GetStudentAttendanceOccurenceRequest, StudentAttendanceOccurence>(new(attendanceGuid));
            }

            if (await _dataAccess.FetchAsync(new IsStudentCourseScheduledExisting(request.StudentCode, request.CourseScheduledGuid)))
            {
                throw new ExpectationFailedException(nameof(InsertStudentAttendanceOccurence));
            }

            throw new DoesNotExistException(typeof(StudentCourseScheduled), (request.StudentCode, nameof(request.StudentCode)),
                                                                            (request.CourseScheduledGuid, nameof(request.CourseScheduledGuid)));
        }
    }
}
