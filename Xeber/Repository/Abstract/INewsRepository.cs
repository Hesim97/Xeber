using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xeber.Entity;

namespace Xeber.Repository.Abstract
{
    public interface INewsRepository
    {
        IQueryable<News> News { get; }

    }
}
