using Microsoft.AspNetCore.Mvc;
using RefitCompare.Server.Views;

namespace RefitCompare.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("{username}")]
        public ActionResult<UserView> GetUser(string username)
        {
            return new UserView
            {
                Name = username,
                Description = "Sample user",
                Roles = new []{"First role", "Second role"}
            };
        }
    }
}