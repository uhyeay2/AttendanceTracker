﻿namespace AttendanceTracker.Data.DataTransferObjects
{
    public class Instructor_DTO
    {
        public int Id { get; set; }

        public string InstructorCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public Instructor AsInstructor() => new(InstructorCode, FirstName, LastName);
    }
}
