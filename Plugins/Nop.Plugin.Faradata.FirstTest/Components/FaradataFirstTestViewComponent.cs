using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.FirstTest.Components
{
    public class FaradataFirstTestViewComponent: NopViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Plugins/Faradata.FirstTest/Views/PublicInfo.cshtml");
        }
    }
}
