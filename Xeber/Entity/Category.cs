using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xeber.Entity
{
    public class Category
    {
        //public Category()
        //{
        //    News = new HashSet<News>();
        //}

        public int Id { get; set; }
        public string CatName { get; set; }
        public string CatImg { get; set; }

        public List<News> News { get; set; }
    }
}

