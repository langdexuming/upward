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
using Reggie.Blog.Constants;

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
            var taskList = new List<Task>();

            var querySkillTask = _blogContext.Skills.AsNoTracking().ToListAsync();
            taskList.Add(querySkillTask);

            var queryJobExperienceTask = _blogContext.JobExperiences.AsNoTracking().ToListAsync();
            taskList.Add(queryJobExperienceTask);

            var querySampleTask = _blogContext.Samples.AsNoTracking().ToListAsync();
            taskList.Add(querySampleTask);

            var queryContentFlagTask = _blogContext.ContentFlags.AsNoTracking().ToListAsync();
            taskList.Add(queryContentFlagTask);

            var querySwtichFlagTask = _blogContext.SwitchFlags.AsNoTracking().ToListAsync();
            taskList.Add(querySwtichFlagTask);

            await Task.WhenAll(taskList);

            var dictionary = new Dictionary<string, string>();
            if (queryContentFlagTask.Result?.Count > 0)
            {
                foreach (var contentFlag in queryContentFlagTask.Result)
                {
                    dictionary.Add(contentFlag.Name, contentFlag.Content);
                }
            }


            return View(new ResumeViewModel
            {
                Skills = querySkillTask.Result,
                JobExperiences = queryJobExperienceTask.Result,
                Samples = querySampleTask.Result,
                ContentFlags = queryContentFlagTask.Result,
                Motto = dictionary.ContainsKey(AppConstants.Motto) ? dictionary[AppConstants.Motto] : "",
                PersonalProfile = dictionary.ContainsKey(AppConstants.PersonalProfile) ? dictionary[AppConstants.PersonalProfile] : "",
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Manage()
        {
            return View();
        }

        public IActionResult CreateInformalEssay()
        {
            return View();
        }
    }
}
