using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class GetAllStudentsPaginatedRequest : IRequest<IEnumerable<Student>>, IValidatable
    {
        public GetAllStudentsPaginatedRequest() { }

        public GetAllStudentsPaginatedRequest(int pageNumber, int recordsPerPage)
        {
            PageNumber = pageNumber;
            RecordsPerPage = recordsPerPage;
        }

        public int PageNumber { get; set; }
        public int RecordsPerPage { get; set; }

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfOutsideRange(PageNumber, nameof(PageNumber), minLength: 1)
                .AddFailureIfOutsideRange(RecordsPerPage, nameof(RecordsPerPage), minLength: 1, maxLength: 150)
            .IsValidWhenNoFailures();
    }

    internal class GetAllStudentsPaginatedHandler : DataHandler<GetAllStudentsPaginatedRequest, IEnumerable<Student>>
    {
        public GetAllStudentsPaginatedHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<IEnumerable<Student>> HandleRequestAsync(GetAllStudentsPaginatedRequest request)
        {
            var dto = await _dataAccess.FetchListAsync(new GetAllStudentsPaginated(request.PageNumber, request.RecordsPerPage));

            return dto.Any() ? dto.Select(_ => _.AsStudent()) 
                : throw new DoesNotExistException(typeof(Student), 
                    (request.PageNumber, nameof(request.PageNumber)), 
                    (request.RecordsPerPage, nameof(request.RecordsPerPage))
                );
        }
    }
}
