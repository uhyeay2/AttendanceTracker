namespace AttendanceTracker.Domain.Interfaces
{
    public interface IRandomStringFactory
    {
        public string RandomStringNumbersOnly(int length = 10);

        public string RandomStringLettersOnly(int length = 10);

        public string RandomStringLettersOrNumbers(int length = 10);
    }
}
