using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xeber.Entity;
using Xeber.Repository.Abstract;

namespace Xeber.Repository.Concrete.EntityFramework
{
    public class efCategoryRepository : ICategoryRepository
    {
        private NewsContext context;
        public efCategoryRepository(NewsContext ctx)
        {
            context = ctx;
        }
        public void AddCategory(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = context.Categories.FirstOrDefault(p => p.Id == categoryId);
            if (category!=null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories;
        }

        public Category GetById(int categoryId)
        {
            return context.Categories.FirstOrDefault(p => p.Id == categoryId);
        }

        public void UpdateCategory(Category entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
