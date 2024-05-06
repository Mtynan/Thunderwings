using Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Thunderwings.UnitTests.Controllers
{
    public class BaseApiControllerTests
    {
        private readonly TestableBaseApiController _controller;

        public BaseApiControllerTests()
        {
            _controller = new TestableBaseApiController();
        }
        private class TestableBaseApiController : BaseApiController
        {
            public ActionResult TestHandleResult<T>(Response<T> result)
            {
                return HandleResult(result);
            }
        }

        [Fact]
        public void HandleResult_ReturnsOk_WhenIsSuccessTrueAndDataNotNull()
        {
            // arrange
            var response = new Response<string> { IsSuccess = true, Data = "Test Data" };

            // act
            var result = _controller.TestHandleResult(response);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Test Data", okResult.Value);
        }

        [Fact]
        public void HandleResult_ReturnsNoContent_WhenIsSuccessTrueAndDataIsNull()
        {
            // arrange
            var response = new Response<string> { IsSuccess = true, Data = null };

            // act
            var result = _controller.TestHandleResult(response);

            // assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
