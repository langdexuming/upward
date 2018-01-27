using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reggie.Blog.Data;
using Reggie.Blog.Models;
using Microsoft.AspNetCore.Authorization;

namespace Reggie.Blog.Controllers.Manage
{
    [Authorize]
    public class EssayCategoriesController : Controller
    {
        private readonly BlogContext _context;

        public EssayCategoriesController(BlogContext context)
        {
            _context = context;
        }

        // GET: EssayCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.EssayCategories.ToListAsync());
        }

        // GET: EssayCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var essayCategory = await _context.EssayCategories
                .SingleOrDefaultAsync(m => m.EssayCategoryId == id);
            if (essayCategory == null)
            {
                return NotFound();
            }

            return View(essayCategory);
        }

        // GET: EssayCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EssayCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EssayCategoryId,Title,Description,Remark")] EssayCategory essayCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(essayCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(essayCategory);
        }

        // GET: EssayCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var essayCategory = await _context.EssayCategories.SingleOrDefaultAsync(m => m.EssayCategoryId == id);
            if (essayCategory == null)
            {
                return NotFound();
            }
            return View(essayCategory);
        }

        // POST: EssayCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EssayCategoryId,Title,Description,Remark")] EssayCategory essayCategory)
        {
            if (id != essayCategory.EssayCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(essayCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EssayCategoryExists(essayCategory.EssayCategoryId))
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
            return View(essayCategory);
        }

        // GET: EssayCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var essayCategory = await _context.EssayCategories
                .SingleOrDefaultAsync(m => m.EssayCategoryId == id);
            if (essayCategory == null)
            {
                return NotFound();
            }

            return View(essayCategory);
        }

        // POST: EssayCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var essayCategory = await _context.EssayCategories.SingleOrDefaultAsync(m => m.EssayCategoryId == id);
            _context.EssayCategories.Remove(essayCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EssayCategoryExists(int id)
        {
            return _context.EssayCategories.Any(e => e.EssayCategoryId == id);
        }
    }
}
