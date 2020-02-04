﻿using Microsoft.AspNetCore.Mvc;
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
        public CategoryMenuViewComponent(ICategoryRepository repo)
        {
            _repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            return View(_repository.GetAll());
        }
    }
}
