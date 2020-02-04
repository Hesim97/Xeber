using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xeber.Entity;

namespace Xeber.Repository.Abstract
{
   public interface ICategoryRepository
    {
        Category GetById(int categoryId);
        IQueryable<Category> GetAll();
        void AddCategory(Category entity);
        void UpdateCategory(Category entity);
        void DeleteCategory(int categoryId);
    }
}
