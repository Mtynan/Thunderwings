using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(Response<T> result)
        {
            if(result.IsSuccess && result.Data is not null) 
                return Ok(result.Data);
            if (result.IsSuccess && result.Data is null)
                return NoContent();
            return BadRequest();
        }
    }
}
