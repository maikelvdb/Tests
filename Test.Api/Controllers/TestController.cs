using Microsoft.AspNetCore.Mvc;
using Test.Api.Middleware;
using Test.Api.ParameterBinding.FromItem;

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("CorrelationId")]
        public IActionResult GetCorrelationId([FromItem(SetCorrelationIdMiddleware.CORRELATION_ID)] Guid id)
        {
            return NotFound();
        }
    }
}
