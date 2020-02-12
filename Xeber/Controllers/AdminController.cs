using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Xeber.Entity;
using Xeber.Models;
using Xeber.Repository.Abstract;
using Xeber.Repository.Concrete.EntityFramework;

namespace Xeber.Controllers
{

    public class AdminController : Controller
    {
        private INewsRepository newsRepository;
        private ICategoryRepository categoryRepository;

        public AdminController(INewsRepository _repository, ICategoryRepository _categoryRepository)
        {
            newsRepository = _repository;
            categoryRepository = _categoryRepository;
        }
        // GET: Admin
        [Authorize]
        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 3;
            var query = newsRepository.GetAllAdmin().ToList().ToPagedList(pageNumber, pageSize);
            //var newsContext = _context.News.Include(n => n.Category).ToPagedList(1,5);
            return View(query);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "Id", "CatName");
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(News entity,IFormFile file)
        {
            
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                entity.IsActiv = true;
                entity.ViewCount = 0;
                entity.NewsImg = file.FileName;

                newsRepository.AddNews(entity);
                return RedirectToAction("Index");
            } 
          
            return View(entity);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "Id", "CatName");
            return View(newsRepository.GetById(id));
        }

        [HttpPost]
        public async Task <IActionResult> Edit(News entity, IFormFile file)
        {
           
            if (ModelState.IsValid)
            {
                
                //News tbl_News = new News();
                if (file != null)
                {
                    //Set Key Name
                    string FileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    //Get url To Save
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", FileName);
                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                       await file.CopyToAsync(stream);
                    }
                    if (file.FileName==null)
                    {
                        
                    }
                            
                    entity.NewsImg = file.FileName; 
                } 
                      newsRepository.UpdateNews(entity);
                      return RedirectToAction("Index");
                              
            }
            ViewData["CategoryId"] = new SelectList(categoryRepository.GetAll(), "Id", "CatName");
            return View(entity);
        }
       
        [HttpGet]
        public IActionResult Delete(int id)
        {
          
            return View(newsRepository.GetById(id));
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int newsId)
        {

            newsRepository.DeleteNews(newsId);
            TempData["message"] = $"{newsId} nomreli xeber silindi";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(newsRepository.GetById(id));
        }
    }
}
