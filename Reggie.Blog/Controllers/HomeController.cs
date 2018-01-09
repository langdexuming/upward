using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reggie.Blog.Data;
using Reggie.Blog.Extensions;
using Reggie.Blog.ViewModels;

namespace Reggie.Blog.Controllers
{
    public class HomeController : Controller
    {
        private const string Tag = nameof(HomeController);
        private readonly BlogContext _blogContext;
        private readonly ILogger _logger;

        private const string essaySessionKeyName = "CanCreateInformalEssay";

        public HomeController(BlogContext context, ILogger<HomeController> logger)
        {
            _blogContext = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var canCreateInformalEssay = HttpContext.Session.Get<bool>(essaySessionKeyName);

            return View(new IndexViewModel()
            {
                CanCreateInformalEssay = canCreateInformalEssay
            });
        }

        public IActionResult GameLife()
        {
            return View();
        }

        public IActionResult LeaveMessage()
        {
            return View();
        }

        public async Task<IActionResult> Resume()
        {
            var list = await _blogContext.Skills.ToListAsync();
            return View(new ResumeViewModel
            {
                Skills = list
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
