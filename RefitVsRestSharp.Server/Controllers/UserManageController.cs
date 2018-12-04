using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RefitVsRestSharp.Server.Views;

namespace RefitVsRestSharp.Server.Controllers
{
    [Controller]
    public class FileController : Controller
    {
        private readonly ILogger _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost("/files/upload")]
        public string Upload(IFormFile file)
        {
            var stream = new MemoryStream();
            file.CopyTo(stream);

            string fileContent = Encoding.UTF8.GetString(stream.ToArray());

            _logger.LogInformation($"File content: {fileContent}");
            
            return fileContent;
        }
    }
    
    
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