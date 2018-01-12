using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reggie.Blog.Data;
using Reggie.Blog.Models;
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
            return View(list);
        }

        [HttpPost]
        [Route("CreateSkill")]
        public async Task<IActionResult> CreateSkill(Skill skill)
        {
            if (skill == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogContext.Skills.Add(skill);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }

            return View(skill);
        }

        [HttpPut]
        [Route("UpdateSkill")]
        public async Task<IActionResult> UpdateSkill(Skill skill)
        {
            if (skill == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogContext.Skills.Add(skill);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }

            return View(skill);
        }

        [HttpDelete]
        [Route("DeleteSkill")]
        public async Task<IActionResult> DeleteSkill(int? id)
        {

            var item = await _blogContext.Skills.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

            return View(item);
        }
    }
}