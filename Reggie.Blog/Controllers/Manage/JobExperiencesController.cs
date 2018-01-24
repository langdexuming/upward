using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reggie.Blog.Data;
using Reggie.Blog.Models;

namespace Reggie.Blog.Controllers.Manage
{
    public class JobExperiencesController : Controller
    {
        private readonly BlogContext _context;

        public JobExperiencesController(BlogContext context)
        {
            _context = context;
        }

        // GET: JobExperiences
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobExperiences.ToListAsync());
        }

        // GET: JobExperiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobExperience = await _context.JobExperiences
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobExperience == null)
            {
                return NotFound();
            }

            return View(jobExperience);
        }

        // GET: JobExperiences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobExperiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,Position,JobContent,StartDate,EndDate")] JobExperience jobExperience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobExperience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobExperience);
        }

        // GET: JobExperiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobExperience = await _context.JobExperiences.SingleOrDefaultAsync(m => m.Id == id);
            if (jobExperience == null)
            {
                return NotFound();
            }
            return View(jobExperience);
        }

        // POST: JobExperiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,Position,JobContent,StartDate,EndDate")] JobExperience jobExperience)
        {
            if (id != jobExperience.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobExperience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExperienceExists(jobExperience.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobExperience);
        }

        // GET: JobExperiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobExperience = await _context.JobExperiences
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobExperience == null)
            {
                return NotFound();
            }

            return View(jobExperience);
        }

        // POST: JobExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobExperience = await _context.JobExperiences.SingleOrDefaultAsync(m => m.Id == id);
            _context.JobExperiences.Remove(jobExperience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExperienceExists(int id)
        {
            return _context.JobExperiences.Any(e => e.Id == id);
        }
    }
}
