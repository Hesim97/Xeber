using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xeber.Entity;
using Xeber.Repository.Concrete.EntityFramework;

namespace Xeber.Controllers
{
    public class LangsController : Controller
    {
        private readonly NewsContext _context;

        public LangsController(NewsContext context)
        {
            _context = context;
        }

        // GET: Langs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Langs.ToListAsync());
        }

        // GET: Langs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langs = await _context.Langs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langs == null)
            {
                return NotFound();
            }

            return View(langs);
        }

        // GET: Langs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Langs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DisplayName,Priority")] Langs langs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(langs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(langs);
        }

        // GET: Langs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langs = await _context.Langs.FindAsync(id);
            if (langs == null)
            {
                return NotFound();
            }
            return View(langs);
        }

        // POST: Langs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DisplayName,Priority")] Langs langs)
        {
            if (id != langs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(langs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LangsExists(langs.Id))
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
            return View(langs);
        }

        // GET: Langs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langs = await _context.Langs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langs == null)
            {
                return NotFound();
            }

            return View(langs);
        }

        // POST: Langs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var langs = await _context.Langs.FindAsync(id);
            _context.Langs.Remove(langs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LangsExists(int id)
        {
            return _context.Langs.Any(e => e.Id == id);
        }
    }
}
