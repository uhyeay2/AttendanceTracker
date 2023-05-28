using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Tests.Shared.DataSeeder
{
    public class SeedStudentRequest : DataSeederRequest<Student_DTO>
    {
        public SeedStudentRequest(string? studentCode = null, string? firstName = null, string? lastName = null, DateTime? dateOfBirth = null) 
        {
           StudentCode = studentCode;
           FirstName = firstName;
           LastName = lastName;
           DateOfBirth = dateOfBirth;
        }

        public string?  StudentCode { get; set; }
        public string?  FirstName { get; set; }
        public string?  LastName { get; set; }
        public DateTime?  DateOfBirth { get; set; }

        public override async Task<Student_DTO> ExecuteAsync(DataSeeder dataSeeder)
        {
            if (string.IsNullOrWhiteSpace(StudentCode)) StudentCode = _randomStringFactory.RandomStringLettersOrNumbers(StudentCodeConstants.ExpectedLength);

            if (string.IsNullOrWhiteSpace(FirstName)) FirstName = _randomStringFactory.RandomStringLettersOrNumbers();

            if (string.IsNullOrWhiteSpace(LastName)) LastName = _randomStringFactory.RandomStringLettersOrNumbers();

            if (!DateOfBirth.HasValue) DateOfBirth = DateTime.Today.AddYears(-20);

            return await dataSeeder.SeedFetchAndQueueForDeletionAsync(
                new InsertStudent(StudentCode, FirstName, LastName, DateOfBirth.Value),
                new GetStudentByCode(StudentCode),
                new DeleteStudent(StudentCode)
            );
        }
    }
}
