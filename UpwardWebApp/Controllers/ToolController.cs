using Microsoft.AspNetCore.Mvc;

namespace UpwardWebApp.Views.Tool
{
    [Route("[controller]")]
    public class ToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}