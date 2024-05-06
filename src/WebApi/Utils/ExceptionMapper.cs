using Domain.Exceptions;
using FluentValidation;

namespace WebApi.Utils
{
    public static class ExceptionMapper
    {
        public static (int StatusCode, string Title) Map(Exception ex)
        {
            return ex switch
            {
                EmptyBasketException e => (StatusCodes.Status409Conflict, e.Message),
                NotFoundException e => (StatusCodes.Status404NotFound, e.Message),
                ValidationException e => (StatusCodes.Status400BadRequest, e.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
            };
        }
    }
}
