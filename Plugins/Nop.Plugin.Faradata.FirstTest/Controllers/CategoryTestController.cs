using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.FirstTest.Controllers
{
    [Area(FirstTestAreaNames.Admin)]
    [Authorize]
    public class CategoryTestController : BasePluginController
    {
        private readonly ICategoryService _categoryService;

        public CategoryTestController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View("~/Plugins/Faradata.FirstTest/Views/Category/Index.cshtml");
        }

    }
}
