using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xeber.Entity;
using Xeber.Repository.Abstract;

namespace Xeber.Repository.Concrete.EntityFramework
{
    public class efNewsRepository : INewsRepository
    {
        private NewsContext context;
        public efNewsRepository(NewsContext ctx)
        {
            context = ctx;
        }

        public void AddNews(News entity)
        {
            context.News.Add(entity);
            context.SaveChanges();
        }

        public void DeleteNews(int newsId)
        {
            var news = context.News.FirstOrDefault(p => p.NewsId == newsId);
            if (news != null)
            {
                context.News.Remove(news);
                context.SaveChanges();
            }
        }

        public IQueryable<News> GetAll()
        {

            return context.News.Where(i=>i.Deleted==false && i.IsActiv==true);
        }

        public IQueryable<News> GetAllAdmin()
        {

            return context.News;
        }

        public News GetById(int newsId)
        {
           return context.News.FirstOrDefault(p => p.NewsId == newsId);
        }

        public void SearchNews(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var query = GetAll().Where(i => EF.Functions.Like(i.NewsContent, "%" + q + "%") || EF.Functions.Like(i.NewsTitle, "%" + q + "%")
              && (i.IsActiv == true && i.Deleted == false)).OrderByDescending(i => i.CreateDate);
            }
            
        }

        public void UpdateNews(News entity)
        {
            var news = GetById(entity.NewsId);
            if (news !=null)
            {              
                if (entity.ViewCount==null)
                {
                    news.ViewCount = 0;
                }                                   
                news.NewsTitle = entity.NewsTitle;
                news.NewsContent = entity.NewsContent;
                news.Deleted = entity.Deleted;
                news.IsActiv = entity.IsActiv;
                if (entity.NewsImg!=null)
                {
                   news.NewsImg = entity.NewsImg;
                }              
                news.UpdateDate = DateTime.Now;
                context.SaveChanges();
            }
            context.SaveChanges();
        }
    }
}
