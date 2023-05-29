namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentControllerTests
{
    public class GetStudentsByNameTests : BaseStudentControllerTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", " ")]
        [InlineData("", null)]
        [InlineData(" ", "")]
        [InlineData(" ", " ")]
        [InlineData(" ", null)]
        [InlineData(null, "")]
        [InlineData(null, " ")]
        [InlineData(null, null)]
        public async Task GetStudentsByName_Given_FirstNameAndLastNameNotProvided_ShouldThrow_ValidationFailedException(string firstName, string lastName)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetStudentsByName(new(firstName, lastName)));
        }

        [Fact]
        public async Task GetStudentsByName_Given_FirstName_Should_ReturnStudents_WithNameLikeFirstName()
        {
            var nameToSearchWith = "Jo";

            var studentsToExpect = new[]
            {
                new SeedStudentRequest(firstName: "John"),          new SeedStudentRequest(firstName: "Joe"),
                new SeedStudentRequest(firstName: "Josephine"),     new SeedStudentRequest(firstName: "Joanna")
            };

            var studentsToNotExpect = new[]
            {
                new SeedStudentRequest(firstName: "Mario"),         new SeedStudentRequest(firstName: "Luigi"),
                new SeedStudentRequest(firstName: "Peach"),         new SeedStudentRequest(firstName: "Daisy"),
            };

            foreach (var student in studentsToExpect.Concat(studentsToNotExpect))
            {
                await SeedAsync(student);
            }

            var results = await _controller.GetStudentsByName(new(firstName: nameToSearchWith));

            Assert.Multiple(() =>
            {
                Assert.True(results.All(_ => _.FirstName.Contains(nameToSearchWith)));

                Assert.True(studentsToExpect.Select(_ => _.StudentCode)
                                            .All(c => results.Select(_ => _.StudentCode)
                                            .Contains(c)));

                Assert.True(studentsToNotExpect.Select(_ => _.StudentCode)
                                               .All(c => results.Select(_ => _.StudentCode)
                                               .Contains(c) == false));
            });
        }
        
        [Fact]
        public async Task GetStudentsByName_Given_LastName_Should_ReturnStudents_WithNameLikeLastName()
        {
            var nameToSearchWith = "son";

            var studentsToExpect = new[]
            {
                new SeedStudentRequest(lastName: "Anderson"),   new SeedStudentRequest(lastName: "Johnson"),
                new SeedStudentRequest(lastName: "Jackson"),    new SeedStudentRequest(lastName: "Thompson")
            };

            var studentsToNotExpect = new[]
            {
                new SeedStudentRequest(lastName: "Smith"),      new SeedStudentRequest(lastName: "Parker"),
                new SeedStudentRequest(lastName: "Carter"),     new SeedStudentRequest(lastName: "Allen"),
            };

            foreach (var student in studentsToExpect.Concat(studentsToNotExpect))
            {
                await SeedAsync(student);
            }

            var results = await _controller.GetStudentsByName(new(lastName: nameToSearchWith));

            Assert.Multiple(() =>
            {
                Assert.True(results.All(_ => _.LastName.Contains(nameToSearchWith)));

                Assert.True(studentsToExpect.Select(_ => _.StudentCode)
                                            .All(c => results.Select(_ => _.StudentCode)
                                            .Contains(c)));

                Assert.True(studentsToNotExpect.Select(_ => _.StudentCode)
                                                .All(c => results.Select(_ => _.StudentCode)
                                                .Contains(c) == false));
            });
        }

        [Fact]
        public async Task GetStudentsByName_Given_FirstAndLastName_Should_ReturnStudents_WithNameLikeFirstAndLastName()
        {
            var firstNameToSearchWith = "Jo";
            var lastNameToSearchWith = "son";

            var studentsToExpect = new[]
            {
                new SeedStudentRequest(firstName: "John", lastName: "Anderson"),   new SeedStudentRequest(firstName: "Joe", lastName: "Johnson"),
                new SeedStudentRequest(firstName: "Josephine", lastName: "Jackson"),    new SeedStudentRequest(firstName: "Joanna", lastName: "Thompson")
            };

            var studentsToNotExpect = new[]
            {
                new SeedStudentRequest(firstName: "John", lastName: "Smith"),      new SeedStudentRequest(firstName: "Sarah", lastName: "Johnson"),
                new SeedStudentRequest(firstName: "Josephine", lastName: "Carter"),     new SeedStudentRequest(firstName: "Daisy", lastName: "Thompson"),
            };

            foreach (var student in studentsToExpect.Concat(studentsToNotExpect))
            {
                await SeedAsync(student);
            }

            var results = await _controller.GetStudentsByName(new(firstNameToSearchWith, lastNameToSearchWith));

            Assert.Multiple(() =>
            {
                Assert.True(results.All(_ => _.FirstName.Contains(firstNameToSearchWith)));

                Assert.True(results.All(_ => _.LastName.Contains(lastNameToSearchWith)));

                Assert.True(studentsToExpect.Select(_ => _.StudentCode)
                                            .All(c => results.Select(_ => _.StudentCode)
                                            .Contains(c)));

                Assert.True(studentsToNotExpect.Select(_ => _.StudentCode)
                                                .All(c => results.Select(_ => _.StudentCode)
                                                .Contains(c) == false));
            });
        }
    }
}
