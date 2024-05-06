using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Thunderwings.UnitTests.Controllers
{
    public class ErrorsControllerTests
    {
        private readonly Mock<IExceptionHandlerFeature> _mockFeature;
        private readonly ErrorsController _errorsController;
        private readonly Mock<ILogger<ErrorsController>> _logger;

        public ErrorsControllerTests()
        {
            _mockFeature = new Mock<IExceptionHandlerFeature>();
            _logger = new Mock<ILogger<ErrorsController>>();
            _errorsController = new ErrorsController(_logger.Object);
            _errorsController.ControllerContext.HttpContext = new DefaultHttpContext();
            _errorsController.ControllerContext.HttpContext.Features.Set(_mockFeature.Object);
        }

        [Fact]
        public void Index_Returns400_WhenValidationException()
        {
            // arrange    
            _mockFeature.Setup(m => m.Error).Returns(new ValidationException("error"));

            // act
            var result = Assert.IsType<ObjectResult>(_errorsController.Index());
            var problemDetails = Assert.IsType<ProblemDetails>(result.Value);

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Contains("error", problemDetails.Title);
        }

        [Fact]
        public void Index_Returns404_NotFoundException()
        {
            // arrange
            _mockFeature.Setup(m => m.Error).Returns(new BasketNotFoundException(1));

            // act
            var result = Assert.IsType<ObjectResult>(_errorsController.Index());
            var problemDetails = Assert.IsType<ProblemDetails>(result.Value);

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Contains("Basket not found for user with id 1", problemDetails.Title);
        }

        [Fact]
        public void Index_Returns409_EmptyBasketException()
        {
            // arrange
            _mockFeature.Setup(m => m.Error).Returns(new EmptyBasketException(1));

            // act
            var result = Assert.IsType<ObjectResult>(_errorsController.Index());
            var problemDetails = Assert.IsType<ProblemDetails>(result.Value);

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
            Assert.Contains("The basket with the Id 1 is empty", problemDetails.Title);
        }

    }
}
