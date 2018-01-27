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
    public class SwitchFlagsController : Controller
    {
        private readonly BlogContext _context;

        public SwitchFlagsController(BlogContext context)
        {
            _context = context;
        }

        // GET: SwitchFlags
        public async Task<IActionResult> Index()
        {
            return View(await _context.SwitchFlags.ToListAsync());
        }

        // GET: SwitchFlags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var switchFlag = await _context.SwitchFlags
                .SingleOrDefaultAsync(m => m.Id == id);
            if (switchFlag == null)
            {
                return NotFound();
            }

            return View(switchFlag);
        }

        // GET: SwitchFlags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SwitchFlags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsVaild,Remark")] SwitchFlag switchFlag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(switchFlag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(switchFlag);
        }

        // GET: SwitchFlags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var switchFlag = await _context.SwitchFlags.SingleOrDefaultAsync(m => m.Id == id);
            if (switchFlag == null)
            {
                return NotFound();
            }
            return View(switchFlag);
        }

        // POST: SwitchFlags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsVaild,Remark")] SwitchFlag switchFlag)
        {
            if (id != switchFlag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(switchFlag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwitchFlagExists(switchFlag.Id))
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
            return View(switchFlag);
        }

        // GET: SwitchFlags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var switchFlag = await _context.SwitchFlags
                .SingleOrDefaultAsync(m => m.Id == id);
            if (switchFlag == null)
            {
                return NotFound();
            }

            return View(switchFlag);
        }

        // POST: SwitchFlags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var switchFlag = await _context.SwitchFlags.SingleOrDefaultAsync(m => m.Id == id);
            _context.SwitchFlags.Remove(switchFlag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SwitchFlagExists(int id)
        {
            return _context.SwitchFlags.Any(e => e.Id == id);
        }
    }
}
