using AttendanceTracker.Domain.Enums;

namespace AttendanceTracker.Domain.Models
{
    public class AttendanceOccurence
    {
        public AttendanceOccurence() { }

        public AttendanceOccurence(Guid guid, DateTime dateOfOccurence, string notes, bool isExcused)
        {
            Guid = guid;
            DateOfOccurence = dateOfOccurence;
            Notes = notes;
            IsExcused = isExcused;
        }

        public Guid Guid { get; set; }

        public DateTime DateOfOccurence { get; set; }

        public string Notes { get; set; } = string.Empty;

        public bool IsExcused { get; set; }
    }
}
