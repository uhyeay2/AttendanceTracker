using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers
{
    public class InsertStudentCourseScheduledRequest : RequiredStudentCodeAndCourseScheduledGuidRequest<StudentCourseScheduled>
    {
        public InsertStudentCourseScheduledRequest() { }

        public InsertStudentCourseScheduledRequest(string studentCode, Guid courseScheduledGuid) : base(studentCode, courseScheduledGuid) { }
    }

    internal class InsertStudentCourseScheduledHandler : DataOrchestratorHandler<InsertStudentCourseScheduledRequest, StudentCourseScheduled>
    {
        public InsertStudentCourseScheduledHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<StudentCourseScheduled> HandleRequestAsync(InsertStudentCourseScheduledRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudentCourseScheduled(request.StudentCode, request.CourseScheduledGuid));

            if (rowsAffected.AnyRowsAreUpdated())
            {
                return await _orchestrator.GetResponseAsync<GetStudentCourseScheduledRequest, StudentCourseScheduled>(new(request.StudentCode, request.CourseScheduledGuid));
            }

            if (await _dataAccess.FetchAsync(new IsStudentCourseScheduledExisting(request.StudentCode, request.CourseScheduledGuid)))
            {
                throw new AlreadyExistsException(typeof(StudentCourseScheduled), (request.StudentCode, nameof(request.StudentCode)),
                                                                                 (request.CourseScheduledGuid, nameof(request.CourseScheduledGuid)));
            }

            if (await _dataAccess.FetchAsync(new IsStudentCodeExisting(request.StudentCode)))
            {
                throw new DoesNotExistException(typeof(Student), (request.StudentCode, nameof(request.StudentCode)));
            }

            if (await _dataAccess.FetchAsync(new IsCourseScheduledGuidExisting(request.CourseScheduledGuid)))
            {
                throw new DoesNotExistException(typeof(CourseScheduled), (request.CourseScheduledGuid, nameof(request.CourseScheduledGuid)));
            }

            throw new ExpectationFailedException(nameof(InsertCourseScheduledRequest));
        }
    }
}
