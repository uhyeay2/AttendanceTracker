using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Tests.Shared.DataSeeder
{
    public class SeedInstructorRequest : DataSeederRequest<Instructor_DTO>
    {
        public SeedInstructorRequest(string? instructorCode = null, string? firstName = null, string? lastName = null) 
        {
            InstructorCode = instructorCode;
            FirstName = firstName;
            LastName = lastName;
        }

        public string? InstructorCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public override async Task<Instructor_DTO> ExecuteAsync(DataSeeder dataSeeder)
        {
            if (string.IsNullOrWhiteSpace(InstructorCode)) InstructorCode = _randomStringFactory.RandomStringLettersOrNumbers(InstructorCodeConstants.MaxLength);

            if (string.IsNullOrWhiteSpace(FirstName)) FirstName = _randomStringFactory.RandomStringLettersOrNumbers();

            if (string.IsNullOrWhiteSpace(LastName)) LastName = _randomStringFactory.RandomStringLettersOrNumbers();

            return await dataSeeder.SeedFetchAndQueueForDeletionAsync(
                new InsertInstructor(InstructorCode, FirstName, LastName),
                new GetInstructorByCode(InstructorCode),
                new DeleteInstructor(InstructorCode)
            );
        }
    }
}
