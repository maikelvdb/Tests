using Microsoft.AspNetCore.Mvc;

namespace Test.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusCodeController : ControllerBase
    {

        [HttpGet("{StatusCode:int}")]
        public IActionResult StatusCode([FromRoute(Name = "StatusCode")] int statusCode)
        {
            var httpStatusCode = (System.Net.HttpStatusCode)statusCode;

            return StatusCode(statusCode, httpStatusCode.ToString());
        }
    }
}
