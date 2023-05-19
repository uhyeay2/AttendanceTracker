using AttendanceTracker.Domain.Interfaces;

namespace AttendanceTracker.Domain.Factories
{
    public class RandomCharacterFactory : IRandomCharacterFactory
    {
        private static readonly string _numbers, _letters, _lettersAndNumbers;

        static RandomCharacterFactory()
        {
            _numbers = "1234567890";
            _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            _lettersAndNumbers = _letters + _numbers;
        }

        public char GetRandomNumber() => _numbers.ElementAt(Random.Shared.Next(0, _numbers.Length));

        public char GetRandomLetter() => _letters.ElementAt(Random.Shared.Next(0, _letters.Length));

        public char GetRandomLetterOrNumber() => _lettersAndNumbers.ElementAt(Random.Shared.Next(0, _lettersAndNumbers.Length));
    }
}
