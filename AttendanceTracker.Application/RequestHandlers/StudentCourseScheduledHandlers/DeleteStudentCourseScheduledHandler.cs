using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers
{
    public class DeleteStudentCourseScheduledRequest : RequiredStudentCodeAndCourseScheduledGuidRequest
    {
        public DeleteStudentCourseScheduledRequest() { }

        public DeleteStudentCourseScheduledRequest(string studentCode, Guid courseScheduledGuid) : base(studentCode, courseScheduledGuid) { }
    }

    internal class DeleteStudentCourseScheduledHandler : DataHandler<DeleteStudentCourseScheduledRequest>
    {
        public DeleteStudentCourseScheduledHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(DeleteStudentCourseScheduledRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudentCourseScheduled(request.StudentCode, request.CourseScheduledGuid));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsStudentCourseScheduledExisting(request.StudentCode, request.CourseScheduledGuid)) ?
                    new ExpectationFailedException(nameof(DeleteStudentCourseScheduledRequest))
                    : new DoesNotExistException(typeof(StudentCourseScheduled), (request.StudentCode, nameof(request.StudentCode)), 
                                                                                (request.CourseScheduledGuid, nameof(request.CourseScheduledGuid)));
            }
        }
    }
}
