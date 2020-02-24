using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using RestSharp;
using X.PagedList;
using Xeber.Entity;
using Xeber.Repository.Abstract;
using Xeber.Repository.Concrete.EntityFramework;

namespace Xeber.Controllers
{
    public class NewsController : Controller
    {
        //private INewsLangsRepository newsLangsRepository;
        private readonly IStringLocalizer<NewsController> _localizer;
        private INewsRepository repository;
        private NewsContext context;
        public NewsController(/*INewsLangsRepository _newsLangsRepository,*/ IStringLocalizer<NewsController> localizer, INewsRepository _repository, NewsContext context)
        {
            //newsLangsRepository = _newsLangsRepository;
            _localizer = localizer;
            repository = _repository;
            this.context = context;
        }
        public async Task<IActionResult> Index(string q,int? id)
        {
            var query = context.NewsLang.Include(n => n.News);
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

            var newsContext = context.NewsLang.Include(n => n.News).Where(i => i.LangId == langInt);
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

        public async Task<IActionResult> Details(int Id,NewsLang newslang)
        {
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
            if (Id == null)
            {
                return NotFound();
            }

            var newsLang = await context.NewsLang
                .Include(n =>n.News)
                .FirstOrDefaultAsync(m => m.NewsId == Id && m.LangId==langInt);
            var query = await context.NewsLang.Where(x => x.LangId == langInt && x.NewsId == Id).FirstOrDefaultAsync();
            //var query = await context.NewsLang.Where(x => x.Id == Id && x.LangId == langInt).SingleOrDefaultAsync();
            if (query == null)
            {
                return NotFound();
            }
            query.ViewCount += 1;
            newslang.ViewCount = query.ViewCount;
            context.Update(newsLang);         
            if (newsLang == null)
            {
                return NotFound();
            }
            //ViewBag.category = query.News.Category.CatName;
            //ViewBag.CatId = query.News.CategoryId;
            await context.SaveChangesAsync();
            return View(query);
        }    
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }



    }
}