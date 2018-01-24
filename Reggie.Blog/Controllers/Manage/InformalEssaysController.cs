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
    public class InformalEssaysController : Controller
    {
        private readonly BlogContext _context;

        public InformalEssaysController(BlogContext context)
        {
            _context = context;
        }

        // GET: InformalEssays
        public async Task<IActionResult> Index()
        {
            var blogContext = _context.InformalEssays.Include(i => i.EssayCategoryItem);
            return View(await blogContext.ToListAsync());
        }

        // GET: InformalEssays/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informalEssay = await _context.InformalEssays
                .Include(i => i.EssayCategoryItem)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (informalEssay == null)
            {
                return NotFound();
            }

            return View(informalEssay);
        }

        // GET: InformalEssays/Create
        public IActionResult Create()
        {
            ViewData["EssayCategoryId"] = new SelectList(_context.EssayCategories, "EssayCategoryId", "Title");
            return View();
        }

        // POST: InformalEssays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EssayCategoryId,Title,Message,UserName,CreateDateTime")] InformalEssay informalEssay)
        {
            if (ModelState.IsValid)
            {
                if (informalEssay.CreateDateTime == default(DateTime))
                {
                    informalEssay.CreateDateTime = DateTime.Now;
                }

                _context.Add(informalEssay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EssayCategoryId"] = new SelectList(_context.EssayCategories, "EssayCategoryId", "Title", informalEssay.EssayCategoryId);
            return View(informalEssay);
        }

        // GET: InformalEssays/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informalEssay = await _context.InformalEssays.SingleOrDefaultAsync(m => m.Id == id);
            if (informalEssay == null)
            {
                return NotFound();
            }
            ViewData["EssayCategoryId"] = new SelectList(_context.EssayCategories, "EssayCategoryId", "Title", informalEssay.EssayCategoryId);
            return View(informalEssay);
        }

        // POST: InformalEssays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,EssayCategoryId,Title,Message,UserName,CreateDateTime")] InformalEssay informalEssay)
        {
            if (id != informalEssay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informalEssay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformalEssayExists(informalEssay.Id))
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
            ViewData["EssayCategoryId"] = new SelectList(_context.EssayCategories, "EssayCategoryId", "Title", informalEssay.EssayCategoryId);
            return View(informalEssay);
        }

        // GET: InformalEssays/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informalEssay = await _context.InformalEssays
                .Include(i => i.EssayCategoryItem)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (informalEssay == null)
            {
                return NotFound();
            }

            return View(informalEssay);
        }

        // POST: InformalEssays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var informalEssay = await _context.InformalEssays.SingleOrDefaultAsync(m => m.Id == id);
            _context.InformalEssays.Remove(informalEssay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformalEssayExists(long id)
        {
            return _context.InformalEssays.Any(e => e.Id == id);
        }
    }
}
