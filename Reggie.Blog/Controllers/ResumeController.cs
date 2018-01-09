using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reggie.Blog.Data;
using Reggie.Blog.ViewModels;

namespace Reggie.Blog.Controllers
{
    public class ResumeController : Controller
    {
        private readonly BlogContext _blogContext;
        private readonly ILogger _logger;

        public ResumeController(BlogContext context, ILogger<ResumeController> logger)
        {
            _blogContext = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _blogContext.Skills.ToListAsync();
            return View(new ResumeViewModel
            {
                Skills = list
            });
        }

        [HttpPost]
        [Route("CreateSkill")]
        public IActionResult CreateSkill()
        {
            return new ObjectResult("");
        }
    }
}