using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Xeber.Entity
{
    public class Langs
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DisplayName { get; set; }
        [Required]
        public int Priority { get; set; }
        public List<NewsLang> NewsLang { get; set; }
    }
}
