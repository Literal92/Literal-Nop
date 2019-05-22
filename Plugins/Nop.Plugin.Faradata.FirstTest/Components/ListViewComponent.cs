using Microsoft.AspNetCore.Mvc;
using Nop.Services.Catalog;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.FirstTest.Components
{
    [ViewComponent(Name = "Faradata_FirstTest_ListCategory")]
    public class ListViewComponent : NopViewComponent
    {
        private readonly ICategoryService _categoryService;

        public ListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var list = _categoryService.GetAllCategories();
            return View("~/Plugins/Faradata.FirstTest/Views/Category/_List.cshtml", list);
        }
    }
}
