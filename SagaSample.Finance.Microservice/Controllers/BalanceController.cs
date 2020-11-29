using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SagaSample.Finance.Microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BalanceController : ControllerBase
    {
        private readonly ILogger<BalanceController> _logger;

        public BalanceController(ILogger<BalanceController> logger)
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
