using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers
{
    public class GetAllStudentCourseScheduledByStudentCodeRequest : RequiredCodeRequest<StudentCoursesScheduled>
    {
        public GetAllStudentCourseScheduledByStudentCodeRequest() { }

        public GetAllStudentCourseScheduledByStudentCodeRequest(string code) : base(code) { }
    }

    internal class GetAllStudentCourseScheduledByStudentCodeHandler 
    : DataOrchestratorHandler<GetAllStudentCourseScheduledByStudentCodeRequest, StudentCoursesScheduled>
    {
        public GetAllStudentCourseScheduledByStudentCodeHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<StudentCoursesScheduled> HandleRequestAsync(GetAllStudentCourseScheduledByStudentCodeRequest request)
        {
            var coursesDTO = await _dataAccess.FetchListAsync(new GetAllStudentCourseScheduledByStudentCode(request.Code));

            if (!coursesDTO.Any())
            {
                throw new DoesNotExistException(typeof(StudentCourseScheduled), request.Code, "StudentCode");
            }

            var studentDTO = await _dataAccess.FetchAsync(new GetStudentByCode(request.Code));

            var getCoursesTasks = coursesDTO.Select(_ =>
                _orchestrator.GetResponseAsync<GetCourseScheduledByGuidRequest, CourseScheduled>(new GetCourseScheduledByGuidRequest(_.Guid)));

            await Task.WhenAll(getCoursesTasks);

            return new StudentCoursesScheduled(studentDTO.AsStudent(), getCoursesTasks.Select(_ => _.Result));
        }
    }
}