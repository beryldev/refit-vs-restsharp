using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RefitVsRestSharp.Server.Views;

namespace RefitVsRestSharp.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private static IEnumerable<User> Users => new[]
        {
            new User
            {
                Name = "username",
                Description = "Sample user",
                Roles = new[] {"RoleA", "RoleB"}
            }
        };

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<User>> Search(string role = "")
        {
            return string.IsNullOrEmpty(role) ? Users.ToList()
                : Users.Where(u => u.Roles.Contains(role)).ToList();
        }
        
        [HttpGet("{username}")]
        public ActionResult<User> Get(string username)
        {
            return new User
            {
                Name = username,
                Description = "Sample user",
                Roles = new []{"RoleA", "RoleB"}
            };
        }

        [HttpPost]
        public async Task<string> Create()
        {
            using var sr = new StreamReader(Request.Body);
            string body = await sr.ReadToEndAsync();
            _logger.LogInformation($"Request body: {body}");
            return body;
        }
    }
}