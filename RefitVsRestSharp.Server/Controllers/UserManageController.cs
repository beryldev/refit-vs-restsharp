using Microsoft.AspNetCore.Mvc;
using RefitVsRestSharp.Server.Views;

namespace RefitVsRestSharp.Server.Controllers
{
    [Controller]
    public class UserManageController : Controller
    {
        [HttpPost("/users/new")]
        public JsonResult Create(User user)
        {
            JsonResult json = Json(user);
            return json;
        }
    }
}