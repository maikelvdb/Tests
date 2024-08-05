using Microsoft.AspNetCore.Mvc;
using Test.Api.Attributes;
using Test.Api.Middleware;
using Test.Api.ParameterBinding.FromItem;

namespace Test.Api.Controllers
{
    [VersionRoute("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("CorrelationId")]
        public IActionResult GetCorrelationId([FromItem(SetCorrelationIdMiddleware.CORRELATION_ID)] Guid id)
        {
            return NotFound();
        }

        [HttpGet("VerionTest")]
        public IActionResult GetVersionTest([FromRoute(Name = "VersionNr")] int versionNr)
        {
            return Ok(versionNr);
        }
    }
}
