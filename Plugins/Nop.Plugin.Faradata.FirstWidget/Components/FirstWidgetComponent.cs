using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.FirstWidget.Components
{
    [ViewComponent(Name = "FirstWidget")]
    public class FirstWidgetComponent : NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Faradata.FirstWidget/Views/configure/PublicInfo.cshtml");
        }
    }
}
