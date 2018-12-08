using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace RefitVsRestSharp.Server.Controllers
{
    [Route("api/resources")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly ILogger _logger;

        public ResourceController(ILogger<ResourceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> GetResources()
        {
            var result = string.Empty;
            if (Request.Headers.TryGetValue("x-test", out StringValues values))
            {
                string value = values.FirstOrDefault();
                result = $"x-test:{value}";
            }

            _logger.LogInformation($"HEADER {result}");
            return result;
        }
        
        
    }
}