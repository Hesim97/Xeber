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
    public class NewsLangsController : Controller
    {
        private readonly NewsContext _context;

        public NewsLangsController(NewsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string q, int? id)
        {
            var query = _context.NewsLang.Include(n => n.News);
            int langInt = 0;
            var lang = Request.Cookies["lang"];
            switch (lang)
            {
                case "az":
                    {
                        langInt = (int)LangEnums.az;
                        break;
                    }
                case "en":
                    {
                        langInt = (int)LangEnums.en;
                        break;
                    }
                case "ru":
                    {
                        langInt = (int)LangEnums.ru;
                        break;
                    }
                default:
                    break;
            }

            var newsContext = _context.NewsLang.Include(n => n.News).Where(i => i.LangId == langInt);
            if (!string.IsNullOrEmpty(q))
            {
                newsContext = newsContext.Where(i => EF.Functions.Like(i.NewsContent, "%" + q + "%") || EF.Functions.Like(i.NewsTitle, "%" + q + "%")
                   && (i.News.IsActiv == true && i.News.Deleted == false));
            }
            if (id != null)
            {
                newsContext = newsContext.Where(i => i.News.CategoryId == id);

            }
            ViewBag.q = q;
            ViewBag.sl = id;
            //var pageNumber = page ?? 1;
            //int pageSize = 6;
            //var query1 = query.ToList().ToPagedList(pageNumber, pageSize);
            //ViewBag.pg = pageSize;
            return View(await newsContext.ToListAsync());
        }
        // GET: NewsLangs
        //public async Task<IActionResult> Index()
        //{
        //    var newsContext = _context.NewsLang.Include(n => n.News);
        //    return View(await newsContext.ToListAsync());
        //}

        //// GET: NewsLangs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var newsLang = await _context.NewsLang
        //        .Include(n => n.News)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (newsLang == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(newsLang);
        //}

        // GET: NewsLangs/Create
        public IActionResult Create()
        {
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "NewsTitle");
            ViewData["LangName"] = new SelectList(_context.Langs, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsLang newsLang)
        {
            if (ModelState.IsValid)
            {
                newsLang.CreateDate = DateTime.Now;
                newsLang.ViewCount = 0;
                _context.Add(newsLang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "NewsTitle", newsLang.NewsId);
            ViewData["LangName"] = new SelectList(_context.Langs, "Id", "Name",newsLang.LangId);
            return View(newsLang);
        }

        // GET: NewsLangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLang = await _context.NewsLang.FindAsync(id);
            newsLang.UpdateDate = DateTime.Now;
            if (newsLang == null)
            {
                return NotFound();
            }
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "NewsTitle", newsLang.NewsId);
            ViewData["LangName"] = new SelectList(_context.Langs, "Id", "Name", newsLang.LangId);
            return View(newsLang);
        }

        // POST: NewsLangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NewsId,LangId,NewsTitle,NewsContent,ViewCount,CreateDate,UpdateDate")] NewsLang newsLang)
        {
            if (id != newsLang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    newsLang.UpdateDate = DateTime.Now;
                    newsLang.ViewCount = 0;
                    _context.Update(newsLang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsLangExists(newsLang.Id))
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
            ViewData["NewsId"] = new SelectList(_context.News, "Id", "NewsTitle", newsLang.NewsId);
            ViewData["LangName"] = new SelectList(_context.Langs, "Id", "Name", newsLang.LangId);
            return View(newsLang);
        }

        // GET: NewsLangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLang = await _context.NewsLang
                .Include(n => n.News)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsLang == null)
            {
                return NotFound();
            }

            return View(newsLang);
        }

        // POST: NewsLangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsLang = await _context.NewsLang.FindAsync(id);
            _context.NewsLang.Remove(newsLang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsLangExists(int id)
        {
            return _context.NewsLang.Any(e => e.Id == id);
        }
    }
}
