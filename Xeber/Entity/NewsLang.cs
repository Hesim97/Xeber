using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xeber.Entity
{
    public class NewsLang
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int LangId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public int? ViewCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public News News { get; set; }
        public Langs Langs { get; set; }

    }
}
