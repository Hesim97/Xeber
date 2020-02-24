using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xeber.Entity;

namespace Xeber.Repository.Abstract
{
    public interface INewsLangsRepository
    {
        NewsLang GetById(int id);
        IQueryable<NewsLang> GetAll();
        IQueryable<NewsLang> GetAllAdmin();
        void AddNewsLangs(NewsLang entity);
        void UpdateNewsLangs(NewsLang entity);
        void DeleteNewsLAngs(int Id);
        void SearchNewsLangs(string q);
    }
}
