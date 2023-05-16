using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Data.Tests.TestHelpers
{
    public class RequestFactory
    {
        private readonly Guid _testGuid;

        private readonly string _testString;

        public RequestFactory(Guid testGuid)
        {
            _testGuid = testGuid;

            _testString = _testGuid.ToString();
        }

        #region Student Requests

        /// <summary>
        /// Return a new InsertStudent DataRequestObject. Parameters not provided will be given a default value using DateTime.Now or _testString. 
        /// StudentCode will use _testString[..StudentCodeConstants.ExpectedLength] when no value is provided
        /// </summary>
        public InsertStudent InsertStudent(string? studentCode = null, string? firstName = null, string? lastName = null, DateTime? dateOfBirth = null) =>
            new (new Student(studentCode ?? _testString[..StudentCodeConstants.ExpectedLength], firstName ?? _testString, lastName ?? _testString, dateOfBirth ?? DateTime.Today ));

        /// <summary>
        /// Return a new DeleteStudent DataRequestObject. If studentCode is not provided, will use _testString[..StudentCodeConstants.ExpectedLength]
        /// </summary>
        public DeleteStudent DeleteStudent(string? studentCode = null) => new(studentCode ?? _testString[..StudentCodeConstants.ExpectedLength]);

        /// <summary>
        /// Return a new GetStudentByCode DataRequestObject. If studentCode is not provided, will use _testString[..StudentCodeConstants.ExpectedLength]
        /// </summary>
        public GetStudentByCode GetStudentByCode(string? studentCode = null) => new(studentCode ?? _testString[..StudentCodeConstants.ExpectedLength]);

        /// <summary>
        /// Return a new GetStudentsByName DataRequestObject. If parameters are not provided, they will remain null.
        /// </summary>
        public GetStudentsByName GetStudentsByName(string? firstName = null, string? lastName = null) => new(firstName, lastName);

        /// <summary>
        /// Return a new IsStudentCodeTaken DataRequestObject. If studentCode is not provided, will use _testString[..StudentCodeConstants.ExpectedLength]
        /// </summary>
        public IsStudentCodeTaken IsStudentCodeTaken(string? studentCode = null) => new(studentCode ?? _testString[..StudentCodeConstants.ExpectedLength]);

        /// <summary>
        /// Return a new IsStudentCodeTaken DataRequestObject. If studentCode is not provided, will use _testString[..StudentCodeConstants.ExpectedLength] 
        /// Other parameters will remain null if no value is provided.
        /// </summary>
        public UpdateStudent UpdateStudent(string? studentCode = null, string? firstName = null, string? lastName = null, DateTime? dateOfBirth = null) => 
            new(studentCode ?? _testString[..StudentCodeConstants.ExpectedLength], firstName, lastName, dateOfBirth);

        #endregion

    }
}
