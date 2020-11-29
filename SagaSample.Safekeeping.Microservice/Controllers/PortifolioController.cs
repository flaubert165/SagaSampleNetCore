using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SagaSample.Safekeeping.Microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortifolioController : ControllerBase
    {
        private readonly ILogger<PortifolioController> _logger;

        public PortifolioController(ILogger<PortifolioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
