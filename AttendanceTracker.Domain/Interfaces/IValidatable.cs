namespace AttendanceTracker.Domain.Interfaces
{
    public interface IValidatable
    {
        public bool IsValid(out List<string> validationFailures);
    }
}
