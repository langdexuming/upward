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
using Reggie.Blog.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Reggie.Blog.Utils;

namespace Reggie.Blog.Controllers
{
    public class HomeController : Controller
    {
        private const string Tag = nameof(HomeController);
        private readonly BlogContext _blogContext;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(BlogContext context, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _blogContext = context;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["CanCreateInformalEssay"] = _signInManager.IsSignedIn(HttpContext.User);
            ViewData["EssayCategories"] = await _blogContext.EssayCategories.AsNoTracking().ToListAsync();

            return View(new List<InformalEssay>());
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

            var contentDictionary = new Dictionary<string, string>();
            if (queryContentFlagTask.Result?.Count > 0)
            {
                foreach (var contentFlag in queryContentFlagTask.Result)
                {
                    contentDictionary.Add(contentFlag.Name, contentFlag.Content);
                }
            }

            var switchDictionary = new Dictionary<string, bool>();
            if (querySwtichFlagTask.Result?.Count > 0)
            {
                foreach (var switchDFlag in querySwtichFlagTask.Result)
                {
                    switchDictionary.Add(switchDFlag.Name, switchDFlag.IsVaild);
                }
            }

            return View(new ResumeViewModel
            {
                Skills = querySkillTask.Result,
                JobExperiences = queryJobExperienceTask.Result,
                Samples = querySampleTask.Result,
                ContentFlags = queryContentFlagTask.Result,
                Motto = contentDictionary.ContainsKey(AppConstants.Motto) ? contentDictionary[AppConstants.Motto] : "",
                PersonalProfile = contentDictionary.ContainsKey(AppConstants.PersonalProfile) ? contentDictionary[AppConstants.PersonalProfile] : "",
                IsShowSkills = switchDictionary.ContainsKey(AppConstants.IsShowSkills) ? switchDictionary[AppConstants.IsShowSkills] : false,
                IsShowJobExperiences = switchDictionary.ContainsKey(AppConstants.IsShowJobExperiences) ? switchDictionary[AppConstants.IsShowJobExperiences] : false,
                IsShowSamples = switchDictionary.ContainsKey(AppConstants.IsShowSamples) ? switchDictionary[AppConstants.IsShowSamples] : false,
                IsShowEducationInformation = switchDictionary.ContainsKey(AppConstants.IsShowEducationInformation) ? switchDictionary[AppConstants.IsShowEducationInformation] : false,
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

        [Authorize]
        public async Task<IActionResult> CreateInformalEssay()
        {
            ViewData["EssayCategories"] = await _blogContext.EssayCategories.AsNoTracking().ToListAsync();
            ViewData["EssayCategoryId"] = new SelectList(_blogContext.EssayCategories, "EssayCategoryId", "Title");

            return View(new InformalEssay());
        }

        // POST: HomeController/CreateInformalEssay
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateInformalEssay([Bind("Id,EssayCategoryId,Title,Message,UserName,CreateDateTime")] InformalEssay informalEssay)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(informalEssay.UserName))
                {
                    informalEssay.UserName = _userManager.GetUserName(HttpContext.User);
                }

                if (informalEssay.CreateDateTime == default(DateTime))
                {
                    informalEssay.CreateDateTime = DateTime.Now;
                }

                _blogContext.Add(informalEssay);
                await _blogContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EssayCategories"] = await _blogContext.EssayCategories.AsNoTracking().ToListAsync();
            ViewData["EssayCategoryId"] = new SelectList(_blogContext.EssayCategories, "EssayCategoryId", "Title", informalEssay.EssayCategoryId);

            return View(informalEssay);
        }

        [HttpGet]
        public async Task<IActionResult> SearchInformalEssay(int lastId)
        {
            List<InformalEssay> informalEssayList;
            int loadInformalEssayCount = AppConstants.LoadInformalEssayCount;

            if (lastId == -1)
            {
                //首次获取
                informalEssayList = await _blogContext.InformalEssays.AsNoTracking().OrderByDescending(x => x.Id).Take(loadInformalEssayCount).ToListAsync();
            }
            else
            {
                informalEssayList = await _blogContext.InformalEssays.AsNoTracking().Where(x => x.Id < lastId).OrderByDescending(x => x.Id).Take(loadInformalEssayCount).ToListAsync();
            }

            informalEssayList.ForEach(x =>
            {
                x.Message = ContentProcessUtil.ConvertSummeryFrom(x.Message, AppConstants.EaasySummaryTextMaxLength);
            }
            );

            var result = new JsonResult(informalEssayList);
            return result;
        }

        public async Task<IActionResult> InformalEssayDetail(int id)
        {
            var informalEssay = await _blogContext.InformalEssays.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (informalEssay == null)
            {
                return NotFound();
            }

            ViewData["EssayCategories"] = await _blogContext.EssayCategories.AsNoTracking().ToListAsync();

            return View(informalEssay);
        }

    }
}
