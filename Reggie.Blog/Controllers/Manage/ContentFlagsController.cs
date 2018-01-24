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
    public class ContentFlagsController : Controller
    {
        private readonly BlogContext _context;

        public ContentFlagsController(BlogContext context)
        {
            _context = context;
        }

        // GET: ContentFlags
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContentFlags.ToListAsync());
        }

        // GET: ContentFlags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentFlag = await _context.ContentFlags
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contentFlag == null)
            {
                return NotFound();
            }

            return View(contentFlag);
        }

        // GET: ContentFlags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContentFlags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Content,Remark")] ContentFlag contentFlag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentFlag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contentFlag);
        }

        // GET: ContentFlags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentFlag = await _context.ContentFlags.SingleOrDefaultAsync(m => m.Id == id);
            if (contentFlag == null)
            {
                return NotFound();
            }
            return View(contentFlag);
        }

        // POST: ContentFlags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Content,Remark")] ContentFlag contentFlag)
        {
            if (id != contentFlag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentFlag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentFlagExists(contentFlag.Id))
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
            return View(contentFlag);
        }

        // GET: ContentFlags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentFlag = await _context.ContentFlags
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contentFlag == null)
            {
                return NotFound();
            }

            return View(contentFlag);
        }

        // POST: ContentFlags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentFlag = await _context.ContentFlags.SingleOrDefaultAsync(m => m.Id == id);
            _context.ContentFlags.Remove(contentFlag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentFlagExists(int id)
        {
            return _context.ContentFlags.Any(e => e.Id == id);
        }
    }
}
