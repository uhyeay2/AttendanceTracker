using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers
{
    public class InsertCourseScheduledRequest : IRequest<CourseScheduled>, IValidatable
    {
        public InsertCourseScheduledRequest() { }

        public InsertCourseScheduledRequest(string courseCode, string instructorCode, DateTime startDate, DateTime endDate)
        {
            CourseCode = courseCode;
            InstructorCode = instructorCode;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string CourseCode { get; set; } = string.Empty;
        public string InstructorCode { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfOutsideRange(CourseCode, nameof(CourseCode), maxLength: CourseCodeConstants.MaxLength)
                .AddFailureIfOutsideRange(InstructorCode, nameof(InstructorCode), maxLength: InstructorCodeConstants.MaxLength)
                .AddFailureIfDateTimeIsMinValue(StartDate, nameof(StartDate))
                .AddFailureIfDateTimeIsMinValue(EndDate, nameof(EndDate))
            .IsValidWhenNoFailures();
    }

    internal class InsertCourseScheduledHandler : DataOrchestratorHandler<InsertCourseScheduledRequest, CourseScheduled>
    {
        public InsertCourseScheduledHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<CourseScheduled> HandleRequestAsync(InsertCourseScheduledRequest request)
        {
            var guid = Guid.NewGuid();

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertCourseScheduled(guid, request.CourseCode, request.InstructorCode, request.StartDate, request.EndDate));

            if (rowsAffected.AnyRowsAreUpdated())
            {
                return await _orchestrator.GetResponseAsync<GetCourseScheduledByGuidRequest, CourseScheduled>(new GetCourseScheduledByGuidRequest(guid));
            }
                
            List<(object?, string)> codesNotExisting = new();

            if (!await _dataAccess.FetchAsync(new IsCourseCodeExisting(request.CourseCode)))
            {
                codesNotExisting.Add((request.CourseCode, nameof(request.CourseCode)));
            }

            if (!await _dataAccess.FetchAsync(new IsInstructorCodeExisting(request.InstructorCode)))
            {
                codesNotExisting.Add((request.InstructorCode, nameof(request.InstructorCode)));
            }

            if (codesNotExisting.Any())
            {
                throw new DoesNotExistException(typeof(CourseScheduled), codesNotExisting);
            }

            throw new ExpectationFailedException(nameof(InsertCourseScheduledRequest));
        }
    }
}
