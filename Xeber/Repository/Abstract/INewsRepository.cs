using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xeber.Entity;

namespace Xeber.Repository.Abstract
{
    public interface INewsRepository
    {
        News GetById(int newsId);
        IQueryable<News> GetAll();
        IQueryable<News> GetAllAdmin();
        void AddNews(News entity);
        void UpdateNews(News entity);
        void DeleteNews(int newsId);
        void SearchNews(string q);
    }
}
