using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        private readonly ILogger<ErrorsController> _logger;
        public ErrorsController(ILogger<ErrorsController> logger)
        {
            _logger = logger;
        }
        [Route("/error")]
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (feature != null)
            {
                _logger.LogError(feature.Error, feature.Error.Message);
                var (statusCode, title) = ExceptionMapper.Map(feature.Error);
                return Problem(statusCode: statusCode, title: title);
            }
            return Problem();
        }
    }
}
