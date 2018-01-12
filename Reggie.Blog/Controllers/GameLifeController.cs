using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reggie.Blog.Data;

namespace Reggie.Blog.Controllers
{
    public class GameLifeController : Controller
    {
        private readonly BlogContext _blogContext;
        private readonly ILogger _logger;

        public GameLifeController(BlogContext context, ILogger<GameLifeController> logger)
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