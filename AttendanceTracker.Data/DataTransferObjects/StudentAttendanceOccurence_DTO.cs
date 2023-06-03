namespace AttendanceTracker.Data.DataTransferObjects
{
    public class StudentAttendanceOccurence_DTO
    {
        public int Id { get; set; }

        public int StudentCourseScheduledId { get; set; }

        public int AttendanceOccurenceId { get; set; }

        public Guid Guid { get; set; }

        public DateTime DateOfOccurence { get; set; }

        public string Notes { get; set; } = string.Empty;

        public bool IsExcused { get; set; }

        public AttendanceOccurence AsAttendanceOccurence() =>
            new(Guid, DateOfOccurence, Notes, IsExcused);

        public StudentAttendanceOccurence AsStudentAttendanceOccurence(Student_DTO student, StudentCourseScheduled_DTO studentCourseScheduled, Course_DTO course, Instructor_DTO instructor) =>
            new(student.AsStudent(),
                studentCourseScheduled.AsCourseScheduled(course, instructor),
                this.AsAttendanceOccurence()
            );
    }
}
