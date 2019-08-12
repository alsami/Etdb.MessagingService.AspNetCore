using System.Linq;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;

namespace Etdb.MessagingService.Controllers.V1
{
    [ApiController]
    [Route("api/v1/dummy")]
    public class DummyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => this.Ok(new
        {
            Message =
                $"User authenticated {this.HttpContext.User.Claims.First(claim => claim.Type == JwtClaimTypes.Subject).Value}"
        });
    }
}