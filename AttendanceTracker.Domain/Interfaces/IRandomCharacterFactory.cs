namespace AttendanceTracker.Domain.Interfaces
{
    public interface IRandomCharacterFactory
    {
        public char GetRandomLetter();

        public char GetRandomNumber();

        public char GetRandomLetterOrNumber();
    }
}
