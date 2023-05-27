using AttendanceTracker.Data.Abstraction.Interfaces;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;
using AttendanceTracker.Data.DataTransferObjects;

namespace AttendanceTracker.Data.Tests.TestHelpers
{
    public class DataSeeder 
    {
        public DataSeeder(IDataAccess dataAccess) => _dataAccess = dataAccess;

        private readonly IDataAccess _dataAccess;
        
        private readonly List<IDataRequest> _deleteSeededRecordRequests = new();

        public async Task PurgeSeededRecords()
        {
            // Loop backwards through requests deleting seeded records. Backwards to try to avoid Foreign Key Conflicts
            for (int i = _deleteSeededRecordRequests.Count - 1; i >= 0; i--)
            {
                try
                {
                    // attempt to delete record, remove from list if successful
                    await _dataAccess.ExecuteAsync(_deleteSeededRecordRequests[i]);
                    _deleteSeededRecordRequests.RemoveAt(i);
                }
                catch (Exception) { /* TODO: Log Purge Failures ? */ }
            }
        }

        public async Task<CourseScheduled_DTO> NewStudentCourseScheduled(string studentCode, Guid courseScheduledGuid) =>
            await SeedFetchAndQueueForDeletion(
                new InsertStudentCourseScheduled(studentCode, courseScheduledGuid), new GetStudentCourseScheduled(studentCode, courseScheduledGuid), new DeleteStudentCourseScheduled(studentCode, courseScheduledGuid));

        public async Task<Instructor_DTO> NewInstructorAsync(string instructorCode, string firstName, string lastName) =>
            await SeedFetchAndQueueForDeletion(
                new InsertInstructor(instructorCode, firstName, lastName), new GetInstructorByCode(instructorCode), new DeleteInstructor(instructorCode));

        public async Task<Student_DTO> NewStudentAsync(string studentCode, string firstName, string lastName, DateTime dateOfBirth) =>
            await SeedFetchAndQueueForDeletion(
                new InsertStudent(studentCode, firstName, lastName, dateOfBirth), new GetStudentByCode(studentCode), new DeleteStudent(studentCode));

        public async Task<Subject_DTO> NewSubjectAsync(string subjectCode, string name) =>
            await SeedFetchAndQueueForDeletion( 
                new InsertSubject(subjectCode, name), new GetSubjectByCode(subjectCode), new DeleteSubject(subjectCode));

        public async Task<Course_DTO> NewCourseAsync(string courseCode,string subjectCode, string name) => 
             await SeedFetchAndQueueForDeletion(
                 new InsertCourse(courseCode, subjectCode, name), new GetCourseByCourseCode(courseCode), new DeleteCourse(courseCode));

        public async Task<CourseScheduled_DTO> NewCourseScheduledAsync(Guid guid, string courseCode, string instructorCode, DateTime startDate, DateTime endDate) =>
             await SeedFetchAndQueueForDeletion(
                 new InsertCourseScheduled(guid, courseCode, instructorCode, startDate, endDate), new GetCourseScheduledByGuid(guid), new DeleteCourseScheduled(guid));

        private async Task<TResponse> SeedFetchAndQueueForDeletion<TResponse>(IDataRequest insertRequest, IDataRequest<TResponse> fetchRequest, IDataRequest deleteRequest)
        {
            await _dataAccess.ExecuteAsync(insertRequest);

            _deleteSeededRecordRequests.Add(deleteRequest);

            return await _dataAccess.FetchAsync(fetchRequest);
        }
    }
}
