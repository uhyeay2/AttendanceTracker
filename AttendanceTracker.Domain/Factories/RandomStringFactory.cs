using AttendanceTracker.Domain.Interfaces;

namespace AttendanceTracker.Domain.Factories
{
    public class RandomStringFactory : IRandomStringFactory
    {
        public static RandomStringFactory SharedInstance = new(new RandomCharacterFactory());

        private readonly IRandomCharacterFactory _randomCharacterFactory;

        public RandomStringFactory(IRandomCharacterFactory randomCharacterFactory) => _randomCharacterFactory = randomCharacterFactory;

        public string RandomStringLettersOnly(int length = 10) => GetRandomAtLength(length, _randomCharacterFactory.GetRandomLetter);

        public string RandomStringLettersOrNumbers(int length = 10) => GetRandomAtLength(length, _randomCharacterFactory.GetRandomLetterOrNumber);

        public string RandomStringNumbersOnly(int length = 10) => GetRandomAtLength(length, _randomCharacterFactory.GetRandomNumber);

        private static string GetRandomAtLength(int length, Func<char> characterGeneration)
        {
            var characters = new char[length];

            for (int i = 0; i < length; i++)
            {
                characters[i] = characterGeneration();
            }

            return new(characters);
        }
    }
}
