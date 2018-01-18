using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reggie.Blog.Data;
using Reggie.Blog.Models;
using Reggie.Blog.ViewModels;
using Reggie.Blog.Constants;

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

        [HttpPost]
        public async Task<IActionResult> UpdateContentFlag(ContentFlag contentFlag)
        {
            if (contentFlag == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogContext.ContentFlags.Update(contentFlag);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                return BadRequest();
            }

            return new ObjectResult(contentFlag);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSwitchFlag(SwitchFlag switchFlag)
        {
            if (switchFlag == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogContext.SwitchFlags.Update(switchFlag);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                return BadRequest();
            }

            return new ObjectResult(switchFlag);
        }


        public async Task<IActionResult> GetSkills()
        {
            var list = await _blogContext.Skills.ToListAsync();
            return new ObjectResult(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill(Skill skill)
        {
            if (skill == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var findItem = await _blogContext.Skills.FirstOrDefaultAsync(x => x.Name == skill.Name);
                if (findItem != null)
                {
                    return BadRequest();
                }

                if (skill.CreateDateTime == default(DateTime))
                {
                    skill.CreateDateTime = DateTime.Now;
                }

                try
                {
                    _blogContext.Skills.Add(skill);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return new ObjectResult(skill);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSkill(Skill skill)
        {
            if (skill == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (skill.CreateDateTime == default(DateTime))
                {
                    skill.CreateDateTime = DateTime.Now;
                }

                skill.LastUpdateDateTime = DateTime.Now;

                try
                {
                    _blogContext.Skills.Update(skill);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return new ObjectResult(skill);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSkill(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var skill = await _blogContext.Skills
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NoContent();
            }

            try
            {
                _blogContext.Skills.Remove(skill);
                await _blogContext.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                return BadRequest();
            }

            return new ObjectResult(skill);
        }

        [HttpGet]
        public async Task<IActionResult> GetJobExperience(int id)
        {
            var jobExperience = await _blogContext.JobExperiences.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

            if (jobExperience == null)
            {
                return NotFound();
            }

            return new ObjectResult(jobExperience);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobExperience(JobExperience jobExperience)
        {
            if (jobExperience == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogContext.JobExperiences.Add(jobExperience);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return new ObjectResult(jobExperience);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateJobExperience(JobExperience jobExperience)
        {
            if (jobExperience == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogContext.JobExperiences.Update(jobExperience);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return new ObjectResult(jobExperience);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteJobExperience(int? id)
        {
            if (id == null)
            {

                return BadRequest();
            }
            var jobExperience = await _blogContext.JobExperiences
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobExperience == null)
            {
                return NoContent();
            }

            try
            {
                _blogContext.JobExperiences.Remove(jobExperience);
                await _blogContext.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                return BadRequest();
            }

            return new ObjectResult(jobExperience);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSample(Sample sample)
        {
            if (sample == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogContext.Samples.Add(sample);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return new ObjectResult(sample);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSample(Sample sample)
        {
            if (sample == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogContext.Samples.Update(sample);
                    await _blogContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return new ObjectResult(sample);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSample(int? id)
        {
            if (id == null)
            {

                return BadRequest();
            }
            var sample = await _blogContext.Samples
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sample == null)
            {
                return NoContent();
            }

            try
            {
                _blogContext.Samples.Remove(sample);
                await _blogContext.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                return BadRequest();
            }

            return new ObjectResult(sample);
        }
    }
}