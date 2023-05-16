using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class GetStudentByStudentCodeRequest : RequiredStudentCodeRequest<Student> { }

    internal class GetStudentByStudentCodeHandler : DataTaskHandler<GetStudentByStudentCodeRequest, Student>
    {
        public GetStudentByStudentCodeHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<Student> HandleRequestAsync(GetStudentByStudentCodeRequest request)
        {
            var dto = await _dataAccess.FetchAsync(new GetStudentByCode(request.StudentCode));

            return dto == null
                ? throw new DoesNotExistException(typeof(Student), request.StudentCode, nameof(request.StudentCode))
                : new Student(dto.StudentCode, dto.FirstName, dto.LastName, dto.DateOfBirth);
        }
    }
}
