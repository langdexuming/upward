using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reggie.Blog.Data;

namespace Reggie.Blog.Controllers
{
    public class LeaveMessageController : Controller
    {
        private readonly BlogContext _blogContext;
        private readonly ILogger _logger;

        public LeaveMessageController(BlogContext context, ILogger<LeaveMessageController> logger)
        {
            _blogContext = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}