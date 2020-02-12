using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xeber.Repository.Abstract;

namespace Xeber.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private ICategoryRepository _repository;
        private readonly IStringLocalizer<CategoryMenuViewComponent> localizer;
        public CategoryMenuViewComponent(ICategoryRepository repo, IStringLocalizer<CategoryMenuViewComponent> localizer)
        {
            _repository = repo;
            this.localizer = localizer;
        }
        public IViewComponentResult Invoke()
        {

            return View(_repository.GetAll());
        }
    }
}
