using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.FirstTest.Controllers
{
    [Area(FirstTestAreaNames.Admin)]
    public class FaradataFirstTestController : BasePluginController
    {
        private readonly ICategoryService _categoryService;

        public FaradataFirstTestController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Configure()
        {
            return View("~/Plugins/Faradata.FirstTest/Views/Configure.cshtml");
        }

        public IActionResult Get()
        {
            return View("~/Plugins/Faradata.FirstTest/Views/FaradataFirstTest/Get.cshtml");
        }

        public IActionResult Index()
        {
            return View("~/Plugins/Faradata.FirstTest/Views/FaradataFirstTest/Index.cshtml");
        }

    }
}
