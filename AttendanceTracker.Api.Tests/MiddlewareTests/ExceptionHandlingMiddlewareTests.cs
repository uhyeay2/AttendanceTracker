using AttendanceTracker.Api.Middleware;
using Microsoft.AspNetCore.Http;
using Moq;

namespace AttendanceTracker.Api.Tests.MiddlewareTests
{
    public class ExceptionHandlingMiddlewareTests
    {
        private readonly ExceptionHandlingMiddleware _middleware = new();

        public static IEnumerable<object[]> ExceptionTypesAndExpectedStatusCodes = new[]
        {
            new object[] {new ValidationFailedException(), StatusCodes.Status400BadRequest},
            new object[] {new DoesNotExistException(), StatusCodes.Status404NotFound},
            new object[] {new AlreadyExistsException(), StatusCodes.Status409Conflict},
            new object[] {new Exception(), StatusCodes.Status500InternalServerError}
        };

        [Theory]
        [MemberData(nameof(ExceptionTypesAndExpectedStatusCodes))]
        public async Task InvokeAsync_GivenExceptionThrown_ShouldSet_ExpectedResponseStatusCode(Exception exception, int expectedStatusCode)
        {
            var context = new DefaultHttpContext();

            await _middleware.InvokeAsync(context, (HttpContext _) => throw exception);

            var statusCode = context.Response.StatusCode;

            Assert.Equal(expectedStatusCode, statusCode);
        }
    }
}
