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

        public IQueryable<News> News => context.News;

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        //public void Update(News news)
        //{
        //    context.Entry(news).State = EntityState.Modified();
        //}
    }
}
