using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xeber.Entity
{
    public class News 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }
        public string NewsTitle { get; set; }
        public int CategoryId { get; set; }
        public string NewsContent { get; set; }               
        public DateTime CreateDate { get; set; }
        [BindNever]
        public DateTime? UpdateDate { get; set; }
        public string NewsImg { get; set; }
        [BindNever]
        public int? ViewCount { get; set; }      
        public bool IsActiv { get; set; }
        public bool Deleted { get; set; }
        public Category Category { get; set; }
        public List<NewsLang> NewsLang { get; set; }
    }
}

