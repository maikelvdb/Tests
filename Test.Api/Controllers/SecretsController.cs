using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Test.Api.Configuration.Models;

namespace Test.Api.Controllers;

[Route("api/[controller]")]
public class SecretsController(IOptions<SecretsConfig> secrets) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(secrets);
    }
}
