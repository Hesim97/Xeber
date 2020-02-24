using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xeber.Entity;
using Xeber.Repository.Abstract;

namespace Xeber.Repository.Concrete.EntityFramework
{
    public class efNewsLangsRepository : INewsLangsRepository
    {
        private NewsContext context;
        public efNewsLangsRepository(NewsContext ctx)
        {
            context = ctx;
        }

        public void AddNewsLangs(NewsLang entity)
        {
            context.NewsLang.Add(entity);
            context.SaveChanges();
        }

        public void DeleteNewsLAngs(int Id)
        {

            var news = context.NewsLang.FirstOrDefault(p => p.Id == Id);
            if (news != null)
            {
                context.NewsLang.Remove(news);
                context.SaveChanges();
            }
        }

        public IQueryable<NewsLang> GetAll()
        {
            return context.NewsLang.Where(i => i.News.Deleted == false && i.News.IsActiv == true);
        }

        public IQueryable<NewsLang> GetAllAdmin()
        {
            return context.NewsLang;
        }

        public NewsLang GetById(int id)
        {
            return context.NewsLang.FirstOrDefault(p => p.Id== id);
        }

        public void SearchNewsLangs(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var query = GetAll().Where(i => EF.Functions.Like(i.NewsContent, "%" + q + "%") || EF.Functions.Like(i.NewsTitle, "%" + q + "%")
              && (i.News.IsActiv == true && i.News.Deleted == false)).OrderByDescending(i => i.CreateDate);
            }
        }

        public void UpdateNewsLangs(NewsLang entity)
        {
            var newslang = GetById(entity.Id);
            if (newslang != null)
            {
                if (entity.ViewCount == null)
                {
                    newslang.ViewCount = 0;
                }
                newslang.NewsTitle = entity.NewsTitle;
                newslang.NewsContent = entity.NewsContent;


                newslang.UpdateDate = DateTime.Now;
                context.SaveChanges();
            }
            context.SaveChanges();
        }
    }
}
