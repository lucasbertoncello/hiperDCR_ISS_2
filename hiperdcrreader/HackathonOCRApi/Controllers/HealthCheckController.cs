using Microsoft.AspNetCore.Mvc;

namespace HackathonOCRApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Ok";
        }
    }
}
