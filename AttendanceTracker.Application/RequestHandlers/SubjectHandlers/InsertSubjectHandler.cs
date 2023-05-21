using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.RequestHandlers.SubjectHandlers
{
    public class InsertSubjectRequest : IRequest<Subject>, IValidatable
    {
        public string SubjectCode { get; set; } = null!;
        public string Name { get; set; } = null!;

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(SubjectCode, nameof(SubjectCode))
                .AddFailureIfNullOrWhiteSpace(Name, nameof(Name))
            .IsValidWhenNoFailures();
    }

    internal class InsertSubjectHandler : DataHandler<InsertSubjectRequest, Subject>
    {
        public InsertSubjectHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<Subject> HandleRequestAsync(InsertSubjectRequest request)
        {
            if (await _dataAccess.FetchAsync(new IsSubjectCodeExisting(request.SubjectCode)))
            {
                throw new AlreadyExistsException(typeof(Subject), (request.SubjectCode, nameof(request.SubjectCode)));
            }

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertSubject(request.SubjectCode, request.Name));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw new ExpectationFailedException(nameof(InsertSubjectRequest));
            }

            var dto = await _dataAccess.FetchAsync(new GetSubjectByCode(request.SubjectCode));

            return new Subject(dto.SubjectCode, dto.Name);
        }
    }
}
