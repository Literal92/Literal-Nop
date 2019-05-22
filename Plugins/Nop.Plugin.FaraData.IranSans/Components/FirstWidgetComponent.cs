using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.IranSansWidget.Components
{
    [ViewComponent(Name = "IranSansWidget")]
    public class IranSansWidgetComponent : NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Faradata.IranSansWidget/Views/configure/PublicInfo.cshtml");
        }
    }
}
