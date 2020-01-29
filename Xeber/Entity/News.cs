using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xeber.Entity
{
    public class News 
    {
        public int NewsId { get; set; }
        public string NewsTitle { get; set; }
        public int CategoryId { get; set; }
        public string NewsContent { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string NewsImg { get; set; }
        public int? ViewCount { get; set; }
        public bool IsActiv { get; set; }
        public bool Deleted { get; set; }
        public Category Category { get; set; }
    }
}

