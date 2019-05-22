using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Faradata.FirstTest.Services;
using Nop.Services.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Faradata.FirstTest.Area.Api.Controllers
{
    [Area(FirstTestAreaNames.Api)]
    //[AuthenticationFilter]
    public class CategoryController : BasePluginController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICustomerTokenService _CustomerToken;

        public CategoryController(ICategoryService categoryService, ICustomerTokenService CustomerToken)
        {
            _categoryService = categoryService;
            _CustomerToken = CustomerToken;
        }

        public IActionResult Get()
        {
            var list = _CustomerToken.GetByToken("68fb3c1e-84b5-4207-96f4-658ca255f29d");
            return Ok(new
            {
                list = list
            });
        }

        public IActionResult GetFirst()
        {
            var rec = _categoryService.GetAllCategories().FirstOrDefault();
            return Ok(new
            {
                rec = rec
            });
        }

    }
}
