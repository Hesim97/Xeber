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
        private readonly IStringLocalizer<NewsController> _localizer;
        private INewsRepository repository;
        private NewsContext context;
        public NewsController(IStringLocalizer<NewsController> localizer,INewsRepository _repository, NewsContext context)
        {
            _localizer = localizer;
            repository = _repository;
            this.context = context;
        }
        public IActionResult Index(string q,int? id,int? page)
        {

            var query = repository.GetAll();
            if (id!=null)
            {
                query = query.Where(i => i.CategoryId ==id);

            }          

            if (!string.IsNullOrEmpty(q))
            {
                query = repository.GetAll().Where(i => EF.Functions.Like(i.NewsContent, "%" + q + "%") || EF.Functions.Like(i.NewsTitle, "%" + q + "%")
                   && (i.IsActiv == true && i.Deleted == false));
            }
            ViewBag.q = q;
            ViewBag.sl = id;
            //var pageNumber = page ?? 1;
            //int pageSize = 6;
            //var query1 = query.ToList().ToPagedList(pageNumber, pageSize);            
            //ViewBag.pg = pageSize;
            return  View(query);  
            
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id, News news)
        {
            var news1 = await repository.GetAll()
                .Include(n => n.Category)
               .FirstOrDefaultAsync(m => m.NewsId == Id);

            var query = await repository.GetAll().Where(x => x.NewsId == Id).SingleOrDefaultAsync();
            query.ViewCount += 1;
            news.ViewCount = query.ViewCount;
            context.Update(news1);
            ViewBag.category = query.Category.CatName;
            ViewBag.CatId = query.CategoryId;
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