using AttendanceTracker.Domain.Enums;

namespace AttendanceTracker.Domain.Models
{
    public class AttendanceOccurence
    {
        public AttendanceOccurenceType OccurenceType { get; set; }

        public DateTime DateOfOccurence { get; set; }

        public string Notes { get; set; } = string.Empty;

        public bool IsExcused { get; set; }
    }
}
